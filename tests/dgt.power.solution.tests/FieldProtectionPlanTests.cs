// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.solution.Base;
using dgt.power.tests.Extensions;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Spectre.Console.Testing;
using System.ServiceModel;

namespace dgt.power.solution.tests;

public class FieldProtectionPlanTests
{
    [Test]
    public async Task Prepare_CompleteTableAndExplicitAttribute_DeduplicatesAndPreservesScope()
    {
        var fields = new[] { Field("calc", 1), Field("rollup", 2), Field("formula", 3) };
        var entity = Table(fields);
        var requests = new List<OrganizationRequest>();
        var service = Mock.Of<IOrganizationServiceAsync2>();
        service.ExecuteAsync(Any<OrganizationRequest>(), Any<CancellationToken>()).Returns((request, _) =>
        {
            requests.Add(request);
            return Response(request, entity);
        });
        var decisions = new[]
        {
            Decision(entity.MetadataId!.Value, 1), Decision(fields[0].MetadataId!.Value, 2),
            Decision(entity.MetadataId.Value, 1) with { Include = false }
        };
        var plan = await FieldProtectionPlan.PrepareAsync(service.Object, decisions, CancellationToken.None);
        using var console = new TestConsole();
        plan.Render(console);
        var progress = new CopyExecutionProgress();
        await plan.ExecuteAsync(service.Object, progress, console, CancellationToken.None);
        await Assert.That(progress.ProtectedFields.Count).IsEqualTo(3);
        await Assert.That(requests.Count(r => r is RetrieveMetadataChangesRequest)).IsZero();
        await Assert.That(string.Join(",", requests.Select(r => r.RequestName))).IsEqualTo(
            "RetrieveEntity,UpdateAttribute,RetrieveAttribute,UpdateAttribute,RetrieveAttribute,UpdateAttribute,RetrieveAttribute");
        await Assert.That(console.Output).Contains("Power Fx Formula");
        await Assert.That(console.Output).Contains("shared environment");
        foreach (var request in requests.OfType<UpdateAttributeRequest>())
        {
            await Assert.That(request.Attribute.IsCustomizable.Value).IsFalse();
            await Assert.That(request.MergeLabels).IsTrue();
            await Assert.That(request.Attribute.DisplayName).IsNull();
            await Assert.That(request.Attribute.GetType().GetProperty("FormulaDefinition")?.GetValue(request.Attribute)).IsNull();
        }
    }

    [Test]
    public async Task Prepare_StandaloneAttribute_ResolvesWithoutParentMembership()
    {
        var field = Field("standalone", 3);
        var entity = Table([field]);
        var calls = new List<OrganizationRequest>();
        var service = Mock.Of<IOrganizationServiceAsync2>();
        service.ExecuteAsync(Any<OrganizationRequest>(), Any<CancellationToken>()).Returns((request, _) =>
        {
            calls.Add(request);
            if (request is RetrieveMetadataChangesRequest changes)
            {
                var condition = changes.Query.AttributeQuery.Criteria.Conditions.Single();
                if (condition.PropertyName != "MetadataId" || !changes.Query.AttributeQuery.Properties.AllProperties)
                    throw new InvalidOperationException("Unbounded or partial attribute metadata request.");
                return new RetrieveMetadataChangesResponse
                {
                    Results = { ["EntityMetadata"] = new EntityMetadataCollection { entity } }
                };
            }
            return Response(request, entity);
        });
        var plan = await FieldProtectionPlan.PrepareAsync(service.Object,
            [Decision(field.MetadataId!.Value, 2), Decision(field.MetadataId.Value, 2)], CancellationToken.None);
        var query = calls.OfType<RetrieveMetadataChangesRequest>().Single().Query;
        await Assert.That(query.Properties.PropertyNames.Contains(nameof(EntityMetadata.LogicalName))).IsTrue();
        await Assert.That(query.Properties.PropertyNames.Contains(nameof(EntityMetadata.Attributes))).IsTrue();
        await Assert.That(query.AttributeQuery.Criteria.Conditions.Single().Value is Guid[] ids &&
            ids.SequenceEqual([field.MetadataId.Value])).IsTrue();
        using var console = new TestConsole();
        await plan.ExecuteAsync(service.Object, new CopyExecutionProgress(), console, CancellationToken.None);
        await Assert.That(calls.OfType<UpdateAttributeRequest>().Count()).IsEqualTo(1);
        await Assert.That(calls.OfType<RetrieveEntityRequest>().Count()).IsZero();
    }

