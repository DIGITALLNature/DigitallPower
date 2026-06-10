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
    
    [DataContract]
    [EntityLogicalName("attribute")]
    [System.CodeDom.Compiler.GeneratedCode("dgtp", "2026")]
    [ExcludeFromCodeCoverage]
    public partial class Attribute : Entity, INotifyPropertyChanging, INotifyPropertyChanged
    {
        #region ctor
        [DebuggerNonUserCode]
        public Attribute() : this(false)
        {
        }
        [DebuggerNonUserCode]
        public Attribute(bool trackChanges = false) : base(EntityLogicalName)
        {
            _trackChanges = trackChanges;
        }
        [DebuggerNonUserCode]
        public Attribute(Guid id, bool trackChanges = false) : base(EntityLogicalName, id)
        {
            _trackChanges = trackChanges;
        }
        [DebuggerNonUserCode]
        public Attribute(KeyAttributeCollection keyAttributes, bool trackChanges = false) : base(EntityLogicalName, keyAttributes)
        {
            _trackChanges = trackChanges;
        }
        [DebuggerNonUserCode]
        public Attribute(string keyName, object keyValue, bool trackChanges = false) : base(EntityLogicalName, keyName, keyValue)
        {
            _trackChanges = trackChanges;
        }
        #endregion

        #region fields
        private readonly bool _trackChanges;
        private readonly Lazy<HashSet<string>> _changedProperties = new();
        #endregion

        #region consts
        public const string EntityLogicalName = "attribute";
        public const string PrimaryNameAttribute = "name";
        public const int EntityTypeCode = 9808;
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
        [AttributeLogicalName("attributeid")]
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
                AttributeId = value;
            }
        }

        /// <summary>
		/// Unique identifier of the attribute.
		/// </summary>
        [AttributeLogicalName("attributeid")]
        public Guid? AttributeId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<Guid?>("attributeid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("attributeid", value);
                base.Id = value.HasValue ? value.Value : Guid.Empty;
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Attribute Of
		/// </summary>
        [AttributeLogicalName("attributeof")]
        public Guid? AttributeOf
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<Guid?>("attributeof");
            }
        }

        /// <summary>
		/// Attribute Type Id
		/// </summary>
        [AttributeLogicalName("attributetypeid")]
        public Guid? AttributeTypeId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<Guid?>("attributetypeid");
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
		/// The external name of this attribute.
		/// </summary>
        [AttributeLogicalName("externalname")]
        public string? ExternalName
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("externalname");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("externalname", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// The logical name of this attribute.
		/// </summary>
        [AttributeLogicalName("logicalname")]
        public string? LogicalName
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("logicalname");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("logicalname", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// The managed property logical name of this attribute.
		/// </summary>
        [AttributeLogicalName("managedpropertylogicalname")]
        public string? ManagedPropertyLogicalName
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("managedpropertylogicalname");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("managedpropertylogicalname", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// The managed property parent attribute name of this attribute.
		/// </summary>
        [AttributeLogicalName("managedpropertyparentattributename")]
        public string? ManagedPropertyParentAttributeName
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("managedpropertyparentattributename");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("managedpropertyparentattributename", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// The name of this Attribute.
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
		/// The physical name of this attribute.
		/// </summary>
        [AttributeLogicalName("physicalname")]
        public string? PhysicalName
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("physicalname");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("physicalname", value);
                OnPropertyChanged();
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
		/// The table column name of this attribute.
		/// </summary>
        [AttributeLogicalName("tablecolumnname")]
        public string? TableColumnName
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("tablecolumnname");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("tablecolumnname", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Valid For Read API
		/// </summary>
        [AttributeLogicalName("validforreadapi")]
        public bool? ValidForReadAPI
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("validforreadapi");
            }
        }

        /// <summary>
		/// The version number of this attribute.
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
            public struct ValidForReadAPI
            {
                public const bool No = false;
                public const bool Yes = true;
            }
        }
        #endregion

        #region LogicalNames
        public static partial class LogicalNames
        {
            public const string AttributeId = "attributeid";
            public const string AttributeOf = "attributeof";
            public const string AttributeTypeId = "attributetypeid";
            public const string ComponentState = "componentstate";
            public const string ExternalName = "externalname";
            public const string LogicalName = "logicalname";
            public const string ManagedPropertyLogicalName = "managedpropertylogicalname";
            public const string ManagedPropertyParentAttributeName = "managedpropertyparentattributename";
            public const string Name = "name";
            public const string OverwriteTime = "overwritetime";
            public const string PhysicalName = "physicalname";
            public const string SolutionId = "solutionid";
            public const string TableColumnName = "tablecolumnname";
            public const string ValidForReadAPI = "validforreadapi";
            public const string VersionNumber = "versionnumber";
        }
        #endregion

        #region Relations
        public static partial class Relations
        {
            public static class OneToMany
            {
                public const string AttributeAiskillconfigAttribute = "attribute_aiskillconfig_Attribute";
                public const string AttributeDvfilesearchattribute = "attribute_dvfilesearchattribute";
                public const string AttributeDvtablesearchattribute = "attribute_dvtablesearchattribute";
                public const string AttributeSensitivitylabelattributemappingAttributeId = "attribute_sensitivitylabelattributemapping_AttributeId";
                public const string AttributeSolutioncomponentattrconfig = "attribute_solutioncomponentattrconfig";
                public const string AttributeclusterconfigExtensionofrecordidAttribute = "attributeclusterconfig_extensionofrecordid_attribute";
                public const string EmailaddressconfigurationAttributeAttributeId = "emailaddressconfiguration_attribute_AttributeId";
                public const string ReferencedattributeRelationshipattribute = "referencedattribute_relationshipattribute";
                public const string ReferencingattributeRelationshipattribute = "referencingattribute_relationshipattribute";
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

        public static Attribute Retrieve(IOrganizationService service, Guid id)
        {
            return Retrieve(service, id, new ColumnSet(true));
        }

        public static Attribute Retrieve(IOrganizationService service, Guid id, ColumnSet columnSet)
        {
            return service.Retrieve("attribute", id, columnSet).ToEntity<Attribute>();
        }

        public Attribute GetChangedEntity()
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
            return new Attribute(Id) { Attributes = attr };
        }
        #endregion
    }

    #region Context
    public partial class DataContext
    {
        public IQueryable<Attribute> AttributeSet
        {
            get
            {
                return CreateQuery<Attribute>();
            }
        }
    }
    #endregion
}
