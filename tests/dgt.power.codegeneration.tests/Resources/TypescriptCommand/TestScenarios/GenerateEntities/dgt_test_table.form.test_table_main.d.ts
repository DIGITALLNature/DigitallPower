/* eslint-disable */

declare namespace XrmTable.DgtTestTable {
    export interface TestTableMainFormContext extends Xrm.FormContext {
        getAttribute(): Xrm.Attributes.Attribute[];
        getAttribute<T extends Xrm.Attributes.Attribute>(attributeName: string): T;
        getAttribute(attributeName: string): Xrm.Attributes.Attribute;
        getAttribute(index: number): Xrm.Attributes.Attribute;

        getControl(): Xrm.Controls.Control[];
        getControl<T extends Xrm.Controls.Control>(controlName: string): T;
        getControl(controlName: string): Xrm.Controls.Control;
        getControl(index: number): Xrm.Controls.Control;

        getAttribute(name: "dgt_autonumber"): Xrm.Attributes.StringAttribute;

        getControl(name: "dgt_autonumber"): Xrm.Controls.StringAttribute;

        getAttribute(name: "dgt_choice_bool"): Xrm.Attributes.BooleanAttribute;

        getControl(name: "dgt_choice_bool"): Xrm.Controls.BooleanAttribute;

        getAttribute(name: "dgt_choice_multiple"): Xrm.Attributes.MultiSelectOptionSetAttribute;

        getControl(name: "dgt_choice_multiple"): Xrm.Controls.MultiSelectOptionSetAttribute;

        getAttribute(name: "dgt_choice_multiple_global"): Xrm.Attributes.MultiSelectOptionSetAttribute;

        getControl(name: "dgt_choice_multiple_global"): Xrm.Controls.MultiSelectOptionSetAttribute;

        getAttribute(name: "dgt_choice_single"): Xrm.Attributes.OptionSetAttribute;

        getControl(name: "dgt_choice_single"): Xrm.Controls.OptionSetAttribute;

        getAttribute(name: "dgt_choice_single_global"): Xrm.Attributes.OptionSetAttribute;

        getControl(name: "dgt_choice_single_global"): Xrm.Controls.OptionSetAttribute;

        getAttribute(name: "dgt_currency"): Xrm.Attributes.NumberAttribute;

        getControl(name: "dgt_currency"): Xrm.Controls.NumberAttribute;

        getAttribute(name: "dgt_date_and_time"): Xrm.Attributes.DateAttribute;

        getControl(name: "dgt_date_and_time"): Xrm.Controls.DateAttribute;

        getAttribute(name: "dgt_date_only"): Xrm.Attributes.DateAttribute;

        getControl(name: "dgt_date_only"): Xrm.Controls.DateAttribute;

        getAttribute(name: "dgt_file"): Xrm.Attributes.Attribute;

        getControl(name: "dgt_file"): Xrm.Controls.Attribute;

        getAttribute(name: "dgt_file_image"): Xrm.Attributes.Attribute;

        getControl(name: "dgt_file_image"): Xrm.Controls.Attribute;

        getAttribute(name: "dgt_file_image_partial"): Xrm.Attributes.Attribute;

        getControl(name: "dgt_file_image_partial"): Xrm.Controls.Attribute;

        getAttribute(name: "dgt_lookup"): Xrm.Attributes.LookupAttribute;

        getControl(name: "dgt_lookup"): Xrm.Controls.LookupAttribute;

        getAttribute(name: "dgt_lookup_customer"): Xrm.Attributes.LookupAttribute;

        getControl(name: "dgt_lookup_customer"): Xrm.Controls.LookupAttribute;

        getAttribute(name: "dgt_number_decimal"): Xrm.Attributes.NumberAttribute;

        getControl(name: "dgt_number_decimal"): Xrm.Controls.NumberAttribute;

        getAttribute(name: "dgt_number_duration"): Xrm.Attributes.NumberAttribute;

        getControl(name: "dgt_number_duration"): Xrm.Controls.NumberAttribute;

        getAttribute(name: "dgt_number_float"): Xrm.Attributes.NumberAttribute;

        getControl(name: "dgt_number_float"): Xrm.Controls.NumberAttribute;

        getAttribute(name: "dgt_number_language_code"): Xrm.Attributes.NumberAttribute;

        getControl(name: "dgt_number_language_code"): Xrm.Controls.NumberAttribute;

        getAttribute(name: "dgt_number_time_zone"): Xrm.Attributes.NumberAttribute;

        getControl(name: "dgt_number_time_zone"): Xrm.Controls.NumberAttribute;

