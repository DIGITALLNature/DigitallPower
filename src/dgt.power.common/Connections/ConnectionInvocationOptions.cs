// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common;

namespace dgt.power.common.Connections;

public sealed class ConnectionInvocationOptions
{
    private ConnectionInvocationOptions(string? connectionName, string? connectionString, bool nonInteractive)
    {
        ConnectionName = connectionName;
        ConnectionString = connectionString;
        NonInteractive = nonInteractive;
        AllowUnencryptedStorage = ExecutionEnvironment.IsTruthy(
            Environment.GetEnvironmentVariable("DGTP_ALLOW_UNENCRYPTED_STORAGE"));
    }

    public string? ConnectionName { get; }

    public string? ConnectionString { get; }

    public bool NonInteractive { get; }

    public bool AllowUnencryptedStorage { get; }

    public static ConnectionInvocationOptions FromArguments(IReadOnlyList<string> args)
    {
        ArgumentNullException.ThrowIfNull(args);
        var argumentConnection = GetOptionValue(args, "--connection");
        var argumentConnectionString = GetOptionValue(args, "--connection-string");
        var argumentNonInteractive = args.Any(argument =>
            string.Equals(argument, "--non-interactive", StringComparison.OrdinalIgnoreCase));

        return new ConnectionInvocationOptions(
            argumentConnection ?? Environment.GetEnvironmentVariable("DGTP_CONNECTION"),
            argumentConnectionString ?? Environment.GetEnvironmentVariable("DGTP_CONNECTION_STRING"),
            argumentNonInteractive
            || ExecutionEnvironment.IsTruthy(Environment.GetEnvironmentVariable("DGTP_NON_INTERACTIVE")));
    }

    private static string? GetOptionValue(IReadOnlyList<string> args, string option)
    {
        for (var index = 0; index < args.Count; index++)
        {
            var argument = args[index];
            if (argument.StartsWith($"{option}=", StringComparison.OrdinalIgnoreCase))
            {
                return argument[(option.Length + 1)..];
            }

            if (string.Equals(argument, option, StringComparison.OrdinalIgnoreCase)
                && index + 1 < args.Count
                && !args[index + 1].StartsWith("--", StringComparison.Ordinal))
            {
                return args[index + 1];
            }
        }

        return null;
    }
}
