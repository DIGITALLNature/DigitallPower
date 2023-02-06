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
	/// Defines free/busy times for a service and for resources or resource groups, such as working, non-working, vacation, and blocked.
	/// </summary>
	[DataContractAttribute()]
	[EntityLogicalNameAttribute("calendarrule")]
	[System.CodeDom.Compiler.GeneratedCode("dgtp", "2023")]
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
		public CalendarRule(Guid id, bool trackChanges = false) : base(EntityLogicalName,id)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public CalendarRule(KeyAttributeCollection keyAttributes, bool trackChanges = false) : base(EntityLogicalName,keyAttributes)
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
        private readonly Lazy<HashSet<string>> _changedProperties = new Lazy<HashSet<string>>();
        #endregion

        #region consts
        public const string EntityLogicalName = "calendarrule";
        public const string PrimaryNameAttribute = "name";
        public const int EntityTypeCode = 4004;
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
		[AttributeLogicalNameAttribute("calendarruleid")]
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
                OnPropertyChanging(nameof(CalendarRuleId));
                SetAttributeValue("calendarruleid", value);
				if (value.HasValue)
				{
					base.Id = value.Value;
				}
				else
				{
					base.Id = System.Guid.Empty;
				}
                OnPropertyChanged(nameof(CalendarRuleId));
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
                OnPropertyChanging(nameof(CalendarId));
                SetAttributeValue("calendarid", value);
                OnPropertyChanged(nameof(CalendarId));
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
                OnPropertyChanging(nameof(Description));
                SetAttributeValue("description", value);
                OnPropertyChanged(nameof(Description));
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
                OnPropertyChanging(nameof(Duration));
                SetAttributeValue("duration", value);
                OnPropertyChanged(nameof(Duration));
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
                OnPropertyChanging(nameof(EffectiveIntervalEnd));
                SetAttributeValue("effectiveintervalend", value);
                OnPropertyChanged(nameof(EffectiveIntervalEnd));
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
                OnPropertyChanging(nameof(EffectiveIntervalStart));
                SetAttributeValue("effectiveintervalstart", value);
                OnPropertyChanged(nameof(EffectiveIntervalStart));
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
                OnPropertyChanging(nameof(Effort));
                SetAttributeValue("effort", value);
                OnPropertyChanged(nameof(Effort));
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
                OnPropertyChanging(nameof(EndTime));
                SetAttributeValue("endtime", value);
                OnPropertyChanged(nameof(EndTime));
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
                OnPropertyChanging(nameof(ExtentCode));
                SetAttributeValue("extentcode", value);
                OnPropertyChanged(nameof(ExtentCode));
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
                OnPropertyChanging(nameof(GroupDesignator));
                SetAttributeValue("groupdesignator", value);
                OnPropertyChanged(nameof(GroupDesignator));
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
                OnPropertyChanging(nameof(InnerCalendarId));
                SetAttributeValue("innercalendarid", value);
                OnPropertyChanged(nameof(InnerCalendarId));
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
                OnPropertyChanging(nameof(IsModified));
                SetAttributeValue("ismodified", value);
                OnPropertyChanged(nameof(IsModified));
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
                OnPropertyChanging(nameof(IsSelected));
                SetAttributeValue("isselected", value);
                OnPropertyChanged(nameof(IsSelected));
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
                OnPropertyChanging(nameof(IsSimple));
                SetAttributeValue("issimple", value);
                OnPropertyChanged(nameof(IsSimple));
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
                OnPropertyChanging(nameof(IsVaried));
                SetAttributeValue("isvaried", value);
                OnPropertyChanged(nameof(IsVaried));
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
                OnPropertyChanging(nameof(Name));
                SetAttributeValue("name", value);
                OnPropertyChanged(nameof(Name));
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
                OnPropertyChanging(nameof(Offset));
                SetAttributeValue("offset", value);
                OnPropertyChanged(nameof(Offset));
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
                OnPropertyChanging(nameof(Pattern));
                SetAttributeValue("pattern", value);
                OnPropertyChanged(nameof(Pattern));
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
                OnPropertyChanging(nameof(Rank));
                SetAttributeValue("rank", value);
                OnPropertyChanged(nameof(Rank));
            }
        }

		/// <summary>
		/// Unique identifier of the service with which the calendar rule is associated.
		/// </summary>
		[AttributeLogicalName("serviceid")]
        public EntityReference? ServiceId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<EntityReference?>("serviceid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(ServiceId));
                SetAttributeValue("serviceid", value);
                OnPropertyChanged(nameof(ServiceId));
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
                OnPropertyChanging(nameof(StartTime));
                SetAttributeValue("starttime", value);
                OnPropertyChanged(nameof(StartTime));
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
                OnPropertyChanging(nameof(SubCode));
                SetAttributeValue("subcode", value);
                OnPropertyChanged(nameof(SubCode));
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
                OnPropertyChanging(nameof(TimeCode));
                SetAttributeValue("timecode", value);
                OnPropertyChanged(nameof(TimeCode));
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
                OnPropertyChanging(nameof(TimeZoneCode));
                SetAttributeValue("timezonecode", value);
                OnPropertyChanged(nameof(TimeZoneCode));
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
		public static class Options
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
		public static class LogicalNames
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
				public const string ServiceId = "serviceid";
				public const string StartTime = "starttime";
				public const string SubCode = "subcode";
				public const string TimeCode = "timecode";
				public const string TimeZoneCode = "timezonecode";
				public const string VersionNumber = "versionnumber";
		}
		#endregion

		#region Relations
        public static class Relations
        {
            public static class OneToMany
            {
				public const string UserentityinstancedataCalendarrule = "userentityinstancedata_calendarrule";
            }

            public static class ManyToOne
            {
				public const string CalendarCalendarRules = "calendar_calendar_rules";
				public const string InnerCalendarCalendarRules = "inner_calendar_calendar_rules";
				public const string LkCalendarruleCreatedby = "lk_calendarrule_createdby";
				public const string LkCalendarruleCreatedonbehalfby = "lk_calendarrule_createdonbehalfby";
				public const string LkCalendarruleModifiedby = "lk_calendarrule_modifiedby";
				public const string LkCalendarruleModifiedonbehalfby = "lk_calendarrule_modifiedonbehalfby";
				public const string ServiceCalendarRules = "service_calendar_rules";
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
        public static CalendarRule Retrieve(IOrganizationService service, Guid id)
        {
            return Retrieve(service,id, new ColumnSet(true));
        }

        public static CalendarRule Retrieve(IOrganizationService service, Guid id, ColumnSet columnSet)
        {
            return service.Retrieve("calendarrule", id, columnSet).ToEntity<CalendarRule>();
        }

        public CalendarRule GetChangedEntity()
        {
            if (_trackChanges)
            {
                var attr = new AttributeCollection();
                foreach (var attrName in _changedProperties.Value.Select(changedProperty => ((AttributeLogicalNameAttribute) GetType().GetProperty(changedProperty)!.GetCustomAttribute(typeof (AttributeLogicalNameAttribute))!).LogicalName).Where(attrName => Contains(attrName)))
                {
                    attr.Add(attrName,this[attrName]);
                }
                return new  CalendarRule(Id) {Attributes = attr };
            }
            return this;
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
