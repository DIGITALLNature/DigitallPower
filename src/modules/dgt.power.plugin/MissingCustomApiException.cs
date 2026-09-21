// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common.Exceptions;

namespace dgt.power.plugin;

/// <summary>
/// Thrown when a plugin type declares a Custom API that cannot be found on the target environment.
/// </summary>
[Serializable]
public sealed class MissingCustomApiException : AbstractPowerException
{
    public MissingCustomApiException(string customApiName)
        : base($"Custom API '{customApiName}' declared by the plugin assembly was not found on the target environment.")
    {
    }

    public MissingCustomApiException()
        : this(string.Empty)
    {
    }

    public MissingCustomApiException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
