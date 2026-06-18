// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common;

namespace dgt.power.cli.tests;

[NotInParallel(nameof(ExecutionEnvironmentTests))]
public class ExecutionEnvironmentTests
{
    [Test]
    public async Task IsCiAgent_ReturnsFalse_WhenNoCiEnvironmentVariablesAreSet()
    {
        var savedVars = ExecutionEnvironment.CiEnvironmentVariables
            .ToDictionary(environmentVariable => environmentVariable,
                Environment.GetEnvironmentVariable);

        try
        {
            foreach (var environmentVariable in ExecutionEnvironment.CiEnvironmentVariables)
            {
                Environment.SetEnvironmentVariable(environmentVariable, null);
            }

            await Assert.That(ExecutionEnvironment.IsCiAgent).IsFalse();
        }
        finally
        {
            foreach (var (environmentVariable, value) in savedVars)
            {
                Environment.SetEnvironmentVariable(environmentVariable, value);
            }
        }
    }

    [Test]
    [Arguments("TF_BUILD", "true")]
    [Arguments("BUILD_BUILDURI", "vstfs:///Build/Build/123")]
    [Arguments("GITHUB_ACTIONS", "true")]
    [Arguments("GITLAB_CI", "true")]
    [Arguments("JENKINS_URL", "http://jenkins.local")]
    [Arguments("CI", "true")]
    public async Task IsCiAgent_ReturnsTrue_WhenAnyKnownCiEnvironmentVariableIsSet(string environmentVariable, string value)
    {
        var savedVars = ExecutionEnvironment.CiEnvironmentVariables
            .ToDictionary(variable => variable, Environment.GetEnvironmentVariable);

        try
        {
            foreach (var variable in ExecutionEnvironment.CiEnvironmentVariables)
            {
                Environment.SetEnvironmentVariable(variable, null);
            }

            Environment.SetEnvironmentVariable(environmentVariable, value);
            await Assert.That(ExecutionEnvironment.IsCiAgent).IsTrue();
        }
        finally
        {
            foreach (var (variable, originalValue) in savedVars)
            {
                Environment.SetEnvironmentVariable(variable, originalValue);
            }
        }
    }

    [Test]
    [Arguments("1")]
    [Arguments("true")]
    [Arguments("TRUE")]
    [Arguments("yes")]
    [Arguments("YES")]
    public async Task IsTruthy_ReturnsTrue_ForTruthyValues(string value)
    {
        await Assert.That(ExecutionEnvironment.IsTruthy(value)).IsTrue();
    }

    [Test]
    [Arguments(null)]
    [Arguments("")]
    [Arguments("0")]
    [Arguments("false")]
    [Arguments("no")]
    [Arguments("random")]
    public async Task IsTruthy_ReturnsFalse_ForNonTruthyValues(string? value)
    {
        await Assert.That(ExecutionEnvironment.IsTruthy(value)).IsFalse();
    }
}
