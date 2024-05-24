// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using dgt.power.maintenance.Base;
using dgt.power.maintenance.Logic;
using dgt.power.maintenance.tests.Base;
using dgt.power.tests;
using dgt.power.tests.FakeExecutor;
using FakeXrmEasy.FakeMessageExecutors;
using Microsoft.Crm.Sdk.Messages;
#pragma warning disable CS8602

namespace dgt.power.maintenance.tests;

public class BulkDeleteUtilTests : MaintenanceTestsBase<BulkDeleteUtil>
{
    private readonly BulkDeleteExecutor _bulkDeleteExecutor;
    public BulkDeleteUtilTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
        _bulkDeleteExecutor = new BulkDeleteExecutor();
    }

    protected override CommandTestContext<BulkDeleteUtil, MaintenanceVerb> GetContext() =>
        GetBuilder()
            .WithFakeMessageExecutor<FetchXmlToQueryExpressionRequest>(new FetchXmlToQueryExpressionRequestExecutor())
            .WithFakeMessageExecutor<BulkDeleteRequest>(_bulkDeleteExecutor)
            .Build();

    [Theory]
    [InlineData("")]
    [InlineData("       ")]
    [InlineData(null)]
    public void ShouldSkipIfInlineDataIsNullOrWhitespace(string? inlineData) =>
        GetContext()
            .Execute(new MaintenanceVerb
                {
                    InlineData = inlineData!
                }
            ).Should().BeTrue();

    [Fact]
    public void ShouldExecuteBulkDeleteJob()
    {
        // Arrange
        var context = GetContext();
        context.Execute(new MaintenanceVerb
            {
                InlineData = File.ReadAllText(GetResourcePath("fetch.xml"))
            }
        ).Should().BeTrue();

        // Assert
        var asyncOperation = context.GetSingle<AsyncOperation>();
        asyncOperation.OperationType.Value.Should().Be(AsyncOperation.Options.OperationType.BulkDelete);
        asyncOperation.StatusCode.Value.Should().Be(AsyncOperation.Options.StatusCode.Succeeded);
    }

    [Fact]
    public void ShouldFailOnInvalidFetchXml()
    {
        // Arrange
        GetContext()
            .Execute(new MaintenanceVerb
                {
                    InlineData = "<non-fetch/>"
                }
            ).Should().BeFalse();
    }

    [Fact]
    public void ShouldFailOnIfBulkDeleteJobExecutionFails()
    {
        // Arrange
        _bulkDeleteExecutor.ExpectedStatusCode = AsyncOperation.Options.StatusCode.Failed;
        var context = GetContext();
        context.Execute(new MaintenanceVerb
            {
                InlineData = File.ReadAllText(GetResourcePath("fetch.xml"))
            }
        ).Should().BeFalse();
        var asyncOperation = context.GetSingle<AsyncOperation>();
        asyncOperation.OperationType.Value.Should().Be(AsyncOperation.Options.OperationType.BulkDelete);
        asyncOperation.StatusCode.Value.Should().Be(AsyncOperation.Options.StatusCode.Failed);
    }
}
