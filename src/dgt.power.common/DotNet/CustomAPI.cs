using System.Diagnostics.CodeAnalysis;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System.Runtime.Serialization;

// ReSharper disable All
namespace dgt.power.dataverse
{
    /// <inheritdoc cref="Microsoft.Xrm.Sdk.Entity" />
    /// <summary>
	/// Entity that defines a custom API
	/// </summary>
    [EntityLogicalName("customapi")]
    [System.CodeDom.Compiler.GeneratedCode("dgtp", "2026")]
    [ExcludeFromCodeCoverage]
    [SuppressMessage("Design", "CA1034")]
    [SuppressMessage("Performance", "CA1815")]
    public partial class CustomAPI : Entity
    {
        #region ctor
        public CustomAPI() : base(EntityLogicalName) { }

        public CustomAPI(Guid id) : base(EntityLogicalName, id) { }

        public CustomAPI(KeyAttributeCollection keyAttributes) : base(EntityLogicalName, keyAttributes) { }

        public CustomAPI(string keyName, object keyValue) : base(EntityLogicalName, keyName, keyValue) { }
        #endregion

        #region consts
        public const string EntityLogicalName = "customapi";
        public const string PrimaryNameAttribute = "name";
        public const int EntityTypeCode = 10036;
        #endregion

        #region Attributes
        [AttributeLogicalName("customapiid")]
        [IgnoreDataMember]
        public new Guid Id
        {
            get
            {
                return base.Id;
            }
            set
            {
                CustomAPIId = value;
            }
        }

        /// <summary>
		/// Unique identifier for custom API instances
		/// </summary>
        [AttributeLogicalName("customapiid")]
        public Guid? CustomAPIId
        {
            get
            {
                return GetAttributeValue<Guid?>("customapiid");
            }
            set
            {
                SetAttributeValue("customapiid", value);
                base.Id = value.HasValue ? value.Value : Guid.Empty;
            }
        }

