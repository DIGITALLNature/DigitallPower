// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common.Exceptions;

namespace dgt.power.webresource;

[Serializable]
public sealed class WebResourceSolutionNotFoundException : AbstractPowerException
{
    public WebResourceSolutionNotFoundException(string solutionUniqueName)
        : base($"Solution '{solutionUniqueName}' was not found.")
    {
    }

    public WebResourceSolutionNotFoundException()
        : this(string.Empty)
    {
    }

    public WebResourceSolutionNotFoundException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
