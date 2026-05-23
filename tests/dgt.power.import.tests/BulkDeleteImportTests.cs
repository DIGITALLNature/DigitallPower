// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using dgt.power.dto;
using dgt.power.import.Base;
using dgt.power.import.Logic;
using dgt.power.import.tests.Base;
using dgt.power.tests;
using dgt.power.tests.FakeExecutor;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;

namespace dgt.power.import.tests;

public class BulkDeleteImportTests : ImportTestBase<BulkDeleteImport>
{
    protected override CommandTestContextBuilder<BulkDeleteImport, ImportVerb> GetBuilder()
    {
        return base.GetBuilder()
            .WithFakeMessageExecutor(new WhoAmIExecutor())
            .WithFakeMessageExecutor(new BulkDeleteExecutor())
            .WithCustomConfiguration(svc => svc.Options.UserId = Guid.Parse("f4e8821a-97d2-4938-8b73-8744431e59c8"));
    }


    [Test]
    public async Task ShouldFailOnCreationOfBulkDeleteWithoutFetchXml()
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
        await Assert.That(context.Execute(new ImportVerb
            {
                FileName = "missing-fetch-deletes.json",
                FileDir = ResourceDirectory
            }
        )).IsFalse();

        await Assert.That(context.Get<AsyncOperation>().Any(x => x.Name == "Missing FetchXml")).IsFalse();
    }

    [Test]
    public async Task ShouldFailOnMissingBulkDeleteJobs()
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
        await Assert.That(context.Execute(new ImportVerb
            {
                FileName = GetResourcePath("bulk-deletes.json"),
                FileDir = ResourceDirectory
            }
        )).IsFalse();
    }

    [Test]
    public async Task ShouldFailOnEmptyConfiguration() =>
        await Assert.That(GetContext().Execute(new ImportVerb
            {
                FileName = WriteConfigurationArtifact(new BulkDeletes
                {
                    Deletes = new List<BulkDelete>(),
                }).Name,
                FileDir = ArtifactDirectory
            }
        )).IsFalse();

    [Test]
    public async Task ShouldFailOnWrongConfiguration() =>
        await Assert.That(GetContext().Execute(new ImportVerb
            {
                FileName = string.Empty,
                FileDir = ArtifactDirectory
        }
        )).IsFalse();

    [Test]
    public async Task ShouldCopyDeactivatedBulkDeleteJob()
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
        await Assert.That(context.Execute(new ImportVerb
            {
                FileName = "bulk-deletes.json",
                FileDir = ResourceDirectory
            }
        )).IsTrue();

        var deletes = context.Get<AsyncOperation>(x => x.OperationType != null
                                                        && x.OperationType.Value == AsyncOperation.Options.OperationType
                                                            .BulkDelete
                                                        && x.RecurrenceStartTime != null)
            .OrderBy(x => x.Name)
            .ToList();

        await Assert.That(deletes).HasCount().EqualTo(3);

        var existing = deletes.Single(x => x.Name == existingBulkDeleteJob.Name);
        await Assert.That(string.IsNullOrWhiteSpace(existing.RecurrencePattern)).IsFalse();
    }

    [Test]
    public async Task ShouldImportBulkDeleteJobsWithAlreadyDisabledJob()
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
        await Assert.That(context.Execute(new ImportVerb
            {
                FileName = "bulk-deletes.json",
                FileDir = ResourceDirectory
            }
        )).IsTrue();

        var deletes = context.Get<AsyncOperation>(x => x.OperationType != null
                                                        && x.OperationType.Value == AsyncOperation.Options.OperationType
                                                            .BulkDelete
                                                        && x.RecurrenceStartTime != null)
            .OrderBy(x => x.Name)
            .ToList();

        await Assert.That(deletes).HasCount().EqualTo(3);

        var disabled = deletes.Single(e => e.Id == disabledBulkDeleteJob.Id);
        await Assert.That(disabled.RecurrencePattern).IsEqualTo(disabledBulkDeleteJob.RecurrencePattern);
    }


    [Test]
    public async Task ShouldImportBulkDeleteJobs()
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
        await Assert.That(context.Execute(new ImportVerb
            {
                FileName = "bulk-deletes.json",
                FileDir = ResourceDirectory
            }
        )).IsTrue();

        var deletes = context.Get<AsyncOperation>(x => x.OperationType != null
                                                        && x.OperationType.Value == AsyncOperation.Options.OperationType
                                                            .BulkDelete
                                                        && x.RecurrenceStartTime != null)
            .OrderBy(x => x.Name)
            .ToList();

        await Assert.That(deletes).HasCount().EqualTo(3);

        var disabled = deletes.Single(e => e.Name == disabledBulkDeleteJob.Name);
        var disabledTicks = disabled.RecurrenceStartTime?.Ticks ?? -1;
        await Assert.That(disabledTicks >= DateTime.UtcNow.Ticks && disabledTicks <= DateTime.UtcNow.AddMinutes(10).AddSeconds(5).Ticks).IsTrue();

        var existing = deletes.Single(e => e.Name == existingBulkDeleteJob.Name);
        await Assert.That(existing.RecurrencePattern).IsEqualTo("FREQ=DAILY");
        await Assert.That($"{existing.RecurrenceStartTime:HH:mm}").IsEqualTo("05:34");
    }

    [Test]
    public async Task ShouldNotCreateDisabledBulkDeleteJob()
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
        await Assert.That(context.Execute(new ImportVerb
            {
                FileName = "disabled-bulk-deletes.json",
                FileDir = ResourceDirectory,
            }
        )).IsTrue();

#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var deletes = (from rec in context.DataContext.AsyncOperationSet
            where rec.OperationType != null
            where rec.OperationType.Value == AsyncOperation.Options.OperationType.BulkDelete
            where rec.RecurrenceStartTime != null
            orderby rec.Name
            select rec).ToList();
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        await Assert.That(deletes).HasCount().EqualTo(2);

        var disabled = deletes.Single(e => e.Name == disabledBulkDeleteJob.Name);
        var disabledTicks = disabled.RecurrenceStartTime?.Ticks ?? -1;
        await Assert.That(disabledTicks >= DateTime.UtcNow.Ticks && disabledTicks <= DateTime.UtcNow.AddMinutes(10).AddSeconds(5).Ticks).IsTrue();

        var existing = deletes.Single(e => e.Name == existingBulkDeleteJob.Name);
        await Assert.That(existing.RecurrencePattern).IsEqualTo("FREQ=DAILY");
        await Assert.That($"{existing.RecurrenceStartTime:HH:mm}").IsEqualTo("09:34");
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
