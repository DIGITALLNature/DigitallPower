using System.Diagnostics.CodeAnalysis;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Reflection;
using System.Runtime.Serialization;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query; 
using AttributeCollection = Microsoft.Xrm.Sdk.AttributeCollection;

// ReSharper disable All
namespace dgt.power.dataverse
{
	/// <inheritdoc />
	/// <summary>
	/// Grouping of security privileges. Users are assigned roles that authorize their access to the Microsoft CRM system.
	/// </summary>
	[DataContractAttribute()]
	[EntityLogicalNameAttribute("role")]
	[System.CodeDom.Compiler.GeneratedCode("dgtp", "2023")]
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
		public Role(Guid id, bool trackChanges = false) : base(EntityLogicalName,id)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public Role(KeyAttributeCollection keyAttributes, bool trackChanges = false) : base(EntityLogicalName,keyAttributes)
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
        private readonly Lazy<HashSet<string>> _changedProperties = new Lazy<HashSet<string>>();
        #endregion

        #region consts
        public const string EntityLogicalName = "role";
        public const int EntityTypeCode = 1036;
        #endregion

        #region Events
        public event PropertyChangedEventHandler? PropertyChanged;
        public event PropertyChangingEventHandler? PropertyChanging;

        [DebuggerNonUserCode]
		private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null) PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
		[AttributeLogicalNameAttribute("roleid")]
		public new System.Guid Id
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
                OnPropertyChanging(nameof(RoleId));
                SetAttributeValue("roleid", value);
				if (value.HasValue)
				{
					base.Id = value.Value;
				}
				else
				{
					base.Id = System.Guid.Empty;
				}
                OnPropertyChanged(nameof(RoleId));
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
                OnPropertyChanging(nameof(BusinessUnitId));
                SetAttributeValue("businessunitid", value);
                OnPropertyChanged(nameof(BusinessUnitId));
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
                OnPropertyChanging(nameof(CanBeDeleted));
                SetAttributeValue("canbedeleted", value);
                OnPropertyChanged(nameof(CanBeDeleted));
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
                OnPropertyChanging(nameof(ImportSequenceNumber));
                SetAttributeValue("importsequencenumber", value);
                OnPropertyChanged(nameof(ImportSequenceNumber));
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
                OnPropertyChanging(nameof(IsCustomizable));
                SetAttributeValue("iscustomizable", value);
                OnPropertyChanged(nameof(IsCustomizable));
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
                OnPropertyChanging(nameof(IsInherited));
                SetAttributeValue("isinherited", value);
                OnPropertyChanged(nameof(IsInherited));
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
                OnPropertyChanging(nameof(Name));
                SetAttributeValue("name", value);
                OnPropertyChanged(nameof(Name));
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
                OnPropertyChanging(nameof(OverriddenCreatedOn));
                SetAttributeValue("overriddencreatedon", value);
                OnPropertyChanged(nameof(OverriddenCreatedOn));
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
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("Role_AsyncOperations")]
		public System.Collections.Generic.IEnumerable<AsyncOperation> RoleAsyncOperations
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<AsyncOperation>("Role_AsyncOperations", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("RoleAsyncOperations");
				this.SetRelatedEntities<AsyncOperation>("Role_AsyncOperations", null, value);
				this.OnPropertyChanged("RoleAsyncOperations");
			}
		}

		/// <summary>
		/// 1:N role_parent_role
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("role_parent_role")]
		public System.Collections.Generic.IEnumerable<Role> RoleParentRole
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Role>("role_parent_role", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("RoleParentRole");
				this.SetRelatedEntities<Role>("role_parent_role", null, value);
				this.OnPropertyChanged("RoleParentRole");
			}
		}

		/// <summary>
		/// 1:N role_parent_root_role
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("role_parent_root_role")]
		public System.Collections.Generic.IEnumerable<Role> RoleParentRootRole
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Role>("role_parent_root_role", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("RoleParentRootRole");
				this.SetRelatedEntities<Role>("role_parent_root_role", null, value);
				this.OnPropertyChanged("RoleParentRootRole");
			}
		}

		#endregion

		#region Options
		public static class Options
		{
			    public struct ComponentState
                {
					public const int Published = 0;
					public const int Unpublished = 1;
					public const int Deleted = 2;
					public const int DeletedUnpublished = 3;
                }
			    public struct IsInherited
                {
					public const int TeamPrivilegesOnly = 0;
					public const int DirectUser_Basic_AccessLevelAndTeamPrivileges = 1;
                }
                public struct IsManaged
                {
                    public const bool Unmanaged = false;
                    public const bool Managed = true;
                }
		}
		#endregion

		#region LogicalNames
		public static class LogicalNames
		{
				public const string RoleId = "roleid";
				public const string BusinessUnitId = "businessunitid";
				public const string CanBeDeleted = "canbedeleted";
				public const string ComponentState = "componentstate";
				public const string CreatedBy = "createdby";
				public const string CreatedOn = "createdon";
				public const string CreatedOnBehalfBy = "createdonbehalfby";
				public const string ImportSequenceNumber = "importsequencenumber";
				public const string IsCustomizable = "iscustomizable";
				public const string IsInherited = "isinherited";
				public const string IsManaged = "ismanaged";
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
				public const string VersionNumber = "versionnumber";
		}
		#endregion

		#region AlternateKeys
		public static class AlternateKeys
		{
				public const string ParentRootRoleIdBusinessUnitLookupKey = "parentrootroleid_businessunitid";
				public const string RoleTemplateIdBusinessUnitLookupKey = "roletemplateid_businessunitid";
		}
		#endregion

		#region Relations
        public static class Relations
        {
            public static class OneToMany
            {
				public const string LkMsdynRoleid = "lk_msdyn_roleid";
				public const string MsdynRoleMsdynPersonasecurityrolemapping = "msdyn_role_msdyn_personasecurityrolemapping";
				public const string RoleAsyncOperations = "Role_AsyncOperations";
				public const string RoleBulkDeleteFailures = "Role_BulkDeleteFailures";
				public const string RoleParentRole = "role_parent_role";
				public const string RoleParentRootRole = "role_parent_root_role";
				public const string RoleSyncErrors = "Role_SyncErrors";
				public const string UserentityinstancedataRole = "userentityinstancedata_role";
            }

            public static class ManyToOne
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

            public static class ManyToMany
            {
				public const string Applicationuserrole = "applicationuserrole";
				public const string AppmodulerolesAssociation = "appmoduleroles_association";
				public const string RoleprivilegesAssociation = "roleprivileges_association";
				public const string SystemuserrolesAssociation = "systemuserroles_association";
				public const string TeamrolesAssociation = "teamroles_association";
            }
        }

        #endregion

		#region Methods

        public static Role Retrieve(IOrganizationService service, Guid id)
        {
            return Retrieve(service,id, new ColumnSet(true));
        }

        public static Role Retrieve(IOrganizationService service, Guid id, ColumnSet columnSet)
        {
            return service.Retrieve("role", id, columnSet).ToEntity<Role>();
        }

        public Role GetChangedEntity()
        {
            if (_trackChanges)
            {
                var attr = new AttributeCollection();
                foreach (var attrName in _changedProperties.Value.Select(changedProperty => ((AttributeLogicalNameAttribute) GetType().GetProperty(changedProperty)!.GetCustomAttribute(typeof (AttributeLogicalNameAttribute))!).LogicalName).Where(attrName => Contains(attrName)))
                {
                    attr.Add(attrName,this[attrName]);
                }
                return new  Role(Id) {Attributes = attr };
            }
            return this;
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
