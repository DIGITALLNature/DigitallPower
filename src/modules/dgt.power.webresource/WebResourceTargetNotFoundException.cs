// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common.Exceptions;

namespace dgt.power.webresource;

[Serializable]
public sealed class WebResourceTargetNotFoundException : AbstractPowerException
{
    public WebResourceTargetNotFoundException(string target)
        : base($"WebResource target was not found: '{target}'.")
    {
    }

    public WebResourceTargetNotFoundException()
        : this(string.Empty)
    {
    }

    public WebResourceTargetNotFoundException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
