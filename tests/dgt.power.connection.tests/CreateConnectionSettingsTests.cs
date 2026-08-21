// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.connection.Commands;

namespace dgt.power.connection.tests;

public class CreateConnectionSettingsTests
{
    [Test]
    public async Task ShouldBeValid_WhenOnlyUrlProvided()
    {
#pragma warning disable S1075
        var settings = new CreateConnectionSettings { Name = "TEST", Url = "https://contoso.crm.dynamics.com" };
#pragma warning restore S1075

        await Assert.That(settings.Validate().Successful).IsTrue();
    }

    [Test]
    public async Task ShouldBeValid_WhenOnlyConnectionStringProvided()
    {
        var settings = new CreateConnectionSettings { Name = "TEST", ConnectionString = "AuthType=OAuth;Url=https://contoso.crm.dynamics.com" };

        await Assert.That(settings.Validate().Successful).IsTrue();
    }

    [Test]
    public async Task ShouldBeValid_WhenFederatedOptionsFullyProvided()
    {
#pragma warning disable S1075
        var settings = new CreateConnectionSettings
        {
            Name = "TEST",
            Url = "https://contoso.crm.dynamics.com",
            AzureDevOpsFederated = true,
            TenantId = "tenant",
            ApplicationId = "app",
            ServiceConnectionId = "sc"
        };
#pragma warning restore S1075

        await Assert.That(settings.Validate().Successful).IsTrue();
    }

    [Test]
    public async Task ShouldFail_WhenNeitherUrlNorConnectionStringProvided()
    {
        var settings = new CreateConnectionSettings { Name = "TEST" };

        await Assert.That(settings.Validate().Successful).IsFalse();
    }

    [Test]
    public async Task ShouldFail_WhenBothUrlAndConnectionStringProvided()
    {
#pragma warning disable S1075
        var settings = new CreateConnectionSettings
        {
            Name = "TEST",
            Url = "https://contoso.crm.dynamics.com",
            ConnectionString = "AuthType=OAuth;Url=https://contoso.crm.dynamics.com"
        };
#pragma warning restore S1075

        await Assert.That(settings.Validate().Successful).IsFalse();
    }

    [Test]
    public async Task ShouldFail_WhenFederatedAndConnectionStringBothProvided()
    {
#pragma warning disable S1075
        var settings = new CreateConnectionSettings
        {
            Name = "TEST",
            AzureDevOpsFederated = true,
            Url = "https://contoso.crm.dynamics.com",
            ConnectionString = "AuthType=OAuth;Url=https://contoso.crm.dynamics.com",
            TenantId = "tenant",
            ApplicationId = "app",
            ServiceConnectionId = "sc"
        };
#pragma warning restore S1075

        await Assert.That(settings.Validate().Successful).IsFalse();
    }

    [Test]
    public async Task ShouldFail_WhenFederatedWithoutUrl()
    {
        var settings = new CreateConnectionSettings
        {
            Name = "TEST",
            AzureDevOpsFederated = true,
            TenantId = "tenant",
            ApplicationId = "app",
            ServiceConnectionId = "sc"
        };

        await Assert.That(settings.Validate().Successful).IsFalse();
    }

    [Test]
    public async Task ShouldFail_WhenFederatedMissingRequiredOptions()
    {
#pragma warning disable S1075
        var settings = new CreateConnectionSettings
        {
            Name = "TEST",
            AzureDevOpsFederated = true,
            Url = "https://contoso.crm.dynamics.com"
        };
#pragma warning restore S1075

        await Assert.That(settings.Validate().Successful).IsFalse();
    }

    [Test]
    public async Task ShouldFail_WhenFederatedOptionsProvidedWithoutFlag()
    {
#pragma warning disable S1075
        var settings = new CreateConnectionSettings
        {
            Name = "TEST",
            Url = "https://contoso.crm.dynamics.com",
            TenantId = "tenant"
        };
#pragma warning restore S1075

        await Assert.That(settings.Validate().Successful).IsFalse();
    }
}
