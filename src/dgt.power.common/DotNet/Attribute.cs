using System.Diagnostics.CodeAnalysis;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System.Runtime.Serialization;

// ReSharper disable All
namespace dgt.power.dataverse
{
    /// <inheritdoc cref="Microsoft.Xrm.Sdk.Entity" />
    
    [EntityLogicalName("attribute")]
    [System.CodeDom.Compiler.GeneratedCode("dgtp", "2026")]
    [ExcludeFromCodeCoverage]
    [SuppressMessage("Design", "CA1034")]
    [SuppressMessage("Performance", "CA1815")]
    public partial class Attribute : Entity
    {
        #region ctor
        public Attribute() : base(EntityLogicalName) { }

        public Attribute(Guid id) : base(EntityLogicalName, id) { }

        public Attribute(KeyAttributeCollection keyAttributes) : base(EntityLogicalName, keyAttributes) { }

        public Attribute(string keyName, object keyValue) : base(EntityLogicalName, keyName, keyValue) { }
        #endregion

        #region consts
        public const string EntityLogicalName = "attribute";
        public const string PrimaryNameAttribute = "name";
        public const int EntityTypeCode = 9808;
        #endregion

        #region Attributes
        [AttributeLogicalName("attributeid")]
        [IgnoreDataMember]
        public new Guid Id
        {
            get
            {
                return base.Id;
            }
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
            get
            {
                return GetAttributeValue<Guid?>("attributeid");
            }
            set
            {
                SetAttributeValue("attributeid", value);
                base.Id = value.HasValue ? value.Value : Guid.Empty;
            }
        }

        /// <summary>
		/// Attribute Of
		/// </summary>
        [AttributeLogicalName("attributeof")]
        public Guid? AttributeOf
        {
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
            get
            {
                return GetAttributeValue<string?>("externalname");
            }
            set
            {
                SetAttributeValue("externalname", value);
            }
        }

        /// <summary>
		/// The logical name of this attribute.
		/// </summary>
        [AttributeLogicalName("logicalname")]
        public string? LogicalName
        {
            get
            {
                return GetAttributeValue<string?>("logicalname");
            }
            set
            {
                SetAttributeValue("logicalname", value);
            }
        }

        /// <summary>
		/// The managed property logical name of this attribute.
		/// </summary>
        [AttributeLogicalName("managedpropertylogicalname")]
        public string? ManagedPropertyLogicalName
        {
            get
            {
                return GetAttributeValue<string?>("managedpropertylogicalname");
            }
            set
            {
                SetAttributeValue("managedpropertylogicalname", value);
            }
        }

        /// <summary>
		/// The managed property parent attribute name of this attribute.
		/// </summary>
        [AttributeLogicalName("managedpropertyparentattributename")]
        public string? ManagedPropertyParentAttributeName
        {
            get
            {
                return GetAttributeValue<string?>("managedpropertyparentattributename");
            }
            set
            {
                SetAttributeValue("managedpropertyparentattributename", value);
            }
        }

        /// <summary>
		/// The name of this Attribute.
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
		/// The physical name of this attribute.
		/// </summary>
        [AttributeLogicalName("physicalname")]
        public string? PhysicalName
        {
            get
            {
                return GetAttributeValue<string?>("physicalname");
            }
            set
            {
                SetAttributeValue("physicalname", value);
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
		/// The table column name of this attribute.
		/// </summary>
        [AttributeLogicalName("tablecolumnname")]
        public string? TableColumnName
        {
            get
            {
                return GetAttributeValue<string?>("tablecolumnname");
            }
            set
            {
                SetAttributeValue("tablecolumnname", value);
            }
        }

        /// <summary>
		/// Valid For Read API
		/// </summary>
        [AttributeLogicalName("validforreadapi")]
        public bool? ValidForReadAPI
        {
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
                public const string ControlconfigurationAttributeAttribute = "controlconfiguration_attribute_attribute";
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
