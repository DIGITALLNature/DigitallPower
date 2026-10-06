using System.Diagnostics.CodeAnalysis;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System.Runtime.Serialization;

// ReSharper disable All
namespace dgt.power.dataverse
{
    /// <inheritdoc cref="Microsoft.Xrm.Sdk.Entity" />
    /// <summary>
	/// A solution which contains CRM customizations.
	/// </summary>
    [EntityLogicalName("solution")]
    [System.CodeDom.Compiler.GeneratedCode("dgtp", "2026")]
    [ExcludeFromCodeCoverage]
    [SuppressMessage("Design", "CA1034")]
    [SuppressMessage("Performance", "CA1815")]
    public partial class Solution : Entity
    {
        #region ctor
        public Solution() : base(EntityLogicalName) { }

        public Solution(Guid id) : base(EntityLogicalName, id) { }

        public Solution(KeyAttributeCollection keyAttributes) : base(EntityLogicalName, keyAttributes) { }

        public Solution(string keyName, object keyValue) : base(EntityLogicalName, keyName, keyValue) { }
        #endregion

        #region consts
        public const string EntityLogicalName = "solution";
        public const string PrimaryNameAttribute = "friendlyname";
        public const int EntityTypeCode = 7100;
        #endregion

        #region Attributes
        [AttributeLogicalName("solutionid")]
        [IgnoreDataMember]
        public new Guid Id
        {
            get
            {
                return base.Id;
            }
            set
            {
                SolutionId = value;
            }
        }

        /// <summary>
		/// Unique identifier of the solution.
		/// </summary>
        [AttributeLogicalName("solutionid")]
        public Guid? SolutionId
        {
            get
            {
                return GetAttributeValue<Guid?>("solutionid");
            }
            set
            {
                SetAttributeValue("solutionid", value);
                base.Id = value.HasValue ? value.Value : Guid.Empty;
            }
        }

        /// <summary>
		/// A link to an optional configuration page for this solution.
		/// </summary>
        [AttributeLogicalName("configurationpageid")]
        public EntityReference? ConfigurationPageId
        {
            get
            {
                return GetAttributeValue<EntityReference?>("configurationpageid");
            }
            set
            {
                SetAttributeValue("configurationpageid", value);
            }
        }

        /// <summary>
		/// Unique identifier of the user who created the solution.
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
		/// Date and time when the solution was created.
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
		/// Unique identifier of the delegate user who created the solution.
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
		/// Description of the solution.
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
		/// Indicates if solution is enabled for source control integration
		/// </summary>
        [AttributeLogicalName("enabledforsourcecontrolintegration")]
        public bool? EnabledForSourceControlIntegration
        {
            get
            {
                return GetAttributeValue<bool?>("enabledforsourcecontrolintegration");
            }
            set
            {
                SetAttributeValue("enabledforsourcecontrolintegration", value);
            }
        }

        /// <summary>
		/// User display name for the solution.
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

        /// <summary>
		/// Date and time when the solution was installed/upgraded.
		/// </summary>
        [AttributeLogicalName("installedon")]
        public DateTime? InstalledOn
        {
            get
            {
                return GetAttributeValue<DateTime?>("installedon");
            }
        }

        /// <summary>
		/// Information about whether the solution is api managed.
		/// </summary>
        [AttributeLogicalName("isapimanaged")]
        public bool? IsApiManaged
        {
            get
            {
                return GetAttributeValue<bool?>("isapimanaged");
            }
        }

        /// <summary>
		/// Indicates whether the solution is managed or unmanaged.
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
		/// Indicates whether the solution is visible outside of the platform.
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
		/// Unique identifier of the user who last modified the solution.
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
		/// Date and time when the solution was last modified.
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
		/// Unique identifier of the delegate user who modified the solution.
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
		/// Unique identifier of the organization associated with the solution.
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
		/// Unique identifier of the parent solution. Should only be non-null if this solution is a patch.
		/// </summary>
        [AttributeLogicalName("parentsolutionid")]
        public EntityReference? ParentSolutionId
        {
            get
            {
                return GetAttributeValue<EntityReference?>("parentsolutionid");
            }
        }

        
        [AttributeLogicalName("pinpointassetid")]
        public string? PinpointAssetId
        {
            get
            {
                return GetAttributeValue<string?>("pinpointassetid");
            }
        }

