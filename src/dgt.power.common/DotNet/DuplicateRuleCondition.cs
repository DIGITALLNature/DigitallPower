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
	/// Condition of a duplicate detection rule.
	/// </summary>
    [DataContract]
    [EntityLogicalName("duplicaterulecondition")]
    [System.CodeDom.Compiler.GeneratedCode("dgtp", "2026")]
    [ExcludeFromCodeCoverage]
    [SuppressMessage("Design", "CA1034")]
    [SuppressMessage("Performance", "CA1815")]
    public partial class DuplicateRuleCondition : Entity, INotifyPropertyChanging, INotifyPropertyChanged
    {
        #region ctor
        [DebuggerNonUserCode]
        public DuplicateRuleCondition() : this(false)
        {
        }
        [DebuggerNonUserCode]
        public DuplicateRuleCondition(bool trackChanges = false) : base(EntityLogicalName)
        {
            _trackChanges = trackChanges;
        }
        [DebuggerNonUserCode]
        public DuplicateRuleCondition(Guid id, bool trackChanges = false) : base(EntityLogicalName, id)
        {
            _trackChanges = trackChanges;
        }
        [DebuggerNonUserCode]
        public DuplicateRuleCondition(KeyAttributeCollection keyAttributes, bool trackChanges = false) : base(EntityLogicalName, keyAttributes)
        {
            _trackChanges = trackChanges;
        }
        [DebuggerNonUserCode]
        public DuplicateRuleCondition(string keyName, object keyValue, bool trackChanges = false) : base(EntityLogicalName, keyName, keyValue)
        {
            _trackChanges = trackChanges;
        }
        #endregion

        #region fields
        private readonly bool _trackChanges;
        private readonly Lazy<HashSet<string>> _changedProperties = new();
        #endregion

        #region consts
        public const string EntityLogicalName = "duplicaterulecondition";
        public const int EntityTypeCode = 4416;
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
        [AttributeLogicalName("duplicateruleconditionid")]
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
                DuplicateRuleConditionId = value;
            }
        }

        /// <summary>
		/// Unique identifier of the condition.
		/// </summary>
        [AttributeLogicalName("duplicateruleconditionid")]
        public Guid? DuplicateRuleConditionId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<Guid?>("duplicateruleconditionid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("duplicateruleconditionid", value);
                base.Id = value.HasValue ? value.Value : Guid.Empty;
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Field that is being compared.
		/// </summary>
        [AttributeLogicalName("baseattributename")]
        public string? BaseAttributeName
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("baseattributename");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("baseattributename", value);
                OnPropertyChanged();
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
		/// Unique identifier of the user who created the condition.
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
		/// Date and time when the condition was created.
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
		/// Unique identifier of the delegate user who created the duplicate rule condition.
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
		/// Determines whether to consider blank values as non-duplicate values
		/// </summary>
        [AttributeLogicalName("ignoreblankvalues")]
        public bool? IgnoreBlankValues
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("ignoreblankvalues");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("ignoreblankvalues", value);
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
		/// Field that is being compared with the base field.
		/// </summary>
        [AttributeLogicalName("matchingattributename")]
        public string? MatchingAttributeName
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("matchingattributename");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("matchingattributename", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier of the user who last modified the condition.
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
		/// Date and time when the condition was last modified.
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
		/// Unique identifier of the delegate user who last modified the duplicate rule condition.
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
		/// Operator for this rule condition.
		/// </summary>
        [AttributeLogicalName("operatorcode")]
        public OptionSetValue? OperatorCode
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("operatorcode");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("operatorcode", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Parameter value of N if the operator is Same First Characters or Same Last Characters.
		/// </summary>
        [AttributeLogicalName("operatorparam")]
        public int? OperatorParam
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("operatorparam");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("operatorparam", value);
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
		/// Unique identifier of the user or team who owns the duplicate rule condition.
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
		/// Unique identifier of the business unit that owns the condition.
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
		/// Unique identifier of the user who owns the condition.
		/// </summary>
        [AttributeLogicalName("owninguser")]
        public Guid? OwningUser
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<Guid?>("owninguser");
            }
        }

        /// <summary>
		/// Unique identifier of the object with which the condition is associated.
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

        
        [AttributeLogicalName("uniquerulename")]
        public string? UniqueRuleName
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("uniquerulename");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("uniquerulename", value);
                OnPropertyChanged();
            }
        }
        #endregion

        #region NavigationProperties
        #endregion

        #region Options
        public static partial class Options
        {
            public struct ComponentState
            {
                public const int Published = 0;
                public const int Unpublished = 1;
                public const int Deleted = 2;
                public const int DeletedUnpublished = 3;
            }
            public struct IgnoreBlankValues
            {
                public const bool False = false;
                public const bool True = true;
            }
            public struct IsManaged
            {
                public const bool Unmanaged = false;
                public const bool Managed = true;
            }
            public struct OperatorCode
            {
                public const int ExactMatch = 0;
                public const int SameFirstCharacters = 1;
                public const int SameLastCharacters = 2;
                public const int SameDate = 3;
                public const int SameDateAndTime = 4;
                public const int ExactMatchPickListLabel = 5;
                public const int ExactMatchPickListValue = 6;
            }
        }
        #endregion

        #region LogicalNames
        public static partial class LogicalNames
        {
            public const string DuplicateRuleConditionId = "duplicateruleconditionid";
            public const string BaseAttributeName = "baseattributename";
            public const string ComponentIdUnique = "componentidunique";
            public const string ComponentState = "componentstate";
            public const string CreatedBy = "createdby";
            public const string CreatedOn = "createdon";
            public const string CreatedOnBehalfBy = "createdonbehalfby";
            public const string IgnoreBlankValues = "ignoreblankvalues";
            public const string IsCustomizable = "iscustomizable";
            public const string IsManaged = "ismanaged";
            public const string MatchingAttributeName = "matchingattributename";
            public const string ModifiedBy = "modifiedby";
            public const string ModifiedOn = "modifiedon";
            public const string ModifiedOnBehalfBy = "modifiedonbehalfby";
            public const string OperatorCode = "operatorcode";
            public const string OperatorParam = "operatorparam";
            public const string OverwriteTime = "overwritetime";
            public const string OwnerId = "ownerid";
            public const string OwningBusinessUnit = "owningbusinessunit";
            public const string OwningUser = "owninguser";
            public const string RegardingObjectId = "regardingobjectid";
            public const string SolutionId = "solutionid";
            public const string UniqueRuleName = "uniquerulename";
        }
        #endregion

        #region AlternateKeys
        public static partial class AlternateKeys
        {
            public const string Dupruleconditionuniquekey = "dupruleconditionuniquekey";
        }
        #endregion

        #region Relations
        public static partial class Relations
        {
            public static class OneToMany
            {
                public const string DuplicateRuleConditionSyncErrors = "DuplicateRuleCondition_SyncErrors";
                public const string UserentityinstancedataDuplicaterulecondition = "userentityinstancedata_duplicaterulecondition";
            }

            public static partial class ManyToOne
            {
                public const string DuplicateRuleDuplicateRuleConditions = "DuplicateRule_DuplicateRuleConditions";
                public const string LkDuplicateruleconditionCreatedonbehalfby = "lk_duplicaterulecondition_createdonbehalfby";
                public const string LkDuplicateruleconditionModifiedonbehalfby = "lk_duplicaterulecondition_modifiedonbehalfby";
                public const string LkDuplicateruleconditionbaseCreatedby = "lk_duplicateruleconditionbase_createdby";
                public const string LkDuplicateruleconditionbaseModifiedby = "lk_duplicateruleconditionbase_modifiedby";
            }

            public static partial class ManyToMany
            {
            }
        }
        #endregion

        #region Methods

        public static DuplicateRuleCondition Retrieve(IOrganizationService service, Guid id)
        {
            return Retrieve(service, id, new ColumnSet(true));
        }

        public static DuplicateRuleCondition Retrieve(IOrganizationService service, Guid id, ColumnSet columnSet)
        {
            return service.Retrieve("duplicaterulecondition", id, columnSet).ToEntity<DuplicateRuleCondition>();
        }

        public DuplicateRuleCondition GetChangedEntity()
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
            return new DuplicateRuleCondition(Id) { Attributes = attr };
        }
        #endregion
    }

    #region Context
    public partial class DataContext
    {
        public IQueryable<DuplicateRuleCondition> DuplicateRuleConditionSet
        {
            get
            {
                return CreateQuery<DuplicateRuleCondition>();
            }
        }
    }
    #endregion
}
