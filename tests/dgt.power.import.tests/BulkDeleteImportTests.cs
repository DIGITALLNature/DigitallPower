using dgt.power.dataverse;
using dgt.power.dto;
using dgt.power.import.Base;
using dgt.power.import.Logic;
using dgt.power.import.tests.Base;
using dgt.power.tests;
using dgt.power.tests.FakeExecutor;
using FluentAssertions;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Xunit.Abstractions;

namespace dgt.power.import.tests;

public class BulkDeleteImportTests : ImportTestBase<BulkDeleteImport>
{
    public BulkDeleteImportTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    protected override CommandTestContextBuilder<BulkDeleteImport, ImportVerb> GetBuilder()
    {
        return base.GetBuilder()
            .WithFakeMessageExecutor<WhoAmIRequest>(new WhoAmIExecutor())
            .WithFakeMessageExecutor<BulkDeleteRequest>(new BulkDeleteExecutor());
    }


    [Fact]
    public void ShouldFailOnCreationOfBulkDeleteWithoutFetchXml()
    {
        var (disabledBulkDeleteJob, existingBulkDeleteJob, data) = GetData();

        var context = GetBuilder()
            .WithData(data)
            .WithData(new[]
            {
                disabledBulkDeleteJob,
                existingBulkDeleteJob,
            })
            .Build();
        context.Execute(new ImportVerb
            {
                FileName = "missing-fetch-deletes.json",
                FileDir = ResourceDirectory
            }
        ).Should().BeFalse();

        context.Get<AsyncOperation>().Should().NotContain(x => x.Name == "Missing FetchXml");
    }

    [Fact]
    public void ShouldFailOnMissingBulkDeleteJobs()
    {
        var (disabledBulkDeleteJob, existingBulkDeleteJob, data) = GetData();
        var missingBulkDeleteJob = new AsyncOperation(Guid.NewGuid())
        {
            Name = "Missing Job",
            RecurrenceStartTime = DateTime.UtcNow,
            OperationType = new OptionSetValue(AsyncOperation.Options.OperationType.BulkDelete)
        };
        var context = GetBuilder()
            .WithData(data)
            .WithData(new[]
            {
                disabledBulkDeleteJob,
                existingBulkDeleteJob,
                missingBulkDeleteJob
            })
            .Build();
        context.Execute(new ImportVerb
            {
                FileName = GetResourcePath("bulk-deletes.json"),
                FileDir = ResourceDirectory
            }
        ).Should().BeFalse();
    }

    [Fact]
    public void ShouldFailOnEmptyConfiguration() =>
        GetContext().Execute(new ImportVerb
            {
                FileName = WriteConfigurationArtifact(new BulkDeletes
                {
                    Deletes = new List<BulkDelete>(),
                }).Name,
                FileDir = ArtifactDirectory
            }
        ).Should().BeFalse();

    [Fact]
    public void ShouldFailOnWrongConfiguration() =>
        GetContext().Execute(new ImportVerb
            {
                FileName = string.Empty,
                FileDir = ArtifactDirectory
        }
        ).Should().BeFalse();

