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
	/// Please provide the description for entity
	/// </summary>
	[DataContractAttribute()]
	[EntityLogicalNameAttribute("routingruleitem")]
	[System.CodeDom.Compiler.GeneratedCode("dgtp", "2023")]
    [ExcludeFromCodeCoverage]
	public partial class RoutingRuleItem : Entity, INotifyPropertyChanging, INotifyPropertyChanged
    {
	    #region ctor
		[DebuggerNonUserCode]
		public RoutingRuleItem() : this(false)
        {
        }

        [DebuggerNonUserCode]
		public RoutingRuleItem(bool trackChanges = false) : base(EntityLogicalName)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public RoutingRuleItem(Guid id, bool trackChanges = false) : base(EntityLogicalName,id)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public RoutingRuleItem(KeyAttributeCollection keyAttributes, bool trackChanges = false) : base(EntityLogicalName,keyAttributes)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public RoutingRuleItem(string keyName, object keyValue, bool trackChanges = false) : base(EntityLogicalName, keyName, keyValue)
        {
			_trackChanges = trackChanges;
        }
        #endregion

		#region fields
        private readonly bool _trackChanges;
        private readonly Lazy<HashSet<string>> _changedProperties = new Lazy<HashSet<string>>();
        #endregion

        #region consts
        public const string EntityLogicalName = "routingruleitem";
        public const string PrimaryNameAttribute = "name";
        public const int EntityTypeCode = 8199;
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
		[AttributeLogicalNameAttribute("routingruleitemid")]
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
				RoutingRuleItemId = value;
			}
		}

		/// <summary>
		/// Unique identifier for entity instances
		/// </summary>
		[AttributeLogicalName("routingruleitemid")]
        public Guid? RoutingRuleItemId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<Guid?>("routingruleitemid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(RoutingRuleItemId));
                SetAttributeValue("routingruleitemid", value);
				if (value.HasValue)
				{
					base.Id = value.Value;
				}
				else
				{
					base.Id = System.Guid.Empty;
				}
                OnPropertyChanged(nameof(RoutingRuleItemId));
            }
        }

		/// <summary>
		/// Look for user/team records or create a new record.
		/// </summary>
		[AttributeLogicalName("assignobjectid")]
        public EntityReference? AssignObjectId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<EntityReference?>("assignobjectid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(AssignObjectId));
                SetAttributeValue("assignobjectid", value);
                OnPropertyChanged(nameof(AssignObjectId));
            }
        }

		/// <summary>
		/// Shows the date and time when the item was last assigned to a user.
		/// </summary>
		[AttributeLogicalName("assignobjectidmodifiedon")]
        public DateTime? AssignObjectIdModifiedOn
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<DateTime?>("assignobjectidmodifiedon");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(AssignObjectIdModifiedOn));
                SetAttributeValue("assignobjectidmodifiedon", value);
                OnPropertyChanged(nameof(AssignObjectIdModifiedOn));
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
		/// Condition for Rule item
		/// </summary>
		[AttributeLogicalName("conditionxml")]
        public string? ConditionXml
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("conditionxml");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(ConditionXml));
                SetAttributeValue("conditionxml", value);
                OnPropertyChanged(nameof(ConditionXml));
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
		/// Provide a description for the rule item.
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
		/// Exchange rate for the currency associated with the routing rule item with respect to the base currency.
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
		/// Choose if you want to route the record to queue or user/team.
		/// </summary>
		[AttributeLogicalName("msdyn_routeto")]
        public OptionSetValue? MsdynRouteto
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<OptionSetValue?>("msdyn_routeto");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(MsdynRouteto));
                SetAttributeValue("msdyn_routeto", value);
                OnPropertyChanged(nameof(MsdynRouteto));
            }
        }

		/// <summary>
		/// Provide a name for the rule item.
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
		/// Unique identifier of the organization associated with the routing rule item.
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
		/// Owner Id
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
        }

		/// <summary>
		/// Look for a queue or create a new queue.
		/// </summary>
		[AttributeLogicalName("routedqueueid")]
        public EntityReference? RoutedQueueId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<EntityReference?>("routedqueueid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(RoutedQueueId));
                SetAttributeValue("routedqueueid", value);
                OnPropertyChanged(nameof(RoutedQueueId));
            }
        }

		/// <summary>
		/// Unique identifier for Routing Rule associated with Rule Item.
		/// </summary>
		[AttributeLogicalName("routingruleid")]
        public EntityReference? RoutingRuleId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<EntityReference?>("routingruleid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(RoutingRuleId));
                SetAttributeValue("routingruleid", value);
                OnPropertyChanged(nameof(RoutingRuleId));
            }
        }

		/// <summary>
		/// For internal use only.
		/// </summary>
		[AttributeLogicalName("routingruleitemidunique")]
        public Guid? RoutingRuleItemIdUnique
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<Guid?>("routingruleitemidunique");
            }
        }

		/// <summary>
		/// Sequence number of the routing rule item
		/// </summary>
		[AttributeLogicalName("sequencenumber")]
        public int? SequenceNumber
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<int?>("sequencenumber");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(SequenceNumber));
                SetAttributeValue("sequencenumber", value);
                OnPropertyChanged(nameof(SequenceNumber));
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
		/// Version number of the Routing Rule Item.
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
		/// 1:N routingruleitem_AsyncOperations
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("routingruleitem_AsyncOperations")]
		public System.Collections.Generic.IEnumerable<AsyncOperation> RoutingruleitemAsyncOperations
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<AsyncOperation>("routingruleitem_AsyncOperations", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("RoutingruleitemAsyncOperations");
				this.SetRelatedEntities<AsyncOperation>("routingruleitem_AsyncOperations", null, value);
				this.OnPropertyChanged("RoutingruleitemAsyncOperations");
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
			    public struct MsdynRouteto
                {
					public const int Queue = 1;
					public const int User_Team = 2;
                }
		}
		#endregion

		#region LogicalNames
		public static class LogicalNames
		{
				public const string RoutingRuleItemId = "routingruleitemid";
				public const string AssignObjectId = "assignobjectid";
				public const string AssignObjectIdModifiedOn = "assignobjectidmodifiedon";
				public const string ComponentState = "componentstate";
				public const string ConditionXml = "conditionxml";
				public const string CreatedBy = "createdby";
				public const string CreatedOn = "createdon";
				public const string CreatedOnBehalfBy = "createdonbehalfby";
				public const string Description = "description";
				public const string ExchangeRate = "exchangerate";
				public const string IsManaged = "ismanaged";
				public const string ModifiedBy = "modifiedby";
				public const string ModifiedOn = "modifiedon";
				public const string ModifiedOnBehalfBy = "modifiedonbehalfby";
				public const string MsdynRouteto = "msdyn_routeto";
				public const string Name = "name";
				public const string OrganizationId = "organizationid";
				public const string OverwriteTime = "overwritetime";
				public const string OwnerId = "ownerid";
				public const string OwningBusinessUnit = "owningbusinessunit";
				public const string OwningUser = "owninguser";
				public const string RoutedQueueId = "routedqueueid";
				public const string RoutingRuleId = "routingruleid";
				public const string RoutingRuleItemIdUnique = "routingruleitemidunique";
				public const string SequenceNumber = "sequencenumber";
				public const string SolutionId = "solutionid";
				public const string TimeZoneRuleVersionNumber = "timezoneruleversionnumber";
				public const string TransactionCurrencyId = "transactioncurrencyid";
				public const string UTCConversionTimeZoneCode = "utcconversiontimezonecode";
				public const string VersionNumber = "versionnumber";
		}
		#endregion

		#region Relations
        public static class Relations
        {
            public static class OneToMany
            {
				public const string RoutingruleitemAnnotation = "routingruleitem_Annotation";
				public const string RoutingruleitemAsyncOperations = "routingruleitem_AsyncOperations";
				public const string RoutingruleitemBulkDeleteFailures = "routingruleitem_BulkDeleteFailures";
				public const string RoutingruleitemProcessSessions = "routingruleitem_ProcessSessions";
				public const string RoutingruleitemUserentityinstancedatas = "routingruleitem_userentityinstancedatas";
            }

            public static class ManyToOne
            {
				public const string LkRoutingRuleItemCreatedby = "lk_RoutingRuleItem_createdby";
				public const string LkRoutingruleitemCreatedonbehalfby = "lk_routingruleitem_createdonbehalfby";
				public const string LkRoutingruleitemModifiedby = "lk_routingruleitem_modifiedby";
				public const string LkRoutingruleitemModifiedonbehalfby = "lk_routingruleitem_modifiedonbehalfby";
				public const string OrganizationRoutingruleitems = "organization_routingruleitems";
				public const string QueueRoutingruleitem = "queue_routingruleitem";
				public const string RoutingruleEntries = "routingrule_entries";
				public const string TeamRoutingruleitem = "team_routingruleitem";
				public const string TransactionCurrencyRoutingruleitem = "TransactionCurrency_routingruleitem";
				public const string UserRoutingruleitem = "user_routingruleitem";
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
        public static RoutingRuleItem Retrieve(IOrganizationService service, Guid id)
        {
            return Retrieve(service,id, new ColumnSet(true));
        }

        public static RoutingRuleItem Retrieve(IOrganizationService service, Guid id, ColumnSet columnSet)
        {
            return service.Retrieve("routingruleitem", id, columnSet).ToEntity<RoutingRuleItem>();
        }

        public RoutingRuleItem GetChangedEntity()
        {
            if (_trackChanges)
            {
                var attr = new AttributeCollection();
                foreach (var attrName in _changedProperties.Value.Select(changedProperty => ((AttributeLogicalNameAttribute) GetType().GetProperty(changedProperty)!.GetCustomAttribute(typeof (AttributeLogicalNameAttribute))!).LogicalName).Where(attrName => Contains(attrName)))
                {
                    attr.Add(attrName,this[attrName]);
                }
                return new  RoutingRuleItem(Id) {Attributes = attr };
            }
            return this;
        }
        #endregion
	}

	#region Context
	public partial class DataContext
	{
		public IQueryable<RoutingRuleItem> RoutingRuleItemSet
		{
			get
			{
				return CreateQuery<RoutingRuleItem>();
			}
		}
	}
	#endregion
}
