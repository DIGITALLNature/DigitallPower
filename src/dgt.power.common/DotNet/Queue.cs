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
	/// A list of records that require action, such as accounts, activities, and cases.
	/// </summary>
    [DataContract]
    [EntityLogicalName("queue")]
    [System.CodeDom.Compiler.GeneratedCode("dgtp", "2026")]
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
        public Queue(Guid id, bool trackChanges = false) : base(EntityLogicalName, id)
        {
            _trackChanges = trackChanges;
        }
        [DebuggerNonUserCode]
        public Queue(KeyAttributeCollection keyAttributes, bool trackChanges = false) : base(EntityLogicalName, keyAttributes)
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
        private readonly Lazy<HashSet<string>> _changedProperties = new();
        #endregion

        #region consts
        public const string EntityLogicalName = "queue";
        public const string PrimaryNameAttribute = "name";
        public const int EntityTypeCode = 2020;
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
        [AttributeLogicalName("queueid")]
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
                OnPropertyChanging();
                SetAttributeValue("queueid", value);
                base.Id = value.HasValue ? value.Value : Guid.Empty;
                OnPropertyChanged();
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
                OnPropertyChanging();
                SetAttributeValue("businessunitid", value);
                OnPropertyChanged();
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
                OnPropertyChanging();
                SetAttributeValue("description", value);
                OnPropertyChanged();
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
                OnPropertyChanging();
                SetAttributeValue("emailaddress", value);
                OnPropertyChanged();
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
                OnPropertyChanging();
                SetAttributeValue("emailrouteraccessapproval", value);
                OnPropertyChanged();
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
                OnPropertyChanging();
                SetAttributeValue("entityimage", value);
                OnPropertyChanged();
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
                OnPropertyChanging();
                SetAttributeValue("ignoreunsolicitedemail", value);
                OnPropertyChanged();
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
                OnPropertyChanging();
                SetAttributeValue("importsequencenumber", value);
                OnPropertyChanged();
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
                OnPropertyChanging();
                SetAttributeValue("incomingemaildeliverymethod", value);
                OnPropertyChanged();
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
                OnPropertyChanging();
                SetAttributeValue("incomingemailfilteringmethod", value);
                OnPropertyChanged();
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
                OnPropertyChanging();
                SetAttributeValue("name", value);
                OnPropertyChanged();
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
                OnPropertyChanging();
                SetAttributeValue("outgoingemaildeliverymethod", value);
                OnPropertyChanged();
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
                OnPropertyChanging();
                SetAttributeValue("overriddencreatedon", value);
                OnPropertyChanged();
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
                OnPropertyChanging();
                SetAttributeValue("ownerid", value);
                OnPropertyChanged();
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
                OnPropertyChanging();
                SetAttributeValue("primaryuserid", value);
                OnPropertyChanged();
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
                OnPropertyChanging();
                SetAttributeValue("queueviewtype", value);
                OnPropertyChanged();
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
                OnPropertyChanging();
                SetAttributeValue("statecode", value);
                OnPropertyChanged();
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
                OnPropertyChanging();
                SetAttributeValue("statuscode", value);
                OnPropertyChanged();
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
                OnPropertyChanging();
                SetAttributeValue("transactioncurrencyid", value);
                OnPropertyChanged();
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
        [RelationshipSchemaName("Queue_AsyncOperations")]
        public IEnumerable<AsyncOperation> QueueAsyncOperations
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<AsyncOperation>("Queue_AsyncOperations", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("Queue_AsyncOperations", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N queue_routingruleitem
        /// </summary>
        [RelationshipSchemaName("queue_routingruleitem")]
        public IEnumerable<RoutingRuleItem> QueueRoutingruleitem
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<RoutingRuleItem>("queue_routingruleitem", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("queue_routingruleitem", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N queue_system_user
        /// </summary>
        [RelationshipSchemaName("queue_system_user")]
        public IEnumerable<SystemUser> QueueSystemUser
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<SystemUser>("queue_system_user", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("queue_system_user", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N queue_team
        /// </summary>
        [RelationshipSchemaName("queue_team")]
        public IEnumerable<Team> QueueTeam
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<Team>("queue_team", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("queue_team", null, value);
                OnPropertyChanged();
            }
        }
        #endregion

        #region Options
        public static partial class Options
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
                public const int EmailMessagesFromDynamics365LeadsContactsAndAccounts = 2;
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
        public static partial class LogicalNames
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
            public const string Name = "name";
            public const string NumberOfItems = "numberofitems";
            public const string NumberOfMembers = "numberofmembers";
            public const string OrganizationId = "organizationid";
            public const string OutgoingEmailDeliveryMethod = "outgoingemaildeliverymethod";
            public const string OverriddenCreatedOn = "overriddencreatedon";
            public const string OwnerId = "ownerid";
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
        public static partial class Relations
        {
            public static class OneToMany
            {
                public const string ConvertruleQueue = "convertrule_queue";
                public const string EmailAcceptingentityQueue = "email_acceptingentity_queue";
                public const string MailboxRegardingQueue = "mailbox_regarding_queue";
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

            public static partial class ManyToOne
            {
                public const string BusinessUnitQueues = "business_unit_queues";
                public const string BusinessUnitQueues2 = "business_unit_queues2";
                public const string LkQueueCreatedonbehalfby = "lk_queue_createdonbehalfby";
                public const string LkQueueEntityimage = "lk_queue_entityimage";
                public const string LkQueueModifiedonbehalfby = "lk_queue_modifiedonbehalfby";
                public const string LkQueueQueueItemCount = "lk_queue_QueueItemCount";
                public const string LkQueueQueueMemberCount = "lk_queue_QueueMemberCount";
                public const string LkQueuebaseCreatedby = "lk_queuebase_createdby";
                public const string LkQueuebaseModifiedby = "lk_queuebase_modifiedby";
                public const string OrganizationQueues = "organization_queues";
                public const string OwnerQueues = "owner_queues";
                public const string QueueDefaultmailboxMailbox = "queue_defaultmailbox_mailbox";
                public const string QueuePrimaryUser = "queue_primary_user";
                public const string TransactionCurrencyQueue = "TransactionCurrency_Queue";
            }

            public static partial class ManyToMany
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
            return Retrieve(service, id, new ColumnSet(true));
        }

        public static Queue Retrieve(IOrganizationService service, Guid id, ColumnSet columnSet)
        {
            return service.Retrieve("queue", id, columnSet).ToEntity<Queue>();
        }

        public Queue GetChangedEntity()
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
            return new Queue(Id) { Attributes = attr };
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
