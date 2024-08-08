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
	[EntityLogicalNameAttribute("ec4u_carrier_missing_dependency")]
	[System.CodeDom.Compiler.GeneratedCode("dgtp", "2023")]
    [ExcludeFromCodeCoverage]
	public partial class Ec4uCarrierMissingDependency : Entity, INotifyPropertyChanging, INotifyPropertyChanged
    {
	    #region ctor
		[DebuggerNonUserCode]
		public Ec4uCarrierMissingDependency() : this(false)
        {
        }

        [DebuggerNonUserCode]
		public Ec4uCarrierMissingDependency(bool trackChanges = false) : base(EntityLogicalName)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public Ec4uCarrierMissingDependency(Guid id, bool trackChanges = false) : base(EntityLogicalName,id)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public Ec4uCarrierMissingDependency(KeyAttributeCollection keyAttributes, bool trackChanges = false) : base(EntityLogicalName,keyAttributes)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public Ec4uCarrierMissingDependency(string keyName, object keyValue, bool trackChanges = false) : base(EntityLogicalName, keyName, keyValue)
        {
			_trackChanges = trackChanges;
        }
        #endregion

		#region fields
        private readonly bool _trackChanges;
        private readonly Lazy<HashSet<string>> _changedProperties = new Lazy<HashSet<string>>();
        #endregion

        #region consts
        public const string EntityLogicalName = "ec4u_carrier_missing_dependency";
        public const string PrimaryNameAttribute = "ec4u_cmd_component";
        public const int EntityTypeCode = 10527;
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
		[AttributeLogicalNameAttribute("ec4u_carrier_missing_dependencyid")]
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
				Ec4uCarrierMissingDependencyId = value;
			}
		}

		/// <summary>
		/// Unique identifier for entity instances
		/// </summary>
		[AttributeLogicalName("ec4u_carrier_missing_dependencyid")]
        public Guid? Ec4uCarrierMissingDependencyId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<Guid?>("ec4u_carrier_missing_dependencyid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(Ec4uCarrierMissingDependencyId));
                SetAttributeValue("ec4u_carrier_missing_dependencyid", value);
				if (value.HasValue)
				{
					base.Id = value.Value;
				}
				else
				{
					base.Id = System.Guid.Empty;
				}
                OnPropertyChanged(nameof(Ec4uCarrierMissingDependencyId));
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

		
		[AttributeLogicalName("ec4u_cmd_carrier_dependency_check_id")]
        public EntityReference? Ec4uCmdCarrierDependencyCheckId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<EntityReference?>("ec4u_cmd_carrier_dependency_check_id");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(Ec4uCmdCarrierDependencyCheckId));
                SetAttributeValue("ec4u_cmd_carrier_dependency_check_id", value);
                OnPropertyChanged(nameof(Ec4uCmdCarrierDependencyCheckId));
            }
        }

		
		[AttributeLogicalName("ec4u_cmd_carrier_id")]
        public EntityReference? Ec4uCmdCarrierId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<EntityReference?>("ec4u_cmd_carrier_id");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(Ec4uCmdCarrierId));
                SetAttributeValue("ec4u_cmd_carrier_id", value);
                OnPropertyChanged(nameof(Ec4uCmdCarrierId));
            }
        }

		/// <summary>
		/// The Carrier Component
		/// </summary>
		[AttributeLogicalName("ec4u_cmd_component")]
        public string? Ec4uCmdComponent
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("ec4u_cmd_component");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(Ec4uCmdComponent));
                SetAttributeValue("ec4u_cmd_component", value);
                OnPropertyChanged(nameof(Ec4uCmdComponent));
            }
        }

		
		[AttributeLogicalName("ec4u_cmd_required_component_objectid")]
        public string? Ec4uCmdRequiredComponentObjectid
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("ec4u_cmd_required_component_objectid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(Ec4uCmdRequiredComponentObjectid));
                SetAttributeValue("ec4u_cmd_required_component_objectid", value);
                OnPropertyChanged(nameof(Ec4uCmdRequiredComponentObjectid));
            }
        }

		
		[AttributeLogicalName("ec4u_cmd_required_component_type_name")]
        public string? Ec4uCmdRequiredComponentTypeName
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("ec4u_cmd_required_component_type_name");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(Ec4uCmdRequiredComponentTypeName));
                SetAttributeValue("ec4u_cmd_required_component_type_name", value);
                OnPropertyChanged(nameof(Ec4uCmdRequiredComponentTypeName));
            }
        }

		
		[AttributeLogicalName("ec4u_cmd_required_component_type_no")]
        public int? Ec4uCmdRequiredComponentTypeNo
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<int?>("ec4u_cmd_required_component_type_no");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(Ec4uCmdRequiredComponentTypeNo));
                SetAttributeValue("ec4u_cmd_required_component_type_no", value);
                OnPropertyChanged(nameof(Ec4uCmdRequiredComponentTypeNo));
            }
        }

		
		[AttributeLogicalName("ec4u_cmd_solution_component_recordid")]
        public string? Ec4uCmdSolutionComponentRecordid
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("ec4u_cmd_solution_component_recordid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(Ec4uCmdSolutionComponentRecordid));
                SetAttributeValue("ec4u_cmd_solution_component_recordid", value);
                OnPropertyChanged(nameof(Ec4uCmdSolutionComponentRecordid));
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
				public const string Ec4uCarrierMissingDependencyId = "ec4u_carrier_missing_dependencyid";
				public const string CreatedBy = "createdby";
				public const string CreatedOn = "createdon";
				public const string CreatedOnBehalfBy = "createdonbehalfby";
				public const string Ec4uCmdCarrierDependencyCheckId = "ec4u_cmd_carrier_dependency_check_id";
				public const string Ec4uCmdCarrierId = "ec4u_cmd_carrier_id";
				public const string Ec4uCmdComponent = "ec4u_cmd_component";
				public const string Ec4uCmdRequiredComponentObjectid = "ec4u_cmd_required_component_objectid";
				public const string Ec4uCmdRequiredComponentTypeName = "ec4u_cmd_required_component_type_name";
				public const string Ec4uCmdRequiredComponentTypeNo = "ec4u_cmd_required_component_type_no";
				public const string Ec4uCmdSolutionComponentRecordid = "ec4u_cmd_solution_component_recordid";
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
				public const string Ec4uCarrierMissingDependencyAsyncOperations = "ec4u_carrier_missing_dependency_AsyncOperations";
				public const string Ec4uCarrierMissingDependencyBulkDeleteFailures = "ec4u_carrier_missing_dependency_BulkDeleteFailures";
				public const string Ec4uCarrierMissingDependencyMailboxTrackingFolders = "ec4u_carrier_missing_dependency_MailboxTrackingFolders";
				public const string Ec4uCarrierMissingDependencyPrincipalObjectAttributeAccesses = "ec4u_carrier_missing_dependency_PrincipalObjectAttributeAccesses";
				public const string Ec4uCarrierMissingDependencyProcessSession = "ec4u_carrier_missing_dependency_ProcessSession";
				public const string Ec4uCarrierMissingDependencySyncErrors = "ec4u_carrier_missing_dependency_SyncErrors";
				public const string Ec4uCarrierMissingDependencyUserEntityInstanceDatas = "ec4u_carrier_missing_dependency_UserEntityInstanceDatas";
            }

            public static class ManyToOne
            {
				public const string BusinessUnitEc4uCarrierMissingDependency = "business_unit_ec4u_carrier_missing_dependency";
				public const string Ec4uCarrierDependencyCheckToCarrierMissingDependencyOnCarrierDependencyCheckId = "ec4u_carrier_dependency_check_to_carrier_missing_dependency_on_carrier_dependency_check_id";
				public const string Ec4uCarrierToCarrierMissingDependencyOnCarrierId = "ec4u_carrier_to_carrier_missing_dependency_on_carrier_id";
				public const string LkEc4uCarrierMissingDependencyCreatedby = "lk_ec4u_carrier_missing_dependency_createdby";
				public const string LkEc4uCarrierMissingDependencyCreatedonbehalfby = "lk_ec4u_carrier_missing_dependency_createdonbehalfby";
				public const string LkEc4uCarrierMissingDependencyModifiedby = "lk_ec4u_carrier_missing_dependency_modifiedby";
				public const string LkEc4uCarrierMissingDependencyModifiedonbehalfby = "lk_ec4u_carrier_missing_dependency_modifiedonbehalfby";
				public const string OwnerEc4uCarrierMissingDependency = "owner_ec4u_carrier_missing_dependency";
				public const string TeamEc4uCarrierMissingDependency = "team_ec4u_carrier_missing_dependency";
				public const string UserEc4uCarrierMissingDependency = "user_ec4u_carrier_missing_dependency";
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
        public static Ec4uCarrierMissingDependency Retrieve(IOrganizationService service, Guid id)
        {
            return Retrieve(service,id, new ColumnSet(true));
        }

        public static Ec4uCarrierMissingDependency Retrieve(IOrganizationService service, Guid id, ColumnSet columnSet)
        {
            return service.Retrieve("ec4u_carrier_missing_dependency", id, columnSet).ToEntity<Ec4uCarrierMissingDependency>();
        }

        public Ec4uCarrierMissingDependency GetChangedEntity()
        {
            if (_trackChanges)
            {
                var attr = new AttributeCollection();
                foreach (var attrName in _changedProperties.Value.Select(changedProperty => ((AttributeLogicalNameAttribute) GetType().GetProperty(changedProperty)!.GetCustomAttribute(typeof (AttributeLogicalNameAttribute))!).LogicalName).Where(attrName => Contains(attrName)))
                {
                    attr.Add(attrName,this[attrName]);
                }
                return new  Ec4uCarrierMissingDependency(Id) {Attributes = attr };
            }
            return this;
        }
        #endregion
	}

	#region Context
	public partial class DataContext
	{
		public IQueryable<Ec4uCarrierMissingDependency> Ec4uCarrierMissingDependencySet
		{
			get
			{
				return CreateQuery<Ec4uCarrierMissingDependency>();
			}
		}
	}
	#endregion
}
