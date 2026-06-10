using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using AttributeCollection = Microsoft.Xrm.Sdk.AttributeCollection;

// ReSharper disable All
namespace dgt.power.dataverse
{
    /// <inheritdoc cref="Microsoft.Xrm.Sdk.Entity" />
    /// <summary>
	/// Contains data to represent an Azure Active Directory Application used to connect to secure web-hosted resources.
	/// </summary>
    [DataContract]
    [EntityLogicalName("managedidentity")]
    [System.CodeDom.Compiler.GeneratedCode("dgtp", "2026")]
    [ExcludeFromCodeCoverage]
    public partial class ManagedIdentity : Entity, INotifyPropertyChanging, INotifyPropertyChanged
    {
        #region ctor
        [DebuggerNonUserCode]
        public ManagedIdentity() : this(false)
        {
        }
        [DebuggerNonUserCode]
        public ManagedIdentity(bool trackChanges = false) : base(EntityLogicalName)
        {
            _trackChanges = trackChanges;
        }
        [DebuggerNonUserCode]
        public ManagedIdentity(Guid id, bool trackChanges = false) : base(EntityLogicalName, id)
        {
            _trackChanges = trackChanges;
        }
        [DebuggerNonUserCode]
        public ManagedIdentity(KeyAttributeCollection keyAttributes, bool trackChanges = false) : base(EntityLogicalName, keyAttributes)
        {
            _trackChanges = trackChanges;
        }
        [DebuggerNonUserCode]
        public ManagedIdentity(string keyName, object keyValue, bool trackChanges = false) : base(EntityLogicalName, keyName, keyValue)
        {
            _trackChanges = trackChanges;
        }
        #endregion

        #region fields
        private readonly bool _trackChanges;
        private readonly Lazy<HashSet<string>> _changedProperties = new();
        #endregion

        #region consts
        public const string EntityLogicalName = "managedidentity";
        public const string PrimaryNameAttribute = "name";
        public const int EntityTypeCode = 10034;
        #endregion

        #region Events
        #pragma warning disable CS8612
        public event PropertyChangedEventHandler? PropertyChanged;
        public event PropertyChangingEventHandler? PropertyChanging;
        #pragma warning restore CS8612
        [DebuggerNonUserCode]
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
        [AttributeLogicalName("managedidentityid")]
        public new Guid Id
        {
            [DebuggerNonUserCode]
            get
            {
                return base.Id;
            }
            [DebuggerNonUserCode]
            set
            {
                ManagedIdentityId = value;
            }
        }

