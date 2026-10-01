// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common.Exceptions;

namespace dgt.power.plugin;

/// <summary>
/// Thrown when a requested target solution cannot be found in Dataverse.
/// </summary>
[Serializable]
public sealed class MissingSolutionException : AbstractPowerException
{
    public MissingSolutionException(string solutionUniqueName)
        : base($"Solution '{solutionUniqueName}' was not found in Dataverse.")
    {
    }

    public MissingSolutionException()
        : this(string.Empty)
    {
    }

    public MissingSolutionException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
