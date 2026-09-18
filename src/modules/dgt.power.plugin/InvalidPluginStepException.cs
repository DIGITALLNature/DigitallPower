// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common.Exceptions;

namespace dgt.power.plugin;

/// <summary>
/// Thrown when a declared plugin step or step image combines a message/stage/image-type in a way
/// Dataverse itself would reject (e.g. an asynchronous step in a pre-validation/pre-operation
/// stage, or a pre-image on a 'Create' message before the record exists).
/// </summary>
[Serializable]
public sealed class InvalidPluginStepException : AbstractPowerException
{
    public InvalidPluginStepException(string message)
        : base(message)
    {
    }
}
