using System.Diagnostics.CodeAnalysis;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System.Runtime.Serialization;

// ReSharper disable All
namespace dgt.power.dataverse
{
    /// <inheritdoc cref="Microsoft.Xrm.Sdk.Entity" />
    /// <summary>
	/// Grouping of security privileges. Users are assigned roles that authorize their access to the Microsoft CRM system.
	/// </summary>
    [EntityLogicalName("role")]
    [System.CodeDom.Compiler.GeneratedCode("dgtp", "2026")]
    [ExcludeFromCodeCoverage]
    [SuppressMessage("Design", "CA1034")]
    [SuppressMessage("Performance", "CA1815")]
    public partial class Role : Entity
    {
        #region ctor
        public Role() : base(EntityLogicalName) { }

        public Role(Guid id) : base(EntityLogicalName, id) { }

        public Role(KeyAttributeCollection keyAttributes) : base(EntityLogicalName, keyAttributes) { }

        public Role(string keyName, object keyValue) : base(EntityLogicalName, keyName, keyValue) { }
        #endregion

        #region consts
        public const string EntityLogicalName = "role";
        public const string PrimaryNameAttribute = "name";
        public const int EntityTypeCode = 1036;
        #endregion

        #region Attributes
        [AttributeLogicalName("roleid")]
        [IgnoreDataMember]
        public new Guid Id
        {
            get
            {
                return base.Id;
            }
            set
            {
                RoleId = value;
            }
        }

        /// <summary>
		/// Unique identifier of the role.
		/// </summary>
        [AttributeLogicalName("roleid")]
        public Guid? RoleId
        {
            get
            {
                return GetAttributeValue<Guid?>("roleid");
            }
            set
            {
                SetAttributeValue("roleid", value);
                base.Id = value.HasValue ? value.Value : Guid.Empty;
            }
        }

        /// <summary>
		/// Application Id of user who created the role
		/// </summary>
        [AttributeLogicalName("applicationid")]
        public Guid? ApplicationId
        {
            get
            {
                return GetAttributeValue<Guid?>("applicationid");
            }
            set
            {
                SetAttributeValue("applicationid", value);
            }
        }

        /// <summary>
		/// Personas/Licenses the security role applies to
		/// </summary>
        [AttributeLogicalName("appliesto")]
        public string? AppliesTo
        {
            get
            {
                return GetAttributeValue<string?>("appliesto");
            }
            set
            {
                SetAttributeValue("appliesto", value);
            }
        }

        /// <summary>
		/// Unique identifier of the business unit with which the role is associated.
		/// </summary>
        [AttributeLogicalName("businessunitid")]
        public EntityReference? BusinessUnitId
        {
            get
            {
                return GetAttributeValue<EntityReference?>("businessunitid");
            }
            set
            {
                SetAttributeValue("businessunitid", value);
            }
        }

        /// <summary>
		/// Tells whether the role can be deleted.
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
		/// Unique identifier of the user who created the role.
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
		/// Date and time when the role was created.
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
		/// Unique identifier of the delegate user who created the role.
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
		/// Description of the security role
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
		/// The Id, Aot Name, of an FnORole.
		/// </summary>
        [AttributeLogicalName("FnOAotName")]
        public string? FnOAotName
        {
            get
            {
                return GetAttributeValue<string?>("FnOAotName");
            }
            set
            {
                SetAttributeValue("FnOAotName", value);
            }
        }

