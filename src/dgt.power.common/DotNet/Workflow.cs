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
	/// Set of logical rules that define the steps necessary to automate a specific business process, task, or set of actions to be performed.
	/// </summary>
    [DataContract]
    [EntityLogicalName("workflow")]
    [System.CodeDom.Compiler.GeneratedCode("dgtp", "2026")]
    [ExcludeFromCodeCoverage]
    public partial class Workflow : Entity, INotifyPropertyChanging, INotifyPropertyChanged
    {
        #region ctor
        [DebuggerNonUserCode]
        public Workflow() : this(false)
        {
        }
        [DebuggerNonUserCode]
        public Workflow(bool trackChanges = false) : base(EntityLogicalName)
        {
            _trackChanges = trackChanges;
        }
        [DebuggerNonUserCode]
        public Workflow(Guid id, bool trackChanges = false) : base(EntityLogicalName, id)
        {
            _trackChanges = trackChanges;
        }
        [DebuggerNonUserCode]
        public Workflow(KeyAttributeCollection keyAttributes, bool trackChanges = false) : base(EntityLogicalName, keyAttributes)
        {
            _trackChanges = trackChanges;
        }
        [DebuggerNonUserCode]
        public Workflow(string keyName, object keyValue, bool trackChanges = false) : base(EntityLogicalName, keyName, keyValue)
        {
            _trackChanges = trackChanges;
        }
        #endregion

        #region fields
        private readonly bool _trackChanges;
        private readonly Lazy<HashSet<string>> _changedProperties = new();
        #endregion

        #region consts
        public const string EntityLogicalName = "workflow";
        public const string PrimaryNameAttribute = "name";
        public const int EntityTypeCode = 4703;
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
        [AttributeLogicalName("workflowid")]
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
                WorkflowId = value;
            }
        }

        /// <summary>
		/// Unique identifier of the process.
		/// </summary>
        [AttributeLogicalName("workflowid")]
        public Guid? WorkflowId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<Guid?>("workflowid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("workflowid", value);
                base.Id = value.HasValue ? value.Value : Guid.Empty;
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier of the latest activation record for the process.
		/// </summary>
        [AttributeLogicalName("activeworkflowid")]
        public EntityReference? ActiveWorkflowId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<EntityReference?>("activeworkflowid");
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

        /// <summary>
		/// Billing context this flow is in.
		/// </summary>
        [AttributeLogicalName("billingcontext")]
        public string? BillingContext
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("billingcontext");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("billingcontext", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Business Process Type.
		/// </summary>
        [AttributeLogicalName("businessprocesstype")]
        public OptionSetValue? BusinessProcessType
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("businessprocesstype");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("businessprocesstype", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Category of the process.
		/// </summary>
        [AttributeLogicalName("category")]
        public OptionSetValue? Category
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("category");
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
		/// Claims related to this workflow.
		/// </summary>
        [AttributeLogicalName("claims")]
        public string? Claims
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("claims");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("claims", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Business logic converted into client data
		/// </summary>
        [AttributeLogicalName("clientdata")]
        public string? ClientData
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("clientdata");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("clientdata", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// For Internal Use Only.
		/// </summary>
        [AttributeLogicalName("clientdataiscompressed")]
        public bool? ClientDataIsCompressed
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("clientdataiscompressed");
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
		/// Connection References related to this workflow.
		/// </summary>
        [AttributeLogicalName("connectionreferences")]
        public string? ConnectionReferences
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("connectionreferences");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("connectionreferences", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier of the user who created the process.
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
		/// Date and time when the process was created.
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
		/// Unique identifier of the delegate user who created the process.
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
		/// Create metadata for this workflow.
		/// </summary>
        [AttributeLogicalName("createmetadata")]
        public string? CreateMetadata
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("createmetadata");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("createmetadata", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Stage of the process when triggered on Create.
		/// </summary>
        [AttributeLogicalName("createstage")]
        public OptionSetValue? CreateStage
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("createstage");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("createstage", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Credentials related to this workflow.
		/// </summary>
        [AttributeLogicalName("credentials")]
        public string? Credentials
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("credentials");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("credentials", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Definition of the business logic of this workflow instance.
		/// </summary>
        [AttributeLogicalName("definition")]
        public string? Definition
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("definition");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("definition", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Stage of the process when triggered on Delete.
		/// </summary>
        [AttributeLogicalName("deletestage")]
        public OptionSetValue? DeleteStage
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("deletestage");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("deletestage", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Soft dependencies of this workflow instance.
		/// </summary>
        [AttributeLogicalName("dependencies")]
        public string? Dependencies
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("dependencies");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("dependencies", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Description of the process.
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
		/// Desktop flow modules related to this workflow.
		/// </summary>
        [AttributeLogicalName("desktopflowmodules")]
        public string? DesktopFlowModules
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("desktopflowmodules");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("desktopflowmodules", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// comma separated list of one or more Dynamics First Party Solution Unique names that this workflow is in context of.
		/// </summary>
        [AttributeLogicalName("dynamicssolutioncontext")]
        public string? DynamicsSolutionContext
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("dynamicssolutioncontext");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("dynamicssolutioncontext", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Shows the default image for the record.
		/// </summary>
        [AttributeLogicalName("entityimage")]
        public byte[]? EntityImage
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<byte[]?>("entityimage");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("entityimage", value);
                OnPropertyChanged();
            }
        }

        
        [AttributeLogicalName("entityimage_timestamp")]
        public long? EntityImageTimestamp
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<long?>("entityimage_timestamp");
            }
        }

        
        [AttributeLogicalName("entityimage_url")]
        public string? EntityImageURL
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("entityimage_url");
            }
        }

        /// <summary>
		/// For internal use only.
		/// </summary>
        [AttributeLogicalName("entityimageid")]
        public Guid? EntityImageId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<Guid?>("entityimageid");
            }
        }

        /// <summary>
		/// Flow group the flow is associated with.
		/// </summary>
        [AttributeLogicalName("flowgroup")]
        public EntityReference? FlowGroup
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<EntityReference?>("flowgroup");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("flowgroup", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier of the associated form.
		/// </summary>
        [AttributeLogicalName("formid")]
        public Guid? FormId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<Guid?>("formid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("formid", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Input parameters to the process.
		/// </summary>
        [AttributeLogicalName("inputparameters")]
        public string? InputParameters
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("inputparameters");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("inputparameters", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Inputs definition for this workflow.
		/// </summary>
        [AttributeLogicalName("inputs")]
        public string? Inputs
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("inputs");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("inputs", value);
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
		/// Indicates whether the process was created using the Microsoft Dynamics 365 Web application.
		/// </summary>
        [AttributeLogicalName("iscrmuiworkflow")]
        public bool? IsCrmUIWorkflow
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("iscrmuiworkflow");
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
		/// Defines whether other publishers can attach custom processing steps to this action
		/// </summary>
        [AttributeLogicalName("iscustomprocessingstepallowedforotherpublishers")]
        public BooleanManagedProperty? IsCustomProcessingStepAllowedForOtherPublishers
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<BooleanManagedProperty?>("iscustomprocessingstepallowedforotherpublishers");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("iscustomprocessingstepallowedforotherpublishers", value);
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
		/// Whether or not the steps in the process are executed in a single transaction.
		/// </summary>
        [AttributeLogicalName("istransacted")]
        public bool? IsTransacted
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("istransacted");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("istransacted", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Language of the process.
		/// </summary>
        [AttributeLogicalName("languagecode")]
        public int? LanguageCode
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("languagecode");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("languagecode", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// The user object that should be used to establish the license the flow should operate under.
		/// </summary>
        [AttributeLogicalName("licensee")]
        public EntityReference? Licensee
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<EntityReference?>("licensee");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("licensee", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// The source of the license entitlements.
		/// </summary>
        [AttributeLogicalName("licenseentitledby")]
        public EntityReference? LicenseEntitledBy
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<EntityReference?>("licenseentitledby");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("licenseentitledby", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Additional metadata for this workflow.
		/// </summary>
        [AttributeLogicalName("metadata")]
        public string? Metadata
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("metadata");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("metadata", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Shows the mode of the process.
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
		/// Type of the Modern Flow.
		/// </summary>
        [AttributeLogicalName("modernflowtype")]
        public OptionSetValue? ModernFlowType
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("modernflowtype");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("modernflowtype", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier of the user who last modified the process.
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
		/// Date and time when the process was last modified.
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
		/// Unique identifier of the delegate user who last modified the process.
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
		/// Flow modify metadata used for telemetry, etc.
		/// </summary>
        [AttributeLogicalName("modifymetadata")]
        public string? ModifyMetadata
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("modifymetadata");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("modifymetadata", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Name of the process.
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
		/// Indicates whether the process is able to run as an on-demand process.
		/// </summary>
        [AttributeLogicalName("ondemand")]
        public bool? OnDemand
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("ondemand");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("ondemand", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Outputs definition for this workflow.
		/// </summary>
        [AttributeLogicalName("outputs")]
        public string? Outputs
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("outputs");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("outputs", value);
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
		/// Unique identifier of the user or team who owns the process.
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
		/// Unique identifier of the business unit that owns the process.
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
		/// Unique identifier of the team who owns the process.
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
		/// Unique identifier of the user who owns the process.
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

        /// <summary>
		/// Unique identifier of the definition for process activation.
		/// </summary>
        [AttributeLogicalName("parentworkflowid")]
        public EntityReference? ParentWorkflowId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<EntityReference?>("parentworkflowid");
            }
        }

        /// <summary>
		/// For Internal Use Only.
		/// </summary>
        [AttributeLogicalName("planverified")]
        public bool? PlanVerified
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("planverified");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("planverified", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier of the plug-in type.
		/// </summary>
        [AttributeLogicalName("plugintypeid")]
        public EntityReference? PluginTypeId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<EntityReference?>("plugintypeid");
            }
        }

        /// <summary>
		/// Primary entity for the process. The process can be associated for one or more SDK operations defined on the primary entity.
		/// </summary>
        [AttributeLogicalName("primaryentity")]
        public string? PrimaryEntity
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("primaryentity");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("primaryentity", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Type the business process flow order.
		/// </summary>
        [AttributeLogicalName("processorder")]
        public int? ProcessOrder
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("processorder");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("processorder", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Contains the role assignment for the process.
		/// </summary>
        [AttributeLogicalName("processroleassignment")]
        public string? ProcessRoleAssignment
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("processroleassignment");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("processroleassignment", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier of the associated form for process trigger.
		/// </summary>
        [AttributeLogicalName("processtriggerformid")]
        public Guid? ProcessTriggerFormId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<Guid?>("processtriggerformid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("processtriggerformid", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Scope of the process trigger.
		/// </summary>
        [AttributeLogicalName("processtriggerscope")]
        public OptionSetValue? ProcessTriggerScope
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("processtriggerscope");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("processtriggerscope", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates the rank for order of execution for the synchronous workflow.
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
		/// The renderer type of Workflow
		/// </summary>
        [AttributeLogicalName("rendererobjecttypecode")]
        public string? RendererObjectTypeCode
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("rendererobjecttypecode");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("rendererobjecttypecode", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// For internal use only.
		/// </summary>
        [AttributeLogicalName("resourcecontainer")]
        public string? ResourceContainer
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("resourcecontainer");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("resourcecontainer", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// For internal use only.
		/// </summary>
        [AttributeLogicalName("resourceid")]
        public Guid? ResourceId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<Guid?>("resourceid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("resourceid", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Specifies the system user account under which a workflow executes.
		/// </summary>
        [AttributeLogicalName("runas")]
        public OptionSetValue? RunAs
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("runas");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("runas", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Schema version for this workflow.
		/// </summary>
        [AttributeLogicalName("schemaversion")]
        public string? SchemaVersion
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("schemaversion");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("schemaversion", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Scope of the process.
		/// </summary>
        [AttributeLogicalName("scope")]
        public OptionSetValue? Scope
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("scope");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("scope", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier of the SDK Message associated with this workflow.
		/// </summary>
        [AttributeLogicalName("sdkmessageid")]
        public EntityReference? SdkMessageId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<EntityReference?>("sdkmessageid");
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
		/// Status of the workflow
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
		/// Reason for the status of the workflow
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
		/// Indicates whether the process can be included in other processes as a child process.
		/// </summary>
        [AttributeLogicalName("subprocess")]
        public bool? Subprocess
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("subprocess");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("subprocess", value);
                OnPropertyChanged();
            }
        }

        
        [AttributeLogicalName("suspensionreasondetails")]
        public string? SuspensionReasonDetails
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("suspensionreasondetails");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("suspensionreasondetails", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Select whether synchronous workflow failures will be saved to log files.
		/// </summary>
        [AttributeLogicalName("syncworkflowlogonfailure")]
        public bool? SyncWorkflowLogOnFailure
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("syncworkflowlogonfailure");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("syncworkflowlogonfailure", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// The throttling behavior type.
		/// </summary>
        [AttributeLogicalName("throttlingbehavior")]
        public OptionSetValue? ThrottlingBehavior
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("throttlingbehavior");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("throttlingbehavior", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether the process will be triggered when the primary entity is created.
		/// </summary>
        [AttributeLogicalName("triggeroncreate")]
        public bool? TriggerOnCreate
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("triggeroncreate");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("triggeroncreate", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether the process will be triggered on deletion of the primary entity.
		/// </summary>
        [AttributeLogicalName("triggerondelete")]
        public bool? TriggerOnDelete
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("triggerondelete");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("triggerondelete", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Attributes that trigger the process when updated.
		/// </summary>
        [AttributeLogicalName("triggeronupdateattributelist")]
        public string? TriggerOnUpdateAttributeList
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("triggeronupdateattributelist");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("triggeronupdateattributelist", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// For Internal Use Only.
		/// </summary>
        [AttributeLogicalName("trustedaccess")]
        public bool? TrustedAccess
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("trustedaccess");
            }
        }

        /// <summary>
		/// Type of the process.
		/// </summary>
        [AttributeLogicalName("type")]
        public OptionSetValue? Type
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("type");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("type", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// For internal use only.
		/// </summary>
        [AttributeLogicalName("uidata")]
        public string? UIData
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("uidata");
            }
        }

        /// <summary>
		/// Type of the UI Flow process.
		/// </summary>
        [AttributeLogicalName("uiflowtype")]
        public OptionSetValue? UIFlowType
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("uiflowtype");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("uiflowtype", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique name of the process
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
		/// Select the stage a process will be triggered on update.
		/// </summary>
        [AttributeLogicalName("updatestage")]
        public OptionSetValue? UpdateStage
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("updatestage");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("updatestage", value);
                OnPropertyChanged();
            }
        }

        
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
		/// For internal use only.
		/// </summary>
        [AttributeLogicalName("workflowidunique")]
        public Guid? WorkflowIdUnique
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<Guid?>("workflowidunique");
            }
        }

        /// <summary>
		/// XAML that defines the process.
		/// </summary>
        [AttributeLogicalName("xaml")]
        public string? Xaml
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("xaml");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("xaml", value);
                OnPropertyChanged();
            }
        }
        #endregion

        #region NavigationProperties

        /// <summary>
        /// 1:N lk_asyncoperation_workflowactivationid
        /// </summary>
        [RelationshipSchemaName("lk_asyncoperation_workflowactivationid")]
        public IEnumerable<AsyncOperation> LkAsyncoperationWorkflowactivationid
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<AsyncOperation>("lk_asyncoperation_workflowactivationid", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_asyncoperation_workflowactivationid", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N process_processstage
        /// </summary>
        [RelationshipSchemaName("process_processstage")]
        public IEnumerable<ProcessStage> ProcessProcessstage
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<ProcessStage>("process_processstage", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("process_processstage", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N slabase_workflowid
        /// </summary>
        [RelationshipSchemaName("slabase_workflowid")]
        public IEnumerable<SLA> SlabaseWorkflowid
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<SLA>("slabase_workflowid", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("slabase_workflowid", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N workflow_active_workflow
        /// </summary>
        [RelationshipSchemaName("workflow_active_workflow")]
        public IEnumerable<Workflow> WorkflowActiveWorkflow
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<Workflow>("workflow_active_workflow", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("workflow_active_workflow", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N Workflow_licenseentitledby
        /// </summary>
        [RelationshipSchemaName("Workflow_licenseentitledby")]
        public IEnumerable<Workflow> WorkflowLicenseentitledby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<Workflow>("Workflow_licenseentitledby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("Workflow_licenseentitledby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N workflow_parent_workflow
        /// </summary>
        [RelationshipSchemaName("workflow_parent_workflow")]
        public IEnumerable<Workflow> WorkflowParentWorkflow
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<Workflow>("workflow_parent_workflow", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("workflow_parent_workflow", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N Workflow_routingrule
        /// </summary>
        [RelationshipSchemaName("Workflow_routingrule")]
        public IEnumerable<RoutingRule> WorkflowRoutingrule
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<RoutingRule>("Workflow_routingrule", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("Workflow_routingrule", null, value);
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
            public struct BusinessProcessType
            {
                public const int BusinessFlow = 0;
                public const int TaskFlow = 1;
            }
            public struct Category
            {
                public const int Workflow = 0;
                public const int Dialog = 1;
                public const int BusinessRule = 2;
                public const int Action = 3;
                public const int BusinessProcessFlow = 4;
                public const int ModernFlow = 5;
                public const int DesktopFlow = 6;
                public const int AIFlow = 7;
            }
            public struct ClientDataIsCompressed
            {
                public const bool WorkflowDoesNotHaveCompressedClientData = false;
                public const bool WorkflowHasCompressedClientData = true;
            }
            public struct ComponentState
            {
                public const int Published = 0;
                public const int Unpublished = 1;
                public const int Deleted = 2;
                public const int DeletedUnpublished = 3;
            }
            public struct CreateStage
            {
                public const int PreOperation = 20;
                public const int PostOperation = 40;
            }
            public struct DeleteStage
            {
                public const int PreOperation = 20;
                public const int PostOperation = 40;
            }
            public struct IsCrmUIWorkflow
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsManaged
            {
                public const bool Unmanaged = false;
                public const bool Managed = true;
            }
            public struct IsTransacted
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct Mode
            {
                public const int Background = 0;
                public const int RealTime = 1;
            }
            public struct ModernFlowType
            {
                public const int PowerAutomateFlow = 0;
                public const int CopilotStudioFlow = 1;
                public const int M365CopilotAgentFlow = 2;
            }
            public struct OnDemand
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct PlanVerified
            {
                public const bool NotVerified = false;
                public const bool Verified = true;
            }
            public struct ProcessTriggerScope
            {
                public const int Form = 1;
                public const int Entity = 2;
            }
            public struct RunAs
            {
                public const int Owner = 0;
                public const int CallingUser = 1;
            }
            public struct Scope
            {
                public const int User = 1;
                public const int BusinessUnit = 2;
                public const int ParentChildBusinessUnits = 3;
                public const int Organization = 4;
            }
            public struct StateCode
            {
                public const int Draft = 0;
                public const int Activated = 1;
                public const int Suspended = 2;
            }
            public struct StatusCode
            {
                public const int Draft = 1;
                public const int Activated = 2;
                public const int CompanyDLPViolation = 3;
            }
            public struct Subprocess
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct SyncWorkflowLogOnFailure
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct ThrottlingBehavior
            {
                public const int None = 0;
                public const int TenantPool = 1;
                public const int CopilotStudio = 2;
            }
            public struct TriggerOnCreate
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct TriggerOnDelete
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct TrustedAccess
            {
                public const bool WorkflowHasNotGoneThroughAccessCheck = false;
                public const bool WorkflowHasGoneThroughAccessCheck = true;
            }
            public struct Type
            {
                public const int Definition = 1;
                public const int Activation = 2;
                public const int Template = 3;
            }
            public struct UIFlowType
            {
                public const int WindowsRecorderV1 = 0;
                public const int SeleniumIDE = 1;
                public const int PowerAutomateDesktop = 2;
                public const int Test = 3;
                public const int Recording = 101;
            }
            public struct UpdateStage
            {
                public const int PreOperation = 20;
                public const int PostOperation = 40;
            }
        }
        #endregion

        #region LogicalNames
        public static partial class LogicalNames
        {
            public const string WorkflowId = "workflowid";
            public const string ActiveWorkflowId = "activeworkflowid";
            public const string AsyncAutoDelete = "asyncautodelete";
            public const string BillingContext = "billingcontext";
            public const string BusinessProcessType = "businessprocesstype";
            public const string Category = "category";
            public const string Claims = "claims";
            public const string ClientData = "clientdata";
            public const string ClientDataIsCompressed = "clientdataiscompressed";
            public const string ComponentState = "componentstate";
            public const string ConnectionReferences = "connectionreferences";
            public const string CreatedBy = "createdby";
            public const string CreatedOn = "createdon";
            public const string CreatedOnBehalfBy = "createdonbehalfby";
            public const string CreateMetadata = "createmetadata";
            public const string CreateStage = "createstage";
            public const string Credentials = "credentials";
            public const string Definition = "definition";
            public const string DeleteStage = "deletestage";
            public const string Dependencies = "dependencies";
            public const string Description = "description";
            public const string DesktopFlowModules = "desktopflowmodules";
            public const string DynamicsSolutionContext = "dynamicssolutioncontext";
            public const string EntityImage = "entityimage";
            public const string EntityImageTimestamp = "entityimage_timestamp";
            public const string EntityImageURL = "entityimage_url";
            public const string EntityImageId = "entityimageid";
            public const string FlowGroup = "flowgroup";
            public const string FormId = "formid";
            public const string InputParameters = "inputparameters";
            public const string Inputs = "inputs";
            public const string IntroducedVersion = "introducedversion";
            public const string IsCrmUIWorkflow = "iscrmuiworkflow";
            public const string IsCustomizable = "iscustomizable";
            public const string IsCustomProcessingStepAllowedForOtherPublishers = "iscustomprocessingstepallowedforotherpublishers";
            public const string IsManaged = "ismanaged";
            public const string IsTransacted = "istransacted";
            public const string LanguageCode = "languagecode";
            public const string Licensee = "licensee";
            public const string LicenseEntitledBy = "licenseentitledby";
            public const string Metadata = "metadata";
            public const string Mode = "mode";
            public const string ModernFlowType = "modernflowtype";
            public const string ModifiedBy = "modifiedby";
            public const string ModifiedOn = "modifiedon";
            public const string ModifiedOnBehalfBy = "modifiedonbehalfby";
            public const string ModifyMetadata = "modifymetadata";
            public const string Name = "name";
            public const string OnDemand = "ondemand";
            public const string Outputs = "outputs";
            public const string OverwriteTime = "overwritetime";
            public const string OwnerId = "ownerid";
            public const string OwningBusinessUnit = "owningbusinessunit";
            public const string OwningTeam = "owningteam";
            public const string OwningUser = "owninguser";
            public const string ParentWorkflowId = "parentworkflowid";
            public const string PlanVerified = "planverified";
            public const string PluginTypeId = "plugintypeid";
            public const string PrimaryEntity = "primaryentity";
            public const string ProcessOrder = "processorder";
            public const string ProcessRoleAssignment = "processroleassignment";
            public const string ProcessTriggerFormId = "processtriggerformid";
            public const string ProcessTriggerScope = "processtriggerscope";
            public const string Rank = "rank";
            public const string RendererObjectTypeCode = "rendererobjecttypecode";
            public const string ResourceContainer = "resourcecontainer";
            public const string ResourceId = "resourceid";
            public const string RunAs = "runas";
            public const string SchemaVersion = "schemaversion";
            public const string Scope = "scope";
            public const string SdkMessageId = "sdkmessageid";
            public const string SolutionId = "solutionid";
            public const string StateCode = "statecode";
            public const string StatusCode = "statuscode";
            public const string Subprocess = "subprocess";
            public const string SuspensionReasonDetails = "suspensionreasondetails";
            public const string SyncWorkflowLogOnFailure = "syncworkflowlogonfailure";
            public const string ThrottlingBehavior = "throttlingbehavior";
            public const string TriggerOnCreate = "triggeroncreate";
            public const string TriggerOnDelete = "triggerondelete";
            public const string TriggerOnUpdateAttributeList = "triggeronupdateattributelist";
            public const string TrustedAccess = "trustedaccess";
            public const string Type = "type";
            public const string UIData = "uidata";
            public const string UIFlowType = "uiflowtype";
            public const string UniqueName = "uniquename";
            public const string UpdateStage = "updatestage";
            public const string VersionNumber = "versionnumber";
            public const string WorkflowIdUnique = "workflowidunique";
            public const string Xaml = "xaml";
        }
        #endregion

        #region Relations
        public static partial class Relations
        {
            public static class OneToMany
            {
                public const string AdxInvitationRedemptionworkflow = "adx_invitation_redemptionworkflow";
                public const string AIPluginOperationWorkflowWorkflow = "AIPluginOperation_Workflow_Workflow";
                public const string CatalogassignmentWorkflow = "catalogassignment_workflow";
                public const string CommentArtifactWorkflow = "Comment_Artifact_Workflow";
                public const string ConvertruleitembaseWorkflowid = "convertruleitembase_workflowid";
                public const string FlowcapacityassignmentWorkflow = "flowcapacityassignment_workflow";
                public const string FloweventWorkflow = "flowevent_workflow";
                public const string FlowtriggerinstanceWorkflowidWorkflow = "flowtriggerinstance_workflowid_workflow";
                public const string LkAsyncoperationWorkflowactivationid = "lk_asyncoperation_workflowactivationid";
                public const string LkExpiredprocessProcessid = "lk_expiredprocess_processid";
                public const string LkNewprocessProcessid = "lk_newprocess_processid";
                public const string LkProcesssessionProcessid = "lk_processsession_processid";
                public const string LkTranslationprocessProcessid = "lk_translationprocess_processid";
                public const string MsdynRetrainworkflowMsdynToaimodel = "msdyn_retrainworkflow_msdyn_toaimodel";
                public const string MsdynScheduleinferenceworkflowMsdynToaimodel = "msdyn_scheduleinferenceworkflow_msdyn_toaimodel";
                public const string MsdynWorkflowMsdynPmrecording = "msdyn_workflow_msdyn_pmrecording";
                public const string MsdynWorkflowMsdynSolutionhealthruleResolutionaction = "msdyn_workflow_msdyn_solutionhealthrule_resolutionaction";
                public const string MsdynWorkflowMsdynSolutionhealthruleWorkflow = "msdyn_workflow_msdyn_solutionhealthrule_Workflow";
                public const string MsdynWorkflowSlaitemCustomtimecalculationworkflowid = "msdyn_workflow_slaitem_customtimecalculationworkflowid";
                public const string ProcessProcessstage = "process_processstage";
                public const string ProcessProcesstrigger = "process_processtrigger";
                public const string RegardingobjectidProcess = "regardingobjectid_process";
                public const string SavingruleWorkflow = "savingrule_workflow";
                public const string SlabaseWorkflowid = "slabase_workflowid";
                public const string SlaitembaseWorkflowid = "slaitembase_workflowid";
                public const string TaggedprocessProcessWorkflow = "taggedprocess_Process_workflow";
                public const string UserentityinstancedataWorkflow = "userentityinstancedata_workflow";
                public const string WorkflowActiveWorkflow = "workflow_active_workflow";
                public const string WorkflowAnnotation = "Workflow_Annotation";
                public const string WorkflowBusinessprocess = "workflow_businessprocess";
                public const string WorkflowComponentchangesetversions = "workflow_componentchangesetversions";
                public const string WorkflowComponentversionnrddatasourceset = "workflow_componentversionnrddatasourceset";
                public const string WorkflowComponentversions = "workflow_componentversions";
                public const string WorkflowDependencies = "workflow_dependencies";
                public const string WorkflowDesktopflowbinaryProcess = "workflow_desktopflowbinary_Process";
                public const string WorkflowFlowaggregationWorkflowid = "workflow_flowaggregation_workflowid";
                public const string WorkflowFlowlogCloudflowid = "workflow_flowlog_cloudflowid";
                public const string WorkflowFlowlogDesktopflowid = "workflow_flowlog_desktopflowid";
                public const string WorkflowFlowrunWorkflow = "workflow_flowrun_Workflow";
                public const string WorkflowLicenseentitledby = "Workflow_licenseentitledby";
                public const string WorkflowParentWorkflow = "workflow_parent_workflow";
                public const string WorkflowRoutingrule = "Workflow_routingrule";
                public const string WorkflowSyncErrors = "Workflow_SyncErrors";
                public const string WorkflowWorkflowbinaryProcess = "workflow_workflowbinary_Process";
                public const string WorkflowidConvertrule = "workflowid_convertrule";
                public const string WorkflowidProfilerule = "workflowid_profilerule";
                public const string WorkflowmetadataWorkflowIdWorkflow = "workflowmetadata_WorkflowId_workflow";
            }

            public static partial class ManyToOne
            {
                public const string BusinessUnitWorkflow = "business_unit_workflow";
                public const string OwnerWorkflows = "owner_workflows";
                public const string SystemUserWorkflow = "system_user_workflow";
                public const string TeamWorkflow = "team_workflow";
                public const string WorkflowActiveWorkflow = "workflow_active_workflow";
                public const string WorkflowCreatedby = "workflow_createdby";
                public const string WorkflowCreatedonbehalfby = "workflow_createdonbehalfby";
                public const string WorkflowEntityimage = "workflow_entityimage";
                public const string WorkflowFlowgroup = "Workflow_flowgroup";
                public const string WorkflowLicensee = "Workflow_licensee";
                public const string WorkflowLicenseentitledby = "Workflow_licenseentitledby";
                public const string WorkflowModifiedby = "workflow_modifiedby";
                public const string WorkflowModifiedonbehalfby = "workflow_modifiedonbehalfby";
                public const string WorkflowParentWorkflow = "workflow_parent_workflow";
            }

            public static partial class ManyToMany
            {
                public const string BotcomponentWorkflow = "botcomponent_workflow";
                public const string WorkflowCardConnections = "workflow_card_connections";
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

        public static Workflow Retrieve(IOrganizationService service, Guid id)
        {
            return Retrieve(service, id, new ColumnSet(true));
        }

        public static Workflow Retrieve(IOrganizationService service, Guid id, ColumnSet columnSet)
        {
            return service.Retrieve("workflow", id, columnSet).ToEntity<Workflow>();
        }

        public Workflow GetChangedEntity()
        {
            if (!_trackChanges)
            {
                return this;
            }
                var attr = new AttributeCollection();
            foreach (var attrName in _changedProperties.Value.Select(changedProperty => ((AttributeLogicalNameAttribute) GetType().GetProperty(changedProperty)!.GetCustomAttribute(typeof(AttributeLogicalNameAttribute))!).LogicalName).Where(attrName => Contains(attrName)))
            {
                attr.Add(attrName, this[attrName]);
            }
            return new Workflow(Id) { Attributes = attr };
        }
        #endregion
    }

    #region Context
    public partial class DataContext
    {
        public IQueryable<Workflow> WorkflowSet
        {
            get
            {
                return CreateQuery<Workflow>();
            }
        }
    }
    #endregion
}
