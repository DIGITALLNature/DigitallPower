/* eslint-disable */

declare namespace Xrm.Events {
    export interface EventContext {
        getFormContext<DgtTestTableFormContext>(): DgtTestTableFormContext;
    }
}

declare namespace XrmTable.DgtTestTable {
    export interface FormContext extends Xrm.FormContext {
        getAttribute(): Xrm.Collection.ItemCollection<Attributes.Attribute> | null;

        getControl(): Xrm.Collection.ItemCollection<Controls.Control> | null;

        /// <summary>
        /// Unique identifier of the user who created the record.
        /// </summary>
        getAttribute(name: "createdby"): Xrm.Attributes.LookupAttribute;

        /// <summary>
        /// Unique identifier of the user who created the record.
        /// </summary>
        getControl(name: "createdby"): Xrm.Controls.LookupControl;

        /// <summary>
        /// Date and time when the record was created.
        /// </summary>
        getAttribute(name: "createdon"): Xrm.Attributes.DateAttribute;

        /// <summary>
        /// Date and time when the record was created.
        /// </summary>
        getControl(name: "createdon"): Xrm.Controls.DateControl;

        /// <summary>
        /// Unique identifier of the delegate user who created the record.
        /// </summary>
        getAttribute(name: "createdonbehalfby"): Xrm.Attributes.LookupAttribute;

        /// <summary>
        /// Unique identifier of the delegate user who created the record.
        /// </summary>
        getControl(name: "createdonbehalfby"): Xrm.Controls.LookupControl;

        getAttribute(name: "dgt_autonumber"): Xrm.Attributes.StringAttribute;

        getControl(name: "dgt_autonumber"): Xrm.Controls.StringControl;

        getAttribute(name: "dgt_choice_bool"): Xrm.Attributes.BooleanAttribute;

        getControl(name: "dgt_choice_bool"): Xrm.Controls.BooleanControl;

        getAttribute(name: "dgt_choice_multiple"): Xrm.Attributes.MultiSelectOptionSetAttribute;

        getControl(name: "dgt_choice_multiple"): Xrm.Controls.MultiSelectOptionSetControl;

        getAttribute(name: "dgt_choice_multiple_global"): Xrm.Attributes.MultiSelectOptionSetAttribute;

        getControl(name: "dgt_choice_multiple_global"): Xrm.Controls.MultiSelectOptionSetControl;

        getAttribute(name: "dgt_choice_single"): Xrm.Attributes.OptionSetAttribute;

        getControl(name: "dgt_choice_single"): Xrm.Controls.OptionSetControl;

        getAttribute(name: "dgt_choice_single_global"): Xrm.Attributes.OptionSetAttribute;

        getControl(name: "dgt_choice_single_global"): Xrm.Controls.OptionSetControl;

        getAttribute(name: "dgt_currency"): Xrm.Attributes.NumberAttribute;

        getControl(name: "dgt_currency"): Xrm.Controls.NumberControl;

        /// <summary>
        /// Value of the Currency in base currency.
        /// </summary>
        getAttribute(name: "dgt_currency_base"): Xrm.Attributes.NumberAttribute;

        /// <summary>
        /// Value of the Currency in base currency.
        /// </summary>
        getControl(name: "dgt_currency_base"): Xrm.Controls.NumberControl;

        getAttribute(name: "dgt_date_and_time"): Xrm.Attributes.DateAttribute;

        getControl(name: "dgt_date_and_time"): Xrm.Controls.DateControl;

        getAttribute(name: "dgt_date_only"): Xrm.Attributes.DateAttribute;

        getControl(name: "dgt_date_only"): Xrm.Controls.DateControl;

        getAttribute(name: "dgt_file"): Xrm.Attributes.Attribute;

        getControl(name: "dgt_file"): Xrm.Controls.Control;

        getAttribute(name: "dgt_file_image"): Xrm.Attributes.Attribute;

        getControl(name: "dgt_file_image"): Xrm.Controls.Control;

        getAttribute(name: "dgt_file_image_partial"): Xrm.Attributes.Attribute;

        getControl(name: "dgt_file_image_partial"): Xrm.Controls.Control;

