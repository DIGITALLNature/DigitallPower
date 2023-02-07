// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Globalization;

namespace dgt.power.common.Exceptions;

public class FailedConnectionException : AbstractPowerException
{
    public static string ErrorMessage(string environment) => $"Connection to '{environment}' failed. Please review your connection.";

    public FailedConnectionException(string environment) : base(ErrorMessage(environment))
    {
    }

    public FailedConnectionException(string environment, Exception innerException) : base(ErrorMessage(environment), innerException)
    {
    }
}
