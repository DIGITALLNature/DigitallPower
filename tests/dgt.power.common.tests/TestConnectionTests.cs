// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.ServiceModel;
using dgt.power.tests;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;

namespace dgt.power.common.tests;

public class TestConnectionTests
{
    [Test]
    public async Task ConfiguredCallers_RouteInterleavedRequestsAndPropagateFaultWithoutFallback()
    {
        var standard = Mock.Of<IOrganizationServiceAsync2>();
        var first = Mock.Of<IOrganizationServiceAsync2>();
        var second = Mock.Of<IOrganizationServiceAsync2>();
        var connection = new TestConnection(standard.Object);
        var firstId = Guid.NewGuid();
        var secondId = Guid.NewGuid();
        connection.CallerServices.Add(firstId, first.Object);
        connection.CallerServices.Add(secondId, second.Object);
        var request = new OrganizationRequest("TestOperation");
        var routes = new List<string>();
        var response = new OrganizationResponse();
        standard.ExecuteAsync(request, CancellationToken.None).Returns(() =>
        {
            routes.Add("standard");
            return response;
        });
        first.ExecuteAsync(request, CancellationToken.None).Returns(() =>
        {
            routes.Add("first");
            return response;
        });
        second.ExecuteAsync(request, CancellationToken.None).Returns(() =>
        {
            routes.Add("second");
            return response;
        });
        var healthChecks = 0;
        standard.Execute(Any<OrganizationRequest>()).Returns(() =>
        {
            healthChecks++;
            return new WhoAmIResponse();
        });

        var standardService = await connection.GetServiceAsync();
        var firstService = await connection.GetServiceAsync(firstId);
        var secondService = await connection.GetServiceAsync(secondId);
        await standardService.ExecuteAsync(request, CancellationToken.None);
        await firstService.ExecuteAsync(request, CancellationToken.None);
        await secondService.ExecuteAsync(request, CancellationToken.None);
        await standardService.ExecuteAsync(request, CancellationToken.None);

        var fault = new FaultException<OrganizationServiceFault>(
            new OrganizationServiceFault { Message = "Dataverse denied the caller operation." });
        var failing = new OrganizationRequest("DeniedOperation");
        first.ExecuteAsync(failing, CancellationToken.None).Throws(fault);
        var attempt = firstService.ExecuteAsync(failing, CancellationToken.None);
        await Assert.That(attempt).Throws<FaultException<OrganizationServiceFault>>();
        await Assert.That(ReferenceEquals(attempt.Exception?.InnerException, fault)).IsTrue();
        await Assert.That(string.Join(",", routes)).IsEqualTo("standard,first,second,standard");
        await Assert.That(healthChecks).IsEqualTo(1);
        await Assert.That(ReferenceEquals(await connection.GetServiceAsync(), standardService)).IsTrue();
        await Assert.That(healthChecks).IsEqualTo(2);
    }

    [Test]
    public async Task UnknownAndEmptyCallers_ThrowWithoutUsingStandardService()
    {
        var standard = Mock.Of<IOrganizationServiceAsync2>();
        var connection = new TestConnection(standard.Object);
        var healthChecks = 0;
        standard.Execute(Any<OrganizationRequest>()).Returns(() =>
        {
            healthChecks++;
            return new WhoAmIResponse();
        });

        await Assert.That(async () => await connection.GetServiceAsync(Guid.NewGuid())).Throws<NotSupportedException>();
        await Assert.That(async () => await connection.GetServiceAsync(Guid.Empty)).Throws<ArgumentException>();
        await Assert.That(healthChecks).IsEqualTo(0);
        await Assert.That(connection.CallerServices.Count).IsEqualTo(0);
    }
}
