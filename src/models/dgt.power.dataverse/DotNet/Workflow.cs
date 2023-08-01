// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics.CodeAnalysis;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Reflection;
using System.Runtime.Serialization;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query; 
using AttributeCollection = Microsoft.Xrm.Sdk.AttributeCollection;

// ReSharper disable All
namespace dgt.power.dataverse
{
	/// <inheritdoc />
	/// <summary>
	/// Set of logical rules that define the steps necessary to automate a specific business process, task, or set of actions to be performed.
	/// </summary>
	[DataContractAttribute()]
	[EntityLogicalNameAttribute("workflow")]
	[System.CodeDom.Compiler.GeneratedCode("dgtp", "2023")]
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
		public Workflow(Guid id, bool trackChanges = false) : base(EntityLogicalName,id)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public Workflow(KeyAttributeCollection keyAttributes, bool trackChanges = false) : base(EntityLogicalName,keyAttributes)
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
        private readonly Lazy<HashSet<string>> _changedProperties = new Lazy<HashSet<string>>();
        #endregion

        #region consts
        public const string EntityLogicalName = "workflow";
        public const string PrimaryNameAttribute = "name";
        public const int EntityTypeCode = 4703;
        #endregion

        #region Events
        public event PropertyChangedEventHandler? PropertyChanged;
        public event PropertyChangingEventHandler? PropertyChanging;

