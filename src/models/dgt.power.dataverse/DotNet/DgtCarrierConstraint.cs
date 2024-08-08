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
	[EntityLogicalNameAttribute("dgt_carrier_constraint")]
	[System.CodeDom.Compiler.GeneratedCode("dgtp", "2023")]
    [ExcludeFromCodeCoverage]
	public partial class DgtCarrierConstraint : Entity, INotifyPropertyChanging, INotifyPropertyChanged
    {
	    #region ctor
		[DebuggerNonUserCode]
		public DgtCarrierConstraint() : this(false)
        {
        }

        [DebuggerNonUserCode]
		public DgtCarrierConstraint(bool trackChanges = false) : base(EntityLogicalName)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public DgtCarrierConstraint(Guid id, bool trackChanges = false) : base(EntityLogicalName,id)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public DgtCarrierConstraint(KeyAttributeCollection keyAttributes, bool trackChanges = false) : base(EntityLogicalName,keyAttributes)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public DgtCarrierConstraint(string keyName, object keyValue, bool trackChanges = false) : base(EntityLogicalName, keyName, keyValue)
        {
			_trackChanges = trackChanges;
        }
        #endregion

		#region fields
        private readonly bool _trackChanges;
        private readonly Lazy<HashSet<string>> _changedProperties = new Lazy<HashSet<string>>();
        #endregion

        #region consts
        public const string EntityLogicalName = "dgt_carrier_constraint";
        public const string PrimaryNameAttribute = "dgt_name";
        public const int EntityTypeCode = 10408;
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
		[AttributeLogicalNameAttribute("dgt_carrier_constraintid")]
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
				DgtCarrierConstraintId = value;
			}
		}

		/// <summary>
		/// Unique identifier for entity instances
		/// </summary>
		[AttributeLogicalName("dgt_carrier_constraintid")]
        public Guid? DgtCarrierConstraintId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<Guid?>("dgt_carrier_constraintid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(DgtCarrierConstraintId));
                SetAttributeValue("dgt_carrier_constraintid", value);
				if (value.HasValue)
				{
					base.Id = value.Value;
				}
				else
				{
					base.Id = System.Guid.Empty;
				}
                OnPropertyChanged(nameof(DgtCarrierConstraintId));
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

		/// <summary>
		/// Custom Api that will be executed for this constraint check
		/// </summary>
		[AttributeLogicalName("dgt_customapi_id")]
        public EntityReference? DgtCustomapiId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<EntityReference?>("dgt_customapi_id");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(DgtCustomapiId));
                SetAttributeValue("dgt_customapi_id", value);
                OnPropertyChanged(nameof(DgtCustomapiId));
            }
        }

		/// <summary>
		/// Whether this constraint is enabled by default or needs to be configured manually
		/// </summary>
		[AttributeLogicalName("dgt_default_bit")]
        public bool? DgtDefaultBit
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<bool?>("dgt_default_bit");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(DgtDefaultBit));
                SetAttributeValue("dgt_default_bit", value);
                OnPropertyChanged(nameof(DgtDefaultBit));
            }
        }

		
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

		/// <summary>
		/// Contains a short description what this check does
		/// </summary>
		[AttributeLogicalName("dgt_short_description_txt")]
        public string? DgtShortDescriptionTxt
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("dgt_short_description_txt");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(DgtShortDescriptionTxt));
                SetAttributeValue("dgt_short_description_txt", value);
                OnPropertyChanged(nameof(DgtShortDescriptionTxt));
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
		/// Status of the Carrier Constraint
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
		/// Reason for the status of the Carrier Constraint
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
		/// 1:N dgt_carrier_constraint_AsyncOperations
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("dgt_carrier_constraint_AsyncOperations")]
		public System.Collections.Generic.IEnumerable<AsyncOperation> DgtCarrierConstraintAsyncOperations
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<AsyncOperation>("dgt_carrier_constraint_AsyncOperations", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("DgtCarrierConstraintAsyncOperations");
				this.SetRelatedEntities<AsyncOperation>("dgt_carrier_constraint_AsyncOperations", null, value);
				this.OnPropertyChanged("DgtCarrierConstraintAsyncOperations");
			}
		}

		#endregion

		#region Options
		public static class Options
		{
                public struct DgtDefaultBit
                {
                    public const bool No = false;
                    public const bool Yes = true;
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
				public const string DgtCarrierConstraintId = "dgt_carrier_constraintid";
				public const string CreatedBy = "createdby";
				public const string CreatedOn = "createdon";
				public const string CreatedOnBehalfBy = "createdonbehalfby";
				public const string DgtCustomapiId = "dgt_customapi_id";
				public const string DgtDefaultBit = "dgt_default_bit";
				public const string DgtName = "dgt_name";
				public const string DgtShortDescriptionTxt = "dgt_short_description_txt";
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
				public const string DgtCarrierConstraintAsyncOperations = "dgt_carrier_constraint_AsyncOperations";
				public const string DgtCarrierConstraintBulkDeleteFailures = "dgt_carrier_constraint_BulkDeleteFailures";
				public const string DgtCarrierConstraintMailboxTrackingFolders = "dgt_carrier_constraint_MailboxTrackingFolders";
				public const string DgtCarrierConstraintPrincipalObjectAttributeAccesses = "dgt_carrier_constraint_PrincipalObjectAttributeAccesses";
				public const string DgtCarrierConstraintProcessSession = "dgt_carrier_constraint_ProcessSession";
				public const string DgtCarrierConstraintSyncErrors = "dgt_carrier_constraint_SyncErrors";
				public const string DgtCarrierConstraintUserEntityInstanceDatas = "dgt_carrier_constraint_UserEntityInstanceDatas";
            }

            public static class ManyToOne
            {
				public const string DgtCarrierConstraintCustomapiIdCustomapi = "dgt_carrier_constraint_customapi_id_customapi";
				public const string LkDgtCarrierConstraintCreatedby = "lk_dgt_carrier_constraint_createdby";
				public const string LkDgtCarrierConstraintCreatedonbehalfby = "lk_dgt_carrier_constraint_createdonbehalfby";
				public const string LkDgtCarrierConstraintModifiedby = "lk_dgt_carrier_constraint_modifiedby";
				public const string LkDgtCarrierConstraintModifiedonbehalfby = "lk_dgt_carrier_constraint_modifiedonbehalfby";
				public const string OrganizationDgtCarrierConstraint = "organization_dgt_carrier_constraint";
            }

            public static class ManyToMany
            {
				public const string DgtCarrierConstraintDgtCarrierDgtCarrier = "dgt_carrier_constraint_dgt_carrier_dgt_carrier";
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
        public static DgtCarrierConstraint Retrieve(IOrganizationService service, Guid id)
        {
            return Retrieve(service,id, new ColumnSet(true));
        }

        public static DgtCarrierConstraint Retrieve(IOrganizationService service, Guid id, ColumnSet columnSet)
        {
            return service.Retrieve("dgt_carrier_constraint", id, columnSet).ToEntity<DgtCarrierConstraint>();
        }

        public DgtCarrierConstraint GetChangedEntity()
        {
            if (_trackChanges)
            {
                var attr = new AttributeCollection();
                foreach (var attrName in _changedProperties.Value.Select(changedProperty => ((AttributeLogicalNameAttribute) GetType().GetProperty(changedProperty)!.GetCustomAttribute(typeof (AttributeLogicalNameAttribute))!).LogicalName).Where(attrName => Contains(attrName)))
                {
                    attr.Add(attrName,this[attrName]);
                }
                return new  DgtCarrierConstraint(Id) {Attributes = attr };
            }
            return this;
        }
        #endregion
	}

	#region Context
	public partial class DataContext
	{
		public IQueryable<DgtCarrierConstraint> DgtCarrierConstraintSet
		{
			get
			{
				return CreateQuery<DgtCarrierConstraint>();
			}
		}
	}
	#endregion
}
