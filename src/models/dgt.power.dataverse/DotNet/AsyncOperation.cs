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
	/// Process whose execution can proceed independently or in the background.
	/// </summary>
	[DataContractAttribute()]
	[EntityLogicalNameAttribute("asyncoperation")]
	[System.CodeDom.Compiler.GeneratedCode("dgtp", "2023")]
    [ExcludeFromCodeCoverage]
	public partial class AsyncOperation : Entity, INotifyPropertyChanging, INotifyPropertyChanged
    {
	    #region ctor
		[DebuggerNonUserCode]
		public AsyncOperation() : this(false)
        {
        }

        [DebuggerNonUserCode]
		public AsyncOperation(bool trackChanges = false) : base(EntityLogicalName)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public AsyncOperation(Guid id, bool trackChanges = false) : base(EntityLogicalName,id)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public AsyncOperation(KeyAttributeCollection keyAttributes, bool trackChanges = false) : base(EntityLogicalName,keyAttributes)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public AsyncOperation(string keyName, object keyValue, bool trackChanges = false) : base(EntityLogicalName, keyName, keyValue)
        {
			_trackChanges = trackChanges;
        }
        #endregion

		#region fields
        private readonly bool _trackChanges;
        private readonly Lazy<HashSet<string>> _changedProperties = new Lazy<HashSet<string>>();
        #endregion

        #region consts
        public const string EntityLogicalName = "asyncoperation";
        public const string PrimaryNameAttribute = "name";
        public const int EntityTypeCode = 4700;
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
		[AttributeLogicalNameAttribute("asyncoperationid")]
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
				AsyncOperationId = value;
			}
		}

		/// <summary>
		/// Unique identifier of the system job.
		/// </summary>
		[AttributeLogicalName("asyncoperationid")]
        public Guid? AsyncOperationId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<Guid?>("asyncoperationid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(AsyncOperationId));
                SetAttributeValue("asyncoperationid", value);
				if (value.HasValue)
				{
					base.Id = value.Value;
				}
				else
				{
					base.Id = System.Guid.Empty;
				}
                OnPropertyChanged(nameof(AsyncOperationId));
            }
        }

		/// <summary>
		/// The breadcrumb record ID.
		/// </summary>
		[AttributeLogicalName("breadcrumbid")]
        public Guid? BreadcrumbId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<Guid?>("breadcrumbid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(BreadcrumbId));
                SetAttributeValue("breadcrumbid", value);
                OnPropertyChanged(nameof(BreadcrumbId));
            }
        }

		/// <summary>
		/// The origin of the caller.
		/// </summary>
		[AttributeLogicalName("callerorigin")]
        public string? CallerOrigin
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("callerorigin");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(CallerOrigin));
                SetAttributeValue("callerorigin", value);
                OnPropertyChanged(nameof(CallerOrigin));
            }
        }

		/// <summary>
		/// Date and time when the system job was completed.
		/// </summary>
		[AttributeLogicalName("completedon")]
        public DateTime? CompletedOn
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<DateTime?>("completedon");
            }
        }

		/// <summary>
		/// Unique identifier used to correlate between multiple SDK requests and system jobs.
		/// </summary>
		[AttributeLogicalName("correlationid")]
        public Guid? CorrelationId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<Guid?>("correlationid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(CorrelationId));
                SetAttributeValue("correlationid", value);
                OnPropertyChanged(nameof(CorrelationId));
            }
        }

		/// <summary>
		/// Last time the correlation depth was updated.
		/// </summary>
		[AttributeLogicalName("correlationupdatedtime")]
        public DateTime? CorrelationUpdatedTime
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<DateTime?>("correlationupdatedtime");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(CorrelationUpdatedTime));
                SetAttributeValue("correlationupdatedtime", value);
                OnPropertyChanged(nameof(CorrelationUpdatedTime));
            }
        }

		/// <summary>
		/// Unique identifier of the user who created the system job.
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
		/// Date and time when the system job was created.
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
		/// Unique identifier of the delegate user who created the asyncoperation.
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
		/// Unstructured data associated with the system job.
		/// </summary>
		[AttributeLogicalName("data")]
        public string? Data
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("data");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(Data));
                SetAttributeValue("data", value);
                OnPropertyChanged(nameof(Data));
            }
        }

		/// <summary>
		/// File Id for the blob url used for file storage.
		/// </summary>
		[AttributeLogicalName("datablobid")]
        public Guid? DataBlobId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<Guid?>("datablobid");
            }
        }

		/// <summary>
		/// Execution of all operations with the same dependency token is serialized.
		/// </summary>
		[AttributeLogicalName("dependencytoken")]
        public string? DependencyToken
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("dependencytoken");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(DependencyToken));
                SetAttributeValue("dependencytoken", value);
                OnPropertyChanged(nameof(DependencyToken));
            }
        }

		/// <summary>
		/// Number of SDK calls made since the first call.
		/// </summary>
		[AttributeLogicalName("depth")]
        public int? Depth
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<int?>("depth");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(Depth));
                SetAttributeValue("depth", value);
                OnPropertyChanged(nameof(Depth));
            }
        }

		/// <summary>
		/// Error code returned from a canceled system job.
		/// </summary>
		[AttributeLogicalName("errorcode")]
        public int? ErrorCode
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<int?>("errorcode");
            }
        }

		/// <summary>
		/// Time that the system job has taken to execute.
		/// </summary>
		[AttributeLogicalName("executiontimespan")]
        public double? ExecutionTimeSpan
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<double?>("executiontimespan");
            }
        }

		/// <summary>
		/// The datetime when the Expander pipeline started.
		/// </summary>
		[AttributeLogicalName("expanderstarttime")]
        public DateTime? ExpanderStartTime
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<DateTime?>("expanderstarttime");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(ExpanderStartTime));
                SetAttributeValue("expanderstarttime", value);
                OnPropertyChanged(nameof(ExpanderStartTime));
            }
        }

		/// <summary>
		/// Message provided by the system job.
		/// </summary>
		[AttributeLogicalName("friendlymessage")]
        public string? FriendlyMessage
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("friendlymessage");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(FriendlyMessage));
                SetAttributeValue("friendlymessage", value);
                OnPropertyChanged(nameof(FriendlyMessage));
            }
        }

		/// <summary>
		/// Unique identifier of the host that owns this system job.
		/// </summary>
		[AttributeLogicalName("hostid")]
        public string? HostId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("hostid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(HostId));
                SetAttributeValue("hostid", value);
                OnPropertyChanged(nameof(HostId));
            }
        }

		/// <summary>
		/// Indicates that the system job is waiting for an event.
		/// </summary>
		[AttributeLogicalName("iswaitingforevent")]
        public bool? IsWaitingForEvent
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<bool?>("iswaitingforevent");
            }
        }

		/// <summary>
		/// Message related to the system job.
		/// </summary>
		[AttributeLogicalName("message")]
        public string? Message
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("message");
            }
        }

		/// <summary>
		/// Name of the message that started this system job.
		/// </summary>
		[AttributeLogicalName("messagename")]
        public string? MessageName
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("messagename");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(MessageName));
                SetAttributeValue("messagename", value);
                OnPropertyChanged(nameof(MessageName));
            }
        }

		/// <summary>
		/// Unique identifier of the user who last modified the system job.
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
		/// Date and time when the system job was last modified.
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
		/// Unique identifier of the delegate user who last modified the asyncoperation.
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
		/// Name of the system job.
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
		/// Type of the system job.
		/// </summary>
		[AttributeLogicalName("operationtype")]
        public OptionSetValue? OperationType
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<OptionSetValue?>("operationtype");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(OperationType));
                SetAttributeValue("operationtype", value);
                OnPropertyChanged(nameof(OperationType));
            }
        }

		/// <summary>
		/// Unique identifier of the user or team who owns the system job.
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
		/// Unique identifier of the business unit that owns the system job.
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
		/// Unique identifier of the owning extension with which the system job is associated.
		/// </summary>
		[AttributeLogicalName("owningextensionid")]
        public EntityReference? OwningExtensionId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<EntityReference?>("owningextensionid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(OwningExtensionId));
                SetAttributeValue("owningextensionid", value);
                OnPropertyChanged(nameof(OwningExtensionId));
            }
        }

		
		[AttributeLogicalName("owningextensionidname")]
        public string? OwningExtensionIdName
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("owningextensionidname");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(OwningExtensionIdName));
                SetAttributeValue("owningextensionidname", value);
                OnPropertyChanged(nameof(OwningExtensionIdName));
            }
        }

		
		[AttributeLogicalName("owningextensiontypecode")]
        public string? OwningExtensionTypeCode
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("owningextensiontypecode");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(OwningExtensionTypeCode));
                SetAttributeValue("owningextensiontypecode", value);
                OnPropertyChanged(nameof(OwningExtensionTypeCode));
            }
        }

		/// <summary>
		/// Unique identifier of the team who owns the record.
		/// </summary>
		[AttributeLogicalName("owningteam")]
        public EntityReference? OwningTeam
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<EntityReference?>("owningteam");
            }
        }

		/// <summary>
		/// Unique identifier of the user who owns the record.
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

		
		[AttributeLogicalName("parentpluginexecutionid")]
        public Guid? ParentPluginExecutionId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<Guid?>("parentpluginexecutionid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(ParentPluginExecutionId));
                SetAttributeValue("parentpluginexecutionid", value);
                OnPropertyChanged(nameof(ParentPluginExecutionId));
            }
        }

		/// <summary>
		/// Indicates whether the system job should run only after the specified date and time.
		/// </summary>
		[AttributeLogicalName("postponeuntil")]
        public DateTime? PostponeUntil
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<DateTime?>("postponeuntil");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(PostponeUntil));
                SetAttributeValue("postponeuntil", value);
                OnPropertyChanged(nameof(PostponeUntil));
            }
        }

		/// <summary>
		/// Type of entity with which the system job is primarily associated.
		/// </summary>
		[AttributeLogicalName("primaryentitytype")]
        public string? PrimaryEntityType
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("primaryentitytype");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(PrimaryEntityType));
                SetAttributeValue("primaryentitytype", value);
                OnPropertyChanged(nameof(PrimaryEntityType));
            }
        }

		/// <summary>
		/// Pattern of the system job's recurrence.
		/// </summary>
		[AttributeLogicalName("recurrencepattern")]
        public string? RecurrencePattern
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("recurrencepattern");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(RecurrencePattern));
                SetAttributeValue("recurrencepattern", value);
                OnPropertyChanged(nameof(RecurrencePattern));
            }
        }

		/// <summary>
		/// Starting time in UTC for the recurrence pattern.
		/// </summary>
		[AttributeLogicalName("recurrencestarttime")]
        public DateTime? RecurrenceStartTime
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<DateTime?>("recurrencestarttime");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(RecurrenceStartTime));
                SetAttributeValue("recurrencestarttime", value);
                OnPropertyChanged(nameof(RecurrenceStartTime));
            }
        }

		/// <summary>
		/// Unique identifier of the object with which the system job is associated.
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

		
		[AttributeLogicalName("regardingobjectidname")]
        public string? RegardingObjectIdName
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("regardingobjectidname");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(RegardingObjectIdName));
                SetAttributeValue("regardingobjectidname", value);
                OnPropertyChanged(nameof(RegardingObjectIdName));
            }
        }

		
		[AttributeLogicalName("regardingobjectidyominame")]
        public string? RegardingObjectIdYomiName
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("regardingobjectidyominame");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(RegardingObjectIdYomiName));
                SetAttributeValue("regardingobjectidyominame", value);
                OnPropertyChanged(nameof(RegardingObjectIdYomiName));
            }
        }

		
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
		/// Unique identifier of the request that generated the system job.
		/// </summary>
		[AttributeLogicalName("requestid")]
        public Guid? RequestId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<Guid?>("requestid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(RequestId));
                SetAttributeValue("requestid", value);
                OnPropertyChanged(nameof(RequestId));
            }
        }

		/// <summary>
		/// Retain job history.
		/// </summary>
		[AttributeLogicalName("retainjobhistory")]
        public bool? RetainJobHistory
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<bool?>("retainjobhistory");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(RetainJobHistory));
                SetAttributeValue("retainjobhistory", value);
                OnPropertyChanged(nameof(RetainJobHistory));
            }
        }

		/// <summary>
		/// Number of times to retry the system job.
		/// </summary>
		[AttributeLogicalName("retrycount")]
        public int? RetryCount
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<int?>("retrycount");
            }
        }

		/// <summary>
		/// Root execution context of the job that trigerred async job.
		/// </summary>
		[AttributeLogicalName("rootexecutioncontext")]
        public string? RootExecutionContext
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("rootexecutioncontext");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(RootExecutionContext));
                SetAttributeValue("rootexecutioncontext", value);
                OnPropertyChanged(nameof(RootExecutionContext));
            }
        }

		/// <summary>
		/// Order in which operations were submitted.
		/// </summary>
		[AttributeLogicalName("sequence")]
        public long? Sequence
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<long?>("sequence");
            }
        }

		/// <summary>
		/// Date and time when the system job was started.
		/// </summary>
		[AttributeLogicalName("startedon")]
        public DateTime? StartedOn
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<DateTime?>("startedon");
            }
        }

		/// <summary>
		/// Status of the system job.
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
		/// Reason for the status of the system job.
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
		/// The Subtype of the Async Job
		/// </summary>
		[AttributeLogicalName("subtype")]
        public int? Subtype
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<int?>("subtype");
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
		/// Unique identifier of the workflow activation related to the system job.
		/// </summary>
		[AttributeLogicalName("workflowactivationid")]
        public EntityReference? WorkflowActivationId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<EntityReference?>("workflowactivationid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(WorkflowActivationId));
                SetAttributeValue("workflowactivationid", value);
                OnPropertyChanged(nameof(WorkflowActivationId));
            }
        }

		/// <summary>
		/// Name of a workflow stage.
		/// </summary>
		[AttributeLogicalName("workflowstagename")]
        public string? WorkflowStageName
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("workflowstagename");
            }
        }

		/// <summary>
		/// The workload name.
		/// </summary>
		[AttributeLogicalName("workload")]
        public string? Workload
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("workload");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(Workload));
                SetAttributeValue("workload", value);
                OnPropertyChanged(nameof(Workload));
            }
        }


		#endregion

		#region NavigationProperties
		#endregion

		#region Options
		public static class Options
		{
                public struct IsWaitingForEvent
                {
                    public const bool No = false;
                    public const bool Yes = true;
                }
			    public struct OperationType
                {
					public const int SystemEvent = 1;
					public const int BulkEmail = 2;
					public const int ImportFileParse = 3;
					public const int TransformParseData = 4;
					public const int Import = 5;
					public const int ActivityPropagation = 6;
					public const int DuplicateDetectionRulePublish = 7;
					public const int BulkDuplicateDetection = 8;
					public const int SQMDataCollection = 9;
					public const int Workflow = 10;
					public const int QuickCampaign = 11;
					public const int MatchcodeUpdate = 12;
					public const int BulkDelete = 13;
					public const int DeletionService = 14;
					public const int IndexManagement = 15;
					public const int CollectOrganizationStatistics = 16;
					public const int ImportSubprocess = 17;
					public const int CalculateOrganizationStorageSize = 18;
					public const int CollectOrganizationDatabaseStatistics = 19;
					public const int CollectionOrganizationSizeStatistics = 20;
					public const int DatabaseTuning = 21;
					public const int CalculateOrganizationMaximumStorageSize = 22;
					public const int BulkDeleteSubprocess = 23;
					public const int UpdateStatisticIntervals = 24;
					public const int OrganizationFullTextCatalogIndex = 25;
					public const int DatabaseLogBackup = 26;
					public const int UpdateContractStates = 27;
					public const int DBCCSHRINKDATABASEMaintenanceJob = 28;
					public const int DBCCSHRINKFILEMaintenanceJob = 29;
					public const int ReindexAllIndicesMaintenanceJob = 30;
					public const int StorageLimitNotification = 31;
					public const int CleanupInactiveWorkflowAssemblies = 32;
					public const int RecurringSeriesExpansion = 35;
					public const int ImportSampleData = 38;
					public const int GoalRollUp = 40;
					public const int AuditPartitionCreation = 41;
					public const int CheckForLanguagePackUpdates = 42;
					public const int ProvisionLanguagePack = 43;
					public const int UpdateOrganizationDatabase = 44;
					public const int UpdateSolution = 45;
					public const int RegenerateEntityRowCountSnapshotData = 46;
					public const int RegenerateReadShareSnapshotData = 47;
					public const int PostToYammer = 49;
					public const int OutgoingActivity = 50;
					public const int IncomingEmailProcessing = 51;
					public const int MailboxTestAccess = 52;
					public const int EncryptionHealthCheck = 53;
					public const int ExecuteAsyncRequest = 54;
					public const int UpdateEntitlementStates = 56;
					public const int CalculateRollupField = 57;
					public const int MassCalculateRollupField = 58;
					public const int ImportTranslation = 59;
					public const int ConvertDateAndTimeBehavior = 62;
					public const int EntityKeyIndexCreation = 63;
					public const int UpdateKnowledgeArticleStates = 65;
					public const int ResourceBookingSync = 68;
					public const int RelationshipAssistantCards = 69;
					public const int CleanupSolutionComponents = 71;
					public const int AppModuleMetadataOperation = 72;
					public const int ALMAnomalyDetectionOperation = 73;
					public const int FlowNotification = 75;
					public const int RibbonClientMetadataOperation = 76;
					public const int CallbackRegistrationExpanderOperation = 79;
					public const int MigrateNotesToAttachmentsJob = 85;
					public const int MigrateArticleContentToFileStorage = 86;
					public const int UpdatedDeactivedOnForResolvedCasesJob = 87;
					public const int CascadeAssign = 90;
					public const int CascadeDelete = 91;
					public const int EventExpanderOperation = 92;
					public const int ImportSolutionMetadata = 93;
					public const int BulkDeleteFileAttachment = 94;
					public const int RefreshBusinessUnitForRecordsOwnedByPrincipal = 95;
					public const int RevokeInheritedAccess = 96;
					public const int CreateOrRefreshVirtualEntity = 98;
					public const int CascadeFlowSessionPermissionsAsyncOperation = 100;
					public const int UpdateModernFlowAsyncOperation = 101;
					public const int AsyncArchiveAsyncOperation = 102;
					public const int CancelAsyncOperations_System_ = 103;
					public const int ProvisionLanguageForUser = 201;
					public const int ExportSolutionAsyncOperation = 202;
					public const int ImportSolutionAsyncOperation = 203;
					public const int PublishAllAsyncOperation = 204;
					public const int DeleteAndPromoteAsyncOperation = 207;
					public const int UninstallSolutionAsyncOperation = 208;
					public const int ProvisionLanguageAsyncOperation = 209;
					public const int ImportTranslationAsyncOperation = 210;
					public const int StageAndUpgradeAsyncOperation = 211;
					public const int DenormalizationAsyncOperation = 239;
					public const int RefreshRuntimeIntegrationComponentsAsyncOperation = 250;
					public const int BulkArchiveOperation = 300;
					public const int ArchiveExecutionAsyncOperation = 301;
					public const int FinOpsDeploymentAsyncOperation = 302;
					public const int PurgeArchivedContentOperation = 304;
					public const int RegisterOfferingAsyncOperation = 305;
					public const int ExecuteDataProcessingConfiguration = 306;
					public const int SyncSynapseTablesSchema = 307;
					public const int FinOpsDBSyncAsyncOperation = 308;
					public const int FinOpsUnitTestAsyncOperation = 309;
					public const int CatalogServiceGeneratePackageAsyncOperation = 320;
					public const int CatalogServiceSubmitApprovalRequestAsyncOperation = 321;
					public const int CatalogServiceInstallRequestAsyncOperation = 322;
					public const int CascadeGrantOrRevokeAccessVersionTrackingAsyncOperation = 12801;
					public const int AIBuilderTrainingEvents = 190690091;
					public const int AIBuilderPredictionEvents = 190690092;
                }
                public struct RetainJobHistory
                {
                    public const bool No = false;
                    public const bool Yes = true;
                }
                public struct StateCode
                {
					public const int Ready = 0;
					public const int Suspended = 1;
					public const int Locked = 2;
					public const int Completed = 3;
                }
                public struct StatusCode
                {
					public const int WaitingForResources = 0;
					public const int Waiting = 10;
					public const int InProgress = 20;
					public const int Pausing = 21;
					public const int Canceling = 22;
					public const int Succeeded = 30;
					public const int Failed = 31;
					public const int Canceled = 32;
                }
		}
		#endregion

		#region LogicalNames
		public static class LogicalNames
		{
				public const string AsyncOperationId = "asyncoperationid";
				public const string BreadcrumbId = "breadcrumbid";
				public const string CallerOrigin = "callerorigin";
				public const string CompletedOn = "completedon";
				public const string CorrelationId = "correlationid";
				public const string CorrelationUpdatedTime = "correlationupdatedtime";
				public const string CreatedBy = "createdby";
				public const string CreatedOn = "createdon";
				public const string CreatedOnBehalfBy = "createdonbehalfby";
				public const string Data = "data";
				public const string DataBlobId = "datablobid";
				public const string DependencyToken = "dependencytoken";
				public const string Depth = "depth";
				public const string ErrorCode = "errorcode";
				public const string ExecutionTimeSpan = "executiontimespan";
				public const string ExpanderStartTime = "expanderstarttime";
				public const string FriendlyMessage = "friendlymessage";
				public const string HostId = "hostid";
				public const string IsWaitingForEvent = "iswaitingforevent";
				public const string Message = "message";
				public const string MessageName = "messagename";
				public const string ModifiedBy = "modifiedby";
				public const string ModifiedOn = "modifiedon";
				public const string ModifiedOnBehalfBy = "modifiedonbehalfby";
				public const string Name = "name";
				public const string OperationType = "operationtype";
				public const string OwnerId = "ownerid";
				public const string OwnerIdType = "owneridtype";
				public const string OwningBusinessUnit = "owningbusinessunit";
				public const string OwningExtensionId = "owningextensionid";
				public const string OwningExtensionIdName = "owningextensionidname";
				public const string OwningExtensionTypeCode = "owningextensiontypecode";
				public const string OwningTeam = "owningteam";
				public const string OwningUser = "owninguser";
				public const string ParentPluginExecutionId = "parentpluginexecutionid";
				public const string PostponeUntil = "postponeuntil";
				public const string PrimaryEntityType = "primaryentitytype";
				public const string RecurrencePattern = "recurrencepattern";
				public const string RecurrenceStartTime = "recurrencestarttime";
				public const string RegardingObjectId = "regardingobjectid";
				public const string RegardingObjectIdName = "regardingobjectidname";
				public const string RegardingObjectIdYomiName = "regardingobjectidyominame";
				public const string RegardingObjectTypeCode = "regardingobjecttypecode";
				public const string RequestId = "requestid";
				public const string RetainJobHistory = "retainjobhistory";
				public const string RetryCount = "retrycount";
				public const string RootExecutionContext = "rootexecutioncontext";
				public const string Sequence = "sequence";
				public const string StartedOn = "startedon";
				public const string StateCode = "statecode";
				public const string StatusCode = "statuscode";
				public const string Subtype = "subtype";
				public const string TimeZoneRuleVersionNumber = "timezoneruleversionnumber";
				public const string UTCConversionTimeZoneCode = "utcconversiontimezonecode";
				public const string WorkflowActivationId = "workflowactivationid";
				public const string WorkflowStageName = "workflowstagename";
				public const string Workload = "workload";
		}
		#endregion

		#region Relations
        public static class Relations
        {
            public static class OneToMany
            {
				public const string AsyncOperationBulkDeleteOperation = "AsyncOperation_BulkDeleteOperation";
				public const string AsyncOperationDuplicateBaseRecord = "AsyncOperation_DuplicateBaseRecord";
				public const string AsyncOperationEmails = "AsyncOperation_Emails";
				public const string AsyncoperationFileAttachments = "asyncoperation_FileAttachments";
				public const string AsyncOperationMailboxTrackingFolder = "AsyncOperation_MailboxTrackingFolder";
				public const string AsyncOperationSocialActivities = "AsyncOperation_SocialActivities";
				public const string AsyncoperationWorkflowwaitsubscription = "asyncoperation_workflowwaitsubscription";
				public const string LkWorkflowlogAsyncoperationChildworkflow = "lk_workflowlog_asyncoperation_childworkflow";
				public const string LkWorkflowlogAsyncoperations = "lk_workflowlog_asyncoperations";
				public const string UserentityinstancedataAsyncoperation = "userentityinstancedata_asyncoperation";
            }

            public static class ManyToOne
            {
				public const string AccountAsyncOperations = "Account_AsyncOperations";
				public const string AccountleadsAsyncOperations = "accountleads_AsyncOperations";
				public const string ActivityfileattachmentAsyncOperations = "activityfileattachment_AsyncOperations";
				public const string ActivityMimeAttachmentAsyncOperations = "ActivityMimeAttachment_AsyncOperations";
				public const string ActivitymonitorAsyncOperations = "activitymonitor_AsyncOperations";
				public const string ActivityPointerAsyncOperations = "ActivityPointer_AsyncOperations";
				public const string AdminsettingsentityAsyncOperations = "adminsettingsentity_AsyncOperations";
				public const string AnnotationAsyncOperations = "Annotation_AsyncOperations";
				public const string AnnualFiscalCalendarAsyncOperations = "AnnualFiscalCalendar_AsyncOperations";
				public const string AppactionAsyncOperations = "appaction_AsyncOperations";
				public const string AppactionmigrationAsyncOperations = "appactionmigration_AsyncOperations";
				public const string AppactionruleAsyncOperations = "appactionrule_AsyncOperations";
				public const string AppelementAsyncOperations = "appelement_AsyncOperations";
				public const string ApplicationuserAsyncOperations = "applicationuser_AsyncOperations";
				public const string AppmodulecomponentedgeAsyncOperations = "appmodulecomponentedge_AsyncOperations";
				public const string AppmodulecomponentnodeAsyncOperations = "appmodulecomponentnode_AsyncOperations";
				public const string AppointmentAsyncOperations = "Appointment_AsyncOperations";
				public const string AppsettingAsyncOperations = "appsetting_AsyncOperations";
				public const string AppusersettingAsyncOperations = "appusersetting_AsyncOperations";
				public const string AttributeimageconfigAsyncOperations = "attributeimageconfig_AsyncOperations";
				public const string AttributeMapAsyncOperations = "AttributeMap_AsyncOperations";
				public const string BookableresourceAsyncOperations = "bookableresource_AsyncOperations";
				public const string BookableresourcebookingAsyncOperations = "bookableresourcebooking_AsyncOperations";
				public const string BookableresourcebookingexchangesyncidmappingAsyncOperations = "bookableresourcebookingexchangesyncidmapping_AsyncOperations";
				public const string BookableresourcebookingheaderAsyncOperations = "bookableresourcebookingheader_AsyncOperations";
				public const string BookableresourcecategoryAsyncOperations = "bookableresourcecategory_AsyncOperations";
				public const string BookableresourcecategoryassnAsyncOperations = "bookableresourcecategoryassn_AsyncOperations";
				public const string BookableresourcecharacteristicAsyncOperations = "bookableresourcecharacteristic_AsyncOperations";
				public const string BookableresourcegroupAsyncOperations = "bookableresourcegroup_AsyncOperations";
				public const string BookingstatusAsyncOperations = "bookingstatus_AsyncOperations";
				public const string BotAsyncOperations = "bot_AsyncOperations";
				public const string BotcomponentAsyncOperations = "botcomponent_AsyncOperations";
				public const string BulkOperationAsyncOperations = "BulkOperation_AsyncOperations";
				public const string BulkOperationLogAsyncOperations = "BulkOperationLog_AsyncOperations";
				public const string BusinessUnitAsyncoperation = "business_unit_asyncoperation";
				public const string BusinessUnitAsyncOperations = "BusinessUnit_AsyncOperations";
				public const string BusinessUnitNewsArticleAsyncOperations = "BusinessUnitNewsArticle_AsyncOperations";
				public const string CalendarAsyncOperations = "Calendar_AsyncOperations";
				public const string CampaignAsyncOperations = "Campaign_AsyncOperations";
				public const string CampaignActivityAsyncOperations = "CampaignActivity_AsyncOperations";
				public const string CampaignactivityitemAsyncOperations = "campaignactivityitem_AsyncOperations";
				public const string CampaignitemAsyncOperations = "campaignitem_AsyncOperations";
				public const string CampaignResponseAsyncOperations = "CampaignResponse_AsyncOperations";
				public const string CanvasappextendedmetadataAsyncOperations = "canvasappextendedmetadata_AsyncOperations";
				public const string CardAsyncOperations = "card_AsyncOperations";
				public const string CascadegrantrevokeaccessrecordstrackerAsyncOperations = "cascadegrantrevokeaccessrecordstracker_AsyncOperations";
				public const string CascadegrantrevokeaccessversiontrackerAsyncOperations = "cascadegrantrevokeaccessversiontracker_AsyncOperations";
				public const string CatalogAsyncOperations = "catalog_AsyncOperations";
				public const string CatalogassignmentAsyncOperations = "catalogassignment_AsyncOperations";
				public const string ChannelaccessprofileAsyncOperations = "channelaccessprofile_AsyncOperations";
				public const string CharacteristicAsyncOperations = "characteristic_AsyncOperations";
				public const string ChatAsyncOperations = "chat_AsyncOperations";
				public const string ChildincidentcountAsyncOperations = "childincidentcount_AsyncOperations";
				public const string CommentAsyncOperations = "comment_AsyncOperations";
				public const string CommitmentAsyncOperations = "commitment_AsyncOperations";
				public const string CompetitorAsyncOperations = "Competitor_AsyncOperations";
				public const string CompetitoraddressAsyncOperations = "competitoraddress_AsyncOperations";
				public const string CompetitorproductAsyncOperations = "competitorproduct_AsyncOperations";
				public const string CompetitorsalesliteratureAsyncOperations = "competitorsalesliterature_AsyncOperations";
				public const string ConnectionAsyncOperations = "Connection_AsyncOperations";
				public const string ConnectionRoleAsyncOperations = "Connection_Role_AsyncOperations";
				public const string ConnectionreferenceAsyncOperations = "connectionreference_AsyncOperations";
				public const string ConnectorAsyncOperations = "connector_AsyncOperations";
				public const string ConstraintBasedGroupAsyncOperations = "ConstraintBasedGroup_AsyncOperations";
				public const string ContactAsyncOperations = "Contact_AsyncOperations";
				public const string ContactinvoicesAsyncOperations = "contactinvoices_AsyncOperations";
				public const string ContactleadsAsyncOperations = "contactleads_AsyncOperations";
				public const string ContactordersAsyncOperations = "contactorders_AsyncOperations";
				public const string ContactquotesAsyncOperations = "contactquotes_AsyncOperations";
				public const string ContractAsyncOperations = "Contract_AsyncOperations";
				public const string ContractDetailAsyncOperations = "ContractDetail_AsyncOperations";
				public const string ContractTemplateAsyncOperations = "ContractTemplate_AsyncOperations";
				public const string ConversationtranscriptAsyncOperations = "conversationtranscript_AsyncOperations";
				public const string ConvertruleAsyncOperations = "Convertrule_AsyncOperations";
				public const string CustomapiAsyncOperations = "customapi_AsyncOperations";
				public const string CustomapirequestparameterAsyncOperations = "customapirequestparameter_AsyncOperations";
				public const string CustomapiresponsepropertyAsyncOperations = "customapiresponseproperty_AsyncOperations";
				public const string CustomerAddressAsyncOperations = "CustomerAddress_AsyncOperations";
				public const string CustomerOpportunityRoleAsyncOperations = "CustomerOpportunityRole_AsyncOperations";
				public const string CustomerRelationshipAsyncOperations = "CustomerRelationship_AsyncOperations";
				public const string DatalakefolderAsyncOperations = "datalakefolder_AsyncOperations";
				public const string DatalakefolderpermissionAsyncOperations = "datalakefolderpermission_AsyncOperations";
				public const string DatalakeworkspaceAsyncOperations = "datalakeworkspace_AsyncOperations";
				public const string DatalakeworkspacepermissionAsyncOperations = "datalakeworkspacepermission_AsyncOperations";
				public const string DataprocessingconfigurationAsyncOperations = "dataprocessingconfiguration_AsyncOperations";
				public const string DesktopflowbinaryAsyncOperations = "desktopflowbinary_AsyncOperations";
				public const string DesktopflowmoduleAsyncOperations = "desktopflowmodule_AsyncOperations";
				public const string DiscountAsyncOperations = "Discount_AsyncOperations";
				public const string DiscountTypeAsyncOperations = "DiscountType_AsyncOperations";
				public const string DisplayStringAsyncOperations = "DisplayString_AsyncOperations";
				public const string DynamicpropertyAsyncOperations = "dynamicproperty_AsyncOperations";
				public const string DynamicpropertyassociationAsyncOperations = "dynamicpropertyassociation_AsyncOperations";
				public const string DynamicpropertyinstanceAsyncOperations = "dynamicpropertyinstance_AsyncOperations";
				public const string DynamicpropertyoptionsetitemAsyncOperations = "dynamicpropertyoptionsetitem_AsyncOperations";
				public const string Ec4uAcquirelegalbasisAsyncOperations = "ec4u_acquirelegalbasis_AsyncOperations";
				public const string Ec4uCarrierAsyncOperations = "ec4u_carrier_AsyncOperations";
				public const string Ec4uCarrierDependencyCheckAsyncOperations = "ec4u_carrier_dependency_check_AsyncOperations";
				public const string Ec4uCarrierMissingDependencyAsyncOperations = "ec4u_carrier_missing_dependency_AsyncOperations";
				public const string Ec4uGdprBpfCorrectionAsyncOperations = "ec4u_gdpr_bpf_correction_AsyncOperations";
				public const string Ec4uGdprBpfDeletionAsyncOperations = "ec4u_gdpr_bpf_deletion_AsyncOperations";
				public const string Ec4uGdprBpfInformationAsyncOperations = "ec4u_gdpr_bpf_information_AsyncOperations";
				public const string Ec4uGdprConfigEntityAsyncOperations = "ec4u_gdpr_config_entity_AsyncOperations";
				public const string Ec4uGdprConfigFieldAsyncOperations = "ec4u_gdpr_config_field_AsyncOperations";
				public const string Ec4uGdprConfigHierarchyAsyncOperations = "ec4u_gdpr_config_hierarchy_AsyncOperations";
				public const string Ec4uGdprProtocolAsyncOperations = "ec4u_gdpr_protocol_AsyncOperations";
				public const string Ec4uGdprProtocolDetailAsyncOperations = "ec4u_gdpr_protocol_detail_AsyncOperations";
				public const string Ec4uGdprReportAsyncOperations = "ec4u_gdpr_report_AsyncOperations";
				public const string Ec4uGdprRequestAsyncOperations = "ec4u_gdpr_request_AsyncOperations";
				public const string Ec4uLegalbasisAsyncOperations = "ec4u_legalbasis_AsyncOperations";
				public const string Ec4uLegalbasistypeAsyncOperations = "ec4u_legalbasistype_AsyncOperations";
				public const string Ec4uWorkbenchAsyncOperations = "ec4u_workbench_AsyncOperations";
				public const string Ec4uWorkbenchHistoryAsyncOperations = "ec4u_workbench_history_AsyncOperations";
				public const string EmailAsyncOperations = "Email_AsyncOperations";
				public const string EmailserverprofileAsyncoperations = "emailserverprofile_asyncoperations";
				public const string EntitlementAsyncOperations = "entitlement_AsyncOperations";
				public const string EntitlementchannelAsyncOperations = "entitlementchannel_AsyncOperations";
				public const string EntitlementcontactsAsyncOperations = "entitlementcontacts_AsyncOperations";
				public const string EntitlemententityallocationtypemappingAsyncOperations = "entitlemententityallocationtypemapping_AsyncOperations";
				public const string EntitlementproductsAsyncOperations = "entitlementproducts_AsyncOperations";
				public const string EntitlementtemplateAsyncOperations = "entitlementtemplate_AsyncOperations";
				public const string EntitlementtemplatechannelAsyncOperations = "entitlementtemplatechannel_AsyncOperations";
				public const string EntitlementtemplateproductsAsyncOperations = "entitlementtemplateproducts_AsyncOperations";
				public const string EntityanalyticsconfigAsyncOperations = "entityanalyticsconfig_AsyncOperations";
				public const string EntityimageconfigAsyncOperations = "entityimageconfig_AsyncOperations";
				public const string EntityindexAsyncOperations = "entityindex_AsyncOperations";
				public const string EntityMapAsyncOperations = "EntityMap_AsyncOperations";
				public const string EntityrecordfilterAsyncOperations = "entityrecordfilter_AsyncOperations";
				public const string EnvironmentvariabledefinitionAsyncOperations = "environmentvariabledefinition_AsyncOperations";
				public const string EnvironmentvariablevalueAsyncOperations = "environmentvariablevalue_AsyncOperations";
				public const string EquipmentAsyncOperations = "Equipment_AsyncOperations";
				public const string ExportedexcelAsyncOperations = "exportedexcel_AsyncOperations";
				public const string ExportsolutionuploadAsyncOperations = "exportsolutionupload_AsyncOperations";
				public const string ExternalpartyAsyncOperations = "externalparty_AsyncOperations";
				public const string ExternalpartyitemAsyncOperations = "externalpartyitem_AsyncOperations";
				public const string FaxAsyncOperations = "Fax_AsyncOperations";
				public const string FeaturecontrolsettingAsyncOperations = "featurecontrolsetting_AsyncOperations";
				public const string FileAttachmentAsyncOperationDataBlobId = "FileAttachment_AsyncOperation_DataBlobId";
				public const string FixedMonthlyFiscalCalendarAsyncOperations = "FixedMonthlyFiscalCalendar_AsyncOperations";
				public const string FlowmachineAsyncOperations = "flowmachine_AsyncOperations";
				public const string FlowmachinegroupAsyncOperations = "flowmachinegroup_AsyncOperations";
				public const string FlowmachineimageAsyncOperations = "flowmachineimage_AsyncOperations";
				public const string FlowmachineimageversionAsyncOperations = "flowmachineimageversion_AsyncOperations";
				public const string FlowmachinenetworkAsyncOperations = "flowmachinenetwork_AsyncOperations";
				public const string FlowsessionAsyncOperations = "flowsession_AsyncOperations";
				public const string GoalAsyncOperations = "Goal_AsyncOperations";
				public const string GoalrollupqueryAsyncOperations = "goalrollupquery_AsyncOperations";
				public const string HolidaywrapperAsyncOperations = "holidaywrapper_AsyncOperations";
				public const string ImportAsyncOperations = "Import_AsyncOperations";
				public const string ImportDataAsyncOperations = "ImportData_AsyncOperations";
				public const string ImportFileAsyncOperations = "ImportFile_AsyncOperations";
				public const string ImportLogAsyncOperations = "ImportLog_AsyncOperations";
				public const string ImportMapAsyncOperations = "ImportMap_AsyncOperations";
				public const string IncidentAsyncOperations = "Incident_AsyncOperations";
				public const string IncidentknowledgebaserecordAsyncOperations = "incidentknowledgebaserecord_AsyncOperations";
				public const string IncidentResolutionAsyncOperations = "IncidentResolution_AsyncOperations";
				public const string IndexattributesAsyncOperations = "indexattributes_AsyncOperations";
				public const string InteractionforemailAsyncOperations = "interactionforemail_AsyncOperations";
				public const string InternalcatalogassignmentAsyncOperations = "internalcatalogassignment_AsyncOperations";
				public const string InvoiceAsyncOperations = "Invoice_AsyncOperations";
				public const string InvoiceDetailAsyncOperations = "InvoiceDetail_AsyncOperations";
				public const string IsvConfigAsyncOperations = "IsvConfig_AsyncOperations";
				public const string KbArticleAsyncOperations = "KbArticle_AsyncOperations";
				public const string KbArticleCommentAsyncOperations = "KbArticleComment_AsyncOperations";
				public const string KbArticleTemplateAsyncOperations = "KbArticleTemplate_AsyncOperations";
				public const string KeyvaultreferenceAsyncOperations = "keyvaultreference_AsyncOperations";
				public const string KnowledgearticleAsyncOperations = "knowledgearticle_AsyncOperations";
				public const string KnowledgearticleincidentAsyncOperations = "knowledgearticleincident_AsyncOperations";
				public const string KnowledgeBaseRecordAsyncOperations = "KnowledgeBaseRecord_AsyncOperations";
				public const string LeadAsyncOperations = "Lead_AsyncOperations";
				public const string LeadaddressAsyncOperations = "leadaddress_AsyncOperations";
				public const string LeadcompetitorsAsyncOperations = "leadcompetitors_AsyncOperations";
				public const string LeadproductAsyncOperations = "leadproduct_AsyncOperations";
				public const string LeadtoopportunitysalesprocessAsyncOperations = "leadtoopportunitysalesprocess_AsyncOperations";
				public const string LetterAsyncOperations = "Letter_AsyncOperations";
				public const string ListAsyncOperations = "List_AsyncOperations";
				public const string ListmemberAsyncOperations = "listmember_AsyncOperations";
				public const string ListoperationAsyncOperations = "listoperation_AsyncOperations";
				public const string LkAsyncoperationCreatedby = "lk_asyncoperation_createdby";
				public const string LkAsyncoperationCreatedonbehalfby = "lk_asyncoperation_createdonbehalfby";
				public const string LkAsyncoperationModifiedby = "lk_asyncoperation_modifiedby";
				public const string LkAsyncoperationModifiedonbehalfby = "lk_asyncoperation_modifiedonbehalfby";
				public const string LkAsyncoperationWorkflowactivationid = "lk_asyncoperation_workflowactivationid";
				public const string MailboxAsyncoperations = "mailbox_asyncoperations";
				public const string MailMergeTemplateAsyncOperations = "MailMergeTemplate_AsyncOperations";
				public const string ManagedidentityAsyncOperations = "managedidentity_AsyncOperations";
				public const string MarketingformdisplayattributesAsyncOperations = "marketingformdisplayattributes_AsyncOperations";
				public const string MetricAsyncOperations = "metric_AsyncOperations";
				public const string MobileofflineprofileextensionAsyncOperations = "mobileofflineprofileextension_AsyncOperations";
				public const string MonthlyFiscalCalendarAsyncOperations = "MonthlyFiscalCalendar_AsyncOperations";
				public const string MsdynAccountkpiitemAsyncOperations = "msdyn_accountkpiitem_AsyncOperations";
				public const string MsdynActioncardregardingAsyncOperations = "msdyn_actioncardregarding_AsyncOperations";
				public const string MsdynActioncardrolesettingAsyncOperations = "msdyn_actioncardrolesetting_AsyncOperations";
				public const string MsdynActivityanalysiscleanupstateAsyncOperations = "msdyn_activityanalysiscleanupstate_AsyncOperations";
				public const string MsdynActivityanalysisconfigAsyncOperations = "msdyn_activityanalysisconfig_AsyncOperations";
				public const string MsdynAdaptivecardconfigurationAsyncOperations = "msdyn_adaptivecardconfiguration_AsyncOperations";
				public const string MsdynAdminappstateAsyncOperations = "msdyn_adminappstate_AsyncOperations";
				public const string MsdynAgentcapacityupdatehistoryAsyncOperations = "msdyn_agentcapacityupdatehistory_AsyncOperations";
				public const string MsdynAgentresourceforecastingAsyncOperations = "msdyn_agentresourceforecasting_AsyncOperations";
				public const string MsdynAgentstatushistoryAsyncOperations = "msdyn_agentstatushistory_AsyncOperations";
				public const string MsdynAibdatasetAsyncOperations = "msdyn_aibdataset_AsyncOperations";
				public const string MsdynAibdatasetfileAsyncOperations = "msdyn_aibdatasetfile_AsyncOperations";
				public const string MsdynAibdatasetrecordAsyncOperations = "msdyn_aibdatasetrecord_AsyncOperations";
				public const string MsdynAibdatasetscontainerAsyncOperations = "msdyn_aibdatasetscontainer_AsyncOperations";
				public const string MsdynAibfeedbackloopAsyncOperations = "msdyn_aibfeedbackloop_AsyncOperations";
				public const string MsdynAibfileAsyncOperations = "msdyn_aibfile_AsyncOperations";
				public const string MsdynAibfileattacheddataAsyncOperations = "msdyn_aibfileattacheddata_AsyncOperations";
				public const string MsdynAiconfigurationAsyncOperations = "msdyn_aiconfiguration_AsyncOperations";
				public const string MsdynAicontactsuggestionAsyncOperations = "msdyn_aicontactsuggestion_AsyncOperations";
				public const string MsdynAifptrainingdocumentAsyncOperations = "msdyn_aifptrainingdocument_AsyncOperations";
				public const string MsdynAimodelAsyncOperations = "msdyn_aimodel_AsyncOperations";
				public const string MsdynAiodimageAsyncOperations = "msdyn_aiodimage_AsyncOperations";
				public const string MsdynAiodlabelAsyncOperations = "msdyn_aiodlabel_AsyncOperations";
				public const string MsdynAiodtrainingboundingboxAsyncOperations = "msdyn_aiodtrainingboundingbox_AsyncOperations";
				public const string MsdynAiodtrainingimageAsyncOperations = "msdyn_aiodtrainingimage_AsyncOperations";
				public const string MsdynAitemplateAsyncOperations = "msdyn_aitemplate_AsyncOperations";
				public const string MsdynAnalysiscomponentAsyncOperations = "msdyn_analysiscomponent_AsyncOperations";
				public const string MsdynAnalysisjobAsyncOperations = "msdyn_analysisjob_AsyncOperations";
				public const string MsdynAnalysisresultAsyncOperations = "msdyn_analysisresult_AsyncOperations";
				public const string MsdynAnalysisresultdetailAsyncOperations = "msdyn_analysisresultdetail_AsyncOperations";
				public const string MsdynAnalyticsAsyncOperations = "msdyn_analytics_AsyncOperations";
				public const string MsdynAnalyticsadminsettingsAsyncOperations = "msdyn_analyticsadminsettings_AsyncOperations";
				public const string MsdynAnalyticsforcsAsyncOperations = "msdyn_analyticsforcs_AsyncOperations";
				public const string MsdynAppconfigurationAsyncOperations = "msdyn_appconfiguration_AsyncOperations";
				public const string MsdynAppinsightsmetadataAsyncOperations = "msdyn_appinsightsmetadata_AsyncOperations";
				public const string MsdynApplicationextensionAsyncOperations = "msdyn_applicationextension_AsyncOperations";
				public const string MsdynApplicationtabtemplateAsyncOperations = "msdyn_applicationtabtemplate_AsyncOperations";
				public const string MsdynAppprofilerolemappingAsyncOperations = "msdyn_appprofilerolemapping_AsyncOperations";
				public const string MsdynAssetcategorytemplateassociationAsyncOperations = "msdyn_assetcategorytemplateassociation_AsyncOperations";
				public const string MsdynAssettemplateassociationAsyncOperations = "msdyn_assettemplateassociation_AsyncOperations";
				public const string MsdynAssignmentconfigurationAsyncOperations = "msdyn_assignmentconfiguration_AsyncOperations";
				public const string MsdynAssignmentconfigurationstepAsyncOperations = "msdyn_assignmentconfigurationstep_AsyncOperations";
				public const string MsdynAssignmentmapAsyncOperations = "msdyn_assignmentmap_AsyncOperations";
				public const string MsdynAssignmentruleAsyncOperations = "msdyn_assignmentrule_AsyncOperations";
				public const string MsdynAttributeAsyncOperations = "msdyn_attribute_AsyncOperations";
				public const string MsdynAttributeinfluencestatisticsAsyncOperations = "msdyn_attributeinfluencestatistics_AsyncOperations";
				public const string MsdynAttributevalueAsyncOperations = "msdyn_attributevalue_AsyncOperations";
				public const string MsdynAuthenticationsettingsAsyncOperations = "msdyn_authenticationsettings_AsyncOperations";
				public const string MsdynAuthsettingsentryAsyncOperations = "msdyn_authsettingsentry_AsyncOperations";
				public const string MsdynAutocaptureruleAsyncOperations = "msdyn_autocapturerule_AsyncOperations";
				public const string MsdynAutocapturesettingsAsyncOperations = "msdyn_autocapturesettings_AsyncOperations";
				public const string MsdynBookableresourcecapacityprofileAsyncOperations = "msdyn_bookableresourcecapacityprofile_AsyncOperations";
				public const string MsdynCallablecontextAsyncOperations = "msdyn_callablecontext_AsyncOperations";
				public const string MsdynCannedmessageAsyncOperations = "msdyn_cannedmessage_AsyncOperations";
				public const string MsdynCapacityprofileAsyncOperations = "msdyn_capacityprofile_AsyncOperations";
				public const string MsdynCaseenrichmentAsyncOperations = "msdyn_caseenrichment_AsyncOperations";
				public const string MsdynCasesuggestionrequestpayloadAsyncOperations = "msdyn_casesuggestionrequestpayload_AsyncOperations";
				public const string MsdynCasetopicAsyncOperations = "msdyn_casetopic_AsyncOperations";
				public const string MsdynCasetopicIncidentAsyncOperations = "msdyn_casetopic_incident_AsyncOperations";
				public const string MsdynCasetopicsettingAsyncOperations = "msdyn_casetopicsetting_AsyncOperations";
				public const string MsdynCasetopicsummaryAsyncOperations = "msdyn_casetopicsummary_AsyncOperations";
				public const string MsdynCdsentityengagementctxAsyncOperations = "msdyn_cdsentityengagementctx_AsyncOperations";
				public const string MsdynChannelcapabilityAsyncOperations = "msdyn_channelcapability_AsyncOperations";
				public const string MsdynChanneldefinitionAsyncOperations = "msdyn_channeldefinition_AsyncOperations";
				public const string MsdynChanneldefinitionconsentAsyncOperations = "msdyn_channeldefinitionconsent_AsyncOperations";
				public const string MsdynChanneldefinitionlocaleAsyncOperations = "msdyn_channeldefinitionlocale_AsyncOperations";
				public const string MsdynChannelinstanceAsyncOperations = "msdyn_channelinstance_AsyncOperations";
				public const string MsdynChannelinstanceaccountAsyncOperations = "msdyn_channelinstanceaccount_AsyncOperations";
				public const string MsdynChannelmessagepartAsyncOperations = "msdyn_channelmessagepart_AsyncOperations";
				public const string MsdynChannelproviderAsyncOperations = "msdyn_channelprovider_AsyncOperations";
				public const string MsdynCiproviderAsyncOperations = "msdyn_ciprovider_AsyncOperations";
				public const string MsdynCollabgraphresourceAsyncOperations = "msdyn_collabgraphresource_AsyncOperations";
				public const string MsdynCollabspaceteamassociationAsyncOperations = "msdyn_collabspaceteamassociation_AsyncOperations";
				public const string MsdynConsoleapplicationnotificationfieldAsyncOperations = "msdyn_consoleapplicationnotificationfield_AsyncOperations";
				public const string MsdynConsoleapplicationnotificationtemplateAsyncOperations = "msdyn_consoleapplicationnotificationtemplate_AsyncOperations";
				public const string MsdynConsoleapplicationsessiontemplateAsyncOperations = "msdyn_consoleapplicationsessiontemplate_AsyncOperations";
				public const string MsdynConsoleapplicationtemplateAsyncOperations = "msdyn_consoleapplicationtemplate_AsyncOperations";
				public const string MsdynConsoleapplicationtemplateparameterAsyncOperations = "msdyn_consoleapplicationtemplateparameter_AsyncOperations";
				public const string MsdynConsoleapplicationtypeAsyncOperations = "msdyn_consoleapplicationtype_AsyncOperations";
				public const string MsdynConsoleappparameterdefinitionAsyncOperations = "msdyn_consoleappparameterdefinition_AsyncOperations";
				public const string MsdynConsumingapplicationAsyncOperations = "msdyn_consumingapplication_AsyncOperations";
				public const string MsdynContactkpiitemAsyncOperations = "msdyn_contactkpiitem_AsyncOperations";
				public const string MsdynContactsuggestionruleAsyncOperations = "msdyn_contactsuggestionrule_AsyncOperations";
				public const string MsdynContactsuggestionrulesetAsyncOperations = "msdyn_contactsuggestionruleset_AsyncOperations";
				public const string MsdynConversationactionAsyncOperations = "msdyn_conversationaction_AsyncOperations";
				public const string MsdynConversationactionlocaleAsyncOperations = "msdyn_conversationactionlocale_AsyncOperations";
				public const string MsdynConversationdataAsyncOperations = "msdyn_conversationdata_AsyncOperations";
				public const string MsdynConversationinsightAsyncOperations = "msdyn_conversationinsight_AsyncOperations";
				public const string MsdynConversationmessageblockAsyncOperations = "msdyn_conversationmessageblock_AsyncOperations";
				public const string MsdynCrmconnectionAsyncOperations = "msdyn_crmconnection_AsyncOperations";
				public const string MsdynCsadminconfigAsyncOperations = "msdyn_csadminconfig_AsyncOperations";
				public const string MsdynCustomapirulesetconfigurationAsyncOperations = "msdyn_customapirulesetconfiguration_AsyncOperations";
				public const string MsdynCustomcontrolextendedsettingsAsyncOperations = "msdyn_customcontrolextendedsettings_AsyncOperations";
				public const string MsdynCustomerassetAsyncOperations = "msdyn_customerasset_AsyncOperations";
				public const string MsdynCustomerassetattachmentAsyncOperations = "msdyn_customerassetattachment_AsyncOperations";
				public const string MsdynCustomerassetcategoryAsyncOperations = "msdyn_customerassetcategory_AsyncOperations";
				public const string MsdynCustomeremailcommunicationAsyncOperations = "msdyn_customeremailcommunication_AsyncOperations";
				public const string MsdynDailyaccountkpiitemAsyncOperations = "msdyn_dailyaccountkpiitem_AsyncOperations";
				public const string MsdynDailycontactkpiitemAsyncOperations = "msdyn_dailycontactkpiitem_AsyncOperations";
				public const string MsdynDailyleadkpiitemAsyncOperations = "msdyn_dailyleadkpiitem_AsyncOperations";
				public const string MsdynDailyopportunitykpiitemAsyncOperations = "msdyn_dailyopportunitykpiitem_AsyncOperations";
				public const string MsdynDataanalyticscustomizedreportAsyncOperations = "msdyn_dataanalyticscustomizedreport_AsyncOperations";
				public const string MsdynDataanalyticsdatasetAsyncOperations = "msdyn_dataanalyticsdataset_AsyncOperations";
				public const string MsdynDataanalyticsreportAsyncOperations = "msdyn_dataanalyticsreport_AsyncOperations";
				public const string MsdynDataanalyticsreportCsrmanagerAsyncOperations = "msdyn_dataanalyticsreport_csrmanager_AsyncOperations";
				public const string MsdynDataanalyticsreportForecastAsyncOperations = "msdyn_dataanalyticsreport_forecast_AsyncOperations";
				public const string MsdynDataanalyticsreportKsinsightsAsyncOperations = "msdyn_dataanalyticsreport_ksinsights_AsyncOperations";
				public const string MsdynDataanalyticsreportSutreportingAsyncOperations = "msdyn_dataanalyticsreport_sutreporting_AsyncOperations";
				public const string MsdynDataanalyticsworkspaceAsyncOperations = "msdyn_dataanalyticsworkspace_AsyncOperations";
				public const string MsdynDatabaseversionAsyncOperations = "msdyn_databaseversion_AsyncOperations";
				public const string MsdynDataflowAsyncOperations = "msdyn_dataflow_AsyncOperations";
				public const string MsdynDataflowrefreshhistoryAsyncOperations = "msdyn_dataflowrefreshhistory_AsyncOperations";
				public const string MsdynDatahygienesettinginfoAsyncOperations = "msdyn_datahygienesettinginfo_AsyncOperations";
				public const string MsdynDatainsightsandanalyticsfeatureAsyncOperations = "msdyn_datainsightsandanalyticsfeature_AsyncOperations";
				public const string MsdynDealmanageraccessAsyncOperations = "msdyn_dealmanageraccess_AsyncOperations";
				public const string MsdynDealmanagersettingsAsyncOperations = "msdyn_dealmanagersettings_AsyncOperations";
				public const string MsdynDecisioncontractAsyncOperations = "msdyn_decisioncontract_AsyncOperations";
				public const string MsdynDecisionrulesetAsyncOperations = "msdyn_decisionruleset_AsyncOperations";
				public const string MsdynDefextendedchannelinstanceAsyncOperations = "msdyn_defextendedchannelinstance_AsyncOperations";
				public const string MsdynDefextendedchannelinstanceaccountAsyncOperations = "msdyn_defextendedchannelinstanceaccount_AsyncOperations";
				public const string MsdynDeletedconversationAsyncOperations = "msdyn_deletedconversation_AsyncOperations";
				public const string MsdynDigitalsellingactivetaskAsyncOperations = "msdyn_digitalsellingactivetask_AsyncOperations";
				public const string MsdynDigitalsellingcompletedtaskAsyncOperations = "msdyn_digitalsellingcompletedtask_AsyncOperations";
				public const string MsdynDistributedlockAsyncOperations = "msdyn_distributedlock_AsyncOperations";
				public const string MsdynDuplicatedetectionpluginrunAsyncOperations = "msdyn_duplicatedetectionpluginrun_AsyncOperations";
				public const string MsdynDuplicateleadmappingAsyncOperations = "msdyn_duplicateleadmapping_AsyncOperations";
				public const string MsdynEffortpredictionresultAsyncOperations = "msdyn_effortpredictionresult_AsyncOperations";
				public const string MsdynEntityconfigAsyncOperations = "msdyn_entityconfig_AsyncOperations";
				public const string MsdynEntitylinkchatconfigurationAsyncOperations = "msdyn_entitylinkchatconfiguration_AsyncOperations";
				public const string MsdynEntityrankingruleAsyncOperations = "msdyn_entityrankingrule_AsyncOperations";
				public const string MsdynEntityrefreshhistoryAsyncOperations = "msdyn_entityrefreshhistory_AsyncOperations";
				public const string MsdynEntityroutingconfigurationAsyncOperations = "msdyn_entityroutingconfiguration_AsyncOperations";
				public const string MsdynExtendedusersettingAsyncOperations = "msdyn_extendedusersetting_AsyncOperations";
				public const string MsdynFavoriteknowledgearticleAsyncOperations = "msdyn_favoriteknowledgearticle_AsyncOperations";
				public const string MsdynFederatedarticleAsyncOperations = "msdyn_federatedarticle_AsyncOperations";
				public const string MsdynFederatedarticleincidentAsyncOperations = "msdyn_federatedarticleincident_AsyncOperations";
				public const string MsdynFlowcardtypeAsyncOperations = "msdyn_flowcardtype_AsyncOperations";
				public const string MsdynForecastconfigurationAsyncOperations = "msdyn_forecastconfiguration_AsyncOperations";
				public const string MsdynForecastdefinitionAsyncOperations = "msdyn_forecastdefinition_AsyncOperations";
				public const string MsdynForecastingcacheAsyncOperations = "msdyn_forecastingcache_AsyncOperations";
				public const string MsdynForecastinstanceAsyncOperations = "msdyn_forecastinstance_AsyncOperations";
				public const string MsdynForecastpredictionstatusAsyncOperations = "msdyn_forecastpredictionstatus_AsyncOperations";
				public const string MsdynForecastrecurrenceAsyncOperations = "msdyn_forecastrecurrence_AsyncOperations";
				public const string MsdynForecastsettingsandsummaryAsyncOperations = "msdyn_forecastsettingsandsummary_AsyncOperations";
				public const string MsdynFunctionallocationAsyncOperations = "msdyn_functionallocation_AsyncOperations";
				public const string MsdynGdprdataAsyncOperations = "msdyn_gdprdata_AsyncOperations";
				public const string MsdynHelppageAsyncOperations = "msdyn_helppage_AsyncOperations";
				public const string MsdynIcebreakersconfigAsyncOperations = "msdyn_icebreakersconfig_AsyncOperations";
				public const string MsdynIermlmodelAsyncOperations = "msdyn_iermlmodel_AsyncOperations";
				public const string MsdynIermltrainingAsyncOperations = "msdyn_iermltraining_AsyncOperations";
				public const string MsdynInboxconfigurationAsyncOperations = "msdyn_inboxconfiguration_AsyncOperations";
				public const string MsdynInsightsstorevirtualentityAsyncOperations = "msdyn_insightsstorevirtualentity_AsyncOperations";
				public const string MsdynIntegratedsearchproviderAsyncOperations = "msdyn_integratedsearchprovider_AsyncOperations";
				public const string MsdynIotalertAsyncOperations = "msdyn_iotalert_AsyncOperations";
				public const string MsdynIotdeviceAsyncOperations = "msdyn_iotdevice_AsyncOperations";
				public const string MsdynIotdevicecategoryAsyncOperations = "msdyn_iotdevicecategory_AsyncOperations";
				public const string MsdynIotdevicecommandAsyncOperations = "msdyn_iotdevicecommand_AsyncOperations";
				public const string MsdynIotdevicecommanddefinitionAsyncOperations = "msdyn_iotdevicecommanddefinition_AsyncOperations";
				public const string MsdynIotdevicedatahistoryAsyncOperations = "msdyn_iotdevicedatahistory_AsyncOperations";
				public const string MsdynIotdevicepropertyAsyncOperations = "msdyn_iotdeviceproperty_AsyncOperations";
				public const string MsdynIotdeviceregistrationhistoryAsyncOperations = "msdyn_iotdeviceregistrationhistory_AsyncOperations";
				public const string MsdynIotdevicevisualizationconfigurationAsyncOperations = "msdyn_iotdevicevisualizationconfiguration_AsyncOperations";
				public const string MsdynIotfieldmappingAsyncOperations = "msdyn_iotfieldmapping_AsyncOperations";
				public const string MsdynIotpropertydefinitionAsyncOperations = "msdyn_iotpropertydefinition_AsyncOperations";
				public const string MsdynIotproviderAsyncOperations = "msdyn_iotprovider_AsyncOperations";
				public const string MsdynIotproviderinstanceAsyncOperations = "msdyn_iotproviderinstance_AsyncOperations";
				public const string MsdynIotsettingsAsyncOperations = "msdyn_iotsettings_AsyncOperations";
				public const string MsdynIottocaseprocessAsyncOperations = "msdyn_iottocaseprocess_AsyncOperations";
				public const string MsdynKalanguagesettingAsyncOperations = "msdyn_kalanguagesetting_AsyncOperations";
				public const string MsdynKbattachmentAsyncOperations = "msdyn_kbattachment_AsyncOperations";
				public const string MsdynKbenrichmentAsyncOperations = "msdyn_kbenrichment_AsyncOperations";
				public const string MsdynKbkeywordsdescsuggestionsettingAsyncOperations = "msdyn_kbkeywordsdescsuggestionsetting_AsyncOperations";
				public const string MsdynKmfederatedsearchconfigAsyncOperations = "msdyn_kmfederatedsearchconfig_AsyncOperations";
				public const string MsdynKmpersonalizationsettingAsyncOperations = "msdyn_kmpersonalizationsetting_AsyncOperations";
				public const string MsdynKnowledgearticleimageAsyncOperations = "msdyn_knowledgearticleimage_AsyncOperations";
				public const string MsdynKnowledgearticletemplateAsyncOperations = "msdyn_knowledgearticletemplate_AsyncOperations";
				public const string MsdynKnowledgeconfigurationAsyncOperations = "msdyn_knowledgeconfiguration_AsyncOperations";
				public const string MsdynKnowledgeinteractioninsightAsyncOperations = "msdyn_knowledgeinteractioninsight_AsyncOperations";
				public const string MsdynKnowledgemanagementsettingAsyncOperations = "msdyn_knowledgemanagementsetting_AsyncOperations";
				public const string MsdynKnowledgepersonalfilterAsyncOperations = "msdyn_knowledgepersonalfilter_AsyncOperations";
				public const string MsdynKnowledgesearchfilterAsyncOperations = "msdyn_knowledgesearchfilter_AsyncOperations";
				public const string MsdynKnowledgesearchinsightAsyncOperations = "msdyn_knowledgesearchinsight_AsyncOperations";
				public const string MsdynKpieventdataAsyncOperations = "msdyn_kpieventdata_AsyncOperations";
				public const string MsdynKpieventdefinitionAsyncOperations = "msdyn_kpieventdefinition_AsyncOperations";
				public const string MsdynLeadhygienesettingAsyncOperations = "msdyn_leadhygienesetting_AsyncOperations";
				public const string MsdynLeadkpiitemAsyncOperations = "msdyn_leadkpiitem_AsyncOperations";
				public const string MsdynLeadmodelconfigAsyncOperations = "msdyn_leadmodelconfig_AsyncOperations";
				public const string MsdynLinkedentityattributevalidityAsyncOperations = "msdyn_linkedentityattributevalidity_AsyncOperations";
				public const string MsdynLiveconversationAsyncOperations = "msdyn_liveconversation_AsyncOperations";
				public const string MsdynLiveworkitemeventAsyncOperations = "msdyn_liveworkitemevent_AsyncOperations";
				public const string MsdynLiveworkstreamAsyncOperations = "msdyn_liveworkstream_AsyncOperations";
				public const string MsdynLiveworkstreamcapacityprofileAsyncOperations = "msdyn_liveworkstreamcapacityprofile_AsyncOperations";
				public const string MsdynMacrosessionAsyncOperations = "msdyn_macrosession_AsyncOperations";
				public const string MsdynMaskingruleAsyncOperations = "msdyn_maskingrule_AsyncOperations";
				public const string MsdynMasterentityroutingconfigurationAsyncOperations = "msdyn_masterentityroutingconfiguration_AsyncOperations";
				public const string MsdynMigrationtrackerAsyncOperations = "msdyn_migrationtracker_AsyncOperations";
				public const string MsdynModelpreviewstatusAsyncOperations = "msdyn_modelpreviewstatus_AsyncOperations";
				public const string MsdynMostcontactedAsyncOperations = "msdyn_mostcontacted_AsyncOperations";
				public const string MsdynMostcontactedbyAsyncOperations = "msdyn_mostcontactedby_AsyncOperations";
				public const string MsdynMsteamssettingAsyncOperations = "msdyn_msteamssetting_AsyncOperations";
				public const string MsdynMsteamssettingsv2AsyncOperations = "msdyn_msteamssettingsv2_AsyncOperations";
				public const string MsdynNotesanalysisconfigAsyncOperations = "msdyn_notesanalysisconfig_AsyncOperations";
				public const string MsdynNotificationfieldAsyncOperations = "msdyn_notificationfield_AsyncOperations";
				public const string MsdynNotificationtemplateAsyncOperations = "msdyn_notificationtemplate_AsyncOperations";
				public const string MsdynOcGeolocationproviderAsyncOperations = "msdyn_oc_geolocationprovider_AsyncOperations";
				public const string MsdynOcautoblockruleAsyncOperations = "msdyn_ocautoblockrule_AsyncOperations";
				public const string MsdynOcbotchannelregistrationAsyncOperations = "msdyn_ocbotchannelregistration_AsyncOperations";
				public const string MsdynOcbotchannelregistrationsecretAsyncOperations = "msdyn_ocbotchannelregistrationsecret_AsyncOperations";
				public const string MsdynOcchannelapiconversationprivilegeAsyncOperations = "msdyn_occhannelapiconversationprivilege_AsyncOperations";
				public const string MsdynOcchannelapimessageprivilegeAsyncOperations = "msdyn_occhannelapimessageprivilege_AsyncOperations";
				public const string MsdynOcchannelapimethodmappingAsyncOperations = "msdyn_occhannelapimethodmapping_AsyncOperations";
				public const string MsdynOcchannelconfigurationAsyncOperations = "msdyn_occhannelconfiguration_AsyncOperations";
				public const string MsdynOcchannelstateconfigurationAsyncOperations = "msdyn_occhannelstateconfiguration_AsyncOperations";
				public const string MsdynOcexternalcontextAsyncOperations = "msdyn_ocexternalcontext_AsyncOperations";
				public const string MsdynOcflaggedspamAsyncOperations = "msdyn_ocflaggedspam_AsyncOperations";
				public const string MsdynOclanguageAsyncOperations = "msdyn_oclanguage_AsyncOperations";
				public const string MsdynOcliveworkitemAsyncOperations = "msdyn_ocliveworkitem_AsyncOperations";
				public const string MsdynOcliveworkitemcapacityprofileAsyncOperations = "msdyn_ocliveworkitemcapacityprofile_AsyncOperations";
				public const string MsdynOcliveworkitemcharacteristicAsyncOperations = "msdyn_ocliveworkitemcharacteristic_AsyncOperations";
				public const string MsdynOcliveworkitemcontextitemAsyncOperations = "msdyn_ocliveworkitemcontextitem_AsyncOperations";
				public const string MsdynOcliveworkitemparticipantAsyncOperations = "msdyn_ocliveworkitemparticipant_AsyncOperations";
				public const string MsdynOcliveworkitemsentimentAsyncOperations = "msdyn_ocliveworkitemsentiment_AsyncOperations";
				public const string MsdynOcliveworkstreamcontextvariableAsyncOperations = "msdyn_ocliveworkstreamcontextvariable_AsyncOperations";
				public const string MsdynOclocalizationdataAsyncOperations = "msdyn_oclocalizationdata_AsyncOperations";
				public const string MsdynOcpaymentprofileAsyncOperations = "msdyn_ocpaymentprofile_AsyncOperations";
				public const string MsdynOcprovisioningstateAsyncOperations = "msdyn_ocprovisioningstate_AsyncOperations";
				public const string MsdynOcrecordingAsyncOperations = "msdyn_ocrecording_AsyncOperations";
				public const string MsdynOcrequestAsyncOperations = "msdyn_ocrequest_AsyncOperations";
				public const string MsdynOcrichobjectAsyncOperations = "msdyn_ocrichobject_AsyncOperations";
				public const string MsdynOcrichobjectmapAsyncOperations = "msdyn_ocrichobjectmap_AsyncOperations";
				public const string MsdynOcruleitemAsyncOperations = "msdyn_ocruleitem_AsyncOperations";
				public const string MsdynOcsentimentdailytopicAsyncOperations = "msdyn_ocsentimentdailytopic_AsyncOperations";
				public const string MsdynOcsentimentdailytopickeywordAsyncOperations = "msdyn_ocsentimentdailytopickeyword_AsyncOperations";
				public const string MsdynOcsentimentdailytopictrendingAsyncOperations = "msdyn_ocsentimentdailytopictrending_AsyncOperations";
				public const string MsdynOcsessionAsyncOperations = "msdyn_ocsession_AsyncOperations";
				public const string MsdynOcsessioncharacteristicAsyncOperations = "msdyn_ocsessioncharacteristic_AsyncOperations";
				public const string MsdynOcsessionparticipanteventAsyncOperations = "msdyn_ocsessionparticipantevent_AsyncOperations";
				public const string MsdynOcsessionsentimentAsyncOperations = "msdyn_ocsessionsentiment_AsyncOperations";
				public const string MsdynOcsimltrainingAsyncOperations = "msdyn_ocsimltraining_AsyncOperations";
				public const string MsdynOcsitdimportconfigAsyncOperations = "msdyn_ocsitdimportconfig_AsyncOperations";
				public const string MsdynOcsitdskillAsyncOperations = "msdyn_ocsitdskill_AsyncOperations";
				public const string MsdynOcsitrainingdataAsyncOperations = "msdyn_ocsitrainingdata_AsyncOperations";
				public const string MsdynOcskillidentmlmodelAsyncOperations = "msdyn_ocskillidentmlmodel_AsyncOperations";
				public const string MsdynOcsystemmessageAsyncOperations = "msdyn_ocsystemmessage_AsyncOperations";
				public const string MsdynOctagAsyncOperations = "msdyn_octag_AsyncOperations";
				public const string MsdynOmnichannelconfigurationAsyncOperations = "msdyn_omnichannelconfiguration_AsyncOperations";
				public const string MsdynOmnichannelpersonalizationAsyncOperations = "msdyn_omnichannelpersonalization_AsyncOperations";
				public const string MsdynOmnichannelqueueAsyncOperations = "msdyn_omnichannelqueue_AsyncOperations";
				public const string MsdynOmnichannelsyncconfigAsyncOperations = "msdyn_omnichannelsyncconfig_AsyncOperations";
				public const string MsdynOperatinghourAsyncOperations = "msdyn_operatinghour_AsyncOperations";
				public const string MsdynOpportunitykpiitemAsyncOperations = "msdyn_opportunitykpiitem_AsyncOperations";
				public const string MsdynOpportunitymodelconfigAsyncOperations = "msdyn_opportunitymodelconfig_AsyncOperations";
				public const string MsdynOverflowactionconfigAsyncOperations = "msdyn_overflowactionconfig_AsyncOperations";
				public const string MsdynPaneconfigurationAsyncOperations = "msdyn_paneconfiguration_AsyncOperations";
				public const string MsdynPanetabconfigurationAsyncOperations = "msdyn_panetabconfiguration_AsyncOperations";
				public const string MsdynPanetoolconfigurationAsyncOperations = "msdyn_panetoolconfiguration_AsyncOperations";
				public const string MsdynPersonalmessageAsyncOperations = "msdyn_personalmessage_AsyncOperations";
				public const string MsdynPersonalsoundsettingAsyncOperations = "msdyn_personalsoundsetting_AsyncOperations";
				public const string MsdynPersonasecurityrolemappingAsyncOperations = "msdyn_personasecurityrolemapping_AsyncOperations";
				public const string MsdynPlaybookactivityAsyncOperations = "msdyn_playbookactivity_AsyncOperations";
				public const string MsdynPlaybookactivityattributeAsyncOperations = "msdyn_playbookactivityattribute_AsyncOperations";
				public const string MsdynPlaybookcategoryAsyncOperations = "msdyn_playbookcategory_AsyncOperations";
				public const string MsdynPlaybookinstanceAsyncOperations = "msdyn_playbookinstance_AsyncOperations";
				public const string MsdynPlaybooktemplateAsyncOperations = "msdyn_playbooktemplate_AsyncOperations";
				public const string MsdynPmanalysishistoryAsyncOperations = "msdyn_pmanalysishistory_AsyncOperations";
				public const string MsdynPmcalendarAsyncOperations = "msdyn_pmcalendar_AsyncOperations";
				public const string MsdynPmcalendarversionAsyncOperations = "msdyn_pmcalendarversion_AsyncOperations";
				public const string MsdynPminferredtaskAsyncOperations = "msdyn_pminferredtask_AsyncOperations";
				public const string MsdynPmprocessextendedmetadataversionAsyncOperations = "msdyn_pmprocessextendedmetadataversion_AsyncOperations";
				public const string MsdynPmprocesstemplateAsyncOperations = "msdyn_pmprocesstemplate_AsyncOperations";
				public const string MsdynPmprocessusersettingsAsyncOperations = "msdyn_pmprocessusersettings_AsyncOperations";
				public const string MsdynPmprocessversionAsyncOperations = "msdyn_pmprocessversion_AsyncOperations";
				public const string MsdynPmrecordingAsyncOperations = "msdyn_pmrecording_AsyncOperations";
				public const string MsdynPmtemplateAsyncOperations = "msdyn_pmtemplate_AsyncOperations";
				public const string MsdynPmviewAsyncOperations = "msdyn_pmview_AsyncOperations";
				public const string MsdynPostalbumAsyncOperations = "msdyn_postalbum_AsyncOperations";
				public const string MsdynPostconfigAsyncOperations = "msdyn_postconfig_AsyncOperations";
				public const string MsdynPostruleconfigAsyncOperations = "msdyn_postruleconfig_AsyncOperations";
				public const string MsdynPredictivemodelscoreAsyncOperations = "msdyn_predictivemodelscore_AsyncOperations";
				public const string MsdynPredictivescoreAsyncOperations = "msdyn_predictivescore_AsyncOperations";
				public const string MsdynPreferredagentAsyncOperations = "msdyn_preferredagent_AsyncOperations";
				public const string MsdynPreferredagentcustomeridentityAsyncOperations = "msdyn_preferredagentcustomeridentity_AsyncOperations";
				public const string MsdynPreferredagentroutedentityAsyncOperations = "msdyn_preferredagentroutedentity_AsyncOperations";
				public const string MsdynPresenceAsyncOperations = "msdyn_presence_AsyncOperations";
				public const string MsdynProductivityactioninputparameterAsyncOperations = "msdyn_productivityactioninputparameter_AsyncOperations";
				public const string MsdynProductivityactionoutputparameterAsyncOperations = "msdyn_productivityactionoutputparameter_AsyncOperations";
				public const string MsdynProductivityagentscriptAsyncOperations = "msdyn_productivityagentscript_AsyncOperations";
				public const string MsdynProductivityagentscriptstepAsyncOperations = "msdyn_productivityagentscriptstep_AsyncOperations";
				public const string MsdynProductivitymacroactiontemplateAsyncOperations = "msdyn_productivitymacroactiontemplate_AsyncOperations";
				public const string MsdynProductivitymacroconnectorAsyncOperations = "msdyn_productivitymacroconnector_AsyncOperations";
				public const string MsdynProductivitymacrosolutionconfigurationAsyncOperations = "msdyn_productivitymacrosolutionconfiguration_AsyncOperations";
				public const string MsdynProductivityparameterdefinitionAsyncOperations = "msdyn_productivityparameterdefinition_AsyncOperations";
				public const string MsdynPropertyAsyncOperations = "msdyn_property_AsyncOperations";
				public const string MsdynPropertyassetassociationAsyncOperations = "msdyn_propertyassetassociation_AsyncOperations";
				public const string MsdynPropertylogAsyncOperations = "msdyn_propertylog_AsyncOperations";
				public const string MsdynPropertytemplateassociationAsyncOperations = "msdyn_propertytemplateassociation_AsyncOperations";
				public const string MsdynProviderAsyncOperations = "msdyn_provider_AsyncOperations";
				public const string MsdynRealtimescoringAsyncOperations = "msdyn_realtimescoring_AsyncOperations";
				public const string MsdynRecomputetrackerAsyncOperations = "msdyn_recomputetracker_AsyncOperations";
				public const string MsdynRecordingAsyncOperations = "msdyn_recording_AsyncOperations";
				public const string MsdynRecurringsalesactionAsyncOperations = "msdyn_recurringsalesaction_AsyncOperations";
				public const string MsdynRelationshipinsightsunifiedconfigAsyncOperations = "msdyn_relationshipinsightsunifiedconfig_AsyncOperations";
				public const string MsdynReportbookmarkAsyncOperations = "msdyn_reportbookmark_AsyncOperations";
				public const string MsdynRichtextfileAsyncOperations = "msdyn_richtextfile_AsyncOperations";
				public const string MsdynRoutingconfigurationAsyncOperations = "msdyn_routingconfiguration_AsyncOperations";
				public const string MsdynRoutingconfigurationstepAsyncOperations = "msdyn_routingconfigurationstep_AsyncOperations";
				public const string MsdynRoutingrequestAsyncOperations = "msdyn_routingrequest_AsyncOperations";
				public const string MsdynRoutingrulesetsettingAsyncOperations = "msdyn_routingrulesetsetting_AsyncOperations";
				public const string MsdynRulesetdependencymappingAsyncOperations = "msdyn_rulesetdependencymapping_AsyncOperations";
				public const string MsdynSabackupdiagnosticAsyncOperations = "msdyn_sabackupdiagnostic_AsyncOperations";
				public const string MsdynSabatchruninstanceAsyncOperations = "msdyn_sabatchruninstance_AsyncOperations";
				public const string MsdynSalesaccelerationinsightAsyncOperations = "msdyn_salesaccelerationinsight_AsyncOperations";
				public const string MsdynSalesaccelerationsettingsAsyncOperations = "msdyn_salesaccelerationsettings_AsyncOperations";
				public const string MsdynSalesassignmentsettingAsyncOperations = "msdyn_salesassignmentsetting_AsyncOperations";
				public const string MsdynSalesinsightssettingsAsyncOperations = "msdyn_salesinsightssettings_AsyncOperations";
				public const string MsdynSalesocmessageAsyncOperations = "msdyn_salesocmessage_AsyncOperations";
				public const string MsdynSalesocsmstemplateAsyncOperations = "msdyn_salesocsmstemplate_AsyncOperations";
				public const string MsdynSalesroutingdiagnosticAsyncOperations = "msdyn_salesroutingdiagnostic_AsyncOperations";
				public const string MsdynSalesroutingrunAsyncOperations = "msdyn_salesroutingrun_AsyncOperations";
				public const string MsdynSalessuggestionAsyncOperations = "msdyn_salessuggestion_AsyncOperations";
				public const string MsdynSalestagAsyncOperations = "msdyn_salestag_AsyncOperations";
				public const string MsdynSaruninstanceAsyncOperations = "msdyn_saruninstance_AsyncOperations";
				public const string MsdynSearchconfigurationAsyncOperations = "msdyn_searchconfiguration_AsyncOperations";
				public const string MsdynSegmentAsyncOperations = "msdyn_segment_AsyncOperations";
				public const string MsdynSegmentationsettingAsyncOperations = "msdyn_segmentationsetting_AsyncOperations";
				public const string MsdynSegmentattributeAsyncOperations = "msdyn_segmentattribute_AsyncOperations";
				public const string MsdynSegmentcatalogueAsyncOperations = "msdyn_segmentcatalogue_AsyncOperations";
				public const string MsdynSentimentanalysisAsyncOperations = "msdyn_sentimentanalysis_AsyncOperations";
				public const string MsdynSequenceAsyncOperations = "msdyn_sequence_AsyncOperations";
				public const string MsdynSequencestatAsyncOperations = "msdyn_sequencestat_AsyncOperations";
				public const string MsdynSequencetargetAsyncOperations = "msdyn_sequencetarget_AsyncOperations";
				public const string MsdynSequencetargetstepAsyncOperations = "msdyn_sequencetargetstep_AsyncOperations";
				public const string MsdynSequencetemplateAsyncOperations = "msdyn_sequencetemplate_AsyncOperations";
				public const string MsdynServiceconfigurationAsyncOperations = "msdyn_serviceconfiguration_AsyncOperations";
				public const string MsdynServiceoneprovisioningrequestAsyncOperations = "msdyn_serviceoneprovisioningrequest_AsyncOperations";
				public const string MsdynSessiondataAsyncOperations = "msdyn_sessiondata_AsyncOperations";
				public const string MsdynSessioneventAsyncOperations = "msdyn_sessionevent_AsyncOperations";
				public const string MsdynSessionparticipantAsyncOperations = "msdyn_sessionparticipant_AsyncOperations";
				public const string MsdynSessionparticipantdataAsyncOperations = "msdyn_sessionparticipantdata_AsyncOperations";
				public const string MsdynSessiontemplateAsyncOperations = "msdyn_sessiontemplate_AsyncOperations";
				public const string MsdynShareasconfigurationAsyncOperations = "msdyn_shareasconfiguration_AsyncOperations";
				public const string MsdynSiconfigAsyncOperations = "msdyn_siconfig_AsyncOperations";
				public const string MsdynSikeyvalueconfigAsyncOperations = "msdyn_sikeyvalueconfig_AsyncOperations";
				public const string MsdynSimilarentitiesfeatureimportanceAsyncOperations = "msdyn_similarentitiesfeatureimportance_AsyncOperations";
				public const string MsdynSkillattachmentruleitemAsyncOperations = "msdyn_skillattachmentruleitem_AsyncOperations";
				public const string MsdynSkillattachmenttargetAsyncOperations = "msdyn_skillattachmenttarget_AsyncOperations";
				public const string MsdynSlakpiAsyncOperations = "msdyn_slakpi_AsyncOperations";
				public const string MsdynSmartassistconfigAsyncOperations = "msdyn_smartassistconfig_AsyncOperations";
				public const string MsdynSolutionhealthruleAsyncOperations = "msdyn_solutionhealthrule_AsyncOperations";
				public const string MsdynSolutionhealthruleargumentAsyncOperations = "msdyn_solutionhealthruleargument_AsyncOperations";
				public const string MsdynSolutionhealthrulesetAsyncOperations = "msdyn_solutionhealthruleset_AsyncOperations";
				public const string MsdynSoundfileAsyncOperations = "msdyn_soundfile_AsyncOperations";
				public const string MsdynSoundnotificationsettingAsyncOperations = "msdyn_soundnotificationsetting_AsyncOperations";
				public const string MsdynSuggestionassignmentruleAsyncOperations = "msdyn_suggestionassignmentrule_AsyncOperations";
				public const string MsdynSuggestioninteractionAsyncOperations = "msdyn_suggestioninteraction_AsyncOperations";
				public const string MsdynSuggestionprincipalobjectaccessAsyncOperations = "msdyn_suggestionprincipalobjectaccess_AsyncOperations";
				public const string MsdynSuggestionrequestpayloadAsyncOperations = "msdyn_suggestionrequestpayload_AsyncOperations";
				public const string MsdynSuggestionsellerpriorityAsyncOperations = "msdyn_suggestionsellerpriority_AsyncOperations";
				public const string MsdynSuggestionsmodelsummaryAsyncOperations = "msdyn_suggestionsmodelsummary_AsyncOperations";
				public const string MsdynSuggestionssettingAsyncOperations = "msdyn_suggestionssetting_AsyncOperations";
				public const string MsdynSwarmAsyncOperations = "msdyn_swarm_AsyncOperations";
				public const string MsdynSwarmparticipantAsyncOperations = "msdyn_swarmparticipant_AsyncOperations";
				public const string MsdynSwarmparticipantruleAsyncOperations = "msdyn_swarmparticipantrule_AsyncOperations";
				public const string MsdynSwarmroleAsyncOperations = "msdyn_swarmrole_AsyncOperations";
				public const string MsdynSwarmskillAsyncOperations = "msdyn_swarmskill_AsyncOperations";
				public const string MsdynSwarmtemplateAsyncOperations = "msdyn_swarmtemplate_AsyncOperations";
				public const string MsdynTaggedrecordAsyncOperations = "msdyn_taggedrecord_AsyncOperations";
				public const string MsdynTeamschatassociationAsyncOperations = "msdyn_teamschatassociation_AsyncOperations";
				public const string MsdynTeamschatsuggestionAsyncOperations = "msdyn_teamschatsuggestion_AsyncOperations";
				public const string MsdynTeamscollaborationAsyncOperations = "msdyn_teamscollaboration_AsyncOperations";
				public const string MsdynTeamsdialeradminsettingsAsyncOperations = "msdyn_teamsdialeradminsettings_AsyncOperations";
				public const string MsdynTemplateforpropertiesAsyncOperations = "msdyn_templateforproperties_AsyncOperations";
				public const string MsdynTemplateparameterAsyncOperations = "msdyn_templateparameter_AsyncOperations";
				public const string MsdynTemplatetagsAsyncOperations = "msdyn_templatetags_AsyncOperations";
				public const string MsdynTimespentAsyncOperations = "msdyn_timespent_AsyncOperations";
				public const string MsdynTourAsyncOperations = "msdyn_tour_AsyncOperations";
				public const string MsdynTranscriptAsyncOperations = "msdyn_transcript_AsyncOperations";
				public const string MsdynUnifiedroutingdiagnosticAsyncOperations = "msdyn_unifiedroutingdiagnostic_AsyncOperations";
				public const string MsdynUnifiedroutingrunAsyncOperations = "msdyn_unifiedroutingrun_AsyncOperations";
				public const string MsdynUnifiedroutingsetuptrackerAsyncOperations = "msdyn_unifiedroutingsetuptracker_AsyncOperations";
				public const string MsdynUntrackedappointmentAsyncOperations = "msdyn_untrackedappointment_AsyncOperations";
				public const string MsdynUpgraderunAsyncOperations = "msdyn_upgraderun_AsyncOperations";
				public const string MsdynUpgradestepAsyncOperations = "msdyn_upgradestep_AsyncOperations";
				public const string MsdynUpgradeversionAsyncOperations = "msdyn_upgradeversion_AsyncOperations";
				public const string MsdynUrnotificationtemplateAsyncOperations = "msdyn_urnotificationtemplate_AsyncOperations";
				public const string MsdynUrnotificationtemplatemappingAsyncOperations = "msdyn_urnotificationtemplatemapping_AsyncOperations";
				public const string MsdynUsagemetricAsyncOperations = "msdyn_usagemetric_AsyncOperations";
				public const string MsdynUsagereportingAsyncOperations = "msdyn_usagereporting_AsyncOperations";
				public const string MsdynUsersettingAsyncOperations = "msdyn_usersetting_AsyncOperations";
				public const string MsdynVirtualtablecolumncandidateAsyncOperations = "msdyn_virtualtablecolumncandidate_AsyncOperations";
				public const string MsdynVisitorjourneyAsyncOperations = "msdyn_visitorjourney_AsyncOperations";
				public const string MsdynVivacustomerlistAsyncOperations = "msdyn_vivacustomerlist_AsyncOperations";
				public const string MsdynVivaentitysettingAsyncOperations = "msdyn_vivaentitysetting_AsyncOperations";
				public const string MsdynVivaorgsettingAsyncOperations = "msdyn_vivaorgsetting_AsyncOperations";
				public const string MsdynVivausersettingAsyncOperations = "msdyn_vivausersetting_AsyncOperations";
				public const string MsdynWallsavedqueryAsyncOperations = "msdyn_wallsavedquery_AsyncOperations";
				public const string MsdynWallsavedqueryusersettingsAsyncOperations = "msdyn_wallsavedqueryusersettings_AsyncOperations";
				public const string MsdynWkwcolleaguesforcompanyAsyncOperations = "msdyn_wkwcolleaguesforcompany_AsyncOperations";
				public const string MsdynWkwcolleaguesforcontactAsyncOperations = "msdyn_wkwcolleaguesforcontact_AsyncOperations";
				public const string MsdynWkwconfigAsyncOperations = "msdyn_wkwconfig_AsyncOperations";
				public const string MsdynWorkflowactionstatusAsyncOperations = "msdyn_workflowactionstatus_AsyncOperations";
				public const string MsdynWorklistviewconfigurationAsyncOperations = "msdyn_worklistviewconfiguration_AsyncOperations";
				public const string MsdynWorkqueuestateAsyncOperations = "msdyn_workqueuestate_AsyncOperations";
				public const string MsdynWorkqueueusersettingAsyncOperations = "msdyn_workqueueusersetting_AsyncOperations";
				public const string MsdynceBotcontentAsyncOperations = "msdynce_botcontent_AsyncOperations";
				public const string MsdyncrmAddtocalendarstyleAsyncOperations = "msdyncrm_addtocalendarstyle_AsyncOperations";
				public const string MsdyncrmBasestyleAsyncOperations = "msdyncrm_basestyle_AsyncOperations";
				public const string MsdyncrmButtonstyleAsyncOperations = "msdyncrm_buttonstyle_AsyncOperations";
				public const string MsdyncrmCodestyleAsyncOperations = "msdyncrm_codestyle_AsyncOperations";
				public const string MsdyncrmColumnstyleAsyncOperations = "msdyncrm_columnstyle_AsyncOperations";
				public const string MsdyncrmContentblockstyleAsyncOperations = "msdyncrm_contentblockstyle_AsyncOperations";
				public const string MsdyncrmDividerstyleAsyncOperations = "msdyncrm_dividerstyle_AsyncOperations";
				public const string MsdyncrmGeneralstylesAsyncOperations = "msdyncrm_generalstyles_AsyncOperations";
				public const string MsdyncrmImagestyleAsyncOperations = "msdyncrm_imagestyle_AsyncOperations";
				public const string MsdyncrmLayoutstyleAsyncOperations = "msdyncrm_layoutstyle_AsyncOperations";
				public const string MsdyncrmQrcodestyleAsyncOperations = "msdyncrm_qrcodestyle_AsyncOperations";
				public const string MsdyncrmTextstyleAsyncOperations = "msdyncrm_textstyle_AsyncOperations";
				public const string MsdyncrmVideostyleAsyncOperations = "msdyncrm_videostyle_AsyncOperations";
				public const string MsdynmktCatalogeventstatusconfigurationAsyncOperations = "msdynmkt_catalogeventstatusconfiguration_AsyncOperations";
				public const string MsdynmktConfigurationAsyncOperations = "msdynmkt_configuration_AsyncOperations";
				public const string MsdynmktEventmetadataAsyncOperations = "msdynmkt_eventmetadata_AsyncOperations";
				public const string MsdynmktEventmetadataSdkmessageprocessingstepAsyncOperations = "msdynmkt_eventmetadata_sdkmessageprocessingstep_AsyncOperations";
				public const string MsdynmktEventparametermetadataAsyncOperations = "msdynmkt_eventparametermetadata_AsyncOperations";
				public const string MsdynmktExperimentv2AsyncOperations = "msdynmkt_experimentv2_AsyncOperations";
				public const string MsdynmktFeatureconfigurationAsyncOperations = "msdynmkt_featureconfiguration_AsyncOperations";
				public const string MsdynmktInfobipchannelinstanceAsyncOperations = "msdynmkt_infobipchannelinstance_AsyncOperations";
				public const string MsdynmktInfobipchannelinstanceaccountAsyncOperations = "msdynmkt_infobipchannelinstanceaccount_AsyncOperations";
				public const string MsdynmktLinkmobilitychannelinstanceAsyncOperations = "msdynmkt_linkmobilitychannelinstance_AsyncOperations";
				public const string MsdynmktLinkmobilitychannelinstanceaccountAsyncOperations = "msdynmkt_linkmobilitychannelinstanceaccount_AsyncOperations";
				public const string MsdynmktMetadataentityrelationshipAsyncOperations = "msdynmkt_metadataentityrelationship_AsyncOperations";
				public const string MsdynmktMetadataitemAsyncOperations = "msdynmkt_metadataitem_AsyncOperations";
				public const string MsdynmktMetadatastorestateAsyncOperations = "msdynmkt_metadatastorestate_AsyncOperations";
				public const string MsdynmktMocksmsproviderchannelinstanceAsyncOperations = "msdynmkt_mocksmsproviderchannelinstance_AsyncOperations";
				public const string MsdynmktMocksmsproviderchannelinstanceaccountAsyncOperations = "msdynmkt_mocksmsproviderchannelinstanceaccount_AsyncOperations";
				public const string MsdynmktPredefinedplaceholderAsyncOperations = "msdynmkt_predefinedplaceholder_AsyncOperations";
				public const string MsdynmktTelesignchannelinstanceAsyncOperations = "msdynmkt_telesignchannelinstance_AsyncOperations";
				public const string MsdynmktTelesignchannelinstanceaccountAsyncOperations = "msdynmkt_telesignchannelinstanceaccount_AsyncOperations";
				public const string MsdynmktTwiliochannelinstanceAsyncOperations = "msdynmkt_twiliochannelinstance_AsyncOperations";
				public const string MsdynmktTwiliochannelinstanceaccountAsyncOperations = "msdynmkt_twiliochannelinstanceaccount_AsyncOperations";
				public const string MsfpAlertAsyncOperations = "msfp_alert_AsyncOperations";
				public const string MsfpAlertruleAsyncOperations = "msfp_alertrule_AsyncOperations";
				public const string MsfpEmailtemplateAsyncOperations = "msfp_emailtemplate_AsyncOperations";
				public const string MsfpFileresponseAsyncOperations = "msfp_fileresponse_AsyncOperations";
				public const string MsfpLocalizedemailtemplateAsyncOperations = "msfp_localizedemailtemplate_AsyncOperations";
				public const string MsfpProjectAsyncOperations = "msfp_project_AsyncOperations";
				public const string MsfpQuestionAsyncOperations = "msfp_question_AsyncOperations";
				public const string MsfpQuestionresponseAsyncOperations = "msfp_questionresponse_AsyncOperations";
				public const string MsfpSatisfactionmetricAsyncOperations = "msfp_satisfactionmetric_AsyncOperations";
				public const string MsfpSurveyAsyncOperations = "msfp_survey_AsyncOperations";
				public const string MsfpSurveyinviteAsyncOperations = "msfp_surveyinvite_AsyncOperations";
				public const string MsfpSurveyreminderAsyncOperations = "msfp_surveyreminder_AsyncOperations";
				public const string MsfpSurveyresponseAsyncOperations = "msfp_surveyresponse_AsyncOperations";
				public const string MsfpUnsubscribedrecipientAsyncOperations = "msfp_unsubscribedrecipient_AsyncOperations";
				public const string MsgraphresourcetosubscriptionAsyncOperations = "msgraphresourcetosubscription_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaDef00953ba86dAsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_def_00953ba86d_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaDef0c22ae9e8eAsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_def_0c22ae9e8e_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaDef1c6c8bafd1AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_def_1c6c8bafd1_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaDef3430122a33AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_def_3430122a33_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaDef96c1eb3520AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_def_96c1eb3520_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaDefB1a182e9c8AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_def_b1a182e9c8_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaDefE3bf28df86AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_def_e3bf28df86_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaDefE5537d018aAsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_def_e5537d018a_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaDefEe71d1226aAsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_def_ee71d1226a_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaDefFfd19a5250AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_def_ffd19a5250_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT12062a29316AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t1_2062a29316_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT12a8bdbda8fAsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t1_2a8bdbda8f_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT13d3d3a827bAsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t1_3d3d3a827b_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT14b75f83914AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t1_4b75f83914_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT15c169ef238AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t1_5c169ef238_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT16f6e64d2d0AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t1_6f6e64d2d0_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT17071985af5AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t1_7071985af5_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT18e1ad69a9cAsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t1_8e1ad69a9c_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT1A8567fad71AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t1_a8567fad71_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT1E8ece5e3a4AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t1_e8ece5e3a4_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT10050c3cae86AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t10_050c3cae86_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT1005f8960225AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t10_05f8960225_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT10175176e791AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t10_175176e791_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT101ae9aba71bAsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t10_1ae9aba71b_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT1043904e4236AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t10_43904e4236_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT1046d78dc4c0AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t10_46d78dc4c0_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT10840de5be5bAsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t10_840de5be5b_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT10A65901ff6bAsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t10_a65901ff6b_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT10Bbe8491a4cAsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t10_bbe8491a4c_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT10C6216089e7AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t10_c6216089e7_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT11191d47da52AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t11_191d47da52_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT111aa2dd8353AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t11_1aa2dd8353_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT111f763c2b21AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t11_1f763c2b21_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT11355447c1b8AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t11_355447c1b8_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT1157154580deAsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t11_57154580de_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT11C15f04df88AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t11_c15f04df88_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT11C200febf0cAsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t11_c200febf0c_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT11C533d064d5AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t11_c533d064d5_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT11Eedec5d10cAsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t11_eedec5d10c_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT11Ff0deec922AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t11_ff0deec922_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT124171d43cc7AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t12_4171d43cc7_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT12542b499f6fAsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t12_542b499f6f_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT12670b0850c0AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t12_670b0850c0_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT127232d02acdAsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t12_7232d02acd_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT1277efa630feAsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t12_77efa630fe_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT1291855bb2f7AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t12_91855bb2f7_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT1292bc47a6ebAsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t12_92bc47a6eb_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT1296e6ba6bd1AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t12_96e6ba6bd1_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT12Bc43dc92b5AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t12_bc43dc92b5_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT12C1eef4d03aAsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t12_c1eef4d03a_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT20092e7e40aAsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t2_0092e7e40a_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT21225727811AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t2_1225727811_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT2164455c24aAsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t2_164455c24a_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT23d0d5f42e9AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t2_3d0d5f42e9_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT23e3ad1e11bAsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t2_3e3ad1e11b_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT25a2677fa0fAsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t2_5a2677fa0f_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT299118b1d5fAsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t2_99118b1d5f_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT2C78e121ee6AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t2_c78e121ee6_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT2Dbf5b76f03AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t2_dbf5b76f03_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT2E28fef2bbaAsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t2_e28fef2bba_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT30f1afbb350AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t3_0f1afbb350_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT32581b7e267AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t3_2581b7e267_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT36634415773AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t3_6634415773_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT36853dee4d2AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t3_6853dee4d2_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT36dcd947688AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t3_6dcd947688_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT380101a7e38AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t3_80101a7e38_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT3C21a1260acAsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t3_c21a1260ac_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT3C59ea92a3aAsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t3_c59ea92a3a_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT3D55f2fa8ddAsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t3_d55f2fa8dd_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT3D85dd82696AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t3_d85dd82696_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT4115d677022AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t4_115d677022_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT42468772e17AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t4_2468772e17_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT437c34150a1AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t4_37c34150a1_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT45113e1140cAsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t4_5113e1140c_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT471ab6e7243AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t4_71ab6e7243_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT4Ae02f22693AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t4_ae02f22693_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT4B867875e80AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t4_b867875e80_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT4Ba55d05beaAsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t4_ba55d05bea_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT4D94da8a3a3AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t4_d94da8a3a3_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT4F36e4cc057AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t4_f36e4cc057_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT50e16d1263cAsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t5_0e16d1263c_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT50e4c0f375aAsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t5_0e4c0f375a_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT524ddc6e94aAsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t5_24ddc6e94a_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT525b4801af0AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t5_25b4801af0_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT54ee485d1feAsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t5_4ee485d1fe_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT563a4b35e04AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t5_63a4b35e04_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT59469c72fa9AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t5_9469c72fa9_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT59bcd382830AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t5_9bcd382830_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT5C7adee95ddAsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t5_c7adee95dd_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT5Ff65a9fc08AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t5_ff65a9fc08_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT61ef57bc397AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t6_1ef57bc397_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT632579f0e25AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t6_32579f0e25_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT63a3bcd69afAsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t6_3a3bcd69af_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT64e584bde60AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t6_4e584bde60_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT6897f136b73AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t6_897f136b73_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT68b32f605e4AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t6_8b32f605e4_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT6B53c024913AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t6_b53c024913_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT6Bc80a01a49AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t6_bc80a01a49_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT6Ef539ea408AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t6_ef539ea408_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT6F7701a2990AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t6_f7701a2990_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT7118e821cbbAsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t7_118e821cbb_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT72b3ab930c5AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t7_2b3ab930c5_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT72c79b475a1AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t7_2c79b475a1_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT73d67bbb7ecAsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t7_3d67bbb7ec_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT76c486355b3AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t7_6c486355b3_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT77c755fe61dAsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t7_7c755fe61d_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT78605946c87AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t7_8605946c87_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT7Bd49e41c2dAsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t7_bd49e41c2d_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT7Fa13d1af3eAsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t7_fa13d1af3e_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT7Fab0f857b5AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t7_fab0f857b5_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT92c439b903cAsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t9_2c439b903c_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT9351a001e08AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t9_351a001e08_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT94251be212fAsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t9_4251be212f_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT950966a5af0AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t9_50966a5af0_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT9587e1edc42AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t9_587e1edc42_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT95a54131c80AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t9_5a54131c80_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT965e0181465AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t9_65e0181465_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT97fdd915386AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t9_7fdd915386_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT99a0017b6f8AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t9_9a0017b6f8_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaT9Cab0ac200aAsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_t9_cab0ac200a_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaTa80ab8691386AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_ta8_0ab8691386_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaTa81359a424a7AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_ta8_1359a424a7_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaTa840c23450edAsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_ta8_40c23450ed_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaTa890efed25dbAsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_ta8_90efed25db_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaTa898fb4824c8AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_ta8_98fb4824c8_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaTa8A797a12af6AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_ta8_a797a12af6_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaTa8A944cae038AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_ta8_a944cae038_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaTa8F28d1abf40AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_ta8_f28d1abf40_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaTa8F9956aedb7AsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_ta8_f9956aedb7_AsyncOperations";
				public const string NewSystemDonotuseentityRp53fd1p1ekxpaTa8Feedd2fd0eAsyncOperations = "new_system_donotuseentity_rp53fd1p1ekxpa_ta8_feedd2fd0e_AsyncOperations";
				public const string NewSystemSolDonotuseentityRp53fd1p1ekxpaSol1250835AsyncOperations = "new_system_sol_donotuseentity_rp53fd1p1ekxpa_sol_1250835_AsyncOperations";
				public const string NewSystemSolDonotuseentityRp53fd1p1ekxpaSol3aa9185AsyncOperations = "new_system_sol_donotuseentity_rp53fd1p1ekxpa_sol_3aa9185_AsyncOperations";
				public const string NewSystemSolDonotuseentityRp53fd1p1ekxpaSol55a3a82AsyncOperations = "new_system_sol_donotuseentity_rp53fd1p1ekxpa_sol_55a3a82_AsyncOperations";
				public const string NewSystemSolDonotuseentityRp53fd1p1ekxpaSol95d5b32AsyncOperations = "new_system_sol_donotuseentity_rp53fd1p1ekxpa_sol_95d5b32_AsyncOperations";
				public const string NewSystemSolDonotuseentityRp53fd1p1ekxpaSolAc091e5AsyncOperations = "new_system_sol_donotuseentity_rp53fd1p1ekxpa_sol_ac091e5_AsyncOperations";
				public const string NewSystemSolDonotuseentityRp53fd1p1ekxpaSolAdf8474AsyncOperations = "new_system_sol_donotuseentity_rp53fd1p1ekxpa_sol_adf8474_AsyncOperations";
				public const string NewSystemSolDonotuseentityRp53fd1p1ekxpaSolB0a3d13AsyncOperations = "new_system_sol_donotuseentity_rp53fd1p1ekxpa_sol_b0a3d13_AsyncOperations";
				public const string OpportunityAsyncOperations = "Opportunity_AsyncOperations";
				public const string OpportunityCloseAsyncOperations = "OpportunityClose_AsyncOperations";
				public const string OpportunitycompetitorsAsyncOperations = "opportunitycompetitors_AsyncOperations";
				public const string OpportunityProductAsyncOperations = "OpportunityProduct_AsyncOperations";
				public const string OpportunitysalesprocessAsyncOperations = "opportunitysalesprocess_AsyncOperations";
				public const string OrderCloseAsyncOperations = "OrderClose_AsyncOperations";
				public const string OrganizationAsyncOperations = "Organization_AsyncOperations";
				public const string OrganizationdatasyncstateAsyncOperations = "organizationdatasyncstate_AsyncOperations";
				public const string OrganizationdatasyncsubscriptionAsyncOperations = "organizationdatasyncsubscription_AsyncOperations";
				public const string OrganizationdatasyncsubscriptionentityAsyncOperations = "organizationdatasyncsubscriptionentity_AsyncOperations";
				public const string OrganizationsettingAsyncOperations = "organizationsetting_AsyncOperations";
				public const string OwnerAsyncoperations = "owner_asyncoperations";
				public const string PackageAsyncOperations = "package_AsyncOperations";
				public const string PdfsettingAsyncOperations = "pdfsetting_AsyncOperations";
				public const string PhoneCallAsyncOperations = "PhoneCall_AsyncOperations";
				public const string PhonetocaseprocessAsyncOperations = "phonetocaseprocess_AsyncOperations";
				public const string PluginpackageAsyncOperations = "pluginpackage_AsyncOperations";
				public const string PositionAsyncOperations = "position_AsyncOperations";
				public const string PostAsyncOperations = "post_AsyncOperations";
				public const string PostFollowAsyncOperations = "PostFollow_AsyncOperations";
				public const string PowerbidatasetAsyncOperations = "powerbidataset_AsyncOperations";
				public const string PowerbimashupparameterAsyncOperations = "powerbimashupparameter_AsyncOperations";
				public const string PowerbireportAsyncOperations = "powerbireport_AsyncOperations";
				public const string PowerfxruleAsyncOperations = "powerfxrule_AsyncOperations";
				public const string PriceLevelAsyncOperations = "PriceLevel_AsyncOperations";
				public const string PrivilegeAsyncOperations = "Privilege_AsyncOperations";
				public const string PrivilegesremovalsettingAsyncOperations = "privilegesremovalsetting_AsyncOperations";
				public const string ProcessstageparameterAsyncOperations = "processstageparameter_AsyncOperations";
				public const string ProductAsyncOperations = "Product_AsyncOperations";
				public const string ProductAssociationAsyncOperations = "ProductAssociation_AsyncOperations";
				public const string ProductPriceLevelAsyncOperations = "ProductPriceLevel_AsyncOperations";
				public const string ProductsalesliteratureAsyncOperations = "productsalesliterature_AsyncOperations";
				public const string ProductSubstituteAsyncOperations = "ProductSubstitute_AsyncOperations";
				public const string ProfileruleAsyncOperations = "profilerule_AsyncOperations";
				public const string ProvisionlanguageforuserAsyncOperations = "provisionlanguageforuser_AsyncOperations";
				public const string QuarterlyFiscalCalendarAsyncOperations = "QuarterlyFiscalCalendar_AsyncOperations";
				public const string QueueAsyncOperations = "Queue_AsyncOperations";
				public const string QueueItemAsyncOperations = "QueueItem_AsyncOperations";
				public const string QuoteAsyncOperations = "Quote_AsyncOperations";
				public const string QuoteCloseAsyncOperations = "QuoteClose_AsyncOperations";
				public const string QuoteDetailAsyncOperations = "QuoteDetail_AsyncOperations";
				public const string RatingmodelAsyncOperations = "ratingmodel_AsyncOperations";
				public const string RatingvalueAsyncOperations = "ratingvalue_AsyncOperations";
				public const string RecordfilterAsyncOperations = "recordfilter_AsyncOperations";
				public const string RecurringAppointmentMasterAsyncOperations = "RecurringAppointmentMaster_AsyncOperations";
				public const string RelationshipattributeAsyncOperations = "relationshipattribute_AsyncOperations";
				public const string RelationshipRoleAsyncOperations = "RelationshipRole_AsyncOperations";
				public const string RelationshipRoleMapAsyncOperations = "RelationshipRoleMap_AsyncOperations";
				public const string ReportAsyncOperations = "Report_AsyncOperations";
				public const string ResourceAsyncOperations = "Resource_AsyncOperations";
				public const string ResourceGroupAsyncOperations = "ResourceGroup_AsyncOperations";
				public const string ResourcegroupexpansionAsyncOperations = "resourcegroupexpansion_AsyncOperations";
				public const string ResourceSpecAsyncOperations = "ResourceSpec_AsyncOperations";
				public const string RevokeinheritedaccessrecordstrackerAsyncOperations = "revokeinheritedaccessrecordstracker_AsyncOperations";
				public const string RoleAsyncOperations = "Role_AsyncOperations";
				public const string RoleeditorlayoutAsyncOperations = "roleeditorlayout_AsyncOperations";
				public const string RollupfieldAsyncOperations = "rollupfield_AsyncOperations";
				public const string RoutingruleAsyncOperations = "routingrule_AsyncOperations";
				public const string RoutingruleitemAsyncOperations = "routingruleitem_AsyncOperations";
				public const string SalesLiteratureAsyncOperations = "SalesLiterature_AsyncOperations";
				public const string SalesLiteratureItemAsyncOperations = "SalesLiteratureItem_AsyncOperations";
				public const string SalesOrderAsyncOperations = "SalesOrder_AsyncOperations";
				public const string SalesOrderDetailAsyncOperations = "SalesOrderDetail_AsyncOperations";
				public const string SalesprocessinstanceAsyncOperations = "salesprocessinstance_AsyncOperations";
				public const string SavedQueryAsyncOperations = "SavedQuery_AsyncOperations";
				public const string SdkMessageProcessingStepAsyncOperations = "SdkMessageProcessingStep_AsyncOperations";
				public const string SearchrelationshipsettingsAsyncOperations = "searchrelationshipsettings_AsyncOperations";
				public const string SemiAnnualFiscalCalendarAsyncOperations = "SemiAnnualFiscalCalendar_AsyncOperations";
				public const string ServiceAsyncOperations = "Service_AsyncOperations";
				public const string ServiceAppointmentAsyncOperations = "ServiceAppointment_AsyncOperations";
				public const string ServicecontractcontactsAsyncOperations = "servicecontractcontacts_AsyncOperations";
				public const string ServiceplanAsyncOperations = "serviceplan_AsyncOperations";
				public const string ServiceplanmappingAsyncOperations = "serviceplanmapping_AsyncOperations";
				public const string SettingdefinitionAsyncOperations = "settingdefinition_AsyncOperations";
				public const string SharedlinksettingAsyncOperations = "sharedlinksetting_AsyncOperations";
				public const string SharedobjectAsyncOperations = "sharedobject_AsyncOperations";
				public const string SharedworkspaceAsyncOperations = "sharedworkspace_AsyncOperations";
				public const string SharePointDocumentLocationAsyncOperations = "SharePointDocumentLocation_AsyncOperations";
				public const string SharePointSiteAsyncOperations = "SharePointSite_AsyncOperations";
				public const string SimilarityruleAsyncOperations = "similarityrule_AsyncOperations";
				public const string SiteAsyncOperations = "Site_AsyncOperations";
				public const string SlabaseAsyncOperations = "slabase_AsyncOperations";
				public const string SocialActivityAsyncOperations = "SocialActivity_AsyncOperations";
				public const string SocialProfileAsyncOperations = "SocialProfile_AsyncOperations";
				public const string SolutioncomponentattributeconfigurationAsyncOperations = "solutioncomponentattributeconfiguration_AsyncOperations";
				public const string SolutioncomponentbatchconfigurationAsyncOperations = "solutioncomponentbatchconfiguration_AsyncOperations";
				public const string SolutioncomponentconfigurationAsyncOperations = "solutioncomponentconfiguration_AsyncOperations";
				public const string SolutioncomponentrelationshipconfigurationAsyncOperations = "solutioncomponentrelationshipconfiguration_AsyncOperations";
				public const string StagedentityAsyncOperations = "stagedentity_AsyncOperations";
				public const string StagesolutionuploadAsyncOperations = "stagesolutionupload_AsyncOperations";
				public const string SubjectAsyncOperations = "Subject_AsyncOperations";
				public const string SupportusertableAsyncOperations = "supportusertable_AsyncOperations";
				public const string SynapsedatabaseAsyncOperations = "synapsedatabase_AsyncOperations";
				public const string SynapselinkexternaltablestateAsyncOperations = "synapselinkexternaltablestate_AsyncOperations";
				public const string SynapselinkprofileAsyncOperations = "synapselinkprofile_AsyncOperations";
				public const string SynapselinkprofileentityAsyncOperations = "synapselinkprofileentity_AsyncOperations";
				public const string SynapselinkprofileentitystateAsyncOperations = "synapselinkprofileentitystate_AsyncOperations";
				public const string SynapselinkscheduleAsyncOperations = "synapselinkschedule_AsyncOperations";
				public const string SystemUserAsyncoperation = "system_user_asyncoperation";
				public const string SystemFormAsyncOperations = "SystemForm_AsyncOperations";
				public const string SystemUserAsyncOperations = "SystemUser_AsyncOperations";
				public const string SystemuserauthorizationchangetrackerAsyncOperations = "systemuserauthorizationchangetracker_AsyncOperations";
				public const string TaskAsyncOperations = "Task_AsyncOperations";
				public const string TdsmetadataAsyncOperations = "tdsmetadata_AsyncOperations";
				public const string TeamAsyncoperation = "team_asyncoperation";
				public const string TeamAsyncOperations = "Team_AsyncOperations";
				public const string TeammobileofflineprofilemembershipAsyncOperations = "teammobileofflineprofilemembership_AsyncOperations";
				public const string TemplateAsyncOperations = "Template_AsyncOperations";
				public const string TerritoryAsyncOperations = "Territory_AsyncOperations";
				public const string ThemeAsyncOperations = "theme_AsyncOperations";
				public const string TopicAsyncOperations = "topic_AsyncOperations";
				public const string TopichistoryAsyncOperations = "topichistory_AsyncOperations";
				public const string TopicmodelAsyncOperations = "topicmodel_AsyncOperations";
				public const string TopicmodelconfigurationAsyncOperations = "topicmodelconfiguration_AsyncOperations";
				public const string TopicmodelexecutionhistoryAsyncOperations = "topicmodelexecutionhistory_AsyncOperations";
				public const string TransactionCurrencyAsyncOperations = "TransactionCurrency_AsyncOperations";
				public const string UoMAsyncOperations = "UoM_AsyncOperations";
				public const string UoMScheduleAsyncOperations = "UoMSchedule_AsyncOperations";
				public const string UserFormAsyncOperations = "UserForm_AsyncOperations";
				public const string UsermappingAsyncOperations = "usermapping_AsyncOperations";
				public const string UsermobileofflineprofilemembershipAsyncOperations = "usermobileofflineprofilemembership_AsyncOperations";
				public const string UserQueryAsyncOperations = "UserQuery_AsyncOperations";
				public const string UserratingAsyncOperations = "userrating_AsyncOperations";
				public const string VirtualentitymetadataAsyncOperations = "virtualentitymetadata_AsyncOperations";
				public const string WorkflowbinaryAsyncOperations = "workflowbinary_AsyncOperations";
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
        public static AsyncOperation Retrieve(IOrganizationService service, Guid id)
        {
            return Retrieve(service,id, new ColumnSet(true));
        }

        public static AsyncOperation Retrieve(IOrganizationService service, Guid id, ColumnSet columnSet)
        {
            return service.Retrieve("asyncoperation", id, columnSet).ToEntity<AsyncOperation>();
        }

        public AsyncOperation GetChangedEntity()
        {
            if (_trackChanges)
            {
                var attr = new AttributeCollection();
                foreach (var attrName in _changedProperties.Value.Select(changedProperty => ((AttributeLogicalNameAttribute) GetType().GetProperty(changedProperty)!.GetCustomAttribute(typeof (AttributeLogicalNameAttribute))!).LogicalName).Where(attrName => Contains(attrName)))
                {
                    attr.Add(attrName,this[attrName]);
                }
                return new  AsyncOperation(Id) {Attributes = attr };
            }
            return this;
        }
        #endregion
	}

	#region Context
	public partial class DataContext
	{
		public IQueryable<AsyncOperation> AsyncOperationSet
		{
			get
			{
				return CreateQuery<AsyncOperation>();
			}
		}
	}
	#endregion
}
