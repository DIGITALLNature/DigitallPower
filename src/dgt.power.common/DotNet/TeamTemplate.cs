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
	/// Team template for an entity enabled for automatically created access teams.
	/// </summary>
    [DataContract]
    [EntityLogicalName("teamtemplate")]
    [System.CodeDom.Compiler.GeneratedCode("dgtp", "2026")]
    [ExcludeFromCodeCoverage]
    public partial class TeamTemplate : Entity, INotifyPropertyChanging, INotifyPropertyChanged
    {
        #region ctor
        [DebuggerNonUserCode]
        public TeamTemplate() : this(false)
        {
        }
        [DebuggerNonUserCode]
        public TeamTemplate(bool trackChanges = false) : base(EntityLogicalName)
        {
            _trackChanges = trackChanges;
        }
        [DebuggerNonUserCode]
        public TeamTemplate(Guid id, bool trackChanges = false) : base(EntityLogicalName, id)
        {
            _trackChanges = trackChanges;
        }
        [DebuggerNonUserCode]
        public TeamTemplate(KeyAttributeCollection keyAttributes, bool trackChanges = false) : base(EntityLogicalName, keyAttributes)
        {
            _trackChanges = trackChanges;
        }
        [DebuggerNonUserCode]
        public TeamTemplate(string keyName, object keyValue, bool trackChanges = false) : base(EntityLogicalName, keyName, keyValue)
        {
            _trackChanges = trackChanges;
        }
        #endregion

        #region fields
        private readonly bool _trackChanges;
        private readonly Lazy<HashSet<string>> _changedProperties = new();
        #endregion

        #region consts
        public const string EntityLogicalName = "teamtemplate";
        public const string PrimaryNameAttribute = "teamtemplatename";
        public const int EntityTypeCode = 92;
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
        [AttributeLogicalName("teamtemplateid")]
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
                TeamTemplateId = value;
            }
        }

        /// <summary>
		/// Unique identifier of the team template.
		/// </summary>
        [AttributeLogicalName("teamtemplateid")]
        public Guid? TeamTemplateId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<Guid?>("teamtemplateid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("teamtemplateid", value);
                base.Id = value.HasValue ? value.Value : Guid.Empty;
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// For internal use only.
		/// </summary>
        [AttributeLogicalName("componentidunique")]
        public Guid? ComponentIdUnique
        {
            [DebuggerNonUserCode]
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
            [DebuggerNonUserCode]
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
            [DebuggerNonUserCode]
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
            [DebuggerNonUserCode]
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
            [DebuggerNonUserCode]
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
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("defaultaccessrightsmask");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("defaultaccessrightsmask", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Type additional information that describes the team.
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
		/// For internal use only.
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
		/// Information about whether this team template is user-defined or system-defined.
		/// </summary>
        [AttributeLogicalName("issystem")]
        public bool? IsSystem
        {
            [DebuggerNonUserCode]
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
            [DebuggerNonUserCode]
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
            [DebuggerNonUserCode]
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
            [DebuggerNonUserCode]
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
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("objecttypecode");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("objecttypecode", value);
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
		/// Type the name of the team template.
		/// </summary>
        [AttributeLogicalName("teamtemplatename")]
        public string? TeamTemplateName
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("teamtemplatename");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("teamtemplatename", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Version number for team template.
		/// </summary>
        [AttributeLogicalName("versionnumber")]
        public long? Versionnumber
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
        /// 1:N teamtemplate_Teams
        /// </summary>
        [RelationshipSchemaName("teamtemplate_Teams")]
        public IEnumerable<Team> TeamtemplateTeams
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<Team>("teamtemplate_Teams", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("teamtemplate_Teams", null, value);
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

        public static TeamTemplate Retrieve(IOrganizationService service, Guid id)
        {
            return Retrieve(service, id, new ColumnSet(true));
        }

        public static TeamTemplate Retrieve(IOrganizationService service, Guid id, ColumnSet columnSet)
        {
            return service.Retrieve("teamtemplate", id, columnSet).ToEntity<TeamTemplate>();
        }

        public TeamTemplate GetChangedEntity()
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
            return new TeamTemplate(Id) { Attributes = attr };
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
