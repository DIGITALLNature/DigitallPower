// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common.Exceptions;

namespace dgt.power.plugin.Deployment.Batch;

/// <summary>Thrown when a push target argument cannot be resolved to local artifacts.</summary>
[Serializable]
public sealed class ArtifactPathException : AbstractPowerException
{
    public ArtifactPathException(string message)
        : base(message)
    {
    }

    public ArtifactPathException()
        : this(string.Empty)
    {
    }

    public ArtifactPathException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
