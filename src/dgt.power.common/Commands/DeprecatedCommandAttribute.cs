// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.common.Commands;

/// <summary>
/// Marks a <see cref="Spectre.Console.Cli.CommandSettings"/> class as deprecated so that
/// <c>DeprecationInterceptor</c> prints a warning whenever a command bound to it (or to a
/// subclass of it) is invoked.
/// </summary>
/// <remarks>
/// Apply this to a branch-level settings class (e.g. <c>ProfileSettings</c>) to deprecate every
/// command under that branch at once, since the attribute is inherited by subclasses. Apply it to
/// a single leaf settings class instead to deprecate just that one command.
/// </remarks>
/// <param name="useInstead">
/// The name of the command/branch to use instead, if any (e.g. "connection"). Shown in the
/// warning message. Leave <see langword="null"/> if the command is deprecated without a direct
/// replacement.
/// </param>
[AttributeUsage(AttributeTargets.Class)]
public sealed class DeprecatedCommandAttribute(string? useInstead = null) : Attribute
{
    public string? UseInstead { get; } = useInstead;
}
