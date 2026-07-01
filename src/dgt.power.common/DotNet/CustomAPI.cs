using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using AttributeCollection = Microsoft.Xrm.Sdk.AttributeCollection;

// ReSharper disable All
namespace dgt.power.dataverse
{
    /// <inheritdoc cref="Microsoft.Xrm.Sdk.Entity" />
    /// <summary>
	/// Entity that defines a custom API
	/// </summary>
    [DataContract]
    [EntityLogicalName("customapi")]
    [System.CodeDom.Compiler.GeneratedCode("dgtp", "2026")]
    [ExcludeFromCodeCoverage]
    [SuppressMessage("Design", "CA1034")]
    [SuppressMessage("Performance", "CA1815")]
    public partial class CustomAPI : Entity, INotifyPropertyChanging, INotifyPropertyChanged
    {
        #region ctor
        [DebuggerNonUserCode]
        public CustomAPI() : this(false)
        {
        }
        [DebuggerNonUserCode]
        public CustomAPI(bool trackChanges = false) : base(EntityLogicalName)
        {
            _trackChanges = trackChanges;
        }
        [DebuggerNonUserCode]
        public CustomAPI(Guid id, bool trackChanges = false) : base(EntityLogicalName, id)
        {
            _trackChanges = trackChanges;
        }
        [DebuggerNonUserCode]
        public CustomAPI(KeyAttributeCollection keyAttributes, bool trackChanges = false) : base(EntityLogicalName, keyAttributes)
        {
            _trackChanges = trackChanges;
        }
        [DebuggerNonUserCode]
        public CustomAPI(string keyName, object keyValue, bool trackChanges = false) : base(EntityLogicalName, keyName, keyValue)
        {
            _trackChanges = trackChanges;
        }
        #endregion

        #region fields
        private readonly bool _trackChanges;
        private readonly Lazy<HashSet<string>> _changedProperties = new();
        #endregion

        #region consts
        public const string EntityLogicalName = "customapi";
        public const string PrimaryNameAttribute = "name";
        public const int EntityTypeCode = 10038;
        #endregion

        #region Events
        #pragma warning disable CS8612
        public event PropertyChangedEventHandler? PropertyChanged;
        public event PropertyChangingEventHandler? PropertyChanging;
        #pragma warning restore CS8612
        [DebuggerNonUserCode]
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            if (_trackChanges)
            {
                _changedProperties.Value.Add(propertyName);
            }
        }
        [DebuggerNonUserCode]
        private void OnPropertyChanging([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanging != null) PropertyChanging.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }
        #endregion

