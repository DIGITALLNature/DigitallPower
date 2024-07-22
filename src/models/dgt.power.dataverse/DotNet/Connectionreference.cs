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
	[EntityLogicalNameAttribute("connectionreference")]
	[System.CodeDom.Compiler.GeneratedCode("dgtp", "2023")]
    [ExcludeFromCodeCoverage]
	public partial class Connectionreference : Entity, INotifyPropertyChanging, INotifyPropertyChanged
    {
	    #region ctor
		[DebuggerNonUserCode]
		public Connectionreference() : this(false)
        {
        }

        [DebuggerNonUserCode]
		public Connectionreference(bool trackChanges = false) : base(EntityLogicalName)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public Connectionreference(Guid id, bool trackChanges = false) : base(EntityLogicalName,id)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public Connectionreference(KeyAttributeCollection keyAttributes, bool trackChanges = false) : base(EntityLogicalName,keyAttributes)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public Connectionreference(string keyName, object keyValue, bool trackChanges = false) : base(EntityLogicalName, keyName, keyValue)
        {
			_trackChanges = trackChanges;
        }
        #endregion

		#region fields
        private readonly bool _trackChanges;
        private readonly Lazy<HashSet<string>> _changedProperties = new Lazy<HashSet<string>>();
        #endregion

        #region consts
        public const string EntityLogicalName = "connectionreference";
        public const string PrimaryNameAttribute = "connectionreferencedisplayname";
        public const int EntityTypeCode = 10095;
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
		[AttributeLogicalNameAttribute("connectionreferenceid")]
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
				ConnectionreferenceId = value;
			}
		}

		/// <summary>
		/// Unique identifier for entity instances
		/// </summary>
		[AttributeLogicalName("connectionreferenceid")]
        public Guid? ConnectionreferenceId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<Guid?>("connectionreferenceid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(ConnectionreferenceId));
                SetAttributeValue("connectionreferenceid", value);
				if (value.HasValue)
				{
					base.Id = value.Value;
				}
				else
				{
					base.Id = System.Guid.Empty;
				}
                OnPropertyChanged(nameof(ConnectionreferenceId));
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
		/// Id of the connection in API hub
		/// </summary>
		[AttributeLogicalName("connectionid")]
        public string? ConnectionId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("connectionid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(ConnectionId));
                SetAttributeValue("connectionid", value);
                OnPropertyChanged(nameof(ConnectionId));
            }
        }

		/// <summary>
		/// Display name of the connection reference
		/// </summary>
		[AttributeLogicalName("connectionreferencedisplayname")]
        public string? Connectionreferencedisplayname
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("connectionreferencedisplayname");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(Connectionreferencedisplayname));
                SetAttributeValue("connectionreferencedisplayname", value);
                OnPropertyChanged(nameof(Connectionreferencedisplayname));
            }
        }

		/// <summary>
		/// Connection Reference unique name
		/// </summary>
		[AttributeLogicalName("connectionreferencelogicalname")]
        public string? ConnectionReferenceLogicalName
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("connectionreferencelogicalname");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(ConnectionReferenceLogicalName));
                SetAttributeValue("connectionreferencelogicalname", value);
                OnPropertyChanged(nameof(ConnectionReferenceLogicalName));
            }
        }

		/// <summary>
		/// Id of the Public/Shared Connector
		/// </summary>
		[AttributeLogicalName("connectorid")]
        public string? ConnectorId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("connectorid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(ConnectorId));
                SetAttributeValue("connectorid", value);
                OnPropertyChanged(nameof(ConnectorId));
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
		/// Look up to the Connector entity
		/// </summary>
		[AttributeLogicalName("customconnectorid")]
        public EntityReference? CustomConnectorId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<EntityReference?>("customconnectorid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(CustomConnectorId));
                SetAttributeValue("customconnectorid", value);
                OnPropertyChanged(nameof(CustomConnectorId));
            }
        }

		/// <summary>
		/// Description of Connection Reference
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
		/// The Prompting Behavior for this connection reference
		/// </summary>
		[AttributeLogicalName("promptingbehavior")]
        public OptionSetValue? PromptingBehavior
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<OptionSetValue?>("promptingbehavior");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(PromptingBehavior));
                SetAttributeValue("promptingbehavior", value);
                OnPropertyChanged(nameof(PromptingBehavior));
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
		/// Status of the Connection Reference
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
		/// Reason for the status of the Connection Reference
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
		/// 1:N connectionreference_AsyncOperations
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("connectionreference_AsyncOperations")]
		public System.Collections.Generic.IEnumerable<AsyncOperation> ConnectionreferenceAsyncOperations
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<AsyncOperation>("connectionreference_AsyncOperations", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("ConnectionreferenceAsyncOperations");
				this.SetRelatedEntities<AsyncOperation>("connectionreference_AsyncOperations", null, value);
				this.OnPropertyChanged("ConnectionreferenceAsyncOperations");
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
			    public struct PromptingBehavior
                {
					public const int PromptOnImport = 0;
					public const int Skip = 1;
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
				public const string ConnectionreferenceId = "connectionreferenceid";
				public const string ComponentIdUnique = "componentidunique";
				public const string ComponentState = "componentstate";
				public const string ConnectionId = "connectionid";
				public const string Connectionreferencedisplayname = "connectionreferencedisplayname";
				public const string ConnectionReferenceLogicalName = "connectionreferencelogicalname";
				public const string ConnectorId = "connectorid";
				public const string CreatedBy = "createdby";
				public const string CreatedOn = "createdon";
				public const string CreatedOnBehalfBy = "createdonbehalfby";
				public const string CustomConnectorId = "customconnectorid";
				public const string Description = "description";
				public const string ImportSequenceNumber = "importsequencenumber";
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
				public const string PromptingBehavior = "promptingbehavior";
				public const string SolutionId = "solutionid";
				public const string Statecode = "statecode";
				public const string Statuscode = "statuscode";
				public const string TimeZoneRuleVersionNumber = "timezoneruleversionnumber";
				public const string UTCConversionTimeZoneCode = "utcconversiontimezonecode";
				public const string VersionNumber = "versionnumber";
		}
		#endregion

		#region AlternateKeys
		public static class AlternateKeys
		{
				public const string ConnectionReferenceLogicalNameKey = "connectionreferencelogicalnamekey";
		}
		#endregion

		#region Relations
        public static class Relations
        {
            public static class OneToMany
            {
				public const string BotConnectionreference = "bot_connectionreference";
				public const string ConnectionreferenceAsyncOperations = "connectionreference_AsyncOperations";
				public const string ConnectionreferenceBulkDeleteFailures = "connectionreference_BulkDeleteFailures";
				public const string ConnectionreferenceConnectioninstance = "connectionreference_connectioninstance";
				public const string ConnectionreferenceFederatedknowledgeconfiguration = "connectionreference_federatedknowledgeconfiguration";
				public const string ConnectionreferenceMailboxTrackingFolders = "connectionreference_MailboxTrackingFolders";
				public const string ConnectionreferencePrincipalObjectAttributeAccesses = "connectionreference_PrincipalObjectAttributeAccesses";
				public const string ConnectionreferenceProcessSession = "connectionreference_ProcessSession";
				public const string ConnectionreferenceSyncErrors = "connectionreference_SyncErrors";
				public const string ConnectionreferenceUserEntityInstanceDatas = "connectionreference_UserEntityInstanceDatas";
				public const string MsdynAIConfigurationConnectionReference = "msdyn_AIConfiguration_ConnectionReference";
				public const string MsdynConnreferenceMsdynConnectordatasource = "msdyn_connreference_msdyn_connectordatasource";
				public const string MsdynDfcrCrConnect = "msdyn_dfcr_cr_connect";
            }

            public static class ManyToOne
            {
				public const string BusinessUnitConnectionreference = "business_unit_connectionreference";
				public const string ConnectorConnectionreference = "connector_connectionreference";
				public const string LkConnectionreferenceCreatedby = "lk_connectionreference_createdby";
				public const string LkConnectionreferenceCreatedonbehalfby = "lk_connectionreference_createdonbehalfby";
				public const string LkConnectionreferenceModifiedby = "lk_connectionreference_modifiedby";
				public const string LkConnectionreferenceModifiedonbehalfby = "lk_connectionreference_modifiedonbehalfby";
				public const string OwnerConnectionreference = "owner_connectionreference";
				public const string TeamConnectionreference = "team_connectionreference";
				public const string UserConnectionreference = "user_connectionreference";
            }

            public static class ManyToMany
            {
				public const string BotcomponentConnectionreference = "botcomponent_connectionreference";
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
        public static Connectionreference Retrieve(IOrganizationService service, Guid id)
        {
            return Retrieve(service,id, new ColumnSet(true));
        }

        public static Connectionreference Retrieve(IOrganizationService service, Guid id, ColumnSet columnSet)
        {
            return service.Retrieve("connectionreference", id, columnSet).ToEntity<Connectionreference>();
        }

        public Connectionreference GetChangedEntity()
        {
            if (_trackChanges)
            {
                var attr = new AttributeCollection();
                foreach (var attrName in _changedProperties.Value.Select(changedProperty => ((AttributeLogicalNameAttribute) GetType().GetProperty(changedProperty)!.GetCustomAttribute(typeof (AttributeLogicalNameAttribute))!).LogicalName).Where(attrName => Contains(attrName)))
                {
                    attr.Add(attrName,this[attrName]);
                }
                return new  Connectionreference(Id) {Attributes = attr };
            }
            return this;
        }
        #endregion
	}

	#region Context
	public partial class DataContext
	{
		public IQueryable<Connectionreference> ConnectionreferenceSet
		{
			get
			{
				return CreateQuery<Connectionreference>();
			}
		}
	}
	#endregion
}
