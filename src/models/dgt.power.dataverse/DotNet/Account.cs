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
	/// Business that represents a customer or potential customer. The company that is billed in business transactions.
	/// </summary>
	[DataContractAttribute()]
	[EntityLogicalNameAttribute("account")]
	[System.CodeDom.Compiler.GeneratedCode("dgtp", "2023")]
    [ExcludeFromCodeCoverage]
	public partial class Account : Entity, INotifyPropertyChanging, INotifyPropertyChanged
    {
	    #region ctor
		[DebuggerNonUserCode]
		public Account() : this(false)
        {
        }

        [DebuggerNonUserCode]
		public Account(bool trackChanges = false) : base(EntityLogicalName)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public Account(Guid id, bool trackChanges = false) : base(EntityLogicalName,id)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public Account(KeyAttributeCollection keyAttributes, bool trackChanges = false) : base(EntityLogicalName,keyAttributes)
        {
			_trackChanges = trackChanges;
        }

        [DebuggerNonUserCode]
		public Account(string keyName, object keyValue, bool trackChanges = false) : base(EntityLogicalName, keyName, keyValue)
        {
			_trackChanges = trackChanges;
        }
        #endregion

		#region fields
        private readonly bool _trackChanges;
        private readonly Lazy<HashSet<string>> _changedProperties = new Lazy<HashSet<string>>();
        #endregion

        #region consts
        public const string EntityLogicalName = "account";
        public const int EntityTypeCode = 1;
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
		[AttributeLogicalNameAttribute("accountid")]
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
				AccountId = value;
			}
		}

		/// <summary>
		/// Unique identifier of the account.
		/// </summary>
		[AttributeLogicalName("accountid")]
        public Guid? AccountId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<Guid?>("accountid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(AccountId));
                SetAttributeValue("accountid", value);
				if (value.HasValue)
				{
					base.Id = value.Value;
				}
				else
				{
					base.Id = System.Guid.Empty;
				}
                OnPropertyChanged(nameof(AccountId));
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
		/// Select a category to indicate whether the customer account is standard or preferred.
		/// </summary>
		[AttributeLogicalName("accountcategorycode")]
        public OptionSetValue? AccountCategoryCode
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<OptionSetValue?>("accountcategorycode");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(AccountCategoryCode));
                SetAttributeValue("accountcategorycode", value);
                OnPropertyChanged(nameof(AccountCategoryCode));
            }
        }

		/// <summary>
		/// Select a classification code to indicate the potential value of the customer account based on the projected return on investment, cooperation level, sales cycle length or other criteria.
		/// </summary>
		[AttributeLogicalName("accountclassificationcode")]
        public OptionSetValue? AccountClassificationCode
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<OptionSetValue?>("accountclassificationcode");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(AccountClassificationCode));
                SetAttributeValue("accountclassificationcode", value);
                OnPropertyChanged(nameof(AccountClassificationCode));
            }
        }

		/// <summary>
		/// Type an ID number or code for the account to quickly search and identify the account in system views.
		/// </summary>
		[AttributeLogicalName("accountnumber")]
        public string? AccountNumber
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("accountnumber");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(AccountNumber));
                SetAttributeValue("accountnumber", value);
                OnPropertyChanged(nameof(AccountNumber));
            }
        }

		/// <summary>
		/// Select a rating to indicate the value of the customer account.
		/// </summary>
		[AttributeLogicalName("accountratingcode")]
        public OptionSetValue? AccountRatingCode
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<OptionSetValue?>("accountratingcode");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(AccountRatingCode));
                SetAttributeValue("accountratingcode", value);
                OnPropertyChanged(nameof(AccountRatingCode));
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
                OnPropertyChanging(nameof(Address1AddressTypeCode));
                SetAttributeValue("address1_addresstypecode", value);
                OnPropertyChanged(nameof(Address1AddressTypeCode));
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
                OnPropertyChanging(nameof(Address1Country));
                SetAttributeValue("address1_country", value);
                OnPropertyChanged(nameof(Address1Country));
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
                OnPropertyChanging(nameof(Address1County));
                SetAttributeValue("address1_county", value);
                OnPropertyChanged(nameof(Address1County));
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
                OnPropertyChanging(nameof(Address1Fax));
                SetAttributeValue("address1_fax", value);
                OnPropertyChanged(nameof(Address1Fax));
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
                OnPropertyChanging(nameof(Address1FreightTermsCode));
                SetAttributeValue("address1_freighttermscode", value);
                OnPropertyChanged(nameof(Address1FreightTermsCode));
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
                OnPropertyChanging(nameof(Address1Latitude));
                SetAttributeValue("address1_latitude", value);
                OnPropertyChanged(nameof(Address1Latitude));
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
                OnPropertyChanging(nameof(Address1Line1));
                SetAttributeValue("address1_line1", value);
                OnPropertyChanged(nameof(Address1Line1));
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
                OnPropertyChanging(nameof(Address1Line2));
                SetAttributeValue("address1_line2", value);
                OnPropertyChanged(nameof(Address1Line2));
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
                OnPropertyChanging(nameof(Address1Line3));
                SetAttributeValue("address1_line3", value);
                OnPropertyChanged(nameof(Address1Line3));
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
                OnPropertyChanging(nameof(Address1Longitude));
                SetAttributeValue("address1_longitude", value);
                OnPropertyChanged(nameof(Address1Longitude));
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
                OnPropertyChanging(nameof(Address1Name));
                SetAttributeValue("address1_name", value);
                OnPropertyChanged(nameof(Address1Name));
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
                OnPropertyChanging(nameof(Address1PostalCode));
                SetAttributeValue("address1_postalcode", value);
                OnPropertyChanged(nameof(Address1PostalCode));
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
                OnPropertyChanging(nameof(Address1PostOfficeBox));
                SetAttributeValue("address1_postofficebox", value);
                OnPropertyChanged(nameof(Address1PostOfficeBox));
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
                OnPropertyChanging(nameof(Address1PrimaryContactName));
                SetAttributeValue("address1_primarycontactname", value);
                OnPropertyChanged(nameof(Address1PrimaryContactName));
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
                OnPropertyChanging(nameof(Address1ShippingMethodCode));
                SetAttributeValue("address1_shippingmethodcode", value);
                OnPropertyChanged(nameof(Address1ShippingMethodCode));
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
                OnPropertyChanging(nameof(Address1StateOrProvince));
                SetAttributeValue("address1_stateorprovince", value);
                OnPropertyChanged(nameof(Address1StateOrProvince));
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
                OnPropertyChanging(nameof(Address1Telephone1));
                SetAttributeValue("address1_telephone1", value);
                OnPropertyChanged(nameof(Address1Telephone1));
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
                OnPropertyChanging(nameof(Address1Telephone2));
                SetAttributeValue("address1_telephone2", value);
                OnPropertyChanged(nameof(Address1Telephone2));
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
                OnPropertyChanging(nameof(Address1Telephone3));
                SetAttributeValue("address1_telephone3", value);
                OnPropertyChanged(nameof(Address1Telephone3));
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
                OnPropertyChanging(nameof(Address1UPSZone));
                SetAttributeValue("address1_upszone", value);
                OnPropertyChanged(nameof(Address1UPSZone));
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
                OnPropertyChanging(nameof(Address1UTCOffset));
                SetAttributeValue("address1_utcoffset", value);
                OnPropertyChanged(nameof(Address1UTCOffset));
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
                OnPropertyChanging(nameof(Address2AddressTypeCode));
                SetAttributeValue("address2_addresstypecode", value);
                OnPropertyChanged(nameof(Address2AddressTypeCode));
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
                OnPropertyChanging(nameof(Address2Country));
                SetAttributeValue("address2_country", value);
                OnPropertyChanged(nameof(Address2Country));
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
                OnPropertyChanging(nameof(Address2County));
                SetAttributeValue("address2_county", value);
                OnPropertyChanged(nameof(Address2County));
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
                OnPropertyChanging(nameof(Address2Fax));
                SetAttributeValue("address2_fax", value);
                OnPropertyChanged(nameof(Address2Fax));
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
                OnPropertyChanging(nameof(Address2FreightTermsCode));
                SetAttributeValue("address2_freighttermscode", value);
                OnPropertyChanged(nameof(Address2FreightTermsCode));
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
                OnPropertyChanging(nameof(Address2Latitude));
                SetAttributeValue("address2_latitude", value);
                OnPropertyChanged(nameof(Address2Latitude));
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
                OnPropertyChanging(nameof(Address2Line1));
                SetAttributeValue("address2_line1", value);
                OnPropertyChanged(nameof(Address2Line1));
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
                OnPropertyChanging(nameof(Address2Line2));
                SetAttributeValue("address2_line2", value);
                OnPropertyChanged(nameof(Address2Line2));
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
                OnPropertyChanging(nameof(Address2Line3));
                SetAttributeValue("address2_line3", value);
                OnPropertyChanged(nameof(Address2Line3));
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
                OnPropertyChanging(nameof(Address2Longitude));
                SetAttributeValue("address2_longitude", value);
                OnPropertyChanged(nameof(Address2Longitude));
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
                OnPropertyChanging(nameof(Address2Name));
                SetAttributeValue("address2_name", value);
                OnPropertyChanged(nameof(Address2Name));
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
                OnPropertyChanging(nameof(Address2PostalCode));
                SetAttributeValue("address2_postalcode", value);
                OnPropertyChanged(nameof(Address2PostalCode));
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
                OnPropertyChanging(nameof(Address2PostOfficeBox));
                SetAttributeValue("address2_postofficebox", value);
                OnPropertyChanged(nameof(Address2PostOfficeBox));
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
                OnPropertyChanging(nameof(Address2PrimaryContactName));
                SetAttributeValue("address2_primarycontactname", value);
                OnPropertyChanged(nameof(Address2PrimaryContactName));
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
                OnPropertyChanging(nameof(Address2ShippingMethodCode));
                SetAttributeValue("address2_shippingmethodcode", value);
                OnPropertyChanged(nameof(Address2ShippingMethodCode));
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
                OnPropertyChanging(nameof(Address2StateOrProvince));
                SetAttributeValue("address2_stateorprovince", value);
                OnPropertyChanged(nameof(Address2StateOrProvince));
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
                OnPropertyChanging(nameof(Address2Telephone1));
                SetAttributeValue("address2_telephone1", value);
                OnPropertyChanged(nameof(Address2Telephone1));
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
                OnPropertyChanging(nameof(Address2Telephone2));
                SetAttributeValue("address2_telephone2", value);
                OnPropertyChanged(nameof(Address2Telephone2));
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
                OnPropertyChanging(nameof(Address2Telephone3));
                SetAttributeValue("address2_telephone3", value);
                OnPropertyChanged(nameof(Address2Telephone3));
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
                OnPropertyChanging(nameof(Address2UPSZone));
                SetAttributeValue("address2_upszone", value);
                OnPropertyChanged(nameof(Address2UPSZone));
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
                OnPropertyChanging(nameof(Address2UTCOffset));
                SetAttributeValue("address2_utcoffset", value);
                OnPropertyChanged(nameof(Address2UTCOffset));
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
		/// The base currency equivalent of the aging 30 field.
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
		/// The base currency equivalent of the aging 60 field.
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
		/// The base currency equivalent of the aging 90 field.
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
		/// Select the legal designation or other business type of the account for contracts or reporting purposes.
		/// </summary>
		[AttributeLogicalName("businesstypecode")]
        public OptionSetValue? BusinessTypeCode
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<OptionSetValue?>("businesstypecode");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(BusinessTypeCode));
                SetAttributeValue("businesstypecode", value);
                OnPropertyChanged(nameof(BusinessTypeCode));
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
		/// Type the credit limit of the account. This is a useful reference when you address invoice and accounting issues with the customer.
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
                OnPropertyChanging(nameof(CreditLimit));
                SetAttributeValue("creditlimit", value);
                OnPropertyChanged(nameof(CreditLimit));
            }
        }

		/// <summary>
		/// Shows the credit limit converted to the system's default base currency for reporting purposes.
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
		/// Select whether the credit for the account is on hold. This is a useful reference while addressing the invoice and accounting issues with the customer.
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
                OnPropertyChanging(nameof(CreditOnHold));
                SetAttributeValue("creditonhold", value);
                OnPropertyChanged(nameof(CreditOnHold));
            }
        }

		/// <summary>
		/// Select the size category or range of the account for segmentation and reporting purposes.
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
                OnPropertyChanging(nameof(CustomerSizeCode));
                SetAttributeValue("customersizecode", value);
                OnPropertyChanged(nameof(CustomerSizeCode));
            }
        }

		/// <summary>
		/// Select the category that best describes the relationship between the account and your organization.
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
                OnPropertyChanging(nameof(CustomerTypeCode));
                SetAttributeValue("customertypecode", value);
                OnPropertyChanged(nameof(CustomerTypeCode));
            }
        }

		/// <summary>
		/// Choose the default price list associated with the account to make sure the correct product prices for this customer are applied in sales opportunities, quotes, and orders.
		/// </summary>
		[AttributeLogicalName("defaultpricelevelid")]
        public EntityReference? DefaultPriceLevelId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<EntityReference?>("defaultpricelevelid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(DefaultPriceLevelId));
                SetAttributeValue("defaultpricelevelid", value);
                OnPropertyChanged(nameof(DefaultPriceLevelId));
            }
        }

		/// <summary>
		/// Type additional information to describe the account, such as an excerpt from the company's website.
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
		/// Select whether the account allows bulk email sent through campaigns. If Do Not Allow is selected, the account can be added to marketing lists, but is excluded from email.
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
                OnPropertyChanging(nameof(DoNotBulkEMail));
                SetAttributeValue("donotbulkemail", value);
                OnPropertyChanged(nameof(DoNotBulkEMail));
            }
        }

		/// <summary>
		/// Select whether the account allows bulk postal mail sent through marketing campaigns or quick campaigns. If Do Not Allow is selected, the account can be added to marketing lists, but will be excluded from the postal mail.
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
                OnPropertyChanging(nameof(DoNotBulkPostalMail));
                SetAttributeValue("donotbulkpostalmail", value);
                OnPropertyChanged(nameof(DoNotBulkPostalMail));
            }
        }

		/// <summary>
		/// Select whether the account allows direct email sent from Microsoft Dynamics 365.
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
                OnPropertyChanging(nameof(DoNotEMail));
                SetAttributeValue("donotemail", value);
                OnPropertyChanged(nameof(DoNotEMail));
            }
        }

		/// <summary>
		/// Select whether the account allows faxes. If Do Not Allow is selected, the account will be excluded from fax activities distributed in marketing campaigns.
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
                OnPropertyChanging(nameof(DoNotFax));
                SetAttributeValue("donotfax", value);
                OnPropertyChanged(nameof(DoNotFax));
            }
        }

		/// <summary>
		/// Select whether the account allows phone calls. If Do Not Allow is selected, the account will be excluded from phone call activities distributed in marketing campaigns.
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
                OnPropertyChanging(nameof(DoNotPhone));
                SetAttributeValue("donotphone", value);
                OnPropertyChanged(nameof(DoNotPhone));
            }
        }

		/// <summary>
		/// Select whether the account allows direct mail. If Do Not Allow is selected, the account will be excluded from letter activities distributed in marketing campaigns.
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
                OnPropertyChanging(nameof(DoNotPostalMail));
                SetAttributeValue("donotpostalmail", value);
                OnPropertyChanged(nameof(DoNotPostalMail));
            }
        }

		/// <summary>
		/// Select whether the account accepts marketing materials, such as brochures or catalogs.
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
                OnPropertyChanging(nameof(DoNotSendMM));
                SetAttributeValue("donotsendmm", value);
                OnPropertyChanged(nameof(DoNotSendMM));
            }
        }

		/// <summary>
		/// Type the primary email address for the account.
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
                OnPropertyChanging(nameof(EMailAddress1));
                SetAttributeValue("emailaddress1", value);
                OnPropertyChanged(nameof(EMailAddress1));
            }
        }

		/// <summary>
		/// Type the secondary email address for the account.
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
                OnPropertyChanging(nameof(EMailAddress2));
                SetAttributeValue("emailaddress2", value);
                OnPropertyChanged(nameof(EMailAddress2));
            }
        }

		/// <summary>
		/// Type an alternate email address for the account.
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
                OnPropertyChanging(nameof(EMailAddress3));
                SetAttributeValue("emailaddress3", value);
                OnPropertyChanged(nameof(EMailAddress3));
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
		/// Type the fax number for the account.
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
                OnPropertyChanging(nameof(Fax));
                SetAttributeValue("fax", value);
                OnPropertyChanged(nameof(Fax));
            }
        }

		/// <summary>
		/// Information about whether to allow following email activity like opens, attachment views and link clicks for emails sent to the account.
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
                OnPropertyChanging(nameof(FollowEmail));
                SetAttributeValue("followemail", value);
                OnPropertyChanged(nameof(FollowEmail));
            }
        }

		/// <summary>
		/// Type the URL for the account's FTP site to enable users to access data and share documents.
		/// </summary>
		[AttributeLogicalName("ftpsiteurl")]
        public string? FtpSiteURL
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("ftpsiteurl");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(FtpSiteURL));
                SetAttributeValue("ftpsiteurl", value);
                OnPropertyChanged(nameof(FtpSiteURL));
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
		/// Select the account's primary industry for use in marketing segmentation and demographic analysis.
		/// </summary>
		[AttributeLogicalName("industrycode")]
        public OptionSetValue? IndustryCode
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<OptionSetValue?>("industrycode");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(IndustryCode));
                SetAttributeValue("industrycode", value);
                OnPropertyChanged(nameof(IndustryCode));
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
                OnPropertyChanging(nameof(LastOnHoldTime));
                SetAttributeValue("lastonholdtime", value);
                OnPropertyChanged(nameof(LastOnHoldTime));
            }
        }

		/// <summary>
		/// Shows the date when the account was last included in a marketing campaign or quick campaign.
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
                OnPropertyChanging(nameof(LastUsedInCampaign));
                SetAttributeValue("lastusedincampaign", value);
                OnPropertyChanged(nameof(LastUsedInCampaign));
            }
        }

		/// <summary>
		/// Type the market capitalization of the account to identify the company's equity, used as an indicator in financial performance analysis.
		/// </summary>
		[AttributeLogicalName("marketcap")]
        public Money? MarketCap
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<Money?>("marketcap");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(MarketCap));
                SetAttributeValue("marketcap", value);
                OnPropertyChanged(nameof(MarketCap));
            }
        }

		/// <summary>
		/// Shows the market capitalization converted to the system's default base currency.
		/// </summary>
		[AttributeLogicalName("marketcap_base")]
        public Money? MarketCapBase
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<Money?>("marketcap_base");
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
                OnPropertyChanging(nameof(MarketingOnly));
                SetAttributeValue("marketingonly", value);
                OnPropertyChanged(nameof(MarketingOnly));
            }
        }

		/// <summary>
		/// Shows the master account that the account was merged with.
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
		/// Shows whether the account has been merged with another account.
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
		/// Shows who created the record on behalf of another user.
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
		/// Describes whether account is opted out or not
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
		/// Sales Acceleration Insights ID
		/// </summary>
		[AttributeLogicalName("msdyn_salesaccelerationinsightid")]
        public EntityReference? MsdynSalesaccelerationinsightid
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<EntityReference?>("msdyn_salesaccelerationinsightid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(MsdynSalesaccelerationinsightid));
                SetAttributeValue("msdyn_salesaccelerationinsightid", value);
                OnPropertyChanged(nameof(MsdynSalesaccelerationinsightid));
            }
        }

		/// <summary>
		/// Unique identifier for Segment associated with account.
		/// </summary>
		[AttributeLogicalName("msdyn_segmentid")]
        public EntityReference? MsdynSegmentid
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<EntityReference?>("msdyn_segmentid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(MsdynSegmentid));
                SetAttributeValue("msdyn_segmentid", value);
                OnPropertyChanged(nameof(MsdynSegmentid));
            }
        }

		/// <summary>
		/// Type the company or business name.
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
		/// Type the number of employees that work at the account for use in marketing segmentation and demographic analysis.
		/// </summary>
		[AttributeLogicalName("numberofemployees")]
        public int? NumberOfEmployees
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<int?>("numberofemployees");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(NumberOfEmployees));
                SetAttributeValue("numberofemployees", value);
                OnPropertyChanged(nameof(NumberOfEmployees));
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
		/// Number of open opportunities against an account and its child accounts.
		/// </summary>
		[AttributeLogicalName("opendeals")]
        public int? OpenDeals
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<int?>("opendeals");
            }
        }

		/// <summary>
		/// Last Updated time of rollup field Open Deals.
		/// </summary>
		[AttributeLogicalName("opendeals_date")]
        public DateTime? OpenDealsDate
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<DateTime?>("opendeals_date");
            }
        }

		/// <summary>
		/// State of rollup field Open Deals.
		/// </summary>
		[AttributeLogicalName("opendeals_state")]
        public int? OpenDealsState
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<int?>("opendeals_state");
            }
        }

		/// <summary>
		/// Sum of open revenue against an account and its child accounts.
		/// </summary>
		[AttributeLogicalName("openrevenue")]
        public Money? OpenRevenue
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<Money?>("openrevenue");
            }
        }

		/// <summary>
		/// Value of the Open Revenue in base currency.
		/// </summary>
		[AttributeLogicalName("openrevenue_base")]
        public Money? OpenRevenueBase
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<Money?>("openrevenue_base");
            }
        }

		/// <summary>
		/// Last Updated time of rollup field Open Revenue.
		/// </summary>
		[AttributeLogicalName("openrevenue_date")]
        public DateTime? OpenRevenueDate
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<DateTime?>("openrevenue_date");
            }
        }

		/// <summary>
		/// State of rollup field Open Revenue.
		/// </summary>
		[AttributeLogicalName("openrevenue_state")]
        public int? OpenRevenueState
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<int?>("openrevenue_state");
            }
        }

		/// <summary>
		/// Shows the lead that the account was created from if the account was created by converting a lead in Microsoft Dynamics 365. This is used to relate the account to data on the originating lead for use in reporting and analytics.
		/// </summary>
		[AttributeLogicalName("originatingleadid")]
        public EntityReference? OriginatingLeadId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<EntityReference?>("originatingleadid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(OriginatingLeadId));
                SetAttributeValue("originatingleadid", value);
                OnPropertyChanged(nameof(OriginatingLeadId));
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
                OnPropertyChanging(nameof(OwnerId));
                SetAttributeValue("ownerid", value);
                OnPropertyChanged(nameof(OwnerId));
            }
        }

		
		[AttributeLogicalName("owneridtype")]
        public string? OwnerIdType
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("owneridtype");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(OwnerIdType));
                SetAttributeValue("owneridtype", value);
                OnPropertyChanged(nameof(OwnerIdType));
            }
        }

		/// <summary>
		/// Select the account's ownership structure, such as public or private.
		/// </summary>
		[AttributeLogicalName("ownershipcode")]
        public OptionSetValue? OwnershipCode
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<OptionSetValue?>("ownershipcode");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(OwnershipCode));
                SetAttributeValue("ownershipcode", value);
                OnPropertyChanged(nameof(OwnershipCode));
            }
        }

		/// <summary>
		/// Shows the business unit that the record owner belongs to.
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
		/// Unique identifier of the team who owns the account.
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
		/// Unique identifier of the user who owns the account.
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
		/// Choose the parent account associated with this account to show parent and child businesses in reporting and analytics.
		/// </summary>
		[AttributeLogicalName("parentaccountid")]
        public EntityReference? ParentAccountId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<EntityReference?>("parentaccountid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(ParentAccountId));
                SetAttributeValue("parentaccountid", value);
                OnPropertyChanged(nameof(ParentAccountId));
            }
        }

		/// <summary>
		/// For system use only. Legacy Microsoft Dynamics CRM 3.0 workflow data.
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
                OnPropertyChanging(nameof(ParticipatesInWorkflow));
                SetAttributeValue("participatesinworkflow", value);
                OnPropertyChanged(nameof(ParticipatesInWorkflow));
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
                OnPropertyChanging(nameof(PaymentTermsCode));
                SetAttributeValue("paymenttermscode", value);
                OnPropertyChanged(nameof(PaymentTermsCode));
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
                OnPropertyChanging(nameof(PreferredAppointmentDayCode));
                SetAttributeValue("preferredappointmentdaycode", value);
                OnPropertyChanged(nameof(PreferredAppointmentDayCode));
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
                OnPropertyChanging(nameof(PreferredAppointmentTimeCode));
                SetAttributeValue("preferredappointmenttimecode", value);
                OnPropertyChanged(nameof(PreferredAppointmentTimeCode));
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
                OnPropertyChanging(nameof(PreferredContactMethodCode));
                SetAttributeValue("preferredcontactmethodcode", value);
                OnPropertyChanged(nameof(PreferredContactMethodCode));
            }
        }

		/// <summary>
		/// Choose the account's preferred service facility or equipment to make sure services are scheduled correctly for the customer.
		/// </summary>
		[AttributeLogicalName("preferredequipmentid")]
        public EntityReference? PreferredEquipmentId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<EntityReference?>("preferredequipmentid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(PreferredEquipmentId));
                SetAttributeValue("preferredequipmentid", value);
                OnPropertyChanged(nameof(PreferredEquipmentId));
            }
        }

		/// <summary>
		/// Choose the account's preferred service for reference when you schedule service activities.
		/// </summary>
		[AttributeLogicalName("preferredserviceid")]
        public EntityReference? PreferredServiceId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<EntityReference?>("preferredserviceid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(PreferredServiceId));
                SetAttributeValue("preferredserviceid", value);
                OnPropertyChanged(nameof(PreferredServiceId));
            }
        }

		/// <summary>
		/// Choose the preferred service representative for reference when you schedule service activities for the account.
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
                OnPropertyChanging(nameof(PreferredSystemUserId));
                SetAttributeValue("preferredsystemuserid", value);
                OnPropertyChanged(nameof(PreferredSystemUserId));
            }
        }

		/// <summary>
		/// Choose the primary contact for the account to provide quick access to contact details.
		/// </summary>
		[AttributeLogicalName("primarycontactid")]
        public EntityReference? PrimaryContactId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<EntityReference?>("primarycontactid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(PrimaryContactId));
                SetAttributeValue("primarycontactid", value);
                OnPropertyChanged(nameof(PrimaryContactId));
            }
        }

		/// <summary>
		/// Primary Satori ID for Account
		/// </summary>
		[AttributeLogicalName("primarysatoriid")]
        public string? PrimarySatoriId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("primarysatoriid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(PrimarySatoriId));
                SetAttributeValue("primarysatoriid", value);
                OnPropertyChanged(nameof(PrimarySatoriId));
            }
        }

		/// <summary>
		/// Primary Twitter ID for Account
		/// </summary>
		[AttributeLogicalName("primarytwitterid")]
        public string? PrimaryTwitterId
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("primarytwitterid");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(PrimaryTwitterId));
                SetAttributeValue("primarytwitterid", value);
                OnPropertyChanged(nameof(PrimaryTwitterId));
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
		/// Type the annual revenue for the account, used as an indicator in financial performance analysis.
		/// </summary>
		[AttributeLogicalName("revenue")]
        public Money? Revenue
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<Money?>("revenue");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(Revenue));
                SetAttributeValue("revenue", value);
                OnPropertyChanged(nameof(Revenue));
            }
        }

		/// <summary>
		/// Shows the annual revenue converted to the system's default base currency. The calculations use the exchange rate specified in the Currencies area.
		/// </summary>
		[AttributeLogicalName("revenue_base")]
        public Money? RevenueBase
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<Money?>("revenue_base");
            }
        }

		/// <summary>
		/// Type the number of shares available to the public for the account. This number is used as an indicator in financial performance analysis.
		/// </summary>
		[AttributeLogicalName("sharesoutstanding")]
        public int? SharesOutstanding
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<int?>("sharesoutstanding");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(SharesOutstanding));
                SetAttributeValue("sharesoutstanding", value);
                OnPropertyChanged(nameof(SharesOutstanding));
            }
        }

		/// <summary>
		/// Select a shipping method for deliveries sent to the account's address to designate the preferred carrier or other delivery option.
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
                OnPropertyChanging(nameof(ShippingMethodCode));
                SetAttributeValue("shippingmethodcode", value);
                OnPropertyChanged(nameof(ShippingMethodCode));
            }
        }

		/// <summary>
		/// Type the Standard Industrial Classification (SIC) code that indicates the account's primary industry of business, for use in marketing segmentation and demographic analysis.
		/// </summary>
		[AttributeLogicalName("sic")]
        public string? SIC
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("sic");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(SIC));
                SetAttributeValue("sic", value);
                OnPropertyChanged(nameof(SIC));
            }
        }

		/// <summary>
		/// Choose the service level agreement (SLA) that you want to apply to the Account record.
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
                OnPropertyChanging(nameof(SLAId));
                SetAttributeValue("slaid", value);
                OnPropertyChanged(nameof(SLAId));
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
		/// Shows whether the account is active or inactive. Inactive accounts are read-only and can't be edited unless they are reactivated.
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
                OnPropertyChanging(nameof(StateCode));
                SetAttributeValue("statecode", value);
                OnPropertyChanged(nameof(StateCode));
            }
        }

		/// <summary>
		/// Select the account's status.
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
                OnPropertyChanging(nameof(StatusCode));
                SetAttributeValue("statuscode", value);
                OnPropertyChanged(nameof(StatusCode));
            }
        }

		/// <summary>
		/// Type the stock exchange at which the account is listed to track their stock and financial performance of the company.
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
		/// Number of users or conversations followed the record
		/// </summary>
		[AttributeLogicalName("teamsfollowed")]
        public int? TeamsFollowed
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<int?>("teamsfollowed");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(TeamsFollowed));
                SetAttributeValue("teamsfollowed", value);
                OnPropertyChanged(nameof(TeamsFollowed));
            }
        }

		/// <summary>
		/// Type the main phone number for this account.
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
                OnPropertyChanging(nameof(Telephone1));
                SetAttributeValue("telephone1", value);
                OnPropertyChanged(nameof(Telephone1));
            }
        }

		/// <summary>
		/// Type a second phone number for this account.
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
                OnPropertyChanging(nameof(Telephone2));
                SetAttributeValue("telephone2", value);
                OnPropertyChanged(nameof(Telephone2));
            }
        }

		/// <summary>
		/// Type a third phone number for this account.
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
                OnPropertyChanging(nameof(Telephone3));
                SetAttributeValue("telephone3", value);
                OnPropertyChanged(nameof(Telephone3));
            }
        }

		/// <summary>
		/// Select a region or territory for the account for use in segmentation and analysis.
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
                OnPropertyChanging(nameof(TerritoryCode));
                SetAttributeValue("territorycode", value);
                OnPropertyChanged(nameof(TerritoryCode));
            }
        }

		/// <summary>
		/// Choose the sales region or territory for the account to make sure the account is assigned to the correct representative and for use in segmentation and analysis.
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
		/// Type the stock exchange symbol for the account to track financial performance of the company. You can click the code entered in this field to access the latest trading information from MSN Money.
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
		/// Total time spent for emails (read and write) and meetings by me in relation to account record.
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
                OnPropertyChanging(nameof(TimeZoneRuleVersionNumber));
                SetAttributeValue("timezoneruleversionnumber", value);
                OnPropertyChanged(nameof(TimeZoneRuleVersionNumber));
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
		/// Version number of the account.
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
		/// Type the account's website URL to get quick details about the company profile.
		/// </summary>
		[AttributeLogicalName("websiteurl")]
        public string? WebSiteURL
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("websiteurl");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(WebSiteURL));
                SetAttributeValue("websiteurl", value);
                OnPropertyChanged(nameof(WebSiteURL));
            }
        }

		/// <summary>
		/// Type the phonetic spelling of the company name, if specified in Japanese, to make sure the name is pronounced correctly in phone calls and other communications.
		/// </summary>
		[AttributeLogicalName("yominame")]
        public string? YomiName
        {
            [DebuggerNonUserCode]
			get
            {
                return GetAttributeValue<string?>("yominame");
            }
            [DebuggerNonUserCode]
			set
            {
                OnPropertyChanging(nameof(YomiName));
                SetAttributeValue("yominame", value);
                OnPropertyChanged(nameof(YomiName));
            }
        }


		#endregion

		#region NavigationProperties
		/// <summary>
		/// 1:N Account_AsyncOperations
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("Account_AsyncOperations")]
		public System.Collections.Generic.IEnumerable<AsyncOperation> AccountAsyncOperations
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<AsyncOperation>("Account_AsyncOperations", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("AccountAsyncOperations");
				this.SetRelatedEntities<AsyncOperation>("Account_AsyncOperations", null, value);
				this.OnPropertyChanged("AccountAsyncOperations");
			}
		}

		/// <summary>
		/// 1:N account_master_account
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("account_master_account")]
		public System.Collections.Generic.IEnumerable<Account> AccountMasterAccount
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Account>("account_master_account", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("AccountMasterAccount");
				this.SetRelatedEntities<Account>("account_master_account", null, value);
				this.OnPropertyChanged("AccountMasterAccount");
			}
		}

		/// <summary>
		/// 1:N account_parent_account
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("account_parent_account")]
		public System.Collections.Generic.IEnumerable<Account> AccountParentAccount
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Account>("account_parent_account", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("AccountParentAccount");
				this.SetRelatedEntities<Account>("account_parent_account", null, value);
				this.OnPropertyChanged("AccountParentAccount");
			}
		}

		/// <summary>
		/// 1:N contact_customer_accounts
		/// </summary>	
		[Microsoft.Xrm.Sdk.RelationshipSchemaNameAttribute("contact_customer_accounts")]
		public System.Collections.Generic.IEnumerable<Contact> ContactCustomerAccounts
		{
			[DebuggerNonUserCode]
			get
			{
				return this.GetRelatedEntities<Contact>("contact_customer_accounts", null);
			}
			[DebuggerNonUserCode]
			set
			{
				this.OnPropertyChanging("ContactCustomerAccounts");
				this.SetRelatedEntities<Contact>("contact_customer_accounts", null, value);
				this.OnPropertyChanged("ContactCustomerAccounts");
			}
		}

		#endregion

		#region Options
		public static class Options
		{
			    public struct AccountCategoryCode
                {
					public const int PreferredCustomer = 1;
					public const int Standard = 2;
                }
			    public struct AccountClassificationCode
                {
					public const int DefaultValue = 1;
                }
			    public struct AccountRatingCode
                {
					public const int DefaultValue = 1;
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
			    public struct BusinessTypeCode
                {
					public const int DefaultValue = 1;
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
					public const int Competitor = 1;
					public const int Consultant = 2;
					public const int Customer = 3;
					public const int Investor = 4;
					public const int Partner = 5;
					public const int Influencer = 6;
					public const int Press = 7;
					public const int Prospect = 8;
					public const int Reseller = 9;
					public const int Supplier = 10;
					public const int Vendor = 11;
					public const int Other = 12;
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
                public struct FollowEmail
                {
                    public const bool DoNotAllow = false;
                    public const bool Allow = true;
                }
			    public struct IndustryCode
                {
					public const int Accounting = 1;
					public const int AgricultureAndNonPetrolNaturalResourceExtraction = 2;
					public const int BroadcastingPrintingAndPublishing = 3;
					public const int Brokers = 4;
					public const int BuildingSupplyRetail = 5;
					public const int BusinessServices = 6;
					public const int Consulting = 7;
					public const int ConsumerServices = 8;
					public const int Design_DirectionAndCreativeManagement = 9;
					public const int Distributors_DispatchersAndProcessors = 10;
					public const int Doctor_sOfficesAndClinics = 11;
					public const int DurableManufacturing = 12;
					public const int EatingAndDrinkingPlaces = 13;
					public const int EntertainmentRetail = 14;
					public const int EquipmentRentalAndLeasing = 15;
					public const int Financial = 16;
					public const int FoodAndTobaccoProcessing = 17;
					public const int InboundCapitalIntensiveProcessing = 18;
					public const int InboundRepairAndServices = 19;
					public const int Insurance = 20;
					public const int LegalServices = 21;
					public const int NonDurableMerchandiseRetail = 22;
					public const int OutboundConsumerService = 23;
					public const int PetrochemicalExtractionAndDistribution = 24;
					public const int ServiceRetail = 25;
					public const int SIGAffiliations = 26;
					public const int SocialServices = 27;
					public const int SpecialOutboundTradeContractors = 28;
					public const int SpecialtyRealty = 29;
					public const int Transportation = 30;
					public const int UtilityCreationAndDistribution = 31;
					public const int VehicleRetail = 32;
					public const int Wholesale = 33;
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
                public struct MsdynGdproptout
                {
                    public const bool No = false;
                    public const bool Yes = true;
                }
			    public struct OwnershipCode
                {
					public const int Public = 1;
					public const int Private = 2;
					public const int Subsidiary = 3;
					public const int Other = 4;
                }
                public struct ParticipatesInWorkflow
                {
                    public const bool No = false;
                    public const bool Yes = true;
                }
			    public struct PaymentTermsCode
                {
					public const int Net30 = 1;
					public const int _2_percent_10_Net30 = 2;
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
		public static class LogicalNames
		{
				public const string AccountId = "accountid";
				public const string Address1AddressId = "address1_addressid";
				public const string Address2AddressId = "address2_addressid";
				public const string AccountCategoryCode = "accountcategorycode";
				public const string AccountClassificationCode = "accountclassificationcode";
				public const string AccountNumber = "accountnumber";
				public const string AccountRatingCode = "accountratingcode";
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
				public const string Aging30 = "aging30";
				public const string Aging30Base = "aging30_base";
				public const string Aging60 = "aging60";
				public const string Aging60Base = "aging60_base";
				public const string Aging90 = "aging90";
				public const string Aging90Base = "aging90_base";
				public const string BusinessTypeCode = "businesstypecode";
				public const string CreatedBy = "createdby";
				public const string CreatedByExternalParty = "createdbyexternalparty";
				public const string CreatedOn = "createdon";
				public const string CreatedOnBehalfBy = "createdonbehalfby";
				public const string CreditLimit = "creditlimit";
				public const string CreditLimitBase = "creditlimit_base";
				public const string CreditOnHold = "creditonhold";
				public const string CustomerSizeCode = "customersizecode";
				public const string CustomerTypeCode = "customertypecode";
				public const string DefaultPriceLevelId = "defaultpricelevelid";
				public const string Description = "description";
				public const string DoNotBulkEMail = "donotbulkemail";
				public const string DoNotBulkPostalMail = "donotbulkpostalmail";
				public const string DoNotEMail = "donotemail";
				public const string DoNotFax = "donotfax";
				public const string DoNotPhone = "donotphone";
				public const string DoNotPostalMail = "donotpostalmail";
				public const string DoNotSendMM = "donotsendmm";
				public const string EMailAddress1 = "emailaddress1";
				public const string EMailAddress2 = "emailaddress2";
				public const string EMailAddress3 = "emailaddress3";
				public const string EntityImage = "entityimage";
				public const string EntityImageTimestamp = "entityimage_timestamp";
				public const string EntityImageURL = "entityimage_url";
				public const string EntityImageId = "entityimageid";
				public const string ExchangeRate = "exchangerate";
				public const string Fax = "fax";
				public const string FollowEmail = "followemail";
				public const string FtpSiteURL = "ftpsiteurl";
				public const string ImportSequenceNumber = "importsequencenumber";
				public const string IndustryCode = "industrycode";
				public const string LastOnHoldTime = "lastonholdtime";
				public const string LastUsedInCampaign = "lastusedincampaign";
				public const string MarketCap = "marketcap";
				public const string MarketCapBase = "marketcap_base";
				public const string MarketingOnly = "marketingonly";
				public const string MasterId = "masterid";
				public const string Merged = "merged";
				public const string ModifiedBy = "modifiedby";
				public const string ModifiedByExternalParty = "modifiedbyexternalparty";
				public const string ModifiedOn = "modifiedon";
				public const string ModifiedOnBehalfBy = "modifiedonbehalfby";
				public const string MsdynGdproptout = "msdyn_gdproptout";
				public const string MsdynSalesaccelerationinsightid = "msdyn_salesaccelerationinsightid";
				public const string MsdynSegmentid = "msdyn_segmentid";
				public const string Name = "name";
				public const string NumberOfEmployees = "numberofemployees";
				public const string OnHoldTime = "onholdtime";
				public const string OpenDeals = "opendeals";
				public const string OpenDealsDate = "opendeals_date";
				public const string OpenDealsState = "opendeals_state";
				public const string OpenRevenue = "openrevenue";
				public const string OpenRevenueBase = "openrevenue_base";
				public const string OpenRevenueDate = "openrevenue_date";
				public const string OpenRevenueState = "openrevenue_state";
				public const string OriginatingLeadId = "originatingleadid";
				public const string OverriddenCreatedOn = "overriddencreatedon";
				public const string OwnerId = "ownerid";
				public const string OwnerIdType = "owneridtype";
				public const string OwnershipCode = "ownershipcode";
				public const string OwningBusinessUnit = "owningbusinessunit";
				public const string OwningTeam = "owningteam";
				public const string OwningUser = "owninguser";
				public const string ParentAccountId = "parentaccountid";
				public const string ParticipatesInWorkflow = "participatesinworkflow";
				public const string PaymentTermsCode = "paymenttermscode";
				public const string PreferredAppointmentDayCode = "preferredappointmentdaycode";
				public const string PreferredAppointmentTimeCode = "preferredappointmenttimecode";
				public const string PreferredContactMethodCode = "preferredcontactmethodcode";
				public const string PreferredEquipmentId = "preferredequipmentid";
				public const string PreferredServiceId = "preferredserviceid";
				public const string PreferredSystemUserId = "preferredsystemuserid";
				public const string PrimaryContactId = "primarycontactid";
				public const string PrimarySatoriId = "primarysatoriid";
				public const string PrimaryTwitterId = "primarytwitterid";
				public const string ProcessId = "processid";
				public const string Revenue = "revenue";
				public const string RevenueBase = "revenue_base";
				public const string SharesOutstanding = "sharesoutstanding";
				public const string ShippingMethodCode = "shippingmethodcode";
				public const string SIC = "sic";
				public const string SLAId = "slaid";
				public const string SLAInvokedId = "slainvokedid";
				public const string StageId = "stageid";
				public const string StateCode = "statecode";
				public const string StatusCode = "statuscode";
				public const string StockExchange = "stockexchange";
				public const string TeamsFollowed = "teamsfollowed";
				public const string Telephone1 = "telephone1";
				public const string Telephone2 = "telephone2";
				public const string Telephone3 = "telephone3";
				public const string TerritoryCode = "territorycode";
				public const string TerritoryId = "territoryid";
				public const string TickerSymbol = "tickersymbol";
				public const string TimeSpentByMeOnEmailAndMeetings = "timespentbymeonemailandmeetings";
				public const string TimeZoneRuleVersionNumber = "timezoneruleversionnumber";
				public const string TransactionCurrencyId = "transactioncurrencyid";
				public const string TraversedPath = "traversedpath";
				public const string UTCConversionTimeZoneCode = "utcconversiontimezonecode";
				public const string VersionNumber = "versionnumber";
				public const string WebSiteURL = "websiteurl";
				public const string YomiName = "yominame";
		}
		#endregion

		#region Relations
        public static class Relations
        {
            public static class OneToMany
            {
				public const string AccountActioncard = "account_actioncard";
				public const string AccountActivityParties = "account_activity_parties";
				public const string AccountActivityPointers = "Account_ActivityPointers";
				public const string AccountAnnotation = "Account_Annotation";
				public const string AccountAppointments = "Account_Appointments";
				public const string AccountAsyncOperations = "Account_AsyncOperations";
				public const string AccountBookableresourceAccountId = "account_bookableresource_AccountId";
				public const string AccountBulkDeleteFailures = "Account_BulkDeleteFailures";
				public const string AccountBulkOperations = "account_BulkOperations";
				public const string AccountCampaignResponses = "account_CampaignResponses";
				public const string AccountChats = "account_chats";
				public const string AccountConnections1 = "account_connections1";
				public const string AccountConnections2 = "account_connections2";
				public const string AccountCustomerOpportunityRoles = "account_customer_opportunity_roles";
				public const string AccountCustomerRelationshipCustomer = "account_customer_relationship_customer";
				public const string AccountCustomerRelationshipPartner = "account_customer_relationship_partner";
				public const string AccountCustomerAddress = "Account_CustomerAddress";
				public const string AccountDuplicateBaseRecord = "Account_DuplicateBaseRecord";
				public const string AccountDuplicateMatchingRecord = "Account_DuplicateMatchingRecord";
				public const string AccountEmailEmailSender = "Account_Email_EmailSender";
				public const string AccountEmailSendersAccount = "Account_Email_SendersAccount";
				public const string AccountEmails = "Account_Emails";
				public const string AccountEntitlementAccount = "account_entitlement_Account";
				public const string AccountEntitlementCustomer = "account_entitlement_Customer";
				public const string AccountFaxes = "Account_Faxes";
				public const string AccountIncidentResolutions = "account_IncidentResolutions";
				public const string AccountLetters = "Account_Letters";
				public const string AccountMailboxTrackingFolder = "Account_MailboxTrackingFolder";
				public const string AccountMasterAccount = "account_master_account";
				public const string AccountMsdynOcliveworkitems = "account_msdyn_ocliveworkitems";
				public const string AccountMsdynOcsessions = "account_msdyn_ocsessions";
				public const string AccountMsfpAlerts = "account_msfp_alerts";
				public const string AccountMsfpSurveyinvites = "account_msfp_surveyinvites";
				public const string AccountMsfpSurveyresponses = "account_msfp_surveyresponses";
				public const string AccountOpportunityCloses = "account_OpportunityCloses";
				public const string AccountOrderCloses = "account_OrderCloses";
				public const string AccountParentAccount = "account_parent_account";
				public const string AccountPhonecalls = "Account_Phonecalls";
				public const string AccountPostFollows = "account_PostFollows";
				public const string AccountPostRegardings = "account_PostRegardings";
				public const string AccountPostRoles = "account_PostRoles";
				public const string AccountPosts = "account_Posts";
				public const string AccountPrincipalobjectattributeaccess = "account_principalobjectattributeaccess";
				public const string AccountProcessSessions = "Account_ProcessSessions";
				public const string AccountQuoteCloses = "account_QuoteCloses";
				public const string AccountRecurringAppointmentMasters = "Account_RecurringAppointmentMasters";
				public const string AccountServiceAppointments = "Account_ServiceAppointments";
				public const string AccountSharepointDocument = "Account_SharepointDocument";
				public const string AccountSharepointDocumentLocation = "Account_SharepointDocumentLocation";
				public const string AccountSocialActivities = "Account_SocialActivities";
				public const string AccountSyncErrors = "Account_SyncErrors";
				public const string AccountTasks = "Account_Tasks";
				public const string ContactCustomerAccounts = "contact_customer_accounts";
				public const string ContractBillingcustomerAccounts = "contract_billingcustomer_accounts";
				public const string ContractCustomerAccounts = "contract_customer_accounts";
				public const string ContractlineitemCustomerAccounts = "contractlineitem_customer_accounts";
				public const string CreatedAccountBulkOperationLogs2 = "CreatedAccount_BulkOperationLogs2";
				public const string IncidentCustomerAccounts = "incident_customer_accounts";
				public const string InvoiceCustomerAccounts = "invoice_customer_accounts";
				public const string LeadCustomerAccounts = "lead_customer_accounts";
				public const string LeadParentAccount = "lead_parent_account";
				public const string MsdynAccountMsdynAicontactsuggestionSourcerecord = "msdyn_account_msdyn_aicontactsuggestion_sourcerecord";
				public const string MsdynAccountMsdynCustomerassetAccount = "msdyn_account_msdyn_customerasset_Account";
				public const string MsdynAccountMsdynIotdeviceAccount = "msdyn_account_msdyn_iotdevice_Account";
				public const string MsdynAccountMsdynLiveconversationCustomer = "msdyn_account_msdyn_liveconversation_Customer";
				public const string MsdynAccountMsdynOcliveworkitemCustomer = "msdyn_account_msdyn_ocliveworkitem_Customer";
				public const string MsdynPlaybookinstanceAccount = "msdyn_playbookinstance_account";
				public const string MsdynSabackupdiagnosticAccountMsdynTarget = "msdyn_sabackupdiagnostic_account_msdyn_target";
				public const string MsdynSalesaccelerationinsightsAccount = "msdyn_salesaccelerationinsights_account";
				public const string MsdynSalesroutingdiagnosticAccountMsdynTarget = "msdyn_salesroutingdiagnostic_account_msdyn_target";
				public const string MsdynSalessuggestionAccount = "msdyn_salessuggestion_account";
				public const string MsdynSequencetargetAccountMsdynTarget = "msdyn_sequencetarget_account_msdyn_target";
				public const string MsdynSwarmAccount = "msdyn_swarm_account";
				public const string OpportunityCustomerAccounts = "opportunity_customer_accounts";
				public const string OpportunityParentAccount = "opportunity_parent_account";
				public const string OrderCustomerAccounts = "order_customer_accounts";
				public const string QuoteCustomerAccounts = "quote_customer_accounts";
				public const string SlakpiinstanceAccount = "slakpiinstance_account";
				public const string SocialActivityPostAuthorAccounts = "SocialActivity_PostAuthor_accounts";
				public const string SocialActivityPostAuthorAccountAccounts = "SocialActivity_PostAuthorAccount_accounts";
				public const string SocialprofileCustomerAccounts = "Socialprofile_customer_accounts";
				public const string SourceAccountBulkOperationLogs = "SourceAccount_BulkOperationLogs";
				public const string UserentityinstancedataAccount = "userentityinstancedata_account";
            }

            public static class ManyToOne
            {
				public const string AccountMasterAccount = "account_master_account";
				public const string AccountOriginatingLead = "account_originating_lead";
				public const string AccountParentAccount = "account_parent_account";
				public const string AccountPrimaryContact = "account_primary_contact";
				public const string BusinessUnitAccounts = "business_unit_accounts";
				public const string EquipmentAccounts = "equipment_accounts";
				public const string LkAccountEntityimage = "lk_account_entityimage";
				public const string LkAccountbaseCreatedby = "lk_accountbase_createdby";
				public const string LkAccountbaseCreatedonbehalfby = "lk_accountbase_createdonbehalfby";
				public const string LkAccountbaseModifiedby = "lk_accountbase_modifiedby";
				public const string LkAccountbaseModifiedonbehalfby = "lk_accountbase_modifiedonbehalfby";
				public const string LkExternalpartyAccountCreatedby = "lk_externalparty_account_createdby";
				public const string LkExternalpartyAccountModifiedby = "lk_externalparty_account_modifiedby";
				public const string ManualslaAccount = "manualsla_account";
				public const string MsdynInsightsidSalesaccelerationinsights = "msdyn_insightsid_salesaccelerationinsights";
				public const string MsdynMsdynSegmentAccount = "msdyn_msdyn_segment_account";
				public const string OwnerAccounts = "owner_accounts";
				public const string PriceLevelAccounts = "price_level_accounts";
				public const string ProcessstageAccount = "processstage_account";
				public const string ServiceAccounts = "service_accounts";
				public const string SlaAccount = "sla_account";
				public const string SystemUserAccounts = "system_user_accounts";
				public const string TeamAccounts = "team_accounts";
				public const string TerritoryAccounts = "territory_accounts";
				public const string TransactioncurrencyAccount = "transactioncurrency_account";
				public const string UserAccounts = "user_accounts";
            }

            public static class ManyToMany
            {
				public const string AccountleadsAssociation = "accountleads_association";
				public const string BulkOperationAccounts = "BulkOperation_Accounts";
				public const string CampaignActivityAccounts = "CampaignActivity_Accounts";
				public const string ListaccountAssociation = "listaccount_association";
				public const string MsdynMsdynFunctionallocationAccount = "msdyn_msdyn_functionallocation_account";
            }
        }

        #endregion

		#region Methods

        public static Account Retrieve(IOrganizationService service, Guid id)
        {
            return Retrieve(service,id, new ColumnSet(true));
        }

        public static Account Retrieve(IOrganizationService service, Guid id, ColumnSet columnSet)
        {
            return service.Retrieve("account", id, columnSet).ToEntity<Account>();
        }

        public Account GetChangedEntity()
        {
            if (_trackChanges)
            {
                var attr = new AttributeCollection();
                foreach (var attrName in _changedProperties.Value.Select(changedProperty => ((AttributeLogicalNameAttribute) GetType().GetProperty(changedProperty)!.GetCustomAttribute(typeof (AttributeLogicalNameAttribute))!).LogicalName).Where(attrName => Contains(attrName)))
                {
                    attr.Add(attrName,this[attrName]);
                }
                return new  Account(Id) {Attributes = attr };
            }
            return this;
        }
        #endregion
	}

	#region Context
	public partial class DataContext
	{
		public IQueryable<Account> AccountSet
		{
			get
			{
				return CreateQuery<Account>();
			}
		}
	}
	#endregion
}
