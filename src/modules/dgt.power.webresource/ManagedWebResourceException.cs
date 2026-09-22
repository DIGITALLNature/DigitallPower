// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

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
