using System.Diagnostics.CodeAnalysis;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System.Runtime.Serialization;

// ReSharper disable All
namespace dgt.power.dataverse
{
    /// <inheritdoc cref="Microsoft.Xrm.Sdk.Entity" />
    /// <summary>
	/// Copy of an entity's attributes before or after the core system operation.
	/// </summary>
    [EntityLogicalName("sdkmessageprocessingstepimage")]
    [System.CodeDom.Compiler.GeneratedCode("dgtp", "2026")]
    [ExcludeFromCodeCoverage]
    [SuppressMessage("Design", "CA1034")]
    [SuppressMessage("Performance", "CA1815")]
    public partial class SdkMessageProcessingStepImage : Entity
    {
        #region ctor
        public SdkMessageProcessingStepImage() : base(EntityLogicalName) { }

        public SdkMessageProcessingStepImage(Guid id) : base(EntityLogicalName, id) { }

        public SdkMessageProcessingStepImage(KeyAttributeCollection keyAttributes) : base(EntityLogicalName, keyAttributes) { }

        public SdkMessageProcessingStepImage(string keyName, object keyValue) : base(EntityLogicalName, keyName, keyValue) { }
        #endregion

        #region consts
        public const string EntityLogicalName = "sdkmessageprocessingstepimage";
        public const string PrimaryNameAttribute = "name";
        public const int EntityTypeCode = 4615;
        #endregion

        #region Attributes
        [AttributeLogicalName("sdkmessageprocessingstepimageid")]
        [IgnoreDataMember]
        public new Guid Id
        {
            get
            {
                return base.Id;
            }
            set
            {
                SdkMessageProcessingStepImageId = value;
            }
        }

        /// <summary>
		/// Unique identifier of the SDK message processing step image entity.
		/// </summary>
        [AttributeLogicalName("sdkmessageprocessingstepimageid")]
        public Guid? SdkMessageProcessingStepImageId
        {
            get
            {
                return GetAttributeValue<Guid?>("sdkmessageprocessingstepimageid");
            }
            set
            {
                SetAttributeValue("sdkmessageprocessingstepimageid", value);
                base.Id = value.HasValue ? value.Value : Guid.Empty;
            }
        }

        /// <summary>
		/// Comma-separated list of attributes that are to be passed into the SDK message processing step image.
		/// </summary>
        [AttributeLogicalName("attributes")]
        public string? AttributesField
        {
            get
            {
                return GetAttributeValue<string?>("attributes");
            }
            set
            {
                SetAttributeValue("attributes", value);
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
		/// Unique identifier of the user who created the SDK message processing step image.
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
		/// Date and time when the SDK message processing step image was created.
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
		/// Unique identifier of the delegate user who created the sdkmessageprocessingstepimage.
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
		/// Customization level of the SDK message processing step image.
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
		/// Description of the SDK message processing step image.
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
		/// Key name used to access the pre-image or post-image property bags in a step.
		/// </summary>
        [AttributeLogicalName("entityalias")]
        public string? EntityAlias
        {
            get
            {
                return GetAttributeValue<string?>("entityalias");
            }
            set
            {
                SetAttributeValue("entityalias", value);
            }
        }

        /// <summary>
		/// Type of image requested.
		/// </summary>
        [AttributeLogicalName("imagetype")]
        public OptionSetValue? ImageType
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("imagetype");
            }
            set
            {
                SetAttributeValue("imagetype", value);
            }
        }

        /// <summary>
		/// Version in which the form is introduced.
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
		/// Information that specifies whether this component can be customized.
		/// </summary>
        [AttributeLogicalName("iscustomizable")]
        public BooleanManagedProperty? IsCustomizable
        {
            get
            {
                return GetAttributeValue<BooleanManagedProperty?>("iscustomizable");
            }
            set
            {
                SetAttributeValue("iscustomizable", value);
            }
        }

        
        [AttributeLogicalName("ismanaged")]
        public bool? IsManaged
        {
            get
            {
                return GetAttributeValue<bool?>("ismanaged");
            }
        }

