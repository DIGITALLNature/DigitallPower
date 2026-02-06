import {
    BooleanAttributeMock,
    BooleanControlMock,
    DateAttributeMock,
    DateControlMock,
    EventContextMock,
    ItemCollectionMock,
    LookupAttributeMock,
    LookupControlMock,
    NumberAttributeMock,
    NumberControlMock,
    OptionSetAttributeMock,
    OptionSetControlMock,
    StringAttributeMock,
    StringControlMock,
    TabMock,
    XrmMockGenerator,
} from "xrm-mock";
import { EventContextWithEventArgsMock } from "xrm-mock/dist/xrm-mock/events/eventcontextwitheventargs.mock";
import { IXrmMockFormTestContextBuilder, XrmMockAttributeType, XrmMockControlType } from "./xrm_mock_form_test_context_types";

/** Auxiliary class for simplified xrm-mock testing */
export class XrmMockFormTestContextBuilder implements IXrmMockFormTestContextBuilder {
    private readonly mockAttributes: Map<string, XrmMockAttributeType>;
    private readonly mockControls: Map<string, XrmMockControlType>;
    private readonly controlConfig: XrmForm.Tester.XrmFormMockControl[];
    private readonly attributeConfig: XrmForm.Tester.XrmFormMockAttribute[];
    private readonly tabConfig: XrmForm.Tester.XrmFormMockTab[];

    /**
     * @summary Default constructor function
     * @param initialControlsConfig Configuration for control names/ types / status and related attributes
     * @param initialAttributesConfig Configuration for attribute names/ types / values, required level
     * @param initialTabsConfig Configuration for tab/section/controls
     */
    constructor(
        initialControlsConfig: XrmForm.Tester.XrmFormMockControl[],
        initialAttributesConfig: XrmForm.Tester.XrmFormMockAttribute[],
        initialTabsConfig: XrmForm.Tester.XrmFormMockTab[]
    ) {
        // Gets inputs from the generated files with the form description
        this.mockAttributes = new Map<string, XrmMockAttributeType>();
        this.mockControls = new Map<string, XrmMockControlType>();
        this.controlConfig = [...initialControlsConfig];
        this.attributeConfig = [...initialAttributesConfig];
        this.tabConfig = initialTabsConfig;
    }

    /**
     * @summary Simple function to get the correctly mapped load event with arguments to pass to form load
     * @returns the mock load event to be passed to the onload form event
     */
    public GetLoadExecutionContext(): EventContextWithEventArgsMock<Xrm.Events.LoadEventArguments> {
        const eventContext = XrmMockGenerator.getEventContext() ?? new EventContextMock({});
        const eventArgs: Xrm.Events.LoadEventArguments = {
            getDataLoadState: () => XrmEnum.FormDataLoadState.InitialLoad,
        };

        return new EventContextWithEventArgsMock<Xrm.Events.LoadEventArguments>({
            context: eventContext.context,
            depth: eventContext.depth,
            eventArgs: eventArgs,
            eventSource: eventContext.eventSource,
            formContext: eventContext.formContext,
            sharedVariables: eventContext.sharedVariables,
        });
    }

    /**
     * Modifies the default form context config with the given update data
     * @param updateData Simply array with the control/attributes to be changed
     * @returns the object itself
     */
    public WithData(updateData: XrmForm.Tester.XrmFormMockAttributeControlUpdateBase[]): this {
        this.UpdateControlConfig(updateData);
        return this;
    }

    /**
     * Given the form context config and data it generates the xrm-mock form, needs to be called before the test starts
     * @returns The object itself
     */
    public BuildMockFormContext(): this {
        XrmMockGenerator.initialise();
        this.InitializeMockFormContextControls();
        this.InitTabs();
        return this;
    }

