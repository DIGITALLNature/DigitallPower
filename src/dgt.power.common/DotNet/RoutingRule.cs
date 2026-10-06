using System.Diagnostics.CodeAnalysis;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System.Runtime.Serialization;

// ReSharper disable All
namespace dgt.power.dataverse
{
    /// <inheritdoc cref="Microsoft.Xrm.Sdk.Entity" />
    /// <summary>
	/// Define Routing Rule to route cases to right people at the right time
	/// </summary>
    [EntityLogicalName("routingrule")]
    [System.CodeDom.Compiler.GeneratedCode("dgtp", "2026")]
    [ExcludeFromCodeCoverage]
    [SuppressMessage("Design", "CA1034")]
    [SuppressMessage("Performance", "CA1815")]
    public partial class RoutingRule : Entity
    {
        #region ctor
        public RoutingRule() : base(EntityLogicalName) { }

        public RoutingRule(Guid id) : base(EntityLogicalName, id) { }

        public RoutingRule(KeyAttributeCollection keyAttributes) : base(EntityLogicalName, keyAttributes) { }

        public RoutingRule(string keyName, object keyValue) : base(EntityLogicalName, keyName, keyValue) { }
        #endregion

        #region consts
        public const string EntityLogicalName = "routingrule";
        public const string PrimaryNameAttribute = "name";
        public const int EntityTypeCode = 8181;
        #endregion

        #region Attributes
        [AttributeLogicalName("routingruleid")]
        [IgnoreDataMember]
        public new Guid Id
        {
            get
            {
                return base.Id;
            }
            set
            {
                RoutingRuleId = value;
            }
        }

