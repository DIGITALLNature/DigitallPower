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
	/// Person with access to the Microsoft CRM system and who owns objects in the Microsoft CRM database.
	/// </summary>
    [DataContract]
    [EntityLogicalName("systemuser")]
    [System.CodeDom.Compiler.GeneratedCode("dgtp", "2026")]
    [ExcludeFromCodeCoverage]
    public partial class SystemUser : Entity, INotifyPropertyChanging, INotifyPropertyChanged
    {
        #region ctor
        [DebuggerNonUserCode]
        public SystemUser() : this(false)
        {
        }
        [DebuggerNonUserCode]
        public SystemUser(bool trackChanges = false) : base(EntityLogicalName)
        {
            _trackChanges = trackChanges;
        }
        [DebuggerNonUserCode]
        public SystemUser(Guid id, bool trackChanges = false) : base(EntityLogicalName, id)
        {
            _trackChanges = trackChanges;
        }
        [DebuggerNonUserCode]
        public SystemUser(KeyAttributeCollection keyAttributes, bool trackChanges = false) : base(EntityLogicalName, keyAttributes)
        {
            _trackChanges = trackChanges;
        }
        [DebuggerNonUserCode]
        public SystemUser(string keyName, object keyValue, bool trackChanges = false) : base(EntityLogicalName, keyName, keyValue)
        {
            _trackChanges = trackChanges;
        }
        #endregion

        #region fields
        private readonly bool _trackChanges;
        private readonly Lazy<HashSet<string>> _changedProperties = new();
        #endregion

        #region consts
        public const string EntityLogicalName = "systemuser";
        public const string PrimaryNameAttribute = "fullname";
        public const int EntityTypeCode = 8;
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
        [AttributeLogicalName("systemuserid")]
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
                SystemUserId = value;
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
		/// Unique identifier for the user.
		/// </summary>
        [AttributeLogicalName("systemuserid")]
        public Guid? SystemUserId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<Guid?>("systemuserid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("systemuserid", value);
                base.Id = value.HasValue ? value.Value : Guid.Empty;
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Type of user.
		/// </summary>
        [AttributeLogicalName("accessmode")]
        public OptionSetValue? AccessMode
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("accessmode");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("accessmode", value);
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
		/// Shows the complete primary address.
		/// </summary>
        [AttributeLogicalName("address1_composite")]
        public string? Address1Composite
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("address1_composite");
            }
        }

        /// <summary>
		/// Country/region name in address 1.
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
		/// Shows the complete secondary address.
		/// </summary>
        [AttributeLogicalName("address2_composite")]
        public string? Address2Composite
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("address2_composite");
            }
        }

        /// <summary>
		/// Country/region name in address 2.
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
		/// The identifier for the application. This is used to access data in another application.
		/// </summary>
        [AttributeLogicalName("applicationid")]
        public Guid? ApplicationId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<Guid?>("applicationid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("applicationid", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// The URI used as a unique logical identifier for the external app. This can be used to validate the application.
		/// </summary>
        [AttributeLogicalName("applicationiduri")]
        public string? ApplicationIdUri
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("applicationiduri");
            }
        }

        /// <summary>
		/// This is the application directory object Id.
		/// </summary>
        [AttributeLogicalName("azureactivedirectoryobjectid")]
        public Guid? AzureActiveDirectoryObjectId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<Guid?>("azureactivedirectoryobjectid");
            }
        }

        /// <summary>
		/// Date and time when the user was set as soft deleted in Azure.
		/// </summary>
        [AttributeLogicalName("azuredeletedon")]
        public DateTime? AzureDeletedOn
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<DateTime?>("azuredeletedon");
            }
        }

        /// <summary>
		/// Azure state of user
		/// </summary>
        [AttributeLogicalName("azurestate")]
        public OptionSetValue? AzureState
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("azurestate");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("azurestate", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier of the business unit with which the user is associated.
		/// </summary>
        [AttributeLogicalName("businessunitid")]
        public EntityReference? BusinessUnitId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<EntityReference?>("businessunitid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("businessunitid", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Fiscal calendar associated with the user.
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
		/// License type of user. This is used only in the on-premises version of the product. Online licenses are managed through Microsoft 365 Office Portal
		/// </summary>
        [AttributeLogicalName("caltype")]
        public OptionSetValue? CALType
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("caltype");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("caltype", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier of the user who created the user.
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
		/// Date and time when the user was created.
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
		/// Unique identifier of the delegate user who created the systemuser.
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
		/// Indicates if default outlook filters have been populated.
		/// </summary>
        [AttributeLogicalName("defaultfilterspopulated")]
        public bool? DefaultFiltersPopulated
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("defaultfilterspopulated");
            }
        }

        /// <summary>
		/// Select the mailbox associated with this user.
		/// </summary>
        [AttributeLogicalName("defaultmailbox")]
        public EntityReference? DefaultMailbox
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<EntityReference?>("defaultmailbox");
            }
        }

        /// <summary>
		/// Type a default folder name for the user's OneDrive For Business location.
		/// </summary>
        [AttributeLogicalName("defaultodbfoldername")]
        public string? DefaultOdbFolderName
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("defaultodbfoldername");
            }
        }

        /// <summary>
		/// User delete state
		/// </summary>
        [AttributeLogicalName("deletedstate")]
        public OptionSetValue? DeletedState
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("deletedstate");
            }
        }

        /// <summary>
		/// Reason for disabling the user.
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
		/// Whether to display the user in service views.
		/// </summary>
        [AttributeLogicalName("displayinserviceviews")]
        public bool? DisplayInServiceViews
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("displayinserviceviews");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("displayinserviceviews", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Active Directory domain of which the user is a member.
		/// </summary>
        [AttributeLogicalName("domainname")]
        public string? DomainName
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("domainname");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("domainname", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Shows the status of the primary email address.
		/// </summary>
        [AttributeLogicalName("emailrouteraccessapproval")]
        public OptionSetValue? EmailRouterAccessApproval
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("emailrouteraccessapproval");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("emailrouteraccessapproval", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Employee identifier for the user.
		/// </summary>
        [AttributeLogicalName("employeeid")]
        public string? EmployeeId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("employeeid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("employeeid", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Shows the default image for the record.
		/// </summary>
        [AttributeLogicalName("entityimage")]
        public byte[]? EntityImage
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<byte[]?>("entityimage");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("entityimage", value);
                OnPropertyChanged();
            }
        }

        
        [AttributeLogicalName("entityimage_timestamp")]
        public long? EntityImageTimestamp
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<long?>("entityimage_timestamp");
            }
        }

        
        [AttributeLogicalName("entityimage_url")]
        public string? EntityImageURL
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("entityimage_url");
            }
        }

        /// <summary>
		/// For internal use only.
		/// </summary>
        [AttributeLogicalName("entityimageid")]
        public Guid? EntityImageId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<Guid?>("entityimageid");
            }
        }

        /// <summary>
		/// Exchange rate for the currency associated with the systemuser with respect to the base currency.
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
		/// First name of the user.
		/// </summary>
        [AttributeLogicalName("firstname")]
        public string? FirstName
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("firstname");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("firstname", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Full name of the user.
		/// </summary>
        [AttributeLogicalName("fullname")]
        public string? FullName
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("fullname");
            }
        }

        /// <summary>
		/// Government identifier for the user.
		/// </summary>
        [AttributeLogicalName("governmentid")]
        public string? GovernmentId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("governmentid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("governmentid", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Home phone number for the user.
		/// </summary>
        [AttributeLogicalName("homephone")]
        public string? HomePhone
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("homephone");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("homephone", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// For internal use only.
		/// </summary>
        [AttributeLogicalName("identityid")]
        public int? IdentityId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("identityid");
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
		/// Incoming email delivery method for the user.
		/// </summary>
        [AttributeLogicalName("incomingemaildeliverymethod")]
        public OptionSetValue? IncomingEmailDeliveryMethod
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("incomingemaildeliverymethod");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("incomingemaildeliverymethod", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Internal email address for the user.
		/// </summary>
        [AttributeLogicalName("internalemailaddress")]
        public string? InternalEMailAddress
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("internalemailaddress");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("internalemailaddress", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// User invitation status.
		/// </summary>
        [AttributeLogicalName("invitestatuscode")]
        public OptionSetValue? InviteStatusCode
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("invitestatuscode");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("invitestatuscode", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Bypasses the selected user from IP firewall restriction
		/// </summary>
        [AttributeLogicalName("isallowedbyipfirewall")]
        public bool? IsAllowedByIpFirewall
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isallowedbyipfirewall");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("isallowedbyipfirewall", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Information about whether the user is enabled.
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
		/// Shows the status of approval of the email address by O365 Admin.
		/// </summary>
        [AttributeLogicalName("isemailaddressapprovedbyo365admin")]
        public bool? IsEmailAddressApprovedByO365Admin
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isemailaddressapprovedbyo365admin");
            }
        }

        /// <summary>
		/// Check if user is an integration user.
		/// </summary>
        [AttributeLogicalName("isintegrationuser")]
        public bool? IsIntegrationUser
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isintegrationuser");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("isintegrationuser", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Information about whether the user is licensed.
		/// </summary>
        [AttributeLogicalName("islicensed")]
        public bool? IsLicensed
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("islicensed");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("islicensed", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Information about whether the user is synced with the directory.
		/// </summary>
        [AttributeLogicalName("issyncwithdirectory")]
        public bool? IsSyncWithDirectory
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("issyncwithdirectory");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("issyncwithdirectory", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Job title of the user.
		/// </summary>
        [AttributeLogicalName("jobtitle")]
        public string? JobTitle
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("jobtitle");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("jobtitle", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Last name of the user.
		/// </summary>
        [AttributeLogicalName("lastname")]
        public string? LastName
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("lastname");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("lastname", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Middle name of the user.
		/// </summary>
        [AttributeLogicalName("middlename")]
        public string? MiddleName
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("middlename");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("middlename", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Mobile alert email address for the user.
		/// </summary>
        [AttributeLogicalName("mobilealertemail")]
        public string? MobileAlertEMail
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("mobilealertemail");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("mobilealertemail", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Items contained with a particular SystemUser.
		/// </summary>
        [AttributeLogicalName("mobileofflineprofileid")]
        public EntityReference? MobileOfflineProfileId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<EntityReference?>("mobileofflineprofileid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("mobileofflineprofileid", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Mobile phone number for the user.
		/// </summary>
        [AttributeLogicalName("mobilephone")]
        public string? MobilePhone
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("mobilephone");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("mobilephone", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier of the user who last modified the user.
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
		/// Date and time when the user was last modified.
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
		/// Unique identifier of the delegate user who last modified the systemuser.
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
		/// Nickname of the user.
		/// </summary>
        [AttributeLogicalName("nickname")]
        public string? NickName
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("nickname");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("nickname", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier of the organization associated with the user.
		/// </summary>
        [AttributeLogicalName("organizationid")]
        public Guid? OrganizationId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<Guid?>("organizationid");
            }
        }

        /// <summary>
		/// Outgoing email delivery method for the user.
		/// </summary>
        [AttributeLogicalName("outgoingemaildeliverymethod")]
        public OptionSetValue? OutgoingEmailDeliveryMethod
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("outgoingemaildeliverymethod");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("outgoingemaildeliverymethod", value);
                OnPropertyChanged();
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
		/// Unique identifier of the manager of the user.
		/// </summary>
        [AttributeLogicalName("parentsystemuserid")]
        public EntityReference? ParentSystemUserId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<EntityReference?>("parentsystemuserid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("parentsystemuserid", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// For internal use only.
		/// </summary>
        [AttributeLogicalName("passporthi")]
        public int? PassportHi
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("passporthi");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("passporthi", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// For internal use only.
		/// </summary>
        [AttributeLogicalName("passportlo")]
        public int? PassportLo
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("passportlo");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("passportlo", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Personal email address of the user.
		/// </summary>
        [AttributeLogicalName("personalemailaddress")]
        public string? PersonalEMailAddress
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("personalemailaddress");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("personalemailaddress", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// URL for the Website on which a photo of the user is located.
		/// </summary>
        [AttributeLogicalName("photourl")]
        public string? PhotoUrl
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("photourl");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("photourl", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// User's position in hierarchical security model.
		/// </summary>
        [AttributeLogicalName("positionid")]
        public EntityReference? PositionId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<EntityReference?>("positionid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("positionid", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Preferred address for the user.
		/// </summary>
        [AttributeLogicalName("preferredaddresscode")]
        public OptionSetValue? PreferredAddressCode
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("preferredaddresscode");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("preferredaddresscode", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Preferred email address for the user.
		/// </summary>
        [AttributeLogicalName("preferredemailcode")]
        public OptionSetValue? PreferredEmailCode
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("preferredemailcode");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("preferredemailcode", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Preferred phone number for the user.
		/// </summary>
        [AttributeLogicalName("preferredphonecode")]
        public OptionSetValue? PreferredPhoneCode
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("preferredphonecode");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("preferredphonecode", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Shows the ID of the process.
		/// </summary>
        [AttributeLogicalName("processid")]
        public Guid? ProcessId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<Guid?>("processid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("processid", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier of the default queue for the user.
		/// </summary>
        [AttributeLogicalName("queueid")]
        public EntityReference? QueueId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<EntityReference?>("queueid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("queueid", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Salutation for correspondence with the user.
		/// </summary>
        [AttributeLogicalName("salutation")]
        public string? Salutation
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("salutation");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("salutation", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Check if user is a setup user.
		/// </summary>
        [AttributeLogicalName("setupuser")]
        public bool? SetupUser
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("setupuser");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("setupuser", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// SharePoint Work Email Address
		/// </summary>
        [AttributeLogicalName("sharepointemailaddress")]
        public string? SharePointEmailAddress
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("sharepointemailaddress");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("sharepointemailaddress", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Skill set of the user.
		/// </summary>
        [AttributeLogicalName("skills")]
        public string? Skills
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("skills");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("skills", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Shows the ID of the stage.
		/// </summary>
        [AttributeLogicalName("stageid")]
        public Guid? StageId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<Guid?>("stageid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("stageid", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// The type of user
		/// </summary>
        [AttributeLogicalName("systemmanagedusertype")]
        public OptionSetValue? SystemManagedUserType
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("systemmanagedusertype");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("systemmanagedusertype", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier of the territory to which the user is assigned.
		/// </summary>
        [AttributeLogicalName("territoryid")]
        public EntityReference? TerritoryId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<EntityReference?>("territoryid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("territoryid", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// For internal use only.
		/// </summary>
        [AttributeLogicalName("timezoneruleversionnumber")]
        public int? TimeZoneRuleVersionNumber
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("timezoneruleversionnumber");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("timezoneruleversionnumber", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Title of the user.
		/// </summary>
        [AttributeLogicalName("title")]
        public string? Title
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("title");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("title", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier of the currency associated with the systemuser.
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
		/// For internal use only.
		/// </summary>
        [AttributeLogicalName("traversedpath")]
        public string? TraversedPath
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("traversedpath");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("traversedpath", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Shows the type of user license.
		/// </summary>
        [AttributeLogicalName("userlicensetype")]
        public int? UserLicenseType
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("userlicensetype");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("userlicensetype", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// User PUID User Identifiable Information
		/// </summary>
        [AttributeLogicalName("userpuid")]
        public string? UserPuid
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("userpuid");
            }
        }

        /// <summary>
		/// Time zone code that was in use when the record was created.
		/// </summary>
        [AttributeLogicalName("utcconversiontimezonecode")]
        public int? UTCConversionTimeZoneCode
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("utcconversiontimezonecode");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("utcconversiontimezonecode", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Version number of the user.
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
		/// Windows Live ID
		/// </summary>
        [AttributeLogicalName("windowsliveid")]
        public string? WindowsLiveID
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("windowsliveid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("windowsliveid", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// User's Yammer login email address
		/// </summary>
        [AttributeLogicalName("yammeremailaddress")]
        public string? YammerEmailAddress
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("yammeremailaddress");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("yammeremailaddress", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// User's Yammer ID
		/// </summary>
        [AttributeLogicalName("yammeruserid")]
        public string? YammerUserId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("yammeruserid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("yammeruserid", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Pronunciation of the first name of the user, written in phonetic hiragana or katakana characters.
		/// </summary>
        [AttributeLogicalName("yomifirstname")]
        public string? YomiFirstName
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("yomifirstname");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("yomifirstname", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Pronunciation of the full name of the user, written in phonetic hiragana or katakana characters.
		/// </summary>
        [AttributeLogicalName("yomifullname")]
        public string? YomiFullName
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("yomifullname");
            }
        }

        /// <summary>
		/// Pronunciation of the last name of the user, written in phonetic hiragana or katakana characters.
		/// </summary>
        [AttributeLogicalName("yomilastname")]
        public string? YomiLastName
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("yomilastname");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("yomilastname", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Pronunciation of the middle name of the user, written in phonetic hiragana or katakana characters.
		/// </summary>
        [AttributeLogicalName("yomimiddlename")]
        public string? YomiMiddleName
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("yomimiddlename");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("yomimiddlename", value);
                OnPropertyChanged();
            }
        }
        #endregion

        #region NavigationProperties

        /// <summary>
        /// 1:N contact_owning_user
        /// </summary>
        [RelationshipSchemaName("contact_owning_user")]
        public IEnumerable<Contact> ContactOwningUser
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<Contact>("contact_owning_user", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("contact_owning_user", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N createdby_pluginassembly
        /// </summary>
        [RelationshipSchemaName("createdby_pluginassembly")]
        public IEnumerable<PluginAssembly> CreatedbyPluginassembly
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<PluginAssembly>("createdby_pluginassembly", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("createdby_pluginassembly", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N createdby_plugintype
        /// </summary>
        [RelationshipSchemaName("createdby_plugintype")]
        public IEnumerable<PluginType> CreatedbyPlugintype
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<PluginType>("createdby_plugintype", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("createdby_plugintype", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N createdby_sdkmessage
        /// </summary>
        [RelationshipSchemaName("createdby_sdkmessage")]
        public IEnumerable<SdkMessage> CreatedbySdkmessage
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<SdkMessage>("createdby_sdkmessage", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("createdby_sdkmessage", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N createdby_sdkmessagefilter
        /// </summary>
        [RelationshipSchemaName("createdby_sdkmessagefilter")]
        public IEnumerable<SdkMessageFilter> CreatedbySdkmessagefilter
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<SdkMessageFilter>("createdby_sdkmessagefilter", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("createdby_sdkmessagefilter", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N createdby_sdkmessageprocessingstep
        /// </summary>
        [RelationshipSchemaName("createdby_sdkmessageprocessingstep")]
        public IEnumerable<SdkMessageProcessingStep> CreatedbySdkmessageprocessingstep
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<SdkMessageProcessingStep>("createdby_sdkmessageprocessingstep", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("createdby_sdkmessageprocessingstep", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N createdby_sdkmessageprocessingstepimage
        /// </summary>
        [RelationshipSchemaName("createdby_sdkmessageprocessingstepimage")]
        public IEnumerable<SdkMessageProcessingStepImage> CreatedbySdkmessageprocessingstepimage
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<SdkMessageProcessingStepImage>("createdby_sdkmessageprocessingstepimage", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("createdby_sdkmessageprocessingstepimage", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N createdby_sdkmessageprocessingstepsecureconfig
        /// </summary>
        [RelationshipSchemaName("createdby_sdkmessageprocessingstepsecureconfig")]
        public IEnumerable<SdkMessageProcessingStepSecureConfig> CreatedbySdkmessageprocessingstepsecureconfig
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<SdkMessageProcessingStepSecureConfig>("createdby_sdkmessageprocessingstepsecureconfig", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("createdby_sdkmessageprocessingstepsecureconfig", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N impersonatinguserid_sdkmessageprocessingstep
        /// </summary>
        [RelationshipSchemaName("impersonatinguserid_sdkmessageprocessingstep")]
        public IEnumerable<SdkMessageProcessingStep> ImpersonatinguseridSdkmessageprocessingstep
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<SdkMessageProcessingStep>("impersonatinguserid_sdkmessageprocessingstep", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("impersonatinguserid_sdkmessageprocessingstep", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_accountbase_createdby
        /// </summary>
        [RelationshipSchemaName("lk_accountbase_createdby")]
        public IEnumerable<Account> LkAccountbaseCreatedby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<Account>("lk_accountbase_createdby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_accountbase_createdby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_accountbase_createdonbehalfby
        /// </summary>
        [RelationshipSchemaName("lk_accountbase_createdonbehalfby")]
        public IEnumerable<Account> LkAccountbaseCreatedonbehalfby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<Account>("lk_accountbase_createdonbehalfby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_accountbase_createdonbehalfby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_accountbase_modifiedby
        /// </summary>
        [RelationshipSchemaName("lk_accountbase_modifiedby")]
        public IEnumerable<Account> LkAccountbaseModifiedby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<Account>("lk_accountbase_modifiedby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_accountbase_modifiedby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_accountbase_modifiedonbehalfby
        /// </summary>
        [RelationshipSchemaName("lk_accountbase_modifiedonbehalfby")]
        public IEnumerable<Account> LkAccountbaseModifiedonbehalfby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<Account>("lk_accountbase_modifiedonbehalfby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_accountbase_modifiedonbehalfby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_asyncoperation_createdby
        /// </summary>
        [RelationshipSchemaName("lk_asyncoperation_createdby")]
        public IEnumerable<AsyncOperation> LkAsyncoperationCreatedby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<AsyncOperation>("lk_asyncoperation_createdby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_asyncoperation_createdby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_asyncoperation_createdonbehalfby
        /// </summary>
        [RelationshipSchemaName("lk_asyncoperation_createdonbehalfby")]
        public IEnumerable<AsyncOperation> LkAsyncoperationCreatedonbehalfby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<AsyncOperation>("lk_asyncoperation_createdonbehalfby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_asyncoperation_createdonbehalfby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_asyncoperation_modifiedby
        /// </summary>
        [RelationshipSchemaName("lk_asyncoperation_modifiedby")]
        public IEnumerable<AsyncOperation> LkAsyncoperationModifiedby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<AsyncOperation>("lk_asyncoperation_modifiedby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_asyncoperation_modifiedby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_asyncoperation_modifiedonbehalfby
        /// </summary>
        [RelationshipSchemaName("lk_asyncoperation_modifiedonbehalfby")]
        public IEnumerable<AsyncOperation> LkAsyncoperationModifiedonbehalfby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<AsyncOperation>("lk_asyncoperation_modifiedonbehalfby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_asyncoperation_modifiedonbehalfby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_businessunit_createdonbehalfby
        /// </summary>
        [RelationshipSchemaName("lk_businessunit_createdonbehalfby")]
        public IEnumerable<BusinessUnit> LkBusinessunitCreatedonbehalfby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<BusinessUnit>("lk_businessunit_createdonbehalfby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_businessunit_createdonbehalfby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_businessunit_modifiedonbehalfby
        /// </summary>
        [RelationshipSchemaName("lk_businessunit_modifiedonbehalfby")]
        public IEnumerable<BusinessUnit> LkBusinessunitModifiedonbehalfby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<BusinessUnit>("lk_businessunit_modifiedonbehalfby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_businessunit_modifiedonbehalfby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_businessunitbase_createdby
        /// </summary>
        [RelationshipSchemaName("lk_businessunitbase_createdby")]
        public IEnumerable<BusinessUnit> LkBusinessunitbaseCreatedby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<BusinessUnit>("lk_businessunitbase_createdby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_businessunitbase_createdby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_businessunitbase_modifiedby
        /// </summary>
        [RelationshipSchemaName("lk_businessunitbase_modifiedby")]
        public IEnumerable<BusinessUnit> LkBusinessunitbaseModifiedby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<BusinessUnit>("lk_businessunitbase_modifiedby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_businessunitbase_modifiedby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_calendar_createdby
        /// </summary>
        [RelationshipSchemaName("lk_calendar_createdby")]
        public IEnumerable<Calendar> LkCalendarCreatedby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<Calendar>("lk_calendar_createdby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_calendar_createdby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_calendar_createdonbehalfby
        /// </summary>
        [RelationshipSchemaName("lk_calendar_createdonbehalfby")]
        public IEnumerable<Calendar> LkCalendarCreatedonbehalfby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<Calendar>("lk_calendar_createdonbehalfby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_calendar_createdonbehalfby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_calendar_modifiedby
        /// </summary>
        [RelationshipSchemaName("lk_calendar_modifiedby")]
        public IEnumerable<Calendar> LkCalendarModifiedby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<Calendar>("lk_calendar_modifiedby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_calendar_modifiedby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_calendar_modifiedonbehalfby
        /// </summary>
        [RelationshipSchemaName("lk_calendar_modifiedonbehalfby")]
        public IEnumerable<Calendar> LkCalendarModifiedonbehalfby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<Calendar>("lk_calendar_modifiedonbehalfby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_calendar_modifiedonbehalfby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_calendarrule_createdby
        /// </summary>
        [RelationshipSchemaName("lk_calendarrule_createdby")]
        public IEnumerable<CalendarRule> LkCalendarruleCreatedby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<CalendarRule>("lk_calendarrule_createdby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_calendarrule_createdby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_calendarrule_createdonbehalfby
        /// </summary>
        [RelationshipSchemaName("lk_calendarrule_createdonbehalfby")]
        public IEnumerable<CalendarRule> LkCalendarruleCreatedonbehalfby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<CalendarRule>("lk_calendarrule_createdonbehalfby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_calendarrule_createdonbehalfby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_calendarrule_modifiedby
        /// </summary>
        [RelationshipSchemaName("lk_calendarrule_modifiedby")]
        public IEnumerable<CalendarRule> LkCalendarruleModifiedby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<CalendarRule>("lk_calendarrule_modifiedby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_calendarrule_modifiedby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_calendarrule_modifiedonbehalfby
        /// </summary>
        [RelationshipSchemaName("lk_calendarrule_modifiedonbehalfby")]
        public IEnumerable<CalendarRule> LkCalendarruleModifiedonbehalfby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<CalendarRule>("lk_calendarrule_modifiedonbehalfby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_calendarrule_modifiedonbehalfby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_contact_createdonbehalfby
        /// </summary>
        [RelationshipSchemaName("lk_contact_createdonbehalfby")]
        public IEnumerable<Contact> LkContactCreatedonbehalfby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<Contact>("lk_contact_createdonbehalfby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_contact_createdonbehalfby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_contact_modifiedonbehalfby
        /// </summary>
        [RelationshipSchemaName("lk_contact_modifiedonbehalfby")]
        public IEnumerable<Contact> LkContactModifiedonbehalfby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<Contact>("lk_contact_modifiedonbehalfby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_contact_modifiedonbehalfby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_contactbase_createdby
        /// </summary>
        [RelationshipSchemaName("lk_contactbase_createdby")]
        public IEnumerable<Contact> LkContactbaseCreatedby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<Contact>("lk_contactbase_createdby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_contactbase_createdby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_contactbase_modifiedby
        /// </summary>
        [RelationshipSchemaName("lk_contactbase_modifiedby")]
        public IEnumerable<Contact> LkContactbaseModifiedby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<Contact>("lk_contactbase_modifiedby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_contactbase_modifiedby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_customapi_createdby
        /// </summary>
        [RelationshipSchemaName("lk_customapi_createdby")]
        public IEnumerable<CustomAPI> LkCustomapiCreatedby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<CustomAPI>("lk_customapi_createdby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_customapi_createdby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_customapi_createdonbehalfby
        /// </summary>
        [RelationshipSchemaName("lk_customapi_createdonbehalfby")]
        public IEnumerable<CustomAPI> LkCustomapiCreatedonbehalfby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<CustomAPI>("lk_customapi_createdonbehalfby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_customapi_createdonbehalfby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_customapi_modifiedby
        /// </summary>
        [RelationshipSchemaName("lk_customapi_modifiedby")]
        public IEnumerable<CustomAPI> LkCustomapiModifiedby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<CustomAPI>("lk_customapi_modifiedby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_customapi_modifiedby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_customapi_modifiedonbehalfby
        /// </summary>
        [RelationshipSchemaName("lk_customapi_modifiedonbehalfby")]
        public IEnumerable<CustomAPI> LkCustomapiModifiedonbehalfby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<CustomAPI>("lk_customapi_modifiedonbehalfby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_customapi_modifiedonbehalfby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_customapirequestparameter_createdby
        /// </summary>
        [RelationshipSchemaName("lk_customapirequestparameter_createdby")]
        public IEnumerable<CustomAPIRequestParameter> LkCustomapirequestparameterCreatedby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<CustomAPIRequestParameter>("lk_customapirequestparameter_createdby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_customapirequestparameter_createdby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_customapirequestparameter_createdonbehalfby
        /// </summary>
        [RelationshipSchemaName("lk_customapirequestparameter_createdonbehalfby")]
        public IEnumerable<CustomAPIRequestParameter> LkCustomapirequestparameterCreatedonbehalfby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<CustomAPIRequestParameter>("lk_customapirequestparameter_createdonbehalfby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_customapirequestparameter_createdonbehalfby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_customapirequestparameter_modifiedby
        /// </summary>
        [RelationshipSchemaName("lk_customapirequestparameter_modifiedby")]
        public IEnumerable<CustomAPIRequestParameter> LkCustomapirequestparameterModifiedby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<CustomAPIRequestParameter>("lk_customapirequestparameter_modifiedby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_customapirequestparameter_modifiedby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_customapirequestparameter_modifiedonbehalfby
        /// </summary>
        [RelationshipSchemaName("lk_customapirequestparameter_modifiedonbehalfby")]
        public IEnumerable<CustomAPIRequestParameter> LkCustomapirequestparameterModifiedonbehalfby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<CustomAPIRequestParameter>("lk_customapirequestparameter_modifiedonbehalfby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_customapirequestparameter_modifiedonbehalfby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_customapiresponseproperty_createdby
        /// </summary>
        [RelationshipSchemaName("lk_customapiresponseproperty_createdby")]
        public IEnumerable<CustomAPIResponseProperty> LkCustomapiresponsepropertyCreatedby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<CustomAPIResponseProperty>("lk_customapiresponseproperty_createdby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_customapiresponseproperty_createdby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_customapiresponseproperty_createdonbehalfby
        /// </summary>
        [RelationshipSchemaName("lk_customapiresponseproperty_createdonbehalfby")]
        public IEnumerable<CustomAPIResponseProperty> LkCustomapiresponsepropertyCreatedonbehalfby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<CustomAPIResponseProperty>("lk_customapiresponseproperty_createdonbehalfby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_customapiresponseproperty_createdonbehalfby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_customapiresponseproperty_modifiedby
        /// </summary>
        [RelationshipSchemaName("lk_customapiresponseproperty_modifiedby")]
        public IEnumerable<CustomAPIResponseProperty> LkCustomapiresponsepropertyModifiedby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<CustomAPIResponseProperty>("lk_customapiresponseproperty_modifiedby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_customapiresponseproperty_modifiedby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_customapiresponseproperty_modifiedonbehalfby
        /// </summary>
        [RelationshipSchemaName("lk_customapiresponseproperty_modifiedonbehalfby")]
        public IEnumerable<CustomAPIResponseProperty> LkCustomapiresponsepropertyModifiedonbehalfby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<CustomAPIResponseProperty>("lk_customapiresponseproperty_modifiedonbehalfby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_customapiresponseproperty_modifiedonbehalfby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_documenttemplatebase_createdby
        /// </summary>
        [RelationshipSchemaName("lk_documenttemplatebase_createdby")]
        public IEnumerable<DocumentTemplate> LkDocumenttemplatebaseCreatedby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<DocumentTemplate>("lk_documenttemplatebase_createdby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_documenttemplatebase_createdby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_documenttemplatebase_createdonbehalfby
        /// </summary>
        [RelationshipSchemaName("lk_documenttemplatebase_createdonbehalfby")]
        public IEnumerable<DocumentTemplate> LkDocumenttemplatebaseCreatedonbehalfby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<DocumentTemplate>("lk_documenttemplatebase_createdonbehalfby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_documenttemplatebase_createdonbehalfby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_documenttemplatebase_modifiedby
        /// </summary>
        [RelationshipSchemaName("lk_documenttemplatebase_modifiedby")]
        public IEnumerable<DocumentTemplate> LkDocumenttemplatebaseModifiedby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<DocumentTemplate>("lk_documenttemplatebase_modifiedby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_documenttemplatebase_modifiedby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_documenttemplatebase_modifiedonbehalfby
        /// </summary>
        [RelationshipSchemaName("lk_documenttemplatebase_modifiedonbehalfby")]
        public IEnumerable<DocumentTemplate> LkDocumenttemplatebaseModifiedonbehalfby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<DocumentTemplate>("lk_documenttemplatebase_modifiedonbehalfby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_documenttemplatebase_modifiedonbehalfby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_duplicaterule_createdonbehalfby
        /// </summary>
        [RelationshipSchemaName("lk_duplicaterule_createdonbehalfby")]
        public IEnumerable<DuplicateRule> LkDuplicateruleCreatedonbehalfby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<DuplicateRule>("lk_duplicaterule_createdonbehalfby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_duplicaterule_createdonbehalfby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_duplicaterule_modifiedonbehalfby
        /// </summary>
        [RelationshipSchemaName("lk_duplicaterule_modifiedonbehalfby")]
        public IEnumerable<DuplicateRule> LkDuplicateruleModifiedonbehalfby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<DuplicateRule>("lk_duplicaterule_modifiedonbehalfby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_duplicaterule_modifiedonbehalfby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_duplicaterulebase_createdby
        /// </summary>
        [RelationshipSchemaName("lk_duplicaterulebase_createdby")]
        public IEnumerable<DuplicateRule> LkDuplicaterulebaseCreatedby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<DuplicateRule>("lk_duplicaterulebase_createdby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_duplicaterulebase_createdby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_duplicaterulebase_modifiedby
        /// </summary>
        [RelationshipSchemaName("lk_duplicaterulebase_modifiedby")]
        public IEnumerable<DuplicateRule> LkDuplicaterulebaseModifiedby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<DuplicateRule>("lk_duplicaterulebase_modifiedby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_duplicaterulebase_modifiedby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_duplicaterulecondition_createdonbehalfby
        /// </summary>
        [RelationshipSchemaName("lk_duplicaterulecondition_createdonbehalfby")]
        public IEnumerable<DuplicateRuleCondition> LkDuplicateruleconditionCreatedonbehalfby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<DuplicateRuleCondition>("lk_duplicaterulecondition_createdonbehalfby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_duplicaterulecondition_createdonbehalfby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_duplicaterulecondition_modifiedonbehalfby
        /// </summary>
        [RelationshipSchemaName("lk_duplicaterulecondition_modifiedonbehalfby")]
        public IEnumerable<DuplicateRuleCondition> LkDuplicateruleconditionModifiedonbehalfby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<DuplicateRuleCondition>("lk_duplicaterulecondition_modifiedonbehalfby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_duplicaterulecondition_modifiedonbehalfby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_duplicateruleconditionbase_createdby
        /// </summary>
        [RelationshipSchemaName("lk_duplicateruleconditionbase_createdby")]
        public IEnumerable<DuplicateRuleCondition> LkDuplicateruleconditionbaseCreatedby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<DuplicateRuleCondition>("lk_duplicateruleconditionbase_createdby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_duplicateruleconditionbase_createdby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_duplicateruleconditionbase_modifiedby
        /// </summary>
        [RelationshipSchemaName("lk_duplicateruleconditionbase_modifiedby")]
        public IEnumerable<DuplicateRuleCondition> LkDuplicateruleconditionbaseModifiedby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<DuplicateRuleCondition>("lk_duplicateruleconditionbase_modifiedby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_duplicateruleconditionbase_modifiedby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_importjobbase_createdby
        /// </summary>
        [RelationshipSchemaName("lk_importjobbase_createdby")]
        public IEnumerable<ImportJob> LkImportjobbaseCreatedby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<ImportJob>("lk_importjobbase_createdby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_importjobbase_createdby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_importjobbase_createdonbehalfby
        /// </summary>
        [RelationshipSchemaName("lk_importjobbase_createdonbehalfby")]
        public IEnumerable<ImportJob> LkImportjobbaseCreatedonbehalfby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<ImportJob>("lk_importjobbase_createdonbehalfby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_importjobbase_createdonbehalfby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_importjobbase_modifiedby
        /// </summary>
        [RelationshipSchemaName("lk_importjobbase_modifiedby")]
        public IEnumerable<ImportJob> LkImportjobbaseModifiedby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<ImportJob>("lk_importjobbase_modifiedby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_importjobbase_modifiedby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_importjobbase_modifiedonbehalfby
        /// </summary>
        [RelationshipSchemaName("lk_importjobbase_modifiedonbehalfby")]
        public IEnumerable<ImportJob> LkImportjobbaseModifiedonbehalfby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<ImportJob>("lk_importjobbase_modifiedonbehalfby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_importjobbase_modifiedonbehalfby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_managedidentity_createdby
        /// </summary>
        [RelationshipSchemaName("lk_managedidentity_createdby")]
        public IEnumerable<ManagedIdentity> LkManagedidentityCreatedby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<ManagedIdentity>("lk_managedidentity_createdby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_managedidentity_createdby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_managedidentity_createdonbehalfby
        /// </summary>
        [RelationshipSchemaName("lk_managedidentity_createdonbehalfby")]
        public IEnumerable<ManagedIdentity> LkManagedidentityCreatedonbehalfby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<ManagedIdentity>("lk_managedidentity_createdonbehalfby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_managedidentity_createdonbehalfby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_managedidentity_modifiedby
        /// </summary>
        [RelationshipSchemaName("lk_managedidentity_modifiedby")]
        public IEnumerable<ManagedIdentity> LkManagedidentityModifiedby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<ManagedIdentity>("lk_managedidentity_modifiedby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_managedidentity_modifiedby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_managedidentity_modifiedonbehalfby
        /// </summary>
        [RelationshipSchemaName("lk_managedidentity_modifiedonbehalfby")]
        public IEnumerable<ManagedIdentity> LkManagedidentityModifiedonbehalfby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<ManagedIdentity>("lk_managedidentity_modifiedonbehalfby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_managedidentity_modifiedonbehalfby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_organization_createdonbehalfby
        /// </summary>
        [RelationshipSchemaName("lk_organization_createdonbehalfby")]
        public IEnumerable<Organization> LkOrganizationCreatedonbehalfby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<Organization>("lk_organization_createdonbehalfby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_organization_createdonbehalfby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_organization_modifiedonbehalfby
        /// </summary>
        [RelationshipSchemaName("lk_organization_modifiedonbehalfby")]
        public IEnumerable<Organization> LkOrganizationModifiedonbehalfby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<Organization>("lk_organization_modifiedonbehalfby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_organization_modifiedonbehalfby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_organizationbase_createdby
        /// </summary>
        [RelationshipSchemaName("lk_organizationbase_createdby")]
        public IEnumerable<Organization> LkOrganizationbaseCreatedby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<Organization>("lk_organizationbase_createdby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_organizationbase_createdby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_organizationbase_modifiedby
        /// </summary>
        [RelationshipSchemaName("lk_organizationbase_modifiedby")]
        public IEnumerable<Organization> LkOrganizationbaseModifiedby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<Organization>("lk_organizationbase_modifiedby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_organizationbase_modifiedby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_pluginassembly_createdonbehalfby
        /// </summary>
        [RelationshipSchemaName("lk_pluginassembly_createdonbehalfby")]
        public IEnumerable<PluginAssembly> LkPluginassemblyCreatedonbehalfby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<PluginAssembly>("lk_pluginassembly_createdonbehalfby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_pluginassembly_createdonbehalfby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_pluginassembly_modifiedonbehalfby
        /// </summary>
        [RelationshipSchemaName("lk_pluginassembly_modifiedonbehalfby")]
        public IEnumerable<PluginAssembly> LkPluginassemblyModifiedonbehalfby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<PluginAssembly>("lk_pluginassembly_modifiedonbehalfby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_pluginassembly_modifiedonbehalfby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_pluginpackage_createdby
        /// </summary>
        [RelationshipSchemaName("lk_pluginpackage_createdby")]
        public IEnumerable<PluginPackage> LkPluginpackageCreatedby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<PluginPackage>("lk_pluginpackage_createdby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_pluginpackage_createdby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_pluginpackage_createdonbehalfby
        /// </summary>
        [RelationshipSchemaName("lk_pluginpackage_createdonbehalfby")]
        public IEnumerable<PluginPackage> LkPluginpackageCreatedonbehalfby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<PluginPackage>("lk_pluginpackage_createdonbehalfby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_pluginpackage_createdonbehalfby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_pluginpackage_modifiedby
        /// </summary>
        [RelationshipSchemaName("lk_pluginpackage_modifiedby")]
        public IEnumerable<PluginPackage> LkPluginpackageModifiedby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<PluginPackage>("lk_pluginpackage_modifiedby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_pluginpackage_modifiedby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_pluginpackage_modifiedonbehalfby
        /// </summary>
        [RelationshipSchemaName("lk_pluginpackage_modifiedonbehalfby")]
        public IEnumerable<PluginPackage> LkPluginpackageModifiedonbehalfby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<PluginPackage>("lk_pluginpackage_modifiedonbehalfby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_pluginpackage_modifiedonbehalfby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_plugintype_createdonbehalfby
        /// </summary>
        [RelationshipSchemaName("lk_plugintype_createdonbehalfby")]
        public IEnumerable<PluginType> LkPlugintypeCreatedonbehalfby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<PluginType>("lk_plugintype_createdonbehalfby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_plugintype_createdonbehalfby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_plugintype_modifiedonbehalfby
        /// </summary>
        [RelationshipSchemaName("lk_plugintype_modifiedonbehalfby")]
        public IEnumerable<PluginType> LkPlugintypeModifiedonbehalfby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<PluginType>("lk_plugintype_modifiedonbehalfby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_plugintype_modifiedonbehalfby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_publisher_createdby
        /// </summary>
        [RelationshipSchemaName("lk_publisher_createdby")]
        public IEnumerable<Publisher> LkPublisherCreatedby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<Publisher>("lk_publisher_createdby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_publisher_createdby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_publisher_modifiedby
        /// </summary>
        [RelationshipSchemaName("lk_publisher_modifiedby")]
        public IEnumerable<Publisher> LkPublisherModifiedby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<Publisher>("lk_publisher_modifiedby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_publisher_modifiedby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_publisherbase_createdonbehalfby
        /// </summary>
        [RelationshipSchemaName("lk_publisherbase_createdonbehalfby")]
        public IEnumerable<Publisher> LkPublisherbaseCreatedonbehalfby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<Publisher>("lk_publisherbase_createdonbehalfby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_publisherbase_createdonbehalfby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_publisherbase_modifiedonbehalfby
        /// </summary>
        [RelationshipSchemaName("lk_publisherbase_modifiedonbehalfby")]
        public IEnumerable<Publisher> LkPublisherbaseModifiedonbehalfby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<Publisher>("lk_publisherbase_modifiedonbehalfby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_publisherbase_modifiedonbehalfby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_queue_createdonbehalfby
        /// </summary>
        [RelationshipSchemaName("lk_queue_createdonbehalfby")]
        public IEnumerable<Queue> LkQueueCreatedonbehalfby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<Queue>("lk_queue_createdonbehalfby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_queue_createdonbehalfby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_queue_modifiedonbehalfby
        /// </summary>
        [RelationshipSchemaName("lk_queue_modifiedonbehalfby")]
        public IEnumerable<Queue> LkQueueModifiedonbehalfby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<Queue>("lk_queue_modifiedonbehalfby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_queue_modifiedonbehalfby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_queuebase_createdby
        /// </summary>
        [RelationshipSchemaName("lk_queuebase_createdby")]
        public IEnumerable<Queue> LkQueuebaseCreatedby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<Queue>("lk_queuebase_createdby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_queuebase_createdby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_queuebase_modifiedby
        /// </summary>
        [RelationshipSchemaName("lk_queuebase_modifiedby")]
        public IEnumerable<Queue> LkQueuebaseModifiedby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<Queue>("lk_queuebase_modifiedby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_queuebase_modifiedby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_role_createdonbehalfby
        /// </summary>
        [RelationshipSchemaName("lk_role_createdonbehalfby")]
        public IEnumerable<Role> LkRoleCreatedonbehalfby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<Role>("lk_role_createdonbehalfby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_role_createdonbehalfby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_role_modifiedonbehalfby
        /// </summary>
        [RelationshipSchemaName("lk_role_modifiedonbehalfby")]
        public IEnumerable<Role> LkRoleModifiedonbehalfby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<Role>("lk_role_modifiedonbehalfby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_role_modifiedonbehalfby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_rolebase_createdby
        /// </summary>
        [RelationshipSchemaName("lk_rolebase_createdby")]
        public IEnumerable<Role> LkRolebaseCreatedby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<Role>("lk_rolebase_createdby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_rolebase_createdby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_rolebase_modifiedby
        /// </summary>
        [RelationshipSchemaName("lk_rolebase_modifiedby")]
        public IEnumerable<Role> LkRolebaseModifiedby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<Role>("lk_rolebase_modifiedby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_rolebase_modifiedby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_routingrule_createdby
        /// </summary>
        [RelationshipSchemaName("lk_routingrule_createdby")]
        public IEnumerable<RoutingRule> LkRoutingruleCreatedby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<RoutingRule>("lk_routingrule_createdby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_routingrule_createdby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_routingrule_createdonbehalfby
        /// </summary>
        [RelationshipSchemaName("lk_routingrule_createdonbehalfby")]
        public IEnumerable<RoutingRule> LkRoutingruleCreatedonbehalfby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<RoutingRule>("lk_routingrule_createdonbehalfby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_routingrule_createdonbehalfby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_routingrule_modifiedby
        /// </summary>
        [RelationshipSchemaName("lk_routingrule_modifiedby")]
        public IEnumerable<RoutingRule> LkRoutingruleModifiedby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<RoutingRule>("lk_routingrule_modifiedby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_routingrule_modifiedby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_routingrule_modifiedonbehalfby
        /// </summary>
        [RelationshipSchemaName("lk_routingrule_modifiedonbehalfby")]
        public IEnumerable<RoutingRule> LkRoutingruleModifiedonbehalfby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<RoutingRule>("lk_routingrule_modifiedonbehalfby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_routingrule_modifiedonbehalfby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_RoutingRuleItem_createdby
        /// </summary>
        [RelationshipSchemaName("lk_RoutingRuleItem_createdby")]
        public IEnumerable<RoutingRuleItem> LkRoutingRuleItemCreatedby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<RoutingRuleItem>("lk_RoutingRuleItem_createdby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_RoutingRuleItem_createdby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_routingruleitem_createdonbehalfby
        /// </summary>
        [RelationshipSchemaName("lk_routingruleitem_createdonbehalfby")]
        public IEnumerable<RoutingRuleItem> LkRoutingruleitemCreatedonbehalfby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<RoutingRuleItem>("lk_routingruleitem_createdonbehalfby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_routingruleitem_createdonbehalfby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_routingruleitem_modifiedby
        /// </summary>
        [RelationshipSchemaName("lk_routingruleitem_modifiedby")]
        public IEnumerable<RoutingRuleItem> LkRoutingruleitemModifiedby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<RoutingRuleItem>("lk_routingruleitem_modifiedby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_routingruleitem_modifiedby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_routingruleitem_modifiedonbehalfby
        /// </summary>
        [RelationshipSchemaName("lk_routingruleitem_modifiedonbehalfby")]
        public IEnumerable<RoutingRuleItem> LkRoutingruleitemModifiedonbehalfby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<RoutingRuleItem>("lk_routingruleitem_modifiedonbehalfby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_routingruleitem_modifiedonbehalfby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_savedquery_createdonbehalfby
        /// </summary>
        [RelationshipSchemaName("lk_savedquery_createdonbehalfby")]
        public IEnumerable<SavedQuery> LkSavedqueryCreatedonbehalfby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<SavedQuery>("lk_savedquery_createdonbehalfby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_savedquery_createdonbehalfby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_savedquery_modifiedonbehalfby
        /// </summary>
        [RelationshipSchemaName("lk_savedquery_modifiedonbehalfby")]
        public IEnumerable<SavedQuery> LkSavedqueryModifiedonbehalfby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<SavedQuery>("lk_savedquery_modifiedonbehalfby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_savedquery_modifiedonbehalfby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_savedquerybase_createdby
        /// </summary>
        [RelationshipSchemaName("lk_savedquerybase_createdby")]
        public IEnumerable<SavedQuery> LkSavedquerybaseCreatedby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<SavedQuery>("lk_savedquerybase_createdby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_savedquerybase_createdby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_savedquerybase_modifiedby
        /// </summary>
        [RelationshipSchemaName("lk_savedquerybase_modifiedby")]
        public IEnumerable<SavedQuery> LkSavedquerybaseModifiedby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<SavedQuery>("lk_savedquerybase_modifiedby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_savedquerybase_modifiedby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_sdkmessage_createdonbehalfby
        /// </summary>
        [RelationshipSchemaName("lk_sdkmessage_createdonbehalfby")]
        public IEnumerable<SdkMessage> LkSdkmessageCreatedonbehalfby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<SdkMessage>("lk_sdkmessage_createdonbehalfby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_sdkmessage_createdonbehalfby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_sdkmessage_modifiedonbehalfby
        /// </summary>
        [RelationshipSchemaName("lk_sdkmessage_modifiedonbehalfby")]
        public IEnumerable<SdkMessage> LkSdkmessageModifiedonbehalfby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<SdkMessage>("lk_sdkmessage_modifiedonbehalfby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_sdkmessage_modifiedonbehalfby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_sdkmessagefilter_createdonbehalfby
        /// </summary>
        [RelationshipSchemaName("lk_sdkmessagefilter_createdonbehalfby")]
        public IEnumerable<SdkMessageFilter> LkSdkmessagefilterCreatedonbehalfby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<SdkMessageFilter>("lk_sdkmessagefilter_createdonbehalfby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_sdkmessagefilter_createdonbehalfby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_sdkmessagefilter_modifiedonbehalfby
        /// </summary>
        [RelationshipSchemaName("lk_sdkmessagefilter_modifiedonbehalfby")]
        public IEnumerable<SdkMessageFilter> LkSdkmessagefilterModifiedonbehalfby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<SdkMessageFilter>("lk_sdkmessagefilter_modifiedonbehalfby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_sdkmessagefilter_modifiedonbehalfby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_sdkmessageprocessingstep_createdonbehalfby
        /// </summary>
        [RelationshipSchemaName("lk_sdkmessageprocessingstep_createdonbehalfby")]
        public IEnumerable<SdkMessageProcessingStep> LkSdkmessageprocessingstepCreatedonbehalfby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<SdkMessageProcessingStep>("lk_sdkmessageprocessingstep_createdonbehalfby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_sdkmessageprocessingstep_createdonbehalfby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_sdkmessageprocessingstep_modifiedonbehalfby
        /// </summary>
        [RelationshipSchemaName("lk_sdkmessageprocessingstep_modifiedonbehalfby")]
        public IEnumerable<SdkMessageProcessingStep> LkSdkmessageprocessingstepModifiedonbehalfby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<SdkMessageProcessingStep>("lk_sdkmessageprocessingstep_modifiedonbehalfby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_sdkmessageprocessingstep_modifiedonbehalfby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_sdkmessageprocessingstepimage_createdonbehalfby
        /// </summary>
        [RelationshipSchemaName("lk_sdkmessageprocessingstepimage_createdonbehalfby")]
        public IEnumerable<SdkMessageProcessingStepImage> LkSdkmessageprocessingstepimageCreatedonbehalfby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<SdkMessageProcessingStepImage>("lk_sdkmessageprocessingstepimage_createdonbehalfby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_sdkmessageprocessingstepimage_createdonbehalfby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_sdkmessageprocessingstepimage_modifiedonbehalfby
        /// </summary>
        [RelationshipSchemaName("lk_sdkmessageprocessingstepimage_modifiedonbehalfby")]
        public IEnumerable<SdkMessageProcessingStepImage> LkSdkmessageprocessingstepimageModifiedonbehalfby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<SdkMessageProcessingStepImage>("lk_sdkmessageprocessingstepimage_modifiedonbehalfby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_sdkmessageprocessingstepimage_modifiedonbehalfby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_sdkmessageprocessingstepsecureconfig_createdonbehalfby
        /// </summary>
        [RelationshipSchemaName("lk_sdkmessageprocessingstepsecureconfig_createdonbehalfby")]
        public IEnumerable<SdkMessageProcessingStepSecureConfig> LkSdkmessageprocessingstepsecureconfigCreatedonbehalfby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<SdkMessageProcessingStepSecureConfig>("lk_sdkmessageprocessingstepsecureconfig_createdonbehalfby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_sdkmessageprocessingstepsecureconfig_createdonbehalfby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_sdkmessageprocessingstepsecureconfig_modifiedonbehalfby
        /// </summary>
        [RelationshipSchemaName("lk_sdkmessageprocessingstepsecureconfig_modifiedonbehalfby")]
        public IEnumerable<SdkMessageProcessingStepSecureConfig> LkSdkmessageprocessingstepsecureconfigModifiedonbehalfby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<SdkMessageProcessingStepSecureConfig>("lk_sdkmessageprocessingstepsecureconfig_modifiedonbehalfby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_sdkmessageprocessingstepsecureconfig_modifiedonbehalfby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_slabase_createdby
        /// </summary>
        [RelationshipSchemaName("lk_slabase_createdby")]
        public IEnumerable<SLA> LkSlabaseCreatedby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<SLA>("lk_slabase_createdby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_slabase_createdby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_slabase_createdonbehalfby
        /// </summary>
        [RelationshipSchemaName("lk_slabase_createdonbehalfby")]
        public IEnumerable<SLA> LkSlabaseCreatedonbehalfby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<SLA>("lk_slabase_createdonbehalfby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_slabase_createdonbehalfby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_slabase_modifiedby
        /// </summary>
        [RelationshipSchemaName("lk_slabase_modifiedby")]
        public IEnumerable<SLA> LkSlabaseModifiedby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<SLA>("lk_slabase_modifiedby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_slabase_modifiedby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_slabase_modifiedonbehalfby
        /// </summary>
        [RelationshipSchemaName("lk_slabase_modifiedonbehalfby")]
        public IEnumerable<SLA> LkSlabaseModifiedonbehalfby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<SLA>("lk_slabase_modifiedonbehalfby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_slabase_modifiedonbehalfby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_solution_createdby
        /// </summary>
        [RelationshipSchemaName("lk_solution_createdby")]
        public IEnumerable<Solution> LkSolutionCreatedby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<Solution>("lk_solution_createdby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_solution_createdby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_solution_modifiedby
        /// </summary>
        [RelationshipSchemaName("lk_solution_modifiedby")]
        public IEnumerable<Solution> LkSolutionModifiedby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<Solution>("lk_solution_modifiedby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_solution_modifiedby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_solutionbase_createdonbehalfby
        /// </summary>
        [RelationshipSchemaName("lk_solutionbase_createdonbehalfby")]
        public IEnumerable<Solution> LkSolutionbaseCreatedonbehalfby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<Solution>("lk_solutionbase_createdonbehalfby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_solutionbase_createdonbehalfby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_solutionbase_modifiedonbehalfby
        /// </summary>
        [RelationshipSchemaName("lk_solutionbase_modifiedonbehalfby")]
        public IEnumerable<Solution> LkSolutionbaseModifiedonbehalfby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<Solution>("lk_solutionbase_modifiedonbehalfby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_solutionbase_modifiedonbehalfby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_solutioncomponentbase_createdonbehalfby
        /// </summary>
        [RelationshipSchemaName("lk_solutioncomponentbase_createdonbehalfby")]
        public IEnumerable<SolutionComponent> LkSolutioncomponentbaseCreatedonbehalfby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<SolutionComponent>("lk_solutioncomponentbase_createdonbehalfby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_solutioncomponentbase_createdonbehalfby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_solutioncomponentbase_modifiedonbehalfby
        /// </summary>
        [RelationshipSchemaName("lk_solutioncomponentbase_modifiedonbehalfby")]
        public IEnumerable<SolutionComponent> LkSolutioncomponentbaseModifiedonbehalfby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<SolutionComponent>("lk_solutioncomponentbase_modifiedonbehalfby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_solutioncomponentbase_modifiedonbehalfby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_systemuser_createdonbehalfby
        /// </summary>
        [RelationshipSchemaName("lk_systemuser_createdonbehalfby")]
        public IEnumerable<SystemUser> LkSystemuserCreatedonbehalfby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<SystemUser>("lk_systemuser_createdonbehalfby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_systemuser_createdonbehalfby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_systemuser_modifiedonbehalfby
        /// </summary>
        [RelationshipSchemaName("lk_systemuser_modifiedonbehalfby")]
        public IEnumerable<SystemUser> LkSystemuserModifiedonbehalfby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<SystemUser>("lk_systemuser_modifiedonbehalfby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_systemuser_modifiedonbehalfby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_systemuserbase_createdby
        /// </summary>
        [RelationshipSchemaName("lk_systemuserbase_createdby")]
        public IEnumerable<SystemUser> LkSystemuserbaseCreatedby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<SystemUser>("lk_systemuserbase_createdby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_systemuserbase_createdby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_systemuserbase_modifiedby
        /// </summary>
        [RelationshipSchemaName("lk_systemuserbase_modifiedby")]
        public IEnumerable<SystemUser> LkSystemuserbaseModifiedby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<SystemUser>("lk_systemuserbase_modifiedby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_systemuserbase_modifiedby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_team_createdonbehalfby
        /// </summary>
        [RelationshipSchemaName("lk_team_createdonbehalfby")]
        public IEnumerable<Team> LkTeamCreatedonbehalfby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<Team>("lk_team_createdonbehalfby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_team_createdonbehalfby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_team_modifiedonbehalfby
        /// </summary>
        [RelationshipSchemaName("lk_team_modifiedonbehalfby")]
        public IEnumerable<Team> LkTeamModifiedonbehalfby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<Team>("lk_team_modifiedonbehalfby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_team_modifiedonbehalfby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_teambase_administratorid
        /// </summary>
        [RelationshipSchemaName("lk_teambase_administratorid")]
        public IEnumerable<Team> LkTeambaseAdministratorid
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<Team>("lk_teambase_administratorid", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_teambase_administratorid", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_teambase_createdby
        /// </summary>
        [RelationshipSchemaName("lk_teambase_createdby")]
        public IEnumerable<Team> LkTeambaseCreatedby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<Team>("lk_teambase_createdby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_teambase_createdby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_teambase_modifiedby
        /// </summary>
        [RelationshipSchemaName("lk_teambase_modifiedby")]
        public IEnumerable<Team> LkTeambaseModifiedby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<Team>("lk_teambase_modifiedby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_teambase_modifiedby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_teamtemplate_createdby
        /// </summary>
        [RelationshipSchemaName("lk_teamtemplate_createdby")]
        public IEnumerable<TeamTemplate> LkTeamtemplateCreatedby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<TeamTemplate>("lk_teamtemplate_createdby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_teamtemplate_createdby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_teamtemplate_createdonbehalfby
        /// </summary>
        [RelationshipSchemaName("lk_teamtemplate_createdonbehalfby")]
        public IEnumerable<TeamTemplate> LkTeamtemplateCreatedonbehalfby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<TeamTemplate>("lk_teamtemplate_createdonbehalfby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_teamtemplate_createdonbehalfby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_teamtemplate_modifiedby
        /// </summary>
        [RelationshipSchemaName("lk_teamtemplate_modifiedby")]
        public IEnumerable<TeamTemplate> LkTeamtemplateModifiedby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<TeamTemplate>("lk_teamtemplate_modifiedby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_teamtemplate_modifiedby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_teamtemplate_modifiedonbehalfby
        /// </summary>
        [RelationshipSchemaName("lk_teamtemplate_modifiedonbehalfby")]
        public IEnumerable<TeamTemplate> LkTeamtemplateModifiedonbehalfby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<TeamTemplate>("lk_teamtemplate_modifiedonbehalfby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_teamtemplate_modifiedonbehalfby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_webresourcebase_createdonbehalfby
        /// </summary>
        [RelationshipSchemaName("lk_webresourcebase_createdonbehalfby")]
        public IEnumerable<WebResource> LkWebresourcebaseCreatedonbehalfby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<WebResource>("lk_webresourcebase_createdonbehalfby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_webresourcebase_createdonbehalfby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N lk_webresourcebase_modifiedonbehalfby
        /// </summary>
        [RelationshipSchemaName("lk_webresourcebase_modifiedonbehalfby")]
        public IEnumerable<WebResource> LkWebresourcebaseModifiedonbehalfby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<WebResource>("lk_webresourcebase_modifiedonbehalfby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("lk_webresourcebase_modifiedonbehalfby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N modifiedby_pluginassembly
        /// </summary>
        [RelationshipSchemaName("modifiedby_pluginassembly")]
        public IEnumerable<PluginAssembly> ModifiedbyPluginassembly
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<PluginAssembly>("modifiedby_pluginassembly", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("modifiedby_pluginassembly", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N modifiedby_plugintype
        /// </summary>
        [RelationshipSchemaName("modifiedby_plugintype")]
        public IEnumerable<PluginType> ModifiedbyPlugintype
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<PluginType>("modifiedby_plugintype", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("modifiedby_plugintype", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N modifiedby_sdkmessage
        /// </summary>
        [RelationshipSchemaName("modifiedby_sdkmessage")]
        public IEnumerable<SdkMessage> ModifiedbySdkmessage
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<SdkMessage>("modifiedby_sdkmessage", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("modifiedby_sdkmessage", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N modifiedby_sdkmessagefilter
        /// </summary>
        [RelationshipSchemaName("modifiedby_sdkmessagefilter")]
        public IEnumerable<SdkMessageFilter> ModifiedbySdkmessagefilter
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<SdkMessageFilter>("modifiedby_sdkmessagefilter", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("modifiedby_sdkmessagefilter", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N modifiedby_sdkmessageprocessingstep
        /// </summary>
        [RelationshipSchemaName("modifiedby_sdkmessageprocessingstep")]
        public IEnumerable<SdkMessageProcessingStep> ModifiedbySdkmessageprocessingstep
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<SdkMessageProcessingStep>("modifiedby_sdkmessageprocessingstep", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("modifiedby_sdkmessageprocessingstep", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N modifiedby_sdkmessageprocessingstepimage
        /// </summary>
        [RelationshipSchemaName("modifiedby_sdkmessageprocessingstepimage")]
        public IEnumerable<SdkMessageProcessingStepImage> ModifiedbySdkmessageprocessingstepimage
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<SdkMessageProcessingStepImage>("modifiedby_sdkmessageprocessingstepimage", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("modifiedby_sdkmessageprocessingstepimage", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N modifiedby_sdkmessageprocessingstepsecureconfig
        /// </summary>
        [RelationshipSchemaName("modifiedby_sdkmessageprocessingstepsecureconfig")]
        public IEnumerable<SdkMessageProcessingStepSecureConfig> ModifiedbySdkmessageprocessingstepsecureconfig
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<SdkMessageProcessingStepSecureConfig>("modifiedby_sdkmessageprocessingstepsecureconfig", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("modifiedby_sdkmessageprocessingstepsecureconfig", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N queue_primary_user
        /// </summary>
        [RelationshipSchemaName("queue_primary_user")]
        public IEnumerable<Queue> QueuePrimaryUser
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<Queue>("queue_primary_user", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("queue_primary_user", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N system_user_accounts
        /// </summary>
        [RelationshipSchemaName("system_user_accounts")]
        public IEnumerable<Account> SystemUserAccounts
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<Account>("system_user_accounts", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("system_user_accounts", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N system_user_asyncoperation
        /// </summary>
        [RelationshipSchemaName("system_user_asyncoperation")]
        public IEnumerable<AsyncOperation> SystemUserAsyncoperation
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<AsyncOperation>("system_user_asyncoperation", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("system_user_asyncoperation", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N system_user_contacts
        /// </summary>
        [RelationshipSchemaName("system_user_contacts")]
        public IEnumerable<Contact> SystemUserContacts
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<Contact>("system_user_contacts", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("system_user_contacts", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N system_user_workflow
        /// </summary>
        [RelationshipSchemaName("system_user_workflow")]
        public IEnumerable<Workflow> SystemUserWorkflow
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<Workflow>("system_user_workflow", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("system_user_workflow", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N SystemUser_AsyncOperations
        /// </summary>
        [RelationshipSchemaName("SystemUser_AsyncOperations")]
        public IEnumerable<AsyncOperation> SystemUserAsyncOperations
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<AsyncOperation>("SystemUser_AsyncOperations", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("SystemUser_AsyncOperations", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N SystemUser_DuplicateRules
        /// </summary>
        [RelationshipSchemaName("SystemUser_DuplicateRules")]
        public IEnumerable<DuplicateRule> SystemUserDuplicateRules
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<DuplicateRule>("SystemUser_DuplicateRules", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("SystemUser_DuplicateRules", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N user_accounts
        /// </summary>
        [RelationshipSchemaName("user_accounts")]
        public IEnumerable<Account> UserAccounts
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<Account>("user_accounts", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("user_accounts", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N user_customapi
        /// </summary>
        [RelationshipSchemaName("user_customapi")]
        public IEnumerable<CustomAPI> UserCustomapi
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<CustomAPI>("user_customapi", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("user_customapi", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N user_managedidentity
        /// </summary>
        [RelationshipSchemaName("user_managedidentity")]
        public IEnumerable<ManagedIdentity> UserManagedidentity
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<ManagedIdentity>("user_managedidentity", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("user_managedidentity", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N user_parent_user
        /// </summary>
        [RelationshipSchemaName("user_parent_user")]
        public IEnumerable<SystemUser> UserParentUser
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<SystemUser>("user_parent_user", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("user_parent_user", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N user_routingrule
        /// </summary>
        [RelationshipSchemaName("user_routingrule")]
        public IEnumerable<RoutingRule> UserRoutingrule
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<RoutingRule>("user_routingrule", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("user_routingrule", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N user_routingruleitem
        /// </summary>
        [RelationshipSchemaName("user_routingruleitem")]
        public IEnumerable<RoutingRuleItem> UserRoutingruleitem
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<RoutingRuleItem>("user_routingruleitem", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("user_routingruleitem", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N user_slabase
        /// </summary>
        [RelationshipSchemaName("user_slabase")]
        public IEnumerable<SLA> UserSlabase
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<SLA>("user_slabase", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("user_slabase", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N webresource_createdby
        /// </summary>
        [RelationshipSchemaName("webresource_createdby")]
        public IEnumerable<WebResource> WebresourceCreatedby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<WebResource>("webresource_createdby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("webresource_createdby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N webresource_modifiedby
        /// </summary>
        [RelationshipSchemaName("webresource_modifiedby")]
        public IEnumerable<WebResource> WebresourceModifiedby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<WebResource>("webresource_modifiedby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("webresource_modifiedby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N workflow_createdby
        /// </summary>
        [RelationshipSchemaName("workflow_createdby")]
        public IEnumerable<Workflow> WorkflowCreatedby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<Workflow>("workflow_createdby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("workflow_createdby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N workflow_createdonbehalfby
        /// </summary>
        [RelationshipSchemaName("workflow_createdonbehalfby")]
        public IEnumerable<Workflow> WorkflowCreatedonbehalfby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<Workflow>("workflow_createdonbehalfby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("workflow_createdonbehalfby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N Workflow_licensee
        /// </summary>
        [RelationshipSchemaName("Workflow_licensee")]
        public IEnumerable<Workflow> WorkflowLicensee
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<Workflow>("Workflow_licensee", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("Workflow_licensee", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N workflow_modifiedby
        /// </summary>
        [RelationshipSchemaName("workflow_modifiedby")]
        public IEnumerable<Workflow> WorkflowModifiedby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<Workflow>("workflow_modifiedby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("workflow_modifiedby", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N workflow_modifiedonbehalfby
        /// </summary>
        [RelationshipSchemaName("workflow_modifiedonbehalfby")]
        public IEnumerable<Workflow> WorkflowModifiedonbehalfby
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<Workflow>("workflow_modifiedonbehalfby", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("workflow_modifiedonbehalfby", null, value);
                OnPropertyChanged();
            }
        }
        #endregion

        #region Options
        public static partial class Options
        {
            public struct AccessMode
            {
                public const int ReadWrite = 0;
                public const int Administrative = 1;
                public const int Read = 2;
                public const int SupportUser = 3;
                public const int NonInteractive = 4;
                public const int DelegatedAdmin = 5;
            }
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
            public struct AzureState
            {
                public const int Exists = 0;
                public const int SoftDeleted = 1;
                public const int NotFoundOrHardDeleted = 2;
            }
            public struct CALType
            {
                public const int Professional = 0;
                public const int Administrative = 1;
                public const int Basic = 2;
                public const int DeviceProfessional = 3;
                public const int DeviceBasic = 4;
                public const int Essential = 5;
                public const int DeviceEssential = 6;
                public const int Enterprise = 7;
                public const int DeviceEnterprise = 8;
                public const int Sales = 9;
                public const int Service = 10;
                public const int FieldService = 11;
                public const int ProjectService = 12;
            }
            public struct DefaultFiltersPopulated
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct DeletedState
            {
                public const int NotDeleted = 0;
                public const int SoftDeleted = 1;
            }
            public struct DisplayInServiceViews
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct EmailRouterAccessApproval
            {
                public const int Empty = 0;
                public const int Approved = 1;
                public const int PendingApproval = 2;
                public const int Rejected = 3;
            }
            public struct IncomingEmailDeliveryMethod
            {
                public const int None = 0;
                public const int MicrosoftDynamics365ForOutlook = 1;
                public const int ServerSideSynchronizationOrEmailRouter = 2;
                public const int ForwardMailbox = 3;
            }
            public struct InviteStatusCode
            {
                public const int InvitationNotSent = 0;
                public const int Invited = 1;
                public const int InvitationNearExpired = 2;
                public const int InvitationExpired = 3;
                public const int InvitationAccepted = 4;
                public const int InvitationRejected = 5;
                public const int InvitationRevoked = 6;
            }
            public struct IsAllowedByIpFirewall
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsDisabled
            {
                public const bool Enabled = false;
                public const bool Disabled = true;
            }
            public struct IsEmailAddressApprovedByO365Admin
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsIntegrationUser
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsLicensed
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct IsSyncWithDirectory
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct OutgoingEmailDeliveryMethod
            {
                public const int None = 0;
                public const int MicrosoftDynamics365ForOutlook = 1;
                public const int ServerSideSynchronizationOrEmailRouter = 2;
            }
            public struct PreferredAddressCode
            {
                public const int MailingAddress = 1;
                public const int OtherAddress = 2;
            }
            public struct PreferredEmailCode
            {
                public const int DefaultValue = 1;
            }
            public struct PreferredPhoneCode
            {
                public const int MainPhone = 1;
                public const int OtherPhone = 2;
                public const int HomePhone = 3;
                public const int MobilePhone = 4;
            }
            public struct SetupUser
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct SystemManagedUserType
            {
                public const int EntraUser = 0;
                public const int C2User = 1;
                public const int ImpersonableStubUser = 2;
                public const int AgenticUser = 3;
            }
        }
        #endregion

        #region LogicalNames
        public static partial class LogicalNames
        {
            public const string Address1AddressId = "address1_addressid";
            public const string Address2AddressId = "address2_addressid";
            public const string SystemUserId = "systemuserid";
            public const string AccessMode = "accessmode";
            public const string Address1AddressTypeCode = "address1_addresstypecode";
            public const string Address1City = "address1_city";
            public const string Address1Composite = "address1_composite";
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
            public const string Address2Composite = "address2_composite";
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
            public const string ApplicationId = "applicationid";
            public const string ApplicationIdUri = "applicationiduri";
            public const string AzureActiveDirectoryObjectId = "azureactivedirectoryobjectid";
            public const string AzureDeletedOn = "azuredeletedon";
            public const string AzureState = "azurestate";
            public const string BusinessUnitId = "businessunitid";
            public const string CalendarId = "calendarid";
            public const string CALType = "caltype";
            public const string CreatedBy = "createdby";
            public const string CreatedOn = "createdon";
            public const string CreatedOnBehalfBy = "createdonbehalfby";
            public const string DefaultFiltersPopulated = "defaultfilterspopulated";
            public const string DefaultMailbox = "defaultmailbox";
            public const string DefaultOdbFolderName = "defaultodbfoldername";
            public const string DeletedState = "deletedstate";
            public const string DisabledReason = "disabledreason";
            public const string DisplayInServiceViews = "displayinserviceviews";
            public const string DomainName = "domainname";
            public const string EmailRouterAccessApproval = "emailrouteraccessapproval";
            public const string EmployeeId = "employeeid";
            public const string EntityImage = "entityimage";
            public const string EntityImageTimestamp = "entityimage_timestamp";
            public const string EntityImageURL = "entityimage_url";
            public const string EntityImageId = "entityimageid";
            public const string ExchangeRate = "exchangerate";
            public const string FirstName = "firstname";
            public const string FullName = "fullname";
            public const string GovernmentId = "governmentid";
            public const string HomePhone = "homephone";
            public const string IdentityId = "identityid";
            public const string ImportSequenceNumber = "importsequencenumber";
            public const string IncomingEmailDeliveryMethod = "incomingemaildeliverymethod";
            public const string InternalEMailAddress = "internalemailaddress";
            public const string InviteStatusCode = "invitestatuscode";
            public const string IsAllowedByIpFirewall = "isallowedbyipfirewall";
            public const string IsDisabled = "isdisabled";
            public const string IsEmailAddressApprovedByO365Admin = "isemailaddressapprovedbyo365admin";
            public const string IsIntegrationUser = "isintegrationuser";
            public const string IsLicensed = "islicensed";
            public const string IsSyncWithDirectory = "issyncwithdirectory";
            public const string JobTitle = "jobtitle";
            public const string LastName = "lastname";
            public const string MiddleName = "middlename";
            public const string MobileAlertEMail = "mobilealertemail";
            public const string MobileOfflineProfileId = "mobileofflineprofileid";
            public const string MobilePhone = "mobilephone";
            public const string ModifiedBy = "modifiedby";
            public const string ModifiedOn = "modifiedon";
            public const string ModifiedOnBehalfBy = "modifiedonbehalfby";
            public const string NickName = "nickname";
            public const string OrganizationId = "organizationid";
            public const string OutgoingEmailDeliveryMethod = "outgoingemaildeliverymethod";
            public const string OverriddenCreatedOn = "overriddencreatedon";
            public const string ParentSystemUserId = "parentsystemuserid";
            public const string PassportHi = "passporthi";
            public const string PassportLo = "passportlo";
            public const string PersonalEMailAddress = "personalemailaddress";
            public const string PhotoUrl = "photourl";
            public const string PositionId = "positionid";
            public const string PreferredAddressCode = "preferredaddresscode";
            public const string PreferredEmailCode = "preferredemailcode";
            public const string PreferredPhoneCode = "preferredphonecode";
            public const string ProcessId = "processid";
            public const string QueueId = "queueid";
            public const string Salutation = "salutation";
            public const string SetupUser = "setupuser";
            public const string SharePointEmailAddress = "sharepointemailaddress";
            public const string Skills = "skills";
            public const string StageId = "stageid";
            public const string SystemManagedUserType = "systemmanagedusertype";
            public const string TerritoryId = "territoryid";
            public const string TimeZoneRuleVersionNumber = "timezoneruleversionnumber";
            public const string Title = "title";
            public const string TransactionCurrencyId = "transactioncurrencyid";
            public const string TraversedPath = "traversedpath";
            public const string UserLicenseType = "userlicensetype";
            public const string UserPuid = "userpuid";
            public const string UTCConversionTimeZoneCode = "utcconversiontimezonecode";
            public const string VersionNumber = "versionnumber";
            public const string WindowsLiveID = "windowsliveid";
            public const string YammerEmailAddress = "yammeremailaddress";
            public const string YammerUserId = "yammeruserid";
            public const string YomiFirstName = "yomifirstname";
            public const string YomiFullName = "yomifullname";
            public const string YomiLastName = "yomilastname";
            public const string YomiMiddleName = "yomimiddlename";
        }
        #endregion

        #region AlternateKeys
        public static partial class AlternateKeys
        {
            public const string AADObjectId = "aadobjectid";
        }
        #endregion

        #region Relations
        public static partial class Relations
        {
            public static class OneToMany
            {
                public const string ActioncardusersettingsOwningUser = "actioncardusersettings_owning_user";
                public const string AdxInviteredemptionSystemuserCreatedby = "adx_inviteredemption_systemuser_createdby";
                public const string AdxInviteredemptionSystemuserCreatedonbehalfby = "adx_inviteredemption_systemuser_createdonbehalfby";
                public const string AdxInviteredemptionSystemuserModifiedby = "adx_inviteredemption_systemuser_modifiedby";
                public const string AdxInviteredemptionSystemuserModifiedonbehalfby = "adx_inviteredemption_systemuser_modifiedonbehalfby";
                public const string AdxInviteredemptionSystemuserOwninguser = "adx_inviteredemption_systemuser_owninguser";
                public const string AdxPortalcommentSystemuserCreatedby = "adx_portalcomment_systemuser_createdby";
                public const string AdxPortalcommentSystemuserCreatedonbehalfby = "adx_portalcomment_systemuser_createdonbehalfby";
                public const string AdxPortalcommentSystemuserModifiedby = "adx_portalcomment_systemuser_modifiedby";
                public const string AdxPortalcommentSystemuserModifiedonbehalfby = "adx_portalcomment_systemuser_modifiedonbehalfby";
                public const string AdxPortalcommentSystemuserOwninguser = "adx_portalcomment_systemuser_owninguser";
                public const string AdxWebformsessionSystemuser = "adx_webformsession_systemuser";
                public const string AIPluginUserSettingSystemUserSyst = "AIPluginUserSetting_SystemUser_Syst";
                public const string AnnotationOwningUser = "annotation_owning_user";
                public const string BizmapSystemuserBusinessid = "bizmap_systemuser_businessid";
                public const string ChatSystemuserCreatedby = "chat_systemuser_createdby";
                public const string ChatSystemuserCreatedonbehalfby = "chat_systemuser_createdonbehalfby";
                public const string ChatSystemuserModifiedby = "chat_systemuser_modifiedby";
                public const string ChatSystemuserModifiedonbehalfby = "chat_systemuser_modifiedonbehalfby";
                public const string ChatSystemuserOwninguser = "chat_systemuser_owninguser";
                public const string ContactOwningUser = "contact_owning_user";
                public const string CreatedbyAttributemap = "createdby_attributemap";
                public const string CreatedbyConnection = "createdby_connection";
                public const string CreatedbyConnectionRole = "createdby_connection_role";
                public const string CreatedbyCustomerRelationship = "createdby_customer_relationship";
                public const string CreatedbyEntitymap = "createdby_entitymap";
                public const string CreatedbyExpanderevent = "createdby_expanderevent";
                public const string CreatedbyPluginassembly = "createdby_pluginassembly";
                public const string CreatedbyPlugintracelog = "createdby_plugintracelog";
                public const string CreatedbyPlugintype = "createdby_plugintype";
                public const string CreatedbyPlugintypestatistic = "createdby_plugintypestatistic";
                public const string CreatedbyRelationshipRole = "createdby_relationship_role";
                public const string CreatedbyRelationshipRoleMap = "createdby_relationship_role_map";
                public const string CreatedbySdkmessage = "createdby_sdkmessage";
                public const string CreatedbySdkmessagefilter = "createdby_sdkmessagefilter";
                public const string CreatedbySdkmessagepair = "createdby_sdkmessagepair";
                public const string CreatedbySdkmessageprocessingstep = "createdby_sdkmessageprocessingstep";
                public const string CreatedbySdkmessageprocessingstepimage = "createdby_sdkmessageprocessingstepimage";
                public const string CreatedbySdkmessageprocessingstepsecureconfig = "createdby_sdkmessageprocessingstepsecureconfig";
                public const string CreatedbySdkmessagerequest = "createdby_sdkmessagerequest";
                public const string CreatedbySdkmessagerequestfield = "createdby_sdkmessagerequestfield";
                public const string CreatedbySdkmessageresponse = "createdby_sdkmessageresponse";
                public const string CreatedbySdkmessageresponsefield = "createdby_sdkmessageresponsefield";
                public const string CreatedbyServiceendpoint = "createdby_serviceendpoint";
                public const string CreatedonbehalfbyAttributemap = "createdonbehalfby_attributemap";
                public const string CreatedonbehalfbyCustomerRelationship = "createdonbehalfby_customer_relationship";
                public const string EmailAcceptingentitySystemuser = "email_acceptingentity_systemuser";
                public const string FlowmachinegroupPasswordChangedBy = "flowmachinegroup_PasswordChangedBy";
                public const string FlowmachinegroupRotationStartedBy = "flowmachinegroup_RotationStartedBy";
                public const string ImpersonatinguseridSdkmessageprocessingstep = "impersonatinguserid_sdkmessageprocessingstep";
                public const string ImportFileSystemUser = "ImportFile_SystemUser";
                public const string KnowledgearticlePrimaryauthorid = "knowledgearticle_primaryauthorid";
                public const string LkAccountbaseCreatedby = "lk_accountbase_createdby";
                public const string LkAccountbaseCreatedonbehalfby = "lk_accountbase_createdonbehalfby";
                public const string LkAccountbaseModifiedby = "lk_accountbase_modifiedby";
                public const string LkAccountbaseModifiedonbehalfby = "lk_accountbase_modifiedonbehalfby";
                public const string LkACIViewMapperCreatedby = "lk_ACIViewMapper_createdby";
                public const string LkACIViewMapperCreatedonbehalfby = "lk_ACIViewMapper_createdonbehalfby";
                public const string LkACIViewMapperModifiedby = "lk_ACIViewMapper_modifiedby";
                public const string LkACIViewMapperModifiedonbehalfby = "lk_ACIViewMapper_modifiedonbehalfby";
                public const string LkActioncardbaseCreatedby = "lk_actioncardbase_createdby";
                public const string LkActioncardbaseCreatedonbehalfby = "lk_actioncardbase_createdonbehalfby";
                public const string LkActioncardbaseModifiedby = "lk_actioncardbase_modifiedby";
                public const string LkActioncardbaseModifiedonbehalfby = "lk_actioncardbase_modifiedonbehalfby";
                public const string LkActivityfileattachmentCreatedby = "lk_activityfileattachment_createdby";
                public const string LkActivityfileattachmentCreatedonbehalfby = "lk_activityfileattachment_createdonbehalfby";
                public const string LkActivityfileattachmentModifiedby = "lk_activityfileattachment_modifiedby";
                public const string LkActivityfileattachmentModifiedonbehalfby = "lk_activityfileattachment_modifiedonbehalfby";
                public const string LkActivitypointerCreatedby = "lk_activitypointer_createdby";
                public const string LkActivitypointerCreatedonbehalfby = "lk_activitypointer_createdonbehalfby";
                public const string LkActivitypointerModifiedby = "lk_activitypointer_modifiedby";
                public const string LkActivitypointerModifiedonbehalfby = "lk_activitypointer_modifiedonbehalfby";
                public const string LkAdvancedsimilarityruleCreatedby = "lk_advancedsimilarityrule_createdby";
                public const string LkAdvancedsimilarityruleCreatedonbehalfby = "lk_advancedsimilarityrule_createdonbehalfby";
                public const string LkAdvancedsimilarityruleModifiedby = "lk_advancedsimilarityrule_modifiedby";
                public const string LkAdvancedsimilarityruleModifiedonbehalfby = "lk_advancedsimilarityrule_modifiedonbehalfby";
                public const string LkAdxExternalidentityCreatedby = "lk_adx_externalidentity_createdby";
                public const string LkAdxExternalidentityCreatedonbehalfby = "lk_adx_externalidentity_createdonbehalfby";
                public const string LkAdxExternalidentityModifiedby = "lk_adx_externalidentity_modifiedby";
                public const string LkAdxExternalidentityModifiedonbehalfby = "lk_adx_externalidentity_modifiedonbehalfby";
                public const string LkAdxInvitationCreatedby = "lk_adx_invitation_createdby";
                public const string LkAdxInvitationCreatedonbehalfby = "lk_adx_invitation_createdonbehalfby";
                public const string LkAdxInvitationModifiedby = "lk_adx_invitation_modifiedby";
                public const string LkAdxInvitationModifiedonbehalfby = "lk_adx_invitation_modifiedonbehalfby";
                public const string LkAdxSettingCreatedby = "lk_adx_setting_createdby";
                public const string LkAdxSettingCreatedonbehalfby = "lk_adx_setting_createdonbehalfby";
                public const string LkAdxSettingModifiedby = "lk_adx_setting_modifiedby";
                public const string LkAdxSettingModifiedonbehalfby = "lk_adx_setting_modifiedonbehalfby";
                public const string LkAdxWebformsessionCreatedby = "lk_adx_webformsession_createdby";
                public const string LkAdxWebformsessionCreatedonbehalfby = "lk_adx_webformsession_createdonbehalfby";
                public const string LkAdxWebformsessionModifiedby = "lk_adx_webformsession_modifiedby";
                public const string LkAdxWebformsessionModifiedonbehalfby = "lk_adx_webformsession_modifiedonbehalfby";
                public const string LkAgentconversationmessageCreatedby = "lk_agentconversationmessage_createdby";
                public const string LkAgentconversationmessageCreatedonbehalfby = "lk_agentconversationmessage_createdonbehalfby";
                public const string LkAgentconversationmessageModifiedby = "lk_agentconversationmessage_modifiedby";
                public const string LkAgentconversationmessageModifiedonbehalfby = "lk_agentconversationmessage_modifiedonbehalfby";
                public const string LkAgentconversationmessagefileCreatedby = "lk_agentconversationmessagefile_createdby";
                public const string LkAgentconversationmessagefileCreatedonbehalfby = "lk_agentconversationmessagefile_createdonbehalfby";
                public const string LkAgentconversationmessagefileModifiedby = "lk_agentconversationmessagefile_modifiedby";
                public const string LkAgentconversationmessagefileModifiedonbehalfby = "lk_agentconversationmessagefile_modifiedonbehalfby";
                public const string LkAgentfeeditemCreatedby = "lk_agentfeeditem_createdby";
                public const string LkAgentfeeditemCreatedonbehalfby = "lk_agentfeeditem_createdonbehalfby";
                public const string LkAgentfeeditemModifiedby = "lk_agentfeeditem_modifiedby";
                public const string LkAgentfeeditemModifiedonbehalfby = "lk_agentfeeditem_modifiedonbehalfby";
                public const string LkAgenthubgoalCreatedby = "lk_agenthubgoal_createdby";
                public const string LkAgenthubgoalCreatedonbehalfby = "lk_agenthubgoal_createdonbehalfby";
                public const string LkAgenthubgoalModifiedby = "lk_agenthubgoal_modifiedby";
                public const string LkAgenthubgoalModifiedonbehalfby = "lk_agenthubgoal_modifiedonbehalfby";
                public const string LkAgenthubinsightCreatedby = "lk_agenthubinsight_createdby";
                public const string LkAgenthubinsightCreatedonbehalfby = "lk_agenthubinsight_createdonbehalfby";
                public const string LkAgenthubinsightModifiedby = "lk_agenthubinsight_modifiedby";
                public const string LkAgenthubinsightModifiedonbehalfby = "lk_agenthubinsight_modifiedonbehalfby";
                public const string LkAgenthubmetricCreatedby = "lk_agenthubmetric_createdby";
                public const string LkAgenthubmetricCreatedonbehalfby = "lk_agenthubmetric_createdonbehalfby";
                public const string LkAgenthubmetricModifiedby = "lk_agenthubmetric_modifiedby";
                public const string LkAgenthubmetricModifiedonbehalfby = "lk_agenthubmetric_modifiedonbehalfby";
                public const string LkAgenticscenarioCreatedby = "lk_agenticscenario_createdby";
                public const string LkAgenticscenarioCreatedonbehalfby = "lk_agenticscenario_createdonbehalfby";
                public const string LkAgenticscenarioModifiedby = "lk_agenticscenario_modifiedby";
                public const string LkAgenticscenarioModifiedonbehalfby = "lk_agenticscenario_modifiedonbehalfby";
                public const string LkAgentmemoryCreatedby = "lk_agentmemory_createdby";
                public const string LkAgentmemoryCreatedonbehalfby = "lk_agentmemory_createdonbehalfby";
                public const string LkAgentmemoryModifiedby = "lk_agentmemory_modifiedby";
                public const string LkAgentmemoryModifiedonbehalfby = "lk_agentmemory_modifiedonbehalfby";
                public const string LkAgenttaskCreatedby = "lk_agenttask_createdby";
                public const string LkAgenttaskCreatedonbehalfby = "lk_agenttask_createdonbehalfby";
                public const string LkAgenttaskModifiedby = "lk_agenttask_modifiedby";
                public const string LkAgenttaskModifiedonbehalfby = "lk_agenttask_modifiedonbehalfby";
                public const string LkAicopilotCreatedby = "lk_aicopilot_createdby";
                public const string LkAicopilotCreatedonbehalfby = "lk_aicopilot_createdonbehalfby";
                public const string LkAicopilotModifiedby = "lk_aicopilot_modifiedby";
                public const string LkAicopilotModifiedonbehalfby = "lk_aicopilot_modifiedonbehalfby";
                public const string LkAiinsightcardCreatedby = "lk_aiinsightcard_createdby";
                public const string LkAiinsightcardCreatedonbehalfby = "lk_aiinsightcard_createdonbehalfby";
                public const string LkAiinsightcardModifiedby = "lk_aiinsightcard_modifiedby";
                public const string LkAiinsightcardModifiedonbehalfby = "lk_aiinsightcard_modifiedonbehalfby";
                public const string LkAipluginCreatedby = "lk_aiplugin_createdby";
                public const string LkAipluginCreatedonbehalfby = "lk_aiplugin_createdonbehalfby";
                public const string LkAipluginModifiedby = "lk_aiplugin_modifiedby";
                public const string LkAipluginModifiedonbehalfby = "lk_aiplugin_modifiedonbehalfby";
                public const string LkAipluginauthCreatedby = "lk_aipluginauth_createdby";
                public const string LkAipluginauthCreatedonbehalfby = "lk_aipluginauth_createdonbehalfby";
                public const string LkAipluginauthModifiedby = "lk_aipluginauth_modifiedby";
                public const string LkAipluginauthModifiedonbehalfby = "lk_aipluginauth_modifiedonbehalfby";
                public const string LkAipluginconversationstarterCreatedby = "lk_aipluginconversationstarter_createdby";
                public const string LkAipluginconversationstarterCreatedonbehalfby = "lk_aipluginconversationstarter_createdonbehalfby";
                public const string LkAipluginconversationstarterModifiedby = "lk_aipluginconversationstarter_modifiedby";
                public const string LkAipluginconversationstarterModifiedonbehalfby = "lk_aipluginconversationstarter_modifiedonbehalfby";
                public const string LkAipluginconversationstartermappingCreatedby = "lk_aipluginconversationstartermapping_createdby";
                public const string LkAipluginconversationstartermappingCreatedonbehalfby = "lk_aipluginconversationstartermapping_createdonbehalfby";
                public const string LkAipluginconversationstartermappingModifiedby = "lk_aipluginconversationstartermapping_modifiedby";
                public const string LkAipluginconversationstartermappingModifiedonbehalfby = "lk_aipluginconversationstartermapping_modifiedonbehalfby";
                public const string LkAipluginexternalschemaCreatedby = "lk_aipluginexternalschema_createdby";
                public const string LkAipluginexternalschemaCreatedonbehalfby = "lk_aipluginexternalschema_createdonbehalfby";
                public const string LkAipluginexternalschemaModifiedby = "lk_aipluginexternalschema_modifiedby";
                public const string LkAipluginexternalschemaModifiedonbehalfby = "lk_aipluginexternalschema_modifiedonbehalfby";
                public const string LkAipluginexternalschemapropertyCreatedby = "lk_aipluginexternalschemaproperty_createdby";
                public const string LkAipluginexternalschemapropertyCreatedonbehalfby = "lk_aipluginexternalschemaproperty_createdonbehalfby";
                public const string LkAipluginexternalschemapropertyModifiedby = "lk_aipluginexternalschemaproperty_modifiedby";
                public const string LkAipluginexternalschemapropertyModifiedonbehalfby = "lk_aipluginexternalschemaproperty_modifiedonbehalfby";
                public const string LkAiplugingovernanceCreatedby = "lk_aiplugingovernance_createdby";
                public const string LkAiplugingovernanceCreatedonbehalfby = "lk_aiplugingovernance_createdonbehalfby";
                public const string LkAiplugingovernanceModifiedby = "lk_aiplugingovernance_modifiedby";
                public const string LkAiplugingovernanceModifiedonbehalfby = "lk_aiplugingovernance_modifiedonbehalfby";
                public const string LkAiplugingovernanceextCreatedby = "lk_aiplugingovernanceext_createdby";
                public const string LkAiplugingovernanceextCreatedonbehalfby = "lk_aiplugingovernanceext_createdonbehalfby";
                public const string LkAiplugingovernanceextModifiedby = "lk_aiplugingovernanceext_modifiedby";
                public const string LkAiplugingovernanceextModifiedonbehalfby = "lk_aiplugingovernanceext_modifiedonbehalfby";
                public const string LkAiplugininstanceCreatedby = "lk_aiplugininstance_createdby";
                public const string LkAiplugininstanceCreatedonbehalfby = "lk_aiplugininstance_createdonbehalfby";
                public const string LkAiplugininstanceModifiedby = "lk_aiplugininstance_modifiedby";
                public const string LkAiplugininstanceModifiedonbehalfby = "lk_aiplugininstance_modifiedonbehalfby";
                public const string LkAipluginoperationCreatedby = "lk_aipluginoperation_createdby";
                public const string LkAipluginoperationCreatedonbehalfby = "lk_aipluginoperation_createdonbehalfby";
                public const string LkAipluginoperationModifiedby = "lk_aipluginoperation_modifiedby";
                public const string LkAipluginoperationModifiedonbehalfby = "lk_aipluginoperation_modifiedonbehalfby";
                public const string LkAipluginoperationparameterCreatedby = "lk_aipluginoperationparameter_createdby";
                public const string LkAipluginoperationparameterCreatedonbehalfby = "lk_aipluginoperationparameter_createdonbehalfby";
                public const string LkAipluginoperationparameterModifiedby = "lk_aipluginoperationparameter_modifiedby";
                public const string LkAipluginoperationparameterModifiedonbehalfby = "lk_aipluginoperationparameter_modifiedonbehalfby";
                public const string LkAipluginoperationresponsetemplateCreatedby = "lk_aipluginoperationresponsetemplate_createdby";
                public const string LkAipluginoperationresponsetemplateCreatedonbehalfby = "lk_aipluginoperationresponsetemplate_createdonbehalfby";
                public const string LkAipluginoperationresponsetemplateModifiedby = "lk_aipluginoperationresponsetemplate_modifiedby";
                public const string LkAipluginoperationresponsetemplateModifiedonbehalfby = "lk_aipluginoperationresponsetemplate_modifiedonbehalfby";
                public const string LkAiplugintitleCreatedby = "lk_aiplugintitle_createdby";
                public const string LkAiplugintitleCreatedonbehalfby = "lk_aiplugintitle_createdonbehalfby";
                public const string LkAiplugintitleModifiedby = "lk_aiplugintitle_modifiedby";
                public const string LkAiplugintitleModifiedonbehalfby = "lk_aiplugintitle_modifiedonbehalfby";
                public const string LkAipluginusersettingCreatedby = "lk_aipluginusersetting_createdby";
                public const string LkAipluginusersettingCreatedonbehalfby = "lk_aipluginusersetting_createdonbehalfby";
                public const string LkAipluginusersettingModifiedby = "lk_aipluginusersetting_modifiedby";
                public const string LkAipluginusersettingModifiedonbehalfby = "lk_aipluginusersetting_modifiedonbehalfby";
                public const string LkAiskillconfigCreatedby = "lk_aiskillconfig_createdby";
                public const string LkAiskillconfigCreatedonbehalfby = "lk_aiskillconfig_createdonbehalfby";
                public const string LkAiskillconfigModifiedby = "lk_aiskillconfig_modifiedby";
                public const string LkAiskillconfigModifiedonbehalfby = "lk_aiskillconfig_modifiedonbehalfby";
                public const string LkAllowedmcpclientCreatedby = "lk_allowedmcpclient_createdby";
                public const string LkAllowedmcpclientCreatedonbehalfby = "lk_allowedmcpclient_createdonbehalfby";
                public const string LkAllowedmcpclientModifiedby = "lk_allowedmcpclient_modifiedby";
                public const string LkAllowedmcpclientModifiedonbehalfby = "lk_allowedmcpclient_modifiedonbehalfby";
                public const string LkAnnotationbaseCreatedby = "lk_annotationbase_createdby";
                public const string LkAnnotationbaseCreatedonbehalfby = "lk_annotationbase_createdonbehalfby";
                public const string LkAnnotationbaseModifiedby = "lk_annotationbase_modifiedby";
                public const string LkAnnotationbaseModifiedonbehalfby = "lk_annotationbase_modifiedonbehalfby";
                public const string LkAnnualfiscalcalendarCreatedby = "lk_annualfiscalcalendar_createdby";
                public const string LkAnnualfiscalcalendarCreatedonbehalfby = "lk_annualfiscalcalendar_createdonbehalfby";
                public const string LkAnnualfiscalcalendarModifiedby = "lk_annualfiscalcalendar_modifiedby";
                public const string LkAnnualfiscalcalendarModifiedonbehalfby = "lk_annualfiscalcalendar_modifiedonbehalfby";
                public const string LkAnnualfiscalcalendarSalespersonid = "lk_annualfiscalcalendar_salespersonid";
                public const string LkAnyprivilegeentityCreatedby = "lk_anyprivilegeentity_createdby";
                public const string LkAnyprivilegeentityCreatedonbehalfby = "lk_anyprivilegeentity_createdonbehalfby";
                public const string LkAnyprivilegeentityModifiedby = "lk_anyprivilegeentity_modifiedby";
                public const string LkAnyprivilegeentityModifiedonbehalfby = "lk_anyprivilegeentity_modifiedonbehalfby";
                public const string LkAppactionCreatedby = "lk_appaction_createdby";
                public const string LkAppactionCreatedonbehalfby = "lk_appaction_createdonbehalfby";
                public const string LkAppactionModifiedby = "lk_appaction_modifiedby";
                public const string LkAppactionModifiedonbehalfby = "lk_appaction_modifiedonbehalfby";
                public const string LkAppactionmigrationCreatedby = "lk_appactionmigration_createdby";
                public const string LkAppactionmigrationCreatedonbehalfby = "lk_appactionmigration_createdonbehalfby";
                public const string LkAppactionmigrationModifiedby = "lk_appactionmigration_modifiedby";
                public const string LkAppactionmigrationModifiedonbehalfby = "lk_appactionmigration_modifiedonbehalfby";
                public const string LkAppactionruleCreatedby = "lk_appactionrule_createdby";
                public const string LkAppactionruleCreatedonbehalfby = "lk_appactionrule_createdonbehalfby";
                public const string LkAppactionruleModifiedby = "lk_appactionrule_modifiedby";
                public const string LkAppactionruleModifiedonbehalfby = "lk_appactionrule_modifiedonbehalfby";
                public const string LkAppconfigCreatedby = "lk_appconfig_createdby";
                public const string LkAppconfigCreatedonbehalfby = "lk_appconfig_createdonbehalfby";
                public const string LkAppconfigModifiedby = "lk_appconfig_modifiedby";
                public const string LkAppconfigModifiedonbehalfby = "lk_appconfig_modifiedonbehalfby";
                public const string LkAppconfiginstanceCreatedby = "lk_appconfiginstance_createdby";
                public const string LkAppconfiginstanceCreatedonbehalfby = "lk_appconfiginstance_createdonbehalfby";
                public const string LkAppconfiginstanceModifiedby = "lk_appconfiginstance_modifiedby";
                public const string LkAppconfiginstanceModifiedonbehalfby = "lk_appconfiginstance_modifiedonbehalfby";
                public const string LkAppconfigmasterCreatedby = "lk_appconfigmaster_createdby";
                public const string LkAppconfigmasterCreatedonbehalfby = "lk_appconfigmaster_createdonbehalfby";
                public const string LkAppconfigmasterModifiedby = "lk_appconfigmaster_modifiedby";
                public const string LkAppconfigmasterModifiedonbehalfby = "lk_appconfigmaster_modifiedonbehalfby";
                public const string LkAppelementCreatedby = "lk_appelement_createdby";
                public const string LkAppelementCreatedonbehalfby = "lk_appelement_createdonbehalfby";
                public const string LkAppelementModifiedby = "lk_appelement_modifiedby";
                public const string LkAppelementModifiedonbehalfby = "lk_appelement_modifiedonbehalfby";
                public const string LkAppentitysearchviewCreatedby = "lk_appentitysearchview_createdby";
                public const string LkAppentitysearchviewCreatedonbehalfby = "lk_appentitysearchview_createdonbehalfby";
                public const string LkAppentitysearchviewModifiedby = "lk_appentitysearchview_modifiedby";
                public const string LkAppentitysearchviewModifiedonbehalfby = "lk_appentitysearchview_modifiedonbehalfby";
                public const string LkApplicationfileCreatedby = "lk_applicationfile_createdby";
                public const string LkApplicationfileCreatedonbehalfby = "lk_applicationfile_createdonbehalfby";
                public const string LkApplicationfileModifiedby = "lk_applicationfile_modifiedby";
                public const string LkApplicationfileModifiedonbehalfby = "lk_applicationfile_modifiedonbehalfby";
                public const string LkApplicationuserCreatedby = "lk_applicationuser_createdby";
                public const string LkApplicationuserCreatedonbehalfby = "lk_applicationuser_createdonbehalfby";
                public const string LkApplicationuserModifiedby = "lk_applicationuser_modifiedby";
                public const string LkApplicationuserModifiedonbehalfby = "lk_applicationuser_modifiedonbehalfby";
                public const string LkAppmoduleCreatedby = "lk_appmodule_createdby";
                public const string LkAppmoduleCreatedonbehalfby = "lk_appmodule_createdonbehalfby";
                public const string LkAppmoduleModifiedby = "lk_appmodule_modifiedby";
                public const string LkAppmoduleModifiedonbehalfby = "lk_appmodule_modifiedonbehalfby";
                public const string LkAppmodulecomponentCreatedby = "lk_appmodulecomponent_createdby";
                public const string LkAppmodulecomponentCreatedonbehalfby = "lk_appmodulecomponent_createdonbehalfby";
                public const string LkAppmodulecomponentModifiedby = "lk_appmodulecomponent_modifiedby";
                public const string LkAppmodulecomponentModifiedonbehalfby = "lk_appmodulecomponent_modifiedonbehalfby";
                public const string LkAppmodulecomponentedgeCreatedby = "lk_appmodulecomponentedge_createdby";
                public const string LkAppmodulecomponentedgeCreatedonbehalfby = "lk_appmodulecomponentedge_createdonbehalfby";
                public const string LkAppmodulecomponentedgeModifiedby = "lk_appmodulecomponentedge_modifiedby";
                public const string LkAppmodulecomponentedgeModifiedonbehalfby = "lk_appmodulecomponentedge_modifiedonbehalfby";
                public const string LkAppmodulecomponentnodeCreatedby = "lk_appmodulecomponentnode_createdby";
                public const string LkAppmodulecomponentnodeCreatedonbehalfby = "lk_appmodulecomponentnode_createdonbehalfby";
                public const string LkAppmodulecomponentnodeModifiedby = "lk_appmodulecomponentnode_modifiedby";
                public const string LkAppmodulecomponentnodeModifiedonbehalfby = "lk_appmodulecomponentnode_modifiedonbehalfby";
                public const string LkAppnotificationCreatedby = "lk_appnotification_createdby";
                public const string LkAppnotificationCreatedonbehalfby = "lk_appnotification_createdonbehalfby";
                public const string LkAppnotificationModifiedby = "lk_appnotification_modifiedby";
                public const string LkAppnotificationModifiedonbehalfby = "lk_appnotification_modifiedonbehalfby";
                public const string LkAppnotificationsignalCreatedby = "lk_appnotificationsignal_createdby";
                public const string LkAppnotificationsignalCreatedonbehalfby = "lk_appnotificationsignal_createdonbehalfby";
                public const string LkAppnotificationsignalModifiedby = "lk_appnotificationsignal_modifiedby";
                public const string LkAppnotificationsignalModifiedonbehalfby = "lk_appnotificationsignal_modifiedonbehalfby";
                public const string LkAppointmentCreatedby = "lk_appointment_createdby";
                public const string LkAppointmentCreatedonbehalfby = "lk_appointment_createdonbehalfby";
                public const string LkAppointmentModifiedby = "lk_appointment_modifiedby";
                public const string LkAppointmentModifiedonbehalfby = "lk_appointment_modifiedonbehalfby";
                public const string LkApprovalprocessCreatedby = "lk_approvalprocess_createdby";
                public const string LkApprovalprocessCreatedonbehalfby = "lk_approvalprocess_createdonbehalfby";
                public const string LkApprovalprocessModifiedby = "lk_approvalprocess_modifiedby";
                public const string LkApprovalprocessModifiedonbehalfby = "lk_approvalprocess_modifiedonbehalfby";
                public const string LkApprovalstageapprovalCreatedby = "lk_approvalstageapproval_createdby";
                public const string LkApprovalstageapprovalCreatedonbehalfby = "lk_approvalstageapproval_createdonbehalfby";
                public const string LkApprovalstageapprovalModifiedby = "lk_approvalstageapproval_modifiedby";
                public const string LkApprovalstageapprovalModifiedonbehalfby = "lk_approvalstageapproval_modifiedonbehalfby";
                public const string LkApprovalstageconditionCreatedby = "lk_approvalstagecondition_createdby";
                public const string LkApprovalstageconditionCreatedonbehalfby = "lk_approvalstagecondition_createdonbehalfby";
                public const string LkApprovalstageconditionModifiedby = "lk_approvalstagecondition_modifiedby";
                public const string LkApprovalstageconditionModifiedonbehalfby = "lk_approvalstagecondition_modifiedonbehalfby";
                public const string LkApprovalstageintelligentCreatedby = "lk_approvalstageintelligent_createdby";
                public const string LkApprovalstageintelligentCreatedonbehalfby = "lk_approvalstageintelligent_createdonbehalfby";
                public const string LkApprovalstageintelligentModifiedby = "lk_approvalstageintelligent_modifiedby";
                public const string LkApprovalstageintelligentModifiedonbehalfby = "lk_approvalstageintelligent_modifiedonbehalfby";
                public const string LkApprovalstageorderCreatedby = "lk_approvalstageorder_createdby";
                public const string LkApprovalstageorderCreatedonbehalfby = "lk_approvalstageorder_createdonbehalfby";
                public const string LkApprovalstageorderModifiedby = "lk_approvalstageorder_modifiedby";
                public const string LkApprovalstageorderModifiedonbehalfby = "lk_approvalstageorder_modifiedonbehalfby";
                public const string LkAppsettingCreatedby = "lk_appsetting_createdby";
                public const string LkAppsettingCreatedonbehalfby = "lk_appsetting_createdonbehalfby";
                public const string LkAppsettingModifiedby = "lk_appsetting_modifiedby";
                public const string LkAppsettingModifiedonbehalfby = "lk_appsetting_modifiedonbehalfby";
                public const string LkAppusersettingCreatedby = "lk_appusersetting_createdby";
                public const string LkAppusersettingCreatedonbehalfby = "lk_appusersetting_createdonbehalfby";
                public const string LkAppusersettingModifiedby = "lk_appusersetting_modifiedby";
                public const string LkAppusersettingModifiedonbehalfby = "lk_appusersetting_modifiedonbehalfby";
                public const string LkArchivecleanupinfoCreatedby = "lk_archivecleanupinfo_createdby";
                public const string LkArchivecleanupinfoCreatedonbehalfby = "lk_archivecleanupinfo_createdonbehalfby";
                public const string LkArchivecleanupinfoModifiedby = "lk_archivecleanupinfo_modifiedby";
                public const string LkArchivecleanupinfoModifiedonbehalfby = "lk_archivecleanupinfo_modifiedonbehalfby";
                public const string LkArchivecleanupoperationCreatedby = "lk_archivecleanupoperation_createdby";
                public const string LkArchivecleanupoperationCreatedonbehalfby = "lk_archivecleanupoperation_createdonbehalfby";
                public const string LkArchivecleanupoperationModifiedby = "lk_archivecleanupoperation_modifiedby";
                public const string LkArchivecleanupoperationModifiedonbehalfby = "lk_archivecleanupoperation_modifiedonbehalfby";
                public const string LkAsyncoperationCreatedby = "lk_asyncoperation_createdby";
                public const string LkAsyncoperationCreatedonbehalfby = "lk_asyncoperation_createdonbehalfby";
                public const string LkAsyncoperationModifiedby = "lk_asyncoperation_modifiedby";
                public const string LkAsyncoperationModifiedonbehalfby = "lk_asyncoperation_modifiedonbehalfby";
                public const string LkAthenareconciliationinfoCreatedby = "lk_athenareconciliationinfo_createdby";
                public const string LkAthenareconciliationinfoCreatedonbehalfby = "lk_athenareconciliationinfo_createdonbehalfby";
                public const string LkAthenareconciliationinfoModifiedby = "lk_athenareconciliationinfo_modifiedby";
                public const string LkAthenareconciliationinfoModifiedonbehalfby = "lk_athenareconciliationinfo_modifiedonbehalfby";
                public const string LkAttributeclusterconfigCreatedby = "lk_attributeclusterconfig_createdby";
                public const string LkAttributeclusterconfigCreatedonbehalfby = "lk_attributeclusterconfig_createdonbehalfby";
                public const string LkAttributeclusterconfigModifiedby = "lk_attributeclusterconfig_modifiedby";
                public const string LkAttributeclusterconfigModifiedonbehalfby = "lk_attributeclusterconfig_modifiedonbehalfby";
                public const string LkAuditCallinguserid = "lk_audit_callinguserid";
                public const string LkAuditUserid = "lk_audit_userid";
                public const string LkAuthorizationserverCreatedby = "lk_authorizationserver_createdby";
                public const string LkAuthorizationserverCreatedonbehalfby = "lk_authorizationserver_createdonbehalfby";
                public const string LkAuthorizationserverModifiedby = "lk_authorizationserver_modifiedby";
                public const string LkAuthorizationserverModifiedonbehalfby = "lk_authorizationserver_modifiedonbehalfby";
                public const string LkAzureserviceconnectionCreatedby = "lk_azureserviceconnection_createdby";
                public const string LkAzureserviceconnectionCreatedonbehalfby = "lk_azureserviceconnection_createdonbehalfby";
                public const string LkAzureserviceconnectionModifiedby = "lk_azureserviceconnection_modifiedby";
                public const string LkAzureserviceconnectionModifiedonbehalfby = "lk_azureserviceconnection_modifiedonbehalfby";
                public const string LkBackgroundoperationCreatedby = "lk_backgroundoperation_createdby";
                public const string LkBackgroundoperationCreatedonbehalfby = "lk_backgroundoperation_createdonbehalfby";
                public const string LkBackgroundoperationModifiedby = "lk_backgroundoperation_modifiedby";
                public const string LkBackgroundoperationModifiedonbehalfby = "lk_backgroundoperation_modifiedonbehalfby";
                public const string LkBotCreatedby = "lk_bot_createdby";
                public const string LkBotCreatedonbehalfby = "lk_bot_createdonbehalfby";
                public const string LkBotModifiedby = "lk_bot_modifiedby";
                public const string LkBotModifiedonbehalfby = "lk_bot_modifiedonbehalfby";
                public const string LkBotcomponentCreatedby = "lk_botcomponent_createdby";
                public const string LkBotcomponentCreatedonbehalfby = "lk_botcomponent_createdonbehalfby";
                public const string LkBotcomponentModifiedby = "lk_botcomponent_modifiedby";
                public const string LkBotcomponentModifiedonbehalfby = "lk_botcomponent_modifiedonbehalfby";
                public const string LkBotcomponentcollectionCreatedby = "lk_botcomponentcollection_createdby";
                public const string LkBotcomponentcollectionCreatedonbehalfby = "lk_botcomponentcollection_createdonbehalfby";
                public const string LkBotcomponentcollectionModifiedby = "lk_botcomponentcollection_modifiedby";
                public const string LkBotcomponentcollectionModifiedonbehalfby = "lk_botcomponentcollection_modifiedonbehalfby";
                public const string LkBulkarchiveconfigCreatedby = "lk_bulkarchiveconfig_createdby";
                public const string LkBulkarchiveconfigCreatedonbehalfby = "lk_bulkarchiveconfig_createdonbehalfby";
                public const string LkBulkarchiveconfigModifiedby = "lk_bulkarchiveconfig_modifiedby";
                public const string LkBulkarchiveconfigModifiedonbehalfby = "lk_bulkarchiveconfig_modifiedonbehalfby";
                public const string LkBulkarchivefailuredetailCreatedby = "lk_bulkarchivefailuredetail_createdby";
                public const string LkBulkarchivefailuredetailCreatedonbehalfby = "lk_bulkarchivefailuredetail_createdonbehalfby";
                public const string LkBulkarchivefailuredetailModifiedby = "lk_bulkarchivefailuredetail_modifiedby";
                public const string LkBulkarchivefailuredetailModifiedonbehalfby = "lk_bulkarchivefailuredetail_modifiedonbehalfby";
                public const string LkBulkarchiveoperationCreatedby = "lk_bulkarchiveoperation_createdby";
                public const string LkBulkarchiveoperationCreatedonbehalfby = "lk_bulkarchiveoperation_createdonbehalfby";
                public const string LkBulkarchiveoperationModifiedby = "lk_bulkarchiveoperation_modifiedby";
                public const string LkBulkarchiveoperationModifiedonbehalfby = "lk_bulkarchiveoperation_modifiedonbehalfby";
                public const string LkBulkarchiveoperationdetailCreatedby = "lk_bulkarchiveoperationdetail_createdby";
                public const string LkBulkarchiveoperationdetailCreatedonbehalfby = "lk_bulkarchiveoperationdetail_createdonbehalfby";
                public const string LkBulkarchiveoperationdetailModifiedby = "lk_bulkarchiveoperationdetail_modifiedby";
                public const string LkBulkarchiveoperationdetailModifiedonbehalfby = "lk_bulkarchiveoperationdetail_modifiedonbehalfby";
                public const string LkBulkdeleteoperationCreatedonbehalfby = "lk_bulkdeleteoperation_createdonbehalfby";
                public const string LkBulkdeleteoperationModifiedonbehalfby = "lk_bulkdeleteoperation_modifiedonbehalfby";
                public const string LkBulkdeleteoperationbaseCreatedby = "lk_bulkdeleteoperationbase_createdby";
                public const string LkBulkdeleteoperationbaseModifiedby = "lk_bulkdeleteoperationbase_modifiedby";
                public const string LkBusinessprocessCreatedby = "lk_businessprocess_createdby";
                public const string LkBusinessprocessCreatedonbehalfby = "lk_businessprocess_createdonbehalfby";
                public const string LkBusinessprocessModifiedby = "lk_businessprocess_modifiedby";
                public const string LkBusinessprocessModifiedonbehalfby = "lk_businessprocess_modifiedonbehalfby";
                public const string LkBusinessprocessflowinstancebaseCreatedby = "lk_businessprocessflowinstancebase_createdby";
                public const string LkBusinessprocessflowinstancebaseCreatedonbehalfby = "lk_businessprocessflowinstancebase_createdonbehalfby";
                public const string LkBusinessprocessflowinstancebaseModifiedby = "lk_businessprocessflowinstancebase_modifiedby";
                public const string LkBusinessprocessflowinstancebaseModifiedonbehalfby = "lk_businessprocessflowinstancebase_modifiedonbehalfby";
                public const string LkBusinessunitCreatedonbehalfby = "lk_businessunit_createdonbehalfby";
                public const string LkBusinessunitModifiedonbehalfby = "lk_businessunit_modifiedonbehalfby";
                public const string LkBusinessunitbaseCreatedby = "lk_businessunitbase_createdby";
                public const string LkBusinessunitbaseModifiedby = "lk_businessunitbase_modifiedby";
                public const string LkBusinessunitnewsarticleCreatedonbehalfby = "lk_businessunitnewsarticle_createdonbehalfby";
                public const string LkBusinessunitnewsarticleModifiedonbehalfby = "lk_businessunitnewsarticle_modifiedonbehalfby";
                public const string LkBusinessunitnewsarticlebaseCreatedby = "lk_businessunitnewsarticlebase_createdby";
                public const string LkBusinessunitnewsarticlebaseModifiedby = "lk_businessunitnewsarticlebase_modifiedby";
                public const string LkCalendarCreatedby = "lk_calendar_createdby";
                public const string LkCalendarCreatedonbehalfby = "lk_calendar_createdonbehalfby";
                public const string LkCalendarModifiedby = "lk_calendar_modifiedby";
                public const string LkCalendarModifiedonbehalfby = "lk_calendar_modifiedonbehalfby";
                public const string LkCalendarruleCreatedby = "lk_calendarrule_createdby";
                public const string LkCalendarruleCreatedonbehalfby = "lk_calendarrule_createdonbehalfby";
                public const string LkCalendarruleModifiedby = "lk_calendarrule_modifiedby";
                public const string LkCalendarruleModifiedonbehalfby = "lk_calendarrule_modifiedonbehalfby";
                public const string LkCallbackregistrationCreatedby = "lk_callbackregistration_createdby";
                public const string LkCallbackregistrationCreatedonbehalfby = "lk_callbackregistration_createdonbehalfby";
                public const string LkCallbackregistrationModifiedby = "lk_callbackregistration_modifiedby";
                public const string LkCallbackregistrationModifiedonbehalfby = "lk_callbackregistration_modifiedonbehalfby";
                public const string LkCanvasappextendedmetadataCreatedby = "lk_canvasappextendedmetadata_createdby";
                public const string LkCanvasappextendedmetadataCreatedonbehalfby = "lk_canvasappextendedmetadata_createdonbehalfby";
                public const string LkCanvasappextendedmetadataModifiedby = "lk_canvasappextendedmetadata_modifiedby";
                public const string LkCanvasappextendedmetadataModifiedonbehalfby = "lk_canvasappextendedmetadata_modifiedonbehalfby";
                public const string LkCardCreatedby = "lk_card_createdby";
                public const string LkCardCreatedonbehalfby = "lk_card_createdonbehalfby";
                public const string LkCardModifiedby = "lk_card_modifiedby";
                public const string LkCardModifiedonbehalfby = "lk_card_modifiedonbehalfby";
                public const string LkCardstateitemCreatedby = "lk_cardstateitem_createdby";
                public const string LkCardstateitemCreatedonbehalfby = "lk_cardstateitem_createdonbehalfby";
                public const string LkCardstateitemModifiedby = "lk_cardstateitem_modifiedby";
                public const string LkCardstateitemModifiedonbehalfby = "lk_cardstateitem_modifiedonbehalfby";
                public const string LkCardtypeCreatedby = "lk_cardtype_createdby";
                public const string LkCardtypeCreatedonbehalfby = "lk_cardtype_createdonbehalfby";
                public const string LkCardtypeModifiedby = "lk_cardtype_modifiedby";
                public const string LkCardtypeModifiedonbehalfby = "lk_cardtype_modifiedonbehalfby";
                public const string LkCatalogCreatedby = "lk_catalog_createdby";
                public const string LkCatalogCreatedonbehalfby = "lk_catalog_createdonbehalfby";
                public const string LkCatalogModifiedby = "lk_catalog_modifiedby";
                public const string LkCatalogModifiedonbehalfby = "lk_catalog_modifiedonbehalfby";
                public const string LkCatalogassignmentCreatedby = "lk_catalogassignment_createdby";
                public const string LkCatalogassignmentCreatedonbehalfby = "lk_catalogassignment_createdonbehalfby";
                public const string LkCatalogassignmentModifiedby = "lk_catalogassignment_modifiedby";
                public const string LkCatalogassignmentModifiedonbehalfby = "lk_catalogassignment_modifiedonbehalfby";
                public const string LkCategoryCreatedby = "lk_category_createdby";
                public const string LkCategoryCreatedonbehalfby = "lk_category_createdonbehalfby";
                public const string LkCategoryModifiedby = "lk_category_modifiedby";
                public const string LkCategoryModifiedonbehalfby = "lk_category_modifiedonbehalfby";
                public const string LkCertificatecredentialCreatedby = "lk_certificatecredential_createdby";
                public const string LkCertificatecredentialCreatedonbehalfby = "lk_certificatecredential_createdonbehalfby";
                public const string LkCertificatecredentialModifiedby = "lk_certificatecredential_modifiedby";
                public const string LkCertificatecredentialModifiedonbehalfby = "lk_certificatecredential_modifiedonbehalfby";
                public const string LkChannelaccessprofileCreatedby = "lk_channelaccessprofile_createdby";
                public const string LkChannelaccessprofileCreatedonbehalfby = "lk_channelaccessprofile_createdonbehalfby";
                public const string LkChannelaccessprofileModifiedby = "lk_channelaccessprofile_modifiedby";
                public const string LkChannelaccessprofileModifiedonbehalfby = "lk_channelaccessprofile_modifiedonbehalfby";
                public const string LkChannelPropertyCreatedby = "lk_ChannelProperty_createdby";
                public const string LkChannelPropertyCreatedonbehalfby = "lk_ChannelProperty_createdonbehalfby";
                public const string LkChannelPropertyModifiedby = "lk_ChannelProperty_modifiedby";
                public const string LkChannelPropertyModifiedonbehalfby = "lk_ChannelProperty_modifiedonbehalfby";
                public const string LkChannelPropertyGroupCreatedby = "lk_ChannelPropertyGroup_createdby";
                public const string LkChannelPropertyGroupCreatedonbehalfby = "lk_ChannelPropertyGroup_createdonbehalfby";
                public const string LkChannelPropertyGroupModifiedby = "lk_ChannelPropertyGroup_modifiedby";
                public const string LkChannelPropertyGroupModifiedonbehalfby = "lk_ChannelPropertyGroup_modifiedonbehalfby";
                public const string LkColumnmappingCreatedby = "lk_columnmapping_createdby";
                public const string LkColumnmappingCreatedonbehalfby = "lk_columnmapping_createdonbehalfby";
                public const string LkColumnmappingModifiedby = "lk_columnmapping_modifiedby";
                public const string LkColumnmappingModifiedonbehalfby = "lk_columnmapping_modifiedonbehalfby";
                public const string LkCommentCreatedby = "lk_comment_createdby";
                public const string LkCommentCreatedonbehalfby = "lk_comment_createdonbehalfby";
                public const string LkCommentModifiedby = "lk_comment_modifiedby";
                public const string LkCommentModifiedonbehalfby = "lk_comment_modifiedonbehalfby";
                public const string LkComponentchangesetpayloadCreatedby = "lk_componentchangesetpayload_createdby";
                public const string LkComponentchangesetpayloadCreatedonbehalfby = "lk_componentchangesetpayload_createdonbehalfby";
                public const string LkComponentchangesetpayloadModifiedby = "lk_componentchangesetpayload_modifiedby";
                public const string LkComponentchangesetpayloadModifiedonbehalfby = "lk_componentchangesetpayload_modifiedonbehalfby";
                public const string LkComponentchangesetversionCreatedby = "lk_componentchangesetversion_createdby";
                public const string LkComponentchangesetversionCreatedonbehalfby = "lk_componentchangesetversion_createdonbehalfby";
                public const string LkComponentchangesetversionModifiedby = "lk_componentchangesetversion_modifiedby";
                public const string LkComponentchangesetversionModifiedonbehalfby = "lk_componentchangesetversion_modifiedonbehalfby";
                public const string LkComponentversionCreatedby = "lk_componentversion_createdby";
                public const string LkComponentversionModifiedby = "lk_componentversion_modifiedby";
                public const string LkComponentversionnrddatasourceCreatedby = "lk_componentversionnrddatasource_createdby";
                public const string LkComponentversionnrddatasourceCreatedonbehalfby = "lk_componentversionnrddatasource_createdonbehalfby";
                public const string LkComponentversionnrddatasourceModifiedby = "lk_componentversionnrddatasource_modifiedby";
                public const string LkComponentversionnrddatasourceModifiedonbehalfby = "lk_componentversionnrddatasource_modifiedonbehalfby";
                public const string LkComputeruseagentCreatedby = "lk_computeruseagent_createdby";
                public const string LkComputeruseagentCreatedonbehalfby = "lk_computeruseagent_createdonbehalfby";
                public const string LkComputeruseagentModifiedby = "lk_computeruseagent_modifiedby";
                public const string LkComputeruseagentModifiedonbehalfby = "lk_computeruseagent_modifiedonbehalfby";
                public const string LkConnectionbaseCreatedonbehalfby = "lk_connectionbase_createdonbehalfby";
                public const string LkConnectionbaseModifiedonbehalfby = "lk_connectionbase_modifiedonbehalfby";
                public const string LkConnectioninstanceCreatedby = "lk_connectioninstance_createdby";
                public const string LkConnectioninstanceCreatedonbehalfby = "lk_connectioninstance_createdonbehalfby";
                public const string LkConnectioninstanceModifiedby = "lk_connectioninstance_modifiedby";
                public const string LkConnectioninstanceModifiedonbehalfby = "lk_connectioninstance_modifiedonbehalfby";
                public const string LkConnectionreferenceCreatedby = "lk_connectionreference_createdby";
                public const string LkConnectionreferenceCreatedonbehalfby = "lk_connectionreference_createdonbehalfby";
                public const string LkConnectionreferenceModifiedby = "lk_connectionreference_modifiedby";
                public const string LkConnectionreferenceModifiedonbehalfby = "lk_connectionreference_modifiedonbehalfby";
                public const string LkConnectionrolebaseCreatedonbehalfby = "lk_connectionrolebase_createdonbehalfby";
                public const string LkConnectionrolebaseModifiedonbehalfby = "lk_connectionrolebase_modifiedonbehalfby";
                public const string LkConnectorCreatedby = "lk_connector_createdby";
                public const string LkConnectorCreatedonbehalfby = "lk_connector_createdonbehalfby";
                public const string LkConnectorModifiedby = "lk_connector_modifiedby";
                public const string LkConnectorModifiedonbehalfby = "lk_connector_modifiedonbehalfby";
                public const string LkContactCreatedonbehalfby = "lk_contact_createdonbehalfby";
                public const string LkContactModifiedonbehalfby = "lk_contact_modifiedonbehalfby";
                public const string LkContactbaseCreatedby = "lk_contactbase_createdby";
                public const string LkContactbaseModifiedby = "lk_contactbase_modifiedby";
                public const string LkConversationtranscriptCreatedby = "lk_conversationtranscript_createdby";
                public const string LkConversationtranscriptCreatedonbehalfby = "lk_conversationtranscript_createdonbehalfby";
                public const string LkConversationtranscriptModifiedby = "lk_conversationtranscript_modifiedby";
                public const string LkConversationtranscriptModifiedonbehalfby = "lk_conversationtranscript_modifiedonbehalfby";
                public const string LkConvertruleCreatedby = "lk_convertrule_createdby";
                public const string LkConvertRuleCreatedonbehalfby = "lk_ConvertRule_createdonbehalfby";
                public const string LkConvertRuleModifiedby = "lk_ConvertRule_modifiedby";
                public const string LkConvertRuleModifiedonbehalfby = "lk_ConvertRule_modifiedonbehalfby";
                public const string LkConvertruleitembaseCreatedby = "lk_convertruleitembase_createdby";
                public const string LkConvertruleitembaseCreatedonbehalfby = "lk_convertruleitembase_createdonbehalfby";
                public const string LkConvertruleitembaseModifiedby = "lk_convertruleitembase_modifiedby";
                public const string LkConvertruleitembaseModifiedonbehalfby = "lk_convertruleitembase_modifiedonbehalfby";
                public const string LkCopilotexamplequestionCreatedby = "lk_copilotexamplequestion_createdby";
                public const string LkCopilotexamplequestionCreatedonbehalfby = "lk_copilotexamplequestion_createdonbehalfby";
                public const string LkCopilotexamplequestionModifiedby = "lk_copilotexamplequestion_modifiedby";
                public const string LkCopilotexamplequestionModifiedonbehalfby = "lk_copilotexamplequestion_modifiedonbehalfby";
                public const string LkCopilotglossarytermCreatedby = "lk_copilotglossaryterm_createdby";
                public const string LkCopilotglossarytermCreatedonbehalfby = "lk_copilotglossaryterm_createdonbehalfby";
                public const string LkCopilotglossarytermModifiedby = "lk_copilotglossaryterm_modifiedby";
                public const string LkCopilotglossarytermModifiedonbehalfby = "lk_copilotglossaryterm_modifiedonbehalfby";
                public const string LkCopilotsynonymsCreatedby = "lk_copilotsynonyms_createdby";
                public const string LkCopilotsynonymsCreatedonbehalfby = "lk_copilotsynonyms_createdonbehalfby";
                public const string LkCopilotsynonymsModifiedby = "lk_copilotsynonyms_modifiedby";
                public const string LkCopilotsynonymsModifiedonbehalfby = "lk_copilotsynonyms_modifiedonbehalfby";
                public const string LkCredentialCreatedby = "lk_credential_createdby";
                public const string LkCredentialCreatedonbehalfby = "lk_credential_createdonbehalfby";
                public const string LkCredentialModifiedby = "lk_credential_modifiedby";
                public const string LkCredentialModifiedonbehalfby = "lk_credential_modifiedonbehalfby";
                public const string LkCustomapiCreatedby = "lk_customapi_createdby";
                public const string LkCustomapiCreatedonbehalfby = "lk_customapi_createdonbehalfby";
                public const string LkCustomapiModifiedby = "lk_customapi_modifiedby";
                public const string LkCustomapiModifiedonbehalfby = "lk_customapi_modifiedonbehalfby";
                public const string LkCustomapirequestparameterCreatedby = "lk_customapirequestparameter_createdby";
                public const string LkCustomapirequestparameterCreatedonbehalfby = "lk_customapirequestparameter_createdonbehalfby";
                public const string LkCustomapirequestparameterModifiedby = "lk_customapirequestparameter_modifiedby";
                public const string LkCustomapirequestparameterModifiedonbehalfby = "lk_customapirequestparameter_modifiedonbehalfby";
                public const string LkCustomapiresponsepropertyCreatedby = "lk_customapiresponseproperty_createdby";
                public const string LkCustomapiresponsepropertyCreatedonbehalfby = "lk_customapiresponseproperty_createdonbehalfby";
                public const string LkCustomapiresponsepropertyModifiedby = "lk_customapiresponseproperty_modifiedby";
                public const string LkCustomapiresponsepropertyModifiedonbehalfby = "lk_customapiresponseproperty_modifiedonbehalfby";
                public const string LkCustomcontrolCreatedby = "lk_customcontrol_createdby";
                public const string LkCustomcontrolCreatedonbehalfby = "lk_customcontrol_createdonbehalfby";
                public const string LkCustomcontrolModifiedby = "lk_customcontrol_modifiedby";
                public const string LkCustomcontrolModifiedonbehalfby = "lk_customcontrol_modifiedonbehalfby";
                public const string LkCustomcontroldefaultconfigCreatedby = "lk_customcontroldefaultconfig_createdby";
                public const string LkCustomcontroldefaultconfigCreatedonbehalfby = "lk_customcontroldefaultconfig_createdonbehalfby";
                public const string LkCustomcontroldefaultconfigModifiedby = "lk_customcontroldefaultconfig_modifiedby";
                public const string LkCustomcontroldefaultconfigModifiedonbehalfby = "lk_customcontroldefaultconfig_modifiedonbehalfby";
                public const string LkCustomcontrolresourceCreatedby = "lk_customcontrolresource_createdby";
                public const string LkCustomcontrolresourceCreatedonbehalfby = "lk_customcontrolresource_createdonbehalfby";
                public const string LkCustomcontrolresourceModifiedby = "lk_customcontrolresource_modifiedby";
                public const string LkCustomcontrolresourceModifiedonbehalfby = "lk_customcontrolresource_modifiedonbehalfby";
                public const string LkCustomeraddressCreatedonbehalfby = "lk_customeraddress_createdonbehalfby";
                public const string LkCustomeraddressModifiedonbehalfby = "lk_customeraddress_modifiedonbehalfby";
                public const string LkCustomeraddressbaseCreatedby = "lk_customeraddressbase_createdby";
                public const string LkCustomeraddressbaseModifiedby = "lk_customeraddressbase_modifiedby";
                public const string LkDatalakefolderCreatedby = "lk_datalakefolder_createdby";
                public const string LkDatalakefolderCreatedonbehalfby = "lk_datalakefolder_createdonbehalfby";
                public const string LkDatalakefolderModifiedby = "lk_datalakefolder_modifiedby";
                public const string LkDatalakefolderModifiedonbehalfby = "lk_datalakefolder_modifiedonbehalfby";
                public const string LkDatalakefolderpermissionCreatedby = "lk_datalakefolderpermission_createdby";
                public const string LkDatalakefolderpermissionCreatedonbehalfby = "lk_datalakefolderpermission_createdonbehalfby";
                public const string LkDatalakefolderpermissionModifiedby = "lk_datalakefolderpermission_modifiedby";
                public const string LkDatalakefolderpermissionModifiedonbehalfby = "lk_datalakefolderpermission_modifiedonbehalfby";
                public const string LkDatalakeworkspaceCreatedby = "lk_datalakeworkspace_createdby";
                public const string LkDatalakeworkspaceCreatedonbehalfby = "lk_datalakeworkspace_createdonbehalfby";
                public const string LkDatalakeworkspaceModifiedby = "lk_datalakeworkspace_modifiedby";
                public const string LkDatalakeworkspaceModifiedonbehalfby = "lk_datalakeworkspace_modifiedonbehalfby";
                public const string LkDatalakeworkspacepermissionCreatedby = "lk_datalakeworkspacepermission_createdby";
                public const string LkDatalakeworkspacepermissionCreatedonbehalfby = "lk_datalakeworkspacepermission_createdonbehalfby";
                public const string LkDatalakeworkspacepermissionModifiedby = "lk_datalakeworkspacepermission_modifiedby";
                public const string LkDatalakeworkspacepermissionModifiedonbehalfby = "lk_datalakeworkspacepermission_modifiedonbehalfby";
                public const string LkDataprocessingconfigurationCreatedby = "lk_dataprocessingconfiguration_createdby";
                public const string LkDataprocessingconfigurationCreatedonbehalfby = "lk_dataprocessingconfiguration_createdonbehalfby";
                public const string LkDataprocessingconfigurationModifiedby = "lk_dataprocessingconfiguration_modifiedby";
                public const string LkDataprocessingconfigurationModifiedonbehalfby = "lk_dataprocessingconfiguration_modifiedonbehalfby";
                public const string LkDelegatedauthorizationCreatedby = "lk_delegatedauthorization_createdby";
                public const string LkDelegatedauthorizationCreatedonbehalfby = "lk_delegatedauthorization_createdonbehalfby";
                public const string LkDelegatedauthorizationModifiedby = "lk_delegatedauthorization_modifiedby";
                public const string LkDelegatedauthorizationModifiedonbehalfby = "lk_delegatedauthorization_modifiedonbehalfby";
                public const string LkDeleteditemreferenceCreatedby = "lk_deleteditemreference_createdby";
                public const string LkDeleteditemreferenceCreatedonbehalfby = "lk_deleteditemreference_createdonbehalfby";
                public const string LkDeleteditemreferenceModifiedby = "lk_deleteditemreference_modifiedby";
                public const string LkDeleteditemreferenceModifiedonbehalfby = "lk_deleteditemreference_modifiedonbehalfby";
                public const string LkDelveactionhubCreatedby = "lk_delveactionhub_createdby";
                public const string LkDelveactionhubCreatedonbehalfby = "lk_delveactionhub_createdonbehalfby";
                public const string LkDelveactionhubModifiedby = "lk_delveactionhub_modifiedby";
                public const string LkDelveactionhubModifiedonbehalfby = "lk_delveactionhub_modifiedonbehalfby";
                public const string LkDesktopflowbinaryCreatedby = "lk_desktopflowbinary_createdby";
                public const string LkDesktopflowbinaryCreatedonbehalfby = "lk_desktopflowbinary_createdonbehalfby";
                public const string LkDesktopflowbinaryModifiedby = "lk_desktopflowbinary_modifiedby";
                public const string LkDesktopflowbinaryModifiedonbehalfby = "lk_desktopflowbinary_modifiedonbehalfby";
                public const string LkDesktopflowmoduleCreatedby = "lk_desktopflowmodule_createdby";
                public const string LkDesktopflowmoduleCreatedonbehalfby = "lk_desktopflowmodule_createdonbehalfby";
                public const string LkDesktopflowmoduleModifiedby = "lk_desktopflowmodule_modifiedby";
                public const string LkDesktopflowmoduleModifiedonbehalfby = "lk_desktopflowmodule_modifiedonbehalfby";
                public const string LkDisplayStringbaseCreatedby = "lk_DisplayStringbase_createdby";
                public const string LkDisplayStringbaseCreatedonbehalfby = "lk_DisplayStringbase_createdonbehalfby";
                public const string LkDisplayStringbaseModifiedby = "lk_DisplayStringbase_modifiedby";
                public const string LkDisplayStringbaseModifiedonbehalfby = "lk_DisplayStringbase_modifiedonbehalfby";
                public const string LkDocumentindexCreatedby = "lk_documentindex_createdby";
                public const string LkDocumentindexCreatedonbehalfby = "lk_documentindex_createdonbehalfby";
                public const string LkDocumentindexModifiedby = "lk_documentindex_modifiedby";
                public const string LkDocumentindexModifiedonbehalfby = "lk_documentindex_modifiedonbehalfby";
                public const string LkDocumenttemplatebaseCreatedby = "lk_documenttemplatebase_createdby";
                public const string LkDocumenttemplatebaseCreatedonbehalfby = "lk_documenttemplatebase_createdonbehalfby";
                public const string LkDocumenttemplatebaseModifiedby = "lk_documenttemplatebase_modifiedby";
                public const string LkDocumenttemplatebaseModifiedonbehalfby = "lk_documenttemplatebase_modifiedonbehalfby";
                public const string LkDuplicateruleCreatedonbehalfby = "lk_duplicaterule_createdonbehalfby";
                public const string LkDuplicateruleModifiedonbehalfby = "lk_duplicaterule_modifiedonbehalfby";
                public const string LkDuplicaterulebaseCreatedby = "lk_duplicaterulebase_createdby";
                public const string LkDuplicaterulebaseModifiedby = "lk_duplicaterulebase_modifiedby";
                public const string LkDuplicateruleconditionCreatedonbehalfby = "lk_duplicaterulecondition_createdonbehalfby";
                public const string LkDuplicateruleconditionModifiedonbehalfby = "lk_duplicaterulecondition_modifiedonbehalfby";
                public const string LkDuplicateruleconditionbaseCreatedby = "lk_duplicateruleconditionbase_createdby";
                public const string LkDuplicateruleconditionbaseModifiedby = "lk_duplicateruleconditionbase_modifiedby";
                public const string LkDvfilesearchCreatedby = "lk_dvfilesearch_createdby";
                public const string LkDvfilesearchCreatedonbehalfby = "lk_dvfilesearch_createdonbehalfby";
                public const string LkDvfilesearchModifiedby = "lk_dvfilesearch_modifiedby";
                public const string LkDvfilesearchModifiedonbehalfby = "lk_dvfilesearch_modifiedonbehalfby";
                public const string LkDvfilesearchattributeCreatedby = "lk_dvfilesearchattribute_createdby";
                public const string LkDvfilesearchattributeCreatedonbehalfby = "lk_dvfilesearchattribute_createdonbehalfby";
                public const string LkDvfilesearchattributeModifiedby = "lk_dvfilesearchattribute_modifiedby";
                public const string LkDvfilesearchattributeModifiedonbehalfby = "lk_dvfilesearchattribute_modifiedonbehalfby";
                public const string LkDvfilesearchentityCreatedby = "lk_dvfilesearchentity_createdby";
                public const string LkDvfilesearchentityCreatedonbehalfby = "lk_dvfilesearchentity_createdonbehalfby";
                public const string LkDvfilesearchentityModifiedby = "lk_dvfilesearchentity_modifiedby";
                public const string LkDvfilesearchentityModifiedonbehalfby = "lk_dvfilesearchentity_modifiedonbehalfby";
                public const string LkDvtablesearchCreatedby = "lk_dvtablesearch_createdby";
                public const string LkDvtablesearchCreatedonbehalfby = "lk_dvtablesearch_createdonbehalfby";
                public const string LkDvtablesearchModifiedby = "lk_dvtablesearch_modifiedby";
                public const string LkDvtablesearchModifiedonbehalfby = "lk_dvtablesearch_modifiedonbehalfby";
                public const string LkDvtablesearchattributeCreatedby = "lk_dvtablesearchattribute_createdby";
                public const string LkDvtablesearchattributeCreatedonbehalfby = "lk_dvtablesearchattribute_createdonbehalfby";
                public const string LkDvtablesearchattributeModifiedby = "lk_dvtablesearchattribute_modifiedby";
                public const string LkDvtablesearchattributeModifiedonbehalfby = "lk_dvtablesearchattribute_modifiedonbehalfby";
                public const string LkDvtablesearchentityCreatedby = "lk_dvtablesearchentity_createdby";
                public const string LkDvtablesearchentityCreatedonbehalfby = "lk_dvtablesearchentity_createdonbehalfby";
                public const string LkDvtablesearchentityModifiedby = "lk_dvtablesearchentity_modifiedby";
                public const string LkDvtablesearchentityModifiedonbehalfby = "lk_dvtablesearchentity_modifiedonbehalfby";
                public const string LkEmailCreatedby = "lk_email_createdby";
                public const string LkEmailCreatedonbehalfby = "lk_email_createdonbehalfby";
                public const string LkEmailModifiedby = "lk_email_modifiedby";
                public const string LkEmailModifiedonbehalfby = "lk_email_modifiedonbehalfby";
                public const string LkEmailaddressconfigurationCreatedby = "lk_emailaddressconfiguration_createdby";
                public const string LkEmailaddressconfigurationCreatedonbehalfby = "lk_emailaddressconfiguration_createdonbehalfby";
                public const string LkEmailaddressconfigurationModifiedby = "lk_emailaddressconfiguration_modifiedby";
                public const string LkEmailaddressconfigurationModifiedonbehalfby = "lk_emailaddressconfiguration_modifiedonbehalfby";
                public const string LkEmailserverprofileCreatedby = "lk_emailserverprofile_createdby";
                public const string LkEmailserverprofileCreatedonbehalfby = "lk_emailserverprofile_createdonbehalfby";
                public const string LkEmailserverprofileModifiedby = "lk_emailserverprofile_modifiedby";
                public const string LkEmailserverprofileModifiedonbehalfby = "lk_emailserverprofile_modifiedonbehalfby";
                public const string LkEmailsignaturebaseCreatedby = "lk_emailsignaturebase_createdby";
                public const string LkEmailsignaturebaseCreatedonbehalfby = "lk_emailsignaturebase_createdonbehalfby";
                public const string LkEmailsignaturebaseModifiedby = "lk_emailsignaturebase_modifiedby";
                public const string LkEmailsignaturebaseModifiedonbehalfby = "lk_emailsignaturebase_modifiedonbehalfby";
                public const string LkEnablearchivalrequestCreatedby = "lk_enablearchivalrequest_createdby";
                public const string LkEnablearchivalrequestCreatedonbehalfby = "lk_enablearchivalrequest_createdonbehalfby";
                public const string LkEnablearchivalrequestModifiedby = "lk_enablearchivalrequest_modifiedby";
                public const string LkEnablearchivalrequestModifiedonbehalfby = "lk_enablearchivalrequest_modifiedonbehalfby";
                public const string LkEntityclusterconfigCreatedby = "lk_entityclusterconfig_createdby";
                public const string LkEntityclusterconfigCreatedonbehalfby = "lk_entityclusterconfig_createdonbehalfby";
                public const string LkEntityclusterconfigModifiedby = "lk_entityclusterconfig_modifiedby";
                public const string LkEntityclusterconfigModifiedonbehalfby = "lk_entityclusterconfig_modifiedonbehalfby";
                public const string LkEntitymapCreatedonbehalfby = "lk_entitymap_createdonbehalfby";
                public const string LkEntitymapModifiedonbehalfby = "lk_entitymap_modifiedonbehalfby";
                public const string LkEntityrecordfilterCreatedby = "lk_entityrecordfilter_createdby";
                public const string LkEntityrecordfilterCreatedonbehalfby = "lk_entityrecordfilter_createdonbehalfby";
                public const string LkEntityrecordfilterModifiedby = "lk_entityrecordfilter_modifiedby";
                public const string LkEntityrecordfilterModifiedonbehalfby = "lk_entityrecordfilter_modifiedonbehalfby";
                public const string LkEnvironmentvariabledefinitionCreatedby = "lk_environmentvariabledefinition_createdby";
                public const string LkEnvironmentvariabledefinitionCreatedonbehalfby = "lk_environmentvariabledefinition_createdonbehalfby";
                public const string LkEnvironmentvariabledefinitionModifiedby = "lk_environmentvariabledefinition_modifiedby";
                public const string LkEnvironmentvariabledefinitionModifiedonbehalfby = "lk_environmentvariabledefinition_modifiedonbehalfby";
                public const string LkEnvironmentvariablevalueCreatedby = "lk_environmentvariablevalue_createdby";
                public const string LkEnvironmentvariablevalueCreatedonbehalfby = "lk_environmentvariablevalue_createdonbehalfby";
                public const string LkEnvironmentvariablevalueModifiedby = "lk_environmentvariablevalue_modifiedby";
                public const string LkEnvironmentvariablevalueModifiedonbehalfby = "lk_environmentvariablevalue_modifiedonbehalfby";
                public const string LkEventexpanderbreadcrumbCreatedby = "lk_eventexpanderbreadcrumb_createdby";
                public const string LkEventexpanderbreadcrumbCreatedonbehalfby = "lk_eventexpanderbreadcrumb_createdonbehalfby";
                public const string LkEventexpanderbreadcrumbModifiedby = "lk_eventexpanderbreadcrumb_modifiedby";
                public const string LkEventexpanderbreadcrumbModifiedonbehalfby = "lk_eventexpanderbreadcrumb_modifiedonbehalfby";
                public const string LkExpandereventCreatedonbehalfby = "lk_expanderevent_createdonbehalfby";
                public const string LkExpandereventModifiedonbehalfby = "lk_expanderevent_modifiedonbehalfby";
                public const string LkExpiredprocessCreatedby = "lk_expiredprocess_createdby";
                public const string LkExpiredprocessCreatedonbehalfby = "lk_expiredprocess_createdonbehalfby";
                public const string LkExpiredprocessModifiedby = "lk_expiredprocess_modifiedby";
                public const string LkExpiredprocessModifiedonbehalfby = "lk_expiredprocess_modifiedonbehalfby";
                public const string LkExportedexcelCreatedby = "lk_exportedexcel_createdby";
                public const string LkExportedexcelCreatedonbehalfby = "lk_exportedexcel_createdonbehalfby";
                public const string LkExportedexcelModifiedby = "lk_exportedexcel_modifiedby";
                public const string LkExportedexcelModifiedonbehalfby = "lk_exportedexcel_modifiedonbehalfby";
                public const string LkExportsolutionuploadCreatedby = "lk_exportsolutionupload_createdby";
                public const string LkExportsolutionuploadCreatedonbehalfby = "lk_exportsolutionupload_createdonbehalfby";
                public const string LkExportsolutionuploadModifiedby = "lk_exportsolutionupload_modifiedby";
                public const string LkExportsolutionuploadModifiedonbehalfby = "lk_exportsolutionupload_modifiedonbehalfby";
                public const string LkExternalpartyCreatedby = "lk_externalparty_createdby";
                public const string LkExternalpartyCreatedonbehalfby = "lk_externalparty_createdonbehalfby";
                public const string LkExternalpartyModifiedby = "lk_externalparty_modifiedby";
                public const string LkExternalpartyModifiedonbehalfby = "lk_externalparty_modifiedonbehalfby";
                public const string LkExternalpartyitemCreatedby = "lk_externalpartyitem_createdby";
                public const string LkExternalpartyitemCreatedonbehalfby = "lk_externalpartyitem_createdonbehalfby";
                public const string LkExternalpartyitemModifiedby = "lk_externalpartyitem_modifiedby";
                public const string LkExternalpartyitemModifiedonbehalfby = "lk_externalpartyitem_modifiedonbehalfby";
                public const string LkFabricaiskillCreatedby = "lk_fabricaiskill_createdby";
                public const string LkFabricaiskillCreatedonbehalfby = "lk_fabricaiskill_createdonbehalfby";
                public const string LkFabricaiskillModifiedby = "lk_fabricaiskill_modifiedby";
                public const string LkFabricaiskillModifiedonbehalfby = "lk_fabricaiskill_modifiedonbehalfby";
                public const string LkFaxCreatedby = "lk_fax_createdby";
                public const string LkFaxCreatedonbehalfby = "lk_fax_createdonbehalfby";
                public const string LkFaxModifiedby = "lk_fax_modifiedby";
                public const string LkFaxModifiedonbehalfby = "lk_fax_modifiedonbehalfby";
                public const string LkFeaturecontrolsettingCreatedby = "lk_featurecontrolsetting_createdby";
                public const string LkFeaturecontrolsettingCreatedonbehalfby = "lk_featurecontrolsetting_createdonbehalfby";
                public const string LkFeaturecontrolsettingModifiedby = "lk_featurecontrolsetting_modifiedby";
                public const string LkFeaturecontrolsettingModifiedonbehalfby = "lk_featurecontrolsetting_modifiedonbehalfby";
                public const string LkFederatedknowledgecitationCreatedby = "lk_federatedknowledgecitation_createdby";
                public const string LkFederatedknowledgecitationCreatedonbehalfby = "lk_federatedknowledgecitation_createdonbehalfby";
                public const string LkFederatedknowledgecitationModifiedby = "lk_federatedknowledgecitation_modifiedby";
                public const string LkFederatedknowledgecitationModifiedonbehalfby = "lk_federatedknowledgecitation_modifiedonbehalfby";
                public const string LkFederatedknowledgeconfigurationCreatedby = "lk_federatedknowledgeconfiguration_createdby";
                public const string LkFederatedknowledgeconfigurationCreatedonbehalfby = "lk_federatedknowledgeconfiguration_createdonbehalfby";
                public const string LkFederatedknowledgeconfigurationModifiedby = "lk_federatedknowledgeconfiguration_modifiedby";
                public const string LkFederatedknowledgeconfigurationModifiedonbehalfby = "lk_federatedknowledgeconfiguration_modifiedonbehalfby";
                public const string LkFederatedknowledgeentityconfigurationCreatedby = "lk_federatedknowledgeentityconfiguration_createdby";
                public const string LkFederatedknowledgeentityconfigurationCreatedonbehalfby = "lk_federatedknowledgeentityconfiguration_createdonbehalfby";
                public const string LkFederatedknowledgeentityconfigurationModifiedby = "lk_federatedknowledgeentityconfiguration_modifiedby";
                public const string LkFederatedknowledgeentityconfigurationModifiedonbehalfby = "lk_federatedknowledgeentityconfiguration_modifiedonbehalfby";
                public const string LkFederatedknowledgemetadatarefreshCreatedby = "lk_federatedknowledgemetadatarefresh_createdby";
                public const string LkFederatedknowledgemetadatarefreshCreatedonbehalfby = "lk_federatedknowledgemetadatarefresh_createdonbehalfby";
                public const string LkFederatedknowledgemetadatarefreshModifiedby = "lk_federatedknowledgemetadatarefresh_modifiedby";
                public const string LkFederatedknowledgemetadatarefreshModifiedonbehalfby = "lk_federatedknowledgemetadatarefresh_modifiedonbehalfby";
                public const string LkFeedbackClosedby = "lk_feedback_closedby";
                public const string LkFeedbackCreatedby = "lk_feedback_createdby";
                public const string LkFeedbackCreatedonbehalfby = "lk_feedback_createdonbehalfby";
                public const string LkFeedbackModifiedby = "lk_feedback_modifiedby";
                public const string LkFeedbackModifiedonbehalfby = "lk_feedback_modifiedonbehalfby";
                public const string LkFieldsecurityprofileCreatedby = "lk_fieldsecurityprofile_createdby";
                public const string LkFieldsecurityprofileCreatedonbehalfby = "lk_fieldsecurityprofile_createdonbehalfby";
                public const string LkFieldsecurityprofileModifiedby = "lk_fieldsecurityprofile_modifiedby";
                public const string LkFieldsecurityprofileModifiedonbehalfby = "lk_fieldsecurityprofile_modifiedonbehalfby";
                public const string LkFixedmonthlyfiscalcalendarCreatedby = "lk_fixedmonthlyfiscalcalendar_createdby";
                public const string LkFixedmonthlyfiscalcalendarCreatedonbehalfby = "lk_fixedmonthlyfiscalcalendar_createdonbehalfby";
                public const string LkFixedmonthlyfiscalcalendarModifiedby = "lk_fixedmonthlyfiscalcalendar_modifiedby";
                public const string LkFixedmonthlyfiscalcalendarModifiedonbehalfby = "lk_fixedmonthlyfiscalcalendar_modifiedonbehalfby";
                public const string LkFixedmonthlyfiscalcalendarSalespersonid = "lk_fixedmonthlyfiscalcalendar_salespersonid";
                public const string LkFlowaggregationCreatedby = "lk_flowaggregation_createdby";
                public const string LkFlowaggregationCreatedonbehalfby = "lk_flowaggregation_createdonbehalfby";
                public const string LkFlowaggregationModifiedby = "lk_flowaggregation_modifiedby";
                public const string LkFlowaggregationModifiedonbehalfby = "lk_flowaggregation_modifiedonbehalfby";
                public const string LkFlowcapacityassignmentCreatedby = "lk_flowcapacityassignment_createdby";
                public const string LkFlowcapacityassignmentCreatedonbehalfby = "lk_flowcapacityassignment_createdonbehalfby";
                public const string LkFlowcapacityassignmentModifiedby = "lk_flowcapacityassignment_modifiedby";
                public const string LkFlowcapacityassignmentModifiedonbehalfby = "lk_flowcapacityassignment_modifiedonbehalfby";
                public const string LkFlowcredentialapplicationCreatedby = "lk_flowcredentialapplication_createdby";
                public const string LkFlowcredentialapplicationCreatedonbehalfby = "lk_flowcredentialapplication_createdonbehalfby";
                public const string LkFlowcredentialapplicationModifiedby = "lk_flowcredentialapplication_modifiedby";
                public const string LkFlowcredentialapplicationModifiedonbehalfby = "lk_flowcredentialapplication_modifiedonbehalfby";
                public const string LkFloweventCreatedby = "lk_flowevent_createdby";
                public const string LkFloweventCreatedonbehalfby = "lk_flowevent_createdonbehalfby";
                public const string LkFloweventModifiedby = "lk_flowevent_modifiedby";
                public const string LkFloweventModifiedonbehalfby = "lk_flowevent_modifiedonbehalfby";
                public const string LkFlowgroupCreatedby = "lk_flowgroup_createdby";
                public const string LkFlowgroupCreatedonbehalfby = "lk_flowgroup_createdonbehalfby";
                public const string LkFlowgroupModifiedby = "lk_flowgroup_modifiedby";
                public const string LkFlowgroupModifiedonbehalfby = "lk_flowgroup_modifiedonbehalfby";
                public const string LkFlowlogCreatedby = "lk_flowlog_createdby";
                public const string LkFlowlogCreatedonbehalfby = "lk_flowlog_createdonbehalfby";
                public const string LkFlowlogModifiedby = "lk_flowlog_modifiedby";
                public const string LkFlowlogModifiedonbehalfby = "lk_flowlog_modifiedonbehalfby";
                public const string LkFlowmachineCreatedby = "lk_flowmachine_createdby";
                public const string LkFlowmachineCreatedonbehalfby = "lk_flowmachine_createdonbehalfby";
                public const string LkFlowmachineModifiedby = "lk_flowmachine_modifiedby";
                public const string LkFlowmachineModifiedonbehalfby = "lk_flowmachine_modifiedonbehalfby";
                public const string LkFlowmachinegroupCreatedby = "lk_flowmachinegroup_createdby";
                public const string LkFlowmachinegroupCreatedonbehalfby = "lk_flowmachinegroup_createdonbehalfby";
                public const string LkFlowmachinegroupModifiedby = "lk_flowmachinegroup_modifiedby";
                public const string LkFlowmachinegroupModifiedonbehalfby = "lk_flowmachinegroup_modifiedonbehalfby";
                public const string LkFlowmachineimageCreatedby = "lk_flowmachineimage_createdby";
                public const string LkFlowmachineimageCreatedonbehalfby = "lk_flowmachineimage_createdonbehalfby";
                public const string LkFlowmachineimageModifiedby = "lk_flowmachineimage_modifiedby";
                public const string LkFlowmachineimageModifiedonbehalfby = "lk_flowmachineimage_modifiedonbehalfby";
                public const string LkFlowmachineimageversionCreatedby = "lk_flowmachineimageversion_createdby";
                public const string LkFlowmachineimageversionCreatedonbehalfby = "lk_flowmachineimageversion_createdonbehalfby";
                public const string LkFlowmachineimageversionModifiedby = "lk_flowmachineimageversion_modifiedby";
                public const string LkFlowmachineimageversionModifiedonbehalfby = "lk_flowmachineimageversion_modifiedonbehalfby";
                public const string LkFlowmachinenetworkCreatedby = "lk_flowmachinenetwork_createdby";
                public const string LkFlowmachinenetworkCreatedonbehalfby = "lk_flowmachinenetwork_createdonbehalfby";
                public const string LkFlowmachinenetworkModifiedby = "lk_flowmachinenetwork_modifiedby";
                public const string LkFlowmachinenetworkModifiedonbehalfby = "lk_flowmachinenetwork_modifiedonbehalfby";
                public const string LkFlowrunCreatedby = "lk_flowrun_createdby";
                public const string LkFlowrunCreatedonbehalfby = "lk_flowrun_createdonbehalfby";
                public const string LkFlowrunModifiedby = "lk_flowrun_modifiedby";
                public const string LkFlowrunModifiedonbehalfby = "lk_flowrun_modifiedonbehalfby";
                public const string LkFlowsessionCreatedby = "lk_flowsession_createdby";
                public const string LkFlowsessionCreatedonbehalfby = "lk_flowsession_createdonbehalfby";
                public const string LkFlowsessionModifiedby = "lk_flowsession_modifiedby";
                public const string LkFlowsessionModifiedonbehalfby = "lk_flowsession_modifiedonbehalfby";
                public const string LkFlowsessionbinaryCreatedby = "lk_flowsessionbinary_createdby";
                public const string LkFlowsessionbinaryCreatedonbehalfby = "lk_flowsessionbinary_createdonbehalfby";
                public const string LkFlowsessionbinaryModifiedby = "lk_flowsessionbinary_modifiedby";
                public const string LkFlowsessionbinaryModifiedonbehalfby = "lk_flowsessionbinary_modifiedonbehalfby";
                public const string LkFlowtestsessionCreatedby = "lk_flowtestsession_createdby";
                public const string LkFlowtestsessionCreatedonbehalfby = "lk_flowtestsession_createdonbehalfby";
                public const string LkFlowtestsessionModifiedby = "lk_flowtestsession_modifiedby";
                public const string LkFlowtestsessionModifiedonbehalfby = "lk_flowtestsession_modifiedonbehalfby";
                public const string LkFlowtriggerCreatedby = "lk_flowtrigger_createdby";
                public const string LkFlowtriggerCreatedonbehalfby = "lk_flowtrigger_createdonbehalfby";
                public const string LkFlowtriggerModifiedby = "lk_flowtrigger_modifiedby";
                public const string LkFlowtriggerModifiedonbehalfby = "lk_flowtrigger_modifiedonbehalfby";
                public const string LkFlowtriggerinstanceCreatedby = "lk_flowtriggerinstance_createdby";
                public const string LkFlowtriggerinstanceCreatedonbehalfby = "lk_flowtriggerinstance_createdonbehalfby";
                public const string LkFlowtriggerinstanceModifiedby = "lk_flowtriggerinstance_modifiedby";
                public const string LkFlowtriggerinstanceModifiedonbehalfby = "lk_flowtriggerinstance_modifiedonbehalfby";
                public const string LkFxexpressionCreatedby = "lk_fxexpression_createdby";
                public const string LkFxexpressionCreatedonbehalfby = "lk_fxexpression_createdonbehalfby";
                public const string LkFxexpressionModifiedby = "lk_fxexpression_modifiedby";
                public const string LkFxexpressionModifiedonbehalfby = "lk_fxexpression_modifiedonbehalfby";
                public const string LkGithubappconfigCreatedby = "lk_githubappconfig_createdby";
                public const string LkGithubappconfigCreatedonbehalfby = "lk_githubappconfig_createdonbehalfby";
                public const string LkGithubappconfigModifiedby = "lk_githubappconfig_modifiedby";
                public const string LkGithubappconfigModifiedonbehalfby = "lk_githubappconfig_modifiedonbehalfby";
                public const string LkGoalCreatedby = "lk_goal_createdby";
                public const string LkGoalCreatedonbehalfby = "lk_goal_createdonbehalfby";
                public const string LkGoalModifiedby = "lk_goal_modifiedby";
                public const string LkGoalModifiedonbehalfby = "lk_goal_modifiedonbehalfby";
                public const string LkGoalrollupqueryCreatedby = "lk_goalrollupquery_createdby";
                public const string LkGoalrollupqueryCreatedonbehalfby = "lk_goalrollupquery_createdonbehalfby";
                public const string LkGoalrollupqueryModifiedby = "lk_goalrollupquery_modifiedby";
                public const string LkGoalrollupqueryModifiedonbehalfby = "lk_goalrollupquery_modifiedonbehalfby";
                public const string LkGovernanceconfigurationCreatedby = "lk_governanceconfiguration_createdby";
                public const string LkGovernanceconfigurationCreatedonbehalfby = "lk_governanceconfiguration_createdonbehalfby";
                public const string LkGovernanceconfigurationModifiedby = "lk_governanceconfiguration_modifiedby";
                public const string LkGovernanceconfigurationModifiedonbehalfby = "lk_governanceconfiguration_modifiedonbehalfby";
                public const string LkImportCreatedonbehalfby = "lk_import_createdonbehalfby";
                public const string LkImportModifiedonbehalfby = "lk_import_modifiedonbehalfby";
                public const string LkImportbaseCreatedby = "lk_importbase_createdby";
                public const string LkImportbaseModifiedby = "lk_importbase_modifiedby";
                public const string LkImportdataCreatedonbehalfby = "lk_importdata_createdonbehalfby";
                public const string LkImportdataModifiedonbehalfby = "lk_importdata_modifiedonbehalfby";
                public const string LkImportdatabaseCreatedby = "lk_importdatabase_createdby";
                public const string LkImportdatabaseModifiedby = "lk_importdatabase_modifiedby";
                public const string LkImportentitymappingCreatedby = "lk_importentitymapping_createdby";
                public const string LkImportentitymappingCreatedonbehalfby = "lk_importentitymapping_createdonbehalfby";
                public const string LkImportentitymappingModifiedby = "lk_importentitymapping_modifiedby";
                public const string LkImportentitymappingModifiedonbehalfby = "lk_importentitymapping_modifiedonbehalfby";
                public const string LkImportfilebaseCreatedby = "lk_importfilebase_createdby";
                public const string LkImportfilebaseCreatedonbehalfby = "lk_importfilebase_createdonbehalfby";
                public const string LkImportfilebaseModifiedby = "lk_importfilebase_modifiedby";
                public const string LkImportfilebaseModifiedonbehalfby = "lk_importfilebase_modifiedonbehalfby";
                public const string LkImportjobbaseCreatedby = "lk_importjobbase_createdby";
                public const string LkImportjobbaseCreatedonbehalfby = "lk_importjobbase_createdonbehalfby";
                public const string LkImportjobbaseModifiedby = "lk_importjobbase_modifiedby";
                public const string LkImportjobbaseModifiedonbehalfby = "lk_importjobbase_modifiedonbehalfby";
                public const string LkImportlogCreatedonbehalfby = "lk_importlog_createdonbehalfby";
                public const string LkImportlogModifiedonbehalfby = "lk_importlog_modifiedonbehalfby";
                public const string LkImportlogbaseCreatedby = "lk_importlogbase_createdby";
                public const string LkImportlogbaseModifiedby = "lk_importlogbase_modifiedby";
                public const string LkImportmapCreatedonbehalfby = "lk_importmap_createdonbehalfby";
                public const string LkImportmapModifiedonbehalfby = "lk_importmap_modifiedonbehalfby";
                public const string LkImportmapbaseCreatedby = "lk_importmapbase_createdby";
                public const string LkImportmapbaseModifiedby = "lk_importmapbase_modifiedby";
                public const string LkIndexedtraitCreatedby = "lk_indexedtrait_createdby";
                public const string LkIndexedtraitCreatedonbehalfby = "lk_indexedtrait_createdonbehalfby";
                public const string LkIndexedtraitModifiedby = "lk_indexedtrait_modifiedby";
                public const string LkIndexedtraitModifiedonbehalfby = "lk_indexedtrait_modifiedonbehalfby";
                public const string LkIntegrationstatusCreatedby = "lk_integrationstatus_createdby";
                public const string LkIntegrationstatusCreatedonbehalfby = "lk_integrationstatus_createdonbehalfby";
                public const string LkIntegrationstatusModifiedby = "lk_integrationstatus_modifiedby";
                public const string LkIntegrationstatusModifiedonbehalfby = "lk_integrationstatus_modifiedonbehalfby";
                public const string LkIntelligentmemoryCreatedby = "lk_intelligentmemory_createdby";
                public const string LkIntelligentmemoryCreatedonbehalfby = "lk_intelligentmemory_createdonbehalfby";
                public const string LkIntelligentmemoryModifiedby = "lk_intelligentmemory_modifiedby";
                public const string LkIntelligentmemoryModifiedonbehalfby = "lk_intelligentmemory_modifiedonbehalfby";
                public const string LkInteractionforemailCreatedby = "lk_interactionforemail_createdby";
                public const string LkInteractionforemailCreatedonbehalfby = "lk_interactionforemail_createdonbehalfby";
                public const string LkInteractionforemailModifiedby = "lk_interactionforemail_modifiedby";
                public const string LkInteractionforemailModifiedonbehalfby = "lk_interactionforemail_modifiedonbehalfby";
                public const string LkInternaladdressCreatedonbehalfby = "lk_internaladdress_createdonbehalfby";
                public const string LkInternaladdressModifiedonbehalfby = "lk_internaladdress_modifiedonbehalfby";
                public const string LkInternaladdressbaseCreatedby = "lk_internaladdressbase_createdby";
                public const string LkInternaladdressbaseModifiedby = "lk_internaladdressbase_modifiedby";
                public const string LkInternalcatalogassignmentCreatedby = "lk_internalcatalogassignment_createdby";
                public const string LkInternalcatalogassignmentCreatedonbehalfby = "lk_internalcatalogassignment_createdonbehalfby";
                public const string LkInternalcatalogassignmentModifiedby = "lk_internalcatalogassignment_modifiedby";
                public const string LkInternalcatalogassignmentModifiedonbehalfby = "lk_internalcatalogassignment_modifiedonbehalfby";
                public const string LkIsvconfigCreatedonbehalfby = "lk_isvconfig_createdonbehalfby";
                public const string LkIsvconfigModifiedonbehalfby = "lk_isvconfig_modifiedonbehalfby";
                public const string LkIsvconfigbaseCreatedby = "lk_isvconfigbase_createdby";
                public const string LkIsvconfigbaseModifiedby = "lk_isvconfigbase_modifiedby";
                public const string LkKbarticleCreatedonbehalfby = "lk_kbarticle_createdonbehalfby";
                public const string LkKbarticleModifiedonbehalfby = "lk_kbarticle_modifiedonbehalfby";
                public const string LkKbarticlebaseCreatedby = "lk_kbarticlebase_createdby";
                public const string LkKbarticlebaseModifiedby = "lk_kbarticlebase_modifiedby";
                public const string LkKbarticlecommentCreatedonbehalfby = "lk_kbarticlecomment_createdonbehalfby";
                public const string LkKbarticlecommentModifiedonbehalfby = "lk_kbarticlecomment_modifiedonbehalfby";
                public const string LkKbarticlecommentbaseCreatedby = "lk_kbarticlecommentbase_createdby";
                public const string LkKbarticlecommentbaseModifiedby = "lk_kbarticlecommentbase_modifiedby";
                public const string LkKbarticletemplateCreatedonbehalfby = "lk_kbarticletemplate_createdonbehalfby";
                public const string LkKbarticletemplateModifiedonbehalfby = "lk_kbarticletemplate_modifiedonbehalfby";
                public const string LkKbarticletemplatebaseCreatedby = "lk_kbarticletemplatebase_createdby";
                public const string LkKbarticletemplatebaseModifiedby = "lk_kbarticletemplatebase_modifiedby";
                public const string LkKeyvaultreferenceCreatedby = "lk_keyvaultreference_createdby";
                public const string LkKeyvaultreferenceCreatedonbehalfby = "lk_keyvaultreference_createdonbehalfby";
                public const string LkKeyvaultreferenceModifiedby = "lk_keyvaultreference_modifiedby";
                public const string LkKeyvaultreferenceModifiedonbehalfby = "lk_keyvaultreference_modifiedonbehalfby";
                public const string LkKnowledgearticleCreatedby = "lk_knowledgearticle_createdby";
                public const string LkKnowledgearticleCreatedonbehalfby = "lk_knowledgearticle_createdonbehalfby";
                public const string LkKnowledgearticleModifiedby = "lk_knowledgearticle_modifiedby";
                public const string LkKnowledgearticleModifiedonbehalfby = "lk_knowledgearticle_modifiedonbehalfby";
                public const string LkKnowledgearticleviewsCreatedby = "lk_knowledgearticleviews_createdby";
                public const string LkKnowledgearticleviewsCreatedonbehalfby = "lk_knowledgearticleviews_createdonbehalfby";
                public const string LkKnowledgearticleviewsModifiedby = "lk_knowledgearticleviews_modifiedby";
                public const string LkKnowledgearticleviewsModifiedonbehalfby = "lk_knowledgearticleviews_modifiedonbehalfby";
                public const string LkKnowledgeBaseRecordCreatedby = "lk_KnowledgeBaseRecord_createdby";
                public const string LkKnowledgeBaseRecordCreatedonbehalfby = "lk_KnowledgeBaseRecord_createdonbehalfby";
                public const string LkKnowledgeBaseRecordModifiedby = "lk_KnowledgeBaseRecord_modifiedby";
                public const string LkKnowledgeBaseRecordModifiedonbehalfby = "lk_KnowledgeBaseRecord_modifiedonbehalfby";
                public const string LkKnowledgefaqCreatedby = "lk_knowledgefaq_createdby";
                public const string LkKnowledgefaqCreatedonbehalfby = "lk_knowledgefaq_createdonbehalfby";
                public const string LkKnowledgefaqModifiedby = "lk_knowledgefaq_modifiedby";
                public const string LkKnowledgefaqModifiedonbehalfby = "lk_knowledgefaq_modifiedonbehalfby";
                public const string LkKnowledgesearchmodelCreatedby = "lk_knowledgesearchmodel_createdby";
                public const string LkKnowledgesearchmodelCreatedonbehalfby = "lk_knowledgesearchmodel_createdonbehalfby";
                public const string LkKnowledgesearchmodelModifiedby = "lk_knowledgesearchmodel_modifiedby";
                public const string LkKnowledgesearchmodelModifiedonbehalfby = "lk_knowledgesearchmodel_modifiedonbehalfby";
                public const string LkKnowledgesourceconsumerCreatedby = "lk_knowledgesourceconsumer_createdby";
                public const string LkKnowledgesourceconsumerCreatedonbehalfby = "lk_knowledgesourceconsumer_createdonbehalfby";
                public const string LkKnowledgesourceconsumerModifiedby = "lk_knowledgesourceconsumer_modifiedby";
                public const string LkKnowledgesourceconsumerModifiedonbehalfby = "lk_knowledgesourceconsumer_modifiedonbehalfby";
                public const string LkKnowledgesourceprofileCreatedby = "lk_knowledgesourceprofile_createdby";
                public const string LkKnowledgesourceprofileCreatedonbehalfby = "lk_knowledgesourceprofile_createdonbehalfby";
                public const string LkKnowledgesourceprofileModifiedby = "lk_knowledgesourceprofile_modifiedby";
                public const string LkKnowledgesourceprofileModifiedonbehalfby = "lk_knowledgesourceprofile_modifiedonbehalfby";
                public const string LkLetterCreatedby = "lk_letter_createdby";
                public const string LkLetterCreatedonbehalfby = "lk_letter_createdonbehalfby";
                public const string LkLetterModifiedby = "lk_letter_modifiedby";
                public const string LkLetterModifiedonbehalfby = "lk_letter_modifiedonbehalfby";
                public const string LkLookupmappingCreatedby = "lk_lookupmapping_createdby";
                public const string LkLookupmappingCreatedonbehalfby = "lk_lookupmapping_createdonbehalfby";
                public const string LkLookupmappingModifiedby = "lk_lookupmapping_modifiedby";
                public const string LkLookupmappingModifiedonbehalfby = "lk_lookupmapping_modifiedonbehalfby";
                public const string LkMailboxCreatedby = "lk_mailbox_createdby";
                public const string LkMailboxCreatedonbehalfby = "lk_mailbox_createdonbehalfby";
                public const string LkMailboxModifiedby = "lk_mailbox_modifiedby";
                public const string LkMailboxModifiedonbehalfby = "lk_mailbox_modifiedonbehalfby";
                public const string LkMailboxtrackingfolderCreatedby = "lk_mailboxtrackingfolder_createdby";
                public const string LkMailboxtrackingfolderCreatedonbehalfby = "lk_mailboxtrackingfolder_createdonbehalfby";
                public const string LkMailboxtrackingfolderModifiedby = "lk_mailboxtrackingfolder_modifiedby";
                public const string LkMailboxtrackingfolderModifiedonbehalfby = "lk_mailboxtrackingfolder_modifiedonbehalfby";
                public const string LkMailmergetemplateCreatedonbehalfby = "lk_mailmergetemplate_createdonbehalfby";
                public const string LkMailmergetemplateModifiedonbehalfby = "lk_mailmergetemplate_modifiedonbehalfby";
                public const string LkMailmergetemplatebaseCreatedby = "lk_mailmergetemplatebase_createdby";
                public const string LkMailmergetemplatebaseModifiedby = "lk_mailmergetemplatebase_modifiedby";
                public const string LkMainfewshotCreatedby = "lk_mainfewshot_createdby";
                public const string LkMainfewshotCreatedonbehalfby = "lk_mainfewshot_createdonbehalfby";
                public const string LkMainfewshotModifiedby = "lk_mainfewshot_modifiedby";
                public const string LkMainfewshotModifiedonbehalfby = "lk_mainfewshot_modifiedonbehalfby";
                public const string LkMakerfewshotCreatedby = "lk_makerfewshot_createdby";
                public const string LkMakerfewshotCreatedonbehalfby = "lk_makerfewshot_createdonbehalfby";
                public const string LkMakerfewshotModifiedby = "lk_makerfewshot_modifiedby";
                public const string LkMakerfewshotModifiedonbehalfby = "lk_makerfewshot_modifiedonbehalfby";
                public const string LkManagedidentityCreatedby = "lk_managedidentity_createdby";
                public const string LkManagedidentityCreatedonbehalfby = "lk_managedidentity_createdonbehalfby";
                public const string LkManagedidentityModifiedby = "lk_managedidentity_modifiedby";
                public const string LkManagedidentityModifiedonbehalfby = "lk_managedidentity_modifiedonbehalfby";
                public const string LkMcppromptCreatedby = "lk_mcpprompt_createdby";
                public const string LkMcppromptCreatedonbehalfby = "lk_mcpprompt_createdonbehalfby";
                public const string LkMcppromptModifiedby = "lk_mcpprompt_modifiedby";
                public const string LkMcppromptModifiedonbehalfby = "lk_mcpprompt_modifiedonbehalfby";
                public const string LkMcpresourceCreatedby = "lk_mcpresource_createdby";
                public const string LkMcpresourceCreatedonbehalfby = "lk_mcpresource_createdonbehalfby";
                public const string LkMcpresourceModifiedby = "lk_mcpresource_modifiedby";
                public const string LkMcpresourceModifiedonbehalfby = "lk_mcpresource_modifiedonbehalfby";
                public const string LkMcpresourcecontentCreatedby = "lk_mcpresourcecontent_createdby";
                public const string LkMcpresourcecontentCreatedonbehalfby = "lk_mcpresourcecontent_createdonbehalfby";
                public const string LkMcpresourcecontentModifiedby = "lk_mcpresourcecontent_modifiedby";
                public const string LkMcpresourcecontentModifiedonbehalfby = "lk_mcpresourcecontent_modifiedonbehalfby";
                public const string LkMcpserverCreatedby = "lk_mcpserver_createdby";
                public const string LkMcpserverCreatedonbehalfby = "lk_mcpserver_createdonbehalfby";
                public const string LkMcpserverModifiedby = "lk_mcpserver_modifiedby";
                public const string LkMcpserverModifiedonbehalfby = "lk_mcpserver_modifiedonbehalfby";
                public const string LkMcptoolCreatedby = "lk_mcptool_createdby";
                public const string LkMcptoolCreatedonbehalfby = "lk_mcptool_createdonbehalfby";
                public const string LkMcptoolModifiedby = "lk_mcptool_modifiedby";
                public const string LkMcptoolModifiedonbehalfby = "lk_mcptool_modifiedonbehalfby";
                public const string LkMetadataforarchivalCreatedby = "lk_metadataforarchival_createdby";
                public const string LkMetadataforarchivalCreatedonbehalfby = "lk_metadataforarchival_createdonbehalfby";
                public const string LkMetadataforarchivalModifiedby = "lk_metadataforarchival_modifiedby";
                public const string LkMetadataforarchivalModifiedonbehalfby = "lk_metadataforarchival_modifiedonbehalfby";
                public const string LkMetricCreatedby = "lk_metric_createdby";
                public const string LkMetricCreatedonbehalfby = "lk_metric_createdonbehalfby";
                public const string LkMetricModifiedby = "lk_metric_modifiedby";
                public const string LkMetricModifiedonbehalfby = "lk_metric_modifiedonbehalfby";
                public const string LkMobileOfflineProfileCreatedby = "lk_MobileOfflineProfile_createdby";
                public const string LkMobileOfflineProfileCreatedonbehalfby = "lk_MobileOfflineProfile_createdonbehalfby";
                public const string LkMobileOfflineProfileModifiedby = "lk_MobileOfflineProfile_modifiedby";
                public const string LkMobileOfflineProfileModifiedonbehalfby = "lk_MobileOfflineProfile_modifiedonbehalfby";
                public const string LkMobileofflineprofileextensionCreatedby = "lk_mobileofflineprofileextension_createdby";
                public const string LkMobileofflineprofileextensionCreatedonbehalfby = "lk_mobileofflineprofileextension_createdonbehalfby";
                public const string LkMobileofflineprofileextensionModifiedby = "lk_mobileofflineprofileextension_modifiedby";
                public const string LkMobileofflineprofileextensionModifiedonbehalfby = "lk_mobileofflineprofileextension_modifiedonbehalfby";
                public const string LkMobileOfflineProfileItemCreatedby = "lk_MobileOfflineProfileItem_createdby";
                public const string LkMobileofflineprofileitemCreatedonbehalfby = "lk_mobileofflineprofileitem_createdonbehalfby";
                public const string LkMobileofflineprofileitemModifiedby = "lk_mobileofflineprofileitem_modifiedby";
                public const string LkMobileofflineprofileitemModifiedonbehalfby = "lk_mobileofflineprofileitem_modifiedonbehalfby";
                public const string LkMobileOfflineProfileItemAssociationCreatedby = "lk_MobileOfflineProfileItemAssociation_createdby";
                public const string LkMobileofflineprofileitemassociationCreatedonbehalfby = "lk_mobileofflineprofileitemassociation_createdonbehalfby";
                public const string LkMobileofflineprofileitemassociationModifiedby = "lk_mobileofflineprofileitemassociation_modifiedby";
                public const string LkMobileofflineprofileitemassociationModifiedonbehalfby = "lk_mobileofflineprofileitemassociation_modifiedonbehalfby";
                public const string LkMonthlyfiscalcalendarCreatedby = "lk_monthlyfiscalcalendar_createdby";
                public const string LkMonthlyfiscalcalendarCreatedonbehalfby = "lk_monthlyfiscalcalendar_createdonbehalfby";
                public const string LkMonthlyfiscalcalendarModifiedby = "lk_monthlyfiscalcalendar_modifiedby";
                public const string LkMonthlyfiscalcalendarModifiedonbehalfby = "lk_monthlyfiscalcalendar_modifiedonbehalfby";
                public const string LkMonthlyfiscalcalendarSalespersonid = "lk_monthlyfiscalcalendar_salespersonid";
                public const string LkMsdynAibdatasetCreatedby = "lk_msdyn_aibdataset_createdby";
                public const string LkMsdynAibdatasetCreatedonbehalfby = "lk_msdyn_aibdataset_createdonbehalfby";
                public const string LkMsdynAibdatasetModifiedby = "lk_msdyn_aibdataset_modifiedby";
                public const string LkMsdynAibdatasetModifiedonbehalfby = "lk_msdyn_aibdataset_modifiedonbehalfby";
                public const string LkMsdynAibdatasetfileCreatedby = "lk_msdyn_aibdatasetfile_createdby";
                public const string LkMsdynAibdatasetfileCreatedonbehalfby = "lk_msdyn_aibdatasetfile_createdonbehalfby";
                public const string LkMsdynAibdatasetfileModifiedby = "lk_msdyn_aibdatasetfile_modifiedby";
                public const string LkMsdynAibdatasetfileModifiedonbehalfby = "lk_msdyn_aibdatasetfile_modifiedonbehalfby";
                public const string LkMsdynAibdatasetrecordCreatedby = "lk_msdyn_aibdatasetrecord_createdby";
                public const string LkMsdynAibdatasetrecordCreatedonbehalfby = "lk_msdyn_aibdatasetrecord_createdonbehalfby";
                public const string LkMsdynAibdatasetrecordModifiedby = "lk_msdyn_aibdatasetrecord_modifiedby";
                public const string LkMsdynAibdatasetrecordModifiedonbehalfby = "lk_msdyn_aibdatasetrecord_modifiedonbehalfby";
                public const string LkMsdynAibdatasetscontainerCreatedby = "lk_msdyn_aibdatasetscontainer_createdby";
                public const string LkMsdynAibdatasetscontainerCreatedonbehalfby = "lk_msdyn_aibdatasetscontainer_createdonbehalfby";
                public const string LkMsdynAibdatasetscontainerModifiedby = "lk_msdyn_aibdatasetscontainer_modifiedby";
                public const string LkMsdynAibdatasetscontainerModifiedonbehalfby = "lk_msdyn_aibdatasetscontainer_modifiedonbehalfby";
                public const string LkMsdynAibfeedbackloopCreatedby = "lk_msdyn_aibfeedbackloop_createdby";
                public const string LkMsdynAibfeedbackloopCreatedonbehalfby = "lk_msdyn_aibfeedbackloop_createdonbehalfby";
                public const string LkMsdynAibfeedbackloopModifiedby = "lk_msdyn_aibfeedbackloop_modifiedby";
                public const string LkMsdynAibfeedbackloopModifiedonbehalfby = "lk_msdyn_aibfeedbackloop_modifiedonbehalfby";
                public const string LkMsdynAibfileCreatedby = "lk_msdyn_aibfile_createdby";
                public const string LkMsdynAibfileCreatedonbehalfby = "lk_msdyn_aibfile_createdonbehalfby";
                public const string LkMsdynAibfileModifiedby = "lk_msdyn_aibfile_modifiedby";
                public const string LkMsdynAibfileModifiedonbehalfby = "lk_msdyn_aibfile_modifiedonbehalfby";
                public const string LkMsdynAibfileattacheddataCreatedby = "lk_msdyn_aibfileattacheddata_createdby";
                public const string LkMsdynAibfileattacheddataCreatedonbehalfby = "lk_msdyn_aibfileattacheddata_createdonbehalfby";
                public const string LkMsdynAibfileattacheddataModifiedby = "lk_msdyn_aibfileattacheddata_modifiedby";
                public const string LkMsdynAibfileattacheddataModifiedonbehalfby = "lk_msdyn_aibfileattacheddata_modifiedonbehalfby";
                public const string LkMsdynAiconfigurationCreatedby = "lk_msdyn_aiconfiguration_createdby";
                public const string LkMsdynAiconfigurationCreatedonbehalfby = "lk_msdyn_aiconfiguration_createdonbehalfby";
                public const string LkMsdynAiconfigurationModifiedby = "lk_msdyn_aiconfiguration_modifiedby";
                public const string LkMsdynAiconfigurationModifiedonbehalfby = "lk_msdyn_aiconfiguration_modifiedonbehalfby";
                public const string LkMsdynAiconfigurationsearchCreatedby = "lk_msdyn_aiconfigurationsearch_createdby";
                public const string LkMsdynAiconfigurationsearchCreatedonbehalfby = "lk_msdyn_aiconfigurationsearch_createdonbehalfby";
                public const string LkMsdynAiconfigurationsearchModifiedby = "lk_msdyn_aiconfigurationsearch_modifiedby";
                public const string LkMsdynAiconfigurationsearchModifiedonbehalfby = "lk_msdyn_aiconfigurationsearch_modifiedonbehalfby";
                public const string LkMsdynAidataprocessingeventCreatedby = "lk_msdyn_aidataprocessingevent_createdby";
                public const string LkMsdynAidataprocessingeventCreatedonbehalfby = "lk_msdyn_aidataprocessingevent_createdonbehalfby";
                public const string LkMsdynAidataprocessingeventModifiedby = "lk_msdyn_aidataprocessingevent_modifiedby";
                public const string LkMsdynAidataprocessingeventModifiedonbehalfby = "lk_msdyn_aidataprocessingevent_modifiedonbehalfby";
                public const string LkMsdynAidocumenttemplateCreatedby = "lk_msdyn_aidocumenttemplate_createdby";
                public const string LkMsdynAidocumenttemplateCreatedonbehalfby = "lk_msdyn_aidocumenttemplate_createdonbehalfby";
                public const string LkMsdynAidocumenttemplateModifiedby = "lk_msdyn_aidocumenttemplate_modifiedby";
                public const string LkMsdynAidocumenttemplateModifiedonbehalfby = "lk_msdyn_aidocumenttemplate_modifiedonbehalfby";
                public const string LkMsdynAievaluationconfigurationCreatedby = "lk_msdyn_aievaluationconfiguration_createdby";
                public const string LkMsdynAievaluationconfigurationCreatedonbehalfby = "lk_msdyn_aievaluationconfiguration_createdonbehalfby";
                public const string LkMsdynAievaluationconfigurationModifiedby = "lk_msdyn_aievaluationconfiguration_modifiedby";
                public const string LkMsdynAievaluationconfigurationModifiedonbehalfby = "lk_msdyn_aievaluationconfiguration_modifiedonbehalfby";
                public const string LkMsdynAievaluationmetricCreatedby = "lk_msdyn_aievaluationmetric_createdby";
                public const string LkMsdynAievaluationmetricCreatedonbehalfby = "lk_msdyn_aievaluationmetric_createdonbehalfby";
                public const string LkMsdynAievaluationmetricModifiedby = "lk_msdyn_aievaluationmetric_modifiedby";
                public const string LkMsdynAievaluationmetricModifiedonbehalfby = "lk_msdyn_aievaluationmetric_modifiedonbehalfby";
                public const string LkMsdynAievaluationrunCreatedby = "lk_msdyn_aievaluationrun_createdby";
                public const string LkMsdynAievaluationrunCreatedonbehalfby = "lk_msdyn_aievaluationrun_createdonbehalfby";
                public const string LkMsdynAievaluationrunModifiedby = "lk_msdyn_aievaluationrun_modifiedby";
                public const string LkMsdynAievaluationrunModifiedonbehalfby = "lk_msdyn_aievaluationrun_modifiedonbehalfby";
                public const string LkMsdynAieventCreatedby = "lk_msdyn_aievent_createdby";
                public const string LkMsdynAieventCreatedonbehalfby = "lk_msdyn_aievent_createdonbehalfby";
                public const string LkMsdynAieventModifiedby = "lk_msdyn_aievent_modifiedby";
                public const string LkMsdynAieventModifiedonbehalfby = "lk_msdyn_aievent_modifiedonbehalfby";
                public const string LkMsdynAifptrainingdocumentCreatedby = "lk_msdyn_aifptrainingdocument_createdby";
                public const string LkMsdynAifptrainingdocumentCreatedonbehalfby = "lk_msdyn_aifptrainingdocument_createdonbehalfby";
                public const string LkMsdynAifptrainingdocumentModifiedby = "lk_msdyn_aifptrainingdocument_modifiedby";
                public const string LkMsdynAifptrainingdocumentModifiedonbehalfby = "lk_msdyn_aifptrainingdocument_modifiedonbehalfby";
                public const string LkMsdynAimodelCreatedby = "lk_msdyn_aimodel_createdby";
                public const string LkMsdynAimodelCreatedonbehalfby = "lk_msdyn_aimodel_createdonbehalfby";
                public const string LkMsdynAimodelModifiedby = "lk_msdyn_aimodel_modifiedby";
                public const string LkMsdynAimodelModifiedonbehalfby = "lk_msdyn_aimodel_modifiedonbehalfby";
                public const string LkMsdynAimodelcatalogCreatedby = "lk_msdyn_aimodelcatalog_createdby";
                public const string LkMsdynAimodelcatalogCreatedonbehalfby = "lk_msdyn_aimodelcatalog_createdonbehalfby";
                public const string LkMsdynAimodelcatalogModifiedby = "lk_msdyn_aimodelcatalog_modifiedby";
                public const string LkMsdynAimodelcatalogModifiedonbehalfby = "lk_msdyn_aimodelcatalog_modifiedonbehalfby";
                public const string LkMsdynAiodimageCreatedby = "lk_msdyn_aiodimage_createdby";
                public const string LkMsdynAiodimageCreatedonbehalfby = "lk_msdyn_aiodimage_createdonbehalfby";
                public const string LkMsdynAiodimageModifiedby = "lk_msdyn_aiodimage_modifiedby";
                public const string LkMsdynAiodimageModifiedonbehalfby = "lk_msdyn_aiodimage_modifiedonbehalfby";
                public const string LkMsdynAiodlabelCreatedby = "lk_msdyn_aiodlabel_createdby";
                public const string LkMsdynAiodlabelCreatedonbehalfby = "lk_msdyn_aiodlabel_createdonbehalfby";
                public const string LkMsdynAiodlabelModifiedby = "lk_msdyn_aiodlabel_modifiedby";
                public const string LkMsdynAiodlabelModifiedonbehalfby = "lk_msdyn_aiodlabel_modifiedonbehalfby";
                public const string LkMsdynAiodtrainingboundingboxCreatedby = "lk_msdyn_aiodtrainingboundingbox_createdby";
                public const string LkMsdynAiodtrainingboundingboxCreatedonbehalfby = "lk_msdyn_aiodtrainingboundingbox_createdonbehalfby";
                public const string LkMsdynAiodtrainingboundingboxModifiedby = "lk_msdyn_aiodtrainingboundingbox_modifiedby";
                public const string LkMsdynAiodtrainingboundingboxModifiedonbehalfby = "lk_msdyn_aiodtrainingboundingbox_modifiedonbehalfby";
                public const string LkMsdynAiodtrainingimageCreatedby = "lk_msdyn_aiodtrainingimage_createdby";
                public const string LkMsdynAiodtrainingimageCreatedonbehalfby = "lk_msdyn_aiodtrainingimage_createdonbehalfby";
                public const string LkMsdynAiodtrainingimageModifiedby = "lk_msdyn_aiodtrainingimage_modifiedby";
                public const string LkMsdynAiodtrainingimageModifiedonbehalfby = "lk_msdyn_aiodtrainingimage_modifiedonbehalfby";
                public const string LkMsdynAioptimizationCreatedby = "lk_msdyn_aioptimization_createdby";
                public const string LkMsdynAioptimizationCreatedonbehalfby = "lk_msdyn_aioptimization_createdonbehalfby";
                public const string LkMsdynAioptimizationModifiedby = "lk_msdyn_aioptimization_modifiedby";
                public const string LkMsdynAioptimizationModifiedonbehalfby = "lk_msdyn_aioptimization_modifiedonbehalfby";
                public const string LkMsdynAioptimizationprivatedataCreatedby = "lk_msdyn_aioptimizationprivatedata_createdby";
                public const string LkMsdynAioptimizationprivatedataCreatedonbehalfby = "lk_msdyn_aioptimizationprivatedata_createdonbehalfby";
                public const string LkMsdynAioptimizationprivatedataModifiedby = "lk_msdyn_aioptimizationprivatedata_modifiedby";
                public const string LkMsdynAioptimizationprivatedataModifiedonbehalfby = "lk_msdyn_aioptimizationprivatedata_modifiedonbehalfby";
                public const string LkMsdynAitemplateCreatedby = "lk_msdyn_aitemplate_createdby";
                public const string LkMsdynAitemplateCreatedonbehalfby = "lk_msdyn_aitemplate_createdonbehalfby";
                public const string LkMsdynAitemplateModifiedby = "lk_msdyn_aitemplate_modifiedby";
                public const string LkMsdynAitemplateModifiedonbehalfby = "lk_msdyn_aitemplate_modifiedonbehalfby";
                public const string LkMsdynAitestcaseCreatedby = "lk_msdyn_aitestcase_createdby";
                public const string LkMsdynAitestcaseCreatedonbehalfby = "lk_msdyn_aitestcase_createdonbehalfby";
                public const string LkMsdynAitestcaseModifiedby = "lk_msdyn_aitestcase_modifiedby";
                public const string LkMsdynAitestcaseModifiedonbehalfby = "lk_msdyn_aitestcase_modifiedonbehalfby";
                public const string LkMsdynAitestcasedocumentCreatedby = "lk_msdyn_aitestcasedocument_createdby";
                public const string LkMsdynAitestcasedocumentCreatedonbehalfby = "lk_msdyn_aitestcasedocument_createdonbehalfby";
                public const string LkMsdynAitestcasedocumentModifiedby = "lk_msdyn_aitestcasedocument_modifiedby";
                public const string LkMsdynAitestcasedocumentModifiedonbehalfby = "lk_msdyn_aitestcasedocument_modifiedonbehalfby";
                public const string LkMsdynAitestcaseinputCreatedby = "lk_msdyn_aitestcaseinput_createdby";
                public const string LkMsdynAitestcaseinputCreatedonbehalfby = "lk_msdyn_aitestcaseinput_createdonbehalfby";
                public const string LkMsdynAitestcaseinputModifiedby = "lk_msdyn_aitestcaseinput_modifiedby";
                public const string LkMsdynAitestcaseinputModifiedonbehalfby = "lk_msdyn_aitestcaseinput_modifiedonbehalfby";
                public const string LkMsdynAitestrunCreatedby = "lk_msdyn_aitestrun_createdby";
                public const string LkMsdynAitestrunCreatedonbehalfby = "lk_msdyn_aitestrun_createdonbehalfby";
                public const string LkMsdynAitestrunModifiedby = "lk_msdyn_aitestrun_modifiedby";
                public const string LkMsdynAitestrunModifiedonbehalfby = "lk_msdyn_aitestrun_modifiedonbehalfby";
                public const string LkMsdynAitestrunbatchCreatedby = "lk_msdyn_aitestrunbatch_createdby";
                public const string LkMsdynAitestrunbatchCreatedonbehalfby = "lk_msdyn_aitestrunbatch_createdonbehalfby";
                public const string LkMsdynAitestrunbatchModifiedby = "lk_msdyn_aitestrunbatch_modifiedby";
                public const string LkMsdynAitestrunbatchModifiedonbehalfby = "lk_msdyn_aitestrunbatch_modifiedonbehalfby";
                public const string LkMsdynAnalysiscomponentCreatedby = "lk_msdyn_analysiscomponent_createdby";
                public const string LkMsdynAnalysiscomponentCreatedonbehalfby = "lk_msdyn_analysiscomponent_createdonbehalfby";
                public const string LkMsdynAnalysiscomponentModifiedby = "lk_msdyn_analysiscomponent_modifiedby";
                public const string LkMsdynAnalysiscomponentModifiedonbehalfby = "lk_msdyn_analysiscomponent_modifiedonbehalfby";
                public const string LkMsdynAnalysisjobCreatedby = "lk_msdyn_analysisjob_createdby";
                public const string LkMsdynAnalysisjobCreatedonbehalfby = "lk_msdyn_analysisjob_createdonbehalfby";
                public const string LkMsdynAnalysisjobModifiedby = "lk_msdyn_analysisjob_modifiedby";
                public const string LkMsdynAnalysisjobModifiedonbehalfby = "lk_msdyn_analysisjob_modifiedonbehalfby";
                public const string LkMsdynAnalysisoverrideCreatedby = "lk_msdyn_analysisoverride_createdby";
                public const string LkMsdynAnalysisoverrideCreatedonbehalfby = "lk_msdyn_analysisoverride_createdonbehalfby";
                public const string LkMsdynAnalysisoverrideModifiedby = "lk_msdyn_analysisoverride_modifiedby";
                public const string LkMsdynAnalysisoverrideModifiedonbehalfby = "lk_msdyn_analysisoverride_modifiedonbehalfby";
                public const string LkMsdynAnalysisresultCreatedby = "lk_msdyn_analysisresult_createdby";
                public const string LkMsdynAnalysisresultCreatedonbehalfby = "lk_msdyn_analysisresult_createdonbehalfby";
                public const string LkMsdynAnalysisresultModifiedby = "lk_msdyn_analysisresult_modifiedby";
                public const string LkMsdynAnalysisresultModifiedonbehalfby = "lk_msdyn_analysisresult_modifiedonbehalfby";
                public const string LkMsdynAnalysisresultdetailCreatedby = "lk_msdyn_analysisresultdetail_createdby";
                public const string LkMsdynAnalysisresultdetailCreatedonbehalfby = "lk_msdyn_analysisresultdetail_createdonbehalfby";
                public const string LkMsdynAnalysisresultdetailModifiedby = "lk_msdyn_analysisresultdetail_modifiedby";
                public const string LkMsdynAnalysisresultdetailModifiedonbehalfby = "lk_msdyn_analysisresultdetail_modifiedonbehalfby";
                public const string LkMsdynAppinsightsmetadataCreatedby = "lk_msdyn_appinsightsmetadata_createdby";
                public const string LkMsdynAppinsightsmetadataCreatedonbehalfby = "lk_msdyn_appinsightsmetadata_createdonbehalfby";
                public const string LkMsdynAppinsightsmetadataModifiedby = "lk_msdyn_appinsightsmetadata_modifiedby";
                public const string LkMsdynAppinsightsmetadataModifiedonbehalfby = "lk_msdyn_appinsightsmetadata_modifiedonbehalfby";
                public const string LkMsdynBulkharvestrunlogCreatedby = "lk_msdyn_bulkharvestrunlog_createdby";
                public const string LkMsdynBulkharvestrunlogCreatedonbehalfby = "lk_msdyn_bulkharvestrunlog_createdonbehalfby";
                public const string LkMsdynBulkharvestrunlogModifiedby = "lk_msdyn_bulkharvestrunlog_modifiedby";
                public const string LkMsdynBulkharvestrunlogModifiedonbehalfby = "lk_msdyn_bulkharvestrunlog_modifiedonbehalfby";
                public const string LkMsdynCopilotinteractionsCreatedby = "lk_msdyn_copilotinteractions_createdby";
                public const string LkMsdynCopilotinteractionsCreatedonbehalfby = "lk_msdyn_copilotinteractions_createdonbehalfby";
                public const string LkMsdynCopilotinteractionsModifiedby = "lk_msdyn_copilotinteractions_modifiedby";
                public const string LkMsdynCopilotinteractionsModifiedonbehalfby = "lk_msdyn_copilotinteractions_modifiedonbehalfby";
                public const string LkMsdynCustomcontrolextendedsettingsCreatedby = "lk_msdyn_customcontrolextendedsettings_createdby";
                public const string LkMsdynCustomcontrolextendedsettingsCreatedonbehalfby = "lk_msdyn_customcontrolextendedsettings_createdonbehalfby";
                public const string LkMsdynCustomcontrolextendedsettingsModifiedby = "lk_msdyn_customcontrolextendedsettings_modifiedby";
                public const string LkMsdynCustomcontrolextendedsettingsModifiedonbehalfby = "lk_msdyn_customcontrolextendedsettings_modifiedonbehalfby";
                public const string LkMsdynDataflowCreatedby = "lk_msdyn_dataflow_createdby";
                public const string LkMsdynDataflowCreatedonbehalfby = "lk_msdyn_dataflow_createdonbehalfby";
                public const string LkMsdynDataflowDatalakefolderCreatedby = "lk_msdyn_dataflow_datalakefolder_createdby";
                public const string LkMsdynDataflowDatalakefolderCreatedonbehalfby = "lk_msdyn_dataflow_datalakefolder_createdonbehalfby";
                public const string LkMsdynDataflowDatalakefolderModifiedby = "lk_msdyn_dataflow_datalakefolder_modifiedby";
                public const string LkMsdynDataflowDatalakefolderModifiedonbehalfby = "lk_msdyn_dataflow_datalakefolder_modifiedonbehalfby";
                public const string LkMsdynDataflowModifiedby = "lk_msdyn_dataflow_modifiedby";
                public const string LkMsdynDataflowModifiedonbehalfby = "lk_msdyn_dataflow_modifiedonbehalfby";
                public const string LkMsdynDataflowconnectionreferenceCreatedby = "lk_msdyn_dataflowconnectionreference_createdby";
                public const string LkMsdynDataflowconnectionreferenceCreatedonbehalfby = "lk_msdyn_dataflowconnectionreference_createdonbehalfby";
                public const string LkMsdynDataflowconnectionreferenceModifiedby = "lk_msdyn_dataflowconnectionreference_modifiedby";
                public const string LkMsdynDataflowconnectionreferenceModifiedonbehalfby = "lk_msdyn_dataflowconnectionreference_modifiedonbehalfby";
                public const string LkMsdynDataflowrefreshhistoryCreatedby = "lk_msdyn_dataflowrefreshhistory_createdby";
                public const string LkMsdynDataflowrefreshhistoryCreatedonbehalfby = "lk_msdyn_dataflowrefreshhistory_createdonbehalfby";
                public const string LkMsdynDataflowrefreshhistoryModifiedby = "lk_msdyn_dataflowrefreshhistory_modifiedby";
                public const string LkMsdynDataflowrefreshhistoryModifiedonbehalfby = "lk_msdyn_dataflowrefreshhistory_modifiedonbehalfby";
                public const string LkMsdynDataflowtemplateCreatedby = "lk_msdyn_dataflowtemplate_createdby";
                public const string LkMsdynDataflowtemplateCreatedonbehalfby = "lk_msdyn_dataflowtemplate_createdonbehalfby";
                public const string LkMsdynDataflowtemplateModifiedby = "lk_msdyn_dataflowtemplate_modifiedby";
                public const string LkMsdynDataflowtemplateModifiedonbehalfby = "lk_msdyn_dataflowtemplate_modifiedonbehalfby";
                public const string LkMsdynDataworkspaceCreatedby = "lk_msdyn_dataworkspace_createdby";
                public const string LkMsdynDataworkspaceCreatedonbehalfby = "lk_msdyn_dataworkspace_createdonbehalfby";
                public const string LkMsdynDataworkspaceModifiedby = "lk_msdyn_dataworkspace_modifiedby";
                public const string LkMsdynDataworkspaceModifiedonbehalfby = "lk_msdyn_dataworkspace_modifiedonbehalfby";
                public const string LkMsdynDmsrequestCreatedby = "lk_msdyn_dmsrequest_createdby";
                public const string LkMsdynDmsrequestCreatedonbehalfby = "lk_msdyn_dmsrequest_createdonbehalfby";
                public const string LkMsdynDmsrequestModifiedby = "lk_msdyn_dmsrequest_modifiedby";
                public const string LkMsdynDmsrequestModifiedonbehalfby = "lk_msdyn_dmsrequest_modifiedonbehalfby";
                public const string LkMsdynDmsrequeststatusCreatedby = "lk_msdyn_dmsrequeststatus_createdby";
                public const string LkMsdynDmsrequeststatusCreatedonbehalfby = "lk_msdyn_dmsrequeststatus_createdonbehalfby";
                public const string LkMsdynDmsrequeststatusModifiedby = "lk_msdyn_dmsrequeststatus_modifiedby";
                public const string LkMsdynDmsrequeststatusModifiedonbehalfby = "lk_msdyn_dmsrequeststatus_modifiedonbehalfby";
                public const string LkMsdynDmssyncrequestCreatedby = "lk_msdyn_dmssyncrequest_createdby";
                public const string LkMsdynDmssyncrequestCreatedonbehalfby = "lk_msdyn_dmssyncrequest_createdonbehalfby";
                public const string LkMsdynDmssyncrequestModifiedby = "lk_msdyn_dmssyncrequest_modifiedby";
                public const string LkMsdynDmssyncrequestModifiedonbehalfby = "lk_msdyn_dmssyncrequest_modifiedonbehalfby";
                public const string LkMsdynDmssyncstatusCreatedby = "lk_msdyn_dmssyncstatus_createdby";
                public const string LkMsdynDmssyncstatusCreatedonbehalfby = "lk_msdyn_dmssyncstatus_createdonbehalfby";
                public const string LkMsdynDmssyncstatusModifiedby = "lk_msdyn_dmssyncstatus_modifiedby";
                public const string LkMsdynDmssyncstatusModifiedonbehalfby = "lk_msdyn_dmssyncstatus_modifiedonbehalfby";
                public const string LkMsdynEntitylinkchatconfigurationCreatedby = "lk_msdyn_entitylinkchatconfiguration_createdby";
                public const string LkMsdynEntitylinkchatconfigurationCreatedonbehalfby = "lk_msdyn_entitylinkchatconfiguration_createdonbehalfby";
                public const string LkMsdynEntitylinkchatconfigurationModifiedby = "lk_msdyn_entitylinkchatconfiguration_modifiedby";
                public const string LkMsdynEntitylinkchatconfigurationModifiedonbehalfby = "lk_msdyn_entitylinkchatconfiguration_modifiedonbehalfby";
                public const string LkMsdynEntityrefreshhistoryCreatedby = "lk_msdyn_entityrefreshhistory_createdby";
                public const string LkMsdynEntityrefreshhistoryCreatedonbehalfby = "lk_msdyn_entityrefreshhistory_createdonbehalfby";
                public const string LkMsdynEntityrefreshhistoryModifiedby = "lk_msdyn_entityrefreshhistory_modifiedby";
                public const string LkMsdynEntityrefreshhistoryModifiedonbehalfby = "lk_msdyn_entityrefreshhistory_modifiedonbehalfby";
                public const string LkMsdynFavoriteknowledgearticleCreatedby = "lk_msdyn_favoriteknowledgearticle_createdby";
                public const string LkMsdynFavoriteknowledgearticleCreatedonbehalfby = "lk_msdyn_favoriteknowledgearticle_createdonbehalfby";
                public const string LkMsdynFavoriteknowledgearticleModifiedby = "lk_msdyn_favoriteknowledgearticle_modifiedby";
                public const string LkMsdynFavoriteknowledgearticleModifiedonbehalfby = "lk_msdyn_favoriteknowledgearticle_modifiedonbehalfby";
                public const string LkMsdynFederatedarticleCreatedby = "lk_msdyn_federatedarticle_createdby";
                public const string LkMsdynFederatedarticleCreatedonbehalfby = "lk_msdyn_federatedarticle_createdonbehalfby";
                public const string LkMsdynFederatedarticleModifiedby = "lk_msdyn_federatedarticle_modifiedby";
                public const string LkMsdynFederatedarticleModifiedonbehalfby = "lk_msdyn_federatedarticle_modifiedonbehalfby";
                public const string LkMsdynFederatedarticleincidentCreatedby = "lk_msdyn_federatedarticleincident_createdby";
                public const string LkMsdynFederatedarticleincidentCreatedonbehalfby = "lk_msdyn_federatedarticleincident_createdonbehalfby";
                public const string LkMsdynFederatedarticleincidentModifiedby = "lk_msdyn_federatedarticleincident_modifiedby";
                public const string LkMsdynFederatedarticleincidentModifiedonbehalfby = "lk_msdyn_federatedarticleincident_modifiedonbehalfby";
                public const string LkMsdynFileuploadCreatedby = "lk_msdyn_fileupload_createdby";
                public const string LkMsdynFileuploadCreatedonbehalfby = "lk_msdyn_fileupload_createdonbehalfby";
                public const string LkMsdynFileuploadModifiedby = "lk_msdyn_fileupload_modifiedby";
                public const string LkMsdynFileuploadModifiedonbehalfby = "lk_msdyn_fileupload_modifiedonbehalfby";
                public const string LkMsdynFlowActionapprovalmodelCreatedby = "lk_msdyn_flow_actionapprovalmodel_createdby";
                public const string LkMsdynFlowActionapprovalmodelCreatedonbehalfby = "lk_msdyn_flow_actionapprovalmodel_createdonbehalfby";
                public const string LkMsdynFlowActionapprovalmodelModifiedby = "lk_msdyn_flow_actionapprovalmodel_modifiedby";
                public const string LkMsdynFlowActionapprovalmodelModifiedonbehalfby = "lk_msdyn_flow_actionapprovalmodel_modifiedonbehalfby";
                public const string LkMsdynFlowApprovalCreatedby = "lk_msdyn_flow_approval_createdby";
                public const string LkMsdynFlowApprovalCreatedonbehalfby = "lk_msdyn_flow_approval_createdonbehalfby";
                public const string LkMsdynFlowApprovalModifiedby = "lk_msdyn_flow_approval_modifiedby";
                public const string LkMsdynFlowApprovalModifiedonbehalfby = "lk_msdyn_flow_approval_modifiedonbehalfby";
                public const string LkMsdynFlowApprovalrequestCreatedby = "lk_msdyn_flow_approvalrequest_createdby";
                public const string LkMsdynFlowApprovalrequestCreatedonbehalfby = "lk_msdyn_flow_approvalrequest_createdonbehalfby";
                public const string LkMsdynFlowApprovalrequestModifiedby = "lk_msdyn_flow_approvalrequest_modifiedby";
                public const string LkMsdynFlowApprovalrequestModifiedonbehalfby = "lk_msdyn_flow_approvalrequest_modifiedonbehalfby";
                public const string LkMsdynFlowApprovalresponseCreatedby = "lk_msdyn_flow_approvalresponse_createdby";
                public const string LkMsdynFlowApprovalresponseCreatedonbehalfby = "lk_msdyn_flow_approvalresponse_createdonbehalfby";
                public const string LkMsdynFlowApprovalresponseModifiedby = "lk_msdyn_flow_approvalresponse_modifiedby";
                public const string LkMsdynFlowApprovalresponseModifiedonbehalfby = "lk_msdyn_flow_approvalresponse_modifiedonbehalfby";
                public const string LkMsdynFlowApprovalstepCreatedby = "lk_msdyn_flow_approvalstep_createdby";
                public const string LkMsdynFlowApprovalstepCreatedonbehalfby = "lk_msdyn_flow_approvalstep_createdonbehalfby";
                public const string LkMsdynFlowApprovalstepModifiedby = "lk_msdyn_flow_approvalstep_modifiedby";
                public const string LkMsdynFlowApprovalstepModifiedonbehalfby = "lk_msdyn_flow_approvalstep_modifiedonbehalfby";
                public const string LkMsdynFlowAwaitallactionapprovalmodelCreatedby = "lk_msdyn_flow_awaitallactionapprovalmodel_createdby";
                public const string LkMsdynFlowAwaitallactionapprovalmodelCreatedonbehalfby = "lk_msdyn_flow_awaitallactionapprovalmodel_createdonbehalfby";
                public const string LkMsdynFlowAwaitallactionapprovalmodelModifiedby = "lk_msdyn_flow_awaitallactionapprovalmodel_modifiedby";
                public const string LkMsdynFlowAwaitallactionapprovalmodelModifiedonbehalfby = "lk_msdyn_flow_awaitallactionapprovalmodel_modifiedonbehalfby";
                public const string LkMsdynFlowAwaitallapprovalmodelCreatedby = "lk_msdyn_flow_awaitallapprovalmodel_createdby";
                public const string LkMsdynFlowAwaitallapprovalmodelCreatedonbehalfby = "lk_msdyn_flow_awaitallapprovalmodel_createdonbehalfby";
                public const string LkMsdynFlowAwaitallapprovalmodelModifiedby = "lk_msdyn_flow_awaitallapprovalmodel_modifiedby";
                public const string LkMsdynFlowAwaitallapprovalmodelModifiedonbehalfby = "lk_msdyn_flow_awaitallapprovalmodel_modifiedonbehalfby";
                public const string LkMsdynFlowBasicapprovalmodelCreatedby = "lk_msdyn_flow_basicapprovalmodel_createdby";
                public const string LkMsdynFlowBasicapprovalmodelCreatedonbehalfby = "lk_msdyn_flow_basicapprovalmodel_createdonbehalfby";
                public const string LkMsdynFlowBasicapprovalmodelModifiedby = "lk_msdyn_flow_basicapprovalmodel_modifiedby";
                public const string LkMsdynFlowBasicapprovalmodelModifiedonbehalfby = "lk_msdyn_flow_basicapprovalmodel_modifiedonbehalfby";
                public const string LkMsdynFlowFlowapprovalCreatedby = "lk_msdyn_flow_flowapproval_createdby";
                public const string LkMsdynFlowFlowapprovalCreatedonbehalfby = "lk_msdyn_flow_flowapproval_createdonbehalfby";
                public const string LkMsdynFlowFlowapprovalModifiedby = "lk_msdyn_flow_flowapproval_modifiedby";
                public const string LkMsdynFlowFlowapprovalModifiedonbehalfby = "lk_msdyn_flow_flowapproval_modifiedonbehalfby";
                public const string LkMsdynFormmappingCreatedby = "lk_msdyn_formmapping_createdby";
                public const string LkMsdynFormmappingCreatedonbehalfby = "lk_msdyn_formmapping_createdonbehalfby";
                public const string LkMsdynFormmappingModifiedby = "lk_msdyn_formmapping_modifiedby";
                public const string LkMsdynFormmappingModifiedonbehalfby = "lk_msdyn_formmapping_modifiedonbehalfby";
                public const string LkMsdynFunctionCreatedby = "lk_msdyn_function_createdby";
                public const string LkMsdynFunctionCreatedonbehalfby = "lk_msdyn_function_createdonbehalfby";
                public const string LkMsdynFunctionModifiedby = "lk_msdyn_function_modifiedby";
                public const string LkMsdynFunctionModifiedonbehalfby = "lk_msdyn_function_modifiedonbehalfby";
                public const string LkMsdynHarvesteligibilityconditionCreatedby = "lk_msdyn_harvesteligibilitycondition_createdby";
                public const string LkMsdynHarvesteligibilityconditionCreatedonbehalfby = "lk_msdyn_harvesteligibilitycondition_createdonbehalfby";
                public const string LkMsdynHarvesteligibilityconditionModifiedby = "lk_msdyn_harvesteligibilitycondition_modifiedby";
                public const string LkMsdynHarvesteligibilityconditionModifiedonbehalfby = "lk_msdyn_harvesteligibilitycondition_modifiedonbehalfby";
                public const string LkMsdynHarvestworkitemCreatedby = "lk_msdyn_harvestworkitem_createdby";
                public const string LkMsdynHarvestworkitemCreatedonbehalfby = "lk_msdyn_harvestworkitem_createdonbehalfby";
                public const string LkMsdynHarvestworkitemModifiedby = "lk_msdyn_harvestworkitem_modifiedby";
                public const string LkMsdynHarvestworkitemModifiedonbehalfby = "lk_msdyn_harvestworkitem_modifiedonbehalfby";
                public const string LkMsdynHealthcareFeedbackCreatedby = "lk_msdyn_healthcare_feedback_createdby";
                public const string LkMsdynHealthcareFeedbackCreatedonbehalfby = "lk_msdyn_healthcare_feedback_createdonbehalfby";
                public const string LkMsdynHealthcareFeedbackModifiedby = "lk_msdyn_healthcare_feedback_modifiedby";
                public const string LkMsdynHealthcareFeedbackModifiedonbehalfby = "lk_msdyn_healthcare_feedback_modifiedonbehalfby";
                public const string LkMsdynHelppageCreatedby = "lk_msdyn_helppage_createdby";
                public const string LkMsdynHelppageCreatedonbehalfby = "lk_msdyn_helppage_createdonbehalfby";
                public const string LkMsdynHelppageModifiedby = "lk_msdyn_helppage_modifiedby";
                public const string LkMsdynHelppageModifiedonbehalfby = "lk_msdyn_helppage_modifiedonbehalfby";
                public const string LkMsdynHistoricalcaseharvestbatchCreatedby = "lk_msdyn_historicalcaseharvestbatch_createdby";
                public const string LkMsdynHistoricalcaseharvestbatchCreatedonbehalfby = "lk_msdyn_historicalcaseharvestbatch_createdonbehalfby";
                public const string LkMsdynHistoricalcaseharvestbatchModifiedby = "lk_msdyn_historicalcaseharvestbatch_modifiedby";
                public const string LkMsdynHistoricalcaseharvestbatchModifiedonbehalfby = "lk_msdyn_historicalcaseharvestbatch_modifiedonbehalfby";
                public const string LkMsdynHistoricalcaseharvestrunCreatedby = "lk_msdyn_historicalcaseharvestrun_createdby";
                public const string LkMsdynHistoricalcaseharvestrunCreatedonbehalfby = "lk_msdyn_historicalcaseharvestrun_createdonbehalfby";
                public const string LkMsdynHistoricalcaseharvestrunModifiedby = "lk_msdyn_historicalcaseharvestrun_modifiedby";
                public const string LkMsdynHistoricalcaseharvestrunModifiedonbehalfby = "lk_msdyn_historicalcaseharvestrun_modifiedonbehalfby";
                public const string LkMsdynInsightsstorevirtualentityCreatedby = "lk_msdyn_insightsstorevirtualentity_createdby";
                public const string LkMsdynInsightsstorevirtualentityCreatedonbehalfby = "lk_msdyn_insightsstorevirtualentity_createdonbehalfby";
                public const string LkMsdynInsightsstorevirtualentityModifiedby = "lk_msdyn_insightsstorevirtualentity_modifiedby";
                public const string LkMsdynInsightsstorevirtualentityModifiedonbehalfby = "lk_msdyn_insightsstorevirtualentity_modifiedonbehalfby";
                public const string LkMsdynIntegratedsearchproviderCreatedby = "lk_msdyn_integratedsearchprovider_createdby";
                public const string LkMsdynIntegratedsearchproviderCreatedonbehalfby = "lk_msdyn_integratedsearchprovider_createdonbehalfby";
                public const string LkMsdynIntegratedsearchproviderModifiedby = "lk_msdyn_integratedsearchprovider_modifiedby";
                public const string LkMsdynIntegratedsearchproviderModifiedonbehalfby = "lk_msdyn_integratedsearchprovider_modifiedonbehalfby";
                public const string LkMsdynInterimupdateknowledgearticleCreatedby = "lk_msdyn_interimupdateknowledgearticle_createdby";
                public const string LkMsdynInterimupdateknowledgearticleCreatedonbehalfby = "lk_msdyn_interimupdateknowledgearticle_createdonbehalfby";
                public const string LkMsdynInterimupdateknowledgearticleModifiedby = "lk_msdyn_interimupdateknowledgearticle_modifiedby";
                public const string LkMsdynInterimupdateknowledgearticleModifiedonbehalfby = "lk_msdyn_interimupdateknowledgearticle_modifiedonbehalfby";
                public const string LkMsdynKalanguagesettingCreatedby = "lk_msdyn_kalanguagesetting_createdby";
                public const string LkMsdynKalanguagesettingCreatedonbehalfby = "lk_msdyn_kalanguagesetting_createdonbehalfby";
                public const string LkMsdynKalanguagesettingModifiedby = "lk_msdyn_kalanguagesetting_modifiedby";
                public const string LkMsdynKalanguagesettingModifiedonbehalfby = "lk_msdyn_kalanguagesetting_modifiedonbehalfby";
                public const string LkMsdynKbattachmentCreatedby = "lk_msdyn_kbattachment_createdby";
                public const string LkMsdynKbattachmentCreatedonbehalfby = "lk_msdyn_kbattachment_createdonbehalfby";
                public const string LkMsdynKbattachmentModifiedby = "lk_msdyn_kbattachment_modifiedby";
                public const string LkMsdynKbattachmentModifiedonbehalfby = "lk_msdyn_kbattachment_modifiedonbehalfby";
                public const string LkMsdynKmfederatedsearchconfigCreatedby = "lk_msdyn_kmfederatedsearchconfig_createdby";
                public const string LkMsdynKmfederatedsearchconfigCreatedonbehalfby = "lk_msdyn_kmfederatedsearchconfig_createdonbehalfby";
                public const string LkMsdynKmfederatedsearchconfigModifiedby = "lk_msdyn_kmfederatedsearchconfig_modifiedby";
                public const string LkMsdynKmfederatedsearchconfigModifiedonbehalfby = "lk_msdyn_kmfederatedsearchconfig_modifiedonbehalfby";
                public const string LkMsdynKmpersonalizationsettingCreatedby = "lk_msdyn_kmpersonalizationsetting_createdby";
                public const string LkMsdynKmpersonalizationsettingCreatedonbehalfby = "lk_msdyn_kmpersonalizationsetting_createdonbehalfby";
                public const string LkMsdynKmpersonalizationsettingModifiedby = "lk_msdyn_kmpersonalizationsetting_modifiedby";
                public const string LkMsdynKmpersonalizationsettingModifiedonbehalfby = "lk_msdyn_kmpersonalizationsetting_modifiedonbehalfby";
                public const string LkMsdynKnowledgearticlecustomentityCreatedby = "lk_msdyn_knowledgearticlecustomentity_createdby";
                public const string LkMsdynKnowledgearticlecustomentityCreatedonbehalfby = "lk_msdyn_knowledgearticlecustomentity_createdonbehalfby";
                public const string LkMsdynKnowledgearticlecustomentityModifiedby = "lk_msdyn_knowledgearticlecustomentity_modifiedby";
                public const string LkMsdynKnowledgearticlecustomentityModifiedonbehalfby = "lk_msdyn_knowledgearticlecustomentity_modifiedonbehalfby";
                public const string LkMsdynKnowledgearticleimageCreatedby = "lk_msdyn_knowledgearticleimage_createdby";
                public const string LkMsdynKnowledgearticleimageCreatedonbehalfby = "lk_msdyn_knowledgearticleimage_createdonbehalfby";
                public const string LkMsdynKnowledgearticleimageModifiedby = "lk_msdyn_knowledgearticleimage_modifiedby";
                public const string LkMsdynKnowledgearticleimageModifiedonbehalfby = "lk_msdyn_knowledgearticleimage_modifiedonbehalfby";
                public const string LkMsdynKnowledgearticletemplateCreatedby = "lk_msdyn_knowledgearticletemplate_createdby";
                public const string LkMsdynKnowledgearticletemplateCreatedonbehalfby = "lk_msdyn_knowledgearticletemplate_createdonbehalfby";
                public const string LkMsdynKnowledgearticletemplateModifiedby = "lk_msdyn_knowledgearticletemplate_modifiedby";
                public const string LkMsdynKnowledgearticletemplateModifiedonbehalfby = "lk_msdyn_knowledgearticletemplate_modifiedonbehalfby";
                public const string LkMsdynKnowledgeassetconfigurationCreatedby = "lk_msdyn_knowledgeassetconfiguration_createdby";
                public const string LkMsdynKnowledgeassetconfigurationCreatedonbehalfby = "lk_msdyn_knowledgeassetconfiguration_createdonbehalfby";
                public const string LkMsdynKnowledgeassetconfigurationModifiedby = "lk_msdyn_knowledgeassetconfiguration_modifiedby";
                public const string LkMsdynKnowledgeassetconfigurationModifiedonbehalfby = "lk_msdyn_knowledgeassetconfiguration_modifiedonbehalfby";
                public const string LkMsdynKnowledgeconfigurationCreatedby = "lk_msdyn_knowledgeconfiguration_createdby";
                public const string LkMsdynKnowledgeconfigurationCreatedonbehalfby = "lk_msdyn_knowledgeconfiguration_createdonbehalfby";
                public const string LkMsdynKnowledgeconfigurationModifiedby = "lk_msdyn_knowledgeconfiguration_modifiedby";
                public const string LkMsdynKnowledgeconfigurationModifiedonbehalfby = "lk_msdyn_knowledgeconfiguration_modifiedonbehalfby";
                public const string LkMsdynKnowledgeharvestjobrecordCreatedby = "lk_msdyn_knowledgeharvestjobrecord_createdby";
                public const string LkMsdynKnowledgeharvestjobrecordCreatedonbehalfby = "lk_msdyn_knowledgeharvestjobrecord_createdonbehalfby";
                public const string LkMsdynKnowledgeharvestjobrecordModifiedby = "lk_msdyn_knowledgeharvestjobrecord_modifiedby";
                public const string LkMsdynKnowledgeharvestjobrecordModifiedonbehalfby = "lk_msdyn_knowledgeharvestjobrecord_modifiedonbehalfby";
                public const string LkMsdynKnowledgeharvestplanCreatedby = "lk_msdyn_knowledgeharvestplan_createdby";
                public const string LkMsdynKnowledgeharvestplanCreatedonbehalfby = "lk_msdyn_knowledgeharvestplan_createdonbehalfby";
                public const string LkMsdynKnowledgeharvestplanModifiedby = "lk_msdyn_knowledgeharvestplan_modifiedby";
                public const string LkMsdynKnowledgeharvestplanModifiedonbehalfby = "lk_msdyn_knowledgeharvestplan_modifiedonbehalfby";
                public const string LkMsdynKnowledgeinteractioninsightCreatedby = "lk_msdyn_knowledgeinteractioninsight_createdby";
                public const string LkMsdynKnowledgeinteractioninsightCreatedonbehalfby = "lk_msdyn_knowledgeinteractioninsight_createdonbehalfby";
                public const string LkMsdynKnowledgeinteractioninsightModifiedby = "lk_msdyn_knowledgeinteractioninsight_modifiedby";
                public const string LkMsdynKnowledgeinteractioninsightModifiedonbehalfby = "lk_msdyn_knowledgeinteractioninsight_modifiedonbehalfby";
                public const string LkMsdynKnowledgemanagementsettingCreatedby = "lk_msdyn_knowledgemanagementsetting_createdby";
                public const string LkMsdynKnowledgemanagementsettingCreatedonbehalfby = "lk_msdyn_knowledgemanagementsetting_createdonbehalfby";
                public const string LkMsdynKnowledgemanagementsettingModifiedby = "lk_msdyn_knowledgemanagementsetting_modifiedby";
                public const string LkMsdynKnowledgemanagementsettingModifiedonbehalfby = "lk_msdyn_knowledgemanagementsetting_modifiedonbehalfby";
                public const string LkMsdynKnowledgepersonalfilterCreatedby = "lk_msdyn_knowledgepersonalfilter_createdby";
                public const string LkMsdynKnowledgepersonalfilterCreatedonbehalfby = "lk_msdyn_knowledgepersonalfilter_createdonbehalfby";
                public const string LkMsdynKnowledgepersonalfilterModifiedby = "lk_msdyn_knowledgepersonalfilter_modifiedby";
                public const string LkMsdynKnowledgepersonalfilterModifiedonbehalfby = "lk_msdyn_knowledgepersonalfilter_modifiedonbehalfby";
                public const string LkMsdynKnowledgesearchfilterCreatedby = "lk_msdyn_knowledgesearchfilter_createdby";
                public const string LkMsdynKnowledgesearchfilterCreatedonbehalfby = "lk_msdyn_knowledgesearchfilter_createdonbehalfby";
                public const string LkMsdynKnowledgesearchfilterModifiedby = "lk_msdyn_knowledgesearchfilter_modifiedby";
                public const string LkMsdynKnowledgesearchfilterModifiedonbehalfby = "lk_msdyn_knowledgesearchfilter_modifiedonbehalfby";
                public const string LkMsdynKnowledgesearchinsightCreatedby = "lk_msdyn_knowledgesearchinsight_createdby";
                public const string LkMsdynKnowledgesearchinsightCreatedonbehalfby = "lk_msdyn_knowledgesearchinsight_createdonbehalfby";
                public const string LkMsdynKnowledgesearchinsightModifiedby = "lk_msdyn_knowledgesearchinsight_modifiedby";
                public const string LkMsdynKnowledgesearchinsightModifiedonbehalfby = "lk_msdyn_knowledgesearchinsight_modifiedonbehalfby";
                public const string LkMsdynMobileappCreatedby = "lk_msdyn_mobileapp_createdby";
                public const string LkMsdynMobileappCreatedonbehalfby = "lk_msdyn_mobileapp_createdonbehalfby";
                public const string LkMsdynMobileappModifiedby = "lk_msdyn_mobileapp_modifiedby";
                public const string LkMsdynMobileappModifiedonbehalfby = "lk_msdyn_mobileapp_modifiedonbehalfby";
                public const string LkMsdynModulerundetailCreatedby = "lk_msdyn_modulerundetail_createdby";
                public const string LkMsdynModulerundetailCreatedonbehalfby = "lk_msdyn_modulerundetail_createdonbehalfby";
                public const string LkMsdynModulerundetailModifiedby = "lk_msdyn_modulerundetail_modifiedby";
                public const string LkMsdynModulerundetailModifiedonbehalfby = "lk_msdyn_modulerundetail_modifiedonbehalfby";
                public const string LkMsdynObjectdetectionproductCreatedby = "lk_msdyn_objectdetectionproduct_createdby";
                public const string LkMsdynObjectdetectionproductCreatedonbehalfby = "lk_msdyn_objectdetectionproduct_createdonbehalfby";
                public const string LkMsdynObjectdetectionproductModifiedby = "lk_msdyn_objectdetectionproduct_modifiedby";
                public const string LkMsdynObjectdetectionproductModifiedonbehalfby = "lk_msdyn_objectdetectionproduct_modifiedonbehalfby";
                public const string LkMsdynOnlineshopperintentionCreatedby = "lk_msdyn_onlineshopperintention_createdby";
                public const string LkMsdynOnlineshopperintentionCreatedonbehalfby = "lk_msdyn_onlineshopperintention_createdonbehalfby";
                public const string LkMsdynOnlineshopperintentionModifiedby = "lk_msdyn_onlineshopperintention_modifiedby";
                public const string LkMsdynOnlineshopperintentionModifiedonbehalfby = "lk_msdyn_onlineshopperintention_modifiedonbehalfby";
                public const string LkMsdynPlanCreatedby = "lk_msdyn_plan_createdby";
                public const string LkMsdynPlanCreatedonbehalfby = "lk_msdyn_plan_createdonbehalfby";
                public const string LkMsdynPlanModifiedby = "lk_msdyn_plan_modifiedby";
                public const string LkMsdynPlanModifiedonbehalfby = "lk_msdyn_plan_modifiedonbehalfby";
                public const string LkMsdynPlanartifactCreatedby = "lk_msdyn_planartifact_createdby";
                public const string LkMsdynPlanartifactCreatedonbehalfby = "lk_msdyn_planartifact_createdonbehalfby";
                public const string LkMsdynPlanartifactModifiedby = "lk_msdyn_planartifact_modifiedby";
                public const string LkMsdynPlanartifactModifiedonbehalfby = "lk_msdyn_planartifact_modifiedonbehalfby";
                public const string LkMsdynPlanattachmentCreatedby = "lk_msdyn_planattachment_createdby";
                public const string LkMsdynPlanattachmentCreatedonbehalfby = "lk_msdyn_planattachment_createdonbehalfby";
                public const string LkMsdynPlanattachmentModifiedby = "lk_msdyn_planattachment_modifiedby";
                public const string LkMsdynPlanattachmentModifiedonbehalfby = "lk_msdyn_planattachment_modifiedonbehalfby";
                public const string LkMsdynPmanalysishistoryCreatedby = "lk_msdyn_pmanalysishistory_createdby";
                public const string LkMsdynPmanalysishistoryCreatedonbehalfby = "lk_msdyn_pmanalysishistory_createdonbehalfby";
                public const string LkMsdynPmanalysishistoryModifiedby = "lk_msdyn_pmanalysishistory_modifiedby";
                public const string LkMsdynPmanalysishistoryModifiedonbehalfby = "lk_msdyn_pmanalysishistory_modifiedonbehalfby";
                public const string LkMsdynPmbusinessruleautomationconfigCreatedby = "lk_msdyn_pmbusinessruleautomationconfig_createdby";
                public const string LkMsdynPmbusinessruleautomationconfigCreatedonbehalfby = "lk_msdyn_pmbusinessruleautomationconfig_createdonbehalfby";
                public const string LkMsdynPmbusinessruleautomationconfigModifiedby = "lk_msdyn_pmbusinessruleautomationconfig_modifiedby";
                public const string LkMsdynPmbusinessruleautomationconfigModifiedonbehalfby = "lk_msdyn_pmbusinessruleautomationconfig_modifiedonbehalfby";
                public const string LkMsdynPmcalendarCreatedby = "lk_msdyn_pmcalendar_createdby";
                public const string LkMsdynPmcalendarCreatedonbehalfby = "lk_msdyn_pmcalendar_createdonbehalfby";
                public const string LkMsdynPmcalendarModifiedby = "lk_msdyn_pmcalendar_modifiedby";
                public const string LkMsdynPmcalendarModifiedonbehalfby = "lk_msdyn_pmcalendar_modifiedonbehalfby";
                public const string LkMsdynPmcalendarversionCreatedby = "lk_msdyn_pmcalendarversion_createdby";
                public const string LkMsdynPmcalendarversionCreatedonbehalfby = "lk_msdyn_pmcalendarversion_createdonbehalfby";
                public const string LkMsdynPmcalendarversionModifiedby = "lk_msdyn_pmcalendarversion_modifiedby";
                public const string LkMsdynPmcalendarversionModifiedonbehalfby = "lk_msdyn_pmcalendarversion_modifiedonbehalfby";
                public const string LkMsdynPminferredtaskCreatedby = "lk_msdyn_pminferredtask_createdby";
                public const string LkMsdynPminferredtaskCreatedonbehalfby = "lk_msdyn_pminferredtask_createdonbehalfby";
                public const string LkMsdynPminferredtaskModifiedby = "lk_msdyn_pminferredtask_modifiedby";
                public const string LkMsdynPminferredtaskModifiedonbehalfby = "lk_msdyn_pminferredtask_modifiedonbehalfby";
                public const string LkMsdynPmprocessextendedmetadataversionCreatedby = "lk_msdyn_pmprocessextendedmetadataversion_createdby";
                public const string LkMsdynPmprocessextendedmetadataversionCreatedonbehalfby = "lk_msdyn_pmprocessextendedmetadataversion_createdonbehalfby";
                public const string LkMsdynPmprocessextendedmetadataversionModifiedby = "lk_msdyn_pmprocessextendedmetadataversion_modifiedby";
                public const string LkMsdynPmprocessextendedmetadataversionModifiedonbehalfby = "lk_msdyn_pmprocessextendedmetadataversion_modifiedonbehalfby";
                public const string LkMsdynPmprocesstemplateCreatedby = "lk_msdyn_pmprocesstemplate_createdby";
                public const string LkMsdynPmprocesstemplateCreatedonbehalfby = "lk_msdyn_pmprocesstemplate_createdonbehalfby";
                public const string LkMsdynPmprocesstemplateModifiedby = "lk_msdyn_pmprocesstemplate_modifiedby";
                public const string LkMsdynPmprocesstemplateModifiedonbehalfby = "lk_msdyn_pmprocesstemplate_modifiedonbehalfby";
                public const string LkMsdynPmprocessusersettingsCreatedby = "lk_msdyn_pmprocessusersettings_createdby";
                public const string LkMsdynPmprocessusersettingsCreatedonbehalfby = "lk_msdyn_pmprocessusersettings_createdonbehalfby";
                public const string LkMsdynPmprocessusersettingsModifiedby = "lk_msdyn_pmprocessusersettings_modifiedby";
                public const string LkMsdynPmprocessusersettingsModifiedonbehalfby = "lk_msdyn_pmprocessusersettings_modifiedonbehalfby";
                public const string LkMsdynPmprocessversionCreatedby = "lk_msdyn_pmprocessversion_createdby";
                public const string LkMsdynPmprocessversionCreatedonbehalfby = "lk_msdyn_pmprocessversion_createdonbehalfby";
                public const string LkMsdynPmprocessversionModifiedby = "lk_msdyn_pmprocessversion_modifiedby";
                public const string LkMsdynPmprocessversionModifiedonbehalfby = "lk_msdyn_pmprocessversion_modifiedonbehalfby";
                public const string LkMsdynPmrecordingCreatedby = "lk_msdyn_pmrecording_createdby";
                public const string LkMsdynPmrecordingCreatedonbehalfby = "lk_msdyn_pmrecording_createdonbehalfby";
                public const string LkMsdynPmrecordingModifiedby = "lk_msdyn_pmrecording_modifiedby";
                public const string LkMsdynPmrecordingModifiedonbehalfby = "lk_msdyn_pmrecording_modifiedonbehalfby";
                public const string LkMsdynPmsimulationCreatedby = "lk_msdyn_pmsimulation_createdby";
                public const string LkMsdynPmsimulationCreatedonbehalfby = "lk_msdyn_pmsimulation_createdonbehalfby";
                public const string LkMsdynPmsimulationModifiedby = "lk_msdyn_pmsimulation_modifiedby";
                public const string LkMsdynPmsimulationModifiedonbehalfby = "lk_msdyn_pmsimulation_modifiedonbehalfby";
                public const string LkMsdynPmtabCreatedby = "lk_msdyn_pmtab_createdby";
                public const string LkMsdynPmtabCreatedonbehalfby = "lk_msdyn_pmtab_createdonbehalfby";
                public const string LkMsdynPmtabModifiedby = "lk_msdyn_pmtab_modifiedby";
                public const string LkMsdynPmtabModifiedonbehalfby = "lk_msdyn_pmtab_modifiedonbehalfby";
                public const string LkMsdynPmtemplateCreatedby = "lk_msdyn_pmtemplate_createdby";
                public const string LkMsdynPmtemplateCreatedonbehalfby = "lk_msdyn_pmtemplate_createdonbehalfby";
                public const string LkMsdynPmtemplateModifiedby = "lk_msdyn_pmtemplate_modifiedby";
                public const string LkMsdynPmtemplateModifiedonbehalfby = "lk_msdyn_pmtemplate_modifiedonbehalfby";
                public const string LkMsdynPmviewCreatedby = "lk_msdyn_pmview_createdby";
                public const string LkMsdynPmviewCreatedonbehalfby = "lk_msdyn_pmview_createdonbehalfby";
                public const string LkMsdynPmviewModifiedby = "lk_msdyn_pmview_modifiedby";
                public const string LkMsdynPmviewModifiedonbehalfby = "lk_msdyn_pmview_modifiedonbehalfby";
                public const string LkMsdynPowerappswrapbuildCreatedby = "lk_msdyn_powerappswrapbuild_createdby";
                public const string LkMsdynPowerappswrapbuildCreatedonbehalfby = "lk_msdyn_powerappswrapbuild_createdonbehalfby";
                public const string LkMsdynPowerappswrapbuildModifiedby = "lk_msdyn_powerappswrapbuild_modifiedby";
                public const string LkMsdynPowerappswrapbuildModifiedonbehalfby = "lk_msdyn_powerappswrapbuild_modifiedonbehalfby";
                public const string LkMsdynQnaCreatedby = "lk_msdyn_qna_createdby";
                public const string LkMsdynQnaCreatedonbehalfby = "lk_msdyn_qna_createdonbehalfby";
                public const string LkMsdynQnaModifiedby = "lk_msdyn_qna_modifiedby";
                public const string LkMsdynQnaModifiedonbehalfby = "lk_msdyn_qna_modifiedonbehalfby";
                public const string LkMsdynRichtextfileCreatedby = "lk_msdyn_richtextfile_createdby";
                public const string LkMsdynRichtextfileCreatedonbehalfby = "lk_msdyn_richtextfile_createdonbehalfby";
                public const string LkMsdynRichtextfileModifiedby = "lk_msdyn_richtextfile_modifiedby";
                public const string LkMsdynRichtextfileModifiedonbehalfby = "lk_msdyn_richtextfile_modifiedonbehalfby";
                public const string LkMsdynRtestructuredtemplateCreatedby = "lk_msdyn_rtestructuredtemplate_createdby";
                public const string LkMsdynRtestructuredtemplateCreatedonbehalfby = "lk_msdyn_rtestructuredtemplate_createdonbehalfby";
                public const string LkMsdynRtestructuredtemplateModifiedby = "lk_msdyn_rtestructuredtemplate_modifiedby";
                public const string LkMsdynRtestructuredtemplateModifiedonbehalfby = "lk_msdyn_rtestructuredtemplate_modifiedonbehalfby";
                public const string LkMsdynRtestructuredtemplateconfigCreatedby = "lk_msdyn_rtestructuredtemplateconfig_createdby";
                public const string LkMsdynRtestructuredtemplateconfigCreatedonbehalfby = "lk_msdyn_rtestructuredtemplateconfig_createdonbehalfby";
                public const string LkMsdynRtestructuredtemplateconfigModifiedby = "lk_msdyn_rtestructuredtemplateconfig_modifiedby";
                public const string LkMsdynRtestructuredtemplateconfigModifiedonbehalfby = "lk_msdyn_rtestructuredtemplateconfig_modifiedonbehalfby";
                public const string LkMsdynRtetemplatemappingCreatedby = "lk_msdyn_rtetemplatemapping_createdby";
                public const string LkMsdynRtetemplatemappingCreatedonbehalfby = "lk_msdyn_rtetemplatemapping_createdonbehalfby";
                public const string LkMsdynRtetemplatemappingModifiedby = "lk_msdyn_rtetemplatemapping_modifiedby";
                public const string LkMsdynRtetemplatemappingModifiedonbehalfby = "lk_msdyn_rtetemplatemapping_modifiedonbehalfby";
                public const string LkMsdynSalesforcestructuredobjectCreatedby = "lk_msdyn_salesforcestructuredobject_createdby";
                public const string LkMsdynSalesforcestructuredobjectCreatedonbehalfby = "lk_msdyn_salesforcestructuredobject_createdonbehalfby";
                public const string LkMsdynSalesforcestructuredobjectModifiedby = "lk_msdyn_salesforcestructuredobject_modifiedby";
                public const string LkMsdynSalesforcestructuredobjectModifiedonbehalfby = "lk_msdyn_salesforcestructuredobject_modifiedonbehalfby";
                public const string LkMsdynSalesforcestructuredqnaconfigCreatedby = "lk_msdyn_salesforcestructuredqnaconfig_createdby";
                public const string LkMsdynSalesforcestructuredqnaconfigCreatedonbehalfby = "lk_msdyn_salesforcestructuredqnaconfig_createdonbehalfby";
                public const string LkMsdynSalesforcestructuredqnaconfigModifiedby = "lk_msdyn_salesforcestructuredqnaconfig_modifiedby";
                public const string LkMsdynSalesforcestructuredqnaconfigModifiedonbehalfby = "lk_msdyn_salesforcestructuredqnaconfig_modifiedonbehalfby";
                public const string LkMsdynScheduleCreatedby = "lk_msdyn_schedule_createdby";
                public const string LkMsdynScheduleCreatedonbehalfby = "lk_msdyn_schedule_createdonbehalfby";
                public const string LkMsdynScheduleModifiedby = "lk_msdyn_schedule_modifiedby";
                public const string LkMsdynScheduleModifiedonbehalfby = "lk_msdyn_schedule_modifiedonbehalfby";
                public const string LkMsdynServiceconfigurationCreatedby = "lk_msdyn_serviceconfiguration_createdby";
                public const string LkMsdynServiceconfigurationCreatedonbehalfby = "lk_msdyn_serviceconfiguration_createdonbehalfby";
                public const string LkMsdynServiceconfigurationModifiedby = "lk_msdyn_serviceconfiguration_modifiedby";
                public const string LkMsdynServiceconfigurationModifiedonbehalfby = "lk_msdyn_serviceconfiguration_modifiedonbehalfby";
                public const string LkMsdynSlakpiCreatedby = "lk_msdyn_slakpi_createdby";
                public const string LkMsdynSlakpiCreatedonbehalfby = "lk_msdyn_slakpi_createdonbehalfby";
                public const string LkMsdynSlakpiModifiedby = "lk_msdyn_slakpi_modifiedby";
                public const string LkMsdynSlakpiModifiedonbehalfby = "lk_msdyn_slakpi_modifiedonbehalfby";
                public const string LkMsdynSolutionhealthruleCreatedby = "lk_msdyn_solutionhealthrule_createdby";
                public const string LkMsdynSolutionhealthruleCreatedonbehalfby = "lk_msdyn_solutionhealthrule_createdonbehalfby";
                public const string LkMsdynSolutionhealthruleModifiedby = "lk_msdyn_solutionhealthrule_modifiedby";
                public const string LkMsdynSolutionhealthruleModifiedonbehalfby = "lk_msdyn_solutionhealthrule_modifiedonbehalfby";
                public const string LkMsdynSolutionhealthruleargumentCreatedby = "lk_msdyn_solutionhealthruleargument_createdby";
                public const string LkMsdynSolutionhealthruleargumentCreatedonbehalfby = "lk_msdyn_solutionhealthruleargument_createdonbehalfby";
                public const string LkMsdynSolutionhealthruleargumentModifiedby = "lk_msdyn_solutionhealthruleargument_modifiedby";
                public const string LkMsdynSolutionhealthruleargumentModifiedonbehalfby = "lk_msdyn_solutionhealthruleargument_modifiedonbehalfby";
                public const string LkMsdynSolutionhealthrulesetCreatedby = "lk_msdyn_solutionhealthruleset_createdby";
                public const string LkMsdynSolutionhealthrulesetCreatedonbehalfby = "lk_msdyn_solutionhealthruleset_createdonbehalfby";
                public const string LkMsdynSolutionhealthrulesetModifiedby = "lk_msdyn_solutionhealthruleset_modifiedby";
                public const string LkMsdynSolutionhealthrulesetModifiedonbehalfby = "lk_msdyn_solutionhealthruleset_modifiedonbehalfby";
                public const string LkMsdynTimelinepinCreatedby = "lk_msdyn_timelinepin_createdby";
                public const string LkMsdynTimelinepinCreatedonbehalfby = "lk_msdyn_timelinepin_createdonbehalfby";
                public const string LkMsdynTimelinepinModifiedby = "lk_msdyn_timelinepin_modifiedby";
                public const string LkMsdynTimelinepinModifiedonbehalfby = "lk_msdyn_timelinepin_modifiedonbehalfby";
                public const string LkMsdynTourCreatedby = "lk_msdyn_tour_createdby";
                public const string LkMsdynTourCreatedonbehalfby = "lk_msdyn_tour_createdonbehalfby";
                public const string LkMsdynTourModifiedby = "lk_msdyn_tour_modifiedby";
                public const string LkMsdynTourModifiedonbehalfby = "lk_msdyn_tour_modifiedonbehalfby";
                public const string LkMsdynVirtualtablecolumncandidateCreatedby = "lk_msdyn_virtualtablecolumncandidate_createdby";
                public const string LkMsdynVirtualtablecolumncandidateCreatedonbehalfby = "lk_msdyn_virtualtablecolumncandidate_createdonbehalfby";
                public const string LkMsdynVirtualtablecolumncandidateModifiedby = "lk_msdyn_virtualtablecolumncandidate_modifiedby";
                public const string LkMsdynVirtualtablecolumncandidateModifiedonbehalfby = "lk_msdyn_virtualtablecolumncandidate_modifiedonbehalfby";
                public const string LkMsdynWorkflowactionstatusCreatedby = "lk_msdyn_workflowactionstatus_createdby";
                public const string LkMsdynWorkflowactionstatusCreatedonbehalfby = "lk_msdyn_workflowactionstatus_createdonbehalfby";
                public const string LkMsdynWorkflowactionstatusModifiedby = "lk_msdyn_workflowactionstatus_modifiedby";
                public const string LkMsdynWorkflowactionstatusModifiedonbehalfby = "lk_msdyn_workflowactionstatus_modifiedonbehalfby";
                public const string LkMsdynceBotcontentCreatedby = "lk_msdynce_botcontent_createdby";
                public const string LkMsdynceBotcontentCreatedonbehalfby = "lk_msdynce_botcontent_createdonbehalfby";
                public const string LkMsdynceBotcontentModifiedby = "lk_msdynce_botcontent_modifiedby";
                public const string LkMsdynceBotcontentModifiedonbehalfby = "lk_msdynce_botcontent_modifiedonbehalfby";
                public const string LkMspcatCatalogsubmissionfilesCreatedby = "lk_mspcat_catalogsubmissionfiles_createdby";
                public const string LkMspcatCatalogsubmissionfilesCreatedonbehalfby = "lk_mspcat_catalogsubmissionfiles_createdonbehalfby";
                public const string LkMspcatCatalogsubmissionfilesModifiedby = "lk_mspcat_catalogsubmissionfiles_modifiedby";
                public const string LkMspcatCatalogsubmissionfilesModifiedonbehalfby = "lk_mspcat_catalogsubmissionfiles_modifiedonbehalfby";
                public const string LkMspcatPackagestoreCreatedby = "lk_mspcat_packagestore_createdby";
                public const string LkMspcatPackagestoreCreatedonbehalfby = "lk_mspcat_packagestore_createdonbehalfby";
                public const string LkMspcatPackagestoreModifiedby = "lk_mspcat_packagestore_modifiedby";
                public const string LkMspcatPackagestoreModifiedonbehalfby = "lk_mspcat_packagestore_modifiedonbehalfby";
                public const string LkNavigationsettingCreatedby = "lk_navigationsetting_createdby";
                public const string LkNavigationsettingCreatedonbehalfby = "lk_navigationsetting_createdonbehalfby";
                public const string LkNavigationsettingModifiedby = "lk_navigationsetting_modifiedby";
                public const string LkNavigationsettingModifiedonbehalfby = "lk_navigationsetting_modifiedonbehalfby";
                public const string LkNewprocessCreatedby = "lk_newprocess_createdby";
                public const string LkNewprocessCreatedonbehalfby = "lk_newprocess_createdonbehalfby";
                public const string LkNewprocessModifiedby = "lk_newprocess_modifiedby";
                public const string LkNewprocessModifiedonbehalfby = "lk_newprocess_modifiedonbehalfby";
                public const string LkNlsqregistrationCreatedby = "lk_nlsqregistration_createdby";
                public const string LkNlsqregistrationCreatedonbehalfby = "lk_nlsqregistration_createdonbehalfby";
                public const string LkNlsqregistrationModifiedby = "lk_nlsqregistration_modifiedby";
                public const string LkNlsqregistrationModifiedonbehalfby = "lk_nlsqregistration_modifiedonbehalfby";
                public const string LkOfficedocumentbaseCreatedonbehalfby = "lk_officedocumentbase_createdonbehalfby";
                public const string LkOfficedocumentbaseModifiedonbehalfby = "lk_officedocumentbase_modifiedonbehalfby";
                public const string LkOfficegraphdocumentCreatedonbehalfby = "lk_officegraphdocument_createdonbehalfby";
                public const string LkOfficegraphdocumentModifiedonbehalfby = "lk_officegraphdocument_modifiedonbehalfby";
                public const string LkOfflinecommanddefinitionCreatedby = "lk_offlinecommanddefinition_createdby";
                public const string LkOfflinecommanddefinitionCreatedonbehalfby = "lk_offlinecommanddefinition_createdonbehalfby";
                public const string LkOfflinecommanddefinitionModifiedby = "lk_offlinecommanddefinition_modifiedby";
                public const string LkOfflinecommanddefinitionModifiedonbehalfby = "lk_offlinecommanddefinition_modifiedonbehalfby";
                public const string LkOrganizationCreatedonbehalfby = "lk_organization_createdonbehalfby";
                public const string LkOrganizationModifiedonbehalfby = "lk_organization_modifiedonbehalfby";
                public const string LkOrganizationbaseCreatedby = "lk_organizationbase_createdby";
                public const string LkOrganizationbaseModifiedby = "lk_organizationbase_modifiedby";
                public const string LkOrganizationdatasyncfnostateCreatedby = "lk_organizationdatasyncfnostate_createdby";
                public const string LkOrganizationdatasyncfnostateCreatedonbehalfby = "lk_organizationdatasyncfnostate_createdonbehalfby";
                public const string LkOrganizationdatasyncfnostateModifiedby = "lk_organizationdatasyncfnostate_modifiedby";
                public const string LkOrganizationdatasyncfnostateModifiedonbehalfby = "lk_organizationdatasyncfnostate_modifiedonbehalfby";
                public const string LkOrganizationdatasyncstateCreatedby = "lk_organizationdatasyncstate_createdby";
                public const string LkOrganizationdatasyncstateCreatedonbehalfby = "lk_organizationdatasyncstate_createdonbehalfby";
                public const string LkOrganizationdatasyncstateModifiedby = "lk_organizationdatasyncstate_modifiedby";
                public const string LkOrganizationdatasyncstateModifiedonbehalfby = "lk_organizationdatasyncstate_modifiedonbehalfby";
                public const string LkOrganizationdatasyncsubscriptionCreatedby = "lk_organizationdatasyncsubscription_createdby";
                public const string LkOrganizationdatasyncsubscriptionCreatedonbehalfby = "lk_organizationdatasyncsubscription_createdonbehalfby";
                public const string LkOrganizationdatasyncsubscriptionModifiedby = "lk_organizationdatasyncsubscription_modifiedby";
                public const string LkOrganizationdatasyncsubscriptionModifiedonbehalfby = "lk_organizationdatasyncsubscription_modifiedonbehalfby";
                public const string LkOrganizationdatasyncsubscriptionentityCreatedby = "lk_organizationdatasyncsubscriptionentity_createdby";
                public const string LkOrganizationdatasyncsubscriptionentityCreatedonbehalfby = "lk_organizationdatasyncsubscriptionentity_createdonbehalfby";
                public const string LkOrganizationdatasyncsubscriptionentityModifiedby = "lk_organizationdatasyncsubscriptionentity_modifiedby";
                public const string LkOrganizationdatasyncsubscriptionentityModifiedonbehalfby = "lk_organizationdatasyncsubscriptionentity_modifiedonbehalfby";
                public const string LkOrganizationdatasyncsubscriptionfnotableCreatedby = "lk_organizationdatasyncsubscriptionfnotable_createdby";
                public const string LkOrganizationdatasyncsubscriptionfnotableCreatedonbehalfby = "lk_organizationdatasyncsubscriptionfnotable_createdonbehalfby";
                public const string LkOrganizationdatasyncsubscriptionfnotableModifiedby = "lk_organizationdatasyncsubscriptionfnotable_modifiedby";
                public const string LkOrganizationdatasyncsubscriptionfnotableModifiedonbehalfby = "lk_organizationdatasyncsubscriptionfnotable_modifiedonbehalfby";
                public const string LkOrganizationsettingCreatedby = "lk_organizationsetting_createdby";
                public const string LkOrganizationsettingCreatedonbehalfby = "lk_organizationsetting_createdonbehalfby";
                public const string LkOrganizationsettingModifiedby = "lk_organizationsetting_modifiedby";
                public const string LkOrganizationsettingModifiedonbehalfby = "lk_organizationsetting_modifiedonbehalfby";
                public const string LkOwnermappingCreatedby = "lk_ownermapping_createdby";
                public const string LkOwnermappingCreatedonbehalfby = "lk_ownermapping_createdonbehalfby";
                public const string LkOwnermappingModifiedby = "lk_ownermapping_modifiedby";
                public const string LkOwnermappingModifiedonbehalfby = "lk_ownermapping_modifiedonbehalfby";
                public const string LkPackageCreatedby = "lk_package_createdby";
                public const string LkPackageCreatedonbehalfby = "lk_package_createdonbehalfby";
                public const string LkPackageModifiedby = "lk_package_modifiedby";
                public const string LkPackageModifiedonbehalfby = "lk_package_modifiedonbehalfby";
                public const string LkPackagehistoryCreatedby = "lk_packagehistory_createdby";
                public const string LkPackagehistoryCreatedonbehalfby = "lk_packagehistory_createdonbehalfby";
                public const string LkPackagehistoryModifiedby = "lk_packagehistory_modifiedby";
                public const string LkPackagehistoryModifiedonbehalfby = "lk_packagehistory_modifiedonbehalfby";
                public const string LkPartnerapplicationCreatedby = "lk_partnerapplication_createdby";
                public const string LkPartnerapplicationCreatedonbehalfby = "lk_partnerapplication_createdonbehalfby";
                public const string LkPartnerapplicationModifiedby = "lk_partnerapplication_modifiedby";
                public const string LkPartnerapplicationModifiedonbehalfby = "lk_partnerapplication_modifiedonbehalfby";
                public const string LkPdfsettingCreatedby = "lk_pdfsetting_createdby";
                public const string LkPdfsettingCreatedonbehalfby = "lk_pdfsetting_createdonbehalfby";
                public const string LkPdfsettingModifiedby = "lk_pdfsetting_modifiedby";
                public const string LkPdfsettingModifiedonbehalfby = "lk_pdfsetting_modifiedonbehalfby";
                public const string LkPersonaldocumenttemplatebaseCreatedby = "lk_personaldocumenttemplatebase_createdby";
                public const string LkPersonaldocumenttemplatebaseCreatedonbehalfby = "lk_personaldocumenttemplatebase_createdonbehalfby";
                public const string LkPersonaldocumenttemplatebaseModifiedby = "lk_personaldocumenttemplatebase_modifiedby";
                public const string LkPersonaldocumenttemplatebaseModifiedonbehalfby = "lk_personaldocumenttemplatebase_modifiedonbehalfby";
                public const string LkPhonecallCreatedby = "lk_phonecall_createdby";
                public const string LkPhonecallCreatedonbehalfby = "lk_phonecall_createdonbehalfby";
                public const string LkPhonecallModifiedby = "lk_phonecall_modifiedby";
                public const string LkPhonecallModifiedonbehalfby = "lk_phonecall_modifiedonbehalfby";
                public const string LkPicklistmappingCreatedby = "lk_picklistmapping_createdby";
                public const string LkPicklistmappingCreatedonbehalfby = "lk_picklistmapping_createdonbehalfby";
                public const string LkPicklistmappingModifiedby = "lk_picklistmapping_modifiedby";
                public const string LkPicklistmappingModifiedonbehalfby = "lk_picklistmapping_modifiedonbehalfby";
                public const string LkPluginCreatedby = "lk_plugin_createdby";
                public const string LkPluginCreatedonbehalfby = "lk_plugin_createdonbehalfby";
                public const string LkPluginModifiedby = "lk_plugin_modifiedby";
                public const string LkPluginModifiedonbehalfby = "lk_plugin_modifiedonbehalfby";
                public const string LkPluginassemblyCreatedonbehalfby = "lk_pluginassembly_createdonbehalfby";
                public const string LkPluginassemblyModifiedonbehalfby = "lk_pluginassembly_modifiedonbehalfby";
                public const string LkPluginpackageCreatedby = "lk_pluginpackage_createdby";
                public const string LkPluginpackageCreatedonbehalfby = "lk_pluginpackage_createdonbehalfby";
                public const string LkPluginpackageModifiedby = "lk_pluginpackage_modifiedby";
                public const string LkPluginpackageModifiedonbehalfby = "lk_pluginpackage_modifiedonbehalfby";
                public const string LkPlugintracelogbaseCreatedonbehalfby = "lk_plugintracelogbase_createdonbehalfby";
                public const string LkPlugintypeCreatedonbehalfby = "lk_plugintype_createdonbehalfby";
                public const string LkPlugintypeModifiedonbehalfby = "lk_plugintype_modifiedonbehalfby";
                public const string LkPlugintypestatisticbaseCreatedonbehalfby = "lk_plugintypestatisticbase_createdonbehalfby";
                public const string LkPlugintypestatisticbaseModifiedonbehalfby = "lk_plugintypestatisticbase_modifiedonbehalfby";
                public const string LkPositionCreatedby = "lk_position_createdby";
                public const string LkPositionCreatedonbehalfby = "lk_position_createdonbehalfby";
                public const string LkPositionModifiedby = "lk_position_modifiedby";
                public const string LkPositionModifiedonbehalfby = "lk_position_modifiedonbehalfby";
                public const string LkPostCreatedby = "lk_post_createdby";
                public const string LkPostCreatedonbehalfby = "lk_post_createdonbehalfby";
                public const string LkPostModifiedby = "lk_post_modifiedby";
                public const string LkPostModifiedonbehalfby = "lk_post_modifiedonbehalfby";
                public const string LkPostcommentCreatedby = "lk_postcomment_createdby";
                public const string LkPostcommentCreatedonbehalfby = "lk_postcomment_createdonbehalfby";
                public const string LkPostFollowCreatedby = "lk_PostFollow_createdby";
                public const string LkPostfollowCreatedonbehalfby = "lk_postfollow_createdonbehalfby";
                public const string LkPostlikeCreatedby = "lk_postlike_createdby";
                public const string LkPostlikeCreatedonbehalfby = "lk_postlike_createdonbehalfby";
                public const string LkPowerfxruleCreatedby = "lk_powerfxrule_createdby";
                public const string LkPowerfxruleCreatedonbehalfby = "lk_powerfxrule_createdonbehalfby";
                public const string LkPowerfxruleModifiedby = "lk_powerfxrule_modifiedby";
                public const string LkPowerfxruleModifiedonbehalfby = "lk_powerfxrule_modifiedonbehalfby";
                public const string LkPowerpagecomponentCreatedby = "lk_powerpagecomponent_createdby";
                public const string LkPowerpagecomponentCreatedonbehalfby = "lk_powerpagecomponent_createdonbehalfby";
                public const string LkPowerpagecomponentModifiedby = "lk_powerpagecomponent_modifiedby";
                public const string LkPowerpagecomponentModifiedonbehalfby = "lk_powerpagecomponent_modifiedonbehalfby";
                public const string LkPowerpagesddosalertCreatedby = "lk_powerpagesddosalert_createdby";
                public const string LkPowerpagesddosalertCreatedonbehalfby = "lk_powerpagesddosalert_createdonbehalfby";
                public const string LkPowerpagesddosalertModifiedby = "lk_powerpagesddosalert_modifiedby";
                public const string LkPowerpagesddosalertModifiedonbehalfby = "lk_powerpagesddosalert_modifiedonbehalfby";
                public const string LkPowerpagesiteCreatedby = "lk_powerpagesite_createdby";
                public const string LkPowerpagesiteCreatedonbehalfby = "lk_powerpagesite_createdonbehalfby";
                public const string LkPowerpagesiteModifiedby = "lk_powerpagesite_modifiedby";
                public const string LkPowerpagesiteModifiedonbehalfby = "lk_powerpagesite_modifiedonbehalfby";
                public const string LkPowerpagesitelanguageCreatedby = "lk_powerpagesitelanguage_createdby";
                public const string LkPowerpagesitelanguageCreatedonbehalfby = "lk_powerpagesitelanguage_createdonbehalfby";
                public const string LkPowerpagesitelanguageModifiedby = "lk_powerpagesitelanguage_modifiedby";
                public const string LkPowerpagesitelanguageModifiedonbehalfby = "lk_powerpagesitelanguage_modifiedonbehalfby";
                public const string LkPowerpagesitepublishedCreatedby = "lk_powerpagesitepublished_createdby";
                public const string LkPowerpagesitepublishedCreatedonbehalfby = "lk_powerpagesitepublished_createdonbehalfby";
                public const string LkPowerpagesitepublishedModifiedby = "lk_powerpagesitepublished_modifiedby";
                public const string LkPowerpagesitepublishedModifiedonbehalfby = "lk_powerpagesitepublished_modifiedonbehalfby";
                public const string LkPowerpageslogCreatedby = "lk_powerpageslog_createdby";
                public const string LkPowerpageslogCreatedonbehalfby = "lk_powerpageslog_createdonbehalfby";
                public const string LkPowerpageslogModifiedby = "lk_powerpageslog_modifiedby";
                public const string LkPowerpageslogModifiedonbehalfby = "lk_powerpageslog_modifiedonbehalfby";
                public const string LkPowerpagesmanagedidentityCreatedby = "lk_powerpagesmanagedidentity_createdby";
                public const string LkPowerpagesmanagedidentityCreatedonbehalfby = "lk_powerpagesmanagedidentity_createdonbehalfby";
                public const string LkPowerpagesmanagedidentityModifiedby = "lk_powerpagesmanagedidentity_modifiedby";
                public const string LkPowerpagesmanagedidentityModifiedonbehalfby = "lk_powerpagesmanagedidentity_modifiedonbehalfby";
                public const string LkPowerpagesscanreportCreatedby = "lk_powerpagesscanreport_createdby";
                public const string LkPowerpagesscanreportCreatedonbehalfby = "lk_powerpagesscanreport_createdonbehalfby";
                public const string LkPowerpagesscanreportModifiedby = "lk_powerpagesscanreport_modifiedby";
                public const string LkPowerpagesscanreportModifiedonbehalfby = "lk_powerpagesscanreport_modifiedonbehalfby";
                public const string LkPowerpagessiteaifeedbackCreatedby = "lk_powerpagessiteaifeedback_createdby";
                public const string LkPowerpagessiteaifeedbackCreatedonbehalfby = "lk_powerpagessiteaifeedback_createdonbehalfby";
                public const string LkPowerpagessiteaifeedbackModifiedby = "lk_powerpagessiteaifeedback_modifiedby";
                public const string LkPowerpagessiteaifeedbackModifiedonbehalfby = "lk_powerpagessiteaifeedback_modifiedonbehalfby";
                public const string LkPowerpagessourcefileCreatedby = "lk_powerpagessourcefile_createdby";
                public const string LkPowerpagessourcefileCreatedonbehalfby = "lk_powerpagessourcefile_createdonbehalfby";
                public const string LkPowerpagessourcefileModifiedby = "lk_powerpagessourcefile_modifiedby";
                public const string LkPowerpagessourcefileModifiedonbehalfby = "lk_powerpagessourcefile_modifiedonbehalfby";
                public const string LkPrivilegecheckerlogCreatedby = "lk_privilegecheckerlog_createdby";
                public const string LkPrivilegecheckerlogCreatedonbehalfby = "lk_privilegecheckerlog_createdonbehalfby";
                public const string LkPrivilegecheckerlogModifiedby = "lk_privilegecheckerlog_modifiedby";
                public const string LkPrivilegecheckerlogModifiedonbehalfby = "lk_privilegecheckerlog_modifiedonbehalfby";
                public const string LkPrivilegecheckerrunCreatedby = "lk_privilegecheckerrun_createdby";
                public const string LkPrivilegecheckerrunCreatedonbehalfby = "lk_privilegecheckerrun_createdonbehalfby";
                public const string LkPrivilegecheckerrunModifiedby = "lk_privilegecheckerrun_modifiedby";
                public const string LkPrivilegecheckerrunModifiedonbehalfby = "lk_privilegecheckerrun_modifiedonbehalfby";
                public const string LkPrivilegesremovalsettingCreatedby = "lk_privilegesremovalsetting_createdby";
                public const string LkPrivilegesremovalsettingCreatedonbehalfby = "lk_privilegesremovalsetting_createdonbehalfby";
                public const string LkPrivilegesremovalsettingModifiedby = "lk_privilegesremovalsetting_modifiedby";
                public const string LkPrivilegesremovalsettingModifiedonbehalfby = "lk_privilegesremovalsetting_modifiedonbehalfby";
                public const string LkProcessorregistrationCreatedby = "lk_processorregistration_createdby";
                public const string LkProcessorregistrationCreatedonbehalfby = "lk_processorregistration_createdonbehalfby";
                public const string LkProcessorregistrationModifiedby = "lk_processorregistration_modifiedby";
                public const string LkProcessorregistrationModifiedonbehalfby = "lk_processorregistration_modifiedonbehalfby";
                public const string LkProcesssessionCanceledby = "lk_processsession_canceledby";
                public const string LkProcesssessionCompletedby = "lk_processsession_completedby";
                public const string LkProcesssessionCreatedby = "lk_processsession_createdby";
                public const string LkProcesssessionExecutedby = "lk_processsession_executedby";
                public const string LkProcesssessionModifiedby = "lk_processsession_modifiedby";
                public const string LkProcesssessionStartedby = "lk_processsession_startedby";
                public const string LkProcesssessionbaseCreatedonbehalfby = "lk_processsessionbase_createdonbehalfby";
                public const string LkProcesssessionbaseModifiedonbehalfby = "lk_processsessionbase_modifiedonbehalfby";
                public const string LkProcessstageparameterCreatedby = "lk_processstageparameter_createdby";
                public const string LkProcessstageparameterCreatedonbehalfby = "lk_processstageparameter_createdonbehalfby";
                public const string LkProcessstageparameterModifiedby = "lk_processstageparameter_modifiedby";
                public const string LkProcessstageparameterModifiedonbehalfby = "lk_processstageparameter_modifiedonbehalfby";
                public const string LkProcesstriggerbaseCreatedby = "lk_processtriggerbase_createdby";
                public const string LkProcesstriggerbaseCreatedonbehalfby = "lk_processtriggerbase_createdonbehalfby";
                public const string LkProcesstriggerbaseModifiedby = "lk_processtriggerbase_modifiedby";
                public const string LkProcesstriggerbaseModifiedonbehalfby = "lk_processtriggerbase_modifiedonbehalfby";
                public const string LkProfileruleCreatedby = "lk_profilerule_createdby";
                public const string LkProfileruleCreatedonbehalfby = "lk_profilerule_createdonbehalfby";
                public const string LkProfileruleModifiedby = "lk_profilerule_modifiedby";
                public const string LkProfileruleModifiedonbehalfby = "lk_profilerule_modifiedonbehalfby";
                public const string LkProfileruleitemCreatedby = "lk_profileruleitem_createdby";
                public const string LkProfileruleitemCreatedonbehalfby = "lk_profileruleitem_createdonbehalfby";
                public const string LkProfileruleitemModifiedby = "lk_profileruleitem_modifiedby";
                public const string LkProfileruleitemModifiedonbehalfby = "lk_profileruleitem_modifiedonbehalfby";
                public const string LkPublisherCreatedby = "lk_publisher_createdby";
                public const string LkPublisherModifiedby = "lk_publisher_modifiedby";
                public const string LkPublisheraddressbaseCreatedby = "lk_publisheraddressbase_createdby";
                public const string LkPublisheraddressbaseCreatedonbehalfby = "lk_publisheraddressbase_createdonbehalfby";
                public const string LkPublisheraddressbaseModifiedby = "lk_publisheraddressbase_modifiedby";
                public const string LkPublisheraddressbaseModifiedonbehalfby = "lk_publisheraddressbase_modifiedonbehalfby";
                public const string LkPublisherbaseCreatedonbehalfby = "lk_publisherbase_createdonbehalfby";
                public const string LkPublisherbaseModifiedonbehalfby = "lk_publisherbase_modifiedonbehalfby";
                public const string LkPurviewlabelinfoCreatedby = "lk_purviewlabelinfo_createdby";
                public const string LkPurviewlabelinfoCreatedonbehalfby = "lk_purviewlabelinfo_createdonbehalfby";
                public const string LkPurviewlabelinfoModifiedby = "lk_purviewlabelinfo_modifiedby";
                public const string LkPurviewlabelinfoModifiedonbehalfby = "lk_purviewlabelinfo_modifiedonbehalfby";
                public const string LkPurviewlabelsynccacheCreatedby = "lk_purviewlabelsynccache_createdby";
                public const string LkPurviewlabelsynccacheCreatedonbehalfby = "lk_purviewlabelsynccache_createdonbehalfby";
                public const string LkPurviewlabelsynccacheModifiedby = "lk_purviewlabelsynccache_modifiedby";
                public const string LkPurviewlabelsynccacheModifiedonbehalfby = "lk_purviewlabelsynccache_modifiedonbehalfby";
                public const string LkQuarterlyfiscalcalendarCreatedby = "lk_quarterlyfiscalcalendar_createdby";
                public const string LkQuarterlyfiscalcalendarCreatedonbehalfby = "lk_quarterlyfiscalcalendar_createdonbehalfby";
                public const string LkQuarterlyfiscalcalendarModifiedby = "lk_quarterlyfiscalcalendar_modifiedby";
                public const string LkQuarterlyfiscalcalendarModifiedonbehalfby = "lk_quarterlyfiscalcalendar_modifiedonbehalfby";
                public const string LkQuarterlyfiscalcalendarSalespersonid = "lk_quarterlyfiscalcalendar_salespersonid";
                public const string LkQueueCreatedonbehalfby = "lk_queue_createdonbehalfby";
                public const string LkQueueModifiedonbehalfby = "lk_queue_modifiedonbehalfby";
                public const string LkQueuebaseCreatedby = "lk_queuebase_createdby";
                public const string LkQueuebaseModifiedby = "lk_queuebase_modifiedby";
                public const string LkQueueitemCreatedonbehalfby = "lk_queueitem_createdonbehalfby";
                public const string LkQueueitemModifiedonbehalfby = "lk_queueitem_modifiedonbehalfby";
                public const string LkQueueitembaseCreatedby = "lk_queueitembase_createdby";
                public const string LkQueueitembaseModifiedby = "lk_queueitembase_modifiedby";
                public const string LkQueueitembaseWorkerid = "lk_queueitembase_workerid";
                public const string LkRecentlyusedCreatedby = "lk_recentlyused_createdby";
                public const string LkRecentlyusedCreatedonbehalfby = "lk_recentlyused_createdonbehalfby";
                public const string LkRecentlyusedModifiedby = "lk_recentlyused_modifiedby";
                public const string LkRecentlyusedModifiedonbehalfby = "lk_recentlyused_modifiedonbehalfby";
                public const string LkRecommendeddocumentCreatedby = "lk_recommendeddocument_createdby";
                public const string LkRecommendeddocumentCreatedonbehalfby = "lk_recommendeddocument_createdonbehalfby";
                public const string LkRecommendeddocumentModifiedby = "lk_recommendeddocument_modifiedby";
                public const string LkRecommendeddocumentModifiedonbehalfby = "lk_recommendeddocument_modifiedonbehalfby";
                public const string LkReconciliationentityinfoCreatedby = "lk_reconciliationentityinfo_createdby";
                public const string LkReconciliationentityinfoCreatedonbehalfby = "lk_reconciliationentityinfo_createdonbehalfby";
                public const string LkReconciliationentityinfoModifiedby = "lk_reconciliationentityinfo_modifiedby";
                public const string LkReconciliationentityinfoModifiedonbehalfby = "lk_reconciliationentityinfo_modifiedonbehalfby";
                public const string LkReconciliationentitystepinfoCreatedby = "lk_reconciliationentitystepinfo_createdby";
                public const string LkReconciliationentitystepinfoCreatedonbehalfby = "lk_reconciliationentitystepinfo_createdonbehalfby";
                public const string LkReconciliationentitystepinfoModifiedby = "lk_reconciliationentitystepinfo_modifiedby";
                public const string LkReconciliationentitystepinfoModifiedonbehalfby = "lk_reconciliationentitystepinfo_modifiedonbehalfby";
                public const string LkReconciliationinfoCreatedby = "lk_reconciliationinfo_createdby";
                public const string LkReconciliationinfoCreatedonbehalfby = "lk_reconciliationinfo_createdonbehalfby";
                public const string LkReconciliationinfoModifiedby = "lk_reconciliationinfo_modifiedby";
                public const string LkReconciliationinfoModifiedonbehalfby = "lk_reconciliationinfo_modifiedonbehalfby";
                public const string LkRecordfilterCreatedby = "lk_recordfilter_createdby";
                public const string LkRecordfilterCreatedonbehalfby = "lk_recordfilter_createdonbehalfby";
                public const string LkRecordfilterModifiedby = "lk_recordfilter_modifiedby";
                public const string LkRecordfilterModifiedonbehalfby = "lk_recordfilter_modifiedonbehalfby";
                public const string LkRecurrenceruleCreatedby = "lk_recurrencerule_createdby";
                public const string LkRecurrenceruleModifiedby = "lk_recurrencerule_modifiedby";
                public const string LkRecurrencerulebaseCreatedonbehalfby = "lk_recurrencerulebase_createdonbehalfby";
                public const string LkRecurrencerulebaseModifiedonbehalfby = "lk_recurrencerulebase_modifiedonbehalfby";
                public const string LkRecurringappointmentmasterCreatedby = "lk_recurringappointmentmaster_createdby";
                public const string LkRecurringappointmentmasterCreatedonbehalfby = "lk_recurringappointmentmaster_createdonbehalfby";
                public const string LkRecurringappointmentmasterModifiedby = "lk_recurringappointmentmaster_modifiedby";
                public const string LkRecurringappointmentmasterModifiedonbehalfby = "lk_recurringappointmentmaster_modifiedonbehalfby";
                public const string LkRecyclebinconfigCreatedby = "lk_recyclebinconfig_createdby";
                public const string LkRecyclebinconfigCreatedonbehalfby = "lk_recyclebinconfig_createdonbehalfby";
                public const string LkRecyclebinconfigModifiedby = "lk_recyclebinconfig_modifiedby";
                public const string LkRecyclebinconfigModifiedonbehalfby = "lk_recyclebinconfig_modifiedonbehalfby";
                public const string LkRelationshiproleCreatedonbehalfby = "lk_relationshiprole_createdonbehalfby";
                public const string LkRelationshiproleModifiedonbehalfby = "lk_relationshiprole_modifiedonbehalfby";
                public const string LkRelationshiprolemapCreatedonbehalfby = "lk_relationshiprolemap_createdonbehalfby";
                public const string LkRelationshiprolemapModifiedonbehalfby = "lk_relationshiprolemap_modifiedonbehalfby";
                public const string LkReportCreatedonbehalfby = "lk_report_createdonbehalfby";
                public const string LkReportModifiedonbehalfby = "lk_report_modifiedonbehalfby";
                public const string LkReportbaseCreatedby = "lk_reportbase_createdby";
                public const string LkReportbaseModifiedby = "lk_reportbase_modifiedby";
                public const string LkReportcategoryCreatedonbehalfby = "lk_reportcategory_createdonbehalfby";
                public const string LkReportcategoryModifiedonbehalfby = "lk_reportcategory_modifiedonbehalfby";
                public const string LkReportcategorybaseCreatedby = "lk_reportcategorybase_createdby";
                public const string LkReportcategorybaseModifiedby = "lk_reportcategorybase_modifiedby";
                public const string LkReportentityCreatedonbehalfby = "lk_reportentity_createdonbehalfby";
                public const string LkReportentityModifiedonbehalfby = "lk_reportentity_modifiedonbehalfby";
                public const string LkReportentitybaseCreatedby = "lk_reportentitybase_createdby";
                public const string LkReportentitybaseModifiedby = "lk_reportentitybase_modifiedby";
                public const string LkReportlinkCreatedonbehalfby = "lk_reportlink_createdonbehalfby";
                public const string LkReportlinkModifiedonbehalfby = "lk_reportlink_modifiedonbehalfby";
                public const string LkReportlinkbaseCreatedby = "lk_reportlinkbase_createdby";
                public const string LkReportlinkbaseModifiedby = "lk_reportlinkbase_modifiedby";
                public const string LkReportparameterCreatedby = "lk_reportparameter_createdby";
                public const string LkReportparameterCreatedonbehalfby = "lk_reportparameter_createdonbehalfby";
                public const string LkReportparameterModifiedby = "lk_reportparameter_modifiedby";
                public const string LkReportparameterModifiedonbehalfby = "lk_reportparameter_modifiedonbehalfby";
                public const string LkReportvisibilityCreatedonbehalfby = "lk_reportvisibility_createdonbehalfby";
                public const string LkReportvisibilityModifiedonbehalfby = "lk_reportvisibility_modifiedonbehalfby";
                public const string LkReportvisibilitybaseCreatedby = "lk_reportvisibilitybase_createdby";
                public const string LkReportvisibilitybaseModifiedby = "lk_reportvisibilitybase_modifiedby";
                public const string LkRetaineddataexcelCreatedby = "lk_retaineddataexcel_createdby";
                public const string LkRetaineddataexcelCreatedonbehalfby = "lk_retaineddataexcel_createdonbehalfby";
                public const string LkRetaineddataexcelModifiedby = "lk_retaineddataexcel_modifiedby";
                public const string LkRetaineddataexcelModifiedonbehalfby = "lk_retaineddataexcel_modifiedonbehalfby";
                public const string LkRetentioncleanupinfoCreatedby = "lk_retentioncleanupinfo_createdby";
                public const string LkRetentioncleanupinfoCreatedonbehalfby = "lk_retentioncleanupinfo_createdonbehalfby";
                public const string LkRetentioncleanupinfoModifiedby = "lk_retentioncleanupinfo_modifiedby";
                public const string LkRetentioncleanupinfoModifiedonbehalfby = "lk_retentioncleanupinfo_modifiedonbehalfby";
                public const string LkRetentioncleanupoperationCreatedby = "lk_retentioncleanupoperation_createdby";
                public const string LkRetentioncleanupoperationCreatedonbehalfby = "lk_retentioncleanupoperation_createdonbehalfby";
                public const string LkRetentioncleanupoperationModifiedby = "lk_retentioncleanupoperation_modifiedby";
                public const string LkRetentioncleanupoperationModifiedonbehalfby = "lk_retentioncleanupoperation_modifiedonbehalfby";
                public const string LkRetentionconfigCreatedby = "lk_retentionconfig_createdby";
                public const string LkRetentionconfigCreatedonbehalfby = "lk_retentionconfig_createdonbehalfby";
                public const string LkRetentionconfigModifiedby = "lk_retentionconfig_modifiedby";
                public const string LkRetentionconfigModifiedonbehalfby = "lk_retentionconfig_modifiedonbehalfby";
                public const string LkRetentionfailuredetailCreatedby = "lk_retentionfailuredetail_createdby";
                public const string LkRetentionfailuredetailCreatedonbehalfby = "lk_retentionfailuredetail_createdonbehalfby";
                public const string LkRetentionfailuredetailModifiedby = "lk_retentionfailuredetail_modifiedby";
                public const string LkRetentionfailuredetailModifiedonbehalfby = "lk_retentionfailuredetail_modifiedonbehalfby";
                public const string LkRetentionoperationCreatedby = "lk_retentionoperation_createdby";
                public const string LkRetentionoperationCreatedonbehalfby = "lk_retentionoperation_createdonbehalfby";
                public const string LkRetentionoperationModifiedby = "lk_retentionoperation_modifiedby";
                public const string LkRetentionoperationModifiedonbehalfby = "lk_retentionoperation_modifiedonbehalfby";
                public const string LkRetentionoperationdetailCreatedby = "lk_retentionoperationdetail_createdby";
                public const string LkRetentionoperationdetailCreatedonbehalfby = "lk_retentionoperationdetail_createdonbehalfby";
                public const string LkRetentionoperationdetailModifiedby = "lk_retentionoperationdetail_modifiedby";
                public const string LkRetentionoperationdetailModifiedonbehalfby = "lk_retentionoperationdetail_modifiedonbehalfby";
                public const string LkRetentionsuccessdetailCreatedby = "lk_retentionsuccessdetail_createdby";
                public const string LkRetentionsuccessdetailCreatedonbehalfby = "lk_retentionsuccessdetail_createdonbehalfby";
                public const string LkRetentionsuccessdetailModifiedby = "lk_retentionsuccessdetail_modifiedby";
                public const string LkRetentionsuccessdetailModifiedonbehalfby = "lk_retentionsuccessdetail_modifiedonbehalfby";
                public const string LkRevokeinheritedaccessrecordstrackerCreatedby = "lk_revokeinheritedaccessrecordstracker_createdby";
                public const string LkRevokeinheritedaccessrecordstrackerCreatedonbehalfby = "lk_revokeinheritedaccessrecordstracker_createdonbehalfby";
                public const string LkRevokeinheritedaccessrecordstrackerModifiedby = "lk_revokeinheritedaccessrecordstracker_modifiedby";
                public const string LkRevokeinheritedaccessrecordstrackerModifiedonbehalfby = "lk_revokeinheritedaccessrecordstracker_modifiedonbehalfby";
                public const string LkRibboncommandCreatedby = "lk_ribboncommand_createdby";
                public const string LkRibboncommandCreatedonbehalfby = "lk_ribboncommand_createdonbehalfby";
                public const string LkRibboncommandModifiedby = "lk_ribboncommand_modifiedby";
                public const string LkRibboncommandModifiedonbehalfby = "lk_ribboncommand_modifiedonbehalfby";
                public const string LkRibbonruleCreatedby = "lk_ribbonrule_createdby";
                public const string LkRibbonruleCreatedonbehalfby = "lk_ribbonrule_createdonbehalfby";
                public const string LkRibbonruleModifiedby = "lk_ribbonrule_modifiedby";
                public const string LkRibbonruleModifiedonbehalfby = "lk_ribbonrule_modifiedonbehalfby";
                public const string LkRoleCreatedonbehalfby = "lk_role_createdonbehalfby";
                public const string LkRoleModifiedonbehalfby = "lk_role_modifiedonbehalfby";
                public const string LkRolebaseCreatedby = "lk_rolebase_createdby";
                public const string LkRolebaseModifiedby = "lk_rolebase_modifiedby";
                public const string LkRollupfieldCreatedby = "lk_rollupfield_createdby";
                public const string LkRollupfieldCreatedonbehalfby = "lk_rollupfield_createdonbehalfby";
                public const string LkRollupfieldModifiedby = "lk_rollupfield_modifiedby";
                public const string LkRollupfieldModifiedonbehalfby = "lk_rollupfield_modifiedonbehalfby";
                public const string LkRoutingruleCreatedby = "lk_routingrule_createdby";
                public const string LkRoutingruleCreatedonbehalfby = "lk_routingrule_createdonbehalfby";
                public const string LkRoutingruleModifiedby = "lk_routingrule_modifiedby";
                public const string LkRoutingruleModifiedonbehalfby = "lk_routingrule_modifiedonbehalfby";
                public const string LkRoutingRuleItemCreatedby = "lk_RoutingRuleItem_createdby";
                public const string LkRoutingruleitemCreatedonbehalfby = "lk_routingruleitem_createdonbehalfby";
                public const string LkRoutingruleitemModifiedby = "lk_routingruleitem_modifiedby";
                public const string LkRoutingruleitemModifiedonbehalfby = "lk_routingruleitem_modifiedonbehalfby";
                public const string LkSaSuggestedactionCreatedby = "lk_sa_suggestedaction_createdby";
                public const string LkSaSuggestedactionCreatedonbehalfby = "lk_sa_suggestedaction_createdonbehalfby";
                public const string LkSaSuggestedactionModifiedby = "lk_sa_suggestedaction_modifiedby";
                public const string LkSaSuggestedactionModifiedonbehalfby = "lk_sa_suggestedaction_modifiedonbehalfby";
                public const string LkSaSuggestedactioncriteriaCreatedby = "lk_sa_suggestedactioncriteria_createdby";
                public const string LkSaSuggestedactioncriteriaCreatedonbehalfby = "lk_sa_suggestedactioncriteria_createdonbehalfby";
                public const string LkSaSuggestedactioncriteriaModifiedby = "lk_sa_suggestedactioncriteria_modifiedby";
                public const string LkSaSuggestedactioncriteriaModifiedonbehalfby = "lk_sa_suggestedactioncriteria_modifiedonbehalfby";
                public const string LkSavedorginsightsconfigurationCreatedby = "lk_savedorginsightsconfiguration_createdby";
                public const string LkSavedorginsightsconfigurationCreatedonbehalfby = "lk_savedorginsightsconfiguration_createdonbehalfby";
                public const string LkSavedorginsightsconfigurationModifiedby = "lk_savedorginsightsconfiguration_modifiedby";
                public const string LkSavedorginsightsconfigurationModifiedonbehalfby = "lk_savedorginsightsconfiguration_modifiedonbehalfby";
                public const string LkSavedqueryCreatedonbehalfby = "lk_savedquery_createdonbehalfby";
                public const string LkSavedqueryModifiedonbehalfby = "lk_savedquery_modifiedonbehalfby";
                public const string LkSavedquerybaseCreatedby = "lk_savedquerybase_createdby";
                public const string LkSavedquerybaseModifiedby = "lk_savedquerybase_modifiedby";
                public const string LkSavedqueryvisualizationbaseCreatedby = "lk_savedqueryvisualizationbase_createdby";
                public const string LkSavedqueryvisualizationbaseCreatedonbehalfby = "lk_savedqueryvisualizationbase_createdonbehalfby";
                public const string LkSavedqueryvisualizationbaseModifiedby = "lk_savedqueryvisualizationbase_modifiedby";
                public const string LkSavedqueryvisualizationbaseModifiedonbehalfby = "lk_savedqueryvisualizationbase_modifiedonbehalfby";
                public const string LkSavingruleCreatedby = "lk_savingrule_createdby";
                public const string LkSavingruleCreatedonbehalfby = "lk_savingrule_createdonbehalfby";
                public const string LkSavingruleModifiedby = "lk_savingrule_modifiedby";
                public const string LkSavingruleModifiedonbehalfby = "lk_savingrule_modifiedonbehalfby";
                public const string LkSdkmessageCreatedonbehalfby = "lk_sdkmessage_createdonbehalfby";
                public const string LkSdkmessageModifiedonbehalfby = "lk_sdkmessage_modifiedonbehalfby";
                public const string LkSdkmessagefilterCreatedonbehalfby = "lk_sdkmessagefilter_createdonbehalfby";
                public const string LkSdkmessagefilterModifiedonbehalfby = "lk_sdkmessagefilter_modifiedonbehalfby";
                public const string LkSdkmessagepairCreatedonbehalfby = "lk_sdkmessagepair_createdonbehalfby";
                public const string LkSdkmessagepairModifiedonbehalfby = "lk_sdkmessagepair_modifiedonbehalfby";
                public const string LkSdkmessageprocessingstepCreatedonbehalfby = "lk_sdkmessageprocessingstep_createdonbehalfby";
                public const string LkSdkmessageprocessingstepModifiedonbehalfby = "lk_sdkmessageprocessingstep_modifiedonbehalfby";
                public const string LkSdkmessageprocessingstepimageCreatedonbehalfby = "lk_sdkmessageprocessingstepimage_createdonbehalfby";
                public const string LkSdkmessageprocessingstepimageModifiedonbehalfby = "lk_sdkmessageprocessingstepimage_modifiedonbehalfby";
                public const string LkSdkmessageprocessingstepsecureconfigCreatedonbehalfby = "lk_sdkmessageprocessingstepsecureconfig_createdonbehalfby";
                public const string LkSdkmessageprocessingstepsecureconfigModifiedonbehalfby = "lk_sdkmessageprocessingstepsecureconfig_modifiedonbehalfby";
                public const string LkSdkmessagerequestCreatedonbehalfby = "lk_sdkmessagerequest_createdonbehalfby";
                public const string LkSdkmessagerequestModifiedonbehalfby = "lk_sdkmessagerequest_modifiedonbehalfby";
                public const string LkSdkmessagerequestfieldCreatedonbehalfby = "lk_sdkmessagerequestfield_createdonbehalfby";
                public const string LkSdkmessagerequestfieldModifiedonbehalfby = "lk_sdkmessagerequestfield_modifiedonbehalfby";
                public const string LkSdkmessageresponseCreatedonbehalfby = "lk_sdkmessageresponse_createdonbehalfby";
                public const string LkSdkmessageresponseModifiedonbehalfby = "lk_sdkmessageresponse_modifiedonbehalfby";
                public const string LkSdkmessageresponsefieldCreatedonbehalfby = "lk_sdkmessageresponsefield_createdonbehalfby";
                public const string LkSdkmessageresponsefieldModifiedonbehalfby = "lk_sdkmessageresponsefield_modifiedonbehalfby";
                public const string LkSearchattributesettingsCreatedby = "lk_searchattributesettings_createdby";
                public const string LkSearchattributesettingsCreatedonbehalfby = "lk_searchattributesettings_createdonbehalfby";
                public const string LkSearchattributesettingsModifiedby = "lk_searchattributesettings_modifiedby";
                public const string LkSearchattributesettingsModifiedonbehalfby = "lk_searchattributesettings_modifiedonbehalfby";
                public const string LkSearchcustomanalyzerCreatedby = "lk_searchcustomanalyzer_createdby";
                public const string LkSearchcustomanalyzerCreatedonbehalfby = "lk_searchcustomanalyzer_createdonbehalfby";
                public const string LkSearchcustomanalyzerModifiedby = "lk_searchcustomanalyzer_modifiedby";
                public const string LkSearchcustomanalyzerModifiedonbehalfby = "lk_searchcustomanalyzer_modifiedonbehalfby";
                public const string LkSearchrelationshipsettingsCreatedby = "lk_searchrelationshipsettings_createdby";
                public const string LkSearchrelationshipsettingsCreatedonbehalfby = "lk_searchrelationshipsettings_createdonbehalfby";
                public const string LkSearchrelationshipsettingsModifiedby = "lk_searchrelationshipsettings_modifiedby";
                public const string LkSearchrelationshipsettingsModifiedonbehalfby = "lk_searchrelationshipsettings_modifiedonbehalfby";
                public const string LkSearchresultscacheCreatedby = "lk_searchresultscache_createdby";
                public const string LkSearchresultscacheCreatedonbehalfby = "lk_searchresultscache_createdonbehalfby";
                public const string LkSearchresultscacheModifiedby = "lk_searchresultscache_modifiedby";
                public const string LkSearchresultscacheModifiedonbehalfby = "lk_searchresultscache_modifiedonbehalfby";
                public const string LkSemiannualfiscalcalendarCreatedby = "lk_semiannualfiscalcalendar_createdby";
                public const string LkSemiannualfiscalcalendarCreatedonbehalfby = "lk_semiannualfiscalcalendar_createdonbehalfby";
                public const string LkSemiannualfiscalcalendarModifiedby = "lk_semiannualfiscalcalendar_modifiedby";
                public const string LkSemiannualfiscalcalendarModifiedonbehalfby = "lk_semiannualfiscalcalendar_modifiedonbehalfby";
                public const string LkSemiannualfiscalcalendarSalespersonid = "lk_semiannualfiscalcalendar_salespersonid";
                public const string LkSensitivitylabelattributemappingCreatedby = "lk_sensitivitylabelattributemapping_createdby";
                public const string LkSensitivitylabelattributemappingCreatedonbehalfby = "lk_sensitivitylabelattributemapping_createdonbehalfby";
                public const string LkSensitivitylabelattributemappingModifiedby = "lk_sensitivitylabelattributemapping_modifiedby";
                public const string LkSensitivitylabelattributemappingModifiedonbehalfby = "lk_sensitivitylabelattributemapping_modifiedonbehalfby";
                public const string LkServiceendpointbaseCreatedonbehalfby = "lk_serviceendpointbase_createdonbehalfby";
                public const string LkServiceendpointbaseModifiedonbehalfby = "lk_serviceendpointbase_modifiedonbehalfby";
                public const string LkServiceplanCreatedby = "lk_serviceplan_createdby";
                public const string LkServiceplanCreatedonbehalfby = "lk_serviceplan_createdonbehalfby";
                public const string LkServiceplanModifiedby = "lk_serviceplan_modifiedby";
                public const string LkServiceplanModifiedonbehalfby = "lk_serviceplan_modifiedonbehalfby";
                public const string LkServiceplancustomcontrolCreatedby = "lk_serviceplancustomcontrol_createdby";
                public const string LkServiceplancustomcontrolCreatedonbehalfby = "lk_serviceplancustomcontrol_createdonbehalfby";
                public const string LkServiceplancustomcontrolModifiedby = "lk_serviceplancustomcontrol_modifiedby";
                public const string LkServiceplancustomcontrolModifiedonbehalfby = "lk_serviceplancustomcontrol_modifiedonbehalfby";
                public const string LkServiceplanmappingCreatedby = "lk_serviceplanmapping_createdby";
                public const string LkServiceplanmappingCreatedonbehalfby = "lk_serviceplanmapping_createdonbehalfby";
                public const string LkServiceplanmappingModifiedby = "lk_serviceplanmapping_modifiedby";
                public const string LkServiceplanmappingModifiedonbehalfby = "lk_serviceplanmapping_modifiedonbehalfby";
                public const string LkSettingdefinitionCreatedby = "lk_settingdefinition_createdby";
                public const string LkSettingdefinitionCreatedonbehalfby = "lk_settingdefinition_createdonbehalfby";
                public const string LkSettingdefinitionModifiedby = "lk_settingdefinition_modifiedby";
                public const string LkSettingdefinitionModifiedonbehalfby = "lk_settingdefinition_modifiedonbehalfby";
                public const string LkSharedlinksettingCreatedby = "lk_sharedlinksetting_createdby";
                public const string LkSharedlinksettingCreatedonbehalfby = "lk_sharedlinksetting_createdonbehalfby";
                public const string LkSharedlinksettingModifiedby = "lk_sharedlinksetting_modifiedby";
                public const string LkSharedlinksettingModifiedonbehalfby = "lk_sharedlinksetting_modifiedonbehalfby";
                public const string LkSharedobjectCreatedby = "lk_sharedobject_createdby";
                public const string LkSharedobjectCreatedonbehalfby = "lk_sharedobject_createdonbehalfby";
                public const string LkSharedobjectModifiedby = "lk_sharedobject_modifiedby";
                public const string LkSharedobjectModifiedonbehalfby = "lk_sharedobject_modifiedonbehalfby";
                public const string LkSharedworkspaceCreatedby = "lk_sharedworkspace_createdby";
                public const string LkSharedworkspaceCreatedonbehalfby = "lk_sharedworkspace_createdonbehalfby";
                public const string LkSharedworkspaceModifiedby = "lk_sharedworkspace_modifiedby";
                public const string LkSharedworkspaceModifiedonbehalfby = "lk_sharedworkspace_modifiedonbehalfby";
                public const string LkSharedworkspaceaccesstoken2Createdby = "lk_sharedworkspaceaccesstoken2_createdby";
                public const string LkSharedworkspaceaccesstoken2Createdonbehalfby = "lk_sharedworkspaceaccesstoken2_createdonbehalfby";
                public const string LkSharedworkspaceaccesstoken2Modifiedby = "lk_sharedworkspaceaccesstoken2_modifiedby";
                public const string LkSharedworkspaceaccesstoken2Modifiedonbehalfby = "lk_sharedworkspaceaccesstoken2_modifiedonbehalfby";
                public const string LkSharedworkspacepoolCreatedby = "lk_sharedworkspacepool_createdby";
                public const string LkSharedworkspacepoolCreatedonbehalfby = "lk_sharedworkspacepool_createdonbehalfby";
                public const string LkSharedworkspacepoolModifiedby = "lk_sharedworkspacepool_modifiedby";
                public const string LkSharedworkspacepoolModifiedonbehalfby = "lk_sharedworkspacepool_modifiedonbehalfby";
                public const string LkSharePointDataCreatedby = "lk_SharePointData_createdby";
                public const string LkSharePointDataCreatedonbehalfby = "lk_SharePointData_createdonbehalfby";
                public const string LkSharePointDataModifiedby = "lk_SharePointData_modifiedby";
                public const string LkSharePointDataModifiedonbehalfby = "lk_SharePointData_modifiedonbehalfby";
                public const string LkSharepointdataUser = "lk_sharepointdata_user";
                public const string LkSharepointdocumentbaseCreatedby = "lk_sharepointdocumentbase_createdby";
                public const string LkSharepointdocumentbaseCreatedonbehalfby = "lk_sharepointdocumentbase_createdonbehalfby";
                public const string LkSharepointdocumentbaseModifiedby = "lk_sharepointdocumentbase_modifiedby";
                public const string LkSharepointdocumentbaseModifiedonbehalfby = "lk_sharepointdocumentbase_modifiedonbehalfby";
                public const string LkSharepointdocumentlocationbaseCreatedby = "lk_sharepointdocumentlocationbase_createdby";
                public const string LkSharepointdocumentlocationbaseCreatedonbehalfby = "lk_sharepointdocumentlocationbase_createdonbehalfby";
                public const string LkSharepointdocumentlocationbaseModifiedby = "lk_sharepointdocumentlocationbase_modifiedby";
                public const string LkSharepointdocumentlocationbaseModifiedonbehalfby = "lk_sharepointdocumentlocationbase_modifiedonbehalfby";
                public const string LkSharepointmanagedidentityCreatedby = "lk_sharepointmanagedidentity_createdby";
                public const string LkSharepointmanagedidentityCreatedonbehalfby = "lk_sharepointmanagedidentity_createdonbehalfby";
                public const string LkSharepointmanagedidentityModifiedby = "lk_sharepointmanagedidentity_modifiedby";
                public const string LkSharepointmanagedidentityModifiedonbehalfby = "lk_sharepointmanagedidentity_modifiedonbehalfby";
                public const string LkSharepointsitebaseCreatedby = "lk_sharepointsitebase_createdby";
                public const string LkSharepointsitebaseCreatedonbehalfby = "lk_sharepointsitebase_createdonbehalfby";
                public const string LkSharepointsitebaseModifiedby = "lk_sharepointsitebase_modifiedby";
                public const string LkSharepointsitebaseModifiedonbehalfby = "lk_sharepointsitebase_modifiedonbehalfby";
                public const string LkSideloadedaipluginCreatedby = "lk_sideloadedaiplugin_createdby";
                public const string LkSideloadedaipluginCreatedonbehalfby = "lk_sideloadedaiplugin_createdonbehalfby";
                public const string LkSideloadedaipluginModifiedby = "lk_sideloadedaiplugin_modifiedby";
                public const string LkSideloadedaipluginModifiedonbehalfby = "lk_sideloadedaiplugin_modifiedonbehalfby";
                public const string LkSignalCreatedby = "lk_signal_createdby";
                public const string LkSignalCreatedonbehalfby = "lk_signal_createdonbehalfby";
                public const string LkSignalModifiedby = "lk_signal_modifiedby";
                public const string LkSignalModifiedonbehalfby = "lk_signal_modifiedonbehalfby";
                public const string LkSignalregistrationCreatedby = "lk_signalregistration_createdby";
                public const string LkSignalregistrationCreatedonbehalfby = "lk_signalregistration_createdonbehalfby";
                public const string LkSignalregistrationModifiedby = "lk_signalregistration_modifiedby";
                public const string LkSignalregistrationModifiedonbehalfby = "lk_signalregistration_modifiedonbehalfby";
                public const string LkSimilarityruleCreatedonbehalfby = "lk_similarityrule_createdonbehalfby";
                public const string LkSimilarityruleModifiedonbehalfby = "lk_similarityrule_modifiedonbehalfby";
                public const string LkSiteMapCreatedby = "lk_SiteMap_createdby";
                public const string LkSiteMapCreatedonbehalfby = "lk_SiteMap_createdonbehalfby";
                public const string LkSiteMapModifiedby = "lk_SiteMap_modifiedby";
                public const string LkSiteMapModifiedonbehalfby = "lk_SiteMap_modifiedonbehalfby";
                public const string LkSkillCreatedby = "lk_skill_createdby";
                public const string LkSkillCreatedonbehalfby = "lk_skill_createdonbehalfby";
                public const string LkSkillModifiedby = "lk_skill_modifiedby";
                public const string LkSkillModifiedonbehalfby = "lk_skill_modifiedonbehalfby";
                public const string LkSkillresourceCreatedby = "lk_skillresource_createdby";
                public const string LkSkillresourceCreatedonbehalfby = "lk_skillresource_createdonbehalfby";
                public const string LkSkillresourceModifiedby = "lk_skillresource_modifiedby";
                public const string LkSkillresourceModifiedonbehalfby = "lk_skillresource_modifiedonbehalfby";
                public const string LkSlabaseCreatedby = "lk_slabase_createdby";
                public const string LkSlabaseCreatedonbehalfby = "lk_slabase_createdonbehalfby";
                public const string LkSlabaseModifiedby = "lk_slabase_modifiedby";
                public const string LkSlabaseModifiedonbehalfby = "lk_slabase_modifiedonbehalfby";
                public const string LkSlaitembaseCreatedby = "lk_slaitembase_createdby";
                public const string LkSlaitembaseCreatedonbehalfby = "lk_slaitembase_createdonbehalfby";
                public const string LkSlaitembaseModifiedby = "lk_slaitembase_modifiedby";
                public const string LkSlaitembaseModifiedonbehalfby = "lk_slaitembase_modifiedonbehalfby";
                public const string LkSlakpiinstancebaseCreatedby = "lk_slakpiinstancebase_createdby";
                public const string LkSlakpiinstancebaseCreatedonbehalfby = "lk_slakpiinstancebase_createdonbehalfby";
                public const string LkSlakpiinstancebaseModifiedby = "lk_slakpiinstancebase_modifiedby";
                public const string LkSlakpiinstancebaseModifiedonbehalfby = "lk_slakpiinstancebase_modifiedonbehalfby";
                public const string LkSocialactivityCreatedby = "lk_socialactivity_createdby";
                public const string LkSocialactivityModifiedby = "lk_socialactivity_modifiedby";
                public const string LkSocialactivitybaseCreatedonbehalfby = "lk_socialactivitybase_createdonbehalfby";
                public const string LkSocialactivitybaseModifiedonbehalfby = "lk_socialactivitybase_modifiedonbehalfby";
                public const string LkSocialinsightsconfigurationCreatedby = "lk_socialinsightsconfiguration_createdby";
                public const string LkSocialinsightsconfigurationCreatedonbehalfby = "lk_socialinsightsconfiguration_createdonbehalfby";
                public const string LkSocialinsightsconfigurationModifiedby = "lk_socialinsightsconfiguration_modifiedby";
                public const string LkSocialinsightsconfigurationModifiedonbehalfby = "lk_socialinsightsconfiguration_modifiedonbehalfby";
                public const string LkSocialProfileCreatedonbehalfby = "lk_SocialProfile_createdonbehalfby";
                public const string LkSocialProfileModifiedonbehalfby = "lk_SocialProfile_modifiedonbehalfby";
                public const string LkSolutionCreatedby = "lk_solution_createdby";
                public const string LkSolutionModifiedby = "lk_solution_modifiedby";
                public const string LkSolutionbaseCreatedonbehalfby = "lk_solutionbase_createdonbehalfby";
                public const string LkSolutionbaseModifiedonbehalfby = "lk_solutionbase_modifiedonbehalfby";
                public const string LkSolutioncomponentattributeconfigurationCreatedby = "lk_solutioncomponentattributeconfiguration_createdby";
                public const string LkSolutioncomponentattributeconfigurationCreatedonbehalfby = "lk_solutioncomponentattributeconfiguration_createdonbehalfby";
                public const string LkSolutioncomponentattributeconfigurationModifiedby = "lk_solutioncomponentattributeconfiguration_modifiedby";
                public const string LkSolutioncomponentattributeconfigurationModifiedonbehalfby = "lk_solutioncomponentattributeconfiguration_modifiedonbehalfby";
                public const string LkSolutioncomponentbaseCreatedonbehalfby = "lk_solutioncomponentbase_createdonbehalfby";
                public const string LkSolutioncomponentbaseModifiedonbehalfby = "lk_solutioncomponentbase_modifiedonbehalfby";
                public const string LkSolutioncomponentbatchconfigurationCreatedby = "lk_solutioncomponentbatchconfiguration_createdby";
                public const string LkSolutioncomponentbatchconfigurationCreatedonbehalfby = "lk_solutioncomponentbatchconfiguration_createdonbehalfby";
                public const string LkSolutioncomponentbatchconfigurationModifiedby = "lk_solutioncomponentbatchconfiguration_modifiedby";
                public const string LkSolutioncomponentbatchconfigurationModifiedonbehalfby = "lk_solutioncomponentbatchconfiguration_modifiedonbehalfby";
                public const string LkSolutioncomponentconfigurationCreatedby = "lk_solutioncomponentconfiguration_createdby";
                public const string LkSolutioncomponentconfigurationCreatedonbehalfby = "lk_solutioncomponentconfiguration_createdonbehalfby";
                public const string LkSolutioncomponentconfigurationModifiedby = "lk_solutioncomponentconfiguration_modifiedby";
                public const string LkSolutioncomponentconfigurationModifiedonbehalfby = "lk_solutioncomponentconfiguration_modifiedonbehalfby";
                public const string LkSolutioncomponentrelationshipconfigurationCreatedby = "lk_solutioncomponentrelationshipconfiguration_createdby";
                public const string LkSolutioncomponentrelationshipconfigurationCreatedonbehalfby = "lk_solutioncomponentrelationshipconfiguration_createdonbehalfby";
                public const string LkSolutioncomponentrelationshipconfigurationModifiedby = "lk_solutioncomponentrelationshipconfiguration_modifiedby";
                public const string LkSolutioncomponentrelationshipconfigurationModifiedonbehalfby = "lk_solutioncomponentrelationshipconfiguration_modifiedonbehalfby";
                public const string LkSourcecontrolbranchconfigurationCreatedby = "lk_sourcecontrolbranchconfiguration_createdby";
                public const string LkSourcecontrolbranchconfigurationCreatedonbehalfby = "lk_sourcecontrolbranchconfiguration_createdonbehalfby";
                public const string LkSourcecontrolbranchconfigurationModifiedby = "lk_sourcecontrolbranchconfiguration_modifiedby";
                public const string LkSourcecontrolbranchconfigurationModifiedonbehalfby = "lk_sourcecontrolbranchconfiguration_modifiedonbehalfby";
                public const string LkSourcecontrolcomponentCreatedby = "lk_sourcecontrolcomponent_createdby";
                public const string LkSourcecontrolcomponentCreatedonbehalfby = "lk_sourcecontrolcomponent_createdonbehalfby";
                public const string LkSourcecontrolcomponentModifiedby = "lk_sourcecontrolcomponent_modifiedby";
                public const string LkSourcecontrolcomponentModifiedonbehalfby = "lk_sourcecontrolcomponent_modifiedonbehalfby";
                public const string LkSourcecontrolcomponentpayloadCreatedby = "lk_sourcecontrolcomponentpayload_createdby";
                public const string LkSourcecontrolcomponentpayloadCreatedonbehalfby = "lk_sourcecontrolcomponentpayload_createdonbehalfby";
                public const string LkSourcecontrolcomponentpayloadModifiedby = "lk_sourcecontrolcomponentpayload_modifiedby";
                public const string LkSourcecontrolcomponentpayloadModifiedonbehalfby = "lk_sourcecontrolcomponentpayload_modifiedonbehalfby";
                public const string LkSourcecontrolconfigurationCreatedby = "lk_sourcecontrolconfiguration_createdby";
                public const string LkSourcecontrolconfigurationCreatedonbehalfby = "lk_sourcecontrolconfiguration_createdonbehalfby";
                public const string LkSourcecontrolconfigurationModifiedby = "lk_sourcecontrolconfiguration_modifiedby";
                public const string LkSourcecontrolconfigurationModifiedonbehalfby = "lk_sourcecontrolconfiguration_modifiedonbehalfby";
                public const string LkSourcecontroloperationstatusCreatedby = "lk_sourcecontroloperationstatus_createdby";
                public const string LkSourcecontroloperationstatusCreatedonbehalfby = "lk_sourcecontroloperationstatus_createdonbehalfby";
                public const string LkSourcecontroloperationstatusModifiedby = "lk_sourcecontroloperationstatus_modifiedby";
                public const string LkSourcecontroloperationstatusModifiedonbehalfby = "lk_sourcecontroloperationstatus_modifiedonbehalfby";
                public const string LkStagedattributelookupvalueCreatedby = "lk_stagedattributelookupvalue_createdby";
                public const string LkStagedattributelookupvalueCreatedonbehalfby = "lk_stagedattributelookupvalue_createdonbehalfby";
                public const string LkStagedattributelookupvalueModifiedby = "lk_stagedattributelookupvalue_modifiedby";
                public const string LkStagedattributelookupvalueModifiedonbehalfby = "lk_stagedattributelookupvalue_modifiedonbehalfby";
                public const string LkStagedattributepicklistvalueCreatedby = "lk_stagedattributepicklistvalue_createdby";
                public const string LkStagedattributepicklistvalueCreatedonbehalfby = "lk_stagedattributepicklistvalue_createdonbehalfby";
                public const string LkStagedattributepicklistvalueModifiedby = "lk_stagedattributepicklistvalue_modifiedby";
                public const string LkStagedattributepicklistvalueModifiedonbehalfby = "lk_stagedattributepicklistvalue_modifiedonbehalfby";
                public const string LkStagedentityCreatedby = "lk_stagedentity_createdby";
                public const string LkStagedentityCreatedonbehalfby = "lk_stagedentity_createdonbehalfby";
                public const string LkStagedentityModifiedby = "lk_stagedentity_modifiedby";
                public const string LkStagedentityModifiedonbehalfby = "lk_stagedentity_modifiedonbehalfby";
                public const string LkStagedentityattributeCreatedby = "lk_stagedentityattribute_createdby";
                public const string LkStagedentityattributeCreatedonbehalfby = "lk_stagedentityattribute_createdonbehalfby";
                public const string LkStagedentityattributeModifiedby = "lk_stagedentityattribute_modifiedby";
                public const string LkStagedentityattributeModifiedonbehalfby = "lk_stagedentityattribute_modifiedonbehalfby";
                public const string LkStagedentityrelationshipCreatedby = "lk_stagedentityrelationship_createdby";
                public const string LkStagedentityrelationshipCreatedonbehalfby = "lk_stagedentityrelationship_createdonbehalfby";
                public const string LkStagedentityrelationshipModifiedby = "lk_stagedentityrelationship_modifiedby";
                public const string LkStagedentityrelationshipModifiedonbehalfby = "lk_stagedentityrelationship_modifiedonbehalfby";
                public const string LkStagedentityrelationshiprelationshipsCreatedby = "lk_stagedentityrelationshiprelationships_createdby";
                public const string LkStagedentityrelationshiprelationshipsCreatedonbehalfby = "lk_stagedentityrelationshiprelationships_createdonbehalfby";
                public const string LkStagedentityrelationshiprelationshipsModifiedby = "lk_stagedentityrelationshiprelationships_modifiedby";
                public const string LkStagedentityrelationshiprelationshipsModifiedonbehalfby = "lk_stagedentityrelationshiprelationships_modifiedonbehalfby";
                public const string LkStagedentityrelationshiproleCreatedby = "lk_stagedentityrelationshiprole_createdby";
                public const string LkStagedentityrelationshiproleCreatedonbehalfby = "lk_stagedentityrelationshiprole_createdonbehalfby";
                public const string LkStagedentityrelationshiproleModifiedby = "lk_stagedentityrelationshiprole_modifiedby";
                public const string LkStagedentityrelationshiproleModifiedonbehalfby = "lk_stagedentityrelationshiprole_modifiedonbehalfby";
                public const string LkStagedmetadataasyncoperationCreatedby = "lk_stagedmetadataasyncoperation_createdby";
                public const string LkStagedmetadataasyncoperationCreatedonbehalfby = "lk_stagedmetadataasyncoperation_createdonbehalfby";
                public const string LkStagedmetadataasyncoperationModifiedby = "lk_stagedmetadataasyncoperation_modifiedby";
                public const string LkStagedmetadataasyncoperationModifiedonbehalfby = "lk_stagedmetadataasyncoperation_modifiedonbehalfby";
                public const string LkStagedoptionsetCreatedby = "lk_stagedoptionset_createdby";
                public const string LkStagedoptionsetCreatedonbehalfby = "lk_stagedoptionset_createdonbehalfby";
                public const string LkStagedoptionsetModifiedby = "lk_stagedoptionset_modifiedby";
                public const string LkStagedoptionsetModifiedonbehalfby = "lk_stagedoptionset_modifiedonbehalfby";
                public const string LkStagedrelationshipCreatedby = "lk_stagedrelationship_createdby";
                public const string LkStagedrelationshipCreatedonbehalfby = "lk_stagedrelationship_createdonbehalfby";
                public const string LkStagedrelationshipModifiedby = "lk_stagedrelationship_modifiedby";
                public const string LkStagedrelationshipModifiedonbehalfby = "lk_stagedrelationship_modifiedonbehalfby";
                public const string LkStagedrelationshipextraconditionCreatedby = "lk_stagedrelationshipextracondition_createdby";
                public const string LkStagedrelationshipextraconditionCreatedonbehalfby = "lk_stagedrelationshipextracondition_createdonbehalfby";
                public const string LkStagedrelationshipextraconditionModifiedby = "lk_stagedrelationshipextracondition_modifiedby";
                public const string LkStagedrelationshipextraconditionModifiedonbehalfby = "lk_stagedrelationshipextracondition_modifiedonbehalfby";
                public const string LkStagedsourcecontrolcomponentCreatedby = "lk_stagedsourcecontrolcomponent_createdby";
                public const string LkStagedsourcecontrolcomponentCreatedonbehalfby = "lk_stagedsourcecontrolcomponent_createdonbehalfby";
                public const string LkStagedsourcecontrolcomponentModifiedby = "lk_stagedsourcecontrolcomponent_modifiedby";
                public const string LkStagedsourcecontrolcomponentModifiedonbehalfby = "lk_stagedsourcecontrolcomponent_modifiedonbehalfby";
                public const string LkStagedviewattributeCreatedby = "lk_stagedviewattribute_createdby";
                public const string LkStagedviewattributeCreatedonbehalfby = "lk_stagedviewattribute_createdonbehalfby";
                public const string LkStagedviewattributeModifiedby = "lk_stagedviewattribute_modifiedby";
                public const string LkStagedviewattributeModifiedonbehalfby = "lk_stagedviewattribute_modifiedonbehalfby";
                public const string LkStagesolutionuploadCreatedby = "lk_stagesolutionupload_createdby";
                public const string LkStagesolutionuploadCreatedonbehalfby = "lk_stagesolutionupload_createdonbehalfby";
                public const string LkStagesolutionuploadModifiedby = "lk_stagesolutionupload_modifiedby";
                public const string LkStagesolutionuploadModifiedonbehalfby = "lk_stagesolutionupload_modifiedonbehalfby";
                public const string LkSubjectCreatedonbehalfby = "lk_subject_createdonbehalfby";
                public const string LkSubjectModifiedonbehalfby = "lk_subject_modifiedonbehalfby";
                public const string LkSubjectbaseCreatedby = "lk_subjectbase_createdby";
                public const string LkSubjectbaseModifiedby = "lk_subjectbase_modifiedby";
                public const string LkSuggestioncardtemplateCreatedby = "lk_suggestioncardtemplate_createdby";
                public const string LkSuggestioncardtemplateCreatedonbehalfby = "lk_suggestioncardtemplate_createdonbehalfby";
                public const string LkSuggestioncardtemplateModifiedby = "lk_suggestioncardtemplate_modifiedby";
                public const string LkSuggestioncardtemplateModifiedonbehalfby = "lk_suggestioncardtemplate_modifiedonbehalfby";
                public const string LkSupportusertableCreatedby = "lk_supportusertable_createdby";
                public const string LkSupportusertableCreatedonbehalfby = "lk_supportusertable_createdonbehalfby";
                public const string LkSupportusertableModifiedby = "lk_supportusertable_modifiedby";
                public const string LkSupportusertableModifiedonbehalfby = "lk_supportusertable_modifiedonbehalfby";
                public const string LkSynapsedatabaseCreatedby = "lk_synapsedatabase_createdby";
                public const string LkSynapsedatabaseCreatedonbehalfby = "lk_synapsedatabase_createdonbehalfby";
                public const string LkSynapsedatabaseModifiedby = "lk_synapsedatabase_modifiedby";
                public const string LkSynapsedatabaseModifiedonbehalfby = "lk_synapsedatabase_modifiedonbehalfby";
                public const string LkSynapselinkexternaltablestateCreatedby = "lk_synapselinkexternaltablestate_createdby";
                public const string LkSynapselinkexternaltablestateCreatedonbehalfby = "lk_synapselinkexternaltablestate_createdonbehalfby";
                public const string LkSynapselinkexternaltablestateModifiedby = "lk_synapselinkexternaltablestate_modifiedby";
                public const string LkSynapselinkexternaltablestateModifiedonbehalfby = "lk_synapselinkexternaltablestate_modifiedonbehalfby";
                public const string LkSynapselinkprofileCreatedby = "lk_synapselinkprofile_createdby";
                public const string LkSynapselinkprofileCreatedonbehalfby = "lk_synapselinkprofile_createdonbehalfby";
                public const string LkSynapselinkprofileModifiedby = "lk_synapselinkprofile_modifiedby";
                public const string LkSynapselinkprofileModifiedonbehalfby = "lk_synapselinkprofile_modifiedonbehalfby";
                public const string LkSynapselinkprofileentityCreatedby = "lk_synapselinkprofileentity_createdby";
                public const string LkSynapselinkprofileentityCreatedonbehalfby = "lk_synapselinkprofileentity_createdonbehalfby";
                public const string LkSynapselinkprofileentityModifiedby = "lk_synapselinkprofileentity_modifiedby";
                public const string LkSynapselinkprofileentityModifiedonbehalfby = "lk_synapselinkprofileentity_modifiedonbehalfby";
                public const string LkSynapselinkprofileentitystateCreatedby = "lk_synapselinkprofileentitystate_createdby";
                public const string LkSynapselinkprofileentitystateCreatedonbehalfby = "lk_synapselinkprofileentitystate_createdonbehalfby";
                public const string LkSynapselinkprofileentitystateModifiedby = "lk_synapselinkprofileentitystate_modifiedby";
                public const string LkSynapselinkprofileentitystateModifiedonbehalfby = "lk_synapselinkprofileentitystate_modifiedonbehalfby";
                public const string LkSynapselinkscheduleCreatedby = "lk_synapselinkschedule_createdby";
                public const string LkSynapselinkscheduleCreatedonbehalfby = "lk_synapselinkschedule_createdonbehalfby";
                public const string LkSynapselinkscheduleModifiedby = "lk_synapselinkschedule_modifiedby";
                public const string LkSynapselinkscheduleModifiedonbehalfby = "lk_synapselinkschedule_modifiedonbehalfby";
                public const string LkSyncattributemappingprofileCreatedby = "lk_syncattributemappingprofile_createdby";
                public const string LkSyncattributemappingprofileCreatedonbehalfby = "lk_syncattributemappingprofile_createdonbehalfby";
                public const string LkSyncattributemappingprofileModifiedby = "lk_syncattributemappingprofile_modifiedby";
                public const string LkSyncattributemappingprofileModifiedonbehalfby = "lk_syncattributemappingprofile_modifiedonbehalfby";
                public const string LkSyncerrorbaseCreatedby = "lk_syncerrorbase_createdby";
                public const string LkSyncerrorbaseCreatedonbehalfby = "lk_syncerrorbase_createdonbehalfby";
                public const string LkSyncerrorbaseModifiedby = "lk_syncerrorbase_modifiedby";
                public const string LkSyncerrorbaseModifiedonbehalfby = "lk_syncerrorbase_modifiedonbehalfby";
                public const string LkSystemapplicationmetadataCreatedby = "lk_systemapplicationmetadata_createdby";
                public const string LkSystemapplicationmetadataCreatedonbehalfby = "lk_systemapplicationmetadata_createdonbehalfby";
                public const string LkSystemapplicationmetadataModifiedby = "lk_systemapplicationmetadata_modifiedby";
                public const string LkSystemapplicationmetadataModifiedonbehalfby = "lk_systemapplicationmetadata_modifiedonbehalfby";
                public const string LkSystemuserCreatedonbehalfby = "lk_systemuser_createdonbehalfby";
                public const string LkSystemuserModifiedonbehalfby = "lk_systemuser_modifiedonbehalfby";
                public const string LkSystemuserbaseCreatedby = "lk_systemuserbase_createdby";
                public const string LkSystemuserbaseModifiedby = "lk_systemuserbase_modifiedby";
                public const string LkTagCreatedby = "lk_tag_createdby";
                public const string LkTagCreatedonbehalfby = "lk_tag_createdonbehalfby";
                public const string LkTagModifiedby = "lk_tag_modifiedby";
                public const string LkTagModifiedonbehalfby = "lk_tag_modifiedonbehalfby";
                public const string LkTaggedflowsessionCreatedby = "lk_taggedflowsession_createdby";
                public const string LkTaggedflowsessionCreatedonbehalfby = "lk_taggedflowsession_createdonbehalfby";
                public const string LkTaggedflowsessionModifiedby = "lk_taggedflowsession_modifiedby";
                public const string LkTaggedflowsessionModifiedonbehalfby = "lk_taggedflowsession_modifiedonbehalfby";
                public const string LkTaggedprocessCreatedby = "lk_taggedprocess_createdby";
                public const string LkTaggedprocessCreatedonbehalfby = "lk_taggedprocess_createdonbehalfby";
                public const string LkTaggedprocessModifiedby = "lk_taggedprocess_modifiedby";
                public const string LkTaggedprocessModifiedonbehalfby = "lk_taggedprocess_modifiedonbehalfby";
                public const string LkTaskCreatedby = "lk_task_createdby";
                public const string LkTaskCreatedonbehalfby = "lk_task_createdonbehalfby";
                public const string LkTaskModifiedby = "lk_task_modifiedby";
                public const string LkTaskModifiedonbehalfby = "lk_task_modifiedonbehalfby";
                public const string LkTdsmetadataCreatedby = "lk_tdsmetadata_createdby";
                public const string LkTdsmetadataCreatedonbehalfby = "lk_tdsmetadata_createdonbehalfby";
                public const string LkTdsmetadataModifiedby = "lk_tdsmetadata_modifiedby";
                public const string LkTdsmetadataModifiedonbehalfby = "lk_tdsmetadata_modifiedonbehalfby";
                public const string LkTeamCreatedonbehalfby = "lk_team_createdonbehalfby";
                public const string LkTeamModifiedonbehalfby = "lk_team_modifiedonbehalfby";
                public const string LkTeambaseAdministratorid = "lk_teambase_administratorid";
                public const string LkTeambaseCreatedby = "lk_teambase_createdby";
                public const string LkTeambaseModifiedby = "lk_teambase_modifiedby";
                public const string LkTeammobileofflineprofilemembershipCreatedby = "lk_teammobileofflineprofilemembership_createdby";
                public const string LkTeammobileofflineprofilemembershipCreatedonbehalfby = "lk_teammobileofflineprofilemembership_createdonbehalfby";
                public const string LkTeammobileofflineprofilemembershipModifiedby = "lk_teammobileofflineprofilemembership_modifiedby";
                public const string LkTeammobileofflineprofilemembershipModifiedonbehalfby = "lk_teammobileofflineprofilemembership_modifiedonbehalfby";
                public const string LkTeamtemplateCreatedby = "lk_teamtemplate_createdby";
                public const string LkTeamtemplateCreatedonbehalfby = "lk_teamtemplate_createdonbehalfby";
                public const string LkTeamtemplateModifiedby = "lk_teamtemplate_modifiedby";
                public const string LkTeamtemplateModifiedonbehalfby = "lk_teamtemplate_modifiedonbehalfby";
                public const string LkTemplatebaseCreatedby = "lk_templatebase_createdby";
                public const string LkTemplatebaseCreatedonbehalfby = "lk_templatebase_createdonbehalfby";
                public const string LkTemplatebaseModifiedby = "lk_templatebase_modifiedby";
                public const string LkTemplatebaseModifiedonbehalfby = "lk_templatebase_modifiedonbehalfby";
                public const string LkTerritoryCreatedonbehalfby = "lk_territory_createdonbehalfby";
                public const string LkTerritoryModifiedonbehalfby = "lk_territory_modifiedonbehalfby";
                public const string LkTerritorybaseCreatedby = "lk_territorybase_createdby";
                public const string LkTerritorybaseModifiedby = "lk_territorybase_modifiedby";
                public const string LkTextdatarecordsindexingstatusCreatedby = "lk_textdatarecordsindexingstatus_createdby";
                public const string LkTextdatarecordsindexingstatusCreatedonbehalfby = "lk_textdatarecordsindexingstatus_createdonbehalfby";
                public const string LkTextdatarecordsindexingstatusModifiedby = "lk_textdatarecordsindexingstatus_modifiedby";
                public const string LkTextdatarecordsindexingstatusModifiedonbehalfby = "lk_textdatarecordsindexingstatus_modifiedonbehalfby";
                public const string LkThemeCreatedby = "lk_theme_createdby";
                public const string LkThemeCreatedonbehalfby = "lk_theme_createdonbehalfby";
                public const string LkThemeModifiedby = "lk_theme_modifiedby";
                public const string LkThemeModifiedonbehalfby = "lk_theme_modifiedonbehalfby";
                public const string LkTimezonedefinitionCreatedby = "lk_timezonedefinition_createdby";
                public const string LkTimezonedefinitionCreatedonbehalfby = "lk_timezonedefinition_createdonbehalfby";
                public const string LkTimezonedefinitionModifiedby = "lk_timezonedefinition_modifiedby";
                public const string LkTimezonedefinitionModifiedonbehalfby = "lk_timezonedefinition_modifiedonbehalfby";
                public const string LkTimezonelocalizednameCreatedby = "lk_timezonelocalizedname_createdby";
                public const string LkTimezonelocalizednameCreatedonbehalfby = "lk_timezonelocalizedname_createdonbehalfby";
                public const string LkTimezonelocalizednameModifiedby = "lk_timezonelocalizedname_modifiedby";
                public const string LkTimezonelocalizednameModifiedonbehalfby = "lk_timezonelocalizedname_modifiedonbehalfby";
                public const string LkTimezoneruleCreatedby = "lk_timezonerule_createdby";
                public const string LkTimezoneruleCreatedonbehalfby = "lk_timezonerule_createdonbehalfby";
                public const string LkTimezoneruleModifiedby = "lk_timezonerule_modifiedby";
                public const string LkTimezoneruleModifiedonbehalfby = "lk_timezonerule_modifiedonbehalfby";
                public const string LkToolinggatewayCreatedby = "lk_toolinggateway_createdby";
                public const string LkToolinggatewayCreatedonbehalfby = "lk_toolinggateway_createdonbehalfby";
                public const string LkToolinggatewayModifiedby = "lk_toolinggateway_modifiedby";
                public const string LkToolinggatewayModifiedonbehalfby = "lk_toolinggateway_modifiedonbehalfby";
                public const string LkToolinggatewaymcpserverCreatedby = "lk_toolinggatewaymcpserver_createdby";
                public const string LkToolinggatewaymcpserverCreatedonbehalfby = "lk_toolinggatewaymcpserver_createdonbehalfby";
                public const string LkToolinggatewaymcpserverModifiedby = "lk_toolinggatewaymcpserver_modifiedby";
                public const string LkToolinggatewaymcpserverModifiedonbehalfby = "lk_toolinggatewaymcpserver_modifiedonbehalfby";
                public const string LkTracelogCreatedby = "lk_tracelog_createdby";
                public const string LkTracelogCreatedonbehalfby = "lk_tracelog_createdonbehalfby";
                public const string LkTracelogModifiedby = "lk_tracelog_modifiedby";
                public const string LkTracelogModifiedonbehalfby = "lk_tracelog_modifiedonbehalfby";
                public const string LkTraitCreatedby = "lk_trait_createdby";
                public const string LkTraitCreatedonbehalfby = "lk_trait_createdonbehalfby";
                public const string LkTraitModifiedby = "lk_trait_modifiedby";
                public const string LkTraitModifiedonbehalfby = "lk_trait_modifiedonbehalfby";
                public const string LkTraitregistrationCreatedby = "lk_traitregistration_createdby";
                public const string LkTraitregistrationCreatedonbehalfby = "lk_traitregistration_createdonbehalfby";
                public const string LkTraitregistrationModifiedby = "lk_traitregistration_modifiedby";
                public const string LkTraitregistrationModifiedonbehalfby = "lk_traitregistration_modifiedonbehalfby";
                public const string LkTransactioncurrencyCreatedonbehalfby = "lk_transactioncurrency_createdonbehalfby";
                public const string LkTransactioncurrencyModifiedonbehalfby = "lk_transactioncurrency_modifiedonbehalfby";
                public const string LkTransactioncurrencybaseCreatedby = "lk_transactioncurrencybase_createdby";
                public const string LkTransactioncurrencybaseModifiedby = "lk_transactioncurrencybase_modifiedby";
                public const string LkTransformationmappingCreatedby = "lk_transformationmapping_createdby";
                public const string LkTransformationmappingCreatedonbehalfby = "lk_transformationmapping_createdonbehalfby";
                public const string LkTransformationmappingModifiedby = "lk_transformationmapping_modifiedby";
                public const string LkTransformationmappingModifiedonbehalfby = "lk_transformationmapping_modifiedonbehalfby";
                public const string LkTransformationparametermappingCreatedby = "lk_transformationparametermapping_createdby";
                public const string LkTransformationparametermappingCreatedonbehalfby = "lk_transformationparametermapping_createdonbehalfby";
                public const string LkTransformationparametermappingModifiedby = "lk_transformationparametermapping_modifiedby";
                public const string LkTransformationparametermappingModifiedonbehalfby = "lk_transformationparametermapping_modifiedonbehalfby";
                public const string LkTranslationprocessCreatedby = "lk_translationprocess_createdby";
                public const string LkTranslationprocessCreatedonbehalfby = "lk_translationprocess_createdonbehalfby";
                public const string LkTranslationprocessModifiedby = "lk_translationprocess_modifiedby";
                public const string LkTranslationprocessModifiedonbehalfby = "lk_translationprocess_modifiedonbehalfby";
                public const string LkUnstructuredfilesearchentityCreatedby = "lk_unstructuredfilesearchentity_createdby";
                public const string LkUnstructuredfilesearchentityCreatedonbehalfby = "lk_unstructuredfilesearchentity_createdonbehalfby";
                public const string LkUnstructuredfilesearchentityModifiedby = "lk_unstructuredfilesearchentity_modifiedby";
                public const string LkUnstructuredfilesearchentityModifiedonbehalfby = "lk_unstructuredfilesearchentity_modifiedonbehalfby";
                public const string LkUnstructuredfilesearchrecordCreatedby = "lk_unstructuredfilesearchrecord_createdby";
                public const string LkUnstructuredfilesearchrecordCreatedonbehalfby = "lk_unstructuredfilesearchrecord_createdonbehalfby";
                public const string LkUnstructuredfilesearchrecordModifiedby = "lk_unstructuredfilesearchrecord_modifiedby";
                public const string LkUnstructuredfilesearchrecordModifiedonbehalfby = "lk_unstructuredfilesearchrecord_modifiedonbehalfby";
                public const string LkUnstructuredfilesearchrecordstatusCreatedby = "lk_unstructuredfilesearchrecordstatus_createdby";
                public const string LkUnstructuredfilesearchrecordstatusCreatedonbehalfby = "lk_unstructuredfilesearchrecordstatus_createdonbehalfby";
                public const string LkUnstructuredfilesearchrecordstatusModifiedby = "lk_unstructuredfilesearchrecordstatus_modifiedby";
                public const string LkUnstructuredfilesearchrecordstatusModifiedonbehalfby = "lk_unstructuredfilesearchrecordstatus_modifiedonbehalfby";
                public const string LkUntrackedemailCreatedby = "lk_untrackedemail_createdby";
                public const string LkUntrackedemailCreatedonbehalfby = "lk_untrackedemail_createdonbehalfby";
                public const string LkUntrackedemailModifiedby = "lk_untrackedemail_modifiedby";
                public const string LkUntrackedemailModifiedonbehalfby = "lk_untrackedemail_modifiedonbehalfby";
                public const string LkUserapplicationmetadataCreatedby = "lk_userapplicationmetadata_createdby";
                public const string LkUserapplicationmetadataCreatedonbehalfby = "lk_userapplicationmetadata_createdonbehalfby";
                public const string LkUserapplicationmetadataModifiedby = "lk_userapplicationmetadata_modifiedby";
                public const string LkUserapplicationmetadataModifiedonbehalfby = "lk_userapplicationmetadata_modifiedonbehalfby";
                public const string LkUserfiscalcalendarCreatedby = "lk_userfiscalcalendar_createdby";
                public const string LkUserfiscalcalendarCreatedonbehalfby = "lk_userfiscalcalendar_createdonbehalfby";
                public const string LkUserfiscalcalendarModifiedby = "lk_userfiscalcalendar_modifiedby";
                public const string LkUserfiscalcalendarModifiedonbehalfby = "lk_userfiscalcalendar_modifiedonbehalfby";
                public const string LkUserformCreatedby = "lk_userform_createdby";
                public const string LkUserformModifiedby = "lk_userform_modifiedby";
                public const string LkUserformbaseCreatedonbehalfby = "lk_userformbase_createdonbehalfby";
                public const string LkUserformbaseModifiedonbehalfby = "lk_userformbase_modifiedonbehalfby";
                public const string LkUsermappingCreatedby = "lk_usermapping_createdby";
                public const string LkUsermappingCreatedonbehalfby = "lk_usermapping_createdonbehalfby";
                public const string LkUsermappingModifiedby = "lk_usermapping_modifiedby";
                public const string LkUsermappingModifiedonbehalfby = "lk_usermapping_modifiedonbehalfby";
                public const string LkUsermobileofflineprofilemembershipCreatedby = "lk_usermobileofflineprofilemembership_createdby";
                public const string LkUsermobileofflineprofilemembershipCreatedonbehalfby = "lk_usermobileofflineprofilemembership_createdonbehalfby";
                public const string LkUsermobileofflineprofilemembershipModifiedby = "lk_usermobileofflineprofilemembership_modifiedby";
                public const string LkUsermobileofflineprofilemembershipModifiedonbehalfby = "lk_usermobileofflineprofilemembership_modifiedonbehalfby";
                public const string LkUserqueryCreatedby = "lk_userquery_createdby";
                public const string LkUserqueryCreatedonbehalfby = "lk_userquery_createdonbehalfby";
                public const string LkUserqueryModifiedby = "lk_userquery_modifiedby";
                public const string LkUserqueryModifiedonbehalfby = "lk_userquery_modifiedonbehalfby";
                public const string LkUserqueryvisualizationCreatedby = "lk_userqueryvisualization_createdby";
                public const string LkUserqueryvisualizationModifiedby = "lk_userqueryvisualization_modifiedby";
                public const string LkUserqueryvisualizationbaseCreatedonbehalfby = "lk_userqueryvisualizationbase_createdonbehalfby";
                public const string LkUserqueryvisualizationbaseModifiedonbehalfby = "lk_userqueryvisualizationbase_modifiedonbehalfby";
                public const string LkUserratingCreatedby = "lk_userrating_createdby";
                public const string LkUserratingCreatedonbehalfby = "lk_userrating_createdonbehalfby";
                public const string LkUserratingModifiedby = "lk_userrating_modifiedby";
                public const string LkUserratingModifiedonbehalfby = "lk_userrating_modifiedonbehalfby";
                public const string LkUsersettingsCreatedonbehalfby = "lk_usersettings_createdonbehalfby";
                public const string LkUsersettingsModifiedonbehalfby = "lk_usersettings_modifiedonbehalfby";
                public const string LkUsersettingsbaseCreatedby = "lk_usersettingsbase_createdby";
                public const string LkUsersettingsbaseModifiedby = "lk_usersettingsbase_modifiedby";
                public const string LkUxagentcomponentCreatedby = "lk_uxagentcomponent_createdby";
                public const string LkUxagentcomponentCreatedonbehalfby = "lk_uxagentcomponent_createdonbehalfby";
                public const string LkUxagentcomponentModifiedby = "lk_uxagentcomponent_modifiedby";
                public const string LkUxagentcomponentModifiedonbehalfby = "lk_uxagentcomponent_modifiedonbehalfby";
                public const string LkUxagentcomponentrevisionCreatedby = "lk_uxagentcomponentrevision_createdby";
                public const string LkUxagentcomponentrevisionCreatedonbehalfby = "lk_uxagentcomponentrevision_createdonbehalfby";
                public const string LkUxagentcomponentrevisionModifiedby = "lk_uxagentcomponentrevision_modifiedby";
                public const string LkUxagentcomponentrevisionModifiedonbehalfby = "lk_uxagentcomponentrevision_modifiedonbehalfby";
                public const string LkUxagentprojectCreatedby = "lk_uxagentproject_createdby";
                public const string LkUxagentprojectCreatedonbehalfby = "lk_uxagentproject_createdonbehalfby";
                public const string LkUxagentprojectModifiedby = "lk_uxagentproject_modifiedby";
                public const string LkUxagentprojectModifiedonbehalfby = "lk_uxagentproject_modifiedonbehalfby";
                public const string LkUxagentprojectfileCreatedby = "lk_uxagentprojectfile_createdby";
                public const string LkUxagentprojectfileCreatedonbehalfby = "lk_uxagentprojectfile_createdonbehalfby";
                public const string LkUxagentprojectfileModifiedby = "lk_uxagentprojectfile_modifiedby";
                public const string LkUxagentprojectfileModifiedonbehalfby = "lk_uxagentprojectfile_modifiedonbehalfby";
                public const string LkViewasexamplequestionCreatedby = "lk_viewasexamplequestion_createdby";
                public const string LkViewasexamplequestionCreatedonbehalfby = "lk_viewasexamplequestion_createdonbehalfby";
                public const string LkViewasexamplequestionModifiedby = "lk_viewasexamplequestion_modifiedby";
                public const string LkViewasexamplequestionModifiedonbehalfby = "lk_viewasexamplequestion_modifiedonbehalfby";
                public const string LkVirtualentitymetadataCreatedby = "lk_virtualentitymetadata_createdby";
                public const string LkVirtualentitymetadataCreatedonbehalfby = "lk_virtualentitymetadata_createdonbehalfby";
                public const string LkVirtualentitymetadataModifiedby = "lk_virtualentitymetadata_modifiedby";
                public const string LkVirtualentitymetadataModifiedonbehalfby = "lk_virtualentitymetadata_modifiedonbehalfby";
                public const string LkWebresourcebaseCreatedonbehalfby = "lk_webresourcebase_createdonbehalfby";
                public const string LkWebresourcebaseModifiedonbehalfby = "lk_webresourcebase_modifiedonbehalfby";
                public const string LkWebwizardCreatedby = "lk_webwizard_createdby";
                public const string LkWebwizardCreatedonbehalfby = "lk_webwizard_createdonbehalfby";
                public const string LkWebwizardModifiedby = "lk_webwizard_modifiedby";
                public const string LkWebwizardModifiedonbehalfby = "lk_webwizard_modifiedonbehalfby";
                public const string LkWizardaccessprivilegeCreatedby = "lk_wizardaccessprivilege_createdby";
                public const string LkWizardaccessprivilegeCreatedonbehalfby = "lk_wizardaccessprivilege_createdonbehalfby";
                public const string LkWizardaccessprivilegeModifiedby = "lk_wizardaccessprivilege_modifiedby";
                public const string LkWizardaccessprivilegeModifiedonbehalfby = "lk_wizardaccessprivilege_modifiedonbehalfby";
                public const string LkWizardpageCreatedby = "lk_wizardpage_createdby";
                public const string LkWizardpageCreatedonbehalfby = "lk_wizardpage_createdonbehalfby";
                public const string LkWizardpageModifiedby = "lk_wizardpage_modifiedby";
                public const string LkWizardpageModifiedonbehalfby = "lk_wizardpage_modifiedonbehalfby";
                public const string LkWorkflowbinaryCreatedby = "lk_workflowbinary_createdby";
                public const string LkWorkflowbinaryCreatedonbehalfby = "lk_workflowbinary_createdonbehalfby";
                public const string LkWorkflowbinaryModifiedby = "lk_workflowbinary_modifiedby";
                public const string LkWorkflowbinaryModifiedonbehalfby = "lk_workflowbinary_modifiedonbehalfby";
                public const string LkWorkflowlogCreatedby = "lk_workflowlog_createdby";
                public const string LkWorkflowlogCreatedonbehalfby = "lk_workflowlog_createdonbehalfby";
                public const string LkWorkflowlogModifiedby = "lk_workflowlog_modifiedby";
                public const string LkWorkflowlogModifiedonbehalfby = "lk_workflowlog_modifiedonbehalfby";
                public const string LkWorkflowmetadataCreatedby = "lk_workflowmetadata_createdby";
                public const string LkWorkflowmetadataCreatedonbehalfby = "lk_workflowmetadata_createdonbehalfby";
                public const string LkWorkflowmetadataModifiedby = "lk_workflowmetadata_modifiedby";
                public const string LkWorkflowmetadataModifiedonbehalfby = "lk_workflowmetadata_modifiedonbehalfby";
                public const string LkWorkqueueCreatedby = "lk_workqueue_createdby";
                public const string LkWorkqueueCreatedonbehalfby = "lk_workqueue_createdonbehalfby";
                public const string LkWorkqueueModifiedby = "lk_workqueue_modifiedby";
                public const string LkWorkqueueModifiedonbehalfby = "lk_workqueue_modifiedonbehalfby";
                public const string LkWorkqueueitemCreatedby = "lk_workqueueitem_createdby";
                public const string LkWorkqueueitemCreatedonbehalfby = "lk_workqueueitem_createdonbehalfby";
                public const string LkWorkqueueitemModifiedby = "lk_workqueueitem_modifiedby";
                public const string LkWorkqueueitemModifiedonbehalfby = "lk_workqueueitem_modifiedonbehalfby";
                public const string MailboxEmailaddressapprovedbySystemuser = "mailbox_emailaddressapprovedby_systemuser";
                public const string MailboxRegardingSystemuser = "mailbox_regarding_systemuser";
                public const string MailboxTestandenablelastattemptedbySystemuser = "mailbox_testandenablelastattemptedby_systemuser";
                public const string ModifiedbyAttributemap = "modifiedby_attributemap";
                public const string ModifiedbyConnection = "modifiedby_connection";
                public const string ModifiedbyConnectionRole = "modifiedby_connection_role";
                public const string ModifiedbyCustomerRelationship = "modifiedby_customer_relationship";
                public const string ModifiedbyEntitymap = "modifiedby_entitymap";
                public const string ModifiedbyExpanderevent = "modifiedby_expanderevent";
                public const string ModifiedbyPluginassembly = "modifiedby_pluginassembly";
                public const string ModifiedbyPlugintype = "modifiedby_plugintype";
                public const string ModifiedbyPlugintypestatistic = "modifiedby_plugintypestatistic";
                public const string ModifiedbyRelationshipRole = "modifiedby_relationship_role";
                public const string ModifiedbyRelationshipRoleMap = "modifiedby_relationship_role_map";
                public const string ModifiedbySdkmessage = "modifiedby_sdkmessage";
                public const string ModifiedbySdkmessagefilter = "modifiedby_sdkmessagefilter";
                public const string ModifiedbySdkmessagepair = "modifiedby_sdkmessagepair";
                public const string ModifiedbySdkmessageprocessingstep = "modifiedby_sdkmessageprocessingstep";
                public const string ModifiedbySdkmessageprocessingstepimage = "modifiedby_sdkmessageprocessingstepimage";
                public const string ModifiedbySdkmessageprocessingstepsecureconfig = "modifiedby_sdkmessageprocessingstepsecureconfig";
                public const string ModifiedbySdkmessagerequest = "modifiedby_sdkmessagerequest";
                public const string ModifiedbySdkmessagerequestfield = "modifiedby_sdkmessagerequestfield";
                public const string ModifiedbySdkmessageresponse = "modifiedby_sdkmessageresponse";
                public const string ModifiedbySdkmessageresponsefield = "modifiedby_sdkmessageresponsefield";
                public const string ModifiedbyServiceendpoint = "modifiedby_serviceendpoint";
                public const string ModifiedonbehalfbyAttributemap = "modifiedonbehalfby_attributemap";
                public const string ModifiedonbehalfbyCustomerRelationship = "modifiedonbehalfby_customer_relationship";
                public const string MsppSystemuserMsppAdplacementCreatedby = "mspp_systemuser_mspp_adplacement_createdby";
                public const string MsppSystemuserMsppAdplacementModifiedby = "mspp_systemuser_mspp_adplacement_modifiedby";
                public const string MsppSystemuserMsppColumnpermissionCreatedby = "mspp_systemuser_mspp_columnpermission_createdby";
                public const string MsppSystemuserMsppColumnpermissionModifiedby = "mspp_systemuser_mspp_columnpermission_modifiedby";
                public const string MsppSystemuserMsppColumnpermissionprofileCreatedby = "mspp_systemuser_mspp_columnpermissionprofile_createdby";
                public const string MsppSystemuserMsppColumnpermissionprofileModifiedby = "mspp_systemuser_mspp_columnpermissionprofile_modifiedby";
                public const string MsppSystemuserMsppContentsnippetCreatedby = "mspp_systemuser_mspp_contentsnippet_createdby";
                public const string MsppSystemuserMsppContentsnippetModifiedby = "mspp_systemuser_mspp_contentsnippet_modifiedby";
                public const string MsppSystemuserMsppEntityformCreatedby = "mspp_systemuser_mspp_entityform_createdby";
                public const string MsppSystemuserMsppEntityformModifiedby = "mspp_systemuser_mspp_entityform_modifiedby";
                public const string MsppSystemuserMsppEntityformmetadataCreatedby = "mspp_systemuser_mspp_entityformmetadata_createdby";
                public const string MsppSystemuserMsppEntityformmetadataModifiedby = "mspp_systemuser_mspp_entityformmetadata_modifiedby";
                public const string MsppSystemuserMsppEntitylistCreatedby = "mspp_systemuser_mspp_entitylist_createdby";
                public const string MsppSystemuserMsppEntitylistModifiedby = "mspp_systemuser_mspp_entitylist_modifiedby";
                public const string MsppSystemuserMsppEntitypermissionCreatedby = "mspp_systemuser_mspp_entitypermission_createdby";
                public const string MsppSystemuserMsppEntitypermissionModifiedby = "mspp_systemuser_mspp_entitypermission_modifiedby";
                public const string MsppSystemuserMsppPagetemplateCreatedby = "mspp_systemuser_mspp_pagetemplate_createdby";
                public const string MsppSystemuserMsppPagetemplateModifiedby = "mspp_systemuser_mspp_pagetemplate_modifiedby";
                public const string MsppSystemuserMsppPollplacementCreatedby = "mspp_systemuser_mspp_pollplacement_createdby";
                public const string MsppSystemuserMsppPollplacementModifiedby = "mspp_systemuser_mspp_pollplacement_modifiedby";
                public const string MsppSystemuserMsppPublishingstateCreatedby = "mspp_systemuser_mspp_publishingstate_createdby";
                public const string MsppSystemuserMsppPublishingstateModifiedby = "mspp_systemuser_mspp_publishingstate_modifiedby";
                public const string MsppSystemuserMsppPublishingstatetransitionruleCreatedby = "mspp_systemuser_mspp_publishingstatetransitionrule_createdby";
                public const string MsppSystemuserMsppPublishingstatetransitionruleModifiedby = "mspp_systemuser_mspp_publishingstatetransitionrule_modifiedby";
                public const string MsppSystemuserMsppRedirectCreatedby = "mspp_systemuser_mspp_redirect_createdby";
                public const string MsppSystemuserMsppRedirectModifiedby = "mspp_systemuser_mspp_redirect_modifiedby";
                public const string MsppSystemuserMsppShortcutCreatedby = "mspp_systemuser_mspp_shortcut_createdby";
                public const string MsppSystemuserMsppShortcutModifiedby = "mspp_systemuser_mspp_shortcut_modifiedby";
                public const string MsppSystemuserMsppSitemarkerCreatedby = "mspp_systemuser_mspp_sitemarker_createdby";
                public const string MsppSystemuserMsppSitemarkerModifiedby = "mspp_systemuser_mspp_sitemarker_modifiedby";
                public const string MsppSystemuserMsppSitesettingCreatedby = "mspp_systemuser_mspp_sitesetting_createdby";
                public const string MsppSystemuserMsppSitesettingModifiedby = "mspp_systemuser_mspp_sitesetting_modifiedby";
                public const string MsppSystemuserMsppWebfileCreatedby = "mspp_systemuser_mspp_webfile_createdby";
                public const string MsppSystemuserMsppWebfileModifiedby = "mspp_systemuser_mspp_webfile_modifiedby";
                public const string MsppSystemuserMsppWebformCreatedby = "mspp_systemuser_mspp_webform_createdby";
                public const string MsppSystemuserMsppWebformModifiedby = "mspp_systemuser_mspp_webform_modifiedby";
                public const string MsppSystemuserMsppWebformmetadataCreatedby = "mspp_systemuser_mspp_webformmetadata_createdby";
                public const string MsppSystemuserMsppWebformmetadataModifiedby = "mspp_systemuser_mspp_webformmetadata_modifiedby";
                public const string MsppSystemuserMsppWebformstepCreatedby = "mspp_systemuser_mspp_webformstep_createdby";
                public const string MsppSystemuserMsppWebformstepModifiedby = "mspp_systemuser_mspp_webformstep_modifiedby";
                public const string MsppSystemuserMsppWeblinkCreatedby = "mspp_systemuser_mspp_weblink_createdby";
                public const string MsppSystemuserMsppWeblinkModifiedby = "mspp_systemuser_mspp_weblink_modifiedby";
                public const string MsppSystemuserMsppWeblinksetCreatedby = "mspp_systemuser_mspp_weblinkset_createdby";
                public const string MsppSystemuserMsppWeblinksetModifiedby = "mspp_systemuser_mspp_weblinkset_modifiedby";
                public const string MsppSystemuserMsppWebpageCreatedby = "mspp_systemuser_mspp_webpage_createdby";
                public const string MsppSystemuserMsppWebpageModifiedby = "mspp_systemuser_mspp_webpage_modifiedby";
                public const string MsppSystemuserMsppWebpageaccesscontrolruleCreatedby = "mspp_systemuser_mspp_webpageaccesscontrolrule_createdby";
                public const string MsppSystemuserMsppWebpageaccesscontrolruleModifiedby = "mspp_systemuser_mspp_webpageaccesscontrolrule_modifiedby";
                public const string MsppSystemuserMsppWebroleCreatedby = "mspp_systemuser_mspp_webrole_createdby";
                public const string MsppSystemuserMsppWebroleModifiedby = "mspp_systemuser_mspp_webrole_modifiedby";
                public const string MsppSystemuserMsppWebsiteCreatedby = "mspp_systemuser_mspp_website_createdby";
                public const string MsppSystemuserMsppWebsiteModifiedby = "mspp_systemuser_mspp_website_modifiedby";
                public const string MsppSystemuserMsppWebsiteaccessCreatedby = "mspp_systemuser_mspp_websiteaccess_createdby";
                public const string MsppSystemuserMsppWebsiteaccessModifiedby = "mspp_systemuser_mspp_websiteaccess_modifiedby";
                public const string MsppSystemuserMsppWebsitelanguageCreatedby = "mspp_systemuser_mspp_websitelanguage_createdby";
                public const string MsppSystemuserMsppWebsitelanguageModifiedby = "mspp_systemuser_mspp_websitelanguage_modifiedby";
                public const string MsppSystemuserMsppWebtemplateCreatedby = "mspp_systemuser_mspp_webtemplate_createdby";
                public const string MsppSystemuserMsppWebtemplateModifiedby = "mspp_systemuser_mspp_webtemplate_modifiedby";
                public const string MultientitysearchCreatedby = "multientitysearch_createdby";
                public const string MultientitysearchCreatedonbehalfby = "multientitysearch_createdonbehalfby";
                public const string MultientitysearchModifiedby = "multientitysearch_modifiedby";
                public const string MultientitysearchModifiedonbehalfby = "multientitysearch_modifiedonbehalfby";
                public const string OwnerMappingSystemUser = "OwnerMapping_SystemUser";
                public const string PrivilegecheckerlogCheckedUserSystemuser = "privilegecheckerlog_CheckedUser_systemuser";
                public const string PrivilegecheckerlogImpersonatingUserSystemuser = "privilegecheckerlog_ImpersonatingUser_systemuser";
                public const string PrivilegecheckerlogSupportingCallerSystemuser = "privilegecheckerlog_SupportingCaller_systemuser";
                public const string QueuePrimaryUser = "queue_primary_user";
                public const string SaSuggestedactionCompletedBySystemuser = "sa_suggestedaction_CompletedBy_systemuser";
                public const string SideloadedaipluginSideloadedpluginownerid = "sideloadedaiplugin_sideloadedpluginownerid";
                public const string SocialProfileOwningUser = "socialProfile_owning_user";
                public const string SupPrincipalidSystemuser = "sup_principalid_systemuser";
                public const string SystemUserAccounts = "system_user_accounts";
                public const string SystemUserActivityParties = "system_user_activity_parties";
                public const string SystemUserAsyncoperation = "system_user_asyncoperation";
                public const string SystemUserContacts = "system_user_contacts";
                public const string SystemUserEmailTemplates = "system_user_email_templates";
                public const string SystemUserQuotas = "system_user_quotas";
                public const string SystemUserTerritories = "system_user_territories";
                public const string SystemUserWorkflow = "system_user_workflow";
                public const string SystemuserAppusersettingUserId = "systemuser_appusersetting_userId";
                public const string SystemUserAsyncOperations = "SystemUser_AsyncOperations";
                public const string SystemuserBotPublishedby = "systemuser_bot_publishedby";
                public const string SystemUserBulkDeleteFailures = "SystemUser_BulkDeleteFailures";
                public const string SystemuserConnections1 = "systemuser_connections1";
                public const string SystemuserConnections2 = "systemuser_connections2";
                public const string SystemUserDuplicateBaseRecord = "SystemUser_DuplicateBaseRecord";
                public const string SystemUserDuplicateMatchingRecord = "SystemUser_DuplicateMatchingRecord";
                public const string SystemUserDuplicateRules = "SystemUser_DuplicateRules";
                public const string SystemUserEmailEmailSender = "SystemUser_Email_EmailSender";
                public const string SystemUserExternalPartyItems = "SystemUser_ExternalPartyItems";
                public const string SystemUserImportData = "SystemUser_ImportData";
                public const string SystemUserImportFiles = "SystemUser_ImportFiles";
                public const string SystemUserImportLogs = "SystemUser_ImportLogs";
                public const string SystemUserImportMaps = "SystemUser_ImportMaps";
                public const string SystemUserImports = "SystemUser_Imports";
                public const string SystemuserInternalAddresses = "systemuser_internal_addresses";
                public const string SystemuserPostFollows = "systemuser_PostFollows";
                public const string SystemuserPostRegardings = "systemuser_PostRegardings";
                public const string SystemuserPostRoles = "systemuser_PostRoles";
                public const string SystemuserPrincipalobjectattributeaccess = "systemuser_principalobjectattributeaccess";
                public const string SystemuserPrincipalobjectattributeaccessPrincipalid = "systemuser_principalobjectattributeaccess_principalid";
                public const string SystemUserProcessSessions = "SystemUser_ProcessSessions";
                public const string SystemuserSourcecontroloperationstatusStartedBy = "systemuser_sourcecontroloperationstatus_StartedBy";
                public const string SystemUserSyncError = "SystemUser_SyncError";
                public const string SystemUserSyncErrors = "SystemUser_SyncErrors";
                public const string SystemuserUsermobileofflineprofilemembershipSystemUserId = "systemuser_usermobileofflineprofilemembership_SystemUserId";
                public const string SystemuserbusinessunitentitymapSystemuseridSystemuser = "systemuserbusinessunitentitymap_systemuserid_systemuser";
                public const string TeamsChatActivityLinkrecordSystemUser = "teams_chat_activity_linkrecord_systemUser";
                public const string TeamsChatActivityUnlinkrecordSystemUser = "teams_chat_activity_unlinkrecord_systemUser";
                public const string UserAccounts = "user_accounts";
                public const string UserActivity = "user_activity";
                public const string UserActivityfileattachment = "user_activityfileattachment";
                public const string UserAdxInvitation = "user_adx_invitation";
                public const string UserAdxSetting = "user_adx_setting";
                public const string UserAgentconversationmessage = "user_agentconversationmessage";
                public const string UserAgentconversationmessagefile = "user_agentconversationmessagefile";
                public const string UserAgentfeeditem = "user_agentfeeditem";
                public const string UserAgenthubgoal = "user_agenthubgoal";
                public const string UserAgenthubinsight = "user_agenthubinsight";
                public const string UserAgenthubmetric = "user_agenthubmetric";
                public const string UserAgenticscenario = "user_agenticscenario";
                public const string UserAgentmemory = "user_agentmemory";
                public const string UserAgentrule = "user_agentrule";
                public const string UserAgenttask = "user_agenttask";
                public const string UserAiinsightcard = "user_aiinsightcard";
                public const string UserAiplugin = "user_aiplugin";
                public const string UserAipluginauth = "user_aipluginauth";
                public const string UserAipluginconversationstarter = "user_aipluginconversationstarter";
                public const string UserAipluginconversationstartermapping = "user_aipluginconversationstartermapping";
                public const string UserAipluginexternalschema = "user_aipluginexternalschema";
                public const string UserAipluginexternalschemaproperty = "user_aipluginexternalschemaproperty";
                public const string UserAiplugingovernance = "user_aiplugingovernance";
                public const string UserAiplugingovernanceext = "user_aiplugingovernanceext";
                public const string UserAiplugininstance = "user_aiplugininstance";
                public const string UserAipluginoperation = "user_aipluginoperation";
                public const string UserAipluginoperationparameter = "user_aipluginoperationparameter";
                public const string UserAipluginoperationresponsetemplate = "user_aipluginoperationresponsetemplate";
                public const string UserAipluginusersetting = "user_aipluginusersetting";
                public const string UserAiskillconfig = "user_aiskillconfig";
                public const string UserAppnotification = "user_appnotification";
                public const string UserAppointment = "user_appointment";
                public const string UserApprovalprocess = "user_approvalprocess";
                public const string UserApprovalstageapproval = "user_approvalstageapproval";
                public const string UserApprovalstagecondition = "user_approvalstagecondition";
                public const string UserApprovalstageintelligent = "user_approvalstageintelligent";
                public const string UserApprovalstageorder = "user_approvalstageorder";
                public const string UserArchivecleanupinfo = "user_archivecleanupinfo";
                public const string UserArchivecleanupoperation = "user_archivecleanupoperation";
                public const string UserBot = "user_bot";
                public const string UserBotcomponent = "user_botcomponent";
                public const string UserBotcomponentcollection = "user_botcomponentcollection";
                public const string UserBulkarchiveconfig = "user_bulkarchiveconfig";
                public const string UserBulkarchivefailuredetail = "user_bulkarchivefailuredetail";
                public const string UserBulkarchiveoperation = "user_bulkarchiveoperation";
                public const string UserBusinessprocess = "user_businessprocess";
                public const string UserCanvasappextendedmetadata = "user_canvasappextendedmetadata";
                public const string UserCard = "user_card";
                public const string UserCertificatecredential = "user_certificatecredential";
                public const string UserChannelaccessprofile = "user_channelaccessprofile";
                public const string UserComment = "user_comment";
                public const string UserComponentversion = "user_componentversion";
                public const string UserComputeruseagent = "user_computeruseagent";
                public const string UserConnectioninstance = "user_connectioninstance";
                public const string UserConnectionreference = "user_connectionreference";
                public const string UserConnector = "user_connector";
                public const string UserConversationtranscript = "user_conversationtranscript";
                public const string UserConvertrule = "user_convertrule";
                public const string UserCopilotglossaryterm = "user_copilotglossaryterm";
                public const string UserCopilotsynonyms = "user_copilotsynonyms";
                public const string UserCredential = "user_credential";
                public const string UserCustomapi = "user_customapi";
                public const string UserCustomerRelationship = "user_customer_relationship";
                public const string UserDatalakefolder = "user_datalakefolder";
                public const string UserDesktopflowbinary = "user_desktopflowbinary";
                public const string UserDesktopflowmodule = "user_desktopflowmodule";
                public const string UserDvfilesearch = "user_dvfilesearch";
                public const string UserDvfilesearchattribute = "user_dvfilesearchattribute";
                public const string UserDvfilesearchentity = "user_dvfilesearchentity";
                public const string UserDvtablesearch = "user_dvtablesearch";
                public const string UserDvtablesearchattribute = "user_dvtablesearchattribute";
                public const string UserDvtablesearchentity = "user_dvtablesearchentity";
                public const string UserEmail = "user_email";
                public const string UserEnablearchivalrequest = "user_enablearchivalrequest";
                public const string UserEnvironmentvariabledefinition = "user_environmentvariabledefinition";
                public const string UserExchangesyncidmapping = "user_exchangesyncidmapping";
                public const string UserExportedexcel = "user_exportedexcel";
                public const string UserExportsolutionupload = "user_exportsolutionupload";
                public const string UserExternalparty = "user_externalparty";
                public const string UserFabricaiskill = "user_fabricaiskill";
                public const string UserFax = "user_fax";
                public const string UserFeaturecontrolsetting = "user_featurecontrolsetting";
                public const string UserFederatedknowledgecitation = "user_federatedknowledgecitation";
                public const string UserFederatedknowledgeconfiguration = "user_federatedknowledgeconfiguration";
                public const string UserFederatedknowledgeentityconfiguration = "user_federatedknowledgeentityconfiguration";
                public const string UserFederatedknowledgemetadatarefresh = "user_federatedknowledgemetadatarefresh";
                public const string UserFlowaggregation = "user_flowaggregation";
                public const string UserFlowcapacityassignment = "user_flowcapacityassignment";
                public const string UserFlowcredentialapplication = "user_flowcredentialapplication";
                public const string UserFlowevent = "user_flowevent";
                public const string UserFlowgroup = "user_flowgroup";
                public const string UserFlowmachine = "user_flowmachine";
                public const string UserFlowmachinegroup = "user_flowmachinegroup";
                public const string UserFlowmachineimage = "user_flowmachineimage";
                public const string UserFlowmachineimageversion = "user_flowmachineimageversion";
                public const string UserFlowmachinenetwork = "user_flowmachinenetwork";
                public const string UserFlowrun = "user_flowrun";
                public const string UserFlowsession = "user_flowsession";
                public const string UserFlowsessionbinary = "user_flowsessionbinary";
                public const string UserFlowtestsession = "user_flowtestsession";
                public const string UserFlowtrigger = "user_flowtrigger";
                public const string UserFlowtriggerinstance = "user_flowtriggerinstance";
                public const string UserFxexpression = "user_fxexpression";
                public const string UserGithubappconfig = "user_githubappconfig";
                public const string UserGoal = "user_goal";
                public const string UserGoalGoalowner = "user_goal_goalowner";
                public const string UserGovernanceconfiguration = "user_governanceconfiguration";
                public const string UserIndexedtrait = "user_indexedtrait";
                public const string UserIntelligentmemory = "user_intelligentmemory";
                public const string UserInteractionforemail = "user_interactionforemail";
                public const string UserKeyvaultreference = "user_keyvaultreference";
                public const string UserKnowledgearticle = "user_knowledgearticle";
                public const string UserKnowledgefaq = "user_knowledgefaq";
                public const string UserKnowledgesourceconsumer = "user_knowledgesourceconsumer";
                public const string UserKnowledgesourceprofile = "user_knowledgesourceprofile";
                public const string UserLetter = "user_letter";
                public const string UserMailbox = "user_mailbox";
                public const string UserManagedidentity = "user_managedidentity";
                public const string UserMcpprompt = "user_mcpprompt";
                public const string UserMcpresource = "user_mcpresource";
                public const string UserMcpresourcecontent = "user_mcpresourcecontent";
                public const string UserMcpserver = "user_mcpserver";
                public const string UserMcptool = "user_mcptool";
                public const string UserMsdynAibdataset = "user_msdyn_aibdataset";
                public const string UserMsdynAibdatasetfile = "user_msdyn_aibdatasetfile";
                public const string UserMsdynAibdatasetrecord = "user_msdyn_aibdatasetrecord";
                public const string UserMsdynAibdatasetscontainer = "user_msdyn_aibdatasetscontainer";
                public const string UserMsdynAibfeedbackloop = "user_msdyn_aibfeedbackloop";
                public const string UserMsdynAibfile = "user_msdyn_aibfile";
                public const string UserMsdynAibfileattacheddata = "user_msdyn_aibfileattacheddata";
                public const string UserMsdynAiconfigurationsearch = "user_msdyn_aiconfigurationsearch";
                public const string UserMsdynAidataprocessingevent = "user_msdyn_aidataprocessingevent";
                public const string UserMsdynAidocumenttemplate = "user_msdyn_aidocumenttemplate";
                public const string UserMsdynAievaluationconfiguration = "user_msdyn_aievaluationconfiguration";
                public const string UserMsdynAievaluationrun = "user_msdyn_aievaluationrun";
                public const string UserMsdynAievent = "user_msdyn_aievent";
                public const string UserMsdynAifptrainingdocument = "user_msdyn_aifptrainingdocument";
                public const string UserMsdynAimodel = "user_msdyn_aimodel";
                public const string UserMsdynAimodelcatalog = "user_msdyn_aimodelcatalog";
                public const string UserMsdynAiodimage = "user_msdyn_aiodimage";
                public const string UserMsdynAiodlabel = "user_msdyn_aiodlabel";
                public const string UserMsdynAiodtrainingboundingbox = "user_msdyn_aiodtrainingboundingbox";
                public const string UserMsdynAiodtrainingimage = "user_msdyn_aiodtrainingimage";
                public const string UserMsdynAioptimization = "user_msdyn_aioptimization";
                public const string UserMsdynAioptimizationprivatedata = "user_msdyn_aioptimizationprivatedata";
                public const string UserMsdynAitemplate = "user_msdyn_aitemplate";
                public const string UserMsdynAitestcase = "user_msdyn_aitestcase";
                public const string UserMsdynAitestcasedocument = "user_msdyn_aitestcasedocument";
                public const string UserMsdynAitestcaseinput = "user_msdyn_aitestcaseinput";
                public const string UserMsdynAitestrun = "user_msdyn_aitestrun";
                public const string UserMsdynAitestrunbatch = "user_msdyn_aitestrunbatch";
                public const string UserMsdynAnalysiscomponent = "user_msdyn_analysiscomponent";
                public const string UserMsdynAnalysisjob = "user_msdyn_analysisjob";
                public const string UserMsdynAnalysisoverride = "user_msdyn_analysisoverride";
                public const string UserMsdynAnalysisresult = "user_msdyn_analysisresult";
                public const string UserMsdynAnalysisresultdetail = "user_msdyn_analysisresultdetail";
                public const string UserMsdynBulkharvestrunlog = "user_msdyn_bulkharvestrunlog";
                public const string UserMsdynCopilotinteractions = "user_msdyn_copilotinteractions";
                public const string UserMsdynCustomcontrolextendedsettings = "user_msdyn_customcontrolextendedsettings";
                public const string UserMsdynDataflow = "user_msdyn_dataflow";
                public const string UserMsdynDataflowDatalakefolder = "user_msdyn_dataflow_datalakefolder";
                public const string UserMsdynDataflowconnectionreference = "user_msdyn_dataflowconnectionreference";
                public const string UserMsdynDataflowrefreshhistory = "user_msdyn_dataflowrefreshhistory";
                public const string UserMsdynDataflowtemplate = "user_msdyn_dataflowtemplate";
                public const string UserMsdynDataworkspace = "user_msdyn_dataworkspace";
                public const string UserMsdynDmsrequest = "user_msdyn_dmsrequest";
                public const string UserMsdynDmsrequeststatus = "user_msdyn_dmsrequeststatus";
                public const string UserMsdynDmssyncrequest = "user_msdyn_dmssyncrequest";
                public const string UserMsdynDmssyncstatus = "user_msdyn_dmssyncstatus";
                public const string UserMsdynEntitylinkchatconfiguration = "user_msdyn_entitylinkchatconfiguration";
                public const string UserMsdynEntityrefreshhistory = "user_msdyn_entityrefreshhistory";
                public const string UserMsdynFavoriteknowledgearticle = "user_msdyn_favoriteknowledgearticle";
                public const string UserMsdynFederatedarticle = "user_msdyn_federatedarticle";
                public const string UserMsdynFileupload = "user_msdyn_fileupload";
                public const string UserMsdynFlowActionapprovalmodel = "user_msdyn_flow_actionapprovalmodel";
                public const string UserMsdynFlowApproval = "user_msdyn_flow_approval";
                public const string UserMsdynFlowApprovalrequest = "user_msdyn_flow_approvalrequest";
                public const string UserMsdynFlowApprovalresponse = "user_msdyn_flow_approvalresponse";
                public const string UserMsdynFlowApprovalstep = "user_msdyn_flow_approvalstep";
                public const string UserMsdynFlowAwaitallactionapprovalmodel = "user_msdyn_flow_awaitallactionapprovalmodel";
                public const string UserMsdynFlowAwaitallapprovalmodel = "user_msdyn_flow_awaitallapprovalmodel";
                public const string UserMsdynFlowBasicapprovalmodel = "user_msdyn_flow_basicapprovalmodel";
                public const string UserMsdynFlowFlowapproval = "user_msdyn_flow_flowapproval";
                public const string UserMsdynFormmapping = "user_msdyn_formmapping";
                public const string UserMsdynFunction = "user_msdyn_function";
                public const string UserMsdynHarvesteligibilitycondition = "user_msdyn_harvesteligibilitycondition";
                public const string UserMsdynHarvestworkitem = "user_msdyn_harvestworkitem";
                public const string UserMsdynHealthcareFeedback = "user_msdyn_healthcare_feedback";
                public const string UserMsdynHistoricalcaseharvestbatch = "user_msdyn_historicalcaseharvestbatch";
                public const string UserMsdynHistoricalcaseharvestrun = "user_msdyn_historicalcaseharvestrun";
                public const string UserMsdynIntegratedsearchprovider = "user_msdyn_integratedsearchprovider";
                public const string UserMsdynInterimupdateknowledgearticle = "user_msdyn_interimupdateknowledgearticle";
                public const string UserMsdynKalanguagesetting = "user_msdyn_kalanguagesetting";
                public const string UserMsdynKbattachment = "user_msdyn_kbattachment";
                public const string UserMsdynKmfederatedsearchconfig = "user_msdyn_kmfederatedsearchconfig";
                public const string UserMsdynKnowledgearticlecustomentity = "user_msdyn_knowledgearticlecustomentity";
                public const string UserMsdynKnowledgearticleimage = "user_msdyn_knowledgearticleimage";
                public const string UserMsdynKnowledgearticletemplate = "user_msdyn_knowledgearticletemplate";
                public const string UserMsdynKnowledgeassetconfiguration = "user_msdyn_knowledgeassetconfiguration";
                public const string UserMsdynKnowledgeharvestjobrecord = "user_msdyn_knowledgeharvestjobrecord";
                public const string UserMsdynKnowledgeharvestplan = "user_msdyn_knowledgeharvestplan";
                public const string UserMsdynKnowledgeinteractioninsight = "user_msdyn_knowledgeinteractioninsight";
                public const string UserMsdynKnowledgemanagementsetting = "user_msdyn_knowledgemanagementsetting";
                public const string UserMsdynKnowledgepersonalfilter = "user_msdyn_knowledgepersonalfilter";
                public const string UserMsdynKnowledgesearchfilter = "user_msdyn_knowledgesearchfilter";
                public const string UserMsdynKnowledgesearchinsight = "user_msdyn_knowledgesearchinsight";
                public const string UserMsdynMobileapp = "user_msdyn_mobileapp";
                public const string UserMsdynObjectdetectionproduct = "user_msdyn_objectdetectionproduct";
                public const string UserMsdynOnlineshopperintention = "user_msdyn_onlineshopperintention";
                public const string UserMsdynPlan = "user_msdyn_plan";
                public const string UserMsdynPlanartifact = "user_msdyn_planartifact";
                public const string UserMsdynPlanattachment = "user_msdyn_planattachment";
                public const string UserMsdynPmanalysishistory = "user_msdyn_pmanalysishistory";
                public const string UserMsdynPmbusinessruleautomationconfig = "user_msdyn_pmbusinessruleautomationconfig";
                public const string UserMsdynPmcalendar = "user_msdyn_pmcalendar";
                public const string UserMsdynPmcalendarversion = "user_msdyn_pmcalendarversion";
                public const string UserMsdynPminferredtask = "user_msdyn_pminferredtask";
                public const string UserMsdynPmprocessextendedmetadataversion = "user_msdyn_pmprocessextendedmetadataversion";
                public const string UserMsdynPmprocesstemplate = "user_msdyn_pmprocesstemplate";
                public const string UserMsdynPmprocessusersettings = "user_msdyn_pmprocessusersettings";
                public const string UserMsdynPmprocessversion = "user_msdyn_pmprocessversion";
                public const string UserMsdynPmrecording = "user_msdyn_pmrecording";
                public const string UserMsdynPmsimulation = "user_msdyn_pmsimulation";
                public const string UserMsdynPmtab = "user_msdyn_pmtab";
                public const string UserMsdynPmtemplate = "user_msdyn_pmtemplate";
                public const string UserMsdynPmview = "user_msdyn_pmview";
                public const string UserMsdynPowerappswrapbuild = "user_msdyn_powerappswrapbuild";
                public const string UserMsdynQna = "user_msdyn_qna";
                public const string UserMsdynRichtextfile = "user_msdyn_richtextfile";
                public const string UserMsdynRtestructuredtemplateconfig = "user_msdyn_rtestructuredtemplateconfig";
                public const string UserMsdynSalesforcestructuredobject = "user_msdyn_salesforcestructuredobject";
                public const string UserMsdynSalesforcestructuredqnaconfig = "user_msdyn_salesforcestructuredqnaconfig";
                public const string UserMsdynSchedule = "user_msdyn_schedule";
                public const string UserMsdynServiceconfiguration = "user_msdyn_serviceconfiguration";
                public const string UserMsdynSlakpi = "user_msdyn_slakpi";
                public const string UserMsdynSolutionhealthrule = "user_msdyn_solutionhealthrule";
                public const string UserMsdynSolutionhealthruleargument = "user_msdyn_solutionhealthruleargument";
                public const string UserMsdynVirtualtablecolumncandidate = "user_msdyn_virtualtablecolumncandidate";
                public const string UserMsdynceBotcontent = "user_msdynce_botcontent";
                public const string UserMspcatCatalogsubmissionfiles = "user_mspcat_catalogsubmissionfiles";
                public const string UserMspcatPackagestore = "user_mspcat_packagestore";
                public const string UserNlsqregistration = "user_nlsqregistration";
                public const string UserOfficedocument = "user_officedocument";
                public const string UserOwnerPostfollows = "user_owner_postfollows";
                public const string UserParentUser = "user_parent_user";
                public const string UserPdfsetting = "user_pdfsetting";
                public const string UserPhonecall = "user_phonecall";
                public const string UserPlannerbusinessscenario = "user_plannerbusinessscenario";
                public const string UserPlannersyncaction = "user_plannersyncaction";
                public const string UserPlugin = "user_plugin";
                public const string UserPowerfxrule = "user_powerfxrule";
                public const string UserPowerpagecomponent = "user_powerpagecomponent";
                public const string UserPowerpagesddosalert = "user_powerpagesddosalert";
                public const string UserPowerpagesite = "user_powerpagesite";
                public const string UserPowerpagesitelanguage = "user_powerpagesitelanguage";
                public const string UserPowerpagesitepublished = "user_powerpagesitepublished";
                public const string UserPowerpageslog = "user_powerpageslog";
                public const string UserPowerpagesmanagedidentity = "user_powerpagesmanagedidentity";
                public const string UserPowerpagesscanreport = "user_powerpagesscanreport";
                public const string UserPowerpagessiteaifeedback = "user_powerpagessiteaifeedback";
                public const string UserPowerpagessourcefile = "user_powerpagessourcefile";
                public const string UserPrivilegecheckerrun = "user_privilegecheckerrun";
                public const string UserProcessorregistration = "user_processorregistration";
                public const string UserProcessstageparameter = "user_processstageparameter";
                public const string UserProfilerule = "user_profilerule";
                public const string UserRecentlyused = "user_recentlyused";
                public const string UserReconciliationentityinfo = "user_reconciliationentityinfo";
                public const string UserReconciliationentitystepinfo = "user_reconciliationentitystepinfo";
                public const string UserReconciliationinfo = "user_reconciliationinfo";
                public const string UserRecurringappointmentmaster = "user_recurringappointmentmaster";
                public const string UserRetaineddataexcel = "user_retaineddataexcel";
                public const string UserRetentioncleanupinfo = "user_retentioncleanupinfo";
                public const string UserRetentioncleanupoperation = "user_retentioncleanupoperation";
                public const string UserRetentionconfig = "user_retentionconfig";
                public const string UserRetentionfailuredetail = "user_retentionfailuredetail";
                public const string UserRetentionoperation = "user_retentionoperation";
                public const string UserRetentionsuccessdetail = "user_retentionsuccessdetail";
                public const string UserRoutingrule = "user_routingrule";
                public const string UserRoutingruleitem = "user_routingruleitem";
                public const string UserSavingrule = "user_savingrule";
                public const string UserSettings = "user_settings";
                public const string UserSharepointdocumentlocation = "user_sharepointdocumentlocation";
                public const string UserSharepointsite = "user_sharepointsite";
                public const string UserSideloadedaiplugin = "user_sideloadedaiplugin";
                public const string UserSignal = "user_signal";
                public const string UserSignalregistration = "user_signalregistration";
                public const string UserSkill = "user_skill";
                public const string UserSlabase = "user_slabase";
                public const string UserSocialactivity = "user_socialactivity";
                public const string UserSolutioncomponentbatchconfiguration = "user_solutioncomponentbatchconfiguration";
                public const string UserStagesolutionupload = "user_stagesolutionupload";
                public const string UserSynapsedatabase = "user_synapsedatabase";
                public const string UserTag = "user_tag";
                public const string UserTaggedflowsession = "user_taggedflowsession";
                public const string UserTaggedprocess = "user_taggedprocess";
                public const string UserTask = "user_task";
                public const string UserTdsmetadata = "user_tdsmetadata";
                public const string UserToolinggateway = "user_toolinggateway";
                public const string UserToolinggatewaymcpserver = "user_toolinggatewaymcpserver";
                public const string UserTrait = "user_trait";
                public const string UserTraitregistration = "user_traitregistration";
                public const string UserUnstructuredfilesearchentity = "user_unstructuredfilesearchentity";
                public const string UserUnstructuredfilesearchrecord = "user_unstructuredfilesearchrecord";
                public const string UserUnstructuredfilesearchrecordstatus = "user_unstructuredfilesearchrecordstatus";
                public const string UserUntrackedemail = "user_untrackedemail";
                public const string UserUserapplicationmetadata = "user_userapplicationmetadata";
                public const string UserUserauthztracker = "user_userauthztracker";
                public const string UserUserform = "user_userform";
                public const string UserUserquery = "user_userquery";
                public const string UserUserqueryvisualizations = "user_userqueryvisualizations";
                public const string UserUxagentcomponent = "user_uxagentcomponent";
                public const string UserUxagentcomponentrevision = "user_uxagentcomponentrevision";
                public const string UserWorkflowbinary = "user_workflowbinary";
                public const string UserWorkflowmetadata = "user_workflowmetadata";
                public const string UserWorkqueue = "user_workqueue";
                public const string UserWorkqueueitem = "user_workqueueitem";
                public const string UserentityinstancedataOwningUser = "userentityinstancedata_owning_user";
                public const string UserentityinstancedataSystemuser = "userentityinstancedata_systemuser";
                public const string UserentityuisettingsOwningUser = "userentityuisettings_owning_user";
                public const string WebresourceCreatedby = "webresource_createdby";
                public const string WebresourceModifiedby = "webresource_modifiedby";
                public const string WorkflowCreatedby = "workflow_createdby";
                public const string WorkflowCreatedonbehalfby = "workflow_createdonbehalfby";
                public const string WorkflowDependencyCreatedby = "workflow_dependency_createdby";
                public const string WorkflowDependencyCreatedonbehalfby = "workflow_dependency_createdonbehalfby";
                public const string WorkflowDependencyModifiedby = "workflow_dependency_modifiedby";
                public const string WorkflowDependencyModifiedonbehalfby = "workflow_dependency_modifiedonbehalfby";
                public const string WorkflowLicensee = "Workflow_licensee";
                public const string WorkflowModifiedby = "workflow_modifiedby";
                public const string WorkflowModifiedonbehalfby = "workflow_modifiedonbehalfby";
                public const string WorkqueueitemProcessinguser = "workqueueitem_processinguser";
            }

            public static partial class ManyToOne
            {
                public const string BusinessUnitSystemUsers = "business_unit_system_users";
                public const string CalendarSystemUsers = "calendar_system_users";
                public const string LkSystemuserCreatedonbehalfby = "lk_systemuser_createdonbehalfby";
                public const string LkSystemuserEntityimage = "lk_systemuser_entityimage";
                public const string LkSystemuserModifiedonbehalfby = "lk_systemuser_modifiedonbehalfby";
                public const string LkSystemuserbaseCreatedby = "lk_systemuserbase_createdby";
                public const string LkSystemuserbaseModifiedby = "lk_systemuserbase_modifiedby";
                public const string MobileOfflineProfileSystemUser = "MobileOfflineProfile_SystemUser";
                public const string OrganizationSystemUsers = "organization_system_users";
                public const string PositionUsers = "position_users";
                public const string ProcessstageSystemusers = "processstage_systemusers";
                public const string QueueSystemUser = "queue_system_user";
                public const string SystemuserDefaultmailboxMailbox = "systemuser_defaultmailbox_mailbox";
                public const string TerritorySystemUsers = "territory_system_users";
                public const string TransactionCurrencySystemUser = "TransactionCurrency_SystemUser";
                public const string UserParentUser = "user_parent_user";
            }

            public static partial class ManyToMany
            {
                public const string MsdynFlowActionapprovalmodelrelationshipSystemuser = "msdyn_flow_actionapprovalmodelrelationship_systemuser";
                public const string MsdynFlowAwaitallactionmodelrelationshipUser = "msdyn_flow_awaitallactionmodelrelationship_user";
                public const string MsdynFlowAwaitallmodelrelationshipSystemuser = "msdyn_flow_awaitallmodelrelationship_systemuser";
                public const string MsdynFlowBasicapprovalmodelrelationshipSystemuser = "msdyn_flow_basicapprovalmodelrelationship_systemuser";
                public const string PowerpagecomponentWebroleSystemuser = "powerpagecomponent_webrole_systemuser";
                public const string QueuemembershipAssociation = "queuemembership_association";
                public const string SystemuserprofilesAssociation = "systemuserprofiles_association";
                public const string SystemuserrolesAssociation = "systemuserroles_association";
                public const string SystemusersyncmappingprofilesAssociation = "systemusersyncmappingprofiles_association";
                public const string TeammembershipAssociation = "teammembership_association";
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

        public static SystemUser Retrieve(IOrganizationService service, Guid id)
        {
            return Retrieve(service, id, new ColumnSet(true));
        }

        public static SystemUser Retrieve(IOrganizationService service, Guid id, ColumnSet columnSet)
        {
            return service.Retrieve("systemuser", id, columnSet).ToEntity<SystemUser>();
        }

        public SystemUser GetChangedEntity()
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
            return new SystemUser(Id) { Attributes = attr };
        }
        #endregion
    }

    #region Context
    public partial class DataContext
    {
        public IQueryable<SystemUser> SystemUserSet
        {
            get
            {
                return CreateQuery<SystemUser>();
            }
        }
    }
    #endregion
}
