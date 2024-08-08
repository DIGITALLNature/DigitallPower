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
	[EntityLogicalNameAttribute("dgt_carrier_dependency_check")]
	[System.CodeDom.Compiler.GeneratedCode("dgtp", "2023")]
    [ExcludeFromCodeCoverage]
	public partial class DgtCarrierDependencyCheck : Entity, INotifyPropertyChanging, INotifyPropertyChanged
    {
	    #region ctor
		[DebuggerNonUserCode]
		public DgtCarrierDependencyCheck() : this(false)
        {
        }

        [DebuggerNonUserCode]
		public DgtCarrierDependencyCheck(bool trackChanges = false) : base(EntityLogicalName)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public DgtCarrierDependencyCheck(Guid id, bool trackChanges = false) : base(EntityLogicalName,id)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public DgtCarrierDependencyCheck(KeyAttributeCollection keyAttributes, bool trackChanges = false) : base(EntityLogicalName,keyAttributes)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public DgtCarrierDependencyCheck(string keyName, object keyValue, bool trackChanges = false) : base(EntityLogicalName, keyName, keyValue)
        {
			_trackChanges = trackChanges;
        }
        #endregion

		#region fields
        private readonly bool _trackChanges;
        private readonly Lazy<HashSet<string>> _changedProperties = new Lazy<HashSet<string>>();
        #endregion

        #region consts
        public const string EntityLogicalName = "dgt_carrier_dependency_check";
        public const string PrimaryNameAttribute = "dgt_checkref";
        public const int EntityTypeCode = 10424;
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
		[AttributeLogicalNameAttribute("dgt_carrier_dependency_checkid")]
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
				DgtCarrierDependencyCheckId = value;
			}
		}

		/// <summary>
		/// Unique identifier for entity instances
		/// </summary>
		[AttributeLogicalName("dgt_carrier_dependency_checkid")]
        public Guid? DgtCarrierDependencyCheckId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<Guid?>("dgt_carrier_dependency_checkid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(DgtCarrierDependencyCheckId));
                SetAttributeValue("dgt_carrier_dependency_checkid", value);
				if (value.HasValue)
				{
					base.Id = value.Value;
				}
				else
				{
					base.Id = System.Guid.Empty;
				}
                OnPropertyChanged(nameof(DgtCarrierDependencyCheckId));
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
		/// The Carrier Check Ref
		/// </summary>
		[AttributeLogicalName("dgt_checkref")]
        public string? DgtCheckref
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("dgt_checkref");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(DgtCheckref));
                SetAttributeValue("dgt_checkref", value);
                OnPropertyChanged(nameof(DgtCheckref));
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
		/// Unique identifier for the organization
		/// </summary>
		[AttributeLogicalName("organizationid")]
        public EntityReference? OrganizationId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<EntityReference?>("organizationid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(OrganizationId));
                SetAttributeValue("organizationid", value);
                OnPropertyChanged(nameof(OrganizationId));
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
		/// 1:N dgt_carrier_dependency_check_AsyncOperations
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("dgt_carrier_dependency_check_AsyncOperations")]
		public System.Collections.Generic.IEnumerable<AsyncOperation> DgtCarrierDependencyCheckAsyncOperations
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<AsyncOperation>("dgt_carrier_dependency_check_AsyncOperations", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("DgtCarrierDependencyCheckAsyncOperations");
				this.SetRelatedEntities<AsyncOperation>("dgt_carrier_dependency_check_AsyncOperations", null, value);
				this.OnPropertyChanged("DgtCarrierDependencyCheckAsyncOperations");
			}
		}

		/// <summary>
		/// 1:N dgt_carrier_dependency_check_to_missing_check
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("dgt_carrier_dependency_check_to_missing_check")]
		public System.Collections.Generic.IEnumerable<DgtCarrierMissingDependency> DgtCarrierDependencyCheckToMissingCheck
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<DgtCarrierMissingDependency>("dgt_carrier_dependency_check_to_missing_check", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("DgtCarrierDependencyCheckToMissingCheck");
				this.SetRelatedEntities<DgtCarrierMissingDependency>("dgt_carrier_dependency_check_to_missing_check", null, value);
				this.OnPropertyChanged("DgtCarrierDependencyCheckToMissingCheck");
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
				public const string DgtCarrierDependencyCheckId = "dgt_carrier_dependency_checkid";
				public const string CreatedBy = "createdby";
				public const string CreatedOn = "createdon";
				public const string CreatedOnBehalfBy = "createdonbehalfby";
				public const string DgtCarrierId = "dgt_carrier_id";
				public const string DgtCheckref = "dgt_checkref";
				public const string ImportSequenceNumber = "importsequencenumber";
				public const string ModifiedBy = "modifiedby";
				public const string ModifiedOn = "modifiedon";
				public const string ModifiedOnBehalfBy = "modifiedonbehalfby";
				public const string OrganizationId = "organizationid";
				public const string OverriddenCreatedOn = "overriddencreatedon";
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
				public const string DgtCarrierDependencyCheckAsyncOperations = "dgt_carrier_dependency_check_AsyncOperations";
				public const string DgtCarrierDependencyCheckBulkDeleteFailures = "dgt_carrier_dependency_check_BulkDeleteFailures";
				public const string DgtCarrierDependencyCheckMailboxTrackingFolders = "dgt_carrier_dependency_check_MailboxTrackingFolders";
				public const string DgtCarrierDependencyCheckPrincipalObjectAttributeAccesses = "dgt_carrier_dependency_check_PrincipalObjectAttributeAccesses";
				public const string DgtCarrierDependencyCheckProcessSession = "dgt_carrier_dependency_check_ProcessSession";
				public const string DgtCarrierDependencyCheckSyncErrors = "dgt_carrier_dependency_check_SyncErrors";
				public const string DgtCarrierDependencyCheckToMissingCheck = "dgt_carrier_dependency_check_to_missing_check";
				public const string DgtCarrierDependencyCheckUserEntityInstanceDatas = "dgt_carrier_dependency_check_UserEntityInstanceDatas";
            }

            public static class ManyToOne
            {
				public const string DgtCarrierToCarrierDependencyCheck = "dgt_carrier_to_carrier_dependency_check";
				public const string LkDgtCarrierDependencyCheckCreatedby = "lk_dgt_carrier_dependency_check_createdby";
				public const string LkDgtCarrierDependencyCheckCreatedonbehalfby = "lk_dgt_carrier_dependency_check_createdonbehalfby";
				public const string LkDgtCarrierDependencyCheckModifiedby = "lk_dgt_carrier_dependency_check_modifiedby";
				public const string LkDgtCarrierDependencyCheckModifiedonbehalfby = "lk_dgt_carrier_dependency_check_modifiedonbehalfby";
				public const string OrganizationDgtCarrierDependencyCheck = "organization_dgt_carrier_dependency_check";
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
        public static DgtCarrierDependencyCheck Retrieve(IOrganizationService service, Guid id)
        {
            return Retrieve(service,id, new ColumnSet(true));
        }

        public static DgtCarrierDependencyCheck Retrieve(IOrganizationService service, Guid id, ColumnSet columnSet)
        {
            return service.Retrieve("dgt_carrier_dependency_check", id, columnSet).ToEntity<DgtCarrierDependencyCheck>();
        }

        public DgtCarrierDependencyCheck GetChangedEntity()
        {
            if (_trackChanges)
            {
                var attr = new AttributeCollection();
                foreach (var attrName in _changedProperties.Value.Select(changedProperty => ((AttributeLogicalNameAttribute) GetType().GetProperty(changedProperty)!.GetCustomAttribute(typeof (AttributeLogicalNameAttribute))!).LogicalName).Where(attrName => Contains(attrName)))
                {
                    attr.Add(attrName,this[attrName]);
                }
                return new  DgtCarrierDependencyCheck(Id) {Attributes = attr };
            }
            return this;
        }
        #endregion
	}

	#region Context
	public partial class DataContext
	{
		public IQueryable<DgtCarrierDependencyCheck> DgtCarrierDependencyCheckSet
		{
			get
			{
				return CreateQuery<DgtCarrierDependencyCheck>();
			}
		}
	}
	#endregion
}
