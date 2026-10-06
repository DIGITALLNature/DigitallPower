using System.Diagnostics.CodeAnalysis;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System.Runtime.Serialization;

// ReSharper disable All
namespace dgt.power.dataverse
{
    /// <inheritdoc cref="Microsoft.Xrm.Sdk.Entity" />
    /// <summary>
	/// Stage in the execution pipeline that a plug-in is to execute.
	/// </summary>
    [EntityLogicalName("sdkmessageprocessingstep")]
    [System.CodeDom.Compiler.GeneratedCode("dgtp", "2026")]
    [ExcludeFromCodeCoverage]
    [SuppressMessage("Design", "CA1034")]
    [SuppressMessage("Performance", "CA1815")]
    public partial class SdkMessageProcessingStep : Entity
    {
        #region ctor
        public SdkMessageProcessingStep() : base(EntityLogicalName) { }

        public SdkMessageProcessingStep(Guid id) : base(EntityLogicalName, id) { }

        public SdkMessageProcessingStep(KeyAttributeCollection keyAttributes) : base(EntityLogicalName, keyAttributes) { }

        public SdkMessageProcessingStep(string keyName, object keyValue) : base(EntityLogicalName, keyName, keyValue) { }
        #endregion

        #region consts
        public const string EntityLogicalName = "sdkmessageprocessingstep";
        public const string PrimaryNameAttribute = "name";
        public const int EntityTypeCode = 4608;
        #endregion

        #region Attributes
        [AttributeLogicalName("sdkmessageprocessingstepid")]
        [IgnoreDataMember]
        public new Guid Id
        {
            get
            {
                return base.Id;
            }
            set
            {
                SdkMessageProcessingStepId = value;
            }
        }

        /// <summary>
		/// Unique identifier of the SDK message processing step entity.
		/// </summary>
        [AttributeLogicalName("sdkmessageprocessingstepid")]
        public Guid? SdkMessageProcessingStepId
        {
            get
            {
                return GetAttributeValue<Guid?>("sdkmessageprocessingstepid");
            }
            set
            {
                SetAttributeValue("sdkmessageprocessingstepid", value);
                base.Id = value.HasValue ? value.Value : Guid.Empty;
            }
        }

        /// <summary>
		/// Indicates whether the asynchronous system job is automatically deleted on completion.
		/// </summary>
        [AttributeLogicalName("asyncautodelete")]
        public bool? AsyncAutoDelete
        {
            get
            {
                return GetAttributeValue<bool?>("asyncautodelete");
            }
            set
            {
                SetAttributeValue("asyncautodelete", value);
            }
        }

        
        [AttributeLogicalName("canbebypassed")]
        public bool? CanBeBypassed
        {
            get
            {
                return GetAttributeValue<bool?>("canbebypassed");
            }
            set
            {
                SetAttributeValue("canbebypassed", value);
            }
        }

        /// <summary>
		/// Identifies whether a SDK Message Processing Step type will be ReadOnly or Read Write. false - ReadWrite, true - ReadOnly
		/// </summary>
        [AttributeLogicalName("canusereadonlyconnection")]
        public bool? CanUseReadOnlyConnection
        {
            get
            {
                return GetAttributeValue<bool?>("canusereadonlyconnection");
            }
            set
            {
                SetAttributeValue("canusereadonlyconnection", value);
            }
        }

