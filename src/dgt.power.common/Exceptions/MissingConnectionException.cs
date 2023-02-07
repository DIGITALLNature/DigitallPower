// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.common.Exceptions;

public class MissingConnectionException : AbstractPowerException
{
    public const string ErrorMessage =
        "No connection is selected. Please select an existing connection with dgtp profile select or create a new one with dgtp profile create";

    public MissingConnectionException() : base(ErrorMessage)
    {

    }
    public MissingConnectionException(Exception innerException) : base(ErrorMessage, innerException)
    {
    }
}
