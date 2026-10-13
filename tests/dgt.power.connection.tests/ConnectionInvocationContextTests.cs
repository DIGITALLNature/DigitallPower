// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common.Connections;
using dgt.power.connection.Commands;

namespace dgt.power.connection.tests;

public class ConnectionInvocationContextTests
{
    [Test]
    public async Task CaptureUsesParsedOptionValues()
    {
        var context = new ConnectionInvocationContext();

        context.Capture(new CreateConnectionSettings
        {
            Name = "dev",
            Connection = "dev",
            ConnectionString = $"AuthType=ClientSecret;Url={ConnectionTestUrls.Dataverse};",
            NonInteractive = true
        });

        await Assert.That(context.ConnectionName).IsEqualTo("dev");
        await Assert.That(context.ConnectionString)
            .IsEqualTo($"AuthType=ClientSecret;Url={ConnectionTestUrls.Dataverse};");
        await Assert.That(context.NonInteractive).IsTrue();
    }
}