        /// <summary>
		/// Name of the property on the Request message.
		/// </summary>
        [AttributeLogicalName("messagepropertyname")]
        public string? MessagePropertyName
        {
            get
            {
                return GetAttributeValue<string?>("messagepropertyname");
            }
            set
            {
                SetAttributeValue("messagepropertyname", value);
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
		/// Unique identifier of the delegate user who last modified the sdkmessageprocessingstepimage.
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
		/// Name of SdkMessage processing step image.
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
		/// Name of the related entity.
		/// </summary>
        [AttributeLogicalName("relatedattributename")]
        public string? RelatedAttributeName
        {
            get
            {
                return GetAttributeValue<string?>("relatedattributename");
            }
            set
            {
                SetAttributeValue("relatedattributename", value);
            }
        }

        /// <summary>
		/// Unique identifier of the SDK message processing step.
		/// </summary>
        [AttributeLogicalName("sdkmessageprocessingstepid")]
        public EntityReference? SdkMessageProcessingStepId
        {
            get
            {
                return GetAttributeValue<EntityReference?>("sdkmessageprocessingstepid");
            }
            set
            {
                SetAttributeValue("sdkmessageprocessingstepid", value);
            }
        }

        /// <summary>
		/// Unique identifier of the SDK message processing step image.
		/// </summary>
        [AttributeLogicalName("sdkmessageprocessingstepimageidunique")]
        public Guid? SdkMessageProcessingStepImageIdUnique
        {
            get
            {
                return GetAttributeValue<Guid?>("sdkmessageprocessingstepimageidunique");
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
		/// Number that identifies a specific revision of the step image.
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
            public struct ImageType
            {
                public const int PreImage = 0;
                public const int PostImage = 1;
                public const int Both = 2;
            }
            public struct IsManaged
            {
                public const bool Unmanaged = false;
                public const bool Managed = true;
            }
        }
        #endregion

        #region LogicalNames
        public static partial class LogicalNames
        {
            public const string SdkMessageProcessingStepImageId = "sdkmessageprocessingstepimageid";
            public const string Attributes = "attributes";
            public const string ComponentState = "componentstate";
            public const string CreatedBy = "createdby";
            public const string CreatedOn = "createdon";
            public const string CreatedOnBehalfBy = "createdonbehalfby";
            public const string CustomizationLevel = "customizationlevel";
            public const string Description = "description";
            public const string EntityAlias = "entityalias";
            public const string ImageType = "imagetype";
            public const string IntroducedVersion = "introducedversion";
            public const string IsCustomizable = "iscustomizable";
            public const string IsManaged = "ismanaged";
            public const string MessagePropertyName = "messagepropertyname";
            public const string ModifiedBy = "modifiedby";
            public const string ModifiedOn = "modifiedon";
            public const string ModifiedOnBehalfBy = "modifiedonbehalfby";
            public const string Name = "name";
            public const string OrganizationId = "organizationid";
            public const string OverwriteTime = "overwritetime";
            public const string RelatedAttributeName = "relatedattributename";
            public const string SdkMessageProcessingStepId = "sdkmessageprocessingstepid";
            public const string SdkMessageProcessingStepImageIdUnique = "sdkmessageprocessingstepimageidunique";
            public const string SolutionId = "solutionid";
            public const string VersionNumber = "versionnumber";
        }
        #endregion

        #region Relations
        public static partial class Relations
        {
            public static class OneToMany
            {
                public const string UserentityinstancedataSdkmessageprocessingstepimage = "userentityinstancedata_sdkmessageprocessingstepimage";
            }

            public static partial class ManyToOne
            {
                public const string CreatedbySdkmessageprocessingstepimage = "createdby_sdkmessageprocessingstepimage";
                public const string LkSdkmessageprocessingstepimageCreatedonbehalfby = "lk_sdkmessageprocessingstepimage_createdonbehalfby";
                public const string LkSdkmessageprocessingstepimageModifiedonbehalfby = "lk_sdkmessageprocessingstepimage_modifiedonbehalfby";
                public const string ModifiedbySdkmessageprocessingstepimage = "modifiedby_sdkmessageprocessingstepimage";
                public const string OrganizationSdkmessageprocessingstepimage = "organization_sdkmessageprocessingstepimage";
                public const string SdkmessageprocessingstepidSdkmessageprocessingstepimage = "sdkmessageprocessingstepid_sdkmessageprocessingstepimage";
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
        public IQueryable<SdkMessageProcessingStepImage> SdkMessageProcessingStepImageSet
        {
            get
            {
                return CreateQuery<SdkMessageProcessingStepImage>();
            }
        }
    }
    #endregion
}
