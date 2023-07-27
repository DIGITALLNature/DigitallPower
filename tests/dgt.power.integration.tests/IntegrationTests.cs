// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;

namespace dgt.power.integration.tests;

public class IntegrationTests
{
    private ServiceClient? _integrationService;

    /// <summary>
    /// 
    /// </summary>
    protected static bool IntegrationTestsEnabled { get
        {
            // setx DGTP_TESTS_INTEGRATION_RUN "true"
            if (bool.TryParse(Environment.GetEnvironmentVariable("DGTP_TESTS_INTEGRATION_RUN"),out var result))
            {
                return result;
            }
            return false;
        }
    }

    //protected Process GenerateProcess(string arguments)
    //{
    //    return new Process
    //    {
    //        StartInfo = new ProcessStartInfo
    //        {
    //            FileName = "C:\\Users\\raaa\\source\\repos\\DIGITALL\\Dynamics Power\\src\\dgt.power\\bin\\Debug\\net7.0\\dgt.power.exe",
    //            RedirectStandardInput = false,
    //            RedirectStandardError = false,
    //            UseShellExecute = false,
    //            RedirectStandardOutput = true,
    //            CreateNoWindow = true,
    //            Arguments = arguments
    //        }
    //    };
    //}

    protected IOrganizationService IntegrationService
    {
        get
        {
            if (_integrationService == null)
            {
                _integrationService = new ServiceClient(Connectionstring);
            }

            return _integrationService;
        }
    }

    protected string Connectionstring
    {
        get
        {
            var connectionstring = Environment.GetEnvironmentVariable("DGTP_TESTS_INTEGRATION_CONNECTION");

            if (string.IsNullOrWhiteSpace(connectionstring))
            {
                throw new ArgumentNullException("connectionstring", "ENV:DGTP_TESTS_INTEGRATION_CONNECTION is not set");
            }

            return connectionstring;
        }
    }
}
