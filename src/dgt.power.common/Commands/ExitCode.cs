// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

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
