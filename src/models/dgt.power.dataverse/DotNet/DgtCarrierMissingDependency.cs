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
	[EntityLogicalNameAttribute("dgt_carrier_missing_dependency")]
	[System.CodeDom.Compiler.GeneratedCode("dgtp", "2023")]
    [ExcludeFromCodeCoverage]
	public partial class DgtCarrierMissingDependency : Entity, INotifyPropertyChanging, INotifyPropertyChanged
    {
	    #region ctor
		[DebuggerNonUserCode]
		public DgtCarrierMissingDependency() : this(false)
        {
        }

        [DebuggerNonUserCode]
		public DgtCarrierMissingDependency(bool trackChanges = false) : base(EntityLogicalName)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public DgtCarrierMissingDependency(Guid id, bool trackChanges = false) : base(EntityLogicalName,id)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public DgtCarrierMissingDependency(KeyAttributeCollection keyAttributes, bool trackChanges = false) : base(EntityLogicalName,keyAttributes)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public DgtCarrierMissingDependency(string keyName, object keyValue, bool trackChanges = false) : base(EntityLogicalName, keyName, keyValue)
        {
			_trackChanges = trackChanges;
        }
        #endregion

		#region fields
        private readonly bool _trackChanges;
        private readonly Lazy<HashSet<string>> _changedProperties = new Lazy<HashSet<string>>();
        #endregion

        #region consts
        public const string EntityLogicalName = "dgt_carrier_missing_dependency";
        public const string PrimaryNameAttribute = "dgt_component";
        public const int EntityTypeCode = 10425;
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
		[AttributeLogicalNameAttribute("dgt_carrier_missing_dependencyid")]
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
				DgtCarrierMissingDependencyId = value;
			}
		}

		/// <summary>
		/// Unique identifier for entity instances
		/// </summary>
		[AttributeLogicalName("dgt_carrier_missing_dependencyid")]
        public Guid? DgtCarrierMissingDependencyId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<Guid?>("dgt_carrier_missing_dependencyid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(DgtCarrierMissingDependencyId));
                SetAttributeValue("dgt_carrier_missing_dependencyid", value);
				if (value.HasValue)
				{
					base.Id = value.Value;
				}
				else
				{
					base.Id = System.Guid.Empty;
				}
                OnPropertyChanged(nameof(DgtCarrierMissingDependencyId));
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

		
		[AttributeLogicalName("dgt_carrier_dependency_check_id")]
        public EntityReference? DgtCarrierDependencyCheckId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<EntityReference?>("dgt_carrier_dependency_check_id");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(DgtCarrierDependencyCheckId));
                SetAttributeValue("dgt_carrier_dependency_check_id", value);
                OnPropertyChanged(nameof(DgtCarrierDependencyCheckId));
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
		/// The Carrier Component
		/// </summary>
		[AttributeLogicalName("dgt_component")]
        public string? DgtComponent
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("dgt_component");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(DgtComponent));
                SetAttributeValue("dgt_component", value);
                OnPropertyChanged(nameof(DgtComponent));
            }
        }

		
		[AttributeLogicalName("dgt_required_component_objectid")]
        public string? DgtRequiredComponentObjectid
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("dgt_required_component_objectid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(DgtRequiredComponentObjectid));
                SetAttributeValue("dgt_required_component_objectid", value);
                OnPropertyChanged(nameof(DgtRequiredComponentObjectid));
            }
        }

		
		[AttributeLogicalName("dgt_required_component_type_name")]
        public string? DgtRequiredComponentTypeName
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("dgt_required_component_type_name");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(DgtRequiredComponentTypeName));
                SetAttributeValue("dgt_required_component_type_name", value);
                OnPropertyChanged(nameof(DgtRequiredComponentTypeName));
            }
        }

		
		[AttributeLogicalName("dgt_required_component_type_no")]
        public int? DgtRequiredComponentTypeNo
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<int?>("dgt_required_component_type_no");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(DgtRequiredComponentTypeNo));
                SetAttributeValue("dgt_required_component_type_no", value);
                OnPropertyChanged(nameof(DgtRequiredComponentTypeNo));
            }
        }

		
		[AttributeLogicalName("dgt_solution_component_recordid")]
        public string? DgtSolutionComponentRecordid
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("dgt_solution_component_recordid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(DgtSolutionComponentRecordid));
                SetAttributeValue("dgt_solution_component_recordid", value);
                OnPropertyChanged(nameof(DgtSolutionComponentRecordid));
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
		/// Status of the Carrier Missing Dependency
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
		/// Reason for the status of the Carrier Missing Dependency
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
		/// 1:N dgt_carrier_missing_dependency_AsyncOperations
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("dgt_carrier_missing_dependency_AsyncOperations")]
		public System.Collections.Generic.IEnumerable<AsyncOperation> DgtCarrierMissingDependencyAsyncOperations
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<AsyncOperation>("dgt_carrier_missing_dependency_AsyncOperations", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("DgtCarrierMissingDependencyAsyncOperations");
				this.SetRelatedEntities<AsyncOperation>("dgt_carrier_missing_dependency_AsyncOperations", null, value);
				this.OnPropertyChanged("DgtCarrierMissingDependencyAsyncOperations");
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
				public const string DgtCarrierMissingDependencyId = "dgt_carrier_missing_dependencyid";
				public const string CreatedBy = "createdby";
				public const string CreatedOn = "createdon";
				public const string CreatedOnBehalfBy = "createdonbehalfby";
				public const string DgtCarrierDependencyCheckId = "dgt_carrier_dependency_check_id";
				public const string DgtCarrierId = "dgt_carrier_id";
				public const string DgtComponent = "dgt_component";
				public const string DgtRequiredComponentObjectid = "dgt_required_component_objectid";
				public const string DgtRequiredComponentTypeName = "dgt_required_component_type_name";
				public const string DgtRequiredComponentTypeNo = "dgt_required_component_type_no";
				public const string DgtSolutionComponentRecordid = "dgt_solution_component_recordid";
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
				public const string DgtCarrierMissingDependencyAsyncOperations = "dgt_carrier_missing_dependency_AsyncOperations";
				public const string DgtCarrierMissingDependencyBulkDeleteFailures = "dgt_carrier_missing_dependency_BulkDeleteFailures";
				public const string DgtCarrierMissingDependencyMailboxTrackingFolders = "dgt_carrier_missing_dependency_MailboxTrackingFolders";
				public const string DgtCarrierMissingDependencyPrincipalObjectAttributeAccesses = "dgt_carrier_missing_dependency_PrincipalObjectAttributeAccesses";
				public const string DgtCarrierMissingDependencyProcessSession = "dgt_carrier_missing_dependency_ProcessSession";
				public const string DgtCarrierMissingDependencySyncErrors = "dgt_carrier_missing_dependency_SyncErrors";
				public const string DgtCarrierMissingDependencyUserEntityInstanceDatas = "dgt_carrier_missing_dependency_UserEntityInstanceDatas";
            }

            public static class ManyToOne
            {
				public const string DgtCarrierDependencyCheckToMissingCheck = "dgt_carrier_dependency_check_to_missing_check";
				public const string DgtCarrierToCarrierMissingDependency = "dgt_carrier_to_carrier_missing_dependency";
				public const string LkDgtCarrierMissingDependencyCreatedby = "lk_dgt_carrier_missing_dependency_createdby";
				public const string LkDgtCarrierMissingDependencyCreatedonbehalfby = "lk_dgt_carrier_missing_dependency_createdonbehalfby";
				public const string LkDgtCarrierMissingDependencyModifiedby = "lk_dgt_carrier_missing_dependency_modifiedby";
				public const string LkDgtCarrierMissingDependencyModifiedonbehalfby = "lk_dgt_carrier_missing_dependency_modifiedonbehalfby";
				public const string OrganizationDgtCarrierMissingDependency = "organization_dgt_carrier_missing_dependency";
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
        public static DgtCarrierMissingDependency Retrieve(IOrganizationService service, Guid id)
        {
            return Retrieve(service,id, new ColumnSet(true));
        }

        public static DgtCarrierMissingDependency Retrieve(IOrganizationService service, Guid id, ColumnSet columnSet)
        {
            return service.Retrieve("dgt_carrier_missing_dependency", id, columnSet).ToEntity<DgtCarrierMissingDependency>();
        }

        public DgtCarrierMissingDependency GetChangedEntity()
        {
            if (_trackChanges)
            {
                var attr = new AttributeCollection();
                foreach (var attrName in _changedProperties.Value.Select(changedProperty => ((AttributeLogicalNameAttribute) GetType().GetProperty(changedProperty)!.GetCustomAttribute(typeof (AttributeLogicalNameAttribute))!).LogicalName).Where(attrName => Contains(attrName)))
                {
                    attr.Add(attrName,this[attrName]);
                }
                return new  DgtCarrierMissingDependency(Id) {Attributes = attr };
            }
            return this;
        }
        #endregion
	}

	#region Context
	public partial class DataContext
	{
		public IQueryable<DgtCarrierMissingDependency> DgtCarrierMissingDependencySet
		{
			get
			{
				return CreateQuery<DgtCarrierMissingDependency>();
			}
		}
	}
	#endregion
}
