/* eslint-disable */
///<reference path="""../../Typings/Xrm/index.d.ts""" />

// Entity Account FormContext
export interface AccountFormContext extends Xrm.FormContext {
    getAttribute(): Xrm.Attributes.Attribute[];
    getAttribute<T extends Xrm.Attributes.Attribute>(attributeName: string): T;
    getAttribute(attributeName: string): Xrm.Attributes.Attribute;
    getAttribute(index: number): Xrm.Attributes.Attribute;

    getControl(): Xrm.Controls.Control[];
    getControl<T extends Xrm.Controls.Control>(controlName: string): T;
    getControl(controlName: string): Xrm.Controls.Control;
    getControl(index: number): Xrm.Controls.Control;

    /// <summary>
    /// Select a category to indicate whether the customer account is standard or preferred.
    /// </summary>
    getAttribute(name: 'accountcategorycode'): Xrm.Attributes.OptionSetAttribute;
    /// <summary>
    /// Select a category to indicate whether the customer account is standard or preferred.
    /// </summary>
    getControl(name: 'accountcategorycode'): Xrm.Controls.OptionSetAttribute;
    /// <summary>
    /// Select a classification code to indicate the potential value of the customer account based on the projected return on investment, cooperation level, sales cycle length or other criteria.
    /// </summary>
    getAttribute(name: 'accountclassificationcode'): Xrm.Attributes.OptionSetAttribute;
    /// <summary>
    /// Select a classification code to indicate the potential value of the customer account based on the projected return on investment, cooperation level, sales cycle length or other criteria.
    /// </summary>
    getControl(name: 'accountclassificationcode'): Xrm.Controls.OptionSetAttribute;
    /// <summary>
    /// Unique identifier of the account.
    /// </summary>
    getAttribute(name: 'accountid'): Xrm.Attributes.Attribute;
    /// <summary>
    /// Unique identifier of the account.
    /// </summary>
    getControl(name: 'accountid'): Xrm.Controls.Attribute;
    /// <summary>
    /// Type an ID number or code for the account to quickly search and identify the account in system views.
    /// </summary>
    getAttribute(name: 'accountnumber'): Xrm.Attributes.StringAttribute;
    /// <summary>
    /// Type an ID number or code for the account to quickly search and identify the account in system views.
    /// </summary>
    getControl(name: 'accountnumber'): Xrm.Controls.StringAttribute;
    /// <summary>
    /// Select a rating to indicate the value of the customer account.
    /// </summary>
    getAttribute(name: 'accountratingcode'): Xrm.Attributes.OptionSetAttribute;
    /// <summary>
    /// Select a rating to indicate the value of the customer account.
    /// </summary>
    getControl(name: 'accountratingcode'): Xrm.Controls.OptionSetAttribute;
    /// <summary>
    /// Unique identifier for address 1.
    /// </summary>
    getAttribute(name: 'address1_addressid'): Xrm.Attributes.Attribute;
    /// <summary>
    /// Unique identifier for address 1.
    /// </summary>
    getControl(name: 'address1_addressid'): Xrm.Controls.Attribute;
    /// <summary>
    /// Select the primary address type.
    /// </summary>
    getAttribute(name: 'address1_addresstypecode'): Xrm.Attributes.OptionSetAttribute;
    /// <summary>
    /// Select the primary address type.
    /// </summary>
    getControl(name: 'address1_addresstypecode'): Xrm.Controls.OptionSetAttribute;
    /// <summary>
    /// Type the city for the primary address.
    /// </summary>
    getAttribute(name: 'address1_city'): Xrm.Attributes.StringAttribute;
    /// <summary>
    /// Type the city for the primary address.
    /// </summary>
    getControl(name: 'address1_city'): Xrm.Controls.StringAttribute;
    /// <summary>
    /// Shows the complete primary address.
    /// </summary>
    getAttribute(name: 'address1_composite'): Xrm.Attributes.Attribute;
    /// <summary>
    /// Shows the complete primary address.
    /// </summary>
    getControl(name: 'address1_composite'): Xrm.Controls.Attribute;
    /// <summary>
    /// Type the country or region for the primary address.
    /// </summary>
    getAttribute(name: 'address1_country'): Xrm.Attributes.StringAttribute;
    /// <summary>
    /// Type the country or region for the primary address.
    /// </summary>
    getControl(name: 'address1_country'): Xrm.Controls.StringAttribute;
    /// <summary>
    /// Type the county for the primary address.
    /// </summary>
    getAttribute(name: 'address1_county'): Xrm.Attributes.StringAttribute;
    /// <summary>
    /// Type the county for the primary address.
    /// </summary>
    getControl(name: 'address1_county'): Xrm.Controls.StringAttribute;
    /// <summary>
    /// Type the fax number associated with the primary address.
    /// </summary>
    getAttribute(name: 'address1_fax'): Xrm.Attributes.StringAttribute;
    /// <summary>
    /// Type the fax number associated with the primary address.
    /// </summary>
    getControl(name: 'address1_fax'): Xrm.Controls.StringAttribute;
    /// <summary>
    /// Select the freight terms for the primary address to make sure shipping orders are processed correctly.
    /// </summary>
    getAttribute(name: 'address1_freighttermscode'): Xrm.Attributes.OptionSetAttribute;
    /// <summary>
    /// Select the freight terms for the primary address to make sure shipping orders are processed correctly.
    /// </summary>
    getControl(name: 'address1_freighttermscode'): Xrm.Controls.OptionSetAttribute;
    /// <summary>
    /// Type the latitude value for the primary address for use in mapping and other applications.
    /// </summary>
    getAttribute(name: 'address1_latitude'): Xrm.Attributes.NumberAttribute;
    /// <summary>
    /// Type the latitude value for the primary address for use in mapping and other applications.
    /// </summary>
    getControl(name: 'address1_latitude'): Xrm.Controls.NumberAttribute;
    /// <summary>
    /// Type the first line of the primary address.
    /// </summary>
    getAttribute(name: 'address1_line1'): Xrm.Attributes.StringAttribute;
    /// <summary>
    /// Type the first line of the primary address.
    /// </summary>
    getControl(name: 'address1_line1'): Xrm.Controls.StringAttribute;
    /// <summary>
    /// Type the second line of the primary address.
    /// </summary>
    getAttribute(name: 'address1_line2'): Xrm.Attributes.StringAttribute;
    /// <summary>
    /// Type the second line of the primary address.
    /// </summary>
    getControl(name: 'address1_line2'): Xrm.Controls.StringAttribute;
    /// <summary>
    /// Type the third line of the primary address.
    /// </summary>
    getAttribute(name: 'address1_line3'): Xrm.Attributes.StringAttribute;
    /// <summary>
    /// Type the third line of the primary address.
    /// </summary>
    getControl(name: 'address1_line3'): Xrm.Controls.StringAttribute;
    /// <summary>
    /// Type the longitude value for the primary address for use in mapping and other applications.
    /// </summary>
    getAttribute(name: 'address1_longitude'): Xrm.Attributes.NumberAttribute;
    /// <summary>
    /// Type the longitude value for the primary address for use in mapping and other applications.
    /// </summary>
    getControl(name: 'address1_longitude'): Xrm.Controls.NumberAttribute;
    /// <summary>
    /// Type a descriptive name for the primary address, such as Corporate Headquarters.
    /// </summary>
    getAttribute(name: 'address1_name'): Xrm.Attributes.StringAttribute;
    /// <summary>
    /// Type a descriptive name for the primary address, such as Corporate Headquarters.
    /// </summary>
    getControl(name: 'address1_name'): Xrm.Controls.StringAttribute;
    /// <summary>
    /// Type the ZIP Code or postal code for the primary address.
    /// </summary>
    getAttribute(name: 'address1_postalcode'): Xrm.Attributes.StringAttribute;
    /// <summary>
    /// Type the ZIP Code or postal code for the primary address.
    /// </summary>
    getControl(name: 'address1_postalcode'): Xrm.Controls.StringAttribute;
    /// <summary>
    /// Type the post office box number of the primary address.
    /// </summary>
    getAttribute(name: 'address1_postofficebox'): Xrm.Attributes.StringAttribute;
    /// <summary>
    /// Type the post office box number of the primary address.
    /// </summary>
    getControl(name: 'address1_postofficebox'): Xrm.Controls.StringAttribute;
    /// <summary>
    /// Type the name of the main contact at the account's primary address.
    /// </summary>
    getAttribute(name: 'address1_primarycontactname'): Xrm.Attributes.StringAttribute;
    /// <summary>
    /// Type the name of the main contact at the account's primary address.
    /// </summary>
    getControl(name: 'address1_primarycontactname'): Xrm.Controls.StringAttribute;
    /// <summary>
    /// Select a shipping method for deliveries sent to this address.
    /// </summary>
    getAttribute(name: 'address1_shippingmethodcode'): Xrm.Attributes.OptionSetAttribute;
    /// <summary>
    /// Select a shipping method for deliveries sent to this address.
    /// </summary>
    getControl(name: 'address1_shippingmethodcode'): Xrm.Controls.OptionSetAttribute;
    /// <summary>
    /// Type the state or province of the primary address.
    /// </summary>
    getAttribute(name: 'address1_stateorprovince'): Xrm.Attributes.StringAttribute;
    /// <summary>
    /// Type the state or province of the primary address.
    /// </summary>
    getControl(name: 'address1_stateorprovince'): Xrm.Controls.StringAttribute;
    /// <summary>
    /// Type the main phone number associated with the primary address.
    /// </summary>
    getAttribute(name: 'address1_telephone1'): Xrm.Attributes.StringAttribute;
    /// <summary>
    /// Type the main phone number associated with the primary address.
    /// </summary>
    getControl(name: 'address1_telephone1'): Xrm.Controls.StringAttribute;
    /// <summary>
    /// Type a second phone number associated with the primary address.
    /// </summary>
    getAttribute(name: 'address1_telephone2'): Xrm.Attributes.StringAttribute;
    /// <summary>
    /// Type a second phone number associated with the primary address.
    /// </summary>
    getControl(name: 'address1_telephone2'): Xrm.Controls.StringAttribute;
    /// <summary>
    /// Type a third phone number associated with the primary address.
    /// </summary>
    getAttribute(name: 'address1_telephone3'): Xrm.Attributes.StringAttribute;
    /// <summary>
    /// Type a third phone number associated with the primary address.
    /// </summary>
    getControl(name: 'address1_telephone3'): Xrm.Controls.StringAttribute;
    /// <summary>
    /// Type the UPS zone of the primary address to make sure shipping charges are calculated correctly and deliveries are made promptly, if shipped by UPS.
    /// </summary>
    getAttribute(name: 'address1_upszone'): Xrm.Attributes.StringAttribute;
    /// <summary>
    /// Type the UPS zone of the primary address to make sure shipping charges are calculated correctly and deliveries are made promptly, if shipped by UPS.
    /// </summary>
    getControl(name: 'address1_upszone'): Xrm.Controls.StringAttribute;
    /// <summary>
    /// Select the time zone, or UTC offset, for this address so that other people can reference it when they contact someone at this address.
    /// </summary>
    getAttribute(name: 'address1_utcoffset'): Xrm.Attributes.NumberAttribute;
    /// <summary>
    /// Select the time zone, or UTC offset, for this address so that other people can reference it when they contact someone at this address.
    /// </summary>
    getControl(name: 'address1_utcoffset'): Xrm.Controls.NumberAttribute;
    /// <summary>
    /// Unique identifier for address 2.
    /// </summary>
    getAttribute(name: 'address2_addressid'): Xrm.Attributes.Attribute;
    /// <summary>
    /// Unique identifier for address 2.
    /// </summary>
    getControl(name: 'address2_addressid'): Xrm.Controls.Attribute;
    /// <summary>
    /// Select the secondary address type.
    /// </summary>
    getAttribute(name: 'address2_addresstypecode'): Xrm.Attributes.OptionSetAttribute;
    /// <summary>
    /// Select the secondary address type.
    /// </summary>
    getControl(name: 'address2_addresstypecode'): Xrm.Controls.OptionSetAttribute;
    /// <summary>
    /// Type the city for the secondary address.
    /// </summary>
    getAttribute(name: 'address2_city'): Xrm.Attributes.StringAttribute;
    /// <summary>
    /// Type the city for the secondary address.
    /// </summary>
    getControl(name: 'address2_city'): Xrm.Controls.StringAttribute;
    /// <summary>
    /// Shows the complete secondary address.
    /// </summary>
    getAttribute(name: 'address2_composite'): Xrm.Attributes.Attribute;
    /// <summary>
    /// Shows the complete secondary address.
    /// </summary>
    getControl(name: 'address2_composite'): Xrm.Controls.Attribute;
    /// <summary>
    /// Type the country or region for the secondary address.
    /// </summary>
    getAttribute(name: 'address2_country'): Xrm.Attributes.StringAttribute;
    /// <summary>
    /// Type the country or region for the secondary address.
    /// </summary>
    getControl(name: 'address2_country'): Xrm.Controls.StringAttribute;
    /// <summary>
    /// Type the county for the secondary address.
    /// </summary>
    getAttribute(name: 'address2_county'): Xrm.Attributes.StringAttribute;
    /// <summary>
    /// Type the county for the secondary address.
    /// </summary>
    getControl(name: 'address2_county'): Xrm.Controls.StringAttribute;
    /// <summary>
    /// Type the fax number associated with the secondary address.
    /// </summary>
    getAttribute(name: 'address2_fax'): Xrm.Attributes.StringAttribute;
    /// <summary>
    /// Type the fax number associated with the secondary address.
    /// </summary>
    getControl(name: 'address2_fax'): Xrm.Controls.StringAttribute;
    /// <summary>
    /// Select the freight terms for the secondary address to make sure shipping orders are processed correctly.
    /// </summary>
    getAttribute(name: 'address2_freighttermscode'): Xrm.Attributes.OptionSetAttribute;
    /// <summary>
    /// Select the freight terms for the secondary address to make sure shipping orders are processed correctly.
    /// </summary>
    getControl(name: 'address2_freighttermscode'): Xrm.Controls.OptionSetAttribute;
    /// <summary>
    /// Type the latitude value for the secondary address for use in mapping and other applications.
    /// </summary>
    getAttribute(name: 'address2_latitude'): Xrm.Attributes.NumberAttribute;
    /// <summary>
    /// Type the latitude value for the secondary address for use in mapping and other applications.
    /// </summary>
    getControl(name: 'address2_latitude'): Xrm.Controls.NumberAttribute;
    /// <summary>
    /// Type the first line of the secondary address.
    /// </summary>
    getAttribute(name: 'address2_line1'): Xrm.Attributes.StringAttribute;
    /// <summary>
    /// Type the first line of the secondary address.
    /// </summary>
    getControl(name: 'address2_line1'): Xrm.Controls.StringAttribute;
    /// <summary>
    /// Type the second line of the secondary address.
    /// </summary>
    getAttribute(name: 'address2_line2'): Xrm.Attributes.StringAttribute;
    /// <summary>
    /// Type the second line of the secondary address.
    /// </summary>
    getControl(name: 'address2_line2'): Xrm.Controls.StringAttribute;
    /// <summary>
    /// Type the third line of the secondary address.
    /// </summary>
    getAttribute(name: 'address2_line3'): Xrm.Attributes.StringAttribute;
    /// <summary>
    /// Type the third line of the secondary address.
    /// </summary>
    getControl(name: 'address2_line3'): Xrm.Controls.StringAttribute;
    /// <summary>
    /// Type the longitude value for the secondary address for use in mapping and other applications.
    /// </summary>
    getAttribute(name: 'address2_longitude'): Xrm.Attributes.NumberAttribute;
    /// <summary>
    /// Type the longitude value for the secondary address for use in mapping and other applications.
    /// </summary>
    getControl(name: 'address2_longitude'): Xrm.Controls.NumberAttribute;
    /// <summary>
    /// Type a descriptive name for the secondary address, such as Corporate Headquarters.
    /// </summary>
    getAttribute(name: 'address2_name'): Xrm.Attributes.StringAttribute;
    /// <summary>
    /// Type a descriptive name for the secondary address, such as Corporate Headquarters.
    /// </summary>
    getControl(name: 'address2_name'): Xrm.Controls.StringAttribute;
    /// <summary>
    /// Type the ZIP Code or postal code for the secondary address.
    /// </summary>
    getAttribute(name: 'address2_postalcode'): Xrm.Attributes.StringAttribute;
    /// <summary>
    /// Type the ZIP Code or postal code for the secondary address.
    /// </summary>
    getControl(name: 'address2_postalcode'): Xrm.Controls.StringAttribute;
    /// <summary>
    /// Type the post office box number of the secondary address.
    /// </summary>
    getAttribute(name: 'address2_postofficebox'): Xrm.Attributes.StringAttribute;
    /// <summary>
    /// Type the post office box number of the secondary address.
    /// </summary>
    getControl(name: 'address2_postofficebox'): Xrm.Controls.StringAttribute;
    /// <summary>
    /// Type the name of the main contact at the account's secondary address.
    /// </summary>
    getAttribute(name: 'address2_primarycontactname'): Xrm.Attributes.StringAttribute;
    /// <summary>
    /// Type the name of the main contact at the account's secondary address.
    /// </summary>
    getControl(name: 'address2_primarycontactname'): Xrm.Controls.StringAttribute;
    /// <summary>
    /// Select a shipping method for deliveries sent to this address.
    /// </summary>
    getAttribute(name: 'address2_shippingmethodcode'): Xrm.Attributes.OptionSetAttribute;
    /// <summary>
    /// Select a shipping method for deliveries sent to this address.
    /// </summary>
    getControl(name: 'address2_shippingmethodcode'): Xrm.Controls.OptionSetAttribute;
    /// <summary>
    /// Type the state or province of the secondary address.
    /// </summary>
    getAttribute(name: 'address2_stateorprovince'): Xrm.Attributes.StringAttribute;
    /// <summary>
    /// Type the state or province of the secondary address.
    /// </summary>
    getControl(name: 'address2_stateorprovince'): Xrm.Controls.StringAttribute;
    /// <summary>
    /// Type the main phone number associated with the secondary address.
    /// </summary>
    getAttribute(name: 'address2_telephone1'): Xrm.Attributes.StringAttribute;
    /// <summary>
    /// Type the main phone number associated with the secondary address.
    /// </summary>
    getControl(name: 'address2_telephone1'): Xrm.Controls.StringAttribute;
    /// <summary>
    /// Type a second phone number associated with the secondary address.
    /// </summary>
    getAttribute(name: 'address2_telephone2'): Xrm.Attributes.StringAttribute;
    /// <summary>
    /// Type a second phone number associated with the secondary address.
    /// </summary>
    getControl(name: 'address2_telephone2'): Xrm.Controls.StringAttribute;
    /// <summary>
    /// Type a third phone number associated with the secondary address.
    /// </summary>
    getAttribute(name: 'address2_telephone3'): Xrm.Attributes.StringAttribute;
    /// <summary>
    /// Type a third phone number associated with the secondary address.
    /// </summary>
    getControl(name: 'address2_telephone3'): Xrm.Controls.StringAttribute;
    /// <summary>
    /// Type the UPS zone of the secondary address to make sure shipping charges are calculated correctly and deliveries are made promptly, if shipped by UPS.
    /// </summary>
    getAttribute(name: 'address2_upszone'): Xrm.Attributes.StringAttribute;
    /// <summary>
    /// Type the UPS zone of the secondary address to make sure shipping charges are calculated correctly and deliveries are made promptly, if shipped by UPS.
    /// </summary>
    getControl(name: 'address2_upszone'): Xrm.Controls.StringAttribute;
    /// <summary>
    /// Select the time zone, or UTC offset, for this address so that other people can reference it when they contact someone at this address.
    /// </summary>
    getAttribute(name: 'address2_utcoffset'): Xrm.Attributes.NumberAttribute;
    /// <summary>
    /// Select the time zone, or UTC offset, for this address so that other people can reference it when they contact someone at this address.
    /// </summary>
    getControl(name: 'address2_utcoffset'): Xrm.Controls.NumberAttribute;
    /// <summary>
    /// For system use only.
    /// </summary>
    getAttribute(name: 'aging30'): Xrm.Attributes.NumberAttribute;
    /// <summary>
    /// For system use only.
    /// </summary>
    getControl(name: 'aging30'): Xrm.Controls.NumberAttribute;
    /// <summary>
    /// The base currency equivalent of the aging 30 field.
    /// </summary>
    getAttribute(name: 'aging30_base'): Xrm.Attributes.NumberAttribute;
    /// <summary>
    /// The base currency equivalent of the aging 30 field.
    /// </summary>
    getControl(name: 'aging30_base'): Xrm.Controls.NumberAttribute;
    /// <summary>
    /// For system use only.
    /// </summary>
    getAttribute(name: 'aging60'): Xrm.Attributes.NumberAttribute;
    /// <summary>
    /// For system use only.
    /// </summary>
    getControl(name: 'aging60'): Xrm.Controls.NumberAttribute;
    /// <summary>
    /// The base currency equivalent of the aging 60 field.
    /// </summary>
    getAttribute(name: 'aging60_base'): Xrm.Attributes.NumberAttribute;
    /// <summary>
    /// The base currency equivalent of the aging 60 field.
    /// </summary>
    getControl(name: 'aging60_base'): Xrm.Controls.NumberAttribute;
    /// <summary>
    /// For system use only.
    /// </summary>
    getAttribute(name: 'aging90'): Xrm.Attributes.NumberAttribute;
    /// <summary>
    /// For system use only.
    /// </summary>
    getControl(name: 'aging90'): Xrm.Controls.NumberAttribute;
    /// <summary>
    /// The base currency equivalent of the aging 90 field.
    /// </summary>
    getAttribute(name: 'aging90_base'): Xrm.Attributes.NumberAttribute;
    /// <summary>
    /// The base currency equivalent of the aging 90 field.
    /// </summary>
    getControl(name: 'aging90_base'): Xrm.Controls.NumberAttribute;
    /// <summary>
    /// Select the legal designation or other business type of the account for contracts or reporting purposes.
    /// </summary>
    getAttribute(name: 'businesstypecode'): Xrm.Attributes.OptionSetAttribute;
    /// <summary>
    /// Select the legal designation or other business type of the account for contracts or reporting purposes.
    /// </summary>
    getControl(name: 'businesstypecode'): Xrm.Controls.OptionSetAttribute;
    /// <summary>
    /// Shows who created the record.
    /// </summary>
    getAttribute(name: 'createdby'): Xrm.Attributes.LookupAttribute;
    /// <summary>
    /// Shows who created the record.
    /// </summary>
    getControl(name: 'createdby'): Xrm.Controls.LookupAttribute;
    /// <summary>
    /// Shows the external party who created the record.
    /// </summary>
    getAttribute(name: 'createdbyexternalparty'): Xrm.Attributes.LookupAttribute;
    /// <summary>
    /// Shows the external party who created the record.
    /// </summary>
    getControl(name: 'createdbyexternalparty'): Xrm.Controls.LookupAttribute;
    /// <summary>
    /// Shows the date and time when the record was created. The date and time are displayed in the time zone selected in Microsoft Dynamics 365 options.
    /// </summary>
    getAttribute(name: 'createdon'): Xrm.Attributes.DateAttribute;
    /// <summary>
    /// Shows the date and time when the record was created. The date and time are displayed in the time zone selected in Microsoft Dynamics 365 options.
    /// </summary>
    getControl(name: 'createdon'): Xrm.Controls.DateAttribute;
    /// <summary>
    /// Shows who created the record on behalf of another user.
    /// </summary>
    getAttribute(name: 'createdonbehalfby'): Xrm.Attributes.LookupAttribute;
    /// <summary>
    /// Shows who created the record on behalf of another user.
    /// </summary>
    getControl(name: 'createdonbehalfby'): Xrm.Controls.LookupAttribute;
    /// <summary>
    /// Type the credit limit of the account. This is a useful reference when you address invoice and accounting issues with the customer.
    /// </summary>
    getAttribute(name: 'creditlimit'): Xrm.Attributes.NumberAttribute;
    /// <summary>
    /// Type the credit limit of the account. This is a useful reference when you address invoice and accounting issues with the customer.
    /// </summary>
    getControl(name: 'creditlimit'): Xrm.Controls.NumberAttribute;
    /// <summary>
    /// Shows the credit limit converted to the system's default base currency for reporting purposes.
    /// </summary>
    getAttribute(name: 'creditlimit_base'): Xrm.Attributes.NumberAttribute;
    /// <summary>
    /// Shows the credit limit converted to the system's default base currency for reporting purposes.
    /// </summary>
    getControl(name: 'creditlimit_base'): Xrm.Controls.NumberAttribute;
    /// <summary>
    /// Select whether the credit for the account is on hold. This is a useful reference while addressing the invoice and accounting issues with the customer.
    /// </summary>
    getAttribute(name: 'creditonhold'): Xrm.Attributes.BooleanAttribute;
    /// <summary>
    /// Select whether the credit for the account is on hold. This is a useful reference while addressing the invoice and accounting issues with the customer.
    /// </summary>
    getControl(name: 'creditonhold'): Xrm.Controls.BooleanAttribute;
    /// <summary>
    /// Select the size category or range of the account for segmentation and reporting purposes.
    /// </summary>
    getAttribute(name: 'customersizecode'): Xrm.Attributes.OptionSetAttribute;
    /// <summary>
    /// Select the size category or range of the account for segmentation and reporting purposes.
    /// </summary>
    getControl(name: 'customersizecode'): Xrm.Controls.OptionSetAttribute;
    /// <summary>
    /// Select the category that best describes the relationship between the account and your organization.
    /// </summary>
    getAttribute(name: 'customertypecode'): Xrm.Attributes.OptionSetAttribute;
    /// <summary>
    /// Select the category that best describes the relationship between the account and your organization.
    /// </summary>
    getControl(name: 'customertypecode'): Xrm.Controls.OptionSetAttribute;
    /// <summary>
    /// Type additional information to describe the account, such as an excerpt from the company's website.
    /// </summary>
    getAttribute(name: 'description'): Xrm.Attributes.Attribute;
    /// <summary>
    /// Type additional information to describe the account, such as an excerpt from the company's website.
    /// </summary>
    getControl(name: 'description'): Xrm.Controls.Attribute;
    /// <summary>
    /// Select whether the account allows bulk email sent through campaigns. If Do Not Allow is selected, the account can be added to marketing lists, but is excluded from email.
    /// </summary>
    getAttribute(name: 'donotbulkemail'): Xrm.Attributes.BooleanAttribute;
    /// <summary>
    /// Select whether the account allows bulk email sent through campaigns. If Do Not Allow is selected, the account can be added to marketing lists, but is excluded from email.
    /// </summary>
    getControl(name: 'donotbulkemail'): Xrm.Controls.BooleanAttribute;
    /// <summary>
    /// Select whether the account allows bulk postal mail sent through marketing campaigns or quick campaigns. If Do Not Allow is selected, the account can be added to marketing lists, but will be excluded from the postal mail.
    /// </summary>
    getAttribute(name: 'donotbulkpostalmail'): Xrm.Attributes.BooleanAttribute;
    /// <summary>
    /// Select whether the account allows bulk postal mail sent through marketing campaigns or quick campaigns. If Do Not Allow is selected, the account can be added to marketing lists, but will be excluded from the postal mail.
    /// </summary>
    getControl(name: 'donotbulkpostalmail'): Xrm.Controls.BooleanAttribute;
    /// <summary>
    /// Select whether the account allows direct email sent from Microsoft Dynamics 365.
    /// </summary>
    getAttribute(name: 'donotemail'): Xrm.Attributes.BooleanAttribute;
    /// <summary>
    /// Select whether the account allows direct email sent from Microsoft Dynamics 365.
    /// </summary>
    getControl(name: 'donotemail'): Xrm.Controls.BooleanAttribute;
    /// <summary>
    /// Select whether the account allows faxes. If Do Not Allow is selected, the account will be excluded from fax activities distributed in marketing campaigns.
    /// </summary>
    getAttribute(name: 'donotfax'): Xrm.Attributes.BooleanAttribute;
    /// <summary>
    /// Select whether the account allows faxes. If Do Not Allow is selected, the account will be excluded from fax activities distributed in marketing campaigns.
    /// </summary>
    getControl(name: 'donotfax'): Xrm.Controls.BooleanAttribute;
    /// <summary>
    /// Select whether the account allows phone calls. If Do Not Allow is selected, the account will be excluded from phone call activities distributed in marketing campaigns.
    /// </summary>
    getAttribute(name: 'donotphone'): Xrm.Attributes.BooleanAttribute;
    /// <summary>
    /// Select whether the account allows phone calls. If Do Not Allow is selected, the account will be excluded from phone call activities distributed in marketing campaigns.
    /// </summary>
    getControl(name: 'donotphone'): Xrm.Controls.BooleanAttribute;
    /// <summary>
    /// Select whether the account allows direct mail. If Do Not Allow is selected, the account will be excluded from letter activities distributed in marketing campaigns.
    /// </summary>
    getAttribute(name: 'donotpostalmail'): Xrm.Attributes.BooleanAttribute;
    /// <summary>
    /// Select whether the account allows direct mail. If Do Not Allow is selected, the account will be excluded from letter activities distributed in marketing campaigns.
    /// </summary>
    getControl(name: 'donotpostalmail'): Xrm.Controls.BooleanAttribute;
    /// <summary>
    /// Select whether the account accepts marketing materials, such as brochures or catalogs.
    /// </summary>
    getAttribute(name: 'donotsendmm'): Xrm.Attributes.BooleanAttribute;
    /// <summary>
    /// Select whether the account accepts marketing materials, such as brochures or catalogs.
    /// </summary>
    getControl(name: 'donotsendmm'): Xrm.Controls.BooleanAttribute;
    /// <summary>
    /// Type the primary email address for the account.
    /// </summary>
    getAttribute(name: 'emailaddress1'): Xrm.Attributes.StringAttribute;
    /// <summary>
    /// Type the primary email address for the account.
    /// </summary>
    getControl(name: 'emailaddress1'): Xrm.Controls.StringAttribute;
    /// <summary>
    /// Type the secondary email address for the account.
    /// </summary>
    getAttribute(name: 'emailaddress2'): Xrm.Attributes.StringAttribute;
    /// <summary>
    /// Type the secondary email address for the account.
    /// </summary>
    getControl(name: 'emailaddress2'): Xrm.Controls.StringAttribute;
    /// <summary>
    /// Type an alternate email address for the account.
    /// </summary>
    getAttribute(name: 'emailaddress3'): Xrm.Attributes.StringAttribute;
    /// <summary>
    /// Type an alternate email address for the account.
    /// </summary>
    getControl(name: 'emailaddress3'): Xrm.Controls.StringAttribute;
    /// <summary>
    /// Shows the conversion rate of the record's currency. The exchange rate is used to convert all money fields in the record from the local currency to the system's default currency.
    /// </summary>
    getAttribute(name: 'exchangerate'): Xrm.Attributes.NumberAttribute;
    /// <summary>
    /// Shows the conversion rate of the record's currency. The exchange rate is used to convert all money fields in the record from the local currency to the system's default currency.
    /// </summary>
    getControl(name: 'exchangerate'): Xrm.Controls.NumberAttribute;
    /// <summary>
    /// Type the fax number for the account.
    /// </summary>
    getAttribute(name: 'fax'): Xrm.Attributes.StringAttribute;
    /// <summary>
    /// Type the fax number for the account.
    /// </summary>
    getControl(name: 'fax'): Xrm.Controls.StringAttribute;

