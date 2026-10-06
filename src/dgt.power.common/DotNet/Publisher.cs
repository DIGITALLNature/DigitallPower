using System.Diagnostics.CodeAnalysis;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System.Runtime.Serialization;

// ReSharper disable All
namespace dgt.power.dataverse
{
    /// <inheritdoc cref="Microsoft.Xrm.Sdk.Entity" />
    /// <summary>
	/// A publisher of a CRM solution.
	/// </summary>
    [EntityLogicalName("publisher")]
    [System.CodeDom.Compiler.GeneratedCode("dgtp", "2026")]
    [ExcludeFromCodeCoverage]
    [SuppressMessage("Design", "CA1034")]
    [SuppressMessage("Performance", "CA1815")]
    public partial class Publisher : Entity
    {
        #region ctor
        public Publisher() : base(EntityLogicalName) { }

        public Publisher(Guid id) : base(EntityLogicalName, id) { }

        public Publisher(KeyAttributeCollection keyAttributes) : base(EntityLogicalName, keyAttributes) { }

        public Publisher(string keyName, object keyValue) : base(EntityLogicalName, keyName, keyValue) { }
        #endregion

        #region consts
        public const string EntityLogicalName = "publisher";
        public const string PrimaryNameAttribute = "friendlyname";
        public const int EntityTypeCode = 7101;
        #endregion

        #region Attributes
        [AttributeLogicalName("publisherid")]
        [IgnoreDataMember]
        public new Guid Id
        {
            get
            {
                return base.Id;
            }
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
		/// Unique identifier of the publisher.
		/// </summary>
        [AttributeLogicalName("publisherid")]
        public Guid? PublisherId
        {
            get
            {
                return GetAttributeValue<Guid?>("publisherid");
            }
            set
            {
                SetAttributeValue("publisherid", value);
                base.Id = value.HasValue ? value.Value : Guid.Empty;
            }
        }

        /// <summary>
		/// Type of address for address 1, such as billing, shipping, or primary address.
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
		/// City name for address 1.
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
		/// Country/region name for address 1.
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
		/// County name for address 1.
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
		/// Fax number for address 1.
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
		/// Latitude for address 1.
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
		/// First line for entering address 1 information.
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
		/// Second line for entering address 1 information.
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
		/// Third line for entering address 1 information.
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
		/// Longitude for address 1.
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
		/// Name to enter for address 1.
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
		/// ZIP Code or postal code for address 1.
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
		/// Post office box number for address 1.
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
		/// Method of shipment for address 1.
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
		/// State or province for address 1.
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
		/// First telephone number associated with address 1.
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
		/// Second telephone number associated with address 1.
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
		/// Third telephone number associated with address 1.
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
		/// United Parcel Service (UPS) zone for address 1.
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
		/// UTC offset for address 1. This is the difference between local time and standard Coordinated Universal Time.
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
		/// Type of address for address 2. such as billing, shipping, or primary address.
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
		/// City name for address 2.
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
		/// Country/region name for address 2.
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
		/// County name for address 2.
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
		/// Fax number for address 2.
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
		/// Latitude for address 2.
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
		/// First line for entering address 2 information.
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
		/// Second line for entering address 2 information.
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
		/// Third line for entering address 2 information.
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
		/// Longitude for address 2.
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
		/// Name to enter for address 2.
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
		/// ZIP Code or postal code for address 2.
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
		/// Post office box number for address 2.
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
		/// Method of shipment for address 2.
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
		/// State or province for address 2.
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
		/// First telephone number associated with address 2.
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
		/// Second telephone number associated with address 2.
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
		/// Third telephone number associated with address 2.
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
		/// United Parcel Service (UPS) zone for address 2.
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
		/// UTC offset for address 2. This is the difference between local time and standard Coordinated Universal Time.
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

        /// <summary>
		/// Unique identifier of the user who created the publisher.
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
		/// Date and time when the publisher was created.
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
		/// Unique identifier of the delegate user who created the publisher.
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
		/// Default option value prefix used for newly created options for solutions associated with this publisher.
		/// </summary>
        [AttributeLogicalName("customizationoptionvalueprefix")]
        public int? CustomizationOptionValuePrefix
        {
            get
            {
                return GetAttributeValue<int?>("customizationoptionvalueprefix");
            }
            set
            {
                SetAttributeValue("customizationoptionvalueprefix", value);
            }
        }

        /// <summary>
		/// Prefix used for new entities, attributes, and entity relationships for solutions associated with this publisher.
		/// </summary>
        [AttributeLogicalName("customizationprefix")]
        public string? CustomizationPrefix
        {
            get
            {
                return GetAttributeValue<string?>("customizationprefix");
            }
            set
            {
                SetAttributeValue("customizationprefix", value);
            }
        }

        /// <summary>
		/// Description of the solution.
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
		/// Email address for the publisher.
		/// </summary>
        [AttributeLogicalName("emailaddress")]
        public string? EMailAddress
        {
            get
            {
                return GetAttributeValue<string?>("emailaddress");
            }
            set
            {
                SetAttributeValue("emailaddress", value);
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
		/// User display name for this publisher.
		/// </summary>
        [AttributeLogicalName("friendlyname")]
        public string? FriendlyName
        {
            get
            {
                return GetAttributeValue<string?>("friendlyname");
            }
            set
            {
                SetAttributeValue("friendlyname", value);
            }
        }

        /// <summary>
		/// Indicates whether the publisher was created as part of a managed solution installation.
		/// </summary>
        [AttributeLogicalName("isreadonly")]
        public bool? IsReadonly
        {
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
            get
            {
                return GetAttributeValue<string?>("supportingwebsiteurl");
            }
            set
            {
                SetAttributeValue("supportingwebsiteurl", value);
            }
        }

        /// <summary>
		/// The unique name of this publisher.
		/// </summary>
        [AttributeLogicalName("uniquename")]
        public string? UniqueName
        {
            get
            {
                return GetAttributeValue<string?>("uniquename");
            }
            set
            {
                SetAttributeValue("uniquename", value);
            }
        }

        
        [AttributeLogicalName("versionnumber")]
        public long? VersionNumber
        {
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
        [RelationshipSchemaName("publisher_solution")]
        public IEnumerable<Solution> PublisherSolution
        {
            get
            {
                return GetRelatedEntities<Solution>("publisher_solution", null);
            }
            set
            {
                SetRelatedEntities("publisher_solution", null, value);
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
            public struct IsReadonly
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
        public static partial class Relations
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

            public static partial class ManyToOne
            {
                public const string LkPublisherCreatedby = "lk_publisher_createdby";
                public const string LkPublisherEntityimage = "lk_publisher_entityimage";
                public const string LkPublisherModifiedby = "lk_publisher_modifiedby";
                public const string LkPublisherbaseCreatedonbehalfby = "lk_publisherbase_createdonbehalfby";
                public const string LkPublisherbaseModifiedonbehalfby = "lk_publisherbase_modifiedonbehalfby";
                public const string OrganizationPublisher = "organization_publisher";
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
