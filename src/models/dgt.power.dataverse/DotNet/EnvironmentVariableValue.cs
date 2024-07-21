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
	/// <summary>
	/// Holds the value for the associated EnvironmentVariableDefinition entity.
	/// </summary>
	[DataContractAttribute()]
	[EntityLogicalNameAttribute("environmentvariablevalue")]
	[System.CodeDom.Compiler.GeneratedCode("dgtp", "2023")]
    [ExcludeFromCodeCoverage]
	public partial class EnvironmentVariableValue : Entity, INotifyPropertyChanging, INotifyPropertyChanged
    {
	    #region ctor
		[DebuggerNonUserCode]
		public EnvironmentVariableValue() : this(false)
        {
        }

        [DebuggerNonUserCode]
		public EnvironmentVariableValue(bool trackChanges = false) : base(EntityLogicalName)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public EnvironmentVariableValue(Guid id, bool trackChanges = false) : base(EntityLogicalName,id)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public EnvironmentVariableValue(KeyAttributeCollection keyAttributes, bool trackChanges = false) : base(EntityLogicalName,keyAttributes)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public EnvironmentVariableValue(string keyName, object keyValue, bool trackChanges = false) : base(EntityLogicalName, keyName, keyValue)
        {
			_trackChanges = trackChanges;
        }
        #endregion

		#region fields
        private readonly bool _trackChanges;
        private readonly Lazy<HashSet<string>> _changedProperties = new Lazy<HashSet<string>>();
        #endregion

        #region consts
        public const string EntityLogicalName = "environmentvariablevalue";
        public const string PrimaryNameAttribute = "schemaname";
        public const int EntityTypeCode = 381;
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
		[AttributeLogicalNameAttribute("environmentvariablevalueid")]
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
				EnvironmentVariableValueId = value;
			}
		}

		/// <summary>
		/// Unique identifier for entity instances
		/// </summary>
		[AttributeLogicalName("environmentvariablevalueid")]
        public Guid? EnvironmentVariableValueId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<Guid?>("environmentvariablevalueid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(EnvironmentVariableValueId));
                SetAttributeValue("environmentvariablevalueid", value);
				if (value.HasValue)
				{
					base.Id = value.Value;
				}
				else
				{
					base.Id = System.Guid.Empty;
				}
                OnPropertyChanged(nameof(EnvironmentVariableValueId));
            }
        }

		/// <summary>
		/// For internal use only.
		/// </summary>
		[AttributeLogicalName("componentstate")]
        public OptionSetValue? ComponentState
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<OptionSetValue?>("componentstate");
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

		/// <summary>
		/// Unique identifier for Environment Variable Definition associated with Environment Variable Value.
		/// </summary>
		[AttributeLogicalName("environmentvariabledefinitionid")]
        public EntityReference? EnvironmentVariableDefinitionId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<EntityReference?>("environmentvariabledefinitionid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(EnvironmentVariableDefinitionId));
                SetAttributeValue("environmentvariabledefinitionid", value);
                OnPropertyChanged(nameof(EnvironmentVariableDefinitionId));
            }
        }

		/// <summary>
		/// For internal use only.
		/// </summary>
		[AttributeLogicalName("environmentvariablevalueidunique")]
        public Guid? EnvironmentVariableValueIdUnique
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<Guid?>("environmentvariablevalueidunique");
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
		/// Version in which the form is introduced.
		/// </summary>
		[AttributeLogicalName("introducedversion")]
        public string? IntroducedVersion
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("introducedversion");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(IntroducedVersion));
                SetAttributeValue("introducedversion", value);
                OnPropertyChanged(nameof(IntroducedVersion));
            }
        }

		/// <summary>
		/// Tells whether the component can be customized.
		/// </summary>
		[AttributeLogicalName("iscustomizable")]
        public BooleanManagedProperty? IsCustomizable
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<BooleanManagedProperty?>("iscustomizable");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(IsCustomizable));
                SetAttributeValue("iscustomizable", value);
                OnPropertyChanged(nameof(IsCustomizable));
            }
        }

		/// <summary>
		/// Indicates whether the solution component is part of a managed solution.
		/// </summary>
		[AttributeLogicalName("ismanaged")]
        public bool? IsManaged
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<bool?>("ismanaged");
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
		/// For internal use only.
		/// </summary>
		[AttributeLogicalName("overwritetime")]
        public DateTime? OverwriteTime
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<DateTime?>("overwritetime");
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
		/// Unique entity name.
		/// </summary>
		[AttributeLogicalName("schemaname")]
        public string? SchemaName
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("schemaname");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(SchemaName));
                SetAttributeValue("schemaname", value);
                OnPropertyChanged(nameof(SchemaName));
            }
        }

		/// <summary>
		/// Unique identifier of the associated solution.
		/// </summary>
		[AttributeLogicalName("solutionid")]
        public Guid? SolutionId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<Guid?>("solutionid");
            }
        }

		/// <summary>
		/// Status of the Environment Variable Value
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
		/// Reason for the status of the Environment Variable Value
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
		/// Contains the actual variable data.
		/// </summary>
		[AttributeLogicalName("value")]
        public string? Value
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("value");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(Value));
                SetAttributeValue("value", value);
                OnPropertyChanged(nameof(Value));
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
		/// 1:N environmentvariablevalue_AsyncOperations
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("environmentvariablevalue_AsyncOperations")]
		public System.Collections.Generic.IEnumerable<AsyncOperation> EnvironmentvariablevalueAsyncOperations
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<AsyncOperation>("environmentvariablevalue_AsyncOperations", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("EnvironmentvariablevalueAsyncOperations");
				this.SetRelatedEntities<AsyncOperation>("environmentvariablevalue_AsyncOperations", null, value);
				this.OnPropertyChanged("EnvironmentvariablevalueAsyncOperations");
			}
		}

		#endregion

		#region Options
		public static class Options
		{
			    public struct ComponentState
                {
					public const int Published = 0;
					public const int Unpublished = 1;
					public const int Deleted = 2;
					public const int DeletedUnpublished = 3;
                }
                public struct IsManaged
                {
                    public const bool Unmanaged = false;
                    public const bool Managed = true;
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
				public const string EnvironmentVariableValueId = "environmentvariablevalueid";
				public const string ComponentState = "componentstate";
				public const string CreatedBy = "createdby";
				public const string CreatedOn = "createdon";
				public const string CreatedOnBehalfBy = "createdonbehalfby";
				public const string EnvironmentVariableDefinitionId = "environmentvariabledefinitionid";
				public const string EnvironmentVariableValueIdUnique = "environmentvariablevalueidunique";
				public const string ImportSequenceNumber = "importsequencenumber";
				public const string IntroducedVersion = "introducedversion";
				public const string IsCustomizable = "iscustomizable";
				public const string IsManaged = "ismanaged";
				public const string ModifiedBy = "modifiedby";
				public const string ModifiedOn = "modifiedon";
				public const string ModifiedOnBehalfBy = "modifiedonbehalfby";
				public const string OverriddenCreatedOn = "overriddencreatedon";
				public const string OverwriteTime = "overwritetime";
				public const string OwnerId = "ownerid";
				public const string OwningBusinessUnit = "owningbusinessunit";
				public const string OwningTeam = "owningteam";
				public const string OwningUser = "owninguser";
				public const string SchemaName = "schemaname";
				public const string SolutionId = "solutionid";
				public const string Statecode = "statecode";
				public const string Statuscode = "statuscode";
				public const string TimeZoneRuleVersionNumber = "timezoneruleversionnumber";
				public const string UTCConversionTimeZoneCode = "utcconversiontimezonecode";
				public const string Value = "value";
				public const string VersionNumber = "versionnumber";
		}
		#endregion

		#region Relations
        public static class Relations
        {
            public static class OneToMany
            {
				public const string EnvironmentvariablevalueAsyncOperations = "environmentvariablevalue_AsyncOperations";
				public const string EnvironmentvariablevalueBulkDeleteFailures = "environmentvariablevalue_BulkDeleteFailures";
				public const string EnvironmentvariablevalueDuplicateBaseRecord = "environmentvariablevalue_DuplicateBaseRecord";
				public const string EnvironmentvariablevalueDuplicateMatchingRecord = "environmentvariablevalue_DuplicateMatchingRecord";
				public const string EnvironmentvariablevalueMailboxTrackingFolders = "environmentvariablevalue_MailboxTrackingFolders";
				public const string EnvironmentvariablevaluePrincipalObjectAttributeAccesses = "environmentvariablevalue_PrincipalObjectAttributeAccesses";
				public const string EnvironmentvariablevalueProcessSession = "environmentvariablevalue_ProcessSession";
				public const string EnvironmentvariablevalueSyncErrors = "environmentvariablevalue_SyncErrors";
				public const string EnvironmentvariablevalueUserEntityInstanceDatas = "environmentvariablevalue_UserEntityInstanceDatas";
            }

            public static class ManyToOne
            {
				public const string BusinessUnitEnvironmentvariablevalue = "business_unit_environmentvariablevalue";
				public const string EnvironmentvariabledefinitionEnvironmentvariablevalue = "environmentvariabledefinition_environmentvariablevalue";
				public const string LkEnvironmentvariablevalueCreatedby = "lk_environmentvariablevalue_createdby";
				public const string LkEnvironmentvariablevalueCreatedonbehalfby = "lk_environmentvariablevalue_createdonbehalfby";
				public const string LkEnvironmentvariablevalueModifiedby = "lk_environmentvariablevalue_modifiedby";
				public const string LkEnvironmentvariablevalueModifiedonbehalfby = "lk_environmentvariablevalue_modifiedonbehalfby";
				public const string OwnerEnvironmentvariablevalue = "owner_environmentvariablevalue";
				public const string TeamEnvironmentvariablevalue = "team_environmentvariablevalue";
				public const string UserEnvironmentvariablevalue = "user_environmentvariablevalue";
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
        public static EnvironmentVariableValue Retrieve(IOrganizationService service, Guid id)
        {
            return Retrieve(service,id, new ColumnSet(true));
        }

        public static EnvironmentVariableValue Retrieve(IOrganizationService service, Guid id, ColumnSet columnSet)
        {
            return service.Retrieve("environmentvariablevalue", id, columnSet).ToEntity<EnvironmentVariableValue>();
        }

        public EnvironmentVariableValue GetChangedEntity()
        {
            if (_trackChanges)
            {
                var attr = new AttributeCollection();
                foreach (var attrName in _changedProperties.Value.Select(changedProperty => ((AttributeLogicalNameAttribute) GetType().GetProperty(changedProperty)!.GetCustomAttribute(typeof (AttributeLogicalNameAttribute))!).LogicalName).Where(attrName => Contains(attrName)))
                {
                    attr.Add(attrName,this[attrName]);
                }
                return new  EnvironmentVariableValue(Id) {Attributes = attr };
            }
            return this;
        }
        #endregion
	}

	#region Context
	public partial class DataContext
	{
		public IQueryable<EnvironmentVariableValue> EnvironmentVariableValueSet
		{
			get
			{
				return CreateQuery<EnvironmentVariableValue>();
			}
		}
	}
	#endregion
}
