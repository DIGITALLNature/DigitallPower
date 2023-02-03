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
	/// Saved query against the database.
	/// </summary>
	[DataContractAttribute()]
	[EntityLogicalNameAttribute("savedquery")]
	[System.CodeDom.Compiler.GeneratedCode("ec4u.automation", "1.0.0")]
	public partial class SavedQuery : Entity, INotifyPropertyChanging, INotifyPropertyChanged
    {
	    #region ctor
		[DebuggerNonUserCode]
		public SavedQuery() : this(false)
        {
        }

        [DebuggerNonUserCode]
		public SavedQuery(bool trackChanges = false) : base(EntityLogicalName)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public SavedQuery(Guid id, bool trackChanges = false) : base(EntityLogicalName,id)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public SavedQuery(KeyAttributeCollection keyAttributes, bool trackChanges = false) : base(EntityLogicalName,keyAttributes)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public SavedQuery(string keyName, object keyValue, bool trackChanges = false) : base(EntityLogicalName, keyName, keyValue)
        {
			_trackChanges = trackChanges;
        }
        #endregion

		#region fields
        private readonly bool _trackChanges;
        private readonly Lazy<HashSet<string>> _changedProperties = new Lazy<HashSet<string>>();
        #endregion

        #region consts
        public const string EntityLogicalName = "savedquery";
        public const string PrimaryNameAttribute = "name";
        public const int EntityTypeCode = 1039;
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
		[AttributeLogicalNameAttribute("savedqueryid")]
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
				SavedQueryId = value;
			}
		}

		/// <summary>
		/// Unique identifier of the view.
		/// </summary>
		[AttributeLogicalName("savedqueryid")]
        public Guid? SavedQueryId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<Guid?>("savedqueryid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(SavedQueryId));
                SetAttributeValue("savedqueryid", value);
				if (value.HasValue)
				{
					base.Id = value.Value;
				}
				else
				{
					base.Id = System.Guid.Empty;
				}
                OnPropertyChanged(nameof(SavedQueryId));
            }
        }

		/// <summary>
		/// Type the column name that will be used to group the results from the data collected across multiple records from a system view.
		/// </summary>
		[AttributeLogicalName("advancedgroupby")]
        public string? AdvancedGroupBy
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("advancedgroupby");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(AdvancedGroupBy));
                SetAttributeValue("advancedgroupby", value);
                OnPropertyChanged(nameof(AdvancedGroupBy));
            }
        }

		/// <summary>
		/// Tells whether the view can be deleted.
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
		/// Contains the columns and sorting criteria for the view, stored in XML format.
		/// </summary>
		[AttributeLogicalName("columnsetxml")]
        public string? ColumnSetXml
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("columnsetxml");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(ColumnSetXml));
                SetAttributeValue("columnsetxml", value);
                OnPropertyChanged(nameof(ColumnSetXml));
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
		/// Type information about how the items in the system view are formatted.
		/// </summary>
		[AttributeLogicalName("conditionalformatting")]
        public string? ConditionalFormatting
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("conditionalformatting");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(ConditionalFormatting));
                SetAttributeValue("conditionalformatting", value);
                OnPropertyChanged(nameof(ConditionalFormatting));
            }
        }

		/// <summary>
		/// Shows who created the record.
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
		/// Shows the date and time when the record was created. The date and time are displayed in the time zone selected in Microsoft Dynamics 365 options.
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
		/// Shows who created the record on behalf of another user.
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
		/// Type additional information to describe the view, such as the filter criteria or intended results set.
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
		/// String specifying the query in Fetch XML language.
		/// </summary>
		[AttributeLogicalName("fetchxml")]
        public string? FetchXml
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("fetchxml");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(FetchXml));
                SetAttributeValue("fetchxml", value);
                OnPropertyChanged(nameof(FetchXml));
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
		/// Tells whether a user created the view.
		/// </summary>
		[AttributeLogicalName("iscustom")]
        public bool? IsCustom
        {
            [DebuggerNonUserCode]
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
		/// Tells whether the view is the default view for the specified record type (entity).
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
		/// Tells whether the record is part of a managed solution.
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
		/// Indicates whether or not this is viewable by the entire organization.
		/// </summary>
		[AttributeLogicalName("isprivate")]
        public bool? IsPrivate
        {
            [DebuggerNonUserCode]
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
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<bool?>("isquickfindquery");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(IsQuickFindQuery));
                SetAttributeValue("isquickfindquery", value);
                OnPropertyChanged(nameof(IsQuickFindQuery));
            }
        }

		/// <summary>
		/// Tells whether the view was created by a user.
		/// </summary>
		[AttributeLogicalName("isuserdefined")]
        public bool? IsUserDefined
        {
            [DebuggerNonUserCode]
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
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("layoutjson");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(LayoutJson));
                SetAttributeValue("layoutjson", value);
                OnPropertyChanged(nameof(LayoutJson));
            }
        }

		/// <summary>
		/// For internal use only.
		/// </summary>
		[AttributeLogicalName("layoutxml")]
        public string? LayoutXml
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("layoutxml");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(LayoutXml));
                SetAttributeValue("layoutxml", value);
                OnPropertyChanged(nameof(LayoutXml));
            }
        }

		/// <summary>
		/// Shows who last updated the record.
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
		/// Shows the date and time when the record was last updated. The date and time are displayed in the time zone selected in Microsoft Dynamics 365 options.
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
		/// Shows who last updated the record on behalf of another user.
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
		/// Type a name for the view to describe what results the view will contain. This name is visible to users in the View list.
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
		/// String specifying the corresponding sql query for the fetch xml specified for offline use.
		/// </summary>
		[AttributeLogicalName("offlinesqlquery")]
        public string? OfflineSqlQuery
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("offlinesqlquery");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(OfflineSqlQuery));
                SetAttributeValue("offlinesqlquery", value);
                OnPropertyChanged(nameof(OfflineSqlQuery));
            }
        }

		/// <summary>
		/// Choose the ID of the organization that the record is associated with.
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
		/// For the organization, type the tab order to determine how users navigate through the screen using only the Tab key.
		/// </summary>
		[AttributeLogicalName("organizationtaborder")]
        public int? OrganizationTabOrder
        {
            [DebuggerNonUserCode]
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
            [DebuggerNonUserCode]
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
            [DebuggerNonUserCode]
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
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<int?>("queryappusage");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(QueryAppUsage));
                SetAttributeValue("queryappusage", value);
                OnPropertyChanged(nameof(QueryAppUsage));
            }
        }

		/// <summary>
		/// Shows the type of the query.
		/// </summary>
		[AttributeLogicalName("querytype")]
        public int? QueryType
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<int?>("querytype");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(QueryType));
                SetAttributeValue("querytype", value);
                OnPropertyChanged(nameof(QueryType));
            }
        }

		/// <summary>
		/// Type of entity displayed in the view.
		/// </summary>
		[AttributeLogicalName("returnedtypecode")]
        public string? ReturnedTypeCode
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("returnedtypecode");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(ReturnedTypeCode));
                SetAttributeValue("returnedtypecode", value);
                OnPropertyChanged(nameof(ReturnedTypeCode));
            }
        }

		/// <summary>
		/// For internal use only.
		/// </summary>
		[AttributeLogicalName("savedqueryidunique")]
        public Guid? SavedQueryIdUnique
        {
            [DebuggerNonUserCode]
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
            [DebuggerNonUserCode]
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
		/// Shows the reason code that explains the status of the record.
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
		/// Version number of the view.
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
		/// 1:N SavedQuery_AsyncOperations
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("SavedQuery_AsyncOperations")]
		public System.Collections.Generic.IEnumerable<AsyncOperation> SavedQueryAsyncOperations
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<AsyncOperation>("SavedQuery_AsyncOperations", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("SavedQueryAsyncOperations");
				this.SetRelatedEntities<AsyncOperation>("SavedQuery_AsyncOperations", null, value);
				this.OnPropertyChanged("SavedQueryAsyncOperations");
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
		public static class LogicalNames
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
				public const string SavedQueryIdUnique = "savedqueryidunique";
				public const string SolutionId = "solutionid";
				public const string StateCode = "statecode";
				public const string StatusCode = "statuscode";
				public const string VersionNumber = "versionnumber";
		}
		#endregion

		#region Relations
        public static class Relations
        {
            public static class OneToMany
            {
				public const string LkMobileofflineprofileitemSavedquery = "lk_mobileofflineprofileitem_savedquery";
				public const string SavedQueryAsyncOperations = "SavedQuery_AsyncOperations";
				public const string SavedQueryBulkDeleteFailures = "SavedQuery_BulkDeleteFailures";
				public const string SavedQuerySyncErrors = "SavedQuery_SyncErrors";
				public const string UserentityinstancedataSavedquery = "userentityinstancedata_savedquery";
            }

            public static class ManyToOne
            {
				public const string LkSavedqueryCreatedonbehalfby = "lk_savedquery_createdonbehalfby";
				public const string LkSavedqueryModifiedonbehalfby = "lk_savedquery_modifiedonbehalfby";
				public const string LkSavedquerybaseCreatedby = "lk_savedquerybase_createdby";
				public const string LkSavedquerybaseModifiedby = "lk_savedquerybase_modifiedby";
				public const string OrganizationSavedQueries = "organization_saved_queries";
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
        public static SavedQuery Retrieve(IOrganizationService service, Guid id)
        {
            return Retrieve(service,id, new ColumnSet(true));
        }

        public static SavedQuery Retrieve(IOrganizationService service, Guid id, ColumnSet columnSet)
        {
            return service.Retrieve("savedquery", id, columnSet).ToEntity<SavedQuery>();
        }

        public SavedQuery GetChangedEntity()
        {
            if (_trackChanges)
            {
                var attr = new AttributeCollection();
                foreach (var attrName in _changedProperties.Value.Select(changedProperty => ((AttributeLogicalNameAttribute) GetType().GetProperty(changedProperty)!.GetCustomAttribute(typeof (AttributeLogicalNameAttribute))!).LogicalName).Where(attrName => Contains(attrName)))
                {
                    attr.Add(attrName,this[attrName]);
                }
                return new  SavedQuery(Id) {Attributes = attr };
            }
            return this;
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