        getAttribute(name: "dgt_file_image_partial_timestamp"): Xrm.Attributes.NumberAttribute;

        getControl(name: "dgt_file_image_partial_timestamp"): Xrm.Controls.NumberControl;

        getAttribute(name: "dgt_file_image_partial_url"): Xrm.Attributes.StringAttribute;

        getControl(name: "dgt_file_image_partial_url"): Xrm.Controls.StringControl;

        getAttribute(name: "dgt_file_image_partialid"): Xrm.Attributes.Attribute;

        getControl(name: "dgt_file_image_partialid"): Xrm.Controls.Control;

        getAttribute(name: "dgt_file_image_timestamp"): Xrm.Attributes.NumberAttribute;

        getControl(name: "dgt_file_image_timestamp"): Xrm.Controls.NumberControl;

        getAttribute(name: "dgt_file_image_url"): Xrm.Attributes.StringAttribute;

        getControl(name: "dgt_file_image_url"): Xrm.Controls.StringControl;

        getAttribute(name: "dgt_file_imageid"): Xrm.Attributes.Attribute;

        getControl(name: "dgt_file_imageid"): Xrm.Controls.Control;

        getAttribute(name: "dgt_file_name"): Xrm.Attributes.StringAttribute;

        getControl(name: "dgt_file_name"): Xrm.Controls.StringControl;

        getAttribute(name: "dgt_lookup"): Xrm.Attributes.LookupAttribute;

        getControl(name: "dgt_lookup"): Xrm.Controls.LookupControl;

        getAttribute(name: "dgt_lookup_customer"): Xrm.Attributes.LookupAttribute;

        getControl(name: "dgt_lookup_customer"): Xrm.Controls.LookupControl;

        getAttribute(name: "dgt_number_decimal"): Xrm.Attributes.NumberAttribute;

        getControl(name: "dgt_number_decimal"): Xrm.Controls.NumberControl;

        getAttribute(name: "dgt_number_duration"): Xrm.Attributes.NumberAttribute;

        getControl(name: "dgt_number_duration"): Xrm.Controls.NumberControl;

        getAttribute(name: "dgt_number_float"): Xrm.Attributes.NumberAttribute;

        getControl(name: "dgt_number_float"): Xrm.Controls.NumberControl;

        getAttribute(name: "dgt_number_language_code"): Xrm.Attributes.NumberAttribute;

        getControl(name: "dgt_number_language_code"): Xrm.Controls.NumberControl;

        getAttribute(name: "dgt_number_time_zone"): Xrm.Attributes.NumberAttribute;

        getControl(name: "dgt_number_time_zone"): Xrm.Controls.NumberControl;

        getAttribute(name: "dgt_number_whole"): Xrm.Attributes.NumberAttribute;

        getControl(name: "dgt_number_whole"): Xrm.Controls.NumberControl;

        getAttribute(name: "dgt_primaryname"): Xrm.Attributes.StringAttribute;

        getControl(name: "dgt_primaryname"): Xrm.Controls.StringControl;

        /// <summary>
        /// Unique identifier for entity instances
        /// </summary>
        getAttribute(name: "dgt_test_tableid"): Xrm.Attributes.Attribute;

        /// <summary>
        /// Unique identifier for entity instances
        /// </summary>
        getControl(name: "dgt_test_tableid"): Xrm.Controls.Control;

        getAttribute(name: "dgt_text_area"): Xrm.Attributes.StringAttribute;

        getControl(name: "dgt_text_area"): Xrm.Controls.StringControl;

        getAttribute(name: "dgt_text_email"): Xrm.Attributes.StringAttribute;

        getControl(name: "dgt_text_email"): Xrm.Controls.StringControl;

        getAttribute(name: "dgt_text_multiline"): Xrm.Attributes.StringAttribute;

        getControl(name: "dgt_text_multiline"): Xrm.Controls.StringControl;

        getAttribute(name: "dgt_text_rich"): Xrm.Attributes.StringAttribute;

        getControl(name: "dgt_text_rich"): Xrm.Controls.StringControl;

        getAttribute(name: "dgt_text_rich_multiline"): Xrm.Attributes.StringAttribute;

