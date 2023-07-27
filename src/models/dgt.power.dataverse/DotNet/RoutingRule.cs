// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

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
	/// Define Routing Rule to route cases to right people at the right time
	/// </summary>
	[DataContractAttribute()]
	[EntityLogicalNameAttribute("routingrule")]
	[System.CodeDom.Compiler.GeneratedCode("dgtp", "2023")]
    [ExcludeFromCodeCoverage]
	public partial class RoutingRule : Entity, INotifyPropertyChanging, INotifyPropertyChanged
    {
	    #region ctor
		[DebuggerNonUserCode]
		public RoutingRule() : this(false)
        {
        }

        [DebuggerNonUserCode]
		public RoutingRule(bool trackChanges = false) : base(EntityLogicalName)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public RoutingRule(Guid id, bool trackChanges = false) : base(EntityLogicalName,id)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public RoutingRule(KeyAttributeCollection keyAttributes, bool trackChanges = false) : base(EntityLogicalName,keyAttributes)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public RoutingRule(string keyName, object keyValue, bool trackChanges = false) : base(EntityLogicalName, keyName, keyValue)
        {
			_trackChanges = trackChanges;
        }
        #endregion

		#region fields
        private readonly bool _trackChanges;
        private readonly Lazy<HashSet<string>> _changedProperties = new Lazy<HashSet<string>>();
        #endregion

        #region consts
        public const string EntityLogicalName = "routingrule";
        public const string PrimaryNameAttribute = "name";
        public const int EntityTypeCode = 8181;
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
		[AttributeLogicalNameAttribute("routingruleid")]
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
				RoutingRuleId = value;
			}
		}

		/// <summary>
		/// Unique identifier for entity instances
		/// </summary>
		[AttributeLogicalName("routingruleid")]
        public Guid? RoutingRuleId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<Guid?>("routingruleid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(RoutingRuleId));
                SetAttributeValue("routingruleid", value);
				if (value.HasValue)
				{
					base.Id = value.Value;
				}
				else
				{
					base.Id = System.Guid.Empty;
				}
                OnPropertyChanged(nameof(RoutingRuleId));
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
		/// Provide a description about the objective of the routing rule.
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
		/// Exchange rate for the currency associated with the queue with respect to the base currency.
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
		/// Logical name of the entity (deprecated).
		/// </summary>
		[AttributeLogicalName("msdyn_entitylogicalname")]
        public string? MsdynEntitylogicalname
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("msdyn_entitylogicalname");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(MsdynEntitylogicalname));
                SetAttributeValue("msdyn_entitylogicalname", value);
                OnPropertyChanged(nameof(MsdynEntitylogicalname));
            }
        }

		/// <summary>
		/// Provide a name for the routing rule.
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
		/// the organization associated with the Routing Rule
		/// </summary>
		[AttributeLogicalName("organizationid")]
        public EntityReference? OrganizationId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<EntityReference?>("organizationid");
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
		/// For internal use only.
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
		/// For internal use only.
		/// </summary>
		[AttributeLogicalName("owneridname")]
        public string? OwnerIdName
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("owneridname");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(OwnerIdName));
                SetAttributeValue("owneridname", value);
                OnPropertyChanged(nameof(OwnerIdName));
            }
        }

		/// <summary>
		/// Owner Id Type
		/// </summary>
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
		/// For internal use only
		/// </summary>
		[AttributeLogicalName("owneridyominame")]
        public string? OwnerIdYomiName
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("owneridyominame");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(OwnerIdYomiName));
                SetAttributeValue("owneridyominame", value);
                OnPropertyChanged(nameof(OwnerIdYomiName));
            }
        }

		/// <summary>
		/// For internal use only
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
		/// For internal use only.
		/// </summary>
		[AttributeLogicalName("routingruleidunique")]
        public Guid? RoutingRuleIdUnique
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<Guid?>("routingruleidunique");
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
		/// Status of the Routing Rule
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
		/// Reason for the status of the Routing Rule
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

		/// <summary>
		/// Unique identifier of the currency associated with the Routing Rule.
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

		/// <summary>
		/// Version number of the Routing Rule.
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
		/// Unique identifier for Workflow.
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
		/// 1:N routingrule_AsyncOperations
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("routingrule_AsyncOperations")]
		public System.Collections.Generic.IEnumerable<AsyncOperation> RoutingruleAsyncOperations
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<AsyncOperation>("routingrule_AsyncOperations", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("RoutingruleAsyncOperations");
				this.SetRelatedEntities<AsyncOperation>("routingrule_AsyncOperations", null, value);
				this.OnPropertyChanged("RoutingruleAsyncOperations");
			}
		}

		/// <summary>
		/// 1:N routingrule_entries
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("routingrule_entries")]
		public System.Collections.Generic.IEnumerable<RoutingRuleItem> RoutingruleEntries
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<RoutingRuleItem>("routingrule_entries", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("RoutingruleEntries");
				this.SetRelatedEntities<RoutingRuleItem>("routingrule_entries", null, value);
				this.OnPropertyChanged("RoutingruleEntries");
			}
		}

		#endregion

		#region Options
		public static class Options
		{
			    public struct ComponentState
                {
					public const int Published = 0;
					public const int Unpublished = 1;
					public const int Deleted = 2;
					public const int DeletedUnpublished = 3;
                }
                public struct IsManaged
                {
                    public const bool Unmanaged = false;
                    public const bool Managed = true;
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
				public const string RoutingRuleId = "routingruleid";
				public const string ComponentState = "componentstate";
				public const string CreatedBy = "createdby";
				public const string CreatedOn = "createdon";
				public const string CreatedOnBehalfBy = "createdonbehalfby";
				public const string Description = "description";
				public const string ExchangeRate = "exchangerate";
				public const string IsManaged = "ismanaged";
				public const string ModifiedBy = "modifiedby";
				public const string ModifiedOn = "modifiedon";
				public const string ModifiedOnBehalfBy = "modifiedonbehalfby";
				public const string MsdynEntitylogicalname = "msdyn_entitylogicalname";
				public const string Name = "name";
				public const string OrganizationId = "organizationid";
				public const string OverwriteTime = "overwritetime";
				public const string OwnerId = "ownerid";
				public const string OwnerIdName = "owneridname";
				public const string OwnerIdType = "owneridtype";
				public const string OwnerIdYomiName = "owneridyominame";
				public const string OwningBusinessUnit = "owningbusinessunit";
				public const string OwningTeam = "owningteam";
				public const string OwningUser = "owninguser";
				public const string RoutingRuleIdUnique = "routingruleidunique";
				public const string SolutionId = "solutionid";
				public const string StateCode = "statecode";
				public const string StatusCode = "statuscode";
				public const string TimeZoneRuleVersionNumber = "timezoneruleversionnumber";
				public const string TransactionCurrencyId = "transactioncurrencyid";
				public const string UTCConversionTimeZoneCode = "utcconversiontimezonecode";
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
				public const string RoutingruleAnnotation = "routingrule_Annotation";
				public const string RoutingruleAsyncOperations = "routingrule_AsyncOperations";
				public const string RoutingruleBulkDeleteFailures = "routingrule_BulkDeleteFailures";
				public const string RoutingruleEntries = "routingrule_entries";
				public const string RoutingruleProcessSessions = "routingrule_ProcessSessions";
				public const string RoutingruleUserentityinstancedatas = "routingrule_userentityinstancedatas";
            }

            public static class ManyToOne
            {
				public const string BusinessUnitRoutingrule = "business_unit_routingrule";
				public const string LkRoutingruleCreatedby = "lk_routingrule_createdby";
				public const string LkRoutingruleCreatedonbehalfby = "lk_routingrule_createdonbehalfby";
				public const string LkRoutingruleModifiedby = "lk_routingrule_modifiedby";
				public const string LkRoutingruleModifiedonbehalfby = "lk_routingrule_modifiedonbehalfby";
				public const string OrganizationRoutingRules = "organization_RoutingRules";
				public const string OwnerRoutingrule = "owner_routingrule";
				public const string TeamRoutingrule = "team_routingrule";
				public const string TransactionCurrencyRoutingrule = "TransactionCurrency_Routingrule";
				public const string UserRoutingrule = "user_routingrule";
				public const string WorkflowRoutingrule = "Workflow_routingrule";
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
        public static RoutingRule Retrieve(IOrganizationService service, Guid id)
        {
            return Retrieve(service,id, new ColumnSet(true));
        }

        public static RoutingRule Retrieve(IOrganizationService service, Guid id, ColumnSet columnSet)
        {
            return service.Retrieve("routingrule", id, columnSet).ToEntity<RoutingRule>();
        }

        public RoutingRule GetChangedEntity()
        {
            if (_trackChanges)
            {
                var attr = new AttributeCollection();
                foreach (var attrName in _changedProperties.Value.Select(changedProperty => ((AttributeLogicalNameAttribute) GetType().GetProperty(changedProperty)!.GetCustomAttribute(typeof (AttributeLogicalNameAttribute))!).LogicalName).Where(attrName => Contains(attrName)))
                {
                    attr.Add(attrName,this[attrName]);
                }
                return new  RoutingRule(Id) {Attributes = attr };
            }
            return this;
        }
        #endregion
	}

	#region Context
	public partial class DataContext
	{
		public IQueryable<RoutingRule> RoutingRuleSet
		{
			get
			{
				return CreateQuery<RoutingRule>();
			}
		}
	}
	#endregion
}
