// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.common.Exceptions;

[Serializable]
public abstract class AbstractPowerException : Exception
{
    protected AbstractPowerException()
    {
    }

    protected AbstractPowerException(string message) : base(message)
    {
    }

    protected AbstractPowerException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