    getAttribute(name: 'fisi_optionsetwithpublisher'): Xrm.Attributes.OptionSetAttribute;

    getControl(name: 'fisi_optionsetwithpublisher'): Xrm.Controls.OptionSetAttribute;
    /// <summary>
    /// Information about whether to allow following email activity like opens, attachment views and link clicks for emails sent to the account.
    /// </summary>
    getAttribute(name: 'followemail'): Xrm.Attributes.BooleanAttribute;
    /// <summary>
    /// Information about whether to allow following email activity like opens, attachment views and link clicks for emails sent to the account.
    /// </summary>
    getControl(name: 'followemail'): Xrm.Controls.BooleanAttribute;
    /// <summary>
    /// Type the URL for the account's FTP site to enable users to access data and share documents.
    /// </summary>
    getAttribute(name: 'ftpsiteurl'): Xrm.Attributes.StringAttribute;
    /// <summary>
    /// Type the URL for the account's FTP site to enable users to access data and share documents.
    /// </summary>
    getControl(name: 'ftpsiteurl'): Xrm.Controls.StringAttribute;
    /// <summary>
    /// Unique identifier of the data import or data migration that created this record.
    /// </summary>
    getAttribute(name: 'importsequencenumber'): Xrm.Attributes.NumberAttribute;
    /// <summary>
    /// Unique identifier of the data import or data migration that created this record.
    /// </summary>
    getControl(name: 'importsequencenumber'): Xrm.Controls.NumberAttribute;
    /// <summary>
    /// Select the account's primary industry for use in marketing segmentation and demographic analysis.
    /// </summary>
    getAttribute(name: 'industrycode'): Xrm.Attributes.OptionSetAttribute;
    /// <summary>
    /// Select the account's primary industry for use in marketing segmentation and demographic analysis.
    /// </summary>
    getControl(name: 'industrycode'): Xrm.Controls.OptionSetAttribute;
    /// <summary>
    /// Contains the date and time stamp of the last on hold time.
    /// </summary>
    getAttribute(name: 'lastonholdtime'): Xrm.Attributes.DateAttribute;
    /// <summary>
    /// Contains the date and time stamp of the last on hold time.
    /// </summary>
    getControl(name: 'lastonholdtime'): Xrm.Controls.DateAttribute;
    /// <summary>
    /// Shows the date when the account was last included in a marketing campaign or quick campaign.
    /// </summary>
    getAttribute(name: 'lastusedincampaign'): Xrm.Attributes.DateAttribute;
    /// <summary>
    /// Shows the date when the account was last included in a marketing campaign or quick campaign.
    /// </summary>
    getControl(name: 'lastusedincampaign'): Xrm.Controls.DateAttribute;
    /// <summary>
    /// Type the market capitalization of the account to identify the company's equity, used as an indicator in financial performance analysis.
    /// </summary>
    getAttribute(name: 'marketcap'): Xrm.Attributes.NumberAttribute;
    /// <summary>
    /// Type the market capitalization of the account to identify the company's equity, used as an indicator in financial performance analysis.
    /// </summary>
    getControl(name: 'marketcap'): Xrm.Controls.NumberAttribute;
    /// <summary>
    /// Shows the market capitalization converted to the system's default base currency.
    /// </summary>
    getAttribute(name: 'marketcap_base'): Xrm.Attributes.NumberAttribute;
    /// <summary>
    /// Shows the market capitalization converted to the system's default base currency.
    /// </summary>
    getControl(name: 'marketcap_base'): Xrm.Controls.NumberAttribute;
    /// <summary>
    /// Whether is only for marketing
    /// </summary>
    getAttribute(name: 'marketingonly'): Xrm.Attributes.BooleanAttribute;
    /// <summary>
    /// Whether is only for marketing
    /// </summary>
    getControl(name: 'marketingonly'): Xrm.Controls.BooleanAttribute;
    /// <summary>
    /// Shows the master account that the account was merged with.
    /// </summary>
    getAttribute(name: 'masterid'): Xrm.Attributes.LookupAttribute;
    /// <summary>
    /// Shows the master account that the account was merged with.
    /// </summary>
    getControl(name: 'masterid'): Xrm.Controls.LookupAttribute;
    /// <summary>
    /// Shows whether the account has been merged with another account.
    /// </summary>
    getAttribute(name: 'merged'): Xrm.Attributes.BooleanAttribute;
    /// <summary>
    /// Shows whether the account has been merged with another account.
    /// </summary>
    getControl(name: 'merged'): Xrm.Controls.BooleanAttribute;
    /// <summary>
    /// Shows who last updated the record.
    /// </summary>
    getAttribute(name: 'modifiedby'): Xrm.Attributes.LookupAttribute;
    /// <summary>
    /// Shows who last updated the record.
    /// </summary>
    getControl(name: 'modifiedby'): Xrm.Controls.LookupAttribute;
    /// <summary>
    /// Shows the external party who modified the record.
    /// </summary>
    getAttribute(name: 'modifiedbyexternalparty'): Xrm.Attributes.LookupAttribute;
    /// <summary>
    /// Shows the external party who modified the record.
    /// </summary>
    getControl(name: 'modifiedbyexternalparty'): Xrm.Controls.LookupAttribute;
    /// <summary>
    /// Shows the date and time when the record was last updated. The date and time are displayed in the time zone selected in Microsoft Dynamics 365 options.
    /// </summary>
    getAttribute(name: 'modifiedon'): Xrm.Attributes.DateAttribute;
    /// <summary>
    /// Shows the date and time when the record was last updated. The date and time are displayed in the time zone selected in Microsoft Dynamics 365 options.
    /// </summary>
    getControl(name: 'modifiedon'): Xrm.Controls.DateAttribute;
    /// <summary>
    /// Shows who created the record on behalf of another user.
    /// </summary>
    getAttribute(name: 'modifiedonbehalfby'): Xrm.Attributes.LookupAttribute;
    /// <summary>
    /// Shows who created the record on behalf of another user.
    /// </summary>
    getControl(name: 'modifiedonbehalfby'): Xrm.Controls.LookupAttribute;
    /// <summary>
    /// Type the company or business name.
    /// </summary>
    getAttribute(name: 'name'): Xrm.Attributes.StringAttribute;
    /// <summary>
    /// Type the company or business name.
    /// </summary>
    getControl(name: 'name'): Xrm.Controls.StringAttribute;
    /// <summary>
    /// Type the number of employees that work at the account for use in marketing segmentation and demographic analysis.
    /// </summary>
    getAttribute(name: 'numberofemployees'): Xrm.Attributes.NumberAttribute;
    /// <summary>
    /// Type the number of employees that work at the account for use in marketing segmentation and demographic analysis.
    /// </summary>
    getControl(name: 'numberofemployees'): Xrm.Controls.NumberAttribute;
    /// <summary>
    /// Shows how long, in minutes, that the record was on hold.
    /// </summary>
    getAttribute(name: 'onholdtime'): Xrm.Attributes.NumberAttribute;
    /// <summary>
    /// Shows how long, in minutes, that the record was on hold.
    /// </summary>
    getControl(name: 'onholdtime'): Xrm.Controls.NumberAttribute;
    /// <summary>
    /// Date and time that the record was migrated.
    /// </summary>
    getAttribute(name: 'overriddencreatedon'): Xrm.Attributes.DateAttribute;
    /// <summary>
    /// Date and time that the record was migrated.
    /// </summary>
    getControl(name: 'overriddencreatedon'): Xrm.Controls.DateAttribute;
    /// <summary>
    /// Enter the user or team who is assigned to manage the record. This field is updated every time the record is assigned to a different user.
    /// </summary>
    getAttribute(name: 'ownerid'): Xrm.Attributes.LookupAttribute;
    /// <summary>
    /// Enter the user or team who is assigned to manage the record. This field is updated every time the record is assigned to a different user.
    /// </summary>
    getControl(name: 'ownerid'): Xrm.Controls.LookupAttribute;
    /// <summary>
    /// Select the account's ownership structure, such as public or private.
    /// </summary>
    getAttribute(name: 'ownershipcode'): Xrm.Attributes.OptionSetAttribute;
    /// <summary>
    /// Select the account's ownership structure, such as public or private.
    /// </summary>
    getControl(name: 'ownershipcode'): Xrm.Controls.OptionSetAttribute;
    /// <summary>
    /// Shows the business unit that the record owner belongs to.
    /// </summary>
    getAttribute(name: 'owningbusinessunit'): Xrm.Attributes.LookupAttribute;
    /// <summary>
    /// Shows the business unit that the record owner belongs to.
    /// </summary>
    getControl(name: 'owningbusinessunit'): Xrm.Controls.LookupAttribute;
    /// <summary>
    /// Unique identifier of the team who owns the account.
    /// </summary>
    getAttribute(name: 'owningteam'): Xrm.Attributes.LookupAttribute;
    /// <summary>
    /// Unique identifier of the team who owns the account.
    /// </summary>
    getControl(name: 'owningteam'): Xrm.Controls.LookupAttribute;
    /// <summary>
    /// Unique identifier of the user who owns the account.
    /// </summary>
    getAttribute(name: 'owninguser'): Xrm.Attributes.LookupAttribute;
    /// <summary>
    /// Unique identifier of the user who owns the account.
    /// </summary>
    getControl(name: 'owninguser'): Xrm.Controls.LookupAttribute;
    /// <summary>
    /// Choose the parent account associated with this account to show parent and child businesses in reporting and analytics.
    /// </summary>
    getAttribute(name: 'parentaccountid'): Xrm.Attributes.LookupAttribute;
    /// <summary>
    /// Choose the parent account associated with this account to show parent and child businesses in reporting and analytics.
    /// </summary>
    getControl(name: 'parentaccountid'): Xrm.Controls.LookupAttribute;
    /// <summary>
    /// For system use only. Legacy Microsoft Dynamics CRM 3.0 workflow data.
    /// </summary>
    getAttribute(name: 'participatesinworkflow'): Xrm.Attributes.BooleanAttribute;
    /// <summary>
    /// For system use only. Legacy Microsoft Dynamics CRM 3.0 workflow data.
    /// </summary>
    getControl(name: 'participatesinworkflow'): Xrm.Controls.BooleanAttribute;
    /// <summary>
    /// Select the payment terms to indicate when the customer needs to pay the total amount.
    /// </summary>
    getAttribute(name: 'paymenttermscode'): Xrm.Attributes.OptionSetAttribute;
    /// <summary>
    /// Select the payment terms to indicate when the customer needs to pay the total amount.
    /// </summary>
    getControl(name: 'paymenttermscode'): Xrm.Controls.OptionSetAttribute;
    /// <summary>
    /// Select the preferred day of the week for service appointments.
    /// </summary>
    getAttribute(name: 'preferredappointmentdaycode'): Xrm.Attributes.OptionSetAttribute;
    /// <summary>
    /// Select the preferred day of the week for service appointments.
    /// </summary>
    getControl(name: 'preferredappointmentdaycode'): Xrm.Controls.OptionSetAttribute;
    /// <summary>
    /// Select the preferred time of day for service appointments.
    /// </summary>
    getAttribute(name: 'preferredappointmenttimecode'): Xrm.Attributes.OptionSetAttribute;
    /// <summary>
    /// Select the preferred time of day for service appointments.
    /// </summary>
    getControl(name: 'preferredappointmenttimecode'): Xrm.Controls.OptionSetAttribute;
    /// <summary>
    /// Select the preferred method of contact.
    /// </summary>
    getAttribute(name: 'preferredcontactmethodcode'): Xrm.Attributes.OptionSetAttribute;
    /// <summary>
    /// Select the preferred method of contact.
    /// </summary>
    getControl(name: 'preferredcontactmethodcode'): Xrm.Controls.OptionSetAttribute;
    /// <summary>
    /// Choose the preferred service representative for reference when you schedule service activities for the account.
    /// </summary>
    getAttribute(name: 'preferredsystemuserid'): Xrm.Attributes.LookupAttribute;
    /// <summary>
    /// Choose the preferred service representative for reference when you schedule service activities for the account.
    /// </summary>
    getControl(name: 'preferredsystemuserid'): Xrm.Controls.LookupAttribute;
    /// <summary>
    /// Choose the primary contact for the account to provide quick access to contact details.
    /// </summary>
    getAttribute(name: 'primarycontactid'): Xrm.Attributes.LookupAttribute;
    /// <summary>
    /// Choose the primary contact for the account to provide quick access to contact details.
    /// </summary>
    getControl(name: 'primarycontactid'): Xrm.Controls.LookupAttribute;
    /// <summary>
    /// Primary Satori ID for Account
    /// </summary>
    getAttribute(name: 'primarysatoriid'): Xrm.Attributes.StringAttribute;
    /// <summary>
    /// Primary Satori ID for Account
    /// </summary>
    getControl(name: 'primarysatoriid'): Xrm.Controls.StringAttribute;
    /// <summary>
    /// Primary Twitter ID for Account
    /// </summary>
    getAttribute(name: 'primarytwitterid'): Xrm.Attributes.StringAttribute;
    /// <summary>
    /// Primary Twitter ID for Account
    /// </summary>
    getControl(name: 'primarytwitterid'): Xrm.Controls.StringAttribute;
    /// <summary>
    /// Shows the ID of the process.
    /// </summary>
    getAttribute(name: 'processid'): Xrm.Attributes.Attribute;
    /// <summary>
    /// Shows the ID of the process.
    /// </summary>
    getControl(name: 'processid'): Xrm.Controls.Attribute;
    /// <summary>
    /// Type the annual revenue for the account, used as an indicator in financial performance analysis.
    /// </summary>
    getAttribute(name: 'revenue'): Xrm.Attributes.NumberAttribute;
    /// <summary>
    /// Type the annual revenue for the account, used as an indicator in financial performance analysis.
    /// </summary>
    getControl(name: 'revenue'): Xrm.Controls.NumberAttribute;
    /// <summary>
    /// Shows the annual revenue converted to the system's default base currency. The calculations use the exchange rate specified in the Currencies area.
    /// </summary>
    getAttribute(name: 'revenue_base'): Xrm.Attributes.NumberAttribute;
    /// <summary>
    /// Shows the annual revenue converted to the system's default base currency. The calculations use the exchange rate specified in the Currencies area.
    /// </summary>
    getControl(name: 'revenue_base'): Xrm.Controls.NumberAttribute;
    /// <summary>
    /// Type the number of shares available to the public for the account. This number is used as an indicator in financial performance analysis.
    /// </summary>
    getAttribute(name: 'sharesoutstanding'): Xrm.Attributes.NumberAttribute;
    /// <summary>
    /// Type the number of shares available to the public for the account. This number is used as an indicator in financial performance analysis.
    /// </summary>
    getControl(name: 'sharesoutstanding'): Xrm.Controls.NumberAttribute;
    /// <summary>
    /// Select a shipping method for deliveries sent to the account's address to designate the preferred carrier or other delivery option.
    /// </summary>
    getAttribute(name: 'shippingmethodcode'): Xrm.Attributes.OptionSetAttribute;
    /// <summary>
    /// Select a shipping method for deliveries sent to the account's address to designate the preferred carrier or other delivery option.
    /// </summary>
    getControl(name: 'shippingmethodcode'): Xrm.Controls.OptionSetAttribute;
    /// <summary>
    /// Type the Standard Industrial Classification (SIC) code that indicates the account's primary industry of business, for use in marketing segmentation and demographic analysis.
    /// </summary>
    getAttribute(name: 'sic'): Xrm.Attributes.StringAttribute;
    /// <summary>
    /// Type the Standard Industrial Classification (SIC) code that indicates the account's primary industry of business, for use in marketing segmentation and demographic analysis.
    /// </summary>
    getControl(name: 'sic'): Xrm.Controls.StringAttribute;
    /// <summary>
    /// Choose the service level agreement (SLA) that you want to apply to the Account record.
    /// </summary>
    getAttribute(name: 'slaid'): Xrm.Attributes.LookupAttribute;
    /// <summary>
    /// Choose the service level agreement (SLA) that you want to apply to the Account record.
    /// </summary>
    getControl(name: 'slaid'): Xrm.Controls.LookupAttribute;
    /// <summary>
    /// Last SLA that was applied to this case. This field is for internal use only.
    /// </summary>
    getAttribute(name: 'slainvokedid'): Xrm.Attributes.LookupAttribute;
    /// <summary>
    /// Last SLA that was applied to this case. This field is for internal use only.
    /// </summary>
    getControl(name: 'slainvokedid'): Xrm.Controls.LookupAttribute;
    /// <summary>
    /// Shows the ID of the stage.
    /// </summary>
    getAttribute(name: 'stageid'): Xrm.Attributes.Attribute;
    /// <summary>
    /// Shows the ID of the stage.
    /// </summary>
    getControl(name: 'stageid'): Xrm.Controls.Attribute;
    /// <summary>
    /// Shows whether the account is active or inactive. Inactive accounts are read-only and can't be edited unless they are reactivated.
    /// </summary>
    getAttribute(name: 'statecode'): Xrm.Attributes.OptionSetAttribute;
    /// <summary>
    /// Shows whether the account is active or inactive. Inactive accounts are read-only and can't be edited unless they are reactivated.
    /// </summary>
    getControl(name: 'statecode'): Xrm.Controls.OptionSetAttribute;
    /// <summary>
    /// Select the account's status.
    /// </summary>
    getAttribute(name: 'statuscode'): Xrm.Attributes.OptionSetAttribute;
    /// <summary>
    /// Select the account's status.
    /// </summary>
    getControl(name: 'statuscode'): Xrm.Controls.OptionSetAttribute;
    /// <summary>
    /// Type the stock exchange at which the account is listed to track their stock and financial performance of the company.
    /// </summary>
    getAttribute(name: 'stockexchange'): Xrm.Attributes.StringAttribute;
    /// <summary>
    /// Type the stock exchange at which the account is listed to track their stock and financial performance of the company.
    /// </summary>
    getControl(name: 'stockexchange'): Xrm.Controls.StringAttribute;
    /// <summary>
    /// Type the main phone number for this account.
    /// </summary>
    getAttribute(name: 'telephone1'): Xrm.Attributes.StringAttribute;
    /// <summary>
    /// Type the main phone number for this account.
    /// </summary>
    getControl(name: 'telephone1'): Xrm.Controls.StringAttribute;
    /// <summary>
    /// Type a second phone number for this account.
    /// </summary>
    getAttribute(name: 'telephone2'): Xrm.Attributes.StringAttribute;
    /// <summary>
    /// Type a second phone number for this account.
    /// </summary>
    getControl(name: 'telephone2'): Xrm.Controls.StringAttribute;
    /// <summary>
    /// Type a third phone number for this account.
    /// </summary>
    getAttribute(name: 'telephone3'): Xrm.Attributes.StringAttribute;
    /// <summary>
    /// Type a third phone number for this account.
    /// </summary>
    getControl(name: 'telephone3'): Xrm.Controls.StringAttribute;
    /// <summary>
    /// Select a region or territory for the account for use in segmentation and analysis.
    /// </summary>
    getAttribute(name: 'territorycode'): Xrm.Attributes.OptionSetAttribute;
    /// <summary>
    /// Select a region or territory for the account for use in segmentation and analysis.
    /// </summary>
    getControl(name: 'territorycode'): Xrm.Controls.OptionSetAttribute;
    /// <summary>
    /// Type the stock exchange symbol for the account to track financial performance of the company. You can click the code entered in this field to access the latest trading information from MSN Money.
    /// </summary>
    getAttribute(name: 'tickersymbol'): Xrm.Attributes.StringAttribute;
    /// <summary>
    /// Type the stock exchange symbol for the account to track financial performance of the company. You can click the code entered in this field to access the latest trading information from MSN Money.
    /// </summary>
    getControl(name: 'tickersymbol'): Xrm.Controls.StringAttribute;
    /// <summary>
    /// Total time spent for emails (read and write) and meetings by me in relation to account record.
    /// </summary>
    getAttribute(name: 'timespentbymeonemailandmeetings'): Xrm.Attributes.StringAttribute;
    /// <summary>
    /// Total time spent for emails (read and write) and meetings by me in relation to account record.
    /// </summary>
    getControl(name: 'timespentbymeonemailandmeetings'): Xrm.Controls.StringAttribute;
    /// <summary>
    /// For internal use only.
    /// </summary>
    getAttribute(name: 'timezoneruleversionnumber'): Xrm.Attributes.NumberAttribute;
    /// <summary>
    /// For internal use only.
    /// </summary>
    getControl(name: 'timezoneruleversionnumber'): Xrm.Controls.NumberAttribute;
    /// <summary>
    /// Choose the local currency for the record to make sure budgets are reported in the correct currency.
    /// </summary>
    getAttribute(name: 'transactioncurrencyid'): Xrm.Attributes.LookupAttribute;
    /// <summary>
    /// Choose the local currency for the record to make sure budgets are reported in the correct currency.
    /// </summary>
    getControl(name: 'transactioncurrencyid'): Xrm.Controls.LookupAttribute;
    /// <summary>
    /// For internal use only.
    /// </summary>
    getAttribute(name: 'traversedpath'): Xrm.Attributes.StringAttribute;
    /// <summary>
    /// For internal use only.
    /// </summary>
    getControl(name: 'traversedpath'): Xrm.Controls.StringAttribute;
    /// <summary>
    /// Time zone code that was in use when the record was created.
    /// </summary>
    getAttribute(name: 'utcconversiontimezonecode'): Xrm.Attributes.NumberAttribute;
    /// <summary>
    /// Time zone code that was in use when the record was created.
    /// </summary>
    getControl(name: 'utcconversiontimezonecode'): Xrm.Controls.NumberAttribute;
    /// <summary>
    /// Version number of the account.
    /// </summary>
    getAttribute(name: 'versionnumber'): Xrm.Attributes.NumberAttribute;
    /// <summary>
    /// Version number of the account.
    /// </summary>
    getControl(name: 'versionnumber'): Xrm.Controls.NumberAttribute;
    /// <summary>
    /// Type the account's website URL to get quick details about the company profile.
    /// </summary>
    getAttribute(name: 'websiteurl'): Xrm.Attributes.StringAttribute;
    /// <summary>
    /// Type the account's website URL to get quick details about the company profile.
    /// </summary>
    getControl(name: 'websiteurl'): Xrm.Controls.StringAttribute;
    /// <summary>
    /// Type the phonetic spelling of the company name, if specified in Japanese, to make sure the name is pronounced correctly in phone calls and other communications.
    /// </summary>
    getAttribute(name: 'yominame'): Xrm.Attributes.StringAttribute;
    /// <summary>
    /// Type the phonetic spelling of the company name, if specified in Japanese, to make sure the name is pronounced correctly in phone calls and other communications.
    /// </summary>
    getControl(name: 'yominame'): Xrm.Controls.StringAttribute;
}

