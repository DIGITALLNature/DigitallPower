// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using System.Diagnostics.CodeAnalysis;

namespace dgt.power.webresource.Scaffolding;

[SuppressMessage(
    "Design",
    "CA1064:Exceptions should be public",
    Justification = "The exception is internal and is translated to command diagnostics.")]
internal sealed class WebResourceScaffoldingException : Exception
{
    public WebResourceScaffoldingException()
    {
    }

    public WebResourceScaffoldingException(string message) : base(message)
    {
    }

    public WebResourceScaffoldingException(string message, Exception? innerException) : base(message, innerException)
    {
    }
}
