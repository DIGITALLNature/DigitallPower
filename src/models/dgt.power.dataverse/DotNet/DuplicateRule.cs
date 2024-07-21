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
	/// Rule used to identify potential duplicates.
	/// </summary>
	[DataContractAttribute()]
	[EntityLogicalNameAttribute("duplicaterule")]
	[System.CodeDom.Compiler.GeneratedCode("dgtp", "2023")]
    [ExcludeFromCodeCoverage]
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
		public DuplicateRule(Guid id, bool trackChanges = false) : base(EntityLogicalName,id)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public DuplicateRule(KeyAttributeCollection keyAttributes, bool trackChanges = false) : base(EntityLogicalName,keyAttributes)
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
        private readonly Lazy<HashSet<string>> _changedProperties = new Lazy<HashSet<string>>();
        #endregion

        #region consts
        public const string EntityLogicalName = "duplicaterule";
        public const string PrimaryNameAttribute = "name";
        public const int EntityTypeCode = 4414;
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
		[AttributeLogicalNameAttribute("duplicateruleid")]
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
                OnPropertyChanging(nameof(DuplicateRuleId));
                SetAttributeValue("duplicateruleid", value);
				if (value.HasValue)
				{
					base.Id = value.Value;
				}
				else
				{
					base.Id = System.Guid.Empty;
				}
                OnPropertyChanged(nameof(DuplicateRuleId));
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
                OnPropertyChanging(nameof(BaseEntityName));
                SetAttributeValue("baseentityname", value);
                OnPropertyChanged(nameof(BaseEntityName));
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
                OnPropertyChanging(nameof(Description));
                SetAttributeValue("description", value);
                OnPropertyChanged(nameof(Description));
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
                OnPropertyChanging(nameof(ExcludeInactiveRecords));
                SetAttributeValue("excludeinactiverecords", value);
                OnPropertyChanged(nameof(ExcludeInactiveRecords));
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
                OnPropertyChanging(nameof(IsCaseSensitive));
                SetAttributeValue("iscasesensitive", value);
                OnPropertyChanged(nameof(IsCaseSensitive));
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
                OnPropertyChanging(nameof(MatchingEntityName));
                SetAttributeValue("matchingentityname", value);
                OnPropertyChanged(nameof(MatchingEntityName));
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
                OnPropertyChanging(nameof(Name));
                SetAttributeValue("name", value);
                OnPropertyChanged(nameof(Name));
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
                OnPropertyChanging(nameof(OwnerId));
                SetAttributeValue("ownerid", value);
                OnPropertyChanged(nameof(OwnerId));
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
                OnPropertyChanging(nameof(StatusCode));
                SetAttributeValue("statuscode", value);
                OnPropertyChanged(nameof(StatusCode));
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


		#endregion

		#region NavigationProperties
		/// <summary>
		/// 1:N DuplicateRule_DuplicateRuleConditions
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("DuplicateRule_DuplicateRuleConditions")]
		public System.Collections.Generic.IEnumerable<DuplicateRuleCondition> DuplicateRuleDuplicateRuleConditions
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<DuplicateRuleCondition>("DuplicateRule_DuplicateRuleConditions", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("DuplicateRuleDuplicateRuleConditions");
				this.SetRelatedEntities<DuplicateRuleCondition>("DuplicateRule_DuplicateRuleConditions", null, value);
				this.OnPropertyChanged("DuplicateRuleDuplicateRuleConditions");
			}
		}

		#endregion

		#region Options
		public static class Options
		{
			    public struct BaseEntityTypeCode
                {
					public const int Account = 1;
					public const int Contact = 2;
					public const int Opportunity = 3;
					public const int Lead = 4;
					public const int Note = 5;
					public const int BusinessUnitMap = 6;
					public const int Owner = 7;
					public const int User = 8;
					public const int Team = 9;
					public const int BusinessUnit = 10;
					public const int SystemUserPrincipal = 14;
					public const int AccountLeads = 16;
					public const int ContactInvoices = 17;
					public const int ContactQuotes = 18;
					public const int ContactOrders = 19;
					public const int ServiceContractContact = 20;
					public const int ProductSalesLiterature = 21;
					public const int ContactLeads = 22;
					public const int LeadCompetitors = 24;
					public const int OpportunityCompetitors = 25;
					public const int CompetitorSalesLiterature = 26;
					public const int LeadProduct = 27;
					public const int Subscription = 29;
					public const int FilterTemplate = 30;
					public const int PrivilegeObjectTypeCode = 31;
					public const int SalesProcessInstance = 32;
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
					public const int PrincipalEntityBusinessUnitMap = 61;
					public const int RecordFilter = 72;
					public const int EntityRecordFilter = 73;
					public const int SecuredMaskingRule = 74;
					public const int VirtualEntityDataProvider = 78;
					public const int VirtualEntityDataSource = 85;
					public const int TeamTemplate = 92;
					public const int SocialProfile = 99;
					public const int ServicePlan = 101;
					public const int PrivilegesRemovalSetting = 103;
					public const int Case = 112;
					public const int ChildIncidentCount = 113;
					public const int Competitor = 123;
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
					public const int PhoneToCaseProcess = 952;
					public const int OpportunitySalesProcess = 953;
					public const int LeadToOpportunitySalesProcess = 954;
					public const int ExpiredProcess = 955;
					public const int Attachment = 1001;
					public const int Attachment_ = 1002;
					public const int InternalAddress = 1003;
					public const int CompetitorAddress = 1004;
					public const int CompetitorProduct = 1006;
					public const int ImageDescriptor = 1007;
					public const int Contract = 1010;
					public const int ContractLine = 1011;
					public const int Discount = 1013;
					public const int ArticleTemplate = 1016;
					public const int LeadAddress = 1017;
					public const int Organization = 1019;
					public const int OrganizationUI = 1021;
					public const int PriceList = 1022;
					public const int Privilege = 1023;
					public const int Product = 1024;
					public const int ProductAssociation = 1025;
					public const int PriceListItem = 1026;
					public const int ProductRelationship = 1028;
					public const int SystemForm = 1030;
					public const int UserDashboard = 1031;
					public const int SecurityRole = 1036;
					public const int RoleTemplate = 1037;
					public const int SalesLiterature = 1038;
					public const int View = 1039;
					public const int StringMap = 1043;
					public const int Property = 1048;
					public const int PropertyOptionSetItem = 1049;
					public const int Unit = 1055;
					public const int UnitGroup = 1056;
					public const int SalesAttachment = 1070;
					public const int Address = 1071;
					public const int SubscriptionClients = 1072;
					public const int StatusMap = 1075;
					public const int DiscountList = 1080;
					public const int ArticleComment = 1082;
					public const int OpportunityLine = 1083;
					public const int Quote = 1084;
					public const int QuoteLine = 1085;
					public const int UserFiscalCalendar = 1086;
					public const int Order = 1088;
					public const int OrderLine = 1089;
					public const int Invoice = 1090;
					public const int InvoiceLine = 1091;
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
					public const int Characteristic = 1141;
					public const int RatingValue = 1142;
					public const int RatingModel = 1144;
					public const int BookableResourceBooking = 1145;
					public const int BookableResourceBookingHeader = 1146;
					public const int BookableResourceCategory = 1147;
					public const int BookableResourceCharacteristic = 1148;
					public const int BookableResourceCategoryAssn = 1149;
					public const int BookableResource = 1150;
					public const int BookableResourceGroup = 1151;
					public const int BookingStatus = 1152;
					public const int DocumentSuggestions = 1189;
					public const int SuggestionCardTemplate = 1190;
					public const int FieldSecurityProfile = 1200;
					public const int FieldPermission = 1201;
					public const int TeamProfiles = 1203;
					public const int Application = 1204;
					public const int ChannelPropertyGroup = 1234;
					public const int PropertyAssociation = 1235;
					public const int ChannelProperty = 1236;
					public const int SocialInsightsConfiguration = 1300;
					public const int SavedOrganizationInsightsConfiguration = 1309;
					public const int PropertyInstance = 1333;
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
					public const int ContractTemplate = 2011;
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
					public const int Facility_Equipment = 4000;
					public const int Service = 4001;
					public const int Resource = 4002;
					public const int Calendar = 4003;
					public const int CalendarRule = 4004;
					public const int SchedulingGroup = 4005;
					public const int ResourceSpecification = 4006;
					public const int ResourceGroup = 4007;
					public const int Site = 4009;
					public const int ResourceExpansion = 4010;
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
					public const int CaseResolution = 4206;
					public const int Letter = 4207;
					public const int OpportunityClose = 4208;
					public const int OrderClose = 4209;
					public const int PhoneCall = 4210;
					public const int QuoteClose = 4211;
					public const int Task = 4212;
					public const int ServiceActivity = 4214;
					public const int Commitment = 4215;
					public const int SocialActivity = 4216;
					public const int UntrackedEmail = 4220;
					public const int SavedView = 4230;
					public const int MetadataDifference = 4231;
					public const int BusinessDataLocalizedLabel = 4232;
					public const int RecurrenceRule = 4250;
					public const int RecurringAppointment = 4251;
					public const int EmailSearch = 4299;
					public const int MarketingList = 4300;
					public const int MarketingListMember = 4301;
					public const int Campaign = 4400;
					public const int CampaignResponse = 4401;
					public const int CampaignActivity = 4402;
					public const int CampaignItem = 4403;
					public const int CampaignActivityItem = 4404;
					public const int BulkOperationLog = 4405;
					public const int QuickCampaign = 4406;
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
					public const int BookableResourceBookingToExchangeIdMapping = 4421;
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
					public const int OpportunityRelationship = 4503;
					public const int EntitlementTemplateProduct = 4545;
					public const int Auditing = 4567;
					public const int RibbonClientMetadata_ = 4579;
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
					public const int EntitlementProduct = 6363;
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
					public const int EntitlementContact = 7272;
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
					public const int Entitlement = 9700;
					public const int EntitlementChannel = 9701;
					public const int EntitlementTemplate = 9702;
					public const int EntitlementTemplateChannel = 9703;
					public const int EntitlementEntityAllocationTypeMapping = 9704;
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
					public const int IncidentKnowledgeBaseRecord = 9931;
					public const int TimeStampDateMapping = 9932;
					public const int AzureServiceConnection = 9936;
					public const int DocumentTemplate = 9940;
					public const int PersonalDocumentTemplate = 9941;
					public const int TopicModelConfiguration = 9942;
					public const int TopicModelExecutionHistory = 9943;
					public const int TopicModel = 9944;
					public const int TextAnalyticsEntityMapping = 9945;
					public const int TopicHistory = 9946;
					public const int KnowledgeSearchModel = 9947;
					public const int TextAnalyticsTopic = 9948;
					public const int AdvancedSimilarityRule = 9949;
					public const int OfficeGraphDocument = 9950;
					public const int SimilarityRule = 9951;
					public const int KnowledgeArticle = 9953;
					public const int KnowledgeArticleIncident = 9954;
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
					public const int StagedEntity = 10019;
					public const int StagedEntityAttribute = 10020;
					public const int StagedMetadataAsyncOperation = 10021;
					public const int KeyVaultReference = 10022;
					public const int ManagedIdentity = 10023;
					public const int Catalog = 10024;
					public const int CatalogAssignment = 10025;
					public const int InternalCatalogAssignment = 10026;
					public const int CustomAPI = 10027;
					public const int CustomAPIRequestParameter = 10028;
					public const int CustomAPIResponseProperty = 10029;
					public const int PluginPackage = 10030;
					public const int NonRelationalDataSource = 10031;
					public const int ProvisionLanguageForUser = 10032;
					public const int SharedObject = 10033;
					public const int SharedWorkspace = 10034;
					public const int SharedWorkspaceAccessToken = 10035;
					public const int SharedWorkspaceNonRelational = 10036;
					public const int SharedWorkspacePool = 10037;
					public const int DataLakeFolder = 10038;
					public const int DataLakeFolderPermission = 10039;
					public const int DataLakeWorkspace = 10040;
					public const int DataLakeWorkspacePermission = 10041;
					public const int DataProcessingConfiguration = 10042;
					public const int ExportedExcel = 10043;
					public const int RetainedDataExcel = 10044;
					public const int SynapseDatabase = 10045;
					public const int SynapseLinkExternalTableState = 10046;
					public const int SynapseLinkProfile = 10047;
					public const int SynapseLinkProfileEntity = 10048;
					public const int SynapseLinkProfileEntityState = 10049;
					public const int SynapseLinkSchedule = 10050;
					public const int ComponentVersion = 10051;
					public const int ComponentVersionDataSource = 10052;
					public const int ComponentVersion_Internal_ = 10053;
					public const int DataflowRefreshHistory = 10054;
					public const int EntityRefreshHistory = 10055;
					public const int SharedLinkSetting = 10056;
					public const int DelegatedAuthorization = 10057;
					public const int CascadeGrantRevokeAccessRecordsTracker = 10059;
					public const int CascadeGrantRevokeAccessVersionTracker = 10060;
					public const int RevokeInheritedAccessRecordsTracker = 10061;
					public const int TdsMetadata = 10062;
					public const int ModelDrivenAppElement = 10063;
					public const int ModelDrivenAppComponentNode_sEdge = 10064;
					public const int ModelDrivenAppComponentNode = 10065;
					public const int ModelDrivenAppSetting = 10066;
					public const int ModelDrivenAppUserSetting = 10067;
					public const int OrganizationSetting = 10068;
					public const int SettingDefinition = 10069;
					public const int CanvasAppExtendedMetadata = 10070;
					public const int ServicePlanMapping = 10071;
					public const int ServicePlanCustomControl = 10072;
					public const int ApplicationUser = 10074;
					public const int ODataV4DataSource = 10077;
					public const int WorkflowBinary = 10078;
					public const int Credential = 10079;
					public const int DesktopFlowModule = 10080;
					public const int FlowCapacityAssignment = 10081;
					public const int FlowCredentialApplication = 10082;
					public const int FlowEvent = 10083;
					public const int FlowMachine = 10084;
					public const int FlowMachineGroup = 10085;
					public const int FlowMachineImage = 10086;
					public const int FlowMachineImageVersion = 10087;
					public const int FlowMachineNetwork = 10088;
					public const int ProcessStageParameter = 10089;
					public const int WorkQueue = 10090;
					public const int WorkQueueItem = 10091;
					public const int DesktopFlowBinary = 10092;
					public const int FlowLog = 10093;
					public const int FlowRun = 10094;
					public const int ConnectionReference = 10095;
					public const int DVFileSearch = 10096;
					public const int DVFileSearchAttribute = 10097;
					public const int DVFileSearchEntity = 10098;
					public const int DVTableSearch = 10099;
					public const int DVTableSearchAttribute = 10100;
					public const int DVTableSearchEntity = 10101;
					public const int AICopilot = 10102;
					public const int AIPluginAuth = 10103;
					public const int AIPluginConversationStarter = 10104;
					public const int AIPluginConversationStarterMapping = 10105;
					public const int AIPluginGovernance = 10106;
					public const int AIPluginGovernanceExtended = 10107;
					public const int AIPluginOperationResponseTemplate = 10108;
					public const int AIPluginTitle = 10109;
					public const int SideloadedAIPlugin = 10110;
					public const int AIPlugin = 10111;
					public const int AIPluginExternalSchema = 10112;
					public const int AIPluginExternalSchemaProperty = 10113;
					public const int AIPluginInstance = 10114;
					public const int AIPluginOperation = 10115;
					public const int AIPluginOperationParameter = 10116;
					public const int AIPluginUserSetting = 10117;
					public const int AIEvent = 10119;
					public const int AIBuilderFeedbackLoop = 10120;
					public const int AIFormProcessingDocument = 10121;
					public const int AIObjectDetectionImage = 10122;
					public const int AIObjectDetectionLabel = 10123;
					public const int AIObjectDetectionBoundingBox = 10124;
					public const int AIObjectDetectionImageMapping = 10125;
					public const int AIBuilderDataset = 10127;
					public const int AIBuilderDatasetFile = 10128;
					public const int AIBuilderDatasetRecord = 10129;
					public const int AIBuilderDatasetsContainer = 10130;
					public const int AIBuilderFile = 10131;
					public const int AIBuilderFileAttachedData = 10132;
					public const int HelpPage = 10133;
					public const int Tour = 10134;
					public const int BotContent = 10135;
					public const int ConversationTranscript = 10136;
					public const int Chatbot = 10137;
					public const int ChatbotSubcomponent = 10138;
					public const int ChatbotComponentCollection = 10139;
					public const int Comment_ = 10150;
					public const int FabricAISkill = 10151;
					public const int AppInsightsMetadata = 10152;
					public const int DataflowConnectionReference = 10153;
					public const int Schedule = 10154;
					public const int DataflowTemplate = 10155;
					public const int DataflowDatalakeFolder = 10156;
					public const int DataMovementServiceRequest = 10157;
					public const int DataMovementServiceRequestStatus = 10158;
					public const int DMSSyncRequest = 10159;
					public const int DMSSyncStatus = 10160;
					public const int KnowledgeAssetConfiguration = 10161;
					public const int ModuleRunDetail = 10162;
					public const int SalesforceStructuredObject = 10163;
					public const int SalesforceStructuredQnAConfig = 10164;
					public const int WorkflowActionStatus = 10165;
					public const int PDFSetting = 10166;
					public const int ActivityFileAttachment = 10167;
					public const int TeamsChat = 10168;
					public const int ServiceConfiguration = 10169;
					public const int SLAKPI = 10170;
					public const int IntegratedSearchProvider = 10171;
					public const int KnowledgeManagementSetting = 10172;
					public const int KnowledgeFederatedArticle = 10173;
					public const int KnowledgeFederatedArticleIncident = 10174;
					public const int SearchProvider = 10175;
					public const int KnowledgeArticleImage = 10176;
					public const int KnowledgeConfiguration = 10177;
					public const int KnowledgeInteractionInsight = 10178;
					public const int KnowledgeSearchInsight = 10179;
					public const int FavoriteKnowledgeArticle = 10180;
					public const int KnowledgeArticleLanguageSetting = 10181;
					public const int KnowledgeArticleAttachment = 10182;
					public const int KnowledgePersonalization = 10183;
					public const int KnowledgeArticleTemplate = 10184;
					public const int KnowledgeSearchPersonalFilterConfig = 10185;
					public const int KnowledgeSearchFilter = 10186;
					public const int SupportUserTable = 10188;
					public const int FxExpression = 10189;
					public const int PowerfxRule = 10190;
					public const int PlannerBusinessScenario = 10191;
					public const int PlannerSyncAction = 10192;
					public const int MsGraphResourceToSubscription = 10193;
					public const int VirtualEntityMetadata = 10194;
					public const int BackgroundOperation = 10195;
					public const int MobileOfflineProfileExtension = 10196;
					public const int MobileOfflineProfileItemFilter = 10197;
					public const int TeamMobileOfflineProfileMembership = 10198;
					public const int UserMobileOfflineProfileMembership = 10199;
					public const int OrganizationDataSyncSubscription = 10200;
					public const int OrganizationDataSyncSubscriptionEntity = 10201;
					public const int OrganizationDataSyncSubscriptionFnoTable = 10202;
					public const int OrganizationDataSyncFnoState = 10203;
					public const int OrganizationDataSyncState = 10204;
					public const int ArchiveCleanupInfo = 10205;
					public const int ArchiveCleanupOperation = 10206;
					public const int BulkArchiveConfig = 10207;
					public const int BulkArchiveFailureDetail = 10208;
					public const int BulkArchiveOperation = 10209;
					public const int BulkArchiveOperationDetail = 10210;
					public const int EnableArchivalRequest = 10211;
					public const int MetadataForArchival = 10212;
					public const int ReconciliationEntityInfo = 10213;
					public const int ReconciliationEntityStepInfo = 10214;
					public const int ReconciliationInfo = 10215;
					public const int RetentionCleanupInfo = 10216;
					public const int RetentionCleanupOperation = 10217;
					public const int RetentionConfig = 10218;
					public const int RetentionFailureDetail = 10219;
					public const int RetentionOperation = 10220;
					public const int RetentionOperationDetail = 10221;
					public const int Notification_ = 10222;
					public const int UserRating = 10223;
					public const int MobileApp = 10224;
					public const int InsightsStoreDataSource = 10225;
					public const int InsightsStoreVirtualEntity = 10226;
					public const int RoleEditorLayout = 10227;
					public const int DeletedRecordReference = 10228;
					public const int RestoreDeletedRecordsConfiguration = 10229;
					public const int AppAction = 10230;
					public const int AppActionMigration = 10231;
					public const int AppActionRule = 10232;
					public const int Card = 10235;
					public const int CardStateItem = 10236;
					public const int EntityLinkChatConfiguration = 10239;
					public const int RichTextAttachment = 10240;
					public const int CustomControlExtendedSetting = 10241;
					public const int TimelinePin = 10242;
					public const int VirtualConnectorDataSource = 10243;
					public const int VirtualTableColumnCandidate = 10244;
					public const int PMAnalysisHistory = 10245;
					public const int PMBusinessRuleAutomationConfig = 10246;
					public const int PMCalendar = 10247;
					public const int PMCalendarVersion = 10248;
					public const int PMInferredTask = 10249;
					public const int PMProcessExtendedMetadataVersion = 10250;
					public const int PMProcessTemplate = 10251;
					public const int PMProcessUserSettings = 10252;
					public const int PMProcessVersion = 10253;
					public const int PMRecording = 10254;
					public const int PMTemplate = 10255;
					public const int PMView = 10256;
					public const int AnalysisComponent = 10257;
					public const int AnalysisJob = 10258;
					public const int AnalysisOverride = 10259;
					public const int AnalysisResult = 10260;
					public const int AnalysisResultDetail = 10261;
					public const int SolutionHealthRule = 10262;
					public const int SolutionHealthRuleArgument = 10263;
					public const int SolutionHealthRuleSet = 10264;
					public const int PowerBIDataset = 10265;
					public const int Powerbidatasetapdx = 10266;
					public const int PowerBIMashupParameter = 10267;
					public const int PowerBIReport = 10268;
					public const int Powerbireportapdx = 10269;
					public const int FileUpload = 10270;
					public const int MainFewShot = 10271;
					public const int MakerFewShot = 10272;
					public const int SearchAttributeSettings = 10273;
					public const int SearchCustomAnalyzer = 10274;
					public const int SearchRelationshipSettings = 10275;
					public const int SearchResultsCache = 10276;
					public const int SearchTelemetry = 10277;
					public const int CopilotExampleQuestion = 10278;
					public const int CopilotGlossaryTerm = 10279;
					public const int CopilotSynonyms = 10280;
					public const int SiteComponent = 10281;
					public const int Site_ = 10282;
					public const int SiteLanguage = 10283;
					public const int PowerPagesSitePublished = 10284;
					public const int ExternalIdentity = 10287;
					public const int Invitation = 10288;
					public const int InviteRedemption = 10289;
					public const int PortalComment = 10290;
					public const int Setting = 10291;
					public const int MultistepFormSession = 10292;
					public const int AdPlacement = 10296;
					public const int ColumnPermission = 10297;
					public const int ColumnPermissionProfile = 10298;
					public const int ContentSnippet = 10299;
					public const int BasicForm = 10300;
					public const int BasicFormMetadata = 10301;
					public const int List = 10302;
					public const int TablePermission = 10303;
					public const int PageTemplate = 10304;
					public const int PollPlacement = 10305;
					public const int PowerPagesCoreEntityDS = 10306;
					public const int PublishingState = 10307;
					public const int PublishingStateTransitionRule = 10308;
					public const int Redirect = 10309;
					public const int Shortcut = 10310;
					public const int SiteMarker = 10311;
					public const int SiteSetting = 10312;
					public const int WebFile = 10313;
					public const int MultistepForm = 10314;
					public const int MultistepFormMetadata = 10315;
					public const int FormStep = 10316;
					public const int WebLink = 10317;
					public const int WebLinkSet = 10318;
					public const int WebPage = 10319;
					public const int WebPageAccessControlRule = 10320;
					public const int WebRole = 10321;
					public const int Website = 10322;
					public const int WebsiteAccess = 10323;
					public const int WebsiteLanguage = 10324;
					public const int WebTemplate = 10325;
					public const int PowerPagesScanReport = 10332;
					public const int PowerPagesLog = 10333;
					public const int CatalogSubmissionFiles = 10338;
					public const int PackageSubmissionStore = 10339;
					public const int ListOperation = 10340;
					public const int MarketingFormDisplayAttributes = 10341;
					public const int DatabaseVersion = 10342;
					public const int UpgradeRun = 10343;
					public const int UpgradeStep = 10344;
					public const int UpgradeVersion = 10345;
					public const int ActivityMonitor = 10346;
					public const int OriginatingQueueMapping = 10347;
					public const int UnifiedRoutingSetupTracker = 10348;
					public const int AvailableTimes = 10349;
					public const int AvailableTimesDataSource = 10350;
					public const int ResourceGroupDataSource = 10351;
					public const int VirtualResourceGroupResource = 10352;
					public const int MigrationTracker = 10353;
					public const int BgJobLedger = 10354;
					public const int Intent = 10355;
					public const int JobsState = 10356;
					public const int AssetCategoryTemplateAssociation = 10357;
					public const int AssetTemplateAssociation = 10358;
					public const int CustomerAsset = 10359;
					public const int CustomerAssetAttachment = 10362;
					public const int CustomerAssetCategory = 10363;
					public const int FunctionalLocation = 10364;
					public const int PropertyDefinition = 10365;
					public const int PropertyAssetAssociation = 10366;
					public const int PropertyLog = 10367;
					public const int PropertyTemplateAssociation = 10368;
					public const int TemplateForProperties = 10369;
					public const int IoTAlert = 10373;
					public const int IoTDevice = 10374;
					public const int IoTDeviceCategory = 10375;
					public const int IoTDeviceCommand = 10376;
					public const int IoTDeviceCommandDefinition = 10377;
					public const int IoTDeviceDataHistory = 10378;
					public const int IoTDeviceProperty = 10379;
					public const int IoTDeviceRegistrationHistory = 10380;
					public const int IoTDeviceVisualizationConfiguration = 10381;
					public const int IoTFieldMapping = 10382;
					public const int IoTPropertyDefinition = 10383;
					public const int IoTProvider = 10384;
					public const int IoTProviderInstance = 10385;
					public const int IoTSettings = 10386;
					public const int IoTAlertToCaseProcess = 10389;
					public const int PlaybookCallableContext = 10391;
					public const int PlaybookActivity = 10392;
					public const int PlaybookActivityAttribute = 10393;
					public const int _DEPRECATED_PlaybookCategory = 10394;
					public const int Playbook = 10395;
					public const int _DEPRECATED_PlaybookTemplate = 10396;
					public const int AdminSettingsEntity = 10398;
					public const int CollabSpaceTeamAssociation = 10399;
					public const int CRMConnection = 10400;
					public const int TaggedRecord = 10401;
					public const int CopilotForSalesCustomerList = 10402;
					public const int MsdynVivaentitysetting = 10403;
					public const int MsdynVivaorgextensioncred = 10404;
					public const int MsdynVivaorgsetting = 10405;
					public const int MsdynVivausersetting = 10406;
					public const int SalesCopilotInsight = 10407;
					public const int OrgLevelSettingsForSalesCopilotApps = 10408;
					public const int SalesCopilotUserSetting = 10409;
					public const int AppProfile = 10410;
					public const int ApplicationExtension = 10411;
					public const int ApplicationTabTemplate = 10412;
					public const int AppProfileRoleMapping = 10413;
					public const int NotificationField = 10414;
					public const int NotificationTemplate = 10415;
					public const int SessionTemplate = 10416;
					public const int TemplateParameter = 10417;
					public const int ChannelIntegrationFrameworkV1_0Provider = 10423;
					public const int NotificationField_Deprecated_ = 10424;
					public const int NotificationTemplate_Deprecated_ = 10425;
					public const int AppParameterDefinition_Deprecated_ = 10426;
					public const int SessionTemplates_Deprecated_ = 10427;
					public const int ApplicationTabTemplate_Deprecated_ = 10428;
					public const int Parameter_Deprecated_ = 10429;
					public const int TemplateTag_Deprecated_ = 10430;
					public const int ApplicationType_Deprecated_ = 10431;
					public const int ChannelIntegrationFrameworkV2_0Provider = 10440;
					public const int ConversationData_Deprecated_ = 10442;
					public const int KPIEventData = 10443;
					public const int KPIEventDefinition = 10444;
					public const int SessionData_Deprecated_ = 10445;
					public const int SessionParticipantData_Deprecated_ = 10446;
					public const int ChannelDefinition = 10447;
					public const int ChannelDefinitionConsent = 10448;
					public const int ChannelDefinitionLocale = 10449;
					public const int ChannelInstance = 10450;
					public const int ChannelInstanceAccount = 10451;
					public const int ChannelMessageAttachment = 10452;
					public const int ChannelMessageContextPart = 10453;
					public const int ChannelMessagePart = 10454;
					public const int ConsumingApplication = 10455;
					public const int MsdynDefExtendedChannelInstance = 10456;
					public const int MsdynDefExtendedChannelInstanceAccount = 10457;
					public const int ProductivityPaneConfiguration = 10458;
					public const int PaneTabConfiguration = 10459;
					public const int PaneToolConfiguration = 10460;
					public const int AgentScript = 10462;
					public const int AgentScriptStep = 10463;
					public const int ActionInputParameter = 10465;
					public const int ActionOutputParameter = 10466;
					public const int MacroActionTemplate = 10467;
					public const int MacroSolutionConfiguration = 10468;
					public const int MacroConnector = 10469;
					public const int MacroRunHistory = 10470;
					public const int ParameterDefinition = 10471;
					public const int AdaptiveCardConfiguration = 10474;
					public const int SmartassistConfiguration = 10475;
					public const int ReadTracker = 10477;
					public const int ReadTrackingEnabledInformation = 10478;
					public const int MicrosoftTeamsGraphResourceEntity = 10479;
					public const int MsdynMsteamssetting = 10480;
					public const int MsdynMsteamssettingsv2 = 10481;
					public const int MicrosoftTeamsCollaborationEntity = 10482;
					public const int TeamsDialerAdminSettings = 10483;
					public const int TeamsContactSuggestionByAI = 10484;
					public const int ContactSuggestionRule = 10485;
					public const int ContactSuggestionRuleset = 10486;
					public const int MicrosoftTeamsChatAssociationEntity = 10487;
					public const int MicrosoftTeamsChatSuggestion = 10488;
					public const int MicrosoftOrgchartNodeEntity = 10489;
					public const int ForecastManualAdjustmentHistory = 10490;
					public const int DistributedLock = 10491;
					public const int EntityDeltaChange = 10492;
					public const int FileUploadStatusTracker = 10493;
					public const int Forecast = 10494;
					public const int ForecastConfiguration = 10495;
					public const int ForecastDefinition = 10496;
					public const int ForecastingCache = 10497;
					public const int ForecastInsights = 10498;
					public const int Forecast_ = 10499;
					public const int ForecastPredictionData = 10500;
					public const int ForecastPredictionStatus = 10501;
					public const int ForecastRecurrence = 10502;
					public const int RecomputeTracker = 10503;
					public const int ForecastRecurrence_ = 10504;
					public const int ShareAsConfiguration = 10505;
					public const int CustomerEmailCommunication = 10506;
					public const int GDPRData = 10507;
					public const int ODOSFeatureMetadata = 10508;
					public const int ODOSMetadata = 10509;
					public const int RecurringSalesAction = 10510;
					public const int RecurringSalesActionV2 = 10511;
					public const int MsdynRelationshipinsightsunifiedconfig = 10512;
					public const int Siconfig = 10513;
					public const int SIKeyValueConfig = 10514;
					public const int UsageMetric = 10515;
					public const int ActionCardRegarding = 10516;
					public const int ActionCardRoleSetting = 10517;
					public const int EntityRankingRule = 10518;
					public const int Flowcardtype = 10519;
					public const int Salesinsightssettings = 10520;
					public const int ActionCardUsage = 10521;
					public const int ActionCardUsageAggregation = 10522;
					public const int AutoCaptureRule = 10523;
					public const int AutoCaptureSettings = 10524;
					public const int UntrackedAppointment = 10525;
					public const int SuggestedActivity = 10526;
					public const int SuggestedActivityDataSource = 10527;
					public const int SuggestedContact = 10528;
					public const int SuggestedContactsDataSource = 10529;
					public const int NotesAnalysisConfig = 10530;
					public const int Icebreakersconfig = 10531;
					public const int Dealmanageraccess = 10532;
					public const int DealManagerSettings = 10533;
					public const int AccountKPIItem = 10534;
					public const int ActivityAnalysisCleanUpState = 10535;
					public const int RelationshipAnalyticsConfig = 10536;
					public const int ContactKPIItem = 10537;
					public const int DailyKpisForAccount = 10538;
					public const int DailyKpisForContact = 10539;
					public const int DailyKpisForLead = 10540;
					public const int DailyKpisForOpportunity = 10541;
					public const int LeadKPIItem = 10542;
					public const int MostContacted = 10543;
					public const int MostContactedBy = 10544;
					public const int OpportunityKPIItem = 10545;
					public const int RelationshipAnalyticsMetadata = 10546;
					public const int SimilarEntitiesFeatureImportance = 10547;
					public const int Wkwcolleaguesforcompany = 10548;
					public const int Wkwcolleaguesforcontact = 10549;
					public const int Wkwconfig = 10550;
					public const int AttributeInfluenceStatistics = 10551;
					public const int PredictionComputationOperation = 10552;
					public const int PredictionModelStatus = 10553;
					public const int PredictionScheduledOperation = 10554;
					public const int PredictiveModelScore = 10555;
					public const int PredictiveScore = 10556;
					public const int PredictiveScoringSyncStatus = 10557;
					public const int RealTimeScoring = 10558;
					public const int RealTimeScoringOperation = 10559;
					public const int SubmodelDefinition = 10560;
					public const int TimeSpentInBPF = 10561;
					public const int TrainingResult = 10562;
					public const int OpportunityModelConfig = 10563;
					public const int LeadModelConfig = 10564;
					public const int ModelPreviewStatus = 10565;
					public const int ProfileAlbum = 10566;
					public const int PostConfiguration = 10567;
					public const int PostRuleConfiguration = 10568;
					public const int WallView = 10569;
					public const int Filter = 10570;
					public const int CustomerVoiceAlert = 10571;
					public const int CustomerVoiceAlertRule = 10572;
					public const int CustomerVoiceSurveyEmailTemplate = 10573;
					public const int CustomerVoiceFileResponse = 10574;
					public const int CustomerVoiceLocalizedSurveyEmailTemplate = 10575;
					public const int CustomerVoiceProject = 10576;
					public const int CustomerVoiceSurveyQuestion = 10577;
					public const int CustomerVoiceSurveyQuestionResponse = 10578;
					public const int CustomerVoiceSatisfactionMetric = 10579;
					public const int CustomerVoiceSurvey = 10580;
					public const int CustomerVoiceSurveyInvite = 10581;
					public const int CustomerVoiceSurveyReminder = 10582;
					public const int CustomerVoiceSurveyResponse = 10583;
					public const int CustomerVoiceUnsubscribedRecipient = 10584;
					public const int AddToCalendarStyle = 10585;
					public const int Basestyle = 10586;
					public const int ButtonStyle = 10587;
					public const int CodeStyle = 10588;
					public const int ColumnStyle = 10589;
					public const int ContentBlock = 10590;
					public const int DividerStyle = 10591;
					public const int GeneralStyles = 10592;
					public const int Imagestyle = 10593;
					public const int LayoutStyle = 10594;
					public const int QRCodeStyle = 10595;
					public const int TextStyle = 10596;
					public const int VideoStyle = 10597;
					public const int AppState = 10598;
					public const int CSAdminConfig = 10599;
					public const int CustomAPIRulesetConfiguration = 10600;
					public const int DecisionContract = 10601;
					public const int DecisionRuleSet = 10602;
					public const int Rulesetentitymapping = 10603;
					public const int RoutingDiagnosticItem = 10604;
					public const int RoutingDiagnostic = 10605;
					public const int InboxCardConfiguration = 10606;
					public const int InboxConfiguration = 10607;
					public const int InboxEntityConfiguration = 10608;
					public const int AppProfileCopilotConfiguration = 10609;
					public const int AgentCopilotSetting = 10610;
					public const int CopilotSummarizationSetting = 10611;
					public const int CaseEnrichment = 10612;
					public const int CaseSuggestion = 10613;
					public const int CaseSuggestionRequestPayload = 10614;
					public const int CaseSuggestionsDataSouce = 10615;
					public const int AgentPreferenceForCopilot = 10616;
					public const int CopilotInteraction = 10617;
					public const int CopilotInteractionData = 10618;
					public const int CopilotTranscript = 10619;
					public const int CopilotTranscriptData = 10620;
					public const int KBEnrichment = 10621;
					public const int KnowledgeArticleSuggestion = 10622;
					public const int KnowledgeArticleSuggestionDataSource = 10623;
					public const int ServiceCopilotPlugin = 10624;
					public const int ServiceCopilotPluginRole = 10625;
					public const int SuggestionInteraction = 10626;
					public const int SuggestionRequestPayload = 10627;
					public const int SuggestionsModelSummary = 10628;
					public const int SuggestionsSetting = 10629;
					public const int Swarm = 10630;
					public const int SwarmParticipant = 10631;
					public const int SwarmParticipantRule = 10632;
					public const int SwarmRole = 10633;
					public const int SwarmSkill = 10634;
					public const int SwarmTemplate = 10635;
					public const int EntityAttachment = 10636;
					public const int CustomerServiceKeyValueConfiguration = 10637;
					public const int MasterEntityRoutingConfiguration = 10638;
					public const int RoutingRuleSetSetting = 10639;
					public const int AssignmentConfiguration = 10640;
					public const int AssignmentConfigurationStep = 10641;
					public const int CapacityProfile = 10642;
					public const int OverflowActionConfig = 10643;
					public const int PreferredAgent = 10644;
					public const int PreferredAgentCustomerIdentity = 10645;
					public const int PreferredAgentRoutedEntity = 10646;
					public const int RoutingConfiguration = 10647;
					public const int RoutingConfigurationStep = 10648;
					public const int CustomMessagingAccount = 10649;
					public const int ChannelConfiguration = 10650;
					public const int ChannelStateConfiguration = 10651;
					public const int ProvisioningState = 10652;
					public const int AdminAppState = 10653;
					public const int AgentStatusHistory = 10654;
					public const int PowerBIConfiguration = 10655;
					public const int AuthenticationSettings = 10656;
					public const int AuthSettingsEntry = 10657;
					public const int QuickReply = 10658;
					public const int EntityRoutingContext = 10659;
					public const int ChannelCapability = 10660;
					public const int ConversationAction = 10661;
					public const int ConversationActionLocale = 10662;
					public const int ConversationMessageBlock = 10663;
					public const int DeletedConversation = 10664;
					public const int DeprecatedWorkstreamEntityConfiguration = 10665;
					public const int Entity_ = 10666;
					public const int OngoingConversation_Deprecated_ = 10667;
					public const int LiveWorkItemEvent = 10668;
					public const int WorkStream = 10669;
					public const int MaskingRule = 10670;
					public const int AutoBlockRule = 10671;
					public const int BotChannelRegistrationSecret = 10672;
					public const int OmnichannelChannelApiConversationPrivilege = 10673;
					public const int OmnichannelChannelApiMessagePrivilege = 10674;
					public const int ChannelApiMethodMapping = 10675;
					public const int ExternalContext = 10676;
					public const int FlaggedSpam = 10677;
					public const int Language_ = 10678;
					public const int Conversation = 10679;
					public const int ContextItemValue = 10682;
					public const int LiveWorkItemParticipant_Deprecated_ = 10683;
					public const int ConversationSentiment = 10684;
					public const int ContextVariable = 10685;
					public const int Localization = 10686;
					public const int OCPaymentProfile = 10687;
					public const int Recording = 10688;
					public const int OmnichannelRequest = 10689;
					public const int RichMessage = 10690;
					public const int RichObjectMap = 10691;
					public const int RuleItem_ = 10692;
					public const int SentimentDailyTopic = 10693;
					public const int SentimentDailyTopicKeyword = 10694;
					public const int SentimentDailyTopicTrending = 10695;
					public const int Session = 10696;
					public const int SessionParticipantEvent = 10697;
					public const int SessionSentiment = 10698;
					public const int Message = 10699;
					public const int Tag = 10700;
					public const int GeoLocationProvider = 10701;
					public const int OmnichannelConfiguration = 10702;
					public const int OmnichannelPersonalization = 10703;
					public const int OmnichannelQueue_Deprecated_ = 10704;
					public const int OmnichannelSyncConfig = 10705;
					public const int OperatingHour = 10706;
					public const int PersonalQuickReply = 10707;
					public const int PersonalSoundSetting = 10708;
					public const int PersonaSecurityRoleMapping = 10709;
					public const int Presence = 10710;
					public const int Provider = 10711;
					public const int RoutingRequest = 10712;
					public const int SearchConfiguration = 10713;
					public const int SentimentAnalysis = 10714;
					public const int SessionEvent = 10715;
					public const int SessionParticipant = 10716;
					public const int AudioFile = 10717;
					public const int SoundNotificationSetting = 10718;
					public const int Transcript = 10719;
					public const int URNotificationTemplate = 10720;
					public const int URNotificationTemplateMapping = 10721;
					public const int UserSettings_ = 10722;
					public const int SelfService = 10723;
					public const int AgentCapacityUpdateHistory = 10730;
					public const int BookableResourceCapacityProfile = 10731;
					public const int WorkStreamCapacityProfile = 10732;
					public const int ConversationCapacityProfile = 10733;
					public const int AgentCapacityProfileUnit = 10734;
					public const int AgentChannelState = 10735;
					public const int AgentStatus = 10736;
					public const int ConversationCharacteristic = 10737;
					public const int SessionCharacteristic = 10738;
					public const int SkillAttachmentRule = 10739;
					public const int AttachSkill = 10740;
					public const int ModelTrainingDetails = 10741;
					public const int TrainingDataImportConfiguration = 10742;
					public const int CharacteristicMapping = 10743;
					public const int TrainingRecord = 10744;
					public const int SkillFinderModel = 10745;
					public const int EffortEstimate = 10746;
					public const int EffortEstimationModel = 10747;
					public const int EffortModelTrainingDetails = 10748;
					public const int ConversationInsight = 10749;
					public const int ActiveICDExtension = 10750;
					public const int EntityWorkstreamMap = 10751;
					public const int ICDExtension = 10752;
					public const int LockStatus = 10753;
					public const int OmnichannelAgentAssignmentCustomApiPrivilege = 10754;
					public const int ConversationActionItem = 10755;
					public const int ConversationAggregatedInsights = 10756;
					public const int Comment__ = 10757;
					public const int ConversationParticipantInsights = 10758;
					public const int ConversationParticipantSentiment = 10759;
					public const int ConversationQuestion = 10760;
					public const int ConversationSegmentSentiment = 10761;
					public const int ConversationSentiment_ = 10762;
					public const int ConversationSignal = 10763;
					public const int ConversationSubject = 10764;
					public const int ConversationSummarySuggestion = 10765;
					public const int ConversationSystemTag = 10766;
					public const int ConversationTag = 10767;
					public const int Recording_ = 10768;
					public const int SCIConversation = 10769;
					public const int CustomEmailHighlight = 10770;
					public const int CustomHighlight = 10771;
					public const int CustomPublisher = 10772;
					public const int EnvironmentSettings = 10773;
					public const int UserSettings__ = 10774;
					public const int CatalogEventStatusConfiguration = 10775;
					public const int Configuration = 10776;
					public const int Trigger = 10777;
					public const int TriggersToSdkMessageProcessingSteps = 10778;
					public const int EventParameterMetadata = 10779;
					public const int TrackingContext = 10780;
					public const int MarketingFeatureConfiguration = 10781;
					public const int MsdynmktExperimentv2 = 10782;
					public const int ACSChannelInstance = 10783;
					public const int ACSChannelInstanceAccount = 10784;
					public const int InfobipChannelInstance = 10785;
					public const int InfobipChannelInstanceAccount = 10786;
					public const int LinkMobilityChannelInstance = 10787;
					public const int LinkMobilityChannelInstanceAccount = 10788;
					public const int MockSmsProviderChannelInstance = 10789;
					public const int MockSmsProviderChannelInstanceAccount = 10790;
					public const int TeleSignChannelInstance = 10791;
					public const int TeleSignChannelInstanceAccount = 10792;
					public const int TwilioChannelInstance = 10793;
					public const int TwilioChannelInstanceAccount = 10794;
					public const int VibesChannelInstance = 10795;
					public const int VibesChannelInstanceAccount = 10796;
					public const int PredefinedPlaceholder = 10797;
					public const int MetadataEntityRelationship = 10798;
					public const int MetadataItem = 10799;
					public const int MetadataStoreState = 10800;
					public const int DigitalSellingActiveTask = 10801;
					public const int DigitalSellingCompletedTask = 10802;
					public const int SalesTag = 10803;
					public const int Sequence = 10804;
					public const int SequenceStat = 10805;
					public const int SequenceTarget = 10806;
					public const int SequenceTargetStep = 10807;
					public const int SequenceTemplate = 10808;
					public const int Sabackupdiagnostic = 10810;
					public const int SABatchRunInstance = 10811;
					public const int Salesroutingdiagnostic = 10812;
					public const int SARunInstance = 10813;
					public const int Segment = 10814;
					public const int Segmentsetting = 10815;
					public const int SegmentProperty = 10816;
					public const int SegmentsUtil = 10817;
					public const int AssignmentRule = 10818;
					public const int SellerAttribute = 10819;
					public const int SellerAttributeValue = 10820;
					public const int AssignmentMap = 10821;
					public const int SalesAssignmentSetting = 10822;
					public const int SalesRoutingRun = 10823;
					public const int ExtendedUserSetting = 10825;
					public const int SalesAccelerationInsights = 10826;
					public const int SalesAccelerationSettings = 10827;
					public const int Insight = 10828;
					public const int WorkListSuggestion = 10829;
					public const int WorkListSuggestionSource = 10830;
					public const int WorkListViewConfiguration = 10831;
					public const int WorkQueueRecord = 10832;
					public const int WorkQueueRecordState = 10833;
					public const int WorkListUserSetting = 10834;
					public const int WQDataSource = 10835;
					public const int SuggestionAssignmentRule = 10836;
					public const int SuggestionPrincipalObjectAccess = 10837;
					public const int SuggestionSellerPriority = 10838;
					public const int DataHygieneSettingInfo = 10839;
					public const int DuplicateDetectionPluginRun = 10840;
					public const int DuplicateLeadMapping = 10841;
					public const int LeadHygieneSetting = 10842;
					public const int LinkedEntityAttributeValidity = 10843;
					public const int SalesProvisioningRequest = 10844;
					public const int SalesOmnichannelMessage = 10845;
					public const int TextMessageTemplate = 10846;
					public const int DataAnalyticsAdminSettings_Deprecated_ = 10847;
					public const int DataAnalyticsReport = 10848;
					public const int Insights = 10849;
					public const int SalesAccelerationReports = 10850;
					public const int BotSession = 10852;
					public const int Conversationsuggestionrequestpayload = 10853;
					public const int DataAnalyticsUserCustomizedReport = 10854;
					public const int DataAnalyticsDataset = 10855;
					public const int DataAnalyticsWorkspace = 10856;
					public const int ReportBookmark = 10857;
					public const int AgentResourceForecasting = 10858;
					public const int _Deprecated_DynamicsCustomerServiceAnalytics = 10859;
					public const int CaseTopic = 10860;
					public const int CaseTopicSetting = 10861;
					public const int CaseTopicSummary = 10862;
					public const int CaseTopicIncidentMapping = 10863;
					public const int CustomerServiceHistoricalAnalytics = 10864;
					public const int Forecast__ = 10865;
					public const int KnowledgeAnalytics = 10866;
					public const int ForecastSummaryAndSetting = 10867;
					public const int KeywordsDescriptionSuggestionSetting = 10868;
					public const int ConversationSummaryInteraction = 10869;
					public const int ConversationSummarySetting = 10870;
					public const int ConversationTopic = 10871;
					public const int ConversationTopicSetting = 10872;
					public const int ConversationTopicSummary = 10873;
					public const int ConversationTopicConversationMapping = 10874;
					public const int OmnichannelHistoricalAnalytics = 10875;
					public const int OmnichannelVoiceHistoricalAnalytics_preview__Deprecated_ = 10876;
					public const int OmnichannelRealtimeAnalytics = 10877;
					public const int Booking = 10878;
					public const int RequirementCharacteristic = 10981;
					public const int RequirementOrganizationUnit = 10982;
					public const int BookingSetupMetadata = 10983;
					public const int Configuration_ = 10984;
					public const int BookableResourceAssociation = 10985;
					public const int Actual = 10986;
					public const int ClientExtension = 10987;
					public const int RequirementGroup = 10988;
					public const int RequirementRelationship = 10989;
					public const int BookingAlert = 10990;
					public const int BookingAlertStatus = 10991;
					public const int BookingChange = 10992;
					public const int BookingRule = 10993;
					public const int BusinessClosure = 10994;
					public const int OrganizationalUnit = 10995;
					public const int Priority = 10996;
					public const int RequirementResourceCategory = 10997;
					public const int RequirementResourcePreference = 10998;
					public const int RequirementStatus = 10999;
					public const int ResourceRequirement = 11000;
					public const int ResourceRequirementDetail = 11001;
					public const int ResourceTerritory = 11002;
					public const int ScheduleBoardSetting = 11003;
					public const int SchedulingParameter = 11004;
					public const int SystemUserSchedulerSetting = 11005;
					public const int FulfillmentPreference = 11006;
					public const int TimeGroupDetail = 11007;
					public const int TransactionOrigin = 11008;
					public const int WorkTemplate = 11009;
					public const int RequirementChange = 11010;
					public const int RequirementDependency = 11011;
					public const int SchedulingFeatureFlag = 11012;
					public const int AccountProjectPriceList = 11013;
					public const int ProjectServiceApproval = 11014;
					public const int BatchJob = 11015;
					public const int ProjectStages = 11016;
					public const int InvoiceProcess = 11017;
					public const int CompetencyRequirement_Deprecated_ = 11018;
					public const int ContactPriceList = 11019;
					public const int ProjectContractLineInvoiceSchedule = 11020;
					public const int ProjectContractLineMilestone = 11021;
					public const int ActualDataExport_Deprecated_ = 11022;
					public const int Delegation = 11023;
					public const int PricingDimension = 11024;
					public const int PricingDimensionFieldName = 11025;
					public const int Estimate = 11026;
					public const int EstimateLine = 11027;
					public const int Expense = 11028;
					public const int ExpenseCategory = 11029;
					public const int ExpenseReceipt = 11030;
					public const int Fact = 11031;
					public const int FieldComputation = 11032;
					public const int FindWorkEvent_DeprecatedInV3_0_ = 11033;
					public const int IntegrationJob = 11034;
					public const int IntegrationJobDetail = 11035;
					public const int InvoiceFrequency = 11036;
					public const int InvoiceFrequencyDetail = 11037;
					public const int InvoiceLineDetail = 11038;
					public const int Journal = 11039;
					public const int JournalLine = 11040;
					public const int ResultCache = 11041;
					public const int OpportunityLineResourceCategory_Deprecated_ = 11042;
					public const int OpportunityLineDetail_Deprecated_ = 11043;
					public const int OpportunityLineTransactionCategory_Deprecated_ = 11044;
					public const int OpportunityLineTransactionClassification_Deprecated_ = 11045;
					public const int OpportunityProjectPriceList = 11046;
					public const int ProjectContractLineResourceCategory = 11047;
					public const int ProjectContractLineDetail = 11048;
					public const int ProjectContractLineTransactionCategory = 11049;
					public const int ProjectContractLineTransactionClassification = 11050;
					public const int ProjectContractProjectPriceList = 11051;
					public const int ProcessNotes = 11052;
					public const int Project = 11053;
					public const int ProjectApproval = 11054;
					public const int ProjectParameter = 11055;
					public const int ProjectParameterPriceList = 11056;
					public const int ProjectPriceList = 11057;
					public const int ProjectTask = 11058;
					public const int ProjectTaskDependency = 11059;
					public const int ProjectTaskStatusUser = 11060;
					public const int ProjectTeamMember = 11061;
					public const int ProjectTeamMemberSignUp_DeprecatedInV3_0_ = 11062;
					public const int ProjectTransactionCategory_Deprecated_ = 11063;
					public const int QuoteLineAnalyticsBreakdown = 11064;
					public const int QuoteLineInvoiceSchedule = 11065;
					public const int QuoteLineResourceCategory = 11066;
					public const int QuoteLineMilestone = 11067;
					public const int QuoteLineDetail = 11068;
					public const int QuoteLineTransactionCategory = 11069;
					public const int QuoteLineTransactionClassification = 11070;
					public const int QuoteProjectPriceList = 11071;
					public const int ResourceAssignment = 11072;
					public const int ResourceAssignmentDetail_DeprecatedInV2_0_ = 11073;
					public const int RolePriceMarkup = 11074;
					public const int RolePrice = 11075;
					public const int ResourceRequest = 11076;
					public const int RoleCompetencyRequirement = 11077;
					public const int RoleUtilization = 11078;
					public const int TimeEntry = 11079;
					public const int TimeOffCalendar = 11080;
					public const int TransactionCategory = 11081;
					public const int TransactionCategoryClassification = 11082;
					public const int TransactionCategoryHierarchyElement = 11083;
					public const int TransactionCategoryPrice = 11084;
					public const int TransactionConnection = 11085;
					public const int TransactionType = 11086;
					public const int UserWorkHistory = 11087;
					public const int TimeSource = 11094;
					public const int ApprovalSet = 11095;
					public const int ContractLineDetailPerformance = 11096;
					public const int ContractPerformance = 11097;
					public const int ThreeDimensionalModel = 11098;
					public const int InspectionTemplate = 11099;
					public const int InspectionAttachment = 11100;
					public const int InspectionTemplateVersion = 11101;
					public const int Inspection = 11102;
					public const int InspectionResponse = 11103;
					public const int FunctionalLocationType = 11104;
					public const int LocationTemplateAssociation = 11105;
					public const int FunctionalLocationTypeTemplateAssociation = 11106;
					public const int PropertyLocationAssociation = 11107;
					public const int Warranty = 11108;
					public const int PaymentTerm = 11112;
					public const int PurchaseOrder = 11113;
					public const int PurchaseOrderProduct = 11114;
					public const int PurchaseOrderReceipt = 11115;
					public const int PurchaseOrderReceiptProduct = 11116;
					public const int ShipVia = 11117;
					public const int TaxCode = 11118;
					public const int TaxCodeDetail = 11119;
					public const int Warehouse = 11120;
					public const int Agreement = 11121;
					public const int AgreementBookingDate = 11122;
					public const int AgreementBookingIncident = 11123;
					public const int AgreementBookingProduct = 11124;
					public const int AgreementBookingService = 11125;
					public const int AgreementBookingServiceTask = 11126;
					public const int AgreementBookingSetup = 11127;
					public const int AgreementInvoiceDate = 11128;
					public const int AgreementInvoiceProduct = 11129;
					public const int AgreementInvoiceSetup = 11130;
					public const int AgreementSubstatus = 11131;
					public const int BookingJournal = 11132;
					public const int BookingTimestamp = 11133;
					public const int PurchaseOrderBusinessProcess = 11134;
					public const int CaseToWorkOrderBusinessProcess = 11135;
					public const int AgreementBusinessProcess = 11136;
					public const int WorkOrderBusinessProcess = 11137;
					public const int EntitlementApplication = 11138;
					public const int FieldServicePriceListItem = 11139;
					public const int FieldServiceSetting = 11140;
					public const int FieldServiceSLAConfiguration = 11141;
					public const int FieldServiceSystemJob = 11142;
					public const int IncidentType = 11143;
					public const int IncidentTypeCharacteristic = 11144;
					public const int IncidentTypeProduct = 11145;
					public const int IncidentTypeService = 11146;
					public const int IncidentTypeServiceTask = 11147;
					public const int IncidentTypesSetup = 11148;
					public const int IncidentTypeRequirementGroup = 11149;
					public const int InventoryAdjustment = 11150;
					public const int InventoryAdjustmentProduct = 11151;
					public const int InventoryJournal = 11152;
					public const int InventoryTransfer = 11153;
					public const int OrderInvoicingDate = 11154;
					public const int OrderInvoicingProduct = 11155;
					public const int OrderInvoicingSetup = 11156;
					public const int OrderInvoicingSetupDate = 11157;
					public const int Payment = 11158;
					public const int PaymentDetail = 11159;
					public const int PaymentMethod = 11160;
					public const int PostalCode = 11161;
					public const int ProductInventory = 11162;
					public const int PurchaseOrderBill = 11163;
					public const int PurchaseOrderSubStatus = 11164;
					public const int QuoteBookingIncident = 11165;
					public const int QuoteBookingProduct = 11166;
					public const int QuoteBookingService = 11167;
					public const int QuoteBookingServiceTask = 11168;
					public const int QuoteBookingSetup = 11169;
					public const int QuoteInvoicingProduct = 11170;
					public const int QuoteInvoicingSetup = 11171;
					public const int ResourcePayType = 11172;
					public const int RMA = 11173;
					public const int RMAProduct = 11174;
					public const int RMAReceipt = 11175;
					public const int RMAReceiptProduct = 11176;
					public const int RMASubStatus = 11177;
					public const int RTV = 11178;
					public const int RTVProduct = 11179;
					public const int RTVSubstatus = 11180;
					public const int ServiceTaskType = 11181;
					public const int TimeOffRequest = 11182;
					public const int UniqueNumber = 11183;
					public const int WorkOrder = 11184;
					public const int WorkOrderCharacteristic_Deprecated_ = 11185;
					public const int WorkOrderDetailsGenerationQueue_Deprecated_ = 11186;
					public const int WorkOrderIncident = 11187;
					public const int WorkOrderProduct = 11188;
					public const int ResourceRestriction_Deprecated_ = 11189;
					public const int WorkOrderService = 11190;
					public const int WorkOrderServiceTask = 11191;
					public const int WorkOrderSubstatus = 11192;
					public const int WorkOrderType = 11193;
					public const int BookableResourceBookingQuickNote = 11195;
					public const int FieldServiceFrontlineWorkerConfiguration = 11196;
					public const int IncidentTypeSuggestionResult = 11199;
					public const int IncidentTypeSuggestionRunHistory = 11200;
					public const int IncidentTypeResolution = 11201;
					public const int Insurance = 11202;
					public const int NotToExceed = 11203;
					public const int AssetSuggestion = 11204;
					public const int ProblematicAssetFeedback = 11205;
					public const int Resolution = 11206;
					public const int Trade = 11207;
					public const int TradeCoverage = 11208;
					public const int WorkOrderNotToExceed = 11211;
					public const int WorkOrderResolution = 11212;
					public const int CFSIoTAlertProcessFlow = 11213;
					public const int GeolocationSettings = 11214;
					public const int GeolocationTracking = 11215;
					public const int EntityConfiguration = 11216;
					public const int Geofence = 11217;
					public const int GeofenceEvent = 11218;
					public const int GeofencingSettings = 11219;
					public const int AssetSuggestionsSetting = 11220;
					public const int FieldServiceHistoricalAnalytics = 11221;
					public const int ResourceDuration_preview_ = 11222;
					public const int PredictiveDuration_preview_ = 11223;
					public const int PredictiveWorkHourDurationSetting = 11224;
					public const int MobileSource = 11225;
					public const int Channel = 11226;
					public const int Scenario = 11227;
					public const int SurveyAnswerOption = 11228;
					public const int SurveyResponse = 11229;
					public const int SurveyResponseValue = 11230;
					public const int ChatWidgetLanguage_deprecated_ = 11231;
					public const int ChatWidget = 11232;
					public const int LiveChatContext = 11233;
					public const int ChatWidgetLocation = 11234;
					public const int LocalizedSurveyQuestion_Deprecated_ = 11235;
					public const int SurveyQuestionSequence = 11236;
					public const int SurveyQuestion = 11237;
					public const int CommunicationProviderSetting = 11238;
					public const int CommunicationProviderSettingEntry = 11239;
					public const int PhoneNumber = 11240;
					public const int Carrier = 11241;
					public const int SMSNumberSettings = 11242;
					public const int SMSEngagementContext = 11243;
					public const int SMSNumber = 11244;
					public const int SMSSettingSecret = 11245;
					public const int FacebookEngagementContext = 11246;
					public const int FacebookApplication = 11247;
					public const int FacebookPage = 11248;
					public const int CustomMessagingEngagementContext = 11249;
					public const int LINEEngagementContext = 11250;
					public const int CustomMessagingChannel = 11251;
					public const int LINEAccount = 11252;
					public const int TwitterAccount = 11253;
					public const int TwitterHandle = 11254;
					public const int WeChatAccount = 11255;
					public const int WhatsAppAccount = 11256;
					public const int WhatsAppNumber = 11257;
					public const int TwitterEngagementContext = 11258;
					public const int WeChatEngagementContext = 11259;
					public const int WhatsAppEngagementContext = 11260;
					public const int AppleMessagesForBusinessAccount = 11261;
					public const int AppleMessagesForBusinessEngagementContext = 11262;
					public const int OCApplePayEntity = 11263;
					public const int Google_sBusinessMessagesAgentAccount = 11264;
					public const int Google_sBusinessMessagesPartnerAccount = 11265;
					public const int Google_sBusinessMessagesEngagementContext = 11266;
					public const int TwitterHandleProvisioningStatus = 11267;
					public const int OCTwitterHandleSecret = 11268;
					public const int MicrosoftTeamsAccount = 11269;
					public const int _Deprecated_TeamsEngagementContext = 11270;
					public const int TeamsEngagementContext = 11271;
					public const int OutboundConfiguration = 11272;
					public const int OutboundMessage = 11273;
					public const int UIIAction = 11274;
					public const int UIIAudit = 11275;
					public const int UIIContext = 11276;
					public const int HostedControl = 11277;
					public const int UIINonHostedApplication = 11278;
					public const int Option = 11279;
					public const int UIISavedSession = 11280;
					public const int UIISessionTransfer = 11281;
					public const int UIIWorkflow = 11282;
					public const int UIIWorkflowStep = 11283;
					public const int UIIWorkflowStepMapping = 11284;
					public const int ActionCallWorkflow = 11285;
					public const int ActionCall = 11286;
					public const int AgentScriptTaskCategory = 11287;
					public const int AgentScriptAnswer = 11288;
					public const int Audit_DiagnosticsSetting = 11289;
					public const int Configuration__ = 11290;
					public const int CustomizationFile = 11291;
					public const int EntityType = 11292;
					public const int EntitySearch = 11293;
					public const int Form = 11294;
					public const int LanguageModule = 11295;
					public const int Scriptlet = 11296;
					public const int ScriptTaskTrigger = 11297;
					public const int CTISearch = 11298;
					public const int SessionInformation = 11299;
					public const int SessionTransfer = 11300;
					public const int AgentScriptTask = 11301;
					public const int ToolbarButton = 11302;
					public const int Toolbar = 11303;
					public const int TraceSourceSetting = 11304;
					public const int UnifiedInterfaceSettings = 11305;
					public const int Event = 11306;
					public const int UserSetting = 11307;
					public const int WindowNavigationRule = 11308;
					public const int ViewAsExampleQuestion = 11333;
					public const int SalesUsageTelemetryReports = 11389;
					public const int SalesUsageReporting = 11390;
					public const int PMSimulation = 11391;
					public const int SurveySetting = 11392;
					public const int Carrier_ = 11393;
					public const int CarrierDependencyCheck = 11394;
					public const int CarrierMissingDependency = 11395;
					public const int Workbench = 11396;
					public const int WorkbenchHistory = 11397;
					public const int AutonomousCaseCreationAndEnrichmentRules = 11406;
					public const int CopilotAnalytics = 11410;
					public const int FederatedKnowledgeConfiguration = 11411;
					public const int FederatedKnowledgeEntityConfiguration = 11412;
					public const int FormMapping = 11423;
					public const int EventExpanderBreadcrumb = 18085;
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
					public const int Opportunity = 3;
					public const int Lead = 4;
					public const int Note = 5;
					public const int BusinessUnitMap = 6;
					public const int Owner = 7;
					public const int User = 8;
					public const int Team = 9;
					public const int BusinessUnit = 10;
					public const int SystemUserPrincipal = 14;
					public const int AccountLeads = 16;
					public const int ContactInvoices = 17;
					public const int ContactQuotes = 18;
					public const int ContactOrders = 19;
					public const int ServiceContractContact = 20;
					public const int ProductSalesLiterature = 21;
					public const int ContactLeads = 22;
					public const int LeadCompetitors = 24;
					public const int OpportunityCompetitors = 25;
					public const int CompetitorSalesLiterature = 26;
					public const int LeadProduct = 27;
					public const int Subscription = 29;
					public const int FilterTemplate = 30;
					public const int PrivilegeObjectTypeCode = 31;
					public const int SalesProcessInstance = 32;
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
					public const int PrincipalEntityBusinessUnitMap = 61;
					public const int RecordFilter = 72;
					public const int EntityRecordFilter = 73;
					public const int SecuredMaskingRule = 74;
					public const int VirtualEntityDataProvider = 78;
					public const int VirtualEntityDataSource = 85;
					public const int TeamTemplate = 92;
					public const int SocialProfile = 99;
					public const int ServicePlan = 101;
					public const int PrivilegesRemovalSetting = 103;
					public const int Case = 112;
					public const int ChildIncidentCount = 113;
					public const int Competitor = 123;
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
					public const int PhoneToCaseProcess = 952;
					public const int OpportunitySalesProcess = 953;
					public const int LeadToOpportunitySalesProcess = 954;
					public const int ExpiredProcess = 955;
					public const int Attachment = 1001;
					public const int Attachment_ = 1002;
					public const int InternalAddress = 1003;
					public const int CompetitorAddress = 1004;
					public const int CompetitorProduct = 1006;
					public const int ImageDescriptor = 1007;
					public const int Contract = 1010;
					public const int ContractLine = 1011;
					public const int Discount = 1013;
					public const int ArticleTemplate = 1016;
					public const int LeadAddress = 1017;
					public const int Organization = 1019;
					public const int OrganizationUI = 1021;
					public const int PriceList = 1022;
					public const int Privilege = 1023;
					public const int Product = 1024;
					public const int ProductAssociation = 1025;
					public const int PriceListItem = 1026;
					public const int ProductRelationship = 1028;
					public const int SystemForm = 1030;
					public const int UserDashboard = 1031;
					public const int SecurityRole = 1036;
					public const int RoleTemplate = 1037;
					public const int SalesLiterature = 1038;
					public const int View = 1039;
					public const int StringMap = 1043;
					public const int Property = 1048;
					public const int PropertyOptionSetItem = 1049;
					public const int Unit = 1055;
					public const int UnitGroup = 1056;
					public const int SalesAttachment = 1070;
					public const int Address = 1071;
					public const int SubscriptionClients = 1072;
					public const int StatusMap = 1075;
					public const int DiscountList = 1080;
					public const int ArticleComment = 1082;
					public const int OpportunityLine = 1083;
					public const int Quote = 1084;
					public const int QuoteLine = 1085;
					public const int UserFiscalCalendar = 1086;
					public const int Order = 1088;
					public const int OrderLine = 1089;
					public const int Invoice = 1090;
					public const int InvoiceLine = 1091;
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
					public const int Characteristic = 1141;
					public const int RatingValue = 1142;
					public const int RatingModel = 1144;
					public const int BookableResourceBooking = 1145;
					public const int BookableResourceBookingHeader = 1146;
					public const int BookableResourceCategory = 1147;
					public const int BookableResourceCharacteristic = 1148;
					public const int BookableResourceCategoryAssn = 1149;
					public const int BookableResource = 1150;
					public const int BookableResourceGroup = 1151;
					public const int BookingStatus = 1152;
					public const int DocumentSuggestions = 1189;
					public const int SuggestionCardTemplate = 1190;
					public const int FieldSecurityProfile = 1200;
					public const int FieldPermission = 1201;
					public const int TeamProfiles = 1203;
					public const int Application = 1204;
					public const int ChannelPropertyGroup = 1234;
					public const int PropertyAssociation = 1235;
					public const int ChannelProperty = 1236;
					public const int SocialInsightsConfiguration = 1300;
					public const int SavedOrganizationInsightsConfiguration = 1309;
					public const int PropertyInstance = 1333;
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
					public const int ContractTemplate = 2011;
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
					public const int Facility_Equipment = 4000;
					public const int Service = 4001;
					public const int Resource = 4002;
					public const int Calendar = 4003;
					public const int CalendarRule = 4004;
					public const int SchedulingGroup = 4005;
					public const int ResourceSpecification = 4006;
					public const int ResourceGroup = 4007;
					public const int Site = 4009;
					public const int ResourceExpansion = 4010;
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
					public const int CaseResolution = 4206;
					public const int Letter = 4207;
					public const int OpportunityClose = 4208;
					public const int OrderClose = 4209;
					public const int PhoneCall = 4210;
					public const int QuoteClose = 4211;
					public const int Task = 4212;
					public const int ServiceActivity = 4214;
					public const int Commitment = 4215;
					public const int SocialActivity = 4216;
					public const int UntrackedEmail = 4220;
					public const int SavedView = 4230;
					public const int MetadataDifference = 4231;
					public const int BusinessDataLocalizedLabel = 4232;
					public const int RecurrenceRule = 4250;
					public const int RecurringAppointment = 4251;
					public const int EmailSearch = 4299;
					public const int MarketingList = 4300;
					public const int MarketingListMember = 4301;
					public const int Campaign = 4400;
					public const int CampaignResponse = 4401;
					public const int CampaignActivity = 4402;
					public const int CampaignItem = 4403;
					public const int CampaignActivityItem = 4404;
					public const int BulkOperationLog = 4405;
					public const int QuickCampaign = 4406;
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
					public const int BookableResourceBookingToExchangeIdMapping = 4421;
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
					public const int OpportunityRelationship = 4503;
					public const int EntitlementTemplateProduct = 4545;
					public const int Auditing = 4567;
					public const int RibbonClientMetadata_ = 4579;
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
					public const int EntitlementProduct = 6363;
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
					public const int EntitlementContact = 7272;
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
					public const int Entitlement = 9700;
					public const int EntitlementChannel = 9701;
					public const int EntitlementTemplate = 9702;
					public const int EntitlementTemplateChannel = 9703;
					public const int EntitlementEntityAllocationTypeMapping = 9704;
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
					public const int IncidentKnowledgeBaseRecord = 9931;
					public const int TimeStampDateMapping = 9932;
					public const int AzureServiceConnection = 9936;
					public const int DocumentTemplate = 9940;
					public const int PersonalDocumentTemplate = 9941;
					public const int TopicModelConfiguration = 9942;
					public const int TopicModelExecutionHistory = 9943;
					public const int TopicModel = 9944;
					public const int TextAnalyticsEntityMapping = 9945;
					public const int TopicHistory = 9946;
					public const int KnowledgeSearchModel = 9947;
					public const int TextAnalyticsTopic = 9948;
					public const int AdvancedSimilarityRule = 9949;
					public const int OfficeGraphDocument = 9950;
					public const int SimilarityRule = 9951;
					public const int KnowledgeArticle = 9953;
					public const int KnowledgeArticleIncident = 9954;
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
					public const int StagedEntity = 10019;
					public const int StagedEntityAttribute = 10020;
					public const int StagedMetadataAsyncOperation = 10021;
					public const int KeyVaultReference = 10022;
					public const int ManagedIdentity = 10023;
					public const int Catalog = 10024;
					public const int CatalogAssignment = 10025;
					public const int InternalCatalogAssignment = 10026;
					public const int CustomAPI = 10027;
					public const int CustomAPIRequestParameter = 10028;
					public const int CustomAPIResponseProperty = 10029;
					public const int PluginPackage = 10030;
					public const int NonRelationalDataSource = 10031;
					public const int ProvisionLanguageForUser = 10032;
					public const int SharedObject = 10033;
					public const int SharedWorkspace = 10034;
					public const int SharedWorkspaceAccessToken = 10035;
					public const int SharedWorkspaceNonRelational = 10036;
					public const int SharedWorkspacePool = 10037;
					public const int DataLakeFolder = 10038;
					public const int DataLakeFolderPermission = 10039;
					public const int DataLakeWorkspace = 10040;
					public const int DataLakeWorkspacePermission = 10041;
					public const int DataProcessingConfiguration = 10042;
					public const int ExportedExcel = 10043;
					public const int RetainedDataExcel = 10044;
					public const int SynapseDatabase = 10045;
					public const int SynapseLinkExternalTableState = 10046;
					public const int SynapseLinkProfile = 10047;
					public const int SynapseLinkProfileEntity = 10048;
					public const int SynapseLinkProfileEntityState = 10049;
					public const int SynapseLinkSchedule = 10050;
					public const int ComponentVersion = 10051;
					public const int ComponentVersionDataSource = 10052;
					public const int ComponentVersion_Internal_ = 10053;
					public const int DataflowRefreshHistory = 10054;
					public const int EntityRefreshHistory = 10055;
					public const int SharedLinkSetting = 10056;
					public const int DelegatedAuthorization = 10057;
					public const int CascadeGrantRevokeAccessRecordsTracker = 10059;
					public const int CascadeGrantRevokeAccessVersionTracker = 10060;
					public const int RevokeInheritedAccessRecordsTracker = 10061;
					public const int TdsMetadata = 10062;
					public const int ModelDrivenAppElement = 10063;
					public const int ModelDrivenAppComponentNode_sEdge = 10064;
					public const int ModelDrivenAppComponentNode = 10065;
					public const int ModelDrivenAppSetting = 10066;
					public const int ModelDrivenAppUserSetting = 10067;
					public const int OrganizationSetting = 10068;
					public const int SettingDefinition = 10069;
					public const int CanvasAppExtendedMetadata = 10070;
					public const int ServicePlanMapping = 10071;
					public const int ServicePlanCustomControl = 10072;
					public const int ApplicationUser = 10074;
					public const int ODataV4DataSource = 10077;
					public const int WorkflowBinary = 10078;
					public const int Credential = 10079;
					public const int DesktopFlowModule = 10080;
					public const int FlowCapacityAssignment = 10081;
					public const int FlowCredentialApplication = 10082;
					public const int FlowEvent = 10083;
					public const int FlowMachine = 10084;
					public const int FlowMachineGroup = 10085;
					public const int FlowMachineImage = 10086;
					public const int FlowMachineImageVersion = 10087;
					public const int FlowMachineNetwork = 10088;
					public const int ProcessStageParameter = 10089;
					public const int WorkQueue = 10090;
					public const int WorkQueueItem = 10091;
					public const int DesktopFlowBinary = 10092;
					public const int FlowLog = 10093;
					public const int FlowRun = 10094;
					public const int ConnectionReference = 10095;
					public const int DVFileSearch = 10096;
					public const int DVFileSearchAttribute = 10097;
					public const int DVFileSearchEntity = 10098;
					public const int DVTableSearch = 10099;
					public const int DVTableSearchAttribute = 10100;
					public const int DVTableSearchEntity = 10101;
					public const int AICopilot = 10102;
					public const int AIPluginAuth = 10103;
					public const int AIPluginConversationStarter = 10104;
					public const int AIPluginConversationStarterMapping = 10105;
					public const int AIPluginGovernance = 10106;
					public const int AIPluginGovernanceExtended = 10107;
					public const int AIPluginOperationResponseTemplate = 10108;
					public const int AIPluginTitle = 10109;
					public const int SideloadedAIPlugin = 10110;
					public const int AIPlugin = 10111;
					public const int AIPluginExternalSchema = 10112;
					public const int AIPluginExternalSchemaProperty = 10113;
					public const int AIPluginInstance = 10114;
					public const int AIPluginOperation = 10115;
					public const int AIPluginOperationParameter = 10116;
					public const int AIPluginUserSetting = 10117;
					public const int AIEvent = 10119;
					public const int AIBuilderFeedbackLoop = 10120;
					public const int AIFormProcessingDocument = 10121;
					public const int AIObjectDetectionImage = 10122;
					public const int AIObjectDetectionLabel = 10123;
					public const int AIObjectDetectionBoundingBox = 10124;
					public const int AIObjectDetectionImageMapping = 10125;
					public const int AIBuilderDataset = 10127;
					public const int AIBuilderDatasetFile = 10128;
					public const int AIBuilderDatasetRecord = 10129;
					public const int AIBuilderDatasetsContainer = 10130;
					public const int AIBuilderFile = 10131;
					public const int AIBuilderFileAttachedData = 10132;
					public const int HelpPage = 10133;
					public const int Tour = 10134;
					public const int BotContent = 10135;
					public const int ConversationTranscript = 10136;
					public const int Chatbot = 10137;
					public const int ChatbotSubcomponent = 10138;
					public const int ChatbotComponentCollection = 10139;
					public const int Comment_ = 10150;
					public const int FabricAISkill = 10151;
					public const int AppInsightsMetadata = 10152;
					public const int DataflowConnectionReference = 10153;
					public const int Schedule = 10154;
					public const int DataflowTemplate = 10155;
					public const int DataflowDatalakeFolder = 10156;
					public const int DataMovementServiceRequest = 10157;
					public const int DataMovementServiceRequestStatus = 10158;
					public const int DMSSyncRequest = 10159;
					public const int DMSSyncStatus = 10160;
					public const int KnowledgeAssetConfiguration = 10161;
					public const int ModuleRunDetail = 10162;
					public const int SalesforceStructuredObject = 10163;
					public const int SalesforceStructuredQnAConfig = 10164;
					public const int WorkflowActionStatus = 10165;
					public const int PDFSetting = 10166;
					public const int ActivityFileAttachment = 10167;
					public const int TeamsChat = 10168;
					public const int ServiceConfiguration = 10169;
					public const int SLAKPI = 10170;
					public const int IntegratedSearchProvider = 10171;
					public const int KnowledgeManagementSetting = 10172;
					public const int KnowledgeFederatedArticle = 10173;
					public const int KnowledgeFederatedArticleIncident = 10174;
					public const int SearchProvider = 10175;
					public const int KnowledgeArticleImage = 10176;
					public const int KnowledgeConfiguration = 10177;
					public const int KnowledgeInteractionInsight = 10178;
					public const int KnowledgeSearchInsight = 10179;
					public const int FavoriteKnowledgeArticle = 10180;
					public const int KnowledgeArticleLanguageSetting = 10181;
					public const int KnowledgeArticleAttachment = 10182;
					public const int KnowledgePersonalization = 10183;
					public const int KnowledgeArticleTemplate = 10184;
					public const int KnowledgeSearchPersonalFilterConfig = 10185;
					public const int KnowledgeSearchFilter = 10186;
					public const int SupportUserTable = 10188;
					public const int FxExpression = 10189;
					public const int PowerfxRule = 10190;
					public const int PlannerBusinessScenario = 10191;
					public const int PlannerSyncAction = 10192;
					public const int MsGraphResourceToSubscription = 10193;
					public const int VirtualEntityMetadata = 10194;
					public const int BackgroundOperation = 10195;
					public const int MobileOfflineProfileExtension = 10196;
					public const int MobileOfflineProfileItemFilter = 10197;
					public const int TeamMobileOfflineProfileMembership = 10198;
					public const int UserMobileOfflineProfileMembership = 10199;
					public const int OrganizationDataSyncSubscription = 10200;
					public const int OrganizationDataSyncSubscriptionEntity = 10201;
					public const int OrganizationDataSyncSubscriptionFnoTable = 10202;
					public const int OrganizationDataSyncFnoState = 10203;
					public const int OrganizationDataSyncState = 10204;
					public const int ArchiveCleanupInfo = 10205;
					public const int ArchiveCleanupOperation = 10206;
					public const int BulkArchiveConfig = 10207;
					public const int BulkArchiveFailureDetail = 10208;
					public const int BulkArchiveOperation = 10209;
					public const int BulkArchiveOperationDetail = 10210;
					public const int EnableArchivalRequest = 10211;
					public const int MetadataForArchival = 10212;
					public const int ReconciliationEntityInfo = 10213;
					public const int ReconciliationEntityStepInfo = 10214;
					public const int ReconciliationInfo = 10215;
					public const int RetentionCleanupInfo = 10216;
					public const int RetentionCleanupOperation = 10217;
					public const int RetentionConfig = 10218;
					public const int RetentionFailureDetail = 10219;
					public const int RetentionOperation = 10220;
					public const int RetentionOperationDetail = 10221;
					public const int Notification_ = 10222;
					public const int UserRating = 10223;
					public const int MobileApp = 10224;
					public const int InsightsStoreDataSource = 10225;
					public const int InsightsStoreVirtualEntity = 10226;
					public const int RoleEditorLayout = 10227;
					public const int DeletedRecordReference = 10228;
					public const int RestoreDeletedRecordsConfiguration = 10229;
					public const int AppAction = 10230;
					public const int AppActionMigration = 10231;
					public const int AppActionRule = 10232;
					public const int Card = 10235;
					public const int CardStateItem = 10236;
					public const int EntityLinkChatConfiguration = 10239;
					public const int RichTextAttachment = 10240;
					public const int CustomControlExtendedSetting = 10241;
					public const int TimelinePin = 10242;
					public const int VirtualConnectorDataSource = 10243;
					public const int VirtualTableColumnCandidate = 10244;
					public const int PMAnalysisHistory = 10245;
					public const int PMBusinessRuleAutomationConfig = 10246;
					public const int PMCalendar = 10247;
					public const int PMCalendarVersion = 10248;
					public const int PMInferredTask = 10249;
					public const int PMProcessExtendedMetadataVersion = 10250;
					public const int PMProcessTemplate = 10251;
					public const int PMProcessUserSettings = 10252;
					public const int PMProcessVersion = 10253;
					public const int PMRecording = 10254;
					public const int PMTemplate = 10255;
					public const int PMView = 10256;
					public const int AnalysisComponent = 10257;
					public const int AnalysisJob = 10258;
					public const int AnalysisOverride = 10259;
					public const int AnalysisResult = 10260;
					public const int AnalysisResultDetail = 10261;
					public const int SolutionHealthRule = 10262;
					public const int SolutionHealthRuleArgument = 10263;
					public const int SolutionHealthRuleSet = 10264;
					public const int PowerBIDataset = 10265;
					public const int Powerbidatasetapdx = 10266;
					public const int PowerBIMashupParameter = 10267;
					public const int PowerBIReport = 10268;
					public const int Powerbireportapdx = 10269;
					public const int FileUpload = 10270;
					public const int MainFewShot = 10271;
					public const int MakerFewShot = 10272;
					public const int SearchAttributeSettings = 10273;
					public const int SearchCustomAnalyzer = 10274;
					public const int SearchRelationshipSettings = 10275;
					public const int SearchResultsCache = 10276;
					public const int SearchTelemetry = 10277;
					public const int CopilotExampleQuestion = 10278;
					public const int CopilotGlossaryTerm = 10279;
					public const int CopilotSynonyms = 10280;
					public const int SiteComponent = 10281;
					public const int Site_ = 10282;
					public const int SiteLanguage = 10283;
					public const int PowerPagesSitePublished = 10284;
					public const int ExternalIdentity = 10287;
					public const int Invitation = 10288;
					public const int InviteRedemption = 10289;
					public const int PortalComment = 10290;
					public const int Setting = 10291;
					public const int MultistepFormSession = 10292;
					public const int AdPlacement = 10296;
					public const int ColumnPermission = 10297;
					public const int ColumnPermissionProfile = 10298;
					public const int ContentSnippet = 10299;
					public const int BasicForm = 10300;
					public const int BasicFormMetadata = 10301;
					public const int List = 10302;
					public const int TablePermission = 10303;
					public const int PageTemplate = 10304;
					public const int PollPlacement = 10305;
					public const int PowerPagesCoreEntityDS = 10306;
					public const int PublishingState = 10307;
					public const int PublishingStateTransitionRule = 10308;
					public const int Redirect = 10309;
					public const int Shortcut = 10310;
					public const int SiteMarker = 10311;
					public const int SiteSetting = 10312;
					public const int WebFile = 10313;
					public const int MultistepForm = 10314;
					public const int MultistepFormMetadata = 10315;
					public const int FormStep = 10316;
					public const int WebLink = 10317;
					public const int WebLinkSet = 10318;
					public const int WebPage = 10319;
					public const int WebPageAccessControlRule = 10320;
					public const int WebRole = 10321;
					public const int Website = 10322;
					public const int WebsiteAccess = 10323;
					public const int WebsiteLanguage = 10324;
					public const int WebTemplate = 10325;
					public const int PowerPagesScanReport = 10332;
					public const int PowerPagesLog = 10333;
					public const int CatalogSubmissionFiles = 10338;
					public const int PackageSubmissionStore = 10339;
					public const int ListOperation = 10340;
					public const int MarketingFormDisplayAttributes = 10341;
					public const int DatabaseVersion = 10342;
					public const int UpgradeRun = 10343;
					public const int UpgradeStep = 10344;
					public const int UpgradeVersion = 10345;
					public const int ActivityMonitor = 10346;
					public const int OriginatingQueueMapping = 10347;
					public const int UnifiedRoutingSetupTracker = 10348;
					public const int AvailableTimes = 10349;
					public const int AvailableTimesDataSource = 10350;
					public const int ResourceGroupDataSource = 10351;
					public const int VirtualResourceGroupResource = 10352;
					public const int MigrationTracker = 10353;
					public const int BgJobLedger = 10354;
					public const int Intent = 10355;
					public const int JobsState = 10356;
					public const int AssetCategoryTemplateAssociation = 10357;
					public const int AssetTemplateAssociation = 10358;
					public const int CustomerAsset = 10359;
					public const int CustomerAssetAttachment = 10362;
					public const int CustomerAssetCategory = 10363;
					public const int FunctionalLocation = 10364;
					public const int PropertyDefinition = 10365;
					public const int PropertyAssetAssociation = 10366;
					public const int PropertyLog = 10367;
					public const int PropertyTemplateAssociation = 10368;
					public const int TemplateForProperties = 10369;
					public const int IoTAlert = 10373;
					public const int IoTDevice = 10374;
					public const int IoTDeviceCategory = 10375;
					public const int IoTDeviceCommand = 10376;
					public const int IoTDeviceCommandDefinition = 10377;
					public const int IoTDeviceDataHistory = 10378;
					public const int IoTDeviceProperty = 10379;
					public const int IoTDeviceRegistrationHistory = 10380;
					public const int IoTDeviceVisualizationConfiguration = 10381;
					public const int IoTFieldMapping = 10382;
					public const int IoTPropertyDefinition = 10383;
					public const int IoTProvider = 10384;
					public const int IoTProviderInstance = 10385;
					public const int IoTSettings = 10386;
					public const int IoTAlertToCaseProcess = 10389;
					public const int PlaybookCallableContext = 10391;
					public const int PlaybookActivity = 10392;
					public const int PlaybookActivityAttribute = 10393;
					public const int _DEPRECATED_PlaybookCategory = 10394;
					public const int Playbook = 10395;
					public const int _DEPRECATED_PlaybookTemplate = 10396;
					public const int AdminSettingsEntity = 10398;
					public const int CollabSpaceTeamAssociation = 10399;
					public const int CRMConnection = 10400;
					public const int TaggedRecord = 10401;
					public const int CopilotForSalesCustomerList = 10402;
					public const int MsdynVivaentitysetting = 10403;
					public const int MsdynVivaorgextensioncred = 10404;
					public const int MsdynVivaorgsetting = 10405;
					public const int MsdynVivausersetting = 10406;
					public const int SalesCopilotInsight = 10407;
					public const int OrgLevelSettingsForSalesCopilotApps = 10408;
					public const int SalesCopilotUserSetting = 10409;
					public const int AppProfile = 10410;
					public const int ApplicationExtension = 10411;
					public const int ApplicationTabTemplate = 10412;
					public const int AppProfileRoleMapping = 10413;
					public const int NotificationField = 10414;
					public const int NotificationTemplate = 10415;
					public const int SessionTemplate = 10416;
					public const int TemplateParameter = 10417;
					public const int ChannelIntegrationFrameworkV1_0Provider = 10423;
					public const int NotificationField_Deprecated_ = 10424;
					public const int NotificationTemplate_Deprecated_ = 10425;
					public const int AppParameterDefinition_Deprecated_ = 10426;
					public const int SessionTemplates_Deprecated_ = 10427;
					public const int ApplicationTabTemplate_Deprecated_ = 10428;
					public const int Parameter_Deprecated_ = 10429;
					public const int TemplateTag_Deprecated_ = 10430;
					public const int ApplicationType_Deprecated_ = 10431;
					public const int ChannelIntegrationFrameworkV2_0Provider = 10440;
					public const int ConversationData_Deprecated_ = 10442;
					public const int KPIEventData = 10443;
					public const int KPIEventDefinition = 10444;
					public const int SessionData_Deprecated_ = 10445;
					public const int SessionParticipantData_Deprecated_ = 10446;
					public const int ChannelDefinition = 10447;
					public const int ChannelDefinitionConsent = 10448;
					public const int ChannelDefinitionLocale = 10449;
					public const int ChannelInstance = 10450;
					public const int ChannelInstanceAccount = 10451;
					public const int ChannelMessageAttachment = 10452;
					public const int ChannelMessageContextPart = 10453;
					public const int ChannelMessagePart = 10454;
					public const int ConsumingApplication = 10455;
					public const int MsdynDefExtendedChannelInstance = 10456;
					public const int MsdynDefExtendedChannelInstanceAccount = 10457;
					public const int ProductivityPaneConfiguration = 10458;
					public const int PaneTabConfiguration = 10459;
					public const int PaneToolConfiguration = 10460;
					public const int AgentScript = 10462;
					public const int AgentScriptStep = 10463;
					public const int ActionInputParameter = 10465;
					public const int ActionOutputParameter = 10466;
					public const int MacroActionTemplate = 10467;
					public const int MacroSolutionConfiguration = 10468;
					public const int MacroConnector = 10469;
					public const int MacroRunHistory = 10470;
					public const int ParameterDefinition = 10471;
					public const int AdaptiveCardConfiguration = 10474;
					public const int SmartassistConfiguration = 10475;
					public const int ReadTracker = 10477;
					public const int ReadTrackingEnabledInformation = 10478;
					public const int MicrosoftTeamsGraphResourceEntity = 10479;
					public const int MsdynMsteamssetting = 10480;
					public const int MsdynMsteamssettingsv2 = 10481;
					public const int MicrosoftTeamsCollaborationEntity = 10482;
					public const int TeamsDialerAdminSettings = 10483;
					public const int TeamsContactSuggestionByAI = 10484;
					public const int ContactSuggestionRule = 10485;
					public const int ContactSuggestionRuleset = 10486;
					public const int MicrosoftTeamsChatAssociationEntity = 10487;
					public const int MicrosoftTeamsChatSuggestion = 10488;
					public const int MicrosoftOrgchartNodeEntity = 10489;
					public const int ForecastManualAdjustmentHistory = 10490;
					public const int DistributedLock = 10491;
					public const int EntityDeltaChange = 10492;
					public const int FileUploadStatusTracker = 10493;
					public const int Forecast = 10494;
					public const int ForecastConfiguration = 10495;
					public const int ForecastDefinition = 10496;
					public const int ForecastingCache = 10497;
					public const int ForecastInsights = 10498;
					public const int Forecast_ = 10499;
					public const int ForecastPredictionData = 10500;
					public const int ForecastPredictionStatus = 10501;
					public const int ForecastRecurrence = 10502;
					public const int RecomputeTracker = 10503;
					public const int ForecastRecurrence_ = 10504;
					public const int ShareAsConfiguration = 10505;
					public const int CustomerEmailCommunication = 10506;
					public const int GDPRData = 10507;
					public const int ODOSFeatureMetadata = 10508;
					public const int ODOSMetadata = 10509;
					public const int RecurringSalesAction = 10510;
					public const int RecurringSalesActionV2 = 10511;
					public const int MsdynRelationshipinsightsunifiedconfig = 10512;
					public const int Siconfig = 10513;
					public const int SIKeyValueConfig = 10514;
					public const int UsageMetric = 10515;
					public const int ActionCardRegarding = 10516;
					public const int ActionCardRoleSetting = 10517;
					public const int EntityRankingRule = 10518;
					public const int Flowcardtype = 10519;
					public const int Salesinsightssettings = 10520;
					public const int ActionCardUsage = 10521;
					public const int ActionCardUsageAggregation = 10522;
					public const int AutoCaptureRule = 10523;
					public const int AutoCaptureSettings = 10524;
					public const int UntrackedAppointment = 10525;
					public const int SuggestedActivity = 10526;
					public const int SuggestedActivityDataSource = 10527;
					public const int SuggestedContact = 10528;
					public const int SuggestedContactsDataSource = 10529;
					public const int NotesAnalysisConfig = 10530;
					public const int Icebreakersconfig = 10531;
					public const int Dealmanageraccess = 10532;
					public const int DealManagerSettings = 10533;
					public const int AccountKPIItem = 10534;
					public const int ActivityAnalysisCleanUpState = 10535;
					public const int RelationshipAnalyticsConfig = 10536;
					public const int ContactKPIItem = 10537;
					public const int DailyKpisForAccount = 10538;
					public const int DailyKpisForContact = 10539;
					public const int DailyKpisForLead = 10540;
					public const int DailyKpisForOpportunity = 10541;
					public const int LeadKPIItem = 10542;
					public const int MostContacted = 10543;
					public const int MostContactedBy = 10544;
					public const int OpportunityKPIItem = 10545;
					public const int RelationshipAnalyticsMetadata = 10546;
					public const int SimilarEntitiesFeatureImportance = 10547;
					public const int Wkwcolleaguesforcompany = 10548;
					public const int Wkwcolleaguesforcontact = 10549;
					public const int Wkwconfig = 10550;
					public const int AttributeInfluenceStatistics = 10551;
					public const int PredictionComputationOperation = 10552;
					public const int PredictionModelStatus = 10553;
					public const int PredictionScheduledOperation = 10554;
					public const int PredictiveModelScore = 10555;
					public const int PredictiveScore = 10556;
					public const int PredictiveScoringSyncStatus = 10557;
					public const int RealTimeScoring = 10558;
					public const int RealTimeScoringOperation = 10559;
					public const int SubmodelDefinition = 10560;
					public const int TimeSpentInBPF = 10561;
					public const int TrainingResult = 10562;
					public const int OpportunityModelConfig = 10563;
					public const int LeadModelConfig = 10564;
					public const int ModelPreviewStatus = 10565;
					public const int ProfileAlbum = 10566;
					public const int PostConfiguration = 10567;
					public const int PostRuleConfiguration = 10568;
					public const int WallView = 10569;
					public const int Filter = 10570;
					public const int CustomerVoiceAlert = 10571;
					public const int CustomerVoiceAlertRule = 10572;
					public const int CustomerVoiceSurveyEmailTemplate = 10573;
					public const int CustomerVoiceFileResponse = 10574;
					public const int CustomerVoiceLocalizedSurveyEmailTemplate = 10575;
					public const int CustomerVoiceProject = 10576;
					public const int CustomerVoiceSurveyQuestion = 10577;
					public const int CustomerVoiceSurveyQuestionResponse = 10578;
					public const int CustomerVoiceSatisfactionMetric = 10579;
					public const int CustomerVoiceSurvey = 10580;
					public const int CustomerVoiceSurveyInvite = 10581;
					public const int CustomerVoiceSurveyReminder = 10582;
					public const int CustomerVoiceSurveyResponse = 10583;
					public const int CustomerVoiceUnsubscribedRecipient = 10584;
					public const int AddToCalendarStyle = 10585;
					public const int Basestyle = 10586;
					public const int ButtonStyle = 10587;
					public const int CodeStyle = 10588;
					public const int ColumnStyle = 10589;
					public const int ContentBlock = 10590;
					public const int DividerStyle = 10591;
					public const int GeneralStyles = 10592;
					public const int Imagestyle = 10593;
					public const int LayoutStyle = 10594;
					public const int QRCodeStyle = 10595;
					public const int TextStyle = 10596;
					public const int VideoStyle = 10597;
					public const int AppState = 10598;
					public const int CSAdminConfig = 10599;
					public const int CustomAPIRulesetConfiguration = 10600;
					public const int DecisionContract = 10601;
					public const int DecisionRuleSet = 10602;
					public const int Rulesetentitymapping = 10603;
					public const int RoutingDiagnosticItem = 10604;
					public const int RoutingDiagnostic = 10605;
					public const int InboxCardConfiguration = 10606;
					public const int InboxConfiguration = 10607;
					public const int InboxEntityConfiguration = 10608;
					public const int AppProfileCopilotConfiguration = 10609;
					public const int AgentCopilotSetting = 10610;
					public const int CopilotSummarizationSetting = 10611;
					public const int CaseEnrichment = 10612;
					public const int CaseSuggestion = 10613;
					public const int CaseSuggestionRequestPayload = 10614;
					public const int CaseSuggestionsDataSouce = 10615;
					public const int AgentPreferenceForCopilot = 10616;
					public const int CopilotInteraction = 10617;
					public const int CopilotInteractionData = 10618;
					public const int CopilotTranscript = 10619;
					public const int CopilotTranscriptData = 10620;
					public const int KBEnrichment = 10621;
					public const int KnowledgeArticleSuggestion = 10622;
					public const int KnowledgeArticleSuggestionDataSource = 10623;
					public const int ServiceCopilotPlugin = 10624;
					public const int ServiceCopilotPluginRole = 10625;
					public const int SuggestionInteraction = 10626;
					public const int SuggestionRequestPayload = 10627;
					public const int SuggestionsModelSummary = 10628;
					public const int SuggestionsSetting = 10629;
					public const int Swarm = 10630;
					public const int SwarmParticipant = 10631;
					public const int SwarmParticipantRule = 10632;
					public const int SwarmRole = 10633;
					public const int SwarmSkill = 10634;
					public const int SwarmTemplate = 10635;
					public const int EntityAttachment = 10636;
					public const int CustomerServiceKeyValueConfiguration = 10637;
					public const int MasterEntityRoutingConfiguration = 10638;
					public const int RoutingRuleSetSetting = 10639;
					public const int AssignmentConfiguration = 10640;
					public const int AssignmentConfigurationStep = 10641;
					public const int CapacityProfile = 10642;
					public const int OverflowActionConfig = 10643;
					public const int PreferredAgent = 10644;
					public const int PreferredAgentCustomerIdentity = 10645;
					public const int PreferredAgentRoutedEntity = 10646;
					public const int RoutingConfiguration = 10647;
					public const int RoutingConfigurationStep = 10648;
					public const int CustomMessagingAccount = 10649;
					public const int ChannelConfiguration = 10650;
					public const int ChannelStateConfiguration = 10651;
					public const int ProvisioningState = 10652;
					public const int AdminAppState = 10653;
					public const int AgentStatusHistory = 10654;
					public const int PowerBIConfiguration = 10655;
					public const int AuthenticationSettings = 10656;
					public const int AuthSettingsEntry = 10657;
					public const int QuickReply = 10658;
					public const int EntityRoutingContext = 10659;
					public const int ChannelCapability = 10660;
					public const int ConversationAction = 10661;
					public const int ConversationActionLocale = 10662;
					public const int ConversationMessageBlock = 10663;
					public const int DeletedConversation = 10664;
					public const int DeprecatedWorkstreamEntityConfiguration = 10665;
					public const int Entity_ = 10666;
					public const int OngoingConversation_Deprecated_ = 10667;
					public const int LiveWorkItemEvent = 10668;
					public const int WorkStream = 10669;
					public const int MaskingRule = 10670;
					public const int AutoBlockRule = 10671;
					public const int BotChannelRegistrationSecret = 10672;
					public const int OmnichannelChannelApiConversationPrivilege = 10673;
					public const int OmnichannelChannelApiMessagePrivilege = 10674;
					public const int ChannelApiMethodMapping = 10675;
					public const int ExternalContext = 10676;
					public const int FlaggedSpam = 10677;
					public const int Language_ = 10678;
					public const int Conversation = 10679;
					public const int ContextItemValue = 10682;
					public const int LiveWorkItemParticipant_Deprecated_ = 10683;
					public const int ConversationSentiment = 10684;
					public const int ContextVariable = 10685;
					public const int Localization = 10686;
					public const int OCPaymentProfile = 10687;
					public const int Recording = 10688;
					public const int OmnichannelRequest = 10689;
					public const int RichMessage = 10690;
					public const int RichObjectMap = 10691;
					public const int RuleItem_ = 10692;
					public const int SentimentDailyTopic = 10693;
					public const int SentimentDailyTopicKeyword = 10694;
					public const int SentimentDailyTopicTrending = 10695;
					public const int Session = 10696;
					public const int SessionParticipantEvent = 10697;
					public const int SessionSentiment = 10698;
					public const int Message = 10699;
					public const int Tag = 10700;
					public const int GeoLocationProvider = 10701;
					public const int OmnichannelConfiguration = 10702;
					public const int OmnichannelPersonalization = 10703;
					public const int OmnichannelQueue_Deprecated_ = 10704;
					public const int OmnichannelSyncConfig = 10705;
					public const int OperatingHour = 10706;
					public const int PersonalQuickReply = 10707;
					public const int PersonalSoundSetting = 10708;
					public const int PersonaSecurityRoleMapping = 10709;
					public const int Presence = 10710;
					public const int Provider = 10711;
					public const int RoutingRequest = 10712;
					public const int SearchConfiguration = 10713;
					public const int SentimentAnalysis = 10714;
					public const int SessionEvent = 10715;
					public const int SessionParticipant = 10716;
					public const int AudioFile = 10717;
					public const int SoundNotificationSetting = 10718;
					public const int Transcript = 10719;
					public const int URNotificationTemplate = 10720;
					public const int URNotificationTemplateMapping = 10721;
					public const int UserSettings_ = 10722;
					public const int SelfService = 10723;
					public const int AgentCapacityUpdateHistory = 10730;
					public const int BookableResourceCapacityProfile = 10731;
					public const int WorkStreamCapacityProfile = 10732;
					public const int ConversationCapacityProfile = 10733;
					public const int AgentCapacityProfileUnit = 10734;
					public const int AgentChannelState = 10735;
					public const int AgentStatus = 10736;
					public const int ConversationCharacteristic = 10737;
					public const int SessionCharacteristic = 10738;
					public const int SkillAttachmentRule = 10739;
					public const int AttachSkill = 10740;
					public const int ModelTrainingDetails = 10741;
					public const int TrainingDataImportConfiguration = 10742;
					public const int CharacteristicMapping = 10743;
					public const int TrainingRecord = 10744;
					public const int SkillFinderModel = 10745;
					public const int EffortEstimate = 10746;
					public const int EffortEstimationModel = 10747;
					public const int EffortModelTrainingDetails = 10748;
					public const int ConversationInsight = 10749;
					public const int ActiveICDExtension = 10750;
					public const int EntityWorkstreamMap = 10751;
					public const int ICDExtension = 10752;
					public const int LockStatus = 10753;
					public const int OmnichannelAgentAssignmentCustomApiPrivilege = 10754;
					public const int ConversationActionItem = 10755;
					public const int ConversationAggregatedInsights = 10756;
					public const int Comment__ = 10757;
					public const int ConversationParticipantInsights = 10758;
					public const int ConversationParticipantSentiment = 10759;
					public const int ConversationQuestion = 10760;
					public const int ConversationSegmentSentiment = 10761;
					public const int ConversationSentiment_ = 10762;
					public const int ConversationSignal = 10763;
					public const int ConversationSubject = 10764;
					public const int ConversationSummarySuggestion = 10765;
					public const int ConversationSystemTag = 10766;
					public const int ConversationTag = 10767;
					public const int Recording_ = 10768;
					public const int SCIConversation = 10769;
					public const int CustomEmailHighlight = 10770;
					public const int CustomHighlight = 10771;
					public const int CustomPublisher = 10772;
					public const int EnvironmentSettings = 10773;
					public const int UserSettings__ = 10774;
					public const int CatalogEventStatusConfiguration = 10775;
					public const int Configuration = 10776;
					public const int Trigger = 10777;
					public const int TriggersToSdkMessageProcessingSteps = 10778;
					public const int EventParameterMetadata = 10779;
					public const int TrackingContext = 10780;
					public const int MarketingFeatureConfiguration = 10781;
					public const int MsdynmktExperimentv2 = 10782;
					public const int ACSChannelInstance = 10783;
					public const int ACSChannelInstanceAccount = 10784;
					public const int InfobipChannelInstance = 10785;
					public const int InfobipChannelInstanceAccount = 10786;
					public const int LinkMobilityChannelInstance = 10787;
					public const int LinkMobilityChannelInstanceAccount = 10788;
					public const int MockSmsProviderChannelInstance = 10789;
					public const int MockSmsProviderChannelInstanceAccount = 10790;
					public const int TeleSignChannelInstance = 10791;
					public const int TeleSignChannelInstanceAccount = 10792;
					public const int TwilioChannelInstance = 10793;
					public const int TwilioChannelInstanceAccount = 10794;
					public const int VibesChannelInstance = 10795;
					public const int VibesChannelInstanceAccount = 10796;
					public const int PredefinedPlaceholder = 10797;
					public const int MetadataEntityRelationship = 10798;
					public const int MetadataItem = 10799;
					public const int MetadataStoreState = 10800;
					public const int DigitalSellingActiveTask = 10801;
					public const int DigitalSellingCompletedTask = 10802;
					public const int SalesTag = 10803;
					public const int Sequence = 10804;
					public const int SequenceStat = 10805;
					public const int SequenceTarget = 10806;
					public const int SequenceTargetStep = 10807;
					public const int SequenceTemplate = 10808;
					public const int Sabackupdiagnostic = 10810;
					public const int SABatchRunInstance = 10811;
					public const int Salesroutingdiagnostic = 10812;
					public const int SARunInstance = 10813;
					public const int Segment = 10814;
					public const int Segmentsetting = 10815;
					public const int SegmentProperty = 10816;
					public const int SegmentsUtil = 10817;
					public const int AssignmentRule = 10818;
					public const int SellerAttribute = 10819;
					public const int SellerAttributeValue = 10820;
					public const int AssignmentMap = 10821;
					public const int SalesAssignmentSetting = 10822;
					public const int SalesRoutingRun = 10823;
					public const int ExtendedUserSetting = 10825;
					public const int SalesAccelerationInsights = 10826;
					public const int SalesAccelerationSettings = 10827;
					public const int Insight = 10828;
					public const int WorkListSuggestion = 10829;
					public const int WorkListSuggestionSource = 10830;
					public const int WorkListViewConfiguration = 10831;
					public const int WorkQueueRecord = 10832;
					public const int WorkQueueRecordState = 10833;
					public const int WorkListUserSetting = 10834;
					public const int WQDataSource = 10835;
					public const int SuggestionAssignmentRule = 10836;
					public const int SuggestionPrincipalObjectAccess = 10837;
					public const int SuggestionSellerPriority = 10838;
					public const int DataHygieneSettingInfo = 10839;
					public const int DuplicateDetectionPluginRun = 10840;
					public const int DuplicateLeadMapping = 10841;
					public const int LeadHygieneSetting = 10842;
					public const int LinkedEntityAttributeValidity = 10843;
					public const int SalesProvisioningRequest = 10844;
					public const int SalesOmnichannelMessage = 10845;
					public const int TextMessageTemplate = 10846;
					public const int DataAnalyticsAdminSettings_Deprecated_ = 10847;
					public const int DataAnalyticsReport = 10848;
					public const int Insights = 10849;
					public const int SalesAccelerationReports = 10850;
					public const int BotSession = 10852;
					public const int Conversationsuggestionrequestpayload = 10853;
					public const int DataAnalyticsUserCustomizedReport = 10854;
					public const int DataAnalyticsDataset = 10855;
					public const int DataAnalyticsWorkspace = 10856;
					public const int ReportBookmark = 10857;
					public const int AgentResourceForecasting = 10858;
					public const int _Deprecated_DynamicsCustomerServiceAnalytics = 10859;
					public const int CaseTopic = 10860;
					public const int CaseTopicSetting = 10861;
					public const int CaseTopicSummary = 10862;
					public const int CaseTopicIncidentMapping = 10863;
					public const int CustomerServiceHistoricalAnalytics = 10864;
					public const int Forecast__ = 10865;
					public const int KnowledgeAnalytics = 10866;
					public const int ForecastSummaryAndSetting = 10867;
					public const int KeywordsDescriptionSuggestionSetting = 10868;
					public const int ConversationSummaryInteraction = 10869;
					public const int ConversationSummarySetting = 10870;
					public const int ConversationTopic = 10871;
					public const int ConversationTopicSetting = 10872;
					public const int ConversationTopicSummary = 10873;
					public const int ConversationTopicConversationMapping = 10874;
					public const int OmnichannelHistoricalAnalytics = 10875;
					public const int OmnichannelVoiceHistoricalAnalytics_preview__Deprecated_ = 10876;
					public const int OmnichannelRealtimeAnalytics = 10877;
					public const int Booking = 10878;
					public const int RequirementCharacteristic = 10981;
					public const int RequirementOrganizationUnit = 10982;
					public const int BookingSetupMetadata = 10983;
					public const int Configuration_ = 10984;
					public const int BookableResourceAssociation = 10985;
					public const int Actual = 10986;
					public const int ClientExtension = 10987;
					public const int RequirementGroup = 10988;
					public const int RequirementRelationship = 10989;
					public const int BookingAlert = 10990;
					public const int BookingAlertStatus = 10991;
					public const int BookingChange = 10992;
					public const int BookingRule = 10993;
					public const int BusinessClosure = 10994;
					public const int OrganizationalUnit = 10995;
					public const int Priority = 10996;
					public const int RequirementResourceCategory = 10997;
					public const int RequirementResourcePreference = 10998;
					public const int RequirementStatus = 10999;
					public const int ResourceRequirement = 11000;
					public const int ResourceRequirementDetail = 11001;
					public const int ResourceTerritory = 11002;
					public const int ScheduleBoardSetting = 11003;
					public const int SchedulingParameter = 11004;
					public const int SystemUserSchedulerSetting = 11005;
					public const int FulfillmentPreference = 11006;
					public const int TimeGroupDetail = 11007;
					public const int TransactionOrigin = 11008;
					public const int WorkTemplate = 11009;
					public const int RequirementChange = 11010;
					public const int RequirementDependency = 11011;
					public const int SchedulingFeatureFlag = 11012;
					public const int AccountProjectPriceList = 11013;
					public const int ProjectServiceApproval = 11014;
					public const int BatchJob = 11015;
					public const int ProjectStages = 11016;
					public const int InvoiceProcess = 11017;
					public const int CompetencyRequirement_Deprecated_ = 11018;
					public const int ContactPriceList = 11019;
					public const int ProjectContractLineInvoiceSchedule = 11020;
					public const int ProjectContractLineMilestone = 11021;
					public const int ActualDataExport_Deprecated_ = 11022;
					public const int Delegation = 11023;
					public const int PricingDimension = 11024;
					public const int PricingDimensionFieldName = 11025;
					public const int Estimate = 11026;
					public const int EstimateLine = 11027;
					public const int Expense = 11028;
					public const int ExpenseCategory = 11029;
					public const int ExpenseReceipt = 11030;
					public const int Fact = 11031;
					public const int FieldComputation = 11032;
					public const int FindWorkEvent_DeprecatedInV3_0_ = 11033;
					public const int IntegrationJob = 11034;
					public const int IntegrationJobDetail = 11035;
					public const int InvoiceFrequency = 11036;
					public const int InvoiceFrequencyDetail = 11037;
					public const int InvoiceLineDetail = 11038;
					public const int Journal = 11039;
					public const int JournalLine = 11040;
					public const int ResultCache = 11041;
					public const int OpportunityLineResourceCategory_Deprecated_ = 11042;
					public const int OpportunityLineDetail_Deprecated_ = 11043;
					public const int OpportunityLineTransactionCategory_Deprecated_ = 11044;
					public const int OpportunityLineTransactionClassification_Deprecated_ = 11045;
					public const int OpportunityProjectPriceList = 11046;
					public const int ProjectContractLineResourceCategory = 11047;
					public const int ProjectContractLineDetail = 11048;
					public const int ProjectContractLineTransactionCategory = 11049;
					public const int ProjectContractLineTransactionClassification = 11050;
					public const int ProjectContractProjectPriceList = 11051;
					public const int ProcessNotes = 11052;
					public const int Project = 11053;
					public const int ProjectApproval = 11054;
					public const int ProjectParameter = 11055;
					public const int ProjectParameterPriceList = 11056;
					public const int ProjectPriceList = 11057;
					public const int ProjectTask = 11058;
					public const int ProjectTaskDependency = 11059;
					public const int ProjectTaskStatusUser = 11060;
					public const int ProjectTeamMember = 11061;
					public const int ProjectTeamMemberSignUp_DeprecatedInV3_0_ = 11062;
					public const int ProjectTransactionCategory_Deprecated_ = 11063;
					public const int QuoteLineAnalyticsBreakdown = 11064;
					public const int QuoteLineInvoiceSchedule = 11065;
					public const int QuoteLineResourceCategory = 11066;
					public const int QuoteLineMilestone = 11067;
					public const int QuoteLineDetail = 11068;
					public const int QuoteLineTransactionCategory = 11069;
					public const int QuoteLineTransactionClassification = 11070;
					public const int QuoteProjectPriceList = 11071;
					public const int ResourceAssignment = 11072;
					public const int ResourceAssignmentDetail_DeprecatedInV2_0_ = 11073;
					public const int RolePriceMarkup = 11074;
					public const int RolePrice = 11075;
					public const int ResourceRequest = 11076;
					public const int RoleCompetencyRequirement = 11077;
					public const int RoleUtilization = 11078;
					public const int TimeEntry = 11079;
					public const int TimeOffCalendar = 11080;
					public const int TransactionCategory = 11081;
					public const int TransactionCategoryClassification = 11082;
					public const int TransactionCategoryHierarchyElement = 11083;
					public const int TransactionCategoryPrice = 11084;
					public const int TransactionConnection = 11085;
					public const int TransactionType = 11086;
					public const int UserWorkHistory = 11087;
					public const int TimeSource = 11094;
					public const int ApprovalSet = 11095;
					public const int ContractLineDetailPerformance = 11096;
					public const int ContractPerformance = 11097;
					public const int ThreeDimensionalModel = 11098;
					public const int InspectionTemplate = 11099;
					public const int InspectionAttachment = 11100;
					public const int InspectionTemplateVersion = 11101;
					public const int Inspection = 11102;
					public const int InspectionResponse = 11103;
					public const int FunctionalLocationType = 11104;
					public const int LocationTemplateAssociation = 11105;
					public const int FunctionalLocationTypeTemplateAssociation = 11106;
					public const int PropertyLocationAssociation = 11107;
					public const int Warranty = 11108;
					public const int PaymentTerm = 11112;
					public const int PurchaseOrder = 11113;
					public const int PurchaseOrderProduct = 11114;
					public const int PurchaseOrderReceipt = 11115;
					public const int PurchaseOrderReceiptProduct = 11116;
					public const int ShipVia = 11117;
					public const int TaxCode = 11118;
					public const int TaxCodeDetail = 11119;
					public const int Warehouse = 11120;
					public const int Agreement = 11121;
					public const int AgreementBookingDate = 11122;
					public const int AgreementBookingIncident = 11123;
					public const int AgreementBookingProduct = 11124;
					public const int AgreementBookingService = 11125;
					public const int AgreementBookingServiceTask = 11126;
					public const int AgreementBookingSetup = 11127;
					public const int AgreementInvoiceDate = 11128;
					public const int AgreementInvoiceProduct = 11129;
					public const int AgreementInvoiceSetup = 11130;
					public const int AgreementSubstatus = 11131;
					public const int BookingJournal = 11132;
					public const int BookingTimestamp = 11133;
					public const int PurchaseOrderBusinessProcess = 11134;
					public const int CaseToWorkOrderBusinessProcess = 11135;
					public const int AgreementBusinessProcess = 11136;
					public const int WorkOrderBusinessProcess = 11137;
					public const int EntitlementApplication = 11138;
					public const int FieldServicePriceListItem = 11139;
					public const int FieldServiceSetting = 11140;
					public const int FieldServiceSLAConfiguration = 11141;
					public const int FieldServiceSystemJob = 11142;
					public const int IncidentType = 11143;
					public const int IncidentTypeCharacteristic = 11144;
					public const int IncidentTypeProduct = 11145;
					public const int IncidentTypeService = 11146;
					public const int IncidentTypeServiceTask = 11147;
					public const int IncidentTypesSetup = 11148;
					public const int IncidentTypeRequirementGroup = 11149;
					public const int InventoryAdjustment = 11150;
					public const int InventoryAdjustmentProduct = 11151;
					public const int InventoryJournal = 11152;
					public const int InventoryTransfer = 11153;
					public const int OrderInvoicingDate = 11154;
					public const int OrderInvoicingProduct = 11155;
					public const int OrderInvoicingSetup = 11156;
					public const int OrderInvoicingSetupDate = 11157;
					public const int Payment = 11158;
					public const int PaymentDetail = 11159;
					public const int PaymentMethod = 11160;
					public const int PostalCode = 11161;
					public const int ProductInventory = 11162;
					public const int PurchaseOrderBill = 11163;
					public const int PurchaseOrderSubStatus = 11164;
					public const int QuoteBookingIncident = 11165;
					public const int QuoteBookingProduct = 11166;
					public const int QuoteBookingService = 11167;
					public const int QuoteBookingServiceTask = 11168;
					public const int QuoteBookingSetup = 11169;
					public const int QuoteInvoicingProduct = 11170;
					public const int QuoteInvoicingSetup = 11171;
					public const int ResourcePayType = 11172;
					public const int RMA = 11173;
					public const int RMAProduct = 11174;
					public const int RMAReceipt = 11175;
					public const int RMAReceiptProduct = 11176;
					public const int RMASubStatus = 11177;
					public const int RTV = 11178;
					public const int RTVProduct = 11179;
					public const int RTVSubstatus = 11180;
					public const int ServiceTaskType = 11181;
					public const int TimeOffRequest = 11182;
					public const int UniqueNumber = 11183;
					public const int WorkOrder = 11184;
					public const int WorkOrderCharacteristic_Deprecated_ = 11185;
					public const int WorkOrderDetailsGenerationQueue_Deprecated_ = 11186;
					public const int WorkOrderIncident = 11187;
					public const int WorkOrderProduct = 11188;
					public const int ResourceRestriction_Deprecated_ = 11189;
					public const int WorkOrderService = 11190;
					public const int WorkOrderServiceTask = 11191;
					public const int WorkOrderSubstatus = 11192;
					public const int WorkOrderType = 11193;
					public const int BookableResourceBookingQuickNote = 11195;
					public const int FieldServiceFrontlineWorkerConfiguration = 11196;
					public const int IncidentTypeSuggestionResult = 11199;
					public const int IncidentTypeSuggestionRunHistory = 11200;
					public const int IncidentTypeResolution = 11201;
					public const int Insurance = 11202;
					public const int NotToExceed = 11203;
					public const int AssetSuggestion = 11204;
					public const int ProblematicAssetFeedback = 11205;
					public const int Resolution = 11206;
					public const int Trade = 11207;
					public const int TradeCoverage = 11208;
					public const int WorkOrderNotToExceed = 11211;
					public const int WorkOrderResolution = 11212;
					public const int CFSIoTAlertProcessFlow = 11213;
					public const int GeolocationSettings = 11214;
					public const int GeolocationTracking = 11215;
					public const int EntityConfiguration = 11216;
					public const int Geofence = 11217;
					public const int GeofenceEvent = 11218;
					public const int GeofencingSettings = 11219;
					public const int AssetSuggestionsSetting = 11220;
					public const int FieldServiceHistoricalAnalytics = 11221;
					public const int ResourceDuration_preview_ = 11222;
					public const int PredictiveDuration_preview_ = 11223;
					public const int PredictiveWorkHourDurationSetting = 11224;
					public const int MobileSource = 11225;
					public const int Channel = 11226;
					public const int Scenario = 11227;
					public const int SurveyAnswerOption = 11228;
					public const int SurveyResponse = 11229;
					public const int SurveyResponseValue = 11230;
					public const int ChatWidgetLanguage_deprecated_ = 11231;
					public const int ChatWidget = 11232;
					public const int LiveChatContext = 11233;
					public const int ChatWidgetLocation = 11234;
					public const int LocalizedSurveyQuestion_Deprecated_ = 11235;
					public const int SurveyQuestionSequence = 11236;
					public const int SurveyQuestion = 11237;
					public const int CommunicationProviderSetting = 11238;
					public const int CommunicationProviderSettingEntry = 11239;
					public const int PhoneNumber = 11240;
					public const int Carrier = 11241;
					public const int SMSNumberSettings = 11242;
					public const int SMSEngagementContext = 11243;
					public const int SMSNumber = 11244;
					public const int SMSSettingSecret = 11245;
					public const int FacebookEngagementContext = 11246;
					public const int FacebookApplication = 11247;
					public const int FacebookPage = 11248;
					public const int CustomMessagingEngagementContext = 11249;
					public const int LINEEngagementContext = 11250;
					public const int CustomMessagingChannel = 11251;
					public const int LINEAccount = 11252;
					public const int TwitterAccount = 11253;
					public const int TwitterHandle = 11254;
					public const int WeChatAccount = 11255;
					public const int WhatsAppAccount = 11256;
					public const int WhatsAppNumber = 11257;
					public const int TwitterEngagementContext = 11258;
					public const int WeChatEngagementContext = 11259;
					public const int WhatsAppEngagementContext = 11260;
					public const int AppleMessagesForBusinessAccount = 11261;
					public const int AppleMessagesForBusinessEngagementContext = 11262;
					public const int OCApplePayEntity = 11263;
					public const int Google_sBusinessMessagesAgentAccount = 11264;
					public const int Google_sBusinessMessagesPartnerAccount = 11265;
					public const int Google_sBusinessMessagesEngagementContext = 11266;
					public const int TwitterHandleProvisioningStatus = 11267;
					public const int OCTwitterHandleSecret = 11268;
					public const int MicrosoftTeamsAccount = 11269;
					public const int _Deprecated_TeamsEngagementContext = 11270;
					public const int TeamsEngagementContext = 11271;
					public const int OutboundConfiguration = 11272;
					public const int OutboundMessage = 11273;
					public const int UIIAction = 11274;
					public const int UIIAudit = 11275;
					public const int UIIContext = 11276;
					public const int HostedControl = 11277;
					public const int UIINonHostedApplication = 11278;
					public const int Option = 11279;
					public const int UIISavedSession = 11280;
					public const int UIISessionTransfer = 11281;
					public const int UIIWorkflow = 11282;
					public const int UIIWorkflowStep = 11283;
					public const int UIIWorkflowStepMapping = 11284;
					public const int ActionCallWorkflow = 11285;
					public const int ActionCall = 11286;
					public const int AgentScriptTaskCategory = 11287;
					public const int AgentScriptAnswer = 11288;
					public const int Audit_DiagnosticsSetting = 11289;
					public const int Configuration__ = 11290;
					public const int CustomizationFile = 11291;
					public const int EntityType = 11292;
					public const int EntitySearch = 11293;
					public const int Form = 11294;
					public const int LanguageModule = 11295;
					public const int Scriptlet = 11296;
					public const int ScriptTaskTrigger = 11297;
					public const int CTISearch = 11298;
					public const int SessionInformation = 11299;
					public const int SessionTransfer = 11300;
					public const int AgentScriptTask = 11301;
					public const int ToolbarButton = 11302;
					public const int Toolbar = 11303;
					public const int TraceSourceSetting = 11304;
					public const int UnifiedInterfaceSettings = 11305;
					public const int Event = 11306;
					public const int UserSetting = 11307;
					public const int WindowNavigationRule = 11308;
					public const int ViewAsExampleQuestion = 11333;
					public const int SalesUsageTelemetryReports = 11389;
					public const int SalesUsageReporting = 11390;
					public const int PMSimulation = 11391;
					public const int SurveySetting = 11392;
					public const int Carrier_ = 11393;
					public const int CarrierDependencyCheck = 11394;
					public const int CarrierMissingDependency = 11395;
					public const int Workbench = 11396;
					public const int WorkbenchHistory = 11397;
					public const int AutonomousCaseCreationAndEnrichmentRules = 11406;
					public const int CopilotAnalytics = 11410;
					public const int FederatedKnowledgeConfiguration = 11411;
					public const int FederatedKnowledgeEntityConfiguration = 11412;
					public const int FormMapping = 11423;
					public const int EventExpanderBreadcrumb = 18085;
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
		public static class LogicalNames
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
		public static class AlternateKeys
		{
				public const string Dupdetectionuniquekey = "dupdetectionuniquekey";
		}
		#endregion

		#region Relations
        public static class Relations
        {
            public static class OneToMany
            {
				public const string DuplicateRuleAnnotation = "DuplicateRule_Annotation";
				public const string DuplicateRuleDuplicateBaseRecord = "DuplicateRule_DuplicateBaseRecord";
				public const string DuplicateRuleDuplicateRuleConditions = "DuplicateRule_DuplicateRuleConditions";
				public const string DuplicateRuleSyncErrors = "DuplicateRule_SyncErrors";
				public const string UserentityinstancedataDuplicaterule = "userentityinstancedata_duplicaterule";
            }

            public static class ManyToOne
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
        public static DuplicateRule Retrieve(IOrganizationService service, Guid id)
        {
            return Retrieve(service,id, new ColumnSet(true));
        }

        public static DuplicateRule Retrieve(IOrganizationService service, Guid id, ColumnSet columnSet)
        {
            return service.Retrieve("duplicaterule", id, columnSet).ToEntity<DuplicateRule>();
        }

        public DuplicateRule GetChangedEntity()
        {
            if (_trackChanges)
            {
                var attr = new AttributeCollection();
                foreach (var attrName in _changedProperties.Value.Select(changedProperty => ((AttributeLogicalNameAttribute) GetType().GetProperty(changedProperty)!.GetCustomAttribute(typeof (AttributeLogicalNameAttribute))!).LogicalName).Where(attrName => Contains(attrName)))
                {
                    attr.Add(attrName,this[attrName]);
                }
                return new  DuplicateRule(Id) {Attributes = attr };
            }
            return this;
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
