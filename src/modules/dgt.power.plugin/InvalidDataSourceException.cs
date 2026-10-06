// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common.Exceptions;

namespace dgt.power.plugin;

/// <summary>
/// Thrown when an existing table is incompatible with a custom data provider's data-source configuration.
/// </summary>
[Serializable]
// ReSharper disable once ConvertToPrimaryConstructor
public sealed class InvalidDataSourceException : AbstractPowerException
{
    public InvalidDataSourceException(string message)
        : base(message)
    {
    }

    public InvalidDataSourceException()
        : this(string.Empty)
    {
    }

    public InvalidDataSourceException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
