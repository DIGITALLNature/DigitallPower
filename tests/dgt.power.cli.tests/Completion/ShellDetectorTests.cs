// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.Commands.Complete;

namespace dgt.power.cli.tests.Completion;

public class ShellDetectorTests
{
    [Test]
    [Arguments("bash",       "bash")]
    [Arguments("zsh",        "zsh")]
    [Arguments("fish",       "fish")]
    [Arguments("pwsh",       "pwsh")]
    [Arguments("powershell", "pwsh")]
    [Arguments("BASH",       "bash")]
    [Arguments("ZSH",        "zsh")]
    [Arguments(null,         null)]
    [Arguments("unknown",    null)]
    public async Task NormalizeShellName_MapsKnownShells(string? input, string? expected)
    {
        var result = ShellDetector.NormalizeShellName(input);
        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    [Arguments("bash",       "bash")]
    [Arguments("zsh",        "zsh")]
    [Arguments("fish",       "fish")]
    [Arguments("pwsh",       "powershell")]
    [Arguments("powershell", "powershell")]
    public async Task ToDotnetSuggestName_MapsToSuggestArgument(string shell, string expected)
    {
        var result = ShellDetector.ToDotnetSuggestName(shell);
        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    [Arguments("bash", ".bashrc")]
    [Arguments("zsh",  ".zshrc")]
    public async Task GetRcFilePath_ReturnsHomeRelativePath(string shell, string expectedSuffix)
    {
        var result = ShellDetector.GetRcFilePath(shell);
        await Assert.That(result).IsNotNull();
        await Assert.That(result!).EndsWith(expectedSuffix);
    }

    [Test]
    public async Task GetRcFilePath_ForFish_IsInsideConfigFishConfd()
    {
        var result = ShellDetector.GetRcFilePath("fish");
        await Assert.That(result).IsNotNull();
        await Assert.That(result!).Contains(Path.Combine("fish", "conf.d"));
    }

    [Test]
    public async Task GetRcFilePath_ForUnknownShell_ReturnsNull()
    {
        var result = ShellDetector.GetRcFilePath("unknown-shell");
        await Assert.That(result).IsNull();
    }
}