// Entity Account
export const AccountMetadata = {
    typeName: "mscrm.account",
    logicalName: "account",
    collectionName: "accounts",
    primaryIdAttribute: "accountid",
    attributeTypes: {
        AccountCategoryCode: "OptionSet",
        AccountClassificationCode: "OptionSet",
        AccountId: "Attribute",
        AccountNumber: "String",
        AccountRatingCode: "OptionSet",
        Address1AddressId: "Attribute",
        Address1AddressTypeCode: "OptionSet",
        Address1City: "String",
        Address1Composite: "Attribute",
        Address1Country: "String",
        Address1County: "String",
        Address1Fax: "String",
        Address1FreightTermsCode: "OptionSet",
        Address1Latitude: "Double",
        Address1Line1: "String",
        Address1Line2: "String",
        Address1Line3: "String",
        Address1Longitude: "Double",
        Address1Name: "String",
        Address1PostalCode: "String",
        Address1PostOfficeBox: "String",
        Address1PrimaryContactName: "String",
        Address1ShippingMethodCode: "OptionSet",
        Address1StateOrProvince: "String",
        Address1Telephone1: "String",
        Address1Telephone2: "String",
        Address1Telephone3: "String",
        Address1UPSZone: "String",
        Address1UTCOffset: "Integer",
        Address2AddressId: "Attribute",
        Address2AddressTypeCode: "OptionSet",
        Address2City: "String",
        Address2Composite: "Attribute",
        Address2Country: "String",
        Address2County: "String",
        Address2Fax: "String",
        Address2FreightTermsCode: "OptionSet",
        Address2Latitude: "Double",
        Address2Line1: "String",
        Address2Line2: "String",
        Address2Line3: "String",
        Address2Longitude: "Double",
        Address2Name: "String",
        Address2PostalCode: "String",
        Address2PostOfficeBox: "String",
        Address2PrimaryContactName: "String",
        Address2ShippingMethodCode: "OptionSet",
        Address2StateOrProvince: "String",
        Address2Telephone1: "String",
        Address2Telephone2: "String",
        Address2Telephone3: "String",
        Address2UPSZone: "String",
        Address2UTCOffset: "Integer",
        Aging30: "Integer",
        Aging30Base: "Integer",
        Aging60: "Integer",
        Aging60Base: "Integer",
        Aging90: "Integer",
        Aging90Base: "Integer",
        BusinessTypeCode: "OptionSet",
        CreatedBy: "Lookup",
        CreatedByExternalParty: "Lookup",
        CreatedOn: "DateAndTime:UserLocal",
        CreatedOnBehalfBy: "Lookup",
        CreditLimit: "Integer",
        CreditLimitBase: "Integer",
        CreditOnHold: "Optionset",
        CustomerSizeCode: "OptionSet",
        CustomerTypeCode: "OptionSet",
        Description: "Attribute",
        DoNotBulkEMail: "Optionset",
        DoNotBulkPostalMail: "Optionset",
        DoNotEMail: "Optionset",
        DoNotFax: "Optionset",
        DoNotPhone: "Optionset",
        DoNotPostalMail: "Optionset",
        DoNotSendMM: "Optionset",
        EMailAddress1: "String",
        EMailAddress2: "String",
        EMailAddress3: "String",
        ExchangeRate: "Decimal",
        Fax: "String",
        FisiOptionSetWithPublisher: "OptionSet",
        FollowEmail: "Optionset",
        FtpSiteURL: "String",
        ImportSequenceNumber: "Integer",
        IndustryCode: "OptionSet",
        LastOnHoldTime: "DateAndTime:UserLocal",
        LastUsedInCampaign: "DateAndTime:UserLocal",
        MarketCap: "Integer",
        MarketCapBase: "Integer",
        MarketingOnly: "Optionset",
        MasterId: "Lookup",
        Merged: "Optionset",
        ModifiedBy: "Lookup",
        ModifiedByExternalParty: "Lookup",
        ModifiedOn: "DateAndTime:UserLocal",
        ModifiedOnBehalfBy: "Lookup",
        Name: "String",
        NumberOfEmployees: "Integer",
        OnHoldTime: "Integer",
        OverriddenCreatedOn: "DateAndTime:UserLocal",
        OwnerId: "Lookup",
        OwnershipCode: "OptionSet",
        OwningBusinessUnit: "Lookup",
        OwningTeam: "Lookup",
        OwningUser: "Lookup",
        ParentAccountId: "Lookup",
        ParticipatesInWorkflow: "Optionset",
        PaymentTermsCode: "OptionSet",
        PreferredAppointmentDayCode: "OptionSet",
        PreferredAppointmentTimeCode: "OptionSet",
        PreferredContactMethodCode: "OptionSet",
        PreferredSystemUserId: "Lookup",
        PrimaryContactId: "Lookup",
        PrimarySatoriId: "String",
        PrimaryTwitterId: "String",
        ProcessId: "Attribute",
        Revenue: "Integer",
        RevenueBase: "Integer",
        SharesOutstanding: "Integer",
        ShippingMethodCode: "OptionSet",
        SIC: "String",
        SLAId: "Lookup",
        SLAInvokedId: "Lookup",
        StageId: "Attribute",
        StateCode: "OptionSet",
        StatusCode: "OptionSet",
        StockExchange: "String",
        Telephone1: "String",
        Telephone2: "String",
        Telephone3: "String",
        TerritoryCode: "OptionSet",
        TickerSymbol: "String",
        TimeSpentByMeOnEmailAndMeetings: "String",
        TimeZoneRuleVersionNumber: "Integer",
        TransactionCurrencyId: "Lookup",
        TraversedPath: "String",
        UTCConversionTimeZoneCode: "Integer",
        VersionNumber: "BigInt",
        WebSiteURL: "String",
        YomiName: "String",
    }
};

