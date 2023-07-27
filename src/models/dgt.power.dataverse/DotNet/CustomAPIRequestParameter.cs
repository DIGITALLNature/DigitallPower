// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

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
	/// Entity that defines a request parameter for a custom API
	/// </summary>
	[DataContractAttribute()]
	[EntityLogicalNameAttribute("customapirequestparameter")]
	[System.CodeDom.Compiler.GeneratedCode("dgtp", "2023")]
    [ExcludeFromCodeCoverage]
	public partial class CustomAPIRequestParameter : Entity, INotifyPropertyChanging, INotifyPropertyChanged
    {
	    #region ctor
		[DebuggerNonUserCode]
		public CustomAPIRequestParameter() : this(false)
        {
        }

        [DebuggerNonUserCode]
		public CustomAPIRequestParameter(bool trackChanges = false) : base(EntityLogicalName)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public CustomAPIRequestParameter(Guid id, bool trackChanges = false) : base(EntityLogicalName,id)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public CustomAPIRequestParameter(KeyAttributeCollection keyAttributes, bool trackChanges = false) : base(EntityLogicalName,keyAttributes)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public CustomAPIRequestParameter(string keyName, object keyValue, bool trackChanges = false) : base(EntityLogicalName, keyName, keyValue)
        {
			_trackChanges = trackChanges;
        }
        #endregion

		#region fields
        private readonly bool _trackChanges;
        private readonly Lazy<HashSet<string>> _changedProperties = new Lazy<HashSet<string>>();
        #endregion

        #region consts
        public const string EntityLogicalName = "customapirequestparameter";
        public const string PrimaryNameAttribute = "name";
        public const int EntityTypeCode = 10022;
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
		[AttributeLogicalNameAttribute("customapirequestparameterid")]
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
				CustomAPIRequestParameterId = value;
			}
		}

		/// <summary>
		/// Unique identifier for custom API request parameter instances
		/// </summary>
		[AttributeLogicalName("customapirequestparameterid")]
        public Guid? CustomAPIRequestParameterId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<Guid?>("customapirequestparameterid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(CustomAPIRequestParameterId));
                SetAttributeValue("customapirequestparameterid", value);
				if (value.HasValue)
				{
					base.Id = value.Value;
				}
				else
				{
					base.Id = System.Guid.Empty;
				}
                OnPropertyChanged(nameof(CustomAPIRequestParameterId));
            }
        }

		/// <summary>
		/// For internal use only.
		/// </summary>
		[AttributeLogicalName("componentidunique")]
        public Guid? ComponentIdUnique
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<Guid?>("componentidunique");
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
		/// Unique identifier for the custom API that owns this custom API request parameter
		/// </summary>
		[AttributeLogicalName("customapiid")]
        public EntityReference? CustomAPIId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<EntityReference?>("customapiid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(CustomAPIId));
                SetAttributeValue("customapiid", value);
                OnPropertyChanged(nameof(CustomAPIId));
            }
        }

		/// <summary>
		/// Localized description for custom API request parameter instances
		/// </summary>
		[AttributeLogicalName("description")]
        public string? Description
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("description");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(Description));
                SetAttributeValue("description", value);
                OnPropertyChanged(nameof(Description));
            }
        }

		/// <summary>
		/// Localized display name for custom API request parameter instances
		/// </summary>
		[AttributeLogicalName("displayname")]
        public string? DisplayName
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("displayname");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(DisplayName));
                SetAttributeValue("displayname", value);
                OnPropertyChanged(nameof(DisplayName));
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
		/// For internal use only.
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
		/// Indicates if the custom API request parameter is optional
		/// </summary>
		[AttributeLogicalName("isoptional")]
        public bool? IsOptional
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<bool?>("isoptional");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(IsOptional));
                SetAttributeValue("isoptional", value);
                OnPropertyChanged(nameof(IsOptional));
            }
        }

		/// <summary>
		/// The logical name of the entity bound to the custom API request parameter
		/// </summary>
		[AttributeLogicalName("logicalentityname")]
        public string? LogicalEntityName
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("logicalentityname");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(LogicalEntityName));
                SetAttributeValue("logicalentityname", value);
                OnPropertyChanged(nameof(LogicalEntityName));
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
		/// The primary name of the custom API request parameter
		/// </summary>
		[AttributeLogicalName("name")]
        public string? Name
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("name");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(Name));
                SetAttributeValue("name", value);
                OnPropertyChanged(nameof(Name));
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
		/// Status of the Custom API Request Parameter
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
		/// Reason for the status of the Custom API Request Parameter
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
		/// The data type of the custom API request parameter
		/// </summary>
		[AttributeLogicalName("type")]
        public OptionSetValue? Type
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<OptionSetValue?>("type");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(Type));
                SetAttributeValue("type", value);
                OnPropertyChanged(nameof(Type));
            }
        }

		/// <summary>
		/// Unique name for the custom API request parameter
		/// </summary>
		[AttributeLogicalName("uniquename")]
        public string? UniqueName
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("uniquename");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(UniqueName));
                SetAttributeValue("uniquename", value);
                OnPropertyChanged(nameof(UniqueName));
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
		/// 1:N customapirequestparameter_AsyncOperations
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("customapirequestparameter_AsyncOperations")]
		public System.Collections.Generic.IEnumerable<AsyncOperation> CustomapirequestparameterAsyncOperations
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<AsyncOperation>("customapirequestparameter_AsyncOperations", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("CustomapirequestparameterAsyncOperations");
				this.SetRelatedEntities<AsyncOperation>("customapirequestparameter_AsyncOperations", null, value);
				this.OnPropertyChanged("CustomapirequestparameterAsyncOperations");
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
                public struct IsOptional
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
			    public struct Type
                {
					public const int Boolean = 0;
					public const int DateTime = 1;
					public const int Decimal = 2;
					public const int Entity = 3;
					public const int EntityCollection = 4;
					public const int EntityReference = 5;
					public const int Float = 6;
					public const int Integer = 7;
					public const int Money = 8;
					public const int Picklist = 9;
					public const int String = 10;
					public const int StringArray = 11;
					public const int Guid = 12;
                }
		}
		#endregion

		#region LogicalNames
		public static class LogicalNames
		{
				public const string CustomAPIRequestParameterId = "customapirequestparameterid";
				public const string ComponentIdUnique = "componentidunique";
				public const string ComponentState = "componentstate";
				public const string CreatedBy = "createdby";
				public const string CreatedOn = "createdon";
				public const string CreatedOnBehalfBy = "createdonbehalfby";
				public const string CustomAPIId = "customapiid";
				public const string Description = "description";
				public const string DisplayName = "displayname";
				public const string ImportSequenceNumber = "importsequencenumber";
				public const string IsCustomizable = "iscustomizable";
				public const string IsManaged = "ismanaged";
				public const string IsOptional = "isoptional";
				public const string LogicalEntityName = "logicalentityname";
				public const string ModifiedBy = "modifiedby";
				public const string ModifiedOn = "modifiedon";
				public const string ModifiedOnBehalfBy = "modifiedonbehalfby";
				public const string Name = "name";
				public const string OverriddenCreatedOn = "overriddencreatedon";
				public const string OverwriteTime = "overwritetime";
				public const string OwnerId = "ownerid";
				public const string OwnerIdType = "owneridtype";
				public const string OwningBusinessUnit = "owningbusinessunit";
				public const string OwningTeam = "owningteam";
				public const string OwningUser = "owninguser";
				public const string SolutionId = "solutionid";
				public const string Statecode = "statecode";
				public const string Statuscode = "statuscode";
				public const string TimeZoneRuleVersionNumber = "timezoneruleversionnumber";
				public const string Type = "type";
				public const string UniqueName = "uniquename";
				public const string UTCConversionTimeZoneCode = "utcconversiontimezonecode";
				public const string VersionNumber = "versionnumber";
		}
		#endregion

		#region AlternateKeys
		public static class AlternateKeys
		{
				public const string CustomAPIRequestParameterExportKey = "customapirequestparameterexportkey";
		}
		#endregion

		#region Relations
        public static class Relations
        {
            public static class OneToMany
            {
				public const string CustomapirequestparameterAsyncOperations = "customapirequestparameter_AsyncOperations";
				public const string CustomapirequestparameterBulkDeleteFailures = "customapirequestparameter_BulkDeleteFailures";
				public const string CustomapirequestparameterMailboxTrackingFolders = "customapirequestparameter_MailboxTrackingFolders";
				public const string CustomapirequestparameterPrincipalObjectAttributeAccesses = "customapirequestparameter_PrincipalObjectAttributeAccesses";
				public const string CustomapirequestparameterProcessSession = "customapirequestparameter_ProcessSession";
				public const string CustomapirequestparameterSyncErrors = "customapirequestparameter_SyncErrors";
				public const string CustomapirequestparameterUserEntityInstanceDatas = "customapirequestparameter_UserEntityInstanceDatas";
				public const string MsdynCustomapirequestparameterMsdynCustomapirulesetconfigurationCustomAPIRequestParameter = "msdyn_customapirequestparameter_msdyn_customapirulesetconfiguration_CustomAPIRequestParameter";
				public const string MsdynmktCustomapirequestparameterEventparameter = "msdynmkt_customapirequestparameter_eventparameter";
            }

            public static class ManyToOne
            {
				public const string BusinessUnitCustomapirequestparameter = "business_unit_customapirequestparameter";
				public const string CustomapiCustomapirequestparameter = "customapi_customapirequestparameter";
				public const string LkCustomapirequestparameterCreatedby = "lk_customapirequestparameter_createdby";
				public const string LkCustomapirequestparameterCreatedonbehalfby = "lk_customapirequestparameter_createdonbehalfby";
				public const string LkCustomapirequestparameterModifiedby = "lk_customapirequestparameter_modifiedby";
				public const string LkCustomapirequestparameterModifiedonbehalfby = "lk_customapirequestparameter_modifiedonbehalfby";
				public const string OwnerCustomapirequestparameter = "owner_customapirequestparameter";
				public const string TeamCustomapirequestparameter = "team_customapirequestparameter";
				public const string UserCustomapirequestparameter = "user_customapirequestparameter";
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
        public static CustomAPIRequestParameter Retrieve(IOrganizationService service, Guid id)
        {
            return Retrieve(service,id, new ColumnSet(true));
        }

        public static CustomAPIRequestParameter Retrieve(IOrganizationService service, Guid id, ColumnSet columnSet)
        {
            return service.Retrieve("customapirequestparameter", id, columnSet).ToEntity<CustomAPIRequestParameter>();
        }

        public CustomAPIRequestParameter GetChangedEntity()
        {
            if (_trackChanges)
            {
                var attr = new AttributeCollection();
                foreach (var attrName in _changedProperties.Value.Select(changedProperty => ((AttributeLogicalNameAttribute) GetType().GetProperty(changedProperty)!.GetCustomAttribute(typeof (AttributeLogicalNameAttribute))!).LogicalName).Where(attrName => Contains(attrName)))
                {
                    attr.Add(attrName,this[attrName]);
                }
                return new  CustomAPIRequestParameter(Id) {Attributes = attr };
            }
            return this;
        }
        #endregion
	}

	#region Context
	public partial class DataContext
	{
		public IQueryable<CustomAPIRequestParameter> CustomAPIRequestParameterSet
		{
			get
			{
				return CreateQuery<CustomAPIRequestParameter>();
			}
		}
	}
	#endregion
}
