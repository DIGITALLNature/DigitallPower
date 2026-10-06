using System.Diagnostics.CodeAnalysis;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System.Runtime.Serialization;

// ReSharper disable All
namespace dgt.power.dataverse
{
    /// <inheritdoc cref="Microsoft.Xrm.Sdk.Entity" />
    /// <summary>
	/// Entity that defines a request parameter for a custom API
	/// </summary>
    [EntityLogicalName("customapirequestparameter")]
    [System.CodeDom.Compiler.GeneratedCode("dgtp", "2026")]
    [ExcludeFromCodeCoverage]
    [SuppressMessage("Design", "CA1034")]
    [SuppressMessage("Performance", "CA1815")]
    public partial class CustomAPIRequestParameter : Entity
    {
        #region ctor
        public CustomAPIRequestParameter() : base(EntityLogicalName) { }

        public CustomAPIRequestParameter(Guid id) : base(EntityLogicalName, id) { }

        public CustomAPIRequestParameter(KeyAttributeCollection keyAttributes) : base(EntityLogicalName, keyAttributes) { }

        public CustomAPIRequestParameter(string keyName, object keyValue) : base(EntityLogicalName, keyName, keyValue) { }
        #endregion

        #region consts
        public const string EntityLogicalName = "customapirequestparameter";
        public const string PrimaryNameAttribute = "name";
        public const int EntityTypeCode = 10037;
        #endregion

        #region Attributes
        [AttributeLogicalName("customapirequestparameterid")]
        [IgnoreDataMember]
        public new Guid Id
        {
            get
            {
                return base.Id;
            }
            set
            {
                CustomAPIRequestParameterId = value;
            }
        }

        /// <summary>
		/// Unique identifier for custom API request parameter instances
		/// </summary>
        [AttributeLogicalName("customapirequestparameterid")]
        public Guid? CustomAPIRequestParameterId
        {
            get
            {
                return GetAttributeValue<Guid?>("customapirequestparameterid");
            }
            set
            {
                SetAttributeValue("customapirequestparameterid", value);
                base.Id = value.HasValue ? value.Value : Guid.Empty;
            }
        }