// Account Attribute constants
export const enum AccountAttributes {
    AccountCategoryCode = "accountcategorycode",
    AccountClassificationCode = "accountclassificationcode",
    AccountId = "accountid",
    AccountNumber = "accountnumber",
    AccountRatingCode = "accountratingcode",
    Address1AddressId = "address1_addressid",
    Address1AddressTypeCode = "address1_addresstypecode",
    Address1City = "address1_city",
    Address1Composite = "address1_composite",
    Address1Country = "address1_country",
    Address1County = "address1_county",
    Address1Fax = "address1_fax",
    Address1FreightTermsCode = "address1_freighttermscode",
    Address1Latitude = "address1_latitude",
    Address1Line1 = "address1_line1",
    Address1Line2 = "address1_line2",
    Address1Line3 = "address1_line3",
    Address1Longitude = "address1_longitude",
    Address1Name = "address1_name",
    Address1PostalCode = "address1_postalcode",
    Address1PostOfficeBox = "address1_postofficebox",
    Address1PrimaryContactName = "address1_primarycontactname",
    Address1ShippingMethodCode = "address1_shippingmethodcode",
    Address1StateOrProvince = "address1_stateorprovince",
    Address1Telephone1 = "address1_telephone1",
    Address1Telephone2 = "address1_telephone2",
    Address1Telephone3 = "address1_telephone3",
    Address1UPSZone = "address1_upszone",
    Address1UTCOffset = "address1_utcoffset",
    Address2AddressId = "address2_addressid",
    Address2AddressTypeCode = "address2_addresstypecode",
    Address2City = "address2_city",
    Address2Composite = "address2_composite",
    Address2Country = "address2_country",
    Address2County = "address2_county",
    Address2Fax = "address2_fax",
    Address2FreightTermsCode = "address2_freighttermscode",
    Address2Latitude = "address2_latitude",
    Address2Line1 = "address2_line1",
    Address2Line2 = "address2_line2",
    Address2Line3 = "address2_line3",
    Address2Longitude = "address2_longitude",
    Address2Name = "address2_name",
    Address2PostalCode = "address2_postalcode",
    Address2PostOfficeBox = "address2_postofficebox",
    Address2PrimaryContactName = "address2_primarycontactname",
    Address2ShippingMethodCode = "address2_shippingmethodcode",
    Address2StateOrProvince = "address2_stateorprovince",
    Address2Telephone1 = "address2_telephone1",
    Address2Telephone2 = "address2_telephone2",
    Address2Telephone3 = "address2_telephone3",
    Address2UPSZone = "address2_upszone",
    Address2UTCOffset = "address2_utcoffset",
    Aging30 = "aging30",
    Aging30Base = "aging30_base",
    Aging60 = "aging60",
    Aging60Base = "aging60_base",
    Aging90 = "aging90",
    Aging90Base = "aging90_base",
    BusinessTypeCode = "businesstypecode",
    CreatedBy = "createdby",
    CreatedByExternalParty = "createdbyexternalparty",
    CreatedOn = "createdon",
    CreatedOnBehalfBy = "createdonbehalfby",
    CreditLimit = "creditlimit",
    CreditLimitBase = "creditlimit_base",
    CreditOnHold = "creditonhold",
    CustomerSizeCode = "customersizecode",
    CustomerTypeCode = "customertypecode",
    Description = "description",
    DoNotBulkEMail = "donotbulkemail",
    DoNotBulkPostalMail = "donotbulkpostalmail",
    DoNotEMail = "donotemail",
    DoNotFax = "donotfax",
    DoNotPhone = "donotphone",
    DoNotPostalMail = "donotpostalmail",
    DoNotSendMM = "donotsendmm",
    EMailAddress1 = "emailaddress1",
    EMailAddress2 = "emailaddress2",
    EMailAddress3 = "emailaddress3",
    ExchangeRate = "exchangerate",
    Fax = "fax",
    FisiOptionSetWithPublisher = "fisi_optionsetwithpublisher",
    FollowEmail = "followemail",
    FtpSiteURL = "ftpsiteurl",
    ImportSequenceNumber = "importsequencenumber",
    IndustryCode = "industrycode",
    LastOnHoldTime = "lastonholdtime",
    LastUsedInCampaign = "lastusedincampaign",
    MarketCap = "marketcap",
    MarketCapBase = "marketcap_base",
    MarketingOnly = "marketingonly",
    MasterId = "masterid",
    Merged = "merged",
    ModifiedBy = "modifiedby",
    ModifiedByExternalParty = "modifiedbyexternalparty",
    ModifiedOn = "modifiedon",
    ModifiedOnBehalfBy = "modifiedonbehalfby",
    Name = "name",
    NumberOfEmployees = "numberofemployees",
    OnHoldTime = "onholdtime",
    OverriddenCreatedOn = "overriddencreatedon",
    OwnerId = "ownerid",
    OwnershipCode = "ownershipcode",
    OwningBusinessUnit = "owningbusinessunit",
    OwningTeam = "owningteam",
    OwningUser = "owninguser",
    ParentAccountId = "parentaccountid",
    ParticipatesInWorkflow = "participatesinworkflow",
    PaymentTermsCode = "paymenttermscode",
    PreferredAppointmentDayCode = "preferredappointmentdaycode",
    PreferredAppointmentTimeCode = "preferredappointmenttimecode",
    PreferredContactMethodCode = "preferredcontactmethodcode",
    PreferredSystemUserId = "preferredsystemuserid",
    PrimaryContactId = "primarycontactid",
    PrimarySatoriId = "primarysatoriid",
    PrimaryTwitterId = "primarytwitterid",
    ProcessId = "processid",
    Revenue = "revenue",
    RevenueBase = "revenue_base",
    SharesOutstanding = "sharesoutstanding",
    ShippingMethodCode = "shippingmethodcode",
    SIC = "sic",
    SLAId = "slaid",
    SLAInvokedId = "slainvokedid",
    StageId = "stageid",
    StateCode = "statecode",
    StatusCode = "statuscode",
    StockExchange = "stockexchange",
    Telephone1 = "telephone1",
    Telephone2 = "telephone2",
    Telephone3 = "telephone3",
    TerritoryCode = "territorycode",
    TickerSymbol = "tickersymbol",
    TimeSpentByMeOnEmailAndMeetings = "timespentbymeonemailandmeetings",
    TimeZoneRuleVersionNumber = "timezoneruleversionnumber",
    TransactionCurrencyId = "transactioncurrencyid",
    TraversedPath = "traversedpath",
    UTCConversionTimeZoneCode = "utcconversiontimezonecode",
    VersionNumber = "versionnumber",
    WebSiteURL = "websiteurl",
    YomiName = "yominame",
}


