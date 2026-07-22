// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.codegeneration.Constants;

public static class Warnings
{
    public static string TsExtensionDeprecation =>
        "Warning: some Forms end with '.ts', please remove the '.ts' in your config! It's deprecated!";

    /// <summary>
    ///     Dataverse resolves the <c>name</c> attribute of out-of-box records (e.g. system forms) based on the
    ///     connecting user's personal UI language setting, not on any per-request language parameter. If that
    ///     session language differs from the language configured for code generation, form names retrieved from
    ///     Dataverse may not match `Forms` entries in the config, causing forms to be silently skipped. This is a
    ///     known Dataverse platform limitation - see README for details and mitigation.
    /// </summary>
    public static string FormNameLanguageMismatch(int sessionLanguageCode, int configuredLanguageCode) =>
        $"[bold yellow]Warning:[/] Your Dataverse connection's UI language ({sessionLanguageCode}) differs from " +
        $"the configured code generation language ({configuredLanguageCode}). Form names (systemform.name) are " +
        "resolved by Dataverse using the connection's UI language, not the configured language - form filtering " +
        "via 'Forms' in the config may skip forms unexpectedly. Set your personal Dataverse UI language to match " +
        "the configured language to avoid this. See README 'Known Limitations' section for details.";
}