        #region Attributes
        [AttributeLogicalName("customapiid")]
        public new Guid Id
        {
            [DebuggerNonUserCode]
            get
            {
                return base.Id;
            }
            [DebuggerNonUserCode]
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
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<Guid?>("customapiid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("customapiid", value);
                base.Id = value.HasValue ? value.Value : Guid.Empty;
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// The type of custom processing step allowed
		/// </summary>
        [AttributeLogicalName("allowedcustomprocessingsteptype")]
        public OptionSetValue? AllowedCustomProcessingStepType
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("allowedcustomprocessingsteptype");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("allowedcustomprocessingsteptype", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// The binding type of the custom API
		/// </summary>
        [AttributeLogicalName("bindingtype")]
        public OptionSetValue? BindingType
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("bindingtype");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("bindingtype", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// The logical name of the entity bound to the custom API
		/// </summary>
        [AttributeLogicalName("boundentitylogicalname")]
        public string? BoundEntityLogicalName
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("boundentitylogicalname");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("boundentitylogicalname", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// For internal use only.
		/// </summary>
        [AttributeLogicalName("componentidunique")]
        public Guid? ComponentIdUnique
        {
            [DebuggerNonUserCode]
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
            [DebuggerNonUserCode]
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
            [DebuggerNonUserCode]
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
            [DebuggerNonUserCode]
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
            [DebuggerNonUserCode]
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
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("description");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("description", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Localized display name for custom API instances
		/// </summary>
        [AttributeLogicalName("displayname")]
        public string? DisplayName
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("displayname");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("displayname", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Name of the privilege that allows execution of the custom API
		/// </summary>
        [AttributeLogicalName("executeprivilegename")]
        public string? ExecutePrivilegeName
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("executeprivilegename");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("executeprivilegename", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier for fxexpression associated with Custom API.
		/// </summary>
        [AttributeLogicalName("fxexpressionid")]
        public EntityReference? FxExpressionId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<EntityReference?>("fxexpressionid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("fxexpressionid", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Sequence number of the import that created this record.
		/// </summary>
        [AttributeLogicalName("importsequencenumber")]
        public int? ImportSequenceNumber
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("importsequencenumber");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("importsequencenumber", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// For internal use only.
		/// </summary>
        [AttributeLogicalName("iscustomizable")]
        public BooleanManagedProperty? IsCustomizable
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<BooleanManagedProperty?>("iscustomizable");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("iscustomizable", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates if the custom API is a function (GET is supported) or not (POST is supported)
		/// </summary>
        [AttributeLogicalName("isfunction")]
        public bool? IsFunction
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isfunction");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("isfunction", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether the solution component is part of a managed solution.
		/// </summary>
        [AttributeLogicalName("ismanaged")]
        public bool? IsManaged
        {
            [DebuggerNonUserCode]
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
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isprivate");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("isprivate", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier of the user who modified the record.
		/// </summary>
        [AttributeLogicalName("modifiedby")]
        public EntityReference? ModifiedBy
        {
            [DebuggerNonUserCode]
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
            [DebuggerNonUserCode]
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
            [DebuggerNonUserCode]
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
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("name");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("name", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Date and time that the record was migrated.
		/// </summary>
        [AttributeLogicalName("overriddencreatedon")]
        public DateTime? OverriddenCreatedOn
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<DateTime?>("overriddencreatedon");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("overriddencreatedon", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// For internal use only.
		/// </summary>
        [AttributeLogicalName("overwritetime")]
        public DateTime? OverwriteTime
        {
            [DebuggerNonUserCode]
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
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<EntityReference?>("ownerid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("ownerid", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier for the business unit that owns the record
		/// </summary>
        [AttributeLogicalName("owningbusinessunit")]
        public EntityReference? OwningBusinessUnit
        {
            [DebuggerNonUserCode]
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
            [DebuggerNonUserCode]
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
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<EntityReference?>("owninguser");
            }
        }

        
        [AttributeLogicalName("plugintypeid")]
        public EntityReference? PluginTypeId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<EntityReference?>("plugintypeid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("plugintypeid", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier for powerfxrule associated with Custom API.
		/// </summary>
        [AttributeLogicalName("powerfxruleid")]
        public EntityReference? PowerfxRuleId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<EntityReference?>("powerfxruleid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("powerfxruleid", value);
                OnPropertyChanged();
            }
        }

        
        [AttributeLogicalName("sdkmessageid")]
        public EntityReference? SdkMessageId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<EntityReference?>("sdkmessageid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("sdkmessageid", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier of the associated solution.
		/// </summary>
        [AttributeLogicalName("solutionid")]
        public Guid? SolutionId
        {
            [DebuggerNonUserCode]
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
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("statecode");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("statecode", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Reason for the status of the Custom API
		/// </summary>
        [AttributeLogicalName("statuscode")]
        public OptionSetValue? Statuscode
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("statuscode");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("statuscode", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// For internal use only.
		/// </summary>
        [AttributeLogicalName("timezoneruleversionnumber")]
        public int? TimeZoneRuleVersionNumber
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("timezoneruleversionnumber");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("timezoneruleversionnumber", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique name for the custom API
		/// </summary>
        [AttributeLogicalName("uniquename")]
        public string? UniqueName
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("uniquename");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("uniquename", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Time zone code that was in use when the record was created.
		/// </summary>
        [AttributeLogicalName("utcconversiontimezonecode")]
        public int? UTCConversionTimeZoneCode
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("utcconversiontimezonecode");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("utcconversiontimezonecode", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Version Number
		/// </summary>
        [AttributeLogicalName("versionnumber")]
        public long? VersionNumber
        {
            [DebuggerNonUserCode]
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
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("workflowsdkstepenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("workflowsdkstepenabled", value);
                OnPropertyChanged();
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
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<AsyncOperation>("customapi_AsyncOperations", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("customapi_AsyncOperations", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N customapi_customapirequestparameter
        /// </summary>
        [RelationshipSchemaName("customapi_customapirequestparameter")]
        public IEnumerable<CustomAPIRequestParameter> CustomapiCustomapirequestparameter
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<CustomAPIRequestParameter>("customapi_customapirequestparameter", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("customapi_customapirequestparameter", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N customapi_customapiresponseproperty
        /// </summary>
        [RelationshipSchemaName("customapi_customapiresponseproperty")]
        public IEnumerable<CustomAPIResponseProperty> CustomapiCustomapiresponseproperty
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<CustomAPIResponseProperty>("customapi_customapiresponseproperty", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("customapi_customapiresponseproperty", null, value);
                OnPropertyChanged();
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

        public static CustomAPI Retrieve(IOrganizationService service, Guid id)
        {
            return Retrieve(service, id, new ColumnSet(true));
        }

        public static CustomAPI Retrieve(IOrganizationService service, Guid id, ColumnSet columnSet)
        {
            return service.Retrieve("customapi", id, columnSet).ToEntity<CustomAPI>();
        }

        public CustomAPI GetChangedEntity()
        {
            if (!_trackChanges)
            {
                return this;
            }

            var attr = new AttributeCollection();
            foreach (var attrName in _changedProperties.Value.Select(changedProperty => GetType().GetProperty(changedProperty)!.GetCustomAttribute<AttributeLogicalNameAttribute>()!.LogicalName).Where(attrName => Contains(attrName)))
            {
                attr.Add(attrName, this[attrName]);
            }
            return new CustomAPI(Id) { Attributes = attr };
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
