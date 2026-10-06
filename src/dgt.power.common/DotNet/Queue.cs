using System.Diagnostics.CodeAnalysis;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System.Runtime.Serialization;

// ReSharper disable All
namespace dgt.power.dataverse
{
    /// <inheritdoc cref="Microsoft.Xrm.Sdk.Entity" />
    /// <summary>
	/// A list of records that require action, such as accounts, activities, and cases.
	/// </summary>
    [EntityLogicalName("queue")]
    [System.CodeDom.Compiler.GeneratedCode("dgtp", "2026")]
    [ExcludeFromCodeCoverage]
    [SuppressMessage("Design", "CA1034")]
    [SuppressMessage("Performance", "CA1815")]
    public partial class Queue : Entity
    {
        #region ctor
        public Queue() : base(EntityLogicalName) { }

        public Queue(Guid id) : base(EntityLogicalName, id) { }

        public Queue(KeyAttributeCollection keyAttributes) : base(EntityLogicalName, keyAttributes) { }

        public Queue(string keyName, object keyValue) : base(EntityLogicalName, keyName, keyValue) { }
        #endregion

        #region consts
        public const string EntityLogicalName = "queue";
        public const string PrimaryNameAttribute = "name";
        public const int EntityTypeCode = 2020;
        #endregion

        #region Attributes
        [AttributeLogicalName("queueid")]
        [IgnoreDataMember]
        public new Guid Id
        {
            get
            {
                return base.Id;
            }
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
            get
            {
                return GetAttributeValue<Guid?>("queueid");
            }
            set
            {
                SetAttributeValue("queueid", value);
                base.Id = value.HasValue ? value.Value : Guid.Empty;
            }
        }

        /// <summary>
		/// This attribute is no longer used. The data is now in the Mailbox.AllowEmailConnectorToUseCredentials attribute.
		/// </summary>
        [AttributeLogicalName("allowemailcredentials")]
        public bool? AllowEmailCredentials
        {
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
		/// Unique identifier of the user who created the queue record.
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
		/// Date and time when the queue was created.
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
		/// Unique identifier of the delegate user who created the queue.
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
		/// Select the mailbox associated with this queue.
		/// </summary>
        [AttributeLogicalName("defaultmailbox")]
        public EntityReference? DefaultMailbox
        {
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
		/// Email address that is associated with the queue.
		/// </summary>
        [AttributeLogicalName("emailaddress")]
        public string? EMailAddress
        {
            get
            {
                return GetAttributeValue<string?>("emailaddress");
            }
            set
            {
                SetAttributeValue("emailaddress", value);
            }
        }

        /// <summary>
		/// This attribute is no longer used. The data is now in the Mailbox.Password attribute.
		/// </summary>
        [AttributeLogicalName("emailpassword")]
        public string? EmailPassword
        {
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
            get
            {
                return GetAttributeValue<OptionSetValue?>("emailrouteraccessapproval");
            }
            set
            {
                SetAttributeValue("emailrouteraccessapproval", value);
            }
        }

        /// <summary>
		/// This attribute is no longer used. The data is now in the Mailbox.UserName attribute.
		/// </summary>
        [AttributeLogicalName("emailusername")]
        public string? EmailUsername
        {
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
            get
            {
                return GetAttributeValue<byte[]?>("entityimage");
            }
            set
            {
                SetAttributeValue("entityimage", value);
            }
        }

        
        [AttributeLogicalName("entityimage_timestamp")]
        public long? EntityImageTimestamp
        {
            get
            {
                return GetAttributeValue<long?>("entityimage_timestamp");
            }
        }

        
        [AttributeLogicalName("entityimage_url")]
        public string? EntityImageURL
        {
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
            get
            {
                return GetAttributeValue<bool?>("ignoreunsolicitedemail");
            }
            set
            {
                SetAttributeValue("ignoreunsolicitedemail", value);
            }
        }

        /// <summary>
		/// Unique identifier of the data import or data migration that created this record.
		/// </summary>
        [AttributeLogicalName("importsequencenumber")]
        public int? ImportSequenceNumber
        {
            get
            {
                return GetAttributeValue<int?>("importsequencenumber");
            }
            set
            {
                SetAttributeValue("importsequencenumber", value);
            }
        }

        /// <summary>
		/// Incoming email delivery method for the queue.
		/// </summary>
        [AttributeLogicalName("incomingemaildeliverymethod")]
        public OptionSetValue? IncomingEmailDeliveryMethod
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("incomingemaildeliverymethod");
            }
            set
            {
                SetAttributeValue("incomingemaildeliverymethod", value);
            }
        }

