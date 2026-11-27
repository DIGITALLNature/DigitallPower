// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using System.IO.Compression;

namespace dgt.power.webresource.tests.Scaffolding;

internal static class TemplateArchiveFixture
{
    private const string Root = "webresources-template-test/";

    public static byte[] Create(params FixtureEntry[] entries) =>
        CreateRaw(
        [
            new FixtureEntry(Root, [], (int)FileAttributes.Directory),
            .. entries.Select(entry => entry with { Name = Root + entry.Name })
        ]);

    public static byte[] CreateRaw(params FixtureEntry[] entries)
    {
        using var output = new MemoryStream();
        using (var archive = new ZipArchive(output, ZipArchiveMode.Create, leaveOpen: true))
        {
            foreach (var fixtureEntry in entries)
            {
                var entry = archive.CreateEntry(fixtureEntry.Name, CompressionLevel.NoCompression);
                if (fixtureEntry.ExternalAttributes is { } attributes)
                {
                    entry.ExternalAttributes = attributes;
                }

                if (fixtureEntry.Name.EndsWith('/'))
                {
                    continue;
                }

                using var content = entry.Open();
                content.Write(fixtureEntry.Content);
            }
        }

        return output.ToArray();
    }

    internal sealed record FixtureEntry(string Name, byte[] Content, int? ExternalAttributes = null)
    {
        public FixtureEntry(string name, string content, int? externalAttributes = null)
            : this(name, System.Text.Encoding.UTF8.GetBytes(content), externalAttributes)
        {
        }
    }
}
