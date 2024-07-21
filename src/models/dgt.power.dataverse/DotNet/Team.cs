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
	/// Collection of system users that routinely collaborate. Teams can be used to simplify record sharing and provide team members with common access to organization data when team members belong to different Business Units.
	/// </summary>
	[DataContractAttribute()]
	[EntityLogicalNameAttribute("team")]
	[System.CodeDom.Compiler.GeneratedCode("dgtp", "2023")]
    [ExcludeFromCodeCoverage]
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
		public Team(Guid id, bool trackChanges = false) : base(EntityLogicalName,id)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public Team(KeyAttributeCollection keyAttributes, bool trackChanges = false) : base(EntityLogicalName,keyAttributes)
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
        private readonly Lazy<HashSet<string>> _changedProperties = new Lazy<HashSet<string>>();
        #endregion

        #region consts
        public const string EntityLogicalName = "team";
        public const string PrimaryNameAttribute = "name";
        public const int EntityTypeCode = 9;
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
		[AttributeLogicalNameAttribute("teamid")]
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
                OnPropertyChanging(nameof(TeamId));
                SetAttributeValue("teamid", value);
				if (value.HasValue)
				{
					base.Id = value.Value;
				}
				else
				{
					base.Id = System.Guid.Empty;
				}
                OnPropertyChanged(nameof(TeamId));
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
                OnPropertyChanging(nameof(AdministratorId));
                SetAttributeValue("administratorid", value);
                OnPropertyChanged(nameof(AdministratorId));
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
                OnPropertyChanging(nameof(AzureActiveDirectoryObjectId));
                SetAttributeValue("azureactivedirectoryobjectid", value);
                OnPropertyChanged(nameof(AzureActiveDirectoryObjectId));
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
                OnPropertyChanging(nameof(BusinessUnitId));
                SetAttributeValue("businessunitid", value);
                OnPropertyChanged(nameof(BusinessUnitId));
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
                OnPropertyChanging(nameof(DelegatedAuthorizationId));
                SetAttributeValue("delegatedauthorizationid", value);
                OnPropertyChanged(nameof(DelegatedAuthorizationId));
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
                OnPropertyChanging(nameof(Description));
                SetAttributeValue("description", value);
                OnPropertyChanged(nameof(Description));
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
                OnPropertyChanging(nameof(EMailAddress));
                SetAttributeValue("emailaddress", value);
                OnPropertyChanged(nameof(EMailAddress));
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
                OnPropertyChanging(nameof(ImportSequenceNumber));
                SetAttributeValue("importsequencenumber", value);
                OnPropertyChanged(nameof(ImportSequenceNumber));
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
                OnPropertyChanging(nameof(MembershipType));
                SetAttributeValue("membershiptype", value);
                OnPropertyChanged(nameof(MembershipType));
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
                OnPropertyChanging(nameof(Name));
                SetAttributeValue("name", value);
                OnPropertyChanged(nameof(Name));
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
                OnPropertyChanging(nameof(OverriddenCreatedOn));
                SetAttributeValue("overriddencreatedon", value);
                OnPropertyChanged(nameof(OverriddenCreatedOn));
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
                OnPropertyChanging(nameof(ProcessId));
                SetAttributeValue("processid", value);
                OnPropertyChanged(nameof(ProcessId));
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
                OnPropertyChanging(nameof(QueueId));
                SetAttributeValue("queueid", value);
                OnPropertyChanged(nameof(QueueId));
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
                OnPropertyChanging(nameof(RegardingObjectId));
                SetAttributeValue("regardingobjectid", value);
                OnPropertyChanged(nameof(RegardingObjectId));
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
                OnPropertyChanging(nameof(StageId));
                SetAttributeValue("stageid", value);
                OnPropertyChanged(nameof(StageId));
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
                OnPropertyChanging(nameof(TeamTemplateId));
                SetAttributeValue("teamtemplateid", value);
                OnPropertyChanged(nameof(TeamTemplateId));
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
                OnPropertyChanging(nameof(TeamType));
                SetAttributeValue("teamtype", value);
                OnPropertyChanged(nameof(TeamType));
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
                OnPropertyChanging(nameof(TransactionCurrencyId));
                SetAttributeValue("transactioncurrencyid", value);
                OnPropertyChanged(nameof(TransactionCurrencyId));
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
                OnPropertyChanging(nameof(TraversedPath));
                SetAttributeValue("traversedpath", value);
                OnPropertyChanged(nameof(TraversedPath));
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
                OnPropertyChanging(nameof(YomiName));
                SetAttributeValue("yominame", value);
                OnPropertyChanged(nameof(YomiName));
            }
        }


		#endregion

		#region NavigationProperties
		/// <summary>
		/// 1:N team_accounts
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("team_accounts")]
		public System.Collections.Generic.IEnumerable<Account> TeamAccounts
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Account>("team_accounts", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("TeamAccounts");
				this.SetRelatedEntities<Account>("team_accounts", null, value);
				this.OnPropertyChanged("TeamAccounts");
			}
		}

		/// <summary>
		/// 1:N team_asyncoperation
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("team_asyncoperation")]
		public System.Collections.Generic.IEnumerable<AsyncOperation> TeamAsyncoperation
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<AsyncOperation>("team_asyncoperation", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("TeamAsyncoperation");
				this.SetRelatedEntities<AsyncOperation>("team_asyncoperation", null, value);
				this.OnPropertyChanged("TeamAsyncoperation");
			}
		}

		/// <summary>
		/// 1:N Team_AsyncOperations
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("Team_AsyncOperations")]
		public System.Collections.Generic.IEnumerable<AsyncOperation> TeamAsyncOperations
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<AsyncOperation>("Team_AsyncOperations", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("TeamAsyncOperations");
				this.SetRelatedEntities<AsyncOperation>("Team_AsyncOperations", null, value);
				this.OnPropertyChanged("TeamAsyncOperations");
			}
		}

		/// <summary>
		/// 1:N team_contacts
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("team_contacts")]
		public System.Collections.Generic.IEnumerable<Contact> TeamContacts
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Contact>("team_contacts", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("TeamContacts");
				this.SetRelatedEntities<Contact>("team_contacts", null, value);
				this.OnPropertyChanged("TeamContacts");
			}
		}

		/// <summary>
		/// 1:N team_customapi
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("team_customapi")]
		public System.Collections.Generic.IEnumerable<CustomAPI> TeamCustomapi
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<CustomAPI>("team_customapi", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("TeamCustomapi");
				this.SetRelatedEntities<CustomAPI>("team_customapi", null, value);
				this.OnPropertyChanged("TeamCustomapi");
			}
		}

		/// <summary>
		/// 1:N team_DuplicateRules
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("team_DuplicateRules")]
		public System.Collections.Generic.IEnumerable<DuplicateRule> TeamDuplicateRules
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<DuplicateRule>("team_DuplicateRules", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("TeamDuplicateRules");
				this.SetRelatedEntities<DuplicateRule>("team_DuplicateRules", null, value);
				this.OnPropertyChanged("TeamDuplicateRules");
			}
		}

		/// <summary>
		/// 1:N team_environmentvariabledefinition
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("team_environmentvariabledefinition")]
		public System.Collections.Generic.IEnumerable<EnvironmentVariableDefinition> TeamEnvironmentvariabledefinition
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<EnvironmentVariableDefinition>("team_environmentvariabledefinition", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("TeamEnvironmentvariabledefinition");
				this.SetRelatedEntities<EnvironmentVariableDefinition>("team_environmentvariabledefinition", null, value);
				this.OnPropertyChanged("TeamEnvironmentvariabledefinition");
			}
		}

		/// <summary>
		/// 1:N team_environmentvariablevalue
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("team_environmentvariablevalue")]
		public System.Collections.Generic.IEnumerable<EnvironmentVariableValue> TeamEnvironmentvariablevalue
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<EnvironmentVariableValue>("team_environmentvariablevalue", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("TeamEnvironmentvariablevalue");
				this.SetRelatedEntities<EnvironmentVariableValue>("team_environmentvariablevalue", null, value);
				this.OnPropertyChanged("TeamEnvironmentvariablevalue");
			}
		}

		/// <summary>
		/// 1:N team_routingrule
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("team_routingrule")]
		public System.Collections.Generic.IEnumerable<RoutingRule> TeamRoutingrule
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<RoutingRule>("team_routingrule", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("TeamRoutingrule");
				this.SetRelatedEntities<RoutingRule>("team_routingrule", null, value);
				this.OnPropertyChanged("TeamRoutingrule");
			}
		}

		/// <summary>
		/// 1:N team_routingruleitem
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("team_routingruleitem")]
		public System.Collections.Generic.IEnumerable<RoutingRuleItem> TeamRoutingruleitem
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<RoutingRuleItem>("team_routingruleitem", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("TeamRoutingruleitem");
				this.SetRelatedEntities<RoutingRuleItem>("team_routingruleitem", null, value);
				this.OnPropertyChanged("TeamRoutingruleitem");
			}
		}

		/// <summary>
		/// 1:N team_slaBase
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("team_slaBase")]
		public System.Collections.Generic.IEnumerable<SLA> TeamSlaBase
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<SLA>("team_slaBase", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("TeamSlaBase");
				this.SetRelatedEntities<SLA>("team_slaBase", null, value);
				this.OnPropertyChanged("TeamSlaBase");
			}
		}

		/// <summary>
		/// 1:N team_workflow
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("team_workflow")]
		public System.Collections.Generic.IEnumerable<Workflow> TeamWorkflow
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Workflow>("team_workflow", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("TeamWorkflow");
				this.SetRelatedEntities<Workflow>("team_workflow", null, value);
				this.OnPropertyChanged("TeamWorkflow");
			}
		}

		#endregion

		#region Options
		public static class Options
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
		public static class LogicalNames
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
		public static class AlternateKeys
		{
				public const string ObjectIdWithMembershipType = "aadobjectid_membershiptype";
		}
		#endregion

		#region Relations
        public static class Relations
        {
            public static class OneToMany
            {
				public const string AdxInviteredemptionTeamOwningteam = "adx_inviteredemption_team_owningteam";
				public const string AdxPortalcommentTeamOwningteam = "adx_portalcomment_team_owningteam";
				public const string ChatTeamOwningteam = "chat_team_owningteam";
				public const string ImportFileTeam = "ImportFile_Team";
				public const string LeadOwningTeam = "lead_owning_team";
				public const string MsdynApprovalTeamOwningteam = "msdyn_approval_team_owningteam";
				public const string MsdynBookingalertTeamOwningteam = "msdyn_bookingalert_team_owningteam";
				public const string MsdynCopilottranscriptTeamOwningteam = "msdyn_copilottranscript_team_owningteam";
				public const string MsdynOcliveworkitemTeamOwningteam = "msdyn_ocliveworkitem_team_owningteam";
				public const string MsdynOcoutboundmessageTeamOwningteam = "msdyn_ocoutboundmessage_team_owningteam";
				public const string MsdynOcsessionTeamOwningteam = "msdyn_ocsession_team_owningteam";
				public const string MsdynTeamMsdynProjectProjectteamid = "msdyn_team_msdyn_project_projectteamid";
				public const string MsdynTeamMsdynSalesroutingrunOwnerassigned = "msdyn_team_msdyn_salesroutingrun_ownerassigned";
				public const string MsdynTeamMsdynSalesroutingrunPreviousowner = "msdyn_team_msdyn_salesroutingrun_previousowner";
				public const string MsfpAlertTeamOwningteam = "msfp_alert_team_owningteam";
				public const string MsfpSurveyinviteTeamOwningteam = "msfp_surveyinvite_team_owningteam";
				public const string MsfpSurveyresponseTeamOwningteam = "msfp_surveyresponse_team_owningteam";
				public const string OwningTeamPostfollows = "OwningTeam_postfollows";
				public const string TeamAccounts = "team_accounts";
				public const string TeamActioncardusersettings = "team_actioncardusersettings";
				public const string TeamActivity = "team_activity";
				public const string TeamActivityfileattachment = "team_activityfileattachment";
				public const string TeamActivitymonitor = "team_activitymonitor";
				public const string TeamAdminsettingsentity = "team_adminsettingsentity";
				public const string TeamAdxInvitation = "team_adx_invitation";
				public const string TeamAdxSetting = "team_adx_setting";
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
				public const string TeamAnnotations = "team_annotations";
				public const string TeamAppnotification = "team_appnotification";
				public const string TeamAppointment = "team_appointment";
				public const string TeamArchivecleanupinfo = "team_archivecleanupinfo";
				public const string TeamArchivecleanupoperation = "team_archivecleanupoperation";
				public const string TeamAsyncoperation = "team_asyncoperation";
				public const string TeamAsyncOperations = "Team_AsyncOperations";
				public const string TeamBookableresource = "team_bookableresource";
				public const string TeamBookableresourcebooking = "team_bookableresourcebooking";
				public const string TeamBookableresourcebookingexchangesyncidmapping = "team_bookableresourcebookingexchangesyncidmapping";
				public const string TeamBookableresourcebookingheader = "team_bookableresourcebookingheader";
				public const string TeamBookableresourcecategory = "team_bookableresourcecategory";
				public const string TeamBookableresourcecategoryassn = "team_bookableresourcecategoryassn";
				public const string TeamBookableresourcecharacteristic = "team_bookableresourcecharacteristic";
				public const string TeamBookableresourcegroup = "team_bookableresourcegroup";
				public const string TeamBookingstatus = "team_bookingstatus";
				public const string TeamBot = "team_bot";
				public const string TeamBotcomponent = "team_botcomponent";
				public const string TeamBotcomponentcollection = "team_botcomponentcollection";
				public const string TeamBulkarchiveconfig = "team_bulkarchiveconfig";
				public const string TeamBulkarchivefailuredetail = "team_bulkarchivefailuredetail";
				public const string TeamBulkarchiveoperation = "team_bulkarchiveoperation";
				public const string TeamBulkDeleteFailures = "Team_BulkDeleteFailures";
				public const string TeamBulkOperation = "team_BulkOperation";
				public const string TeamBulkoperationlog = "team_bulkoperationlog";
				public const string TeamCampaignactivity = "team_campaignactivity";
				public const string TeamCampaignresponse = "team_campaignresponse";
				public const string TeamCampaigns = "team_Campaigns";
				public const string TeamCanvasappextendedmetadata = "team_canvasappextendedmetadata";
				public const string TeamCard = "team_card";
				public const string TeamChannelaccessprofile = "team_channelaccessprofile";
				public const string TeamCharacteristic = "team_characteristic";
				public const string TeamComment = "team_comment";
				public const string TeamComponentversion = "team_componentversion";
				public const string TeamConnectioninstance = "team_connectioninstance";
				public const string TeamConnectionreference = "team_connectionreference";
				public const string TeamConnections1 = "team_connections1";
				public const string TeamConnections2 = "team_connections2";
				public const string TeamConnector = "team_connector";
				public const string TeamContacts = "team_contacts";
				public const string TeamContractdetail = "team_contractdetail";
				public const string TeamConversationtranscript = "team_conversationtranscript";
				public const string TeamConvertrule = "team_convertrule";
				public const string TeamCopilotglossaryterm = "team_copilotglossaryterm";
				public const string TeamCopilotsynonyms = "team_copilotsynonyms";
				public const string TeamCr072Booking = "team_cr072_booking";
				public const string TeamCredential = "team_credential";
				public const string TeamCustomapi = "team_customapi";
				public const string TeamCustomerOpportunityRoles = "team_customer_opportunity_roles";
				public const string TeamCustomerRelationship = "team_customer_relationship";
				public const string TeamDatalakefolder = "team_datalakefolder";
				public const string TeamDesktopflowbinary = "team_desktopflowbinary";
				public const string TeamDesktopflowmodule = "team_desktopflowmodule";
				public const string TeamDgtWorkbench = "team_dgt_workbench";
				public const string TeamDgtWorkbenchHistory = "team_dgt_workbench_history";
				public const string TeamDuplicateBaseRecord = "Team_DuplicateBaseRecord";
				public const string TeamDuplicateMatchingRecord = "Team_DuplicateMatchingRecord";
				public const string TeamDuplicateRules = "team_DuplicateRules";
				public const string TeamDvfilesearch = "team_dvfilesearch";
				public const string TeamDvfilesearchattribute = "team_dvfilesearchattribute";
				public const string TeamDvfilesearchentity = "team_dvfilesearchentity";
				public const string TeamDvtablesearch = "team_dvtablesearch";
				public const string TeamDvtablesearchattribute = "team_dvtablesearchattribute";
				public const string TeamDvtablesearchentity = "team_dvtablesearchentity";
				public const string TeamDynamicPropertyInstance = "team_DynamicPropertyInstance";
				public const string TeamEmail = "team_email";
				public const string TeamEmailTemplates = "team_email_templates";
				public const string TeamEmailserverprofile = "team_emailserverprofile";
				public const string TeamEnablearchivalrequest = "team_enablearchivalrequest";
				public const string TeamEntitlement = "team_entitlement";
				public const string TeamEntitlementchannel = "team_entitlementchannel";
				public const string TeamEntitlemententityallocationtypemapping = "team_entitlemententityallocationtypemapping";
				public const string TeamEnvironmentvariabledefinition = "team_environmentvariabledefinition";
				public const string TeamEnvironmentvariablevalue = "team_environmentvariablevalue";
				public const string TeamExchangesyncidmapping = "team_exchangesyncidmapping";
				public const string TeamExportedexcel = "team_exportedexcel";
				public const string TeamExportsolutionupload = "team_exportsolutionupload";
				public const string TeamExternalparty = "team_externalparty";
				public const string TeamFabricaiskill = "team_fabricaiskill";
				public const string TeamFax = "team_fax";
				public const string TeamFeaturecontrolsetting = "team_featurecontrolsetting";
				public const string TeamFederatedknowledgeconfiguration = "team_federatedknowledgeconfiguration";
				public const string TeamFederatedknowledgeentityconfiguration = "team_federatedknowledgeentityconfiguration";
				public const string TeamFlowcapacityassignment = "team_flowcapacityassignment";
				public const string TeamFlowcredentialapplication = "team_flowcredentialapplication";
				public const string TeamFlowevent = "team_flowevent";
				public const string TeamFlowmachine = "team_flowmachine";
				public const string TeamFlowmachinegroup = "team_flowmachinegroup";
				public const string TeamFlowmachineimage = "team_flowmachineimage";
				public const string TeamFlowmachineimageversion = "team_flowmachineimageversion";
				public const string TeamFlowmachinenetwork = "team_flowmachinenetwork";
				public const string TeamFlowrun = "team_flowrun";
				public const string TeamFlowsession = "team_flowsession";
				public const string TeamFxexpression = "team_fxexpression";
				public const string TeamGoal = "team_goal";
				public const string TeamGoalGoalowner = "team_goal_goalowner";
				public const string TeamGoalrollupquery = "team_goalrollupquery";
				public const string TeamImportData = "team_ImportData";
				public const string TeamImportFiles = "team_ImportFiles";
				public const string TeamImportLogs = "team_ImportLogs";
				public const string TeamImportMaps = "team_ImportMaps";
				public const string TeamImports = "team_Imports";
				public const string TeamIncidentresolution = "team_incidentresolution";
				public const string TeamIncidents = "team_incidents";
				public const string TeamInteractionforemail = "team_interactionforemail";
				public const string TeamInvoicedetail = "team_invoicedetail";
				public const string TeamInvoices = "team_invoices";
				public const string TeamKeyvaultreference = "team_keyvaultreference";
				public const string TeamKnowledgearticle = "team_knowledgearticle";
				public const string TeamKnowledgearticleincident = "team_knowledgearticleincident";
				public const string TeamLetter = "team_letter";
				public const string TeamList = "team_list";
				public const string TeamListoperation = "team_listoperation";
				public const string TeamMailbox = "team_mailbox";
				public const string TeamMailboxtrackingcategory = "team_mailboxtrackingcategory";
				public const string TeamMailboxtrackingfolder = "team_mailboxtrackingfolder";
				public const string TeamManagedidentity = "team_managedidentity";
				public const string TeamMsdynAccountpricelist = "team_msdyn_accountpricelist";
				public const string TeamMsdynActioncardactionstat = "team_msdyn_actioncardactionstat";
				public const string TeamMsdynActioncardregarding = "team_msdyn_actioncardregarding";
				public const string TeamMsdynActioncardrolesetting = "team_msdyn_actioncardrolesetting";
				public const string TeamMsdynActioncardstataggregation = "team_msdyn_actioncardstataggregation";
				public const string TeamMsdynActiveicdextension = "team_msdyn_activeicdextension";
				public const string TeamMsdynActual = "team_msdyn_actual";
				public const string TeamMsdynAdminappstate = "team_msdyn_adminappstate";
				public const string TeamMsdynAgentcapacityprofileunit = "team_msdyn_agentcapacityprofileunit";
				public const string TeamMsdynAgentcapacityupdatehistory = "team_msdyn_agentcapacityupdatehistory";
				public const string TeamMsdynAgentchannelstate = "team_msdyn_agentchannelstate";
				public const string TeamMsdynAgentstatus = "team_msdyn_agentstatus";
				public const string TeamMsdynAgentstatushistory = "team_msdyn_agentstatushistory";
				public const string TeamMsdynAgreement = "team_msdyn_agreement";
				public const string TeamMsdynAgreementbookingdate = "team_msdyn_agreementbookingdate";
				public const string TeamMsdynAgreementbookingincident = "team_msdyn_agreementbookingincident";
				public const string TeamMsdynAgreementbookingproduct = "team_msdyn_agreementbookingproduct";
				public const string TeamMsdynAgreementbookingservice = "team_msdyn_agreementbookingservice";
				public const string TeamMsdynAgreementbookingservicetask = "team_msdyn_agreementbookingservicetask";
				public const string TeamMsdynAgreementbookingsetup = "team_msdyn_agreementbookingsetup";
				public const string TeamMsdynAgreementinvoicedate = "team_msdyn_agreementinvoicedate";
				public const string TeamMsdynAgreementinvoiceproduct = "team_msdyn_agreementinvoiceproduct";
				public const string TeamMsdynAgreementinvoicesetup = "team_msdyn_agreementinvoicesetup";
				public const string TeamMsdynAgreementsubstatus = "team_msdyn_agreementsubstatus";
				public const string TeamMsdynAibdataset = "team_msdyn_aibdataset";
				public const string TeamMsdynAibdatasetfile = "team_msdyn_aibdatasetfile";
				public const string TeamMsdynAibdatasetrecord = "team_msdyn_aibdatasetrecord";
				public const string TeamMsdynAibdatasetscontainer = "team_msdyn_aibdatasetscontainer";
				public const string TeamMsdynAibfeedbackloop = "team_msdyn_aibfeedbackloop";
				public const string TeamMsdynAibfile = "team_msdyn_aibfile";
				public const string TeamMsdynAibfileattacheddata = "team_msdyn_aibfileattacheddata";
				public const string TeamMsdynAicontactsuggestion = "team_msdyn_aicontactsuggestion";
				public const string TeamMsdynAievent = "team_msdyn_aievent";
				public const string TeamMsdynAifptrainingdocument = "team_msdyn_aifptrainingdocument";
				public const string TeamMsdynAimodel = "team_msdyn_aimodel";
				public const string TeamMsdynAiodimage = "team_msdyn_aiodimage";
				public const string TeamMsdynAiodlabel = "team_msdyn_aiodlabel";
				public const string TeamMsdynAiodtrainingboundingbox = "team_msdyn_aiodtrainingboundingbox";
				public const string TeamMsdynAiodtrainingimage = "team_msdyn_aiodtrainingimage";
				public const string TeamMsdynAitemplate = "team_msdyn_aitemplate";
				public const string TeamMsdynAnalysiscomponent = "team_msdyn_analysiscomponent";
				public const string TeamMsdynAnalysisjob = "team_msdyn_analysisjob";
				public const string TeamMsdynAnalysisoverride = "team_msdyn_analysisoverride";
				public const string TeamMsdynAnalysisresult = "team_msdyn_analysisresult";
				public const string TeamMsdynAnalysisresultdetail = "team_msdyn_analysisresultdetail";
				public const string TeamMsdynAnalytics = "team_msdyn_analytics";
				public const string TeamMsdynAnalyticsadminsettings = "team_msdyn_analyticsadminsettings";
				public const string TeamMsdynAnalyticsforcs = "team_msdyn_analyticsforcs";
				public const string TeamMsdynAppconfiguration = "team_msdyn_appconfiguration";
				public const string TeamMsdynApplicationextension = "team_msdyn_applicationextension";
				public const string TeamMsdynApplicationtabtemplate = "team_msdyn_applicationtabtemplate";
				public const string TeamMsdynAppprofilerolemapping = "team_msdyn_appprofilerolemapping";
				public const string TeamMsdynApprovalset = "team_msdyn_approvalset";
				public const string TeamMsdynAppstate = "team_msdyn_appstate";
				public const string TeamMsdynAssetcategorytemplateassociation = "team_msdyn_assetcategorytemplateassociation";
				public const string TeamMsdynAssettemplateassociation = "team_msdyn_assettemplateassociation";
				public const string TeamMsdynAssignmentconfiguration = "team_msdyn_assignmentconfiguration";
				public const string TeamMsdynAssignmentconfigurationstep = "team_msdyn_assignmentconfigurationstep";
				public const string TeamMsdynAssignmentmap = "team_msdyn_assignmentmap";
				public const string TeamMsdynAssignmentrule = "team_msdyn_assignmentrule";
				public const string TeamMsdynAttribute = "team_msdyn_attribute";
				public const string TeamMsdynAttributevalue = "team_msdyn_attributevalue";
				public const string TeamMsdynAuthenticationsettings = "team_msdyn_authenticationsettings";
				public const string TeamMsdynAuthsettingsentry = "team_msdyn_authsettingsentry";
				public const string TeamMsdynAutocapturerule = "team_msdyn_autocapturerule";
				public const string TeamMsdynAutocapturesettings = "team_msdyn_autocapturesettings";
				public const string TeamMsdynAutonomouscasecreationrule = "team_msdyn_autonomouscasecreationrule";
				public const string TeamMsdynBgjobledger = "team_msdyn_bgjobledger";
				public const string TeamMsdynBookableresourceassociation = "team_msdyn_bookableresourceassociation";
				public const string TeamMsdynBookableresourcebookingquicknote = "team_msdyn_bookableresourcebookingquicknote";
				public const string TeamMsdynBookableresourcecapacityprofile = "team_msdyn_bookableresourcecapacityprofile";
				public const string TeamMsdynBookingalertstatus = "team_msdyn_bookingalertstatus";
				public const string TeamMsdynBookingchange = "team_msdyn_bookingchange";
				public const string TeamMsdynBookingjournal = "team_msdyn_bookingjournal";
				public const string TeamMsdynBookingrule = "team_msdyn_bookingrule";
				public const string TeamMsdynBookingsetupmetadata = "team_msdyn_bookingsetupmetadata";
				public const string TeamMsdynBookingtimestamp = "team_msdyn_bookingtimestamp";
				public const string TeamMsdynBotsession = "team_msdyn_botsession";
				public const string TeamMsdynBusinessclosure = "team_msdyn_businessclosure";
				public const string TeamMsdynCallablecontext = "team_msdyn_callablecontext";
				public const string TeamMsdynCapacityprofile = "team_msdyn_capacityprofile";
				public const string TeamMsdynCdsentityengagementctx = "team_msdyn_cdsentityengagementctx";
				public const string TeamMsdynChannel = "team_msdyn_channel";
				public const string TeamMsdynChanneldefinition = "team_msdyn_channeldefinition";
				public const string TeamMsdynChanneldefinitionconsent = "team_msdyn_channeldefinitionconsent";
				public const string TeamMsdynChanneldefinitionlocale = "team_msdyn_channeldefinitionlocale";
				public const string TeamMsdynChannelinstance = "team_msdyn_channelinstance";
				public const string TeamMsdynChannelinstanceaccount = "team_msdyn_channelinstanceaccount";
				public const string TeamMsdynChannelmessageattachment = "team_msdyn_channelmessageattachment";
				public const string TeamMsdynChannelmessagecontextpart = "team_msdyn_channelmessagecontextpart";
				public const string TeamMsdynChannelmessagepart = "team_msdyn_channelmessagepart";
				public const string TeamMsdynChannelprovider = "team_msdyn_channelprovider";
				public const string TeamMsdynCharacteristicreqforteammember = "team_msdyn_characteristicreqforteammember";
				public const string TeamMsdynChatansweroption = "team_msdyn_chatansweroption";
				public const string TeamMsdynChatquestionnaireresponse = "team_msdyn_chatquestionnaireresponse";
				public const string TeamMsdynChatquestionnaireresponseitem = "team_msdyn_chatquestionnaireresponseitem";
				public const string TeamMsdynChatwidgetlanguage = "team_msdyn_chatwidgetlanguage";
				public const string TeamMsdynClientextension = "team_msdyn_clientextension";
				public const string TeamMsdynConfiguration = "team_msdyn_configuration";
				public const string TeamMsdynConsoleapplicationnotificationfield = "team_msdyn_consoleapplicationnotificationfield";
				public const string TeamMsdynConsoleapplicationnotificationtemplate = "team_msdyn_consoleapplicationnotificationtemplate";
				public const string TeamMsdynConsoleapplicationsessiontemplate = "team_msdyn_consoleapplicationsessiontemplate";
				public const string TeamMsdynConsoleapplicationtemplate = "team_msdyn_consoleapplicationtemplate";
				public const string TeamMsdynConsoleapplicationtemplateparameter = "team_msdyn_consoleapplicationtemplateparameter";
				public const string TeamMsdynConsumingapplication = "team_msdyn_consumingapplication";
				public const string TeamMsdynContactpricelist = "team_msdyn_contactpricelist";
				public const string TeamMsdynContactsuggestionrule = "team_msdyn_contactsuggestionrule";
				public const string TeamMsdynContactsuggestionruleset = "team_msdyn_contactsuggestionruleset";
				public const string TeamMsdynContractlinedetailperformance = "team_msdyn_contractlinedetailperformance";
				public const string TeamMsdynContractlinescheduleofvalue = "team_msdyn_contractlinescheduleofvalue";
				public const string TeamMsdynContractperformance = "team_msdyn_contractperformance";
				public const string TeamMsdynConversationaction = "team_msdyn_conversationaction";
				public const string TeamMsdynConversationactionitem = "team_msdyn_conversationactionitem";
				public const string TeamMsdynConversationactionlocale = "team_msdyn_conversationactionlocale";
				public const string TeamMsdynConversationaggregatedinsights = "team_msdyn_conversationaggregatedinsights";
				public const string TeamMsdynConversationcomment = "team_msdyn_conversationcomment";
				public const string TeamMsdynConversationdata = "team_msdyn_conversationdata";
				public const string TeamMsdynConversationinsight = "team_msdyn_conversationinsight";
				public const string TeamMsdynConversationmessageblock = "team_msdyn_conversationmessageblock";
				public const string TeamMsdynConversationparticipantinsights = "team_msdyn_conversationparticipantinsights";
				public const string TeamMsdynConversationparticipantsentiment = "team_msdyn_conversationparticipantsentiment";
				public const string TeamMsdynConversationquestion = "team_msdyn_conversationquestion";
				public const string TeamMsdynConversationsegmentsentiment = "team_msdyn_conversationsegmentsentiment";
				public const string TeamMsdynConversationsentiment = "team_msdyn_conversationsentiment";
				public const string TeamMsdynConversationsignal = "team_msdyn_conversationsignal";
				public const string TeamMsdynConversationsubject = "team_msdyn_conversationsubject";
				public const string TeamMsdynConversationsummarysuggestion = "team_msdyn_conversationsummarysuggestion";
				public const string TeamMsdynConversationsystemtag = "team_msdyn_conversationsystemtag";
				public const string TeamMsdynConversationtag = "team_msdyn_conversationtag";
				public const string TeamMsdynCopilotagentpreference = "team_msdyn_copilotagentpreference";
				public const string TeamMsdynCopilotinteractiondata = "team_msdyn_copilotinteractiondata";
				public const string TeamMsdynCopilottranscriptdata = "team_msdyn_copilottranscriptdata";
				public const string TeamMsdynCrmconnection = "team_msdyn_crmconnection";
				public const string TeamMsdynCsadminconfig = "team_msdyn_csadminconfig";
				public const string TeamMsdynCustomapirulesetconfiguration = "team_msdyn_customapirulesetconfiguration";
				public const string TeamMsdynCustomcontrolextendedsettings = "team_msdyn_customcontrolextendedsettings";
				public const string TeamMsdynCustomengagementctx = "team_msdyn_customengagementctx";
				public const string TeamMsdynCustomerasset = "team_msdyn_customerasset";
				public const string TeamMsdynCustomerassetattachment = "team_msdyn_customerassetattachment";
				public const string TeamMsdynCustomerassetcategory = "team_msdyn_customerassetcategory";
				public const string TeamMsdynDataanalyticscustomizedreport = "team_msdyn_dataanalyticscustomizedreport";
				public const string TeamMsdynDataanalyticsdataset = "team_msdyn_dataanalyticsdataset";
				public const string TeamMsdynDataanalyticsreport = "team_msdyn_dataanalyticsreport";
				public const string TeamMsdynDataanalyticsworkspace = "team_msdyn_dataanalyticsworkspace";
				public const string TeamMsdynDataexport = "team_msdyn_dataexport";
				public const string TeamMsdynDataflow = "team_msdyn_dataflow";
				public const string TeamMsdynDataflowDatalakefolder = "team_msdyn_dataflow_datalakefolder";
				public const string TeamMsdynDataflowconnectionreference = "team_msdyn_dataflowconnectionreference";
				public const string TeamMsdynDataflowrefreshhistory = "team_msdyn_dataflowrefreshhistory";
				public const string TeamMsdynDataflowtemplate = "team_msdyn_dataflowtemplate";
				public const string TeamMsdynDealmanageraccess = "team_msdyn_dealmanageraccess";
				public const string TeamMsdynDealmanagersettings = "team_msdyn_dealmanagersettings";
				public const string TeamMsdynDecisioncontract = "team_msdyn_decisioncontract";
				public const string TeamMsdynDecisionruleset = "team_msdyn_decisionruleset";
				public const string TeamMsdynDefextendedchannelinstance = "team_msdyn_defextendedchannelinstance";
				public const string TeamMsdynDefextendedchannelinstanceaccount = "team_msdyn_defextendedchannelinstanceaccount";
				public const string TeamMsdynDelegation = "team_msdyn_delegation";
				public const string TeamMsdynDeletedconversation = "team_msdyn_deletedconversation";
				public const string TeamMsdynDimension = "team_msdyn_dimension";
				public const string TeamMsdynDmsrequest = "team_msdyn_dmsrequest";
				public const string TeamMsdynDmsrequeststatus = "team_msdyn_dmsrequeststatus";
				public const string TeamMsdynDmssyncrequest = "team_msdyn_dmssyncrequest";
				public const string TeamMsdynDmssyncstatus = "team_msdyn_dmssyncstatus";
				public const string TeamMsdynDuplicateleadmapping = "team_msdyn_duplicateleadmapping";
				public const string TeamMsdynEffortpredictionresult = "team_msdyn_effortpredictionresult";
				public const string TeamMsdynEntitlementapplication = "team_msdyn_entitlementapplication";
				public const string TeamMsdynEntityattachment = "team_msdyn_entityattachment";
				public const string TeamMsdynEntityconfig = "team_msdyn_entityconfig";
				public const string TeamMsdynEntityconfiguration = "team_msdyn_entityconfiguration";
				public const string TeamMsdynEntitylinkchatconfiguration = "team_msdyn_entitylinkchatconfiguration";
				public const string TeamMsdynEntityrankingrule = "team_msdyn_entityrankingrule";
				public const string TeamMsdynEntityrefreshhistory = "team_msdyn_entityrefreshhistory";
				public const string TeamMsdynEntityroutingconfiguration = "team_msdyn_entityroutingconfiguration";
				public const string TeamMsdynEntityworkstreammap = "team_msdyn_entityworkstreammap";
				public const string TeamMsdynEstimate = "team_msdyn_estimate";
				public const string TeamMsdynEstimateline = "team_msdyn_estimateline";
				public const string TeamMsdynExpense = "team_msdyn_expense";
				public const string TeamMsdynExpensereceipt = "team_msdyn_expensereceipt";
				public const string TeamMsdynExtendedusersetting = "team_msdyn_extendedusersetting";
				public const string TeamMsdynFacebookengagementctx = "team_msdyn_facebookengagementctx";
				public const string TeamMsdynFact = "team_msdyn_fact";
				public const string TeamMsdynFavoriteknowledgearticle = "team_msdyn_favoriteknowledgearticle";
				public const string TeamMsdynFederatedarticle = "team_msdyn_federatedarticle";
				public const string TeamMsdynFieldcomputation = "team_msdyn_fieldcomputation";
				public const string TeamMsdynFieldservicesetting = "team_msdyn_fieldservicesetting";
				public const string TeamMsdynFieldserviceslaconfiguration = "team_msdyn_fieldserviceslaconfiguration";
				public const string TeamMsdynFileupload = "team_msdyn_fileupload";
				public const string TeamMsdynFindworkevent = "team_msdyn_findworkevent";
				public const string TeamMsdynFlowcardtype = "team_msdyn_flowcardtype";
				public const string TeamMsdynForecastconfiguration = "team_msdyn_forecastconfiguration";
				public const string TeamMsdynForecastdefinition = "team_msdyn_forecastdefinition";
				public const string TeamMsdynForecastinstance = "team_msdyn_forecastinstance";
				public const string TeamMsdynForecastrecurrence = "team_msdyn_forecastrecurrence";
				public const string TeamMsdynFormmapping = "team_msdyn_formmapping";
				public const string TeamMsdynFunctionallocation = "team_msdyn_functionallocation";
				public const string TeamMsdynFunctionallocationtype = "team_msdyn_functionallocationtype";
				public const string TeamMsdynGdprdata = "team_msdyn_gdprdata";
				public const string TeamMsdynGeofence = "team_msdyn_geofence";
				public const string TeamMsdynGeofenceevent = "team_msdyn_geofenceevent";
				public const string TeamMsdynGeofencingsettings = "team_msdyn_geofencingsettings";
				public const string TeamMsdynIcdextension = "team_msdyn_icdextension";
				public const string TeamMsdynIcebreakersconfig = "team_msdyn_icebreakersconfig";
				public const string TeamMsdynIermlmodel = "team_msdyn_iermlmodel";
				public const string TeamMsdynIermltraining = "team_msdyn_iermltraining";
				public const string TeamMsdynIncidenttype = "team_msdyn_incidenttype";
				public const string TeamMsdynIncidenttypeRequirementgroup = "team_msdyn_incidenttype_requirementgroup";
				public const string TeamMsdynIncidenttypecharacteristic = "team_msdyn_incidenttypecharacteristic";
				public const string TeamMsdynIncidenttypeproduct = "team_msdyn_incidenttypeproduct";
				public const string TeamMsdynIncidenttyperecommendationresult = "team_msdyn_incidenttyperecommendationresult";
				public const string TeamMsdynIncidenttyperecommendationrunhistory = "team_msdyn_incidenttyperecommendationrunhistory";
				public const string TeamMsdynIncidenttyperesolution = "team_msdyn_incidenttyperesolution";
				public const string TeamMsdynIncidenttypeservice = "team_msdyn_incidenttypeservice";
				public const string TeamMsdynIncidenttypeservicetask = "team_msdyn_incidenttypeservicetask";
				public const string TeamMsdynIncidenttypessetup = "team_msdyn_incidenttypessetup";
				public const string TeamMsdynInspection = "team_msdyn_inspection";
				public const string TeamMsdynInspectionattachment = "team_msdyn_inspectionattachment";
				public const string TeamMsdynInspectiondefinition = "team_msdyn_inspectiondefinition";
				public const string TeamMsdynInspectioninstance = "team_msdyn_inspectioninstance";
				public const string TeamMsdynInspectionresponse = "team_msdyn_inspectionresponse";
				public const string TeamMsdynInsurance = "team_msdyn_insurance";
				public const string TeamMsdynIntegratedsearchprovider = "team_msdyn_integratedsearchprovider";
				public const string TeamMsdynIntegrationjob = "team_msdyn_integrationjob";
				public const string TeamMsdynIntegrationjobdetail = "team_msdyn_integrationjobdetail";
				public const string TeamMsdynIntent = "team_msdyn_intent";
				public const string TeamMsdynInventoryadjustment = "team_msdyn_inventoryadjustment";
				public const string TeamMsdynInventoryadjustmentproduct = "team_msdyn_inventoryadjustmentproduct";
				public const string TeamMsdynInventoryjournal = "team_msdyn_inventoryjournal";
				public const string TeamMsdynInventorytransfer = "team_msdyn_inventorytransfer";
				public const string TeamMsdynInvoicelinetransaction = "team_msdyn_invoicelinetransaction";
				public const string TeamMsdynIotalert = "team_msdyn_iotalert";
				public const string TeamMsdynIotdevice = "team_msdyn_iotdevice";
				public const string TeamMsdynIotdevicecategory = "team_msdyn_iotdevicecategory";
				public const string TeamMsdynIotdevicecommand = "team_msdyn_iotdevicecommand";
				public const string TeamMsdynIotdevicecommanddefinition = "team_msdyn_iotdevicecommanddefinition";
				public const string TeamMsdynIotdevicedatahistory = "team_msdyn_iotdevicedatahistory";
				public const string TeamMsdynIotdeviceproperty = "team_msdyn_iotdeviceproperty";
				public const string TeamMsdynIotdeviceregistrationhistory = "team_msdyn_iotdeviceregistrationhistory";
				public const string TeamMsdynIotdevicevisualizationconfiguration = "team_msdyn_iotdevicevisualizationconfiguration";
				public const string TeamMsdynIotfieldmapping = "team_msdyn_iotfieldmapping";
				public const string TeamMsdynIotpropertydefinition = "team_msdyn_iotpropertydefinition";
				public const string TeamMsdynIotprovider = "team_msdyn_iotprovider";
				public const string TeamMsdynIotproviderinstance = "team_msdyn_iotproviderinstance";
				public const string TeamMsdynIotsettings = "team_msdyn_iotsettings";
				public const string TeamMsdynJobsstate = "team_msdyn_jobsstate";
				public const string TeamMsdynJournal = "team_msdyn_journal";
				public const string TeamMsdynJournalline = "team_msdyn_journalline";
				public const string TeamMsdynKalanguagesetting = "team_msdyn_kalanguagesetting";
				public const string TeamMsdynKbattachment = "team_msdyn_kbattachment";
				public const string TeamMsdynKmfederatedsearchconfig = "team_msdyn_kmfederatedsearchconfig";
				public const string TeamMsdynKnowledgearticleimage = "team_msdyn_knowledgearticleimage";
				public const string TeamMsdynKnowledgearticletemplate = "team_msdyn_knowledgearticletemplate";
				public const string TeamMsdynKnowledgeassetconfiguration = "team_msdyn_knowledgeassetconfiguration";
				public const string TeamMsdynKnowledgeinteractioninsight = "team_msdyn_knowledgeinteractioninsight";
				public const string TeamMsdynKnowledgemanagementsetting = "team_msdyn_knowledgemanagementsetting";
				public const string TeamMsdynKnowledgepersonalfilter = "team_msdyn_knowledgepersonalfilter";
				public const string TeamMsdynKnowledgesearchfilter = "team_msdyn_knowledgesearchfilter";
				public const string TeamMsdynKnowledgesearchinsight = "team_msdyn_knowledgesearchinsight";
				public const string TeamMsdynKpieventdata = "team_msdyn_kpieventdata";
				public const string TeamMsdynKpieventdefinition = "team_msdyn_kpieventdefinition";
				public const string TeamMsdynLeadmodelconfig = "team_msdyn_leadmodelconfig";
				public const string TeamMsdynLineengagementctx = "team_msdyn_lineengagementctx";
				public const string TeamMsdynLivechatconfig = "team_msdyn_livechatconfig";
				public const string TeamMsdynLivechatengagementctx = "team_msdyn_livechatengagementctx";
				public const string TeamMsdynLivechatwidgetlocation = "team_msdyn_livechatwidgetlocation";
				public const string TeamMsdynLiveconversation = "team_msdyn_liveconversation";
				public const string TeamMsdynLiveworkitemevent = "team_msdyn_liveworkitemevent";
				public const string TeamMsdynLiveworkstream = "team_msdyn_liveworkstream";
				public const string TeamMsdynLiveworkstreamcapacityprofile = "team_msdyn_liveworkstreamcapacityprofile";
				public const string TeamMsdynLocationtemplateassociation = "team_msdyn_locationtemplateassociation";
				public const string TeamMsdynLocationtypetemplateassociation = "team_msdyn_locationtypetemplateassociation";
				public const string TeamMsdynLockstatus = "team_msdyn_lockstatus";
				public const string TeamMsdynMacrosession = "team_msdyn_macrosession";
				public const string TeamMsdynMasterentityroutingconfiguration = "team_msdyn_masterentityroutingconfiguration";
				public const string TeamMsdynMigrationtracker = "team_msdyn_migrationtracker";
				public const string TeamMsdynMobileapp = "team_msdyn_mobileapp";
				public const string TeamMsdynMobilesource = "team_msdyn_mobilesource";
				public const string TeamMsdynModelpreviewstatus = "team_msdyn_modelpreviewstatus";
				public const string TeamMsdynMsteamssetting = "team_msdyn_msteamssetting";
				public const string TeamMsdynNotesanalysisconfig = "team_msdyn_notesanalysisconfig";
				public const string TeamMsdynNotificationfield = "team_msdyn_notificationfield";
				public const string TeamMsdynNotificationtemplate = "team_msdyn_notificationtemplate";
				public const string TeamMsdynNottoexceed = "team_msdyn_nottoexceed";
				public const string TeamMsdynOcGeolocationprovider = "team_msdyn_oc_geolocationprovider";
				public const string TeamMsdynOcagentassignedcustomapiprivilege = "team_msdyn_ocagentassignedcustomapiprivilege";
				public const string TeamMsdynOcapplebusinessaccount = "team_msdyn_ocapplebusinessaccount";
				public const string TeamMsdynOcapplemessagesforbusinessengagementctx = "team_msdyn_ocapplemessagesforbusinessengagementctx";
				public const string TeamMsdynOcapplepay = "team_msdyn_ocapplepay";
				public const string TeamMsdynOcautoblockrule = "team_msdyn_ocautoblockrule";
				public const string TeamMsdynOcbotchannelregistration = "team_msdyn_ocbotchannelregistration";
				public const string TeamMsdynOcbotchannelregistrationsecret = "team_msdyn_ocbotchannelregistrationsecret";
				public const string TeamMsdynOccarrier = "team_msdyn_occarrier";
				public const string TeamMsdynOcchannelapiconversationprivilege = "team_msdyn_occhannelapiconversationprivilege";
				public const string TeamMsdynOcchannelapimessageprivilege = "team_msdyn_occhannelapimessageprivilege";
				public const string TeamMsdynOcchannelapimethodmapping = "team_msdyn_occhannelapimethodmapping";
				public const string TeamMsdynOccommunicationprovidersetting = "team_msdyn_occommunicationprovidersetting";
				public const string TeamMsdynOccommunicationprovidersettingentry = "team_msdyn_occommunicationprovidersettingentry";
				public const string TeamMsdynOccustommessagingchannel = "team_msdyn_occustommessagingchannel";
				public const string TeamMsdynOcexternalcontext = "team_msdyn_ocexternalcontext";
				public const string TeamMsdynOcfbapplication = "team_msdyn_ocfbapplication";
				public const string TeamMsdynOcfbpage = "team_msdyn_ocfbpage";
				public const string TeamMsdynOcflaggedspam = "team_msdyn_ocflaggedspam";
				public const string TeamMsdynOcgooglebusinessmessagesagentaccount = "team_msdyn_ocgooglebusinessmessagesagentaccount";
				public const string TeamMsdynOcgooglebusinessmessagesengagementctx = "team_msdyn_ocgooglebusinessmessagesengagementctx";
				public const string TeamMsdynOcgooglebusinessmessagespartneraccount = "team_msdyn_ocgooglebusinessmessagespartneraccount";
				public const string TeamMsdynOclanguage = "team_msdyn_oclanguage";
				public const string TeamMsdynOclinechannelconfig = "team_msdyn_oclinechannelconfig";
				public const string TeamMsdynOcliveworkitemcapacityprofile = "team_msdyn_ocliveworkitemcapacityprofile";
				public const string TeamMsdynOcliveworkitemcharacteristic = "team_msdyn_ocliveworkitemcharacteristic";
				public const string TeamMsdynOcliveworkitemcontextitem = "team_msdyn_ocliveworkitemcontextitem";
				public const string TeamMsdynOcliveworkitemparticipant = "team_msdyn_ocliveworkitemparticipant";
				public const string TeamMsdynOcliveworkitemsentiment = "team_msdyn_ocliveworkitemsentiment";
				public const string TeamMsdynOcliveworkstreamcontextvariable = "team_msdyn_ocliveworkstreamcontextvariable";
				public const string TeamMsdynOcoutboundconfiguration = "team_msdyn_ocoutboundconfiguration";
				public const string TeamMsdynOcpaymentprofile = "team_msdyn_ocpaymentprofile";
				public const string TeamMsdynOcphonenumber = "team_msdyn_ocphonenumber";
				public const string TeamMsdynOcprovisioningstate = "team_msdyn_ocprovisioningstate";
				public const string TeamMsdynOcrecording = "team_msdyn_ocrecording";
				public const string TeamMsdynOcrequest = "team_msdyn_ocrequest";
				public const string TeamMsdynOcrichobject = "team_msdyn_ocrichobject";
				public const string TeamMsdynOcrichobjectmap = "team_msdyn_ocrichobjectmap";
				public const string TeamMsdynOcruleitem = "team_msdyn_ocruleitem";
				public const string TeamMsdynOcsentimentdailytopic = "team_msdyn_ocsentimentdailytopic";
				public const string TeamMsdynOcsentimentdailytopickeyword = "team_msdyn_ocsentimentdailytopickeyword";
				public const string TeamMsdynOcsentimentdailytopictrending = "team_msdyn_ocsentimentdailytopictrending";
				public const string TeamMsdynOcsessioncharacteristic = "team_msdyn_ocsessioncharacteristic";
				public const string TeamMsdynOcsessionparticipantevent = "team_msdyn_ocsessionparticipantevent";
				public const string TeamMsdynOcsessionsentiment = "team_msdyn_ocsessionsentiment";
				public const string TeamMsdynOcsimltraining = "team_msdyn_ocsimltraining";
				public const string TeamMsdynOcsitdimportconfig = "team_msdyn_ocsitdimportconfig";
				public const string TeamMsdynOcsitdskill = "team_msdyn_ocsitdskill";
				public const string TeamMsdynOcsitrainingdata = "team_msdyn_ocsitrainingdata";
				public const string TeamMsdynOcskillidentmlmodel = "team_msdyn_ocskillidentmlmodel";
				public const string TeamMsdynOcsmssettingsecret = "team_msdyn_ocsmssettingsecret";
				public const string TeamMsdynOcteamschannelconfig = "team_msdyn_octeamschannelconfig";
				public const string TeamMsdynOctwitterapplication = "team_msdyn_octwitterapplication";
				public const string TeamMsdynOctwitterhandle = "team_msdyn_octwitterhandle";
				public const string TeamMsdynOctwitterhandleprovisioningstatus = "team_msdyn_octwitterhandleprovisioningstatus";
				public const string TeamMsdynOctwitterhandlesecret = "team_msdyn_octwitterhandlesecret";
				public const string TeamMsdynOcwechatchannelconfig = "team_msdyn_ocwechatchannelconfig";
				public const string TeamMsdynOcwhatsappchannelaccount = "team_msdyn_ocwhatsappchannelaccount";
				public const string TeamMsdynOcwhatsappchannelnumber = "team_msdyn_ocwhatsappchannelnumber";
				public const string TeamMsdynOmnichannelpersonalization = "team_msdyn_omnichannelpersonalization";
				public const string TeamMsdynOmnichannelqueue = "team_msdyn_omnichannelqueue";
				public const string TeamMsdynOmnichannelsyncconfig = "team_msdyn_omnichannelsyncconfig";
				public const string TeamMsdynOperatinghour = "team_msdyn_operatinghour";
				public const string TeamMsdynOpportunitylineresourcecategory = "team_msdyn_opportunitylineresourcecategory";
				public const string TeamMsdynOpportunitylinetransaction = "team_msdyn_opportunitylinetransaction";
				public const string TeamMsdynOpportunitylinetransactioncategory = "team_msdyn_opportunitylinetransactioncategory";
				public const string TeamMsdynOpportunitylinetransactionclassificatio = "team_msdyn_opportunitylinetransactionclassificatio";
				public const string TeamMsdynOpportunitymodelconfig = "team_msdyn_opportunitymodelconfig";
				public const string TeamMsdynOpportunitypricelist = "team_msdyn_opportunitypricelist";
				public const string TeamMsdynOrderinvoicingdate = "team_msdyn_orderinvoicingdate";
				public const string TeamMsdynOrderinvoicingproduct = "team_msdyn_orderinvoicingproduct";
				public const string TeamMsdynOrderinvoicingsetup = "team_msdyn_orderinvoicingsetup";
				public const string TeamMsdynOrderinvoicingsetupdate = "team_msdyn_orderinvoicingsetupdate";
				public const string TeamMsdynOrderlineresourcecategory = "team_msdyn_orderlineresourcecategory";
				public const string TeamMsdynOrderlinetransaction = "team_msdyn_orderlinetransaction";
				public const string TeamMsdynOrderlinetransactioncategory = "team_msdyn_orderlinetransactioncategory";
				public const string TeamMsdynOrderlinetransactionclassification = "team_msdyn_orderlinetransactionclassification";
				public const string TeamMsdynOrderpricelist = "team_msdyn_orderpricelist";
				public const string TeamMsdynOrgchartnode = "team_msdyn_orgchartnode";
				public const string TeamMsdynOverflowactionconfig = "team_msdyn_overflowactionconfig";
				public const string TeamMsdynPayment = "team_msdyn_payment";
				public const string TeamMsdynPaymentdetail = "team_msdyn_paymentdetail";
				public const string TeamMsdynPaymentmethod = "team_msdyn_paymentmethod";
				public const string TeamMsdynPaymentterm = "team_msdyn_paymentterm";
				public const string TeamMsdynPersonalmessage = "team_msdyn_personalmessage";
				public const string TeamMsdynPersonalsoundsetting = "team_msdyn_personalsoundsetting";
				public const string TeamMsdynPlaybookactivity = "team_msdyn_playbookactivity";
				public const string TeamMsdynPlaybookactivityattribute = "team_msdyn_playbookactivityattribute";
				public const string TeamMsdynPlaybookcategory = "team_msdyn_playbookcategory";
				public const string TeamMsdynPlaybookinstance = "team_msdyn_playbookinstance";
				public const string TeamMsdynPlaybooktemplate = "team_msdyn_playbooktemplate";
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
				public const string TeamMsdynPmtemplate = "team_msdyn_pmtemplate";
				public const string TeamMsdynPmview = "team_msdyn_pmview";
				public const string TeamMsdynPostalbum = "team_msdyn_postalbum";
				public const string TeamMsdynPostalcode = "team_msdyn_postalcode";
				public const string TeamMsdynPredictioncomputationoperation = "team_msdyn_predictioncomputationoperation";
				public const string TeamMsdynPredictionmodelstatus = "team_msdyn_predictionmodelstatus";
				public const string TeamMsdynPredictionscheduledoperation = "team_msdyn_predictionscheduledoperation";
				public const string TeamMsdynPredictivescoringsyncstatus = "team_msdyn_predictivescoringsyncstatus";
				public const string TeamMsdynPreferredagent = "team_msdyn_preferredagent";
				public const string TeamMsdynPreferredagentcustomeridentity = "team_msdyn_preferredagentcustomeridentity";
				public const string TeamMsdynPreferredagentroutedentity = "team_msdyn_preferredagentroutedentity";
				public const string TeamMsdynPriority = "team_msdyn_priority";
				public const string TeamMsdynProblematicasset = "team_msdyn_problematicasset";
				public const string TeamMsdynProblematicassetfeedback = "team_msdyn_problematicassetfeedback";
				public const string TeamMsdynProductivityactioninputparameter = "team_msdyn_productivityactioninputparameter";
				public const string TeamMsdynProductivityactionoutputparameter = "team_msdyn_productivityactionoutputparameter";
				public const string TeamMsdynProductivityagentscript = "team_msdyn_productivityagentscript";
				public const string TeamMsdynProductivityagentscriptstep = "team_msdyn_productivityagentscriptstep";
				public const string TeamMsdynProductivitymacroactiontemplate = "team_msdyn_productivitymacroactiontemplate";
				public const string TeamMsdynProductivitymacroconnector = "team_msdyn_productivitymacroconnector";
				public const string TeamMsdynProductivitymacrosolutionconfiguration = "team_msdyn_productivitymacrosolutionconfiguration";
				public const string TeamMsdynProductivityparameterdefinition = "team_msdyn_productivityparameterdefinition";
				public const string TeamMsdynProject = "team_msdyn_project";
				public const string TeamMsdynProjectapproval = "team_msdyn_projectapproval";
				public const string TeamMsdynProjectpricelist = "team_msdyn_projectpricelist";
				public const string TeamMsdynProjecttask = "team_msdyn_projecttask";
				public const string TeamMsdynProjecttaskdependency = "team_msdyn_projecttaskdependency";
				public const string TeamMsdynProjecttaskstatususer = "team_msdyn_projecttaskstatususer";
				public const string TeamMsdynProjectteam = "team_msdyn_projectteam";
				public const string TeamMsdynProjecttransactioncategory = "team_msdyn_projecttransactioncategory";
				public const string TeamMsdynProperty = "team_msdyn_property";
				public const string TeamMsdynPropertyassetassociation = "team_msdyn_propertyassetassociation";
				public const string TeamMsdynPropertylocationassociation = "team_msdyn_propertylocationassociation";
				public const string TeamMsdynPropertylog = "team_msdyn_propertylog";
				public const string TeamMsdynPropertytemplateassociation = "team_msdyn_propertytemplateassociation";
				public const string TeamMsdynPurchaseorder = "team_msdyn_purchaseorder";
				public const string TeamMsdynPurchaseorderbill = "team_msdyn_purchaseorderbill";
				public const string TeamMsdynPurchaseorderproduct = "team_msdyn_purchaseorderproduct";
				public const string TeamMsdynPurchaseorderreceipt = "team_msdyn_purchaseorderreceipt";
				public const string TeamMsdynPurchaseorderreceiptproduct = "team_msdyn_purchaseorderreceiptproduct";
				public const string TeamMsdynPurchaseordersubstatus = "team_msdyn_purchaseordersubstatus";
				public const string TeamMsdynQuestionsequence = "team_msdyn_questionsequence";
				public const string TeamMsdynQuotebookingincident = "team_msdyn_quotebookingincident";
				public const string TeamMsdynQuotebookingproduct = "team_msdyn_quotebookingproduct";
				public const string TeamMsdynQuotebookingservice = "team_msdyn_quotebookingservice";
				public const string TeamMsdynQuotebookingservicetask = "team_msdyn_quotebookingservicetask";
				public const string TeamMsdynQuotebookingsetup = "team_msdyn_quotebookingsetup";
				public const string TeamMsdynQuoteinvoicingproduct = "team_msdyn_quoteinvoicingproduct";
				public const string TeamMsdynQuoteinvoicingsetup = "team_msdyn_quoteinvoicingsetup";
				public const string TeamMsdynQuotelineanalyticsbreakdown = "team_msdyn_quotelineanalyticsbreakdown";
				public const string TeamMsdynQuotelineresourcecategory = "team_msdyn_quotelineresourcecategory";
				public const string TeamMsdynQuotelinescheduleofvalue = "team_msdyn_quotelinescheduleofvalue";
				public const string TeamMsdynQuotelinetransaction = "team_msdyn_quotelinetransaction";
				public const string TeamMsdynQuotelinetransactioncategory = "team_msdyn_quotelinetransactioncategory";
				public const string TeamMsdynQuotelinetransactionclassification = "team_msdyn_quotelinetransactionclassification";
				public const string TeamMsdynQuotepricelist = "team_msdyn_quotepricelist";
				public const string TeamMsdynReadtracker = "team_msdyn_readtracker";
				public const string TeamMsdynRealtimescoring = "team_msdyn_realtimescoring";
				public const string TeamMsdynRealtimescoringoperation = "team_msdyn_realtimescoringoperation";
				public const string TeamMsdynRecording = "team_msdyn_recording";
				public const string TeamMsdynRelationshipinsightsunifiedconfig = "team_msdyn_relationshipinsightsunifiedconfig";
				public const string TeamMsdynReportbookmark = "team_msdyn_reportbookmark";
				public const string TeamMsdynRequirementchange = "team_msdyn_requirementchange";
				public const string TeamMsdynRequirementcharacteristic = "team_msdyn_requirementcharacteristic";
				public const string TeamMsdynRequirementdependency = "team_msdyn_requirementdependency";
				public const string TeamMsdynRequirementgroup = "team_msdyn_requirementgroup";
				public const string TeamMsdynRequirementorganizationunit = "team_msdyn_requirementorganizationunit";
				public const string TeamMsdynRequirementrelationship = "team_msdyn_requirementrelationship";
				public const string TeamMsdynRequirementresourcecategory = "team_msdyn_requirementresourcecategory";
				public const string TeamMsdynRequirementresourcepreference = "team_msdyn_requirementresourcepreference";
				public const string TeamMsdynRequirementstatus = "team_msdyn_requirementstatus";
				public const string TeamMsdynResolution = "team_msdyn_resolution";
				public const string TeamMsdynResourceassignment = "team_msdyn_resourceassignment";
				public const string TeamMsdynResourceassignmentdetail = "team_msdyn_resourceassignmentdetail";
				public const string TeamMsdynResourcepaytype = "team_msdyn_resourcepaytype";
				public const string TeamMsdynResourcerequest = "team_msdyn_resourcerequest";
				public const string TeamMsdynResourcerequirement = "team_msdyn_resourcerequirement";
				public const string TeamMsdynResourcerequirementdetail = "team_msdyn_resourcerequirementdetail";
				public const string TeamMsdynResourceterritory = "team_msdyn_resourceterritory";
				public const string TeamMsdynRichtextfile = "team_msdyn_richtextfile";
				public const string TeamMsdynRma = "team_msdyn_rma";
				public const string TeamMsdynRmaproduct = "team_msdyn_rmaproduct";
				public const string TeamMsdynRmareceipt = "team_msdyn_rmareceipt";
				public const string TeamMsdynRmareceiptproduct = "team_msdyn_rmareceiptproduct";
				public const string TeamMsdynRmasubstatus = "team_msdyn_rmasubstatus";
				public const string TeamMsdynRolecompetencyrequirement = "team_msdyn_rolecompetencyrequirement";
				public const string TeamMsdynRoleutilization = "team_msdyn_roleutilization";
				public const string TeamMsdynRoutingconfiguration = "team_msdyn_routingconfiguration";
				public const string TeamMsdynRoutingconfigurationstep = "team_msdyn_routingconfigurationstep";
				public const string TeamMsdynRoutingrequest = "team_msdyn_routingrequest";
				public const string TeamMsdynRtv = "team_msdyn_rtv";
				public const string TeamMsdynRtvproduct = "team_msdyn_rtvproduct";
				public const string TeamMsdynRtvsubstatus = "team_msdyn_rtvsubstatus";
				public const string TeamMsdynRulesetdependencymapping = "team_msdyn_rulesetdependencymapping";
				public const string TeamMsdynSalescopilotinsight = "team_msdyn_salescopilotinsight";
				public const string TeamMsdynSalesforcestructuredobject = "team_msdyn_salesforcestructuredobject";
				public const string TeamMsdynSalesforcestructuredqnaconfig = "team_msdyn_salesforcestructuredqnaconfig";
				public const string TeamMsdynSalesinsightssettings = "team_msdyn_salesinsightssettings";
				public const string TeamMsdynSalesocmessage = "team_msdyn_salesocmessage";
				public const string TeamMsdynSalesocsmstemplate = "team_msdyn_salesocsmstemplate";
				public const string TeamMsdynSalesroutingrun = "team_msdyn_salesroutingrun";
				public const string TeamMsdynSalessuggestion = "team_msdyn_salessuggestion";
				public const string TeamMsdynSalestag = "team_msdyn_salestag";
				public const string TeamMsdynScenario = "team_msdyn_scenario";
				public const string TeamMsdynSchedule = "team_msdyn_schedule";
				public const string TeamMsdynScheduleboardsetting = "team_msdyn_scheduleboardsetting";
				public const string TeamMsdynSchedulingfeatureflag = "team_msdyn_schedulingfeatureflag";
				public const string TeamMsdynSciconversation = "team_msdyn_sciconversation";
				public const string TeamMsdynScicustomemailhighlight = "team_msdyn_scicustomemailhighlight";
				public const string TeamMsdynScicustomhighlight = "team_msdyn_scicustomhighlight";
				public const string TeamMsdynScicustompublisher = "team_msdyn_scicustompublisher";
				public const string TeamMsdynSciusersettings = "team_msdyn_sciusersettings";
				public const string TeamMsdynSearchconfiguration = "team_msdyn_searchconfiguration";
				public const string TeamMsdynSegment = "team_msdyn_segment";
				public const string TeamMsdynSequence = "team_msdyn_sequence";
				public const string TeamMsdynSequencestat = "team_msdyn_sequencestat";
				public const string TeamMsdynSequencetarget = "team_msdyn_sequencetarget";
				public const string TeamMsdynSequencetargetstep = "team_msdyn_sequencetargetstep";
				public const string TeamMsdynSequencetemplate = "team_msdyn_sequencetemplate";
				public const string TeamMsdynServiceconfiguration = "team_msdyn_serviceconfiguration";
				public const string TeamMsdynServiceoneprovisioningrequest = "team_msdyn_serviceoneprovisioningrequest";
				public const string TeamMsdynServicetasktype = "team_msdyn_servicetasktype";
				public const string TeamMsdynSessiondata = "team_msdyn_sessiondata";
				public const string TeamMsdynSessionevent = "team_msdyn_sessionevent";
				public const string TeamMsdynSessionparticipant = "team_msdyn_sessionparticipant";
				public const string TeamMsdynSessionparticipantdata = "team_msdyn_sessionparticipantdata";
				public const string TeamMsdynSessiontemplate = "team_msdyn_sessiontemplate";
				public const string TeamMsdynShipvia = "team_msdyn_shipvia";
				public const string TeamMsdynSiconfig = "team_msdyn_siconfig";
				public const string TeamMsdynSkillattachmentruleitem = "team_msdyn_skillattachmentruleitem";
				public const string TeamMsdynSkillattachmenttarget = "team_msdyn_skillattachmenttarget";
				public const string TeamMsdynSlakpi = "team_msdyn_slakpi";
				public const string TeamMsdynSmsengagementctx = "team_msdyn_smsengagementctx";
				public const string TeamMsdynSmsnumber = "team_msdyn_smsnumber";
				public const string TeamMsdynSolutionhealthrule = "team_msdyn_solutionhealthrule";
				public const string TeamMsdynSolutionhealthruleargument = "team_msdyn_solutionhealthruleargument";
				public const string TeamMsdynSoundnotificationsetting = "team_msdyn_soundnotificationsetting";
				public const string TeamMsdynSubmodeldefinition = "team_msdyn_submodeldefinition";
				public const string TeamMsdynSuggestionassignmentrule = "team_msdyn_suggestionassignmentrule";
				public const string TeamMsdynSuggestionprincipalobjectaccess = "team_msdyn_suggestionprincipalobjectaccess";
				public const string TeamMsdynSuggestionsellerpriority = "team_msdyn_suggestionsellerpriority";
				public const string TeamMsdynSurveyquestion = "team_msdyn_surveyquestion";
				public const string TeamMsdynSwarm = "team_msdyn_swarm";
				public const string TeamMsdynSwarmparticipant = "team_msdyn_swarmparticipant";
				public const string TeamMsdynSwarmparticipantrule = "team_msdyn_swarmparticipantrule";
				public const string TeamMsdynSwarmrole = "team_msdyn_swarmrole";
				public const string TeamMsdynSwarmskill = "team_msdyn_swarmskill";
				public const string TeamMsdynSwarmtemplate = "team_msdyn_swarmtemplate";
				public const string TeamMsdynSystemuserschedulersetting = "team_msdyn_systemuserschedulersetting";
				public const string TeamMsdynTaggedrecord = "team_msdyn_taggedrecord";
				public const string TeamMsdynTaxcode = "team_msdyn_taxcode";
				public const string TeamMsdynTaxcodedetail = "team_msdyn_taxcodedetail";
				public const string TeamMsdynTeamschannelengagementctx = "team_msdyn_teamschannelengagementctx";
				public const string TeamMsdynTeamsengagementctx = "team_msdyn_teamsengagementctx";
				public const string TeamMsdynTemplateforproperties = "team_msdyn_templateforproperties";
				public const string TeamMsdynTemplateparameter = "team_msdyn_templateparameter";
				public const string TeamMsdynTemplatetags = "team_msdyn_templatetags";
				public const string TeamMsdynTimeentry = "team_msdyn_timeentry";
				public const string TeamMsdynTimeentrysetting = "team_msdyn_timeentrysetting";
				public const string TeamMsdynTimegroup = "team_msdyn_timegroup";
				public const string TeamMsdynTimegroupdetail = "team_msdyn_timegroupdetail";
				public const string TeamMsdynTimeoffcalendar = "team_msdyn_timeoffcalendar";
				public const string TeamMsdynTimeoffrequest = "team_msdyn_timeoffrequest";
				public const string TeamMsdynTimespent = "team_msdyn_timespent";
				public const string TeamMsdynTrade = "team_msdyn_trade";
				public const string TeamMsdynTradecoverage = "team_msdyn_tradecoverage";
				public const string TeamMsdynTrainingresult = "team_msdyn_trainingresult";
				public const string TeamMsdynTransactionconnection = "team_msdyn_transactionconnection";
				public const string TeamMsdynTransactionorigin = "team_msdyn_transactionorigin";
				public const string TeamMsdynTranscript = "team_msdyn_transcript";
				public const string TeamMsdynTwitterengagementctx = "team_msdyn_twitterengagementctx";
				public const string TeamMsdynUnifiedroutingdiagnostic = "team_msdyn_unifiedroutingdiagnostic";
				public const string TeamMsdynUnifiedroutingrun = "team_msdyn_unifiedroutingrun";
				public const string TeamMsdynUntrackedappointment = "team_msdyn_untrackedappointment";
				public const string TeamMsdynUrnotificationtemplate = "team_msdyn_urnotificationtemplate";
				public const string TeamMsdynUrnotificationtemplatemapping = "team_msdyn_urnotificationtemplatemapping";
				public const string TeamMsdynUserworkhistory = "team_msdyn_userworkhistory";
				public const string TeamMsdynVirtualtablecolumncandidate = "team_msdyn_virtualtablecolumncandidate";
				public const string TeamMsdynVisitorjourney = "team_msdyn_visitorjourney";
				public const string TeamMsdynVivacustomerlist = "team_msdyn_vivacustomerlist";
				public const string TeamMsdynVivausersetting = "team_msdyn_vivausersetting";
				public const string TeamMsdynWallsavedqueryusersettings = "team_msdyn_wallsavedqueryusersettings";
				public const string TeamMsdynWarehouse = "team_msdyn_warehouse";
				public const string TeamMsdynWarranty = "team_msdyn_warranty";
				public const string TeamMsdynWechatengagementctx = "team_msdyn_wechatengagementctx";
				public const string TeamMsdynWhatsappengagementctx = "team_msdyn_whatsappengagementctx";
				public const string TeamMsdynWkwconfig = "team_msdyn_wkwconfig";
				public const string TeamMsdynWorkhourtemplate = "team_msdyn_workhourtemplate";
				public const string TeamMsdynWorkorder = "team_msdyn_workorder";
				public const string TeamMsdynWorkordercharacteristic = "team_msdyn_workordercharacteristic";
				public const string TeamMsdynWorkorderincident = "team_msdyn_workorderincident";
				public const string TeamMsdynWorkordernte = "team_msdyn_workordernte";
				public const string TeamMsdynWorkorderproduct = "team_msdyn_workorderproduct";
				public const string TeamMsdynWorkorderresolution = "team_msdyn_workorderresolution";
				public const string TeamMsdynWorkorderresourcerestriction = "team_msdyn_workorderresourcerestriction";
				public const string TeamMsdynWorkorderservice = "team_msdyn_workorderservice";
				public const string TeamMsdynWorkorderservicetask = "team_msdyn_workorderservicetask";
				public const string TeamMsdynWorkordersubstatus = "team_msdyn_workordersubstatus";
				public const string TeamMsdynWorkordertype = "team_msdyn_workordertype";
				public const string TeamMsdynWorkqueuestate = "team_msdyn_workqueuestate";
				public const string TeamMsdynWorkqueueusersetting = "team_msdyn_workqueueusersetting";
				public const string TeamMsdynceBotcontent = "team_msdynce_botcontent";
				public const string TeamMsdyncrmAddtocalendarstyle = "team_msdyncrm_addtocalendarstyle";
				public const string TeamMsdyncrmBasestyle = "team_msdyncrm_basestyle";
				public const string TeamMsdyncrmButtonstyle = "team_msdyncrm_buttonstyle";
				public const string TeamMsdyncrmCodestyle = "team_msdyncrm_codestyle";
				public const string TeamMsdyncrmColumnstyle = "team_msdyncrm_columnstyle";
				public const string TeamMsdyncrmContentblockstyle = "team_msdyncrm_contentblockstyle";
				public const string TeamMsdyncrmDividerstyle = "team_msdyncrm_dividerstyle";
				public const string TeamMsdyncrmGeneralstyles = "team_msdyncrm_generalstyles";
				public const string TeamMsdyncrmImagestyle = "team_msdyncrm_imagestyle";
				public const string TeamMsdyncrmLayoutstyle = "team_msdyncrm_layoutstyle";
				public const string TeamMsdyncrmQrcodestyle = "team_msdyncrm_qrcodestyle";
				public const string TeamMsdyncrmTextstyle = "team_msdyncrm_textstyle";
				public const string TeamMsdyncrmVideostyle = "team_msdyncrm_videostyle";
				public const string TeamMsdynmktByoacschannelinstance = "team_msdynmkt_byoacschannelinstance";
				public const string TeamMsdynmktByoacschannelinstanceaccount = "team_msdynmkt_byoacschannelinstanceaccount";
				public const string TeamMsdynmktCatalogeventstatusconfiguration = "team_msdynmkt_catalogeventstatusconfiguration";
				public const string TeamMsdynmktConfiguration = "team_msdynmkt_configuration";
				public const string TeamMsdynmktEventmetadata = "team_msdynmkt_eventmetadata";
				public const string TeamMsdynmktEventparametermetadata = "team_msdynmkt_eventparametermetadata";
				public const string TeamMsdynmktExperimentv2 = "team_msdynmkt_experimentv2";
				public const string TeamMsdynmktFeatureconfiguration = "team_msdynmkt_featureconfiguration";
				public const string TeamMsdynmktInfobipchannelinstance = "team_msdynmkt_infobipchannelinstance";
				public const string TeamMsdynmktInfobipchannelinstanceaccount = "team_msdynmkt_infobipchannelinstanceaccount";
				public const string TeamMsdynmktLinkmobilitychannelinstance = "team_msdynmkt_linkmobilitychannelinstance";
				public const string TeamMsdynmktLinkmobilitychannelinstanceaccount = "team_msdynmkt_linkmobilitychannelinstanceaccount";
				public const string TeamMsdynmktMetadataentityrelationship = "team_msdynmkt_metadataentityrelationship";
				public const string TeamMsdynmktMetadataitem = "team_msdynmkt_metadataitem";
				public const string TeamMsdynmktMetadatastorestate = "team_msdynmkt_metadatastorestate";
				public const string TeamMsdynmktMocksmsproviderchannelinstance = "team_msdynmkt_mocksmsproviderchannelinstance";
				public const string TeamMsdynmktMocksmsproviderchannelinstanceaccount = "team_msdynmkt_mocksmsproviderchannelinstanceaccount";
				public const string TeamMsdynmktPredefinedplaceholder = "team_msdynmkt_predefinedplaceholder";
				public const string TeamMsdynmktTelesignchannelinstance = "team_msdynmkt_telesignchannelinstance";
				public const string TeamMsdynmktTelesignchannelinstanceaccount = "team_msdynmkt_telesignchannelinstanceaccount";
				public const string TeamMsdynmktTwiliochannelinstance = "team_msdynmkt_twiliochannelinstance";
				public const string TeamMsdynmktTwiliochannelinstanceaccount = "team_msdynmkt_twiliochannelinstanceaccount";
				public const string TeamMsdynmktVibeschannelinstance = "team_msdynmkt_vibeschannelinstance";
				public const string TeamMsdynmktVibeschannelinstanceaccount = "team_msdynmkt_vibeschannelinstanceaccount";
				public const string TeamMsdyusdActioncallworkflow = "team_msdyusd_actioncallworkflow";
				public const string TeamMsdyusdAgentscriptaction = "team_msdyusd_agentscriptaction";
				public const string TeamMsdyusdAgentscripttaskcategory = "team_msdyusd_agentscripttaskcategory";
				public const string TeamMsdyusdAnswer = "team_msdyusd_answer";
				public const string TeamMsdyusdAuditanddiagnosticssetting = "team_msdyusd_auditanddiagnosticssetting";
				public const string TeamMsdyusdConfiguration = "team_msdyusd_configuration";
				public const string TeamMsdyusdCustomizationfiles = "team_msdyusd_customizationfiles";
				public const string TeamMsdyusdEntityassignment = "team_msdyusd_entityassignment";
				public const string TeamMsdyusdEntitysearch = "team_msdyusd_entitysearch";
				public const string TeamMsdyusdForm = "team_msdyusd_form";
				public const string TeamMsdyusdLanguagemodule = "team_msdyusd_languagemodule";
				public const string TeamMsdyusdScriptlet = "team_msdyusd_scriptlet";
				public const string TeamMsdyusdScripttasktrigger = "team_msdyusd_scripttasktrigger";
				public const string TeamMsdyusdSearch = "team_msdyusd_search";
				public const string TeamMsdyusdSessioninformation = "team_msdyusd_sessioninformation";
				public const string TeamMsdyusdSessiontransfer = "team_msdyusd_sessiontransfer";
				public const string TeamMsdyusdTask = "team_msdyusd_task";
				public const string TeamMsdyusdToolbarbutton = "team_msdyusd_toolbarbutton";
				public const string TeamMsdyusdToolbarstrip = "team_msdyusd_toolbarstrip";
				public const string TeamMsdyusdTracesourcesetting = "team_msdyusd_tracesourcesetting";
				public const string TeamMsdyusdUcisettings = "team_msdyusd_ucisettings";
				public const string TeamMsdyusdUiievent = "team_msdyusd_uiievent";
				public const string TeamMsdyusdUsersettings = "team_msdyusd_usersettings";
				public const string TeamMsdyusdWindowroute = "team_msdyusd_windowroute";
				public const string TeamMsfpAlertrule = "team_msfp_alertrule";
				public const string TeamMsfpEmailtemplate = "team_msfp_emailtemplate";
				public const string TeamMsfpFileresponse = "team_msfp_fileresponse";
				public const string TeamMsfpLocalizedemailtemplate = "team_msfp_localizedemailtemplate";
				public const string TeamMsfpProject = "team_msfp_project";
				public const string TeamMsfpQuestion = "team_msfp_question";
				public const string TeamMsfpQuestionresponse = "team_msfp_questionresponse";
				public const string TeamMsfpSatisfactionmetric = "team_msfp_satisfactionmetric";
				public const string TeamMsfpSurvey = "team_msfp_survey";
				public const string TeamMsfpSurveyreminder = "team_msfp_surveyreminder";
				public const string TeamMsfpUnsubscribedrecipient = "team_msfp_unsubscribedrecipient";
				public const string TeamMspcatCatalogsubmissionfiles = "team_mspcat_catalogsubmissionfiles";
				public const string TeamMspcatPackagestore = "team_mspcat_packagestore";
				public const string TeamNlsqregistration = "team_nlsqregistration";
				public const string TeamOpportunities = "team_opportunities";
				public const string TeamOpportunityclose = "team_opportunityclose";
				public const string TeamOpportunityproduct = "team_opportunityproduct";
				public const string TeamOrderclose = "team_orderclose";
				public const string TeamOrders = "team_orders";
				public const string TeamPdfsetting = "team_pdfsetting";
				public const string TeamPhonecall = "team_phonecall";
				public const string TeamPlannerbusinessscenario = "team_plannerbusinessscenario";
				public const string TeamPlannersyncaction = "team_plannersyncaction";
				public const string TeamPostRegardings = "team_PostRegardings";
				public const string TeamPostRoles = "team_PostRoles";
				public const string TeamPowerbidataset = "team_powerbidataset";
				public const string TeamPowerbidatasetapdx = "team_powerbidatasetapdx";
				public const string TeamPowerbimashupparameter = "team_powerbimashupparameter";
				public const string TeamPowerbireport = "team_powerbireport";
				public const string TeamPowerbireportapdx = "team_powerbireportapdx";
				public const string TeamPowerfxrule = "team_powerfxrule";
				public const string TeamPowerpagecomponent = "team_powerpagecomponent";
				public const string TeamPowerpagesite = "team_powerpagesite";
				public const string TeamPowerpagesitelanguage = "team_powerpagesitelanguage";
				public const string TeamPowerpagesitepublished = "team_powerpagesitepublished";
				public const string TeamPowerpageslog = "team_powerpageslog";
				public const string TeamPowerpagesscanreport = "team_powerpagesscanreport";
				public const string TeamPrincipalobjectattributeaccess = "team_principalobjectattributeaccess";
				public const string TeamPrincipalobjectattributeaccessPrincipalid = "team_principalobjectattributeaccess_principalid";
				public const string TeamProcesssession = "team_processsession";
				public const string TeamProcessSessions = "Team_ProcessSessions";
				public const string TeamProcessstageparameter = "team_processstageparameter";
				public const string TeamProfilerule = "team_profilerule";
				public const string TeamQueueitembaseWorkerid = "team_queueitembase_workerid";
				public const string TeamQuoteclose = "team_quoteclose";
				public const string TeamQuotedetail = "team_quotedetail";
				public const string TeamQuotes = "team_quotes";
				public const string TeamRatingmodel = "team_ratingmodel";
				public const string TeamRatingvalue = "team_ratingvalue";
				public const string TeamRecentlyused = "team_recentlyused";
				public const string TeamReconciliationentityinfo = "team_reconciliationentityinfo";
				public const string TeamReconciliationentitystepinfo = "team_reconciliationentitystepinfo";
				public const string TeamReconciliationinfo = "team_reconciliationinfo";
				public const string TeamRecurringappointmentmaster = "team_recurringappointmentmaster";
				public const string TeamResourceGroups = "team_resource_groups";
				public const string TeamResourceSpecs = "team_resource_specs";
				public const string TeamRetaineddataexcel = "team_retaineddataexcel";
				public const string TeamRetentioncleanupinfo = "team_retentioncleanupinfo";
				public const string TeamRetentioncleanupoperation = "team_retentioncleanupoperation";
				public const string TeamRetentionconfig = "team_retentionconfig";
				public const string TeamRetentionfailuredetail = "team_retentionfailuredetail";
				public const string TeamRetentionoperation = "team_retentionoperation";
				public const string TeamRoutingrule = "team_routingrule";
				public const string TeamRoutingruleitem = "team_routingruleitem";
				public const string TeamSalesorderdetail = "team_salesorderdetail";
				public const string TeamServiceAppointments = "team_service_appointments";
				public const string TeamServiceContracts = "team_service_contracts";
				public const string TeamSharepointdocumentlocation = "team_sharepointdocumentlocation";
				public const string TeamSharepointsite = "team_sharepointsite";
				public const string TeamSideloadedaiplugin = "team_sideloadedaiplugin";
				public const string TeamSlaBase = "team_slaBase";
				public const string TeamSocialactivity = "team_socialactivity";
				public const string TeamSolutioncomponentbatchconfiguration = "team_solutioncomponentbatchconfiguration";
				public const string TeamStagesolutionupload = "team_stagesolutionupload";
				public const string TeamSynapsedatabase = "team_synapsedatabase";
				public const string TeamSyncError = "team_SyncError";
				public const string TeamSyncErrors = "Team_SyncErrors";
				public const string TeamTask = "team_task";
				public const string TeamTdsmetadata = "team_tdsmetadata";
				public const string TeamTeammobileofflineprofilemembershipTeamId = "team_teammobileofflineprofilemembership_TeamId";
				public const string TeamUiiAction = "team_uii_action";
				public const string TeamUiiAudit = "team_uii_audit";
				public const string TeamUiiContext = "team_uii_context";
				public const string TeamUiiHostedapplication = "team_uii_hostedapplication";
				public const string TeamUiiNonhostedapplication = "team_uii_nonhostedapplication";
				public const string TeamUiiOption = "team_uii_option";
				public const string TeamUiiSavedsession = "team_uii_savedsession";
				public const string TeamUiiSessiontransfer = "team_uii_sessiontransfer";
				public const string TeamUiiWorkflow = "team_uii_workflow";
				public const string TeamUiiWorkflowWorkflowstepMapping = "team_uii_workflow_workflowstep_mapping";
				public const string TeamUiiWorkflowstep = "team_uii_workflowstep";
				public const string TeamUserentityinstancedata = "team_userentityinstancedata";
				public const string TeamUserentityuisettings = "team_userentityuisettings";
				public const string TeamUserform = "team_userform";
				public const string TeamUserquery = "team_userquery";
				public const string TeamUserqueryvisualizations = "team_userqueryvisualizations";
				public const string TeamWorkflow = "team_workflow";
				public const string TeamWorkflowbinary = "team_workflowbinary";
				public const string TeamWorkflowlog = "team_workflowlog";
				public const string TeamWorkqueue = "team_workqueue";
				public const string TeamWorkqueueitem = "team_workqueueitem";
				public const string UserentityinstancedataTeam = "userentityinstancedata_team";
            }

            public static class ManyToOne
            {
				public const string BusinessUnitTeams = "business_unit_teams";
				public const string KnowledgearticleTeams = "knowledgearticle_Teams";
				public const string LkTeamCreatedonbehalfby = "lk_team_createdonbehalfby";
				public const string LkTeamModifiedonbehalfby = "lk_team_modifiedonbehalfby";
				public const string LkTeambaseAdministratorid = "lk_teambase_administratorid";
				public const string LkTeambaseCreatedby = "lk_teambase_createdby";
				public const string LkTeambaseModifiedby = "lk_teambase_modifiedby";
				public const string OpportunityTeams = "opportunity_Teams";
				public const string OrganizationTeams = "organization_teams";
				public const string ProcessstageTeams = "processstage_teams";
				public const string QueueTeam = "queue_team";
				public const string TeamDelegatedauthorization = "team_delegatedauthorization";
				public const string TeamtemplateTeams = "teamtemplate_Teams";
				public const string TransactionCurrencyTeam = "TransactionCurrency_Team";
            }

            public static class ManyToMany
            {
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
            return Retrieve(service,id, new ColumnSet(true));
        }

        public static Team Retrieve(IOrganizationService service, Guid id, ColumnSet columnSet)
        {
            return service.Retrieve("team", id, columnSet).ToEntity<Team>();
        }

        public Team GetChangedEntity()
        {
            if (_trackChanges)
            {
                var attr = new AttributeCollection();
                foreach (var attrName in _changedProperties.Value.Select(changedProperty => ((AttributeLogicalNameAttribute) GetType().GetProperty(changedProperty)!.GetCustomAttribute(typeof (AttributeLogicalNameAttribute))!).LogicalName).Where(attrName => Contains(attrName)))
                {
                    attr.Add(attrName,this[attrName]);
                }
                return new  Team(Id) {Attributes = attr };
            }
            return this;
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
