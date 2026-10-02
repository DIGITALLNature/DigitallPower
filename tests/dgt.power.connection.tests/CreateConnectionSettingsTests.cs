// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.connection.Commands;

namespace dgt.power.connection.tests;

public class CreateConnectionSettingsTests
{
    [Test]
    public async Task UserConnectionRequiresTenant()
    {
        var settings = new CreateConnectionSettings { Name = "dev", Url = "https://contoso.crm.dynamics.com" };

        await Assert.That(settings.Validate().Successful).IsFalse();
    }

    [Test]
    public async Task UserConnectionRejectsWhitespaceUrl()
    {
        var settings = new CreateConnectionSettings
        {
            Name = "dev",
            Url = " ",
            TenantId = "contoso.onmicrosoft.com"
        };

        await Assert.That(settings.Validate().Successful).IsFalse();
    }

    [Test]
    public async Task UserConnectionAcceptsUrlAndTenant()
    {
        var settings = new CreateConnectionSettings
        {
            Name = "dev",
            Url = "https://contoso.crm.dynamics.com",
            TenantId = "contoso.onmicrosoft.com"
        };

        await Assert.That(settings.Validate().Successful).IsTrue();
    }

    [Test]
    public async Task ClientSecretRequiresApplicationId()
    {
        var settings = new CreateConnectionSettings
        {
            Name = "prod",
            Url = "https://contoso.crm.dynamics.com",
            TenantId = "tenant",
            ClientSecret = true
        };

        await Assert.That(settings.Validate().Successful).IsFalse();
    }

    [Test]
    public async Task ClientSecretAcceptsApplicationId()
    {
        var settings = new CreateConnectionSettings
        {
            Name = "prod",
            Url = "https://contoso.crm.dynamics.com",
            TenantId = "tenant",
            ApplicationId = "client",
            ClientSecret = true
        };

        await Assert.That(settings.Validate().Successful).IsTrue();
    }

    [Test]
    public async Task FederatedConnectionAcceptsServiceConnectionName()
    {
        var settings = new CreateConnectionSettings
        {
            Name = "pipeline",
            AzureDevOpsFederated = true,
            ServiceConnectionName = "Power Platform"
        };

        await Assert.That(settings.Validate().Successful).IsTrue();
    }
}
