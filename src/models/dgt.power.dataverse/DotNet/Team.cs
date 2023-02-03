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
	[System.CodeDom.Compiler.GeneratedCode("ec4u.automation", "1.0.0")]
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
		/// The Azure active directory object Id for a group.
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
		/// Type of the associated record for team - used for system managed access teams only.
		/// </summary>
		[AttributeLogicalName("regardingobjecttypecode")]
        public string? RegardingObjectTypeCode
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("regardingobjecttypecode");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(RegardingObjectTypeCode));
                SetAttributeValue("regardingobjecttypecode", value);
                OnPropertyChanged(nameof(RegardingObjectTypeCode));
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
		/// 1:N team_customapirequestparameter
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("team_customapirequestparameter")]
		public System.Collections.Generic.IEnumerable<CustomAPIRequestParameter> TeamCustomapirequestparameter
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<CustomAPIRequestParameter>("team_customapirequestparameter", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("TeamCustomapirequestparameter");
				this.SetRelatedEntities<CustomAPIRequestParameter>("team_customapirequestparameter", null, value);
				this.OnPropertyChanged("TeamCustomapirequestparameter");
			}
		}

		/// <summary>
		/// 1:N team_customapiresponseproperty
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("team_customapiresponseproperty")]
		public System.Collections.Generic.IEnumerable<CustomAPIResponseProperty> TeamCustomapiresponseproperty
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<CustomAPIResponseProperty>("team_customapiresponseproperty", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("TeamCustomapiresponseproperty");
				this.SetRelatedEntities<CustomAPIResponseProperty>("team_customapiresponseproperty", null, value);
				this.OnPropertyChanged("TeamCustomapiresponseproperty");
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
					public const int AADSecurityGroup = 2;
					public const int AADOfficeGroup = 3;
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
				public const string RegardingObjectTypeCode = "regardingobjecttypecode";
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
				public const string AADObjectIdWithMembershipType = "aadobjectid_membershiptype";
		}
		#endregion

		#region Relations
        public static class Relations
        {
            public static class OneToMany
            {
				public const string ChatTeamOwningteam = "chat_team_owningteam";
				public const string ImportFileTeam = "ImportFile_Team";
				public const string LeadOwningTeam = "lead_owning_team";
				public const string MsdynOcliveworkitemTeamOwningteam = "msdyn_ocliveworkitem_team_owningteam";
				public const string MsdynOcsessionTeamOwningteam = "msdyn_ocsession_team_owningteam";
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
				public const string TeamAnnotations = "team_annotations";
				public const string TeamAppnotification = "team_appnotification";
				public const string TeamAppointment = "team_appointment";
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
				public const string TeamConnectionreference = "team_connectionreference";
				public const string TeamConnections1 = "team_connections1";
				public const string TeamConnections2 = "team_connections2";
				public const string TeamConnector = "team_connector";
				public const string TeamContacts = "team_contacts";
				public const string TeamContractdetail = "team_contractdetail";
				public const string TeamConversationtranscript = "team_conversationtranscript";
				public const string TeamConvertrule = "team_convertrule";
				public const string TeamCustomapi = "team_customapi";
				public const string TeamCustomapirequestparameter = "team_customapirequestparameter";
				public const string TeamCustomapiresponseproperty = "team_customapiresponseproperty";
				public const string TeamCustomerOpportunityRoles = "team_customer_opportunity_roles";
				public const string TeamCustomerRelationship = "team_customer_relationship";
				public const string TeamDatalakefolder = "team_datalakefolder";
				public const string TeamDatalakefolderpermission = "team_datalakefolderpermission";
				public const string TeamDesktopflowbinary = "team_desktopflowbinary";
				public const string TeamDesktopflowmodule = "team_desktopflowmodule";
				public const string TeamDuplicateBaseRecord = "Team_DuplicateBaseRecord";
				public const string TeamDuplicateMatchingRecord = "Team_DuplicateMatchingRecord";
				public const string TeamDuplicateRules = "team_DuplicateRules";
				public const string TeamDynamicPropertyInstance = "team_DynamicPropertyInstance";
				public const string TeamEc4uAcquirelegalbasis = "team_ec4u_acquirelegalbasis";
				public const string TeamEc4uGdprProtocol = "team_ec4u_gdpr_protocol";
				public const string TeamEc4uGdprProtocolDetail = "team_ec4u_gdpr_protocol_detail";
				public const string TeamEc4uGdprReport = "team_ec4u_gdpr_report";
				public const string TeamEc4uGdprRequest = "team_ec4u_gdpr_request";
				public const string TeamEc4uLegalbasis = "team_ec4u_legalbasis";
				public const string TeamEmail = "team_email";
				public const string TeamEmailTemplates = "team_email_templates";
				public const string TeamEmailserverprofile = "team_emailserverprofile";
				public const string TeamEntitlement = "team_entitlement";
				public const string TeamEntitlementchannel = "team_entitlementchannel";
				public const string TeamEntitlemententityallocationtypemapping = "team_entitlemententityallocationtypemapping";
				public const string TeamEnvironmentvariabledefinition = "team_environmentvariabledefinition";
				public const string TeamEnvironmentvariablevalue = "team_environmentvariablevalue";
				public const string TeamExchangesyncidmapping = "team_exchangesyncidmapping";
				public const string TeamExportsolutionupload = "team_exportsolutionupload";
				public const string TeamExternalparty = "team_externalparty";
				public const string TeamFax = "team_fax";
				public const string TeamFeaturecontrolsetting = "team_featurecontrolsetting";
				public const string TeamFlowmachine = "team_flowmachine";
				public const string TeamFlowmachinegroup = "team_flowmachinegroup";
				public const string TeamFlowmachineimage = "team_flowmachineimage";
				public const string TeamFlowmachineimageversion = "team_flowmachineimageversion";
				public const string TeamFlowmachinenetwork = "team_flowmachinenetwork";
				public const string TeamFlowsession = "team_flowsession";
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
				public const string TeamMsdynActioncardregarding = "team_msdyn_actioncardregarding";
				public const string TeamMsdynActioncardrolesetting = "team_msdyn_actioncardrolesetting";
				public const string TeamMsdynAdminappstate = "team_msdyn_adminappstate";
				public const string TeamMsdynAgentstatushistory = "team_msdyn_agentstatushistory";
				public const string TeamMsdynAibdataset = "team_msdyn_aibdataset";
				public const string TeamMsdynAibdatasetfile = "team_msdyn_aibdatasetfile";
				public const string TeamMsdynAibdatasetrecord = "team_msdyn_aibdatasetrecord";
				public const string TeamMsdynAibdatasetscontainer = "team_msdyn_aibdatasetscontainer";
				public const string TeamMsdynAibfeedbackloop = "team_msdyn_aibfeedbackloop";
				public const string TeamMsdynAibfile = "team_msdyn_aibfile";
				public const string TeamMsdynAibfileattacheddata = "team_msdyn_aibfileattacheddata";
				public const string TeamMsdynAicontactsuggestion = "team_msdyn_aicontactsuggestion";
				public const string TeamMsdynAifptrainingdocument = "team_msdyn_aifptrainingdocument";
				public const string TeamMsdynAimodel = "team_msdyn_aimodel";
				public const string TeamMsdynAiodimage = "team_msdyn_aiodimage";
				public const string TeamMsdynAiodlabel = "team_msdyn_aiodlabel";
				public const string TeamMsdynAiodtrainingboundingbox = "team_msdyn_aiodtrainingboundingbox";
				public const string TeamMsdynAiodtrainingimage = "team_msdyn_aiodtrainingimage";
				public const string TeamMsdynAitemplate = "team_msdyn_aitemplate";
				public const string TeamMsdynAnalysiscomponent = "team_msdyn_analysiscomponent";
				public const string TeamMsdynAnalysisjob = "team_msdyn_analysisjob";
				public const string TeamMsdynAnalysisresult = "team_msdyn_analysisresult";
				public const string TeamMsdynAnalysisresultdetail = "team_msdyn_analysisresultdetail";
				public const string TeamMsdynAnalytics = "team_msdyn_analytics";
				public const string TeamMsdynAnalyticsadminsettings = "team_msdyn_analyticsadminsettings";
				public const string TeamMsdynAnalyticsforcs = "team_msdyn_analyticsforcs";
				public const string TeamMsdynAppconfiguration = "team_msdyn_appconfiguration";
				public const string TeamMsdynApplicationextension = "team_msdyn_applicationextension";
				public const string TeamMsdynApplicationtabtemplate = "team_msdyn_applicationtabtemplate";
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
				public const string TeamMsdynBookableresourcecapacityprofile = "team_msdyn_bookableresourcecapacityprofile";
				public const string TeamMsdynCallablecontext = "team_msdyn_callablecontext";
				public const string TeamMsdynCapacityprofile = "team_msdyn_capacityprofile";
				public const string TeamMsdynCdsentityengagementctx = "team_msdyn_cdsentityengagementctx";
				public const string TeamMsdynChanneldefinition = "team_msdyn_channeldefinition";
				public const string TeamMsdynChanneldefinitionconsent = "team_msdyn_channeldefinitionconsent";
				public const string TeamMsdynChanneldefinitionlocale = "team_msdyn_channeldefinitionlocale";
				public const string TeamMsdynChannelinstance = "team_msdyn_channelinstance";
				public const string TeamMsdynChannelinstanceaccount = "team_msdyn_channelinstanceaccount";
				public const string TeamMsdynChannelmessagepart = "team_msdyn_channelmessagepart";
				public const string TeamMsdynChannelprovider = "team_msdyn_channelprovider";
				public const string TeamMsdynConsumingapplication = "team_msdyn_consumingapplication";
				public const string TeamMsdynContactsuggestionrule = "team_msdyn_contactsuggestionrule";
				public const string TeamMsdynContactsuggestionruleset = "team_msdyn_contactsuggestionruleset";
				public const string TeamMsdynConversationaction = "team_msdyn_conversationaction";
				public const string TeamMsdynConversationactionlocale = "team_msdyn_conversationactionlocale";
				public const string TeamMsdynConversationdata = "team_msdyn_conversationdata";
				public const string TeamMsdynCrmconnection = "team_msdyn_crmconnection";
				public const string TeamMsdynCsadminconfig = "team_msdyn_csadminconfig";
				public const string TeamMsdynCustomapirulesetconfiguration = "team_msdyn_customapirulesetconfiguration";
				public const string TeamMsdynCustomcontrolextendedsettings = "team_msdyn_customcontrolextendedsettings";
				public const string TeamMsdynCustomerasset = "team_msdyn_customerasset";
				public const string TeamMsdynCustomerassetattachment = "team_msdyn_customerassetattachment";
				public const string TeamMsdynCustomerassetcategory = "team_msdyn_customerassetcategory";
				public const string TeamMsdynDataanalyticscustomizedreport = "team_msdyn_dataanalyticscustomizedreport";
				public const string TeamMsdynDataanalyticsdataset = "team_msdyn_dataanalyticsdataset";
				public const string TeamMsdynDataanalyticsreport = "team_msdyn_dataanalyticsreport";
				public const string TeamMsdynDataanalyticsworkspace = "team_msdyn_dataanalyticsworkspace";
				public const string TeamMsdynDataflow = "team_msdyn_dataflow";
				public const string TeamMsdynDataflowrefreshhistory = "team_msdyn_dataflowrefreshhistory";
				public const string TeamMsdynDealmanageraccess = "team_msdyn_dealmanageraccess";
				public const string TeamMsdynDealmanagersettings = "team_msdyn_dealmanagersettings";
				public const string TeamMsdynDecisioncontract = "team_msdyn_decisioncontract";
				public const string TeamMsdynDecisionruleset = "team_msdyn_decisionruleset";
				public const string TeamMsdynDefextendedchannelinstance = "team_msdyn_defextendedchannelinstance";
				public const string TeamMsdynDefextendedchannelinstanceaccount = "team_msdyn_defextendedchannelinstanceaccount";
				public const string TeamMsdynDuplicateleadmapping = "team_msdyn_duplicateleadmapping";
				public const string TeamMsdynEffortpredictionresult = "team_msdyn_effortpredictionresult";
				public const string TeamMsdynEntityconfig = "team_msdyn_entityconfig";
				public const string TeamMsdynEntitylinkchatconfiguration = "team_msdyn_entitylinkchatconfiguration";
				public const string TeamMsdynEntityrankingrule = "team_msdyn_entityrankingrule";
				public const string TeamMsdynEntityrefreshhistory = "team_msdyn_entityrefreshhistory";
				public const string TeamMsdynEntityroutingconfiguration = "team_msdyn_entityroutingconfiguration";
				public const string TeamMsdynExtendedusersetting = "team_msdyn_extendedusersetting";
				public const string TeamMsdynFederatedarticle = "team_msdyn_federatedarticle";
				public const string TeamMsdynFlowcardtype = "team_msdyn_flowcardtype";
				public const string TeamMsdynForecastconfiguration = "team_msdyn_forecastconfiguration";
				public const string TeamMsdynForecastdefinition = "team_msdyn_forecastdefinition";
				public const string TeamMsdynForecastinstance = "team_msdyn_forecastinstance";
				public const string TeamMsdynForecastrecurrence = "team_msdyn_forecastrecurrence";
				public const string TeamMsdynFunctionallocation = "team_msdyn_functionallocation";
				public const string TeamMsdynGdprdata = "team_msdyn_gdprdata";
				public const string TeamMsdynIcebreakersconfig = "team_msdyn_icebreakersconfig";
				public const string TeamMsdynIermlmodel = "team_msdyn_iermlmodel";
				public const string TeamMsdynIermltraining = "team_msdyn_iermltraining";
				public const string TeamMsdynIntegratedsearchprovider = "team_msdyn_integratedsearchprovider";
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
				public const string TeamMsdynKalanguagesetting = "team_msdyn_kalanguagesetting";
				public const string TeamMsdynKbattachment = "team_msdyn_kbattachment";
				public const string TeamMsdynKmfederatedsearchconfig = "team_msdyn_kmfederatedsearchconfig";
				public const string TeamMsdynKnowledgearticleimage = "team_msdyn_knowledgearticleimage";
				public const string TeamMsdynKnowledgearticletemplate = "team_msdyn_knowledgearticletemplate";
				public const string TeamMsdynKnowledgeinteractioninsight = "team_msdyn_knowledgeinteractioninsight";
				public const string TeamMsdynKnowledgemanagementsetting = "team_msdyn_knowledgemanagementsetting";
				public const string TeamMsdynKnowledgepersonalfilter = "team_msdyn_knowledgepersonalfilter";
				public const string TeamMsdynKnowledgesearchfilter = "team_msdyn_knowledgesearchfilter";
				public const string TeamMsdynKnowledgesearchinsight = "team_msdyn_knowledgesearchinsight";
				public const string TeamMsdynKpieventdata = "team_msdyn_kpieventdata";
				public const string TeamMsdynKpieventdefinition = "team_msdyn_kpieventdefinition";
				public const string TeamMsdynLeadmodelconfig = "team_msdyn_leadmodelconfig";
				public const string TeamMsdynLiveconversation = "team_msdyn_liveconversation";
				public const string TeamMsdynLiveworkitemevent = "team_msdyn_liveworkitemevent";
				public const string TeamMsdynLiveworkstream = "team_msdyn_liveworkstream";
				public const string TeamMsdynLiveworkstreamcapacityprofile = "team_msdyn_liveworkstreamcapacityprofile";
				public const string TeamMsdynMacrosession = "team_msdyn_macrosession";
				public const string TeamMsdynMasterentityroutingconfiguration = "team_msdyn_masterentityroutingconfiguration";
				public const string TeamMsdynMigrationtracker = "team_msdyn_migrationtracker";
				public const string TeamMsdynModelpreviewstatus = "team_msdyn_modelpreviewstatus";
				public const string TeamMsdynMsteamssetting = "team_msdyn_msteamssetting";
				public const string TeamMsdynNotesanalysisconfig = "team_msdyn_notesanalysisconfig";
				public const string TeamMsdynNotificationfield = "team_msdyn_notificationfield";
				public const string TeamMsdynNotificationtemplate = "team_msdyn_notificationtemplate";
				public const string TeamMsdynOcGeolocationprovider = "team_msdyn_oc_geolocationprovider";
				public const string TeamMsdynOcautoblockrule = "team_msdyn_ocautoblockrule";
				public const string TeamMsdynOcbotchannelregistration = "team_msdyn_ocbotchannelregistration";
				public const string TeamMsdynOcchannelapiconversationprivilege = "team_msdyn_occhannelapiconversationprivilege";
				public const string TeamMsdynOcchannelapimessageprivilege = "team_msdyn_occhannelapimessageprivilege";
				public const string TeamMsdynOcchannelapimethodmapping = "team_msdyn_occhannelapimethodmapping";
				public const string TeamMsdynOcflaggedspam = "team_msdyn_ocflaggedspam";
				public const string TeamMsdynOclanguage = "team_msdyn_oclanguage";
				public const string TeamMsdynOcliveworkitemcapacityprofile = "team_msdyn_ocliveworkitemcapacityprofile";
				public const string TeamMsdynOcliveworkitemcharacteristic = "team_msdyn_ocliveworkitemcharacteristic";
				public const string TeamMsdynOcliveworkitemcontextitem = "team_msdyn_ocliveworkitemcontextitem";
				public const string TeamMsdynOcliveworkitemparticipant = "team_msdyn_ocliveworkitemparticipant";
				public const string TeamMsdynOcliveworkitemsentiment = "team_msdyn_ocliveworkitemsentiment";
				public const string TeamMsdynOcliveworkstreamcontextvariable = "team_msdyn_ocliveworkstreamcontextvariable";
				public const string TeamMsdynOcpaymentprofile = "team_msdyn_ocpaymentprofile";
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
				public const string TeamMsdynOmnichannelpersonalization = "team_msdyn_omnichannelpersonalization";
				public const string TeamMsdynOmnichannelqueue = "team_msdyn_omnichannelqueue";
				public const string TeamMsdynOmnichannelsyncconfig = "team_msdyn_omnichannelsyncconfig";
				public const string TeamMsdynOperatinghour = "team_msdyn_operatinghour";
				public const string TeamMsdynOpportunitymodelconfig = "team_msdyn_opportunitymodelconfig";
				public const string TeamMsdynOverflowactionconfig = "team_msdyn_overflowactionconfig";
				public const string TeamMsdynPersonalmessage = "team_msdyn_personalmessage";
				public const string TeamMsdynPersonalsoundsetting = "team_msdyn_personalsoundsetting";
				public const string TeamMsdynPlaybookactivity = "team_msdyn_playbookactivity";
				public const string TeamMsdynPlaybookactivityattribute = "team_msdyn_playbookactivityattribute";
				public const string TeamMsdynPlaybookcategory = "team_msdyn_playbookcategory";
				public const string TeamMsdynPlaybookinstance = "team_msdyn_playbookinstance";
				public const string TeamMsdynPlaybooktemplate = "team_msdyn_playbooktemplate";
				public const string TeamMsdynPmanalysishistory = "team_msdyn_pmanalysishistory";
				public const string TeamMsdynPmcalendar = "team_msdyn_pmcalendar";
				public const string TeamMsdynPmcalendarversion = "team_msdyn_pmcalendarversion";
				public const string TeamMsdynPminferredtask = "team_msdyn_pminferredtask";
				public const string TeamMsdynPmprocessextendedmetadataversion = "team_msdyn_pmprocessextendedmetadataversion";
				public const string TeamMsdynPmprocessusersettings = "team_msdyn_pmprocessusersettings";
				public const string TeamMsdynPmprocessversion = "team_msdyn_pmprocessversion";
				public const string TeamMsdynPmrecording = "team_msdyn_pmrecording";
				public const string TeamMsdynPmtemplate = "team_msdyn_pmtemplate";
				public const string TeamMsdynPmview = "team_msdyn_pmview";
				public const string TeamMsdynPostalbum = "team_msdyn_postalbum";
				public const string TeamMsdynProductivityactioninputparameter = "team_msdyn_productivityactioninputparameter";
				public const string TeamMsdynProductivityactionoutputparameter = "team_msdyn_productivityactionoutputparameter";
				public const string TeamMsdynProductivityagentscript = "team_msdyn_productivityagentscript";
				public const string TeamMsdynProductivityagentscriptstep = "team_msdyn_productivityagentscriptstep";
				public const string TeamMsdynProductivitymacroactiontemplate = "team_msdyn_productivitymacroactiontemplate";
				public const string TeamMsdynProductivitymacroconnector = "team_msdyn_productivitymacroconnector";
				public const string TeamMsdynProductivitymacrosolutionconfiguration = "team_msdyn_productivitymacrosolutionconfiguration";
				public const string TeamMsdynProductivityparameterdefinition = "team_msdyn_productivityparameterdefinition";
				public const string TeamMsdynProperty = "team_msdyn_property";
				public const string TeamMsdynPropertyassetassociation = "team_msdyn_propertyassetassociation";
				public const string TeamMsdynPropertylog = "team_msdyn_propertylog";
				public const string TeamMsdynPropertytemplateassociation = "team_msdyn_propertytemplateassociation";
				public const string TeamMsdynRealtimescoring = "team_msdyn_realtimescoring";
				public const string TeamMsdynRecording = "team_msdyn_recording";
				public const string TeamMsdynRelationshipinsightsunifiedconfig = "team_msdyn_relationshipinsightsunifiedconfig";
				public const string TeamMsdynReportbookmark = "team_msdyn_reportbookmark";
				public const string TeamMsdynRichtextfile = "team_msdyn_richtextfile";
				public const string TeamMsdynRoutingconfiguration = "team_msdyn_routingconfiguration";
				public const string TeamMsdynRoutingconfigurationstep = "team_msdyn_routingconfigurationstep";
				public const string TeamMsdynRoutingrequest = "team_msdyn_routingrequest";
				public const string TeamMsdynRulesetdependencymapping = "team_msdyn_rulesetdependencymapping";
				public const string TeamMsdynSalesinsightssettings = "team_msdyn_salesinsightssettings";
				public const string TeamMsdynSalesroutingrun = "team_msdyn_salesroutingrun";
				public const string TeamMsdynSalessuggestion = "team_msdyn_salessuggestion";
				public const string TeamMsdynSalestag = "team_msdyn_salestag";
				public const string TeamMsdynSearchconfiguration = "team_msdyn_searchconfiguration";
				public const string TeamMsdynSegment = "team_msdyn_segment";
				public const string TeamMsdynSequence = "team_msdyn_sequence";
				public const string TeamMsdynSequencestat = "team_msdyn_sequencestat";
				public const string TeamMsdynSequencetarget = "team_msdyn_sequencetarget";
				public const string TeamMsdynSequencetargetstep = "team_msdyn_sequencetargetstep";
				public const string TeamMsdynSequencetemplate = "team_msdyn_sequencetemplate";
				public const string TeamMsdynServiceconfiguration = "team_msdyn_serviceconfiguration";
				public const string TeamMsdynSessiondata = "team_msdyn_sessiondata";
				public const string TeamMsdynSessionevent = "team_msdyn_sessionevent";
				public const string TeamMsdynSessionparticipant = "team_msdyn_sessionparticipant";
				public const string TeamMsdynSessionparticipantdata = "team_msdyn_sessionparticipantdata";
				public const string TeamMsdynSessiontemplate = "team_msdyn_sessiontemplate";
				public const string TeamMsdynSiconfig = "team_msdyn_siconfig";
				public const string TeamMsdynSkillattachmentruleitem = "team_msdyn_skillattachmentruleitem";
				public const string TeamMsdynSkillattachmenttarget = "team_msdyn_skillattachmenttarget";
				public const string TeamMsdynSlakpi = "team_msdyn_slakpi";
				public const string TeamMsdynSolutionhealthrule = "team_msdyn_solutionhealthrule";
				public const string TeamMsdynSolutionhealthruleargument = "team_msdyn_solutionhealthruleargument";
				public const string TeamMsdynSoundnotificationsetting = "team_msdyn_soundnotificationsetting";
				public const string TeamMsdynSwarm = "team_msdyn_swarm";
				public const string TeamMsdynSwarmparticipant = "team_msdyn_swarmparticipant";
				public const string TeamMsdynSwarmparticipantrule = "team_msdyn_swarmparticipantrule";
				public const string TeamMsdynSwarmrole = "team_msdyn_swarmrole";
				public const string TeamMsdynSwarmskill = "team_msdyn_swarmskill";
				public const string TeamMsdynSwarmtemplate = "team_msdyn_swarmtemplate";
				public const string TeamMsdynTaggedrecord = "team_msdyn_taggedrecord";
				public const string TeamMsdynTemplateforproperties = "team_msdyn_templateforproperties";
				public const string TeamMsdynTemplateparameter = "team_msdyn_templateparameter";
				public const string TeamMsdynTimespent = "team_msdyn_timespent";
				public const string TeamMsdynTranscript = "team_msdyn_transcript";
				public const string TeamMsdynUnifiedroutingdiagnostic = "team_msdyn_unifiedroutingdiagnostic";
				public const string TeamMsdynUnifiedroutingrun = "team_msdyn_unifiedroutingrun";
				public const string TeamMsdynUntrackedappointment = "team_msdyn_untrackedappointment";
				public const string TeamMsdynUrnotificationtemplate = "team_msdyn_urnotificationtemplate";
				public const string TeamMsdynUrnotificationtemplatemapping = "team_msdyn_urnotificationtemplatemapping";
				public const string TeamMsdynVirtualtablecolumncandidate = "team_msdyn_virtualtablecolumncandidate";
				public const string TeamMsdynVisitorjourney = "team_msdyn_visitorjourney";
				public const string TeamMsdynVivacustomerlist = "team_msdyn_vivacustomerlist";
				public const string TeamMsdynVivausersetting = "team_msdyn_vivausersetting";
				public const string TeamMsdynWallsavedqueryusersettings = "team_msdyn_wallsavedqueryusersettings";
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
				public const string TeamMsdynmktCatalogeventstatusconfiguration = "team_msdynmkt_catalogeventstatusconfiguration";
				public const string TeamMsdynmktConfiguration = "team_msdynmkt_configuration";
				public const string TeamMsdynmktEventmetadata = "team_msdynmkt_eventmetadata";
				public const string TeamMsdynmktEventparametermetadata = "team_msdynmkt_eventparametermetadata";
				public const string TeamMsdynmktFeatureconfiguration = "team_msdynmkt_featureconfiguration";
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
				public const string TeamOpportunities = "team_opportunities";
				public const string TeamOpportunityclose = "team_opportunityclose";
				public const string TeamOpportunityproduct = "team_opportunityproduct";
				public const string TeamOrderclose = "team_orderclose";
				public const string TeamOrders = "team_orders";
				public const string TeamPdfsetting = "team_pdfsetting";
				public const string TeamPhonecall = "team_phonecall";
				public const string TeamPostRegardings = "team_PostRegardings";
				public const string TeamPostRoles = "team_PostRoles";
				public const string TeamPowerbidataset = "team_powerbidataset";
				public const string TeamPowerbimashupparameter = "team_powerbimashupparameter";
				public const string TeamPowerbireport = "team_powerbireport";
				public const string TeamPowerfxrule = "team_powerfxrule";
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
				public const string TeamRecurringappointmentmaster = "team_recurringappointmentmaster";
				public const string TeamResourceGroups = "team_resource_groups";
				public const string TeamResourceSpecs = "team_resource_specs";
				public const string TeamRevokeinheritedaccessrecordstracker = "team_revokeinheritedaccessrecordstracker";
				public const string TeamRoutingrule = "team_routingrule";
				public const string TeamRoutingruleitem = "team_routingruleitem";
				public const string TeamSalesorderdetail = "team_salesorderdetail";
				public const string TeamServiceAppointments = "team_service_appointments";
				public const string TeamServiceContracts = "team_service_contracts";
				public const string TeamSharepointdocumentlocation = "team_sharepointdocumentlocation";
				public const string TeamSharepointsite = "team_sharepointsite";
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
				public const string TeamUserentityinstancedata = "team_userentityinstancedata";
				public const string TeamUserentityuisettings = "team_userentityuisettings";
				public const string TeamUserform = "team_userform";
				public const string TeamUserquery = "team_userquery";
				public const string TeamUserqueryvisualizations = "team_userqueryvisualizations";
				public const string TeamWorkflow = "team_workflow";
				public const string TeamWorkflowbinary = "team_workflowbinary";
				public const string TeamWorkflowlog = "team_workflowlog";
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