        /// <summary>
		/// Identifier of the publisher of this solution in Microsoft Pinpoint.
		/// </summary>
        [AttributeLogicalName("pinpointpublisherid")]
        public long? PinpointPublisherId
        {
            get
            {
                return GetAttributeValue<long?>("pinpointpublisherid");
            }
        }

        /// <summary>
		/// Default locale of the solution in Microsoft Pinpoint.
		/// </summary>
        [AttributeLogicalName("pinpointsolutiondefaultlocale")]
        public string? PinpointSolutionDefaultLocale
        {
            get
            {
                return GetAttributeValue<string?>("pinpointsolutiondefaultlocale");
            }
        }

        /// <summary>
		/// Identifier of the solution in Microsoft Pinpoint.
		/// </summary>
        [AttributeLogicalName("pinpointsolutionid")]
        public long? PinpointSolutionId
        {
            get
            {
                return GetAttributeValue<long?>("pinpointsolutionid");
            }
        }

        /// <summary>
		/// Unique identifier of the publisher.
		/// </summary>
        [AttributeLogicalName("publisherid")]
        public EntityReference? PublisherId
        {
            get
            {
                return GetAttributeValue<EntityReference?>("publisherid");
            }
            set
            {
                SetAttributeValue("publisherid", value);
            }
        }

        /// <summary>
		/// Solution package source organization version
		/// </summary>
        [AttributeLogicalName("solutionpackageversion")]
        public string? SolutionPackageVersion
        {
            get
            {
                return GetAttributeValue<string?>("solutionpackageversion");
            }
            set
            {
                SetAttributeValue("solutionpackageversion", value);
            }
        }

