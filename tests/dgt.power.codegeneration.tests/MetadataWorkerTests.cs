// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.codegeneration.Base;
using dgt.power.codegeneration.Constants;
using dgt.power.codegeneration.Logic;
using dgt.power.codegeneration.tests.Base;
using dgt.power.dataverse;
using Microsoft.Xrm.Sdk.Metadata;

namespace dgt.power.codegeneration.tests;

public class MetadataWorkerTests : CodeGenerationTestsBase<MetadataWorker>
{
    [Test]
    public async Task ShouldFailOnMissingConfiguration() =>
        await Assert.That(GetContext()
            .Execute(new CodeGenerationVerb
            {
                Config = "missing.json"
            })).IsFalse();

    [Test]
    public async Task ShouldGenerateEntityMetadataForGivenEntities()
    {
        var accountMetadata = new EntityMetadata
        {
            LogicalName = Account.EntityLogicalName,
            EntitySetName = "accounts"
        };

        var context = GetBuilder()
            .WithMetaData(accountMetadata)
            .Build();

        var config = new CodeGenerationConfig
        {
            Entities = new[]
            {
                accountMetadata.LogicalName
            }
        };
        var args = new CodeGenerationVerb
        {
            Config = WriteConfigurationArtifact(config).FullName,
            TargetDirectory = ArtifactDirectory,
        };

        await Assert.That(context
            .Execute(args)).IsTrue();

        await Assert.That(Directory.Exists(GetArtifactPath(args.Folder))).IsTrue();
        var metadataDirectoryPath = $"{GetArtifactPath(args.Folder)}/{Folders.Metadata}";
        await Assert.That(Directory.Exists(metadataDirectoryPath)).IsTrue();
        var files = Directory.GetFiles(metadataDirectoryPath);
        await Assert.That(files).HasCount().EqualTo(1);
        var generatedAccountMetadata = GetEntityMetadataArtifact(accountMetadata.LogicalName);
        await Assert.That(generatedAccountMetadata.EntitySetName).IsEqualTo(accountMetadata.EntitySetName);
    }

    [Test]
    public async Task ShouldCreateModelFolderIfNotExisting()
    {
        var context = GetContext();

        var config = new CodeGenerationConfig();
        var args = new CodeGenerationVerb
        {
            Config = WriteConfigurationArtifact(config).FullName,
            TargetDirectory = ArtifactDirectory,
        };

        await Assert.That(context
            .Execute(args)).IsTrue();

        await Assert.That(Directory.Exists(GetArtifactPath(args.Folder))).IsTrue();
        await Assert.That(Directory.Exists($"{GetArtifactPath(args.Folder)}/{Folders.Metadata}")).IsTrue();
    }

    [Test]
    public async Task ShouldDeleteExistingFilesInMetaDataFolder()
    {
        var context = GetContext();
        var config = new CodeGenerationConfig();
        var args = new CodeGenerationVerb
        {
            Config = WriteConfigurationArtifact(config).FullName,
            TargetDirectory = ArtifactDirectory,
        };
        Directory.CreateDirectory(GetArtifactPath(args.Folder));
        var metadataDirectoryPath = $"{GetArtifactPath(args.Folder)}/{Folders.Metadata}";
        Directory.CreateDirectory(metadataDirectoryPath);
        var testEntityMetdataPath = $"{metadataDirectoryPath}/testentity.xml";
        File.WriteAllText(testEntityMetdataPath, "<entity/>");
        await Assert.That(File.Exists(testEntityMetdataPath)).IsTrue();

        await Assert.That(context
            .Execute(args)).IsTrue();

        await Assert.That(Directory.Exists(GetArtifactPath(args.Folder))).IsTrue();
        await Assert.That(Directory.Exists(metadataDirectoryPath)).IsTrue();
        await Assert.That(File.Exists(testEntityMetdataPath)).IsFalse();
        await Assert.That(Directory.EnumerateFileSystemEntries(metadataDirectoryPath)).IsEmpty();
    }
}
