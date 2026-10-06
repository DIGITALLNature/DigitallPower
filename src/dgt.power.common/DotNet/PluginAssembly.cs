using System.Diagnostics.CodeAnalysis;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System.Runtime.Serialization;

// ReSharper disable All
namespace dgt.power.dataverse
{
    /// <inheritdoc cref="Microsoft.Xrm.Sdk.Entity" />
    /// <summary>
	/// Assembly that contains one or more plug-in types.
	/// </summary>
    [EntityLogicalName("pluginassembly")]
    [System.CodeDom.Compiler.GeneratedCode("dgtp", "2026")]
    [ExcludeFromCodeCoverage]
    [SuppressMessage("Design", "CA1034")]
    [SuppressMessage("Performance", "CA1815")]
    public partial class PluginAssembly : Entity
    {
        #region ctor
        public PluginAssembly() : base(EntityLogicalName) { }

        public PluginAssembly(Guid id) : base(EntityLogicalName, id) { }

        public PluginAssembly(KeyAttributeCollection keyAttributes) : base(EntityLogicalName, keyAttributes) { }

        public PluginAssembly(string keyName, object keyValue) : base(EntityLogicalName, keyName, keyValue) { }
        #endregion

        #region consts
        public const string EntityLogicalName = "pluginassembly";
        public const string PrimaryNameAttribute = "name";
        public const int EntityTypeCode = 4605;
        #endregion

        #region Attributes
        [AttributeLogicalName("pluginassemblyid")]
        [IgnoreDataMember]
        public new Guid Id
        {
            get
            {
                return base.Id;
            }
            set
            {
                PluginAssemblyId = value;
            }
        }

        /// <summary>
		/// Unique identifier of the plug-in assembly.
		/// </summary>
        [AttributeLogicalName("pluginassemblyid")]
        public Guid? PluginAssemblyId
        {
            get
            {
                return GetAttributeValue<Guid?>("pluginassemblyid");
            }
            set
            {
                SetAttributeValue("pluginassemblyid", value);
                base.Id = value.HasValue ? value.Value : Guid.Empty;
            }
        }