    [Test]
    public async Task Prepare_ObservedTechnicalMetadata_ReportsExceptionsAndSkipsNonCalculations()
    {
        var rollupDate = new DateTimeAttributeMetadata { LogicalName = "rollup_date", MetadataId = Guid.NewGuid(), SourceType = 0 };
        var rollupState = new IntegerAttributeMetadata { LogicalName = "rollup_state", MetadataId = Guid.NewGuid(), SourceType = 0 };
        foreach (var companion in new AttributeMetadata[] { rollupDate, rollupState })
        {
            companion.SetSealedPropertyValue(nameof(AttributeMetadata.IsManaged), false);
            companion.SetSealedPropertyValue(nameof(AttributeMetadata.IsLogical), false);
        }
        var money = new MoneyAttributeMetadata { LogicalName = "amount_base", MetadataId = Guid.NewGuid(), SourceType = 0 };
        money.SetSealedPropertyValue(nameof(MoneyAttributeMetadata.IsBaseCurrency), true);
        money.SetSealedPropertyValue(nameof(AttributeMetadata.IsManaged), false);
        var managed = Field("managed_with_active_layer", 1);
        managed.SetSealedPropertyValue(nameof(AttributeMetadata.IsManaged), true);
        var entity = Table([rollupDate, rollupState, money, Field("bpf_duration", 1), Field("prompt", 4),
            Field("simple", 0), Field("known_null", null), managed]);
        var service = Mock.Of<IOrganizationServiceAsync2>();
        service.ExecuteAsync(Any<OrganizationRequest>(), Any<CancellationToken>()).Returns((request, _) => Response(request, entity));
        var plan = await FieldProtectionPlan.PrepareAsync(service.Object, [Decision(entity.MetadataId!.Value, 1)], CancellationToken.None);
        using var console = new TestConsole();
        plan.Render(console);
        var progress = new CopyExecutionProgress();
        await plan.ExecuteAsync(service.Object, progress, console, CancellationToken.None);
        await Assert.That(progress.ProtectedFields).IsEmpty();
        await Assert.That(console.Output).Contains("basis-currency companion");
        await Assert.That(console.Output).Contains("historic BPF duration");
        await Assert.That(console.Output).DoesNotContain("rollup_date");
        await Assert.That(console.Output).DoesNotContain("managed_with_active_layer");
    }

    [Test]
    public async Task Prepare_SkeletonAndExcludedApp_DoNotExpandOrReadMetadata()
    {
        var service = Mock.Of<IOrganizationServiceAsync2>();
        service.ExecuteAsync(Any<OrganizationRequest>(), Any<CancellationToken>()).Returns(() =>
            throw new InvalidOperationException("Out-of-scope metadata queried."));
        var plan = await FieldProtectionPlan.PrepareAsync(service.Object,
            [Decision(Guid.NewGuid(), 1) with { DoNotIncludeSubcomponents = true },
                Decision(Guid.NewGuid(), 2) with { Include = false },
                Decision(Guid.NewGuid(), 80)], CancellationToken.None);
        using var console = new TestConsole();
        await plan.ExecuteAsync(service.Object, new CopyExecutionProgress(), console, CancellationToken.None);
    }

    [Test]
    [Arguments("immutable")]
    [Arguments("unknown-managed")]
    [Arguments("missing-property")]
    [Arguments("missing-id")]
    [Arguments("unsupported-storage")]
    [Arguments("unknown-source")]
    public async Task Prepare_LaterInvalidField_FailsBeforeUpdates(string scenario)
    {
        var invalid = Field("z_invalid", 1);
        switch (scenario)
        {
            case "immutable":
                invalid.IsCustomizable = new BooleanManagedProperty(true) { CanBeChanged = false };
                break;
            case "unknown-managed":
                invalid.SetSealedPropertyValue(nameof(AttributeMetadata.IsManaged), null);
                break;
            case "missing-property":
                invalid.IsCustomizable = null;
                break;
            case "missing-id":
                invalid.MetadataId = null;
                break;
            case "unsupported-storage":
                invalid.SetSealedPropertyValue(nameof(AttributeMetadata.AttributeType), AttributeTypeCode.Lookup);
                break;
            case "unknown-source":
                invalid.SourceType = 999;
                break;
        }
        var entity = Table([Field("a_valid", 1), invalid]);
        var writes = 0;
        var service = Mock.Of<IOrganizationServiceAsync2>();
        service.ExecuteAsync(Any<OrganizationRequest>(), Any<CancellationToken>()).Returns((request, _) =>
        {
            if (request is UpdateAttributeRequest) writes++;
            return Response(request, entity);
        });
        await Assert.That(async () => await FieldProtectionPlan.PrepareAsync(service.Object,
            [Decision(entity.MetadataId!.Value, 1)], CancellationToken.None)).ThrowsExactly<InvalidOperationException>();
        await Assert.That(writes).IsZero();
    }

