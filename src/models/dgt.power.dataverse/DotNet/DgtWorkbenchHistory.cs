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
	
	[DataContractAttribute()]
	[EntityLogicalNameAttribute("dgt_workbench_history")]
	[System.CodeDom.Compiler.GeneratedCode("dgtp", "2023")]
    [ExcludeFromCodeCoverage]
	public partial class DgtWorkbenchHistory : Entity, INotifyPropertyChanging, INotifyPropertyChanged
    {
	    #region ctor
		[DebuggerNonUserCode]
		public DgtWorkbenchHistory() : this(false)
        {
        }

        [DebuggerNonUserCode]
		public DgtWorkbenchHistory(bool trackChanges = false) : base(EntityLogicalName)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public DgtWorkbenchHistory(Guid id, bool trackChanges = false) : base(EntityLogicalName,id)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public DgtWorkbenchHistory(KeyAttributeCollection keyAttributes, bool trackChanges = false) : base(EntityLogicalName,keyAttributes)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public DgtWorkbenchHistory(string keyName, object keyValue, bool trackChanges = false) : base(EntityLogicalName, keyName, keyValue)
        {
			_trackChanges = trackChanges;
        }
        #endregion

		#region fields
        private readonly bool _trackChanges;
        private readonly Lazy<HashSet<string>> _changedProperties = new Lazy<HashSet<string>>();
        #endregion

        #region consts
        public const string EntityLogicalName = "dgt_workbench_history";
        public const string PrimaryNameAttribute = "dgt_entry";
        public const int EntityTypeCode = 10427;
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
		[AttributeLogicalNameAttribute("dgt_workbench_historyid")]
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
				DgtWorkbenchHistoryId = value;
			}
		}

		/// <summary>
		/// Unique identifier for entity instances
		/// </summary>
		[AttributeLogicalName("dgt_workbench_historyid")]
        public Guid? DgtWorkbenchHistoryId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<Guid?>("dgt_workbench_historyid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(DgtWorkbenchHistoryId));
                SetAttributeValue("dgt_workbench_historyid", value);
				if (value.HasValue)
				{
					base.Id = value.Value;
				}
				else
				{
					base.Id = System.Guid.Empty;
				}
                OnPropertyChanged(nameof(DgtWorkbenchHistoryId));
            }
        }

		/// <summary>
		/// Unique identifier of the user who created the record.
		/// </summary>
		[AttributeLogicalName("createdby")]
        public EntityReference? CreatedBy
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<EntityReference?>("createdby");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(CreatedBy));
                SetAttributeValue("createdby", value);
                OnPropertyChanged(nameof(CreatedBy));
            }
        }

		/// <summary>
		/// Date and time when the record was created.
		/// </summary>
		[AttributeLogicalName("createdon")]
        public DateTime? CreatedOn
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<DateTime?>("createdon");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(CreatedOn));
                SetAttributeValue("createdon", value);
                OnPropertyChanged(nameof(CreatedOn));
            }
        }

		/// <summary>
		/// Unique identifier of the delegate user who created the record.
		/// </summary>
		[AttributeLogicalName("createdonbehalfby")]
        public EntityReference? CreatedOnBehalfBy
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<EntityReference?>("createdonbehalfby");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(CreatedOnBehalfBy));
                SetAttributeValue("createdonbehalfby", value);
                OnPropertyChanged(nameof(CreatedOnBehalfBy));
            }
        }

		
		[AttributeLogicalName("dgt_carrier_id")]
        public EntityReference? DgtCarrierId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<EntityReference?>("dgt_carrier_id");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(DgtCarrierId));
                SetAttributeValue("dgt_carrier_id", value);
                OnPropertyChanged(nameof(DgtCarrierId));
            }
        }

		
		[AttributeLogicalName("dgt_carrier_version")]
        public string? DgtCarrierVersion
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("dgt_carrier_version");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(DgtCarrierVersion));
                SetAttributeValue("dgt_carrier_version", value);
                OnPropertyChanged(nameof(DgtCarrierVersion));
            }
        }

		
		[AttributeLogicalName("dgt_component_mover_log")]
        public string? DgtComponentMoverLog
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("dgt_component_mover_log");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(DgtComponentMoverLog));
                SetAttributeValue("dgt_component_mover_log", value);
                OnPropertyChanged(nameof(DgtComponentMoverLog));
            }
        }

		
		[AttributeLogicalName("dgt_constraint_check_log")]
        public string? DgtConstraintCheckLog
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("dgt_constraint_check_log");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(DgtConstraintCheckLog));
                SetAttributeValue("dgt_constraint_check_log", value);
                OnPropertyChanged(nameof(DgtConstraintCheckLog));
            }
        }

		/// <summary>
		/// The Workbench History Entry.
		/// </summary>
		[AttributeLogicalName("dgt_entry")]
        public string? DgtEntry
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("dgt_entry");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(DgtEntry));
                SetAttributeValue("dgt_entry", value);
                OnPropertyChanged(nameof(DgtEntry));
            }
        }

		
		[AttributeLogicalName("dgt_workbench_id")]
        public EntityReference? DgtWorkbenchId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<EntityReference?>("dgt_workbench_id");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(DgtWorkbenchId));
                SetAttributeValue("dgt_workbench_id", value);
                OnPropertyChanged(nameof(DgtWorkbenchId));
            }
        }

		
		[AttributeLogicalName("dgt_workbench_version")]
        public string? DgtWorkbenchVersion
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("dgt_workbench_version");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(DgtWorkbenchVersion));
                SetAttributeValue("dgt_workbench_version", value);
                OnPropertyChanged(nameof(DgtWorkbenchVersion));
            }
        }

		/// <summary>
		/// Sequence number of the import that created this record.
		/// </summary>
		[AttributeLogicalName("importsequencenumber")]
        public int? ImportSequenceNumber
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<int?>("importsequencenumber");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(ImportSequenceNumber));
                SetAttributeValue("importsequencenumber", value);
                OnPropertyChanged(nameof(ImportSequenceNumber));
            }
        }

		/// <summary>
		/// Unique identifier of the user who modified the record.
		/// </summary>
		[AttributeLogicalName("modifiedby")]
        public EntityReference? ModifiedBy
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<EntityReference?>("modifiedby");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(ModifiedBy));
                SetAttributeValue("modifiedby", value);
                OnPropertyChanged(nameof(ModifiedBy));
            }
        }

		/// <summary>
		/// Date and time when the record was modified.
		/// </summary>
		[AttributeLogicalName("modifiedon")]
        public DateTime? ModifiedOn
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<DateTime?>("modifiedon");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(ModifiedOn));
                SetAttributeValue("modifiedon", value);
                OnPropertyChanged(nameof(ModifiedOn));
            }
        }

		/// <summary>
		/// Unique identifier of the delegate user who modified the record.
		/// </summary>
		[AttributeLogicalName("modifiedonbehalfby")]
        public EntityReference? ModifiedOnBehalfBy
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<EntityReference?>("modifiedonbehalfby");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(ModifiedOnBehalfBy));
                SetAttributeValue("modifiedonbehalfby", value);
                OnPropertyChanged(nameof(ModifiedOnBehalfBy));
            }
        }

		/// <summary>
		/// Date and time that the record was migrated.
		/// </summary>
		[AttributeLogicalName("overriddencreatedon")]
        public DateTime? OverriddenCreatedOn
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<DateTime?>("overriddencreatedon");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(OverriddenCreatedOn));
                SetAttributeValue("overriddencreatedon", value);
                OnPropertyChanged(nameof(OverriddenCreatedOn));
            }
        }

		/// <summary>
		/// Owner Id
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

		/// <summary>
		/// Unique identifier for the business unit that owns the record
		/// </summary>
		[AttributeLogicalName("owningbusinessunit")]
        public EntityReference? OwningBusinessUnit
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<EntityReference?>("owningbusinessunit");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(OwningBusinessUnit));
                SetAttributeValue("owningbusinessunit", value);
                OnPropertyChanged(nameof(OwningBusinessUnit));
            }
        }

		/// <summary>
		/// Unique identifier for the team that owns the record.
		/// </summary>
		[AttributeLogicalName("owningteam")]
        public EntityReference? OwningTeam
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<EntityReference?>("owningteam");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(OwningTeam));
                SetAttributeValue("owningteam", value);
                OnPropertyChanged(nameof(OwningTeam));
            }
        }

		/// <summary>
		/// Unique identifier for the user that owns the record.
		/// </summary>
		[AttributeLogicalName("owninguser")]
        public EntityReference? OwningUser
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<EntityReference?>("owninguser");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(OwningUser));
                SetAttributeValue("owninguser", value);
                OnPropertyChanged(nameof(OwningUser));
            }
        }

		/// <summary>
		/// Status of the Workbench History
		/// </summary>
		[AttributeLogicalName("statecode")]
        public OptionSetValue? Statecode
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<OptionSetValue?>("statecode");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(Statecode));
                SetAttributeValue("statecode", value);
                OnPropertyChanged(nameof(Statecode));
            }
        }

		/// <summary>
		/// Reason for the status of the Workbench History
		/// </summary>
		[AttributeLogicalName("statuscode")]
        public OptionSetValue? Statuscode
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<OptionSetValue?>("statuscode");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(Statuscode));
                SetAttributeValue("statuscode", value);
                OnPropertyChanged(nameof(Statuscode));
            }
        }

		/// <summary>
		/// For internal use only.
		/// </summary>
		[AttributeLogicalName("timezoneruleversionnumber")]
        public int? TimeZoneRuleVersionNumber
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<int?>("timezoneruleversionnumber");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(TimeZoneRuleVersionNumber));
                SetAttributeValue("timezoneruleversionnumber", value);
                OnPropertyChanged(nameof(TimeZoneRuleVersionNumber));
            }
        }

		/// <summary>
		/// Time zone code that was in use when the record was created.
		/// </summary>
		[AttributeLogicalName("utcconversiontimezonecode")]
        public int? UTCConversionTimeZoneCode
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<int?>("utcconversiontimezonecode");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(UTCConversionTimeZoneCode));
                SetAttributeValue("utcconversiontimezonecode", value);
                OnPropertyChanged(nameof(UTCConversionTimeZoneCode));
            }
        }

		/// <summary>
		/// Version Number
		/// </summary>
		[AttributeLogicalName("versionnumber")]
        public long? VersionNumber
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<long?>("versionnumber");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(VersionNumber));
                SetAttributeValue("versionnumber", value);
                OnPropertyChanged(nameof(VersionNumber));
            }
        }


		#endregion

		#region NavigationProperties
		/// <summary>
		/// 1:N dgt_workbench_history_AsyncOperations
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("dgt_workbench_history_AsyncOperations")]
		public System.Collections.Generic.IEnumerable<AsyncOperation> DgtWorkbenchHistoryAsyncOperations
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<AsyncOperation>("dgt_workbench_history_AsyncOperations", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("DgtWorkbenchHistoryAsyncOperations");
				this.SetRelatedEntities<AsyncOperation>("dgt_workbench_history_AsyncOperations", null, value);
				this.OnPropertyChanged("DgtWorkbenchHistoryAsyncOperations");
			}
		}

		#endregion

		#region Options
		public static class Options
		{
                public struct Statecode
                {
					public const int Active = 0;
					public const int Inactive = 1;
                }
                public struct Statuscode
                {
					public const int Active = 1;
					public const int Success = 2;
					public const int Failure = 283510001;
                }
		}
		#endregion

		#region LogicalNames
		public static class LogicalNames
		{
				public const string DgtWorkbenchHistoryId = "dgt_workbench_historyid";
				public const string CreatedBy = "createdby";
				public const string CreatedOn = "createdon";
				public const string CreatedOnBehalfBy = "createdonbehalfby";
				public const string DgtCarrierId = "dgt_carrier_id";
				public const string DgtCarrierVersion = "dgt_carrier_version";
				public const string DgtComponentMoverLog = "dgt_component_mover_log";
				public const string DgtConstraintCheckLog = "dgt_constraint_check_log";
				public const string DgtEntry = "dgt_entry";
				public const string DgtWorkbenchId = "dgt_workbench_id";
				public const string DgtWorkbenchVersion = "dgt_workbench_version";
				public const string ImportSequenceNumber = "importsequencenumber";
				public const string ModifiedBy = "modifiedby";
				public const string ModifiedOn = "modifiedon";
				public const string ModifiedOnBehalfBy = "modifiedonbehalfby";
				public const string OverriddenCreatedOn = "overriddencreatedon";
				public const string OwnerId = "ownerid";
				public const string OwningBusinessUnit = "owningbusinessunit";
				public const string OwningTeam = "owningteam";
				public const string OwningUser = "owninguser";
				public const string Statecode = "statecode";
				public const string Statuscode = "statuscode";
				public const string TimeZoneRuleVersionNumber = "timezoneruleversionnumber";
				public const string UTCConversionTimeZoneCode = "utcconversiontimezonecode";
				public const string VersionNumber = "versionnumber";
		}
		#endregion

		#region Relations
        public static class Relations
        {
            public static class OneToMany
            {
				public const string DgtWorkbenchHistoryAsyncOperations = "dgt_workbench_history_AsyncOperations";
				public const string DgtWorkbenchHistoryBulkDeleteFailures = "dgt_workbench_history_BulkDeleteFailures";
				public const string DgtWorkbenchHistoryDuplicateBaseRecord = "dgt_workbench_history_DuplicateBaseRecord";
				public const string DgtWorkbenchHistoryDuplicateMatchingRecord = "dgt_workbench_history_DuplicateMatchingRecord";
				public const string DgtWorkbenchHistoryLogDgtWorkbenchHistoryId = "dgt_workbench_history_log_dgt_workbench_history_id";
				public const string DgtWorkbenchHistoryMailboxTrackingFolders = "dgt_workbench_history_MailboxTrackingFolders";
				public const string DgtWorkbenchHistoryPrincipalObjectAttributeAccesses = "dgt_workbench_history_PrincipalObjectAttributeAccesses";
				public const string DgtWorkbenchHistoryProcessSession = "dgt_workbench_history_ProcessSession";
				public const string DgtWorkbenchHistorySyncErrors = "dgt_workbench_history_SyncErrors";
				public const string DgtWorkbenchHistoryUserEntityInstanceDatas = "dgt_workbench_history_UserEntityInstanceDatas";
            }

            public static class ManyToOne
            {
				public const string BusinessUnitDgtWorkbenchHistory = "business_unit_dgt_workbench_history";
				public const string DgtCarrierToWorkbenchHistoryOnCarrier = "dgt_carrier_to_workbench_history_on_carrier";
				public const string DgtWorkbenchToWorkbenchHistory = "dgt_workbench_to_workbench_history";
				public const string LkDgtWorkbenchHistoryCreatedby = "lk_dgt_workbench_history_createdby";
				public const string LkDgtWorkbenchHistoryCreatedonbehalfby = "lk_dgt_workbench_history_createdonbehalfby";
				public const string LkDgtWorkbenchHistoryModifiedby = "lk_dgt_workbench_history_modifiedby";
				public const string LkDgtWorkbenchHistoryModifiedonbehalfby = "lk_dgt_workbench_history_modifiedonbehalfby";
				public const string OwnerDgtWorkbenchHistory = "owner_dgt_workbench_history";
				public const string TeamDgtWorkbenchHistory = "team_dgt_workbench_history";
				public const string UserDgtWorkbenchHistory = "user_dgt_workbench_history";
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
        public static DgtWorkbenchHistory Retrieve(IOrganizationService service, Guid id)
        {
            return Retrieve(service,id, new ColumnSet(true));
        }

        public static DgtWorkbenchHistory Retrieve(IOrganizationService service, Guid id, ColumnSet columnSet)
        {
            return service.Retrieve("dgt_workbench_history", id, columnSet).ToEntity<DgtWorkbenchHistory>();
        }

        public DgtWorkbenchHistory GetChangedEntity()
        {
            if (_trackChanges)
            {
                var attr = new AttributeCollection();
                foreach (var attrName in _changedProperties.Value.Select(changedProperty => ((AttributeLogicalNameAttribute) GetType().GetProperty(changedProperty)!.GetCustomAttribute(typeof (AttributeLogicalNameAttribute))!).LogicalName).Where(attrName => Contains(attrName)))
                {
                    attr.Add(attrName,this[attrName]);
                }
                return new  DgtWorkbenchHistory(Id) {Attributes = attr };
            }
            return this;
        }
        #endregion
	}

	#region Context
	public partial class DataContext
	{
		public IQueryable<DgtWorkbenchHistory> DgtWorkbenchHistorySet
		{
			get
			{
				return CreateQuery<DgtWorkbenchHistory>();
			}
		}
	}
	#endregion
}
