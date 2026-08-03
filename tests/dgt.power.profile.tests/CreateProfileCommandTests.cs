// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.ServiceModel;
using dgt.power.profile.Commands;
using dgt.power.profile.tests.Base;
using dgt.power.tests.Extensions;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;

namespace dgt.power.profile.tests;

[NotInParallel("Serial_Profile_Tests")]
public class CreateProfileCommandTests : ProfileTestsBase<CreateProfileCommand, CreateProfileSettings>
{
    [Test]
    public async Task ShouldSaveCreatedProfileAsCurrent()
    {
        var settings = new CreateProfileSettings
        {
            Name = "TEST",
            ConnectionString = @"AuthType=OAuth;
  Username=jsmith@contoso.onmicrosoft.com;
  Password=passcode;
  Url=https://contosotest.crm.dynamics.com;
  AppId=51f81489-12ee-4a9e-aaae-a2591f45987d;
  RedirectUri=app://58145B91-0C36-4500-8554-080854F2AC97;
  TokenCacheStorePath=c:\MyTokenCache;
  LoginPrompt=Auto"
        };

        await GetContext().Execute(settings).Succeed();

        await Assert.That(GetIdentities().Current).IsEqualTo(settings.Name);
        await Assert.That(GetIdentities().CurrentConnectionString).IsEqualTo(settings.ConnectionString);
    }

    [Test]
    public async Task ShouldSkipConnectionCheck()
    {

        var settings = new CreateProfileSettings
        {
            Name = "TEST",
            ConnectionString = @"AuthType=OAuth;
  Username=jsmith@contoso.onmicrosoft.com;
  Password=passcode;
  Url=https://contosotest.crm.dynamics.com;
  AppId=51f81489-12ee-4a9e-aaae-a2591f45987d;
  RedirectUri=app://58145B91-0C36-4500-8554-080854F2AC97;
  TokenCacheStorePath=c:\MyTokenCache;
  LoginPrompt=Auto",
            SkipChecking = true
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

        var settings = new CreateProfileSettings
        {
            Name = "test",
            ConnectionString = @"AuthType=OAuth;
  Username=jsmith@contoso.onmicrosoft.com;
  Password=passcode;
  Url=https://contosotest.crm.dynamics.com;
  AppId=51f81489-12ee-4a9e-aaae-a2591f45987d;
  RedirectUri=app://58145B91-0C36-4500-8554-080854F2AC97;
  TokenCacheStorePath=c:\MyTokenCache;
  LoginPrompt=Auto"
        };

        var context = GetBuilder()
            .WithExecutionMock<WhoAmIRequest>(_ => throw new FaultException<OrganizationServiceFault>(new OrganizationServiceFault()))
            .Build();

        await Assert.That(() => context.Execute(settings)).ThrowsExactly<FaultException<OrganizationServiceFault>>();
    }

    [Test]
    public async Task ShouldNotPersistIdentity_WhenConnectionCheckFails()
    {
        var settings = new CreateProfileSettings
        {
            Name = "BROKEN",
            ConnectionString = @"AuthType=OAuth;
  Username=jsmith@contoso.onmicrosoft.com;
  Password=passcode;
  Url=https://contosotest.crm.dynamics.com;
  AppId=51f81489-12ee-4a9e-aaae-a2591f45987d;
  RedirectUri=app://58145B91-0C36-4500-8554-080854F2AC97;
  TokenCacheStorePath=c:\MyTokenCache;
  LoginPrompt=Auto"
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
        var existingSettings = new CreateProfileSettings
        {
            Name = "GOOD",
            ConnectionString = @"AuthType=OAuth;
  Username=jsmith@contoso.onmicrosoft.com;
  Password=passcode;
  Url=https://contosotest.crm.dynamics.com;
  AppId=51f81489-12ee-4a9e-aaae-a2591f45987d;
  RedirectUri=app://58145B91-0C36-4500-8554-080854F2AC97;
  TokenCacheStorePath=c:\MyTokenCache;
  LoginPrompt=Auto"
        };
        await GetContext().Execute(existingSettings).Succeed();
        await Assert.That(GetIdentities().Current).IsEqualTo(existingSettings.Name);

        var brokenSettings = new CreateProfileSettings
        {
            Name = "BROKEN",
            ConnectionString = existingSettings.ConnectionString
        };
        var context = GetBuilder()
            .WithExecutionMock<WhoAmIRequest>(_ => throw new FaultException<OrganizationServiceFault>(new OrganizationServiceFault()))
            .Build();

        await Assert.That(() => context.Execute(brokenSettings)).ThrowsExactly<FaultException<OrganizationServiceFault>>();

        await Assert.That(GetIdentities().Current).IsEqualTo(existingSettings.Name);
        await Assert.That(GetIdentities().Contains(brokenSettings.Name)).IsFalse();
    }
}
