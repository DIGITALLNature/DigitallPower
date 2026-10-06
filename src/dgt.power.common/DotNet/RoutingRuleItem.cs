using System.Diagnostics.CodeAnalysis;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System.Runtime.Serialization;

// ReSharper disable All
namespace dgt.power.dataverse
{
    /// <inheritdoc cref="Microsoft.Xrm.Sdk.Entity" />
    /// <summary>
	/// Please provide the description for entity
	/// </summary>
    [EntityLogicalName("routingruleitem")]
    [System.CodeDom.Compiler.GeneratedCode("dgtp", "2026")]
    [ExcludeFromCodeCoverage]
    [SuppressMessage("Design", "CA1034")]
    [SuppressMessage("Performance", "CA1815")]
    public partial class RoutingRuleItem : Entity
    {
        #region ctor
        public RoutingRuleItem() : base(EntityLogicalName) { }

        public RoutingRuleItem(Guid id) : base(EntityLogicalName, id) { }

        public RoutingRuleItem(KeyAttributeCollection keyAttributes) : base(EntityLogicalName, keyAttributes) { }

        public RoutingRuleItem(string keyName, object keyValue) : base(EntityLogicalName, keyName, keyValue) { }
        #endregion

        #region consts
        public const string EntityLogicalName = "routingruleitem";
        public const string PrimaryNameAttribute = "name";
        public const int EntityTypeCode = 8199;
        #endregion

        #region Attributes
        [AttributeLogicalName("routingruleitemid")]
        [IgnoreDataMember]
        public new Guid Id
        {
            get
            {
                return base.Id;
            }
            set
            {
                RoutingRuleItemId = value;
            }
        }

        /// <summary>
		/// Unique identifier for entity instances
		/// </summary>
        [AttributeLogicalName("routingruleitemid")]
        public Guid? RoutingRuleItemId
        {
            get
            {
                return GetAttributeValue<Guid?>("routingruleitemid");
            }
            set
            {
                SetAttributeValue("routingruleitemid", value);
                base.Id = value.HasValue ? value.Value : Guid.Empty;
            }
        }

        /// <summary>
		/// Show who is assigned on item.
		/// </summary>
        [AttributeLogicalName("assignobjectid")]
        public EntityReference? AssignObjectId
        {
            get
            {
                return GetAttributeValue<EntityReference?>("assignobjectid");
            }
            set
            {
                SetAttributeValue("assignobjectid", value);
            }
        }

        /// <summary>
		/// Shows the date and time when the item was last assigned to a user.
		/// </summary>
        [AttributeLogicalName("assignobjectidmodifiedon")]
        public DateTime? AssignObjectIdModifiedOn
        {
            get
            {
                return GetAttributeValue<DateTime?>("assignobjectidmodifiedon");
            }
            set
            {
                SetAttributeValue("assignobjectidmodifiedon", value);
            }
        }

