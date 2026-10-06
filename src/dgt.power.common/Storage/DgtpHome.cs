// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using IOPath = System.IO.Path;

namespace dgt.power.common.Storage;

public sealed class DgtpHome
{
    public DgtpHome(string? path = null)
    {
        Path = IOPath.GetFullPath(path ?? ResolveDefaultPath());
        Directory.CreateDirectory(Path);
        RestrictDirectoryPermissions(Path);
    }

    public string Path { get; }

    public string ConnectionsPath => IOPath.Combine(Path, "connections.json");

    public string StatePath => IOPath.Combine(Path, "state.json");

    public string SecretsDirectory
    {
        get
        {
            var directory = IOPath.Combine(Path, "secrets");
            Directory.CreateDirectory(directory);
            RestrictDirectoryPermissions(directory);
            return directory;
        }
    }

    private static string ResolveDefaultPath()
    {
        var configuredPath = Environment.GetEnvironmentVariable("DGTP_HOME");
        if (!string.IsNullOrWhiteSpace(configuredPath))
        {
            return configuredPath;
        }

        if (OperatingSystem.IsLinux())
        {
            var xdgDataHome = Environment.GetEnvironmentVariable("XDG_DATA_HOME");
            if (!string.IsNullOrWhiteSpace(xdgDataHome))
            {
                return IOPath.Combine(xdgDataHome, "dgtp");
            }

            return IOPath.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                ".local",
                "share",
                "dgtp");
        }

        return IOPath.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "dgtp");
    }

    internal static void RestrictDirectoryPermissions(string path)
    {
        if (!OperatingSystem.IsWindows())
        {
            File.SetUnixFileMode(path, UnixFileMode.UserRead | UnixFileMode.UserWrite | UnixFileMode.UserExecute);
        }
    }
}
