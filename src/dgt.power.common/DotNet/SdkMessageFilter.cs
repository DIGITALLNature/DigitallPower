using System.Diagnostics.CodeAnalysis;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System.Runtime.Serialization;

// ReSharper disable All
namespace dgt.power.dataverse
{
    /// <inheritdoc cref="Microsoft.Xrm.Sdk.Entity" />
    /// <summary>
	/// Filter that defines which SDK messages are valid for each type of entity.
	/// </summary>
    [EntityLogicalName("sdkmessagefilter")]
    [System.CodeDom.Compiler.GeneratedCode("dgtp", "2026")]
    [ExcludeFromCodeCoverage]
    [SuppressMessage("Design", "CA1034")]
    [SuppressMessage("Performance", "CA1815")]
    public partial class SdkMessageFilter : Entity
    {
        #region ctor
        public SdkMessageFilter() : base(EntityLogicalName) { }

        public SdkMessageFilter(Guid id) : base(EntityLogicalName, id) { }

        public SdkMessageFilter(KeyAttributeCollection keyAttributes) : base(EntityLogicalName, keyAttributes) { }

        public SdkMessageFilter(string keyName, object keyValue) : base(EntityLogicalName, keyName, keyValue) { }
        #endregion

        #region consts
        public const string EntityLogicalName = "sdkmessagefilter";
        public const string PrimaryNameAttribute = "name";
        public const int EntityTypeCode = 4607;
        #endregion

        #region Attributes
        [AttributeLogicalName("sdkmessagefilterid")]
        [IgnoreDataMember]
        public new Guid Id
        {
            get
            {
                return base.Id;
            }
            set
            {
                SdkMessageFilterId = value;
            }
        }

