// Copyright (c) DIGITALL Nature. All rights reserved

using dgt.power.common;
using dgt.power.documenttemplates.Logic;
using Spectre.Console;
using Spectre.Console.Cli;

namespace dgt.power.documenttemplates.Commands;

// ReSharper disable once ClassNeverInstantiated.Global -- instantiated by Spectre.Console.Cli through DI.
public sealed class DocumentTemplateImportCommand(ICommandSpanFactory spans, IDataverseConnection connection, IAnsiConsole console)
    : PowerCommand<ImportDocumentTemplatesSettings>(spans, console)
{
    public override ValidationResult Validate(CommandContext context, ImportDocumentTemplatesSettings settings)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(settings);
        return context.Remaining.Raw.Count > 0 || context.Remaining.Parsed.Any()
            ? ValidationResult.Error("Unknown documenttemplates import arguments: " +
                string.Join(" ", context.Remaining.Raw.Concat(context.Remaining.Parsed.Select(option => "--" + option.Key))))
            : settings.Validate();
    }

    protected override Task<bool> RunAsync(ImportDocumentTemplatesSettings settings, CancellationToken cancellationToken) =>
        new DocumentTemplateImportEngine(connection, Console).ImportAsync(settings.Manifest!, settings.DryRun, cancellationToken);
}
