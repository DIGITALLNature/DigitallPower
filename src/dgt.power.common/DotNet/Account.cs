using System.Diagnostics.CodeAnalysis;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System.Runtime.Serialization;

// ReSharper disable All
namespace dgt.power.dataverse
{
    /// <inheritdoc cref="Microsoft.Xrm.Sdk.Entity" />
    /// <summary>
	/// Business that represents a customer or potential customer. The company that is billed in business transactions.
	/// </summary>
    [EntityLogicalName("account")]
    [System.CodeDom.Compiler.GeneratedCode("dgtp", "2026")]
    [ExcludeFromCodeCoverage]
    [SuppressMessage("Design", "CA1034")]
    [SuppressMessage("Performance", "CA1815")]
    public partial class Account : Entity
    {
        #region ctor
        public Account() : base(EntityLogicalName) { }

        public Account(Guid id) : base(EntityLogicalName, id) { }

        public Account(KeyAttributeCollection keyAttributes) : base(EntityLogicalName, keyAttributes) { }

        public Account(string keyName, object keyValue) : base(EntityLogicalName, keyName, keyValue) { }
        #endregion

        #region consts
        public const string EntityLogicalName = "account";
        public const string PrimaryNameAttribute = "name";
        public const int EntityTypeCode = 1;
        #endregion

        #region Attributes
        [AttributeLogicalName("accountid")]
        [IgnoreDataMember]
        public new Guid Id
        {
            get
            {
                return base.Id;
            }
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
            get
            {
                return GetAttributeValue<Guid?>("accountid");
            }
            set
            {
                SetAttributeValue("accountid", value);
                base.Id = value.HasValue ? value.Value : Guid.Empty;
            }
        }

        /// <summary>
		/// Unique identifier for address 1.
		/// </summary>
        [AttributeLogicalName("address1_addressid")]
        public Guid? Address1AddressId
        {
            get
            {
                return GetAttributeValue<Guid?>("address1_addressid");
            }
            set
            {
                SetAttributeValue("address1_addressid", value);
                base.Id = value.HasValue ? value.Value : Guid.Empty;
            }
        }

        /// <summary>
		/// Unique identifier for address 2.
		/// </summary>
        [AttributeLogicalName("address2_addressid")]
        public Guid? Address2AddressId
        {
            get
            {
                return GetAttributeValue<Guid?>("address2_addressid");
            }
            set
            {
                SetAttributeValue("address2_addressid", value);
                base.Id = value.HasValue ? value.Value : Guid.Empty;
            }
        }

