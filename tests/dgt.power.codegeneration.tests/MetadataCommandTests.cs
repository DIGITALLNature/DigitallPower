using dgt.power.codegeneration.Base;
using dgt.power.codegeneration.Constants;
using dgt.power.codegeneration.Logic;
using dgt.power.codegeneration.tests.Base;
using dgt.power.dataverse;
using FluentAssertions;
using Microsoft.Xrm.Sdk.Metadata;
using Xunit.Abstractions;

namespace dgt.power.codegeneration.tests;

public class MetadataCommandTests : CodeGenerationTestsBase<MetadataCommand>
{
    public MetadataCommandTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    [Fact]
    public void ShouldFailOnMissingConfiguration() =>
        GetContext()
            .Execute(new CodeGenerationVerb
            {
                Config = "missing.json"
            })
            .Should().BeFalse();

    [Fact]
    public void ShouldGenerateEntityMetadataForGivenEntities()
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

        context
            .Execute(args)
            .Should().BeTrue();

        Directory.Exists(GetArtifactPath(args.Folder)).Should().BeTrue();
        var metadataDirectoryPath = $"{GetArtifactPath(args.Folder)}/{Folders.Metadata}";
        Directory.Exists(metadataDirectoryPath).Should().BeTrue();
        var files = Directory.GetFiles(metadataDirectoryPath);
        files.Should().HaveCount(1);
        var generatedAccountMetadata = GetEntityMetadataArtifact(accountMetadata.LogicalName);
        generatedAccountMetadata.EntitySetName.Should().Be(accountMetadata.EntitySetName);
    }

    [Fact]
    public void ShouldCreateModelFolderIfNotExisting()
    {
        var context = GetContext();

        var config = new CodeGenerationConfig();
        var args = new CodeGenerationVerb
        {
            Config = WriteConfigurationArtifact(config).FullName,
            TargetDirectory = ArtifactDirectory,
        };

        context
            .Execute(args)
            .Should().BeTrue();

        Directory.Exists(GetArtifactPath(args.Folder)).Should().BeTrue();
        Directory.Exists($"{GetArtifactPath(args.Folder)}/{Folders.Metadata}").Should().BeTrue();
    }

    [Fact]
    public void ShouldDeleteExistingFilesInMetaDataFolder()
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
        File.Exists(testEntityMetdataPath).Should().BeTrue();

        context
            .Execute(args)
            .Should().BeTrue();

        Directory.Exists(GetArtifactPath(args.Folder)).Should().BeTrue();
        Directory.Exists(metadataDirectoryPath).Should().BeTrue();
        File.Exists(testEntityMetdataPath).Should().BeFalse();
        Directory.EnumerateFileSystemEntries(metadataDirectoryPath).Should().BeEmpty();
    }
}
