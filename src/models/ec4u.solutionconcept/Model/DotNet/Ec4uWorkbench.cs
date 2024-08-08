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
	[EntityLogicalNameAttribute("ec4u_workbench")]
	[System.CodeDom.Compiler.GeneratedCode("dgtp", "2023")]
    [ExcludeFromCodeCoverage]
	public partial class Ec4uWorkbench : Entity, INotifyPropertyChanging, INotifyPropertyChanged
    {
	    #region ctor
		[DebuggerNonUserCode]
		public Ec4uWorkbench() : this(false)
        {
        }

        [DebuggerNonUserCode]
		public Ec4uWorkbench(bool trackChanges = false) : base(EntityLogicalName)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public Ec4uWorkbench(Guid id, bool trackChanges = false) : base(EntityLogicalName,id)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public Ec4uWorkbench(KeyAttributeCollection keyAttributes, bool trackChanges = false) : base(EntityLogicalName,keyAttributes)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public Ec4uWorkbench(string keyName, object keyValue, bool trackChanges = false) : base(EntityLogicalName, keyName, keyValue)
        {
			_trackChanges = trackChanges;
        }
        #endregion

		#region fields
        private readonly bool _trackChanges;
        private readonly Lazy<HashSet<string>> _changedProperties = new Lazy<HashSet<string>>();
        #endregion

        #region consts
        public const string EntityLogicalName = "ec4u_workbench";
        public const string PrimaryNameAttribute = "ec4u_wb_name";
        public const int EntityTypeCode = 10528;
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
		[AttributeLogicalNameAttribute("ec4u_workbenchid")]
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
				Ec4uWorkbenchId = value;
			}
		}

		/// <summary>
		/// Unique identifier for entity instances
		/// </summary>
		[AttributeLogicalName("ec4u_workbenchid")]
        public Guid? Ec4uWorkbenchId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<Guid?>("ec4u_workbenchid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(Ec4uWorkbenchId));
                SetAttributeValue("ec4u_workbenchid", value);
				if (value.HasValue)
				{
					base.Id = value.Value;
				}
				else
				{
					base.Id = System.Guid.Empty;
				}
                OnPropertyChanged(nameof(Ec4uWorkbenchId));
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

		
		[AttributeLogicalName("ec4u_assembly_opt")]
        public bool? Ec4uAssemblyOpt
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<bool?>("ec4u_assembly_opt");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(Ec4uAssemblyOpt));
                SetAttributeValue("ec4u_assembly_opt", value);
                OnPropertyChanged(nameof(Ec4uAssemblyOpt));
            }
        }

		
		[AttributeLogicalName("ec4u_wb_carrier_id")]
        public EntityReference? Ec4uWbCarrierId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<EntityReference?>("ec4u_wb_carrier_id");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(Ec4uWbCarrierId));
                SetAttributeValue("ec4u_wb_carrier_id", value);
                OnPropertyChanged(nameof(Ec4uWbCarrierId));
            }
        }

		/// <summary>
		/// The Workbench Name.
		/// </summary>
		[AttributeLogicalName("ec4u_wb_name")]
        public string? Ec4uWbName
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("ec4u_wb_name");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(Ec4uWbName));
                SetAttributeValue("ec4u_wb_name", value);
                OnPropertyChanged(nameof(Ec4uWbName));
            }
        }

		
		[AttributeLogicalName("ec4u_wb_nature_set")]
        public OptionSetValue? Ec4uWbNatureSet
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<OptionSetValue?>("ec4u_wb_nature_set");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(Ec4uWbNatureSet));
                SetAttributeValue("ec4u_wb_nature_set", value);
                OnPropertyChanged(nameof(Ec4uWbNatureSet));
            }
        }

		
		[AttributeLogicalName("ec4u_wb_solutionfriendlyname")]
        public string? Ec4uWbSolutionfriendlyname
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("ec4u_wb_solutionfriendlyname");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(Ec4uWbSolutionfriendlyname));
                SetAttributeValue("ec4u_wb_solutionfriendlyname", value);
                OnPropertyChanged(nameof(Ec4uWbSolutionfriendlyname));
            }
        }

		
		[AttributeLogicalName("ec4u_wb_solutionid")]
        public string? Ec4uWbSolutionid
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("ec4u_wb_solutionid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(Ec4uWbSolutionid));
                SetAttributeValue("ec4u_wb_solutionid", value);
                OnPropertyChanged(nameof(Ec4uWbSolutionid));
            }
        }

		
		[AttributeLogicalName("ec4u_wb_solutionuniquename")]
        public string? Ec4uWbSolutionuniquename
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("ec4u_wb_solutionuniquename");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(Ec4uWbSolutionuniquename));
                SetAttributeValue("ec4u_wb_solutionuniquename", value);
                OnPropertyChanged(nameof(Ec4uWbSolutionuniquename));
            }
        }

		
		[AttributeLogicalName("ec4u_wb_solutionversion")]
        public string? Ec4uWbSolutionversion
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("ec4u_wb_solutionversion");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(Ec4uWbSolutionversion));
                SetAttributeValue("ec4u_wb_solutionversion", value);
                OnPropertyChanged(nameof(Ec4uWbSolutionversion));
            }
        }

		
		[AttributeLogicalName("ec4u_wb_target_carrier_id")]
        public EntityReference? Ec4uWbTargetCarrierId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<EntityReference?>("ec4u_wb_target_carrier_id");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(Ec4uWbTargetCarrierId));
                SetAttributeValue("ec4u_wb_target_carrier_id", value);
                OnPropertyChanged(nameof(Ec4uWbTargetCarrierId));
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
		/// 1:N ec4u_workbench_to_carrier_on_workbench
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("ec4u_workbench_to_carrier_on_workbench")]
		public System.Collections.Generic.IEnumerable<Ec4uCarrier> Ec4uWorkbenchToCarrierOnWorkbench
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Ec4uCarrier>("ec4u_workbench_to_carrier_on_workbench", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("Ec4uWorkbenchToCarrierOnWorkbench");
				this.SetRelatedEntities<Ec4uCarrier>("ec4u_workbench_to_carrier_on_workbench", null, value);
				this.OnPropertyChanged("Ec4uWorkbenchToCarrierOnWorkbench");
			}
		}

		/// <summary>
		/// 1:N ec4u_workbench_to_workbench_history_on_workbench
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("ec4u_workbench_to_workbench_history_on_workbench")]
		public System.Collections.Generic.IEnumerable<Ec4uWorkbenchHistory> Ec4uWorkbenchToWorkbenchHistoryOnWorkbench
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Ec4uWorkbenchHistory>("ec4u_workbench_to_workbench_history_on_workbench", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("Ec4uWorkbenchToWorkbenchHistoryOnWorkbench");
				this.SetRelatedEntities<Ec4uWorkbenchHistory>("ec4u_workbench_to_workbench_history_on_workbench", null, value);
				this.OnPropertyChanged("Ec4uWorkbenchToWorkbenchHistoryOnWorkbench");
			}
		}

		#endregion

		#region Options
		public static class Options
		{
                public struct Ec4uAssemblyOpt
                {
                    public const bool No = false;
                    public const bool Yes = true;
                }
			    public struct Ec4uWbNatureSet
                {
					public const int Workbench = 596030000;
					public const int Assembly = 596030001;
					public const int EnvironmentVariable = 596030002;
					public const int ConnectionReference = 596030003;
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
					public const int Merge = 596030000;
					public const int Failure = 596030001;
					public const int Finalize = 596030002;
					public const int Close = 596030003;
                }
		}
		#endregion

		#region LogicalNames
		public static class LogicalNames
		{
				public const string Ec4uWorkbenchId = "ec4u_workbenchid";
				public const string CreatedBy = "createdby";
				public const string CreatedOn = "createdon";
				public const string CreatedOnBehalfBy = "createdonbehalfby";
				public const string Ec4uAssemblyOpt = "ec4u_assembly_opt";
				public const string Ec4uWbCarrierId = "ec4u_wb_carrier_id";
				public const string Ec4uWbName = "ec4u_wb_name";
				public const string Ec4uWbNatureSet = "ec4u_wb_nature_set";
				public const string Ec4uWbSolutionfriendlyname = "ec4u_wb_solutionfriendlyname";
				public const string Ec4uWbSolutionid = "ec4u_wb_solutionid";
				public const string Ec4uWbSolutionuniquename = "ec4u_wb_solutionuniquename";
				public const string Ec4uWbSolutionversion = "ec4u_wb_solutionversion";
				public const string Ec4uWbTargetCarrierId = "ec4u_wb_target_carrier_id";
				public const string ImportSequenceNumber = "importsequencenumber";
				public const string ModifiedBy = "modifiedby";
				public const string ModifiedOn = "modifiedon";
				public const string ModifiedOnBehalfBy = "modifiedonbehalfby";
				public const string OverriddenCreatedOn = "overriddencreatedon";
				public const string OwnerId = "ownerid";
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

		#region AlternateKeys
		public static class AlternateKeys
		{
				public const string SolutionUniqueName = "ec4u_wb_solution_unique_name";
		}
		#endregion

		#region Relations
        public static class Relations
        {
            public static class OneToMany
            {
				public const string Ec4uWorkbenchAsyncOperations = "ec4u_workbench_AsyncOperations";
				public const string Ec4uWorkbenchBulkDeleteFailures = "ec4u_workbench_BulkDeleteFailures";
				public const string Ec4uWorkbenchDuplicateBaseRecord = "ec4u_workbench_DuplicateBaseRecord";
				public const string Ec4uWorkbenchDuplicateMatchingRecord = "ec4u_workbench_DuplicateMatchingRecord";
				public const string Ec4uWorkbenchMailboxTrackingFolders = "ec4u_workbench_MailboxTrackingFolders";
				public const string Ec4uWorkbenchPrincipalObjectAttributeAccesses = "ec4u_workbench_PrincipalObjectAttributeAccesses";
				public const string Ec4uWorkbenchProcessSession = "ec4u_workbench_ProcessSession";
				public const string Ec4uWorkbenchSyncErrors = "ec4u_workbench_SyncErrors";
				public const string Ec4uWorkbenchToCarrierOnWorkbench = "ec4u_workbench_to_carrier_on_workbench";
				public const string Ec4uWorkbenchToWorkbenchHistoryOnWorkbench = "ec4u_workbench_to_workbench_history_on_workbench";
				public const string Ec4uWorkbenchUserEntityInstanceDatas = "ec4u_workbench_UserEntityInstanceDatas";
            }

            public static class ManyToOne
            {
				public const string BusinessUnitEc4uWorkbench = "business_unit_ec4u_workbench";
				public const string Ec4uCarrierToWorkbenchOnCarrier = "ec4u_carrier_to_workbench_on_carrier";
				public const string Ec4uCarrierToWorkbenchOnTargetCarrier = "ec4u_carrier_to_workbench_on_target_carrier";
				public const string LkEc4uWorkbenchCreatedby = "lk_ec4u_workbench_createdby";
				public const string LkEc4uWorkbenchCreatedonbehalfby = "lk_ec4u_workbench_createdonbehalfby";
				public const string LkEc4uWorkbenchModifiedby = "lk_ec4u_workbench_modifiedby";
				public const string LkEc4uWorkbenchModifiedonbehalfby = "lk_ec4u_workbench_modifiedonbehalfby";
				public const string OwnerEc4uWorkbench = "owner_ec4u_workbench";
				public const string ProcessstageEc4uWorkbench = "processstage_ec4u_workbench";
				public const string TeamEc4uWorkbench = "team_ec4u_workbench";
				public const string UserEc4uWorkbench = "user_ec4u_workbench";
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
        public static Ec4uWorkbench Retrieve(IOrganizationService service, Guid id)
        {
            return Retrieve(service,id, new ColumnSet(true));
        }

        public static Ec4uWorkbench Retrieve(IOrganizationService service, Guid id, ColumnSet columnSet)
        {
            return service.Retrieve("ec4u_workbench", id, columnSet).ToEntity<Ec4uWorkbench>();
        }

        public Ec4uWorkbench GetChangedEntity()
        {
            if (_trackChanges)
            {
                var attr = new AttributeCollection();
                foreach (var attrName in _changedProperties.Value.Select(changedProperty => ((AttributeLogicalNameAttribute) GetType().GetProperty(changedProperty)!.GetCustomAttribute(typeof (AttributeLogicalNameAttribute))!).LogicalName).Where(attrName => Contains(attrName)))
                {
                    attr.Add(attrName,this[attrName]);
                }
                return new  Ec4uWorkbench(Id) {Attributes = attr };
            }
            return this;
        }
        #endregion
	}

	#region Context
	public partial class DataContext
	{
		public IQueryable<Ec4uWorkbench> Ec4uWorkbenchSet
		{
			get
			{
				return CreateQuery<Ec4uWorkbench>();
			}
		}
	}
	#endregion
}
