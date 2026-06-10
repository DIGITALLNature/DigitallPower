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
	/// Defines free/busy times for a service and for resources or resource groups, such as working, non-working, vacation, and blocked.
	/// </summary>
    [DataContract]
    [EntityLogicalName("calendarrule")]
    [System.CodeDom.Compiler.GeneratedCode("dgtp", "2026")]
    [ExcludeFromCodeCoverage]
    public partial class CalendarRule : Entity, INotifyPropertyChanging, INotifyPropertyChanged
    {
        #region ctor
        [DebuggerNonUserCode]
        public CalendarRule() : this(false)
        {
        }
        [DebuggerNonUserCode]
        public CalendarRule(bool trackChanges = false) : base(EntityLogicalName)
        {
            _trackChanges = trackChanges;
        }
        [DebuggerNonUserCode]
        public CalendarRule(Guid id, bool trackChanges = false) : base(EntityLogicalName, id)
        {
            _trackChanges = trackChanges;
        }
        [DebuggerNonUserCode]
        public CalendarRule(KeyAttributeCollection keyAttributes, bool trackChanges = false) : base(EntityLogicalName, keyAttributes)
        {
            _trackChanges = trackChanges;
        }
        [DebuggerNonUserCode]
        public CalendarRule(string keyName, object keyValue, bool trackChanges = false) : base(EntityLogicalName, keyName, keyValue)
        {
            _trackChanges = trackChanges;
        }
        #endregion

        #region fields
        private readonly bool _trackChanges;
        private readonly Lazy<HashSet<string>> _changedProperties = new();
        #endregion

        #region consts
        public const string EntityLogicalName = "calendarrule";
        public const string PrimaryNameAttribute = "name";
        public const int EntityTypeCode = 4004;
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
        [AttributeLogicalName("calendarruleid")]
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
                CalendarRuleId = value;
            }
        }

        /// <summary>
		/// Unique identifier of the calendar rule.
		/// </summary>
        [AttributeLogicalName("calendarruleid")]
        public Guid? CalendarRuleId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<Guid?>("calendarruleid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("calendarruleid", value);
                base.Id = value.HasValue ? value.Value : Guid.Empty;
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier of the business unit with which the calendar rule is associated.
		/// </summary>
        [AttributeLogicalName("businessunitid")]
        public Guid? BusinessUnitId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<Guid?>("businessunitid");
            }
        }

        /// <summary>
		/// Unique identifier of the calendar with which the calendar rule is associated.
		/// </summary>
        [AttributeLogicalName("calendarid")]
        public EntityReference? CalendarId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<EntityReference?>("calendarid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("calendarid", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier of the user who created the calendar rule.
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
		/// Date and time when the calendar rule was created.
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
		/// Unique identifier of the delegate user who created the calendarrule.
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
		/// Defines free/busy times for a service and for resources or resource groups, such as working, non-working, vacation, and blocked.
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
		/// Duration of the calendar rule in minutes.
		/// </summary>
        [AttributeLogicalName("duration")]
        public int? Duration
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("duration");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("duration", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Effective interval end of the calendar rule.
		/// </summary>
        [AttributeLogicalName("effectiveintervalend")]
        public DateTime? EffectiveIntervalEnd
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<DateTime?>("effectiveintervalend");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("effectiveintervalend", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Effective interval start of the calendar rule.
		/// </summary>
        [AttributeLogicalName("effectiveintervalstart")]
        public DateTime? EffectiveIntervalStart
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<DateTime?>("effectiveintervalstart");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("effectiveintervalstart", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Effort available for a resource during the time described by the calendar rule.
		/// </summary>
        [AttributeLogicalName("effort")]
        public double? Effort
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<double?>("effort");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("effort", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// For internal use only.
		/// </summary>
        [AttributeLogicalName("endtime")]
        public DateTime? EndTime
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<DateTime?>("endtime");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("endtime", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Extent of the calendar rule.
		/// </summary>
        [AttributeLogicalName("extentcode")]
        public int? ExtentCode
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("extentcode");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("extentcode", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier of the group.
		/// </summary>
        [AttributeLogicalName("groupdesignator")]
        public string? GroupDesignator
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("groupdesignator");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("groupdesignator", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier of the inner calendar for non-leaf calendar rules.
		/// </summary>
        [AttributeLogicalName("innercalendarid")]
        public EntityReference? InnerCalendarId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<EntityReference?>("innercalendarid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("innercalendarid", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// For internal use only.
		/// </summary>
        [AttributeLogicalName("ismodified")]
        public bool? IsModified
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("ismodified");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("ismodified", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Flag used in vary-by-day calendar rules.
		/// </summary>
        [AttributeLogicalName("isselected")]
        public bool? IsSelected
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isselected");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("isselected", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Flag used in vary-by-day calendar rules.
		/// </summary>
        [AttributeLogicalName("issimple")]
        public bool? IsSimple
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("issimple");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("issimple", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Flag used in leaf nonrecurring rules.
		/// </summary>
        [AttributeLogicalName("isvaried")]
        public bool? IsVaried
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isvaried");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("isvaried", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier of the user who last modified the calendar rule.
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
		/// Date and time when the calendar rule was last modified.
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
		/// Unique identifier of the delegate user who last modified the calendarrule.
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
		/// Name of the calendar rule.
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
		/// Start offset for leaf nonrecurring rules.
		/// </summary>
        [AttributeLogicalName("offset")]
        public int? Offset
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("offset");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("offset", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier of the organization with which the calendar rule is associated.
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
		/// Pattern of the rule recurrence.
		/// </summary>
        [AttributeLogicalName("pattern")]
        public string? Pattern
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("pattern");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("pattern", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Rank of the calendar rule.
		/// </summary>
        [AttributeLogicalName("rank")]
        public int? Rank
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("rank");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("rank", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Start time for the rule.
		/// </summary>
        [AttributeLogicalName("starttime")]
        public DateTime? StartTime
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<DateTime?>("starttime");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("starttime", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Sub-type of calendar rule.
		/// </summary>
        [AttributeLogicalName("subcode")]
        public int? SubCode
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("subcode");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("subcode", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Type of calendar rule such as working hours, break, holiday, or time off.
		/// </summary>
        [AttributeLogicalName("timecode")]
        public int? TimeCode
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("timecode");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("timecode", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Local time zone for the calendar rule.
		/// </summary>
        [AttributeLogicalName("timezonecode")]
        public int? TimeZoneCode
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("timezonecode");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("timezonecode", value);
                OnPropertyChanged();
            }
        }

        
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
        #endregion

        #region Options
        public static partial class Options
        {
            public struct IsModified
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsSelected
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsSimple
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsVaried
            {
                public const bool No = false;
                public const bool Yes = true;
            }
        }
        #endregion

        #region LogicalNames
        public static partial class LogicalNames
        {
            public const string CalendarRuleId = "calendarruleid";
            public const string BusinessUnitId = "businessunitid";
            public const string CalendarId = "calendarid";
            public const string CreatedBy = "createdby";
            public const string CreatedOn = "createdon";
            public const string CreatedOnBehalfBy = "createdonbehalfby";
            public const string Description = "description";
            public const string Duration = "duration";
            public const string EffectiveIntervalEnd = "effectiveintervalend";
            public const string EffectiveIntervalStart = "effectiveintervalstart";
            public const string Effort = "effort";
            public const string EndTime = "endtime";
            public const string ExtentCode = "extentcode";
            public const string GroupDesignator = "groupdesignator";
            public const string InnerCalendarId = "innercalendarid";
            public const string IsModified = "ismodified";
            public const string IsSelected = "isselected";
            public const string IsSimple = "issimple";
            public const string IsVaried = "isvaried";
            public const string ModifiedBy = "modifiedby";
            public const string ModifiedOn = "modifiedon";
            public const string ModifiedOnBehalfBy = "modifiedonbehalfby";
            public const string Name = "name";
            public const string Offset = "offset";
            public const string OrganizationId = "organizationid";
            public const string Pattern = "pattern";
            public const string Rank = "rank";
            public const string StartTime = "starttime";
            public const string SubCode = "subcode";
            public const string TimeCode = "timecode";
            public const string TimeZoneCode = "timezonecode";
            public const string VersionNumber = "versionnumber";
        }
        #endregion

        #region Relations
        public static partial class Relations
        {
            public static class OneToMany
            {
                public const string UserentityinstancedataCalendarrule = "userentityinstancedata_calendarrule";
            }

            public static partial class ManyToOne
            {
                public const string CalendarCalendarRules = "calendar_calendar_rules";
                public const string InnerCalendarCalendarRules = "inner_calendar_calendar_rules";
                public const string LkCalendarruleCreatedby = "lk_calendarrule_createdby";
                public const string LkCalendarruleCreatedonbehalfby = "lk_calendarrule_createdonbehalfby";
                public const string LkCalendarruleModifiedby = "lk_calendarrule_modifiedby";
                public const string LkCalendarruleModifiedonbehalfby = "lk_calendarrule_modifiedonbehalfby";
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

        public static CalendarRule Retrieve(IOrganizationService service, Guid id)
        {
            return Retrieve(service, id, new ColumnSet(true));
        }

        public static CalendarRule Retrieve(IOrganizationService service, Guid id, ColumnSet columnSet)
        {
            return service.Retrieve("calendarrule", id, columnSet).ToEntity<CalendarRule>();
        }

        public CalendarRule GetChangedEntity()
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
            return new CalendarRule(Id) { Attributes = attr };
        }
        #endregion
    }

    #region Context
    public partial class DataContext
    {
        public IQueryable<CalendarRule> CalendarRuleSet
        {
            get
            {
                return CreateQuery<CalendarRule>();
            }
        }
    }
    #endregion
}
