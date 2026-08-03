// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using Spectre.Console.Cli;

namespace dgt.power.cli.tests.TestDoubles;

/// <summary>
/// A dependency-free command double used to exercise Spectre.Console.Cli's argument parsing
/// for a given <typeparamref name="TSettings"/> in isolation, without pulling in the production
/// dependency graph (<c>ITracer</c>, <c>IOrganizationService</c>, <c>IConfigResolver</c>, ...).
/// The parsed settings can be inspected via <see cref="Spectre.Console.Cli.Testing.CommandAppResult.Settings"/>.
/// </summary>
// ReSharper disable once ClassNeverInstantiated.Global
internal sealed class NoOpCommand<TSettings> : Command<TSettings>
    where TSettings : CommandSettings
{
    protected override int Execute(CommandContext context, TSettings settings, CancellationToken cancellationToken) => 0;
}
