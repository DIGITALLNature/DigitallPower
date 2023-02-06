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
	/// A component of a CRM solution.
	/// </summary>
	[DataContractAttribute()]
	[EntityLogicalNameAttribute("solutioncomponent")]
	[System.CodeDom.Compiler.GeneratedCode("dgtp", "2023")]
    [ExcludeFromCodeCoverage]
	public partial class SolutionComponent : Entity, INotifyPropertyChanging, INotifyPropertyChanged
    {
	    #region ctor
		[DebuggerNonUserCode]
		public SolutionComponent() : this(false)
        {
        }

        [DebuggerNonUserCode]
		public SolutionComponent(bool trackChanges = false) : base(EntityLogicalName)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public SolutionComponent(Guid id, bool trackChanges = false) : base(EntityLogicalName,id)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public SolutionComponent(KeyAttributeCollection keyAttributes, bool trackChanges = false) : base(EntityLogicalName,keyAttributes)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public SolutionComponent(string keyName, object keyValue, bool trackChanges = false) : base(EntityLogicalName, keyName, keyValue)
        {
			_trackChanges = trackChanges;
        }
        #endregion

		#region fields
        private readonly bool _trackChanges;
        private readonly Lazy<HashSet<string>> _changedProperties = new Lazy<HashSet<string>>();
        #endregion

        #region consts
        public const string EntityLogicalName = "solutioncomponent";
        public const int EntityTypeCode = 7103;
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
		[AttributeLogicalNameAttribute("solutioncomponentid")]
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
				base.Id = value;	
			}
		}

		/// <summary>
		/// Unique identifier of the solution component.
		/// </summary>
		[AttributeLogicalName("solutioncomponentid")]
        public Guid? SolutionComponentId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<Guid?>("solutioncomponentid");
            }
        }

		/// <summary>
		/// The object type code of the component.
		/// </summary>
		[AttributeLogicalName("componenttype")]
        public OptionSetValue? ComponentType
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<OptionSetValue?>("componenttype");
            }
        }

		/// <summary>
		/// Unique identifier of the user who created the solution
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
		/// Date and time when the solution was created.
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
		/// Unique identifier of the delegate user who created the solution.
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
		/// Indicates whether this component is metadata or data.
		/// </summary>
		[AttributeLogicalName("ismetadata")]
        public bool? IsMetadata
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<bool?>("ismetadata");
            }
        }

		/// <summary>
		/// Unique identifier of the user who last modified the solution.
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
		/// Date and time when the solution was last modified.
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
		/// Unique identifier of the delegate user who modified the solution.
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
		/// Unique identifier of the object with which the component is associated.
		/// </summary>
		[AttributeLogicalName("objectid")]
        public Guid? ObjectId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<Guid?>("objectid");
            }
        }

		/// <summary>
		/// Indicates the include behavior of the root component.
		/// </summary>
		[AttributeLogicalName("rootcomponentbehavior")]
        public OptionSetValue? RootComponentBehavior
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<OptionSetValue?>("rootcomponentbehavior");
            }
        }

		/// <summary>
		/// The parent ID of the subcomponent, which will be a root
		/// </summary>
		[AttributeLogicalName("rootsolutioncomponentid")]
        public Guid? RootSolutionComponentId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<Guid?>("rootsolutioncomponentid");
            }
        }

		/// <summary>
		/// Unique identifier of the solution.
		/// </summary>
		[AttributeLogicalName("solutionid")]
        public EntityReference? SolutionId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<EntityReference?>("solutionid");
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


		#endregion

		#region NavigationProperties
		/// <summary>
		/// 1:N solutioncomponent_parent_solutioncomponent
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("solutioncomponent_parent_solutioncomponent")]
		public System.Collections.Generic.IEnumerable<SolutionComponent> SolutioncomponentParentSolutioncomponent
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<SolutionComponent>("solutioncomponent_parent_solutioncomponent", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("SolutioncomponentParentSolutioncomponent");
				this.SetRelatedEntities<SolutionComponent>("solutioncomponent_parent_solutioncomponent", null, value);
				this.OnPropertyChanged("SolutioncomponentParentSolutioncomponent");
			}
		}

		#endregion

		#region Options
		public static class Options
		{
			    public struct ComponentType
                {
					public const int Entity = 1;
					public const int Attribute = 2;
					public const int Relationship = 3;
					public const int AttributePicklistValue = 4;
					public const int AttributeLookupValue = 5;
					public const int ViewAttribute = 6;
					public const int LocalizedLabel = 7;
					public const int RelationshipExtraCondition = 8;
					public const int OptionSet = 9;
					public const int EntityRelationship = 10;
					public const int EntityRelationshipRole = 11;
					public const int EntityRelationshipRelationships = 12;
					public const int ManagedProperty = 13;
					public const int EntityKey = 14;
					public const int Privilege = 16;
					public const int PrivilegeObjectTypeCode = 17;
					public const int Index = 18;
					public const int Role = 20;
					public const int RolePrivilege = 21;
					public const int DisplayString = 22;
					public const int DisplayStringMap = 23;
					public const int Form = 24;
					public const int Organization = 25;
					public const int SavedQuery = 26;
					public const int Workflow = 29;
					public const int Report = 31;
					public const int ReportEntity = 32;
					public const int ReportCategory = 33;
					public const int ReportVisibility = 34;
					public const int Attachment = 35;
					public const int EmailTemplate = 36;
					public const int ContractTemplate = 37;
					public const int KBArticleTemplate = 38;
					public const int MailMergeTemplate = 39;
					public const int DuplicateRule = 44;
					public const int DuplicateRuleCondition = 45;
					public const int EntityMap = 46;
					public const int AttributeMap = 47;
					public const int RibbonCommand = 48;
					public const int RibbonContextGroup = 49;
					public const int RibbonCustomization = 50;
					public const int RibbonRule = 52;
					public const int RibbonTabToCommandMap = 53;
					public const int RibbonDiff = 55;
					public const int SavedQueryVisualization = 59;
					public const int SystemForm = 60;
					public const int WebResource = 61;
					public const int SiteMap = 62;
					public const int ConnectionRole = 63;
					public const int ComplexControl = 64;
					public const int HierarchyRule = 65;
					public const int CustomControl = 66;
					public const int CustomControlDefaultConfig = 68;
					public const int FieldSecurityProfile = 70;
					public const int FieldPermission = 71;
					public const int PluginType = 90;
					public const int PluginAssembly = 91;
					public const int SDKMessageProcessingStep = 92;
					public const int SDKMessageProcessingStepImage = 93;
					public const int ServiceEndpoint = 95;
					public const int RoutingRule = 150;
					public const int RoutingRuleItem = 151;
					public const int SLA = 152;
					public const int SLAItem = 153;
					public const int ConvertRule = 154;
					public const int ConvertRuleItem = 155;
					public const int MobileOfflineProfile = 161;
					public const int MobileOfflineProfileItem = 162;
					public const int SimilarityRule = 165;
					public const int DataSourceMapping = 166;
					public const int SDKMessage = 201;
					public const int SDKMessageFilter = 202;
					public const int SdkMessagePair = 203;
					public const int SdkMessageRequest = 204;
					public const int SdkMessageRequestField = 205;
					public const int SdkMessageResponse = 206;
					public const int SdkMessageResponseField = 207;
					public const int ImportMap = 208;
					public const int WebWizard = 210;
					public const int CanvasApp = 300;
					public const int Connector = 371;
					public const int Connector_ = 372;
					public const int EnvironmentVariableDefinition = 380;
					public const int EnvironmentVariableValue = 381;
					public const int AIProjectType = 400;
					public const int AIProject = 401;
					public const int AIConfiguration = 402;
					public const int EntityAnalyticsConfiguration = 430;
					public const int AttributeImageConfiguration = 431;
					public const int EntityImageConfiguration = 432;
                }
                public struct IsMetadata
                {
                    public const bool Data = false;
                    public const bool Metadata = true;
                }
			    public struct RootComponentBehavior
                {
					public const int IncludeSubcomponents = 0;
					public const int DoNotIncludeSubcomponents = 1;
					public const int IncludeAsShellOnly = 2;
                }
		}
		#endregion

		#region LogicalNames
		public static class LogicalNames
		{
				public const string SolutionComponentId = "solutioncomponentid";
				public const string ComponentType = "componenttype";
				public const string CreatedBy = "createdby";
				public const string CreatedOn = "createdon";
				public const string CreatedOnBehalfBy = "createdonbehalfby";
				public const string IsMetadata = "ismetadata";
				public const string ModifiedBy = "modifiedby";
				public const string ModifiedOn = "modifiedon";
				public const string ModifiedOnBehalfBy = "modifiedonbehalfby";
				public const string ObjectId = "objectid";
				public const string RootComponentBehavior = "rootcomponentbehavior";
				public const string RootSolutionComponentId = "rootsolutioncomponentid";
				public const string SolutionId = "solutionid";
				public const string VersionNumber = "versionnumber";
		}
		#endregion

		#region Relations
        public static class Relations
        {
            public static class OneToMany
            {
				public const string SolutioncomponentParentSolutioncomponent = "solutioncomponent_parent_solutioncomponent";
				public const string UserentityinstancedataSolutioncomponent = "userentityinstancedata_solutioncomponent";
            }

            public static class ManyToOne
            {
				public const string LkSolutioncomponentbaseCreatedonbehalfby = "lk_solutioncomponentbase_createdonbehalfby";
				public const string LkSolutioncomponentbaseModifiedonbehalfby = "lk_solutioncomponentbase_modifiedonbehalfby";
				public const string SolutionSolutioncomponent = "solution_solutioncomponent";
				public const string SolutioncomponentParentSolutioncomponent = "solutioncomponent_parent_solutioncomponent";
            }

            public static class ManyToMany
            {
            }
        }

        #endregion

		#region Methods
        public static SolutionComponent Retrieve(IOrganizationService service, Guid id)
        {
            return Retrieve(service,id, new ColumnSet(true));
        }

        public static SolutionComponent Retrieve(IOrganizationService service, Guid id, ColumnSet columnSet)
        {
            return service.Retrieve("solutioncomponent", id, columnSet).ToEntity<SolutionComponent>();
        }

        public SolutionComponent GetChangedEntity()
        {
            if (_trackChanges)
            {
                var attr = new AttributeCollection();
                foreach (var attrName in _changedProperties.Value.Select(changedProperty => ((AttributeLogicalNameAttribute) GetType().GetProperty(changedProperty)!.GetCustomAttribute(typeof (AttributeLogicalNameAttribute))!).LogicalName).Where(attrName => Contains(attrName)))
                {
                    attr.Add(attrName,this[attrName]);
                }
                return new  SolutionComponent(Id) {Attributes = attr };
            }
            return this;
        }
        #endregion
	}

	#region Context
	public partial class DataContext
	{
		public IQueryable<SolutionComponent> SolutionComponentSet
		{
			get
			{
				return CreateQuery<SolutionComponent>();
			}
		}
	}
	#endregion
}
