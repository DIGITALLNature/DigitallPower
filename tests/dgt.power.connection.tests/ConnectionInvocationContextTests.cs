// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common;
using dgt.power.common.Connections;

namespace dgt.power.connection.tests;

public class ConnectionInvocationContextTests
{
    [Test]
    public async Task CaptureUsesParsedOptionValues()
    {
        var context = new ConnectionInvocationContext();

        context.Capture(new TestSettings
        {
            Connection = "dev",
            ConnectionString = "AuthType=ClientSecret;Url=https://contoso.crm.dynamics.com;",
            NonInteractive = true
        });

        await Assert.That(context.ConnectionName).IsEqualTo("dev");
        await Assert.That(context.ConnectionString)
            .IsEqualTo("AuthType=ClientSecret;Url=https://contoso.crm.dynamics.com;");
        await Assert.That(context.NonInteractive).IsTrue();
    }

    private sealed class TestSettings : BaseProgramSettings
    {
    }
}
