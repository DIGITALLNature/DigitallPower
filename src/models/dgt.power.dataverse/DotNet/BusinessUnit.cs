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
	/// Business, division, or department in the Microsoft Dynamics 365 database.
	/// </summary>
	[DataContractAttribute()]
	[EntityLogicalNameAttribute("businessunit")]
	[System.CodeDom.Compiler.GeneratedCode("dgtp", "2023")]
    [ExcludeFromCodeCoverage]
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
		public BusinessUnit(Guid id, bool trackChanges = false) : base(EntityLogicalName,id)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public BusinessUnit(KeyAttributeCollection keyAttributes, bool trackChanges = false) : base(EntityLogicalName,keyAttributes)
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
        private readonly Lazy<HashSet<string>> _changedProperties = new Lazy<HashSet<string>>();
        #endregion

        #region consts
        public const string EntityLogicalName = "businessunit";
        public const string PrimaryNameAttribute = "name";
        public const int EntityTypeCode = 10;
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
		[AttributeLogicalNameAttribute("businessunitid")]
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
                OnPropertyChanging(nameof(Address1AddressId));
                SetAttributeValue("address1_addressid", value);
				if (value.HasValue)
				{
					base.Id = value.Value;
				}
				else
				{
					base.Id = System.Guid.Empty;
				}
                OnPropertyChanged(nameof(Address1AddressId));
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
                OnPropertyChanging(nameof(Address2AddressId));
                SetAttributeValue("address2_addressid", value);
				if (value.HasValue)
				{
					base.Id = value.Value;
				}
				else
				{
					base.Id = System.Guid.Empty;
				}
                OnPropertyChanged(nameof(Address2AddressId));
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
                OnPropertyChanging(nameof(BusinessUnitId));
                SetAttributeValue("businessunitid", value);
				if (value.HasValue)
				{
					base.Id = value.Value;
				}
				else
				{
					base.Id = System.Guid.Empty;
				}
                OnPropertyChanged(nameof(BusinessUnitId));
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
                OnPropertyChanging(nameof(Address1AddressTypeCode));
                SetAttributeValue("address1_addresstypecode", value);
                OnPropertyChanged(nameof(Address1AddressTypeCode));
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
                OnPropertyChanging(nameof(Address1City));
                SetAttributeValue("address1_city", value);
                OnPropertyChanged(nameof(Address1City));
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
                OnPropertyChanging(nameof(Address1Country));
                SetAttributeValue("address1_country", value);
                OnPropertyChanged(nameof(Address1Country));
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
                OnPropertyChanging(nameof(Address1County));
                SetAttributeValue("address1_county", value);
                OnPropertyChanged(nameof(Address1County));
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
                OnPropertyChanging(nameof(Address1Fax));
                SetAttributeValue("address1_fax", value);
                OnPropertyChanged(nameof(Address1Fax));
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
                OnPropertyChanging(nameof(Address1Latitude));
                SetAttributeValue("address1_latitude", value);
                OnPropertyChanged(nameof(Address1Latitude));
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
                OnPropertyChanging(nameof(Address1Line1));
                SetAttributeValue("address1_line1", value);
                OnPropertyChanged(nameof(Address1Line1));
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
                OnPropertyChanging(nameof(Address1Line2));
                SetAttributeValue("address1_line2", value);
                OnPropertyChanged(nameof(Address1Line2));
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
                OnPropertyChanging(nameof(Address1Line3));
                SetAttributeValue("address1_line3", value);
                OnPropertyChanged(nameof(Address1Line3));
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
                OnPropertyChanging(nameof(Address1Longitude));
                SetAttributeValue("address1_longitude", value);
                OnPropertyChanged(nameof(Address1Longitude));
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
                OnPropertyChanging(nameof(Address1Name));
                SetAttributeValue("address1_name", value);
                OnPropertyChanged(nameof(Address1Name));
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
                OnPropertyChanging(nameof(Address1PostalCode));
                SetAttributeValue("address1_postalcode", value);
                OnPropertyChanged(nameof(Address1PostalCode));
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
                OnPropertyChanging(nameof(Address1PostOfficeBox));
                SetAttributeValue("address1_postofficebox", value);
                OnPropertyChanged(nameof(Address1PostOfficeBox));
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
                OnPropertyChanging(nameof(Address1ShippingMethodCode));
                SetAttributeValue("address1_shippingmethodcode", value);
                OnPropertyChanged(nameof(Address1ShippingMethodCode));
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
                OnPropertyChanging(nameof(Address1StateOrProvince));
                SetAttributeValue("address1_stateorprovince", value);
                OnPropertyChanged(nameof(Address1StateOrProvince));
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
                OnPropertyChanging(nameof(Address1Telephone1));
                SetAttributeValue("address1_telephone1", value);
                OnPropertyChanged(nameof(Address1Telephone1));
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
                OnPropertyChanging(nameof(Address1Telephone2));
                SetAttributeValue("address1_telephone2", value);
                OnPropertyChanged(nameof(Address1Telephone2));
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
                OnPropertyChanging(nameof(Address1Telephone3));
                SetAttributeValue("address1_telephone3", value);
                OnPropertyChanged(nameof(Address1Telephone3));
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
                OnPropertyChanging(nameof(Address1UPSZone));
                SetAttributeValue("address1_upszone", value);
                OnPropertyChanged(nameof(Address1UPSZone));
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
                OnPropertyChanging(nameof(Address1UTCOffset));
                SetAttributeValue("address1_utcoffset", value);
                OnPropertyChanged(nameof(Address1UTCOffset));
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
                OnPropertyChanging(nameof(Address2AddressTypeCode));
                SetAttributeValue("address2_addresstypecode", value);
                OnPropertyChanged(nameof(Address2AddressTypeCode));
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
                OnPropertyChanging(nameof(Address2City));
                SetAttributeValue("address2_city", value);
                OnPropertyChanged(nameof(Address2City));
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
                OnPropertyChanging(nameof(Address2Country));
                SetAttributeValue("address2_country", value);
                OnPropertyChanged(nameof(Address2Country));
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
                OnPropertyChanging(nameof(Address2County));
                SetAttributeValue("address2_county", value);
                OnPropertyChanged(nameof(Address2County));
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
                OnPropertyChanging(nameof(Address2Fax));
                SetAttributeValue("address2_fax", value);
                OnPropertyChanged(nameof(Address2Fax));
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
                OnPropertyChanging(nameof(Address2Latitude));
                SetAttributeValue("address2_latitude", value);
                OnPropertyChanged(nameof(Address2Latitude));
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
                OnPropertyChanging(nameof(Address2Line1));
                SetAttributeValue("address2_line1", value);
                OnPropertyChanged(nameof(Address2Line1));
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
                OnPropertyChanging(nameof(Address2Line2));
                SetAttributeValue("address2_line2", value);
                OnPropertyChanged(nameof(Address2Line2));
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
                OnPropertyChanging(nameof(Address2Line3));
                SetAttributeValue("address2_line3", value);
                OnPropertyChanged(nameof(Address2Line3));
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
                OnPropertyChanging(nameof(Address2Longitude));
                SetAttributeValue("address2_longitude", value);
                OnPropertyChanged(nameof(Address2Longitude));
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
                OnPropertyChanging(nameof(Address2Name));
                SetAttributeValue("address2_name", value);
                OnPropertyChanged(nameof(Address2Name));
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
                OnPropertyChanging(nameof(Address2PostalCode));
                SetAttributeValue("address2_postalcode", value);
                OnPropertyChanged(nameof(Address2PostalCode));
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
                OnPropertyChanging(nameof(Address2PostOfficeBox));
                SetAttributeValue("address2_postofficebox", value);
                OnPropertyChanged(nameof(Address2PostOfficeBox));
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
                OnPropertyChanging(nameof(Address2ShippingMethodCode));
                SetAttributeValue("address2_shippingmethodcode", value);
                OnPropertyChanged(nameof(Address2ShippingMethodCode));
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
                OnPropertyChanging(nameof(Address2StateOrProvince));
                SetAttributeValue("address2_stateorprovince", value);
                OnPropertyChanged(nameof(Address2StateOrProvince));
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
                OnPropertyChanging(nameof(Address2Telephone1));
                SetAttributeValue("address2_telephone1", value);
                OnPropertyChanged(nameof(Address2Telephone1));
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
                OnPropertyChanging(nameof(Address2Telephone2));
                SetAttributeValue("address2_telephone2", value);
                OnPropertyChanged(nameof(Address2Telephone2));
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
                OnPropertyChanging(nameof(Address2Telephone3));
                SetAttributeValue("address2_telephone3", value);
                OnPropertyChanged(nameof(Address2Telephone3));
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
                OnPropertyChanging(nameof(Address2UPSZone));
                SetAttributeValue("address2_upszone", value);
                OnPropertyChanged(nameof(Address2UPSZone));
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
                OnPropertyChanging(nameof(Address2UTCOffset));
                SetAttributeValue("address2_utcoffset", value);
                OnPropertyChanged(nameof(Address2UTCOffset));
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
                OnPropertyChanging(nameof(CalendarId));
                SetAttributeValue("calendarid", value);
                OnPropertyChanged(nameof(CalendarId));
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
                OnPropertyChanging(nameof(CostCenter));
                SetAttributeValue("costcenter", value);
                OnPropertyChanged(nameof(CostCenter));
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
                OnPropertyChanging(nameof(CreditLimit));
                SetAttributeValue("creditlimit", value);
                OnPropertyChanged(nameof(CreditLimit));
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
                OnPropertyChanging(nameof(Description));
                SetAttributeValue("description", value);
                OnPropertyChanged(nameof(Description));
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
                OnPropertyChanging(nameof(DivisionName));
                SetAttributeValue("divisionname", value);
                OnPropertyChanged(nameof(DivisionName));
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
                OnPropertyChanging(nameof(EMailAddress));
                SetAttributeValue("emailaddress", value);
                OnPropertyChanged(nameof(EMailAddress));
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
                OnPropertyChanging(nameof(FileAsName));
                SetAttributeValue("fileasname", value);
                OnPropertyChanged(nameof(FileAsName));
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
                OnPropertyChanging(nameof(FtpSiteUrl));
                SetAttributeValue("ftpsiteurl", value);
                OnPropertyChanged(nameof(FtpSiteUrl));
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
                OnPropertyChanging(nameof(InheritanceMask));
                SetAttributeValue("inheritancemask", value);
                OnPropertyChanged(nameof(InheritanceMask));
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
                OnPropertyChanging(nameof(IsDisabled));
                SetAttributeValue("isdisabled", value);
                OnPropertyChanged(nameof(IsDisabled));
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
                OnPropertyChanging(nameof(Name));
                SetAttributeValue("name", value);
                OnPropertyChanged(nameof(Name));
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
                OnPropertyChanging(nameof(OverriddenCreatedOn));
                SetAttributeValue("overriddencreatedon", value);
                OnPropertyChanged(nameof(OverriddenCreatedOn));
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
                OnPropertyChanging(nameof(ParentBusinessUnitId));
                SetAttributeValue("parentbusinessunitid", value);
                OnPropertyChanged(nameof(ParentBusinessUnitId));
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
                OnPropertyChanging(nameof(Picture));
                SetAttributeValue("picture", value);
                OnPropertyChanged(nameof(Picture));
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
                OnPropertyChanging(nameof(StockExchange));
                SetAttributeValue("stockexchange", value);
                OnPropertyChanged(nameof(StockExchange));
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
                OnPropertyChanging(nameof(TickerSymbol));
                SetAttributeValue("tickersymbol", value);
                OnPropertyChanged(nameof(TickerSymbol));
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
                OnPropertyChanging(nameof(TransactionCurrencyId));
                SetAttributeValue("transactioncurrencyid", value);
                OnPropertyChanged(nameof(TransactionCurrencyId));
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
                OnPropertyChanging(nameof(UTCOffset));
                SetAttributeValue("utcoffset", value);
                OnPropertyChanged(nameof(UTCOffset));
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
                OnPropertyChanging(nameof(WebSiteUrl));
                SetAttributeValue("websiteurl", value);
                OnPropertyChanged(nameof(WebSiteUrl));
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
                OnPropertyChanging(nameof(WorkflowSuspended));
                SetAttributeValue("workflowsuspended", value);
                OnPropertyChanged(nameof(WorkflowSuspended));
            }
        }


		#endregion

		#region NavigationProperties
		/// <summary>
		/// 1:N business_unit_accounts
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("business_unit_accounts")]
		public System.Collections.Generic.IEnumerable<Account> BusinessUnitAccounts
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Account>("business_unit_accounts", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("BusinessUnitAccounts");
				this.SetRelatedEntities<Account>("business_unit_accounts", null, value);
				this.OnPropertyChanged("BusinessUnitAccounts");
			}
		}

		/// <summary>
		/// 1:N business_unit_asyncoperation
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("business_unit_asyncoperation")]
		public System.Collections.Generic.IEnumerable<AsyncOperation> BusinessUnitAsyncoperation
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<AsyncOperation>("business_unit_asyncoperation", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("BusinessUnitAsyncoperation");
				this.SetRelatedEntities<AsyncOperation>("business_unit_asyncoperation", null, value);
				this.OnPropertyChanged("BusinessUnitAsyncoperation");
			}
		}

		/// <summary>
		/// 1:N business_unit_calendars
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("business_unit_calendars")]
		public System.Collections.Generic.IEnumerable<Calendar> BusinessUnitCalendars
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Calendar>("business_unit_calendars", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("BusinessUnitCalendars");
				this.SetRelatedEntities<Calendar>("business_unit_calendars", null, value);
				this.OnPropertyChanged("BusinessUnitCalendars");
			}
		}

		/// <summary>
		/// 1:N business_unit_contacts
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("business_unit_contacts")]
		public System.Collections.Generic.IEnumerable<Contact> BusinessUnitContacts
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Contact>("business_unit_contacts", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("BusinessUnitContacts");
				this.SetRelatedEntities<Contact>("business_unit_contacts", null, value);
				this.OnPropertyChanged("BusinessUnitContacts");
			}
		}

		/// <summary>
		/// 1:N business_unit_customapi
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("business_unit_customapi")]
		public System.Collections.Generic.IEnumerable<CustomAPI> BusinessUnitCustomapi
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<CustomAPI>("business_unit_customapi", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("BusinessUnitCustomapi");
				this.SetRelatedEntities<CustomAPI>("business_unit_customapi", null, value);
				this.OnPropertyChanged("BusinessUnitCustomapi");
			}
		}

		/// <summary>
		/// 1:N business_unit_customapirequestparameter
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("business_unit_customapirequestparameter")]
		public System.Collections.Generic.IEnumerable<CustomAPIRequestParameter> BusinessUnitCustomapirequestparameter
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<CustomAPIRequestParameter>("business_unit_customapirequestparameter", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("BusinessUnitCustomapirequestparameter");
				this.SetRelatedEntities<CustomAPIRequestParameter>("business_unit_customapirequestparameter", null, value);
				this.OnPropertyChanged("BusinessUnitCustomapirequestparameter");
			}
		}

		/// <summary>
		/// 1:N business_unit_customapiresponseproperty
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("business_unit_customapiresponseproperty")]
		public System.Collections.Generic.IEnumerable<CustomAPIResponseProperty> BusinessUnitCustomapiresponseproperty
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<CustomAPIResponseProperty>("business_unit_customapiresponseproperty", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("BusinessUnitCustomapiresponseproperty");
				this.SetRelatedEntities<CustomAPIResponseProperty>("business_unit_customapiresponseproperty", null, value);
				this.OnPropertyChanged("BusinessUnitCustomapiresponseproperty");
			}
		}

		/// <summary>
		/// 1:N business_unit_ec4u_carrier
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("business_unit_ec4u_carrier")]
		public System.Collections.Generic.IEnumerable<Ec4uCarrier> BusinessUnitEc4uCarrier
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Ec4uCarrier>("business_unit_ec4u_carrier", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("BusinessUnitEc4uCarrier");
				this.SetRelatedEntities<Ec4uCarrier>("business_unit_ec4u_carrier", null, value);
				this.OnPropertyChanged("BusinessUnitEc4uCarrier");
			}
		}

		/// <summary>
		/// 1:N business_unit_parent_business_unit
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("business_unit_parent_business_unit")]
		public System.Collections.Generic.IEnumerable<BusinessUnit> BusinessUnitParentBusinessUnit
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<BusinessUnit>("business_unit_parent_business_unit", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("BusinessUnitParentBusinessUnit");
				this.SetRelatedEntities<BusinessUnit>("business_unit_parent_business_unit", null, value);
				this.OnPropertyChanged("BusinessUnitParentBusinessUnit");
			}
		}

		/// <summary>
		/// 1:N business_unit_queues
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("business_unit_queues")]
		public System.Collections.Generic.IEnumerable<Queue> BusinessUnitQueues
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Queue>("business_unit_queues", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("BusinessUnitQueues");
				this.SetRelatedEntities<Queue>("business_unit_queues", null, value);
				this.OnPropertyChanged("BusinessUnitQueues");
			}
		}

		/// <summary>
		/// 1:N business_unit_queues2
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("business_unit_queues2")]
		public System.Collections.Generic.IEnumerable<Queue> BusinessUnitQueues2
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Queue>("business_unit_queues2", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("BusinessUnitQueues2");
				this.SetRelatedEntities<Queue>("business_unit_queues2", null, value);
				this.OnPropertyChanged("BusinessUnitQueues2");
			}
		}

		/// <summary>
		/// 1:N business_unit_roles
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("business_unit_roles")]
		public System.Collections.Generic.IEnumerable<Role> BusinessUnitRoles
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Role>("business_unit_roles", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("BusinessUnitRoles");
				this.SetRelatedEntities<Role>("business_unit_roles", null, value);
				this.OnPropertyChanged("BusinessUnitRoles");
			}
		}

		/// <summary>
		/// 1:N business_unit_routingrule
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("business_unit_routingrule")]
		public System.Collections.Generic.IEnumerable<RoutingRule> BusinessUnitRoutingrule
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<RoutingRule>("business_unit_routingrule", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("BusinessUnitRoutingrule");
				this.SetRelatedEntities<RoutingRule>("business_unit_routingrule", null, value);
				this.OnPropertyChanged("BusinessUnitRoutingrule");
			}
		}

		/// <summary>
		/// 1:N business_unit_slabase
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("business_unit_slabase")]
		public System.Collections.Generic.IEnumerable<SLA> BusinessUnitSlabase
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<SLA>("business_unit_slabase", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("BusinessUnitSlabase");
				this.SetRelatedEntities<SLA>("business_unit_slabase", null, value);
				this.OnPropertyChanged("BusinessUnitSlabase");
			}
		}

		/// <summary>
		/// 1:N business_unit_system_users
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("business_unit_system_users")]
		public System.Collections.Generic.IEnumerable<SystemUser> BusinessUnitSystemUsers
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<SystemUser>("business_unit_system_users", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("BusinessUnitSystemUsers");
				this.SetRelatedEntities<SystemUser>("business_unit_system_users", null, value);
				this.OnPropertyChanged("BusinessUnitSystemUsers");
			}
		}

		/// <summary>
		/// 1:N business_unit_teams
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("business_unit_teams")]
		public System.Collections.Generic.IEnumerable<Team> BusinessUnitTeams
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Team>("business_unit_teams", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("BusinessUnitTeams");
				this.SetRelatedEntities<Team>("business_unit_teams", null, value);
				this.OnPropertyChanged("BusinessUnitTeams");
			}
		}

		/// <summary>
		/// 1:N business_unit_workflow
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("business_unit_workflow")]
		public System.Collections.Generic.IEnumerable<Workflow> BusinessUnitWorkflow
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Workflow>("business_unit_workflow", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("BusinessUnitWorkflow");
				this.SetRelatedEntities<Workflow>("business_unit_workflow", null, value);
				this.OnPropertyChanged("BusinessUnitWorkflow");
			}
		}

		/// <summary>
		/// 1:N BusinessUnit_AsyncOperations
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("BusinessUnit_AsyncOperations")]
		public System.Collections.Generic.IEnumerable<AsyncOperation> BusinessUnitAsyncOperations
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<AsyncOperation>("BusinessUnit_AsyncOperations", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("BusinessUnitAsyncOperations");
				this.SetRelatedEntities<AsyncOperation>("BusinessUnit_AsyncOperations", null, value);
				this.OnPropertyChanged("BusinessUnitAsyncOperations");
			}
		}

		/// <summary>
		/// 1:N BusinessUnit_DuplicateRules
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("BusinessUnit_DuplicateRules")]
		public System.Collections.Generic.IEnumerable<DuplicateRule> BusinessUnitDuplicateRules
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<DuplicateRule>("BusinessUnit_DuplicateRules", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("BusinessUnitDuplicateRules");
				this.SetRelatedEntities<DuplicateRule>("BusinessUnit_DuplicateRules", null, value);
				this.OnPropertyChanged("BusinessUnitDuplicateRules");
			}
		}

		#endregion

		#region Options
		public static class Options
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
		public static class LogicalNames
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
        public static class Relations
        {
            public static class OneToMany
            {
				public const string ActioncardusersettingsBusinessunit = "actioncardusersettings_businessunit";
				public const string BizmapBusinessidBusinessunit = "bizmap_businessid_businessunit";
				public const string BizmapSubbusinessidBusinessunit = "bizmap_subbusinessid_businessunit";
				public const string BulkDeleteOperationBusinessUnit = "BulkDeleteOperation_BusinessUnit";
				public const string BusinessCustomerOpportunityRoles = "business_customer_opportunity_roles";
				public const string BusinessUnitAccounts = "business_unit_accounts";
				public const string BusinessUnitActioncards = "business_unit_actioncards";
				public const string BusinessUnitActivityfileattachment = "business_unit_activityfileattachment";
				public const string BusinessUnitActivitymonitor = "business_unit_activitymonitor";
				public const string BusinessUnitActivitypointer = "business_unit_activitypointer";
				public const string BusinessUnitAdminsettingsentity = "business_unit_adminsettingsentity";
				public const string BusinessUnitAnnotations = "business_unit_annotations";
				public const string BusinessUnitApplicationuser = "business_unit_applicationuser";
				public const string BusinessUnitAppnotification = "business_unit_appnotification";
				public const string BusinessUnitAppointmentActivities = "business_unit_appointment_activities";
				public const string BusinessUnitAsyncoperation = "business_unit_asyncoperation";
				public const string BusinessUnitBookableresource = "business_unit_bookableresource";
				public const string BusinessUnitBookableresourcebooking = "business_unit_bookableresourcebooking";
				public const string BusinessUnitBookableresourcebookingexchangesyncidmapping = "business_unit_bookableresourcebookingexchangesyncidmapping";
				public const string BusinessUnitBookableresourcebookingheader = "business_unit_bookableresourcebookingheader";
				public const string BusinessUnitBookableresourcecategory = "business_unit_bookableresourcecategory";
				public const string BusinessUnitBookableresourcecategoryassn = "business_unit_bookableresourcecategoryassn";
				public const string BusinessUnitBookableresourcecharacteristic = "business_unit_bookableresourcecharacteristic";
				public const string BusinessUnitBookableresourcegroup = "business_unit_bookableresourcegroup";
				public const string BusinessUnitBookingstatus = "business_unit_bookingstatus";
				public const string BusinessUnitBot = "business_unit_bot";
				public const string BusinessUnitBotcomponent = "business_unit_botcomponent";
				public const string BusinessUnitBulkOperationActivities = "business_unit_BulkOperation_activities";
				public const string BusinessUnitCalendars = "business_unit_calendars";
				public const string BusinessUnitCampaignactivityActivities = "business_unit_campaignactivity_activities";
				public const string BusinessUnitCampaignresponseActivities = "business_unit_campaignresponse_activities";
				public const string BusinessUnitCanvasappextendedmetadata = "business_unit_canvasappextendedmetadata";
				public const string BusinessUnitCard = "business_unit_card";
				public const string BusinessUnitCategory = "business_unit_category";
				public const string BusinessUnitChannelaccessprofile = "business_unit_channelaccessprofile";
				public const string BusinessUnitCharacteristic = "business_unit_characteristic";
				public const string BusinessUnitComment = "business_unit_comment";
				public const string BusinessUnitConnectionreference = "business_unit_connectionreference";
				public const string BusinessUnitConnections = "business_unit_connections";
				public const string BusinessUnitConnector = "business_unit_connector";
				public const string BusinessUnitConstraintBasedGroups = "business_unit_constraint_based_groups";
				public const string BusinessUnitContacts = "business_unit_contacts";
				public const string BusinessUnitConversationtranscript = "business_unit_conversationtranscript";
				public const string BusinessUnitConvertrule = "business_unit_convertrule";
				public const string BusinessUnitCustomapi = "business_unit_customapi";
				public const string BusinessUnitCustomapirequestparameter = "business_unit_customapirequestparameter";
				public const string BusinessUnitCustomapiresponseproperty = "business_unit_customapiresponseproperty";
				public const string BusinessUnitCustomerRelationship = "business_unit_customer_relationship";
				public const string BusinessUnitDatalakefolder = "business_unit_datalakefolder";
				public const string BusinessUnitDatalakefolderpermission = "business_unit_datalakefolderpermission";
				public const string BusinessUnitDesktopflowbinary = "business_unit_desktopflowbinary";
				public const string BusinessUnitDesktopflowmodule = "business_unit_desktopflowmodule";
				public const string BusinessUnitDynamicproperyinstance = "business_unit_dynamicproperyinstance";
				public const string BusinessUnitEc4uAcquirelegalbasis = "business_unit_ec4u_acquirelegalbasis";
				public const string BusinessUnitEc4uCarrier = "business_unit_ec4u_carrier";
				public const string BusinessUnitEc4uCarrierDependencyCheck = "business_unit_ec4u_carrier_dependency_check";
				public const string BusinessUnitEc4uCarrierMissingDependency = "business_unit_ec4u_carrier_missing_dependency";
				public const string BusinessUnitEc4uGdprProtocol = "business_unit_ec4u_gdpr_protocol";
				public const string BusinessUnitEc4uGdprProtocolDetail = "business_unit_ec4u_gdpr_protocol_detail";
				public const string BusinessUnitEc4uGdprReport = "business_unit_ec4u_gdpr_report";
				public const string BusinessUnitEc4uGdprRequest = "business_unit_ec4u_gdpr_request";
				public const string BusinessUnitEc4uLegalbasis = "business_unit_ec4u_legalbasis";
				public const string BusinessUnitEc4uWorkbench = "business_unit_ec4u_workbench";
				public const string BusinessUnitEc4uWorkbenchHistory = "business_unit_ec4u_workbench_history";
				public const string BusinessUnitEmailActivities = "business_unit_email_activities";
				public const string BusinessUnitEmailserverprofile = "business_unit_emailserverprofile";
				public const string BusinessUnitEmailsignatures = "business_unit_emailsignatures";
				public const string BusinessUnitEntitlement = "business_unit_entitlement";
				public const string BusinessUnitEntitlemententityallocationtypemapping = "business_unit_entitlemententityallocationtypemapping";
				public const string BusinessUnitEnvironmentvariabledefinition = "business_unit_environmentvariabledefinition";
				public const string BusinessUnitEnvironmentvariablevalue = "business_unit_environmentvariablevalue";
				public const string BusinessUnitEquipment = "business_unit_equipment";
				public const string BusinessUnitExchangesyncidmapping = "business_unit_exchangesyncidmapping";
				public const string BusinessUnitExportedexcel = "business_unit_exportedexcel";
				public const string BusinessUnitExportsolutionupload = "business_unit_exportsolutionupload";
				public const string BusinessUnitExternalparty = "business_unit_externalparty";
				public const string BusinessUnitFaxActivities = "business_unit_fax_activities";
				public const string BusinessUnitFeaturecontrolsetting = "business_unit_featurecontrolsetting";
				public const string BusinessUnitFeedback = "business_unit_feedback";
				public const string BusinessUnitFlowmachine = "business_unit_flowmachine";
				public const string BusinessUnitFlowmachinegroup = "business_unit_flowmachinegroup";
				public const string BusinessUnitFlowmachineimage = "business_unit_flowmachineimage";
				public const string BusinessUnitFlowmachineimageversion = "business_unit_flowmachineimageversion";
				public const string BusinessUnitFlowmachinenetwork = "business_unit_flowmachinenetwork";
				public const string BusinessUnitFlowsession = "business_unit_flowsession";
				public const string BusinessUnitGoal = "business_unit_goal";
				public const string BusinessUnitGoalrollupquery = "business_unit_goalrollupquery";
				public const string BusinessUnitIncidentResolutionActivities = "business_unit_incident_resolution_activities";
				public const string BusinessUnitIncidents = "business_unit_incidents";
				public const string BusinessUnitInteractionforemail = "business_unit_interactionforemail";
				public const string BusinessUnitInvoices = "business_unit_invoices";
				public const string BusinessUnitKeyvaultreference = "business_unit_keyvaultreference";
				public const string BusinessUnitKnowledgearticle = "business_unit_knowledgearticle";
				public const string BusinessUnitLeads = "business_unit_leads";
				public const string BusinessUnitLetterActivities = "business_unit_letter_activities";
				public const string BusinessUnitList = "business_unit_list";
				public const string BusinessUnitListoperation = "business_unit_listoperation";
				public const string BusinessUnitMailbox = "business_unit_mailbox";
				public const string BusinessUnitMailmergetemplates = "business_unit_mailmergetemplates";
				public const string BusinessUnitManagedidentity = "business_unit_managedidentity";
				public const string BusinessUnitMsdynActioncardregarding = "business_unit_msdyn_actioncardregarding";
				public const string BusinessUnitMsdynActioncardrolesetting = "business_unit_msdyn_actioncardrolesetting";
				public const string BusinessUnitMsdynAdminappstate = "business_unit_msdyn_adminappstate";
				public const string BusinessUnitMsdynAgentcapacityupdatehistory = "business_unit_msdyn_agentcapacityupdatehistory";
				public const string BusinessUnitMsdynAgentstatushistory = "business_unit_msdyn_agentstatushistory";
				public const string BusinessUnitMsdynAibdataset = "business_unit_msdyn_aibdataset";
				public const string BusinessUnitMsdynAibdatasetfile = "business_unit_msdyn_aibdatasetfile";
				public const string BusinessUnitMsdynAibdatasetrecord = "business_unit_msdyn_aibdatasetrecord";
				public const string BusinessUnitMsdynAibdatasetscontainer = "business_unit_msdyn_aibdatasetscontainer";
				public const string BusinessUnitMsdynAibfeedbackloop = "business_unit_msdyn_aibfeedbackloop";
				public const string BusinessUnitMsdynAibfile = "business_unit_msdyn_aibfile";
				public const string BusinessUnitMsdynAibfileattacheddata = "business_unit_msdyn_aibfileattacheddata";
				public const string BusinessUnitMsdynAicontactsuggestion = "business_unit_msdyn_aicontactsuggestion";
				public const string BusinessUnitMsdynAifptrainingdocument = "business_unit_msdyn_aifptrainingdocument";
				public const string BusinessUnitMsdynAimodel = "business_unit_msdyn_aimodel";
				public const string BusinessUnitMsdynAiodimage = "business_unit_msdyn_aiodimage";
				public const string BusinessUnitMsdynAiodlabel = "business_unit_msdyn_aiodlabel";
				public const string BusinessUnitMsdynAiodtrainingboundingbox = "business_unit_msdyn_aiodtrainingboundingbox";
				public const string BusinessUnitMsdynAiodtrainingimage = "business_unit_msdyn_aiodtrainingimage";
				public const string BusinessUnitMsdynAitemplate = "business_unit_msdyn_aitemplate";
				public const string BusinessUnitMsdynAnalysiscomponent = "business_unit_msdyn_analysiscomponent";
				public const string BusinessUnitMsdynAnalysisjob = "business_unit_msdyn_analysisjob";
				public const string BusinessUnitMsdynAnalysisresult = "business_unit_msdyn_analysisresult";
				public const string BusinessUnitMsdynAnalysisresultdetail = "business_unit_msdyn_analysisresultdetail";
				public const string BusinessUnitMsdynAnalytics = "business_unit_msdyn_analytics";
				public const string BusinessUnitMsdynAnalyticsadminsettings = "business_unit_msdyn_analyticsadminsettings";
				public const string BusinessUnitMsdynAnalyticsforcs = "business_unit_msdyn_analyticsforcs";
				public const string BusinessUnitMsdynAppconfiguration = "business_unit_msdyn_appconfiguration";
				public const string BusinessUnitMsdynApplicationextension = "business_unit_msdyn_applicationextension";
				public const string BusinessUnitMsdynApplicationtabtemplate = "business_unit_msdyn_applicationtabtemplate";
				public const string BusinessUnitMsdynAppprofilerolemapping = "business_unit_msdyn_appprofilerolemapping";
				public const string BusinessUnitMsdynAssetcategorytemplateassociation = "business_unit_msdyn_assetcategorytemplateassociation";
				public const string BusinessUnitMsdynAssettemplateassociation = "business_unit_msdyn_assettemplateassociation";
				public const string BusinessUnitMsdynAssignmentconfiguration = "business_unit_msdyn_assignmentconfiguration";
				public const string BusinessUnitMsdynAssignmentconfigurationstep = "business_unit_msdyn_assignmentconfigurationstep";
				public const string BusinessUnitMsdynAssignmentmap = "business_unit_msdyn_assignmentmap";
				public const string BusinessUnitMsdynAssignmentrule = "business_unit_msdyn_assignmentrule";
				public const string BusinessUnitMsdynAttribute = "business_unit_msdyn_attribute";
				public const string BusinessUnitMsdynAttributevalue = "business_unit_msdyn_attributevalue";
				public const string BusinessUnitMsdynAuthenticationsettings = "business_unit_msdyn_authenticationsettings";
				public const string BusinessUnitMsdynAuthsettingsentry = "business_unit_msdyn_authsettingsentry";
				public const string BusinessUnitMsdynAutocapturerule = "business_unit_msdyn_autocapturerule";
				public const string BusinessUnitMsdynAutocapturesettings = "business_unit_msdyn_autocapturesettings";
				public const string BusinessUnitMsdynBookableresourcecapacityprofile = "business_unit_msdyn_bookableresourcecapacityprofile";
				public const string BusinessUnitMsdynCallablecontext = "business_unit_msdyn_callablecontext";
				public const string BusinessUnitMsdynCapacityprofile = "business_unit_msdyn_capacityprofile";
				public const string BusinessUnitMsdynCdsentityengagementctx = "business_unit_msdyn_cdsentityengagementctx";
				public const string BusinessUnitMsdynChanneldefinition = "business_unit_msdyn_channeldefinition";
				public const string BusinessUnitMsdynChanneldefinitionconsent = "business_unit_msdyn_channeldefinitionconsent";
				public const string BusinessUnitMsdynChanneldefinitionlocale = "business_unit_msdyn_channeldefinitionlocale";
				public const string BusinessUnitMsdynChannelinstance = "business_unit_msdyn_channelinstance";
				public const string BusinessUnitMsdynChannelinstanceaccount = "business_unit_msdyn_channelinstanceaccount";
				public const string BusinessUnitMsdynChannelmessagepart = "business_unit_msdyn_channelmessagepart";
				public const string BusinessUnitMsdynChannelprovider = "business_unit_msdyn_channelprovider";
				public const string BusinessUnitMsdynConsoleapplicationnotificationfield = "business_unit_msdyn_consoleapplicationnotificationfield";
				public const string BusinessUnitMsdynConsoleapplicationnotificationtemplate = "business_unit_msdyn_consoleapplicationnotificationtemplate";
				public const string BusinessUnitMsdynConsoleapplicationsessiontemplate = "business_unit_msdyn_consoleapplicationsessiontemplate";
				public const string BusinessUnitMsdynConsoleapplicationtemplate = "business_unit_msdyn_consoleapplicationtemplate";
				public const string BusinessUnitMsdynConsoleapplicationtemplateparameter = "business_unit_msdyn_consoleapplicationtemplateparameter";
				public const string BusinessUnitMsdynConsumingapplication = "business_unit_msdyn_consumingapplication";
				public const string BusinessUnitMsdynContactsuggestionrule = "business_unit_msdyn_contactsuggestionrule";
				public const string BusinessUnitMsdynContactsuggestionruleset = "business_unit_msdyn_contactsuggestionruleset";
				public const string BusinessUnitMsdynConversationaction = "business_unit_msdyn_conversationaction";
				public const string BusinessUnitMsdynConversationactionlocale = "business_unit_msdyn_conversationactionlocale";
				public const string BusinessUnitMsdynConversationdata = "business_unit_msdyn_conversationdata";
				public const string BusinessUnitMsdynConversationinsight = "business_unit_msdyn_conversationinsight";
				public const string BusinessUnitMsdynConversationmessageblock = "business_unit_msdyn_conversationmessageblock";
				public const string BusinessUnitMsdynCrmconnection = "business_unit_msdyn_crmconnection";
				public const string BusinessUnitMsdynCsadminconfig = "business_unit_msdyn_csadminconfig";
				public const string BusinessUnitMsdynCustomapirulesetconfiguration = "business_unit_msdyn_customapirulesetconfiguration";
				public const string BusinessUnitMsdynCustomcontrolextendedsettings = "business_unit_msdyn_customcontrolextendedsettings";
				public const string BusinessUnitMsdynCustomerasset = "business_unit_msdyn_customerasset";
				public const string BusinessUnitMsdynCustomerassetattachment = "business_unit_msdyn_customerassetattachment";
				public const string BusinessUnitMsdynCustomerassetcategory = "business_unit_msdyn_customerassetcategory";
				public const string BusinessUnitMsdynDataanalyticscustomizedreport = "business_unit_msdyn_dataanalyticscustomizedreport";
				public const string BusinessUnitMsdynDataanalyticsdataset = "business_unit_msdyn_dataanalyticsdataset";
				public const string BusinessUnitMsdynDataanalyticsreport = "business_unit_msdyn_dataanalyticsreport";
				public const string BusinessUnitMsdynDataanalyticsworkspace = "business_unit_msdyn_dataanalyticsworkspace";
				public const string BusinessUnitMsdynDataflow = "business_unit_msdyn_dataflow";
				public const string BusinessUnitMsdynDataflowrefreshhistory = "business_unit_msdyn_dataflowrefreshhistory";
				public const string BusinessUnitMsdynDealmanageraccess = "business_unit_msdyn_dealmanageraccess";
				public const string BusinessUnitMsdynDealmanagersettings = "business_unit_msdyn_dealmanagersettings";
				public const string BusinessUnitMsdynDecisioncontract = "business_unit_msdyn_decisioncontract";
				public const string BusinessUnitMsdynDecisionruleset = "business_unit_msdyn_decisionruleset";
				public const string BusinessUnitMsdynDefextendedchannelinstance = "business_unit_msdyn_defextendedchannelinstance";
				public const string BusinessUnitMsdynDefextendedchannelinstanceaccount = "business_unit_msdyn_defextendedchannelinstanceaccount";
				public const string BusinessUnitMsdynDeletedconversation = "business_unit_msdyn_deletedconversation";
				public const string BusinessUnitMsdynDuplicateleadmapping = "business_unit_msdyn_duplicateleadmapping";
				public const string BusinessUnitMsdynEffortpredictionresult = "business_unit_msdyn_effortpredictionresult";
				public const string BusinessUnitMsdynEntityconfig = "business_unit_msdyn_entityconfig";
				public const string BusinessUnitMsdynEntitylinkchatconfiguration = "business_unit_msdyn_entitylinkchatconfiguration";
				public const string BusinessUnitMsdynEntityrankingrule = "business_unit_msdyn_entityrankingrule";
				public const string BusinessUnitMsdynEntityrefreshhistory = "business_unit_msdyn_entityrefreshhistory";
				public const string BusinessUnitMsdynEntityroutingconfiguration = "business_unit_msdyn_entityroutingconfiguration";
				public const string BusinessUnitMsdynExtendedusersetting = "business_unit_msdyn_extendedusersetting";
				public const string BusinessUnitMsdynFavoriteknowledgearticle = "business_unit_msdyn_favoriteknowledgearticle";
				public const string BusinessUnitMsdynFederatedarticle = "business_unit_msdyn_federatedarticle";
				public const string BusinessUnitMsdynFlowcardtype = "business_unit_msdyn_flowcardtype";
				public const string BusinessUnitMsdynForecastconfiguration = "business_unit_msdyn_forecastconfiguration";
				public const string BusinessUnitMsdynForecastdefinition = "business_unit_msdyn_forecastdefinition";
				public const string BusinessUnitMsdynForecastinstance = "business_unit_msdyn_forecastinstance";
				public const string BusinessUnitMsdynForecastrecurrence = "business_unit_msdyn_forecastrecurrence";
				public const string BusinessUnitMsdynFunctionallocation = "business_unit_msdyn_functionallocation";
				public const string BusinessUnitMsdynGdprdata = "business_unit_msdyn_gdprdata";
				public const string BusinessUnitMsdynIcebreakersconfig = "business_unit_msdyn_icebreakersconfig";
				public const string BusinessUnitMsdynIermlmodel = "business_unit_msdyn_iermlmodel";
				public const string BusinessUnitMsdynIermltraining = "business_unit_msdyn_iermltraining";
				public const string BusinessUnitMsdynIntegratedsearchprovider = "business_unit_msdyn_integratedsearchprovider";
				public const string BusinessUnitMsdynIotalert = "business_unit_msdyn_iotalert";
				public const string BusinessUnitMsdynIotdevice = "business_unit_msdyn_iotdevice";
				public const string BusinessUnitMsdynIotdevicecategory = "business_unit_msdyn_iotdevicecategory";
				public const string BusinessUnitMsdynIotdevicecommand = "business_unit_msdyn_iotdevicecommand";
				public const string BusinessUnitMsdynIotdevicecommanddefinition = "business_unit_msdyn_iotdevicecommanddefinition";
				public const string BusinessUnitMsdynIotdevicedatahistory = "business_unit_msdyn_iotdevicedatahistory";
				public const string BusinessUnitMsdynIotdeviceproperty = "business_unit_msdyn_iotdeviceproperty";
				public const string BusinessUnitMsdynIotdeviceregistrationhistory = "business_unit_msdyn_iotdeviceregistrationhistory";
				public const string BusinessUnitMsdynIotdevicevisualizationconfiguration = "business_unit_msdyn_iotdevicevisualizationconfiguration";
				public const string BusinessUnitMsdynIotfieldmapping = "business_unit_msdyn_iotfieldmapping";
				public const string BusinessUnitMsdynIotpropertydefinition = "business_unit_msdyn_iotpropertydefinition";
				public const string BusinessUnitMsdynIotprovider = "business_unit_msdyn_iotprovider";
				public const string BusinessUnitMsdynIotproviderinstance = "business_unit_msdyn_iotproviderinstance";
				public const string BusinessUnitMsdynIotsettings = "business_unit_msdyn_iotsettings";
				public const string BusinessUnitMsdynKalanguagesetting = "business_unit_msdyn_kalanguagesetting";
				public const string BusinessUnitMsdynKbattachment = "business_unit_msdyn_kbattachment";
				public const string BusinessUnitMsdynKmfederatedsearchconfig = "business_unit_msdyn_kmfederatedsearchconfig";
				public const string BusinessUnitMsdynKnowledgearticleimage = "business_unit_msdyn_knowledgearticleimage";
				public const string BusinessUnitMsdynKnowledgearticletemplate = "business_unit_msdyn_knowledgearticletemplate";
				public const string BusinessUnitMsdynKnowledgeinteractioninsight = "business_unit_msdyn_knowledgeinteractioninsight";
				public const string BusinessUnitMsdynKnowledgemanagementsetting = "business_unit_msdyn_knowledgemanagementsetting";
				public const string BusinessUnitMsdynKnowledgepersonalfilter = "business_unit_msdyn_knowledgepersonalfilter";
				public const string BusinessUnitMsdynKnowledgesearchfilter = "business_unit_msdyn_knowledgesearchfilter";
				public const string BusinessUnitMsdynKnowledgesearchinsight = "business_unit_msdyn_knowledgesearchinsight";
				public const string BusinessUnitMsdynKpieventdata = "business_unit_msdyn_kpieventdata";
				public const string BusinessUnitMsdynKpieventdefinition = "business_unit_msdyn_kpieventdefinition";
				public const string BusinessUnitMsdynLeadmodelconfig = "business_unit_msdyn_leadmodelconfig";
				public const string BusinessUnitMsdynLiveconversation = "business_unit_msdyn_liveconversation";
				public const string BusinessUnitMsdynLiveworkitemevent = "business_unit_msdyn_liveworkitemevent";
				public const string BusinessUnitMsdynLiveworkstream = "business_unit_msdyn_liveworkstream";
				public const string BusinessUnitMsdynLiveworkstreamcapacityprofile = "business_unit_msdyn_liveworkstreamcapacityprofile";
				public const string BusinessUnitMsdynMacrosession = "business_unit_msdyn_macrosession";
				public const string BusinessUnitMsdynMasterentityroutingconfiguration = "business_unit_msdyn_masterentityroutingconfiguration";
				public const string BusinessUnitMsdynMigrationtracker = "business_unit_msdyn_migrationtracker";
				public const string BusinessUnitMsdynModelpreviewstatus = "business_unit_msdyn_modelpreviewstatus";
				public const string BusinessUnitMsdynMsteamssetting = "business_unit_msdyn_msteamssetting";
				public const string BusinessUnitMsdynNotesanalysisconfig = "business_unit_msdyn_notesanalysisconfig";
				public const string BusinessUnitMsdynNotificationfield = "business_unit_msdyn_notificationfield";
				public const string BusinessUnitMsdynNotificationtemplate = "business_unit_msdyn_notificationtemplate";
				public const string BusinessUnitMsdynOcGeolocationprovider = "business_unit_msdyn_oc_geolocationprovider";
				public const string BusinessUnitMsdynOcautoblockrule = "business_unit_msdyn_ocautoblockrule";
				public const string BusinessUnitMsdynOcbotchannelregistration = "business_unit_msdyn_ocbotchannelregistration";
				public const string BusinessUnitMsdynOcbotchannelregistrationsecret = "business_unit_msdyn_ocbotchannelregistrationsecret";
				public const string BusinessUnitMsdynOcchannelapiconversationprivilege = "business_unit_msdyn_occhannelapiconversationprivilege";
				public const string BusinessUnitMsdynOcchannelapimessageprivilege = "business_unit_msdyn_occhannelapimessageprivilege";
				public const string BusinessUnitMsdynOcchannelapimethodmapping = "business_unit_msdyn_occhannelapimethodmapping";
				public const string BusinessUnitMsdynOcexternalcontext = "business_unit_msdyn_ocexternalcontext";
				public const string BusinessUnitMsdynOcflaggedspam = "business_unit_msdyn_ocflaggedspam";
				public const string BusinessUnitMsdynOclanguage = "business_unit_msdyn_oclanguage";
				public const string BusinessUnitMsdynOcliveworkitemcapacityprofile = "business_unit_msdyn_ocliveworkitemcapacityprofile";
				public const string BusinessUnitMsdynOcliveworkitemcharacteristic = "business_unit_msdyn_ocliveworkitemcharacteristic";
				public const string BusinessUnitMsdynOcliveworkitemcontextitem = "business_unit_msdyn_ocliveworkitemcontextitem";
				public const string BusinessUnitMsdynOcliveworkitemparticipant = "business_unit_msdyn_ocliveworkitemparticipant";
				public const string BusinessUnitMsdynOcliveworkitemsentiment = "business_unit_msdyn_ocliveworkitemsentiment";
				public const string BusinessUnitMsdynOcliveworkstreamcontextvariable = "business_unit_msdyn_ocliveworkstreamcontextvariable";
				public const string BusinessUnitMsdynOcpaymentprofile = "business_unit_msdyn_ocpaymentprofile";
				public const string BusinessUnitMsdynOcprovisioningstate = "business_unit_msdyn_ocprovisioningstate";
				public const string BusinessUnitMsdynOcrecording = "business_unit_msdyn_ocrecording";
				public const string BusinessUnitMsdynOcrequest = "business_unit_msdyn_ocrequest";
				public const string BusinessUnitMsdynOcrichobject = "business_unit_msdyn_ocrichobject";
				public const string BusinessUnitMsdynOcrichobjectmap = "business_unit_msdyn_ocrichobjectmap";
				public const string BusinessUnitMsdynOcruleitem = "business_unit_msdyn_ocruleitem";
				public const string BusinessUnitMsdynOcsentimentdailytopic = "business_unit_msdyn_ocsentimentdailytopic";
				public const string BusinessUnitMsdynOcsentimentdailytopickeyword = "business_unit_msdyn_ocsentimentdailytopickeyword";
				public const string BusinessUnitMsdynOcsentimentdailytopictrending = "business_unit_msdyn_ocsentimentdailytopictrending";
				public const string BusinessUnitMsdynOcsessioncharacteristic = "business_unit_msdyn_ocsessioncharacteristic";
				public const string BusinessUnitMsdynOcsessionparticipantevent = "business_unit_msdyn_ocsessionparticipantevent";
				public const string BusinessUnitMsdynOcsessionsentiment = "business_unit_msdyn_ocsessionsentiment";
				public const string BusinessUnitMsdynOcsimltraining = "business_unit_msdyn_ocsimltraining";
				public const string BusinessUnitMsdynOcsitdimportconfig = "business_unit_msdyn_ocsitdimportconfig";
				public const string BusinessUnitMsdynOcsitdskill = "business_unit_msdyn_ocsitdskill";
				public const string BusinessUnitMsdynOcsitrainingdata = "business_unit_msdyn_ocsitrainingdata";
				public const string BusinessUnitMsdynOcskillidentmlmodel = "business_unit_msdyn_ocskillidentmlmodel";
				public const string BusinessUnitMsdynOmnichannelpersonalization = "business_unit_msdyn_omnichannelpersonalization";
				public const string BusinessUnitMsdynOmnichannelqueue = "business_unit_msdyn_omnichannelqueue";
				public const string BusinessUnitMsdynOmnichannelsyncconfig = "business_unit_msdyn_omnichannelsyncconfig";
				public const string BusinessUnitMsdynOperatinghour = "business_unit_msdyn_operatinghour";
				public const string BusinessUnitMsdynOpportunitymodelconfig = "business_unit_msdyn_opportunitymodelconfig";
				public const string BusinessUnitMsdynOverflowactionconfig = "business_unit_msdyn_overflowactionconfig";
				public const string BusinessUnitMsdynPersonalmessage = "business_unit_msdyn_personalmessage";
				public const string BusinessUnitMsdynPersonalsoundsetting = "business_unit_msdyn_personalsoundsetting";
				public const string BusinessUnitMsdynPlaybookactivity = "business_unit_msdyn_playbookactivity";
				public const string BusinessUnitMsdynPlaybookactivityattribute = "business_unit_msdyn_playbookactivityattribute";
				public const string BusinessUnitMsdynPlaybookcategory = "business_unit_msdyn_playbookcategory";
				public const string BusinessUnitMsdynPlaybookinstance = "business_unit_msdyn_playbookinstance";
				public const string BusinessUnitMsdynPlaybooktemplate = "business_unit_msdyn_playbooktemplate";
				public const string BusinessUnitMsdynPmanalysishistory = "business_unit_msdyn_pmanalysishistory";
				public const string BusinessUnitMsdynPmcalendar = "business_unit_msdyn_pmcalendar";
				public const string BusinessUnitMsdynPmcalendarversion = "business_unit_msdyn_pmcalendarversion";
				public const string BusinessUnitMsdynPminferredtask = "business_unit_msdyn_pminferredtask";
				public const string BusinessUnitMsdynPmprocessextendedmetadataversion = "business_unit_msdyn_pmprocessextendedmetadataversion";
				public const string BusinessUnitMsdynPmprocesstemplate = "business_unit_msdyn_pmprocesstemplate";
				public const string BusinessUnitMsdynPmprocessusersettings = "business_unit_msdyn_pmprocessusersettings";
				public const string BusinessUnitMsdynPmprocessversion = "business_unit_msdyn_pmprocessversion";
				public const string BusinessUnitMsdynPmrecording = "business_unit_msdyn_pmrecording";
				public const string BusinessUnitMsdynPmtemplate = "business_unit_msdyn_pmtemplate";
				public const string BusinessUnitMsdynPmview = "business_unit_msdyn_pmview";
				public const string BusinessUnitMsdynPostalbum = "business_unit_msdyn_postalbum";
				public const string BusinessUnitMsdynPreferredagent = "business_unit_msdyn_preferredagent";
				public const string BusinessUnitMsdynPreferredagentcustomeridentity = "business_unit_msdyn_preferredagentcustomeridentity";
				public const string BusinessUnitMsdynPreferredagentroutedentity = "business_unit_msdyn_preferredagentroutedentity";
				public const string BusinessUnitMsdynProductivityactioninputparameter = "business_unit_msdyn_productivityactioninputparameter";
				public const string BusinessUnitMsdynProductivityactionoutputparameter = "business_unit_msdyn_productivityactionoutputparameter";
				public const string BusinessUnitMsdynProductivityagentscript = "business_unit_msdyn_productivityagentscript";
				public const string BusinessUnitMsdynProductivityagentscriptstep = "business_unit_msdyn_productivityagentscriptstep";
				public const string BusinessUnitMsdynProductivitymacroactiontemplate = "business_unit_msdyn_productivitymacroactiontemplate";
				public const string BusinessUnitMsdynProductivitymacroconnector = "business_unit_msdyn_productivitymacroconnector";
				public const string BusinessUnitMsdynProductivitymacrosolutionconfiguration = "business_unit_msdyn_productivitymacrosolutionconfiguration";
				public const string BusinessUnitMsdynProductivityparameterdefinition = "business_unit_msdyn_productivityparameterdefinition";
				public const string BusinessUnitMsdynProperty = "business_unit_msdyn_property";
				public const string BusinessUnitMsdynPropertyassetassociation = "business_unit_msdyn_propertyassetassociation";
				public const string BusinessUnitMsdynPropertylog = "business_unit_msdyn_propertylog";
				public const string BusinessUnitMsdynPropertytemplateassociation = "business_unit_msdyn_propertytemplateassociation";
				public const string BusinessUnitMsdynRealtimescoring = "business_unit_msdyn_realtimescoring";
				public const string BusinessUnitMsdynRecording = "business_unit_msdyn_recording";
				public const string BusinessUnitMsdynRelationshipinsightsunifiedconfig = "business_unit_msdyn_relationshipinsightsunifiedconfig";
				public const string BusinessUnitMsdynReportbookmark = "business_unit_msdyn_reportbookmark";
				public const string BusinessUnitMsdynRichtextfile = "business_unit_msdyn_richtextfile";
				public const string BusinessUnitMsdynRoutingconfiguration = "business_unit_msdyn_routingconfiguration";
				public const string BusinessUnitMsdynRoutingconfigurationstep = "business_unit_msdyn_routingconfigurationstep";
				public const string BusinessUnitMsdynRoutingrequest = "business_unit_msdyn_routingrequest";
				public const string BusinessUnitMsdynRulesetdependencymapping = "business_unit_msdyn_rulesetdependencymapping";
				public const string BusinessUnitMsdynSalesinsightssettings = "business_unit_msdyn_salesinsightssettings";
				public const string BusinessUnitMsdynSalesocmessage = "business_unit_msdyn_salesocmessage";
				public const string BusinessUnitMsdynSalesocsmstemplate = "business_unit_msdyn_salesocsmstemplate";
				public const string BusinessUnitMsdynSalesroutingrun = "business_unit_msdyn_salesroutingrun";
				public const string BusinessUnitMsdynSalessuggestion = "business_unit_msdyn_salessuggestion";
				public const string BusinessUnitMsdynSalestag = "business_unit_msdyn_salestag";
				public const string BusinessUnitMsdynSearchconfiguration = "business_unit_msdyn_searchconfiguration";
				public const string BusinessUnitMsdynSegment = "business_unit_msdyn_segment";
				public const string BusinessUnitMsdynSequence = "business_unit_msdyn_sequence";
				public const string BusinessUnitMsdynSequencestat = "business_unit_msdyn_sequencestat";
				public const string BusinessUnitMsdynSequencetarget = "business_unit_msdyn_sequencetarget";
				public const string BusinessUnitMsdynSequencetargetstep = "business_unit_msdyn_sequencetargetstep";
				public const string BusinessUnitMsdynSequencetemplate = "business_unit_msdyn_sequencetemplate";
				public const string BusinessUnitMsdynServiceconfiguration = "business_unit_msdyn_serviceconfiguration";
				public const string BusinessUnitMsdynServiceoneprovisioningrequest = "business_unit_msdyn_serviceoneprovisioningrequest";
				public const string BusinessUnitMsdynSessiondata = "business_unit_msdyn_sessiondata";
				public const string BusinessUnitMsdynSessionevent = "business_unit_msdyn_sessionevent";
				public const string BusinessUnitMsdynSessionparticipant = "business_unit_msdyn_sessionparticipant";
				public const string BusinessUnitMsdynSessionparticipantdata = "business_unit_msdyn_sessionparticipantdata";
				public const string BusinessUnitMsdynSessiontemplate = "business_unit_msdyn_sessiontemplate";
				public const string BusinessUnitMsdynSiconfig = "business_unit_msdyn_siconfig";
				public const string BusinessUnitMsdynSkillattachmentruleitem = "business_unit_msdyn_skillattachmentruleitem";
				public const string BusinessUnitMsdynSkillattachmenttarget = "business_unit_msdyn_skillattachmenttarget";
				public const string BusinessUnitMsdynSlakpi = "business_unit_msdyn_slakpi";
				public const string BusinessUnitMsdynSolutionhealthrule = "business_unit_msdyn_solutionhealthrule";
				public const string BusinessUnitMsdynSolutionhealthruleargument = "business_unit_msdyn_solutionhealthruleargument";
				public const string BusinessUnitMsdynSoundnotificationsetting = "business_unit_msdyn_soundnotificationsetting";
				public const string BusinessUnitMsdynSuggestionassignmentrule = "business_unit_msdyn_suggestionassignmentrule";
				public const string BusinessUnitMsdynSuggestionprincipalobjectaccess = "business_unit_msdyn_suggestionprincipalobjectaccess";
				public const string BusinessUnitMsdynSuggestionsellerpriority = "business_unit_msdyn_suggestionsellerpriority";
				public const string BusinessUnitMsdynSwarm = "business_unit_msdyn_swarm";
				public const string BusinessUnitMsdynSwarmparticipant = "business_unit_msdyn_swarmparticipant";
				public const string BusinessUnitMsdynSwarmparticipantrule = "business_unit_msdyn_swarmparticipantrule";
				public const string BusinessUnitMsdynSwarmrole = "business_unit_msdyn_swarmrole";
				public const string BusinessUnitMsdynSwarmskill = "business_unit_msdyn_swarmskill";
				public const string BusinessUnitMsdynSwarmtemplate = "business_unit_msdyn_swarmtemplate";
				public const string BusinessUnitMsdynTaggedrecord = "business_unit_msdyn_taggedrecord";
				public const string BusinessUnitMsdynTemplateforproperties = "business_unit_msdyn_templateforproperties";
				public const string BusinessUnitMsdynTemplateparameter = "business_unit_msdyn_templateparameter";
				public const string BusinessUnitMsdynTemplatetags = "business_unit_msdyn_templatetags";
				public const string BusinessUnitMsdynTimespent = "business_unit_msdyn_timespent";
				public const string BusinessUnitMsdynTranscript = "business_unit_msdyn_transcript";
				public const string BusinessUnitMsdynUnifiedroutingdiagnostic = "business_unit_msdyn_unifiedroutingdiagnostic";
				public const string BusinessUnitMsdynUnifiedroutingrun = "business_unit_msdyn_unifiedroutingrun";
				public const string BusinessUnitMsdynUntrackedappointment = "business_unit_msdyn_untrackedappointment";
				public const string BusinessUnitMsdynUrnotificationtemplate = "business_unit_msdyn_urnotificationtemplate";
				public const string BusinessUnitMsdynUrnotificationtemplatemapping = "business_unit_msdyn_urnotificationtemplatemapping";
				public const string BusinessUnitMsdynVirtualtablecolumncandidate = "business_unit_msdyn_virtualtablecolumncandidate";
				public const string BusinessUnitMsdynVisitorjourney = "business_unit_msdyn_visitorjourney";
				public const string BusinessUnitMsdynVivacustomerlist = "business_unit_msdyn_vivacustomerlist";
				public const string BusinessUnitMsdynVivausersetting = "business_unit_msdyn_vivausersetting";
				public const string BusinessUnitMsdynWallsavedqueryusersettings = "business_unit_msdyn_wallsavedqueryusersettings";
				public const string BusinessUnitMsdynWkwconfig = "business_unit_msdyn_wkwconfig";
				public const string BusinessUnitMsdynWorkqueuestate = "business_unit_msdyn_workqueuestate";
				public const string BusinessUnitMsdynWorkqueueusersetting = "business_unit_msdyn_workqueueusersetting";
				public const string BusinessUnitMsdynceBotcontent = "business_unit_msdynce_botcontent";
				public const string BusinessUnitMsdyncrmAddtocalendarstyle = "business_unit_msdyncrm_addtocalendarstyle";
				public const string BusinessUnitMsdyncrmBasestyle = "business_unit_msdyncrm_basestyle";
				public const string BusinessUnitMsdyncrmButtonstyle = "business_unit_msdyncrm_buttonstyle";
				public const string BusinessUnitMsdyncrmCodestyle = "business_unit_msdyncrm_codestyle";
				public const string BusinessUnitMsdyncrmColumnstyle = "business_unit_msdyncrm_columnstyle";
				public const string BusinessUnitMsdyncrmContentblockstyle = "business_unit_msdyncrm_contentblockstyle";
				public const string BusinessUnitMsdyncrmDividerstyle = "business_unit_msdyncrm_dividerstyle";
				public const string BusinessUnitMsdyncrmGeneralstyles = "business_unit_msdyncrm_generalstyles";
				public const string BusinessUnitMsdyncrmImagestyle = "business_unit_msdyncrm_imagestyle";
				public const string BusinessUnitMsdyncrmLayoutstyle = "business_unit_msdyncrm_layoutstyle";
				public const string BusinessUnitMsdyncrmQrcodestyle = "business_unit_msdyncrm_qrcodestyle";
				public const string BusinessUnitMsdyncrmTextstyle = "business_unit_msdyncrm_textstyle";
				public const string BusinessUnitMsdyncrmVideostyle = "business_unit_msdyncrm_videostyle";
				public const string BusinessUnitMsdynmktCatalogeventstatusconfiguration = "business_unit_msdynmkt_catalogeventstatusconfiguration";
				public const string BusinessUnitMsdynmktConfiguration = "business_unit_msdynmkt_configuration";
				public const string BusinessUnitMsdynmktEventmetadata = "business_unit_msdynmkt_eventmetadata";
				public const string BusinessUnitMsdynmktEventparametermetadata = "business_unit_msdynmkt_eventparametermetadata";
				public const string BusinessUnitMsdynmktExperimentv2 = "business_unit_msdynmkt_experimentv2";
				public const string BusinessUnitMsdynmktFeatureconfiguration = "business_unit_msdynmkt_featureconfiguration";
				public const string BusinessUnitMsdynmktInfobipchannelinstance = "business_unit_msdynmkt_infobipchannelinstance";
				public const string BusinessUnitMsdynmktInfobipchannelinstanceaccount = "business_unit_msdynmkt_infobipchannelinstanceaccount";
				public const string BusinessUnitMsdynmktLinkmobilitychannelinstance = "business_unit_msdynmkt_linkmobilitychannelinstance";
				public const string BusinessUnitMsdynmktLinkmobilitychannelinstanceaccount = "business_unit_msdynmkt_linkmobilitychannelinstanceaccount";
				public const string BusinessUnitMsdynmktMetadataentityrelationship = "business_unit_msdynmkt_metadataentityrelationship";
				public const string BusinessUnitMsdynmktMetadataitem = "business_unit_msdynmkt_metadataitem";
				public const string BusinessUnitMsdynmktMetadatastorestate = "business_unit_msdynmkt_metadatastorestate";
				public const string BusinessUnitMsdynmktMocksmsproviderchannelinstance = "business_unit_msdynmkt_mocksmsproviderchannelinstance";
				public const string BusinessUnitMsdynmktMocksmsproviderchannelinstanceaccount = "business_unit_msdynmkt_mocksmsproviderchannelinstanceaccount";
				public const string BusinessUnitMsdynmktPredefinedplaceholder = "business_unit_msdynmkt_predefinedplaceholder";
				public const string BusinessUnitMsdynmktTelesignchannelinstance = "business_unit_msdynmkt_telesignchannelinstance";
				public const string BusinessUnitMsdynmktTelesignchannelinstanceaccount = "business_unit_msdynmkt_telesignchannelinstanceaccount";
				public const string BusinessUnitMsdynmktTwiliochannelinstance = "business_unit_msdynmkt_twiliochannelinstance";
				public const string BusinessUnitMsdynmktTwiliochannelinstanceaccount = "business_unit_msdynmkt_twiliochannelinstanceaccount";
				public const string BusinessUnitMsfpAlertrule = "business_unit_msfp_alertrule";
				public const string BusinessUnitMsfpEmailtemplate = "business_unit_msfp_emailtemplate";
				public const string BusinessUnitMsfpFileresponse = "business_unit_msfp_fileresponse";
				public const string BusinessUnitMsfpLocalizedemailtemplate = "business_unit_msfp_localizedemailtemplate";
				public const string BusinessUnitMsfpProject = "business_unit_msfp_project";
				public const string BusinessUnitMsfpQuestion = "business_unit_msfp_question";
				public const string BusinessUnitMsfpQuestionresponse = "business_unit_msfp_questionresponse";
				public const string BusinessUnitMsfpSatisfactionmetric = "business_unit_msfp_satisfactionmetric";
				public const string BusinessUnitMsfpSurvey = "business_unit_msfp_survey";
				public const string BusinessUnitMsfpSurveyreminder = "business_unit_msfp_surveyreminder";
				public const string BusinessUnitMsfpUnsubscribedrecipient = "business_unit_msfp_unsubscribedrecipient";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaDef00953ba86d = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_def_00953ba86d";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaDef0c22ae9e8e = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_def_0c22ae9e8e";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaDef1c6c8bafd1 = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_def_1c6c8bafd1";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaDef3430122a33 = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_def_3430122a33";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaDef96c1eb3520 = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_def_96c1eb3520";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaDefB1a182e9c8 = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_def_b1a182e9c8";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaDefE3bf28df86 = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_def_e3bf28df86";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaDefE5537d018a = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_def_e5537d018a";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaDefEe71d1226a = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_def_ee71d1226a";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaDefFfd19a5250 = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_def_ffd19a5250";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT12062a29316 = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t1_2062a29316";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT12a8bdbda8f = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t1_2a8bdbda8f";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT13d3d3a827b = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t1_3d3d3a827b";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT14b75f83914 = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t1_4b75f83914";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT15c169ef238 = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t1_5c169ef238";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT16f6e64d2d0 = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t1_6f6e64d2d0";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT17071985af5 = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t1_7071985af5";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT18e1ad69a9c = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t1_8e1ad69a9c";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT1A8567fad71 = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t1_a8567fad71";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT1E8ece5e3a4 = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t1_e8ece5e3a4";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT11191d47da52 = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t11_191d47da52";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT111aa2dd8353 = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t11_1aa2dd8353";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT111f763c2b21 = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t11_1f763c2b21";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT11355447c1b8 = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t11_355447c1b8";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT1157154580de = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t11_57154580de";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT11C15f04df88 = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t11_c15f04df88";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT11C200febf0c = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t11_c200febf0c";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT11C533d064d5 = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t11_c533d064d5";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT11Eedec5d10c = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t11_eedec5d10c";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT11Ff0deec922 = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t11_ff0deec922";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT124171d43cc7 = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t12_4171d43cc7";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT12542b499f6f = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t12_542b499f6f";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT12670b0850c0 = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t12_670b0850c0";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT127232d02acd = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t12_7232d02acd";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT1277efa630fe = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t12_77efa630fe";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT1291855bb2f7 = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t12_91855bb2f7";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT1292bc47a6eb = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t12_92bc47a6eb";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT1296e6ba6bd1 = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t12_96e6ba6bd1";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT12Bc43dc92b5 = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t12_bc43dc92b5";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT12C1eef4d03a = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t12_c1eef4d03a";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT20092e7e40a = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t2_0092e7e40a";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT21225727811 = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t2_1225727811";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT2164455c24a = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t2_164455c24a";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT23d0d5f42e9 = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t2_3d0d5f42e9";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT23e3ad1e11b = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t2_3e3ad1e11b";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT25a2677fa0f = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t2_5a2677fa0f";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT299118b1d5f = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t2_99118b1d5f";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT2C78e121ee6 = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t2_c78e121ee6";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT2Dbf5b76f03 = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t2_dbf5b76f03";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT2E28fef2bba = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t2_e28fef2bba";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT30f1afbb350 = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t3_0f1afbb350";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT32581b7e267 = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t3_2581b7e267";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT36634415773 = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t3_6634415773";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT36853dee4d2 = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t3_6853dee4d2";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT36dcd947688 = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t3_6dcd947688";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT380101a7e38 = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t3_80101a7e38";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT3C21a1260ac = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t3_c21a1260ac";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT3C59ea92a3a = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t3_c59ea92a3a";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT3D55f2fa8dd = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t3_d55f2fa8dd";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT3D85dd82696 = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t3_d85dd82696";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT4115d677022 = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t4_115d677022";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT42468772e17 = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t4_2468772e17";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT437c34150a1 = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t4_37c34150a1";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT45113e1140c = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t4_5113e1140c";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT471ab6e7243 = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t4_71ab6e7243";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT4Ae02f22693 = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t4_ae02f22693";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT4B867875e80 = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t4_b867875e80";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT4Ba55d05bea = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t4_ba55d05bea";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT4D94da8a3a3 = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t4_d94da8a3a3";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT4F36e4cc057 = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t4_f36e4cc057";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT50e16d1263c = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t5_0e16d1263c";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT50e4c0f375a = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t5_0e4c0f375a";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT524ddc6e94a = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t5_24ddc6e94a";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT525b4801af0 = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t5_25b4801af0";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT54ee485d1fe = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t5_4ee485d1fe";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT563a4b35e04 = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t5_63a4b35e04";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT59469c72fa9 = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t5_9469c72fa9";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT59bcd382830 = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t5_9bcd382830";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT5C7adee95dd = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t5_c7adee95dd";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT5Ff65a9fc08 = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t5_ff65a9fc08";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT61ef57bc397 = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t6_1ef57bc397";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT632579f0e25 = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t6_32579f0e25";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT63a3bcd69af = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t6_3a3bcd69af";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT64e584bde60 = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t6_4e584bde60";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT6897f136b73 = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t6_897f136b73";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT68b32f605e4 = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t6_8b32f605e4";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT6B53c024913 = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t6_b53c024913";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT6Bc80a01a49 = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t6_bc80a01a49";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT6Ef539ea408 = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t6_ef539ea408";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT6F7701a2990 = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t6_f7701a2990";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT7118e821cbb = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t7_118e821cbb";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT72b3ab930c5 = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t7_2b3ab930c5";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT72c79b475a1 = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t7_2c79b475a1";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT73d67bbb7ec = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t7_3d67bbb7ec";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT76c486355b3 = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t7_6c486355b3";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT77c755fe61d = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t7_7c755fe61d";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT78605946c87 = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t7_8605946c87";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT7Bd49e41c2d = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t7_bd49e41c2d";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT7Fa13d1af3e = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t7_fa13d1af3e";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT7Fab0f857b5 = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t7_fab0f857b5";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT92c439b903c = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t9_2c439b903c";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT9351a001e08 = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t9_351a001e08";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT94251be212f = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t9_4251be212f";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT950966a5af0 = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t9_50966a5af0";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT9587e1edc42 = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t9_587e1edc42";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT95a54131c80 = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t9_5a54131c80";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT965e0181465 = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t9_65e0181465";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT97fdd915386 = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t9_7fdd915386";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT99a0017b6f8 = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t9_9a0017b6f8";
				public const string BusinessUnitNewSystemDonotuseentityRp53fd1p1ekxpaT9Cab0ac200a = "business_unit_new_system_donotuseentity_rp53fd1p1ekxpa_t9_cab0ac200a";
				public const string BusinessUnitNewSystemSolDonotuseentityRp53fd1p1ekxpaSol1250835 = "business_unit_new_system_sol_donotuseentity_rp53fd1p1ekxpa_sol_1250835";
				public const string BusinessUnitNewSystemSolDonotuseentityRp53fd1p1ekxpaSol3aa9185 = "business_unit_new_system_sol_donotuseentity_rp53fd1p1ekxpa_sol_3aa9185";
				public const string BusinessUnitNewSystemSolDonotuseentityRp53fd1p1ekxpaSol55a3a82 = "business_unit_new_system_sol_donotuseentity_rp53fd1p1ekxpa_sol_55a3a82";
				public const string BusinessUnitNewSystemSolDonotuseentityRp53fd1p1ekxpaSol95d5b32 = "business_unit_new_system_sol_donotuseentity_rp53fd1p1ekxpa_sol_95d5b32";
				public const string BusinessUnitNewSystemSolDonotuseentityRp53fd1p1ekxpaSolAc091e5 = "business_unit_new_system_sol_donotuseentity_rp53fd1p1ekxpa_sol_ac091e5";
				public const string BusinessUnitNewSystemSolDonotuseentityRp53fd1p1ekxpaSolAdf8474 = "business_unit_new_system_sol_donotuseentity_rp53fd1p1ekxpa_sol_adf8474";
				public const string BusinessUnitNewSystemSolDonotuseentityRp53fd1p1ekxpaSolB0a3d13 = "business_unit_new_system_sol_donotuseentity_rp53fd1p1ekxpa_sol_b0a3d13";
				public const string BusinessUnitOpportunities = "business_unit_opportunities";
				public const string BusinessUnitOpportunityCloseActivities = "business_unit_opportunity_close_activities";
				public const string BusinessUnitOrderCloseActivities = "business_unit_order_close_activities";
				public const string BusinessUnitOrders = "business_unit_orders";
				public const string BusinessUnitParentBusinessUnit = "business_unit_parent_business_unit";
				public const string BusinessUnitPdfsetting = "business_unit_pdfsetting";
				public const string BusinessUnitPersonaldocumenttemplates = "business_unit_personaldocumenttemplates";
				public const string BusinessUnitPhoneCallActivities = "business_unit_phone_call_activities";
				public const string BusinessUnitPostfollows = "business_unit_postfollows";
				public const string BusinessUnitPostRegarding = "business_unit_PostRegarding";
				public const string BusinessUnitPowerbidataset = "business_unit_powerbidataset";
				public const string BusinessUnitPowerbimashupparameter = "business_unit_powerbimashupparameter";
				public const string BusinessUnitPowerbireport = "business_unit_powerbireport";
				public const string BusinessUnitPowerfxrule = "business_unit_powerfxrule";
				public const string BusinessUnitPrincipalentitybusinessunitmap = "business_unit_principalentitybusinessunitmap";
				public const string BusinessUnitProcessstageparameter = "business_unit_processstageparameter";
				public const string BusinessUnitProfilerule = "business_unit_profilerule";
				public const string BusinessUnitQueues = "business_unit_queues";
				public const string BusinessUnitQueues2 = "business_unit_queues2";
				public const string BusinessUnitQuoteCloseActivities = "business_unit_quote_close_activities";
				public const string BusinessUnitQuotes = "business_unit_quotes";
				public const string BusinessUnitRatingmodel = "business_unit_ratingmodel";
				public const string BusinessUnitRatingvalue = "business_unit_ratingvalue";
				public const string BusinessUnitRecurrencerule = "business_unit_recurrencerule";
				public const string BusinessUnitRecurringappointmentmasterActivities = "business_unit_recurringappointmentmaster_activities";
				public const string BusinessUnitReports = "business_unit_reports";
				public const string BusinessUnitResourceGroups = "business_unit_resource_groups";
				public const string BusinessUnitResourceSpecs = "business_unit_resource_specs";
				public const string BusinessUnitResources = "business_unit_resources";
				public const string BusinessUnitRoles = "business_unit_roles";
				public const string BusinessUnitRoutingrule = "business_unit_routingrule";
				public const string BusinessUnitSalesprocessinstance = "business_unit_salesprocessinstance";
				public const string BusinessUnitServiceAppointments = "business_unit_service_appointments";
				public const string BusinessUnitServiceContracts = "business_unit_service_contracts";
				public const string BusinessUnitSharepointdocument = "business_unit_sharepointdocument";
				public const string BusinessUnitSharepointdocument2 = "business_unit_sharepointdocument2";
				public const string BusinessUnitSharepointdocumentlocation = "business_unit_sharepointdocumentlocation";
				public const string BusinessUnitSharepointsites = "business_unit_sharepointsites";
				public const string BusinessUnitSlabase = "business_unit_slabase";
				public const string BusinessUnitSlakpiinstance = "business_unit_slakpiinstance";
				public const string BusinessUnitSocialactivity = "business_unit_socialactivity";
				public const string BusinessUnitSocialprofiles = "business_unit_socialprofiles";
				public const string BusinessUnitSolutioncomponentbatchconfiguration = "business_unit_solutioncomponentbatchconfiguration";
				public const string BusinessUnitStagesolutionupload = "business_unit_stagesolutionupload";
				public const string BusinessUnitSynapsedatabase = "business_unit_synapsedatabase";
				public const string BusinessUnitSystemUsers = "business_unit_system_users";
				public const string BusinessUnitTaskActivities = "business_unit_task_activities";
				public const string BusinessUnitTdsmetadata = "business_unit_tdsmetadata";
				public const string BusinessUnitTeams = "business_unit_teams";
				public const string BusinessUnitTemplates = "business_unit_templates";
				public const string BusinessUnitTraceRegarding = "business_unit_TraceRegarding";
				public const string BusinessUnitUntrackedemailActivities = "business_unit_untrackedemail_activities";
				public const string BusinessUnitUserSettings = "business_unit_user_settings";
				public const string BusinessUnitUserapplicationmetadata = "business_unit_userapplicationmetadata";
				public const string BusinessUnitUserform = "business_unit_userform";
				public const string BusinessUnitUserquery = "business_unit_userquery";
				public const string BusinessUnitUserqueryvisualizations = "business_unit_userqueryvisualizations";
				public const string BusinessUnitWorkflow = "business_unit_workflow";
				public const string BusinessUnitWorkflowbinary = "business_unit_workflowbinary";
				public const string BusinessUnitWorkflowlogs = "business_unit_workflowlogs";
				public const string BusinessUnitAsyncOperations = "BusinessUnit_AsyncOperations";
				public const string BusinessUnitBulkDeleteFailures = "BusinessUnit_BulkDeleteFailures";
				public const string BusinessunitCallbackregistration = "businessunit_callbackregistration";
				public const string BusinessUnitCampaigns = "BusinessUnit_Campaigns";
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
				public const string MsdynOcliveworkitemBusinessunitOwningbusinessunit = "msdyn_ocliveworkitem_businessunit_owningbusinessunit";
				public const string MsdynOcsessionBusinessunitOwningbusinessunit = "msdyn_ocsession_businessunit_owningbusinessunit";
				public const string MsfpAlertBusinessunitOwningbusinessunit = "msfp_alert_businessunit_owningbusinessunit";
				public const string MsfpSurveyinviteBusinessunitOwningbusinessunit = "msfp_surveyinvite_businessunit_owningbusinessunit";
				public const string MsfpSurveyresponseBusinessunitOwningbusinessunit = "msfp_surveyresponse_businessunit_owningbusinessunit";
				public const string OwningBusinessunitProcesssessions = "Owning_businessunit_processsessions";
				public const string SystemuserbusinessunitentitymapBusinessunitidBusinessunit = "systemuserbusinessunitentitymap_businessunitid_businessunit";
				public const string UserentityinstancedataBusinessunit = "userentityinstancedata_businessunit";
				public const string UserentityuisettingsBusinessunit = "userentityuisettings_businessunit";
            }

            public static class ManyToOne
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
        public static BusinessUnit Retrieve(IOrganizationService service, Guid id)
        {
            return Retrieve(service,id, new ColumnSet(true));
        }

        public static BusinessUnit Retrieve(IOrganizationService service, Guid id, ColumnSet columnSet)
        {
            return service.Retrieve("businessunit", id, columnSet).ToEntity<BusinessUnit>();
        }

        public BusinessUnit GetChangedEntity()
        {
            if (_trackChanges)
            {
                var attr = new AttributeCollection();
                foreach (var attrName in _changedProperties.Value.Select(changedProperty => ((AttributeLogicalNameAttribute) GetType().GetProperty(changedProperty)!.GetCustomAttribute(typeof (AttributeLogicalNameAttribute))!).LogicalName).Where(attrName => Contains(attrName)))
                {
                    attr.Add(attrName,this[attrName]);
                }
                return new  BusinessUnit(Id) {Attributes = attr };
            }
            return this;
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
