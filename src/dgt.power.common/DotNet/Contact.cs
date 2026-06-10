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
	/// Person with whom a business unit has a relationship, such as customer, supplier, and colleague.
	/// </summary>
    [DataContract]
    [EntityLogicalName("contact")]
    [System.CodeDom.Compiler.GeneratedCode("dgtp", "2026")]
    [ExcludeFromCodeCoverage]
    public partial class Contact : Entity, INotifyPropertyChanging, INotifyPropertyChanged
    {
        #region ctor
        [DebuggerNonUserCode]
        public Contact() : this(false)
        {
        }
        [DebuggerNonUserCode]
        public Contact(bool trackChanges = false) : base(EntityLogicalName)
        {
            _trackChanges = trackChanges;
        }
        [DebuggerNonUserCode]
        public Contact(Guid id, bool trackChanges = false) : base(EntityLogicalName, id)
        {
            _trackChanges = trackChanges;
        }
        [DebuggerNonUserCode]
        public Contact(KeyAttributeCollection keyAttributes, bool trackChanges = false) : base(EntityLogicalName, keyAttributes)
        {
            _trackChanges = trackChanges;
        }
        [DebuggerNonUserCode]
        public Contact(string keyName, object keyValue, bool trackChanges = false) : base(EntityLogicalName, keyName, keyValue)
        {
            _trackChanges = trackChanges;
        }
        #endregion

        #region fields
        private readonly bool _trackChanges;
        private readonly Lazy<HashSet<string>> _changedProperties = new();
        #endregion

        #region consts
        public const string EntityLogicalName = "contact";
        public const string PrimaryNameAttribute = "fullname";
        public const int EntityTypeCode = 2;
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
        [AttributeLogicalName("contactid")]
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
                ContactId = value;
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
		/// Unique identifier for address 3.
		/// </summary>
        [AttributeLogicalName("address3_addressid")]
        public Guid? Address3AddressId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<Guid?>("address3_addressid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("address3_addressid", value);
                base.Id = value.HasValue ? value.Value : Guid.Empty;
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier of the contact.
		/// </summary>
        [AttributeLogicalName("contactid")]
        public Guid? ContactId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<Guid?>("contactid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("contactid", value);
                base.Id = value.HasValue ? value.Value : Guid.Empty;
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier of the account with which the contact is associated.
		/// </summary>
        [AttributeLogicalName("accountid")]
        public EntityReference? AccountId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<EntityReference?>("accountid");
            }
        }

        /// <summary>
		/// Select the contact's role within the company or sales process, such as decision maker, employee, or influencer.
		/// </summary>
        [AttributeLogicalName("accountrolecode")]
        public OptionSetValue? AccountRoleCode
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("accountrolecode");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("accountrolecode", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Select the primary address type.
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
		/// Type the city for the primary address.
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
		/// Type the country or region for the primary address.
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
		/// Type the county for the primary address.
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
		/// Type the fax number associated with the primary address.
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
		/// Select the freight terms for the primary address to make sure shipping orders are processed correctly.
		/// </summary>
        [AttributeLogicalName("address1_freighttermscode")]
        public OptionSetValue? Address1FreightTermsCode
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("address1_freighttermscode");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("address1_freighttermscode", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Type the latitude value for the primary address for use in mapping and other applications.
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
		/// Type the first line of the primary address.
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
		/// Type the second line of the primary address.
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
		/// Type the third line of the primary address.
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
		/// Type the longitude value for the primary address for use in mapping and other applications.
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
		/// Type a descriptive name for the primary address, such as Corporate Headquarters.
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
		/// Type the ZIP Code or postal code for the primary address.
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
		/// Type the post office box number of the primary address.
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
		/// Type the name of the main contact at the account's primary address.
		/// </summary>
        [AttributeLogicalName("address1_primarycontactname")]
        public string? Address1PrimaryContactName
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("address1_primarycontactname");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("address1_primarycontactname", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Select a shipping method for deliveries sent to this address.
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
		/// Type the state or province of the primary address.
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
		/// Type the main phone number associated with the primary address.
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
		/// Type a second phone number associated with the primary address.
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
		/// Type a third phone number associated with the primary address.
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
		/// Type the UPS zone of the primary address to make sure shipping charges are calculated correctly and deliveries are made promptly, if shipped by UPS.
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
		/// Select the time zone, or UTC offset, for this address so that other people can reference it when they contact someone at this address.
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
		/// Select the secondary address type.
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
		/// Type the city for the secondary address.
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
		/// Type the country or region for the secondary address.
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
		/// Type the county for the secondary address.
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
		/// Type the fax number associated with the secondary address.
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
		/// Select the freight terms for the secondary address to make sure shipping orders are processed correctly.
		/// </summary>
        [AttributeLogicalName("address2_freighttermscode")]
        public OptionSetValue? Address2FreightTermsCode
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("address2_freighttermscode");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("address2_freighttermscode", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Type the latitude value for the secondary address for use in mapping and other applications.
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
		/// Type the first line of the secondary address.
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
		/// Type the second line of the secondary address.
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
		/// Type the third line of the secondary address.
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
		/// Type the longitude value for the secondary address for use in mapping and other applications.
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
		/// Type a descriptive name for the secondary address, such as Corporate Headquarters.
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
		/// Type the ZIP Code or postal code for the secondary address.
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
		/// Type the post office box number of the secondary address.
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
		/// Type the name of the main contact at the account's secondary address.
		/// </summary>
        [AttributeLogicalName("address2_primarycontactname")]
        public string? Address2PrimaryContactName
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("address2_primarycontactname");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("address2_primarycontactname", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Select a shipping method for deliveries sent to this address.
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
		/// Type the state or province of the secondary address.
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
		/// Type the main phone number associated with the secondary address.
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
		/// Type a second phone number associated with the secondary address.
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
		/// Type a third phone number associated with the secondary address.
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
		/// Type the UPS zone of the secondary address to make sure shipping charges are calculated correctly and deliveries are made promptly, if shipped by UPS.
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
		/// Select the time zone, or UTC offset, for this address so that other people can reference it when they contact someone at this address.
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
		/// Select the third address type.
		/// </summary>
        [AttributeLogicalName("address3_addresstypecode")]
        public OptionSetValue? Address3AddressTypeCode
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("address3_addresstypecode");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("address3_addresstypecode", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Type the city for the 3rd address.
		/// </summary>
        [AttributeLogicalName("address3_city")]
        public string? Address3City
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("address3_city");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("address3_city", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Shows the complete third address.
		/// </summary>
        [AttributeLogicalName("address3_composite")]
        public string? Address3Composite
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("address3_composite");
            }
        }

        /// <summary>
		/// the country or region for the 3rd address.
		/// </summary>
        [AttributeLogicalName("address3_country")]
        public string? Address3Country
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("address3_country");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("address3_country", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Type the county for the third address.
		/// </summary>
        [AttributeLogicalName("address3_county")]
        public string? Address3County
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("address3_county");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("address3_county", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Type the fax number associated with the third address.
		/// </summary>
        [AttributeLogicalName("address3_fax")]
        public string? Address3Fax
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("address3_fax");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("address3_fax", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Select the freight terms for the third address to make sure shipping orders are processed correctly.
		/// </summary>
        [AttributeLogicalName("address3_freighttermscode")]
        public OptionSetValue? Address3FreightTermsCode
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("address3_freighttermscode");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("address3_freighttermscode", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Type the latitude value for the third address for use in mapping and other applications.
		/// </summary>
        [AttributeLogicalName("address3_latitude")]
        public double? Address3Latitude
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<double?>("address3_latitude");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("address3_latitude", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// the first line of the 3rd address.
		/// </summary>
        [AttributeLogicalName("address3_line1")]
        public string? Address3Line1
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("address3_line1");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("address3_line1", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// the second line of the 3rd address.
		/// </summary>
        [AttributeLogicalName("address3_line2")]
        public string? Address3Line2
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("address3_line2");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("address3_line2", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// the third line of the 3rd address.
		/// </summary>
        [AttributeLogicalName("address3_line3")]
        public string? Address3Line3
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("address3_line3");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("address3_line3", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Type the longitude value for the third address for use in mapping and other applications.
		/// </summary>
        [AttributeLogicalName("address3_longitude")]
        public double? Address3Longitude
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<double?>("address3_longitude");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("address3_longitude", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Type a descriptive name for the third address, such as Corporate Headquarters.
		/// </summary>
        [AttributeLogicalName("address3_name")]
        public string? Address3Name
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("address3_name");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("address3_name", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// the ZIP Code or postal code for the 3rd address.
		/// </summary>
        [AttributeLogicalName("address3_postalcode")]
        public string? Address3PostalCode
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("address3_postalcode");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("address3_postalcode", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// the post office box number of the 3rd address.
		/// </summary>
        [AttributeLogicalName("address3_postofficebox")]
        public string? Address3PostOfficeBox
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("address3_postofficebox");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("address3_postofficebox", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Type the name of the main contact at the account's third address.
		/// </summary>
        [AttributeLogicalName("address3_primarycontactname")]
        public string? Address3PrimaryContactName
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("address3_primarycontactname");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("address3_primarycontactname", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Select a shipping method for deliveries sent to this address.
		/// </summary>
        [AttributeLogicalName("address3_shippingmethodcode")]
        public OptionSetValue? Address3ShippingMethodCode
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("address3_shippingmethodcode");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("address3_shippingmethodcode", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// the state or province of the third address.
		/// </summary>
        [AttributeLogicalName("address3_stateorprovince")]
        public string? Address3StateOrProvince
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("address3_stateorprovince");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("address3_stateorprovince", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Type the main phone number associated with the third address.
		/// </summary>
        [AttributeLogicalName("address3_telephone1")]
        public string? Address3Telephone1
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("address3_telephone1");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("address3_telephone1", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Type a second phone number associated with the third address.
		/// </summary>
        [AttributeLogicalName("address3_telephone2")]
        public string? Address3Telephone2
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("address3_telephone2");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("address3_telephone2", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Type a third phone number associated with the primary address.
		/// </summary>
        [AttributeLogicalName("address3_telephone3")]
        public string? Address3Telephone3
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("address3_telephone3");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("address3_telephone3", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Type the UPS zone of the third address to make sure shipping charges are calculated correctly and deliveries are made promptly, if shipped by UPS.
		/// </summary>
        [AttributeLogicalName("address3_upszone")]
        public string? Address3UPSZone
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("address3_upszone");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("address3_upszone", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Select the time zone, or UTC offset, for this address so that other people can reference it when they contact someone at this address.
		/// </summary>
        [AttributeLogicalName("address3_utcoffset")]
        public int? Address3UTCOffset
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("address3_utcoffset");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("address3_utcoffset", value);
                OnPropertyChanged();
            }
        }

        
        [AttributeLogicalName("adx_confirmremovepassword")]
        public bool? AdxConfirmRemovePassword
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("adx_confirmremovepassword");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("adx_confirmremovepassword", value);
                OnPropertyChanged();
            }
        }

        
        [AttributeLogicalName("adx_createdbyipaddress")]
        public string? AdxCreatedByIPAddress
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("adx_createdbyipaddress");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("adx_createdbyipaddress", value);
                OnPropertyChanged();
            }
        }

        
        [AttributeLogicalName("adx_createdbyusername")]
        public string? AdxCreatedByUsername
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("adx_createdbyusername");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("adx_createdbyusername", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Shows the current count of failed password attempts for the contact.
		/// </summary>
        [AttributeLogicalName("adx_identity_accessfailedcount")]
        public int? AdxIdentityAccessfailedcount
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("adx_identity_accessfailedcount");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("adx_identity_accessfailedcount", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Determines if the email is confirmed by the contact.
		/// </summary>
        [AttributeLogicalName("adx_identity_emailaddress1confirmed")]
        public bool? AdxIdentityEmailaddress1confirmed
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("adx_identity_emailaddress1confirmed");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("adx_identity_emailaddress1confirmed", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates the last date and time the user successfully signed in to a portal.
		/// </summary>
        [AttributeLogicalName("adx_identity_lastsuccessfullogin")]
        public DateTime? AdxIdentityLastsuccessfullogin
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<DateTime?>("adx_identity_lastsuccessfullogin");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("adx_identity_lastsuccessfullogin", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates that the contact can no longer sign in to the portal using the local account.
		/// </summary>
        [AttributeLogicalName("adx_identity_locallogindisabled")]
        public bool? AdxIdentityLocallogindisabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("adx_identity_locallogindisabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("adx_identity_locallogindisabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Determines if this contact will track failed access attempts and become locked after too many failed attempts. To prevent the contact from becoming locked, you can disable this setting.
		/// </summary>
        [AttributeLogicalName("adx_identity_lockoutenabled")]
        public bool? AdxIdentityLockoutenabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("adx_identity_lockoutenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("adx_identity_lockoutenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Shows the moment in time when the locked contact becomes unlocked again.
		/// </summary>
        [AttributeLogicalName("adx_identity_lockoutenddate")]
        public DateTime? AdxIdentityLockoutenddate
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<DateTime?>("adx_identity_lockoutenddate");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("adx_identity_lockoutenddate", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Determines if web authentication is enabled for the contact.
		/// </summary>
        [AttributeLogicalName("adx_identity_logonenabled")]
        public bool? AdxIdentityLogonenabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("adx_identity_logonenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("adx_identity_logonenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Determines if the phone number is confirmed by the contact.
		/// </summary>
        [AttributeLogicalName("adx_identity_mobilephoneconfirmed")]
        public bool? AdxIdentityMobilephoneconfirmed
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("adx_identity_mobilephoneconfirmed");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("adx_identity_mobilephoneconfirmed", value);
                OnPropertyChanged();
            }
        }

        
        [AttributeLogicalName("adx_identity_newpassword")]
        public string? AdxIdentityNewpassword
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("adx_identity_newpassword");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("adx_identity_newpassword", value);
                OnPropertyChanged();
            }
        }

        
        [AttributeLogicalName("adx_identity_passwordhash")]
        public string? AdxIdentityPasswordhash
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("adx_identity_passwordhash");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("adx_identity_passwordhash", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// A token used to manage the web authentication session.
		/// </summary>
        [AttributeLogicalName("adx_identity_securitystamp")]
        public string? AdxIdentitySecuritystamp
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("adx_identity_securitystamp");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("adx_identity_securitystamp", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Determines if two-factor authentication is enabled for the contact.
		/// </summary>
        [AttributeLogicalName("adx_identity_twofactorenabled")]
        public bool? AdxIdentityTwofactorenabled
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("adx_identity_twofactorenabled");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("adx_identity_twofactorenabled", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Shows the user identity for local web authentication.
		/// </summary>
        [AttributeLogicalName("adx_identity_username")]
        public string? AdxIdentityUsername
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("adx_identity_username");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("adx_identity_username", value);
                OnPropertyChanged();
            }
        }

        
        [AttributeLogicalName("adx_modifiedbyipaddress")]
        public string? AdxModifiedByIPAddress
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("adx_modifiedbyipaddress");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("adx_modifiedbyipaddress", value);
                OnPropertyChanged();
            }
        }

        
        [AttributeLogicalName("adx_modifiedbyusername")]
        public string? AdxModifiedByUsername
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("adx_modifiedbyusername");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("adx_modifiedbyusername", value);
                OnPropertyChanged();
            }
        }

        
        [AttributeLogicalName("adx_organizationname")]
        public string? AdxOrganizationName
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("adx_organizationname");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("adx_organizationname", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// User’s preferred portal LCID
		/// </summary>
        [AttributeLogicalName("adx_preferredlcid")]
        public int? AdxPreferredlcid
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("adx_preferredlcid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("adx_preferredlcid", value);
                OnPropertyChanged();
            }
        }

        
        [AttributeLogicalName("adx_profilealert")]
        public bool? AdxProfilealert
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("adx_profilealert");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("adx_profilealert", value);
                OnPropertyChanged();
            }
        }

        
        [AttributeLogicalName("adx_profilealertdate")]
        public DateTime? AdxProfilealertdate
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<DateTime?>("adx_profilealertdate");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("adx_profilealertdate", value);
                OnPropertyChanged();
            }
        }

        
        [AttributeLogicalName("adx_profilealertinstructions")]
        public string? AdxProfilealertinstructions
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("adx_profilealertinstructions");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("adx_profilealertinstructions", value);
                OnPropertyChanged();
            }
        }

        
        [AttributeLogicalName("adx_profileisanonymous")]
        public bool? AdxProfileIsAnonymous
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("adx_profileisanonymous");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("adx_profileisanonymous", value);
                OnPropertyChanged();
            }
        }

        
        [AttributeLogicalName("adx_profilelastactivity")]
        public DateTime? AdxProfileLastActivity
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<DateTime?>("adx_profilelastactivity");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("adx_profilelastactivity", value);
                OnPropertyChanged();
            }
        }

        
        [AttributeLogicalName("adx_profilemodifiedon")]
        public DateTime? AdxProfilemodifiedon
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<DateTime?>("adx_profilemodifiedon");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("adx_profilemodifiedon", value);
                OnPropertyChanged();
            }
        }

        
        [AttributeLogicalName("adx_publicprofilecopy")]
        public string? AdxPublicProfileCopy
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("adx_publicprofilecopy");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("adx_publicprofilecopy", value);
                OnPropertyChanged();
            }
        }

        
        [AttributeLogicalName("adx_timezone")]
        public int? AdxTimeZone
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("adx_timezone");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("adx_timezone", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// For system use only.
		/// </summary>
        [AttributeLogicalName("aging30")]
        public Money? Aging30
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<Money?>("aging30");
            }
        }

        /// <summary>
		/// Shows the Aging 30 field converted to the system's default base currency. The calculations use the exchange rate specified in the Currencies area.
		/// </summary>
        [AttributeLogicalName("aging30_base")]
        public Money? Aging30Base
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<Money?>("aging30_base");
            }
        }

        /// <summary>
		/// For system use only.
		/// </summary>
        [AttributeLogicalName("aging60")]
        public Money? Aging60
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<Money?>("aging60");
            }
        }

        /// <summary>
		/// Shows the Aging 60 field converted to the system's default base currency. The calculations use the exchange rate specified in the Currencies area.
		/// </summary>
        [AttributeLogicalName("aging60_base")]
        public Money? Aging60Base
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<Money?>("aging60_base");
            }
        }

        /// <summary>
		/// For system use only.
		/// </summary>
        [AttributeLogicalName("aging90")]
        public Money? Aging90
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<Money?>("aging90");
            }
        }

        /// <summary>
		/// Shows the Aging 90 field converted to the system's default base currency. The calculations use the exchange rate specified in the Currencies area.
		/// </summary>
        [AttributeLogicalName("aging90_base")]
        public Money? Aging90Base
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<Money?>("aging90_base");
            }
        }

        /// <summary>
		/// Enter the date of the contact's wedding or service anniversary for use in customer gift programs or other communications.
		/// </summary>
        [AttributeLogicalName("anniversary")]
        public DateTime? Anniversary
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<DateTime?>("anniversary");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("anniversary", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Type the contact's annual income for use in profiling and financial analysis.
		/// </summary>
        [AttributeLogicalName("annualincome")]
        public Money? AnnualIncome
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<Money?>("annualincome");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("annualincome", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Shows the Annual Income field converted to the system's default base currency. The calculations use the exchange rate specified in the Currencies area.
		/// </summary>
        [AttributeLogicalName("annualincome_base")]
        public Money? AnnualIncomeBase
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<Money?>("annualincome_base");
            }
        }

        /// <summary>
		/// Type the name of the contact's assistant.
		/// </summary>
        [AttributeLogicalName("assistantname")]
        public string? AssistantName
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("assistantname");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("assistantname", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Type the phone number for the contact's assistant.
		/// </summary>
        [AttributeLogicalName("assistantphone")]
        public string? AssistantPhone
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("assistantphone");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("assistantphone", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Enter the contact's birthday for use in customer gift programs or other communications.
		/// </summary>
        [AttributeLogicalName("birthdate")]
        public DateTime? BirthDate
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<DateTime?>("birthdate");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("birthdate", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Type a second business phone number for this contact.
		/// </summary>
        [AttributeLogicalName("business2")]
        public string? Business2
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("business2");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("business2", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Type a callback phone number for this contact.
		/// </summary>
        [AttributeLogicalName("callback")]
        public string? Callback
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("callback");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("callback", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Type the names of the contact's children for reference in communications and client programs.
		/// </summary>
        [AttributeLogicalName("childrensnames")]
        public string? ChildrensNames
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("childrensnames");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("childrensnames", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Type the company phone of the contact.
		/// </summary>
        [AttributeLogicalName("company")]
        public string? Company
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("company");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("company", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Shows who created the record.
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
		/// Shows the external party who created the record.
		/// </summary>
        [AttributeLogicalName("createdbyexternalparty")]
        public EntityReference? CreatedByExternalParty
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<EntityReference?>("createdbyexternalparty");
            }
        }

        /// <summary>
		/// Shows the date and time when the record was created. The date and time are displayed in the time zone selected in Microsoft Dynamics 365 options.
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
		/// Shows who created the record on behalf of another user.
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
		/// Type the credit limit of the contact for reference when you address invoice and accounting issues with the customer.
		/// </summary>
        [AttributeLogicalName("creditlimit")]
        public Money? CreditLimit
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<Money?>("creditlimit");
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
		/// Shows the Credit Limit field converted to the system's default base currency for reporting purposes. The calculations use the exchange rate specified in the Currencies area.
		/// </summary>
        [AttributeLogicalName("creditlimit_base")]
        public Money? CreditLimitBase
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<Money?>("creditlimit_base");
            }
        }

        /// <summary>
		/// Select whether the contact is on a credit hold, for reference when addressing invoice and accounting issues.
		/// </summary>
        [AttributeLogicalName("creditonhold")]
        public bool? CreditOnHold
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("creditonhold");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("creditonhold", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Select the size of the contact's company for segmentation and reporting purposes.
		/// </summary>
        [AttributeLogicalName("customersizecode")]
        public OptionSetValue? CustomerSizeCode
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("customersizecode");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("customersizecode", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Select the category that best describes the relationship between the contact and your organization.
		/// </summary>
        [AttributeLogicalName("customertypecode")]
        public OptionSetValue? CustomerTypeCode
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("customertypecode");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("customertypecode", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Type the department or business unit where the contact works in the parent company or business.
		/// </summary>
        [AttributeLogicalName("department")]
        public string? Department
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("department");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("department", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Type additional information to describe the contact, such as an excerpt from the company's website.
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
		/// Select whether the contact accepts bulk email sent through marketing campaigns or quick campaigns. If Do Not Allow is selected, the contact can be added to marketing lists, but will be excluded from the email.
		/// </summary>
        [AttributeLogicalName("donotbulkemail")]
        public bool? DoNotBulkEMail
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("donotbulkemail");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("donotbulkemail", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Select whether the contact accepts bulk postal mail sent through marketing campaigns or quick campaigns. If Do Not Allow is selected, the contact can be added to marketing lists, but will be excluded from the letters.
		/// </summary>
        [AttributeLogicalName("donotbulkpostalmail")]
        public bool? DoNotBulkPostalMail
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("donotbulkpostalmail");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("donotbulkpostalmail", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Select whether the contact allows direct email sent from Microsoft Dynamics 365. If Do Not Allow is selected, Microsoft Dynamics 365 will not send the email.
		/// </summary>
        [AttributeLogicalName("donotemail")]
        public bool? DoNotEMail
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("donotemail");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("donotemail", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Select whether the contact allows faxes. If Do Not Allow is selected, the contact will be excluded from any fax activities distributed in marketing campaigns.
		/// </summary>
        [AttributeLogicalName("donotfax")]
        public bool? DoNotFax
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("donotfax");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("donotfax", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Select whether the contact accepts phone calls. If Do Not Allow is selected, the contact will be excluded from any phone call activities distributed in marketing campaigns.
		/// </summary>
        [AttributeLogicalName("donotphone")]
        public bool? DoNotPhone
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("donotphone");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("donotphone", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Select whether the contact allows direct mail. If Do Not Allow is selected, the contact will be excluded from letter activities distributed in marketing campaigns.
		/// </summary>
        [AttributeLogicalName("donotpostalmail")]
        public bool? DoNotPostalMail
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("donotpostalmail");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("donotpostalmail", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Select whether the contact accepts marketing materials, such as brochures or catalogs. Contacts that opt out can be excluded from marketing initiatives.
		/// </summary>
        [AttributeLogicalName("donotsendmm")]
        public bool? DoNotSendMM
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("donotsendmm");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("donotsendmm", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Select the contact's highest level of education for use in segmentation and analysis.
		/// </summary>
        [AttributeLogicalName("educationcode")]
        public OptionSetValue? EducationCode
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("educationcode");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("educationcode", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Type the primary email address for the contact.
		/// </summary>
        [AttributeLogicalName("emailaddress1")]
        public string? EMailAddress1
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("emailaddress1");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("emailaddress1", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Type the secondary email address for the contact.
		/// </summary>
        [AttributeLogicalName("emailaddress2")]
        public string? EMailAddress2
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("emailaddress2");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("emailaddress2", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Type an alternate email address for the contact.
		/// </summary>
        [AttributeLogicalName("emailaddress3")]
        public string? EMailAddress3
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("emailaddress3");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("emailaddress3", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Type the employee ID or number for the contact for reference in orders, service cases, or other communications with the contact's organization.
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
		/// Shows the conversion rate of the record's currency. The exchange rate is used to convert all money fields in the record from the local currency to the system's default currency.
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
		/// Identifier for an external user.
		/// </summary>
        [AttributeLogicalName("externaluseridentifier")]
        public string? ExternalUserIdentifier
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("externaluseridentifier");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("externaluseridentifier", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Select the marital status of the contact for reference in follow-up phone calls and other communications.
		/// </summary>
        [AttributeLogicalName("familystatuscode")]
        public OptionSetValue? FamilyStatusCode
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("familystatuscode");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("familystatuscode", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Type the fax number for the contact.
		/// </summary>
        [AttributeLogicalName("fax")]
        public string? Fax
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("fax");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("fax", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Type the contact's first name to make sure the contact is addressed correctly in sales calls, email, and marketing campaigns.
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
		/// Information about whether to allow following email activity like opens, attachment views and link clicks for emails sent to the contact.
		/// </summary>
        [AttributeLogicalName("followemail")]
        public bool? FollowEmail
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("followemail");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("followemail", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Type the URL for the contact's FTP site to enable users to access data and share documents.
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
		/// Combines and shows the contact's first and last names so that the full name can be displayed in views and reports.
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
		/// Select the contact's gender to make sure the contact is addressed correctly in sales calls, email, and marketing campaigns.
		/// </summary>
        [AttributeLogicalName("gendercode")]
        public OptionSetValue? GenderCode
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("gendercode");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("gendercode", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Type the passport number or other government ID for the contact for use in documents or reports.
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
		/// Select whether the contact has any children for reference in follow-up phone calls and other communications.
		/// </summary>
        [AttributeLogicalName("haschildrencode")]
        public OptionSetValue? HasChildrenCode
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("haschildrencode");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("haschildrencode", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Type a second home phone number for this contact.
		/// </summary>
        [AttributeLogicalName("home2")]
        public string? Home2
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("home2");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("home2", value);
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
		/// Select whether the contact exists in a separate accounting or other system, such as Microsoft Dynamics GP or another ERP database, for use in integration processes.
		/// </summary>
        [AttributeLogicalName("isbackofficecustomer")]
        public bool? IsBackofficeCustomer
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("isbackofficecustomer");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("isbackofficecustomer", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Type the job title of the contact to make sure the contact is addressed correctly in sales calls, email, and marketing campaigns.
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
		/// Type the contact's last name to make sure the contact is addressed correctly in sales calls, email, and marketing campaigns.
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
		/// Contains the date and time stamp of the last on hold time.
		/// </summary>
        [AttributeLogicalName("lastonholdtime")]
        public DateTime? LastOnHoldTime
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<DateTime?>("lastonholdtime");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("lastonholdtime", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Shows the date when the contact was last included in a marketing campaign or quick campaign.
		/// </summary>
        [AttributeLogicalName("lastusedincampaign")]
        public DateTime? LastUsedInCampaign
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<DateTime?>("lastusedincampaign");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("lastusedincampaign", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Select the primary marketing source that directed the contact to your organization.
		/// </summary>
        [AttributeLogicalName("leadsourcecode")]
        public OptionSetValue? LeadSourceCode
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("leadsourcecode");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("leadsourcecode", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Type the name of the contact's manager for use in escalating issues or other follow-up communications with the contact.
		/// </summary>
        [AttributeLogicalName("managername")]
        public string? ManagerName
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("managername");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("managername", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Type the phone number for the contact's manager.
		/// </summary>
        [AttributeLogicalName("managerphone")]
        public string? ManagerPhone
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("managerphone");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("managerphone", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Whether is only for marketing
		/// </summary>
        [AttributeLogicalName("marketingonly")]
        public bool? MarketingOnly
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("marketingonly");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("marketingonly", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier of the master contact for merge.
		/// </summary>
        [AttributeLogicalName("masterid")]
        public EntityReference? MasterId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<EntityReference?>("masterid");
            }
        }

        /// <summary>
		/// Shows whether the account has been merged with a master contact.
		/// </summary>
        [AttributeLogicalName("merged")]
        public bool? Merged
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("merged");
            }
        }

        /// <summary>
		/// Type the contact's middle name or initial to make sure the contact is addressed correctly.
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
		/// Type the mobile phone number for the contact.
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
		/// Shows who last updated the record.
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
		/// Shows the external party who modified the record.
		/// </summary>
        [AttributeLogicalName("modifiedbyexternalparty")]
        public EntityReference? ModifiedByExternalParty
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<EntityReference?>("modifiedbyexternalparty");
            }
        }

        /// <summary>
		/// Shows the date and time when the record was last updated. The date and time are displayed in the time zone selected in Microsoft Dynamics 365 options.
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
		/// Shows who last updated the record on behalf of another user.
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
		/// Unique identifier for Account associated with Contact.
		/// </summary>
        [AttributeLogicalName("msa_managingpartnerid")]
        public EntityReference? MsaManagingpartnerid
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<EntityReference?>("msa_managingpartnerid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("msa_managingpartnerid", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates that the contact has opted out of web tracking.
		/// </summary>
        [AttributeLogicalName("msdyn_disablewebtracking")]
        public bool? MsdynDisablewebtracking
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("msdyn_disablewebtracking");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("msdyn_disablewebtracking", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates that the contact is considered a minor in their jurisdiction.
		/// </summary>
        [AttributeLogicalName("msdyn_isminor")]
        public bool? MsdynIsminor
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("msdyn_isminor");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("msdyn_isminor", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates that the contact is considered a minor in their jurisdiction and has parental consent.
		/// </summary>
        [AttributeLogicalName("msdyn_isminorwithparentalconsent")]
        public bool? MsdynIsminorwithparentalconsent
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("msdyn_isminorwithparentalconsent");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("msdyn_isminorwithparentalconsent", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Indicates the date and time that the person agreed to the portal terms and conditions.
		/// </summary>
        [AttributeLogicalName("msdyn_portaltermsagreementdate")]
        public DateTime? MsdynPortaltermsagreementdate
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<DateTime?>("msdyn_portaltermsagreementdate");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("msdyn_portaltermsagreementdate", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// User’s preferred portal language
		/// </summary>
        [AttributeLogicalName("mspp_userpreferredlcid")]
        public OptionSetValue? MsppUserpreferredlcid
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("mspp_userpreferredlcid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("mspp_userpreferredlcid", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Type the contact's nickname.
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
		/// Type the number of children the contact has for reference in follow-up phone calls and other communications.
		/// </summary>
        [AttributeLogicalName("numberofchildren")]
        public int? NumberOfChildren
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("numberofchildren");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("numberofchildren", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Shows how long, in minutes, that the record was on hold.
		/// </summary>
        [AttributeLogicalName("onholdtime")]
        public int? OnHoldTime
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<int?>("onholdtime");
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
		/// Enter the user or team who is assigned to manage the record. This field is updated every time the record is assigned to a different user.
		/// </summary>
        [AttributeLogicalName("ownerid")]
        public EntityReference? OwnerId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<EntityReference?>("ownerid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("ownerid", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier of the business unit that owns the contact.
		/// </summary>
        [AttributeLogicalName("owningbusinessunit")]
        public EntityReference? OwningBusinessUnit
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<EntityReference?>("owningbusinessunit");
            }
        }

        /// <summary>
		/// Unique identifier of the team who owns the contact.
		/// </summary>
        [AttributeLogicalName("owningteam")]
        public EntityReference? OwningTeam
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<EntityReference?>("owningteam");
            }
        }

        /// <summary>
		/// Unique identifier of the user who owns the contact.
		/// </summary>
        [AttributeLogicalName("owninguser")]
        public EntityReference? OwningUser
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<EntityReference?>("owninguser");
            }
        }

        /// <summary>
		/// Type the pager number for the contact.
		/// </summary>
        [AttributeLogicalName("pager")]
        public string? Pager
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("pager");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("pager", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Unique identifier of the parent contact.
		/// </summary>
        [AttributeLogicalName("parentcontactid")]
        public EntityReference? ParentContactId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<EntityReference?>("parentcontactid");
            }
        }

        /// <summary>
		/// Select the parent account or parent contact for the contact to provide a quick link to additional details, such as financial information, activities, and opportunities.
		/// </summary>
        [AttributeLogicalName("parentcustomerid")]
        public EntityReference? ParentCustomerId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<EntityReference?>("parentcustomerid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("parentcustomerid", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Shows whether the contact participates in workflow rules.
		/// </summary>
        [AttributeLogicalName("participatesinworkflow")]
        public bool? ParticipatesInWorkflow
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<bool?>("participatesinworkflow");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("participatesinworkflow", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Select the payment terms to indicate when the customer needs to pay the total amount.
		/// </summary>
        [AttributeLogicalName("paymenttermscode")]
        public OptionSetValue? PaymentTermsCode
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("paymenttermscode");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("paymenttermscode", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Select the preferred day of the week for service appointments.
		/// </summary>
        [AttributeLogicalName("preferredappointmentdaycode")]
        public OptionSetValue? PreferredAppointmentDayCode
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("preferredappointmentdaycode");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("preferredappointmentdaycode", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Select the preferred time of day for service appointments.
		/// </summary>
        [AttributeLogicalName("preferredappointmenttimecode")]
        public OptionSetValue? PreferredAppointmentTimeCode
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("preferredappointmenttimecode");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("preferredappointmenttimecode", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Select the preferred method of contact.
		/// </summary>
        [AttributeLogicalName("preferredcontactmethodcode")]
        public OptionSetValue? PreferredContactMethodCode
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("preferredcontactmethodcode");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("preferredcontactmethodcode", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Choose the regular or preferred customer service representative for reference when scheduling service activities for the contact.
		/// </summary>
        [AttributeLogicalName("preferredsystemuserid")]
        public EntityReference? PreferredSystemUserId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<EntityReference?>("preferredsystemuserid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("preferredsystemuserid", value);
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
		/// Type the salutation of the contact to make sure the contact is addressed correctly in sales calls, email messages, and marketing campaigns.
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
		/// Select a shipping method for deliveries sent to this address.
		/// </summary>
        [AttributeLogicalName("shippingmethodcode")]
        public OptionSetValue? ShippingMethodCode
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("shippingmethodcode");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("shippingmethodcode", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Choose the service level agreement (SLA) that you want to apply to the Contact record.
		/// </summary>
        [AttributeLogicalName("slaid")]
        public EntityReference? SLAId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<EntityReference?>("slaid");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("slaid", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Last SLA that was applied to this case. This field is for internal use only.
		/// </summary>
        [AttributeLogicalName("slainvokedid")]
        public EntityReference? SLAInvokedId
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<EntityReference?>("slainvokedid");
            }
        }

        /// <summary>
		/// Type the name of the contact's spouse or partner for reference during calls, events, or other communications with the contact.
		/// </summary>
        [AttributeLogicalName("spousesname")]
        public string? SpousesName
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("spousesname");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("spousesname", value);
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
		/// Shows whether the contact is active or inactive. Inactive contacts are read-only and can't be edited unless they are reactivated.
		/// </summary>
        [AttributeLogicalName("statecode")]
        public OptionSetValue? StateCode
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("statecode");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("statecode", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Select the contact's status.
		/// </summary>
        [AttributeLogicalName("statuscode")]
        public OptionSetValue? StatusCode
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("statuscode");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("statuscode", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// For internal use only.
		/// </summary>
        [AttributeLogicalName("subscriptionid")]
        public Guid? SubscriptionId
        {
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("subscriptionid", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Type the suffix used in the contact's name, such as Jr. or Sr. to make sure the contact is addressed correctly in sales calls, email, and marketing campaigns.
		/// </summary>
        [AttributeLogicalName("suffix")]
        public string? Suffix
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("suffix");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("suffix", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Type the main phone number for this contact.
		/// </summary>
        [AttributeLogicalName("telephone1")]
        public string? Telephone1
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("telephone1");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("telephone1", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Type a second phone number for this contact.
		/// </summary>
        [AttributeLogicalName("telephone2")]
        public string? Telephone2
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("telephone2");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("telephone2", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Type a third phone number for this contact.
		/// </summary>
        [AttributeLogicalName("telephone3")]
        public string? Telephone3
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("telephone3");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("telephone3", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Select a region or territory for the contact for use in segmentation and analysis.
		/// </summary>
        [AttributeLogicalName("territorycode")]
        public OptionSetValue? TerritoryCode
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<OptionSetValue?>("territorycode");
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetAttributeValue("territorycode", value);
                OnPropertyChanged();
            }
        }

        /// <summary>
		/// Total time spent for emails (read and write) and meetings by me in relation to the contact record.
		/// </summary>
        [AttributeLogicalName("timespentbymeonemailandmeetings")]
        public string? TimeSpentByMeOnEmailAndMeetings
        {
            [DebuggerNonUserCode]
            get
            {
                return GetAttributeValue<string?>("timespentbymeonemailandmeetings");
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
		/// Choose the local currency for the record to make sure budgets are reported in the correct currency.
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
		/// Version number of the contact.
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
		/// Type the contact's professional or personal website or blog URL.
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
		/// Type the phonetic spelling of the contact's first name, if the name is specified in Japanese, to make sure the name is pronounced correctly in phone calls with the contact.
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
		/// Shows the combined Yomi first and last names of the contact so that the full phonetic name can be displayed in views and reports.
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
		/// Type the phonetic spelling of the contact's last name, if the name is specified in Japanese, to make sure the name is pronounced correctly in phone calls with the contact.
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
		/// Type the phonetic spelling of the contact's middle name, if the name is specified in Japanese, to make sure the name is pronounced correctly in phone calls with the contact.
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
        /// 1:N account_primary_contact
        /// </summary>
        [RelationshipSchemaName("account_primary_contact")]
        public IEnumerable<Account> AccountPrimaryContact
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<Account>("account_primary_contact", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("account_primary_contact", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N Contact_AsyncOperations
        /// </summary>
        [RelationshipSchemaName("Contact_AsyncOperations")]
        public IEnumerable<AsyncOperation> ContactAsyncOperations
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<AsyncOperation>("Contact_AsyncOperations", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("Contact_AsyncOperations", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N contact_customer_contacts
        /// </summary>
        [RelationshipSchemaName("contact_customer_contacts")]
        public IEnumerable<Contact> ContactCustomerContacts
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<Contact>("contact_customer_contacts", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("contact_customer_contacts", null, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 1:N contact_master_contact
        /// </summary>
        [RelationshipSchemaName("contact_master_contact")]
        public IEnumerable<Contact> ContactMasterContact
        {
            [DebuggerNonUserCode]
            get
            {
                return GetRelatedEntities<Contact>("contact_master_contact", null);
            }
            [DebuggerNonUserCode]
            set
            {
                OnPropertyChanging();
                SetRelatedEntities("contact_master_contact", null, value);
                OnPropertyChanged();
            }
        }
        #endregion

        #region Options
        public static partial class Options
        {
            public struct AccountRoleCode
            {
                public const int DecisionMaker = 1;
                public const int Employee = 2;
                public const int Influencer = 3;
            }
            public struct Address1AddressTypeCode
            {
                public const int BillTo = 1;
                public const int ShipTo = 2;
                public const int Primary = 3;
                public const int Other = 4;
            }
            public struct Address1FreightTermsCode
            {
                public const int FOB = 1;
                public const int NoCharge = 2;
            }
            public struct Address1ShippingMethodCode
            {
                public const int Airborne = 1;
                public const int DHL = 2;
                public const int FedEx = 3;
                public const int UPS = 4;
                public const int PostalMail = 5;
                public const int FullLoad = 6;
                public const int WillCall = 7;
            }
            public struct Address2AddressTypeCode
            {
                public const int DefaultValue = 1;
            }
            public struct Address2FreightTermsCode
            {
                public const int DefaultValue = 1;
            }
            public struct Address2ShippingMethodCode
            {
                public const int DefaultValue = 1;
            }
            public struct Address3AddressTypeCode
            {
                public const int DefaultValue = 1;
            }
            public struct Address3FreightTermsCode
            {
                public const int DefaultValue = 1;
            }
            public struct Address3ShippingMethodCode
            {
                public const int DefaultValue = 1;
            }
            public struct AdxConfirmRemovePassword
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct AdxIdentityEmailaddress1confirmed
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct AdxIdentityLocallogindisabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct AdxIdentityLockoutenabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct AdxIdentityLogonenabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct AdxIdentityMobilephoneconfirmed
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct AdxIdentityTwofactorenabled
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct AdxProfilealert
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct AdxProfileIsAnonymous
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct CreditOnHold
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct CustomerSizeCode
            {
                public const int DefaultValue = 1;
            }
            public struct CustomerTypeCode
            {
                public const int DefaultValue = 1;
            }
            public struct DoNotBulkEMail
            {
                public const bool Allow = false;
                public const bool DoNotAllow = true;
            }
            public struct DoNotBulkPostalMail
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct DoNotEMail
            {
                public const bool Allow = false;
                public const bool DoNotAllow = true;
            }
            public struct DoNotFax
            {
                public const bool Allow = false;
                public const bool DoNotAllow = true;
            }
            public struct DoNotPhone
            {
                public const bool Allow = false;
                public const bool DoNotAllow = true;
            }
            public struct DoNotPostalMail
            {
                public const bool Allow = false;
                public const bool DoNotAllow = true;
            }
            public struct DoNotSendMM
            {
                public const bool Send = false;
                public const bool DoNotSend = true;
            }
            public struct EducationCode
            {
                public const int DefaultValue = 1;
            }
            public struct FamilyStatusCode
            {
                public const int Single = 1;
                public const int Married = 2;
                public const int Divorced = 3;
                public const int Widowed = 4;
            }
            public struct FollowEmail
            {
                public const bool DoNotAllow = false;
                public const bool Allow = true;
            }
            public struct GenderCode
            {
                public const int Male = 1;
                public const int Female = 2;
            }
            public struct HasChildrenCode
            {
                public const int DefaultValue = 1;
            }
            public struct IsBackofficeCustomer
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct LeadSourceCode
            {
                public const int DefaultValue = 1;
            }
            public struct MarketingOnly
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct Merged
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct MsdynDisablewebtracking
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct MsdynIsminor
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct MsdynIsminorwithparentalconsent
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct MsppUserpreferredlcid
            {
                public const int Arabic = 1025;
                public const int BulgarianBulgaria = 1026;
                public const int CatalanCatalan = 1027;
                public const int ChineseTraditional = 1028;
                public const int CzechCzechRepublic = 1029;
                public const int DanishDenmark = 1030;
                public const int GermanGermany = 1031;
                public const int GreekGreece = 1032;
                public const int English = 1033;
                public const int FinnishFinland = 1035;
                public const int FrenchFrance = 1036;
                public const int Hebrew = 1037;
                public const int HungarianHungary = 1038;
                public const int ItalianItaly = 1040;
                public const int JapaneseJapan = 1041;
                public const int KoreanKorea = 1042;
                public const int DutchNetherlands = 1043;
                public const int NorwegianBokmLNorway = 1044;
                public const int PolishPoland = 1045;
                public const int PortugueseBrazil = 1046;
                public const int RomanianRomania = 1048;
                public const int RussianRussia = 1049;
                public const int CroatianCroatia = 1050;
                public const int SlovakSlovakia = 1051;
                public const int SwedishSweden = 1053;
                public const int ThaiThailand = 1054;
                public const int TurkishTürkiye = 1055;
                public const int IndonesianIndonesia = 1057;
                public const int UkrainianUkraine = 1058;
                public const int SlovenianSlovenia = 1060;
                public const int EstonianEstonia = 1061;
                public const int LatvianLatvia = 1062;
                public const int LithuanianLithuania = 1063;
                public const int VietnameseVietnam = 1066;
                public const int BasqueBasque = 1069;
                public const int HindiIndia = 1081;
                public const int MalayMalaysia = 1086;
                public const int KazakhKazakhstan = 1087;
                public const int GalicianSpain = 1110;
                public const int ChineseChina = 2052;
                public const int PortuguesePortugal = 2070;
                public const int SerbianLatinSerbia = 2074;
                public const int ChineseHongKongSAR = 3076;
                public const int SpanishTraditionalSortSpain = 3082;
                public const int SerbianCyrillicSerbia = 3098;
            }
            public struct ParticipatesInWorkflow
            {
                public const bool No = false;
                public const bool Yes = true;
            }
            public struct PaymentTermsCode
            {
                public const int Net30 = 1;
                public const int _2Percent10Net30 = 2;
                public const int Net45 = 3;
                public const int Net60 = 4;
            }
            public struct PreferredAppointmentDayCode
            {
                public const int Sunday = 0;
                public const int Monday = 1;
                public const int Tuesday = 2;
                public const int Wednesday = 3;
                public const int Thursday = 4;
                public const int Friday = 5;
                public const int Saturday = 6;
            }
            public struct PreferredAppointmentTimeCode
            {
                public const int Morning = 1;
                public const int Afternoon = 2;
                public const int Evening = 3;
            }
            public struct PreferredContactMethodCode
            {
                public const int Any = 1;
                public const int Email = 2;
                public const int Phone = 3;
                public const int Fax = 4;
                public const int Mail = 5;
            }
            public struct ShippingMethodCode
            {
                public const int DefaultValue = 1;
            }
            public struct StateCode
            {
                public const int Active = 0;
                public const int Inactive = 1;
            }
            public struct StatusCode
            {
                public const int Active = 1;
                public const int Inactive = 2;
            }
            public struct TerritoryCode
            {
                public const int DefaultValue = 1;
            }
        }
        #endregion

        #region LogicalNames
        public static partial class LogicalNames
        {
            public const string Address1AddressId = "address1_addressid";
            public const string Address2AddressId = "address2_addressid";
            public const string Address3AddressId = "address3_addressid";
            public const string ContactId = "contactid";
            public const string AccountId = "accountid";
            public const string AccountRoleCode = "accountrolecode";
            public const string Address1AddressTypeCode = "address1_addresstypecode";
            public const string Address1City = "address1_city";
            public const string Address1Composite = "address1_composite";
            public const string Address1Country = "address1_country";
            public const string Address1County = "address1_county";
            public const string Address1Fax = "address1_fax";
            public const string Address1FreightTermsCode = "address1_freighttermscode";
            public const string Address1Latitude = "address1_latitude";
            public const string Address1Line1 = "address1_line1";
            public const string Address1Line2 = "address1_line2";
            public const string Address1Line3 = "address1_line3";
            public const string Address1Longitude = "address1_longitude";
            public const string Address1Name = "address1_name";
            public const string Address1PostalCode = "address1_postalcode";
            public const string Address1PostOfficeBox = "address1_postofficebox";
            public const string Address1PrimaryContactName = "address1_primarycontactname";
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
            public const string Address2FreightTermsCode = "address2_freighttermscode";
            public const string Address2Latitude = "address2_latitude";
            public const string Address2Line1 = "address2_line1";
            public const string Address2Line2 = "address2_line2";
            public const string Address2Line3 = "address2_line3";
            public const string Address2Longitude = "address2_longitude";
            public const string Address2Name = "address2_name";
            public const string Address2PostalCode = "address2_postalcode";
            public const string Address2PostOfficeBox = "address2_postofficebox";
            public const string Address2PrimaryContactName = "address2_primarycontactname";
            public const string Address2ShippingMethodCode = "address2_shippingmethodcode";
            public const string Address2StateOrProvince = "address2_stateorprovince";
            public const string Address2Telephone1 = "address2_telephone1";
            public const string Address2Telephone2 = "address2_telephone2";
            public const string Address2Telephone3 = "address2_telephone3";
            public const string Address2UPSZone = "address2_upszone";
            public const string Address2UTCOffset = "address2_utcoffset";
            public const string Address3AddressTypeCode = "address3_addresstypecode";
            public const string Address3City = "address3_city";
            public const string Address3Composite = "address3_composite";
            public const string Address3Country = "address3_country";
            public const string Address3County = "address3_county";
            public const string Address3Fax = "address3_fax";
            public const string Address3FreightTermsCode = "address3_freighttermscode";
            public const string Address3Latitude = "address3_latitude";
            public const string Address3Line1 = "address3_line1";
            public const string Address3Line2 = "address3_line2";
            public const string Address3Line3 = "address3_line3";
            public const string Address3Longitude = "address3_longitude";
            public const string Address3Name = "address3_name";
            public const string Address3PostalCode = "address3_postalcode";
            public const string Address3PostOfficeBox = "address3_postofficebox";
            public const string Address3PrimaryContactName = "address3_primarycontactname";
            public const string Address3ShippingMethodCode = "address3_shippingmethodcode";
            public const string Address3StateOrProvince = "address3_stateorprovince";
            public const string Address3Telephone1 = "address3_telephone1";
            public const string Address3Telephone2 = "address3_telephone2";
            public const string Address3Telephone3 = "address3_telephone3";
            public const string Address3UPSZone = "address3_upszone";
            public const string Address3UTCOffset = "address3_utcoffset";
            public const string AdxConfirmRemovePassword = "adx_confirmremovepassword";
            public const string AdxCreatedByIPAddress = "adx_createdbyipaddress";
            public const string AdxCreatedByUsername = "adx_createdbyusername";
            public const string AdxIdentityAccessfailedcount = "adx_identity_accessfailedcount";
            public const string AdxIdentityEmailaddress1confirmed = "adx_identity_emailaddress1confirmed";
            public const string AdxIdentityLastsuccessfullogin = "adx_identity_lastsuccessfullogin";
            public const string AdxIdentityLocallogindisabled = "adx_identity_locallogindisabled";
            public const string AdxIdentityLockoutenabled = "adx_identity_lockoutenabled";
            public const string AdxIdentityLockoutenddate = "adx_identity_lockoutenddate";
            public const string AdxIdentityLogonenabled = "adx_identity_logonenabled";
            public const string AdxIdentityMobilephoneconfirmed = "adx_identity_mobilephoneconfirmed";
            public const string AdxIdentityNewpassword = "adx_identity_newpassword";
            public const string AdxIdentityPasswordhash = "adx_identity_passwordhash";
            public const string AdxIdentitySecuritystamp = "adx_identity_securitystamp";
            public const string AdxIdentityTwofactorenabled = "adx_identity_twofactorenabled";
            public const string AdxIdentityUsername = "adx_identity_username";
            public const string AdxModifiedByIPAddress = "adx_modifiedbyipaddress";
            public const string AdxModifiedByUsername = "adx_modifiedbyusername";
            public const string AdxOrganizationName = "adx_organizationname";
            public const string AdxPreferredlcid = "adx_preferredlcid";
            public const string AdxProfilealert = "adx_profilealert";
            public const string AdxProfilealertdate = "adx_profilealertdate";
            public const string AdxProfilealertinstructions = "adx_profilealertinstructions";
            public const string AdxProfileIsAnonymous = "adx_profileisanonymous";
            public const string AdxProfileLastActivity = "adx_profilelastactivity";
            public const string AdxProfilemodifiedon = "adx_profilemodifiedon";
            public const string AdxPublicProfileCopy = "adx_publicprofilecopy";
            public const string AdxTimeZone = "adx_timezone";
            public const string Aging30 = "aging30";
            public const string Aging30Base = "aging30_base";
            public const string Aging60 = "aging60";
            public const string Aging60Base = "aging60_base";
            public const string Aging90 = "aging90";
            public const string Aging90Base = "aging90_base";
            public const string Anniversary = "anniversary";
            public const string AnnualIncome = "annualincome";
            public const string AnnualIncomeBase = "annualincome_base";
            public const string AssistantName = "assistantname";
            public const string AssistantPhone = "assistantphone";
            public const string BirthDate = "birthdate";
            public const string Business2 = "business2";
            public const string Callback = "callback";
            public const string ChildrensNames = "childrensnames";
            public const string Company = "company";
            public const string CreatedBy = "createdby";
            public const string CreatedByExternalParty = "createdbyexternalparty";
            public const string CreatedOn = "createdon";
            public const string CreatedOnBehalfBy = "createdonbehalfby";
            public const string CreditLimit = "creditlimit";
            public const string CreditLimitBase = "creditlimit_base";
            public const string CreditOnHold = "creditonhold";
            public const string CustomerSizeCode = "customersizecode";
            public const string CustomerTypeCode = "customertypecode";
            public const string Department = "department";
            public const string Description = "description";
            public const string DoNotBulkEMail = "donotbulkemail";
            public const string DoNotBulkPostalMail = "donotbulkpostalmail";
            public const string DoNotEMail = "donotemail";
            public const string DoNotFax = "donotfax";
            public const string DoNotPhone = "donotphone";
            public const string DoNotPostalMail = "donotpostalmail";
            public const string DoNotSendMM = "donotsendmm";
            public const string EducationCode = "educationcode";
            public const string EMailAddress1 = "emailaddress1";
            public const string EMailAddress2 = "emailaddress2";
            public const string EMailAddress3 = "emailaddress3";
            public const string EmployeeId = "employeeid";
            public const string EntityImage = "entityimage";
            public const string EntityImageTimestamp = "entityimage_timestamp";
            public const string EntityImageURL = "entityimage_url";
            public const string EntityImageId = "entityimageid";
            public const string ExchangeRate = "exchangerate";
            public const string ExternalUserIdentifier = "externaluseridentifier";
            public const string FamilyStatusCode = "familystatuscode";
            public const string Fax = "fax";
            public const string FirstName = "firstname";
            public const string FollowEmail = "followemail";
            public const string FtpSiteUrl = "ftpsiteurl";
            public const string FullName = "fullname";
            public const string GenderCode = "gendercode";
            public const string GovernmentId = "governmentid";
            public const string HasChildrenCode = "haschildrencode";
            public const string Home2 = "home2";
            public const string ImportSequenceNumber = "importsequencenumber";
            public const string IsBackofficeCustomer = "isbackofficecustomer";
            public const string JobTitle = "jobtitle";
            public const string LastName = "lastname";
            public const string LastOnHoldTime = "lastonholdtime";
            public const string LastUsedInCampaign = "lastusedincampaign";
            public const string LeadSourceCode = "leadsourcecode";
            public const string ManagerName = "managername";
            public const string ManagerPhone = "managerphone";
            public const string MarketingOnly = "marketingonly";
            public const string MasterId = "masterid";
            public const string Merged = "merged";
            public const string MiddleName = "middlename";
            public const string MobilePhone = "mobilephone";
            public const string ModifiedBy = "modifiedby";
            public const string ModifiedByExternalParty = "modifiedbyexternalparty";
            public const string ModifiedOn = "modifiedon";
            public const string ModifiedOnBehalfBy = "modifiedonbehalfby";
            public const string MsaManagingpartnerid = "msa_managingpartnerid";
            public const string MsdynDisablewebtracking = "msdyn_disablewebtracking";
            public const string MsdynIsminor = "msdyn_isminor";
            public const string MsdynIsminorwithparentalconsent = "msdyn_isminorwithparentalconsent";
            public const string MsdynPortaltermsagreementdate = "msdyn_portaltermsagreementdate";
            public const string MsppUserpreferredlcid = "mspp_userpreferredlcid";
            public const string NickName = "nickname";
            public const string NumberOfChildren = "numberofchildren";
            public const string OnHoldTime = "onholdtime";
            public const string OverriddenCreatedOn = "overriddencreatedon";
            public const string OwnerId = "ownerid";
            public const string OwningBusinessUnit = "owningbusinessunit";
            public const string OwningTeam = "owningteam";
            public const string OwningUser = "owninguser";
            public const string Pager = "pager";
            public const string ParentContactId = "parentcontactid";
            public const string ParentCustomerId = "parentcustomerid";
            public const string ParticipatesInWorkflow = "participatesinworkflow";
            public const string PaymentTermsCode = "paymenttermscode";
            public const string PreferredAppointmentDayCode = "preferredappointmentdaycode";
            public const string PreferredAppointmentTimeCode = "preferredappointmenttimecode";
            public const string PreferredContactMethodCode = "preferredcontactmethodcode";
            public const string PreferredSystemUserId = "preferredsystemuserid";
            public const string ProcessId = "processid";
            public const string Salutation = "salutation";
            public const string ShippingMethodCode = "shippingmethodcode";
            public const string SLAId = "slaid";
            public const string SLAInvokedId = "slainvokedid";
            public const string SpousesName = "spousesname";
            public const string StageId = "stageid";
            public const string StateCode = "statecode";
            public const string StatusCode = "statuscode";
            public const string SubscriptionId = "subscriptionid";
            public const string Suffix = "suffix";
            public const string Telephone1 = "telephone1";
            public const string Telephone2 = "telephone2";
            public const string Telephone3 = "telephone3";
            public const string TerritoryCode = "territorycode";
            public const string TimeSpentByMeOnEmailAndMeetings = "timespentbymeonemailandmeetings";
            public const string TimeZoneRuleVersionNumber = "timezoneruleversionnumber";
            public const string TransactionCurrencyId = "transactioncurrencyid";
            public const string TraversedPath = "traversedpath";
            public const string UTCConversionTimeZoneCode = "utcconversiontimezonecode";
            public const string VersionNumber = "versionnumber";
            public const string WebSiteUrl = "websiteurl";
            public const string YomiFirstName = "yomifirstname";
            public const string YomiFullName = "yomifullname";
            public const string YomiLastName = "yomilastname";
            public const string YomiMiddleName = "yomimiddlename";
        }
        #endregion

        #region Relations
        public static partial class Relations
        {
            public static class OneToMany
            {
                public const string AccountPrimaryContact = "account_primary_contact";
                public const string AdxContactExternalidentity = "adx_contact_externalidentity";
                public const string AdxInvitationInvitecontact = "adx_invitation_invitecontact";
                public const string AdxInvitationInvitercontact = "adx_invitation_invitercontact";
                public const string AdxInvitationRedeemedContact = "adx_invitation_redeemedContact";
                public const string AdxWebformsessionContact = "adx_webformsession_contact";
                public const string ContactActioncard = "contact_actioncard";
                public const string ContactActivityParties = "contact_activity_parties";
                public const string ContactActivityPointers = "Contact_ActivityPointers";
                public const string ContactAdxInviteredemptions = "contact_adx_inviteredemptions";
                public const string ContactAdxPortalcomments = "contact_adx_portalcomments";
                public const string ContactAnnotation = "Contact_Annotation";
                public const string ContactAppointments = "Contact_Appointments";
                public const string ContactAsyncOperations = "Contact_AsyncOperations";
                public const string ContactBulkDeleteFailures = "Contact_BulkDeleteFailures";
                public const string ContactChats = "contact_chats";
                public const string ContactConnections1 = "contact_connections1";
                public const string ContactConnections2 = "contact_connections2";
                public const string ContactCustomerContacts = "contact_customer_contacts";
                public const string ContactCustomerRelationshipCustomer = "contact_customer_relationship_customer";
                public const string ContactCustomerRelationshipPartner = "contact_customer_relationship_partner";
                public const string ContactCustomerAddress = "Contact_CustomerAddress";
                public const string ContactDuplicateBaseRecord = "Contact_DuplicateBaseRecord";
                public const string ContactDuplicateMatchingRecord = "Contact_DuplicateMatchingRecord";
                public const string ContactEmailEmailSender = "Contact_Email_EmailSender";
                public const string ContactEmails = "Contact_Emails";
                public const string ContactExternalPartyItems = "Contact_ExternalPartyItems";
                public const string ContactFaxes = "Contact_Faxes";
                public const string ContactFeedback = "Contact_Feedback";
                public const string ContactLetters = "Contact_Letters";
                public const string ContactMailboxTrackingFolder = "Contact_MailboxTrackingFolder";
                public const string ContactMasterContact = "contact_master_contact";
                public const string ContactPhonecalls = "Contact_Phonecalls";
                public const string ContactPostFollows = "contact_PostFollows";
                public const string ContactPostRegardings = "contact_PostRegardings";
                public const string ContactPostRoles = "contact_PostRoles";
                public const string ContactPrincipalobjectattributeaccess = "contact_principalobjectattributeaccess";
                public const string ContactProcessSessions = "Contact_ProcessSessions";
                public const string ContactRecurringAppointmentMasters = "Contact_RecurringAppointmentMasters";
                public const string ContactSocialActivities = "Contact_SocialActivities";
                public const string ContactSyncErrors = "Contact_SyncErrors";
                public const string ContactTasks = "Contact_Tasks";
                public const string LkContactFeedbackCreatedby = "lk_contact_feedback_createdby";
                public const string LkContactFeedbackCreatedonbehalfby = "lk_contact_feedback_createdonbehalfby";
                public const string PowerPagesSiteAIFeedbackContactContact = "PowerPagesSiteAIFeedback_Contact_Contact";
                public const string SlakpiinstanceContact = "slakpiinstance_contact";
                public const string SocialactivityPostauthorContacts = "socialactivity_postauthor_contacts";
                public const string SocialactivityPostauthoraccountContacts = "socialactivity_postauthoraccount_contacts";
                public const string SocialprofileCustomerContacts = "Socialprofile_customer_contacts";
                public const string UserentityinstancedataContact = "userentityinstancedata_contact";
            }

            public static partial class ManyToOne
            {
                public const string BusinessUnitContacts = "business_unit_contacts";
                public const string ContactCustomerAccounts = "contact_customer_accounts";
                public const string ContactCustomerContacts = "contact_customer_contacts";
                public const string ContactMasterContact = "contact_master_contact";
                public const string ContactOwningUser = "contact_owning_user";
                public const string LkContactCreatedonbehalfby = "lk_contact_createdonbehalfby";
                public const string LkContactEntityimage = "lk_contact_entityimage";
                public const string LkContactModifiedonbehalfby = "lk_contact_modifiedonbehalfby";
                public const string LkContactbaseCreatedby = "lk_contactbase_createdby";
                public const string LkContactbaseModifiedby = "lk_contactbase_modifiedby";
                public const string LkExternalpartyContactCreatedby = "lk_externalparty_contact_createdby";
                public const string LkExternalpartyContactModifiedby = "lk_externalparty_contact_modifiedby";
                public const string ManualslaContact = "manualsla_contact";
                public const string MsaContactManagingpartner = "msa_contact_managingpartner";
                public const string OwnerContacts = "owner_contacts";
                public const string ProcessstageContact = "processstage_contact";
                public const string SlaContact = "sla_contact";
                public const string SystemUserContacts = "system_user_contacts";
                public const string TeamContacts = "team_contacts";
                public const string TransactioncurrencyContact = "transactioncurrency_contact";
            }

            public static partial class ManyToMany
            {
                public const string AdxInvitationInvitecontacts = "adx_invitation_invitecontacts";
                public const string AdxInvitationRedeemedcontacts = "adx_invitation_redeemedcontacts";
                public const string ContactSubscriptionAssociation = "contact_subscription_association";
                public const string PowerpagecomponentMsppWebroleContact = "powerpagecomponent_mspp_webrole_contact";
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

        public static Contact Retrieve(IOrganizationService service, Guid id)
        {
            return Retrieve(service, id, new ColumnSet(true));
        }

        public static Contact Retrieve(IOrganizationService service, Guid id, ColumnSet columnSet)
        {
            return service.Retrieve("contact", id, columnSet).ToEntity<Contact>();
        }

        public Contact GetChangedEntity()
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
            return new Contact(Id) { Attributes = attr };
        }
        #endregion
    }

    #region Context
    public partial class DataContext
    {
        public IQueryable<Contact> ContactSet
        {
            get
            {
                return CreateQuery<Contact>();
            }
        }
    }
    #endregion
}
