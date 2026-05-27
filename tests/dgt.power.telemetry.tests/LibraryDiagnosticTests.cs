// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using Digitall.Dataverse.Testing;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;

namespace dgt.power.telemetry.tests;

/// <summary>
/// Diagnostic tests to verify Digitall.Dataverse.Testing library behavior.
/// These tests help identify library bugs vs test setup issues.
/// </summary>
public class LibraryDiagnosticTests
{
    [Test]
    public async Task Create_ShouldPreserveAttributesOnRetrieve()
    {
        var service = new FakeOrganizationServiceAsync();
        service.AddDefaultRequests();
        var meta = new EntityMetadata();
        meta.GetType().GetProperty("LogicalName")!.SetValue(meta, "queue");
        service.AddMetadata(meta);

        var queueId = Guid.NewGuid();
        var queue = new Entity("queue", queueId)
        {
            ["name"] = "Test Queue",
            ["description"] = "Test Description"
        };

        var createdId = service.Create(queue);
        var retrieved = service.Retrieve("queue", createdId, new ColumnSet(true));

        await Assert.That(retrieved.GetAttributeValue<string>("name")).IsEqualTo("Test Queue");
        await Assert.That(retrieved.GetAttributeValue<string>("description")).IsEqualTo("Test Description");
    }

    [Test]
    public async Task Create_ShouldUseProvidedEntityId()
    {
        var service = new FakeOrganizationServiceAsync();
        service.AddDefaultRequests();
        var meta = new EntityMetadata();
        meta.GetType().GetProperty("LogicalName")!.SetValue(meta, "queue");
        service.AddMetadata(meta);

        var queueId = Guid.NewGuid();
        var queue = new Entity("queue", queueId) { ["name"] = "Test" };

        var createdId = service.Create(queue);

        await Assert.That(createdId).IsEqualTo(queueId);
    }

    [Test]
    public async Task Create_ShouldPreserveAttributesWithEarlyBoundType()
    {
        var service = new FakeOrganizationServiceAsync();
        service.AddDefaultRequests();
        var meta = new EntityMetadata();
        meta.GetType().GetProperty("LogicalName")!.SetValue(meta, "queue");
        service.AddMetadata(meta);

        var queueId = Guid.NewGuid();
        var queue = new Queue(queueId)
        {
            Name = "Early Bound Queue",
            Description = "EB Description"
        };

        var createdId = service.Create(queue);
        var retrieved = service.Retrieve("queue", createdId, new ColumnSet(true));
        var typedRetrieved = retrieved.ToEntity<Queue>();

        await Assert.That(typedRetrieved.Name).IsEqualTo("Early Bound Queue");
        await Assert.That(typedRetrieved.Description).IsEqualTo("EB Description");
    }

    [Test]
    public async Task Create_RelatedEntities_ShouldNotThrowKeyNotFound()
    {
        var service = new FakeOrganizationServiceAsync();
        service.AddDefaultRequests();

        var calendarMeta = new EntityMetadata();
        calendarMeta.GetType().GetProperty("LogicalName")!.SetValue(calendarMeta, "calendar");
        service.AddMetadata(calendarMeta);

        var calendarRuleMeta = new EntityMetadata();
        calendarRuleMeta.GetType().GetProperty("LogicalName")!.SetValue(calendarRuleMeta, "calendarrule");
        service.AddMetadata(calendarRuleMeta);

        // Register relationship
        var relationship = new OneToManyRelationshipMetadata
        {
            SchemaName = "calendar_calendar_rules",
            ReferencedEntity = "calendar",
            ReferencedAttribute = "calendarid",
            ReferencingEntity = "calendarrule",
            ReferencingAttribute = "calendarid"
        };
        service.AddRelationships(relationship);

        var calendarId = Guid.NewGuid();
        var calendar = new Entity("calendar", calendarId) { ["name"] = "Business Hours" };

        // Create calendar with related entities
        var calendarRule = new Entity("calendarrule") { ["name"] = "Rule 1" };
        calendar.RelatedEntities.Add(
            new Relationship("calendar_calendar_rules"),
            new EntityCollection(new[] { calendarRule }));

        // This should not throw
        var createdId = service.Create(calendar);
        await Assert.That(createdId).IsEqualTo(calendarId);
    }
}
