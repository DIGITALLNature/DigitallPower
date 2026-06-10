using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using AttributeCollection = Microsoft.Xrm.Sdk.AttributeCollection;

// ReSharper disable All
namespace dgt.power.dataverse
{
    /// <inheritdoc cref="Microsoft.Xrm.Sdk.Entity" />
    /// <summary>
	/// Grouping of security privileges. Users are assigned roles that authorize their access to the Microsoft CRM system.
	/// </summary>
    [DataContract]
    [EntityLogicalName("role")]
    [System.CodeDom.Compiler.GeneratedCode("dgtp", "2026")]
    [ExcludeFromCodeCoverage]
    public partial class Role : Entity, INotifyPropertyChanging, INotifyPropertyChanged
    {
        #region ctor
        [DebuggerNonUserCode]
        public Role() : this(false)
        {
        }
        [DebuggerNonUserCode]
        public Role(bool trackChanges = false) : base(EntityLogicalName)
        {
            _trackChanges = trackChanges;
        }
        [DebuggerNonUserCode]
        public Role(Guid id, bool trackChanges = false) : base(EntityLogicalName, id)
        {
            _trackChanges = trackChanges;
        }
        [DebuggerNonUserCode]
        public Role(KeyAttributeCollection keyAttributes, bool trackChanges = false) : base(EntityLogicalName, keyAttributes)
        {
            _trackChanges = trackChanges;
        }
        [DebuggerNonUserCode]
        public Role(string keyName, object keyValue, bool trackChanges = false) : base(EntityLogicalName, keyName, keyValue)
        {
            _trackChanges = trackChanges;
        }
        #endregion

        #region fields
        private readonly bool _trackChanges;
        private readonly Lazy<HashSet<string>> _changedProperties = new();
        #endregion

        #region consts
        public const string EntityLogicalName = "role";
        public const string PrimaryNameAttribute = "name";
        public const int EntityTypeCode = 1036;
        #endregion

        #region Events
        #pragma warning disable CS8612
        public event PropertyChangedEventHandler? PropertyChanged;
        public event PropertyChangingEventHandler? PropertyChanging;
        #pragma warning restore CS8612
        [DebuggerNonUserCode]
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            if (_trackChanges)
            {
                _changedProperties.Value.Add(propertyName);
            }
        }
        [DebuggerNonUserCode]
        private void OnPropertyChanging([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanging != null) PropertyChanging.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }
        #endregion

        #region Attributes
        [AttributeLogicalName("roleid")]
        public new Guid Id
        {
            [DebuggerNonUserCode]
            get
            {
                return base.Id;
            }
            [DebuggerNonUserCode]
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
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<Guid?>("roleid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("roleid", value);
                base.Id = value.HasValue ? value.Value : Guid.Empty;
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Application Id of user who created the role
		/// </summary>
        [AttributeLogicalName("applicationid")]
        public Guid? ApplicationId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<Guid?>("applicationid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("applicationid", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Personas/Licenses the security role applies to
		/// </summary>
        [AttributeLogicalName("appliesto")]
        public string? AppliesTo
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("appliesto");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("appliesto", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier of the business unit with which the role is associated.
		/// </summary>
        [AttributeLogicalName("businessunitid")]
        public EntityReference? BusinessUnitId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<EntityReference?>("businessunitid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("businessunitid", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Tells whether the role can be deleted.
		/// </summary>
        [AttributeLogicalName("canbedeleted")]
        public BooleanManagedProperty? CanBeDeleted
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<BooleanManagedProperty?>("canbedeleted");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("canbedeleted", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// For internal use only.
		/// </summary>
        [AttributeLogicalName("componentstate")]
        public OptionSetValue? ComponentState
        {
            [DebuggerNonUserCode]
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
            [DebuggerNonUserCode]
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
            [DebuggerNonUserCode]
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
            [DebuggerNonUserCode]
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
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("description");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("description", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier of the data import or data migration that created this record.
		/// </summary>
        [AttributeLogicalName("importsequencenumber")]
        public int? ImportSequenceNumber
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("importsequencenumber");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("importsequencenumber", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Value indicating whether security role is auto-assigned based on user license
		/// </summary>
        [AttributeLogicalName("isautoassigned")]
        public OptionSetValue? IsAutoAssigned
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("isautoassigned");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("isautoassigned", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Information that specifies whether this component can be customized.
		/// </summary>
        [AttributeLogicalName("iscustomizable")]
        public BooleanManagedProperty? IsCustomizable
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<BooleanManagedProperty?>("iscustomizable");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("iscustomizable", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Role is inherited by users from team membership, if role associated with team.
		/// </summary>
        [AttributeLogicalName("isinherited")]
        public OptionSetValue? IsInherited
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("isinherited");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("isinherited", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates whether the solution component is part of a managed solution.
		/// </summary>
        [AttributeLogicalName("ismanaged")]
        public bool? IsManaged
        {
            [DebuggerNonUserCode]
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
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("issytemgenerated");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("issytemgenerated", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier of the user who last modified the role.
		/// </summary>
        [AttributeLogicalName("modifiedby")]
        public EntityReference? ModifiedBy
        {
            [DebuggerNonUserCode]
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
            [DebuggerNonUserCode]
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
            [DebuggerNonUserCode]
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
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("name");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("name", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier of the organization associated with the role.
		/// </summary>
        [AttributeLogicalName("organizationid")]
        public Guid? OrganizationId
        {
            [DebuggerNonUserCode]
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
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<DateTime?>("overriddencreatedon");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("overriddencreatedon", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// For internal use only.
		/// </summary>
        [AttributeLogicalName("overwritetime")]
        public DateTime? OverwriteTime
        {
            [DebuggerNonUserCode]
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
            [DebuggerNonUserCode]
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
            [DebuggerNonUserCode]
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
            [DebuggerNonUserCode]
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
            [DebuggerNonUserCode]
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
            [DebuggerNonUserCode]
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
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("summaryofcoretablepermissions");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("summaryofcoretablepermissions", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Version number of the role.
		/// </summary>
        [AttributeLogicalName("versionnumber")]
        public long? VersionNumber
        {
            [DebuggerNonUserCode]
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
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<AsyncOperation>("Role_AsyncOperations", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("Role_AsyncOperations", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N role_parent_role
        /// </summary>
        [RelationshipSchemaName("role_parent_role")]
        public IEnumerable<Role> RoleParentRole
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<Role>("role_parent_role", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("role_parent_role", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N role_parent_root_role
        /// </summary>
        [RelationshipSchemaName("role_parent_root_role")]
        public IEnumerable<Role> RoleParentRootRole
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<Role>("role_parent_root_role", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("role_parent_root_role", null, value);
                OnPropertyChanged();
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

        public static Role Retrieve(IOrganizationService service, Guid id)
        {
            return Retrieve(service, id, new ColumnSet(true));
        }

        public static Role Retrieve(IOrganizationService service, Guid id, ColumnSet columnSet)
        {
            return service.Retrieve("role", id, columnSet).ToEntity<Role>();
        }

        public Role GetChangedEntity()
        {
            if (!_trackChanges)
            {
                return this;
            }
                var attr = new AttributeCollection();
            foreach (var attrName in _changedProperties.Value.Select(changedProperty => ((AttributeLogicalNameAttribute) GetType().GetProperty(changedProperty)!.GetCustomAttribute(typeof(AttributeLogicalNameAttribute))!).LogicalName).Where(attrName => Contains(attrName)))
            {
                attr.Add(attrName, this[attrName]);
            }
            return new Role(Id) { Attributes = attr };
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