        /// <summary>
		/// The type of custom processing step allowed
		/// </summary>
        [AttributeLogicalName("allowedcustomprocessingsteptype")]
        public OptionSetValue? AllowedCustomProcessingStepType
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("allowedcustomprocessingsteptype");
            }
            set
            {
                SetAttributeValue("allowedcustomprocessingsteptype", value);
            }
        }

        /// <summary>
		/// The binding type of the custom API
		/// </summary>
        [AttributeLogicalName("bindingtype")]
        public OptionSetValue? BindingType
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("bindingtype");
            }
            set
            {
                SetAttributeValue("bindingtype", value);
            }
        }

        /// <summary>
		/// The logical name of the entity bound to the custom API
		/// </summary>
        [AttributeLogicalName("boundentitylogicalname")]
        public string? BoundEntityLogicalName
        {
            get
            {
                return GetAttributeValue<string?>("boundentitylogicalname");
            }
            set
            {
                SetAttributeValue("boundentitylogicalname", value);
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
		/// Localized description for custom API instances
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
		/// Localized display name for custom API instances
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
		/// Name of the privilege that allows execution of the custom API
		/// </summary>
        [AttributeLogicalName("executeprivilegename")]
        public string? ExecutePrivilegeName
        {
            get
            {
                return GetAttributeValue<string?>("executeprivilegename");
            }
            set
            {
                SetAttributeValue("executeprivilegename", value);
            }
        }

        /// <summary>
		/// Unique identifier for fxexpression associated with Custom API.
		/// </summary>
        [AttributeLogicalName("fxexpressionid")]
        public EntityReference? FxExpressionId
        {
            get
            {
                return GetAttributeValue<EntityReference?>("fxexpressionid");
            }
            set
            {
                SetAttributeValue("fxexpressionid", value);
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
		/// Indicates if the custom API is a function (GET is supported) or not (POST is supported)
		/// </summary>
        [AttributeLogicalName("isfunction")]
        public bool? IsFunction
        {
            get
            {
                return GetAttributeValue<bool?>("isfunction");
            }
            set
            {
                SetAttributeValue("isfunction", value);
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
		/// Indicates if the custom API is private (hidden from metadata and documentation)
		/// </summary>
        [AttributeLogicalName("isprivate")]
        public bool? IsPrivate
        {
            get
            {
                return GetAttributeValue<bool?>("isprivate");
            }
            set
            {
                SetAttributeValue("isprivate", value);
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
		/// The primary name of the custom API
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

        
        [AttributeLogicalName("plugintypeid")]
        public EntityReference? PluginTypeId
        {
            get
            {
                return GetAttributeValue<EntityReference?>("plugintypeid");
            }
            set
            {
                SetAttributeValue("plugintypeid", value);
            }
        }

        /// <summary>
		/// Unique identifier for powerfxrule associated with Custom API.
		/// </summary>
        [AttributeLogicalName("powerfxruleid")]
        public EntityReference? PowerfxRuleId
        {
            get
            {
                return GetAttributeValue<EntityReference?>("powerfxruleid");
            }
            set
            {
                SetAttributeValue("powerfxruleid", value);
            }
        }

        
        [AttributeLogicalName("sdkmessageid")]
        public EntityReference? SdkMessageId
        {
            get
            {
                return GetAttributeValue<EntityReference?>("sdkmessageid");
            }
            set
            {
                SetAttributeValue("sdkmessageid", value);
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
		/// Status of the Custom API
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
		/// Reason for the status of the Custom API
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
		/// Unique name for the custom API
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

        /// <summary>
		/// Indicates if the custom API is enabled as a workflow action
		/// </summary>
        [AttributeLogicalName("workflowsdkstepenabled")]
        public bool? WorkflowSdkStepEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("workflowsdkstepenabled");
            }
            set
            {
                SetAttributeValue("workflowsdkstepenabled", value);
            }
        }
        #endregion

        #region NavigationProperties

        /// <summary>
        /// 1:N customapi_AsyncOperations
        /// </summary>
        [RelationshipSchemaName("customapi_AsyncOperations")]
        public IEnumerable<AsyncOperation> CustomapiAsyncOperations
        {
            get
            {
                return GetRelatedEntities<AsyncOperation>("customapi_AsyncOperations", null);
            }
            set
            {
                SetRelatedEntities("customapi_AsyncOperations", null, value);
            }
        }

        /// <summary>
        /// 1:N customapi_customapirequestparameter
        /// </summary>
        [RelationshipSchemaName("customapi_customapirequestparameter")]
        public IEnumerable<CustomAPIRequestParameter> CustomapiCustomapirequestparameter
        {
            get
            {
                return GetRelatedEntities<CustomAPIRequestParameter>("customapi_customapirequestparameter", null);
            }
            set
            {
                SetRelatedEntities("customapi_customapirequestparameter", null, value);
            }
        }

        /// <summary>
        /// 1:N customapi_customapiresponseproperty
        /// </summary>
        [RelationshipSchemaName("customapi_customapiresponseproperty")]
        public IEnumerable<CustomAPIResponseProperty> CustomapiCustomapiresponseproperty
        {
            get
            {
                return GetRelatedEntities<CustomAPIResponseProperty>("customapi_customapiresponseproperty", null);
            }
            set
            {
                SetRelatedEntities("customapi_customapiresponseproperty", null, value);
            }
        }
        #endregion

        #region Options
        public static partial class Options
        {
            public struct AllowedCustomProcessingStepType
            {
                public const int None = 0;
                public const int AsyncOnly = 1;
                public const int SyncAndAsync = 2;
            }
            public struct BindingType
            {
                public const int Global = 0;
                public const int Entity = 1;
                public const int EntityCollection = 2;
            }
            public struct ComponentState
            {
                public const int Published = 0;
                public const int Unpublished = 1;
                public const int Deleted = 2;
                public const int DeletedUnpublished = 3;
            }
            public struct IsFunction
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsManaged
            {
                public const bool Unmanaged = false;
                public const bool Managed = true;
            }
            public struct IsPrivate
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
            public struct WorkflowSdkStepEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
        }
        #endregion

        #region LogicalNames
        public static partial class LogicalNames
        {
            public const string CustomAPIId = "customapiid";
            public const string AllowedCustomProcessingStepType = "allowedcustomprocessingsteptype";
            public const string BindingType = "bindingtype";
            public const string BoundEntityLogicalName = "boundentitylogicalname";
            public const string ComponentIdUnique = "componentidunique";
            public const string ComponentState = "componentstate";
            public const string CreatedBy = "createdby";
            public const string CreatedOn = "createdon";
            public const string CreatedOnBehalfBy = "createdonbehalfby";
            public const string Description = "description";
            public const string DisplayName = "displayname";
            public const string ExecutePrivilegeName = "executeprivilegename";
            public const string FxExpressionId = "fxexpressionid";
            public const string ImportSequenceNumber = "importsequencenumber";
            public const string IsCustomizable = "iscustomizable";
            public const string IsFunction = "isfunction";
            public const string IsManaged = "ismanaged";
            public const string IsPrivate = "isprivate";
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
            public const string PluginTypeId = "plugintypeid";
            public const string PowerfxRuleId = "powerfxruleid";
            public const string SdkMessageId = "sdkmessageid";
            public const string SolutionId = "solutionid";
            public const string Statecode = "statecode";
            public const string Statuscode = "statuscode";
            public const string TimeZoneRuleVersionNumber = "timezoneruleversionnumber";
            public const string UniqueName = "uniquename";
            public const string UTCConversionTimeZoneCode = "utcconversiontimezonecode";
            public const string VersionNumber = "versionnumber";
            public const string WorkflowSdkStepEnabled = "workflowsdkstepenabled";
        }
        #endregion

        #region AlternateKeys
        public static partial class AlternateKeys
        {
            public const string CustomAPIExportKey = "custom api export key";
        }
        #endregion

        #region Relations
        public static partial class Relations
        {
            public static class OneToMany
            {
                public const string AIPluginOperationCustomAPICustomAPI = "AIPluginOperation_CustomAPI_CustomAPI";
                public const string CatalogassignmentCustomapi = "catalogassignment_customapi";
                public const string CustomapiAiskillconfigGenerationapi = "customapi_aiskillconfig_generationapi";
                public const string CustomapiAsyncOperations = "customapi_AsyncOperations";
                public const string CustomapiBulkDeleteFailures = "customapi_BulkDeleteFailures";
                public const string CustomapiCustomapirequestparameter = "customapi_customapirequestparameter";
                public const string CustomapiCustomapiresponseproperty = "customapi_customapiresponseproperty";
                public const string CustomapiMailboxTrackingFolders = "customapi_MailboxTrackingFolders";
                public const string CustomapiMsdynFunctionCustomapi = "customapi_msdyn_function_customapi";
                public const string CustomapiPluginCustomAPI = "customapi_plugin_CustomAPI";
                public const string CustomapiPrincipalObjectAttributeAccesses = "customapi_PrincipalObjectAttributeAccesses";
                public const string CustomapiProcessSession = "customapi_ProcessSession";
                public const string CustomapiServiceplanmapping = "customapi_serviceplanmapping";
                public const string CustomapiSyncErrors = "customapi_SyncErrors";
                public const string CustomapiUserEntityInstanceDatas = "customapi_UserEntityInstanceDatas";
                public const string FabricaiskillCustomapiid = "fabricaiskill_customapiid";
                public const string MCPToolCustomAPICustomAPI = "MCPTool_CustomAPI_CustomAPI";
                public const string MsdynCustomapiMsdynPmbusinessruleautomationconfigCustomApiId = "msdyn_customapi_msdyn_pmbusinessruleautomationconfig_CustomApiId";
                public const string MsdynFormmappingCustomapiid = "msdyn_formmapping_customapiid";
                public const string MsdynKnowledgeassetconfigurationCustomapiid = "msdyn_knowledgeassetconfiguration_customapiid";
            }

            public static partial class ManyToOne
            {
                public const string BusinessUnitCustomapi = "business_unit_customapi";
                public const string FxexpressionCustomapi = "fxexpression_customapi";
                public const string LkCustomapiCreatedby = "lk_customapi_createdby";
                public const string LkCustomapiCreatedonbehalfby = "lk_customapi_createdonbehalfby";
                public const string LkCustomapiModifiedby = "lk_customapi_modifiedby";
                public const string LkCustomapiModifiedonbehalfby = "lk_customapi_modifiedonbehalfby";
                public const string OwnerCustomapi = "owner_customapi";
                public const string PlugintypeCustomapi = "plugintype_customapi";
                public const string PowerfxruleCustomapi = "powerfxrule_customapi";
                public const string SdkmessageCustomapi = "sdkmessage_customapi";
                public const string TeamCustomapi = "team_customapi";
                public const string UserCustomapi = "user_customapi";
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
        public IQueryable<CustomAPI> CustomAPISet
        {
            get
            {
                return CreateQuery<CustomAPI>();
            }
        }
    }
    #endregion
}
