// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.Completion;
using TUnit.Assertions.Extensions;

namespace dgt.power.telemetry.tests.Completion;

public class SuggestDirectiveTests
{
    [Test]
    [Arguments("[suggest:0]", 0)]
    [Arguments("[suggest:8]", 8)]
    [Arguments("[suggest:100]", 100)]
    [Arguments("[suggest:32767]", 32767)]
    public async Task TryParse_ValidDirective_ReturnsTrueAndCorrectPosition(string input, int expectedPosition)
    {
        var result = SuggestDirective.TryParse(input, out var directive);

        await Assert.That(result).IsTrue();
        await Assert.That(directive.Position).IsEqualTo(expectedPosition);
    }

    [Test]
    [Arguments("")]
    [Arguments("suggest:8")]
    [Arguments("[suggest:]")]
    [Arguments("[suggest:-1]")]
    [Arguments("[suggest:8] extra")]
    [Arguments("export")]
    [Arguments("--help")]
    public async Task TryParse_InvalidInput_ReturnsFalse(string input)
    {
        var result = SuggestDirective.TryParse(input, out _);

        await Assert.That(result).IsFalse();
    }

    [Test]
    [Arguments("[suggest:0]")]
    [Arguments("[suggest:42]")]
    public async Task IsMatch_ValidDirective_ReturnsTrue(string input)
    {
        await Assert.That(SuggestDirective.IsMatch(input)).IsTrue();
    }

    [Test]
    [Arguments("export")]
    [Arguments("")]
    [Arguments("[suggest:]")]
    public async Task IsMatch_InvalidInput_ReturnsFalse(string input)
    {
        await Assert.That(SuggestDirective.IsMatch(input)).IsFalse();
    }
}
