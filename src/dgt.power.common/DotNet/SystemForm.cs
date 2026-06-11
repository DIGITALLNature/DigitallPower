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
	/// Organization-owned entity customizations including form layout and dashboards.
	/// </summary>
    [DataContract]
    [EntityLogicalName("systemform")]
    [System.CodeDom.Compiler.GeneratedCode("dgtp", "2026")]
    [ExcludeFromCodeCoverage]
    [SuppressMessage("Design", "CA1034")]
    [SuppressMessage("Performance", "CA1815")]
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
        public SystemForm(Guid id, bool trackChanges = false) : base(EntityLogicalName, id)
        {
            _trackChanges = trackChanges;
        }
        [DebuggerNonUserCode]
        public SystemForm(KeyAttributeCollection keyAttributes, bool trackChanges = false) : base(EntityLogicalName, keyAttributes)
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
        private readonly Lazy<HashSet<string>> _changedProperties = new();
        #endregion

        #region consts
        public const string EntityLogicalName = "systemform";
        public const string PrimaryNameAttribute = "name";
        public const int EntityTypeCode = 1030;
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
        [AttributeLogicalName("formid")]
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
                OnPropertyChanging();
                SetAttributeValue("formid", value);
                base.Id = value.HasValue ? value.Value : Guid.Empty;
                OnPropertyChanged();
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
                OnPropertyChanging();
                SetAttributeValue("ancestorformid", value);
                OnPropertyChanged();
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
                OnPropertyChanging();
                SetAttributeValue("canbedeleted", value);
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
                OnPropertyChanging();
                SetAttributeValue("description", value);
                OnPropertyChanged();
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
                OnPropertyChanging();
                SetAttributeValue("formactivationstate", value);
                OnPropertyChanged();
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
                OnPropertyChanging();
                SetAttributeValue("formjson", value);
                OnPropertyChanged();
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
                OnPropertyChanging();
                SetAttributeValue("formpresentation", value);
                OnPropertyChanged();
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
                OnPropertyChanging();
                SetAttributeValue("formxml", value);
                OnPropertyChanged();
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
                OnPropertyChanging();
                SetAttributeValue("introducedversion", value);
                OnPropertyChanged();
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
                OnPropertyChanging();
                SetAttributeValue("isairmerged", value);
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
                OnPropertyChanging();
                SetAttributeValue("isdefault", value);
                OnPropertyChanged();
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
                OnPropertyChanging();
                SetAttributeValue("isdesktopenabled", value);
                OnPropertyChanged();
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
                OnPropertyChanging();
                SetAttributeValue("istabletenabled", value);
                OnPropertyChanged();
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
                OnPropertyChanging();
                SetAttributeValue("name", value);
                OnPropertyChanged();
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
                OnPropertyChanging();
                SetAttributeValue("objecttypecode", value);
                OnPropertyChanged();
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
                OnPropertyChanging();
                SetAttributeValue("type", value);
                OnPropertyChanged();
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
                OnPropertyChanging();
                SetAttributeValue("uniquename", value);
                OnPropertyChanged();
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
                OnPropertyChanging();
                SetAttributeValue("version", value);
                OnPropertyChanged();
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
        [RelationshipSchemaName("form_ancestor_form")]
        public IEnumerable<SystemForm> FormAncestorForm
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<SystemForm>("form_ancestor_form", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("form_ancestor_form", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N SystemForm_AsyncOperations
        /// </summary>
        [RelationshipSchemaName("SystemForm_AsyncOperations")]
        public IEnumerable<AsyncOperation> SystemFormAsyncOperations
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<AsyncOperation>("SystemForm_AsyncOperations", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("SystemForm_AsyncOperations", null, value);
                OnPropertyChanged();
            }
        }
        #endregion

        #region Options
        public static partial class Options
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
        public static partial class LogicalNames
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
        public static partial class Relations
        {
            public static class OneToMany
            {
                public const string FormAncestorForm = "form_ancestor_form";
                public const string ProcesstriggerSystemform = "processtrigger_systemform";
                public const string SocialinsightsconfigurationSystemform = "socialinsightsconfiguration_systemform";
                public const string SystemFormAsyncOperations = "SystemForm_AsyncOperations";
                public const string SystemFormBulkDeleteFailures = "SystemForm_BulkDeleteFailures";
            }

            public static partial class ManyToOne
            {
                public const string FormAncestorForm = "form_ancestor_form";
                public const string OrganizationSystemforms = "organization_systemforms";
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

        public static SystemForm Retrieve(IOrganizationService service, Guid id)
        {
            return Retrieve(service, id, new ColumnSet(true));
        }

        public static SystemForm Retrieve(IOrganizationService service, Guid id, ColumnSet columnSet)
        {
            return service.Retrieve("systemform", id, columnSet).ToEntity<SystemForm>();
        }

        public SystemForm GetChangedEntity()
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
            return new SystemForm(Id) { Attributes = attr };
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