    [Fact]
    public void ShouldCopyDeactivatedBulkDeleteJob()
    {
        var (disabledBulkDeleteJob, existingBulkDeleteJob, data) = GetData();
        existingBulkDeleteJob.RecurrencePattern = string.Empty;
        var context = GetBuilder()
            .WithData(data)
            .WithData(new[]
            {
                disabledBulkDeleteJob,
                existingBulkDeleteJob,
            })
            .Build();
        context.Execute(new ImportVerb
            {
                FileName = "bulk-deletes.json",
                FileDir = ResourceDirectory
            }
        ).Should().BeTrue();

        var deletes = context.Get<AsyncOperation>(x => x.OperationType != null
                                                       && x.OperationType.Value == AsyncOperation.Options.OperationType
                                                           .BulkDelete
                                                       && x.RecurrenceStartTime != null)
            .OrderBy(x => x.Name)
            .ToList();

        deletes.Should().HaveCount(3);

        var existing = deletes.Single(x => x.Name == existingBulkDeleteJob.Name);
        existing.RecurrencePattern.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public void ShouldImportBulkDeleteJobsWithAlreadyDisabledJob()
    {
        var (disabledBulkDeleteJob, existingBulkDeleteJob, data) = GetData();
        disabledBulkDeleteJob.RecurrencePattern = string.Empty;
        var context = GetBuilder()
            .WithData(data)
            .WithData(new[]
            {
                disabledBulkDeleteJob,
                existingBulkDeleteJob,
            })
            .Build();
        context.Execute(new ImportVerb
            {
                FileName = "bulk-deletes.json",
                FileDir = ResourceDirectory
            }
        ).Should().BeTrue();

        var deletes = context.Get<AsyncOperation>(x => x.OperationType != null
                                                       && x.OperationType.Value == AsyncOperation.Options.OperationType
                                                           .BulkDelete
                                                       && x.RecurrenceStartTime != null)
            .OrderBy(x => x.Name)
            .ToList();

        deletes.Should().HaveCount(3);

        var disabled = deletes.Single(e => e.Id == disabledBulkDeleteJob.Id);
        disabled.RecurrencePattern.Should().Be(disabledBulkDeleteJob.RecurrencePattern);
    }


    [Fact]
    public void ShouldImportBulkDeleteJobs()
    {
        var (disabledBulkDeleteJob, existingBulkDeleteJob, data) = GetData();
        var context = GetBuilder()
            .WithData(data)
            .WithData(new[]
            {
                disabledBulkDeleteJob,
                existingBulkDeleteJob,
            })
            .Build();
        context.Execute(new ImportVerb
            {
                FileName = "bulk-deletes.json",
                FileDir = ResourceDirectory
            }
        ).Should().BeTrue();

        var deletes = context.Get<AsyncOperation>(x => x.OperationType != null
                                                       && x.OperationType.Value == AsyncOperation.Options.OperationType
                                                           .BulkDelete
                                                       && x.RecurrenceStartTime != null)
            .OrderBy(x => x.Name)
            .ToList();

        deletes.Should().HaveCount(3);

        var disabled = deletes.Single(e => e.Name == disabledBulkDeleteJob.Name);
        (disabled.RecurrenceStartTime?.Ticks ?? -1)
            .Should()
            .BeInRange(DateTime.UtcNow.Ticks, DateTime.UtcNow.AddMinutes(10).AddSeconds(5).Ticks);

        var existing = deletes.Single(e => e.Name == existingBulkDeleteJob.Name);
        existing.RecurrencePattern.Should().Be("FREQ=DAILY");
        $"{existing.RecurrenceStartTime:HH:mm}".Should().Be("05:34");
    }

    [Fact]
    public void ShouldNotCreateDisabledBulkDeleteJob()
    {
        var (disabledBulkDeleteJob, existingBulkDeleteJob, data) = GetData();
        var context = GetBuilder()
            .WithData(data)
            .WithData(new[]
            {
                disabledBulkDeleteJob,
                existingBulkDeleteJob
            })
            .Build();
        context.Execute(new ImportVerb
            {
                FileName = "disabled-bulk-deletes.json",
                FileDir = ResourceDirectory,
            }
        ).Should().BeTrue();

#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var deletes = (from rec in context.DataContext.AsyncOperationSet
            where rec.OperationType != null
            where rec.OperationType.Value == AsyncOperation.Options.OperationType.BulkDelete
            where rec.RecurrenceStartTime != null
            orderby rec.Name
            select rec).ToList();
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        deletes.Should().HaveCount(2);

        var disabled = deletes.Single(e => e.Name == disabledBulkDeleteJob.Name);
        Assert.InRange(disabled.RecurrenceStartTime?.Ticks ?? -1, DateTime.UtcNow.Ticks,
            DateTime.UtcNow.AddMinutes(10).AddSeconds(5).Ticks);
        (disabled.RecurrenceStartTime?.Ticks ?? -1)
            .Should()
            .BeInRange(DateTime.UtcNow.Ticks, DateTime.UtcNow.AddMinutes(10).AddSeconds(5).Ticks);


        var existing = deletes.Single(e => e.Name == existingBulkDeleteJob.Name);
        existing.RecurrencePattern.Should().Be("FREQ=DAILY");
        $"{existing.RecurrenceStartTime:HH:mm}".Should().Be("09:34");
    }

    private static (AsyncOperation disabledBulkDelete, AsyncOperation existingBulkDelete, IEnumerable<Entity> data)
        GetData()
    {
        var system = new SystemUser(Guid.NewGuid())
        {
            LastName = "SYSTEM",
            IsDisabled = true,
            Attributes =
            {
                {"fullname", "SYSTEM"}
            }
        };

        var techUser = new SystemUser(Guid.NewGuid())
        {
            DomainName = "tech.user@domain.suffix",
            FirstName = "Tech.",
            LastName = "User",
            IsDisabled = false,
            Attributes =
            {
                {"fullname", "Tech. User"}
            }
        };

        var pipelineUser = new SystemUser(Guid.Parse("f4e8821a-97d2-4938-8b73-8744431e59c8"))
        {
            DomainName = "pipeline.user@devlab.onmicrosoft.com",
            FirstName = "Pipeline",
            LastName = "User",
            IsDisabled = false,
            Attributes =
            {
                {"fullname", "Pipeline User"}
            }
        };

        var existingBulkDelete = new AsyncOperation(Guid.NewGuid())
        {
            Name = "Analysis Results Cleanup Job",
            OperationType = new OptionSetValue(AsyncOperation.Options.OperationType.BulkDelete),
            RecurrenceStartTime = DateTime.UtcNow.AddDays(-1),
            RecurrencePattern = "FREQ=WEEKLY",
            OwnerId = system.ToEntityReference(),
            Data =
                "<string>&lt;fetch version=\"1.0\" output-format=\"xml-platform\" mapping=\"logical\" &gt;&lt;entity name=\"testentity\" &gt;&lt;attribute name=\"name\" /&gt;&lt;/entity&gt;&lt;/fetch&gt;</string>"
        };

        var disabledBulkDelete = new AsyncOperation(Guid.NewGuid())
        {
            Name = "Old Bulk Delete",
            OperationType = new OptionSetValue(AsyncOperation.Options.OperationType.BulkDelete),
            RecurrenceStartTime = DateTime.UtcNow.AddDays(-1),
            RecurrencePattern = "FREQ=WEEKLY",
            OwnerId = system.ToEntityReference(),
            Data =
                "<string>&lt;fetch version=\"1.0\" output-format=\"xml-platform\" mapping=\"logical\" &gt;&lt;entity name=\"testentity\" &gt;&lt;attribute name=\"name\" /&gt;&lt;/entity&gt;&lt;/fetch&gt;</string>"
        };


        return (disabledBulkDelete, existingBulkDelete, new[] {system, techUser, pipelineUser});
    }
}
