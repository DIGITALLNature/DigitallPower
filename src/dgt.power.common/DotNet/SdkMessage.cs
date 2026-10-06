using System.Diagnostics.CodeAnalysis;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System.Runtime.Serialization;

// ReSharper disable All
namespace dgt.power.dataverse
{
    /// <inheritdoc cref="Microsoft.Xrm.Sdk.Entity" />
    /// <summary>
	/// Message that is supported by the SDK.
	/// </summary>
    [EntityLogicalName("sdkmessage")]
    [System.CodeDom.Compiler.GeneratedCode("dgtp", "2026")]
    [ExcludeFromCodeCoverage]
    [SuppressMessage("Design", "CA1034")]
    [SuppressMessage("Performance", "CA1815")]
    public partial class SdkMessage : Entity
    {
        #region ctor
        public SdkMessage() : base(EntityLogicalName) { }

        public SdkMessage(Guid id) : base(EntityLogicalName, id) { }

        public SdkMessage(KeyAttributeCollection keyAttributes) : base(EntityLogicalName, keyAttributes) { }

        public SdkMessage(string keyName, object keyValue) : base(EntityLogicalName, keyName, keyValue) { }
        #endregion

        #region consts
        public const string EntityLogicalName = "sdkmessage";
        public const string PrimaryNameAttribute = "name";
        public const int EntityTypeCode = 4606;
        #endregion

        #region Attributes
        [AttributeLogicalName("sdkmessageid")]
        [IgnoreDataMember]
        public new Guid Id
        {
            get
            {
                return base.Id;
            }
            set
            {
                SdkMessageId = value;
            }
        }

        /// <summary>
		/// Unique identifier of the SDK message entity.
		/// </summary>
        [AttributeLogicalName("sdkmessageid")]
        public Guid? SdkMessageId
        {
            get
            {
                return GetAttributeValue<Guid?>("sdkmessageid");
            }
            set
            {
                SetAttributeValue("sdkmessageid", value);
                base.Id = value.HasValue ? value.Value : Guid.Empty;
            }
        }

        /// <summary>
		/// Information about whether the SDK message is automatically transacted.
		/// </summary>
        [AttributeLogicalName("autotransact")]
        public bool? AutoTransact
        {
            get
            {
                return GetAttributeValue<bool?>("autotransact");
            }
            set
            {
                SetAttributeValue("autotransact", value);
            }
        }

        /// <summary>
		/// Identifies where a method will be exposed. 0 - Server, 1 - Client, 2 - both.
		/// </summary>
        [AttributeLogicalName("availability")]
        public int? Availability
        {
            get
            {
                return GetAttributeValue<int?>("availability");
            }
            set
            {
                SetAttributeValue("availability", value);
            }
        }

        /// <summary>
		/// If this is a categorized method, this is the name, otherwise None.
		/// </summary>
        [AttributeLogicalName("categoryname")]
        public string? CategoryName
        {
            get
            {
                return GetAttributeValue<string?>("categoryname");
            }
            set
            {
                SetAttributeValue("categoryname", value);
            }
        }

