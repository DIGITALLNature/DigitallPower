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
		/// Type of user - Application user or Bot application user
		/// </summary>
		[AttributeLogicalName("msdyn_agentType")]
        public OptionSetValue? MsdynAgentType
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<OptionSetValue?>("msdyn_agentType");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(MsdynAgentType));
                SetAttributeValue("msdyn_agentType", value);
                OnPropertyChanged(nameof(MsdynAgentType));
            }
        }

		/// <summary>
		/// Application ID of the bot.
		/// </summary>
		[AttributeLogicalName("msdyn_botapplicationid")]
        public string? MsdynBotApplicationId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("msdyn_botapplicationid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(MsdynBotApplicationId));
                SetAttributeValue("msdyn_botapplicationid", value);
                OnPropertyChanged(nameof(MsdynBotApplicationId));
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
		/// Bot handle
		/// </summary>
		[AttributeLogicalName("msdyn_bothandle")]
        public string? MsdynBothandle
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("msdyn_bothandle");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(MsdynBothandle));
                SetAttributeValue("msdyn_bothandle", value);
                OnPropertyChanged(nameof(MsdynBothandle));
            }
        }

		/// <summary>
		/// Indicates the type of bot
		/// </summary>
		[AttributeLogicalName("msdyn_botprovider")]
        public OptionSetValue? MsdynBotProvider
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<OptionSetValue?>("msdyn_botprovider");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(MsdynBotProvider));
                SetAttributeValue("msdyn_botprovider", value);
                OnPropertyChanged(nameof(MsdynBotProvider));
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
		/// Environment Id of the CDS environment that owns the bot user.
		/// </summary>
		[AttributeLogicalName("msdyn_owningenvironmentid")]
        public string? MsdynOwningEnvironmentId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("msdyn_owningenvironmentid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(MsdynOwningEnvironmentId));
                SetAttributeValue("msdyn_owningenvironmentid", value);
                OnPropertyChanged(nameof(MsdynOwningEnvironmentId));
            }
        }

		
		[AttributeLogicalName("msdyn_phonenumberid")]
        public EntityReference? MsdynPhonenumberid
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<EntityReference?>("msdyn_phonenumberid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(MsdynPhonenumberid));
                SetAttributeValue("msdyn_phonenumberid", value);
                OnPropertyChanged(nameof(MsdynPhonenumberid));
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
		/// Unique identifier for Configuration associated with User.
		/// </summary>
		[AttributeLogicalName("msdyusd_usdconfigurationid")]
        public EntityReference? MsdyusdUSDConfigurationId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<EntityReference?>("msdyusd_usdconfigurationid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(MsdyusdUSDConfigurationId));
                SetAttributeValue("msdyusd_usdconfigurationid", value);
                OnPropertyChanged(nameof(MsdyusdUSDConfigurationId));
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
		/// 1:N lk_connectionreference_createdby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_connectionreference_createdby")]
		public System.Collections.Generic.IEnumerable<Connectionreference> LkConnectionreferenceCreatedby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Connectionreference>("lk_connectionreference_createdby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkConnectionreferenceCreatedby");
				this.SetRelatedEntities<Connectionreference>("lk_connectionreference_createdby", null, value);
				this.OnPropertyChanged("LkConnectionreferenceCreatedby");
			}
		}

		/// <summary>
		/// 1:N lk_connectionreference_createdonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_connectionreference_createdonbehalfby")]
		public System.Collections.Generic.IEnumerable<Connectionreference> LkConnectionreferenceCreatedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Connectionreference>("lk_connectionreference_createdonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkConnectionreferenceCreatedonbehalfby");
				this.SetRelatedEntities<Connectionreference>("lk_connectionreference_createdonbehalfby", null, value);
				this.OnPropertyChanged("LkConnectionreferenceCreatedonbehalfby");
			}
		}

		/// <summary>
		/// 1:N lk_connectionreference_modifiedby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_connectionreference_modifiedby")]
		public System.Collections.Generic.IEnumerable<Connectionreference> LkConnectionreferenceModifiedby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Connectionreference>("lk_connectionreference_modifiedby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkConnectionreferenceModifiedby");
				this.SetRelatedEntities<Connectionreference>("lk_connectionreference_modifiedby", null, value);
				this.OnPropertyChanged("LkConnectionreferenceModifiedby");
			}
		}

		/// <summary>
		/// 1:N lk_connectionreference_modifiedonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_connectionreference_modifiedonbehalfby")]
		public System.Collections.Generic.IEnumerable<Connectionreference> LkConnectionreferenceModifiedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Connectionreference>("lk_connectionreference_modifiedonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkConnectionreferenceModifiedonbehalfby");
				this.SetRelatedEntities<Connectionreference>("lk_connectionreference_modifiedonbehalfby", null, value);
				this.OnPropertyChanged("LkConnectionreferenceModifiedonbehalfby");
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
		/// 1:N lk_dgt_carrier_createdby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_dgt_carrier_createdby")]
		public System.Collections.Generic.IEnumerable<DgtCarrier> LkDgtCarrierCreatedby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<DgtCarrier>("lk_dgt_carrier_createdby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkDgtCarrierCreatedby");
				this.SetRelatedEntities<DgtCarrier>("lk_dgt_carrier_createdby", null, value);
				this.OnPropertyChanged("LkDgtCarrierCreatedby");
			}
		}

		/// <summary>
		/// 1:N lk_dgt_carrier_createdonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_dgt_carrier_createdonbehalfby")]
		public System.Collections.Generic.IEnumerable<DgtCarrier> LkDgtCarrierCreatedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<DgtCarrier>("lk_dgt_carrier_createdonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkDgtCarrierCreatedonbehalfby");
				this.SetRelatedEntities<DgtCarrier>("lk_dgt_carrier_createdonbehalfby", null, value);
				this.OnPropertyChanged("LkDgtCarrierCreatedonbehalfby");
			}
		}

		/// <summary>
		/// 1:N lk_dgt_carrier_modifiedby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_dgt_carrier_modifiedby")]
		public System.Collections.Generic.IEnumerable<DgtCarrier> LkDgtCarrierModifiedby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<DgtCarrier>("lk_dgt_carrier_modifiedby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkDgtCarrierModifiedby");
				this.SetRelatedEntities<DgtCarrier>("lk_dgt_carrier_modifiedby", null, value);
				this.OnPropertyChanged("LkDgtCarrierModifiedby");
			}
		}

		/// <summary>
		/// 1:N lk_dgt_carrier_modifiedonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_dgt_carrier_modifiedonbehalfby")]
		public System.Collections.Generic.IEnumerable<DgtCarrier> LkDgtCarrierModifiedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<DgtCarrier>("lk_dgt_carrier_modifiedonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkDgtCarrierModifiedonbehalfby");
				this.SetRelatedEntities<DgtCarrier>("lk_dgt_carrier_modifiedonbehalfby", null, value);
				this.OnPropertyChanged("LkDgtCarrierModifiedonbehalfby");
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
		/// 1:N lk_environmentvariabledefinition_createdby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_environmentvariabledefinition_createdby")]
		public System.Collections.Generic.IEnumerable<EnvironmentVariableDefinition> LkEnvironmentvariabledefinitionCreatedby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<EnvironmentVariableDefinition>("lk_environmentvariabledefinition_createdby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkEnvironmentvariabledefinitionCreatedby");
				this.SetRelatedEntities<EnvironmentVariableDefinition>("lk_environmentvariabledefinition_createdby", null, value);
				this.OnPropertyChanged("LkEnvironmentvariabledefinitionCreatedby");
			}
		}

		/// <summary>
		/// 1:N lk_environmentvariabledefinition_createdonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_environmentvariabledefinition_createdonbehalfby")]
		public System.Collections.Generic.IEnumerable<EnvironmentVariableDefinition> LkEnvironmentvariabledefinitionCreatedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<EnvironmentVariableDefinition>("lk_environmentvariabledefinition_createdonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkEnvironmentvariabledefinitionCreatedonbehalfby");
				this.SetRelatedEntities<EnvironmentVariableDefinition>("lk_environmentvariabledefinition_createdonbehalfby", null, value);
				this.OnPropertyChanged("LkEnvironmentvariabledefinitionCreatedonbehalfby");
			}
		}

		/// <summary>
		/// 1:N lk_environmentvariabledefinition_modifiedby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_environmentvariabledefinition_modifiedby")]
		public System.Collections.Generic.IEnumerable<EnvironmentVariableDefinition> LkEnvironmentvariabledefinitionModifiedby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<EnvironmentVariableDefinition>("lk_environmentvariabledefinition_modifiedby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkEnvironmentvariabledefinitionModifiedby");
				this.SetRelatedEntities<EnvironmentVariableDefinition>("lk_environmentvariabledefinition_modifiedby", null, value);
				this.OnPropertyChanged("LkEnvironmentvariabledefinitionModifiedby");
			}
		}

		/// <summary>
		/// 1:N lk_environmentvariabledefinition_modifiedonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_environmentvariabledefinition_modifiedonbehalfby")]
		public System.Collections.Generic.IEnumerable<EnvironmentVariableDefinition> LkEnvironmentvariabledefinitionModifiedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<EnvironmentVariableDefinition>("lk_environmentvariabledefinition_modifiedonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkEnvironmentvariabledefinitionModifiedonbehalfby");
				this.SetRelatedEntities<EnvironmentVariableDefinition>("lk_environmentvariabledefinition_modifiedonbehalfby", null, value);
				this.OnPropertyChanged("LkEnvironmentvariabledefinitionModifiedonbehalfby");
			}
		}

		/// <summary>
		/// 1:N lk_environmentvariablevalue_createdby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_environmentvariablevalue_createdby")]
		public System.Collections.Generic.IEnumerable<EnvironmentVariableValue> LkEnvironmentvariablevalueCreatedby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<EnvironmentVariableValue>("lk_environmentvariablevalue_createdby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkEnvironmentvariablevalueCreatedby");
				this.SetRelatedEntities<EnvironmentVariableValue>("lk_environmentvariablevalue_createdby", null, value);
				this.OnPropertyChanged("LkEnvironmentvariablevalueCreatedby");
			}
		}

		/// <summary>
		/// 1:N lk_environmentvariablevalue_createdonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_environmentvariablevalue_createdonbehalfby")]
		public System.Collections.Generic.IEnumerable<EnvironmentVariableValue> LkEnvironmentvariablevalueCreatedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<EnvironmentVariableValue>("lk_environmentvariablevalue_createdonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkEnvironmentvariablevalueCreatedonbehalfby");
				this.SetRelatedEntities<EnvironmentVariableValue>("lk_environmentvariablevalue_createdonbehalfby", null, value);
				this.OnPropertyChanged("LkEnvironmentvariablevalueCreatedonbehalfby");
			}
		}

		/// <summary>
		/// 1:N lk_environmentvariablevalue_modifiedby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_environmentvariablevalue_modifiedby")]
		public System.Collections.Generic.IEnumerable<EnvironmentVariableValue> LkEnvironmentvariablevalueModifiedby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<EnvironmentVariableValue>("lk_environmentvariablevalue_modifiedby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkEnvironmentvariablevalueModifiedby");
				this.SetRelatedEntities<EnvironmentVariableValue>("lk_environmentvariablevalue_modifiedby", null, value);
				this.OnPropertyChanged("LkEnvironmentvariablevalueModifiedby");
			}
		}

		/// <summary>
		/// 1:N lk_environmentvariablevalue_modifiedonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_environmentvariablevalue_modifiedonbehalfby")]
		public System.Collections.Generic.IEnumerable<EnvironmentVariableValue> LkEnvironmentvariablevalueModifiedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<EnvironmentVariableValue>("lk_environmentvariablevalue_modifiedonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkEnvironmentvariablevalueModifiedonbehalfby");
				this.SetRelatedEntities<EnvironmentVariableValue>("lk_environmentvariablevalue_modifiedonbehalfby", null, value);
				this.OnPropertyChanged("LkEnvironmentvariablevalueModifiedonbehalfby");
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
		/// 1:N lk_pluginpackage_createdby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_pluginpackage_createdby")]
		public System.Collections.Generic.IEnumerable<PluginPackage> LkPluginpackageCreatedby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<PluginPackage>("lk_pluginpackage_createdby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkPluginpackageCreatedby");
				this.SetRelatedEntities<PluginPackage>("lk_pluginpackage_createdby", null, value);
				this.OnPropertyChanged("LkPluginpackageCreatedby");
			}
		}

		/// <summary>
		/// 1:N lk_pluginpackage_createdonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_pluginpackage_createdonbehalfby")]
		public System.Collections.Generic.IEnumerable<PluginPackage> LkPluginpackageCreatedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<PluginPackage>("lk_pluginpackage_createdonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkPluginpackageCreatedonbehalfby");
				this.SetRelatedEntities<PluginPackage>("lk_pluginpackage_createdonbehalfby", null, value);
				this.OnPropertyChanged("LkPluginpackageCreatedonbehalfby");
			}
		}

		/// <summary>
		/// 1:N lk_pluginpackage_modifiedby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_pluginpackage_modifiedby")]
		public System.Collections.Generic.IEnumerable<PluginPackage> LkPluginpackageModifiedby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<PluginPackage>("lk_pluginpackage_modifiedby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkPluginpackageModifiedby");
				this.SetRelatedEntities<PluginPackage>("lk_pluginpackage_modifiedby", null, value);
				this.OnPropertyChanged("LkPluginpackageModifiedby");
			}
		}

		/// <summary>
		/// 1:N lk_pluginpackage_modifiedonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_pluginpackage_modifiedonbehalfby")]
		public System.Collections.Generic.IEnumerable<PluginPackage> LkPluginpackageModifiedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<PluginPackage>("lk_pluginpackage_modifiedonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkPluginpackageModifiedonbehalfby");
				this.SetRelatedEntities<PluginPackage>("lk_pluginpackage_modifiedonbehalfby", null, value);
				this.OnPropertyChanged("LkPluginpackageModifiedonbehalfby");
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
		/// 1:N lk_webresourcebase_createdonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_webresourcebase_createdonbehalfby")]
		public System.Collections.Generic.IEnumerable<WebResource> LkWebresourcebaseCreatedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<WebResource>("lk_webresourcebase_createdonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkWebresourcebaseCreatedonbehalfby");
				this.SetRelatedEntities<WebResource>("lk_webresourcebase_createdonbehalfby", null, value);
				this.OnPropertyChanged("LkWebresourcebaseCreatedonbehalfby");
			}
		}

		/// <summary>
		/// 1:N lk_webresourcebase_modifiedonbehalfby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("lk_webresourcebase_modifiedonbehalfby")]
		public System.Collections.Generic.IEnumerable<WebResource> LkWebresourcebaseModifiedonbehalfby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<WebResource>("lk_webresourcebase_modifiedonbehalfby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("LkWebresourcebaseModifiedonbehalfby");
				this.SetRelatedEntities<WebResource>("lk_webresourcebase_modifiedonbehalfby", null, value);
				this.OnPropertyChanged("LkWebresourcebaseModifiedonbehalfby");
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
		/// 1:N user_connectionreference
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("user_connectionreference")]
		public System.Collections.Generic.IEnumerable<Connectionreference> UserConnectionreference
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Connectionreference>("user_connectionreference", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("UserConnectionreference");
				this.SetRelatedEntities<Connectionreference>("user_connectionreference", null, value);
				this.OnPropertyChanged("UserConnectionreference");
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
		/// 1:N user_environmentvariabledefinition
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("user_environmentvariabledefinition")]
		public System.Collections.Generic.IEnumerable<EnvironmentVariableDefinition> UserEnvironmentvariabledefinition
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<EnvironmentVariableDefinition>("user_environmentvariabledefinition", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("UserEnvironmentvariabledefinition");
				this.SetRelatedEntities<EnvironmentVariableDefinition>("user_environmentvariabledefinition", null, value);
				this.OnPropertyChanged("UserEnvironmentvariabledefinition");
			}
		}

		/// <summary>
		/// 1:N user_environmentvariablevalue
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("user_environmentvariablevalue")]
		public System.Collections.Generic.IEnumerable<EnvironmentVariableValue> UserEnvironmentvariablevalue
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<EnvironmentVariableValue>("user_environmentvariablevalue", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("UserEnvironmentvariablevalue");
				this.SetRelatedEntities<EnvironmentVariableValue>("user_environmentvariablevalue", null, value);
				this.OnPropertyChanged("UserEnvironmentvariablevalue");
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
		/// 1:N webresource_createdby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("webresource_createdby")]
		public System.Collections.Generic.IEnumerable<WebResource> WebresourceCreatedby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<WebResource>("webresource_createdby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("WebresourceCreatedby");
				this.SetRelatedEntities<WebResource>("webresource_createdby", null, value);
				this.OnPropertyChanged("WebresourceCreatedby");
			}
		}

		/// <summary>
		/// 1:N webresource_modifiedby
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("webresource_modifiedby")]
		public System.Collections.Generic.IEnumerable<WebResource> WebresourceModifiedby
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<WebResource>("webresource_modifiedby", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("WebresourceModifiedby");
				this.SetRelatedEntities<WebResource>("webresource_modifiedby", null, value);
				this.OnPropertyChanged("WebresourceModifiedby");
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
			    public struct MsdynAgentType
                {
					public const int ApplicationUser = 192350000;
					public const int BotApplicationUser = 192350001;
                }
			    public struct MsdynBotProvider
                {
					public const int VirtualAgent = 192350000;
					public const int Other = 192350001;
					public const int None = 192350002;
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
				public const string MobilePhone = "mobilephone";
				public const string ModifiedBy = "modifiedby";
				public const string ModifiedOn = "modifiedon";
				public const string ModifiedOnBehalfBy = "modifiedonbehalfby";
				public const string MsdynAgentType = "msdyn_agentType";
				public const string MsdynBotApplicationId = "msdyn_botapplicationid";
				public const string MsdynBotDescription = "msdyn_botdescription";
				public const string MsdynBotEndpoint = "msdyn_botendpoint";
				public const string MsdynBothandle = "msdyn_bothandle";
				public const string MsdynBotProvider = "msdyn_botprovider";
				public const string MsdynBotSecretKeys = "msdyn_botsecretkeys";
				public const string MsdynCapacity = "msdyn_capacity";
				public const string MsdynDefaultPresenceIdUser = "msdyn_defaultpresenceiduser";
				public const string MsdynGdproptout = "msdyn_gdproptout";
				public const string MsdynGridwrappercontrolfield = "msdyn_gridwrappercontrolfield";
				public const string MsdynIsexpertenabledforswarm = "msdyn_isexpertenabledforswarm";
				public const string MsdynOwningEnvironmentId = "msdyn_owningenvironmentid";
				public const string MsdynPhonenumberid = "msdyn_phonenumberid";
				public const string MsdynUserType = "msdyn_usertype";
				public const string MsdyusdUSDConfigurationId = "msdyusd_usdconfigurationid";
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
				public const string LkAicopilotCreatedby = "lk_aicopilot_createdby";
				public const string LkAicopilotCreatedonbehalfby = "lk_aicopilot_createdonbehalfby";
				public const string LkAicopilotModifiedby = "lk_aicopilot_modifiedby";
				public const string LkAicopilotModifiedonbehalfby = "lk_aicopilot_modifiedonbehalfby";
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
				public const string LkComponentversionCreatedby = "lk_componentversion_createdby";
				public const string LkComponentversionModifiedby = "lk_componentversion_modifiedby";
				public const string LkComponentversionnrddatasourceCreatedby = "lk_componentversionnrddatasource_createdby";
				public const string LkComponentversionnrddatasourceCreatedonbehalfby = "lk_componentversionnrddatasource_createdonbehalfby";
				public const string LkComponentversionnrddatasourceModifiedby = "lk_componentversionnrddatasource_modifiedby";
				public const string LkComponentversionnrddatasourceModifiedonbehalfby = "lk_componentversionnrddatasource_modifiedonbehalfby";
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
				public const string LkCr072BookingCreatedby = "lk_cr072_booking_createdby";
				public const string LkCr072BookingCreatedonbehalfby = "lk_cr072_booking_createdonbehalfby";
				public const string LkCr072BookingModifiedby = "lk_cr072_booking_modifiedby";
				public const string LkCr072BookingModifiedonbehalfby = "lk_cr072_booking_modifiedonbehalfby";
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
				public const string LkDgtCarrierCreatedby = "lk_dgt_carrier_createdby";
				public const string LkDgtCarrierCreatedonbehalfby = "lk_dgt_carrier_createdonbehalfby";
				public const string LkDgtCarrierDependencyCheckCreatedby = "lk_dgt_carrier_dependency_check_createdby";
				public const string LkDgtCarrierDependencyCheckCreatedonbehalfby = "lk_dgt_carrier_dependency_check_createdonbehalfby";
				public const string LkDgtCarrierDependencyCheckModifiedby = "lk_dgt_carrier_dependency_check_modifiedby";
				public const string LkDgtCarrierDependencyCheckModifiedonbehalfby = "lk_dgt_carrier_dependency_check_modifiedonbehalfby";
				public const string LkDgtCarrierMissingDependencyCreatedby = "lk_dgt_carrier_missing_dependency_createdby";
				public const string LkDgtCarrierMissingDependencyCreatedonbehalfby = "lk_dgt_carrier_missing_dependency_createdonbehalfby";
				public const string LkDgtCarrierMissingDependencyModifiedby = "lk_dgt_carrier_missing_dependency_modifiedby";
				public const string LkDgtCarrierMissingDependencyModifiedonbehalfby = "lk_dgt_carrier_missing_dependency_modifiedonbehalfby";
				public const string LkDgtCarrierModifiedby = "lk_dgt_carrier_modifiedby";
				public const string LkDgtCarrierModifiedonbehalfby = "lk_dgt_carrier_modifiedonbehalfby";
				public const string LkDgtWorkbenchCreatedby = "lk_dgt_workbench_createdby";
				public const string LkDgtWorkbenchCreatedonbehalfby = "lk_dgt_workbench_createdonbehalfby";
				public const string LkDgtWorkbenchHistoryCreatedby = "lk_dgt_workbench_history_createdby";
				public const string LkDgtWorkbenchHistoryCreatedonbehalfby = "lk_dgt_workbench_history_createdonbehalfby";
				public const string LkDgtWorkbenchHistoryModifiedby = "lk_dgt_workbench_history_modifiedby";
				public const string LkDgtWorkbenchHistoryModifiedonbehalfby = "lk_dgt_workbench_history_modifiedonbehalfby";
				public const string LkDgtWorkbenchModifiedby = "lk_dgt_workbench_modifiedby";
				public const string LkDgtWorkbenchModifiedonbehalfby = "lk_dgt_workbench_modifiedonbehalfby";
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
				public const string LkEnablearchivalrequestCreatedby = "lk_enablearchivalrequest_createdby";
				public const string LkEnablearchivalrequestCreatedonbehalfby = "lk_enablearchivalrequest_createdonbehalfby";
				public const string LkEnablearchivalrequestModifiedby = "lk_enablearchivalrequest_modifiedby";
				public const string LkEnablearchivalrequestModifiedonbehalfby = "lk_enablearchivalrequest_modifiedonbehalfby";
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
				public const string LkEquipmentCreatedby = "lk_equipment_createdby";
				public const string LkEquipmentCreatedonbehalfby = "lk_equipment_createdonbehalfby";
				public const string LkEquipmentModifiedby = "lk_equipment_modifiedby";
				public const string LkEquipmentModifiedonbehalfby = "lk_equipment_modifiedonbehalfby";
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
				public const string LkFederatedknowledgeconfigurationCreatedby = "lk_federatedknowledgeconfiguration_createdby";
				public const string LkFederatedknowledgeconfigurationCreatedonbehalfby = "lk_federatedknowledgeconfiguration_createdonbehalfby";
				public const string LkFederatedknowledgeconfigurationModifiedby = "lk_federatedknowledgeconfiguration_modifiedby";
				public const string LkFederatedknowledgeconfigurationModifiedonbehalfby = "lk_federatedknowledgeconfiguration_modifiedonbehalfby";
				public const string LkFederatedknowledgeentityconfigurationCreatedby = "lk_federatedknowledgeentityconfiguration_createdby";
				public const string LkFederatedknowledgeentityconfigurationCreatedonbehalfby = "lk_federatedknowledgeentityconfiguration_createdonbehalfby";
				public const string LkFederatedknowledgeentityconfigurationModifiedby = "lk_federatedknowledgeentityconfiguration_modifiedby";
				public const string LkFederatedknowledgeentityconfigurationModifiedonbehalfby = "lk_federatedknowledgeentityconfiguration_modifiedonbehalfby";
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
				public const string LkFxexpressionCreatedby = "lk_fxexpression_createdby";
				public const string LkFxexpressionCreatedonbehalfby = "lk_fxexpression_createdonbehalfby";
				public const string LkFxexpressionModifiedby = "lk_fxexpression_modifiedby";
				public const string LkFxexpressionModifiedonbehalfby = "lk_fxexpression_modifiedonbehalfby";
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
				public const string LkMarketingformdisplayattributesCreatedby = "lk_marketingformdisplayattributes_createdby";
				public const string LkMarketingformdisplayattributesCreatedonbehalfby = "lk_marketingformdisplayattributes_createdonbehalfby";
				public const string LkMarketingformdisplayattributesModifiedby = "lk_marketingformdisplayattributes_modifiedby";
				public const string LkMarketingformdisplayattributesModifiedonbehalfby = "lk_marketingformdisplayattributes_modifiedonbehalfby";
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
				public const string LkMsdyn3dmodelCreatedby = "lk_msdyn_3dmodel_createdby";
				public const string LkMsdyn3dmodelCreatedonbehalfby = "lk_msdyn_3dmodel_createdonbehalfby";
				public const string LkMsdyn3dmodelModifiedby = "lk_msdyn_3dmodel_modifiedby";
				public const string LkMsdyn3dmodelModifiedonbehalfby = "lk_msdyn_3dmodel_modifiedonbehalfby";
				public const string LkMsdynAccountkpiitemCreatedby = "lk_msdyn_accountkpiitem_createdby";
				public const string LkMsdynAccountkpiitemCreatedonbehalfby = "lk_msdyn_accountkpiitem_createdonbehalfby";
				public const string LkMsdynAccountkpiitemModifiedby = "lk_msdyn_accountkpiitem_modifiedby";
				public const string LkMsdynAccountkpiitemModifiedonbehalfby = "lk_msdyn_accountkpiitem_modifiedonbehalfby";
				public const string LkMsdynAccountpricelistCreatedby = "lk_msdyn_accountpricelist_createdby";
				public const string LkMsdynAccountpricelistCreatedonbehalfby = "lk_msdyn_accountpricelist_createdonbehalfby";
				public const string LkMsdynAccountpricelistModifiedby = "lk_msdyn_accountpricelist_modifiedby";
				public const string LkMsdynAccountpricelistModifiedonbehalfby = "lk_msdyn_accountpricelist_modifiedonbehalfby";
				public const string LkMsdynActioncardactionstatCreatedby = "lk_msdyn_actioncardactionstat_createdby";
				public const string LkMsdynActioncardactionstatCreatedonbehalfby = "lk_msdyn_actioncardactionstat_createdonbehalfby";
				public const string LkMsdynActioncardactionstatModifiedby = "lk_msdyn_actioncardactionstat_modifiedby";
				public const string LkMsdynActioncardactionstatModifiedonbehalfby = "lk_msdyn_actioncardactionstat_modifiedonbehalfby";
				public const string LkMsdynActioncardregardingCreatedby = "lk_msdyn_actioncardregarding_createdby";
				public const string LkMsdynActioncardregardingCreatedonbehalfby = "lk_msdyn_actioncardregarding_createdonbehalfby";
				public const string LkMsdynActioncardregardingModifiedby = "lk_msdyn_actioncardregarding_modifiedby";
				public const string LkMsdynActioncardregardingModifiedonbehalfby = "lk_msdyn_actioncardregarding_modifiedonbehalfby";
				public const string LkMsdynActioncardrolesettingCreatedby = "lk_msdyn_actioncardrolesetting_createdby";
				public const string LkMsdynActioncardrolesettingCreatedonbehalfby = "lk_msdyn_actioncardrolesetting_createdonbehalfby";
				public const string LkMsdynActioncardrolesettingModifiedby = "lk_msdyn_actioncardrolesetting_modifiedby";
				public const string LkMsdynActioncardrolesettingModifiedonbehalfby = "lk_msdyn_actioncardrolesetting_modifiedonbehalfby";
				public const string LkMsdynActioncardstataggregationCreatedby = "lk_msdyn_actioncardstataggregation_createdby";
				public const string LkMsdynActioncardstataggregationCreatedonbehalfby = "lk_msdyn_actioncardstataggregation_createdonbehalfby";
				public const string LkMsdynActioncardstataggregationModifiedby = "lk_msdyn_actioncardstataggregation_modifiedby";
				public const string LkMsdynActioncardstataggregationModifiedonbehalfby = "lk_msdyn_actioncardstataggregation_modifiedonbehalfby";
				public const string LkMsdynActiveicdextensionCreatedby = "lk_msdyn_activeicdextension_createdby";
				public const string LkMsdynActiveicdextensionCreatedonbehalfby = "lk_msdyn_activeicdextension_createdonbehalfby";
				public const string LkMsdynActiveicdextensionModifiedby = "lk_msdyn_activeicdextension_modifiedby";
				public const string LkMsdynActiveicdextensionModifiedonbehalfby = "lk_msdyn_activeicdextension_modifiedonbehalfby";
				public const string LkMsdynActivityanalysiscleanupstateCreatedby = "lk_msdyn_activityanalysiscleanupstate_createdby";
				public const string LkMsdynActivityanalysiscleanupstateCreatedonbehalfby = "lk_msdyn_activityanalysiscleanupstate_createdonbehalfby";
				public const string LkMsdynActivityanalysiscleanupstateModifiedby = "lk_msdyn_activityanalysiscleanupstate_modifiedby";
				public const string LkMsdynActivityanalysiscleanupstateModifiedonbehalfby = "lk_msdyn_activityanalysiscleanupstate_modifiedonbehalfby";
				public const string LkMsdynActivityanalysisconfigCreatedby = "lk_msdyn_activityanalysisconfig_createdby";
				public const string LkMsdynActivityanalysisconfigCreatedonbehalfby = "lk_msdyn_activityanalysisconfig_createdonbehalfby";
				public const string LkMsdynActivityanalysisconfigModifiedby = "lk_msdyn_activityanalysisconfig_modifiedby";
				public const string LkMsdynActivityanalysisconfigModifiedonbehalfby = "lk_msdyn_activityanalysisconfig_modifiedonbehalfby";
				public const string LkMsdynActualCreatedby = "lk_msdyn_actual_createdby";
				public const string LkMsdynActualCreatedonbehalfby = "lk_msdyn_actual_createdonbehalfby";
				public const string LkMsdynActualModifiedby = "lk_msdyn_actual_modifiedby";
				public const string LkMsdynActualModifiedonbehalfby = "lk_msdyn_actual_modifiedonbehalfby";
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
				public const string LkMsdynAgentcapacityprofileunitCreatedby = "lk_msdyn_agentcapacityprofileunit_createdby";
				public const string LkMsdynAgentcapacityprofileunitCreatedonbehalfby = "lk_msdyn_agentcapacityprofileunit_createdonbehalfby";
				public const string LkMsdynAgentcapacityprofileunitModifiedby = "lk_msdyn_agentcapacityprofileunit_modifiedby";
				public const string LkMsdynAgentcapacityprofileunitModifiedonbehalfby = "lk_msdyn_agentcapacityprofileunit_modifiedonbehalfby";
				public const string LkMsdynAgentcapacityupdatehistoryCreatedby = "lk_msdyn_agentcapacityupdatehistory_createdby";
				public const string LkMsdynAgentcapacityupdatehistoryCreatedonbehalfby = "lk_msdyn_agentcapacityupdatehistory_createdonbehalfby";
				public const string LkMsdynAgentcapacityupdatehistoryModifiedby = "lk_msdyn_agentcapacityupdatehistory_modifiedby";
				public const string LkMsdynAgentcapacityupdatehistoryModifiedonbehalfby = "lk_msdyn_agentcapacityupdatehistory_modifiedonbehalfby";
				public const string LkMsdynAgentchannelstateCreatedby = "lk_msdyn_agentchannelstate_createdby";
				public const string LkMsdynAgentchannelstateCreatedonbehalfby = "lk_msdyn_agentchannelstate_createdonbehalfby";
				public const string LkMsdynAgentchannelstateModifiedby = "lk_msdyn_agentchannelstate_modifiedby";
				public const string LkMsdynAgentchannelstateModifiedonbehalfby = "lk_msdyn_agentchannelstate_modifiedonbehalfby";
				public const string LkMsdynAgentcopilotsettingCreatedby = "lk_msdyn_agentcopilotsetting_createdby";
				public const string LkMsdynAgentcopilotsettingCreatedonbehalfby = "lk_msdyn_agentcopilotsetting_createdonbehalfby";
				public const string LkMsdynAgentcopilotsettingModifiedby = "lk_msdyn_agentcopilotsetting_modifiedby";
				public const string LkMsdynAgentcopilotsettingModifiedonbehalfby = "lk_msdyn_agentcopilotsetting_modifiedonbehalfby";
				public const string LkMsdynAgentresourceforecastingCreatedby = "lk_msdyn_agentresourceforecasting_createdby";
				public const string LkMsdynAgentresourceforecastingCreatedonbehalfby = "lk_msdyn_agentresourceforecasting_createdonbehalfby";
				public const string LkMsdynAgentresourceforecastingModifiedby = "lk_msdyn_agentresourceforecasting_modifiedby";
				public const string LkMsdynAgentresourceforecastingModifiedonbehalfby = "lk_msdyn_agentresourceforecasting_modifiedonbehalfby";
				public const string LkMsdynAgentstatusCreatedby = "lk_msdyn_agentstatus_createdby";
				public const string LkMsdynAgentstatusCreatedonbehalfby = "lk_msdyn_agentstatus_createdonbehalfby";
				public const string LkMsdynAgentstatusModifiedby = "lk_msdyn_agentstatus_modifiedby";
				public const string LkMsdynAgentstatusModifiedonbehalfby = "lk_msdyn_agentstatus_modifiedonbehalfby";
				public const string LkMsdynAgentstatushistoryCreatedby = "lk_msdyn_agentstatushistory_createdby";
				public const string LkMsdynAgentstatushistoryCreatedonbehalfby = "lk_msdyn_agentstatushistory_createdonbehalfby";
				public const string LkMsdynAgentstatushistoryModifiedby = "lk_msdyn_agentstatushistory_modifiedby";
				public const string LkMsdynAgentstatushistoryModifiedonbehalfby = "lk_msdyn_agentstatushistory_modifiedonbehalfby";
				public const string LkMsdynAgreementCreatedby = "lk_msdyn_agreement_createdby";
				public const string LkMsdynAgreementCreatedonbehalfby = "lk_msdyn_agreement_createdonbehalfby";
				public const string LkMsdynAgreementModifiedby = "lk_msdyn_agreement_modifiedby";
				public const string LkMsdynAgreementModifiedonbehalfby = "lk_msdyn_agreement_modifiedonbehalfby";
				public const string LkMsdynAgreementbookingdateCreatedby = "lk_msdyn_agreementbookingdate_createdby";
				public const string LkMsdynAgreementbookingdateCreatedonbehalfby = "lk_msdyn_agreementbookingdate_createdonbehalfby";
				public const string LkMsdynAgreementbookingdateModifiedby = "lk_msdyn_agreementbookingdate_modifiedby";
				public const string LkMsdynAgreementbookingdateModifiedonbehalfby = "lk_msdyn_agreementbookingdate_modifiedonbehalfby";
				public const string LkMsdynAgreementbookingincidentCreatedby = "lk_msdyn_agreementbookingincident_createdby";
				public const string LkMsdynAgreementbookingincidentCreatedonbehalfby = "lk_msdyn_agreementbookingincident_createdonbehalfby";
				public const string LkMsdynAgreementbookingincidentModifiedby = "lk_msdyn_agreementbookingincident_modifiedby";
				public const string LkMsdynAgreementbookingincidentModifiedonbehalfby = "lk_msdyn_agreementbookingincident_modifiedonbehalfby";
				public const string LkMsdynAgreementbookingproductCreatedby = "lk_msdyn_agreementbookingproduct_createdby";
				public const string LkMsdynAgreementbookingproductCreatedonbehalfby = "lk_msdyn_agreementbookingproduct_createdonbehalfby";
				public const string LkMsdynAgreementbookingproductModifiedby = "lk_msdyn_agreementbookingproduct_modifiedby";
				public const string LkMsdynAgreementbookingproductModifiedonbehalfby = "lk_msdyn_agreementbookingproduct_modifiedonbehalfby";
				public const string LkMsdynAgreementbookingserviceCreatedby = "lk_msdyn_agreementbookingservice_createdby";
				public const string LkMsdynAgreementbookingserviceCreatedonbehalfby = "lk_msdyn_agreementbookingservice_createdonbehalfby";
				public const string LkMsdynAgreementbookingserviceModifiedby = "lk_msdyn_agreementbookingservice_modifiedby";
				public const string LkMsdynAgreementbookingserviceModifiedonbehalfby = "lk_msdyn_agreementbookingservice_modifiedonbehalfby";
				public const string LkMsdynAgreementbookingservicetaskCreatedby = "lk_msdyn_agreementbookingservicetask_createdby";
				public const string LkMsdynAgreementbookingservicetaskCreatedonbehalfby = "lk_msdyn_agreementbookingservicetask_createdonbehalfby";
				public const string LkMsdynAgreementbookingservicetaskModifiedby = "lk_msdyn_agreementbookingservicetask_modifiedby";
				public const string LkMsdynAgreementbookingservicetaskModifiedonbehalfby = "lk_msdyn_agreementbookingservicetask_modifiedonbehalfby";
				public const string LkMsdynAgreementbookingsetupCreatedby = "lk_msdyn_agreementbookingsetup_createdby";
				public const string LkMsdynAgreementbookingsetupCreatedonbehalfby = "lk_msdyn_agreementbookingsetup_createdonbehalfby";
				public const string LkMsdynAgreementbookingsetupModifiedby = "lk_msdyn_agreementbookingsetup_modifiedby";
				public const string LkMsdynAgreementbookingsetupModifiedonbehalfby = "lk_msdyn_agreementbookingsetup_modifiedonbehalfby";
				public const string LkMsdynAgreementinvoicedateCreatedby = "lk_msdyn_agreementinvoicedate_createdby";
				public const string LkMsdynAgreementinvoicedateCreatedonbehalfby = "lk_msdyn_agreementinvoicedate_createdonbehalfby";
				public const string LkMsdynAgreementinvoicedateModifiedby = "lk_msdyn_agreementinvoicedate_modifiedby";
				public const string LkMsdynAgreementinvoicedateModifiedonbehalfby = "lk_msdyn_agreementinvoicedate_modifiedonbehalfby";
				public const string LkMsdynAgreementinvoiceproductCreatedby = "lk_msdyn_agreementinvoiceproduct_createdby";
				public const string LkMsdynAgreementinvoiceproductCreatedonbehalfby = "lk_msdyn_agreementinvoiceproduct_createdonbehalfby";
				public const string LkMsdynAgreementinvoiceproductModifiedby = "lk_msdyn_agreementinvoiceproduct_modifiedby";
				public const string LkMsdynAgreementinvoiceproductModifiedonbehalfby = "lk_msdyn_agreementinvoiceproduct_modifiedonbehalfby";
				public const string LkMsdynAgreementinvoicesetupCreatedby = "lk_msdyn_agreementinvoicesetup_createdby";
				public const string LkMsdynAgreementinvoicesetupCreatedonbehalfby = "lk_msdyn_agreementinvoicesetup_createdonbehalfby";
				public const string LkMsdynAgreementinvoicesetupModifiedby = "lk_msdyn_agreementinvoicesetup_modifiedby";
				public const string LkMsdynAgreementinvoicesetupModifiedonbehalfby = "lk_msdyn_agreementinvoicesetup_modifiedonbehalfby";
				public const string LkMsdynAgreementsubstatusCreatedby = "lk_msdyn_agreementsubstatus_createdby";
				public const string LkMsdynAgreementsubstatusCreatedonbehalfby = "lk_msdyn_agreementsubstatus_createdonbehalfby";
				public const string LkMsdynAgreementsubstatusModifiedby = "lk_msdyn_agreementsubstatus_modifiedby";
				public const string LkMsdynAgreementsubstatusModifiedonbehalfby = "lk_msdyn_agreementsubstatus_modifiedonbehalfby";
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
				public const string LkMsdynAppcopilotconfigurationCreatedby = "lk_msdyn_appcopilotconfiguration_createdby";
				public const string LkMsdynAppcopilotconfigurationCreatedonbehalfby = "lk_msdyn_appcopilotconfiguration_createdonbehalfby";
				public const string LkMsdynAppcopilotconfigurationModifiedby = "lk_msdyn_appcopilotconfiguration_modifiedby";
				public const string LkMsdynAppcopilotconfigurationModifiedonbehalfby = "lk_msdyn_appcopilotconfiguration_modifiedonbehalfby";
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
				public const string LkMsdynAppprofilerolemappingCreatedby = "lk_msdyn_appprofilerolemapping_createdby";
				public const string LkMsdynAppprofilerolemappingCreatedonbehalfby = "lk_msdyn_appprofilerolemapping_createdonbehalfby";
				public const string LkMsdynAppprofilerolemappingModifiedby = "lk_msdyn_appprofilerolemapping_modifiedby";
				public const string LkMsdynAppprofilerolemappingModifiedonbehalfby = "lk_msdyn_appprofilerolemapping_modifiedonbehalfby";
				public const string LkMsdynApprovalsetCreatedby = "lk_msdyn_approvalset_createdby";
				public const string LkMsdynApprovalsetCreatedonbehalfby = "lk_msdyn_approvalset_createdonbehalfby";
				public const string LkMsdynApprovalsetModifiedby = "lk_msdyn_approvalset_modifiedby";
				public const string LkMsdynApprovalsetModifiedonbehalfby = "lk_msdyn_approvalset_modifiedonbehalfby";
				public const string LkMsdynAppstateCreatedby = "lk_msdyn_appstate_createdby";
				public const string LkMsdynAppstateCreatedonbehalfby = "lk_msdyn_appstate_createdonbehalfby";
				public const string LkMsdynAppstateModifiedby = "lk_msdyn_appstate_modifiedby";
				public const string LkMsdynAppstateModifiedonbehalfby = "lk_msdyn_appstate_modifiedonbehalfby";
				public const string LkMsdynAssetcategorytemplateassociationCreatedby = "lk_msdyn_assetcategorytemplateassociation_createdby";
				public const string LkMsdynAssetcategorytemplateassociationCreatedonbehalfby = "lk_msdyn_assetcategorytemplateassociation_createdonbehalfby";
				public const string LkMsdynAssetcategorytemplateassociationModifiedby = "lk_msdyn_assetcategorytemplateassociation_modifiedby";
				public const string LkMsdynAssetcategorytemplateassociationModifiedonbehalfby = "lk_msdyn_assetcategorytemplateassociation_modifiedonbehalfby";
				public const string LkMsdynAssetsuggestionssettingCreatedby = "lk_msdyn_assetsuggestionssetting_createdby";
				public const string LkMsdynAssetsuggestionssettingCreatedonbehalfby = "lk_msdyn_assetsuggestionssetting_createdonbehalfby";
				public const string LkMsdynAssetsuggestionssettingModifiedby = "lk_msdyn_assetsuggestionssetting_modifiedby";
				public const string LkMsdynAssetsuggestionssettingModifiedonbehalfby = "lk_msdyn_assetsuggestionssetting_modifiedonbehalfby";
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
				public const string LkMsdynAutonomouscasecreationruleCreatedby = "lk_msdyn_autonomouscasecreationrule_createdby";
				public const string LkMsdynAutonomouscasecreationruleCreatedonbehalfby = "lk_msdyn_autonomouscasecreationrule_createdonbehalfby";
				public const string LkMsdynAutonomouscasecreationruleModifiedby = "lk_msdyn_autonomouscasecreationrule_modifiedby";
				public const string LkMsdynAutonomouscasecreationruleModifiedonbehalfby = "lk_msdyn_autonomouscasecreationrule_modifiedonbehalfby";
				public const string LkMsdynBatchjobCreatedby = "lk_msdyn_batchjob_createdby";
				public const string LkMsdynBatchjobCreatedonbehalfby = "lk_msdyn_batchjob_createdonbehalfby";
				public const string LkMsdynBatchjobModifiedby = "lk_msdyn_batchjob_modifiedby";
				public const string LkMsdynBatchjobModifiedonbehalfby = "lk_msdyn_batchjob_modifiedonbehalfby";
				public const string LkMsdynBgjobledgerCreatedby = "lk_msdyn_bgjobledger_createdby";
				public const string LkMsdynBgjobledgerCreatedonbehalfby = "lk_msdyn_bgjobledger_createdonbehalfby";
				public const string LkMsdynBgjobledgerModifiedby = "lk_msdyn_bgjobledger_modifiedby";
				public const string LkMsdynBgjobledgerModifiedonbehalfby = "lk_msdyn_bgjobledger_modifiedonbehalfby";
				public const string LkMsdynBookableresourceassociationCreatedby = "lk_msdyn_bookableresourceassociation_createdby";
				public const string LkMsdynBookableresourceassociationCreatedonbehalfby = "lk_msdyn_bookableresourceassociation_createdonbehalfby";
				public const string LkMsdynBookableresourceassociationModifiedby = "lk_msdyn_bookableresourceassociation_modifiedby";
				public const string LkMsdynBookableresourceassociationModifiedonbehalfby = "lk_msdyn_bookableresourceassociation_modifiedonbehalfby";
				public const string LkMsdynBookableresourcebookingquicknoteCreatedby = "lk_msdyn_bookableresourcebookingquicknote_createdby";
				public const string LkMsdynBookableresourcebookingquicknoteCreatedonbehalfby = "lk_msdyn_bookableresourcebookingquicknote_createdonbehalfby";
				public const string LkMsdynBookableresourcebookingquicknoteModifiedby = "lk_msdyn_bookableresourcebookingquicknote_modifiedby";
				public const string LkMsdynBookableresourcebookingquicknoteModifiedonbehalfby = "lk_msdyn_bookableresourcebookingquicknote_modifiedonbehalfby";
				public const string LkMsdynBookableresourcecapacityprofileCreatedby = "lk_msdyn_bookableresourcecapacityprofile_createdby";
				public const string LkMsdynBookableresourcecapacityprofileCreatedonbehalfby = "lk_msdyn_bookableresourcecapacityprofile_createdonbehalfby";
				public const string LkMsdynBookableresourcecapacityprofileModifiedby = "lk_msdyn_bookableresourcecapacityprofile_modifiedby";
				public const string LkMsdynBookableresourcecapacityprofileModifiedonbehalfby = "lk_msdyn_bookableresourcecapacityprofile_modifiedonbehalfby";
				public const string LkMsdynBookingalertstatusCreatedby = "lk_msdyn_bookingalertstatus_createdby";
				public const string LkMsdynBookingalertstatusCreatedonbehalfby = "lk_msdyn_bookingalertstatus_createdonbehalfby";
				public const string LkMsdynBookingalertstatusModifiedby = "lk_msdyn_bookingalertstatus_modifiedby";
				public const string LkMsdynBookingalertstatusModifiedonbehalfby = "lk_msdyn_bookingalertstatus_modifiedonbehalfby";
				public const string LkMsdynBookingchangeCreatedby = "lk_msdyn_bookingchange_createdby";
				public const string LkMsdynBookingchangeCreatedonbehalfby = "lk_msdyn_bookingchange_createdonbehalfby";
				public const string LkMsdynBookingchangeModifiedby = "lk_msdyn_bookingchange_modifiedby";
				public const string LkMsdynBookingchangeModifiedonbehalfby = "lk_msdyn_bookingchange_modifiedonbehalfby";
				public const string LkMsdynBookingjournalCreatedby = "lk_msdyn_bookingjournal_createdby";
				public const string LkMsdynBookingjournalCreatedonbehalfby = "lk_msdyn_bookingjournal_createdonbehalfby";
				public const string LkMsdynBookingjournalModifiedby = "lk_msdyn_bookingjournal_modifiedby";
				public const string LkMsdynBookingjournalModifiedonbehalfby = "lk_msdyn_bookingjournal_modifiedonbehalfby";
				public const string LkMsdynBookingruleCreatedby = "lk_msdyn_bookingrule_createdby";
				public const string LkMsdynBookingruleCreatedonbehalfby = "lk_msdyn_bookingrule_createdonbehalfby";
				public const string LkMsdynBookingruleModifiedby = "lk_msdyn_bookingrule_modifiedby";
				public const string LkMsdynBookingruleModifiedonbehalfby = "lk_msdyn_bookingrule_modifiedonbehalfby";
				public const string LkMsdynBookingsetupmetadataCreatedby = "lk_msdyn_bookingsetupmetadata_createdby";
				public const string LkMsdynBookingsetupmetadataCreatedonbehalfby = "lk_msdyn_bookingsetupmetadata_createdonbehalfby";
				public const string LkMsdynBookingsetupmetadataModifiedby = "lk_msdyn_bookingsetupmetadata_modifiedby";
				public const string LkMsdynBookingsetupmetadataModifiedonbehalfby = "lk_msdyn_bookingsetupmetadata_modifiedonbehalfby";
				public const string LkMsdynBookingtimestampCreatedby = "lk_msdyn_bookingtimestamp_createdby";
				public const string LkMsdynBookingtimestampCreatedonbehalfby = "lk_msdyn_bookingtimestamp_createdonbehalfby";
				public const string LkMsdynBookingtimestampModifiedby = "lk_msdyn_bookingtimestamp_modifiedby";
				public const string LkMsdynBookingtimestampModifiedonbehalfby = "lk_msdyn_bookingtimestamp_modifiedonbehalfby";
				public const string LkMsdynBotsessionCreatedby = "lk_msdyn_botsession_createdby";
				public const string LkMsdynBotsessionCreatedonbehalfby = "lk_msdyn_botsession_createdonbehalfby";
				public const string LkMsdynBotsessionModifiedby = "lk_msdyn_botsession_modifiedby";
				public const string LkMsdynBotsessionModifiedonbehalfby = "lk_msdyn_botsession_modifiedonbehalfby";
				public const string LkMsdynBpf2c5fe86acc8b414b8322ae571000c799Createdby = "lk_msdyn_bpf_2c5fe86acc8b414b8322ae571000c799_createdby";
				public const string LkMsdynBpf2c5fe86acc8b414b8322ae571000c799Createdonbehalfby = "lk_msdyn_bpf_2c5fe86acc8b414b8322ae571000c799_createdonbehalfby";
				public const string LkMsdynBpf2c5fe86acc8b414b8322ae571000c799Modifiedby = "lk_msdyn_bpf_2c5fe86acc8b414b8322ae571000c799_modifiedby";
				public const string LkMsdynBpf2c5fe86acc8b414b8322ae571000c799Modifiedonbehalfby = "lk_msdyn_bpf_2c5fe86acc8b414b8322ae571000c799_modifiedonbehalfby";
				public const string LkMsdynBpf477c16f59170487b8b4dc895c5dcd09bCreatedby = "lk_msdyn_bpf_477c16f59170487b8b4dc895c5dcd09b_createdby";
				public const string LkMsdynBpf477c16f59170487b8b4dc895c5dcd09bCreatedonbehalfby = "lk_msdyn_bpf_477c16f59170487b8b4dc895c5dcd09b_createdonbehalfby";
				public const string LkMsdynBpf477c16f59170487b8b4dc895c5dcd09bModifiedby = "lk_msdyn_bpf_477c16f59170487b8b4dc895c5dcd09b_modifiedby";
				public const string LkMsdynBpf477c16f59170487b8b4dc895c5dcd09bModifiedonbehalfby = "lk_msdyn_bpf_477c16f59170487b8b4dc895c5dcd09b_modifiedonbehalfby";
				public const string LkMsdynBpf665e73aa18c247d886bfc50499c73b82Createdby = "lk_msdyn_bpf_665e73aa18c247d886bfc50499c73b82_createdby";
				public const string LkMsdynBpf665e73aa18c247d886bfc50499c73b82Createdonbehalfby = "lk_msdyn_bpf_665e73aa18c247d886bfc50499c73b82_createdonbehalfby";
				public const string LkMsdynBpf665e73aa18c247d886bfc50499c73b82Modifiedby = "lk_msdyn_bpf_665e73aa18c247d886bfc50499c73b82_modifiedby";
				public const string LkMsdynBpf665e73aa18c247d886bfc50499c73b82Modifiedonbehalfby = "lk_msdyn_bpf_665e73aa18c247d886bfc50499c73b82_modifiedonbehalfby";
				public const string LkMsdynBpf989e9b1857e24af18787d5143b67523bCreatedby = "lk_msdyn_bpf_989e9b1857e24af18787d5143b67523b_createdby";
				public const string LkMsdynBpf989e9b1857e24af18787d5143b67523bCreatedonbehalfby = "lk_msdyn_bpf_989e9b1857e24af18787d5143b67523b_createdonbehalfby";
				public const string LkMsdynBpf989e9b1857e24af18787d5143b67523bModifiedby = "lk_msdyn_bpf_989e9b1857e24af18787d5143b67523b_modifiedby";
				public const string LkMsdynBpf989e9b1857e24af18787d5143b67523bModifiedonbehalfby = "lk_msdyn_bpf_989e9b1857e24af18787d5143b67523b_modifiedonbehalfby";
				public const string LkMsdynBpfBaa0a411a239410cb8bded8b5fdd88e3Createdby = "lk_msdyn_bpf_baa0a411a239410cb8bded8b5fdd88e3_createdby";
				public const string LkMsdynBpfBaa0a411a239410cb8bded8b5fdd88e3Createdonbehalfby = "lk_msdyn_bpf_baa0a411a239410cb8bded8b5fdd88e3_createdonbehalfby";
				public const string LkMsdynBpfBaa0a411a239410cb8bded8b5fdd88e3Modifiedby = "lk_msdyn_bpf_baa0a411a239410cb8bded8b5fdd88e3_modifiedby";
				public const string LkMsdynBpfBaa0a411a239410cb8bded8b5fdd88e3Modifiedonbehalfby = "lk_msdyn_bpf_baa0a411a239410cb8bded8b5fdd88e3_modifiedonbehalfby";
				public const string LkMsdynBpfD3d97bac8c294105840e99e37a9d1c39Createdby = "lk_msdyn_bpf_d3d97bac8c294105840e99e37a9d1c39_createdby";
				public const string LkMsdynBpfD3d97bac8c294105840e99e37a9d1c39Createdonbehalfby = "lk_msdyn_bpf_d3d97bac8c294105840e99e37a9d1c39_createdonbehalfby";
				public const string LkMsdynBpfD3d97bac8c294105840e99e37a9d1c39Modifiedby = "lk_msdyn_bpf_d3d97bac8c294105840e99e37a9d1c39_modifiedby";
				public const string LkMsdynBpfD3d97bac8c294105840e99e37a9d1c39Modifiedonbehalfby = "lk_msdyn_bpf_d3d97bac8c294105840e99e37a9d1c39_modifiedonbehalfby";
				public const string LkMsdynBpfD8f9dc7f099f44db9d641dd81fbd470dCreatedby = "lk_msdyn_bpf_d8f9dc7f099f44db9d641dd81fbd470d_createdby";
				public const string LkMsdynBpfD8f9dc7f099f44db9d641dd81fbd470dCreatedonbehalfby = "lk_msdyn_bpf_d8f9dc7f099f44db9d641dd81fbd470d_createdonbehalfby";
				public const string LkMsdynBpfD8f9dc7f099f44db9d641dd81fbd470dModifiedby = "lk_msdyn_bpf_d8f9dc7f099f44db9d641dd81fbd470d_modifiedby";
				public const string LkMsdynBpfD8f9dc7f099f44db9d641dd81fbd470dModifiedonbehalfby = "lk_msdyn_bpf_d8f9dc7f099f44db9d641dd81fbd470d_modifiedonbehalfby";
				public const string LkMsdynBusinessclosureCreatedby = "lk_msdyn_businessclosure_createdby";
				public const string LkMsdynBusinessclosureCreatedonbehalfby = "lk_msdyn_businessclosure_createdonbehalfby";
				public const string LkMsdynBusinessclosureModifiedby = "lk_msdyn_businessclosure_modifiedby";
				public const string LkMsdynBusinessclosureModifiedonbehalfby = "lk_msdyn_businessclosure_modifiedonbehalfby";
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
				public const string LkMsdynChannelCreatedby = "lk_msdyn_channel_createdby";
				public const string LkMsdynChannelCreatedonbehalfby = "lk_msdyn_channel_createdonbehalfby";
				public const string LkMsdynChannelModifiedby = "lk_msdyn_channel_modifiedby";
				public const string LkMsdynChannelModifiedonbehalfby = "lk_msdyn_channel_modifiedonbehalfby";
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
				public const string LkMsdynChannelmessageattachmentCreatedby = "lk_msdyn_channelmessageattachment_createdby";
				public const string LkMsdynChannelmessageattachmentCreatedonbehalfby = "lk_msdyn_channelmessageattachment_createdonbehalfby";
				public const string LkMsdynChannelmessageattachmentModifiedby = "lk_msdyn_channelmessageattachment_modifiedby";
				public const string LkMsdynChannelmessageattachmentModifiedonbehalfby = "lk_msdyn_channelmessageattachment_modifiedonbehalfby";
				public const string LkMsdynChannelmessagecontextpartCreatedby = "lk_msdyn_channelmessagecontextpart_createdby";
				public const string LkMsdynChannelmessagecontextpartCreatedonbehalfby = "lk_msdyn_channelmessagecontextpart_createdonbehalfby";
				public const string LkMsdynChannelmessagecontextpartModifiedby = "lk_msdyn_channelmessagecontextpart_modifiedby";
				public const string LkMsdynChannelmessagecontextpartModifiedonbehalfby = "lk_msdyn_channelmessagecontextpart_modifiedonbehalfby";
				public const string LkMsdynChannelmessagepartCreatedby = "lk_msdyn_channelmessagepart_createdby";
				public const string LkMsdynChannelmessagepartCreatedonbehalfby = "lk_msdyn_channelmessagepart_createdonbehalfby";
				public const string LkMsdynChannelmessagepartModifiedby = "lk_msdyn_channelmessagepart_modifiedby";
				public const string LkMsdynChannelmessagepartModifiedonbehalfby = "lk_msdyn_channelmessagepart_modifiedonbehalfby";
				public const string LkMsdynChannelproviderCreatedby = "lk_msdyn_channelprovider_createdby";
				public const string LkMsdynChannelproviderCreatedonbehalfby = "lk_msdyn_channelprovider_createdonbehalfby";
				public const string LkMsdynChannelproviderModifiedby = "lk_msdyn_channelprovider_modifiedby";
				public const string LkMsdynChannelproviderModifiedonbehalfby = "lk_msdyn_channelprovider_modifiedonbehalfby";
				public const string LkMsdynCharacteristicreqforteammemberCreatedby = "lk_msdyn_characteristicreqforteammember_createdby";
				public const string LkMsdynCharacteristicreqforteammemberCreatedonbehalfby = "lk_msdyn_characteristicreqforteammember_createdonbehalfby";
				public const string LkMsdynCharacteristicreqforteammemberModifiedby = "lk_msdyn_characteristicreqforteammember_modifiedby";
				public const string LkMsdynCharacteristicreqforteammemberModifiedonbehalfby = "lk_msdyn_characteristicreqforteammember_modifiedonbehalfby";
				public const string LkMsdynChatansweroptionCreatedby = "lk_msdyn_chatansweroption_createdby";
				public const string LkMsdynChatansweroptionCreatedonbehalfby = "lk_msdyn_chatansweroption_createdonbehalfby";
				public const string LkMsdynChatansweroptionModifiedby = "lk_msdyn_chatansweroption_modifiedby";
				public const string LkMsdynChatansweroptionModifiedonbehalfby = "lk_msdyn_chatansweroption_modifiedonbehalfby";
				public const string LkMsdynChatquestionnaireresponseCreatedby = "lk_msdyn_chatquestionnaireresponse_createdby";
				public const string LkMsdynChatquestionnaireresponseCreatedonbehalfby = "lk_msdyn_chatquestionnaireresponse_createdonbehalfby";
				public const string LkMsdynChatquestionnaireresponseModifiedby = "lk_msdyn_chatquestionnaireresponse_modifiedby";
				public const string LkMsdynChatquestionnaireresponseModifiedonbehalfby = "lk_msdyn_chatquestionnaireresponse_modifiedonbehalfby";
				public const string LkMsdynChatquestionnaireresponseitemCreatedby = "lk_msdyn_chatquestionnaireresponseitem_createdby";
				public const string LkMsdynChatquestionnaireresponseitemCreatedonbehalfby = "lk_msdyn_chatquestionnaireresponseitem_createdonbehalfby";
				public const string LkMsdynChatquestionnaireresponseitemModifiedby = "lk_msdyn_chatquestionnaireresponseitem_modifiedby";
				public const string LkMsdynChatquestionnaireresponseitemModifiedonbehalfby = "lk_msdyn_chatquestionnaireresponseitem_modifiedonbehalfby";
				public const string LkMsdynChatwidgetlanguageCreatedby = "lk_msdyn_chatwidgetlanguage_createdby";
				public const string LkMsdynChatwidgetlanguageCreatedonbehalfby = "lk_msdyn_chatwidgetlanguage_createdonbehalfby";
				public const string LkMsdynChatwidgetlanguageModifiedby = "lk_msdyn_chatwidgetlanguage_modifiedby";
				public const string LkMsdynChatwidgetlanguageModifiedonbehalfby = "lk_msdyn_chatwidgetlanguage_modifiedonbehalfby";
				public const string LkMsdynCiproviderCreatedby = "lk_msdyn_ciprovider_createdby";
				public const string LkMsdynCiproviderCreatedonbehalfby = "lk_msdyn_ciprovider_createdonbehalfby";
				public const string LkMsdynCiproviderModifiedby = "lk_msdyn_ciprovider_modifiedby";
				public const string LkMsdynCiproviderModifiedonbehalfby = "lk_msdyn_ciprovider_modifiedonbehalfby";
				public const string LkMsdynClientextensionCreatedby = "lk_msdyn_clientextension_createdby";
				public const string LkMsdynClientextensionCreatedonbehalfby = "lk_msdyn_clientextension_createdonbehalfby";
				public const string LkMsdynClientextensionModifiedby = "lk_msdyn_clientextension_modifiedby";
				public const string LkMsdynClientextensionModifiedonbehalfby = "lk_msdyn_clientextension_modifiedonbehalfby";
				public const string LkMsdynCollabgraphresourceCreatedby = "lk_msdyn_collabgraphresource_createdby";
				public const string LkMsdynCollabgraphresourceCreatedonbehalfby = "lk_msdyn_collabgraphresource_createdonbehalfby";
				public const string LkMsdynCollabgraphresourceModifiedby = "lk_msdyn_collabgraphresource_modifiedby";
				public const string LkMsdynCollabgraphresourceModifiedonbehalfby = "lk_msdyn_collabgraphresource_modifiedonbehalfby";
				public const string LkMsdynCollabspaceteamassociationCreatedby = "lk_msdyn_collabspaceteamassociation_createdby";
				public const string LkMsdynCollabspaceteamassociationCreatedonbehalfby = "lk_msdyn_collabspaceteamassociation_createdonbehalfby";
				public const string LkMsdynCollabspaceteamassociationModifiedby = "lk_msdyn_collabspaceteamassociation_modifiedby";
				public const string LkMsdynCollabspaceteamassociationModifiedonbehalfby = "lk_msdyn_collabspaceteamassociation_modifiedonbehalfby";
				public const string LkMsdynConfigurationCreatedby = "lk_msdyn_configuration_createdby";
				public const string LkMsdynConfigurationCreatedonbehalfby = "lk_msdyn_configuration_createdonbehalfby";
				public const string LkMsdynConfigurationModifiedby = "lk_msdyn_configuration_modifiedby";
				public const string LkMsdynConfigurationModifiedonbehalfby = "lk_msdyn_configuration_modifiedonbehalfby";
				public const string LkMsdynConsoleapplicationnotificationfieldCreatedby = "lk_msdyn_consoleapplicationnotificationfield_createdby";
				public const string LkMsdynConsoleapplicationnotificationfieldCreatedonbehalfby = "lk_msdyn_consoleapplicationnotificationfield_createdonbehalfby";
				public const string LkMsdynConsoleapplicationnotificationfieldModifiedby = "lk_msdyn_consoleapplicationnotificationfield_modifiedby";
				public const string LkMsdynConsoleapplicationnotificationfieldModifiedonbehalfby = "lk_msdyn_consoleapplicationnotificationfield_modifiedonbehalfby";
				public const string LkMsdynConsoleapplicationnotificationtemplateCreatedby = "lk_msdyn_consoleapplicationnotificationtemplate_createdby";
				public const string LkMsdynConsoleapplicationnotificationtemplateCreatedonbehalfby = "lk_msdyn_consoleapplicationnotificationtemplate_createdonbehalfby";
				public const string LkMsdynConsoleapplicationnotificationtemplateModifiedby = "lk_msdyn_consoleapplicationnotificationtemplate_modifiedby";
				public const string LkMsdynConsoleapplicationnotificationtemplateModifiedonbehalfby = "lk_msdyn_consoleapplicationnotificationtemplate_modifiedonbehalfby";
				public const string LkMsdynConsoleapplicationsessiontemplateCreatedby = "lk_msdyn_consoleapplicationsessiontemplate_createdby";
				public const string LkMsdynConsoleapplicationsessiontemplateCreatedonbehalfby = "lk_msdyn_consoleapplicationsessiontemplate_createdonbehalfby";
				public const string LkMsdynConsoleapplicationsessiontemplateModifiedby = "lk_msdyn_consoleapplicationsessiontemplate_modifiedby";
				public const string LkMsdynConsoleapplicationsessiontemplateModifiedonbehalfby = "lk_msdyn_consoleapplicationsessiontemplate_modifiedonbehalfby";
				public const string LkMsdynConsoleapplicationtemplateCreatedby = "lk_msdyn_consoleapplicationtemplate_createdby";
				public const string LkMsdynConsoleapplicationtemplateCreatedonbehalfby = "lk_msdyn_consoleapplicationtemplate_createdonbehalfby";
				public const string LkMsdynConsoleapplicationtemplateModifiedby = "lk_msdyn_consoleapplicationtemplate_modifiedby";
				public const string LkMsdynConsoleapplicationtemplateModifiedonbehalfby = "lk_msdyn_consoleapplicationtemplate_modifiedonbehalfby";
				public const string LkMsdynConsoleapplicationtemplateparameterCreatedby = "lk_msdyn_consoleapplicationtemplateparameter_createdby";
				public const string LkMsdynConsoleapplicationtemplateparameterCreatedonbehalfby = "lk_msdyn_consoleapplicationtemplateparameter_createdonbehalfby";
				public const string LkMsdynConsoleapplicationtemplateparameterModifiedby = "lk_msdyn_consoleapplicationtemplateparameter_modifiedby";
				public const string LkMsdynConsoleapplicationtemplateparameterModifiedonbehalfby = "lk_msdyn_consoleapplicationtemplateparameter_modifiedonbehalfby";
				public const string LkMsdynConsoleapplicationtypeCreatedby = "lk_msdyn_consoleapplicationtype_createdby";
				public const string LkMsdynConsoleapplicationtypeCreatedonbehalfby = "lk_msdyn_consoleapplicationtype_createdonbehalfby";
				public const string LkMsdynConsoleapplicationtypeModifiedby = "lk_msdyn_consoleapplicationtype_modifiedby";
				public const string LkMsdynConsoleapplicationtypeModifiedonbehalfby = "lk_msdyn_consoleapplicationtype_modifiedonbehalfby";
				public const string LkMsdynConsoleappparameterdefinitionCreatedby = "lk_msdyn_consoleappparameterdefinition_createdby";
				public const string LkMsdynConsoleappparameterdefinitionCreatedonbehalfby = "lk_msdyn_consoleappparameterdefinition_createdonbehalfby";
				public const string LkMsdynConsoleappparameterdefinitionModifiedby = "lk_msdyn_consoleappparameterdefinition_modifiedby";
				public const string LkMsdynConsoleappparameterdefinitionModifiedonbehalfby = "lk_msdyn_consoleappparameterdefinition_modifiedonbehalfby";
				public const string LkMsdynConsumingapplicationCreatedby = "lk_msdyn_consumingapplication_createdby";
				public const string LkMsdynConsumingapplicationCreatedonbehalfby = "lk_msdyn_consumingapplication_createdonbehalfby";
				public const string LkMsdynConsumingapplicationModifiedby = "lk_msdyn_consumingapplication_modifiedby";
				public const string LkMsdynConsumingapplicationModifiedonbehalfby = "lk_msdyn_consumingapplication_modifiedonbehalfby";
				public const string LkMsdynContactkpiitemCreatedby = "lk_msdyn_contactkpiitem_createdby";
				public const string LkMsdynContactkpiitemCreatedonbehalfby = "lk_msdyn_contactkpiitem_createdonbehalfby";
				public const string LkMsdynContactkpiitemModifiedby = "lk_msdyn_contactkpiitem_modifiedby";
				public const string LkMsdynContactkpiitemModifiedonbehalfby = "lk_msdyn_contactkpiitem_modifiedonbehalfby";
				public const string LkMsdynContactpricelistCreatedby = "lk_msdyn_contactpricelist_createdby";
				public const string LkMsdynContactpricelistCreatedonbehalfby = "lk_msdyn_contactpricelist_createdonbehalfby";
				public const string LkMsdynContactpricelistModifiedby = "lk_msdyn_contactpricelist_modifiedby";
				public const string LkMsdynContactpricelistModifiedonbehalfby = "lk_msdyn_contactpricelist_modifiedonbehalfby";
				public const string LkMsdynContactsuggestionruleCreatedby = "lk_msdyn_contactsuggestionrule_createdby";
				public const string LkMsdynContactsuggestionruleCreatedonbehalfby = "lk_msdyn_contactsuggestionrule_createdonbehalfby";
				public const string LkMsdynContactsuggestionruleModifiedby = "lk_msdyn_contactsuggestionrule_modifiedby";
				public const string LkMsdynContactsuggestionruleModifiedonbehalfby = "lk_msdyn_contactsuggestionrule_modifiedonbehalfby";
				public const string LkMsdynContactsuggestionrulesetCreatedby = "lk_msdyn_contactsuggestionruleset_createdby";
				public const string LkMsdynContactsuggestionrulesetCreatedonbehalfby = "lk_msdyn_contactsuggestionruleset_createdonbehalfby";
				public const string LkMsdynContactsuggestionrulesetModifiedby = "lk_msdyn_contactsuggestionruleset_modifiedby";
				public const string LkMsdynContactsuggestionrulesetModifiedonbehalfby = "lk_msdyn_contactsuggestionruleset_modifiedonbehalfby";
				public const string LkMsdynContractlinedetailperformanceCreatedby = "lk_msdyn_contractlinedetailperformance_createdby";
				public const string LkMsdynContractlinedetailperformanceCreatedonbehalfby = "lk_msdyn_contractlinedetailperformance_createdonbehalfby";
				public const string LkMsdynContractlinedetailperformanceModifiedby = "lk_msdyn_contractlinedetailperformance_modifiedby";
				public const string LkMsdynContractlinedetailperformanceModifiedonbehalfby = "lk_msdyn_contractlinedetailperformance_modifiedonbehalfby";
				public const string LkMsdynContractlineinvoicescheduleCreatedby = "lk_msdyn_contractlineinvoiceschedule_createdby";
				public const string LkMsdynContractlineinvoicescheduleCreatedonbehalfby = "lk_msdyn_contractlineinvoiceschedule_createdonbehalfby";
				public const string LkMsdynContractlineinvoicescheduleModifiedby = "lk_msdyn_contractlineinvoiceschedule_modifiedby";
				public const string LkMsdynContractlineinvoicescheduleModifiedonbehalfby = "lk_msdyn_contractlineinvoiceschedule_modifiedonbehalfby";
				public const string LkMsdynContractlinescheduleofvalueCreatedby = "lk_msdyn_contractlinescheduleofvalue_createdby";
				public const string LkMsdynContractlinescheduleofvalueCreatedonbehalfby = "lk_msdyn_contractlinescheduleofvalue_createdonbehalfby";
				public const string LkMsdynContractlinescheduleofvalueModifiedby = "lk_msdyn_contractlinescheduleofvalue_modifiedby";
				public const string LkMsdynContractlinescheduleofvalueModifiedonbehalfby = "lk_msdyn_contractlinescheduleofvalue_modifiedonbehalfby";
				public const string LkMsdynContractperformanceCreatedby = "lk_msdyn_contractperformance_createdby";
				public const string LkMsdynContractperformanceCreatedonbehalfby = "lk_msdyn_contractperformance_createdonbehalfby";
				public const string LkMsdynContractperformanceModifiedby = "lk_msdyn_contractperformance_modifiedby";
				public const string LkMsdynContractperformanceModifiedonbehalfby = "lk_msdyn_contractperformance_modifiedonbehalfby";
				public const string LkMsdynConversationactionCreatedby = "lk_msdyn_conversationaction_createdby";
				public const string LkMsdynConversationactionCreatedonbehalfby = "lk_msdyn_conversationaction_createdonbehalfby";
				public const string LkMsdynConversationactionModifiedby = "lk_msdyn_conversationaction_modifiedby";
				public const string LkMsdynConversationactionModifiedonbehalfby = "lk_msdyn_conversationaction_modifiedonbehalfby";
				public const string LkMsdynConversationactionitemCreatedby = "lk_msdyn_conversationactionitem_createdby";
				public const string LkMsdynConversationactionitemCreatedonbehalfby = "lk_msdyn_conversationactionitem_createdonbehalfby";
				public const string LkMsdynConversationactionitemModifiedby = "lk_msdyn_conversationactionitem_modifiedby";
				public const string LkMsdynConversationactionitemModifiedonbehalfby = "lk_msdyn_conversationactionitem_modifiedonbehalfby";
				public const string LkMsdynConversationactionlocaleCreatedby = "lk_msdyn_conversationactionlocale_createdby";
				public const string LkMsdynConversationactionlocaleCreatedonbehalfby = "lk_msdyn_conversationactionlocale_createdonbehalfby";
				public const string LkMsdynConversationactionlocaleModifiedby = "lk_msdyn_conversationactionlocale_modifiedby";
				public const string LkMsdynConversationactionlocaleModifiedonbehalfby = "lk_msdyn_conversationactionlocale_modifiedonbehalfby";
				public const string LkMsdynConversationaggregatedinsightsCreatedby = "lk_msdyn_conversationaggregatedinsights_createdby";
				public const string LkMsdynConversationaggregatedinsightsCreatedonbehalfby = "lk_msdyn_conversationaggregatedinsights_createdonbehalfby";
				public const string LkMsdynConversationaggregatedinsightsModifiedby = "lk_msdyn_conversationaggregatedinsights_modifiedby";
				public const string LkMsdynConversationaggregatedinsightsModifiedonbehalfby = "lk_msdyn_conversationaggregatedinsights_modifiedonbehalfby";
				public const string LkMsdynConversationcommentCreatedby = "lk_msdyn_conversationcomment_createdby";
				public const string LkMsdynConversationcommentCreatedonbehalfby = "lk_msdyn_conversationcomment_createdonbehalfby";
				public const string LkMsdynConversationcommentModifiedby = "lk_msdyn_conversationcomment_modifiedby";
				public const string LkMsdynConversationcommentModifiedonbehalfby = "lk_msdyn_conversationcomment_modifiedonbehalfby";
				public const string LkMsdynConversationdataCreatedby = "lk_msdyn_conversationdata_createdby";
				public const string LkMsdynConversationdataCreatedonbehalfby = "lk_msdyn_conversationdata_createdonbehalfby";
				public const string LkMsdynConversationdataModifiedby = "lk_msdyn_conversationdata_modifiedby";
				public const string LkMsdynConversationdataModifiedonbehalfby = "lk_msdyn_conversationdata_modifiedonbehalfby";
				public const string LkMsdynConversationinsightCreatedby = "lk_msdyn_conversationinsight_createdby";
				public const string LkMsdynConversationinsightCreatedonbehalfby = "lk_msdyn_conversationinsight_createdonbehalfby";
				public const string LkMsdynConversationinsightModifiedby = "lk_msdyn_conversationinsight_modifiedby";
				public const string LkMsdynConversationinsightModifiedonbehalfby = "lk_msdyn_conversationinsight_modifiedonbehalfby";
				public const string LkMsdynConversationmessageblockCreatedby = "lk_msdyn_conversationmessageblock_createdby";
				public const string LkMsdynConversationmessageblockCreatedonbehalfby = "lk_msdyn_conversationmessageblock_createdonbehalfby";
				public const string LkMsdynConversationmessageblockModifiedby = "lk_msdyn_conversationmessageblock_modifiedby";
				public const string LkMsdynConversationmessageblockModifiedonbehalfby = "lk_msdyn_conversationmessageblock_modifiedonbehalfby";
				public const string LkMsdynConversationparticipantinsightsCreatedby = "lk_msdyn_conversationparticipantinsights_createdby";
				public const string LkMsdynConversationparticipantinsightsCreatedonbehalfby = "lk_msdyn_conversationparticipantinsights_createdonbehalfby";
				public const string LkMsdynConversationparticipantinsightsModifiedby = "lk_msdyn_conversationparticipantinsights_modifiedby";
				public const string LkMsdynConversationparticipantinsightsModifiedonbehalfby = "lk_msdyn_conversationparticipantinsights_modifiedonbehalfby";
				public const string LkMsdynConversationparticipantsentimentCreatedby = "lk_msdyn_conversationparticipantsentiment_createdby";
				public const string LkMsdynConversationparticipantsentimentCreatedonbehalfby = "lk_msdyn_conversationparticipantsentiment_createdonbehalfby";
				public const string LkMsdynConversationparticipantsentimentModifiedby = "lk_msdyn_conversationparticipantsentiment_modifiedby";
				public const string LkMsdynConversationparticipantsentimentModifiedonbehalfby = "lk_msdyn_conversationparticipantsentiment_modifiedonbehalfby";
				public const string LkMsdynConversationquestionCreatedby = "lk_msdyn_conversationquestion_createdby";
				public const string LkMsdynConversationquestionCreatedonbehalfby = "lk_msdyn_conversationquestion_createdonbehalfby";
				public const string LkMsdynConversationquestionModifiedby = "lk_msdyn_conversationquestion_modifiedby";
				public const string LkMsdynConversationquestionModifiedonbehalfby = "lk_msdyn_conversationquestion_modifiedonbehalfby";
				public const string LkMsdynConversationsegmentsentimentCreatedby = "lk_msdyn_conversationsegmentsentiment_createdby";
				public const string LkMsdynConversationsegmentsentimentCreatedonbehalfby = "lk_msdyn_conversationsegmentsentiment_createdonbehalfby";
				public const string LkMsdynConversationsegmentsentimentModifiedby = "lk_msdyn_conversationsegmentsentiment_modifiedby";
				public const string LkMsdynConversationsegmentsentimentModifiedonbehalfby = "lk_msdyn_conversationsegmentsentiment_modifiedonbehalfby";
				public const string LkMsdynConversationsentimentCreatedby = "lk_msdyn_conversationsentiment_createdby";
				public const string LkMsdynConversationsentimentCreatedonbehalfby = "lk_msdyn_conversationsentiment_createdonbehalfby";
				public const string LkMsdynConversationsentimentModifiedby = "lk_msdyn_conversationsentiment_modifiedby";
				public const string LkMsdynConversationsentimentModifiedonbehalfby = "lk_msdyn_conversationsentiment_modifiedonbehalfby";
				public const string LkMsdynConversationsignalCreatedby = "lk_msdyn_conversationsignal_createdby";
				public const string LkMsdynConversationsignalCreatedonbehalfby = "lk_msdyn_conversationsignal_createdonbehalfby";
				public const string LkMsdynConversationsignalModifiedby = "lk_msdyn_conversationsignal_modifiedby";
				public const string LkMsdynConversationsignalModifiedonbehalfby = "lk_msdyn_conversationsignal_modifiedonbehalfby";
				public const string LkMsdynConversationsubjectCreatedby = "lk_msdyn_conversationsubject_createdby";
				public const string LkMsdynConversationsubjectCreatedonbehalfby = "lk_msdyn_conversationsubject_createdonbehalfby";
				public const string LkMsdynConversationsubjectModifiedby = "lk_msdyn_conversationsubject_modifiedby";
				public const string LkMsdynConversationsubjectModifiedonbehalfby = "lk_msdyn_conversationsubject_modifiedonbehalfby";
				public const string LkMsdynConversationsuggestionrequestpayloadCreatedby = "lk_msdyn_conversationsuggestionrequestpayload_createdby";
				public const string LkMsdynConversationsuggestionrequestpayloadCreatedonbehalfby = "lk_msdyn_conversationsuggestionrequestpayload_createdonbehalfby";
				public const string LkMsdynConversationsuggestionrequestpayloadModifiedby = "lk_msdyn_conversationsuggestionrequestpayload_modifiedby";
				public const string LkMsdynConversationsuggestionrequestpayloadModifiedonbehalfby = "lk_msdyn_conversationsuggestionrequestpayload_modifiedonbehalfby";
				public const string LkMsdynConversationsummaryinteractionCreatedby = "lk_msdyn_conversationsummaryinteraction_createdby";
				public const string LkMsdynConversationsummaryinteractionCreatedonbehalfby = "lk_msdyn_conversationsummaryinteraction_createdonbehalfby";
				public const string LkMsdynConversationsummaryinteractionModifiedby = "lk_msdyn_conversationsummaryinteraction_modifiedby";
				public const string LkMsdynConversationsummaryinteractionModifiedonbehalfby = "lk_msdyn_conversationsummaryinteraction_modifiedonbehalfby";
				public const string LkMsdynConversationsummarysettingCreatedby = "lk_msdyn_conversationsummarysetting_createdby";
				public const string LkMsdynConversationsummarysettingCreatedonbehalfby = "lk_msdyn_conversationsummarysetting_createdonbehalfby";
				public const string LkMsdynConversationsummarysettingModifiedby = "lk_msdyn_conversationsummarysetting_modifiedby";
				public const string LkMsdynConversationsummarysettingModifiedonbehalfby = "lk_msdyn_conversationsummarysetting_modifiedonbehalfby";
				public const string LkMsdynConversationsummarysuggestionCreatedby = "lk_msdyn_conversationsummarysuggestion_createdby";
				public const string LkMsdynConversationsummarysuggestionCreatedonbehalfby = "lk_msdyn_conversationsummarysuggestion_createdonbehalfby";
				public const string LkMsdynConversationsummarysuggestionModifiedby = "lk_msdyn_conversationsummarysuggestion_modifiedby";
				public const string LkMsdynConversationsummarysuggestionModifiedonbehalfby = "lk_msdyn_conversationsummarysuggestion_modifiedonbehalfby";
				public const string LkMsdynConversationsystemtagCreatedby = "lk_msdyn_conversationsystemtag_createdby";
				public const string LkMsdynConversationsystemtagCreatedonbehalfby = "lk_msdyn_conversationsystemtag_createdonbehalfby";
				public const string LkMsdynConversationsystemtagModifiedby = "lk_msdyn_conversationsystemtag_modifiedby";
				public const string LkMsdynConversationsystemtagModifiedonbehalfby = "lk_msdyn_conversationsystemtag_modifiedonbehalfby";
				public const string LkMsdynConversationtagCreatedby = "lk_msdyn_conversationtag_createdby";
				public const string LkMsdynConversationtagCreatedonbehalfby = "lk_msdyn_conversationtag_createdonbehalfby";
				public const string LkMsdynConversationtagModifiedby = "lk_msdyn_conversationtag_modifiedby";
				public const string LkMsdynConversationtagModifiedonbehalfby = "lk_msdyn_conversationtag_modifiedonbehalfby";
				public const string LkMsdynConversationtopicConversationCreatedby = "lk_msdyn_conversationtopic_conversation_createdby";
				public const string LkMsdynConversationtopicConversationCreatedonbehalfby = "lk_msdyn_conversationtopic_conversation_createdonbehalfby";
				public const string LkMsdynConversationtopicConversationModifiedby = "lk_msdyn_conversationtopic_conversation_modifiedby";
				public const string LkMsdynConversationtopicConversationModifiedonbehalfby = "lk_msdyn_conversationtopic_conversation_modifiedonbehalfby";
				public const string LkMsdynConversationtopicCreatedby = "lk_msdyn_conversationtopic_createdby";
				public const string LkMsdynConversationtopicCreatedonbehalfby = "lk_msdyn_conversationtopic_createdonbehalfby";
				public const string LkMsdynConversationtopicModifiedby = "lk_msdyn_conversationtopic_modifiedby";
				public const string LkMsdynConversationtopicModifiedonbehalfby = "lk_msdyn_conversationtopic_modifiedonbehalfby";
				public const string LkMsdynConversationtopicsettingCreatedby = "lk_msdyn_conversationtopicsetting_createdby";
				public const string LkMsdynConversationtopicsettingCreatedonbehalfby = "lk_msdyn_conversationtopicsetting_createdonbehalfby";
				public const string LkMsdynConversationtopicsettingModifiedby = "lk_msdyn_conversationtopicsetting_modifiedby";
				public const string LkMsdynConversationtopicsettingModifiedonbehalfby = "lk_msdyn_conversationtopicsetting_modifiedonbehalfby";
				public const string LkMsdynConversationtopicsummaryCreatedby = "lk_msdyn_conversationtopicsummary_createdby";
				public const string LkMsdynConversationtopicsummaryCreatedonbehalfby = "lk_msdyn_conversationtopicsummary_createdonbehalfby";
				public const string LkMsdynConversationtopicsummaryModifiedby = "lk_msdyn_conversationtopicsummary_modifiedby";
				public const string LkMsdynConversationtopicsummaryModifiedonbehalfby = "lk_msdyn_conversationtopicsummary_modifiedonbehalfby";
				public const string LkMsdynCopilotagentpreferenceCreatedby = "lk_msdyn_copilotagentpreference_createdby";
				public const string LkMsdynCopilotagentpreferenceCreatedonbehalfby = "lk_msdyn_copilotagentpreference_createdonbehalfby";
				public const string LkMsdynCopilotagentpreferenceModifiedby = "lk_msdyn_copilotagentpreference_modifiedby";
				public const string LkMsdynCopilotagentpreferenceModifiedonbehalfby = "lk_msdyn_copilotagentpreference_modifiedonbehalfby";
				public const string LkMsdynCopilotinteractionCreatedby = "lk_msdyn_copilotinteraction_createdby";
				public const string LkMsdynCopilotinteractionCreatedonbehalfby = "lk_msdyn_copilotinteraction_createdonbehalfby";
				public const string LkMsdynCopilotinteractionModifiedby = "lk_msdyn_copilotinteraction_modifiedby";
				public const string LkMsdynCopilotinteractionModifiedonbehalfby = "lk_msdyn_copilotinteraction_modifiedonbehalfby";
				public const string LkMsdynCopilotinteractiondataCreatedby = "lk_msdyn_copilotinteractiondata_createdby";
				public const string LkMsdynCopilotinteractiondataCreatedonbehalfby = "lk_msdyn_copilotinteractiondata_createdonbehalfby";
				public const string LkMsdynCopilotinteractiondataModifiedby = "lk_msdyn_copilotinteractiondata_modifiedby";
				public const string LkMsdynCopilotinteractiondataModifiedonbehalfby = "lk_msdyn_copilotinteractiondata_modifiedonbehalfby";
				public const string LkMsdynCopilotsummarizationsettingCreatedby = "lk_msdyn_copilotsummarizationsetting_createdby";
				public const string LkMsdynCopilotsummarizationsettingCreatedonbehalfby = "lk_msdyn_copilotsummarizationsetting_createdonbehalfby";
				public const string LkMsdynCopilotsummarizationsettingModifiedby = "lk_msdyn_copilotsummarizationsetting_modifiedby";
				public const string LkMsdynCopilotsummarizationsettingModifiedonbehalfby = "lk_msdyn_copilotsummarizationsetting_modifiedonbehalfby";
				public const string LkMsdynCopilottranscriptdataCreatedby = "lk_msdyn_copilottranscriptdata_createdby";
				public const string LkMsdynCopilottranscriptdataCreatedonbehalfby = "lk_msdyn_copilottranscriptdata_createdonbehalfby";
				public const string LkMsdynCopilottranscriptdataModifiedby = "lk_msdyn_copilottranscriptdata_modifiedby";
				public const string LkMsdynCopilottranscriptdataModifiedonbehalfby = "lk_msdyn_copilottranscriptdata_modifiedonbehalfby";
				public const string LkMsdynCrmconnectionCreatedby = "lk_msdyn_crmconnection_createdby";
				public const string LkMsdynCrmconnectionCreatedonbehalfby = "lk_msdyn_crmconnection_createdonbehalfby";
				public const string LkMsdynCrmconnectionModifiedby = "lk_msdyn_crmconnection_modifiedby";
				public const string LkMsdynCrmconnectionModifiedonbehalfby = "lk_msdyn_crmconnection_modifiedonbehalfby";
				public const string LkMsdynCsadminconfigCreatedby = "lk_msdyn_csadminconfig_createdby";
				public const string LkMsdynCsadminconfigCreatedonbehalfby = "lk_msdyn_csadminconfig_createdonbehalfby";
				public const string LkMsdynCsadminconfigModifiedby = "lk_msdyn_csadminconfig_modifiedby";
				public const string LkMsdynCsadminconfigModifiedonbehalfby = "lk_msdyn_csadminconfig_modifiedonbehalfby";
				public const string LkMsdynCskeyvalueconfigCreatedby = "lk_msdyn_cskeyvalueconfig_createdby";
				public const string LkMsdynCskeyvalueconfigCreatedonbehalfby = "lk_msdyn_cskeyvalueconfig_createdonbehalfby";
				public const string LkMsdynCskeyvalueconfigModifiedby = "lk_msdyn_cskeyvalueconfig_modifiedby";
				public const string LkMsdynCskeyvalueconfigModifiedonbehalfby = "lk_msdyn_cskeyvalueconfig_modifiedonbehalfby";
				public const string LkMsdynCustomapirulesetconfigurationCreatedby = "lk_msdyn_customapirulesetconfiguration_createdby";
				public const string LkMsdynCustomapirulesetconfigurationCreatedonbehalfby = "lk_msdyn_customapirulesetconfiguration_createdonbehalfby";
				public const string LkMsdynCustomapirulesetconfigurationModifiedby = "lk_msdyn_customapirulesetconfiguration_modifiedby";
				public const string LkMsdynCustomapirulesetconfigurationModifiedonbehalfby = "lk_msdyn_customapirulesetconfiguration_modifiedonbehalfby";
				public const string LkMsdynCustomcontrolextendedsettingsCreatedby = "lk_msdyn_customcontrolextendedsettings_createdby";
				public const string LkMsdynCustomcontrolextendedsettingsCreatedonbehalfby = "lk_msdyn_customcontrolextendedsettings_createdonbehalfby";
				public const string LkMsdynCustomcontrolextendedsettingsModifiedby = "lk_msdyn_customcontrolextendedsettings_modifiedby";
				public const string LkMsdynCustomcontrolextendedsettingsModifiedonbehalfby = "lk_msdyn_customcontrolextendedsettings_modifiedonbehalfby";
				public const string LkMsdynCustomengagementctxCreatedby = "lk_msdyn_customengagementctx_createdby";
				public const string LkMsdynCustomengagementctxCreatedonbehalfby = "lk_msdyn_customengagementctx_createdonbehalfby";
				public const string LkMsdynCustomengagementctxModifiedby = "lk_msdyn_customengagementctx_modifiedby";
				public const string LkMsdynCustomengagementctxModifiedonbehalfby = "lk_msdyn_customengagementctx_modifiedonbehalfby";
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
				public const string LkMsdynDailyaccountkpiitemCreatedby = "lk_msdyn_dailyaccountkpiitem_createdby";
				public const string LkMsdynDailyaccountkpiitemCreatedonbehalfby = "lk_msdyn_dailyaccountkpiitem_createdonbehalfby";
				public const string LkMsdynDailyaccountkpiitemModifiedby = "lk_msdyn_dailyaccountkpiitem_modifiedby";
				public const string LkMsdynDailyaccountkpiitemModifiedonbehalfby = "lk_msdyn_dailyaccountkpiitem_modifiedonbehalfby";
				public const string LkMsdynDailycontactkpiitemCreatedby = "lk_msdyn_dailycontactkpiitem_createdby";
				public const string LkMsdynDailycontactkpiitemCreatedonbehalfby = "lk_msdyn_dailycontactkpiitem_createdonbehalfby";
				public const string LkMsdynDailycontactkpiitemModifiedby = "lk_msdyn_dailycontactkpiitem_modifiedby";
				public const string LkMsdynDailycontactkpiitemModifiedonbehalfby = "lk_msdyn_dailycontactkpiitem_modifiedonbehalfby";
				public const string LkMsdynDailyleadkpiitemCreatedby = "lk_msdyn_dailyleadkpiitem_createdby";
				public const string LkMsdynDailyleadkpiitemCreatedonbehalfby = "lk_msdyn_dailyleadkpiitem_createdonbehalfby";
				public const string LkMsdynDailyleadkpiitemModifiedby = "lk_msdyn_dailyleadkpiitem_modifiedby";
				public const string LkMsdynDailyleadkpiitemModifiedonbehalfby = "lk_msdyn_dailyleadkpiitem_modifiedonbehalfby";
				public const string LkMsdynDailyopportunitykpiitemCreatedby = "lk_msdyn_dailyopportunitykpiitem_createdby";
				public const string LkMsdynDailyopportunitykpiitemCreatedonbehalfby = "lk_msdyn_dailyopportunitykpiitem_createdonbehalfby";
				public const string LkMsdynDailyopportunitykpiitemModifiedby = "lk_msdyn_dailyopportunitykpiitem_modifiedby";
				public const string LkMsdynDailyopportunitykpiitemModifiedonbehalfby = "lk_msdyn_dailyopportunitykpiitem_modifiedonbehalfby";
				public const string LkMsdynDataanalyticscustomizedreportCreatedby = "lk_msdyn_dataanalyticscustomizedreport_createdby";
				public const string LkMsdynDataanalyticscustomizedreportCreatedonbehalfby = "lk_msdyn_dataanalyticscustomizedreport_createdonbehalfby";
				public const string LkMsdynDataanalyticscustomizedreportModifiedby = "lk_msdyn_dataanalyticscustomizedreport_modifiedby";
				public const string LkMsdynDataanalyticscustomizedreportModifiedonbehalfby = "lk_msdyn_dataanalyticscustomizedreport_modifiedonbehalfby";
				public const string LkMsdynDataanalyticsdatasetCreatedby = "lk_msdyn_dataanalyticsdataset_createdby";
				public const string LkMsdynDataanalyticsdatasetCreatedonbehalfby = "lk_msdyn_dataanalyticsdataset_createdonbehalfby";
				public const string LkMsdynDataanalyticsdatasetModifiedby = "lk_msdyn_dataanalyticsdataset_modifiedby";
				public const string LkMsdynDataanalyticsdatasetModifiedonbehalfby = "lk_msdyn_dataanalyticsdataset_modifiedonbehalfby";
				public const string LkMsdynDataanalyticsreportCopilotCreatedby = "lk_msdyn_dataanalyticsreport_copilot_createdby";
				public const string LkMsdynDataanalyticsreportCopilotCreatedonbehalfby = "lk_msdyn_dataanalyticsreport_copilot_createdonbehalfby";
				public const string LkMsdynDataanalyticsreportCopilotModifiedby = "lk_msdyn_dataanalyticsreport_copilot_modifiedby";
				public const string LkMsdynDataanalyticsreportCopilotModifiedonbehalfby = "lk_msdyn_dataanalyticsreport_copilot_modifiedonbehalfby";
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
				public const string LkMsdynDataanalyticsreportFsCreatedby = "lk_msdyn_dataanalyticsreport_fs_createdby";
				public const string LkMsdynDataanalyticsreportFsCreatedonbehalfby = "lk_msdyn_dataanalyticsreport_fs_createdonbehalfby";
				public const string LkMsdynDataanalyticsreportFsModifiedby = "lk_msdyn_dataanalyticsreport_fs_modifiedby";
				public const string LkMsdynDataanalyticsreportFsModifiedonbehalfby = "lk_msdyn_dataanalyticsreport_fs_modifiedonbehalfby";
				public const string LkMsdynDataanalyticsreportFspredictrsCreatedby = "lk_msdyn_dataanalyticsreport_fspredictrs_createdby";
				public const string LkMsdynDataanalyticsreportFspredictrsCreatedonbehalfby = "lk_msdyn_dataanalyticsreport_fspredictrs_createdonbehalfby";
				public const string LkMsdynDataanalyticsreportFspredictrsModifiedby = "lk_msdyn_dataanalyticsreport_fspredictrs_modifiedby";
				public const string LkMsdynDataanalyticsreportFspredictrsModifiedonbehalfby = "lk_msdyn_dataanalyticsreport_fspredictrs_modifiedonbehalfby";
				public const string LkMsdynDataanalyticsreportFspredictwhdCreatedby = "lk_msdyn_dataanalyticsreport_fspredictwhd_createdby";
				public const string LkMsdynDataanalyticsreportFspredictwhdCreatedonbehalfby = "lk_msdyn_dataanalyticsreport_fspredictwhd_createdonbehalfby";
				public const string LkMsdynDataanalyticsreportFspredictwhdModifiedby = "lk_msdyn_dataanalyticsreport_fspredictwhd_modifiedby";
				public const string LkMsdynDataanalyticsreportFspredictwhdModifiedonbehalfby = "lk_msdyn_dataanalyticsreport_fspredictwhd_modifiedonbehalfby";
				public const string LkMsdynDataanalyticsreportKsinsightsCreatedby = "lk_msdyn_dataanalyticsreport_ksinsights_createdby";
				public const string LkMsdynDataanalyticsreportKsinsightsCreatedonbehalfby = "lk_msdyn_dataanalyticsreport_ksinsights_createdonbehalfby";
				public const string LkMsdynDataanalyticsreportKsinsightsModifiedby = "lk_msdyn_dataanalyticsreport_ksinsights_modifiedby";
				public const string LkMsdynDataanalyticsreportKsinsightsModifiedonbehalfby = "lk_msdyn_dataanalyticsreport_ksinsights_modifiedonbehalfby";
				public const string LkMsdynDataanalyticsreportModifiedby = "lk_msdyn_dataanalyticsreport_modifiedby";
				public const string LkMsdynDataanalyticsreportModifiedonbehalfby = "lk_msdyn_dataanalyticsreport_modifiedonbehalfby";
				public const string LkMsdynDataanalyticsreportOcCreatedby = "lk_msdyn_dataanalyticsreport_oc_createdby";
				public const string LkMsdynDataanalyticsreportOcCreatedonbehalfby = "lk_msdyn_dataanalyticsreport_oc_createdonbehalfby";
				public const string LkMsdynDataanalyticsreportOcModifiedby = "lk_msdyn_dataanalyticsreport_oc_modifiedby";
				public const string LkMsdynDataanalyticsreportOcModifiedonbehalfby = "lk_msdyn_dataanalyticsreport_oc_modifiedonbehalfby";
				public const string LkMsdynDataanalyticsreportOcRtCreatedby = "lk_msdyn_dataanalyticsreport_oc_rt_createdby";
				public const string LkMsdynDataanalyticsreportOcRtCreatedonbehalfby = "lk_msdyn_dataanalyticsreport_oc_rt_createdonbehalfby";
				public const string LkMsdynDataanalyticsreportOcRtModifiedby = "lk_msdyn_dataanalyticsreport_oc_rt_modifiedby";
				public const string LkMsdynDataanalyticsreportOcRtModifiedonbehalfby = "lk_msdyn_dataanalyticsreport_oc_rt_modifiedonbehalfby";
				public const string LkMsdynDataanalyticsreportOcvoiceCreatedby = "lk_msdyn_dataanalyticsreport_ocvoice_createdby";
				public const string LkMsdynDataanalyticsreportOcvoiceCreatedonbehalfby = "lk_msdyn_dataanalyticsreport_ocvoice_createdonbehalfby";
				public const string LkMsdynDataanalyticsreportOcvoiceModifiedby = "lk_msdyn_dataanalyticsreport_ocvoice_modifiedby";
				public const string LkMsdynDataanalyticsreportOcvoiceModifiedonbehalfby = "lk_msdyn_dataanalyticsreport_ocvoice_modifiedonbehalfby";
				public const string LkMsdynDataanalyticsreportSareportingCreatedby = "lk_msdyn_dataanalyticsreport_sareporting_createdby";
				public const string LkMsdynDataanalyticsreportSareportingCreatedonbehalfby = "lk_msdyn_dataanalyticsreport_sareporting_createdonbehalfby";
				public const string LkMsdynDataanalyticsreportSareportingModifiedby = "lk_msdyn_dataanalyticsreport_sareporting_modifiedby";
				public const string LkMsdynDataanalyticsreportSareportingModifiedonbehalfby = "lk_msdyn_dataanalyticsreport_sareporting_modifiedonbehalfby";
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
				public const string LkMsdynDataexportCreatedby = "lk_msdyn_dataexport_createdby";
				public const string LkMsdynDataexportCreatedonbehalfby = "lk_msdyn_dataexport_createdonbehalfby";
				public const string LkMsdynDataexportModifiedby = "lk_msdyn_dataexport_modifiedby";
				public const string LkMsdynDataexportModifiedonbehalfby = "lk_msdyn_dataexport_modifiedonbehalfby";
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
				public const string LkMsdynDelegationCreatedby = "lk_msdyn_delegation_createdby";
				public const string LkMsdynDelegationCreatedonbehalfby = "lk_msdyn_delegation_createdonbehalfby";
				public const string LkMsdynDelegationModifiedby = "lk_msdyn_delegation_modifiedby";
				public const string LkMsdynDelegationModifiedonbehalfby = "lk_msdyn_delegation_modifiedonbehalfby";
				public const string LkMsdynDeletedconversationCreatedby = "lk_msdyn_deletedconversation_createdby";
				public const string LkMsdynDeletedconversationCreatedonbehalfby = "lk_msdyn_deletedconversation_createdonbehalfby";
				public const string LkMsdynDeletedconversationModifiedby = "lk_msdyn_deletedconversation_modifiedby";
				public const string LkMsdynDeletedconversationModifiedonbehalfby = "lk_msdyn_deletedconversation_modifiedonbehalfby";
				public const string LkMsdynDigitalsellingactivetaskCreatedby = "lk_msdyn_digitalsellingactivetask_createdby";
				public const string LkMsdynDigitalsellingactivetaskCreatedonbehalfby = "lk_msdyn_digitalsellingactivetask_createdonbehalfby";
				public const string LkMsdynDigitalsellingactivetaskModifiedby = "lk_msdyn_digitalsellingactivetask_modifiedby";
				public const string LkMsdynDigitalsellingactivetaskModifiedonbehalfby = "lk_msdyn_digitalsellingactivetask_modifiedonbehalfby";
				public const string LkMsdynDigitalsellingcompletedtaskCreatedby = "lk_msdyn_digitalsellingcompletedtask_createdby";
				public const string LkMsdynDigitalsellingcompletedtaskCreatedonbehalfby = "lk_msdyn_digitalsellingcompletedtask_createdonbehalfby";
				public const string LkMsdynDigitalsellingcompletedtaskModifiedby = "lk_msdyn_digitalsellingcompletedtask_modifiedby";
				public const string LkMsdynDigitalsellingcompletedtaskModifiedonbehalfby = "lk_msdyn_digitalsellingcompletedtask_modifiedonbehalfby";
				public const string LkMsdynDimensionCreatedby = "lk_msdyn_dimension_createdby";
				public const string LkMsdynDimensionCreatedonbehalfby = "lk_msdyn_dimension_createdonbehalfby";
				public const string LkMsdynDimensionModifiedby = "lk_msdyn_dimension_modifiedby";
				public const string LkMsdynDimensionModifiedonbehalfby = "lk_msdyn_dimension_modifiedonbehalfby";
				public const string LkMsdynDimensionfieldnameCreatedby = "lk_msdyn_dimensionfieldname_createdby";
				public const string LkMsdynDimensionfieldnameCreatedonbehalfby = "lk_msdyn_dimensionfieldname_createdonbehalfby";
				public const string LkMsdynDimensionfieldnameModifiedby = "lk_msdyn_dimensionfieldname_modifiedby";
				public const string LkMsdynDimensionfieldnameModifiedonbehalfby = "lk_msdyn_dimensionfieldname_modifiedonbehalfby";
				public const string LkMsdynDistributedlockCreatedby = "lk_msdyn_distributedlock_createdby";
				public const string LkMsdynDistributedlockCreatedonbehalfby = "lk_msdyn_distributedlock_createdonbehalfby";
				public const string LkMsdynDistributedlockModifiedby = "lk_msdyn_distributedlock_modifiedby";
				public const string LkMsdynDistributedlockModifiedonbehalfby = "lk_msdyn_distributedlock_modifiedonbehalfby";
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
				public const string LkMsdynEntitlementapplicationCreatedby = "lk_msdyn_entitlementapplication_createdby";
				public const string LkMsdynEntitlementapplicationCreatedonbehalfby = "lk_msdyn_entitlementapplication_createdonbehalfby";
				public const string LkMsdynEntitlementapplicationModifiedby = "lk_msdyn_entitlementapplication_modifiedby";
				public const string LkMsdynEntitlementapplicationModifiedonbehalfby = "lk_msdyn_entitlementapplication_modifiedonbehalfby";
				public const string LkMsdynEntityattachmentCreatedby = "lk_msdyn_entityattachment_createdby";
				public const string LkMsdynEntityattachmentCreatedonbehalfby = "lk_msdyn_entityattachment_createdonbehalfby";
				public const string LkMsdynEntityattachmentModifiedby = "lk_msdyn_entityattachment_modifiedby";
				public const string LkMsdynEntityattachmentModifiedonbehalfby = "lk_msdyn_entityattachment_modifiedonbehalfby";
				public const string LkMsdynEntityconfigCreatedby = "lk_msdyn_entityconfig_createdby";
				public const string LkMsdynEntityconfigCreatedonbehalfby = "lk_msdyn_entityconfig_createdonbehalfby";
				public const string LkMsdynEntityconfigModifiedby = "lk_msdyn_entityconfig_modifiedby";
				public const string LkMsdynEntityconfigModifiedonbehalfby = "lk_msdyn_entityconfig_modifiedonbehalfby";
				public const string LkMsdynEntityconfigurationCreatedby = "lk_msdyn_entityconfiguration_createdby";
				public const string LkMsdynEntityconfigurationCreatedonbehalfby = "lk_msdyn_entityconfiguration_createdonbehalfby";
				public const string LkMsdynEntityconfigurationModifiedby = "lk_msdyn_entityconfiguration_modifiedby";
				public const string LkMsdynEntityconfigurationModifiedonbehalfby = "lk_msdyn_entityconfiguration_modifiedonbehalfby";
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
				public const string LkMsdynEntityworkstreammapCreatedby = "lk_msdyn_entityworkstreammap_createdby";
				public const string LkMsdynEntityworkstreammapCreatedonbehalfby = "lk_msdyn_entityworkstreammap_createdonbehalfby";
				public const string LkMsdynEntityworkstreammapModifiedby = "lk_msdyn_entityworkstreammap_modifiedby";
				public const string LkMsdynEntityworkstreammapModifiedonbehalfby = "lk_msdyn_entityworkstreammap_modifiedonbehalfby";
				public const string LkMsdynEstimateCreatedby = "lk_msdyn_estimate_createdby";
				public const string LkMsdynEstimateCreatedonbehalfby = "lk_msdyn_estimate_createdonbehalfby";
				public const string LkMsdynEstimateModifiedby = "lk_msdyn_estimate_modifiedby";
				public const string LkMsdynEstimateModifiedonbehalfby = "lk_msdyn_estimate_modifiedonbehalfby";
				public const string LkMsdynEstimatelineCreatedby = "lk_msdyn_estimateline_createdby";
				public const string LkMsdynEstimatelineCreatedonbehalfby = "lk_msdyn_estimateline_createdonbehalfby";
				public const string LkMsdynEstimatelineModifiedby = "lk_msdyn_estimateline_modifiedby";
				public const string LkMsdynEstimatelineModifiedonbehalfby = "lk_msdyn_estimateline_modifiedonbehalfby";
				public const string LkMsdynExpenseCreatedby = "lk_msdyn_expense_createdby";
				public const string LkMsdynExpenseCreatedonbehalfby = "lk_msdyn_expense_createdonbehalfby";
				public const string LkMsdynExpenseModifiedby = "lk_msdyn_expense_modifiedby";
				public const string LkMsdynExpenseModifiedonbehalfby = "lk_msdyn_expense_modifiedonbehalfby";
				public const string LkMsdynExpensecategoryCreatedby = "lk_msdyn_expensecategory_createdby";
				public const string LkMsdynExpensecategoryCreatedonbehalfby = "lk_msdyn_expensecategory_createdonbehalfby";
				public const string LkMsdynExpensecategoryModifiedby = "lk_msdyn_expensecategory_modifiedby";
				public const string LkMsdynExpensecategoryModifiedonbehalfby = "lk_msdyn_expensecategory_modifiedonbehalfby";
				public const string LkMsdynExpensereceiptCreatedby = "lk_msdyn_expensereceipt_createdby";
				public const string LkMsdynExpensereceiptCreatedonbehalfby = "lk_msdyn_expensereceipt_createdonbehalfby";
				public const string LkMsdynExpensereceiptModifiedby = "lk_msdyn_expensereceipt_modifiedby";
				public const string LkMsdynExpensereceiptModifiedonbehalfby = "lk_msdyn_expensereceipt_modifiedonbehalfby";
				public const string LkMsdynExtendedusersettingCreatedby = "lk_msdyn_extendedusersetting_createdby";
				public const string LkMsdynExtendedusersettingCreatedonbehalfby = "lk_msdyn_extendedusersetting_createdonbehalfby";
				public const string LkMsdynExtendedusersettingModifiedby = "lk_msdyn_extendedusersetting_modifiedby";
				public const string LkMsdynExtendedusersettingModifiedonbehalfby = "lk_msdyn_extendedusersetting_modifiedonbehalfby";
				public const string LkMsdynFacebookengagementctxCreatedby = "lk_msdyn_facebookengagementctx_createdby";
				public const string LkMsdynFacebookengagementctxCreatedonbehalfby = "lk_msdyn_facebookengagementctx_createdonbehalfby";
				public const string LkMsdynFacebookengagementctxModifiedby = "lk_msdyn_facebookengagementctx_modifiedby";
				public const string LkMsdynFacebookengagementctxModifiedonbehalfby = "lk_msdyn_facebookengagementctx_modifiedonbehalfby";
				public const string LkMsdynFactCreatedby = "lk_msdyn_fact_createdby";
				public const string LkMsdynFactCreatedonbehalfby = "lk_msdyn_fact_createdonbehalfby";
				public const string LkMsdynFactModifiedby = "lk_msdyn_fact_modifiedby";
				public const string LkMsdynFactModifiedonbehalfby = "lk_msdyn_fact_modifiedonbehalfby";
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
				public const string LkMsdynFieldcomputationCreatedby = "lk_msdyn_fieldcomputation_createdby";
				public const string LkMsdynFieldcomputationCreatedonbehalfby = "lk_msdyn_fieldcomputation_createdonbehalfby";
				public const string LkMsdynFieldcomputationModifiedby = "lk_msdyn_fieldcomputation_modifiedby";
				public const string LkMsdynFieldcomputationModifiedonbehalfby = "lk_msdyn_fieldcomputation_modifiedonbehalfby";
				public const string LkMsdynFieldservicepricelistitemCreatedby = "lk_msdyn_fieldservicepricelistitem_createdby";
				public const string LkMsdynFieldservicepricelistitemCreatedonbehalfby = "lk_msdyn_fieldservicepricelistitem_createdonbehalfby";
				public const string LkMsdynFieldservicepricelistitemModifiedby = "lk_msdyn_fieldservicepricelistitem_modifiedby";
				public const string LkMsdynFieldservicepricelistitemModifiedonbehalfby = "lk_msdyn_fieldservicepricelistitem_modifiedonbehalfby";
				public const string LkMsdynFieldservicesettingCreatedby = "lk_msdyn_fieldservicesetting_createdby";
				public const string LkMsdynFieldservicesettingCreatedonbehalfby = "lk_msdyn_fieldservicesetting_createdonbehalfby";
				public const string LkMsdynFieldservicesettingModifiedby = "lk_msdyn_fieldservicesetting_modifiedby";
				public const string LkMsdynFieldservicesettingModifiedonbehalfby = "lk_msdyn_fieldservicesetting_modifiedonbehalfby";
				public const string LkMsdynFieldserviceslaconfigurationCreatedby = "lk_msdyn_fieldserviceslaconfiguration_createdby";
				public const string LkMsdynFieldserviceslaconfigurationCreatedonbehalfby = "lk_msdyn_fieldserviceslaconfiguration_createdonbehalfby";
				public const string LkMsdynFieldserviceslaconfigurationModifiedby = "lk_msdyn_fieldserviceslaconfiguration_modifiedby";
				public const string LkMsdynFieldserviceslaconfigurationModifiedonbehalfby = "lk_msdyn_fieldserviceslaconfiguration_modifiedonbehalfby";
				public const string LkMsdynFieldservicesystemjobCreatedby = "lk_msdyn_fieldservicesystemjob_createdby";
				public const string LkMsdynFieldservicesystemjobCreatedonbehalfby = "lk_msdyn_fieldservicesystemjob_createdonbehalfby";
				public const string LkMsdynFieldservicesystemjobModifiedby = "lk_msdyn_fieldservicesystemjob_modifiedby";
				public const string LkMsdynFieldservicesystemjobModifiedonbehalfby = "lk_msdyn_fieldservicesystemjob_modifiedonbehalfby";
				public const string LkMsdynFileuploadCreatedby = "lk_msdyn_fileupload_createdby";
				public const string LkMsdynFileuploadCreatedonbehalfby = "lk_msdyn_fileupload_createdonbehalfby";
				public const string LkMsdynFileuploadModifiedby = "lk_msdyn_fileupload_modifiedby";
				public const string LkMsdynFileuploadModifiedonbehalfby = "lk_msdyn_fileupload_modifiedonbehalfby";
				public const string LkMsdynFileuploadstatustrackerCreatedby = "lk_msdyn_fileuploadstatustracker_createdby";
				public const string LkMsdynFileuploadstatustrackerCreatedonbehalfby = "lk_msdyn_fileuploadstatustracker_createdonbehalfby";
				public const string LkMsdynFileuploadstatustrackerModifiedby = "lk_msdyn_fileuploadstatustracker_modifiedby";
				public const string LkMsdynFileuploadstatustrackerModifiedonbehalfby = "lk_msdyn_fileuploadstatustracker_modifiedonbehalfby";
				public const string LkMsdynFindworkeventCreatedby = "lk_msdyn_findworkevent_createdby";
				public const string LkMsdynFindworkeventCreatedonbehalfby = "lk_msdyn_findworkevent_createdonbehalfby";
				public const string LkMsdynFindworkeventModifiedby = "lk_msdyn_findworkevent_modifiedby";
				public const string LkMsdynFindworkeventModifiedonbehalfby = "lk_msdyn_findworkevent_modifiedonbehalfby";
				public const string LkMsdynFlowcardtypeCreatedby = "lk_msdyn_flowcardtype_createdby";
				public const string LkMsdynFlowcardtypeCreatedonbehalfby = "lk_msdyn_flowcardtype_createdonbehalfby";
				public const string LkMsdynFlowcardtypeModifiedby = "lk_msdyn_flowcardtype_modifiedby";
				public const string LkMsdynFlowcardtypeModifiedonbehalfby = "lk_msdyn_flowcardtype_modifiedonbehalfby";
				public const string LkMsdynFlwconfigurationCreatedby = "lk_msdyn_flwconfiguration_createdby";
				public const string LkMsdynFlwconfigurationCreatedonbehalfby = "lk_msdyn_flwconfiguration_createdonbehalfby";
				public const string LkMsdynFlwconfigurationModifiedby = "lk_msdyn_flwconfiguration_modifiedby";
				public const string LkMsdynFlwconfigurationModifiedonbehalfby = "lk_msdyn_flwconfiguration_modifiedonbehalfby";
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
				public const string LkMsdynFormmappingCreatedby = "lk_msdyn_formmapping_createdby";
				public const string LkMsdynFormmappingCreatedonbehalfby = "lk_msdyn_formmapping_createdonbehalfby";
				public const string LkMsdynFormmappingModifiedby = "lk_msdyn_formmapping_modifiedby";
				public const string LkMsdynFormmappingModifiedonbehalfby = "lk_msdyn_formmapping_modifiedonbehalfby";
				public const string LkMsdynFunctionallocationCreatedby = "lk_msdyn_functionallocation_createdby";
				public const string LkMsdynFunctionallocationCreatedonbehalfby = "lk_msdyn_functionallocation_createdonbehalfby";
				public const string LkMsdynFunctionallocationModifiedby = "lk_msdyn_functionallocation_modifiedby";
				public const string LkMsdynFunctionallocationModifiedonbehalfby = "lk_msdyn_functionallocation_modifiedonbehalfby";
				public const string LkMsdynFunctionallocationtypeCreatedby = "lk_msdyn_functionallocationtype_createdby";
				public const string LkMsdynFunctionallocationtypeCreatedonbehalfby = "lk_msdyn_functionallocationtype_createdonbehalfby";
				public const string LkMsdynFunctionallocationtypeModifiedby = "lk_msdyn_functionallocationtype_modifiedby";
				public const string LkMsdynFunctionallocationtypeModifiedonbehalfby = "lk_msdyn_functionallocationtype_modifiedonbehalfby";
				public const string LkMsdynGdprdataCreatedby = "lk_msdyn_gdprdata_createdby";
				public const string LkMsdynGdprdataCreatedonbehalfby = "lk_msdyn_gdprdata_createdonbehalfby";
				public const string LkMsdynGdprdataModifiedby = "lk_msdyn_gdprdata_modifiedby";
				public const string LkMsdynGdprdataModifiedonbehalfby = "lk_msdyn_gdprdata_modifiedonbehalfby";
				public const string LkMsdynGeofenceCreatedby = "lk_msdyn_geofence_createdby";
				public const string LkMsdynGeofenceCreatedonbehalfby = "lk_msdyn_geofence_createdonbehalfby";
				public const string LkMsdynGeofenceModifiedby = "lk_msdyn_geofence_modifiedby";
				public const string LkMsdynGeofenceModifiedonbehalfby = "lk_msdyn_geofence_modifiedonbehalfby";
				public const string LkMsdynGeofenceeventCreatedby = "lk_msdyn_geofenceevent_createdby";
				public const string LkMsdynGeofenceeventCreatedonbehalfby = "lk_msdyn_geofenceevent_createdonbehalfby";
				public const string LkMsdynGeofenceeventModifiedby = "lk_msdyn_geofenceevent_modifiedby";
				public const string LkMsdynGeofenceeventModifiedonbehalfby = "lk_msdyn_geofenceevent_modifiedonbehalfby";
				public const string LkMsdynGeofencingsettingsCreatedby = "lk_msdyn_geofencingsettings_createdby";
				public const string LkMsdynGeofencingsettingsCreatedonbehalfby = "lk_msdyn_geofencingsettings_createdonbehalfby";
				public const string LkMsdynGeofencingsettingsModifiedby = "lk_msdyn_geofencingsettings_modifiedby";
				public const string LkMsdynGeofencingsettingsModifiedonbehalfby = "lk_msdyn_geofencingsettings_modifiedonbehalfby";
				public const string LkMsdynGeolocationsettingsCreatedby = "lk_msdyn_geolocationsettings_createdby";
				public const string LkMsdynGeolocationsettingsCreatedonbehalfby = "lk_msdyn_geolocationsettings_createdonbehalfby";
				public const string LkMsdynGeolocationsettingsModifiedby = "lk_msdyn_geolocationsettings_modifiedby";
				public const string LkMsdynGeolocationsettingsModifiedonbehalfby = "lk_msdyn_geolocationsettings_modifiedonbehalfby";
				public const string LkMsdynGeolocationtrackingCreatedby = "lk_msdyn_geolocationtracking_createdby";
				public const string LkMsdynGeolocationtrackingCreatedonbehalfby = "lk_msdyn_geolocationtracking_createdonbehalfby";
				public const string LkMsdynGeolocationtrackingModifiedby = "lk_msdyn_geolocationtracking_modifiedby";
				public const string LkMsdynGeolocationtrackingModifiedonbehalfby = "lk_msdyn_geolocationtracking_modifiedonbehalfby";
				public const string LkMsdynHelppageCreatedby = "lk_msdyn_helppage_createdby";
				public const string LkMsdynHelppageCreatedonbehalfby = "lk_msdyn_helppage_createdonbehalfby";
				public const string LkMsdynHelppageModifiedby = "lk_msdyn_helppage_modifiedby";
				public const string LkMsdynHelppageModifiedonbehalfby = "lk_msdyn_helppage_modifiedonbehalfby";
				public const string LkMsdynIcdextensionCreatedby = "lk_msdyn_icdextension_createdby";
				public const string LkMsdynIcdextensionCreatedonbehalfby = "lk_msdyn_icdextension_createdonbehalfby";
				public const string LkMsdynIcdextensionModifiedby = "lk_msdyn_icdextension_modifiedby";
				public const string LkMsdynIcdextensionModifiedonbehalfby = "lk_msdyn_icdextension_modifiedonbehalfby";
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
				public const string LkMsdynInboxcardconfigurationCreatedby = "lk_msdyn_inboxcardconfiguration_createdby";
				public const string LkMsdynInboxcardconfigurationCreatedonbehalfby = "lk_msdyn_inboxcardconfiguration_createdonbehalfby";
				public const string LkMsdynInboxcardconfigurationModifiedby = "lk_msdyn_inboxcardconfiguration_modifiedby";
				public const string LkMsdynInboxcardconfigurationModifiedonbehalfby = "lk_msdyn_inboxcardconfiguration_modifiedonbehalfby";
				public const string LkMsdynInboxconfigurationCreatedby = "lk_msdyn_inboxconfiguration_createdby";
				public const string LkMsdynInboxconfigurationCreatedonbehalfby = "lk_msdyn_inboxconfiguration_createdonbehalfby";
				public const string LkMsdynInboxconfigurationModifiedby = "lk_msdyn_inboxconfiguration_modifiedby";
				public const string LkMsdynInboxconfigurationModifiedonbehalfby = "lk_msdyn_inboxconfiguration_modifiedonbehalfby";
				public const string LkMsdynInboxentityconfigCreatedby = "lk_msdyn_inboxentityconfig_createdby";
				public const string LkMsdynInboxentityconfigCreatedonbehalfby = "lk_msdyn_inboxentityconfig_createdonbehalfby";
				public const string LkMsdynInboxentityconfigModifiedby = "lk_msdyn_inboxentityconfig_modifiedby";
				public const string LkMsdynInboxentityconfigModifiedonbehalfby = "lk_msdyn_inboxentityconfig_modifiedonbehalfby";
				public const string LkMsdynIncidenttypeCreatedby = "lk_msdyn_incidenttype_createdby";
				public const string LkMsdynIncidenttypeCreatedonbehalfby = "lk_msdyn_incidenttype_createdonbehalfby";
				public const string LkMsdynIncidenttypeModifiedby = "lk_msdyn_incidenttype_modifiedby";
				public const string LkMsdynIncidenttypeModifiedonbehalfby = "lk_msdyn_incidenttype_modifiedonbehalfby";
				public const string LkMsdynIncidenttypeRequirementgroupCreatedby = "lk_msdyn_incidenttype_requirementgroup_createdby";
				public const string LkMsdynIncidenttypeRequirementgroupCreatedonbehalfby = "lk_msdyn_incidenttype_requirementgroup_createdonbehalfby";
				public const string LkMsdynIncidenttypeRequirementgroupModifiedby = "lk_msdyn_incidenttype_requirementgroup_modifiedby";
				public const string LkMsdynIncidenttypeRequirementgroupModifiedonbehalfby = "lk_msdyn_incidenttype_requirementgroup_modifiedonbehalfby";
				public const string LkMsdynIncidenttypecharacteristicCreatedby = "lk_msdyn_incidenttypecharacteristic_createdby";
				public const string LkMsdynIncidenttypecharacteristicCreatedonbehalfby = "lk_msdyn_incidenttypecharacteristic_createdonbehalfby";
				public const string LkMsdynIncidenttypecharacteristicModifiedby = "lk_msdyn_incidenttypecharacteristic_modifiedby";
				public const string LkMsdynIncidenttypecharacteristicModifiedonbehalfby = "lk_msdyn_incidenttypecharacteristic_modifiedonbehalfby";
				public const string LkMsdynIncidenttypeproductCreatedby = "lk_msdyn_incidenttypeproduct_createdby";
				public const string LkMsdynIncidenttypeproductCreatedonbehalfby = "lk_msdyn_incidenttypeproduct_createdonbehalfby";
				public const string LkMsdynIncidenttypeproductModifiedby = "lk_msdyn_incidenttypeproduct_modifiedby";
				public const string LkMsdynIncidenttypeproductModifiedonbehalfby = "lk_msdyn_incidenttypeproduct_modifiedonbehalfby";
				public const string LkMsdynIncidenttyperecommendationresultCreatedby = "lk_msdyn_incidenttyperecommendationresult_createdby";
				public const string LkMsdynIncidenttyperecommendationresultCreatedonbehalfby = "lk_msdyn_incidenttyperecommendationresult_createdonbehalfby";
				public const string LkMsdynIncidenttyperecommendationresultModifiedby = "lk_msdyn_incidenttyperecommendationresult_modifiedby";
				public const string LkMsdynIncidenttyperecommendationresultModifiedonbehalfby = "lk_msdyn_incidenttyperecommendationresult_modifiedonbehalfby";
				public const string LkMsdynIncidenttyperecommendationrunhistoryCreatedby = "lk_msdyn_incidenttyperecommendationrunhistory_createdby";
				public const string LkMsdynIncidenttyperecommendationrunhistoryCreatedonbehalfby = "lk_msdyn_incidenttyperecommendationrunhistory_createdonbehalfby";
				public const string LkMsdynIncidenttyperecommendationrunhistoryModifiedby = "lk_msdyn_incidenttyperecommendationrunhistory_modifiedby";
				public const string LkMsdynIncidenttyperecommendationrunhistoryModifiedonbehalfby = "lk_msdyn_incidenttyperecommendationrunhistory_modifiedonbehalfby";
				public const string LkMsdynIncidenttyperesolutionCreatedby = "lk_msdyn_incidenttyperesolution_createdby";
				public const string LkMsdynIncidenttyperesolutionCreatedonbehalfby = "lk_msdyn_incidenttyperesolution_createdonbehalfby";
				public const string LkMsdynIncidenttyperesolutionModifiedby = "lk_msdyn_incidenttyperesolution_modifiedby";
				public const string LkMsdynIncidenttyperesolutionModifiedonbehalfby = "lk_msdyn_incidenttyperesolution_modifiedonbehalfby";
				public const string LkMsdynIncidenttypeserviceCreatedby = "lk_msdyn_incidenttypeservice_createdby";
				public const string LkMsdynIncidenttypeserviceCreatedonbehalfby = "lk_msdyn_incidenttypeservice_createdonbehalfby";
				public const string LkMsdynIncidenttypeserviceModifiedby = "lk_msdyn_incidenttypeservice_modifiedby";
				public const string LkMsdynIncidenttypeserviceModifiedonbehalfby = "lk_msdyn_incidenttypeservice_modifiedonbehalfby";
				public const string LkMsdynIncidenttypeservicetaskCreatedby = "lk_msdyn_incidenttypeservicetask_createdby";
				public const string LkMsdynIncidenttypeservicetaskCreatedonbehalfby = "lk_msdyn_incidenttypeservicetask_createdonbehalfby";
				public const string LkMsdynIncidenttypeservicetaskModifiedby = "lk_msdyn_incidenttypeservicetask_modifiedby";
				public const string LkMsdynIncidenttypeservicetaskModifiedonbehalfby = "lk_msdyn_incidenttypeservicetask_modifiedonbehalfby";
				public const string LkMsdynIncidenttypessetupCreatedby = "lk_msdyn_incidenttypessetup_createdby";
				public const string LkMsdynIncidenttypessetupCreatedonbehalfby = "lk_msdyn_incidenttypessetup_createdonbehalfby";
				public const string LkMsdynIncidenttypessetupModifiedby = "lk_msdyn_incidenttypessetup_modifiedby";
				public const string LkMsdynIncidenttypessetupModifiedonbehalfby = "lk_msdyn_incidenttypessetup_modifiedonbehalfby";
				public const string LkMsdynInsightsstorevirtualentityCreatedby = "lk_msdyn_insightsstorevirtualentity_createdby";
				public const string LkMsdynInsightsstorevirtualentityCreatedonbehalfby = "lk_msdyn_insightsstorevirtualentity_createdonbehalfby";
				public const string LkMsdynInsightsstorevirtualentityModifiedby = "lk_msdyn_insightsstorevirtualentity_modifiedby";
				public const string LkMsdynInsightsstorevirtualentityModifiedonbehalfby = "lk_msdyn_insightsstorevirtualentity_modifiedonbehalfby";
				public const string LkMsdynInspectionCreatedby = "lk_msdyn_inspection_createdby";
				public const string LkMsdynInspectionCreatedonbehalfby = "lk_msdyn_inspection_createdonbehalfby";
				public const string LkMsdynInspectionModifiedby = "lk_msdyn_inspection_modifiedby";
				public const string LkMsdynInspectionModifiedonbehalfby = "lk_msdyn_inspection_modifiedonbehalfby";
				public const string LkMsdynInspectionattachmentCreatedby = "lk_msdyn_inspectionattachment_createdby";
				public const string LkMsdynInspectionattachmentCreatedonbehalfby = "lk_msdyn_inspectionattachment_createdonbehalfby";
				public const string LkMsdynInspectionattachmentModifiedby = "lk_msdyn_inspectionattachment_modifiedby";
				public const string LkMsdynInspectionattachmentModifiedonbehalfby = "lk_msdyn_inspectionattachment_modifiedonbehalfby";
				public const string LkMsdynInspectiondefinitionCreatedby = "lk_msdyn_inspectiondefinition_createdby";
				public const string LkMsdynInspectiondefinitionCreatedonbehalfby = "lk_msdyn_inspectiondefinition_createdonbehalfby";
				public const string LkMsdynInspectiondefinitionModifiedby = "lk_msdyn_inspectiondefinition_modifiedby";
				public const string LkMsdynInspectiondefinitionModifiedonbehalfby = "lk_msdyn_inspectiondefinition_modifiedonbehalfby";
				public const string LkMsdynInspectioninstanceCreatedby = "lk_msdyn_inspectioninstance_createdby";
				public const string LkMsdynInspectioninstanceCreatedonbehalfby = "lk_msdyn_inspectioninstance_createdonbehalfby";
				public const string LkMsdynInspectioninstanceModifiedby = "lk_msdyn_inspectioninstance_modifiedby";
				public const string LkMsdynInspectioninstanceModifiedonbehalfby = "lk_msdyn_inspectioninstance_modifiedonbehalfby";
				public const string LkMsdynInspectionresponseCreatedby = "lk_msdyn_inspectionresponse_createdby";
				public const string LkMsdynInspectionresponseCreatedonbehalfby = "lk_msdyn_inspectionresponse_createdonbehalfby";
				public const string LkMsdynInspectionresponseModifiedby = "lk_msdyn_inspectionresponse_modifiedby";
				public const string LkMsdynInspectionresponseModifiedonbehalfby = "lk_msdyn_inspectionresponse_modifiedonbehalfby";
				public const string LkMsdynInsuranceCreatedby = "lk_msdyn_insurance_createdby";
				public const string LkMsdynInsuranceCreatedonbehalfby = "lk_msdyn_insurance_createdonbehalfby";
				public const string LkMsdynInsuranceModifiedby = "lk_msdyn_insurance_modifiedby";
				public const string LkMsdynInsuranceModifiedonbehalfby = "lk_msdyn_insurance_modifiedonbehalfby";
				public const string LkMsdynIntegratedsearchproviderCreatedby = "lk_msdyn_integratedsearchprovider_createdby";
				public const string LkMsdynIntegratedsearchproviderCreatedonbehalfby = "lk_msdyn_integratedsearchprovider_createdonbehalfby";
				public const string LkMsdynIntegratedsearchproviderModifiedby = "lk_msdyn_integratedsearchprovider_modifiedby";
				public const string LkMsdynIntegratedsearchproviderModifiedonbehalfby = "lk_msdyn_integratedsearchprovider_modifiedonbehalfby";
				public const string LkMsdynIntegrationjobCreatedby = "lk_msdyn_integrationjob_createdby";
				public const string LkMsdynIntegrationjobCreatedonbehalfby = "lk_msdyn_integrationjob_createdonbehalfby";
				public const string LkMsdynIntegrationjobModifiedby = "lk_msdyn_integrationjob_modifiedby";
				public const string LkMsdynIntegrationjobModifiedonbehalfby = "lk_msdyn_integrationjob_modifiedonbehalfby";
				public const string LkMsdynIntegrationjobdetailCreatedby = "lk_msdyn_integrationjobdetail_createdby";
				public const string LkMsdynIntegrationjobdetailCreatedonbehalfby = "lk_msdyn_integrationjobdetail_createdonbehalfby";
				public const string LkMsdynIntegrationjobdetailModifiedby = "lk_msdyn_integrationjobdetail_modifiedby";
				public const string LkMsdynIntegrationjobdetailModifiedonbehalfby = "lk_msdyn_integrationjobdetail_modifiedonbehalfby";
				public const string LkMsdynIntentCreatedby = "lk_msdyn_intent_createdby";
				public const string LkMsdynIntentCreatedonbehalfby = "lk_msdyn_intent_createdonbehalfby";
				public const string LkMsdynIntentModifiedby = "lk_msdyn_intent_modifiedby";
				public const string LkMsdynIntentModifiedonbehalfby = "lk_msdyn_intent_modifiedonbehalfby";
				public const string LkMsdynInventoryadjustmentCreatedby = "lk_msdyn_inventoryadjustment_createdby";
				public const string LkMsdynInventoryadjustmentCreatedonbehalfby = "lk_msdyn_inventoryadjustment_createdonbehalfby";
				public const string LkMsdynInventoryadjustmentModifiedby = "lk_msdyn_inventoryadjustment_modifiedby";
				public const string LkMsdynInventoryadjustmentModifiedonbehalfby = "lk_msdyn_inventoryadjustment_modifiedonbehalfby";
				public const string LkMsdynInventoryadjustmentproductCreatedby = "lk_msdyn_inventoryadjustmentproduct_createdby";
				public const string LkMsdynInventoryadjustmentproductCreatedonbehalfby = "lk_msdyn_inventoryadjustmentproduct_createdonbehalfby";
				public const string LkMsdynInventoryadjustmentproductModifiedby = "lk_msdyn_inventoryadjustmentproduct_modifiedby";
				public const string LkMsdynInventoryadjustmentproductModifiedonbehalfby = "lk_msdyn_inventoryadjustmentproduct_modifiedonbehalfby";
				public const string LkMsdynInventoryjournalCreatedby = "lk_msdyn_inventoryjournal_createdby";
				public const string LkMsdynInventoryjournalCreatedonbehalfby = "lk_msdyn_inventoryjournal_createdonbehalfby";
				public const string LkMsdynInventoryjournalModifiedby = "lk_msdyn_inventoryjournal_modifiedby";
				public const string LkMsdynInventoryjournalModifiedonbehalfby = "lk_msdyn_inventoryjournal_modifiedonbehalfby";
				public const string LkMsdynInventorytransferCreatedby = "lk_msdyn_inventorytransfer_createdby";
				public const string LkMsdynInventorytransferCreatedonbehalfby = "lk_msdyn_inventorytransfer_createdonbehalfby";
				public const string LkMsdynInventorytransferModifiedby = "lk_msdyn_inventorytransfer_modifiedby";
				public const string LkMsdynInventorytransferModifiedonbehalfby = "lk_msdyn_inventorytransfer_modifiedonbehalfby";
				public const string LkMsdynInvoicefrequencyCreatedby = "lk_msdyn_invoicefrequency_createdby";
				public const string LkMsdynInvoicefrequencyCreatedonbehalfby = "lk_msdyn_invoicefrequency_createdonbehalfby";
				public const string LkMsdynInvoicefrequencyModifiedby = "lk_msdyn_invoicefrequency_modifiedby";
				public const string LkMsdynInvoicefrequencyModifiedonbehalfby = "lk_msdyn_invoicefrequency_modifiedonbehalfby";
				public const string LkMsdynInvoicefrequencydetailCreatedby = "lk_msdyn_invoicefrequencydetail_createdby";
				public const string LkMsdynInvoicefrequencydetailCreatedonbehalfby = "lk_msdyn_invoicefrequencydetail_createdonbehalfby";
				public const string LkMsdynInvoicefrequencydetailModifiedby = "lk_msdyn_invoicefrequencydetail_modifiedby";
				public const string LkMsdynInvoicefrequencydetailModifiedonbehalfby = "lk_msdyn_invoicefrequencydetail_modifiedonbehalfby";
				public const string LkMsdynInvoicelinetransactionCreatedby = "lk_msdyn_invoicelinetransaction_createdby";
				public const string LkMsdynInvoicelinetransactionCreatedonbehalfby = "lk_msdyn_invoicelinetransaction_createdonbehalfby";
				public const string LkMsdynInvoicelinetransactionModifiedby = "lk_msdyn_invoicelinetransaction_modifiedby";
				public const string LkMsdynInvoicelinetransactionModifiedonbehalfby = "lk_msdyn_invoicelinetransaction_modifiedonbehalfby";
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
				public const string LkMsdynJobsstateCreatedby = "lk_msdyn_jobsstate_createdby";
				public const string LkMsdynJobsstateCreatedonbehalfby = "lk_msdyn_jobsstate_createdonbehalfby";
				public const string LkMsdynJobsstateModifiedby = "lk_msdyn_jobsstate_modifiedby";
				public const string LkMsdynJobsstateModifiedonbehalfby = "lk_msdyn_jobsstate_modifiedonbehalfby";
				public const string LkMsdynJournalCreatedby = "lk_msdyn_journal_createdby";
				public const string LkMsdynJournalCreatedonbehalfby = "lk_msdyn_journal_createdonbehalfby";
				public const string LkMsdynJournalModifiedby = "lk_msdyn_journal_modifiedby";
				public const string LkMsdynJournalModifiedonbehalfby = "lk_msdyn_journal_modifiedonbehalfby";
				public const string LkMsdynJournallineCreatedby = "lk_msdyn_journalline_createdby";
				public const string LkMsdynJournallineCreatedonbehalfby = "lk_msdyn_journalline_createdonbehalfby";
				public const string LkMsdynJournallineModifiedby = "lk_msdyn_journalline_modifiedby";
				public const string LkMsdynJournallineModifiedonbehalfby = "lk_msdyn_journalline_modifiedonbehalfby";
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
				public const string LkMsdynKnowledgeassetconfigurationCreatedby = "lk_msdyn_knowledgeassetconfiguration_createdby";
				public const string LkMsdynKnowledgeassetconfigurationCreatedonbehalfby = "lk_msdyn_knowledgeassetconfiguration_createdonbehalfby";
				public const string LkMsdynKnowledgeassetconfigurationModifiedby = "lk_msdyn_knowledgeassetconfiguration_modifiedby";
				public const string LkMsdynKnowledgeassetconfigurationModifiedonbehalfby = "lk_msdyn_knowledgeassetconfiguration_modifiedonbehalfby";
				public const string LkMsdynKnowledgeconfigurationCreatedby = "lk_msdyn_knowledgeconfiguration_createdby";
				public const string LkMsdynKnowledgeconfigurationCreatedonbehalfby = "lk_msdyn_knowledgeconfiguration_createdonbehalfby";
				public const string LkMsdynKnowledgeconfigurationModifiedby = "lk_msdyn_knowledgeconfiguration_modifiedby";
				public const string LkMsdynKnowledgeconfigurationModifiedonbehalfby = "lk_msdyn_knowledgeconfiguration_modifiedonbehalfby";
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
				public const string LkMsdynLeadkpiitemCreatedby = "lk_msdyn_leadkpiitem_createdby";
				public const string LkMsdynLeadkpiitemCreatedonbehalfby = "lk_msdyn_leadkpiitem_createdonbehalfby";
				public const string LkMsdynLeadkpiitemModifiedby = "lk_msdyn_leadkpiitem_modifiedby";
				public const string LkMsdynLeadkpiitemModifiedonbehalfby = "lk_msdyn_leadkpiitem_modifiedonbehalfby";
				public const string LkMsdynLeadmodelconfigCreatedby = "lk_msdyn_leadmodelconfig_createdby";
				public const string LkMsdynLeadmodelconfigCreatedonbehalfby = "lk_msdyn_leadmodelconfig_createdonbehalfby";
				public const string LkMsdynLeadmodelconfigModifiedby = "lk_msdyn_leadmodelconfig_modifiedby";
				public const string LkMsdynLeadmodelconfigModifiedonbehalfby = "lk_msdyn_leadmodelconfig_modifiedonbehalfby";
				public const string LkMsdynLineengagementctxCreatedby = "lk_msdyn_lineengagementctx_createdby";
				public const string LkMsdynLineengagementctxCreatedonbehalfby = "lk_msdyn_lineengagementctx_createdonbehalfby";
				public const string LkMsdynLineengagementctxModifiedby = "lk_msdyn_lineengagementctx_modifiedby";
				public const string LkMsdynLineengagementctxModifiedonbehalfby = "lk_msdyn_lineengagementctx_modifiedonbehalfby";
				public const string LkMsdynLinkedentityattributevalidityCreatedby = "lk_msdyn_linkedentityattributevalidity_createdby";
				public const string LkMsdynLinkedentityattributevalidityCreatedonbehalfby = "lk_msdyn_linkedentityattributevalidity_createdonbehalfby";
				public const string LkMsdynLinkedentityattributevalidityModifiedby = "lk_msdyn_linkedentityattributevalidity_modifiedby";
				public const string LkMsdynLinkedentityattributevalidityModifiedonbehalfby = "lk_msdyn_linkedentityattributevalidity_modifiedonbehalfby";
				public const string LkMsdynLivechatconfigCreatedby = "lk_msdyn_livechatconfig_createdby";
				public const string LkMsdynLivechatconfigCreatedonbehalfby = "lk_msdyn_livechatconfig_createdonbehalfby";
				public const string LkMsdynLivechatconfigModifiedby = "lk_msdyn_livechatconfig_modifiedby";
				public const string LkMsdynLivechatconfigModifiedonbehalfby = "lk_msdyn_livechatconfig_modifiedonbehalfby";
				public const string LkMsdynLivechatengagementctxCreatedby = "lk_msdyn_livechatengagementctx_createdby";
				public const string LkMsdynLivechatengagementctxCreatedonbehalfby = "lk_msdyn_livechatengagementctx_createdonbehalfby";
				public const string LkMsdynLivechatengagementctxModifiedby = "lk_msdyn_livechatengagementctx_modifiedby";
				public const string LkMsdynLivechatengagementctxModifiedonbehalfby = "lk_msdyn_livechatengagementctx_modifiedonbehalfby";
				public const string LkMsdynLivechatwidgetlocationCreatedby = "lk_msdyn_livechatwidgetlocation_createdby";
				public const string LkMsdynLivechatwidgetlocationCreatedonbehalfby = "lk_msdyn_livechatwidgetlocation_createdonbehalfby";
				public const string LkMsdynLivechatwidgetlocationModifiedby = "lk_msdyn_livechatwidgetlocation_modifiedby";
				public const string LkMsdynLivechatwidgetlocationModifiedonbehalfby = "lk_msdyn_livechatwidgetlocation_modifiedonbehalfby";
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
				public const string LkMsdynLocalizedsurveyquestionCreatedby = "lk_msdyn_localizedsurveyquestion_createdby";
				public const string LkMsdynLocalizedsurveyquestionCreatedonbehalfby = "lk_msdyn_localizedsurveyquestion_createdonbehalfby";
				public const string LkMsdynLocalizedsurveyquestionModifiedby = "lk_msdyn_localizedsurveyquestion_modifiedby";
				public const string LkMsdynLocalizedsurveyquestionModifiedonbehalfby = "lk_msdyn_localizedsurveyquestion_modifiedonbehalfby";
				public const string LkMsdynLocationtemplateassociationCreatedby = "lk_msdyn_locationtemplateassociation_createdby";
				public const string LkMsdynLocationtemplateassociationCreatedonbehalfby = "lk_msdyn_locationtemplateassociation_createdonbehalfby";
				public const string LkMsdynLocationtemplateassociationModifiedby = "lk_msdyn_locationtemplateassociation_modifiedby";
				public const string LkMsdynLocationtemplateassociationModifiedonbehalfby = "lk_msdyn_locationtemplateassociation_modifiedonbehalfby";
				public const string LkMsdynLocationtypetemplateassociationCreatedby = "lk_msdyn_locationtypetemplateassociation_createdby";
				public const string LkMsdynLocationtypetemplateassociationCreatedonbehalfby = "lk_msdyn_locationtypetemplateassociation_createdonbehalfby";
				public const string LkMsdynLocationtypetemplateassociationModifiedby = "lk_msdyn_locationtypetemplateassociation_modifiedby";
				public const string LkMsdynLocationtypetemplateassociationModifiedonbehalfby = "lk_msdyn_locationtypetemplateassociation_modifiedonbehalfby";
				public const string LkMsdynLockstatusCreatedby = "lk_msdyn_lockstatus_createdby";
				public const string LkMsdynLockstatusCreatedonbehalfby = "lk_msdyn_lockstatus_createdonbehalfby";
				public const string LkMsdynLockstatusModifiedby = "lk_msdyn_lockstatus_modifiedby";
				public const string LkMsdynLockstatusModifiedonbehalfby = "lk_msdyn_lockstatus_modifiedonbehalfby";
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
				public const string LkMsdynMlresultcacheCreatedby = "lk_msdyn_mlresultcache_createdby";
				public const string LkMsdynMlresultcacheCreatedonbehalfby = "lk_msdyn_mlresultcache_createdonbehalfby";
				public const string LkMsdynMlresultcacheModifiedby = "lk_msdyn_mlresultcache_modifiedby";
				public const string LkMsdynMlresultcacheModifiedonbehalfby = "lk_msdyn_mlresultcache_modifiedonbehalfby";
				public const string LkMsdynMobileappCreatedby = "lk_msdyn_mobileapp_createdby";
				public const string LkMsdynMobileappCreatedonbehalfby = "lk_msdyn_mobileapp_createdonbehalfby";
				public const string LkMsdynMobileappModifiedby = "lk_msdyn_mobileapp_modifiedby";
				public const string LkMsdynMobileappModifiedonbehalfby = "lk_msdyn_mobileapp_modifiedonbehalfby";
				public const string LkMsdynMobilesourceCreatedby = "lk_msdyn_mobilesource_createdby";
				public const string LkMsdynMobilesourceCreatedonbehalfby = "lk_msdyn_mobilesource_createdonbehalfby";
				public const string LkMsdynMobilesourceModifiedby = "lk_msdyn_mobilesource_modifiedby";
				public const string LkMsdynMobilesourceModifiedonbehalfby = "lk_msdyn_mobilesource_modifiedonbehalfby";
				public const string LkMsdynModelpreviewstatusCreatedby = "lk_msdyn_modelpreviewstatus_createdby";
				public const string LkMsdynModelpreviewstatusCreatedonbehalfby = "lk_msdyn_modelpreviewstatus_createdonbehalfby";
				public const string LkMsdynModelpreviewstatusModifiedby = "lk_msdyn_modelpreviewstatus_modifiedby";
				public const string LkMsdynModelpreviewstatusModifiedonbehalfby = "lk_msdyn_modelpreviewstatus_modifiedonbehalfby";
				public const string LkMsdynModulerundetailCreatedby = "lk_msdyn_modulerundetail_createdby";
				public const string LkMsdynModulerundetailCreatedonbehalfby = "lk_msdyn_modulerundetail_createdonbehalfby";
				public const string LkMsdynModulerundetailModifiedby = "lk_msdyn_modulerundetail_modifiedby";
				public const string LkMsdynModulerundetailModifiedonbehalfby = "lk_msdyn_modulerundetail_modifiedonbehalfby";
				public const string LkMsdynMostcontactedCreatedby = "lk_msdyn_mostcontacted_createdby";
				public const string LkMsdynMostcontactedCreatedonbehalfby = "lk_msdyn_mostcontacted_createdonbehalfby";
				public const string LkMsdynMostcontactedModifiedby = "lk_msdyn_mostcontacted_modifiedby";
				public const string LkMsdynMostcontactedModifiedonbehalfby = "lk_msdyn_mostcontacted_modifiedonbehalfby";
				public const string LkMsdynMostcontactedbyCreatedby = "lk_msdyn_mostcontactedby_createdby";
				public const string LkMsdynMostcontactedbyCreatedonbehalfby = "lk_msdyn_mostcontactedby_createdonbehalfby";
				public const string LkMsdynMostcontactedbyModifiedby = "lk_msdyn_mostcontactedby_modifiedby";
				public const string LkMsdynMostcontactedbyModifiedonbehalfby = "lk_msdyn_mostcontactedby_modifiedonbehalfby";
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
				public const string LkMsdynNottoexceedCreatedby = "lk_msdyn_nottoexceed_createdby";
				public const string LkMsdynNottoexceedCreatedonbehalfby = "lk_msdyn_nottoexceed_createdonbehalfby";
				public const string LkMsdynNottoexceedModifiedby = "lk_msdyn_nottoexceed_modifiedby";
				public const string LkMsdynNottoexceedModifiedonbehalfby = "lk_msdyn_nottoexceed_modifiedonbehalfby";
				public const string LkMsdynOcGeolocationproviderCreatedby = "lk_msdyn_oc_geolocationprovider_createdby";
				public const string LkMsdynOcGeolocationproviderCreatedonbehalfby = "lk_msdyn_oc_geolocationprovider_createdonbehalfby";
				public const string LkMsdynOcGeolocationproviderModifiedby = "lk_msdyn_oc_geolocationprovider_modifiedby";
				public const string LkMsdynOcGeolocationproviderModifiedonbehalfby = "lk_msdyn_oc_geolocationprovider_modifiedonbehalfby";
				public const string LkMsdynOcagentassignedcustomapiprivilegeCreatedby = "lk_msdyn_ocagentassignedcustomapiprivilege_createdby";
				public const string LkMsdynOcagentassignedcustomapiprivilegeCreatedonbehalfby = "lk_msdyn_ocagentassignedcustomapiprivilege_createdonbehalfby";
				public const string LkMsdynOcagentassignedcustomapiprivilegeModifiedby = "lk_msdyn_ocagentassignedcustomapiprivilege_modifiedby";
				public const string LkMsdynOcagentassignedcustomapiprivilegeModifiedonbehalfby = "lk_msdyn_ocagentassignedcustomapiprivilege_modifiedonbehalfby";
				public const string LkMsdynOcapplebusinessaccountCreatedby = "lk_msdyn_ocapplebusinessaccount_createdby";
				public const string LkMsdynOcapplebusinessaccountCreatedonbehalfby = "lk_msdyn_ocapplebusinessaccount_createdonbehalfby";
				public const string LkMsdynOcapplebusinessaccountModifiedby = "lk_msdyn_ocapplebusinessaccount_modifiedby";
				public const string LkMsdynOcapplebusinessaccountModifiedonbehalfby = "lk_msdyn_ocapplebusinessaccount_modifiedonbehalfby";
				public const string LkMsdynOcapplemessagesforbusinessengagementctxCreatedby = "lk_msdyn_ocapplemessagesforbusinessengagementctx_createdby";
				public const string LkMsdynOcapplemessagesforbusinessengagementctxCreatedonbehalfby = "lk_msdyn_ocapplemessagesforbusinessengagementctx_createdonbehalfby";
				public const string LkMsdynOcapplemessagesforbusinessengagementctxModifiedby = "lk_msdyn_ocapplemessagesforbusinessengagementctx_modifiedby";
				public const string LkMsdynOcapplemessagesforbusinessengagementctxModifiedonbehalfby = "lk_msdyn_ocapplemessagesforbusinessengagementctx_modifiedonbehalfby";
				public const string LkMsdynOcapplepayCreatedby = "lk_msdyn_ocapplepay_createdby";
				public const string LkMsdynOcapplepayCreatedonbehalfby = "lk_msdyn_ocapplepay_createdonbehalfby";
				public const string LkMsdynOcapplepayModifiedby = "lk_msdyn_ocapplepay_modifiedby";
				public const string LkMsdynOcapplepayModifiedonbehalfby = "lk_msdyn_ocapplepay_modifiedonbehalfby";
				public const string LkMsdynOcautoblockruleCreatedby = "lk_msdyn_ocautoblockrule_createdby";
				public const string LkMsdynOcautoblockruleCreatedonbehalfby = "lk_msdyn_ocautoblockrule_createdonbehalfby";
				public const string LkMsdynOcautoblockruleModifiedby = "lk_msdyn_ocautoblockrule_modifiedby";
				public const string LkMsdynOcautoblockruleModifiedonbehalfby = "lk_msdyn_ocautoblockrule_modifiedonbehalfby";
				public const string LkMsdynOcbotchannelregistrationCreatedby = "lk_msdyn_ocbotchannelregistration_createdby";
				public const string LkMsdynOcbotchannelregistrationCreatedonbehalfby = "lk_msdyn_ocbotchannelregistration_createdonbehalfby";
				public const string LkMsdynOcbotchannelregistrationModifiedby = "lk_msdyn_ocbotchannelregistration_modifiedby";
				public const string LkMsdynOcbotchannelregistrationModifiedonbehalfby = "lk_msdyn_ocbotchannelregistration_modifiedonbehalfby";
				public const string LkMsdynOcbotchannelregistrationsecretCreatedby = "lk_msdyn_ocbotchannelregistrationsecret_createdby";
				public const string LkMsdynOcbotchannelregistrationsecretCreatedonbehalfby = "lk_msdyn_ocbotchannelregistrationsecret_createdonbehalfby";
				public const string LkMsdynOcbotchannelregistrationsecretModifiedby = "lk_msdyn_ocbotchannelregistrationsecret_modifiedby";
				public const string LkMsdynOcbotchannelregistrationsecretModifiedonbehalfby = "lk_msdyn_ocbotchannelregistrationsecret_modifiedonbehalfby";
				public const string LkMsdynOccarrierCreatedby = "lk_msdyn_occarrier_createdby";
				public const string LkMsdynOccarrierCreatedonbehalfby = "lk_msdyn_occarrier_createdonbehalfby";
				public const string LkMsdynOccarrierModifiedby = "lk_msdyn_occarrier_modifiedby";
				public const string LkMsdynOccarrierModifiedonbehalfby = "lk_msdyn_occarrier_modifiedonbehalfby";
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
				public const string LkMsdynOccommunicationprovidersettingCreatedby = "lk_msdyn_occommunicationprovidersetting_createdby";
				public const string LkMsdynOccommunicationprovidersettingCreatedonbehalfby = "lk_msdyn_occommunicationprovidersetting_createdonbehalfby";
				public const string LkMsdynOccommunicationprovidersettingModifiedby = "lk_msdyn_occommunicationprovidersetting_modifiedby";
				public const string LkMsdynOccommunicationprovidersettingModifiedonbehalfby = "lk_msdyn_occommunicationprovidersetting_modifiedonbehalfby";
				public const string LkMsdynOccommunicationprovidersettingentryCreatedby = "lk_msdyn_occommunicationprovidersettingentry_createdby";
				public const string LkMsdynOccommunicationprovidersettingentryCreatedonbehalfby = "lk_msdyn_occommunicationprovidersettingentry_createdonbehalfby";
				public const string LkMsdynOccommunicationprovidersettingentryModifiedby = "lk_msdyn_occommunicationprovidersettingentry_modifiedby";
				public const string LkMsdynOccommunicationprovidersettingentryModifiedonbehalfby = "lk_msdyn_occommunicationprovidersettingentry_modifiedonbehalfby";
				public const string LkMsdynOccustommessagingchannelCreatedby = "lk_msdyn_occustommessagingchannel_createdby";
				public const string LkMsdynOccustommessagingchannelCreatedonbehalfby = "lk_msdyn_occustommessagingchannel_createdonbehalfby";
				public const string LkMsdynOccustommessagingchannelModifiedby = "lk_msdyn_occustommessagingchannel_modifiedby";
				public const string LkMsdynOccustommessagingchannelModifiedonbehalfby = "lk_msdyn_occustommessagingchannel_modifiedonbehalfby";
				public const string LkMsdynOcexternalcontextCreatedby = "lk_msdyn_ocexternalcontext_createdby";
				public const string LkMsdynOcexternalcontextCreatedonbehalfby = "lk_msdyn_ocexternalcontext_createdonbehalfby";
				public const string LkMsdynOcexternalcontextModifiedby = "lk_msdyn_ocexternalcontext_modifiedby";
				public const string LkMsdynOcexternalcontextModifiedonbehalfby = "lk_msdyn_ocexternalcontext_modifiedonbehalfby";
				public const string LkMsdynOcfbapplicationCreatedby = "lk_msdyn_ocfbapplication_createdby";
				public const string LkMsdynOcfbapplicationCreatedonbehalfby = "lk_msdyn_ocfbapplication_createdonbehalfby";
				public const string LkMsdynOcfbapplicationModifiedby = "lk_msdyn_ocfbapplication_modifiedby";
				public const string LkMsdynOcfbapplicationModifiedonbehalfby = "lk_msdyn_ocfbapplication_modifiedonbehalfby";
				public const string LkMsdynOcfbpageCreatedby = "lk_msdyn_ocfbpage_createdby";
				public const string LkMsdynOcfbpageCreatedonbehalfby = "lk_msdyn_ocfbpage_createdonbehalfby";
				public const string LkMsdynOcfbpageModifiedby = "lk_msdyn_ocfbpage_modifiedby";
				public const string LkMsdynOcfbpageModifiedonbehalfby = "lk_msdyn_ocfbpage_modifiedonbehalfby";
				public const string LkMsdynOcflaggedspamCreatedby = "lk_msdyn_ocflaggedspam_createdby";
				public const string LkMsdynOcflaggedspamCreatedonbehalfby = "lk_msdyn_ocflaggedspam_createdonbehalfby";
				public const string LkMsdynOcflaggedspamModifiedby = "lk_msdyn_ocflaggedspam_modifiedby";
				public const string LkMsdynOcflaggedspamModifiedonbehalfby = "lk_msdyn_ocflaggedspam_modifiedonbehalfby";
				public const string LkMsdynOcgooglebusinessmessagesagentaccountCreatedby = "lk_msdyn_ocgooglebusinessmessagesagentaccount_createdby";
				public const string LkMsdynOcgooglebusinessmessagesagentaccountCreatedonbehalfby = "lk_msdyn_ocgooglebusinessmessagesagentaccount_createdonbehalfby";
				public const string LkMsdynOcgooglebusinessmessagesagentaccountModifiedby = "lk_msdyn_ocgooglebusinessmessagesagentaccount_modifiedby";
				public const string LkMsdynOcgooglebusinessmessagesagentaccountModifiedonbehalfby = "lk_msdyn_ocgooglebusinessmessagesagentaccount_modifiedonbehalfby";
				public const string LkMsdynOcgooglebusinessmessagesengagementctxCreatedby = "lk_msdyn_ocgooglebusinessmessagesengagementctx_createdby";
				public const string LkMsdynOcgooglebusinessmessagesengagementctxCreatedonbehalfby = "lk_msdyn_ocgooglebusinessmessagesengagementctx_createdonbehalfby";
				public const string LkMsdynOcgooglebusinessmessagesengagementctxModifiedby = "lk_msdyn_ocgooglebusinessmessagesengagementctx_modifiedby";
				public const string LkMsdynOcgooglebusinessmessagesengagementctxModifiedonbehalfby = "lk_msdyn_ocgooglebusinessmessagesengagementctx_modifiedonbehalfby";
				public const string LkMsdynOcgooglebusinessmessagespartneraccountCreatedby = "lk_msdyn_ocgooglebusinessmessagespartneraccount_createdby";
				public const string LkMsdynOcgooglebusinessmessagespartneraccountCreatedonbehalfby = "lk_msdyn_ocgooglebusinessmessagespartneraccount_createdonbehalfby";
				public const string LkMsdynOcgooglebusinessmessagespartneraccountModifiedby = "lk_msdyn_ocgooglebusinessmessagespartneraccount_modifiedby";
				public const string LkMsdynOcgooglebusinessmessagespartneraccountModifiedonbehalfby = "lk_msdyn_ocgooglebusinessmessagespartneraccount_modifiedonbehalfby";
				public const string LkMsdynOclanguageCreatedby = "lk_msdyn_oclanguage_createdby";
				public const string LkMsdynOclanguageCreatedonbehalfby = "lk_msdyn_oclanguage_createdonbehalfby";
				public const string LkMsdynOclanguageModifiedby = "lk_msdyn_oclanguage_modifiedby";
				public const string LkMsdynOclanguageModifiedonbehalfby = "lk_msdyn_oclanguage_modifiedonbehalfby";
				public const string LkMsdynOclinechannelconfigCreatedby = "lk_msdyn_oclinechannelconfig_createdby";
				public const string LkMsdynOclinechannelconfigCreatedonbehalfby = "lk_msdyn_oclinechannelconfig_createdonbehalfby";
				public const string LkMsdynOclinechannelconfigModifiedby = "lk_msdyn_oclinechannelconfig_modifiedby";
				public const string LkMsdynOclinechannelconfigModifiedonbehalfby = "lk_msdyn_oclinechannelconfig_modifiedonbehalfby";
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
				public const string LkMsdynOcoutboundconfigurationCreatedby = "lk_msdyn_ocoutboundconfiguration_createdby";
				public const string LkMsdynOcoutboundconfigurationCreatedonbehalfby = "lk_msdyn_ocoutboundconfiguration_createdonbehalfby";
				public const string LkMsdynOcoutboundconfigurationModifiedby = "lk_msdyn_ocoutboundconfiguration_modifiedby";
				public const string LkMsdynOcoutboundconfigurationModifiedonbehalfby = "lk_msdyn_ocoutboundconfiguration_modifiedonbehalfby";
				public const string LkMsdynOcpaymentprofileCreatedby = "lk_msdyn_ocpaymentprofile_createdby";
				public const string LkMsdynOcpaymentprofileCreatedonbehalfby = "lk_msdyn_ocpaymentprofile_createdonbehalfby";
				public const string LkMsdynOcpaymentprofileModifiedby = "lk_msdyn_ocpaymentprofile_modifiedby";
				public const string LkMsdynOcpaymentprofileModifiedonbehalfby = "lk_msdyn_ocpaymentprofile_modifiedonbehalfby";
				public const string LkMsdynOcphonenumberCreatedby = "lk_msdyn_ocphonenumber_createdby";
				public const string LkMsdynOcphonenumberCreatedonbehalfby = "lk_msdyn_ocphonenumber_createdonbehalfby";
				public const string LkMsdynOcphonenumberModifiedby = "lk_msdyn_ocphonenumber_modifiedby";
				public const string LkMsdynOcphonenumberModifiedonbehalfby = "lk_msdyn_ocphonenumber_modifiedonbehalfby";
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
				public const string LkMsdynOcsmschannelsettingCreatedby = "lk_msdyn_ocsmschannelsetting_createdby";
				public const string LkMsdynOcsmschannelsettingCreatedonbehalfby = "lk_msdyn_ocsmschannelsetting_createdonbehalfby";
				public const string LkMsdynOcsmschannelsettingModifiedby = "lk_msdyn_ocsmschannelsetting_modifiedby";
				public const string LkMsdynOcsmschannelsettingModifiedonbehalfby = "lk_msdyn_ocsmschannelsetting_modifiedonbehalfby";
				public const string LkMsdynOcsmssettingsecretCreatedby = "lk_msdyn_ocsmssettingsecret_createdby";
				public const string LkMsdynOcsmssettingsecretCreatedonbehalfby = "lk_msdyn_ocsmssettingsecret_createdonbehalfby";
				public const string LkMsdynOcsmssettingsecretModifiedby = "lk_msdyn_ocsmssettingsecret_modifiedby";
				public const string LkMsdynOcsmssettingsecretModifiedonbehalfby = "lk_msdyn_ocsmssettingsecret_modifiedonbehalfby";
				public const string LkMsdynOcsystemmessageCreatedby = "lk_msdyn_ocsystemmessage_createdby";
				public const string LkMsdynOcsystemmessageCreatedonbehalfby = "lk_msdyn_ocsystemmessage_createdonbehalfby";
				public const string LkMsdynOcsystemmessageModifiedby = "lk_msdyn_ocsystemmessage_modifiedby";
				public const string LkMsdynOcsystemmessageModifiedonbehalfby = "lk_msdyn_ocsystemmessage_modifiedonbehalfby";
				public const string LkMsdynOctagCreatedby = "lk_msdyn_octag_createdby";
				public const string LkMsdynOctagCreatedonbehalfby = "lk_msdyn_octag_createdonbehalfby";
				public const string LkMsdynOctagModifiedby = "lk_msdyn_octag_modifiedby";
				public const string LkMsdynOctagModifiedonbehalfby = "lk_msdyn_octag_modifiedonbehalfby";
				public const string LkMsdynOcteamschannelconfigCreatedby = "lk_msdyn_octeamschannelconfig_createdby";
				public const string LkMsdynOcteamschannelconfigCreatedonbehalfby = "lk_msdyn_octeamschannelconfig_createdonbehalfby";
				public const string LkMsdynOcteamschannelconfigModifiedby = "lk_msdyn_octeamschannelconfig_modifiedby";
				public const string LkMsdynOcteamschannelconfigModifiedonbehalfby = "lk_msdyn_octeamschannelconfig_modifiedonbehalfby";
				public const string LkMsdynOctwitterapplicationCreatedby = "lk_msdyn_octwitterapplication_createdby";
				public const string LkMsdynOctwitterapplicationCreatedonbehalfby = "lk_msdyn_octwitterapplication_createdonbehalfby";
				public const string LkMsdynOctwitterapplicationModifiedby = "lk_msdyn_octwitterapplication_modifiedby";
				public const string LkMsdynOctwitterapplicationModifiedonbehalfby = "lk_msdyn_octwitterapplication_modifiedonbehalfby";
				public const string LkMsdynOctwitterhandleCreatedby = "lk_msdyn_octwitterhandle_createdby";
				public const string LkMsdynOctwitterhandleCreatedonbehalfby = "lk_msdyn_octwitterhandle_createdonbehalfby";
				public const string LkMsdynOctwitterhandleModifiedby = "lk_msdyn_octwitterhandle_modifiedby";
				public const string LkMsdynOctwitterhandleModifiedonbehalfby = "lk_msdyn_octwitterhandle_modifiedonbehalfby";
				public const string LkMsdynOctwitterhandleprovisioningstatusCreatedby = "lk_msdyn_octwitterhandleprovisioningstatus_createdby";
				public const string LkMsdynOctwitterhandleprovisioningstatusCreatedonbehalfby = "lk_msdyn_octwitterhandleprovisioningstatus_createdonbehalfby";
				public const string LkMsdynOctwitterhandleprovisioningstatusModifiedby = "lk_msdyn_octwitterhandleprovisioningstatus_modifiedby";
				public const string LkMsdynOctwitterhandleprovisioningstatusModifiedonbehalfby = "lk_msdyn_octwitterhandleprovisioningstatus_modifiedonbehalfby";
				public const string LkMsdynOctwitterhandlesecretCreatedby = "lk_msdyn_octwitterhandlesecret_createdby";
				public const string LkMsdynOctwitterhandlesecretCreatedonbehalfby = "lk_msdyn_octwitterhandlesecret_createdonbehalfby";
				public const string LkMsdynOctwitterhandlesecretModifiedby = "lk_msdyn_octwitterhandlesecret_modifiedby";
				public const string LkMsdynOctwitterhandlesecretModifiedonbehalfby = "lk_msdyn_octwitterhandlesecret_modifiedonbehalfby";
				public const string LkMsdynOcwechatchannelconfigCreatedby = "lk_msdyn_ocwechatchannelconfig_createdby";
				public const string LkMsdynOcwechatchannelconfigCreatedonbehalfby = "lk_msdyn_ocwechatchannelconfig_createdonbehalfby";
				public const string LkMsdynOcwechatchannelconfigModifiedby = "lk_msdyn_ocwechatchannelconfig_modifiedby";
				public const string LkMsdynOcwechatchannelconfigModifiedonbehalfby = "lk_msdyn_ocwechatchannelconfig_modifiedonbehalfby";
				public const string LkMsdynOcwhatsappchannelaccountCreatedby = "lk_msdyn_ocwhatsappchannelaccount_createdby";
				public const string LkMsdynOcwhatsappchannelaccountCreatedonbehalfby = "lk_msdyn_ocwhatsappchannelaccount_createdonbehalfby";
				public const string LkMsdynOcwhatsappchannelaccountModifiedby = "lk_msdyn_ocwhatsappchannelaccount_modifiedby";
				public const string LkMsdynOcwhatsappchannelaccountModifiedonbehalfby = "lk_msdyn_ocwhatsappchannelaccount_modifiedonbehalfby";
				public const string LkMsdynOcwhatsappchannelnumberCreatedby = "lk_msdyn_ocwhatsappchannelnumber_createdby";
				public const string LkMsdynOcwhatsappchannelnumberCreatedonbehalfby = "lk_msdyn_ocwhatsappchannelnumber_createdonbehalfby";
				public const string LkMsdynOcwhatsappchannelnumberModifiedby = "lk_msdyn_ocwhatsappchannelnumber_modifiedby";
				public const string LkMsdynOcwhatsappchannelnumberModifiedonbehalfby = "lk_msdyn_ocwhatsappchannelnumber_modifiedonbehalfby";
				public const string LkMsdynOdosfeaturemetadataCreatedby = "lk_msdyn_odosfeaturemetadata_createdby";
				public const string LkMsdynOdosfeaturemetadataCreatedonbehalfby = "lk_msdyn_odosfeaturemetadata_createdonbehalfby";
				public const string LkMsdynOdosfeaturemetadataModifiedby = "lk_msdyn_odosfeaturemetadata_modifiedby";
				public const string LkMsdynOdosfeaturemetadataModifiedonbehalfby = "lk_msdyn_odosfeaturemetadata_modifiedonbehalfby";
				public const string LkMsdynOdosmetadataCreatedby = "lk_msdyn_odosmetadata_createdby";
				public const string LkMsdynOdosmetadataCreatedonbehalfby = "lk_msdyn_odosmetadata_createdonbehalfby";
				public const string LkMsdynOdosmetadataModifiedby = "lk_msdyn_odosmetadata_modifiedby";
				public const string LkMsdynOdosmetadataModifiedonbehalfby = "lk_msdyn_odosmetadata_modifiedonbehalfby";
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
				public const string LkMsdynOpportunitykpiitemCreatedby = "lk_msdyn_opportunitykpiitem_createdby";
				public const string LkMsdynOpportunitykpiitemCreatedonbehalfby = "lk_msdyn_opportunitykpiitem_createdonbehalfby";
				public const string LkMsdynOpportunitykpiitemModifiedby = "lk_msdyn_opportunitykpiitem_modifiedby";
				public const string LkMsdynOpportunitykpiitemModifiedonbehalfby = "lk_msdyn_opportunitykpiitem_modifiedonbehalfby";
				public const string LkMsdynOpportunitylineresourcecategoryCreatedby = "lk_msdyn_opportunitylineresourcecategory_createdby";
				public const string LkMsdynOpportunitylineresourcecategoryCreatedonbehalfby = "lk_msdyn_opportunitylineresourcecategory_createdonbehalfby";
				public const string LkMsdynOpportunitylineresourcecategoryModifiedby = "lk_msdyn_opportunitylineresourcecategory_modifiedby";
				public const string LkMsdynOpportunitylineresourcecategoryModifiedonbehalfby = "lk_msdyn_opportunitylineresourcecategory_modifiedonbehalfby";
				public const string LkMsdynOpportunitylinetransactionCreatedby = "lk_msdyn_opportunitylinetransaction_createdby";
				public const string LkMsdynOpportunitylinetransactionCreatedonbehalfby = "lk_msdyn_opportunitylinetransaction_createdonbehalfby";
				public const string LkMsdynOpportunitylinetransactionModifiedby = "lk_msdyn_opportunitylinetransaction_modifiedby";
				public const string LkMsdynOpportunitylinetransactionModifiedonbehalfby = "lk_msdyn_opportunitylinetransaction_modifiedonbehalfby";
				public const string LkMsdynOpportunitylinetransactioncategoryCreatedby = "lk_msdyn_opportunitylinetransactioncategory_createdby";
				public const string LkMsdynOpportunitylinetransactioncategoryCreatedonbehalfby = "lk_msdyn_opportunitylinetransactioncategory_createdonbehalfby";
				public const string LkMsdynOpportunitylinetransactioncategoryModifiedby = "lk_msdyn_opportunitylinetransactioncategory_modifiedby";
				public const string LkMsdynOpportunitylinetransactioncategoryModifiedonbehalfby = "lk_msdyn_opportunitylinetransactioncategory_modifiedonbehalfby";
				public const string LkMsdynOpportunitylinetransactionclassificatioCreatedby = "lk_msdyn_opportunitylinetransactionclassificatio_createdby";
				public const string LkMsdynOpportunitylinetransactionclassificatioCreatedonbehalfby = "lk_msdyn_opportunitylinetransactionclassificatio_createdonbehalfby";
				public const string LkMsdynOpportunitylinetransactionclassificatioModifiedby = "lk_msdyn_opportunitylinetransactionclassificatio_modifiedby";
				public const string LkMsdynOpportunitylinetransactionclassificatioModifiedonbehalfby = "lk_msdyn_opportunitylinetransactionclassificatio_modifiedonbehalfby";
				public const string LkMsdynOpportunitymodelconfigCreatedby = "lk_msdyn_opportunitymodelconfig_createdby";
				public const string LkMsdynOpportunitymodelconfigCreatedonbehalfby = "lk_msdyn_opportunitymodelconfig_createdonbehalfby";
				public const string LkMsdynOpportunitymodelconfigModifiedby = "lk_msdyn_opportunitymodelconfig_modifiedby";
				public const string LkMsdynOpportunitymodelconfigModifiedonbehalfby = "lk_msdyn_opportunitymodelconfig_modifiedonbehalfby";
				public const string LkMsdynOpportunitypricelistCreatedby = "lk_msdyn_opportunitypricelist_createdby";
				public const string LkMsdynOpportunitypricelistCreatedonbehalfby = "lk_msdyn_opportunitypricelist_createdonbehalfby";
				public const string LkMsdynOpportunitypricelistModifiedby = "lk_msdyn_opportunitypricelist_modifiedby";
				public const string LkMsdynOpportunitypricelistModifiedonbehalfby = "lk_msdyn_opportunitypricelist_modifiedonbehalfby";
				public const string LkMsdynOrderinvoicingdateCreatedby = "lk_msdyn_orderinvoicingdate_createdby";
				public const string LkMsdynOrderinvoicingdateCreatedonbehalfby = "lk_msdyn_orderinvoicingdate_createdonbehalfby";
				public const string LkMsdynOrderinvoicingdateModifiedby = "lk_msdyn_orderinvoicingdate_modifiedby";
				public const string LkMsdynOrderinvoicingdateModifiedonbehalfby = "lk_msdyn_orderinvoicingdate_modifiedonbehalfby";
				public const string LkMsdynOrderinvoicingproductCreatedby = "lk_msdyn_orderinvoicingproduct_createdby";
				public const string LkMsdynOrderinvoicingproductCreatedonbehalfby = "lk_msdyn_orderinvoicingproduct_createdonbehalfby";
				public const string LkMsdynOrderinvoicingproductModifiedby = "lk_msdyn_orderinvoicingproduct_modifiedby";
				public const string LkMsdynOrderinvoicingproductModifiedonbehalfby = "lk_msdyn_orderinvoicingproduct_modifiedonbehalfby";
				public const string LkMsdynOrderinvoicingsetupCreatedby = "lk_msdyn_orderinvoicingsetup_createdby";
				public const string LkMsdynOrderinvoicingsetupCreatedonbehalfby = "lk_msdyn_orderinvoicingsetup_createdonbehalfby";
				public const string LkMsdynOrderinvoicingsetupModifiedby = "lk_msdyn_orderinvoicingsetup_modifiedby";
				public const string LkMsdynOrderinvoicingsetupModifiedonbehalfby = "lk_msdyn_orderinvoicingsetup_modifiedonbehalfby";
				public const string LkMsdynOrderinvoicingsetupdateCreatedby = "lk_msdyn_orderinvoicingsetupdate_createdby";
				public const string LkMsdynOrderinvoicingsetupdateCreatedonbehalfby = "lk_msdyn_orderinvoicingsetupdate_createdonbehalfby";
				public const string LkMsdynOrderinvoicingsetupdateModifiedby = "lk_msdyn_orderinvoicingsetupdate_modifiedby";
				public const string LkMsdynOrderinvoicingsetupdateModifiedonbehalfby = "lk_msdyn_orderinvoicingsetupdate_modifiedonbehalfby";
				public const string LkMsdynOrderlineresourcecategoryCreatedby = "lk_msdyn_orderlineresourcecategory_createdby";
				public const string LkMsdynOrderlineresourcecategoryCreatedonbehalfby = "lk_msdyn_orderlineresourcecategory_createdonbehalfby";
				public const string LkMsdynOrderlineresourcecategoryModifiedby = "lk_msdyn_orderlineresourcecategory_modifiedby";
				public const string LkMsdynOrderlineresourcecategoryModifiedonbehalfby = "lk_msdyn_orderlineresourcecategory_modifiedonbehalfby";
				public const string LkMsdynOrderlinetransactionCreatedby = "lk_msdyn_orderlinetransaction_createdby";
				public const string LkMsdynOrderlinetransactionCreatedonbehalfby = "lk_msdyn_orderlinetransaction_createdonbehalfby";
				public const string LkMsdynOrderlinetransactionModifiedby = "lk_msdyn_orderlinetransaction_modifiedby";
				public const string LkMsdynOrderlinetransactionModifiedonbehalfby = "lk_msdyn_orderlinetransaction_modifiedonbehalfby";
				public const string LkMsdynOrderlinetransactioncategoryCreatedby = "lk_msdyn_orderlinetransactioncategory_createdby";
				public const string LkMsdynOrderlinetransactioncategoryCreatedonbehalfby = "lk_msdyn_orderlinetransactioncategory_createdonbehalfby";
				public const string LkMsdynOrderlinetransactioncategoryModifiedby = "lk_msdyn_orderlinetransactioncategory_modifiedby";
				public const string LkMsdynOrderlinetransactioncategoryModifiedonbehalfby = "lk_msdyn_orderlinetransactioncategory_modifiedonbehalfby";
				public const string LkMsdynOrderlinetransactionclassificationCreatedby = "lk_msdyn_orderlinetransactionclassification_createdby";
				public const string LkMsdynOrderlinetransactionclassificationCreatedonbehalfby = "lk_msdyn_orderlinetransactionclassification_createdonbehalfby";
				public const string LkMsdynOrderlinetransactionclassificationModifiedby = "lk_msdyn_orderlinetransactionclassification_modifiedby";
				public const string LkMsdynOrderlinetransactionclassificationModifiedonbehalfby = "lk_msdyn_orderlinetransactionclassification_modifiedonbehalfby";
				public const string LkMsdynOrderpricelistCreatedby = "lk_msdyn_orderpricelist_createdby";
				public const string LkMsdynOrderpricelistCreatedonbehalfby = "lk_msdyn_orderpricelist_createdonbehalfby";
				public const string LkMsdynOrderpricelistModifiedby = "lk_msdyn_orderpricelist_modifiedby";
				public const string LkMsdynOrderpricelistModifiedonbehalfby = "lk_msdyn_orderpricelist_modifiedonbehalfby";
				public const string LkMsdynOrganizationalunitCreatedby = "lk_msdyn_organizationalunit_createdby";
				public const string LkMsdynOrganizationalunitCreatedonbehalfby = "lk_msdyn_organizationalunit_createdonbehalfby";
				public const string LkMsdynOrganizationalunitModifiedby = "lk_msdyn_organizationalunit_modifiedby";
				public const string LkMsdynOrganizationalunitModifiedonbehalfby = "lk_msdyn_organizationalunit_modifiedonbehalfby";
				public const string LkMsdynOrgchartnodeCreatedby = "lk_msdyn_orgchartnode_createdby";
				public const string LkMsdynOrgchartnodeCreatedonbehalfby = "lk_msdyn_orgchartnode_createdonbehalfby";
				public const string LkMsdynOrgchartnodeModifiedby = "lk_msdyn_orgchartnode_modifiedby";
				public const string LkMsdynOrgchartnodeModifiedonbehalfby = "lk_msdyn_orgchartnode_modifiedonbehalfby";
				public const string LkMsdynOriginatingqueueCreatedby = "lk_msdyn_originatingqueue_createdby";
				public const string LkMsdynOriginatingqueueCreatedonbehalfby = "lk_msdyn_originatingqueue_createdonbehalfby";
				public const string LkMsdynOriginatingqueueModifiedby = "lk_msdyn_originatingqueue_modifiedby";
				public const string LkMsdynOriginatingqueueModifiedonbehalfby = "lk_msdyn_originatingqueue_modifiedonbehalfby";
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
				public const string LkMsdynPaymentCreatedby = "lk_msdyn_payment_createdby";
				public const string LkMsdynPaymentCreatedonbehalfby = "lk_msdyn_payment_createdonbehalfby";
				public const string LkMsdynPaymentModifiedby = "lk_msdyn_payment_modifiedby";
				public const string LkMsdynPaymentModifiedonbehalfby = "lk_msdyn_payment_modifiedonbehalfby";
				public const string LkMsdynPaymentdetailCreatedby = "lk_msdyn_paymentdetail_createdby";
				public const string LkMsdynPaymentdetailCreatedonbehalfby = "lk_msdyn_paymentdetail_createdonbehalfby";
				public const string LkMsdynPaymentdetailModifiedby = "lk_msdyn_paymentdetail_modifiedby";
				public const string LkMsdynPaymentdetailModifiedonbehalfby = "lk_msdyn_paymentdetail_modifiedonbehalfby";
				public const string LkMsdynPaymentmethodCreatedby = "lk_msdyn_paymentmethod_createdby";
				public const string LkMsdynPaymentmethodCreatedonbehalfby = "lk_msdyn_paymentmethod_createdonbehalfby";
				public const string LkMsdynPaymentmethodModifiedby = "lk_msdyn_paymentmethod_modifiedby";
				public const string LkMsdynPaymentmethodModifiedonbehalfby = "lk_msdyn_paymentmethod_modifiedonbehalfby";
				public const string LkMsdynPaymenttermCreatedby = "lk_msdyn_paymentterm_createdby";
				public const string LkMsdynPaymenttermCreatedonbehalfby = "lk_msdyn_paymentterm_createdonbehalfby";
				public const string LkMsdynPaymenttermModifiedby = "lk_msdyn_paymentterm_modifiedby";
				public const string LkMsdynPaymenttermModifiedonbehalfby = "lk_msdyn_paymentterm_modifiedonbehalfby";
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
				public const string LkMsdynPostalcodeCreatedby = "lk_msdyn_postalcode_createdby";
				public const string LkMsdynPostalcodeCreatedonbehalfby = "lk_msdyn_postalcode_createdonbehalfby";
				public const string LkMsdynPostalcodeModifiedby = "lk_msdyn_postalcode_modifiedby";
				public const string LkMsdynPostalcodeModifiedonbehalfby = "lk_msdyn_postalcode_modifiedonbehalfby";
				public const string LkMsdynPostconfigCreatedby = "lk_msdyn_postconfig_createdby";
				public const string LkMsdynPostconfigCreatedonbehalfby = "lk_msdyn_postconfig_createdonbehalfby";
				public const string LkMsdynPostconfigModifiedby = "lk_msdyn_postconfig_modifiedby";
				public const string LkMsdynPostconfigModifiedonbehalfby = "lk_msdyn_postconfig_modifiedonbehalfby";
				public const string LkMsdynPostruleconfigCreatedby = "lk_msdyn_postruleconfig_createdby";
				public const string LkMsdynPostruleconfigCreatedonbehalfby = "lk_msdyn_postruleconfig_createdonbehalfby";
				public const string LkMsdynPostruleconfigModifiedby = "lk_msdyn_postruleconfig_modifiedby";
				public const string LkMsdynPostruleconfigModifiedonbehalfby = "lk_msdyn_postruleconfig_modifiedonbehalfby";
				public const string LkMsdynPredictioncomputationoperationCreatedby = "lk_msdyn_predictioncomputationoperation_createdby";
				public const string LkMsdynPredictioncomputationoperationCreatedonbehalfby = "lk_msdyn_predictioncomputationoperation_createdonbehalfby";
				public const string LkMsdynPredictioncomputationoperationModifiedby = "lk_msdyn_predictioncomputationoperation_modifiedby";
				public const string LkMsdynPredictioncomputationoperationModifiedonbehalfby = "lk_msdyn_predictioncomputationoperation_modifiedonbehalfby";
				public const string LkMsdynPredictionmodelstatusCreatedby = "lk_msdyn_predictionmodelstatus_createdby";
				public const string LkMsdynPredictionmodelstatusCreatedonbehalfby = "lk_msdyn_predictionmodelstatus_createdonbehalfby";
				public const string LkMsdynPredictionmodelstatusModifiedby = "lk_msdyn_predictionmodelstatus_modifiedby";
				public const string LkMsdynPredictionmodelstatusModifiedonbehalfby = "lk_msdyn_predictionmodelstatus_modifiedonbehalfby";
				public const string LkMsdynPredictionscheduledoperationCreatedby = "lk_msdyn_predictionscheduledoperation_createdby";
				public const string LkMsdynPredictionscheduledoperationCreatedonbehalfby = "lk_msdyn_predictionscheduledoperation_createdonbehalfby";
				public const string LkMsdynPredictionscheduledoperationModifiedby = "lk_msdyn_predictionscheduledoperation_modifiedby";
				public const string LkMsdynPredictionscheduledoperationModifiedonbehalfby = "lk_msdyn_predictionscheduledoperation_modifiedonbehalfby";
				public const string LkMsdynPredictivemodelscoreCreatedby = "lk_msdyn_predictivemodelscore_createdby";
				public const string LkMsdynPredictivemodelscoreCreatedonbehalfby = "lk_msdyn_predictivemodelscore_createdonbehalfby";
				public const string LkMsdynPredictivemodelscoreModifiedby = "lk_msdyn_predictivemodelscore_modifiedby";
				public const string LkMsdynPredictivemodelscoreModifiedonbehalfby = "lk_msdyn_predictivemodelscore_modifiedonbehalfby";
				public const string LkMsdynPredictivescoreCreatedby = "lk_msdyn_predictivescore_createdby";
				public const string LkMsdynPredictivescoreCreatedonbehalfby = "lk_msdyn_predictivescore_createdonbehalfby";
				public const string LkMsdynPredictivescoreModifiedby = "lk_msdyn_predictivescore_modifiedby";
				public const string LkMsdynPredictivescoreModifiedonbehalfby = "lk_msdyn_predictivescore_modifiedonbehalfby";
				public const string LkMsdynPredictivescoringsyncstatusCreatedby = "lk_msdyn_predictivescoringsyncstatus_createdby";
				public const string LkMsdynPredictivescoringsyncstatusCreatedonbehalfby = "lk_msdyn_predictivescoringsyncstatus_createdonbehalfby";
				public const string LkMsdynPredictivescoringsyncstatusModifiedby = "lk_msdyn_predictivescoringsyncstatus_modifiedby";
				public const string LkMsdynPredictivescoringsyncstatusModifiedonbehalfby = "lk_msdyn_predictivescoringsyncstatus_modifiedonbehalfby";
				public const string LkMsdynPredictworkhourdurationsettingCreatedby = "lk_msdyn_predictworkhourdurationsetting_createdby";
				public const string LkMsdynPredictworkhourdurationsettingCreatedonbehalfby = "lk_msdyn_predictworkhourdurationsetting_createdonbehalfby";
				public const string LkMsdynPredictworkhourdurationsettingModifiedby = "lk_msdyn_predictworkhourdurationsetting_modifiedby";
				public const string LkMsdynPredictworkhourdurationsettingModifiedonbehalfby = "lk_msdyn_predictworkhourdurationsetting_modifiedonbehalfby";
				public const string LkMsdynPreferredagentCreatedby = "lk_msdyn_preferredagent_createdby";
				public const string LkMsdynPreferredagentCreatedonbehalfby = "lk_msdyn_preferredagent_createdonbehalfby";
				public const string LkMsdynPreferredagentModifiedby = "lk_msdyn_preferredagent_modifiedby";
				public const string LkMsdynPreferredagentModifiedonbehalfby = "lk_msdyn_preferredagent_modifiedonbehalfby";
				public const string LkMsdynPreferredagentcustomeridentityCreatedby = "lk_msdyn_preferredagentcustomeridentity_createdby";
				public const string LkMsdynPreferredagentcustomeridentityCreatedonbehalfby = "lk_msdyn_preferredagentcustomeridentity_createdonbehalfby";
				public const string LkMsdynPreferredagentcustomeridentityModifiedby = "lk_msdyn_preferredagentcustomeridentity_modifiedby";
				public const string LkMsdynPreferredagentcustomeridentityModifiedonbehalfby = "lk_msdyn_preferredagentcustomeridentity_modifiedonbehalfby";
				public const string LkMsdynPreferredagentroutedentityCreatedby = "lk_msdyn_preferredagentroutedentity_createdby";
				public const string LkMsdynPreferredagentroutedentityCreatedonbehalfby = "lk_msdyn_preferredagentroutedentity_createdonbehalfby";
				public const string LkMsdynPreferredagentroutedentityModifiedby = "lk_msdyn_preferredagentroutedentity_modifiedby";
				public const string LkMsdynPreferredagentroutedentityModifiedonbehalfby = "lk_msdyn_preferredagentroutedentity_modifiedonbehalfby";
				public const string LkMsdynPresenceCreatedby = "lk_msdyn_presence_createdby";
				public const string LkMsdynPresenceCreatedonbehalfby = "lk_msdyn_presence_createdonbehalfby";
				public const string LkMsdynPresenceModifiedby = "lk_msdyn_presence_modifiedby";
				public const string LkMsdynPresenceModifiedonbehalfby = "lk_msdyn_presence_modifiedonbehalfby";
				public const string LkMsdynPriorityCreatedby = "lk_msdyn_priority_createdby";
				public const string LkMsdynPriorityCreatedonbehalfby = "lk_msdyn_priority_createdonbehalfby";
				public const string LkMsdynPriorityModifiedby = "lk_msdyn_priority_modifiedby";
				public const string LkMsdynPriorityModifiedonbehalfby = "lk_msdyn_priority_modifiedonbehalfby";
				public const string LkMsdynProblematicassetCreatedby = "lk_msdyn_problematicasset_createdby";
				public const string LkMsdynProblematicassetCreatedonbehalfby = "lk_msdyn_problematicasset_createdonbehalfby";
				public const string LkMsdynProblematicassetModifiedby = "lk_msdyn_problematicasset_modifiedby";
				public const string LkMsdynProblematicassetModifiedonbehalfby = "lk_msdyn_problematicasset_modifiedonbehalfby";
				public const string LkMsdynProblematicassetfeedbackCreatedby = "lk_msdyn_problematicassetfeedback_createdby";
				public const string LkMsdynProblematicassetfeedbackCreatedonbehalfby = "lk_msdyn_problematicassetfeedback_createdonbehalfby";
				public const string LkMsdynProblematicassetfeedbackModifiedby = "lk_msdyn_problematicassetfeedback_modifiedby";
				public const string LkMsdynProblematicassetfeedbackModifiedonbehalfby = "lk_msdyn_problematicassetfeedback_modifiedonbehalfby";
				public const string LkMsdynProcessnotesCreatedby = "lk_msdyn_processnotes_createdby";
				public const string LkMsdynProcessnotesCreatedonbehalfby = "lk_msdyn_processnotes_createdonbehalfby";
				public const string LkMsdynProcessnotesModifiedby = "lk_msdyn_processnotes_modifiedby";
				public const string LkMsdynProcessnotesModifiedonbehalfby = "lk_msdyn_processnotes_modifiedonbehalfby";
				public const string LkMsdynProductinventoryCreatedby = "lk_msdyn_productinventory_createdby";
				public const string LkMsdynProductinventoryCreatedonbehalfby = "lk_msdyn_productinventory_createdonbehalfby";
				public const string LkMsdynProductinventoryModifiedby = "lk_msdyn_productinventory_modifiedby";
				public const string LkMsdynProductinventoryModifiedonbehalfby = "lk_msdyn_productinventory_modifiedonbehalfby";
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
				public const string LkMsdynProjectCreatedby = "lk_msdyn_project_createdby";
				public const string LkMsdynProjectCreatedonbehalfby = "lk_msdyn_project_createdonbehalfby";
				public const string LkMsdynProjectModifiedby = "lk_msdyn_project_modifiedby";
				public const string LkMsdynProjectModifiedonbehalfby = "lk_msdyn_project_modifiedonbehalfby";
				public const string LkMsdynProjectapprovalCreatedby = "lk_msdyn_projectapproval_createdby";
				public const string LkMsdynProjectapprovalCreatedonbehalfby = "lk_msdyn_projectapproval_createdonbehalfby";
				public const string LkMsdynProjectapprovalModifiedby = "lk_msdyn_projectapproval_modifiedby";
				public const string LkMsdynProjectapprovalModifiedonbehalfby = "lk_msdyn_projectapproval_modifiedonbehalfby";
				public const string LkMsdynProjectparameterCreatedby = "lk_msdyn_projectparameter_createdby";
				public const string LkMsdynProjectparameterCreatedonbehalfby = "lk_msdyn_projectparameter_createdonbehalfby";
				public const string LkMsdynProjectparameterModifiedby = "lk_msdyn_projectparameter_modifiedby";
				public const string LkMsdynProjectparameterModifiedonbehalfby = "lk_msdyn_projectparameter_modifiedonbehalfby";
				public const string LkMsdynProjectparameterpricelistCreatedby = "lk_msdyn_projectparameterpricelist_createdby";
				public const string LkMsdynProjectparameterpricelistCreatedonbehalfby = "lk_msdyn_projectparameterpricelist_createdonbehalfby";
				public const string LkMsdynProjectparameterpricelistModifiedby = "lk_msdyn_projectparameterpricelist_modifiedby";
				public const string LkMsdynProjectparameterpricelistModifiedonbehalfby = "lk_msdyn_projectparameterpricelist_modifiedonbehalfby";
				public const string LkMsdynProjectpricelistCreatedby = "lk_msdyn_projectpricelist_createdby";
				public const string LkMsdynProjectpricelistCreatedonbehalfby = "lk_msdyn_projectpricelist_createdonbehalfby";
				public const string LkMsdynProjectpricelistModifiedby = "lk_msdyn_projectpricelist_modifiedby";
				public const string LkMsdynProjectpricelistModifiedonbehalfby = "lk_msdyn_projectpricelist_modifiedonbehalfby";
				public const string LkMsdynProjecttaskCreatedby = "lk_msdyn_projecttask_createdby";
				public const string LkMsdynProjecttaskCreatedonbehalfby = "lk_msdyn_projecttask_createdonbehalfby";
				public const string LkMsdynProjecttaskModifiedby = "lk_msdyn_projecttask_modifiedby";
				public const string LkMsdynProjecttaskModifiedonbehalfby = "lk_msdyn_projecttask_modifiedonbehalfby";
				public const string LkMsdynProjecttaskdependencyCreatedby = "lk_msdyn_projecttaskdependency_createdby";
				public const string LkMsdynProjecttaskdependencyCreatedonbehalfby = "lk_msdyn_projecttaskdependency_createdonbehalfby";
				public const string LkMsdynProjecttaskdependencyModifiedby = "lk_msdyn_projecttaskdependency_modifiedby";
				public const string LkMsdynProjecttaskdependencyModifiedonbehalfby = "lk_msdyn_projecttaskdependency_modifiedonbehalfby";
				public const string LkMsdynProjecttaskstatususerCreatedby = "lk_msdyn_projecttaskstatususer_createdby";
				public const string LkMsdynProjecttaskstatususerCreatedonbehalfby = "lk_msdyn_projecttaskstatususer_createdonbehalfby";
				public const string LkMsdynProjecttaskstatususerModifiedby = "lk_msdyn_projecttaskstatususer_modifiedby";
				public const string LkMsdynProjecttaskstatususerModifiedonbehalfby = "lk_msdyn_projecttaskstatususer_modifiedonbehalfby";
				public const string LkMsdynProjectteamCreatedby = "lk_msdyn_projectteam_createdby";
				public const string LkMsdynProjectteamCreatedonbehalfby = "lk_msdyn_projectteam_createdonbehalfby";
				public const string LkMsdynProjectteamModifiedby = "lk_msdyn_projectteam_modifiedby";
				public const string LkMsdynProjectteamModifiedonbehalfby = "lk_msdyn_projectteam_modifiedonbehalfby";
				public const string LkMsdynProjectteammembersignupCreatedby = "lk_msdyn_projectteammembersignup_createdby";
				public const string LkMsdynProjectteammembersignupCreatedonbehalfby = "lk_msdyn_projectteammembersignup_createdonbehalfby";
				public const string LkMsdynProjectteammembersignupModifiedby = "lk_msdyn_projectteammembersignup_modifiedby";
				public const string LkMsdynProjectteammembersignupModifiedonbehalfby = "lk_msdyn_projectteammembersignup_modifiedonbehalfby";
				public const string LkMsdynProjecttransactioncategoryCreatedby = "lk_msdyn_projecttransactioncategory_createdby";
				public const string LkMsdynProjecttransactioncategoryCreatedonbehalfby = "lk_msdyn_projecttransactioncategory_createdonbehalfby";
				public const string LkMsdynProjecttransactioncategoryModifiedby = "lk_msdyn_projecttransactioncategory_modifiedby";
				public const string LkMsdynProjecttransactioncategoryModifiedonbehalfby = "lk_msdyn_projecttransactioncategory_modifiedonbehalfby";
				public const string LkMsdynPropertyCreatedby = "lk_msdyn_property_createdby";
				public const string LkMsdynPropertyCreatedonbehalfby = "lk_msdyn_property_createdonbehalfby";
				public const string LkMsdynPropertyModifiedby = "lk_msdyn_property_modifiedby";
				public const string LkMsdynPropertyModifiedonbehalfby = "lk_msdyn_property_modifiedonbehalfby";
				public const string LkMsdynPropertyassetassociationCreatedby = "lk_msdyn_propertyassetassociation_createdby";
				public const string LkMsdynPropertyassetassociationCreatedonbehalfby = "lk_msdyn_propertyassetassociation_createdonbehalfby";
				public const string LkMsdynPropertyassetassociationModifiedby = "lk_msdyn_propertyassetassociation_modifiedby";
				public const string LkMsdynPropertyassetassociationModifiedonbehalfby = "lk_msdyn_propertyassetassociation_modifiedonbehalfby";
				public const string LkMsdynPropertylocationassociationCreatedby = "lk_msdyn_propertylocationassociation_createdby";
				public const string LkMsdynPropertylocationassociationCreatedonbehalfby = "lk_msdyn_propertylocationassociation_createdonbehalfby";
				public const string LkMsdynPropertylocationassociationModifiedby = "lk_msdyn_propertylocationassociation_modifiedby";
				public const string LkMsdynPropertylocationassociationModifiedonbehalfby = "lk_msdyn_propertylocationassociation_modifiedonbehalfby";
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
				public const string LkMsdynPurchaseorderCreatedby = "lk_msdyn_purchaseorder_createdby";
				public const string LkMsdynPurchaseorderCreatedonbehalfby = "lk_msdyn_purchaseorder_createdonbehalfby";
				public const string LkMsdynPurchaseorderModifiedby = "lk_msdyn_purchaseorder_modifiedby";
				public const string LkMsdynPurchaseorderModifiedonbehalfby = "lk_msdyn_purchaseorder_modifiedonbehalfby";
				public const string LkMsdynPurchaseorderbillCreatedby = "lk_msdyn_purchaseorderbill_createdby";
				public const string LkMsdynPurchaseorderbillCreatedonbehalfby = "lk_msdyn_purchaseorderbill_createdonbehalfby";
				public const string LkMsdynPurchaseorderbillModifiedby = "lk_msdyn_purchaseorderbill_modifiedby";
				public const string LkMsdynPurchaseorderbillModifiedonbehalfby = "lk_msdyn_purchaseorderbill_modifiedonbehalfby";
				public const string LkMsdynPurchaseorderproductCreatedby = "lk_msdyn_purchaseorderproduct_createdby";
				public const string LkMsdynPurchaseorderproductCreatedonbehalfby = "lk_msdyn_purchaseorderproduct_createdonbehalfby";
				public const string LkMsdynPurchaseorderproductModifiedby = "lk_msdyn_purchaseorderproduct_modifiedby";
				public const string LkMsdynPurchaseorderproductModifiedonbehalfby = "lk_msdyn_purchaseorderproduct_modifiedonbehalfby";
				public const string LkMsdynPurchaseorderreceiptCreatedby = "lk_msdyn_purchaseorderreceipt_createdby";
				public const string LkMsdynPurchaseorderreceiptCreatedonbehalfby = "lk_msdyn_purchaseorderreceipt_createdonbehalfby";
				public const string LkMsdynPurchaseorderreceiptModifiedby = "lk_msdyn_purchaseorderreceipt_modifiedby";
				public const string LkMsdynPurchaseorderreceiptModifiedonbehalfby = "lk_msdyn_purchaseorderreceipt_modifiedonbehalfby";
				public const string LkMsdynPurchaseorderreceiptproductCreatedby = "lk_msdyn_purchaseorderreceiptproduct_createdby";
				public const string LkMsdynPurchaseorderreceiptproductCreatedonbehalfby = "lk_msdyn_purchaseorderreceiptproduct_createdonbehalfby";
				public const string LkMsdynPurchaseorderreceiptproductModifiedby = "lk_msdyn_purchaseorderreceiptproduct_modifiedby";
				public const string LkMsdynPurchaseorderreceiptproductModifiedonbehalfby = "lk_msdyn_purchaseorderreceiptproduct_modifiedonbehalfby";
				public const string LkMsdynPurchaseordersubstatusCreatedby = "lk_msdyn_purchaseordersubstatus_createdby";
				public const string LkMsdynPurchaseordersubstatusCreatedonbehalfby = "lk_msdyn_purchaseordersubstatus_createdonbehalfby";
				public const string LkMsdynPurchaseordersubstatusModifiedby = "lk_msdyn_purchaseordersubstatus_modifiedby";
				public const string LkMsdynPurchaseordersubstatusModifiedonbehalfby = "lk_msdyn_purchaseordersubstatus_modifiedonbehalfby";
				public const string LkMsdynQuestionsequenceCreatedby = "lk_msdyn_questionsequence_createdby";
				public const string LkMsdynQuestionsequenceCreatedonbehalfby = "lk_msdyn_questionsequence_createdonbehalfby";
				public const string LkMsdynQuestionsequenceModifiedby = "lk_msdyn_questionsequence_modifiedby";
				public const string LkMsdynQuestionsequenceModifiedonbehalfby = "lk_msdyn_questionsequence_modifiedonbehalfby";
				public const string LkMsdynQuotebookingincidentCreatedby = "lk_msdyn_quotebookingincident_createdby";
				public const string LkMsdynQuotebookingincidentCreatedonbehalfby = "lk_msdyn_quotebookingincident_createdonbehalfby";
				public const string LkMsdynQuotebookingincidentModifiedby = "lk_msdyn_quotebookingincident_modifiedby";
				public const string LkMsdynQuotebookingincidentModifiedonbehalfby = "lk_msdyn_quotebookingincident_modifiedonbehalfby";
				public const string LkMsdynQuotebookingproductCreatedby = "lk_msdyn_quotebookingproduct_createdby";
				public const string LkMsdynQuotebookingproductCreatedonbehalfby = "lk_msdyn_quotebookingproduct_createdonbehalfby";
				public const string LkMsdynQuotebookingproductModifiedby = "lk_msdyn_quotebookingproduct_modifiedby";
				public const string LkMsdynQuotebookingproductModifiedonbehalfby = "lk_msdyn_quotebookingproduct_modifiedonbehalfby";
				public const string LkMsdynQuotebookingserviceCreatedby = "lk_msdyn_quotebookingservice_createdby";
				public const string LkMsdynQuotebookingserviceCreatedonbehalfby = "lk_msdyn_quotebookingservice_createdonbehalfby";
				public const string LkMsdynQuotebookingserviceModifiedby = "lk_msdyn_quotebookingservice_modifiedby";
				public const string LkMsdynQuotebookingserviceModifiedonbehalfby = "lk_msdyn_quotebookingservice_modifiedonbehalfby";
				public const string LkMsdynQuotebookingservicetaskCreatedby = "lk_msdyn_quotebookingservicetask_createdby";
				public const string LkMsdynQuotebookingservicetaskCreatedonbehalfby = "lk_msdyn_quotebookingservicetask_createdonbehalfby";
				public const string LkMsdynQuotebookingservicetaskModifiedby = "lk_msdyn_quotebookingservicetask_modifiedby";
				public const string LkMsdynQuotebookingservicetaskModifiedonbehalfby = "lk_msdyn_quotebookingservicetask_modifiedonbehalfby";
				public const string LkMsdynQuotebookingsetupCreatedby = "lk_msdyn_quotebookingsetup_createdby";
				public const string LkMsdynQuotebookingsetupCreatedonbehalfby = "lk_msdyn_quotebookingsetup_createdonbehalfby";
				public const string LkMsdynQuotebookingsetupModifiedby = "lk_msdyn_quotebookingsetup_modifiedby";
				public const string LkMsdynQuotebookingsetupModifiedonbehalfby = "lk_msdyn_quotebookingsetup_modifiedonbehalfby";
				public const string LkMsdynQuoteinvoicingproductCreatedby = "lk_msdyn_quoteinvoicingproduct_createdby";
				public const string LkMsdynQuoteinvoicingproductCreatedonbehalfby = "lk_msdyn_quoteinvoicingproduct_createdonbehalfby";
				public const string LkMsdynQuoteinvoicingproductModifiedby = "lk_msdyn_quoteinvoicingproduct_modifiedby";
				public const string LkMsdynQuoteinvoicingproductModifiedonbehalfby = "lk_msdyn_quoteinvoicingproduct_modifiedonbehalfby";
				public const string LkMsdynQuoteinvoicingsetupCreatedby = "lk_msdyn_quoteinvoicingsetup_createdby";
				public const string LkMsdynQuoteinvoicingsetupCreatedonbehalfby = "lk_msdyn_quoteinvoicingsetup_createdonbehalfby";
				public const string LkMsdynQuoteinvoicingsetupModifiedby = "lk_msdyn_quoteinvoicingsetup_modifiedby";
				public const string LkMsdynQuoteinvoicingsetupModifiedonbehalfby = "lk_msdyn_quoteinvoicingsetup_modifiedonbehalfby";
				public const string LkMsdynQuotelineanalyticsbreakdownCreatedby = "lk_msdyn_quotelineanalyticsbreakdown_createdby";
				public const string LkMsdynQuotelineanalyticsbreakdownCreatedonbehalfby = "lk_msdyn_quotelineanalyticsbreakdown_createdonbehalfby";
				public const string LkMsdynQuotelineanalyticsbreakdownModifiedby = "lk_msdyn_quotelineanalyticsbreakdown_modifiedby";
				public const string LkMsdynQuotelineanalyticsbreakdownModifiedonbehalfby = "lk_msdyn_quotelineanalyticsbreakdown_modifiedonbehalfby";
				public const string LkMsdynQuotelineinvoicescheduleCreatedby = "lk_msdyn_quotelineinvoiceschedule_createdby";
				public const string LkMsdynQuotelineinvoicescheduleCreatedonbehalfby = "lk_msdyn_quotelineinvoiceschedule_createdonbehalfby";
				public const string LkMsdynQuotelineinvoicescheduleModifiedby = "lk_msdyn_quotelineinvoiceschedule_modifiedby";
				public const string LkMsdynQuotelineinvoicescheduleModifiedonbehalfby = "lk_msdyn_quotelineinvoiceschedule_modifiedonbehalfby";
				public const string LkMsdynQuotelineresourcecategoryCreatedby = "lk_msdyn_quotelineresourcecategory_createdby";
				public const string LkMsdynQuotelineresourcecategoryCreatedonbehalfby = "lk_msdyn_quotelineresourcecategory_createdonbehalfby";
				public const string LkMsdynQuotelineresourcecategoryModifiedby = "lk_msdyn_quotelineresourcecategory_modifiedby";
				public const string LkMsdynQuotelineresourcecategoryModifiedonbehalfby = "lk_msdyn_quotelineresourcecategory_modifiedonbehalfby";
				public const string LkMsdynQuotelinescheduleofvalueCreatedby = "lk_msdyn_quotelinescheduleofvalue_createdby";
				public const string LkMsdynQuotelinescheduleofvalueCreatedonbehalfby = "lk_msdyn_quotelinescheduleofvalue_createdonbehalfby";
				public const string LkMsdynQuotelinescheduleofvalueModifiedby = "lk_msdyn_quotelinescheduleofvalue_modifiedby";
				public const string LkMsdynQuotelinescheduleofvalueModifiedonbehalfby = "lk_msdyn_quotelinescheduleofvalue_modifiedonbehalfby";
				public const string LkMsdynQuotelinetransactionCreatedby = "lk_msdyn_quotelinetransaction_createdby";
				public const string LkMsdynQuotelinetransactionCreatedonbehalfby = "lk_msdyn_quotelinetransaction_createdonbehalfby";
				public const string LkMsdynQuotelinetransactionModifiedby = "lk_msdyn_quotelinetransaction_modifiedby";
				public const string LkMsdynQuotelinetransactionModifiedonbehalfby = "lk_msdyn_quotelinetransaction_modifiedonbehalfby";
				public const string LkMsdynQuotelinetransactioncategoryCreatedby = "lk_msdyn_quotelinetransactioncategory_createdby";
				public const string LkMsdynQuotelinetransactioncategoryCreatedonbehalfby = "lk_msdyn_quotelinetransactioncategory_createdonbehalfby";
				public const string LkMsdynQuotelinetransactioncategoryModifiedby = "lk_msdyn_quotelinetransactioncategory_modifiedby";
				public const string LkMsdynQuotelinetransactioncategoryModifiedonbehalfby = "lk_msdyn_quotelinetransactioncategory_modifiedonbehalfby";
				public const string LkMsdynQuotelinetransactionclassificationCreatedby = "lk_msdyn_quotelinetransactionclassification_createdby";
				public const string LkMsdynQuotelinetransactionclassificationCreatedonbehalfby = "lk_msdyn_quotelinetransactionclassification_createdonbehalfby";
				public const string LkMsdynQuotelinetransactionclassificationModifiedby = "lk_msdyn_quotelinetransactionclassification_modifiedby";
				public const string LkMsdynQuotelinetransactionclassificationModifiedonbehalfby = "lk_msdyn_quotelinetransactionclassification_modifiedonbehalfby";
				public const string LkMsdynQuotepricelistCreatedby = "lk_msdyn_quotepricelist_createdby";
				public const string LkMsdynQuotepricelistCreatedonbehalfby = "lk_msdyn_quotepricelist_createdonbehalfby";
				public const string LkMsdynQuotepricelistModifiedby = "lk_msdyn_quotepricelist_modifiedby";
				public const string LkMsdynQuotepricelistModifiedonbehalfby = "lk_msdyn_quotepricelist_modifiedonbehalfby";
				public const string LkMsdynReadtrackerCreatedby = "lk_msdyn_readtracker_createdby";
				public const string LkMsdynReadtrackerCreatedonbehalfby = "lk_msdyn_readtracker_createdonbehalfby";
				public const string LkMsdynReadtrackerModifiedby = "lk_msdyn_readtracker_modifiedby";
				public const string LkMsdynReadtrackerModifiedonbehalfby = "lk_msdyn_readtracker_modifiedonbehalfby";
				public const string LkMsdynReadtrackingenabledinfoCreatedby = "lk_msdyn_readtrackingenabledinfo_createdby";
				public const string LkMsdynReadtrackingenabledinfoCreatedonbehalfby = "lk_msdyn_readtrackingenabledinfo_createdonbehalfby";
				public const string LkMsdynReadtrackingenabledinfoModifiedby = "lk_msdyn_readtrackingenabledinfo_modifiedby";
				public const string LkMsdynReadtrackingenabledinfoModifiedonbehalfby = "lk_msdyn_readtrackingenabledinfo_modifiedonbehalfby";
				public const string LkMsdynRealtimescoringCreatedby = "lk_msdyn_realtimescoring_createdby";
				public const string LkMsdynRealtimescoringCreatedonbehalfby = "lk_msdyn_realtimescoring_createdonbehalfby";
				public const string LkMsdynRealtimescoringModifiedby = "lk_msdyn_realtimescoring_modifiedby";
				public const string LkMsdynRealtimescoringModifiedonbehalfby = "lk_msdyn_realtimescoring_modifiedonbehalfby";
				public const string LkMsdynRealtimescoringoperationCreatedby = "lk_msdyn_realtimescoringoperation_createdby";
				public const string LkMsdynRealtimescoringoperationCreatedonbehalfby = "lk_msdyn_realtimescoringoperation_createdonbehalfby";
				public const string LkMsdynRealtimescoringoperationModifiedby = "lk_msdyn_realtimescoringoperation_modifiedby";
				public const string LkMsdynRealtimescoringoperationModifiedonbehalfby = "lk_msdyn_realtimescoringoperation_modifiedonbehalfby";
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
				public const string LkMsdynRecurringsalesactionv2Createdby = "lk_msdyn_recurringsalesactionv2_createdby";
				public const string LkMsdynRecurringsalesactionv2Createdonbehalfby = "lk_msdyn_recurringsalesactionv2_createdonbehalfby";
				public const string LkMsdynRecurringsalesactionv2Modifiedby = "lk_msdyn_recurringsalesactionv2_modifiedby";
				public const string LkMsdynRecurringsalesactionv2Modifiedonbehalfby = "lk_msdyn_recurringsalesactionv2_modifiedonbehalfby";
				public const string LkMsdynRelationshipanalyticsmetadataCreatedby = "lk_msdyn_relationshipanalyticsmetadata_createdby";
				public const string LkMsdynRelationshipanalyticsmetadataCreatedonbehalfby = "lk_msdyn_relationshipanalyticsmetadata_createdonbehalfby";
				public const string LkMsdynRelationshipanalyticsmetadataModifiedby = "lk_msdyn_relationshipanalyticsmetadata_modifiedby";
				public const string LkMsdynRelationshipanalyticsmetadataModifiedonbehalfby = "lk_msdyn_relationshipanalyticsmetadata_modifiedonbehalfby";
				public const string LkMsdynRelationshipinsightsunifiedconfigCreatedby = "lk_msdyn_relationshipinsightsunifiedconfig_createdby";
				public const string LkMsdynRelationshipinsightsunifiedconfigCreatedonbehalfby = "lk_msdyn_relationshipinsightsunifiedconfig_createdonbehalfby";
				public const string LkMsdynRelationshipinsightsunifiedconfigModifiedby = "lk_msdyn_relationshipinsightsunifiedconfig_modifiedby";
				public const string LkMsdynRelationshipinsightsunifiedconfigModifiedonbehalfby = "lk_msdyn_relationshipinsightsunifiedconfig_modifiedonbehalfby";
				public const string LkMsdynReportbookmarkCreatedby = "lk_msdyn_reportbookmark_createdby";
				public const string LkMsdynReportbookmarkCreatedonbehalfby = "lk_msdyn_reportbookmark_createdonbehalfby";
				public const string LkMsdynReportbookmarkModifiedby = "lk_msdyn_reportbookmark_modifiedby";
				public const string LkMsdynReportbookmarkModifiedonbehalfby = "lk_msdyn_reportbookmark_modifiedonbehalfby";
				public const string LkMsdynRequirementchangeCreatedby = "lk_msdyn_requirementchange_createdby";
				public const string LkMsdynRequirementchangeCreatedonbehalfby = "lk_msdyn_requirementchange_createdonbehalfby";
				public const string LkMsdynRequirementchangeModifiedby = "lk_msdyn_requirementchange_modifiedby";
				public const string LkMsdynRequirementchangeModifiedonbehalfby = "lk_msdyn_requirementchange_modifiedonbehalfby";
				public const string LkMsdynRequirementcharacteristicCreatedby = "lk_msdyn_requirementcharacteristic_createdby";
				public const string LkMsdynRequirementcharacteristicCreatedonbehalfby = "lk_msdyn_requirementcharacteristic_createdonbehalfby";
				public const string LkMsdynRequirementcharacteristicModifiedby = "lk_msdyn_requirementcharacteristic_modifiedby";
				public const string LkMsdynRequirementcharacteristicModifiedonbehalfby = "lk_msdyn_requirementcharacteristic_modifiedonbehalfby";
				public const string LkMsdynRequirementdependencyCreatedby = "lk_msdyn_requirementdependency_createdby";
				public const string LkMsdynRequirementdependencyCreatedonbehalfby = "lk_msdyn_requirementdependency_createdonbehalfby";
				public const string LkMsdynRequirementdependencyModifiedby = "lk_msdyn_requirementdependency_modifiedby";
				public const string LkMsdynRequirementdependencyModifiedonbehalfby = "lk_msdyn_requirementdependency_modifiedonbehalfby";
				public const string LkMsdynRequirementgroupCreatedby = "lk_msdyn_requirementgroup_createdby";
				public const string LkMsdynRequirementgroupCreatedonbehalfby = "lk_msdyn_requirementgroup_createdonbehalfby";
				public const string LkMsdynRequirementgroupModifiedby = "lk_msdyn_requirementgroup_modifiedby";
				public const string LkMsdynRequirementgroupModifiedonbehalfby = "lk_msdyn_requirementgroup_modifiedonbehalfby";
				public const string LkMsdynRequirementorganizationunitCreatedby = "lk_msdyn_requirementorganizationunit_createdby";
				public const string LkMsdynRequirementorganizationunitCreatedonbehalfby = "lk_msdyn_requirementorganizationunit_createdonbehalfby";
				public const string LkMsdynRequirementorganizationunitModifiedby = "lk_msdyn_requirementorganizationunit_modifiedby";
				public const string LkMsdynRequirementorganizationunitModifiedonbehalfby = "lk_msdyn_requirementorganizationunit_modifiedonbehalfby";
				public const string LkMsdynRequirementrelationshipCreatedby = "lk_msdyn_requirementrelationship_createdby";
				public const string LkMsdynRequirementrelationshipCreatedonbehalfby = "lk_msdyn_requirementrelationship_createdonbehalfby";
				public const string LkMsdynRequirementrelationshipModifiedby = "lk_msdyn_requirementrelationship_modifiedby";
				public const string LkMsdynRequirementrelationshipModifiedonbehalfby = "lk_msdyn_requirementrelationship_modifiedonbehalfby";
				public const string LkMsdynRequirementresourcecategoryCreatedby = "lk_msdyn_requirementresourcecategory_createdby";
				public const string LkMsdynRequirementresourcecategoryCreatedonbehalfby = "lk_msdyn_requirementresourcecategory_createdonbehalfby";
				public const string LkMsdynRequirementresourcecategoryModifiedby = "lk_msdyn_requirementresourcecategory_modifiedby";
				public const string LkMsdynRequirementresourcecategoryModifiedonbehalfby = "lk_msdyn_requirementresourcecategory_modifiedonbehalfby";
				public const string LkMsdynRequirementresourcepreferenceCreatedby = "lk_msdyn_requirementresourcepreference_createdby";
				public const string LkMsdynRequirementresourcepreferenceCreatedonbehalfby = "lk_msdyn_requirementresourcepreference_createdonbehalfby";
				public const string LkMsdynRequirementresourcepreferenceModifiedby = "lk_msdyn_requirementresourcepreference_modifiedby";
				public const string LkMsdynRequirementresourcepreferenceModifiedonbehalfby = "lk_msdyn_requirementresourcepreference_modifiedonbehalfby";
				public const string LkMsdynRequirementstatusCreatedby = "lk_msdyn_requirementstatus_createdby";
				public const string LkMsdynRequirementstatusCreatedonbehalfby = "lk_msdyn_requirementstatus_createdonbehalfby";
				public const string LkMsdynRequirementstatusModifiedby = "lk_msdyn_requirementstatus_modifiedby";
				public const string LkMsdynRequirementstatusModifiedonbehalfby = "lk_msdyn_requirementstatus_modifiedonbehalfby";
				public const string LkMsdynResolutionCreatedby = "lk_msdyn_resolution_createdby";
				public const string LkMsdynResolutionCreatedonbehalfby = "lk_msdyn_resolution_createdonbehalfby";
				public const string LkMsdynResolutionModifiedby = "lk_msdyn_resolution_modifiedby";
				public const string LkMsdynResolutionModifiedonbehalfby = "lk_msdyn_resolution_modifiedonbehalfby";
				public const string LkMsdynResourceassignmentCreatedby = "lk_msdyn_resourceassignment_createdby";
				public const string LkMsdynResourceassignmentCreatedonbehalfby = "lk_msdyn_resourceassignment_createdonbehalfby";
				public const string LkMsdynResourceassignmentModifiedby = "lk_msdyn_resourceassignment_modifiedby";
				public const string LkMsdynResourceassignmentModifiedonbehalfby = "lk_msdyn_resourceassignment_modifiedonbehalfby";
				public const string LkMsdynResourceassignmentdetailCreatedby = "lk_msdyn_resourceassignmentdetail_createdby";
				public const string LkMsdynResourceassignmentdetailCreatedonbehalfby = "lk_msdyn_resourceassignmentdetail_createdonbehalfby";
				public const string LkMsdynResourceassignmentdetailModifiedby = "lk_msdyn_resourceassignmentdetail_modifiedby";
				public const string LkMsdynResourceassignmentdetailModifiedonbehalfby = "lk_msdyn_resourceassignmentdetail_modifiedonbehalfby";
				public const string LkMsdynResourcecategorymarkuppricelevelCreatedby = "lk_msdyn_resourcecategorymarkuppricelevel_createdby";
				public const string LkMsdynResourcecategorymarkuppricelevelCreatedonbehalfby = "lk_msdyn_resourcecategorymarkuppricelevel_createdonbehalfby";
				public const string LkMsdynResourcecategorymarkuppricelevelModifiedby = "lk_msdyn_resourcecategorymarkuppricelevel_modifiedby";
				public const string LkMsdynResourcecategorymarkuppricelevelModifiedonbehalfby = "lk_msdyn_resourcecategorymarkuppricelevel_modifiedonbehalfby";
				public const string LkMsdynResourcecategorypricelevelCreatedby = "lk_msdyn_resourcecategorypricelevel_createdby";
				public const string LkMsdynResourcecategorypricelevelCreatedonbehalfby = "lk_msdyn_resourcecategorypricelevel_createdonbehalfby";
				public const string LkMsdynResourcecategorypricelevelModifiedby = "lk_msdyn_resourcecategorypricelevel_modifiedby";
				public const string LkMsdynResourcecategorypricelevelModifiedonbehalfby = "lk_msdyn_resourcecategorypricelevel_modifiedonbehalfby";
				public const string LkMsdynResourcepaytypeCreatedby = "lk_msdyn_resourcepaytype_createdby";
				public const string LkMsdynResourcepaytypeCreatedonbehalfby = "lk_msdyn_resourcepaytype_createdonbehalfby";
				public const string LkMsdynResourcepaytypeModifiedby = "lk_msdyn_resourcepaytype_modifiedby";
				public const string LkMsdynResourcepaytypeModifiedonbehalfby = "lk_msdyn_resourcepaytype_modifiedonbehalfby";
				public const string LkMsdynResourcerequestCreatedby = "lk_msdyn_resourcerequest_createdby";
				public const string LkMsdynResourcerequestCreatedonbehalfby = "lk_msdyn_resourcerequest_createdonbehalfby";
				public const string LkMsdynResourcerequestModifiedby = "lk_msdyn_resourcerequest_modifiedby";
				public const string LkMsdynResourcerequestModifiedonbehalfby = "lk_msdyn_resourcerequest_modifiedonbehalfby";
				public const string LkMsdynResourcerequirementCreatedby = "lk_msdyn_resourcerequirement_createdby";
				public const string LkMsdynResourcerequirementCreatedonbehalfby = "lk_msdyn_resourcerequirement_createdonbehalfby";
				public const string LkMsdynResourcerequirementModifiedby = "lk_msdyn_resourcerequirement_modifiedby";
				public const string LkMsdynResourcerequirementModifiedonbehalfby = "lk_msdyn_resourcerequirement_modifiedonbehalfby";
				public const string LkMsdynResourcerequirementdetailCreatedby = "lk_msdyn_resourcerequirementdetail_createdby";
				public const string LkMsdynResourcerequirementdetailCreatedonbehalfby = "lk_msdyn_resourcerequirementdetail_createdonbehalfby";
				public const string LkMsdynResourcerequirementdetailModifiedby = "lk_msdyn_resourcerequirementdetail_modifiedby";
				public const string LkMsdynResourcerequirementdetailModifiedonbehalfby = "lk_msdyn_resourcerequirementdetail_modifiedonbehalfby";
				public const string LkMsdynResourceterritoryCreatedby = "lk_msdyn_resourceterritory_createdby";
				public const string LkMsdynResourceterritoryCreatedonbehalfby = "lk_msdyn_resourceterritory_createdonbehalfby";
				public const string LkMsdynResourceterritoryModifiedby = "lk_msdyn_resourceterritory_modifiedby";
				public const string LkMsdynResourceterritoryModifiedonbehalfby = "lk_msdyn_resourceterritory_modifiedonbehalfby";
				public const string LkMsdynRichtextfileCreatedby = "lk_msdyn_richtextfile_createdby";
				public const string LkMsdynRichtextfileCreatedonbehalfby = "lk_msdyn_richtextfile_createdonbehalfby";
				public const string LkMsdynRichtextfileModifiedby = "lk_msdyn_richtextfile_modifiedby";
				public const string LkMsdynRichtextfileModifiedonbehalfby = "lk_msdyn_richtextfile_modifiedonbehalfby";
				public const string LkMsdynRmaCreatedby = "lk_msdyn_rma_createdby";
				public const string LkMsdynRmaCreatedonbehalfby = "lk_msdyn_rma_createdonbehalfby";
				public const string LkMsdynRmaModifiedby = "lk_msdyn_rma_modifiedby";
				public const string LkMsdynRmaModifiedonbehalfby = "lk_msdyn_rma_modifiedonbehalfby";
				public const string LkMsdynRmaproductCreatedby = "lk_msdyn_rmaproduct_createdby";
				public const string LkMsdynRmaproductCreatedonbehalfby = "lk_msdyn_rmaproduct_createdonbehalfby";
				public const string LkMsdynRmaproductModifiedby = "lk_msdyn_rmaproduct_modifiedby";
				public const string LkMsdynRmaproductModifiedonbehalfby = "lk_msdyn_rmaproduct_modifiedonbehalfby";
				public const string LkMsdynRmareceiptCreatedby = "lk_msdyn_rmareceipt_createdby";
				public const string LkMsdynRmareceiptCreatedonbehalfby = "lk_msdyn_rmareceipt_createdonbehalfby";
				public const string LkMsdynRmareceiptModifiedby = "lk_msdyn_rmareceipt_modifiedby";
				public const string LkMsdynRmareceiptModifiedonbehalfby = "lk_msdyn_rmareceipt_modifiedonbehalfby";
				public const string LkMsdynRmareceiptproductCreatedby = "lk_msdyn_rmareceiptproduct_createdby";
				public const string LkMsdynRmareceiptproductCreatedonbehalfby = "lk_msdyn_rmareceiptproduct_createdonbehalfby";
				public const string LkMsdynRmareceiptproductModifiedby = "lk_msdyn_rmareceiptproduct_modifiedby";
				public const string LkMsdynRmareceiptproductModifiedonbehalfby = "lk_msdyn_rmareceiptproduct_modifiedonbehalfby";
				public const string LkMsdynRmasubstatusCreatedby = "lk_msdyn_rmasubstatus_createdby";
				public const string LkMsdynRmasubstatusCreatedonbehalfby = "lk_msdyn_rmasubstatus_createdonbehalfby";
				public const string LkMsdynRmasubstatusModifiedby = "lk_msdyn_rmasubstatus_modifiedby";
				public const string LkMsdynRmasubstatusModifiedonbehalfby = "lk_msdyn_rmasubstatus_modifiedonbehalfby";
				public const string LkMsdynRolecompetencyrequirementCreatedby = "lk_msdyn_rolecompetencyrequirement_createdby";
				public const string LkMsdynRolecompetencyrequirementCreatedonbehalfby = "lk_msdyn_rolecompetencyrequirement_createdonbehalfby";
				public const string LkMsdynRolecompetencyrequirementModifiedby = "lk_msdyn_rolecompetencyrequirement_modifiedby";
				public const string LkMsdynRolecompetencyrequirementModifiedonbehalfby = "lk_msdyn_rolecompetencyrequirement_modifiedonbehalfby";
				public const string LkMsdynRoleutilizationCreatedby = "lk_msdyn_roleutilization_createdby";
				public const string LkMsdynRoleutilizationCreatedonbehalfby = "lk_msdyn_roleutilization_createdonbehalfby";
				public const string LkMsdynRoleutilizationModifiedby = "lk_msdyn_roleutilization_modifiedby";
				public const string LkMsdynRoleutilizationModifiedonbehalfby = "lk_msdyn_roleutilization_modifiedonbehalfby";
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
				public const string LkMsdynRtvCreatedby = "lk_msdyn_rtv_createdby";
				public const string LkMsdynRtvCreatedonbehalfby = "lk_msdyn_rtv_createdonbehalfby";
				public const string LkMsdynRtvModifiedby = "lk_msdyn_rtv_modifiedby";
				public const string LkMsdynRtvModifiedonbehalfby = "lk_msdyn_rtv_modifiedonbehalfby";
				public const string LkMsdynRtvproductCreatedby = "lk_msdyn_rtvproduct_createdby";
				public const string LkMsdynRtvproductCreatedonbehalfby = "lk_msdyn_rtvproduct_createdonbehalfby";
				public const string LkMsdynRtvproductModifiedby = "lk_msdyn_rtvproduct_modifiedby";
				public const string LkMsdynRtvproductModifiedonbehalfby = "lk_msdyn_rtvproduct_modifiedonbehalfby";
				public const string LkMsdynRtvsubstatusCreatedby = "lk_msdyn_rtvsubstatus_createdby";
				public const string LkMsdynRtvsubstatusCreatedonbehalfby = "lk_msdyn_rtvsubstatus_createdonbehalfby";
				public const string LkMsdynRtvsubstatusModifiedby = "lk_msdyn_rtvsubstatus_modifiedby";
				public const string LkMsdynRtvsubstatusModifiedonbehalfby = "lk_msdyn_rtvsubstatus_modifiedonbehalfby";
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
				public const string LkMsdynSalescopilotinsightCreatedby = "lk_msdyn_salescopilotinsight_createdby";
				public const string LkMsdynSalescopilotinsightCreatedonbehalfby = "lk_msdyn_salescopilotinsight_createdonbehalfby";
				public const string LkMsdynSalescopilotinsightModifiedby = "lk_msdyn_salescopilotinsight_modifiedby";
				public const string LkMsdynSalescopilotinsightModifiedonbehalfby = "lk_msdyn_salescopilotinsight_modifiedonbehalfby";
				public const string LkMsdynSalescopilotorgsettingsCreatedby = "lk_msdyn_salescopilotorgsettings_createdby";
				public const string LkMsdynSalescopilotorgsettingsCreatedonbehalfby = "lk_msdyn_salescopilotorgsettings_createdonbehalfby";
				public const string LkMsdynSalescopilotorgsettingsModifiedby = "lk_msdyn_salescopilotorgsettings_modifiedby";
				public const string LkMsdynSalescopilotorgsettingsModifiedonbehalfby = "lk_msdyn_salescopilotorgsettings_modifiedonbehalfby";
				public const string LkMsdynSalescopilotusersettingCreatedby = "lk_msdyn_salescopilotusersetting_createdby";
				public const string LkMsdynSalescopilotusersettingCreatedonbehalfby = "lk_msdyn_salescopilotusersetting_createdonbehalfby";
				public const string LkMsdynSalescopilotusersettingModifiedby = "lk_msdyn_salescopilotusersetting_modifiedby";
				public const string LkMsdynSalescopilotusersettingModifiedonbehalfby = "lk_msdyn_salescopilotusersetting_modifiedonbehalfby";
				public const string LkMsdynSalesforcestructuredobjectCreatedby = "lk_msdyn_salesforcestructuredobject_createdby";
				public const string LkMsdynSalesforcestructuredobjectCreatedonbehalfby = "lk_msdyn_salesforcestructuredobject_createdonbehalfby";
				public const string LkMsdynSalesforcestructuredobjectModifiedby = "lk_msdyn_salesforcestructuredobject_modifiedby";
				public const string LkMsdynSalesforcestructuredobjectModifiedonbehalfby = "lk_msdyn_salesforcestructuredobject_modifiedonbehalfby";
				public const string LkMsdynSalesforcestructuredqnaconfigCreatedby = "lk_msdyn_salesforcestructuredqnaconfig_createdby";
				public const string LkMsdynSalesforcestructuredqnaconfigCreatedonbehalfby = "lk_msdyn_salesforcestructuredqnaconfig_createdonbehalfby";
				public const string LkMsdynSalesforcestructuredqnaconfigModifiedby = "lk_msdyn_salesforcestructuredqnaconfig_modifiedby";
				public const string LkMsdynSalesforcestructuredqnaconfigModifiedonbehalfby = "lk_msdyn_salesforcestructuredqnaconfig_modifiedonbehalfby";
				public const string LkMsdynSalesinsightssettingsCreatedby = "lk_msdyn_salesinsightssettings_createdby";
				public const string LkMsdynSalesinsightssettingsCreatedonbehalfby = "lk_msdyn_salesinsightssettings_createdonbehalfby";
				public const string LkMsdynSalesinsightssettingsModifiedby = "lk_msdyn_salesinsightssettings_modifiedby";
				public const string LkMsdynSalesinsightssettingsModifiedonbehalfby = "lk_msdyn_salesinsightssettings_modifiedonbehalfby";
				public const string LkMsdynSalesocmessageCreatedby = "lk_msdyn_salesocmessage_createdby";
				public const string LkMsdynSalesocmessageCreatedonbehalfby = "lk_msdyn_salesocmessage_createdonbehalfby";
				public const string LkMsdynSalesocmessageModifiedby = "lk_msdyn_salesocmessage_modifiedby";
				public const string LkMsdynSalesocmessageModifiedonbehalfby = "lk_msdyn_salesocmessage_modifiedonbehalfby";
				public const string LkMsdynSalesocsmstemplateCreatedby = "lk_msdyn_salesocsmstemplate_createdby";
				public const string LkMsdynSalesocsmstemplateCreatedonbehalfby = "lk_msdyn_salesocsmstemplate_createdonbehalfby";
				public const string LkMsdynSalesocsmstemplateModifiedby = "lk_msdyn_salesocsmstemplate_modifiedby";
				public const string LkMsdynSalesocsmstemplateModifiedonbehalfby = "lk_msdyn_salesocsmstemplate_modifiedonbehalfby";
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
				public const string LkMsdynScenarioCreatedby = "lk_msdyn_scenario_createdby";
				public const string LkMsdynScenarioCreatedonbehalfby = "lk_msdyn_scenario_createdonbehalfby";
				public const string LkMsdynScenarioModifiedby = "lk_msdyn_scenario_modifiedby";
				public const string LkMsdynScenarioModifiedonbehalfby = "lk_msdyn_scenario_modifiedonbehalfby";
				public const string LkMsdynScheduleCreatedby = "lk_msdyn_schedule_createdby";
				public const string LkMsdynScheduleCreatedonbehalfby = "lk_msdyn_schedule_createdonbehalfby";
				public const string LkMsdynScheduleModifiedby = "lk_msdyn_schedule_modifiedby";
				public const string LkMsdynScheduleModifiedonbehalfby = "lk_msdyn_schedule_modifiedonbehalfby";
				public const string LkMsdynScheduleboardsettingCreatedby = "lk_msdyn_scheduleboardsetting_createdby";
				public const string LkMsdynScheduleboardsettingCreatedonbehalfby = "lk_msdyn_scheduleboardsetting_createdonbehalfby";
				public const string LkMsdynScheduleboardsettingModifiedby = "lk_msdyn_scheduleboardsetting_modifiedby";
				public const string LkMsdynScheduleboardsettingModifiedonbehalfby = "lk_msdyn_scheduleboardsetting_modifiedonbehalfby";
				public const string LkMsdynSchedulingfeatureflagCreatedby = "lk_msdyn_schedulingfeatureflag_createdby";
				public const string LkMsdynSchedulingfeatureflagCreatedonbehalfby = "lk_msdyn_schedulingfeatureflag_createdonbehalfby";
				public const string LkMsdynSchedulingfeatureflagModifiedby = "lk_msdyn_schedulingfeatureflag_modifiedby";
				public const string LkMsdynSchedulingfeatureflagModifiedonbehalfby = "lk_msdyn_schedulingfeatureflag_modifiedonbehalfby";
				public const string LkMsdynSchedulingparameterCreatedby = "lk_msdyn_schedulingparameter_createdby";
				public const string LkMsdynSchedulingparameterCreatedonbehalfby = "lk_msdyn_schedulingparameter_createdonbehalfby";
				public const string LkMsdynSchedulingparameterModifiedby = "lk_msdyn_schedulingparameter_modifiedby";
				public const string LkMsdynSchedulingparameterModifiedonbehalfby = "lk_msdyn_schedulingparameter_modifiedonbehalfby";
				public const string LkMsdynSciconversationCreatedby = "lk_msdyn_sciconversation_createdby";
				public const string LkMsdynSciconversationCreatedonbehalfby = "lk_msdyn_sciconversation_createdonbehalfby";
				public const string LkMsdynSciconversationModifiedby = "lk_msdyn_sciconversation_modifiedby";
				public const string LkMsdynSciconversationModifiedonbehalfby = "lk_msdyn_sciconversation_modifiedonbehalfby";
				public const string LkMsdynScicustomemailhighlightCreatedby = "lk_msdyn_scicustomemailhighlight_createdby";
				public const string LkMsdynScicustomemailhighlightCreatedonbehalfby = "lk_msdyn_scicustomemailhighlight_createdonbehalfby";
				public const string LkMsdynScicustomemailhighlightModifiedby = "lk_msdyn_scicustomemailhighlight_modifiedby";
				public const string LkMsdynScicustomemailhighlightModifiedonbehalfby = "lk_msdyn_scicustomemailhighlight_modifiedonbehalfby";
				public const string LkMsdynScicustomhighlightCreatedby = "lk_msdyn_scicustomhighlight_createdby";
				public const string LkMsdynScicustomhighlightCreatedonbehalfby = "lk_msdyn_scicustomhighlight_createdonbehalfby";
				public const string LkMsdynScicustomhighlightModifiedby = "lk_msdyn_scicustomhighlight_modifiedby";
				public const string LkMsdynScicustomhighlightModifiedonbehalfby = "lk_msdyn_scicustomhighlight_modifiedonbehalfby";
				public const string LkMsdynScicustompublisherCreatedby = "lk_msdyn_scicustompublisher_createdby";
				public const string LkMsdynScicustompublisherCreatedonbehalfby = "lk_msdyn_scicustompublisher_createdonbehalfby";
				public const string LkMsdynScicustompublisherModifiedby = "lk_msdyn_scicustompublisher_modifiedby";
				public const string LkMsdynScicustompublisherModifiedonbehalfby = "lk_msdyn_scicustompublisher_modifiedonbehalfby";
				public const string LkMsdynScienvironmentsettingsCreatedby = "lk_msdyn_scienvironmentsettings_createdby";
				public const string LkMsdynScienvironmentsettingsCreatedonbehalfby = "lk_msdyn_scienvironmentsettings_createdonbehalfby";
				public const string LkMsdynScienvironmentsettingsModifiedby = "lk_msdyn_scienvironmentsettings_modifiedby";
				public const string LkMsdynScienvironmentsettingsModifiedonbehalfby = "lk_msdyn_scienvironmentsettings_modifiedonbehalfby";
				public const string LkMsdynSciusersettingsCreatedby = "lk_msdyn_sciusersettings_createdby";
				public const string LkMsdynSciusersettingsCreatedonbehalfby = "lk_msdyn_sciusersettings_createdonbehalfby";
				public const string LkMsdynSciusersettingsModifiedby = "lk_msdyn_sciusersettings_modifiedby";
				public const string LkMsdynSciusersettingsModifiedonbehalfby = "lk_msdyn_sciusersettings_modifiedonbehalfby";
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
				public const string LkMsdynServicecopilotpluginCreatedby = "lk_msdyn_servicecopilotplugin_createdby";
				public const string LkMsdynServicecopilotpluginCreatedonbehalfby = "lk_msdyn_servicecopilotplugin_createdonbehalfby";
				public const string LkMsdynServicecopilotpluginModifiedby = "lk_msdyn_servicecopilotplugin_modifiedby";
				public const string LkMsdynServicecopilotpluginModifiedonbehalfby = "lk_msdyn_servicecopilotplugin_modifiedonbehalfby";
				public const string LkMsdynServicecopilotpluginroleCreatedby = "lk_msdyn_servicecopilotpluginrole_createdby";
				public const string LkMsdynServicecopilotpluginroleCreatedonbehalfby = "lk_msdyn_servicecopilotpluginrole_createdonbehalfby";
				public const string LkMsdynServicecopilotpluginroleModifiedby = "lk_msdyn_servicecopilotpluginrole_modifiedby";
				public const string LkMsdynServicecopilotpluginroleModifiedonbehalfby = "lk_msdyn_servicecopilotpluginrole_modifiedonbehalfby";
				public const string LkMsdynServiceoneprovisioningrequestCreatedby = "lk_msdyn_serviceoneprovisioningrequest_createdby";
				public const string LkMsdynServiceoneprovisioningrequestCreatedonbehalfby = "lk_msdyn_serviceoneprovisioningrequest_createdonbehalfby";
				public const string LkMsdynServiceoneprovisioningrequestModifiedby = "lk_msdyn_serviceoneprovisioningrequest_modifiedby";
				public const string LkMsdynServiceoneprovisioningrequestModifiedonbehalfby = "lk_msdyn_serviceoneprovisioningrequest_modifiedonbehalfby";
				public const string LkMsdynServicetasktypeCreatedby = "lk_msdyn_servicetasktype_createdby";
				public const string LkMsdynServicetasktypeCreatedonbehalfby = "lk_msdyn_servicetasktype_createdonbehalfby";
				public const string LkMsdynServicetasktypeModifiedby = "lk_msdyn_servicetasktype_modifiedby";
				public const string LkMsdynServicetasktypeModifiedonbehalfby = "lk_msdyn_servicetasktype_modifiedonbehalfby";
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
				public const string LkMsdynShipviaCreatedby = "lk_msdyn_shipvia_createdby";
				public const string LkMsdynShipviaCreatedonbehalfby = "lk_msdyn_shipvia_createdonbehalfby";
				public const string LkMsdynShipviaModifiedby = "lk_msdyn_shipvia_modifiedby";
				public const string LkMsdynShipviaModifiedonbehalfby = "lk_msdyn_shipvia_modifiedonbehalfby";
				public const string LkMsdynSiconfigCreatedby = "lk_msdyn_siconfig_createdby";
				public const string LkMsdynSiconfigCreatedonbehalfby = "lk_msdyn_siconfig_createdonbehalfby";
				public const string LkMsdynSiconfigModifiedby = "lk_msdyn_siconfig_modifiedby";
				public const string LkMsdynSiconfigModifiedonbehalfby = "lk_msdyn_siconfig_modifiedonbehalfby";
				public const string LkMsdynSikeyvalueconfigCreatedby = "lk_msdyn_sikeyvalueconfig_createdby";
				public const string LkMsdynSikeyvalueconfigCreatedonbehalfby = "lk_msdyn_sikeyvalueconfig_createdonbehalfby";
				public const string LkMsdynSikeyvalueconfigModifiedby = "lk_msdyn_sikeyvalueconfig_modifiedby";
				public const string LkMsdynSikeyvalueconfigModifiedonbehalfby = "lk_msdyn_sikeyvalueconfig_modifiedonbehalfby";
				public const string LkMsdynSimilarentitiesfeatureimportanceCreatedby = "lk_msdyn_similarentitiesfeatureimportance_createdby";
				public const string LkMsdynSimilarentitiesfeatureimportanceCreatedonbehalfby = "lk_msdyn_similarentitiesfeatureimportance_createdonbehalfby";
				public const string LkMsdynSimilarentitiesfeatureimportanceModifiedby = "lk_msdyn_similarentitiesfeatureimportance_modifiedby";
				public const string LkMsdynSimilarentitiesfeatureimportanceModifiedonbehalfby = "lk_msdyn_similarentitiesfeatureimportance_modifiedonbehalfby";
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
				public const string LkMsdynSmsengagementctxCreatedby = "lk_msdyn_smsengagementctx_createdby";
				public const string LkMsdynSmsengagementctxCreatedonbehalfby = "lk_msdyn_smsengagementctx_createdonbehalfby";
				public const string LkMsdynSmsengagementctxModifiedby = "lk_msdyn_smsengagementctx_modifiedby";
				public const string LkMsdynSmsengagementctxModifiedonbehalfby = "lk_msdyn_smsengagementctx_modifiedonbehalfby";
				public const string LkMsdynSmsnumberCreatedby = "lk_msdyn_smsnumber_createdby";
				public const string LkMsdynSmsnumberCreatedonbehalfby = "lk_msdyn_smsnumber_createdonbehalfby";
				public const string LkMsdynSmsnumberModifiedby = "lk_msdyn_smsnumber_modifiedby";
				public const string LkMsdynSmsnumberModifiedonbehalfby = "lk_msdyn_smsnumber_modifiedonbehalfby";
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
				public const string LkMsdynSubmodeldefinitionCreatedby = "lk_msdyn_submodeldefinition_createdby";
				public const string LkMsdynSubmodeldefinitionCreatedonbehalfby = "lk_msdyn_submodeldefinition_createdonbehalfby";
				public const string LkMsdynSubmodeldefinitionModifiedby = "lk_msdyn_submodeldefinition_modifiedby";
				public const string LkMsdynSubmodeldefinitionModifiedonbehalfby = "lk_msdyn_submodeldefinition_modifiedonbehalfby";
				public const string LkMsdynSuggestionassignmentruleCreatedby = "lk_msdyn_suggestionassignmentrule_createdby";
				public const string LkMsdynSuggestionassignmentruleCreatedonbehalfby = "lk_msdyn_suggestionassignmentrule_createdonbehalfby";
				public const string LkMsdynSuggestionassignmentruleModifiedby = "lk_msdyn_suggestionassignmentrule_modifiedby";
				public const string LkMsdynSuggestionassignmentruleModifiedonbehalfby = "lk_msdyn_suggestionassignmentrule_modifiedonbehalfby";
				public const string LkMsdynSuggestioninteractionCreatedby = "lk_msdyn_suggestioninteraction_createdby";
				public const string LkMsdynSuggestioninteractionCreatedonbehalfby = "lk_msdyn_suggestioninteraction_createdonbehalfby";
				public const string LkMsdynSuggestioninteractionModifiedby = "lk_msdyn_suggestioninteraction_modifiedby";
				public const string LkMsdynSuggestioninteractionModifiedonbehalfby = "lk_msdyn_suggestioninteraction_modifiedonbehalfby";
				public const string LkMsdynSuggestionprincipalobjectaccessCreatedby = "lk_msdyn_suggestionprincipalobjectaccess_createdby";
				public const string LkMsdynSuggestionprincipalobjectaccessCreatedonbehalfby = "lk_msdyn_suggestionprincipalobjectaccess_createdonbehalfby";
				public const string LkMsdynSuggestionprincipalobjectaccessModifiedby = "lk_msdyn_suggestionprincipalobjectaccess_modifiedby";
				public const string LkMsdynSuggestionprincipalobjectaccessModifiedonbehalfby = "lk_msdyn_suggestionprincipalobjectaccess_modifiedonbehalfby";
				public const string LkMsdynSuggestionrequestpayloadCreatedby = "lk_msdyn_suggestionrequestpayload_createdby";
				public const string LkMsdynSuggestionrequestpayloadCreatedonbehalfby = "lk_msdyn_suggestionrequestpayload_createdonbehalfby";
				public const string LkMsdynSuggestionrequestpayloadModifiedby = "lk_msdyn_suggestionrequestpayload_modifiedby";
				public const string LkMsdynSuggestionrequestpayloadModifiedonbehalfby = "lk_msdyn_suggestionrequestpayload_modifiedonbehalfby";
				public const string LkMsdynSuggestionsellerpriorityCreatedby = "lk_msdyn_suggestionsellerpriority_createdby";
				public const string LkMsdynSuggestionsellerpriorityCreatedonbehalfby = "lk_msdyn_suggestionsellerpriority_createdonbehalfby";
				public const string LkMsdynSuggestionsellerpriorityModifiedby = "lk_msdyn_suggestionsellerpriority_modifiedby";
				public const string LkMsdynSuggestionsellerpriorityModifiedonbehalfby = "lk_msdyn_suggestionsellerpriority_modifiedonbehalfby";
				public const string LkMsdynSuggestionsmodelsummaryCreatedby = "lk_msdyn_suggestionsmodelsummary_createdby";
				public const string LkMsdynSuggestionsmodelsummaryCreatedonbehalfby = "lk_msdyn_suggestionsmodelsummary_createdonbehalfby";
				public const string LkMsdynSuggestionsmodelsummaryModifiedby = "lk_msdyn_suggestionsmodelsummary_modifiedby";
				public const string LkMsdynSuggestionsmodelsummaryModifiedonbehalfby = "lk_msdyn_suggestionsmodelsummary_modifiedonbehalfby";
				public const string LkMsdynSuggestionssettingCreatedby = "lk_msdyn_suggestionssetting_createdby";
				public const string LkMsdynSuggestionssettingCreatedonbehalfby = "lk_msdyn_suggestionssetting_createdonbehalfby";
				public const string LkMsdynSuggestionssettingModifiedby = "lk_msdyn_suggestionssetting_modifiedby";
				public const string LkMsdynSuggestionssettingModifiedonbehalfby = "lk_msdyn_suggestionssetting_modifiedonbehalfby";
				public const string LkMsdynSurveyquestionCreatedby = "lk_msdyn_surveyquestion_createdby";
				public const string LkMsdynSurveyquestionCreatedonbehalfby = "lk_msdyn_surveyquestion_createdonbehalfby";
				public const string LkMsdynSurveyquestionModifiedby = "lk_msdyn_surveyquestion_modifiedby";
				public const string LkMsdynSurveyquestionModifiedonbehalfby = "lk_msdyn_surveyquestion_modifiedonbehalfby";
				public const string LkMsdynSurveysettingCreatedby = "lk_msdyn_surveysetting_createdby";
				public const string LkMsdynSurveysettingCreatedonbehalfby = "lk_msdyn_surveysetting_createdonbehalfby";
				public const string LkMsdynSurveysettingModifiedby = "lk_msdyn_surveysetting_modifiedby";
				public const string LkMsdynSurveysettingModifiedonbehalfby = "lk_msdyn_surveysetting_modifiedonbehalfby";
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
				public const string LkMsdynSystemuserschedulersettingCreatedby = "lk_msdyn_systemuserschedulersetting_createdby";
				public const string LkMsdynSystemuserschedulersettingCreatedonbehalfby = "lk_msdyn_systemuserschedulersetting_createdonbehalfby";
				public const string LkMsdynSystemuserschedulersettingModifiedby = "lk_msdyn_systemuserschedulersetting_modifiedby";
				public const string LkMsdynSystemuserschedulersettingModifiedonbehalfby = "lk_msdyn_systemuserschedulersetting_modifiedonbehalfby";
				public const string LkMsdynTaggedrecordCreatedby = "lk_msdyn_taggedrecord_createdby";
				public const string LkMsdynTaggedrecordCreatedonbehalfby = "lk_msdyn_taggedrecord_createdonbehalfby";
				public const string LkMsdynTaggedrecordModifiedby = "lk_msdyn_taggedrecord_modifiedby";
				public const string LkMsdynTaggedrecordModifiedonbehalfby = "lk_msdyn_taggedrecord_modifiedonbehalfby";
				public const string LkMsdynTaxcodeCreatedby = "lk_msdyn_taxcode_createdby";
				public const string LkMsdynTaxcodeCreatedonbehalfby = "lk_msdyn_taxcode_createdonbehalfby";
				public const string LkMsdynTaxcodeModifiedby = "lk_msdyn_taxcode_modifiedby";
				public const string LkMsdynTaxcodeModifiedonbehalfby = "lk_msdyn_taxcode_modifiedonbehalfby";
				public const string LkMsdynTaxcodedetailCreatedby = "lk_msdyn_taxcodedetail_createdby";
				public const string LkMsdynTaxcodedetailCreatedonbehalfby = "lk_msdyn_taxcodedetail_createdonbehalfby";
				public const string LkMsdynTaxcodedetailModifiedby = "lk_msdyn_taxcodedetail_modifiedby";
				public const string LkMsdynTaxcodedetailModifiedonbehalfby = "lk_msdyn_taxcodedetail_modifiedonbehalfby";
				public const string LkMsdynTeamschannelengagementctxCreatedby = "lk_msdyn_teamschannelengagementctx_createdby";
				public const string LkMsdynTeamschannelengagementctxCreatedonbehalfby = "lk_msdyn_teamschannelengagementctx_createdonbehalfby";
				public const string LkMsdynTeamschannelengagementctxModifiedby = "lk_msdyn_teamschannelengagementctx_modifiedby";
				public const string LkMsdynTeamschannelengagementctxModifiedonbehalfby = "lk_msdyn_teamschannelengagementctx_modifiedonbehalfby";
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
				public const string LkMsdynTeamsengagementctxCreatedby = "lk_msdyn_teamsengagementctx_createdby";
				public const string LkMsdynTeamsengagementctxCreatedonbehalfby = "lk_msdyn_teamsengagementctx_createdonbehalfby";
				public const string LkMsdynTeamsengagementctxModifiedby = "lk_msdyn_teamsengagementctx_modifiedby";
				public const string LkMsdynTeamsengagementctxModifiedonbehalfby = "lk_msdyn_teamsengagementctx_modifiedonbehalfby";
				public const string LkMsdynTemplateforpropertiesCreatedby = "lk_msdyn_templateforproperties_createdby";
				public const string LkMsdynTemplateforpropertiesCreatedonbehalfby = "lk_msdyn_templateforproperties_createdonbehalfby";
				public const string LkMsdynTemplateforpropertiesModifiedby = "lk_msdyn_templateforproperties_modifiedby";
				public const string LkMsdynTemplateforpropertiesModifiedonbehalfby = "lk_msdyn_templateforproperties_modifiedonbehalfby";
				public const string LkMsdynTemplateparameterCreatedby = "lk_msdyn_templateparameter_createdby";
				public const string LkMsdynTemplateparameterCreatedonbehalfby = "lk_msdyn_templateparameter_createdonbehalfby";
				public const string LkMsdynTemplateparameterModifiedby = "lk_msdyn_templateparameter_modifiedby";
				public const string LkMsdynTemplateparameterModifiedonbehalfby = "lk_msdyn_templateparameter_modifiedonbehalfby";
				public const string LkMsdynTemplatetagsCreatedby = "lk_msdyn_templatetags_createdby";
				public const string LkMsdynTemplatetagsCreatedonbehalfby = "lk_msdyn_templatetags_createdonbehalfby";
				public const string LkMsdynTemplatetagsModifiedby = "lk_msdyn_templatetags_modifiedby";
				public const string LkMsdynTemplatetagsModifiedonbehalfby = "lk_msdyn_templatetags_modifiedonbehalfby";
				public const string LkMsdynTimeentryCreatedby = "lk_msdyn_timeentry_createdby";
				public const string LkMsdynTimeentryCreatedonbehalfby = "lk_msdyn_timeentry_createdonbehalfby";
				public const string LkMsdynTimeentryModifiedby = "lk_msdyn_timeentry_modifiedby";
				public const string LkMsdynTimeentryModifiedonbehalfby = "lk_msdyn_timeentry_modifiedonbehalfby";
				public const string LkMsdynTimeentrysettingCreatedby = "lk_msdyn_timeentrysetting_createdby";
				public const string LkMsdynTimeentrysettingCreatedonbehalfby = "lk_msdyn_timeentrysetting_createdonbehalfby";
				public const string LkMsdynTimeentrysettingModifiedby = "lk_msdyn_timeentrysetting_modifiedby";
				public const string LkMsdynTimeentrysettingModifiedonbehalfby = "lk_msdyn_timeentrysetting_modifiedonbehalfby";
				public const string LkMsdynTimegroupCreatedby = "lk_msdyn_timegroup_createdby";
				public const string LkMsdynTimegroupCreatedonbehalfby = "lk_msdyn_timegroup_createdonbehalfby";
				public const string LkMsdynTimegroupModifiedby = "lk_msdyn_timegroup_modifiedby";
				public const string LkMsdynTimegroupModifiedonbehalfby = "lk_msdyn_timegroup_modifiedonbehalfby";
				public const string LkMsdynTimegroupdetailCreatedby = "lk_msdyn_timegroupdetail_createdby";
				public const string LkMsdynTimegroupdetailCreatedonbehalfby = "lk_msdyn_timegroupdetail_createdonbehalfby";
				public const string LkMsdynTimegroupdetailModifiedby = "lk_msdyn_timegroupdetail_modifiedby";
				public const string LkMsdynTimegroupdetailModifiedonbehalfby = "lk_msdyn_timegroupdetail_modifiedonbehalfby";
				public const string LkMsdynTimelinepinCreatedby = "lk_msdyn_timelinepin_createdby";
				public const string LkMsdynTimelinepinCreatedonbehalfby = "lk_msdyn_timelinepin_createdonbehalfby";
				public const string LkMsdynTimelinepinModifiedby = "lk_msdyn_timelinepin_modifiedby";
				public const string LkMsdynTimelinepinModifiedonbehalfby = "lk_msdyn_timelinepin_modifiedonbehalfby";
				public const string LkMsdynTimeoffcalendarCreatedby = "lk_msdyn_timeoffcalendar_createdby";
				public const string LkMsdynTimeoffcalendarCreatedonbehalfby = "lk_msdyn_timeoffcalendar_createdonbehalfby";
				public const string LkMsdynTimeoffcalendarModifiedby = "lk_msdyn_timeoffcalendar_modifiedby";
				public const string LkMsdynTimeoffcalendarModifiedonbehalfby = "lk_msdyn_timeoffcalendar_modifiedonbehalfby";
				public const string LkMsdynTimeoffrequestCreatedby = "lk_msdyn_timeoffrequest_createdby";
				public const string LkMsdynTimeoffrequestCreatedonbehalfby = "lk_msdyn_timeoffrequest_createdonbehalfby";
				public const string LkMsdynTimeoffrequestModifiedby = "lk_msdyn_timeoffrequest_modifiedby";
				public const string LkMsdynTimeoffrequestModifiedonbehalfby = "lk_msdyn_timeoffrequest_modifiedonbehalfby";
				public const string LkMsdynTimespentCreatedby = "lk_msdyn_timespent_createdby";
				public const string LkMsdynTimespentCreatedonbehalfby = "lk_msdyn_timespent_createdonbehalfby";
				public const string LkMsdynTimespentModifiedby = "lk_msdyn_timespent_modifiedby";
				public const string LkMsdynTimespentModifiedonbehalfby = "lk_msdyn_timespent_modifiedonbehalfby";
				public const string LkMsdynTourCreatedby = "lk_msdyn_tour_createdby";
				public const string LkMsdynTourCreatedonbehalfby = "lk_msdyn_tour_createdonbehalfby";
				public const string LkMsdynTourModifiedby = "lk_msdyn_tour_modifiedby";
				public const string LkMsdynTourModifiedonbehalfby = "lk_msdyn_tour_modifiedonbehalfby";
				public const string LkMsdynTradeCreatedby = "lk_msdyn_trade_createdby";
				public const string LkMsdynTradeCreatedonbehalfby = "lk_msdyn_trade_createdonbehalfby";
				public const string LkMsdynTradeModifiedby = "lk_msdyn_trade_modifiedby";
				public const string LkMsdynTradeModifiedonbehalfby = "lk_msdyn_trade_modifiedonbehalfby";
				public const string LkMsdynTradecoverageCreatedby = "lk_msdyn_tradecoverage_createdby";
				public const string LkMsdynTradecoverageCreatedonbehalfby = "lk_msdyn_tradecoverage_createdonbehalfby";
				public const string LkMsdynTradecoverageModifiedby = "lk_msdyn_tradecoverage_modifiedby";
				public const string LkMsdynTradecoverageModifiedonbehalfby = "lk_msdyn_tradecoverage_modifiedonbehalfby";
				public const string LkMsdynTrainingresultCreatedby = "lk_msdyn_trainingresult_createdby";
				public const string LkMsdynTrainingresultCreatedonbehalfby = "lk_msdyn_trainingresult_createdonbehalfby";
				public const string LkMsdynTrainingresultModifiedby = "lk_msdyn_trainingresult_modifiedby";
				public const string LkMsdynTrainingresultModifiedonbehalfby = "lk_msdyn_trainingresult_modifiedonbehalfby";
				public const string LkMsdynTransactioncategoryCreatedby = "lk_msdyn_transactioncategory_createdby";
				public const string LkMsdynTransactioncategoryCreatedonbehalfby = "lk_msdyn_transactioncategory_createdonbehalfby";
				public const string LkMsdynTransactioncategoryModifiedby = "lk_msdyn_transactioncategory_modifiedby";
				public const string LkMsdynTransactioncategoryModifiedonbehalfby = "lk_msdyn_transactioncategory_modifiedonbehalfby";
				public const string LkMsdynTransactioncategoryclassificationCreatedby = "lk_msdyn_transactioncategoryclassification_createdby";
				public const string LkMsdynTransactioncategoryclassificationCreatedonbehalfby = "lk_msdyn_transactioncategoryclassification_createdonbehalfby";
				public const string LkMsdynTransactioncategoryclassificationModifiedby = "lk_msdyn_transactioncategoryclassification_modifiedby";
				public const string LkMsdynTransactioncategoryclassificationModifiedonbehalfby = "lk_msdyn_transactioncategoryclassification_modifiedonbehalfby";
				public const string LkMsdynTransactioncategoryhierarchyelementCreatedby = "lk_msdyn_transactioncategoryhierarchyelement_createdby";
				public const string LkMsdynTransactioncategoryhierarchyelementCreatedonbehalfby = "lk_msdyn_transactioncategoryhierarchyelement_createdonbehalfby";
				public const string LkMsdynTransactioncategoryhierarchyelementModifiedby = "lk_msdyn_transactioncategoryhierarchyelement_modifiedby";
				public const string LkMsdynTransactioncategoryhierarchyelementModifiedonbehalfby = "lk_msdyn_transactioncategoryhierarchyelement_modifiedonbehalfby";
				public const string LkMsdynTransactioncategorypricelevelCreatedby = "lk_msdyn_transactioncategorypricelevel_createdby";
				public const string LkMsdynTransactioncategorypricelevelCreatedonbehalfby = "lk_msdyn_transactioncategorypricelevel_createdonbehalfby";
				public const string LkMsdynTransactioncategorypricelevelModifiedby = "lk_msdyn_transactioncategorypricelevel_modifiedby";
				public const string LkMsdynTransactioncategorypricelevelModifiedonbehalfby = "lk_msdyn_transactioncategorypricelevel_modifiedonbehalfby";
				public const string LkMsdynTransactionconnectionCreatedby = "lk_msdyn_transactionconnection_createdby";
				public const string LkMsdynTransactionconnectionCreatedonbehalfby = "lk_msdyn_transactionconnection_createdonbehalfby";
				public const string LkMsdynTransactionconnectionModifiedby = "lk_msdyn_transactionconnection_modifiedby";
				public const string LkMsdynTransactionconnectionModifiedonbehalfby = "lk_msdyn_transactionconnection_modifiedonbehalfby";
				public const string LkMsdynTransactionoriginCreatedby = "lk_msdyn_transactionorigin_createdby";
				public const string LkMsdynTransactionoriginCreatedonbehalfby = "lk_msdyn_transactionorigin_createdonbehalfby";
				public const string LkMsdynTransactionoriginModifiedby = "lk_msdyn_transactionorigin_modifiedby";
				public const string LkMsdynTransactionoriginModifiedonbehalfby = "lk_msdyn_transactionorigin_modifiedonbehalfby";
				public const string LkMsdynTransactiontypeCreatedby = "lk_msdyn_transactiontype_createdby";
				public const string LkMsdynTransactiontypeCreatedonbehalfby = "lk_msdyn_transactiontype_createdonbehalfby";
				public const string LkMsdynTransactiontypeModifiedby = "lk_msdyn_transactiontype_modifiedby";
				public const string LkMsdynTransactiontypeModifiedonbehalfby = "lk_msdyn_transactiontype_modifiedonbehalfby";
				public const string LkMsdynTranscriptCreatedby = "lk_msdyn_transcript_createdby";
				public const string LkMsdynTranscriptCreatedonbehalfby = "lk_msdyn_transcript_createdonbehalfby";
				public const string LkMsdynTranscriptModifiedby = "lk_msdyn_transcript_modifiedby";
				public const string LkMsdynTranscriptModifiedonbehalfby = "lk_msdyn_transcript_modifiedonbehalfby";
				public const string LkMsdynTwitterengagementctxCreatedby = "lk_msdyn_twitterengagementctx_createdby";
				public const string LkMsdynTwitterengagementctxCreatedonbehalfby = "lk_msdyn_twitterengagementctx_createdonbehalfby";
				public const string LkMsdynTwitterengagementctxModifiedby = "lk_msdyn_twitterengagementctx_modifiedby";
				public const string LkMsdynTwitterengagementctxModifiedonbehalfby = "lk_msdyn_twitterengagementctx_modifiedonbehalfby";
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
				public const string LkMsdynUniquenumberCreatedby = "lk_msdyn_uniquenumber_createdby";
				public const string LkMsdynUniquenumberCreatedonbehalfby = "lk_msdyn_uniquenumber_createdonbehalfby";
				public const string LkMsdynUniquenumberModifiedby = "lk_msdyn_uniquenumber_modifiedby";
				public const string LkMsdynUniquenumberModifiedonbehalfby = "lk_msdyn_uniquenumber_modifiedonbehalfby";
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
				public const string LkMsdynUserworkhistoryCreatedby = "lk_msdyn_userworkhistory_createdby";
				public const string LkMsdynUserworkhistoryCreatedonbehalfby = "lk_msdyn_userworkhistory_createdonbehalfby";
				public const string LkMsdynUserworkhistoryModifiedby = "lk_msdyn_userworkhistory_modifiedby";
				public const string LkMsdynUserworkhistoryModifiedonbehalfby = "lk_msdyn_userworkhistory_modifiedonbehalfby";
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
				public const string LkMsdynVivaorgextensioncredCreatedby = "lk_msdyn_vivaorgextensioncred_createdby";
				public const string LkMsdynVivaorgextensioncredCreatedonbehalfby = "lk_msdyn_vivaorgextensioncred_createdonbehalfby";
				public const string LkMsdynVivaorgextensioncredModifiedby = "lk_msdyn_vivaorgextensioncred_modifiedby";
				public const string LkMsdynVivaorgextensioncredModifiedonbehalfby = "lk_msdyn_vivaorgextensioncred_modifiedonbehalfby";
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
				public const string LkMsdynWarehouseCreatedby = "lk_msdyn_warehouse_createdby";
				public const string LkMsdynWarehouseCreatedonbehalfby = "lk_msdyn_warehouse_createdonbehalfby";
				public const string LkMsdynWarehouseModifiedby = "lk_msdyn_warehouse_modifiedby";
				public const string LkMsdynWarehouseModifiedonbehalfby = "lk_msdyn_warehouse_modifiedonbehalfby";
				public const string LkMsdynWarrantyCreatedby = "lk_msdyn_warranty_createdby";
				public const string LkMsdynWarrantyCreatedonbehalfby = "lk_msdyn_warranty_createdonbehalfby";
				public const string LkMsdynWarrantyModifiedby = "lk_msdyn_warranty_modifiedby";
				public const string LkMsdynWarrantyModifiedonbehalfby = "lk_msdyn_warranty_modifiedonbehalfby";
				public const string LkMsdynWechatengagementctxCreatedby = "lk_msdyn_wechatengagementctx_createdby";
				public const string LkMsdynWechatengagementctxCreatedonbehalfby = "lk_msdyn_wechatengagementctx_createdonbehalfby";
				public const string LkMsdynWechatengagementctxModifiedby = "lk_msdyn_wechatengagementctx_modifiedby";
				public const string LkMsdynWechatengagementctxModifiedonbehalfby = "lk_msdyn_wechatengagementctx_modifiedonbehalfby";
				public const string LkMsdynWhatsappengagementctxCreatedby = "lk_msdyn_whatsappengagementctx_createdby";
				public const string LkMsdynWhatsappengagementctxCreatedonbehalfby = "lk_msdyn_whatsappengagementctx_createdonbehalfby";
				public const string LkMsdynWhatsappengagementctxModifiedby = "lk_msdyn_whatsappengagementctx_modifiedby";
				public const string LkMsdynWhatsappengagementctxModifiedonbehalfby = "lk_msdyn_whatsappengagementctx_modifiedonbehalfby";
				public const string LkMsdynWkwcolleaguesforcompanyCreatedby = "lk_msdyn_wkwcolleaguesforcompany_createdby";
				public const string LkMsdynWkwcolleaguesforcompanyCreatedonbehalfby = "lk_msdyn_wkwcolleaguesforcompany_createdonbehalfby";
				public const string LkMsdynWkwcolleaguesforcompanyModifiedby = "lk_msdyn_wkwcolleaguesforcompany_modifiedby";
				public const string LkMsdynWkwcolleaguesforcompanyModifiedonbehalfby = "lk_msdyn_wkwcolleaguesforcompany_modifiedonbehalfby";
				public const string LkMsdynWkwcolleaguesforcontactCreatedby = "lk_msdyn_wkwcolleaguesforcontact_createdby";
				public const string LkMsdynWkwcolleaguesforcontactCreatedonbehalfby = "lk_msdyn_wkwcolleaguesforcontact_createdonbehalfby";
				public const string LkMsdynWkwcolleaguesforcontactModifiedby = "lk_msdyn_wkwcolleaguesforcontact_modifiedby";
				public const string LkMsdynWkwcolleaguesforcontactModifiedonbehalfby = "lk_msdyn_wkwcolleaguesforcontact_modifiedonbehalfby";
				public const string LkMsdynWkwconfigCreatedby = "lk_msdyn_wkwconfig_createdby";
				public const string LkMsdynWkwconfigCreatedonbehalfby = "lk_msdyn_wkwconfig_createdonbehalfby";
				public const string LkMsdynWkwconfigModifiedby = "lk_msdyn_wkwconfig_modifiedby";
				public const string LkMsdynWkwconfigModifiedonbehalfby = "lk_msdyn_wkwconfig_modifiedonbehalfby";
				public const string LkMsdynWorkflowactionstatusCreatedby = "lk_msdyn_workflowactionstatus_createdby";
				public const string LkMsdynWorkflowactionstatusCreatedonbehalfby = "lk_msdyn_workflowactionstatus_createdonbehalfby";
				public const string LkMsdynWorkflowactionstatusModifiedby = "lk_msdyn_workflowactionstatus_modifiedby";
				public const string LkMsdynWorkflowactionstatusModifiedonbehalfby = "lk_msdyn_workflowactionstatus_modifiedonbehalfby";
				public const string LkMsdynWorkhourtemplateCreatedby = "lk_msdyn_workhourtemplate_createdby";
				public const string LkMsdynWorkhourtemplateCreatedonbehalfby = "lk_msdyn_workhourtemplate_createdonbehalfby";
				public const string LkMsdynWorkhourtemplateModifiedby = "lk_msdyn_workhourtemplate_modifiedby";
				public const string LkMsdynWorkhourtemplateModifiedonbehalfby = "lk_msdyn_workhourtemplate_modifiedonbehalfby";
				public const string LkMsdynWorklistviewconfigurationCreatedby = "lk_msdyn_worklistviewconfiguration_createdby";
				public const string LkMsdynWorklistviewconfigurationCreatedonbehalfby = "lk_msdyn_worklistviewconfiguration_createdonbehalfby";
				public const string LkMsdynWorklistviewconfigurationModifiedby = "lk_msdyn_worklistviewconfiguration_modifiedby";
				public const string LkMsdynWorklistviewconfigurationModifiedonbehalfby = "lk_msdyn_worklistviewconfiguration_modifiedonbehalfby";
				public const string LkMsdynWorkorderCreatedby = "lk_msdyn_workorder_createdby";
				public const string LkMsdynWorkorderCreatedonbehalfby = "lk_msdyn_workorder_createdonbehalfby";
				public const string LkMsdynWorkorderModifiedby = "lk_msdyn_workorder_modifiedby";
				public const string LkMsdynWorkorderModifiedonbehalfby = "lk_msdyn_workorder_modifiedonbehalfby";
				public const string LkMsdynWorkordercharacteristicCreatedby = "lk_msdyn_workordercharacteristic_createdby";
				public const string LkMsdynWorkordercharacteristicCreatedonbehalfby = "lk_msdyn_workordercharacteristic_createdonbehalfby";
				public const string LkMsdynWorkordercharacteristicModifiedby = "lk_msdyn_workordercharacteristic_modifiedby";
				public const string LkMsdynWorkordercharacteristicModifiedonbehalfby = "lk_msdyn_workordercharacteristic_modifiedonbehalfby";
				public const string LkMsdynWorkorderdetailsgenerationqueueCreatedby = "lk_msdyn_workorderdetailsgenerationqueue_createdby";
				public const string LkMsdynWorkorderdetailsgenerationqueueCreatedonbehalfby = "lk_msdyn_workorderdetailsgenerationqueue_createdonbehalfby";
				public const string LkMsdynWorkorderdetailsgenerationqueueModifiedby = "lk_msdyn_workorderdetailsgenerationqueue_modifiedby";
				public const string LkMsdynWorkorderdetailsgenerationqueueModifiedonbehalfby = "lk_msdyn_workorderdetailsgenerationqueue_modifiedonbehalfby";
				public const string LkMsdynWorkorderincidentCreatedby = "lk_msdyn_workorderincident_createdby";
				public const string LkMsdynWorkorderincidentCreatedonbehalfby = "lk_msdyn_workorderincident_createdonbehalfby";
				public const string LkMsdynWorkorderincidentModifiedby = "lk_msdyn_workorderincident_modifiedby";
				public const string LkMsdynWorkorderincidentModifiedonbehalfby = "lk_msdyn_workorderincident_modifiedonbehalfby";
				public const string LkMsdynWorkordernteCreatedby = "lk_msdyn_workordernte_createdby";
				public const string LkMsdynWorkordernteCreatedonbehalfby = "lk_msdyn_workordernte_createdonbehalfby";
				public const string LkMsdynWorkordernteModifiedby = "lk_msdyn_workordernte_modifiedby";
				public const string LkMsdynWorkordernteModifiedonbehalfby = "lk_msdyn_workordernte_modifiedonbehalfby";
				public const string LkMsdynWorkorderproductCreatedby = "lk_msdyn_workorderproduct_createdby";
				public const string LkMsdynWorkorderproductCreatedonbehalfby = "lk_msdyn_workorderproduct_createdonbehalfby";
				public const string LkMsdynWorkorderproductModifiedby = "lk_msdyn_workorderproduct_modifiedby";
				public const string LkMsdynWorkorderproductModifiedonbehalfby = "lk_msdyn_workorderproduct_modifiedonbehalfby";
				public const string LkMsdynWorkorderresolutionCreatedby = "lk_msdyn_workorderresolution_createdby";
				public const string LkMsdynWorkorderresolutionCreatedonbehalfby = "lk_msdyn_workorderresolution_createdonbehalfby";
				public const string LkMsdynWorkorderresolutionModifiedby = "lk_msdyn_workorderresolution_modifiedby";
				public const string LkMsdynWorkorderresolutionModifiedonbehalfby = "lk_msdyn_workorderresolution_modifiedonbehalfby";
				public const string LkMsdynWorkorderresourcerestrictionCreatedby = "lk_msdyn_workorderresourcerestriction_createdby";
				public const string LkMsdynWorkorderresourcerestrictionCreatedonbehalfby = "lk_msdyn_workorderresourcerestriction_createdonbehalfby";
				public const string LkMsdynWorkorderresourcerestrictionModifiedby = "lk_msdyn_workorderresourcerestriction_modifiedby";
				public const string LkMsdynWorkorderresourcerestrictionModifiedonbehalfby = "lk_msdyn_workorderresourcerestriction_modifiedonbehalfby";
				public const string LkMsdynWorkorderserviceCreatedby = "lk_msdyn_workorderservice_createdby";
				public const string LkMsdynWorkorderserviceCreatedonbehalfby = "lk_msdyn_workorderservice_createdonbehalfby";
				public const string LkMsdynWorkorderserviceModifiedby = "lk_msdyn_workorderservice_modifiedby";
				public const string LkMsdynWorkorderserviceModifiedonbehalfby = "lk_msdyn_workorderservice_modifiedonbehalfby";
				public const string LkMsdynWorkorderservicetaskCreatedby = "lk_msdyn_workorderservicetask_createdby";
				public const string LkMsdynWorkorderservicetaskCreatedonbehalfby = "lk_msdyn_workorderservicetask_createdonbehalfby";
				public const string LkMsdynWorkorderservicetaskModifiedby = "lk_msdyn_workorderservicetask_modifiedby";
				public const string LkMsdynWorkorderservicetaskModifiedonbehalfby = "lk_msdyn_workorderservicetask_modifiedonbehalfby";
				public const string LkMsdynWorkordersubstatusCreatedby = "lk_msdyn_workordersubstatus_createdby";
				public const string LkMsdynWorkordersubstatusCreatedonbehalfby = "lk_msdyn_workordersubstatus_createdonbehalfby";
				public const string LkMsdynWorkordersubstatusModifiedby = "lk_msdyn_workordersubstatus_modifiedby";
				public const string LkMsdynWorkordersubstatusModifiedonbehalfby = "lk_msdyn_workordersubstatus_modifiedonbehalfby";
				public const string LkMsdynWorkordertypeCreatedby = "lk_msdyn_workordertype_createdby";
				public const string LkMsdynWorkordertypeCreatedonbehalfby = "lk_msdyn_workordertype_createdonbehalfby";
				public const string LkMsdynWorkordertypeModifiedby = "lk_msdyn_workordertype_modifiedby";
				public const string LkMsdynWorkordertypeModifiedonbehalfby = "lk_msdyn_workordertype_modifiedonbehalfby";
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
				public const string LkMsdynmktByoacschannelinstanceCreatedby = "lk_msdynmkt_byoacschannelinstance_createdby";
				public const string LkMsdynmktByoacschannelinstanceCreatedonbehalfby = "lk_msdynmkt_byoacschannelinstance_createdonbehalfby";
				public const string LkMsdynmktByoacschannelinstanceModifiedby = "lk_msdynmkt_byoacschannelinstance_modifiedby";
				public const string LkMsdynmktByoacschannelinstanceModifiedonbehalfby = "lk_msdynmkt_byoacschannelinstance_modifiedonbehalfby";
				public const string LkMsdynmktByoacschannelinstanceaccountCreatedby = "lk_msdynmkt_byoacschannelinstanceaccount_createdby";
				public const string LkMsdynmktByoacschannelinstanceaccountCreatedonbehalfby = "lk_msdynmkt_byoacschannelinstanceaccount_createdonbehalfby";
				public const string LkMsdynmktByoacschannelinstanceaccountModifiedby = "lk_msdynmkt_byoacschannelinstanceaccount_modifiedby";
				public const string LkMsdynmktByoacschannelinstanceaccountModifiedonbehalfby = "lk_msdynmkt_byoacschannelinstanceaccount_modifiedonbehalfby";
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
				public const string LkMsdynmktExperimentv2Createdby = "lk_msdynmkt_experimentv2_createdby";
				public const string LkMsdynmktExperimentv2Createdonbehalfby = "lk_msdynmkt_experimentv2_createdonbehalfby";
				public const string LkMsdynmktExperimentv2Modifiedby = "lk_msdynmkt_experimentv2_modifiedby";
				public const string LkMsdynmktExperimentv2Modifiedonbehalfby = "lk_msdynmkt_experimentv2_modifiedonbehalfby";
				public const string LkMsdynmktFeatureconfigurationCreatedby = "lk_msdynmkt_featureconfiguration_createdby";
				public const string LkMsdynmktFeatureconfigurationCreatedonbehalfby = "lk_msdynmkt_featureconfiguration_createdonbehalfby";
				public const string LkMsdynmktFeatureconfigurationModifiedby = "lk_msdynmkt_featureconfiguration_modifiedby";
				public const string LkMsdynmktFeatureconfigurationModifiedonbehalfby = "lk_msdynmkt_featureconfiguration_modifiedonbehalfby";
				public const string LkMsdynmktInfobipchannelinstanceCreatedby = "lk_msdynmkt_infobipchannelinstance_createdby";
				public const string LkMsdynmktInfobipchannelinstanceCreatedonbehalfby = "lk_msdynmkt_infobipchannelinstance_createdonbehalfby";
				public const string LkMsdynmktInfobipchannelinstanceModifiedby = "lk_msdynmkt_infobipchannelinstance_modifiedby";
				public const string LkMsdynmktInfobipchannelinstanceModifiedonbehalfby = "lk_msdynmkt_infobipchannelinstance_modifiedonbehalfby";
				public const string LkMsdynmktInfobipchannelinstanceaccountCreatedby = "lk_msdynmkt_infobipchannelinstanceaccount_createdby";
				public const string LkMsdynmktInfobipchannelinstanceaccountCreatedonbehalfby = "lk_msdynmkt_infobipchannelinstanceaccount_createdonbehalfby";
				public const string LkMsdynmktInfobipchannelinstanceaccountModifiedby = "lk_msdynmkt_infobipchannelinstanceaccount_modifiedby";
				public const string LkMsdynmktInfobipchannelinstanceaccountModifiedonbehalfby = "lk_msdynmkt_infobipchannelinstanceaccount_modifiedonbehalfby";
				public const string LkMsdynmktLinkmobilitychannelinstanceCreatedby = "lk_msdynmkt_linkmobilitychannelinstance_createdby";
				public const string LkMsdynmktLinkmobilitychannelinstanceCreatedonbehalfby = "lk_msdynmkt_linkmobilitychannelinstance_createdonbehalfby";
				public const string LkMsdynmktLinkmobilitychannelinstanceModifiedby = "lk_msdynmkt_linkmobilitychannelinstance_modifiedby";
				public const string LkMsdynmktLinkmobilitychannelinstanceModifiedonbehalfby = "lk_msdynmkt_linkmobilitychannelinstance_modifiedonbehalfby";
				public const string LkMsdynmktLinkmobilitychannelinstanceaccountCreatedby = "lk_msdynmkt_linkmobilitychannelinstanceaccount_createdby";
				public const string LkMsdynmktLinkmobilitychannelinstanceaccountCreatedonbehalfby = "lk_msdynmkt_linkmobilitychannelinstanceaccount_createdonbehalfby";
				public const string LkMsdynmktLinkmobilitychannelinstanceaccountModifiedby = "lk_msdynmkt_linkmobilitychannelinstanceaccount_modifiedby";
				public const string LkMsdynmktLinkmobilitychannelinstanceaccountModifiedonbehalfby = "lk_msdynmkt_linkmobilitychannelinstanceaccount_modifiedonbehalfby";
				public const string LkMsdynmktMetadataentityrelationshipCreatedby = "lk_msdynmkt_metadataentityrelationship_createdby";
				public const string LkMsdynmktMetadataentityrelationshipCreatedonbehalfby = "lk_msdynmkt_metadataentityrelationship_createdonbehalfby";
				public const string LkMsdynmktMetadataentityrelationshipModifiedby = "lk_msdynmkt_metadataentityrelationship_modifiedby";
				public const string LkMsdynmktMetadataentityrelationshipModifiedonbehalfby = "lk_msdynmkt_metadataentityrelationship_modifiedonbehalfby";
				public const string LkMsdynmktMetadataitemCreatedby = "lk_msdynmkt_metadataitem_createdby";
				public const string LkMsdynmktMetadataitemCreatedonbehalfby = "lk_msdynmkt_metadataitem_createdonbehalfby";
				public const string LkMsdynmktMetadataitemModifiedby = "lk_msdynmkt_metadataitem_modifiedby";
				public const string LkMsdynmktMetadataitemModifiedonbehalfby = "lk_msdynmkt_metadataitem_modifiedonbehalfby";
				public const string LkMsdynmktMetadatastorestateCreatedby = "lk_msdynmkt_metadatastorestate_createdby";
				public const string LkMsdynmktMetadatastorestateCreatedonbehalfby = "lk_msdynmkt_metadatastorestate_createdonbehalfby";
				public const string LkMsdynmktMetadatastorestateModifiedby = "lk_msdynmkt_metadatastorestate_modifiedby";
				public const string LkMsdynmktMetadatastorestateModifiedonbehalfby = "lk_msdynmkt_metadatastorestate_modifiedonbehalfby";
				public const string LkMsdynmktMocksmsproviderchannelinstanceCreatedby = "lk_msdynmkt_mocksmsproviderchannelinstance_createdby";
				public const string LkMsdynmktMocksmsproviderchannelinstanceCreatedonbehalfby = "lk_msdynmkt_mocksmsproviderchannelinstance_createdonbehalfby";
				public const string LkMsdynmktMocksmsproviderchannelinstanceModifiedby = "lk_msdynmkt_mocksmsproviderchannelinstance_modifiedby";
				public const string LkMsdynmktMocksmsproviderchannelinstanceModifiedonbehalfby = "lk_msdynmkt_mocksmsproviderchannelinstance_modifiedonbehalfby";
				public const string LkMsdynmktMocksmsproviderchannelinstanceaccountCreatedby = "lk_msdynmkt_mocksmsproviderchannelinstanceaccount_createdby";
				public const string LkMsdynmktMocksmsproviderchannelinstanceaccountCreatedonbehalfby = "lk_msdynmkt_mocksmsproviderchannelinstanceaccount_createdonbehalfby";
				public const string LkMsdynmktMocksmsproviderchannelinstanceaccountModifiedby = "lk_msdynmkt_mocksmsproviderchannelinstanceaccount_modifiedby";
				public const string LkMsdynmktMocksmsproviderchannelinstanceaccountModifiedonbehalfby = "lk_msdynmkt_mocksmsproviderchannelinstanceaccount_modifiedonbehalfby";
				public const string LkMsdynmktPredefinedplaceholderCreatedby = "lk_msdynmkt_predefinedplaceholder_createdby";
				public const string LkMsdynmktPredefinedplaceholderCreatedonbehalfby = "lk_msdynmkt_predefinedplaceholder_createdonbehalfby";
				public const string LkMsdynmktPredefinedplaceholderModifiedby = "lk_msdynmkt_predefinedplaceholder_modifiedby";
				public const string LkMsdynmktPredefinedplaceholderModifiedonbehalfby = "lk_msdynmkt_predefinedplaceholder_modifiedonbehalfby";
				public const string LkMsdynmktTelesignchannelinstanceCreatedby = "lk_msdynmkt_telesignchannelinstance_createdby";
				public const string LkMsdynmktTelesignchannelinstanceCreatedonbehalfby = "lk_msdynmkt_telesignchannelinstance_createdonbehalfby";
				public const string LkMsdynmktTelesignchannelinstanceModifiedby = "lk_msdynmkt_telesignchannelinstance_modifiedby";
				public const string LkMsdynmktTelesignchannelinstanceModifiedonbehalfby = "lk_msdynmkt_telesignchannelinstance_modifiedonbehalfby";
				public const string LkMsdynmktTelesignchannelinstanceaccountCreatedby = "lk_msdynmkt_telesignchannelinstanceaccount_createdby";
				public const string LkMsdynmktTelesignchannelinstanceaccountCreatedonbehalfby = "lk_msdynmkt_telesignchannelinstanceaccount_createdonbehalfby";
				public const string LkMsdynmktTelesignchannelinstanceaccountModifiedby = "lk_msdynmkt_telesignchannelinstanceaccount_modifiedby";
				public const string LkMsdynmktTelesignchannelinstanceaccountModifiedonbehalfby = "lk_msdynmkt_telesignchannelinstanceaccount_modifiedonbehalfby";
				public const string LkMsdynmktTrackingcontextCreatedby = "lk_msdynmkt_trackingcontext_createdby";
				public const string LkMsdynmktTrackingcontextCreatedonbehalfby = "lk_msdynmkt_trackingcontext_createdonbehalfby";
				public const string LkMsdynmktTrackingcontextModifiedby = "lk_msdynmkt_trackingcontext_modifiedby";
				public const string LkMsdynmktTrackingcontextModifiedonbehalfby = "lk_msdynmkt_trackingcontext_modifiedonbehalfby";
				public const string LkMsdynmktTwiliochannelinstanceCreatedby = "lk_msdynmkt_twiliochannelinstance_createdby";
				public const string LkMsdynmktTwiliochannelinstanceCreatedonbehalfby = "lk_msdynmkt_twiliochannelinstance_createdonbehalfby";
				public const string LkMsdynmktTwiliochannelinstanceModifiedby = "lk_msdynmkt_twiliochannelinstance_modifiedby";
				public const string LkMsdynmktTwiliochannelinstanceModifiedonbehalfby = "lk_msdynmkt_twiliochannelinstance_modifiedonbehalfby";
				public const string LkMsdynmktTwiliochannelinstanceaccountCreatedby = "lk_msdynmkt_twiliochannelinstanceaccount_createdby";
				public const string LkMsdynmktTwiliochannelinstanceaccountCreatedonbehalfby = "lk_msdynmkt_twiliochannelinstanceaccount_createdonbehalfby";
				public const string LkMsdynmktTwiliochannelinstanceaccountModifiedby = "lk_msdynmkt_twiliochannelinstanceaccount_modifiedby";
				public const string LkMsdynmktTwiliochannelinstanceaccountModifiedonbehalfby = "lk_msdynmkt_twiliochannelinstanceaccount_modifiedonbehalfby";
				public const string LkMsdynmktVibeschannelinstanceCreatedby = "lk_msdynmkt_vibeschannelinstance_createdby";
				public const string LkMsdynmktVibeschannelinstanceCreatedonbehalfby = "lk_msdynmkt_vibeschannelinstance_createdonbehalfby";
				public const string LkMsdynmktVibeschannelinstanceModifiedby = "lk_msdynmkt_vibeschannelinstance_modifiedby";
				public const string LkMsdynmktVibeschannelinstanceModifiedonbehalfby = "lk_msdynmkt_vibeschannelinstance_modifiedonbehalfby";
				public const string LkMsdynmktVibeschannelinstanceaccountCreatedby = "lk_msdynmkt_vibeschannelinstanceaccount_createdby";
				public const string LkMsdynmktVibeschannelinstanceaccountCreatedonbehalfby = "lk_msdynmkt_vibeschannelinstanceaccount_createdonbehalfby";
				public const string LkMsdynmktVibeschannelinstanceaccountModifiedby = "lk_msdynmkt_vibeschannelinstanceaccount_modifiedby";
				public const string LkMsdynmktVibeschannelinstanceaccountModifiedonbehalfby = "lk_msdynmkt_vibeschannelinstanceaccount_modifiedonbehalfby";
				public const string LkMsdyusdActioncallworkflowCreatedby = "lk_msdyusd_actioncallworkflow_createdby";
				public const string LkMsdyusdActioncallworkflowCreatedonbehalfby = "lk_msdyusd_actioncallworkflow_createdonbehalfby";
				public const string LkMsdyusdActioncallworkflowModifiedby = "lk_msdyusd_actioncallworkflow_modifiedby";
				public const string LkMsdyusdActioncallworkflowModifiedonbehalfby = "lk_msdyusd_actioncallworkflow_modifiedonbehalfby";
				public const string LkMsdyusdAgentscriptactionCreatedby = "lk_msdyusd_agentscriptaction_createdby";
				public const string LkMsdyusdAgentscriptactionCreatedonbehalfby = "lk_msdyusd_agentscriptaction_createdonbehalfby";
				public const string LkMsdyusdAgentscriptactionModifiedby = "lk_msdyusd_agentscriptaction_modifiedby";
				public const string LkMsdyusdAgentscriptactionModifiedonbehalfby = "lk_msdyusd_agentscriptaction_modifiedonbehalfby";
				public const string LkMsdyusdAgentscripttaskcategoryCreatedby = "lk_msdyusd_agentscripttaskcategory_createdby";
				public const string LkMsdyusdAgentscripttaskcategoryCreatedonbehalfby = "lk_msdyusd_agentscripttaskcategory_createdonbehalfby";
				public const string LkMsdyusdAgentscripttaskcategoryModifiedby = "lk_msdyusd_agentscripttaskcategory_modifiedby";
				public const string LkMsdyusdAgentscripttaskcategoryModifiedonbehalfby = "lk_msdyusd_agentscripttaskcategory_modifiedonbehalfby";
				public const string LkMsdyusdAnswerCreatedby = "lk_msdyusd_answer_createdby";
				public const string LkMsdyusdAnswerCreatedonbehalfby = "lk_msdyusd_answer_createdonbehalfby";
				public const string LkMsdyusdAnswerModifiedby = "lk_msdyusd_answer_modifiedby";
				public const string LkMsdyusdAnswerModifiedonbehalfby = "lk_msdyusd_answer_modifiedonbehalfby";
				public const string LkMsdyusdAuditanddiagnosticssettingCreatedby = "lk_msdyusd_auditanddiagnosticssetting_createdby";
				public const string LkMsdyusdAuditanddiagnosticssettingCreatedonbehalfby = "lk_msdyusd_auditanddiagnosticssetting_createdonbehalfby";
				public const string LkMsdyusdAuditanddiagnosticssettingModifiedby = "lk_msdyusd_auditanddiagnosticssetting_modifiedby";
				public const string LkMsdyusdAuditanddiagnosticssettingModifiedonbehalfby = "lk_msdyusd_auditanddiagnosticssetting_modifiedonbehalfby";
				public const string LkMsdyusdConfigurationCreatedby = "lk_msdyusd_configuration_createdby";
				public const string LkMsdyusdConfigurationCreatedonbehalfby = "lk_msdyusd_configuration_createdonbehalfby";
				public const string LkMsdyusdConfigurationModifiedby = "lk_msdyusd_configuration_modifiedby";
				public const string LkMsdyusdConfigurationModifiedonbehalfby = "lk_msdyusd_configuration_modifiedonbehalfby";
				public const string LkMsdyusdCustomizationfilesCreatedby = "lk_msdyusd_customizationfiles_createdby";
				public const string LkMsdyusdCustomizationfilesCreatedonbehalfby = "lk_msdyusd_customizationfiles_createdonbehalfby";
				public const string LkMsdyusdCustomizationfilesModifiedby = "lk_msdyusd_customizationfiles_modifiedby";
				public const string LkMsdyusdCustomizationfilesModifiedonbehalfby = "lk_msdyusd_customizationfiles_modifiedonbehalfby";
				public const string LkMsdyusdEntityassignmentCreatedby = "lk_msdyusd_entityassignment_createdby";
				public const string LkMsdyusdEntityassignmentCreatedonbehalfby = "lk_msdyusd_entityassignment_createdonbehalfby";
				public const string LkMsdyusdEntityassignmentModifiedby = "lk_msdyusd_entityassignment_modifiedby";
				public const string LkMsdyusdEntityassignmentModifiedonbehalfby = "lk_msdyusd_entityassignment_modifiedonbehalfby";
				public const string LkMsdyusdEntitysearchCreatedby = "lk_msdyusd_entitysearch_createdby";
				public const string LkMsdyusdEntitysearchCreatedonbehalfby = "lk_msdyusd_entitysearch_createdonbehalfby";
				public const string LkMsdyusdEntitysearchModifiedby = "lk_msdyusd_entitysearch_modifiedby";
				public const string LkMsdyusdEntitysearchModifiedonbehalfby = "lk_msdyusd_entitysearch_modifiedonbehalfby";
				public const string LkMsdyusdFormCreatedby = "lk_msdyusd_form_createdby";
				public const string LkMsdyusdFormCreatedonbehalfby = "lk_msdyusd_form_createdonbehalfby";
				public const string LkMsdyusdFormModifiedby = "lk_msdyusd_form_modifiedby";
				public const string LkMsdyusdFormModifiedonbehalfby = "lk_msdyusd_form_modifiedonbehalfby";
				public const string LkMsdyusdLanguagemoduleCreatedby = "lk_msdyusd_languagemodule_createdby";
				public const string LkMsdyusdLanguagemoduleCreatedonbehalfby = "lk_msdyusd_languagemodule_createdonbehalfby";
				public const string LkMsdyusdLanguagemoduleModifiedby = "lk_msdyusd_languagemodule_modifiedby";
				public const string LkMsdyusdLanguagemoduleModifiedonbehalfby = "lk_msdyusd_languagemodule_modifiedonbehalfby";
				public const string LkMsdyusdScriptletCreatedby = "lk_msdyusd_scriptlet_createdby";
				public const string LkMsdyusdScriptletCreatedonbehalfby = "lk_msdyusd_scriptlet_createdonbehalfby";
				public const string LkMsdyusdScriptletModifiedby = "lk_msdyusd_scriptlet_modifiedby";
				public const string LkMsdyusdScriptletModifiedonbehalfby = "lk_msdyusd_scriptlet_modifiedonbehalfby";
				public const string LkMsdyusdScripttasktriggerCreatedby = "lk_msdyusd_scripttasktrigger_createdby";
				public const string LkMsdyusdScripttasktriggerCreatedonbehalfby = "lk_msdyusd_scripttasktrigger_createdonbehalfby";
				public const string LkMsdyusdScripttasktriggerModifiedby = "lk_msdyusd_scripttasktrigger_modifiedby";
				public const string LkMsdyusdScripttasktriggerModifiedonbehalfby = "lk_msdyusd_scripttasktrigger_modifiedonbehalfby";
				public const string LkMsdyusdSearchCreatedby = "lk_msdyusd_search_createdby";
				public const string LkMsdyusdSearchCreatedonbehalfby = "lk_msdyusd_search_createdonbehalfby";
				public const string LkMsdyusdSearchModifiedby = "lk_msdyusd_search_modifiedby";
				public const string LkMsdyusdSearchModifiedonbehalfby = "lk_msdyusd_search_modifiedonbehalfby";
				public const string LkMsdyusdSessioninformationCreatedby = "lk_msdyusd_sessioninformation_createdby";
				public const string LkMsdyusdSessioninformationCreatedonbehalfby = "lk_msdyusd_sessioninformation_createdonbehalfby";
				public const string LkMsdyusdSessioninformationModifiedby = "lk_msdyusd_sessioninformation_modifiedby";
				public const string LkMsdyusdSessioninformationModifiedonbehalfby = "lk_msdyusd_sessioninformation_modifiedonbehalfby";
				public const string LkMsdyusdSessiontransferCreatedby = "lk_msdyusd_sessiontransfer_createdby";
				public const string LkMsdyusdSessiontransferCreatedonbehalfby = "lk_msdyusd_sessiontransfer_createdonbehalfby";
				public const string LkMsdyusdSessiontransferModifiedby = "lk_msdyusd_sessiontransfer_modifiedby";
				public const string LkMsdyusdSessiontransferModifiedonbehalfby = "lk_msdyusd_sessiontransfer_modifiedonbehalfby";
				public const string LkMsdyusdTaskCreatedby = "lk_msdyusd_task_createdby";
				public const string LkMsdyusdTaskCreatedonbehalfby = "lk_msdyusd_task_createdonbehalfby";
				public const string LkMsdyusdTaskModifiedby = "lk_msdyusd_task_modifiedby";
				public const string LkMsdyusdTaskModifiedonbehalfby = "lk_msdyusd_task_modifiedonbehalfby";
				public const string LkMsdyusdToolbarbuttonCreatedby = "lk_msdyusd_toolbarbutton_createdby";
				public const string LkMsdyusdToolbarbuttonCreatedonbehalfby = "lk_msdyusd_toolbarbutton_createdonbehalfby";
				public const string LkMsdyusdToolbarbuttonModifiedby = "lk_msdyusd_toolbarbutton_modifiedby";
				public const string LkMsdyusdToolbarbuttonModifiedonbehalfby = "lk_msdyusd_toolbarbutton_modifiedonbehalfby";
				public const string LkMsdyusdToolbarstripCreatedby = "lk_msdyusd_toolbarstrip_createdby";
				public const string LkMsdyusdToolbarstripCreatedonbehalfby = "lk_msdyusd_toolbarstrip_createdonbehalfby";
				public const string LkMsdyusdToolbarstripModifiedby = "lk_msdyusd_toolbarstrip_modifiedby";
				public const string LkMsdyusdToolbarstripModifiedonbehalfby = "lk_msdyusd_toolbarstrip_modifiedonbehalfby";
				public const string LkMsdyusdTracesourcesettingCreatedby = "lk_msdyusd_tracesourcesetting_createdby";
				public const string LkMsdyusdTracesourcesettingCreatedonbehalfby = "lk_msdyusd_tracesourcesetting_createdonbehalfby";
				public const string LkMsdyusdTracesourcesettingModifiedby = "lk_msdyusd_tracesourcesetting_modifiedby";
				public const string LkMsdyusdTracesourcesettingModifiedonbehalfby = "lk_msdyusd_tracesourcesetting_modifiedonbehalfby";
				public const string LkMsdyusdUcisettingsCreatedby = "lk_msdyusd_ucisettings_createdby";
				public const string LkMsdyusdUcisettingsCreatedonbehalfby = "lk_msdyusd_ucisettings_createdonbehalfby";
				public const string LkMsdyusdUcisettingsModifiedby = "lk_msdyusd_ucisettings_modifiedby";
				public const string LkMsdyusdUcisettingsModifiedonbehalfby = "lk_msdyusd_ucisettings_modifiedonbehalfby";
				public const string LkMsdyusdUiieventCreatedby = "lk_msdyusd_uiievent_createdby";
				public const string LkMsdyusdUiieventCreatedonbehalfby = "lk_msdyusd_uiievent_createdonbehalfby";
				public const string LkMsdyusdUiieventModifiedby = "lk_msdyusd_uiievent_modifiedby";
				public const string LkMsdyusdUiieventModifiedonbehalfby = "lk_msdyusd_uiievent_modifiedonbehalfby";
				public const string LkMsdyusdUsersettingsCreatedby = "lk_msdyusd_usersettings_createdby";
				public const string LkMsdyusdUsersettingsCreatedonbehalfby = "lk_msdyusd_usersettings_createdonbehalfby";
				public const string LkMsdyusdUsersettingsModifiedby = "lk_msdyusd_usersettings_modifiedby";
				public const string LkMsdyusdUsersettingsModifiedonbehalfby = "lk_msdyusd_usersettings_modifiedonbehalfby";
				public const string LkMsdyusdWindowrouteCreatedby = "lk_msdyusd_windowroute_createdby";
				public const string LkMsdyusdWindowrouteCreatedonbehalfby = "lk_msdyusd_windowroute_createdonbehalfby";
				public const string LkMsdyusdWindowrouteModifiedby = "lk_msdyusd_windowroute_modifiedby";
				public const string LkMsdyusdWindowrouteModifiedonbehalfby = "lk_msdyusd_windowroute_modifiedonbehalfby";
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
				public const string LkPowerbidatasetapdxCreatedby = "lk_powerbidatasetapdx_createdby";
				public const string LkPowerbidatasetapdxCreatedonbehalfby = "lk_powerbidatasetapdx_createdonbehalfby";
				public const string LkPowerbidatasetapdxModifiedby = "lk_powerbidatasetapdx_modifiedby";
				public const string LkPowerbidatasetapdxModifiedonbehalfby = "lk_powerbidatasetapdx_modifiedonbehalfby";
				public const string LkPowerbimashupparameterCreatedby = "lk_powerbimashupparameter_createdby";
				public const string LkPowerbimashupparameterCreatedonbehalfby = "lk_powerbimashupparameter_createdonbehalfby";
				public const string LkPowerbimashupparameterModifiedby = "lk_powerbimashupparameter_modifiedby";
				public const string LkPowerbimashupparameterModifiedonbehalfby = "lk_powerbimashupparameter_modifiedonbehalfby";
				public const string LkPowerbireportCreatedby = "lk_powerbireport_createdby";
				public const string LkPowerbireportCreatedonbehalfby = "lk_powerbireport_createdonbehalfby";
				public const string LkPowerbireportModifiedby = "lk_powerbireport_modifiedby";
				public const string LkPowerbireportModifiedonbehalfby = "lk_powerbireport_modifiedonbehalfby";
				public const string LkPowerbireportapdxCreatedby = "lk_powerbireportapdx_createdby";
				public const string LkPowerbireportapdxCreatedonbehalfby = "lk_powerbireportapdx_createdonbehalfby";
				public const string LkPowerbireportapdxModifiedby = "lk_powerbireportapdx_modifiedby";
				public const string LkPowerbireportapdxModifiedonbehalfby = "lk_powerbireportapdx_modifiedonbehalfby";
				public const string LkPowerfxruleCreatedby = "lk_powerfxrule_createdby";
				public const string LkPowerfxruleCreatedonbehalfby = "lk_powerfxrule_createdonbehalfby";
				public const string LkPowerfxruleModifiedby = "lk_powerfxrule_modifiedby";
				public const string LkPowerfxruleModifiedonbehalfby = "lk_powerfxrule_modifiedonbehalfby";
				public const string LkPowerpagecomponentCreatedby = "lk_powerpagecomponent_createdby";
				public const string LkPowerpagecomponentCreatedonbehalfby = "lk_powerpagecomponent_createdonbehalfby";
				public const string LkPowerpagecomponentModifiedby = "lk_powerpagecomponent_modifiedby";
				public const string LkPowerpagecomponentModifiedonbehalfby = "lk_powerpagecomponent_modifiedonbehalfby";
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
				public const string LkPowerpagesscanreportCreatedby = "lk_powerpagesscanreport_createdby";
				public const string LkPowerpagesscanreportCreatedonbehalfby = "lk_powerpagesscanreport_createdonbehalfby";
				public const string LkPowerpagesscanreportModifiedby = "lk_powerpagesscanreport_modifiedby";
				public const string LkPowerpagesscanreportModifiedonbehalfby = "lk_powerpagesscanreport_modifiedonbehalfby";
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
				public const string LkReportvisibilityCreatedonbehalfby = "lk_reportvisibility_createdonbehalfby";
				public const string LkReportvisibilityModifiedonbehalfby = "lk_reportvisibility_modifiedonbehalfby";
				public const string LkReportvisibilitybaseCreatedby = "lk_reportvisibilitybase_createdby";
				public const string LkReportvisibilitybaseModifiedby = "lk_reportvisibilitybase_modifiedby";
				public const string LkResourcespecCreatedby = "lk_resourcespec_createdby";
				public const string LkResourcespecCreatedonbehalfby = "lk_resourcespec_createdonbehalfby";
				public const string LkResourcespecModifiedby = "lk_resourcespec_modifiedby";
				public const string LkResourcespecModifiedonbehalfby = "lk_resourcespec_modifiedonbehalfby";
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
				public const string LkSharedworkspaceaccesstokenCreatedby = "lk_sharedworkspaceaccesstoken_createdby";
				public const string LkSharedworkspaceaccesstokenCreatedonbehalfby = "lk_sharedworkspaceaccesstoken_createdonbehalfby";
				public const string LkSharedworkspaceaccesstokenModifiedby = "lk_sharedworkspaceaccesstoken_modifiedby";
				public const string LkSharedworkspaceaccesstokenModifiedonbehalfby = "lk_sharedworkspaceaccesstoken_modifiedonbehalfby";
				public const string LkSharedworkspacenrCreatedby = "lk_sharedworkspacenr_createdby";
				public const string LkSharedworkspacenrCreatedonbehalfby = "lk_sharedworkspacenr_createdonbehalfby";
				public const string LkSharedworkspacenrModifiedby = "lk_sharedworkspacenr_modifiedby";
				public const string LkSharedworkspacenrModifiedonbehalfby = "lk_sharedworkspacenr_modifiedonbehalfby";
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
				public const string LkSharepointsitebaseCreatedby = "lk_sharepointsitebase_createdby";
				public const string LkSharepointsitebaseCreatedonbehalfby = "lk_sharepointsitebase_createdonbehalfby";
				public const string LkSharepointsitebaseModifiedby = "lk_sharepointsitebase_modifiedby";
				public const string LkSharepointsitebaseModifiedonbehalfby = "lk_sharepointsitebase_modifiedonbehalfby";
				public const string LkSideloadedaipluginCreatedby = "lk_sideloadedaiplugin_createdby";
				public const string LkSideloadedaipluginCreatedonbehalfby = "lk_sideloadedaiplugin_createdonbehalfby";
				public const string LkSideloadedaipluginModifiedby = "lk_sideloadedaiplugin_modifiedby";
				public const string LkSideloadedaipluginModifiedonbehalfby = "lk_sideloadedaiplugin_modifiedonbehalfby";
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
				public const string LkStagedentityCreatedby = "lk_stagedentity_createdby";
				public const string LkStagedentityCreatedonbehalfby = "lk_stagedentity_createdonbehalfby";
				public const string LkStagedentityModifiedby = "lk_stagedentity_modifiedby";
				public const string LkStagedentityModifiedonbehalfby = "lk_stagedentity_modifiedonbehalfby";
				public const string LkStagedentityattributeCreatedby = "lk_stagedentityattribute_createdby";
				public const string LkStagedentityattributeCreatedonbehalfby = "lk_stagedentityattribute_createdonbehalfby";
				public const string LkStagedentityattributeModifiedby = "lk_stagedentityattribute_modifiedby";
				public const string LkStagedentityattributeModifiedonbehalfby = "lk_stagedentityattribute_modifiedonbehalfby";
				public const string LkStagedmetadataasyncoperationCreatedby = "lk_stagedmetadataasyncoperation_createdby";
				public const string LkStagedmetadataasyncoperationCreatedonbehalfby = "lk_stagedmetadataasyncoperation_createdonbehalfby";
				public const string LkStagedmetadataasyncoperationModifiedby = "lk_stagedmetadataasyncoperation_modifiedby";
				public const string LkStagedmetadataasyncoperationModifiedonbehalfby = "lk_stagedmetadataasyncoperation_modifiedonbehalfby";
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
				public const string LkUiiActionCreatedby = "lk_uii_action_createdby";
				public const string LkUiiActionCreatedonbehalfby = "lk_uii_action_createdonbehalfby";
				public const string LkUiiActionModifiedby = "lk_uii_action_modifiedby";
				public const string LkUiiActionModifiedonbehalfby = "lk_uii_action_modifiedonbehalfby";
				public const string LkUiiAuditCreatedby = "lk_uii_audit_createdby";
				public const string LkUiiAuditCreatedonbehalfby = "lk_uii_audit_createdonbehalfby";
				public const string LkUiiAuditModifiedby = "lk_uii_audit_modifiedby";
				public const string LkUiiAuditModifiedonbehalfby = "lk_uii_audit_modifiedonbehalfby";
				public const string LkUiiContextCreatedby = "lk_uii_context_createdby";
				public const string LkUiiContextCreatedonbehalfby = "lk_uii_context_createdonbehalfby";
				public const string LkUiiContextModifiedby = "lk_uii_context_modifiedby";
				public const string LkUiiContextModifiedonbehalfby = "lk_uii_context_modifiedonbehalfby";
				public const string LkUiiHostedapplicationCreatedby = "lk_uii_hostedapplication_createdby";
				public const string LkUiiHostedapplicationCreatedonbehalfby = "lk_uii_hostedapplication_createdonbehalfby";
				public const string LkUiiHostedapplicationModifiedby = "lk_uii_hostedapplication_modifiedby";
				public const string LkUiiHostedapplicationModifiedonbehalfby = "lk_uii_hostedapplication_modifiedonbehalfby";
				public const string LkUiiNonhostedapplicationCreatedby = "lk_uii_nonhostedapplication_createdby";
				public const string LkUiiNonhostedapplicationCreatedonbehalfby = "lk_uii_nonhostedapplication_createdonbehalfby";
				public const string LkUiiNonhostedapplicationModifiedby = "lk_uii_nonhostedapplication_modifiedby";
				public const string LkUiiNonhostedapplicationModifiedonbehalfby = "lk_uii_nonhostedapplication_modifiedonbehalfby";
				public const string LkUiiOptionCreatedby = "lk_uii_option_createdby";
				public const string LkUiiOptionCreatedonbehalfby = "lk_uii_option_createdonbehalfby";
				public const string LkUiiOptionModifiedby = "lk_uii_option_modifiedby";
				public const string LkUiiOptionModifiedonbehalfby = "lk_uii_option_modifiedonbehalfby";
				public const string LkUiiSavedsessionCreatedby = "lk_uii_savedsession_createdby";
				public const string LkUiiSavedsessionCreatedonbehalfby = "lk_uii_savedsession_createdonbehalfby";
				public const string LkUiiSavedsessionModifiedby = "lk_uii_savedsession_modifiedby";
				public const string LkUiiSavedsessionModifiedonbehalfby = "lk_uii_savedsession_modifiedonbehalfby";
				public const string LkUiiSessiontransferCreatedby = "lk_uii_sessiontransfer_createdby";
				public const string LkUiiSessiontransferCreatedonbehalfby = "lk_uii_sessiontransfer_createdonbehalfby";
				public const string LkUiiSessiontransferModifiedby = "lk_uii_sessiontransfer_modifiedby";
				public const string LkUiiSessiontransferModifiedonbehalfby = "lk_uii_sessiontransfer_modifiedonbehalfby";
				public const string LkUiiWorkflowCreatedby = "lk_uii_workflow_createdby";
				public const string LkUiiWorkflowCreatedonbehalfby = "lk_uii_workflow_createdonbehalfby";
				public const string LkUiiWorkflowModifiedby = "lk_uii_workflow_modifiedby";
				public const string LkUiiWorkflowModifiedonbehalfby = "lk_uii_workflow_modifiedonbehalfby";
				public const string LkUiiWorkflowWorkflowstepMappingCreatedby = "lk_uii_workflow_workflowstep_mapping_createdby";
				public const string LkUiiWorkflowWorkflowstepMappingCreatedonbehalfby = "lk_uii_workflow_workflowstep_mapping_createdonbehalfby";
				public const string LkUiiWorkflowWorkflowstepMappingModifiedby = "lk_uii_workflow_workflowstep_mapping_modifiedby";
				public const string LkUiiWorkflowWorkflowstepMappingModifiedonbehalfby = "lk_uii_workflow_workflowstep_mapping_modifiedonbehalfby";
				public const string LkUiiWorkflowstepCreatedby = "lk_uii_workflowstep_createdby";
				public const string LkUiiWorkflowstepCreatedonbehalfby = "lk_uii_workflowstep_createdonbehalfby";
				public const string LkUiiWorkflowstepModifiedby = "lk_uii_workflowstep_modifiedby";
				public const string LkUiiWorkflowstepModifiedonbehalfby = "lk_uii_workflowstep_modifiedonbehalfby";
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
				public const string LkWorkqueueCreatedby = "lk_workqueue_createdby";
				public const string LkWorkqueueCreatedonbehalfby = "lk_workqueue_createdonbehalfby";
				public const string LkWorkqueueModifiedby = "lk_workqueue_modifiedby";
				public const string LkWorkqueueModifiedonbehalfby = "lk_workqueue_modifiedonbehalfby";
				public const string LkWorkqueueitemCreatedby = "lk_workqueueitem_createdby";
				public const string LkWorkqueueitemCreatedonbehalfby = "lk_workqueueitem_createdonbehalfby";
				public const string LkWorkqueueitemModifiedby = "lk_workqueueitem_modifiedby";
				public const string LkWorkqueueitemModifiedonbehalfby = "lk_workqueueitem_modifiedonbehalfby";
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
				public const string MsdynAccountmanagerOpportunity = "msdyn_accountmanager_opportunity";
				public const string MsdynAccountmanagerQuote = "msdyn_accountmanager_quote";
				public const string MsdynAccountmanagerSalesorder = "msdyn_accountmanager_salesorder";
				public const string MsdynApprovalSystemuserCreatedby = "msdyn_approval_systemuser_createdby";
				public const string MsdynApprovalSystemuserCreatedonbehalfby = "msdyn_approval_systemuser_createdonbehalfby";
				public const string MsdynApprovalSystemuserModifiedby = "msdyn_approval_systemuser_modifiedby";
				public const string MsdynApprovalSystemuserModifiedonbehalfby = "msdyn_approval_systemuser_modifiedonbehalfby";
				public const string MsdynApprovalSystemuserOwninguser = "msdyn_approval_systemuser_owninguser";
				public const string MsdynBookingalertSystemuserCreatedby = "msdyn_bookingalert_systemuser_createdby";
				public const string MsdynBookingalertSystemuserCreatedonbehalfby = "msdyn_bookingalert_systemuser_createdonbehalfby";
				public const string MsdynBookingalertSystemuserModifiedby = "msdyn_bookingalert_systemuser_modifiedby";
				public const string MsdynBookingalertSystemuserModifiedonbehalfby = "msdyn_bookingalert_systemuser_modifiedonbehalfby";
				public const string MsdynBookingalertSystemuserOwninguser = "msdyn_bookingalert_systemuser_owninguser";
				public const string MsdynCopilottranscriptSystemuserCreatedby = "msdyn_copilottranscript_systemuser_createdby";
				public const string MsdynCopilottranscriptSystemuserCreatedonbehalfby = "msdyn_copilottranscript_systemuser_createdonbehalfby";
				public const string MsdynCopilottranscriptSystemuserModifiedby = "msdyn_copilottranscript_systemuser_modifiedby";
				public const string MsdynCopilottranscriptSystemuserModifiedonbehalfby = "msdyn_copilottranscript_systemuser_modifiedonbehalfby";
				public const string MsdynCopilottranscriptSystemuserOwninguser = "msdyn_copilottranscript_systemuser_owninguser";
				public const string MsdynCustomeremailcommunicationToUserId = "msdyn_customeremailcommunication_ToUserId";
				public const string MsdynMsdynConversationparticipantinsightsSystemuserMsdynUser = "msdyn_msdyn_conversationparticipantinsights_systemuser_msdyn_User";
				public const string MsdynMsdynForecastOwnerid = "msdyn_msdyn_forecast_ownerid";
				public const string MsdynMsdynReadtrackerSystemuser = "msdyn_msdyn_readtracker_systemuser";
				public const string MsdynMsdynShareasconfigurationSharedbyuserid = "msdyn_msdyn_shareasconfiguration_sharedbyuserid";
				public const string MsdynMsdynShareasconfigurationSharedwithuserid = "msdyn_msdyn_shareasconfiguration_sharedwithuserid";
				public const string MsdynOcliveworkitemSystemuserCreatedby = "msdyn_ocliveworkitem_systemuser_createdby";
				public const string MsdynOcliveworkitemSystemuserCreatedonbehalfby = "msdyn_ocliveworkitem_systemuser_createdonbehalfby";
				public const string MsdynOcliveworkitemSystemuserModifiedby = "msdyn_ocliveworkitem_systemuser_modifiedby";
				public const string MsdynOcliveworkitemSystemuserModifiedonbehalfby = "msdyn_ocliveworkitem_systemuser_modifiedonbehalfby";
				public const string MsdynOcliveworkitemSystemuserOwninguser = "msdyn_ocliveworkitem_systemuser_owninguser";
				public const string MsdynOcliveworkitemcharacteristicSkilladdedby = "msdyn_ocliveworkitemcharacteristic_skilladdedby";
				public const string MsdynOcoutboundmessageSystemuserCreatedby = "msdyn_ocoutboundmessage_systemuser_createdby";
				public const string MsdynOcoutboundmessageSystemuserCreatedonbehalfby = "msdyn_ocoutboundmessage_systemuser_createdonbehalfby";
				public const string MsdynOcoutboundmessageSystemuserModifiedby = "msdyn_ocoutboundmessage_systemuser_modifiedby";
				public const string MsdynOcoutboundmessageSystemuserModifiedonbehalfby = "msdyn_ocoutboundmessage_systemuser_modifiedonbehalfby";
				public const string MsdynOcoutboundmessageSystemuserOwninguser = "msdyn_ocoutboundmessage_systemuser_owninguser";
				public const string MsdynOcsessionSystemuserCreatedby = "msdyn_ocsession_systemuser_createdby";
				public const string MsdynOcsessionSystemuserCreatedonbehalfby = "msdyn_ocsession_systemuser_createdonbehalfby";
				public const string MsdynOcsessionSystemuserModifiedby = "msdyn_ocsession_systemuser_modifiedby";
				public const string MsdynOcsessionSystemuserModifiedonbehalfby = "msdyn_ocsession_systemuser_modifiedonbehalfby";
				public const string MsdynOcsessionSystemuserOwninguser = "msdyn_ocsession_systemuser_owninguser";
				public const string MsdynSystemuserMsdynAgentcapacityprofileunitAgentid = "msdyn_systemuser_msdyn_agentcapacityprofileunit_agentid";
				public const string MsdynSystemuserMsdynAgentchannelstateAgentid = "msdyn_systemuser_msdyn_agentchannelstate_agentid";
				public const string MsdynSystemuserMsdynAgentstatusAgentid = "msdyn_systemuser_msdyn_agentstatus_agentid";
				public const string MsdynSystemuserMsdynAgentstatushistoryAgentid = "msdyn_systemuser_msdyn_agentstatushistory_Agentid";
				public const string MsdynSystemuserMsdynApprovalsetApprover = "msdyn_systemuser_msdyn_approvalset_Approver";
				public const string MsdynSystemuserMsdynConversationmessageblockMsdynAgentid = "msdyn_systemuser_msdyn_conversationmessageblock_msdyn_agentid";
				public const string MsdynSystemuserMsdynDataanalyticsworkspaceConfiguredby = "msdyn_systemuser_msdyn_dataanalyticsworkspace_configuredby";
				public const string MsdynSystemuserMsdynDuplicateleadmappingDismissedBy = "msdyn_systemuser_msdyn_duplicateleadmapping_DismissedBy";
				public const string MsdynSystemuserMsdynExpenseManager = "msdyn_systemuser_msdyn_expense_manager";
				public const string MsdynSystemuserMsdynFieldservicesystemjobOwnerId = "msdyn_systemuser_msdyn_fieldservicesystemjob_OwnerId";
				public const string MsdynSystemuserMsdynGeolocationtrackingUserId = "msdyn_systemuser_msdyn_geolocationtracking_UserId";
				public const string MsdynSystemuserMsdynLiveconversation = "msdyn_systemuser_msdyn_liveconversation";
				public const string MsdynSystemuserMsdynLiveworkstreamMsdynBotUser = "msdyn_systemuser_msdyn_liveworkstream_msdyn_bot_user";
				public const string MsdynSystemuserMsdynMsdynAssignmentmapSystemuserid = "msdyn_systemuser_msdyn_msdyn_assignmentmap_systemuserid";
				public const string MsdynSystemuserMsdynOcliveworkitemActiveagentid = "msdyn_systemuser_msdyn_ocliveworkitem_activeagentid";
				public const string MsdynSystemuserMsdynOcliveworkitemparticipantAgentid = "msdyn_systemuser_msdyn_ocliveworkitemparticipant_agentid";
				public const string MsdynSystemuserMsdynOcsitrainingdataApprovedby = "msdyn_systemuser_msdyn_ocsitrainingdata_approvedby";
				public const string MsdynSystemuserMsdynPreferredagentSystemuserid = "msdyn_systemuser_msdyn_preferredagent_systemuserid";
				public const string MsdynSystemuserMsdynProjectProjectmanager = "msdyn_systemuser_msdyn_project_projectmanager";
				public const string MsdynSystemuserMsdynProjectapprovalApprovedBy = "msdyn_systemuser_msdyn_projectapproval_ApprovedBy";
				public const string MsdynSystemuserMsdynProjectapprovalManager = "msdyn_systemuser_msdyn_projectapproval_Manager";
				public const string MsdynSystemuserMsdynPurchaseorderApprovedRejectedBy = "msdyn_systemuser_msdyn_purchaseorder_ApprovedRejectedBy";
				public const string MsdynSystemuserMsdynPurchaseorderOrderedBy = "msdyn_systemuser_msdyn_purchaseorder_OrderedBy";
				public const string MsdynSystemuserMsdynPurchaseorderreceiptReceivedBy = "msdyn_systemuser_msdyn_purchaseorderreceipt_ReceivedBy";
				public const string MsdynSystemuserMsdynResourceassignmentUserresourceid = "msdyn_systemuser_msdyn_resourceassignment_userresourceid";
				public const string MsdynSystemuserMsdynResourcerequestClaimedby = "msdyn_systemuser_msdyn_resourcerequest_claimedby";
				public const string MsdynSystemuserMsdynResourcerequestRequestedby = "msdyn_systemuser_msdyn_resourcerequest_requestedby";
				public const string MsdynSystemuserMsdynRmaApprovedBy = "msdyn_systemuser_msdyn_rma_ApprovedBy";
				public const string MsdynSystemuserMsdynRmareceiptReceivedBy = "msdyn_systemuser_msdyn_rmareceipt_ReceivedBy";
				public const string MsdynSystemuserMsdynRtvApprovedDeclinedBy = "msdyn_systemuser_msdyn_rtv_ApprovedDeclinedBy";
				public const string MsdynSystemuserMsdynRtvReturnedBy = "msdyn_systemuser_msdyn_rtv_ReturnedBy";
				public const string MsdynSystemuserMsdynSalesroutingrunOwnerassigned = "msdyn_systemuser_msdyn_salesroutingrun_ownerassigned";
				public const string MsdynSystemuserMsdynSalesroutingrunPreviousowner = "msdyn_systemuser_msdyn_salesroutingrun_previousowner";
				public const string MsdynSystemuserMsdynSessionparticipantAgentid = "msdyn_systemuser_msdyn_sessionparticipant_agentid";
				public const string MsdynSystemuserMsdynSwarmparticipantUserid = "msdyn_systemuser_msdyn_swarmparticipant_userid";
				public const string MsdynSystemuserMsdynSystemuserschedulersettingUser = "msdyn_systemuser_msdyn_systemuserschedulersetting_User";
				public const string MsdynSystemuserMsdynTimeentryManager = "msdyn_systemuser_msdyn_timeentry_manager";
				public const string MsdynSystemuserMsdynTimeoffrequestApprovedby = "msdyn_systemuser_msdyn_timeoffrequest_Approvedby";
				public const string MsdynSystemuserMsdynUnifiedroutingrunAssignedagent = "msdyn_systemuser_msdyn_unifiedroutingrun_assignedagent";
				public const string MsdynSystemuserMsdynWorkorderClosedBy = "msdyn_systemuser_msdyn_workorder_ClosedBy";
				public const string MsdynSystemuserOcruleitem = "msdyn_systemuser_ocruleitem";
				public const string MsdynSystemuserSuggestionprincipalobjectaccessPrincipalid = "msdyn_systemuser_suggestionprincipalobjectaccess_principalid";
				public const string MsdynSystemuserWallsavedqueryusersettingsUserid = "msdyn_systemuser_wallsavedqueryusersettings_userid";
				public const string MsdyusdSystemuserMsdyusdUsersettingsUser = "msdyusd_systemuser_msdyusd_usersettings_User";
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
				public const string OpportunityOwningUser = "opportunity_owning_user";
				public const string OwnerMappingSystemUser = "OwnerMapping_SystemUser";
				public const string OwningUserDynamicpropertyinsatance = "OwningUser_Dynamicpropertyinsatance";
				public const string QueuePrimaryUser = "queue_primary_user";
				public const string SideloadedaipluginSideloadedpluginownerid = "sideloadedaiplugin_sideloadedpluginownerid";
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
				public const string SystemuserMsdynWkwcolleaguesforcompanyIntroducerSystemuserid = "systemuser_msdyn_wkwcolleaguesforcompany_introducer_systemuserid";
				public const string SystemuserMsdynWkwcolleaguesforcontactIntroducerSystemuserid = "systemuser_msdyn_wkwcolleaguesforcontact_introducer_systemuserid";
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
				public const string UiiToagentUiiSessiontransfer = "uii_toagent_uii_sessiontransfer";
				public const string UserAccounts = "user_accounts";
				public const string UserActivity = "user_activity";
				public const string UserActivityfileattachment = "user_activityfileattachment";
				public const string UserActivitymonitor = "user_activitymonitor";
				public const string UserAdminsettingsentity = "user_adminsettingsentity";
				public const string UserAdxInvitation = "user_adx_invitation";
				public const string UserAdxSetting = "user_adx_setting";
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
				public const string UserAppnotification = "user_appnotification";
				public const string UserAppointment = "user_appointment";
				public const string UserArchivecleanupinfo = "user_archivecleanupinfo";
				public const string UserArchivecleanupoperation = "user_archivecleanupoperation";
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
				public const string UserBotcomponentcollection = "user_botcomponentcollection";
				public const string UserBulkarchiveconfig = "user_bulkarchiveconfig";
				public const string UserBulkarchivefailuredetail = "user_bulkarchivefailuredetail";
				public const string UserBulkarchiveoperation = "user_bulkarchiveoperation";
				public const string UserBulkOperation = "user_BulkOperation";
				public const string UserBulkoperationlog = "user_bulkoperationlog";
				public const string UserCampaignactivity = "user_campaignactivity";
				public const string UserCampaignresponse = "user_campaignresponse";
				public const string UserCanvasappextendedmetadata = "user_canvasappextendedmetadata";
				public const string UserCard = "user_card";
				public const string UserChannelaccessprofile = "user_channelaccessprofile";
				public const string UserCharacteristic = "user_characteristic";
				public const string UserComment = "user_comment";
				public const string UserComponentversion = "user_componentversion";
				public const string UserConnectioninstance = "user_connectioninstance";
				public const string UserConnectionreference = "user_connectionreference";
				public const string UserConnector = "user_connector";
				public const string UserContractdetail = "user_contractdetail";
				public const string UserConversationtranscript = "user_conversationtranscript";
				public const string UserConvertrule = "user_convertrule";
				public const string UserCopilotglossaryterm = "user_copilotglossaryterm";
				public const string UserCopilotsynonyms = "user_copilotsynonyms";
				public const string UserCr072Booking = "user_cr072_booking";
				public const string UserCredential = "user_credential";
				public const string UserCustomapi = "user_customapi";
				public const string UserCustomerOpportunityRoles = "user_customer_opportunity_roles";
				public const string UserCustomerRelationship = "user_customer_relationship";
				public const string UserDatalakefolder = "user_datalakefolder";
				public const string UserDesktopflowbinary = "user_desktopflowbinary";
				public const string UserDesktopflowmodule = "user_desktopflowmodule";
				public const string UserDgtWorkbench = "user_dgt_workbench";
				public const string UserDgtWorkbenchHistory = "user_dgt_workbench_history";
				public const string UserDvfilesearch = "user_dvfilesearch";
				public const string UserDvfilesearchattribute = "user_dvfilesearchattribute";
				public const string UserDvfilesearchentity = "user_dvfilesearchentity";
				public const string UserDvtablesearch = "user_dvtablesearch";
				public const string UserDvtablesearchattribute = "user_dvtablesearchattribute";
				public const string UserDvtablesearchentity = "user_dvtablesearchentity";
				public const string UserEmail = "user_email";
				public const string UserEnablearchivalrequest = "user_enablearchivalrequest";
				public const string UserEntitlement = "user_entitlement";
				public const string UserEntitlementchannel = "user_entitlementchannel";
				public const string UserEntitlemententityallocationtypemapping = "user_entitlemententityallocationtypemapping";
				public const string UserEnvironmentvariabledefinition = "user_environmentvariabledefinition";
				public const string UserEnvironmentvariablevalue = "user_environmentvariablevalue";
				public const string UserExchangesyncidmapping = "user_exchangesyncidmapping";
				public const string UserExportedexcel = "user_exportedexcel";
				public const string UserExportsolutionupload = "user_exportsolutionupload";
				public const string UserExternalparty = "user_externalparty";
				public const string UserFabricaiskill = "user_fabricaiskill";
				public const string UserFax = "user_fax";
				public const string UserFeaturecontrolsetting = "user_featurecontrolsetting";
				public const string UserFederatedknowledgeconfiguration = "user_federatedknowledgeconfiguration";
				public const string UserFederatedknowledgeentityconfiguration = "user_federatedknowledgeentityconfiguration";
				public const string UserFlowcapacityassignment = "user_flowcapacityassignment";
				public const string UserFlowcredentialapplication = "user_flowcredentialapplication";
				public const string UserFlowevent = "user_flowevent";
				public const string UserFlowmachine = "user_flowmachine";
				public const string UserFlowmachinegroup = "user_flowmachinegroup";
				public const string UserFlowmachineimage = "user_flowmachineimage";
				public const string UserFlowmachineimageversion = "user_flowmachineimageversion";
				public const string UserFlowmachinenetwork = "user_flowmachinenetwork";
				public const string UserFlowrun = "user_flowrun";
				public const string UserFlowsession = "user_flowsession";
				public const string UserFxexpression = "user_fxexpression";
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
				public const string UserMsdynAccountpricelist = "user_msdyn_accountpricelist";
				public const string UserMsdynActioncardactionstat = "user_msdyn_actioncardactionstat";
				public const string UserMsdynActioncardregarding = "user_msdyn_actioncardregarding";
				public const string UserMsdynActioncardrolesetting = "user_msdyn_actioncardrolesetting";
				public const string UserMsdynActioncardstataggregation = "user_msdyn_actioncardstataggregation";
				public const string UserMsdynActiveicdextension = "user_msdyn_activeicdextension";
				public const string UserMsdynActual = "user_msdyn_actual";
				public const string UserMsdynAdminappstate = "user_msdyn_adminappstate";
				public const string UserMsdynAgentcapacityprofileunit = "user_msdyn_agentcapacityprofileunit";
				public const string UserMsdynAgentcapacityupdatehistory = "user_msdyn_agentcapacityupdatehistory";
				public const string UserMsdynAgentcapacityupdatehistoryAgentid = "user_msdyn_agentcapacityupdatehistory_agentid";
				public const string UserMsdynAgentchannelstate = "user_msdyn_agentchannelstate";
				public const string UserMsdynAgentstatus = "user_msdyn_agentstatus";
				public const string UserMsdynAgentstatushistory = "user_msdyn_agentstatushistory";
				public const string UserMsdynAgreement = "user_msdyn_agreement";
				public const string UserMsdynAgreementbookingdate = "user_msdyn_agreementbookingdate";
				public const string UserMsdynAgreementbookingincident = "user_msdyn_agreementbookingincident";
				public const string UserMsdynAgreementbookingproduct = "user_msdyn_agreementbookingproduct";
				public const string UserMsdynAgreementbookingservice = "user_msdyn_agreementbookingservice";
				public const string UserMsdynAgreementbookingservicetask = "user_msdyn_agreementbookingservicetask";
				public const string UserMsdynAgreementbookingsetup = "user_msdyn_agreementbookingsetup";
				public const string UserMsdynAgreementinvoicedate = "user_msdyn_agreementinvoicedate";
				public const string UserMsdynAgreementinvoiceproduct = "user_msdyn_agreementinvoiceproduct";
				public const string UserMsdynAgreementinvoicesetup = "user_msdyn_agreementinvoicesetup";
				public const string UserMsdynAgreementsubstatus = "user_msdyn_agreementsubstatus";
				public const string UserMsdynAibdataset = "user_msdyn_aibdataset";
				public const string UserMsdynAibdatasetfile = "user_msdyn_aibdatasetfile";
				public const string UserMsdynAibdatasetrecord = "user_msdyn_aibdatasetrecord";
				public const string UserMsdynAibdatasetscontainer = "user_msdyn_aibdatasetscontainer";
				public const string UserMsdynAibfeedbackloop = "user_msdyn_aibfeedbackloop";
				public const string UserMsdynAibfile = "user_msdyn_aibfile";
				public const string UserMsdynAibfileattacheddata = "user_msdyn_aibfileattacheddata";
				public const string UserMsdynAicontactsuggestion = "user_msdyn_aicontactsuggestion";
				public const string UserMsdynAievent = "user_msdyn_aievent";
				public const string UserMsdynAifptrainingdocument = "user_msdyn_aifptrainingdocument";
				public const string UserMsdynAimodel = "user_msdyn_aimodel";
				public const string UserMsdynAiodimage = "user_msdyn_aiodimage";
				public const string UserMsdynAiodlabel = "user_msdyn_aiodlabel";
				public const string UserMsdynAiodtrainingboundingbox = "user_msdyn_aiodtrainingboundingbox";
				public const string UserMsdynAiodtrainingimage = "user_msdyn_aiodtrainingimage";
				public const string UserMsdynAitemplate = "user_msdyn_aitemplate";
				public const string UserMsdynAnalysiscomponent = "user_msdyn_analysiscomponent";
				public const string UserMsdynAnalysisjob = "user_msdyn_analysisjob";
				public const string UserMsdynAnalysisoverride = "user_msdyn_analysisoverride";
				public const string UserMsdynAnalysisresult = "user_msdyn_analysisresult";
				public const string UserMsdynAnalysisresultdetail = "user_msdyn_analysisresultdetail";
				public const string UserMsdynAnalytics = "user_msdyn_analytics";
				public const string UserMsdynAnalyticsadminsettings = "user_msdyn_analyticsadminsettings";
				public const string UserMsdynAnalyticsforcs = "user_msdyn_analyticsforcs";
				public const string UserMsdynAppconfiguration = "user_msdyn_appconfiguration";
				public const string UserMsdynApplicationextension = "user_msdyn_applicationextension";
				public const string UserMsdynApplicationtabtemplate = "user_msdyn_applicationtabtemplate";
				public const string UserMsdynAppprofilerolemapping = "user_msdyn_appprofilerolemapping";
				public const string UserMsdynApprovalset = "user_msdyn_approvalset";
				public const string UserMsdynAppstate = "user_msdyn_appstate";
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
				public const string UserMsdynAutonomouscasecreationrule = "user_msdyn_autonomouscasecreationrule";
				public const string UserMsdynBgjobledger = "user_msdyn_bgjobledger";
				public const string UserMsdynBookableresourceassociation = "user_msdyn_bookableresourceassociation";
				public const string UserMsdynBookableresourcebookingquicknote = "user_msdyn_bookableresourcebookingquicknote";
				public const string UserMsdynBookableresourcecapacityprofile = "user_msdyn_bookableresourcecapacityprofile";
				public const string UserMsdynBookingalertstatus = "user_msdyn_bookingalertstatus";
				public const string UserMsdynBookingchange = "user_msdyn_bookingchange";
				public const string UserMsdynBookingjournal = "user_msdyn_bookingjournal";
				public const string UserMsdynBookingrule = "user_msdyn_bookingrule";
				public const string UserMsdynBookingsetupmetadata = "user_msdyn_bookingsetupmetadata";
				public const string UserMsdynBookingtimestamp = "user_msdyn_bookingtimestamp";
				public const string UserMsdynBotsession = "user_msdyn_botsession";
				public const string UserMsdynBusinessclosure = "user_msdyn_businessclosure";
				public const string UserMsdynCallablecontext = "user_msdyn_callablecontext";
				public const string UserMsdynCapacityprofile = "user_msdyn_capacityprofile";
				public const string UserMsdynCdsentityengagementctx = "user_msdyn_cdsentityengagementctx";
				public const string UserMsdynChannel = "user_msdyn_channel";
				public const string UserMsdynChanneldefinition = "user_msdyn_channeldefinition";
				public const string UserMsdynChanneldefinitionconsent = "user_msdyn_channeldefinitionconsent";
				public const string UserMsdynChanneldefinitionlocale = "user_msdyn_channeldefinitionlocale";
				public const string UserMsdynChannelinstance = "user_msdyn_channelinstance";
				public const string UserMsdynChannelinstanceaccount = "user_msdyn_channelinstanceaccount";
				public const string UserMsdynChannelmessageattachment = "user_msdyn_channelmessageattachment";
				public const string UserMsdynChannelmessagecontextpart = "user_msdyn_channelmessagecontextpart";
				public const string UserMsdynChannelmessagepart = "user_msdyn_channelmessagepart";
				public const string UserMsdynChannelprovider = "user_msdyn_channelprovider";
				public const string UserMsdynCharacteristicreqforteammember = "user_msdyn_characteristicreqforteammember";
				public const string UserMsdynChatansweroption = "user_msdyn_chatansweroption";
				public const string UserMsdynChatquestionnaireresponse = "user_msdyn_chatquestionnaireresponse";
				public const string UserMsdynChatquestionnaireresponseitem = "user_msdyn_chatquestionnaireresponseitem";
				public const string UserMsdynChatwidgetlanguage = "user_msdyn_chatwidgetlanguage";
				public const string UserMsdynClientextension = "user_msdyn_clientextension";
				public const string UserMsdynConfiguration = "user_msdyn_configuration";
				public const string UserMsdynConsoleapplicationnotificationfield = "user_msdyn_consoleapplicationnotificationfield";
				public const string UserMsdynConsoleapplicationnotificationtemplate = "user_msdyn_consoleapplicationnotificationtemplate";
				public const string UserMsdynConsoleapplicationsessiontemplate = "user_msdyn_consoleapplicationsessiontemplate";
				public const string UserMsdynConsoleapplicationtemplate = "user_msdyn_consoleapplicationtemplate";
				public const string UserMsdynConsoleapplicationtemplateparameter = "user_msdyn_consoleapplicationtemplateparameter";
				public const string UserMsdynConsumingapplication = "user_msdyn_consumingapplication";
				public const string UserMsdynContactpricelist = "user_msdyn_contactpricelist";
				public const string UserMsdynContactsuggestionrule = "user_msdyn_contactsuggestionrule";
				public const string UserMsdynContactsuggestionruleset = "user_msdyn_contactsuggestionruleset";
				public const string UserMsdynContractlinedetailperformance = "user_msdyn_contractlinedetailperformance";
				public const string UserMsdynContractlinescheduleofvalue = "user_msdyn_contractlinescheduleofvalue";
				public const string UserMsdynContractperformance = "user_msdyn_contractperformance";
				public const string UserMsdynConversationaction = "user_msdyn_conversationaction";
				public const string UserMsdynConversationactionitem = "user_msdyn_conversationactionitem";
				public const string UserMsdynConversationactionlocale = "user_msdyn_conversationactionlocale";
				public const string UserMsdynConversationaggregatedinsights = "user_msdyn_conversationaggregatedinsights";
				public const string UserMsdynConversationcomment = "user_msdyn_conversationcomment";
				public const string UserMsdynConversationdata = "user_msdyn_conversationdata";
				public const string UserMsdynConversationinsight = "user_msdyn_conversationinsight";
				public const string UserMsdynConversationmessageblock = "user_msdyn_conversationmessageblock";
				public const string UserMsdynConversationparticipantinsights = "user_msdyn_conversationparticipantinsights";
				public const string UserMsdynConversationparticipantsentiment = "user_msdyn_conversationparticipantsentiment";
				public const string UserMsdynConversationquestion = "user_msdyn_conversationquestion";
				public const string UserMsdynConversationsegmentsentiment = "user_msdyn_conversationsegmentsentiment";
				public const string UserMsdynConversationsentiment = "user_msdyn_conversationsentiment";
				public const string UserMsdynConversationsignal = "user_msdyn_conversationsignal";
				public const string UserMsdynConversationsubject = "user_msdyn_conversationsubject";
				public const string UserMsdynConversationsummarysuggestion = "user_msdyn_conversationsummarysuggestion";
				public const string UserMsdynConversationsystemtag = "user_msdyn_conversationsystemtag";
				public const string UserMsdynConversationtag = "user_msdyn_conversationtag";
				public const string UserMsdynCopilotagentpreference = "user_msdyn_copilotagentpreference";
				public const string UserMsdynCopilotinteractiondata = "user_msdyn_copilotinteractiondata";
				public const string UserMsdynCopilottranscriptdata = "user_msdyn_copilottranscriptdata";
				public const string UserMsdynCrmconnection = "user_msdyn_crmconnection";
				public const string UserMsdynCsadminconfig = "user_msdyn_csadminconfig";
				public const string UserMsdynCustomapirulesetconfiguration = "user_msdyn_customapirulesetconfiguration";
				public const string UserMsdynCustomcontrolextendedsettings = "user_msdyn_customcontrolextendedsettings";
				public const string UserMsdynCustomengagementctx = "user_msdyn_customengagementctx";
				public const string UserMsdynCustomerasset = "user_msdyn_customerasset";
				public const string UserMsdynCustomerassetattachment = "user_msdyn_customerassetattachment";
				public const string UserMsdynCustomerassetcategory = "user_msdyn_customerassetcategory";
				public const string UserMsdynDataanalyticscustomizedreport = "user_msdyn_dataanalyticscustomizedreport";
				public const string UserMsdynDataanalyticsdataset = "user_msdyn_dataanalyticsdataset";
				public const string UserMsdynDataanalyticsreport = "user_msdyn_dataanalyticsreport";
				public const string UserMsdynDataanalyticsworkspace = "user_msdyn_dataanalyticsworkspace";
				public const string UserMsdynDataexport = "user_msdyn_dataexport";
				public const string UserMsdynDataflow = "user_msdyn_dataflow";
				public const string UserMsdynDataflowDatalakefolder = "user_msdyn_dataflow_datalakefolder";
				public const string UserMsdynDataflowconnectionreference = "user_msdyn_dataflowconnectionreference";
				public const string UserMsdynDataflowrefreshhistory = "user_msdyn_dataflowrefreshhistory";
				public const string UserMsdynDataflowtemplate = "user_msdyn_dataflowtemplate";
				public const string UserMsdynDealmanageraccess = "user_msdyn_dealmanageraccess";
				public const string UserMsdynDealmanagersettings = "user_msdyn_dealmanagersettings";
				public const string UserMsdynDecisioncontract = "user_msdyn_decisioncontract";
				public const string UserMsdynDecisionruleset = "user_msdyn_decisionruleset";
				public const string UserMsdynDefextendedchannelinstance = "user_msdyn_defextendedchannelinstance";
				public const string UserMsdynDefextendedchannelinstanceaccount = "user_msdyn_defextendedchannelinstanceaccount";
				public const string UserMsdynDelegation = "user_msdyn_delegation";
				public const string UserMsdynDeletedconversation = "user_msdyn_deletedconversation";
				public const string UserMsdynDimension = "user_msdyn_dimension";
				public const string UserMsdynDmsrequest = "user_msdyn_dmsrequest";
				public const string UserMsdynDmsrequeststatus = "user_msdyn_dmsrequeststatus";
				public const string UserMsdynDmssyncrequest = "user_msdyn_dmssyncrequest";
				public const string UserMsdynDmssyncstatus = "user_msdyn_dmssyncstatus";
				public const string UserMsdynDuplicateleadmapping = "user_msdyn_duplicateleadmapping";
				public const string UserMsdynEffortpredictionresult = "user_msdyn_effortpredictionresult";
				public const string UserMsdynEntitlementapplication = "user_msdyn_entitlementapplication";
				public const string UserMsdynEntityattachment = "user_msdyn_entityattachment";
				public const string UserMsdynEntityconfig = "user_msdyn_entityconfig";
				public const string UserMsdynEntityconfiguration = "user_msdyn_entityconfiguration";
				public const string UserMsdynEntitylinkchatconfiguration = "user_msdyn_entitylinkchatconfiguration";
				public const string UserMsdynEntityrankingrule = "user_msdyn_entityrankingrule";
				public const string UserMsdynEntityrefreshhistory = "user_msdyn_entityrefreshhistory";
				public const string UserMsdynEntityroutingconfiguration = "user_msdyn_entityroutingconfiguration";
				public const string UserMsdynEntityworkstreammap = "user_msdyn_entityworkstreammap";
				public const string UserMsdynEstimate = "user_msdyn_estimate";
				public const string UserMsdynEstimateline = "user_msdyn_estimateline";
				public const string UserMsdynExpense = "user_msdyn_expense";
				public const string UserMsdynExpensereceipt = "user_msdyn_expensereceipt";
				public const string UserMsdynExtendedusersetting = "user_msdyn_extendedusersetting";
				public const string UserMsdynFacebookengagementctx = "user_msdyn_facebookengagementctx";
				public const string UserMsdynFact = "user_msdyn_fact";
				public const string UserMsdynFavoriteknowledgearticle = "user_msdyn_favoriteknowledgearticle";
				public const string UserMsdynFederatedarticle = "user_msdyn_federatedarticle";
				public const string UserMsdynFieldcomputation = "user_msdyn_fieldcomputation";
				public const string UserMsdynFieldservicesetting = "user_msdyn_fieldservicesetting";
				public const string UserMsdynFieldserviceslaconfiguration = "user_msdyn_fieldserviceslaconfiguration";
				public const string UserMsdynFileupload = "user_msdyn_fileupload";
				public const string UserMsdynFindworkevent = "user_msdyn_findworkevent";
				public const string UserMsdynFlowcardtype = "user_msdyn_flowcardtype";
				public const string UserMsdynForecastconfiguration = "user_msdyn_forecastconfiguration";
				public const string UserMsdynForecastdefinition = "user_msdyn_forecastdefinition";
				public const string UserMsdynForecastinstance = "user_msdyn_forecastinstance";
				public const string UserMsdynForecastrecurrence = "user_msdyn_forecastrecurrence";
				public const string UserMsdynFormmapping = "user_msdyn_formmapping";
				public const string UserMsdynFunctionallocation = "user_msdyn_functionallocation";
				public const string UserMsdynFunctionallocationtype = "user_msdyn_functionallocationtype";
				public const string UserMsdynGdprdata = "user_msdyn_gdprdata";
				public const string UserMsdynGeofence = "user_msdyn_geofence";
				public const string UserMsdynGeofenceevent = "user_msdyn_geofenceevent";
				public const string UserMsdynGeofencingsettings = "user_msdyn_geofencingsettings";
				public const string UserMsdynIcdextension = "user_msdyn_icdextension";
				public const string UserMsdynIcebreakersconfig = "user_msdyn_icebreakersconfig";
				public const string UserMsdynIermlmodel = "user_msdyn_iermlmodel";
				public const string UserMsdynIermltraining = "user_msdyn_iermltraining";
				public const string UserMsdynIncidenttype = "user_msdyn_incidenttype";
				public const string UserMsdynIncidenttypeRequirementgroup = "user_msdyn_incidenttype_requirementgroup";
				public const string UserMsdynIncidenttypecharacteristic = "user_msdyn_incidenttypecharacteristic";
				public const string UserMsdynIncidenttypeproduct = "user_msdyn_incidenttypeproduct";
				public const string UserMsdynIncidenttyperecommendationresult = "user_msdyn_incidenttyperecommendationresult";
				public const string UserMsdynIncidenttyperecommendationrunhistory = "user_msdyn_incidenttyperecommendationrunhistory";
				public const string UserMsdynIncidenttyperesolution = "user_msdyn_incidenttyperesolution";
				public const string UserMsdynIncidenttypeservice = "user_msdyn_incidenttypeservice";
				public const string UserMsdynIncidenttypeservicetask = "user_msdyn_incidenttypeservicetask";
				public const string UserMsdynIncidenttypessetup = "user_msdyn_incidenttypessetup";
				public const string UserMsdynInspection = "user_msdyn_inspection";
				public const string UserMsdynInspectionattachment = "user_msdyn_inspectionattachment";
				public const string UserMsdynInspectiondefinition = "user_msdyn_inspectiondefinition";
				public const string UserMsdynInspectioninstance = "user_msdyn_inspectioninstance";
				public const string UserMsdynInspectionresponse = "user_msdyn_inspectionresponse";
				public const string UserMsdynInsurance = "user_msdyn_insurance";
				public const string UserMsdynIntegratedsearchprovider = "user_msdyn_integratedsearchprovider";
				public const string UserMsdynIntegrationjob = "user_msdyn_integrationjob";
				public const string UserMsdynIntegrationjobdetail = "user_msdyn_integrationjobdetail";
				public const string UserMsdynIntent = "user_msdyn_intent";
				public const string UserMsdynInventoryadjustment = "user_msdyn_inventoryadjustment";
				public const string UserMsdynInventoryadjustmentproduct = "user_msdyn_inventoryadjustmentproduct";
				public const string UserMsdynInventoryjournal = "user_msdyn_inventoryjournal";
				public const string UserMsdynInventorytransfer = "user_msdyn_inventorytransfer";
				public const string UserMsdynInvoicelinetransaction = "user_msdyn_invoicelinetransaction";
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
				public const string UserMsdynJobsstate = "user_msdyn_jobsstate";
				public const string UserMsdynJournal = "user_msdyn_journal";
				public const string UserMsdynJournalline = "user_msdyn_journalline";
				public const string UserMsdynKalanguagesetting = "user_msdyn_kalanguagesetting";
				public const string UserMsdynKbattachment = "user_msdyn_kbattachment";
				public const string UserMsdynKmfederatedsearchconfig = "user_msdyn_kmfederatedsearchconfig";
				public const string UserMsdynKnowledgearticleimage = "user_msdyn_knowledgearticleimage";
				public const string UserMsdynKnowledgearticletemplate = "user_msdyn_knowledgearticletemplate";
				public const string UserMsdynKnowledgeassetconfiguration = "user_msdyn_knowledgeassetconfiguration";
				public const string UserMsdynKnowledgeinteractioninsight = "user_msdyn_knowledgeinteractioninsight";
				public const string UserMsdynKnowledgemanagementsetting = "user_msdyn_knowledgemanagementsetting";
				public const string UserMsdynKnowledgepersonalfilter = "user_msdyn_knowledgepersonalfilter";
				public const string UserMsdynKnowledgesearchfilter = "user_msdyn_knowledgesearchfilter";
				public const string UserMsdynKnowledgesearchinsight = "user_msdyn_knowledgesearchinsight";
				public const string UserMsdynKpieventdata = "user_msdyn_kpieventdata";
				public const string UserMsdynKpieventdefinition = "user_msdyn_kpieventdefinition";
				public const string UserMsdynLeadmodelconfig = "user_msdyn_leadmodelconfig";
				public const string UserMsdynLineengagementctx = "user_msdyn_lineengagementctx";
				public const string UserMsdynLivechatconfig = "user_msdyn_livechatconfig";
				public const string UserMsdynLivechatengagementctx = "user_msdyn_livechatengagementctx";
				public const string UserMsdynLivechatwidgetlocation = "user_msdyn_livechatwidgetlocation";
				public const string UserMsdynLiveconversation = "user_msdyn_liveconversation";
				public const string UserMsdynLiveworkitemevent = "user_msdyn_liveworkitemevent";
				public const string UserMsdynLiveworkstream = "user_msdyn_liveworkstream";
				public const string UserMsdynLiveworkstreamcapacityprofile = "user_msdyn_liveworkstreamcapacityprofile";
				public const string UserMsdynLocationtemplateassociation = "user_msdyn_locationtemplateassociation";
				public const string UserMsdynLocationtypetemplateassociation = "user_msdyn_locationtypetemplateassociation";
				public const string UserMsdynLockstatus = "user_msdyn_lockstatus";
				public const string UserMsdynMacrosession = "user_msdyn_macrosession";
				public const string UserMsdynMasterentityroutingconfiguration = "user_msdyn_masterentityroutingconfiguration";
				public const string UserMsdynMigrationtracker = "user_msdyn_migrationtracker";
				public const string UserMsdynMobileapp = "user_msdyn_mobileapp";
				public const string UserMsdynMobilesource = "user_msdyn_mobilesource";
				public const string UserMsdynModelpreviewstatus = "user_msdyn_modelpreviewstatus";
				public const string UserMsdynMsteamssetting = "user_msdyn_msteamssetting";
				public const string UserMsdynNotesanalysisconfig = "user_msdyn_notesanalysisconfig";
				public const string UserMsdynNotificationfield = "user_msdyn_notificationfield";
				public const string UserMsdynNotificationtemplate = "user_msdyn_notificationtemplate";
				public const string UserMsdynNottoexceed = "user_msdyn_nottoexceed";
				public const string UserMsdynOcGeolocationprovider = "user_msdyn_oc_geolocationprovider";
				public const string UserMsdynOcagentassignedcustomapiprivilege = "user_msdyn_ocagentassignedcustomapiprivilege";
				public const string UserMsdynOcapplebusinessaccount = "user_msdyn_ocapplebusinessaccount";
				public const string UserMsdynOcapplemessagesforbusinessengagementctx = "user_msdyn_ocapplemessagesforbusinessengagementctx";
				public const string UserMsdynOcapplepay = "user_msdyn_ocapplepay";
				public const string UserMsdynOcautoblockrule = "user_msdyn_ocautoblockrule";
				public const string UserMsdynOcbotchannelregistration = "user_msdyn_ocbotchannelregistration";
				public const string UserMsdynOcbotchannelregistrationsecret = "user_msdyn_ocbotchannelregistrationsecret";
				public const string UserMsdynOccarrier = "user_msdyn_occarrier";
				public const string UserMsdynOcchannelapiconversationprivilege = "user_msdyn_occhannelapiconversationprivilege";
				public const string UserMsdynOcchannelapimessageprivilege = "user_msdyn_occhannelapimessageprivilege";
				public const string UserMsdynOcchannelapimethodmapping = "user_msdyn_occhannelapimethodmapping";
				public const string UserMsdynOccommunicationprovidersetting = "user_msdyn_occommunicationprovidersetting";
				public const string UserMsdynOccommunicationprovidersettingentry = "user_msdyn_occommunicationprovidersettingentry";
				public const string UserMsdynOccustommessagingchannel = "user_msdyn_occustommessagingchannel";
				public const string UserMsdynOcexternalcontext = "user_msdyn_ocexternalcontext";
				public const string UserMsdynOcfbapplication = "user_msdyn_ocfbapplication";
				public const string UserMsdynOcfbpage = "user_msdyn_ocfbpage";
				public const string UserMsdynOcflaggedspam = "user_msdyn_ocflaggedspam";
				public const string UserMsdynOcgooglebusinessmessagesagentaccount = "user_msdyn_ocgooglebusinessmessagesagentaccount";
				public const string UserMsdynOcgooglebusinessmessagesengagementctx = "user_msdyn_ocgooglebusinessmessagesengagementctx";
				public const string UserMsdynOcgooglebusinessmessagespartneraccount = "user_msdyn_ocgooglebusinessmessagespartneraccount";
				public const string UserMsdynOclanguage = "user_msdyn_oclanguage";
				public const string UserMsdynOclinechannelconfig = "user_msdyn_oclinechannelconfig";
				public const string UserMsdynOcliveworkitemcapacityprofile = "user_msdyn_ocliveworkitemcapacityprofile";
				public const string UserMsdynOcliveworkitemcharacteristic = "user_msdyn_ocliveworkitemcharacteristic";
				public const string UserMsdynOcliveworkitemcontextitem = "user_msdyn_ocliveworkitemcontextitem";
				public const string UserMsdynOcliveworkitemparticipant = "user_msdyn_ocliveworkitemparticipant";
				public const string UserMsdynOcliveworkitemsentiment = "user_msdyn_ocliveworkitemsentiment";
				public const string UserMsdynOcliveworkstreamcontextvariable = "user_msdyn_ocliveworkstreamcontextvariable";
				public const string UserMsdynOcoutboundconfiguration = "user_msdyn_ocoutboundconfiguration";
				public const string UserMsdynOcpaymentprofile = "user_msdyn_ocpaymentprofile";
				public const string UserMsdynOcphonenumber = "user_msdyn_ocphonenumber";
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
				public const string UserMsdynOcsmssettingsecret = "user_msdyn_ocsmssettingsecret";
				public const string UserMsdynOcteamschannelconfig = "user_msdyn_octeamschannelconfig";
				public const string UserMsdynOctwitterapplication = "user_msdyn_octwitterapplication";
				public const string UserMsdynOctwitterhandle = "user_msdyn_octwitterhandle";
				public const string UserMsdynOctwitterhandleprovisioningstatus = "user_msdyn_octwitterhandleprovisioningstatus";
				public const string UserMsdynOctwitterhandlesecret = "user_msdyn_octwitterhandlesecret";
				public const string UserMsdynOcwechatchannelconfig = "user_msdyn_ocwechatchannelconfig";
				public const string UserMsdynOcwhatsappchannelaccount = "user_msdyn_ocwhatsappchannelaccount";
				public const string UserMsdynOcwhatsappchannelnumber = "user_msdyn_ocwhatsappchannelnumber";
				public const string UserMsdynOmnichannelpersonalization = "user_msdyn_omnichannelpersonalization";
				public const string UserMsdynOmnichannelqueue = "user_msdyn_omnichannelqueue";
				public const string UserMsdynOmnichannelsyncconfig = "user_msdyn_omnichannelsyncconfig";
				public const string UserMsdynOperatinghour = "user_msdyn_operatinghour";
				public const string UserMsdynOpportunitylineresourcecategory = "user_msdyn_opportunitylineresourcecategory";
				public const string UserMsdynOpportunitylinetransaction = "user_msdyn_opportunitylinetransaction";
				public const string UserMsdynOpportunitylinetransactioncategory = "user_msdyn_opportunitylinetransactioncategory";
				public const string UserMsdynOpportunitylinetransactionclassificatio = "user_msdyn_opportunitylinetransactionclassificatio";
				public const string UserMsdynOpportunitymodelconfig = "user_msdyn_opportunitymodelconfig";
				public const string UserMsdynOpportunitypricelist = "user_msdyn_opportunitypricelist";
				public const string UserMsdynOrderinvoicingdate = "user_msdyn_orderinvoicingdate";
				public const string UserMsdynOrderinvoicingproduct = "user_msdyn_orderinvoicingproduct";
				public const string UserMsdynOrderinvoicingsetup = "user_msdyn_orderinvoicingsetup";
				public const string UserMsdynOrderinvoicingsetupdate = "user_msdyn_orderinvoicingsetupdate";
				public const string UserMsdynOrderlineresourcecategory = "user_msdyn_orderlineresourcecategory";
				public const string UserMsdynOrderlinetransaction = "user_msdyn_orderlinetransaction";
				public const string UserMsdynOrderlinetransactioncategory = "user_msdyn_orderlinetransactioncategory";
				public const string UserMsdynOrderlinetransactionclassification = "user_msdyn_orderlinetransactionclassification";
				public const string UserMsdynOrderpricelist = "user_msdyn_orderpricelist";
				public const string UserMsdynOrgchartnode = "user_msdyn_orgchartnode";
				public const string UserMsdynOverflowactionconfig = "user_msdyn_overflowactionconfig";
				public const string UserMsdynPayment = "user_msdyn_payment";
				public const string UserMsdynPaymentdetail = "user_msdyn_paymentdetail";
				public const string UserMsdynPaymentmethod = "user_msdyn_paymentmethod";
				public const string UserMsdynPaymentterm = "user_msdyn_paymentterm";
				public const string UserMsdynPersonalmessage = "user_msdyn_personalmessage";
				public const string UserMsdynPersonalsoundsetting = "user_msdyn_personalsoundsetting";
				public const string UserMsdynPlaybookactivity = "user_msdyn_playbookactivity";
				public const string UserMsdynPlaybookactivityattribute = "user_msdyn_playbookactivityattribute";
				public const string UserMsdynPlaybookcategory = "user_msdyn_playbookcategory";
				public const string UserMsdynPlaybookinstance = "user_msdyn_playbookinstance";
				public const string UserMsdynPlaybooktemplate = "user_msdyn_playbooktemplate";
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
				public const string UserMsdynPmtemplate = "user_msdyn_pmtemplate";
				public const string UserMsdynPmview = "user_msdyn_pmview";
				public const string UserMsdynPostalbum = "user_msdyn_postalbum";
				public const string UserMsdynPostalcode = "user_msdyn_postalcode";
				public const string UserMsdynPredictioncomputationoperation = "user_msdyn_predictioncomputationoperation";
				public const string UserMsdynPredictionmodelstatus = "user_msdyn_predictionmodelstatus";
				public const string UserMsdynPredictionscheduledoperation = "user_msdyn_predictionscheduledoperation";
				public const string UserMsdynPredictivescoringsyncstatus = "user_msdyn_predictivescoringsyncstatus";
				public const string UserMsdynPreferredagent = "user_msdyn_preferredagent";
				public const string UserMsdynPreferredagentcustomeridentity = "user_msdyn_preferredagentcustomeridentity";
				public const string UserMsdynPreferredagentroutedentity = "user_msdyn_preferredagentroutedentity";
				public const string UserMsdynPriority = "user_msdyn_priority";
				public const string UserMsdynProblematicasset = "user_msdyn_problematicasset";
				public const string UserMsdynProblematicassetfeedback = "user_msdyn_problematicassetfeedback";
				public const string UserMsdynProductivityactioninputparameter = "user_msdyn_productivityactioninputparameter";
				public const string UserMsdynProductivityactionoutputparameter = "user_msdyn_productivityactionoutputparameter";
				public const string UserMsdynProductivityagentscript = "user_msdyn_productivityagentscript";
				public const string UserMsdynProductivityagentscriptstep = "user_msdyn_productivityagentscriptstep";
				public const string UserMsdynProductivitymacroactiontemplate = "user_msdyn_productivitymacroactiontemplate";
				public const string UserMsdynProductivitymacroconnector = "user_msdyn_productivitymacroconnector";
				public const string UserMsdynProductivitymacrosolutionconfiguration = "user_msdyn_productivitymacrosolutionconfiguration";
				public const string UserMsdynProductivityparameterdefinition = "user_msdyn_productivityparameterdefinition";
				public const string UserMsdynProject = "user_msdyn_project";
				public const string UserMsdynProjectapproval = "user_msdyn_projectapproval";
				public const string UserMsdynProjectpricelist = "user_msdyn_projectpricelist";
				public const string UserMsdynProjecttask = "user_msdyn_projecttask";
				public const string UserMsdynProjecttaskdependency = "user_msdyn_projecttaskdependency";
				public const string UserMsdynProjecttaskstatususer = "user_msdyn_projecttaskstatususer";
				public const string UserMsdynProjectteam = "user_msdyn_projectteam";
				public const string UserMsdynProjecttransactioncategory = "user_msdyn_projecttransactioncategory";
				public const string UserMsdynProperty = "user_msdyn_property";
				public const string UserMsdynPropertyassetassociation = "user_msdyn_propertyassetassociation";
				public const string UserMsdynPropertylocationassociation = "user_msdyn_propertylocationassociation";
				public const string UserMsdynPropertylog = "user_msdyn_propertylog";
				public const string UserMsdynPropertytemplateassociation = "user_msdyn_propertytemplateassociation";
				public const string UserMsdynPurchaseorder = "user_msdyn_purchaseorder";
				public const string UserMsdynPurchaseorderbill = "user_msdyn_purchaseorderbill";
				public const string UserMsdynPurchaseorderproduct = "user_msdyn_purchaseorderproduct";
				public const string UserMsdynPurchaseorderreceipt = "user_msdyn_purchaseorderreceipt";
				public const string UserMsdynPurchaseorderreceiptproduct = "user_msdyn_purchaseorderreceiptproduct";
				public const string UserMsdynPurchaseordersubstatus = "user_msdyn_purchaseordersubstatus";
				public const string UserMsdynQuestionsequence = "user_msdyn_questionsequence";
				public const string UserMsdynQuotebookingincident = "user_msdyn_quotebookingincident";
				public const string UserMsdynQuotebookingproduct = "user_msdyn_quotebookingproduct";
				public const string UserMsdynQuotebookingservice = "user_msdyn_quotebookingservice";
				public const string UserMsdynQuotebookingservicetask = "user_msdyn_quotebookingservicetask";
				public const string UserMsdynQuotebookingsetup = "user_msdyn_quotebookingsetup";
				public const string UserMsdynQuoteinvoicingproduct = "user_msdyn_quoteinvoicingproduct";
				public const string UserMsdynQuoteinvoicingsetup = "user_msdyn_quoteinvoicingsetup";
				public const string UserMsdynQuotelineanalyticsbreakdown = "user_msdyn_quotelineanalyticsbreakdown";
				public const string UserMsdynQuotelineresourcecategory = "user_msdyn_quotelineresourcecategory";
				public const string UserMsdynQuotelinescheduleofvalue = "user_msdyn_quotelinescheduleofvalue";
				public const string UserMsdynQuotelinetransaction = "user_msdyn_quotelinetransaction";
				public const string UserMsdynQuotelinetransactioncategory = "user_msdyn_quotelinetransactioncategory";
				public const string UserMsdynQuotelinetransactionclassification = "user_msdyn_quotelinetransactionclassification";
				public const string UserMsdynQuotepricelist = "user_msdyn_quotepricelist";
				public const string UserMsdynReadtracker = "user_msdyn_readtracker";
				public const string UserMsdynRealtimescoring = "user_msdyn_realtimescoring";
				public const string UserMsdynRealtimescoringoperation = "user_msdyn_realtimescoringoperation";
				public const string UserMsdynRecording = "user_msdyn_recording";
				public const string UserMsdynRelationshipinsightsunifiedconfig = "user_msdyn_relationshipinsightsunifiedconfig";
				public const string UserMsdynReportbookmark = "user_msdyn_reportbookmark";
				public const string UserMsdynRequirementchange = "user_msdyn_requirementchange";
				public const string UserMsdynRequirementcharacteristic = "user_msdyn_requirementcharacteristic";
				public const string UserMsdynRequirementdependency = "user_msdyn_requirementdependency";
				public const string UserMsdynRequirementgroup = "user_msdyn_requirementgroup";
				public const string UserMsdynRequirementorganizationunit = "user_msdyn_requirementorganizationunit";
				public const string UserMsdynRequirementrelationship = "user_msdyn_requirementrelationship";
				public const string UserMsdynRequirementresourcecategory = "user_msdyn_requirementresourcecategory";
				public const string UserMsdynRequirementresourcepreference = "user_msdyn_requirementresourcepreference";
				public const string UserMsdynRequirementstatus = "user_msdyn_requirementstatus";
				public const string UserMsdynResolution = "user_msdyn_resolution";
				public const string UserMsdynResourceassignment = "user_msdyn_resourceassignment";
				public const string UserMsdynResourceassignmentdetail = "user_msdyn_resourceassignmentdetail";
				public const string UserMsdynResourcepaytype = "user_msdyn_resourcepaytype";
				public const string UserMsdynResourcerequest = "user_msdyn_resourcerequest";
				public const string UserMsdynResourcerequirement = "user_msdyn_resourcerequirement";
				public const string UserMsdynResourcerequirementdetail = "user_msdyn_resourcerequirementdetail";
				public const string UserMsdynResourceterritory = "user_msdyn_resourceterritory";
				public const string UserMsdynRichtextfile = "user_msdyn_richtextfile";
				public const string UserMsdynRma = "user_msdyn_rma";
				public const string UserMsdynRmaproduct = "user_msdyn_rmaproduct";
				public const string UserMsdynRmareceipt = "user_msdyn_rmareceipt";
				public const string UserMsdynRmareceiptproduct = "user_msdyn_rmareceiptproduct";
				public const string UserMsdynRmasubstatus = "user_msdyn_rmasubstatus";
				public const string UserMsdynRolecompetencyrequirement = "user_msdyn_rolecompetencyrequirement";
				public const string UserMsdynRoleutilization = "user_msdyn_roleutilization";
				public const string UserMsdynRoutingconfiguration = "user_msdyn_routingconfiguration";
				public const string UserMsdynRoutingconfigurationstep = "user_msdyn_routingconfigurationstep";
				public const string UserMsdynRoutingrequest = "user_msdyn_routingrequest";
				public const string UserMsdynRtv = "user_msdyn_rtv";
				public const string UserMsdynRtvproduct = "user_msdyn_rtvproduct";
				public const string UserMsdynRtvsubstatus = "user_msdyn_rtvsubstatus";
				public const string UserMsdynRulesetdependencymapping = "user_msdyn_rulesetdependencymapping";
				public const string UserMsdynSalescopilotinsight = "user_msdyn_salescopilotinsight";
				public const string UserMsdynSalesforcestructuredobject = "user_msdyn_salesforcestructuredobject";
				public const string UserMsdynSalesforcestructuredqnaconfig = "user_msdyn_salesforcestructuredqnaconfig";
				public const string UserMsdynSalesinsightssettings = "user_msdyn_salesinsightssettings";
				public const string UserMsdynSalesocmessage = "user_msdyn_salesocmessage";
				public const string UserMsdynSalesocsmstemplate = "user_msdyn_salesocsmstemplate";
				public const string UserMsdynSalesroutingrun = "user_msdyn_salesroutingrun";
				public const string UserMsdynSalessuggestion = "user_msdyn_salessuggestion";
				public const string UserMsdynSalestag = "user_msdyn_salestag";
				public const string UserMsdynScenario = "user_msdyn_scenario";
				public const string UserMsdynSchedule = "user_msdyn_schedule";
				public const string UserMsdynScheduleboardsetting = "user_msdyn_scheduleboardsetting";
				public const string UserMsdynSchedulingfeatureflag = "user_msdyn_schedulingfeatureflag";
				public const string UserMsdynSciconversation = "user_msdyn_sciconversation";
				public const string UserMsdynScicustomemailhighlight = "user_msdyn_scicustomemailhighlight";
				public const string UserMsdynScicustomhighlight = "user_msdyn_scicustomhighlight";
				public const string UserMsdynScicustompublisher = "user_msdyn_scicustompublisher";
				public const string UserMsdynSciusersettings = "user_msdyn_sciusersettings";
				public const string UserMsdynSearchconfiguration = "user_msdyn_searchconfiguration";
				public const string UserMsdynSegment = "user_msdyn_segment";
				public const string UserMsdynSequence = "user_msdyn_sequence";
				public const string UserMsdynSequencestat = "user_msdyn_sequencestat";
				public const string UserMsdynSequencetarget = "user_msdyn_sequencetarget";
				public const string UserMsdynSequencetargetstep = "user_msdyn_sequencetargetstep";
				public const string UserMsdynSequencetemplate = "user_msdyn_sequencetemplate";
				public const string UserMsdynServiceconfiguration = "user_msdyn_serviceconfiguration";
				public const string UserMsdynServiceoneprovisioningrequest = "user_msdyn_serviceoneprovisioningrequest";
				public const string UserMsdynServicetasktype = "user_msdyn_servicetasktype";
				public const string UserMsdynSessiondata = "user_msdyn_sessiondata";
				public const string UserMsdynSessionevent = "user_msdyn_sessionevent";
				public const string UserMsdynSessionparticipant = "user_msdyn_sessionparticipant";
				public const string UserMsdynSessionparticipantdata = "user_msdyn_sessionparticipantdata";
				public const string UserMsdynSessiontemplate = "user_msdyn_sessiontemplate";
				public const string UserMsdynShipvia = "user_msdyn_shipvia";
				public const string UserMsdynSiconfig = "user_msdyn_siconfig";
				public const string UserMsdynSkillattachmentruleitem = "user_msdyn_skillattachmentruleitem";
				public const string UserMsdynSkillattachmenttarget = "user_msdyn_skillattachmenttarget";
				public const string UserMsdynSlakpi = "user_msdyn_slakpi";
				public const string UserMsdynSmsengagementctx = "user_msdyn_smsengagementctx";
				public const string UserMsdynSmsnumber = "user_msdyn_smsnumber";
				public const string UserMsdynSolutionhealthrule = "user_msdyn_solutionhealthrule";
				public const string UserMsdynSolutionhealthruleargument = "user_msdyn_solutionhealthruleargument";
				public const string UserMsdynSoundnotificationsetting = "user_msdyn_soundnotificationsetting";
				public const string UserMsdynSubmodeldefinition = "user_msdyn_submodeldefinition";
				public const string UserMsdynSuggestionassignmentrule = "user_msdyn_suggestionassignmentrule";
				public const string UserMsdynSuggestionprincipalobjectaccess = "user_msdyn_suggestionprincipalobjectaccess";
				public const string UserMsdynSuggestionsellerpriority = "user_msdyn_suggestionsellerpriority";
				public const string UserMsdynSurveyquestion = "user_msdyn_surveyquestion";
				public const string UserMsdynSwarm = "user_msdyn_swarm";
				public const string UserMsdynSwarmparticipant = "user_msdyn_swarmparticipant";
				public const string UserMsdynSwarmparticipantrule = "user_msdyn_swarmparticipantrule";
				public const string UserMsdynSwarmrole = "user_msdyn_swarmrole";
				public const string UserMsdynSwarmskill = "user_msdyn_swarmskill";
				public const string UserMsdynSwarmtemplate = "user_msdyn_swarmtemplate";
				public const string UserMsdynSystemuserschedulersetting = "user_msdyn_systemuserschedulersetting";
				public const string UserMsdynTaggedrecord = "user_msdyn_taggedrecord";
				public const string UserMsdynTaxcode = "user_msdyn_taxcode";
				public const string UserMsdynTaxcodedetail = "user_msdyn_taxcodedetail";
				public const string UserMsdynTeamschannelengagementctx = "user_msdyn_teamschannelengagementctx";
				public const string UserMsdynTeamsengagementctx = "user_msdyn_teamsengagementctx";
				public const string UserMsdynTemplateforproperties = "user_msdyn_templateforproperties";
				public const string UserMsdynTemplateparameter = "user_msdyn_templateparameter";
				public const string UserMsdynTemplatetags = "user_msdyn_templatetags";
				public const string UserMsdynTimeentry = "user_msdyn_timeentry";
				public const string UserMsdynTimeentrysetting = "user_msdyn_timeentrysetting";
				public const string UserMsdynTimegroup = "user_msdyn_timegroup";
				public const string UserMsdynTimegroupdetail = "user_msdyn_timegroupdetail";
				public const string UserMsdynTimeoffcalendar = "user_msdyn_timeoffcalendar";
				public const string UserMsdynTimeoffrequest = "user_msdyn_timeoffrequest";
				public const string UserMsdynTimespent = "user_msdyn_timespent";
				public const string UserMsdynTrade = "user_msdyn_trade";
				public const string UserMsdynTradecoverage = "user_msdyn_tradecoverage";
				public const string UserMsdynTrainingresult = "user_msdyn_trainingresult";
				public const string UserMsdynTransactionconnection = "user_msdyn_transactionconnection";
				public const string UserMsdynTransactionorigin = "user_msdyn_transactionorigin";
				public const string UserMsdynTranscript = "user_msdyn_transcript";
				public const string UserMsdynTwitterengagementctx = "user_msdyn_twitterengagementctx";
				public const string UserMsdynUnifiedroutingdiagnostic = "user_msdyn_unifiedroutingdiagnostic";
				public const string UserMsdynUnifiedroutingrun = "user_msdyn_unifiedroutingrun";
				public const string UserMsdynUntrackedappointment = "user_msdyn_untrackedappointment";
				public const string UserMsdynUrnotificationtemplate = "user_msdyn_urnotificationtemplate";
				public const string UserMsdynUrnotificationtemplatemapping = "user_msdyn_urnotificationtemplatemapping";
				public const string UserMsdynUserworkhistory = "user_msdyn_userworkhistory";
				public const string UserMsdynVirtualtablecolumncandidate = "user_msdyn_virtualtablecolumncandidate";
				public const string UserMsdynVisitorjourney = "user_msdyn_visitorjourney";
				public const string UserMsdynVivacustomerlist = "user_msdyn_vivacustomerlist";
				public const string UserMsdynVivausersetting = "user_msdyn_vivausersetting";
				public const string UserMsdynWallsavedqueryusersettings = "user_msdyn_wallsavedqueryusersettings";
				public const string UserMsdynWarehouse = "user_msdyn_warehouse";
				public const string UserMsdynWarranty = "user_msdyn_warranty";
				public const string UserMsdynWechatengagementctx = "user_msdyn_wechatengagementctx";
				public const string UserMsdynWhatsappengagementctx = "user_msdyn_whatsappengagementctx";
				public const string UserMsdynWkwconfig = "user_msdyn_wkwconfig";
				public const string UserMsdynWorkhourtemplate = "user_msdyn_workhourtemplate";
				public const string UserMsdynWorkorder = "user_msdyn_workorder";
				public const string UserMsdynWorkordercharacteristic = "user_msdyn_workordercharacteristic";
				public const string UserMsdynWorkorderincident = "user_msdyn_workorderincident";
				public const string UserMsdynWorkordernte = "user_msdyn_workordernte";
				public const string UserMsdynWorkorderproduct = "user_msdyn_workorderproduct";
				public const string UserMsdynWorkorderresolution = "user_msdyn_workorderresolution";
				public const string UserMsdynWorkorderresourcerestriction = "user_msdyn_workorderresourcerestriction";
				public const string UserMsdynWorkorderservice = "user_msdyn_workorderservice";
				public const string UserMsdynWorkorderservicetask = "user_msdyn_workorderservicetask";
				public const string UserMsdynWorkordersubstatus = "user_msdyn_workordersubstatus";
				public const string UserMsdynWorkordertype = "user_msdyn_workordertype";
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
				public const string UserMsdynmktByoacschannelinstance = "user_msdynmkt_byoacschannelinstance";
				public const string UserMsdynmktByoacschannelinstanceaccount = "user_msdynmkt_byoacschannelinstanceaccount";
				public const string UserMsdynmktCatalogeventstatusconfiguration = "user_msdynmkt_catalogeventstatusconfiguration";
				public const string UserMsdynmktConfiguration = "user_msdynmkt_configuration";
				public const string UserMsdynmktEventmetadata = "user_msdynmkt_eventmetadata";
				public const string UserMsdynmktEventparametermetadata = "user_msdynmkt_eventparametermetadata";
				public const string UserMsdynmktExperimentv2 = "user_msdynmkt_experimentv2";
				public const string UserMsdynmktFeatureconfiguration = "user_msdynmkt_featureconfiguration";
				public const string UserMsdynmktInfobipchannelinstance = "user_msdynmkt_infobipchannelinstance";
				public const string UserMsdynmktInfobipchannelinstanceaccount = "user_msdynmkt_infobipchannelinstanceaccount";
				public const string UserMsdynmktLinkmobilitychannelinstance = "user_msdynmkt_linkmobilitychannelinstance";
				public const string UserMsdynmktLinkmobilitychannelinstanceaccount = "user_msdynmkt_linkmobilitychannelinstanceaccount";
				public const string UserMsdynmktMetadataentityrelationship = "user_msdynmkt_metadataentityrelationship";
				public const string UserMsdynmktMetadataitem = "user_msdynmkt_metadataitem";
				public const string UserMsdynmktMetadatastorestate = "user_msdynmkt_metadatastorestate";
				public const string UserMsdynmktMocksmsproviderchannelinstance = "user_msdynmkt_mocksmsproviderchannelinstance";
				public const string UserMsdynmktMocksmsproviderchannelinstanceaccount = "user_msdynmkt_mocksmsproviderchannelinstanceaccount";
				public const string UserMsdynmktPredefinedplaceholder = "user_msdynmkt_predefinedplaceholder";
				public const string UserMsdynmktTelesignchannelinstance = "user_msdynmkt_telesignchannelinstance";
				public const string UserMsdynmktTelesignchannelinstanceaccount = "user_msdynmkt_telesignchannelinstanceaccount";
				public const string UserMsdynmktTwiliochannelinstance = "user_msdynmkt_twiliochannelinstance";
				public const string UserMsdynmktTwiliochannelinstanceaccount = "user_msdynmkt_twiliochannelinstanceaccount";
				public const string UserMsdynmktVibeschannelinstance = "user_msdynmkt_vibeschannelinstance";
				public const string UserMsdynmktVibeschannelinstanceaccount = "user_msdynmkt_vibeschannelinstanceaccount";
				public const string UserMsdyusdActioncallworkflow = "user_msdyusd_actioncallworkflow";
				public const string UserMsdyusdAgentscriptaction = "user_msdyusd_agentscriptaction";
				public const string UserMsdyusdAgentscripttaskcategory = "user_msdyusd_agentscripttaskcategory";
				public const string UserMsdyusdAnswer = "user_msdyusd_answer";
				public const string UserMsdyusdAuditanddiagnosticssetting = "user_msdyusd_auditanddiagnosticssetting";
				public const string UserMsdyusdConfiguration = "user_msdyusd_configuration";
				public const string UserMsdyusdCustomizationfiles = "user_msdyusd_customizationfiles";
				public const string UserMsdyusdEntityassignment = "user_msdyusd_entityassignment";
				public const string UserMsdyusdEntitysearch = "user_msdyusd_entitysearch";
				public const string UserMsdyusdForm = "user_msdyusd_form";
				public const string UserMsdyusdLanguagemodule = "user_msdyusd_languagemodule";
				public const string UserMsdyusdScriptlet = "user_msdyusd_scriptlet";
				public const string UserMsdyusdScripttasktrigger = "user_msdyusd_scripttasktrigger";
				public const string UserMsdyusdSearch = "user_msdyusd_search";
				public const string UserMsdyusdSessioninformation = "user_msdyusd_sessioninformation";
				public const string UserMsdyusdSessiontransfer = "user_msdyusd_sessiontransfer";
				public const string UserMsdyusdTask = "user_msdyusd_task";
				public const string UserMsdyusdToolbarbutton = "user_msdyusd_toolbarbutton";
				public const string UserMsdyusdToolbarstrip = "user_msdyusd_toolbarstrip";
				public const string UserMsdyusdTracesourcesetting = "user_msdyusd_tracesourcesetting";
				public const string UserMsdyusdUcisettings = "user_msdyusd_ucisettings";
				public const string UserMsdyusdUiievent = "user_msdyusd_uiievent";
				public const string UserMsdyusdUsersettings = "user_msdyusd_usersettings";
				public const string UserMsdyusdWindowroute = "user_msdyusd_windowroute";
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
				public const string UserMspcatCatalogsubmissionfiles = "user_mspcat_catalogsubmissionfiles";
				public const string UserMspcatPackagestore = "user_mspcat_packagestore";
				public const string UserNlsqregistration = "user_nlsqregistration";
				public const string UserOpportunityclose = "user_opportunityclose";
				public const string UserOpportunityproduct = "user_opportunityproduct";
				public const string UserOrderclose = "user_orderclose";
				public const string UserOwnerPostfollows = "user_owner_postfollows";
				public const string UserParentUser = "user_parent_user";
				public const string UserPdfsetting = "user_pdfsetting";
				public const string UserPhonecall = "user_phonecall";
				public const string UserPlannerbusinessscenario = "user_plannerbusinessscenario";
				public const string UserPlannersyncaction = "user_plannersyncaction";
				public const string UserPowerbidataset = "user_powerbidataset";
				public const string UserPowerbidatasetapdx = "user_powerbidatasetapdx";
				public const string UserPowerbimashupparameter = "user_powerbimashupparameter";
				public const string UserPowerbireport = "user_powerbireport";
				public const string UserPowerbireportapdx = "user_powerbireportapdx";
				public const string UserPowerfxrule = "user_powerfxrule";
				public const string UserPowerpagecomponent = "user_powerpagecomponent";
				public const string UserPowerpagesite = "user_powerpagesite";
				public const string UserPowerpagesitelanguage = "user_powerpagesitelanguage";
				public const string UserPowerpagesitepublished = "user_powerpagesitepublished";
				public const string UserPowerpageslog = "user_powerpageslog";
				public const string UserPowerpagesscanreport = "user_powerpagesscanreport";
				public const string UserProcessstageparameter = "user_processstageparameter";
				public const string UserProfilerule = "user_profilerule";
				public const string UserQuoteclose = "user_quoteclose";
				public const string UserQuotedetail = "user_quotedetail";
				public const string UserRatingmodel = "user_ratingmodel";
				public const string UserRatingvalue = "user_ratingvalue";
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
				public const string UserRoutingrule = "user_routingrule";
				public const string UserRoutingruleitem = "user_routingruleitem";
				public const string UserSalesorderdetail = "user_salesorderdetail";
				public const string UserSettings = "user_settings";
				public const string UserSharepointdocumentlocation = "user_sharepointdocumentlocation";
				public const string UserSharepointsite = "user_sharepointsite";
				public const string UserSideloadedaiplugin = "user_sideloadedaiplugin";
				public const string UserSlabase = "user_slabase";
				public const string UserSocialactivity = "user_socialactivity";
				public const string UserSolutioncomponentbatchconfiguration = "user_solutioncomponentbatchconfiguration";
				public const string UserStagesolutionupload = "user_stagesolutionupload";
				public const string UserSynapsedatabase = "user_synapsedatabase";
				public const string UserTask = "user_task";
				public const string UserTdsmetadata = "user_tdsmetadata";
				public const string UserUiiAction = "user_uii_action";
				public const string UserUiiAudit = "user_uii_audit";
				public const string UserUiiContext = "user_uii_context";
				public const string UserUiiHostedapplication = "user_uii_hostedapplication";
				public const string UserUiiNonhostedapplication = "user_uii_nonhostedapplication";
				public const string UserUiiOption = "user_uii_option";
				public const string UserUiiSavedsession = "user_uii_savedsession";
				public const string UserUiiSessiontransfer = "user_uii_sessiontransfer";
				public const string UserUiiWorkflow = "user_uii_workflow";
				public const string UserUiiWorkflowWorkflowstepMapping = "user_uii_workflow_workflowstep_mapping";
				public const string UserUiiWorkflowstep = "user_uii_workflowstep";
				public const string UserUntrackedemail = "user_untrackedemail";
				public const string UserUserapplicationmetadata = "user_userapplicationmetadata";
				public const string UserUserauthztracker = "user_userauthztracker";
				public const string UserUserform = "user_userform";
				public const string UserUserquery = "user_userquery";
				public const string UserUserqueryvisualizations = "user_userqueryvisualizations";
				public const string UserWorkflowbinary = "user_workflowbinary";
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
				public const string WorkflowModifiedby = "workflow_modifiedby";
				public const string WorkflowModifiedonbehalfby = "workflow_modifiedonbehalfby";
				public const string WorkqueueitemProcessinguser = "workqueueitem_processinguser";
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
				public const string MsdynMsdynOcphonenumberSystemuserPhonenumberid = "msdyn_msdyn_ocphonenumber_systemuser_phonenumberid";
				public const string MsdynMsdynPresenceSystemuser = "msdyn_msdyn_presence_systemuser";
				public const string MsdyusdConfigurationUsers = "msdyusd_configuration_users";
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
				public const string MsdynCiproviderSystemuserMembership = "msdyn_ciprovider_systemuser_membership";
				public const string MsdynMsdynAttributevalueSystemuser = "msdyn_msdyn_attributevalue_systemuser";
				public const string MsdynMsdynLiveworkstreamSystemuser = "msdyn_msdyn_liveworkstream_systemuser";
				public const string MsdynResourcerequirementSystemuser = "msdyn_resourcerequirement_systemuser";
				public const string MsdynSystemuserMsdynOmnichannelqueue = "msdyn_systemuser_msdyn_omnichannelqueue";
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
