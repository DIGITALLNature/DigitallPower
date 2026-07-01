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
	/// Business, division, or department in the Microsoft Dynamics 365 database.
	/// </summary>
    [DataContract]
    [EntityLogicalName("businessunit")]
    [System.CodeDom.Compiler.GeneratedCode("dgtp", "2026")]
    [ExcludeFromCodeCoverage]
    [SuppressMessage("Design", "CA1034")]
    [SuppressMessage("Performance", "CA1815")]
    public partial class BusinessUnit : Entity, INotifyPropertyChanging, INotifyPropertyChanged
    {
        #region ctor
        [DebuggerNonUserCode]
        public BusinessUnit() : this(false)
        {
        }
        [DebuggerNonUserCode]
        public BusinessUnit(bool trackChanges = false) : base(EntityLogicalName)
        {
            _trackChanges = trackChanges;
        }
        [DebuggerNonUserCode]
        public BusinessUnit(Guid id, bool trackChanges = false) : base(EntityLogicalName, id)
        {
            _trackChanges = trackChanges;
        }
        [DebuggerNonUserCode]
        public BusinessUnit(KeyAttributeCollection keyAttributes, bool trackChanges = false) : base(EntityLogicalName, keyAttributes)
        {
            _trackChanges = trackChanges;
        }
        [DebuggerNonUserCode]
        public BusinessUnit(string keyName, object keyValue, bool trackChanges = false) : base(EntityLogicalName, keyName, keyValue)
        {
            _trackChanges = trackChanges;
        }
        #endregion

        #region fields
        private readonly bool _trackChanges;
        private readonly Lazy<HashSet<string>> _changedProperties = new();
        #endregion

        #region consts
        public const string EntityLogicalName = "businessunit";
        public const string PrimaryNameAttribute = "name";
        public const int EntityTypeCode = 10;
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
        [AttributeLogicalName("businessunitid")]
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
                BusinessUnitId = value;
            }
        }

        /// <summary>
		/// Unique identifier for address 1.
		/// </summary>
        [AttributeLogicalName("address1_addressid")]
        public Guid? Address1AddressId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<Guid?>("address1_addressid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("address1_addressid", value);
                base.Id = value.HasValue ? value.Value : Guid.Empty;
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier for address 2.
		/// </summary>
        [AttributeLogicalName("address2_addressid")]
        public Guid? Address2AddressId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<Guid?>("address2_addressid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("address2_addressid", value);
                base.Id = value.HasValue ? value.Value : Guid.Empty;
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier of the business unit.
		/// </summary>
        [AttributeLogicalName("businessunitid")]
        public Guid? BusinessUnitId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<Guid?>("businessunitid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("businessunitid", value);
                base.Id = value.HasValue ? value.Value : Guid.Empty;
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Type of address for address 1, such as billing, shipping, or primary address.
		/// </summary>
        [AttributeLogicalName("address1_addresstypecode")]
        public OptionSetValue? Address1AddressTypeCode
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("address1_addresstypecode");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("address1_addresstypecode", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// City name for address 1.
		/// </summary>
        [AttributeLogicalName("address1_city")]
        public string? Address1City
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("address1_city");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("address1_city", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Country/region name for address 1.
		/// </summary>
        [AttributeLogicalName("address1_country")]
        public string? Address1Country
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("address1_country");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("address1_country", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// County name for address 1.
		/// </summary>
        [AttributeLogicalName("address1_county")]
        public string? Address1County
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("address1_county");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("address1_county", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Fax number for address 1.
		/// </summary>
        [AttributeLogicalName("address1_fax")]
        public string? Address1Fax
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("address1_fax");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("address1_fax", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Latitude for address 1.
		/// </summary>
        [AttributeLogicalName("address1_latitude")]
        public double? Address1Latitude
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<double?>("address1_latitude");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("address1_latitude", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// First line for entering address 1 information.
		/// </summary>
        [AttributeLogicalName("address1_line1")]
        public string? Address1Line1
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("address1_line1");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("address1_line1", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Second line for entering address 1 information.
		/// </summary>
        [AttributeLogicalName("address1_line2")]
        public string? Address1Line2
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("address1_line2");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("address1_line2", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Third line for entering address 1 information.
		/// </summary>
        [AttributeLogicalName("address1_line3")]
        public string? Address1Line3
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("address1_line3");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("address1_line3", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Longitude for address 1.
		/// </summary>
        [AttributeLogicalName("address1_longitude")]
        public double? Address1Longitude
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<double?>("address1_longitude");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("address1_longitude", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Name to enter for address 1.
		/// </summary>
        [AttributeLogicalName("address1_name")]
        public string? Address1Name
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("address1_name");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("address1_name", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// ZIP Code or postal code for address 1.
		/// </summary>
        [AttributeLogicalName("address1_postalcode")]
        public string? Address1PostalCode
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("address1_postalcode");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("address1_postalcode", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Post office box number for address 1.
		/// </summary>
        [AttributeLogicalName("address1_postofficebox")]
        public string? Address1PostOfficeBox
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("address1_postofficebox");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("address1_postofficebox", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Method of shipment for address 1.
		/// </summary>
        [AttributeLogicalName("address1_shippingmethodcode")]
        public OptionSetValue? Address1ShippingMethodCode
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("address1_shippingmethodcode");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("address1_shippingmethodcode", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// State or province for address 1.
		/// </summary>
        [AttributeLogicalName("address1_stateorprovince")]
        public string? Address1StateOrProvince
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("address1_stateorprovince");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("address1_stateorprovince", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// First telephone number associated with address 1.
		/// </summary>
        [AttributeLogicalName("address1_telephone1")]
        public string? Address1Telephone1
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("address1_telephone1");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("address1_telephone1", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Second telephone number associated with address 1.
		/// </summary>
        [AttributeLogicalName("address1_telephone2")]
        public string? Address1Telephone2
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("address1_telephone2");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("address1_telephone2", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Third telephone number associated with address 1.
		/// </summary>
        [AttributeLogicalName("address1_telephone3")]
        public string? Address1Telephone3
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("address1_telephone3");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("address1_telephone3", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// United Parcel Service (UPS) zone for address 1.
		/// </summary>
        [AttributeLogicalName("address1_upszone")]
        public string? Address1UPSZone
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("address1_upszone");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("address1_upszone", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// UTC offset for address 1. This is the difference between local time and standard Coordinated Universal Time.
		/// </summary>
        [AttributeLogicalName("address1_utcoffset")]
        public int? Address1UTCOffset
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("address1_utcoffset");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("address1_utcoffset", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Type of address for address 2, such as billing, shipping, or primary address.
		/// </summary>
        [AttributeLogicalName("address2_addresstypecode")]
        public OptionSetValue? Address2AddressTypeCode
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("address2_addresstypecode");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("address2_addresstypecode", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// City name for address 2.
		/// </summary>
        [AttributeLogicalName("address2_city")]
        public string? Address2City
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("address2_city");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("address2_city", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Country/region name for address 2.
		/// </summary>
        [AttributeLogicalName("address2_country")]
        public string? Address2Country
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("address2_country");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("address2_country", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// County name for address 2.
		/// </summary>
        [AttributeLogicalName("address2_county")]
        public string? Address2County
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("address2_county");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("address2_county", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Fax number for address 2.
		/// </summary>
        [AttributeLogicalName("address2_fax")]
        public string? Address2Fax
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("address2_fax");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("address2_fax", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Latitude for address 2.
		/// </summary>
        [AttributeLogicalName("address2_latitude")]
        public double? Address2Latitude
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<double?>("address2_latitude");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("address2_latitude", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// First line for entering address 2 information.
		/// </summary>
        [AttributeLogicalName("address2_line1")]
        public string? Address2Line1
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("address2_line1");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("address2_line1", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Second line for entering address 2 information.
		/// </summary>
        [AttributeLogicalName("address2_line2")]
        public string? Address2Line2
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("address2_line2");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("address2_line2", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Third line for entering address 2 information.
		/// </summary>
        [AttributeLogicalName("address2_line3")]
        public string? Address2Line3
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("address2_line3");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("address2_line3", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Longitude for address 2.
		/// </summary>
        [AttributeLogicalName("address2_longitude")]
        public double? Address2Longitude
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<double?>("address2_longitude");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("address2_longitude", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Name to enter for address 2.
		/// </summary>
        [AttributeLogicalName("address2_name")]
        public string? Address2Name
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("address2_name");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("address2_name", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// ZIP Code or postal code for address 2.
		/// </summary>
        [AttributeLogicalName("address2_postalcode")]
        public string? Address2PostalCode
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("address2_postalcode");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("address2_postalcode", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Post office box number for address 2.
		/// </summary>
        [AttributeLogicalName("address2_postofficebox")]
        public string? Address2PostOfficeBox
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("address2_postofficebox");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("address2_postofficebox", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Method of shipment for address 2.
		/// </summary>
        [AttributeLogicalName("address2_shippingmethodcode")]
        public OptionSetValue? Address2ShippingMethodCode
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("address2_shippingmethodcode");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("address2_shippingmethodcode", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// State or province for address 2.
		/// </summary>
        [AttributeLogicalName("address2_stateorprovince")]
        public string? Address2StateOrProvince
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("address2_stateorprovince");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("address2_stateorprovince", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// First telephone number associated with address 2.
		/// </summary>
        [AttributeLogicalName("address2_telephone1")]
        public string? Address2Telephone1
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("address2_telephone1");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("address2_telephone1", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Second telephone number associated with address 2.
		/// </summary>
        [AttributeLogicalName("address2_telephone2")]
        public string? Address2Telephone2
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("address2_telephone2");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("address2_telephone2", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Third telephone number associated with address 2.
		/// </summary>
        [AttributeLogicalName("address2_telephone3")]
        public string? Address2Telephone3
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("address2_telephone3");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("address2_telephone3", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// United Parcel Service (UPS) zone for address 2.
		/// </summary>
        [AttributeLogicalName("address2_upszone")]
        public string? Address2UPSZone
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("address2_upszone");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("address2_upszone", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// UTC offset for address 2. This is the difference between local time and standard Coordinated Universal Time.
		/// </summary>
        [AttributeLogicalName("address2_utcoffset")]
        public int? Address2UTCOffset
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("address2_utcoffset");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("address2_utcoffset", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Fiscal calendar associated with the business unit.
		/// </summary>
        [AttributeLogicalName("calendarid")]
        public EntityReference? CalendarId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<EntityReference?>("calendarid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("calendarid", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Name of the business unit cost center.
		/// </summary>
        [AttributeLogicalName("costcenter")]
        public string? CostCenter
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("costcenter");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("costcenter", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier of the user who created the business unit.
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
		/// Date and time when the business unit was created.
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
		/// Unique identifier of the delegate user who created the businessunit.
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
		/// Credit limit for the business unit.
		/// </summary>
        [AttributeLogicalName("creditlimit")]
        public double? CreditLimit
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<double?>("creditlimit");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("creditlimit", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Description of the business unit.
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
		/// Reason for disabling the business unit.
		/// </summary>
        [AttributeLogicalName("disabledreason")]
        public string? DisabledReason
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("disabledreason");
            }
        }

        /// <summary>
		/// Name of the division to which the business unit belongs.
		/// </summary>
        [AttributeLogicalName("divisionname")]
        public string? DivisionName
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("divisionname");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("divisionname", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Email address for the business unit.
		/// </summary>
        [AttributeLogicalName("emailaddress")]
        public string? EMailAddress
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("emailaddress");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("emailaddress", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Exchange rate for the currency associated with the businessunit with respect to the base currency.
		/// </summary>
        [AttributeLogicalName("exchangerate")]
        public decimal? ExchangeRate
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<decimal?>("exchangerate");
            }
        }

        /// <summary>
		/// Alternative name under which the business unit can be filed.
		/// </summary>
        [AttributeLogicalName("fileasname")]
        public string? FileAsName
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("fileasname");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("fileasname", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// FTP site URL for the business unit.
		/// </summary>
        [AttributeLogicalName("ftpsiteurl")]
        public string? FtpSiteUrl
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("ftpsiteurl");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("ftpsiteurl", value);
                OnPropertyChanged();
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
                OnPropertyChanging();
                SetAttributeValue("importsequencenumber", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Inheritance mask for the business unit.
		/// </summary>
        [AttributeLogicalName("inheritancemask")]
        public int? InheritanceMask
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("inheritancemask");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("inheritancemask", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Information about whether the business unit is enabled or disabled.
		/// </summary>
        [AttributeLogicalName("isdisabled")]
        public bool? IsDisabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isdisabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("isdisabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier of the user who last modified the business unit.
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
		/// Date and time when the business unit was last modified.
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
		/// Unique identifier of the delegate user who last modified the businessunit.
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
		/// Name of the business unit.
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
		/// Unique identifier of the organization associated with the business unit.
		/// </summary>
        [AttributeLogicalName("organizationid")]
        public EntityReference? OrganizationId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<EntityReference?>("organizationid");
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
                OnPropertyChanging();
                SetAttributeValue("overriddencreatedon", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier for the parent business unit.
		/// </summary>
        [AttributeLogicalName("parentbusinessunitid")]
        public EntityReference? ParentBusinessUnitId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<EntityReference?>("parentbusinessunitid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("parentbusinessunitid", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Picture or diagram of the business unit.
		/// </summary>
        [AttributeLogicalName("picture")]
        public string? Picture
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("picture");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("picture", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Stock exchange on which the business is listed.
		/// </summary>
        [AttributeLogicalName("stockexchange")]
        public string? StockExchange
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("stockexchange");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("stockexchange", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Stock exchange ticker symbol for the business unit.
		/// </summary>
        [AttributeLogicalName("tickersymbol")]
        public string? TickerSymbol
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("tickersymbol");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("tickersymbol", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier of the currency associated with the businessunit.
		/// </summary>
        [AttributeLogicalName("transactioncurrencyid")]
        public EntityReference? TransactionCurrencyId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<EntityReference?>("transactioncurrencyid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("transactioncurrencyid", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// UTC offset for the business unit. This is the difference between local time and standard Coordinated Universal Time.
		/// </summary>
        [AttributeLogicalName("utcoffset")]
        public int? UTCOffset
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("utcoffset");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("utcoffset", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Version number of the business unit.
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

        /// <summary>
		/// Website URL for the business unit.
		/// </summary>
        [AttributeLogicalName("websiteurl")]
        public string? WebSiteUrl
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("websiteurl");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("websiteurl", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Information about whether workflow or sales process rules have been suspended.
		/// </summary>
        [AttributeLogicalName("workflowsuspended")]
        public bool? WorkflowSuspended
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("workflowsuspended");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("workflowsuspended", value);
                OnPropertyChanged();
            }
        }
        #endregion

        #region NavigationProperties

        /// <summary>
        /// 1:N business_unit_accounts
        /// </summary>
        [RelationshipSchemaName("business_unit_accounts")]
        public IEnumerable<Account> BusinessUnitAccounts
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<Account>("business_unit_accounts", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("business_unit_accounts", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N business_unit_asyncoperation
        /// </summary>
        [RelationshipSchemaName("business_unit_asyncoperation")]
        public IEnumerable<AsyncOperation> BusinessUnitAsyncoperation
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<AsyncOperation>("business_unit_asyncoperation", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("business_unit_asyncoperation", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N business_unit_calendars
        /// </summary>
        [RelationshipSchemaName("business_unit_calendars")]
        public IEnumerable<Calendar> BusinessUnitCalendars
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<Calendar>("business_unit_calendars", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("business_unit_calendars", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N business_unit_contacts
        /// </summary>
        [RelationshipSchemaName("business_unit_contacts")]
        public IEnumerable<Contact> BusinessUnitContacts
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<Contact>("business_unit_contacts", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("business_unit_contacts", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N business_unit_customapi
        /// </summary>
        [RelationshipSchemaName("business_unit_customapi")]
        public IEnumerable<CustomAPI> BusinessUnitCustomapi
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<CustomAPI>("business_unit_customapi", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("business_unit_customapi", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N business_unit_managedidentity
        /// </summary>
        [RelationshipSchemaName("business_unit_managedidentity")]
        public IEnumerable<ManagedIdentity> BusinessUnitManagedidentity
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<ManagedIdentity>("business_unit_managedidentity", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("business_unit_managedidentity", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N business_unit_parent_business_unit
        /// </summary>
        [RelationshipSchemaName("business_unit_parent_business_unit")]
        public IEnumerable<BusinessUnit> BusinessUnitParentBusinessUnit
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<BusinessUnit>("business_unit_parent_business_unit", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("business_unit_parent_business_unit", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N business_unit_queues
        /// </summary>
        [RelationshipSchemaName("business_unit_queues")]
        public IEnumerable<Queue> BusinessUnitQueues
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<Queue>("business_unit_queues", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("business_unit_queues", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N business_unit_queues2
        /// </summary>
        [RelationshipSchemaName("business_unit_queues2")]
        public IEnumerable<Queue> BusinessUnitQueues2
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<Queue>("business_unit_queues2", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("business_unit_queues2", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N business_unit_roles
        /// </summary>
        [RelationshipSchemaName("business_unit_roles")]
        public IEnumerable<Role> BusinessUnitRoles
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<Role>("business_unit_roles", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("business_unit_roles", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N business_unit_routingrule
        /// </summary>
        [RelationshipSchemaName("business_unit_routingrule")]
        public IEnumerable<RoutingRule> BusinessUnitRoutingrule
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<RoutingRule>("business_unit_routingrule", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("business_unit_routingrule", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N business_unit_slabase
        /// </summary>
        [RelationshipSchemaName("business_unit_slabase")]
        public IEnumerable<SLA> BusinessUnitSlabase
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<SLA>("business_unit_slabase", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("business_unit_slabase", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N business_unit_system_users
        /// </summary>
        [RelationshipSchemaName("business_unit_system_users")]
        public IEnumerable<SystemUser> BusinessUnitSystemUsers
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<SystemUser>("business_unit_system_users", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("business_unit_system_users", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N business_unit_teams
        /// </summary>
        [RelationshipSchemaName("business_unit_teams")]
        public IEnumerable<Team> BusinessUnitTeams
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<Team>("business_unit_teams", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("business_unit_teams", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N business_unit_workflow
        /// </summary>
        [RelationshipSchemaName("business_unit_workflow")]
        public IEnumerable<Workflow> BusinessUnitWorkflow
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<Workflow>("business_unit_workflow", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("business_unit_workflow", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N BusinessUnit_AsyncOperations
        /// </summary>
        [RelationshipSchemaName("BusinessUnit_AsyncOperations")]
        public IEnumerable<AsyncOperation> BusinessUnitAsyncOperations
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<AsyncOperation>("BusinessUnit_AsyncOperations", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("BusinessUnit_AsyncOperations", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N BusinessUnit_DuplicateRules
        /// </summary>
        [RelationshipSchemaName("BusinessUnit_DuplicateRules")]
        public IEnumerable<DuplicateRule> BusinessUnitDuplicateRules
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<DuplicateRule>("BusinessUnit_DuplicateRules", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("BusinessUnit_DuplicateRules", null, value);
                OnPropertyChanged();
            }
        }
        #endregion

        #region Options
        public static partial class Options
        {
            public struct Address1AddressTypeCode
            {
                public const int DefaultValue = 1;
            }
            public struct Address1ShippingMethodCode
            {
                public const int DefaultValue = 1;
            }
            public struct Address2AddressTypeCode
            {
                public const int DefaultValue = 1;
            }
            public struct Address2ShippingMethodCode
            {
                public const int DefaultValue = 1;
            }
            public struct IsDisabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct WorkflowSuspended
            {
                public const bool No = false;
                public const bool Yes = true;
            }
        }
        #endregion

        #region LogicalNames
        public static partial class LogicalNames
        {
            public const string Address1AddressId = "address1_addressid";
            public const string Address2AddressId = "address2_addressid";
            public const string BusinessUnitId = "businessunitid";
            public const string Address1AddressTypeCode = "address1_addresstypecode";
            public const string Address1City = "address1_city";
            public const string Address1Country = "address1_country";
            public const string Address1County = "address1_county";
            public const string Address1Fax = "address1_fax";
            public const string Address1Latitude = "address1_latitude";
            public const string Address1Line1 = "address1_line1";
            public const string Address1Line2 = "address1_line2";
            public const string Address1Line3 = "address1_line3";
            public const string Address1Longitude = "address1_longitude";
            public const string Address1Name = "address1_name";
            public const string Address1PostalCode = "address1_postalcode";
            public const string Address1PostOfficeBox = "address1_postofficebox";
            public const string Address1ShippingMethodCode = "address1_shippingmethodcode";
            public const string Address1StateOrProvince = "address1_stateorprovince";
            public const string Address1Telephone1 = "address1_telephone1";
            public const string Address1Telephone2 = "address1_telephone2";
            public const string Address1Telephone3 = "address1_telephone3";
            public const string Address1UPSZone = "address1_upszone";
            public const string Address1UTCOffset = "address1_utcoffset";
            public const string Address2AddressTypeCode = "address2_addresstypecode";
            public const string Address2City = "address2_city";
            public const string Address2Country = "address2_country";
            public const string Address2County = "address2_county";
            public const string Address2Fax = "address2_fax";
            public const string Address2Latitude = "address2_latitude";
            public const string Address2Line1 = "address2_line1";
            public const string Address2Line2 = "address2_line2";
            public const string Address2Line3 = "address2_line3";
            public const string Address2Longitude = "address2_longitude";
            public const string Address2Name = "address2_name";
            public const string Address2PostalCode = "address2_postalcode";
            public const string Address2PostOfficeBox = "address2_postofficebox";
            public const string Address2ShippingMethodCode = "address2_shippingmethodcode";
            public const string Address2StateOrProvince = "address2_stateorprovince";
            public const string Address2Telephone1 = "address2_telephone1";
            public const string Address2Telephone2 = "address2_telephone2";
            public const string Address2Telephone3 = "address2_telephone3";
            public const string Address2UPSZone = "address2_upszone";
            public const string Address2UTCOffset = "address2_utcoffset";
            public const string CalendarId = "calendarid";
            public const string CostCenter = "costcenter";
            public const string CreatedBy = "createdby";
            public const string CreatedOn = "createdon";
            public const string CreatedOnBehalfBy = "createdonbehalfby";
            public const string CreditLimit = "creditlimit";
            public const string Description = "description";
            public const string DisabledReason = "disabledreason";
            public const string DivisionName = "divisionname";
            public const string EMailAddress = "emailaddress";
            public const string ExchangeRate = "exchangerate";
            public const string FileAsName = "fileasname";
            public const string FtpSiteUrl = "ftpsiteurl";
            public const string ImportSequenceNumber = "importsequencenumber";
            public const string InheritanceMask = "inheritancemask";
            public const string IsDisabled = "isdisabled";
            public const string ModifiedBy = "modifiedby";
            public const string ModifiedOn = "modifiedon";
            public const string ModifiedOnBehalfBy = "modifiedonbehalfby";
            public const string Name = "name";
            public const string OrganizationId = "organizationid";
            public const string OverriddenCreatedOn = "overriddencreatedon";
            public const string ParentBusinessUnitId = "parentbusinessunitid";
            public const string Picture = "picture";
            public const string StockExchange = "stockexchange";
            public const string TickerSymbol = "tickersymbol";
            public const string TransactionCurrencyId = "transactioncurrencyid";
            public const string UTCOffset = "utcoffset";
            public const string VersionNumber = "versionnumber";
            public const string WebSiteUrl = "websiteurl";
            public const string WorkflowSuspended = "workflowsuspended";
        }
        #endregion

        #region Relations
        public static partial class Relations
        {
            public static class OneToMany
            {
                public const string ActioncardusersettingsBusinessunit = "actioncardusersettings_businessunit";
                public const string AdxInviteredemptionBusinessunitOwningbusinessunit = "adx_inviteredemption_businessunit_owningbusinessunit";
                public const string AdxPortalcommentBusinessunitOwningbusinessunit = "adx_portalcomment_businessunit_owningbusinessunit";
                public const string BizmapBusinessidBusinessunit = "bizmap_businessid_businessunit";
                public const string BizmapSubbusinessidBusinessunit = "bizmap_subbusinessid_businessunit";
                public const string BulkDeleteOperationBusinessUnit = "BulkDeleteOperation_BusinessUnit";
                public const string BusinessUnitAccounts = "business_unit_accounts";
                public const string BusinessUnitActioncards = "business_unit_actioncards";
                public const string BusinessUnitActivityfileattachment = "business_unit_activityfileattachment";
                public const string BusinessUnitActivitypointer = "business_unit_activitypointer";
                public const string BusinessUnitAdxInvitation = "business_unit_adx_invitation";
                public const string BusinessUnitAdxSetting = "business_unit_adx_setting";
                public const string BusinessUnitAgentconversationmessage = "business_unit_agentconversationmessage";
                public const string BusinessUnitAgentconversationmessagefile = "business_unit_agentconversationmessagefile";
                public const string BusinessUnitAgentfeeditem = "business_unit_agentfeeditem";
                public const string BusinessUnitAgenthubgoal = "business_unit_agenthubgoal";
                public const string BusinessUnitAgenthubinsight = "business_unit_agenthubinsight";
                public const string BusinessUnitAgenthubmetric = "business_unit_agenthubmetric";
                public const string BusinessUnitAgenticscenario = "business_unit_agenticscenario";
                public const string BusinessUnitAgentmemory = "business_unit_agentmemory";
                public const string BusinessUnitAgentrule = "business_unit_agentrule";
                public const string BusinessUnitAgenttask = "business_unit_agenttask";
                public const string BusinessUnitAiinsightcard = "business_unit_aiinsightcard";
                public const string BusinessUnitAiplugin = "business_unit_aiplugin";
                public const string BusinessUnitAipluginauth = "business_unit_aipluginauth";
                public const string BusinessUnitAipluginconversationstarter = "business_unit_aipluginconversationstarter";
                public const string BusinessUnitAipluginconversationstartermapping = "business_unit_aipluginconversationstartermapping";
                public const string BusinessUnitAipluginexternalschema = "business_unit_aipluginexternalschema";
                public const string BusinessUnitAipluginexternalschemaproperty = "business_unit_aipluginexternalschemaproperty";
                public const string BusinessUnitAiplugingovernance = "business_unit_aiplugingovernance";
                public const string BusinessUnitAiplugingovernanceext = "business_unit_aiplugingovernanceext";
                public const string BusinessUnitAiplugininstance = "business_unit_aiplugininstance";
                public const string BusinessUnitAipluginoperation = "business_unit_aipluginoperation";
                public const string BusinessUnitAipluginoperationparameter = "business_unit_aipluginoperationparameter";
                public const string BusinessUnitAipluginoperationresponsetemplate = "business_unit_aipluginoperationresponsetemplate";
                public const string BusinessUnitAipluginusersetting = "business_unit_aipluginusersetting";
                public const string BusinessUnitAiskillconfig = "business_unit_aiskillconfig";
                public const string BusinessUnitAnnotations = "business_unit_annotations";
                public const string BusinessUnitApplicationuser = "business_unit_applicationuser";
                public const string BusinessUnitAppnotification = "business_unit_appnotification";
                public const string BusinessUnitAppointmentActivities = "business_unit_appointment_activities";
                public const string BusinessUnitApprovalprocess = "business_unit_approvalprocess";
                public const string BusinessUnitApprovalstageapproval = "business_unit_approvalstageapproval";
                public const string BusinessUnitApprovalstagecondition = "business_unit_approvalstagecondition";
                public const string BusinessUnitApprovalstageintelligent = "business_unit_approvalstageintelligent";
                public const string BusinessUnitApprovalstageorder = "business_unit_approvalstageorder";
                public const string BusinessUnitArchivecleanupinfo = "business_unit_archivecleanupinfo";
                public const string BusinessUnitArchivecleanupoperation = "business_unit_archivecleanupoperation";
                public const string BusinessUnitAsyncoperation = "business_unit_asyncoperation";
                public const string BusinessUnitBot = "business_unit_bot";
                public const string BusinessUnitBotcomponent = "business_unit_botcomponent";
                public const string BusinessUnitBotcomponentcollection = "business_unit_botcomponentcollection";
                public const string BusinessUnitBulkarchiveconfig = "business_unit_bulkarchiveconfig";
                public const string BusinessUnitBulkarchivefailuredetail = "business_unit_bulkarchivefailuredetail";
                public const string BusinessUnitBulkarchiveoperation = "business_unit_bulkarchiveoperation";
                public const string BusinessUnitBusinessprocess = "business_unit_businessprocess";
                public const string BusinessUnitCalendars = "business_unit_calendars";
                public const string BusinessUnitCanvasappextendedmetadata = "business_unit_canvasappextendedmetadata";
                public const string BusinessUnitCard = "business_unit_card";
                public const string BusinessUnitCategory = "business_unit_category";
                public const string BusinessUnitCertificatecredential = "business_unit_certificatecredential";
                public const string BusinessUnitChannelaccessprofile = "business_unit_channelaccessprofile";
                public const string BusinessUnitComment = "business_unit_comment";
                public const string BusinessUnitComputeruseagent = "business_unit_computeruseagent";
                public const string BusinessUnitConnectioninstance = "business_unit_connectioninstance";
                public const string BusinessUnitConnectionreference = "business_unit_connectionreference";
                public const string BusinessUnitConnections = "business_unit_connections";
                public const string BusinessUnitConnector = "business_unit_connector";
                public const string BusinessUnitContacts = "business_unit_contacts";
                public const string BusinessUnitConversationtranscript = "business_unit_conversationtranscript";
                public const string BusinessUnitConvertrule = "business_unit_convertrule";
                public const string BusinessUnitCopilotglossaryterm = "business_unit_copilotglossaryterm";
                public const string BusinessUnitCopilotsynonyms = "business_unit_copilotsynonyms";
                public const string BusinessUnitCredential = "business_unit_credential";
                public const string BusinessUnitCustomapi = "business_unit_customapi";
                public const string BusinessUnitCustomerRelationship = "business_unit_customer_relationship";
                public const string BusinessUnitDatalakefolder = "business_unit_datalakefolder";
                public const string BusinessUnitDesktopflowbinary = "business_unit_desktopflowbinary";
                public const string BusinessUnitDesktopflowmodule = "business_unit_desktopflowmodule";
                public const string BusinessUnitDvfilesearch = "business_unit_dvfilesearch";
                public const string BusinessUnitDvfilesearchattribute = "business_unit_dvfilesearchattribute";
                public const string BusinessUnitDvfilesearchentity = "business_unit_dvfilesearchentity";
                public const string BusinessUnitDvtablesearch = "business_unit_dvtablesearch";
                public const string BusinessUnitDvtablesearchattribute = "business_unit_dvtablesearchattribute";
                public const string BusinessUnitDvtablesearchentity = "business_unit_dvtablesearchentity";
                public const string BusinessUnitEmailActivities = "business_unit_email_activities";
                public const string BusinessUnitEmailserverprofile = "business_unit_emailserverprofile";
                public const string BusinessUnitEmailsignatures = "business_unit_emailsignatures";
                public const string BusinessUnitEnablearchivalrequest = "business_unit_enablearchivalrequest";
                public const string BusinessUnitEnvironmentvariabledefinition = "business_unit_environmentvariabledefinition";
                public const string BusinessUnitExchangesyncidmapping = "business_unit_exchangesyncidmapping";
                public const string BusinessUnitExportedexcel = "business_unit_exportedexcel";
                public const string BusinessUnitExportsolutionupload = "business_unit_exportsolutionupload";
                public const string BusinessUnitExternalparty = "business_unit_externalparty";
                public const string BusinessUnitFabricaiskill = "business_unit_fabricaiskill";
                public const string BusinessUnitFaxActivities = "business_unit_fax_activities";
                public const string BusinessUnitFeaturecontrolsetting = "business_unit_featurecontrolsetting";
                public const string BusinessUnitFederatedknowledgecitation = "business_unit_federatedknowledgecitation";
                public const string BusinessUnitFederatedknowledgeconfiguration = "business_unit_federatedknowledgeconfiguration";
                public const string BusinessUnitFederatedknowledgeentityconfiguration = "business_unit_federatedknowledgeentityconfiguration";
                public const string BusinessUnitFederatedknowledgemetadatarefresh = "business_unit_federatedknowledgemetadatarefresh";
                public const string BusinessUnitFeedback = "business_unit_feedback";
                public const string BusinessUnitFlowaggregation = "business_unit_flowaggregation";
                public const string BusinessUnitFlowcapacityassignment = "business_unit_flowcapacityassignment";
                public const string BusinessUnitFlowcredentialapplication = "business_unit_flowcredentialapplication";
                public const string BusinessUnitFlowevent = "business_unit_flowevent";
                public const string BusinessUnitFlowgroup = "business_unit_flowgroup";
                public const string BusinessUnitFlowmachine = "business_unit_flowmachine";
                public const string BusinessUnitFlowmachinegroup = "business_unit_flowmachinegroup";
                public const string BusinessUnitFlowmachineimage = "business_unit_flowmachineimage";
                public const string BusinessUnitFlowmachineimageversion = "business_unit_flowmachineimageversion";
                public const string BusinessUnitFlowmachinenetwork = "business_unit_flowmachinenetwork";
                public const string BusinessUnitFlowrun = "business_unit_flowrun";
                public const string BusinessUnitFlowsession = "business_unit_flowsession";
                public const string BusinessUnitFlowsessionbinary = "business_unit_flowsessionbinary";
                public const string BusinessUnitFlowtestsession = "business_unit_flowtestsession";
                public const string BusinessUnitFlowtrigger = "business_unit_flowtrigger";
                public const string BusinessUnitFlowtriggerinstance = "business_unit_flowtriggerinstance";
                public const string BusinessUnitFxexpression = "business_unit_fxexpression";
                public const string BusinessUnitGithubappconfig = "business_unit_githubappconfig";
                public const string BusinessUnitGoal = "business_unit_goal";
                public const string BusinessUnitGoalrollupquery = "business_unit_goalrollupquery";
                public const string BusinessUnitGovernanceconfiguration = "business_unit_governanceconfiguration";
                public const string BusinessUnitIndexedtrait = "business_unit_indexedtrait";
                public const string BusinessUnitIntelligentmemory = "business_unit_intelligentmemory";
                public const string BusinessUnitInteractionforemail = "business_unit_interactionforemail";
                public const string BusinessUnitKeyvaultreference = "business_unit_keyvaultreference";
                public const string BusinessUnitKnowledgearticle = "business_unit_knowledgearticle";
                public const string BusinessUnitKnowledgefaq = "business_unit_knowledgefaq";
                public const string BusinessUnitKnowledgesourceconsumer = "business_unit_knowledgesourceconsumer";
                public const string BusinessUnitKnowledgesourceprofile = "business_unit_knowledgesourceprofile";
                public const string BusinessUnitLetterActivities = "business_unit_letter_activities";
                public const string BusinessUnitMailbox = "business_unit_mailbox";
                public const string BusinessUnitMailmergetemplates = "business_unit_mailmergetemplates";
                public const string BusinessUnitManagedidentity = "business_unit_managedidentity";
                public const string BusinessUnitMcpprompt = "business_unit_mcpprompt";
                public const string BusinessUnitMcpresource = "business_unit_mcpresource";
                public const string BusinessUnitMcpresourcecontent = "business_unit_mcpresourcecontent";
                public const string BusinessUnitMcpserver = "business_unit_mcpserver";
                public const string BusinessUnitMcptool = "business_unit_mcptool";
                public const string BusinessUnitMsdynAibdataset = "business_unit_msdyn_aibdataset";
                public const string BusinessUnitMsdynAibdatasetfile = "business_unit_msdyn_aibdatasetfile";
                public const string BusinessUnitMsdynAibdatasetrecord = "business_unit_msdyn_aibdatasetrecord";
                public const string BusinessUnitMsdynAibdatasetscontainer = "business_unit_msdyn_aibdatasetscontainer";
                public const string BusinessUnitMsdynAibfeedbackloop = "business_unit_msdyn_aibfeedbackloop";
                public const string BusinessUnitMsdynAibfile = "business_unit_msdyn_aibfile";
                public const string BusinessUnitMsdynAibfileattacheddata = "business_unit_msdyn_aibfileattacheddata";
                public const string BusinessUnitMsdynAiconfigurationsearch = "business_unit_msdyn_aiconfigurationsearch";
                public const string BusinessUnitMsdynAidataprocessingevent = "business_unit_msdyn_aidataprocessingevent";
                public const string BusinessUnitMsdynAidocumenttemplate = "business_unit_msdyn_aidocumenttemplate";
                public const string BusinessUnitMsdynAievaluationconfiguration = "business_unit_msdyn_aievaluationconfiguration";
                public const string BusinessUnitMsdynAievaluationrun = "business_unit_msdyn_aievaluationrun";
                public const string BusinessUnitMsdynAievent = "business_unit_msdyn_aievent";
                public const string BusinessUnitMsdynAifptrainingdocument = "business_unit_msdyn_aifptrainingdocument";
                public const string BusinessUnitMsdynAimodel = "business_unit_msdyn_aimodel";
                public const string BusinessUnitMsdynAimodelcatalog = "business_unit_msdyn_aimodelcatalog";
                public const string BusinessUnitMsdynAiodimage = "business_unit_msdyn_aiodimage";
                public const string BusinessUnitMsdynAiodlabel = "business_unit_msdyn_aiodlabel";
                public const string BusinessUnitMsdynAiodtrainingboundingbox = "business_unit_msdyn_aiodtrainingboundingbox";
                public const string BusinessUnitMsdynAiodtrainingimage = "business_unit_msdyn_aiodtrainingimage";
                public const string BusinessUnitMsdynAioptimization = "business_unit_msdyn_aioptimization";
                public const string BusinessUnitMsdynAioptimizationprivatedata = "business_unit_msdyn_aioptimizationprivatedata";
                public const string BusinessUnitMsdynAitemplate = "business_unit_msdyn_aitemplate";
                public const string BusinessUnitMsdynAitestcase = "business_unit_msdyn_aitestcase";
                public const string BusinessUnitMsdynAitestcasedocument = "business_unit_msdyn_aitestcasedocument";
                public const string BusinessUnitMsdynAitestcaseinput = "business_unit_msdyn_aitestcaseinput";
                public const string BusinessUnitMsdynAitestrun = "business_unit_msdyn_aitestrun";
                public const string BusinessUnitMsdynAitestrunbatch = "business_unit_msdyn_aitestrunbatch";
                public const string BusinessUnitMsdynAnalysiscomponent = "business_unit_msdyn_analysiscomponent";
                public const string BusinessUnitMsdynAnalysisjob = "business_unit_msdyn_analysisjob";
                public const string BusinessUnitMsdynAnalysisoverride = "business_unit_msdyn_analysisoverride";
                public const string BusinessUnitMsdynAnalysisresult = "business_unit_msdyn_analysisresult";
                public const string BusinessUnitMsdynAnalysisresultdetail = "business_unit_msdyn_analysisresultdetail";
                public const string BusinessUnitMsdynBulkharvestrunlog = "business_unit_msdyn_bulkharvestrunlog";
                public const string BusinessUnitMsdynCopilotinteractions = "business_unit_msdyn_copilotinteractions";
                public const string BusinessUnitMsdynCustomcontrolextendedsettings = "business_unit_msdyn_customcontrolextendedsettings";
                public const string BusinessUnitMsdynDataflow = "business_unit_msdyn_dataflow";
                public const string BusinessUnitMsdynDataflowDatalakefolder = "business_unit_msdyn_dataflow_datalakefolder";
                public const string BusinessUnitMsdynDataflowconnectionreference = "business_unit_msdyn_dataflowconnectionreference";
                public const string BusinessUnitMsdynDataflowrefreshhistory = "business_unit_msdyn_dataflowrefreshhistory";
                public const string BusinessUnitMsdynDataflowtemplate = "business_unit_msdyn_dataflowtemplate";
                public const string BusinessUnitMsdynDataworkspace = "business_unit_msdyn_dataworkspace";
                public const string BusinessUnitMsdynDmsrequest = "business_unit_msdyn_dmsrequest";
                public const string BusinessUnitMsdynDmsrequeststatus = "business_unit_msdyn_dmsrequeststatus";
                public const string BusinessUnitMsdynDmssyncrequest = "business_unit_msdyn_dmssyncrequest";
                public const string BusinessUnitMsdynDmssyncstatus = "business_unit_msdyn_dmssyncstatus";
                public const string BusinessUnitMsdynEntitylinkchatconfiguration = "business_unit_msdyn_entitylinkchatconfiguration";
                public const string BusinessUnitMsdynEntityrefreshhistory = "business_unit_msdyn_entityrefreshhistory";
                public const string BusinessUnitMsdynFavoriteknowledgearticle = "business_unit_msdyn_favoriteknowledgearticle";
                public const string BusinessUnitMsdynFederatedarticle = "business_unit_msdyn_federatedarticle";
                public const string BusinessUnitMsdynFileupload = "business_unit_msdyn_fileupload";
                public const string BusinessUnitMsdynFlowActionapprovalmodel = "business_unit_msdyn_flow_actionapprovalmodel";
                public const string BusinessUnitMsdynFlowApproval = "business_unit_msdyn_flow_approval";
                public const string BusinessUnitMsdynFlowApprovalrequest = "business_unit_msdyn_flow_approvalrequest";
                public const string BusinessUnitMsdynFlowApprovalresponse = "business_unit_msdyn_flow_approvalresponse";
                public const string BusinessUnitMsdynFlowApprovalstep = "business_unit_msdyn_flow_approvalstep";
                public const string BusinessUnitMsdynFlowAwaitallactionapprovalmodel = "business_unit_msdyn_flow_awaitallactionapprovalmodel";
                public const string BusinessUnitMsdynFlowAwaitallapprovalmodel = "business_unit_msdyn_flow_awaitallapprovalmodel";
                public const string BusinessUnitMsdynFlowBasicapprovalmodel = "business_unit_msdyn_flow_basicapprovalmodel";
                public const string BusinessUnitMsdynFlowFlowapproval = "business_unit_msdyn_flow_flowapproval";
                public const string BusinessUnitMsdynFormmapping = "business_unit_msdyn_formmapping";
                public const string BusinessUnitMsdynFunction = "business_unit_msdyn_function";
                public const string BusinessUnitMsdynHarvesteligibilitycondition = "business_unit_msdyn_harvesteligibilitycondition";
                public const string BusinessUnitMsdynHarvestworkitem = "business_unit_msdyn_harvestworkitem";
                public const string BusinessUnitMsdynHealthcareFeedback = "business_unit_msdyn_healthcare_feedback";
                public const string BusinessUnitMsdynHistoricalcaseharvestbatch = "business_unit_msdyn_historicalcaseharvestbatch";
                public const string BusinessUnitMsdynHistoricalcaseharvestrun = "business_unit_msdyn_historicalcaseharvestrun";
                public const string BusinessUnitMsdynIntegratedsearchprovider = "business_unit_msdyn_integratedsearchprovider";
                public const string BusinessUnitMsdynInterimupdateknowledgearticle = "business_unit_msdyn_interimupdateknowledgearticle";
                public const string BusinessUnitMsdynKalanguagesetting = "business_unit_msdyn_kalanguagesetting";
                public const string BusinessUnitMsdynKbattachment = "business_unit_msdyn_kbattachment";
                public const string BusinessUnitMsdynKmfederatedsearchconfig = "business_unit_msdyn_kmfederatedsearchconfig";
                public const string BusinessUnitMsdynKnowledgearticlecustomentity = "business_unit_msdyn_knowledgearticlecustomentity";
                public const string BusinessUnitMsdynKnowledgearticleimage = "business_unit_msdyn_knowledgearticleimage";
                public const string BusinessUnitMsdynKnowledgearticletemplate = "business_unit_msdyn_knowledgearticletemplate";
                public const string BusinessUnitMsdynKnowledgeassetconfiguration = "business_unit_msdyn_knowledgeassetconfiguration";
                public const string BusinessUnitMsdynKnowledgeharvestjobrecord = "business_unit_msdyn_knowledgeharvestjobrecord";
                public const string BusinessUnitMsdynKnowledgeharvestplan = "business_unit_msdyn_knowledgeharvestplan";
                public const string BusinessUnitMsdynKnowledgeinteractioninsight = "business_unit_msdyn_knowledgeinteractioninsight";
                public const string BusinessUnitMsdynKnowledgemanagementsetting = "business_unit_msdyn_knowledgemanagementsetting";
                public const string BusinessUnitMsdynKnowledgepersonalfilter = "business_unit_msdyn_knowledgepersonalfilter";
                public const string BusinessUnitMsdynKnowledgesearchfilter = "business_unit_msdyn_knowledgesearchfilter";
                public const string BusinessUnitMsdynKnowledgesearchinsight = "business_unit_msdyn_knowledgesearchinsight";
                public const string BusinessUnitMsdynMobileapp = "business_unit_msdyn_mobileapp";
                public const string BusinessUnitMsdynObjectdetectionproduct = "business_unit_msdyn_objectdetectionproduct";
                public const string BusinessUnitMsdynOnlineshopperintention = "business_unit_msdyn_onlineshopperintention";
                public const string BusinessUnitMsdynPlan = "business_unit_msdyn_plan";
                public const string BusinessUnitMsdynPlanartifact = "business_unit_msdyn_planartifact";
                public const string BusinessUnitMsdynPlanattachment = "business_unit_msdyn_planattachment";
                public const string BusinessUnitMsdynPmanalysishistory = "business_unit_msdyn_pmanalysishistory";
                public const string BusinessUnitMsdynPmbusinessruleautomationconfig = "business_unit_msdyn_pmbusinessruleautomationconfig";
                public const string BusinessUnitMsdynPmcalendar = "business_unit_msdyn_pmcalendar";
                public const string BusinessUnitMsdynPmcalendarversion = "business_unit_msdyn_pmcalendarversion";
                public const string BusinessUnitMsdynPminferredtask = "business_unit_msdyn_pminferredtask";
                public const string BusinessUnitMsdynPmprocessextendedmetadataversion = "business_unit_msdyn_pmprocessextendedmetadataversion";
                public const string BusinessUnitMsdynPmprocesstemplate = "business_unit_msdyn_pmprocesstemplate";
                public const string BusinessUnitMsdynPmprocessusersettings = "business_unit_msdyn_pmprocessusersettings";
                public const string BusinessUnitMsdynPmprocessversion = "business_unit_msdyn_pmprocessversion";
                public const string BusinessUnitMsdynPmrecording = "business_unit_msdyn_pmrecording";
                public const string BusinessUnitMsdynPmsimulation = "business_unit_msdyn_pmsimulation";
                public const string BusinessUnitMsdynPmtab = "business_unit_msdyn_pmtab";
                public const string BusinessUnitMsdynPmtemplate = "business_unit_msdyn_pmtemplate";
                public const string BusinessUnitMsdynPmview = "business_unit_msdyn_pmview";
                public const string BusinessUnitMsdynPowerappswrapbuild = "business_unit_msdyn_powerappswrapbuild";
                public const string BusinessUnitMsdynQna = "business_unit_msdyn_qna";
                public const string BusinessUnitMsdynRichtextfile = "business_unit_msdyn_richtextfile";
                public const string BusinessUnitMsdynRtestructuredtemplateconfig = "business_unit_msdyn_rtestructuredtemplateconfig";
                public const string BusinessUnitMsdynSalesforcestructuredobject = "business_unit_msdyn_salesforcestructuredobject";
                public const string BusinessUnitMsdynSalesforcestructuredqnaconfig = "business_unit_msdyn_salesforcestructuredqnaconfig";
                public const string BusinessUnitMsdynSchedule = "business_unit_msdyn_schedule";
                public const string BusinessUnitMsdynServiceconfiguration = "business_unit_msdyn_serviceconfiguration";
                public const string BusinessUnitMsdynSlakpi = "business_unit_msdyn_slakpi";
                public const string BusinessUnitMsdynSolutionhealthrule = "business_unit_msdyn_solutionhealthrule";
                public const string BusinessUnitMsdynSolutionhealthruleargument = "business_unit_msdyn_solutionhealthruleargument";
                public const string BusinessUnitMsdynVirtualtablecolumncandidate = "business_unit_msdyn_virtualtablecolumncandidate";
                public const string BusinessUnitMsdynceBotcontent = "business_unit_msdynce_botcontent";
                public const string BusinessUnitMsecCleanup = "business_unit_msec_cleanup";
                public const string BusinessUnitMspcatCatalogsubmissionfiles = "business_unit_mspcat_catalogsubmissionfiles";
                public const string BusinessUnitMspcatPackagestore = "business_unit_mspcat_packagestore";
                public const string BusinessUnitNewEventaggregator = "business_unit_new_eventaggregator";
                public const string BusinessUnitNewEventaggregatorscans = "business_unit_new_eventaggregatorscans";
                public const string BusinessUnitNewGaurdianfullscan = "business_unit_new_gaurdianfullscan";
                public const string BusinessUnitNewGaurdianhealthchecks = "business_unit_new_gaurdianhealthchecks";
                public const string BusinessUnitNlsqregistration = "business_unit_nlsqregistration";
                public const string BusinessUnitOfficedocument = "business_unit_officedocument";
                public const string BusinessUnitParentBusinessUnit = "business_unit_parent_business_unit";
                public const string BusinessUnitPdfsetting = "business_unit_pdfsetting";
                public const string BusinessUnitPersonaldocumenttemplates = "business_unit_personaldocumenttemplates";
                public const string BusinessUnitPhoneCallActivities = "business_unit_phone_call_activities";
                public const string BusinessUnitPlannerbusinessscenario = "business_unit_plannerbusinessscenario";
                public const string BusinessUnitPlannersyncaction = "business_unit_plannersyncaction";
                public const string BusinessUnitPlugin = "business_unit_plugin";
                public const string BusinessUnitPostfollows = "business_unit_postfollows";
                public const string BusinessUnitPostRegarding = "business_unit_PostRegarding";
                public const string BusinessUnitPowerfxrule = "business_unit_powerfxrule";
                public const string BusinessUnitPowerpagecomponent = "business_unit_powerpagecomponent";
                public const string BusinessUnitPowerpagesddosalert = "business_unit_powerpagesddosalert";
                public const string BusinessUnitPowerpagesite = "business_unit_powerpagesite";
                public const string BusinessUnitPowerpagesitelanguage = "business_unit_powerpagesitelanguage";
                public const string BusinessUnitPowerpagesitepublished = "business_unit_powerpagesitepublished";
                public const string BusinessUnitPowerpageslog = "business_unit_powerpageslog";
                public const string BusinessUnitPowerpagesmanagedidentity = "business_unit_powerpagesmanagedidentity";
                public const string BusinessUnitPowerpagesscanreport = "business_unit_powerpagesscanreport";
                public const string BusinessUnitPowerpagessiteaifeedback = "business_unit_powerpagessiteaifeedback";
                public const string BusinessUnitPowerpagessourcefile = "business_unit_powerpagessourcefile";
                public const string BusinessUnitPrivilegecheckerrun = "business_unit_privilegecheckerrun";
                public const string BusinessUnitProcessorregistration = "business_unit_processorregistration";
                public const string BusinessUnitProcessstageparameter = "business_unit_processstageparameter";
                public const string BusinessUnitProfilerule = "business_unit_profilerule";
                public const string BusinessUnitQueues = "business_unit_queues";
                public const string BusinessUnitQueues2 = "business_unit_queues2";
                public const string BusinessUnitRecentlyused = "business_unit_recentlyused";
                public const string BusinessUnitReconciliationentityinfo = "business_unit_reconciliationentityinfo";
                public const string BusinessUnitReconciliationentitystepinfo = "business_unit_reconciliationentitystepinfo";
                public const string BusinessUnitReconciliationinfo = "business_unit_reconciliationinfo";
                public const string BusinessUnitRecurrencerule = "business_unit_recurrencerule";
                public const string BusinessUnitRecurringappointmentmasterActivities = "business_unit_recurringappointmentmaster_activities";
                public const string BusinessUnitReports = "business_unit_reports";
                public const string BusinessUnitRetaineddataexcel = "business_unit_retaineddataexcel";
                public const string BusinessUnitRetentioncleanupinfo = "business_unit_retentioncleanupinfo";
                public const string BusinessUnitRetentioncleanupoperation = "business_unit_retentioncleanupoperation";
                public const string BusinessUnitRetentionconfig = "business_unit_retentionconfig";
                public const string BusinessUnitRetentionfailuredetail = "business_unit_retentionfailuredetail";
                public const string BusinessUnitRetentionoperation = "business_unit_retentionoperation";
                public const string BusinessUnitRetentionsuccessdetail = "business_unit_retentionsuccessdetail";
                public const string BusinessUnitRoles = "business_unit_roles";
                public const string BusinessUnitRoutingrule = "business_unit_routingrule";
                public const string BusinessUnitSavingrule = "business_unit_savingrule";
                public const string BusinessUnitSharepointdocument = "business_unit_sharepointdocument";
                public const string BusinessUnitSharepointdocument2 = "business_unit_sharepointdocument2";
                public const string BusinessUnitSharepointdocumentlocation = "business_unit_sharepointdocumentlocation";
                public const string BusinessUnitSharepointsites = "business_unit_sharepointsites";
                public const string BusinessUnitSideloadedaiplugin = "business_unit_sideloadedaiplugin";
                public const string BusinessUnitSignal = "business_unit_signal";
                public const string BusinessUnitSignalregistration = "business_unit_signalregistration";
                public const string BusinessUnitSkill = "business_unit_skill";
                public const string BusinessUnitSlabase = "business_unit_slabase";
                public const string BusinessUnitSlakpiinstance = "business_unit_slakpiinstance";
                public const string BusinessUnitSocialactivity = "business_unit_socialactivity";
                public const string BusinessUnitSocialprofiles = "business_unit_socialprofiles";
                public const string BusinessUnitSolutioncomponentbatchconfiguration = "business_unit_solutioncomponentbatchconfiguration";
                public const string BusinessUnitStagesolutionupload = "business_unit_stagesolutionupload";
                public const string BusinessUnitSynapsedatabase = "business_unit_synapsedatabase";
                public const string BusinessUnitSystemUsers = "business_unit_system_users";
                public const string BusinessUnitTag = "business_unit_tag";
                public const string BusinessUnitTaggedflowsession = "business_unit_taggedflowsession";
                public const string BusinessUnitTaggedprocess = "business_unit_taggedprocess";
                public const string BusinessUnitTaskActivities = "business_unit_task_activities";
                public const string BusinessUnitTdsmetadata = "business_unit_tdsmetadata";
                public const string BusinessUnitTeams = "business_unit_teams";
                public const string BusinessUnitTemplates = "business_unit_templates";
                public const string BusinessUnitToolinggateway = "business_unit_toolinggateway";
                public const string BusinessUnitToolinggatewaymcpserver = "business_unit_toolinggatewaymcpserver";
                public const string BusinessUnitTraceRegarding = "business_unit_TraceRegarding";
                public const string BusinessUnitTrait = "business_unit_trait";
                public const string BusinessUnitTraitregistration = "business_unit_traitregistration";
                public const string BusinessUnitUnstructuredfilesearchentity = "business_unit_unstructuredfilesearchentity";
                public const string BusinessUnitUnstructuredfilesearchrecord = "business_unit_unstructuredfilesearchrecord";
                public const string BusinessUnitUnstructuredfilesearchrecordstatus = "business_unit_unstructuredfilesearchrecordstatus";
                public const string BusinessUnitUntrackedemailActivities = "business_unit_untrackedemail_activities";
                public const string BusinessUnitUserSettings = "business_unit_user_settings";
                public const string BusinessUnitUserapplicationmetadata = "business_unit_userapplicationmetadata";
                public const string BusinessUnitUserform = "business_unit_userform";
                public const string BusinessUnitUserquery = "business_unit_userquery";
                public const string BusinessUnitUserqueryvisualizations = "business_unit_userqueryvisualizations";
                public const string BusinessUnitUxagentcomponent = "business_unit_uxagentcomponent";
                public const string BusinessUnitUxagentcomponentrevision = "business_unit_uxagentcomponentrevision";
                public const string BusinessUnitWorkflow = "business_unit_workflow";
                public const string BusinessUnitWorkflowbinary = "business_unit_workflowbinary";
                public const string BusinessUnitWorkflowlogs = "business_unit_workflowlogs";
                public const string BusinessUnitWorkflowmetadata = "business_unit_workflowmetadata";
                public const string BusinessUnitWorkqueue = "business_unit_workqueue";
                public const string BusinessUnitWorkqueueitem = "business_unit_workqueueitem";
                public const string BusinessUnitAsyncOperations = "BusinessUnit_AsyncOperations";
                public const string BusinessUnitBulkDeleteFailures = "BusinessUnit_BulkDeleteFailures";
                public const string BusinessunitCallbackregistration = "businessunit_callbackregistration";
                public const string BusinessunitCanvasapp = "businessunit_canvasapp";
                public const string BusinessUnitDuplicateRules = "BusinessUnit_DuplicateRules";
                public const string BusinessUnitImportData = "BusinessUnit_ImportData";
                public const string BusinessUnitImportFiles = "BusinessUnit_ImportFiles";
                public const string BusinessUnitImportLogs = "BusinessUnit_ImportLogs";
                public const string BusinessUnitImportMaps = "BusinessUnit_ImportMaps";
                public const string BusinessUnitImports = "BusinessUnit_Imports";
                public const string BusinessunitInternalAddresses = "businessunit_internal_addresses";
                public const string BusinessunitMailboxtrackingcategory = "businessunit_mailboxtrackingcategory";
                public const string BusinessunitMailboxtrackingfolder = "businessunit_mailboxtrackingfolder";
                public const string BusinessunitPrincipalobjectattributeaccess = "businessunit_principalobjectattributeaccess";
                public const string BusinessUnitProcessSessions = "BusinessUnit_ProcessSessions";
                public const string BusinessUnitSyncError = "BusinessUnit_SyncError";
                public const string BusinessUnitSyncErrors = "BusinessUnit_SyncErrors";
                public const string ChatBusinessunitOwningbusinessunit = "chat_businessunit_owningbusinessunit";
                public const string LkUserfiscalcalendarBusinessunit = "lk_userfiscalcalendar_businessunit";
                public const string OwningBusinessunitProcesssessions = "Owning_businessunit_processsessions";
                public const string SystemuserbusinessunitentitymapBusinessunitidBusinessunit = "systemuserbusinessunitentitymap_businessunitid_businessunit";
                public const string UserentityinstancedataBusinessunit = "userentityinstancedata_businessunit";
                public const string UserentityuisettingsBusinessunit = "userentityuisettings_businessunit";
            }

            public static partial class ManyToOne
            {
                public const string BusinessUnitParentBusinessUnit = "business_unit_parent_business_unit";
                public const string BusinessUnitCalendar = "BusinessUnit_Calendar";
                public const string LkBusinessunitCreatedonbehalfby = "lk_businessunit_createdonbehalfby";
                public const string LkBusinessunitModifiedonbehalfby = "lk_businessunit_modifiedonbehalfby";
                public const string LkBusinessunitbaseCreatedby = "lk_businessunitbase_createdby";
                public const string LkBusinessunitbaseModifiedby = "lk_businessunitbase_modifiedby";
                public const string OrganizationBusinessUnits = "organization_business_units";
                public const string TransactionCurrencyBusinessUnit = "TransactionCurrency_BusinessUnit";
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

        public static BusinessUnit Retrieve(IOrganizationService service, Guid id)
        {
            return Retrieve(service, id, new ColumnSet(true));
        }

        public static BusinessUnit Retrieve(IOrganizationService service, Guid id, ColumnSet columnSet)
        {
            return service.Retrieve("businessunit", id, columnSet).ToEntity<BusinessUnit>();
        }

        public BusinessUnit GetChangedEntity()
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
            return new BusinessUnit(Id) { Attributes = attr };
        }
        #endregion
    }

    #region Context
    public partial class DataContext
    {
        public IQueryable<BusinessUnit> BusinessUnitSet
        {
            get
            {
                return CreateQuery<BusinessUnit>();
            }
        }
    }
    #endregion
}
