using System.Diagnostics.CodeAnalysis;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System.Runtime.Serialization;

// ReSharper disable All
namespace dgt.power.dataverse
{
    /// <inheritdoc cref="Microsoft.Xrm.Sdk.Entity" />
    /// <summary>
	/// Calendar used by the scheduling system to define when an appointment or activity is to occur.
	/// </summary>
    [EntityLogicalName("calendar")]
    [System.CodeDom.Compiler.GeneratedCode("dgtp", "2026")]
    [ExcludeFromCodeCoverage]
    [SuppressMessage("Design", "CA1034")]
    [SuppressMessage("Performance", "CA1815")]
    public partial class Calendar : Entity
    {
        #region ctor
        public Calendar() : base(EntityLogicalName) { }

        public Calendar(Guid id) : base(EntityLogicalName, id) { }

        public Calendar(KeyAttributeCollection keyAttributes) : base(EntityLogicalName, keyAttributes) { }

        public Calendar(string keyName, object keyValue) : base(EntityLogicalName, keyName, keyValue) { }
        #endregion

        #region consts
        public const string EntityLogicalName = "calendar";
        public const string PrimaryNameAttribute = "name";
        public const int EntityTypeCode = 4003;
        #endregion

        #region Attributes
        [AttributeLogicalName("calendarid")]
        [IgnoreDataMember]
        public new Guid Id
        {
            get
            {
                return base.Id;
            }
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
            get
            {
                return GetAttributeValue<Guid?>("calendarid");
            }
            set
            {
                SetAttributeValue("calendarid", value);
                base.Id = value.HasValue ? value.Value : Guid.Empty;
            }
        }

        /// <summary>
		/// Unique identifier of the business unit with which the calendar is associated.
		/// </summary>
        [AttributeLogicalName("businessunitid")]
        public EntityReference? BusinessUnitId
        {
            get
            {
                return GetAttributeValue<EntityReference?>("businessunitid");
            }
            set
            {
                SetAttributeValue("businessunitid", value);
            }
        }

        /// <summary>
		/// Unique identifier of the user who created the calendar.
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
		/// Date and time when the calendar was created.
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
		/// Unique identifier of the delegate user who created the calendar.
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
		/// Calendar used by the scheduling system to define when an appointment or activity is to occur.
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
		/// Holiday Schedule CalendarId
		/// </summary>
        [AttributeLogicalName("holidayschedulecalendarid")]
        public EntityReference? HolidayScheduleCalendarId
        {
            get
            {
                return GetAttributeValue<EntityReference?>("holidayschedulecalendarid");
            }
            set
            {
                SetAttributeValue("holidayschedulecalendarid", value);
            }
        }

        /// <summary>
		/// Calendar is shared by other calendars, such as the organization calendar.
		/// </summary>
        [AttributeLogicalName("isshared")]
        public bool? IsShared
        {
            get
            {
                return GetAttributeValue<bool?>("isshared");
            }
            set
            {
                SetAttributeValue("isshared", value);
            }
        }

        /// <summary>
		/// Unique identifier of the user who last modified the calendar.
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
		/// Date and time when the calendar was last modified.
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
		/// Unique identifier of the delegate user who last modified the calendar.
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
		/// Name of the calendar.
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
		/// Unique identifier of the organization with which the calendar is associated.
		/// </summary>
        [AttributeLogicalName("organizationid")]
        public EntityReference? OrganizationId
        {
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
            get
            {
                return GetAttributeValue<Guid?>("primaryuserid");
            }
            set
            {
                SetAttributeValue("primaryuserid", value);
            }
        }

        /// <summary>
		/// Calendar type, such as User work hour calendar, or Customer service hour calendar.
		/// </summary>
        [AttributeLogicalName("type")]
        public OptionSetValue? Type
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("type");
            }
            set
            {
                SetAttributeValue("type", value);
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

        /// <summary>
        /// 1:N BusinessUnit_Calendar
        /// </summary>
        [RelationshipSchemaName("BusinessUnit_Calendar")]
        public IEnumerable<BusinessUnit> BusinessUnitCalendar
        {
            get
            {
                return GetRelatedEntities<BusinessUnit>("BusinessUnit_Calendar", null);
            }
            set
            {
                SetRelatedEntities("BusinessUnit_Calendar", null, value);
            }
        }

        /// <summary>
        /// 1:N Calendar_AsyncOperations
        /// </summary>
        [RelationshipSchemaName("Calendar_AsyncOperations")]
        public IEnumerable<AsyncOperation> CalendarAsyncOperations
        {
            get
            {
                return GetRelatedEntities<AsyncOperation>("Calendar_AsyncOperations", null);
            }
            set
            {
                SetRelatedEntities("Calendar_AsyncOperations", null, value);
            }
        }

        /// <summary>
        /// 1:N calendar_calendar_rules
        /// </summary>
        [RelationshipSchemaName("calendar_calendar_rules")]
        public IEnumerable<CalendarRule> CalendarCalendarRules
        {
            get
            {
                return GetRelatedEntities<CalendarRule>("calendar_calendar_rules", null);
            }
            set
            {
                SetRelatedEntities("calendar_calendar_rules", null, value);
            }
        }

        /// <summary>
        /// 1:N calendar_customercalendar_holidaycalendar
        /// </summary>
        [RelationshipSchemaName("calendar_customercalendar_holidaycalendar")]
        public IEnumerable<Calendar> CalendarCustomercalendarHolidaycalendar
        {
            get
            {
                return GetRelatedEntities<Calendar>("calendar_customercalendar_holidaycalendar", null);
            }
            set
            {
                SetRelatedEntities("calendar_customercalendar_holidaycalendar", null, value);
            }
        }

        /// <summary>
        /// 1:N calendar_organization
        /// </summary>
        [RelationshipSchemaName("calendar_organization")]
        public IEnumerable<Organization> CalendarOrganization
        {
            get
            {
                return GetRelatedEntities<Organization>("calendar_organization", null);
            }
            set
            {
                SetRelatedEntities("calendar_organization", null, value);
            }
        }

        /// <summary>
        /// 1:N calendar_system_users
        /// </summary>
        [RelationshipSchemaName("calendar_system_users")]
        public IEnumerable<SystemUser> CalendarSystemUsers
        {
            get
            {
                return GetRelatedEntities<SystemUser>("calendar_system_users", null);
            }
            set
            {
                SetRelatedEntities("calendar_system_users", null, value);
            }
        }

        /// <summary>
        /// 1:N inner_calendar_calendar_rules
        /// </summary>
        [RelationshipSchemaName("inner_calendar_calendar_rules")]
        public IEnumerable<CalendarRule> InnerCalendarCalendarRules
        {
            get
            {
                return GetRelatedEntities<CalendarRule>("inner_calendar_calendar_rules", null);
            }
            set
            {
                SetRelatedEntities("inner_calendar_calendar_rules", null, value);
            }
        }

        /// <summary>
        /// 1:N slabase_businesshoursid
        /// </summary>
        [RelationshipSchemaName("slabase_businesshoursid")]
        public IEnumerable<SLA> SlabaseBusinesshoursid
        {
            get
            {
                return GetRelatedEntities<SLA>("slabase_businesshoursid", null);
            }
            set
            {
                SetRelatedEntities("slabase_businesshoursid", null, value);
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
