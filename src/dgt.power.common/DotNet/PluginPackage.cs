using System.Diagnostics.CodeAnalysis;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System.Runtime.Serialization;

// ReSharper disable All
namespace dgt.power.dataverse
{
    /// <inheritdoc cref="Microsoft.Xrm.Sdk.Entity" />
    
    [EntityLogicalName("pluginpackage")]
    [System.CodeDom.Compiler.GeneratedCode("dgtp", "2026")]
    [ExcludeFromCodeCoverage]
    [SuppressMessage("Design", "CA1034")]
    [SuppressMessage("Performance", "CA1815")]
    public partial class PluginPackage : Entity
    {
        #region ctor
        public PluginPackage() : base(EntityLogicalName) { }

        public PluginPackage(Guid id) : base(EntityLogicalName, id) { }

        public PluginPackage(KeyAttributeCollection keyAttributes) : base(EntityLogicalName, keyAttributes) { }

        public PluginPackage(string keyName, object keyValue) : base(EntityLogicalName, keyName, keyValue) { }
        #endregion

        #region consts
        public const string EntityLogicalName = "pluginpackage";
        public const string PrimaryNameAttribute = "name";
        public const int EntityTypeCode = 10039;
        #endregion

        #region Attributes
        [AttributeLogicalName("pluginpackageid")]
        [IgnoreDataMember]
        public new Guid Id
        {
            get
            {
                return base.Id;
            }
            set
            {
                PluginPackageId = value;
            }
        }

        /// <summary>
		/// Unique identifier for entity instances
		/// </summary>
        [AttributeLogicalName("pluginpackageid")]
        public Guid? PluginPackageId
        {
            get
            {
                return GetAttributeValue<Guid?>("pluginpackageid");
            }
            set
            {
                SetAttributeValue("pluginpackageid", value);
                base.Id = value.HasValue ? value.Value : Guid.Empty;
            }
        }

