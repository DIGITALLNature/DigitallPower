// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.Completion;
using Spectre.Console.Cli.Help;

namespace dgt.power.cli.tests.Completion;

/// <summary>
/// Unit tests for <see cref="CompletionEngine"/> using minimal in-memory ICommandModel mocks.
/// All Spectre.Console.Cli.Help interfaces are public, so no InternalsVisibleTo is needed.
/// </summary>
public class CompletionEngineTests
{
    // ── Tokenizer ─────────────────────────────────────────────────────────────

    [Test]
    [Arguments("dgtp documenttemplates", new[] { "dgtp", "documenttemplates" })]
    [Arguments("dgtp documenttemplates export --overwrite", new[] { "dgtp", "documenttemplates", "export", "--overwrite" })]
    [Arguments("dgtp teamtemplates  export", new[] { "dgtp", "teamtemplates", "export" })]
    [Arguments("dgtp plugin push \"c:/my path/plugin.dll\"", new[] { "dgtp", "plugin", "push", "c:/my path/plugin.dll" })]
    [Arguments("", new string[0])]
    public async Task Tokenize_SplitsCorrectly(string input, string[] expected)
    {
        var tokens = CompletionEngine.Tokenize(input);

        await Assert.That(tokens).IsEquivalentTo(expected);
    }

    // ── Root-level completions ────────────────────────────────────────────────

    [Test]
    public async Task GetCompletions_EmptyLineAfterAppName_ReturnsTopLevelCommands()
    {
        var model = BuildModel(
            Command("documenttemplates"),
            Command("plugin"),
            Command("teamtemplates"),
            Command("maintenance"));

        var result = CompletionEngine.GetCompletions(model, "dgtp ", 5);

        await Assert.That(result).Contains("documenttemplates");
        await Assert.That(result).Contains("plugin");
        await Assert.That(result).Contains("teamtemplates");
        await Assert.That(result).Contains("maintenance");
    }

    [Test]
    public async Task GetCompletions_PartialPrefix_FiltersTopLevelCommands()
    {
        var model = BuildModel(
            Command("documenttemplates"),
            Command("plugin"),
            Command("maintenance"));

        var result = CompletionEngine.GetCompletions(model, "dgtp doc", 8);

        await Assert.That(result).Contains("documenttemplates");
        await Assert.That(result).DoesNotContain("plugin");
        await Assert.That(result).DoesNotContain("maintenance");
    }

    [Test]
    public async Task GetCompletions_HiddenCommands_AreExcluded()
    {
        var model = BuildModel(
            Command("documenttemplates"),
            Command("internal-only", isHidden: true));

        var result = CompletionEngine.GetCompletions(model, "dgtp ", 5);

        await Assert.That(result).Contains("documenttemplates");
        await Assert.That(result).DoesNotContain("internal-only");
    }

    // ── Sub-command navigation ────────────────────────────────────────────────

    [Test]
    public async Task GetCompletions_AfterBranch_ReturnsSubcommands()
    {
        var model = BuildModel(
            Branch("documenttemplates",
                Command("import"),
                Command("export")));

        const string line = "dgtp documenttemplates ";
        var result = CompletionEngine.GetCompletions(model, line, line.Length);

        await Assert.That(result).Contains("import");
        await Assert.That(result).Contains("export");
        await Assert.That(result).DoesNotContain("teamtemplates");
    }

    [Test]
    public async Task GetCompletions_TeamTemplatesBranch_ReturnsOperations()
    {
        var model = BuildModel(
            Branch("teamtemplates",
                Command("import"),
                Command("export")));

        var result = CompletionEngine.GetCompletions(model, "dgtp teamtemplates ", 19);

        await Assert.That(result).Contains("import");
        await Assert.That(result).Contains("export");
    }

    [Test]
    public async Task GetCompletions_PartialSubcommand_FiltersCorrectly()
    {
        var model = BuildModel(
            Branch("teamtemplates",
                Command("import"),
                Command("export")));

        const string line = "dgtp teamtemplates exp";
        var result = CompletionEngine.GetCompletions(model, line, line.Length);

        await Assert.That(result).Contains("export");
        await Assert.That(result).DoesNotContain("import");
    }

