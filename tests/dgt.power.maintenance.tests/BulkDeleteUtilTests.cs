// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using dgt.power.maintenance.Base;
using dgt.power.maintenance.Logic;
using dgt.power.maintenance.tests.Base;
using dgt.power.tests;
using dgt.power.tests.FakeExecutor;

#pragma warning disable CS8602

namespace dgt.power.maintenance.tests;

public class BulkDeleteUtilTests : MaintenanceTestsBase<BulkDeleteUtil>
{
    private readonly BulkDeleteExecutor _bulkDeleteExecutor = new();

    protected override CommandTestContext<BulkDeleteUtil, MaintenanceVerb> GetContext() =>
        GetBuilder()
            .WithFakeMessageExecutor(_bulkDeleteExecutor)
            .Build();

    [Test]
    [Arguments("")]
    [Arguments("       ")]
    [Arguments(null)]
    public async Task ShouldSkipIfInlineDataIsNullOrWhitespace(string? inlineData) =>
        await Assert.That(GetContext()
            .Execute(new MaintenanceVerb
                {
                    InlineData = inlineData!
                }
            )).IsTrue();

    [Test]
    public async Task ShouldExecuteBulkDeleteJob()
    {
        // Arrange
        var context = GetContext();
        var inlineData = await File.ReadAllTextAsync(GetResourcePath("fetch.xml"));
        await Assert.That(context.Execute(new MaintenanceVerb
            {
                InlineData = inlineData
            }
        )).IsTrue();

        // Assert
        var asyncOperation = context.GetSingle<AsyncOperation>();
        await Assert.That(asyncOperation.OperationType.Value).IsEqualTo(AsyncOperation.Options.OperationType.BulkDelete);
        await Assert.That(asyncOperation.StatusCode.Value).IsEqualTo(AsyncOperation.Options.StatusCode.Succeeded);
    }

    [Test]
    public async Task ShouldFailOnInvalidFetchXml()
    {
        // Arrange
        await Assert.That(GetContext()
            .Execute(new MaintenanceVerb
                {
                    InlineData = "<non-fetch/>"
                }
            )).IsFalse();
    }

    [Test]
    public async Task ShouldFailOnIfBulkDeleteJobExecutionFails()
    {
        // Arrange
        _bulkDeleteExecutor.ExpectedStatusCode = AsyncOperation.Options.StatusCode.Failed;
        var context = GetContext();
        var inlineData = await File.ReadAllTextAsync(GetResourcePath("fetch.xml"));
        await Assert.That(context.Execute(new MaintenanceVerb
            {
                InlineData = inlineData
            }
        )).IsFalse();
        var asyncOperation = context.GetSingle<AsyncOperation>();
        await Assert.That(asyncOperation.OperationType.Value).IsEqualTo(AsyncOperation.Options.OperationType.BulkDelete);
        await Assert.That(asyncOperation.StatusCode.Value).IsEqualTo(AsyncOperation.Options.StatusCode.Failed);
    }
}
