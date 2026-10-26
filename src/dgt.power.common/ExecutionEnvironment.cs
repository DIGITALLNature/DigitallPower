// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

namespace dgt.power.common;

public static class ExecutionEnvironment
{
    private static readonly string[] s_ciEnvironmentVariables =
    [
        "TF_BUILD",
        "BUILD_BUILDURI",
        "GITHUB_ACTIONS",
        "GITLAB_CI",
        "JENKINS_URL",
        "CI"
    ];

    public static IReadOnlyCollection<string> CiEnvironmentVariables => s_ciEnvironmentVariables;

    public static bool IsCiAgent => Array.Exists(
        s_ciEnvironmentVariables,
        environmentVariable => Environment.GetEnvironmentVariable(environmentVariable) != null);

    public static bool IsTruthy(string? value) =>
        value is not null &&
        (value.Equals("1", StringComparison.Ordinal) ||
         value.Equals("true", StringComparison.OrdinalIgnoreCase) ||
         value.Equals("yes", StringComparison.OrdinalIgnoreCase));
}
