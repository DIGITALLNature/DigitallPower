using System.Diagnostics.CodeAnalysis;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System.Runtime.Serialization;

// ReSharper disable All
namespace dgt.power.dataverse
{
    /// <inheritdoc cref="Microsoft.Xrm.Sdk.Entity" />
    /// <summary>
	/// Non-public custom configuration that is passed to a plug-in's constructor.
	/// </summary>
    [EntityLogicalName("sdkmessageprocessingstepsecureconfig")]
    [System.CodeDom.Compiler.GeneratedCode("dgtp", "2026")]
    [ExcludeFromCodeCoverage]
    [SuppressMessage("Design", "CA1034")]
    [SuppressMessage("Performance", "CA1815")]
    public partial class SdkMessageProcessingStepSecureConfig : Entity
    {
        #region ctor
        public SdkMessageProcessingStepSecureConfig() : base(EntityLogicalName) { }

        public SdkMessageProcessingStepSecureConfig(Guid id) : base(EntityLogicalName, id) { }

        public SdkMessageProcessingStepSecureConfig(KeyAttributeCollection keyAttributes) : base(EntityLogicalName, keyAttributes) { }

        public SdkMessageProcessingStepSecureConfig(string keyName, object keyValue) : base(EntityLogicalName, keyName, keyValue) { }
        #endregion

        #region consts
        public const string EntityLogicalName = "sdkmessageprocessingstepsecureconfig";
        public const int EntityTypeCode = 4616;
        #endregion

        #region Attributes
        [AttributeLogicalName("sdkmessageprocessingstepsecureconfigid")]
        [IgnoreDataMember]
        public new Guid Id
        {
            get
            {
                return base.Id;
            }
            set
            {
                SdkMessageProcessingStepSecureConfigId = value;
            }
        }

        /// <summary>
		/// Unique identifier of the SDK message processing step secure configuration.
		/// </summary>
        [AttributeLogicalName("sdkmessageprocessingstepsecureconfigid")]
        public Guid? SdkMessageProcessingStepSecureConfigId
        {
            get
            {
                return GetAttributeValue<Guid?>("sdkmessageprocessingstepsecureconfigid");
            }
            set
            {
                SetAttributeValue("sdkmessageprocessingstepsecureconfigid", value);
                base.Id = value.HasValue ? value.Value : Guid.Empty;
            }
        }

        /// <summary>
		/// Unique identifier of the user who created the SDK message processing step.
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
		/// Date and time when the SDK message processing step was created.
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
		/// Unique identifier of the delegate user who created the sdkmessageprocessingstepsecureconfig.
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
		/// Customization level of the SDK message processing step secure configuration.
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
		/// Unique identifier of the user who last modified the SDK message processing step.
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
		/// Date and time when the SDK message processing step was last modified.
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
		/// Unique identifier of the delegate user who last modified the sdkmessageprocessingstepsecureconfig.
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
		/// Unique identifier of the organization with which the SDK message processing step is associated.
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
		/// Unique identifier of the SDK message processing step.
		/// </summary>
        [AttributeLogicalName("sdkmessageprocessingstepsecureconfigidunique")]
        public Guid? SdkMessageProcessingStepSecureConfigIdUnique
        {
            get
            {
                return GetAttributeValue<Guid?>("sdkmessageprocessingstepsecureconfigidunique");
            }
        }

        /// <summary>
		/// Secure step-specific configuration for the plug-in type that is passed to the plug-in's constructor at run time.
		/// </summary>
        [AttributeLogicalName("secureconfig")]
        public string? SecureConfig
        {
            get
            {
                return GetAttributeValue<string?>("secureconfig");
            }
            set
            {
                SetAttributeValue("secureconfig", value);
            }
        }
        #endregion

        #region NavigationProperties

        /// <summary>
        /// 1:N sdkmessageprocessingstepsecureconfigid_sdkmessageprocessingstep
        /// </summary>
        [RelationshipSchemaName("sdkmessageprocessingstepsecureconfigid_sdkmessageprocessingstep")]
        public IEnumerable<SdkMessageProcessingStep> SdkmessageprocessingstepsecureconfigidSdkmessageprocessingstep
        {
            get
            {
                return GetRelatedEntities<SdkMessageProcessingStep>("sdkmessageprocessingstepsecureconfigid_sdkmessageprocessingstep", null);
            }
            set
            {
                SetRelatedEntities("sdkmessageprocessingstepsecureconfigid_sdkmessageprocessingstep", null, value);
            }
        }
        #endregion

        #region Options
        public static partial class Options
        {
        }
        #endregion

        #region LogicalNames
        public static partial class LogicalNames
        {
            public const string SdkMessageProcessingStepSecureConfigId = "sdkmessageprocessingstepsecureconfigid";
            public const string CreatedBy = "createdby";
            public const string CreatedOn = "createdon";
            public const string CreatedOnBehalfBy = "createdonbehalfby";
            public const string CustomizationLevel = "customizationlevel";
            public const string ModifiedBy = "modifiedby";
            public const string ModifiedOn = "modifiedon";
            public const string ModifiedOnBehalfBy = "modifiedonbehalfby";
            public const string OrganizationId = "organizationid";
            public const string SdkMessageProcessingStepSecureConfigIdUnique = "sdkmessageprocessingstepsecureconfigidunique";
            public const string SecureConfig = "secureconfig";
        }
        #endregion

        #region Relations
        public static partial class Relations
        {
            public static class OneToMany
            {
                public const string SdkmessageprocessingstepsecureconfigDeletedItemReferences = "sdkmessageprocessingstepsecureconfig_DeletedItemReferences";
                public const string SdkmessageprocessingstepsecureconfigidSdkmessageprocessingstep = "sdkmessageprocessingstepsecureconfigid_sdkmessageprocessingstep";
                public const string UserentityinstancedataSdkmessageprocessingstepsecureconfig = "userentityinstancedata_sdkmessageprocessingstepsecureconfig";
            }

            public static partial class ManyToOne
            {
                public const string CreatedbySdkmessageprocessingstepsecureconfig = "createdby_sdkmessageprocessingstepsecureconfig";
                public const string LkSdkmessageprocessingstepsecureconfigCreatedonbehalfby = "lk_sdkmessageprocessingstepsecureconfig_createdonbehalfby";
                public const string LkSdkmessageprocessingstepsecureconfigModifiedonbehalfby = "lk_sdkmessageprocessingstepsecureconfig_modifiedonbehalfby";
                public const string ModifiedbySdkmessageprocessingstepsecureconfig = "modifiedby_sdkmessageprocessingstepsecureconfig";
                public const string OrganizationSdkmessageprocessingstepsecureconfig = "organization_sdkmessageprocessingstepsecureconfig";
            }

            public static partial class ManyToMany
            {
            }
        }
        #endregion

        #region Methods
        #endregion
    }

    #region Context
    public partial class DataContext
    {
        public IQueryable<SdkMessageProcessingStepSecureConfig> SdkMessageProcessingStepSecureConfigSet
        {
            get
            {
                return CreateQuery<SdkMessageProcessingStepSecureConfig>();
            }
        }
    }
    #endregion
}
