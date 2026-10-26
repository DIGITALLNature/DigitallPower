// Copyright (c) DIGITALL Nature. All rights reserved

using dgt.power.common;
using dgt.power.documenttemplates.Logic;
using Spectre.Console;
using Spectre.Console.Cli;

namespace dgt.power.documenttemplates.Commands;

// ReSharper disable once ClassNeverInstantiated.Global -- instantiated by Spectre.Console.Cli through DI.
public sealed class DocumentTemplateExportCommand(ICommandSpanFactory spans, IDataverseConnection connection, IAnsiConsole console)
    : PowerCommand<ExportDocumentTemplatesSettings>(spans, console)
{
    public override ValidationResult Validate(CommandContext context, ExportDocumentTemplatesSettings settings)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(settings);
        return context.Remaining.Raw.Count > 0 || context.Remaining.Parsed.Any()
            ? ValidationResult.Error("Unknown documenttemplates export arguments: " +
                string.Join(" ", context.Remaining.Raw.Concat(context.Remaining.Parsed.Select(option => "--" + option.Key))))
            : settings.Validate();
    }

    protected override Task<bool> RunAsync(ExportDocumentTemplatesSettings settings, CancellationToken cancellationToken) =>
        new DocumentTemplateExportEngine(connection, Console).ExportAsync(settings.Directory!, settings.Entity, settings.Type,
            settings.Overwrite, cancellationToken);
}
