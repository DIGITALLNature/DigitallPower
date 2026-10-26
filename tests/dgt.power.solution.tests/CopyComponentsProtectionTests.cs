// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common;
using dgt.power.dataverse;
using dgt.power.solution.Base;
using dgt.power.tests;
using dgt.power.tests.Extensions;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using Spectre.Console.Cli;
using Spectre.Console.Testing;
using System.ServiceModel;

namespace dgt.power.solution.tests;

public class CopyComponentsProtectionTests
{
    [Test]
    [Arguments(false)]
    [Arguments(true)]
    public async Task Command_RawModes_NoProtectionReadsOrWrites(bool dryRun)
    {
        using var fixture = new Fixture { FailMetadata = true };
        await Assert.That(await fixture.RunAsync(raw: true, dryRun: dryRun)).IsZero();
        await Assert.That(fixture.Requests.OfType<RetrieveEntityRequest>()).IsEmpty();
        await Assert.That(fixture.Requests.OfType<RetrieveMetadataChangesRequest>()).IsEmpty();
        await Assert.That(fixture.Requests.OfType<UpdateAttributeRequest>()).IsEmpty();
        await Assert.That(fixture.Requests.OfType<AddSolutionComponentRequest>().Count()).IsEqualTo(dryRun ? 0 : 1);
    }

    [Test]
    public async Task Command_DryRun_UsesSameProtectionPreflightWithoutAnyMutation()
    {
        using var fixture = new Fixture();
        await Assert.That(await fixture.RunAsync(dryRun: true)).IsZero();
        await Assert.That(fixture.Requests.OfType<RetrieveEntityRequest>().Count()).IsEqualTo(2);
        await Assert.That(fixture.Requests.Any(r => r is AddSolutionComponentRequest or RemoveSolutionComponentRequest or UpdateAttributeRequest or PublishXmlRequest))
            .IsFalse();
        await Assert.That(fixture.Console.Output).Contains("Set IsCustomizable=false");
        await Assert.That(fixture.Console.Output).Contains("Labels/translations");
        await Assert.That(fixture.Console.Output).Contains("probe.calculated");
    }

    [Test]
    [Arguments(false)]
    [Arguments(true)]
    public async Task Command_LaterInvalidCandidate_FailsBeforeAllMutations(bool dryRun)
    {
        using var fixture = new Fixture { InvalidLaterField = true };
        await Assert.That(async () => await fixture.RunAsync(dryRun: dryRun)).ThrowsExactly<InvalidOperationException>();
        await Assert.That(fixture.Requests.Any(r => r is AddSolutionComponentRequest or RemoveSolutionComponentRequest or UpdateAttributeRequest))
            .IsFalse();
    }

    [Test]
    public async Task Command_MetadataFault_FailsBeforeCopy()
    {
        using var fixture = new Fixture { FailMetadata = true };
        await Assert.That(async () => await fixture.RunAsync()).ThrowsExactly<FaultException<OrganizationServiceFault>>();
        await Assert.That(fixture.Requests.OfType<AddSolutionComponentRequest>()).IsEmpty();
    }

    [Test]
    public async Task Command_ProtectionRunsOnlyAfterCopyAndAppCleanup()
    {
        using var fixture = new Fixture { IncludeApp = true };
        await Assert.That(await fixture.RunAsync(apps: AppHandling.Strip)).IsZero();
        var names = fixture.Requests.Select(r => r.RequestName).ToList();
        await Assert.That(names.LastIndexOf("RemoveSolutionComponent") < names.IndexOf("UpdateAttribute")).IsTrue();
        await Assert.That(names.LastIndexOf("AddSolutionComponent") < names.IndexOf("UpdateAttribute")).IsTrue();
        await Assert.That(fixture.Requests.OfType<UpdateAttributeRequest>().Count()).IsEqualTo(1);
        await Assert.That(fixture.Requests.OfType<PublishXmlRequest>()).IsEmpty();
    }

