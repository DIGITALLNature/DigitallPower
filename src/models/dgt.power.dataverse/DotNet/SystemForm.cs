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
	/// Organization-owned entity customizations including form layout and dashboards.
	/// </summary>
	[DataContractAttribute()]
	[EntityLogicalNameAttribute("systemform")]
	[System.CodeDom.Compiler.GeneratedCode("dgtp", "2023")]
    [ExcludeFromCodeCoverage]
	public partial class SystemForm : Entity, INotifyPropertyChanging, INotifyPropertyChanged
    {
	    #region ctor
		[DebuggerNonUserCode]
		public SystemForm() : this(false)
        {
        }

        [DebuggerNonUserCode]
		public SystemForm(bool trackChanges = false) : base(EntityLogicalName)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public SystemForm(Guid id, bool trackChanges = false) : base(EntityLogicalName,id)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public SystemForm(KeyAttributeCollection keyAttributes, bool trackChanges = false) : base(EntityLogicalName,keyAttributes)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public SystemForm(string keyName, object keyValue, bool trackChanges = false) : base(EntityLogicalName, keyName, keyValue)
        {
			_trackChanges = trackChanges;
        }
        #endregion

		#region fields
        private readonly bool _trackChanges;
        private readonly Lazy<HashSet<string>> _changedProperties = new Lazy<HashSet<string>>();
        #endregion

        #region consts
        public const string EntityLogicalName = "systemform";
        public const int EntityTypeCode = 1030;
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
		[AttributeLogicalNameAttribute("formid")]
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
				FormId = value;
			}
		}

		/// <summary>
		/// Unique identifier of the record type form.
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
				if (value.HasValue)
				{
					base.Id = value.Value;
				}
				else
				{
					base.Id = System.Guid.Empty;
				}
                OnPropertyChanged(nameof(FormId));
            }
        }

		/// <summary>
		/// Unique identifier of the parent form.
		/// </summary>
		[AttributeLogicalName("ancestorformid")]
        public EntityReference? AncestorFormId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<EntityReference?>("ancestorformid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(AncestorFormId));
                SetAttributeValue("ancestorformid", value);
                OnPropertyChanged(nameof(AncestorFormId));
            }
        }

		/// <summary>
		/// Information that specifies whether this component can be deleted.
		/// </summary>
		[AttributeLogicalName("canbedeleted")]
        public BooleanManagedProperty? CanBeDeleted
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<BooleanManagedProperty?>("canbedeleted");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(CanBeDeleted));
                SetAttributeValue("canbedeleted", value);
                OnPropertyChanged(nameof(CanBeDeleted));
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
		/// Description of the form or dashboard.
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
		/// Specifies the state of the form.
		/// </summary>
		[AttributeLogicalName("formactivationstate")]
        public OptionSetValue? FormActivationState
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<OptionSetValue?>("formactivationstate");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(FormActivationState));
                SetAttributeValue("formactivationstate", value);
                OnPropertyChanged(nameof(FormActivationState));
            }
        }

		/// <summary>
		/// Unique identifier of the form used when synchronizing customizations for the Microsoft Dynamics 365 client for Outlook.
		/// </summary>
		[AttributeLogicalName("formidunique")]
        public Guid? FormIdUnique
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<Guid?>("formidunique");
            }
        }

		/// <summary>
		/// Json representation of the form layout.
		/// </summary>
		[AttributeLogicalName("formjson")]
        public string? FormJson
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("formjson");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(FormJson));
                SetAttributeValue("formjson", value);
                OnPropertyChanged(nameof(FormJson));
            }
        }

		/// <summary>
		/// Specifies whether this form is in the updated UI layout in Microsoft Dynamics CRM 2015 or Microsoft Dynamics CRM Online 2015 Update.
		/// </summary>
		[AttributeLogicalName("formpresentation")]
        public OptionSetValue? FormPresentation
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<OptionSetValue?>("formpresentation");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(FormPresentation));
                SetAttributeValue("formpresentation", value);
                OnPropertyChanged(nameof(FormPresentation));
            }
        }

		/// <summary>
		/// XML representation of the form layout.
		/// </summary>
		[AttributeLogicalName("formxml")]
        public string? FormXml
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("formxml");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(FormXml));
                SetAttributeValue("formxml", value);
                OnPropertyChanged(nameof(FormXml));
            }
        }

		/// <summary>
		/// formXml diff as in a managed solution. for internal use only
		/// </summary>
		[AttributeLogicalName("formxmlmanaged")]
        public string? FormXmlManaged
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("formxmlmanaged");
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
		/// Specifies whether this form is merged with the updated UI layout in Microsoft Dynamics CRM 2015 or Microsoft Dynamics CRM Online 2015 Update.
		/// </summary>
		[AttributeLogicalName("isairmerged")]
        public bool? IsAIRMerged
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<bool?>("isairmerged");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(IsAIRMerged));
                SetAttributeValue("isairmerged", value);
                OnPropertyChanged(nameof(IsAIRMerged));
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
		/// Information that specifies whether the form or the dashboard is the system default.
		/// </summary>
		[AttributeLogicalName("isdefault")]
        public bool? IsDefault
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<bool?>("isdefault");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(IsDefault));
                SetAttributeValue("isdefault", value);
                OnPropertyChanged(nameof(IsDefault));
            }
        }

		/// <summary>
		/// Information that specifies whether the dashboard is enabled for desktop.
		/// </summary>
		[AttributeLogicalName("isdesktopenabled")]
        public bool? IsDesktopEnabled
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<bool?>("isdesktopenabled");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(IsDesktopEnabled));
                SetAttributeValue("isdesktopenabled", value);
                OnPropertyChanged(nameof(IsDesktopEnabled));
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
		/// Information that specifies whether the dashboard is enabled for tablet.
		/// </summary>
		[AttributeLogicalName("istabletenabled")]
        public bool? IsTabletEnabled
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<bool?>("istabletenabled");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(IsTabletEnabled));
                SetAttributeValue("istabletenabled", value);
                OnPropertyChanged(nameof(IsTabletEnabled));
            }
        }

		/// <summary>
		/// Name of the form.
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
		/// Code that represents the record type.
		/// </summary>
		[AttributeLogicalName("objecttypecode")]
        public string? ObjectTypeCode
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("objecttypecode");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(ObjectTypeCode));
                SetAttributeValue("objecttypecode", value);
                OnPropertyChanged(nameof(ObjectTypeCode));
            }
        }

		/// <summary>
		/// Unique identifier of the organization.
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

		
		[AttributeLogicalName("publishedon")]
        public DateTime? PublishedOn
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<DateTime?>("publishedon");
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
		/// Type of the form, for example, Dashboard or Preview.
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
		/// Unique Name
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
		/// For internal use only.
		/// </summary>
		[AttributeLogicalName("version")]
        public int? Version
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<int?>("version");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(Version));
                SetAttributeValue("version", value);
                OnPropertyChanged(nameof(Version));
            }
        }

		/// <summary>
		/// Represents a version of customizations to be synchronized with the Microsoft Dynamics 365 client for Outlook.
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
		/// 1:N form_ancestor_form
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("form_ancestor_form")]
		public System.Collections.Generic.IEnumerable<SystemForm> FormAncestorForm
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<SystemForm>("form_ancestor_form", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("FormAncestorForm");
				this.SetRelatedEntities<SystemForm>("form_ancestor_form", null, value);
				this.OnPropertyChanged("FormAncestorForm");
			}
		}

		/// <summary>
		/// 1:N SystemForm_AsyncOperations
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("SystemForm_AsyncOperations")]
		public System.Collections.Generic.IEnumerable<AsyncOperation> SystemFormAsyncOperations
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<AsyncOperation>("SystemForm_AsyncOperations", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("SystemFormAsyncOperations");
				this.SetRelatedEntities<AsyncOperation>("SystemForm_AsyncOperations", null, value);
				this.OnPropertyChanged("SystemFormAsyncOperations");
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
			    public struct FormActivationState
                {
					public const int Inactive = 0;
					public const int Active = 1;
                }
			    public struct FormPresentation
                {
					public const int ClassicForm = 0;
					public const int AirForm = 1;
					public const int ConvertedICForm = 2;
                }
                public struct IsAIRMerged
                {
                    public const bool No = false;
                    public const bool Yes = true;
                }
                public struct IsDefault
                {
                    public const bool No = false;
                    public const bool Yes = true;
                }
                public struct IsDesktopEnabled
                {
                    public const bool No = false;
                    public const bool Yes = true;
                }
                public struct IsManaged
                {
                    public const bool Unmanaged = false;
                    public const bool Managed = true;
                }
                public struct IsTabletEnabled
                {
                    public const bool No = false;
                    public const bool Yes = true;
                }
			    public struct Type
                {
					public const int Dashboard = 0;
					public const int AppointmentBook = 1;
					public const int Main = 2;
					public const int MiniCampaignBO = 3;
					public const int Preview = 4;
					public const int MobileExpress = 5;
					public const int QuickViewForm = 6;
					public const int QuickCreate = 7;
					public const int Dialog = 8;
					public const int TaskFlowForm = 9;
					public const int InteractionCentricDashboard = 10;
					public const int Card = 11;
					public const int MainInteractiveExperience = 12;
					public const int ContextualDashboard = 13;
					public const int Other = 100;
					public const int MainBackup = 101;
					public const int AppointmentBookBackup = 102;
					public const int PowerBIDashboard = 103;
                }
		}
		#endregion

		#region LogicalNames
		public static class LogicalNames
		{
				public const string FormId = "formid";
				public const string AncestorFormId = "ancestorformid";
				public const string CanBeDeleted = "canbedeleted";
				public const string ComponentState = "componentstate";
				public const string Description = "description";
				public const string FormActivationState = "formactivationstate";
				public const string FormIdUnique = "formidunique";
				public const string FormJson = "formjson";
				public const string FormPresentation = "formpresentation";
				public const string FormXml = "formxml";
				public const string FormXmlManaged = "formxmlmanaged";
				public const string IntroducedVersion = "introducedversion";
				public const string IsAIRMerged = "isairmerged";
				public const string IsCustomizable = "iscustomizable";
				public const string IsDefault = "isdefault";
				public const string IsDesktopEnabled = "isdesktopenabled";
				public const string IsManaged = "ismanaged";
				public const string IsTabletEnabled = "istabletenabled";
				public const string Name = "name";
				public const string ObjectTypeCode = "objecttypecode";
				public const string OrganizationId = "organizationid";
				public const string OverwriteTime = "overwritetime";
				public const string PublishedOn = "publishedon";
				public const string SolutionId = "solutionid";
				public const string Type = "type";
				public const string UniqueName = "uniquename";
				public const string Version = "version";
				public const string VersionNumber = "versionnumber";
		}
		#endregion

		#region Relations
        public static class Relations
        {
            public static class OneToMany
            {
				public const string FormAncestorForm = "form_ancestor_form";
				public const string ProcesstriggerSystemform = "processtrigger_systemform";
				public const string SocialinsightsconfigurationSystemform = "socialinsightsconfiguration_systemform";
				public const string SystemFormAsyncOperations = "SystemForm_AsyncOperations";
				public const string SystemFormBulkDeleteFailures = "SystemForm_BulkDeleteFailures";
            }

            public static class ManyToOne
            {
				public const string FormAncestorForm = "form_ancestor_form";
				public const string OrganizationSystemforms = "organization_systemforms";
            }

            public static class ManyToMany
            {
            }
        }

        #endregion

		#region Methods

        public static SystemForm Retrieve(IOrganizationService service, Guid id)
        {
            return Retrieve(service,id, new ColumnSet(true));
        }

        public static SystemForm Retrieve(IOrganizationService service, Guid id, ColumnSet columnSet)
        {
            return service.Retrieve("systemform", id, columnSet).ToEntity<SystemForm>();
        }

        public SystemForm GetChangedEntity()
        {
            if (_trackChanges)
            {
                var attr = new AttributeCollection();
                foreach (var attrName in _changedProperties.Value.Select(changedProperty => ((AttributeLogicalNameAttribute) GetType().GetProperty(changedProperty)!.GetCustomAttribute(typeof (AttributeLogicalNameAttribute))!).LogicalName).Where(attrName => Contains(attrName)))
                {
                    attr.Add(attrName,this[attrName]);
                }
                return new  SystemForm(Id) {Attributes = attr };
            }
            return this;
        }
        #endregion
	}

	#region Context
	public partial class DataContext
	{
		public IQueryable<SystemForm> SystemFormSet
		{
			get
			{
				return CreateQuery<SystemForm>();
			}
		}
	}
	#endregion
}
