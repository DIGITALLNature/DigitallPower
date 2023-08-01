// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Runtime.Serialization;

namespace dgt.power.common.Exceptions;

[Serializable]
public class MissingConnectionException : AbstractPowerException
{
    public MissingConnectionException() : base(ErrorMessage)
    {
    }

    public MissingConnectionException(Exception innerException) : base(ErrorMessage, innerException)
    {
    }

    protected MissingConnectionException(SerializationInfo serializationInfo, StreamingContext streamingContext) :
        base(serializationInfo, streamingContext)
    {
    }

    public static string ErrorMessage =>
        "No connection is selected. Please select an existing connection with dgtp profile select or create a new one with dgtp profile create";
}
