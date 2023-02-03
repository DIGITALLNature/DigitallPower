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
	/// A publisher of a CRM solution.
	/// </summary>
	[DataContractAttribute()]
	[EntityLogicalNameAttribute("publisher")]
	[System.CodeDom.Compiler.GeneratedCode("dgtp", "2023")]
    [ExcludeFromCodeCoverage]
	public partial class Publisher : Entity, INotifyPropertyChanging, INotifyPropertyChanged
    {
	    #region ctor
		[DebuggerNonUserCode]
		public Publisher() : this(false)
        {
        }

        [DebuggerNonUserCode]
		public Publisher(bool trackChanges = false) : base(EntityLogicalName)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public Publisher(Guid id, bool trackChanges = false) : base(EntityLogicalName,id)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public Publisher(KeyAttributeCollection keyAttributes, bool trackChanges = false) : base(EntityLogicalName,keyAttributes)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public Publisher(string keyName, object keyValue, bool trackChanges = false) : base(EntityLogicalName, keyName, keyValue)
        {
			_trackChanges = trackChanges;
        }
        #endregion

		#region fields
        private readonly bool _trackChanges;
        private readonly Lazy<HashSet<string>> _changedProperties = new Lazy<HashSet<string>>();
        #endregion

        #region consts
        public const string EntityLogicalName = "publisher";
        public const int EntityTypeCode = 7101;
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
		[AttributeLogicalNameAttribute("publisherid")]
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
				PublisherId = value;
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
		/// Unique identifier of the publisher.
		/// </summary>
		[AttributeLogicalName("publisherid")]
        public Guid? PublisherId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<Guid?>("publisherid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(PublisherId));
                SetAttributeValue("publisherid", value);
				if (value.HasValue)
				{
					base.Id = value.Value;
				}
				else
				{
					base.Id = System.Guid.Empty;
				}
                OnPropertyChanged(nameof(PublisherId));
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
		/// Type of address for address 2. such as billing, shipping, or primary address.
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
		/// Unique identifier of the user who created the publisher.
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
		/// Date and time when the publisher was created.
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
		/// Unique identifier of the delegate user who created the publisher.
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
		/// Default option value prefix used for newly created options for solutions associated with this publisher.
		/// </summary>
		[AttributeLogicalName("customizationoptionvalueprefix")]
        public int? CustomizationOptionValuePrefix
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<int?>("customizationoptionvalueprefix");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(CustomizationOptionValuePrefix));
                SetAttributeValue("customizationoptionvalueprefix", value);
                OnPropertyChanged(nameof(CustomizationOptionValuePrefix));
            }
        }

		/// <summary>
		/// Prefix used for new entities, attributes, and entity relationships for solutions associated with this publisher.
		/// </summary>
		[AttributeLogicalName("customizationprefix")]
        public string? CustomizationPrefix
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("customizationprefix");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(CustomizationPrefix));
                SetAttributeValue("customizationprefix", value);
                OnPropertyChanged(nameof(CustomizationPrefix));
            }
        }

		/// <summary>
		/// Description of the solution.
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
		/// Email address for the publisher.
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
		/// User display name for this publisher.
		/// </summary>
		[AttributeLogicalName("friendlyname")]
        public string? FriendlyName
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("friendlyname");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(FriendlyName));
                SetAttributeValue("friendlyname", value);
                OnPropertyChanged(nameof(FriendlyName));
            }
        }

		/// <summary>
		/// Indicates whether the publisher was created as part of a managed solution installation.
		/// </summary>
		[AttributeLogicalName("isreadonly")]
        public bool? IsReadonly
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<bool?>("isreadonly");
            }
        }

		/// <summary>
		/// Unique identifier of the user who last modified the publisher.
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
		/// Date and time when the publisher was last modified.
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
		/// Unique identifier of the delegate user who modified the publisher.
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
		/// Unique identifier of the organization associated with the publisher.
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
		/// Default locale of the publisher in Microsoft Pinpoint.
		/// </summary>
		[AttributeLogicalName("pinpointpublisherdefaultlocale")]
        public string? PinpointPublisherDefaultLocale
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("pinpointpublisherdefaultlocale");
            }
        }

		/// <summary>
		/// Identifier of the publisher in Microsoft Pinpoint.
		/// </summary>
		[AttributeLogicalName("pinpointpublisherid")]
        public long? PinpointPublisherId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<long?>("pinpointpublisherid");
            }
        }

		/// <summary>
		/// URL for the supporting website of this publisher.
		/// </summary>
		[AttributeLogicalName("supportingwebsiteurl")]
        public string? SupportingWebsiteUrl
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("supportingwebsiteurl");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(SupportingWebsiteUrl));
                SetAttributeValue("supportingwebsiteurl", value);
                OnPropertyChanged(nameof(SupportingWebsiteUrl));
            }
        }

		/// <summary>
		/// The unique name of this publisher.
		/// </summary>
		[AttributeLogicalName("uniquename")]
        public string? UniqueName
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("uniquename");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(UniqueName));
                SetAttributeValue("uniquename", value);
                OnPropertyChanged(nameof(UniqueName));
            }
        }

		
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
		/// <summary>
		/// 1:N publisher_solution
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("publisher_solution")]
		public System.Collections.Generic.IEnumerable<Solution> PublisherSolution
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Solution>("publisher_solution", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("PublisherSolution");
				this.SetRelatedEntities<Solution>("publisher_solution", null, value);
				this.OnPropertyChanged("PublisherSolution");
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
                public struct IsReadonly
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
				public const string PublisherId = "publisherid";
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
				public const string CreatedBy = "createdby";
				public const string CreatedOn = "createdon";
				public const string CreatedOnBehalfBy = "createdonbehalfby";
				public const string CustomizationOptionValuePrefix = "customizationoptionvalueprefix";
				public const string CustomizationPrefix = "customizationprefix";
				public const string Description = "description";
				public const string EMailAddress = "emailaddress";
				public const string EntityImage = "entityimage";
				public const string EntityImageTimestamp = "entityimage_timestamp";
				public const string EntityImageURL = "entityimage_url";
				public const string EntityImageId = "entityimageid";
				public const string FriendlyName = "friendlyname";
				public const string IsReadonly = "isreadonly";
				public const string ModifiedBy = "modifiedby";
				public const string ModifiedOn = "modifiedon";
				public const string ModifiedOnBehalfBy = "modifiedonbehalfby";
				public const string OrganizationId = "organizationid";
				public const string PinpointPublisherDefaultLocale = "pinpointpublisherdefaultlocale";
				public const string PinpointPublisherId = "pinpointpublisherid";
				public const string SupportingWebsiteUrl = "supportingwebsiteurl";
				public const string UniqueName = "uniquename";
				public const string VersionNumber = "versionnumber";
		}
		#endregion

		#region Relations
        public static class Relations
        {
            public static class OneToMany
            {
				public const string PublisherAppmodule = "publisher_appmodule";
				public const string PublisherDuplicateBaseRecord = "Publisher_DuplicateBaseRecord";
				public const string PublisherDuplicateMatchingRecord = "Publisher_DuplicateMatchingRecord";
				public const string PublisherPublisherAddress = "Publisher_PublisherAddress";
				public const string PublisherSolution = "publisher_solution";
				public const string PublisherSyncErrors = "Publisher_SyncErrors";
				public const string UserentityinstancedataPublisher = "userentityinstancedata_publisher";
            }

            public static class ManyToOne
            {
				public const string LkPublisherCreatedby = "lk_publisher_createdby";
				public const string LkPublisherEntityimage = "lk_publisher_entityimage";
				public const string LkPublisherModifiedby = "lk_publisher_modifiedby";
				public const string LkPublisherbaseCreatedonbehalfby = "lk_publisherbase_createdonbehalfby";
				public const string LkPublisherbaseModifiedonbehalfby = "lk_publisherbase_modifiedonbehalfby";
				public const string OrganizationPublisher = "organization_publisher";
            }

            public static class ManyToMany
            {
            }
        }

        #endregion

		#region Methods

        public static Publisher Retrieve(IOrganizationService service, Guid id)
        {
            return Retrieve(service,id, new ColumnSet(true));
        }

        public static Publisher Retrieve(IOrganizationService service, Guid id, ColumnSet columnSet)
        {
            return service.Retrieve("publisher", id, columnSet).ToEntity<Publisher>();
        }

        public Publisher GetChangedEntity()
        {
            if (_trackChanges)
            {
                var attr = new AttributeCollection();
                foreach (var attrName in _changedProperties.Value.Select(changedProperty => ((AttributeLogicalNameAttribute) GetType().GetProperty(changedProperty)!.GetCustomAttribute(typeof (AttributeLogicalNameAttribute))!).LogicalName).Where(attrName => Contains(attrName)))
                {
                    attr.Add(attrName,this[attrName]);
                }
                return new  Publisher(Id) {Attributes = attr };
            }
            return this;
        }
        #endregion
	}

	#region Context
	public partial class DataContext
	{
		public IQueryable<Publisher> PublisherSet
		{
			get
			{
				return CreateQuery<Publisher>();
			}
		}
	}
	#endregion
}
