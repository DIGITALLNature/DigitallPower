/* eslint-disable */

declare namespace Xrm.Events {
    export interface EventContext {
        getFormContext<TestTableMainFormContext>(): TestTableMainFormContext;
    }
}

declare namespace XrmTable.DgtTestTable {
    export interface TestTableMainFormContext extends Xrm.FormContext {
        getAttribute(): Xrm.Collection.ItemCollection<Attributes.Attribute> | null;

        getControl(): Xrm.Collection.ItemCollection<Controls.Control> | null;

        /// <summary>
        /// Reason for the status of the TestTable
        /// </summary>
        getAttribute(name: "statuscode"): Xrm.Attributes.OptionSetAttribute;

        /// <summary>
        /// Reason for the status of the TestTable
        /// </summary>
        getControl(name: "statuscode"): Xrm.Controls.OptionSetControl;

        ui: TestTableMainUi;
    }

    export interface TestTableMainUi extends Xrm.Ui {
        tabs: TestTableMainTabs;
    }

    export interface TestTableMainTabs extends Xrm.Collection.ItemCollection<Xrm.Controls.Tab> {
        sections: TestTableMain_381eabba5e55425dA69aBd052e952938Sections;

        get(itemName: "{381eabba-5e55-425d-a69a-bd052e952938}"): TestTableMain_381eabba5e55425dA69aBd052e952938;
    }

    export interface TestTableMain_381eabba5e55425dA69aBd052e952938 extends Xrm.Controls.Tab {
        sections: TestTableMain_381eabba5e55425dA69aBd052e952938Sections;
    }



    export interface TestTableMain_381eabba5e55425dA69aBd052e952938Sections extends Xrm.Collection.ItemCollection<Xrm.Controls.Section> {
    }
}
