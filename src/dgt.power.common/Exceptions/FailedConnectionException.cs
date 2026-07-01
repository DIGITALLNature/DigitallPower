// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

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
