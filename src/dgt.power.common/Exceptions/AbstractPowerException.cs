// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

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
