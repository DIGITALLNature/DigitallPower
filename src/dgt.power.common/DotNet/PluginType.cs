using System.Diagnostics.CodeAnalysis;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System.Runtime.Serialization;

// ReSharper disable All
namespace dgt.power.dataverse
{
    /// <inheritdoc cref="Microsoft.Xrm.Sdk.Entity" />
    /// <summary>
	/// Type that inherits from the IPlugin interface and is contained within a plug-in assembly.
	/// </summary>
    [EntityLogicalName("plugintype")]
    [System.CodeDom.Compiler.GeneratedCode("dgtp", "2026")]
    [ExcludeFromCodeCoverage]
    [SuppressMessage("Design", "CA1034")]
    [SuppressMessage("Performance", "CA1815")]
    public partial class PluginType : Entity
    {
        #region ctor
        public PluginType() : base(EntityLogicalName) { }

        public PluginType(Guid id) : base(EntityLogicalName, id) { }

        public PluginType(KeyAttributeCollection keyAttributes) : base(EntityLogicalName, keyAttributes) { }

        public PluginType(string keyName, object keyValue) : base(EntityLogicalName, keyName, keyValue) { }
        #endregion

        #region consts
        public const string EntityLogicalName = "plugintype";
        public const string PrimaryNameAttribute = "name";
        public const int EntityTypeCode = 4602;
        #endregion

        #region Attributes
        [AttributeLogicalName("plugintypeid")]
        [IgnoreDataMember]
        public new Guid Id
        {
            get
            {
                return base.Id;
            }
            set
            {
                PluginTypeId = value;
            }
        }

        /// <summary>
		/// Unique identifier of the plug-in type.
		/// </summary>
        [AttributeLogicalName("plugintypeid")]
        public Guid? PluginTypeId
        {
            get
            {
                return GetAttributeValue<Guid?>("plugintypeid");
            }
            set
            {
                SetAttributeValue("plugintypeid", value);
                base.Id = value.HasValue ? value.Value : Guid.Empty;
            }
        }

