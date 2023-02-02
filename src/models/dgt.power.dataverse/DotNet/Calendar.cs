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
	/// Calendar used by the scheduling system to define when an appointment or activity is to occur.
	/// </summary>
	[DataContractAttribute()]
	[EntityLogicalNameAttribute("calendar")]
	[System.CodeDom.Compiler.GeneratedCode("ec4u.automation", "1.0.0")]
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
		public Calendar(Guid id, bool trackChanges = false) : base(EntityLogicalName,id)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public Calendar(KeyAttributeCollection keyAttributes, bool trackChanges = false) : base(EntityLogicalName,keyAttributes)
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
        private readonly Lazy<HashSet<string>> _changedProperties = new Lazy<HashSet<string>>();
        #endregion

        #region consts
        public const string EntityLogicalName = "calendar";
        public const int EntityTypeCode = 4003;
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
		[AttributeLogicalNameAttribute("calendarid")]
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
                OnPropertyChanging(nameof(CalendarId));
                SetAttributeValue("calendarid", value);
				if (value.HasValue)
				{
					base.Id = value.Value;
				}
				else
				{
					base.Id = System.Guid.Empty;
				}
                OnPropertyChanged(nameof(CalendarId));
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
                OnPropertyChanging(nameof(BusinessUnitId));
                SetAttributeValue("businessunitid", value);
                OnPropertyChanged(nameof(BusinessUnitId));
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
                OnPropertyChanging(nameof(Description));
                SetAttributeValue("description", value);
                OnPropertyChanged(nameof(Description));
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
                OnPropertyChanging(nameof(HolidayScheduleCalendarId));
                SetAttributeValue("holidayschedulecalendarid", value);
                OnPropertyChanged(nameof(HolidayScheduleCalendarId));
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
                OnPropertyChanging(nameof(IsShared));
                SetAttributeValue("isshared", value);
                OnPropertyChanged(nameof(IsShared));
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
                OnPropertyChanging(nameof(Name));
                SetAttributeValue("name", value);
                OnPropertyChanged(nameof(Name));
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
                OnPropertyChanging(nameof(PrimaryUserId));
                SetAttributeValue("primaryuserid", value);
                OnPropertyChanged(nameof(PrimaryUserId));
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
                OnPropertyChanging(nameof(Type));
                SetAttributeValue("type", value);
                OnPropertyChanged(nameof(Type));
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
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("BusinessUnit_Calendar")]
		public System.Collections.Generic.IEnumerable<BusinessUnit> BusinessUnitCalendar
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<BusinessUnit>("BusinessUnit_Calendar", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("BusinessUnitCalendar");
				this.SetRelatedEntities<BusinessUnit>("BusinessUnit_Calendar", null, value);
				this.OnPropertyChanged("BusinessUnitCalendar");
			}
		}

		/// <summary>
		/// 1:N Calendar_AsyncOperations
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("Calendar_AsyncOperations")]
		public System.Collections.Generic.IEnumerable<AsyncOperation> CalendarAsyncOperations
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<AsyncOperation>("Calendar_AsyncOperations", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("CalendarAsyncOperations");
				this.SetRelatedEntities<AsyncOperation>("Calendar_AsyncOperations", null, value);
				this.OnPropertyChanged("CalendarAsyncOperations");
			}
		}

		/// <summary>
		/// 1:N calendar_calendar_rules
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("calendar_calendar_rules")]
		public System.Collections.Generic.IEnumerable<CalendarRule> CalendarCalendarRules
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<CalendarRule>("calendar_calendar_rules", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("CalendarCalendarRules");
				this.SetRelatedEntities<CalendarRule>("calendar_calendar_rules", null, value);
				this.OnPropertyChanged("CalendarCalendarRules");
			}
		}

		/// <summary>
		/// 1:N calendar_customercalendar_holidaycalendar
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("calendar_customercalendar_holidaycalendar")]
		public System.Collections.Generic.IEnumerable<Calendar> CalendarCustomercalendarHolidaycalendar
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Calendar>("calendar_customercalendar_holidaycalendar", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("CalendarCustomercalendarHolidaycalendar");
				this.SetRelatedEntities<Calendar>("calendar_customercalendar_holidaycalendar", null, value);
				this.OnPropertyChanged("CalendarCustomercalendarHolidaycalendar");
			}
		}

		/// <summary>
		/// 1:N calendar_organization
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("calendar_organization")]
		public System.Collections.Generic.IEnumerable<Organization> CalendarOrganization
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Organization>("calendar_organization", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("CalendarOrganization");
				this.SetRelatedEntities<Organization>("calendar_organization", null, value);
				this.OnPropertyChanged("CalendarOrganization");
			}
		}

		/// <summary>
		/// 1:N calendar_system_users
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("calendar_system_users")]
		public System.Collections.Generic.IEnumerable<SystemUser> CalendarSystemUsers
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<SystemUser>("calendar_system_users", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("CalendarSystemUsers");
				this.SetRelatedEntities<SystemUser>("calendar_system_users", null, value);
				this.OnPropertyChanged("CalendarSystemUsers");
			}
		}

		/// <summary>
		/// 1:N inner_calendar_calendar_rules
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("inner_calendar_calendar_rules")]
		public System.Collections.Generic.IEnumerable<CalendarRule> InnerCalendarCalendarRules
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<CalendarRule>("inner_calendar_calendar_rules", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("InnerCalendarCalendarRules");
				this.SetRelatedEntities<CalendarRule>("inner_calendar_calendar_rules", null, value);
				this.OnPropertyChanged("InnerCalendarCalendarRules");
			}
		}

		/// <summary>
		/// 1:N slabase_businesshoursid
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("slabase_businesshoursid")]
		public System.Collections.Generic.IEnumerable<SLA> SlabaseBusinesshoursid
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<SLA>("slabase_businesshoursid", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("SlabaseBusinesshoursid");
				this.SetRelatedEntities<SLA>("slabase_businesshoursid", null, value);
				this.OnPropertyChanged("SlabaseBusinesshoursid");
			}
		}

		#endregion

		#region Options
		public static class Options
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
		public static class LogicalNames
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
        public static class Relations
        {
            public static class OneToMany
            {
				public const string BusinessUnitCalendar = "BusinessUnit_Calendar";
				public const string CalendarAnnotation = "Calendar_Annotation";
				public const string CalendarAsyncOperations = "Calendar_AsyncOperations";
				public const string CalendarBookableresources = "calendar_bookableresources";
				public const string CalendarBulkDeleteFailures = "Calendar_BulkDeleteFailures";
				public const string CalendarCalendarRules = "calendar_calendar_rules";
				public const string CalendarCustomercalendarHolidaycalendar = "calendar_customercalendar_holidaycalendar";
				public const string CalendarEquipment = "calendar_equipment";
				public const string CalendarOrganization = "calendar_organization";
				public const string CalendarServices = "calendar_services";
				public const string CalendarSlaitem = "calendar_slaitem";
				public const string CalendarSystemUsers = "calendar_system_users";
				public const string InnerCalendarCalendarRules = "inner_calendar_calendar_rules";
				public const string SlabaseBusinesshoursid = "slabase_businesshoursid";
				public const string UserentityinstancedataCalendar = "userentityinstancedata_calendar";
            }

            public static class ManyToOne
            {
				public const string BusinessUnitCalendars = "business_unit_calendars";
				public const string CalendarCustomercalendarHolidaycalendar = "calendar_customercalendar_holidaycalendar";
				public const string LkCalendarCreatedby = "lk_calendar_createdby";
				public const string LkCalendarCreatedonbehalfby = "lk_calendar_createdonbehalfby";
				public const string LkCalendarModifiedby = "lk_calendar_modifiedby";
				public const string LkCalendarModifiedonbehalfby = "lk_calendar_modifiedonbehalfby";
				public const string OrganizationCalendars = "organization_calendars";
            }

            public static class ManyToMany
            {
            }
        }

        #endregion

		#region Methods

        public static Calendar Retrieve(IOrganizationService service, Guid id)
        {
            return Retrieve(service,id, new ColumnSet(true));
        }

        public static Calendar Retrieve(IOrganizationService service, Guid id, ColumnSet columnSet)
        {
            return service.Retrieve("calendar", id, columnSet).ToEntity<Calendar>();
        }

        public Calendar GetChangedEntity()
        {
            if (_trackChanges)
            {
                var attr = new AttributeCollection();
                foreach (var attrName in _changedProperties.Value.Select(changedProperty => ((AttributeLogicalNameAttribute) GetType().GetProperty(changedProperty)!.GetCustomAttribute(typeof (AttributeLogicalNameAttribute))!).LogicalName).Where(attrName => Contains(attrName)))
                {
                    attr.Add(attrName,this[attrName]);
                }
                return new  Calendar(Id) {Attributes = attr };
            }
            return this;
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
