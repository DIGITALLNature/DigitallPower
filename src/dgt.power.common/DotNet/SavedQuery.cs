using System.Diagnostics.CodeAnalysis;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System.Runtime.Serialization;

// ReSharper disable All
namespace dgt.power.dataverse
{
    /// <inheritdoc cref="Microsoft.Xrm.Sdk.Entity" />
    /// <summary>
	/// Saved query against the database.
	/// </summary>
    [EntityLogicalName("savedquery")]
    [System.CodeDom.Compiler.GeneratedCode("dgtp", "2026")]
    [ExcludeFromCodeCoverage]
    [SuppressMessage("Design", "CA1034")]
    [SuppressMessage("Performance", "CA1815")]
    public partial class SavedQuery : Entity
    {
        #region ctor
        public SavedQuery() : base(EntityLogicalName) { }

        public SavedQuery(Guid id) : base(EntityLogicalName, id) { }

        public SavedQuery(KeyAttributeCollection keyAttributes) : base(EntityLogicalName, keyAttributes) { }

        public SavedQuery(string keyName, object keyValue) : base(EntityLogicalName, keyName, keyValue) { }
        #endregion

        #region consts
        public const string EntityLogicalName = "savedquery";
        public const string PrimaryNameAttribute = "name";
        public const int EntityTypeCode = 1039;
        #endregion

        #region Attributes
        [AttributeLogicalName("savedqueryid")]
        [IgnoreDataMember]
        public new Guid Id
        {
            get
            {
                return base.Id;
            }
            set
            {
                SavedQueryId = value;
            }
        }

        /// <summary>
		/// Unique identifier of the view.
		/// </summary>
        [AttributeLogicalName("savedqueryid")]
        public Guid? SavedQueryId
        {
            get
            {
                return GetAttributeValue<Guid?>("savedqueryid");
            }
            set
            {
                SetAttributeValue("savedqueryid", value);
                base.Id = value.HasValue ? value.Value : Guid.Empty;
            }
        }

        /// <summary>
		/// Type the column name that will be used to group the results from the data collected across multiple records from a system view.
		/// </summary>
        [AttributeLogicalName("advancedgroupby")]
        public string? AdvancedGroupBy
        {
            get
            {
                return GetAttributeValue<string?>("advancedgroupby");
            }
            set
            {
                SetAttributeValue("advancedgroupby", value);
            }
        }

