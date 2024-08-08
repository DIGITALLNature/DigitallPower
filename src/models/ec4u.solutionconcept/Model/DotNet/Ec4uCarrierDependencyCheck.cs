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
	[EntityLogicalNameAttribute("ec4u_carrier_dependency_check")]
	[System.CodeDom.Compiler.GeneratedCode("dgtp", "2023")]
    [ExcludeFromCodeCoverage]
	public partial class Ec4uCarrierDependencyCheck : Entity, INotifyPropertyChanging, INotifyPropertyChanged
    {
	    #region ctor
		[DebuggerNonUserCode]
		public Ec4uCarrierDependencyCheck() : this(false)
        {
        }

        [DebuggerNonUserCode]
		public Ec4uCarrierDependencyCheck(bool trackChanges = false) : base(EntityLogicalName)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public Ec4uCarrierDependencyCheck(Guid id, bool trackChanges = false) : base(EntityLogicalName,id)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public Ec4uCarrierDependencyCheck(KeyAttributeCollection keyAttributes, bool trackChanges = false) : base(EntityLogicalName,keyAttributes)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public Ec4uCarrierDependencyCheck(string keyName, object keyValue, bool trackChanges = false) : base(EntityLogicalName, keyName, keyValue)
        {
			_trackChanges = trackChanges;
        }
        #endregion

		#region fields
        private readonly bool _trackChanges;
        private readonly Lazy<HashSet<string>> _changedProperties = new Lazy<HashSet<string>>();
        #endregion

        #region consts
        public const string EntityLogicalName = "ec4u_carrier_dependency_check";
        public const string PrimaryNameAttribute = "ec4u_cdc_checkref";
        public const int EntityTypeCode = 10526;
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
		[AttributeLogicalNameAttribute("ec4u_carrier_dependency_checkid")]
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
				Ec4uCarrierDependencyCheckId = value;
			}
		}

		/// <summary>
		/// Unique identifier for entity instances
		/// </summary>
		[AttributeLogicalName("ec4u_carrier_dependency_checkid")]
        public Guid? Ec4uCarrierDependencyCheckId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<Guid?>("ec4u_carrier_dependency_checkid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(Ec4uCarrierDependencyCheckId));
                SetAttributeValue("ec4u_carrier_dependency_checkid", value);
				if (value.HasValue)
				{
					base.Id = value.Value;
				}
				else
				{
					base.Id = System.Guid.Empty;
				}
                OnPropertyChanged(nameof(Ec4uCarrierDependencyCheckId));
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

		
		[AttributeLogicalName("ec4u_cdc_carrier_id")]
        public EntityReference? Ec4uCdcCarrierId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<EntityReference?>("ec4u_cdc_carrier_id");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(Ec4uCdcCarrierId));
                SetAttributeValue("ec4u_cdc_carrier_id", value);
                OnPropertyChanged(nameof(Ec4uCdcCarrierId));
            }
        }

		/// <summary>
		/// The Carrier Check Ref
		/// </summary>
		[AttributeLogicalName("ec4u_cdc_checkref")]
        public string? Ec4uCdcCheckref
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("ec4u_cdc_checkref");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(Ec4uCdcCheckref));
                SetAttributeValue("ec4u_cdc_checkref", value);
                OnPropertyChanged(nameof(Ec4uCdcCheckref));
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
		/// Status of the Carrier Dependency Check
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
		/// Reason for the status of the Carrier Dependency Check
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
		/// <summary>
		/// 1:N ec4u_carrier_dependency_check_to_carrier_missing_dependency_on_carrier_dependency_check_id
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("ec4u_carrier_dependency_check_to_carrier_missing_dependency_on_carrier_dependency_check_id")]
		public System.Collections.Generic.IEnumerable<Ec4uCarrierMissingDependency> Ec4uCarrierDependencyCheckToCarrierMissingDependencyOnCarrierDependencyCheckId
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Ec4uCarrierMissingDependency>("ec4u_carrier_dependency_check_to_carrier_missing_dependency_on_carrier_dependency_check_id", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("Ec4uCarrierDependencyCheckToCarrierMissingDependencyOnCarrierDependencyCheckId");
				this.SetRelatedEntities<Ec4uCarrierMissingDependency>("ec4u_carrier_dependency_check_to_carrier_missing_dependency_on_carrier_dependency_check_id", null, value);
				this.OnPropertyChanged("Ec4uCarrierDependencyCheckToCarrierMissingDependencyOnCarrierDependencyCheckId");
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
					public const int Inactive = 2;
                }
		}
		#endregion

		#region LogicalNames
		public static class LogicalNames
		{
				public const string Ec4uCarrierDependencyCheckId = "ec4u_carrier_dependency_checkid";
				public const string CreatedBy = "createdby";
				public const string CreatedOn = "createdon";
				public const string CreatedOnBehalfBy = "createdonbehalfby";
				public const string Ec4uCdcCarrierId = "ec4u_cdc_carrier_id";
				public const string Ec4uCdcCheckref = "ec4u_cdc_checkref";
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
				public const string Ec4uCarrierDependencyCheckAsyncOperations = "ec4u_carrier_dependency_check_AsyncOperations";
				public const string Ec4uCarrierDependencyCheckBulkDeleteFailures = "ec4u_carrier_dependency_check_BulkDeleteFailures";
				public const string Ec4uCarrierDependencyCheckMailboxTrackingFolders = "ec4u_carrier_dependency_check_MailboxTrackingFolders";
				public const string Ec4uCarrierDependencyCheckPrincipalObjectAttributeAccesses = "ec4u_carrier_dependency_check_PrincipalObjectAttributeAccesses";
				public const string Ec4uCarrierDependencyCheckProcessSession = "ec4u_carrier_dependency_check_ProcessSession";
				public const string Ec4uCarrierDependencyCheckSyncErrors = "ec4u_carrier_dependency_check_SyncErrors";
				public const string Ec4uCarrierDependencyCheckToCarrierMissingDependencyOnCarrierDependencyCheckId = "ec4u_carrier_dependency_check_to_carrier_missing_dependency_on_carrier_dependency_check_id";
				public const string Ec4uCarrierDependencyCheckUserEntityInstanceDatas = "ec4u_carrier_dependency_check_UserEntityInstanceDatas";
            }

            public static class ManyToOne
            {
				public const string BusinessUnitEc4uCarrierDependencyCheck = "business_unit_ec4u_carrier_dependency_check";
				public const string Ec4uCarrierToCarrierDependencyCheckOnCarrierId = "ec4u_carrier_to_carrier_dependency_check_on_carrier_id";
				public const string LkEc4uCarrierDependencyCheckCreatedby = "lk_ec4u_carrier_dependency_check_createdby";
				public const string LkEc4uCarrierDependencyCheckCreatedonbehalfby = "lk_ec4u_carrier_dependency_check_createdonbehalfby";
				public const string LkEc4uCarrierDependencyCheckModifiedby = "lk_ec4u_carrier_dependency_check_modifiedby";
				public const string LkEc4uCarrierDependencyCheckModifiedonbehalfby = "lk_ec4u_carrier_dependency_check_modifiedonbehalfby";
				public const string OwnerEc4uCarrierDependencyCheck = "owner_ec4u_carrier_dependency_check";
				public const string TeamEc4uCarrierDependencyCheck = "team_ec4u_carrier_dependency_check";
				public const string UserEc4uCarrierDependencyCheck = "user_ec4u_carrier_dependency_check";
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
        public static Ec4uCarrierDependencyCheck Retrieve(IOrganizationService service, Guid id)
        {
            return Retrieve(service,id, new ColumnSet(true));
        }

        public static Ec4uCarrierDependencyCheck Retrieve(IOrganizationService service, Guid id, ColumnSet columnSet)
        {
            return service.Retrieve("ec4u_carrier_dependency_check", id, columnSet).ToEntity<Ec4uCarrierDependencyCheck>();
        }

        public Ec4uCarrierDependencyCheck GetChangedEntity()
        {
            if (_trackChanges)
            {
                var attr = new AttributeCollection();
                foreach (var attrName in _changedProperties.Value.Select(changedProperty => ((AttributeLogicalNameAttribute) GetType().GetProperty(changedProperty)!.GetCustomAttribute(typeof (AttributeLogicalNameAttribute))!).LogicalName).Where(attrName => Contains(attrName)))
                {
                    attr.Add(attrName,this[attrName]);
                }
                return new  Ec4uCarrierDependencyCheck(Id) {Attributes = attr };
            }
            return this;
        }
        #endregion
	}

	#region Context
	public partial class DataContext
	{
		public IQueryable<Ec4uCarrierDependencyCheck> Ec4uCarrierDependencyCheckSet
		{
			get
			{
				return CreateQuery<Ec4uCarrierDependencyCheck>();
			}
		}
	}
	#endregion
}
