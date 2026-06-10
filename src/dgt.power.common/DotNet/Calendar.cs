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
	/// Calendar used by the scheduling system to define when an appointment or activity is to occur.
	/// </summary>
    [DataContract]
    [EntityLogicalName("calendar")]
    [System.CodeDom.Compiler.GeneratedCode("dgtp", "2026")]
    [ExcludeFromCodeCoverage]
    public partial class Calendar : Entity, INotifyPropertyChanging, INotifyPropertyChanged
    {
        #region ctor
        [DebuggerNonUserCode]
        public Calendar() : this(false)
        {
        }
        [DebuggerNonUserCode]
        public Calendar(bool trackChanges = false) : base(EntityLogicalName)
        {
            _trackChanges = trackChanges;
        }
        [DebuggerNonUserCode]
        public Calendar(Guid id, bool trackChanges = false) : base(EntityLogicalName, id)
        {
            _trackChanges = trackChanges;
        }
        [DebuggerNonUserCode]
        public Calendar(KeyAttributeCollection keyAttributes, bool trackChanges = false) : base(EntityLogicalName, keyAttributes)
        {
            _trackChanges = trackChanges;
        }
        [DebuggerNonUserCode]
        public Calendar(string keyName, object keyValue, bool trackChanges = false) : base(EntityLogicalName, keyName, keyValue)
        {
            _trackChanges = trackChanges;
        }
        #endregion

        #region fields
        private readonly bool _trackChanges;
        private readonly Lazy<HashSet<string>> _changedProperties = new();
        #endregion

        #region consts
        public const string EntityLogicalName = "calendar";
        public const string PrimaryNameAttribute = "name";
        public const int EntityTypeCode = 4003;
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
        [AttributeLogicalName("calendarid")]
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
                CalendarId = value;
            }
        }

        /// <summary>
		/// Unique identifier of the calendar.
		/// </summary>
        [AttributeLogicalName("calendarid")]
        public Guid? CalendarId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<Guid?>("calendarid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("calendarid", value);
                base.Id = value.HasValue ? value.Value : Guid.Empty;
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier of the business unit with which the calendar is associated.
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
                OnPropertyChanging();
                SetAttributeValue("businessunitid", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier of the user who created the calendar.
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
		/// Date and time when the calendar was created.
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
		/// Unique identifier of the delegate user who created the calendar.
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
		/// Calendar used by the scheduling system to define when an appointment or activity is to occur.
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
		/// Holiday Schedule CalendarId
		/// </summary>
        [AttributeLogicalName("holidayschedulecalendarid")]
        public EntityReference? HolidayScheduleCalendarId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<EntityReference?>("holidayschedulecalendarid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("holidayschedulecalendarid", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Calendar is shared by other calendars, such as the organization calendar.
		/// </summary>
        [AttributeLogicalName("isshared")]
        public bool? IsShared
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isshared");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("isshared", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier of the user who last modified the calendar.
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
		/// Date and time when the calendar was last modified.
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
		/// Unique identifier of the delegate user who last modified the calendar.
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
		/// Name of the calendar.
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
		/// Unique identifier of the organization with which the calendar is associated.
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
		/// Unique identifier of the primary user of this calendar.
		/// </summary>
        [AttributeLogicalName("primaryuserid")]
        public Guid? PrimaryUserId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<Guid?>("primaryuserid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("primaryuserid", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Calendar type, such as User work hour calendar, or Customer service hour calendar.
		/// </summary>
        [AttributeLogicalName("type")]
        public OptionSetValue? Type
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("type");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("type", value);
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

        /// <summary>
        /// 1:N BusinessUnit_Calendar
        /// </summary>
        [RelationshipSchemaName("BusinessUnit_Calendar")]
        public IEnumerable<BusinessUnit> BusinessUnitCalendar
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<BusinessUnit>("BusinessUnit_Calendar", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("BusinessUnit_Calendar", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N Calendar_AsyncOperations
        /// </summary>
        [RelationshipSchemaName("Calendar_AsyncOperations")]
        public IEnumerable<AsyncOperation> CalendarAsyncOperations
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<AsyncOperation>("Calendar_AsyncOperations", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("Calendar_AsyncOperations", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N calendar_calendar_rules
        /// </summary>
        [RelationshipSchemaName("calendar_calendar_rules")]
        public IEnumerable<CalendarRule> CalendarCalendarRules
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<CalendarRule>("calendar_calendar_rules", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("calendar_calendar_rules", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N calendar_customercalendar_holidaycalendar
        /// </summary>
        [RelationshipSchemaName("calendar_customercalendar_holidaycalendar")]
        public IEnumerable<Calendar> CalendarCustomercalendarHolidaycalendar
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<Calendar>("calendar_customercalendar_holidaycalendar", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("calendar_customercalendar_holidaycalendar", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N calendar_organization
        /// </summary>
        [RelationshipSchemaName("calendar_organization")]
        public IEnumerable<Organization> CalendarOrganization
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<Organization>("calendar_organization", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("calendar_organization", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N calendar_system_users
        /// </summary>
        [RelationshipSchemaName("calendar_system_users")]
        public IEnumerable<SystemUser> CalendarSystemUsers
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<SystemUser>("calendar_system_users", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("calendar_system_users", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N inner_calendar_calendar_rules
        /// </summary>
        [RelationshipSchemaName("inner_calendar_calendar_rules")]
        public IEnumerable<CalendarRule> InnerCalendarCalendarRules
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<CalendarRule>("inner_calendar_calendar_rules", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("inner_calendar_calendar_rules", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N slabase_businesshoursid
        /// </summary>
        [RelationshipSchemaName("slabase_businesshoursid")]
        public IEnumerable<SLA> SlabaseBusinesshoursid
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<SLA>("slabase_businesshoursid", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("slabase_businesshoursid", null, value);
                OnPropertyChanged();
            }
        }
        #endregion

        #region Options
        public static partial class Options
        {
            public struct IsShared
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct Type
            {
                public const int InnerCalendarType = -1;
                public const int Default = 0;
                public const int CustomerService = 1;
                public const int HolidaySchedule = 2;
            }
        }
        #endregion

        #region LogicalNames
        public static partial class LogicalNames
        {
            public const string CalendarId = "calendarid";
            public const string BusinessUnitId = "businessunitid";
            public const string CreatedBy = "createdby";
            public const string CreatedOn = "createdon";
            public const string CreatedOnBehalfBy = "createdonbehalfby";
            public const string Description = "description";
            public const string HolidayScheduleCalendarId = "holidayschedulecalendarid";
            public const string IsShared = "isshared";
            public const string ModifiedBy = "modifiedby";
            public const string ModifiedOn = "modifiedon";
            public const string ModifiedOnBehalfBy = "modifiedonbehalfby";
            public const string Name = "name";
            public const string OrganizationId = "organizationid";
            public const string PrimaryUserId = "primaryuserid";
            public const string Type = "type";
            public const string VersionNumber = "versionnumber";
        }
        #endregion

        #region Relations
        public static partial class Relations
        {
            public static class OneToMany
            {
                public const string BusinessUnitCalendar = "BusinessUnit_Calendar";
                public const string CalendarAnnotation = "Calendar_Annotation";
                public const string CalendarAsyncOperations = "Calendar_AsyncOperations";
                public const string CalendarBulkDeleteFailures = "Calendar_BulkDeleteFailures";
                public const string CalendarCalendarRules = "calendar_calendar_rules";
                public const string CalendarCustomercalendarHolidaycalendar = "calendar_customercalendar_holidaycalendar";
                public const string CalendarOrganization = "calendar_organization";
                public const string CalendarSlaitem = "calendar_slaitem";
                public const string CalendarSystemUsers = "calendar_system_users";
                public const string InnerCalendarCalendarRules = "inner_calendar_calendar_rules";
                public const string SlabaseBusinesshoursid = "slabase_businesshoursid";
                public const string UserentityinstancedataCalendar = "userentityinstancedata_calendar";
            }

            public static partial class ManyToOne
            {
                public const string BusinessUnitCalendars = "business_unit_calendars";
                public const string CalendarCustomercalendarHolidaycalendar = "calendar_customercalendar_holidaycalendar";
                public const string LkCalendarCreatedby = "lk_calendar_createdby";
                public const string LkCalendarCreatedonbehalfby = "lk_calendar_createdonbehalfby";
                public const string LkCalendarModifiedby = "lk_calendar_modifiedby";
                public const string LkCalendarModifiedonbehalfby = "lk_calendar_modifiedonbehalfby";
                public const string OrganizationCalendars = "organization_calendars";
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

        public static Calendar Retrieve(IOrganizationService service, Guid id)
        {
            return Retrieve(service, id, new ColumnSet(true));
        }

        public static Calendar Retrieve(IOrganizationService service, Guid id, ColumnSet columnSet)
        {
            return service.Retrieve("calendar", id, columnSet).ToEntity<Calendar>();
        }

        public Calendar GetChangedEntity()
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
            return new Calendar(Id) { Attributes = attr };
        }
        #endregion
    }

    #region Context
    public partial class DataContext
    {
        public IQueryable<Calendar> CalendarSet
        {
            get
            {
                return CreateQuery<Calendar>();
            }
        }
    }
    #endregion
}
