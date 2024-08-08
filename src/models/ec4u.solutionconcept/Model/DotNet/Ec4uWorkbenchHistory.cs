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
namespace ec4u.solutionconcept
{
	/// <inheritdoc />
	
	[DataContractAttribute()]
	[EntityLogicalNameAttribute("ec4u_workbench_history")]
	[System.CodeDom.Compiler.GeneratedCode("dgtp", "2023")]
    [ExcludeFromCodeCoverage]
	public partial class Ec4uWorkbenchHistory : Entity, INotifyPropertyChanging, INotifyPropertyChanged
    {
	    #region ctor
		[DebuggerNonUserCode]
		public Ec4uWorkbenchHistory() : this(false)
        {
        }

        [DebuggerNonUserCode]
		public Ec4uWorkbenchHistory(bool trackChanges = false) : base(EntityLogicalName)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public Ec4uWorkbenchHistory(Guid id, bool trackChanges = false) : base(EntityLogicalName,id)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public Ec4uWorkbenchHistory(KeyAttributeCollection keyAttributes, bool trackChanges = false) : base(EntityLogicalName,keyAttributes)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public Ec4uWorkbenchHistory(string keyName, object keyValue, bool trackChanges = false) : base(EntityLogicalName, keyName, keyValue)
        {
			_trackChanges = trackChanges;
        }
        #endregion

		#region fields
        private readonly bool _trackChanges;
        private readonly Lazy<HashSet<string>> _changedProperties = new Lazy<HashSet<string>>();
        #endregion

