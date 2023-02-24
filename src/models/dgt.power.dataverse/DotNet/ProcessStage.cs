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
	/// Stage associated with a process.
	/// </summary>
	[DataContractAttribute()]
	[EntityLogicalNameAttribute("processstage")]
	[System.CodeDom.Compiler.GeneratedCode("dgtp", "2023")]
    [ExcludeFromCodeCoverage]
	public partial class ProcessStage : Entity, INotifyPropertyChanging, INotifyPropertyChanged
    {
	    #region ctor
		[DebuggerNonUserCode]
		public ProcessStage() : this(false)
        {
        }

        [DebuggerNonUserCode]
		public ProcessStage(bool trackChanges = false) : base(EntityLogicalName)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public ProcessStage(Guid id, bool trackChanges = false) : base(EntityLogicalName,id)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public ProcessStage(KeyAttributeCollection keyAttributes, bool trackChanges = false) : base(EntityLogicalName,keyAttributes)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public ProcessStage(string keyName, object keyValue, bool trackChanges = false) : base(EntityLogicalName, keyName, keyValue)
        {
			_trackChanges = trackChanges;
        }
        #endregion

		#region fields
        private readonly bool _trackChanges;
        private readonly Lazy<HashSet<string>> _changedProperties = new Lazy<HashSet<string>>();
        #endregion

        #region consts
        public const string EntityLogicalName = "processstage";
        public const string PrimaryNameAttribute = "stagename";
        public const int EntityTypeCode = 4724;
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
		[AttributeLogicalNameAttribute("processstageid")]
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
				ProcessStageId = value;
			}
		}

		/// <summary>
		/// Shows the ID of the process stage record.
		/// </summary>
		[AttributeLogicalName("processstageid")]
        public Guid? ProcessStageId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<Guid?>("processstageid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(ProcessStageId));
                SetAttributeValue("processstageid", value);
				if (value.HasValue)
				{
					base.Id = value.Value;
				}
				else
				{
					base.Id = System.Guid.Empty;
				}
                OnPropertyChanged(nameof(ProcessStageId));
            }
        }

		/// <summary>
		/// Step metadata for process stage
		/// </summary>
		[AttributeLogicalName("clientdata")]
        public string? ClientData
        {
            [DebuggerNonUserCode]
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
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("connector");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(Connector));
                SetAttributeValue("connector", value);
                OnPropertyChanged(nameof(Connector));
            }
        }

		/// <summary>
		/// Whether the stage is a trigger
		/// </summary>
		[AttributeLogicalName("istrigger")]
        public bool? IsTrigger
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<bool?>("istrigger");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(IsTrigger));
                SetAttributeValue("istrigger", value);
                OnPropertyChanged(nameof(IsTrigger));
            }
        }

		/// <summary>
		/// The operation id of the stage
		/// </summary>
		[AttributeLogicalName("operationid")]
        public string? OperationId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("operationid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(OperationId));
                SetAttributeValue("operationid", value);
                OnPropertyChanged(nameof(OperationId));
            }
        }

		/// <summary>
		/// The operation kind
		/// </summary>
		[AttributeLogicalName("operationkind")]
        public OptionSetValue? OperationKind
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<OptionSetValue?>("operationkind");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(OperationKind));
                SetAttributeValue("operationkind", value);
                OnPropertyChanged(nameof(OperationKind));
            }
        }

		/// <summary>
		/// The type of the operation
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
                OnPropertyChanging(nameof(OperationType));
                SetAttributeValue("operationtype", value);
                OnPropertyChanged(nameof(OperationType));
            }
        }

		/// <summary>
		/// Enter the user or team who is assigned to manage the record. This field is updated every time the record is assigned to a different user.
		/// </summary>
		[AttributeLogicalName("ownerid")]
        public EntityReference? OwnerId
        {
            [DebuggerNonUserCode]
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
            [DebuggerNonUserCode]
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
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("parametername");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(ParameterName));
                SetAttributeValue("parametername", value);
                OnPropertyChanged(nameof(ParameterName));
            }
        }

		/// <summary>
		/// The parameter value.
		/// </summary>
		[AttributeLogicalName("parametervalue")]
        public string? ParameterValue
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("parametervalue");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(ParameterValue));
                SetAttributeValue("parametervalue", value);
                OnPropertyChanged(nameof(ParameterValue));
            }
        }

		/// <summary>
		/// The parent stage for the parameter.
		/// </summary>
		[AttributeLogicalName("parentprocessstageid")]
        public EntityReference? ParentProcessStageId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<EntityReference?>("parentprocessstageid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(ParentProcessStageId));
                SetAttributeValue("parentprocessstageid", value);
                OnPropertyChanged(nameof(ParentProcessStageId));
            }
        }

		/// <summary>
		/// Primary entity associated with the stage.
		/// </summary>
		[AttributeLogicalName("primaryentitytypecode")]
        public string? PrimaryEntityTypeCode
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("primaryentitytypecode");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(PrimaryEntityTypeCode));
                SetAttributeValue("primaryentitytypecode", value);
                OnPropertyChanged(nameof(PrimaryEntityTypeCode));
            }
        }

		/// <summary>
		/// Shows the ID of the process.
		/// </summary>
		[AttributeLogicalName("processid")]
        public EntityReference? ProcessId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<EntityReference?>("processid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(ProcessId));
                SetAttributeValue("processid", value);
                OnPropertyChanged(nameof(ProcessId));
            }
        }

		/// <summary>
		/// Select the category of the sales process.
		/// </summary>
		[AttributeLogicalName("stagecategory")]
        public OptionSetValue? StageCategory
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<OptionSetValue?>("stagecategory");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(StageCategory));
                SetAttributeValue("stagecategory", value);
                OnPropertyChanged(nameof(StageCategory));
            }
        }

		/// <summary>
		/// Type a name for the process stage.
		/// </summary>
		[AttributeLogicalName("stagename")]
        public string? StageName
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("stagename");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(StageName));
                SetAttributeValue("stagename", value);
                OnPropertyChanged(nameof(StageName));
            }
        }

		/// <summary>
		/// Version number of the process stage.
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
		/// 1:N processstage_account
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("processstage_account")]
		public System.Collections.Generic.IEnumerable<Account> ProcessstageAccount
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Account>("processstage_account", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("ProcessstageAccount");
				this.SetRelatedEntities<Account>("processstage_account", null, value);
				this.OnPropertyChanged("ProcessstageAccount");
			}
		}

		/// <summary>
		/// 1:N processstage_contact
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("processstage_contact")]
		public System.Collections.Generic.IEnumerable<Contact> ProcessstageContact
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Contact>("processstage_contact", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("ProcessstageContact");
				this.SetRelatedEntities<Contact>("processstage_contact", null, value);
				this.OnPropertyChanged("ProcessstageContact");
			}
		}

		/// <summary>
		/// 1:N processstage_ec4u_carrier
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("processstage_ec4u_carrier")]
		public System.Collections.Generic.IEnumerable<Ec4uCarrier> ProcessstageEc4uCarrier
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Ec4uCarrier>("processstage_ec4u_carrier", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("ProcessstageEc4uCarrier");
				this.SetRelatedEntities<Ec4uCarrier>("processstage_ec4u_carrier", null, value);
				this.OnPropertyChanged("ProcessstageEc4uCarrier");
			}
		}

		/// <summary>
		/// 1:N processstage_parentprocessstage
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("processstage_parentprocessstage")]
		public System.Collections.Generic.IEnumerable<ProcessStage> ProcessstageParentprocessstage
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<ProcessStage>("processstage_parentprocessstage", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("ProcessstageParentprocessstage");
				this.SetRelatedEntities<ProcessStage>("processstage_parentprocessstage", null, value);
				this.OnPropertyChanged("ProcessstageParentprocessstage");
			}
		}

		/// <summary>
		/// 1:N processstage_systemusers
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("processstage_systemusers")]
		public System.Collections.Generic.IEnumerable<SystemUser> ProcessstageSystemusers
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<SystemUser>("processstage_systemusers", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("ProcessstageSystemusers");
				this.SetRelatedEntities<SystemUser>("processstage_systemusers", null, value);
				this.OnPropertyChanged("ProcessstageSystemusers");
			}
		}

		/// <summary>
		/// 1:N processstage_teams
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("processstage_teams")]
		public System.Collections.Generic.IEnumerable<Team> ProcessstageTeams
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Team>("processstage_teams", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("ProcessstageTeams");
				this.SetRelatedEntities<Team>("processstage_teams", null, value);
				this.OnPropertyChanged("ProcessstageTeams");
			}
		}

		#endregion

		#region Options
		public static class Options
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
		public static class LogicalNames
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
        public static class Relations
        {
            public static class OneToMany
            {
				public const string LkEc4uGdprBpfCorrectionActivestageid = "lk_ec4u_gdpr_bpf_correction_activestageid";
				public const string LkEc4uGdprBpfDeletionActivestageid = "lk_ec4u_gdpr_bpf_deletion_activestageid";
				public const string LkEc4uGdprBpfInformationActivestageid = "lk_ec4u_gdpr_bpf_information_activestageid";
				public const string LkExpiredprocessActivestageid = "lk_expiredprocess_activestageid";
				public const string LkLeadtoopportunitysalesprocessActivestageid = "lk_leadtoopportunitysalesprocess_activestageid";
				public const string LkMsdynIottocaseprocessActivestageid = "lk_msdyn_iottocaseprocess_activestageid";
				public const string LkNewprocessActivestageid = "lk_newprocess_activestageid";
				public const string LkOpportunitysalesprocessActivestageid = "lk_opportunitysalesprocess_activestageid";
				public const string LkPhonetocaseprocessActivestageid = "lk_phonetocaseprocess_activestageid";
				public const string LkTranslationprocessActivestageid = "lk_translationprocess_activestageid";
				public const string MsdynProcessstageMsdynTimespentStage = "msdyn_processstage_msdyn_timespent_stage";
				public const string ProcessstageAccount = "processstage_account";
				public const string ProcessstageAppointments = "processstage_appointments";
				public const string ProcessstageBookableResource = "processstage_BookableResource";
				public const string ProcessstageBookableResourceBooking = "processstage_BookableResourceBooking";
				public const string ProcessstageBookableResourceBookingHeader = "processstage_BookableResourceBookingHeader";
				public const string ProcessstageBookableResourceCharacteristic = "processstage_BookableResourceCharacteristic";
				public const string ProcessstageCampaignactivities = "processstage_campaignactivities";
				public const string ProcessstageCampaignresponses = "processstage_campaignresponses";
				public const string ProcessstageCampaigns = "processstage_campaigns";
				public const string ProcessstageCompetitors = "processstage_competitors";
				public const string ProcessstageContact = "processstage_contact";
				public const string ProcessstageEc4uCarrier = "processstage_ec4u_carrier";
				public const string ProcessstageEc4uGdprRequest = "processstage_ec4u_gdpr_request";
				public const string ProcessstageEc4uWorkbench = "processstage_ec4u_workbench";
				public const string ProcessstageEmails = "processstage_emails";
				public const string ProcessstageEntitlement = "processstage_Entitlement";
				public const string ProcessstageFaxes = "processstage_faxes";
				public const string ProcessstageIncident = "processstage_incident";
				public const string ProcessstageInvoices = "processstage_invoices";
				public const string ProcessstageKnowledgearticle = "processstage_knowledgearticle";
				public const string ProcessstageLead = "processstage_lead";
				public const string ProcessstageLetters = "processstage_letters";
				public const string ProcessstageLists = "processstage_lists";
				public const string ProcessstageMsdynIotalert = "processstage_msdyn_iotalert";
				public const string ProcessstageOpportunity = "processstage_opportunity";
				public const string ProcessstageParentprocessstage = "processstage_parentprocessstage";
				public const string ProcessstagePhonecalls = "processstage_phonecalls";
				public const string ProcessstageProcessstageparameter = "processstage_processstageparameter";
				public const string ProcessstageProductpricelevels = "processstage_productpricelevels";
				public const string ProcessstageProducts = "processstage_products";
				public const string ProcessstageQuotes = "processstage_quotes";
				public const string ProcessstageRecurringappointmentmasters = "processstage_recurringappointmentmasters";
				public const string ProcessstageSalesliteratures = "processstage_salesliteratures";
				public const string ProcessstageSalesorders = "processstage_salesorders";
				public const string ProcessStageSyncErrors = "ProcessStage_SyncErrors";
				public const string ProcessstageSystemusers = "processstage_systemusers";
				public const string ProcessstageTasks = "processstage_tasks";
				public const string ProcessstageTeams = "processstage_teams";
            }

            public static class ManyToOne
            {
				public const string ProcessProcessstage = "process_processstage";
				public const string ProcessstageParentprocessstage = "processstage_parentprocessstage";
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
        public static ProcessStage Retrieve(IOrganizationService service, Guid id)
        {
            return Retrieve(service,id, new ColumnSet(true));
        }

        public static ProcessStage Retrieve(IOrganizationService service, Guid id, ColumnSet columnSet)
        {
            return service.Retrieve("processstage", id, columnSet).ToEntity<ProcessStage>();
        }

        public ProcessStage GetChangedEntity()
        {
            if (_trackChanges)
            {
                var attr = new AttributeCollection();
                foreach (var attrName in _changedProperties.Value.Select(changedProperty => ((AttributeLogicalNameAttribute) GetType().GetProperty(changedProperty)!.GetCustomAttribute(typeof (AttributeLogicalNameAttribute))!).LogicalName).Where(attrName => Contains(attrName)))
                {
                    attr.Add(attrName,this[attrName]);
                }
                return new  ProcessStage(Id) {Attributes = attr };
            }
            return this;
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
