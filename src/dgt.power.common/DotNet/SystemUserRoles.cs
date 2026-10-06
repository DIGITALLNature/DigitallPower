using System.Diagnostics.CodeAnalysis;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System.Runtime.Serialization;

// ReSharper disable All
namespace dgt.power.dataverse
{
    /// <inheritdoc cref="Microsoft.Xrm.Sdk.Entity" />
    
    [EntityLogicalName("systemuserroles")]
    [System.CodeDom.Compiler.GeneratedCode("dgtp", "2026")]
    [ExcludeFromCodeCoverage]
    [SuppressMessage("Design", "CA1034")]
    [SuppressMessage("Performance", "CA1815")]
    public partial class SystemUserRoles : Entity
    {
        #region ctor
        public SystemUserRoles() : base(EntityLogicalName) { }

        public SystemUserRoles(Guid id) : base(EntityLogicalName, id) { }

        public SystemUserRoles(KeyAttributeCollection keyAttributes) : base(EntityLogicalName, keyAttributes) { }

        public SystemUserRoles(string keyName, object keyValue) : base(EntityLogicalName, keyName, keyValue) { }
        #endregion

        #region consts
        public const string EntityLogicalName = "systemuserroles";
        public const int EntityTypeCode = 15;
        #endregion

        #region Attributes
        [AttributeLogicalName("systemuserroleid")]
        [IgnoreDataMember]
        public new Guid Id
        {
            get
            {
                return base.Id;
            }
            set
            {
                SystemUserRoleId = value;
            }
        }

        /// <summary>
		/// For internal use only.
		/// </summary>
        [AttributeLogicalName("systemuserroleid")]
        public Guid? SystemUserRoleId
        {
            get
            {
                return GetAttributeValue<Guid?>("systemuserroleid");
            }
            set
            {
                SetAttributeValue("systemuserroleid", value);
                base.Id = value.HasValue ? value.Value : Guid.Empty;
            }
        }

        
        [AttributeLogicalName("roleid")]
        public Guid? RoleId
        {
            get
            {
                return GetAttributeValue<Guid?>("roleid");
            }
        }

        
        [AttributeLogicalName("systemuserid")]
        public Guid? SystemUserId
        {
            get
            {
                return GetAttributeValue<Guid?>("systemuserid");
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
        #endregion

        #region Options
        public static partial class Options
        {
        }
        #endregion

        #region LogicalNames
        public static partial class LogicalNames
        {
            public const string SystemUserRoleId = "systemuserroleid";
            public const string RoleId = "roleid";
            public const string SystemUserId = "systemuserid";
            public const string VersionNumber = "versionnumber";
        }
        #endregion

        #region Relations
        public static partial class Relations
        {
            public static class OneToMany
            {
            }

            public static partial class ManyToOne
            {
            }

            public static partial class ManyToMany
            {
                public const string SystemuserrolesAssociation = "systemuserroles_association";
            }
        }
        #endregion

        #region Methods
        #endregion
    }

    #region Context
    public partial class DataContext
    {
        public IQueryable<SystemUserRoles> SystemUserRolesSet
        {
            get
            {
                return CreateQuery<SystemUserRoles>();
            }
        }
    }
    #endregion
}
