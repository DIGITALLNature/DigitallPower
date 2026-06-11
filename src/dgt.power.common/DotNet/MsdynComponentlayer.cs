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
    [EntityLogicalName("msdyn_componentlayer")]
    [System.CodeDom.Compiler.GeneratedCode("dgtp", "2026")]
    [ExcludeFromCodeCoverage]
    [SuppressMessage("Design", "CA1034")]
    [SuppressMessage("Performance", "CA1815")]
    public partial class MsdynComponentlayer : Entity, INotifyPropertyChanging, INotifyPropertyChanged
    {
        #region ctor
        [DebuggerNonUserCode]
        public MsdynComponentlayer() : this(false)
        {
        }
        [DebuggerNonUserCode]
        public MsdynComponentlayer(bool trackChanges = false) : base(EntityLogicalName)
        {
            _trackChanges = trackChanges;
        }
        [DebuggerNonUserCode]
        public MsdynComponentlayer(Guid id, bool trackChanges = false) : base(EntityLogicalName, id)
        {
            _trackChanges = trackChanges;
        }
        [DebuggerNonUserCode]
        public MsdynComponentlayer(KeyAttributeCollection keyAttributes, bool trackChanges = false) : base(EntityLogicalName, keyAttributes)
        {
            _trackChanges = trackChanges;
        }
        [DebuggerNonUserCode]
        public MsdynComponentlayer(string keyName, object keyValue, bool trackChanges = false) : base(EntityLogicalName, keyName, keyValue)
        {
            _trackChanges = trackChanges;
        }
        #endregion

        #region fields
        private readonly bool _trackChanges;
        private readonly Lazy<HashSet<string>> _changedProperties = new();
        #endregion

        #region consts
        public const string EntityLogicalName = "msdyn_componentlayer";
        public const string PrimaryNameAttribute = "msdyn_name";
        public const int EntityTypeCode = 10006;
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
        [AttributeLogicalName("msdyn_componentlayerid")]
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
                MsdynComponentlayerId = value;
            }
        }

        /// <summary>
		/// Unique identifier for entity instances
		/// </summary>
        [AttributeLogicalName("msdyn_componentlayerid")]
        public Guid? MsdynComponentlayerId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<Guid?>("msdyn_componentlayerid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("msdyn_componentlayerid", value);
                base.Id = value.HasValue ? value.Value : Guid.Empty;
                OnPropertyChanged();
            }
        }

        
        [AttributeLogicalName("msdyn_changes")]
        public string? MsdynChanges
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("msdyn_changes");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("msdyn_changes", value);
                OnPropertyChanged();
            }
        }

        
        [AttributeLogicalName("msdyn_children")]
        public string? MsdynChildren
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("msdyn_children");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("msdyn_children", value);
                OnPropertyChanged();
            }
        }

        
        [AttributeLogicalName("msdyn_componentid")]
        public string? MsdynComponentid
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("msdyn_componentid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("msdyn_componentid", value);
                OnPropertyChanged();
            }
        }

        
        [AttributeLogicalName("msdyn_componentjson")]
        public string? MsdynComponentjson
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("msdyn_componentjson");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("msdyn_componentjson", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// The name of the component.
		/// </summary>
        [AttributeLogicalName("msdyn_name")]
        public string? MsdynName
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("msdyn_name");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("msdyn_name", value);
                OnPropertyChanged();
            }
        }

        
        [AttributeLogicalName("msdyn_order")]
        public int? MsdynOrder
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("msdyn_order");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("msdyn_order", value);
                OnPropertyChanged();
            }
        }

        
        [AttributeLogicalName("msdyn_overwritetime")]
        public DateTime? MsdynEndtime
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<DateTime?>("msdyn_overwritetime");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("msdyn_overwritetime", value);
                OnPropertyChanged();
            }
        }

        
        [AttributeLogicalName("msdyn_publishername")]
        public string? MsdynPublishername
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("msdyn_publishername");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("msdyn_publishername", value);
                OnPropertyChanged();
            }
        }

        
        [AttributeLogicalName("msdyn_solutioncomponentname")]
        public string? MsdynSolutioncomponentname
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("msdyn_solutioncomponentname");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("msdyn_solutioncomponentname", value);
                OnPropertyChanged();
            }
        }

        
        [AttributeLogicalName("msdyn_solutionname")]
        public string? MsdynSolutionname
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("msdyn_solutionname");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("msdyn_solutionname", value);
                OnPropertyChanged();
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
            public const string MsdynComponentlayerId = "msdyn_componentlayerid";
            public const string MsdynChanges = "msdyn_changes";
            public const string MsdynChildren = "msdyn_children";
            public const string MsdynComponentid = "msdyn_componentid";
            public const string MsdynComponentjson = "msdyn_componentjson";
            public const string MsdynName = "msdyn_name";
            public const string MsdynOrder = "msdyn_order";
            public const string MsdynEndtime = "msdyn_overwritetime";
            public const string MsdynPublishername = "msdyn_publishername";
            public const string MsdynSolutioncomponentname = "msdyn_solutioncomponentname";
            public const string MsdynSolutionname = "msdyn_solutionname";
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

        public static MsdynComponentlayer Retrieve(IOrganizationService service, Guid id)
        {
            return Retrieve(service, id, new ColumnSet(true));
        }

        public static MsdynComponentlayer Retrieve(IOrganizationService service, Guid id, ColumnSet columnSet)
        {
            return service.Retrieve("msdyn_componentlayer", id, columnSet).ToEntity<MsdynComponentlayer>();
        }

        public MsdynComponentlayer GetChangedEntity()
        {
            if (!_trackChanges)
            {
                return this;
            }

            var attr = new AttributeCollection();
            foreach (var attrName in _changedProperties.Value.Select(changedProperty => GetType().GetProperty(changedProperty)!.GetCustomAttribute<AttributeLogicalNameAttribute>()!.LogicalName).Where(attrName => Contains(attrName)))
            {
                attr.Add(attrName, this[attrName]);
            }
            return new MsdynComponentlayer(Id) { Attributes = attr };
        }
        #endregion
    }

    #region Context
    public partial class DataContext
    {
        public IQueryable<MsdynComponentlayer> MsdynComponentlayerSet
        {
            get
            {
                return CreateQuery<MsdynComponentlayer>();
            }
        }
    }
    #endregion
}
