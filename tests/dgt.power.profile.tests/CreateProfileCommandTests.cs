// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.ServiceModel;
using dgt.power.profile.Commands;
using dgt.power.profile.tests.Base;
using dgt.power.tests.Extensions;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;

namespace dgt.power.profile.tests;

[Collection("Serial_Profile_Tests")]
public class CreateProfileCommandTests : ProfileTestsBase<CreateProfileCommand, CreateProfileSettings>
{
    public CreateProfileCommandTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    [Fact]
    public void ShouldSaveCreatedProfileAsCurrent()
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

        GetContext().Execute(settings).Should().Succeed();

        GetIdentities().Current.Should().Be(settings.Name);
        GetIdentities().CurrentConnectionString.Should().Be(settings.ConnectionString);
    }

    [Fact]
    public void ShouldSkipConnectionCheck()
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
  LoginPrompt=Auto",
            SkipChecking = true
        };

        var context = GetBuilder()
            .WithExecutionMock<WhoAmIRequest>(_ => throw new FaultException<OrganizationServiceFault>(new OrganizationServiceFault()))
            .Build();

        context.Execute(settings).Should().Succeed();

        GetIdentities().Current.Should().Be(settings.Name);
        GetIdentities().CurrentConnectionString.Should().Be(settings.ConnectionString);
    }

    [Fact]
    public void ShouldFailOnInvalidConnection()
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
  LoginPrompt=Auto",
        };

        var context = GetBuilder()
            .WithExecutionMock<WhoAmIRequest>(_ => throw new FaultException<OrganizationServiceFault>(new OrganizationServiceFault()))
            .Build();

        Action actor = () => context.Execute(settings);

        actor.Should().ThrowExactly<FaultException<OrganizationServiceFault>>();
    }
}