        /// <summary>
		/// Select a category to indicate whether the customer account is standard or preferred.
		/// </summary>
        [AttributeLogicalName("accountcategorycode")]
        public OptionSetValue? AccountCategoryCode
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("accountcategorycode");
            }
            set
            {
                SetAttributeValue("accountcategorycode", value);
            }
        }

        /// <summary>
		/// Select a classification code to indicate the potential value of the customer account based on the projected return on investment, cooperation level, sales cycle length or other criteria.
		/// </summary>
        [AttributeLogicalName("accountclassificationcode")]
        public OptionSetValue? AccountClassificationCode
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("accountclassificationcode");
            }
            set
            {
                SetAttributeValue("accountclassificationcode", value);
            }
        }

        /// <summary>
		/// Type an ID number or code for the account to quickly search and identify the account in system views.
		/// </summary>
        [AttributeLogicalName("accountnumber")]
        public string? AccountNumber
        {
            get
            {
                return GetAttributeValue<string?>("accountnumber");
            }
            set
            {
                SetAttributeValue("accountnumber", value);
            }
        }

        /// <summary>
		/// Select a rating to indicate the value of the customer account.
		/// </summary>
        [AttributeLogicalName("accountratingcode")]
        public OptionSetValue? AccountRatingCode
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("accountratingcode");
            }
            set
            {
                SetAttributeValue("accountratingcode", value);
            }
        }

        /// <summary>
		/// Select the primary address type.
		/// </summary>
        [AttributeLogicalName("address1_addresstypecode")]
        public OptionSetValue? Address1AddressTypeCode
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("address1_addresstypecode");
            }
            set
            {
                SetAttributeValue("address1_addresstypecode", value);
            }
        }

        /// <summary>
		/// Type the city for the primary address.
		/// </summary>
        [AttributeLogicalName("address1_city")]
        public string? Address1City
        {
            get
            {
                return GetAttributeValue<string?>("address1_city");
            }
            set
            {
                SetAttributeValue("address1_city", value);
            }
        }

        /// <summary>
		/// Shows the complete primary address.
		/// </summary>
        [AttributeLogicalName("address1_composite")]
        public string? Address1Composite
        {
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
            get
            {
                return GetAttributeValue<string?>("address1_country");
            }
            set
            {
                SetAttributeValue("address1_country", value);
            }
        }

        /// <summary>
		/// Type the county for the primary address.
		/// </summary>
        [AttributeLogicalName("address1_county")]
        public string? Address1County
        {
            get
            {
                return GetAttributeValue<string?>("address1_county");
            }
            set
            {
                SetAttributeValue("address1_county", value);
            }
        }

        /// <summary>
		/// Type the fax number associated with the primary address.
		/// </summary>
        [AttributeLogicalName("address1_fax")]
        public string? Address1Fax
        {
            get
            {
                return GetAttributeValue<string?>("address1_fax");
            }
            set
            {
                SetAttributeValue("address1_fax", value);
            }
        }

        /// <summary>
		/// Select the freight terms for the primary address to make sure shipping orders are processed correctly.
		/// </summary>
        [AttributeLogicalName("address1_freighttermscode")]
        public OptionSetValue? Address1FreightTermsCode
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("address1_freighttermscode");
            }
            set
            {
                SetAttributeValue("address1_freighttermscode", value);
            }
        }

        /// <summary>
		/// Type the latitude value for the primary address for use in mapping and other applications.
		/// </summary>
        [AttributeLogicalName("address1_latitude")]
        public double? Address1Latitude
        {
            get
            {
                return GetAttributeValue<double?>("address1_latitude");
            }
            set
            {
                SetAttributeValue("address1_latitude", value);
            }
        }

        /// <summary>
		/// Type the first line of the primary address.
		/// </summary>
        [AttributeLogicalName("address1_line1")]
        public string? Address1Line1
        {
            get
            {
                return GetAttributeValue<string?>("address1_line1");
            }
            set
            {
                SetAttributeValue("address1_line1", value);
            }
        }

        /// <summary>
		/// Type the second line of the primary address.
		/// </summary>
        [AttributeLogicalName("address1_line2")]
        public string? Address1Line2
        {
            get
            {
                return GetAttributeValue<string?>("address1_line2");
            }
            set
            {
                SetAttributeValue("address1_line2", value);
            }
        }

        /// <summary>
		/// Type the third line of the primary address.
		/// </summary>
        [AttributeLogicalName("address1_line3")]
        public string? Address1Line3
        {
            get
            {
                return GetAttributeValue<string?>("address1_line3");
            }
            set
            {
                SetAttributeValue("address1_line3", value);
            }
        }

        /// <summary>
		/// Type the longitude value for the primary address for use in mapping and other applications.
		/// </summary>
        [AttributeLogicalName("address1_longitude")]
        public double? Address1Longitude
        {
            get
            {
                return GetAttributeValue<double?>("address1_longitude");
            }
            set
            {
                SetAttributeValue("address1_longitude", value);
            }
        }

        /// <summary>
		/// Type a descriptive name for the primary address, such as Corporate Headquarters.
		/// </summary>
        [AttributeLogicalName("address1_name")]
        public string? Address1Name
        {
            get
            {
                return GetAttributeValue<string?>("address1_name");
            }
            set
            {
                SetAttributeValue("address1_name", value);
            }
        }

        /// <summary>
		/// Type the ZIP Code or postal code for the primary address.
		/// </summary>
        [AttributeLogicalName("address1_postalcode")]
        public string? Address1PostalCode
        {
            get
            {
                return GetAttributeValue<string?>("address1_postalcode");
            }
            set
            {
                SetAttributeValue("address1_postalcode", value);
            }
        }

        /// <summary>
		/// Type the post office box number of the primary address.
		/// </summary>
        [AttributeLogicalName("address1_postofficebox")]
        public string? Address1PostOfficeBox
        {
            get
            {
                return GetAttributeValue<string?>("address1_postofficebox");
            }
            set
            {
                SetAttributeValue("address1_postofficebox", value);
            }
        }

        /// <summary>
		/// Type the name of the main contact at the account's primary address.
		/// </summary>
        [AttributeLogicalName("address1_primarycontactname")]
        public string? Address1PrimaryContactName
        {
            get
            {
                return GetAttributeValue<string?>("address1_primarycontactname");
            }
            set
            {
                SetAttributeValue("address1_primarycontactname", value);
            }
        }

        /// <summary>
		/// Select a shipping method for deliveries sent to this address.
		/// </summary>
        [AttributeLogicalName("address1_shippingmethodcode")]
        public OptionSetValue? Address1ShippingMethodCode
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("address1_shippingmethodcode");
            }
            set
            {
                SetAttributeValue("address1_shippingmethodcode", value);
            }
        }

        /// <summary>
		/// Type the state or province of the primary address.
		/// </summary>
        [AttributeLogicalName("address1_stateorprovince")]
        public string? Address1StateOrProvince
        {
            get
            {
                return GetAttributeValue<string?>("address1_stateorprovince");
            }
            set
            {
                SetAttributeValue("address1_stateorprovince", value);
            }
        }

        /// <summary>
		/// Type the main phone number associated with the primary address.
		/// </summary>
        [AttributeLogicalName("address1_telephone1")]
        public string? Address1Telephone1
        {
            get
            {
                return GetAttributeValue<string?>("address1_telephone1");
            }
            set
            {
                SetAttributeValue("address1_telephone1", value);
            }
        }

        /// <summary>
		/// Type a second phone number associated with the primary address.
		/// </summary>
        [AttributeLogicalName("address1_telephone2")]
        public string? Address1Telephone2
        {
            get
            {
                return GetAttributeValue<string?>("address1_telephone2");
            }
            set
            {
                SetAttributeValue("address1_telephone2", value);
            }
        }

        /// <summary>
		/// Type a third phone number associated with the primary address.
		/// </summary>
        [AttributeLogicalName("address1_telephone3")]
        public string? Address1Telephone3
        {
            get
            {
                return GetAttributeValue<string?>("address1_telephone3");
            }
            set
            {
                SetAttributeValue("address1_telephone3", value);
            }
        }

        /// <summary>
		/// Type the UPS zone of the primary address to make sure shipping charges are calculated correctly and deliveries are made promptly, if shipped by UPS.
		/// </summary>
        [AttributeLogicalName("address1_upszone")]
        public string? Address1UPSZone
        {
            get
            {
                return GetAttributeValue<string?>("address1_upszone");
            }
            set
            {
                SetAttributeValue("address1_upszone", value);
            }
        }

        /// <summary>
		/// Select the time zone, or UTC offset, for this address so that other people can reference it when they contact someone at this address.
		/// </summary>
        [AttributeLogicalName("address1_utcoffset")]
        public int? Address1UTCOffset
        {
            get
            {
                return GetAttributeValue<int?>("address1_utcoffset");
            }
            set
            {
                SetAttributeValue("address1_utcoffset", value);
            }
        }

        /// <summary>
		/// Select the secondary address type.
		/// </summary>
        [AttributeLogicalName("address2_addresstypecode")]
        public OptionSetValue? Address2AddressTypeCode
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("address2_addresstypecode");
            }
            set
            {
                SetAttributeValue("address2_addresstypecode", value);
            }
        }

        /// <summary>
		/// Type the city for the secondary address.
		/// </summary>
        [AttributeLogicalName("address2_city")]
        public string? Address2City
        {
            get
            {
                return GetAttributeValue<string?>("address2_city");
            }
            set
            {
                SetAttributeValue("address2_city", value);
            }
        }

        /// <summary>
		/// Shows the complete secondary address.
		/// </summary>
        [AttributeLogicalName("address2_composite")]
        public string? Address2Composite
        {
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
            get
            {
                return GetAttributeValue<string?>("address2_country");
            }
            set
            {
                SetAttributeValue("address2_country", value);
            }
        }

        /// <summary>
		/// Type the county for the secondary address.
		/// </summary>
        [AttributeLogicalName("address2_county")]
        public string? Address2County
        {
            get
            {
                return GetAttributeValue<string?>("address2_county");
            }
            set
            {
                SetAttributeValue("address2_county", value);
            }
        }

        /// <summary>
		/// Type the fax number associated with the secondary address.
		/// </summary>
        [AttributeLogicalName("address2_fax")]
        public string? Address2Fax
        {
            get
            {
                return GetAttributeValue<string?>("address2_fax");
            }
            set
            {
                SetAttributeValue("address2_fax", value);
            }
        }

        /// <summary>
		/// Select the freight terms for the secondary address to make sure shipping orders are processed correctly.
		/// </summary>
        [AttributeLogicalName("address2_freighttermscode")]
        public OptionSetValue? Address2FreightTermsCode
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("address2_freighttermscode");
            }
            set
            {
                SetAttributeValue("address2_freighttermscode", value);
            }
        }

        /// <summary>
		/// Type the latitude value for the secondary address for use in mapping and other applications.
		/// </summary>
        [AttributeLogicalName("address2_latitude")]
        public double? Address2Latitude
        {
            get
            {
                return GetAttributeValue<double?>("address2_latitude");
            }
            set
            {
                SetAttributeValue("address2_latitude", value);
            }
        }

        /// <summary>
		/// Type the first line of the secondary address.
		/// </summary>
        [AttributeLogicalName("address2_line1")]
        public string? Address2Line1
        {
            get
            {
                return GetAttributeValue<string?>("address2_line1");
            }
            set
            {
                SetAttributeValue("address2_line1", value);
            }
        }

        /// <summary>
		/// Type the second line of the secondary address.
		/// </summary>
        [AttributeLogicalName("address2_line2")]
        public string? Address2Line2
        {
            get
            {
                return GetAttributeValue<string?>("address2_line2");
            }
            set
            {
                SetAttributeValue("address2_line2", value);
            }
        }

        /// <summary>
		/// Type the third line of the secondary address.
		/// </summary>
        [AttributeLogicalName("address2_line3")]
        public string? Address2Line3
        {
            get
            {
                return GetAttributeValue<string?>("address2_line3");
            }
            set
            {
                SetAttributeValue("address2_line3", value);
            }
        }

        /// <summary>
		/// Type the longitude value for the secondary address for use in mapping and other applications.
		/// </summary>
        [AttributeLogicalName("address2_longitude")]
        public double? Address2Longitude
        {
            get
            {
                return GetAttributeValue<double?>("address2_longitude");
            }
            set
            {
                SetAttributeValue("address2_longitude", value);
            }
        }

        /// <summary>
		/// Type a descriptive name for the secondary address, such as Corporate Headquarters.
		/// </summary>
        [AttributeLogicalName("address2_name")]
        public string? Address2Name
        {
            get
            {
                return GetAttributeValue<string?>("address2_name");
            }
            set
            {
                SetAttributeValue("address2_name", value);
            }
        }

        /// <summary>
		/// Type the ZIP Code or postal code for the secondary address.
		/// </summary>
        [AttributeLogicalName("address2_postalcode")]
        public string? Address2PostalCode
        {
            get
            {
                return GetAttributeValue<string?>("address2_postalcode");
            }
            set
            {
                SetAttributeValue("address2_postalcode", value);
            }
        }

        /// <summary>
		/// Type the post office box number of the secondary address.
		/// </summary>
        [AttributeLogicalName("address2_postofficebox")]
        public string? Address2PostOfficeBox
        {
            get
            {
                return GetAttributeValue<string?>("address2_postofficebox");
            }
            set
            {
                SetAttributeValue("address2_postofficebox", value);
            }
        }

        /// <summary>
		/// Type the name of the main contact at the account's secondary address.
		/// </summary>
        [AttributeLogicalName("address2_primarycontactname")]
        public string? Address2PrimaryContactName
        {
            get
            {
                return GetAttributeValue<string?>("address2_primarycontactname");
            }
            set
            {
                SetAttributeValue("address2_primarycontactname", value);
            }
        }

        /// <summary>
		/// Select a shipping method for deliveries sent to this address.
		/// </summary>
        [AttributeLogicalName("address2_shippingmethodcode")]
        public OptionSetValue? Address2ShippingMethodCode
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("address2_shippingmethodcode");
            }
            set
            {
                SetAttributeValue("address2_shippingmethodcode", value);
            }
        }

        /// <summary>
		/// Type the state or province of the secondary address.
		/// </summary>
        [AttributeLogicalName("address2_stateorprovince")]
        public string? Address2StateOrProvince
        {
            get
            {
                return GetAttributeValue<string?>("address2_stateorprovince");
            }
            set
            {
                SetAttributeValue("address2_stateorprovince", value);
            }
        }

        /// <summary>
		/// Type the main phone number associated with the secondary address.
		/// </summary>
        [AttributeLogicalName("address2_telephone1")]
        public string? Address2Telephone1
        {
            get
            {
                return GetAttributeValue<string?>("address2_telephone1");
            }
            set
            {
                SetAttributeValue("address2_telephone1", value);
            }
        }

        /// <summary>
		/// Type a second phone number associated with the secondary address.
		/// </summary>
        [AttributeLogicalName("address2_telephone2")]
        public string? Address2Telephone2
        {
            get
            {
                return GetAttributeValue<string?>("address2_telephone2");
            }
            set
            {
                SetAttributeValue("address2_telephone2", value);
            }
        }

        /// <summary>
		/// Type a third phone number associated with the secondary address.
		/// </summary>
        [AttributeLogicalName("address2_telephone3")]
        public string? Address2Telephone3
        {
            get
            {
                return GetAttributeValue<string?>("address2_telephone3");
            }
            set
            {
                SetAttributeValue("address2_telephone3", value);
            }
        }

        /// <summary>
		/// Type the UPS zone of the secondary address to make sure shipping charges are calculated correctly and deliveries are made promptly, if shipped by UPS.
		/// </summary>
        [AttributeLogicalName("address2_upszone")]
        public string? Address2UPSZone
        {
            get
            {
                return GetAttributeValue<string?>("address2_upszone");
            }
            set
            {
                SetAttributeValue("address2_upszone", value);
            }
        }

        /// <summary>
		/// Select the time zone, or UTC offset, for this address so that other people can reference it when they contact someone at this address.
		/// </summary>
        [AttributeLogicalName("address2_utcoffset")]
        public int? Address2UTCOffset
        {
            get
            {
                return GetAttributeValue<int?>("address2_utcoffset");
            }
            set
            {
                SetAttributeValue("address2_utcoffset", value);
            }
        }

        
        [AttributeLogicalName("adx_createdbyipaddress")]
        public string? AdxCreatedByIPAddress
        {
            get
            {
                return GetAttributeValue<string?>("adx_createdbyipaddress");
            }
            set
            {
                SetAttributeValue("adx_createdbyipaddress", value);
            }
        }

        
        [AttributeLogicalName("adx_createdbyusername")]
        public string? AdxCreatedByUsername
        {
            get
            {
                return GetAttributeValue<string?>("adx_createdbyusername");
            }
            set
            {
                SetAttributeValue("adx_createdbyusername", value);
            }
        }

        
        [AttributeLogicalName("adx_modifiedbyipaddress")]
        public string? AdxModifiedByIPAddress
        {
            get
            {
                return GetAttributeValue<string?>("adx_modifiedbyipaddress");
            }
            set
            {
                SetAttributeValue("adx_modifiedbyipaddress", value);
            }
        }

        
        [AttributeLogicalName("adx_modifiedbyusername")]
        public string? AdxModifiedByUsername
        {
            get
            {
                return GetAttributeValue<string?>("adx_modifiedbyusername");
            }
            set
            {
                SetAttributeValue("adx_modifiedbyusername", value);
            }
        }

        /// <summary>
		/// For system use only.
		/// </summary>
        [AttributeLogicalName("aging30")]
        public Money? Aging30
        {
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
            get
            {
                return GetAttributeValue<OptionSetValue?>("businesstypecode");
            }
            set
            {
                SetAttributeValue("businesstypecode", value);
            }
        }

        /// <summary>
		/// Shows who created the record.
		/// </summary>
        [AttributeLogicalName("createdby")]
        public EntityReference? CreatedBy
        {
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
            get
            {
                return GetAttributeValue<Money?>("creditlimit");
            }
            set
            {
                SetAttributeValue("creditlimit", value);
            }
        }

        /// <summary>
		/// Shows the credit limit converted to the system's default base currency for reporting purposes.
		/// </summary>
        [AttributeLogicalName("creditlimit_base")]
        public Money? CreditLimitBase
        {
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
            get
            {
                return GetAttributeValue<bool?>("creditonhold");
            }
            set
            {
                SetAttributeValue("creditonhold", value);
            }
        }

        /// <summary>
		/// Select the size category or range of the account for segmentation and reporting purposes.
		/// </summary>
        [AttributeLogicalName("customersizecode")]
        public OptionSetValue? CustomerSizeCode
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("customersizecode");
            }
            set
            {
                SetAttributeValue("customersizecode", value);
            }
        }

        /// <summary>
		/// Select the category that best describes the relationship between the account and your organization.
		/// </summary>
        [AttributeLogicalName("customertypecode")]
        public OptionSetValue? CustomerTypeCode
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("customertypecode");
            }
            set
            {
                SetAttributeValue("customertypecode", value);
            }
        }

        /// <summary>
		/// Type additional information to describe the account, such as an excerpt from the company's website.
		/// </summary>
        [AttributeLogicalName("description")]
        public string? Description
        {
            get
            {
                return GetAttributeValue<string?>("description");
            }
            set
            {
                SetAttributeValue("description", value);
            }
        }

        /// <summary>
		/// Select whether the account allows bulk email sent through campaigns. If Do Not Allow is selected, the account can be added to marketing lists, but is excluded from email.
		/// </summary>
        [AttributeLogicalName("donotbulkemail")]
        public bool? DoNotBulkEMail
        {
            get
            {
                return GetAttributeValue<bool?>("donotbulkemail");
            }
            set
            {
                SetAttributeValue("donotbulkemail", value);
            }
        }

        /// <summary>
		/// Select whether the account allows bulk postal mail sent through marketing campaigns or quick campaigns. If Do Not Allow is selected, the account can be added to marketing lists, but will be excluded from the postal mail.
		/// </summary>
        [AttributeLogicalName("donotbulkpostalmail")]
        public bool? DoNotBulkPostalMail
        {
            get
            {
                return GetAttributeValue<bool?>("donotbulkpostalmail");
            }
            set
            {
                SetAttributeValue("donotbulkpostalmail", value);
            }
        }

        /// <summary>
		/// Select whether the account allows direct email sent from Microsoft Dynamics 365.
		/// </summary>
        [AttributeLogicalName("donotemail")]
        public bool? DoNotEMail
        {
            get
            {
                return GetAttributeValue<bool?>("donotemail");
            }
            set
            {
                SetAttributeValue("donotemail", value);
            }
        }

        /// <summary>
		/// Select whether the account allows faxes. If Do Not Allow is selected, the account will be excluded from fax activities distributed in marketing campaigns.
		/// </summary>
        [AttributeLogicalName("donotfax")]
        public bool? DoNotFax
        {
            get
            {
                return GetAttributeValue<bool?>("donotfax");
            }
            set
            {
                SetAttributeValue("donotfax", value);
            }
        }

        /// <summary>
		/// Select whether the account allows phone calls. If Do Not Allow is selected, the account will be excluded from phone call activities distributed in marketing campaigns.
		/// </summary>
        [AttributeLogicalName("donotphone")]
        public bool? DoNotPhone
        {
            get
            {
                return GetAttributeValue<bool?>("donotphone");
            }
            set
            {
                SetAttributeValue("donotphone", value);
            }
        }

        /// <summary>
		/// Select whether the account allows direct mail. If Do Not Allow is selected, the account will be excluded from letter activities distributed in marketing campaigns.
		/// </summary>
        [AttributeLogicalName("donotpostalmail")]
        public bool? DoNotPostalMail
        {
            get
            {
                return GetAttributeValue<bool?>("donotpostalmail");
            }
            set
            {
                SetAttributeValue("donotpostalmail", value);
            }
        }

        /// <summary>
		/// Select whether the account accepts marketing materials, such as brochures or catalogs.
		/// </summary>
        [AttributeLogicalName("donotsendmm")]
        public bool? DoNotSendMM
        {
            get
            {
                return GetAttributeValue<bool?>("donotsendmm");
            }
            set
            {
                SetAttributeValue("donotsendmm", value);
            }
        }

        /// <summary>
		/// Type the primary email address for the account.
		/// </summary>
        [AttributeLogicalName("emailaddress1")]
        public string? EMailAddress1
        {
            get
            {
                return GetAttributeValue<string?>("emailaddress1");
            }
            set
            {
                SetAttributeValue("emailaddress1", value);
            }
        }

        /// <summary>
		/// Type the secondary email address for the account.
		/// </summary>
        [AttributeLogicalName("emailaddress2")]
        public string? EMailAddress2
        {
            get
            {
                return GetAttributeValue<string?>("emailaddress2");
            }
            set
            {
                SetAttributeValue("emailaddress2", value);
            }
        }

        /// <summary>
		/// Type an alternate email address for the account.
		/// </summary>
        [AttributeLogicalName("emailaddress3")]
        public string? EMailAddress3
        {
            get
            {
                return GetAttributeValue<string?>("emailaddress3");
            }
            set
            {
                SetAttributeValue("emailaddress3", value);
            }
        }

        /// <summary>
		/// Shows the default image for the record.
		/// </summary>
        [AttributeLogicalName("entityimage")]
        public byte[]? EntityImage
        {
            get
            {
                return GetAttributeValue<byte[]?>("entityimage");
            }
            set
            {
                SetAttributeValue("entityimage", value);
            }
        }

        
        [AttributeLogicalName("entityimage_timestamp")]
        public long? EntityImageTimestamp
        {
            get
            {
                return GetAttributeValue<long?>("entityimage_timestamp");
            }
        }

        
        [AttributeLogicalName("entityimage_url")]
        public string? EntityImageURL
        {
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
            get
            {
                return GetAttributeValue<string?>("fax");
            }
            set
            {
                SetAttributeValue("fax", value);
            }
        }

        /// <summary>
		/// Information about whether to allow following email activity like opens, attachment views and link clicks for emails sent to the account.
		/// </summary>
        [AttributeLogicalName("followemail")]
        public bool? FollowEmail
        {
            get
            {
                return GetAttributeValue<bool?>("followemail");
            }
            set
            {
                SetAttributeValue("followemail", value);
            }
        }

        /// <summary>
		/// Type the URL for the account's FTP site to enable users to access data and share documents.
		/// </summary>
        [AttributeLogicalName("ftpsiteurl")]
        public string? FtpSiteURL
        {
            get
            {
                return GetAttributeValue<string?>("ftpsiteurl");
            }
            set
            {
                SetAttributeValue("ftpsiteurl", value);
            }
        }

        /// <summary>
		/// Unique identifier of the data import or data migration that created this record.
		/// </summary>
        [AttributeLogicalName("importsequencenumber")]
        public int? ImportSequenceNumber
        {
            get
            {
                return GetAttributeValue<int?>("importsequencenumber");
            }
            set
            {
                SetAttributeValue("importsequencenumber", value);
            }
        }

        /// <summary>
		/// Select the account's primary industry for use in marketing segmentation and demographic analysis.
		/// </summary>
        [AttributeLogicalName("industrycode")]
        public OptionSetValue? IndustryCode
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("industrycode");
            }
            set
            {
                SetAttributeValue("industrycode", value);
            }
        }

        /// <summary>
		/// Contains the date and time stamp of the last on hold time.
		/// </summary>
        [AttributeLogicalName("lastonholdtime")]
        public DateTime? LastOnHoldTime
        {
            get
            {
                return GetAttributeValue<DateTime?>("lastonholdtime");
            }
            set
            {
                SetAttributeValue("lastonholdtime", value);
            }
        }

        /// <summary>
		/// Shows the date when the account was last included in a marketing campaign or quick campaign.
		/// </summary>
        [AttributeLogicalName("lastusedincampaign")]
        public DateTime? LastUsedInCampaign
        {
            get
            {
                return GetAttributeValue<DateTime?>("lastusedincampaign");
            }
            set
            {
                SetAttributeValue("lastusedincampaign", value);
            }
        }

        /// <summary>
		/// Type the market capitalization of the account to identify the company's equity, used as an indicator in financial performance analysis.
		/// </summary>
        [AttributeLogicalName("marketcap")]
        public Money? MarketCap
        {
            get
            {
                return GetAttributeValue<Money?>("marketcap");
            }
            set
            {
                SetAttributeValue("marketcap", value);
            }
        }

        /// <summary>
		/// Shows the market capitalization converted to the system's default base currency.
		/// </summary>
        [AttributeLogicalName("marketcap_base")]
        public Money? MarketCapBase
        {
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
            get
            {
                return GetAttributeValue<bool?>("marketingonly");
            }
            set
            {
                SetAttributeValue("marketingonly", value);
            }
        }

        /// <summary>
		/// Shows the master account that the account was merged with.
		/// </summary>
        [AttributeLogicalName("masterid")]
        public EntityReference? MasterId
        {
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
            get
            {
                return GetAttributeValue<EntityReference?>("modifiedonbehalfby");
            }
        }

        /// <summary>
		/// Unique identifier for Account associated with Account.
		/// </summary>
        [AttributeLogicalName("msa_managingpartnerid")]
        public EntityReference? MsaManagingpartnerid
        {
            get
            {
                return GetAttributeValue<EntityReference?>("msa_managingpartnerid");
            }
            set
            {
                SetAttributeValue("msa_managingpartnerid", value);
            }
        }

        /// <summary>
		/// Type the company or business name.
		/// </summary>
        [AttributeLogicalName("name")]
        public string? Name
        {
            get
            {
                return GetAttributeValue<string?>("name");
            }
            set
            {
                SetAttributeValue("name", value);
            }
        }

        /// <summary>
		/// Type the number of employees that work at the account for use in marketing segmentation and demographic analysis.
		/// </summary>
        [AttributeLogicalName("numberofemployees")]
        public int? NumberOfEmployees
        {
            get
            {
                return GetAttributeValue<int?>("numberofemployees");
            }
            set
            {
                SetAttributeValue("numberofemployees", value);
            }
        }

        /// <summary>
		/// Shows how long, in minutes, that the record was on hold.
		/// </summary>
        [AttributeLogicalName("onholdtime")]
        public int? OnHoldTime
        {
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
            get
            {
                return GetAttributeValue<DateTime?>("overriddencreatedon");
            }
            set
            {
                SetAttributeValue("overriddencreatedon", value);
            }
        }

        /// <summary>
		/// Enter the user or team who is assigned to manage the record. This field is updated every time the record is assigned to a different user.
		/// </summary>
        [AttributeLogicalName("ownerid")]
        public EntityReference? OwnerId
        {
            get
            {
                return GetAttributeValue<EntityReference?>("ownerid");
            }
            set
            {
                SetAttributeValue("ownerid", value);
            }
        }

        /// <summary>
		/// Select the account's ownership structure, such as public or private.
		/// </summary>
        [AttributeLogicalName("ownershipcode")]
        public OptionSetValue? OwnershipCode
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("ownershipcode");
            }
            set
            {
                SetAttributeValue("ownershipcode", value);
            }
        }

        /// <summary>
		/// Shows the business unit that the record owner belongs to.
		/// </summary>
        [AttributeLogicalName("owningbusinessunit")]
        public EntityReference? OwningBusinessUnit
        {
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
            get
            {
                return GetAttributeValue<EntityReference?>("parentaccountid");
            }
            set
            {
                SetAttributeValue("parentaccountid", value);
            }
        }

        /// <summary>
		/// For system use only. Legacy Microsoft Dynamics CRM 3.0 workflow data.
		/// </summary>
        [AttributeLogicalName("participatesinworkflow")]
        public bool? ParticipatesInWorkflow
        {
            get
            {
                return GetAttributeValue<bool?>("participatesinworkflow");
            }
            set
            {
                SetAttributeValue("participatesinworkflow", value);
            }
        }

        /// <summary>
		/// Select the payment terms to indicate when the customer needs to pay the total amount.
		/// </summary>
        [AttributeLogicalName("paymenttermscode")]
        public OptionSetValue? PaymentTermsCode
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("paymenttermscode");
            }
            set
            {
                SetAttributeValue("paymenttermscode", value);
            }
        }

        /// <summary>
		/// Select the preferred day of the week for service appointments.
		/// </summary>
        [AttributeLogicalName("preferredappointmentdaycode")]
        public OptionSetValue? PreferredAppointmentDayCode
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("preferredappointmentdaycode");
            }
            set
            {
                SetAttributeValue("preferredappointmentdaycode", value);
            }
        }

        /// <summary>
		/// Select the preferred time of day for service appointments.
		/// </summary>
        [AttributeLogicalName("preferredappointmenttimecode")]
        public OptionSetValue? PreferredAppointmentTimeCode
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("preferredappointmenttimecode");
            }
            set
            {
                SetAttributeValue("preferredappointmenttimecode", value);
            }
        }

        /// <summary>
		/// Select the preferred method of contact.
		/// </summary>
        [AttributeLogicalName("preferredcontactmethodcode")]
        public OptionSetValue? PreferredContactMethodCode
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("preferredcontactmethodcode");
            }
            set
            {
                SetAttributeValue("preferredcontactmethodcode", value);
            }
        }

        /// <summary>
		/// Choose the preferred service representative for reference when you schedule service activities for the account.
		/// </summary>
        [AttributeLogicalName("preferredsystemuserid")]
        public EntityReference? PreferredSystemUserId
        {
            get
            {
                return GetAttributeValue<EntityReference?>("preferredsystemuserid");
            }
            set
            {
                SetAttributeValue("preferredsystemuserid", value);
            }
        }

        /// <summary>
		/// Choose the primary contact for the account to provide quick access to contact details.
		/// </summary>
        [AttributeLogicalName("primarycontactid")]
        public EntityReference? PrimaryContactId
        {
            get
            {
                return GetAttributeValue<EntityReference?>("primarycontactid");
            }
            set
            {
                SetAttributeValue("primarycontactid", value);
            }
        }

        /// <summary>
		/// Primary Satori ID for Account
		/// </summary>
        [AttributeLogicalName("primarysatoriid")]
        public string? PrimarySatoriId
        {
            get
            {
                return GetAttributeValue<string?>("primarysatoriid");
            }
            set
            {
                SetAttributeValue("primarysatoriid", value);
            }
        }

        /// <summary>
		/// Primary Twitter ID for Account
		/// </summary>
        [AttributeLogicalName("primarytwitterid")]
        public string? PrimaryTwitterId
        {
            get
            {
                return GetAttributeValue<string?>("primarytwitterid");
            }
            set
            {
                SetAttributeValue("primarytwitterid", value);
            }
        }

        /// <summary>
		/// Shows the ID of the process.
		/// </summary>
        [AttributeLogicalName("processid")]
        public Guid? ProcessId
        {
            get
            {
                return GetAttributeValue<Guid?>("processid");
            }
            set
            {
                SetAttributeValue("processid", value);
            }
        }

        /// <summary>
		/// Type the annual revenue for the account, used as an indicator in financial performance analysis.
		/// </summary>
        [AttributeLogicalName("revenue")]
        public Money? Revenue
        {
            get
            {
                return GetAttributeValue<Money?>("revenue");
            }
            set
            {
                SetAttributeValue("revenue", value);
            }
        }

        /// <summary>
		/// Shows the annual revenue converted to the system's default base currency. The calculations use the exchange rate specified in the Currencies area.
		/// </summary>
        [AttributeLogicalName("revenue_base")]
        public Money? RevenueBase
        {
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
            get
            {
                return GetAttributeValue<int?>("sharesoutstanding");
            }
            set
            {
                SetAttributeValue("sharesoutstanding", value);
            }
        }

        /// <summary>
		/// Select a shipping method for deliveries sent to the account's address to designate the preferred carrier or other delivery option.
		/// </summary>
        [AttributeLogicalName("shippingmethodcode")]
        public OptionSetValue? ShippingMethodCode
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("shippingmethodcode");
            }
            set
            {
                SetAttributeValue("shippingmethodcode", value);
            }
        }

        /// <summary>
		/// Type the Standard Industrial Classification (SIC) code that indicates the account's primary industry of business, for use in marketing segmentation and demographic analysis.
		/// </summary>
        [AttributeLogicalName("sic")]
        public string? SIC
        {
            get
            {
                return GetAttributeValue<string?>("sic");
            }
            set
            {
                SetAttributeValue("sic", value);
            }
        }

        /// <summary>
		/// Choose the service level agreement (SLA) that you want to apply to the Account record.
		/// </summary>
        [AttributeLogicalName("slaid")]
        public EntityReference? SLAId
        {
            get
            {
                return GetAttributeValue<EntityReference?>("slaid");
            }
            set
            {
                SetAttributeValue("slaid", value);
            }
        }

        /// <summary>
		/// Last SLA that was applied to this case. This field is for internal use only.
		/// </summary>
        [AttributeLogicalName("slainvokedid")]
        public EntityReference? SLAInvokedId
        {
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
            get
            {
                return GetAttributeValue<Guid?>("stageid");
            }
            set
            {
                SetAttributeValue("stageid", value);
            }
        }

        /// <summary>
		/// Shows whether the account is active or inactive. Inactive accounts are read-only and can't be edited unless they are reactivated.
		/// </summary>
        [AttributeLogicalName("statecode")]
        public OptionSetValue? StateCode
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("statecode");
            }
            set
            {
                SetAttributeValue("statecode", value);
            }
        }

        /// <summary>
		/// Select the account's status.
		/// </summary>
        [AttributeLogicalName("statuscode")]
        public OptionSetValue? StatusCode
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("statuscode");
            }
            set
            {
                SetAttributeValue("statuscode", value);
            }
        }

        /// <summary>
		/// Type the stock exchange at which the account is listed to track their stock and financial performance of the company.
		/// </summary>
        [AttributeLogicalName("stockexchange")]
        public string? StockExchange
        {
            get
            {
                return GetAttributeValue<string?>("stockexchange");
            }
            set
            {
                SetAttributeValue("stockexchange", value);
            }
        }

        /// <summary>
		/// Type the main phone number for this account.
		/// </summary>
        [AttributeLogicalName("telephone1")]
        public string? Telephone1
        {
            get
            {
                return GetAttributeValue<string?>("telephone1");
            }
            set
            {
                SetAttributeValue("telephone1", value);
            }
        }

        /// <summary>
		/// Type a second phone number for this account.
		/// </summary>
        [AttributeLogicalName("telephone2")]
        public string? Telephone2
        {
            get
            {
                return GetAttributeValue<string?>("telephone2");
            }
            set
            {
                SetAttributeValue("telephone2", value);
            }
        }

        /// <summary>
		/// Type a third phone number for this account.
		/// </summary>
        [AttributeLogicalName("telephone3")]
        public string? Telephone3
        {
            get
            {
                return GetAttributeValue<string?>("telephone3");
            }
            set
            {
                SetAttributeValue("telephone3", value);
            }
        }

        /// <summary>
		/// Select a region or territory for the account for use in segmentation and analysis.
		/// </summary>
        [AttributeLogicalName("territorycode")]
        public OptionSetValue? TerritoryCode
        {
            get
            {
                return GetAttributeValue<OptionSetValue?>("territorycode");
            }
            set
            {
                SetAttributeValue("territorycode", value);
            }
        }

        /// <summary>
		/// Type the stock exchange symbol for the account to track financial performance of the company. You can click the code entered in this field to access the latest trading information from MSN Money.
		/// </summary>
        [AttributeLogicalName("tickersymbol")]
        public string? TickerSymbol
        {
            get
            {
                return GetAttributeValue<string?>("tickersymbol");
            }
            set
            {
                SetAttributeValue("tickersymbol", value);
            }
        }

        /// <summary>
		/// Total time spent for emails (read and write) and meetings by me in relation to account record.
		/// </summary>
        [AttributeLogicalName("timespentbymeonemailandmeetings")]
        public string? TimeSpentByMeOnEmailAndMeetings
        {
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
            get
            {
                return GetAttributeValue<int?>("timezoneruleversionnumber");
            }
            set
            {
                SetAttributeValue("timezoneruleversionnumber", value);
            }
        }

        /// <summary>
		/// Choose the local currency for the record to make sure budgets are reported in the correct currency.
		/// </summary>
        [AttributeLogicalName("transactioncurrencyid")]
        public EntityReference? TransactionCurrencyId
        {
            get
            {
                return GetAttributeValue<EntityReference?>("transactioncurrencyid");
            }
            set
            {
                SetAttributeValue("transactioncurrencyid", value);
            }
        }

        /// <summary>
		/// For internal use only.
		/// </summary>
        [AttributeLogicalName("traversedpath")]
        public string? TraversedPath
        {
            get
            {
                return GetAttributeValue<string?>("traversedpath");
            }
            set
            {
                SetAttributeValue("traversedpath", value);
            }
        }

        /// <summary>
		/// Time zone code that was in use when the record was created.
		/// </summary>
        [AttributeLogicalName("utcconversiontimezonecode")]
        public int? UTCConversionTimeZoneCode
        {
            get
            {
                return GetAttributeValue<int?>("utcconversiontimezonecode");
            }
            set
            {
                SetAttributeValue("utcconversiontimezonecode", value);
            }
        }

        /// <summary>
		/// Version number of the account.
		/// </summary>
        [AttributeLogicalName("versionnumber")]
        public long? VersionNumber
        {
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
            get
            {
                return GetAttributeValue<string?>("websiteurl");
            }
            set
            {
                SetAttributeValue("websiteurl", value);
            }
        }

        /// <summary>
		/// Type the phonetic spelling of the company name, if specified in Japanese, to make sure the name is pronounced correctly in phone calls and other communications.
		/// </summary>
        [AttributeLogicalName("yominame")]
        public string? YomiName
        {
            get
            {
                return GetAttributeValue<string?>("yominame");
            }
            set
            {
                SetAttributeValue("yominame", value);
            }
        }
        #endregion

        #region NavigationProperties

        /// <summary>
        /// 1:N Account_AsyncOperations
        /// </summary>
        [RelationshipSchemaName("Account_AsyncOperations")]
        public IEnumerable<AsyncOperation> AccountAsyncOperations
        {
            get
            {
                return GetRelatedEntities<AsyncOperation>("Account_AsyncOperations", null);
            }
            set
            {
                SetRelatedEntities("Account_AsyncOperations", null, value);
            }
        }

        /// <summary>
        /// 1:N account_master_account
        /// </summary>
        [RelationshipSchemaName("account_master_account")]
        public IEnumerable<Account> AccountMasterAccount
        {
            get
            {
                return GetRelatedEntities<Account>("account_master_account", null);
            }
            set
            {
                SetRelatedEntities("account_master_account", null, value);
            }
        }

        /// <summary>
        /// 1:N account_parent_account
        /// </summary>
        [RelationshipSchemaName("account_parent_account")]
        public IEnumerable<Account> AccountParentAccount
        {
            get
            {
                return GetRelatedEntities<Account>("account_parent_account", null);
            }
            set
            {
                SetRelatedEntities("account_parent_account", null, value);
            }
        }

        /// <summary>
        /// 1:N contact_customer_accounts
        /// </summary>
        [RelationshipSchemaName("contact_customer_accounts")]
        public IEnumerable<Contact> ContactCustomerAccounts
        {
            get
            {
                return GetRelatedEntities<Contact>("contact_customer_accounts", null);
            }
            set
            {
                SetRelatedEntities("contact_customer_accounts", null, value);
            }
        }

        /// <summary>
        /// 1:N msa_account_managingpartner
        /// </summary>
        [RelationshipSchemaName("msa_account_managingpartner")]
        public IEnumerable<Account> MsaAccountManagingpartner
        {
            get
            {
                return GetRelatedEntities<Account>("msa_account_managingpartner", null);
            }
            set
            {
                SetRelatedEntities("msa_account_managingpartner", null, value);
            }
        }

        /// <summary>
        /// 1:N msa_contact_managingpartner
        /// </summary>
        [RelationshipSchemaName("msa_contact_managingpartner")]
        public IEnumerable<Contact> MsaContactManagingpartner
        {
            get
            {
                return GetRelatedEntities<Contact>("msa_contact_managingpartner", null);
            }
            set
            {
                SetRelatedEntities("msa_contact_managingpartner", null, value);
            }
        }
        #endregion

        #region Options
        public static partial class Options
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
                public const int DesignDirectionAndCreativeManagement = 9;
                public const int DistributorsDispatchersAndProcessors = 10;
                public const int DoctorSOfficesAndClinics = 11;
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
            public const string AdxCreatedByIPAddress = "adx_createdbyipaddress";
            public const string AdxCreatedByUsername = "adx_createdbyusername";
            public const string AdxModifiedByIPAddress = "adx_modifiedbyipaddress";
            public const string AdxModifiedByUsername = "adx_modifiedbyusername";
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
            public const string MsaManagingpartnerid = "msa_managingpartnerid";
            public const string Name = "name";
            public const string NumberOfEmployees = "numberofemployees";
            public const string OnHoldTime = "onholdtime";
            public const string OverriddenCreatedOn = "overriddencreatedon";
            public const string OwnerId = "ownerid";
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
            public const string Telephone1 = "telephone1";
            public const string Telephone2 = "telephone2";
            public const string Telephone3 = "telephone3";
            public const string TerritoryCode = "territorycode";
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
        public static partial class Relations
        {
            public static class OneToMany
            {
                public const string AccountActioncard = "account_actioncard";
                public const string AccountActivityParties = "account_activity_parties";
                public const string AccountActivityPointers = "Account_ActivityPointers";
                public const string AccountAdxInviteredemptions = "account_adx_inviteredemptions";
                public const string AccountAdxPortalcomments = "account_adx_portalcomments";
                public const string AccountAnnotation = "Account_Annotation";
                public const string AccountAppointments = "Account_Appointments";
                public const string AccountAsyncOperations = "Account_AsyncOperations";
                public const string AccountBulkDeleteFailures = "Account_BulkDeleteFailures";
                public const string AccountChats = "account_chats";
                public const string AccountConnections1 = "account_connections1";
                public const string AccountConnections2 = "account_connections2";
                public const string AccountCustomerRelationshipCustomer = "account_customer_relationship_customer";
                public const string AccountCustomerRelationshipPartner = "account_customer_relationship_partner";
                public const string AccountCustomerAddress = "Account_CustomerAddress";
                public const string AccountDeletedItemReferences = "account_DeletedItemReferences";
                public const string AccountDuplicateBaseRecord = "Account_DuplicateBaseRecord";
                public const string AccountDuplicateMatchingRecord = "Account_DuplicateMatchingRecord";
                public const string AccountEmailEmailSender = "Account_Email_EmailSender";
                public const string AccountEmailSendersAccount = "Account_Email_SendersAccount";
                public const string AccountEmails = "Account_Emails";
                public const string AccountFaxes = "Account_Faxes";
                public const string AccountLetters = "Account_Letters";
                public const string AccountMailboxTrackingFolder = "Account_MailboxTrackingFolder";
                public const string AccountMasterAccount = "account_master_account";
                public const string AccountParentAccount = "account_parent_account";
                public const string AccountPhonecalls = "Account_Phonecalls";
                public const string AccountPostFollows = "account_PostFollows";
                public const string AccountPostRegardings = "account_PostRegardings";
                public const string AccountPostRoles = "account_PostRoles";
                public const string AccountPrincipalobjectattributeaccess = "account_principalobjectattributeaccess";
                public const string AccountProcessSessions = "Account_ProcessSessions";
                public const string AccountRecurringAppointmentMasters = "Account_RecurringAppointmentMasters";
                public const string AccountSharepointDocument = "Account_SharepointDocument";
                public const string AccountSharepointDocumentLocation = "Account_SharepointDocumentLocation";
                public const string AccountSocialActivities = "Account_SocialActivities";
                public const string AccountSyncErrors = "Account_SyncErrors";
                public const string AccountTasks = "Account_Tasks";
                public const string AdxInvitationAssigntoaccount = "adx_invitation_assigntoaccount";
                public const string ContactCustomerAccounts = "contact_customer_accounts";
                public const string MsaAccountManagingpartner = "msa_account_managingpartner";
                public const string MsaContactManagingpartner = "msa_contact_managingpartner";
                public const string SlakpiinstanceAccount = "slakpiinstance_account";
                public const string SocialActivityPostAuthorAccounts = "SocialActivity_PostAuthor_accounts";
                public const string SocialActivityPostAuthorAccountAccounts = "SocialActivity_PostAuthorAccount_accounts";
                public const string SocialprofileCustomerAccounts = "Socialprofile_customer_accounts";
                public const string UserentityinstancedataAccount = "userentityinstancedata_account";
            }

            public static partial class ManyToOne
            {
                public const string AccountMasterAccount = "account_master_account";
                public const string AccountParentAccount = "account_parent_account";
                public const string AccountPrimaryContact = "account_primary_contact";
                public const string BusinessUnitAccounts = "business_unit_accounts";
                public const string LkAccountEntityimage = "lk_account_entityimage";
                public const string LkAccountbaseCreatedby = "lk_accountbase_createdby";
                public const string LkAccountbaseCreatedonbehalfby = "lk_accountbase_createdonbehalfby";
                public const string LkAccountbaseModifiedby = "lk_accountbase_modifiedby";
                public const string LkAccountbaseModifiedonbehalfby = "lk_accountbase_modifiedonbehalfby";
                public const string LkExternalpartyAccountCreatedby = "lk_externalparty_account_createdby";
                public const string LkExternalpartyAccountModifiedby = "lk_externalparty_account_modifiedby";
                public const string ManualslaAccount = "manualsla_account";
                public const string MsaAccountManagingpartner = "msa_account_managingpartner";
                public const string OwnerAccounts = "owner_accounts";
                public const string ProcessstageAccount = "processstage_account";
                public const string SlaAccount = "sla_account";
                public const string SystemUserAccounts = "system_user_accounts";
                public const string TeamAccounts = "team_accounts";
                public const string TransactioncurrencyAccount = "transactioncurrency_account";
                public const string UserAccounts = "user_accounts";
            }

            public static partial class ManyToMany
            {
                public const string PowerpagecomponentMsppWebroleAccount = "powerpagecomponent_mspp_webrole_account";
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
