// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.ServiceModel;
using dgt.power.connection.Commands;
using dgt.power.connection.tests.Base;
using dgt.power.tests.Extensions;
using dgt.power.common.Logic;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;

namespace dgt.power.connection.tests;

[NotInParallel("Serial_Connection_Tests")]
public class CreateConnectionCommandTests : ConnectionTestsBase<CreateConnectionCommand, CreateConnectionSettings>
{
    private const string ConnectionString = @"AuthType=OAuth;
  Username=jsmith@contoso.onmicrosoft.com;
  Password=passcode;
  Url=https://contosotest.crm.dynamics.com;
  AppId=51f81489-12ee-4a9e-aaae-a2591f45987d;
  RedirectUri=app://58145B91-0C36-4500-8554-080854F2AC97;
  LoginPrompt=Auto";

    [Test]
    public async Task ShouldSaveCreatedConnectionAsCurrent()
    {
        var settings = new CreateConnectionSettings
        {
            Name = "TEST",
            ConnectionString = ConnectionString
        };

        await GetContext().Execute(settings).Succeed();

        await Assert.That(GetIdentities().Current).IsEqualTo(settings.Name);
        await Assert.That(GetIdentities().CurrentConnectionString).IsEqualTo(settings.ConnectionString);
    }

    [Test]
    public async Task ShouldCreateTokenIdentity_WhenUrlIsProvided()
    {
#pragma warning disable S1075
        var settings = new CreateConnectionSettings
        {
            Name = "TOKEN",
            Url = "https://contoso.crm.dynamics.com",
            NoVerify = true
        };
#pragma warning restore S1075

        await GetContext().Execute(settings).Succeed();

        await Assert.That(GetIdentities().Current).IsEqualTo(settings.Name);
        await Assert.That(GetIdentities().CurrentConnectionString).IsEqualTo(settings.Url);
        await Assert.That(ProfileManager.CurrentIdentity is TokenIdentity).IsTrue();
    }

    [Test]
    public async Task ShouldSkipConnectionCheck()
    {
        var settings = new CreateConnectionSettings
        {
            Name = "TEST",
            ConnectionString = ConnectionString,
            NoVerify = true
        };

        var context = GetBuilder()
            .WithExecutionMock<WhoAmIRequest>(_ => throw new FaultException<OrganizationServiceFault>(new OrganizationServiceFault()))
            .Build();

        await context.Execute(settings).Succeed();

        await Assert.That(GetIdentities().Current).IsEqualTo(settings.Name);
        await Assert.That(GetIdentities().CurrentConnectionString).IsEqualTo(settings.ConnectionString);
    }

    [Test]
    public async Task ShouldFailOnInvalidConnection()
    {
        var settings = new CreateConnectionSettings
        {
            Name = "test",
            ConnectionString = ConnectionString
        };

        var context = GetBuilder()
            .WithExecutionMock<WhoAmIRequest>(_ => throw new FaultException<OrganizationServiceFault>(new OrganizationServiceFault()))
            .Build();

        await Assert.That(() => context.Execute(settings)).ThrowsExactly<FaultException<OrganizationServiceFault>>();
    }

    [Test]
    public async Task ShouldNotPersistIdentity_WhenConnectionCheckFails()
    {
        var settings = new CreateConnectionSettings
        {
            Name = "BROKEN",
            ConnectionString = ConnectionString
        };

        var context = GetBuilder()
            .WithExecutionMock<WhoAmIRequest>(_ => throw new FaultException<OrganizationServiceFault>(new OrganizationServiceFault()))
            .Build();

        await Assert.That(() => context.Execute(settings)).ThrowsExactly<FaultException<OrganizationServiceFault>>();

        await Assert.That(GetIdentities().Contains(settings.Name)).IsFalse();
    }

    [Test]
    public async Task ShouldNotChangeCurrentIdentity_WhenNewIdentityCreationFails()
    {
        var existingSettings = new CreateConnectionSettings
        {
            Name = "GOOD",
            ConnectionString = ConnectionString
        };
        await GetContext().Execute(existingSettings).Succeed();
        await Assert.That(GetIdentities().Current).IsEqualTo(existingSettings.Name);

        var brokenSettings = new CreateConnectionSettings
        {
            Name = "BROKEN",
            ConnectionString = ConnectionString
        };
        var context = GetBuilder()
            .WithExecutionMock<WhoAmIRequest>(_ => throw new FaultException<OrganizationServiceFault>(new OrganizationServiceFault()))
            .Build();

        await Assert.That(() => context.Execute(brokenSettings)).ThrowsExactly<FaultException<OrganizationServiceFault>>();

        await Assert.That(GetIdentities().Current).IsEqualTo(existingSettings.Name);
        await Assert.That(GetIdentities().Contains(brokenSettings.Name)).IsFalse();
    }
}
