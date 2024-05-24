// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Runtime.Serialization;

namespace dgt.power.common.Exceptions;

[Serializable]
public class FailedConnectionException : AbstractPowerException
{
    public FailedConnectionException(string environment) : base(ErrorMessage(environment))
    {
    }

    public FailedConnectionException(string environment, Exception innerException) : base(ErrorMessage(environment),
        innerException)
    {
    }

    protected FailedConnectionException(SerializationInfo serializationInfo, StreamingContext streamingContext) :
        base(serializationInfo, streamingContext)
    {
    }

    public static string ErrorMessage(string environment) =>
        $"Connection to '{environment}' failed. Please review your connection.";
}
