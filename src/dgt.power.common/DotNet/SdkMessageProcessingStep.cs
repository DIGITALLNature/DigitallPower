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
	/// Stage in the execution pipeline that a plug-in is to execute.
	/// </summary>
    [DataContract]
    [EntityLogicalName("sdkmessageprocessingstep")]
    [System.CodeDom.Compiler.GeneratedCode("dgtp", "2026")]
    [ExcludeFromCodeCoverage]
    [SuppressMessage("Design", "CA1034")]
    [SuppressMessage("Performance", "CA1815")]
    public partial class SdkMessageProcessingStep : Entity, INotifyPropertyChanging, INotifyPropertyChanged
    {
        #region ctor
        [DebuggerNonUserCode]
        public SdkMessageProcessingStep() : this(false)
        {
        }
        [DebuggerNonUserCode]
        public SdkMessageProcessingStep(bool trackChanges = false) : base(EntityLogicalName)
        {
            _trackChanges = trackChanges;
        }
        [DebuggerNonUserCode]
        public SdkMessageProcessingStep(Guid id, bool trackChanges = false) : base(EntityLogicalName, id)
        {
            _trackChanges = trackChanges;
        }
        [DebuggerNonUserCode]
        public SdkMessageProcessingStep(KeyAttributeCollection keyAttributes, bool trackChanges = false) : base(EntityLogicalName, keyAttributes)
        {
            _trackChanges = trackChanges;
        }
        [DebuggerNonUserCode]
        public SdkMessageProcessingStep(string keyName, object keyValue, bool trackChanges = false) : base(EntityLogicalName, keyName, keyValue)
        {
            _trackChanges = trackChanges;
        }
        #endregion

        #region fields
        private readonly bool _trackChanges;
        private readonly Lazy<HashSet<string>> _changedProperties = new();
        #endregion

        #region consts
        public const string EntityLogicalName = "sdkmessageprocessingstep";
        public const string PrimaryNameAttribute = "name";
        public const int EntityTypeCode = 4608;
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
        [AttributeLogicalName("sdkmessageprocessingstepid")]
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
                SdkMessageProcessingStepId = value;
            }
        }

        /// <summary>
		/// Unique identifier of the SDK message processing step entity.
		/// </summary>
        [AttributeLogicalName("sdkmessageprocessingstepid")]
        public Guid? SdkMessageProcessingStepId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<Guid?>("sdkmessageprocessingstepid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("sdkmessageprocessingstepid", value);
                base.Id = value.HasValue ? value.Value : Guid.Empty;
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether the asynchronous system job is automatically deleted on completion.
		/// </summary>
        [AttributeLogicalName("asyncautodelete")]
        public bool? AsyncAutoDelete
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("asyncautodelete");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("asyncautodelete", value);
                OnPropertyChanged();
            }
        }

        
        [AttributeLogicalName("canbebypassed")]
        public bool? CanBeBypassed
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("canbebypassed");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("canbebypassed", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Identifies whether a SDK Message Processing Step type will be ReadOnly or Read Write. false - ReadWrite, true - ReadOnly
		/// </summary>
        [AttributeLogicalName("canusereadonlyconnection")]
        public bool? CanUseReadOnlyConnection
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("canusereadonlyconnection");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("canusereadonlyconnection", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// For internal use only.
		/// </summary>
        [AttributeLogicalName("category")]
        public string? Category
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("category");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("category", value);
                OnPropertyChanged();
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
		/// Step-specific configuration for the plug-in type. Passed to the plug-in constructor at run time.
		/// </summary>
        [AttributeLogicalName("configuration")]
        public string? Configuration
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("configuration");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("configuration", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier of the user who created the SDK message processing step.
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
		/// Date and time when the SDK message processing step was created.
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
		/// Unique identifier of the delegate user who created the sdkmessageprocessingstep.
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
		/// Customization level of the SDK message processing step.
		/// </summary>
        [AttributeLogicalName("customizationlevel")]
        public int? CustomizationLevel
        {
            [DebuggerNonUserCode]
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
		/// EnablePluginProfiler
		/// </summary>
        [AttributeLogicalName("enablepluginprofiler")]
        public bool? EnablePluginProfiler
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("enablepluginprofiler");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("enablepluginprofiler", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Configuration for sending pipeline events to the Event Expander service.
		/// </summary>
        [AttributeLogicalName("eventexpander")]
        public string? EventExpander
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("eventexpander");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("eventexpander", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier of the associated event handler.
		/// </summary>
        [AttributeLogicalName("eventhandler")]
        public EntityReference? EventHandler
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<EntityReference?>("eventhandler");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("eventhandler", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Comma-separated list of attributes. If at least one of these attributes is modified, the plug-in should execute.
		/// </summary>
        [AttributeLogicalName("filteringattributes")]
        public string? FilteringAttributesField
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("filteringattributes");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("filteringattributes", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier for fxexpression associated with SdkMessageProcessingStep.
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
		/// Unique identifier of the user to impersonate context when step is executed.
		/// </summary>
        [AttributeLogicalName("impersonatinguserid")]
        public EntityReference? ImpersonatingUserId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<EntityReference?>("impersonatinguserid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("impersonatinguserid", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Version in which the form is introduced.
		/// </summary>
        [AttributeLogicalName("introducedversion")]
        public string? IntroducedVersion
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("introducedversion");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("introducedversion", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Identifies if a plug-in should be executed from a parent pipeline, a child pipeline, or both.
		/// </summary>
        [AttributeLogicalName("invocationsource")]
        public OptionSetValue? InvocationSource
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("invocationsource");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("invocationsource", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Information that specifies whether this component can be customized.
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
		/// Information that specifies whether this component should be hidden.
		/// </summary>
        [AttributeLogicalName("ishidden")]
        public BooleanManagedProperty? IsHidden
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<BooleanManagedProperty?>("ishidden");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("ishidden", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Information that specifies whether this component is managed.
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
		/// Run-time mode of execution, for example, synchronous or asynchronous.
		/// </summary>
        [AttributeLogicalName("mode")]
        public OptionSetValue? Mode
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("mode");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("mode", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier of the user who last modified the SDK message processing step.
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
		/// Date and time when the SDK message processing step was last modified.
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
		/// Unique identifier of the delegate user who last modified the sdkmessageprocessingstep.
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
		/// Name of SdkMessage processing step.
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
		/// Unique identifier of the organization with which the SDK message processing step is associated.
		/// </summary>
        [AttributeLogicalName("organizationid")]
        public EntityReference? OrganizationId
        {
            [DebuggerNonUserCode]
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
            [DebuggerNonUserCode]
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
		/// Unique identifier for powerfxrule associated with SdkMessageProcessingStep.
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

        /// <summary>
		/// Processing order within the stage.
		/// </summary>
        [AttributeLogicalName("rank")]
        public int? Rank
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("rank");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("rank", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// For internal use only. Holds miscellaneous properties related to runtime integration.
		/// </summary>
        [AttributeLogicalName("runtimeintegrationproperties")]
        public string? RuntimeIntegrationProperties
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("runtimeintegrationproperties");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("runtimeintegrationproperties", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier of the SDK message filter.
		/// </summary>
        [AttributeLogicalName("sdkmessagefilterid")]
        public EntityReference? SdkMessageFilterId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<EntityReference?>("sdkmessagefilterid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("sdkmessagefilterid", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier of the SDK message.
		/// </summary>
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
		/// Unique identifier of the SDK message processing step.
		/// </summary>
        [AttributeLogicalName("sdkmessageprocessingstepidunique")]
        public Guid? SdkMessageProcessingStepIdUnique
        {
            [DebuggerNonUserCode]
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
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<EntityReference?>("sdkmessageprocessingstepsecureconfigid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("sdkmessageprocessingstepsecureconfigid", value);
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
		/// Stage in the execution pipeline that the SDK message processing step is in.
		/// </summary>
        [AttributeLogicalName("stage")]
        public OptionSetValue? Stage
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("stage");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("stage", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Status of the SDK message processing step.
		/// </summary>
        [AttributeLogicalName("statecode")]
        public OptionSetValue? StateCode
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
		/// Reason for the status of the SDK message processing step.
		/// </summary>
        [AttributeLogicalName("statuscode")]
        public OptionSetValue? StatusCode
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
		/// Deployment that the SDK message processing step should be executed on; server, client, or both.
		/// </summary>
        [AttributeLogicalName("supporteddeployment")]
        public OptionSetValue? SupportedDeployment
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("supporteddeployment");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("supporteddeployment", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Number that identifies a specific revision of the SDK message processing step.
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
        #endregion

        #region NavigationProperties

        /// <summary>
        /// 1:N SdkMessageProcessingStep_AsyncOperations
        /// </summary>
        [RelationshipSchemaName("SdkMessageProcessingStep_AsyncOperations")]
        public IEnumerable<AsyncOperation> SdkMessageProcessingStepAsyncOperations
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<AsyncOperation>("SdkMessageProcessingStep_AsyncOperations", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("SdkMessageProcessingStep_AsyncOperations", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N sdkmessageprocessingstepid_sdkmessageprocessingstepimage
        /// </summary>
        [RelationshipSchemaName("sdkmessageprocessingstepid_sdkmessageprocessingstepimage")]
        public IEnumerable<SdkMessageProcessingStepImage> SdkmessageprocessingstepidSdkmessageprocessingstepimage
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<SdkMessageProcessingStepImage>("sdkmessageprocessingstepid_sdkmessageprocessingstepimage", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("sdkmessageprocessingstepid_sdkmessageprocessingstepimage", null, value);
                OnPropertyChanged();
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

        public static SdkMessageProcessingStep Retrieve(IOrganizationService service, Guid id)
        {
            return Retrieve(service, id, new ColumnSet(true));
        }

        public static SdkMessageProcessingStep Retrieve(IOrganizationService service, Guid id, ColumnSet columnSet)
        {
            return service.Retrieve("sdkmessageprocessingstep", id, columnSet).ToEntity<SdkMessageProcessingStep>();
        }

        public SdkMessageProcessingStep GetChangedEntity()
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
            return new SdkMessageProcessingStep(Id) { Attributes = attr };
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
