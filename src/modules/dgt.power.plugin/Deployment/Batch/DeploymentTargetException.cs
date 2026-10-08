// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common.Exceptions;

namespace dgt.power.plugin.Deployment.Batch;

/// <summary>Thrown when a push target argument cannot be resolved to local artifacts.</summary>
[Serializable]
public sealed class DeploymentTargetException : AbstractPowerException
{
    public DeploymentTargetException(string message)
        : base(message)
    {
    }

    public DeploymentTargetException()
        : this(string.Empty)
    {
    }

    public DeploymentTargetException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
