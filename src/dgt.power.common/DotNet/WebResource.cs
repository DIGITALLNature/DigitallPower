using System.Diagnostics.CodeAnalysis;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System.Runtime.Serialization;

// ReSharper disable All
namespace dgt.power.dataverse
{
    /// <inheritdoc cref="Microsoft.Xrm.Sdk.Entity" />
    /// <summary>
	/// Data equivalent to files used in Web development. Web resources provide client-side components that are used to provide custom user interface elements.
	/// </summary>
    [EntityLogicalName("webresource")]
    [System.CodeDom.Compiler.GeneratedCode("dgtp", "2026")]
    [ExcludeFromCodeCoverage]
    [SuppressMessage("Design", "CA1034")]
    [SuppressMessage("Performance", "CA1815")]
    public partial class WebResource : Entity
    {
        #region ctor
        public WebResource() : base(EntityLogicalName) { }

        public WebResource(Guid id) : base(EntityLogicalName, id) { }

        public WebResource(KeyAttributeCollection keyAttributes) : base(EntityLogicalName, keyAttributes) { }

        public WebResource(string keyName, object keyValue) : base(EntityLogicalName, keyName, keyValue) { }
        #endregion

        #region consts
        public const string EntityLogicalName = "webresource";
        public const string PrimaryNameAttribute = "name";
        public const int EntityTypeCode = 9333;
        #endregion

        #region Attributes
        [AttributeLogicalName("webresourceid")]
        [IgnoreDataMember]
        public new Guid Id
        {
            get
            {
                return base.Id;
            }
            set
            {
                WebResourceId = value;
            }
        }

        /// <summary>
		/// Unique identifier of the web resource.
		/// </summary>
        [AttributeLogicalName("webresourceid")]
        public Guid? WebResourceId
        {
            get
            {
                return GetAttributeValue<Guid?>("webresourceid");
            }
            set
            {
                SetAttributeValue("webresourceid", value);
                base.Id = value.HasValue ? value.Value : Guid.Empty;
            }
        }

