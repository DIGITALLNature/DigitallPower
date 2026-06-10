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
	/// Process whose execution can proceed independently or in the background.
	/// </summary>
    [DataContract]
    [EntityLogicalName("asyncoperation")]
    [System.CodeDom.Compiler.GeneratedCode("dgtp", "2026")]
    [ExcludeFromCodeCoverage]
    public partial class AsyncOperation : Entity, INotifyPropertyChanging, INotifyPropertyChanged
    {
        #region ctor
        [DebuggerNonUserCode]
        public AsyncOperation() : this(false)
        {
        }
        [DebuggerNonUserCode]
        public AsyncOperation(bool trackChanges = false) : base(EntityLogicalName)
        {
            _trackChanges = trackChanges;
        }
        [DebuggerNonUserCode]
        public AsyncOperation(Guid id, bool trackChanges = false) : base(EntityLogicalName, id)
        {
            _trackChanges = trackChanges;
        }
        [DebuggerNonUserCode]
        public AsyncOperation(KeyAttributeCollection keyAttributes, bool trackChanges = false) : base(EntityLogicalName, keyAttributes)
        {
            _trackChanges = trackChanges;
        }
        [DebuggerNonUserCode]
        public AsyncOperation(string keyName, object keyValue, bool trackChanges = false) : base(EntityLogicalName, keyName, keyValue)
        {
            _trackChanges = trackChanges;
        }
        #endregion

        #region fields
        private readonly bool _trackChanges;
        private readonly Lazy<HashSet<string>> _changedProperties = new();
        #endregion

        #region consts
        public const string EntityLogicalName = "asyncoperation";
        public const string PrimaryNameAttribute = "name";
        public const int EntityTypeCode = 4700;
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
        [AttributeLogicalName("asyncoperationid")]
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
                AsyncOperationId = value;
            }
        }

        /// <summary>
		/// Unique identifier of the system job.
		/// </summary>
        [AttributeLogicalName("asyncoperationid")]
        public Guid? AsyncOperationId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<Guid?>("asyncoperationid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("asyncoperationid", value);
                base.Id = value.HasValue ? value.Value : Guid.Empty;
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// The breadcrumb record ID.
		/// </summary>
        [AttributeLogicalName("breadcrumbid")]
        public Guid? BreadcrumbId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<Guid?>("breadcrumbid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("breadcrumbid", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// The origin of the caller.
		/// </summary>
        [AttributeLogicalName("callerorigin")]
        public string? CallerOrigin
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("callerorigin");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("callerorigin", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Date and time when the system job was completed.
		/// </summary>
        [AttributeLogicalName("completedon")]
        public DateTime? CompletedOn
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<DateTime?>("completedon");
            }
        }

        /// <summary>
		/// Unique identifier used to correlate between multiple SDK requests and system jobs.
		/// </summary>
        [AttributeLogicalName("correlationid")]
        public Guid? CorrelationId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<Guid?>("correlationid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("correlationid", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Last time the correlation depth was updated.
		/// </summary>
        [AttributeLogicalName("correlationupdatedtime")]
        public DateTime? CorrelationUpdatedTime
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<DateTime?>("correlationupdatedtime");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("correlationupdatedtime", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier of the user who created the system job.
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
		/// Date and time when the system job was created.
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
		/// Unique identifier of the delegate user who created the asyncoperation.
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
		/// Unstructured data associated with the system job.
		/// </summary>
        [AttributeLogicalName("data")]
        public string? Data
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("data");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("data", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// File Id for the blob url used for file storage.
		/// </summary>
        [AttributeLogicalName("datablobid")]
        public Guid? DataBlobId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<Guid?>("datablobid");
            }
        }

        
        [AttributeLogicalName("datablobid_name")]
        public string? DataBlobIdName
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("datablobid_name");
            }
        }

        /// <summary>
		/// Execution of all operations with the same dependency token is serialized.
		/// </summary>
        [AttributeLogicalName("dependencytoken")]
        public string? DependencyToken
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("dependencytoken");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("dependencytoken", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Number of SDK calls made since the first call.
		/// </summary>
        [AttributeLogicalName("depth")]
        public int? Depth
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("depth");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("depth", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Error code returned from a canceled system job.
		/// </summary>
        [AttributeLogicalName("errorcode")]
        public int? ErrorCode
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("errorcode");
            }
        }

        /// <summary>
		/// Time that the system job has taken to execute.
		/// </summary>
        [AttributeLogicalName("executiontimespan")]
        public double? ExecutionTimeSpan
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<double?>("executiontimespan");
            }
        }

        /// <summary>
		/// The datetime when the Expander pipeline started.
		/// </summary>
        [AttributeLogicalName("expanderstarttime")]
        public DateTime? ExpanderStartTime
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<DateTime?>("expanderstarttime");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("expanderstarttime", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Message provided by the system job.
		/// </summary>
        [AttributeLogicalName("friendlymessage")]
        public string? FriendlyMessage
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("friendlymessage");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("friendlymessage", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier of the host that owns this system job.
		/// </summary>
        [AttributeLogicalName("hostid")]
        public string? HostId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("hostid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("hostid", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates that the system job is waiting for an event.
		/// </summary>
        [AttributeLogicalName("iswaitingforevent")]
        public bool? IsWaitingForEvent
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("iswaitingforevent");
            }
        }

        /// <summary>
		/// Message related to the system job.
		/// </summary>
        [AttributeLogicalName("message")]
        public string? Message
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("message");
            }
        }

        /// <summary>
		/// Name of the message that started this system job.
		/// </summary>
        [AttributeLogicalName("messagename")]
        public string? MessageName
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("messagename");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("messagename", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier of the user who last modified the system job.
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
		/// Date and time when the system job was last modified.
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
		/// Unique identifier of the delegate user who last modified the asyncoperation.
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
		/// Name of the system job.
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
		/// Type of the system job.
		/// </summary>
        [AttributeLogicalName("operationtype")]
        public OptionSetValue? OperationType
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("operationtype");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("operationtype", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier of the user or team who owns the system job.
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
		/// Unique identifier of the business unit that owns the system job.
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
		/// Unique identifier of the owning extension with which the system job is associated.
		/// </summary>
        [AttributeLogicalName("owningextensionid")]
        public EntityReference? OwningExtensionId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<EntityReference?>("owningextensionid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("owningextensionid", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier of the team who owns the record.
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
		/// Unique identifier of the user who owns the record.
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

        
        [AttributeLogicalName("parentpluginexecutionid")]
        public Guid? ParentPluginExecutionId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<Guid?>("parentpluginexecutionid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("parentpluginexecutionid", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether the system job should run only after the specified date and time.
		/// </summary>
        [AttributeLogicalName("postponeuntil")]
        public DateTime? PostponeUntil
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<DateTime?>("postponeuntil");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("postponeuntil", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Type of entity with which the system job is primarily associated.
		/// </summary>
        [AttributeLogicalName("primaryentitytype")]
        public string? PrimaryEntityType
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("primaryentitytype");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("primaryentitytype", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Pattern of the system job's recurrence.
		/// </summary>
        [AttributeLogicalName("recurrencepattern")]
        public string? RecurrencePattern
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("recurrencepattern");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("recurrencepattern", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Starting time in UTC for the recurrence pattern.
		/// </summary>
        [AttributeLogicalName("recurrencestarttime")]
        public DateTime? RecurrenceStartTime
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<DateTime?>("recurrencestarttime");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("recurrencestarttime", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier of the object with which the system job is associated.
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
		/// Unique identifier of the request that generated the system job.
		/// </summary>
        [AttributeLogicalName("requestid")]
        public Guid? RequestId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<Guid?>("requestid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("requestid", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Retain job history.
		/// </summary>
        [AttributeLogicalName("retainjobhistory")]
        public bool? RetainJobHistory
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("retainjobhistory");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("retainjobhistory", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Number of times to retry the system job.
		/// </summary>
        [AttributeLogicalName("retrycount")]
        public int? RetryCount
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("retrycount");
            }
        }

        /// <summary>
		/// Root execution context of the job that trigerred async job.
		/// </summary>
        [AttributeLogicalName("rootexecutioncontext")]
        public string? RootExecutionContext
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("rootexecutioncontext");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("rootexecutioncontext", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Order in which operations were submitted.
		/// </summary>
        [AttributeLogicalName("sequence")]
        public long? Sequence
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<long?>("sequence");
            }
        }

        /// <summary>
		/// Date and time when the system job was started.
		/// </summary>
        [AttributeLogicalName("startedon")]
        public DateTime? StartedOn
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<DateTime?>("startedon");
            }
        }

        /// <summary>
		/// Status of the system job.
		/// </summary>
        [AttributeLogicalName("statecode")]
        public OptionSetValue? StateCode
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
		/// Reason for the status of the system job.
		/// </summary>
        [AttributeLogicalName("statuscode")]
        public OptionSetValue? StatusCode
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
		/// The Subtype of the Async Job
		/// </summary>
        [AttributeLogicalName("subtype")]
        public int? Subtype
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("subtype");
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
		/// Unique identifier of the workflow activation related to the system job.
		/// </summary>
        [AttributeLogicalName("workflowactivationid")]
        public EntityReference? WorkflowActivationId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<EntityReference?>("workflowactivationid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("workflowactivationid", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Name of a workflow stage.
		/// </summary>
        [AttributeLogicalName("workflowstagename")]
        public string? WorkflowStageName
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("workflowstagename");
            }
        }

        /// <summary>
		/// The workload name.
		/// </summary>
        [AttributeLogicalName("workload")]
        public string? Workload
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("workload");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("workload", value);
                OnPropertyChanged();
            }
        }
        #endregion

        #region NavigationProperties
        #endregion

        #region Options
        public static partial class Options
        {
            public struct IsWaitingForEvent
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct OperationType
            {
                public const int SystemEvent = 1;
                public const int BulkEmail = 2;
                public const int ImportFileParse = 3;
                public const int TransformParseData = 4;
                public const int Import = 5;
                public const int ActivityPropagation = 6;
                public const int DuplicateDetectionRulePublish = 7;
                public const int BulkDuplicateDetection = 8;
                public const int SQMDataCollection = 9;
                public const int Workflow = 10;
                public const int QuickCampaign = 11;
                public const int MatchcodeUpdate = 12;
                public const int BulkDelete = 13;
                public const int DeletionService = 14;
                public const int IndexManagement = 15;
                public const int CollectOrganizationStatistics = 16;
                public const int ImportSubprocess = 17;
                public const int CalculateOrganizationStorageSize = 18;
                public const int CollectOrganizationDatabaseStatistics = 19;
                public const int CollectionOrganizationSizeStatistics = 20;
                public const int DatabaseTuning = 21;
                public const int CalculateOrganizationMaximumStorageSize = 22;
                public const int BulkDeleteSubprocess = 23;
                public const int UpdateStatisticIntervals = 24;
                public const int OrganizationFullTextCatalogIndex = 25;
                public const int DatabaseLogBackup = 26;
                public const int UpdateContractStates = 27;
                public const int DBCCSHRINKDATABASEMaintenanceJob = 28;
                public const int DBCCSHRINKFILEMaintenanceJob = 29;
                public const int ReindexAllIndicesMaintenanceJob = 30;
                public const int StorageLimitNotification = 31;
                public const int CleanupInactiveWorkflowAssemblies = 32;
                public const int RecurringSeriesExpansion = 35;
                public const int ImportSampleData = 38;
                public const int GoalRollUp = 40;
                public const int AuditPartitionCreation = 41;
                public const int CheckForLanguagePackUpdates = 42;
                public const int ProvisionLanguagePack = 43;
                public const int UpdateOrganizationDatabase = 44;
                public const int UpdateSolution = 45;
                public const int RegenerateEntityRowCountSnapshotData = 46;
                public const int RegenerateReadShareSnapshotData = 47;
                public const int PostToYammer = 49;
                public const int OutgoingActivity = 50;
                public const int IncomingEmailProcessing = 51;
                public const int MailboxTestAccess = 52;
                public const int EncryptionHealthCheck = 53;
                public const int ExecuteAsyncRequest = 54;
                public const int UpdateEntitlementStates = 56;
                public const int CalculateRollupField = 57;
                public const int MassCalculateRollupField = 58;
                public const int ImportTranslation = 59;
                public const int ConvertDateAndTimeBehavior = 62;
                public const int EntityKeyIndexCreation = 63;
                public const int UpdateKnowledgeArticleStates = 65;
                public const int ResourceBookingSync = 68;
                public const int RelationshipAssistantCards = 69;
                public const int CleanupSolutionComponents = 71;
                public const int AppModuleMetadataOperation = 72;
                public const int ALMAnomalyDetectionOperation = 73;
                public const int FlowNotification = 75;
                public const int RibbonClientMetadataOperation = 76;
                public const int CallbackRegistrationExpanderOperation = 79;
                public const int MigrateNotesToAttachmentsJob = 85;
                public const int MigrateArticleContentToFileStorage = 86;
                public const int UpdatedDeactivedOnForResolvedCasesJob = 87;
                public const int CascadeReparentDBAsyncOperation = 88;
                public const int CascadeMergeAsyncOperation = 89;
                public const int CascadeAssign = 90;
                public const int CascadeDelete = 91;
                public const int EventExpanderOperation = 92;
                public const int ImportSolutionMetadata = 93;
                public const int BulkDeleteFileAttachment = 94;
                public const int RefreshBusinessUnitForRecordsOwnedByPrincipal = 95;
                public const int RevokeInheritedAccess = 96;
                public const int CreateOrRefreshVirtualEntity = 98;
                public const int CascadeFlowSessionPermissionsAsyncOperation = 100;
                public const int UpdateModernFlowAsyncOperation = 101;
                public const int AsyncArchiveAsyncOperation = 102;
                public const int CancelAsyncOperationsSystem = 103;
                public const int ProcessTableForRecycleBin = 104;
                public const int CascadeAssignAllAsyncOperation = 105;
                public const int BackgroundTeamServiceAsyncOperation = 106;
                public const int AsyncRestoreJob = 187;
                public const int ProvisionLanguageForUser = 201;
                public const int ExportSolutionAsyncOperation = 202;
                public const int ImportSolutionAsyncOperation = 203;
                public const int PublishAllAsyncOperation = 204;
                public const int DeleteAndPromoteAsyncOperation = 207;
                public const int UninstallSolutionAsyncOperation = 208;
                public const int ProvisionLanguageAsyncOperation = 209;
                public const int ImportTranslationAsyncOperation = 210;
                public const int StageAndUpgradeAsyncOperation = 211;
                public const int DenormalizationAsyncOperation = 239;
                public const int RefreshRuntimeIntegrationComponentsAsyncOperation = 250;
                public const int BulkArchiveOperation = 300;
                public const int ArchiveExecutionAsyncOperation = 301;
                public const int FinOpsDeploymentAsyncOperation = 302;
                public const int PurgeArchivedContentOperation = 304;
                public const int RegisterOfferingAsyncOperation = 305;
                public const int ExecuteDataProcessingConfiguration = 306;
                public const int SyncSynapseTablesSchema = 307;
                public const int FinOpsDBSyncAsyncOperation = 308;
                public const int FinOpsUnitTestAsyncOperation = 309;
                public const int CatalogServiceGeneratePackageAsyncOperation = 320;
                public const int CatalogServiceSubmitApprovalRequestAsyncOperation = 321;
                public const int CatalogServiceInstallRequestAsyncOperation = 322;
                public const int TDSEndpointProvisioningNewTVFFunctionsAndGrantPermissionAsyncOperation = 330;
                public const int FinOpsDeployCustomPackageAsyncOperation = 332;
                public const int DeletesRelatedElasticTableRecordsWhenASQLRecordIsDeleted = 333;
                public const int DeletesRelatedElasticOrSQLTableRecordsWhenAnElasticTableRecordIsDeleted = 334;
                public const int CatalogServiceAsycOperationToPollForASolutionCheckerRequest = 335;
                public const int CatalogServiceAsycOperationToSubmitASolutionCheckerRequest = 336;
                public const int SolutionServiceAsyncOperationToInstallSolutionAfterAppUpdates = 337;
                public const int PromptColumnBulkUpdateOperation = 338;
                public const int InstantEntitiesCleanupOperation = 339;
                public const int PowerPagesRefreshEAuthZDataOperation = 340;
                public const int PollPackageDeploymentStatus = 341;
                public const int ServiceAppUpdate = 342;
                public const int ServiceDbUpdate = 343;
                public const int ApplyReplicatedRecordsToClusterPartition = 344;
                public const int CascadeGrantOrRevokeAccessVersionTrackingAsyncOperation = 12801;
                public const int AIBuilderTrainingEvents = 190690091;
                public const int AIBuilderPredictionEvents = 190690092;
            }
            public struct RetainJobHistory
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct StateCode
            {
                public const int Ready = 0;
                public const int Suspended = 1;
                public const int Locked = 2;
                public const int Completed = 3;
            }
            public struct StatusCode
            {
                public const int WaitingForResources = 0;
                public const int Waiting = 10;
                public const int InProgress = 20;
                public const int Pausing = 21;
                public const int Canceling = 22;
                public const int Succeeded = 30;
                public const int Failed = 31;
                public const int Canceled = 32;
            }
        }
        #endregion

        #region LogicalNames
        public static partial class LogicalNames
        {
            public const string AsyncOperationId = "asyncoperationid";
            public const string BreadcrumbId = "breadcrumbid";
            public const string CallerOrigin = "callerorigin";
            public const string CompletedOn = "completedon";
            public const string CorrelationId = "correlationid";
            public const string CorrelationUpdatedTime = "correlationupdatedtime";
            public const string CreatedBy = "createdby";
            public const string CreatedOn = "createdon";
            public const string CreatedOnBehalfBy = "createdonbehalfby";
            public const string Data = "data";
            public const string DataBlobId = "datablobid";
            public const string DataBlobIdName = "datablobid_name";
            public const string DependencyToken = "dependencytoken";
            public const string Depth = "depth";
            public const string ErrorCode = "errorcode";
            public const string ExecutionTimeSpan = "executiontimespan";
            public const string ExpanderStartTime = "expanderstarttime";
            public const string FriendlyMessage = "friendlymessage";
            public const string HostId = "hostid";
            public const string IsWaitingForEvent = "iswaitingforevent";
            public const string Message = "message";
            public const string MessageName = "messagename";
            public const string ModifiedBy = "modifiedby";
            public const string ModifiedOn = "modifiedon";
            public const string ModifiedOnBehalfBy = "modifiedonbehalfby";
            public const string Name = "name";
            public const string OperationType = "operationtype";
            public const string OwnerId = "ownerid";
            public const string OwningBusinessUnit = "owningbusinessunit";
            public const string OwningExtensionId = "owningextensionid";
            public const string OwningTeam = "owningteam";
            public const string OwningUser = "owninguser";
            public const string ParentPluginExecutionId = "parentpluginexecutionid";
            public const string PostponeUntil = "postponeuntil";
            public const string PrimaryEntityType = "primaryentitytype";
            public const string RecurrencePattern = "recurrencepattern";
            public const string RecurrenceStartTime = "recurrencestarttime";
            public const string RegardingObjectId = "regardingobjectid";
            public const string RequestId = "requestid";
            public const string RetainJobHistory = "retainjobhistory";
            public const string RetryCount = "retrycount";
            public const string RootExecutionContext = "rootexecutioncontext";
            public const string Sequence = "sequence";
            public const string StartedOn = "startedon";
            public const string StateCode = "statecode";
            public const string StatusCode = "statuscode";
            public const string Subtype = "subtype";
            public const string TimeZoneRuleVersionNumber = "timezoneruleversionnumber";
            public const string UTCConversionTimeZoneCode = "utcconversiontimezonecode";
            public const string WorkflowActivationId = "workflowactivationid";
            public const string WorkflowStageName = "workflowstagename";
            public const string Workload = "workload";
        }
        #endregion

        #region Relations
        public static partial class Relations
        {
            public static class OneToMany
            {
                public const string AsyncOperationBulkDeleteOperation = "AsyncOperation_BulkDeleteOperation";
                public const string AsyncOperationDeletedItemReferenceRegardingObjectId = "AsyncOperation_DeletedItemReference_RegardingObjectId";
                public const string AsyncOperationDuplicateBaseRecord = "AsyncOperation_DuplicateBaseRecord";
                public const string AsyncOperationEmails = "AsyncOperation_Emails";
                public const string AsyncoperationFileAttachments = "asyncoperation_FileAttachments";
                public const string AsyncOperationMailboxTrackingFolder = "AsyncOperation_MailboxTrackingFolder";
                public const string AsyncOperationSocialActivities = "AsyncOperation_SocialActivities";
                public const string AsyncoperationWorkflowwaitsubscription = "asyncoperation_workflowwaitsubscription";
                public const string LkWorkflowlogAsyncoperationChildworkflow = "lk_workflowlog_asyncoperation_childworkflow";
                public const string LkWorkflowlogAsyncoperations = "lk_workflowlog_asyncoperations";
                public const string UserentityinstancedataAsyncoperation = "userentityinstancedata_asyncoperation";
            }

            public static partial class ManyToOne
            {
                public const string AccountAsyncOperations = "Account_AsyncOperations";
                public const string ActivityfileattachmentAsyncOperations = "activityfileattachment_AsyncOperations";
                public const string ActivityMimeAttachmentAsyncOperations = "ActivityMimeAttachment_AsyncOperations";
                public const string ActivityPointerAsyncOperations = "ActivityPointer_AsyncOperations";
                public const string AdxExternalidentityAsyncOperations = "adx_externalidentity_AsyncOperations";
                public const string AdxInvitationAsyncOperations = "adx_invitation_AsyncOperations";
                public const string AdxInviteredemptionAsyncOperations = "adx_inviteredemption_AsyncOperations";
                public const string AdxPortalcommentAsyncOperations = "adx_portalcomment_AsyncOperations";
                public const string AdxSettingAsyncOperations = "adx_setting_AsyncOperations";
                public const string AdxWebformsessionAsyncOperations = "adx_webformsession_AsyncOperations";
                public const string AgentconversationmessageAsyncOperations = "agentconversationmessage_AsyncOperations";
                public const string AgentconversationmessagefileAsyncOperations = "agentconversationmessagefile_AsyncOperations";
                public const string AgentfeeditemAsyncOperations = "agentfeeditem_AsyncOperations";
                public const string AgenthubgoalAsyncOperations = "agenthubgoal_AsyncOperations";
                public const string AgenthubinsightAsyncOperations = "agenthubinsight_AsyncOperations";
                public const string AgenthubmetricAsyncOperations = "agenthubmetric_AsyncOperations";
                public const string AgentruleAsyncOperations = "agentrule_AsyncOperations";
                public const string AicopilotAsyncOperations = "aicopilot_AsyncOperations";
                public const string AiinsightcardAsyncOperations = "aiinsightcard_AsyncOperations";
                public const string AipluginAsyncOperations = "aiplugin_AsyncOperations";
                public const string AipluginauthAsyncOperations = "aipluginauth_AsyncOperations";
                public const string AipluginconversationstarterAsyncOperations = "aipluginconversationstarter_AsyncOperations";
                public const string AipluginconversationstartermappingAsyncOperations = "aipluginconversationstartermapping_AsyncOperations";
                public const string AipluginexternalschemaAsyncOperations = "aipluginexternalschema_AsyncOperations";
                public const string AipluginexternalschemapropertyAsyncOperations = "aipluginexternalschemaproperty_AsyncOperations";
                public const string AiplugingovernanceAsyncOperations = "aiplugingovernance_AsyncOperations";
                public const string AiplugingovernanceextAsyncOperations = "aiplugingovernanceext_AsyncOperations";
                public const string AiplugininstanceAsyncOperations = "aiplugininstance_AsyncOperations";
                public const string AipluginoperationAsyncOperations = "aipluginoperation_AsyncOperations";
                public const string AipluginoperationparameterAsyncOperations = "aipluginoperationparameter_AsyncOperations";
                public const string AipluginoperationresponsetemplateAsyncOperations = "aipluginoperationresponsetemplate_AsyncOperations";
                public const string AiplugintitleAsyncOperations = "aiplugintitle_AsyncOperations";
                public const string AipluginusersettingAsyncOperations = "aipluginusersetting_AsyncOperations";
                public const string AiskillconfigAsyncOperations = "aiskillconfig_AsyncOperations";
                public const string AllowedmcpclientAsyncOperations = "allowedmcpclient_AsyncOperations";
                public const string AnnotationAsyncOperations = "Annotation_AsyncOperations";
                public const string AnnualFiscalCalendarAsyncOperations = "AnnualFiscalCalendar_AsyncOperations";
                public const string AnyprivilegeentityAsyncOperations = "anyprivilegeentity_AsyncOperations";
                public const string AppactionAsyncOperations = "appaction_AsyncOperations";
                public const string AppactionmigrationAsyncOperations = "appactionmigration_AsyncOperations";
                public const string AppactionruleAsyncOperations = "appactionrule_AsyncOperations";
                public const string AppelementAsyncOperations = "appelement_AsyncOperations";
                public const string AppentitysearchviewAsyncOperations = "appentitysearchview_AsyncOperations";
                public const string ApplicationAsyncOperations = "application_AsyncOperations";
                public const string ApplicationuserAsyncOperations = "applicationuser_AsyncOperations";
                public const string AppmodulecomponentedgeAsyncOperations = "appmodulecomponentedge_AsyncOperations";
                public const string AppmodulecomponentnodeAsyncOperations = "appmodulecomponentnode_AsyncOperations";
                public const string AppointmentAsyncOperations = "Appointment_AsyncOperations";
                public const string ApprovalprocessAsyncOperations = "approvalprocess_AsyncOperations";
                public const string ApprovalstageapprovalAsyncOperations = "approvalstageapproval_AsyncOperations";
                public const string ApprovalstageconditionAsyncOperations = "approvalstagecondition_AsyncOperations";
                public const string ApprovalstageintelligentAsyncOperations = "approvalstageintelligent_AsyncOperations";
                public const string ApprovalstageorderAsyncOperations = "approvalstageorder_AsyncOperations";
                public const string AppsettingAsyncOperations = "appsetting_AsyncOperations";
                public const string AppusersettingAsyncOperations = "appusersetting_AsyncOperations";
                public const string ArchivecleanupinfoAsyncOperations = "archivecleanupinfo_AsyncOperations";
                public const string ArchivecleanupoperationAsyncOperations = "archivecleanupoperation_AsyncOperations";
                public const string AthenareconciliationinfoAsyncOperations = "athenareconciliationinfo_AsyncOperations";
                public const string AttributeclusterconfigAsyncOperations = "attributeclusterconfig_AsyncOperations";
                public const string AttributeimageconfigAsyncOperations = "attributeimageconfig_AsyncOperations";
                public const string AttributeMapAsyncOperations = "AttributeMap_AsyncOperations";
                public const string AttributemaskingruleAsyncOperations = "attributemaskingrule_AsyncOperations";
                public const string AttributepicklistvalueAsyncOperations = "attributepicklistvalue_AsyncOperations";
                public const string BotAsyncOperations = "bot_AsyncOperations";
                public const string BotcomponentAsyncOperations = "botcomponent_AsyncOperations";
                public const string BotcomponentcollectionAsyncOperations = "botcomponentcollection_AsyncOperations";
                public const string BulkarchiveconfigAsyncOperations = "bulkarchiveconfig_AsyncOperations";
                public const string BulkarchivefailuredetailAsyncOperations = "bulkarchivefailuredetail_AsyncOperations";
                public const string BulkarchiveoperationAsyncOperations = "bulkarchiveoperation_AsyncOperations";
                public const string BulkarchiveoperationdetailAsyncOperations = "bulkarchiveoperationdetail_AsyncOperations";
                public const string BusinessUnitAsyncoperation = "business_unit_asyncoperation";
                public const string BusinessprocessAsyncOperations = "businessprocess_AsyncOperations";
                public const string BusinessUnitAsyncOperations = "BusinessUnit_AsyncOperations";
                public const string BusinessUnitNewsArticleAsyncOperations = "BusinessUnitNewsArticle_AsyncOperations";
                public const string CalendarAsyncOperations = "Calendar_AsyncOperations";
                public const string CanvasappextendedmetadataAsyncOperations = "canvasappextendedmetadata_AsyncOperations";
                public const string CardAsyncOperations = "card_AsyncOperations";
                public const string CascadegrantrevokeaccessrecordstrackerAsyncOperations = "cascadegrantrevokeaccessrecordstracker_AsyncOperations";
                public const string CascadegrantrevokeaccessversiontrackerAsyncOperations = "cascadegrantrevokeaccessversiontracker_AsyncOperations";
                public const string CatalogAsyncOperations = "catalog_AsyncOperations";
                public const string CatalogassignmentAsyncOperations = "catalogassignment_AsyncOperations";
                public const string CertificatecredentialAsyncOperations = "certificatecredential_AsyncOperations";
                public const string ChannelaccessprofileAsyncOperations = "channelaccessprofile_AsyncOperations";
                public const string ChatAsyncOperations = "chat_AsyncOperations";
                public const string CommentAsyncOperations = "comment_AsyncOperations";
                public const string ComputeruseagentAsyncOperations = "computeruseagent_AsyncOperations";
                public const string ConnectionAsyncOperations = "Connection_AsyncOperations";
                public const string ConnectionRoleAsyncOperations = "Connection_Role_AsyncOperations";
                public const string ConnectioninstanceAsyncOperations = "connectioninstance_AsyncOperations";
                public const string ConnectionreferenceAsyncOperations = "connectionreference_AsyncOperations";
                public const string ConnectorAsyncOperations = "connector_AsyncOperations";
                public const string ContactAsyncOperations = "Contact_AsyncOperations";
                public const string ConversationtranscriptAsyncOperations = "conversationtranscript_AsyncOperations";
                public const string ConvertruleAsyncOperations = "Convertrule_AsyncOperations";
                public const string CopilotexamplequestionAsyncOperations = "copilotexamplequestion_AsyncOperations";
                public const string CopilotglossarytermAsyncOperations = "copilotglossaryterm_AsyncOperations";
                public const string CopilotsynonymsAsyncOperations = "copilotsynonyms_AsyncOperations";
                public const string CredentialAsyncOperations = "credential_AsyncOperations";
                public const string CustomapiAsyncOperations = "customapi_AsyncOperations";
                public const string CustomapirequestparameterAsyncOperations = "customapirequestparameter_AsyncOperations";
                public const string CustomapiresponsepropertyAsyncOperations = "customapiresponseproperty_AsyncOperations";
                public const string CustomerAddressAsyncOperations = "CustomerAddress_AsyncOperations";
                public const string CustomerRelationshipAsyncOperations = "CustomerRelationship_AsyncOperations";
                public const string DatalakefolderAsyncOperations = "datalakefolder_AsyncOperations";
                public const string DatalakefolderpermissionAsyncOperations = "datalakefolderpermission_AsyncOperations";
                public const string DatalakeworkspaceAsyncOperations = "datalakeworkspace_AsyncOperations";
                public const string DatalakeworkspacepermissionAsyncOperations = "datalakeworkspacepermission_AsyncOperations";
                public const string DataprocessingconfigurationAsyncOperations = "dataprocessingconfiguration_AsyncOperations";
                public const string DelegatedauthorizationAsyncOperations = "delegatedauthorization_AsyncOperations";
                public const string DeleteditemreferenceAsyncOperations = "deleteditemreference_AsyncOperations";
                public const string DesktopflowbinaryAsyncOperations = "desktopflowbinary_AsyncOperations";
                public const string DesktopflowmoduleAsyncOperations = "desktopflowmodule_AsyncOperations";
                public const string DisplayStringAsyncOperations = "DisplayString_AsyncOperations";
                public const string DvfilesearchAsyncOperations = "dvfilesearch_AsyncOperations";
                public const string DvfilesearchattributeAsyncOperations = "dvfilesearchattribute_AsyncOperations";
                public const string DvfilesearchentityAsyncOperations = "dvfilesearchentity_AsyncOperations";
                public const string DvtablesearchAsyncOperations = "dvtablesearch_AsyncOperations";
                public const string DvtablesearchattributeAsyncOperations = "dvtablesearchattribute_AsyncOperations";
                public const string DvtablesearchentityAsyncOperations = "dvtablesearchentity_AsyncOperations";
                public const string EmailAsyncOperations = "Email_AsyncOperations";
                public const string EmailaddressconfigurationAsyncOperations = "emailaddressconfiguration_AsyncOperations";
                public const string EmailserverprofileAsyncoperations = "emailserverprofile_asyncoperations";
                public const string EnablearchivalrequestAsyncOperations = "enablearchivalrequest_AsyncOperations";
                public const string EntityanalyticsconfigAsyncOperations = "entityanalyticsconfig_AsyncOperations";
                public const string EntityclusterconfigAsyncOperations = "entityclusterconfig_AsyncOperations";
                public const string EntityimageconfigAsyncOperations = "entityimageconfig_AsyncOperations";
                public const string EntityindexAsyncOperations = "entityindex_AsyncOperations";
                public const string EntityMapAsyncOperations = "EntityMap_AsyncOperations";
                public const string EntityrecordfilterAsyncOperations = "entityrecordfilter_AsyncOperations";
                public const string EnvironmentvariabledefinitionAsyncOperations = "environmentvariabledefinition_AsyncOperations";
                public const string EnvironmentvariablevalueAsyncOperations = "environmentvariablevalue_AsyncOperations";
                public const string ExportedexcelAsyncOperations = "exportedexcel_AsyncOperations";
                public const string ExportsolutionuploadAsyncOperations = "exportsolutionupload_AsyncOperations";
                public const string ExternalpartyAsyncOperations = "externalparty_AsyncOperations";
                public const string ExternalpartyitemAsyncOperations = "externalpartyitem_AsyncOperations";
                public const string FabricaiskillAsyncOperations = "fabricaiskill_AsyncOperations";
                public const string FaxAsyncOperations = "Fax_AsyncOperations";
                public const string FeaturecontrolsettingAsyncOperations = "featurecontrolsetting_AsyncOperations";
                public const string FederatedknowledgecitationAsyncOperations = "federatedknowledgecitation_AsyncOperations";
                public const string FederatedknowledgeconfigurationAsyncOperations = "federatedknowledgeconfiguration_AsyncOperations";
                public const string FederatedknowledgeentityconfigurationAsyncOperations = "federatedknowledgeentityconfiguration_AsyncOperations";
                public const string FederatedknowledgemetadatarefreshAsyncOperations = "federatedknowledgemetadatarefresh_AsyncOperations";
                public const string FileAttachmentAsyncOperationDataBlobId = "FileAttachment_AsyncOperation_DataBlobId";
                public const string FixedMonthlyFiscalCalendarAsyncOperations = "FixedMonthlyFiscalCalendar_AsyncOperations";
                public const string FlowcapacityassignmentAsyncOperations = "flowcapacityassignment_AsyncOperations";
                public const string FlowcredentialapplicationAsyncOperations = "flowcredentialapplication_AsyncOperations";
                public const string FloweventAsyncOperations = "flowevent_AsyncOperations";
                public const string FlowgroupAsyncOperations = "flowgroup_AsyncOperations";
                public const string FlowmachineAsyncOperations = "flowmachine_AsyncOperations";
                public const string FlowmachinegroupAsyncOperations = "flowmachinegroup_AsyncOperations";
                public const string FlowmachineimageAsyncOperations = "flowmachineimage_AsyncOperations";
                public const string FlowmachineimageversionAsyncOperations = "flowmachineimageversion_AsyncOperations";
                public const string FlowmachinenetworkAsyncOperations = "flowmachinenetwork_AsyncOperations";
                public const string FlowsessionAsyncOperations = "flowsession_AsyncOperations";
                public const string FlowsessionbinaryAsyncOperations = "flowsessionbinary_AsyncOperations";
                public const string FlowtestsessionAsyncOperations = "flowtestsession_AsyncOperations";
                public const string FlowtriggerAsyncOperations = "flowtrigger_AsyncOperations";
                public const string FlowtriggerinstanceAsyncOperations = "flowtriggerinstance_AsyncOperations";
                public const string FxexpressionAsyncOperations = "fxexpression_AsyncOperations";
                public const string GithubappconfigAsyncOperations = "githubappconfig_AsyncOperations";
                public const string GoalAsyncOperations = "Goal_AsyncOperations";
                public const string GoalrollupqueryAsyncOperations = "goalrollupquery_AsyncOperations";
                public const string GovernanceconfigurationAsyncOperations = "governanceconfiguration_AsyncOperations";
                public const string HolidaywrapperAsyncOperations = "holidaywrapper_AsyncOperations";
                public const string ImportAsyncOperations = "Import_AsyncOperations";
                public const string ImportDataAsyncOperations = "ImportData_AsyncOperations";
                public const string ImportFileAsyncOperations = "ImportFile_AsyncOperations";
                public const string ImportLogAsyncOperations = "ImportLog_AsyncOperations";
                public const string ImportMapAsyncOperations = "ImportMap_AsyncOperations";
                public const string IndexattributesAsyncOperations = "indexattributes_AsyncOperations";
                public const string InteractionforemailAsyncOperations = "interactionforemail_AsyncOperations";
                public const string InternalcatalogassignmentAsyncOperations = "internalcatalogassignment_AsyncOperations";
                public const string IsvConfigAsyncOperations = "IsvConfig_AsyncOperations";
                public const string KbArticleAsyncOperations = "KbArticle_AsyncOperations";
                public const string KbArticleCommentAsyncOperations = "KbArticleComment_AsyncOperations";
                public const string KbArticleTemplateAsyncOperations = "KbArticleTemplate_AsyncOperations";
                public const string KeyvaultreferenceAsyncOperations = "keyvaultreference_AsyncOperations";
                public const string KnowledgearticleAsyncOperations = "knowledgearticle_AsyncOperations";
                public const string KnowledgeBaseRecordAsyncOperations = "KnowledgeBaseRecord_AsyncOperations";
                public const string KnowledgefaqAsyncOperations = "knowledgefaq_AsyncOperations";
                public const string KnowledgesourceconsumerAsyncOperations = "knowledgesourceconsumer_AsyncOperations";
                public const string KnowledgesourceprofileAsyncOperations = "knowledgesourceprofile_AsyncOperations";
                public const string LetterAsyncOperations = "Letter_AsyncOperations";
                public const string LkAsyncoperationCreatedby = "lk_asyncoperation_createdby";
                public const string LkAsyncoperationCreatedonbehalfby = "lk_asyncoperation_createdonbehalfby";
                public const string LkAsyncoperationModifiedby = "lk_asyncoperation_modifiedby";
                public const string LkAsyncoperationModifiedonbehalfby = "lk_asyncoperation_modifiedonbehalfby";
                public const string LkAsyncoperationWorkflowactivationid = "lk_asyncoperation_workflowactivationid";
                public const string MailboxAsyncoperations = "mailbox_asyncoperations";
                public const string MailMergeTemplateAsyncOperations = "MailMergeTemplate_AsyncOperations";
                public const string MainfewshotAsyncOperations = "mainfewshot_AsyncOperations";
                public const string MakerfewshotAsyncOperations = "makerfewshot_AsyncOperations";
                public const string ManagedidentityAsyncOperations = "managedidentity_AsyncOperations";
                public const string MaskingruleAsyncOperations = "maskingrule_AsyncOperations";
                public const string McppromptAsyncOperations = "mcpprompt_AsyncOperations";
                public const string McpresourceAsyncOperations = "mcpresource_AsyncOperations";
                public const string McpresourcecontentAsyncOperations = "mcpresourcecontent_AsyncOperations";
                public const string McpserverAsyncOperations = "mcpserver_AsyncOperations";
                public const string McptoolAsyncOperations = "mcptool_AsyncOperations";
                public const string MetadataforarchivalAsyncOperations = "metadataforarchival_AsyncOperations";
                public const string MetricAsyncOperations = "metric_AsyncOperations";
                public const string MobileofflineprofileextensionAsyncOperations = "mobileofflineprofileextension_AsyncOperations";
                public const string MonthlyFiscalCalendarAsyncOperations = "MonthlyFiscalCalendar_AsyncOperations";
                public const string MsdynAibdatasetAsyncOperations = "msdyn_aibdataset_AsyncOperations";
                public const string MsdynAibdatasetfileAsyncOperations = "msdyn_aibdatasetfile_AsyncOperations";
                public const string MsdynAibdatasetrecordAsyncOperations = "msdyn_aibdatasetrecord_AsyncOperations";
                public const string MsdynAibdatasetscontainerAsyncOperations = "msdyn_aibdatasetscontainer_AsyncOperations";
                public const string MsdynAibfeedbackloopAsyncOperations = "msdyn_aibfeedbackloop_AsyncOperations";
                public const string MsdynAibfileAsyncOperations = "msdyn_aibfile_AsyncOperations";
                public const string MsdynAibfileattacheddataAsyncOperations = "msdyn_aibfileattacheddata_AsyncOperations";
                public const string MsdynAiconfigurationAsyncOperations = "msdyn_aiconfiguration_AsyncOperations";
                public const string MsdynAiconfigurationsearchAsyncOperations = "msdyn_aiconfigurationsearch_AsyncOperations";
                public const string MsdynAidataprocessingeventAsyncOperations = "msdyn_aidataprocessingevent_AsyncOperations";
                public const string MsdynAidocumenttemplateAsyncOperations = "msdyn_aidocumenttemplate_AsyncOperations";
                public const string MsdynAievaluationconfigurationAsyncOperations = "msdyn_aievaluationconfiguration_AsyncOperations";
                public const string MsdynAievaluationrunAsyncOperations = "msdyn_aievaluationrun_AsyncOperations";
                public const string MsdynAieventAsyncOperations = "msdyn_aievent_AsyncOperations";
                public const string MsdynAifptrainingdocumentAsyncOperations = "msdyn_aifptrainingdocument_AsyncOperations";
                public const string MsdynAimodelAsyncOperations = "msdyn_aimodel_AsyncOperations";
                public const string MsdynAimodelcatalogAsyncOperations = "msdyn_aimodelcatalog_AsyncOperations";
                public const string MsdynAiodimageAsyncOperations = "msdyn_aiodimage_AsyncOperations";
                public const string MsdynAiodlabelAsyncOperations = "msdyn_aiodlabel_AsyncOperations";
                public const string MsdynAiodtrainingboundingboxAsyncOperations = "msdyn_aiodtrainingboundingbox_AsyncOperations";
                public const string MsdynAiodtrainingimageAsyncOperations = "msdyn_aiodtrainingimage_AsyncOperations";
                public const string MsdynAioptimizationAsyncOperations = "msdyn_aioptimization_AsyncOperations";
                public const string MsdynAioptimizationprivatedataAsyncOperations = "msdyn_aioptimizationprivatedata_AsyncOperations";
                public const string MsdynAitemplateAsyncOperations = "msdyn_aitemplate_AsyncOperations";
                public const string MsdynAitestcaseAsyncOperations = "msdyn_aitestcase_AsyncOperations";
                public const string MsdynAitestcasedocumentAsyncOperations = "msdyn_aitestcasedocument_AsyncOperations";
                public const string MsdynAitestcaseinputAsyncOperations = "msdyn_aitestcaseinput_AsyncOperations";
                public const string MsdynAitestrunAsyncOperations = "msdyn_aitestrun_AsyncOperations";
                public const string MsdynAitestrunbatchAsyncOperations = "msdyn_aitestrunbatch_AsyncOperations";
                public const string MsdynAnalysiscomponentAsyncOperations = "msdyn_analysiscomponent_AsyncOperations";
                public const string MsdynAnalysisjobAsyncOperations = "msdyn_analysisjob_AsyncOperations";
                public const string MsdynAnalysisoverrideAsyncOperations = "msdyn_analysisoverride_AsyncOperations";
                public const string MsdynAnalysisresultAsyncOperations = "msdyn_analysisresult_AsyncOperations";
                public const string MsdynAnalysisresultdetailAsyncOperations = "msdyn_analysisresultdetail_AsyncOperations";
                public const string MsdynAppinsightsmetadataAsyncOperations = "msdyn_appinsightsmetadata_AsyncOperations";
                public const string MsdynBulkharvestrunlogAsyncOperations = "msdyn_bulkharvestrunlog_AsyncOperations";
                public const string MsdynCopilotinteractionsAsyncOperations = "msdyn_copilotinteractions_AsyncOperations";
                public const string MsdynCustomcontrolextendedsettingsAsyncOperations = "msdyn_customcontrolextendedsettings_AsyncOperations";
                public const string MsdynDataflowAsyncOperations = "msdyn_dataflow_AsyncOperations";
                public const string MsdynDataflowDatalakefolderAsyncOperations = "msdyn_dataflow_datalakefolder_AsyncOperations";
                public const string MsdynDataflowconnectionreferenceAsyncOperations = "msdyn_dataflowconnectionreference_AsyncOperations";
                public const string MsdynDataflowrefreshhistoryAsyncOperations = "msdyn_dataflowrefreshhistory_AsyncOperations";
                public const string MsdynDataflowtemplateAsyncOperations = "msdyn_dataflowtemplate_AsyncOperations";
                public const string MsdynDataworkspaceAsyncOperations = "msdyn_dataworkspace_AsyncOperations";
                public const string MsdynDmsrequestAsyncOperations = "msdyn_dmsrequest_AsyncOperations";
                public const string MsdynDmsrequeststatusAsyncOperations = "msdyn_dmsrequeststatus_AsyncOperations";
                public const string MsdynDmssyncrequestAsyncOperations = "msdyn_dmssyncrequest_AsyncOperations";
                public const string MsdynDmssyncstatusAsyncOperations = "msdyn_dmssyncstatus_AsyncOperations";
                public const string MsdynEntitylinkchatconfigurationAsyncOperations = "msdyn_entitylinkchatconfiguration_AsyncOperations";
                public const string MsdynEntityrefreshhistoryAsyncOperations = "msdyn_entityrefreshhistory_AsyncOperations";
                public const string MsdynFavoriteknowledgearticleAsyncOperations = "msdyn_favoriteknowledgearticle_AsyncOperations";
                public const string MsdynFederatedarticleAsyncOperations = "msdyn_federatedarticle_AsyncOperations";
                public const string MsdynFederatedarticleincidentAsyncOperations = "msdyn_federatedarticleincident_AsyncOperations";
                public const string MsdynFileuploadAsyncOperations = "msdyn_fileupload_AsyncOperations";
                public const string MsdynFlowActionapprovalmodelAsyncOperations = "msdyn_flow_actionapprovalmodel_AsyncOperations";
                public const string MsdynFlowApprovalAsyncOperations = "msdyn_flow_approval_AsyncOperations";
                public const string MsdynFlowApprovalrequestAsyncOperations = "msdyn_flow_approvalrequest_AsyncOperations";
                public const string MsdynFlowApprovalresponseAsyncOperations = "msdyn_flow_approvalresponse_AsyncOperations";
                public const string MsdynFlowApprovalstepAsyncOperations = "msdyn_flow_approvalstep_AsyncOperations";
                public const string MsdynFlowAwaitallactionapprovalmodelAsyncOperations = "msdyn_flow_awaitallactionapprovalmodel_AsyncOperations";
                public const string MsdynFlowAwaitallapprovalmodelAsyncOperations = "msdyn_flow_awaitallapprovalmodel_AsyncOperations";
                public const string MsdynFlowBasicapprovalmodelAsyncOperations = "msdyn_flow_basicapprovalmodel_AsyncOperations";
                public const string MsdynFlowFlowapprovalAsyncOperations = "msdyn_flow_flowapproval_AsyncOperations";
                public const string MsdynFormmappingAsyncOperations = "msdyn_formmapping_AsyncOperations";
                public const string MsdynFunctionAsyncOperations = "msdyn_function_AsyncOperations";
                public const string MsdynHarvesteligibilityconditionAsyncOperations = "msdyn_harvesteligibilitycondition_AsyncOperations";
                public const string MsdynHarvestworkitemAsyncOperations = "msdyn_harvestworkitem_AsyncOperations";
                public const string MsdynHealthcareFeedbackAsyncOperations = "msdyn_healthcare_feedback_AsyncOperations";
                public const string MsdynHelppageAsyncOperations = "msdyn_helppage_AsyncOperations";
                public const string MsdynHistoricalcaseharvestbatchAsyncOperations = "msdyn_historicalcaseharvestbatch_AsyncOperations";
                public const string MsdynHistoricalcaseharvestrunAsyncOperations = "msdyn_historicalcaseharvestrun_AsyncOperations";
                public const string MsdynInsightsstorevirtualentityAsyncOperations = "msdyn_insightsstorevirtualentity_AsyncOperations";
                public const string MsdynIntegratedsearchproviderAsyncOperations = "msdyn_integratedsearchprovider_AsyncOperations";
                public const string MsdynInterimupdateknowledgearticleAsyncOperations = "msdyn_interimupdateknowledgearticle_AsyncOperations";
                public const string MsdynKalanguagesettingAsyncOperations = "msdyn_kalanguagesetting_AsyncOperations";
                public const string MsdynKbattachmentAsyncOperations = "msdyn_kbattachment_AsyncOperations";
                public const string MsdynKmfederatedsearchconfigAsyncOperations = "msdyn_kmfederatedsearchconfig_AsyncOperations";
                public const string MsdynKmpersonalizationsettingAsyncOperations = "msdyn_kmpersonalizationsetting_AsyncOperations";
                public const string MsdynKnowledgearticlecustomentityAsyncOperations = "msdyn_knowledgearticlecustomentity_AsyncOperations";
                public const string MsdynKnowledgearticleimageAsyncOperations = "msdyn_knowledgearticleimage_AsyncOperations";
                public const string MsdynKnowledgearticletemplateAsyncOperations = "msdyn_knowledgearticletemplate_AsyncOperations";
                public const string MsdynKnowledgeassetconfigurationAsyncOperations = "msdyn_knowledgeassetconfiguration_AsyncOperations";
                public const string MsdynKnowledgeconfigurationAsyncOperations = "msdyn_knowledgeconfiguration_AsyncOperations";
                public const string MsdynKnowledgeharvestjobrecordAsyncOperations = "msdyn_knowledgeharvestjobrecord_AsyncOperations";
                public const string MsdynKnowledgeharvestplanAsyncOperations = "msdyn_knowledgeharvestplan_AsyncOperations";
                public const string MsdynKnowledgeinteractioninsightAsyncOperations = "msdyn_knowledgeinteractioninsight_AsyncOperations";
                public const string MsdynKnowledgemanagementsettingAsyncOperations = "msdyn_knowledgemanagementsetting_AsyncOperations";
                public const string MsdynKnowledgepersonalfilterAsyncOperations = "msdyn_knowledgepersonalfilter_AsyncOperations";
                public const string MsdynKnowledgesearchfilterAsyncOperations = "msdyn_knowledgesearchfilter_AsyncOperations";
                public const string MsdynKnowledgesearchinsightAsyncOperations = "msdyn_knowledgesearchinsight_AsyncOperations";
                public const string MsdynMobileappAsyncOperations = "msdyn_mobileapp_AsyncOperations";
                public const string MsdynModulerundetailAsyncOperations = "msdyn_modulerundetail_AsyncOperations";
                public const string MsdynObjectdetectionproductAsyncOperations = "msdyn_objectdetectionproduct_AsyncOperations";
                public const string MsdynOnlineshopperintentionAsyncOperations = "msdyn_onlineshopperintention_AsyncOperations";
                public const string MsdynPlanAsyncOperations = "msdyn_plan_AsyncOperations";
                public const string MsdynPlanartifactAsyncOperations = "msdyn_planartifact_AsyncOperations";
                public const string MsdynPlanattachmentAsyncOperations = "msdyn_planattachment_AsyncOperations";
                public const string MsdynPmanalysishistoryAsyncOperations = "msdyn_pmanalysishistory_AsyncOperations";
                public const string MsdynPmbusinessruleautomationconfigAsyncOperations = "msdyn_pmbusinessruleautomationconfig_AsyncOperations";
                public const string MsdynPmcalendarAsyncOperations = "msdyn_pmcalendar_AsyncOperations";
                public const string MsdynPmcalendarversionAsyncOperations = "msdyn_pmcalendarversion_AsyncOperations";
                public const string MsdynPminferredtaskAsyncOperations = "msdyn_pminferredtask_AsyncOperations";
                public const string MsdynPmprocessextendedmetadataversionAsyncOperations = "msdyn_pmprocessextendedmetadataversion_AsyncOperations";
                public const string MsdynPmprocesstemplateAsyncOperations = "msdyn_pmprocesstemplate_AsyncOperations";
                public const string MsdynPmprocessusersettingsAsyncOperations = "msdyn_pmprocessusersettings_AsyncOperations";
                public const string MsdynPmprocessversionAsyncOperations = "msdyn_pmprocessversion_AsyncOperations";
                public const string MsdynPmrecordingAsyncOperations = "msdyn_pmrecording_AsyncOperations";
                public const string MsdynPmsimulationAsyncOperations = "msdyn_pmsimulation_AsyncOperations";
                public const string MsdynPmtabAsyncOperations = "msdyn_pmtab_AsyncOperations";
                public const string MsdynPmtemplateAsyncOperations = "msdyn_pmtemplate_AsyncOperations";
                public const string MsdynPmviewAsyncOperations = "msdyn_pmview_AsyncOperations";
                public const string MsdynPowerappswrapbuildAsyncOperations = "msdyn_powerappswrapbuild_AsyncOperations";
                public const string MsdynQnaAsyncOperations = "msdyn_qna_AsyncOperations";
                public const string MsdynRichtextfileAsyncOperations = "msdyn_richtextfile_AsyncOperations";
                public const string MsdynRtestructuredtemplateAsyncOperations = "msdyn_rtestructuredtemplate_AsyncOperations";
                public const string MsdynRtestructuredtemplateconfigAsyncOperations = "msdyn_rtestructuredtemplateconfig_AsyncOperations";
                public const string MsdynRtetemplatemappingAsyncOperations = "msdyn_rtetemplatemapping_AsyncOperations";
                public const string MsdynSalesforcestructuredobjectAsyncOperations = "msdyn_salesforcestructuredobject_AsyncOperations";
                public const string MsdynSalesforcestructuredqnaconfigAsyncOperations = "msdyn_salesforcestructuredqnaconfig_AsyncOperations";
                public const string MsdynScheduleAsyncOperations = "msdyn_schedule_AsyncOperations";
                public const string MsdynServiceconfigurationAsyncOperations = "msdyn_serviceconfiguration_AsyncOperations";
                public const string MsdynSlakpiAsyncOperations = "msdyn_slakpi_AsyncOperations";
                public const string MsdynSolutionhealthruleAsyncOperations = "msdyn_solutionhealthrule_AsyncOperations";
                public const string MsdynSolutionhealthruleargumentAsyncOperations = "msdyn_solutionhealthruleargument_AsyncOperations";
                public const string MsdynSolutionhealthrulesetAsyncOperations = "msdyn_solutionhealthruleset_AsyncOperations";
                public const string MsdynTourAsyncOperations = "msdyn_tour_AsyncOperations";
                public const string MsdynVirtualtablecolumncandidateAsyncOperations = "msdyn_virtualtablecolumncandidate_AsyncOperations";
                public const string MsdynWorkflowactionstatusAsyncOperations = "msdyn_workflowactionstatus_AsyncOperations";
                public const string MsdynceBotcontentAsyncOperations = "msdynce_botcontent_AsyncOperations";
                public const string MsgraphresourcetosubscriptionAsyncOperations = "msgraphresourcetosubscription_AsyncOperations";
                public const string MspcatCatalogsubmissionfilesAsyncOperations = "mspcat_catalogsubmissionfiles_AsyncOperations";
                public const string MspcatPackagestoreAsyncOperations = "mspcat_packagestore_AsyncOperations";
                public const string OrganizationAsyncOperations = "Organization_AsyncOperations";
                public const string OrganizationdatasyncfnostateAsyncOperations = "organizationdatasyncfnostate_AsyncOperations";
                public const string OrganizationdatasyncstateAsyncOperations = "organizationdatasyncstate_AsyncOperations";
                public const string OrganizationdatasyncsubscriptionAsyncOperations = "organizationdatasyncsubscription_AsyncOperations";
                public const string OrganizationdatasyncsubscriptionentityAsyncOperations = "organizationdatasyncsubscriptionentity_AsyncOperations";
                public const string OrganizationdatasyncsubscriptionfnotableAsyncOperations = "organizationdatasyncsubscriptionfnotable_AsyncOperations";
                public const string OrganizationsettingAsyncOperations = "organizationsetting_AsyncOperations";
                public const string OwnerAsyncoperations = "owner_asyncoperations";
                public const string PackageAsyncOperations = "package_AsyncOperations";
                public const string PackagehistoryAsyncOperations = "packagehistory_AsyncOperations";
                public const string PdfsettingAsyncOperations = "pdfsetting_AsyncOperations";
                public const string PhoneCallAsyncOperations = "PhoneCall_AsyncOperations";
                public const string PlannerbusinessscenarioAsyncOperations = "plannerbusinessscenario_AsyncOperations";
                public const string PlannersyncactionAsyncOperations = "plannersyncaction_AsyncOperations";
                public const string PluginAsyncOperations = "plugin_AsyncOperations";
                public const string PluginpackageAsyncOperations = "pluginpackage_AsyncOperations";
                public const string PositionAsyncOperations = "position_AsyncOperations";
                public const string PostAsyncOperations = "post_AsyncOperations";
                public const string PostFollowAsyncOperations = "PostFollow_AsyncOperations";
                public const string PowerfxruleAsyncOperations = "powerfxrule_AsyncOperations";
                public const string PowerpagecomponentAsyncOperations = "powerpagecomponent_AsyncOperations";
                public const string PowerpagesddosalertAsyncOperations = "powerpagesddosalert_AsyncOperations";
                public const string PowerpagesiteAsyncOperations = "powerpagesite_AsyncOperations";
                public const string PowerpagesitelanguageAsyncOperations = "powerpagesitelanguage_AsyncOperations";
                public const string PowerpagesitepublishedAsyncOperations = "powerpagesitepublished_AsyncOperations";
                public const string PowerpagesmanagedidentityAsyncOperations = "powerpagesmanagedidentity_AsyncOperations";
                public const string PowerpagesscanreportAsyncOperations = "powerpagesscanreport_AsyncOperations";
                public const string PowerpagessourcefileAsyncOperations = "powerpagessourcefile_AsyncOperations";
                public const string PrivilegeAsyncOperations = "Privilege_AsyncOperations";
                public const string PrivilegecheckerlogAsyncOperations = "privilegecheckerlog_AsyncOperations";
                public const string PrivilegecheckerrunAsyncOperations = "privilegecheckerrun_AsyncOperations";
                public const string PrivilegesremovalsettingAsyncOperations = "privilegesremovalsetting_AsyncOperations";
                public const string ProcessorregistrationAsyncOperations = "processorregistration_AsyncOperations";
                public const string ProcessstageparameterAsyncOperations = "processstageparameter_AsyncOperations";
                public const string ProfileruleAsyncOperations = "profilerule_AsyncOperations";
                public const string ProvisionlanguageforuserAsyncOperations = "provisionlanguageforuser_AsyncOperations";
                public const string PurviewlabelinfoAsyncOperations = "purviewlabelinfo_AsyncOperations";
                public const string PurviewlabelsynccacheAsyncOperations = "purviewlabelsynccache_AsyncOperations";
                public const string QuarterlyFiscalCalendarAsyncOperations = "QuarterlyFiscalCalendar_AsyncOperations";
                public const string QueueAsyncOperations = "Queue_AsyncOperations";
                public const string QueueItemAsyncOperations = "QueueItem_AsyncOperations";
                public const string ReconciliationentityinfoAsyncOperations = "reconciliationentityinfo_AsyncOperations";
                public const string ReconciliationentitystepinfoAsyncOperations = "reconciliationentitystepinfo_AsyncOperations";
                public const string ReconciliationinfoAsyncOperations = "reconciliationinfo_AsyncOperations";
                public const string RecordfilterAsyncOperations = "recordfilter_AsyncOperations";
                public const string RecurringAppointmentMasterAsyncOperations = "RecurringAppointmentMaster_AsyncOperations";
                public const string RecyclebinconfigAsyncOperations = "recyclebinconfig_AsyncOperations";
                public const string RelationshipattributeAsyncOperations = "relationshipattribute_AsyncOperations";
                public const string RelationshipRoleAsyncOperations = "RelationshipRole_AsyncOperations";
                public const string RelationshipRoleMapAsyncOperations = "RelationshipRoleMap_AsyncOperations";
                public const string ReportAsyncOperations = "Report_AsyncOperations";
                public const string ReportparameterAsyncOperations = "reportparameter_AsyncOperations";
                public const string RetaineddataexcelAsyncOperations = "retaineddataexcel_AsyncOperations";
                public const string RetentioncleanupinfoAsyncOperations = "retentioncleanupinfo_AsyncOperations";
                public const string RetentioncleanupoperationAsyncOperations = "retentioncleanupoperation_AsyncOperations";
                public const string RetentionconfigAsyncOperations = "retentionconfig_AsyncOperations";
                public const string RetentionfailuredetailAsyncOperations = "retentionfailuredetail_AsyncOperations";
                public const string RetentionoperationAsyncOperations = "retentionoperation_AsyncOperations";
                public const string RetentionoperationdetailAsyncOperations = "retentionoperationdetail_AsyncOperations";
                public const string RetentionsuccessdetailAsyncOperations = "retentionsuccessdetail_AsyncOperations";
                public const string RevokeinheritedaccessrecordstrackerAsyncOperations = "revokeinheritedaccessrecordstracker_AsyncOperations";
                public const string RoleAsyncOperations = "Role_AsyncOperations";
                public const string RoleeditorlayoutAsyncOperations = "roleeditorlayout_AsyncOperations";
                public const string RollupfieldAsyncOperations = "rollupfield_AsyncOperations";
                public const string RoutingruleAsyncOperations = "routingrule_AsyncOperations";
                public const string RoutingruleitemAsyncOperations = "routingruleitem_AsyncOperations";
                public const string SaSuggestedactionAsyncOperations = "sa_suggestedaction_AsyncOperations";
                public const string SaSuggestedactioncriteriaAsyncOperations = "sa_suggestedactioncriteria_AsyncOperations";
                public const string SavedQueryAsyncOperations = "SavedQuery_AsyncOperations";
                public const string SavingruleAsyncOperations = "savingrule_AsyncOperations";
                public const string SdkMessageProcessingStepAsyncOperations = "SdkMessageProcessingStep_AsyncOperations";
                public const string SearchattributesettingsAsyncOperations = "searchattributesettings_AsyncOperations";
                public const string SearchcustomanalyzerAsyncOperations = "searchcustomanalyzer_AsyncOperations";
                public const string SearchrelationshipsettingsAsyncOperations = "searchrelationshipsettings_AsyncOperations";
                public const string SemiAnnualFiscalCalendarAsyncOperations = "SemiAnnualFiscalCalendar_AsyncOperations";
                public const string SensitivitylabelattributemappingAsyncOperations = "sensitivitylabelattributemapping_AsyncOperations";
                public const string ServiceplanAsyncOperations = "serviceplan_AsyncOperations";
                public const string ServiceplancustomcontrolAsyncOperations = "serviceplancustomcontrol_AsyncOperations";
                public const string ServiceplanmappingAsyncOperations = "serviceplanmapping_AsyncOperations";
                public const string SettingdefinitionAsyncOperations = "settingdefinition_AsyncOperations";
                public const string SharedlinksettingAsyncOperations = "sharedlinksetting_AsyncOperations";
                public const string SharedobjectAsyncOperations = "sharedobject_AsyncOperations";
                public const string SharedworkspaceAsyncOperations = "sharedworkspace_AsyncOperations";
                public const string SharedworkspacepoolAsyncOperations = "sharedworkspacepool_AsyncOperations";
                public const string SharePointDocumentLocationAsyncOperations = "SharePointDocumentLocation_AsyncOperations";
                public const string SharepointmanagedidentityAsyncOperations = "sharepointmanagedidentity_AsyncOperations";
                public const string SharePointSiteAsyncOperations = "SharePointSite_AsyncOperations";
                public const string SideloadedaipluginAsyncOperations = "sideloadedaiplugin_AsyncOperations";
                public const string SignalregistrationAsyncOperations = "signalregistration_AsyncOperations";
                public const string SimilarityruleAsyncOperations = "similarityrule_AsyncOperations";
                public const string SkillAsyncOperations = "skill_AsyncOperations";
                public const string SkillresourceAsyncOperations = "skillresource_AsyncOperations";
                public const string SlabaseAsyncOperations = "slabase_AsyncOperations";
                public const string SocialActivityAsyncOperations = "SocialActivity_AsyncOperations";
                public const string SocialProfileAsyncOperations = "SocialProfile_AsyncOperations";
                public const string SolutioncomponentattributeconfigurationAsyncOperations = "solutioncomponentattributeconfiguration_AsyncOperations";
                public const string SolutioncomponentbatchconfigurationAsyncOperations = "solutioncomponentbatchconfiguration_AsyncOperations";
                public const string SolutioncomponentconfigurationAsyncOperations = "solutioncomponentconfiguration_AsyncOperations";
                public const string SolutioncomponentrelationshipconfigurationAsyncOperations = "solutioncomponentrelationshipconfiguration_AsyncOperations";
                public const string StagedattributelookupvalueAsyncOperations = "stagedattributelookupvalue_AsyncOperations";
                public const string StagedattributepicklistvalueAsyncOperations = "stagedattributepicklistvalue_AsyncOperations";
                public const string StagedentityAsyncOperations = "stagedentity_AsyncOperations";
                public const string StagedentityattributeAsyncOperations = "stagedentityattribute_AsyncOperations";
                public const string StagedentityrelationshipAsyncOperations = "stagedentityrelationship_AsyncOperations";
                public const string StagedentityrelationshiprelationshipsAsyncOperations = "stagedentityrelationshiprelationships_AsyncOperations";
                public const string StagedentityrelationshiproleAsyncOperations = "stagedentityrelationshiprole_AsyncOperations";
                public const string StagedmetadataasyncoperationAsyncOperations = "stagedmetadataasyncoperation_AsyncOperations";
                public const string StagedoptionsetAsyncOperations = "stagedoptionset_AsyncOperations";
                public const string StagedrelationshipAsyncOperations = "stagedrelationship_AsyncOperations";
                public const string StagedrelationshipextraconditionAsyncOperations = "stagedrelationshipextracondition_AsyncOperations";
                public const string StagedviewattributeAsyncOperations = "stagedviewattribute_AsyncOperations";
                public const string StagesolutionuploadAsyncOperations = "stagesolutionupload_AsyncOperations";
                public const string SubjectAsyncOperations = "Subject_AsyncOperations";
                public const string SupportusertableAsyncOperations = "supportusertable_AsyncOperations";
                public const string SynapsedatabaseAsyncOperations = "synapsedatabase_AsyncOperations";
                public const string SynapselinkexternaltablestateAsyncOperations = "synapselinkexternaltablestate_AsyncOperations";
                public const string SynapselinkprofileAsyncOperations = "synapselinkprofile_AsyncOperations";
                public const string SynapselinkprofileentityAsyncOperations = "synapselinkprofileentity_AsyncOperations";
                public const string SynapselinkprofileentitystateAsyncOperations = "synapselinkprofileentitystate_AsyncOperations";
                public const string SynapselinkscheduleAsyncOperations = "synapselinkschedule_AsyncOperations";
                public const string SystemUserAsyncoperation = "system_user_asyncoperation";
                public const string SystemFormAsyncOperations = "SystemForm_AsyncOperations";
                public const string SystemUserAsyncOperations = "SystemUser_AsyncOperations";
                public const string SystemuserauthorizationchangetrackerAsyncOperations = "systemuserauthorizationchangetracker_AsyncOperations";
                public const string TagAsyncOperations = "tag_AsyncOperations";
                public const string TaggedflowsessionAsyncOperations = "taggedflowsession_AsyncOperations";
                public const string TaggedprocessAsyncOperations = "taggedprocess_AsyncOperations";
                public const string TaskAsyncOperations = "Task_AsyncOperations";
                public const string TdsmetadataAsyncOperations = "tdsmetadata_AsyncOperations";
                public const string TeamAsyncoperation = "team_asyncoperation";
                public const string TeamAsyncOperations = "Team_AsyncOperations";
                public const string TeammobileofflineprofilemembershipAsyncOperations = "teammobileofflineprofilemembership_AsyncOperations";
                public const string TemplateAsyncOperations = "Template_AsyncOperations";
                public const string TerritoryAsyncOperations = "Territory_AsyncOperations";
                public const string ThemeAsyncOperations = "theme_AsyncOperations";
                public const string ToolinggatewayAsyncOperations = "toolinggateway_AsyncOperations";
                public const string ToolinggatewaymcpserverAsyncOperations = "toolinggatewaymcpserver_AsyncOperations";
                public const string TraitregistrationAsyncOperations = "traitregistration_AsyncOperations";
                public const string TransactionCurrencyAsyncOperations = "TransactionCurrency_AsyncOperations";
                public const string UnstructuredfilesearchentityAsyncOperations = "unstructuredfilesearchentity_AsyncOperations";
                public const string UnstructuredfilesearchrecordAsyncOperations = "unstructuredfilesearchrecord_AsyncOperations";
                public const string UnstructuredfilesearchrecordstatusAsyncOperations = "unstructuredfilesearchrecordstatus_AsyncOperations";
                public const string UserFormAsyncOperations = "UserForm_AsyncOperations";
                public const string UsermappingAsyncOperations = "usermapping_AsyncOperations";
                public const string UsermobileofflineprofilemembershipAsyncOperations = "usermobileofflineprofilemembership_AsyncOperations";
                public const string UserQueryAsyncOperations = "UserQuery_AsyncOperations";
                public const string UserratingAsyncOperations = "userrating_AsyncOperations";
                public const string UxagentcomponentAsyncOperations = "uxagentcomponent_AsyncOperations";
                public const string UxagentcomponentrevisionAsyncOperations = "uxagentcomponentrevision_AsyncOperations";
                public const string UxagentprojectAsyncOperations = "uxagentproject_AsyncOperations";
                public const string UxagentprojectfileAsyncOperations = "uxagentprojectfile_AsyncOperations";
                public const string ViewasexamplequestionAsyncOperations = "viewasexamplequestion_AsyncOperations";
                public const string VirtualentitymetadataAsyncOperations = "virtualentitymetadata_AsyncOperations";
                public const string WorkflowbinaryAsyncOperations = "workflowbinary_AsyncOperations";
                public const string WorkflowmetadataAsyncOperations = "workflowmetadata_AsyncOperations";
                public const string WorkqueueAsyncOperations = "workqueue_AsyncOperations";
                public const string WorkqueueitemAsyncOperations = "workqueueitem_AsyncOperations";
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

        public static AsyncOperation Retrieve(IOrganizationService service, Guid id)
        {
            return Retrieve(service, id, new ColumnSet(true));
        }

        public static AsyncOperation Retrieve(IOrganizationService service, Guid id, ColumnSet columnSet)
        {
            return service.Retrieve("asyncoperation", id, columnSet).ToEntity<AsyncOperation>();
        }

        public AsyncOperation GetChangedEntity()
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
            return new AsyncOperation(Id) { Attributes = attr };
        }
        #endregion
    }

    #region Context
    public partial class DataContext
    {
        public IQueryable<AsyncOperation> AsyncOperationSet
        {
            get
            {
                return CreateQuery<AsyncOperation>();
            }
        }
    }
    #endregion
}