    [Test]
    public async Task Prepare_AlreadyProtectedImmutableField_RequiresNoUpdate()
    {
        var field = Field("protected", 1);
        field.IsCustomizable = new BooleanManagedProperty(false) { CanBeChanged = false };
        var entity = Table([field]);
        var service = Mock.Of<IOrganizationServiceAsync2>();
        service.ExecuteAsync(Any<OrganizationRequest>(), Any<CancellationToken>()).Returns((request, _) => Response(request, entity));
        var plan = await FieldProtectionPlan.PrepareAsync(service.Object, [Decision(entity.MetadataId!.Value, 1)], CancellationToken.None);
        using var console = new TestConsole();
        plan.Render(console);
        var progress = new CopyExecutionProgress();
        await plan.ExecuteAsync(service.Object, progress, console, CancellationToken.None);
        await Assert.That(progress.ProtectedFields).IsEmpty();
        await Assert.That(console.Output).Contains("already protected");
    }

    [Test]
    [Arguments("update-fault")]
    [Arguments("read-fault")]
    [Arguments("mismatch")]
    [Arguments("cancel")]
    public async Task Execute_SecondFieldFails_StopsAndKeepsConfirmedAndUnconfirmedEffects(string scenario)
    {
        var fields = new[] { Field("a", 1), Field("b", 2), Field("c", 3) };
        var entity = Table(fields);
        var updates = 0;
        var reads = 0;
        using var cancellation = new CancellationTokenSource();
        var service = Mock.Of<IOrganizationServiceAsync2>();
        service.ExecuteAsync(Any<OrganizationRequest>(), Any<CancellationToken>()).Returns((request, _) =>
        {
            if (request is UpdateAttributeRequest)
            {
                updates++;
                if (updates == 2 && scenario == "update-fault")
                    throw new FaultException<OrganizationServiceFault>(new OrganizationServiceFault { Message = "update denied" });
                if (updates == 2 && scenario == "cancel")
                    cancellation.Cancel();
            }
            if (request is RetrieveAttributeRequest)
            {
                reads++;
                if (reads == 2 && scenario == "read-fault")
                    throw new FaultException<OrganizationServiceFault>(new OrganizationServiceFault { Message = "read denied" });
                if (reads == 2 && scenario == "mismatch")
                    return new RetrieveAttributeResponse { Results = { ["AttributeMetadata"] = fields[1] } };
            }
            return Response(request, entity);
        });
        var plan = await FieldProtectionPlan.PrepareAsync(service.Object, [Decision(entity.MetadataId!.Value, 1)], cancellation.Token);
        using var console = new TestConsole();
        var progress = new CopyExecutionProgress { CopiedComponents = 3, CopyCompleted = true };
        console.Profile.Width = 240;
        if (scenario == "cancel")
        {
            await Assert.That(async () => await plan.ExecuteAsync(service.Object, progress, console, cancellation.Token))
                .Throws<OperationCanceledException>();
        }
        else if (scenario == "mismatch")
        {
            await Assert.That(async () => await plan.ExecuteAsync(service.Object, progress, console, cancellation.Token))
                .ThrowsExactly<InvalidOperationException>();
        }
        else
        {
            await Assert.That(async () => await plan.ExecuteAsync(service.Object, progress, console, cancellation.Token))
                .ThrowsExactly<FaultException<OrganizationServiceFault>>();
        }
        progress.ReportFailure(console, cancellation.IsCancellationRequested);
        await Assert.That(updates).IsEqualTo(2);
        await Assert.That(progress.ProtectedFields.Count).IsEqualTo(1);
        await Assert.That(progress.PendingEffect!).Contains("probe.b");
        await Assert.That(console.Output).Contains("3 confirmed copied");
        await Assert.That(console.Output).Contains("1 confirmed protected");
        await Assert.That(console.Output).Contains("No rollback");
        await Assert.That(console.Output).Contains("unconfirmed");
    }

