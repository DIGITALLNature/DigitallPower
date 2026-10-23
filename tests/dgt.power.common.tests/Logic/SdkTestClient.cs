// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Reflection;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.PowerPlatform.Dataverse.Client;
using AuthenticationType = Microsoft.PowerPlatform.Dataverse.Client.AuthenticationType;

namespace dgt.power.common.tests.Logic;

internal static class SdkTestClient
{
    internal const string EnvironmentUrl = "https://example.crm.dynamics.com";

#pragma warning disable S3011 // SDK internals are needed only to construct an offline caller-bearing test proxy.
    internal static ServiceClient Create(SdkTestTransport? transport = null)
    {
        // Initialize the SDK's real caller-bearing proxy without authentication or network I/O.
        var proxyType = typeof(ServiceClient).Assembly.GetType(
            "Microsoft.PowerPlatform.Dataverse.Client.Connector.OrganizationWebProxyClientAsync")!;
        var proxy = Activator.CreateInstance(proxyType,
            new Uri(new Uri(EnvironmentUrl), "XRMServices/2011/Organization.svc"), false)!;
        if (transport is not null)
        {
            var endpoint = (ServiceEndpoint)proxyType.GetProperty("Endpoint")!.GetValue(proxy)!;
            endpoint.Binding = new CustomBinding(
                new TextMessageEncodingBindingElement(MessageVersion.Soap12WSAddressing10, Encoding.UTF8),
                transport);
        }
        var constructor = typeof(ServiceClient).GetConstructor(
            BindingFlags.Instance | BindingFlags.NonPublic,
            null,
            [proxyType, typeof(bool), typeof(AuthenticationType),
                typeof(Version), typeof(ILogger)],
            null)!;
        var client = (ServiceClient)constructor.Invoke([proxy, true, AuthenticationType.OAuth, new Version(9, 2), null]);
        return client;
    }

    internal static void SetField(object target, string name, object? value) =>
        target.GetType().GetField(name, BindingFlags.Instance | BindingFlags.NonPublic)!.SetValue(target, value);
#pragma warning restore S3011
}
