// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.connection.Commands;

namespace dgt.power.connection.tests;

public class CreateConnectionSettingsTests
{
    [Test]
    public async Task UserConnectionDoesNotRequireTenant()
    {
        var settings = new CreateConnectionSettings { Name = "dev", Url = "https://contoso.crm.dynamics.com" };

        await Assert.That(settings.Validate().Successful).IsTrue();
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
    public async Task UserConnectionAcceptsOptionalTenant()
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
    public async Task ClientSecretRequiresClientId()
    {
        var settings = new CreateConnectionSettings
        {
            Name = "prod",
            Url = "https://contoso.crm.dynamics.com",
            TenantId = "tenant",
            ClientSecret = "test-secret"
        };

        await Assert.That(settings.Validate().Successful).IsFalse();
    }

    [Test]
    public async Task ClientSecretRequiresTenant()
    {
        var settings = new CreateConnectionSettings
        {
            Name = "prod",
            Url = "https://contoso.crm.dynamics.com",
            ClientId = "client",
            ClientSecret = "test-secret"
        };

        await Assert.That(settings.Validate().Successful).IsFalse();
    }

    [Test]
    public async Task ClientSecretAcceptsTenantAndClientId()
    {
        var settings = new CreateConnectionSettings
        {
            Name = "prod",
            Url = "https://contoso.crm.dynamics.com",
            TenantId = "tenant",
            ClientId = "client",
            ClientSecret = "test-secret"
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

    [Test]
    [Arguments("")]
    [Arguments(" ")]
    public async Task ClientSecretRejectsEmptyValue(string secret)
    {
        var settings = new CreateConnectionSettings
        {
            Name = "prod", Url = "https://contoso.crm.dynamics.com",
            TenantId = "tenant", ClientId = "client", ClientSecret = secret
        };

        await Assert.That(settings.Validate().Successful).IsFalse();
    }

    [Test]
    public async Task CertificatePasswordRequiresFile()
    {
        var settings = new CreateConnectionSettings
        {
            Name = "prod", Url = "https://contoso.crm.dynamics.com",
            TenantId = "tenant", ClientId = "client",
            CertificateThumbprint = "thumbprint", CertificatePassword = "password"
        };

        await Assert.That(settings.Validate().Successful).IsFalse();
    }

    [Test]
    [Arguments(null)]
    [Arguments("")]
    [Arguments("password")]
    public async Task CertificateFileAcceptsOptionalPassword(string? password)
    {
        var settings = new CreateConnectionSettings
        {
            Name = "prod", Url = "https://contoso.crm.dynamics.com",
            TenantId = "tenant", ClientId = "client",
            CertificatePath = "certificate.pfx", CertificatePassword = password
        };

        await Assert.That(settings.Validate().Successful).IsTrue();
    }
}
