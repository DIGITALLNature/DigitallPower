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
	/// Contains information about the tracked service-level KPIs for cases that belong to different customers.
	/// </summary>
	[DataContractAttribute()]
	[EntityLogicalNameAttribute("sla")]
	[System.CodeDom.Compiler.GeneratedCode("ec4u.automation", "1.0.0")]
	public partial class SLA : Entity, INotifyPropertyChanging, INotifyPropertyChanged
    {
	    #region ctor
		[DebuggerNonUserCode]
		public SLA() : this(false)
        {
        }

        [DebuggerNonUserCode]
		public SLA(bool trackChanges = false) : base(EntityLogicalName)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public SLA(Guid id, bool trackChanges = false) : base(EntityLogicalName,id)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public SLA(KeyAttributeCollection keyAttributes, bool trackChanges = false) : base(EntityLogicalName,keyAttributes)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public SLA(string keyName, object keyValue, bool trackChanges = false) : base(EntityLogicalName, keyName, keyValue)
        {
			_trackChanges = trackChanges;
        }
        #endregion

		#region fields
        private readonly bool _trackChanges;
        private readonly Lazy<HashSet<string>> _changedProperties = new Lazy<HashSet<string>>();
        #endregion

        #region consts
        public const string EntityLogicalName = "sla";
        public const int EntityTypeCode = 9750;
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
		[AttributeLogicalNameAttribute("slaid")]
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
				SLAId = value;
			}
		}

		/// <summary>
		/// Unique identifier of the SLA.
		/// </summary>
		[AttributeLogicalName("slaid")]
        public Guid? SLAId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<Guid?>("slaid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(SLAId));
                SetAttributeValue("slaid", value);
				if (value.HasValue)
				{
					base.Id = value.Value;
				}
				else
				{
					base.Id = System.Guid.Empty;
				}
                OnPropertyChanged(nameof(SLAId));
            }
        }

		/// <summary>
		/// Select whether this SLA will allow pausing and resuming during the time calculation.
		/// </summary>
		[AttributeLogicalName("allowpauseresume")]
        public bool? AllowPauseResume
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<bool?>("allowpauseresume");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(AllowPauseResume));
                SetAttributeValue("allowpauseresume", value);
                OnPropertyChanged(nameof(AllowPauseResume));
            }
        }

		
		[AttributeLogicalName("allowpauseresumename")]
        public string? AllowPauseResumeName
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("allowpauseresumename");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(AllowPauseResumeName));
                SetAttributeValue("allowpauseresumename", value);
                OnPropertyChanged(nameof(AllowPauseResumeName));
            }
        }

		/// <summary>
		/// Select the field that specifies the date and time from which the SLA items will be calculated for the case record. For example, if you select the Case Created On field, SLA calculation will begin from the time the case is created.
		/// </summary>
		[AttributeLogicalName("applicablefrom")]
        public string? ApplicableFrom
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("applicablefrom");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(ApplicableFrom));
                SetAttributeValue("applicablefrom", value);
                OnPropertyChanged(nameof(ApplicableFrom));
            }
        }

		/// <summary>
		/// Select the field that specifies the date and time from which the SLA items will be calculated. For example, if you select the Case Created On field, SLA calculation will begin from the time the case is created.
		/// </summary>
		[AttributeLogicalName("applicablefrompicklist")]
        public OptionSetValue? ApplicableFromPickList
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<OptionSetValue?>("applicablefrompicklist");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(ApplicableFromPickList));
                SetAttributeValue("applicablefrompicklist", value);
                OnPropertyChanged(nameof(ApplicableFromPickList));
            }
        }

		/// <summary>
		/// Choose the business hours for calculating SLA item timelines.
		/// </summary>
		[AttributeLogicalName("businesshoursid")]
        public EntityReference? BusinessHoursId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<EntityReference?>("businesshoursid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(BusinessHoursId));
                SetAttributeValue("businesshoursid", value);
                OnPropertyChanged(nameof(BusinessHoursId));
            }
        }

		/// <summary>
		/// Type additional information to describe the SLA
		/// </summary>
		[AttributeLogicalName("changedattributelist")]
        public string? ChangedAttributeList
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("changedattributelist");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(ChangedAttributeList));
                SetAttributeValue("changedattributelist", value);
                OnPropertyChanged(nameof(ChangedAttributeList));
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
		/// Shows who created the record.
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
		/// Shows the date and time when the record was created. The date and time are displayed in the time zone selected in Microsoft Dynamics 365 options.
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
		/// Shows who created the record on behalf of another user.
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
		/// Type additional information to describe the SLA
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
		/// Exchange rate between the currency associated with the SLA record and the base currency.
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
		/// Tells whether this SLA is the default one.
		/// </summary>
		[AttributeLogicalName("isdefault")]
        public bool? IsDefault
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<bool?>("isdefault");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(IsDefault));
                SetAttributeValue("isdefault", value);
                OnPropertyChanged(nameof(IsDefault));
            }
        }

		/// <summary>
		/// For internal use only.
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
		/// Shows who last updated the record.
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
		/// Shows the date and time when the record was last updated. The date and time are displayed in the time zone selected in Microsoft Dynamics 365 options.
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
		/// Shows who created the record on behalf of another user.
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
		/// Type a descriptive name of the service level agreement (SLA).
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
		/// Choose the entity type that the SLA is defined.
		/// </summary>
		[AttributeLogicalName("objecttypecode")]
        public OptionSetValue? ObjectTypeCode
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<OptionSetValue?>("objecttypecode");
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
		/// Enter the user or team who owns the SLA. This field is updated every time the item is assigned to a different user.
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
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(OwningBusinessUnit));
                SetAttributeValue("owningbusinessunit", value);
                OnPropertyChanged(nameof(OwningBusinessUnit));
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
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(OwningTeam));
                SetAttributeValue("owningteam", value);
                OnPropertyChanged(nameof(OwningTeam));
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
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(OwningUser));
                SetAttributeValue("owninguser", value);
                OnPropertyChanged(nameof(OwningUser));
            }
        }

		/// <summary>
		/// Shows the primary entity that the SLA has been created for.
		/// </summary>
		[AttributeLogicalName("primaryentityotc")]
        public int? PrimaryEntityOTC
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<int?>("primaryentityotc");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(PrimaryEntityOTC));
                SetAttributeValue("primaryentityotc", value);
                OnPropertyChanged(nameof(PrimaryEntityOTC));
            }
        }

		/// <summary>
		/// For internal use only.
		/// </summary>
		[AttributeLogicalName("slaidunique")]
        public Guid? SLAIdUnique
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<Guid?>("slaidunique");
            }
        }

		/// <summary>
		/// Select the type of service level agreement (SLA).
		/// </summary>
		[AttributeLogicalName("slatype")]
        public OptionSetValue? SLAType
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<OptionSetValue?>("slatype");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(SLAType));
                SetAttributeValue("slatype", value);
                OnPropertyChanged(nameof(SLAType));
            }
        }

		
		[AttributeLogicalName("slatypename")]
        public string? SLATypeName
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("slatypename");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(SLATypeName));
                SetAttributeValue("slatypename", value);
                OnPropertyChanged(nameof(SLATypeName));
            }
        }

		
		[AttributeLogicalName("slaversion")]
        public OptionSetValue? Slaversion
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<OptionSetValue?>("slaversion");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(Slaversion));
                SetAttributeValue("slaversion", value);
                OnPropertyChanged(nameof(Slaversion));
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
		/// Shows whether the Service Level Agreement (SLA) is active or inactive.
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
                OnPropertyChanging(nameof(StateCode));
                SetAttributeValue("statecode", value);
                OnPropertyChanged(nameof(StateCode));
            }
        }

		/// <summary>
		/// Select the status of the service level agreement (SLA).
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
		/// Unique identifier of the currency associated with the SLA record.
		/// </summary>
		[AttributeLogicalName("transactioncurrencyid")]
        public EntityReference? TransactionCurrencyId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<EntityReference?>("transactioncurrencyid");
            }
        }

		/// <summary>
		/// Version number of the SLA.
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
		/// Workflow associated with the SLA.
		/// </summary>
		[AttributeLogicalName("workflowid")]
        public EntityReference? WorkflowId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<EntityReference?>("workflowid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(WorkflowId));
                SetAttributeValue("workflowid", value);
                OnPropertyChanged(nameof(WorkflowId));
            }
        }

		
		[AttributeLogicalName("workflowidname")]
        public string? WorkflowIdName
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("workflowidname");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(WorkflowIdName));
                SetAttributeValue("workflowidname", value);
                OnPropertyChanged(nameof(WorkflowIdName));
            }
        }


		#endregion

		#region NavigationProperties
		/// <summary>
		/// 1:N manualsla_account
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("manualsla_account")]
		public System.Collections.Generic.IEnumerable<Account> ManualslaAccount
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Account>("manualsla_account", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("ManualslaAccount");
				this.SetRelatedEntities<Account>("manualsla_account", null, value);
				this.OnPropertyChanged("ManualslaAccount");
			}
		}

		/// <summary>
		/// 1:N manualsla_contact
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("manualsla_contact")]
		public System.Collections.Generic.IEnumerable<Contact> ManualslaContact
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Contact>("manualsla_contact", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("ManualslaContact");
				this.SetRelatedEntities<Contact>("manualsla_contact", null, value);
				this.OnPropertyChanged("ManualslaContact");
			}
		}

		/// <summary>
		/// 1:N sla_account
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("sla_account")]
		public System.Collections.Generic.IEnumerable<Account> SlaAccount
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Account>("sla_account", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("SlaAccount");
				this.SetRelatedEntities<Account>("sla_account", null, value);
				this.OnPropertyChanged("SlaAccount");
			}
		}

		/// <summary>
		/// 1:N sla_contact
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("sla_contact")]
		public System.Collections.Generic.IEnumerable<Contact> SlaContact
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Contact>("sla_contact", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("SlaContact");
				this.SetRelatedEntities<Contact>("sla_contact", null, value);
				this.OnPropertyChanged("SlaContact");
			}
		}

		/// <summary>
		/// 1:N slabase_AsyncOperations
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("slabase_AsyncOperations")]
		public System.Collections.Generic.IEnumerable<AsyncOperation> SlabaseAsyncOperations
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<AsyncOperation>("slabase_AsyncOperations", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("SlabaseAsyncOperations");
				this.SetRelatedEntities<AsyncOperation>("slabase_AsyncOperations", null, value);
				this.OnPropertyChanged("SlabaseAsyncOperations");
			}
		}

		#endregion

		#region Options
		public static class Options
		{
                public struct AllowPauseResume
                {
                    public const bool DoNotAllow = false;
                    public const bool Allow = true;
                }
			    public struct ApplicableFromPickList
                {
					public const int No = 1;
					public const int Yes = 2;
                }
			    public struct ComponentState
                {
					public const int Published = 0;
					public const int Unpublished = 1;
					public const int Deleted = 2;
					public const int DeletedUnpublished = 3;
                }
                public struct IsDefault
                {
                    public const bool No = false;
                    public const bool Yes = true;
                }
                public struct IsManaged
                {
                    public const bool Unmanaged = false;
                    public const bool Managed = true;
                }
			    public struct ObjectTypeCode
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
					public const int OpportunityProduct = 1083;
					public const int Quote = 1084;
					public const int QuoteProduct = 1085;
					public const int UserFiscalCalendar = 1086;
					public const int Order = 1088;
					public const int OrderProduct = 1089;
					public const int Invoice = 1090;
					public const int InvoiceProduct = 1091;
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
					public const int SLA_ = 9750;
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
					public const int StageSolutionUpload = 10010;
					public const int ExportSolutionUpload = 10011;
					public const int FeatureControlSetting = 10012;
					public const int SolutionComponentSummary = 10013;
					public const int SolutionComponentCountSummary = 10014;
					public const int SolutionComponentDataSource = 10015;
					public const int SolutionComponentCountDataSource = 10016;
					public const int AADUser = 10017;
					public const int Catalog = 10018;
					public const int CatalogAssignment = 10019;
					public const int InternalCatalogAssignment = 10020;
					public const int CustomAPI = 10021;
					public const int CustomAPIRequestParameter = 10022;
					public const int CustomAPIResponseProperty = 10023;
					public const int ProvisionLanguageForUser = 10024;
					public const int SharedObject = 10025;
					public const int SharedWorkspace = 10026;
					public const int DataLakeFolder = 10027;
					public const int DataLakeFolderPermission = 10028;
					public const int DataLakeWorkspace = 10029;
					public const int DataLakeWorkspacePermission = 10030;
					public const int DataProcessingConfiguration = 10031;
					public const int SynapseDatabase = 10032;
					public const int SynapseLinkExternalTableState = 10033;
					public const int SynapseLinkProfile = 10034;
					public const int SynapseLinkProfileEntity = 10035;
					public const int SynapseLinkProfileEntityState = 10036;
					public const int SynapseLinkSchedule = 10037;
					public const int DataflowRefreshHistory = 10038;
					public const int EntityRefreshHistory = 10039;
					public const int SharedLinkSetting = 10040;
					public const int CascadeGrantRevokeAccessRecordsTracker = 10041;
					public const int CascadeGrantRevokeAccessVersionTracker = 10042;
					public const int RevokeInheritedAccessRecordsTracker = 10043;
					public const int ModelDrivenAppElement = 10044;
					public const int ModelDrivenAppComponentNode_sEdge = 10045;
					public const int ModelDrivenAppComponentNode = 10046;
					public const int ModelDrivenAppSetting = 10047;
					public const int ModelDrivenAppUserSetting = 10048;
					public const int OrganizationSetting = 10049;
					public const int SettingDefinition = 10050;
					public const int CanvasAppExtendedMetadata = 10051;
					public const int ServicePlanMapping = 10052;
					public const int ApplicationUser = 10054;
					public const int ODataV4DataSource = 10057;
					public const int FlowMachine = 10058;
					public const int FlowMachineGroup = 10059;
					public const int FlowMachineImage = 10060;
					public const int FlowMachineImageVersion = 10061;
					public const int ProcessStageParameter = 10062;
					public const int WorkflowBinary = 10063;
					public const int ConnectionReference = 10064;
					public const int AIBuilderFeedbackLoop = 10065;
					public const int AIFormProcessingDocument = 10066;
					public const int AIObjectDetectionImage = 10067;
					public const int AIObjectDetectionLabel = 10068;
					public const int AIObjectDetectionBoundingBox = 10069;
					public const int AIObjectDetectionImageMapping = 10070;
					public const int AIBuilderDataset = 10072;
					public const int AIBuilderDatasetFile = 10073;
					public const int AIBuilderDatasetRecord = 10074;
					public const int AIBuilderDatasetsContainer = 10075;
					public const int AIBuilderFile = 10076;
					public const int AIBuilderFileAttachedData = 10077;
					public const int HelpPage = 10078;
					public const int Tour = 10079;
					public const int BotContent = 10080;
					public const int ConversationTranscript = 10081;
					public const int Chatbot = 10082;
					public const int ChatbotSubcomponent = 10083;
					public const int Comment_ = 10089;
					public const int PDFSetting = 10090;
					public const int ActivityFileAttachment = 10091;
					public const int TeamsChat = 10092;
					public const int ServiceConfiguration = 10093;
					public const int SLAKPI = 10094;
					public const int KnowledgeManagementSetting = 10095;
					public const int KnowledgeFederatedArticle = 10096;
					public const int KnowledgeFederatedArticleIncident = 10097;
					public const int SearchProvider = 10098;
					public const int KnowledgeArticleImage = 10099;
					public const int KnowledgeInteractionInsight = 10100;
					public const int KnowledgeSearchInsight = 10101;
					public const int KnowledgeArticleLanguageSetting = 10102;
					public const int KnowledgeArticleAttachment = 10103;
					public const int KnowledgePersonalization = 10104;
					public const int KnowledgeArticleTemplate = 10105;
					public const int KnowledgeSearchPersonalFilterConfig = 10106;
					public const int KnowledgeSearchFilter = 10107;
					public const int PluginPackage = 10109;
					public const int KeyVaultReference = 10110;
					public const int ManagedIdentity = 10111;
					public const int VirtualEntityMetadata = 10112;
					public const int MobileOfflineProfileItemFilter = 10113;
					public const int TeamMobileOfflineProfileMembership = 10114;
					public const int UserMobileOfflineProfileMembership = 10115;
					public const int OrganizationDataSyncSubscription = 10116;
					public const int OrganizationDataSyncSubscriptionEntity = 10117;
					public const int OrganizationDataSyncState = 10118;
					public const int NonRelationalDataSource = 10119;
					public const int Notification_ = 10120;
					public const int UserRating = 10121;
					public const int InsightsStoreDataSource = 10122;
					public const int InsightsStoreVirtualEntity = 10123;
					public const int AppAction = 10124;
					public const int AppActionMigration = 10125;
					public const int AppActionRule = 10126;
					public const int RichTextAttachment = 10129;
					public const int CustomControlExtendedSetting = 10130;
					public const int SearchTelemetry = 10131;
					public const int PMAnalysisHistory = 10132;
					public const int PMInferredTask = 10133;
					public const int PMRecording = 10134;
					public const int PMTemplate = 10135;
					public const int AnalysisComponent = 10136;
					public const int AnalysisJob = 10137;
					public const int AnalysisResult = 10138;
					public const int AnalysisResultDetail = 10139;
					public const int SolutionHealthRule = 10140;
					public const int SolutionHealthRuleArgument = 10141;
					public const int SolutionHealthRuleSet = 10142;
					public const int ListOperation = 10143;
					public const int MarketingFormDisplayAttributes = 10144;
					public const int DatabaseVersion = 10145;
					public const int UpgradeRun = 10146;
					public const int UpgradeStep = 10147;
					public const int UpgradeVersion = 10148;
					public const int ActivityMonitor = 10149;
					public const int UnifiedRoutingSetupTracker = 10150;
					public const int AvailableTimes = 10151;
					public const int AvailableTimesDataSource = 10152;
					public const int ResourceGroupDataSource = 10153;
					public const int VirtualResourceGroupResource = 10154;
					public const int MigrationTracker = 10155;
					public const int AssetCategoryTemplateAssociation = 10156;
					public const int AssetTemplateAssociation = 10157;
					public const int CustomerAsset = 10158;
					public const int CustomerAssetAttachment = 10161;
					public const int CustomerAssetCategory = 10162;
					public const int FunctionalLocation = 10163;
					public const int PropertyDefinition = 10164;
					public const int PropertyAssetAssociation = 10165;
					public const int PropertyLog = 10166;
					public const int PropertyTemplateAssociation = 10167;
					public const int TemplateForProperties = 10168;
					public const int IoTAlert = 10172;
					public const int IoTDevice = 10173;
					public const int IoTDeviceCategory = 10174;
					public const int IoTDeviceCommand = 10175;
					public const int IoTDeviceCommandDefinition = 10176;
					public const int IoTDeviceDataHistory = 10177;
					public const int IoTDeviceProperty = 10178;
					public const int IoTDeviceRegistrationHistory = 10179;
					public const int IoTDeviceVisualizationConfiguration = 10180;
					public const int IoTFieldMapping = 10181;
					public const int IoTPropertyDefinition = 10182;
					public const int IoTProvider = 10183;
					public const int IoTProviderInstance = 10184;
					public const int IoTSettings = 10185;
					public const int IoTAlertToCaseProcess = 10188;
					public const int AppProfile = 10190;
					public const int ApplicationExtension = 10191;
					public const int ApplicationTabTemplate = 10192;
					public const int NotificationField = 10193;
					public const int NotificationTemplate = 10194;
					public const int SessionTemplate = 10195;
					public const int TemplateParameter = 10196;
					public const int ChannelIntegrationFrameworkV2_0Provider = 10202;
					public const int ProductivityPaneConfiguration = 10204;
					public const int PaneTabConfiguration = 10205;
					public const int PaneToolConfiguration = 10206;
					public const int AgentScript = 10208;
					public const int AgentScriptStep = 10209;
					public const int ActionInputParameter = 10211;
					public const int ActionOutputParameter = 10212;
					public const int MacroActionTemplate = 10213;
					public const int MacroSolutionConfiguration = 10214;
					public const int MacroConnector = 10215;
					public const int MacroRunHistory = 10216;
					public const int ParameterDefinition = 10217;
					public const int AdaptiveCardConfiguration = 10220;
					public const int SmartassistConfiguration = 10221;
					public const int CaseEnrichment = 10223;
					public const int CaseSuggestion = 10224;
					public const int CaseSuggestionRequestPayload = 10225;
					public const int CaseSuggestionsDataSouce = 10226;
					public const int KBEnrichment = 10227;
					public const int KnowledgeArticleSuggestion = 10228;
					public const int KnowledgeArticleSuggestionDataSource = 10229;
					public const int SuggestionInteraction = 10230;
					public const int SuggestionRequestPayload = 10231;
					public const int SuggestionsModelSummary = 10232;
					public const int SuggestionsSetting = 10233;
					public const int DataAnalyticsAdminSettings_Deprecated_ = 10234;
					public const int DataAnalyticsUserCustomizedReport = 10235;
					public const int DataAnalyticsReport = 10236;
					public const int Insights = 10237;
					public const int _Deprecated_DynamicsCustomerServiceAnalytics = 10238;
					public const int CaseTopic = 10239;
					public const int CaseTopicSetting = 10240;
					public const int CaseTopicSummary = 10241;
					public const int CaseTopicIncidentMapping = 10242;
					public const int CustomerServiceHistoricalAnalytics = 10243;
					public const int Forecast_preview_ = 10244;
					public const int KnowledgeAnalytics = 10245;
					public const int ForecastSummaryAndSetting = 10246;
					public const int KeywordsDescriptionSuggestionSetting = 10247;
					public const int PlaybookCallableContext = 10248;
					public const int PlaybookActivity = 10249;
					public const int PlaybookActivityAttribute = 10250;
					public const int PlaybookCategory = 10251;
					public const int Playbook = 10252;
					public const int PlaybookTemplate = 10253;
					public const int AdminSettingsEntity = 10255;
					public const int SalesUsageTelemetryReports = 10256;
					public const int SalesUsageReporting = 10257;
					public const int ConversationData_Deprecated_ = 10258;
					public const int KPIEventData = 10259;
					public const int KPIEventDefinition = 10260;
					public const int SessionData_Deprecated_ = 10261;
					public const int SessionParticipantData_Deprecated_ = 10262;
					public const int MicrosoftTeamsGraphResourceEntity = 10263;
					public const int MsdynMsteamssetting = 10264;
					public const int MsdynMsteamssettingsv2 = 10265;
					public const int MicrosoftTeamsCollaborationEntity = 10266;
					public const int TeamsDialerAdminSettings = 10267;
					public const int TeamsContactSuggestionByAI = 10268;
					public const int ContactSuggestionRule = 10269;
					public const int ContactSuggestionRuleset = 10270;
					public const int EntityLinkChatConfiguration = 10271;
					public const int MicrosoftTeamsChatAssociationEntity = 10272;
					public const int MicrosoftTeamsChatSuggestion = 10273;
					public const int ForecastConfiguration = 10274;
					public const int ForecastDefinition = 10275;
					public const int Forecast = 10276;
					public const int ForecastRecurrence = 10277;
					public const int GDPRData = 10278;
					public const int MsdynRelationshipinsightsunifiedconfig = 10279;
					public const int Siconfig = 10280;
					public const int SIKeyValueConfig = 10281;
					public const int UsageMetric = 10282;
					public const int ActionCardRegarding = 10283;
					public const int ActionCardRoleSetting = 10284;
					public const int EntityRankingRule = 10285;
					public const int Flowcardtype = 10286;
					public const int Salesinsightssettings = 10287;
					public const int AutoCaptureRule = 10288;
					public const int AutoCaptureSettings = 10289;
					public const int UntrackedAppointment = 10290;
					public const int SuggestedActivity = 10291;
					public const int SuggestedActivityDataSource = 10292;
					public const int SuggestedContact = 10293;
					public const int SuggestedContactsDataSource = 10294;
					public const int NotesAnalysisConfig = 10295;
					public const int Icebreakersconfig = 10296;
					public const int SalesTag = 10297;
					public const int Sequence = 10298;
					public const int SequenceStat = 10299;
					public const int SequenceTarget = 10300;
					public const int SequenceTargetStep = 10301;
					public const int SequenceTemplate = 10302;
					public const int Segment = 10304;
					public const int SegmentsUtil = 10305;
					public const int AssignmentRule = 10306;
					public const int Attribute_ = 10307;
					public const int AttributeValue = 10308;
					public const int AssignmentMap = 10309;
					public const int SalesAssignmentSetting = 10310;
					public const int SalesRoutingRun = 10311;
					public const int ExtendedUserSetting = 10313;
					public const int SalesAccelerationSettings = 10314;
					public const int Suggestion = 10315;
					public const int WorkListSuggestion = 10316;
					public const int WorkListSuggestionSource = 10317;
					public const int WorkListViewConfiguration = 10318;
					public const int WorkQueueRecord = 10319;
					public const int WorkQueueRecordState = 10320;
					public const int WorkListUserSetting = 10321;
					public const int WQDataSource = 10322;
					public const int Dealmanageraccess = 10323;
					public const int DealManagerSettings = 10324;
					public const int Recording = 10325;
					public const int PredictiveModelScore = 10326;
					public const int PredictiveScore = 10327;
					public const int TimeSpentInBPF = 10328;
					public const int OpportunityModelConfig = 10329;
					public const int LeadModelConfig = 10330;
					public const int ModelPreviewStatus = 10331;
					public const int DuplicateDetectionPluginRun = 10332;
					public const int DuplicateLeadMapping = 10333;
					public const int LeadHygieneSetting = 10334;
					public const int ProfileAlbum = 10335;
					public const int PostConfiguration = 10336;
					public const int PostRuleConfiguration = 10337;
					public const int WallView = 10338;
					public const int Filter = 10339;
					public const int CustomerVoiceAlert = 10340;
					public const int CustomerVoiceAlertRule = 10341;
					public const int CustomerVoiceSurveyEmailTemplate = 10342;
					public const int CustomerVoiceFileResponse = 10343;
					public const int CustomerVoiceLocalizedSurveyEmailTemplate = 10344;
					public const int CustomerVoiceProject = 10345;
					public const int CustomerVoiceSurveyQuestion = 10346;
					public const int CustomerVoiceSurveyQuestionResponse = 10347;
					public const int CustomerVoiceSatisfactionMetric = 10348;
					public const int CustomerVoiceSurvey = 10349;
					public const int CustomerVoiceSurveyInvite = 10350;
					public const int CustomerVoiceSurveyReminder = 10351;
					public const int CustomerVoiceSurveyResponse = 10352;
					public const int CustomerVoiceUnsubscribedRecipient = 10353;
					public const int CSAdminConfig = 10354;
					public const int DecisionContract = 10355;
					public const int DecisionRuleSet = 10356;
					public const int Rulesetentitymapping = 10357;
					public const int RoutingDiagnosticItem = 10358;
					public const int RoutingDiagnostic = 10359;
					public const int InboxConfiguration = 10360;
					public const int Swarm = 10361;
					public const int SwarmParticipant = 10362;
					public const int SwarmParticipantRule = 10363;
					public const int SwarmRole = 10364;
					public const int SwarmSkill = 10365;
					public const int SwarmTemplate = 10366;
					public const int MasterEntityRoutingConfiguration = 10367;
					public const int RoutingRuleSetSetting = 10368;
					public const int AssignmentConfiguration = 10369;
					public const int AssignmentConfigurationStep = 10370;
					public const int CapacityProfile = 10371;
					public const int OverflowActionConfig = 10372;
					public const int RoutingConfiguration = 10373;
					public const int RoutingConfigurationStep = 10374;
					public const int BotChannelRegistration = 10375;
					public const int ChannelConfiguration = 10376;
					public const int ChannelStateConfiguration = 10377;
					public const int ProvisioningState = 10378;
					public const int AdminAppState = 10379;
					public const int AgentStatusHistory = 10380;
					public const int PowerBIConfiguration = 10381;
					public const int AuthenticationSettings = 10382;
					public const int AuthSettingsEntry = 10383;
					public const int QuickReply = 10384;
					public const int EntityRoutingContext = 10385;
					public const int ChannelCapability = 10386;
					public const int ConversationAction = 10387;
					public const int ConversationActionLocale = 10388;
					public const int DeprecatedWorkstreamEntityConfiguration = 10389;
					public const int Entity_ = 10390;
					public const int OngoingConversation_Deprecated_ = 10391;
					public const int LiveWorkItemEvent = 10392;
					public const int WorkStream = 10393;
					public const int MaskingRule = 10394;
					public const int AutoBlockRule = 10395;
					public const int OmnichannelChannelApiConversationPrivilege = 10396;
					public const int OmnichannelChannelApiMessagePrivilege = 10397;
					public const int ChannelApiMethodMapping = 10398;
					public const int FlaggedSpam = 10399;
					public const int Language_ = 10400;
					public const int Conversation = 10401;
					public const int ContextItemValue = 10404;
					public const int LiveWorkItemParticipant_Deprecated_ = 10405;
					public const int ConversationSentiment = 10406;
					public const int ContextVariable = 10407;
					public const int Localization = 10408;
					public const int OCPaymentProfile = 10409;
					public const int Recording_ = 10410;
					public const int OmnichannelRequest = 10411;
					public const int RichMessage = 10412;
					public const int RichMessageMap = 10413;
					public const int RuleItem_ = 10414;
					public const int SentimentDailyTopic = 10415;
					public const int SentimentDailyTopicKeyword = 10416;
					public const int SentimentDailyTopicTrending = 10417;
					public const int Session = 10418;
					public const int SessionParticipantEvent = 10419;
					public const int SessionSentiment = 10420;
					public const int Message = 10421;
					public const int Tag = 10422;
					public const int GeoLocationProvider = 10423;
					public const int OmnichannelConfiguration = 10424;
					public const int OmnichannelPersonalization = 10425;
					public const int OmnichannelQueue_Deprecated_ = 10426;
					public const int OmnichannelSyncConfig = 10427;
					public const int OperatingHour = 10428;
					public const int PersonalQuickReply = 10429;
					public const int PersonalSoundSetting = 10430;
					public const int PersonaSecurityRoleMapping = 10431;
					public const int Presence = 10432;
					public const int Provider = 10433;
					public const int RoutingRequest = 10434;
					public const int SearchConfiguration = 10435;
					public const int SentimentAnalysis = 10436;
					public const int SessionEvent = 10437;
					public const int SessionParticipant = 10438;
					public const int AudioFile = 10439;
					public const int SoundNotificationSetting = 10440;
					public const int Transcript = 10441;
					public const int URNotificationTemplate = 10442;
					public const int URNotificationTemplateMapping = 10443;
					public const int UserSettings_ = 10444;
					public const int SelfService = 10445;
					public const int BookableResourceCapacityProfile = 10452;
					public const int WorkStreamCapacityProfile = 10453;
					public const int ConversationCapacityProfile = 10454;
					public const int ConversationCharacteristic = 10455;
					public const int SessionCharacteristic = 10456;
					public const int SkillAttachmentRule = 10457;
					public const int AttachSkill = 10458;
					public const int ModelTrainingDetails = 10459;
					public const int TrainingDataImportConfiguration = 10460;
					public const int CharacteristicMapping = 10461;
					public const int TrainingRecord = 10462;
					public const int SkillFinderModel = 10463;
					public const int EffortEstimate = 10464;
					public const int EffortEstimationModel = 10465;
					public const int EffortModelTrainingDetails = 10466;
					public const int GDPRBPFCorrection = 10552;
					public const int GDPRBPFDeletion = 10553;
					public const int GDPRBPFInformation = 10554;
					public const int GDPREntityConfiguration = 10555;
					public const int GDPRFieldConfiguration = 10556;
					public const int GDPRHierarchyConfiguration = 10557;
					public const int GDPRProtocol = 10558;
					public const int GDPRProtocolDetail = 10559;
					public const int GDPRReport = 10560;
					public const int GDPRRequest = 10561;
					public const int AcquireLegalBasis = 10562;
					public const int LegalBasis = 10563;
					public const int LegalBasisType = 10564;
					public const int AddToCalendarStyle = 10599;
					public const int Basestyle = 10600;
					public const int ButtonStyle = 10601;
					public const int CodeStyle = 10602;
					public const int ColumnStyle = 10603;
					public const int ContentBlock = 10604;
					public const int DividerStyle = 10605;
					public const int GeneralStyles = 10606;
					public const int Imagestyle = 10607;
					public const int LayoutStyle = 10608;
					public const int QRCodeStyle = 10609;
					public const int TextStyle = 10610;
					public const int VideoStyle = 10611;
					public const int CustomerEmailCommunication = 10612;
					public const int SalesAccelerationInsights = 10613;
					public const int AttributeInfluenceStatistics = 10614;
					public const int RecurringSalesAction = 10615;
					public const int SABatchRunInstance = 10616;
					public const int SARunInstance = 10617;
					public const int ForecastManualAdjustmentHistory = 10618;
					public const int DistributedLock = 10619;
					public const int EntityDeltaChange = 10620;
					public const int FileUploadStatusTracker = 10621;
					public const int Forecast_ = 10622;
					public const int ForecastingCache = 10623;
					public const int ForecastInsights = 10624;
					public const int ForecastPredictionData = 10625;
					public const int ForecastPredictionStatus = 10626;
					public const int RecomputeTracker = 10627;
					public const int ForecastRecurrence_ = 10628;
					public const int ShareAsConfiguration = 10629;
					public const int AppInsightsMetadata = 10630;
					public const int CRMConnection = 10631;
					public const int TaggedRecord = 10632;
					public const int MsGraphResourceToSubscription = 10633;
					public const int PMProcessUserSettings = 10634;
					public const int ReportBookmark = 10635;
					public const int Card = 10646;
					public const int DesktopFlowBinary = 10647;
					public const int FlowMachineNetwork = 10648;
					public const int Sabackupdiagnostic = 10649;
					public const int Salesroutingdiagnostic = 10650;
					public const int Segmentsetting = 10651;
					public const int Segmentattribute = 10652;
					public const int DataHygieneSettingInfo = 10653;
					public const int LinkedEntityAttributeValidity = 10654;
					public const int PMCalendar = 10655;
					public const int PMCalendarVersion = 10656;
					public const int PMProcessExtendedMetadataVersion = 10657;
					public const int PMProcessVersion = 10658;
					public const int PMView = 10659;
					public const int CustomAPIRulesetConfiguration = 10660;
					public const int DataAnalyticsDataset = 10661;
					public const int DataAnalyticsWorkspace = 10662;
					public const int AgentResourceForecasting = 10663;
					public const int IntegratedSearchProvider = 10664;
					public const int PowerfxRule = 10665;
					public const int WorkflowActionStatus = 10666;
					public const int SupportUserTable = 10687;
					public const int PowerBIDataset = 10688;
					public const int PowerBIMashupParameter = 10689;
					public const int PowerBIReport = 10690;
					public const int VirtualConnectorDataSource = 10691;
					public const int VirtualTableColumnCandidate = 10692;
					public const int RealTimeScoring = 10693;
					public const int RoleEditorLayout = 10694;
					public const int SearchRelationshipSettings = 10695;
					public const int VivaSalesCustomerList = 10696;
					public const int MsdynVivaentitysetting = 10697;
					public const int MsdynVivaorgsetting = 10698;
					public const int MsdynVivausersetting = 10699;
					public const int TdsMetadata = 10700;
					public const int ChannelDefinition = 10701;
					public const int ChannelDefinitionConsent = 10702;
					public const int ChannelDefinitionLocale = 10703;
					public const int ChannelInstance = 10704;
					public const int ChannelInstanceAccount = 10705;
					public const int ChannelMessagePart = 10706;
					public const int ConsumingApplication = 10707;
					public const int MsdynDefExtendedChannelInstance = 10708;
					public const int MsdynDefExtendedChannelInstanceAccount = 10709;
					public const int DesktopFlowModule = 10710;
                }
			    public struct SLAType
                {
					public const int Standard = 0;
					public const int Enhanced = 1;
                }
			    public struct Slaversion
                {
					public const int VersionWC = 100000000;
					public const int VersionUC = 100000001;
                }
                public struct StateCode
                {
					public const int Draft = 0;
					public const int Active = 1;
                }
                public struct StatusCode
                {
					public const int Draft = 1;
					public const int Active = 2;
                }
		}
		#endregion

		#region LogicalNames
		public static class LogicalNames
		{
				public const string SLAId = "slaid";
				public const string AllowPauseResume = "allowpauseresume";
				public const string AllowPauseResumeName = "allowpauseresumename";
				public const string ApplicableFrom = "applicablefrom";
				public const string ApplicableFromPickList = "applicablefrompicklist";
				public const string BusinessHoursId = "businesshoursid";
				public const string ChangedAttributeList = "changedattributelist";
				public const string ComponentState = "componentstate";
				public const string CreatedBy = "createdby";
				public const string CreatedOn = "createdon";
				public const string CreatedOnBehalfBy = "createdonbehalfby";
				public const string Description = "description";
				public const string ExchangeRate = "exchangerate";
				public const string IsDefault = "isdefault";
				public const string IsManaged = "ismanaged";
				public const string ModifiedBy = "modifiedby";
				public const string ModifiedOn = "modifiedon";
				public const string ModifiedOnBehalfBy = "modifiedonbehalfby";
				public const string Name = "name";
				public const string ObjectTypeCode = "objecttypecode";
				public const string OverwriteTime = "overwritetime";
				public const string OwnerId = "ownerid";
				public const string OwnerIdType = "owneridtype";
				public const string OwningBusinessUnit = "owningbusinessunit";
				public const string OwningTeam = "owningteam";
				public const string OwningUser = "owninguser";
				public const string PrimaryEntityOTC = "primaryentityotc";
				public const string SLAIdUnique = "slaidunique";
				public const string SLAType = "slatype";
				public const string SLATypeName = "slatypename";
				public const string Slaversion = "slaversion";
				public const string SolutionId = "solutionid";
				public const string StateCode = "statecode";
				public const string StatusCode = "statuscode";
				public const string TransactionCurrencyId = "transactioncurrencyid";
				public const string VersionNumber = "versionnumber";
				public const string WorkflowId = "workflowid";
				public const string WorkflowIdName = "workflowidname";
		}
		#endregion

		#region Relations
        public static class Relations
        {
            public static class OneToMany
            {
				public const string BulkoperationSlaSlaid = "bulkoperation_sla_slaid";
				public const string BulkoperationSlaSlainvokedid = "bulkoperation_sla_slainvokedid";
				public const string CampaignactivitySlaSlaid = "campaignactivity_sla_slaid";
				public const string CampaignactivitySlaSlainvokedid = "campaignactivity_sla_slainvokedid";
				public const string CampaignresponseSlaSlaid = "campaignresponse_sla_slaid";
				public const string CampaignresponseSlaSlainvokedid = "campaignresponse_sla_slainvokedid";
				public const string ChatSlaSlaid = "chat_sla_slaid";
				public const string ChatSlaSlainvokedid = "chat_sla_slainvokedid";
				public const string IncidentresolutionSlaSlaid = "incidentresolution_sla_slaid";
				public const string IncidentresolutionSlaSlainvokedid = "incidentresolution_sla_slainvokedid";
				public const string ManualslaAccount = "manualsla_account";
				public const string ManualslaActivitypointer = "manualsla_activitypointer";
				public const string ManualslaAppointment = "manualsla_appointment";
				public const string ManualslaCases = "manualsla_cases";
				public const string ManualslaContact = "manualsla_contact";
				public const string ManualslaEmail = "manualsla_email";
				public const string ManualslaFax = "manualsla_fax";
				public const string ManualslaInvoice = "manualsla_invoice";
				public const string ManualslaLead = "manualsla_lead";
				public const string ManualslaLetter = "manualsla_letter";
				public const string ManualslaOpportunity = "manualsla_opportunity";
				public const string ManualslaPhonecall = "manualsla_phonecall";
				public const string ManualslaQuote = "manualsla_quote";
				public const string ManualslaSalesorder = "manualsla_salesorder";
				public const string ManualslaServiceappointment = "manualsla_serviceappointment";
				public const string ManualslaSocialactivity = "manualsla_socialactivity";
				public const string ManualslaTask = "manualsla_task";
				public const string MsdynMigrationtrackerLegacySLASla = "msdyn_migrationtracker_LegacySLA_sla";
				public const string MsdynMigrationtrackerModernSLASla = "msdyn_migrationtracker_ModernSLA_sla";
				public const string MsdynOcliveworkitemSlaSlaid = "msdyn_ocliveworkitem_sla_slaid";
				public const string MsdynOcliveworkitemSlaSlainvokedid = "msdyn_ocliveworkitem_sla_slainvokedid";
				public const string MsdynOcsessionSlaSlaid = "msdyn_ocsession_sla_slaid";
				public const string MsdynOcsessionSlaSlainvokedid = "msdyn_ocsession_sla_slainvokedid";
				public const string MsfpAlertSlaSlaid = "msfp_alert_sla_slaid";
				public const string MsfpAlertSlaSlainvokedid = "msfp_alert_sla_slainvokedid";
				public const string MsfpSurveyinviteSlaSlaid = "msfp_surveyinvite_sla_slaid";
				public const string MsfpSurveyinviteSlaSlainvokedid = "msfp_surveyinvite_sla_slainvokedid";
				public const string MsfpSurveyresponseSlaSlaid = "msfp_surveyresponse_sla_slaid";
				public const string MsfpSurveyresponseSlaSlainvokedid = "msfp_surveyresponse_sla_slainvokedid";
				public const string OpportunitycloseSlaSlaid = "opportunityclose_sla_slaid";
				public const string OpportunitycloseSlaSlainvokedid = "opportunityclose_sla_slainvokedid";
				public const string OrdercloseSlaSlaid = "orderclose_sla_slaid";
				public const string OrdercloseSlaSlainvokedid = "orderclose_sla_slainvokedid";
				public const string QuotecloseSlaSlaid = "quoteclose_sla_slaid";
				public const string QuotecloseSlaSlainvokedid = "quoteclose_sla_slainvokedid";
				public const string SlaAccount = "sla_account";
				public const string SlaActivitypointer = "sla_activitypointer";
				public const string SlaAnnotation = "sla_Annotation";
				public const string SlaAppointment = "sla_appointment";
				public const string SlaCases = "sla_cases";
				public const string SlaContact = "sla_contact";
				public const string SlaEmail = "sla_email";
				public const string SlaEntitlement = "sla_entitlement";
				public const string SlaEntitlementtemplate = "sla_entitlementtemplate";
				public const string SlaFax = "sla_fax";
				public const string SlaInvoice = "sla_invoice";
				public const string SlaLead = "sla_lead";
				public const string SlaLetter = "sla_letter";
				public const string SlaOpportunity = "sla_opportunity";
				public const string SlaPhonecall = "sla_phonecall";
				public const string SlaQuote = "sla_quote";
				public const string SlaSalesorder = "sla_salesorder";
				public const string SlaServiceappointment = "sla_serviceappointment";
				public const string SlaSlaitemSlaId = "sla_slaitem_slaId";
				public const string SlaSocialactivity = "sla_socialactivity";
				public const string SLASyncErrors = "SLA_SyncErrors";
				public const string SlaTask = "sla_task";
				public const string SlabaseAsyncOperations = "slabase_AsyncOperations";
				public const string SlabaseBulkDeleteFailures = "slabase_BulkDeleteFailures";
				public const string SlabaseProcessSessions = "slabase_ProcessSessions";
				public const string SlabaseUserentityinstancedatas = "slabase_userentityinstancedatas";
            }

            public static class ManyToOne
            {
				public const string BusinessUnitSlabase = "business_unit_slabase";
				public const string LkSlabaseCreatedby = "lk_slabase_createdby";
				public const string LkSlabaseCreatedonbehalfby = "lk_slabase_createdonbehalfby";
				public const string LkSlabaseModifiedby = "lk_slabase_modifiedby";
				public const string LkSlabaseModifiedonbehalfby = "lk_slabase_modifiedonbehalfby";
				public const string OwnerSlas = "owner_slas";
				public const string SlabaseBusinesshoursid = "slabase_businesshoursid";
				public const string SlabaseWorkflowid = "slabase_workflowid";
				public const string TeamSlaBase = "team_slaBase";
				public const string TransactionCurrencySLA = "TransactionCurrency_SLA";
				public const string UserSlabase = "user_slabase";
            }

            public static class ManyToMany
            {
            }
        }

        #endregion

		#region Methods

        public static SLA Retrieve(IOrganizationService service, Guid id)
        {
            return Retrieve(service,id, new ColumnSet(true));
        }

        public static SLA Retrieve(IOrganizationService service, Guid id, ColumnSet columnSet)
        {
            return service.Retrieve("sla", id, columnSet).ToEntity<SLA>();
        }

        public SLA GetChangedEntity()
        {
            if (_trackChanges)
            {
                var attr = new AttributeCollection();
                foreach (var attrName in _changedProperties.Value.Select(changedProperty => ((AttributeLogicalNameAttribute) GetType().GetProperty(changedProperty)!.GetCustomAttribute(typeof (AttributeLogicalNameAttribute))!).LogicalName).Where(attrName => Contains(attrName)))
                {
                    attr.Add(attrName,this[attrName]);
                }
                return new  SLA(Id) {Attributes = attr };
            }
            return this;
        }
        #endregion
	}

	#region Context
	public partial class DataContext
	{
		public IQueryable<SLA> SLASet
		{
			get
			{
				return CreateQuery<SLA>();
			}
		}
	}
	#endregion
}
