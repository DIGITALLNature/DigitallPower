// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common.Extensions;
using dgt.power.plugin.Deployment.Local;
using Spectre.Console;

namespace dgt.power.plugin.Deployment.Batch;

/// <summary>Reads every resolved artifact before any Dataverse access so the batch can be validated as a whole.</summary>
internal sealed class DeploymentTargetReader(IAnsiConsole console)
{
    internal TargetReadResult Read(IReadOnlyList<string> paths, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(paths);
        var targets = new List<LocalDeploymentTarget>();
        var skipped = new List<string>();
        var failures = new List<TargetReadFailure>();
        foreach (var path in paths)
        {
            cancellationToken.ThrowIfCancellationRequested();
            try
            {
                ReadSingle(path, targets, skipped, failures);
            }
            catch (Exception e) when (e is not OutOfMemoryException and not StackOverflowException and not OperationCanceledException)
            {
                failures.Add(new TargetReadFailure(path, e.DiagnosticMessage()));
            }
        }

        return new TargetReadResult(targets, skipped, failures);
    }

    private void ReadSingle(string path, List<LocalDeploymentTarget> targets, List<string> skipped, List<TargetReadFailure> failures)
    {
        if (path.EndsWith(".nupkg", StringComparison.OrdinalIgnoreCase))
        {
            var package = new PluginPackageReader(console).Read(path);
            if (package is null)
            {
                failures.Add(new TargetReadFailure(path, "Failed to read package"));
                return;
            }

            targets.Add(new LocalDeploymentTarget(path, null, package));
            return;
        }

        using var loadContext = MetadataLoadContextFactory.Create(Path.GetDirectoryName(path)!);
        var assembly = new AssemblyReflectionReader(console).Read(path, loadContext);
        if (assembly is null)
        {
            failures.Add(new TargetReadFailure(path, "Failed to read assembly"));
        }
        else if (assembly.Kind == LocalAssemblyKind.None)
        {
            skipped.Add(path);
        }
        else
        {
            targets.Add(new LocalDeploymentTarget(path, assembly, null));
        }
    }
}
