declare namespace XrmForm.Tester {
    export type XrmFormMockControlType =
        | "boolean"
        | "date"
        | "gridControl"
        | "lookup"
        | "multiSet"
        | "number"
        | "optionSet"
        | "quickView"
        | "string";
    export type XrmFormMockAttributeType = "boolean" | "date" | "lookup" | "multiSet" | "number" | "optionSet" | "string";

    export interface XrmFormMockControl<CtlNames extends string, AttNames extends string> {
        attributeName?: AttNames;
        isVisible?: boolean;
        isDisabled?: boolean;
        name: CtlNames;
        type: XrmFormMockControlType;
    }

    export interface XrmMockAttributeValue {
        valueBoolean?: boolean;
        valueString?: string;
        valueNumber?: number;
        valueDate?: Date;
        valueLookup?: Xrm.LookupValue[];
    }

    export interface XrmFormMockAttribute<AttNames extends string> {
        name: AttNames;
        value?: XrmMockAttributeValue;
        requiredLevel?: Xrm.Attributes.RequirementLevel;
        type: XrmFormMockAttributeType;
        options?: Xrm.OptionSetValue[];
        isDirty?: boolean;
    }

    export interface XrmFormMockTabSection<SectionNames extends string, CtlNames extends string> {
        name: SectionNames;
        label?: string;
        isVisible?: boolean;
        controlNames: CtlNames[];
    }
    export interface XrmFormMockTab<TabNames extends string, SectionNames extends string, CtlNames extends string> {
        name: TabNames;
        label?: string;
        isVisible?: boolean;
        displayState?: Xrm.DisplayState;
        parent?: Xrm.Ui;
        sectionNames: SectionNames[];
        sections: XrmFormMockTabSection<SectionNames, CtlNames>[];
    }
}
