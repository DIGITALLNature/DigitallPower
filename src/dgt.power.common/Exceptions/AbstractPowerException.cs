// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.common.Exceptions;

public abstract class AbstractPowerException : Exception
{
    public AbstractPowerException(string environment) : base(environment)
    {

    }
    public AbstractPowerException(string environment, Exception innerException) : base(environment, innerException)
    {
    }
}