        /// <summary>
		/// For internal use only.
		/// </summary>
        [AttributeLogicalName("componentstate")]
        public OptionSetValue? ComponentState
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("componentstate");
            }
        }

        /// <summary>
		/// Unique identifier of the user who created the SDK message.
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
		/// Date and time when the SDK message was created.
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
		/// Unique identifier of the delegate user who created the sdkmessage.
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
		/// Customization level of the SDK message.
		/// </summary>
        [AttributeLogicalName("customizationlevel")]
        public int? CustomizationLevel
        {
            get
            {
                return GetAttributeValue<int?>("customizationlevel");
            }
        }

        /// <summary>
		/// Name of the privilege that allows execution of the SDK message
		/// </summary>
        [AttributeLogicalName("executeprivilegename")]
        public string? ExecutePrivilegeName
        {
            get
            {
                return GetAttributeValue<string?>("executeprivilegename");
            }
            set
            {
                SetAttributeValue("executeprivilegename", value);
            }
        }

        /// <summary>
		/// Indicates whether the SDK message should have its requests expanded per primary entity defined in its filters.
		/// </summary>
        [AttributeLogicalName("expand")]
        public bool? Expand
        {
            get
            {
                return GetAttributeValue<bool?>("expand");
            }
            set
            {
                SetAttributeValue("expand", value);
            }
        }

        /// <summary>
		/// Version in which the component is introduced.
		/// </summary>
        [AttributeLogicalName("introducedversion")]
        public string? IntroducedVersion
        {
            get
            {
                return GetAttributeValue<string?>("introducedversion");
            }
            set
            {
                SetAttributeValue("introducedversion", value);
            }
        }

        /// <summary>
		/// Information about whether the SDK message is active.
		/// </summary>
        [AttributeLogicalName("isactive")]
        public bool? IsActive
        {
            get
            {
                return GetAttributeValue<bool?>("isactive");
            }
            set
            {
                SetAttributeValue("isactive", value);
            }
        }

        /// <summary>
		/// Information that specifies whether this component is managed.
		/// </summary>
        [AttributeLogicalName("ismanaged")]
        public bool? IsManaged
        {
            get
            {
                return GetAttributeValue<bool?>("ismanaged");
            }
        }

        /// <summary>
		/// Indicates whether the SDK message is private.
		/// </summary>
        [AttributeLogicalName("isprivate")]
        public bool? IsPrivate
        {
            get
            {
                return GetAttributeValue<bool?>("isprivate");
            }
            set
            {
                SetAttributeValue("isprivate", value);
            }
        }

        /// <summary>
		/// Identifies whether an SDK message will be ReadOnly or Read Write. false - ReadWrite, true - ReadOnly .
		/// </summary>
        [AttributeLogicalName("isreadonly")]
        public bool? IsReadOnly
        {
            get
            {
                return GetAttributeValue<bool?>("isreadonly");
            }
            set
            {
                SetAttributeValue("isreadonly", value);
            }
        }

        /// <summary>
		/// For internal use only.
		/// </summary>
        [AttributeLogicalName("isvalidforexecuteasync")]
        public bool? IsValidForExecuteAsync
        {
            get
            {
                return GetAttributeValue<bool?>("isvalidforexecuteasync");
            }
        }

        /// <summary>
		/// Unique identifier of the user who last modified the SDK message.
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
		/// Date and time when the SDK message was last modified.
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
		/// Unique identifier of the delegate user who last modified the sdkmessage.
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
		/// Name of the SDK message.
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
		/// Unique identifier of the organization with which the SDK message is associated.
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
		/// For internal use only.
		/// </summary>
        [AttributeLogicalName("overwritetime")]
        public DateTime? OverwriteTime
        {
            get
            {
                return GetAttributeValue<DateTime?>("overwritetime");
            }
        }

        /// <summary>
		/// Unique identifier of the SDK message.
		/// </summary>
        [AttributeLogicalName("sdkmessageidunique")]
        public Guid? SdkMessageIdUnique
        {
            get
            {
                return GetAttributeValue<Guid?>("sdkmessageidunique");
            }
        }

        /// <summary>
		/// Unique identifier of the associated solution.
		/// </summary>
        [AttributeLogicalName("solutionid")]
        public Guid? SolutionId
        {
            get
            {
                return GetAttributeValue<Guid?>("solutionid");
            }
        }

        /// <summary>
		/// Indicates whether the SDK message is a template.
		/// </summary>
        [AttributeLogicalName("template")]
        public bool? Template
        {
            get
            {
                return GetAttributeValue<bool?>("template");
            }
            set
            {
                SetAttributeValue("template", value);
            }
        }

        /// <summary>
		/// For internal use only.
		/// </summary>
        [AttributeLogicalName("throttlesettings")]
        public string? ThrottleSettings
        {
            get
            {
                return GetAttributeValue<string?>("throttlesettings");
            }
        }

        /// <summary>
		/// Number that identifies a specific revision of the SDK message.
		/// </summary>
        [AttributeLogicalName("versionnumber")]
        public long? VersionNumber
        {
            get
            {
                return GetAttributeValue<long?>("versionnumber");
            }
        }

        /// <summary>
		/// Whether or not the SDK message can be called from a workflow.
		/// </summary>
        [AttributeLogicalName("workflowsdkstepenabled")]
        public bool? WorkflowSdkStepEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("workflowsdkstepenabled");
            }
        }
        #endregion

        #region NavigationProperties

        /// <summary>
        /// 1:N sdkmessage_customapi
        /// </summary>
        [RelationshipSchemaName("sdkmessage_customapi")]
        public IEnumerable<CustomAPI> SdkmessageCustomapi
        {
            get
            {
                return GetRelatedEntities<CustomAPI>("sdkmessage_customapi", null);
            }
            set
            {
                SetRelatedEntities("sdkmessage_customapi", null, value);
            }
        }

        /// <summary>
        /// 1:N sdkmessageid_sdkmessagefilter
        /// </summary>
        [RelationshipSchemaName("sdkmessageid_sdkmessagefilter")]
        public IEnumerable<SdkMessageFilter> SdkmessageidSdkmessagefilter
        {
            get
            {
                return GetRelatedEntities<SdkMessageFilter>("sdkmessageid_sdkmessagefilter", null);
            }
            set
            {
                SetRelatedEntities("sdkmessageid_sdkmessagefilter", null, value);
            }
        }

        /// <summary>
        /// 1:N sdkmessageid_sdkmessageprocessingstep
        /// </summary>
        [RelationshipSchemaName("sdkmessageid_sdkmessageprocessingstep")]
        public IEnumerable<SdkMessageProcessingStep> SdkmessageidSdkmessageprocessingstep
        {
            get
            {
                return GetRelatedEntities<SdkMessageProcessingStep>("sdkmessageid_sdkmessageprocessingstep", null);
            }
            set
            {
                SetRelatedEntities("sdkmessageid_sdkmessageprocessingstep", null, value);
            }
        }
        #endregion

        #region Options
        public static partial class Options
        {
            public struct AutoTransact
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct ComponentState
            {
                public const int Published = 0;
                public const int Unpublished = 1;
                public const int Deleted = 2;
                public const int DeletedUnpublished = 3;
            }
            public struct Expand
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsActive
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsManaged
            {
                public const bool Unmanaged = false;
                public const bool Managed = true;
            }
            public struct IsPrivate
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsReadOnly
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsValidForExecuteAsync
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct Template
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct WorkflowSdkStepEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
        }
        #endregion

        #region LogicalNames
        public static partial class LogicalNames
        {
            public const string SdkMessageId = "sdkmessageid";
            public const string AutoTransact = "autotransact";
            public const string Availability = "availability";
            public const string CategoryName = "categoryname";
            public const string ComponentState = "componentstate";
            public const string CreatedBy = "createdby";
            public const string CreatedOn = "createdon";
            public const string CreatedOnBehalfBy = "createdonbehalfby";
            public const string CustomizationLevel = "customizationlevel";
            public const string ExecutePrivilegeName = "executeprivilegename";
            public const string Expand = "expand";
            public const string IntroducedVersion = "introducedversion";
            public const string IsActive = "isactive";
            public const string IsManaged = "ismanaged";
            public const string IsPrivate = "isprivate";
            public const string IsReadOnly = "isreadonly";
            public const string IsValidForExecuteAsync = "isvalidforexecuteasync";
            public const string ModifiedBy = "modifiedby";
            public const string ModifiedOn = "modifiedon";
            public const string ModifiedOnBehalfBy = "modifiedonbehalfby";
            public const string Name = "name";
            public const string OrganizationId = "organizationid";
            public const string OverwriteTime = "overwritetime";
            public const string SdkMessageIdUnique = "sdkmessageidunique";
            public const string SolutionId = "solutionid";
            public const string Template = "template";
            public const string ThrottleSettings = "throttlesettings";
            public const string VersionNumber = "versionnumber";
            public const string WorkflowSdkStepEnabled = "workflowsdkstepenabled";
        }
        #endregion

        #region Relations
        public static partial class Relations
        {
            public static class OneToMany
            {
                public const string MessageSdkmessagepair = "message_sdkmessagepair";
                public const string SdkmessageAiskillconfigSdkmessageid = "sdkmessage_aiskillconfig_sdkmessageid";
                public const string SdkmessageCustomapi = "sdkmessage_customapi";
                public const string SdkmessageServiceplanmapping = "sdkmessage_serviceplanmapping";
                public const string SdkmessageidSdkmessagefilter = "sdkmessageid_sdkmessagefilter";
                public const string SdkmessageidSdkmessageprocessingstep = "sdkmessageid_sdkmessageprocessingstep";
                public const string SdkmessageidWorkflowDependency = "sdkmessageid_workflow_dependency";
                public const string UserentityinstancedataSdkmessage = "userentityinstancedata_sdkmessage";
            }

            public static partial class ManyToOne
            {
                public const string CreatedbySdkmessage = "createdby_sdkmessage";
                public const string LkSdkmessageCreatedonbehalfby = "lk_sdkmessage_createdonbehalfby";
                public const string LkSdkmessageModifiedonbehalfby = "lk_sdkmessage_modifiedonbehalfby";
                public const string ModifiedbySdkmessage = "modifiedby_sdkmessage";
                public const string OrganizationSdkmessage = "organization_sdkmessage";
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
        public IQueryable<SdkMessage> SdkMessageSet
        {
            get
            {
                return CreateQuery<SdkMessage>();
            }
        }
    }
    #endregion
}
