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
	/// A list of records that require action, such as accounts, activities, and cases.
	/// </summary>
	[DataContractAttribute()]
	[EntityLogicalNameAttribute("queue")]
	[System.CodeDom.Compiler.GeneratedCode("dgtp", "2023")]
    [ExcludeFromCodeCoverage]
	public partial class Queue : Entity, INotifyPropertyChanging, INotifyPropertyChanged
    {
	    #region ctor
		[DebuggerNonUserCode]
		public Queue() : this(false)
        {
        }

        [DebuggerNonUserCode]
		public Queue(bool trackChanges = false) : base(EntityLogicalName)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public Queue(Guid id, bool trackChanges = false) : base(EntityLogicalName,id)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public Queue(KeyAttributeCollection keyAttributes, bool trackChanges = false) : base(EntityLogicalName,keyAttributes)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public Queue(string keyName, object keyValue, bool trackChanges = false) : base(EntityLogicalName, keyName, keyValue)
        {
			_trackChanges = trackChanges;
        }
        #endregion

		#region fields
        private readonly bool _trackChanges;
        private readonly Lazy<HashSet<string>> _changedProperties = new Lazy<HashSet<string>>();
        #endregion

        #region consts
        public const string EntityLogicalName = "queue";
        public const string PrimaryNameAttribute = "name";
        public const int EntityTypeCode = 2020;
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
		[AttributeLogicalNameAttribute("queueid")]
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
				QueueId = value;
			}
		}

		/// <summary>
		/// Unique identifier of the queue.
		/// </summary>
		[AttributeLogicalName("queueid")]
        public Guid? QueueId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<Guid?>("queueid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(QueueId));
                SetAttributeValue("queueid", value);
				if (value.HasValue)
				{
					base.Id = value.Value;
				}
				else
				{
					base.Id = System.Guid.Empty;
				}
                OnPropertyChanged(nameof(QueueId));
            }
        }

		/// <summary>
		/// This attribute is no longer used. The data is now in the Mailbox.AllowEmailConnectorToUseCredentials attribute.
		/// </summary>
		[AttributeLogicalName("allowemailcredentials")]
        public bool? AllowEmailCredentials
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<bool?>("allowemailcredentials");
            }
        }

		/// <summary>
		/// Unique identifier of the business unit with which the queue is associated.
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
		/// Unique identifier of the user who created the queue record.
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
		/// Date and time when the queue was created.
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
		/// Unique identifier of the delegate user who created the queue.
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
		/// Select the mailbox associated with this queue.
		/// </summary>
		[AttributeLogicalName("defaultmailbox")]
        public EntityReference? DefaultMailbox
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<EntityReference?>("defaultmailbox");
            }
        }

		/// <summary>
		/// Description of the queue.
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
		/// Email address that is associated with the queue.
		/// </summary>
		[AttributeLogicalName("emailaddress")]
        public string? EMailAddress
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("emailaddress");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(EMailAddress));
                SetAttributeValue("emailaddress", value);
                OnPropertyChanged(nameof(EMailAddress));
            }
        }

		/// <summary>
		/// This attribute is no longer used. The data is now in the Mailbox.Password attribute.
		/// </summary>
		[AttributeLogicalName("emailpassword")]
        public string? EmailPassword
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("emailpassword");
            }
        }

		/// <summary>
		/// Shows the status of the primary email address.
		/// </summary>
		[AttributeLogicalName("emailrouteraccessapproval")]
        public OptionSetValue? EmailRouterAccessApproval
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<OptionSetValue?>("emailrouteraccessapproval");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(EmailRouterAccessApproval));
                SetAttributeValue("emailrouteraccessapproval", value);
                OnPropertyChanged(nameof(EmailRouterAccessApproval));
            }
        }

		
		[AttributeLogicalName("emailsignature")]
        public EntityReference? EmailSignature
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<EntityReference?>("emailsignature");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(EmailSignature));
                SetAttributeValue("emailsignature", value);
                OnPropertyChanged(nameof(EmailSignature));
            }
        }

		/// <summary>
		/// This attribute is no longer used. The data is now in the Mailbox.UserName attribute.
		/// </summary>
		[AttributeLogicalName("emailusername")]
        public string? EmailUsername
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("emailusername");
            }
        }

		/// <summary>
		/// The default image for the entity.
		/// </summary>
		[AttributeLogicalName("entityimage")]
        public byte[]? EntityImage
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<byte[]?>("entityimage");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(EntityImage));
                SetAttributeValue("entityimage", value);
                OnPropertyChanged(nameof(EntityImage));
            }
        }

		
		[AttributeLogicalName("entityimage_timestamp")]
        public long? EntityImageTimestamp
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<long?>("entityimage_timestamp");
            }
        }

		
		[AttributeLogicalName("entityimage_url")]
        public string? EntityImageURL
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("entityimage_url");
            }
        }

		/// <summary>
		/// For internal use only.
		/// </summary>
		[AttributeLogicalName("entityimageid")]
        public Guid? EntityImageId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<Guid?>("entityimageid");
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
		/// Information that specifies whether a queue is to ignore unsolicited email (deprecated).
		/// </summary>
		[AttributeLogicalName("ignoreunsolicitedemail")]
        public bool? IgnoreUnsolicitedEmail
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<bool?>("ignoreunsolicitedemail");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(IgnoreUnsolicitedEmail));
                SetAttributeValue("ignoreunsolicitedemail", value);
                OnPropertyChanged(nameof(IgnoreUnsolicitedEmail));
            }
        }

		/// <summary>
		/// Unique identifier of the data import or data migration that created this record.
		/// </summary>
		[AttributeLogicalName("importsequencenumber")]
        public int? ImportSequenceNumber
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<int?>("importsequencenumber");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(ImportSequenceNumber));
                SetAttributeValue("importsequencenumber", value);
                OnPropertyChanged(nameof(ImportSequenceNumber));
            }
        }

		/// <summary>
		/// Incoming email delivery method for the queue.
		/// </summary>
		[AttributeLogicalName("incomingemaildeliverymethod")]
        public OptionSetValue? IncomingEmailDeliveryMethod
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<OptionSetValue?>("incomingemaildeliverymethod");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(IncomingEmailDeliveryMethod));
                SetAttributeValue("incomingemaildeliverymethod", value);
                OnPropertyChanged(nameof(IncomingEmailDeliveryMethod));
            }
        }

		/// <summary>
		/// Convert Incoming Email To Activities
		/// </summary>
		[AttributeLogicalName("incomingemailfilteringmethod")]
        public OptionSetValue? IncomingEmailFilteringMethod
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<OptionSetValue?>("incomingemailfilteringmethod");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(IncomingEmailFilteringMethod));
                SetAttributeValue("incomingemailfilteringmethod", value);
                OnPropertyChanged(nameof(IncomingEmailFilteringMethod));
            }
        }

		/// <summary>
		/// Shows the status of approval of the email address by O365 Admin.
		/// </summary>
		[AttributeLogicalName("isemailaddressapprovedbyo365admin")]
        public bool? IsEmailAddressApprovedByO365Admin
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<bool?>("isemailaddressapprovedbyo365admin");
            }
        }

		/// <summary>
		/// Indication of whether a queue is the fax delivery queue.
		/// </summary>
		[AttributeLogicalName("isfaxqueue")]
        public bool? IsFaxQueue
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<bool?>("isfaxqueue");
            }
        }

		/// <summary>
		/// Unique identifier of the user who last modified the queue.
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
		/// Date and time when the queue was last modified.
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
		/// Unique identifier of the delegate user who last modified the queue.
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
		/// Link assignment input contract with queue.
		/// </summary>
		[AttributeLogicalName("msdyn_assignmentinputcontractid")]
        public EntityReference? MsdynAssignmentinputcontractid
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<EntityReference?>("msdyn_assignmentinputcontractid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(MsdynAssignmentinputcontractid));
                SetAttributeValue("msdyn_assignmentinputcontractid", value);
                OnPropertyChanged(nameof(MsdynAssignmentinputcontractid));
            }
        }

		
		[AttributeLogicalName("msdyn_assignmentstrategy")]
        public OptionSetValue? MsdynAssignmentstrategy
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<OptionSetValue?>("msdyn_assignmentstrategy");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(MsdynAssignmentstrategy));
                SetAttributeValue("msdyn_assignmentstrategy", value);
                OnPropertyChanged(nameof(MsdynAssignmentstrategy));
            }
        }

		/// <summary>
		/// Define overflow rules for work items after it enters queue
		/// </summary>
		[AttributeLogicalName("msdyn_inqueueoverflowrulesetid")]
        public EntityReference? MsdynInqueueoverflowrulesetid
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<EntityReference?>("msdyn_inqueueoverflowrulesetid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(MsdynInqueueoverflowrulesetid));
                SetAttributeValue("msdyn_inqueueoverflowrulesetid", value);
                OnPropertyChanged(nameof(MsdynInqueueoverflowrulesetid));
            }
        }

		/// <summary>
		/// Shows whether the queue is set as default or not.
		/// </summary>
		[AttributeLogicalName("msdyn_isdefaultqueue")]
        public bool? MsdynIsdefaultqueue
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<bool?>("msdyn_isdefaultqueue");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(MsdynIsdefaultqueue));
                SetAttributeValue("msdyn_isdefaultqueue", value);
                OnPropertyChanged(nameof(MsdynIsdefaultqueue));
            }
        }

		/// <summary>
		/// Shows whether the queue is used as Omnichannel queue for work distribution.
		/// </summary>
		[AttributeLogicalName("msdyn_isomnichannelqueue")]
        public bool? MsdynIsomnichannelqueue
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<bool?>("msdyn_isomnichannelqueue");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(MsdynIsomnichannelqueue));
                SetAttributeValue("msdyn_isomnichannelqueue", value);
                OnPropertyChanged(nameof(MsdynIsomnichannelqueue));
            }
        }

		/// <summary>
		/// Maximum queue size
		/// </summary>
		[AttributeLogicalName("msdyn_maxqueuesize")]
        public int? MsdynMaxqueuesize
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<int?>("msdyn_maxqueuesize");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(MsdynMaxqueuesize));
                SetAttributeValue("msdyn_maxqueuesize", value);
                OnPropertyChanged(nameof(MsdynMaxqueuesize));
            }
        }

		/// <summary>
		/// Unique identifier for Operating hour associated with Queue
		/// </summary>
		[AttributeLogicalName("msdyn_operatinghourid")]
        public EntityReference? MsdynOperatinghourid
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<EntityReference?>("msdyn_operatinghourid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(MsdynOperatinghourid));
                SetAttributeValue("msdyn_operatinghourid", value);
                OnPropertyChanged(nameof(MsdynOperatinghourid));
            }
        }

		/// <summary>
		/// Define overflow rules for work items before it enters queue
		/// </summary>
		[AttributeLogicalName("msdyn_prequeueoverflowrulesetid")]
        public EntityReference? MsdynPrequeueoverflowrulesetid
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<EntityReference?>("msdyn_prequeueoverflowrulesetid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(MsdynPrequeueoverflowrulesetid));
                SetAttributeValue("msdyn_prequeueoverflowrulesetid", value);
                OnPropertyChanged(nameof(MsdynPrequeueoverflowrulesetid));
            }
        }

		/// <summary>
		/// Priority of the queue to indicate conversation assignment order to the agent.
		/// </summary>
		[AttributeLogicalName("msdyn_priority")]
        public int? MsdynPriority
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<int?>("msdyn_priority");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(MsdynPriority));
                SetAttributeValue("msdyn_priority", value);
                OnPropertyChanged(nameof(MsdynPriority));
            }
        }

		/// <summary>
		/// Defines the type of channels handled by this queue
		/// </summary>
		[AttributeLogicalName("msdyn_queuetype")]
        public OptionSetValue? MsdynQueuetype
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<OptionSetValue?>("msdyn_queuetype");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(MsdynQueuetype));
                SetAttributeValue("msdyn_queuetype", value);
                OnPropertyChanged(nameof(MsdynQueuetype));
            }
        }

		/// <summary>
		/// Unique Name for the entity.
		/// </summary>
		[AttributeLogicalName("msdyn_uniquename")]
        public string? MsdynUniquename
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("msdyn_uniquename");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(MsdynUniquename));
                SetAttributeValue("msdyn_uniquename", value);
                OnPropertyChanged(nameof(MsdynUniquename));
            }
        }

		/// <summary>
		/// Name of the queue.
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
		/// Number of Queue items associated with the queue.
		/// </summary>
		[AttributeLogicalName("numberofitems")]
        public int? NumberOfItems
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<int?>("numberofitems");
            }
        }

		/// <summary>
		/// Number of Members associated with the queue.
		/// </summary>
		[AttributeLogicalName("numberofmembers")]
        public int? NumberOfMembers
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<int?>("numberofmembers");
            }
        }

		/// <summary>
		/// Unique identifier of the organization associated with the queue.
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
		/// Outgoing email delivery method for the queue.
		/// </summary>
		[AttributeLogicalName("outgoingemaildeliverymethod")]
        public OptionSetValue? OutgoingEmailDeliveryMethod
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<OptionSetValue?>("outgoingemaildeliverymethod");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(OutgoingEmailDeliveryMethod));
                SetAttributeValue("outgoingemaildeliverymethod", value);
                OnPropertyChanged(nameof(OutgoingEmailDeliveryMethod));
            }
        }

		/// <summary>
		/// Date and time that the record was migrated.
		/// </summary>
		[AttributeLogicalName("overriddencreatedon")]
        public DateTime? OverriddenCreatedOn
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<DateTime?>("overriddencreatedon");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(OverriddenCreatedOn));
                SetAttributeValue("overriddencreatedon", value);
                OnPropertyChanged(nameof(OverriddenCreatedOn));
            }
        }

		/// <summary>
		/// Unique identifier of the user or team who owns the queue.
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
		/// Unique identifier of the business unit that owns the queue.
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
		/// Unique identifier of the team who owns the queue.
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
		/// Unique identifier of the user who owns the queue.
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
		/// Unique identifier of the owner of the queue.
		/// </summary>
		[AttributeLogicalName("primaryuserid")]
        public EntityReference? PrimaryUserId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<EntityReference?>("primaryuserid");
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
		/// Type of queue that is automatically assigned when a user or queue is created. The type can be public, private, or work in process.
		/// </summary>
		[AttributeLogicalName("queuetypecode")]
        public OptionSetValue? QueueTypeCode
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<OptionSetValue?>("queuetypecode");
            }
        }

		/// <summary>
		/// Select whether the queue is public or private. A public queue can be viewed by all. A private queue can be viewed only by the members added to the queue.
		/// </summary>
		[AttributeLogicalName("queueviewtype")]
        public OptionSetValue? QueueViewType
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<OptionSetValue?>("queueviewtype");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(QueueViewType));
                SetAttributeValue("queueviewtype", value);
                OnPropertyChanged(nameof(QueueViewType));
            }
        }

		/// <summary>
		/// Status of the queue.
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
		/// Reason for the status of the queue.
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
		/// Unique identifier of the currency associated with the queue.
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
		/// Version number of the queue.
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
		/// 1:N Queue_AsyncOperations
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("Queue_AsyncOperations")]
		public System.Collections.Generic.IEnumerable<AsyncOperation> QueueAsyncOperations
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<AsyncOperation>("Queue_AsyncOperations", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("QueueAsyncOperations");
				this.SetRelatedEntities<AsyncOperation>("Queue_AsyncOperations", null, value);
				this.OnPropertyChanged("QueueAsyncOperations");
			}
		}

		/// <summary>
		/// 1:N queue_routingruleitem
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("queue_routingruleitem")]
		public System.Collections.Generic.IEnumerable<RoutingRuleItem> QueueRoutingruleitem
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<RoutingRuleItem>("queue_routingruleitem", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("QueueRoutingruleitem");
				this.SetRelatedEntities<RoutingRuleItem>("queue_routingruleitem", null, value);
				this.OnPropertyChanged("QueueRoutingruleitem");
			}
		}

		/// <summary>
		/// 1:N queue_system_user
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("queue_system_user")]
		public System.Collections.Generic.IEnumerable<SystemUser> QueueSystemUser
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<SystemUser>("queue_system_user", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("QueueSystemUser");
				this.SetRelatedEntities<SystemUser>("queue_system_user", null, value);
				this.OnPropertyChanged("QueueSystemUser");
			}
		}

		/// <summary>
		/// 1:N queue_team
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("queue_team")]
		public System.Collections.Generic.IEnumerable<Team> QueueTeam
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Team>("queue_team", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("QueueTeam");
				this.SetRelatedEntities<Team>("queue_team", null, value);
				this.OnPropertyChanged("QueueTeam");
			}
		}

		#endregion

		#region Options
		public static class Options
		{
                public struct AllowEmailCredentials
                {
                    public const bool No = false;
                    public const bool Yes = true;
                }
			    public struct EmailRouterAccessApproval
                {
					public const int Empty = 0;
					public const int Approved = 1;
					public const int PendingApproval = 2;
					public const int Rejected = 3;
                }
                public struct IgnoreUnsolicitedEmail
                {
                    public const bool AllIncomingEmails = false;
                    public const bool OnlySpecificEmails = true;
                }
			    public struct IncomingEmailDeliveryMethod
                {
					public const int None = 0;
					public const int ServerSideSynchronizationOrEmailRouter = 2;
					public const int ForwardMailbox = 3;
                }
			    public struct IncomingEmailFilteringMethod
                {
					public const int AllEmailMessages = 0;
					public const int EmailMessagesInResponseToDynamics365Email = 1;
					public const int EmailMessagesFromDynamics365Leads_ContactsAndAccounts = 2;
					public const int EmailMessagesFromDynamics365RecordsThatAreEmailEnabled = 3;
					public const int NoEmailMessages = 4;
                }
                public struct IsEmailAddressApprovedByO365Admin
                {
                    public const bool No = false;
                    public const bool Yes = true;
                }
                public struct IsFaxQueue
                {
                    public const bool NonFaxQueue = false;
                    public const bool FaxQueue = true;
                }
			    public struct MsdynAssignmentstrategy
                {
					public const int OmnichannelAssignment = 192350000;
					public const int RoundRobin = 192350001;
					public const int CustomAssignmentConfiguration = 192350002;
					public const int LongestIdle = 192350003;
                }
                public struct MsdynIsdefaultqueue
                {
                    public const bool No = false;
                    public const bool Yes = true;
                }
                public struct MsdynIsomnichannelqueue
                {
                    public const bool No = false;
                    public const bool Yes = true;
                }
			    public struct MsdynQueuetype
                {
					public const int Messaging = 192350000;
					public const int Entity = 192350001;
                }
			    public struct OutgoingEmailDeliveryMethod
                {
					public const int None = 0;
					public const int ServerSideSynchronizationOrEmailRouter = 2;
                }
			    public struct QueueTypeCode
                {
					public const int DefaultValue = 1;
                }
			    public struct QueueViewType
                {
					public const int Public = 0;
					public const int Private = 1;
                }
                public struct StateCode
                {
					public const int Active = 0;
					public const int Inactive = 1;
                }
                public struct StatusCode
                {
					public const int Active = 1;
					public const int Inactive = 2;
                }
		}
		#endregion

		#region LogicalNames
		public static class LogicalNames
		{
				public const string QueueId = "queueid";
				public const string AllowEmailCredentials = "allowemailcredentials";
				public const string BusinessUnitId = "businessunitid";
				public const string CreatedBy = "createdby";
				public const string CreatedOn = "createdon";
				public const string CreatedOnBehalfBy = "createdonbehalfby";
				public const string DefaultMailbox = "defaultmailbox";
				public const string Description = "description";
				public const string EMailAddress = "emailaddress";
				public const string EmailPassword = "emailpassword";
				public const string EmailRouterAccessApproval = "emailrouteraccessapproval";
				public const string EmailSignature = "emailsignature";
				public const string EmailUsername = "emailusername";
				public const string EntityImage = "entityimage";
				public const string EntityImageTimestamp = "entityimage_timestamp";
				public const string EntityImageURL = "entityimage_url";
				public const string EntityImageId = "entityimageid";
				public const string ExchangeRate = "exchangerate";
				public const string IgnoreUnsolicitedEmail = "ignoreunsolicitedemail";
				public const string ImportSequenceNumber = "importsequencenumber";
				public const string IncomingEmailDeliveryMethod = "incomingemaildeliverymethod";
				public const string IncomingEmailFilteringMethod = "incomingemailfilteringmethod";
				public const string IsEmailAddressApprovedByO365Admin = "isemailaddressapprovedbyo365admin";
				public const string IsFaxQueue = "isfaxqueue";
				public const string ModifiedBy = "modifiedby";
				public const string ModifiedOn = "modifiedon";
				public const string ModifiedOnBehalfBy = "modifiedonbehalfby";
				public const string MsdynAssignmentinputcontractid = "msdyn_assignmentinputcontractid";
				public const string MsdynAssignmentstrategy = "msdyn_assignmentstrategy";
				public const string MsdynInqueueoverflowrulesetid = "msdyn_inqueueoverflowrulesetid";
				public const string MsdynIsdefaultqueue = "msdyn_isdefaultqueue";
				public const string MsdynIsomnichannelqueue = "msdyn_isomnichannelqueue";
				public const string MsdynMaxqueuesize = "msdyn_maxqueuesize";
				public const string MsdynOperatinghourid = "msdyn_operatinghourid";
				public const string MsdynPrequeueoverflowrulesetid = "msdyn_prequeueoverflowrulesetid";
				public const string MsdynPriority = "msdyn_priority";
				public const string MsdynQueuetype = "msdyn_queuetype";
				public const string MsdynUniquename = "msdyn_uniquename";
				public const string Name = "name";
				public const string NumberOfItems = "numberofitems";
				public const string NumberOfMembers = "numberofmembers";
				public const string OrganizationId = "organizationid";
				public const string OutgoingEmailDeliveryMethod = "outgoingemaildeliverymethod";
				public const string OverriddenCreatedOn = "overriddencreatedon";
				public const string OwnerId = "ownerid";
				public const string OwnerIdType = "owneridtype";
				public const string OwningBusinessUnit = "owningbusinessunit";
				public const string OwningTeam = "owningteam";
				public const string OwningUser = "owninguser";
				public const string PrimaryUserId = "primaryuserid";
				public const string QueueTypeCode = "queuetypecode";
				public const string QueueViewType = "queueviewtype";
				public const string StateCode = "statecode";
				public const string StatusCode = "statuscode";
				public const string TransactionCurrencyId = "transactioncurrencyid";
				public const string VersionNumber = "versionnumber";
		}
		#endregion

		#region Relations
        public static class Relations
        {
            public static class OneToMany
            {
				public const string ConvertruleQueue = "convertrule_queue";
				public const string EmailAcceptingentityQueue = "email_acceptingentity_queue";
				public const string MailboxRegardingQueue = "mailbox_regarding_queue";
				public const string MsdynLiveworkstreamDefaultqueueQueue = "msdyn_liveworkstream_defaultqueue_Queue";
				public const string MsdynQueueMsdynAssignmentconfigurationQueueid = "msdyn_queue_msdyn_assignmentconfiguration_queueid";
				public const string MsdynQueueMsdynLiveconversation = "msdyn_queue_msdyn_liveconversation";
				public const string MsdynQueueMsdynLiveworkstreamMsdynBotQueue = "msdyn_queue_msdyn_liveworkstream_msdyn_bot_queue";
				public const string MsdynQueueMsdynLiveworkstreamQueueid = "msdyn_queue_msdyn_liveworkstream_queueid";
				public const string MsdynQueueMsdynOcliveworkitemQueueid = "msdyn_queue_msdyn_ocliveworkitem_queueid";
				public const string MsdynQueueMsdynOcsessionQueueid = "msdyn_queue_msdyn_ocsession_queueid";
				public const string MsdynQueueMsdynUnifiedroutingrunQueue = "msdyn_queue_msdyn_unifiedroutingrun_queue";
				public const string MsdynQueueOcruleitem = "msdyn_queue_ocruleitem";
				public const string MsdynRulesetdependencymappingQueueMsdynReferencedpolymorphicentityid = "msdyn_rulesetdependencymapping_queue_msdyn_referencedpolymorphicentityid";
				public const string QueueActivityParties = "queue_activity_parties";
				public const string QueueAsyncOperations = "Queue_AsyncOperations";
				public const string QueueBulkDeleteFailures = "Queue_BulkDeleteFailures";
				public const string QueueConvertruleitem = "queue_convertruleitem";
				public const string QueueDuplicateBaseRecord = "Queue_DuplicateBaseRecord";
				public const string QueueDuplicateMatchingRecord = "Queue_DuplicateMatchingRecord";
				public const string QueueEmailEmailSender = "Queue_Email_EmailSender";
				public const string QueueEntries = "queue_entries";
				public const string QueuePostFollows = "queue_PostFollows";
				public const string QueuePostRegardings = "queue_PostRegardings";
				public const string QueuePostRoles = "queue_PostRoles";
				public const string QueuePrincipalobjectattributeaccess = "queue_principalobjectattributeaccess";
				public const string QueueProcessSessions = "Queue_ProcessSessions";
				public const string QueueRoutingruleitem = "queue_routingruleitem";
				public const string QueueSyncErrors = "Queue_SyncErrors";
				public const string QueueSystemUser = "queue_system_user";
				public const string QueueTeam = "queue_team";
				public const string UserentityinstancedataQueue = "userentityinstancedata_queue";
            }

            public static class ManyToOne
            {
				public const string BusinessUnitQueues = "business_unit_queues";
				public const string BusinessUnitQueues2 = "business_unit_queues2";
				public const string EmailsignatureQueueEmailSignature = "emailsignature_queue_EmailSignature";
				public const string LkQueueCreatedonbehalfby = "lk_queue_createdonbehalfby";
				public const string LkQueueEntityimage = "lk_queue_entityimage";
				public const string LkQueueModifiedonbehalfby = "lk_queue_modifiedonbehalfby";
				public const string LkQueueQueueItemCount = "lk_queue_QueueItemCount";
				public const string LkQueueQueueMemberCount = "lk_queue_QueueMemberCount";
				public const string LkQueuebaseCreatedby = "lk_queuebase_createdby";
				public const string LkQueuebaseModifiedby = "lk_queuebase_modifiedby";
				public const string MsdynDecisionrulesetQueueMsdynInqueueoverflowrulesetid = "msdyn_decisionruleset_queue_msdyn_inqueueoverflowrulesetid";
				public const string MsdynMsdynOperatinghourQueue = "msdyn_msdyn_operatinghour_queue";
				public const string MsdynQueueDecisioncontractid = "msdyn_queue_decisioncontractid";
				public const string MsdynQueueDecisionrulesetId = "msdyn_queue_decisionrulesetId";
				public const string OrganizationQueues = "organization_queues";
				public const string OwnerQueues = "owner_queues";
				public const string QueueDefaultmailboxMailbox = "queue_defaultmailbox_mailbox";
				public const string QueuePrimaryUser = "queue_primary_user";
				public const string TransactionCurrencyQueue = "TransactionCurrency_Queue";
            }

            public static class ManyToMany
            {
				public const string QueuemembershipAssociation = "queuemembership_association";
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
        public static Queue Retrieve(IOrganizationService service, Guid id)
        {
            return Retrieve(service,id, new ColumnSet(true));
        }

        public static Queue Retrieve(IOrganizationService service, Guid id, ColumnSet columnSet)
        {
            return service.Retrieve("queue", id, columnSet).ToEntity<Queue>();
        }

        public Queue GetChangedEntity()
        {
            if (_trackChanges)
            {
                var attr = new AttributeCollection();
                foreach (var attrName in _changedProperties.Value.Select(changedProperty => ((AttributeLogicalNameAttribute) GetType().GetProperty(changedProperty)!.GetCustomAttribute(typeof (AttributeLogicalNameAttribute))!).LogicalName).Where(attrName => Contains(attrName)))
                {
                    attr.Add(attrName,this[attrName]);
                }
                return new  Queue(Id) {Attributes = attr };
            }
            return this;
        }
        #endregion
	}

	#region Context
	public partial class DataContext
	{
		public IQueryable<Queue> QueueSet
		{
			get
			{
				return CreateQuery<Queue>();
			}
		}
	}
	#endregion
}