        /// <summary>
		/// For internal use only.
		/// </summary>
        [AttributeLogicalName("category")]
        public string? Category
        {
            get
            {
                return GetAttributeValue<string?>("category");
            }
            set
            {
                SetAttributeValue("category", value);
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
		/// Step-specific configuration for the plug-in type. Passed to the plug-in constructor at run time.
		/// </summary>
        [AttributeLogicalName("configuration")]
        public string? Configuration
        {
            get
            {
                return GetAttributeValue<string?>("configuration");
            }
            set
            {
                SetAttributeValue("configuration", value);
            }
        }

        /// <summary>
		/// Unique identifier of the user who created the SDK message processing step.
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
		/// Date and time when the SDK message processing step was created.
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
		/// Unique identifier of the delegate user who created the sdkmessageprocessingstep.
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
		/// Customization level of the SDK message processing step.
		/// </summary>
        [AttributeLogicalName("customizationlevel")]
        public int? CustomizationLevel
        {
            get
            {
                return GetAttributeValue<int?>("customizationlevel");
            }
        }

        /// <summary>
		/// Description of the SDK message processing step.
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
		/// EnablePluginProfiler
		/// </summary>
        [AttributeLogicalName("enablepluginprofiler")]
        public bool? EnablePluginProfiler
        {
            get
            {
                return GetAttributeValue<bool?>("enablepluginprofiler");
            }
            set
            {
                SetAttributeValue("enablepluginprofiler", value);
            }
        }

        /// <summary>
		/// Configuration for sending pipeline events to the Event Expander service.
		/// </summary>
        [AttributeLogicalName("eventexpander")]
        public string? EventExpander
        {
            get
            {
                return GetAttributeValue<string?>("eventexpander");
            }
            set
            {
                SetAttributeValue("eventexpander", value);
            }
        }

        /// <summary>
		/// Unique identifier of the associated event handler.
		/// </summary>
        [AttributeLogicalName("eventhandler")]
        public EntityReference? EventHandler
        {
            get
            {
                return GetAttributeValue<EntityReference?>("eventhandler");
            }
            set
            {
                SetAttributeValue("eventhandler", value);
            }
        }

        /// <summary>
		/// Comma-separated list of attributes. If at least one of these attributes is modified, the plug-in should execute.
		/// </summary>
        [AttributeLogicalName("filteringattributes")]
        public string? FilteringAttributesField
        {
            get
            {
                return GetAttributeValue<string?>("filteringattributes");
            }
            set
            {
                SetAttributeValue("filteringattributes", value);
            }
        }

        /// <summary>
		/// Unique identifier for fxexpression associated with SdkMessageProcessingStep.
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
		/// Unique identifier of the user to impersonate context when step is executed.
		/// </summary>
        [AttributeLogicalName("impersonatinguserid")]
        public EntityReference? ImpersonatingUserId
        {
            get
            {
                return GetAttributeValue<EntityReference?>("impersonatinguserid");
            }
            set
            {
                SetAttributeValue("impersonatinguserid", value);
            }
        }

        /// <summary>
		/// Version in which the form is introduced.
		/// </summary>
        [AttributeLogicalName("introducedversion")]
        public string? IntroducedVersion
        {
            get
            {
                return GetAttributeValue<string?>("introducedversion");
            }
            set
            {
                SetAttributeValue("introducedversion", value);
            }
        }

        /// <summary>
		/// Identifies if a plug-in should be executed from a parent pipeline, a child pipeline, or both.
		/// </summary>
        [AttributeLogicalName("invocationsource")]
        public OptionSetValue? InvocationSource
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("invocationsource");
            }
            set
            {
                SetAttributeValue("invocationsource", value);
            }
        }

        /// <summary>
		/// Information that specifies whether this component can be customized.
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
		/// Information that specifies whether this component should be hidden.
		/// </summary>
        [AttributeLogicalName("ishidden")]
        public BooleanManagedProperty? IsHidden
        {
            get
            {
                return GetAttributeValue<BooleanManagedProperty?>("ishidden");
            }
            set
            {
                SetAttributeValue("ishidden", value);
            }
        }

