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
	/// Person with access to the Microsoft CRM system and who owns objects in the Microsoft CRM database.
	/// </summary>
	[DataContractAttribute()]
	[EntityLogicalNameAttribute("systemuser")]
	[System.CodeDom.Compiler.GeneratedCode("dgtp", "2023")]
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
		public SystemUser(Guid id, bool trackChanges = false) : base(EntityLogicalName,id)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public SystemUser(KeyAttributeCollection keyAttributes, bool trackChanges = false) : base(EntityLogicalName,keyAttributes)
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
        private readonly Lazy<HashSet<string>> _changedProperties = new Lazy<HashSet<string>>();
        #endregion

        #region consts
        public const string EntityLogicalName = "systemuser";
        public const string PrimaryNameAttribute = "fullname";
        public const int EntityTypeCode = 8;
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
		[AttributeLogicalNameAttribute("systemuserid")]
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
                OnPropertyChanging(nameof(SystemUserId));
                SetAttributeValue("systemuserid", value);
				if (value.HasValue)
				{
					base.Id = value.Value;
				}
				else
				{
					base.Id = System.Guid.Empty;
				}
                OnPropertyChanged(nameof(SystemUserId));
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
                OnPropertyChanging(nameof(AccessMode));
                SetAttributeValue("accessmode", value);
                OnPropertyChanged(nameof(AccessMode));
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
                OnPropertyChanging(nameof(ApplicationId));
                SetAttributeValue("applicationid", value);
                OnPropertyChanged(nameof(ApplicationId));
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
                OnPropertyChanging(nameof(AzureState));
                SetAttributeValue("azurestate", value);
                OnPropertyChanged(nameof(AzureState));
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
                OnPropertyChanging(nameof(BusinessUnitId));
                SetAttributeValue("businessunitid", value);
                OnPropertyChanged(nameof(BusinessUnitId));
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
                OnPropertyChanging(nameof(CalendarId));
                SetAttributeValue("calendarid", value);
                OnPropertyChanged(nameof(CalendarId));
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
                OnPropertyChanging(nameof(CALType));
                SetAttributeValue("caltype", value);
                OnPropertyChanged(nameof(CALType));
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
                OnPropertyChanging(nameof(DisplayInServiceViews));
                SetAttributeValue("displayinserviceviews", value);
                OnPropertyChanged(nameof(DisplayInServiceViews));
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
                OnPropertyChanging(nameof(DomainName));
                SetAttributeValue("domainname", value);
                OnPropertyChanged(nameof(DomainName));
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
                OnPropertyChanging(nameof(EmailRouterAccessApproval));
                SetAttributeValue("emailrouteraccessapproval", value);
                OnPropertyChanged(nameof(EmailRouterAccessApproval));
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
                OnPropertyChanging(nameof(EmployeeId));
                SetAttributeValue("employeeid", value);
                OnPropertyChanged(nameof(EmployeeId));
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
                OnPropertyChanging(nameof(EntityImage));
                SetAttributeValue("entityimage", value);
                OnPropertyChanged(nameof(EntityImage));
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
                OnPropertyChanging(nameof(FirstName));
                SetAttributeValue("firstname", value);
                OnPropertyChanged(nameof(FirstName));
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
                OnPropertyChanging(nameof(GovernmentId));
                SetAttributeValue("governmentid", value);
                OnPropertyChanged(nameof(GovernmentId));
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
                OnPropertyChanging(nameof(HomePhone));
                SetAttributeValue("homephone", value);
                OnPropertyChanged(nameof(HomePhone));
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
                OnPropertyChanging(nameof(ImportSequenceNumber));
                SetAttributeValue("importsequencenumber", value);
                OnPropertyChanged(nameof(ImportSequenceNumber));
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
                OnPropertyChanging(nameof(IncomingEmailDeliveryMethod));
                SetAttributeValue("incomingemaildeliverymethod", value);
                OnPropertyChanged(nameof(IncomingEmailDeliveryMethod));
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
                OnPropertyChanging(nameof(InternalEMailAddress));
                SetAttributeValue("internalemailaddress", value);
                OnPropertyChanged(nameof(InternalEMailAddress));
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
                OnPropertyChanging(nameof(InviteStatusCode));
                SetAttributeValue("invitestatuscode", value);
                OnPropertyChanged(nameof(InviteStatusCode));
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
                OnPropertyChanging(nameof(IsDisabled));
                SetAttributeValue("isdisabled", value);
                OnPropertyChanged(nameof(IsDisabled));
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
                OnPropertyChanging(nameof(IsIntegrationUser));
                SetAttributeValue("isintegrationuser", value);
                OnPropertyChanged(nameof(IsIntegrationUser));
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
                OnPropertyChanging(nameof(IsLicensed));
                SetAttributeValue("islicensed", value);
                OnPropertyChanged(nameof(IsLicensed));
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
                OnPropertyChanging(nameof(IsSyncWithDirectory));
                SetAttributeValue("issyncwithdirectory", value);
                OnPropertyChanged(nameof(IsSyncWithDirectory));
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
                OnPropertyChanging(nameof(JobTitle));
                SetAttributeValue("jobtitle", value);
                OnPropertyChanged(nameof(JobTitle));
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
                OnPropertyChanging(nameof(LastName));
                SetAttributeValue("lastname", value);
                OnPropertyChanged(nameof(LastName));
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
                OnPropertyChanging(nameof(MiddleName));
                SetAttributeValue("middlename", value);
                OnPropertyChanged(nameof(MiddleName));
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
                OnPropertyChanging(nameof(MobileAlertEMail));
                SetAttributeValue("mobilealertemail", value);
                OnPropertyChanged(nameof(MobileAlertEMail));
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
                OnPropertyChanging(nameof(MobileOfflineProfileId));
                SetAttributeValue("mobileofflineprofileid", value);
                OnPropertyChanged(nameof(MobileOfflineProfileId));
            }
        }

		
		[AttributeLogicalName("mobileofflineprofileidname")]
        public string? MobileOfflineProfileIdName
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("mobileofflineprofileidname");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(MobileOfflineProfileIdName));
                SetAttributeValue("mobileofflineprofileidname", value);
                OnPropertyChanged(nameof(MobileOfflineProfileIdName));
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
                OnPropertyChanging(nameof(MobilePhone));
                SetAttributeValue("mobilephone", value);
                OnPropertyChanged(nameof(MobilePhone));
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
		/// BOT User Description
		/// </summary>
		[AttributeLogicalName("msdyn_botdescription")]
        public string? MsdynBotDescription
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("msdyn_botdescription");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(MsdynBotDescription));
                SetAttributeValue("msdyn_botdescription", value);
                OnPropertyChanged(nameof(MsdynBotDescription));
            }
        }

		/// <summary>
		/// Bot User Endpoint
		/// </summary>
		[AttributeLogicalName("msdyn_botendpoint")]
        public string? MsdynBotEndpoint
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("msdyn_botendpoint");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(MsdynBotEndpoint));
                SetAttributeValue("msdyn_botendpoint", value);
                OnPropertyChanged(nameof(MsdynBotEndpoint));
            }
        }

		/// <summary>
		/// Bot User Secret Keys
		/// </summary>
		[AttributeLogicalName("msdyn_botsecretkeys")]
        public string? MsdynBotSecretKeys
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("msdyn_botsecretkeys");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(MsdynBotSecretKeys));
                SetAttributeValue("msdyn_botsecretkeys", value);
                OnPropertyChanged(nameof(MsdynBotSecretKeys));
            }
        }

		/// <summary>
		/// Capacity associated with the User.
		/// </summary>
		[AttributeLogicalName("msdyn_capacity")]
        public int? MsdynCapacity
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<int?>("msdyn_capacity");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(MsdynCapacity));
                SetAttributeValue("msdyn_capacity", value);
                OnPropertyChanged(nameof(MsdynCapacity));
            }
        }

		/// <summary>
		/// Unique identifier for Presence associated with User.
		/// </summary>
		[AttributeLogicalName("msdyn_defaultpresenceiduser")]
        public EntityReference? MsdynDefaultPresenceIdUser
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<EntityReference?>("msdyn_defaultpresenceiduser");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(MsdynDefaultPresenceIdUser));
                SetAttributeValue("msdyn_defaultpresenceiduser", value);
                OnPropertyChanged(nameof(MsdynDefaultPresenceIdUser));
            }
        }

		/// <summary>
		/// Describes whether user is opted out or not
		/// </summary>
		[AttributeLogicalName("msdyn_gdproptout")]
        public bool? MsdynGdproptout
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<bool?>("msdyn_gdproptout");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(MsdynGdproptout));
                SetAttributeValue("msdyn_gdproptout", value);
                OnPropertyChanged(nameof(MsdynGdproptout));
            }
        }

		/// <summary>
		/// Field to bind grid wrapper control
		/// </summary>
		[AttributeLogicalName("msdyn_gridwrappercontrolfield")]
        public string? MsdynGridwrappercontrolfield
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("msdyn_gridwrappercontrolfield");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(MsdynGridwrappercontrolfield));
                SetAttributeValue("msdyn_gridwrappercontrolfield", value);
                OnPropertyChanged(nameof(MsdynGridwrappercontrolfield));
            }
        }

		/// <summary>
		/// Check if swarm is enabled for the experts.
		/// </summary>
		[AttributeLogicalName("msdyn_isexpertenabledforswarm")]
        public bool? MsdynIsexpertenabledforswarm
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<bool?>("msdyn_isexpertenabledforswarm");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(MsdynIsexpertenabledforswarm));
                SetAttributeValue("msdyn_isexpertenabledforswarm", value);
                OnPropertyChanged(nameof(MsdynIsexpertenabledforswarm));
            }
        }

		/// <summary>
		/// Type of user - CRM or BOT user
		/// </summary>
		[AttributeLogicalName("msdyn_usertype")]
        public OptionSetValue? MsdynUserType
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<OptionSetValue?>("msdyn_usertype");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(MsdynUserType));
                SetAttributeValue("msdyn_usertype", value);
                OnPropertyChanged(nameof(MsdynUserType));
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
                OnPropertyChanging(nameof(NickName));
                SetAttributeValue("nickname", value);
                OnPropertyChanged(nameof(NickName));
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
                OnPropertyChanging(nameof(OutgoingEmailDeliveryMethod));
                SetAttributeValue("outgoingemaildeliverymethod", value);
                OnPropertyChanged(nameof(OutgoingEmailDeliveryMethod));
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
                OnPropertyChanging(nameof(ParentSystemUserId));
                SetAttributeValue("parentsystemuserid", value);
                OnPropertyChanged(nameof(ParentSystemUserId));
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
                OnPropertyChanging(nameof(PassportHi));
                SetAttributeValue("passporthi", value);
                OnPropertyChanged(nameof(PassportHi));
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
                OnPropertyChanging(nameof(PassportLo));
                SetAttributeValue("passportlo", value);
                OnPropertyChanged(nameof(PassportLo));
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
                OnPropertyChanging(nameof(PersonalEMailAddress));
                SetAttributeValue("personalemailaddress", value);
                OnPropertyChanged(nameof(PersonalEMailAddress));
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
                OnPropertyChanging(nameof(PhotoUrl));
                SetAttributeValue("photourl", value);
                OnPropertyChanged(nameof(PhotoUrl));
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
                OnPropertyChanging(nameof(PositionId));
                SetAttributeValue("positionid", value);
                OnPropertyChanged(nameof(PositionId));
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
                OnPropertyChanging(nameof(PreferredAddressCode));
                SetAttributeValue("preferredaddresscode", value);
                OnPropertyChanged(nameof(PreferredAddressCode));
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
                OnPropertyChanging(nameof(PreferredEmailCode));
                SetAttributeValue("preferredemailcode", value);
                OnPropertyChanged(nameof(PreferredEmailCode));
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
                OnPropertyChanging(nameof(PreferredPhoneCode));
                SetAttributeValue("preferredphonecode", value);
                OnPropertyChanged(nameof(PreferredPhoneCode));
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
                OnPropertyChanging(nameof(ProcessId));
                SetAttributeValue("processid", value);
                OnPropertyChanged(nameof(ProcessId));
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
                OnPropertyChanging(nameof(QueueId));
                SetAttributeValue("queueid", value);
                OnPropertyChanged(nameof(QueueId));
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
                OnPropertyChanging(nameof(Salutation));
                SetAttributeValue("salutation", value);
                OnPropertyChanged(nameof(Salutation));
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
                OnPropertyChanging(nameof(SetupUser));
                SetAttributeValue("setupuser", value);
                OnPropertyChanged(nameof(SetupUser));
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
                OnPropertyChanging(nameof(SharePointEmailAddress));
                SetAttributeValue("sharepointemailaddress", value);
                OnPropertyChanged(nameof(SharePointEmailAddress));
            }
        }

		/// <summary>
		/// Site at which the user is located.
		/// </summary>
		[AttributeLogicalName("siteid")]
        public EntityReference? SiteId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<EntityReference?>("siteid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(SiteId));
                SetAttributeValue("siteid", value);
                OnPropertyChanged(nameof(SiteId));
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
                OnPropertyChanging(nameof(Skills));
                SetAttributeValue("skills", value);
                OnPropertyChanged(nameof(Skills));
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
                OnPropertyChanging(nameof(StageId));
                SetAttributeValue("stageid", value);
                OnPropertyChanged(nameof(StageId));
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
                OnPropertyChanging(nameof(TerritoryId));
                SetAttributeValue("territoryid", value);
                OnPropertyChanged(nameof(TerritoryId));
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
                OnPropertyChanging(nameof(TimeZoneRuleVersionNumber));
                SetAttributeValue("timezoneruleversionnumber", value);
                OnPropertyChanged(nameof(TimeZoneRuleVersionNumber));
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
                OnPropertyChanging(nameof(Title));
                SetAttributeValue("title", value);
                OnPropertyChanged(nameof(Title));
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
                OnPropertyChanging(nameof(TransactionCurrencyId));
                SetAttributeValue("transactioncurrencyid", value);
                OnPropertyChanged(nameof(TransactionCurrencyId));
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
                OnPropertyChanging(nameof(TraversedPath));
                SetAttributeValue("traversedpath", value);
                OnPropertyChanged(nameof(TraversedPath));
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
                OnPropertyChanging(nameof(UserLicenseType));
                SetAttributeValue("userlicensetype", value);
                OnPropertyChanged(nameof(UserLicenseType));
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
                OnPropertyChanging(nameof(UTCConversionTimeZoneCode));
                SetAttributeValue("utcconversiontimezonecode", value);
                OnPropertyChanged(nameof(UTCConversionTimeZoneCode));
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
                OnPropertyChanging(nameof(WindowsLiveID));
                SetAttributeValue("windowsliveid", value);
                OnPropertyChanged(nameof(WindowsLiveID));
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
                OnPropertyChanging(nameof(YammerEmailAddress));
                SetAttributeValue("yammeremailaddress", value);
                OnPropertyChanged(nameof(YammerEmailAddress));
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
                OnPropertyChanging(nameof(YammerUserId));
                SetAttributeValue("yammeruserid", value);
                OnPropertyChanged(nameof(YammerUserId));
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
                OnPropertyChanging(nameof(YomiFirstName));
                SetAttributeValue("yomifirstname", value);
                OnPropertyChanged(nameof(YomiFirstName));
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
                OnPropertyChanging(nameof(YomiLastName));
                SetAttributeValue("yomilastname", value);
                OnPropertyChanged(nameof(YomiLastName));
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
                OnPropertyChanging(nameof(YomiMiddleName));
                SetAttributeValue("yomimiddlename", value);
                OnPropertyChanged(nameof(YomiMiddleName));
            }
        }


		#endregion

		#region NavigationProperties
		/// <summary>
		/// 1:N contact_owning_user
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("contact_owning_user")]
		public System.Collections.Generic.IEnumerable<Contact> ContactOwningUser
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Contact>("contact_owning_user", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("ContactOwningUser");
				this.SetRelatedEntities<Contact>("contact_owning_user", null, value);
				this.OnPropertyChanged("ContactOwningUser");
			}
		}

		/// <summary>
		/// 1:N createdby_pluginassembly
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("createdby_pluginassembly")]
		public System.Collections.Generic.IEnumerable<PluginAssembly> CreatedbyPluginassembly
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<PluginAssembly>("createdby_pluginassembly", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("CreatedbyPluginassembly");
				this.SetRelatedEntities<PluginAssembly>("createdby_pluginassembly", null, value);
				this.OnPropertyChanged("CreatedbyPluginassembly");
			}
		}

		/// <summary>
		/// 1:N createdby_plugintype
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("createdby_plugintype")]
		public System.Collections.Generic.IEnumerable<PluginType> CreatedbyPlugintype
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<PluginType>("createdby_plugintype", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("CreatedbyPlugintype");
				this.SetRelatedEntities<PluginType>("createdby_plugintype", null, value);
				this.OnPropertyChanged("CreatedbyPlugintype");
			}
		}

		/// <summary>
		/// 1:N createdby_sdkmessage
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("createdby_sdkmessage")]
		public System.Collections.Generic.IEnumerable<SdkMessage> CreatedbySdkmessage
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<SdkMessage>("createdby_sdkmessage", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("CreatedbySdkmessage");
				this.SetRelatedEntities<SdkMessage>("createdby_sdkmessage", null, value);
				this.OnPropertyChanged("CreatedbySdkmessage");
			}
		}

		/// <summary>
		/// 1:N createdby_sdkmessagefilter
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("createdby_sdkmessagefilter")]
		public System.Collections.Generic.IEnumerable<SdkMessageFilter> CreatedbySdkmessagefilter
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<SdkMessageFilter>("createdby_sdkmessagefilter", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("CreatedbySdkmessagefilter");
				this.SetRelatedEntities<SdkMessageFilter>("createdby_sdkmessagefilter", null, value);
				this.OnPropertyChanged("CreatedbySdkmessagefilter");
			}
		}

		/// <summary>
		/// 1:N createdby_sdkmessageprocessingstep
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("createdby_sdkmessageprocessingstep")]
		public System.Collections.Generic.IEnumerable<SdkMessageProcessingStep> CreatedbySdkmessageprocessingstep
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<SdkMessageProcessingStep>("createdby_sdkmessageprocessingstep", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("CreatedbySdkmessageprocessingstep");
				this.SetRelatedEntities<SdkMessageProcessingStep>("createdby_sdkmessageprocessingstep", null, value);
				this.OnPropertyChanged("CreatedbySdkmessageprocessingstep");
			}
		}

		/// <summary>
		/// 1:N createdby_sdkmessageprocessingstepimage
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("createdby_sdkmessageprocessingstepimage")]
		public System.Collections.Generic.IEnumerable<SdkMessageProcessingStepImage> CreatedbySdkmessageprocessingstepimage
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<SdkMessageProcessingStepImage>("createdby_sdkmessageprocessingstepimage", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("CreatedbySdkmessageprocessingstepimage");
				this.SetRelatedEntities<SdkMessageProcessingStepImage>("createdby_sdkmessageprocessingstepimage", null, value);
				this.OnPropertyChanged("CreatedbySdkmessageprocessingstepimage");
			}
		}

		/// <summary>
		/// 1:N createdby_sdkmessageprocessingstepsecureconfig
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("createdby_sdkmessageprocessingstepsecureconfig")]
		public System.Collections.Generic.IEnumerable<SdkMessageProcessingStepSecureConfig> CreatedbySdkmessageprocessingstepsecureconfig
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<SdkMessageProcessingStepSecureConfig>("createdby_sdkmessageprocessingstepsecureconfig", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("CreatedbySdkmessageprocessingstepsecureconfig");
				this.SetRelatedEntities<SdkMessageProcessingStepSecureConfig>("createdby_sdkmessageprocessingstepsecureconfig", null, value);
				this.OnPropertyChanged("CreatedbySdkmessageprocessingstepsecureconfig");
			}
		}

		/// <summary>
		/// 1:N impersonatinguserid_sdkmessageprocessingstep
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("impersonatinguserid_sdkmessageprocessingstep")]
		public System.Collections.Generic.IEnumerable<SdkMessageProcessingStep> ImpersonatinguseridSdkmessageprocessingstep
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<SdkMessageProcessingStep>("impersonatinguserid_sdkmessageprocessingstep", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("ImpersonatinguseridSdkmessageprocessingstep");
				this.SetRelatedEntities<SdkMessageProcessingStep>("impersonatinguserid_sdkmessageprocessingstep", null, value);
				this.OnPropertyChanged("ImpersonatinguseridSdkmessageprocessingstep");
			}
		}

		/// <summary>
		/// 1:N lk_accountbase_createdby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_accountbase_createdby")]
		public System.Collections.Generic.IEnumerable<Account> LkAccountbaseCreatedby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Account>("lk_accountbase_createdby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkAccountbaseCreatedby");
				this.SetRelatedEntities<Account>("lk_accountbase_createdby", null, value);
				this.OnPropertyChanged("LkAccountbaseCreatedby");
			}
		}

		/// <summary>
		/// 1:N lk_accountbase_createdonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_accountbase_createdonbehalfby")]
		public System.Collections.Generic.IEnumerable<Account> LkAccountbaseCreatedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Account>("lk_accountbase_createdonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkAccountbaseCreatedonbehalfby");
				this.SetRelatedEntities<Account>("lk_accountbase_createdonbehalfby", null, value);
				this.OnPropertyChanged("LkAccountbaseCreatedonbehalfby");
			}
		}

		/// <summary>
		/// 1:N lk_accountbase_modifiedby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_accountbase_modifiedby")]
		public System.Collections.Generic.IEnumerable<Account> LkAccountbaseModifiedby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Account>("lk_accountbase_modifiedby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkAccountbaseModifiedby");
				this.SetRelatedEntities<Account>("lk_accountbase_modifiedby", null, value);
				this.OnPropertyChanged("LkAccountbaseModifiedby");
			}
		}

		/// <summary>
		/// 1:N lk_accountbase_modifiedonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_accountbase_modifiedonbehalfby")]
		public System.Collections.Generic.IEnumerable<Account> LkAccountbaseModifiedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Account>("lk_accountbase_modifiedonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkAccountbaseModifiedonbehalfby");
				this.SetRelatedEntities<Account>("lk_accountbase_modifiedonbehalfby", null, value);
				this.OnPropertyChanged("LkAccountbaseModifiedonbehalfby");
			}
		}

		/// <summary>
		/// 1:N lk_asyncoperation_createdby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_asyncoperation_createdby")]
		public System.Collections.Generic.IEnumerable<AsyncOperation> LkAsyncoperationCreatedby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<AsyncOperation>("lk_asyncoperation_createdby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkAsyncoperationCreatedby");
				this.SetRelatedEntities<AsyncOperation>("lk_asyncoperation_createdby", null, value);
				this.OnPropertyChanged("LkAsyncoperationCreatedby");
			}
		}

		/// <summary>
		/// 1:N lk_asyncoperation_createdonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_asyncoperation_createdonbehalfby")]
		public System.Collections.Generic.IEnumerable<AsyncOperation> LkAsyncoperationCreatedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<AsyncOperation>("lk_asyncoperation_createdonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkAsyncoperationCreatedonbehalfby");
				this.SetRelatedEntities<AsyncOperation>("lk_asyncoperation_createdonbehalfby", null, value);
				this.OnPropertyChanged("LkAsyncoperationCreatedonbehalfby");
			}
		}

		/// <summary>
		/// 1:N lk_asyncoperation_modifiedby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_asyncoperation_modifiedby")]
		public System.Collections.Generic.IEnumerable<AsyncOperation> LkAsyncoperationModifiedby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<AsyncOperation>("lk_asyncoperation_modifiedby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkAsyncoperationModifiedby");
				this.SetRelatedEntities<AsyncOperation>("lk_asyncoperation_modifiedby", null, value);
				this.OnPropertyChanged("LkAsyncoperationModifiedby");
			}
		}

		/// <summary>
		/// 1:N lk_asyncoperation_modifiedonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_asyncoperation_modifiedonbehalfby")]
		public System.Collections.Generic.IEnumerable<AsyncOperation> LkAsyncoperationModifiedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<AsyncOperation>("lk_asyncoperation_modifiedonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkAsyncoperationModifiedonbehalfby");
				this.SetRelatedEntities<AsyncOperation>("lk_asyncoperation_modifiedonbehalfby", null, value);
				this.OnPropertyChanged("LkAsyncoperationModifiedonbehalfby");
			}
		}

		/// <summary>
		/// 1:N lk_businessunit_createdonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_businessunit_createdonbehalfby")]
		public System.Collections.Generic.IEnumerable<BusinessUnit> LkBusinessunitCreatedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<BusinessUnit>("lk_businessunit_createdonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkBusinessunitCreatedonbehalfby");
				this.SetRelatedEntities<BusinessUnit>("lk_businessunit_createdonbehalfby", null, value);
				this.OnPropertyChanged("LkBusinessunitCreatedonbehalfby");
			}
		}

		/// <summary>
		/// 1:N lk_businessunit_modifiedonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_businessunit_modifiedonbehalfby")]
		public System.Collections.Generic.IEnumerable<BusinessUnit> LkBusinessunitModifiedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<BusinessUnit>("lk_businessunit_modifiedonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkBusinessunitModifiedonbehalfby");
				this.SetRelatedEntities<BusinessUnit>("lk_businessunit_modifiedonbehalfby", null, value);
				this.OnPropertyChanged("LkBusinessunitModifiedonbehalfby");
			}
		}

		/// <summary>
		/// 1:N lk_businessunitbase_createdby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_businessunitbase_createdby")]
		public System.Collections.Generic.IEnumerable<BusinessUnit> LkBusinessunitbaseCreatedby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<BusinessUnit>("lk_businessunitbase_createdby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkBusinessunitbaseCreatedby");
				this.SetRelatedEntities<BusinessUnit>("lk_businessunitbase_createdby", null, value);
				this.OnPropertyChanged("LkBusinessunitbaseCreatedby");
			}
		}

		/// <summary>
		/// 1:N lk_businessunitbase_modifiedby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_businessunitbase_modifiedby")]
		public System.Collections.Generic.IEnumerable<BusinessUnit> LkBusinessunitbaseModifiedby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<BusinessUnit>("lk_businessunitbase_modifiedby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkBusinessunitbaseModifiedby");
				this.SetRelatedEntities<BusinessUnit>("lk_businessunitbase_modifiedby", null, value);
				this.OnPropertyChanged("LkBusinessunitbaseModifiedby");
			}
		}

		/// <summary>
		/// 1:N lk_calendar_createdby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_calendar_createdby")]
		public System.Collections.Generic.IEnumerable<Calendar> LkCalendarCreatedby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Calendar>("lk_calendar_createdby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkCalendarCreatedby");
				this.SetRelatedEntities<Calendar>("lk_calendar_createdby", null, value);
				this.OnPropertyChanged("LkCalendarCreatedby");
			}
		}

		/// <summary>
		/// 1:N lk_calendar_createdonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_calendar_createdonbehalfby")]
		public System.Collections.Generic.IEnumerable<Calendar> LkCalendarCreatedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Calendar>("lk_calendar_createdonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkCalendarCreatedonbehalfby");
				this.SetRelatedEntities<Calendar>("lk_calendar_createdonbehalfby", null, value);
				this.OnPropertyChanged("LkCalendarCreatedonbehalfby");
			}
		}

		/// <summary>
		/// 1:N lk_calendar_modifiedby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_calendar_modifiedby")]
		public System.Collections.Generic.IEnumerable<Calendar> LkCalendarModifiedby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Calendar>("lk_calendar_modifiedby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkCalendarModifiedby");
				this.SetRelatedEntities<Calendar>("lk_calendar_modifiedby", null, value);
				this.OnPropertyChanged("LkCalendarModifiedby");
			}
		}

		/// <summary>
		/// 1:N lk_calendar_modifiedonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_calendar_modifiedonbehalfby")]
		public System.Collections.Generic.IEnumerable<Calendar> LkCalendarModifiedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Calendar>("lk_calendar_modifiedonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkCalendarModifiedonbehalfby");
				this.SetRelatedEntities<Calendar>("lk_calendar_modifiedonbehalfby", null, value);
				this.OnPropertyChanged("LkCalendarModifiedonbehalfby");
			}
		}

		/// <summary>
		/// 1:N lk_calendarrule_createdby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_calendarrule_createdby")]
		public System.Collections.Generic.IEnumerable<CalendarRule> LkCalendarruleCreatedby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<CalendarRule>("lk_calendarrule_createdby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkCalendarruleCreatedby");
				this.SetRelatedEntities<CalendarRule>("lk_calendarrule_createdby", null, value);
				this.OnPropertyChanged("LkCalendarruleCreatedby");
			}
		}

		/// <summary>
		/// 1:N lk_calendarrule_createdonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_calendarrule_createdonbehalfby")]
		public System.Collections.Generic.IEnumerable<CalendarRule> LkCalendarruleCreatedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<CalendarRule>("lk_calendarrule_createdonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkCalendarruleCreatedonbehalfby");
				this.SetRelatedEntities<CalendarRule>("lk_calendarrule_createdonbehalfby", null, value);
				this.OnPropertyChanged("LkCalendarruleCreatedonbehalfby");
			}
		}

		/// <summary>
		/// 1:N lk_calendarrule_modifiedby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_calendarrule_modifiedby")]
		public System.Collections.Generic.IEnumerable<CalendarRule> LkCalendarruleModifiedby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<CalendarRule>("lk_calendarrule_modifiedby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkCalendarruleModifiedby");
				this.SetRelatedEntities<CalendarRule>("lk_calendarrule_modifiedby", null, value);
				this.OnPropertyChanged("LkCalendarruleModifiedby");
			}
		}

		/// <summary>
		/// 1:N lk_calendarrule_modifiedonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_calendarrule_modifiedonbehalfby")]
		public System.Collections.Generic.IEnumerable<CalendarRule> LkCalendarruleModifiedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<CalendarRule>("lk_calendarrule_modifiedonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkCalendarruleModifiedonbehalfby");
				this.SetRelatedEntities<CalendarRule>("lk_calendarrule_modifiedonbehalfby", null, value);
				this.OnPropertyChanged("LkCalendarruleModifiedonbehalfby");
			}
		}

		/// <summary>
		/// 1:N lk_contact_createdonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_contact_createdonbehalfby")]
		public System.Collections.Generic.IEnumerable<Contact> LkContactCreatedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Contact>("lk_contact_createdonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkContactCreatedonbehalfby");
				this.SetRelatedEntities<Contact>("lk_contact_createdonbehalfby", null, value);
				this.OnPropertyChanged("LkContactCreatedonbehalfby");
			}
		}

		/// <summary>
		/// 1:N lk_contact_modifiedonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_contact_modifiedonbehalfby")]
		public System.Collections.Generic.IEnumerable<Contact> LkContactModifiedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Contact>("lk_contact_modifiedonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkContactModifiedonbehalfby");
				this.SetRelatedEntities<Contact>("lk_contact_modifiedonbehalfby", null, value);
				this.OnPropertyChanged("LkContactModifiedonbehalfby");
			}
		}

		/// <summary>
		/// 1:N lk_contactbase_createdby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_contactbase_createdby")]
		public System.Collections.Generic.IEnumerable<Contact> LkContactbaseCreatedby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Contact>("lk_contactbase_createdby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkContactbaseCreatedby");
				this.SetRelatedEntities<Contact>("lk_contactbase_createdby", null, value);
				this.OnPropertyChanged("LkContactbaseCreatedby");
			}
		}

		/// <summary>
		/// 1:N lk_contactbase_modifiedby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_contactbase_modifiedby")]
		public System.Collections.Generic.IEnumerable<Contact> LkContactbaseModifiedby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Contact>("lk_contactbase_modifiedby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkContactbaseModifiedby");
				this.SetRelatedEntities<Contact>("lk_contactbase_modifiedby", null, value);
				this.OnPropertyChanged("LkContactbaseModifiedby");
			}
		}

		/// <summary>
		/// 1:N lk_customapi_createdby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_customapi_createdby")]
		public System.Collections.Generic.IEnumerable<CustomAPI> LkCustomapiCreatedby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<CustomAPI>("lk_customapi_createdby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkCustomapiCreatedby");
				this.SetRelatedEntities<CustomAPI>("lk_customapi_createdby", null, value);
				this.OnPropertyChanged("LkCustomapiCreatedby");
			}
		}

		/// <summary>
		/// 1:N lk_customapi_createdonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_customapi_createdonbehalfby")]
		public System.Collections.Generic.IEnumerable<CustomAPI> LkCustomapiCreatedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<CustomAPI>("lk_customapi_createdonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkCustomapiCreatedonbehalfby");
				this.SetRelatedEntities<CustomAPI>("lk_customapi_createdonbehalfby", null, value);
				this.OnPropertyChanged("LkCustomapiCreatedonbehalfby");
			}
		}

		/// <summary>
		/// 1:N lk_customapi_modifiedby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_customapi_modifiedby")]
		public System.Collections.Generic.IEnumerable<CustomAPI> LkCustomapiModifiedby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<CustomAPI>("lk_customapi_modifiedby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkCustomapiModifiedby");
				this.SetRelatedEntities<CustomAPI>("lk_customapi_modifiedby", null, value);
				this.OnPropertyChanged("LkCustomapiModifiedby");
			}
		}

		/// <summary>
		/// 1:N lk_customapi_modifiedonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_customapi_modifiedonbehalfby")]
		public System.Collections.Generic.IEnumerable<CustomAPI> LkCustomapiModifiedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<CustomAPI>("lk_customapi_modifiedonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkCustomapiModifiedonbehalfby");
				this.SetRelatedEntities<CustomAPI>("lk_customapi_modifiedonbehalfby", null, value);
				this.OnPropertyChanged("LkCustomapiModifiedonbehalfby");
			}
		}

		/// <summary>
		/// 1:N lk_customapirequestparameter_createdby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_customapirequestparameter_createdby")]
		public System.Collections.Generic.IEnumerable<CustomAPIRequestParameter> LkCustomapirequestparameterCreatedby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<CustomAPIRequestParameter>("lk_customapirequestparameter_createdby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkCustomapirequestparameterCreatedby");
				this.SetRelatedEntities<CustomAPIRequestParameter>("lk_customapirequestparameter_createdby", null, value);
				this.OnPropertyChanged("LkCustomapirequestparameterCreatedby");
			}
		}

		/// <summary>
		/// 1:N lk_customapirequestparameter_createdonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_customapirequestparameter_createdonbehalfby")]
		public System.Collections.Generic.IEnumerable<CustomAPIRequestParameter> LkCustomapirequestparameterCreatedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<CustomAPIRequestParameter>("lk_customapirequestparameter_createdonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkCustomapirequestparameterCreatedonbehalfby");
				this.SetRelatedEntities<CustomAPIRequestParameter>("lk_customapirequestparameter_createdonbehalfby", null, value);
				this.OnPropertyChanged("LkCustomapirequestparameterCreatedonbehalfby");
			}
		}

		/// <summary>
		/// 1:N lk_customapirequestparameter_modifiedby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_customapirequestparameter_modifiedby")]
		public System.Collections.Generic.IEnumerable<CustomAPIRequestParameter> LkCustomapirequestparameterModifiedby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<CustomAPIRequestParameter>("lk_customapirequestparameter_modifiedby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkCustomapirequestparameterModifiedby");
				this.SetRelatedEntities<CustomAPIRequestParameter>("lk_customapirequestparameter_modifiedby", null, value);
				this.OnPropertyChanged("LkCustomapirequestparameterModifiedby");
			}
		}

		/// <summary>
		/// 1:N lk_customapirequestparameter_modifiedonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_customapirequestparameter_modifiedonbehalfby")]
		public System.Collections.Generic.IEnumerable<CustomAPIRequestParameter> LkCustomapirequestparameterModifiedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<CustomAPIRequestParameter>("lk_customapirequestparameter_modifiedonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkCustomapirequestparameterModifiedonbehalfby");
				this.SetRelatedEntities<CustomAPIRequestParameter>("lk_customapirequestparameter_modifiedonbehalfby", null, value);
				this.OnPropertyChanged("LkCustomapirequestparameterModifiedonbehalfby");
			}
		}

		/// <summary>
		/// 1:N lk_customapiresponseproperty_createdby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_customapiresponseproperty_createdby")]
		public System.Collections.Generic.IEnumerable<CustomAPIResponseProperty> LkCustomapiresponsepropertyCreatedby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<CustomAPIResponseProperty>("lk_customapiresponseproperty_createdby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkCustomapiresponsepropertyCreatedby");
				this.SetRelatedEntities<CustomAPIResponseProperty>("lk_customapiresponseproperty_createdby", null, value);
				this.OnPropertyChanged("LkCustomapiresponsepropertyCreatedby");
			}
		}

		/// <summary>
		/// 1:N lk_customapiresponseproperty_createdonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_customapiresponseproperty_createdonbehalfby")]
		public System.Collections.Generic.IEnumerable<CustomAPIResponseProperty> LkCustomapiresponsepropertyCreatedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<CustomAPIResponseProperty>("lk_customapiresponseproperty_createdonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkCustomapiresponsepropertyCreatedonbehalfby");
				this.SetRelatedEntities<CustomAPIResponseProperty>("lk_customapiresponseproperty_createdonbehalfby", null, value);
				this.OnPropertyChanged("LkCustomapiresponsepropertyCreatedonbehalfby");
			}
		}

		/// <summary>
		/// 1:N lk_customapiresponseproperty_modifiedby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_customapiresponseproperty_modifiedby")]
		public System.Collections.Generic.IEnumerable<CustomAPIResponseProperty> LkCustomapiresponsepropertyModifiedby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<CustomAPIResponseProperty>("lk_customapiresponseproperty_modifiedby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkCustomapiresponsepropertyModifiedby");
				this.SetRelatedEntities<CustomAPIResponseProperty>("lk_customapiresponseproperty_modifiedby", null, value);
				this.OnPropertyChanged("LkCustomapiresponsepropertyModifiedby");
			}
		}

		/// <summary>
		/// 1:N lk_customapiresponseproperty_modifiedonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_customapiresponseproperty_modifiedonbehalfby")]
		public System.Collections.Generic.IEnumerable<CustomAPIResponseProperty> LkCustomapiresponsepropertyModifiedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<CustomAPIResponseProperty>("lk_customapiresponseproperty_modifiedonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkCustomapiresponsepropertyModifiedonbehalfby");
				this.SetRelatedEntities<CustomAPIResponseProperty>("lk_customapiresponseproperty_modifiedonbehalfby", null, value);
				this.OnPropertyChanged("LkCustomapiresponsepropertyModifiedonbehalfby");
			}
		}

		/// <summary>
		/// 1:N lk_documenttemplatebase_createdby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_documenttemplatebase_createdby")]
		public System.Collections.Generic.IEnumerable<DocumentTemplate> LkDocumenttemplatebaseCreatedby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<DocumentTemplate>("lk_documenttemplatebase_createdby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkDocumenttemplatebaseCreatedby");
				this.SetRelatedEntities<DocumentTemplate>("lk_documenttemplatebase_createdby", null, value);
				this.OnPropertyChanged("LkDocumenttemplatebaseCreatedby");
			}
		}

		/// <summary>
		/// 1:N lk_documenttemplatebase_createdonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_documenttemplatebase_createdonbehalfby")]
		public System.Collections.Generic.IEnumerable<DocumentTemplate> LkDocumenttemplatebaseCreatedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<DocumentTemplate>("lk_documenttemplatebase_createdonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkDocumenttemplatebaseCreatedonbehalfby");
				this.SetRelatedEntities<DocumentTemplate>("lk_documenttemplatebase_createdonbehalfby", null, value);
				this.OnPropertyChanged("LkDocumenttemplatebaseCreatedonbehalfby");
			}
		}

		/// <summary>
		/// 1:N lk_documenttemplatebase_modifiedby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_documenttemplatebase_modifiedby")]
		public System.Collections.Generic.IEnumerable<DocumentTemplate> LkDocumenttemplatebaseModifiedby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<DocumentTemplate>("lk_documenttemplatebase_modifiedby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkDocumenttemplatebaseModifiedby");
				this.SetRelatedEntities<DocumentTemplate>("lk_documenttemplatebase_modifiedby", null, value);
				this.OnPropertyChanged("LkDocumenttemplatebaseModifiedby");
			}
		}

		/// <summary>
		/// 1:N lk_documenttemplatebase_modifiedonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_documenttemplatebase_modifiedonbehalfby")]
		public System.Collections.Generic.IEnumerable<DocumentTemplate> LkDocumenttemplatebaseModifiedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<DocumentTemplate>("lk_documenttemplatebase_modifiedonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkDocumenttemplatebaseModifiedonbehalfby");
				this.SetRelatedEntities<DocumentTemplate>("lk_documenttemplatebase_modifiedonbehalfby", null, value);
				this.OnPropertyChanged("LkDocumenttemplatebaseModifiedonbehalfby");
			}
		}

		/// <summary>
		/// 1:N lk_duplicaterule_createdonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_duplicaterule_createdonbehalfby")]
		public System.Collections.Generic.IEnumerable<DuplicateRule> LkDuplicateruleCreatedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<DuplicateRule>("lk_duplicaterule_createdonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkDuplicateruleCreatedonbehalfby");
				this.SetRelatedEntities<DuplicateRule>("lk_duplicaterule_createdonbehalfby", null, value);
				this.OnPropertyChanged("LkDuplicateruleCreatedonbehalfby");
			}
		}

		/// <summary>
		/// 1:N lk_duplicaterule_modifiedonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_duplicaterule_modifiedonbehalfby")]
		public System.Collections.Generic.IEnumerable<DuplicateRule> LkDuplicateruleModifiedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<DuplicateRule>("lk_duplicaterule_modifiedonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkDuplicateruleModifiedonbehalfby");
				this.SetRelatedEntities<DuplicateRule>("lk_duplicaterule_modifiedonbehalfby", null, value);
				this.OnPropertyChanged("LkDuplicateruleModifiedonbehalfby");
			}
		}

		/// <summary>
		/// 1:N lk_duplicaterulebase_createdby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_duplicaterulebase_createdby")]
		public System.Collections.Generic.IEnumerable<DuplicateRule> LkDuplicaterulebaseCreatedby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<DuplicateRule>("lk_duplicaterulebase_createdby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkDuplicaterulebaseCreatedby");
				this.SetRelatedEntities<DuplicateRule>("lk_duplicaterulebase_createdby", null, value);
				this.OnPropertyChanged("LkDuplicaterulebaseCreatedby");
			}
		}

		/// <summary>
		/// 1:N lk_duplicaterulebase_modifiedby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_duplicaterulebase_modifiedby")]
		public System.Collections.Generic.IEnumerable<DuplicateRule> LkDuplicaterulebaseModifiedby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<DuplicateRule>("lk_duplicaterulebase_modifiedby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkDuplicaterulebaseModifiedby");
				this.SetRelatedEntities<DuplicateRule>("lk_duplicaterulebase_modifiedby", null, value);
				this.OnPropertyChanged("LkDuplicaterulebaseModifiedby");
			}
		}

		/// <summary>
		/// 1:N lk_duplicaterulecondition_createdonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_duplicaterulecondition_createdonbehalfby")]
		public System.Collections.Generic.IEnumerable<DuplicateRuleCondition> LkDuplicateruleconditionCreatedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<DuplicateRuleCondition>("lk_duplicaterulecondition_createdonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkDuplicateruleconditionCreatedonbehalfby");
				this.SetRelatedEntities<DuplicateRuleCondition>("lk_duplicaterulecondition_createdonbehalfby", null, value);
				this.OnPropertyChanged("LkDuplicateruleconditionCreatedonbehalfby");
			}
		}

		/// <summary>
		/// 1:N lk_duplicaterulecondition_modifiedonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_duplicaterulecondition_modifiedonbehalfby")]
		public System.Collections.Generic.IEnumerable<DuplicateRuleCondition> LkDuplicateruleconditionModifiedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<DuplicateRuleCondition>("lk_duplicaterulecondition_modifiedonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkDuplicateruleconditionModifiedonbehalfby");
				this.SetRelatedEntities<DuplicateRuleCondition>("lk_duplicaterulecondition_modifiedonbehalfby", null, value);
				this.OnPropertyChanged("LkDuplicateruleconditionModifiedonbehalfby");
			}
		}

		/// <summary>
		/// 1:N lk_duplicateruleconditionbase_createdby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_duplicateruleconditionbase_createdby")]
		public System.Collections.Generic.IEnumerable<DuplicateRuleCondition> LkDuplicateruleconditionbaseCreatedby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<DuplicateRuleCondition>("lk_duplicateruleconditionbase_createdby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkDuplicateruleconditionbaseCreatedby");
				this.SetRelatedEntities<DuplicateRuleCondition>("lk_duplicateruleconditionbase_createdby", null, value);
				this.OnPropertyChanged("LkDuplicateruleconditionbaseCreatedby");
			}
		}

		/// <summary>
		/// 1:N lk_duplicateruleconditionbase_modifiedby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_duplicateruleconditionbase_modifiedby")]
		public System.Collections.Generic.IEnumerable<DuplicateRuleCondition> LkDuplicateruleconditionbaseModifiedby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<DuplicateRuleCondition>("lk_duplicateruleconditionbase_modifiedby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkDuplicateruleconditionbaseModifiedby");
				this.SetRelatedEntities<DuplicateRuleCondition>("lk_duplicateruleconditionbase_modifiedby", null, value);
				this.OnPropertyChanged("LkDuplicateruleconditionbaseModifiedby");
			}
		}

		/// <summary>
		/// 1:N lk_importjobbase_createdby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_importjobbase_createdby")]
		public System.Collections.Generic.IEnumerable<ImportJob> LkImportjobbaseCreatedby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<ImportJob>("lk_importjobbase_createdby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkImportjobbaseCreatedby");
				this.SetRelatedEntities<ImportJob>("lk_importjobbase_createdby", null, value);
				this.OnPropertyChanged("LkImportjobbaseCreatedby");
			}
		}

		/// <summary>
		/// 1:N lk_importjobbase_createdonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_importjobbase_createdonbehalfby")]
		public System.Collections.Generic.IEnumerable<ImportJob> LkImportjobbaseCreatedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<ImportJob>("lk_importjobbase_createdonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkImportjobbaseCreatedonbehalfby");
				this.SetRelatedEntities<ImportJob>("lk_importjobbase_createdonbehalfby", null, value);
				this.OnPropertyChanged("LkImportjobbaseCreatedonbehalfby");
			}
		}

		/// <summary>
		/// 1:N lk_importjobbase_modifiedby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_importjobbase_modifiedby")]
		public System.Collections.Generic.IEnumerable<ImportJob> LkImportjobbaseModifiedby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<ImportJob>("lk_importjobbase_modifiedby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkImportjobbaseModifiedby");
				this.SetRelatedEntities<ImportJob>("lk_importjobbase_modifiedby", null, value);
				this.OnPropertyChanged("LkImportjobbaseModifiedby");
			}
		}

		/// <summary>
		/// 1:N lk_importjobbase_modifiedonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_importjobbase_modifiedonbehalfby")]
		public System.Collections.Generic.IEnumerable<ImportJob> LkImportjobbaseModifiedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<ImportJob>("lk_importjobbase_modifiedonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkImportjobbaseModifiedonbehalfby");
				this.SetRelatedEntities<ImportJob>("lk_importjobbase_modifiedonbehalfby", null, value);
				this.OnPropertyChanged("LkImportjobbaseModifiedonbehalfby");
			}
		}

		/// <summary>
		/// 1:N lk_organization_createdonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_organization_createdonbehalfby")]
		public System.Collections.Generic.IEnumerable<Organization> LkOrganizationCreatedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Organization>("lk_organization_createdonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkOrganizationCreatedonbehalfby");
				this.SetRelatedEntities<Organization>("lk_organization_createdonbehalfby", null, value);
				this.OnPropertyChanged("LkOrganizationCreatedonbehalfby");
			}
		}

		/// <summary>
		/// 1:N lk_organization_modifiedonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_organization_modifiedonbehalfby")]
		public System.Collections.Generic.IEnumerable<Organization> LkOrganizationModifiedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Organization>("lk_organization_modifiedonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkOrganizationModifiedonbehalfby");
				this.SetRelatedEntities<Organization>("lk_organization_modifiedonbehalfby", null, value);
				this.OnPropertyChanged("LkOrganizationModifiedonbehalfby");
			}
		}

		/// <summary>
		/// 1:N lk_organizationbase_createdby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_organizationbase_createdby")]
		public System.Collections.Generic.IEnumerable<Organization> LkOrganizationbaseCreatedby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Organization>("lk_organizationbase_createdby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkOrganizationbaseCreatedby");
				this.SetRelatedEntities<Organization>("lk_organizationbase_createdby", null, value);
				this.OnPropertyChanged("LkOrganizationbaseCreatedby");
			}
		}

		/// <summary>
		/// 1:N lk_organizationbase_modifiedby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_organizationbase_modifiedby")]
		public System.Collections.Generic.IEnumerable<Organization> LkOrganizationbaseModifiedby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Organization>("lk_organizationbase_modifiedby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkOrganizationbaseModifiedby");
				this.SetRelatedEntities<Organization>("lk_organizationbase_modifiedby", null, value);
				this.OnPropertyChanged("LkOrganizationbaseModifiedby");
			}
		}

		/// <summary>
		/// 1:N lk_pluginassembly_createdonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_pluginassembly_createdonbehalfby")]
		public System.Collections.Generic.IEnumerable<PluginAssembly> LkPluginassemblyCreatedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<PluginAssembly>("lk_pluginassembly_createdonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkPluginassemblyCreatedonbehalfby");
				this.SetRelatedEntities<PluginAssembly>("lk_pluginassembly_createdonbehalfby", null, value);
				this.OnPropertyChanged("LkPluginassemblyCreatedonbehalfby");
			}
		}

		/// <summary>
		/// 1:N lk_pluginassembly_modifiedonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_pluginassembly_modifiedonbehalfby")]
		public System.Collections.Generic.IEnumerable<PluginAssembly> LkPluginassemblyModifiedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<PluginAssembly>("lk_pluginassembly_modifiedonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkPluginassemblyModifiedonbehalfby");
				this.SetRelatedEntities<PluginAssembly>("lk_pluginassembly_modifiedonbehalfby", null, value);
				this.OnPropertyChanged("LkPluginassemblyModifiedonbehalfby");
			}
		}

		/// <summary>
		/// 1:N lk_plugintype_createdonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_plugintype_createdonbehalfby")]
		public System.Collections.Generic.IEnumerable<PluginType> LkPlugintypeCreatedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<PluginType>("lk_plugintype_createdonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkPlugintypeCreatedonbehalfby");
				this.SetRelatedEntities<PluginType>("lk_plugintype_createdonbehalfby", null, value);
				this.OnPropertyChanged("LkPlugintypeCreatedonbehalfby");
			}
		}

		/// <summary>
		/// 1:N lk_plugintype_modifiedonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_plugintype_modifiedonbehalfby")]
		public System.Collections.Generic.IEnumerable<PluginType> LkPlugintypeModifiedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<PluginType>("lk_plugintype_modifiedonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkPlugintypeModifiedonbehalfby");
				this.SetRelatedEntities<PluginType>("lk_plugintype_modifiedonbehalfby", null, value);
				this.OnPropertyChanged("LkPlugintypeModifiedonbehalfby");
			}
		}

		/// <summary>
		/// 1:N lk_publisher_createdby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_publisher_createdby")]
		public System.Collections.Generic.IEnumerable<Publisher> LkPublisherCreatedby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Publisher>("lk_publisher_createdby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkPublisherCreatedby");
				this.SetRelatedEntities<Publisher>("lk_publisher_createdby", null, value);
				this.OnPropertyChanged("LkPublisherCreatedby");
			}
		}

		/// <summary>
		/// 1:N lk_publisher_modifiedby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_publisher_modifiedby")]
		public System.Collections.Generic.IEnumerable<Publisher> LkPublisherModifiedby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Publisher>("lk_publisher_modifiedby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkPublisherModifiedby");
				this.SetRelatedEntities<Publisher>("lk_publisher_modifiedby", null, value);
				this.OnPropertyChanged("LkPublisherModifiedby");
			}
		}

		/// <summary>
		/// 1:N lk_publisherbase_createdonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_publisherbase_createdonbehalfby")]
		public System.Collections.Generic.IEnumerable<Publisher> LkPublisherbaseCreatedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Publisher>("lk_publisherbase_createdonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkPublisherbaseCreatedonbehalfby");
				this.SetRelatedEntities<Publisher>("lk_publisherbase_createdonbehalfby", null, value);
				this.OnPropertyChanged("LkPublisherbaseCreatedonbehalfby");
			}
		}

		/// <summary>
		/// 1:N lk_publisherbase_modifiedonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_publisherbase_modifiedonbehalfby")]
		public System.Collections.Generic.IEnumerable<Publisher> LkPublisherbaseModifiedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Publisher>("lk_publisherbase_modifiedonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkPublisherbaseModifiedonbehalfby");
				this.SetRelatedEntities<Publisher>("lk_publisherbase_modifiedonbehalfby", null, value);
				this.OnPropertyChanged("LkPublisherbaseModifiedonbehalfby");
			}
		}

		/// <summary>
		/// 1:N lk_queue_createdonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_queue_createdonbehalfby")]
		public System.Collections.Generic.IEnumerable<Queue> LkQueueCreatedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Queue>("lk_queue_createdonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkQueueCreatedonbehalfby");
				this.SetRelatedEntities<Queue>("lk_queue_createdonbehalfby", null, value);
				this.OnPropertyChanged("LkQueueCreatedonbehalfby");
			}
		}

		/// <summary>
		/// 1:N lk_queue_modifiedonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_queue_modifiedonbehalfby")]
		public System.Collections.Generic.IEnumerable<Queue> LkQueueModifiedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Queue>("lk_queue_modifiedonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkQueueModifiedonbehalfby");
				this.SetRelatedEntities<Queue>("lk_queue_modifiedonbehalfby", null, value);
				this.OnPropertyChanged("LkQueueModifiedonbehalfby");
			}
		}

		/// <summary>
		/// 1:N lk_queuebase_createdby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_queuebase_createdby")]
		public System.Collections.Generic.IEnumerable<Queue> LkQueuebaseCreatedby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Queue>("lk_queuebase_createdby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkQueuebaseCreatedby");
				this.SetRelatedEntities<Queue>("lk_queuebase_createdby", null, value);
				this.OnPropertyChanged("LkQueuebaseCreatedby");
			}
		}

		/// <summary>
		/// 1:N lk_queuebase_modifiedby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_queuebase_modifiedby")]
		public System.Collections.Generic.IEnumerable<Queue> LkQueuebaseModifiedby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Queue>("lk_queuebase_modifiedby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkQueuebaseModifiedby");
				this.SetRelatedEntities<Queue>("lk_queuebase_modifiedby", null, value);
				this.OnPropertyChanged("LkQueuebaseModifiedby");
			}
		}

		/// <summary>
		/// 1:N lk_role_createdonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_role_createdonbehalfby")]
		public System.Collections.Generic.IEnumerable<Role> LkRoleCreatedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Role>("lk_role_createdonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkRoleCreatedonbehalfby");
				this.SetRelatedEntities<Role>("lk_role_createdonbehalfby", null, value);
				this.OnPropertyChanged("LkRoleCreatedonbehalfby");
			}
		}

		/// <summary>
		/// 1:N lk_role_modifiedonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_role_modifiedonbehalfby")]
		public System.Collections.Generic.IEnumerable<Role> LkRoleModifiedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Role>("lk_role_modifiedonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkRoleModifiedonbehalfby");
				this.SetRelatedEntities<Role>("lk_role_modifiedonbehalfby", null, value);
				this.OnPropertyChanged("LkRoleModifiedonbehalfby");
			}
		}

		/// <summary>
		/// 1:N lk_rolebase_createdby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_rolebase_createdby")]
		public System.Collections.Generic.IEnumerable<Role> LkRolebaseCreatedby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Role>("lk_rolebase_createdby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkRolebaseCreatedby");
				this.SetRelatedEntities<Role>("lk_rolebase_createdby", null, value);
				this.OnPropertyChanged("LkRolebaseCreatedby");
			}
		}

		/// <summary>
		/// 1:N lk_rolebase_modifiedby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_rolebase_modifiedby")]
		public System.Collections.Generic.IEnumerable<Role> LkRolebaseModifiedby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Role>("lk_rolebase_modifiedby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkRolebaseModifiedby");
				this.SetRelatedEntities<Role>("lk_rolebase_modifiedby", null, value);
				this.OnPropertyChanged("LkRolebaseModifiedby");
			}
		}

		/// <summary>
		/// 1:N lk_routingrule_createdby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_routingrule_createdby")]
		public System.Collections.Generic.IEnumerable<RoutingRule> LkRoutingruleCreatedby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<RoutingRule>("lk_routingrule_createdby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkRoutingruleCreatedby");
				this.SetRelatedEntities<RoutingRule>("lk_routingrule_createdby", null, value);
				this.OnPropertyChanged("LkRoutingruleCreatedby");
			}
		}

		/// <summary>
		/// 1:N lk_routingrule_createdonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_routingrule_createdonbehalfby")]
		public System.Collections.Generic.IEnumerable<RoutingRule> LkRoutingruleCreatedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<RoutingRule>("lk_routingrule_createdonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkRoutingruleCreatedonbehalfby");
				this.SetRelatedEntities<RoutingRule>("lk_routingrule_createdonbehalfby", null, value);
				this.OnPropertyChanged("LkRoutingruleCreatedonbehalfby");
			}
		}

		/// <summary>
		/// 1:N lk_routingrule_modifiedby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_routingrule_modifiedby")]
		public System.Collections.Generic.IEnumerable<RoutingRule> LkRoutingruleModifiedby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<RoutingRule>("lk_routingrule_modifiedby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkRoutingruleModifiedby");
				this.SetRelatedEntities<RoutingRule>("lk_routingrule_modifiedby", null, value);
				this.OnPropertyChanged("LkRoutingruleModifiedby");
			}
		}

		/// <summary>
		/// 1:N lk_routingrule_modifiedonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_routingrule_modifiedonbehalfby")]
		public System.Collections.Generic.IEnumerable<RoutingRule> LkRoutingruleModifiedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<RoutingRule>("lk_routingrule_modifiedonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkRoutingruleModifiedonbehalfby");
				this.SetRelatedEntities<RoutingRule>("lk_routingrule_modifiedonbehalfby", null, value);
				this.OnPropertyChanged("LkRoutingruleModifiedonbehalfby");
			}
		}

		/// <summary>
		/// 1:N lk_RoutingRuleItem_createdby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_RoutingRuleItem_createdby")]
		public System.Collections.Generic.IEnumerable<RoutingRuleItem> LkRoutingRuleItemCreatedby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<RoutingRuleItem>("lk_RoutingRuleItem_createdby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkRoutingRuleItemCreatedby");
				this.SetRelatedEntities<RoutingRuleItem>("lk_RoutingRuleItem_createdby", null, value);
				this.OnPropertyChanged("LkRoutingRuleItemCreatedby");
			}
		}

		/// <summary>
		/// 1:N lk_routingruleitem_createdonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_routingruleitem_createdonbehalfby")]
		public System.Collections.Generic.IEnumerable<RoutingRuleItem> LkRoutingruleitemCreatedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<RoutingRuleItem>("lk_routingruleitem_createdonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkRoutingruleitemCreatedonbehalfby");
				this.SetRelatedEntities<RoutingRuleItem>("lk_routingruleitem_createdonbehalfby", null, value);
				this.OnPropertyChanged("LkRoutingruleitemCreatedonbehalfby");
			}
		}

		/// <summary>
		/// 1:N lk_routingruleitem_modifiedby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_routingruleitem_modifiedby")]
		public System.Collections.Generic.IEnumerable<RoutingRuleItem> LkRoutingruleitemModifiedby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<RoutingRuleItem>("lk_routingruleitem_modifiedby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkRoutingruleitemModifiedby");
				this.SetRelatedEntities<RoutingRuleItem>("lk_routingruleitem_modifiedby", null, value);
				this.OnPropertyChanged("LkRoutingruleitemModifiedby");
			}
		}

		/// <summary>
		/// 1:N lk_routingruleitem_modifiedonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_routingruleitem_modifiedonbehalfby")]
		public System.Collections.Generic.IEnumerable<RoutingRuleItem> LkRoutingruleitemModifiedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<RoutingRuleItem>("lk_routingruleitem_modifiedonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkRoutingruleitemModifiedonbehalfby");
				this.SetRelatedEntities<RoutingRuleItem>("lk_routingruleitem_modifiedonbehalfby", null, value);
				this.OnPropertyChanged("LkRoutingruleitemModifiedonbehalfby");
			}
		}

		/// <summary>
		/// 1:N lk_savedquery_createdonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_savedquery_createdonbehalfby")]
		public System.Collections.Generic.IEnumerable<SavedQuery> LkSavedqueryCreatedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<SavedQuery>("lk_savedquery_createdonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkSavedqueryCreatedonbehalfby");
				this.SetRelatedEntities<SavedQuery>("lk_savedquery_createdonbehalfby", null, value);
				this.OnPropertyChanged("LkSavedqueryCreatedonbehalfby");
			}
		}

		/// <summary>
		/// 1:N lk_savedquery_modifiedonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_savedquery_modifiedonbehalfby")]
		public System.Collections.Generic.IEnumerable<SavedQuery> LkSavedqueryModifiedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<SavedQuery>("lk_savedquery_modifiedonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkSavedqueryModifiedonbehalfby");
				this.SetRelatedEntities<SavedQuery>("lk_savedquery_modifiedonbehalfby", null, value);
				this.OnPropertyChanged("LkSavedqueryModifiedonbehalfby");
			}
		}

		/// <summary>
		/// 1:N lk_savedquerybase_createdby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_savedquerybase_createdby")]
		public System.Collections.Generic.IEnumerable<SavedQuery> LkSavedquerybaseCreatedby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<SavedQuery>("lk_savedquerybase_createdby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkSavedquerybaseCreatedby");
				this.SetRelatedEntities<SavedQuery>("lk_savedquerybase_createdby", null, value);
				this.OnPropertyChanged("LkSavedquerybaseCreatedby");
			}
		}

		/// <summary>
		/// 1:N lk_savedquerybase_modifiedby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_savedquerybase_modifiedby")]
		public System.Collections.Generic.IEnumerable<SavedQuery> LkSavedquerybaseModifiedby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<SavedQuery>("lk_savedquerybase_modifiedby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkSavedquerybaseModifiedby");
				this.SetRelatedEntities<SavedQuery>("lk_savedquerybase_modifiedby", null, value);
				this.OnPropertyChanged("LkSavedquerybaseModifiedby");
			}
		}

		/// <summary>
		/// 1:N lk_sdkmessage_createdonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_sdkmessage_createdonbehalfby")]
		public System.Collections.Generic.IEnumerable<SdkMessage> LkSdkmessageCreatedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<SdkMessage>("lk_sdkmessage_createdonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkSdkmessageCreatedonbehalfby");
				this.SetRelatedEntities<SdkMessage>("lk_sdkmessage_createdonbehalfby", null, value);
				this.OnPropertyChanged("LkSdkmessageCreatedonbehalfby");
			}
		}

		/// <summary>
		/// 1:N lk_sdkmessage_modifiedonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_sdkmessage_modifiedonbehalfby")]
		public System.Collections.Generic.IEnumerable<SdkMessage> LkSdkmessageModifiedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<SdkMessage>("lk_sdkmessage_modifiedonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkSdkmessageModifiedonbehalfby");
				this.SetRelatedEntities<SdkMessage>("lk_sdkmessage_modifiedonbehalfby", null, value);
				this.OnPropertyChanged("LkSdkmessageModifiedonbehalfby");
			}
		}

		/// <summary>
		/// 1:N lk_sdkmessagefilter_createdonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_sdkmessagefilter_createdonbehalfby")]
		public System.Collections.Generic.IEnumerable<SdkMessageFilter> LkSdkmessagefilterCreatedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<SdkMessageFilter>("lk_sdkmessagefilter_createdonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkSdkmessagefilterCreatedonbehalfby");
				this.SetRelatedEntities<SdkMessageFilter>("lk_sdkmessagefilter_createdonbehalfby", null, value);
				this.OnPropertyChanged("LkSdkmessagefilterCreatedonbehalfby");
			}
		}

		/// <summary>
		/// 1:N lk_sdkmessagefilter_modifiedonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_sdkmessagefilter_modifiedonbehalfby")]
		public System.Collections.Generic.IEnumerable<SdkMessageFilter> LkSdkmessagefilterModifiedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<SdkMessageFilter>("lk_sdkmessagefilter_modifiedonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkSdkmessagefilterModifiedonbehalfby");
				this.SetRelatedEntities<SdkMessageFilter>("lk_sdkmessagefilter_modifiedonbehalfby", null, value);
				this.OnPropertyChanged("LkSdkmessagefilterModifiedonbehalfby");
			}
		}

		/// <summary>
		/// 1:N lk_sdkmessageprocessingstep_createdonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_sdkmessageprocessingstep_createdonbehalfby")]
		public System.Collections.Generic.IEnumerable<SdkMessageProcessingStep> LkSdkmessageprocessingstepCreatedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<SdkMessageProcessingStep>("lk_sdkmessageprocessingstep_createdonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkSdkmessageprocessingstepCreatedonbehalfby");
				this.SetRelatedEntities<SdkMessageProcessingStep>("lk_sdkmessageprocessingstep_createdonbehalfby", null, value);
				this.OnPropertyChanged("LkSdkmessageprocessingstepCreatedonbehalfby");
			}
		}

		/// <summary>
		/// 1:N lk_sdkmessageprocessingstep_modifiedonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_sdkmessageprocessingstep_modifiedonbehalfby")]
		public System.Collections.Generic.IEnumerable<SdkMessageProcessingStep> LkSdkmessageprocessingstepModifiedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<SdkMessageProcessingStep>("lk_sdkmessageprocessingstep_modifiedonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkSdkmessageprocessingstepModifiedonbehalfby");
				this.SetRelatedEntities<SdkMessageProcessingStep>("lk_sdkmessageprocessingstep_modifiedonbehalfby", null, value);
				this.OnPropertyChanged("LkSdkmessageprocessingstepModifiedonbehalfby");
			}
		}

		/// <summary>
		/// 1:N lk_sdkmessageprocessingstepimage_createdonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_sdkmessageprocessingstepimage_createdonbehalfby")]
		public System.Collections.Generic.IEnumerable<SdkMessageProcessingStepImage> LkSdkmessageprocessingstepimageCreatedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<SdkMessageProcessingStepImage>("lk_sdkmessageprocessingstepimage_createdonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkSdkmessageprocessingstepimageCreatedonbehalfby");
				this.SetRelatedEntities<SdkMessageProcessingStepImage>("lk_sdkmessageprocessingstepimage_createdonbehalfby", null, value);
				this.OnPropertyChanged("LkSdkmessageprocessingstepimageCreatedonbehalfby");
			}
		}

		/// <summary>
		/// 1:N lk_sdkmessageprocessingstepimage_modifiedonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_sdkmessageprocessingstepimage_modifiedonbehalfby")]
		public System.Collections.Generic.IEnumerable<SdkMessageProcessingStepImage> LkSdkmessageprocessingstepimageModifiedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<SdkMessageProcessingStepImage>("lk_sdkmessageprocessingstepimage_modifiedonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkSdkmessageprocessingstepimageModifiedonbehalfby");
				this.SetRelatedEntities<SdkMessageProcessingStepImage>("lk_sdkmessageprocessingstepimage_modifiedonbehalfby", null, value);
				this.OnPropertyChanged("LkSdkmessageprocessingstepimageModifiedonbehalfby");
			}
		}

		/// <summary>
		/// 1:N lk_sdkmessageprocessingstepsecureconfig_createdonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_sdkmessageprocessingstepsecureconfig_createdonbehalfby")]
		public System.Collections.Generic.IEnumerable<SdkMessageProcessingStepSecureConfig> LkSdkmessageprocessingstepsecureconfigCreatedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<SdkMessageProcessingStepSecureConfig>("lk_sdkmessageprocessingstepsecureconfig_createdonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkSdkmessageprocessingstepsecureconfigCreatedonbehalfby");
				this.SetRelatedEntities<SdkMessageProcessingStepSecureConfig>("lk_sdkmessageprocessingstepsecureconfig_createdonbehalfby", null, value);
				this.OnPropertyChanged("LkSdkmessageprocessingstepsecureconfigCreatedonbehalfby");
			}
		}

		/// <summary>
		/// 1:N lk_sdkmessageprocessingstepsecureconfig_modifiedonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_sdkmessageprocessingstepsecureconfig_modifiedonbehalfby")]
		public System.Collections.Generic.IEnumerable<SdkMessageProcessingStepSecureConfig> LkSdkmessageprocessingstepsecureconfigModifiedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<SdkMessageProcessingStepSecureConfig>("lk_sdkmessageprocessingstepsecureconfig_modifiedonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkSdkmessageprocessingstepsecureconfigModifiedonbehalfby");
				this.SetRelatedEntities<SdkMessageProcessingStepSecureConfig>("lk_sdkmessageprocessingstepsecureconfig_modifiedonbehalfby", null, value);
				this.OnPropertyChanged("LkSdkmessageprocessingstepsecureconfigModifiedonbehalfby");
			}
		}

		/// <summary>
		/// 1:N lk_slabase_createdby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_slabase_createdby")]
		public System.Collections.Generic.IEnumerable<SLA> LkSlabaseCreatedby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<SLA>("lk_slabase_createdby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkSlabaseCreatedby");
				this.SetRelatedEntities<SLA>("lk_slabase_createdby", null, value);
				this.OnPropertyChanged("LkSlabaseCreatedby");
			}
		}

		/// <summary>
		/// 1:N lk_slabase_createdonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_slabase_createdonbehalfby")]
		public System.Collections.Generic.IEnumerable<SLA> LkSlabaseCreatedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<SLA>("lk_slabase_createdonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkSlabaseCreatedonbehalfby");
				this.SetRelatedEntities<SLA>("lk_slabase_createdonbehalfby", null, value);
				this.OnPropertyChanged("LkSlabaseCreatedonbehalfby");
			}
		}

		/// <summary>
		/// 1:N lk_slabase_modifiedby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_slabase_modifiedby")]
		public System.Collections.Generic.IEnumerable<SLA> LkSlabaseModifiedby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<SLA>("lk_slabase_modifiedby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkSlabaseModifiedby");
				this.SetRelatedEntities<SLA>("lk_slabase_modifiedby", null, value);
				this.OnPropertyChanged("LkSlabaseModifiedby");
			}
		}

		/// <summary>
		/// 1:N lk_slabase_modifiedonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_slabase_modifiedonbehalfby")]
		public System.Collections.Generic.IEnumerable<SLA> LkSlabaseModifiedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<SLA>("lk_slabase_modifiedonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkSlabaseModifiedonbehalfby");
				this.SetRelatedEntities<SLA>("lk_slabase_modifiedonbehalfby", null, value);
				this.OnPropertyChanged("LkSlabaseModifiedonbehalfby");
			}
		}

		/// <summary>
		/// 1:N lk_solution_createdby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_solution_createdby")]
		public System.Collections.Generic.IEnumerable<Solution> LkSolutionCreatedby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Solution>("lk_solution_createdby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkSolutionCreatedby");
				this.SetRelatedEntities<Solution>("lk_solution_createdby", null, value);
				this.OnPropertyChanged("LkSolutionCreatedby");
			}
		}

		/// <summary>
		/// 1:N lk_solution_modifiedby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_solution_modifiedby")]
		public System.Collections.Generic.IEnumerable<Solution> LkSolutionModifiedby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Solution>("lk_solution_modifiedby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkSolutionModifiedby");
				this.SetRelatedEntities<Solution>("lk_solution_modifiedby", null, value);
				this.OnPropertyChanged("LkSolutionModifiedby");
			}
		}

		/// <summary>
		/// 1:N lk_solutionbase_createdonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_solutionbase_createdonbehalfby")]
		public System.Collections.Generic.IEnumerable<Solution> LkSolutionbaseCreatedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Solution>("lk_solutionbase_createdonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkSolutionbaseCreatedonbehalfby");
				this.SetRelatedEntities<Solution>("lk_solutionbase_createdonbehalfby", null, value);
				this.OnPropertyChanged("LkSolutionbaseCreatedonbehalfby");
			}
		}

		/// <summary>
		/// 1:N lk_solutionbase_modifiedonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_solutionbase_modifiedonbehalfby")]
		public System.Collections.Generic.IEnumerable<Solution> LkSolutionbaseModifiedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Solution>("lk_solutionbase_modifiedonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkSolutionbaseModifiedonbehalfby");
				this.SetRelatedEntities<Solution>("lk_solutionbase_modifiedonbehalfby", null, value);
				this.OnPropertyChanged("LkSolutionbaseModifiedonbehalfby");
			}
		}

		/// <summary>
		/// 1:N lk_solutioncomponentbase_createdonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_solutioncomponentbase_createdonbehalfby")]
		public System.Collections.Generic.IEnumerable<SolutionComponent> LkSolutioncomponentbaseCreatedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<SolutionComponent>("lk_solutioncomponentbase_createdonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkSolutioncomponentbaseCreatedonbehalfby");
				this.SetRelatedEntities<SolutionComponent>("lk_solutioncomponentbase_createdonbehalfby", null, value);
				this.OnPropertyChanged("LkSolutioncomponentbaseCreatedonbehalfby");
			}
		}

		/// <summary>
		/// 1:N lk_solutioncomponentbase_modifiedonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_solutioncomponentbase_modifiedonbehalfby")]
		public System.Collections.Generic.IEnumerable<SolutionComponent> LkSolutioncomponentbaseModifiedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<SolutionComponent>("lk_solutioncomponentbase_modifiedonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkSolutioncomponentbaseModifiedonbehalfby");
				this.SetRelatedEntities<SolutionComponent>("lk_solutioncomponentbase_modifiedonbehalfby", null, value);
				this.OnPropertyChanged("LkSolutioncomponentbaseModifiedonbehalfby");
			}
		}

		/// <summary>
		/// 1:N lk_systemuser_createdonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_systemuser_createdonbehalfby")]
		public System.Collections.Generic.IEnumerable<SystemUser> LkSystemuserCreatedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<SystemUser>("lk_systemuser_createdonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkSystemuserCreatedonbehalfby");
				this.SetRelatedEntities<SystemUser>("lk_systemuser_createdonbehalfby", null, value);
				this.OnPropertyChanged("LkSystemuserCreatedonbehalfby");
			}
		}

		/// <summary>
		/// 1:N lk_systemuser_modifiedonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_systemuser_modifiedonbehalfby")]
		public System.Collections.Generic.IEnumerable<SystemUser> LkSystemuserModifiedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<SystemUser>("lk_systemuser_modifiedonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkSystemuserModifiedonbehalfby");
				this.SetRelatedEntities<SystemUser>("lk_systemuser_modifiedonbehalfby", null, value);
				this.OnPropertyChanged("LkSystemuserModifiedonbehalfby");
			}
		}

		/// <summary>
		/// 1:N lk_systemuserbase_createdby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_systemuserbase_createdby")]
		public System.Collections.Generic.IEnumerable<SystemUser> LkSystemuserbaseCreatedby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<SystemUser>("lk_systemuserbase_createdby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkSystemuserbaseCreatedby");
				this.SetRelatedEntities<SystemUser>("lk_systemuserbase_createdby", null, value);
				this.OnPropertyChanged("LkSystemuserbaseCreatedby");
			}
		}

		/// <summary>
		/// 1:N lk_systemuserbase_modifiedby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_systemuserbase_modifiedby")]
		public System.Collections.Generic.IEnumerable<SystemUser> LkSystemuserbaseModifiedby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<SystemUser>("lk_systemuserbase_modifiedby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkSystemuserbaseModifiedby");
				this.SetRelatedEntities<SystemUser>("lk_systemuserbase_modifiedby", null, value);
				this.OnPropertyChanged("LkSystemuserbaseModifiedby");
			}
		}

		/// <summary>
		/// 1:N lk_team_createdonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_team_createdonbehalfby")]
		public System.Collections.Generic.IEnumerable<Team> LkTeamCreatedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Team>("lk_team_createdonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkTeamCreatedonbehalfby");
				this.SetRelatedEntities<Team>("lk_team_createdonbehalfby", null, value);
				this.OnPropertyChanged("LkTeamCreatedonbehalfby");
			}
		}

		/// <summary>
		/// 1:N lk_team_modifiedonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_team_modifiedonbehalfby")]
		public System.Collections.Generic.IEnumerable<Team> LkTeamModifiedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Team>("lk_team_modifiedonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkTeamModifiedonbehalfby");
				this.SetRelatedEntities<Team>("lk_team_modifiedonbehalfby", null, value);
				this.OnPropertyChanged("LkTeamModifiedonbehalfby");
			}
		}

		/// <summary>
		/// 1:N lk_teambase_administratorid
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_teambase_administratorid")]
		public System.Collections.Generic.IEnumerable<Team> LkTeambaseAdministratorid
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Team>("lk_teambase_administratorid", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkTeambaseAdministratorid");
				this.SetRelatedEntities<Team>("lk_teambase_administratorid", null, value);
				this.OnPropertyChanged("LkTeambaseAdministratorid");
			}
		}

		/// <summary>
		/// 1:N lk_teambase_createdby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_teambase_createdby")]
		public System.Collections.Generic.IEnumerable<Team> LkTeambaseCreatedby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Team>("lk_teambase_createdby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkTeambaseCreatedby");
				this.SetRelatedEntities<Team>("lk_teambase_createdby", null, value);
				this.OnPropertyChanged("LkTeambaseCreatedby");
			}
		}

		/// <summary>
		/// 1:N lk_teambase_modifiedby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_teambase_modifiedby")]
		public System.Collections.Generic.IEnumerable<Team> LkTeambaseModifiedby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Team>("lk_teambase_modifiedby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkTeambaseModifiedby");
				this.SetRelatedEntities<Team>("lk_teambase_modifiedby", null, value);
				this.OnPropertyChanged("LkTeambaseModifiedby");
			}
		}

		/// <summary>
		/// 1:N lk_teamtemplate_createdby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_teamtemplate_createdby")]
		public System.Collections.Generic.IEnumerable<TeamTemplate> LkTeamtemplateCreatedby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<TeamTemplate>("lk_teamtemplate_createdby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkTeamtemplateCreatedby");
				this.SetRelatedEntities<TeamTemplate>("lk_teamtemplate_createdby", null, value);
				this.OnPropertyChanged("LkTeamtemplateCreatedby");
			}
		}

		/// <summary>
		/// 1:N lk_teamtemplate_createdonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_teamtemplate_createdonbehalfby")]
		public System.Collections.Generic.IEnumerable<TeamTemplate> LkTeamtemplateCreatedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<TeamTemplate>("lk_teamtemplate_createdonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkTeamtemplateCreatedonbehalfby");
				this.SetRelatedEntities<TeamTemplate>("lk_teamtemplate_createdonbehalfby", null, value);
				this.OnPropertyChanged("LkTeamtemplateCreatedonbehalfby");
			}
		}

		/// <summary>
		/// 1:N lk_teamtemplate_modifiedby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_teamtemplate_modifiedby")]
		public System.Collections.Generic.IEnumerable<TeamTemplate> LkTeamtemplateModifiedby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<TeamTemplate>("lk_teamtemplate_modifiedby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkTeamtemplateModifiedby");
				this.SetRelatedEntities<TeamTemplate>("lk_teamtemplate_modifiedby", null, value);
				this.OnPropertyChanged("LkTeamtemplateModifiedby");
			}
		}

		/// <summary>
		/// 1:N lk_teamtemplate_modifiedonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_teamtemplate_modifiedonbehalfby")]
		public System.Collections.Generic.IEnumerable<TeamTemplate> LkTeamtemplateModifiedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<TeamTemplate>("lk_teamtemplate_modifiedonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkTeamtemplateModifiedonbehalfby");
				this.SetRelatedEntities<TeamTemplate>("lk_teamtemplate_modifiedonbehalfby", null, value);
				this.OnPropertyChanged("LkTeamtemplateModifiedonbehalfby");
			}
		}

		/// <summary>
		/// 1:N modifiedby_pluginassembly
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("modifiedby_pluginassembly")]
		public System.Collections.Generic.IEnumerable<PluginAssembly> ModifiedbyPluginassembly
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<PluginAssembly>("modifiedby_pluginassembly", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("ModifiedbyPluginassembly");
				this.SetRelatedEntities<PluginAssembly>("modifiedby_pluginassembly", null, value);
				this.OnPropertyChanged("ModifiedbyPluginassembly");
			}
		}

		/// <summary>
		/// 1:N modifiedby_plugintype
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("modifiedby_plugintype")]
		public System.Collections.Generic.IEnumerable<PluginType> ModifiedbyPlugintype
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<PluginType>("modifiedby_plugintype", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("ModifiedbyPlugintype");
				this.SetRelatedEntities<PluginType>("modifiedby_plugintype", null, value);
				this.OnPropertyChanged("ModifiedbyPlugintype");
			}
		}

		/// <summary>
		/// 1:N modifiedby_sdkmessage
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("modifiedby_sdkmessage")]
		public System.Collections.Generic.IEnumerable<SdkMessage> ModifiedbySdkmessage
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<SdkMessage>("modifiedby_sdkmessage", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("ModifiedbySdkmessage");
				this.SetRelatedEntities<SdkMessage>("modifiedby_sdkmessage", null, value);
				this.OnPropertyChanged("ModifiedbySdkmessage");
			}
		}

		/// <summary>
		/// 1:N modifiedby_sdkmessagefilter
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("modifiedby_sdkmessagefilter")]
		public System.Collections.Generic.IEnumerable<SdkMessageFilter> ModifiedbySdkmessagefilter
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<SdkMessageFilter>("modifiedby_sdkmessagefilter", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("ModifiedbySdkmessagefilter");
				this.SetRelatedEntities<SdkMessageFilter>("modifiedby_sdkmessagefilter", null, value);
				this.OnPropertyChanged("ModifiedbySdkmessagefilter");
			}
		}

		/// <summary>
		/// 1:N modifiedby_sdkmessageprocessingstep
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("modifiedby_sdkmessageprocessingstep")]
		public System.Collections.Generic.IEnumerable<SdkMessageProcessingStep> ModifiedbySdkmessageprocessingstep
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<SdkMessageProcessingStep>("modifiedby_sdkmessageprocessingstep", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("ModifiedbySdkmessageprocessingstep");
				this.SetRelatedEntities<SdkMessageProcessingStep>("modifiedby_sdkmessageprocessingstep", null, value);
				this.OnPropertyChanged("ModifiedbySdkmessageprocessingstep");
			}
		}

		/// <summary>
		/// 1:N modifiedby_sdkmessageprocessingstepimage
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("modifiedby_sdkmessageprocessingstepimage")]
		public System.Collections.Generic.IEnumerable<SdkMessageProcessingStepImage> ModifiedbySdkmessageprocessingstepimage
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<SdkMessageProcessingStepImage>("modifiedby_sdkmessageprocessingstepimage", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("ModifiedbySdkmessageprocessingstepimage");
				this.SetRelatedEntities<SdkMessageProcessingStepImage>("modifiedby_sdkmessageprocessingstepimage", null, value);
				this.OnPropertyChanged("ModifiedbySdkmessageprocessingstepimage");
			}
		}

		/// <summary>
		/// 1:N modifiedby_sdkmessageprocessingstepsecureconfig
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("modifiedby_sdkmessageprocessingstepsecureconfig")]
		public System.Collections.Generic.IEnumerable<SdkMessageProcessingStepSecureConfig> ModifiedbySdkmessageprocessingstepsecureconfig
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<SdkMessageProcessingStepSecureConfig>("modifiedby_sdkmessageprocessingstepsecureconfig", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("ModifiedbySdkmessageprocessingstepsecureconfig");
				this.SetRelatedEntities<SdkMessageProcessingStepSecureConfig>("modifiedby_sdkmessageprocessingstepsecureconfig", null, value);
				this.OnPropertyChanged("ModifiedbySdkmessageprocessingstepsecureconfig");
			}
		}

		/// <summary>
		/// 1:N queue_primary_user
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("queue_primary_user")]
		public System.Collections.Generic.IEnumerable<Queue> QueuePrimaryUser
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Queue>("queue_primary_user", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("QueuePrimaryUser");
				this.SetRelatedEntities<Queue>("queue_primary_user", null, value);
				this.OnPropertyChanged("QueuePrimaryUser");
			}
		}

		/// <summary>
		/// 1:N system_user_accounts
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("system_user_accounts")]
		public System.Collections.Generic.IEnumerable<Account> SystemUserAccounts
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Account>("system_user_accounts", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("SystemUserAccounts");
				this.SetRelatedEntities<Account>("system_user_accounts", null, value);
				this.OnPropertyChanged("SystemUserAccounts");
			}
		}

		/// <summary>
		/// 1:N system_user_asyncoperation
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("system_user_asyncoperation")]
		public System.Collections.Generic.IEnumerable<AsyncOperation> SystemUserAsyncoperation
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<AsyncOperation>("system_user_asyncoperation", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("SystemUserAsyncoperation");
				this.SetRelatedEntities<AsyncOperation>("system_user_asyncoperation", null, value);
				this.OnPropertyChanged("SystemUserAsyncoperation");
			}
		}

		/// <summary>
		/// 1:N system_user_contacts
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("system_user_contacts")]
		public System.Collections.Generic.IEnumerable<Contact> SystemUserContacts
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Contact>("system_user_contacts", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("SystemUserContacts");
				this.SetRelatedEntities<Contact>("system_user_contacts", null, value);
				this.OnPropertyChanged("SystemUserContacts");
			}
		}

		/// <summary>
		/// 1:N system_user_workflow
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("system_user_workflow")]
		public System.Collections.Generic.IEnumerable<Workflow> SystemUserWorkflow
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Workflow>("system_user_workflow", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("SystemUserWorkflow");
				this.SetRelatedEntities<Workflow>("system_user_workflow", null, value);
				this.OnPropertyChanged("SystemUserWorkflow");
			}
		}

		/// <summary>
		/// 1:N SystemUser_AsyncOperations
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("SystemUser_AsyncOperations")]
		public System.Collections.Generic.IEnumerable<AsyncOperation> SystemUserAsyncOperations
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<AsyncOperation>("SystemUser_AsyncOperations", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("SystemUserAsyncOperations");
				this.SetRelatedEntities<AsyncOperation>("SystemUser_AsyncOperations", null, value);
				this.OnPropertyChanged("SystemUserAsyncOperations");
			}
		}

		/// <summary>
		/// 1:N SystemUser_DuplicateRules
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("SystemUser_DuplicateRules")]
		public System.Collections.Generic.IEnumerable<DuplicateRule> SystemUserDuplicateRules
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<DuplicateRule>("SystemUser_DuplicateRules", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("SystemUserDuplicateRules");
				this.SetRelatedEntities<DuplicateRule>("SystemUser_DuplicateRules", null, value);
				this.OnPropertyChanged("SystemUserDuplicateRules");
			}
		}

		/// <summary>
		/// 1:N user_accounts
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("user_accounts")]
		public System.Collections.Generic.IEnumerable<Account> UserAccounts
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Account>("user_accounts", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("UserAccounts");
				this.SetRelatedEntities<Account>("user_accounts", null, value);
				this.OnPropertyChanged("UserAccounts");
			}
		}

		/// <summary>
		/// 1:N user_customapi
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("user_customapi")]
		public System.Collections.Generic.IEnumerable<CustomAPI> UserCustomapi
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<CustomAPI>("user_customapi", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("UserCustomapi");
				this.SetRelatedEntities<CustomAPI>("user_customapi", null, value);
				this.OnPropertyChanged("UserCustomapi");
			}
		}

		/// <summary>
		/// 1:N user_customapirequestparameter
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("user_customapirequestparameter")]
		public System.Collections.Generic.IEnumerable<CustomAPIRequestParameter> UserCustomapirequestparameter
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<CustomAPIRequestParameter>("user_customapirequestparameter", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("UserCustomapirequestparameter");
				this.SetRelatedEntities<CustomAPIRequestParameter>("user_customapirequestparameter", null, value);
				this.OnPropertyChanged("UserCustomapirequestparameter");
			}
		}

		/// <summary>
		/// 1:N user_customapiresponseproperty
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("user_customapiresponseproperty")]
		public System.Collections.Generic.IEnumerable<CustomAPIResponseProperty> UserCustomapiresponseproperty
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<CustomAPIResponseProperty>("user_customapiresponseproperty", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("UserCustomapiresponseproperty");
				this.SetRelatedEntities<CustomAPIResponseProperty>("user_customapiresponseproperty", null, value);
				this.OnPropertyChanged("UserCustomapiresponseproperty");
			}
		}

		/// <summary>
		/// 1:N user_parent_user
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("user_parent_user")]
		public System.Collections.Generic.IEnumerable<SystemUser> UserParentUser
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<SystemUser>("user_parent_user", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("UserParentUser");
				this.SetRelatedEntities<SystemUser>("user_parent_user", null, value);
				this.OnPropertyChanged("UserParentUser");
			}
		}

		/// <summary>
		/// 1:N user_routingrule
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("user_routingrule")]
		public System.Collections.Generic.IEnumerable<RoutingRule> UserRoutingrule
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<RoutingRule>("user_routingrule", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("UserRoutingrule");
				this.SetRelatedEntities<RoutingRule>("user_routingrule", null, value);
				this.OnPropertyChanged("UserRoutingrule");
			}
		}

		/// <summary>
		/// 1:N user_routingruleitem
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("user_routingruleitem")]
		public System.Collections.Generic.IEnumerable<RoutingRuleItem> UserRoutingruleitem
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<RoutingRuleItem>("user_routingruleitem", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("UserRoutingruleitem");
				this.SetRelatedEntities<RoutingRuleItem>("user_routingruleitem", null, value);
				this.OnPropertyChanged("UserRoutingruleitem");
			}
		}

		/// <summary>
		/// 1:N user_slabase
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("user_slabase")]
		public System.Collections.Generic.IEnumerable<SLA> UserSlabase
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<SLA>("user_slabase", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("UserSlabase");
				this.SetRelatedEntities<SLA>("user_slabase", null, value);
				this.OnPropertyChanged("UserSlabase");
			}
		}

		/// <summary>
		/// 1:N workflow_createdby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("workflow_createdby")]
		public System.Collections.Generic.IEnumerable<Workflow> WorkflowCreatedby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Workflow>("workflow_createdby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("WorkflowCreatedby");
				this.SetRelatedEntities<Workflow>("workflow_createdby", null, value);
				this.OnPropertyChanged("WorkflowCreatedby");
			}
		}

		/// <summary>
		/// 1:N workflow_createdonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("workflow_createdonbehalfby")]
		public System.Collections.Generic.IEnumerable<Workflow> WorkflowCreatedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Workflow>("workflow_createdonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("WorkflowCreatedonbehalfby");
				this.SetRelatedEntities<Workflow>("workflow_createdonbehalfby", null, value);
				this.OnPropertyChanged("WorkflowCreatedonbehalfby");
			}
		}

		/// <summary>
		/// 1:N workflow_modifiedby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("workflow_modifiedby")]
		public System.Collections.Generic.IEnumerable<Workflow> WorkflowModifiedby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Workflow>("workflow_modifiedby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("WorkflowModifiedby");
				this.SetRelatedEntities<Workflow>("workflow_modifiedby", null, value);
				this.OnPropertyChanged("WorkflowModifiedby");
			}
		}

		/// <summary>
		/// 1:N workflow_modifiedonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("workflow_modifiedonbehalfby")]
		public System.Collections.Generic.IEnumerable<Workflow> WorkflowModifiedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Workflow>("workflow_modifiedonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("WorkflowModifiedonbehalfby");
				this.SetRelatedEntities<Workflow>("workflow_modifiedonbehalfby", null, value);
				this.OnPropertyChanged("WorkflowModifiedonbehalfby");
			}
		}

		#endregion

		#region Options
		public static class Options
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
                public struct MsdynGdproptout
                {
                    public const bool No = false;
                    public const bool Yes = true;
                }
                public struct MsdynIsexpertenabledforswarm
                {
                    public const bool No = false;
                    public const bool Yes = true;
                }
			    public struct MsdynUserType
                {
					public const int CRMUser = 192350000;
					public const int BOTUser = 192350001;
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
		}
		#endregion

		#region LogicalNames
		public static class LogicalNames
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
				public const string MobileOfflineProfileIdName = "mobileofflineprofileidname";
				public const string MobilePhone = "mobilephone";
				public const string ModifiedBy = "modifiedby";
				public const string ModifiedOn = "modifiedon";
				public const string ModifiedOnBehalfBy = "modifiedonbehalfby";
				public const string MsdynBotDescription = "msdyn_botdescription";
				public const string MsdynBotEndpoint = "msdyn_botendpoint";
				public const string MsdynBotSecretKeys = "msdyn_botsecretkeys";
				public const string MsdynCapacity = "msdyn_capacity";
				public const string MsdynDefaultPresenceIdUser = "msdyn_defaultpresenceiduser";
				public const string MsdynGdproptout = "msdyn_gdproptout";
				public const string MsdynGridwrappercontrolfield = "msdyn_gridwrappercontrolfield";
				public const string MsdynIsexpertenabledforswarm = "msdyn_isexpertenabledforswarm";
				public const string MsdynUserType = "msdyn_usertype";
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
				public const string SiteId = "siteid";
				public const string Skills = "skills";
				public const string StageId = "stageid";
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
		public static class AlternateKeys
		{
				public const string AADObjectId = "aadobjectid";
		}
		#endregion

		#region Relations
        public static class Relations
        {
            public static class OneToMany
            {
				public const string ActioncardusersettingsOwningUser = "actioncardusersettings_owning_user";
				public const string AnnotationOwningUser = "annotation_owning_user";
				public const string BizmapSystemuserBusinessid = "bizmap_systemuser_businessid";
				public const string ChatSystemuserCreatedby = "chat_systemuser_createdby";
				public const string ChatSystemuserCreatedonbehalfby = "chat_systemuser_createdonbehalfby";
				public const string ChatSystemuserModifiedby = "chat_systemuser_modifiedby";
				public const string ChatSystemuserModifiedonbehalfby = "chat_systemuser_modifiedonbehalfby";
				public const string ChatSystemuserOwninguser = "chat_systemuser_owninguser";
				public const string ConstraintbasedgroupSystemuser = "constraintbasedgroup_systemuser";
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
				public const string DynamicpropertyinsatanceCreatedby = "Dynamicpropertyinsatance_createdby";
				public const string EmailAcceptingentitySystemuser = "email_acceptingentity_systemuser";
				public const string EquipmentSystemuser = "equipment_systemuser";
				public const string FlowmachinegroupPasswordChangedBy = "flowmachinegroup_PasswordChangedBy";
				public const string FlowmachinegroupRotationStartedBy = "flowmachinegroup_RotationStartedBy";
				public const string ImpersonatinguseridSdkmessageprocessingstep = "impersonatinguserid_sdkmessageprocessingstep";
				public const string ImportFileSystemUser = "ImportFile_SystemUser";
				public const string KnowledgearticlePrimaryauthorid = "knowledgearticle_primaryauthorid";
				public const string LeadOwningUser = "lead_owning_user";
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
				public const string LkActivitymonitorCreatedby = "lk_activitymonitor_createdby";
				public const string LkActivitymonitorCreatedonbehalfby = "lk_activitymonitor_createdonbehalfby";
				public const string LkActivitymonitorModifiedby = "lk_activitymonitor_modifiedby";
				public const string LkActivitymonitorModifiedonbehalfby = "lk_activitymonitor_modifiedonbehalfby";
				public const string LkActivitypointerCreatedby = "lk_activitypointer_createdby";
				public const string LkActivitypointerCreatedonbehalfby = "lk_activitypointer_createdonbehalfby";
				public const string LkActivitypointerModifiedby = "lk_activitypointer_modifiedby";
				public const string LkActivitypointerModifiedonbehalfby = "lk_activitypointer_modifiedonbehalfby";
				public const string LkAdminsettingsentityCreatedby = "lk_adminsettingsentity_createdby";
				public const string LkAdminsettingsentityCreatedonbehalfby = "lk_adminsettingsentity_createdonbehalfby";
				public const string LkAdminsettingsentityModifiedby = "lk_adminsettingsentity_modifiedby";
				public const string LkAdminsettingsentityModifiedonbehalfby = "lk_adminsettingsentity_modifiedonbehalfby";
				public const string LkAdvancedsimilarityruleCreatedby = "lk_advancedsimilarityrule_createdby";
				public const string LkAdvancedsimilarityruleCreatedonbehalfby = "lk_advancedsimilarityrule_createdonbehalfby";
				public const string LkAdvancedsimilarityruleModifiedby = "lk_advancedsimilarityrule_modifiedby";
				public const string LkAdvancedsimilarityruleModifiedonbehalfby = "lk_advancedsimilarityrule_modifiedonbehalfby";
				public const string LkAnnotationbaseCreatedby = "lk_annotationbase_createdby";
				public const string LkAnnotationbaseCreatedonbehalfby = "lk_annotationbase_createdonbehalfby";
				public const string LkAnnotationbaseModifiedby = "lk_annotationbase_modifiedby";
				public const string LkAnnotationbaseModifiedonbehalfby = "lk_annotationbase_modifiedonbehalfby";
				public const string LkAnnualfiscalcalendarCreatedby = "lk_annualfiscalcalendar_createdby";
				public const string LkAnnualfiscalcalendarCreatedonbehalfby = "lk_annualfiscalcalendar_createdonbehalfby";
				public const string LkAnnualfiscalcalendarModifiedby = "lk_annualfiscalcalendar_modifiedby";
				public const string LkAnnualfiscalcalendarModifiedonbehalfby = "lk_annualfiscalcalendar_modifiedonbehalfby";
				public const string LkAnnualfiscalcalendarSalespersonid = "lk_annualfiscalcalendar_salespersonid";
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
				public const string LkAppointmentCreatedby = "lk_appointment_createdby";
				public const string LkAppointmentCreatedonbehalfby = "lk_appointment_createdonbehalfby";
				public const string LkAppointmentModifiedby = "lk_appointment_modifiedby";
				public const string LkAppointmentModifiedonbehalfby = "lk_appointment_modifiedonbehalfby";
				public const string LkAppsettingCreatedby = "lk_appsetting_createdby";
				public const string LkAppsettingCreatedonbehalfby = "lk_appsetting_createdonbehalfby";
				public const string LkAppsettingModifiedby = "lk_appsetting_modifiedby";
				public const string LkAppsettingModifiedonbehalfby = "lk_appsetting_modifiedonbehalfby";
				public const string LkAppusersettingCreatedby = "lk_appusersetting_createdby";
				public const string LkAppusersettingCreatedonbehalfby = "lk_appusersetting_createdonbehalfby";
				public const string LkAppusersettingModifiedby = "lk_appusersetting_modifiedby";
				public const string LkAppusersettingModifiedonbehalfby = "lk_appusersetting_modifiedonbehalfby";
				public const string LkAsyncoperationCreatedby = "lk_asyncoperation_createdby";
				public const string LkAsyncoperationCreatedonbehalfby = "lk_asyncoperation_createdonbehalfby";
				public const string LkAsyncoperationModifiedby = "lk_asyncoperation_modifiedby";
				public const string LkAsyncoperationModifiedonbehalfby = "lk_asyncoperation_modifiedonbehalfby";
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
				public const string LkBookableresourceCreatedby = "lk_bookableresource_createdby";
				public const string LkBookableresourceCreatedonbehalfby = "lk_bookableresource_createdonbehalfby";
				public const string LkBookableresourceModifiedby = "lk_bookableresource_modifiedby";
				public const string LkBookableresourceModifiedonbehalfby = "lk_bookableresource_modifiedonbehalfby";
				public const string LkBookableresourcebookingCreatedby = "lk_bookableresourcebooking_createdby";
				public const string LkBookableresourcebookingCreatedonbehalfby = "lk_bookableresourcebooking_createdonbehalfby";
				public const string LkBookableresourcebookingModifiedby = "lk_bookableresourcebooking_modifiedby";
				public const string LkBookableresourcebookingModifiedonbehalfby = "lk_bookableresourcebooking_modifiedonbehalfby";
				public const string LkBookableresourcebookingexchangesyncidmappingCreatedby = "lk_bookableresourcebookingexchangesyncidmapping_createdby";
				public const string LkBookableresourcebookingexchangesyncidmappingCreatedonbehalfby = "lk_bookableresourcebookingexchangesyncidmapping_createdonbehalfby";
				public const string LkBookableresourcebookingexchangesyncidmappingModifiedby = "lk_bookableresourcebookingexchangesyncidmapping_modifiedby";
				public const string LkBookableresourcebookingexchangesyncidmappingModifiedonbehalfby = "lk_bookableresourcebookingexchangesyncidmapping_modifiedonbehalfby";
				public const string LkBookableresourcebookingheaderCreatedby = "lk_bookableresourcebookingheader_createdby";
				public const string LkBookableresourcebookingheaderCreatedonbehalfby = "lk_bookableresourcebookingheader_createdonbehalfby";
				public const string LkBookableresourcebookingheaderModifiedby = "lk_bookableresourcebookingheader_modifiedby";
				public const string LkBookableresourcebookingheaderModifiedonbehalfby = "lk_bookableresourcebookingheader_modifiedonbehalfby";
				public const string LkBookableresourcecategoryCreatedby = "lk_bookableresourcecategory_createdby";
				public const string LkBookableresourcecategoryCreatedonbehalfby = "lk_bookableresourcecategory_createdonbehalfby";
				public const string LkBookableresourcecategoryModifiedby = "lk_bookableresourcecategory_modifiedby";
				public const string LkBookableresourcecategoryModifiedonbehalfby = "lk_bookableresourcecategory_modifiedonbehalfby";
				public const string LkBookableresourcecategoryassnCreatedby = "lk_bookableresourcecategoryassn_createdby";
				public const string LkBookableresourcecategoryassnCreatedonbehalfby = "lk_bookableresourcecategoryassn_createdonbehalfby";
				public const string LkBookableresourcecategoryassnModifiedby = "lk_bookableresourcecategoryassn_modifiedby";
				public const string LkBookableresourcecategoryassnModifiedonbehalfby = "lk_bookableresourcecategoryassn_modifiedonbehalfby";
				public const string LkBookableresourcecharacteristicCreatedby = "lk_bookableresourcecharacteristic_createdby";
				public const string LkBookableresourcecharacteristicCreatedonbehalfby = "lk_bookableresourcecharacteristic_createdonbehalfby";
				public const string LkBookableresourcecharacteristicModifiedby = "lk_bookableresourcecharacteristic_modifiedby";
				public const string LkBookableresourcecharacteristicModifiedonbehalfby = "lk_bookableresourcecharacteristic_modifiedonbehalfby";
				public const string LkBookableresourcegroupCreatedby = "lk_bookableresourcegroup_createdby";
				public const string LkBookableresourcegroupCreatedonbehalfby = "lk_bookableresourcegroup_createdonbehalfby";
				public const string LkBookableresourcegroupModifiedby = "lk_bookableresourcegroup_modifiedby";
				public const string LkBookableresourcegroupModifiedonbehalfby = "lk_bookableresourcegroup_modifiedonbehalfby";
				public const string LkBookingstatusCreatedby = "lk_bookingstatus_createdby";
				public const string LkBookingstatusCreatedonbehalfby = "lk_bookingstatus_createdonbehalfby";
				public const string LkBookingstatusModifiedby = "lk_bookingstatus_modifiedby";
				public const string LkBookingstatusModifiedonbehalfby = "lk_bookingstatus_modifiedonbehalfby";
				public const string LkBotCreatedby = "lk_bot_createdby";
				public const string LkBotCreatedonbehalfby = "lk_bot_createdonbehalfby";
				public const string LkBotModifiedby = "lk_bot_modifiedby";
				public const string LkBotModifiedonbehalfby = "lk_bot_modifiedonbehalfby";
				public const string LkBotcomponentCreatedby = "lk_botcomponent_createdby";
				public const string LkBotcomponentCreatedonbehalfby = "lk_botcomponent_createdonbehalfby";
				public const string LkBotcomponentModifiedby = "lk_botcomponent_modifiedby";
				public const string LkBotcomponentModifiedonbehalfby = "lk_botcomponent_modifiedonbehalfby";
				public const string LkBulkdeleteoperationCreatedonbehalfby = "lk_bulkdeleteoperation_createdonbehalfby";
				public const string LkBulkdeleteoperationModifiedonbehalfby = "lk_bulkdeleteoperation_modifiedonbehalfby";
				public const string LkBulkdeleteoperationbaseCreatedby = "lk_bulkdeleteoperationbase_createdby";
				public const string LkBulkdeleteoperationbaseModifiedby = "lk_bulkdeleteoperationbase_modifiedby";
				public const string LkBulkOperationCreatedby = "lk_BulkOperation_createdby";
				public const string LkBulkOperationCreatedonbehalfby = "lk_BulkOperation_createdonbehalfby";
				public const string LkBulkOperationModifiedby = "lk_BulkOperation_modifiedby";
				public const string LkBulkOperationModifiedonbehalfby = "lk_BulkOperation_modifiedonbehalfby";
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
				public const string LkCampaignCreatedby = "lk_campaign_createdby";
				public const string LkCampaignCreatedonbehalfby = "lk_campaign_createdonbehalfby";
				public const string LkCampaignModifiedby = "lk_campaign_modifiedby";
				public const string LkCampaignModifiedonbehalfby = "lk_campaign_modifiedonbehalfby";
				public const string LkCampaignactivityCreatedby = "lk_campaignactivity_createdby";
				public const string LkCampaignactivityCreatedonbehalfby = "lk_campaignactivity_createdonbehalfby";
				public const string LkCampaignactivityModifiedby = "lk_campaignactivity_modifiedby";
				public const string LkCampaignactivityModifiedonbehalfby = "lk_campaignactivity_modifiedonbehalfby";
				public const string LkCampaignresponseCreatedby = "lk_campaignresponse_createdby";
				public const string LkCampaignresponseCreatedonbehalfby = "lk_campaignresponse_createdonbehalfby";
				public const string LkCampaignresponseModifiedby = "lk_campaignresponse_modifiedby";
				public const string LkCampaignresponseModifiedonbehalfby = "lk_campaignresponse_modifiedonbehalfby";
				public const string LkCanvasappextendedmetadataCreatedby = "lk_canvasappextendedmetadata_createdby";
				public const string LkCanvasappextendedmetadataCreatedonbehalfby = "lk_canvasappextendedmetadata_createdonbehalfby";
				public const string LkCanvasappextendedmetadataModifiedby = "lk_canvasappextendedmetadata_modifiedby";
				public const string LkCanvasappextendedmetadataModifiedonbehalfby = "lk_canvasappextendedmetadata_modifiedonbehalfby";
				public const string LkCardCreatedby = "lk_card_createdby";
				public const string LkCardCreatedonbehalfby = "lk_card_createdonbehalfby";
				public const string LkCardModifiedby = "lk_card_modifiedby";
				public const string LkCardModifiedonbehalfby = "lk_card_modifiedonbehalfby";
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
				public const string LkCharacteristicCreatedby = "lk_characteristic_createdby";
				public const string LkCharacteristicCreatedonbehalfby = "lk_characteristic_createdonbehalfby";
				public const string LkCharacteristicModifiedby = "lk_characteristic_modifiedby";
				public const string LkCharacteristicModifiedonbehalfby = "lk_characteristic_modifiedonbehalfby";
				public const string LkColumnmappingCreatedby = "lk_columnmapping_createdby";
				public const string LkColumnmappingCreatedonbehalfby = "lk_columnmapping_createdonbehalfby";
				public const string LkColumnmappingModifiedby = "lk_columnmapping_modifiedby";
				public const string LkColumnmappingModifiedonbehalfby = "lk_columnmapping_modifiedonbehalfby";
				public const string LkCommentCreatedby = "lk_comment_createdby";
				public const string LkCommentCreatedonbehalfby = "lk_comment_createdonbehalfby";
				public const string LkCommentModifiedby = "lk_comment_modifiedby";
				public const string LkCommentModifiedonbehalfby = "lk_comment_modifiedonbehalfby";
				public const string LkCompetitorCreatedonbehalfby = "lk_competitor_createdonbehalfby";
				public const string LkCompetitorModifiedonbehalfby = "lk_competitor_modifiedonbehalfby";
				public const string LkCompetitoraddressCreatedonbehalfby = "lk_competitoraddress_createdonbehalfby";
				public const string LkCompetitoraddressModifiedonbehalfby = "lk_competitoraddress_modifiedonbehalfby";
				public const string LkCompetitoraddressbaseCreatedby = "lk_competitoraddressbase_createdby";
				public const string LkCompetitoraddressbaseModifiedby = "lk_competitoraddressbase_modifiedby";
				public const string LkCompetitorbaseCreatedby = "lk_competitorbase_createdby";
				public const string LkCompetitorbaseModifiedby = "lk_competitorbase_modifiedby";
				public const string LkConnectionbaseCreatedonbehalfby = "lk_connectionbase_createdonbehalfby";
				public const string LkConnectionbaseModifiedonbehalfby = "lk_connectionbase_modifiedonbehalfby";
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
				public const string LkConstraintbasedgroupCreatedby = "lk_constraintbasedgroup_createdby";
				public const string LkConstraintbasedgroupCreatedonbehalfby = "lk_constraintbasedgroup_createdonbehalfby";
				public const string LkConstraintbasedgroupModifiedby = "lk_constraintbasedgroup_modifiedby";
				public const string LkConstraintbasedgroupModifiedonbehalfby = "lk_constraintbasedgroup_modifiedonbehalfby";
				public const string LkContactCreatedonbehalfby = "lk_contact_createdonbehalfby";
				public const string LkContactModifiedonbehalfby = "lk_contact_modifiedonbehalfby";
				public const string LkContactbaseCreatedby = "lk_contactbase_createdby";
				public const string LkContactbaseModifiedby = "lk_contactbase_modifiedby";
				public const string LkContractCreatedonbehalfby = "lk_contract_createdonbehalfby";
				public const string LkContractModifiedonbehalfby = "lk_contract_modifiedonbehalfby";
				public const string LkContractbaseCreatedby = "lk_contractbase_createdby";
				public const string LkContractbaseModifiedby = "lk_contractbase_modifiedby";
				public const string LkContractdetailCreatedonbehalfby = "lk_contractdetail_createdonbehalfby";
				public const string LkContractdetailModifiedonbehalfby = "lk_contractdetail_modifiedonbehalfby";
				public const string LkContractdetailbaseCreatedby = "lk_contractdetailbase_createdby";
				public const string LkContractdetailbaseModifiedby = "lk_contractdetailbase_modifiedby";
				public const string LkContracttemplateCreatedonbehalfby = "lk_contracttemplate_createdonbehalfby";
				public const string LkContracttemplateModifiedonbehalfby = "lk_contracttemplate_modifiedonbehalfby";
				public const string LkContracttemplatebaseCreatedby = "lk_contracttemplatebase_createdby";
				public const string LkContracttemplatebaseModifiedby = "lk_contracttemplatebase_modifiedby";
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
				public const string LkCustomeropportunityroleCreatedby = "lk_customeropportunityrole_createdby";
				public const string LkCustomeropportunityroleCreatedonbehalfby = "lk_customeropportunityrole_createdonbehalfby";
				public const string LkCustomeropportunityroleModifiedby = "lk_customeropportunityrole_modifiedby";
				public const string LkCustomeropportunityroleModifiedonbehalfby = "lk_customeropportunityrole_modifiedonbehalfby";
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
				public const string LkDiscountCreatedonbehalfby = "lk_discount_createdonbehalfby";
				public const string LkDiscountModifiedonbehalfby = "lk_discount_modifiedonbehalfby";
				public const string LkDiscountbaseCreatedby = "lk_discountbase_createdby";
				public const string LkDiscountbaseModifiedby = "lk_discountbase_modifiedby";
				public const string LkDiscounttypeCreatedonbehalfby = "lk_discounttype_createdonbehalfby";
				public const string LkDiscounttypeModifiedonbehalfby = "lk_discounttype_modifiedonbehalfby";
				public const string LkDiscounttypebaseCreatedby = "lk_discounttypebase_createdby";
				public const string LkDiscounttypebaseModifiedby = "lk_discounttypebase_modifiedby";
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
				public const string LkDynamicPropertyCreatedby = "lk_DynamicProperty_createdby";
				public const string LkDynamicPropertyCreatedonbehalfby = "lk_DynamicProperty_createdonbehalfby";
				public const string LkDynamicPropertyModifiedby = "lk_DynamicProperty_modifiedby";
				public const string LkDynamicPropertyModifiedonbehalfby = "lk_DynamicProperty_modifiedonbehalfby";
				public const string LkDynamicPropertyAssociationattributeCreatedby = "lk_DynamicPropertyAssociationattribute_createdby";
				public const string LkDynamicPropertyAssociationattributeCreatedOnBehalfBy = "lk_DynamicPropertyAssociationattribute_CreatedOnBehalfBy";
				public const string LkDynamicPropertyAssociationattributeModifiedBy = "lk_DynamicPropertyAssociationattribute_ModifiedBy";
				public const string LkDynamicPropertyAssociationattributeModifiedOnBehalfBy = "lk_DynamicPropertyAssociationattribute_ModifiedOnBehalfBy";
				public const string LkDynamicpropertyinsatanceattributeCreatedonbehalfby = "lk_Dynamicpropertyinsatanceattribute_createdonbehalfby";
				public const string LkDynamicpropertyinsatanceattributeModifiedBy = "lk_Dynamicpropertyinsatanceattribute_ModifiedBy";
				public const string LkDynamicpropertyinsatanceattributeModifiedOnBehalfBy = "lk_Dynamicpropertyinsatanceattribute_ModifiedOnBehalfBy";
				public const string LkDynamicPropertyOptionSetItemCreatedby = "lk_DynamicPropertyOptionSetItem_createdby";
				public const string LkDynamicPropertyOptionSetItemCreatedonbehalfby = "lk_DynamicPropertyOptionSetItem_createdonbehalfby";
				public const string LkDynamicPropertyOptionSetItemModifiedby = "lk_DynamicPropertyOptionSetItem_modifiedby";
				public const string LkDynamicPropertyOptionSetItemModifiedonbehalfby = "lk_DynamicPropertyOptionSetItem_modifiedonbehalfby";
				public const string LkEc4uAcquirelegalbasisCreatedby = "lk_ec4u_acquirelegalbasis_createdby";
				public const string LkEc4uAcquirelegalbasisCreatedonbehalfby = "lk_ec4u_acquirelegalbasis_createdonbehalfby";
				public const string LkEc4uAcquirelegalbasisModifiedby = "lk_ec4u_acquirelegalbasis_modifiedby";
				public const string LkEc4uAcquirelegalbasisModifiedonbehalfby = "lk_ec4u_acquirelegalbasis_modifiedonbehalfby";
				public const string LkEc4uGdprBpfCorrectionCreatedby = "lk_ec4u_gdpr_bpf_correction_createdby";
				public const string LkEc4uGdprBpfCorrectionCreatedonbehalfby = "lk_ec4u_gdpr_bpf_correction_createdonbehalfby";
				public const string LkEc4uGdprBpfCorrectionModifiedby = "lk_ec4u_gdpr_bpf_correction_modifiedby";
				public const string LkEc4uGdprBpfCorrectionModifiedonbehalfby = "lk_ec4u_gdpr_bpf_correction_modifiedonbehalfby";
				public const string LkEc4uGdprBpfDeletionCreatedby = "lk_ec4u_gdpr_bpf_deletion_createdby";
				public const string LkEc4uGdprBpfDeletionCreatedonbehalfby = "lk_ec4u_gdpr_bpf_deletion_createdonbehalfby";
				public const string LkEc4uGdprBpfDeletionModifiedby = "lk_ec4u_gdpr_bpf_deletion_modifiedby";
				public const string LkEc4uGdprBpfDeletionModifiedonbehalfby = "lk_ec4u_gdpr_bpf_deletion_modifiedonbehalfby";
				public const string LkEc4uGdprBpfInformationCreatedby = "lk_ec4u_gdpr_bpf_information_createdby";
				public const string LkEc4uGdprBpfInformationCreatedonbehalfby = "lk_ec4u_gdpr_bpf_information_createdonbehalfby";
				public const string LkEc4uGdprBpfInformationModifiedby = "lk_ec4u_gdpr_bpf_information_modifiedby";
				public const string LkEc4uGdprBpfInformationModifiedonbehalfby = "lk_ec4u_gdpr_bpf_information_modifiedonbehalfby";
				public const string LkEc4uGdprConfigEntityCreatedby = "lk_ec4u_gdpr_config_entity_createdby";
				public const string LkEc4uGdprConfigEntityCreatedonbehalfby = "lk_ec4u_gdpr_config_entity_createdonbehalfby";
				public const string LkEc4uGdprConfigEntityModifiedby = "lk_ec4u_gdpr_config_entity_modifiedby";
				public const string LkEc4uGdprConfigEntityModifiedonbehalfby = "lk_ec4u_gdpr_config_entity_modifiedonbehalfby";
				public const string LkEc4uGdprConfigFieldCreatedby = "lk_ec4u_gdpr_config_field_createdby";
				public const string LkEc4uGdprConfigFieldCreatedonbehalfby = "lk_ec4u_gdpr_config_field_createdonbehalfby";
				public const string LkEc4uGdprConfigFieldModifiedby = "lk_ec4u_gdpr_config_field_modifiedby";
				public const string LkEc4uGdprConfigFieldModifiedonbehalfby = "lk_ec4u_gdpr_config_field_modifiedonbehalfby";
				public const string LkEc4uGdprConfigHierarchyCreatedby = "lk_ec4u_gdpr_config_hierarchy_createdby";
				public const string LkEc4uGdprConfigHierarchyCreatedonbehalfby = "lk_ec4u_gdpr_config_hierarchy_createdonbehalfby";
				public const string LkEc4uGdprConfigHierarchyModifiedby = "lk_ec4u_gdpr_config_hierarchy_modifiedby";
				public const string LkEc4uGdprConfigHierarchyModifiedonbehalfby = "lk_ec4u_gdpr_config_hierarchy_modifiedonbehalfby";
				public const string LkEc4uGdprProtocolCreatedby = "lk_ec4u_gdpr_protocol_createdby";
				public const string LkEc4uGdprProtocolCreatedonbehalfby = "lk_ec4u_gdpr_protocol_createdonbehalfby";
				public const string LkEc4uGdprProtocolDetailCreatedby = "lk_ec4u_gdpr_protocol_detail_createdby";
				public const string LkEc4uGdprProtocolDetailCreatedonbehalfby = "lk_ec4u_gdpr_protocol_detail_createdonbehalfby";
				public const string LkEc4uGdprProtocolDetailModifiedby = "lk_ec4u_gdpr_protocol_detail_modifiedby";
				public const string LkEc4uGdprProtocolDetailModifiedonbehalfby = "lk_ec4u_gdpr_protocol_detail_modifiedonbehalfby";
				public const string LkEc4uGdprProtocolModifiedby = "lk_ec4u_gdpr_protocol_modifiedby";
				public const string LkEc4uGdprProtocolModifiedonbehalfby = "lk_ec4u_gdpr_protocol_modifiedonbehalfby";
				public const string LkEc4uGdprReportCreatedby = "lk_ec4u_gdpr_report_createdby";
				public const string LkEc4uGdprReportCreatedonbehalfby = "lk_ec4u_gdpr_report_createdonbehalfby";
				public const string LkEc4uGdprReportModifiedby = "lk_ec4u_gdpr_report_modifiedby";
				public const string LkEc4uGdprReportModifiedonbehalfby = "lk_ec4u_gdpr_report_modifiedonbehalfby";
				public const string LkEc4uGdprRequestCreatedby = "lk_ec4u_gdpr_request_createdby";
				public const string LkEc4uGdprRequestCreatedonbehalfby = "lk_ec4u_gdpr_request_createdonbehalfby";
				public const string LkEc4uGdprRequestModifiedby = "lk_ec4u_gdpr_request_modifiedby";
				public const string LkEc4uGdprRequestModifiedonbehalfby = "lk_ec4u_gdpr_request_modifiedonbehalfby";
				public const string LkEc4uLegalbasisCreatedby = "lk_ec4u_legalbasis_createdby";
				public const string LkEc4uLegalbasisCreatedonbehalfby = "lk_ec4u_legalbasis_createdonbehalfby";
				public const string LkEc4uLegalbasisModifiedby = "lk_ec4u_legalbasis_modifiedby";
				public const string LkEc4uLegalbasisModifiedonbehalfby = "lk_ec4u_legalbasis_modifiedonbehalfby";
				public const string LkEc4uLegalbasistypeCreatedby = "lk_ec4u_legalbasistype_createdby";
				public const string LkEc4uLegalbasistypeCreatedonbehalfby = "lk_ec4u_legalbasistype_createdonbehalfby";
				public const string LkEc4uLegalbasistypeModifiedby = "lk_ec4u_legalbasistype_modifiedby";
				public const string LkEc4uLegalbasistypeModifiedonbehalfby = "lk_ec4u_legalbasistype_modifiedonbehalfby";
				public const string LkEmailCreatedby = "lk_email_createdby";
				public const string LkEmailCreatedonbehalfby = "lk_email_createdonbehalfby";
				public const string LkEmailModifiedby = "lk_email_modifiedby";
				public const string LkEmailModifiedonbehalfby = "lk_email_modifiedonbehalfby";
				public const string LkEmailserverprofileCreatedby = "lk_emailserverprofile_createdby";
				public const string LkEmailserverprofileCreatedonbehalfby = "lk_emailserverprofile_createdonbehalfby";
				public const string LkEmailserverprofileModifiedby = "lk_emailserverprofile_modifiedby";
				public const string LkEmailserverprofileModifiedonbehalfby = "lk_emailserverprofile_modifiedonbehalfby";
				public const string LkEmailsignaturebaseCreatedby = "lk_emailsignaturebase_createdby";
				public const string LkEmailsignaturebaseCreatedonbehalfby = "lk_emailsignaturebase_createdonbehalfby";
				public const string LkEmailsignaturebaseModifiedby = "lk_emailsignaturebase_modifiedby";
				public const string LkEmailsignaturebaseModifiedonbehalfby = "lk_emailsignaturebase_modifiedonbehalfby";
				public const string LkEntitlementCreatedby = "lk_entitlement_createdby";
				public const string LkEntitlementCreatedonbehalfby = "lk_entitlement_createdonbehalfby";
				public const string LkEntitlementModifiedby = "lk_entitlement_modifiedby";
				public const string LkEntitlementModifiedonbehalfby = "lk_entitlement_modifiedonbehalfby";
				public const string LkEntitlementchannelCreatedby = "lk_entitlementchannel_createdby";
				public const string LkEntitlementchannelCreatedonbehalfby = "lk_entitlementchannel_createdonbehalfby";
				public const string LkEntitlementchannelModifiedby = "lk_entitlementchannel_modifiedby";
				public const string LkEntitlementchannelModifiedonbehalfby = "lk_entitlementchannel_modifiedonbehalfby";
				public const string LkEntitlemententityallocationtypemappingCreatedby = "lk_entitlemententityallocationtypemapping_createdby";
				public const string LkEntitlemententityallocationtypemappingCreatedonbehalfby = "lk_entitlemententityallocationtypemapping_createdonbehalfby";
				public const string LkEntitlemententityallocationtypemappingModifiedby = "lk_entitlemententityallocationtypemapping_modifiedby";
				public const string LkEntitlemententityallocationtypemappingModifiedonbehalfby = "lk_entitlemententityallocationtypemapping_modifiedonbehalfby";
				public const string LkEntitlementtemplateCreatedby = "lk_entitlementtemplate_createdby";
				public const string LkEntitlementtemplateCreatedonbehalfby = "lk_entitlementtemplate_createdonbehalfby";
				public const string LkEntitlementtemplateModifiedby = "lk_entitlementtemplate_modifiedby";
				public const string LkEntitlementtemplateModifiedonbehalfby = "lk_entitlementtemplate_modifiedonbehalfby";
				public const string LkEntitlementtemplatechannelCreatedby = "lk_entitlementtemplatechannel_createdby";
				public const string LkEntitlementtemplatechannelCreatedonbehalfby = "lk_entitlementtemplatechannel_createdonbehalfby";
				public const string LkEntitlementtemplatechannelModifiedby = "lk_entitlementtemplatechannel_modifiedby";
				public const string LkEntitlementtemplatechannelModifiedonbehalfby = "lk_entitlementtemplatechannel_modifiedonbehalfby";
				public const string LkEntitymapCreatedonbehalfby = "lk_entitymap_createdonbehalfby";
				public const string LkEntitymapModifiedonbehalfby = "lk_entitymap_modifiedonbehalfby";
				public const string LkEnvironmentvariabledefinitionCreatedby = "lk_environmentvariabledefinition_createdby";
				public const string LkEnvironmentvariabledefinitionCreatedonbehalfby = "lk_environmentvariabledefinition_createdonbehalfby";
				public const string LkEnvironmentvariabledefinitionModifiedby = "lk_environmentvariabledefinition_modifiedby";
				public const string LkEnvironmentvariabledefinitionModifiedonbehalfby = "lk_environmentvariabledefinition_modifiedonbehalfby";
				public const string LkEnvironmentvariablevalueCreatedby = "lk_environmentvariablevalue_createdby";
				public const string LkEnvironmentvariablevalueCreatedonbehalfby = "lk_environmentvariablevalue_createdonbehalfby";
				public const string LkEnvironmentvariablevalueModifiedby = "lk_environmentvariablevalue_modifiedby";
				public const string LkEnvironmentvariablevalueModifiedonbehalfby = "lk_environmentvariablevalue_modifiedonbehalfby";
				public const string LkEquipmentCreatedby = "lk_equipment_createdby";
				public const string LkEquipmentCreatedonbehalfby = "lk_equipment_createdonbehalfby";
				public const string LkEquipmentModifiedby = "lk_equipment_modifiedby";
				public const string LkEquipmentModifiedonbehalfby = "lk_equipment_modifiedonbehalfby";
				public const string LkExpandereventCreatedonbehalfby = "lk_expanderevent_createdonbehalfby";
				public const string LkExpandereventModifiedonbehalfby = "lk_expanderevent_modifiedonbehalfby";
				public const string LkExpiredprocessCreatedby = "lk_expiredprocess_createdby";
				public const string LkExpiredprocessCreatedonbehalfby = "lk_expiredprocess_createdonbehalfby";
				public const string LkExpiredprocessModifiedby = "lk_expiredprocess_modifiedby";
				public const string LkExpiredprocessModifiedonbehalfby = "lk_expiredprocess_modifiedonbehalfby";
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
				public const string LkFaxCreatedby = "lk_fax_createdby";
				public const string LkFaxCreatedonbehalfby = "lk_fax_createdonbehalfby";
				public const string LkFaxModifiedby = "lk_fax_modifiedby";
				public const string LkFaxModifiedonbehalfby = "lk_fax_modifiedonbehalfby";
				public const string LkFeaturecontrolsettingCreatedby = "lk_featurecontrolsetting_createdby";
				public const string LkFeaturecontrolsettingCreatedonbehalfby = "lk_featurecontrolsetting_createdonbehalfby";
				public const string LkFeaturecontrolsettingModifiedby = "lk_featurecontrolsetting_modifiedby";
				public const string LkFeaturecontrolsettingModifiedonbehalfby = "lk_featurecontrolsetting_modifiedonbehalfby";
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
				public const string LkFlowsessionCreatedby = "lk_flowsession_createdby";
				public const string LkFlowsessionCreatedonbehalfby = "lk_flowsession_createdonbehalfby";
				public const string LkFlowsessionModifiedby = "lk_flowsession_modifiedby";
				public const string LkFlowsessionModifiedonbehalfby = "lk_flowsession_modifiedonbehalfby";
				public const string LkGoalCreatedby = "lk_goal_createdby";
				public const string LkGoalCreatedonbehalfby = "lk_goal_createdonbehalfby";
				public const string LkGoalModifiedby = "lk_goal_modifiedby";
				public const string LkGoalModifiedonbehalfby = "lk_goal_modifiedonbehalfby";
				public const string LkGoalrollupqueryCreatedby = "lk_goalrollupquery_createdby";
				public const string LkGoalrollupqueryCreatedonbehalfby = "lk_goalrollupquery_createdonbehalfby";
				public const string LkGoalrollupqueryModifiedby = "lk_goalrollupquery_modifiedby";
				public const string LkGoalrollupqueryModifiedonbehalfby = "lk_goalrollupquery_modifiedonbehalfby";
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
				public const string LkIncidentbaseCreatedby = "lk_incidentbase_createdby";
				public const string LkIncidentbaseCreatedonbehalfby = "lk_incidentbase_createdonbehalfby";
				public const string LkIncidentbaseModifiedby = "lk_incidentbase_modifiedby";
				public const string LkIncidentbaseModifiedonbehalfby = "lk_incidentbase_modifiedonbehalfby";
				public const string LkIncidentresolutionCreatedby = "lk_incidentresolution_createdby";
				public const string LkIncidentresolutionCreatedonbehalfby = "lk_incidentresolution_createdonbehalfby";
				public const string LkIncidentresolutionModifiedby = "lk_incidentresolution_modifiedby";
				public const string LkIncidentresolutionModifiedonbehalfby = "lk_incidentresolution_modifiedonbehalfby";
				public const string LkIntegrationstatusCreatedby = "lk_integrationstatus_createdby";
				public const string LkIntegrationstatusCreatedonbehalfby = "lk_integrationstatus_createdonbehalfby";
				public const string LkIntegrationstatusModifiedby = "lk_integrationstatus_modifiedby";
				public const string LkIntegrationstatusModifiedonbehalfby = "lk_integrationstatus_modifiedonbehalfby";
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
				public const string LkInvoiceCreatedonbehalfby = "lk_invoice_createdonbehalfby";
				public const string LkInvoiceModifiedonbehalfby = "lk_invoice_modifiedonbehalfby";
				public const string LkInvoicebaseCreatedby = "lk_invoicebase_createdby";
				public const string LkInvoicebaseModifiedby = "lk_invoicebase_modifiedby";
				public const string LkInvoicedetailCreatedonbehalfby = "lk_invoicedetail_createdonbehalfby";
				public const string LkInvoicedetailModifiedonbehalfby = "lk_invoicedetail_modifiedonbehalfby";
				public const string LkInvoicedetailbaseCreatedby = "lk_invoicedetailbase_createdby";
				public const string LkInvoicedetailbaseModifiedby = "lk_invoicedetailbase_modifiedby";
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
				public const string LkKnowledgearticleincidentCreatedby = "lk_knowledgearticleincident_createdby";
				public const string LkKnowledgearticleincidentCreatedonbehalfby = "lk_knowledgearticleincident_createdonbehalfby";
				public const string LkKnowledgearticleincidentModifiedby = "lk_knowledgearticleincident_modifiedby";
				public const string LkKnowledgearticleincidentModifiedonbehalfby = "lk_knowledgearticleincident_modifiedonbehalfby";
				public const string LkKnowledgearticleviewsCreatedby = "lk_knowledgearticleviews_createdby";
				public const string LkKnowledgearticleviewsCreatedonbehalfby = "lk_knowledgearticleviews_createdonbehalfby";
				public const string LkKnowledgearticleviewsModifiedby = "lk_knowledgearticleviews_modifiedby";
				public const string LkKnowledgearticleviewsModifiedonbehalfby = "lk_knowledgearticleviews_modifiedonbehalfby";
				public const string LkKnowledgeBaseRecordCreatedby = "lk_KnowledgeBaseRecord_createdby";
				public const string LkKnowledgeBaseRecordCreatedonbehalfby = "lk_KnowledgeBaseRecord_createdonbehalfby";
				public const string LkKnowledgeBaseRecordModifiedby = "lk_KnowledgeBaseRecord_modifiedby";
				public const string LkKnowledgeBaseRecordModifiedonbehalfby = "lk_KnowledgeBaseRecord_modifiedonbehalfby";
				public const string LkKnowledgesearchmodelCreatedby = "lk_knowledgesearchmodel_createdby";
				public const string LkKnowledgesearchmodelCreatedonbehalfby = "lk_knowledgesearchmodel_createdonbehalfby";
				public const string LkKnowledgesearchmodelModifiedby = "lk_knowledgesearchmodel_modifiedby";
				public const string LkKnowledgesearchmodelModifiedonbehalfby = "lk_knowledgesearchmodel_modifiedonbehalfby";
				public const string LkLeadCreatedonbehalfby = "lk_lead_createdonbehalfby";
				public const string LkLeadModifiedonbehalfby = "lk_lead_modifiedonbehalfby";
				public const string LkLeadaddressCreatedonbehalfby = "lk_leadaddress_createdonbehalfby";
				public const string LkLeadaddressModifiedonbehalfby = "lk_leadaddress_modifiedonbehalfby";
				public const string LkLeadaddressbaseCreatedby = "lk_leadaddressbase_createdby";
				public const string LkLeadaddressbaseModifiedby = "lk_leadaddressbase_modifiedby";
				public const string LkLeadbaseCreatedby = "lk_leadbase_createdby";
				public const string LkLeadbaseModifiedby = "lk_leadbase_modifiedby";
				public const string LkLeadtoopportunitysalesprocessCreatedby = "lk_leadtoopportunitysalesprocess_createdby";
				public const string LkLeadtoopportunitysalesprocessCreatedonbehalfby = "lk_leadtoopportunitysalesprocess_createdonbehalfby";
				public const string LkLeadtoopportunitysalesprocessModifiedby = "lk_leadtoopportunitysalesprocess_modifiedby";
				public const string LkLeadtoopportunitysalesprocessModifiedonbehalfby = "lk_leadtoopportunitysalesprocess_modifiedonbehalfby";
				public const string LkLetterCreatedby = "lk_letter_createdby";
				public const string LkLetterCreatedonbehalfby = "lk_letter_createdonbehalfby";
				public const string LkLetterModifiedby = "lk_letter_modifiedby";
				public const string LkLetterModifiedonbehalfby = "lk_letter_modifiedonbehalfby";
				public const string LkListCreatedby = "lk_list_createdby";
				public const string LkListCreatedonbehalfby = "lk_list_createdonbehalfby";
				public const string LkListModifiedby = "lk_list_modifiedby";
				public const string LkListModifiedonbehalfby = "lk_list_modifiedonbehalfby";
				public const string LkListmemberCreatedby = "lk_listmember_createdby";
				public const string LkListmemberCreatedonbehalfby = "lk_listmember_createdonbehalfby";
				public const string LkListmemberModifiedby = "lk_listmember_modifiedby";
				public const string LkListmemberModifiedonbehalfby = "lk_listmember_modifiedonbehalfby";
				public const string LkListoperationCreatedby = "lk_listoperation_createdby";
				public const string LkListoperationCreatedonbehalfby = "lk_listoperation_createdonbehalfby";
				public const string LkListoperationModifiedby = "lk_listoperation_modifiedby";
				public const string LkListoperationModifiedonbehalfby = "lk_listoperation_modifiedonbehalfby";
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
				public const string LkManagedidentityCreatedby = "lk_managedidentity_createdby";
				public const string LkManagedidentityCreatedonbehalfby = "lk_managedidentity_createdonbehalfby";
				public const string LkManagedidentityModifiedby = "lk_managedidentity_modifiedby";
				public const string LkManagedidentityModifiedonbehalfby = "lk_managedidentity_modifiedonbehalfby";
				public const string LkMarketingformdisplayattributesCreatedby = "lk_marketingformdisplayattributes_createdby";
				public const string LkMarketingformdisplayattributesCreatedonbehalfby = "lk_marketingformdisplayattributes_createdonbehalfby";
				public const string LkMarketingformdisplayattributesModifiedby = "lk_marketingformdisplayattributes_modifiedby";
				public const string LkMarketingformdisplayattributesModifiedonbehalfby = "lk_marketingformdisplayattributes_modifiedonbehalfby";
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
				public const string LkMsdynActioncardregardingCreatedby = "lk_msdyn_actioncardregarding_createdby";
				public const string LkMsdynActioncardregardingCreatedonbehalfby = "lk_msdyn_actioncardregarding_createdonbehalfby";
				public const string LkMsdynActioncardregardingModifiedby = "lk_msdyn_actioncardregarding_modifiedby";
				public const string LkMsdynActioncardregardingModifiedonbehalfby = "lk_msdyn_actioncardregarding_modifiedonbehalfby";
				public const string LkMsdynActioncardrolesettingCreatedby = "lk_msdyn_actioncardrolesetting_createdby";
				public const string LkMsdynActioncardrolesettingCreatedonbehalfby = "lk_msdyn_actioncardrolesetting_createdonbehalfby";
				public const string LkMsdynActioncardrolesettingModifiedby = "lk_msdyn_actioncardrolesetting_modifiedby";
				public const string LkMsdynActioncardrolesettingModifiedonbehalfby = "lk_msdyn_actioncardrolesetting_modifiedonbehalfby";
				public const string LkMsdynAdaptivecardconfigurationCreatedby = "lk_msdyn_adaptivecardconfiguration_createdby";
				public const string LkMsdynAdaptivecardconfigurationCreatedonbehalfby = "lk_msdyn_adaptivecardconfiguration_createdonbehalfby";
				public const string LkMsdynAdaptivecardconfigurationModifiedby = "lk_msdyn_adaptivecardconfiguration_modifiedby";
				public const string LkMsdynAdaptivecardconfigurationModifiedonbehalfby = "lk_msdyn_adaptivecardconfiguration_modifiedonbehalfby";
				public const string LkMsdynAdjustmenthistoryCreatedby = "lk_msdyn_adjustmenthistory_createdby";
				public const string LkMsdynAdjustmenthistoryCreatedonbehalfby = "lk_msdyn_adjustmenthistory_createdonbehalfby";
				public const string LkMsdynAdjustmenthistoryModifiedby = "lk_msdyn_adjustmenthistory_modifiedby";
				public const string LkMsdynAdjustmenthistoryModifiedonbehalfby = "lk_msdyn_adjustmenthistory_modifiedonbehalfby";
				public const string LkMsdynAdminappstateCreatedby = "lk_msdyn_adminappstate_createdby";
				public const string LkMsdynAdminappstateCreatedonbehalfby = "lk_msdyn_adminappstate_createdonbehalfby";
				public const string LkMsdynAdminappstateModifiedby = "lk_msdyn_adminappstate_modifiedby";
				public const string LkMsdynAdminappstateModifiedonbehalfby = "lk_msdyn_adminappstate_modifiedonbehalfby";
				public const string LkMsdynAgentresourceforecastingCreatedby = "lk_msdyn_agentresourceforecasting_createdby";
				public const string LkMsdynAgentresourceforecastingCreatedonbehalfby = "lk_msdyn_agentresourceforecasting_createdonbehalfby";
				public const string LkMsdynAgentresourceforecastingModifiedby = "lk_msdyn_agentresourceforecasting_modifiedby";
				public const string LkMsdynAgentresourceforecastingModifiedonbehalfby = "lk_msdyn_agentresourceforecasting_modifiedonbehalfby";
				public const string LkMsdynAgentstatushistoryCreatedby = "lk_msdyn_agentstatushistory_createdby";
				public const string LkMsdynAgentstatushistoryCreatedonbehalfby = "lk_msdyn_agentstatushistory_createdonbehalfby";
				public const string LkMsdynAgentstatushistoryModifiedby = "lk_msdyn_agentstatushistory_modifiedby";
				public const string LkMsdynAgentstatushistoryModifiedonbehalfby = "lk_msdyn_agentstatushistory_modifiedonbehalfby";
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
				public const string LkMsdynAicontactsuggestionCreatedby = "lk_msdyn_aicontactsuggestion_createdby";
				public const string LkMsdynAicontactsuggestionCreatedonbehalfby = "lk_msdyn_aicontactsuggestion_createdonbehalfby";
				public const string LkMsdynAicontactsuggestionModifiedby = "lk_msdyn_aicontactsuggestion_modifiedby";
				public const string LkMsdynAicontactsuggestionModifiedonbehalfby = "lk_msdyn_aicontactsuggestion_modifiedonbehalfby";
				public const string LkMsdynAifptrainingdocumentCreatedby = "lk_msdyn_aifptrainingdocument_createdby";
				public const string LkMsdynAifptrainingdocumentCreatedonbehalfby = "lk_msdyn_aifptrainingdocument_createdonbehalfby";
				public const string LkMsdynAifptrainingdocumentModifiedby = "lk_msdyn_aifptrainingdocument_modifiedby";
				public const string LkMsdynAifptrainingdocumentModifiedonbehalfby = "lk_msdyn_aifptrainingdocument_modifiedonbehalfby";
				public const string LkMsdynAimodelCreatedby = "lk_msdyn_aimodel_createdby";
				public const string LkMsdynAimodelCreatedonbehalfby = "lk_msdyn_aimodel_createdonbehalfby";
				public const string LkMsdynAimodelModifiedby = "lk_msdyn_aimodel_modifiedby";
				public const string LkMsdynAimodelModifiedonbehalfby = "lk_msdyn_aimodel_modifiedonbehalfby";
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
				public const string LkMsdynAitemplateCreatedby = "lk_msdyn_aitemplate_createdby";
				public const string LkMsdynAitemplateCreatedonbehalfby = "lk_msdyn_aitemplate_createdonbehalfby";
				public const string LkMsdynAitemplateModifiedby = "lk_msdyn_aitemplate_modifiedby";
				public const string LkMsdynAitemplateModifiedonbehalfby = "lk_msdyn_aitemplate_modifiedonbehalfby";
				public const string LkMsdynAnalysiscomponentCreatedby = "lk_msdyn_analysiscomponent_createdby";
				public const string LkMsdynAnalysiscomponentCreatedonbehalfby = "lk_msdyn_analysiscomponent_createdonbehalfby";
				public const string LkMsdynAnalysiscomponentModifiedby = "lk_msdyn_analysiscomponent_modifiedby";
				public const string LkMsdynAnalysiscomponentModifiedonbehalfby = "lk_msdyn_analysiscomponent_modifiedonbehalfby";
				public const string LkMsdynAnalysisjobCreatedby = "lk_msdyn_analysisjob_createdby";
				public const string LkMsdynAnalysisjobCreatedonbehalfby = "lk_msdyn_analysisjob_createdonbehalfby";
				public const string LkMsdynAnalysisjobModifiedby = "lk_msdyn_analysisjob_modifiedby";
				public const string LkMsdynAnalysisjobModifiedonbehalfby = "lk_msdyn_analysisjob_modifiedonbehalfby";
				public const string LkMsdynAnalysisresultCreatedby = "lk_msdyn_analysisresult_createdby";
				public const string LkMsdynAnalysisresultCreatedonbehalfby = "lk_msdyn_analysisresult_createdonbehalfby";
				public const string LkMsdynAnalysisresultModifiedby = "lk_msdyn_analysisresult_modifiedby";
				public const string LkMsdynAnalysisresultModifiedonbehalfby = "lk_msdyn_analysisresult_modifiedonbehalfby";
				public const string LkMsdynAnalysisresultdetailCreatedby = "lk_msdyn_analysisresultdetail_createdby";
				public const string LkMsdynAnalysisresultdetailCreatedonbehalfby = "lk_msdyn_analysisresultdetail_createdonbehalfby";
				public const string LkMsdynAnalysisresultdetailModifiedby = "lk_msdyn_analysisresultdetail_modifiedby";
				public const string LkMsdynAnalysisresultdetailModifiedonbehalfby = "lk_msdyn_analysisresultdetail_modifiedonbehalfby";
				public const string LkMsdynAnalyticsCreatedby = "lk_msdyn_analytics_createdby";
				public const string LkMsdynAnalyticsCreatedonbehalfby = "lk_msdyn_analytics_createdonbehalfby";
				public const string LkMsdynAnalyticsModifiedby = "lk_msdyn_analytics_modifiedby";
				public const string LkMsdynAnalyticsModifiedonbehalfby = "lk_msdyn_analytics_modifiedonbehalfby";
				public const string LkMsdynAnalyticsadminsettingsCreatedby = "lk_msdyn_analyticsadminsettings_createdby";
				public const string LkMsdynAnalyticsadminsettingsCreatedonbehalfby = "lk_msdyn_analyticsadminsettings_createdonbehalfby";
				public const string LkMsdynAnalyticsadminsettingsModifiedby = "lk_msdyn_analyticsadminsettings_modifiedby";
				public const string LkMsdynAnalyticsadminsettingsModifiedonbehalfby = "lk_msdyn_analyticsadminsettings_modifiedonbehalfby";
				public const string LkMsdynAnalyticsforcsCreatedby = "lk_msdyn_analyticsforcs_createdby";
				public const string LkMsdynAnalyticsforcsCreatedonbehalfby = "lk_msdyn_analyticsforcs_createdonbehalfby";
				public const string LkMsdynAnalyticsforcsModifiedby = "lk_msdyn_analyticsforcs_modifiedby";
				public const string LkMsdynAnalyticsforcsModifiedonbehalfby = "lk_msdyn_analyticsforcs_modifiedonbehalfby";
				public const string LkMsdynAppconfigurationCreatedby = "lk_msdyn_appconfiguration_createdby";
				public const string LkMsdynAppconfigurationCreatedonbehalfby = "lk_msdyn_appconfiguration_createdonbehalfby";
				public const string LkMsdynAppconfigurationModifiedby = "lk_msdyn_appconfiguration_modifiedby";
				public const string LkMsdynAppconfigurationModifiedonbehalfby = "lk_msdyn_appconfiguration_modifiedonbehalfby";
				public const string LkMsdynAppinsightsmetadataCreatedby = "lk_msdyn_appinsightsmetadata_createdby";
				public const string LkMsdynAppinsightsmetadataCreatedonbehalfby = "lk_msdyn_appinsightsmetadata_createdonbehalfby";
				public const string LkMsdynAppinsightsmetadataModifiedby = "lk_msdyn_appinsightsmetadata_modifiedby";
				public const string LkMsdynAppinsightsmetadataModifiedonbehalfby = "lk_msdyn_appinsightsmetadata_modifiedonbehalfby";
				public const string LkMsdynApplicationextensionCreatedby = "lk_msdyn_applicationextension_createdby";
				public const string LkMsdynApplicationextensionCreatedonbehalfby = "lk_msdyn_applicationextension_createdonbehalfby";
				public const string LkMsdynApplicationextensionModifiedby = "lk_msdyn_applicationextension_modifiedby";
				public const string LkMsdynApplicationextensionModifiedonbehalfby = "lk_msdyn_applicationextension_modifiedonbehalfby";
				public const string LkMsdynApplicationtabtemplateCreatedby = "lk_msdyn_applicationtabtemplate_createdby";
				public const string LkMsdynApplicationtabtemplateCreatedonbehalfby = "lk_msdyn_applicationtabtemplate_createdonbehalfby";
				public const string LkMsdynApplicationtabtemplateModifiedby = "lk_msdyn_applicationtabtemplate_modifiedby";
				public const string LkMsdynApplicationtabtemplateModifiedonbehalfby = "lk_msdyn_applicationtabtemplate_modifiedonbehalfby";
				public const string LkMsdynAssetcategorytemplateassociationCreatedby = "lk_msdyn_assetcategorytemplateassociation_createdby";
				public const string LkMsdynAssetcategorytemplateassociationCreatedonbehalfby = "lk_msdyn_assetcategorytemplateassociation_createdonbehalfby";
				public const string LkMsdynAssetcategorytemplateassociationModifiedby = "lk_msdyn_assetcategorytemplateassociation_modifiedby";
				public const string LkMsdynAssetcategorytemplateassociationModifiedonbehalfby = "lk_msdyn_assetcategorytemplateassociation_modifiedonbehalfby";
				public const string LkMsdynAssettemplateassociationCreatedby = "lk_msdyn_assettemplateassociation_createdby";
				public const string LkMsdynAssettemplateassociationCreatedonbehalfby = "lk_msdyn_assettemplateassociation_createdonbehalfby";
				public const string LkMsdynAssettemplateassociationModifiedby = "lk_msdyn_assettemplateassociation_modifiedby";
				public const string LkMsdynAssettemplateassociationModifiedonbehalfby = "lk_msdyn_assettemplateassociation_modifiedonbehalfby";
				public const string LkMsdynAssignmentconfigurationCreatedby = "lk_msdyn_assignmentconfiguration_createdby";
				public const string LkMsdynAssignmentconfigurationCreatedonbehalfby = "lk_msdyn_assignmentconfiguration_createdonbehalfby";
				public const string LkMsdynAssignmentconfigurationModifiedby = "lk_msdyn_assignmentconfiguration_modifiedby";
				public const string LkMsdynAssignmentconfigurationModifiedonbehalfby = "lk_msdyn_assignmentconfiguration_modifiedonbehalfby";
				public const string LkMsdynAssignmentconfigurationstepCreatedby = "lk_msdyn_assignmentconfigurationstep_createdby";
				public const string LkMsdynAssignmentconfigurationstepCreatedonbehalfby = "lk_msdyn_assignmentconfigurationstep_createdonbehalfby";
				public const string LkMsdynAssignmentconfigurationstepModifiedby = "lk_msdyn_assignmentconfigurationstep_modifiedby";
				public const string LkMsdynAssignmentconfigurationstepModifiedonbehalfby = "lk_msdyn_assignmentconfigurationstep_modifiedonbehalfby";
				public const string LkMsdynAssignmentmapCreatedby = "lk_msdyn_assignmentmap_createdby";
				public const string LkMsdynAssignmentmapCreatedonbehalfby = "lk_msdyn_assignmentmap_createdonbehalfby";
				public const string LkMsdynAssignmentmapModifiedby = "lk_msdyn_assignmentmap_modifiedby";
				public const string LkMsdynAssignmentmapModifiedonbehalfby = "lk_msdyn_assignmentmap_modifiedonbehalfby";
				public const string LkMsdynAssignmentruleCreatedby = "lk_msdyn_assignmentrule_createdby";
				public const string LkMsdynAssignmentruleCreatedonbehalfby = "lk_msdyn_assignmentrule_createdonbehalfby";
				public const string LkMsdynAssignmentruleModifiedby = "lk_msdyn_assignmentrule_modifiedby";
				public const string LkMsdynAssignmentruleModifiedonbehalfby = "lk_msdyn_assignmentrule_modifiedonbehalfby";
				public const string LkMsdynAttributeCreatedby = "lk_msdyn_attribute_createdby";
				public const string LkMsdynAttributeCreatedonbehalfby = "lk_msdyn_attribute_createdonbehalfby";
				public const string LkMsdynAttributeModifiedby = "lk_msdyn_attribute_modifiedby";
				public const string LkMsdynAttributeModifiedonbehalfby = "lk_msdyn_attribute_modifiedonbehalfby";
				public const string LkMsdynAttributeinfluencestatisticsCreatedby = "lk_msdyn_attributeinfluencestatistics_createdby";
				public const string LkMsdynAttributeinfluencestatisticsCreatedonbehalfby = "lk_msdyn_attributeinfluencestatistics_createdonbehalfby";
				public const string LkMsdynAttributeinfluencestatisticsModifiedby = "lk_msdyn_attributeinfluencestatistics_modifiedby";
				public const string LkMsdynAttributeinfluencestatisticsModifiedonbehalfby = "lk_msdyn_attributeinfluencestatistics_modifiedonbehalfby";
				public const string LkMsdynAttributevalueCreatedby = "lk_msdyn_attributevalue_createdby";
				public const string LkMsdynAttributevalueCreatedonbehalfby = "lk_msdyn_attributevalue_createdonbehalfby";
				public const string LkMsdynAttributevalueModifiedby = "lk_msdyn_attributevalue_modifiedby";
				public const string LkMsdynAttributevalueModifiedonbehalfby = "lk_msdyn_attributevalue_modifiedonbehalfby";
				public const string LkMsdynAuthenticationsettingsCreatedby = "lk_msdyn_authenticationsettings_createdby";
				public const string LkMsdynAuthenticationsettingsCreatedonbehalfby = "lk_msdyn_authenticationsettings_createdonbehalfby";
				public const string LkMsdynAuthenticationsettingsModifiedby = "lk_msdyn_authenticationsettings_modifiedby";
				public const string LkMsdynAuthenticationsettingsModifiedonbehalfby = "lk_msdyn_authenticationsettings_modifiedonbehalfby";
				public const string LkMsdynAuthsettingsentryCreatedby = "lk_msdyn_authsettingsentry_createdby";
				public const string LkMsdynAuthsettingsentryCreatedonbehalfby = "lk_msdyn_authsettingsentry_createdonbehalfby";
				public const string LkMsdynAuthsettingsentryModifiedby = "lk_msdyn_authsettingsentry_modifiedby";
				public const string LkMsdynAuthsettingsentryModifiedonbehalfby = "lk_msdyn_authsettingsentry_modifiedonbehalfby";
				public const string LkMsdynAutocaptureruleCreatedby = "lk_msdyn_autocapturerule_createdby";
				public const string LkMsdynAutocaptureruleCreatedonbehalfby = "lk_msdyn_autocapturerule_createdonbehalfby";
				public const string LkMsdynAutocaptureruleModifiedby = "lk_msdyn_autocapturerule_modifiedby";
				public const string LkMsdynAutocaptureruleModifiedonbehalfby = "lk_msdyn_autocapturerule_modifiedonbehalfby";
				public const string LkMsdynAutocapturesettingsCreatedby = "lk_msdyn_autocapturesettings_createdby";
				public const string LkMsdynAutocapturesettingsCreatedonbehalfby = "lk_msdyn_autocapturesettings_createdonbehalfby";
				public const string LkMsdynAutocapturesettingsModifiedby = "lk_msdyn_autocapturesettings_modifiedby";
				public const string LkMsdynAutocapturesettingsModifiedonbehalfby = "lk_msdyn_autocapturesettings_modifiedonbehalfby";
				public const string LkMsdynBookableresourcecapacityprofileCreatedby = "lk_msdyn_bookableresourcecapacityprofile_createdby";
				public const string LkMsdynBookableresourcecapacityprofileCreatedonbehalfby = "lk_msdyn_bookableresourcecapacityprofile_createdonbehalfby";
				public const string LkMsdynBookableresourcecapacityprofileModifiedby = "lk_msdyn_bookableresourcecapacityprofile_modifiedby";
				public const string LkMsdynBookableresourcecapacityprofileModifiedonbehalfby = "lk_msdyn_bookableresourcecapacityprofile_modifiedonbehalfby";
				public const string LkMsdynCallablecontextCreatedby = "lk_msdyn_callablecontext_createdby";
				public const string LkMsdynCallablecontextCreatedonbehalfby = "lk_msdyn_callablecontext_createdonbehalfby";
				public const string LkMsdynCallablecontextModifiedby = "lk_msdyn_callablecontext_modifiedby";
				public const string LkMsdynCallablecontextModifiedonbehalfby = "lk_msdyn_callablecontext_modifiedonbehalfby";
				public const string LkMsdynCannedmessageCreatedby = "lk_msdyn_cannedmessage_createdby";
				public const string LkMsdynCannedmessageCreatedonbehalfby = "lk_msdyn_cannedmessage_createdonbehalfby";
				public const string LkMsdynCannedmessageModifiedby = "lk_msdyn_cannedmessage_modifiedby";
				public const string LkMsdynCannedmessageModifiedonbehalfby = "lk_msdyn_cannedmessage_modifiedonbehalfby";
				public const string LkMsdynCapacityprofileCreatedby = "lk_msdyn_capacityprofile_createdby";
				public const string LkMsdynCapacityprofileCreatedonbehalfby = "lk_msdyn_capacityprofile_createdonbehalfby";
				public const string LkMsdynCapacityprofileModifiedby = "lk_msdyn_capacityprofile_modifiedby";
				public const string LkMsdynCapacityprofileModifiedonbehalfby = "lk_msdyn_capacityprofile_modifiedonbehalfby";
				public const string LkMsdynCaseenrichmentCreatedby = "lk_msdyn_caseenrichment_createdby";
				public const string LkMsdynCaseenrichmentCreatedonbehalfby = "lk_msdyn_caseenrichment_createdonbehalfby";
				public const string LkMsdynCaseenrichmentModifiedby = "lk_msdyn_caseenrichment_modifiedby";
				public const string LkMsdynCaseenrichmentModifiedonbehalfby = "lk_msdyn_caseenrichment_modifiedonbehalfby";
				public const string LkMsdynCasesuggestionrequestpayloadCreatedby = "lk_msdyn_casesuggestionrequestpayload_createdby";
				public const string LkMsdynCasesuggestionrequestpayloadCreatedonbehalfby = "lk_msdyn_casesuggestionrequestpayload_createdonbehalfby";
				public const string LkMsdynCasesuggestionrequestpayloadModifiedby = "lk_msdyn_casesuggestionrequestpayload_modifiedby";
				public const string LkMsdynCasesuggestionrequestpayloadModifiedonbehalfby = "lk_msdyn_casesuggestionrequestpayload_modifiedonbehalfby";
				public const string LkMsdynCasetopicCreatedby = "lk_msdyn_casetopic_createdby";
				public const string LkMsdynCasetopicCreatedonbehalfby = "lk_msdyn_casetopic_createdonbehalfby";
				public const string LkMsdynCasetopicIncidentCreatedby = "lk_msdyn_casetopic_incident_createdby";
				public const string LkMsdynCasetopicIncidentCreatedonbehalfby = "lk_msdyn_casetopic_incident_createdonbehalfby";
				public const string LkMsdynCasetopicIncidentModifiedby = "lk_msdyn_casetopic_incident_modifiedby";
				public const string LkMsdynCasetopicIncidentModifiedonbehalfby = "lk_msdyn_casetopic_incident_modifiedonbehalfby";
				public const string LkMsdynCasetopicModifiedby = "lk_msdyn_casetopic_modifiedby";
				public const string LkMsdynCasetopicModifiedonbehalfby = "lk_msdyn_casetopic_modifiedonbehalfby";
				public const string LkMsdynCasetopicsettingCreatedby = "lk_msdyn_casetopicsetting_createdby";
				public const string LkMsdynCasetopicsettingCreatedonbehalfby = "lk_msdyn_casetopicsetting_createdonbehalfby";
				public const string LkMsdynCasetopicsettingModifiedby = "lk_msdyn_casetopicsetting_modifiedby";
				public const string LkMsdynCasetopicsettingModifiedonbehalfby = "lk_msdyn_casetopicsetting_modifiedonbehalfby";
				public const string LkMsdynCasetopicsummaryCreatedby = "lk_msdyn_casetopicsummary_createdby";
				public const string LkMsdynCasetopicsummaryCreatedonbehalfby = "lk_msdyn_casetopicsummary_createdonbehalfby";
				public const string LkMsdynCasetopicsummaryModifiedby = "lk_msdyn_casetopicsummary_modifiedby";
				public const string LkMsdynCasetopicsummaryModifiedonbehalfby = "lk_msdyn_casetopicsummary_modifiedonbehalfby";
				public const string LkMsdynCdsentityengagementctxCreatedby = "lk_msdyn_cdsentityengagementctx_createdby";
				public const string LkMsdynCdsentityengagementctxCreatedonbehalfby = "lk_msdyn_cdsentityengagementctx_createdonbehalfby";
				public const string LkMsdynCdsentityengagementctxModifiedby = "lk_msdyn_cdsentityengagementctx_modifiedby";
				public const string LkMsdynCdsentityengagementctxModifiedonbehalfby = "lk_msdyn_cdsentityengagementctx_modifiedonbehalfby";
				public const string LkMsdynChannelcapabilityCreatedby = "lk_msdyn_channelcapability_createdby";
				public const string LkMsdynChannelcapabilityCreatedonbehalfby = "lk_msdyn_channelcapability_createdonbehalfby";
				public const string LkMsdynChannelcapabilityModifiedby = "lk_msdyn_channelcapability_modifiedby";
				public const string LkMsdynChannelcapabilityModifiedonbehalfby = "lk_msdyn_channelcapability_modifiedonbehalfby";
				public const string LkMsdynChanneldefinitionCreatedby = "lk_msdyn_channeldefinition_createdby";
				public const string LkMsdynChanneldefinitionCreatedonbehalfby = "lk_msdyn_channeldefinition_createdonbehalfby";
				public const string LkMsdynChanneldefinitionModifiedby = "lk_msdyn_channeldefinition_modifiedby";
				public const string LkMsdynChanneldefinitionModifiedonbehalfby = "lk_msdyn_channeldefinition_modifiedonbehalfby";
				public const string LkMsdynChanneldefinitionconsentCreatedby = "lk_msdyn_channeldefinitionconsent_createdby";
				public const string LkMsdynChanneldefinitionconsentCreatedonbehalfby = "lk_msdyn_channeldefinitionconsent_createdonbehalfby";
				public const string LkMsdynChanneldefinitionconsentModifiedby = "lk_msdyn_channeldefinitionconsent_modifiedby";
				public const string LkMsdynChanneldefinitionconsentModifiedonbehalfby = "lk_msdyn_channeldefinitionconsent_modifiedonbehalfby";
				public const string LkMsdynChanneldefinitionlocaleCreatedby = "lk_msdyn_channeldefinitionlocale_createdby";
				public const string LkMsdynChanneldefinitionlocaleCreatedonbehalfby = "lk_msdyn_channeldefinitionlocale_createdonbehalfby";
				public const string LkMsdynChanneldefinitionlocaleModifiedby = "lk_msdyn_channeldefinitionlocale_modifiedby";
				public const string LkMsdynChanneldefinitionlocaleModifiedonbehalfby = "lk_msdyn_channeldefinitionlocale_modifiedonbehalfby";
				public const string LkMsdynChannelinstanceCreatedby = "lk_msdyn_channelinstance_createdby";
				public const string LkMsdynChannelinstanceCreatedonbehalfby = "lk_msdyn_channelinstance_createdonbehalfby";
				public const string LkMsdynChannelinstanceModifiedby = "lk_msdyn_channelinstance_modifiedby";
				public const string LkMsdynChannelinstanceModifiedonbehalfby = "lk_msdyn_channelinstance_modifiedonbehalfby";
				public const string LkMsdynChannelinstanceaccountCreatedby = "lk_msdyn_channelinstanceaccount_createdby";
				public const string LkMsdynChannelinstanceaccountCreatedonbehalfby = "lk_msdyn_channelinstanceaccount_createdonbehalfby";
				public const string LkMsdynChannelinstanceaccountModifiedby = "lk_msdyn_channelinstanceaccount_modifiedby";
				public const string LkMsdynChannelinstanceaccountModifiedonbehalfby = "lk_msdyn_channelinstanceaccount_modifiedonbehalfby";
				public const string LkMsdynChannelmessagepartCreatedby = "lk_msdyn_channelmessagepart_createdby";
				public const string LkMsdynChannelmessagepartCreatedonbehalfby = "lk_msdyn_channelmessagepart_createdonbehalfby";
				public const string LkMsdynChannelmessagepartModifiedby = "lk_msdyn_channelmessagepart_modifiedby";
				public const string LkMsdynChannelmessagepartModifiedonbehalfby = "lk_msdyn_channelmessagepart_modifiedonbehalfby";
				public const string LkMsdynChannelproviderCreatedby = "lk_msdyn_channelprovider_createdby";
				public const string LkMsdynChannelproviderCreatedonbehalfby = "lk_msdyn_channelprovider_createdonbehalfby";
				public const string LkMsdynChannelproviderModifiedby = "lk_msdyn_channelprovider_modifiedby";
				public const string LkMsdynChannelproviderModifiedonbehalfby = "lk_msdyn_channelprovider_modifiedonbehalfby";
				public const string LkMsdynCollabgraphresourceCreatedby = "lk_msdyn_collabgraphresource_createdby";
				public const string LkMsdynCollabgraphresourceCreatedonbehalfby = "lk_msdyn_collabgraphresource_createdonbehalfby";
				public const string LkMsdynCollabgraphresourceModifiedby = "lk_msdyn_collabgraphresource_modifiedby";
				public const string LkMsdynCollabgraphresourceModifiedonbehalfby = "lk_msdyn_collabgraphresource_modifiedonbehalfby";
				public const string LkMsdynConsumingapplicationCreatedby = "lk_msdyn_consumingapplication_createdby";
				public const string LkMsdynConsumingapplicationCreatedonbehalfby = "lk_msdyn_consumingapplication_createdonbehalfby";
				public const string LkMsdynConsumingapplicationModifiedby = "lk_msdyn_consumingapplication_modifiedby";
				public const string LkMsdynConsumingapplicationModifiedonbehalfby = "lk_msdyn_consumingapplication_modifiedonbehalfby";
				public const string LkMsdynContactsuggestionruleCreatedby = "lk_msdyn_contactsuggestionrule_createdby";
				public const string LkMsdynContactsuggestionruleCreatedonbehalfby = "lk_msdyn_contactsuggestionrule_createdonbehalfby";
				public const string LkMsdynContactsuggestionruleModifiedby = "lk_msdyn_contactsuggestionrule_modifiedby";
				public const string LkMsdynContactsuggestionruleModifiedonbehalfby = "lk_msdyn_contactsuggestionrule_modifiedonbehalfby";
				public const string LkMsdynContactsuggestionrulesetCreatedby = "lk_msdyn_contactsuggestionruleset_createdby";
				public const string LkMsdynContactsuggestionrulesetCreatedonbehalfby = "lk_msdyn_contactsuggestionruleset_createdonbehalfby";
				public const string LkMsdynContactsuggestionrulesetModifiedby = "lk_msdyn_contactsuggestionruleset_modifiedby";
				public const string LkMsdynContactsuggestionrulesetModifiedonbehalfby = "lk_msdyn_contactsuggestionruleset_modifiedonbehalfby";
				public const string LkMsdynConversationactionCreatedby = "lk_msdyn_conversationaction_createdby";
				public const string LkMsdynConversationactionCreatedonbehalfby = "lk_msdyn_conversationaction_createdonbehalfby";
				public const string LkMsdynConversationactionModifiedby = "lk_msdyn_conversationaction_modifiedby";
				public const string LkMsdynConversationactionModifiedonbehalfby = "lk_msdyn_conversationaction_modifiedonbehalfby";
				public const string LkMsdynConversationactionlocaleCreatedby = "lk_msdyn_conversationactionlocale_createdby";
				public const string LkMsdynConversationactionlocaleCreatedonbehalfby = "lk_msdyn_conversationactionlocale_createdonbehalfby";
				public const string LkMsdynConversationactionlocaleModifiedby = "lk_msdyn_conversationactionlocale_modifiedby";
				public const string LkMsdynConversationactionlocaleModifiedonbehalfby = "lk_msdyn_conversationactionlocale_modifiedonbehalfby";
				public const string LkMsdynConversationdataCreatedby = "lk_msdyn_conversationdata_createdby";
				public const string LkMsdynConversationdataCreatedonbehalfby = "lk_msdyn_conversationdata_createdonbehalfby";
				public const string LkMsdynConversationdataModifiedby = "lk_msdyn_conversationdata_modifiedby";
				public const string LkMsdynConversationdataModifiedonbehalfby = "lk_msdyn_conversationdata_modifiedonbehalfby";
				public const string LkMsdynCrmconnectionCreatedby = "lk_msdyn_crmconnection_createdby";
				public const string LkMsdynCrmconnectionCreatedonbehalfby = "lk_msdyn_crmconnection_createdonbehalfby";
				public const string LkMsdynCrmconnectionModifiedby = "lk_msdyn_crmconnection_modifiedby";
				public const string LkMsdynCrmconnectionModifiedonbehalfby = "lk_msdyn_crmconnection_modifiedonbehalfby";
				public const string LkMsdynCsadminconfigCreatedby = "lk_msdyn_csadminconfig_createdby";
				public const string LkMsdynCsadminconfigCreatedonbehalfby = "lk_msdyn_csadminconfig_createdonbehalfby";
				public const string LkMsdynCsadminconfigModifiedby = "lk_msdyn_csadminconfig_modifiedby";
				public const string LkMsdynCsadminconfigModifiedonbehalfby = "lk_msdyn_csadminconfig_modifiedonbehalfby";
				public const string LkMsdynCustomapirulesetconfigurationCreatedby = "lk_msdyn_customapirulesetconfiguration_createdby";
				public const string LkMsdynCustomapirulesetconfigurationCreatedonbehalfby = "lk_msdyn_customapirulesetconfiguration_createdonbehalfby";
				public const string LkMsdynCustomapirulesetconfigurationModifiedby = "lk_msdyn_customapirulesetconfiguration_modifiedby";
				public const string LkMsdynCustomapirulesetconfigurationModifiedonbehalfby = "lk_msdyn_customapirulesetconfiguration_modifiedonbehalfby";
				public const string LkMsdynCustomcontrolextendedsettingsCreatedby = "lk_msdyn_customcontrolextendedsettings_createdby";
				public const string LkMsdynCustomcontrolextendedsettingsCreatedonbehalfby = "lk_msdyn_customcontrolextendedsettings_createdonbehalfby";
				public const string LkMsdynCustomcontrolextendedsettingsModifiedby = "lk_msdyn_customcontrolextendedsettings_modifiedby";
				public const string LkMsdynCustomcontrolextendedsettingsModifiedonbehalfby = "lk_msdyn_customcontrolextendedsettings_modifiedonbehalfby";
				public const string LkMsdynCustomerassetCreatedby = "lk_msdyn_customerasset_createdby";
				public const string LkMsdynCustomerassetCreatedonbehalfby = "lk_msdyn_customerasset_createdonbehalfby";
				public const string LkMsdynCustomerassetModifiedby = "lk_msdyn_customerasset_modifiedby";
				public const string LkMsdynCustomerassetModifiedonbehalfby = "lk_msdyn_customerasset_modifiedonbehalfby";
				public const string LkMsdynCustomerassetattachmentCreatedby = "lk_msdyn_customerassetattachment_createdby";
				public const string LkMsdynCustomerassetattachmentCreatedonbehalfby = "lk_msdyn_customerassetattachment_createdonbehalfby";
				public const string LkMsdynCustomerassetattachmentModifiedby = "lk_msdyn_customerassetattachment_modifiedby";
				public const string LkMsdynCustomerassetattachmentModifiedonbehalfby = "lk_msdyn_customerassetattachment_modifiedonbehalfby";
				public const string LkMsdynCustomerassetcategoryCreatedby = "lk_msdyn_customerassetcategory_createdby";
				public const string LkMsdynCustomerassetcategoryCreatedonbehalfby = "lk_msdyn_customerassetcategory_createdonbehalfby";
				public const string LkMsdynCustomerassetcategoryModifiedby = "lk_msdyn_customerassetcategory_modifiedby";
				public const string LkMsdynCustomerassetcategoryModifiedonbehalfby = "lk_msdyn_customerassetcategory_modifiedonbehalfby";
				public const string LkMsdynCustomeremailcommunicationCreatedby = "lk_msdyn_customeremailcommunication_createdby";
				public const string LkMsdynCustomeremailcommunicationCreatedonbehalfby = "lk_msdyn_customeremailcommunication_createdonbehalfby";
				public const string LkMsdynCustomeremailcommunicationModifiedby = "lk_msdyn_customeremailcommunication_modifiedby";
				public const string LkMsdynCustomeremailcommunicationModifiedonbehalfby = "lk_msdyn_customeremailcommunication_modifiedonbehalfby";
				public const string LkMsdynDataanalyticscustomizedreportCreatedby = "lk_msdyn_dataanalyticscustomizedreport_createdby";
				public const string LkMsdynDataanalyticscustomizedreportCreatedonbehalfby = "lk_msdyn_dataanalyticscustomizedreport_createdonbehalfby";
				public const string LkMsdynDataanalyticscustomizedreportModifiedby = "lk_msdyn_dataanalyticscustomizedreport_modifiedby";
				public const string LkMsdynDataanalyticscustomizedreportModifiedonbehalfby = "lk_msdyn_dataanalyticscustomizedreport_modifiedonbehalfby";
				public const string LkMsdynDataanalyticsdatasetCreatedby = "lk_msdyn_dataanalyticsdataset_createdby";
				public const string LkMsdynDataanalyticsdatasetCreatedonbehalfby = "lk_msdyn_dataanalyticsdataset_createdonbehalfby";
				public const string LkMsdynDataanalyticsdatasetModifiedby = "lk_msdyn_dataanalyticsdataset_modifiedby";
				public const string LkMsdynDataanalyticsdatasetModifiedonbehalfby = "lk_msdyn_dataanalyticsdataset_modifiedonbehalfby";
				public const string LkMsdynDataanalyticsreportCreatedby = "lk_msdyn_dataanalyticsreport_createdby";
				public const string LkMsdynDataanalyticsreportCreatedonbehalfby = "lk_msdyn_dataanalyticsreport_createdonbehalfby";
				public const string LkMsdynDataanalyticsreportCsrmanagerCreatedby = "lk_msdyn_dataanalyticsreport_csrmanager_createdby";
				public const string LkMsdynDataanalyticsreportCsrmanagerCreatedonbehalfby = "lk_msdyn_dataanalyticsreport_csrmanager_createdonbehalfby";
				public const string LkMsdynDataanalyticsreportCsrmanagerModifiedby = "lk_msdyn_dataanalyticsreport_csrmanager_modifiedby";
				public const string LkMsdynDataanalyticsreportCsrmanagerModifiedonbehalfby = "lk_msdyn_dataanalyticsreport_csrmanager_modifiedonbehalfby";
				public const string LkMsdynDataanalyticsreportForecastCreatedby = "lk_msdyn_dataanalyticsreport_forecast_createdby";
				public const string LkMsdynDataanalyticsreportForecastCreatedonbehalfby = "lk_msdyn_dataanalyticsreport_forecast_createdonbehalfby";
				public const string LkMsdynDataanalyticsreportForecastModifiedby = "lk_msdyn_dataanalyticsreport_forecast_modifiedby";
				public const string LkMsdynDataanalyticsreportForecastModifiedonbehalfby = "lk_msdyn_dataanalyticsreport_forecast_modifiedonbehalfby";
				public const string LkMsdynDataanalyticsreportKsinsightsCreatedby = "lk_msdyn_dataanalyticsreport_ksinsights_createdby";
				public const string LkMsdynDataanalyticsreportKsinsightsCreatedonbehalfby = "lk_msdyn_dataanalyticsreport_ksinsights_createdonbehalfby";
				public const string LkMsdynDataanalyticsreportKsinsightsModifiedby = "lk_msdyn_dataanalyticsreport_ksinsights_modifiedby";
				public const string LkMsdynDataanalyticsreportKsinsightsModifiedonbehalfby = "lk_msdyn_dataanalyticsreport_ksinsights_modifiedonbehalfby";
				public const string LkMsdynDataanalyticsreportModifiedby = "lk_msdyn_dataanalyticsreport_modifiedby";
				public const string LkMsdynDataanalyticsreportModifiedonbehalfby = "lk_msdyn_dataanalyticsreport_modifiedonbehalfby";
				public const string LkMsdynDataanalyticsreportSutreportingCreatedby = "lk_msdyn_dataanalyticsreport_sutreporting_createdby";
				public const string LkMsdynDataanalyticsreportSutreportingCreatedonbehalfby = "lk_msdyn_dataanalyticsreport_sutreporting_createdonbehalfby";
				public const string LkMsdynDataanalyticsreportSutreportingModifiedby = "lk_msdyn_dataanalyticsreport_sutreporting_modifiedby";
				public const string LkMsdynDataanalyticsreportSutreportingModifiedonbehalfby = "lk_msdyn_dataanalyticsreport_sutreporting_modifiedonbehalfby";
				public const string LkMsdynDataanalyticsworkspaceCreatedby = "lk_msdyn_dataanalyticsworkspace_createdby";
				public const string LkMsdynDataanalyticsworkspaceCreatedonbehalfby = "lk_msdyn_dataanalyticsworkspace_createdonbehalfby";
				public const string LkMsdynDataanalyticsworkspaceModifiedby = "lk_msdyn_dataanalyticsworkspace_modifiedby";
				public const string LkMsdynDataanalyticsworkspaceModifiedonbehalfby = "lk_msdyn_dataanalyticsworkspace_modifiedonbehalfby";
				public const string LkMsdynDatabaseversionCreatedby = "lk_msdyn_databaseversion_createdby";
				public const string LkMsdynDatabaseversionCreatedonbehalfby = "lk_msdyn_databaseversion_createdonbehalfby";
				public const string LkMsdynDatabaseversionModifiedby = "lk_msdyn_databaseversion_modifiedby";
				public const string LkMsdynDatabaseversionModifiedonbehalfby = "lk_msdyn_databaseversion_modifiedonbehalfby";
				public const string LkMsdynDataflowCreatedby = "lk_msdyn_dataflow_createdby";
				public const string LkMsdynDataflowCreatedonbehalfby = "lk_msdyn_dataflow_createdonbehalfby";
				public const string LkMsdynDataflowModifiedby = "lk_msdyn_dataflow_modifiedby";
				public const string LkMsdynDataflowModifiedonbehalfby = "lk_msdyn_dataflow_modifiedonbehalfby";
				public const string LkMsdynDataflowrefreshhistoryCreatedby = "lk_msdyn_dataflowrefreshhistory_createdby";
				public const string LkMsdynDataflowrefreshhistoryCreatedonbehalfby = "lk_msdyn_dataflowrefreshhistory_createdonbehalfby";
				public const string LkMsdynDataflowrefreshhistoryModifiedby = "lk_msdyn_dataflowrefreshhistory_modifiedby";
				public const string LkMsdynDataflowrefreshhistoryModifiedonbehalfby = "lk_msdyn_dataflowrefreshhistory_modifiedonbehalfby";
				public const string LkMsdynDatahygienesettinginfoCreatedby = "lk_msdyn_datahygienesettinginfo_createdby";
				public const string LkMsdynDatahygienesettinginfoCreatedonbehalfby = "lk_msdyn_datahygienesettinginfo_createdonbehalfby";
				public const string LkMsdynDatahygienesettinginfoModifiedby = "lk_msdyn_datahygienesettinginfo_modifiedby";
				public const string LkMsdynDatahygienesettinginfoModifiedonbehalfby = "lk_msdyn_datahygienesettinginfo_modifiedonbehalfby";
				public const string LkMsdynDatainsightsandanalyticsfeatureCreatedby = "lk_msdyn_datainsightsandanalyticsfeature_createdby";
				public const string LkMsdynDatainsightsandanalyticsfeatureCreatedonbehalfby = "lk_msdyn_datainsightsandanalyticsfeature_createdonbehalfby";
				public const string LkMsdynDatainsightsandanalyticsfeatureModifiedby = "lk_msdyn_datainsightsandanalyticsfeature_modifiedby";
				public const string LkMsdynDatainsightsandanalyticsfeatureModifiedonbehalfby = "lk_msdyn_datainsightsandanalyticsfeature_modifiedonbehalfby";
				public const string LkMsdynDealmanageraccessCreatedby = "lk_msdyn_dealmanageraccess_createdby";
				public const string LkMsdynDealmanageraccessCreatedonbehalfby = "lk_msdyn_dealmanageraccess_createdonbehalfby";
				public const string LkMsdynDealmanageraccessModifiedby = "lk_msdyn_dealmanageraccess_modifiedby";
				public const string LkMsdynDealmanageraccessModifiedonbehalfby = "lk_msdyn_dealmanageraccess_modifiedonbehalfby";
				public const string LkMsdynDealmanagersettingsCreatedby = "lk_msdyn_dealmanagersettings_createdby";
				public const string LkMsdynDealmanagersettingsCreatedonbehalfby = "lk_msdyn_dealmanagersettings_createdonbehalfby";
				public const string LkMsdynDealmanagersettingsModifiedby = "lk_msdyn_dealmanagersettings_modifiedby";
				public const string LkMsdynDealmanagersettingsModifiedonbehalfby = "lk_msdyn_dealmanagersettings_modifiedonbehalfby";
				public const string LkMsdynDecisioncontractCreatedby = "lk_msdyn_decisioncontract_createdby";
				public const string LkMsdynDecisioncontractCreatedonbehalfby = "lk_msdyn_decisioncontract_createdonbehalfby";
				public const string LkMsdynDecisioncontractModifiedby = "lk_msdyn_decisioncontract_modifiedby";
				public const string LkMsdynDecisioncontractModifiedonbehalfby = "lk_msdyn_decisioncontract_modifiedonbehalfby";
				public const string LkMsdynDecisionrulesetCreatedby = "lk_msdyn_decisionruleset_createdby";
				public const string LkMsdynDecisionrulesetCreatedonbehalfby = "lk_msdyn_decisionruleset_createdonbehalfby";
				public const string LkMsdynDecisionrulesetModifiedby = "lk_msdyn_decisionruleset_modifiedby";
				public const string LkMsdynDecisionrulesetModifiedonbehalfby = "lk_msdyn_decisionruleset_modifiedonbehalfby";
				public const string LkMsdynDefextendedchannelinstanceCreatedby = "lk_msdyn_defextendedchannelinstance_createdby";
				public const string LkMsdynDefextendedchannelinstanceCreatedonbehalfby = "lk_msdyn_defextendedchannelinstance_createdonbehalfby";
				public const string LkMsdynDefextendedchannelinstanceModifiedby = "lk_msdyn_defextendedchannelinstance_modifiedby";
				public const string LkMsdynDefextendedchannelinstanceModifiedonbehalfby = "lk_msdyn_defextendedchannelinstance_modifiedonbehalfby";
				public const string LkMsdynDefextendedchannelinstanceaccountCreatedby = "lk_msdyn_defextendedchannelinstanceaccount_createdby";
				public const string LkMsdynDefextendedchannelinstanceaccountCreatedonbehalfby = "lk_msdyn_defextendedchannelinstanceaccount_createdonbehalfby";
				public const string LkMsdynDefextendedchannelinstanceaccountModifiedby = "lk_msdyn_defextendedchannelinstanceaccount_modifiedby";
				public const string LkMsdynDefextendedchannelinstanceaccountModifiedonbehalfby = "lk_msdyn_defextendedchannelinstanceaccount_modifiedonbehalfby";
				public const string LkMsdynDistributedlockCreatedby = "lk_msdyn_distributedlock_createdby";
				public const string LkMsdynDistributedlockCreatedonbehalfby = "lk_msdyn_distributedlock_createdonbehalfby";
				public const string LkMsdynDistributedlockModifiedby = "lk_msdyn_distributedlock_modifiedby";
				public const string LkMsdynDistributedlockModifiedonbehalfby = "lk_msdyn_distributedlock_modifiedonbehalfby";
				public const string LkMsdynDuplicatedetectionpluginrunCreatedby = "lk_msdyn_duplicatedetectionpluginrun_createdby";
				public const string LkMsdynDuplicatedetectionpluginrunCreatedonbehalfby = "lk_msdyn_duplicatedetectionpluginrun_createdonbehalfby";
				public const string LkMsdynDuplicatedetectionpluginrunModifiedby = "lk_msdyn_duplicatedetectionpluginrun_modifiedby";
				public const string LkMsdynDuplicatedetectionpluginrunModifiedonbehalfby = "lk_msdyn_duplicatedetectionpluginrun_modifiedonbehalfby";
				public const string LkMsdynDuplicateleadmappingCreatedby = "lk_msdyn_duplicateleadmapping_createdby";
				public const string LkMsdynDuplicateleadmappingCreatedonbehalfby = "lk_msdyn_duplicateleadmapping_createdonbehalfby";
				public const string LkMsdynDuplicateleadmappingModifiedby = "lk_msdyn_duplicateleadmapping_modifiedby";
				public const string LkMsdynDuplicateleadmappingModifiedonbehalfby = "lk_msdyn_duplicateleadmapping_modifiedonbehalfby";
				public const string LkMsdynEffortpredictionresultCreatedby = "lk_msdyn_effortpredictionresult_createdby";
				public const string LkMsdynEffortpredictionresultCreatedonbehalfby = "lk_msdyn_effortpredictionresult_createdonbehalfby";
				public const string LkMsdynEffortpredictionresultModifiedby = "lk_msdyn_effortpredictionresult_modifiedby";
				public const string LkMsdynEffortpredictionresultModifiedonbehalfby = "lk_msdyn_effortpredictionresult_modifiedonbehalfby";
				public const string LkMsdynEntityconfigCreatedby = "lk_msdyn_entityconfig_createdby";
				public const string LkMsdynEntityconfigCreatedonbehalfby = "lk_msdyn_entityconfig_createdonbehalfby";
				public const string LkMsdynEntityconfigModifiedby = "lk_msdyn_entityconfig_modifiedby";
				public const string LkMsdynEntityconfigModifiedonbehalfby = "lk_msdyn_entityconfig_modifiedonbehalfby";
				public const string LkMsdynEntitydeltachangeCreatedby = "lk_msdyn_entitydeltachange_createdby";
				public const string LkMsdynEntitydeltachangeCreatedonbehalfby = "lk_msdyn_entitydeltachange_createdonbehalfby";
				public const string LkMsdynEntitydeltachangeModifiedby = "lk_msdyn_entitydeltachange_modifiedby";
				public const string LkMsdynEntitydeltachangeModifiedonbehalfby = "lk_msdyn_entitydeltachange_modifiedonbehalfby";
				public const string LkMsdynEntitylinkchatconfigurationCreatedby = "lk_msdyn_entitylinkchatconfiguration_createdby";
				public const string LkMsdynEntitylinkchatconfigurationCreatedonbehalfby = "lk_msdyn_entitylinkchatconfiguration_createdonbehalfby";
				public const string LkMsdynEntitylinkchatconfigurationModifiedby = "lk_msdyn_entitylinkchatconfiguration_modifiedby";
				public const string LkMsdynEntitylinkchatconfigurationModifiedonbehalfby = "lk_msdyn_entitylinkchatconfiguration_modifiedonbehalfby";
				public const string LkMsdynEntityrankingruleCreatedby = "lk_msdyn_entityrankingrule_createdby";
				public const string LkMsdynEntityrankingruleCreatedonbehalfby = "lk_msdyn_entityrankingrule_createdonbehalfby";
				public const string LkMsdynEntityrankingruleModifiedby = "lk_msdyn_entityrankingrule_modifiedby";
				public const string LkMsdynEntityrankingruleModifiedonbehalfby = "lk_msdyn_entityrankingrule_modifiedonbehalfby";
				public const string LkMsdynEntityrefreshhistoryCreatedby = "lk_msdyn_entityrefreshhistory_createdby";
				public const string LkMsdynEntityrefreshhistoryCreatedonbehalfby = "lk_msdyn_entityrefreshhistory_createdonbehalfby";
				public const string LkMsdynEntityrefreshhistoryModifiedby = "lk_msdyn_entityrefreshhistory_modifiedby";
				public const string LkMsdynEntityrefreshhistoryModifiedonbehalfby = "lk_msdyn_entityrefreshhistory_modifiedonbehalfby";
				public const string LkMsdynEntityroutingconfigurationCreatedby = "lk_msdyn_entityroutingconfiguration_createdby";
				public const string LkMsdynEntityroutingconfigurationCreatedonbehalfby = "lk_msdyn_entityroutingconfiguration_createdonbehalfby";
				public const string LkMsdynEntityroutingconfigurationModifiedby = "lk_msdyn_entityroutingconfiguration_modifiedby";
				public const string LkMsdynEntityroutingconfigurationModifiedonbehalfby = "lk_msdyn_entityroutingconfiguration_modifiedonbehalfby";
				public const string LkMsdynExtendedusersettingCreatedby = "lk_msdyn_extendedusersetting_createdby";
				public const string LkMsdynExtendedusersettingCreatedonbehalfby = "lk_msdyn_extendedusersetting_createdonbehalfby";
				public const string LkMsdynExtendedusersettingModifiedby = "lk_msdyn_extendedusersetting_modifiedby";
				public const string LkMsdynExtendedusersettingModifiedonbehalfby = "lk_msdyn_extendedusersetting_modifiedonbehalfby";
				public const string LkMsdynFederatedarticleCreatedby = "lk_msdyn_federatedarticle_createdby";
				public const string LkMsdynFederatedarticleCreatedonbehalfby = "lk_msdyn_federatedarticle_createdonbehalfby";
				public const string LkMsdynFederatedarticleModifiedby = "lk_msdyn_federatedarticle_modifiedby";
				public const string LkMsdynFederatedarticleModifiedonbehalfby = "lk_msdyn_federatedarticle_modifiedonbehalfby";
				public const string LkMsdynFederatedarticleincidentCreatedby = "lk_msdyn_federatedarticleincident_createdby";
				public const string LkMsdynFederatedarticleincidentCreatedonbehalfby = "lk_msdyn_federatedarticleincident_createdonbehalfby";
				public const string LkMsdynFederatedarticleincidentModifiedby = "lk_msdyn_federatedarticleincident_modifiedby";
				public const string LkMsdynFederatedarticleincidentModifiedonbehalfby = "lk_msdyn_federatedarticleincident_modifiedonbehalfby";
				public const string LkMsdynFileuploadstatustrackerCreatedby = "lk_msdyn_fileuploadstatustracker_createdby";
				public const string LkMsdynFileuploadstatustrackerCreatedonbehalfby = "lk_msdyn_fileuploadstatustracker_createdonbehalfby";
				public const string LkMsdynFileuploadstatustrackerModifiedby = "lk_msdyn_fileuploadstatustracker_modifiedby";
				public const string LkMsdynFileuploadstatustrackerModifiedonbehalfby = "lk_msdyn_fileuploadstatustracker_modifiedonbehalfby";
				public const string LkMsdynFlowcardtypeCreatedby = "lk_msdyn_flowcardtype_createdby";
				public const string LkMsdynFlowcardtypeCreatedonbehalfby = "lk_msdyn_flowcardtype_createdonbehalfby";
				public const string LkMsdynFlowcardtypeModifiedby = "lk_msdyn_flowcardtype_modifiedby";
				public const string LkMsdynFlowcardtypeModifiedonbehalfby = "lk_msdyn_flowcardtype_modifiedonbehalfby";
				public const string LkMsdynForecastCreatedby = "lk_msdyn_forecast_createdby";
				public const string LkMsdynForecastCreatedonbehalfby = "lk_msdyn_forecast_createdonbehalfby";
				public const string LkMsdynForecastModifiedby = "lk_msdyn_forecast_modifiedby";
				public const string LkMsdynForecastModifiedonbehalfby = "lk_msdyn_forecast_modifiedonbehalfby";
				public const string LkMsdynForecastconfigurationCreatedby = "lk_msdyn_forecastconfiguration_createdby";
				public const string LkMsdynForecastconfigurationCreatedonbehalfby = "lk_msdyn_forecastconfiguration_createdonbehalfby";
				public const string LkMsdynForecastconfigurationModifiedby = "lk_msdyn_forecastconfiguration_modifiedby";
				public const string LkMsdynForecastconfigurationModifiedonbehalfby = "lk_msdyn_forecastconfiguration_modifiedonbehalfby";
				public const string LkMsdynForecastdefinitionCreatedby = "lk_msdyn_forecastdefinition_createdby";
				public const string LkMsdynForecastdefinitionCreatedonbehalfby = "lk_msdyn_forecastdefinition_createdonbehalfby";
				public const string LkMsdynForecastdefinitionModifiedby = "lk_msdyn_forecastdefinition_modifiedby";
				public const string LkMsdynForecastdefinitionModifiedonbehalfby = "lk_msdyn_forecastdefinition_modifiedonbehalfby";
				public const string LkMsdynForecastingcacheCreatedby = "lk_msdyn_forecastingcache_createdby";
				public const string LkMsdynForecastingcacheCreatedonbehalfby = "lk_msdyn_forecastingcache_createdonbehalfby";
				public const string LkMsdynForecastingcacheModifiedby = "lk_msdyn_forecastingcache_modifiedby";
				public const string LkMsdynForecastingcacheModifiedonbehalfby = "lk_msdyn_forecastingcache_modifiedonbehalfby";
				public const string LkMsdynForecastinsightCreatedby = "lk_msdyn_forecastinsight_createdby";
				public const string LkMsdynForecastinsightCreatedonbehalfby = "lk_msdyn_forecastinsight_createdonbehalfby";
				public const string LkMsdynForecastinsightModifiedby = "lk_msdyn_forecastinsight_modifiedby";
				public const string LkMsdynForecastinsightModifiedonbehalfby = "lk_msdyn_forecastinsight_modifiedonbehalfby";
				public const string LkMsdynForecastinstanceCreatedby = "lk_msdyn_forecastinstance_createdby";
				public const string LkMsdynForecastinstanceCreatedonbehalfby = "lk_msdyn_forecastinstance_createdonbehalfby";
				public const string LkMsdynForecastinstanceModifiedby = "lk_msdyn_forecastinstance_modifiedby";
				public const string LkMsdynForecastinstanceModifiedonbehalfby = "lk_msdyn_forecastinstance_modifiedonbehalfby";
				public const string LkMsdynForecastpredictiondataCreatedby = "lk_msdyn_forecastpredictiondata_createdby";
				public const string LkMsdynForecastpredictiondataCreatedonbehalfby = "lk_msdyn_forecastpredictiondata_createdonbehalfby";
				public const string LkMsdynForecastpredictiondataModifiedby = "lk_msdyn_forecastpredictiondata_modifiedby";
				public const string LkMsdynForecastpredictiondataModifiedonbehalfby = "lk_msdyn_forecastpredictiondata_modifiedonbehalfby";
				public const string LkMsdynForecastpredictionstatusCreatedby = "lk_msdyn_forecastpredictionstatus_createdby";
				public const string LkMsdynForecastpredictionstatusCreatedonbehalfby = "lk_msdyn_forecastpredictionstatus_createdonbehalfby";
				public const string LkMsdynForecastpredictionstatusModifiedby = "lk_msdyn_forecastpredictionstatus_modifiedby";
				public const string LkMsdynForecastpredictionstatusModifiedonbehalfby = "lk_msdyn_forecastpredictionstatus_modifiedonbehalfby";
				public const string LkMsdynForecastrecurrenceCreatedby = "lk_msdyn_forecastrecurrence_createdby";
				public const string LkMsdynForecastrecurrenceCreatedonbehalfby = "lk_msdyn_forecastrecurrence_createdonbehalfby";
				public const string LkMsdynForecastrecurrenceModifiedby = "lk_msdyn_forecastrecurrence_modifiedby";
				public const string LkMsdynForecastrecurrenceModifiedonbehalfby = "lk_msdyn_forecastrecurrence_modifiedonbehalfby";
				public const string LkMsdynForecastsettingsandsummaryCreatedby = "lk_msdyn_forecastsettingsandsummary_createdby";
				public const string LkMsdynForecastsettingsandsummaryCreatedonbehalfby = "lk_msdyn_forecastsettingsandsummary_createdonbehalfby";
				public const string LkMsdynForecastsettingsandsummaryModifiedby = "lk_msdyn_forecastsettingsandsummary_modifiedby";
				public const string LkMsdynForecastsettingsandsummaryModifiedonbehalfby = "lk_msdyn_forecastsettingsandsummary_modifiedonbehalfby";
				public const string LkMsdynFunctionallocationCreatedby = "lk_msdyn_functionallocation_createdby";
				public const string LkMsdynFunctionallocationCreatedonbehalfby = "lk_msdyn_functionallocation_createdonbehalfby";
				public const string LkMsdynFunctionallocationModifiedby = "lk_msdyn_functionallocation_modifiedby";
				public const string LkMsdynFunctionallocationModifiedonbehalfby = "lk_msdyn_functionallocation_modifiedonbehalfby";
				public const string LkMsdynGdprdataCreatedby = "lk_msdyn_gdprdata_createdby";
				public const string LkMsdynGdprdataCreatedonbehalfby = "lk_msdyn_gdprdata_createdonbehalfby";
				public const string LkMsdynGdprdataModifiedby = "lk_msdyn_gdprdata_modifiedby";
				public const string LkMsdynGdprdataModifiedonbehalfby = "lk_msdyn_gdprdata_modifiedonbehalfby";
				public const string LkMsdynHelppageCreatedby = "lk_msdyn_helppage_createdby";
				public const string LkMsdynHelppageCreatedonbehalfby = "lk_msdyn_helppage_createdonbehalfby";
				public const string LkMsdynHelppageModifiedby = "lk_msdyn_helppage_modifiedby";
				public const string LkMsdynHelppageModifiedonbehalfby = "lk_msdyn_helppage_modifiedonbehalfby";
				public const string LkMsdynIcebreakersconfigCreatedby = "lk_msdyn_icebreakersconfig_createdby";
				public const string LkMsdynIcebreakersconfigCreatedonbehalfby = "lk_msdyn_icebreakersconfig_createdonbehalfby";
				public const string LkMsdynIcebreakersconfigModifiedby = "lk_msdyn_icebreakersconfig_modifiedby";
				public const string LkMsdynIcebreakersconfigModifiedonbehalfby = "lk_msdyn_icebreakersconfig_modifiedonbehalfby";
				public const string LkMsdynIermlmodelCreatedby = "lk_msdyn_iermlmodel_createdby";
				public const string LkMsdynIermlmodelCreatedonbehalfby = "lk_msdyn_iermlmodel_createdonbehalfby";
				public const string LkMsdynIermlmodelModifiedby = "lk_msdyn_iermlmodel_modifiedby";
				public const string LkMsdynIermlmodelModifiedonbehalfby = "lk_msdyn_iermlmodel_modifiedonbehalfby";
				public const string LkMsdynIermltrainingCreatedby = "lk_msdyn_iermltraining_createdby";
				public const string LkMsdynIermltrainingCreatedonbehalfby = "lk_msdyn_iermltraining_createdonbehalfby";
				public const string LkMsdynIermltrainingModifiedby = "lk_msdyn_iermltraining_modifiedby";
				public const string LkMsdynIermltrainingModifiedonbehalfby = "lk_msdyn_iermltraining_modifiedonbehalfby";
				public const string LkMsdynInboxconfigurationCreatedby = "lk_msdyn_inboxconfiguration_createdby";
				public const string LkMsdynInboxconfigurationCreatedonbehalfby = "lk_msdyn_inboxconfiguration_createdonbehalfby";
				public const string LkMsdynInboxconfigurationModifiedby = "lk_msdyn_inboxconfiguration_modifiedby";
				public const string LkMsdynInboxconfigurationModifiedonbehalfby = "lk_msdyn_inboxconfiguration_modifiedonbehalfby";
				public const string LkMsdynInsightsstorevirtualentityCreatedby = "lk_msdyn_insightsstorevirtualentity_createdby";
				public const string LkMsdynInsightsstorevirtualentityCreatedonbehalfby = "lk_msdyn_insightsstorevirtualentity_createdonbehalfby";
				public const string LkMsdynInsightsstorevirtualentityModifiedby = "lk_msdyn_insightsstorevirtualentity_modifiedby";
				public const string LkMsdynInsightsstorevirtualentityModifiedonbehalfby = "lk_msdyn_insightsstorevirtualentity_modifiedonbehalfby";
				public const string LkMsdynIntegratedsearchproviderCreatedby = "lk_msdyn_integratedsearchprovider_createdby";
				public const string LkMsdynIntegratedsearchproviderCreatedonbehalfby = "lk_msdyn_integratedsearchprovider_createdonbehalfby";
				public const string LkMsdynIntegratedsearchproviderModifiedby = "lk_msdyn_integratedsearchprovider_modifiedby";
				public const string LkMsdynIntegratedsearchproviderModifiedonbehalfby = "lk_msdyn_integratedsearchprovider_modifiedonbehalfby";
				public const string LkMsdynIotalertCreatedby = "lk_msdyn_iotalert_createdby";
				public const string LkMsdynIotalertCreatedonbehalfby = "lk_msdyn_iotalert_createdonbehalfby";
				public const string LkMsdynIotalertModifiedby = "lk_msdyn_iotalert_modifiedby";
				public const string LkMsdynIotalertModifiedonbehalfby = "lk_msdyn_iotalert_modifiedonbehalfby";
				public const string LkMsdynIotdeviceCreatedby = "lk_msdyn_iotdevice_createdby";
				public const string LkMsdynIotdeviceCreatedonbehalfby = "lk_msdyn_iotdevice_createdonbehalfby";
				public const string LkMsdynIotdeviceModifiedby = "lk_msdyn_iotdevice_modifiedby";
				public const string LkMsdynIotdeviceModifiedonbehalfby = "lk_msdyn_iotdevice_modifiedonbehalfby";
				public const string LkMsdynIotdevicecategoryCreatedby = "lk_msdyn_iotdevicecategory_createdby";
				public const string LkMsdynIotdevicecategoryCreatedonbehalfby = "lk_msdyn_iotdevicecategory_createdonbehalfby";
				public const string LkMsdynIotdevicecategoryModifiedby = "lk_msdyn_iotdevicecategory_modifiedby";
				public const string LkMsdynIotdevicecategoryModifiedonbehalfby = "lk_msdyn_iotdevicecategory_modifiedonbehalfby";
				public const string LkMsdynIotdevicecommandCreatedby = "lk_msdyn_iotdevicecommand_createdby";
				public const string LkMsdynIotdevicecommandCreatedonbehalfby = "lk_msdyn_iotdevicecommand_createdonbehalfby";
				public const string LkMsdynIotdevicecommandModifiedby = "lk_msdyn_iotdevicecommand_modifiedby";
				public const string LkMsdynIotdevicecommandModifiedonbehalfby = "lk_msdyn_iotdevicecommand_modifiedonbehalfby";
				public const string LkMsdynIotdevicecommanddefinitionCreatedby = "lk_msdyn_iotdevicecommanddefinition_createdby";
				public const string LkMsdynIotdevicecommanddefinitionCreatedonbehalfby = "lk_msdyn_iotdevicecommanddefinition_createdonbehalfby";
				public const string LkMsdynIotdevicecommanddefinitionModifiedby = "lk_msdyn_iotdevicecommanddefinition_modifiedby";
				public const string LkMsdynIotdevicecommanddefinitionModifiedonbehalfby = "lk_msdyn_iotdevicecommanddefinition_modifiedonbehalfby";
				public const string LkMsdynIotdevicedatahistoryCreatedby = "lk_msdyn_iotdevicedatahistory_createdby";
				public const string LkMsdynIotdevicedatahistoryCreatedonbehalfby = "lk_msdyn_iotdevicedatahistory_createdonbehalfby";
				public const string LkMsdynIotdevicedatahistoryModifiedby = "lk_msdyn_iotdevicedatahistory_modifiedby";
				public const string LkMsdynIotdevicedatahistoryModifiedonbehalfby = "lk_msdyn_iotdevicedatahistory_modifiedonbehalfby";
				public const string LkMsdynIotdevicepropertyCreatedby = "lk_msdyn_iotdeviceproperty_createdby";
				public const string LkMsdynIotdevicepropertyCreatedonbehalfby = "lk_msdyn_iotdeviceproperty_createdonbehalfby";
				public const string LkMsdynIotdevicepropertyModifiedby = "lk_msdyn_iotdeviceproperty_modifiedby";
				public const string LkMsdynIotdevicepropertyModifiedonbehalfby = "lk_msdyn_iotdeviceproperty_modifiedonbehalfby";
				public const string LkMsdynIotdeviceregistrationhistoryCreatedby = "lk_msdyn_iotdeviceregistrationhistory_createdby";
				public const string LkMsdynIotdeviceregistrationhistoryCreatedonbehalfby = "lk_msdyn_iotdeviceregistrationhistory_createdonbehalfby";
				public const string LkMsdynIotdeviceregistrationhistoryModifiedby = "lk_msdyn_iotdeviceregistrationhistory_modifiedby";
				public const string LkMsdynIotdeviceregistrationhistoryModifiedonbehalfby = "lk_msdyn_iotdeviceregistrationhistory_modifiedonbehalfby";
				public const string LkMsdynIotdevicevisualizationconfigurationCreatedby = "lk_msdyn_iotdevicevisualizationconfiguration_createdby";
				public const string LkMsdynIotdevicevisualizationconfigurationCreatedonbehalfby = "lk_msdyn_iotdevicevisualizationconfiguration_createdonbehalfby";
				public const string LkMsdynIotdevicevisualizationconfigurationModifiedby = "lk_msdyn_iotdevicevisualizationconfiguration_modifiedby";
				public const string LkMsdynIotdevicevisualizationconfigurationModifiedonbehalfby = "lk_msdyn_iotdevicevisualizationconfiguration_modifiedonbehalfby";
				public const string LkMsdynIotfieldmappingCreatedby = "lk_msdyn_iotfieldmapping_createdby";
				public const string LkMsdynIotfieldmappingCreatedonbehalfby = "lk_msdyn_iotfieldmapping_createdonbehalfby";
				public const string LkMsdynIotfieldmappingModifiedby = "lk_msdyn_iotfieldmapping_modifiedby";
				public const string LkMsdynIotfieldmappingModifiedonbehalfby = "lk_msdyn_iotfieldmapping_modifiedonbehalfby";
				public const string LkMsdynIotpropertydefinitionCreatedby = "lk_msdyn_iotpropertydefinition_createdby";
				public const string LkMsdynIotpropertydefinitionCreatedonbehalfby = "lk_msdyn_iotpropertydefinition_createdonbehalfby";
				public const string LkMsdynIotpropertydefinitionModifiedby = "lk_msdyn_iotpropertydefinition_modifiedby";
				public const string LkMsdynIotpropertydefinitionModifiedonbehalfby = "lk_msdyn_iotpropertydefinition_modifiedonbehalfby";
				public const string LkMsdynIotproviderCreatedby = "lk_msdyn_iotprovider_createdby";
				public const string LkMsdynIotproviderCreatedonbehalfby = "lk_msdyn_iotprovider_createdonbehalfby";
				public const string LkMsdynIotproviderModifiedby = "lk_msdyn_iotprovider_modifiedby";
				public const string LkMsdynIotproviderModifiedonbehalfby = "lk_msdyn_iotprovider_modifiedonbehalfby";
				public const string LkMsdynIotproviderinstanceCreatedby = "lk_msdyn_iotproviderinstance_createdby";
				public const string LkMsdynIotproviderinstanceCreatedonbehalfby = "lk_msdyn_iotproviderinstance_createdonbehalfby";
				public const string LkMsdynIotproviderinstanceModifiedby = "lk_msdyn_iotproviderinstance_modifiedby";
				public const string LkMsdynIotproviderinstanceModifiedonbehalfby = "lk_msdyn_iotproviderinstance_modifiedonbehalfby";
				public const string LkMsdynIotsettingsCreatedby = "lk_msdyn_iotsettings_createdby";
				public const string LkMsdynIotsettingsCreatedonbehalfby = "lk_msdyn_iotsettings_createdonbehalfby";
				public const string LkMsdynIotsettingsModifiedby = "lk_msdyn_iotsettings_modifiedby";
				public const string LkMsdynIotsettingsModifiedonbehalfby = "lk_msdyn_iotsettings_modifiedonbehalfby";
				public const string LkMsdynIottocaseprocessCreatedby = "lk_msdyn_iottocaseprocess_createdby";
				public const string LkMsdynIottocaseprocessCreatedonbehalfby = "lk_msdyn_iottocaseprocess_createdonbehalfby";
				public const string LkMsdynIottocaseprocessModifiedby = "lk_msdyn_iottocaseprocess_modifiedby";
				public const string LkMsdynIottocaseprocessModifiedonbehalfby = "lk_msdyn_iottocaseprocess_modifiedonbehalfby";
				public const string LkMsdynKalanguagesettingCreatedby = "lk_msdyn_kalanguagesetting_createdby";
				public const string LkMsdynKalanguagesettingCreatedonbehalfby = "lk_msdyn_kalanguagesetting_createdonbehalfby";
				public const string LkMsdynKalanguagesettingModifiedby = "lk_msdyn_kalanguagesetting_modifiedby";
				public const string LkMsdynKalanguagesettingModifiedonbehalfby = "lk_msdyn_kalanguagesetting_modifiedonbehalfby";
				public const string LkMsdynKbattachmentCreatedby = "lk_msdyn_kbattachment_createdby";
				public const string LkMsdynKbattachmentCreatedonbehalfby = "lk_msdyn_kbattachment_createdonbehalfby";
				public const string LkMsdynKbattachmentModifiedby = "lk_msdyn_kbattachment_modifiedby";
				public const string LkMsdynKbattachmentModifiedonbehalfby = "lk_msdyn_kbattachment_modifiedonbehalfby";
				public const string LkMsdynKbenrichmentCreatedby = "lk_msdyn_kbenrichment_createdby";
				public const string LkMsdynKbenrichmentCreatedonbehalfby = "lk_msdyn_kbenrichment_createdonbehalfby";
				public const string LkMsdynKbenrichmentModifiedby = "lk_msdyn_kbenrichment_modifiedby";
				public const string LkMsdynKbenrichmentModifiedonbehalfby = "lk_msdyn_kbenrichment_modifiedonbehalfby";
				public const string LkMsdynKbkeywordsdescsuggestionsettingCreatedby = "lk_msdyn_kbkeywordsdescsuggestionsetting_createdby";
				public const string LkMsdynKbkeywordsdescsuggestionsettingCreatedonbehalfby = "lk_msdyn_kbkeywordsdescsuggestionsetting_createdonbehalfby";
				public const string LkMsdynKbkeywordsdescsuggestionsettingModifiedby = "lk_msdyn_kbkeywordsdescsuggestionsetting_modifiedby";
				public const string LkMsdynKbkeywordsdescsuggestionsettingModifiedonbehalfby = "lk_msdyn_kbkeywordsdescsuggestionsetting_modifiedonbehalfby";
				public const string LkMsdynKmfederatedsearchconfigCreatedby = "lk_msdyn_kmfederatedsearchconfig_createdby";
				public const string LkMsdynKmfederatedsearchconfigCreatedonbehalfby = "lk_msdyn_kmfederatedsearchconfig_createdonbehalfby";
				public const string LkMsdynKmfederatedsearchconfigModifiedby = "lk_msdyn_kmfederatedsearchconfig_modifiedby";
				public const string LkMsdynKmfederatedsearchconfigModifiedonbehalfby = "lk_msdyn_kmfederatedsearchconfig_modifiedonbehalfby";
				public const string LkMsdynKmpersonalizationsettingCreatedby = "lk_msdyn_kmpersonalizationsetting_createdby";
				public const string LkMsdynKmpersonalizationsettingCreatedonbehalfby = "lk_msdyn_kmpersonalizationsetting_createdonbehalfby";
				public const string LkMsdynKmpersonalizationsettingModifiedby = "lk_msdyn_kmpersonalizationsetting_modifiedby";
				public const string LkMsdynKmpersonalizationsettingModifiedonbehalfby = "lk_msdyn_kmpersonalizationsetting_modifiedonbehalfby";
				public const string LkMsdynKnowledgearticleimageCreatedby = "lk_msdyn_knowledgearticleimage_createdby";
				public const string LkMsdynKnowledgearticleimageCreatedonbehalfby = "lk_msdyn_knowledgearticleimage_createdonbehalfby";
				public const string LkMsdynKnowledgearticleimageModifiedby = "lk_msdyn_knowledgearticleimage_modifiedby";
				public const string LkMsdynKnowledgearticleimageModifiedonbehalfby = "lk_msdyn_knowledgearticleimage_modifiedonbehalfby";
				public const string LkMsdynKnowledgearticletemplateCreatedby = "lk_msdyn_knowledgearticletemplate_createdby";
				public const string LkMsdynKnowledgearticletemplateCreatedonbehalfby = "lk_msdyn_knowledgearticletemplate_createdonbehalfby";
				public const string LkMsdynKnowledgearticletemplateModifiedby = "lk_msdyn_knowledgearticletemplate_modifiedby";
				public const string LkMsdynKnowledgearticletemplateModifiedonbehalfby = "lk_msdyn_knowledgearticletemplate_modifiedonbehalfby";
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
				public const string LkMsdynKpieventdataCreatedby = "lk_msdyn_kpieventdata_createdby";
				public const string LkMsdynKpieventdataCreatedonbehalfby = "lk_msdyn_kpieventdata_createdonbehalfby";
				public const string LkMsdynKpieventdataModifiedby = "lk_msdyn_kpieventdata_modifiedby";
				public const string LkMsdynKpieventdataModifiedonbehalfby = "lk_msdyn_kpieventdata_modifiedonbehalfby";
				public const string LkMsdynKpieventdefinitionCreatedby = "lk_msdyn_kpieventdefinition_createdby";
				public const string LkMsdynKpieventdefinitionCreatedonbehalfby = "lk_msdyn_kpieventdefinition_createdonbehalfby";
				public const string LkMsdynKpieventdefinitionModifiedby = "lk_msdyn_kpieventdefinition_modifiedby";
				public const string LkMsdynKpieventdefinitionModifiedonbehalfby = "lk_msdyn_kpieventdefinition_modifiedonbehalfby";
				public const string LkMsdynLeadhygienesettingCreatedby = "lk_msdyn_leadhygienesetting_createdby";
				public const string LkMsdynLeadhygienesettingCreatedonbehalfby = "lk_msdyn_leadhygienesetting_createdonbehalfby";
				public const string LkMsdynLeadhygienesettingModifiedby = "lk_msdyn_leadhygienesetting_modifiedby";
				public const string LkMsdynLeadhygienesettingModifiedonbehalfby = "lk_msdyn_leadhygienesetting_modifiedonbehalfby";
				public const string LkMsdynLeadmodelconfigCreatedby = "lk_msdyn_leadmodelconfig_createdby";
				public const string LkMsdynLeadmodelconfigCreatedonbehalfby = "lk_msdyn_leadmodelconfig_createdonbehalfby";
				public const string LkMsdynLeadmodelconfigModifiedby = "lk_msdyn_leadmodelconfig_modifiedby";
				public const string LkMsdynLeadmodelconfigModifiedonbehalfby = "lk_msdyn_leadmodelconfig_modifiedonbehalfby";
				public const string LkMsdynLinkedentityattributevalidityCreatedby = "lk_msdyn_linkedentityattributevalidity_createdby";
				public const string LkMsdynLinkedentityattributevalidityCreatedonbehalfby = "lk_msdyn_linkedentityattributevalidity_createdonbehalfby";
				public const string LkMsdynLinkedentityattributevalidityModifiedby = "lk_msdyn_linkedentityattributevalidity_modifiedby";
				public const string LkMsdynLinkedentityattributevalidityModifiedonbehalfby = "lk_msdyn_linkedentityattributevalidity_modifiedonbehalfby";
				public const string LkMsdynLiveconversationCreatedby = "lk_msdyn_liveconversation_createdby";
				public const string LkMsdynLiveconversationCreatedonbehalfby = "lk_msdyn_liveconversation_createdonbehalfby";
				public const string LkMsdynLiveconversationModifiedby = "lk_msdyn_liveconversation_modifiedby";
				public const string LkMsdynLiveconversationModifiedonbehalfby = "lk_msdyn_liveconversation_modifiedonbehalfby";
				public const string LkMsdynLiveworkitemeventCreatedby = "lk_msdyn_liveworkitemevent_createdby";
				public const string LkMsdynLiveworkitemeventCreatedonbehalfby = "lk_msdyn_liveworkitemevent_createdonbehalfby";
				public const string LkMsdynLiveworkitemeventModifiedby = "lk_msdyn_liveworkitemevent_modifiedby";
				public const string LkMsdynLiveworkitemeventModifiedonbehalfby = "lk_msdyn_liveworkitemevent_modifiedonbehalfby";
				public const string LkMsdynLiveworkstreamCreatedby = "lk_msdyn_liveworkstream_createdby";
				public const string LkMsdynLiveworkstreamCreatedonbehalfby = "lk_msdyn_liveworkstream_createdonbehalfby";
				public const string LkMsdynLiveworkstreamModifiedby = "lk_msdyn_liveworkstream_modifiedby";
				public const string LkMsdynLiveworkstreamModifiedonbehalfby = "lk_msdyn_liveworkstream_modifiedonbehalfby";
				public const string LkMsdynLiveworkstreamcapacityprofileCreatedby = "lk_msdyn_liveworkstreamcapacityprofile_createdby";
				public const string LkMsdynLiveworkstreamcapacityprofileCreatedonbehalfby = "lk_msdyn_liveworkstreamcapacityprofile_createdonbehalfby";
				public const string LkMsdynLiveworkstreamcapacityprofileModifiedby = "lk_msdyn_liveworkstreamcapacityprofile_modifiedby";
				public const string LkMsdynLiveworkstreamcapacityprofileModifiedonbehalfby = "lk_msdyn_liveworkstreamcapacityprofile_modifiedonbehalfby";
				public const string LkMsdynMacrosessionCreatedby = "lk_msdyn_macrosession_createdby";
				public const string LkMsdynMacrosessionCreatedonbehalfby = "lk_msdyn_macrosession_createdonbehalfby";
				public const string LkMsdynMacrosessionModifiedby = "lk_msdyn_macrosession_modifiedby";
				public const string LkMsdynMacrosessionModifiedonbehalfby = "lk_msdyn_macrosession_modifiedonbehalfby";
				public const string LkMsdynMaskingruleCreatedby = "lk_msdyn_maskingrule_createdby";
				public const string LkMsdynMaskingruleCreatedonbehalfby = "lk_msdyn_maskingrule_createdonbehalfby";
				public const string LkMsdynMaskingruleModifiedby = "lk_msdyn_maskingrule_modifiedby";
				public const string LkMsdynMaskingruleModifiedonbehalfby = "lk_msdyn_maskingrule_modifiedonbehalfby";
				public const string LkMsdynMasterentityroutingconfigurationCreatedby = "lk_msdyn_masterentityroutingconfiguration_createdby";
				public const string LkMsdynMasterentityroutingconfigurationCreatedonbehalfby = "lk_msdyn_masterentityroutingconfiguration_createdonbehalfby";
				public const string LkMsdynMasterentityroutingconfigurationModifiedby = "lk_msdyn_masterentityroutingconfiguration_modifiedby";
				public const string LkMsdynMasterentityroutingconfigurationModifiedonbehalfby = "lk_msdyn_masterentityroutingconfiguration_modifiedonbehalfby";
				public const string LkMsdynMigrationtrackerCreatedby = "lk_msdyn_migrationtracker_createdby";
				public const string LkMsdynMigrationtrackerCreatedonbehalfby = "lk_msdyn_migrationtracker_createdonbehalfby";
				public const string LkMsdynMigrationtrackerModifiedby = "lk_msdyn_migrationtracker_modifiedby";
				public const string LkMsdynMigrationtrackerModifiedonbehalfby = "lk_msdyn_migrationtracker_modifiedonbehalfby";
				public const string LkMsdynModelpreviewstatusCreatedby = "lk_msdyn_modelpreviewstatus_createdby";
				public const string LkMsdynModelpreviewstatusCreatedonbehalfby = "lk_msdyn_modelpreviewstatus_createdonbehalfby";
				public const string LkMsdynModelpreviewstatusModifiedby = "lk_msdyn_modelpreviewstatus_modifiedby";
				public const string LkMsdynModelpreviewstatusModifiedonbehalfby = "lk_msdyn_modelpreviewstatus_modifiedonbehalfby";
				public const string LkMsdynMsteamssettingCreatedby = "lk_msdyn_msteamssetting_createdby";
				public const string LkMsdynMsteamssettingCreatedonbehalfby = "lk_msdyn_msteamssetting_createdonbehalfby";
				public const string LkMsdynMsteamssettingModifiedby = "lk_msdyn_msteamssetting_modifiedby";
				public const string LkMsdynMsteamssettingModifiedonbehalfby = "lk_msdyn_msteamssetting_modifiedonbehalfby";
				public const string LkMsdynMsteamssettingsv2Createdby = "lk_msdyn_msteamssettingsv2_createdby";
				public const string LkMsdynMsteamssettingsv2Createdonbehalfby = "lk_msdyn_msteamssettingsv2_createdonbehalfby";
				public const string LkMsdynMsteamssettingsv2Modifiedby = "lk_msdyn_msteamssettingsv2_modifiedby";
				public const string LkMsdynMsteamssettingsv2Modifiedonbehalfby = "lk_msdyn_msteamssettingsv2_modifiedonbehalfby";
				public const string LkMsdynNotesanalysisconfigCreatedby = "lk_msdyn_notesanalysisconfig_createdby";
				public const string LkMsdynNotesanalysisconfigCreatedonbehalfby = "lk_msdyn_notesanalysisconfig_createdonbehalfby";
				public const string LkMsdynNotesanalysisconfigModifiedby = "lk_msdyn_notesanalysisconfig_modifiedby";
				public const string LkMsdynNotesanalysisconfigModifiedonbehalfby = "lk_msdyn_notesanalysisconfig_modifiedonbehalfby";
				public const string LkMsdynNotificationfieldCreatedby = "lk_msdyn_notificationfield_createdby";
				public const string LkMsdynNotificationfieldCreatedonbehalfby = "lk_msdyn_notificationfield_createdonbehalfby";
				public const string LkMsdynNotificationfieldModifiedby = "lk_msdyn_notificationfield_modifiedby";
				public const string LkMsdynNotificationfieldModifiedonbehalfby = "lk_msdyn_notificationfield_modifiedonbehalfby";
				public const string LkMsdynNotificationtemplateCreatedby = "lk_msdyn_notificationtemplate_createdby";
				public const string LkMsdynNotificationtemplateCreatedonbehalfby = "lk_msdyn_notificationtemplate_createdonbehalfby";
				public const string LkMsdynNotificationtemplateModifiedby = "lk_msdyn_notificationtemplate_modifiedby";
				public const string LkMsdynNotificationtemplateModifiedonbehalfby = "lk_msdyn_notificationtemplate_modifiedonbehalfby";
				public const string LkMsdynOcGeolocationproviderCreatedby = "lk_msdyn_oc_geolocationprovider_createdby";
				public const string LkMsdynOcGeolocationproviderCreatedonbehalfby = "lk_msdyn_oc_geolocationprovider_createdonbehalfby";
				public const string LkMsdynOcGeolocationproviderModifiedby = "lk_msdyn_oc_geolocationprovider_modifiedby";
				public const string LkMsdynOcGeolocationproviderModifiedonbehalfby = "lk_msdyn_oc_geolocationprovider_modifiedonbehalfby";
				public const string LkMsdynOcautoblockruleCreatedby = "lk_msdyn_ocautoblockrule_createdby";
				public const string LkMsdynOcautoblockruleCreatedonbehalfby = "lk_msdyn_ocautoblockrule_createdonbehalfby";
				public const string LkMsdynOcautoblockruleModifiedby = "lk_msdyn_ocautoblockrule_modifiedby";
				public const string LkMsdynOcautoblockruleModifiedonbehalfby = "lk_msdyn_ocautoblockrule_modifiedonbehalfby";
				public const string LkMsdynOcbotchannelregistrationCreatedby = "lk_msdyn_ocbotchannelregistration_createdby";
				public const string LkMsdynOcbotchannelregistrationCreatedonbehalfby = "lk_msdyn_ocbotchannelregistration_createdonbehalfby";
				public const string LkMsdynOcbotchannelregistrationModifiedby = "lk_msdyn_ocbotchannelregistration_modifiedby";
				public const string LkMsdynOcbotchannelregistrationModifiedonbehalfby = "lk_msdyn_ocbotchannelregistration_modifiedonbehalfby";
				public const string LkMsdynOcchannelapiconversationprivilegeCreatedby = "lk_msdyn_occhannelapiconversationprivilege_createdby";
				public const string LkMsdynOcchannelapiconversationprivilegeCreatedonbehalfby = "lk_msdyn_occhannelapiconversationprivilege_createdonbehalfby";
				public const string LkMsdynOcchannelapiconversationprivilegeModifiedby = "lk_msdyn_occhannelapiconversationprivilege_modifiedby";
				public const string LkMsdynOcchannelapiconversationprivilegeModifiedonbehalfby = "lk_msdyn_occhannelapiconversationprivilege_modifiedonbehalfby";
				public const string LkMsdynOcchannelapimessageprivilegeCreatedby = "lk_msdyn_occhannelapimessageprivilege_createdby";
				public const string LkMsdynOcchannelapimessageprivilegeCreatedonbehalfby = "lk_msdyn_occhannelapimessageprivilege_createdonbehalfby";
				public const string LkMsdynOcchannelapimessageprivilegeModifiedby = "lk_msdyn_occhannelapimessageprivilege_modifiedby";
				public const string LkMsdynOcchannelapimessageprivilegeModifiedonbehalfby = "lk_msdyn_occhannelapimessageprivilege_modifiedonbehalfby";
				public const string LkMsdynOcchannelapimethodmappingCreatedby = "lk_msdyn_occhannelapimethodmapping_createdby";
				public const string LkMsdynOcchannelapimethodmappingCreatedonbehalfby = "lk_msdyn_occhannelapimethodmapping_createdonbehalfby";
				public const string LkMsdynOcchannelapimethodmappingModifiedby = "lk_msdyn_occhannelapimethodmapping_modifiedby";
				public const string LkMsdynOcchannelapimethodmappingModifiedonbehalfby = "lk_msdyn_occhannelapimethodmapping_modifiedonbehalfby";
				public const string LkMsdynOcchannelconfigurationCreatedby = "lk_msdyn_occhannelconfiguration_createdby";
				public const string LkMsdynOcchannelconfigurationCreatedonbehalfby = "lk_msdyn_occhannelconfiguration_createdonbehalfby";
				public const string LkMsdynOcchannelconfigurationModifiedby = "lk_msdyn_occhannelconfiguration_modifiedby";
				public const string LkMsdynOcchannelconfigurationModifiedonbehalfby = "lk_msdyn_occhannelconfiguration_modifiedonbehalfby";
				public const string LkMsdynOcchannelstateconfigurationCreatedby = "lk_msdyn_occhannelstateconfiguration_createdby";
				public const string LkMsdynOcchannelstateconfigurationCreatedonbehalfby = "lk_msdyn_occhannelstateconfiguration_createdonbehalfby";
				public const string LkMsdynOcchannelstateconfigurationModifiedby = "lk_msdyn_occhannelstateconfiguration_modifiedby";
				public const string LkMsdynOcchannelstateconfigurationModifiedonbehalfby = "lk_msdyn_occhannelstateconfiguration_modifiedonbehalfby";
				public const string LkMsdynOcflaggedspamCreatedby = "lk_msdyn_ocflaggedspam_createdby";
				public const string LkMsdynOcflaggedspamCreatedonbehalfby = "lk_msdyn_ocflaggedspam_createdonbehalfby";
				public const string LkMsdynOcflaggedspamModifiedby = "lk_msdyn_ocflaggedspam_modifiedby";
				public const string LkMsdynOcflaggedspamModifiedonbehalfby = "lk_msdyn_ocflaggedspam_modifiedonbehalfby";
				public const string LkMsdynOclanguageCreatedby = "lk_msdyn_oclanguage_createdby";
				public const string LkMsdynOclanguageCreatedonbehalfby = "lk_msdyn_oclanguage_createdonbehalfby";
				public const string LkMsdynOclanguageModifiedby = "lk_msdyn_oclanguage_modifiedby";
				public const string LkMsdynOclanguageModifiedonbehalfby = "lk_msdyn_oclanguage_modifiedonbehalfby";
				public const string LkMsdynOcliveworkitemcapacityprofileCreatedby = "lk_msdyn_ocliveworkitemcapacityprofile_createdby";
				public const string LkMsdynOcliveworkitemcapacityprofileCreatedonbehalfby = "lk_msdyn_ocliveworkitemcapacityprofile_createdonbehalfby";
				public const string LkMsdynOcliveworkitemcapacityprofileModifiedby = "lk_msdyn_ocliveworkitemcapacityprofile_modifiedby";
				public const string LkMsdynOcliveworkitemcapacityprofileModifiedonbehalfby = "lk_msdyn_ocliveworkitemcapacityprofile_modifiedonbehalfby";
				public const string LkMsdynOcliveworkitemcharacteristicCreatedby = "lk_msdyn_ocliveworkitemcharacteristic_createdby";
				public const string LkMsdynOcliveworkitemcharacteristicCreatedonbehalfby = "lk_msdyn_ocliveworkitemcharacteristic_createdonbehalfby";
				public const string LkMsdynOcliveworkitemcharacteristicModifiedby = "lk_msdyn_ocliveworkitemcharacteristic_modifiedby";
				public const string LkMsdynOcliveworkitemcharacteristicModifiedonbehalfby = "lk_msdyn_ocliveworkitemcharacteristic_modifiedonbehalfby";
				public const string LkMsdynOcliveworkitemcontextitemCreatedby = "lk_msdyn_ocliveworkitemcontextitem_createdby";
				public const string LkMsdynOcliveworkitemcontextitemCreatedonbehalfby = "lk_msdyn_ocliveworkitemcontextitem_createdonbehalfby";
				public const string LkMsdynOcliveworkitemcontextitemModifiedby = "lk_msdyn_ocliveworkitemcontextitem_modifiedby";
				public const string LkMsdynOcliveworkitemcontextitemModifiedonbehalfby = "lk_msdyn_ocliveworkitemcontextitem_modifiedonbehalfby";
				public const string LkMsdynOcliveworkitemparticipantCreatedby = "lk_msdyn_ocliveworkitemparticipant_createdby";
				public const string LkMsdynOcliveworkitemparticipantCreatedonbehalfby = "lk_msdyn_ocliveworkitemparticipant_createdonbehalfby";
				public const string LkMsdynOcliveworkitemparticipantModifiedby = "lk_msdyn_ocliveworkitemparticipant_modifiedby";
				public const string LkMsdynOcliveworkitemparticipantModifiedonbehalfby = "lk_msdyn_ocliveworkitemparticipant_modifiedonbehalfby";
				public const string LkMsdynOcliveworkitemsentimentCreatedby = "lk_msdyn_ocliveworkitemsentiment_createdby";
				public const string LkMsdynOcliveworkitemsentimentCreatedonbehalfby = "lk_msdyn_ocliveworkitemsentiment_createdonbehalfby";
				public const string LkMsdynOcliveworkitemsentimentModifiedby = "lk_msdyn_ocliveworkitemsentiment_modifiedby";
				public const string LkMsdynOcliveworkitemsentimentModifiedonbehalfby = "lk_msdyn_ocliveworkitemsentiment_modifiedonbehalfby";
				public const string LkMsdynOcliveworkstreamcontextvariableCreatedby = "lk_msdyn_ocliveworkstreamcontextvariable_createdby";
				public const string LkMsdynOcliveworkstreamcontextvariableCreatedonbehalfby = "lk_msdyn_ocliveworkstreamcontextvariable_createdonbehalfby";
				public const string LkMsdynOcliveworkstreamcontextvariableModifiedby = "lk_msdyn_ocliveworkstreamcontextvariable_modifiedby";
				public const string LkMsdynOcliveworkstreamcontextvariableModifiedonbehalfby = "lk_msdyn_ocliveworkstreamcontextvariable_modifiedonbehalfby";
				public const string LkMsdynOclocalizationdataCreatedby = "lk_msdyn_oclocalizationdata_createdby";
				public const string LkMsdynOclocalizationdataCreatedonbehalfby = "lk_msdyn_oclocalizationdata_createdonbehalfby";
				public const string LkMsdynOclocalizationdataModifiedby = "lk_msdyn_oclocalizationdata_modifiedby";
				public const string LkMsdynOclocalizationdataModifiedonbehalfby = "lk_msdyn_oclocalizationdata_modifiedonbehalfby";
				public const string LkMsdynOcpaymentprofileCreatedby = "lk_msdyn_ocpaymentprofile_createdby";
				public const string LkMsdynOcpaymentprofileCreatedonbehalfby = "lk_msdyn_ocpaymentprofile_createdonbehalfby";
				public const string LkMsdynOcpaymentprofileModifiedby = "lk_msdyn_ocpaymentprofile_modifiedby";
				public const string LkMsdynOcpaymentprofileModifiedonbehalfby = "lk_msdyn_ocpaymentprofile_modifiedonbehalfby";
				public const string LkMsdynOcprovisioningstateCreatedby = "lk_msdyn_ocprovisioningstate_createdby";
				public const string LkMsdynOcprovisioningstateCreatedonbehalfby = "lk_msdyn_ocprovisioningstate_createdonbehalfby";
				public const string LkMsdynOcprovisioningstateModifiedby = "lk_msdyn_ocprovisioningstate_modifiedby";
				public const string LkMsdynOcprovisioningstateModifiedonbehalfby = "lk_msdyn_ocprovisioningstate_modifiedonbehalfby";
				public const string LkMsdynOcrecordingCreatedby = "lk_msdyn_ocrecording_createdby";
				public const string LkMsdynOcrecordingCreatedonbehalfby = "lk_msdyn_ocrecording_createdonbehalfby";
				public const string LkMsdynOcrecordingModifiedby = "lk_msdyn_ocrecording_modifiedby";
				public const string LkMsdynOcrecordingModifiedonbehalfby = "lk_msdyn_ocrecording_modifiedonbehalfby";
				public const string LkMsdynOcrequestCreatedby = "lk_msdyn_ocrequest_createdby";
				public const string LkMsdynOcrequestCreatedonbehalfby = "lk_msdyn_ocrequest_createdonbehalfby";
				public const string LkMsdynOcrequestModifiedby = "lk_msdyn_ocrequest_modifiedby";
				public const string LkMsdynOcrequestModifiedonbehalfby = "lk_msdyn_ocrequest_modifiedonbehalfby";
				public const string LkMsdynOcrichobjectCreatedby = "lk_msdyn_ocrichobject_createdby";
				public const string LkMsdynOcrichobjectCreatedonbehalfby = "lk_msdyn_ocrichobject_createdonbehalfby";
				public const string LkMsdynOcrichobjectModifiedby = "lk_msdyn_ocrichobject_modifiedby";
				public const string LkMsdynOcrichobjectModifiedonbehalfby = "lk_msdyn_ocrichobject_modifiedonbehalfby";
				public const string LkMsdynOcrichobjectmapCreatedby = "lk_msdyn_ocrichobjectmap_createdby";
				public const string LkMsdynOcrichobjectmapCreatedonbehalfby = "lk_msdyn_ocrichobjectmap_createdonbehalfby";
				public const string LkMsdynOcrichobjectmapModifiedby = "lk_msdyn_ocrichobjectmap_modifiedby";
				public const string LkMsdynOcrichobjectmapModifiedonbehalfby = "lk_msdyn_ocrichobjectmap_modifiedonbehalfby";
				public const string LkMsdynOcruleitemCreatedby = "lk_msdyn_ocruleitem_createdby";
				public const string LkMsdynOcruleitemCreatedonbehalfby = "lk_msdyn_ocruleitem_createdonbehalfby";
				public const string LkMsdynOcruleitemModifiedby = "lk_msdyn_ocruleitem_modifiedby";
				public const string LkMsdynOcruleitemModifiedonbehalfby = "lk_msdyn_ocruleitem_modifiedonbehalfby";
				public const string LkMsdynOcsentimentdailytopicCreatedby = "lk_msdyn_ocsentimentdailytopic_createdby";
				public const string LkMsdynOcsentimentdailytopicCreatedonbehalfby = "lk_msdyn_ocsentimentdailytopic_createdonbehalfby";
				public const string LkMsdynOcsentimentdailytopicModifiedby = "lk_msdyn_ocsentimentdailytopic_modifiedby";
				public const string LkMsdynOcsentimentdailytopicModifiedonbehalfby = "lk_msdyn_ocsentimentdailytopic_modifiedonbehalfby";
				public const string LkMsdynOcsentimentdailytopickeywordCreatedby = "lk_msdyn_ocsentimentdailytopickeyword_createdby";
				public const string LkMsdynOcsentimentdailytopickeywordCreatedonbehalfby = "lk_msdyn_ocsentimentdailytopickeyword_createdonbehalfby";
				public const string LkMsdynOcsentimentdailytopickeywordModifiedby = "lk_msdyn_ocsentimentdailytopickeyword_modifiedby";
				public const string LkMsdynOcsentimentdailytopickeywordModifiedonbehalfby = "lk_msdyn_ocsentimentdailytopickeyword_modifiedonbehalfby";
				public const string LkMsdynOcsentimentdailytopictrendingCreatedby = "lk_msdyn_ocsentimentdailytopictrending_createdby";
				public const string LkMsdynOcsentimentdailytopictrendingCreatedonbehalfby = "lk_msdyn_ocsentimentdailytopictrending_createdonbehalfby";
				public const string LkMsdynOcsentimentdailytopictrendingModifiedby = "lk_msdyn_ocsentimentdailytopictrending_modifiedby";
				public const string LkMsdynOcsentimentdailytopictrendingModifiedonbehalfby = "lk_msdyn_ocsentimentdailytopictrending_modifiedonbehalfby";
				public const string LkMsdynOcsessioncharacteristicCreatedby = "lk_msdyn_ocsessioncharacteristic_createdby";
				public const string LkMsdynOcsessioncharacteristicCreatedonbehalfby = "lk_msdyn_ocsessioncharacteristic_createdonbehalfby";
				public const string LkMsdynOcsessioncharacteristicModifiedby = "lk_msdyn_ocsessioncharacteristic_modifiedby";
				public const string LkMsdynOcsessioncharacteristicModifiedonbehalfby = "lk_msdyn_ocsessioncharacteristic_modifiedonbehalfby";
				public const string LkMsdynOcsessionparticipanteventCreatedby = "lk_msdyn_ocsessionparticipantevent_createdby";
				public const string LkMsdynOcsessionparticipanteventCreatedonbehalfby = "lk_msdyn_ocsessionparticipantevent_createdonbehalfby";
				public const string LkMsdynOcsessionparticipanteventModifiedby = "lk_msdyn_ocsessionparticipantevent_modifiedby";
				public const string LkMsdynOcsessionparticipanteventModifiedonbehalfby = "lk_msdyn_ocsessionparticipantevent_modifiedonbehalfby";
				public const string LkMsdynOcsessionsentimentCreatedby = "lk_msdyn_ocsessionsentiment_createdby";
				public const string LkMsdynOcsessionsentimentCreatedonbehalfby = "lk_msdyn_ocsessionsentiment_createdonbehalfby";
				public const string LkMsdynOcsessionsentimentModifiedby = "lk_msdyn_ocsessionsentiment_modifiedby";
				public const string LkMsdynOcsessionsentimentModifiedonbehalfby = "lk_msdyn_ocsessionsentiment_modifiedonbehalfby";
				public const string LkMsdynOcsimltrainingCreatedby = "lk_msdyn_ocsimltraining_createdby";
				public const string LkMsdynOcsimltrainingCreatedonbehalfby = "lk_msdyn_ocsimltraining_createdonbehalfby";
				public const string LkMsdynOcsimltrainingModifiedby = "lk_msdyn_ocsimltraining_modifiedby";
				public const string LkMsdynOcsimltrainingModifiedonbehalfby = "lk_msdyn_ocsimltraining_modifiedonbehalfby";
				public const string LkMsdynOcsitdimportconfigCreatedby = "lk_msdyn_ocsitdimportconfig_createdby";
				public const string LkMsdynOcsitdimportconfigCreatedonbehalfby = "lk_msdyn_ocsitdimportconfig_createdonbehalfby";
				public const string LkMsdynOcsitdimportconfigModifiedby = "lk_msdyn_ocsitdimportconfig_modifiedby";
				public const string LkMsdynOcsitdimportconfigModifiedonbehalfby = "lk_msdyn_ocsitdimportconfig_modifiedonbehalfby";
				public const string LkMsdynOcsitdskillCreatedby = "lk_msdyn_ocsitdskill_createdby";
				public const string LkMsdynOcsitdskillCreatedonbehalfby = "lk_msdyn_ocsitdskill_createdonbehalfby";
				public const string LkMsdynOcsitdskillModifiedby = "lk_msdyn_ocsitdskill_modifiedby";
				public const string LkMsdynOcsitdskillModifiedonbehalfby = "lk_msdyn_ocsitdskill_modifiedonbehalfby";
				public const string LkMsdynOcsitrainingdataCreatedby = "lk_msdyn_ocsitrainingdata_createdby";
				public const string LkMsdynOcsitrainingdataCreatedonbehalfby = "lk_msdyn_ocsitrainingdata_createdonbehalfby";
				public const string LkMsdynOcsitrainingdataModifiedby = "lk_msdyn_ocsitrainingdata_modifiedby";
				public const string LkMsdynOcsitrainingdataModifiedonbehalfby = "lk_msdyn_ocsitrainingdata_modifiedonbehalfby";
				public const string LkMsdynOcskillidentmlmodelCreatedby = "lk_msdyn_ocskillidentmlmodel_createdby";
				public const string LkMsdynOcskillidentmlmodelCreatedonbehalfby = "lk_msdyn_ocskillidentmlmodel_createdonbehalfby";
				public const string LkMsdynOcskillidentmlmodelModifiedby = "lk_msdyn_ocskillidentmlmodel_modifiedby";
				public const string LkMsdynOcskillidentmlmodelModifiedonbehalfby = "lk_msdyn_ocskillidentmlmodel_modifiedonbehalfby";
				public const string LkMsdynOcsystemmessageCreatedby = "lk_msdyn_ocsystemmessage_createdby";
				public const string LkMsdynOcsystemmessageCreatedonbehalfby = "lk_msdyn_ocsystemmessage_createdonbehalfby";
				public const string LkMsdynOcsystemmessageModifiedby = "lk_msdyn_ocsystemmessage_modifiedby";
				public const string LkMsdynOcsystemmessageModifiedonbehalfby = "lk_msdyn_ocsystemmessage_modifiedonbehalfby";
				public const string LkMsdynOctagCreatedby = "lk_msdyn_octag_createdby";
				public const string LkMsdynOctagCreatedonbehalfby = "lk_msdyn_octag_createdonbehalfby";
				public const string LkMsdynOctagModifiedby = "lk_msdyn_octag_modifiedby";
				public const string LkMsdynOctagModifiedonbehalfby = "lk_msdyn_octag_modifiedonbehalfby";
				public const string LkMsdynOmnichannelconfigurationCreatedby = "lk_msdyn_omnichannelconfiguration_createdby";
				public const string LkMsdynOmnichannelconfigurationCreatedonbehalfby = "lk_msdyn_omnichannelconfiguration_createdonbehalfby";
				public const string LkMsdynOmnichannelconfigurationModifiedby = "lk_msdyn_omnichannelconfiguration_modifiedby";
				public const string LkMsdynOmnichannelconfigurationModifiedonbehalfby = "lk_msdyn_omnichannelconfiguration_modifiedonbehalfby";
				public const string LkMsdynOmnichannelpersonalizationCreatedby = "lk_msdyn_omnichannelpersonalization_createdby";
				public const string LkMsdynOmnichannelpersonalizationCreatedonbehalfby = "lk_msdyn_omnichannelpersonalization_createdonbehalfby";
				public const string LkMsdynOmnichannelpersonalizationModifiedby = "lk_msdyn_omnichannelpersonalization_modifiedby";
				public const string LkMsdynOmnichannelpersonalizationModifiedonbehalfby = "lk_msdyn_omnichannelpersonalization_modifiedonbehalfby";
				public const string LkMsdynOmnichannelqueueCreatedby = "lk_msdyn_omnichannelqueue_createdby";
				public const string LkMsdynOmnichannelqueueCreatedonbehalfby = "lk_msdyn_omnichannelqueue_createdonbehalfby";
				public const string LkMsdynOmnichannelqueueModifiedby = "lk_msdyn_omnichannelqueue_modifiedby";
				public const string LkMsdynOmnichannelqueueModifiedonbehalfby = "lk_msdyn_omnichannelqueue_modifiedonbehalfby";
				public const string LkMsdynOmnichannelsyncconfigCreatedby = "lk_msdyn_omnichannelsyncconfig_createdby";
				public const string LkMsdynOmnichannelsyncconfigCreatedonbehalfby = "lk_msdyn_omnichannelsyncconfig_createdonbehalfby";
				public const string LkMsdynOmnichannelsyncconfigModifiedby = "lk_msdyn_omnichannelsyncconfig_modifiedby";
				public const string LkMsdynOmnichannelsyncconfigModifiedonbehalfby = "lk_msdyn_omnichannelsyncconfig_modifiedonbehalfby";
				public const string LkMsdynOperatinghourCreatedby = "lk_msdyn_operatinghour_createdby";
				public const string LkMsdynOperatinghourCreatedonbehalfby = "lk_msdyn_operatinghour_createdonbehalfby";
				public const string LkMsdynOperatinghourModifiedby = "lk_msdyn_operatinghour_modifiedby";
				public const string LkMsdynOperatinghourModifiedonbehalfby = "lk_msdyn_operatinghour_modifiedonbehalfby";
				public const string LkMsdynOpportunitymodelconfigCreatedby = "lk_msdyn_opportunitymodelconfig_createdby";
				public const string LkMsdynOpportunitymodelconfigCreatedonbehalfby = "lk_msdyn_opportunitymodelconfig_createdonbehalfby";
				public const string LkMsdynOpportunitymodelconfigModifiedby = "lk_msdyn_opportunitymodelconfig_modifiedby";
				public const string LkMsdynOpportunitymodelconfigModifiedonbehalfby = "lk_msdyn_opportunitymodelconfig_modifiedonbehalfby";
				public const string LkMsdynOverflowactionconfigCreatedby = "lk_msdyn_overflowactionconfig_createdby";
				public const string LkMsdynOverflowactionconfigCreatedonbehalfby = "lk_msdyn_overflowactionconfig_createdonbehalfby";
				public const string LkMsdynOverflowactionconfigModifiedby = "lk_msdyn_overflowactionconfig_modifiedby";
				public const string LkMsdynOverflowactionconfigModifiedonbehalfby = "lk_msdyn_overflowactionconfig_modifiedonbehalfby";
				public const string LkMsdynPaneconfigurationCreatedby = "lk_msdyn_paneconfiguration_createdby";
				public const string LkMsdynPaneconfigurationCreatedonbehalfby = "lk_msdyn_paneconfiguration_createdonbehalfby";
				public const string LkMsdynPaneconfigurationModifiedby = "lk_msdyn_paneconfiguration_modifiedby";
				public const string LkMsdynPaneconfigurationModifiedonbehalfby = "lk_msdyn_paneconfiguration_modifiedonbehalfby";
				public const string LkMsdynPanetabconfigurationCreatedby = "lk_msdyn_panetabconfiguration_createdby";
				public const string LkMsdynPanetabconfigurationCreatedonbehalfby = "lk_msdyn_panetabconfiguration_createdonbehalfby";
				public const string LkMsdynPanetabconfigurationModifiedby = "lk_msdyn_panetabconfiguration_modifiedby";
				public const string LkMsdynPanetabconfigurationModifiedonbehalfby = "lk_msdyn_panetabconfiguration_modifiedonbehalfby";
				public const string LkMsdynPanetoolconfigurationCreatedby = "lk_msdyn_panetoolconfiguration_createdby";
				public const string LkMsdynPanetoolconfigurationCreatedonbehalfby = "lk_msdyn_panetoolconfiguration_createdonbehalfby";
				public const string LkMsdynPanetoolconfigurationModifiedby = "lk_msdyn_panetoolconfiguration_modifiedby";
				public const string LkMsdynPanetoolconfigurationModifiedonbehalfby = "lk_msdyn_panetoolconfiguration_modifiedonbehalfby";
				public const string LkMsdynPersonalmessageCreatedby = "lk_msdyn_personalmessage_createdby";
				public const string LkMsdynPersonalmessageCreatedonbehalfby = "lk_msdyn_personalmessage_createdonbehalfby";
				public const string LkMsdynPersonalmessageModifiedby = "lk_msdyn_personalmessage_modifiedby";
				public const string LkMsdynPersonalmessageModifiedonbehalfby = "lk_msdyn_personalmessage_modifiedonbehalfby";
				public const string LkMsdynPersonalsoundsettingCreatedby = "lk_msdyn_personalsoundsetting_createdby";
				public const string LkMsdynPersonalsoundsettingCreatedonbehalfby = "lk_msdyn_personalsoundsetting_createdonbehalfby";
				public const string LkMsdynPersonalsoundsettingModifiedby = "lk_msdyn_personalsoundsetting_modifiedby";
				public const string LkMsdynPersonalsoundsettingModifiedonbehalfby = "lk_msdyn_personalsoundsetting_modifiedonbehalfby";
				public const string LkMsdynPersonasecurityrolemappingCreatedby = "lk_msdyn_personasecurityrolemapping_createdby";
				public const string LkMsdynPersonasecurityrolemappingCreatedonbehalfby = "lk_msdyn_personasecurityrolemapping_createdonbehalfby";
				public const string LkMsdynPersonasecurityrolemappingModifiedby = "lk_msdyn_personasecurityrolemapping_modifiedby";
				public const string LkMsdynPersonasecurityrolemappingModifiedonbehalfby = "lk_msdyn_personasecurityrolemapping_modifiedonbehalfby";
				public const string LkMsdynPlaybookactivityCreatedby = "lk_msdyn_playbookactivity_createdby";
				public const string LkMsdynPlaybookactivityCreatedonbehalfby = "lk_msdyn_playbookactivity_createdonbehalfby";
				public const string LkMsdynPlaybookactivityModifiedby = "lk_msdyn_playbookactivity_modifiedby";
				public const string LkMsdynPlaybookactivityModifiedonbehalfby = "lk_msdyn_playbookactivity_modifiedonbehalfby";
				public const string LkMsdynPlaybookactivityattributeCreatedby = "lk_msdyn_playbookactivityattribute_createdby";
				public const string LkMsdynPlaybookactivityattributeCreatedonbehalfby = "lk_msdyn_playbookactivityattribute_createdonbehalfby";
				public const string LkMsdynPlaybookactivityattributeModifiedby = "lk_msdyn_playbookactivityattribute_modifiedby";
				public const string LkMsdynPlaybookactivityattributeModifiedonbehalfby = "lk_msdyn_playbookactivityattribute_modifiedonbehalfby";
				public const string LkMsdynPlaybookcategoryCreatedby = "lk_msdyn_playbookcategory_createdby";
				public const string LkMsdynPlaybookcategoryCreatedonbehalfby = "lk_msdyn_playbookcategory_createdonbehalfby";
				public const string LkMsdynPlaybookcategoryModifiedby = "lk_msdyn_playbookcategory_modifiedby";
				public const string LkMsdynPlaybookcategoryModifiedonbehalfby = "lk_msdyn_playbookcategory_modifiedonbehalfby";
				public const string LkMsdynPlaybookinstanceCreatedby = "lk_msdyn_playbookinstance_createdby";
				public const string LkMsdynPlaybookinstanceCreatedonbehalfby = "lk_msdyn_playbookinstance_createdonbehalfby";
				public const string LkMsdynPlaybookinstanceModifiedby = "lk_msdyn_playbookinstance_modifiedby";
				public const string LkMsdynPlaybookinstanceModifiedonbehalfby = "lk_msdyn_playbookinstance_modifiedonbehalfby";
				public const string LkMsdynPlaybooktemplateCreatedby = "lk_msdyn_playbooktemplate_createdby";
				public const string LkMsdynPlaybooktemplateCreatedonbehalfby = "lk_msdyn_playbooktemplate_createdonbehalfby";
				public const string LkMsdynPlaybooktemplateModifiedby = "lk_msdyn_playbooktemplate_modifiedby";
				public const string LkMsdynPlaybooktemplateModifiedonbehalfby = "lk_msdyn_playbooktemplate_modifiedonbehalfby";
				public const string LkMsdynPmanalysishistoryCreatedby = "lk_msdyn_pmanalysishistory_createdby";
				public const string LkMsdynPmanalysishistoryCreatedonbehalfby = "lk_msdyn_pmanalysishistory_createdonbehalfby";
				public const string LkMsdynPmanalysishistoryModifiedby = "lk_msdyn_pmanalysishistory_modifiedby";
				public const string LkMsdynPmanalysishistoryModifiedonbehalfby = "lk_msdyn_pmanalysishistory_modifiedonbehalfby";
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
				public const string LkMsdynPmtemplateCreatedby = "lk_msdyn_pmtemplate_createdby";
				public const string LkMsdynPmtemplateCreatedonbehalfby = "lk_msdyn_pmtemplate_createdonbehalfby";
				public const string LkMsdynPmtemplateModifiedby = "lk_msdyn_pmtemplate_modifiedby";
				public const string LkMsdynPmtemplateModifiedonbehalfby = "lk_msdyn_pmtemplate_modifiedonbehalfby";
				public const string LkMsdynPmviewCreatedby = "lk_msdyn_pmview_createdby";
				public const string LkMsdynPmviewCreatedonbehalfby = "lk_msdyn_pmview_createdonbehalfby";
				public const string LkMsdynPmviewModifiedby = "lk_msdyn_pmview_modifiedby";
				public const string LkMsdynPmviewModifiedonbehalfby = "lk_msdyn_pmview_modifiedonbehalfby";
				public const string LkMsdynPostalbumCreatedby = "lk_msdyn_postalbum_createdby";
				public const string LkMsdynPostalbumCreatedonbehalfby = "lk_msdyn_postalbum_createdonbehalfby";
				public const string LkMsdynPostalbumModifiedby = "lk_msdyn_postalbum_modifiedby";
				public const string LkMsdynPostalbumModifiedonbehalfby = "lk_msdyn_postalbum_modifiedonbehalfby";
				public const string LkMsdynPostconfigCreatedby = "lk_msdyn_postconfig_createdby";
				public const string LkMsdynPostconfigCreatedonbehalfby = "lk_msdyn_postconfig_createdonbehalfby";
				public const string LkMsdynPostconfigModifiedby = "lk_msdyn_postconfig_modifiedby";
				public const string LkMsdynPostconfigModifiedonbehalfby = "lk_msdyn_postconfig_modifiedonbehalfby";
				public const string LkMsdynPostruleconfigCreatedby = "lk_msdyn_postruleconfig_createdby";
				public const string LkMsdynPostruleconfigCreatedonbehalfby = "lk_msdyn_postruleconfig_createdonbehalfby";
				public const string LkMsdynPostruleconfigModifiedby = "lk_msdyn_postruleconfig_modifiedby";
				public const string LkMsdynPostruleconfigModifiedonbehalfby = "lk_msdyn_postruleconfig_modifiedonbehalfby";
				public const string LkMsdynPredictivemodelscoreCreatedby = "lk_msdyn_predictivemodelscore_createdby";
				public const string LkMsdynPredictivemodelscoreCreatedonbehalfby = "lk_msdyn_predictivemodelscore_createdonbehalfby";
				public const string LkMsdynPredictivemodelscoreModifiedby = "lk_msdyn_predictivemodelscore_modifiedby";
				public const string LkMsdynPredictivemodelscoreModifiedonbehalfby = "lk_msdyn_predictivemodelscore_modifiedonbehalfby";
				public const string LkMsdynPredictivescoreCreatedby = "lk_msdyn_predictivescore_createdby";
				public const string LkMsdynPredictivescoreCreatedonbehalfby = "lk_msdyn_predictivescore_createdonbehalfby";
				public const string LkMsdynPredictivescoreModifiedby = "lk_msdyn_predictivescore_modifiedby";
				public const string LkMsdynPredictivescoreModifiedonbehalfby = "lk_msdyn_predictivescore_modifiedonbehalfby";
				public const string LkMsdynPresenceCreatedby = "lk_msdyn_presence_createdby";
				public const string LkMsdynPresenceCreatedonbehalfby = "lk_msdyn_presence_createdonbehalfby";
				public const string LkMsdynPresenceModifiedby = "lk_msdyn_presence_modifiedby";
				public const string LkMsdynPresenceModifiedonbehalfby = "lk_msdyn_presence_modifiedonbehalfby";
				public const string LkMsdynProductivityactioninputparameterCreatedby = "lk_msdyn_productivityactioninputparameter_createdby";
				public const string LkMsdynProductivityactioninputparameterCreatedonbehalfby = "lk_msdyn_productivityactioninputparameter_createdonbehalfby";
				public const string LkMsdynProductivityactioninputparameterModifiedby = "lk_msdyn_productivityactioninputparameter_modifiedby";
				public const string LkMsdynProductivityactioninputparameterModifiedonbehalfby = "lk_msdyn_productivityactioninputparameter_modifiedonbehalfby";
				public const string LkMsdynProductivityactionoutputparameterCreatedby = "lk_msdyn_productivityactionoutputparameter_createdby";
				public const string LkMsdynProductivityactionoutputparameterCreatedonbehalfby = "lk_msdyn_productivityactionoutputparameter_createdonbehalfby";
				public const string LkMsdynProductivityactionoutputparameterModifiedby = "lk_msdyn_productivityactionoutputparameter_modifiedby";
				public const string LkMsdynProductivityactionoutputparameterModifiedonbehalfby = "lk_msdyn_productivityactionoutputparameter_modifiedonbehalfby";
				public const string LkMsdynProductivityagentscriptCreatedby = "lk_msdyn_productivityagentscript_createdby";
				public const string LkMsdynProductivityagentscriptCreatedonbehalfby = "lk_msdyn_productivityagentscript_createdonbehalfby";
				public const string LkMsdynProductivityagentscriptModifiedby = "lk_msdyn_productivityagentscript_modifiedby";
				public const string LkMsdynProductivityagentscriptModifiedonbehalfby = "lk_msdyn_productivityagentscript_modifiedonbehalfby";
				public const string LkMsdynProductivityagentscriptstepCreatedby = "lk_msdyn_productivityagentscriptstep_createdby";
				public const string LkMsdynProductivityagentscriptstepCreatedonbehalfby = "lk_msdyn_productivityagentscriptstep_createdonbehalfby";
				public const string LkMsdynProductivityagentscriptstepModifiedby = "lk_msdyn_productivityagentscriptstep_modifiedby";
				public const string LkMsdynProductivityagentscriptstepModifiedonbehalfby = "lk_msdyn_productivityagentscriptstep_modifiedonbehalfby";
				public const string LkMsdynProductivitymacroactiontemplateCreatedby = "lk_msdyn_productivitymacroactiontemplate_createdby";
				public const string LkMsdynProductivitymacroactiontemplateCreatedonbehalfby = "lk_msdyn_productivitymacroactiontemplate_createdonbehalfby";
				public const string LkMsdynProductivitymacroactiontemplateModifiedby = "lk_msdyn_productivitymacroactiontemplate_modifiedby";
				public const string LkMsdynProductivitymacroactiontemplateModifiedonbehalfby = "lk_msdyn_productivitymacroactiontemplate_modifiedonbehalfby";
				public const string LkMsdynProductivitymacroconnectorCreatedby = "lk_msdyn_productivitymacroconnector_createdby";
				public const string LkMsdynProductivitymacroconnectorCreatedonbehalfby = "lk_msdyn_productivitymacroconnector_createdonbehalfby";
				public const string LkMsdynProductivitymacroconnectorModifiedby = "lk_msdyn_productivitymacroconnector_modifiedby";
				public const string LkMsdynProductivitymacroconnectorModifiedonbehalfby = "lk_msdyn_productivitymacroconnector_modifiedonbehalfby";
				public const string LkMsdynProductivitymacrosolutionconfigurationCreatedby = "lk_msdyn_productivitymacrosolutionconfiguration_createdby";
				public const string LkMsdynProductivitymacrosolutionconfigurationCreatedonbehalfby = "lk_msdyn_productivitymacrosolutionconfiguration_createdonbehalfby";
				public const string LkMsdynProductivitymacrosolutionconfigurationModifiedby = "lk_msdyn_productivitymacrosolutionconfiguration_modifiedby";
				public const string LkMsdynProductivitymacrosolutionconfigurationModifiedonbehalfby = "lk_msdyn_productivitymacrosolutionconfiguration_modifiedonbehalfby";
				public const string LkMsdynProductivityparameterdefinitionCreatedby = "lk_msdyn_productivityparameterdefinition_createdby";
				public const string LkMsdynProductivityparameterdefinitionCreatedonbehalfby = "lk_msdyn_productivityparameterdefinition_createdonbehalfby";
				public const string LkMsdynProductivityparameterdefinitionModifiedby = "lk_msdyn_productivityparameterdefinition_modifiedby";
				public const string LkMsdynProductivityparameterdefinitionModifiedonbehalfby = "lk_msdyn_productivityparameterdefinition_modifiedonbehalfby";
				public const string LkMsdynPropertyCreatedby = "lk_msdyn_property_createdby";
				public const string LkMsdynPropertyCreatedonbehalfby = "lk_msdyn_property_createdonbehalfby";
				public const string LkMsdynPropertyModifiedby = "lk_msdyn_property_modifiedby";
				public const string LkMsdynPropertyModifiedonbehalfby = "lk_msdyn_property_modifiedonbehalfby";
				public const string LkMsdynPropertyassetassociationCreatedby = "lk_msdyn_propertyassetassociation_createdby";
				public const string LkMsdynPropertyassetassociationCreatedonbehalfby = "lk_msdyn_propertyassetassociation_createdonbehalfby";
				public const string LkMsdynPropertyassetassociationModifiedby = "lk_msdyn_propertyassetassociation_modifiedby";
				public const string LkMsdynPropertyassetassociationModifiedonbehalfby = "lk_msdyn_propertyassetassociation_modifiedonbehalfby";
				public const string LkMsdynPropertylogCreatedby = "lk_msdyn_propertylog_createdby";
				public const string LkMsdynPropertylogCreatedonbehalfby = "lk_msdyn_propertylog_createdonbehalfby";
				public const string LkMsdynPropertylogModifiedby = "lk_msdyn_propertylog_modifiedby";
				public const string LkMsdynPropertylogModifiedonbehalfby = "lk_msdyn_propertylog_modifiedonbehalfby";
				public const string LkMsdynPropertytemplateassociationCreatedby = "lk_msdyn_propertytemplateassociation_createdby";
				public const string LkMsdynPropertytemplateassociationCreatedonbehalfby = "lk_msdyn_propertytemplateassociation_createdonbehalfby";
				public const string LkMsdynPropertytemplateassociationModifiedby = "lk_msdyn_propertytemplateassociation_modifiedby";
				public const string LkMsdynPropertytemplateassociationModifiedonbehalfby = "lk_msdyn_propertytemplateassociation_modifiedonbehalfby";
				public const string LkMsdynProviderCreatedby = "lk_msdyn_provider_createdby";
				public const string LkMsdynProviderCreatedonbehalfby = "lk_msdyn_provider_createdonbehalfby";
				public const string LkMsdynProviderModifiedby = "lk_msdyn_provider_modifiedby";
				public const string LkMsdynProviderModifiedonbehalfby = "lk_msdyn_provider_modifiedonbehalfby";
				public const string LkMsdynRealtimescoringCreatedby = "lk_msdyn_realtimescoring_createdby";
				public const string LkMsdynRealtimescoringCreatedonbehalfby = "lk_msdyn_realtimescoring_createdonbehalfby";
				public const string LkMsdynRealtimescoringModifiedby = "lk_msdyn_realtimescoring_modifiedby";
				public const string LkMsdynRealtimescoringModifiedonbehalfby = "lk_msdyn_realtimescoring_modifiedonbehalfby";
				public const string LkMsdynRecomputetrackerCreatedby = "lk_msdyn_recomputetracker_createdby";
				public const string LkMsdynRecomputetrackerCreatedonbehalfby = "lk_msdyn_recomputetracker_createdonbehalfby";
				public const string LkMsdynRecomputetrackerModifiedby = "lk_msdyn_recomputetracker_modifiedby";
				public const string LkMsdynRecomputetrackerModifiedonbehalfby = "lk_msdyn_recomputetracker_modifiedonbehalfby";
				public const string LkMsdynRecordingCreatedby = "lk_msdyn_recording_createdby";
				public const string LkMsdynRecordingCreatedonbehalfby = "lk_msdyn_recording_createdonbehalfby";
				public const string LkMsdynRecordingModifiedby = "lk_msdyn_recording_modifiedby";
				public const string LkMsdynRecordingModifiedonbehalfby = "lk_msdyn_recording_modifiedonbehalfby";
				public const string LkMsdynRecurrenceCreatedby = "lk_msdyn_recurrence_createdby";
				public const string LkMsdynRecurrenceCreatedonbehalfby = "lk_msdyn_recurrence_createdonbehalfby";
				public const string LkMsdynRecurrenceModifiedby = "lk_msdyn_recurrence_modifiedby";
				public const string LkMsdynRecurrenceModifiedonbehalfby = "lk_msdyn_recurrence_modifiedonbehalfby";
				public const string LkMsdynRecurringsalesactionCreatedby = "lk_msdyn_recurringsalesaction_createdby";
				public const string LkMsdynRecurringsalesactionCreatedonbehalfby = "lk_msdyn_recurringsalesaction_createdonbehalfby";
				public const string LkMsdynRecurringsalesactionModifiedby = "lk_msdyn_recurringsalesaction_modifiedby";
				public const string LkMsdynRecurringsalesactionModifiedonbehalfby = "lk_msdyn_recurringsalesaction_modifiedonbehalfby";
				public const string LkMsdynRelationshipinsightsunifiedconfigCreatedby = "lk_msdyn_relationshipinsightsunifiedconfig_createdby";
				public const string LkMsdynRelationshipinsightsunifiedconfigCreatedonbehalfby = "lk_msdyn_relationshipinsightsunifiedconfig_createdonbehalfby";
				public const string LkMsdynRelationshipinsightsunifiedconfigModifiedby = "lk_msdyn_relationshipinsightsunifiedconfig_modifiedby";
				public const string LkMsdynRelationshipinsightsunifiedconfigModifiedonbehalfby = "lk_msdyn_relationshipinsightsunifiedconfig_modifiedonbehalfby";
				public const string LkMsdynReportbookmarkCreatedby = "lk_msdyn_reportbookmark_createdby";
				public const string LkMsdynReportbookmarkCreatedonbehalfby = "lk_msdyn_reportbookmark_createdonbehalfby";
				public const string LkMsdynReportbookmarkModifiedby = "lk_msdyn_reportbookmark_modifiedby";
				public const string LkMsdynReportbookmarkModifiedonbehalfby = "lk_msdyn_reportbookmark_modifiedonbehalfby";
				public const string LkMsdynRichtextfileCreatedby = "lk_msdyn_richtextfile_createdby";
				public const string LkMsdynRichtextfileCreatedonbehalfby = "lk_msdyn_richtextfile_createdonbehalfby";
				public const string LkMsdynRichtextfileModifiedby = "lk_msdyn_richtextfile_modifiedby";
				public const string LkMsdynRichtextfileModifiedonbehalfby = "lk_msdyn_richtextfile_modifiedonbehalfby";
				public const string LkMsdynRoutingconfigurationCreatedby = "lk_msdyn_routingconfiguration_createdby";
				public const string LkMsdynRoutingconfigurationCreatedonbehalfby = "lk_msdyn_routingconfiguration_createdonbehalfby";
				public const string LkMsdynRoutingconfigurationModifiedby = "lk_msdyn_routingconfiguration_modifiedby";
				public const string LkMsdynRoutingconfigurationModifiedonbehalfby = "lk_msdyn_routingconfiguration_modifiedonbehalfby";
				public const string LkMsdynRoutingconfigurationstepCreatedby = "lk_msdyn_routingconfigurationstep_createdby";
				public const string LkMsdynRoutingconfigurationstepCreatedonbehalfby = "lk_msdyn_routingconfigurationstep_createdonbehalfby";
				public const string LkMsdynRoutingconfigurationstepModifiedby = "lk_msdyn_routingconfigurationstep_modifiedby";
				public const string LkMsdynRoutingconfigurationstepModifiedonbehalfby = "lk_msdyn_routingconfigurationstep_modifiedonbehalfby";
				public const string LkMsdynRoutingrequestCreatedby = "lk_msdyn_routingrequest_createdby";
				public const string LkMsdynRoutingrequestCreatedonbehalfby = "lk_msdyn_routingrequest_createdonbehalfby";
				public const string LkMsdynRoutingrequestModifiedby = "lk_msdyn_routingrequest_modifiedby";
				public const string LkMsdynRoutingrequestModifiedonbehalfby = "lk_msdyn_routingrequest_modifiedonbehalfby";
				public const string LkMsdynRoutingrulesetsettingCreatedby = "lk_msdyn_routingrulesetsetting_createdby";
				public const string LkMsdynRoutingrulesetsettingCreatedonbehalfby = "lk_msdyn_routingrulesetsetting_createdonbehalfby";
				public const string LkMsdynRoutingrulesetsettingModifiedby = "lk_msdyn_routingrulesetsetting_modifiedby";
				public const string LkMsdynRoutingrulesetsettingModifiedonbehalfby = "lk_msdyn_routingrulesetsetting_modifiedonbehalfby";
				public const string LkMsdynRulesetdependencymappingCreatedby = "lk_msdyn_rulesetdependencymapping_createdby";
				public const string LkMsdynRulesetdependencymappingCreatedonbehalfby = "lk_msdyn_rulesetdependencymapping_createdonbehalfby";
				public const string LkMsdynRulesetdependencymappingModifiedby = "lk_msdyn_rulesetdependencymapping_modifiedby";
				public const string LkMsdynRulesetdependencymappingModifiedonbehalfby = "lk_msdyn_rulesetdependencymapping_modifiedonbehalfby";
				public const string LkMsdynSabackupdiagnosticCreatedby = "lk_msdyn_sabackupdiagnostic_createdby";
				public const string LkMsdynSabackupdiagnosticCreatedonbehalfby = "lk_msdyn_sabackupdiagnostic_createdonbehalfby";
				public const string LkMsdynSabackupdiagnosticModifiedby = "lk_msdyn_sabackupdiagnostic_modifiedby";
				public const string LkMsdynSabackupdiagnosticModifiedonbehalfby = "lk_msdyn_sabackupdiagnostic_modifiedonbehalfby";
				public const string LkMsdynSabatchruninstanceCreatedby = "lk_msdyn_sabatchruninstance_createdby";
				public const string LkMsdynSabatchruninstanceCreatedonbehalfby = "lk_msdyn_sabatchruninstance_createdonbehalfby";
				public const string LkMsdynSabatchruninstanceModifiedby = "lk_msdyn_sabatchruninstance_modifiedby";
				public const string LkMsdynSabatchruninstanceModifiedonbehalfby = "lk_msdyn_sabatchruninstance_modifiedonbehalfby";
				public const string LkMsdynSalesaccelerationinsightCreatedby = "lk_msdyn_salesaccelerationinsight_createdby";
				public const string LkMsdynSalesaccelerationinsightCreatedonbehalfby = "lk_msdyn_salesaccelerationinsight_createdonbehalfby";
				public const string LkMsdynSalesaccelerationinsightModifiedby = "lk_msdyn_salesaccelerationinsight_modifiedby";
				public const string LkMsdynSalesaccelerationinsightModifiedonbehalfby = "lk_msdyn_salesaccelerationinsight_modifiedonbehalfby";
				public const string LkMsdynSalesaccelerationsettingsCreatedby = "lk_msdyn_salesaccelerationsettings_createdby";
				public const string LkMsdynSalesaccelerationsettingsCreatedonbehalfby = "lk_msdyn_salesaccelerationsettings_createdonbehalfby";
				public const string LkMsdynSalesaccelerationsettingsModifiedby = "lk_msdyn_salesaccelerationsettings_modifiedby";
				public const string LkMsdynSalesaccelerationsettingsModifiedonbehalfby = "lk_msdyn_salesaccelerationsettings_modifiedonbehalfby";
				public const string LkMsdynSalesassignmentsettingCreatedby = "lk_msdyn_salesassignmentsetting_createdby";
				public const string LkMsdynSalesassignmentsettingCreatedonbehalfby = "lk_msdyn_salesassignmentsetting_createdonbehalfby";
				public const string LkMsdynSalesassignmentsettingModifiedby = "lk_msdyn_salesassignmentsetting_modifiedby";
				public const string LkMsdynSalesassignmentsettingModifiedonbehalfby = "lk_msdyn_salesassignmentsetting_modifiedonbehalfby";
				public const string LkMsdynSalesinsightssettingsCreatedby = "lk_msdyn_salesinsightssettings_createdby";
				public const string LkMsdynSalesinsightssettingsCreatedonbehalfby = "lk_msdyn_salesinsightssettings_createdonbehalfby";
				public const string LkMsdynSalesinsightssettingsModifiedby = "lk_msdyn_salesinsightssettings_modifiedby";
				public const string LkMsdynSalesinsightssettingsModifiedonbehalfby = "lk_msdyn_salesinsightssettings_modifiedonbehalfby";
				public const string LkMsdynSalesroutingdiagnosticCreatedby = "lk_msdyn_salesroutingdiagnostic_createdby";
				public const string LkMsdynSalesroutingdiagnosticCreatedonbehalfby = "lk_msdyn_salesroutingdiagnostic_createdonbehalfby";
				public const string LkMsdynSalesroutingdiagnosticModifiedby = "lk_msdyn_salesroutingdiagnostic_modifiedby";
				public const string LkMsdynSalesroutingdiagnosticModifiedonbehalfby = "lk_msdyn_salesroutingdiagnostic_modifiedonbehalfby";
				public const string LkMsdynSalesroutingrunCreatedby = "lk_msdyn_salesroutingrun_createdby";
				public const string LkMsdynSalesroutingrunCreatedonbehalfby = "lk_msdyn_salesroutingrun_createdonbehalfby";
				public const string LkMsdynSalesroutingrunModifiedby = "lk_msdyn_salesroutingrun_modifiedby";
				public const string LkMsdynSalesroutingrunModifiedonbehalfby = "lk_msdyn_salesroutingrun_modifiedonbehalfby";
				public const string LkMsdynSalessuggestionCreatedby = "lk_msdyn_salessuggestion_createdby";
				public const string LkMsdynSalessuggestionCreatedonbehalfby = "lk_msdyn_salessuggestion_createdonbehalfby";
				public const string LkMsdynSalessuggestionModifiedby = "lk_msdyn_salessuggestion_modifiedby";
				public const string LkMsdynSalessuggestionModifiedonbehalfby = "lk_msdyn_salessuggestion_modifiedonbehalfby";
				public const string LkMsdynSalestagCreatedby = "lk_msdyn_salestag_createdby";
				public const string LkMsdynSalestagCreatedonbehalfby = "lk_msdyn_salestag_createdonbehalfby";
				public const string LkMsdynSalestagModifiedby = "lk_msdyn_salestag_modifiedby";
				public const string LkMsdynSalestagModifiedonbehalfby = "lk_msdyn_salestag_modifiedonbehalfby";
				public const string LkMsdynSaruninstanceCreatedby = "lk_msdyn_saruninstance_createdby";
				public const string LkMsdynSaruninstanceCreatedonbehalfby = "lk_msdyn_saruninstance_createdonbehalfby";
				public const string LkMsdynSaruninstanceModifiedby = "lk_msdyn_saruninstance_modifiedby";
				public const string LkMsdynSaruninstanceModifiedonbehalfby = "lk_msdyn_saruninstance_modifiedonbehalfby";
				public const string LkMsdynSearchconfigurationCreatedby = "lk_msdyn_searchconfiguration_createdby";
				public const string LkMsdynSearchconfigurationCreatedonbehalfby = "lk_msdyn_searchconfiguration_createdonbehalfby";
				public const string LkMsdynSearchconfigurationModifiedby = "lk_msdyn_searchconfiguration_modifiedby";
				public const string LkMsdynSearchconfigurationModifiedonbehalfby = "lk_msdyn_searchconfiguration_modifiedonbehalfby";
				public const string LkMsdynSegmentCreatedby = "lk_msdyn_segment_createdby";
				public const string LkMsdynSegmentCreatedonbehalfby = "lk_msdyn_segment_createdonbehalfby";
				public const string LkMsdynSegmentModifiedby = "lk_msdyn_segment_modifiedby";
				public const string LkMsdynSegmentModifiedonbehalfby = "lk_msdyn_segment_modifiedonbehalfby";
				public const string LkMsdynSegmentationsettingCreatedby = "lk_msdyn_segmentationsetting_createdby";
				public const string LkMsdynSegmentationsettingCreatedonbehalfby = "lk_msdyn_segmentationsetting_createdonbehalfby";
				public const string LkMsdynSegmentationsettingModifiedby = "lk_msdyn_segmentationsetting_modifiedby";
				public const string LkMsdynSegmentationsettingModifiedonbehalfby = "lk_msdyn_segmentationsetting_modifiedonbehalfby";
				public const string LkMsdynSegmentattributeCreatedby = "lk_msdyn_segmentattribute_createdby";
				public const string LkMsdynSegmentattributeCreatedonbehalfby = "lk_msdyn_segmentattribute_createdonbehalfby";
				public const string LkMsdynSegmentattributeModifiedby = "lk_msdyn_segmentattribute_modifiedby";
				public const string LkMsdynSegmentattributeModifiedonbehalfby = "lk_msdyn_segmentattribute_modifiedonbehalfby";
				public const string LkMsdynSegmentcatalogueCreatedby = "lk_msdyn_segmentcatalogue_createdby";
				public const string LkMsdynSegmentcatalogueCreatedonbehalfby = "lk_msdyn_segmentcatalogue_createdonbehalfby";
				public const string LkMsdynSegmentcatalogueModifiedby = "lk_msdyn_segmentcatalogue_modifiedby";
				public const string LkMsdynSegmentcatalogueModifiedonbehalfby = "lk_msdyn_segmentcatalogue_modifiedonbehalfby";
				public const string LkMsdynSentimentanalysisCreatedby = "lk_msdyn_sentimentanalysis_createdby";
				public const string LkMsdynSentimentanalysisCreatedonbehalfby = "lk_msdyn_sentimentanalysis_createdonbehalfby";
				public const string LkMsdynSentimentanalysisModifiedby = "lk_msdyn_sentimentanalysis_modifiedby";
				public const string LkMsdynSentimentanalysisModifiedonbehalfby = "lk_msdyn_sentimentanalysis_modifiedonbehalfby";
				public const string LkMsdynSequenceCreatedby = "lk_msdyn_sequence_createdby";
				public const string LkMsdynSequenceCreatedonbehalfby = "lk_msdyn_sequence_createdonbehalfby";
				public const string LkMsdynSequenceModifiedby = "lk_msdyn_sequence_modifiedby";
				public const string LkMsdynSequenceModifiedonbehalfby = "lk_msdyn_sequence_modifiedonbehalfby";
				public const string LkMsdynSequencestatCreatedby = "lk_msdyn_sequencestat_createdby";
				public const string LkMsdynSequencestatCreatedonbehalfby = "lk_msdyn_sequencestat_createdonbehalfby";
				public const string LkMsdynSequencestatModifiedby = "lk_msdyn_sequencestat_modifiedby";
				public const string LkMsdynSequencestatModifiedonbehalfby = "lk_msdyn_sequencestat_modifiedonbehalfby";
				public const string LkMsdynSequencetargetCreatedby = "lk_msdyn_sequencetarget_createdby";
				public const string LkMsdynSequencetargetCreatedonbehalfby = "lk_msdyn_sequencetarget_createdonbehalfby";
				public const string LkMsdynSequencetargetModifiedby = "lk_msdyn_sequencetarget_modifiedby";
				public const string LkMsdynSequencetargetModifiedonbehalfby = "lk_msdyn_sequencetarget_modifiedonbehalfby";
				public const string LkMsdynSequencetargetstepCreatedby = "lk_msdyn_sequencetargetstep_createdby";
				public const string LkMsdynSequencetargetstepCreatedonbehalfby = "lk_msdyn_sequencetargetstep_createdonbehalfby";
				public const string LkMsdynSequencetargetstepModifiedby = "lk_msdyn_sequencetargetstep_modifiedby";
				public const string LkMsdynSequencetargetstepModifiedonbehalfby = "lk_msdyn_sequencetargetstep_modifiedonbehalfby";
				public const string LkMsdynSequencetemplateCreatedby = "lk_msdyn_sequencetemplate_createdby";
				public const string LkMsdynSequencetemplateCreatedonbehalfby = "lk_msdyn_sequencetemplate_createdonbehalfby";
				public const string LkMsdynSequencetemplateModifiedby = "lk_msdyn_sequencetemplate_modifiedby";
				public const string LkMsdynSequencetemplateModifiedonbehalfby = "lk_msdyn_sequencetemplate_modifiedonbehalfby";
				public const string LkMsdynServiceconfigurationCreatedby = "lk_msdyn_serviceconfiguration_createdby";
				public const string LkMsdynServiceconfigurationCreatedonbehalfby = "lk_msdyn_serviceconfiguration_createdonbehalfby";
				public const string LkMsdynServiceconfigurationModifiedby = "lk_msdyn_serviceconfiguration_modifiedby";
				public const string LkMsdynServiceconfigurationModifiedonbehalfby = "lk_msdyn_serviceconfiguration_modifiedonbehalfby";
				public const string LkMsdynSessiondataCreatedby = "lk_msdyn_sessiondata_createdby";
				public const string LkMsdynSessiondataCreatedonbehalfby = "lk_msdyn_sessiondata_createdonbehalfby";
				public const string LkMsdynSessiondataModifiedby = "lk_msdyn_sessiondata_modifiedby";
				public const string LkMsdynSessiondataModifiedonbehalfby = "lk_msdyn_sessiondata_modifiedonbehalfby";
				public const string LkMsdynSessioneventCreatedby = "lk_msdyn_sessionevent_createdby";
				public const string LkMsdynSessioneventCreatedonbehalfby = "lk_msdyn_sessionevent_createdonbehalfby";
				public const string LkMsdynSessioneventModifiedby = "lk_msdyn_sessionevent_modifiedby";
				public const string LkMsdynSessioneventModifiedonbehalfby = "lk_msdyn_sessionevent_modifiedonbehalfby";
				public const string LkMsdynSessionparticipantCreatedby = "lk_msdyn_sessionparticipant_createdby";
				public const string LkMsdynSessionparticipantCreatedonbehalfby = "lk_msdyn_sessionparticipant_createdonbehalfby";
				public const string LkMsdynSessionparticipantModifiedby = "lk_msdyn_sessionparticipant_modifiedby";
				public const string LkMsdynSessionparticipantModifiedonbehalfby = "lk_msdyn_sessionparticipant_modifiedonbehalfby";
				public const string LkMsdynSessionparticipantdataCreatedby = "lk_msdyn_sessionparticipantdata_createdby";
				public const string LkMsdynSessionparticipantdataCreatedonbehalfby = "lk_msdyn_sessionparticipantdata_createdonbehalfby";
				public const string LkMsdynSessionparticipantdataModifiedby = "lk_msdyn_sessionparticipantdata_modifiedby";
				public const string LkMsdynSessionparticipantdataModifiedonbehalfby = "lk_msdyn_sessionparticipantdata_modifiedonbehalfby";
				public const string LkMsdynSessiontemplateCreatedby = "lk_msdyn_sessiontemplate_createdby";
				public const string LkMsdynSessiontemplateCreatedonbehalfby = "lk_msdyn_sessiontemplate_createdonbehalfby";
				public const string LkMsdynSessiontemplateModifiedby = "lk_msdyn_sessiontemplate_modifiedby";
				public const string LkMsdynSessiontemplateModifiedonbehalfby = "lk_msdyn_sessiontemplate_modifiedonbehalfby";
				public const string LkMsdynShareasconfigurationCreatedby = "lk_msdyn_shareasconfiguration_createdby";
				public const string LkMsdynShareasconfigurationCreatedonbehalfby = "lk_msdyn_shareasconfiguration_createdonbehalfby";
				public const string LkMsdynShareasconfigurationModifiedby = "lk_msdyn_shareasconfiguration_modifiedby";
				public const string LkMsdynShareasconfigurationModifiedonbehalfby = "lk_msdyn_shareasconfiguration_modifiedonbehalfby";
				public const string LkMsdynSiconfigCreatedby = "lk_msdyn_siconfig_createdby";
				public const string LkMsdynSiconfigCreatedonbehalfby = "lk_msdyn_siconfig_createdonbehalfby";
				public const string LkMsdynSiconfigModifiedby = "lk_msdyn_siconfig_modifiedby";
				public const string LkMsdynSiconfigModifiedonbehalfby = "lk_msdyn_siconfig_modifiedonbehalfby";
				public const string LkMsdynSikeyvalueconfigCreatedby = "lk_msdyn_sikeyvalueconfig_createdby";
				public const string LkMsdynSikeyvalueconfigCreatedonbehalfby = "lk_msdyn_sikeyvalueconfig_createdonbehalfby";
				public const string LkMsdynSikeyvalueconfigModifiedby = "lk_msdyn_sikeyvalueconfig_modifiedby";
				public const string LkMsdynSikeyvalueconfigModifiedonbehalfby = "lk_msdyn_sikeyvalueconfig_modifiedonbehalfby";
				public const string LkMsdynSkillattachmentruleitemCreatedby = "lk_msdyn_skillattachmentruleitem_createdby";
				public const string LkMsdynSkillattachmentruleitemCreatedonbehalfby = "lk_msdyn_skillattachmentruleitem_createdonbehalfby";
				public const string LkMsdynSkillattachmentruleitemModifiedby = "lk_msdyn_skillattachmentruleitem_modifiedby";
				public const string LkMsdynSkillattachmentruleitemModifiedonbehalfby = "lk_msdyn_skillattachmentruleitem_modifiedonbehalfby";
				public const string LkMsdynSkillattachmenttargetCreatedby = "lk_msdyn_skillattachmenttarget_createdby";
				public const string LkMsdynSkillattachmenttargetCreatedonbehalfby = "lk_msdyn_skillattachmenttarget_createdonbehalfby";
				public const string LkMsdynSkillattachmenttargetModifiedby = "lk_msdyn_skillattachmenttarget_modifiedby";
				public const string LkMsdynSkillattachmenttargetModifiedonbehalfby = "lk_msdyn_skillattachmenttarget_modifiedonbehalfby";
				public const string LkMsdynSlakpiCreatedby = "lk_msdyn_slakpi_createdby";
				public const string LkMsdynSlakpiCreatedonbehalfby = "lk_msdyn_slakpi_createdonbehalfby";
				public const string LkMsdynSlakpiModifiedby = "lk_msdyn_slakpi_modifiedby";
				public const string LkMsdynSlakpiModifiedonbehalfby = "lk_msdyn_slakpi_modifiedonbehalfby";
				public const string LkMsdynSmartassistconfigCreatedby = "lk_msdyn_smartassistconfig_createdby";
				public const string LkMsdynSmartassistconfigCreatedonbehalfby = "lk_msdyn_smartassistconfig_createdonbehalfby";
				public const string LkMsdynSmartassistconfigModifiedby = "lk_msdyn_smartassistconfig_modifiedby";
				public const string LkMsdynSmartassistconfigModifiedonbehalfby = "lk_msdyn_smartassistconfig_modifiedonbehalfby";
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
				public const string LkMsdynSoundfileCreatedby = "lk_msdyn_soundfile_createdby";
				public const string LkMsdynSoundfileCreatedonbehalfby = "lk_msdyn_soundfile_createdonbehalfby";
				public const string LkMsdynSoundfileModifiedby = "lk_msdyn_soundfile_modifiedby";
				public const string LkMsdynSoundfileModifiedonbehalfby = "lk_msdyn_soundfile_modifiedonbehalfby";
				public const string LkMsdynSoundnotificationsettingCreatedby = "lk_msdyn_soundnotificationsetting_createdby";
				public const string LkMsdynSoundnotificationsettingCreatedonbehalfby = "lk_msdyn_soundnotificationsetting_createdonbehalfby";
				public const string LkMsdynSoundnotificationsettingModifiedby = "lk_msdyn_soundnotificationsetting_modifiedby";
				public const string LkMsdynSoundnotificationsettingModifiedonbehalfby = "lk_msdyn_soundnotificationsetting_modifiedonbehalfby";
				public const string LkMsdynSuggestioninteractionCreatedby = "lk_msdyn_suggestioninteraction_createdby";
				public const string LkMsdynSuggestioninteractionCreatedonbehalfby = "lk_msdyn_suggestioninteraction_createdonbehalfby";
				public const string LkMsdynSuggestioninteractionModifiedby = "lk_msdyn_suggestioninteraction_modifiedby";
				public const string LkMsdynSuggestioninteractionModifiedonbehalfby = "lk_msdyn_suggestioninteraction_modifiedonbehalfby";
				public const string LkMsdynSuggestionrequestpayloadCreatedby = "lk_msdyn_suggestionrequestpayload_createdby";
				public const string LkMsdynSuggestionrequestpayloadCreatedonbehalfby = "lk_msdyn_suggestionrequestpayload_createdonbehalfby";
				public const string LkMsdynSuggestionrequestpayloadModifiedby = "lk_msdyn_suggestionrequestpayload_modifiedby";
				public const string LkMsdynSuggestionrequestpayloadModifiedonbehalfby = "lk_msdyn_suggestionrequestpayload_modifiedonbehalfby";
				public const string LkMsdynSuggestionsmodelsummaryCreatedby = "lk_msdyn_suggestionsmodelsummary_createdby";
				public const string LkMsdynSuggestionsmodelsummaryCreatedonbehalfby = "lk_msdyn_suggestionsmodelsummary_createdonbehalfby";
				public const string LkMsdynSuggestionsmodelsummaryModifiedby = "lk_msdyn_suggestionsmodelsummary_modifiedby";
				public const string LkMsdynSuggestionsmodelsummaryModifiedonbehalfby = "lk_msdyn_suggestionsmodelsummary_modifiedonbehalfby";
				public const string LkMsdynSuggestionssettingCreatedby = "lk_msdyn_suggestionssetting_createdby";
				public const string LkMsdynSuggestionssettingCreatedonbehalfby = "lk_msdyn_suggestionssetting_createdonbehalfby";
				public const string LkMsdynSuggestionssettingModifiedby = "lk_msdyn_suggestionssetting_modifiedby";
				public const string LkMsdynSuggestionssettingModifiedonbehalfby = "lk_msdyn_suggestionssetting_modifiedonbehalfby";
				public const string LkMsdynSwarmCreatedby = "lk_msdyn_swarm_createdby";
				public const string LkMsdynSwarmCreatedonbehalfby = "lk_msdyn_swarm_createdonbehalfby";
				public const string LkMsdynSwarmModifiedby = "lk_msdyn_swarm_modifiedby";
				public const string LkMsdynSwarmModifiedonbehalfby = "lk_msdyn_swarm_modifiedonbehalfby";
				public const string LkMsdynSwarmparticipantCreatedby = "lk_msdyn_swarmparticipant_createdby";
				public const string LkMsdynSwarmparticipantCreatedonbehalfby = "lk_msdyn_swarmparticipant_createdonbehalfby";
				public const string LkMsdynSwarmparticipantModifiedby = "lk_msdyn_swarmparticipant_modifiedby";
				public const string LkMsdynSwarmparticipantModifiedonbehalfby = "lk_msdyn_swarmparticipant_modifiedonbehalfby";
				public const string LkMsdynSwarmparticipantruleCreatedby = "lk_msdyn_swarmparticipantrule_createdby";
				public const string LkMsdynSwarmparticipantruleCreatedonbehalfby = "lk_msdyn_swarmparticipantrule_createdonbehalfby";
				public const string LkMsdynSwarmparticipantruleModifiedby = "lk_msdyn_swarmparticipantrule_modifiedby";
				public const string LkMsdynSwarmparticipantruleModifiedonbehalfby = "lk_msdyn_swarmparticipantrule_modifiedonbehalfby";
				public const string LkMsdynSwarmroleCreatedby = "lk_msdyn_swarmrole_createdby";
				public const string LkMsdynSwarmroleCreatedonbehalfby = "lk_msdyn_swarmrole_createdonbehalfby";
				public const string LkMsdynSwarmroleModifiedby = "lk_msdyn_swarmrole_modifiedby";
				public const string LkMsdynSwarmroleModifiedonbehalfby = "lk_msdyn_swarmrole_modifiedonbehalfby";
				public const string LkMsdynSwarmskillCreatedby = "lk_msdyn_swarmskill_createdby";
				public const string LkMsdynSwarmskillCreatedonbehalfby = "lk_msdyn_swarmskill_createdonbehalfby";
				public const string LkMsdynSwarmskillModifiedby = "lk_msdyn_swarmskill_modifiedby";
				public const string LkMsdynSwarmskillModifiedonbehalfby = "lk_msdyn_swarmskill_modifiedonbehalfby";
				public const string LkMsdynSwarmtemplateCreatedby = "lk_msdyn_swarmtemplate_createdby";
				public const string LkMsdynSwarmtemplateCreatedonbehalfby = "lk_msdyn_swarmtemplate_createdonbehalfby";
				public const string LkMsdynSwarmtemplateModifiedby = "lk_msdyn_swarmtemplate_modifiedby";
				public const string LkMsdynSwarmtemplateModifiedonbehalfby = "lk_msdyn_swarmtemplate_modifiedonbehalfby";
				public const string LkMsdynTaggedrecordCreatedby = "lk_msdyn_taggedrecord_createdby";
				public const string LkMsdynTaggedrecordCreatedonbehalfby = "lk_msdyn_taggedrecord_createdonbehalfby";
				public const string LkMsdynTaggedrecordModifiedby = "lk_msdyn_taggedrecord_modifiedby";
				public const string LkMsdynTaggedrecordModifiedonbehalfby = "lk_msdyn_taggedrecord_modifiedonbehalfby";
				public const string LkMsdynTeamschatassociationCreatedby = "lk_msdyn_teamschatassociation_createdby";
				public const string LkMsdynTeamschatassociationCreatedonbehalfby = "lk_msdyn_teamschatassociation_createdonbehalfby";
				public const string LkMsdynTeamschatassociationModifiedby = "lk_msdyn_teamschatassociation_modifiedby";
				public const string LkMsdynTeamschatassociationModifiedonbehalfby = "lk_msdyn_teamschatassociation_modifiedonbehalfby";
				public const string LkMsdynTeamschatsuggestionCreatedby = "lk_msdyn_teamschatsuggestion_createdby";
				public const string LkMsdynTeamschatsuggestionCreatedonbehalfby = "lk_msdyn_teamschatsuggestion_createdonbehalfby";
				public const string LkMsdynTeamschatsuggestionModifiedby = "lk_msdyn_teamschatsuggestion_modifiedby";
				public const string LkMsdynTeamschatsuggestionModifiedonbehalfby = "lk_msdyn_teamschatsuggestion_modifiedonbehalfby";
				public const string LkMsdynTeamscollaborationCreatedby = "lk_msdyn_teamscollaboration_createdby";
				public const string LkMsdynTeamscollaborationCreatedonbehalfby = "lk_msdyn_teamscollaboration_createdonbehalfby";
				public const string LkMsdynTeamscollaborationModifiedby = "lk_msdyn_teamscollaboration_modifiedby";
				public const string LkMsdynTeamscollaborationModifiedonbehalfby = "lk_msdyn_teamscollaboration_modifiedonbehalfby";
				public const string LkMsdynTeamsdialeradminsettingsCreatedby = "lk_msdyn_teamsdialeradminsettings_createdby";
				public const string LkMsdynTeamsdialeradminsettingsCreatedonbehalfby = "lk_msdyn_teamsdialeradminsettings_createdonbehalfby";
				public const string LkMsdynTeamsdialeradminsettingsModifiedby = "lk_msdyn_teamsdialeradminsettings_modifiedby";
				public const string LkMsdynTeamsdialeradminsettingsModifiedonbehalfby = "lk_msdyn_teamsdialeradminsettings_modifiedonbehalfby";
				public const string LkMsdynTemplateforpropertiesCreatedby = "lk_msdyn_templateforproperties_createdby";
				public const string LkMsdynTemplateforpropertiesCreatedonbehalfby = "lk_msdyn_templateforproperties_createdonbehalfby";
				public const string LkMsdynTemplateforpropertiesModifiedby = "lk_msdyn_templateforproperties_modifiedby";
				public const string LkMsdynTemplateforpropertiesModifiedonbehalfby = "lk_msdyn_templateforproperties_modifiedonbehalfby";
				public const string LkMsdynTemplateparameterCreatedby = "lk_msdyn_templateparameter_createdby";
				public const string LkMsdynTemplateparameterCreatedonbehalfby = "lk_msdyn_templateparameter_createdonbehalfby";
				public const string LkMsdynTemplateparameterModifiedby = "lk_msdyn_templateparameter_modifiedby";
				public const string LkMsdynTemplateparameterModifiedonbehalfby = "lk_msdyn_templateparameter_modifiedonbehalfby";
				public const string LkMsdynTimespentCreatedby = "lk_msdyn_timespent_createdby";
				public const string LkMsdynTimespentCreatedonbehalfby = "lk_msdyn_timespent_createdonbehalfby";
				public const string LkMsdynTimespentModifiedby = "lk_msdyn_timespent_modifiedby";
				public const string LkMsdynTimespentModifiedonbehalfby = "lk_msdyn_timespent_modifiedonbehalfby";
				public const string LkMsdynTourCreatedby = "lk_msdyn_tour_createdby";
				public const string LkMsdynTourCreatedonbehalfby = "lk_msdyn_tour_createdonbehalfby";
				public const string LkMsdynTourModifiedby = "lk_msdyn_tour_modifiedby";
				public const string LkMsdynTourModifiedonbehalfby = "lk_msdyn_tour_modifiedonbehalfby";
				public const string LkMsdynTranscriptCreatedby = "lk_msdyn_transcript_createdby";
				public const string LkMsdynTranscriptCreatedonbehalfby = "lk_msdyn_transcript_createdonbehalfby";
				public const string LkMsdynTranscriptModifiedby = "lk_msdyn_transcript_modifiedby";
				public const string LkMsdynTranscriptModifiedonbehalfby = "lk_msdyn_transcript_modifiedonbehalfby";
				public const string LkMsdynUnifiedroutingdiagnosticCreatedby = "lk_msdyn_unifiedroutingdiagnostic_createdby";
				public const string LkMsdynUnifiedroutingdiagnosticCreatedonbehalfby = "lk_msdyn_unifiedroutingdiagnostic_createdonbehalfby";
				public const string LkMsdynUnifiedroutingdiagnosticModifiedby = "lk_msdyn_unifiedroutingdiagnostic_modifiedby";
				public const string LkMsdynUnifiedroutingdiagnosticModifiedonbehalfby = "lk_msdyn_unifiedroutingdiagnostic_modifiedonbehalfby";
				public const string LkMsdynUnifiedroutingrunCreatedby = "lk_msdyn_unifiedroutingrun_createdby";
				public const string LkMsdynUnifiedroutingrunCreatedonbehalfby = "lk_msdyn_unifiedroutingrun_createdonbehalfby";
				public const string LkMsdynUnifiedroutingrunModifiedby = "lk_msdyn_unifiedroutingrun_modifiedby";
				public const string LkMsdynUnifiedroutingrunModifiedonbehalfby = "lk_msdyn_unifiedroutingrun_modifiedonbehalfby";
				public const string LkMsdynUnifiedroutingsetuptrackerCreatedby = "lk_msdyn_unifiedroutingsetuptracker_createdby";
				public const string LkMsdynUnifiedroutingsetuptrackerCreatedonbehalfby = "lk_msdyn_unifiedroutingsetuptracker_createdonbehalfby";
				public const string LkMsdynUnifiedroutingsetuptrackerModifiedby = "lk_msdyn_unifiedroutingsetuptracker_modifiedby";
				public const string LkMsdynUnifiedroutingsetuptrackerModifiedonbehalfby = "lk_msdyn_unifiedroutingsetuptracker_modifiedonbehalfby";
				public const string LkMsdynUntrackedappointmentCreatedby = "lk_msdyn_untrackedappointment_createdby";
				public const string LkMsdynUntrackedappointmentCreatedonbehalfby = "lk_msdyn_untrackedappointment_createdonbehalfby";
				public const string LkMsdynUntrackedappointmentModifiedby = "lk_msdyn_untrackedappointment_modifiedby";
				public const string LkMsdynUntrackedappointmentModifiedonbehalfby = "lk_msdyn_untrackedappointment_modifiedonbehalfby";
				public const string LkMsdynUpgraderunCreatedby = "lk_msdyn_upgraderun_createdby";
				public const string LkMsdynUpgraderunCreatedonbehalfby = "lk_msdyn_upgraderun_createdonbehalfby";
				public const string LkMsdynUpgraderunModifiedby = "lk_msdyn_upgraderun_modifiedby";
				public const string LkMsdynUpgraderunModifiedonbehalfby = "lk_msdyn_upgraderun_modifiedonbehalfby";
				public const string LkMsdynUpgradestepCreatedby = "lk_msdyn_upgradestep_createdby";
				public const string LkMsdynUpgradestepCreatedonbehalfby = "lk_msdyn_upgradestep_createdonbehalfby";
				public const string LkMsdynUpgradestepModifiedby = "lk_msdyn_upgradestep_modifiedby";
				public const string LkMsdynUpgradestepModifiedonbehalfby = "lk_msdyn_upgradestep_modifiedonbehalfby";
				public const string LkMsdynUpgradeversionCreatedby = "lk_msdyn_upgradeversion_createdby";
				public const string LkMsdynUpgradeversionCreatedonbehalfby = "lk_msdyn_upgradeversion_createdonbehalfby";
				public const string LkMsdynUpgradeversionModifiedby = "lk_msdyn_upgradeversion_modifiedby";
				public const string LkMsdynUpgradeversionModifiedonbehalfby = "lk_msdyn_upgradeversion_modifiedonbehalfby";
				public const string LkMsdynUrnotificationtemplateCreatedby = "lk_msdyn_urnotificationtemplate_createdby";
				public const string LkMsdynUrnotificationtemplateCreatedonbehalfby = "lk_msdyn_urnotificationtemplate_createdonbehalfby";
				public const string LkMsdynUrnotificationtemplateModifiedby = "lk_msdyn_urnotificationtemplate_modifiedby";
				public const string LkMsdynUrnotificationtemplateModifiedonbehalfby = "lk_msdyn_urnotificationtemplate_modifiedonbehalfby";
				public const string LkMsdynUrnotificationtemplatemappingCreatedby = "lk_msdyn_urnotificationtemplatemapping_createdby";
				public const string LkMsdynUrnotificationtemplatemappingCreatedonbehalfby = "lk_msdyn_urnotificationtemplatemapping_createdonbehalfby";
				public const string LkMsdynUrnotificationtemplatemappingModifiedby = "lk_msdyn_urnotificationtemplatemapping_modifiedby";
				public const string LkMsdynUrnotificationtemplatemappingModifiedonbehalfby = "lk_msdyn_urnotificationtemplatemapping_modifiedonbehalfby";
				public const string LkMsdynUsagemetricCreatedby = "lk_msdyn_usagemetric_createdby";
				public const string LkMsdynUsagemetricCreatedonbehalfby = "lk_msdyn_usagemetric_createdonbehalfby";
				public const string LkMsdynUsagemetricModifiedby = "lk_msdyn_usagemetric_modifiedby";
				public const string LkMsdynUsagemetricModifiedonbehalfby = "lk_msdyn_usagemetric_modifiedonbehalfby";
				public const string LkMsdynUsagereportingCreatedby = "lk_msdyn_usagereporting_createdby";
				public const string LkMsdynUsagereportingCreatedonbehalfby = "lk_msdyn_usagereporting_createdonbehalfby";
				public const string LkMsdynUsagereportingModifiedby = "lk_msdyn_usagereporting_modifiedby";
				public const string LkMsdynUsagereportingModifiedonbehalfby = "lk_msdyn_usagereporting_modifiedonbehalfby";
				public const string LkMsdynUsersettingCreatedby = "lk_msdyn_usersetting_createdby";
				public const string LkMsdynUsersettingCreatedonbehalfby = "lk_msdyn_usersetting_createdonbehalfby";
				public const string LkMsdynUsersettingModifiedby = "lk_msdyn_usersetting_modifiedby";
				public const string LkMsdynUsersettingModifiedonbehalfby = "lk_msdyn_usersetting_modifiedonbehalfby";
				public const string LkMsdynVirtualtablecolumncandidateCreatedby = "lk_msdyn_virtualtablecolumncandidate_createdby";
				public const string LkMsdynVirtualtablecolumncandidateCreatedonbehalfby = "lk_msdyn_virtualtablecolumncandidate_createdonbehalfby";
				public const string LkMsdynVirtualtablecolumncandidateModifiedby = "lk_msdyn_virtualtablecolumncandidate_modifiedby";
				public const string LkMsdynVirtualtablecolumncandidateModifiedonbehalfby = "lk_msdyn_virtualtablecolumncandidate_modifiedonbehalfby";
				public const string LkMsdynVisitorjourneyCreatedby = "lk_msdyn_visitorjourney_createdby";
				public const string LkMsdynVisitorjourneyCreatedonbehalfby = "lk_msdyn_visitorjourney_createdonbehalfby";
				public const string LkMsdynVisitorjourneyModifiedby = "lk_msdyn_visitorjourney_modifiedby";
				public const string LkMsdynVisitorjourneyModifiedonbehalfby = "lk_msdyn_visitorjourney_modifiedonbehalfby";
				public const string LkMsdynVivacustomerlistCreatedby = "lk_msdyn_vivacustomerlist_createdby";
				public const string LkMsdynVivacustomerlistCreatedonbehalfby = "lk_msdyn_vivacustomerlist_createdonbehalfby";
				public const string LkMsdynVivacustomerlistModifiedby = "lk_msdyn_vivacustomerlist_modifiedby";
				public const string LkMsdynVivacustomerlistModifiedonbehalfby = "lk_msdyn_vivacustomerlist_modifiedonbehalfby";
				public const string LkMsdynVivaentitysettingCreatedby = "lk_msdyn_vivaentitysetting_createdby";
				public const string LkMsdynVivaentitysettingCreatedonbehalfby = "lk_msdyn_vivaentitysetting_createdonbehalfby";
				public const string LkMsdynVivaentitysettingModifiedby = "lk_msdyn_vivaentitysetting_modifiedby";
				public const string LkMsdynVivaentitysettingModifiedonbehalfby = "lk_msdyn_vivaentitysetting_modifiedonbehalfby";
				public const string LkMsdynVivaorgsettingCreatedby = "lk_msdyn_vivaorgsetting_createdby";
				public const string LkMsdynVivaorgsettingCreatedonbehalfby = "lk_msdyn_vivaorgsetting_createdonbehalfby";
				public const string LkMsdynVivaorgsettingModifiedby = "lk_msdyn_vivaorgsetting_modifiedby";
				public const string LkMsdynVivaorgsettingModifiedonbehalfby = "lk_msdyn_vivaorgsetting_modifiedonbehalfby";
				public const string LkMsdynVivausersettingCreatedby = "lk_msdyn_vivausersetting_createdby";
				public const string LkMsdynVivausersettingCreatedonbehalfby = "lk_msdyn_vivausersetting_createdonbehalfby";
				public const string LkMsdynVivausersettingModifiedby = "lk_msdyn_vivausersetting_modifiedby";
				public const string LkMsdynVivausersettingModifiedonbehalfby = "lk_msdyn_vivausersetting_modifiedonbehalfby";
				public const string LkMsdynWallsavedqueryCreatedby = "lk_msdyn_wallsavedquery_createdby";
				public const string LkMsdynWallsavedqueryCreatedonbehalfby = "lk_msdyn_wallsavedquery_createdonbehalfby";
				public const string LkMsdynWallsavedqueryModifiedby = "lk_msdyn_wallsavedquery_modifiedby";
				public const string LkMsdynWallsavedqueryModifiedonbehalfby = "lk_msdyn_wallsavedquery_modifiedonbehalfby";
				public const string LkMsdynWallsavedqueryusersettingsCreatedby = "lk_msdyn_wallsavedqueryusersettings_createdby";
				public const string LkMsdynWallsavedqueryusersettingsCreatedonbehalfby = "lk_msdyn_wallsavedqueryusersettings_createdonbehalfby";
				public const string LkMsdynWallsavedqueryusersettingsModifiedby = "lk_msdyn_wallsavedqueryusersettings_modifiedby";
				public const string LkMsdynWallsavedqueryusersettingsModifiedonbehalfby = "lk_msdyn_wallsavedqueryusersettings_modifiedonbehalfby";
				public const string LkMsdynWorkflowactionstatusCreatedby = "lk_msdyn_workflowactionstatus_createdby";
				public const string LkMsdynWorkflowactionstatusCreatedonbehalfby = "lk_msdyn_workflowactionstatus_createdonbehalfby";
				public const string LkMsdynWorkflowactionstatusModifiedby = "lk_msdyn_workflowactionstatus_modifiedby";
				public const string LkMsdynWorkflowactionstatusModifiedonbehalfby = "lk_msdyn_workflowactionstatus_modifiedonbehalfby";
				public const string LkMsdynWorklistviewconfigurationCreatedby = "lk_msdyn_worklistviewconfiguration_createdby";
				public const string LkMsdynWorklistviewconfigurationCreatedonbehalfby = "lk_msdyn_worklistviewconfiguration_createdonbehalfby";
				public const string LkMsdynWorklistviewconfigurationModifiedby = "lk_msdyn_worklistviewconfiguration_modifiedby";
				public const string LkMsdynWorklistviewconfigurationModifiedonbehalfby = "lk_msdyn_worklistviewconfiguration_modifiedonbehalfby";
				public const string LkMsdynWorkqueuestateCreatedby = "lk_msdyn_workqueuestate_createdby";
				public const string LkMsdynWorkqueuestateCreatedonbehalfby = "lk_msdyn_workqueuestate_createdonbehalfby";
				public const string LkMsdynWorkqueuestateModifiedby = "lk_msdyn_workqueuestate_modifiedby";
				public const string LkMsdynWorkqueuestateModifiedonbehalfby = "lk_msdyn_workqueuestate_modifiedonbehalfby";
				public const string LkMsdynWorkqueueusersettingCreatedby = "lk_msdyn_workqueueusersetting_createdby";
				public const string LkMsdynWorkqueueusersettingCreatedonbehalfby = "lk_msdyn_workqueueusersetting_createdonbehalfby";
				public const string LkMsdynWorkqueueusersettingModifiedby = "lk_msdyn_workqueueusersetting_modifiedby";
				public const string LkMsdynWorkqueueusersettingModifiedonbehalfby = "lk_msdyn_workqueueusersetting_modifiedonbehalfby";
				public const string LkMsdynceBotcontentCreatedby = "lk_msdynce_botcontent_createdby";
				public const string LkMsdynceBotcontentCreatedonbehalfby = "lk_msdynce_botcontent_createdonbehalfby";
				public const string LkMsdynceBotcontentModifiedby = "lk_msdynce_botcontent_modifiedby";
				public const string LkMsdynceBotcontentModifiedonbehalfby = "lk_msdynce_botcontent_modifiedonbehalfby";
				public const string LkMsdyncrmAddtocalendarstyleCreatedby = "lk_msdyncrm_addtocalendarstyle_createdby";
				public const string LkMsdyncrmAddtocalendarstyleCreatedonbehalfby = "lk_msdyncrm_addtocalendarstyle_createdonbehalfby";
				public const string LkMsdyncrmAddtocalendarstyleModifiedby = "lk_msdyncrm_addtocalendarstyle_modifiedby";
				public const string LkMsdyncrmAddtocalendarstyleModifiedonbehalfby = "lk_msdyncrm_addtocalendarstyle_modifiedonbehalfby";
				public const string LkMsdyncrmBasestyleCreatedby = "lk_msdyncrm_basestyle_createdby";
				public const string LkMsdyncrmBasestyleCreatedonbehalfby = "lk_msdyncrm_basestyle_createdonbehalfby";
				public const string LkMsdyncrmBasestyleModifiedby = "lk_msdyncrm_basestyle_modifiedby";
				public const string LkMsdyncrmBasestyleModifiedonbehalfby = "lk_msdyncrm_basestyle_modifiedonbehalfby";
				public const string LkMsdyncrmButtonstyleCreatedby = "lk_msdyncrm_buttonstyle_createdby";
				public const string LkMsdyncrmButtonstyleCreatedonbehalfby = "lk_msdyncrm_buttonstyle_createdonbehalfby";
				public const string LkMsdyncrmButtonstyleModifiedby = "lk_msdyncrm_buttonstyle_modifiedby";
				public const string LkMsdyncrmButtonstyleModifiedonbehalfby = "lk_msdyncrm_buttonstyle_modifiedonbehalfby";
				public const string LkMsdyncrmCodestyleCreatedby = "lk_msdyncrm_codestyle_createdby";
				public const string LkMsdyncrmCodestyleCreatedonbehalfby = "lk_msdyncrm_codestyle_createdonbehalfby";
				public const string LkMsdyncrmCodestyleModifiedby = "lk_msdyncrm_codestyle_modifiedby";
				public const string LkMsdyncrmCodestyleModifiedonbehalfby = "lk_msdyncrm_codestyle_modifiedonbehalfby";
				public const string LkMsdyncrmColumnstyleCreatedby = "lk_msdyncrm_columnstyle_createdby";
				public const string LkMsdyncrmColumnstyleCreatedonbehalfby = "lk_msdyncrm_columnstyle_createdonbehalfby";
				public const string LkMsdyncrmColumnstyleModifiedby = "lk_msdyncrm_columnstyle_modifiedby";
				public const string LkMsdyncrmColumnstyleModifiedonbehalfby = "lk_msdyncrm_columnstyle_modifiedonbehalfby";
				public const string LkMsdyncrmContentblockstyleCreatedby = "lk_msdyncrm_contentblockstyle_createdby";
				public const string LkMsdyncrmContentblockstyleCreatedonbehalfby = "lk_msdyncrm_contentblockstyle_createdonbehalfby";
				public const string LkMsdyncrmContentblockstyleModifiedby = "lk_msdyncrm_contentblockstyle_modifiedby";
				public const string LkMsdyncrmContentblockstyleModifiedonbehalfby = "lk_msdyncrm_contentblockstyle_modifiedonbehalfby";
				public const string LkMsdyncrmDividerstyleCreatedby = "lk_msdyncrm_dividerstyle_createdby";
				public const string LkMsdyncrmDividerstyleCreatedonbehalfby = "lk_msdyncrm_dividerstyle_createdonbehalfby";
				public const string LkMsdyncrmDividerstyleModifiedby = "lk_msdyncrm_dividerstyle_modifiedby";
				public const string LkMsdyncrmDividerstyleModifiedonbehalfby = "lk_msdyncrm_dividerstyle_modifiedonbehalfby";
				public const string LkMsdyncrmGeneralstylesCreatedby = "lk_msdyncrm_generalstyles_createdby";
				public const string LkMsdyncrmGeneralstylesCreatedonbehalfby = "lk_msdyncrm_generalstyles_createdonbehalfby";
				public const string LkMsdyncrmGeneralstylesModifiedby = "lk_msdyncrm_generalstyles_modifiedby";
				public const string LkMsdyncrmGeneralstylesModifiedonbehalfby = "lk_msdyncrm_generalstyles_modifiedonbehalfby";
				public const string LkMsdyncrmImagestyleCreatedby = "lk_msdyncrm_imagestyle_createdby";
				public const string LkMsdyncrmImagestyleCreatedonbehalfby = "lk_msdyncrm_imagestyle_createdonbehalfby";
				public const string LkMsdyncrmImagestyleModifiedby = "lk_msdyncrm_imagestyle_modifiedby";
				public const string LkMsdyncrmImagestyleModifiedonbehalfby = "lk_msdyncrm_imagestyle_modifiedonbehalfby";
				public const string LkMsdyncrmLayoutstyleCreatedby = "lk_msdyncrm_layoutstyle_createdby";
				public const string LkMsdyncrmLayoutstyleCreatedonbehalfby = "lk_msdyncrm_layoutstyle_createdonbehalfby";
				public const string LkMsdyncrmLayoutstyleModifiedby = "lk_msdyncrm_layoutstyle_modifiedby";
				public const string LkMsdyncrmLayoutstyleModifiedonbehalfby = "lk_msdyncrm_layoutstyle_modifiedonbehalfby";
				public const string LkMsdyncrmQrcodestyleCreatedby = "lk_msdyncrm_qrcodestyle_createdby";
				public const string LkMsdyncrmQrcodestyleCreatedonbehalfby = "lk_msdyncrm_qrcodestyle_createdonbehalfby";
				public const string LkMsdyncrmQrcodestyleModifiedby = "lk_msdyncrm_qrcodestyle_modifiedby";
				public const string LkMsdyncrmQrcodestyleModifiedonbehalfby = "lk_msdyncrm_qrcodestyle_modifiedonbehalfby";
				public const string LkMsdyncrmTextstyleCreatedby = "lk_msdyncrm_textstyle_createdby";
				public const string LkMsdyncrmTextstyleCreatedonbehalfby = "lk_msdyncrm_textstyle_createdonbehalfby";
				public const string LkMsdyncrmTextstyleModifiedby = "lk_msdyncrm_textstyle_modifiedby";
				public const string LkMsdyncrmTextstyleModifiedonbehalfby = "lk_msdyncrm_textstyle_modifiedonbehalfby";
				public const string LkMsdyncrmVideostyleCreatedby = "lk_msdyncrm_videostyle_createdby";
				public const string LkMsdyncrmVideostyleCreatedonbehalfby = "lk_msdyncrm_videostyle_createdonbehalfby";
				public const string LkMsdyncrmVideostyleModifiedby = "lk_msdyncrm_videostyle_modifiedby";
				public const string LkMsdyncrmVideostyleModifiedonbehalfby = "lk_msdyncrm_videostyle_modifiedonbehalfby";
				public const string LkMsdynmktCatalogeventstatusconfigurationCreatedby = "lk_msdynmkt_catalogeventstatusconfiguration_createdby";
				public const string LkMsdynmktCatalogeventstatusconfigurationCreatedonbehalfby = "lk_msdynmkt_catalogeventstatusconfiguration_createdonbehalfby";
				public const string LkMsdynmktCatalogeventstatusconfigurationModifiedby = "lk_msdynmkt_catalogeventstatusconfiguration_modifiedby";
				public const string LkMsdynmktCatalogeventstatusconfigurationModifiedonbehalfby = "lk_msdynmkt_catalogeventstatusconfiguration_modifiedonbehalfby";
				public const string LkMsdynmktConfigurationCreatedby = "lk_msdynmkt_configuration_createdby";
				public const string LkMsdynmktConfigurationCreatedonbehalfby = "lk_msdynmkt_configuration_createdonbehalfby";
				public const string LkMsdynmktConfigurationModifiedby = "lk_msdynmkt_configuration_modifiedby";
				public const string LkMsdynmktConfigurationModifiedonbehalfby = "lk_msdynmkt_configuration_modifiedonbehalfby";
				public const string LkMsdynmktEventmetadataCreatedby = "lk_msdynmkt_eventmetadata_createdby";
				public const string LkMsdynmktEventmetadataCreatedonbehalfby = "lk_msdynmkt_eventmetadata_createdonbehalfby";
				public const string LkMsdynmktEventmetadataModifiedby = "lk_msdynmkt_eventmetadata_modifiedby";
				public const string LkMsdynmktEventmetadataModifiedonbehalfby = "lk_msdynmkt_eventmetadata_modifiedonbehalfby";
				public const string LkMsdynmktEventmetadataSdkmessageprocessingstepCreatedby = "lk_msdynmkt_eventmetadata_sdkmessageprocessingstep_createdby";
				public const string LkMsdynmktEventmetadataSdkmessageprocessingstepCreatedonbehalfby = "lk_msdynmkt_eventmetadata_sdkmessageprocessingstep_createdonbehalfby";
				public const string LkMsdynmktEventmetadataSdkmessageprocessingstepModifiedby = "lk_msdynmkt_eventmetadata_sdkmessageprocessingstep_modifiedby";
				public const string LkMsdynmktEventmetadataSdkmessageprocessingstepModifiedonbehalfby = "lk_msdynmkt_eventmetadata_sdkmessageprocessingstep_modifiedonbehalfby";
				public const string LkMsdynmktEventparametermetadataCreatedby = "lk_msdynmkt_eventparametermetadata_createdby";
				public const string LkMsdynmktEventparametermetadataCreatedonbehalfby = "lk_msdynmkt_eventparametermetadata_createdonbehalfby";
				public const string LkMsdynmktEventparametermetadataModifiedby = "lk_msdynmkt_eventparametermetadata_modifiedby";
				public const string LkMsdynmktEventparametermetadataModifiedonbehalfby = "lk_msdynmkt_eventparametermetadata_modifiedonbehalfby";
				public const string LkMsdynmktFeatureconfigurationCreatedby = "lk_msdynmkt_featureconfiguration_createdby";
				public const string LkMsdynmktFeatureconfigurationCreatedonbehalfby = "lk_msdynmkt_featureconfiguration_createdonbehalfby";
				public const string LkMsdynmktFeatureconfigurationModifiedby = "lk_msdynmkt_featureconfiguration_modifiedby";
				public const string LkMsdynmktFeatureconfigurationModifiedonbehalfby = "lk_msdynmkt_featureconfiguration_modifiedonbehalfby";
				public const string LkMsdynmktTrackingcontextCreatedby = "lk_msdynmkt_trackingcontext_createdby";
				public const string LkMsdynmktTrackingcontextCreatedonbehalfby = "lk_msdynmkt_trackingcontext_createdonbehalfby";
				public const string LkMsdynmktTrackingcontextModifiedby = "lk_msdynmkt_trackingcontext_modifiedby";
				public const string LkMsdynmktTrackingcontextModifiedonbehalfby = "lk_msdynmkt_trackingcontext_modifiedonbehalfby";
				public const string LkMsfpAlertruleCreatedby = "lk_msfp_alertrule_createdby";
				public const string LkMsfpAlertruleCreatedonbehalfby = "lk_msfp_alertrule_createdonbehalfby";
				public const string LkMsfpAlertruleModifiedby = "lk_msfp_alertrule_modifiedby";
				public const string LkMsfpAlertruleModifiedonbehalfby = "lk_msfp_alertrule_modifiedonbehalfby";
				public const string LkMsfpEmailtemplateCreatedby = "lk_msfp_emailtemplate_createdby";
				public const string LkMsfpEmailtemplateCreatedonbehalfby = "lk_msfp_emailtemplate_createdonbehalfby";
				public const string LkMsfpEmailtemplateModifiedby = "lk_msfp_emailtemplate_modifiedby";
				public const string LkMsfpEmailtemplateModifiedonbehalfby = "lk_msfp_emailtemplate_modifiedonbehalfby";
				public const string LkMsfpFileresponseCreatedby = "lk_msfp_fileresponse_createdby";
				public const string LkMsfpFileresponseCreatedonbehalfby = "lk_msfp_fileresponse_createdonbehalfby";
				public const string LkMsfpFileresponseModifiedby = "lk_msfp_fileresponse_modifiedby";
				public const string LkMsfpFileresponseModifiedonbehalfby = "lk_msfp_fileresponse_modifiedonbehalfby";
				public const string LkMsfpLocalizedemailtemplateCreatedby = "lk_msfp_localizedemailtemplate_createdby";
				public const string LkMsfpLocalizedemailtemplateCreatedonbehalfby = "lk_msfp_localizedemailtemplate_createdonbehalfby";
				public const string LkMsfpLocalizedemailtemplateModifiedby = "lk_msfp_localizedemailtemplate_modifiedby";
				public const string LkMsfpLocalizedemailtemplateModifiedonbehalfby = "lk_msfp_localizedemailtemplate_modifiedonbehalfby";
				public const string LkMsfpProjectCreatedby = "lk_msfp_project_createdby";
				public const string LkMsfpProjectCreatedonbehalfby = "lk_msfp_project_createdonbehalfby";
				public const string LkMsfpProjectModifiedby = "lk_msfp_project_modifiedby";
				public const string LkMsfpProjectModifiedonbehalfby = "lk_msfp_project_modifiedonbehalfby";
				public const string LkMsfpQuestionCreatedby = "lk_msfp_question_createdby";
				public const string LkMsfpQuestionCreatedonbehalfby = "lk_msfp_question_createdonbehalfby";
				public const string LkMsfpQuestionModifiedby = "lk_msfp_question_modifiedby";
				public const string LkMsfpQuestionModifiedonbehalfby = "lk_msfp_question_modifiedonbehalfby";
				public const string LkMsfpQuestionresponseCreatedby = "lk_msfp_questionresponse_createdby";
				public const string LkMsfpQuestionresponseCreatedonbehalfby = "lk_msfp_questionresponse_createdonbehalfby";
				public const string LkMsfpQuestionresponseModifiedby = "lk_msfp_questionresponse_modifiedby";
				public const string LkMsfpQuestionresponseModifiedonbehalfby = "lk_msfp_questionresponse_modifiedonbehalfby";
				public const string LkMsfpSatisfactionmetricCreatedby = "lk_msfp_satisfactionmetric_createdby";
				public const string LkMsfpSatisfactionmetricCreatedonbehalfby = "lk_msfp_satisfactionmetric_createdonbehalfby";
				public const string LkMsfpSatisfactionmetricModifiedby = "lk_msfp_satisfactionmetric_modifiedby";
				public const string LkMsfpSatisfactionmetricModifiedonbehalfby = "lk_msfp_satisfactionmetric_modifiedonbehalfby";
				public const string LkMsfpSurveyCreatedby = "lk_msfp_survey_createdby";
				public const string LkMsfpSurveyCreatedonbehalfby = "lk_msfp_survey_createdonbehalfby";
				public const string LkMsfpSurveyModifiedby = "lk_msfp_survey_modifiedby";
				public const string LkMsfpSurveyModifiedonbehalfby = "lk_msfp_survey_modifiedonbehalfby";
				public const string LkMsfpSurveyreminderCreatedby = "lk_msfp_surveyreminder_createdby";
				public const string LkMsfpSurveyreminderCreatedonbehalfby = "lk_msfp_surveyreminder_createdonbehalfby";
				public const string LkMsfpSurveyreminderModifiedby = "lk_msfp_surveyreminder_modifiedby";
				public const string LkMsfpSurveyreminderModifiedonbehalfby = "lk_msfp_surveyreminder_modifiedonbehalfby";
				public const string LkMsfpUnsubscribedrecipientCreatedby = "lk_msfp_unsubscribedrecipient_createdby";
				public const string LkMsfpUnsubscribedrecipientCreatedonbehalfby = "lk_msfp_unsubscribedrecipient_createdonbehalfby";
				public const string LkMsfpUnsubscribedrecipientModifiedby = "lk_msfp_unsubscribedrecipient_modifiedby";
				public const string LkMsfpUnsubscribedrecipientModifiedonbehalfby = "lk_msfp_unsubscribedrecipient_modifiedonbehalfby";
				public const string LkNavigationsettingCreatedby = "lk_navigationsetting_createdby";
				public const string LkNavigationsettingCreatedonbehalfby = "lk_navigationsetting_createdonbehalfby";
				public const string LkNavigationsettingModifiedby = "lk_navigationsetting_modifiedby";
				public const string LkNavigationsettingModifiedonbehalfby = "lk_navigationsetting_modifiedonbehalfby";
				public const string LkNewprocessCreatedby = "lk_newprocess_createdby";
				public const string LkNewprocessCreatedonbehalfby = "lk_newprocess_createdonbehalfby";
				public const string LkNewprocessModifiedby = "lk_newprocess_modifiedby";
				public const string LkNewprocessModifiedonbehalfby = "lk_newprocess_modifiedonbehalfby";
				public const string LkOfficedocumentbaseCreatedonbehalfby = "lk_officedocumentbase_createdonbehalfby";
				public const string LkOfficedocumentbaseModifiedonbehalfby = "lk_officedocumentbase_modifiedonbehalfby";
				public const string LkOfficegraphdocumentCreatedonbehalfby = "lk_officegraphdocument_createdonbehalfby";
				public const string LkOfficegraphdocumentModifiedonbehalfby = "lk_officegraphdocument_modifiedonbehalfby";
				public const string LkOfflinecommanddefinitionCreatedby = "lk_offlinecommanddefinition_createdby";
				public const string LkOfflinecommanddefinitionCreatedonbehalfby = "lk_offlinecommanddefinition_createdonbehalfby";
				public const string LkOfflinecommanddefinitionModifiedby = "lk_offlinecommanddefinition_modifiedby";
				public const string LkOfflinecommanddefinitionModifiedonbehalfby = "lk_offlinecommanddefinition_modifiedonbehalfby";
				public const string LkOpportunityCreatedonbehalfby = "lk_opportunity_createdonbehalfby";
				public const string LkOpportunityModifiedonbehalfby = "lk_opportunity_modifiedonbehalfby";
				public const string LkOpportunitybaseCreatedby = "lk_opportunitybase_createdby";
				public const string LkOpportunitybaseModifiedby = "lk_opportunitybase_modifiedby";
				public const string LkOpportunitycloseCreatedby = "lk_opportunityclose_createdby";
				public const string LkOpportunitycloseCreatedonbehalfby = "lk_opportunityclose_createdonbehalfby";
				public const string LkOpportunitycloseModifiedby = "lk_opportunityclose_modifiedby";
				public const string LkOpportunitycloseModifiedonbehalfby = "lk_opportunityclose_modifiedonbehalfby";
				public const string LkOpportunityproductCreatedonbehalfby = "lk_opportunityproduct_createdonbehalfby";
				public const string LkOpportunityproductModifiedonbehalfby = "lk_opportunityproduct_modifiedonbehalfby";
				public const string LkOpportunityproductbaseCreatedby = "lk_opportunityproductbase_createdby";
				public const string LkOpportunityproductbaseModifiedby = "lk_opportunityproductbase_modifiedby";
				public const string LkOpportunitysalesprocessCreatedby = "lk_opportunitysalesprocess_createdby";
				public const string LkOpportunitysalesprocessCreatedonbehalfby = "lk_opportunitysalesprocess_createdonbehalfby";
				public const string LkOpportunitysalesprocessModifiedby = "lk_opportunitysalesprocess_modifiedby";
				public const string LkOpportunitysalesprocessModifiedonbehalfby = "lk_opportunitysalesprocess_modifiedonbehalfby";
				public const string LkOrdercloseCreatedby = "lk_orderclose_createdby";
				public const string LkOrdercloseCreatedonbehalfby = "lk_orderclose_createdonbehalfby";
				public const string LkOrdercloseModifiedby = "lk_orderclose_modifiedby";
				public const string LkOrdercloseModifiedonbehalfby = "lk_orderclose_modifiedonbehalfby";
				public const string LkOrganizationCreatedonbehalfby = "lk_organization_createdonbehalfby";
				public const string LkOrganizationModifiedonbehalfby = "lk_organization_modifiedonbehalfby";
				public const string LkOrganizationbaseCreatedby = "lk_organizationbase_createdby";
				public const string LkOrganizationbaseModifiedby = "lk_organizationbase_modifiedby";
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
				public const string LkPhonetocaseprocessCreatedby = "lk_phonetocaseprocess_createdby";
				public const string LkPhonetocaseprocessCreatedonbehalfby = "lk_phonetocaseprocess_createdonbehalfby";
				public const string LkPhonetocaseprocessModifiedby = "lk_phonetocaseprocess_modifiedby";
				public const string LkPhonetocaseprocessModifiedonbehalfby = "lk_phonetocaseprocess_modifiedonbehalfby";
				public const string LkPicklistmappingCreatedby = "lk_picklistmapping_createdby";
				public const string LkPicklistmappingCreatedonbehalfby = "lk_picklistmapping_createdonbehalfby";
				public const string LkPicklistmappingModifiedby = "lk_picklistmapping_modifiedby";
				public const string LkPicklistmappingModifiedonbehalfby = "lk_picklistmapping_modifiedonbehalfby";
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
				public const string LkPowerbidatasetCreatedby = "lk_powerbidataset_createdby";
				public const string LkPowerbidatasetCreatedonbehalfby = "lk_powerbidataset_createdonbehalfby";
				public const string LkPowerbidatasetModifiedby = "lk_powerbidataset_modifiedby";
				public const string LkPowerbidatasetModifiedonbehalfby = "lk_powerbidataset_modifiedonbehalfby";
				public const string LkPowerbimashupparameterCreatedby = "lk_powerbimashupparameter_createdby";
				public const string LkPowerbimashupparameterCreatedonbehalfby = "lk_powerbimashupparameter_createdonbehalfby";
				public const string LkPowerbimashupparameterModifiedby = "lk_powerbimashupparameter_modifiedby";
				public const string LkPowerbimashupparameterModifiedonbehalfby = "lk_powerbimashupparameter_modifiedonbehalfby";
				public const string LkPowerbireportCreatedby = "lk_powerbireport_createdby";
				public const string LkPowerbireportCreatedonbehalfby = "lk_powerbireport_createdonbehalfby";
				public const string LkPowerbireportModifiedby = "lk_powerbireport_modifiedby";
				public const string LkPowerbireportModifiedonbehalfby = "lk_powerbireport_modifiedonbehalfby";
				public const string LkPowerfxruleCreatedby = "lk_powerfxrule_createdby";
				public const string LkPowerfxruleCreatedonbehalfby = "lk_powerfxrule_createdonbehalfby";
				public const string LkPowerfxruleModifiedby = "lk_powerfxrule_modifiedby";
				public const string LkPowerfxruleModifiedonbehalfby = "lk_powerfxrule_modifiedonbehalfby";
				public const string LkPricelevelCreatedonbehalfby = "lk_pricelevel_createdonbehalfby";
				public const string LkPricelevelModifiedonbehalfby = "lk_pricelevel_modifiedonbehalfby";
				public const string LkPricelevelbaseCreatedby = "lk_pricelevelbase_createdby";
				public const string LkPricelevelbaseModifiedby = "lk_pricelevelbase_modifiedby";
				public const string LkPrivilegesremovalsettingCreatedby = "lk_privilegesremovalsetting_createdby";
				public const string LkPrivilegesremovalsettingCreatedonbehalfby = "lk_privilegesremovalsetting_createdonbehalfby";
				public const string LkPrivilegesremovalsettingModifiedby = "lk_privilegesremovalsetting_modifiedby";
				public const string LkPrivilegesremovalsettingModifiedonbehalfby = "lk_privilegesremovalsetting_modifiedonbehalfby";
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
				public const string LkProductCreatedonbehalfby = "lk_product_createdonbehalfby";
				public const string LkProductModifiedonbehalfby = "lk_product_modifiedonbehalfby";
				public const string LkProductAssociateCreatedby = "lk_ProductAssociate_createdby";
				public const string LkProductAssociationCreatedonbehalfby = "lk_ProductAssociation_createdonbehalfby";
				public const string LkProductAssociationModifiedby = "lk_ProductAssociation_modifiedby";
				public const string LkProductAssociationModifiedonbehalfby = "lk_ProductAssociation_modifiedonbehalfby";
				public const string LkProductbaseCreatedby = "lk_productbase_createdby";
				public const string LkProductbaseModifiedby = "lk_productbase_modifiedby";
				public const string LkProductpricelevelCreatedonbehalfby = "lk_productpricelevel_createdonbehalfby";
				public const string LkProductpricelevelModifiedonbehalfby = "lk_productpricelevel_modifiedonbehalfby";
				public const string LkProductpricelevelbaseCreatedby = "lk_productpricelevelbase_createdby";
				public const string LkProductpricelevelbaseModifiedby = "lk_productpricelevelbase_modifiedby";
				public const string LkProductSubstituteCreatedby = "lk_ProductSubstitute_createdby";
				public const string LkProductSubstituteCreatedonbehalfby = "lk_ProductSubstitute_createdonbehalfby";
				public const string LkProductSubstituteModifiedby = "lk_ProductSubstitute_modifiedby";
				public const string LkProductSubstituteModifiedonbehalfby = "lk_ProductSubstitute_modifiedonbehalfby";
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
				public const string LkQuoteCreatedonbehalfby = "lk_quote_createdonbehalfby";
				public const string LkQuoteModifiedonbehalfby = "lk_quote_modifiedonbehalfby";
				public const string LkQuotebaseCreatedby = "lk_quotebase_createdby";
				public const string LkQuotebaseModifiedby = "lk_quotebase_modifiedby";
				public const string LkQuotecloseCreatedby = "lk_quoteclose_createdby";
				public const string LkQuotecloseCreatedonbehalfby = "lk_quoteclose_createdonbehalfby";
				public const string LkQuotecloseModifiedby = "lk_quoteclose_modifiedby";
				public const string LkQuotecloseModifiedonbehalfby = "lk_quoteclose_modifiedonbehalfby";
				public const string LkQuotedetailCreatedonbehalfby = "lk_quotedetail_createdonbehalfby";
				public const string LkQuotedetailModifiedonbehalfby = "lk_quotedetail_modifiedonbehalfby";
				public const string LkQuotedetailbaseCreatedby = "lk_quotedetailbase_createdby";
				public const string LkQuotedetailbaseModifiedby = "lk_quotedetailbase_modifiedby";
				public const string LkRatingmodelCreatedby = "lk_ratingmodel_createdby";
				public const string LkRatingmodelCreatedonbehalfby = "lk_ratingmodel_createdonbehalfby";
				public const string LkRatingmodelModifiedby = "lk_ratingmodel_modifiedby";
				public const string LkRatingmodelModifiedonbehalfby = "lk_ratingmodel_modifiedonbehalfby";
				public const string LkRatingvalueCreatedby = "lk_ratingvalue_createdby";
				public const string LkRatingvalueCreatedonbehalfby = "lk_ratingvalue_createdonbehalfby";
				public const string LkRatingvalueModifiedby = "lk_ratingvalue_modifiedby";
				public const string LkRatingvalueModifiedonbehalfby = "lk_ratingvalue_modifiedonbehalfby";
				public const string LkRecommendeddocumentCreatedby = "lk_recommendeddocument_createdby";
				public const string LkRecommendeddocumentCreatedonbehalfby = "lk_recommendeddocument_createdonbehalfby";
				public const string LkRecommendeddocumentModifiedby = "lk_recommendeddocument_modifiedby";
				public const string LkRecommendeddocumentModifiedonbehalfby = "lk_recommendeddocument_modifiedonbehalfby";
				public const string LkRecurrenceruleCreatedby = "lk_recurrencerule_createdby";
				public const string LkRecurrenceruleModifiedby = "lk_recurrencerule_modifiedby";
				public const string LkRecurrencerulebaseCreatedonbehalfby = "lk_recurrencerulebase_createdonbehalfby";
				public const string LkRecurrencerulebaseModifiedonbehalfby = "lk_recurrencerulebase_modifiedonbehalfby";
				public const string LkRecurringappointmentmasterCreatedby = "lk_recurringappointmentmaster_createdby";
				public const string LkRecurringappointmentmasterCreatedonbehalfby = "lk_recurringappointmentmaster_createdonbehalfby";
				public const string LkRecurringappointmentmasterModifiedby = "lk_recurringappointmentmaster_modifiedby";
				public const string LkRecurringappointmentmasterModifiedonbehalfby = "lk_recurringappointmentmaster_modifiedonbehalfby";
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
				public const string LkReportvisibilityCreatedonbehalfby = "lk_reportvisibility_createdonbehalfby";
				public const string LkReportvisibilityModifiedonbehalfby = "lk_reportvisibility_modifiedonbehalfby";
				public const string LkReportvisibilitybaseCreatedby = "lk_reportvisibilitybase_createdby";
				public const string LkReportvisibilitybaseModifiedby = "lk_reportvisibilitybase_modifiedby";
				public const string LkResourcespecCreatedby = "lk_resourcespec_createdby";
				public const string LkResourcespecCreatedonbehalfby = "lk_resourcespec_createdonbehalfby";
				public const string LkResourcespecModifiedby = "lk_resourcespec_modifiedby";
				public const string LkResourcespecModifiedonbehalfby = "lk_resourcespec_modifiedonbehalfby";
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
				public const string LkSalesliteratureCreatedonbehalfby = "lk_salesliterature_createdonbehalfby";
				public const string LkSalesliteratureModifiedonbehalfby = "lk_salesliterature_modifiedonbehalfby";
				public const string LkSalesliteraturebaseCreatedby = "lk_salesliteraturebase_createdby";
				public const string LkSalesliteraturebaseModifiedby = "lk_salesliteraturebase_modifiedby";
				public const string LkSalesliteratureitemCreatedonbehalfby = "lk_salesliteratureitem_createdonbehalfby";
				public const string LkSalesliteratureitemModifiedonbehalfby = "lk_salesliteratureitem_modifiedonbehalfby";
				public const string LkSalesliteratureitembaseCreatedby = "lk_salesliteratureitembase_createdby";
				public const string LkSalesliteratureitembaseModifiedby = "lk_salesliteratureitembase_modifiedby";
				public const string LkSalesorderCreatedonbehalfby = "lk_salesorder_createdonbehalfby";
				public const string LkSalesorderModifiedonbehalfby = "lk_salesorder_modifiedonbehalfby";
				public const string LkSalesorderbaseCreatedby = "lk_salesorderbase_createdby";
				public const string LkSalesorderbaseModifiedby = "lk_salesorderbase_modifiedby";
				public const string LkSalesorderdetailCreatedonbehalfby = "lk_salesorderdetail_createdonbehalfby";
				public const string LkSalesorderdetailModifiedonbehalfby = "lk_salesorderdetail_modifiedonbehalfby";
				public const string LkSalesorderdetailbaseCreatedby = "lk_salesorderdetailbase_createdby";
				public const string LkSalesorderdetailbaseModifiedby = "lk_salesorderdetailbase_modifiedby";
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
				public const string LkSearchrelationshipsettingsCreatedby = "lk_searchrelationshipsettings_createdby";
				public const string LkSearchrelationshipsettingsCreatedonbehalfby = "lk_searchrelationshipsettings_createdonbehalfby";
				public const string LkSearchrelationshipsettingsModifiedby = "lk_searchrelationshipsettings_modifiedby";
				public const string LkSearchrelationshipsettingsModifiedonbehalfby = "lk_searchrelationshipsettings_modifiedonbehalfby";
				public const string LkSemiannualfiscalcalendarCreatedby = "lk_semiannualfiscalcalendar_createdby";
				public const string LkSemiannualfiscalcalendarCreatedonbehalfby = "lk_semiannualfiscalcalendar_createdonbehalfby";
				public const string LkSemiannualfiscalcalendarModifiedby = "lk_semiannualfiscalcalendar_modifiedby";
				public const string LkSemiannualfiscalcalendarModifiedonbehalfby = "lk_semiannualfiscalcalendar_modifiedonbehalfby";
				public const string LkSemiannualfiscalcalendarSalespersonid = "lk_semiannualfiscalcalendar_salespersonid";
				public const string LkServiceCreatedby = "lk_service_createdby";
				public const string LkServiceCreatedonbehalfby = "lk_service_createdonbehalfby";
				public const string LkServiceModifiedby = "lk_service_modifiedby";
				public const string LkServiceModifiedonbehalfby = "lk_service_modifiedonbehalfby";
				public const string LkServiceappointmentCreatedby = "lk_serviceappointment_createdby";
				public const string LkServiceappointmentCreatedonbehalfby = "lk_serviceappointment_createdonbehalfby";
				public const string LkServiceappointmentModifiedby = "lk_serviceappointment_modifiedby";
				public const string LkServiceappointmentModifiedonbehalfby = "lk_serviceappointment_modifiedonbehalfby";
				public const string LkServiceendpointbaseCreatedonbehalfby = "lk_serviceendpointbase_createdonbehalfby";
				public const string LkServiceendpointbaseModifiedonbehalfby = "lk_serviceendpointbase_modifiedonbehalfby";
				public const string LkServiceplanCreatedby = "lk_serviceplan_createdby";
				public const string LkServiceplanCreatedonbehalfby = "lk_serviceplan_createdonbehalfby";
				public const string LkServiceplanModifiedby = "lk_serviceplan_modifiedby";
				public const string LkServiceplanModifiedonbehalfby = "lk_serviceplan_modifiedonbehalfby";
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
				public const string LkSharepointsitebaseCreatedby = "lk_sharepointsitebase_createdby";
				public const string LkSharepointsitebaseCreatedonbehalfby = "lk_sharepointsitebase_createdonbehalfby";
				public const string LkSharepointsitebaseModifiedby = "lk_sharepointsitebase_modifiedby";
				public const string LkSharepointsitebaseModifiedonbehalfby = "lk_sharepointsitebase_modifiedonbehalfby";
				public const string LkSimilarityruleCreatedonbehalfby = "lk_similarityrule_createdonbehalfby";
				public const string LkSimilarityruleModifiedonbehalfby = "lk_similarityrule_modifiedonbehalfby";
				public const string LkSiteCreatedonbehalfby = "lk_site_createdonbehalfby";
				public const string LkSiteModifiedonbehalfby = "lk_site_modifiedonbehalfby";
				public const string LkSitebaseCreatedby = "lk_sitebase_createdby";
				public const string LkSitebaseModifiedby = "lk_sitebase_modifiedby";
				public const string LkSiteMapCreatedby = "lk_SiteMap_createdby";
				public const string LkSiteMapCreatedonbehalfby = "lk_SiteMap_createdonbehalfby";
				public const string LkSiteMapModifiedby = "lk_SiteMap_modifiedby";
				public const string LkSiteMapModifiedonbehalfby = "lk_SiteMap_modifiedonbehalfby";
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
				public const string LkTopicmodelCreatedby = "lk_topicmodel_createdby";
				public const string LkTopicmodelCreatedonbehalfby = "lk_topicmodel_createdonbehalfby";
				public const string LkTopicmodelModifiedby = "lk_topicmodel_modifiedby";
				public const string LkTopicmodelModifiedonbehalfby = "lk_topicmodel_modifiedonbehalfby";
				public const string LkTopicmodelexecutionhistoryCreatedby = "lk_topicmodelexecutionhistory_createdby";
				public const string LkTopicmodelexecutionhistoryCreatedonbehalfby = "lk_topicmodelexecutionhistory_createdonbehalfby";
				public const string LkTopicmodelexecutionhistoryModifiedby = "lk_topicmodelexecutionhistory_modifiedby";
				public const string LkTopicmodelexecutionhistoryModifiedonbehalfby = "lk_topicmodelexecutionhistory_modifiedonbehalfby";
				public const string LkTracelogCreatedby = "lk_tracelog_createdby";
				public const string LkTracelogCreatedonbehalfby = "lk_tracelog_createdonbehalfby";
				public const string LkTracelogModifiedby = "lk_tracelog_modifiedby";
				public const string LkTracelogModifiedonbehalfby = "lk_tracelog_modifiedonbehalfby";
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
				public const string LkUntrackedemailCreatedby = "lk_untrackedemail_createdby";
				public const string LkUntrackedemailCreatedonbehalfby = "lk_untrackedemail_createdonbehalfby";
				public const string LkUntrackedemailModifiedby = "lk_untrackedemail_modifiedby";
				public const string LkUntrackedemailModifiedonbehalfby = "lk_untrackedemail_modifiedonbehalfby";
				public const string LkUomCreatedonbehalfby = "lk_uom_createdonbehalfby";
				public const string LkUomModifiedonbehalfby = "lk_uom_modifiedonbehalfby";
				public const string LkUombaseCreatedby = "lk_uombase_createdby";
				public const string LkUombaseModifiedby = "lk_uombase_modifiedby";
				public const string LkUomscheduleCreatedonbehalfby = "lk_uomschedule_createdonbehalfby";
				public const string LkUomscheduleModifiedonbehalfby = "lk_uomschedule_modifiedonbehalfby";
				public const string LkUomschedulebaseCreatedby = "lk_uomschedulebase_createdby";
				public const string LkUomschedulebaseModifiedby = "lk_uomschedulebase_modifiedby";
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
				public const string MailboxRegardingSystemuser = "mailbox_regarding_systemuser";
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
				public const string MsdynCustomeremailcommunicationToUserId = "msdyn_customeremailcommunication_ToUserId";
				public const string MsdynMsdynForecastOwnerid = "msdyn_msdyn_forecast_ownerid";
				public const string MsdynMsdynShareasconfigurationSharedbyuserid = "msdyn_msdyn_shareasconfiguration_sharedbyuserid";
				public const string MsdynMsdynShareasconfigurationSharedwithuserid = "msdyn_msdyn_shareasconfiguration_sharedwithuserid";
				public const string MsdynOcliveworkitemSystemuserCreatedby = "msdyn_ocliveworkitem_systemuser_createdby";
				public const string MsdynOcliveworkitemSystemuserCreatedonbehalfby = "msdyn_ocliveworkitem_systemuser_createdonbehalfby";
				public const string MsdynOcliveworkitemSystemuserModifiedby = "msdyn_ocliveworkitem_systemuser_modifiedby";
				public const string MsdynOcliveworkitemSystemuserModifiedonbehalfby = "msdyn_ocliveworkitem_systemuser_modifiedonbehalfby";
				public const string MsdynOcliveworkitemSystemuserOwninguser = "msdyn_ocliveworkitem_systemuser_owninguser";
				public const string MsdynOcliveworkitemcharacteristicSkilladdedby = "msdyn_ocliveworkitemcharacteristic_skilladdedby";
				public const string MsdynOcsessionSystemuserCreatedby = "msdyn_ocsession_systemuser_createdby";
				public const string MsdynOcsessionSystemuserCreatedonbehalfby = "msdyn_ocsession_systemuser_createdonbehalfby";
				public const string MsdynOcsessionSystemuserModifiedby = "msdyn_ocsession_systemuser_modifiedby";
				public const string MsdynOcsessionSystemuserModifiedonbehalfby = "msdyn_ocsession_systemuser_modifiedonbehalfby";
				public const string MsdynOcsessionSystemuserOwninguser = "msdyn_ocsession_systemuser_owninguser";
				public const string MsdynSystemuserMsdynAgentstatushistoryAgentid = "msdyn_systemuser_msdyn_agentstatushistory_Agentid";
				public const string MsdynSystemuserMsdynDataanalyticsworkspaceConfiguredby = "msdyn_systemuser_msdyn_dataanalyticsworkspace_configuredby";
				public const string MsdynSystemuserMsdynDuplicateleadmappingDismissedBy = "msdyn_systemuser_msdyn_duplicateleadmapping_DismissedBy";
				public const string MsdynSystemuserMsdynLiveconversation = "msdyn_systemuser_msdyn_liveconversation";
				public const string MsdynSystemuserMsdynLiveworkstreamMsdynBotUser = "msdyn_systemuser_msdyn_liveworkstream_msdyn_bot_user";
				public const string MsdynSystemuserMsdynMsdynAssignmentmapSystemuserid = "msdyn_systemuser_msdyn_msdyn_assignmentmap_systemuserid";
				public const string MsdynSystemuserMsdynOcliveworkitemActiveagentid = "msdyn_systemuser_msdyn_ocliveworkitem_activeagentid";
				public const string MsdynSystemuserMsdynOcliveworkitemparticipantAgentid = "msdyn_systemuser_msdyn_ocliveworkitemparticipant_agentid";
				public const string MsdynSystemuserMsdynOcsitrainingdataApprovedby = "msdyn_systemuser_msdyn_ocsitrainingdata_approvedby";
				public const string MsdynSystemuserMsdynSalesroutingrunOwnerassigned = "msdyn_systemuser_msdyn_salesroutingrun_ownerassigned";
				public const string MsdynSystemuserMsdynSalesroutingrunPreviousowner = "msdyn_systemuser_msdyn_salesroutingrun_previousowner";
				public const string MsdynSystemuserMsdynSessionparticipantAgentid = "msdyn_systemuser_msdyn_sessionparticipant_agentid";
				public const string MsdynSystemuserMsdynSwarmparticipantUserid = "msdyn_systemuser_msdyn_swarmparticipant_userid";
				public const string MsdynSystemuserMsdynUnifiedroutingrunAssignedagent = "msdyn_systemuser_msdyn_unifiedroutingrun_assignedagent";
				public const string MsdynSystemuserOcruleitem = "msdyn_systemuser_ocruleitem";
				public const string MsdynSystemuserWallsavedqueryusersettingsUserid = "msdyn_systemuser_wallsavedqueryusersettings_userid";
				public const string MsfpAlertSystemuserCreatedby = "msfp_alert_systemuser_createdby";
				public const string MsfpAlertSystemuserCreatedonbehalfby = "msfp_alert_systemuser_createdonbehalfby";
				public const string MsfpAlertSystemuserModifiedby = "msfp_alert_systemuser_modifiedby";
				public const string MsfpAlertSystemuserModifiedonbehalfby = "msfp_alert_systemuser_modifiedonbehalfby";
				public const string MsfpAlertSystemuserOwninguser = "msfp_alert_systemuser_owninguser";
				public const string MsfpSurveyinviteSystemuserCreatedby = "msfp_surveyinvite_systemuser_createdby";
				public const string MsfpSurveyinviteSystemuserCreatedonbehalfby = "msfp_surveyinvite_systemuser_createdonbehalfby";
				public const string MsfpSurveyinviteSystemuserModifiedby = "msfp_surveyinvite_systemuser_modifiedby";
				public const string MsfpSurveyinviteSystemuserModifiedonbehalfby = "msfp_surveyinvite_systemuser_modifiedonbehalfby";
				public const string MsfpSurveyinviteSystemuserOwninguser = "msfp_surveyinvite_systemuser_owninguser";
				public const string MsfpSurveyresponseSystemuserCreatedby = "msfp_surveyresponse_systemuser_createdby";
				public const string MsfpSurveyresponseSystemuserCreatedonbehalfby = "msfp_surveyresponse_systemuser_createdonbehalfby";
				public const string MsfpSurveyresponseSystemuserModifiedby = "msfp_surveyresponse_systemuser_modifiedby";
				public const string MsfpSurveyresponseSystemuserModifiedonbehalfby = "msfp_surveyresponse_systemuser_modifiedonbehalfby";
				public const string MsfpSurveyresponseSystemuserOwninguser = "msfp_surveyresponse_systemuser_owninguser";
				public const string MsfpSystemuserMsfpSurveyPublishedby = "msfp_systemuser_msfp_survey_publishedby";
				public const string MultientitysearchCreatedby = "multientitysearch_createdby";
				public const string MultientitysearchCreatedonbehalfby = "multientitysearch_createdonbehalfby";
				public const string MultientitysearchModifiedby = "multientitysearch_modifiedby";
				public const string MultientitysearchModifiedonbehalfby = "multientitysearch_modifiedonbehalfby";
				public const string OpportunityOwningUser = "opportunity_owning_user";
				public const string OwnerMappingSystemUser = "OwnerMapping_SystemUser";
				public const string OwningUserDynamicpropertyinsatance = "OwningUser_Dynamicpropertyinsatance";
				public const string QueuePrimaryUser = "queue_primary_user";
				public const string SocialProfileOwningUser = "socialProfile_owning_user";
				public const string SupPrincipalidSystemuser = "sup_principalid_systemuser";
				public const string SystemUserAccounts = "system_user_accounts";
				public const string SystemUserActivityParties = "system_user_activity_parties";
				public const string SystemUserAsyncoperation = "system_user_asyncoperation";
				public const string SystemUserContacts = "system_user_contacts";
				public const string SystemUserEmailTemplates = "system_user_email_templates";
				public const string SystemUserIncidents = "system_user_incidents";
				public const string SystemUserInvoicedetail = "system_user_invoicedetail";
				public const string SystemUserInvoices = "system_user_invoices";
				public const string SystemUserOrders = "system_user_orders";
				public const string SystemUserQuotas = "system_user_quotas";
				public const string SystemUserQuotedetail = "system_user_quotedetail";
				public const string SystemUserQuotes = "system_user_quotes";
				public const string SystemUserSalesLiterature = "system_user_sales_literature";
				public const string SystemUserSalesorderdetail = "system_user_salesorderdetail";
				public const string SystemUserServiceAppointments = "system_user_service_appointments";
				public const string SystemUserServiceContracts = "system_user_service_contracts";
				public const string SystemUserTerritories = "system_user_territories";
				public const string SystemUserWorkflow = "system_user_workflow";
				public const string SystemuserAppusersettingUserId = "systemuser_appusersetting_userId";
				public const string SystemUserAsyncOperations = "SystemUser_AsyncOperations";
				public const string SystemuserBookableresourceUserId = "systemuser_bookableresource_UserId";
				public const string SystemuserBotPublishedby = "systemuser_bot_publishedby";
				public const string SystemUserBulkDeleteFailures = "SystemUser_BulkDeleteFailures";
				public const string SystemUserCampaigns = "SystemUser_Campaigns";
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
				public const string SystemuserPosts = "systemuser_Posts";
				public const string SystemuserPrincipalobjectattributeaccess = "systemuser_principalobjectattributeaccess";
				public const string SystemuserPrincipalobjectattributeaccessPrincipalid = "systemuser_principalobjectattributeaccess_principalid";
				public const string SystemUserProcessSessions = "SystemUser_ProcessSessions";
				public const string SystemuserResources = "systemuser_resources";
				public const string SystemUserSyncError = "SystemUser_SyncError";
				public const string SystemUserSyncErrors = "SystemUser_SyncErrors";
				public const string SystemuserUsermobileofflineprofilemembershipSystemUserId = "systemuser_usermobileofflineprofilemembership_SystemUserId";
				public const string SystemuserbusinessunitentitymapSystemuseridSystemuser = "systemuserbusinessunitentitymap_systemuserid_systemuser";
				public const string TeamsChatActivityLinkrecordSystemUser = "teams_chat_activity_linkrecord_systemUser";
				public const string TeamsChatActivityUnlinkrecordSystemUser = "teams_chat_activity_unlinkrecord_systemUser";
				public const string UserAccounts = "user_accounts";
				public const string UserActivity = "user_activity";
				public const string UserActivityfileattachment = "user_activityfileattachment";
				public const string UserActivitymonitor = "user_activitymonitor";
				public const string UserAdminsettingsentity = "user_adminsettingsentity";
				public const string UserAppnotification = "user_appnotification";
				public const string UserAppointment = "user_appointment";
				public const string UserBookableresource = "user_bookableresource";
				public const string UserBookableresourcebooking = "user_bookableresourcebooking";
				public const string UserBookableresourcebookingexchangesyncidmapping = "user_bookableresourcebookingexchangesyncidmapping";
				public const string UserBookableresourcebookingheader = "user_bookableresourcebookingheader";
				public const string UserBookableresourcecategory = "user_bookableresourcecategory";
				public const string UserBookableresourcecategoryassn = "user_bookableresourcecategoryassn";
				public const string UserBookableresourcecharacteristic = "user_bookableresourcecharacteristic";
				public const string UserBookableresourcegroup = "user_bookableresourcegroup";
				public const string UserBookingstatus = "user_bookingstatus";
				public const string UserBot = "user_bot";
				public const string UserBotcomponent = "user_botcomponent";
				public const string UserBulkOperation = "user_BulkOperation";
				public const string UserBulkoperationlog = "user_bulkoperationlog";
				public const string UserCampaignactivity = "user_campaignactivity";
				public const string UserCampaignresponse = "user_campaignresponse";
				public const string UserCanvasappextendedmetadata = "user_canvasappextendedmetadata";
				public const string UserCard = "user_card";
				public const string UserChannelaccessprofile = "user_channelaccessprofile";
				public const string UserCharacteristic = "user_characteristic";
				public const string UserComment = "user_comment";
				public const string UserConnectionreference = "user_connectionreference";
				public const string UserConnector = "user_connector";
				public const string UserContractdetail = "user_contractdetail";
				public const string UserConversationtranscript = "user_conversationtranscript";
				public const string UserConvertrule = "user_convertrule";
				public const string UserCustomapi = "user_customapi";
				public const string UserCustomapirequestparameter = "user_customapirequestparameter";
				public const string UserCustomapiresponseproperty = "user_customapiresponseproperty";
				public const string UserCustomerOpportunityRoles = "user_customer_opportunity_roles";
				public const string UserCustomerRelationship = "user_customer_relationship";
				public const string UserDatalakefolder = "user_datalakefolder";
				public const string UserDatalakefolderpermission = "user_datalakefolderpermission";
				public const string UserDesktopflowbinary = "user_desktopflowbinary";
				public const string UserDesktopflowmodule = "user_desktopflowmodule";
				public const string UserEc4uAcquirelegalbasis = "user_ec4u_acquirelegalbasis";
				public const string UserEc4uGdprProtocol = "user_ec4u_gdpr_protocol";
				public const string UserEc4uGdprProtocolDetail = "user_ec4u_gdpr_protocol_detail";
				public const string UserEc4uGdprReport = "user_ec4u_gdpr_report";
				public const string UserEc4uGdprRequest = "user_ec4u_gdpr_request";
				public const string UserEc4uLegalbasis = "user_ec4u_legalbasis";
				public const string UserEmail = "user_email";
				public const string UserEntitlement = "user_entitlement";
				public const string UserEntitlementchannel = "user_entitlementchannel";
				public const string UserEntitlemententityallocationtypemapping = "user_entitlemententityallocationtypemapping";
				public const string UserEnvironmentvariabledefinition = "user_environmentvariabledefinition";
				public const string UserEnvironmentvariablevalue = "user_environmentvariablevalue";
				public const string UserExchangesyncidmapping = "user_exchangesyncidmapping";
				public const string UserExportsolutionupload = "user_exportsolutionupload";
				public const string UserExternalparty = "user_externalparty";
				public const string UserFax = "user_fax";
				public const string UserFeaturecontrolsetting = "user_featurecontrolsetting";
				public const string UserFlowmachine = "user_flowmachine";
				public const string UserFlowmachinegroup = "user_flowmachinegroup";
				public const string UserFlowmachineimage = "user_flowmachineimage";
				public const string UserFlowmachineimageversion = "user_flowmachineimageversion";
				public const string UserFlowmachinenetwork = "user_flowmachinenetwork";
				public const string UserFlowsession = "user_flowsession";
				public const string UserGoal = "user_goal";
				public const string UserGoalGoalowner = "user_goal_goalowner";
				public const string UserIncidentresolution = "user_incidentresolution";
				public const string UserInteractionforemail = "user_interactionforemail";
				public const string UserInvoicedetail = "user_invoicedetail";
				public const string UserKeyvaultreference = "user_keyvaultreference";
				public const string UserKnowledgearticle = "user_knowledgearticle";
				public const string UserKnowledgearticleincident = "user_knowledgearticleincident";
				public const string UserLetter = "user_letter";
				public const string UserList = "user_list";
				public const string UserListoperation = "user_listoperation";
				public const string UserMailbox = "user_mailbox";
				public const string UserManagedidentity = "user_managedidentity";
				public const string UserMsdynActioncardregarding = "user_msdyn_actioncardregarding";
				public const string UserMsdynActioncardrolesetting = "user_msdyn_actioncardrolesetting";
				public const string UserMsdynAdminappstate = "user_msdyn_adminappstate";
				public const string UserMsdynAgentstatushistory = "user_msdyn_agentstatushistory";
				public const string UserMsdynAibdataset = "user_msdyn_aibdataset";
				public const string UserMsdynAibdatasetfile = "user_msdyn_aibdatasetfile";
				public const string UserMsdynAibdatasetrecord = "user_msdyn_aibdatasetrecord";
				public const string UserMsdynAibdatasetscontainer = "user_msdyn_aibdatasetscontainer";
				public const string UserMsdynAibfeedbackloop = "user_msdyn_aibfeedbackloop";
				public const string UserMsdynAibfile = "user_msdyn_aibfile";
				public const string UserMsdynAibfileattacheddata = "user_msdyn_aibfileattacheddata";
				public const string UserMsdynAicontactsuggestion = "user_msdyn_aicontactsuggestion";
				public const string UserMsdynAifptrainingdocument = "user_msdyn_aifptrainingdocument";
				public const string UserMsdynAimodel = "user_msdyn_aimodel";
				public const string UserMsdynAiodimage = "user_msdyn_aiodimage";
				public const string UserMsdynAiodlabel = "user_msdyn_aiodlabel";
				public const string UserMsdynAiodtrainingboundingbox = "user_msdyn_aiodtrainingboundingbox";
				public const string UserMsdynAiodtrainingimage = "user_msdyn_aiodtrainingimage";
				public const string UserMsdynAitemplate = "user_msdyn_aitemplate";
				public const string UserMsdynAnalysiscomponent = "user_msdyn_analysiscomponent";
				public const string UserMsdynAnalysisjob = "user_msdyn_analysisjob";
				public const string UserMsdynAnalysisresult = "user_msdyn_analysisresult";
				public const string UserMsdynAnalysisresultdetail = "user_msdyn_analysisresultdetail";
				public const string UserMsdynAnalytics = "user_msdyn_analytics";
				public const string UserMsdynAnalyticsadminsettings = "user_msdyn_analyticsadminsettings";
				public const string UserMsdynAnalyticsforcs = "user_msdyn_analyticsforcs";
				public const string UserMsdynAppconfiguration = "user_msdyn_appconfiguration";
				public const string UserMsdynApplicationextension = "user_msdyn_applicationextension";
				public const string UserMsdynApplicationtabtemplate = "user_msdyn_applicationtabtemplate";
				public const string UserMsdynAssetcategorytemplateassociation = "user_msdyn_assetcategorytemplateassociation";
				public const string UserMsdynAssettemplateassociation = "user_msdyn_assettemplateassociation";
				public const string UserMsdynAssignmentconfiguration = "user_msdyn_assignmentconfiguration";
				public const string UserMsdynAssignmentconfigurationstep = "user_msdyn_assignmentconfigurationstep";
				public const string UserMsdynAssignmentmap = "user_msdyn_assignmentmap";
				public const string UserMsdynAssignmentrule = "user_msdyn_assignmentrule";
				public const string UserMsdynAttribute = "user_msdyn_attribute";
				public const string UserMsdynAttributevalue = "user_msdyn_attributevalue";
				public const string UserMsdynAuthenticationsettings = "user_msdyn_authenticationsettings";
				public const string UserMsdynAuthsettingsentry = "user_msdyn_authsettingsentry";
				public const string UserMsdynAutocapturerule = "user_msdyn_autocapturerule";
				public const string UserMsdynAutocapturesettings = "user_msdyn_autocapturesettings";
				public const string UserMsdynBookableresourcecapacityprofile = "user_msdyn_bookableresourcecapacityprofile";
				public const string UserMsdynCallablecontext = "user_msdyn_callablecontext";
				public const string UserMsdynCapacityprofile = "user_msdyn_capacityprofile";
				public const string UserMsdynCdsentityengagementctx = "user_msdyn_cdsentityengagementctx";
				public const string UserMsdynChanneldefinition = "user_msdyn_channeldefinition";
				public const string UserMsdynChanneldefinitionconsent = "user_msdyn_channeldefinitionconsent";
				public const string UserMsdynChanneldefinitionlocale = "user_msdyn_channeldefinitionlocale";
				public const string UserMsdynChannelinstance = "user_msdyn_channelinstance";
				public const string UserMsdynChannelinstanceaccount = "user_msdyn_channelinstanceaccount";
				public const string UserMsdynChannelmessagepart = "user_msdyn_channelmessagepart";
				public const string UserMsdynChannelprovider = "user_msdyn_channelprovider";
				public const string UserMsdynConsumingapplication = "user_msdyn_consumingapplication";
				public const string UserMsdynContactsuggestionrule = "user_msdyn_contactsuggestionrule";
				public const string UserMsdynContactsuggestionruleset = "user_msdyn_contactsuggestionruleset";
				public const string UserMsdynConversationaction = "user_msdyn_conversationaction";
				public const string UserMsdynConversationactionlocale = "user_msdyn_conversationactionlocale";
				public const string UserMsdynConversationdata = "user_msdyn_conversationdata";
				public const string UserMsdynCrmconnection = "user_msdyn_crmconnection";
				public const string UserMsdynCsadminconfig = "user_msdyn_csadminconfig";
				public const string UserMsdynCustomapirulesetconfiguration = "user_msdyn_customapirulesetconfiguration";
				public const string UserMsdynCustomcontrolextendedsettings = "user_msdyn_customcontrolextendedsettings";
				public const string UserMsdynCustomerasset = "user_msdyn_customerasset";
				public const string UserMsdynCustomerassetattachment = "user_msdyn_customerassetattachment";
				public const string UserMsdynCustomerassetcategory = "user_msdyn_customerassetcategory";
				public const string UserMsdynDataanalyticscustomizedreport = "user_msdyn_dataanalyticscustomizedreport";
				public const string UserMsdynDataanalyticsdataset = "user_msdyn_dataanalyticsdataset";
				public const string UserMsdynDataanalyticsreport = "user_msdyn_dataanalyticsreport";
				public const string UserMsdynDataanalyticsworkspace = "user_msdyn_dataanalyticsworkspace";
				public const string UserMsdynDataflow = "user_msdyn_dataflow";
				public const string UserMsdynDataflowrefreshhistory = "user_msdyn_dataflowrefreshhistory";
				public const string UserMsdynDealmanageraccess = "user_msdyn_dealmanageraccess";
				public const string UserMsdynDealmanagersettings = "user_msdyn_dealmanagersettings";
				public const string UserMsdynDecisioncontract = "user_msdyn_decisioncontract";
				public const string UserMsdynDecisionruleset = "user_msdyn_decisionruleset";
				public const string UserMsdynDefextendedchannelinstance = "user_msdyn_defextendedchannelinstance";
				public const string UserMsdynDefextendedchannelinstanceaccount = "user_msdyn_defextendedchannelinstanceaccount";
				public const string UserMsdynDuplicateleadmapping = "user_msdyn_duplicateleadmapping";
				public const string UserMsdynEffortpredictionresult = "user_msdyn_effortpredictionresult";
				public const string UserMsdynEntityconfig = "user_msdyn_entityconfig";
				public const string UserMsdynEntitylinkchatconfiguration = "user_msdyn_entitylinkchatconfiguration";
				public const string UserMsdynEntityrankingrule = "user_msdyn_entityrankingrule";
				public const string UserMsdynEntityrefreshhistory = "user_msdyn_entityrefreshhistory";
				public const string UserMsdynEntityroutingconfiguration = "user_msdyn_entityroutingconfiguration";
				public const string UserMsdynExtendedusersetting = "user_msdyn_extendedusersetting";
				public const string UserMsdynFederatedarticle = "user_msdyn_federatedarticle";
				public const string UserMsdynFlowcardtype = "user_msdyn_flowcardtype";
				public const string UserMsdynForecastconfiguration = "user_msdyn_forecastconfiguration";
				public const string UserMsdynForecastdefinition = "user_msdyn_forecastdefinition";
				public const string UserMsdynForecastinstance = "user_msdyn_forecastinstance";
				public const string UserMsdynForecastrecurrence = "user_msdyn_forecastrecurrence";
				public const string UserMsdynFunctionallocation = "user_msdyn_functionallocation";
				public const string UserMsdynGdprdata = "user_msdyn_gdprdata";
				public const string UserMsdynIcebreakersconfig = "user_msdyn_icebreakersconfig";
				public const string UserMsdynIermlmodel = "user_msdyn_iermlmodel";
				public const string UserMsdynIermltraining = "user_msdyn_iermltraining";
				public const string UserMsdynIntegratedsearchprovider = "user_msdyn_integratedsearchprovider";
				public const string UserMsdynIotalert = "user_msdyn_iotalert";
				public const string UserMsdynIotdevice = "user_msdyn_iotdevice";
				public const string UserMsdynIotdevicecategory = "user_msdyn_iotdevicecategory";
				public const string UserMsdynIotdevicecommand = "user_msdyn_iotdevicecommand";
				public const string UserMsdynIotdevicecommanddefinition = "user_msdyn_iotdevicecommanddefinition";
				public const string UserMsdynIotdevicedatahistory = "user_msdyn_iotdevicedatahistory";
				public const string UserMsdynIotdeviceproperty = "user_msdyn_iotdeviceproperty";
				public const string UserMsdynIotdeviceregistrationhistory = "user_msdyn_iotdeviceregistrationhistory";
				public const string UserMsdynIotdevicevisualizationconfiguration = "user_msdyn_iotdevicevisualizationconfiguration";
				public const string UserMsdynIotfieldmapping = "user_msdyn_iotfieldmapping";
				public const string UserMsdynIotpropertydefinition = "user_msdyn_iotpropertydefinition";
				public const string UserMsdynIotprovider = "user_msdyn_iotprovider";
				public const string UserMsdynIotproviderinstance = "user_msdyn_iotproviderinstance";
				public const string UserMsdynIotsettings = "user_msdyn_iotsettings";
				public const string UserMsdynKalanguagesetting = "user_msdyn_kalanguagesetting";
				public const string UserMsdynKbattachment = "user_msdyn_kbattachment";
				public const string UserMsdynKmfederatedsearchconfig = "user_msdyn_kmfederatedsearchconfig";
				public const string UserMsdynKnowledgearticleimage = "user_msdyn_knowledgearticleimage";
				public const string UserMsdynKnowledgearticletemplate = "user_msdyn_knowledgearticletemplate";
				public const string UserMsdynKnowledgeinteractioninsight = "user_msdyn_knowledgeinteractioninsight";
				public const string UserMsdynKnowledgemanagementsetting = "user_msdyn_knowledgemanagementsetting";
				public const string UserMsdynKnowledgepersonalfilter = "user_msdyn_knowledgepersonalfilter";
				public const string UserMsdynKnowledgesearchfilter = "user_msdyn_knowledgesearchfilter";
				public const string UserMsdynKnowledgesearchinsight = "user_msdyn_knowledgesearchinsight";
				public const string UserMsdynKpieventdata = "user_msdyn_kpieventdata";
				public const string UserMsdynKpieventdefinition = "user_msdyn_kpieventdefinition";
				public const string UserMsdynLeadmodelconfig = "user_msdyn_leadmodelconfig";
				public const string UserMsdynLiveconversation = "user_msdyn_liveconversation";
				public const string UserMsdynLiveworkitemevent = "user_msdyn_liveworkitemevent";
				public const string UserMsdynLiveworkstream = "user_msdyn_liveworkstream";
				public const string UserMsdynLiveworkstreamcapacityprofile = "user_msdyn_liveworkstreamcapacityprofile";
				public const string UserMsdynMacrosession = "user_msdyn_macrosession";
				public const string UserMsdynMasterentityroutingconfiguration = "user_msdyn_masterentityroutingconfiguration";
				public const string UserMsdynMigrationtracker = "user_msdyn_migrationtracker";
				public const string UserMsdynModelpreviewstatus = "user_msdyn_modelpreviewstatus";
				public const string UserMsdynMsteamssetting = "user_msdyn_msteamssetting";
				public const string UserMsdynNotesanalysisconfig = "user_msdyn_notesanalysisconfig";
				public const string UserMsdynNotificationfield = "user_msdyn_notificationfield";
				public const string UserMsdynNotificationtemplate = "user_msdyn_notificationtemplate";
				public const string UserMsdynOcGeolocationprovider = "user_msdyn_oc_geolocationprovider";
				public const string UserMsdynOcautoblockrule = "user_msdyn_ocautoblockrule";
				public const string UserMsdynOcbotchannelregistration = "user_msdyn_ocbotchannelregistration";
				public const string UserMsdynOcchannelapiconversationprivilege = "user_msdyn_occhannelapiconversationprivilege";
				public const string UserMsdynOcchannelapimessageprivilege = "user_msdyn_occhannelapimessageprivilege";
				public const string UserMsdynOcchannelapimethodmapping = "user_msdyn_occhannelapimethodmapping";
				public const string UserMsdynOcflaggedspam = "user_msdyn_ocflaggedspam";
				public const string UserMsdynOclanguage = "user_msdyn_oclanguage";
				public const string UserMsdynOcliveworkitemcapacityprofile = "user_msdyn_ocliveworkitemcapacityprofile";
				public const string UserMsdynOcliveworkitemcharacteristic = "user_msdyn_ocliveworkitemcharacteristic";
				public const string UserMsdynOcliveworkitemcontextitem = "user_msdyn_ocliveworkitemcontextitem";
				public const string UserMsdynOcliveworkitemparticipant = "user_msdyn_ocliveworkitemparticipant";
				public const string UserMsdynOcliveworkitemsentiment = "user_msdyn_ocliveworkitemsentiment";
				public const string UserMsdynOcliveworkstreamcontextvariable = "user_msdyn_ocliveworkstreamcontextvariable";
				public const string UserMsdynOcpaymentprofile = "user_msdyn_ocpaymentprofile";
				public const string UserMsdynOcprovisioningstate = "user_msdyn_ocprovisioningstate";
				public const string UserMsdynOcrecording = "user_msdyn_ocrecording";
				public const string UserMsdynOcrequest = "user_msdyn_ocrequest";
				public const string UserMsdynOcrichobject = "user_msdyn_ocrichobject";
				public const string UserMsdynOcrichobjectmap = "user_msdyn_ocrichobjectmap";
				public const string UserMsdynOcruleitem = "user_msdyn_ocruleitem";
				public const string UserMsdynOcsentimentdailytopic = "user_msdyn_ocsentimentdailytopic";
				public const string UserMsdynOcsentimentdailytopickeyword = "user_msdyn_ocsentimentdailytopickeyword";
				public const string UserMsdynOcsentimentdailytopictrending = "user_msdyn_ocsentimentdailytopictrending";
				public const string UserMsdynOcsessioncharacteristic = "user_msdyn_ocsessioncharacteristic";
				public const string UserMsdynOcsessionparticipantevent = "user_msdyn_ocsessionparticipantevent";
				public const string UserMsdynOcsessionsentiment = "user_msdyn_ocsessionsentiment";
				public const string UserMsdynOcsimltraining = "user_msdyn_ocsimltraining";
				public const string UserMsdynOcsitdimportconfig = "user_msdyn_ocsitdimportconfig";
				public const string UserMsdynOcsitdskill = "user_msdyn_ocsitdskill";
				public const string UserMsdynOcsitrainingdata = "user_msdyn_ocsitrainingdata";
				public const string UserMsdynOcskillidentmlmodel = "user_msdyn_ocskillidentmlmodel";
				public const string UserMsdynOmnichannelpersonalization = "user_msdyn_omnichannelpersonalization";
				public const string UserMsdynOmnichannelqueue = "user_msdyn_omnichannelqueue";
				public const string UserMsdynOmnichannelsyncconfig = "user_msdyn_omnichannelsyncconfig";
				public const string UserMsdynOperatinghour = "user_msdyn_operatinghour";
				public const string UserMsdynOpportunitymodelconfig = "user_msdyn_opportunitymodelconfig";
				public const string UserMsdynOverflowactionconfig = "user_msdyn_overflowactionconfig";
				public const string UserMsdynPersonalmessage = "user_msdyn_personalmessage";
				public const string UserMsdynPersonalsoundsetting = "user_msdyn_personalsoundsetting";
				public const string UserMsdynPlaybookactivity = "user_msdyn_playbookactivity";
				public const string UserMsdynPlaybookactivityattribute = "user_msdyn_playbookactivityattribute";
				public const string UserMsdynPlaybookcategory = "user_msdyn_playbookcategory";
				public const string UserMsdynPlaybookinstance = "user_msdyn_playbookinstance";
				public const string UserMsdynPlaybooktemplate = "user_msdyn_playbooktemplate";
				public const string UserMsdynPmanalysishistory = "user_msdyn_pmanalysishistory";
				public const string UserMsdynPmcalendar = "user_msdyn_pmcalendar";
				public const string UserMsdynPmcalendarversion = "user_msdyn_pmcalendarversion";
				public const string UserMsdynPminferredtask = "user_msdyn_pminferredtask";
				public const string UserMsdynPmprocessextendedmetadataversion = "user_msdyn_pmprocessextendedmetadataversion";
				public const string UserMsdynPmprocessusersettings = "user_msdyn_pmprocessusersettings";
				public const string UserMsdynPmprocessversion = "user_msdyn_pmprocessversion";
				public const string UserMsdynPmrecording = "user_msdyn_pmrecording";
				public const string UserMsdynPmtemplate = "user_msdyn_pmtemplate";
				public const string UserMsdynPmview = "user_msdyn_pmview";
				public const string UserMsdynPostalbum = "user_msdyn_postalbum";
				public const string UserMsdynProductivityactioninputparameter = "user_msdyn_productivityactioninputparameter";
				public const string UserMsdynProductivityactionoutputparameter = "user_msdyn_productivityactionoutputparameter";
				public const string UserMsdynProductivityagentscript = "user_msdyn_productivityagentscript";
				public const string UserMsdynProductivityagentscriptstep = "user_msdyn_productivityagentscriptstep";
				public const string UserMsdynProductivitymacroactiontemplate = "user_msdyn_productivitymacroactiontemplate";
				public const string UserMsdynProductivitymacroconnector = "user_msdyn_productivitymacroconnector";
				public const string UserMsdynProductivitymacrosolutionconfiguration = "user_msdyn_productivitymacrosolutionconfiguration";
				public const string UserMsdynProductivityparameterdefinition = "user_msdyn_productivityparameterdefinition";
				public const string UserMsdynProperty = "user_msdyn_property";
				public const string UserMsdynPropertyassetassociation = "user_msdyn_propertyassetassociation";
				public const string UserMsdynPropertylog = "user_msdyn_propertylog";
				public const string UserMsdynPropertytemplateassociation = "user_msdyn_propertytemplateassociation";
				public const string UserMsdynRealtimescoring = "user_msdyn_realtimescoring";
				public const string UserMsdynRecording = "user_msdyn_recording";
				public const string UserMsdynRelationshipinsightsunifiedconfig = "user_msdyn_relationshipinsightsunifiedconfig";
				public const string UserMsdynReportbookmark = "user_msdyn_reportbookmark";
				public const string UserMsdynRichtextfile = "user_msdyn_richtextfile";
				public const string UserMsdynRoutingconfiguration = "user_msdyn_routingconfiguration";
				public const string UserMsdynRoutingconfigurationstep = "user_msdyn_routingconfigurationstep";
				public const string UserMsdynRoutingrequest = "user_msdyn_routingrequest";
				public const string UserMsdynRulesetdependencymapping = "user_msdyn_rulesetdependencymapping";
				public const string UserMsdynSalesinsightssettings = "user_msdyn_salesinsightssettings";
				public const string UserMsdynSalesroutingrun = "user_msdyn_salesroutingrun";
				public const string UserMsdynSalessuggestion = "user_msdyn_salessuggestion";
				public const string UserMsdynSalestag = "user_msdyn_salestag";
				public const string UserMsdynSearchconfiguration = "user_msdyn_searchconfiguration";
				public const string UserMsdynSegment = "user_msdyn_segment";
				public const string UserMsdynSequence = "user_msdyn_sequence";
				public const string UserMsdynSequencestat = "user_msdyn_sequencestat";
				public const string UserMsdynSequencetarget = "user_msdyn_sequencetarget";
				public const string UserMsdynSequencetargetstep = "user_msdyn_sequencetargetstep";
				public const string UserMsdynSequencetemplate = "user_msdyn_sequencetemplate";
				public const string UserMsdynServiceconfiguration = "user_msdyn_serviceconfiguration";
				public const string UserMsdynSessiondata = "user_msdyn_sessiondata";
				public const string UserMsdynSessionevent = "user_msdyn_sessionevent";
				public const string UserMsdynSessionparticipant = "user_msdyn_sessionparticipant";
				public const string UserMsdynSessionparticipantdata = "user_msdyn_sessionparticipantdata";
				public const string UserMsdynSessiontemplate = "user_msdyn_sessiontemplate";
				public const string UserMsdynSiconfig = "user_msdyn_siconfig";
				public const string UserMsdynSkillattachmentruleitem = "user_msdyn_skillattachmentruleitem";
				public const string UserMsdynSkillattachmenttarget = "user_msdyn_skillattachmenttarget";
				public const string UserMsdynSlakpi = "user_msdyn_slakpi";
				public const string UserMsdynSolutionhealthrule = "user_msdyn_solutionhealthrule";
				public const string UserMsdynSolutionhealthruleargument = "user_msdyn_solutionhealthruleargument";
				public const string UserMsdynSoundnotificationsetting = "user_msdyn_soundnotificationsetting";
				public const string UserMsdynSwarm = "user_msdyn_swarm";
				public const string UserMsdynSwarmparticipant = "user_msdyn_swarmparticipant";
				public const string UserMsdynSwarmparticipantrule = "user_msdyn_swarmparticipantrule";
				public const string UserMsdynSwarmrole = "user_msdyn_swarmrole";
				public const string UserMsdynSwarmskill = "user_msdyn_swarmskill";
				public const string UserMsdynSwarmtemplate = "user_msdyn_swarmtemplate";
				public const string UserMsdynTaggedrecord = "user_msdyn_taggedrecord";
				public const string UserMsdynTemplateforproperties = "user_msdyn_templateforproperties";
				public const string UserMsdynTemplateparameter = "user_msdyn_templateparameter";
				public const string UserMsdynTimespent = "user_msdyn_timespent";
				public const string UserMsdynTranscript = "user_msdyn_transcript";
				public const string UserMsdynUnifiedroutingdiagnostic = "user_msdyn_unifiedroutingdiagnostic";
				public const string UserMsdynUnifiedroutingrun = "user_msdyn_unifiedroutingrun";
				public const string UserMsdynUntrackedappointment = "user_msdyn_untrackedappointment";
				public const string UserMsdynUrnotificationtemplate = "user_msdyn_urnotificationtemplate";
				public const string UserMsdynUrnotificationtemplatemapping = "user_msdyn_urnotificationtemplatemapping";
				public const string UserMsdynVirtualtablecolumncandidate = "user_msdyn_virtualtablecolumncandidate";
				public const string UserMsdynVisitorjourney = "user_msdyn_visitorjourney";
				public const string UserMsdynVivacustomerlist = "user_msdyn_vivacustomerlist";
				public const string UserMsdynVivausersetting = "user_msdyn_vivausersetting";
				public const string UserMsdynWallsavedqueryusersettings = "user_msdyn_wallsavedqueryusersettings";
				public const string UserMsdynWorkqueuestate = "user_msdyn_workqueuestate";
				public const string UserMsdynWorkqueueusersetting = "user_msdyn_workqueueusersetting";
				public const string UserMsdynceBotcontent = "user_msdynce_botcontent";
				public const string UserMsdyncrmAddtocalendarstyle = "user_msdyncrm_addtocalendarstyle";
				public const string UserMsdyncrmBasestyle = "user_msdyncrm_basestyle";
				public const string UserMsdyncrmButtonstyle = "user_msdyncrm_buttonstyle";
				public const string UserMsdyncrmCodestyle = "user_msdyncrm_codestyle";
				public const string UserMsdyncrmColumnstyle = "user_msdyncrm_columnstyle";
				public const string UserMsdyncrmContentblockstyle = "user_msdyncrm_contentblockstyle";
				public const string UserMsdyncrmDividerstyle = "user_msdyncrm_dividerstyle";
				public const string UserMsdyncrmGeneralstyles = "user_msdyncrm_generalstyles";
				public const string UserMsdyncrmImagestyle = "user_msdyncrm_imagestyle";
				public const string UserMsdyncrmLayoutstyle = "user_msdyncrm_layoutstyle";
				public const string UserMsdyncrmQrcodestyle = "user_msdyncrm_qrcodestyle";
				public const string UserMsdyncrmTextstyle = "user_msdyncrm_textstyle";
				public const string UserMsdyncrmVideostyle = "user_msdyncrm_videostyle";
				public const string UserMsdynmktCatalogeventstatusconfiguration = "user_msdynmkt_catalogeventstatusconfiguration";
				public const string UserMsdynmktConfiguration = "user_msdynmkt_configuration";
				public const string UserMsdynmktEventmetadata = "user_msdynmkt_eventmetadata";
				public const string UserMsdynmktEventparametermetadata = "user_msdynmkt_eventparametermetadata";
				public const string UserMsdynmktFeatureconfiguration = "user_msdynmkt_featureconfiguration";
				public const string UserMsfpAlertrule = "user_msfp_alertrule";
				public const string UserMsfpEmailtemplate = "user_msfp_emailtemplate";
				public const string UserMsfpFileresponse = "user_msfp_fileresponse";
				public const string UserMsfpLocalizedemailtemplate = "user_msfp_localizedemailtemplate";
				public const string UserMsfpProject = "user_msfp_project";
				public const string UserMsfpQuestion = "user_msfp_question";
				public const string UserMsfpQuestionresponse = "user_msfp_questionresponse";
				public const string UserMsfpSatisfactionmetric = "user_msfp_satisfactionmetric";
				public const string UserMsfpSurvey = "user_msfp_survey";
				public const string UserMsfpSurveyreminder = "user_msfp_surveyreminder";
				public const string UserMsfpUnsubscribedrecipient = "user_msfp_unsubscribedrecipient";
				public const string UserOpportunityclose = "user_opportunityclose";
				public const string UserOpportunityproduct = "user_opportunityproduct";
				public const string UserOrderclose = "user_orderclose";
				public const string UserOwnerPostfollows = "user_owner_postfollows";
				public const string UserParentUser = "user_parent_user";
				public const string UserPdfsetting = "user_pdfsetting";
				public const string UserPhonecall = "user_phonecall";
				public const string UserPowerbidataset = "user_powerbidataset";
				public const string UserPowerbimashupparameter = "user_powerbimashupparameter";
				public const string UserPowerbireport = "user_powerbireport";
				public const string UserPowerfxrule = "user_powerfxrule";
				public const string UserProcessstageparameter = "user_processstageparameter";
				public const string UserProfilerule = "user_profilerule";
				public const string UserQuoteclose = "user_quoteclose";
				public const string UserQuotedetail = "user_quotedetail";
				public const string UserRatingmodel = "user_ratingmodel";
				public const string UserRatingvalue = "user_ratingvalue";
				public const string UserRecurringappointmentmaster = "user_recurringappointmentmaster";
				public const string UserRevokeinheritedaccessrecordstracker = "user_revokeinheritedaccessrecordstracker";
				public const string UserRoutingrule = "user_routingrule";
				public const string UserRoutingruleitem = "user_routingruleitem";
				public const string UserSalesorderdetail = "user_salesorderdetail";
				public const string UserSettings = "user_settings";
				public const string UserSharepointdocumentlocation = "user_sharepointdocumentlocation";
				public const string UserSharepointsite = "user_sharepointsite";
				public const string UserSlabase = "user_slabase";
				public const string UserSocialactivity = "user_socialactivity";
				public const string UserSolutioncomponentbatchconfiguration = "user_solutioncomponentbatchconfiguration";
				public const string UserStagesolutionupload = "user_stagesolutionupload";
				public const string UserSynapsedatabase = "user_synapsedatabase";
				public const string UserTask = "user_task";
				public const string UserTdsmetadata = "user_tdsmetadata";
				public const string UserUntrackedemail = "user_untrackedemail";
				public const string UserUserapplicationmetadata = "user_userapplicationmetadata";
				public const string UserUserauthztracker = "user_userauthztracker";
				public const string UserUserform = "user_userform";
				public const string UserUserquery = "user_userquery";
				public const string UserUserqueryvisualizations = "user_userqueryvisualizations";
				public const string UserWorkflowbinary = "user_workflowbinary";
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
				public const string WorkflowModifiedby = "workflow_modifiedby";
				public const string WorkflowModifiedonbehalfby = "workflow_modifiedonbehalfby";
            }

            public static class ManyToOne
            {
				public const string BusinessUnitSystemUsers = "business_unit_system_users";
				public const string CalendarSystemUsers = "calendar_system_users";
				public const string LkSystemuserCreatedonbehalfby = "lk_systemuser_createdonbehalfby";
				public const string LkSystemuserEntityimage = "lk_systemuser_entityimage";
				public const string LkSystemuserModifiedonbehalfby = "lk_systemuser_modifiedonbehalfby";
				public const string LkSystemuserbaseCreatedby = "lk_systemuserbase_createdby";
				public const string LkSystemuserbaseModifiedby = "lk_systemuserbase_modifiedby";
				public const string MobileOfflineProfileSystemUser = "MobileOfflineProfile_SystemUser";
				public const string MsdynMsdynPresenceSystemuser = "msdyn_msdyn_presence_systemuser";
				public const string OrganizationSystemUsers = "organization_system_users";
				public const string PositionUsers = "position_users";
				public const string ProcessstageSystemusers = "processstage_systemusers";
				public const string QueueSystemUser = "queue_system_user";
				public const string SiteSystemUsers = "site_system_users";
				public const string SystemuserDefaultmailboxMailbox = "systemuser_defaultmailbox_mailbox";
				public const string TerritorySystemUsers = "territory_system_users";
				public const string TransactionCurrencySystemUser = "TransactionCurrency_SystemUser";
				public const string UserParentUser = "user_parent_user";
            }

            public static class ManyToMany
            {
				public const string MsdynAppconfigurationSystemuser = "msdyn_appconfiguration_systemuser";
				public const string MsdynMsdynAttributevalueSystemuser = "msdyn_msdyn_attributevalue_systemuser";
				public const string MsdynSystemuserMsdynOmnichannelqueue = "msdyn_systemuser_msdyn_omnichannelqueue";
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
            return Retrieve(service,id, new ColumnSet(true));
        }

        public static SystemUser Retrieve(IOrganizationService service, Guid id, ColumnSet columnSet)
        {
            return service.Retrieve("systemuser", id, columnSet).ToEntity<SystemUser>();
        }

        public SystemUser GetChangedEntity()
        {
            if (_trackChanges)
            {
                var attr = new AttributeCollection();
                foreach (var attrName in _changedProperties.Value.Select(changedProperty => ((AttributeLogicalNameAttribute) GetType().GetProperty(changedProperty)!.GetCustomAttribute(typeof (AttributeLogicalNameAttribute))!).LogicalName).Where(attrName => Contains(attrName)))
                {
                    attr.Add(attrName,this[attrName]);
                }
                return new  SystemUser(Id) {Attributes = attr };
            }
            return this;
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