        /// <summary>
		/// For internal use only.
		/// </summary>
        [AttributeLogicalName("componentstate")]
        public OptionSetValue? ComponentState
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("componentstate");
            }
        }

        /// <summary>
		/// Condition for Rule item
		/// </summary>
        [AttributeLogicalName("conditionxml")]
        public string? ConditionXml
        {
            get
            {
                return GetAttributeValue<string?>("conditionxml");
            }
            set
            {
                SetAttributeValue("conditionxml", value);
            }
        }

        /// <summary>
		/// Unique identifier of the user who created the record.
		/// </summary>
        [AttributeLogicalName("createdby")]
        public EntityReference? CreatedBy
        {
            get
            {
                return GetAttributeValue<EntityReference?>("createdby");
            }
        }

        /// <summary>
		/// Date and time when the record was created.
		/// </summary>
        [AttributeLogicalName("createdon")]
        public DateTime? CreatedOn
        {
            get
            {
                return GetAttributeValue<DateTime?>("createdon");
            }
        }

        /// <summary>
		/// Unique identifier of the delegate user who created the record.
		/// </summary>
        [AttributeLogicalName("createdonbehalfby")]
        public EntityReference? CreatedOnBehalfBy
        {
            get
            {
                return GetAttributeValue<EntityReference?>("createdonbehalfby");
            }
        }

        /// <summary>
		/// Type additional information to describe the rule item.
		/// </summary>
        [AttributeLogicalName("description")]
        public string? Description
        {
            get
            {
                return GetAttributeValue<string?>("description");
            }
            set
            {
                SetAttributeValue("description", value);
            }
        }

        /// <summary>
		/// Exchange rate for the currency associated with the routing rule item with respect to the base currency.
		/// </summary>
        [AttributeLogicalName("exchangerate")]
        public decimal? ExchangeRate
        {
            get
            {
                return GetAttributeValue<decimal?>("exchangerate");
            }
        }

        /// <summary>
		/// For internal use only.
		/// </summary>
        [AttributeLogicalName("ismanaged")]
        public bool? IsManaged
        {
            get
            {
                return GetAttributeValue<bool?>("ismanaged");
            }
        }

        /// <summary>
		/// Unique identifier of the user who modified the record.
		/// </summary>
        [AttributeLogicalName("modifiedby")]
        public EntityReference? ModifiedBy
        {
            get
            {
                return GetAttributeValue<EntityReference?>("modifiedby");
            }
        }

        /// <summary>
		/// Date and time when the record was modified.
		/// </summary>
        [AttributeLogicalName("modifiedon")]
        public DateTime? ModifiedOn
        {
            get
            {
                return GetAttributeValue<DateTime?>("modifiedon");
            }
        }

        /// <summary>
		/// Unique identifier of the delegate user who modified the record.
		/// </summary>
        [AttributeLogicalName("modifiedonbehalfby")]
        public EntityReference? ModifiedOnBehalfBy
        {
            get
            {
                return GetAttributeValue<EntityReference?>("modifiedonbehalfby");
            }
        }

        /// <summary>
		/// Name of the Routing Rule Item.
		/// </summary>
        [AttributeLogicalName("name")]
        public string? Name
        {
            get
            {
                return GetAttributeValue<string?>("name");
            }
            set
            {
                SetAttributeValue("name", value);
            }
        }

        /// <summary>
		/// Unique identifier of the organization associated with the routing rule item.
		/// </summary>
        [AttributeLogicalName("organizationid")]
        public EntityReference? OrganizationId
        {
            get
            {
                return GetAttributeValue<EntityReference?>("organizationid");
            }
        }

        /// <summary>
		/// For internal use only.
		/// </summary>
        [AttributeLogicalName("overwritetime")]
        public DateTime? OverwriteTime
        {
            get
            {
                return GetAttributeValue<DateTime?>("overwritetime");
            }
        }

        /// <summary>
		/// Owner Id
		/// </summary>
        [AttributeLogicalName("ownerid")]
        public EntityReference? OwnerId
        {
            get
            {
                return GetAttributeValue<EntityReference?>("ownerid");
            }
            set
            {
                SetAttributeValue("ownerid", value);
            }
        }

        /// <summary>
		/// Unique identifier for the business unit that owns the record
		/// </summary>
        [AttributeLogicalName("owningbusinessunit")]
        public EntityReference? OwningBusinessUnit
        {
            get
            {
                return GetAttributeValue<EntityReference?>("owningbusinessunit");
            }
        }

        /// <summary>
		/// Unique identifier for the user that owns the record.
		/// </summary>
        [AttributeLogicalName("owninguser")]
        public EntityReference? OwningUser
        {
            get
            {
                return GetAttributeValue<EntityReference?>("owninguser");
            }
        }

        /// <summary>
		/// Choose the Queue that the item is assigned to.
		/// </summary>
        [AttributeLogicalName("routedqueueid")]
        public EntityReference? RoutedQueueId
        {
            get
            {
                return GetAttributeValue<EntityReference?>("routedqueueid");
            }
            set
            {
                SetAttributeValue("routedqueueid", value);
            }
        }

        /// <summary>
		/// Unique identifier for Routing Rule associated with Rule Item.
		/// </summary>
        [AttributeLogicalName("routingruleid")]
        public EntityReference? RoutingRuleId
        {
            get
            {
                return GetAttributeValue<EntityReference?>("routingruleid");
            }
            set
            {
                SetAttributeValue("routingruleid", value);
            }
        }

        /// <summary>
		/// For internal use only.
		/// </summary>
        [AttributeLogicalName("routingruleitemidunique")]
        public Guid? RoutingRuleItemIdUnique
        {
            get
            {
                return GetAttributeValue<Guid?>("routingruleitemidunique");
            }
        }

        /// <summary>
		/// Sequence number of the routing rule item
		/// </summary>
        [AttributeLogicalName("sequencenumber")]
        public int? SequenceNumber
        {
            get
            {
                return GetAttributeValue<int?>("sequencenumber");
            }
            set
            {
                SetAttributeValue("sequencenumber", value);
            }
        }

        /// <summary>
		/// Unique identifier of the associated solution.
		/// </summary>
        [AttributeLogicalName("solutionid")]
        public Guid? SolutionId
        {
            get
            {
                return GetAttributeValue<Guid?>("solutionid");
            }
        }

        /// <summary>
		/// For internal use only.
		/// </summary>
        [AttributeLogicalName("timezoneruleversionnumber")]
        public int? TimeZoneRuleVersionNumber
        {
            get
            {
                return GetAttributeValue<int?>("timezoneruleversionnumber");
            }
            set
            {
                SetAttributeValue("timezoneruleversionnumber", value);
            }
        }

        /// <summary>
		/// Unique identifier of the currency associated with the Routing Rule.
		/// </summary>
        [AttributeLogicalName("transactioncurrencyid")]
        public EntityReference? TransactionCurrencyId
        {
            get
            {
                return GetAttributeValue<EntityReference?>("transactioncurrencyid");
            }
            set
            {
                SetAttributeValue("transactioncurrencyid", value);
            }
        }

        /// <summary>
		/// Time zone code that was in use when the record was created.
		/// </summary>
        [AttributeLogicalName("utcconversiontimezonecode")]
        public int? UTCConversionTimeZoneCode
        {
            get
            {
                return GetAttributeValue<int?>("utcconversiontimezonecode");
            }
            set
            {
                SetAttributeValue("utcconversiontimezonecode", value);
            }
        }

        /// <summary>
		/// Version number of the Routing Rule Item.
		/// </summary>
        [AttributeLogicalName("versionnumber")]
        public long? VersionNumber
        {
            get
            {
                return GetAttributeValue<long?>("versionnumber");
            }
        }
        #endregion

        #region NavigationProperties

        /// <summary>
        /// 1:N routingruleitem_AsyncOperations
        /// </summary>
        [RelationshipSchemaName("routingruleitem_AsyncOperations")]
        public IEnumerable<AsyncOperation> RoutingruleitemAsyncOperations
        {
            get
            {
                return GetRelatedEntities<AsyncOperation>("routingruleitem_AsyncOperations", null);
            }
            set
            {
                SetRelatedEntities("routingruleitem_AsyncOperations", null, value);
            }
        }
        #endregion

        #region Options
        public static partial class Options
        {
            public struct ComponentState
            {
                public const int Published = 0;
                public const int Unpublished = 1;
                public const int Deleted = 2;
                public const int DeletedUnpublished = 3;
            }
            public struct IsManaged
            {
                public const bool Unmanaged = false;
                public const bool Managed = true;
            }
        }
        #endregion

        #region LogicalNames
        public static partial class LogicalNames
        {
            public const string RoutingRuleItemId = "routingruleitemid";
            public const string AssignObjectId = "assignobjectid";
            public const string AssignObjectIdModifiedOn = "assignobjectidmodifiedon";
            public const string ComponentState = "componentstate";
            public const string ConditionXml = "conditionxml";
            public const string CreatedBy = "createdby";
            public const string CreatedOn = "createdon";
            public const string CreatedOnBehalfBy = "createdonbehalfby";
            public const string Description = "description";
            public const string ExchangeRate = "exchangerate";
            public const string IsManaged = "ismanaged";
            public const string ModifiedBy = "modifiedby";
            public const string ModifiedOn = "modifiedon";
            public const string ModifiedOnBehalfBy = "modifiedonbehalfby";
            public const string Name = "name";
            public const string OrganizationId = "organizationid";
            public const string OverwriteTime = "overwritetime";
            public const string OwnerId = "ownerid";
            public const string OwningBusinessUnit = "owningbusinessunit";
            public const string OwningUser = "owninguser";
            public const string RoutedQueueId = "routedqueueid";
            public const string RoutingRuleId = "routingruleid";
            public const string RoutingRuleItemIdUnique = "routingruleitemidunique";
            public const string SequenceNumber = "sequencenumber";
            public const string SolutionId = "solutionid";
            public const string TimeZoneRuleVersionNumber = "timezoneruleversionnumber";
            public const string TransactionCurrencyId = "transactioncurrencyid";
            public const string UTCConversionTimeZoneCode = "utcconversiontimezonecode";
            public const string VersionNumber = "versionnumber";
        }
        #endregion

        #region Relations
        public static partial class Relations
        {
            public static class OneToMany
            {
                public const string RoutingruleitemAnnotation = "routingruleitem_Annotation";
                public const string RoutingruleitemAsyncOperations = "routingruleitem_AsyncOperations";
                public const string RoutingruleitemBulkDeleteFailures = "routingruleitem_BulkDeleteFailures";
                public const string RoutingruleitemProcessSessions = "routingruleitem_ProcessSessions";
                public const string RoutingruleitemUserentityinstancedatas = "routingruleitem_userentityinstancedatas";
            }

            public static partial class ManyToOne
            {
                public const string LkRoutingRuleItemCreatedby = "lk_RoutingRuleItem_createdby";
                public const string LkRoutingruleitemCreatedonbehalfby = "lk_routingruleitem_createdonbehalfby";
                public const string LkRoutingruleitemModifiedby = "lk_routingruleitem_modifiedby";
                public const string LkRoutingruleitemModifiedonbehalfby = "lk_routingruleitem_modifiedonbehalfby";
                public const string OrganizationRoutingruleitems = "organization_routingruleitems";
                public const string QueueRoutingruleitem = "queue_routingruleitem";
                public const string RoutingruleEntries = "routingrule_entries";
                public const string TeamRoutingruleitem = "team_routingruleitem";
                public const string TransactionCurrencyRoutingruleitem = "TransactionCurrency_routingruleitem";
                public const string UserRoutingruleitem = "user_routingruleitem";
            }

            public static partial class ManyToMany
            {
            }
        }
        #endregion

        #region Methods

        public EntityReference ToNamedEntityReference()
        {
            var reference = ToEntityReference();
            reference.Name = GetAttributeValue<string?>(PrimaryNameAttribute);
            return reference;
        }
        #endregion
    }

    #region Context
    public partial class DataContext
    {
        public IQueryable<RoutingRuleItem> RoutingRuleItemSet
        {
            get
            {
                return CreateQuery<RoutingRuleItem>();
            }
        }
    }
    #endregion
}
