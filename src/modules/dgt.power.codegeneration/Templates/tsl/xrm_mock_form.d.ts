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

    export interface XrmFormMockControl {
        attributeName?: string;
        isVisible?: boolean;
        isDisabled?: boolean;
        name: string;
        type: XrmFormMockControlType;
    }

    export interface XrmMockAttributeValue {
        valueBoolean?: boolean;
        valueString?: string;
        valueNumber?: number;
        valueDate?: Date;
        valueLookup?: Xrm.LookupValue | Xrm.LookupValue[];
    }

    export interface XrmFormMockAttributeControlUpdateBase {
        isDisabled?: boolean;
        isVisible?: boolean;
        controlAttributeName: string;
        controlSpecificName?: string;
        requiredLevel?: XrmEnum.RequirementLevel;
        value?: XrmMockAttributeValue;
    }

    export interface XrmFormMockAttribute {
        name: string;
        value?: XrmMockAttributeValue;
        requiredLevel?: XrmEnum.RequirementLevel;
        type: XrmFormMockAttributeType;
    }

    export interface XrmFormMockTabSection {
        name: string;
        label?: string;
        isVisible?: boolean;
        controlNames: string[];
    }
    export interface XrmFormMockTab {
        name: string;
        label?: string;
        isVisible?: boolean;
        displayState?: Xrm.DisplayState;
        parent?: Xrm.Ui;
        sectionNames: string[];
        sections: XrmFormMockTabSection[];
    }
}
