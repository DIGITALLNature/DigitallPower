using System.Diagnostics.CodeAnalysis;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System.Runtime.Serialization;

// ReSharper disable All
namespace dgt.power.dataverse
{
    /// <inheritdoc cref="Microsoft.Xrm.Sdk.Entity" />
    /// <summary>
	/// Stage associated with a process.
	/// </summary>
    [EntityLogicalName("processstage")]
    [System.CodeDom.Compiler.GeneratedCode("dgtp", "2026")]
    [ExcludeFromCodeCoverage]
    [SuppressMessage("Design", "CA1034")]
    [SuppressMessage("Performance", "CA1815")]
    public partial class ProcessStage : Entity
    {
        #region ctor
        public ProcessStage() : base(EntityLogicalName) { }

        public ProcessStage(Guid id) : base(EntityLogicalName, id) { }

        public ProcessStage(KeyAttributeCollection keyAttributes) : base(EntityLogicalName, keyAttributes) { }

        public ProcessStage(string keyName, object keyValue) : base(EntityLogicalName, keyName, keyValue) { }
        #endregion

        #region consts
        public const string EntityLogicalName = "processstage";
        public const string PrimaryNameAttribute = "stagename";
        public const int EntityTypeCode = 4724;
        #endregion

        #region Attributes
        [AttributeLogicalName("processstageid")]
        [IgnoreDataMember]
        public new Guid Id
        {
            get
            {
                return base.Id;
            }
            set
            {
                ProcessStageId = value;
            }
        }

        /// <summary>
		/// Shows the ID of the process stage record.
		/// </summary>
        [AttributeLogicalName("processstageid")]
        public Guid? ProcessStageId
        {
            get
            {
                return GetAttributeValue<Guid?>("processstageid");
            }
            set
            {
                SetAttributeValue("processstageid", value);
                base.Id = value.HasValue ? value.Value : Guid.Empty;
            }
        }

        /// <summary>
		/// Step metadata for process stage
		/// </summary>
        [AttributeLogicalName("clientdata")]
        public string? ClientData
        {
            get
            {
                return GetAttributeValue<string?>("clientdata");
            }
        }

        /// <summary>
		/// The connector associated with the stage.
		/// </summary>
        [AttributeLogicalName("connector")]
        public string? Connector
        {
            get
            {
                return GetAttributeValue<string?>("connector");
            }
            set
            {
                SetAttributeValue("connector", value);
            }
        }

        /// <summary>
		/// Whether the stage is a trigger
		/// </summary>
        [AttributeLogicalName("istrigger")]
        public bool? IsTrigger
        {
            get
            {
                return GetAttributeValue<bool?>("istrigger");
            }
            set
            {
                SetAttributeValue("istrigger", value);
            }
        }

        /// <summary>
		/// The operation id of the stage
		/// </summary>
        [AttributeLogicalName("operationid")]
        public string? OperationId
        {
            get
            {
                return GetAttributeValue<string?>("operationid");
            }
            set
            {
                SetAttributeValue("operationid", value);
            }
        }

