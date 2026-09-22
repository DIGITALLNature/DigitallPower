// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using dgt.power.solution;
using dgt.power.solution.Base;
using dgt.power.tests;
using dgt.power.tests.Extensions;

namespace dgt.power.solution.tests;

public class SolutionVersionCommandTests : CommandTestsBase<SolutionVersionCommand, SolutionVersionSettings>
{
    [Test]
    public async Task ShouldFailOnEmptySolutionOption() => await GetContext()
        .Execute(new SolutionVersionSettings
        {
            Solution = string.Empty
        })
        .Fail();

    [Test]
    public async Task ShouldFailOnNonExistingSolution() => await GetContext()
        .Execute(new SolutionVersionSettings
        {
            Solution = "missing"
        })
        .Fail();

    [Test]
    public async Task ShouldFailOnInvalidSolutionVersion() => await GetBuilder()
        .WithData(new Solution(Guid.NewGuid())
        {
            UniqueName = "existing",
            Version = "invalid"
        })
        .Build()
        .Execute(new SolutionVersionSettings
        {
            Solution = "existing"
        })
        .Fail();

    [Test]
    public async Task ShouldFailOnInvalidOptionCombination() => await GetBuilder()
        .WithData(new Solution(Guid.NewGuid())
        {
            UniqueName = "existing",
            Version = "1.0.1.2"
        })
        .Build()
        .Execute(new SolutionVersionSettings
        {
            Solution = "existing",
            Major = false,
            Minor = false,
            Build = false,
            Revision = false
        })
        .Fail();

    [Test]
    public async Task ShouldUpdateMajorVersion()
    {
        var solution = new Solution(Guid.NewGuid())
        {
            UniqueName = "existing",
            Version = "1.0.1.2"
        };

        var context = GetBuilder()
            .WithData(solution)
            .Build();

        await context
            .Execute(new SolutionVersionSettings
            {
                Solution = solution.UniqueName,
                Major = true
            })
            .Succeed();

        await Assert.That(context.GetSingle<Solution>(x => x.Id == solution.Id)
            .Version).IsEqualTo("2.0.0.0");
    }

    [Test]
    public async Task ShouldUpdateMinorVersion()
    {
        var solution = new Solution(Guid.NewGuid())
        {
            UniqueName = "existing",
            Version = "1.0.1.2"
        };

        var context = GetBuilder()
            .WithData(solution)
            .Build();

        await context
            .Execute(new SolutionVersionSettings
            {
                Solution = solution.UniqueName,
                Minor = true
            })
            .Succeed();

        await Assert.That(context.GetSingle<Solution>(x => x.Id == solution.Id)
            .Version).IsEqualTo("1.1.0.0");
    }

    [Test]
    public async Task ShouldUpdateBuildVersion()
    {
        var solution = new Solution(Guid.NewGuid())
        {
            UniqueName = "existing",
            Version = "1.0.1.2"
        };

        var context = GetBuilder()
            .WithData(solution)
            .Build();

        await context
            .Execute(new SolutionVersionSettings
            {
                Solution = solution.UniqueName,
                Build = true
            })
            .Succeed();

        await Assert.That(context.GetSingle<Solution>(x => x.Id == solution.Id)
            .Version).IsEqualTo("1.0.2.0");
    }

    [Test]
    public async Task ShouldUpdateRevisionVersion()
    {
        var solution = new Solution(Guid.NewGuid())
        {
            UniqueName = "existing",
            Version = "1.0.1.2"
        };

        var context = GetBuilder()
            .WithData(solution)
            .Build();

        await context
            .Execute(new SolutionVersionSettings
            {
                Solution = solution.UniqueName,
                Revision = true
            })
            .Succeed();

        await Assert.That(context.GetSingle<Solution>(x => x.Id == solution.Id)
            .Version).IsEqualTo("1.0.1.3");
    }

    [Test]
    public async Task ShouldUpdateRevisionIfAllFlagsOmitted()
    {
        var solution = new Solution(Guid.NewGuid())
        {
            UniqueName = "existing",
            Version = "1.0.1.2"
        };

        var context = GetBuilder()
            .WithData(solution)
            .Build();

        await context
            .Execute(new SolutionVersionSettings
            {
                Solution = solution.UniqueName
            })
            .Succeed();

        await Assert.That(context.GetSingle<Solution>(x => x.Id == solution.Id)
            .Version).IsEqualTo("1.0.1.3");
    }
}
