using System.ComponentModel;
using dgt.power.profile.Base;
using Spectre.Console;
using Spectre.Console.Cli;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
#pragma warning disable CS8618

namespace dgt.power.profile.Commands;

public class CreateProfileSettings : ProfileSettings
{
    [CommandArgument(0, "<Name>")]
    [Description("Name")]
    public string Name { get; init; }

    [CommandArgument(1, "<ConnectionString>")]
    [Description("ConnectionString")]
    public string ConnectionString { get; init; }

    [CommandOption("--skipcheck")]
    [Description("Skip testing of the connection")]
    [DefaultValue(false)]
    public bool SkipChecking { get; init; }

    [CommandOption("-s|--security-protocol")]
    [Description("Set specific protocol; [ssl3|tls|tls11|tls12]")]
    [DefaultValue("tls12")]
    public string SecurityProtocol { get; init; }

    [CommandOption("-i|--insecure")]
    [Description("Ignore certificate issues; [true|false]")]
    [DefaultValue(false)]
    public bool Insecure { get; init; }

    public override ValidationResult Validate()
    {
        var validationResult = base.Validate();
        if (!validationResult.Successful)
        {
            return ValidationResult.Error();
        }

        var validProtocols = new[] { "tls", "tls11", "tls12", "ssl3" };
        return !string.IsNullOrWhiteSpace(SecurityProtocol) && validProtocols.Contains(SecurityProtocol)
            ? ValidationResult.Success()
            : ValidationResult.Error();
    }
}
