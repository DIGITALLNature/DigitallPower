// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.plugin.Deployment.Batch;

namespace dgt.power.plugin.tests.Deployment.Batch;

public sealed class DeploymentTargetResolverTests : IDisposable
{
    private readonly string _root = Directory.CreateTempSubdirectory("dgtp-resolver-").FullName;

    public void Dispose() => Directory.Delete(_root, recursive: true);

    private string Touch(string relative)
    {
        var path = Path.Combine(_root, relative);
        Directory.CreateDirectory(Path.GetDirectoryName(path)!);
        File.WriteAllText(path, string.Empty);
        return path;
    }

    [Test]
    public async Task Resolve_File_ReturnsFullPath()
    {
        var file = Touch("a.dll");

        await Assert.That(DeploymentTargetResolver.Resolve(file)).IsEquivalentTo([file]);
    }

    [Test]
    public async Task Resolve_UnsupportedFile_Throws()
    {
        var file = Touch("a.txt");

        await Assert.That(() => DeploymentTargetResolver.Resolve(file)).Throws<DeploymentTargetException>();
    }

    [Test]
    public async Task Resolve_MissingPath_Throws()
    {
        await Assert.That(() => DeploymentTargetResolver.Resolve(Path.Combine(_root, "missing"))).Throws<DeploymentTargetException>();
    }

    [Test]
    public async Task Resolve_Directory_IsTopLevelOnlySortedAndFiltered()
    {
        var b = Touch("b.nupkg");
        var a = Touch("A.dll");
        Touch("c.pdb");
        Touch(Path.Combine("sub", "nested.dll"));

        var resolved = DeploymentTargetResolver.Resolve(_root);

        await Assert.That(resolved).IsEquivalentTo([a, b]);
        await Assert.That(resolved[0]).IsEqualTo(a);
    }

    [Test]
    public async Task Resolve_RecursiveGlob_MatchesParallelBinPaths()
    {
        var one = Touch(Path.Combine("artifacts", "one", "bin", "Release", "one.nupkg"));
        var two = Touch(Path.Combine("artifacts", "two", "bin", "Release", "two.nupkg"));
        Touch(Path.Combine("artifacts", "two", "bin", "Debug", "two.nupkg"));
        Touch(Path.Combine("artifacts", "two", "bin", "Release", "two.dll"));

        var resolved = DeploymentTargetResolver.Resolve(Path.Combine(_root, "artifacts", "**", "bin", "Release", "*.nupkg"));

        await Assert.That(resolved).IsEquivalentTo([one, two]);
    }

    [Test]
    public async Task Resolve_GlobWithForwardSlashes_Matches()
    {
        var file = Touch(Path.Combine("x", "plugin.dll"));

        var resolved = DeploymentTargetResolver.Resolve(_root.Replace('\\', '/') + "/*/plugin.dll");

        await Assert.That(resolved).IsEquivalentTo([file]);
    }

    [Test]
    public async Task Resolve_GlobWithoutMatch_Throws()
    {
        Touch("a.dll");

        await Assert.That(() => DeploymentTargetResolver.Resolve(Path.Combine(_root, "**", "*.nupkg"))).Throws<DeploymentTargetException>();
    }

    [Test]
    public async Task Resolve_GlobWithMissingRoot_Throws()
    {
        await Assert.That(() => DeploymentTargetResolver.Resolve(Path.Combine(_root, "nope", "*.dll"))).Throws<DeploymentTargetException>();
    }
}
