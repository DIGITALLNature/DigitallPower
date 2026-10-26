// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using dgt.power.common.Exceptions;

namespace dgt.power.webresource;

[Serializable]
public sealed class WebResourceMappingException : AbstractPowerException
{
    public WebResourceMappingException(string mappingFile, string message)
        : base($"Mapping file '{mappingFile}' is invalid: {message}")
    {
    }

    public WebResourceMappingException()
        : this(string.Empty, string.Empty)
    {
    }

    public WebResourceMappingException(string message)
        : base(message)
    {
    }

    public WebResourceMappingException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public WebResourceMappingException(string mappingFile, string message, Exception innerException)
        : base($"Mapping file '{mappingFile}' is invalid: {message}", innerException)
    {
    }
}