        getAttribute(name: "dgt_number_whole"): Xrm.Attributes.NumberAttribute;

        getControl(name: "dgt_number_whole"): Xrm.Controls.NumberAttribute;

        getAttribute(name: "dgt_primaryname"): Xrm.Attributes.StringAttribute;

        getControl(name: "dgt_primaryname"): Xrm.Controls.StringAttribute;

        getAttribute(name: "dgt_text_area"): Xrm.Attributes.StringAttribute;

        getControl(name: "dgt_text_area"): Xrm.Controls.StringAttribute;

        getAttribute(name: "dgt_text_email"): Xrm.Attributes.StringAttribute;

        getControl(name: "dgt_text_email"): Xrm.Controls.StringAttribute;

        getAttribute(name: "dgt_text_multiline"): Xrm.Attributes.Attribute;

        getControl(name: "dgt_text_multiline"): Xrm.Controls.Attribute;

        getAttribute(name: "dgt_text_rich"): Xrm.Attributes.StringAttribute;

        getControl(name: "dgt_text_rich"): Xrm.Controls.StringAttribute;

        getAttribute(name: "dgt_text_rich_multiline"): Xrm.Attributes.Attribute;

        getControl(name: "dgt_text_rich_multiline"): Xrm.Controls.Attribute;

        getAttribute(name: "dgt_text_ticker_symbol"): Xrm.Attributes.StringAttribute;

        getControl(name: "dgt_text_ticker_symbol"): Xrm.Controls.StringAttribute;

        getAttribute(name: "dgt_text_url"): Xrm.Attributes.StringAttribute;

        getControl(name: "dgt_text_url"): Xrm.Controls.StringAttribute;

        getAttribute(name: "dgt_textphonenumber"): Xrm.Attributes.StringAttribute;

        getControl(name: "dgt_textphonenumber"): Xrm.Controls.StringAttribute;

        /// <summary>
        /// Owner Id
        /// </summary>
        getAttribute(name: "ownerid"): Xrm.Attributes.LookupAttribute;

        /// <summary>
        /// Owner Id
        /// </summary>
        getControl(name: "ownerid"): Xrm.Controls.LookupAttribute;

        /// <summary>
        /// Reason for the status of the TestTable
        /// </summary>
        getAttribute(name: "statuscode"): Xrm.Attributes.OptionSetAttribute;

        /// <summary>
        /// Reason for the status of the TestTable
        /// </summary>
        getControl(name: "statuscode"): Xrm.Controls.OptionSetAttribute;

        ui: TestTableMainUi;
    }

    export interface TestTableMainUi extends Xrm.Ui {
        tabs: TestTableMainTabs;
    }

    export interface TestTableMainTabs extends Xrm.Collection.ItemCollection<Xrm.Controls.Tab> {
        get(delegate: Xrm.Collection.MatchingDelegate<Xrm.Controls.Tab>): Xrm.Controls.Tab[];
        get(itemNumber: number): Xrm.Controls.Tab;
        get<TSubType extends Xrm.Controls.Tab>(itemNumber: number): TSubType;
        get(itemName: string): Xrm.Controls.Tab;
        get<TSubType extends Xrm.Controls.Tab>(attributeName: string): TSubType;
        get(): Xrm.Controls.Tab[];

        sections: TestTableMain_381eabba5e55425dA69aBd052e952938Sections;

        get(itemName: "{381eabba-5e55-425d-a69a-bd052e952938}"): TestTableMain_381eabba5e55425dA69aBd052e952938;
    }

    export interface TestTableMain_381eabba5e55425dA69aBd052e952938 extends Xrm.Controls.Tab {
        sections: TestTableMain_381eabba5e55425dA69aBd052e952938Sections;
    }

    export interface TestTableMain_381eabba5e55425dA69aBd052e952938Sections extends Xrm.Collection.ItemCollection<Xrm.Controls.Section> {
        get(delegate: Xrm.Collection.MatchingDelegate<Xrm.Controls.Section>): Xrm.Controls.Section[];
        get(itemNumber: number): Xrm.Controls.Section;
        get<TSubType extends Xrm.Controls.Section>(itemNumber: number): TSubType;
        get(itemName: string): Xrm.Controls.Section;
        get<TSubType extends Xrm.Controls.Section>(attributeName: string): TSubType;
        get(): Xrm.Controls.Section[];

        get(itemName: "{de07a47a-5b49-4f20-9c4e-9db58fc26ea5}"): Xrm.Controls.Section;
    }
}
