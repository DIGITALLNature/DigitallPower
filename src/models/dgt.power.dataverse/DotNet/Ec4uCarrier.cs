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
	[EntityLogicalNameAttribute("ec4u_carrier")]
	[System.CodeDom.Compiler.GeneratedCode("dgtp", "2023")]
    [ExcludeFromCodeCoverage]
	public partial class Ec4uCarrier : Entity, INotifyPropertyChanging, INotifyPropertyChanged
    {
	    #region ctor
		[DebuggerNonUserCode]
		public Ec4uCarrier() : this(false)
        {
        }

        [DebuggerNonUserCode]
		public Ec4uCarrier(bool trackChanges = false) : base(EntityLogicalName)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public Ec4uCarrier(Guid id, bool trackChanges = false) : base(EntityLogicalName,id)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public Ec4uCarrier(KeyAttributeCollection keyAttributes, bool trackChanges = false) : base(EntityLogicalName,keyAttributes)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public Ec4uCarrier(string keyName, object keyValue, bool trackChanges = false) : base(EntityLogicalName, keyName, keyValue)
        {
			_trackChanges = trackChanges;
        }
        #endregion

		#region fields
        private readonly bool _trackChanges;
        private readonly Lazy<HashSet<string>> _changedProperties = new Lazy<HashSet<string>>();
        #endregion

        #region consts
        public const string EntityLogicalName = "ec4u_carrier";
        public const string PrimaryNameAttribute = "ec4u_car_reference";
        public const int EntityTypeCode = 10740;
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
		[AttributeLogicalNameAttribute("ec4u_carrierid")]
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
				Ec4uCarrierId = value;
			}
		}

		/// <summary>
		/// Unique identifier for entity instances
		/// </summary>
		[AttributeLogicalName("ec4u_carrierid")]
        public Guid? Ec4uCarrierId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<Guid?>("ec4u_carrierid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(Ec4uCarrierId));
                SetAttributeValue("ec4u_carrierid", value);
				if (value.HasValue)
				{
					base.Id = value.Value;
				}
				else
				{
					base.Id = System.Guid.Empty;
				}
                OnPropertyChanged(nameof(Ec4uCarrierId));
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

		
		[AttributeLogicalName("ec4u_car_handshake_ts")]
        public DateTime? Ec4uCarHandshakeTs
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<DateTime?>("ec4u_car_handshake_ts");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(Ec4uCarHandshakeTs));
                SetAttributeValue("ec4u_car_handshake_ts", value);
                OnPropertyChanged(nameof(Ec4uCarHandshakeTs));
            }
        }

		
		[AttributeLogicalName("ec4u_car_locked_opt")]
        public bool? Ec4uCarLockedOpt
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<bool?>("ec4u_car_locked_opt");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(Ec4uCarLockedOpt));
                SetAttributeValue("ec4u_car_locked_opt", value);
                OnPropertyChanged(nameof(Ec4uCarLockedOpt));
            }
        }

		/// <summary>
		/// The Carrier Reference
		/// </summary>
		[AttributeLogicalName("ec4u_car_reference")]
        public string? Ec4uCarReference
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("ec4u_car_reference");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(Ec4uCarReference));
                SetAttributeValue("ec4u_car_reference", value);
                OnPropertyChanged(nameof(Ec4uCarReference));
            }
        }

		
		[AttributeLogicalName("ec4u_car_solutionfriendlyname")]
        public string? Ec4uCarSolutionfriendlyname
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("ec4u_car_solutionfriendlyname");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(Ec4uCarSolutionfriendlyname));
                SetAttributeValue("ec4u_car_solutionfriendlyname", value);
                OnPropertyChanged(nameof(Ec4uCarSolutionfriendlyname));
            }
        }

		
		[AttributeLogicalName("ec4u_car_solutionid")]
        public string? Ec4uCarSolutionid
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("ec4u_car_solutionid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(Ec4uCarSolutionid));
                SetAttributeValue("ec4u_car_solutionid", value);
                OnPropertyChanged(nameof(Ec4uCarSolutionid));
            }
        }

		
		[AttributeLogicalName("ec4u_car_solutionuniquename")]
        public string? Ec4uCarSolutionuniquename
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("ec4u_car_solutionuniquename");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(Ec4uCarSolutionuniquename));
                SetAttributeValue("ec4u_car_solutionuniquename", value);
                OnPropertyChanged(nameof(Ec4uCarSolutionuniquename));
            }
        }

		
		[AttributeLogicalName("ec4u_car_solutionversion")]
        public string? Ec4uCarSolutionversion
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("ec4u_car_solutionversion");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(Ec4uCarSolutionversion));
                SetAttributeValue("ec4u_car_solutionversion", value);
                OnPropertyChanged(nameof(Ec4uCarSolutionversion));
            }
        }

		
		[AttributeLogicalName("ec4u_car_transport_order_no")]
        public int? Ec4uCarTransportOrderNo
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<int?>("ec4u_car_transport_order_no");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(Ec4uCarTransportOrderNo));
                SetAttributeValue("ec4u_car_transport_order_no", value);
                OnPropertyChanged(nameof(Ec4uCarTransportOrderNo));
            }
        }

		
		[AttributeLogicalName("ec4u_car_workbench_id")]
        public EntityReference? Ec4uCarWorkbenchId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<EntityReference?>("ec4u_car_workbench_id");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(Ec4uCarWorkbenchId));
                SetAttributeValue("ec4u_car_workbench_id", value);
                OnPropertyChanged(nameof(Ec4uCarWorkbenchId));
            }
        }

		
		[AttributeLogicalName("ec4u_constraint_mset")]
        public Microsoft.Xrm.Sdk.OptionSetValueCollection? Ec4uConstraintMset
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<Microsoft.Xrm.Sdk.OptionSetValueCollection?>("ec4u_constraint_mset");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(Ec4uConstraintMset));
                SetAttributeValue("ec4u_constraint_mset", value);
                OnPropertyChanged(nameof(Ec4uConstraintMset));
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
		/// Owner Id Type
		/// </summary>
		[AttributeLogicalName("owneridtype")]
        public string? OwnerIdType
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("owneridtype");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(OwnerIdType));
                SetAttributeValue("owneridtype", value);
                OnPropertyChanged(nameof(OwnerIdType));
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
		/// Contains the id of the process associated with the entity.
		/// </summary>
		[AttributeLogicalName("processid")]
        public Guid? Processid
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<Guid?>("processid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(Processid));
                SetAttributeValue("processid", value);
                OnPropertyChanged(nameof(Processid));
            }
        }

		/// <summary>
		/// Contains the id of the stage where the entity is located.
		/// </summary>
		[AttributeLogicalName("stageid")]
        public Guid? Stageid
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<Guid?>("stageid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(Stageid));
                SetAttributeValue("stageid", value);
                OnPropertyChanged(nameof(Stageid));
            }
        }

		/// <summary>
		/// Status of the Carrier
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
		/// Reason for the status of the Carrier
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
		/// A comma separated list of string values representing the unique identifiers of stages in a Business Process Flow Instance in the order that they occur.
		/// </summary>
		[AttributeLogicalName("traversedpath")]
        public string? Traversedpath
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("traversedpath");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(Traversedpath));
                SetAttributeValue("traversedpath", value);
                OnPropertyChanged(nameof(Traversedpath));
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
		/// <summary>
		/// 1:N ec4u_carrier_AsyncOperations
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("ec4u_carrier_AsyncOperations")]
		public System.Collections.Generic.IEnumerable<AsyncOperation> Ec4uCarrierAsyncOperations
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<AsyncOperation>("ec4u_carrier_AsyncOperations", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("Ec4uCarrierAsyncOperations");
				this.SetRelatedEntities<AsyncOperation>("ec4u_carrier_AsyncOperations", null, value);
				this.OnPropertyChanged("Ec4uCarrierAsyncOperations");
			}
		}

		#endregion

		#region Options
		public static class Options
		{
                public struct Ec4uCarLockedOpt
                {
                    public const bool No = false;
                    public const bool Yes = true;
                }
                public struct Ec4uConstraintMset
                {
					public const int PreventFlows = 596030000;
					public const int PreventManagedEntitiesWithAllAssets = 596030001;
					public const int PreventItemsWithouthActiveLayer = 596030002;
					public const int PreventPluginAssemblys = 596030003;
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
                }
		}
		#endregion

		#region LogicalNames
		public static class LogicalNames
		{
				public const string Ec4uCarrierId = "ec4u_carrierid";
				public const string CreatedBy = "createdby";
				public const string CreatedOn = "createdon";
				public const string CreatedOnBehalfBy = "createdonbehalfby";
				public const string Ec4uCarHandshakeTs = "ec4u_car_handshake_ts";
				public const string Ec4uCarLockedOpt = "ec4u_car_locked_opt";
				public const string Ec4uCarReference = "ec4u_car_reference";
				public const string Ec4uCarSolutionfriendlyname = "ec4u_car_solutionfriendlyname";
				public const string Ec4uCarSolutionid = "ec4u_car_solutionid";
				public const string Ec4uCarSolutionuniquename = "ec4u_car_solutionuniquename";
				public const string Ec4uCarSolutionversion = "ec4u_car_solutionversion";
				public const string Ec4uCarTransportOrderNo = "ec4u_car_transport_order_no";
				public const string Ec4uCarWorkbenchId = "ec4u_car_workbench_id";
				public const string Ec4uConstraintMset = "ec4u_constraint_mset";
				public const string ImportSequenceNumber = "importsequencenumber";
				public const string ModifiedBy = "modifiedby";
				public const string ModifiedOn = "modifiedon";
				public const string ModifiedOnBehalfBy = "modifiedonbehalfby";
				public const string OverriddenCreatedOn = "overriddencreatedon";
				public const string OwnerId = "ownerid";
				public const string OwnerIdType = "owneridtype";
				public const string OwningBusinessUnit = "owningbusinessunit";
				public const string OwningTeam = "owningteam";
				public const string OwningUser = "owninguser";
				public const string Processid = "processid";
				public const string Stageid = "stageid";
				public const string Statecode = "statecode";
				public const string Statuscode = "statuscode";
				public const string TimeZoneRuleVersionNumber = "timezoneruleversionnumber";
				public const string Traversedpath = "traversedpath";
				public const string UTCConversionTimeZoneCode = "utcconversiontimezonecode";
				public const string VersionNumber = "versionnumber";
		}
		#endregion

		#region Relations
        public static class Relations
        {
            public static class OneToMany
            {
				public const string Ec4uCarrierAsyncOperations = "ec4u_carrier_AsyncOperations";
				public const string Ec4uCarrierBulkDeleteFailures = "ec4u_carrier_BulkDeleteFailures";
				public const string Ec4uCarrierDuplicateBaseRecord = "ec4u_carrier_DuplicateBaseRecord";
				public const string Ec4uCarrierDuplicateMatchingRecord = "ec4u_carrier_DuplicateMatchingRecord";
				public const string Ec4uCarrierMailboxTrackingFolders = "ec4u_carrier_MailboxTrackingFolders";
				public const string Ec4uCarrierPrincipalObjectAttributeAccesses = "ec4u_carrier_PrincipalObjectAttributeAccesses";
				public const string Ec4uCarrierProcessSession = "ec4u_carrier_ProcessSession";
				public const string Ec4uCarrierSyncErrors = "ec4u_carrier_SyncErrors";
				public const string Ec4uCarrierToCarrierDependencyCheckOnCarrierId = "ec4u_carrier_to_carrier_dependency_check_on_carrier_id";
				public const string Ec4uCarrierToCarrierMissingDependencyOnCarrierId = "ec4u_carrier_to_carrier_missing_dependency_on_carrier_id";
				public const string Ec4uCarrierToWorkbenchHistoryOnCarrier = "ec4u_carrier_to_workbench_history_on_carrier";
				public const string Ec4uCarrierToWorkbenchOnCarrier = "ec4u_carrier_to_workbench_on_carrier";
				public const string Ec4uCarrierToWorkbenchOnTargetCarrier = "ec4u_carrier_to_workbench_on_target_carrier";
				public const string Ec4uCarrierUserEntityInstanceDatas = "ec4u_carrier_UserEntityInstanceDatas";
            }

            public static class ManyToOne
            {
				public const string BusinessUnitEc4uCarrier = "business_unit_ec4u_carrier";
				public const string Ec4uWorkbenchToCarrierOnWorkbench = "ec4u_workbench_to_carrier_on_workbench";
				public const string LkEc4uCarrierCreatedby = "lk_ec4u_carrier_createdby";
				public const string LkEc4uCarrierCreatedonbehalfby = "lk_ec4u_carrier_createdonbehalfby";
				public const string LkEc4uCarrierModifiedby = "lk_ec4u_carrier_modifiedby";
				public const string LkEc4uCarrierModifiedonbehalfby = "lk_ec4u_carrier_modifiedonbehalfby";
				public const string OwnerEc4uCarrier = "owner_ec4u_carrier";
				public const string ProcessstageEc4uCarrier = "processstage_ec4u_carrier";
				public const string TeamEc4uCarrier = "team_ec4u_carrier";
				public const string UserEc4uCarrier = "user_ec4u_carrier";
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
        public static Ec4uCarrier Retrieve(IOrganizationService service, Guid id)
        {
            return Retrieve(service,id, new ColumnSet(true));
        }

        public static Ec4uCarrier Retrieve(IOrganizationService service, Guid id, ColumnSet columnSet)
        {
            return service.Retrieve("ec4u_carrier", id, columnSet).ToEntity<Ec4uCarrier>();
        }

        public Ec4uCarrier GetChangedEntity()
        {
            if (_trackChanges)
            {
                var attr = new AttributeCollection();
                foreach (var attrName in _changedProperties.Value.Select(changedProperty => ((AttributeLogicalNameAttribute) GetType().GetProperty(changedProperty)!.GetCustomAttribute(typeof (AttributeLogicalNameAttribute))!).LogicalName).Where(attrName => Contains(attrName)))
                {
                    attr.Add(attrName,this[attrName]);
                }
                return new  Ec4uCarrier(Id) {Attributes = attr };
            }
            return this;
        }
        #endregion
	}

	#region Context
	public partial class DataContext
	{
		public IQueryable<Ec4uCarrier> Ec4uCarrierSet
		{
			get
			{
				return CreateQuery<Ec4uCarrier>();
			}
		}
	}
	#endregion
}
