using FakeXrmEasy.Abstractions;
using FakeXrmEasy.Abstractions.Metadata;
using Microsoft.Xrm.Sdk.Metadata;

namespace dgt.power.tests.Extensions;

public static class FakeXrmEasyExtensions
{
    /// <summary>
    /// Initializes relationship metadata for a given collection of metadata.
    /// </summary>
    /// <param name="context">The xrm faked context.</param>
    /// <param name="metadata">The collection of metadata.</param>
    public static void InitializeRelationshipMetadata(this IXrmFakedContext context,
        ICollection<EntityMetadata> metadata)
    {
        metadata
            .Where(x => x.ManyToManyRelationships != null)
            .SelectMany(x => x.ManyToManyRelationships)
            .Select(x => (RelationshipMetadataBase) x)
            .Concat(metadata
                .Where(x => x.OneToManyRelationships != null)
                .SelectMany(x => x.OneToManyRelationships)
                .Select(x => x as RelationshipMetadataBase)
            ).Concat(metadata
                .Where(x => x.ManyToOneRelationships != null)
                .SelectMany(x => x.ManyToOneRelationships)
                .Select(x => x as RelationshipMetadataBase)
            ).ToList()
            .ForEach(relationshipMetadata =>
            {
                if (context.GetRelationship(relationshipMetadata.SchemaName) == null)
                {
                    context.AddRelationship(relationshipMetadata.SchemaName,
                        relationshipMetadata.ToXrmFakedRelationship());
                }
            });
    }

    /// <summary>
    /// Initializes global optionset metadata for a given collection of metiadata.
    /// </summary>
    /// <param name="context">The faked xrm context.</param>
    /// <param name="metadata">The metadata to initialise</param>
    public static void InitializeGlobalOptionsetMetadata(this IXrmFakedContext context,
        IEnumerable<EntityMetadata> metadata) => metadata
        .Where(x => x.Attributes != null)
        .SelectMany(x => x.Attributes)
        .Where(x => x.AttributeType == AttributeTypeCode.Picklist)
        .Select(x => (PicklistAttributeMetadata) x)
        .Where(x => x.OptionSet.IsGlobal == true)
        .Select(x => x.OptionSet)
        .ToList()
        .ForEach(optionSetMetadata =>
            {
                var optionSetRepository = context.GetProperty<IOptionSetMetadataRepository>();
                if (optionSetRepository.GetByName(optionSetMetadata.Name) == null)
                {
                    optionSetRepository.Set(optionSetMetadata.Name, optionSetMetadata);
                }
            }
        );

    /// <summary>
    /// Creates a XrmFakedRelationship out of a RelationshipMetadataBase.
    /// </summary>
    /// <param name="relationshipMetadata"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static XrmFakedRelationship ToXrmFakedRelationship(this RelationshipMetadataBase relationshipMetadata)
    {
        switch (relationshipMetadata)
        {
            case OneToManyRelationshipMetadata oneToManyRelationshipMetadata:
                return new XrmFakedRelationship
                {
                    // TODO: FakeXrmEasy sets the relationship name through the IntersectEntity property in the RetrieveRelationshipRequestExecutor therefore setting the property to schema name here.
                    // see https://github.com/DynamicsValue/fake-xrm-easy-messages/blob/2x-dev/src/FakeXrmEasy.Messages/FakeMessageExecutors/RetrieveRelationshipRequestExecutor.cs
                    IntersectEntity = relationshipMetadata.SchemaName,
                    RelationshipType = XrmFakedRelationship.FakeRelationshipType.OneToMany,
                    Entity1LogicalName = oneToManyRelationshipMetadata.ReferencingEntity,
                    Entity1Attribute = oneToManyRelationshipMetadata.ReferencingAttribute,
                    Entity2LogicalName = oneToManyRelationshipMetadata.ReferencedEntity,
                    Entity2Attribute = oneToManyRelationshipMetadata.ReferencedAttribute
                };
            case ManyToManyRelationshipMetadata manyToManyRelationshipMetadata:
                return new XrmFakedRelationship
                {
                    RelationshipType = XrmFakedRelationship.FakeRelationshipType.ManyToMany,
                    IntersectEntity = manyToManyRelationshipMetadata.IntersectEntityName,
                    Entity1LogicalName = manyToManyRelationshipMetadata.Entity1LogicalName,
                    Entity1Attribute = manyToManyRelationshipMetadata.Entity1IntersectAttribute,
                    Entity2LogicalName = manyToManyRelationshipMetadata.Entity2LogicalName,
                    Entity2Attribute = manyToManyRelationshipMetadata.Entity2IntersectAttribute
                };
            default:
                throw new ArgumentException($"Unknown relationship type '{nameof(relationshipMetadata)}'",
                    nameof(relationshipMetadata));
        }
    }
}
