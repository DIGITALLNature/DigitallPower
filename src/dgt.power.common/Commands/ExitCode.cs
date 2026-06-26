// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.common.Commands;

public enum ExitCode
{
    Success = 0,
    Error = 1,

    /// <summary>
    /// Interactive authentication is required (e.g. MSAL token expired) but the tool
    /// is running in non-interactive mode. The caller must prompt the user to
    /// re-authenticate before retrying the command.
    /// </summary>
    AuthRequired = 2
}