        /// <summary>
		/// Tells whether the view can be deleted.
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
		/// Contains the columns and sorting criteria for the view, stored in XML format.
		/// </summary>
        [AttributeLogicalName("columnsetxml")]
        public string? ColumnSetXml
        {
            get
            {
                return GetAttributeValue<string?>("columnsetxml");
            }
            set
            {
                SetAttributeValue("columnsetxml", value);
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
		/// Type information about how the items in the system view are formatted.
		/// </summary>
        [AttributeLogicalName("conditionalformatting")]
        public string? ConditionalFormatting
        {
            get
            {
                return GetAttributeValue<string?>("conditionalformatting");
            }
            set
            {
                SetAttributeValue("conditionalformatting", value);
            }
        }

        /// <summary>
		/// Shows who created the record.
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
		/// Shows the date and time when the record was created. The date and time are displayed in the time zone selected in Microsoft Dynamics 365 options.
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
		/// Shows who created the record on behalf of another user.
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
		/// Type additional information to describe the view, such as the filter criteria or intended results set.
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
		/// Tells whether the view can retrieve data from all cluster partitions.
		/// </summary>
        [AttributeLogicalName("enablecrosspartition")]
        public bool? EnableCrossPartition
        {
            get
            {
                return GetAttributeValue<bool?>("enablecrosspartition");
            }
            set
            {
                SetAttributeValue("enablecrosspartition", value);
            }
        }

        /// <summary>
		/// String specifying the query in Fetch XML language.
		/// </summary>
        [AttributeLogicalName("fetchxml")]
        public string? FetchXml
        {
            get
            {
                return GetAttributeValue<string?>("fetchxml");
            }
            set
            {
                SetAttributeValue("fetchxml", value);
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
		/// Tells whether a user created the view.
		/// </summary>
        [AttributeLogicalName("iscustom")]
        public bool? IsCustom
        {
            get
            {
                return GetAttributeValue<bool?>("iscustom");
            }
        }

        /// <summary>
		/// Tells whether the component can be customized.
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
		/// Tells whether the view is the default view for the specified record type (entity).
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
		/// Tells whether the record is part of a managed solution.
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
		/// Indicates whether or not this is viewable by the entire organization.
		/// </summary>
        [AttributeLogicalName("isprivate")]
        public bool? IsPrivate
        {
            get
            {
                return GetAttributeValue<bool?>("isprivate");
            }
        }

        /// <summary>
		/// Choose whether the view is compatible with Quick Find. When users search for specific items, you define the fields that are searched in.
		/// </summary>
        [AttributeLogicalName("isquickfindquery")]
        public bool? IsQuickFindQuery
        {
            get
            {
                return GetAttributeValue<bool?>("isquickfindquery");
            }
            set
            {
                SetAttributeValue("isquickfindquery", value);
            }
        }

        /// <summary>
		/// Tells whether the view was created by a user.
		/// </summary>
        [AttributeLogicalName("isuserdefined")]
        public bool? IsUserDefined
        {
            get
            {
                return GetAttributeValue<bool?>("isuserdefined");
            }
        }

        /// <summary>
		/// Layout data in JSON format.
		/// </summary>
        [AttributeLogicalName("layoutjson")]
        public string? LayoutJson
        {
            get
            {
                return GetAttributeValue<string?>("layoutjson");
            }
            set
            {
                SetAttributeValue("layoutjson", value);
            }
        }

        /// <summary>
		/// For internal use only.
		/// </summary>
        [AttributeLogicalName("layoutxml")]
        public string? LayoutXml
        {
            get
            {
                return GetAttributeValue<string?>("layoutxml");
            }
            set
            {
                SetAttributeValue("layoutxml", value);
            }
        }

        /// <summary>
		/// Shows who last updated the record.
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
		/// Shows the date and time when the record was last updated. The date and time are displayed in the time zone selected in Microsoft Dynamics 365 options.
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
		/// Shows who last updated the record on behalf of another user.
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
		/// Type a name for the view to describe what results the view will contain. This name is visible to users in the View list.
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
		/// String specifying the corresponding sql query for the fetch xml specified for offline use.
		/// </summary>
        [AttributeLogicalName("offlinesqlquery")]
        public string? OfflineSqlQuery
        {
            get
            {
                return GetAttributeValue<string?>("offlinesqlquery");
            }
            set
            {
                SetAttributeValue("offlinesqlquery", value);
            }
        }

        /// <summary>
		/// Choose the ID of the organization that the record is associated with.
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
		/// For the organization, type the tab order to determine how users navigate through the screen using only the Tab key.
		/// </summary>
        [AttributeLogicalName("organizationtaborder")]
        public int? OrganizationTabOrder
        {
            get
            {
                return GetAttributeValue<int?>("organizationtaborder");
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
		/// For internal use only.
		/// </summary>
        [AttributeLogicalName("queryapi")]
        public string? QueryAPI
        {
            get
            {
                return GetAttributeValue<string?>("queryapi");
            }
        }

        /// <summary>
		/// For internal use only.
		/// </summary>
        [AttributeLogicalName("queryappusage")]
        public int? QueryAppUsage
        {
            get
            {
                return GetAttributeValue<int?>("queryappusage");
            }
            set
            {
                SetAttributeValue("queryappusage", value);
            }
        }

        /// <summary>
		/// Shows the type of the query.
		/// </summary>
        [AttributeLogicalName("querytype")]
        public int? QueryType
        {
            get
            {
                return GetAttributeValue<int?>("querytype");
            }
            set
            {
                SetAttributeValue("querytype", value);
            }
        }

        /// <summary>
		/// Type of entity displayed in the view.
		/// </summary>
        [AttributeLogicalName("returnedtypecode")]
        public string? ReturnedTypeCode
        {
            get
            {
                return GetAttributeValue<string?>("returnedtypecode");
            }
            set
            {
                SetAttributeValue("returnedtypecode", value);
            }
        }

        /// <summary>
		/// Contains the role display conditions for the SavedQuery.
		/// </summary>
        [AttributeLogicalName("roledisplayconditionsxml")]
        public string? RoleDisplayConditionsXml
        {
            get
            {
                return GetAttributeValue<string?>("roledisplayconditionsxml");
            }
            set
            {
                SetAttributeValue("roledisplayconditionsxml", value);
            }
        }

        /// <summary>
		/// For internal use only.
		/// </summary>
        [AttributeLogicalName("savedqueryidunique")]
        public Guid? SavedQueryIdUnique
        {
            get
            {
                return GetAttributeValue<Guid?>("savedqueryidunique");
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
		/// Shows the status of the view.
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
		/// Shows the reason code that explains the status of the record.
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
		/// Version number of the view.
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
        /// 1:N SavedQuery_AsyncOperations
        /// </summary>
        [RelationshipSchemaName("SavedQuery_AsyncOperations")]
        public IEnumerable<AsyncOperation> SavedQueryAsyncOperations
        {
            get
            {
                return GetRelatedEntities<AsyncOperation>("SavedQuery_AsyncOperations", null);
            }
            set
            {
                SetRelatedEntities("SavedQuery_AsyncOperations", null, value);
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
            public struct EnableCrossPartition
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsCustom
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsDefault
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
            public struct IsQuickFindQuery
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsUserDefined
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct StateCode
            {
                public const int Active = 0;
                public const int Inactive = 1;
            }
            public struct StatusCode
            {
                public const int Active = 1;
                public const int Inactive = 2;
            }
        }
        #endregion

        #region LogicalNames
        public static partial class LogicalNames
        {
            public const string SavedQueryId = "savedqueryid";
            public const string AdvancedGroupBy = "advancedgroupby";
            public const string CanBeDeleted = "canbedeleted";
            public const string ColumnSetXml = "columnsetxml";
            public const string ComponentState = "componentstate";
            public const string ConditionalFormatting = "conditionalformatting";
            public const string CreatedBy = "createdby";
            public const string CreatedOn = "createdon";
            public const string CreatedOnBehalfBy = "createdonbehalfby";
            public const string Description = "description";
            public const string EnableCrossPartition = "enablecrosspartition";
            public const string FetchXml = "fetchxml";
            public const string IntroducedVersion = "introducedversion";
            public const string IsCustom = "iscustom";
            public const string IsCustomizable = "iscustomizable";
            public const string IsDefault = "isdefault";
            public const string IsManaged = "ismanaged";
            public const string IsPrivate = "isprivate";
            public const string IsQuickFindQuery = "isquickfindquery";
            public const string IsUserDefined = "isuserdefined";
            public const string LayoutJson = "layoutjson";
            public const string LayoutXml = "layoutxml";
            public const string ModifiedBy = "modifiedby";
            public const string ModifiedOn = "modifiedon";
            public const string ModifiedOnBehalfBy = "modifiedonbehalfby";
            public const string Name = "name";
            public const string OfflineSqlQuery = "offlinesqlquery";
            public const string OrganizationId = "organizationid";
            public const string OrganizationTabOrder = "organizationtaborder";
            public const string OverwriteTime = "overwritetime";
            public const string QueryAPI = "queryapi";
            public const string QueryAppUsage = "queryappusage";
            public const string QueryType = "querytype";
            public const string ReturnedTypeCode = "returnedtypecode";
            public const string RoleDisplayConditionsXml = "roledisplayconditionsxml";
            public const string SavedQueryIdUnique = "savedqueryidunique";
            public const string SolutionId = "solutionid";
            public const string StateCode = "statecode";
            public const string StatusCode = "statuscode";
            public const string VersionNumber = "versionnumber";
        }
        #endregion

        #region Relations
        public static partial class Relations
        {
            public static class OneToMany
            {
                public const string LkMobileofflineprofileitemSavedquery = "lk_mobileofflineprofileitem_savedquery";
                public const string SavedQueryAsyncOperations = "SavedQuery_AsyncOperations";
                public const string SavedQueryBulkDeleteFailures = "SavedQuery_BulkDeleteFailures";
                public const string SavedQuerySyncErrors = "SavedQuery_SyncErrors";
                public const string UserentityinstancedataSavedquery = "userentityinstancedata_savedquery";
            }

            public static partial class ManyToOne
            {
                public const string LkSavedqueryCreatedonbehalfby = "lk_savedquery_createdonbehalfby";
                public const string LkSavedqueryModifiedonbehalfby = "lk_savedquery_modifiedonbehalfby";
                public const string LkSavedquerybaseCreatedby = "lk_savedquerybase_createdby";
                public const string LkSavedquerybaseModifiedby = "lk_savedquerybase_modifiedby";
                public const string OrganizationSavedQueries = "organization_saved_queries";
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
        public IQueryable<SavedQuery> SavedQuerySet
        {
            get
            {
                return CreateQuery<SavedQuery>();
            }
        }
    }
    #endregion
}