        /// <summary>
		/// Unique identifier for entity instances
		/// </summary>
        [AttributeLogicalName("routingruleid")]
        public Guid? RoutingRuleId
        {
            get
            {
                return GetAttributeValue<Guid?>("routingruleid");
            }
            set
            {
                SetAttributeValue("routingruleid", value);
                base.Id = value.HasValue ? value.Value : Guid.Empty;
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
		/// Type a short description about the objective of the routing rule.
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
		/// Exchange rate for the currency associated with the queue with respect to the base currency.
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
		/// Name of the Routing Rule.
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
		/// the organization associated with the Routing Rule
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
		/// For internal use only.
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
		/// For internal use only
		/// </summary>
        [AttributeLogicalName("owningbusinessunit")]
        public EntityReference? OwningBusinessUnit
        {
            get
            {
                return GetAttributeValue<EntityReference?>("owningbusinessunit");
            }
            set
            {
                SetAttributeValue("owningbusinessunit", value);
            }
        }

        /// <summary>
		/// Unique identifier for the team that owns the record.
		/// </summary>
        [AttributeLogicalName("owningteam")]
        public EntityReference? OwningTeam
        {
            get
            {
                return GetAttributeValue<EntityReference?>("owningteam");
            }
            set
            {
                SetAttributeValue("owningteam", value);
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
            set
            {
                SetAttributeValue("owninguser", value);
            }
        }

        /// <summary>
		/// For internal use only.
		/// </summary>
        [AttributeLogicalName("routingruleidunique")]
        public Guid? RoutingRuleIdUnique
        {
            get
            {
                return GetAttributeValue<Guid?>("routingruleidunique");
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
		/// Status of the Routing Rule
		/// </summary>
        [AttributeLogicalName("statecode")]
        public OptionSetValue? StateCode
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("statecode");
            }
            set
            {
                SetAttributeValue("statecode", value);
            }
        }

        /// <summary>
		/// Reason for the status of the Routing Rule
		/// </summary>
        [AttributeLogicalName("statuscode")]
        public OptionSetValue? StatusCode
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("statuscode");
            }
            set
            {
                SetAttributeValue("statuscode", value);
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
		/// Version number of the Routing Rule.
		/// </summary>
        [AttributeLogicalName("versionnumber")]
        public long? VersionNumber
        {
            get
            {
                return GetAttributeValue<long?>("versionnumber");
            }
        }

        /// <summary>
		/// Unique identifier for Workflow.
		/// </summary>
        [AttributeLogicalName("workflowid")]
        public EntityReference? WorkflowId
        {
            get
            {
                return GetAttributeValue<EntityReference?>("workflowid");
            }
            set
            {
                SetAttributeValue("workflowid", value);
            }
        }
        #endregion

        #region NavigationProperties

        /// <summary>
        /// 1:N routingrule_AsyncOperations
        /// </summary>
        [RelationshipSchemaName("routingrule_AsyncOperations")]
        public IEnumerable<AsyncOperation> RoutingruleAsyncOperations
        {
            get
            {
                return GetRelatedEntities<AsyncOperation>("routingrule_AsyncOperations", null);
            }
            set
            {
                SetRelatedEntities("routingrule_AsyncOperations", null, value);
            }
        }

        /// <summary>
        /// 1:N routingrule_entries
        /// </summary>
        [RelationshipSchemaName("routingrule_entries")]
        public IEnumerable<RoutingRuleItem> RoutingruleEntries
        {
            get
            {
                return GetRelatedEntities<RoutingRuleItem>("routingrule_entries", null);
            }
            set
            {
                SetRelatedEntities("routingrule_entries", null, value);
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
            public struct StateCode
            {
                public const int Draft = 0;
                public const int Active = 1;
            }
            public struct StatusCode
            {
                public const int Draft = 1;
                public const int Active = 2;
            }
        }
        #endregion

        #region LogicalNames
        public static partial class LogicalNames
        {
            public const string RoutingRuleId = "routingruleid";
            public const string ComponentState = "componentstate";
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
            public const string OwningTeam = "owningteam";
            public const string OwningUser = "owninguser";
            public const string RoutingRuleIdUnique = "routingruleidunique";
            public const string SolutionId = "solutionid";
            public const string StateCode = "statecode";
            public const string StatusCode = "statuscode";
            public const string TimeZoneRuleVersionNumber = "timezoneruleversionnumber";
            public const string TransactionCurrencyId = "transactioncurrencyid";
            public const string UTCConversionTimeZoneCode = "utcconversiontimezonecode";
            public const string VersionNumber = "versionnumber";
            public const string WorkflowId = "workflowid";
        }
        #endregion

        #region Relations
        public static partial class Relations
        {
            public static class OneToMany
            {
                public const string RoutingruleAnnotation = "routingrule_Annotation";
                public const string RoutingruleAsyncOperations = "routingrule_AsyncOperations";
                public const string RoutingruleBulkDeleteFailures = "routingrule_BulkDeleteFailures";
                public const string RoutingruleEntries = "routingrule_entries";
                public const string RoutingruleProcessSessions = "routingrule_ProcessSessions";
                public const string RoutingruleUserentityinstancedatas = "routingrule_userentityinstancedatas";
            }

            public static partial class ManyToOne
            {
                public const string BusinessUnitRoutingrule = "business_unit_routingrule";
                public const string LkRoutingruleCreatedby = "lk_routingrule_createdby";
                public const string LkRoutingruleCreatedonbehalfby = "lk_routingrule_createdonbehalfby";
                public const string LkRoutingruleModifiedby = "lk_routingrule_modifiedby";
                public const string LkRoutingruleModifiedonbehalfby = "lk_routingrule_modifiedonbehalfby";
                public const string OrganizationRoutingRules = "organization_RoutingRules";
                public const string OwnerRoutingrule = "owner_routingrule";
                public const string TeamRoutingrule = "team_routingrule";
                public const string TransactionCurrencyRoutingrule = "TransactionCurrency_Routingrule";
                public const string UserRoutingrule = "user_routingrule";
                public const string WorkflowRoutingrule = "Workflow_routingrule";
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
        public IQueryable<RoutingRule> RoutingRuleSet
        {
            get
            {
                return CreateQuery<RoutingRule>();
            }
        }
    }
    #endregion
}