        getControl(name: "dgt_text_rich_multiline"): Xrm.Controls.StringControl;

        getAttribute(name: "dgt_text_ticker_symbol"): Xrm.Attributes.StringAttribute;

        getControl(name: "dgt_text_ticker_symbol"): Xrm.Controls.StringControl;

        getAttribute(name: "dgt_text_url"): Xrm.Attributes.StringAttribute;

        getControl(name: "dgt_text_url"): Xrm.Controls.StringControl;

        getAttribute(name: "dgt_textphonenumber"): Xrm.Attributes.StringAttribute;

        getControl(name: "dgt_textphonenumber"): Xrm.Controls.StringControl;

        /// <summary>
        /// Exchange rate for the currency associated with the entity with respect to the base currency.
        /// </summary>
        getAttribute(name: "exchangerate"): Xrm.Attributes.NumberAttribute;

        /// <summary>
        /// Exchange rate for the currency associated with the entity with respect to the base currency.
        /// </summary>
        getControl(name: "exchangerate"): Xrm.Controls.NumberControl;

        /// <summary>
        /// Sequence number of the import that created this record.
        /// </summary>
        getAttribute(name: "importsequencenumber"): Xrm.Attributes.NumberAttribute;

        /// <summary>
        /// Sequence number of the import that created this record.
        /// </summary>
        getControl(name: "importsequencenumber"): Xrm.Controls.NumberControl;

        /// <summary>
        /// Unique identifier of the user who modified the record.
        /// </summary>
        getAttribute(name: "modifiedby"): Xrm.Attributes.LookupAttribute;

        /// <summary>
        /// Unique identifier of the user who modified the record.
        /// </summary>
        getControl(name: "modifiedby"): Xrm.Controls.LookupControl;

        /// <summary>
        /// Date and time when the record was modified.
        /// </summary>
        getAttribute(name: "modifiedon"): Xrm.Attributes.DateAttribute;

        /// <summary>
        /// Date and time when the record was modified.
        /// </summary>
        getControl(name: "modifiedon"): Xrm.Controls.DateControl;

        /// <summary>
        /// Unique identifier of the delegate user who modified the record.
        /// </summary>
        getAttribute(name: "modifiedonbehalfby"): Xrm.Attributes.LookupAttribute;

        /// <summary>
        /// Unique identifier of the delegate user who modified the record.
        /// </summary>
        getControl(name: "modifiedonbehalfby"): Xrm.Controls.LookupControl;

        /// <summary>
        /// Date and time that the record was migrated.
        /// </summary>
        getAttribute(name: "overriddencreatedon"): Xrm.Attributes.DateAttribute;

        /// <summary>
        /// Date and time that the record was migrated.
        /// </summary>
        getControl(name: "overriddencreatedon"): Xrm.Controls.DateControl;

        /// <summary>
        /// Owner Id
        /// </summary>
        getAttribute(name: "ownerid"): Xrm.Attributes.LookupAttribute;

        /// <summary>
        /// Owner Id
        /// </summary>
        getControl(name: "ownerid"): Xrm.Controls.LookupControl;

        /// <summary>
        /// Unique identifier for the business unit that owns the record
        /// </summary>
        getAttribute(name: "owningbusinessunit"): Xrm.Attributes.LookupAttribute;

        /// <summary>
        /// Unique identifier for the business unit that owns the record
        /// </summary>
        getControl(name: "owningbusinessunit"): Xrm.Controls.LookupControl;

        /// <summary>
        /// Unique identifier for the team that owns the record.
        /// </summary>
        getAttribute(name: "owningteam"): Xrm.Attributes.LookupAttribute;

        /// <summary>
        /// Unique identifier for the team that owns the record.
        /// </summary>
        getControl(name: "owningteam"): Xrm.Controls.LookupControl;

        /// <summary>
        /// Unique identifier for the user that owns the record.
        /// </summary>
        getAttribute(name: "owninguser"): Xrm.Attributes.LookupAttribute;

        /// <summary>
        /// Unique identifier for the user that owns the record.
        /// </summary>
        getControl(name: "owninguser"): Xrm.Controls.LookupControl;

