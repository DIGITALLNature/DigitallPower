// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using dgt.power.webresource.Repositories;
using Digitall.Dataverse.Testing;
using Microsoft.Xrm.Sdk;

namespace dgt.power.webresource.tests.Repositories;

public class WebResourceRepositoryTests
{
    [Test]
    public async Task FindByNamesAsync_FiltersByNameAndMapsRemoteFields()
    {
        var service = CreateService();
        var expected = new WebResource(Guid.NewGuid())
        {
            Name = "contoso_/app.js",
            Content = "Y29udGVudA==",
            WebResourceType = new OptionSetValue(3),
            Attributes = { [WebResource.LogicalNames.IsManaged] = true }
        };
        service.Create(expected);
        service.Create(new WebResource(Guid.NewGuid())
        {
            Name = "contoso_/other.js",
            Content = "b3RoZXI=",
            WebResourceType = new OptionSetValue(3),
            Attributes = { [WebResource.LogicalNames.IsManaged] = false }
        });
        var repository = new WebResourceRepository(service);

        var result = await repository.FindByNamesAsync(["contoso_/app.js"]);

        await Assert.That(result).Count().IsEqualTo(1);
        await Assert.That(result[0].Id).IsEqualTo(expected.Id);
        await Assert.That(result[0].Name).IsEqualTo("contoso_/app.js");
        await Assert.That(result[0].Content).IsEqualTo("Y29udGVudA==");
        await Assert.That(result[0].Type).IsEqualTo(3);
        await Assert.That(result[0].IsManaged).IsTrue();
    }

    private static FakeOrganizationServiceAsync CreateService()
    {
        var service = new FakeOrganizationServiceAsync();
        service.AddDefaultRequests();
        return service;
    }
}
