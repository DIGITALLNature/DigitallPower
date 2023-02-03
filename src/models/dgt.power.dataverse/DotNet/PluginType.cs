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
	/// Type that inherits from the IPlugin interface and is contained within a plug-in assembly.
	/// </summary>
	[DataContractAttribute()]
	[EntityLogicalNameAttribute("plugintype")]
	[System.CodeDom.Compiler.GeneratedCode("dgtp", "2023")]
    [ExcludeFromCodeCoverage]
	public partial class PluginType : Entity, INotifyPropertyChanging, INotifyPropertyChanged
    {
	    #region ctor
		[DebuggerNonUserCode]
		public PluginType() : this(false)
        {
        }

        [DebuggerNonUserCode]
		public PluginType(bool trackChanges = false) : base(EntityLogicalName)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public PluginType(Guid id, bool trackChanges = false) : base(EntityLogicalName,id)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public PluginType(KeyAttributeCollection keyAttributes, bool trackChanges = false) : base(EntityLogicalName,keyAttributes)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public PluginType(string keyName, object keyValue, bool trackChanges = false) : base(EntityLogicalName, keyName, keyValue)
        {
			_trackChanges = trackChanges;
        }
        #endregion

		#region fields
        private readonly bool _trackChanges;
        private readonly Lazy<HashSet<string>> _changedProperties = new Lazy<HashSet<string>>();
        #endregion

        #region consts
        public const string EntityLogicalName = "plugintype";
        public const string PrimaryNameAttribute = "name";
        public const int EntityTypeCode = 4602;
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
		[AttributeLogicalNameAttribute("plugintypeid")]
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
				PluginTypeId = value;
			}
		}

		/// <summary>
		/// Unique identifier of the plug-in type.
		/// </summary>
		[AttributeLogicalName("plugintypeid")]
        public Guid? PluginTypeId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<Guid?>("plugintypeid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(PluginTypeId));
                SetAttributeValue("plugintypeid", value);
				if (value.HasValue)
				{
					base.Id = value.Value;
				}
				else
				{
					base.Id = System.Guid.Empty;
				}
                OnPropertyChanged(nameof(PluginTypeId));
            }
        }

		/// <summary>
		/// Full path name of the plug-in assembly.
		/// </summary>
		[AttributeLogicalName("assemblyname")]
        public string? AssemblyName
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("assemblyname");
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
		/// Unique identifier of the user who created the plug-in type.
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
		/// Date and time when the plug-in type was created.
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
		/// Unique identifier of the delegate user who created the plugintype.
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
		/// Culture code for the plug-in assembly.
		/// </summary>
		[AttributeLogicalName("culture")]
        public string? Culture
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("culture");
            }
        }

		/// <summary>
		/// Customization level of the plug-in type.
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
		/// Serialized Custom Activity Type information, including required arguments. For more information, see SandboxCustomActivityInfo.
		/// </summary>
		[AttributeLogicalName("customworkflowactivityinfo")]
        public string? CustomWorkflowActivityInfo
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("customworkflowactivityinfo");
            }
        }

		/// <summary>
		/// Description of the plug-in type.
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
		/// User friendly name for the plug-in.
		/// </summary>
		[AttributeLogicalName("friendlyname")]
        public string? FriendlyName
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("friendlyname");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(FriendlyName));
                SetAttributeValue("friendlyname", value);
                OnPropertyChanged(nameof(FriendlyName));
            }
        }

		
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
		/// Indicates if the plug-in is a custom activity for workflows.
		/// </summary>
		[AttributeLogicalName("isworkflowactivity")]
        public bool? IsWorkflowActivity
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<bool?>("isworkflowactivity");
            }
        }

		/// <summary>
		/// Major of the version number of the assembly for the plug-in type.
		/// </summary>
		[AttributeLogicalName("major")]
        public int? Major
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<int?>("major");
            }
        }

		/// <summary>
		/// Minor of the version number of the assembly for the plug-in type.
		/// </summary>
		[AttributeLogicalName("minor")]
        public int? Minor
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<int?>("minor");
            }
        }

		/// <summary>
		/// Unique identifier of the user who last modified the plug-in type.
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
		/// Date and time when the plug-in type was last modified.
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
		/// Unique identifier of the delegate user who last modified the plugintype.
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
		/// Name of the plug-in type.
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
		/// Unique identifier of the organization with which the plug-in type is associated.
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
		/// Unique identifier of the plug-in assembly that contains this plug-in type.
		/// </summary>
		[AttributeLogicalName("pluginassemblyid")]
        public EntityReference? PluginAssemblyId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<EntityReference?>("pluginassemblyid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(PluginAssemblyId));
                SetAttributeValue("pluginassemblyid", value);
                OnPropertyChanged(nameof(PluginAssemblyId));
            }
        }

		/// <summary>
		/// Unique identifier of the plug-in type.
		/// </summary>
		[AttributeLogicalName("plugintypeidunique")]
        public Guid? PluginTypeIdUnique
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<Guid?>("plugintypeidunique");
            }
        }

		/// <summary>
		/// Public key token of the assembly for the plug-in type.
		/// </summary>
		[AttributeLogicalName("publickeytoken")]
        public string? PublicKeyToken
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("publickeytoken");
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
		/// Fully qualified type name of the plug-in type.
		/// </summary>
		[AttributeLogicalName("typename")]
        public string? TypeName
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("typename");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(TypeName));
                SetAttributeValue("typename", value);
                OnPropertyChanged(nameof(TypeName));
            }
        }

		/// <summary>
		/// Version number of the assembly for the plug-in type.
		/// </summary>
		[AttributeLogicalName("version")]
        public string? Version
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("version");
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
		/// Group name of workflow custom activity.
		/// </summary>
		[AttributeLogicalName("workflowactivitygroupname")]
        public string? WorkflowActivityGroupName
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("workflowactivitygroupname");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(WorkflowActivityGroupName));
                SetAttributeValue("workflowactivitygroupname", value);
                OnPropertyChanged(nameof(WorkflowActivityGroupName));
            }
        }


		#endregion

		#region NavigationProperties
		/// <summary>
		/// 1:N plugintype_customapi
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("plugintype_customapi")]
		public System.Collections.Generic.IEnumerable<CustomAPI> PlugintypeCustomapi
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<CustomAPI>("plugintype_customapi", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("PlugintypeCustomapi");
				this.SetRelatedEntities<CustomAPI>("plugintype_customapi", null, value);
				this.OnPropertyChanged("PlugintypeCustomapi");
			}
		}

		/// <summary>
		/// 1:N plugintype_sdkmessageprocessingstep
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("plugintype_sdkmessageprocessingstep")]
		public System.Collections.Generic.IEnumerable<SdkMessageProcessingStep> PlugintypeSdkmessageprocessingstep
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<SdkMessageProcessingStep>("plugintype_sdkmessageprocessingstep", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("PlugintypeSdkmessageprocessingstep");
				this.SetRelatedEntities<SdkMessageProcessingStep>("plugintype_sdkmessageprocessingstep", null, value);
				this.OnPropertyChanged("PlugintypeSdkmessageprocessingstep");
			}
		}

		/// <summary>
		/// 1:N plugintypeid_sdkmessageprocessingstep
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("plugintypeid_sdkmessageprocessingstep")]
		public System.Collections.Generic.IEnumerable<SdkMessageProcessingStep> PlugintypeidSdkmessageprocessingstep
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<SdkMessageProcessingStep>("plugintypeid_sdkmessageprocessingstep", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("PlugintypeidSdkmessageprocessingstep");
				this.SetRelatedEntities<SdkMessageProcessingStep>("plugintypeid_sdkmessageprocessingstep", null, value);
				this.OnPropertyChanged("PlugintypeidSdkmessageprocessingstep");
			}
		}

		#endregion

		#region Options
		public static class Options
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
                public struct IsWorkflowActivity
                {
                    public const bool No = false;
                    public const bool Yes = true;
                }
		}
		#endregion

		#region LogicalNames
		public static class LogicalNames
		{
				public const string PluginTypeId = "plugintypeid";
				public const string AssemblyName = "assemblyname";
				public const string ComponentState = "componentstate";
				public const string CreatedBy = "createdby";
				public const string CreatedOn = "createdon";
				public const string CreatedOnBehalfBy = "createdonbehalfby";
				public const string Culture = "culture";
				public const string CustomizationLevel = "customizationlevel";
				public const string CustomWorkflowActivityInfo = "customworkflowactivityinfo";
				public const string Description = "description";
				public const string FriendlyName = "friendlyname";
				public const string IsManaged = "ismanaged";
				public const string IsWorkflowActivity = "isworkflowactivity";
				public const string Major = "major";
				public const string Minor = "minor";
				public const string ModifiedBy = "modifiedby";
				public const string ModifiedOn = "modifiedon";
				public const string ModifiedOnBehalfBy = "modifiedonbehalfby";
				public const string Name = "name";
				public const string OrganizationId = "organizationid";
				public const string OverwriteTime = "overwritetime";
				public const string PluginAssemblyId = "pluginassemblyid";
				public const string PluginTypeIdUnique = "plugintypeidunique";
				public const string PublicKeyToken = "publickeytoken";
				public const string SolutionId = "solutionid";
				public const string TypeName = "typename";
				public const string Version = "version";
				public const string VersionNumber = "versionnumber";
				public const string WorkflowActivityGroupName = "workflowactivitygroupname";
		}
		#endregion

		#region Relations
        public static class Relations
        {
            public static class OneToMany
            {
				public const string PluginTypeService = "plugin_type_service";
				public const string PlugintypeCustomapi = "plugintype_customapi";
				public const string PlugintypePlugintypestatistic = "plugintype_plugintypestatistic";
				public const string PlugintypeSdkmessageprocessingstep = "plugintype_sdkmessageprocessingstep";
				public const string PlugintypeidSdkmessageprocessingstep = "plugintypeid_sdkmessageprocessingstep";
				public const string UserentityinstancedataPlugintype = "userentityinstancedata_plugintype";
            }

            public static class ManyToOne
            {
				public const string CreatedbyPlugintype = "createdby_plugintype";
				public const string LkPlugintypeCreatedonbehalfby = "lk_plugintype_createdonbehalfby";
				public const string LkPlugintypeModifiedonbehalfby = "lk_plugintype_modifiedonbehalfby";
				public const string ModifiedbyPlugintype = "modifiedby_plugintype";
				public const string OrganizationPlugintype = "organization_plugintype";
				public const string PluginassemblyPlugintype = "pluginassembly_plugintype";
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
        public static PluginType Retrieve(IOrganizationService service, Guid id)
        {
            return Retrieve(service,id, new ColumnSet(true));
        }

        public static PluginType Retrieve(IOrganizationService service, Guid id, ColumnSet columnSet)
        {
            return service.Retrieve("plugintype", id, columnSet).ToEntity<PluginType>();
        }

        public PluginType GetChangedEntity()
        {
            if (_trackChanges)
            {
                var attr = new AttributeCollection();
                foreach (var attrName in _changedProperties.Value.Select(changedProperty => ((AttributeLogicalNameAttribute) GetType().GetProperty(changedProperty)!.GetCustomAttribute(typeof (AttributeLogicalNameAttribute))!).LogicalName).Where(attrName => Contains(attrName)))
                {
                    attr.Add(attrName,this[attrName]);
                }
                return new  PluginType(Id) {Attributes = attr };
            }
            return this;
        }
        #endregion
	}

	#region Context
	public partial class DataContext
	{
		public IQueryable<PluginType> PluginTypeSet
		{
			get
			{
				return CreateQuery<PluginType>();
			}
		}
	}
	#endregion
}