    [Test]
    [Arguments("copy")]
    [Arguments("cleanup")]
    public async Task Command_CopyOrCleanupFault_PreventsAllProtection(string phase)
    {
        using var fixture = new Fixture { IncludeApp = true, FailurePhase = phase };
        await Assert.That(async () => await fixture.RunAsync(apps: AppHandling.Strip)).ThrowsExactly<FaultException<OrganizationServiceFault>>();
        await Assert.That(fixture.Requests.OfType<UpdateAttributeRequest>()).IsEmpty();
        await Assert.That(fixture.Console.Output).Contains("No rollback");
        await Assert.That(fixture.Console.Output).Contains(phase == "copy" ? "0 confirmed copied" : "2 confirmed copied");
        await Assert.That(fixture.Console.Output).Contains("failed or unconfirmed");
    }

    [Test]
    [Arguments(AppHandling.Skip)]
    [Arguments(AppHandling.Allow)]
    [Arguments(AppHandling.Strip)]
    public async Task Command_AppModes_ProtectOnlyIntentionallySelectedTable(AppHandling apps)
    {
        using var fixture = new Fixture { IncludeApp = true };
        await Assert.That(await fixture.RunAsync(apps: apps)).IsZero();
        var updates = fixture.Requests.OfType<UpdateAttributeRequest>().ToArray();
        await Assert.That(updates.Length).IsEqualTo(1);
        await Assert.That(updates[0].Attribute.MetadataId).IsEqualTo(fixture.FieldId);
    }

    [Test]
    public async Task Command_CancellationAfterCopy_StopsProtectionAndReportsCompletedEffects()
    {
        using var fixture = new Fixture { CancelAfterCopy = true };
        await Assert.That(async () => await fixture.RunAsync()).Throws<OperationCanceledException>();
        await Assert.That(fixture.Requests.OfType<UpdateAttributeRequest>()).IsEmpty();
        await Assert.That(fixture.Console.Output).Contains("Cancelled");
        await Assert.That(fixture.Console.Output).Contains("1 confirmed copied");
    }

    private sealed class Fixture : IDisposable
    {
        private readonly Guid _sourceId = Guid.NewGuid();
        private readonly Guid _targetId = Guid.NewGuid();
        private readonly Guid _tableId = Guid.NewGuid();
        private readonly Guid _appId = Guid.NewGuid();
        private readonly Guid _automaticAttributeId = Guid.NewGuid();
        private readonly CancellationTokenSource _cancellation = new();
        private int _membershipReads;
        public Guid FieldId { get; } = Guid.NewGuid();
        public List<OrganizationRequest> Requests { get; } = [];
        public TestConsole Console { get; } = new();
        public bool FailMetadata { get; init; }
        public bool InvalidLaterField { get; init; }
        public bool IncludeApp { get; init; }
        public bool CancelAfterCopy { get; init; }
        public string? FailurePhase { get; init; }

        public async Task<int> RunAsync(bool raw = false, bool dryRun = false, AppHandling apps = AppHandling.Skip)
        {
            Console.Profile.Width = 240;
            var service = Mock.Of<IOrganizationServiceAsync2>();
            service.RetrieveMultipleAsync(Any<QueryBase>(), Any<CancellationToken>()).Returns((query, _) => Retrieve((QueryExpression)query));
            service.ExecuteAsync(Any<OrganizationRequest>(), Any<CancellationToken>()).Returns((request, _) => Execute(request));
            var connection = Mock.Of<IDataverseConnection>();
            connection.GetServiceAsync().Returns(service.Object);
            var command = new CopyComponentsCommand(new TestSpanFactory(), connection.Object, Console);
            return await command.ExecuteAsync(
                new CommandContext([], Mock.Of<IRemainingArguments>().Object, "test", null),
                new CopyComponentsSettings { Target = "target", Source = "source", Raw = raw, DryRun = dryRun, Apps = apps },
                _cancellation.Token);
        }