    private UpdateControlConfig(updateAttributes: XrmForm.Tester.XrmFormMockAttributeControlUpdateBase[]): void {
        for (const updateData of updateAttributes) {
            const control = this.controlConfig.find((x) => {
                if (updateData.controlSpecificName) {
                    return x.name === updateData.controlSpecificName;
                }
                return x.attributeName === updateData.controlAttributeName;
            });
            const attribute = this.attributeConfig.find((x) => x.name === updateData.controlAttributeName) ?? null;
            if (updateData.value !== undefined && attribute) {
                attribute.value = updateData.value;
            }
            if (updateData.requiredLevel !== undefined && attribute) {
                attribute.requiredLevel = updateData.requiredLevel;
            }
            if (updateData.isVisible !== undefined && control) {
                control.isVisible = updateData.isVisible;
            }
        }
    }

    private InitializeMockFormContextControls(): void {
        for (const attribute of this.attributeConfig) {
            const attributeMock = XrmMockFormTestContextBuilder.CreateXrmMockFakeAttribute(attribute.name, attribute);
            if (attributeMock) {
                this.mockAttributes.set(attribute.name, attributeMock);
            }
        }
        for (const control of this.controlConfig) {
            const controlMock = this.CreateXrmMockFakeControl(control.name, control);
            if (controlMock) {
                this.mockControls.set(control.name, controlMock);
            }
        }
        const formContext = XrmMockGenerator.getFormContext();
        const attributes = [...this.mockAttributes.values()];
        formContext.data.attributes = new ItemCollectionMock<Xrm.Attributes.Attribute<Xrm.Attributes.AttributeValues>>(
            <Xrm.Attributes.Attribute<Xrm.Attributes.AttributeValues>[]>attributes
        );
    }

    private InitTabs(): this {
        for (const tabSection of this.tabConfig) {
            this.InitTab(tabSection);
        }
        return this;
    }

    private InitTab(tab: XrmForm.Tester.XrmFormMockTab): this {
        const mockTab = XrmMockGenerator.Tab.createTab(tab.name, tab.label, tab.isVisible, tab.displayState, tab.parent);
        for (const tabSection of tab.sections) {
            this.InitTabSection(mockTab, tabSection);
        }
        return this;
    }

    private InitTabSection(tab: TabMock, section: XrmForm.Tester.XrmFormMockTabSection): this {
        const controls = this.FilterControlList(section.controlNames);
        const controlCollection: ItemCollectionMock<Xrm.Controls.Control> = new ItemCollectionMock<Xrm.Controls.Control>(
            <Xrm.Controls.Control[]>controls
        );
        XrmMockGenerator.Section.createSection(section.name, section.label, section.isVisible, tab, controlCollection);
        return this;
    }

    private FilterControlList(controlNames: string[]): XrmMockControlType[] {
        return (
            controlNames
                .map((name) => this.mockControls.get(name))
                .filter((x): x is XrmMockControlType => {
                    return x !== null && x !== undefined;
                }) ?? []
        );
    }

    private static CreateXrmMockFakeAttribute(
        name: string,
        mockAttribute: XrmForm.Tester.XrmFormMockAttribute
    ): XrmMockAttributeType | null {
        switch (mockAttribute.type) {
            case "string":
                return XrmMockGenerator.Attribute.createString({
                    name,
                    value: mockAttribute.value?.valueString,
                });
            case "boolean":
                return XrmMockGenerator.Attribute.createBoolean({ name, value: mockAttribute.value?.valueBoolean });
            case "date":
                return XrmMockGenerator.Attribute.createDate({ name, value: mockAttribute.value?.valueDate });
            case "number":
                return XrmMockGenerator.Attribute.createNumber({ name, value: mockAttribute.value?.valueNumber });
            case "lookup":
                return XrmMockGenerator.Attribute.createLookup(name, mockAttribute.value?.valueLookup);
            case "optionSet":
            case "multiSet":
                return XrmMockGenerator.Attribute.createOptionSet(name, mockAttribute.value?.valueNumber);
            // case "Xrm.Controls.GridControl":
            // case "Xrm.Controls.KbSearchControl":
            // case "Xrm.Control.QuickFormControl":
            // TO BE DONE MOCK GRID CONTROL
            // return null;
            default:
                return null;
        }
    }

