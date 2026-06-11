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
	/// Collection of system users that routinely collaborate. Teams can be used to simplify record sharing and provide team members with common access to organization data when team members belong to different Business Units.
	/// </summary>
    [DataContract]
    [EntityLogicalName("team")]
    [System.CodeDom.Compiler.GeneratedCode("dgtp", "2026")]
    [ExcludeFromCodeCoverage]
    [SuppressMessage("Design", "CA1034")]
    [SuppressMessage("Performance", "CA1815")]
    public partial class Team : Entity, INotifyPropertyChanging, INotifyPropertyChanged
    {
        #region ctor
        [DebuggerNonUserCode]
        public Team() : this(false)
        {
        }
        [DebuggerNonUserCode]
        public Team(bool trackChanges = false) : base(EntityLogicalName)
        {
            _trackChanges = trackChanges;
        }
        [DebuggerNonUserCode]
        public Team(Guid id, bool trackChanges = false) : base(EntityLogicalName, id)
        {
            _trackChanges = trackChanges;
        }
        [DebuggerNonUserCode]
        public Team(KeyAttributeCollection keyAttributes, bool trackChanges = false) : base(EntityLogicalName, keyAttributes)
        {
            _trackChanges = trackChanges;
        }
        [DebuggerNonUserCode]
        public Team(string keyName, object keyValue, bool trackChanges = false) : base(EntityLogicalName, keyName, keyValue)
        {
            _trackChanges = trackChanges;
        }
        #endregion

        #region fields
        private readonly bool _trackChanges;
        private readonly Lazy<HashSet<string>> _changedProperties = new();
        #endregion

        #region consts
        public const string EntityLogicalName = "team";
        public const string PrimaryNameAttribute = "name";
        public const int EntityTypeCode = 9;
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
        [AttributeLogicalName("teamid")]
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
                TeamId = value;
            }
        }

        /// <summary>
		/// Unique identifier for the team.
		/// </summary>
        [AttributeLogicalName("teamid")]
        public Guid? TeamId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<Guid?>("teamid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("teamid", value);
                base.Id = value.HasValue ? value.Value : Guid.Empty;
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier of the user primary responsible for the team.
		/// </summary>
        [AttributeLogicalName("administratorid")]
        public EntityReference? AdministratorId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<EntityReference?>("administratorid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("administratorid", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// The object Id for a group.
		/// </summary>
        [AttributeLogicalName("azureactivedirectoryobjectid")]
        public Guid? AzureActiveDirectoryObjectId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<Guid?>("azureactivedirectoryobjectid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("azureactivedirectoryobjectid", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier of the business unit with which the team is associated.
		/// </summary>
        [AttributeLogicalName("businessunitid")]
        public EntityReference? BusinessUnitId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<EntityReference?>("businessunitid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("businessunitid", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier of the user who created the team.
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
		/// Date and time when the team was created.
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
		/// Unique identifier of the delegate user who created the team.
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
		/// The delegated authorization context for the team.
		/// </summary>
        [AttributeLogicalName("delegatedauthorizationid")]
        public EntityReference? DelegatedAuthorizationId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<EntityReference?>("delegatedauthorizationid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("delegatedauthorizationid", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Description of the team.
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
                OnPropertyChanging();
                SetAttributeValue("description", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Email address for the team.
		/// </summary>
        [AttributeLogicalName("emailaddress")]
        public string? EMailAddress
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("emailaddress");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("emailaddress", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Exchange rate for the currency associated with the team with respect to the base currency.
		/// </summary>
        [AttributeLogicalName("exchangerate")]
        public decimal? ExchangeRate
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<decimal?>("exchangerate");
            }
        }

        /// <summary>
		/// Unique identifier of the data import or data migration that created this record.
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
		/// Information about whether the team is a default business unit team.
		/// </summary>
        [AttributeLogicalName("isdefault")]
        public bool? IsDefault
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isdefault");
            }
        }

        
        [AttributeLogicalName("issastokenset")]
        public bool? IsSasTokenSet
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("issastokenset");
            }
        }

        
        [AttributeLogicalName("membershiptype")]
        public OptionSetValue? MembershipType
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("membershiptype");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("membershiptype", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier of the user who last modified the team.
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
		/// Date and time when the team was last modified.
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
		/// Unique identifier of the delegate user who last modified the team.
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
		/// Name of the team.
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
		/// Unique identifier of the organization associated with the team.
		/// </summary>
        [AttributeLogicalName("organizationid")]
        public Guid? OrganizationId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<Guid?>("organizationid");
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
		/// Shows the ID of the process.
		/// </summary>
        [AttributeLogicalName("processid")]
        public Guid? ProcessId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<Guid?>("processid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("processid", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier of the default queue for the team.
		/// </summary>
        [AttributeLogicalName("queueid")]
        public EntityReference? QueueId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<EntityReference?>("queueid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("queueid", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Choose the record that the team relates to.
		/// </summary>
        [AttributeLogicalName("regardingobjectid")]
        public EntityReference? RegardingObjectId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<EntityReference?>("regardingobjectid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("regardingobjectid", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// For internal use only.
		/// </summary>
        [AttributeLogicalName("sharelinkqualifier")]
        public string? ShareLinkQualifier
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("sharelinkqualifier");
            }
        }

        /// <summary>
		/// Shows the ID of the stage.
		/// </summary>
        [AttributeLogicalName("stageid")]
        public Guid? StageId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<Guid?>("stageid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("stageid", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Select whether the team will be managed by the system.
		/// </summary>
        [AttributeLogicalName("systemmanaged")]
        public bool? SystemManaged
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("systemmanaged");
            }
        }

        /// <summary>
		/// Shows the team template that is associated with the team.
		/// </summary>
        [AttributeLogicalName("teamtemplateid")]
        public EntityReference? TeamTemplateId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<EntityReference?>("teamtemplateid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("teamtemplateid", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Select the team type.
		/// </summary>
        [AttributeLogicalName("teamtype")]
        public OptionSetValue? TeamType
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("teamtype");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("teamtype", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier of the currency associated with the team.
		/// </summary>
        [AttributeLogicalName("transactioncurrencyid")]
        public EntityReference? TransactionCurrencyId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<EntityReference?>("transactioncurrencyid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("transactioncurrencyid", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// For internal use only.
		/// </summary>
        [AttributeLogicalName("traversedpath")]
        public string? TraversedPath
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("traversedpath");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("traversedpath", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Version number of the team.
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

        /// <summary>
		/// Pronunciation of the full name of the team, written in phonetic hiragana or katakana characters.
		/// </summary>
        [AttributeLogicalName("yominame")]
        public string? YomiName
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("yominame");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("yominame", value);
                OnPropertyChanged();
            }
        }
        #endregion

        #region NavigationProperties

        /// <summary>
        /// 1:N team_accounts
        /// </summary>
        [RelationshipSchemaName("team_accounts")]
        public IEnumerable<Account> TeamAccounts
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<Account>("team_accounts", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("team_accounts", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N team_asyncoperation
        /// </summary>
        [RelationshipSchemaName("team_asyncoperation")]
        public IEnumerable<AsyncOperation> TeamAsyncoperation
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<AsyncOperation>("team_asyncoperation", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("team_asyncoperation", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N Team_AsyncOperations
        /// </summary>
        [RelationshipSchemaName("Team_AsyncOperations")]
        public IEnumerable<AsyncOperation> TeamAsyncOperations
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<AsyncOperation>("Team_AsyncOperations", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("Team_AsyncOperations", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N team_contacts
        /// </summary>
        [RelationshipSchemaName("team_contacts")]
        public IEnumerable<Contact> TeamContacts
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<Contact>("team_contacts", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("team_contacts", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N team_customapi
        /// </summary>
        [RelationshipSchemaName("team_customapi")]
        public IEnumerable<CustomAPI> TeamCustomapi
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<CustomAPI>("team_customapi", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("team_customapi", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N team_DuplicateRules
        /// </summary>
        [RelationshipSchemaName("team_DuplicateRules")]
        public IEnumerable<DuplicateRule> TeamDuplicateRules
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<DuplicateRule>("team_DuplicateRules", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("team_DuplicateRules", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N team_managedidentity
        /// </summary>
        [RelationshipSchemaName("team_managedidentity")]
        public IEnumerable<ManagedIdentity> TeamManagedidentity
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<ManagedIdentity>("team_managedidentity", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("team_managedidentity", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N team_routingrule
        /// </summary>
        [RelationshipSchemaName("team_routingrule")]
        public IEnumerable<RoutingRule> TeamRoutingrule
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<RoutingRule>("team_routingrule", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("team_routingrule", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N team_routingruleitem
        /// </summary>
        [RelationshipSchemaName("team_routingruleitem")]
        public IEnumerable<RoutingRuleItem> TeamRoutingruleitem
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<RoutingRuleItem>("team_routingruleitem", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("team_routingruleitem", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N team_slaBase
        /// </summary>
        [RelationshipSchemaName("team_slaBase")]
        public IEnumerable<SLA> TeamSlaBase
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<SLA>("team_slaBase", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("team_slaBase", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N team_workflow
        /// </summary>
        [RelationshipSchemaName("team_workflow")]
        public IEnumerable<Workflow> TeamWorkflow
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<Workflow>("team_workflow", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("team_workflow", null, value);
                OnPropertyChanged();
            }
        }
        #endregion

        #region Options
        public static partial class Options
        {
            public struct IsDefault
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsSasTokenSet
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct MembershipType
            {
                public const int MembersAndGuests = 0;
                public const int Members = 1;
                public const int Owners = 2;
                public const int Guests = 3;
            }
            public struct SystemManaged
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct TeamType
            {
                public const int Owner = 0;
                public const int Access = 1;
                public const int SecurityGroup = 2;
                public const int OfficeGroup = 3;
            }
        }
        #endregion

        #region LogicalNames
        public static partial class LogicalNames
        {
            public const string TeamId = "teamid";
            public const string AdministratorId = "administratorid";
            public const string AzureActiveDirectoryObjectId = "azureactivedirectoryobjectid";
            public const string BusinessUnitId = "businessunitid";
            public const string CreatedBy = "createdby";
            public const string CreatedOn = "createdon";
            public const string CreatedOnBehalfBy = "createdonbehalfby";
            public const string DelegatedAuthorizationId = "delegatedauthorizationid";
            public const string Description = "description";
            public const string EMailAddress = "emailaddress";
            public const string ExchangeRate = "exchangerate";
            public const string ImportSequenceNumber = "importsequencenumber";
            public const string IsDefault = "isdefault";
            public const string IsSasTokenSet = "issastokenset";
            public const string MembershipType = "membershiptype";
            public const string ModifiedBy = "modifiedby";
            public const string ModifiedOn = "modifiedon";
            public const string ModifiedOnBehalfBy = "modifiedonbehalfby";
            public const string Name = "name";
            public const string OrganizationId = "organizationid";
            public const string OverriddenCreatedOn = "overriddencreatedon";
            public const string ProcessId = "processid";
            public const string QueueId = "queueid";
            public const string RegardingObjectId = "regardingobjectid";
            public const string ShareLinkQualifier = "sharelinkqualifier";
            public const string StageId = "stageid";
            public const string SystemManaged = "systemmanaged";
            public const string TeamTemplateId = "teamtemplateid";
            public const string TeamType = "teamtype";
            public const string TransactionCurrencyId = "transactioncurrencyid";
            public const string TraversedPath = "traversedpath";
            public const string VersionNumber = "versionnumber";
            public const string YomiName = "yominame";
        }
        #endregion

        #region AlternateKeys
        public static partial class AlternateKeys
        {
            public const string ObjectIdWithMembershipType = "aadobjectid_membershiptype";
        }
        #endregion

        #region Relations
        public static partial class Relations
        {
            public static class OneToMany
            {
                public const string AdxInviteredemptionTeamOwningteam = "adx_inviteredemption_team_owningteam";
                public const string AdxPortalcommentTeamOwningteam = "adx_portalcomment_team_owningteam";
                public const string AgenticscenarioTaskreviewersTeam = "agenticscenario_taskreviewers_team";
                public const string ChatTeamOwningteam = "chat_team_owningteam";
                public const string ImportFileTeam = "ImportFile_Team";
                public const string OwningTeamPostfollows = "OwningTeam_postfollows";
                public const string TeamAccounts = "team_accounts";
                public const string TeamActioncardusersettings = "team_actioncardusersettings";
                public const string TeamActivity = "team_activity";
                public const string TeamActivityfileattachment = "team_activityfileattachment";
                public const string TeamAdxInvitation = "team_adx_invitation";
                public const string TeamAdxSetting = "team_adx_setting";
                public const string TeamAgentconversationmessage = "team_agentconversationmessage";
                public const string TeamAgentconversationmessagefile = "team_agentconversationmessagefile";
                public const string TeamAgentfeeditem = "team_agentfeeditem";
                public const string TeamAgenthubgoal = "team_agenthubgoal";
                public const string TeamAgenthubinsight = "team_agenthubinsight";
                public const string TeamAgenthubmetric = "team_agenthubmetric";
                public const string TeamAgenticscenario = "team_agenticscenario";
                public const string TeamAgentmemory = "team_agentmemory";
                public const string TeamAgentrule = "team_agentrule";
                public const string TeamAgenttask = "team_agenttask";
                public const string TeamAiinsightcard = "team_aiinsightcard";
                public const string TeamAiplugin = "team_aiplugin";
                public const string TeamAipluginauth = "team_aipluginauth";
                public const string TeamAipluginconversationstarter = "team_aipluginconversationstarter";
                public const string TeamAipluginconversationstartermapping = "team_aipluginconversationstartermapping";
                public const string TeamAipluginexternalschema = "team_aipluginexternalschema";
                public const string TeamAipluginexternalschemaproperty = "team_aipluginexternalschemaproperty";
                public const string TeamAiplugingovernance = "team_aiplugingovernance";
                public const string TeamAiplugingovernanceext = "team_aiplugingovernanceext";
                public const string TeamAiplugininstance = "team_aiplugininstance";
                public const string TeamAipluginoperation = "team_aipluginoperation";
                public const string TeamAipluginoperationparameter = "team_aipluginoperationparameter";
                public const string TeamAipluginoperationresponsetemplate = "team_aipluginoperationresponsetemplate";
                public const string TeamAipluginusersetting = "team_aipluginusersetting";
                public const string TeamAiskillconfig = "team_aiskillconfig";
                public const string TeamAnnotations = "team_annotations";
                public const string TeamAppnotification = "team_appnotification";
                public const string TeamAppointment = "team_appointment";
                public const string TeamApprovalprocess = "team_approvalprocess";
                public const string TeamApprovalstageapproval = "team_approvalstageapproval";
                public const string TeamApprovalstagecondition = "team_approvalstagecondition";
                public const string TeamApprovalstageintelligent = "team_approvalstageintelligent";
                public const string TeamApprovalstageorder = "team_approvalstageorder";
                public const string TeamArchivecleanupinfo = "team_archivecleanupinfo";
                public const string TeamArchivecleanupoperation = "team_archivecleanupoperation";
                public const string TeamAsyncoperation = "team_asyncoperation";
                public const string TeamAsyncOperations = "Team_AsyncOperations";
                public const string TeamBot = "team_bot";
                public const string TeamBotcomponent = "team_botcomponent";
                public const string TeamBotcomponentcollection = "team_botcomponentcollection";
                public const string TeamBulkarchiveconfig = "team_bulkarchiveconfig";
                public const string TeamBulkarchivefailuredetail = "team_bulkarchivefailuredetail";
                public const string TeamBulkarchiveoperation = "team_bulkarchiveoperation";
                public const string TeamBulkDeleteFailures = "Team_BulkDeleteFailures";
                public const string TeamBusinessprocess = "team_businessprocess";
                public const string TeamCanvasappextendedmetadata = "team_canvasappextendedmetadata";
                public const string TeamCard = "team_card";
                public const string TeamCertificatecredential = "team_certificatecredential";
                public const string TeamChannelaccessprofile = "team_channelaccessprofile";
                public const string TeamComment = "team_comment";
                public const string TeamComponentversion = "team_componentversion";
                public const string TeamComputeruseagent = "team_computeruseagent";
                public const string TeamConnectioninstance = "team_connectioninstance";
                public const string TeamConnectionreference = "team_connectionreference";
                public const string TeamConnections1 = "team_connections1";
                public const string TeamConnections2 = "team_connections2";
                public const string TeamConnector = "team_connector";
                public const string TeamContacts = "team_contacts";
                public const string TeamConversationtranscript = "team_conversationtranscript";
                public const string TeamConvertrule = "team_convertrule";
                public const string TeamCopilotglossaryterm = "team_copilotglossaryterm";
                public const string TeamCopilotsynonyms = "team_copilotsynonyms";
                public const string TeamCredential = "team_credential";
                public const string TeamCustomapi = "team_customapi";
                public const string TeamCustomerRelationship = "team_customer_relationship";
                public const string TeamDatalakefolder = "team_datalakefolder";
                public const string TeamDesktopflowbinary = "team_desktopflowbinary";
                public const string TeamDesktopflowmodule = "team_desktopflowmodule";
                public const string TeamDuplicateBaseRecord = "Team_DuplicateBaseRecord";
                public const string TeamDuplicateMatchingRecord = "Team_DuplicateMatchingRecord";
                public const string TeamDuplicateRules = "team_DuplicateRules";
                public const string TeamDvfilesearch = "team_dvfilesearch";
                public const string TeamDvfilesearchattribute = "team_dvfilesearchattribute";
                public const string TeamDvfilesearchentity = "team_dvfilesearchentity";
                public const string TeamDvtablesearch = "team_dvtablesearch";
                public const string TeamDvtablesearchattribute = "team_dvtablesearchattribute";
                public const string TeamDvtablesearchentity = "team_dvtablesearchentity";
                public const string TeamEmail = "team_email";
                public const string TeamEmailTemplates = "team_email_templates";
                public const string TeamEmailserverprofile = "team_emailserverprofile";
                public const string TeamEnablearchivalrequest = "team_enablearchivalrequest";
                public const string TeamEnvironmentvariabledefinition = "team_environmentvariabledefinition";
                public const string TeamExchangesyncidmapping = "team_exchangesyncidmapping";
                public const string TeamExportedexcel = "team_exportedexcel";
                public const string TeamExportsolutionupload = "team_exportsolutionupload";
                public const string TeamExternalparty = "team_externalparty";
                public const string TeamFabricaiskill = "team_fabricaiskill";
                public const string TeamFax = "team_fax";
                public const string TeamFeaturecontrolsetting = "team_featurecontrolsetting";
                public const string TeamFederatedknowledgecitation = "team_federatedknowledgecitation";
                public const string TeamFederatedknowledgeconfiguration = "team_federatedknowledgeconfiguration";
                public const string TeamFederatedknowledgeentityconfiguration = "team_federatedknowledgeentityconfiguration";
                public const string TeamFederatedknowledgemetadatarefresh = "team_federatedknowledgemetadatarefresh";
                public const string TeamFlowaggregation = "team_flowaggregation";
                public const string TeamFlowcapacityassignment = "team_flowcapacityassignment";
                public const string TeamFlowcredentialapplication = "team_flowcredentialapplication";
                public const string TeamFlowevent = "team_flowevent";
                public const string TeamFlowgroup = "team_flowgroup";
                public const string TeamFlowmachine = "team_flowmachine";
                public const string TeamFlowmachinegroup = "team_flowmachinegroup";
                public const string TeamFlowmachineimage = "team_flowmachineimage";
                public const string TeamFlowmachineimageversion = "team_flowmachineimageversion";
                public const string TeamFlowmachinenetwork = "team_flowmachinenetwork";
                public const string TeamFlowrun = "team_flowrun";
                public const string TeamFlowsession = "team_flowsession";
                public const string TeamFlowsessionbinary = "team_flowsessionbinary";
                public const string TeamFlowtestsession = "team_flowtestsession";
                public const string TeamFlowtrigger = "team_flowtrigger";
                public const string TeamFlowtriggerinstance = "team_flowtriggerinstance";
                public const string TeamFxexpression = "team_fxexpression";
                public const string TeamGithubappconfig = "team_githubappconfig";
                public const string TeamGoal = "team_goal";
                public const string TeamGoalGoalowner = "team_goal_goalowner";
                public const string TeamGoalrollupquery = "team_goalrollupquery";
                public const string TeamGovernanceconfiguration = "team_governanceconfiguration";
                public const string TeamImportData = "team_ImportData";
                public const string TeamImportFiles = "team_ImportFiles";
                public const string TeamImportLogs = "team_ImportLogs";
                public const string TeamImportMaps = "team_ImportMaps";
                public const string TeamImports = "team_Imports";
                public const string TeamIndexedtrait = "team_indexedtrait";
                public const string TeamIntelligentmemory = "team_intelligentmemory";
                public const string TeamInteractionforemail = "team_interactionforemail";
                public const string TeamKeyvaultreference = "team_keyvaultreference";
                public const string TeamKnowledgearticle = "team_knowledgearticle";
                public const string TeamKnowledgefaq = "team_knowledgefaq";
                public const string TeamKnowledgesourceconsumer = "team_knowledgesourceconsumer";
                public const string TeamKnowledgesourceprofile = "team_knowledgesourceprofile";
                public const string TeamLetter = "team_letter";
                public const string TeamMailbox = "team_mailbox";
                public const string TeamMailboxtrackingcategory = "team_mailboxtrackingcategory";
                public const string TeamMailboxtrackingfolder = "team_mailboxtrackingfolder";
                public const string TeamManagedidentity = "team_managedidentity";
                public const string TeamMcpprompt = "team_mcpprompt";
                public const string TeamMcpresource = "team_mcpresource";
                public const string TeamMcpresourcecontent = "team_mcpresourcecontent";
                public const string TeamMcpserver = "team_mcpserver";
                public const string TeamMcptool = "team_mcptool";
                public const string TeamMsdynAibdataset = "team_msdyn_aibdataset";
                public const string TeamMsdynAibdatasetfile = "team_msdyn_aibdatasetfile";
                public const string TeamMsdynAibdatasetrecord = "team_msdyn_aibdatasetrecord";
                public const string TeamMsdynAibdatasetscontainer = "team_msdyn_aibdatasetscontainer";
                public const string TeamMsdynAibfeedbackloop = "team_msdyn_aibfeedbackloop";
                public const string TeamMsdynAibfile = "team_msdyn_aibfile";
                public const string TeamMsdynAibfileattacheddata = "team_msdyn_aibfileattacheddata";
                public const string TeamMsdynAiconfigurationsearch = "team_msdyn_aiconfigurationsearch";
                public const string TeamMsdynAidataprocessingevent = "team_msdyn_aidataprocessingevent";
                public const string TeamMsdynAidocumenttemplate = "team_msdyn_aidocumenttemplate";
                public const string TeamMsdynAievaluationconfiguration = "team_msdyn_aievaluationconfiguration";
                public const string TeamMsdynAievaluationrun = "team_msdyn_aievaluationrun";
                public const string TeamMsdynAievent = "team_msdyn_aievent";
                public const string TeamMsdynAifptrainingdocument = "team_msdyn_aifptrainingdocument";
                public const string TeamMsdynAimodel = "team_msdyn_aimodel";
                public const string TeamMsdynAimodelcatalog = "team_msdyn_aimodelcatalog";
                public const string TeamMsdynAiodimage = "team_msdyn_aiodimage";
                public const string TeamMsdynAiodlabel = "team_msdyn_aiodlabel";
                public const string TeamMsdynAiodtrainingboundingbox = "team_msdyn_aiodtrainingboundingbox";
                public const string TeamMsdynAiodtrainingimage = "team_msdyn_aiodtrainingimage";
                public const string TeamMsdynAioptimization = "team_msdyn_aioptimization";
                public const string TeamMsdynAioptimizationprivatedata = "team_msdyn_aioptimizationprivatedata";
                public const string TeamMsdynAitemplate = "team_msdyn_aitemplate";
                public const string TeamMsdynAitestcase = "team_msdyn_aitestcase";
                public const string TeamMsdynAitestcasedocument = "team_msdyn_aitestcasedocument";
                public const string TeamMsdynAitestcaseinput = "team_msdyn_aitestcaseinput";
                public const string TeamMsdynAitestrun = "team_msdyn_aitestrun";
                public const string TeamMsdynAitestrunbatch = "team_msdyn_aitestrunbatch";
                public const string TeamMsdynAnalysiscomponent = "team_msdyn_analysiscomponent";
                public const string TeamMsdynAnalysisjob = "team_msdyn_analysisjob";
                public const string TeamMsdynAnalysisoverride = "team_msdyn_analysisoverride";
                public const string TeamMsdynAnalysisresult = "team_msdyn_analysisresult";
                public const string TeamMsdynAnalysisresultdetail = "team_msdyn_analysisresultdetail";
                public const string TeamMsdynBulkharvestrunlog = "team_msdyn_bulkharvestrunlog";
                public const string TeamMsdynCopilotinteractions = "team_msdyn_copilotinteractions";
                public const string TeamMsdynCustomcontrolextendedsettings = "team_msdyn_customcontrolextendedsettings";
                public const string TeamMsdynDataflow = "team_msdyn_dataflow";
                public const string TeamMsdynDataflowDatalakefolder = "team_msdyn_dataflow_datalakefolder";
                public const string TeamMsdynDataflowconnectionreference = "team_msdyn_dataflowconnectionreference";
                public const string TeamMsdynDataflowrefreshhistory = "team_msdyn_dataflowrefreshhistory";
                public const string TeamMsdynDataflowtemplate = "team_msdyn_dataflowtemplate";
                public const string TeamMsdynDataworkspace = "team_msdyn_dataworkspace";
                public const string TeamMsdynDmsrequest = "team_msdyn_dmsrequest";
                public const string TeamMsdynDmsrequeststatus = "team_msdyn_dmsrequeststatus";
                public const string TeamMsdynDmssyncrequest = "team_msdyn_dmssyncrequest";
                public const string TeamMsdynDmssyncstatus = "team_msdyn_dmssyncstatus";
                public const string TeamMsdynEntitylinkchatconfiguration = "team_msdyn_entitylinkchatconfiguration";
                public const string TeamMsdynEntityrefreshhistory = "team_msdyn_entityrefreshhistory";
                public const string TeamMsdynFavoriteknowledgearticle = "team_msdyn_favoriteknowledgearticle";
                public const string TeamMsdynFederatedarticle = "team_msdyn_federatedarticle";
                public const string TeamMsdynFileupload = "team_msdyn_fileupload";
                public const string TeamMsdynFlowActionapprovalmodel = "team_msdyn_flow_actionapprovalmodel";
                public const string TeamMsdynFlowApproval = "team_msdyn_flow_approval";
                public const string TeamMsdynFlowApprovalrequest = "team_msdyn_flow_approvalrequest";
                public const string TeamMsdynFlowApprovalresponse = "team_msdyn_flow_approvalresponse";
                public const string TeamMsdynFlowApprovalstep = "team_msdyn_flow_approvalstep";
                public const string TeamMsdynFlowAwaitallactionapprovalmodel = "team_msdyn_flow_awaitallactionapprovalmodel";
                public const string TeamMsdynFlowAwaitallapprovalmodel = "team_msdyn_flow_awaitallapprovalmodel";
                public const string TeamMsdynFlowBasicapprovalmodel = "team_msdyn_flow_basicapprovalmodel";
                public const string TeamMsdynFlowFlowapproval = "team_msdyn_flow_flowapproval";
                public const string TeamMsdynFormmapping = "team_msdyn_formmapping";
                public const string TeamMsdynFunction = "team_msdyn_function";
                public const string TeamMsdynHarvesteligibilitycondition = "team_msdyn_harvesteligibilitycondition";
                public const string TeamMsdynHarvestworkitem = "team_msdyn_harvestworkitem";
                public const string TeamMsdynHealthcareFeedback = "team_msdyn_healthcare_feedback";
                public const string TeamMsdynHistoricalcaseharvestbatch = "team_msdyn_historicalcaseharvestbatch";
                public const string TeamMsdynHistoricalcaseharvestrun = "team_msdyn_historicalcaseharvestrun";
                public const string TeamMsdynIntegratedsearchprovider = "team_msdyn_integratedsearchprovider";
                public const string TeamMsdynInterimupdateknowledgearticle = "team_msdyn_interimupdateknowledgearticle";
                public const string TeamMsdynKalanguagesetting = "team_msdyn_kalanguagesetting";
                public const string TeamMsdynKbattachment = "team_msdyn_kbattachment";
                public const string TeamMsdynKmfederatedsearchconfig = "team_msdyn_kmfederatedsearchconfig";
                public const string TeamMsdynKnowledgearticlecustomentity = "team_msdyn_knowledgearticlecustomentity";
                public const string TeamMsdynKnowledgearticleimage = "team_msdyn_knowledgearticleimage";
                public const string TeamMsdynKnowledgearticletemplate = "team_msdyn_knowledgearticletemplate";
                public const string TeamMsdynKnowledgeassetconfiguration = "team_msdyn_knowledgeassetconfiguration";
                public const string TeamMsdynKnowledgeharvestjobrecord = "team_msdyn_knowledgeharvestjobrecord";
                public const string TeamMsdynKnowledgeharvestplan = "team_msdyn_knowledgeharvestplan";
                public const string TeamMsdynKnowledgeinteractioninsight = "team_msdyn_knowledgeinteractioninsight";
                public const string TeamMsdynKnowledgemanagementsetting = "team_msdyn_knowledgemanagementsetting";
                public const string TeamMsdynKnowledgepersonalfilter = "team_msdyn_knowledgepersonalfilter";
                public const string TeamMsdynKnowledgesearchfilter = "team_msdyn_knowledgesearchfilter";
                public const string TeamMsdynKnowledgesearchinsight = "team_msdyn_knowledgesearchinsight";
                public const string TeamMsdynMobileapp = "team_msdyn_mobileapp";
                public const string TeamMsdynObjectdetectionproduct = "team_msdyn_objectdetectionproduct";
                public const string TeamMsdynOnlineshopperintention = "team_msdyn_onlineshopperintention";
                public const string TeamMsdynPlan = "team_msdyn_plan";
                public const string TeamMsdynPlanartifact = "team_msdyn_planartifact";
                public const string TeamMsdynPlanattachment = "team_msdyn_planattachment";
                public const string TeamMsdynPmanalysishistory = "team_msdyn_pmanalysishistory";
                public const string TeamMsdynPmbusinessruleautomationconfig = "team_msdyn_pmbusinessruleautomationconfig";
                public const string TeamMsdynPmcalendar = "team_msdyn_pmcalendar";
                public const string TeamMsdynPmcalendarversion = "team_msdyn_pmcalendarversion";
                public const string TeamMsdynPminferredtask = "team_msdyn_pminferredtask";
                public const string TeamMsdynPmprocessextendedmetadataversion = "team_msdyn_pmprocessextendedmetadataversion";
                public const string TeamMsdynPmprocesstemplate = "team_msdyn_pmprocesstemplate";
                public const string TeamMsdynPmprocessusersettings = "team_msdyn_pmprocessusersettings";
                public const string TeamMsdynPmprocessversion = "team_msdyn_pmprocessversion";
                public const string TeamMsdynPmrecording = "team_msdyn_pmrecording";
                public const string TeamMsdynPmsimulation = "team_msdyn_pmsimulation";
                public const string TeamMsdynPmtab = "team_msdyn_pmtab";
                public const string TeamMsdynPmtemplate = "team_msdyn_pmtemplate";
                public const string TeamMsdynPmview = "team_msdyn_pmview";
                public const string TeamMsdynPowerappswrapbuild = "team_msdyn_powerappswrapbuild";
                public const string TeamMsdynQna = "team_msdyn_qna";
                public const string TeamMsdynRichtextfile = "team_msdyn_richtextfile";
                public const string TeamMsdynRtestructuredtemplateconfig = "team_msdyn_rtestructuredtemplateconfig";
                public const string TeamMsdynSalesforcestructuredobject = "team_msdyn_salesforcestructuredobject";
                public const string TeamMsdynSalesforcestructuredqnaconfig = "team_msdyn_salesforcestructuredqnaconfig";
                public const string TeamMsdynSchedule = "team_msdyn_schedule";
                public const string TeamMsdynServiceconfiguration = "team_msdyn_serviceconfiguration";
                public const string TeamMsdynSlakpi = "team_msdyn_slakpi";
                public const string TeamMsdynSolutionhealthrule = "team_msdyn_solutionhealthrule";
                public const string TeamMsdynSolutionhealthruleargument = "team_msdyn_solutionhealthruleargument";
                public const string TeamMsdynVirtualtablecolumncandidate = "team_msdyn_virtualtablecolumncandidate";
                public const string TeamMsdynceBotcontent = "team_msdynce_botcontent";
                public const string TeamMsecCleanup = "team_msec_cleanup";
                public const string TeamMspcatCatalogsubmissionfiles = "team_mspcat_catalogsubmissionfiles";
                public const string TeamMspcatPackagestore = "team_mspcat_packagestore";
                public const string TeamNewEventaggregator = "team_new_eventaggregator";
                public const string TeamNewEventaggregatorscans = "team_new_eventaggregatorscans";
                public const string TeamNewGaurdianfullscan = "team_new_gaurdianfullscan";
                public const string TeamNewGaurdianhealthchecks = "team_new_gaurdianhealthchecks";
                public const string TeamNlsqregistration = "team_nlsqregistration";
                public const string TeamOfficedocument = "team_officedocument";
                public const string TeamPdfsetting = "team_pdfsetting";
                public const string TeamPhonecall = "team_phonecall";
                public const string TeamPlannerbusinessscenario = "team_plannerbusinessscenario";
                public const string TeamPlannersyncaction = "team_plannersyncaction";
                public const string TeamPlugin = "team_plugin";
                public const string TeamPostRegardings = "team_PostRegardings";
                public const string TeamPostRoles = "team_PostRoles";
                public const string TeamPowerfxrule = "team_powerfxrule";
                public const string TeamPowerpagecomponent = "team_powerpagecomponent";
                public const string TeamPowerpagesddosalert = "team_powerpagesddosalert";
                public const string TeamPowerpagesite = "team_powerpagesite";
                public const string TeamPowerpagesitelanguage = "team_powerpagesitelanguage";
                public const string TeamPowerpagesitepublished = "team_powerpagesitepublished";
                public const string TeamPowerpageslog = "team_powerpageslog";
                public const string TeamPowerpagesmanagedidentity = "team_powerpagesmanagedidentity";
                public const string TeamPowerpagesscanreport = "team_powerpagesscanreport";
                public const string TeamPowerpagessiteaifeedback = "team_powerpagessiteaifeedback";
                public const string TeamPowerpagessourcefile = "team_powerpagessourcefile";
                public const string TeamPrincipalobjectattributeaccess = "team_principalobjectattributeaccess";
                public const string TeamPrincipalobjectattributeaccessPrincipalid = "team_principalobjectattributeaccess_principalid";
                public const string TeamPrivilegecheckerrun = "team_privilegecheckerrun";
                public const string TeamProcessorregistration = "team_processorregistration";
                public const string TeamProcesssession = "team_processsession";
                public const string TeamProcessSessions = "Team_ProcessSessions";
                public const string TeamProcessstageparameter = "team_processstageparameter";
                public const string TeamProfilerule = "team_profilerule";
                public const string TeamQueueitembaseWorkerid = "team_queueitembase_workerid";
                public const string TeamRecentlyused = "team_recentlyused";
                public const string TeamReconciliationentityinfo = "team_reconciliationentityinfo";
                public const string TeamReconciliationentitystepinfo = "team_reconciliationentitystepinfo";
                public const string TeamReconciliationinfo = "team_reconciliationinfo";
                public const string TeamRecurringappointmentmaster = "team_recurringappointmentmaster";
                public const string TeamRetaineddataexcel = "team_retaineddataexcel";
                public const string TeamRetentioncleanupinfo = "team_retentioncleanupinfo";
                public const string TeamRetentioncleanupoperation = "team_retentioncleanupoperation";
                public const string TeamRetentionconfig = "team_retentionconfig";
                public const string TeamRetentionfailuredetail = "team_retentionfailuredetail";
                public const string TeamRetentionoperation = "team_retentionoperation";
                public const string TeamRetentionsuccessdetail = "team_retentionsuccessdetail";
                public const string TeamRoutingrule = "team_routingrule";
                public const string TeamRoutingruleitem = "team_routingruleitem";
                public const string TeamSavingrule = "team_savingrule";
                public const string TeamSharepointdocumentlocation = "team_sharepointdocumentlocation";
                public const string TeamSharepointsite = "team_sharepointsite";
                public const string TeamSideloadedaiplugin = "team_sideloadedaiplugin";
                public const string TeamSignal = "team_signal";
                public const string TeamSignalregistration = "team_signalregistration";
                public const string TeamSkill = "team_skill";
                public const string TeamSlaBase = "team_slaBase";
                public const string TeamSocialactivity = "team_socialactivity";
                public const string TeamSolutioncomponentbatchconfiguration = "team_solutioncomponentbatchconfiguration";
                public const string TeamStagesolutionupload = "team_stagesolutionupload";
                public const string TeamSynapsedatabase = "team_synapsedatabase";
                public const string TeamSyncError = "team_SyncError";
                public const string TeamSyncErrors = "Team_SyncErrors";
                public const string TeamTag = "team_tag";
                public const string TeamTaggedflowsession = "team_taggedflowsession";
                public const string TeamTaggedprocess = "team_taggedprocess";
                public const string TeamTask = "team_task";
                public const string TeamTdsmetadata = "team_tdsmetadata";
                public const string TeamTeammobileofflineprofilemembershipTeamId = "team_teammobileofflineprofilemembership_TeamId";
                public const string TeamToolinggateway = "team_toolinggateway";
                public const string TeamToolinggatewaymcpserver = "team_toolinggatewaymcpserver";
                public const string TeamTrait = "team_trait";
                public const string TeamTraitregistration = "team_traitregistration";
                public const string TeamUnstructuredfilesearchentity = "team_unstructuredfilesearchentity";
                public const string TeamUnstructuredfilesearchrecord = "team_unstructuredfilesearchrecord";
                public const string TeamUnstructuredfilesearchrecordstatus = "team_unstructuredfilesearchrecordstatus";
                public const string TeamUserentityinstancedata = "team_userentityinstancedata";
                public const string TeamUserentityuisettings = "team_userentityuisettings";
                public const string TeamUserform = "team_userform";
                public const string TeamUserquery = "team_userquery";
                public const string TeamUserqueryvisualizations = "team_userqueryvisualizations";
                public const string TeamUxagentcomponent = "team_uxagentcomponent";
                public const string TeamUxagentcomponentrevision = "team_uxagentcomponentrevision";
                public const string TeamWorkflow = "team_workflow";
                public const string TeamWorkflowbinary = "team_workflowbinary";
                public const string TeamWorkflowlog = "team_workflowlog";
                public const string TeamWorkflowmetadata = "team_workflowmetadata";
                public const string TeamWorkqueue = "team_workqueue";
                public const string TeamWorkqueueitem = "team_workqueueitem";
                public const string UserentityinstancedataTeam = "userentityinstancedata_team";
            }

            public static partial class ManyToOne
            {
                public const string BusinessUnitTeams = "business_unit_teams";
                public const string KnowledgearticleTeams = "knowledgearticle_Teams";
                public const string LkTeamCreatedonbehalfby = "lk_team_createdonbehalfby";
                public const string LkTeamModifiedonbehalfby = "lk_team_modifiedonbehalfby";
                public const string LkTeambaseAdministratorid = "lk_teambase_administratorid";
                public const string LkTeambaseCreatedby = "lk_teambase_createdby";
                public const string LkTeambaseModifiedby = "lk_teambase_modifiedby";
                public const string OrganizationTeams = "organization_teams";
                public const string ProcessstageTeams = "processstage_teams";
                public const string QueueTeam = "queue_team";
                public const string TeamDelegatedauthorization = "team_delegatedauthorization";
                public const string TeamtemplateTeams = "teamtemplate_Teams";
                public const string TransactionCurrencyTeam = "TransactionCurrency_Team";
            }

            public static partial class ManyToMany
            {
                public const string MsdynFlowActionapprovalmodelrelationshipTeam = "msdyn_flow_actionapprovalmodelrelationship_team";
                public const string MsdynFlowAwaitallactionmodelrelationshipTeam = "msdyn_flow_awaitallactionmodelrelationship_team";
                public const string MsdynFlowAwaitallmodelrelationshipTeam = "msdyn_flow_awaitallmodelrelationship_team";
                public const string MsdynFlowBasicapprovalmodelrelationshipTeam = "msdyn_flow_basicapprovalmodelrelationship_team";
                public const string TeammembershipAssociation = "teammembership_association";
                public const string TeamprofilesAssociation = "teamprofiles_association";
                public const string TeamrolesAssociation = "teamroles_association";
                public const string TeamsyncattributemappingprofilesAssociation = "teamsyncattributemappingprofiles_association";
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

        public static Team Retrieve(IOrganizationService service, Guid id)
        {
            return Retrieve(service, id, new ColumnSet(true));
        }

        public static Team Retrieve(IOrganizationService service, Guid id, ColumnSet columnSet)
        {
            return service.Retrieve("team", id, columnSet).ToEntity<Team>();
        }

        public Team GetChangedEntity()
        {
            if (!_trackChanges)
            {
                return this;
            }

            var attr = new AttributeCollection();
            foreach (var attrName in _changedProperties.Value.Select(changedProperty => GetType().GetProperty(changedProperty)!.GetCustomAttribute<AttributeLogicalNameAttribute>()!.LogicalName).Where(attrName => Contains(attrName)))
            {
                attr.Add(attrName, this[attrName]);
            }
            return new Team(Id) { Attributes = attr };
        }
        #endregion
    }

    #region Context
    public partial class DataContext
    {
        public IQueryable<Team> TeamSet
        {
            get
            {
                return CreateQuery<Team>();
            }
        }
    }
    #endregion
}
