// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.Completion;
using Spectre.Console.Cli.Help;
using TUnit.Assertions.Extensions;

namespace dgt.power.telemetry.tests.Completion;

/// <summary>
/// Unit tests for <see cref="CompletionEngine"/> using minimal in-memory ICommandModel mocks.
/// All Spectre.Console.Cli.Help interfaces are public, so no InternalsVisibleTo is needed.
/// </summary>
public class CompletionEngineTests
{
    // ── Tokenizer ─────────────────────────────────────────────────────────────

    [Test]
    [Arguments("dgtp export", new[] { "dgtp", "export" })]
    [Arguments("dgtp export --filedir", new[] { "dgtp", "export", "--filedir" })]
    [Arguments("dgtp export  teamtemplates", new[] { "dgtp", "export", "teamtemplates" })]
    [Arguments("dgtp push \"c:/my path/plugin.dll\"", new[] { "dgtp", "push", "c:/my path/plugin.dll" })]
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
            Command("export"),
            Command("import"),
            Command("maintenance"));

        var result = CompletionEngine.GetCompletions(model, "dgtp ", 5);

        await Assert.That(result).Contains("export");
        await Assert.That(result).Contains("import");
        await Assert.That(result).Contains("maintenance");
    }

    [Test]
    public async Task GetCompletions_PartialPrefix_FiltersTopLevelCommands()
    {
        var model = BuildModel(
            Command("export"),
            Command("import"),
            Command("maintenance"));

        var result = CompletionEngine.GetCompletions(model, "dgtp exp", 8);

        await Assert.That(result).Contains("export");
        await Assert.That(result).DoesNotContain("import");
        await Assert.That(result).DoesNotContain("maintenance");
    }

    [Test]
    public async Task GetCompletions_HiddenCommands_AreExcluded()
    {
        var model = BuildModel(
            Command("export"),
            Command("internal-only", isHidden: true));

        var result = CompletionEngine.GetCompletions(model, "dgtp ", 5);

        await Assert.That(result).Contains("export");
        await Assert.That(result).DoesNotContain("internal-only");
    }

    // ── Sub-command navigation ────────────────────────────────────────────────

    [Test]
    public async Task GetCompletions_AfterBranch_ReturnsSubcommands()
    {
        var model = BuildModel(
            Branch("export",
                Command("teamtemplates"),
                Command("bulkdeletes"),
                Command("queues")));

        var result = CompletionEngine.GetCompletions(model, "dgtp export ", 12);

        await Assert.That(result).Contains("teamtemplates");
        await Assert.That(result).Contains("bulkdeletes");
        await Assert.That(result).Contains("queues");
    }

    [Test]
    public async Task GetCompletions_PartialSubcommand_FiltersCorrectly()
    {
        var model = BuildModel(
            Branch("export",
                Command("teamtemplates"),
                Command("bulkdeletes")));

        var result = CompletionEngine.GetCompletions(model, "dgtp export bulk", 16);

        await Assert.That(result).Contains("bulkdeletes");
        await Assert.That(result).DoesNotContain("teamtemplates");
    }

    [Test]
    public async Task GetCompletions_UnrecognisedToken_ReturnsEmpty()
    {
        var model = BuildModel(Command("export"));

        var result = CompletionEngine.GetCompletions(model, "dgtp unknown ", 13);

        await Assert.That(result.Count).IsEqualTo(0);
    }

    // ── Option completion ─────────────────────────────────────────────────────

    [Test]
    public async Task GetCompletions_DashDash_ReturnsAllOptions()
    {
        var model = BuildModel(
            Branch("export",
                CommandWithOptions("teamtemplates",
                    Option("filedir"),
                    Option("filename"),
                    Option("no-telemetry"))));

        var result = CompletionEngine.GetCompletions(model, "dgtp export teamtemplates --", 28);

        await Assert.That(result).Contains("--filedir");
        await Assert.That(result).Contains("--filename");
        await Assert.That(result).Contains("--no-telemetry");
    }

    [Test]
    public async Task GetCompletions_OptionPrefix_FiltersOptions()
    {
        var model = BuildModel(
            Branch("export",
                CommandWithOptions("teamtemplates",
                    Option("filedir"),
                    Option("filename"),
                    Option("inline"))));

        var result = CompletionEngine.GetCompletions(model, "dgtp export teamtemplates --file", 32);

        await Assert.That(result).Contains("--filedir");
        await Assert.That(result).Contains("--filename");
        await Assert.That(result).DoesNotContain("--inline");
    }

    [Test]
    public async Task GetCompletions_HiddenOption_IsExcluded()
    {
        var model = BuildModel(
            CommandWithOptions("push",
                Option("solution"),
                Option("internal", isHidden: true)));

        var result = CompletionEngine.GetCompletions(model, "dgtp push --", 12);

        await Assert.That(result).Contains("--solution");
        await Assert.That(result).DoesNotContain("--internal");
    }

    // ── Position truncation ───────────────────────────────────────────────────

    [Test]
    public async Task GetCompletions_PositionTruncatesLine()
    {
        var model = BuildModel(Command("export"), Command("import"));

        // Position 8 → "dgtp exp" → completes "export"
        var result = CompletionEngine.GetCompletions(model, "dgtp export import", position: 8);

        await Assert.That(result).Contains("export");
        await Assert.That(result).DoesNotContain("import");
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
        ICommandOption[]? options = null) : ICommandInfo
    {
        public string Name => name;
        public string? Description => null;
        public bool IsBranch => children is { Length: > 0 };
        public bool IsDefaultCommand => false;
        public bool IsHidden => isHidden;
        public IReadOnlyList<ICommandParameter> Parameters { get; } =
            (options ?? []).Cast<ICommandParameter>().ToList();
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
}