        /// <summary>
        /// Status of the TestTable
        /// </summary>
        getAttribute(name: "statecode"): Xrm.Attributes.OptionSetAttribute;

        /// <summary>
        /// Status of the TestTable
        /// </summary>
        getControl(name: "statecode"): Xrm.Controls.OptionSetControl;

        /// <summary>
        /// Reason for the status of the TestTable
        /// </summary>
        getAttribute(name: "statuscode"): Xrm.Attributes.OptionSetAttribute;

        /// <summary>
        /// Reason for the status of the TestTable
        /// </summary>
        getControl(name: "statuscode"): Xrm.Controls.OptionSetControl;

        /// <summary>
        /// For internal use only.
        /// </summary>
        getAttribute(name: "timezoneruleversionnumber"): Xrm.Attributes.NumberAttribute;

        /// <summary>
        /// For internal use only.
        /// </summary>
        getControl(name: "timezoneruleversionnumber"): Xrm.Controls.NumberControl;

        /// <summary>
        /// Unique identifier of the currency associated with the entity.
        /// </summary>
        getAttribute(name: "transactioncurrencyid"): Xrm.Attributes.LookupAttribute;

        /// <summary>
        /// Unique identifier of the currency associated with the entity.
        /// </summary>
        getControl(name: "transactioncurrencyid"): Xrm.Controls.LookupControl;

        /// <summary>
        /// Time zone code that was in use when the record was created.
        /// </summary>
        getAttribute(name: "utcconversiontimezonecode"): Xrm.Attributes.NumberAttribute;

        /// <summary>
        /// Time zone code that was in use when the record was created.
        /// </summary>
        getControl(name: "utcconversiontimezonecode"): Xrm.Controls.NumberControl;

        /// <summary>
        /// Version Number
        /// </summary>
        getAttribute(name: "versionnumber"): Xrm.Attributes.NumberAttribute;

        /// <summary>
        /// Version Number
        /// </summary>
        getControl(name: "versionnumber"): Xrm.Controls.NumberControl;

    }

    export const enum Metadata {
        TypeName = "mscrm.dgt_test_table",
        LogicalName = "dgt_test_table",
        CollectionName = "dgt_test_tables",
        PrimaryIdAttribute = "dgt_test_tableid",
    }

    export const enum AttributeTypes {
        CreatedBy = "Lookup",
        CreatedOn = "DateAndTime:UserLocal",
        CreatedOnBehalfBy = "Lookup",
        DgtAutonumber = "String",
        DgtChoiceBool = "Optionset",
        DgtChoiceMultiple = "MultiSelectOptionSet",
        DgtChoiceMultipleGlobal = "MultiSelectOptionSet",
        DgtChoiceSingle = "OptionSet",
        DgtChoiceSingleGlobal = "OptionSet",
        DgtCurrency = "Integer",
        DgtCurrencyBase = "Integer",
        DgtDateAndTime = "DateAndTime:UserLocal",
        DgtDateOnly = "DateOnly:TimeZoneIndependent",
        DgtFile = "Attribute",
        DgtFileImage = "Attribute",
        DgtFileImagePartial = "Attribute",
        DgtFileImagePartialTimestamp = "BigInt",
        DgtFileImagePartialURL = "String",
        DgtFileImagePartialId = "Guid",
        DgtFileImageTimestamp = "BigInt",
        DgtFileImageURL = "String",
        DgtFileImageId = "Guid",
        DgtFileName = "String",
        DgtLookup = "Lookup",
        DgtLookupCustomer = "Lookup",
        DgtNumberDecimal = "Decimal",
        DgtNumberDuration = "Integer",
        DgtNumberFloat = "Double",
        DgtNumberLanguageCode = "Integer",
        DgtNumberTimeZone = "Integer",
        DgtNumberWhole = "Integer",
        DgtPrimaryName = "String",
        DgtTestTableId = "Guid",
        DgtTextArea = "String",
        DgtTextEmail = "String",
        DgtTextMultiline = "String",
        DgtTextRich = "String",
        DgtTextRichMultiline = "String",
        DgtTextTickerSymbol = "String",
        DgtTextUrl = "String",
        DgtTextPhoneNumber = "String",
        ExchangeRate = "Decimal",
        ImportSequenceNumber = "Integer",
        ModifiedBy = "Lookup",
        ModifiedOn = "DateAndTime:UserLocal",
        ModifiedOnBehalfBy = "Lookup",
        OverriddenCreatedOn = "DateAndTime:UserLocal",
        OwnerId = "Lookup",
        OwningBusinessUnit = "Lookup",
        OwningTeam = "Lookup",
        OwningUser = "Lookup",
        Statecode = "OptionSet",
        Statuscode = "OptionSet",
        TimeZoneRuleVersionNumber = "Integer",
        TransactionCurrencyId = "Lookup",
        UTCConversionTimeZoneCode = "Integer",
        VersionNumber = "BigInt",
    }

