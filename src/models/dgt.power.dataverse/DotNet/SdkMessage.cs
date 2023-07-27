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
	/// Message that is supported by the SDK.
	/// </summary>
	[DataContractAttribute()]
	[EntityLogicalNameAttribute("sdkmessage")]
	[System.CodeDom.Compiler.GeneratedCode("dgtp", "2023")]
    [ExcludeFromCodeCoverage]
	public partial class SdkMessage : Entity, INotifyPropertyChanging, INotifyPropertyChanged
    {
	    #region ctor
		[DebuggerNonUserCode]
		public SdkMessage() : this(false)
        {
        }

        [DebuggerNonUserCode]
		public SdkMessage(bool trackChanges = false) : base(EntityLogicalName)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public SdkMessage(Guid id, bool trackChanges = false) : base(EntityLogicalName,id)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public SdkMessage(KeyAttributeCollection keyAttributes, bool trackChanges = false) : base(EntityLogicalName,keyAttributes)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public SdkMessage(string keyName, object keyValue, bool trackChanges = false) : base(EntityLogicalName, keyName, keyValue)
        {
			_trackChanges = trackChanges;
        }
        #endregion

		#region fields
        private readonly bool _trackChanges;
        private readonly Lazy<HashSet<string>> _changedProperties = new Lazy<HashSet<string>>();
        #endregion

        #region consts
        public const string EntityLogicalName = "sdkmessage";
        public const string PrimaryNameAttribute = "name";
        public const int EntityTypeCode = 4606;
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
		[AttributeLogicalNameAttribute("sdkmessageid")]
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
				SdkMessageId = value;
			}
		}

		/// <summary>
		/// Unique identifier of the SDK message entity.
		/// </summary>
		[AttributeLogicalName("sdkmessageid")]
        public Guid? SdkMessageId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<Guid?>("sdkmessageid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(SdkMessageId));
                SetAttributeValue("sdkmessageid", value);
				if (value.HasValue)
				{
					base.Id = value.Value;
				}
				else
				{
					base.Id = System.Guid.Empty;
				}
                OnPropertyChanged(nameof(SdkMessageId));
            }
        }

		/// <summary>
		/// Information about whether the SDK message is automatically transacted.
		/// </summary>
		[AttributeLogicalName("autotransact")]
        public bool? AutoTransact
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<bool?>("autotransact");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(AutoTransact));
                SetAttributeValue("autotransact", value);
                OnPropertyChanged(nameof(AutoTransact));
            }
        }

		/// <summary>
		/// Identifies where a method will be exposed. 0 - Server, 1 - Client, 2 - both.
		/// </summary>
		[AttributeLogicalName("availability")]
        public int? Availability
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<int?>("availability");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(Availability));
                SetAttributeValue("availability", value);
                OnPropertyChanged(nameof(Availability));
            }
        }

		/// <summary>
		/// If this is a categorized method, this is the name, otherwise None.
		/// </summary>
		[AttributeLogicalName("categoryname")]
        public string? CategoryName
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("categoryname");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(CategoryName));
                SetAttributeValue("categoryname", value);
                OnPropertyChanged(nameof(CategoryName));
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
		/// Unique identifier of the user who created the SDK message.
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
		/// Date and time when the SDK message was created.
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
		/// Unique identifier of the delegate user who created the sdkmessage.
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
		/// Customization level of the SDK message.
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
		/// Name of the privilege that allows execution of the SDK message
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
                OnPropertyChanging(nameof(ExecutePrivilegeName));
                SetAttributeValue("executeprivilegename", value);
                OnPropertyChanged(nameof(ExecutePrivilegeName));
            }
        }

		/// <summary>
		/// Indicates whether the SDK message should have its requests expanded per primary entity defined in its filters.
		/// </summary>
		[AttributeLogicalName("expand")]
        public bool? Expand
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<bool?>("expand");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(Expand));
                SetAttributeValue("expand", value);
                OnPropertyChanged(nameof(Expand));
            }
        }

		/// <summary>
		/// Version in which the component is introduced.
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
		/// Information about whether the SDK message is active.
		/// </summary>
		[AttributeLogicalName("isactive")]
        public bool? IsActive
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<bool?>("isactive");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(IsActive));
                SetAttributeValue("isactive", value);
                OnPropertyChanged(nameof(IsActive));
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
		/// Indicates whether the SDK message is private.
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
                OnPropertyChanging(nameof(IsPrivate));
                SetAttributeValue("isprivate", value);
                OnPropertyChanged(nameof(IsPrivate));
            }
        }

		/// <summary>
		/// Identifies whether an SDK message will be ReadOnly or Read Write. false - ReadWrite, true - ReadOnly .
		/// </summary>
		[AttributeLogicalName("isreadonly")]
        public bool? IsReadOnly
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<bool?>("isreadonly");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(IsReadOnly));
                SetAttributeValue("isreadonly", value);
                OnPropertyChanged(nameof(IsReadOnly));
            }
        }

		/// <summary>
		/// For internal use only.
		/// </summary>
		[AttributeLogicalName("isvalidforexecuteasync")]
        public bool? IsValidForExecuteAsync
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<bool?>("isvalidforexecuteasync");
            }
        }

		/// <summary>
		/// Unique identifier of the user who last modified the SDK message.
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
		/// Date and time when the SDK message was last modified.
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
		/// Unique identifier of the delegate user who last modified the sdkmessage.
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
		/// Name of the SDK message.
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
		/// Unique identifier of the organization with which the SDK message is associated.
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
		/// Unique identifier of the SDK message.
		/// </summary>
		[AttributeLogicalName("sdkmessageidunique")]
        public Guid? SdkMessageIdUnique
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<Guid?>("sdkmessageidunique");
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
		/// Indicates whether the SDK message is a template.
		/// </summary>
		[AttributeLogicalName("template")]
        public bool? Template
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<bool?>("template");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(Template));
                SetAttributeValue("template", value);
                OnPropertyChanged(nameof(Template));
            }
        }

		/// <summary>
		/// For internal use only.
		/// </summary>
		[AttributeLogicalName("throttlesettings")]
        public string? ThrottleSettings
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("throttlesettings");
            }
        }

		/// <summary>
		/// Number that identifies a specific revision of the SDK message.
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
		/// Whether or not the SDK message can be called from a workflow.
		/// </summary>
		[AttributeLogicalName("workflowsdkstepenabled")]
        public bool? WorkflowSdkStepEnabled
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<bool?>("workflowsdkstepenabled");
            }
        }


		#endregion

		#region NavigationProperties
		/// <summary>
		/// 1:N sdkmessage_customapi
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("sdkmessage_customapi")]
		public System.Collections.Generic.IEnumerable<CustomAPI> SdkmessageCustomapi
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<CustomAPI>("sdkmessage_customapi", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("SdkmessageCustomapi");
				this.SetRelatedEntities<CustomAPI>("sdkmessage_customapi", null, value);
				this.OnPropertyChanged("SdkmessageCustomapi");
			}
		}

		/// <summary>
		/// 1:N sdkmessageid_sdkmessagefilter
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("sdkmessageid_sdkmessagefilter")]
		public System.Collections.Generic.IEnumerable<SdkMessageFilter> SdkmessageidSdkmessagefilter
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<SdkMessageFilter>("sdkmessageid_sdkmessagefilter", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("SdkmessageidSdkmessagefilter");
				this.SetRelatedEntities<SdkMessageFilter>("sdkmessageid_sdkmessagefilter", null, value);
				this.OnPropertyChanged("SdkmessageidSdkmessagefilter");
			}
		}

		/// <summary>
		/// 1:N sdkmessageid_sdkmessageprocessingstep
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("sdkmessageid_sdkmessageprocessingstep")]
		public System.Collections.Generic.IEnumerable<SdkMessageProcessingStep> SdkmessageidSdkmessageprocessingstep
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<SdkMessageProcessingStep>("sdkmessageid_sdkmessageprocessingstep", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("SdkmessageidSdkmessageprocessingstep");
				this.SetRelatedEntities<SdkMessageProcessingStep>("sdkmessageid_sdkmessageprocessingstep", null, value);
				this.OnPropertyChanged("SdkmessageidSdkmessageprocessingstep");
			}
		}

		#endregion

		#region Options
		public static class Options
		{
                public struct AutoTransact
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
                public struct Expand
                {
                    public const bool No = false;
                    public const bool Yes = true;
                }
                public struct IsActive
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
                public struct IsReadOnly
                {
                    public const bool No = false;
                    public const bool Yes = true;
                }
                public struct IsValidForExecuteAsync
                {
                    public const bool No = false;
                    public const bool Yes = true;
                }
                public struct Template
                {
                    public const bool No = false;
                    public const bool Yes = true;
                }
                public struct WorkflowSdkStepEnabled
                {
                    public const bool No = false;
                    public const bool Yes = true;
                }
		}
		#endregion

		#region LogicalNames
		public static class LogicalNames
		{
				public const string SdkMessageId = "sdkmessageid";
				public const string AutoTransact = "autotransact";
				public const string Availability = "availability";
				public const string CategoryName = "categoryname";
				public const string ComponentState = "componentstate";
				public const string CreatedBy = "createdby";
				public const string CreatedOn = "createdon";
				public const string CreatedOnBehalfBy = "createdonbehalfby";
				public const string CustomizationLevel = "customizationlevel";
				public const string ExecutePrivilegeName = "executeprivilegename";
				public const string Expand = "expand";
				public const string IntroducedVersion = "introducedversion";
				public const string IsActive = "isactive";
				public const string IsManaged = "ismanaged";
				public const string IsPrivate = "isprivate";
				public const string IsReadOnly = "isreadonly";
				public const string IsValidForExecuteAsync = "isvalidforexecuteasync";
				public const string ModifiedBy = "modifiedby";
				public const string ModifiedOn = "modifiedon";
				public const string ModifiedOnBehalfBy = "modifiedonbehalfby";
				public const string Name = "name";
				public const string OrganizationId = "organizationid";
				public const string OverwriteTime = "overwritetime";
				public const string SdkMessageIdUnique = "sdkmessageidunique";
				public const string SolutionId = "solutionid";
				public const string Template = "template";
				public const string ThrottleSettings = "throttlesettings";
				public const string VersionNumber = "versionnumber";
				public const string WorkflowSdkStepEnabled = "workflowsdkstepenabled";
		}
		#endregion

		#region Relations
        public static class Relations
        {
            public static class OneToMany
            {
				public const string MessageSdkmessagepair = "message_sdkmessagepair";
				public const string SdkmessageCustomapi = "sdkmessage_customapi";
				public const string SdkmessageServiceplanmapping = "sdkmessage_serviceplanmapping";
				public const string SdkmessageidSdkmessagefilter = "sdkmessageid_sdkmessagefilter";
				public const string SdkmessageidSdkmessageprocessingstep = "sdkmessageid_sdkmessageprocessingstep";
				public const string SdkmessageidWorkflowDependency = "sdkmessageid_workflow_dependency";
				public const string UserentityinstancedataSdkmessage = "userentityinstancedata_sdkmessage";
            }

            public static class ManyToOne
            {
				public const string CreatedbySdkmessage = "createdby_sdkmessage";
				public const string LkSdkmessageCreatedonbehalfby = "lk_sdkmessage_createdonbehalfby";
				public const string LkSdkmessageModifiedonbehalfby = "lk_sdkmessage_modifiedonbehalfby";
				public const string ModifiedbySdkmessage = "modifiedby_sdkmessage";
				public const string OrganizationSdkmessage = "organization_sdkmessage";
            }

            public static class ManyToMany
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
        public static SdkMessage Retrieve(IOrganizationService service, Guid id)
        {
            return Retrieve(service,id, new ColumnSet(true));
        }

        public static SdkMessage Retrieve(IOrganizationService service, Guid id, ColumnSet columnSet)
        {
            return service.Retrieve("sdkmessage", id, columnSet).ToEntity<SdkMessage>();
        }

        public SdkMessage GetChangedEntity()
        {
            if (_trackChanges)
            {
                var attr = new AttributeCollection();
                foreach (var attrName in _changedProperties.Value.Select(changedProperty => ((AttributeLogicalNameAttribute) GetType().GetProperty(changedProperty)!.GetCustomAttribute(typeof (AttributeLogicalNameAttribute))!).LogicalName).Where(attrName => Contains(attrName)))
                {
                    attr.Add(attrName,this[attrName]);
                }
                return new  SdkMessage(Id) {Attributes = attr };
            }
            return this;
        }
        #endregion
	}

	#region Context
	public partial class DataContext
	{
		public IQueryable<SdkMessage> SdkMessageSet
		{
			get
			{
				return CreateQuery<SdkMessage>();
			}
		}
	}
	#endregion
}