        /// <summary>
		/// Convert Incoming Email To Activities
		/// </summary>
        [AttributeLogicalName("incomingemailfilteringmethod")]
        public OptionSetValue? IncomingEmailFilteringMethod
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("incomingemailfilteringmethod");
            }
            set
            {
                SetAttributeValue("incomingemailfilteringmethod", value);
            }
        }

        /// <summary>
		/// Shows the status of approval of the email address by O365 Admin.
		/// </summary>
        [AttributeLogicalName("isemailaddressapprovedbyo365admin")]
        public bool? IsEmailAddressApprovedByO365Admin
        {
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
		/// Number of Queue items associated with the queue.
		/// </summary>
        [AttributeLogicalName("numberofitems")]
        public int? NumberOfItems
        {
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
            get
            {
                return GetAttributeValue<OptionSetValue?>("outgoingemaildeliverymethod");
            }
            set
            {
                SetAttributeValue("outgoingemaildeliverymethod", value);
            }
        }

        /// <summary>
		/// Date and time that the record was migrated.
		/// </summary>
        [AttributeLogicalName("overriddencreatedon")]
        public DateTime? OverriddenCreatedOn
        {
            get
            {
                return GetAttributeValue<DateTime?>("overriddencreatedon");
            }
            set
            {
                SetAttributeValue("overriddencreatedon", value);
            }
        }

        /// <summary>
		/// Unique identifier of the user or team who owns the queue.
		/// </summary>
        [AttributeLogicalName("ownerid")]
        public EntityReference? OwnerId
        {
            get
            {
                return GetAttributeValue<EntityReference?>("ownerid");
            }
            set
            {
                SetAttributeValue("ownerid", value);
            }
        }

        /// <summary>
		/// Unique identifier of the business unit that owns the queue.
		/// </summary>
        [AttributeLogicalName("owningbusinessunit")]
        public EntityReference? OwningBusinessUnit
        {
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
            get
            {
                return GetAttributeValue<EntityReference?>("primaryuserid");
            }
            set
            {
                SetAttributeValue("primaryuserid", value);
            }
        }

        /// <summary>
		/// Type of queue that is automatically assigned when a user or queue is created. The type can be public, private, or work in process.
		/// </summary>
        [AttributeLogicalName("queuetypecode")]
        public OptionSetValue? QueueTypeCode
        {
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
            get
            {
                return GetAttributeValue<OptionSetValue?>("queueviewtype");
            }
            set
            {
                SetAttributeValue("queueviewtype", value);
            }
        }

        /// <summary>
		/// Status of the queue.
		/// </summary>
        [AttributeLogicalName("statecode")]
        public OptionSetValue? StateCode
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("statecode");
            }
            set
            {
                SetAttributeValue("statecode", value);
            }
        }

        /// <summary>
		/// Reason for the status of the queue.
		/// </summary>
        [AttributeLogicalName("statuscode")]
        public OptionSetValue? StatusCode
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("statuscode");
            }
            set
            {
                SetAttributeValue("statuscode", value);
            }
        }

        /// <summary>
		/// Unique identifier of the currency associated with the queue.
		/// </summary>
        [AttributeLogicalName("transactioncurrencyid")]
        public EntityReference? TransactionCurrencyId
        {
            get
            {
                return GetAttributeValue<EntityReference?>("transactioncurrencyid");
            }
            set
            {
                SetAttributeValue("transactioncurrencyid", value);
            }
        }

        /// <summary>
		/// Version number of the queue.
		/// </summary>
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
        /// 1:N Queue_AsyncOperations
        /// </summary>
        [RelationshipSchemaName("Queue_AsyncOperations")]
        public IEnumerable<AsyncOperation> QueueAsyncOperations
        {
            get
            {
                return GetRelatedEntities<AsyncOperation>("Queue_AsyncOperations", null);
            }
            set
            {
                SetRelatedEntities("Queue_AsyncOperations", null, value);
            }
        }

        /// <summary>
        /// 1:N queue_routingruleitem
        /// </summary>
        [RelationshipSchemaName("queue_routingruleitem")]
        public IEnumerable<RoutingRuleItem> QueueRoutingruleitem
        {
            get
            {
                return GetRelatedEntities<RoutingRuleItem>("queue_routingruleitem", null);
            }
            set
            {
                SetRelatedEntities("queue_routingruleitem", null, value);
            }
        }

        /// <summary>
        /// 1:N queue_system_user
        /// </summary>
        [RelationshipSchemaName("queue_system_user")]
        public IEnumerable<SystemUser> QueueSystemUser
        {
            get
            {
                return GetRelatedEntities<SystemUser>("queue_system_user", null);
            }
            set
            {
                SetRelatedEntities("queue_system_user", null, value);
            }
        }

        /// <summary>
        /// 1:N queue_team
        /// </summary>
        [RelationshipSchemaName("queue_team")]
        public IEnumerable<Team> QueueTeam
        {
            get
            {
                return GetRelatedEntities<Team>("queue_team", null);
            }
            set
            {
                SetRelatedEntities("queue_team", null, value);
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