        /// <summary>
		/// Specifies mode of authentication with web sources like WebApp
		/// </summary>
        [AttributeLogicalName("authtype")]
        public OptionSetValue? AuthType
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("authtype");
            }
            set
            {
                SetAttributeValue("authtype", value);
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
		/// Bytes of the assembly, in Base64 format.
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
		/// Unique identifier of the user who created the plug-in assembly.
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
		/// Date and time when the plug-in assembly was created.
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
		/// Unique identifier of the delegate user who created the pluginassembly.
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
		/// Culture code for the plug-in assembly.
		/// </summary>
        [AttributeLogicalName("culture")]
        public string? Culture
        {
            get
            {
                return GetAttributeValue<string?>("culture");
            }
            set
            {
                SetAttributeValue("culture", value);
            }
        }

        /// <summary>
		/// Customization Level.
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
		/// Description of the plug-in assembly.
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
		/// Information about how the plugin assembly is to be isolated at execution time; None / Sandboxed.
		/// </summary>
        [AttributeLogicalName("isolationmode")]
        public OptionSetValue? IsolationMode
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("isolationmode");
            }
            set
            {
                SetAttributeValue("isolationmode", value);
            }
        }

        
        [AttributeLogicalName("ispasswordset")]
        public bool? IsPasswordSet
        {
            get
            {
                return GetAttributeValue<bool?>("ispasswordset");
            }
        }

        /// <summary>
		/// Major of the assembly version.
		/// </summary>
        [AttributeLogicalName("major")]
        public int? Major
        {
            get
            {
                return GetAttributeValue<int?>("major");
            }
        }

        /// <summary>
		/// Unique identifier for managedidentity associated with pluginassembly.
		/// </summary>
        [AttributeLogicalName("managedidentityid")]
        public EntityReference? ManagedIdentityId
        {
            get
            {
                return GetAttributeValue<EntityReference?>("managedidentityid");
            }
            set
            {
                SetAttributeValue("managedidentityid", value);
            }
        }

        /// <summary>
		/// Minor of the assembly version.
		/// </summary>
        [AttributeLogicalName("minor")]
        public int? Minor
        {
            get
            {
                return GetAttributeValue<int?>("minor");
            }
        }

        /// <summary>
		/// Unique identifier of the user who last modified the plug-in assembly.
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
		/// Date and time when the plug-in assembly was last modified.
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
		/// Unique identifier of the delegate user who last modified the pluginassembly.
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
		/// Name of the plug-in assembly.
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
		/// Unique identifier of the organization with which the plug-in assembly is associated.
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
		/// Unique identifier for Plugin Package associated with Plug-in Assembly.
		/// </summary>
        [AttributeLogicalName("packageid")]
        public EntityReference? PackageId
        {
            get
            {
                return GetAttributeValue<EntityReference?>("packageid");
            }
            set
            {
                SetAttributeValue("packageid", value);
            }
        }

        /// <summary>
		/// User Password
		/// </summary>
        [AttributeLogicalName("password")]
        public string? Password
        {
            set
            {
                SetAttributeValue("password", value);
            }
        }

        /// <summary>
		/// File name of the plug-in assembly. Used when the source type is set to 1.
		/// </summary>
        [AttributeLogicalName("path")]
        public string? Path
        {
            get
            {
                return GetAttributeValue<string?>("path");
            }
            set
            {
                SetAttributeValue("path", value);
            }
        }

        /// <summary>
		/// Unique identifier of the plug-in assembly.
		/// </summary>
        [AttributeLogicalName("pluginassemblyidunique")]
        public Guid? PluginAssemblyIdUnique
        {
            get
            {
                return GetAttributeValue<Guid?>("pluginassemblyidunique");
            }
        }

        /// <summary>
		/// Public key token of the assembly. This value can be obtained from the assembly by using reflection.
		/// </summary>
        [AttributeLogicalName("publickeytoken")]
        public string? PublicKeyToken
        {
            get
            {
                return GetAttributeValue<string?>("publickeytoken");
            }
            set
            {
                SetAttributeValue("publickeytoken", value);
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
		/// Hash of the source of the assembly.
		/// </summary>
        [AttributeLogicalName("sourcehash")]
        public string? SourceHash
        {
            get
            {
                return GetAttributeValue<string?>("sourcehash");
            }
            set
            {
                SetAttributeValue("sourcehash", value);
            }
        }

        /// <summary>
		/// Location of the assembly, for example 0=database, 1=on-disk.
		/// </summary>
        [AttributeLogicalName("sourcetype")]
        public OptionSetValue? SourceType
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("sourcetype");
            }
            set
            {
                SetAttributeValue("sourcetype", value);
            }
        }

        /// <summary>
		/// Web Url
		/// </summary>
        [AttributeLogicalName("url")]
        public string? Url
        {
            get
            {
                return GetAttributeValue<string?>("url");
            }
            set
            {
                SetAttributeValue("url", value);
            }
        }

        /// <summary>
		/// User Name
		/// </summary>
        [AttributeLogicalName("username")]
        public string? UserName
        {
            get
            {
                return GetAttributeValue<string?>("username");
            }
            set
            {
                SetAttributeValue("username", value);
            }
        }

        /// <summary>
		/// Version number of the assembly. The value can be obtained from the assembly through reflection.
		/// </summary>
        [AttributeLogicalName("version")]
        public string? Version
        {
            get
            {
                return GetAttributeValue<string?>("version");
            }
            set
            {
                SetAttributeValue("version", value);
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
        #endregion

        #region NavigationProperties

        /// <summary>
        /// 1:N pluginassembly_plugintype
        /// </summary>
        [RelationshipSchemaName("pluginassembly_plugintype")]
        public IEnumerable<PluginType> PluginassemblyPlugintype
        {
            get
            {
                return GetRelatedEntities<PluginType>("pluginassembly_plugintype", null);
            }
            set
            {
                SetRelatedEntities("pluginassembly_plugintype", null, value);
            }
        }
        #endregion

        #region Options
        public static partial class Options
        {
            public struct AuthType
            {
                public const int BasicAuth = 0;
            }
            public struct ComponentState
            {
                public const int Published = 0;
                public const int Unpublished = 1;
                public const int Deleted = 2;
                public const int DeletedUnpublished = 3;
            }
            public struct IsManaged
            {
                public const bool Unmanaged = false;
                public const bool Managed = true;
            }
            public struct IsolationMode
            {
                public const int None = 1;
                public const int Sandbox = 2;
                public const int External = 3;
            }
            public struct IsPasswordSet
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct SourceType
            {
                public const int Database = 0;
                public const int Disk = 1;
                public const int Normal = 2;
                public const int AzureWebApp = 3;
                public const int FileStore = 4;
            }
        }
        #endregion

        #region LogicalNames
        public static partial class LogicalNames
        {
            public const string PluginAssemblyId = "pluginassemblyid";
            public const string AuthType = "authtype";
            public const string ComponentState = "componentstate";
            public const string Content = "content";
            public const string CreatedBy = "createdby";
            public const string CreatedOn = "createdon";
            public const string CreatedOnBehalfBy = "createdonbehalfby";
            public const string Culture = "culture";
            public const string CustomizationLevel = "customizationlevel";
            public const string Description = "description";
            public const string IntroducedVersion = "introducedversion";
            public const string IsCustomizable = "iscustomizable";
            public const string IsHidden = "ishidden";
            public const string IsManaged = "ismanaged";
            public const string IsolationMode = "isolationmode";
            public const string IsPasswordSet = "ispasswordset";
            public const string Major = "major";
            public const string ManagedIdentityId = "managedidentityid";
            public const string Minor = "minor";
            public const string ModifiedBy = "modifiedby";
            public const string ModifiedOn = "modifiedon";
            public const string ModifiedOnBehalfBy = "modifiedonbehalfby";
            public const string Name = "name";
            public const string OrganizationId = "organizationid";
            public const string OverwriteTime = "overwritetime";
            public const string PackageId = "packageid";
            public const string Password = "password";
            public const string Path = "path";
            public const string PluginAssemblyIdUnique = "pluginassemblyidunique";
            public const string PublicKeyToken = "publickeytoken";
            public const string SolutionId = "solutionid";
            public const string SourceHash = "sourcehash";
            public const string SourceType = "sourcetype";
            public const string Url = "url";
            public const string UserName = "username";
            public const string Version = "version";
            public const string VersionNumber = "versionnumber";
        }
        #endregion

        #region Relations
        public static partial class Relations
        {
            public static class OneToMany
            {
                public const string PluginassemblyPlugintype = "pluginassembly_plugintype";
                public const string UserentityinstancedataPluginassembly = "userentityinstancedata_pluginassembly";
            }

            public static partial class ManyToOne
            {
                public const string CreatedbyPluginassembly = "createdby_pluginassembly";
                public const string LkPluginassemblyCreatedonbehalfby = "lk_pluginassembly_createdonbehalfby";
                public const string LkPluginassemblyModifiedonbehalfby = "lk_pluginassembly_modifiedonbehalfby";
                public const string ManagedidentityPluginAssembly = "managedidentity_PluginAssembly";
                public const string ModifiedbyPluginassembly = "modifiedby_pluginassembly";
                public const string OrganizationPluginassembly = "organization_pluginassembly";
                public const string PluginpackagePluginassembly = "pluginpackage_pluginassembly";
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
        public IQueryable<PluginAssembly> PluginAssemblySet
        {
            get
            {
                return CreateQuery<PluginAssembly>();
            }
        }
    }
    #endregion
}
