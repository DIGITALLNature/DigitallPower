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
	
	[DataContractAttribute()]
	[EntityLogicalNameAttribute("attribute")]
	[System.CodeDom.Compiler.GeneratedCode("dgtp", "2023")]
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
		public Attribute(Guid id, bool trackChanges = false) : base(EntityLogicalName,id)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public Attribute(KeyAttributeCollection keyAttributes, bool trackChanges = false) : base(EntityLogicalName,keyAttributes)
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
        private readonly Lazy<HashSet<string>> _changedProperties = new Lazy<HashSet<string>>();
        #endregion

        #region consts
        public const string EntityLogicalName = "attribute";
        public const int EntityTypeCode = 9808;
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
		[AttributeLogicalNameAttribute("attributeid")]
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
                OnPropertyChanging(nameof(AttributeId));
                SetAttributeValue("attributeid", value);
				if (value.HasValue)
				{
					base.Id = value.Value;
				}
				else
				{
					base.Id = System.Guid.Empty;
				}
                OnPropertyChanged(nameof(AttributeId));
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
                OnPropertyChanging(nameof(ExternalName));
                SetAttributeValue("externalname", value);
                OnPropertyChanged(nameof(ExternalName));
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
                OnPropertyChanging(nameof(LogicalName));
                SetAttributeValue("logicalname", value);
                OnPropertyChanged(nameof(LogicalName));
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
                OnPropertyChanging(nameof(ManagedPropertyLogicalName));
                SetAttributeValue("managedpropertylogicalname", value);
                OnPropertyChanged(nameof(ManagedPropertyLogicalName));
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
                OnPropertyChanging(nameof(ManagedPropertyParentAttributeName));
                SetAttributeValue("managedpropertyparentattributename", value);
                OnPropertyChanged(nameof(ManagedPropertyParentAttributeName));
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
                OnPropertyChanging(nameof(Name));
                SetAttributeValue("name", value);
                OnPropertyChanged(nameof(Name));
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
                OnPropertyChanging(nameof(PhysicalName));
                SetAttributeValue("physicalname", value);
                OnPropertyChanged(nameof(PhysicalName));
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
                OnPropertyChanging(nameof(TableColumnName));
                SetAttributeValue("tablecolumnname", value);
                OnPropertyChanged(nameof(TableColumnName));
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
		public static class Options
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
		public static class LogicalNames
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
        public static class Relations
        {
            public static class OneToMany
            {
				public const string AttributeSolutioncomponentattrconfig = "attribute_solutioncomponentattrconfig";
				public const string ReferencedattributeRelationshipattribute = "referencedattribute_relationshipattribute";
				public const string ReferencingattributeRelationshipattribute = "referencingattribute_relationshipattribute";
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

        public static Attribute Retrieve(IOrganizationService service, Guid id)
        {
            return Retrieve(service,id, new ColumnSet(true));
        }

        public static Attribute Retrieve(IOrganizationService service, Guid id, ColumnSet columnSet)
        {
            return service.Retrieve("attribute", id, columnSet).ToEntity<Attribute>();
        }

        public Attribute GetChangedEntity()
        {
            if (_trackChanges)
            {
                var attr = new AttributeCollection();
                foreach (var attrName in _changedProperties.Value.Select(changedProperty => ((AttributeLogicalNameAttribute) GetType().GetProperty(changedProperty)!.GetCustomAttribute(typeof (AttributeLogicalNameAttribute))!).LogicalName).Where(attrName => Contains(attrName)))
                {
                    attr.Add(attrName,this[attrName]);
                }
                return new  Attribute(Id) {Attributes = attr };
            }
            return this;
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
