using System.Diagnostics.CodeAnalysis;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System.Runtime.Serialization;

// ReSharper disable All
namespace dgt.power.dataverse
{
    /// <inheritdoc cref="Microsoft.Xrm.Sdk.Entity" />
    /// <summary>
	/// Developers can register plug-ins on a data provider to enable data access for virtual entities in the system.
	/// </summary>
    [EntityLogicalName("entitydataprovider")]
    [System.CodeDom.Compiler.GeneratedCode("dgtp", "2026")]
    [ExcludeFromCodeCoverage]
    [SuppressMessage("Design", "CA1034")]
    [SuppressMessage("Performance", "CA1815")]
    public partial class EntityDataProvider : Entity
    {
        #region ctor
        public EntityDataProvider() : base(EntityLogicalName) { }

        public EntityDataProvider(Guid id) : base(EntityLogicalName, id) { }

        public EntityDataProvider(KeyAttributeCollection keyAttributes) : base(EntityLogicalName, keyAttributes) { }

        public EntityDataProvider(string keyName, object keyValue) : base(EntityLogicalName, keyName, keyValue) { }
        #endregion

        #region consts
        public const string EntityLogicalName = "entitydataprovider";
        public const string PrimaryNameAttribute = "name";
        public const int EntityTypeCode = 78;
        #endregion

        #region Attributes
        [AttributeLogicalName("entitydataproviderid")]
        [IgnoreDataMember]
        public new Guid Id
        {
            get
            {
                return base.Id;
            }
            set
            {
                EntityDataProviderId = value;
            }
        }

        /// <summary>
		/// Unique identifier of the data provider.
		/// </summary>
        [AttributeLogicalName("entitydataproviderid")]
        public Guid? EntityDataProviderId
        {
            get
            {
                return GetAttributeValue<Guid?>("entitydataproviderid");
            }
            set
            {
                SetAttributeValue("entitydataproviderid", value);
                base.Id = value.HasValue ? value.Value : Guid.Empty;
            }
        }

        /// <summary>
		/// Contains the archiveplugin id that should be run when Archive is invoked
		/// </summary>
        [AttributeLogicalName("archiveplugin")]
        public Guid? ArchivePlugin
        {
            get
            {
                return GetAttributeValue<Guid?>("archiveplugin");
            }
            set
            {
                SetAttributeValue("archiveplugin", value);
            }
        }

        /// <summary>
		/// Contains the bulkarchiveplugin id that should be run when BulkArchive is invoked
		/// </summary>
        [AttributeLogicalName("bulkarchiveplugin")]
        public Guid? BulkArchivePlugin
        {
            get
            {
                return GetAttributeValue<Guid?>("bulkarchiveplugin");
            }
            set
            {
                SetAttributeValue("bulkarchiveplugin", value);
            }
        }

