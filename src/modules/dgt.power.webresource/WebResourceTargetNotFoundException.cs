// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

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
