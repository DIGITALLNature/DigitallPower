using System.Diagnostics.CodeAnalysis;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System.Runtime.Serialization;

// ReSharper disable All
namespace dgt.power.dataverse
{
    /// <inheritdoc cref="Microsoft.Xrm.Sdk.Entity" />
    /// <summary>
	/// Organization-owned entity customizations including form layout and dashboards.
	/// </summary>
    [EntityLogicalName("systemform")]
    [System.CodeDom.Compiler.GeneratedCode("dgtp", "2026")]
    [ExcludeFromCodeCoverage]
    [SuppressMessage("Design", "CA1034")]
    [SuppressMessage("Performance", "CA1815")]
    public partial class SystemForm : Entity
    {
        #region ctor
        public SystemForm() : base(EntityLogicalName) { }

        public SystemForm(Guid id) : base(EntityLogicalName, id) { }

        public SystemForm(KeyAttributeCollection keyAttributes) : base(EntityLogicalName, keyAttributes) { }

        public SystemForm(string keyName, object keyValue) : base(EntityLogicalName, keyName, keyValue) { }
        #endregion

        #region consts
        public const string EntityLogicalName = "systemform";
        public const string PrimaryNameAttribute = "name";
        public const int EntityTypeCode = 1030;
        #endregion

        #region Attributes
        [AttributeLogicalName("formid")]
        [IgnoreDataMember]
        public new Guid Id
        {
            get
            {
                return base.Id;
            }
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
            get
            {
                return GetAttributeValue<Guid?>("formid");
            }
            set
            {
                SetAttributeValue("formid", value);
                base.Id = value.HasValue ? value.Value : Guid.Empty;
            }
        }

        /// <summary>
		/// Unique identifier of the parent form.
		/// </summary>
        [AttributeLogicalName("ancestorformid")]
        public EntityReference? AncestorFormId
        {
            get
            {
                return GetAttributeValue<EntityReference?>("ancestorformid");
            }
            set
            {
                SetAttributeValue("ancestorformid", value);
            }
        }

        /// <summary>
		/// Information that specifies whether this component can be deleted.
		/// </summary>
        [AttributeLogicalName("canbedeleted")]
        public BooleanManagedProperty? CanBeDeleted
        {
            get
            {
                return GetAttributeValue<BooleanManagedProperty?>("canbedeleted");
            }
            set
            {
                SetAttributeValue("canbedeleted", value);
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
		/// Description of the form or dashboard.
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
		/// Specifies the state of the form.
		/// </summary>
        [AttributeLogicalName("formactivationstate")]
        public OptionSetValue? FormActivationState
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("formactivationstate");
            }
            set
            {
                SetAttributeValue("formactivationstate", value);
            }
        }

        /// <summary>
		/// Unique identifier of the form used when synchronizing customizations for the Microsoft Dynamics 365 client for Outlook.
		/// </summary>
        [AttributeLogicalName("formidunique")]
        public Guid? FormIdUnique
        {
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
            get
            {
                return GetAttributeValue<string?>("formjson");
            }
            set
            {
                SetAttributeValue("formjson", value);
            }
        }

        /// <summary>
		/// Specifies whether this form is in the updated UI layout in Microsoft Dynamics CRM 2015 or Microsoft Dynamics CRM Online 2015 Update.
		/// </summary>
        [AttributeLogicalName("formpresentation")]
        public OptionSetValue? FormPresentation
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("formpresentation");
            }
            set
            {
                SetAttributeValue("formpresentation", value);
            }
        }

        /// <summary>
		/// XML representation of the form layout.
		/// </summary>
        [AttributeLogicalName("formxml")]
        public string? FormXml
        {
            get
            {
                return GetAttributeValue<string?>("formxml");
            }
            set
            {
                SetAttributeValue("formxml", value);
            }
        }

        /// <summary>
		/// formXml diff as in a managed solution. for internal use only
		/// </summary>
        [AttributeLogicalName("formxmlmanaged")]
        public string? FormXmlManaged
        {
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
		/// Specifies whether this form is merged with the updated UI layout in Microsoft Dynamics CRM 2015 or Microsoft Dynamics CRM Online 2015 Update.
		/// </summary>
        [AttributeLogicalName("isairmerged")]
        public bool? IsAIRMerged
        {
            get
            {
                return GetAttributeValue<bool?>("isairmerged");
            }
            set
            {
                SetAttributeValue("isairmerged", value);
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
		/// Information that specifies whether the form or the dashboard is the system default.
		/// </summary>
        [AttributeLogicalName("isdefault")]
        public bool? IsDefault
        {
            get
            {
                return GetAttributeValue<bool?>("isdefault");
            }
            set
            {
                SetAttributeValue("isdefault", value);
            }
        }

        /// <summary>
		/// Information that specifies whether the dashboard is enabled for desktop.
		/// </summary>
        [AttributeLogicalName("isdesktopenabled")]
        public bool? IsDesktopEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("isdesktopenabled");
            }
            set
            {
                SetAttributeValue("isdesktopenabled", value);
            }
        }

        
        [AttributeLogicalName("ismanaged")]
        public bool? IsManaged
        {
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
            get
            {
                return GetAttributeValue<bool?>("istabletenabled");
            }
            set
            {
                SetAttributeValue("istabletenabled", value);
            }
        }

        /// <summary>
		/// Name of the form.
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
		/// Code that represents the record type.
		/// </summary>
        [AttributeLogicalName("objecttypecode")]
        public string? ObjectTypeCode
        {
            get
            {
                return GetAttributeValue<string?>("objecttypecode");
            }
            set
            {
                SetAttributeValue("objecttypecode", value);
            }
        }

        /// <summary>
		/// Unique identifier of the organization.
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

        
        [AttributeLogicalName("publishedon")]
        public DateTime? PublishedOn
        {
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
            get
            {
                return GetAttributeValue<OptionSetValue?>("type");
            }
            set
            {
                SetAttributeValue("type", value);
            }
        }

        /// <summary>
		/// Unique Name
		/// </summary>
        [AttributeLogicalName("uniquename")]
        public string? UniqueName
        {
            get
            {
                return GetAttributeValue<string?>("uniquename");
            }
            set
            {
                SetAttributeValue("uniquename", value);
            }
        }

        /// <summary>
		/// For internal use only.
		/// </summary>
        [AttributeLogicalName("version")]
        public int? Version
        {
            get
            {
                return GetAttributeValue<int?>("version");
            }
            set
            {
                SetAttributeValue("version", value);
            }
        }

        /// <summary>
		/// Represents a version of customizations to be synchronized with the Microsoft Dynamics 365 client for Outlook.
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
        /// 1:N form_ancestor_form
        /// </summary>
        [RelationshipSchemaName("form_ancestor_form")]
        public IEnumerable<SystemForm> FormAncestorForm
        {
            get
            {
                return GetRelatedEntities<SystemForm>("form_ancestor_form", null);
            }
            set
            {
                SetRelatedEntities("form_ancestor_form", null, value);
            }
        }

        /// <summary>
        /// 1:N SystemForm_AsyncOperations
        /// </summary>
        [RelationshipSchemaName("SystemForm_AsyncOperations")]
        public IEnumerable<AsyncOperation> SystemFormAsyncOperations
        {
            get
            {
                return GetRelatedEntities<AsyncOperation>("SystemForm_AsyncOperations", null);
            }
            set
            {
                SetRelatedEntities("SystemForm_AsyncOperations", null, value);
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
