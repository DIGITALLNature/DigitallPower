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
	/// Rule used to identify potential duplicates.
	/// </summary>
    [DataContract]
    [EntityLogicalName("duplicaterule")]
    [System.CodeDom.Compiler.GeneratedCode("dgtp", "2026")]
    [ExcludeFromCodeCoverage]
    [SuppressMessage("Design", "CA1034")]
    [SuppressMessage("Performance", "CA1815")]
    public partial class DuplicateRule : Entity, INotifyPropertyChanging, INotifyPropertyChanged
    {
        #region ctor
        [DebuggerNonUserCode]
        public DuplicateRule() : this(false)
        {
        }
        [DebuggerNonUserCode]
        public DuplicateRule(bool trackChanges = false) : base(EntityLogicalName)
        {
            _trackChanges = trackChanges;
        }
        [DebuggerNonUserCode]
        public DuplicateRule(Guid id, bool trackChanges = false) : base(EntityLogicalName, id)
        {
            _trackChanges = trackChanges;
        }
        [DebuggerNonUserCode]
        public DuplicateRule(KeyAttributeCollection keyAttributes, bool trackChanges = false) : base(EntityLogicalName, keyAttributes)
        {
            _trackChanges = trackChanges;
        }
        [DebuggerNonUserCode]
        public DuplicateRule(string keyName, object keyValue, bool trackChanges = false) : base(EntityLogicalName, keyName, keyValue)
        {
            _trackChanges = trackChanges;
        }
        #endregion

        #region fields
        private readonly bool _trackChanges;
        private readonly Lazy<HashSet<string>> _changedProperties = new();
        #endregion

        #region consts
        public const string EntityLogicalName = "duplicaterule";
        public const string PrimaryNameAttribute = "name";
        public const int EntityTypeCode = 4414;
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
        [AttributeLogicalName("duplicateruleid")]
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
                DuplicateRuleId = value;
            }
        }

        /// <summary>
		/// Unique identifier of the duplicate detection rule.
		/// </summary>
        [AttributeLogicalName("duplicateruleid")]
        public Guid? DuplicateRuleId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<Guid?>("duplicateruleid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("duplicateruleid", value);
                base.Id = value.HasValue ? value.Value : Guid.Empty;
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Database table that stores match codes for the record type being evaluated for potential duplicates.
		/// </summary>
        [AttributeLogicalName("baseentitymatchcodetable")]
        public string? BaseEntityMatchCodeTable
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("baseentitymatchcodetable");
            }
        }

        /// <summary>
		/// Record type of the record being evaluated for potential duplicates.
		/// </summary>
        [AttributeLogicalName("baseentityname")]
        public string? BaseEntityName
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("baseentityname");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("baseentityname", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Record type of the record being evaluated for potential duplicates.
		/// </summary>
        [AttributeLogicalName("baseentitytypecode")]
        public OptionSetValue? BaseEntityTypeCode
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("baseentitytypecode");
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
		/// Unique identifier of the user who created the duplicate detection rule.
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
		/// Date and time when the duplicate detection rule was created.
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
		/// Unique identifier of the delegate user who created the duplicaterule.
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
		/// Description of the duplicate detection rule.
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
		/// Determines whether to flag inactive records as duplicates
		/// </summary>
        [AttributeLogicalName("excludeinactiverecords")]
        public bool? ExcludeInactiveRecords
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("excludeinactiverecords");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("excludeinactiverecords", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates if the operator is case-sensitive.
		/// </summary>
        [AttributeLogicalName("iscasesensitive")]
        public bool? IsCaseSensitive
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("iscasesensitive");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("iscasesensitive", value);
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
		/// Database table that stores match codes for potential duplicate records.
		/// </summary>
        [AttributeLogicalName("matchingentitymatchcodetable")]
        public string? MatchingEntityMatchCodeTable
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("matchingentitymatchcodetable");
            }
        }

        /// <summary>
		/// Record type of the records being evaluated as potential duplicates.
		/// </summary>
        [AttributeLogicalName("matchingentityname")]
        public string? MatchingEntityName
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("matchingentityname");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("matchingentityname", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Record type of the records being evaluated as potential duplicates.
		/// </summary>
        [AttributeLogicalName("matchingentitytypecode")]
        public OptionSetValue? MatchingEntityTypeCode
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("matchingentitytypecode");
            }
        }

        /// <summary>
		/// Unique identifier of the user who last modified the duplicate detection rule.
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
		/// Date and time when the duplicate detection rule was last modified.
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
		/// Unique identifier of the delegate user who last modified the duplicaterule.
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
		/// Name of the duplicate detection rule.
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
		/// Unique identifier of the user or team who owns the duplicate detection rule.
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
		/// Unique identifier of the business unit that owns duplicate detection rule.
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
		/// Unique identifier of the team who owns the duplicate detection rule.
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
		/// Unique identifier of the user who owns the duplicate detection rule.
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
		/// Status of the duplicate detection rule.
		/// </summary>
        [AttributeLogicalName("statecode")]
        public OptionSetValue? StateCode
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("statecode");
            }
        }

        /// <summary>
		/// Reason for the status of the duplicate detection rule.
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
                OnPropertyChanging();
                SetAttributeValue("uniquename", value);
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
        #endregion

        #region NavigationProperties

        /// <summary>
        /// 1:N DuplicateRule_DuplicateRuleConditions
        /// </summary>
        [RelationshipSchemaName("DuplicateRule_DuplicateRuleConditions")]
        public IEnumerable<DuplicateRuleCondition> DuplicateRuleDuplicateRuleConditions
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<DuplicateRuleCondition>("DuplicateRule_DuplicateRuleConditions", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("DuplicateRule_DuplicateRuleConditions", null, value);
                OnPropertyChanged();
            }
        }
        #endregion

        #region Options
        public static partial class Options
        {
            public struct BaseEntityTypeCode
            {
                public const int Account = 1;
                public const int Contact = 2;
                public const int Note = 5;
                public const int BusinessUnitMap = 6;
                public const int Owner = 7;
                public const int User = 8;
                public const int Team = 9;
                public const int BusinessUnit = 10;
                public const int SystemUserPrincipal = 14;
                public const int Subscription = 29;
                public const int FilterTemplate = 30;
                public const int PrivilegeObjectTypeCode = 31;
                public const int SubscriptionSynchronizationInformation = 33;
                public const int TrackingInformationForDeletedEntities = 35;
                public const int ClientUpdate = 36;
                public const int SubscriptionManuallyTrackedObject = 37;
                public const int SystemUserBusinessUnitEntityMap = 42;
                public const int FieldSharing = 44;
                public const int SubscriptionStatisticOffline = 45;
                public const int SubscriptionStatisticOutlook = 46;
                public const int SubscriptionSyncEntryOffline = 47;
                public const int SubscriptionSyncEntryOutlook = 48;
                public const int Position = 50;
                public const int SystemUserManagerMap = 51;
                public const int UserSearchFacet = 52;
                public const int GlobalSearchConfiguration = 54;
                public const int FileAttachment = 55;
                public const int SystemUserAuthorizationChangeTracker = 60;
                public const int RecordFilter = 72;
                public const int EntityRecordFilter = 73;
                public const int SecuredMaskingRule = 74;
                public const int PrivilegeCheckerRun = 75;
                public const int PrivilegeCheckerLog = 76;
                public const int VirtualEntityDataProvider = 78;
                public const int VirtualEntityDataSource = 85;
                public const int TeamTemplate = 92;
                public const int SocialProfile = 99;
                public const int ServicePlan = 101;
                public const int PrivilegesRemovalSetting = 103;
                public const int IndexedArticle = 126;
                public const int Article = 127;
                public const int Subject = 129;
                public const int Announcement = 132;
                public const int ActivityParty = 135;
                public const int UserSettings = 150;
                public const int CanvasApp = 300;
                public const int CallbackRegistration = 301;
                public const int Connector = 372;
                public const int ConnectionInstance = 373;
                public const int EnvironmentVariableDefinition = 380;
                public const int EnvironmentVariableValue = 381;
                public const int AITemplate = 400;
                public const int AIModel = 401;
                public const int AIConfiguration = 402;
                public const int Dataflow = 418;
                public const int EntityAnalyticsConfig = 430;
                public const int ImageAttributeConfiguration = 431;
                public const int EntityImageConfiguration = 432;
                public const int NewProcess = 950;
                public const int TranslationProcess = 951;
                public const int ExpiredProcess = 955;
                public const int Attachment = 1001;
                public const int Attachment_ = 1002;
                public const int InternalAddress = 1003;
                public const int ImageDescriptor = 1007;
                public const int ArticleTemplate = 1016;
                public const int Organization = 1019;
                public const int OrganizationUI = 1021;
                public const int Privilege = 1023;
                public const int SystemForm = 1030;
                public const int UserDashboard = 1031;
                public const int SecurityRole = 1036;
                public const int RoleTemplate = 1037;
                public const int View = 1039;
                public const int StringMap = 1043;
                public const int Address = 1071;
                public const int SubscriptionClients = 1072;
                public const int StatusMap = 1075;
                public const int ArticleComment = 1082;
                public const int UserFiscalCalendar = 1086;
                public const int AuthorizationServer = 1094;
                public const int PartnerApplication = 1095;
                public const int SystemChart = 1111;
                public const int UserChart = 1112;
                public const int RibbonTabToCommandMapping = 1113;
                public const int RibbonContextGroup = 1115;
                public const int RibbonCommand = 1116;
                public const int RibbonRule = 1117;
                public const int ApplicationRibbons = 1120;
                public const int RibbonDifference = 1130;
                public const int ReplicationBacklog = 1140;
                public const int DocumentSuggestions = 1189;
                public const int SuggestionCardTemplate = 1190;
                public const int FieldSecurityProfile = 1200;
                public const int FieldPermission = 1201;
                public const int TeamProfiles = 1203;
                public const int Application = 1204;
                public const int ChannelPropertyGroup = 1234;
                public const int ChannelProperty = 1236;
                public const int SocialInsightsConfiguration = 1300;
                public const int SavedOrganizationInsightsConfiguration = 1309;
                public const int SyncAttributeMappingProfile = 1400;
                public const int SyncAttributeMapping = 1401;
                public const int TeamSyncAttributeMappingProfiles = 1403;
                public const int PrincipalSyncAttributeMap = 1404;
                public const int AnnualFiscalCalendar = 2000;
                public const int SemiannualFiscalCalendar = 2001;
                public const int QuarterlyFiscalCalendar = 2002;
                public const int MonthlyFiscalCalendar = 2003;
                public const int FixedMonthlyFiscalCalendar = 2004;
                public const int EmailTemplate = 2010;
                public const int UnresolvedAddress = 2012;
                public const int Territory = 2013;
                public const int Theme = 2015;
                public const int UserMapping = 2016;
                public const int Queue = 2020;
                public const int QueueItemCount = 2023;
                public const int QueueMemberCount = 2024;
                public const int License = 2027;
                public const int QueueItem = 2029;
                public const int UserEntityUISettings = 2500;
                public const int UserEntityInstanceData = 2501;
                public const int IntegrationStatus = 3000;
                public const int ChannelAccessProfile = 3005;
                public const int ExternalParty = 3008;
                public const int ConnectionRole = 3231;
                public const int ConnectionRoleObjectTypeCode = 3233;
                public const int Connection = 3234;
                public const int Calendar = 4003;
                public const int CalendarRule = 4004;
                public const int InterProcessLock = 4011;
                public const int EmailHash = 4023;
                public const int DisplayStringMap = 4101;
                public const int DisplayString = 4102;
                public const int Notification = 4110;
                public const int ExchangeSyncIdMapping = 4120;
                public const int Activity = 4200;
                public const int Appointment = 4201;
                public const int Email = 4202;
                public const int Fax = 4204;
                public const int Letter = 4207;
                public const int PhoneCall = 4210;
                public const int Task = 4212;
                public const int SocialActivity = 4216;
                public const int UntrackedEmail = 4220;
                public const int SavedView = 4230;
                public const int MetadataDifference = 4231;
                public const int BusinessDataLocalizedLabel = 4232;
                public const int RecurrenceRule = 4250;
                public const int RecurringAppointment = 4251;
                public const int EmailSearch = 4299;
                public const int DataImport = 4410;
                public const int DataMap = 4411;
                public const int ImportSourceFile = 4412;
                public const int ImportData = 4413;
                public const int DuplicateDetectionRule = 4414;
                public const int DuplicateRecord = 4415;
                public const int DuplicateRuleCondition = 4416;
                public const int ColumnMapping = 4417;
                public const int ListValueMapping = 4418;
                public const int LookupMapping = 4419;
                public const int OwnerMapping = 4420;
                public const int ImportLog = 4423;
                public const int BulkDeleteOperation = 4424;
                public const int BulkDeleteFailure = 4425;
                public const int TransformationMapping = 4426;
                public const int TransformationParameterMapping = 4427;
                public const int ImportEntityMapping = 4428;
                public const int DataPerformanceDashboard = 4450;
                public const int OfficeDocument = 4490;
                public const int RelationshipRole = 4500;
                public const int RelationshipRoleMap = 4501;
                public const int CustomerRelationship = 4502;
                public const int Auditing = 4567;
                public const int RibbonClientMetadata = 4579;
                public const int EntityMap = 4600;
                public const int AttributeMap = 4601;
                public const int PlugInType = 4602;
                public const int PlugInTypeStatistic = 4603;
                public const int PlugInAssembly = 4605;
                public const int SdkMessage = 4606;
                public const int SdkMessageFilter = 4607;
                public const int SdkMessageProcessingStep = 4608;
                public const int SdkMessageRequest = 4609;
                public const int SdkMessageResponse = 4610;
                public const int SdkMessageResponseField = 4611;
                public const int SdkMessagePair = 4613;
                public const int SdkMessageRequestField = 4614;
                public const int SdkMessageProcessingStepImage = 4615;
                public const int SdkMessageProcessingStepSecureConfiguration = 4616;
                public const int ServiceEndpoint = 4618;
                public const int PlugInTraceLog = 4619;
                public const int SystemJob = 4700;
                public const int WorkflowWaitSubscription = 4702;
                public const int Process = 4703;
                public const int ProcessDependency = 4704;
                public const int ISVConfig = 4705;
                public const int ProcessLog = 4706;
                public const int ApplicationFile = 4707;
                public const int OrganizationStatistic = 4708;
                public const int SiteMap = 4709;
                public const int ProcessSession = 4710;
                public const int ExpanderEvent = 4711;
                public const int ProcessTrigger = 4712;
                public const int FlowSession = 4720;
                public const int ProcessStage = 4724;
                public const int BusinessProcessFlowInstance = 4725;
                public const int WebWizard = 4800;
                public const int WizardPage = 4802;
                public const int WebWizardAccessPrivilege = 4803;
                public const int TimeZoneDefinition = 4810;
                public const int TimeZoneRule = 4811;
                public const int TimeZoneLocalizedName = 4812;
                public const int RecentlyUsed = 5000;
                public const int NL2SQRegistrationInformation = 5004;
                public const int EventExpanderBreadcrumb = 5006;
                public const int SystemApplicationMetadata = 7000;
                public const int UserApplicationMetadata = 7001;
                public const int Solution = 7100;
                public const int Publisher = 7101;
                public const int PublisherAddress = 7102;
                public const int SolutionComponent = 7103;
                public const int SolutionComponentDefinition = 7104;
                public const int Dependency = 7105;
                public const int DependencyNode = 7106;
                public const int InvalidDependency = 7107;
                public const int DependencyFeature = 7108;
                public const int RuntimeDependency = 7200;
                public const int ElasticFileAttachment = 7755;
                public const int Post = 8000;
                public const int PostRole = 8001;
                public const int PostRegarding = 8002;
                public const int Follow = 8003;
                public const int Comment = 8005;
                public const int Like = 8006;
                public const int ACIViewMapper = 8040;
                public const int Trace = 8050;
                public const int TraceAssociation = 8051;
                public const int TraceRegarding = 8052;
                public const int RoutingRuleSet = 8181;
                public const int RuleItem = 8199;
                public const int AppModuleMetadata = 8700;
                public const int AppModuleMetadataDependency = 8701;
                public const int AppModuleMetadataAsyncOperation = 8702;
                public const int HierarchyRule = 8840;
                public const int ModelDrivenApp = 9006;
                public const int AppModuleComponent = 9007;
                public const int AppModuleRoles = 9009;
                public const int AppConfigMaster = 9011;
                public const int AppConfiguration = 9012;
                public const int AppConfigurationInstance = 9013;
                public const int Report = 9100;
                public const int ReportRelatedEntity = 9101;
                public const int ReportRelatedCategory = 9102;
                public const int ReportVisibility = 9103;
                public const int ReportLink = 9104;
                public const int Currency = 9105;
                public const int MailMergeTemplate = 9106;
                public const int ImportJob = 9107;
                public const int LocalConfigStore = 9201;
                public const int RecordCreationAndUpdateRule = 9300;
                public const int RecordCreationAndUpdateRuleItem = 9301;
                public const int WebResource = 9333;
                public const int ChannelAccessProfileRule = 9400;
                public const int ChannelAccessProfileRuleItem = 9401;
                public const int SharePointSite = 9502;
                public const int SharepointDocument = 9507;
                public const int DocumentLocation = 9508;
                public const int SharePointData = 9509;
                public const int RollupProperties = 9510;
                public const int RollupJob = 9511;
                public const int Goal = 9600;
                public const int RollupQuery = 9602;
                public const int GoalMetric = 9603;
                public const int RollupField = 9604;
                public const int EmailServerProfile = 9605;
                public const int Mailbox = 9606;
                public const int MailboxStatistics = 9607;
                public const int MailboxAutoTrackingFolder = 9608;
                public const int MailboxTrackingCategory = 9609;
                public const int ProcessConfiguration = 9650;
                public const int OrganizationInsightsNotification = 9690;
                public const int OrganizationInsightsMetric = 9699;
                public const int SLA = 9750;
                public const int SLAItem = 9751;
                public const int SLAKPIInstance = 9752;
                public const int CustomControl = 9753;
                public const int CustomControlResource = 9754;
                public const int CustomControlDefaultConfig = 9755;
                public const int Entity = 9800;
                public const int Attribute = 9808;
                public const int OptionSet = 9809;
                public const int EntityKey = 9810;
                public const int EntityRelationship = 9811;
                public const int ManagedProperty = 9812;
                public const int RelationshipEntity = 9813;
                public const int RelationshipAttribute = 9814;
                public const int EntityIndex = 9815;
                public const int IndexAttribute = 9816;
                public const int OptionSetValue = 9817;
                public const int SecuredMaskingColumn = 9820;
                public const int MobileOfflineProfile = 9866;
                public const int MobileOfflineProfileItem = 9867;
                public const int MobileOfflineProfileItemAssociation = 9868;
                public const int SyncError = 9869;
                public const int OfflineCommandDefinition = 9870;
                public const int LanguageProvisioningState = 9875;
                public const int RibbonMetadataToProcess = 9880;
                public const int SolutionHistoryData = 9890;
                public const int NavigationSetting = 9900;
                public const int MultiEntitySearch = 9910;
                public const int MultiSelectOptionValue = 9912;
                public const int HierarchySecurityConfiguration = 9919;
                public const int KnowledgeBaseRecord = 9930;
                public const int TimeStampDateMapping = 9932;
                public const int AzureServiceConnection = 9936;
                public const int DocumentTemplate = 9940;
                public const int PersonalDocumentTemplate = 9941;
                public const int TextAnalyticsEntityMapping = 9945;
                public const int KnowledgeSearchModel = 9947;
                public const int AdvancedSimilarityRule = 9949;
                public const int OfficeGraphDocument = 9950;
                public const int SimilarityRule = 9951;
                public const int KnowledgeArticle = 9953;
                public const int KnowledgeArticleViews = 9955;
                public const int Language = 9957;
                public const int Feedback = 9958;
                public const int Category = 9959;
                public const int KnowledgeArticleCategory = 9960;
                public const int DelveActionHub = 9961;
                public const int ActionCard = 9962;
                public const int ActionCardUserState = 9968;
                public const int ActionCardUserSettings = 9973;
                public const int ActionCardType = 9983;
                public const int InteractionForEmail = 9986;
                public const int ExternalPartyItem = 9987;
                public const int HolidayWrapper = 9996;
                public const int EmailSignature = 9997;
                public const int SolutionComponentAttributeConfiguration = 10000;
                public const int SolutionComponentBatchConfiguration = 10001;
                public const int SolutionComponentConfiguration = 10002;
                public const int SolutionComponentRelationshipConfiguration = 10003;
                public const int SolutionHistory = 10004;
                public const int SolutionHistoryDataSource = 10005;
                public const int ComponentLayer = 10006;
                public const int ComponentLayerDataSource = 10007;
                public const int Package = 10008;
                public const int PackageHistory = 10009;
                public const int StageSolutionUpload = 10011;
                public const int ExportSolutionUpload = 10012;
                public const int FeatureControlSetting = 10013;
                public const int SolutionComponentSummary = 10014;
                public const int SolutionComponentCountSummary = 10015;
                public const int SolutionComponentDataSource = 10016;
                public const int SolutionComponentCountDataSource = 10017;
                public const int MicrosoftEntraID = 10018;
                public const int StagedAttributeLookupValue = 10019;
                public const int StagedAttributePicklistValue = 10020;
                public const int StagedEntity = 10021;
                public const int StagedEntityAttribute = 10022;
                public const int StagedEntityRelationship = 10023;
                public const int StagedEntityRelationshipRelationships = 10024;
                public const int StagedEntityRelationshipRole = 10025;
                public const int StagedMetadataAsyncOperation = 10026;
                public const int StagedOptionset = 10027;
                public const int StagedRelationship = 10028;
                public const int StagedRelationship_ = 10029;
                public const int StagedRelationship__ = 10030;
                public const int AttributeClusterConfig = 10031;
                public const int EntityClusterConfiguration = 10032;
                public const int KeyVaultReference = 10033;
                public const int ManagedIdentity = 10034;
                public const int Catalog = 10035;
                public const int CatalogAssignment = 10036;
                public const int InternalCatalogAssignment = 10037;
                public const int CustomAPI = 10038;
                public const int CustomAPIRequestParameter = 10039;
                public const int CustomAPIResponseProperty = 10040;
                public const int PluginPackage = 10041;
                public const int SensitivityLabel = 10042;
                public const int NonRelationalDataSource = 10043;
                public const int ProvisionLanguageForUser = 10044;
                public const int PurviewLabelInfo = 10045;
                public const int PurviewLabelSyncCache = 10046;
                public const int SensitivityLabelAttributeMapping = 10047;
                public const int AppNotificationSignal = 10048;
                public const int SharedObject = 10049;
                public const int SharedWorkspace = 10050;
                public const int SharedWorkspaceAccessToken = 10051;
                public const int SharedWorkspacePool = 10052;
                public const int DataLakeFolder = 10053;
                public const int DataLakeFolderPermission = 10054;
                public const int DataLakeWorkspace = 10055;
                public const int DataLakeWorkspacePermission = 10056;
                public const int DataProcessingConfiguration = 10057;
                public const int ExportedExcel = 10058;
                public const int RetainedDataExcel = 10059;
                public const int SynapseDatabase = 10060;
                public const int AthenaReconciliationInfo = 10061;
                public const int SynapseLinkExternalTableState = 10062;
                public const int SynapseLinkProfile = 10063;
                public const int SynapseLinkProfileEntity = 10064;
                public const int SynapseLinkProfileEntityState = 10065;
                public const int SynapseLinkSchedule = 10066;
                public const int ComponentChangesetPayload = 10067;
                public const int ComponentChangesetVersion = 10068;
                public const int ComponentVersion = 10069;
                public const int ComponentVersionDataSource = 10070;
                public const int ComponentVersionInternal = 10071;
                public const int DataflowRefreshHistory = 10072;
                public const int EntityRefreshHistory = 10073;
                public const int SharedLinkSetting = 10074;
                public const int AnyPrivilegeEntity = 10075;
                public const int DelegatedAuthorization = 10076;
                public const int CascadeGrantRevokeAccessRecordsTracker = 10078;
                public const int CascadeGrantRevokeAccessVersionTracker = 10079;
                public const int RevokeInheritedAccessRecordsTracker = 10080;
                public const int TdsMetadata = 10081;
                public const int ModelDrivenAppElement = 10082;
                public const int ModelDrivenAppComponentNodeSEdge = 10083;
                public const int ModelDrivenAppComponentNode = 10084;
                public const int ModelDrivenAppSetting = 10085;
                public const int ModelDrivenAppUserSetting = 10086;
                public const int OrganizationSetting = 10087;
                public const int SettingDefinition = 10088;
                public const int CanvasAppExtendedMetadata = 10089;
                public const int ServicePlanMapping = 10090;
                public const int ServicePlanCustomControl = 10091;
                public const int ApplicationUser = 10093;
                public const int GitBranch = 10096;
                public const int GitConfigurationRetrievalDataSource = 10097;
                public const int GitHubAppConfig = 10098;
                public const int GitOrganization = 10099;
                public const int GitProject = 10100;
                public const int GitRepository = 10101;
                public const int GitSolution = 10102;
                public const int SourceControlBranchConfiguration = 10103;
                public const int SourceControlComponent = 10104;
                public const int SourceControlComponentPayload = 10105;
                public const int SourceControlConfiguration = 10106;
                public const int SourceControlOperationStatus = 10107;
                public const int StagedSourceControlComponent = 10108;
                public const int ODataV4DataSource = 10109;
                public const int WorkflowBinary = 10110;
                public const int FlowGroup = 10111;
                public const int BusinessProcess = 10112;
                public const int ComputerUseAgent = 10113;
                public const int Credential = 10114;
                public const int DesktopFlowModule = 10115;
                public const int FlowCapacityAssignment = 10116;
                public const int FlowCredentialApplication = 10117;
                public const int FlowEvent = 10118;
                public const int FlowMachine = 10119;
                public const int FlowMachineGroup = 10120;
                public const int FlowMachineImage = 10121;
                public const int FlowMachineImageVersion = 10122;
                public const int FlowMachineNetwork = 10123;
                public const int FlowSessionBinary = 10124;
                public const int FlowTestSession = 10125;
                public const int FlowTrigger = 10126;
                public const int FlowTriggerInstance = 10127;
                public const int ProcessStageParameter = 10128;
                public const int SavingRule = 10129;
                public const int Tag = 10130;
                public const int TaggedFlowSession = 10131;
                public const int TaggedProcess = 10132;
                public const int WorkflowMetadata = 10133;
                public const int WorkQueue = 10134;
                public const int WorkQueueItem = 10135;
                public const int DesktopFlowBinary = 10136;
                public const int FlowAggregation = 10137;
                public const int FlowLog = 10138;
                public const int FlowRun = 10139;
                public const int ApprovalProcess = 10140;
                public const int ApprovalStageApproval = 10141;
                public const int ApprovalStageCondition = 10142;
                public const int ApprovalStageIntelligent = 10143;
                public const int ApprovalStageOrder = 10144;
                public const int ActionApprovalModel = 10145;
                public const int Approval = 10146;
                public const int ApprovalRequest = 10147;
                public const int ApprovalResponse = 10148;
                public const int ApprovalStep = 10149;
                public const int AwaitAllActionApprovalModel = 10150;
                public const int AwaitAllApprovalModel = 10151;
                public const int BasicApprovalModelData = 10152;
                public const int FlowApproval = 10153;
                public const int ConnectionReference = 10162;
                public const int KnowledgeSourceConsumer = 10163;
                public const int KnowledgeSourceProfile = 10164;
                public const int UnstructuredFileSearchEntity = 10165;
                public const int UnstructuredFileSearchRecord = 10166;
                public const int UnstructuredFileSearchRecordStatus = 10167;
                public const int DVFileSearch = 10168;
                public const int DVFileSearchAttribute = 10169;
                public const int DVFileSearchEntity = 10170;
                public const int DVTableSearch = 10171;
                public const int DVTableSearchAttribute = 10172;
                public const int DVTableSearchEntity = 10173;
                public const int AICopilot = 10174;
                public const int AIPluginAuth = 10175;
                public const int AIPluginConversationStarter = 10176;
                public const int AIPluginConversationStarterMapping = 10177;
                public const int AIPluginGovernance = 10178;
                public const int AIPluginGovernanceExtended = 10179;
                public const int AIPluginOperationResponseTemplate = 10180;
                public const int AIPluginTitle = 10181;
                public const int SideloadedAIPlugin = 10182;
                public const int AIPlugin = 10183;
                public const int AIPluginExternalSchema = 10184;
                public const int AIPluginExternalSchemaProperty = 10185;
                public const int AIPluginInstance = 10186;
                public const int AIPluginOperation = 10187;
                public const int AIPluginOperationParameter = 10188;
                public const int AIPluginUserSetting = 10189;
                public const int AIConfigurationSearch = 10191;
                public const int DataProcessingEvent = 10192;
                public const int AIDocumentTemplate = 10193;
                public const int AIEvent = 10194;
                public const int AIModelCatalog = 10195;
                public const int AIBuilderFeedbackLoop = 10197;
                public const int AIFormProcessingDocument = 10198;
                public const int AIObjectDetectionImage = 10199;
                public const int AIObjectDetectionLabel = 10200;
                public const int AIObjectDetectionBoundingBox = 10201;
                public const int AIObjectDetectionImageMapping = 10202;
                public const int AIBuilderDataset = 10204;
                public const int AIBuilderDatasetFile = 10205;
                public const int AIBuilderDatasetRecord = 10206;
                public const int AIBuilderDatasetsContainer = 10207;
                public const int AIBuilderFile = 10208;
                public const int AIBuilderFileAttachedData = 10209;
                public const int AIEvaluationConfiguration = 10210;
                public const int AIEvaluationMetric = 10211;
                public const int AIEvaluationRun = 10212;
                public const int AIOptimization = 10213;
                public const int AIOptimizationPrivateData = 10214;
                public const int AITestCase = 10215;
                public const int AITestCaseDocument = 10216;
                public const int AITestCaseInput = 10217;
                public const int AITestRun = 10218;
                public const int AITestRunBatch = 10219;
                public const int HelpPage = 10220;
                public const int Tour = 10221;
                public const int BotContent = 10222;
                public const int ConversationTranscript = 10223;
                public const int Agent = 10224;
                public const int AgentComponent = 10225;
                public const int AgentComponentCollection = 10226;
                public const int Comment_ = 10237;
                public const int GovernanceConfiguration = 10238;
                public const int FabricAISkill = 10239;
                public const int AppInsightsMetadata = 10240;
                public const int DataflowConnectionReference = 10241;
                public const int Schedule = 10242;
                public const int DataflowTemplate = 10243;
                public const int DataflowDatalakeFolder = 10244;
                public const int DataMovementServiceRequest = 10245;
                public const int DataMovementServiceRequestStatus = 10246;
                public const int DMSSyncRequest = 10247;
                public const int DMSSyncStatus = 10248;
                public const int KnowledgeAssetConfiguration = 10249;
                public const int ModuleRunDetail = 10250;
                public const int QnA = 10251;
                public const int SalesforceStructuredObject = 10252;
                public const int SalesforceStructuredQnAConfig = 10253;
                public const int WorkflowActionStatus = 10254;
                public const int AllowedMCPClient = 10255;
                public const int FederatedKnowledgeCitation = 10256;
                public const int FederatedKnowledgeConfiguration = 10257;
                public const int FederatedKnowledgeEntityConfiguration = 10258;
                public const int FederatedKnowledgeMetadataRefresh = 10259;
                public const int IntelligentMemory = 10260;
                public const int KnowledgeFAQ = 10261;
                public const int FormMapping = 10262;
                public const int CopilotInteractions = 10263;
                public const int PDFSetting = 10264;
                public const int ActivityFileAttachment = 10265;
                public const int TeamsChat = 10266;
                public const int ServiceConfiguration = 10267;
                public const int SLAKPI = 10268;
                public const int IntegratedSearchProvider = 10269;
                public const int KnowledgeManagementSetting = 10270;
                public const int KnowledgeFederatedArticle = 10271;
                public const int KnowledgeFederatedArticleIncident = 10272;
                public const int SearchProvider = 10273;
                public const int KnowledgeArticleImage = 10274;
                public const int KnowledgeConfiguration = 10275;
                public const int KnowledgeInteractionInsight = 10276;
                public const int KnowledgeSearchInsight = 10277;
                public const int FavoriteKnowledgeArticle = 10278;
                public const int KnowledgeArticleLanguageSetting = 10279;
                public const int KnowledgeArticleAttachment = 10280;
                public const int KnowledgePersonalization = 10281;
                public const int KnowledgeArticleTemplate = 10282;
                public const int KnowledgeSearchPersonalFilterConfig = 10283;
                public const int KnowledgeSearchFilter = 10284;
                public const int BulkHarvestRunLog = 10286;
                public const int HarvestEligibilityCondition = 10287;
                public const int HarvestWorkItem = 10288;
                public const int MsdynHistoricalcaseharvestbatch = 10289;
                public const int MsdynHistoricalcaseharvestrun = 10290;
                public const int InterimUpdateKnowledgeArticle = 10291;
                public const int KnowledgeArticleCustomEntity = 10292;
                public const int KnowledgeHarvestJobRecord = 10293;
                public const int KnowledgeHarvestPlan = 10294;
                public const int SupportUserTable = 10295;
                public const int FxExpression = 10296;
                public const int Function = 10297;
                public const int PlugIn = 10298;
                public const int PowerfxRule = 10299;
                public const int PlannerBusinessScenario = 10300;
                public const int PlannerSyncAction = 10301;
                public const int AgentRule = 10302;
                public const int MCPPrompt = 10303;
                public const int MCPResource = 10304;
                public const int MCPResourceContent = 10305;
                public const int MCPServer = 10306;
                public const int MCPTool = 10307;
                public const int ToolingGateway = 10308;
                public const int ToolingGatewayMCPServer = 10309;
                public const int EmailAddressConfiguration = 10310;
                public const int MsGraphResourceToSubscription = 10311;
                public const int VirtualEntityMetadata = 10312;
                public const int BackgroundOperation = 10313;
                public const int ReportParameter = 10314;
                public const int MobileOfflineProfileExtension = 10315;
                public const int MobileOfflineProfileItemFilter = 10316;
                public const int TeamMobileOfflineProfileMembership = 10317;
                public const int UserMobileOfflineProfileMembership = 10318;
                public const int OrganizationDataSyncSubscription = 10319;
                public const int OrganizationDataSyncSubscriptionEntity = 10320;
                public const int OrganizationDataSyncSubscriptionFnoTable = 10321;
                public const int OrganizationDataSyncFnoState = 10322;
                public const int OrganizationDataSyncState = 10323;
                public const int ArchiveCleanupInfo = 10324;
                public const int ArchiveCleanupOperation = 10325;
                public const int BulkArchiveConfig = 10326;
                public const int BulkArchiveFailureDetail = 10327;
                public const int BulkArchiveOperation = 10328;
                public const int BulkArchiveOperationDetail = 10329;
                public const int EnableArchivalRequest = 10330;
                public const int MetadataForArchival = 10331;
                public const int ReconciliationEntityInfo = 10332;
                public const int ReconciliationEntityStepInfo = 10333;
                public const int ReconciliationInfo = 10334;
                public const int RetentionCleanupInfo = 10335;
                public const int RetentionCleanupOperation = 10336;
                public const int DataLifeCycleConfig = 10337;
                public const int RetentionFailureDetail = 10338;
                public const int RetentionOperation = 10339;
                public const int RetentionOperationDetail = 10340;
                public const int RetentionSuccessDetail = 10341;
                public const int CertificateCredential = 10342;
                public const int Notification_ = 10343;
                public const int UserRating = 10344;
                public const int MobileApp = 10345;
                public const int PowerAppsWrapBuild = 10346;
                public const int InsightsStoreDataSource = 10347;
                public const int InsightsStoreVirtualEntity = 10348;
                public const int RoleEditorLayout = 10349;
                public const int DeletedRecordReference = 10350;
                public const int RestoreDeletedRecordsConfiguration = 10351;
                public const int AppAction = 10352;
                public const int AppActionMigration = 10353;
                public const int AppActionRule = 10354;
                public const int Card = 10357;
                public const int CardStateItem = 10358;
                public const int EntityLinkChatConfiguration = 10361;
                public const int AgentFeedItem = 10362;
                public const int AgentHubGoal = 10363;
                public const int AgentHubInsight = 10364;
                public const int AgentHubMetric = 10365;
                public const int AgenticScenario = 10366;
                public const int AgentMemory = 10367;
                public const int AgentTask = 10368;
                public const int SharePointManagedIdentity = 10369;
                public const int AIInsightCard = 10370;
                public const int AISkillConfig = 10371;
                public const int SuggestedAction = 10372;
                public const int SuggestedActionCriteria = 10373;
                public const int DataWorkspace = 10374;
                public const int Plan = 10375;
                public const int PlanArtifact = 10376;
                public const int PlanAttachment = 10377;
                public const int UXAgentComponent = 10378;
                public const int UXAgentComponentRevision = 10379;
                public const int UXAgentProject = 10380;
                public const int UXAgentProjectFile = 10381;
                public const int AgentConversationMessage = 10382;
                public const int AgentConversationMessageFile = 10383;
                public const int RichTextAttachment = 10384;
                public const int StructuredTemplate = 10385;
                public const int RTEStructuredTemplateConfig = 10386;
                public const int RTETemplateMapping = 10387;
                public const int CustomControlExtendedSetting = 10388;
                public const int TimelinePin = 10389;
                public const int VirtualConnectorDataSource = 10390;
                public const int VirtualTableColumnCandidate = 10391;
                public const int PMAnalysisHistory = 10393;
                public const int PMBusinessRuleAutomationConfig = 10394;
                public const int PMCalendar = 10395;
                public const int PMCalendarVersion = 10396;
                public const int PMInferredTask = 10397;
                public const int PMProcessExtendedMetadataVersion = 10398;
                public const int PMProcessTemplate = 10399;
                public const int PMProcessUserSettings = 10400;
                public const int PMProcessVersion = 10401;
                public const int PMRecording = 10402;
                public const int PMSimulation = 10403;
                public const int PMTab = 10404;
                public const int PMTemplate = 10405;
                public const int PMView = 10406;
                public const int AnalysisComponent = 10407;
                public const int AnalysisJob = 10408;
                public const int AnalysisOverride = 10409;
                public const int AnalysisResult = 10410;
                public const int AnalysisResultDetail = 10411;
                public const int SolutionHealthRule = 10412;
                public const int SolutionHealthRuleArgument = 10413;
                public const int SolutionHealthRuleSet = 10414;
                public const int FileUpload = 10415;
                public const int AppEntitySearchView = 10416;
                public const int MainFewShot = 10417;
                public const int MakerFewShot = 10418;
                public const int SearchAttributeSettings = 10419;
                public const int SearchCustomAnalyzer = 10420;
                public const int SearchRelationshipSettings = 10421;
                public const int SearchResultsCache = 10422;
                public const int SearchTelemetry = 10423;
                public const int TextDataRecordsIndexingStatus = 10424;
                public const int ViewAsExampleQuestion = 10425;
                public const int CopilotExampleQuestion = 10426;
                public const int CopilotGlossaryTerm = 10427;
                public const int CopilotSynonyms = 10428;
                public const int BusinessSkill = 10429;
                public const int BusinessSkillResource = 10430;
                public const int SiteComponent = 10432;
                public const int Site = 10433;
                public const int SiteLanguage = 10434;
                public const int PowerPagesSitePublished = 10435;
                public const int SiteSourceFile = 10436;
                public const int ExternalIdentity = 10439;
                public const int Invitation = 10440;
                public const int InviteRedemption = 10441;
                public const int PortalComment = 10442;
                public const int Setting = 10443;
                public const int MultistepFormSession = 10444;
                public const int AdPlacement = 10448;
                public const int ColumnPermission = 10449;
                public const int ColumnPermissionProfile = 10450;
                public const int ContentSnippet = 10451;
                public const int BasicForm = 10452;
                public const int BasicFormMetadata = 10453;
                public const int List = 10454;
                public const int TablePermission = 10455;
                public const int PageTemplate = 10456;
                public const int PollPlacement = 10457;
                public const int PowerPagesCoreEntityDS = 10458;
                public const int PublishingState = 10459;
                public const int PublishingStateTransitionRule = 10460;
                public const int Redirect = 10461;
                public const int Shortcut = 10462;
                public const int SiteMarker = 10463;
                public const int SiteSetting = 10464;
                public const int WebFile = 10465;
                public const int MultistepForm = 10466;
                public const int MultistepFormMetadata = 10467;
                public const int FormStep = 10468;
                public const int WebLink = 10469;
                public const int WebLinkSet = 10470;
                public const int WebPage = 10471;
                public const int WebPageAccessControlRule = 10472;
                public const int WebRole = 10473;
                public const int Website = 10474;
                public const int WebsiteAccess = 10475;
                public const int WebsiteLanguage = 10476;
                public const int WebTemplate = 10477;
                public const int PowerPagesScanReport = 10484;
                public const int PowerPagesDDOSAlert = 10485;
                public const int PowerPagesLog = 10486;
                public const int PowerPagesManagedIdentity = 10487;
                public const int PowerPagesSiteAIFeedback = 10488;
                public const int CatalogSubmissionFiles = 10494;
                public const int PackageSubmissionStore = 10495;
                public const int Indexedtrait = 10496;
                public const int ProcessorRegistration = 10497;
                public const int Signal = 10498;
                public const int SignalRegistration = 10499;
                public const int Trait = 10500;
                public const int TraitRegistration = 10501;
                public const int EventAggregatorScans = 10559;
                public const int Cleanup = 10560;
                public const int EventAggregator = 10561;
                public const int OnlineShopperIntention = 10562;
                public const int GaurdianFullscan = 10563;
                public const int GaurdianHealthchecks = 10564;
                public const int HealthcareFeedback = 10565;
                public const int ObjectDetectionProduct = 10566;
            }
            public struct ComponentState
            {
                public const int Published = 0;
                public const int Unpublished = 1;
                public const int Deleted = 2;
                public const int DeletedUnpublished = 3;
            }
            public struct ExcludeInactiveRecords
            {
                public const bool False = false;
                public const bool True = true;
            }
            public struct IsCaseSensitive
            {
                public const bool False = false;
                public const bool True = true;
            }
            public struct IsManaged
            {
                public const bool Unmanaged = false;
                public const bool Managed = true;
            }
            public struct MatchingEntityTypeCode
            {
                public const int Account = 1;
                public const int Contact = 2;
                public const int Note = 5;
                public const int BusinessUnitMap = 6;
                public const int Owner = 7;
                public const int User = 8;
                public const int Team = 9;
                public const int BusinessUnit = 10;
                public const int SystemUserPrincipal = 14;
                public const int Subscription = 29;
                public const int FilterTemplate = 30;
                public const int PrivilegeObjectTypeCode = 31;
                public const int SubscriptionSynchronizationInformation = 33;
                public const int TrackingInformationForDeletedEntities = 35;
                public const int ClientUpdate = 36;
                public const int SubscriptionManuallyTrackedObject = 37;
                public const int SystemUserBusinessUnitEntityMap = 42;
                public const int FieldSharing = 44;
                public const int SubscriptionStatisticOffline = 45;
                public const int SubscriptionStatisticOutlook = 46;
                public const int SubscriptionSyncEntryOffline = 47;
                public const int SubscriptionSyncEntryOutlook = 48;
                public const int Position = 50;
                public const int SystemUserManagerMap = 51;
                public const int UserSearchFacet = 52;
                public const int GlobalSearchConfiguration = 54;
                public const int FileAttachment = 55;
                public const int SystemUserAuthorizationChangeTracker = 60;
                public const int RecordFilter = 72;
                public const int EntityRecordFilter = 73;
                public const int SecuredMaskingRule = 74;
                public const int PrivilegeCheckerRun = 75;
                public const int PrivilegeCheckerLog = 76;
                public const int VirtualEntityDataProvider = 78;
                public const int VirtualEntityDataSource = 85;
                public const int TeamTemplate = 92;
                public const int SocialProfile = 99;
                public const int ServicePlan = 101;
                public const int PrivilegesRemovalSetting = 103;
                public const int IndexedArticle = 126;
                public const int Article = 127;
                public const int Subject = 129;
                public const int Announcement = 132;
                public const int ActivityParty = 135;
                public const int UserSettings = 150;
                public const int CanvasApp = 300;
                public const int CallbackRegistration = 301;
                public const int Connector = 372;
                public const int ConnectionInstance = 373;
                public const int EnvironmentVariableDefinition = 380;
                public const int EnvironmentVariableValue = 381;
                public const int AITemplate = 400;
                public const int AIModel = 401;
                public const int AIConfiguration = 402;
                public const int Dataflow = 418;
                public const int EntityAnalyticsConfig = 430;
                public const int ImageAttributeConfiguration = 431;
                public const int EntityImageConfiguration = 432;
                public const int NewProcess = 950;
                public const int TranslationProcess = 951;
                public const int ExpiredProcess = 955;
                public const int Attachment = 1001;
                public const int Attachment_ = 1002;
                public const int InternalAddress = 1003;
                public const int ImageDescriptor = 1007;
                public const int ArticleTemplate = 1016;
                public const int Organization = 1019;
                public const int OrganizationUI = 1021;
                public const int Privilege = 1023;
                public const int SystemForm = 1030;
                public const int UserDashboard = 1031;
                public const int SecurityRole = 1036;
                public const int RoleTemplate = 1037;
                public const int View = 1039;
                public const int StringMap = 1043;
                public const int Address = 1071;
                public const int SubscriptionClients = 1072;
                public const int StatusMap = 1075;
                public const int ArticleComment = 1082;
                public const int UserFiscalCalendar = 1086;
                public const int AuthorizationServer = 1094;
                public const int PartnerApplication = 1095;
                public const int SystemChart = 1111;
                public const int UserChart = 1112;
                public const int RibbonTabToCommandMapping = 1113;
                public const int RibbonContextGroup = 1115;
                public const int RibbonCommand = 1116;
                public const int RibbonRule = 1117;
                public const int ApplicationRibbons = 1120;
                public const int RibbonDifference = 1130;
                public const int ReplicationBacklog = 1140;
                public const int DocumentSuggestions = 1189;
                public const int SuggestionCardTemplate = 1190;
                public const int FieldSecurityProfile = 1200;
                public const int FieldPermission = 1201;
                public const int TeamProfiles = 1203;
                public const int Application = 1204;
                public const int ChannelPropertyGroup = 1234;
                public const int ChannelProperty = 1236;
                public const int SocialInsightsConfiguration = 1300;
                public const int SavedOrganizationInsightsConfiguration = 1309;
                public const int SyncAttributeMappingProfile = 1400;
                public const int SyncAttributeMapping = 1401;
                public const int TeamSyncAttributeMappingProfiles = 1403;
                public const int PrincipalSyncAttributeMap = 1404;
                public const int AnnualFiscalCalendar = 2000;
                public const int SemiannualFiscalCalendar = 2001;
                public const int QuarterlyFiscalCalendar = 2002;
                public const int MonthlyFiscalCalendar = 2003;
                public const int FixedMonthlyFiscalCalendar = 2004;
                public const int EmailTemplate = 2010;
                public const int UnresolvedAddress = 2012;
                public const int Territory = 2013;
                public const int Theme = 2015;
                public const int UserMapping = 2016;
                public const int Queue = 2020;
                public const int QueueItemCount = 2023;
                public const int QueueMemberCount = 2024;
                public const int License = 2027;
                public const int QueueItem = 2029;
                public const int UserEntityUISettings = 2500;
                public const int UserEntityInstanceData = 2501;
                public const int IntegrationStatus = 3000;
                public const int ChannelAccessProfile = 3005;
                public const int ExternalParty = 3008;
                public const int ConnectionRole = 3231;
                public const int ConnectionRoleObjectTypeCode = 3233;
                public const int Connection = 3234;
                public const int Calendar = 4003;
                public const int CalendarRule = 4004;
                public const int InterProcessLock = 4011;
                public const int EmailHash = 4023;
                public const int DisplayStringMap = 4101;
                public const int DisplayString = 4102;
                public const int Notification = 4110;
                public const int ExchangeSyncIdMapping = 4120;
                public const int Activity = 4200;
                public const int Appointment = 4201;
                public const int Email = 4202;
                public const int Fax = 4204;
                public const int Letter = 4207;
                public const int PhoneCall = 4210;
                public const int Task = 4212;
                public const int SocialActivity = 4216;
                public const int UntrackedEmail = 4220;
                public const int SavedView = 4230;
                public const int MetadataDifference = 4231;
                public const int BusinessDataLocalizedLabel = 4232;
                public const int RecurrenceRule = 4250;
                public const int RecurringAppointment = 4251;
                public const int EmailSearch = 4299;
                public const int DataImport = 4410;
                public const int DataMap = 4411;
                public const int ImportSourceFile = 4412;
                public const int ImportData = 4413;
                public const int DuplicateDetectionRule = 4414;
                public const int DuplicateRecord = 4415;
                public const int DuplicateRuleCondition = 4416;
                public const int ColumnMapping = 4417;
                public const int ListValueMapping = 4418;
                public const int LookupMapping = 4419;
                public const int OwnerMapping = 4420;
                public const int ImportLog = 4423;
                public const int BulkDeleteOperation = 4424;
                public const int BulkDeleteFailure = 4425;
                public const int TransformationMapping = 4426;
                public const int TransformationParameterMapping = 4427;
                public const int ImportEntityMapping = 4428;
                public const int DataPerformanceDashboard = 4450;
                public const int OfficeDocument = 4490;
                public const int RelationshipRole = 4500;
                public const int RelationshipRoleMap = 4501;
                public const int CustomerRelationship = 4502;
                public const int Auditing = 4567;
                public const int RibbonClientMetadata = 4579;
                public const int EntityMap = 4600;
                public const int AttributeMap = 4601;
                public const int PlugInType = 4602;
                public const int PlugInTypeStatistic = 4603;
                public const int PlugInAssembly = 4605;
                public const int SdkMessage = 4606;
                public const int SdkMessageFilter = 4607;
                public const int SdkMessageProcessingStep = 4608;
                public const int SdkMessageRequest = 4609;
                public const int SdkMessageResponse = 4610;
                public const int SdkMessageResponseField = 4611;
                public const int SdkMessagePair = 4613;
                public const int SdkMessageRequestField = 4614;
                public const int SdkMessageProcessingStepImage = 4615;
                public const int SdkMessageProcessingStepSecureConfiguration = 4616;
                public const int ServiceEndpoint = 4618;
                public const int PlugInTraceLog = 4619;
                public const int SystemJob = 4700;
                public const int WorkflowWaitSubscription = 4702;
                public const int Process = 4703;
                public const int ProcessDependency = 4704;
                public const int ISVConfig = 4705;
                public const int ProcessLog = 4706;
                public const int ApplicationFile = 4707;
                public const int OrganizationStatistic = 4708;
                public const int SiteMap = 4709;
                public const int ProcessSession = 4710;
                public const int ExpanderEvent = 4711;
                public const int ProcessTrigger = 4712;
                public const int FlowSession = 4720;
                public const int ProcessStage = 4724;
                public const int BusinessProcessFlowInstance = 4725;
                public const int WebWizard = 4800;
                public const int WizardPage = 4802;
                public const int WebWizardAccessPrivilege = 4803;
                public const int TimeZoneDefinition = 4810;
                public const int TimeZoneRule = 4811;
                public const int TimeZoneLocalizedName = 4812;
                public const int RecentlyUsed = 5000;
                public const int NL2SQRegistrationInformation = 5004;
                public const int EventExpanderBreadcrumb = 5006;
                public const int SystemApplicationMetadata = 7000;
                public const int UserApplicationMetadata = 7001;
                public const int Solution = 7100;
                public const int Publisher = 7101;
                public const int PublisherAddress = 7102;
                public const int SolutionComponent = 7103;
                public const int SolutionComponentDefinition = 7104;
                public const int Dependency = 7105;
                public const int DependencyNode = 7106;
                public const int InvalidDependency = 7107;
                public const int DependencyFeature = 7108;
                public const int RuntimeDependency = 7200;
                public const int ElasticFileAttachment = 7755;
                public const int Post = 8000;
                public const int PostRole = 8001;
                public const int PostRegarding = 8002;
                public const int Follow = 8003;
                public const int Comment = 8005;
                public const int Like = 8006;
                public const int ACIViewMapper = 8040;
                public const int Trace = 8050;
                public const int TraceAssociation = 8051;
                public const int TraceRegarding = 8052;
                public const int RoutingRuleSet = 8181;
                public const int RuleItem = 8199;
                public const int AppModuleMetadata = 8700;
                public const int AppModuleMetadataDependency = 8701;
                public const int AppModuleMetadataAsyncOperation = 8702;
                public const int HierarchyRule = 8840;
                public const int ModelDrivenApp = 9006;
                public const int AppModuleComponent = 9007;
                public const int AppModuleRoles = 9009;
                public const int AppConfigMaster = 9011;
                public const int AppConfiguration = 9012;
                public const int AppConfigurationInstance = 9013;
                public const int Report = 9100;
                public const int ReportRelatedEntity = 9101;
                public const int ReportRelatedCategory = 9102;
                public const int ReportVisibility = 9103;
                public const int ReportLink = 9104;
                public const int Currency = 9105;
                public const int MailMergeTemplate = 9106;
                public const int ImportJob = 9107;
                public const int LocalConfigStore = 9201;
                public const int RecordCreationAndUpdateRule = 9300;
                public const int RecordCreationAndUpdateRuleItem = 9301;
                public const int WebResource = 9333;
                public const int ChannelAccessProfileRule = 9400;
                public const int ChannelAccessProfileRuleItem = 9401;
                public const int SharePointSite = 9502;
                public const int SharepointDocument = 9507;
                public const int DocumentLocation = 9508;
                public const int SharePointData = 9509;
                public const int RollupProperties = 9510;
                public const int RollupJob = 9511;
                public const int Goal = 9600;
                public const int RollupQuery = 9602;
                public const int GoalMetric = 9603;
                public const int RollupField = 9604;
                public const int EmailServerProfile = 9605;
                public const int Mailbox = 9606;
                public const int MailboxStatistics = 9607;
                public const int MailboxAutoTrackingFolder = 9608;
                public const int MailboxTrackingCategory = 9609;
                public const int ProcessConfiguration = 9650;
                public const int OrganizationInsightsNotification = 9690;
                public const int OrganizationInsightsMetric = 9699;
                public const int SLA = 9750;
                public const int SLAItem = 9751;
                public const int SLAKPIInstance = 9752;
                public const int CustomControl = 9753;
                public const int CustomControlResource = 9754;
                public const int CustomControlDefaultConfig = 9755;
                public const int Entity = 9800;
                public const int Attribute = 9808;
                public const int OptionSet = 9809;
                public const int EntityKey = 9810;
                public const int EntityRelationship = 9811;
                public const int ManagedProperty = 9812;
                public const int RelationshipEntity = 9813;
                public const int RelationshipAttribute = 9814;
                public const int EntityIndex = 9815;
                public const int IndexAttribute = 9816;
                public const int OptionSetValue = 9817;
                public const int SecuredMaskingColumn = 9820;
                public const int MobileOfflineProfile = 9866;
                public const int MobileOfflineProfileItem = 9867;
                public const int MobileOfflineProfileItemAssociation = 9868;
                public const int SyncError = 9869;
                public const int OfflineCommandDefinition = 9870;
                public const int LanguageProvisioningState = 9875;
                public const int RibbonMetadataToProcess = 9880;
                public const int SolutionHistoryData = 9890;
                public const int NavigationSetting = 9900;
                public const int MultiEntitySearch = 9910;
                public const int MultiSelectOptionValue = 9912;
                public const int HierarchySecurityConfiguration = 9919;
                public const int KnowledgeBaseRecord = 9930;
                public const int TimeStampDateMapping = 9932;
                public const int AzureServiceConnection = 9936;
                public const int DocumentTemplate = 9940;
                public const int PersonalDocumentTemplate = 9941;
                public const int TextAnalyticsEntityMapping = 9945;
                public const int KnowledgeSearchModel = 9947;
                public const int AdvancedSimilarityRule = 9949;
                public const int OfficeGraphDocument = 9950;
                public const int SimilarityRule = 9951;
                public const int KnowledgeArticle = 9953;
                public const int KnowledgeArticleViews = 9955;
                public const int Language = 9957;
                public const int Feedback = 9958;
                public const int Category = 9959;
                public const int KnowledgeArticleCategory = 9960;
                public const int DelveActionHub = 9961;
                public const int ActionCard = 9962;
                public const int ActionCardUserState = 9968;
                public const int ActionCardUserSettings = 9973;
                public const int ActionCardType = 9983;
                public const int InteractionForEmail = 9986;
                public const int ExternalPartyItem = 9987;
                public const int HolidayWrapper = 9996;
                public const int EmailSignature = 9997;
                public const int SolutionComponentAttributeConfiguration = 10000;
                public const int SolutionComponentBatchConfiguration = 10001;
                public const int SolutionComponentConfiguration = 10002;
                public const int SolutionComponentRelationshipConfiguration = 10003;
                public const int SolutionHistory = 10004;
                public const int SolutionHistoryDataSource = 10005;
                public const int ComponentLayer = 10006;
                public const int ComponentLayerDataSource = 10007;
                public const int Package = 10008;
                public const int PackageHistory = 10009;
                public const int StageSolutionUpload = 10011;
                public const int ExportSolutionUpload = 10012;
                public const int FeatureControlSetting = 10013;
                public const int SolutionComponentSummary = 10014;
                public const int SolutionComponentCountSummary = 10015;
                public const int SolutionComponentDataSource = 10016;
                public const int SolutionComponentCountDataSource = 10017;
                public const int MicrosoftEntraID = 10018;
                public const int StagedAttributeLookupValue = 10019;
                public const int StagedAttributePicklistValue = 10020;
                public const int StagedEntity = 10021;
                public const int StagedEntityAttribute = 10022;
                public const int StagedEntityRelationship = 10023;
                public const int StagedEntityRelationshipRelationships = 10024;
                public const int StagedEntityRelationshipRole = 10025;
                public const int StagedMetadataAsyncOperation = 10026;
                public const int StagedOptionset = 10027;
                public const int StagedRelationship = 10028;
                public const int StagedRelationship_ = 10029;
                public const int StagedRelationship__ = 10030;
                public const int AttributeClusterConfig = 10031;
                public const int EntityClusterConfiguration = 10032;
                public const int KeyVaultReference = 10033;
                public const int ManagedIdentity = 10034;
                public const int Catalog = 10035;
                public const int CatalogAssignment = 10036;
                public const int InternalCatalogAssignment = 10037;
                public const int CustomAPI = 10038;
                public const int CustomAPIRequestParameter = 10039;
                public const int CustomAPIResponseProperty = 10040;
                public const int PluginPackage = 10041;
                public const int SensitivityLabel = 10042;
                public const int NonRelationalDataSource = 10043;
                public const int ProvisionLanguageForUser = 10044;
                public const int PurviewLabelInfo = 10045;
                public const int PurviewLabelSyncCache = 10046;
                public const int SensitivityLabelAttributeMapping = 10047;
                public const int AppNotificationSignal = 10048;
                public const int SharedObject = 10049;
                public const int SharedWorkspace = 10050;
                public const int SharedWorkspaceAccessToken = 10051;
                public const int SharedWorkspacePool = 10052;
                public const int DataLakeFolder = 10053;
                public const int DataLakeFolderPermission = 10054;
                public const int DataLakeWorkspace = 10055;
                public const int DataLakeWorkspacePermission = 10056;
                public const int DataProcessingConfiguration = 10057;
                public const int ExportedExcel = 10058;
                public const int RetainedDataExcel = 10059;
                public const int SynapseDatabase = 10060;
                public const int AthenaReconciliationInfo = 10061;
                public const int SynapseLinkExternalTableState = 10062;
                public const int SynapseLinkProfile = 10063;
                public const int SynapseLinkProfileEntity = 10064;
                public const int SynapseLinkProfileEntityState = 10065;
                public const int SynapseLinkSchedule = 10066;
                public const int ComponentChangesetPayload = 10067;
                public const int ComponentChangesetVersion = 10068;
                public const int ComponentVersion = 10069;
                public const int ComponentVersionDataSource = 10070;
                public const int ComponentVersionInternal = 10071;
                public const int DataflowRefreshHistory = 10072;
                public const int EntityRefreshHistory = 10073;
                public const int SharedLinkSetting = 10074;
                public const int AnyPrivilegeEntity = 10075;
                public const int DelegatedAuthorization = 10076;
                public const int CascadeGrantRevokeAccessRecordsTracker = 10078;
                public const int CascadeGrantRevokeAccessVersionTracker = 10079;
                public const int RevokeInheritedAccessRecordsTracker = 10080;
                public const int TdsMetadata = 10081;
                public const int ModelDrivenAppElement = 10082;
                public const int ModelDrivenAppComponentNodeSEdge = 10083;
                public const int ModelDrivenAppComponentNode = 10084;
                public const int ModelDrivenAppSetting = 10085;
                public const int ModelDrivenAppUserSetting = 10086;
                public const int OrganizationSetting = 10087;
                public const int SettingDefinition = 10088;
                public const int CanvasAppExtendedMetadata = 10089;
                public const int ServicePlanMapping = 10090;
                public const int ServicePlanCustomControl = 10091;
                public const int ApplicationUser = 10093;
                public const int GitBranch = 10096;
                public const int GitConfigurationRetrievalDataSource = 10097;
                public const int GitHubAppConfig = 10098;
                public const int GitOrganization = 10099;
                public const int GitProject = 10100;
                public const int GitRepository = 10101;
                public const int GitSolution = 10102;
                public const int SourceControlBranchConfiguration = 10103;
                public const int SourceControlComponent = 10104;
                public const int SourceControlComponentPayload = 10105;
                public const int SourceControlConfiguration = 10106;
                public const int SourceControlOperationStatus = 10107;
                public const int StagedSourceControlComponent = 10108;
                public const int ODataV4DataSource = 10109;
                public const int WorkflowBinary = 10110;
                public const int FlowGroup = 10111;
                public const int BusinessProcess = 10112;
                public const int ComputerUseAgent = 10113;
                public const int Credential = 10114;
                public const int DesktopFlowModule = 10115;
                public const int FlowCapacityAssignment = 10116;
                public const int FlowCredentialApplication = 10117;
                public const int FlowEvent = 10118;
                public const int FlowMachine = 10119;
                public const int FlowMachineGroup = 10120;
                public const int FlowMachineImage = 10121;
                public const int FlowMachineImageVersion = 10122;
                public const int FlowMachineNetwork = 10123;
                public const int FlowSessionBinary = 10124;
                public const int FlowTestSession = 10125;
                public const int FlowTrigger = 10126;
                public const int FlowTriggerInstance = 10127;
                public const int ProcessStageParameter = 10128;
                public const int SavingRule = 10129;
                public const int Tag = 10130;
                public const int TaggedFlowSession = 10131;
                public const int TaggedProcess = 10132;
                public const int WorkflowMetadata = 10133;
                public const int WorkQueue = 10134;
                public const int WorkQueueItem = 10135;
                public const int DesktopFlowBinary = 10136;
                public const int FlowAggregation = 10137;
                public const int FlowLog = 10138;
                public const int FlowRun = 10139;
                public const int ApprovalProcess = 10140;
                public const int ApprovalStageApproval = 10141;
                public const int ApprovalStageCondition = 10142;
                public const int ApprovalStageIntelligent = 10143;
                public const int ApprovalStageOrder = 10144;
                public const int ActionApprovalModel = 10145;
                public const int Approval = 10146;
                public const int ApprovalRequest = 10147;
                public const int ApprovalResponse = 10148;
                public const int ApprovalStep = 10149;
                public const int AwaitAllActionApprovalModel = 10150;
                public const int AwaitAllApprovalModel = 10151;
                public const int BasicApprovalModelData = 10152;
                public const int FlowApproval = 10153;
                public const int ConnectionReference = 10162;
                public const int KnowledgeSourceConsumer = 10163;
                public const int KnowledgeSourceProfile = 10164;
                public const int UnstructuredFileSearchEntity = 10165;
                public const int UnstructuredFileSearchRecord = 10166;
                public const int UnstructuredFileSearchRecordStatus = 10167;
                public const int DVFileSearch = 10168;
                public const int DVFileSearchAttribute = 10169;
                public const int DVFileSearchEntity = 10170;
                public const int DVTableSearch = 10171;
                public const int DVTableSearchAttribute = 10172;
                public const int DVTableSearchEntity = 10173;
                public const int AICopilot = 10174;
                public const int AIPluginAuth = 10175;
                public const int AIPluginConversationStarter = 10176;
                public const int AIPluginConversationStarterMapping = 10177;
                public const int AIPluginGovernance = 10178;
                public const int AIPluginGovernanceExtended = 10179;
                public const int AIPluginOperationResponseTemplate = 10180;
                public const int AIPluginTitle = 10181;
                public const int SideloadedAIPlugin = 10182;
                public const int AIPlugin = 10183;
                public const int AIPluginExternalSchema = 10184;
                public const int AIPluginExternalSchemaProperty = 10185;
                public const int AIPluginInstance = 10186;
                public const int AIPluginOperation = 10187;
                public const int AIPluginOperationParameter = 10188;
                public const int AIPluginUserSetting = 10189;
                public const int AIConfigurationSearch = 10191;
                public const int DataProcessingEvent = 10192;
                public const int AIDocumentTemplate = 10193;
                public const int AIEvent = 10194;
                public const int AIModelCatalog = 10195;
                public const int AIBuilderFeedbackLoop = 10197;
                public const int AIFormProcessingDocument = 10198;
                public const int AIObjectDetectionImage = 10199;
                public const int AIObjectDetectionLabel = 10200;
                public const int AIObjectDetectionBoundingBox = 10201;
                public const int AIObjectDetectionImageMapping = 10202;
                public const int AIBuilderDataset = 10204;
                public const int AIBuilderDatasetFile = 10205;
                public const int AIBuilderDatasetRecord = 10206;
                public const int AIBuilderDatasetsContainer = 10207;
                public const int AIBuilderFile = 10208;
                public const int AIBuilderFileAttachedData = 10209;
                public const int AIEvaluationConfiguration = 10210;
                public const int AIEvaluationMetric = 10211;
                public const int AIEvaluationRun = 10212;
                public const int AIOptimization = 10213;
                public const int AIOptimizationPrivateData = 10214;
                public const int AITestCase = 10215;
                public const int AITestCaseDocument = 10216;
                public const int AITestCaseInput = 10217;
                public const int AITestRun = 10218;
                public const int AITestRunBatch = 10219;
                public const int HelpPage = 10220;
                public const int Tour = 10221;
                public const int BotContent = 10222;
                public const int ConversationTranscript = 10223;
                public const int Agent = 10224;
                public const int AgentComponent = 10225;
                public const int AgentComponentCollection = 10226;
                public const int Comment_ = 10237;
                public const int GovernanceConfiguration = 10238;
                public const int FabricAISkill = 10239;
                public const int AppInsightsMetadata = 10240;
                public const int DataflowConnectionReference = 10241;
                public const int Schedule = 10242;
                public const int DataflowTemplate = 10243;
                public const int DataflowDatalakeFolder = 10244;
                public const int DataMovementServiceRequest = 10245;
                public const int DataMovementServiceRequestStatus = 10246;
                public const int DMSSyncRequest = 10247;
                public const int DMSSyncStatus = 10248;
                public const int KnowledgeAssetConfiguration = 10249;
                public const int ModuleRunDetail = 10250;
                public const int QnA = 10251;
                public const int SalesforceStructuredObject = 10252;
                public const int SalesforceStructuredQnAConfig = 10253;
                public const int WorkflowActionStatus = 10254;
                public const int AllowedMCPClient = 10255;
                public const int FederatedKnowledgeCitation = 10256;
                public const int FederatedKnowledgeConfiguration = 10257;
                public const int FederatedKnowledgeEntityConfiguration = 10258;
                public const int FederatedKnowledgeMetadataRefresh = 10259;
                public const int IntelligentMemory = 10260;
                public const int KnowledgeFAQ = 10261;
                public const int FormMapping = 10262;
                public const int CopilotInteractions = 10263;
                public const int PDFSetting = 10264;
                public const int ActivityFileAttachment = 10265;
                public const int TeamsChat = 10266;
                public const int ServiceConfiguration = 10267;
                public const int SLAKPI = 10268;
                public const int IntegratedSearchProvider = 10269;
                public const int KnowledgeManagementSetting = 10270;
                public const int KnowledgeFederatedArticle = 10271;
                public const int KnowledgeFederatedArticleIncident = 10272;
                public const int SearchProvider = 10273;
                public const int KnowledgeArticleImage = 10274;
                public const int KnowledgeConfiguration = 10275;
                public const int KnowledgeInteractionInsight = 10276;
                public const int KnowledgeSearchInsight = 10277;
                public const int FavoriteKnowledgeArticle = 10278;
                public const int KnowledgeArticleLanguageSetting = 10279;
                public const int KnowledgeArticleAttachment = 10280;
                public const int KnowledgePersonalization = 10281;
                public const int KnowledgeArticleTemplate = 10282;
                public const int KnowledgeSearchPersonalFilterConfig = 10283;
                public const int KnowledgeSearchFilter = 10284;
                public const int BulkHarvestRunLog = 10286;
                public const int HarvestEligibilityCondition = 10287;
                public const int HarvestWorkItem = 10288;
                public const int MsdynHistoricalcaseharvestbatch = 10289;
                public const int MsdynHistoricalcaseharvestrun = 10290;
                public const int InterimUpdateKnowledgeArticle = 10291;
                public const int KnowledgeArticleCustomEntity = 10292;
                public const int KnowledgeHarvestJobRecord = 10293;
                public const int KnowledgeHarvestPlan = 10294;
                public const int SupportUserTable = 10295;
                public const int FxExpression = 10296;
                public const int Function = 10297;
                public const int PlugIn = 10298;
                public const int PowerfxRule = 10299;
                public const int PlannerBusinessScenario = 10300;
                public const int PlannerSyncAction = 10301;
                public const int AgentRule = 10302;
                public const int MCPPrompt = 10303;
                public const int MCPResource = 10304;
                public const int MCPResourceContent = 10305;
                public const int MCPServer = 10306;
                public const int MCPTool = 10307;
                public const int ToolingGateway = 10308;
                public const int ToolingGatewayMCPServer = 10309;
                public const int EmailAddressConfiguration = 10310;
                public const int MsGraphResourceToSubscription = 10311;
                public const int VirtualEntityMetadata = 10312;
                public const int BackgroundOperation = 10313;
                public const int ReportParameter = 10314;
                public const int MobileOfflineProfileExtension = 10315;
                public const int MobileOfflineProfileItemFilter = 10316;
                public const int TeamMobileOfflineProfileMembership = 10317;
                public const int UserMobileOfflineProfileMembership = 10318;
                public const int OrganizationDataSyncSubscription = 10319;
                public const int OrganizationDataSyncSubscriptionEntity = 10320;
                public const int OrganizationDataSyncSubscriptionFnoTable = 10321;
                public const int OrganizationDataSyncFnoState = 10322;
                public const int OrganizationDataSyncState = 10323;
                public const int ArchiveCleanupInfo = 10324;
                public const int ArchiveCleanupOperation = 10325;
                public const int BulkArchiveConfig = 10326;
                public const int BulkArchiveFailureDetail = 10327;
                public const int BulkArchiveOperation = 10328;
                public const int BulkArchiveOperationDetail = 10329;
                public const int EnableArchivalRequest = 10330;
                public const int MetadataForArchival = 10331;
                public const int ReconciliationEntityInfo = 10332;
                public const int ReconciliationEntityStepInfo = 10333;
                public const int ReconciliationInfo = 10334;
                public const int RetentionCleanupInfo = 10335;
                public const int RetentionCleanupOperation = 10336;
                public const int DataLifeCycleConfig = 10337;
                public const int RetentionFailureDetail = 10338;
                public const int RetentionOperation = 10339;
                public const int RetentionOperationDetail = 10340;
                public const int RetentionSuccessDetail = 10341;
                public const int CertificateCredential = 10342;
                public const int Notification_ = 10343;
                public const int UserRating = 10344;
                public const int MobileApp = 10345;
                public const int PowerAppsWrapBuild = 10346;
                public const int InsightsStoreDataSource = 10347;
                public const int InsightsStoreVirtualEntity = 10348;
                public const int RoleEditorLayout = 10349;
                public const int DeletedRecordReference = 10350;
                public const int RestoreDeletedRecordsConfiguration = 10351;
                public const int AppAction = 10352;
                public const int AppActionMigration = 10353;
                public const int AppActionRule = 10354;
                public const int Card = 10357;
                public const int CardStateItem = 10358;
                public const int EntityLinkChatConfiguration = 10361;
                public const int AgentFeedItem = 10362;
                public const int AgentHubGoal = 10363;
                public const int AgentHubInsight = 10364;
                public const int AgentHubMetric = 10365;
                public const int AgenticScenario = 10366;
                public const int AgentMemory = 10367;
                public const int AgentTask = 10368;
                public const int SharePointManagedIdentity = 10369;
                public const int AIInsightCard = 10370;
                public const int AISkillConfig = 10371;
                public const int SuggestedAction = 10372;
                public const int SuggestedActionCriteria = 10373;
                public const int DataWorkspace = 10374;
                public const int Plan = 10375;
                public const int PlanArtifact = 10376;
                public const int PlanAttachment = 10377;
                public const int UXAgentComponent = 10378;
                public const int UXAgentComponentRevision = 10379;
                public const int UXAgentProject = 10380;
                public const int UXAgentProjectFile = 10381;
                public const int AgentConversationMessage = 10382;
                public const int AgentConversationMessageFile = 10383;
                public const int RichTextAttachment = 10384;
                public const int StructuredTemplate = 10385;
                public const int RTEStructuredTemplateConfig = 10386;
                public const int RTETemplateMapping = 10387;
                public const int CustomControlExtendedSetting = 10388;
                public const int TimelinePin = 10389;
                public const int VirtualConnectorDataSource = 10390;
                public const int VirtualTableColumnCandidate = 10391;
                public const int PMAnalysisHistory = 10393;
                public const int PMBusinessRuleAutomationConfig = 10394;
                public const int PMCalendar = 10395;
                public const int PMCalendarVersion = 10396;
                public const int PMInferredTask = 10397;
                public const int PMProcessExtendedMetadataVersion = 10398;
                public const int PMProcessTemplate = 10399;
                public const int PMProcessUserSettings = 10400;
                public const int PMProcessVersion = 10401;
                public const int PMRecording = 10402;
                public const int PMSimulation = 10403;
                public const int PMTab = 10404;
                public const int PMTemplate = 10405;
                public const int PMView = 10406;
                public const int AnalysisComponent = 10407;
                public const int AnalysisJob = 10408;
                public const int AnalysisOverride = 10409;
                public const int AnalysisResult = 10410;
                public const int AnalysisResultDetail = 10411;
                public const int SolutionHealthRule = 10412;
                public const int SolutionHealthRuleArgument = 10413;
                public const int SolutionHealthRuleSet = 10414;
                public const int FileUpload = 10415;
                public const int AppEntitySearchView = 10416;
                public const int MainFewShot = 10417;
                public const int MakerFewShot = 10418;
                public const int SearchAttributeSettings = 10419;
                public const int SearchCustomAnalyzer = 10420;
                public const int SearchRelationshipSettings = 10421;
                public const int SearchResultsCache = 10422;
                public const int SearchTelemetry = 10423;
                public const int TextDataRecordsIndexingStatus = 10424;
                public const int ViewAsExampleQuestion = 10425;
                public const int CopilotExampleQuestion = 10426;
                public const int CopilotGlossaryTerm = 10427;
                public const int CopilotSynonyms = 10428;
                public const int BusinessSkill = 10429;
                public const int BusinessSkillResource = 10430;
                public const int SiteComponent = 10432;
                public const int Site = 10433;
                public const int SiteLanguage = 10434;
                public const int PowerPagesSitePublished = 10435;
                public const int SiteSourceFile = 10436;
                public const int ExternalIdentity = 10439;
                public const int Invitation = 10440;
                public const int InviteRedemption = 10441;
                public const int PortalComment = 10442;
                public const int Setting = 10443;
                public const int MultistepFormSession = 10444;
                public const int AdPlacement = 10448;
                public const int ColumnPermission = 10449;
                public const int ColumnPermissionProfile = 10450;
                public const int ContentSnippet = 10451;
                public const int BasicForm = 10452;
                public const int BasicFormMetadata = 10453;
                public const int List = 10454;
                public const int TablePermission = 10455;
                public const int PageTemplate = 10456;
                public const int PollPlacement = 10457;
                public const int PowerPagesCoreEntityDS = 10458;
                public const int PublishingState = 10459;
                public const int PublishingStateTransitionRule = 10460;
                public const int Redirect = 10461;
                public const int Shortcut = 10462;
                public const int SiteMarker = 10463;
                public const int SiteSetting = 10464;
                public const int WebFile = 10465;
                public const int MultistepForm = 10466;
                public const int MultistepFormMetadata = 10467;
                public const int FormStep = 10468;
                public const int WebLink = 10469;
                public const int WebLinkSet = 10470;
                public const int WebPage = 10471;
                public const int WebPageAccessControlRule = 10472;
                public const int WebRole = 10473;
                public const int Website = 10474;
                public const int WebsiteAccess = 10475;
                public const int WebsiteLanguage = 10476;
                public const int WebTemplate = 10477;
                public const int PowerPagesScanReport = 10484;
                public const int PowerPagesDDOSAlert = 10485;
                public const int PowerPagesLog = 10486;
                public const int PowerPagesManagedIdentity = 10487;
                public const int PowerPagesSiteAIFeedback = 10488;
                public const int CatalogSubmissionFiles = 10494;
                public const int PackageSubmissionStore = 10495;
                public const int Indexedtrait = 10496;
                public const int ProcessorRegistration = 10497;
                public const int Signal = 10498;
                public const int SignalRegistration = 10499;
                public const int Trait = 10500;
                public const int TraitRegistration = 10501;
                public const int EventAggregatorScans = 10559;
                public const int Cleanup = 10560;
                public const int EventAggregator = 10561;
                public const int OnlineShopperIntention = 10562;
                public const int GaurdianFullscan = 10563;
                public const int GaurdianHealthchecks = 10564;
                public const int HealthcareFeedback = 10565;
                public const int ObjectDetectionProduct = 10566;
            }
            public struct StateCode
            {
                public const int Inactive = 0;
                public const int Active = 1;
            }
            public struct StatusCode
            {
                public const int Unpublished = 0;
                public const int Publishing = 1;
                public const int Published = 2;
            }
        }
        #endregion

        #region LogicalNames
        public static partial class LogicalNames
        {
            public const string DuplicateRuleId = "duplicateruleid";
            public const string BaseEntityMatchCodeTable = "baseentitymatchcodetable";
            public const string BaseEntityName = "baseentityname";
            public const string BaseEntityTypeCode = "baseentitytypecode";
            public const string ComponentIdUnique = "componentidunique";
            public const string ComponentState = "componentstate";
            public const string CreatedBy = "createdby";
            public const string CreatedOn = "createdon";
            public const string CreatedOnBehalfBy = "createdonbehalfby";
            public const string Description = "description";
            public const string ExcludeInactiveRecords = "excludeinactiverecords";
            public const string IsCaseSensitive = "iscasesensitive";
            public const string IsCustomizable = "iscustomizable";
            public const string IsManaged = "ismanaged";
            public const string MatchingEntityMatchCodeTable = "matchingentitymatchcodetable";
            public const string MatchingEntityName = "matchingentityname";
            public const string MatchingEntityTypeCode = "matchingentitytypecode";
            public const string ModifiedBy = "modifiedby";
            public const string ModifiedOn = "modifiedon";
            public const string ModifiedOnBehalfBy = "modifiedonbehalfby";
            public const string Name = "name";
            public const string OverwriteTime = "overwritetime";
            public const string OwnerId = "ownerid";
            public const string OwningBusinessUnit = "owningbusinessunit";
            public const string OwningTeam = "owningteam";
            public const string OwningUser = "owninguser";
            public const string SolutionId = "solutionid";
            public const string StateCode = "statecode";
            public const string StatusCode = "statuscode";
            public const string TimeZoneRuleVersionNumber = "timezoneruleversionnumber";
            public const string UniqueName = "uniquename";
            public const string UTCConversionTimeZoneCode = "utcconversiontimezonecode";
        }
        #endregion

        #region AlternateKeys
        public static partial class AlternateKeys
        {
            public const string Dupdetectionuniquekey = "dupdetectionuniquekey";
        }
        #endregion

        #region Relations
        public static partial class Relations
        {
            public static class OneToMany
            {
                public const string DuplicateRuleAnnotation = "DuplicateRule_Annotation";
                public const string DuplicateRuleDuplicateBaseRecord = "DuplicateRule_DuplicateBaseRecord";
                public const string DuplicateRuleDuplicateRuleConditions = "DuplicateRule_DuplicateRuleConditions";
                public const string DuplicateRuleSyncErrors = "DuplicateRule_SyncErrors";
                public const string UserentityinstancedataDuplicaterule = "userentityinstancedata_duplicaterule";
            }

            public static partial class ManyToOne
            {
                public const string BusinessUnitDuplicateRules = "BusinessUnit_DuplicateRules";
                public const string LkDuplicateruleCreatedonbehalfby = "lk_duplicaterule_createdonbehalfby";
                public const string LkDuplicateruleModifiedonbehalfby = "lk_duplicaterule_modifiedonbehalfby";
                public const string LkDuplicaterulebaseCreatedby = "lk_duplicaterulebase_createdby";
                public const string LkDuplicaterulebaseModifiedby = "lk_duplicaterulebase_modifiedby";
                public const string OwnerDuplicaterules = "owner_duplicaterules";
                public const string SystemUserDuplicateRules = "SystemUser_DuplicateRules";
                public const string TeamDuplicateRules = "team_DuplicateRules";
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

        public static DuplicateRule Retrieve(IOrganizationService service, Guid id)
        {
            return Retrieve(service, id, new ColumnSet(true));
        }

        public static DuplicateRule Retrieve(IOrganizationService service, Guid id, ColumnSet columnSet)
        {
            return service.Retrieve("duplicaterule", id, columnSet).ToEntity<DuplicateRule>();
        }

        public DuplicateRule GetChangedEntity()
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
            return new DuplicateRule(Id) { Attributes = attr };
        }
        #endregion
    }

    #region Context
    public partial class DataContext
    {
        public IQueryable<DuplicateRule> DuplicateRuleSet
        {
            get
            {
                return CreateQuery<DuplicateRule>();
            }
        }
    }
    #endregion
}
