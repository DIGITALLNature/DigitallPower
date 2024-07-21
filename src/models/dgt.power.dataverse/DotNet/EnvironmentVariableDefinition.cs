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
	/// Contains information about the settable variable: its type, default value, and etc.
	/// </summary>
	[DataContractAttribute()]
	[EntityLogicalNameAttribute("environmentvariabledefinition")]
	[System.CodeDom.Compiler.GeneratedCode("dgtp", "2023")]
    [ExcludeFromCodeCoverage]
	public partial class EnvironmentVariableDefinition : Entity, INotifyPropertyChanging, INotifyPropertyChanged
    {
	    #region ctor
		[DebuggerNonUserCode]
		public EnvironmentVariableDefinition() : this(false)
        {
        }

        [DebuggerNonUserCode]
		public EnvironmentVariableDefinition(bool trackChanges = false) : base(EntityLogicalName)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public EnvironmentVariableDefinition(Guid id, bool trackChanges = false) : base(EntityLogicalName,id)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public EnvironmentVariableDefinition(KeyAttributeCollection keyAttributes, bool trackChanges = false) : base(EntityLogicalName,keyAttributes)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public EnvironmentVariableDefinition(string keyName, object keyValue, bool trackChanges = false) : base(EntityLogicalName, keyName, keyValue)
        {
			_trackChanges = trackChanges;
        }
        #endregion

		#region fields
        private readonly bool _trackChanges;
        private readonly Lazy<HashSet<string>> _changedProperties = new Lazy<HashSet<string>>();
        #endregion

        #region consts
        public const string EntityLogicalName = "environmentvariabledefinition";
        public const string PrimaryNameAttribute = "schemaname";
        public const int EntityTypeCode = 380;
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
		[AttributeLogicalNameAttribute("environmentvariabledefinitionid")]
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
				EnvironmentVariableDefinitionId = value;
			}
		}

		/// <summary>
		/// Unique identifier for entity instances
		/// </summary>
		[AttributeLogicalName("environmentvariabledefinitionid")]
        public Guid? EnvironmentVariableDefinitionId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<Guid?>("environmentvariabledefinitionid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(EnvironmentVariableDefinitionId));
                SetAttributeValue("environmentvariabledefinitionid", value);
				if (value.HasValue)
				{
					base.Id = value.Value;
				}
				else
				{
					base.Id = System.Guid.Empty;
				}
                OnPropertyChanged(nameof(EnvironmentVariableDefinitionId));
            }
        }

		
		[AttributeLogicalName("apiid")]
        public string? ApiId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("apiid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(ApiId));
                SetAttributeValue("apiid", value);
                OnPropertyChanged(nameof(ApiId));
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
		/// Unique identifier for Connection Reference associated with Environment Variable Definition.
		/// </summary>
		[AttributeLogicalName("connectionreferenceid")]
        public EntityReference? ConnectionReferenceId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<EntityReference?>("connectionreferenceid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(ConnectionReferenceId));
                SetAttributeValue("connectionreferenceid", value);
                OnPropertyChanged(nameof(ConnectionReferenceId));
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
		/// Default variable value to be used if no associated EnvironmentVariableValue entities exist.
		/// </summary>
		[AttributeLogicalName("defaultvalue")]
        public string? DefaultValue
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("defaultvalue");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(DefaultValue));
                SetAttributeValue("defaultvalue", value);
                OnPropertyChanged(nameof(DefaultValue));
            }
        }

		/// <summary>
		/// Description of the variable definition.
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
		/// Display Name of the variable definition.
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
		/// For internal use only.
		/// </summary>
		[AttributeLogicalName("environmentvariabledefinitionidunique")]
        public Guid? EnvironmentVariableDefinitionIdUnique
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<Guid?>("environmentvariabledefinitionidunique");
            }
        }

		/// <summary>
		/// For internal use only.
		/// </summary>
		[AttributeLogicalName("hint")]
        public string? Hint
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("hint");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(Hint));
                SetAttributeValue("hint", value);
                OnPropertyChanged(nameof(Hint));
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
		/// For internal use only.
		/// </summary>
		[AttributeLogicalName("isrequired")]
        public bool? IsRequired
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<bool?>("isrequired");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(IsRequired));
                SetAttributeValue("isrequired", value);
                OnPropertyChanged(nameof(IsRequired));
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

		
		[AttributeLogicalName("parameterkey")]
        public string? ParameterKey
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("parameterkey");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(ParameterKey));
                SetAttributeValue("parameterkey", value);
                OnPropertyChanged(nameof(ParameterKey));
            }
        }

		/// <summary>
		/// Unique identifier for Environment Variable Definition associated with Environment Variable Definition.
		/// </summary>
		[AttributeLogicalName("parentdefinitionid")]
        public EntityReference? ParentDefinitionId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<EntityReference?>("parentdefinitionid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(ParentDefinitionId));
                SetAttributeValue("parentdefinitionid", value);
                OnPropertyChanged(nameof(ParentDefinitionId));
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
		/// Environment variable secret store.
		/// </summary>
		[AttributeLogicalName("secretstore")]
        public OptionSetValue? SecretStore
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<OptionSetValue?>("secretstore");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(SecretStore));
                SetAttributeValue("secretstore", value);
                OnPropertyChanged(nameof(SecretStore));
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
		/// Status of the Environment Variable Definition
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
		/// Reason for the status of the Environment Variable Definition
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
		/// Environment variable value type.
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
		/// For internal use only.
		/// </summary>
		[AttributeLogicalName("valueschema")]
        public string? ValueSchema
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("valueschema");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(ValueSchema));
                SetAttributeValue("valueschema", value);
                OnPropertyChanged(nameof(ValueSchema));
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
		/// 1:N envdefinition_envdefinition
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("envdefinition_envdefinition")]
		public System.Collections.Generic.IEnumerable<EnvironmentVariableDefinition> EnvdefinitionEnvdefinition
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<EnvironmentVariableDefinition>("envdefinition_envdefinition", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("EnvdefinitionEnvdefinition");
				this.SetRelatedEntities<EnvironmentVariableDefinition>("envdefinition_envdefinition", null, value);
				this.OnPropertyChanged("EnvdefinitionEnvdefinition");
			}
		}

		/// <summary>
		/// 1:N environmentvariabledefinition_AsyncOperations
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("environmentvariabledefinition_AsyncOperations")]
		public System.Collections.Generic.IEnumerable<AsyncOperation> EnvironmentvariabledefinitionAsyncOperations
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<AsyncOperation>("environmentvariabledefinition_AsyncOperations", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("EnvironmentvariabledefinitionAsyncOperations");
				this.SetRelatedEntities<AsyncOperation>("environmentvariabledefinition_AsyncOperations", null, value);
				this.OnPropertyChanged("EnvironmentvariabledefinitionAsyncOperations");
			}
		}

		/// <summary>
		/// 1:N environmentvariabledefinition_environmentvariablevalue
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("environmentvariabledefinition_environmentvariablevalue")]
		public System.Collections.Generic.IEnumerable<EnvironmentVariableValue> EnvironmentvariabledefinitionEnvironmentvariablevalue
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<EnvironmentVariableValue>("environmentvariabledefinition_environmentvariablevalue", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("EnvironmentvariabledefinitionEnvironmentvariablevalue");
				this.SetRelatedEntities<EnvironmentVariableValue>("environmentvariabledefinition_environmentvariablevalue", null, value);
				this.OnPropertyChanged("EnvironmentvariabledefinitionEnvironmentvariablevalue");
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
                public struct IsRequired
                {
                    public const bool No = false;
                    public const bool Yes = true;
                }
			    public struct SecretStore
                {
					public const int AzureKeyVault = 0;
					public const int MicrosoftDataverse = 1;
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
					public const int String = 100000000;
					public const int Number = 100000001;
					public const int Boolean = 100000002;
					public const int JSON = 100000003;
					public const int DataSource = 100000004;
					public const int Secret = 100000005;
                }
		}
		#endregion

		#region LogicalNames
		public static class LogicalNames
		{
				public const string EnvironmentVariableDefinitionId = "environmentvariabledefinitionid";
				public const string ApiId = "apiid";
				public const string ComponentState = "componentstate";
				public const string ConnectionReferenceId = "connectionreferenceid";
				public const string CreatedBy = "createdby";
				public const string CreatedOn = "createdon";
				public const string CreatedOnBehalfBy = "createdonbehalfby";
				public const string DefaultValue = "defaultvalue";
				public const string Description = "description";
				public const string DisplayName = "displayname";
				public const string EnvironmentVariableDefinitionIdUnique = "environmentvariabledefinitionidunique";
				public const string Hint = "hint";
				public const string ImportSequenceNumber = "importsequencenumber";
				public const string IntroducedVersion = "introducedversion";
				public const string IsCustomizable = "iscustomizable";
				public const string IsManaged = "ismanaged";
				public const string IsRequired = "isrequired";
				public const string ModifiedBy = "modifiedby";
				public const string ModifiedOn = "modifiedon";
				public const string ModifiedOnBehalfBy = "modifiedonbehalfby";
				public const string OverriddenCreatedOn = "overriddencreatedon";
				public const string OverwriteTime = "overwritetime";
				public const string OwnerId = "ownerid";
				public const string OwningBusinessUnit = "owningbusinessunit";
				public const string OwningTeam = "owningteam";
				public const string OwningUser = "owninguser";
				public const string ParameterKey = "parameterkey";
				public const string ParentDefinitionId = "parentdefinitionid";
				public const string SchemaName = "schemaname";
				public const string SecretStore = "secretstore";
				public const string SolutionId = "solutionid";
				public const string Statecode = "statecode";
				public const string Statuscode = "statuscode";
				public const string TimeZoneRuleVersionNumber = "timezoneruleversionnumber";
				public const string Type = "type";
				public const string UTCConversionTimeZoneCode = "utcconversiontimezonecode";
				public const string ValueSchema = "valueschema";
				public const string VersionNumber = "versionnumber";
		}
		#endregion

		#region AlternateKeys
		public static class AlternateKeys
		{
				public const string EnvironmentVariableDefinitionKey = "definitionkey";
		}
		#endregion

		#region Relations
        public static class Relations
        {
            public static class OneToMany
            {
				public const string EnvdefinitionEnvdefinition = "envdefinition_envdefinition";
				public const string EnvironmentvariabledefinitionAsyncOperations = "environmentvariabledefinition_AsyncOperations";
				public const string EnvironmentvariabledefinitionBulkDeleteFailures = "environmentvariabledefinition_BulkDeleteFailures";
				public const string EnvironmentvariabledefinitionCredentialCyberarkobject = "environmentvariabledefinition_credential_cyberarkobject";
				public const string EnvironmentvariabledefinitionCredentialCyberarksafe = "environmentvariabledefinition_credential_cyberarksafe";
				public const string EnvironmentvariabledefinitionCredentialCyberarkusername = "environmentvariabledefinition_credential_cyberarkusername";
				public const string EnvironmentvariabledefinitionCredentialPassword = "environmentvariabledefinition_credential_password";
				public const string EnvironmentvariabledefinitionCredentialUsername = "environmentvariabledefinition_credential_username";
				public const string EnvironmentvariabledefinitionDuplicateBaseRecord = "environmentvariabledefinition_DuplicateBaseRecord";
				public const string EnvironmentvariabledefinitionDuplicateMatchingRecord = "environmentvariabledefinition_DuplicateMatchingRecord";
				public const string EnvironmentvariabledefinitionEnvironmentvariablevalue = "environmentvariabledefinition_environmentvariablevalue";
				public const string EnvironmentvariabledefinitionMailboxTrackingFolders = "environmentvariabledefinition_MailboxTrackingFolders";
				public const string EnvironmentvariabledefinitionPrincipalObjectAttributeAccesses = "environmentvariabledefinition_PrincipalObjectAttributeAccesses";
				public const string EnvironmentvariabledefinitionProcessSession = "environmentvariabledefinition_ProcessSession";
				public const string EnvironmentvariabledefinitionSyncErrors = "environmentvariabledefinition_SyncErrors";
				public const string EnvironmentvariabledefinitionUserEntityInstanceDatas = "environmentvariabledefinition_UserEntityInstanceDatas";
				public const string EnvvardefinitionPowerbimashupparameter = "envvardefinition_powerbimashupparameter";
            }

            public static class ManyToOne
            {
				public const string BusinessUnitEnvironmentvariabledefinition = "business_unit_environmentvariabledefinition";
				public const string EnvdefinitionEnvdefinition = "envdefinition_envdefinition";
				public const string LkEnvironmentvariabledefinitionCreatedby = "lk_environmentvariabledefinition_createdby";
				public const string LkEnvironmentvariabledefinitionCreatedonbehalfby = "lk_environmentvariabledefinition_createdonbehalfby";
				public const string LkEnvironmentvariabledefinitionModifiedby = "lk_environmentvariabledefinition_modifiedby";
				public const string LkEnvironmentvariabledefinitionModifiedonbehalfby = "lk_environmentvariabledefinition_modifiedonbehalfby";
				public const string OwnerEnvironmentvariabledefinition = "owner_environmentvariabledefinition";
				public const string TeamEnvironmentvariabledefinition = "team_environmentvariabledefinition";
				public const string UserEnvironmentvariabledefinition = "user_environmentvariabledefinition";
            }

            public static class ManyToMany
            {
				public const string BotEnvironmentvariabledefinition = "bot_environmentvariabledefinition";
				public const string BotcomponentEnvironmentvariabledefinition = "botcomponent_environmentvariabledefinition";
				public const string MsdynConnectordatasourceEnvironmentvariable = "msdyn_connectordatasource_environmentvariable";
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
        public static EnvironmentVariableDefinition Retrieve(IOrganizationService service, Guid id)
        {
            return Retrieve(service,id, new ColumnSet(true));
        }

        public static EnvironmentVariableDefinition Retrieve(IOrganizationService service, Guid id, ColumnSet columnSet)
        {
            return service.Retrieve("environmentvariabledefinition", id, columnSet).ToEntity<EnvironmentVariableDefinition>();
        }

        public EnvironmentVariableDefinition GetChangedEntity()
        {
            if (_trackChanges)
            {
                var attr = new AttributeCollection();
                foreach (var attrName in _changedProperties.Value.Select(changedProperty => ((AttributeLogicalNameAttribute) GetType().GetProperty(changedProperty)!.GetCustomAttribute(typeof (AttributeLogicalNameAttribute))!).LogicalName).Where(attrName => Contains(attrName)))
                {
                    attr.Add(attrName,this[attrName]);
                }
                return new  EnvironmentVariableDefinition(Id) {Attributes = attr };
            }
            return this;
        }
        #endregion
	}

	#region Context
	public partial class DataContext
	{
		public IQueryable<EnvironmentVariableDefinition> EnvironmentVariableDefinitionSet
		{
			get
			{
				return CreateQuery<EnvironmentVariableDefinition>();
			}
		}
	}
	#endregion
}