        /// <summary>
		/// For internal use only.
		/// </summary>
        [AttributeLogicalName("componentidunique")]
        public Guid? ComponentIdUnique
        {
            get
            {
                return GetAttributeValue<Guid?>("componentidunique");
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
		/// Unique identifier of the user who created the record.
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
		/// Date and time when the record was created.
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
		/// Unique identifier of the delegate user who created the record.
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
		/// Export Key Version
		/// </summary>
        [AttributeLogicalName("exportkeyversion")]
        public int? ExportKeyVersion
        {
            get
            {
                return GetAttributeValue<int?>("exportkeyversion");
            }
            set
            {
                SetAttributeValue("exportkeyversion", value);
            }
        }

        /// <summary>
		/// Lookup to FileAttachment
		/// </summary>
        [AttributeLogicalName("fileid")]
        public Guid? FileId
        {
            get
            {
                return GetAttributeValue<Guid?>("fileid");
            }
        }

        
        [AttributeLogicalName("fileid_name")]
        public string? FileIdName
        {
            get
            {
                return GetAttributeValue<string?>("fileid_name");
            }
        }

        /// <summary>
		/// Sequence number of the import that created this record.
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
		/// For internal use only.
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
		/// Indicates whether the solution component is part of a managed solution.
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
		/// Managed Identity Id to look up to ManagedIdentity Entity
		/// </summary>
        [AttributeLogicalName("managedidentityid")]
        public EntityReference? Managedidentityid
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
		/// Unique identifier of the user who modified the record.
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
		/// Date and time when the record was modified.
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
		/// Unique identifier of the delegate user who modified the record.
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
		/// The name of the plugin package entity.
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
		/// Unique identifier for the organization
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
		/// Lookup to FileAttachment
		/// </summary>
        [AttributeLogicalName("package")]
        public Guid? Package
        {
            get
            {
                return GetAttributeValue<Guid?>("package");
            }
        }

        
        [AttributeLogicalName("package_name")]
        public string? PackageName
        {
            get
            {
                return GetAttributeValue<string?>("package_name");
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
		/// Status of the Plugin Package
		/// </summary>
        [AttributeLogicalName("statecode")]
        public OptionSetValue? Statecode
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
		/// Reason for the status of the Plugin Package
		/// </summary>
        [AttributeLogicalName("statuscode")]
        public OptionSetValue? Statuscode
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
		/// For internal use only.
		/// </summary>
        [AttributeLogicalName("timezoneruleversionnumber")]
        public int? TimeZoneRuleVersionNumber
        {
            get
            {
                return GetAttributeValue<int?>("timezoneruleversionnumber");
            }
            set
            {
                SetAttributeValue("timezoneruleversionnumber", value);
            }
        }

        /// <summary>
		/// Unique name for the package
		/// </summary>
        [AttributeLogicalName("uniquename")]
        public string? UniqueName
        {
            get
            {
                return GetAttributeValue<string?>("uniquename");
            }
            set
            {
                SetAttributeValue("uniquename", value);
            }
        }

        /// <summary>
		/// Time zone code that was in use when the record was created.
		/// </summary>
        [AttributeLogicalName("utcconversiontimezonecode")]
        public int? UTCConversionTimeZoneCode
        {
            get
            {
                return GetAttributeValue<int?>("utcconversiontimezonecode");
            }
            set
            {
                SetAttributeValue("utcconversiontimezonecode", value);
            }
        }

        /// <summary>
		/// Version of the package
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

        /// <summary>
		/// Version Number
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
        /// 1:N pluginpackage_AsyncOperations
        /// </summary>
        [RelationshipSchemaName("pluginpackage_AsyncOperations")]
        public IEnumerable<AsyncOperation> PluginpackageAsyncOperations
        {
            get
            {
                return GetRelatedEntities<AsyncOperation>("pluginpackage_AsyncOperations", null);
            }
            set
            {
                SetRelatedEntities("pluginpackage_AsyncOperations", null, value);
            }
        }

        /// <summary>
        /// 1:N pluginpackage_pluginassembly
        /// </summary>
        [RelationshipSchemaName("pluginpackage_pluginassembly")]
        public IEnumerable<PluginAssembly> PluginpackagePluginassembly
        {
            get
            {
                return GetRelatedEntities<PluginAssembly>("pluginpackage_pluginassembly", null);
            }
            set
            {
                SetRelatedEntities("pluginpackage_pluginassembly", null, value);
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
            public struct IsManaged
            {
                public const bool Unmanaged = false;
                public const bool Managed = true;
            }
            public struct Statecode
            {
                public const int Active = 0;
                public const int Inactive = 1;
            }
            public struct Statuscode
            {
                public const int Active = 1;
                public const int Inactive = 2;
            }
        }
        #endregion

        #region LogicalNames
        public static partial class LogicalNames
        {
            public const string PluginPackageId = "pluginpackageid";
            public const string ComponentIdUnique = "componentidunique";
            public const string ComponentState = "componentstate";
            public const string Content = "content";
            public const string CreatedBy = "createdby";
            public const string CreatedOn = "createdon";
            public const string CreatedOnBehalfBy = "createdonbehalfby";
            public const string ExportKeyVersion = "exportkeyversion";
            public const string FileId = "fileid";
            public const string FileIdName = "fileid_name";
            public const string ImportSequenceNumber = "importsequencenumber";
            public const string IsCustomizable = "iscustomizable";
            public const string IsManaged = "ismanaged";
            public const string Managedidentityid = "managedidentityid";
            public const string ModifiedBy = "modifiedby";
            public const string ModifiedOn = "modifiedon";
            public const string ModifiedOnBehalfBy = "modifiedonbehalfby";
            public const string Name = "name";
            public const string OrganizationId = "organizationid";
            public const string OverriddenCreatedOn = "overriddencreatedon";
            public const string OverwriteTime = "overwritetime";
            public const string Package = "package";
            public const string PackageName = "package_name";
            public const string SolutionId = "solutionid";
            public const string Statecode = "statecode";
            public const string Statuscode = "statuscode";
            public const string TimeZoneRuleVersionNumber = "timezoneruleversionnumber";
            public const string UniqueName = "uniquename";
            public const string UTCConversionTimeZoneCode = "utcconversiontimezonecode";
            public const string Version = "version";
            public const string VersionNumber = "versionnumber";
        }
        #endregion

        #region AlternateKeys
        public static partial class AlternateKeys
        {
            public const string FullNameOfPackage = "uniquename\"";
        }
        #endregion

        #region Relations
        public static partial class Relations
        {
            public static class OneToMany
            {
                public const string PluginpackageAsyncOperations = "pluginpackage_AsyncOperations";
                public const string PluginpackageBulkDeleteFailures = "pluginpackage_BulkDeleteFailures";
                public const string PluginpackageFileAttachments = "pluginpackage_FileAttachments";
                public const string PluginpackageMailboxTrackingFolders = "pluginpackage_MailboxTrackingFolders";
                public const string PluginpackagePluginassembly = "pluginpackage_pluginassembly";
                public const string PluginpackagePrincipalObjectAttributeAccesses = "pluginpackage_PrincipalObjectAttributeAccesses";
                public const string PluginpackageSyncErrors = "pluginpackage_SyncErrors";
                public const string PluginpackageUserEntityInstanceDatas = "pluginpackage_UserEntityInstanceDatas";
            }

            public static partial class ManyToOne
            {
                public const string FileAttachmentPluginpackageFileId = "FileAttachment_pluginpackage_FileId";
                public const string FileAttachmentPluginpackagePackage = "FileAttachment_pluginpackage_Package";
                public const string LkPluginpackageCreatedby = "lk_pluginpackage_createdby";
                public const string LkPluginpackageCreatedonbehalfby = "lk_pluginpackage_createdonbehalfby";
                public const string LkPluginpackageModifiedby = "lk_pluginpackage_modifiedby";
                public const string LkPluginpackageModifiedonbehalfby = "lk_pluginpackage_modifiedonbehalfby";
                public const string ManagedidentityPluginpackage = "managedidentity_pluginpackage";
                public const string OrganizationPluginpackage = "organization_pluginpackage";
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
        public IQueryable<PluginPackage> PluginPackageSet
        {
            get
            {
                return CreateQuery<PluginPackage>();
            }
        }
    }
    #endregion
}
