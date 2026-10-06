using System.Diagnostics.CodeAnalysis;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System.Runtime.Serialization;

// ReSharper disable All
namespace dgt.power.dataverse
{
    /// <inheritdoc cref="Microsoft.Xrm.Sdk.Entity" />
    /// <summary>
	/// Defines free/busy times for a service and for resources or resource groups, such as working, non-working, vacation, and blocked.
	/// </summary>
    [EntityLogicalName("calendarrule")]
    [System.CodeDom.Compiler.GeneratedCode("dgtp", "2026")]
    [ExcludeFromCodeCoverage]
    [SuppressMessage("Design", "CA1034")]
    [SuppressMessage("Performance", "CA1815")]
    public partial class CalendarRule : Entity
    {
        #region ctor
        public CalendarRule() : base(EntityLogicalName) { }

        public CalendarRule(Guid id) : base(EntityLogicalName, id) { }

        public CalendarRule(KeyAttributeCollection keyAttributes) : base(EntityLogicalName, keyAttributes) { }

        public CalendarRule(string keyName, object keyValue) : base(EntityLogicalName, keyName, keyValue) { }
        #endregion

        #region consts
        public const string EntityLogicalName = "calendarrule";
        public const string PrimaryNameAttribute = "name";
        public const int EntityTypeCode = 4004;
        #endregion

        #region Attributes
        [AttributeLogicalName("calendarruleid")]
        [IgnoreDataMember]
        public new Guid Id
        {
            get
            {
                return base.Id;
            }
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
            get
            {
                return GetAttributeValue<Guid?>("calendarruleid");
            }
            set
            {
                SetAttributeValue("calendarruleid", value);
                base.Id = value.HasValue ? value.Value : Guid.Empty;
            }
        }

        /// <summary>
		/// Unique identifier of the business unit with which the calendar rule is associated.
		/// </summary>
        [AttributeLogicalName("businessunitid")]
        public Guid? BusinessUnitId
        {
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
            get
            {
                return GetAttributeValue<EntityReference?>("calendarid");
            }
            set
            {
                SetAttributeValue("calendarid", value);
            }
        }

        /// <summary>
		/// Unique identifier of the user who created the calendar rule.
		/// </summary>
        [AttributeLogicalName("createdby")]
        public EntityReference? CreatedBy
        {
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
            get
            {
                return GetAttributeValue<string?>("description");
            }
            set
            {
                SetAttributeValue("description", value);
            }
        }

        /// <summary>
		/// Duration of the calendar rule in minutes.
		/// </summary>
        [AttributeLogicalName("duration")]
        public int? Duration
        {
            get
            {
                return GetAttributeValue<int?>("duration");
            }
            set
            {
                SetAttributeValue("duration", value);
            }
        }

        /// <summary>
		/// Effective interval end of the calendar rule.
		/// </summary>
        [AttributeLogicalName("effectiveintervalend")]
        public DateTime? EffectiveIntervalEnd
        {
            get
            {
                return GetAttributeValue<DateTime?>("effectiveintervalend");
            }
            set
            {
                SetAttributeValue("effectiveintervalend", value);
            }
        }

        /// <summary>
		/// Effective interval start of the calendar rule.
		/// </summary>
        [AttributeLogicalName("effectiveintervalstart")]
        public DateTime? EffectiveIntervalStart
        {
            get
            {
                return GetAttributeValue<DateTime?>("effectiveintervalstart");
            }
            set
            {
                SetAttributeValue("effectiveintervalstart", value);
            }
        }

        /// <summary>
		/// Effort available for a resource during the time described by the calendar rule.
		/// </summary>
        [AttributeLogicalName("effort")]
        public double? Effort
        {
            get
            {
                return GetAttributeValue<double?>("effort");
            }
            set
            {
                SetAttributeValue("effort", value);
            }
        }

        /// <summary>
		/// For internal use only.
		/// </summary>
        [AttributeLogicalName("endtime")]
        public DateTime? EndTime
        {
            get
            {
                return GetAttributeValue<DateTime?>("endtime");
            }
            set
            {
                SetAttributeValue("endtime", value);
            }
        }

        /// <summary>
		/// Extent of the calendar rule.
		/// </summary>
        [AttributeLogicalName("extentcode")]
        public int? ExtentCode
        {
            get
            {
                return GetAttributeValue<int?>("extentcode");
            }
            set
            {
                SetAttributeValue("extentcode", value);
            }
        }