    private CreateXrmMockFakeControl(name: string, control: XrmForm.Tester.XrmFormMockControl): XrmMockControlType | null {
        const mockAttribute = control.attributeName ? this.mockAttributes.get(control.attributeName) : null;
        switch (control.type) {
            case "string":
                return XrmMockFormTestContextBuilder.CreateStringMockControl(mockAttribute, control);
            case "boolean":
                return XrmMockFormTestContextBuilder.CreateBooleanMockControl(mockAttribute, control);
            case "date":
                return XrmMockFormTestContextBuilder.CreateDateMockControl(mockAttribute, control);
            case "number":
                return XrmMockFormTestContextBuilder.CreateNumberMockControl(mockAttribute, control);
            case "lookup":
                return XrmMockFormTestContextBuilder.CreateLookupControl(mockAttribute, control);
            case "optionSet":
            case "multiSet":
                return XrmMockFormTestContextBuilder.CreateOptionSetControl(mockAttribute, control);
            case "gridControl":
                return XrmMockGenerator.Control.createGrid(name);
            case "quickView":
                // create function for quick views
                return null;

            // case "Xrm.Controls.KbSearchControl":
            // case "Xrm.Control.QuickFormControl":
            // case "Xrm.Controls.IframeControl":
            // case "Xrm.Controls.StandardControl":
            //     NO MAPPING ON THIS CONTROLS
            //     return null;
            default:
                return null;
        }
    }

    private static CreateStringMockControl(
        mockAttribute: XrmMockAttributeType | null | undefined,
        control: XrmForm.Tester.XrmFormMockControl
    ): StringControlMock | null {
        if (mockAttribute && control.attributeName) {
            return XrmMockGenerator.Control.createString({
                name: control.attributeName,
                attribute: <StringAttributeMock>mockAttribute,
                visible: true,
            });
        }
        return null;
    }

    private static CreateBooleanMockControl(
        mockAttribute: XrmMockAttributeType | null | undefined,
        control: XrmForm.Tester.XrmFormMockControl
    ): BooleanControlMock | null {
        if (mockAttribute && control.attributeName) {
            return XrmMockGenerator.Control.createBoolean({
                name: control.attributeName,
                attribute: <BooleanAttributeMock>mockAttribute,
                visible: true,
            });
        }
        return null;
    }

    private static CreateDateMockControl(
        mockAttribute: XrmMockAttributeType | null | undefined,
        control: XrmForm.Tester.XrmFormMockControl
    ): DateControlMock | null {
        if (mockAttribute && control.attributeName) {
            return XrmMockGenerator.Control.createDate({
                name: control.attributeName,
                attribute: <DateAttributeMock>mockAttribute,
                visible: true,
            });
        }
        return null;
    }

    private static CreateNumberMockControl(
        mockAttribute: XrmMockAttributeType | null | undefined,
        control: XrmForm.Tester.XrmFormMockControl
    ): NumberControlMock | null {
        if (mockAttribute && control.attributeName) {
            return XrmMockGenerator.Control.createNumber({
                name: control.attributeName,
                attribute: <NumberAttributeMock>mockAttribute,
                visible: true,
            });
        }
        return null;
    }

    private static CreateLookupControl(
        mockAttribute: XrmMockAttributeType | null | undefined,
        control: XrmForm.Tester.XrmFormMockControl
    ): LookupControlMock | null {
        if (mockAttribute && control.attributeName) {
            return XrmMockGenerator.Control.createLookup({
                name: control.attributeName,
                attribute: <LookupAttributeMock>mockAttribute,
                visible: true,
            });
        }
        return null;
    }

    private static CreateOptionSetControl(
        mockAttribute: XrmMockAttributeType | null | undefined,
        control: XrmForm.Tester.XrmFormMockControl
    ): OptionSetControlMock | null {
        if (mockAttribute && control.attributeName) {
            return XrmMockGenerator.Control.createOptionSet({
                name: control.attributeName,
                attribute: <OptionSetAttributeMock>mockAttribute,
                visible: true,
            });
        }
        return null;
    }
}
