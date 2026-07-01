// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.common.Exceptions;

[Serializable]
public class MissingConnectionException : AbstractPowerException
{
    public MissingConnectionException() : base(ErrorMessage)
    {
    }

    public MissingConnectionException(string message) : base(message)
    {
    }

    public MissingConnectionException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public MissingConnectionException(Exception innerException) : base(ErrorMessage, innerException)
    {
    }

    public static string ErrorMessage =>
        "No connection is selected. Please select an existing connection with dgtp profile select or create a new one with dgtp profile create";
}
