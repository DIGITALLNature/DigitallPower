// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

namespace dgt.power.common.Exceptions;

[Serializable]
public class FailedConnectionException : AbstractPowerException
{
    public FailedConnectionException()
        : base(DefaultErrorMessage)
    {
    }

    public FailedConnectionException(string environment) : base(ErrorMessage(environment))
    {
    }

    public FailedConnectionException(string environment, Exception innerException) : base(ErrorMessage(environment),
        innerException)
    {
    }

    private static string DefaultErrorMessage => "Connection failed. Please review your connection.";

    public static string ErrorMessage(string environment) =>
        $"Connection to '{environment}' failed. Please review your connection.";
}