        [DebuggerNonUserCode]
		private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null) PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
		[AttributeLogicalNameAttribute("workflowid")]
		public new System.Guid Id
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
                OnPropertyChanging(nameof(WorkflowId));
                SetAttributeValue("workflowid", value);
				if (value.HasValue)
				{
					base.Id = value.Value;
				}
				else
				{
					base.Id = System.Guid.Empty;
				}
                OnPropertyChanged(nameof(WorkflowId));
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
                OnPropertyChanging(nameof(AsyncAutoDelete));
                SetAttributeValue("asyncautodelete", value);
                OnPropertyChanged(nameof(AsyncAutoDelete));
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
                OnPropertyChanging(nameof(BillingContext));
                SetAttributeValue("billingcontext", value);
                OnPropertyChanged(nameof(BillingContext));
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
                OnPropertyChanging(nameof(BusinessProcessType));
                SetAttributeValue("businessprocesstype", value);
                OnPropertyChanged(nameof(BusinessProcessType));
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
                OnPropertyChanging(nameof(Category));
                SetAttributeValue("category", value);
                OnPropertyChanged(nameof(Category));
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
                OnPropertyChanging(nameof(ClientData));
                SetAttributeValue("clientdata", value);
                OnPropertyChanged(nameof(ClientData));
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
                OnPropertyChanging(nameof(ConnectionReferences));
                SetAttributeValue("connectionreferences", value);
                OnPropertyChanged(nameof(ConnectionReferences));
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
                OnPropertyChanging(nameof(CreateStage));
                SetAttributeValue("createstage", value);
                OnPropertyChanged(nameof(CreateStage));
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
                OnPropertyChanging(nameof(Definition));
                SetAttributeValue("definition", value);
                OnPropertyChanged(nameof(Definition));
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
                OnPropertyChanging(nameof(DeleteStage));
                SetAttributeValue("deletestage", value);
                OnPropertyChanged(nameof(DeleteStage));
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
                OnPropertyChanging(nameof(Dependencies));
                SetAttributeValue("dependencies", value);
                OnPropertyChanged(nameof(Dependencies));
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
                OnPropertyChanging(nameof(Description));
                SetAttributeValue("description", value);
                OnPropertyChanged(nameof(Description));
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
                OnPropertyChanging(nameof(DesktopFlowModules));
                SetAttributeValue("desktopflowmodules", value);
                OnPropertyChanged(nameof(DesktopFlowModules));
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
                OnPropertyChanging(nameof(DynamicsSolutionContext));
                SetAttributeValue("dynamicssolutioncontext", value);
                OnPropertyChanged(nameof(DynamicsSolutionContext));
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
                OnPropertyChanging(nameof(EntityImage));
                SetAttributeValue("entityimage", value);
                OnPropertyChanged(nameof(EntityImage));
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
                OnPropertyChanging(nameof(FormId));
                SetAttributeValue("formid", value);
                OnPropertyChanged(nameof(FormId));
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
                OnPropertyChanging(nameof(InputParameters));
                SetAttributeValue("inputparameters", value);
                OnPropertyChanged(nameof(InputParameters));
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
                OnPropertyChanging(nameof(Inputs));
                SetAttributeValue("inputs", value);
                OnPropertyChanged(nameof(Inputs));
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
                OnPropertyChanging(nameof(IntroducedVersion));
                SetAttributeValue("introducedversion", value);
                OnPropertyChanged(nameof(IntroducedVersion));
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
                OnPropertyChanging(nameof(IsCustomizable));
                SetAttributeValue("iscustomizable", value);
                OnPropertyChanged(nameof(IsCustomizable));
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
                OnPropertyChanging(nameof(IsCustomProcessingStepAllowedForOtherPublishers));
                SetAttributeValue("iscustomprocessingstepallowedforotherpublishers", value);
                OnPropertyChanged(nameof(IsCustomProcessingStepAllowedForOtherPublishers));
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
                OnPropertyChanging(nameof(IsTransacted));
                SetAttributeValue("istransacted", value);
                OnPropertyChanged(nameof(IsTransacted));
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
                OnPropertyChanging(nameof(LanguageCode));
                SetAttributeValue("languagecode", value);
                OnPropertyChanged(nameof(LanguageCode));
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
                OnPropertyChanging(nameof(Metadata));
                SetAttributeValue("metadata", value);
                OnPropertyChanged(nameof(Metadata));
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
                OnPropertyChanging(nameof(Mode));
                SetAttributeValue("mode", value);
                OnPropertyChanged(nameof(Mode));
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
                OnPropertyChanging(nameof(Name));
                SetAttributeValue("name", value);
                OnPropertyChanged(nameof(Name));
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
                OnPropertyChanging(nameof(OnDemand));
                SetAttributeValue("ondemand", value);
                OnPropertyChanged(nameof(OnDemand));
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
                OnPropertyChanging(nameof(Outputs));
                SetAttributeValue("outputs", value);
                OnPropertyChanged(nameof(Outputs));
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
                OnPropertyChanging(nameof(OwnerId));
                SetAttributeValue("ownerid", value);
                OnPropertyChanged(nameof(OwnerId));
            }
        }

		
		[AttributeLogicalName("owneridtype")]
        public string? OwnerIdType
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("owneridtype");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(OwnerIdType));
                SetAttributeValue("owneridtype", value);
                OnPropertyChanged(nameof(OwnerIdType));
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
                OnPropertyChanging(nameof(PrimaryEntity));
                SetAttributeValue("primaryentity", value);
                OnPropertyChanged(nameof(PrimaryEntity));
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
                OnPropertyChanging(nameof(ProcessOrder));
                SetAttributeValue("processorder", value);
                OnPropertyChanged(nameof(ProcessOrder));
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
                OnPropertyChanging(nameof(ProcessRoleAssignment));
                SetAttributeValue("processroleassignment", value);
                OnPropertyChanged(nameof(ProcessRoleAssignment));
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
                OnPropertyChanging(nameof(ProcessTriggerFormId));
                SetAttributeValue("processtriggerformid", value);
                OnPropertyChanged(nameof(ProcessTriggerFormId));
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
                OnPropertyChanging(nameof(ProcessTriggerScope));
                SetAttributeValue("processtriggerscope", value);
                OnPropertyChanged(nameof(ProcessTriggerScope));
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
                OnPropertyChanging(nameof(Rank));
                SetAttributeValue("rank", value);
                OnPropertyChanged(nameof(Rank));
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
                OnPropertyChanging(nameof(RendererObjectTypeCode));
                SetAttributeValue("rendererobjecttypecode", value);
                OnPropertyChanged(nameof(RendererObjectTypeCode));
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
                OnPropertyChanging(nameof(RunAs));
                SetAttributeValue("runas", value);
                OnPropertyChanged(nameof(RunAs));
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
                OnPropertyChanging(nameof(SchemaVersion));
                SetAttributeValue("schemaversion", value);
                OnPropertyChanged(nameof(SchemaVersion));
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
                OnPropertyChanging(nameof(Scope));
                SetAttributeValue("scope", value);
                OnPropertyChanged(nameof(Scope));
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
                OnPropertyChanging(nameof(StateCode));
                SetAttributeValue("statecode", value);
                OnPropertyChanged(nameof(StateCode));
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
                OnPropertyChanging(nameof(StatusCode));
                SetAttributeValue("statuscode", value);
                OnPropertyChanged(nameof(StatusCode));
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
                OnPropertyChanging(nameof(Subprocess));
                SetAttributeValue("subprocess", value);
                OnPropertyChanged(nameof(Subprocess));
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
                OnPropertyChanging(nameof(SuspensionReasonDetails));
                SetAttributeValue("suspensionreasondetails", value);
                OnPropertyChanged(nameof(SuspensionReasonDetails));
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
                OnPropertyChanging(nameof(SyncWorkflowLogOnFailure));
                SetAttributeValue("syncworkflowlogonfailure", value);
                OnPropertyChanged(nameof(SyncWorkflowLogOnFailure));
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
                OnPropertyChanging(nameof(TriggerOnCreate));
                SetAttributeValue("triggeroncreate", value);
                OnPropertyChanged(nameof(TriggerOnCreate));
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
                OnPropertyChanging(nameof(TriggerOnDelete));
                SetAttributeValue("triggerondelete", value);
                OnPropertyChanged(nameof(TriggerOnDelete));
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
                OnPropertyChanging(nameof(TriggerOnUpdateAttributeList));
                SetAttributeValue("triggeronupdateattributelist", value);
                OnPropertyChanged(nameof(TriggerOnUpdateAttributeList));
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
                OnPropertyChanging(nameof(Type));
                SetAttributeValue("type", value);
                OnPropertyChanged(nameof(Type));
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
                OnPropertyChanging(nameof(UIFlowType));
                SetAttributeValue("uiflowtype", value);
                OnPropertyChanged(nameof(UIFlowType));
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
                OnPropertyChanging(nameof(UniqueName));
                SetAttributeValue("uniquename", value);
                OnPropertyChanged(nameof(UniqueName));
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
                OnPropertyChanging(nameof(UpdateStage));
                SetAttributeValue("updatestage", value);
                OnPropertyChanged(nameof(UpdateStage));
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
                OnPropertyChanging(nameof(Xaml));
                SetAttributeValue("xaml", value);
                OnPropertyChanged(nameof(Xaml));
            }
        }


		#endregion

		#region NavigationProperties
		/// <summary>
		/// 1:N lk_asyncoperation_workflowactivationid
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_asyncoperation_workflowactivationid")]
		public System.Collections.Generic.IEnumerable<AsyncOperation> LkAsyncoperationWorkflowactivationid
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<AsyncOperation>("lk_asyncoperation_workflowactivationid", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkAsyncoperationWorkflowactivationid");
				this.SetRelatedEntities<AsyncOperation>("lk_asyncoperation_workflowactivationid", null, value);
				this.OnPropertyChanged("LkAsyncoperationWorkflowactivationid");
			}
		}

		/// <summary>
		/// 1:N process_processstage
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("process_processstage")]
		public System.Collections.Generic.IEnumerable<ProcessStage> ProcessProcessstage
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<ProcessStage>("process_processstage", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("ProcessProcessstage");
				this.SetRelatedEntities<ProcessStage>("process_processstage", null, value);
				this.OnPropertyChanged("ProcessProcessstage");
			}
		}

		/// <summary>
		/// 1:N slabase_workflowid
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("slabase_workflowid")]
		public System.Collections.Generic.IEnumerable<SLA> SlabaseWorkflowid
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<SLA>("slabase_workflowid", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("SlabaseWorkflowid");
				this.SetRelatedEntities<SLA>("slabase_workflowid", null, value);
				this.OnPropertyChanged("SlabaseWorkflowid");
			}
		}

		/// <summary>
		/// 1:N workflow_active_workflow
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("workflow_active_workflow")]
		public System.Collections.Generic.IEnumerable<Workflow> WorkflowActiveWorkflow
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Workflow>("workflow_active_workflow", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("WorkflowActiveWorkflow");
				this.SetRelatedEntities<Workflow>("workflow_active_workflow", null, value);
				this.OnPropertyChanged("WorkflowActiveWorkflow");
			}
		}

		/// <summary>
		/// 1:N workflow_parent_workflow
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("workflow_parent_workflow")]
		public System.Collections.Generic.IEnumerable<Workflow> WorkflowParentWorkflow
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Workflow>("workflow_parent_workflow", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("WorkflowParentWorkflow");
				this.SetRelatedEntities<Workflow>("workflow_parent_workflow", null, value);
				this.OnPropertyChanged("WorkflowParentWorkflow");
			}
		}

		/// <summary>
		/// 1:N Workflow_routingrule
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("Workflow_routingrule")]
		public System.Collections.Generic.IEnumerable<RoutingRule> WorkflowRoutingrule
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<RoutingRule>("Workflow_routingrule", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("WorkflowRoutingrule");
				this.SetRelatedEntities<RoutingRule>("Workflow_routingrule", null, value);
				this.OnPropertyChanged("WorkflowRoutingrule");
			}
		}

		#endregion

		#region Options
		public static class Options
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
					public const int Workflow_ = 0;
					public const int Dialog = 1;
					public const int BusinessRule = 2;
					public const int Action = 3;
					public const int BusinessProcessFlow = 4;
					public const int ModernFlow = 5;
					public const int DesktopFlow = 6;
					public const int WebClientAPIFlow = 9000;
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
                public struct OnDemand
                {
                    public const bool No = false;
                    public const bool Yes = true;
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
					public const int Parent_ChildBusinessUnits = 3;
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
					public const int WindowsRecorder_V1_ = 0;
					public const int SeleniumIDE = 1;
					public const int PowerAutomateDesktop = 2;
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
		public static class LogicalNames
		{
				public const string WorkflowId = "workflowid";
				public const string ActiveWorkflowId = "activeworkflowid";
				public const string AsyncAutoDelete = "asyncautodelete";
				public const string BillingContext = "billingcontext";
				public const string BusinessProcessType = "businessprocesstype";
				public const string Category = "category";
				public const string ClientData = "clientdata";
				public const string ComponentState = "componentstate";
				public const string ConnectionReferences = "connectionreferences";
				public const string CreatedBy = "createdby";
				public const string CreatedOn = "createdon";
				public const string CreatedOnBehalfBy = "createdonbehalfby";
				public const string CreateStage = "createstage";
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
				public const string Metadata = "metadata";
				public const string Mode = "mode";
				public const string ModifiedBy = "modifiedby";
				public const string ModifiedOn = "modifiedon";
				public const string ModifiedOnBehalfBy = "modifiedonbehalfby";
				public const string Name = "name";
				public const string OnDemand = "ondemand";
				public const string Outputs = "outputs";
				public const string OverwriteTime = "overwritetime";
				public const string OwnerId = "ownerid";
				public const string OwnerIdType = "owneridtype";
				public const string OwningBusinessUnit = "owningbusinessunit";
				public const string OwningTeam = "owningteam";
				public const string OwningUser = "owninguser";
				public const string ParentWorkflowId = "parentworkflowid";
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
        public static class Relations
        {
            public static class OneToMany
            {
				public const string CatalogassignmentWorkflow = "catalogassignment_workflow";
				public const string CommentArtifactWorkflow = "Comment_Artifact_Workflow";
				public const string ConvertruleitembaseWorkflowid = "convertruleitembase_workflowid";
				public const string LkAsyncoperationWorkflowactivationid = "lk_asyncoperation_workflowactivationid";
				public const string LkEc4uGdprBpfCorrectionProcessid = "lk_ec4u_gdpr_bpf_correction_processid";
				public const string LkEc4uGdprBpfDeletionProcessid = "lk_ec4u_gdpr_bpf_deletion_processid";
				public const string LkEc4uGdprBpfInformationProcessid = "lk_ec4u_gdpr_bpf_information_processid";
				public const string LkExpiredprocessProcessid = "lk_expiredprocess_processid";
				public const string LkLeadtoopportunitysalesprocessProcessid = "lk_leadtoopportunitysalesprocess_processid";
				public const string LkMsdynIottocaseprocessProcessid = "lk_msdyn_iottocaseprocess_processid";
				public const string LkNewprocessProcessid = "lk_newprocess_processid";
				public const string LkOpportunitysalesprocessProcessid = "lk_opportunitysalesprocess_processid";
				public const string LkPhonetocaseprocessProcessid = "lk_phonetocaseprocess_processid";
				public const string LkProcesssessionProcessid = "lk_processsession_processid";
				public const string LkTranslationprocessProcessid = "lk_translationprocess_processid";
				public const string MsdynRetrainworkflowMsdynToaimodel = "msdyn_retrainworkflow_msdyn_toaimodel";
				public const string MsdynScheduleinferenceworkflowMsdynToaimodel = "msdyn_scheduleinferenceworkflow_msdyn_toaimodel";
				public const string MsdynWorkflowMsdynEntityroutingconfigurationDeroutingProcess = "msdyn_workflow_msdyn_entityroutingconfiguration_DeroutingProcess";
				public const string MsdynWorkflowMsdynEntityroutingconfigurationRoutingProcess = "msdyn_workflow_msdyn_entityroutingconfiguration_RoutingProcess";
				public const string MsdynWorkflowMsdynMacrosessionMacroname = "msdyn_workflow_msdyn_macrosession_macroname";
				public const string MsdynWorkflowMsdynPmrecording = "msdyn_workflow_msdyn_pmrecording";
				public const string MsdynWorkflowMsdynProdAgentscriptstepMacroactionid = "msdyn_workflow_msdyn_prod_agentscriptstep_macroactionid";
				public const string MsdynWorkflowMsdynSolutionhealthruleResolutionaction = "msdyn_workflow_msdyn_solutionhealthrule_resolutionaction";
				public const string MsdynWorkflowMsdynSolutionhealthruleWorkflow = "msdyn_workflow_msdyn_solutionhealthrule_Workflow";
				public const string MsdynWorkflowMsdynTimespentBusinessprocessflow = "msdyn_workflow_msdyn_timespent_businessprocessflow";
				public const string MsdynWorkflowSlaitemCustomtimecalculationworkflowid = "msdyn_workflow_slaitem_customtimecalculationworkflowid";
				public const string ProcessProcessstage = "process_processstage";
				public const string ProcessProcesstrigger = "process_processtrigger";
				public const string RegardingobjectidProcess = "regardingobjectid_process";
				public const string SlabaseWorkflowid = "slabase_workflowid";
				public const string SlaitembaseWorkflowid = "slaitembase_workflowid";
				public const string UserentityinstancedataWorkflow = "userentityinstancedata_workflow";
				public const string WorkflowActiveWorkflow = "workflow_active_workflow";
				public const string WorkflowAnnotation = "Workflow_Annotation";
				public const string WorkflowDependencies = "workflow_dependencies";
				public const string WorkflowDesktopflowbinaryProcess = "workflow_desktopflowbinary_Process";
				public const string WorkflowParentWorkflow = "workflow_parent_workflow";
				public const string WorkflowRoutingrule = "Workflow_routingrule";
				public const string WorkflowSyncErrors = "Workflow_SyncErrors";
				public const string WorkflowWorkflowbinaryProcess = "workflow_workflowbinary_Process";
				public const string WorkflowidConvertrule = "workflowid_convertrule";
				public const string WorkflowidProfilerule = "workflowid_profilerule";
            }

            public static class ManyToOne
            {
				public const string BusinessUnitWorkflow = "business_unit_workflow";
				public const string OwnerWorkflows = "owner_workflows";
				public const string SystemUserWorkflow = "system_user_workflow";
				public const string TeamWorkflow = "team_workflow";
				public const string WorkflowActiveWorkflow = "workflow_active_workflow";
				public const string WorkflowCreatedby = "workflow_createdby";
				public const string WorkflowCreatedonbehalfby = "workflow_createdonbehalfby";
				public const string WorkflowEntityimage = "workflow_entityimage";
				public const string WorkflowModifiedby = "workflow_modifiedby";
				public const string WorkflowModifiedonbehalfby = "workflow_modifiedonbehalfby";
				public const string WorkflowParentWorkflow = "workflow_parent_workflow";
            }

            public static class ManyToMany
            {
				public const string BotcomponentWorkflow = "botcomponent_workflow";
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
            return Retrieve(service,id, new ColumnSet(true));
        }

        public static Workflow Retrieve(IOrganizationService service, Guid id, ColumnSet columnSet)
        {
            return service.Retrieve("workflow", id, columnSet).ToEntity<Workflow>();
        }

        public Workflow GetChangedEntity()
        {
            if (_trackChanges)
            {
                var attr = new AttributeCollection();
                foreach (var attrName in _changedProperties.Value.Select(changedProperty => ((AttributeLogicalNameAttribute) GetType().GetProperty(changedProperty)!.GetCustomAttribute(typeof (AttributeLogicalNameAttribute))!).LogicalName).Where(attrName => Contains(attrName)))
                {
                    attr.Add(attrName,this[attrName]);
                }
                return new  Workflow(Id) {Attributes = attr };
            }
            return this;
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
