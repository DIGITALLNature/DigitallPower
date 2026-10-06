using System.Diagnostics.CodeAnalysis;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System.Runtime.Serialization;

// ReSharper disable All
namespace dgt.power.dataverse
{
    /// <inheritdoc cref="Microsoft.Xrm.Sdk.Entity" />
    
    [EntityLogicalName("msdyn_componentlayer")]
    [System.CodeDom.Compiler.GeneratedCode("dgtp", "2026")]
    [ExcludeFromCodeCoverage]
    [SuppressMessage("Design", "CA1034")]
    [SuppressMessage("Performance", "CA1815")]
    public partial class MsdynComponentlayer : Entity
    {
        #region ctor
        public MsdynComponentlayer() : base(EntityLogicalName) { }

        public MsdynComponentlayer(Guid id) : base(EntityLogicalName, id) { }

        public MsdynComponentlayer(KeyAttributeCollection keyAttributes) : base(EntityLogicalName, keyAttributes) { }

        public MsdynComponentlayer(string keyName, object keyValue) : base(EntityLogicalName, keyName, keyValue) { }
        #endregion

        #region consts
        public const string EntityLogicalName = "msdyn_componentlayer";
        public const string PrimaryNameAttribute = "msdyn_name";
        public const int EntityTypeCode = 10006;
        #endregion

        #region Attributes
        [AttributeLogicalName("msdyn_componentlayerid")]
        [IgnoreDataMember]
        public new Guid Id
        {
            get
            {
                return base.Id;
            }
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
            get
            {
                return GetAttributeValue<Guid?>("msdyn_componentlayerid");
            }
            set
            {
                SetAttributeValue("msdyn_componentlayerid", value);
                base.Id = value.HasValue ? value.Value : Guid.Empty;
            }
        }

        
        [AttributeLogicalName("msdyn_changes")]
        public string? MsdynChanges
        {
            get
            {
                return GetAttributeValue<string?>("msdyn_changes");
            }
            set
            {
                SetAttributeValue("msdyn_changes", value);
            }
        }

        
        [AttributeLogicalName("msdyn_children")]
        public string? MsdynChildren
        {
            get
            {
                return GetAttributeValue<string?>("msdyn_children");
            }
            set
            {
                SetAttributeValue("msdyn_children", value);
            }
        }

        
        [AttributeLogicalName("msdyn_componentid")]
        public string? MsdynComponentid
        {
            get
            {
                return GetAttributeValue<string?>("msdyn_componentid");
            }
            set
            {
                SetAttributeValue("msdyn_componentid", value);
            }
        }

        
        [AttributeLogicalName("msdyn_componentjson")]
        public string? MsdynComponentjson
        {
            get
            {
                return GetAttributeValue<string?>("msdyn_componentjson");
            }
            set
            {
                SetAttributeValue("msdyn_componentjson", value);
            }
        }

        /// <summary>
		/// The name of the component.
		/// </summary>
        [AttributeLogicalName("msdyn_name")]
        public string? MsdynName
        {
            get
            {
                return GetAttributeValue<string?>("msdyn_name");
            }
            set
            {
                SetAttributeValue("msdyn_name", value);
            }
        }

        
        [AttributeLogicalName("msdyn_order")]
        public int? MsdynOrder
        {
            get
            {
                return GetAttributeValue<int?>("msdyn_order");
            }
            set
            {
                SetAttributeValue("msdyn_order", value);
            }
        }

        
        [AttributeLogicalName("msdyn_overwritetime")]
        public DateTime? MsdynEndtime
        {
            get
            {
                return GetAttributeValue<DateTime?>("msdyn_overwritetime");
            }
            set
            {
                SetAttributeValue("msdyn_overwritetime", value);
            }
        }

        
        [AttributeLogicalName("msdyn_publishername")]
        public string? MsdynPublishername
        {
            get
            {
                return GetAttributeValue<string?>("msdyn_publishername");
            }
            set
            {
                SetAttributeValue("msdyn_publishername", value);
            }
        }

        
        [AttributeLogicalName("msdyn_solutioncomponentname")]
        public string? MsdynSolutioncomponentname
        {
            get
            {
                return GetAttributeValue<string?>("msdyn_solutioncomponentname");
            }
            set
            {
                SetAttributeValue("msdyn_solutioncomponentname", value);
            }
        }

        
        [AttributeLogicalName("msdyn_solutionname")]
        public string? MsdynSolutionname
        {
            get
            {
                return GetAttributeValue<string?>("msdyn_solutionname");
            }
            set
            {
                SetAttributeValue("msdyn_solutionname", value);
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
