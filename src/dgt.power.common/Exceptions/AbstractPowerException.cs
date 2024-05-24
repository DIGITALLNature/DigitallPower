// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Runtime.Serialization;

namespace dgt.power.common.Exceptions;

[Serializable]
public abstract class AbstractPowerException : Exception
{
    protected AbstractPowerException(string environment) : base(environment)
    {
    }

    protected AbstractPowerException(string environment, Exception innerException) : base(environment, innerException)
    {
    }

    protected AbstractPowerException(SerializationInfo serializationInfo, StreamingContext streamingContext) :
        base(serializationInfo, streamingContext)
    {
    }
}