        /// <summary>
		/// Unique identifier of the data import or data migration that created this record.
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
		/// Value indicating whether security role is auto-assigned based on user license
		/// </summary>
        [AttributeLogicalName("isautoassigned")]
        public OptionSetValue? IsAutoAssigned
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("isautoassigned");
            }
            set
            {
                SetAttributeValue("isautoassigned", value);
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
		/// Role is inherited by users from team membership, if role associated with team.
		/// </summary>
        [AttributeLogicalName("isinherited")]
        public OptionSetValue? IsInherited
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("isinherited");
            }
            set
            {
                SetAttributeValue("isinherited", value);
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
		/// Is this role generated by the system
		/// </summary>
        [AttributeLogicalName("issytemgenerated")]
        public bool? IsSystemGenerated
        {
            get
            {
                return GetAttributeValue<bool?>("issytemgenerated");
            }
            set
            {
                SetAttributeValue("issytemgenerated", value);
            }
        }

        /// <summary>
		/// Unique identifier of the user who last modified the role.
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
		/// Date and time when the role was last modified.
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
		/// Unique identifier of the delegate user who last modified the role.
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
		/// Name of the role.
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
		/// Unique identifier of the organization associated with the role.
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
		/// Unique identifier of the parent role.
		/// </summary>
        [AttributeLogicalName("parentroleid")]
        public EntityReference? ParentRoleId
        {
            get
            {
                return GetAttributeValue<EntityReference?>("parentroleid");
            }
        }

        /// <summary>
		/// Unique identifier of the parent root role.
		/// </summary>
        [AttributeLogicalName("parentrootroleid")]
        public EntityReference? ParentRootRoleId
        {
            get
            {
                return GetAttributeValue<EntityReference?>("parentrootroleid");
            }
        }

        /// <summary>
		/// For internal use only.
		/// </summary>
        [AttributeLogicalName("roleidunique")]
        public Guid? RoleIdUnique
        {
            get
            {
                return GetAttributeValue<Guid?>("roleidunique");
            }
        }

        /// <summary>
		/// Unique identifier of the role template that is associated with the role.
		/// </summary>
        [AttributeLogicalName("roletemplateid")]
        public EntityReference? RoleTemplateId
        {
            get
            {
                return GetAttributeValue<EntityReference?>("roletemplateid");
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
		/// Summary of Core Table Permissions of the Role
		/// </summary>
        [AttributeLogicalName("summaryofcoretablepermissions")]
        public string? SummaryofCoreTablePermissions
        {
            get
            {
                return GetAttributeValue<string?>("summaryofcoretablepermissions");
            }
            set
            {
                SetAttributeValue("summaryofcoretablepermissions", value);
            }
        }

        /// <summary>
		/// Version number of the role.
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
        /// 1:N Role_AsyncOperations
        /// </summary>
        [RelationshipSchemaName("Role_AsyncOperations")]
        public IEnumerable<AsyncOperation> RoleAsyncOperations
        {
            get
            {
                return GetRelatedEntities<AsyncOperation>("Role_AsyncOperations", null);
            }
            set
            {
                SetRelatedEntities("Role_AsyncOperations", null, value);
            }
        }

        /// <summary>
        /// 1:N role_parent_role
        /// </summary>
        [RelationshipSchemaName("role_parent_role")]
        public IEnumerable<Role> RoleParentRole
        {
            get
            {
                return GetRelatedEntities<Role>("role_parent_role", null);
            }
            set
            {
                SetRelatedEntities("role_parent_role", null, value);
            }
        }

        /// <summary>
        /// 1:N role_parent_root_role
        /// </summary>
        [RelationshipSchemaName("role_parent_root_role")]
        public IEnumerable<Role> RoleParentRootRole
        {
            get
            {
                return GetRelatedEntities<Role>("role_parent_root_role", null);
            }
            set
            {
                SetRelatedEntities("role_parent_root_role", null, value);
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
            public struct IsAutoAssigned
            {
                public const int No = 0;
                public const int Yes = 1;
            }
            public struct IsInherited
            {
                public const int TeamPrivilegesOnly = 0;
                public const int DirectUserBasicAccessLevelAndTeamPrivileges = 1;
            }
            public struct IsManaged
            {
                public const bool Unmanaged = false;
                public const bool Managed = true;
            }
            public struct IsSystemGenerated
            {
                public const bool No = false;
                public const bool Yes = true;
            }
        }
        #endregion

        #region LogicalNames
        public static partial class LogicalNames
        {
            public const string RoleId = "roleid";
            public const string ApplicationId = "applicationid";
            public const string AppliesTo = "appliesto";
            public const string BusinessUnitId = "businessunitid";
            public const string CanBeDeleted = "canbedeleted";
            public const string ComponentState = "componentstate";
            public const string CreatedBy = "createdby";
            public const string CreatedOn = "createdon";
            public const string CreatedOnBehalfBy = "createdonbehalfby";
            public const string Description = "description";
            public const string FnOAotName = "FnOAotName";
            public const string ImportSequenceNumber = "importsequencenumber";
            public const string IsAutoAssigned = "isautoassigned";
            public const string IsCustomizable = "iscustomizable";
            public const string IsInherited = "isinherited";
            public const string IsManaged = "ismanaged";
            public const string IsSystemGenerated = "issytemgenerated";
            public const string ModifiedBy = "modifiedby";
            public const string ModifiedOn = "modifiedon";
            public const string ModifiedOnBehalfBy = "modifiedonbehalfby";
            public const string Name = "name";
            public const string OrganizationId = "organizationid";
            public const string OverriddenCreatedOn = "overriddencreatedon";
            public const string OverwriteTime = "overwritetime";
            public const string ParentRoleId = "parentroleid";
            public const string ParentRootRoleId = "parentrootroleid";
            public const string RoleIdUnique = "roleidunique";
            public const string RoleTemplateId = "roletemplateid";
            public const string SolutionId = "solutionid";
            public const string SummaryofCoreTablePermissions = "summaryofcoretablepermissions";
            public const string VersionNumber = "versionnumber";
        }
        #endregion

        #region AlternateKeys
        public static partial class AlternateKeys
        {
            public const string UniqueFnOAotNameName = "fnoaotnameid";
            public const string ParentRootRoleIdBusinessUnitLookupKey = "parentrootroleid_businessunitid";
            public const string RoleTemplateIdBusinessUnitLookupKey = "roletemplateid_businessunitid";
        }
        #endregion

        #region Relations
        public static partial class Relations
        {
            public static class OneToMany
            {
                public const string RoleAsyncOperations = "Role_AsyncOperations";
                public const string RoleBulkDeleteFailures = "Role_BulkDeleteFailures";
                public const string RoleParentRole = "role_parent_role";
                public const string RoleParentRootRole = "role_parent_root_role";
                public const string RoleSyncErrors = "Role_SyncErrors";
                public const string SkillrolemappingRoleIdRole = "skillrolemapping_RoleId_role";
                public const string UserentityinstancedataRole = "userentityinstancedata_role";
            }

            public static partial class ManyToOne
            {
                public const string BusinessUnitRoles = "business_unit_roles";
                public const string LkRoleCreatedonbehalfby = "lk_role_createdonbehalfby";
                public const string LkRoleModifiedonbehalfby = "lk_role_modifiedonbehalfby";
                public const string LkRolebaseCreatedby = "lk_rolebase_createdby";
                public const string LkRolebaseModifiedby = "lk_rolebase_modifiedby";
                public const string OrganizationRoles = "organization_roles";
                public const string RoleParentRole = "role_parent_role";
                public const string RoleParentRootRole = "role_parent_root_role";
                public const string RoleTemplateRoles = "role_template_roles";
                public const string SolutionRole = "solution_role";
            }

            public static partial class ManyToMany
            {
                public const string ApplicationRole = "application_role";
                public const string Applicationuserrole = "applicationuserrole";
                public const string AppmodulerolesAssociation = "appmoduleroles_association";
                public const string RoleprivilegesAssociation = "roleprivileges_association";
                public const string SystemuserrolesAssociation = "systemuserroles_association";
                public const string TeamrolesAssociation = "teamroles_association";
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
        public IQueryable<Role> RoleSet
        {
            get
            {
                return CreateQuery<Role>();
            }
        }
    }
    #endregion
}