        #region consts
        public const string EntityLogicalName = "ec4u_workbench_history";
        public const string PrimaryNameAttribute = "ec4u_wbh_entry";
        public const int EntityTypeCode = 10529;
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
		[AttributeLogicalNameAttribute("ec4u_workbench_historyid")]
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
				Ec4uWorkbenchHistoryId = value;
			}
		}

		/// <summary>
		/// Unique identifier for entity instances
		/// </summary>
		[AttributeLogicalName("ec4u_workbench_historyid")]
        public Guid? Ec4uWorkbenchHistoryId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<Guid?>("ec4u_workbench_historyid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(Ec4uWorkbenchHistoryId));
                SetAttributeValue("ec4u_workbench_historyid", value);
				if (value.HasValue)
				{
					base.Id = value.Value;
				}
				else
				{
					base.Id = System.Guid.Empty;
				}
                OnPropertyChanged(nameof(Ec4uWorkbenchHistoryId));
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
        }

		
		[AttributeLogicalName("ec4u_wbh_carrier_id")]
        public EntityReference? Ec4uWbhCarrierId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<EntityReference?>("ec4u_wbh_carrier_id");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(Ec4uWbhCarrierId));
                SetAttributeValue("ec4u_wbh_carrier_id", value);
                OnPropertyChanged(nameof(Ec4uWbhCarrierId));
            }
        }

		
		[AttributeLogicalName("ec4u_wbh_carrier_version")]
        public string? Ec4uWbhCarrierVersion
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("ec4u_wbh_carrier_version");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(Ec4uWbhCarrierVersion));
                SetAttributeValue("ec4u_wbh_carrier_version", value);
                OnPropertyChanged(nameof(Ec4uWbhCarrierVersion));
            }
        }

		
		[AttributeLogicalName("ec4u_wbh_component_mover_log")]
        public string? Ec4uWbhComponentMoverLog
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("ec4u_wbh_component_mover_log");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(Ec4uWbhComponentMoverLog));
                SetAttributeValue("ec4u_wbh_component_mover_log", value);
                OnPropertyChanged(nameof(Ec4uWbhComponentMoverLog));
            }
        }

		
		[AttributeLogicalName("ec4u_wbh_constraint_check_log")]
        public string? Ec4uWbhConstraintCheckLog
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("ec4u_wbh_constraint_check_log");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(Ec4uWbhConstraintCheckLog));
                SetAttributeValue("ec4u_wbh_constraint_check_log", value);
                OnPropertyChanged(nameof(Ec4uWbhConstraintCheckLog));
            }
        }

		/// <summary>
		/// The Workbench History Entry.
		/// </summary>
		[AttributeLogicalName("ec4u_wbh_entry")]
        public string? Ec4uWbhEntry
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("ec4u_wbh_entry");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(Ec4uWbhEntry));
                SetAttributeValue("ec4u_wbh_entry", value);
                OnPropertyChanged(nameof(Ec4uWbhEntry));
            }
        }

		
		[AttributeLogicalName("ec4u_wbh_workbench_id")]
        public EntityReference? Ec4uWbhWorkbenchId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<EntityReference?>("ec4u_wbh_workbench_id");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(Ec4uWbhWorkbenchId));
                SetAttributeValue("ec4u_wbh_workbench_id", value);
                OnPropertyChanged(nameof(Ec4uWbhWorkbenchId));
            }
        }

		
		[AttributeLogicalName("ec4u_wbh_workbench_version")]
        public string? Ec4uWbhWorkbenchVersion
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("ec4u_wbh_workbench_version");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(Ec4uWbhWorkbenchVersion));
                SetAttributeValue("ec4u_wbh_workbench_version", value);
                OnPropertyChanged(nameof(Ec4uWbhWorkbenchVersion));
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
        }


		#endregion

		#region NavigationProperties
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
					public const int Inactive = 2;
                }
		}
		#endregion

		#region LogicalNames
		public static class LogicalNames
		{
				public const string Ec4uWorkbenchHistoryId = "ec4u_workbench_historyid";
				public const string CreatedBy = "createdby";
				public const string CreatedOn = "createdon";
				public const string CreatedOnBehalfBy = "createdonbehalfby";
				public const string Ec4uWbhCarrierId = "ec4u_wbh_carrier_id";
				public const string Ec4uWbhCarrierVersion = "ec4u_wbh_carrier_version";
				public const string Ec4uWbhComponentMoverLog = "ec4u_wbh_component_mover_log";
				public const string Ec4uWbhConstraintCheckLog = "ec4u_wbh_constraint_check_log";
				public const string Ec4uWbhEntry = "ec4u_wbh_entry";
				public const string Ec4uWbhWorkbenchId = "ec4u_wbh_workbench_id";
				public const string Ec4uWbhWorkbenchVersion = "ec4u_wbh_workbench_version";
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
				public const string Ec4uWorkbenchHistoryAsyncOperations = "ec4u_workbench_history_AsyncOperations";
				public const string Ec4uWorkbenchHistoryBulkDeleteFailures = "ec4u_workbench_history_BulkDeleteFailures";
				public const string Ec4uWorkbenchHistoryDuplicateBaseRecord = "ec4u_workbench_history_DuplicateBaseRecord";
				public const string Ec4uWorkbenchHistoryDuplicateMatchingRecord = "ec4u_workbench_history_DuplicateMatchingRecord";
				public const string Ec4uWorkbenchHistoryMailboxTrackingFolders = "ec4u_workbench_history_MailboxTrackingFolders";
				public const string Ec4uWorkbenchHistoryPrincipalObjectAttributeAccesses = "ec4u_workbench_history_PrincipalObjectAttributeAccesses";
				public const string Ec4uWorkbenchHistoryProcessSession = "ec4u_workbench_history_ProcessSession";
				public const string Ec4uWorkbenchHistorySyncErrors = "ec4u_workbench_history_SyncErrors";
				public const string Ec4uWorkbenchHistoryUserEntityInstanceDatas = "ec4u_workbench_history_UserEntityInstanceDatas";
            }

            public static class ManyToOne
            {
				public const string BusinessUnitEc4uWorkbenchHistory = "business_unit_ec4u_workbench_history";
				public const string Ec4uCarrierToWorkbenchHistoryOnCarrier = "ec4u_carrier_to_workbench_history_on_carrier";
				public const string Ec4uWorkbenchToWorkbenchHistoryOnWorkbench = "ec4u_workbench_to_workbench_history_on_workbench";
				public const string LkEc4uWorkbenchHistoryCreatedby = "lk_ec4u_workbench_history_createdby";
				public const string LkEc4uWorkbenchHistoryCreatedonbehalfby = "lk_ec4u_workbench_history_createdonbehalfby";
				public const string LkEc4uWorkbenchHistoryModifiedby = "lk_ec4u_workbench_history_modifiedby";
				public const string LkEc4uWorkbenchHistoryModifiedonbehalfby = "lk_ec4u_workbench_history_modifiedonbehalfby";
				public const string OwnerEc4uWorkbenchHistory = "owner_ec4u_workbench_history";
				public const string TeamEc4uWorkbenchHistory = "team_ec4u_workbench_history";
				public const string UserEc4uWorkbenchHistory = "user_ec4u_workbench_history";
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
        public static Ec4uWorkbenchHistory Retrieve(IOrganizationService service, Guid id)
        {
            return Retrieve(service,id, new ColumnSet(true));
        }

        public static Ec4uWorkbenchHistory Retrieve(IOrganizationService service, Guid id, ColumnSet columnSet)
        {
            return service.Retrieve("ec4u_workbench_history", id, columnSet).ToEntity<Ec4uWorkbenchHistory>();
        }

        public Ec4uWorkbenchHistory GetChangedEntity()
        {
            if (_trackChanges)
            {
                var attr = new AttributeCollection();
                foreach (var attrName in _changedProperties.Value.Select(changedProperty => ((AttributeLogicalNameAttribute) GetType().GetProperty(changedProperty)!.GetCustomAttribute(typeof (AttributeLogicalNameAttribute))!).LogicalName).Where(attrName => Contains(attrName)))
                {
                    attr.Add(attrName,this[attrName]);
                }
                return new  Ec4uWorkbenchHistory(Id) {Attributes = attr };
            }
            return this;
        }
        #endregion
	}

	#region Context
	public partial class DataContext
	{
		public IQueryable<Ec4uWorkbenchHistory> Ec4uWorkbenchHistorySet
		{
			get
			{
				return CreateQuery<Ec4uWorkbenchHistory>();
			}
		}
	}
	#endregion
}