        /// <summary>
		/// Information that specifies whether this component can be deleted.
		/// </summary>
        [AttributeLogicalName("canbedeleted")]
        public BooleanManagedProperty? CanBeDeleted
        {
            get
            {
                return GetAttributeValue<BooleanManagedProperty?>("canbedeleted");
            }
            set
            {
                SetAttributeValue("canbedeleted", value);
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
		/// Bytes of the web resource, in Base64 format.
		/// </summary>
        [AttributeLogicalName("content")]
        public string? Content
        {
            get
            {
                return GetAttributeValue<string?>("content");
            }
            set
            {
                SetAttributeValue("content", value);
            }
        }

        /// <summary>
		/// Reference to the content file on Azure.
		/// </summary>
        [AttributeLogicalName("contentfileref")]
        public Guid? ContentFileRef
        {
            get
            {
                return GetAttributeValue<Guid?>("contentfileref");
            }
        }

        
        [AttributeLogicalName("contentfileref_name")]
        public string? ContentFileRefName
        {
            get
            {
                return GetAttributeValue<string?>("contentfileref_name");
            }
        }

        /// <summary>
		/// Json representation of the content of the resource.
		/// </summary>
        [AttributeLogicalName("contentjson")]
        public string? ContentJson
        {
            get
            {
                return GetAttributeValue<string?>("contentjson");
            }
            set
            {
                SetAttributeValue("contentjson", value);
            }
        }

        /// <summary>
		/// Reference to the Json content file on Azure.
		/// </summary>
        [AttributeLogicalName("contentjsonfileref")]
        public Guid? ContentJsonFileRef
        {
            get
            {
                return GetAttributeValue<Guid?>("contentjsonfileref");
            }
        }

        
        [AttributeLogicalName("contentjsonfileref_name")]
        public string? ContentJsonFileRefName
        {
            get
            {
                return GetAttributeValue<string?>("contentjsonfileref_name");
            }
        }

        /// <summary>
		/// Unique identifier of the user who created the web resource.
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
		/// Date and time when the web resource was created.
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
		/// Unique identifier of the delegate user who created the web resource.
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
		/// For internal use only.
		/// </summary>
        [AttributeLogicalName("dependencyxml")]
        public string? DependencyXml
        {
            get
            {
                return GetAttributeValue<string?>("dependencyxml");
            }
            set
            {
                SetAttributeValue("dependencyxml", value);
            }
        }

        /// <summary>
		/// Description of the web resource.
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
		/// Display name of the web resource.
		/// </summary>
        [AttributeLogicalName("displayname")]
        public string? DisplayName
        {
            get
            {
                return GetAttributeValue<string?>("displayname");
            }
            set
            {
                SetAttributeValue("displayname", value);
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
		/// Information that specifies whether this web resource is available for mobile client in offline mode.
		/// </summary>
        [AttributeLogicalName("isavailableformobileoffline")]
        public bool? IsAvailableForMobileOffline
        {
            get
            {
                return GetAttributeValue<bool?>("isavailableformobileoffline");
            }
            set
            {
                SetAttributeValue("isavailableformobileoffline", value);
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

        /// <summary>
		/// Information that specifies whether this web resource is enabled for mobile client.
		/// </summary>
        [AttributeLogicalName("isenabledformobileclient")]
        public bool? IsEnabledForMobileClient
        {
            get
            {
                return GetAttributeValue<bool?>("isenabledformobileclient");
            }
            set
            {
                SetAttributeValue("isenabledformobileclient", value);
            }
        }

        /// <summary>
		/// Information that specifies whether this component should be hidden.
		/// </summary>
        [AttributeLogicalName("ishidden")]
        public BooleanManagedProperty? IsHidden
        {
            get
            {
                return GetAttributeValue<BooleanManagedProperty?>("ishidden");
            }
            set
            {
                SetAttributeValue("ishidden", value);
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
		/// Language of the web resource.
		/// </summary>
        [AttributeLogicalName("languagecode")]
        public int? LanguageCode
        {
            get
            {
                return GetAttributeValue<int?>("languagecode");
            }
            set
            {
                SetAttributeValue("languagecode", value);
            }
        }

        /// <summary>
		/// Unique identifier of the user who last modified the web resource.
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
		/// Date and time when the web resource was last modified.
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
		/// Unique identifier of the delegate user who modified the web resource.
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
		/// Name of the web resource.
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
		/// Unique identifier of the organization associated with the web resource.
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
		/// Silverlight runtime version number required by a silverlight web resource.
		/// </summary>
        [AttributeLogicalName("silverlightversion")]
        public string? SilverlightVersion
        {
            get
            {
                return GetAttributeValue<string?>("silverlightversion");
            }
            set
            {
                SetAttributeValue("silverlightversion", value);
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
		/// For internal use only.
		/// </summary>
        [AttributeLogicalName("webresourceidunique")]
        public Guid? WebResourceIdUnique
        {
            get
            {
                return GetAttributeValue<Guid?>("webresourceidunique");
            }
        }

        /// <summary>
		/// Drop-down list for selecting the type of the web resource.
		/// </summary>
        [AttributeLogicalName("webresourcetype")]
        public OptionSetValue? WebResourceType
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("webresourcetype");
            }
            set
            {
                SetAttributeValue("webresourcetype", value);
            }
        }
        #endregion

        #region NavigationProperties

        /// <summary>
        /// 1:N solution_configuration_webresource
        /// </summary>
        [RelationshipSchemaName("solution_configuration_webresource")]
        public IEnumerable<Solution> SolutionConfigurationWebresource
        {
            get
            {
                return GetRelatedEntities<Solution>("solution_configuration_webresource", null);
            }
            set
            {
                SetRelatedEntities("solution_configuration_webresource", null, value);
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
            public struct IsAvailableForMobileOffline
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsEnabledForMobileClient
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsManaged
            {
                public const bool Unmanaged = false;
                public const bool Managed = true;
            }
            public struct WebResourceType
            {
                public const int WebpageHTML = 1;
                public const int StyleSheetCSS = 2;
                public const int ScriptJScript = 3;
                public const int DataXML = 4;
                public const int PNGFormat = 5;
                public const int JPGFormat = 6;
                public const int GIFFormat = 7;
                public const int SilverlightXAP = 8;
                public const int StyleSheetXSL = 9;
                public const int ICOFormat = 10;
                public const int VectorFormatSVG = 11;
                public const int StringRESX = 12;
            }
        }
        #endregion

        #region LogicalNames
        public static partial class LogicalNames
        {
            public const string WebResourceId = "webresourceid";
            public const string CanBeDeleted = "canbedeleted";
            public const string ComponentState = "componentstate";
            public const string Content = "content";
            public const string ContentFileRef = "contentfileref";
            public const string ContentFileRefName = "contentfileref_name";
            public const string ContentJson = "contentjson";
            public const string ContentJsonFileRef = "contentjsonfileref";
            public const string ContentJsonFileRefName = "contentjsonfileref_name";
            public const string CreatedBy = "createdby";
            public const string CreatedOn = "createdon";
            public const string CreatedOnBehalfBy = "createdonbehalfby";
            public const string DependencyXml = "dependencyxml";
            public const string Description = "description";
            public const string DisplayName = "displayname";
            public const string IntroducedVersion = "introducedversion";
            public const string IsAvailableForMobileOffline = "isavailableformobileoffline";
            public const string IsCustomizable = "iscustomizable";
            public const string IsEnabledForMobileClient = "isenabledformobileclient";
            public const string IsHidden = "ishidden";
            public const string IsManaged = "ismanaged";
            public const string LanguageCode = "languagecode";
            public const string ModifiedBy = "modifiedby";
            public const string ModifiedOn = "modifiedon";
            public const string ModifiedOnBehalfBy = "modifiedonbehalfby";
            public const string Name = "name";
            public const string OrganizationId = "organizationid";
            public const string OverwriteTime = "overwritetime";
            public const string SilverlightVersion = "silverlightversion";
            public const string SolutionId = "solutionid";
            public const string VersionNumber = "versionnumber";
            public const string WebResourceIdUnique = "webresourceidunique";
            public const string WebResourceType = "webresourcetype";
        }
        #endregion

        #region Relations
        public static partial class Relations
        {
            public static class OneToMany
            {
                public const string LkThemeLogoid = "lk_theme_logoid";
                public const string SolutionConfigurationWebresource = "solution_configuration_webresource";
                public const string UserentityinstancedataWebresource = "userentityinstancedata_webresource";
                public const string WebresourceAppactionIconwebresourceid = "webresource_appaction_iconwebresourceid";
                public const string WebresourceAppactionOnclickeventjavascriptwebresourceid = "webresource_appaction_onclickeventjavascriptwebresourceid";
                public const string WebresourceFileAttachments = "webresource_FileAttachments";
                public const string WebresourceSavedqueryvisualizations = "webresource_savedqueryvisualizations";
                public const string WebresourceUserqueryvisualizations = "webresource_userqueryvisualizations";
            }

            public static partial class ManyToOne
            {
                public const string FileAttachmentWebResourceContentFileRef = "FileAttachment_WebResource_ContentFileRef";
                public const string FileAttachmentWebResourceContentJsonFileRef = "FileAttachment_WebResource_ContentJsonFileRef";
                public const string LkWebresourcebaseCreatedonbehalfby = "lk_webresourcebase_createdonbehalfby";
                public const string LkWebresourcebaseModifiedonbehalfby = "lk_webresourcebase_modifiedonbehalfby";
                public const string WebresourceCreatedby = "webresource_createdby";
                public const string WebresourceModifiedby = "webresource_modifiedby";
                public const string WebresourceOrganization = "webresource_organization";
            }

            public static partial class ManyToMany
            {
                public const string AppactionruleWebresourceScripts = "appactionrule_webresource_scripts";
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
        public IQueryable<WebResource> WebResourceSet
        {
            get
            {
                return CreateQuery<WebResource>();
            }
        }
    }
    #endregion
}