        /// <summary>
		/// Information that specifies whether this component is managed.
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
		/// Run-time mode of execution, for example, synchronous or asynchronous.
		/// </summary>
        [AttributeLogicalName("mode")]
        public OptionSetValue? Mode
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("mode");
            }
            set
            {
                SetAttributeValue("mode", value);
            }
        }

        /// <summary>
		/// Unique identifier of the user who last modified the SDK message processing step.
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
		/// Date and time when the SDK message processing step was last modified.
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
		/// Unique identifier of the delegate user who last modified the sdkmessageprocessingstep.
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
		/// Name of SdkMessage processing step.
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
		/// Unique identifier of the organization with which the SDK message processing step is associated.
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
		/// Unique identifier of the plug-in type associated with the step.
		/// </summary>
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
		/// Unique identifier for powerfxrule associated with SdkMessageProcessingStep.
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

        /// <summary>
		/// Processing order within the stage.
		/// </summary>
        [AttributeLogicalName("rank")]
        public int? Rank
        {
            get
            {
                return GetAttributeValue<int?>("rank");
            }
            set
            {
                SetAttributeValue("rank", value);
            }
        }

        /// <summary>
		/// For internal use only. Holds miscellaneous properties related to runtime integration.
		/// </summary>
        [AttributeLogicalName("runtimeintegrationproperties")]
        public string? RuntimeIntegrationProperties
        {
            get
            {
                return GetAttributeValue<string?>("runtimeintegrationproperties");
            }
            set
            {
                SetAttributeValue("runtimeintegrationproperties", value);
            }
        }

        /// <summary>
		/// Unique identifier of the SDK message filter.
		/// </summary>
        [AttributeLogicalName("sdkmessagefilterid")]
        public EntityReference? SdkMessageFilterId
        {
            get
            {
                return GetAttributeValue<EntityReference?>("sdkmessagefilterid");
            }
            set
            {
                SetAttributeValue("sdkmessagefilterid", value);
            }
        }

        /// <summary>
		/// Unique identifier of the SDK message.
		/// </summary>
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
		/// Unique identifier of the SDK message processing step.
		/// </summary>
        [AttributeLogicalName("sdkmessageprocessingstepidunique")]
        public Guid? SdkMessageProcessingStepIdUnique
        {
            get
            {
                return GetAttributeValue<Guid?>("sdkmessageprocessingstepidunique");
            }
        }

        /// <summary>
		/// Unique identifier of the Sdk message processing step secure configuration.
		/// </summary>
        [AttributeLogicalName("sdkmessageprocessingstepsecureconfigid")]
        public EntityReference? SdkMessageProcessingStepSecureConfigId
        {
            get
            {
                return GetAttributeValue<EntityReference?>("sdkmessageprocessingstepsecureconfigid");
            }
            set
            {
                SetAttributeValue("sdkmessageprocessingstepsecureconfigid", value);
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
		/// Stage in the execution pipeline that the SDK message processing step is in.
		/// </summary>
        [AttributeLogicalName("stage")]
        public OptionSetValue? Stage
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("stage");
            }
            set
            {
                SetAttributeValue("stage", value);
            }
        }

        /// <summary>
		/// Status of the SDK message processing step.
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
		/// Reason for the status of the SDK message processing step.
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
		/// Deployment that the SDK message processing step should be executed on; server, client, or both.
		/// </summary>
        [AttributeLogicalName("supporteddeployment")]
        public OptionSetValue? SupportedDeployment
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("supporteddeployment");
            }
            set
            {
                SetAttributeValue("supporteddeployment", value);
            }
        }

        /// <summary>
		/// Number that identifies a specific revision of the SDK message processing step.
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
        /// 1:N SdkMessageProcessingStep_AsyncOperations
        /// </summary>
        [RelationshipSchemaName("SdkMessageProcessingStep_AsyncOperations")]
        public IEnumerable<AsyncOperation> SdkMessageProcessingStepAsyncOperations
        {
            get
            {
                return GetRelatedEntities<AsyncOperation>("SdkMessageProcessingStep_AsyncOperations", null);
            }
            set
            {
                SetRelatedEntities("SdkMessageProcessingStep_AsyncOperations", null, value);
            }
        }

        /// <summary>
        /// 1:N sdkmessageprocessingstepid_sdkmessageprocessingstepimage
        /// </summary>
        [RelationshipSchemaName("sdkmessageprocessingstepid_sdkmessageprocessingstepimage")]
        public IEnumerable<SdkMessageProcessingStepImage> SdkmessageprocessingstepidSdkmessageprocessingstepimage
        {
            get
            {
                return GetRelatedEntities<SdkMessageProcessingStepImage>("sdkmessageprocessingstepid_sdkmessageprocessingstepimage", null);
            }
            set
            {
                SetRelatedEntities("sdkmessageprocessingstepid_sdkmessageprocessingstepimage", null, value);
            }
        }
        #endregion

        #region Options
        public static partial class Options
        {
            public struct AsyncAutoDelete
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct CanBeBypassed
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct CanUseReadOnlyConnection
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct ComponentState
            {
                public const int Published = 0;
                public const int Unpublished = 1;
                public const int Deleted = 2;
                public const int DeletedUnpublished = 3;
            }
            public struct EnablePluginProfiler
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct InvocationSource
            {
                public const int Internal = -1;
                public const int Parent = 0;
                public const int Child = 1;
            }
            public struct IsManaged
            {
                public const bool Unmanaged = false;
                public const bool Managed = true;
            }
            public struct Mode
            {
                public const int Synchronous = 0;
                public const int Asynchronous = 1;
            }
            public struct Stage
            {
                public const int InitialPreOperationForInternalUseOnly = 5;
                public const int PreValidation = 10;
                public const int InternalPreOperationBeforeExternalPluginsForInternalUseOnly = 15;
                public const int PreOperation = 20;
                public const int InternalPreOperationAfterExternalPluginsForInternalUseOnly = 25;
                public const int MainOperationForInternalUseOnly = 30;
                public const int InternalPostOperationBeforeExternalPluginsForInternalUseOnly = 35;
                public const int PostOperation = 40;
                public const int InternalPostOperationAfterExternalPluginsForInternalUseOnly = 45;
                public const int PostOperationDeprecated = 50;
                public const int FinalPostOperationForInternalUseOnly = 55;
                public const int PreCommitStageFiredBeforeTransactionCommitForInternalUseOnly = 80;
                public const int PostCommitStageFiredAfterTransactionCommitForInternalUseOnly = 90;
            }
            public struct StateCode
            {
                public const int Enabled = 0;
                public const int Disabled = 1;
            }
            public struct StatusCode
            {
                public const int Enabled = 1;
                public const int Disabled = 2;
            }
            public struct SupportedDeployment
            {
                public const int ServerOnly = 0;
                public const int MicrosoftDynamics365ClientForOutlookOnly = 1;
                public const int Both = 2;
            }
        }
        #endregion

        #region LogicalNames
        public static partial class LogicalNames
        {
            public const string SdkMessageProcessingStepId = "sdkmessageprocessingstepid";
            public const string AsyncAutoDelete = "asyncautodelete";
            public const string CanBeBypassed = "canbebypassed";
            public const string CanUseReadOnlyConnection = "canusereadonlyconnection";
            public const string Category = "category";
            public const string ComponentState = "componentstate";
            public const string Configuration = "configuration";
            public const string CreatedBy = "createdby";
            public const string CreatedOn = "createdon";
            public const string CreatedOnBehalfBy = "createdonbehalfby";
            public const string CustomizationLevel = "customizationlevel";
            public const string Description = "description";
            public const string EnablePluginProfiler = "enablepluginprofiler";
            public const string EventExpander = "eventexpander";
            public const string EventHandler = "eventhandler";
            public const string FilteringAttributes = "filteringattributes";
            public const string FxExpressionId = "fxexpressionid";
            public const string ImpersonatingUserId = "impersonatinguserid";
            public const string IntroducedVersion = "introducedversion";
            public const string InvocationSource = "invocationsource";
            public const string IsCustomizable = "iscustomizable";
            public const string IsHidden = "ishidden";
            public const string IsManaged = "ismanaged";
            public const string Mode = "mode";
            public const string ModifiedBy = "modifiedby";
            public const string ModifiedOn = "modifiedon";
            public const string ModifiedOnBehalfBy = "modifiedonbehalfby";
            public const string Name = "name";
            public const string OrganizationId = "organizationid";
            public const string OverwriteTime = "overwritetime";
            public const string PluginTypeId = "plugintypeid";
            public const string PowerfxRuleId = "powerfxruleid";
            public const string Rank = "rank";
            public const string RuntimeIntegrationProperties = "runtimeintegrationproperties";
            public const string SdkMessageFilterId = "sdkmessagefilterid";
            public const string SdkMessageId = "sdkmessageid";
            public const string SdkMessageProcessingStepIdUnique = "sdkmessageprocessingstepidunique";
            public const string SdkMessageProcessingStepSecureConfigId = "sdkmessageprocessingstepsecureconfigid";
            public const string SolutionId = "solutionid";
            public const string Stage = "stage";
            public const string StateCode = "statecode";
            public const string StatusCode = "statuscode";
            public const string SupportedDeployment = "supporteddeployment";
            public const string VersionNumber = "versionnumber";
        }
        #endregion

        #region Relations
        public static partial class Relations
        {
            public static class OneToMany
            {
                public const string SdkMessageProcessingStepAsyncOperations = "SdkMessageProcessingStep_AsyncOperations";
                public const string SdkmessageprocessingstepPluginSdkMessageProcessingStep = "sdkmessageprocessingstep_plugin_SdkMessageProcessingStep";
                public const string SdkmessageprocessingstepidSdkmessageprocessingstepimage = "sdkmessageprocessingstepid_sdkmessageprocessingstepimage";
                public const string UserentityinstancedataSdkmessageprocessingstep = "userentityinstancedata_sdkmessageprocessingstep";
            }

            public static partial class ManyToOne
            {
                public const string CreatedbySdkmessageprocessingstep = "createdby_sdkmessageprocessingstep";
                public const string FxexpressionSdkmessageprocessingstep = "fxexpression_sdkmessageprocessingstep";
                public const string ImpersonatinguseridSdkmessageprocessingstep = "impersonatinguserid_sdkmessageprocessingstep";
                public const string LkSdkmessageprocessingstepCreatedonbehalfby = "lk_sdkmessageprocessingstep_createdonbehalfby";
                public const string LkSdkmessageprocessingstepModifiedonbehalfby = "lk_sdkmessageprocessingstep_modifiedonbehalfby";
                public const string ModifiedbySdkmessageprocessingstep = "modifiedby_sdkmessageprocessingstep";
                public const string OrganizationSdkmessageprocessingstep = "organization_sdkmessageprocessingstep";
                public const string PlugintypeSdkmessageprocessingstep = "plugintype_sdkmessageprocessingstep";
                public const string PlugintypeidSdkmessageprocessingstep = "plugintypeid_sdkmessageprocessingstep";
                public const string PowerfxruleSdkmessageprocessingstep = "powerfxrule_sdkmessageprocessingstep";
                public const string SdkmessagefilteridSdkmessageprocessingstep = "sdkmessagefilterid_sdkmessageprocessingstep";
                public const string SdkmessageidSdkmessageprocessingstep = "sdkmessageid_sdkmessageprocessingstep";
                public const string SdkmessageprocessingstepsecureconfigidSdkmessageprocessingstep = "sdkmessageprocessingstepsecureconfigid_sdkmessageprocessingstep";
                public const string ServiceendpointSdkmessageprocessingstep = "serviceendpoint_sdkmessageprocessingstep";
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
        public IQueryable<SdkMessageProcessingStep> SdkMessageProcessingStepSet
        {
            get
            {
                return CreateQuery<SdkMessageProcessingStep>();
            }
        }
    }
    #endregion
}