    [Test]
    public async Task GetCompletions_UnrecognisedToken_ReturnsEmpty()
    {
        var model = BuildModel(Command("documenttemplates"));

        var result = CompletionEngine.GetCompletions(model, "dgtp unknown ", 13);

        await Assert.That(result.Count).IsEqualTo(0);
    }

    // ── Option completion ─────────────────────────────────────────────────────

    [Test]
    public async Task GetCompletions_DashDash_ReturnsAllOptions()
    {
        var model = BuildModel(
            Branch("teamtemplates",
                CommandWithOptions("export",
                    Option("table"),
                    Option("overwrite"),
                    Option("dry-run"))));

        var result = CompletionEngine.GetCompletions(model, "dgtp teamtemplates export --", 28);

        await Assert.That(result).Contains("--table");
        await Assert.That(result).Contains("--overwrite");
        await Assert.That(result).Contains("--dry-run");
    }

    [Test]
    public async Task GetCompletions_OptionPrefix_FiltersOptions()
    {
        var model = BuildModel(
            Branch("teamtemplates",
                CommandWithOptions("import",
                    Option("cleanup-table"),
                    Option("cleanup"),
                    Option("dry-run"))));

        var result = CompletionEngine.GetCompletions(model, "dgtp teamtemplates import --cleanup", 35);

        await Assert.That(result).Contains("--cleanup");
        await Assert.That(result).Contains("--cleanup-table");
        await Assert.That(result).DoesNotContain("--dry-run");
    }

    [Test]
    public async Task GetCompletions_HiddenOption_IsExcluded()
    {
        var model = BuildModel(
            Branch("plugin",
                CommandWithOptions("push",
                Option("solution"),
                Option("internal", isHidden: true))));

        var result = CompletionEngine.GetCompletions(model, "dgtp plugin push --", 19);

        await Assert.That(result).Contains("--solution");
        await Assert.That(result).DoesNotContain("--internal");
    }

    // ── Position truncation ───────────────────────────────────────────────────

    [Test]
    public async Task GetCompletions_PositionTruncatesLine()
    {
        var model = BuildModel(Command("documenttemplates"), Command("plugin"));

        var result = CompletionEngine.GetCompletions(model, "dgtp documenttemplates plugin", position: 8);

        await Assert.That(result).Contains("documenttemplates");
        await Assert.That(result).DoesNotContain("plugin");
    }

    // ── Dynamic provider completions ─────────────────────────────────────────

    [Test]
    public async Task GetCompletions_LeafCommandWithPositionalArg_CallsDynamicProvider()
    {
        var model = BuildModel(
            Branch("connection",
                CommandWithArg("select"),
                CommandWithArg("delete"),
                Command("list")));

        var provider = new FakeDynamicProvider(["dev", "prod", "staging"]);

        const string commandLine = "dgtp connection select ";
        var result = CompletionEngine.GetCompletions(model, commandLine, commandLine.Length, provider);

        await Assert.That(result).Contains("dev");
        await Assert.That(result).Contains("prod");
        await Assert.That(result).Contains("staging");
    }

    [Test]
    public async Task GetCompletions_LeafCommandWithPositionalArg_FiltersByPrefix()
    {
        var model = BuildModel(
            Branch("connection", CommandWithArg("select")));

        var provider = new FakeDynamicProvider(["dev", "prod", "staging"]);

        const string commandLine = "dgtp connection select pr";
        var result = CompletionEngine.GetCompletions(model, commandLine, commandLine.Length, provider);

        await Assert.That(result).Contains("prod");
        await Assert.That(result).DoesNotContain("dev");
        await Assert.That(result).DoesNotContain("staging");
    }

    [Test]
    public async Task GetCompletions_BranchCommand_DoesNotCallDynamicProvider()
    {
        var model = BuildModel(
            Branch("connection",
                CommandWithArg("select"),
                CommandWithArg("delete")));

        var provider = new FakeDynamicProvider(["dev", "prod"]);

        // "connection " — still a branch, not a leaf
        const string commandLine = "dgtp connection ";
        var result = CompletionEngine.GetCompletions(model, commandLine, commandLine.Length, provider);

        await Assert.That(provider.WasCalled).IsFalse();
        await Assert.That(result).Contains("select");
        await Assert.That(result).Contains("delete");
    }