        /// <summary>
		/// Unique identifier of the SDK message filter entity.
		/// </summary>
        [AttributeLogicalName("sdkmessagefilterid")]
        public Guid? SdkMessageFilterId
        {
            get
            {
                return GetAttributeValue<Guid?>("sdkmessagefilterid");
            }
            set
            {
                SetAttributeValue("sdkmessagefilterid", value);
                base.Id = value.HasValue ? value.Value : Guid.Empty;
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
		/// Unique identifier of the user who created the SDK message filter.
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
		/// Date and time when the SDK message filter was created.
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
		/// Unique identifier of the delegate user who created the sdkmessagefilter.
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
		/// Customization level of the SDK message filter.
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
		/// Indicates whether a custom SDK message processing step is allowed.
		/// </summary>
        [AttributeLogicalName("iscustomprocessingstepallowed")]
        public bool? IsCustomProcessingStepAllowed
        {
            get
            {
                return GetAttributeValue<bool?>("iscustomprocessingstepallowed");
            }
            set
            {
                SetAttributeValue("iscustomprocessingstepallowed", value);
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
		/// Indicates whether the filter should be visible.
		/// </summary>
        [AttributeLogicalName("isvisible")]
        public bool? IsVisible
        {
            get
            {
                return GetAttributeValue<bool?>("isvisible");
            }
        }

        /// <summary>
		/// Unique identifier of the user who last modified the SDK message filter.
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
		/// Date and time when the SDK message filter was last modified.
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
		/// Unique identifier of the delegate user who last modified the sdkmessagefilter.
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
		/// Name of the SDK message filter.
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
		/// Unique identifier of the organization with which the SDK message filter is associated.
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
		/// Type of entity with which the SDK message filter is primarily associated.
		/// </summary>
        [AttributeLogicalName("primaryobjecttypecode")]
        public string? PrimaryObjectTypeCode
        {
            get
            {
                return GetAttributeValue<string?>("primaryobjecttypecode");
            }
        }

        /// <summary>
		/// For internal use only.
		/// </summary>
        [AttributeLogicalName("restrictionlevel")]
        public int? RestrictionLevel
        {
            get
            {
                return GetAttributeValue<int?>("restrictionlevel");
            }
            set
            {
                SetAttributeValue("restrictionlevel", value);
            }
        }

        /// <summary>
		/// Unique identifier of the SDK message filter.
		/// </summary>
        [AttributeLogicalName("sdkmessagefilteridunique")]
        public Guid? SdkMessageFilterIdUnique
        {
            get
            {
                return GetAttributeValue<Guid?>("sdkmessagefilteridunique");
            }
        }

        /// <summary>
		/// Unique identifier of the related SDK message.
		/// </summary>
        [AttributeLogicalName("sdkmessageid")]
        public EntityReference? SdkMessageId
        {
            get
            {
                return GetAttributeValue<EntityReference?>("sdkmessageid");
            }
            set
            {
                SetAttributeValue("sdkmessageid", value);
            }
        }

        /// <summary>
		/// Type of entity with which the SDK message filter is secondarily associated.
		/// </summary>
        [AttributeLogicalName("secondaryobjecttypecode")]
        public string? SecondaryObjectTypeCode
        {
            get
            {
                return GetAttributeValue<string?>("secondaryobjecttypecode");
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
        /// 1:N sdkmessagefilterid_sdkmessageprocessingstep
        /// </summary>
        [RelationshipSchemaName("sdkmessagefilterid_sdkmessageprocessingstep")]
        public IEnumerable<SdkMessageProcessingStep> SdkmessagefilteridSdkmessageprocessingstep
        {
            get
            {
                return GetRelatedEntities<SdkMessageProcessingStep>("sdkmessagefilterid_sdkmessageprocessingstep", null);
            }
            set
            {
                SetRelatedEntities("sdkmessagefilterid_sdkmessageprocessingstep", null, value);
            }
        }
        #endregion

        #region Options
        public static partial class Options
        {
            public struct ComponentState
            {
                public const int Published = 0;
                public const int Unpublished = 1;
                public const int Deleted = 2;
                public const int DeletedUnpublished = 3;
            }
            public struct IsCustomProcessingStepAllowed
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsManaged
            {
                public const bool Unmanaged = false;
                public const bool Managed = true;
            }
            public struct IsVisible
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
            public const string SdkMessageFilterId = "sdkmessagefilterid";
            public const string Availability = "availability";
            public const string ComponentState = "componentstate";
            public const string CreatedBy = "createdby";
            public const string CreatedOn = "createdon";
            public const string CreatedOnBehalfBy = "createdonbehalfby";
            public const string CustomizationLevel = "customizationlevel";
            public const string IntroducedVersion = "introducedversion";
            public const string IsCustomProcessingStepAllowed = "iscustomprocessingstepallowed";
            public const string IsManaged = "ismanaged";
            public const string IsVisible = "isvisible";
            public const string ModifiedBy = "modifiedby";
            public const string ModifiedOn = "modifiedon";
            public const string ModifiedOnBehalfBy = "modifiedonbehalfby";
            public const string Name = "name";
            public const string OrganizationId = "organizationid";
            public const string OverwriteTime = "overwritetime";
            public const string PrimaryObjectTypeCode = "primaryobjecttypecode";
            public const string RestrictionLevel = "restrictionlevel";
            public const string SdkMessageFilterIdUnique = "sdkmessagefilteridunique";
            public const string SdkMessageId = "sdkmessageid";
            public const string SecondaryObjectTypeCode = "secondaryobjecttypecode";
            public const string SolutionId = "solutionid";
            public const string VersionNumber = "versionnumber";
            public const string WorkflowSdkStepEnabled = "workflowsdkstepenabled";
        }
        #endregion

        #region Relations
        public static partial class Relations
        {
            public static class OneToMany
            {
                public const string SdkmessagefilterInternalcatalogassignment = "sdkmessagefilter_internalcatalogassignment";
                public const string SdkmessagefilteridSdkmessageprocessingstep = "sdkmessagefilterid_sdkmessageprocessingstep";
                public const string UserentityinstancedataSdkmessagefilter = "userentityinstancedata_sdkmessagefilter";
            }

            public static partial class ManyToOne
            {
                public const string CreatedbySdkmessagefilter = "createdby_sdkmessagefilter";
                public const string LkSdkmessagefilterCreatedonbehalfby = "lk_sdkmessagefilter_createdonbehalfby";
                public const string LkSdkmessagefilterModifiedonbehalfby = "lk_sdkmessagefilter_modifiedonbehalfby";
                public const string ModifiedbySdkmessagefilter = "modifiedby_sdkmessagefilter";
                public const string OrganizationSdkmessagefilter = "organization_sdkmessagefilter";
                public const string SdkmessageidSdkmessagefilter = "sdkmessageid_sdkmessagefilter";
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
        public IQueryable<SdkMessageFilter> SdkMessageFilterSet
        {
            get
            {
                return CreateQuery<SdkMessageFilter>();
            }
        }
    }
    #endregion
}