        /// <summary>
		/// The operation kind
		/// </summary>
        [AttributeLogicalName("operationkind")]
        public OptionSetValue? OperationKind
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("operationkind");
            }
            set
            {
                SetAttributeValue("operationkind", value);
            }
        }

        /// <summary>
		/// The type of the operation
		/// </summary>
        [AttributeLogicalName("operationtype")]
        public OptionSetValue? OperationType
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("operationtype");
            }
            set
            {
                SetAttributeValue("operationtype", value);
            }
        }

        /// <summary>
		/// Enter the user or team who is assigned to manage the record. This field is updated every time the record is assigned to a different user.
		/// </summary>
        [AttributeLogicalName("ownerid")]
        public EntityReference? OwnerId
        {
            get
            {
                return GetAttributeValue<EntityReference?>("ownerid");
            }
        }

        /// <summary>
		/// Select the business unit that owns the record.
		/// </summary>
        [AttributeLogicalName("owningbusinessunit")]
        public Guid? OwningBusinessUnit
        {
            get
            {
                return GetAttributeValue<Guid?>("owningbusinessunit");
            }
        }

        /// <summary>
		/// The parameter name.
		/// </summary>
        [AttributeLogicalName("parametername")]
        public string? ParameterName
        {
            get
            {
                return GetAttributeValue<string?>("parametername");
            }
            set
            {
                SetAttributeValue("parametername", value);
            }
        }

        /// <summary>
		/// The parameter value.
		/// </summary>
        [AttributeLogicalName("parametervalue")]
        public string? ParameterValue
        {
            get
            {
                return GetAttributeValue<string?>("parametervalue");
            }
            set
            {
                SetAttributeValue("parametervalue", value);
            }
        }

        /// <summary>
		/// The parent stage for the parameter.
		/// </summary>
        [AttributeLogicalName("parentprocessstageid")]
        public EntityReference? ParentProcessStageId
        {
            get
            {
                return GetAttributeValue<EntityReference?>("parentprocessstageid");
            }
            set
            {
                SetAttributeValue("parentprocessstageid", value);
            }
        }

        /// <summary>
		/// Primary entity associated with the stage.
		/// </summary>
        [AttributeLogicalName("primaryentitytypecode")]
        public string? PrimaryEntityTypeCode
        {
            get
            {
                return GetAttributeValue<string?>("primaryentitytypecode");
            }
            set
            {
                SetAttributeValue("primaryentitytypecode", value);
            }
        }

        /// <summary>
		/// Shows the ID of the process.
		/// </summary>
        [AttributeLogicalName("processid")]
        public EntityReference? ProcessId
        {
            get
            {
                return GetAttributeValue<EntityReference?>("processid");
            }
            set
            {
                SetAttributeValue("processid", value);
            }
        }

        /// <summary>
		/// Select the category of the sales process.
		/// </summary>
        [AttributeLogicalName("stagecategory")]
        public OptionSetValue? StageCategory
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("stagecategory");
            }
            set
            {
                SetAttributeValue("stagecategory", value);
            }
        }

        /// <summary>
		/// Type a name for the process stage.
		/// </summary>
        [AttributeLogicalName("stagename")]
        public string? StageName
        {
            get
            {
                return GetAttributeValue<string?>("stagename");
            }
            set
            {
                SetAttributeValue("stagename", value);
            }
        }

        /// <summary>
		/// Version number of the process stage.
		/// </summary>
        [AttributeLogicalName("versionnumber")]
        public long? VersionNumber
        {
            get
            {
                return GetAttributeValue<long?>("versionnumber");
            }
        }
        #endregion

        #region NavigationProperties

        /// <summary>
        /// 1:N processstage_account
        /// </summary>
        [RelationshipSchemaName("processstage_account")]
        public IEnumerable<Account> ProcessstageAccount
        {
            get
            {
                return GetRelatedEntities<Account>("processstage_account", null);
            }
            set
            {
                SetRelatedEntities("processstage_account", null, value);
            }
        }

        /// <summary>
        /// 1:N processstage_contact
        /// </summary>
        [RelationshipSchemaName("processstage_contact")]
        public IEnumerable<Contact> ProcessstageContact
        {
            get
            {
                return GetRelatedEntities<Contact>("processstage_contact", null);
            }
            set
            {
                SetRelatedEntities("processstage_contact", null, value);
            }
        }

        /// <summary>
        /// 1:N processstage_parentprocessstage
        /// </summary>
        [RelationshipSchemaName("processstage_parentprocessstage")]
        public IEnumerable<ProcessStage> ProcessstageParentprocessstage
        {
            get
            {
                return GetRelatedEntities<ProcessStage>("processstage_parentprocessstage", null);
            }
            set
            {
                SetRelatedEntities("processstage_parentprocessstage", null, value);
            }
        }

        /// <summary>
        /// 1:N processstage_systemusers
        /// </summary>
        [RelationshipSchemaName("processstage_systemusers")]
        public IEnumerable<SystemUser> ProcessstageSystemusers
        {
            get
            {
                return GetRelatedEntities<SystemUser>("processstage_systemusers", null);
            }
            set
            {
                SetRelatedEntities("processstage_systemusers", null, value);
            }
        }

        /// <summary>
        /// 1:N processstage_teams
        /// </summary>
        [RelationshipSchemaName("processstage_teams")]
        public IEnumerable<Team> ProcessstageTeams
        {
            get
            {
                return GetRelatedEntities<Team>("processstage_teams", null);
            }
            set
            {
                SetRelatedEntities("processstage_teams", null, value);
            }
        }
        #endregion

        #region Options
        public static partial class Options
        {
            public struct IsTrigger
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct OperationKind
            {
                public const int Http = 473330000;
                public const int PowerApp = 473330001;
                public const int PowerAppV2 = 473330002;
                public const int Button = 473330003;
                public const int ApiConnection = 473330004;
                public const int Alert = 473330005;
                public const int EventGrid = 473330006;
                public const int CurrentTime = 473330007;
                public const int ConvertTimeZone = 473330008;
                public const int GetFutureTime = 473330009;
                public const int GetPastTime = 473330010;
                public const int AddToTime = 473330011;
                public const int SubtractFromTime = 473330012;
                public const int AzureMonitorAlert = 473330013;
                public const int SecurityCenterAlert = 473330014;
                public const int JsonToJson = 473330015;
                public const int JsonToText = 473330016;
                public const int XmlToJson = 473330017;
                public const int XmlToText = 473330018;
                public const int Geofence = 473330019;
                public const int ODataOpenApiConnection = 473330020;
                public const int IndexOf = 473330021;
                public const int Substring = 473330022;
                public const int VirtualAgent = 473330023;
                public const int FormatNumber = 473330024;
                public const int Skills = 473330025;
                public const int PowerPages = 473330026;
                public const int TeamsWebhook = 473330027;
                public const int TeamsWebhookV2 = 473330028;
            }
            public struct OperationType
            {
                public const int Http = 473330000;
                public const int ApiApp = 473330001;
                public const int Recurrence = 473330002;
                public const int Workflow = 473330003;
                public const int Flow = 473330004;
                public const int Wait = 473330005;
                public const int ApiConnection = 473330006;
                public const int OpenApiConnection = 473330007;
                public const int Manual = 473330008;
                public const int ApiConnectionWebhook = 473330009;
                public const int OpenApiConnectionWebhook = 473330010;
                public const int Response = 473330011;
                public const int HttpWebhook = 473330012;
                public const int Compose = 473330013;
                public const int Query = 473330014;
                public const int Function = 473330015;
                public const int ApiManagement = 473330016;
                public const int XmlValidation = 473330017;
                public const int FlatFileEncoding = 473330018;
                public const int Scope = 473330019;
                public const int Request = 473330020;
                public const int If = 473330021;
                public const int Foreach = 473330022;
                public const int Until = 473330023;
                public const int Xslt = 473330024;
                public const int FlatFileDecoding = 473330025;
                public const int Terminate = 473330026;
                public const int IntegrationAccountArtifactLookup = 473330027;
                public const int Switch = 473330028;
                public const int ParseJson = 473330029;
                public const int Table = 473330030;
                public const int Join = 473330031;
                public const int Select = 473330032;
                public const int InitializeVariable = 473330033;
                public const int IncrementVariable = 473330034;
                public const int DecrementVariable = 473330035;
                public const int SetVariable = 473330036;
                public const int AppendToArrayVariable = 473330037;
                public const int AppendToStringVariable = 473330038;
                public const int Batch = 473330039;
                public const int SendToBatch = 473330040;
                public const int SlidingWindow = 473330041;
                public const int Expression = 473330042;
                public const int Liquid = 473330043;
                public const int JavascriptCode = 473330044;
                public const int As2Decode = 473330045;
                public const int As2Encode = 473330046;
                public const int RosettaNetEncode = 473330047;
                public const int RosettaNetDecode = 473330048;
                public const int RosettaNetWaitForResponse = 473330049;
                public const int ApiConnectionNotification = 473330050;
                public const int Changeset = 473330051;
                public const int SwiftEncode = 473330052;
            }
            public struct StageCategory
            {
                public const int Qualify = 0;
                public const int Develop = 1;
                public const int Propose = 2;
                public const int Close = 3;
                public const int Identify = 4;
                public const int Research = 5;
                public const int Resolve = 6;
                public const int Approval = 7;
            }
        }
        #endregion

        #region LogicalNames
        public static partial class LogicalNames
        {
            public const string ProcessStageId = "processstageid";
            public const string ClientData = "clientdata";
            public const string Connector = "connector";
            public const string IsTrigger = "istrigger";
            public const string OperationId = "operationid";
            public const string OperationKind = "operationkind";
            public const string OperationType = "operationtype";
            public const string OwnerId = "ownerid";
            public const string OwningBusinessUnit = "owningbusinessunit";
            public const string ParameterName = "parametername";
            public const string ParameterValue = "parametervalue";
            public const string ParentProcessStageId = "parentprocessstageid";
            public const string PrimaryEntityTypeCode = "primaryentitytypecode";
            public const string ProcessId = "processid";
            public const string StageCategory = "stagecategory";
            public const string StageName = "stagename";
            public const string VersionNumber = "versionnumber";
        }
        #endregion

        #region Relations
        public static partial class Relations
        {
            public static class OneToMany
            {
                public const string LkExpiredprocessActivestageid = "lk_expiredprocess_activestageid";
                public const string LkNewprocessActivestageid = "lk_newprocess_activestageid";
                public const string LkTranslationprocessActivestageid = "lk_translationprocess_activestageid";
                public const string ProcessstageAccount = "processstage_account";
                public const string ProcessstageAdxPortalcomment = "processstage_adx_portalcomment";
                public const string ProcessstageAppointments = "processstage_appointments";
                public const string ProcessstageContact = "processstage_contact";
                public const string ProcessstageEmails = "processstage_emails";
                public const string ProcessstageFaxes = "processstage_faxes";
                public const string ProcessstageKnowledgearticle = "processstage_knowledgearticle";
                public const string ProcessstageLetters = "processstage_letters";
                public const string ProcessstageParentprocessstage = "processstage_parentprocessstage";
                public const string ProcessstagePhonecalls = "processstage_phonecalls";
                public const string ProcessstageProcessstageparameter = "processstage_processstageparameter";
                public const string ProcessstageRecurringappointmentmasters = "processstage_recurringappointmentmasters";
                public const string ProcessStageSyncErrors = "ProcessStage_SyncErrors";
                public const string ProcessstageSystemusers = "processstage_systemusers";
                public const string ProcessstageTasks = "processstage_tasks";
                public const string ProcessstageTeams = "processstage_teams";
            }

            public static partial class ManyToOne
            {
                public const string ProcessProcessstage = "process_processstage";
                public const string ProcessstageParentprocessstage = "processstage_parentprocessstage";
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
        #endregion
    }

    #region Context
    public partial class DataContext
    {
        public IQueryable<ProcessStage> ProcessStageSet
        {
            get
            {
                return CreateQuery<ProcessStage>();
            }
        }
    }
    #endregion
}
