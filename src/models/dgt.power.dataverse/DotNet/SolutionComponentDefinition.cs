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
	/// Contains all the information required to process a solution aware entity
	/// </summary>
	[DataContractAttribute()]
	[EntityLogicalNameAttribute("solutioncomponentdefinition")]
	[System.CodeDom.Compiler.GeneratedCode("dgtp", "2023")]
    [ExcludeFromCodeCoverage]
	public partial class SolutionComponentDefinition : Entity, INotifyPropertyChanging, INotifyPropertyChanged
    {
	    #region ctor
		[DebuggerNonUserCode]
		public SolutionComponentDefinition() : this(false)
        {
        }

        [DebuggerNonUserCode]
		public SolutionComponentDefinition(bool trackChanges = false) : base(EntityLogicalName)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public SolutionComponentDefinition(Guid id, bool trackChanges = false) : base(EntityLogicalName,id)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public SolutionComponentDefinition(KeyAttributeCollection keyAttributes, bool trackChanges = false) : base(EntityLogicalName,keyAttributes)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public SolutionComponentDefinition(string keyName, object keyValue, bool trackChanges = false) : base(EntityLogicalName, keyName, keyValue)
        {
			_trackChanges = trackChanges;
        }
        #endregion

		#region fields
        private readonly bool _trackChanges;
        private readonly Lazy<HashSet<string>> _changedProperties = new Lazy<HashSet<string>>();
        #endregion

        #region consts
        public const string EntityLogicalName = "solutioncomponentdefinition";
        public const string PrimaryNameAttribute = "name";
        public const int EntityTypeCode = 7104;
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
		[AttributeLogicalNameAttribute("solutioncomponentdefinitionid")]
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
				SolutionComponentDefinitionId = value;
			}
		}

		/// <summary>
		/// Unique identifier of the solution component definition
		/// </summary>
		[AttributeLogicalName("solutioncomponentdefinitionid")]
        public Guid? SolutionComponentDefinitionId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<Guid?>("solutioncomponentdefinitionid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(SolutionComponentDefinitionId));
                SetAttributeValue("solutioncomponentdefinitionid", value);
				if (value.HasValue)
				{
					base.Id = value.Value;
				}
				else
				{
					base.Id = System.Guid.Empty;
				}
                OnPropertyChanged(nameof(SolutionComponentDefinitionId));
            }
        }

		/// <summary>
		/// For internal use only.
		/// </summary>
		[AttributeLogicalName("solutioncomponentdefinitionidunique")]
        public Guid? SolutionComponentDefinitionIdUnique
        {
            [DebuggerNonUserCode]
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
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<bool?>("allowdeletebasesolutionrowandfakedelete");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(AllowDeleteBaseSolutionRowAndFakeDelete));
                SetAttributeValue("allowdeletebasesolutionrowandfakedelete", value);
                OnPropertyChanged(nameof(AllowDeleteBaseSolutionRowAndFakeDelete));
            }
        }

		/// <summary>
		/// Whether this component allows Overwrite Customizations when update managed solution
		/// </summary>
		[AttributeLogicalName("allowoverwritecustomizations")]
        public bool? AllowOverwriteCustomizations
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<bool?>("allowoverwritecustomizations");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(AllowOverwriteCustomizations));
                SetAttributeValue("allowoverwritecustomizations", value);
                OnPropertyChanged(nameof(AllowOverwriteCustomizations));
            }
        }

		/// <summary>
		/// Boolean identifier for a row that is marked as logically deleted in the Active solution and should be re-created back
		/// </summary>
		[AttributeLogicalName("allowrecreateforlogicallydeletedrow")]
        public bool? AllowRecreateForLogicallyDeletedRow
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<bool?>("allowrecreateforlogicallydeletedrow");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(AllowRecreateForLogicallyDeletedRow));
                SetAttributeValue("allowrecreateforlogicallydeletedrow", value);
                OnPropertyChanged(nameof(AllowRecreateForLogicallyDeletedRow));
            }
        }

		/// <summary>
		/// Flag used to indicate whether this component always removes active customizations on uninstall
		/// </summary>
		[AttributeLogicalName("alwaysremoveactivecustomizationsonuninstall")]
        public bool? AlwaysRemoveActiveCustomizationsOnUninstall
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<bool?>("alwaysremoveactivecustomizationsonuninstall");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(AlwaysRemoveActiveCustomizationsOnUninstall));
                SetAttributeValue("alwaysremoveactivecustomizationsonuninstall", value);
                OnPropertyChanged(nameof(AlwaysRemoveActiveCustomizationsOnUninstall));
            }
        }

		/// <summary>
		/// Flag indicating whether the subcomponent can be added directly to the SolutionComponents table
		/// </summary>
		[AttributeLogicalName("canbeaddedtosolutioncomponents")]
        public bool? CanBeAddedToSolutionComponents
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<bool?>("canbeaddedtosolutioncomponents");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(CanBeAddedToSolutionComponents));
                SetAttributeValue("canbeaddedtosolutioncomponents", value);
                OnPropertyChanged(nameof(CanBeAddedToSolutionComponents));
            }
        }

		/// <summary>
		/// Whether this component is hidden using an IsHidden managed property
		/// </summary>
		[AttributeLogicalName("canbehidden")]
        public bool? CanBeHidden
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<bool?>("canbehidden");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(CanBeHidden));
                SetAttributeValue("canbehidden", value);
                OnPropertyChanged(nameof(CanBeHidden));
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
		/// Path to component's XML node
		/// </summary>
		[AttributeLogicalName("componentxpath")]
        public string? ComponentXPath
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("componentxpath");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(ComponentXPath));
                SetAttributeValue("componentxpath", value);
                OnPropertyChanged(nameof(ComponentXPath));
            }
        }

		/// <summary>
		/// Flag that indicates whether this component uses its descendent as its viewable component
		/// </summary>
		[AttributeLogicalName("descendentisviewablecomponent")]
        public bool? DescendentIsViewableComponent
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<bool?>("descendentisviewablecomponent");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(DescendentIsViewableComponent));
                SetAttributeValue("descendentisviewablecomponent", value);
                OnPropertyChanged(nameof(DescendentIsViewableComponent));
            }
        }

		/// <summary>
		/// Group Parent Component Attribute Name
		/// </summary>
		[AttributeLogicalName("groupparentcomponentattributename")]
        public string? GroupParentComponentAttributeName
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("groupparentcomponentattributename");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(GroupParentComponentAttributeName));
                SetAttributeValue("groupparentcomponentattributename", value);
                OnPropertyChanged(nameof(GroupParentComponentAttributeName));
            }
        }

		/// <summary>
		/// Group Parent Component Type
		/// </summary>
		[AttributeLogicalName("groupparentcomponenttype")]
        public int? GroupParentComponentType
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<int?>("groupparentcomponenttype");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(GroupParentComponentType));
                SetAttributeValue("groupparentcomponenttype", value);
                OnPropertyChanged(nameof(GroupParentComponentType));
            }
        }

		/// <summary>
		/// Boolean that indicates if the component has a renamable attribute
		/// </summary>
		[AttributeLogicalName("hasisrenameableattribute")]
        public bool? HasIsRenameableAttribute
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<bool?>("hasisrenameableattribute");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(HasIsRenameableAttribute));
                SetAttributeValue("hasisrenameableattribute", value);
                OnPropertyChanged(nameof(HasIsRenameableAttribute));
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
		/// Version in which the component is introduced.
		/// </summary>
		[AttributeLogicalName("introducedversion")]
        public string? IntroducedVersion
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("introducedversion");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(IntroducedVersion));
                SetAttributeValue("introducedversion", value);
                OnPropertyChanged(nameof(IntroducedVersion));
            }
        }

		/// <summary>
		/// Is dependency disabled for the component
		/// </summary>
		[AttributeLogicalName("isdependencydisabled")]
        public bool? IsDependencyDisabled
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<bool?>("isdependencydisabled");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(IsDependencyDisabled));
                SetAttributeValue("isdependencydisabled", value);
                OnPropertyChanged(nameof(IsDependencyDisabled));
            }
        }

		/// <summary>
		/// Boolean that indicates if the component has user interface enabled
		/// </summary>
		[AttributeLogicalName("isdisplayable")]
        public bool? IsDisplayable
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<bool?>("isdisplayable");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(IsDisplayable));
                SetAttributeValue("isdisplayable", value);
                OnPropertyChanged(nameof(IsDisplayable));
            }
        }

		/// <summary>
		/// Boolean that indicates if the component is managed
		/// </summary>
		[AttributeLogicalName("ismanaged")]
        public bool? IsManaged
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<bool?>("ismanaged");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(IsManaged));
                SetAttributeValue("ismanaged", value);
                OnPropertyChanged(nameof(IsManaged));
            }
        }

		/// <summary>
		/// Whether this component is either a mergeable component, or part of a mergeable component
		/// </summary>
		[AttributeLogicalName("ismergeable")]
        public bool? IsMergeable
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<bool?>("ismergeable");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(IsMergeable));
                SetAttributeValue("ismergeable", value);
                OnPropertyChanged(nameof(IsMergeable));
            }
        }

		/// <summary>
		/// Boolean identifier for metadata components
		/// </summary>
		[AttributeLogicalName("ismetadata")]
        public bool? IsMetadata
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<bool?>("ismetadata");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(IsMetadata));
                SetAttributeValue("ismetadata", value);
                OnPropertyChanged(nameof(IsMetadata));
            }
        }

		/// <summary>
		/// Whether this component is viewable in the SDK and UI
		/// </summary>
		[AttributeLogicalName("isviewable")]
        public bool? IsViewable
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<bool?>("isviewable");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(IsViewable));
                SetAttributeValue("isviewable", value);
                OnPropertyChanged(nameof(IsViewable));
            }
        }

		/// <summary>
		/// Label Type Code
		/// </summary>
		[AttributeLogicalName("labeltypecode")]
        public int? LabelTypeCode
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<int?>("labeltypecode");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(LabelTypeCode));
                SetAttributeValue("labeltypecode", value);
                OnPropertyChanged(nameof(LabelTypeCode));
            }
        }

		/// <summary>
		/// Name
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
		/// Object Type Code
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
                OnPropertyChanging(nameof(ObjectTypeCode));
                SetAttributeValue("objecttypecode", value);
                OnPropertyChanged(nameof(ObjectTypeCode));
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
		/// The attribute name of the parent attribute
		/// </summary>
		[AttributeLogicalName("parentattributename")]
        public string? ParentAttributeName
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("parentattributename");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(ParentAttributeName));
                SetAttributeValue("parentattributename", value);
                OnPropertyChanged(nameof(ParentAttributeName));
            }
        }

		/// <summary>
		/// Component Entity Logical Name
		/// </summary>
		[AttributeLogicalName("primaryentityname")]
        public string? PrimaryEntityName
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("primaryentityname");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(PrimaryEntityName));
                SetAttributeValue("primaryentityname", value);
                OnPropertyChanged(nameof(PrimaryEntityName));
            }
        }

		/// <summary>
		/// Remove Active Customizations Behavior.
		/// </summary>
		[AttributeLogicalName("removeactivecustomizationsbehavior")]
        public OptionSetValue? RemoveActiveCustomizationsBehavior
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<OptionSetValue?>("removeactivecustomizationsbehavior");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(RemoveActiveCustomizationsBehavior));
                SetAttributeValue("removeactivecustomizationsbehavior", value);
                OnPropertyChanged(nameof(RemoveActiveCustomizationsBehavior));
            }
        }

		/// <summary>
		/// Root Solution Component Type Name
		/// </summary>
		[AttributeLogicalName("rootattributename")]
        public string? RootAttributeName
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("rootattributename");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(RootAttributeName));
                SetAttributeValue("rootattributename", value);
                OnPropertyChanged(nameof(RootAttributeName));
            }
        }

		/// <summary>
		/// Root Solution Component Type
		/// </summary>
		[AttributeLogicalName("rootcomponent")]
        public int? RootComponent
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<int?>("rootcomponent");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(RootComponent));
                SetAttributeValue("rootcomponent", value);
                OnPropertyChanged(nameof(RootComponent));
            }
        }

		/// <summary>
		/// Solution Component Type
		/// </summary>
		[AttributeLogicalName("solutioncomponenttype")]
        public int? SolutionComponentType
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<int?>("solutioncomponenttype");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(SolutionComponentType));
                SetAttributeValue("solutioncomponenttype", value);
                OnPropertyChanged(nameof(SolutionComponentType));
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
		/// Boolean identifier for forcing delete for solution update.
		/// </summary>
		[AttributeLogicalName("useforcedeleteforsolutionupdate")]
        public bool? UseForceDeleteForSolutionUpdate
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<bool?>("useforcedeleteforsolutionupdate");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(UseForceDeleteForSolutionUpdate));
                SetAttributeValue("useforcedeleteforsolutionupdate", value);
                OnPropertyChanged(nameof(UseForceDeleteForSolutionUpdate));
            }
        }

		/// <summary>
		/// Boolean identifier for always forcing update.
		/// </summary>
		[AttributeLogicalName("useforceupdatealways")]
        public bool? UseForceUpdateAlways
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<bool?>("useforceupdatealways");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(UseForceUpdateAlways));
                SetAttributeValue("useforceupdatealways", value);
                OnPropertyChanged(nameof(UseForceUpdateAlways));
            }
        }

		/// <summary>
		/// Boolean identifier for using sentine rows.
		/// </summary>
		[AttributeLogicalName("usesentinelrowinbasesolution")]
        public bool? UseSentinelRowInBaseSolution
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<bool?>("usesentinelrowinbasesolution");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(UseSentinelRowInBaseSolution));
                SetAttributeValue("usesentinelrowinbasesolution", value);
                OnPropertyChanged(nameof(UseSentinelRowInBaseSolution));
            }
        }

		/// <summary>
		/// The component type of the viewable descendent
		/// </summary>
		[AttributeLogicalName("viewabledescendentcomponenttype")]
        public int? ViewableDescendentComponentType
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<int?>("viewabledescendentcomponenttype");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(ViewableDescendentComponentType));
                SetAttributeValue("viewabledescendentcomponenttype", value);
                OnPropertyChanged(nameof(ViewableDescendentComponentType));
            }
        }


		#endregion

		#region NavigationProperties
		#endregion

		#region Options
		public static class Options
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
		public static class LogicalNames
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
        public static class Relations
        {
            public static class OneToMany
            {
            }

            public static class ManyToOne
            {
            }

            public static class ManyToMany
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
        public static SolutionComponentDefinition Retrieve(IOrganizationService service, Guid id)
        {
            return Retrieve(service,id, new ColumnSet(true));
        }

        public static SolutionComponentDefinition Retrieve(IOrganizationService service, Guid id, ColumnSet columnSet)
        {
            return service.Retrieve("solutioncomponentdefinition", id, columnSet).ToEntity<SolutionComponentDefinition>();
        }

        public SolutionComponentDefinition GetChangedEntity()
        {
            if (_trackChanges)
            {
                var attr = new AttributeCollection();
                foreach (var attrName in _changedProperties.Value.Select(changedProperty => ((AttributeLogicalNameAttribute) GetType().GetProperty(changedProperty)!.GetCustomAttribute(typeof (AttributeLogicalNameAttribute))!).LogicalName).Where(attrName => Contains(attrName)))
                {
                    attr.Add(attrName,this[attrName]);
                }
                return new  SolutionComponentDefinition(Id) {Attributes = attr };
            }
            return this;
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
