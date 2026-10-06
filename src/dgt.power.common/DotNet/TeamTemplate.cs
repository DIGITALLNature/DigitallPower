using System.Diagnostics.CodeAnalysis;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System.Runtime.Serialization;

// ReSharper disable All
namespace dgt.power.dataverse
{
    /// <inheritdoc cref="Microsoft.Xrm.Sdk.Entity" />
    /// <summary>
	/// Team template for an entity enabled for automatically created access teams.
	/// </summary>
    [EntityLogicalName("teamtemplate")]
    [System.CodeDom.Compiler.GeneratedCode("dgtp", "2026")]
    [ExcludeFromCodeCoverage]
    [SuppressMessage("Design", "CA1034")]
    [SuppressMessage("Performance", "CA1815")]
    public partial class TeamTemplate : Entity
    {
        #region ctor
        public TeamTemplate() : base(EntityLogicalName) { }

        public TeamTemplate(Guid id) : base(EntityLogicalName, id) { }

        public TeamTemplate(KeyAttributeCollection keyAttributes) : base(EntityLogicalName, keyAttributes) { }

        public TeamTemplate(string keyName, object keyValue) : base(EntityLogicalName, keyName, keyValue) { }
        #endregion

        #region consts
        public const string EntityLogicalName = "teamtemplate";
        public const string PrimaryNameAttribute = "teamtemplatename";
        public const int EntityTypeCode = 92;
        #endregion

        #region Attributes
        [AttributeLogicalName("teamtemplateid")]
        [IgnoreDataMember]
        public new Guid Id
        {
            get
            {
                return base.Id;
            }
            set
            {
                TeamTemplateId = value;
            }
        }

        /// <summary>
		/// Unique identifier of the team template.
		/// </summary>
        [AttributeLogicalName("teamtemplateid")]
        public Guid? TeamTemplateId
        {
            get
            {
                return GetAttributeValue<Guid?>("teamtemplateid");
            }
            set
            {
                SetAttributeValue("teamtemplateid", value);
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

        /// <summary>
		/// Unique identifier of the user who created the team template.
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
		/// Date and time when the team template was created.
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
		/// Unique identifier of the delegate user who created the team template.
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
		/// Default access rights mask for the access teams associated with entity instances.
		/// </summary>
        [AttributeLogicalName("defaultaccessrightsmask")]
        public int? DefaultAccessRightsMask
        {
            get
            {
                return GetAttributeValue<int?>("defaultaccessrightsmask");
            }
            set
            {
                SetAttributeValue("defaultaccessrightsmask", value);
            }
        }

        /// <summary>
		/// Type additional information that describes the team.
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
		/// Information about whether this team template is user-defined or system-defined.
		/// </summary>
        [AttributeLogicalName("issystem")]
        public bool? IsSystem
        {
            get
            {
                return GetAttributeValue<bool?>("issystem");
            }
        }

        /// <summary>
		/// Unique identifier of the user who last modified the team template.
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
		/// Date and time when the team template was last modified.
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
		/// Unique identifier of the delegate user who modified the team template.
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
		/// Object type code of entity which is enabled for access teams
		/// </summary>
        [AttributeLogicalName("objecttypecode")]
        public int? ObjectTypeCode
        {
            get
            {
                return GetAttributeValue<int?>("objecttypecode");
            }
            set
            {
                SetAttributeValue("objecttypecode", value);
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
		/// Type the name of the team template.
		/// </summary>
        [AttributeLogicalName("teamtemplatename")]
        public string? TeamTemplateName
        {
            get
            {
                return GetAttributeValue<string?>("teamtemplatename");
            }
            set
            {
                SetAttributeValue("teamtemplatename", value);
            }
        }

        /// <summary>
		/// Version number for team template.
		/// </summary>
        [AttributeLogicalName("versionnumber")]
        public long? Versionnumber
        {
            get
            {
                return GetAttributeValue<long?>("versionnumber");
            }
        }
        #endregion

        #region NavigationProperties

        /// <summary>
        /// 1:N teamtemplate_Teams
        /// </summary>
        [RelationshipSchemaName("teamtemplate_Teams")]
        public IEnumerable<Team> TeamtemplateTeams
        {
            get
            {
                return GetRelatedEntities<Team>("teamtemplate_Teams", null);
            }
            set
            {
                SetRelatedEntities("teamtemplate_Teams", null, value);
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
            public struct IsSystem
            {
                public const bool No = false;
                public const bool Yes = true;
            }
        }
        #endregion

        #region LogicalNames
        public static partial class LogicalNames
        {
            public const string TeamTemplateId = "teamtemplateid";
            public const string ComponentIdUnique = "componentidunique";
            public const string ComponentState = "componentstate";
            public const string CreatedBy = "createdby";
            public const string CreatedOn = "createdon";
            public const string CreatedOnBehalfBy = "createdonbehalfby";
            public const string DefaultAccessRightsMask = "defaultaccessrightsmask";
            public const string Description = "description";
            public const string IsCustomizable = "iscustomizable";
            public const string IsManaged = "ismanaged";
            public const string IsSystem = "issystem";
            public const string ModifiedBy = "modifiedby";
            public const string ModifiedOn = "modifiedon";
            public const string ModifiedOnBehalfBy = "modifiedonbehalfby";
            public const string ObjectTypeCode = "objecttypecode";
            public const string OverwriteTime = "overwritetime";
            public const string SolutionId = "solutionid";
            public const string TeamTemplateName = "teamtemplatename";
            public const string Versionnumber = "versionnumber";
        }
        #endregion

        #region Relations
        public static partial class Relations
        {
            public static class OneToMany
            {
                public const string TeamTemplateSyncErrors = "TeamTemplate_SyncErrors";
                public const string TeamtemplateTeams = "teamtemplate_Teams";
            }

            public static partial class ManyToOne
            {
                public const string LkTeamtemplateCreatedby = "lk_teamtemplate_createdby";
                public const string LkTeamtemplateCreatedonbehalfby = "lk_teamtemplate_createdonbehalfby";
                public const string LkTeamtemplateModifiedby = "lk_teamtemplate_modifiedby";
                public const string LkTeamtemplateModifiedonbehalfby = "lk_teamtemplate_modifiedonbehalfby";
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
        public IQueryable<TeamTemplate> TeamTemplateSet
        {
            get
            {
                return CreateQuery<TeamTemplate>();
            }
        }
    }
    #endregion
}