        /// <summary>
		/// Contains the bulkretainplugin id that should be run when BulkRetain is invoked
		/// </summary>
        [AttributeLogicalName("bulkretainplugin")]
        public Guid? BulkRetainPlugin
        {
            get
            {
                return GetAttributeValue<Guid?>("bulkretainplugin");
            }
            set
            {
                SetAttributeValue("bulkretainplugin", value);
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
		/// Contains the createmultipleplugin id that should be run when CreateMultiple is invoked
		/// </summary>
        [AttributeLogicalName("createmultipleplugin")]
        public Guid? CreateMultiplePlugin
        {
            get
            {
                return GetAttributeValue<Guid?>("createmultipleplugin");
            }
            set
            {
                SetAttributeValue("createmultipleplugin", value);
            }
        }

        /// <summary>
		/// Create Plugin
		/// </summary>
        [AttributeLogicalName("createplugin")]
        public Guid? CreatePlugin
        {
            get
            {
                return GetAttributeValue<Guid?>("createplugin");
            }
            set
            {
                SetAttributeValue("createplugin", value);
            }
        }

        /// <summary>
		/// When creating a Data Provider, the end user must select the name of the Data Source entity that will be created for the provider.
		/// </summary>
        [AttributeLogicalName("datasourcelogicalname")]
        public string? DataSourceLogicalName
        {
            get
            {
                return GetAttributeValue<string?>("datasourcelogicalname");
            }
            set
            {
                SetAttributeValue("datasourcelogicalname", value);
            }
        }

        /// <summary>
		/// Contains the deletemultipleplugin id that should be run when DeleteMultiple is invoked
		/// </summary>
        [AttributeLogicalName("deletemultipleplugin")]
        public Guid? DeleteMultiplePlugin
        {
            get
            {
                return GetAttributeValue<Guid?>("deletemultipleplugin");
            }
            set
            {
                SetAttributeValue("deletemultipleplugin", value);
            }
        }

        /// <summary>
		/// Delete Plugin
		/// </summary>
        [AttributeLogicalName("deleteplugin")]
        public Guid? DeletePlugin
        {
            get
            {
                return GetAttributeValue<Guid?>("deleteplugin");
            }
            set
            {
                SetAttributeValue("deleteplugin", value);
            }
        }

        /// <summary>
		/// What is this Data Provider used for and data store technologies does it target?
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
		/// For internal use only.
		/// </summary>
        [AttributeLogicalName("entitydataprovideridunique")]
        public Guid? EntityDataProviderIdUnique
        {
            get
            {
                return GetAttributeValue<Guid?>("entitydataprovideridunique");
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
		/// Enables expansion support for lookups columns. Only applicable to RetrieveMultiple plugin. Enabling this might modify the filter expression supplied to RetrieveMultiple plugin. Default value is false.
		/// </summary>
        [AttributeLogicalName("lookupexpansionenabled")]
        public bool? LookupExpansionEnabled
        {
            get
            {
                return GetAttributeValue<bool?>("lookupexpansionenabled");
            }
            set
            {
                SetAttributeValue("lookupexpansionenabled", value);
            }
        }

        /// <summary>
		/// The name of this Data Provider. This is the name that appears in the dropdown when creating a new entity.
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
		/// Unique identifier for the organization.
		/// </summary>
        [AttributeLogicalName("organizationid")]
        public Guid? OrganizationId
        {
            get
            {
                return GetAttributeValue<Guid?>("organizationid");
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
		/// Contains the purgearchivedcontentplugin id that should be run when PurgeArchivedContent is invoked
		/// </summary>
        [AttributeLogicalName("purgearchivedcontentplugin")]
        public Guid? PurgeArchivedContentPlugin
        {
            get
            {
                return GetAttributeValue<Guid?>("purgearchivedcontentplugin");
            }
            set
            {
                SetAttributeValue("purgearchivedcontentplugin", value);
            }
        }

        /// <summary>
		/// Contains the purgeretainedcontentplugin id that should be run when PurgeRetainedContent is invoked
		/// </summary>
        [AttributeLogicalName("purgeretainedcontentplugin")]
        public Guid? PurgeRetainedContentPlugin
        {
            get
            {
                return GetAttributeValue<Guid?>("purgeretainedcontentplugin");
            }
            set
            {
                SetAttributeValue("purgeretainedcontentplugin", value);
            }
        }

        /// <summary>
		/// Contains the retainplugin id that should be run when Retain is invoked
		/// </summary>
        [AttributeLogicalName("retainplugin")]
        public Guid? RetainPlugin
        {
            get
            {
                return GetAttributeValue<Guid?>("retainplugin");
            }
            set
            {
                SetAttributeValue("retainplugin", value);
            }
        }

        /// <summary>
		/// Contains the retrieveentitychangesplugin id that should be run when RetrieveEntityChanges is invoked
		/// </summary>
        [AttributeLogicalName("retrieveentitychangesplugin")]
        public Guid? RetrieveEntityChangesPlugin
        {
            get
            {
                return GetAttributeValue<Guid?>("retrieveentitychangesplugin");
            }
            set
            {
                SetAttributeValue("retrieveentitychangesplugin", value);
            }
        }

        /// <summary>
		/// MultipleRetrieve Plugin
		/// </summary>
        [AttributeLogicalName("retrievemultipleplugin")]
        public Guid? RetrieveMultiplePlugin
        {
            get
            {
                return GetAttributeValue<Guid?>("retrievemultipleplugin");
            }
            set
            {
                SetAttributeValue("retrievemultipleplugin", value);
            }
        }

        /// <summary>
		/// Retrieve Plugin
		/// </summary>
        [AttributeLogicalName("retrieveplugin")]
        public Guid? RetrievePlugin
        {
            get
            {
                return GetAttributeValue<Guid?>("retrieveplugin");
            }
            set
            {
                SetAttributeValue("retrieveplugin", value);
            }
        }

        /// <summary>
		/// Contains the rollbackretainplugin id that should be run when Rollback Retain is invoked
		/// </summary>
        [AttributeLogicalName("rollbackretainplugin")]
        public Guid? RollbackRetainPlugin
        {
            get
            {
                return GetAttributeValue<Guid?>("rollbackretainplugin");
            }
            set
            {
                SetAttributeValue("rollbackretainplugin", value);
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
		/// Contains the updatemultipleplugin id that should be run when UpdateMultiple is invoked
		/// </summary>
        [AttributeLogicalName("updatemultipleplugin")]
        public Guid? UpdateMultiplePlugin
        {
            get
            {
                return GetAttributeValue<Guid?>("updatemultipleplugin");
            }
            set
            {
                SetAttributeValue("updatemultipleplugin", value);
            }
        }

        /// <summary>
		/// Update Plugin
		/// </summary>
        [AttributeLogicalName("updateplugin")]
        public Guid? UpdatePlugin
        {
            get
            {
                return GetAttributeValue<Guid?>("updateplugin");
            }
            set
            {
                SetAttributeValue("updateplugin", value);
            }
        }

        /// <summary>
		/// Contains the upsertmultipleplugin id that should be run when UpsertMultiple is invoked
		/// </summary>
        [AttributeLogicalName("upsertmultipleplugin")]
        public Guid? UpsertMultiplePlugin
        {
            get
            {
                return GetAttributeValue<Guid?>("upsertmultipleplugin");
            }
            set
            {
                SetAttributeValue("upsertmultipleplugin", value);
            }
        }

        /// <summary>
		/// Contains the upsertplugin id that should be run when Upsert is invoked
		/// </summary>
        [AttributeLogicalName("upsertplugin")]
        public Guid? UpsertPlugin
        {
            get
            {
                return GetAttributeValue<Guid?>("upsertplugin");
            }
            set
            {
                SetAttributeValue("upsertplugin", value);
            }
        }

        /// <summary>
		/// Contains the validatearchiveconfigplugin id that should be run when ValidateArchiveConfig is invoked
		/// </summary>
        [AttributeLogicalName("validatearchiveconfigplugin")]
        public Guid? ValidateArchiveConfigPlugin
        {
            get
            {
                return GetAttributeValue<Guid?>("validatearchiveconfigplugin");
            }
            set
            {
                SetAttributeValue("validatearchiveconfigplugin", value);
            }
        }

        /// <summary>
		/// Contains the validateretentionconfigplugin id that should be run when ValidateRetentionConfig is invoked
		/// </summary>
        [AttributeLogicalName("validateretentionconfigplugin")]
        public Guid? ValidateRetentionConfigPlugin
        {
            get
            {
                return GetAttributeValue<Guid?>("validateretentionconfigplugin");
            }
            set
            {
                SetAttributeValue("validateretentionconfigplugin", value);
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
            public struct IsManaged
            {
                public const bool Unmanaged = false;
                public const bool Managed = true;
            }
            public struct LookupExpansionEnabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
        }
        #endregion

        #region LogicalNames
        public static partial class LogicalNames
        {
            public const string EntityDataProviderId = "entitydataproviderid";
            public const string ArchivePlugin = "archiveplugin";
            public const string BulkArchivePlugin = "bulkarchiveplugin";
            public const string BulkRetainPlugin = "bulkretainplugin";
            public const string ComponentState = "componentstate";
            public const string CreateMultiplePlugin = "createmultipleplugin";
            public const string CreatePlugin = "createplugin";
            public const string DataSourceLogicalName = "datasourcelogicalname";
            public const string DeleteMultiplePlugin = "deletemultipleplugin";
            public const string DeletePlugin = "deleteplugin";
            public const string Description = "description";
            public const string EntityDataProviderIdUnique = "entitydataprovideridunique";
            public const string IntroducedVersion = "introducedversion";
            public const string IsCustomizable = "iscustomizable";
            public const string IsManaged = "ismanaged";
            public const string LookupExpansionEnabled = "lookupexpansionenabled";
            public const string Name = "name";
            public const string OrganizationId = "organizationid";
            public const string OverwriteTime = "overwritetime";
            public const string PurgeArchivedContentPlugin = "purgearchivedcontentplugin";
            public const string PurgeRetainedContentPlugin = "purgeretainedcontentplugin";
            public const string RetainPlugin = "retainplugin";
            public const string RetrieveEntityChangesPlugin = "retrieveentitychangesplugin";
            public const string RetrieveMultiplePlugin = "retrievemultipleplugin";
            public const string RetrievePlugin = "retrieveplugin";
            public const string RollbackRetainPlugin = "rollbackretainplugin";
            public const string SolutionId = "solutionid";
            public const string UpdateMultiplePlugin = "updatemultipleplugin";
            public const string UpdatePlugin = "updateplugin";
            public const string UpsertMultiplePlugin = "upsertmultipleplugin";
            public const string UpsertPlugin = "upsertplugin";
            public const string ValidateArchiveConfigPlugin = "validatearchiveconfigplugin";
            public const string ValidateRetentionConfigPlugin = "validateretentionconfigplugin";
        }
        #endregion

        #region Relations
        public static partial class Relations
        {
            public static class OneToMany
            {
                public const string EntitydataproviderDatasource = "entitydataprovider_datasource";
            }

            public static partial class ManyToOne
            {
                public const string OrganizationEntitydataprovider = "organization_entitydataprovider";
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
        public IQueryable<EntityDataProvider> EntityDataProviderSet
        {
            get
            {
                return CreateQuery<EntityDataProvider>();
            }
        }
    }
    #endregion
}