        private EntityCollection Retrieve(QueryExpression query)
        {
            if (query.EntityName == "solution")
            {
                var target = query.Criteria.Conditions[0].Values.Contains("target");
                return new EntityCollection([new Solution(target ? _targetId : _sourceId)
                {
                    UniqueName = target ? "target" : "source", ["ismanaged"] = false
                }]);
            }
            if (query.EntityName == "solutioncomponentdefinition")
            {
                return new EntityCollection([Definition(1, "Entity"), Definition(2, "Attribute"), Definition(80, "AppModule")]);
            }
            if (query.EntityName == "solutioncomponent")
            {
                if (query.Criteria.Conditions[0].Values.Contains(_sourceId))
                {
                    var rows = new List<Entity> { Component(1, _tableId) };
                    if (IncludeApp) rows.Add(Component(80, _appId));
                    return new EntityCollection(rows);
                }
                _membershipReads++;
                return new EntityCollection(_membershipReads == 1
                    ? []
                    : new List<Entity> { Component(2, _automaticAttributeId) });
            }
            throw new InvalidOperationException($"Unexpected query: {query.EntityName}");
        }

        private OrganizationResponse Execute(OrganizationRequest request)
        {
            Requests.Add(request);
            switch (request)
            {
                case RetrieveEntityRequest:
                    if (FailMetadata) throw Fault("metadata denied");
                    return new RetrieveEntityResponse { Results = { ["EntityMetadata"] = Metadata() } };
                case AddSolutionComponentRequest:
                    if (FailurePhase == "copy") throw Fault("copy denied");
                    if (CancelAfterCopy) _cancellation.Cancel();
                    return new AddSolutionComponentResponse();
                case RemoveSolutionComponentRequest:
                    if (FailurePhase == "cleanup") throw Fault("cleanup denied");
                    return new RemoveSolutionComponentResponse();
                case UpdateAttributeRequest:
                    return new UpdateAttributeResponse();
                case RetrieveAttributeRequest:
                    return new RetrieveAttributeResponse
                    {
                        Results =
                        {
                            ["AttributeMetadata"] = new AttributeMetadata
                            {
                                MetadataId = FieldId, LogicalName = "calculated",
                                IsCustomizable = new BooleanManagedProperty(false)
                            }
                        }
                    };
                default:
                    throw new InvalidOperationException($"Unexpected request: {request.RequestName}");
            }
        }

        private EntityMetadata Metadata()
        {
            var field = new StringAttributeMetadata
            {
                LogicalName = "calculated", MetadataId = FieldId, SourceType = 1,
                IsCustomizable = new BooleanManagedProperty(true) { CanBeChanged = true }
            };
            field.SetSealedPropertyValue(nameof(AttributeMetadata.IsManaged), false);
            var fields = new List<AttributeMetadata> { field };
            if (InvalidLaterField)
            {
                var invalid = new StringAttributeMetadata
                {
                    LogicalName = "z_invalid", MetadataId = Guid.NewGuid(), SourceType = 1,
                    IsCustomizable = new BooleanManagedProperty(true) { CanBeChanged = false }
                };
                invalid.SetSealedPropertyValue(nameof(AttributeMetadata.IsManaged), false);
                fields.Add(invalid);
            }
            var entity = new EntityMetadata { LogicalName = "probe", MetadataId = _tableId };
            entity.SetSealedPropertyValue(nameof(EntityMetadata.IsManaged), false);
            entity.SetAttributeCollection(fields);
            return entity;
        }

        private static Entity Definition(int type, string name) => new("solutioncomponentdefinition", Guid.NewGuid())
        {
            ["solutioncomponenttype"] = type, ["name"] = name
        };

        private Entity Component(int type, Guid id) => new("solutioncomponent", Guid.NewGuid())
        {
            ["componenttype"] = new OptionSetValue(type), ["objectid"] = id,
            ["solutionid"] = new EntityReference("solution", _sourceId), ["rootcomponentbehavior"] = new OptionSetValue(0)
        };

        private static FaultException<OrganizationServiceFault> Fault(string message) => new(new OrganizationServiceFault { Message = message });

        public void Dispose()
        {
            _cancellation.Dispose();
            Console.Dispose();
        }
    }
}
