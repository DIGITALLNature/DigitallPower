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
		/// Unique identifier for Warehouse associated with Business Unit.
		/// </summary>
		[AttributeLogicalName("msdyn_warehouse")]
        public EntityReference? MsdynWarehouse
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<EntityReference?>("msdyn_warehouse");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(MsdynWarehouse));
                SetAttributeValue("msdyn_warehouse", value);
                OnPropertyChanged(nameof(MsdynWarehouse));
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
		/// 1:N business_unit_environmentvariabledefinition
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("business_unit_environmentvariabledefinition")]
		public System.Collections.Generic.IEnumerable<EnvironmentVariableDefinition> BusinessUnitEnvironmentvariabledefinition
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<EnvironmentVariableDefinition>("business_unit_environmentvariabledefinition", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("BusinessUnitEnvironmentvariabledefinition");
				this.SetRelatedEntities<EnvironmentVariableDefinition>("business_unit_environmentvariabledefinition", null, value);
				this.OnPropertyChanged("BusinessUnitEnvironmentvariabledefinition");
			}
		}

		/// <summary>
		/// 1:N business_unit_environmentvariablevalue
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("business_unit_environmentvariablevalue")]
		public System.Collections.Generic.IEnumerable<EnvironmentVariableValue> BusinessUnitEnvironmentvariablevalue
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<EnvironmentVariableValue>("business_unit_environmentvariablevalue", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("BusinessUnitEnvironmentvariablevalue");
				this.SetRelatedEntities<EnvironmentVariableValue>("business_unit_environmentvariablevalue", null, value);
				this.OnPropertyChanged("BusinessUnitEnvironmentvariablevalue");
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
				public const string MsdynWarehouse = "msdyn_warehouse";
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
				public const string AdxInviteredemptionBusinessunitOwningbusinessunit = "adx_inviteredemption_businessunit_owningbusinessunit";
				public const string AdxPortalcommentBusinessunitOwningbusinessunit = "adx_portalcomment_businessunit_owningbusinessunit";
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
				public const string BusinessUnitAdxInvitation = "business_unit_adx_invitation";
				public const string BusinessUnitAdxSetting = "business_unit_adx_setting";
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
				public const string BusinessUnitAnnotations = "business_unit_annotations";
				public const string BusinessUnitApplicationuser = "business_unit_applicationuser";
				public const string BusinessUnitAppnotification = "business_unit_appnotification";
				public const string BusinessUnitAppointmentActivities = "business_unit_appointment_activities";
				public const string BusinessUnitArchivecleanupinfo = "business_unit_archivecleanupinfo";
				public const string BusinessUnitArchivecleanupoperation = "business_unit_archivecleanupoperation";
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
				public const string BusinessUnitBotcomponentcollection = "business_unit_botcomponentcollection";
				public const string BusinessUnitBulkarchiveconfig = "business_unit_bulkarchiveconfig";
				public const string BusinessUnitBulkarchivefailuredetail = "business_unit_bulkarchivefailuredetail";
				public const string BusinessUnitBulkarchiveoperation = "business_unit_bulkarchiveoperation";
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
				public const string BusinessUnitConnectioninstance = "business_unit_connectioninstance";
				public const string BusinessUnitConnectionreference = "business_unit_connectionreference";
				public const string BusinessUnitConnections = "business_unit_connections";
				public const string BusinessUnitConnector = "business_unit_connector";
				public const string BusinessUnitConstraintBasedGroups = "business_unit_constraint_based_groups";
				public const string BusinessUnitContacts = "business_unit_contacts";
				public const string BusinessUnitConversationtranscript = "business_unit_conversationtranscript";
				public const string BusinessUnitConvertrule = "business_unit_convertrule";
				public const string BusinessUnitCopilotglossaryterm = "business_unit_copilotglossaryterm";
				public const string BusinessUnitCopilotsynonyms = "business_unit_copilotsynonyms";
				public const string BusinessUnitCr072Booking = "business_unit_cr072_booking";
				public const string BusinessUnitCredential = "business_unit_credential";
				public const string BusinessUnitCustomapi = "business_unit_customapi";
				public const string BusinessUnitCustomerRelationship = "business_unit_customer_relationship";
				public const string BusinessUnitDatalakefolder = "business_unit_datalakefolder";
				public const string BusinessUnitDesktopflowbinary = "business_unit_desktopflowbinary";
				public const string BusinessUnitDesktopflowmodule = "business_unit_desktopflowmodule";
				public const string BusinessUnitDgtWorkbench = "business_unit_dgt_workbench";
				public const string BusinessUnitDgtWorkbenchHistory = "business_unit_dgt_workbench_history";
				public const string BusinessUnitDvfilesearch = "business_unit_dvfilesearch";
				public const string BusinessUnitDvfilesearchattribute = "business_unit_dvfilesearchattribute";
				public const string BusinessUnitDvfilesearchentity = "business_unit_dvfilesearchentity";
				public const string BusinessUnitDvtablesearch = "business_unit_dvtablesearch";
				public const string BusinessUnitDvtablesearchattribute = "business_unit_dvtablesearchattribute";
				public const string BusinessUnitDvtablesearchentity = "business_unit_dvtablesearchentity";
				public const string BusinessUnitDynamicproperyinstance = "business_unit_dynamicproperyinstance";
				public const string BusinessUnitEmailActivities = "business_unit_email_activities";
				public const string BusinessUnitEmailserverprofile = "business_unit_emailserverprofile";
				public const string BusinessUnitEmailsignatures = "business_unit_emailsignatures";
				public const string BusinessUnitEnablearchivalrequest = "business_unit_enablearchivalrequest";
				public const string BusinessUnitEntitlement = "business_unit_entitlement";
				public const string BusinessUnitEntitlemententityallocationtypemapping = "business_unit_entitlemententityallocationtypemapping";
				public const string BusinessUnitEnvironmentvariabledefinition = "business_unit_environmentvariabledefinition";
				public const string BusinessUnitEnvironmentvariablevalue = "business_unit_environmentvariablevalue";
				public const string BusinessUnitEquipment = "business_unit_equipment";
				public const string BusinessUnitExchangesyncidmapping = "business_unit_exchangesyncidmapping";
				public const string BusinessUnitExportedexcel = "business_unit_exportedexcel";
				public const string BusinessUnitExportsolutionupload = "business_unit_exportsolutionupload";
				public const string BusinessUnitExternalparty = "business_unit_externalparty";
				public const string BusinessUnitFabricaiskill = "business_unit_fabricaiskill";
				public const string BusinessUnitFaxActivities = "business_unit_fax_activities";
				public const string BusinessUnitFeaturecontrolsetting = "business_unit_featurecontrolsetting";
				public const string BusinessUnitFederatedknowledgeconfiguration = "business_unit_federatedknowledgeconfiguration";
				public const string BusinessUnitFederatedknowledgeentityconfiguration = "business_unit_federatedknowledgeentityconfiguration";
				public const string BusinessUnitFeedback = "business_unit_feedback";
				public const string BusinessUnitFlowcapacityassignment = "business_unit_flowcapacityassignment";
				public const string BusinessUnitFlowcredentialapplication = "business_unit_flowcredentialapplication";
				public const string BusinessUnitFlowevent = "business_unit_flowevent";
				public const string BusinessUnitFlowmachine = "business_unit_flowmachine";
				public const string BusinessUnitFlowmachinegroup = "business_unit_flowmachinegroup";
				public const string BusinessUnitFlowmachineimage = "business_unit_flowmachineimage";
				public const string BusinessUnitFlowmachineimageversion = "business_unit_flowmachineimageversion";
				public const string BusinessUnitFlowmachinenetwork = "business_unit_flowmachinenetwork";
				public const string BusinessUnitFlowrun = "business_unit_flowrun";
				public const string BusinessUnitFlowsession = "business_unit_flowsession";
				public const string BusinessUnitFxexpression = "business_unit_fxexpression";
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
				public const string BusinessUnitMsdynAccountpricelist = "business_unit_msdyn_accountpricelist";
				public const string BusinessUnitMsdynActioncardactionstat = "business_unit_msdyn_actioncardactionstat";
				public const string BusinessUnitMsdynActioncardregarding = "business_unit_msdyn_actioncardregarding";
				public const string BusinessUnitMsdynActioncardrolesetting = "business_unit_msdyn_actioncardrolesetting";
				public const string BusinessUnitMsdynActioncardstataggregation = "business_unit_msdyn_actioncardstataggregation";
				public const string BusinessUnitMsdynActiveicdextension = "business_unit_msdyn_activeicdextension";
				public const string BusinessUnitMsdynActual = "business_unit_msdyn_actual";
				public const string BusinessUnitMsdynAdminappstate = "business_unit_msdyn_adminappstate";
				public const string BusinessUnitMsdynAgentcapacityprofileunit = "business_unit_msdyn_agentcapacityprofileunit";
				public const string BusinessUnitMsdynAgentcapacityupdatehistory = "business_unit_msdyn_agentcapacityupdatehistory";
				public const string BusinessUnitMsdynAgentchannelstate = "business_unit_msdyn_agentchannelstate";
				public const string BusinessUnitMsdynAgentstatus = "business_unit_msdyn_agentstatus";
				public const string BusinessUnitMsdynAgentstatushistory = "business_unit_msdyn_agentstatushistory";
				public const string BusinessUnitMsdynAgreement = "business_unit_msdyn_agreement";
				public const string BusinessUnitMsdynAgreementbookingdate = "business_unit_msdyn_agreementbookingdate";
				public const string BusinessUnitMsdynAgreementbookingincident = "business_unit_msdyn_agreementbookingincident";
				public const string BusinessUnitMsdynAgreementbookingproduct = "business_unit_msdyn_agreementbookingproduct";
				public const string BusinessUnitMsdynAgreementbookingservice = "business_unit_msdyn_agreementbookingservice";
				public const string BusinessUnitMsdynAgreementbookingservicetask = "business_unit_msdyn_agreementbookingservicetask";
				public const string BusinessUnitMsdynAgreementbookingsetup = "business_unit_msdyn_agreementbookingsetup";
				public const string BusinessUnitMsdynAgreementinvoicedate = "business_unit_msdyn_agreementinvoicedate";
				public const string BusinessUnitMsdynAgreementinvoiceproduct = "business_unit_msdyn_agreementinvoiceproduct";
				public const string BusinessUnitMsdynAgreementinvoicesetup = "business_unit_msdyn_agreementinvoicesetup";
				public const string BusinessUnitMsdynAgreementsubstatus = "business_unit_msdyn_agreementsubstatus";
				public const string BusinessUnitMsdynAibdataset = "business_unit_msdyn_aibdataset";
				public const string BusinessUnitMsdynAibdatasetfile = "business_unit_msdyn_aibdatasetfile";
				public const string BusinessUnitMsdynAibdatasetrecord = "business_unit_msdyn_aibdatasetrecord";
				public const string BusinessUnitMsdynAibdatasetscontainer = "business_unit_msdyn_aibdatasetscontainer";
				public const string BusinessUnitMsdynAibfeedbackloop = "business_unit_msdyn_aibfeedbackloop";
				public const string BusinessUnitMsdynAibfile = "business_unit_msdyn_aibfile";
				public const string BusinessUnitMsdynAibfileattacheddata = "business_unit_msdyn_aibfileattacheddata";
				public const string BusinessUnitMsdynAicontactsuggestion = "business_unit_msdyn_aicontactsuggestion";
				public const string BusinessUnitMsdynAievent = "business_unit_msdyn_aievent";
				public const string BusinessUnitMsdynAifptrainingdocument = "business_unit_msdyn_aifptrainingdocument";
				public const string BusinessUnitMsdynAimodel = "business_unit_msdyn_aimodel";
				public const string BusinessUnitMsdynAiodimage = "business_unit_msdyn_aiodimage";
				public const string BusinessUnitMsdynAiodlabel = "business_unit_msdyn_aiodlabel";
				public const string BusinessUnitMsdynAiodtrainingboundingbox = "business_unit_msdyn_aiodtrainingboundingbox";
				public const string BusinessUnitMsdynAiodtrainingimage = "business_unit_msdyn_aiodtrainingimage";
				public const string BusinessUnitMsdynAitemplate = "business_unit_msdyn_aitemplate";
				public const string BusinessUnitMsdynAnalysiscomponent = "business_unit_msdyn_analysiscomponent";
				public const string BusinessUnitMsdynAnalysisjob = "business_unit_msdyn_analysisjob";
				public const string BusinessUnitMsdynAnalysisoverride = "business_unit_msdyn_analysisoverride";
				public const string BusinessUnitMsdynAnalysisresult = "business_unit_msdyn_analysisresult";
				public const string BusinessUnitMsdynAnalysisresultdetail = "business_unit_msdyn_analysisresultdetail";
				public const string BusinessUnitMsdynAnalytics = "business_unit_msdyn_analytics";
				public const string BusinessUnitMsdynAnalyticsadminsettings = "business_unit_msdyn_analyticsadminsettings";
				public const string BusinessUnitMsdynAnalyticsforcs = "business_unit_msdyn_analyticsforcs";
				public const string BusinessUnitMsdynAppconfiguration = "business_unit_msdyn_appconfiguration";
				public const string BusinessUnitMsdynApplicationextension = "business_unit_msdyn_applicationextension";
				public const string BusinessUnitMsdynApplicationtabtemplate = "business_unit_msdyn_applicationtabtemplate";
				public const string BusinessUnitMsdynAppprofilerolemapping = "business_unit_msdyn_appprofilerolemapping";
				public const string BusinessUnitMsdynApprovalset = "business_unit_msdyn_approvalset";
				public const string BusinessUnitMsdynAppstate = "business_unit_msdyn_appstate";
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
				public const string BusinessUnitMsdynAutonomouscasecreationrule = "business_unit_msdyn_autonomouscasecreationrule";
				public const string BusinessUnitMsdynBgjobledger = "business_unit_msdyn_bgjobledger";
				public const string BusinessUnitMsdynBookableresourceassociation = "business_unit_msdyn_bookableresourceassociation";
				public const string BusinessUnitMsdynBookableresourcebookingquicknote = "business_unit_msdyn_bookableresourcebookingquicknote";
				public const string BusinessUnitMsdynBookableresourcecapacityprofile = "business_unit_msdyn_bookableresourcecapacityprofile";
				public const string BusinessUnitMsdynBookingalertstatus = "business_unit_msdyn_bookingalertstatus";
				public const string BusinessUnitMsdynBookingchange = "business_unit_msdyn_bookingchange";
				public const string BusinessUnitMsdynBookingjournal = "business_unit_msdyn_bookingjournal";
				public const string BusinessUnitMsdynBookingrule = "business_unit_msdyn_bookingrule";
				public const string BusinessUnitMsdynBookingsetupmetadata = "business_unit_msdyn_bookingsetupmetadata";
				public const string BusinessUnitMsdynBookingtimestamp = "business_unit_msdyn_bookingtimestamp";
				public const string BusinessUnitMsdynBotsession = "business_unit_msdyn_botsession";
				public const string BusinessUnitMsdynBusinessclosure = "business_unit_msdyn_businessclosure";
				public const string BusinessUnitMsdynCallablecontext = "business_unit_msdyn_callablecontext";
				public const string BusinessUnitMsdynCapacityprofile = "business_unit_msdyn_capacityprofile";
				public const string BusinessUnitMsdynCdsentityengagementctx = "business_unit_msdyn_cdsentityengagementctx";
				public const string BusinessUnitMsdynChannel = "business_unit_msdyn_channel";
				public const string BusinessUnitMsdynChanneldefinition = "business_unit_msdyn_channeldefinition";
				public const string BusinessUnitMsdynChanneldefinitionconsent = "business_unit_msdyn_channeldefinitionconsent";
				public const string BusinessUnitMsdynChanneldefinitionlocale = "business_unit_msdyn_channeldefinitionlocale";
				public const string BusinessUnitMsdynChannelinstance = "business_unit_msdyn_channelinstance";
				public const string BusinessUnitMsdynChannelinstanceaccount = "business_unit_msdyn_channelinstanceaccount";
				public const string BusinessUnitMsdynChannelmessageattachment = "business_unit_msdyn_channelmessageattachment";
				public const string BusinessUnitMsdynChannelmessagecontextpart = "business_unit_msdyn_channelmessagecontextpart";
				public const string BusinessUnitMsdynChannelmessagepart = "business_unit_msdyn_channelmessagepart";
				public const string BusinessUnitMsdynChannelprovider = "business_unit_msdyn_channelprovider";
				public const string BusinessUnitMsdynCharacteristicreqforteammember = "business_unit_msdyn_characteristicreqforteammember";
				public const string BusinessUnitMsdynChatansweroption = "business_unit_msdyn_chatansweroption";
				public const string BusinessUnitMsdynChatquestionnaireresponse = "business_unit_msdyn_chatquestionnaireresponse";
				public const string BusinessUnitMsdynChatquestionnaireresponseitem = "business_unit_msdyn_chatquestionnaireresponseitem";
				public const string BusinessUnitMsdynChatwidgetlanguage = "business_unit_msdyn_chatwidgetlanguage";
				public const string BusinessUnitMsdynClientextension = "business_unit_msdyn_clientextension";
				public const string BusinessUnitMsdynConfiguration = "business_unit_msdyn_configuration";
				public const string BusinessUnitMsdynConsoleapplicationnotificationfield = "business_unit_msdyn_consoleapplicationnotificationfield";
				public const string BusinessUnitMsdynConsoleapplicationnotificationtemplate = "business_unit_msdyn_consoleapplicationnotificationtemplate";
				public const string BusinessUnitMsdynConsoleapplicationsessiontemplate = "business_unit_msdyn_consoleapplicationsessiontemplate";
				public const string BusinessUnitMsdynConsoleapplicationtemplate = "business_unit_msdyn_consoleapplicationtemplate";
				public const string BusinessUnitMsdynConsoleapplicationtemplateparameter = "business_unit_msdyn_consoleapplicationtemplateparameter";
				public const string BusinessUnitMsdynConsumingapplication = "business_unit_msdyn_consumingapplication";
				public const string BusinessUnitMsdynContactpricelist = "business_unit_msdyn_contactpricelist";
				public const string BusinessUnitMsdynContactsuggestionrule = "business_unit_msdyn_contactsuggestionrule";
				public const string BusinessUnitMsdynContactsuggestionruleset = "business_unit_msdyn_contactsuggestionruleset";
				public const string BusinessUnitMsdynContractlinedetailperformance = "business_unit_msdyn_contractlinedetailperformance";
				public const string BusinessUnitMsdynContractlinescheduleofvalue = "business_unit_msdyn_contractlinescheduleofvalue";
				public const string BusinessUnitMsdynContractperformance = "business_unit_msdyn_contractperformance";
				public const string BusinessUnitMsdynConversationaction = "business_unit_msdyn_conversationaction";
				public const string BusinessUnitMsdynConversationactionitem = "business_unit_msdyn_conversationactionitem";
				public const string BusinessUnitMsdynConversationactionlocale = "business_unit_msdyn_conversationactionlocale";
				public const string BusinessUnitMsdynConversationaggregatedinsights = "business_unit_msdyn_conversationaggregatedinsights";
				public const string BusinessUnitMsdynConversationcomment = "business_unit_msdyn_conversationcomment";
				public const string BusinessUnitMsdynConversationdata = "business_unit_msdyn_conversationdata";
				public const string BusinessUnitMsdynConversationinsight = "business_unit_msdyn_conversationinsight";
				public const string BusinessUnitMsdynConversationmessageblock = "business_unit_msdyn_conversationmessageblock";
				public const string BusinessUnitMsdynConversationparticipantinsights = "business_unit_msdyn_conversationparticipantinsights";
				public const string BusinessUnitMsdynConversationparticipantsentiment = "business_unit_msdyn_conversationparticipantsentiment";
				public const string BusinessUnitMsdynConversationquestion = "business_unit_msdyn_conversationquestion";
				public const string BusinessUnitMsdynConversationsegmentsentiment = "business_unit_msdyn_conversationsegmentsentiment";
				public const string BusinessUnitMsdynConversationsentiment = "business_unit_msdyn_conversationsentiment";
				public const string BusinessUnitMsdynConversationsignal = "business_unit_msdyn_conversationsignal";
				public const string BusinessUnitMsdynConversationsubject = "business_unit_msdyn_conversationsubject";
				public const string BusinessUnitMsdynConversationsummarysuggestion = "business_unit_msdyn_conversationsummarysuggestion";
				public const string BusinessUnitMsdynConversationsystemtag = "business_unit_msdyn_conversationsystemtag";
				public const string BusinessUnitMsdynConversationtag = "business_unit_msdyn_conversationtag";
				public const string BusinessUnitMsdynCopilotagentpreference = "business_unit_msdyn_copilotagentpreference";
				public const string BusinessUnitMsdynCopilotinteractiondata = "business_unit_msdyn_copilotinteractiondata";
				public const string BusinessUnitMsdynCopilottranscriptdata = "business_unit_msdyn_copilottranscriptdata";
				public const string BusinessUnitMsdynCrmconnection = "business_unit_msdyn_crmconnection";
				public const string BusinessUnitMsdynCsadminconfig = "business_unit_msdyn_csadminconfig";
				public const string BusinessUnitMsdynCustomapirulesetconfiguration = "business_unit_msdyn_customapirulesetconfiguration";
				public const string BusinessUnitMsdynCustomcontrolextendedsettings = "business_unit_msdyn_customcontrolextendedsettings";
				public const string BusinessUnitMsdynCustomengagementctx = "business_unit_msdyn_customengagementctx";
				public const string BusinessUnitMsdynCustomerasset = "business_unit_msdyn_customerasset";
				public const string BusinessUnitMsdynCustomerassetattachment = "business_unit_msdyn_customerassetattachment";
				public const string BusinessUnitMsdynCustomerassetcategory = "business_unit_msdyn_customerassetcategory";
				public const string BusinessUnitMsdynDataanalyticscustomizedreport = "business_unit_msdyn_dataanalyticscustomizedreport";
				public const string BusinessUnitMsdynDataanalyticsdataset = "business_unit_msdyn_dataanalyticsdataset";
				public const string BusinessUnitMsdynDataanalyticsreport = "business_unit_msdyn_dataanalyticsreport";
				public const string BusinessUnitMsdynDataanalyticsworkspace = "business_unit_msdyn_dataanalyticsworkspace";
				public const string BusinessUnitMsdynDataexport = "business_unit_msdyn_dataexport";
				public const string BusinessUnitMsdynDataflow = "business_unit_msdyn_dataflow";
				public const string BusinessUnitMsdynDataflowDatalakefolder = "business_unit_msdyn_dataflow_datalakefolder";
				public const string BusinessUnitMsdynDataflowconnectionreference = "business_unit_msdyn_dataflowconnectionreference";
				public const string BusinessUnitMsdynDataflowrefreshhistory = "business_unit_msdyn_dataflowrefreshhistory";
				public const string BusinessUnitMsdynDataflowtemplate = "business_unit_msdyn_dataflowtemplate";
				public const string BusinessUnitMsdynDealmanageraccess = "business_unit_msdyn_dealmanageraccess";
				public const string BusinessUnitMsdynDealmanagersettings = "business_unit_msdyn_dealmanagersettings";
				public const string BusinessUnitMsdynDecisioncontract = "business_unit_msdyn_decisioncontract";
				public const string BusinessUnitMsdynDecisionruleset = "business_unit_msdyn_decisionruleset";
				public const string BusinessUnitMsdynDefextendedchannelinstance = "business_unit_msdyn_defextendedchannelinstance";
				public const string BusinessUnitMsdynDefextendedchannelinstanceaccount = "business_unit_msdyn_defextendedchannelinstanceaccount";
				public const string BusinessUnitMsdynDelegation = "business_unit_msdyn_delegation";
				public const string BusinessUnitMsdynDeletedconversation = "business_unit_msdyn_deletedconversation";
				public const string BusinessUnitMsdynDimension = "business_unit_msdyn_dimension";
				public const string BusinessUnitMsdynDmsrequest = "business_unit_msdyn_dmsrequest";
				public const string BusinessUnitMsdynDmsrequeststatus = "business_unit_msdyn_dmsrequeststatus";
				public const string BusinessUnitMsdynDmssyncrequest = "business_unit_msdyn_dmssyncrequest";
				public const string BusinessUnitMsdynDmssyncstatus = "business_unit_msdyn_dmssyncstatus";
				public const string BusinessUnitMsdynDuplicateleadmapping = "business_unit_msdyn_duplicateleadmapping";
				public const string BusinessUnitMsdynEffortpredictionresult = "business_unit_msdyn_effortpredictionresult";
				public const string BusinessUnitMsdynEntitlementapplication = "business_unit_msdyn_entitlementapplication";
				public const string BusinessUnitMsdynEntityattachment = "business_unit_msdyn_entityattachment";
				public const string BusinessUnitMsdynEntityconfig = "business_unit_msdyn_entityconfig";
				public const string BusinessUnitMsdynEntityconfiguration = "business_unit_msdyn_entityconfiguration";
				public const string BusinessUnitMsdynEntitylinkchatconfiguration = "business_unit_msdyn_entitylinkchatconfiguration";
				public const string BusinessUnitMsdynEntityrankingrule = "business_unit_msdyn_entityrankingrule";
				public const string BusinessUnitMsdynEntityrefreshhistory = "business_unit_msdyn_entityrefreshhistory";
				public const string BusinessUnitMsdynEntityroutingconfiguration = "business_unit_msdyn_entityroutingconfiguration";
				public const string BusinessUnitMsdynEntityworkstreammap = "business_unit_msdyn_entityworkstreammap";
				public const string BusinessUnitMsdynEstimate = "business_unit_msdyn_estimate";
				public const string BusinessUnitMsdynEstimateline = "business_unit_msdyn_estimateline";
				public const string BusinessUnitMsdynExpense = "business_unit_msdyn_expense";
				public const string BusinessUnitMsdynExpensereceipt = "business_unit_msdyn_expensereceipt";
				public const string BusinessUnitMsdynExtendedusersetting = "business_unit_msdyn_extendedusersetting";
				public const string BusinessUnitMsdynFacebookengagementctx = "business_unit_msdyn_facebookengagementctx";
				public const string BusinessUnitMsdynFact = "business_unit_msdyn_fact";
				public const string BusinessUnitMsdynFavoriteknowledgearticle = "business_unit_msdyn_favoriteknowledgearticle";
				public const string BusinessUnitMsdynFederatedarticle = "business_unit_msdyn_federatedarticle";
				public const string BusinessUnitMsdynFieldcomputation = "business_unit_msdyn_fieldcomputation";
				public const string BusinessUnitMsdynFieldservicesetting = "business_unit_msdyn_fieldservicesetting";
				public const string BusinessUnitMsdynFieldserviceslaconfiguration = "business_unit_msdyn_fieldserviceslaconfiguration";
				public const string BusinessUnitMsdynFileupload = "business_unit_msdyn_fileupload";
				public const string BusinessUnitMsdynFindworkevent = "business_unit_msdyn_findworkevent";
				public const string BusinessUnitMsdynFlowcardtype = "business_unit_msdyn_flowcardtype";
				public const string BusinessUnitMsdynForecastconfiguration = "business_unit_msdyn_forecastconfiguration";
				public const string BusinessUnitMsdynForecastdefinition = "business_unit_msdyn_forecastdefinition";
				public const string BusinessUnitMsdynForecastinstance = "business_unit_msdyn_forecastinstance";
				public const string BusinessUnitMsdynForecastrecurrence = "business_unit_msdyn_forecastrecurrence";
				public const string BusinessUnitMsdynFormmapping = "business_unit_msdyn_formmapping";
				public const string BusinessUnitMsdynFunctionallocation = "business_unit_msdyn_functionallocation";
				public const string BusinessUnitMsdynFunctionallocationtype = "business_unit_msdyn_functionallocationtype";
				public const string BusinessUnitMsdynGdprdata = "business_unit_msdyn_gdprdata";
				public const string BusinessUnitMsdynGeofence = "business_unit_msdyn_geofence";
				public const string BusinessUnitMsdynGeofenceevent = "business_unit_msdyn_geofenceevent";
				public const string BusinessUnitMsdynGeofencingsettings = "business_unit_msdyn_geofencingsettings";
				public const string BusinessUnitMsdynIcdextension = "business_unit_msdyn_icdextension";
				public const string BusinessUnitMsdynIcebreakersconfig = "business_unit_msdyn_icebreakersconfig";
				public const string BusinessUnitMsdynIermlmodel = "business_unit_msdyn_iermlmodel";
				public const string BusinessUnitMsdynIermltraining = "business_unit_msdyn_iermltraining";
				public const string BusinessUnitMsdynIncidenttype = "business_unit_msdyn_incidenttype";
				public const string BusinessUnitMsdynIncidenttypeRequirementgroup = "business_unit_msdyn_incidenttype_requirementgroup";
				public const string BusinessUnitMsdynIncidenttypecharacteristic = "business_unit_msdyn_incidenttypecharacteristic";
				public const string BusinessUnitMsdynIncidenttypeproduct = "business_unit_msdyn_incidenttypeproduct";
				public const string BusinessUnitMsdynIncidenttyperecommendationresult = "business_unit_msdyn_incidenttyperecommendationresult";
				public const string BusinessUnitMsdynIncidenttyperecommendationrunhistory = "business_unit_msdyn_incidenttyperecommendationrunhistory";
				public const string BusinessUnitMsdynIncidenttyperesolution = "business_unit_msdyn_incidenttyperesolution";
				public const string BusinessUnitMsdynIncidenttypeservice = "business_unit_msdyn_incidenttypeservice";
				public const string BusinessUnitMsdynIncidenttypeservicetask = "business_unit_msdyn_incidenttypeservicetask";
				public const string BusinessUnitMsdynIncidenttypessetup = "business_unit_msdyn_incidenttypessetup";
				public const string BusinessUnitMsdynInspection = "business_unit_msdyn_inspection";
				public const string BusinessUnitMsdynInspectionattachment = "business_unit_msdyn_inspectionattachment";
				public const string BusinessUnitMsdynInspectiondefinition = "business_unit_msdyn_inspectiondefinition";
				public const string BusinessUnitMsdynInspectioninstance = "business_unit_msdyn_inspectioninstance";
				public const string BusinessUnitMsdynInspectionresponse = "business_unit_msdyn_inspectionresponse";
				public const string BusinessUnitMsdynInsurance = "business_unit_msdyn_insurance";
				public const string BusinessUnitMsdynIntegratedsearchprovider = "business_unit_msdyn_integratedsearchprovider";
				public const string BusinessUnitMsdynIntegrationjob = "business_unit_msdyn_integrationjob";
				public const string BusinessUnitMsdynIntegrationjobdetail = "business_unit_msdyn_integrationjobdetail";
				public const string BusinessUnitMsdynIntent = "business_unit_msdyn_intent";
				public const string BusinessUnitMsdynInventoryadjustment = "business_unit_msdyn_inventoryadjustment";
				public const string BusinessUnitMsdynInventoryadjustmentproduct = "business_unit_msdyn_inventoryadjustmentproduct";
				public const string BusinessUnitMsdynInventoryjournal = "business_unit_msdyn_inventoryjournal";
				public const string BusinessUnitMsdynInventorytransfer = "business_unit_msdyn_inventorytransfer";
				public const string BusinessUnitMsdynInvoicelinetransaction = "business_unit_msdyn_invoicelinetransaction";
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
				public const string BusinessUnitMsdynJobsstate = "business_unit_msdyn_jobsstate";
				public const string BusinessUnitMsdynJournal = "business_unit_msdyn_journal";
				public const string BusinessUnitMsdynJournalline = "business_unit_msdyn_journalline";
				public const string BusinessUnitMsdynKalanguagesetting = "business_unit_msdyn_kalanguagesetting";
				public const string BusinessUnitMsdynKbattachment = "business_unit_msdyn_kbattachment";
				public const string BusinessUnitMsdynKmfederatedsearchconfig = "business_unit_msdyn_kmfederatedsearchconfig";
				public const string BusinessUnitMsdynKnowledgearticleimage = "business_unit_msdyn_knowledgearticleimage";
				public const string BusinessUnitMsdynKnowledgearticletemplate = "business_unit_msdyn_knowledgearticletemplate";
				public const string BusinessUnitMsdynKnowledgeassetconfiguration = "business_unit_msdyn_knowledgeassetconfiguration";
				public const string BusinessUnitMsdynKnowledgeinteractioninsight = "business_unit_msdyn_knowledgeinteractioninsight";
				public const string BusinessUnitMsdynKnowledgemanagementsetting = "business_unit_msdyn_knowledgemanagementsetting";
				public const string BusinessUnitMsdynKnowledgepersonalfilter = "business_unit_msdyn_knowledgepersonalfilter";
				public const string BusinessUnitMsdynKnowledgesearchfilter = "business_unit_msdyn_knowledgesearchfilter";
				public const string BusinessUnitMsdynKnowledgesearchinsight = "business_unit_msdyn_knowledgesearchinsight";
				public const string BusinessUnitMsdynKpieventdata = "business_unit_msdyn_kpieventdata";
				public const string BusinessUnitMsdynKpieventdefinition = "business_unit_msdyn_kpieventdefinition";
				public const string BusinessUnitMsdynLeadmodelconfig = "business_unit_msdyn_leadmodelconfig";
				public const string BusinessUnitMsdynLineengagementctx = "business_unit_msdyn_lineengagementctx";
				public const string BusinessUnitMsdynLivechatconfig = "business_unit_msdyn_livechatconfig";
				public const string BusinessUnitMsdynLivechatengagementctx = "business_unit_msdyn_livechatengagementctx";
				public const string BusinessUnitMsdynLivechatwidgetlocation = "business_unit_msdyn_livechatwidgetlocation";
				public const string BusinessUnitMsdynLiveconversation = "business_unit_msdyn_liveconversation";
				public const string BusinessUnitMsdynLiveworkitemevent = "business_unit_msdyn_liveworkitemevent";
				public const string BusinessUnitMsdynLiveworkstream = "business_unit_msdyn_liveworkstream";
				public const string BusinessUnitMsdynLiveworkstreamcapacityprofile = "business_unit_msdyn_liveworkstreamcapacityprofile";
				public const string BusinessUnitMsdynLocationtemplateassociation = "business_unit_msdyn_locationtemplateassociation";
				public const string BusinessUnitMsdynLocationtypetemplateassociation = "business_unit_msdyn_locationtypetemplateassociation";
				public const string BusinessUnitMsdynLockstatus = "business_unit_msdyn_lockstatus";
				public const string BusinessUnitMsdynMacrosession = "business_unit_msdyn_macrosession";
				public const string BusinessUnitMsdynMasterentityroutingconfiguration = "business_unit_msdyn_masterentityroutingconfiguration";
				public const string BusinessUnitMsdynMigrationtracker = "business_unit_msdyn_migrationtracker";
				public const string BusinessUnitMsdynMobileapp = "business_unit_msdyn_mobileapp";
				public const string BusinessUnitMsdynMobilesource = "business_unit_msdyn_mobilesource";
				public const string BusinessUnitMsdynModelpreviewstatus = "business_unit_msdyn_modelpreviewstatus";
				public const string BusinessUnitMsdynMsteamssetting = "business_unit_msdyn_msteamssetting";
				public const string BusinessUnitMsdynNotesanalysisconfig = "business_unit_msdyn_notesanalysisconfig";
				public const string BusinessUnitMsdynNotificationfield = "business_unit_msdyn_notificationfield";
				public const string BusinessUnitMsdynNotificationtemplate = "business_unit_msdyn_notificationtemplate";
				public const string BusinessUnitMsdynNottoexceed = "business_unit_msdyn_nottoexceed";
				public const string BusinessUnitMsdynOcGeolocationprovider = "business_unit_msdyn_oc_geolocationprovider";
				public const string BusinessUnitMsdynOcagentassignedcustomapiprivilege = "business_unit_msdyn_ocagentassignedcustomapiprivilege";
				public const string BusinessUnitMsdynOcapplebusinessaccount = "business_unit_msdyn_ocapplebusinessaccount";
				public const string BusinessUnitMsdynOcapplemessagesforbusinessengagementctx = "business_unit_msdyn_ocapplemessagesforbusinessengagementctx";
				public const string BusinessUnitMsdynOcapplepay = "business_unit_msdyn_ocapplepay";
				public const string BusinessUnitMsdynOcautoblockrule = "business_unit_msdyn_ocautoblockrule";
				public const string BusinessUnitMsdynOcbotchannelregistration = "business_unit_msdyn_ocbotchannelregistration";
				public const string BusinessUnitMsdynOcbotchannelregistrationsecret = "business_unit_msdyn_ocbotchannelregistrationsecret";
				public const string BusinessUnitMsdynOccarrier = "business_unit_msdyn_occarrier";
				public const string BusinessUnitMsdynOcchannelapiconversationprivilege = "business_unit_msdyn_occhannelapiconversationprivilege";
				public const string BusinessUnitMsdynOcchannelapimessageprivilege = "business_unit_msdyn_occhannelapimessageprivilege";
				public const string BusinessUnitMsdynOcchannelapimethodmapping = "business_unit_msdyn_occhannelapimethodmapping";
				public const string BusinessUnitMsdynOccommunicationprovidersetting = "business_unit_msdyn_occommunicationprovidersetting";
				public const string BusinessUnitMsdynOccommunicationprovidersettingentry = "business_unit_msdyn_occommunicationprovidersettingentry";
				public const string BusinessUnitMsdynOccustommessagingchannel = "business_unit_msdyn_occustommessagingchannel";
				public const string BusinessUnitMsdynOcexternalcontext = "business_unit_msdyn_ocexternalcontext";
				public const string BusinessUnitMsdynOcfbapplication = "business_unit_msdyn_ocfbapplication";
				public const string BusinessUnitMsdynOcfbpage = "business_unit_msdyn_ocfbpage";
				public const string BusinessUnitMsdynOcflaggedspam = "business_unit_msdyn_ocflaggedspam";
				public const string BusinessUnitMsdynOcgooglebusinessmessagesagentaccount = "business_unit_msdyn_ocgooglebusinessmessagesagentaccount";
				public const string BusinessUnitMsdynOcgooglebusinessmessagesengagementctx = "business_unit_msdyn_ocgooglebusinessmessagesengagementctx";
				public const string BusinessUnitMsdynOcgooglebusinessmessagespartneraccount = "business_unit_msdyn_ocgooglebusinessmessagespartneraccount";
				public const string BusinessUnitMsdynOclanguage = "business_unit_msdyn_oclanguage";
				public const string BusinessUnitMsdynOclinechannelconfig = "business_unit_msdyn_oclinechannelconfig";
				public const string BusinessUnitMsdynOcliveworkitemcapacityprofile = "business_unit_msdyn_ocliveworkitemcapacityprofile";
				public const string BusinessUnitMsdynOcliveworkitemcharacteristic = "business_unit_msdyn_ocliveworkitemcharacteristic";
				public const string BusinessUnitMsdynOcliveworkitemcontextitem = "business_unit_msdyn_ocliveworkitemcontextitem";
				public const string BusinessUnitMsdynOcliveworkitemparticipant = "business_unit_msdyn_ocliveworkitemparticipant";
				public const string BusinessUnitMsdynOcliveworkitemsentiment = "business_unit_msdyn_ocliveworkitemsentiment";
				public const string BusinessUnitMsdynOcliveworkstreamcontextvariable = "business_unit_msdyn_ocliveworkstreamcontextvariable";
				public const string BusinessUnitMsdynOcoutboundconfiguration = "business_unit_msdyn_ocoutboundconfiguration";
				public const string BusinessUnitMsdynOcpaymentprofile = "business_unit_msdyn_ocpaymentprofile";
				public const string BusinessUnitMsdynOcphonenumber = "business_unit_msdyn_ocphonenumber";
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
				public const string BusinessUnitMsdynOcsmssettingsecret = "business_unit_msdyn_ocsmssettingsecret";
				public const string BusinessUnitMsdynOcteamschannelconfig = "business_unit_msdyn_octeamschannelconfig";
				public const string BusinessUnitMsdynOctwitterapplication = "business_unit_msdyn_octwitterapplication";
				public const string BusinessUnitMsdynOctwitterhandle = "business_unit_msdyn_octwitterhandle";
				public const string BusinessUnitMsdynOctwitterhandleprovisioningstatus = "business_unit_msdyn_octwitterhandleprovisioningstatus";
				public const string BusinessUnitMsdynOctwitterhandlesecret = "business_unit_msdyn_octwitterhandlesecret";
				public const string BusinessUnitMsdynOcwechatchannelconfig = "business_unit_msdyn_ocwechatchannelconfig";
				public const string BusinessUnitMsdynOcwhatsappchannelaccount = "business_unit_msdyn_ocwhatsappchannelaccount";
				public const string BusinessUnitMsdynOcwhatsappchannelnumber = "business_unit_msdyn_ocwhatsappchannelnumber";
				public const string BusinessUnitMsdynOmnichannelpersonalization = "business_unit_msdyn_omnichannelpersonalization";
				public const string BusinessUnitMsdynOmnichannelqueue = "business_unit_msdyn_omnichannelqueue";
				public const string BusinessUnitMsdynOmnichannelsyncconfig = "business_unit_msdyn_omnichannelsyncconfig";
				public const string BusinessUnitMsdynOperatinghour = "business_unit_msdyn_operatinghour";
				public const string BusinessUnitMsdynOpportunitylineresourcecategory = "business_unit_msdyn_opportunitylineresourcecategory";
				public const string BusinessUnitMsdynOpportunitylinetransaction = "business_unit_msdyn_opportunitylinetransaction";
				public const string BusinessUnitMsdynOpportunitylinetransactioncategory = "business_unit_msdyn_opportunitylinetransactioncategory";
				public const string BusinessUnitMsdynOpportunitylinetransactionclassificatio = "business_unit_msdyn_opportunitylinetransactionclassificatio";
				public const string BusinessUnitMsdynOpportunitymodelconfig = "business_unit_msdyn_opportunitymodelconfig";
				public const string BusinessUnitMsdynOpportunitypricelist = "business_unit_msdyn_opportunitypricelist";
				public const string BusinessUnitMsdynOrderinvoicingdate = "business_unit_msdyn_orderinvoicingdate";
				public const string BusinessUnitMsdynOrderinvoicingproduct = "business_unit_msdyn_orderinvoicingproduct";
				public const string BusinessUnitMsdynOrderinvoicingsetup = "business_unit_msdyn_orderinvoicingsetup";
				public const string BusinessUnitMsdynOrderinvoicingsetupdate = "business_unit_msdyn_orderinvoicingsetupdate";
				public const string BusinessUnitMsdynOrderlineresourcecategory = "business_unit_msdyn_orderlineresourcecategory";
				public const string BusinessUnitMsdynOrderlinetransaction = "business_unit_msdyn_orderlinetransaction";
				public const string BusinessUnitMsdynOrderlinetransactioncategory = "business_unit_msdyn_orderlinetransactioncategory";
				public const string BusinessUnitMsdynOrderlinetransactionclassification = "business_unit_msdyn_orderlinetransactionclassification";
				public const string BusinessUnitMsdynOrderpricelist = "business_unit_msdyn_orderpricelist";
				public const string BusinessUnitMsdynOrgchartnode = "business_unit_msdyn_orgchartnode";
				public const string BusinessUnitMsdynOverflowactionconfig = "business_unit_msdyn_overflowactionconfig";
				public const string BusinessUnitMsdynPayment = "business_unit_msdyn_payment";
				public const string BusinessUnitMsdynPaymentdetail = "business_unit_msdyn_paymentdetail";
				public const string BusinessUnitMsdynPaymentmethod = "business_unit_msdyn_paymentmethod";
				public const string BusinessUnitMsdynPaymentterm = "business_unit_msdyn_paymentterm";
				public const string BusinessUnitMsdynPersonalmessage = "business_unit_msdyn_personalmessage";
				public const string BusinessUnitMsdynPersonalsoundsetting = "business_unit_msdyn_personalsoundsetting";
				public const string BusinessUnitMsdynPlaybookactivity = "business_unit_msdyn_playbookactivity";
				public const string BusinessUnitMsdynPlaybookactivityattribute = "business_unit_msdyn_playbookactivityattribute";
				public const string BusinessUnitMsdynPlaybookcategory = "business_unit_msdyn_playbookcategory";
				public const string BusinessUnitMsdynPlaybookinstance = "business_unit_msdyn_playbookinstance";
				public const string BusinessUnitMsdynPlaybooktemplate = "business_unit_msdyn_playbooktemplate";
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
				public const string BusinessUnitMsdynPmtemplate = "business_unit_msdyn_pmtemplate";
				public const string BusinessUnitMsdynPmview = "business_unit_msdyn_pmview";
				public const string BusinessUnitMsdynPostalbum = "business_unit_msdyn_postalbum";
				public const string BusinessUnitMsdynPostalcode = "business_unit_msdyn_postalcode";
				public const string BusinessUnitMsdynPredictioncomputationoperation = "business_unit_msdyn_predictioncomputationoperation";
				public const string BusinessUnitMsdynPredictionmodelstatus = "business_unit_msdyn_predictionmodelstatus";
				public const string BusinessUnitMsdynPredictionscheduledoperation = "business_unit_msdyn_predictionscheduledoperation";
				public const string BusinessUnitMsdynPredictivescoringsyncstatus = "business_unit_msdyn_predictivescoringsyncstatus";
				public const string BusinessUnitMsdynPreferredagent = "business_unit_msdyn_preferredagent";
				public const string BusinessUnitMsdynPreferredagentcustomeridentity = "business_unit_msdyn_preferredagentcustomeridentity";
				public const string BusinessUnitMsdynPreferredagentroutedentity = "business_unit_msdyn_preferredagentroutedentity";
				public const string BusinessUnitMsdynPriority = "business_unit_msdyn_priority";
				public const string BusinessUnitMsdynProblematicasset = "business_unit_msdyn_problematicasset";
				public const string BusinessUnitMsdynProblematicassetfeedback = "business_unit_msdyn_problematicassetfeedback";
				public const string BusinessUnitMsdynProductivityactioninputparameter = "business_unit_msdyn_productivityactioninputparameter";
				public const string BusinessUnitMsdynProductivityactionoutputparameter = "business_unit_msdyn_productivityactionoutputparameter";
				public const string BusinessUnitMsdynProductivityagentscript = "business_unit_msdyn_productivityagentscript";
				public const string BusinessUnitMsdynProductivityagentscriptstep = "business_unit_msdyn_productivityagentscriptstep";
				public const string BusinessUnitMsdynProductivitymacroactiontemplate = "business_unit_msdyn_productivitymacroactiontemplate";
				public const string BusinessUnitMsdynProductivitymacroconnector = "business_unit_msdyn_productivitymacroconnector";
				public const string BusinessUnitMsdynProductivitymacrosolutionconfiguration = "business_unit_msdyn_productivitymacrosolutionconfiguration";
				public const string BusinessUnitMsdynProductivityparameterdefinition = "business_unit_msdyn_productivityparameterdefinition";
				public const string BusinessUnitMsdynProject = "business_unit_msdyn_project";
				public const string BusinessUnitMsdynProjectapproval = "business_unit_msdyn_projectapproval";
				public const string BusinessUnitMsdynProjectpricelist = "business_unit_msdyn_projectpricelist";
				public const string BusinessUnitMsdynProjecttask = "business_unit_msdyn_projecttask";
				public const string BusinessUnitMsdynProjecttaskdependency = "business_unit_msdyn_projecttaskdependency";
				public const string BusinessUnitMsdynProjecttaskstatususer = "business_unit_msdyn_projecttaskstatususer";
				public const string BusinessUnitMsdynProjectteam = "business_unit_msdyn_projectteam";
				public const string BusinessUnitMsdynProjecttransactioncategory = "business_unit_msdyn_projecttransactioncategory";
				public const string BusinessUnitMsdynProperty = "business_unit_msdyn_property";
				public const string BusinessUnitMsdynPropertyassetassociation = "business_unit_msdyn_propertyassetassociation";
				public const string BusinessUnitMsdynPropertylocationassociation = "business_unit_msdyn_propertylocationassociation";
				public const string BusinessUnitMsdynPropertylog = "business_unit_msdyn_propertylog";
				public const string BusinessUnitMsdynPropertytemplateassociation = "business_unit_msdyn_propertytemplateassociation";
				public const string BusinessUnitMsdynPurchaseorder = "business_unit_msdyn_purchaseorder";
				public const string BusinessUnitMsdynPurchaseorderbill = "business_unit_msdyn_purchaseorderbill";
				public const string BusinessUnitMsdynPurchaseorderproduct = "business_unit_msdyn_purchaseorderproduct";
				public const string BusinessUnitMsdynPurchaseorderreceipt = "business_unit_msdyn_purchaseorderreceipt";
				public const string BusinessUnitMsdynPurchaseorderreceiptproduct = "business_unit_msdyn_purchaseorderreceiptproduct";
				public const string BusinessUnitMsdynPurchaseordersubstatus = "business_unit_msdyn_purchaseordersubstatus";
				public const string BusinessUnitMsdynQuestionsequence = "business_unit_msdyn_questionsequence";
				public const string BusinessUnitMsdynQuotebookingincident = "business_unit_msdyn_quotebookingincident";
				public const string BusinessUnitMsdynQuotebookingproduct = "business_unit_msdyn_quotebookingproduct";
				public const string BusinessUnitMsdynQuotebookingservice = "business_unit_msdyn_quotebookingservice";
				public const string BusinessUnitMsdynQuotebookingservicetask = "business_unit_msdyn_quotebookingservicetask";
				public const string BusinessUnitMsdynQuotebookingsetup = "business_unit_msdyn_quotebookingsetup";
				public const string BusinessUnitMsdynQuoteinvoicingproduct = "business_unit_msdyn_quoteinvoicingproduct";
				public const string BusinessUnitMsdynQuoteinvoicingsetup = "business_unit_msdyn_quoteinvoicingsetup";
				public const string BusinessUnitMsdynQuotelineanalyticsbreakdown = "business_unit_msdyn_quotelineanalyticsbreakdown";
				public const string BusinessUnitMsdynQuotelineresourcecategory = "business_unit_msdyn_quotelineresourcecategory";
				public const string BusinessUnitMsdynQuotelinescheduleofvalue = "business_unit_msdyn_quotelinescheduleofvalue";
				public const string BusinessUnitMsdynQuotelinetransaction = "business_unit_msdyn_quotelinetransaction";
				public const string BusinessUnitMsdynQuotelinetransactioncategory = "business_unit_msdyn_quotelinetransactioncategory";
				public const string BusinessUnitMsdynQuotelinetransactionclassification = "business_unit_msdyn_quotelinetransactionclassification";
				public const string BusinessUnitMsdynQuotepricelist = "business_unit_msdyn_quotepricelist";
				public const string BusinessUnitMsdynReadtracker = "business_unit_msdyn_readtracker";
				public const string BusinessUnitMsdynRealtimescoring = "business_unit_msdyn_realtimescoring";
				public const string BusinessUnitMsdynRealtimescoringoperation = "business_unit_msdyn_realtimescoringoperation";
				public const string BusinessUnitMsdynRecording = "business_unit_msdyn_recording";
				public const string BusinessUnitMsdynRelationshipinsightsunifiedconfig = "business_unit_msdyn_relationshipinsightsunifiedconfig";
				public const string BusinessUnitMsdynReportbookmark = "business_unit_msdyn_reportbookmark";
				public const string BusinessUnitMsdynRequirementchange = "business_unit_msdyn_requirementchange";
				public const string BusinessUnitMsdynRequirementcharacteristic = "business_unit_msdyn_requirementcharacteristic";
				public const string BusinessUnitMsdynRequirementdependency = "business_unit_msdyn_requirementdependency";
				public const string BusinessUnitMsdynRequirementgroup = "business_unit_msdyn_requirementgroup";
				public const string BusinessUnitMsdynRequirementorganizationunit = "business_unit_msdyn_requirementorganizationunit";
				public const string BusinessUnitMsdynRequirementrelationship = "business_unit_msdyn_requirementrelationship";
				public const string BusinessUnitMsdynRequirementresourcecategory = "business_unit_msdyn_requirementresourcecategory";
				public const string BusinessUnitMsdynRequirementresourcepreference = "business_unit_msdyn_requirementresourcepreference";
				public const string BusinessUnitMsdynRequirementstatus = "business_unit_msdyn_requirementstatus";
				public const string BusinessUnitMsdynResolution = "business_unit_msdyn_resolution";
				public const string BusinessUnitMsdynResourceassignment = "business_unit_msdyn_resourceassignment";
				public const string BusinessUnitMsdynResourceassignmentdetail = "business_unit_msdyn_resourceassignmentdetail";
				public const string BusinessUnitMsdynResourcepaytype = "business_unit_msdyn_resourcepaytype";
				public const string BusinessUnitMsdynResourcerequest = "business_unit_msdyn_resourcerequest";
				public const string BusinessUnitMsdynResourcerequirement = "business_unit_msdyn_resourcerequirement";
				public const string BusinessUnitMsdynResourcerequirementdetail = "business_unit_msdyn_resourcerequirementdetail";
				public const string BusinessUnitMsdynResourceterritory = "business_unit_msdyn_resourceterritory";
				public const string BusinessUnitMsdynRichtextfile = "business_unit_msdyn_richtextfile";
				public const string BusinessUnitMsdynRma = "business_unit_msdyn_rma";
				public const string BusinessUnitMsdynRmaproduct = "business_unit_msdyn_rmaproduct";
				public const string BusinessUnitMsdynRmareceipt = "business_unit_msdyn_rmareceipt";
				public const string BusinessUnitMsdynRmareceiptproduct = "business_unit_msdyn_rmareceiptproduct";
				public const string BusinessUnitMsdynRmasubstatus = "business_unit_msdyn_rmasubstatus";
				public const string BusinessUnitMsdynRolecompetencyrequirement = "business_unit_msdyn_rolecompetencyrequirement";
				public const string BusinessUnitMsdynRoleutilization = "business_unit_msdyn_roleutilization";
				public const string BusinessUnitMsdynRoutingconfiguration = "business_unit_msdyn_routingconfiguration";
				public const string BusinessUnitMsdynRoutingconfigurationstep = "business_unit_msdyn_routingconfigurationstep";
				public const string BusinessUnitMsdynRoutingrequest = "business_unit_msdyn_routingrequest";
				public const string BusinessUnitMsdynRtv = "business_unit_msdyn_rtv";
				public const string BusinessUnitMsdynRtvproduct = "business_unit_msdyn_rtvproduct";
				public const string BusinessUnitMsdynRtvsubstatus = "business_unit_msdyn_rtvsubstatus";
				public const string BusinessUnitMsdynRulesetdependencymapping = "business_unit_msdyn_rulesetdependencymapping";
				public const string BusinessUnitMsdynSalescopilotinsight = "business_unit_msdyn_salescopilotinsight";
				public const string BusinessUnitMsdynSalesforcestructuredobject = "business_unit_msdyn_salesforcestructuredobject";
				public const string BusinessUnitMsdynSalesforcestructuredqnaconfig = "business_unit_msdyn_salesforcestructuredqnaconfig";
				public const string BusinessUnitMsdynSalesinsightssettings = "business_unit_msdyn_salesinsightssettings";
				public const string BusinessUnitMsdynSalesocmessage = "business_unit_msdyn_salesocmessage";
				public const string BusinessUnitMsdynSalesocsmstemplate = "business_unit_msdyn_salesocsmstemplate";
				public const string BusinessUnitMsdynSalesroutingrun = "business_unit_msdyn_salesroutingrun";
				public const string BusinessUnitMsdynSalessuggestion = "business_unit_msdyn_salessuggestion";
				public const string BusinessUnitMsdynSalestag = "business_unit_msdyn_salestag";
				public const string BusinessUnitMsdynScenario = "business_unit_msdyn_scenario";
				public const string BusinessUnitMsdynSchedule = "business_unit_msdyn_schedule";
				public const string BusinessUnitMsdynScheduleboardsetting = "business_unit_msdyn_scheduleboardsetting";
				public const string BusinessUnitMsdynSchedulingfeatureflag = "business_unit_msdyn_schedulingfeatureflag";
				public const string BusinessUnitMsdynSciconversation = "business_unit_msdyn_sciconversation";
				public const string BusinessUnitMsdynScicustomemailhighlight = "business_unit_msdyn_scicustomemailhighlight";
				public const string BusinessUnitMsdynScicustomhighlight = "business_unit_msdyn_scicustomhighlight";
				public const string BusinessUnitMsdynScicustompublisher = "business_unit_msdyn_scicustompublisher";
				public const string BusinessUnitMsdynSciusersettings = "business_unit_msdyn_sciusersettings";
				public const string BusinessUnitMsdynSearchconfiguration = "business_unit_msdyn_searchconfiguration";
				public const string BusinessUnitMsdynSegment = "business_unit_msdyn_segment";
				public const string BusinessUnitMsdynSequence = "business_unit_msdyn_sequence";
				public const string BusinessUnitMsdynSequencestat = "business_unit_msdyn_sequencestat";
				public const string BusinessUnitMsdynSequencetarget = "business_unit_msdyn_sequencetarget";
				public const string BusinessUnitMsdynSequencetargetstep = "business_unit_msdyn_sequencetargetstep";
				public const string BusinessUnitMsdynSequencetemplate = "business_unit_msdyn_sequencetemplate";
				public const string BusinessUnitMsdynServiceconfiguration = "business_unit_msdyn_serviceconfiguration";
				public const string BusinessUnitMsdynServiceoneprovisioningrequest = "business_unit_msdyn_serviceoneprovisioningrequest";
				public const string BusinessUnitMsdynServicetasktype = "business_unit_msdyn_servicetasktype";
				public const string BusinessUnitMsdynSessiondata = "business_unit_msdyn_sessiondata";
				public const string BusinessUnitMsdynSessionevent = "business_unit_msdyn_sessionevent";
				public const string BusinessUnitMsdynSessionparticipant = "business_unit_msdyn_sessionparticipant";
				public const string BusinessUnitMsdynSessionparticipantdata = "business_unit_msdyn_sessionparticipantdata";
				public const string BusinessUnitMsdynSessiontemplate = "business_unit_msdyn_sessiontemplate";
				public const string BusinessUnitMsdynShipvia = "business_unit_msdyn_shipvia";
				public const string BusinessUnitMsdynSiconfig = "business_unit_msdyn_siconfig";
				public const string BusinessUnitMsdynSkillattachmentruleitem = "business_unit_msdyn_skillattachmentruleitem";
				public const string BusinessUnitMsdynSkillattachmenttarget = "business_unit_msdyn_skillattachmenttarget";
				public const string BusinessUnitMsdynSlakpi = "business_unit_msdyn_slakpi";
				public const string BusinessUnitMsdynSmsengagementctx = "business_unit_msdyn_smsengagementctx";
				public const string BusinessUnitMsdynSmsnumber = "business_unit_msdyn_smsnumber";
				public const string BusinessUnitMsdynSolutionhealthrule = "business_unit_msdyn_solutionhealthrule";
				public const string BusinessUnitMsdynSolutionhealthruleargument = "business_unit_msdyn_solutionhealthruleargument";
				public const string BusinessUnitMsdynSoundnotificationsetting = "business_unit_msdyn_soundnotificationsetting";
				public const string BusinessUnitMsdynSubmodeldefinition = "business_unit_msdyn_submodeldefinition";
				public const string BusinessUnitMsdynSuggestionassignmentrule = "business_unit_msdyn_suggestionassignmentrule";
				public const string BusinessUnitMsdynSuggestionprincipalobjectaccess = "business_unit_msdyn_suggestionprincipalobjectaccess";
				public const string BusinessUnitMsdynSuggestionsellerpriority = "business_unit_msdyn_suggestionsellerpriority";
				public const string BusinessUnitMsdynSurveyquestion = "business_unit_msdyn_surveyquestion";
				public const string BusinessUnitMsdynSwarm = "business_unit_msdyn_swarm";
				public const string BusinessUnitMsdynSwarmparticipant = "business_unit_msdyn_swarmparticipant";
				public const string BusinessUnitMsdynSwarmparticipantrule = "business_unit_msdyn_swarmparticipantrule";
				public const string BusinessUnitMsdynSwarmrole = "business_unit_msdyn_swarmrole";
				public const string BusinessUnitMsdynSwarmskill = "business_unit_msdyn_swarmskill";
				public const string BusinessUnitMsdynSwarmtemplate = "business_unit_msdyn_swarmtemplate";
				public const string BusinessUnitMsdynSystemuserschedulersetting = "business_unit_msdyn_systemuserschedulersetting";
				public const string BusinessUnitMsdynTaggedrecord = "business_unit_msdyn_taggedrecord";
				public const string BusinessUnitMsdynTaxcode = "business_unit_msdyn_taxcode";
				public const string BusinessUnitMsdynTaxcodedetail = "business_unit_msdyn_taxcodedetail";
				public const string BusinessUnitMsdynTeamschannelengagementctx = "business_unit_msdyn_teamschannelengagementctx";
				public const string BusinessUnitMsdynTeamsengagementctx = "business_unit_msdyn_teamsengagementctx";
				public const string BusinessUnitMsdynTemplateforproperties = "business_unit_msdyn_templateforproperties";
				public const string BusinessUnitMsdynTemplateparameter = "business_unit_msdyn_templateparameter";
				public const string BusinessUnitMsdynTemplatetags = "business_unit_msdyn_templatetags";
				public const string BusinessUnitMsdynTimeentry = "business_unit_msdyn_timeentry";
				public const string BusinessUnitMsdynTimeentrysetting = "business_unit_msdyn_timeentrysetting";
				public const string BusinessUnitMsdynTimegroup = "business_unit_msdyn_timegroup";
				public const string BusinessUnitMsdynTimegroupdetail = "business_unit_msdyn_timegroupdetail";
				public const string BusinessUnitMsdynTimeoffcalendar = "business_unit_msdyn_timeoffcalendar";
				public const string BusinessUnitMsdynTimeoffrequest = "business_unit_msdyn_timeoffrequest";
				public const string BusinessUnitMsdynTimespent = "business_unit_msdyn_timespent";
				public const string BusinessUnitMsdynTrade = "business_unit_msdyn_trade";
				public const string BusinessUnitMsdynTradecoverage = "business_unit_msdyn_tradecoverage";
				public const string BusinessUnitMsdynTrainingresult = "business_unit_msdyn_trainingresult";
				public const string BusinessUnitMsdynTransactionconnection = "business_unit_msdyn_transactionconnection";
				public const string BusinessUnitMsdynTransactionorigin = "business_unit_msdyn_transactionorigin";
				public const string BusinessUnitMsdynTranscript = "business_unit_msdyn_transcript";
				public const string BusinessUnitMsdynTwitterengagementctx = "business_unit_msdyn_twitterengagementctx";
				public const string BusinessUnitMsdynUnifiedroutingdiagnostic = "business_unit_msdyn_unifiedroutingdiagnostic";
				public const string BusinessUnitMsdynUnifiedroutingrun = "business_unit_msdyn_unifiedroutingrun";
				public const string BusinessUnitMsdynUntrackedappointment = "business_unit_msdyn_untrackedappointment";
				public const string BusinessUnitMsdynUrnotificationtemplate = "business_unit_msdyn_urnotificationtemplate";
				public const string BusinessUnitMsdynUrnotificationtemplatemapping = "business_unit_msdyn_urnotificationtemplatemapping";
				public const string BusinessUnitMsdynUserworkhistory = "business_unit_msdyn_userworkhistory";
				public const string BusinessUnitMsdynVirtualtablecolumncandidate = "business_unit_msdyn_virtualtablecolumncandidate";
				public const string BusinessUnitMsdynVisitorjourney = "business_unit_msdyn_visitorjourney";
				public const string BusinessUnitMsdynVivacustomerlist = "business_unit_msdyn_vivacustomerlist";
				public const string BusinessUnitMsdynVivausersetting = "business_unit_msdyn_vivausersetting";
				public const string BusinessUnitMsdynWallsavedqueryusersettings = "business_unit_msdyn_wallsavedqueryusersettings";
				public const string BusinessUnitMsdynWarehouse = "business_unit_msdyn_warehouse";
				public const string BusinessUnitMsdynWarranty = "business_unit_msdyn_warranty";
				public const string BusinessUnitMsdynWechatengagementctx = "business_unit_msdyn_wechatengagementctx";
				public const string BusinessUnitMsdynWhatsappengagementctx = "business_unit_msdyn_whatsappengagementctx";
				public const string BusinessUnitMsdynWkwconfig = "business_unit_msdyn_wkwconfig";
				public const string BusinessUnitMsdynWorkhourtemplate = "business_unit_msdyn_workhourtemplate";
				public const string BusinessUnitMsdynWorkorder = "business_unit_msdyn_workorder";
				public const string BusinessUnitMsdynWorkordercharacteristic = "business_unit_msdyn_workordercharacteristic";
				public const string BusinessUnitMsdynWorkorderincident = "business_unit_msdyn_workorderincident";
				public const string BusinessUnitMsdynWorkordernte = "business_unit_msdyn_workordernte";
				public const string BusinessUnitMsdynWorkorderproduct = "business_unit_msdyn_workorderproduct";
				public const string BusinessUnitMsdynWorkorderresolution = "business_unit_msdyn_workorderresolution";
				public const string BusinessUnitMsdynWorkorderresourcerestriction = "business_unit_msdyn_workorderresourcerestriction";
				public const string BusinessUnitMsdynWorkorderservice = "business_unit_msdyn_workorderservice";
				public const string BusinessUnitMsdynWorkorderservicetask = "business_unit_msdyn_workorderservicetask";
				public const string BusinessUnitMsdynWorkordersubstatus = "business_unit_msdyn_workordersubstatus";
				public const string BusinessUnitMsdynWorkordertype = "business_unit_msdyn_workordertype";
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
				public const string BusinessUnitMsdynmktByoacschannelinstance = "business_unit_msdynmkt_byoacschannelinstance";
				public const string BusinessUnitMsdynmktByoacschannelinstanceaccount = "business_unit_msdynmkt_byoacschannelinstanceaccount";
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
				public const string BusinessUnitMsdynmktVibeschannelinstance = "business_unit_msdynmkt_vibeschannelinstance";
				public const string BusinessUnitMsdynmktVibeschannelinstanceaccount = "business_unit_msdynmkt_vibeschannelinstanceaccount";
				public const string BusinessUnitMsdyusdActioncallworkflow = "business_unit_msdyusd_actioncallworkflow";
				public const string BusinessUnitMsdyusdAgentscriptaction = "business_unit_msdyusd_agentscriptaction";
				public const string BusinessUnitMsdyusdAgentscripttaskcategory = "business_unit_msdyusd_agentscripttaskcategory";
				public const string BusinessUnitMsdyusdAnswer = "business_unit_msdyusd_answer";
				public const string BusinessUnitMsdyusdAuditanddiagnosticssetting = "business_unit_msdyusd_auditanddiagnosticssetting";
				public const string BusinessUnitMsdyusdConfiguration = "business_unit_msdyusd_configuration";
				public const string BusinessUnitMsdyusdCustomizationfiles = "business_unit_msdyusd_customizationfiles";
				public const string BusinessUnitMsdyusdEntityassignment = "business_unit_msdyusd_entityassignment";
				public const string BusinessUnitMsdyusdEntitysearch = "business_unit_msdyusd_entitysearch";
				public const string BusinessUnitMsdyusdForm = "business_unit_msdyusd_form";
				public const string BusinessUnitMsdyusdLanguagemodule = "business_unit_msdyusd_languagemodule";
				public const string BusinessUnitMsdyusdScriptlet = "business_unit_msdyusd_scriptlet";
				public const string BusinessUnitMsdyusdScripttasktrigger = "business_unit_msdyusd_scripttasktrigger";
				public const string BusinessUnitMsdyusdSearch = "business_unit_msdyusd_search";
				public const string BusinessUnitMsdyusdSessioninformation = "business_unit_msdyusd_sessioninformation";
				public const string BusinessUnitMsdyusdSessiontransfer = "business_unit_msdyusd_sessiontransfer";
				public const string BusinessUnitMsdyusdTask = "business_unit_msdyusd_task";
				public const string BusinessUnitMsdyusdToolbarbutton = "business_unit_msdyusd_toolbarbutton";
				public const string BusinessUnitMsdyusdToolbarstrip = "business_unit_msdyusd_toolbarstrip";
				public const string BusinessUnitMsdyusdTracesourcesetting = "business_unit_msdyusd_tracesourcesetting";
				public const string BusinessUnitMsdyusdUcisettings = "business_unit_msdyusd_ucisettings";
				public const string BusinessUnitMsdyusdUiievent = "business_unit_msdyusd_uiievent";
				public const string BusinessUnitMsdyusdUsersettings = "business_unit_msdyusd_usersettings";
				public const string BusinessUnitMsdyusdWindowroute = "business_unit_msdyusd_windowroute";
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
				public const string BusinessUnitMspcatCatalogsubmissionfiles = "business_unit_mspcat_catalogsubmissionfiles";
				public const string BusinessUnitMspcatPackagestore = "business_unit_mspcat_packagestore";
				public const string BusinessUnitNlsqregistration = "business_unit_nlsqregistration";
				public const string BusinessUnitOpportunities = "business_unit_opportunities";
				public const string BusinessUnitOpportunityCloseActivities = "business_unit_opportunity_close_activities";
				public const string BusinessUnitOrderCloseActivities = "business_unit_order_close_activities";
				public const string BusinessUnitOrders = "business_unit_orders";
				public const string BusinessUnitParentBusinessUnit = "business_unit_parent_business_unit";
				public const string BusinessUnitPdfsetting = "business_unit_pdfsetting";
				public const string BusinessUnitPersonaldocumenttemplates = "business_unit_personaldocumenttemplates";
				public const string BusinessUnitPhoneCallActivities = "business_unit_phone_call_activities";
				public const string BusinessUnitPlannerbusinessscenario = "business_unit_plannerbusinessscenario";
				public const string BusinessUnitPlannersyncaction = "business_unit_plannersyncaction";
				public const string BusinessUnitPostfollows = "business_unit_postfollows";
				public const string BusinessUnitPostRegarding = "business_unit_PostRegarding";
				public const string BusinessUnitPowerbidataset = "business_unit_powerbidataset";
				public const string BusinessUnitPowerbidatasetapdx = "business_unit_powerbidatasetapdx";
				public const string BusinessUnitPowerbimashupparameter = "business_unit_powerbimashupparameter";
				public const string BusinessUnitPowerbireport = "business_unit_powerbireport";
				public const string BusinessUnitPowerbireportapdx = "business_unit_powerbireportapdx";
				public const string BusinessUnitPowerfxrule = "business_unit_powerfxrule";
				public const string BusinessUnitPowerpagecomponent = "business_unit_powerpagecomponent";
				public const string BusinessUnitPowerpagesite = "business_unit_powerpagesite";
				public const string BusinessUnitPowerpagesitelanguage = "business_unit_powerpagesitelanguage";
				public const string BusinessUnitPowerpagesitepublished = "business_unit_powerpagesitepublished";
				public const string BusinessUnitPowerpageslog = "business_unit_powerpageslog";
				public const string BusinessUnitPowerpagesscanreport = "business_unit_powerpagesscanreport";
				public const string BusinessUnitPrincipalentitybusinessunitmap = "business_unit_principalentitybusinessunitmap";
				public const string BusinessUnitProcessstageparameter = "business_unit_processstageparameter";
				public const string BusinessUnitProfilerule = "business_unit_profilerule";
				public const string BusinessUnitQueues = "business_unit_queues";
				public const string BusinessUnitQueues2 = "business_unit_queues2";
				public const string BusinessUnitQuoteCloseActivities = "business_unit_quote_close_activities";
				public const string BusinessUnitQuotes = "business_unit_quotes";
				public const string BusinessUnitRatingmodel = "business_unit_ratingmodel";
				public const string BusinessUnitRatingvalue = "business_unit_ratingvalue";
				public const string BusinessUnitRecentlyused = "business_unit_recentlyused";
				public const string BusinessUnitReconciliationentityinfo = "business_unit_reconciliationentityinfo";
				public const string BusinessUnitReconciliationentitystepinfo = "business_unit_reconciliationentitystepinfo";
				public const string BusinessUnitReconciliationinfo = "business_unit_reconciliationinfo";
				public const string BusinessUnitRecurrencerule = "business_unit_recurrencerule";
				public const string BusinessUnitRecurringappointmentmasterActivities = "business_unit_recurringappointmentmaster_activities";
				public const string BusinessUnitReports = "business_unit_reports";
				public const string BusinessUnitResourceGroups = "business_unit_resource_groups";
				public const string BusinessUnitResourceSpecs = "business_unit_resource_specs";
				public const string BusinessUnitResources = "business_unit_resources";
				public const string BusinessUnitRetaineddataexcel = "business_unit_retaineddataexcel";
				public const string BusinessUnitRetentioncleanupinfo = "business_unit_retentioncleanupinfo";
				public const string BusinessUnitRetentioncleanupoperation = "business_unit_retentioncleanupoperation";
				public const string BusinessUnitRetentionconfig = "business_unit_retentionconfig";
				public const string BusinessUnitRetentionfailuredetail = "business_unit_retentionfailuredetail";
				public const string BusinessUnitRetentionoperation = "business_unit_retentionoperation";
				public const string BusinessUnitRoles = "business_unit_roles";
				public const string BusinessUnitRoutingrule = "business_unit_routingrule";
				public const string BusinessUnitSalesprocessinstance = "business_unit_salesprocessinstance";
				public const string BusinessUnitServiceAppointments = "business_unit_service_appointments";
				public const string BusinessUnitServiceContracts = "business_unit_service_contracts";
				public const string BusinessUnitSharepointdocument = "business_unit_sharepointdocument";
				public const string BusinessUnitSharepointdocument2 = "business_unit_sharepointdocument2";
				public const string BusinessUnitSharepointdocumentlocation = "business_unit_sharepointdocumentlocation";
				public const string BusinessUnitSharepointsites = "business_unit_sharepointsites";
				public const string BusinessUnitSideloadedaiplugin = "business_unit_sideloadedaiplugin";
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
				public const string BusinessUnitUiiAction = "business_unit_uii_action";
				public const string BusinessUnitUiiAudit = "business_unit_uii_audit";
				public const string BusinessUnitUiiContext = "business_unit_uii_context";
				public const string BusinessUnitUiiHostedapplication = "business_unit_uii_hostedapplication";
				public const string BusinessUnitUiiNonhostedapplication = "business_unit_uii_nonhostedapplication";
				public const string BusinessUnitUiiOption = "business_unit_uii_option";
				public const string BusinessUnitUiiSavedsession = "business_unit_uii_savedsession";
				public const string BusinessUnitUiiSessiontransfer = "business_unit_uii_sessiontransfer";
				public const string BusinessUnitUiiWorkflow = "business_unit_uii_workflow";
				public const string BusinessUnitUiiWorkflowWorkflowstepMapping = "business_unit_uii_workflow_workflowstep_mapping";
				public const string BusinessUnitUiiWorkflowstep = "business_unit_uii_workflowstep";
				public const string BusinessUnitUntrackedemailActivities = "business_unit_untrackedemail_activities";
				public const string BusinessUnitUserSettings = "business_unit_user_settings";
				public const string BusinessUnitUserapplicationmetadata = "business_unit_userapplicationmetadata";
				public const string BusinessUnitUserform = "business_unit_userform";
				public const string BusinessUnitUserquery = "business_unit_userquery";
				public const string BusinessUnitUserqueryvisualizations = "business_unit_userqueryvisualizations";
				public const string BusinessUnitWorkflow = "business_unit_workflow";
				public const string BusinessUnitWorkflowbinary = "business_unit_workflowbinary";
				public const string BusinessUnitWorkflowlogs = "business_unit_workflowlogs";
				public const string BusinessUnitWorkqueue = "business_unit_workqueue";
				public const string BusinessUnitWorkqueueitem = "business_unit_workqueueitem";
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
				public const string MsdynApprovalBusinessunitOwningbusinessunit = "msdyn_approval_businessunit_owningbusinessunit";
				public const string MsdynBookingalertBusinessunitOwningbusinessunit = "msdyn_bookingalert_businessunit_owningbusinessunit";
				public const string MsdynCopilottranscriptBusinessunitOwningbusinessunit = "msdyn_copilottranscript_businessunit_owningbusinessunit";
				public const string MsdynOcliveworkitemBusinessunitOwningbusinessunit = "msdyn_ocliveworkitem_businessunit_owningbusinessunit";
				public const string MsdynOcoutboundmessageBusinessunitOwningbusinessunit = "msdyn_ocoutboundmessage_businessunit_owningbusinessunit";
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
				public const string MsdynMsdynWarehouseBusinessunitWarehouse = "msdyn_msdyn_warehouse_businessunit_Warehouse";
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