    export const enum Attributes {
        CreatedBy = "createdby",
        CreatedOn = "createdon",
        CreatedOnBehalfBy = "createdonbehalfby",
        DgtAutonumber = "dgt_autonumber",
        DgtChoiceBool = "dgt_choice_bool",
        DgtChoiceMultiple = "dgt_choice_multiple",
        DgtChoiceMultipleGlobal = "dgt_choice_multiple_global",
        DgtChoiceSingle = "dgt_choice_single",
        DgtChoiceSingleGlobal = "dgt_choice_single_global",
        DgtCurrency = "dgt_currency",
        DgtCurrencyBase = "dgt_currency_base",
        DgtDateAndTime = "dgt_date_and_time",
        DgtDateOnly = "dgt_date_only",
        DgtFile = "dgt_file",
        DgtFileImage = "dgt_file_image",
        DgtFileImagePartial = "dgt_file_image_partial",
        DgtFileImagePartialTimestamp = "dgt_file_image_partial_timestamp",
        DgtFileImagePartialURL = "dgt_file_image_partial_url",
        DgtFileImagePartialId = "dgt_file_image_partialid",
        DgtFileImageTimestamp = "dgt_file_image_timestamp",
        DgtFileImageURL = "dgt_file_image_url",
        DgtFileImageId = "dgt_file_imageid",
        DgtFileName = "dgt_file_name",
        DgtLookup = "dgt_lookup",
        DgtLookupCustomer = "dgt_lookup_customer",
        DgtNumberDecimal = "dgt_number_decimal",
        DgtNumberDuration = "dgt_number_duration",
        DgtNumberFloat = "dgt_number_float",
        DgtNumberLanguageCode = "dgt_number_language_code",
        DgtNumberTimeZone = "dgt_number_time_zone",
        DgtNumberWhole = "dgt_number_whole",
        DgtPrimaryName = "dgt_primaryname",
        DgtTestTableId = "dgt_test_tableid",
        DgtTextArea = "dgt_text_area",
        DgtTextEmail = "dgt_text_email",
        DgtTextMultiline = "dgt_text_multiline",
        DgtTextRich = "dgt_text_rich",
        DgtTextRichMultiline = "dgt_text_rich_multiline",
        DgtTextTickerSymbol = "dgt_text_ticker_symbol",
        DgtTextUrl = "dgt_text_url",
        DgtTextPhoneNumber = "dgt_textphonenumber",
        ExchangeRate = "exchangerate",
        ImportSequenceNumber = "importsequencenumber",
        ModifiedBy = "modifiedby",
        ModifiedOn = "modifiedon",
        ModifiedOnBehalfBy = "modifiedonbehalfby",
        OverriddenCreatedOn = "overriddencreatedon",
        OwnerId = "ownerid",
        OwningBusinessUnit = "owningbusinessunit",
        OwningTeam = "owningteam",
        OwningUser = "owninguser",
        Statecode = "statecode",
        Statuscode = "statuscode",
        TimeZoneRuleVersionNumber = "timezoneruleversionnumber",
        TransactionCurrencyId = "transactioncurrencyid",
        UTCConversionTimeZoneCode = "utcconversiontimezonecode",
        VersionNumber = "versionnumber",
    }

    export const enum DgtChoiceSingleCode {
        Value = 283510000,
    }

    export const enum DgtChoiceSingleGlobalCode {
        Value = 283510000,
    }

}