        /// <summary>
		/// Solution Type
		/// </summary>
        [AttributeLogicalName("solutiontype")]
        public OptionSetValue? SolutionType
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("solutiontype");
            }
            set
            {
                SetAttributeValue("solutiontype", value);
            }
        }

        /// <summary>
		/// Indicates the current status of source control integration
		/// </summary>
        [AttributeLogicalName("sourcecontrolsyncstatus")]
        public OptionSetValue? SourceControlSyncStatus
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("sourcecontrolsyncstatus");
            }
            set
            {
                SetAttributeValue("sourcecontrolsyncstatus", value);
            }
        }

        /// <summary>
		/// The template suffix of this solution
		/// </summary>
        [AttributeLogicalName("templatesuffix")]
        public string? TemplateSuffix
        {
            get
            {
                return GetAttributeValue<string?>("templatesuffix");
            }
            set
            {
                SetAttributeValue("templatesuffix", value);
            }
        }

        /// <summary>
		/// thumbprint of the solution signature
		/// </summary>
        [AttributeLogicalName("thumbprint")]
        public string? Thumbprint
        {
            set
            {
                SetAttributeValue("thumbprint", value);
            }
        }

        /// <summary>
		/// The unique name of this solution
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
		/// Date and time when the solution was updated.
		/// </summary>
        [AttributeLogicalName("updatedon")]
        public DateTime? UpdatedOn
        {
            get
            {
                return GetAttributeValue<DateTime?>("updatedon");
            }
        }

        /// <summary>
		/// Contains component info for the solution upgrade operation
		/// </summary>
        [AttributeLogicalName("upgradeinfo")]
        public string? UpgradeInfo
        {
            get
            {
                return GetAttributeValue<string?>("upgradeinfo");
            }
        }

        /// <summary>
		/// Solution version, used to identify a solution for upgrades and hotfixes.
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
        /// 1:N solution_parent_solution
        /// </summary>
        [RelationshipSchemaName("solution_parent_solution")]
        public IEnumerable<Solution> SolutionParentSolution
        {
            get
            {
                return GetRelatedEntities<Solution>("solution_parent_solution", null);
            }
            set
            {
                SetRelatedEntities("solution_parent_solution", null, value);
            }
        }

        /// <summary>
        /// 1:N solution_role
        /// </summary>
        [RelationshipSchemaName("solution_role")]
        public IEnumerable<Role> SolutionRole
        {
            get
            {
                return GetRelatedEntities<Role>("solution_role", null);
            }
            set
            {
                SetRelatedEntities("solution_role", null, value);
            }
        }

        /// <summary>
        /// 1:N solution_solutioncomponent
        /// </summary>
        [RelationshipSchemaName("solution_solutioncomponent")]
        public IEnumerable<SolutionComponent> SolutionSolutioncomponent
        {
            get
            {
                return GetRelatedEntities<SolutionComponent>("solution_solutioncomponent", null);
            }
            set
            {
                SetRelatedEntities("solution_solutioncomponent", null, value);
            }
        }
        #endregion

        #region Options
        public static partial class Options
        {
            public struct EnabledForSourceControlIntegration
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsApiManaged
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
            public struct SolutionType
            {
                public const int None = 0;
                public const int Snapshot = 1;
                public const int Internal = 2;
            }
            public struct SourceControlSyncStatus
            {
                public const int NotStarted = 0;
                public const int InitialSyncInProgress = 1;
                public const int ErrorsInInitialSync = 2;
                public const int PendingChangesToBeCommitted = 3;
                public const int Committed = 4;
            }
        }
        #endregion

        #region LogicalNames
        public static partial class LogicalNames
        {
            public const string SolutionId = "solutionid";
            public const string ConfigurationPageId = "configurationpageid";
            public const string CreatedBy = "createdby";
            public const string CreatedOn = "createdon";
            public const string CreatedOnBehalfBy = "createdonbehalfby";
            public const string Description = "description";
            public const string EnabledForSourceControlIntegration = "enabledforsourcecontrolintegration";
            public const string FriendlyName = "friendlyname";
            public const string InstalledOn = "installedon";
            public const string IsApiManaged = "isapimanaged";
            public const string IsManaged = "ismanaged";
            public const string IsVisible = "isvisible";
            public const string ModifiedBy = "modifiedby";
            public const string ModifiedOn = "modifiedon";
            public const string ModifiedOnBehalfBy = "modifiedonbehalfby";
            public const string OrganizationId = "organizationid";
            public const string ParentSolutionId = "parentsolutionid";
            public const string PinpointAssetId = "pinpointassetid";
            public const string PinpointPublisherId = "pinpointpublisherid";
            public const string PinpointSolutionDefaultLocale = "pinpointsolutiondefaultlocale";
            public const string PinpointSolutionId = "pinpointsolutionid";
            public const string PublisherId = "publisherid";
            public const string SolutionPackageVersion = "solutionpackageversion";
            public const string SolutionType = "solutiontype";
            public const string SourceControlSyncStatus = "sourcecontrolsyncstatus";
            public const string TemplateSuffix = "templatesuffix";
            public const string Thumbprint = "thumbprint";
            public const string UniqueName = "uniquename";
            public const string UpdatedOn = "updatedon";
            public const string UpgradeInfo = "upgradeinfo";
            public const string Version = "version";
            public const string VersionNumber = "versionnumber";
        }
        #endregion

        #region Relations
        public static partial class Relations
        {
            public static class OneToMany
            {
                public const string FileAttachmentSolution = "FileAttachment_Solution";
                public const string FKCanvasAppSolution = "FK_CanvasApp_Solution";
                public const string SolutionBaseDependencynode = "solution_base_dependencynode";
                public const string SolutionFieldpermission = "solution_fieldpermission";
                public const string SolutionFieldsecurityprofile = "solution_fieldsecurityprofile";
                public const string SolutionParentSolution = "solution_parent_solution";
                public const string SolutionPrivilege = "solution_privilege";
                public const string SolutionRole = "solution_role";
                public const string SolutionRoleprivileges = "solution_roleprivileges";
                public const string SolutionSolutioncomponent = "solution_solutioncomponent";
                public const string SolutionSyncErrors = "Solution_SyncErrors";
                public const string SolutionTopDependencynode = "solution_top_dependencynode";
                public const string UserSettingsPreferredSolution = "user_settings_preferred_solution";
                public const string UserentityinstancedataSolution = "userentityinstancedata_solution";
            }

            public static partial class ManyToOne
            {
                public const string FileattachmentSolutionFileid = "fileattachment_solution_fileid";
                public const string LkSolutionCreatedby = "lk_solution_createdby";
                public const string LkSolutionModifiedby = "lk_solution_modifiedby";
                public const string LkSolutionbaseCreatedonbehalfby = "lk_solutionbase_createdonbehalfby";
                public const string LkSolutionbaseModifiedonbehalfby = "lk_solutionbase_modifiedonbehalfby";
                public const string OrganizationSolution = "organization_solution";
                public const string PublisherSolution = "publisher_solution";
                public const string SolutionConfigurationWebresource = "solution_configuration_webresource";
                public const string SolutionParentSolution = "solution_parent_solution";
            }

            public static partial class ManyToMany
            {
                public const string PackageSolution = "package_solution";
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
        public IQueryable<Solution> SolutionSet
        {
            get
            {
                return CreateQuery<Solution>();
            }
        }
    }
    #endregion
}
