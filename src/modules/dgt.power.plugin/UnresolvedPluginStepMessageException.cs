// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common.Exceptions;

namespace dgt.power.plugin;

/// <summary>
/// Thrown when a declared plugin step's message/entity combination could not be resolved against
/// the target environment's <c>sdkmessage</c>/<c>sdkmessagefilter</c> records - e.g. the message
/// name is misspelled, or the entity does not support that message.
/// </summary>
[Serializable]
// ReSharper disable once ConvertToPrimaryConstructor
public sealed class UnresolvedPluginStepMessageException : AbstractPowerException
{
    public UnresolvedPluginStepMessageException(string stepName, string messageName, string primaryEntityName)
        : base(
            $"Step '{stepName}' declares message '{messageName}' for entity '{primaryEntityName}', but no matching " +
            "sdkmessage/sdkmessagefilter was found on the target environment. Check for a typo in the message name, " +
            "or that the entity actually supports this message.")
    {
    }

    public UnresolvedPluginStepMessageException()
        : this(string.Empty, string.Empty, string.Empty)
    {
    }

    public UnresolvedPluginStepMessageException(string message)
        : base(message)
    {
    }

    public UnresolvedPluginStepMessageException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