        /// <summary>
		/// Full path name of the plug-in assembly.
		/// </summary>
        [AttributeLogicalName("assemblyname")]
        public string? AssemblyName
        {
            get
            {
                return GetAttributeValue<string?>("assemblyname");
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
		/// Unique identifier of the user who created the plug-in type.
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
		/// Date and time when the plug-in type was created.
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
		/// Unique identifier of the delegate user who created the plugintype.
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
        }

        /// <summary>
		/// Customization level of the plug-in type.
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
		/// Serialized Custom Activity Type information, including required arguments. For more information, see SandboxCustomActivityInfo.
		/// </summary>
        [AttributeLogicalName("customworkflowactivityinfo")]
        public string? CustomWorkflowActivityInfo
        {
            get
            {
                return GetAttributeValue<string?>("customworkflowactivityinfo");
            }
        }

        /// <summary>
		/// Description of the plug-in type.
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
		/// User friendly name for the plug-in.
		/// </summary>
        [AttributeLogicalName("friendlyname")]
        public string? FriendlyName
        {
            get
            {
                return GetAttributeValue<string?>("friendlyname");
            }
            set
            {
                SetAttributeValue("friendlyname", value);
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
		/// Indicates if the plug-in is a custom activity for workflows.
		/// </summary>
        [AttributeLogicalName("isworkflowactivity")]
        public bool? IsWorkflowActivity
        {
            get
            {
                return GetAttributeValue<bool?>("isworkflowactivity");
            }
        }

        /// <summary>
		/// Major of the version number of the assembly for the plug-in type.
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
		/// Minor of the version number of the assembly for the plug-in type.
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
		/// Unique identifier of the user who last modified the plug-in type.
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
		/// Date and time when the plug-in type was last modified.
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
		/// Unique identifier of the delegate user who last modified the plugintype.
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
		/// Name of the plug-in type.
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
		/// Unique identifier of the organization with which the plug-in type is associated.
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
		/// Unique identifier of the plug-in assembly that contains this plug-in type.
		/// </summary>
        [AttributeLogicalName("pluginassemblyid")]
        public EntityReference? PluginAssemblyId
        {
            get
            {
                return GetAttributeValue<EntityReference?>("pluginassemblyid");
            }
            set
            {
                SetAttributeValue("pluginassemblyid", value);
            }
        }

        /// <summary>
		/// Uniquely identifies the plug-in type associated with a plugin package when exporting a solution.
		/// </summary>
        [AttributeLogicalName("plugintypeexportkey")]
        public string? PluginTypeExportKey
        {
            get
            {
                return GetAttributeValue<string?>("plugintypeexportkey");
            }
            set
            {
                SetAttributeValue("plugintypeexportkey", value);
            }
        }

        /// <summary>
		/// Unique identifier of the plug-in type.
		/// </summary>
        [AttributeLogicalName("plugintypeidunique")]
        public Guid? PluginTypeIdUnique
        {
            get
            {
                return GetAttributeValue<Guid?>("plugintypeidunique");
            }
        }

        /// <summary>
		/// Public key token of the assembly for the plug-in type.
		/// </summary>
        [AttributeLogicalName("publickeytoken")]
        public string? PublicKeyToken
        {
            get
            {
                return GetAttributeValue<string?>("publickeytoken");
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
		/// Fully qualified type name of the plug-in type.
		/// </summary>
        [AttributeLogicalName("typename")]
        public string? TypeName
        {
            get
            {
                return GetAttributeValue<string?>("typename");
            }
            set
            {
                SetAttributeValue("typename", value);
            }
        }

        /// <summary>
		/// Version number of the assembly for the plug-in type.
		/// </summary>
        [AttributeLogicalName("version")]
        public string? Version
        {
            get
            {
                return GetAttributeValue<string?>("version");
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
		/// Group name of workflow custom activity.
		/// </summary>
        [AttributeLogicalName("workflowactivitygroupname")]
        public string? WorkflowActivityGroupName
        {
            get
            {
                return GetAttributeValue<string?>("workflowactivitygroupname");
            }
            set
            {
                SetAttributeValue("workflowactivitygroupname", value);
            }
        }
        #endregion

        #region NavigationProperties

        /// <summary>
        /// 1:N plugintype_customapi
        /// </summary>
        [RelationshipSchemaName("plugintype_customapi")]
        public IEnumerable<CustomAPI> PlugintypeCustomapi
        {
            get
            {
                return GetRelatedEntities<CustomAPI>("plugintype_customapi", null);
            }
            set
            {
                SetRelatedEntities("plugintype_customapi", null, value);
            }
        }

        /// <summary>
        /// 1:N plugintype_sdkmessageprocessingstep
        /// </summary>
        [RelationshipSchemaName("plugintype_sdkmessageprocessingstep")]
        public IEnumerable<SdkMessageProcessingStep> PlugintypeSdkmessageprocessingstep
        {
            get
            {
                return GetRelatedEntities<SdkMessageProcessingStep>("plugintype_sdkmessageprocessingstep", null);
            }
            set
            {
                SetRelatedEntities("plugintype_sdkmessageprocessingstep", null, value);
            }
        }

        /// <summary>
        /// 1:N plugintypeid_sdkmessageprocessingstep
        /// </summary>
        [RelationshipSchemaName("plugintypeid_sdkmessageprocessingstep")]
        public IEnumerable<SdkMessageProcessingStep> PlugintypeidSdkmessageprocessingstep
        {
            get
            {
                return GetRelatedEntities<SdkMessageProcessingStep>("plugintypeid_sdkmessageprocessingstep", null);
            }
            set
            {
                SetRelatedEntities("plugintypeid_sdkmessageprocessingstep", null, value);
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
            public struct IsWorkflowActivity
            {
                public const bool No = false;
                public const bool Yes = true;
            }
        }
        #endregion

        #region LogicalNames
        public static partial class LogicalNames
        {
            public const string PluginTypeId = "plugintypeid";
            public const string AssemblyName = "assemblyname";
            public const string ComponentState = "componentstate";
            public const string CreatedBy = "createdby";
            public const string CreatedOn = "createdon";
            public const string CreatedOnBehalfBy = "createdonbehalfby";
            public const string Culture = "culture";
            public const string CustomizationLevel = "customizationlevel";
            public const string CustomWorkflowActivityInfo = "customworkflowactivityinfo";
            public const string Description = "description";
            public const string FriendlyName = "friendlyname";
            public const string IsManaged = "ismanaged";
            public const string IsWorkflowActivity = "isworkflowactivity";
            public const string Major = "major";
            public const string Minor = "minor";
            public const string ModifiedBy = "modifiedby";
            public const string ModifiedOn = "modifiedon";
            public const string ModifiedOnBehalfBy = "modifiedonbehalfby";
            public const string Name = "name";
            public const string OrganizationId = "organizationid";
            public const string OverwriteTime = "overwritetime";
            public const string PluginAssemblyId = "pluginassemblyid";
            public const string PluginTypeExportKey = "plugintypeexportkey";
            public const string PluginTypeIdUnique = "plugintypeidunique";
            public const string PublicKeyToken = "publickeytoken";
            public const string SolutionId = "solutionid";
            public const string TypeName = "typename";
            public const string Version = "version";
            public const string VersionNumber = "versionnumber";
            public const string WorkflowActivityGroupName = "workflowactivitygroupname";
        }
        #endregion

        #region AlternateKeys
        public static partial class AlternateKeys
        {
            public const string PluginTypeEntityKey1 = "plugintypentitykey";
        }
        #endregion

        #region Relations
        public static partial class Relations
        {
            public static class OneToMany
            {
                public const string PlugintypeCustomapi = "plugintype_customapi";
                public const string PlugintypePlugintypestatistic = "plugintype_plugintypestatistic";
                public const string PlugintypeSdkmessageprocessingstep = "plugintype_sdkmessageprocessingstep";
                public const string PlugintypeidSdkmessageprocessingstep = "plugintypeid_sdkmessageprocessingstep";
                public const string UserentityinstancedataPlugintype = "userentityinstancedata_plugintype";
            }

            public static partial class ManyToOne
            {
                public const string CreatedbyPlugintype = "createdby_plugintype";
                public const string LkPlugintypeCreatedonbehalfby = "lk_plugintype_createdonbehalfby";
                public const string LkPlugintypeModifiedonbehalfby = "lk_plugintype_modifiedonbehalfby";
                public const string ModifiedbyPlugintype = "modifiedby_plugintype";
                public const string OrganizationPlugintype = "organization_plugintype";
                public const string PluginassemblyPlugintype = "pluginassembly_plugintype";
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
        public IQueryable<PluginType> PluginTypeSet
        {
            get
            {
                return CreateQuery<PluginType>();
            }
        }
    }
    #endregion
}