    [Test]
    public async Task GetCompletions_WhenNoDynamicProvider_ReturnsEmptyForPositionalArg()
    {
        var model = BuildModel(
            Branch("connection", CommandWithArg("select")));

        const string commandLine = "dgtp connection select ";
        var result = CompletionEngine.GetCompletions(model, commandLine, commandLine.Length);

        await Assert.That(result).IsEmpty();
    }

    [Test]
    public async Task GetCompletions_WhenProviderReturnsNull_ReturnsEmpty()
    {
        var model = BuildModel(
            Branch("connection", CommandWithArg("select")));

        var provider = new FakeDynamicProvider(null); // null = doesn't handle this path

        const string commandLine = "dgtp connection select ";
        var result = CompletionEngine.GetCompletions(model, commandLine, commandLine.Length, provider);

        await Assert.That(result).IsEmpty();
    }

    // ── Model builder helpers ─────────────────────────────────────────────────

    private static ICommandModel BuildModel(params ICommandInfo[] commands) =>
        new FakeModel(commands);

    private static ICommandInfo Command(string name, bool isHidden = false) =>
        new FakeCommandInfo(name, isHidden: isHidden);

    private static ICommandInfo Branch(string name, params ICommandInfo[] children) =>
        new FakeCommandInfo(name, children: children);

    private static ICommandInfo CommandWithOptions(string name, params ICommandOption[] options) =>
        new FakeCommandInfo(name, options: options);

    private static ICommandInfo CommandWithArg(string name, string argValue = "Name") =>
        new FakeCommandInfo(name, positionalArgs: [new FakeCommandArgument(argValue)]);


    private static ICommandOption Option(string longName, bool isHidden = false) =>
        new FakeCommandOption(longName, isHidden);

    // ── Fakes ─────────────────────────────────────────────────────────────────

    private sealed class FakeModel(ICommandInfo[] commands) : ICommandModel
    {
        public string ApplicationName => "dgtp";
        public string? ApplicationVersion => null;
        public IReadOnlyList<string[]> Examples => [];
        public IReadOnlyList<ICommandInfo> Commands { get; } = commands;
        public ICommandInfo? DefaultCommand => null;
    }

    private sealed class FakeCommandInfo(
        string name,
        bool isHidden = false,
        ICommandInfo[]? children = null,
        ICommandOption[]? options = null,
        ICommandArgument[]? positionalArgs = null) : ICommandInfo
    {
        public string Name => name;
        public string? Description => null;
        public bool IsBranch => children is { Length: > 0 };
        public bool IsDefaultCommand => false;
        public bool IsHidden => isHidden;
        public IReadOnlyList<ICommandParameter> Parameters { get; } =
        [
            .. options ?? [],
            .. positionalArgs ?? []
        ];
        public ICommandInfo? Parent => null;
        public IReadOnlyList<string[]> Examples => [];
        public IReadOnlyList<ICommandInfo> Commands { get; } = children ?? [];
        public ICommandInfo? DefaultCommand => null;
    }

    private sealed class FakeCommandOption(string longName, bool isHidden = false) : ICommandOption
    {
        public IReadOnlyList<string> LongNames { get; } = [longName];
        public IReadOnlyList<string> ShortNames => [];
        public string? ValueName => null;
        public bool ValueIsOptional => false;
        public bool IsFlag => false;
        public bool IsRequired => false;
        public string? Description => null;
        public System.ComponentModel.DefaultValueAttribute? DefaultValue => null;
        public bool IsHidden => isHidden;
    }

    private sealed class FakeCommandArgument(string value) : ICommandArgument
    {
        public string Value => value;
        public int Position => 0;
        public bool IsFlag => false;
        public bool IsRequired => true;
        public string? Description => null;
        public System.ComponentModel.DefaultValueAttribute? DefaultValue => null;
        public bool IsHidden => false;
    }

    private sealed class FakeDynamicProvider(IReadOnlyList<string>? names) : IDynamicCompletionProvider
    {
        public bool WasCalled { get; private set; }

        public IReadOnlyList<string>? GetCompletions(IReadOnlyList<string> commandPath, string prefix)
        {
            WasCalled = true;
            if (names is null)
                return null;
            return string.IsNullOrEmpty(prefix)
                ? names
                : names.Where(n => n.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)).ToList();
        }
    }
}
