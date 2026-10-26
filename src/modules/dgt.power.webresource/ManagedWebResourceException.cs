// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using dgt.power.common.Exceptions;

namespace dgt.power.webresource;

[Serializable]
public sealed class ManagedWebResourceException : AbstractPowerException
{
    public ManagedWebResourceException(string webResourceName)
        : base(
            $"WebResource '{webResourceName}' is managed and cannot be updated. " +
            "Updating it would create an unmanaged customization layer.")
    {
    }

    public ManagedWebResourceException()
        : this(string.Empty)
    {
    }

    public ManagedWebResourceException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
