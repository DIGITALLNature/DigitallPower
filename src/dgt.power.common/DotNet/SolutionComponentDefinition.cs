using System.Diagnostics.CodeAnalysis;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System.Runtime.Serialization;

// ReSharper disable All
namespace dgt.power.dataverse
{
    /// <inheritdoc cref="Microsoft.Xrm.Sdk.Entity" />
    /// <summary>
	/// Contains all the information required to process a solution aware entity
	/// </summary>
    [EntityLogicalName("solutioncomponentdefinition")]
    [System.CodeDom.Compiler.GeneratedCode("dgtp", "2026")]
    [ExcludeFromCodeCoverage]
    [SuppressMessage("Design", "CA1034")]
    [SuppressMessage("Performance", "CA1815")]
    public partial class SolutionComponentDefinition : Entity
    {
        #region ctor
        public SolutionComponentDefinition() : base(EntityLogicalName) { }

        public SolutionComponentDefinition(Guid id) : base(EntityLogicalName, id) { }

        public SolutionComponentDefinition(KeyAttributeCollection keyAttributes) : base(EntityLogicalName, keyAttributes) { }

        public SolutionComponentDefinition(string keyName, object keyValue) : base(EntityLogicalName, keyName, keyValue) { }
        #endregion

        #region consts
        public const string EntityLogicalName = "solutioncomponentdefinition";
        public const string PrimaryNameAttribute = "name";
        public const int EntityTypeCode = 7104;
        #endregion

        #region Attributes
        [AttributeLogicalName("solutioncomponentdefinitionid")]
        [IgnoreDataMember]
        public new Guid Id
        {
            get
            {
                return base.Id;
            }
            set
            {
                SolutionComponentDefinitionId = value;
            }
        }

        /// <summary>
		/// Unique identifier of the solution component definition
		/// </summary>
        [AttributeLogicalName("solutioncomponentdefinitionid")]
        public Guid? SolutionComponentDefinitionId
        {
            get
            {
                return GetAttributeValue<Guid?>("solutioncomponentdefinitionid");
            }
            set
            {
                SetAttributeValue("solutioncomponentdefinitionid", value);
                base.Id = value.HasValue ? value.Value : Guid.Empty;
            }
        }

        /// <summary>
		/// For internal use only.
		/// </summary>
        [AttributeLogicalName("solutioncomponentdefinitionidunique")]
        public Guid? SolutionComponentDefinitionIdUnique
        {
            get
            {
                return GetAttributeValue<Guid?>("solutioncomponentdefinitionidunique");
            }
        }

        /// <summary>
		/// Boolean identifier for using deleting base layers.
		/// </summary>
        [AttributeLogicalName("allowdeletebasesolutionrowandfakedelete")]
        public bool? AllowDeleteBaseSolutionRowAndFakeDelete
        {
            get
            {
                return GetAttributeValue<bool?>("allowdeletebasesolutionrowandfakedelete");
            }
            set
            {
                SetAttributeValue("allowdeletebasesolutionrowandfakedelete", value);
            }
        }

        /// <summary>
		/// Whether this component allows Overwrite Customizations when update managed solution
		/// </summary>
        [AttributeLogicalName("allowoverwritecustomizations")]
        public bool? AllowOverwriteCustomizations
        {
            get
            {
                return GetAttributeValue<bool?>("allowoverwritecustomizations");
            }
            set
            {
                SetAttributeValue("allowoverwritecustomizations", value);
            }
        }

        /// <summary>
		/// Boolean identifier for a row that is marked as logically deleted in the Active solution and should be re-created back
		/// </summary>
        [AttributeLogicalName("allowrecreateforlogicallydeletedrow")]
        public bool? AllowRecreateForLogicallyDeletedRow
        {
            get
            {
                return GetAttributeValue<bool?>("allowrecreateforlogicallydeletedrow");
            }
            set
            {
                SetAttributeValue("allowrecreateforlogicallydeletedrow", value);
            }
        }

        /// <summary>
		/// Flag used to indicate whether this component always removes active customizations on uninstall
		/// </summary>
        [AttributeLogicalName("alwaysremoveactivecustomizationsonuninstall")]
        public bool? AlwaysRemoveActiveCustomizationsOnUninstall
        {
            get
            {
                return GetAttributeValue<bool?>("alwaysremoveactivecustomizationsonuninstall");
            }
            set
            {
                SetAttributeValue("alwaysremoveactivecustomizationsonuninstall", value);
            }
        }