        /// <summary>
		/// For internal use only.
		/// </summary>
        [AttributeLogicalName("componentidunique")]
        public Guid? ComponentIdUnique
        {
            get
            {
                return GetAttributeValue<Guid?>("componentidunique");
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
		/// Unique identifier for the custom API that owns this custom API request parameter
		/// </summary>
        [AttributeLogicalName("customapiid")]
        public EntityReference? CustomAPIId
        {
            get
            {
                return GetAttributeValue<EntityReference?>("customapiid");
            }
            set
            {
                SetAttributeValue("customapiid", value);
            }
        }

        /// <summary>
		/// Localized description for custom API request parameter instances
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
		/// Localized display name for custom API request parameter instances
		/// </summary>
        [AttributeLogicalName("displayname")]
        public string? DisplayName
        {
            get
            {
                return GetAttributeValue<string?>("displayname");
            }
            set
            {
                SetAttributeValue("displayname", value);
            }
        }

        /// <summary>
		/// Sequence number of the import that created this record.
		/// </summary>
        [AttributeLogicalName("importsequencenumber")]
        public int? ImportSequenceNumber
        {
            get
            {
                return GetAttributeValue<int?>("importsequencenumber");
            }
            set
            {
                SetAttributeValue("importsequencenumber", value);
            }
        }

        /// <summary>
		/// For internal use only.
		/// </summary>
        [AttributeLogicalName("iscustomizable")]
        public BooleanManagedProperty? IsCustomizable
        {
            get
            {
                return GetAttributeValue<BooleanManagedProperty?>("iscustomizable");
            }
            set
            {
                SetAttributeValue("iscustomizable", value);
            }
        }

        /// <summary>
		/// Indicates whether the solution component is part of a managed solution.
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
		/// Indicates if the custom API request parameter is optional
		/// </summary>
        [AttributeLogicalName("isoptional")]
        public bool? IsOptional
        {
            get
            {
                return GetAttributeValue<bool?>("isoptional");
            }
            set
            {
                SetAttributeValue("isoptional", value);
            }
        }

        /// <summary>
		/// The logical name of the entity bound to the custom API request parameter
		/// </summary>
        [AttributeLogicalName("logicalentityname")]
        public string? LogicalEntityName
        {
            get
            {
                return GetAttributeValue<string?>("logicalentityname");
            }
            set
            {
                SetAttributeValue("logicalentityname", value);
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
		/// The primary name of the custom API request parameter
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
		/// Date and time that the record was migrated.
		/// </summary>
        [AttributeLogicalName("overriddencreatedon")]
        public DateTime? OverriddenCreatedOn
        {
            get
            {
                return GetAttributeValue<DateTime?>("overriddencreatedon");
            }
            set
            {
                SetAttributeValue("overriddencreatedon", value);
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
		/// Unique identifier for the team that owns the record.
		/// </summary>
        [AttributeLogicalName("owningteam")]
        public EntityReference? OwningTeam
        {
            get
            {
                return GetAttributeValue<EntityReference?>("owningteam");
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
		/// Status of the Custom API Request Parameter
		/// </summary>
        [AttributeLogicalName("statecode")]
        public OptionSetValue? Statecode
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
		/// Reason for the status of the Custom API Request Parameter
		/// </summary>
        [AttributeLogicalName("statuscode")]
        public OptionSetValue? Statuscode
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
		/// The data type of the custom API request parameter
		/// </summary>
        [AttributeLogicalName("type")]
        public OptionSetValue? Type
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("type");
            }
            set
            {
                SetAttributeValue("type", value);
            }
        }

        /// <summary>
		/// Unique name for the custom API request parameter
		/// </summary>
        [AttributeLogicalName("uniquename")]
        public string? UniqueName
        {
            get
            {
                return GetAttributeValue<string?>("uniquename");
            }
            set
            {
                SetAttributeValue("uniquename", value);
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
		/// Version Number
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
        /// 1:N customapirequestparameter_AsyncOperations
        /// </summary>
        [RelationshipSchemaName("customapirequestparameter_AsyncOperations")]
        public IEnumerable<AsyncOperation> CustomapirequestparameterAsyncOperations
        {
            get
            {
                return GetRelatedEntities<AsyncOperation>("customapirequestparameter_AsyncOperations", null);
            }
            set
            {
                SetRelatedEntities("customapirequestparameter_AsyncOperations", null, value);
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
            public struct IsOptional
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct Statecode
            {
                public const int Active = 0;
                public const int Inactive = 1;
            }
            public struct Statuscode
            {
                public const int Active = 1;
                public const int Inactive = 2;
            }
            public struct Type
            {
                public const int Boolean = 0;
                public const int DateTime = 1;
                public const int Decimal = 2;
                public const int Entity = 3;
                public const int EntityCollection = 4;
                public const int EntityReference = 5;
                public const int Float = 6;
                public const int Integer = 7;
                public const int Money = 8;
                public const int Picklist = 9;
                public const int String = 10;
                public const int StringArray = 11;
                public const int Guid = 12;
            }
        }
        #endregion

        #region LogicalNames
        public static partial class LogicalNames
        {
            public const string CustomAPIRequestParameterId = "customapirequestparameterid";
            public const string ComponentIdUnique = "componentidunique";
            public const string ComponentState = "componentstate";
            public const string CreatedBy = "createdby";
            public const string CreatedOn = "createdon";
            public const string CreatedOnBehalfBy = "createdonbehalfby";
            public const string CustomAPIId = "customapiid";
            public const string Description = "description";
            public const string DisplayName = "displayname";
            public const string ImportSequenceNumber = "importsequencenumber";
            public const string IsCustomizable = "iscustomizable";
            public const string IsManaged = "ismanaged";
            public const string IsOptional = "isoptional";
            public const string LogicalEntityName = "logicalentityname";
            public const string ModifiedBy = "modifiedby";
            public const string ModifiedOn = "modifiedon";
            public const string ModifiedOnBehalfBy = "modifiedonbehalfby";
            public const string Name = "name";
            public const string OverriddenCreatedOn = "overriddencreatedon";
            public const string OverwriteTime = "overwritetime";
            public const string OwnerId = "ownerid";
            public const string OwningBusinessUnit = "owningbusinessunit";
            public const string OwningTeam = "owningteam";
            public const string OwningUser = "owninguser";
            public const string SolutionId = "solutionid";
            public const string Statecode = "statecode";
            public const string Statuscode = "statuscode";
            public const string TimeZoneRuleVersionNumber = "timezoneruleversionnumber";
            public const string Type = "type";
            public const string UniqueName = "uniquename";
            public const string UTCConversionTimeZoneCode = "utcconversiontimezonecode";
            public const string VersionNumber = "versionnumber";
        }
        #endregion

        #region AlternateKeys
        public static partial class AlternateKeys
        {
            public const string CustomAPIRequestParameterExportKey = "custom api request parameter export key";
        }
        #endregion

        #region Relations
        public static partial class Relations
        {
            public static class OneToMany
            {
                public const string CustomapirequestparameterAsyncOperations = "customapirequestparameter_AsyncOperations";
                public const string CustomapirequestparameterBulkDeleteFailures = "customapirequestparameter_BulkDeleteFailures";
                public const string CustomapirequestparameterMailboxTrackingFolders = "customapirequestparameter_MailboxTrackingFolders";
                public const string CustomapirequestparameterPrincipalObjectAttributeAccesses = "customapirequestparameter_PrincipalObjectAttributeAccesses";
                public const string CustomapirequestparameterProcessSession = "customapirequestparameter_ProcessSession";
                public const string CustomapirequestparameterSyncErrors = "customapirequestparameter_SyncErrors";
                public const string CustomapirequestparameterUserEntityInstanceDatas = "customapirequestparameter_UserEntityInstanceDatas";
            }

            public static partial class ManyToOne
            {
                public const string CustomapiCustomapirequestparameter = "customapi_customapirequestparameter";
                public const string LkCustomapirequestparameterCreatedby = "lk_customapirequestparameter_createdby";
                public const string LkCustomapirequestparameterCreatedonbehalfby = "lk_customapirequestparameter_createdonbehalfby";
                public const string LkCustomapirequestparameterModifiedby = "lk_customapirequestparameter_modifiedby";
                public const string LkCustomapirequestparameterModifiedonbehalfby = "lk_customapirequestparameter_modifiedonbehalfby";
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
        public IQueryable<CustomAPIRequestParameter> CustomAPIRequestParameterSet
        {
            get
            {
                return CreateQuery<CustomAPIRequestParameter>();
            }
        }
    }
    #endregion
}