// Enum AccountCategoryCode
export const enum AccountCategoryCodeCode {
    PreferredCustomer = 1,
    Standard = 2,
}
// Enum AccountClassificationCode
export const enum AccountClassificationCodeCode {
    DefaultValue = 1,
}
// Enum AccountRatingCode
export const enum AccountRatingCodeCode {
    DefaultValue = 1,
}
// Enum Address1AddressTypeCode
export const enum Address1AddressTypeCodeCode {
    BillTo = 1,
    ShipTo = 2,
    Primary = 3,
    Other = 4,
}
// Enum Address1FreightTermsCode
export const enum Address1FreightTermsCodeCode {
    FOB = 1,
    NoCharge = 2,
}
// Enum Address1ShippingMethodCode
export const enum Address1ShippingMethodCodeCode {
    Airborne = 1,
    DHL = 2,
    FedEx = 3,
    UPS = 4,
    PostalMail = 5,
    FullLoad = 6,
    WillCall = 7,
}
// Enum Address2AddressTypeCode
export const enum Address2AddressTypeCodeCode {
    DefaultValue = 1,
}
// Enum Address2FreightTermsCode
export const enum Address2FreightTermsCodeCode {
    DefaultValue = 1,
}
// Enum Address2ShippingMethodCode
export const enum Address2ShippingMethodCodeCode {
    DefaultValue = 1,
}
// Enum BusinessTypeCode
export const enum BusinessTypeCodeCode {
    DefaultValue = 1,
}
// Enum CustomerSizeCode
export const enum CustomerSizeCodeCode {
    DefaultValue = 1,
}
// Enum CustomerTypeCode
export const enum CustomerTypeCodeCode {
    Competitor = 1,
    Consultant = 2,
    Customer = 3,
    Investor = 4,
    Partner = 5,
    Influencer = 6,
    Press = 7,
    Prospect = 8,
    Reseller = 9,
    Supplier = 10,
    Vendor = 11,
    Other = 12,
}
// Enum FisiOptionSetWithPublisher
export const enum FisiOptionSetWithPublisherCode {
    Hallo = 100000000,
    Welt = 100000001,
}
// Enum IndustryCode
export const enum IndustryCodeCode {
    Accounting = 1,
    AgricultureAndNonPetrolNaturalResourceExtraction = 2,
    BroadcastingPrintingAndPublishing = 3,
    Brokers = 4,
    BuildingSupplyRetail = 5,
    BusinessServices = 6,
    Consulting = 7,
    ConsumerServices = 8,
    Design_DirectionAndCreativeManagement = 9,
    Distributors_DispatchersAndProcessors = 10,
    Doctor_sOfficesAndClinics = 11,
    DurableManufacturing = 12,
    EatingAndDrinkingPlaces = 13,
    EntertainmentRetail = 14,
    EquipmentRentalAndLeasing = 15,
    Financial = 16,
    FoodAndTobaccoProcessing = 17,
    InboundCapitalIntensiveProcessing = 18,
    InboundRepairAndServices = 19,
    Insurance = 20,
    LegalServices = 21,
    NonDurableMerchandiseRetail = 22,
    OutboundConsumerService = 23,
    PetrochemicalExtractionAndDistribution = 24,
    ServiceRetail = 25,
    SIGAffiliations = 26,
    SocialServices = 27,
    SpecialOutboundTradeContractors = 28,
    SpecialtyRealty = 29,
    Transportation = 30,
    UtilityCreationAndDistribution = 31,
    VehicleRetail = 32,
    Wholesale = 33,
}
// Enum OwnershipCode
export const enum OwnershipCodeCode {
    Public = 1,
    Private = 2,
    Subsidiary = 3,
    Other = 4,
}
// Enum PaymentTermsCode
export const enum PaymentTermsCodeCode {
    Net30 = 1,
    _2_percent_10_Net30 = 2,
    Net45 = 3,
    Net60 = 4,
}
// Enum PreferredAppointmentDayCode
export const enum PreferredAppointmentDayCodeCode {
    Sunday = 0,
    Monday = 1,
    Tuesday = 2,
    Wednesday = 3,
    Thursday = 4,
    Friday = 5,
    Saturday = 6,
}
// Enum PreferredAppointmentTimeCode
export const enum PreferredAppointmentTimeCodeCode {
    Morning = 1,
    Afternoon = 2,
    Evening = 3,
}
// Enum PreferredContactMethodCode
export const enum PreferredContactMethodCodeCode {
    Any = 1,
    Email = 2,
    Phone = 3,
    Fax = 4,
    Mail = 5,
}
// Enum ShippingMethodCode
export const enum ShippingMethodCodeCode {
    DefaultValue = 1,
}
// Enum TerritoryCode
export const enum TerritoryCodeCode {
    DefaultValue = 1,
}