        /// <summary>
		/// Flag indicating whether the subcomponent can be added directly to the SolutionComponents table
		/// </summary>
        [AttributeLogicalName("canbeaddedtosolutioncomponents")]
        public bool? CanBeAddedToSolutionComponents
        {
            get
            {
                return GetAttributeValue<bool?>("canbeaddedtosolutioncomponents");
            }
            set
            {
                SetAttributeValue("canbeaddedtosolutioncomponents", value);
            }
        }

        /// <summary>
		/// Whether this component is hidden using an IsHidden managed property
		/// </summary>
        [AttributeLogicalName("canbehidden")]
        public bool? CanBeHidden
        {
            get
            {
                return GetAttributeValue<bool?>("canbehidden");
            }
            set
            {
                SetAttributeValue("canbehidden", value);
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
		/// Path to component's XML node
		/// </summary>
        [AttributeLogicalName("componentxpath")]
        public string? ComponentXPath
        {
            get
            {
                return GetAttributeValue<string?>("componentxpath");
            }
            set
            {
                SetAttributeValue("componentxpath", value);
            }
        }

        /// <summary>
		/// Flag that indicates whether this component uses its descendent as its viewable component
		/// </summary>
        [AttributeLogicalName("descendentisviewablecomponent")]
        public bool? DescendentIsViewableComponent
        {
            get
            {
                return GetAttributeValue<bool?>("descendentisviewablecomponent");
            }
            set
            {
                SetAttributeValue("descendentisviewablecomponent", value);
            }
        }

        /// <summary>
		/// Group Parent Component Attribute Name
		/// </summary>
        [AttributeLogicalName("groupparentcomponentattributename")]
        public string? GroupParentComponentAttributeName
        {
            get
            {
                return GetAttributeValue<string?>("groupparentcomponentattributename");
            }
            set
            {
                SetAttributeValue("groupparentcomponentattributename", value);
            }
        }

        /// <summary>
		/// Group Parent Component Type
		/// </summary>
        [AttributeLogicalName("groupparentcomponenttype")]
        public int? GroupParentComponentType
        {
            get
            {
                return GetAttributeValue<int?>("groupparentcomponenttype");
            }
            set
            {
                SetAttributeValue("groupparentcomponenttype", value);
            }
        }

        /// <summary>
		/// Boolean that indicates if the component has a renamable attribute
		/// </summary>
        [AttributeLogicalName("hasisrenameableattribute")]
        public bool? HasIsRenameableAttribute
        {
            get
            {
                return GetAttributeValue<bool?>("hasisrenameableattribute");
            }
            set
            {
                SetAttributeValue("hasisrenameableattribute", value);
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
		/// Version in which the component is introduced.
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
		/// Is dependency disabled for the component
		/// </summary>
        [AttributeLogicalName("isdependencydisabled")]
        public bool? IsDependencyDisabled
        {
            get
            {
                return GetAttributeValue<bool?>("isdependencydisabled");
            }
            set
            {
                SetAttributeValue("isdependencydisabled", value);
            }
        }

        /// <summary>
		/// Boolean that indicates if the component has user interface enabled
		/// </summary>
        [AttributeLogicalName("isdisplayable")]
        public bool? IsDisplayable
        {
            get
            {
                return GetAttributeValue<bool?>("isdisplayable");
            }
            set
            {
                SetAttributeValue("isdisplayable", value);
            }
        }

        /// <summary>
		/// Boolean that indicates if the component is managed
		/// </summary>
        [AttributeLogicalName("ismanaged")]
        public bool? IsManaged
        {
            get
            {
                return GetAttributeValue<bool?>("ismanaged");
            }
            set
            {
                SetAttributeValue("ismanaged", value);
            }
        }

        /// <summary>
		/// Whether this component is either a mergeable component, or part of a mergeable component
		/// </summary>
        [AttributeLogicalName("ismergeable")]
        public bool? IsMergeable
        {
            get
            {
                return GetAttributeValue<bool?>("ismergeable");
            }
            set
            {
                SetAttributeValue("ismergeable", value);
            }
        }

        /// <summary>
		/// Boolean identifier for metadata components
		/// </summary>
        [AttributeLogicalName("ismetadata")]
        public bool? IsMetadata
        {
            get
            {
                return GetAttributeValue<bool?>("ismetadata");
            }
            set
            {
                SetAttributeValue("ismetadata", value);
            }
        }

        /// <summary>
		/// Whether this component is viewable in the SDK and UI
		/// </summary>
        [AttributeLogicalName("isviewable")]
        public bool? IsViewable
        {
            get
            {
                return GetAttributeValue<bool?>("isviewable");
            }
            set
            {
                SetAttributeValue("isviewable", value);
            }
        }

        /// <summary>
		/// Label Type Code
		/// </summary>
        [AttributeLogicalName("labeltypecode")]
        public int? LabelTypeCode
        {
            get
            {
                return GetAttributeValue<int?>("labeltypecode");
            }
            set
            {
                SetAttributeValue("labeltypecode", value);
            }
        }

        /// <summary>
		/// Name
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
		/// Object Type Code
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
		/// The attribute name of the parent attribute
		/// </summary>
        [AttributeLogicalName("parentattributename")]
        public string? ParentAttributeName
        {
            get
            {
                return GetAttributeValue<string?>("parentattributename");
            }
            set
            {
                SetAttributeValue("parentattributename", value);
            }
        }

        /// <summary>
		/// Component Entity Logical Name
		/// </summary>
        [AttributeLogicalName("primaryentityname")]
        public string? PrimaryEntityName
        {
            get
            {
                return GetAttributeValue<string?>("primaryentityname");
            }
            set
            {
                SetAttributeValue("primaryentityname", value);
            }
        }

        /// <summary>
		/// Remove Active Customizations Behavior.
		/// </summary>
        [AttributeLogicalName("removeactivecustomizationsbehavior")]
        public OptionSetValue? RemoveActiveCustomizationsBehavior
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("removeactivecustomizationsbehavior");
            }
            set
            {
                SetAttributeValue("removeactivecustomizationsbehavior", value);
            }
        }

        /// <summary>
		/// Root Solution Component Type Name
		/// </summary>
        [AttributeLogicalName("rootattributename")]
        public string? RootAttributeName
        {
            get
            {
                return GetAttributeValue<string?>("rootattributename");
            }
            set
            {
                SetAttributeValue("rootattributename", value);
            }
        }

        /// <summary>
		/// Root Solution Component Type
		/// </summary>
        [AttributeLogicalName("rootcomponent")]
        public int? RootComponent
        {
            get
            {
                return GetAttributeValue<int?>("rootcomponent");
            }
            set
            {
                SetAttributeValue("rootcomponent", value);
            }
        }

        /// <summary>
		/// Solution Component Type
		/// </summary>
        [AttributeLogicalName("solutioncomponenttype")]
        public int? SolutionComponentType
        {
            get
            {
                return GetAttributeValue<int?>("solutioncomponenttype");
            }
            set
            {
                SetAttributeValue("solutioncomponenttype", value);
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
		/// Boolean identifier for forcing delete for solution update.
		/// </summary>
        [AttributeLogicalName("useforcedeleteforsolutionupdate")]
        public bool? UseForceDeleteForSolutionUpdate
        {
            get
            {
                return GetAttributeValue<bool?>("useforcedeleteforsolutionupdate");
            }
            set
            {
                SetAttributeValue("useforcedeleteforsolutionupdate", value);
            }
        }

        /// <summary>
		/// Boolean identifier for always forcing update.
		/// </summary>
        [AttributeLogicalName("useforceupdatealways")]
        public bool? UseForceUpdateAlways
        {
            get
            {
                return GetAttributeValue<bool?>("useforceupdatealways");
            }
            set
            {
                SetAttributeValue("useforceupdatealways", value);
            }
        }

        /// <summary>
		/// Boolean identifier for using sentine rows.
		/// </summary>
        [AttributeLogicalName("usesentinelrowinbasesolution")]
        public bool? UseSentinelRowInBaseSolution
        {
            get
            {
                return GetAttributeValue<bool?>("usesentinelrowinbasesolution");
            }
            set
            {
                SetAttributeValue("usesentinelrowinbasesolution", value);
            }
        }

        /// <summary>
		/// The component type of the viewable descendent
		/// </summary>
        [AttributeLogicalName("viewabledescendentcomponenttype")]
        public int? ViewableDescendentComponentType
        {
            get
            {
                return GetAttributeValue<int?>("viewabledescendentcomponenttype");
            }
            set
            {
                SetAttributeValue("viewabledescendentcomponenttype", value);
            }
        }
        #endregion

        #region NavigationProperties
        #endregion

        #region Options
        public static partial class Options
        {
            public struct AllowDeleteBaseSolutionRowAndFakeDelete
            {
                public const bool False = false;
                public const bool True = true;
            }
            public struct AllowOverwriteCustomizations
            {
                public const bool False = false;
                public const bool True = true;
            }
            public struct AllowRecreateForLogicallyDeletedRow
            {
                public const bool False = false;
                public const bool True = true;
            }
            public struct AlwaysRemoveActiveCustomizationsOnUninstall
            {
                public const bool False = false;
                public const bool True = true;
            }
            public struct CanBeAddedToSolutionComponents
            {
                public const bool False = false;
                public const bool True = true;
            }
            public struct CanBeHidden
            {
                public const bool False = false;
                public const bool True = true;
            }
            public struct ComponentState
            {
                public const int Published = 0;
                public const int Unpublished = 1;
                public const int Deleted = 2;
                public const int DeletedUnpublished = 3;
            }
            public struct DescendentIsViewableComponent
            {
                public const bool False = false;
                public const bool True = true;
            }
            public struct HasIsRenameableAttribute
            {
                public const bool False = false;
                public const bool True = true;
            }
            public struct IsDependencyDisabled
            {
                public const bool False = false;
                public const bool True = true;
            }
            public struct IsDisplayable
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsManaged
            {
                public const bool Unmanaged = false;
                public const bool Managed = true;
            }
            public struct IsMergeable
            {
                public const bool False = false;
                public const bool True = true;
            }
            public struct IsMetadata
            {
                public const bool False = false;
                public const bool True = true;
            }
            public struct IsViewable
            {
                public const bool False = false;
                public const bool True = true;
            }
            public struct RemoveActiveCustomizationsBehavior
            {
                public const int None = 0;
                public const int NoCascade = 1;
                public const int Cascade = 2;
            }
            public struct UseForceDeleteForSolutionUpdate
            {
                public const bool False = false;
                public const bool True = true;
            }
            public struct UseForceUpdateAlways
            {
                public const bool False = false;
                public const bool True = true;
            }
            public struct UseSentinelRowInBaseSolution
            {
                public const bool False = false;
                public const bool True = true;
            }
        }
        #endregion

        #region LogicalNames
        public static partial class LogicalNames
        {
            public const string SolutionComponentDefinitionId = "solutioncomponentdefinitionid";
            public const string SolutionComponentDefinitionIdUnique = "solutioncomponentdefinitionidunique";
            public const string AllowDeleteBaseSolutionRowAndFakeDelete = "allowdeletebasesolutionrowandfakedelete";
            public const string AllowOverwriteCustomizations = "allowoverwritecustomizations";
            public const string AllowRecreateForLogicallyDeletedRow = "allowrecreateforlogicallydeletedrow";
            public const string AlwaysRemoveActiveCustomizationsOnUninstall = "alwaysremoveactivecustomizationsonuninstall";
            public const string CanBeAddedToSolutionComponents = "canbeaddedtosolutioncomponents";
            public const string CanBeHidden = "canbehidden";
            public const string ComponentState = "componentstate";
            public const string ComponentXPath = "componentxpath";
            public const string DescendentIsViewableComponent = "descendentisviewablecomponent";
            public const string GroupParentComponentAttributeName = "groupparentcomponentattributename";
            public const string GroupParentComponentType = "groupparentcomponenttype";
            public const string HasIsRenameableAttribute = "hasisrenameableattribute";
            public const string ImportSequenceNumber = "importsequencenumber";
            public const string IntroducedVersion = "introducedversion";
            public const string IsDependencyDisabled = "isdependencydisabled";
            public const string IsDisplayable = "isdisplayable";
            public const string IsManaged = "ismanaged";
            public const string IsMergeable = "ismergeable";
            public const string IsMetadata = "ismetadata";
            public const string IsViewable = "isviewable";
            public const string LabelTypeCode = "labeltypecode";
            public const string Name = "name";
            public const string ObjectTypeCode = "objecttypecode";
            public const string OverriddenCreatedOn = "overriddencreatedon";
            public const string OverwriteTime = "overwritetime";
            public const string ParentAttributeName = "parentattributename";
            public const string PrimaryEntityName = "primaryentityname";
            public const string RemoveActiveCustomizationsBehavior = "removeactivecustomizationsbehavior";
            public const string RootAttributeName = "rootattributename";
            public const string RootComponent = "rootcomponent";
            public const string SolutionComponentType = "solutioncomponenttype";
            public const string SolutionId = "solutionid";
            public const string UseForceDeleteForSolutionUpdate = "useforcedeleteforsolutionupdate";
            public const string UseForceUpdateAlways = "useforceupdatealways";
            public const string UseSentinelRowInBaseSolution = "usesentinelrowinbasesolution";
            public const string ViewableDescendentComponentType = "viewabledescendentcomponenttype";
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
        public IQueryable<SolutionComponentDefinition> SolutionComponentDefinitionSet
        {
            get
            {
                return CreateQuery<SolutionComponentDefinition>();
            }
        }
    }
    #endregion
}
