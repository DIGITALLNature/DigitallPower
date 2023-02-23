// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using dgt.power.maintenance.Logic;
using dgt.power.maintenance.Model.Settings;
using dgt.power.tests;
using dgt.power.tests.Extensions;

namespace dgt.power.maintenance.tests;

public class IncrementSolutionVersionTests : CommandTestsBase<IncrementSolutionVersion, IncrementSolutionVersionSettings>
{
    public IncrementSolutionVersionTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    [Fact]
    public void ShouldFailOnEmptySolutionOption() => GetContext()
        .Execute(new IncrementSolutionVersionSettings
        {
            Solution = string.Empty
        }).Should()
        .Fail();

    [Fact]
    public void ShouldFailOnNonExistingSolution() => GetContext()
        .Execute(new IncrementSolutionVersionSettings
        {
            Solution = "missing"
        }).Should()
        .Fail();

    [Fact]
    public void ShouldFailOnInvalidSolutionVersion() => GetBuilder()
        .WithData(new Solution(Guid.NewGuid())
        {
            UniqueName = "existing",
            Version = "invalid"
        })
        .Build()
        .Execute(new IncrementSolutionVersionSettings
        {
            Solution = "existing"
        }).Should()
        .Fail();

    [Fact]
    public void ShouldFailOnInvalidOptionCombination() => GetBuilder()
        .WithData(new Solution(Guid.NewGuid())
        {
            UniqueName = "existing",
            Version = "1.0.1.2"
        })
        .Build()
        .Execute(new IncrementSolutionVersionSettings
        {
            Solution = "existing",
            Major = false,
            Minor = false,
            Build = false,
            Revision = false
        }).Should()
        .Fail();

    [Fact]
    public void ShouldUpdateMajorVersion()
    {
        var solution = new Solution(Guid.NewGuid())
        {
            UniqueName = "existing",
            Version = "1.0.1.2"
        };

        var context = GetBuilder()
            .WithData(solution)
            .Build();

        context
            .Execute(new IncrementSolutionVersionSettings
            {
                Solution = solution.UniqueName,
                Major = true
            }).Should()
            .Succeed();

        context.GetSingle<Solution>(x => x.Id == solution.Id)
            .Version.Should().Be("2.0.0.0");
    }

    [Fact]
    public void ShouldUpdateMinorVersion()
    {
        var solution = new Solution(Guid.NewGuid())
        {
            UniqueName = "existing",
            Version = "1.0.1.2"
        };

        var context = GetBuilder()
            .WithData(solution)
            .Build();

        context
            .Execute(new IncrementSolutionVersionSettings
            {
                Solution = solution.UniqueName,
                Minor = true,
            }).Should()
            .Succeed();

        context.GetSingle<Solution>(x => x.Id == solution.Id)
            .Version.Should().Be("1.1.0.0");
    }

    [Fact]
    public void ShouldUpdateBuildVersion()
    {
        var solution = new Solution(Guid.NewGuid())
        {
            UniqueName = "existing",
            Version = "1.0.1.2"
        };

        var context = GetBuilder()
            .WithData(solution)
            .Build();

        context
            .Execute(new IncrementSolutionVersionSettings
            {
                Solution = solution.UniqueName,
                Build = true,
            }).Should()
            .Succeed();

        context.GetSingle<Solution>(x => x.Id == solution.Id)
            .Version.Should().Be("1.0.2.0");
    }

    [Fact]
    public void ShouldUpdateRevisionVersion()
    {
        var solution = new Solution(Guid.NewGuid())
        {
            UniqueName = "existing",
            Version = "1.0.1.2"
        };

        var context = GetBuilder()
            .WithData(solution)
            .Build();

        context
            .Execute(new IncrementSolutionVersionSettings
            {
                Solution = solution.UniqueName,
                Revision = true,
            }).Should()
            .Succeed();

        context.GetSingle<Solution>(x => x.Id == solution.Id)
            .Version.Should().Be("1.0.1.3");
    }

    [Fact]
    public void ShouldUpdateRevisionIfAllFlagsOmitted()
    {
        var solution = new Solution(Guid.NewGuid())
        {
            UniqueName = "existing",
            Version = "1.0.1.2"
        };

        var context = GetBuilder()
            .WithData(solution)
            .Build();

        context
            .Execute(new IncrementSolutionVersionSettings
            {
                Solution = solution.UniqueName,
            }).Should()
            .Succeed();

        context.GetSingle<Solution>(x => x.Id == solution.Id)
            .Version.Should().Be("1.0.1.3");
    }
}