    [Test]
    [Arguments(false)]
    [Arguments(true)]
    public async Task Prepare_StandaloneMetadataMissingOrFaulted_RejectsWithoutWrites(bool fault)
    {
        var requests = new List<OrganizationRequest>();
        var service = Mock.Of<IOrganizationServiceAsync2>();
        service.ExecuteAsync(Any<OrganizationRequest>(), Any<CancellationToken>()).Returns((request, _) =>
        {
            requests.Add(request);
            if (fault) throw new InvalidOperationException("Metadata unavailable.");
            return new RetrieveMetadataChangesResponse { Results = { ["EntityMetadata"] = new EntityMetadataCollection() } };
        });
        await Assert.That(async () => await FieldProtectionPlan.PrepareAsync(service.Object,
            [Decision(Guid.NewGuid(), 2)], CancellationToken.None)).ThrowsExactly<InvalidOperationException>();
        await Assert.That(requests.All(r => r is RetrieveMetadataChangesRequest)).IsTrue();
    }

    [Test]
    public async Task Prepare_MoneyCalculationWithoutBaseCurrencyFlag_Rejects()
    {
        var money = new MoneyAttributeMetadata
        {
            LogicalName = "money", MetadataId = Guid.NewGuid(), SourceType = 1,
            IsCustomizable = new BooleanManagedProperty(true) { CanBeChanged = true }
        };
        money.SetSealedPropertyValue(nameof(AttributeMetadata.IsManaged), false);
        var entity = Table([money]);
        var service = Mock.Of<IOrganizationServiceAsync2>();
        service.ExecuteAsync(Any<OrganizationRequest>(), Any<CancellationToken>()).Returns((request, _) => Response(request, entity));
        await Assert.That(async () => await FieldProtectionPlan.PrepareAsync(service.Object,
            [Decision(entity.MetadataId!.Value, 1)], CancellationToken.None)).ThrowsExactly<InvalidOperationException>();
    }

    [Test]
    [Arguments(AttributeTypeCode.String)]
    [Arguments(AttributeTypeCode.Integer)]
    [Arguments(AttributeTypeCode.Decimal)]
    [Arguments(AttributeTypeCode.Double)]
    [Arguments(AttributeTypeCode.Money)]
    [Arguments(AttributeTypeCode.DateTime)]
    [Arguments(AttributeTypeCode.Boolean)]
    public async Task CreateUpdate_SupportedStorage_ContainsOnlyIdentityAndManagedProperty(AttributeTypeCode type)
    {
        var id = Guid.NewGuid();
        var decision = new FieldProtectionDecision(id, "probe", "column", 1, type, true, "test");
        var update = decision.CreateUpdate();
        await Assert.That(update.AttributeType).IsEqualTo(type);
        await Assert.That(update.MetadataId).IsEqualTo(id);
        await Assert.That(update.LogicalName).IsEqualTo("column");
        await Assert.That(update.SchemaName).IsNull();
        await Assert.That(update.SourceType).IsNull();
        await Assert.That(update.RequiredLevel).IsNull();
        await Assert.That(update.IsCustomizable.Value).IsFalse();
        await Assert.That(update.DisplayName).IsNull();
    }

    private static OrganizationResponse Response(OrganizationRequest request, EntityMetadata entity) => request switch
    {
        RetrieveEntityRequest => new RetrieveEntityResponse { Results = { ["EntityMetadata"] = entity } },
        UpdateAttributeRequest => new UpdateAttributeResponse(),
        RetrieveAttributeRequest retrieve => ProtectedResponse(entity.Attributes.Single(a => a.MetadataId == retrieve.MetadataId)),
        _ => throw new InvalidOperationException($"Unexpected request: {request.RequestName}")
    };

    private static RetrieveAttributeResponse ProtectedResponse(AttributeMetadata original)
    {
        var result = new AttributeMetadata
        {
            MetadataId = original.MetadataId, LogicalName = original.LogicalName,
            IsCustomizable = new BooleanManagedProperty(false)
        };
        return new RetrieveAttributeResponse { Results = { ["AttributeMetadata"] = result } };
    }

    private static AttributeMetadata Field(string name, int? sourceType)
    {
        AttributeMetadata field = sourceType switch
        {
            2 => new IntegerAttributeMetadata(),
            3 => new DecimalAttributeMetadata(),
            _ => new StringAttributeMetadata()
        };
        field.LogicalName = name;
        field.MetadataId = Guid.NewGuid();
        field.SourceType = sourceType;
        field.IsCustomizable = new BooleanManagedProperty(true) { CanBeChanged = true };
        field.SetSealedPropertyValue(nameof(AttributeMetadata.IsManaged), false);
        return field;
    }

    private static EntityMetadata Table(AttributeMetadata[] fields)
    {
        var entity = new EntityMetadata { LogicalName = "probe", MetadataId = Guid.NewGuid() };
        entity.SetAttributeCollection(fields);
        return entity;
    }

    private static ComponentCopyDecision Decision(Guid id, int type) => new(type, id, "test", true, false, "test");
}
