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
	[EntityLogicalNameAttribute("dgt_workbench")]
	[System.CodeDom.Compiler.GeneratedCode("dgtp", "2023")]
    [ExcludeFromCodeCoverage]
	public partial class DgtWorkbench : Entity, INotifyPropertyChanging, INotifyPropertyChanged
    {
	    #region ctor
		[DebuggerNonUserCode]
		public DgtWorkbench() : this(false)
        {
        }

        [DebuggerNonUserCode]
		public DgtWorkbench(bool trackChanges = false) : base(EntityLogicalName)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public DgtWorkbench(Guid id, bool trackChanges = false) : base(EntityLogicalName,id)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public DgtWorkbench(KeyAttributeCollection keyAttributes, bool trackChanges = false) : base(EntityLogicalName,keyAttributes)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public DgtWorkbench(string keyName, object keyValue, bool trackChanges = false) : base(EntityLogicalName, keyName, keyValue)
        {
			_trackChanges = trackChanges;
        }
        #endregion

		#region fields
        private readonly bool _trackChanges;
        private readonly Lazy<HashSet<string>> _changedProperties = new Lazy<HashSet<string>>();
        #endregion

        #region consts
        public const string EntityLogicalName = "dgt_workbench";
        public const string PrimaryNameAttribute = "dgt_name";
        public const int EntityTypeCode = 10426;
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
		[AttributeLogicalNameAttribute("dgt_workbenchid")]
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
				DgtWorkbenchId = value;
			}
		}

		/// <summary>
		/// Unique identifier for entity instances
		/// </summary>
		[AttributeLogicalName("dgt_workbenchid")]
        public Guid? DgtWorkbenchId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<Guid?>("dgt_workbenchid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(DgtWorkbenchId));
                SetAttributeValue("dgt_workbenchid", value);
				if (value.HasValue)
				{
					base.Id = value.Value;
				}
				else
				{
					base.Id = System.Guid.Empty;
				}
                OnPropertyChanged(nameof(DgtWorkbenchId));
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

		/// <summary>
		/// The Workbench Name.
		/// </summary>
		[AttributeLogicalName("dgt_name")]
        public string? DgtName
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("dgt_name");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(DgtName));
                SetAttributeValue("dgt_name", value);
                OnPropertyChanged(nameof(DgtName));
            }
        }

		
		[AttributeLogicalName("dgt_nature_set")]
        public OptionSetValue? DgtNatureSet
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<OptionSetValue?>("dgt_nature_set");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(DgtNatureSet));
                SetAttributeValue("dgt_nature_set", value);
                OnPropertyChanged(nameof(DgtNatureSet));
            }
        }

		
		[AttributeLogicalName("dgt_solutionfriendlyname")]
        public string? DgtSolutionfriendlyname
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("dgt_solutionfriendlyname");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(DgtSolutionfriendlyname));
                SetAttributeValue("dgt_solutionfriendlyname", value);
                OnPropertyChanged(nameof(DgtSolutionfriendlyname));
            }
        }

		
		[AttributeLogicalName("dgt_solutionid")]
        public string? DgtSolutionid
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("dgt_solutionid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(DgtSolutionid));
                SetAttributeValue("dgt_solutionid", value);
                OnPropertyChanged(nameof(DgtSolutionid));
            }
        }

		
		[AttributeLogicalName("dgt_solutionuniquename")]
        public string? DgtSolutionuniquename
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("dgt_solutionuniquename");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(DgtSolutionuniquename));
                SetAttributeValue("dgt_solutionuniquename", value);
                OnPropertyChanged(nameof(DgtSolutionuniquename));
            }
        }

		
		[AttributeLogicalName("dgt_solutionversion")]
        public string? DgtSolutionversion
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("dgt_solutionversion");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(DgtSolutionversion));
                SetAttributeValue("dgt_solutionversion", value);
                OnPropertyChanged(nameof(DgtSolutionversion));
            }
        }

		
		[AttributeLogicalName("dgt_target_carrier_id")]
        public EntityReference? DgtTargetCarrierId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<EntityReference?>("dgt_target_carrier_id");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(DgtTargetCarrierId));
                SetAttributeValue("dgt_target_carrier_id", value);
                OnPropertyChanged(nameof(DgtTargetCarrierId));
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
		/// Status of the Workbench
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
		/// Reason for the status of the Workbench
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
		/// 1:N dgt_workbench_AsyncOperations
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("dgt_workbench_AsyncOperations")]
		public System.Collections.Generic.IEnumerable<AsyncOperation> DgtWorkbenchAsyncOperations
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<AsyncOperation>("dgt_workbench_AsyncOperations", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("DgtWorkbenchAsyncOperations");
				this.SetRelatedEntities<AsyncOperation>("dgt_workbench_AsyncOperations", null, value);
				this.OnPropertyChanged("DgtWorkbenchAsyncOperations");
			}
		}

		/// <summary>
		/// 1:N dgt_workbench_to_carrier_on_workbench
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("dgt_workbench_to_carrier_on_workbench")]
		public System.Collections.Generic.IEnumerable<DgtCarrier> DgtWorkbenchToCarrierOnWorkbench
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<DgtCarrier>("dgt_workbench_to_carrier_on_workbench", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("DgtWorkbenchToCarrierOnWorkbench");
				this.SetRelatedEntities<DgtCarrier>("dgt_workbench_to_carrier_on_workbench", null, value);
				this.OnPropertyChanged("DgtWorkbenchToCarrierOnWorkbench");
			}
		}

		/// <summary>
		/// 1:N dgt_workbench_to_workbench_history
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("dgt_workbench_to_workbench_history")]
		public System.Collections.Generic.IEnumerable<DgtWorkbenchHistory> DgtWorkbenchToWorkbenchHistory
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<DgtWorkbenchHistory>("dgt_workbench_to_workbench_history", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("DgtWorkbenchToWorkbenchHistory");
				this.SetRelatedEntities<DgtWorkbenchHistory>("dgt_workbench_to_workbench_history", null, value);
				this.OnPropertyChanged("DgtWorkbenchToWorkbenchHistory");
			}
		}

		#endregion

		#region Options
		public static class Options
		{
			    public struct DgtNatureSet
                {
					public const int Workbench = 283510000;
					public const int Assembly = 283510001;
					public const int EnvironmentVariable = 283510002;
					public const int ConnectionReference = 283510003;
                }
                public struct Statecode
                {
					public const int Active = 0;
					public const int Inactive = 1;
                }
                public struct Statuscode
                {
					public const int Active = 1;
					public const int Inactive = 2;
					public const int Merge = 283510001;
					public const int Finalize = 283510002;
					public const int Failure = 283510003;
					public const int Close = 283510004;
                }
		}
		#endregion

		#region LogicalNames
		public static class LogicalNames
		{
				public const string DgtWorkbenchId = "dgt_workbenchid";
				public const string CreatedBy = "createdby";
				public const string CreatedOn = "createdon";
				public const string CreatedOnBehalfBy = "createdonbehalfby";
				public const string DgtCarrierId = "dgt_carrier_id";
				public const string DgtName = "dgt_name";
				public const string DgtNatureSet = "dgt_nature_set";
				public const string DgtSolutionfriendlyname = "dgt_solutionfriendlyname";
				public const string DgtSolutionid = "dgt_solutionid";
				public const string DgtSolutionuniquename = "dgt_solutionuniquename";
				public const string DgtSolutionversion = "dgt_solutionversion";
				public const string DgtTargetCarrierId = "dgt_target_carrier_id";
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
				public const string DgtWorkbenchAsyncOperations = "dgt_workbench_AsyncOperations";
				public const string DgtWorkbenchBulkDeleteFailures = "dgt_workbench_BulkDeleteFailures";
				public const string DgtWorkbenchMailboxTrackingFolders = "dgt_workbench_MailboxTrackingFolders";
				public const string DgtWorkbenchPrincipalObjectAttributeAccesses = "dgt_workbench_PrincipalObjectAttributeAccesses";
				public const string DgtWorkbenchProcessSession = "dgt_workbench_ProcessSession";
				public const string DgtWorkbenchSyncErrors = "dgt_workbench_SyncErrors";
				public const string DgtWorkbenchToCarrierOnWorkbench = "dgt_workbench_to_carrier_on_workbench";
				public const string DgtWorkbenchToWorkbenchHistory = "dgt_workbench_to_workbench_history";
				public const string DgtWorkbenchUserEntityInstanceDatas = "dgt_workbench_UserEntityInstanceDatas";
            }

            public static class ManyToOne
            {
				public const string BusinessUnitDgtWorkbench = "business_unit_dgt_workbench";
				public const string DgtCarrierToWorkbenchOnCarrier = "dgt_carrier_to_workbench_on_carrier";
				public const string DgtCarrierToWorkbenchOnTargetCarrier = "dgt_carrier_to_workbench_on_target_carrier";
				public const string LkDgtWorkbenchCreatedby = "lk_dgt_workbench_createdby";
				public const string LkDgtWorkbenchCreatedonbehalfby = "lk_dgt_workbench_createdonbehalfby";
				public const string LkDgtWorkbenchModifiedby = "lk_dgt_workbench_modifiedby";
				public const string LkDgtWorkbenchModifiedonbehalfby = "lk_dgt_workbench_modifiedonbehalfby";
				public const string OwnerDgtWorkbench = "owner_dgt_workbench";
				public const string TeamDgtWorkbench = "team_dgt_workbench";
				public const string UserDgtWorkbench = "user_dgt_workbench";
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
        public static DgtWorkbench Retrieve(IOrganizationService service, Guid id)
        {
            return Retrieve(service,id, new ColumnSet(true));
        }

        public static DgtWorkbench Retrieve(IOrganizationService service, Guid id, ColumnSet columnSet)
        {
            return service.Retrieve("dgt_workbench", id, columnSet).ToEntity<DgtWorkbench>();
        }

        public DgtWorkbench GetChangedEntity()
        {
            if (_trackChanges)
            {
                var attr = new AttributeCollection();
                foreach (var attrName in _changedProperties.Value.Select(changedProperty => ((AttributeLogicalNameAttribute) GetType().GetProperty(changedProperty)!.GetCustomAttribute(typeof (AttributeLogicalNameAttribute))!).LogicalName).Where(attrName => Contains(attrName)))
                {
                    attr.Add(attrName,this[attrName]);
                }
                return new  DgtWorkbench(Id) {Attributes = attr };
            }
            return this;
        }
        #endregion
	}

	#region Context
	public partial class DataContext
	{
		public IQueryable<DgtWorkbench> DgtWorkbenchSet
		{
			get
			{
				return CreateQuery<DgtWorkbench>();
			}
		}
	}
	#endregion
}