        /// <summary>
		/// Unique identifier of the group.
		/// </summary>
        [AttributeLogicalName("groupdesignator")]
        public string? GroupDesignator
        {
            get
            {
                return GetAttributeValue<string?>("groupdesignator");
            }
            set
            {
                SetAttributeValue("groupdesignator", value);
            }
        }

        /// <summary>
		/// Unique identifier of the inner calendar for non-leaf calendar rules.
		/// </summary>
        [AttributeLogicalName("innercalendarid")]
        public EntityReference? InnerCalendarId
        {
            get
            {
                return GetAttributeValue<EntityReference?>("innercalendarid");
            }
            set
            {
                SetAttributeValue("innercalendarid", value);
            }
        }

        /// <summary>
		/// For internal use only.
		/// </summary>
        [AttributeLogicalName("ismodified")]
        public bool? IsModified
        {
            get
            {
                return GetAttributeValue<bool?>("ismodified");
            }
            set
            {
                SetAttributeValue("ismodified", value);
            }
        }

        /// <summary>
		/// Flag used in vary-by-day calendar rules.
		/// </summary>
        [AttributeLogicalName("isselected")]
        public bool? IsSelected
        {
            get
            {
                return GetAttributeValue<bool?>("isselected");
            }
            set
            {
                SetAttributeValue("isselected", value);
            }
        }

        /// <summary>
		/// Flag used in vary-by-day calendar rules.
		/// </summary>
        [AttributeLogicalName("issimple")]
        public bool? IsSimple
        {
            get
            {
                return GetAttributeValue<bool?>("issimple");
            }
            set
            {
                SetAttributeValue("issimple", value);
            }
        }

        /// <summary>
		/// Flag used in leaf nonrecurring rules.
		/// </summary>
        [AttributeLogicalName("isvaried")]
        public bool? IsVaried
        {
            get
            {
                return GetAttributeValue<bool?>("isvaried");
            }
            set
            {
                SetAttributeValue("isvaried", value);
            }
        }

        /// <summary>
		/// Unique identifier of the user who last modified the calendar rule.
		/// </summary>
        [AttributeLogicalName("modifiedby")]
        public EntityReference? ModifiedBy
        {
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
            get
            {
                return GetAttributeValue<string?>("name");
            }
            set
            {
                SetAttributeValue("name", value);
            }
        }

        /// <summary>
		/// Start offset for leaf nonrecurring rules.
		/// </summary>
        [AttributeLogicalName("offset")]
        public int? Offset
        {
            get
            {
                return GetAttributeValue<int?>("offset");
            }
            set
            {
                SetAttributeValue("offset", value);
            }
        }

        /// <summary>
		/// Unique identifier of the organization with which the calendar rule is associated.
		/// </summary>
        [AttributeLogicalName("organizationid")]
        public Guid? OrganizationId
        {
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
            get
            {
                return GetAttributeValue<string?>("pattern");
            }
            set
            {
                SetAttributeValue("pattern", value);
            }
        }

        /// <summary>
		/// Rank of the calendar rule.
		/// </summary>
        [AttributeLogicalName("rank")]
        public int? Rank
        {
            get
            {
                return GetAttributeValue<int?>("rank");
            }
            set
            {
                SetAttributeValue("rank", value);
            }
        }

        /// <summary>
		/// Start time for the rule.
		/// </summary>
        [AttributeLogicalName("starttime")]
        public DateTime? StartTime
        {
            get
            {
                return GetAttributeValue<DateTime?>("starttime");
            }
            set
            {
                SetAttributeValue("starttime", value);
            }
        }

        /// <summary>
		/// Sub-type of calendar rule.
		/// </summary>
        [AttributeLogicalName("subcode")]
        public int? SubCode
        {
            get
            {
                return GetAttributeValue<int?>("subcode");
            }
            set
            {
                SetAttributeValue("subcode", value);
            }
        }

        /// <summary>
		/// Type of calendar rule such as working hours, break, holiday, or time off.
		/// </summary>
        [AttributeLogicalName("timecode")]
        public int? TimeCode
        {
            get
            {
                return GetAttributeValue<int?>("timecode");
            }
            set
            {
                SetAttributeValue("timecode", value);
            }
        }

        /// <summary>
		/// Local time zone for the calendar rule.
		/// </summary>
        [AttributeLogicalName("timezonecode")]
        public int? TimeZoneCode
        {
            get
            {
                return GetAttributeValue<int?>("timezonecode");
            }
            set
            {
                SetAttributeValue("timezonecode", value);
            }
        }

        
        [AttributeLogicalName("versionnumber")]
        public long? VersionNumber
        {
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
                public const string CalendarruleDeletedItemReferences = "calendarrule_DeletedItemReferences";
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