        /// <summary>
		/// Unique identifier for entity instances
		/// </summary>
        [AttributeLogicalName("managedidentityid")]
        public Guid? ManagedIdentityId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<Guid?>("managedidentityid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("managedidentityid", value);
                base.Id = value.HasValue ? value.Value : Guid.Empty;
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Application Id
		/// </summary>
        [AttributeLogicalName("applicationid")]
        public Guid? ApplicationId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<Guid?>("applicationid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("applicationid", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Contains a secret for the Azure Active Directory application. Once set, it cannot be read except by Dataverse.
		/// </summary>
        [AttributeLogicalName("clientsecret")]
        public string? ClientSecret
        {
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("clientsecret", value);
                OnPropertyChanged();
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
		/// Where the Managed Identity will get the credentials to use.
		/// </summary>
        [AttributeLogicalName("credentialsource")]
        public OptionSetValue? CredentialSource
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("credentialsource");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("credentialsource", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Determines Identity type for Managed Identity
		/// </summary>
        [AttributeLogicalName("identitytype")]
        public OptionSetValue? IdentityType
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("identitytype");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("identitytype", value);
                OnPropertyChanged();
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
                OnPropertyChanging();
                SetAttributeValue("importsequencenumber", value);
                OnPropertyChanged();
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
                OnPropertyChanging();
                SetAttributeValue("iscustomizable", value);
                OnPropertyChanged();
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
		/// Unique identifier for keyvaultreference which contains the secret.
		/// </summary>
        [AttributeLogicalName("keyvaultreferenceid")]
        public EntityReference? KeyVaultReferenceId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<EntityReference?>("keyvaultreferenceid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("keyvaultreferenceid", value);
                OnPropertyChanged();
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
		/// The name assigned to this Managed Identity.
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
                OnPropertyChanging();
                SetAttributeValue("name", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// ObjectId
		/// </summary>
        [AttributeLogicalName("objectid")]
        public Guid? ObjectId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<Guid?>("objectid");
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
                OnPropertyChanging();
                SetAttributeValue("overriddencreatedon", value);
                OnPropertyChanged();
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
                OnPropertyChanging();
                SetAttributeValue("ownerid", value);
                OnPropertyChanged();
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
		/// Status of the Managed Identity
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
                OnPropertyChanging();
                SetAttributeValue("statecode", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Reason for the status of the Managed Identity
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
                OnPropertyChanging();
                SetAttributeValue("statuscode", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Where the Scope of the SubjectName for Managed Identity will be determined.
		/// </summary>
        [AttributeLogicalName("subjectscope")]
        public OptionSetValue? SubjectScope
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("subjectscope");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("subjectscope", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// The Id of the Azure Active Directory Tenant that the Application is part of.
		/// </summary>
        [AttributeLogicalName("tenantid")]
        public Guid? TenantId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<Guid?>("tenantid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("tenantid", value);
                OnPropertyChanged();
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
                OnPropertyChanging();
                SetAttributeValue("timezoneruleversionnumber", value);
                OnPropertyChanged();
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
                OnPropertyChanging();
                SetAttributeValue("utcconversiontimezonecode", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Version indicating the format of the FIC subject.
		/// </summary>
        [AttributeLogicalName("version")]
        public int? Version
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("version");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("version", value);
                OnPropertyChanged();
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
        /// 1:N managedidentity_AsyncOperations
        /// </summary>
        [RelationshipSchemaName("managedidentity_AsyncOperations")]
        public IEnumerable<AsyncOperation> ManagedidentityAsyncOperations
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<AsyncOperation>("managedidentity_AsyncOperations", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("managedidentity_AsyncOperations", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N managedidentity_PluginAssembly
        /// </summary>
        [RelationshipSchemaName("managedidentity_PluginAssembly")]
        public IEnumerable<PluginAssembly> ManagedidentityPluginAssembly
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<PluginAssembly>("managedidentity_PluginAssembly", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("managedidentity_PluginAssembly", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N managedidentity_pluginpackage
        /// </summary>
        [RelationshipSchemaName("managedidentity_pluginpackage")]
        public IEnumerable<PluginPackage> ManagedidentityPluginpackage
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<PluginPackage>("managedidentity_pluginpackage", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("managedidentity_pluginpackage", null, value);
                OnPropertyChanged();
            }
        }
        #endregion

        #region Options
        public static partial class Options
        {
            public struct ComponentState
            {
                public const int Published = 0;
                public const int Unpublished = 1;
                public const int Deleted = 2;
                public const int DeletedUnpublished = 3;
            }
            public struct CredentialSource
            {
                public const int ClientSecret = 0;
                public const int KeyVault = 1;
                public const int IsManaged = 2;
                public const int MicrosoftFirstPartyCertificate = 3;
            }
            public struct IdentityType
            {
                public const int AppRegisteration = 0;
                public const int AgentId = 1;
                public const int AgentIdentityBlueprint = 2;
                public const int AgentUser = 3;
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
            public struct SubjectScope
            {
                public const int GlobalScope = 0;
                public const int EnviornmentScope = 1;
                public const int DevOnlyScope = 2;
            }
        }
        #endregion

        #region LogicalNames
        public static partial class LogicalNames
        {
            public const string ManagedIdentityId = "managedidentityid";
            public const string ApplicationId = "applicationid";
            public const string ClientSecret = "clientsecret";
            public const string ComponentIdUnique = "componentidunique";
            public const string ComponentState = "componentstate";
            public const string CreatedBy = "createdby";
            public const string CreatedOn = "createdon";
            public const string CreatedOnBehalfBy = "createdonbehalfby";
            public const string CredentialSource = "credentialsource";
            public const string IdentityType = "identitytype";
            public const string ImportSequenceNumber = "importsequencenumber";
            public const string IsCustomizable = "iscustomizable";
            public const string IsManaged = "ismanaged";
            public const string KeyVaultReferenceId = "keyvaultreferenceid";
            public const string ModifiedBy = "modifiedby";
            public const string ModifiedOn = "modifiedon";
            public const string ModifiedOnBehalfBy = "modifiedonbehalfby";
            public const string Name = "name";
            public const string ObjectId = "objectid";
            public const string OverriddenCreatedOn = "overriddencreatedon";
            public const string OverwriteTime = "overwritetime";
            public const string OwnerId = "ownerid";
            public const string OwningBusinessUnit = "owningbusinessunit";
            public const string OwningTeam = "owningteam";
            public const string OwningUser = "owninguser";
            public const string SolutionId = "solutionid";
            public const string Statecode = "statecode";
            public const string Statuscode = "statuscode";
            public const string SubjectScope = "subjectscope";
            public const string TenantId = "tenantid";
            public const string TimeZoneRuleVersionNumber = "timezoneruleversionnumber";
            public const string UTCConversionTimeZoneCode = "utcconversiontimezonecode";
            public const string Version = "version";
            public const string VersionNumber = "versionnumber";
        }
        #endregion

        #region Relations
        public static partial class Relations
        {
            public static class OneToMany
            {
                public const string ComponentIdCertificateCredentialManagedidentity = "ComponentId_CertificateCredential_Managedidentity";
                public const string ManagedidentityAsyncOperations = "managedidentity_AsyncOperations";
                public const string ManagedidentityBulkDeleteFailures = "managedidentity_BulkDeleteFailures";
                public const string ManagedidentityDuplicateBaseRecord = "managedidentity_DuplicateBaseRecord";
                public const string ManagedidentityDuplicateMatchingRecord = "managedidentity_DuplicateMatchingRecord";
                public const string ManagedidentityEmailserverprofileAcsmanagedidentityid = "managedidentity_emailserverprofile_acsmanagedidentityid";
                public const string ManagedidentityEmailserverprofileManagedidentityid = "managedidentity_emailserverprofile_managedidentityid";
                public const string ManagedidentityEmailserverprofilePowerplatformmanagedidentityid = "managedidentity_emailserverprofile_powerplatformmanagedidentityid";
                public const string ManagedidentityEmailserverprofilePurviewmanagedidentityid = "managedidentity_emailserverprofile_purviewmanagedidentityid";
                public const string ManagedidentityGithubappconfigManagedIdentityId = "managedidentity_githubappconfig_ManagedIdentityId";
                public const string ManagedidentityKeyVaultReference = "managedidentity_KeyVaultReference";
                public const string ManagedidentityMailboxTrackingFolders = "managedidentity_MailboxTrackingFolders";
                public const string ManagedIdentityMCPServerManagedIdentityId = "ManagedIdentity_MCPServer_ManagedIdentityId";
                public const string ManagedidentityPluginAssembly = "managedidentity_PluginAssembly";
                public const string ManagedidentityPluginpackage = "managedidentity_pluginpackage";
                public const string ManagedidentityPrincipalObjectAttributeAccesses = "managedidentity_PrincipalObjectAttributeAccesses";
                public const string ManagedidentityProcessSession = "managedidentity_ProcessSession";
                public const string ManagedidentityServiceEndpoint = "managedidentity_ServiceEndpoint";
                public const string ManagedIdentitySharePointManagedIdentityManagedIdentityId = "ManagedIdentity_SharePointManagedIdentity_ManagedIdentityId";
                public const string ManagedidentitySyncErrors = "managedidentity_SyncErrors";
                public const string ManagedidentityUserEntityInstanceDatas = "managedidentity_UserEntityInstanceDatas";
                public const string PowerPagesManagedIdentityManagedIdentityManagedIdentity = "PowerPagesManagedIdentity_ManagedIdentity_ManagedIdentity";
            }

            public static partial class ManyToOne
            {
                public const string BusinessUnitManagedidentity = "business_unit_managedidentity";
                public const string KeyvaultreferenceManagedIdentity = "keyvaultreference_ManagedIdentity";
                public const string LkManagedidentityCreatedby = "lk_managedidentity_createdby";
                public const string LkManagedidentityCreatedonbehalfby = "lk_managedidentity_createdonbehalfby";
                public const string LkManagedidentityModifiedby = "lk_managedidentity_modifiedby";
                public const string LkManagedidentityModifiedonbehalfby = "lk_managedidentity_modifiedonbehalfby";
                public const string OwnerManagedidentity = "owner_managedidentity";
                public const string TeamManagedidentity = "team_managedidentity";
                public const string UserManagedidentity = "user_managedidentity";
            }

            public static partial class ManyToMany
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

        public static ManagedIdentity Retrieve(IOrganizationService service, Guid id)
        {
            return Retrieve(service, id, new ColumnSet(true));
        }

        public static ManagedIdentity Retrieve(IOrganizationService service, Guid id, ColumnSet columnSet)
        {
            return service.Retrieve("managedidentity", id, columnSet).ToEntity<ManagedIdentity>();
        }

        public ManagedIdentity GetChangedEntity()
        {
            if (!_trackChanges)
            {
                return this;
            }
                var attr = new AttributeCollection();
            foreach (var attrName in _changedProperties.Value.Select(changedProperty => ((AttributeLogicalNameAttribute) GetType().GetProperty(changedProperty)!.GetCustomAttribute(typeof(AttributeLogicalNameAttribute))!).LogicalName).Where(attrName => Contains(attrName)))
            {
                attr.Add(attrName, this[attrName]);
            }
            return new ManagedIdentity(Id) { Attributes = attr };
        }
        #endregion
    }

    #region Context
    public partial class DataContext
    {
        public IQueryable<ManagedIdentity> ManagedIdentitySet
        {
            get
            {
                return CreateQuery<ManagedIdentity>();
            }
        }
    }
    #endregion
}
