// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

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
        "No connection is selected. Please select an existing connection with dgtp connection select or create a new one with dgtp connection create";
}
