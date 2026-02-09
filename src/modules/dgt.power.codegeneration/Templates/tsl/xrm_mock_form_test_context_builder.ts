import {
    BooleanAttributeMock,
    BooleanControlMock,
    DateAttributeMock,
    DateControlMock,
    EntityMock,
    EventContextMock,
    FormItemMock,
    IEntityComponents,
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
    DataMock,
} from "xrm-mock";
import { EventContextWithEventArgsMock } from "xrm-mock/dist/xrm-mock/events/eventcontextwitheventargs.mock";
import {
    IXrmMockFormTestContextBuilder,
    XrmCustomApiMockData,
    XrmFormMockAttributeControlUpdateBase,
    XrmFormMockServerData,
    XrmMockAttributeType,
    XrmMockConfirmDialogResponses,
    XrmMockControlType,
    XrmMockUiNotification,
    XrmNavigationMockStubs,
    XrmUtilityApiMockStubs,
    XrmWebApiMockStubs,
    XrmWebApiOnlineMockStubs,
} from "./xrm_mock_form_test_context_types";
import { XrmMockFormODataFilter } from "./xrm_mock_form_odata_filter";

/** Auxiliary class for simplified xrm-mock testing */
export class XrmMockFormTestContextBuilder<
    TTabNames extends string,
    TSectionNames extends string,
    TControlName extends string,
    TAttributeNames extends string,
> implements IXrmMockFormTestContextBuilder<TControlName, TAttributeNames>
{
    public static readonly ERROR_NO_CUSTOM_API = "Bad Request";
    public xrmWebApiMockStubs: XrmWebApiMockStubs = {};
    public xrmWebApiOnlineMockStubs: XrmWebApiOnlineMockStubs = {};
    public xrmNavigationMockStubs: XrmNavigationMockStubs = {};
    public xrmUiNotificationsMocks: XrmMockUiNotification = {};
    public xrmUtilityApiMockStubs: XrmUtilityApiMockStubs = {};
    private formEntity: IEntityComponents = {};
    private formType: XrmEnum.FormType = XrmEnum.FormType.Create; //default value
    private confirmDialogConfirmedSeq: XrmMockConfirmDialogResponses[] = [];
    private readonly mockAttributes: Map<TAttributeNames, XrmMockAttributeType>;
    private readonly mockControls: Map<TControlName, XrmMockControlType>;
    private readonly controlConfig: XrmForm.Tester.XrmFormMockControl<TControlName, TAttributeNames>[];
    private readonly attributeConfig: XrmForm.Tester.XrmFormMockAttribute<TAttributeNames>[];
    private readonly tabConfig: XrmForm.Tester.XrmFormMockTab<TTabNames, TSectionNames, TControlName>[];
    private readonly serverMockData: XrmFormMockServerData = {};
    private readonly serverCustomApiMockData: XrmCustomApiMockData = {};

    /**
     * @summary Default constructor function
     * @param initialControlsConfig Configuration for control names/ types / status and related attributes
     * @param initialAttributesConfig Configuration for attribute names/ types / values, required level
     * @param initialTabsConfig Configuration for tab/section/controls
     */
    constructor(
        initialControlsConfig: XrmForm.Tester.XrmFormMockControl<TControlName, TAttributeNames>[],
        initialAttributesConfig: XrmForm.Tester.XrmFormMockAttribute<TAttributeNames>[],
        initialTabsConfig: XrmForm.Tester.XrmFormMockTab<TTabNames, TSectionNames, TControlName>[]
    ) {
        // Gets inputs from the generated files with the form description
        this.mockAttributes = new Map<TAttributeNames, XrmMockAttributeType>();
        this.mockControls = new Map<TControlName, XrmMockControlType>();
        this.controlConfig = [...initialControlsConfig];
        this.attributeConfig = [...initialAttributesConfig];
        this.tabConfig = initialTabsConfig;
    }
    /**
     * @summary Simple function to get the correctly mapped load event with arguments to pass to form load
     * @returns the mock load event to be passed to the onload form event
     */
    public getLoadExecutionContext(): EventContextWithEventArgsMock<Xrm.Events.LoadEventArguments> {
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
     * Given the form context config and data it generates the xrm-mock form, needs to be called before the test starts
     * @returns The object itself
     */
    public buildMockFormContext(): this {
        XrmMockGenerator.initialise({
            entity: this.formEntity,
            process: undefined,
        });
        (XrmMockGenerator.formContext.ui.formSelector.getCurrentItem() as FormItemMock).formType = this.formType;
        this.InitializeMockFormContextControls();
        this.InitTabs();
        this.InitXrmNavigationApiMocks();
        if (this.serverMockData) {
            this.InitXrmWebApiServerMocks();
        }
        if (this.serverCustomApiMockData) {
            this.InitXrmWebApiOnlineMocks();
        }
        this.InitUiNotificationMocks();
        this.InitXrmUtilityMocks();
        return this;
    }

    /**
     * Modifies the default form context config with the given update data
     * @param updateData Simply array with the control/attributes to be changed
     * @returns the object itself
     */
    public withFormData(updateData: XrmFormMockAttributeControlUpdateBase<TControlName, TAttributeNames>[]): this {
        this.UpdateControlConfig(updateData);
        return this;
    }

    /***
     * Set the entity id of the form so it is returned when calling entity.GetId()
     * @param recordId unique id of the form entity
     */
    public withEntityId(entityName: string, entityId: string): this {
        this.formEntity = {
            entityName,
            id: entityId,
        };
        return this;
    }

    /**
     * To set the form type on creation
     * @param formType Form type of the form context
     * @returns
     */
    public withFormType(formType: XrmEnum.FormType): this {
        this.formType = formType;
        return this;
    }

    /**
     * Setup a fake server data to be returned by the sino stubs
     * @param data Simple array of entity lists to be a fake for stubs to return
     */
    public withServerData(data: XrmFormMockServerData): this {
        Object.assign(this.serverMockData, data);
        return this;
    }

    /**
     * Configure the custom api mock data that the test context builder will contain
     * @param data Object containing a map of custom api names and the mock responses
     * @returns
     */
    public withCustomApi(data: XrmCustomApiMockData): this {
        Object.assign(this.serverCustomApiMockData, data);
        return this;
    }

    /**
     * Configure the sequence of confirm cancel dialog response for the form context calls
     * @param confirmDialogConfirmed
     */
    public withUserConfirmDialogSeq(confirmDialogConfirmed: boolean[]): this {
        this.confirmDialogConfirmedSeq = confirmDialogConfirmed.map((x) => ({ confirmed: x, executed: false }));
        return this;
    }

    /**
     * Get mock control in context out of the name string
     * @param controlName
     * @returns
     */
    public getControl<T extends XrmMockControlType>(controlName: TControlName): T | null {
        return <T>this.mockControls.get(controlName) ?? null;
    }

    /**
     * Get mock attribute in context out of the name string
     * @param attributeName
     * @returns
     */
    public getAttribute<T extends XrmMockAttributeType>(attributeName: TAttributeNames): T | null {
        return <T>this.mockAttributes.get(attributeName) ?? null;
    }

    private InitXrmWebApiServerMocks(): void {
        this.xrmWebApiMockStubs = {
            retrieveMultipleRecords: jest.fn((entityLogicalName: string, options?: string, maxPageSize?: number) =>
                this.RetrieveMultipleRecords(entityLogicalName, options, maxPageSize)
            ),
            retrieveRecord: jest.fn((entityLogicalName: string, id: string, options?: string) =>
                this.RetrieveSingleStubRecord(entityLogicalName, id, options)
            ),
            createRecord: jest.fn((entityLogicalName: string, record: XrmTable.DTO.Table<string>) => ({
                entityLogicalName,
                id: crypto.randomUUID(),
            })),
        };
        jest.spyOn(Xrm.WebApi, "retrieveRecord").mockImplementation(this.xrmWebApiMockStubs.retrieveRecord);
        jest.spyOn(Xrm.WebApi, "retrieveMultipleRecords").mockImplementation(this.xrmWebApiMockStubs.retrieveMultipleRecords);
        jest.spyOn(Xrm.WebApi, "createRecord").mockImplementation(this.xrmWebApiMockStubs.createRecord);
    }

    private InitXrmWebApiOnlineMocks(): void {
        this.xrmWebApiOnlineMockStubs = {
            execute: jest.fn(
                (request: XrmWebApi.ExecuteRequest): Xrm.Async.PromiseLike<Response> => this.GetCustomApiMockResponse(request)
            ),
        };
        jest.spyOn(Xrm.WebApi.online, "execute").mockImplementation(this.xrmWebApiOnlineMockStubs.execute);
    }

    private InitXrmNavigationApiMocks(): void {
        this.xrmNavigationMockStubs = {
            openAlertDialog: jest.fn((_alertStrings: Xrm.Navigation.AlertStrings, _alertOptions?: Xrm.Navigation.DialogSizeOptions) => {
                return Promise.resolve() as unknown as Xrm.Async.PromiseLike<void>;
            }),
            openConfirmDialog: jest.fn(
                (_confirmString: Xrm.Navigation.ConfirmStrings, confirmOptions?: Xrm.Navigation.DialogSizeOptions) => {
                    const item = this.confirmDialogConfirmedSeq.find((x) => !x.executed);
                    if (item) {
                        item.executed = true;
                    }
                    /// no dialog seq provided for this dialog... return false by default
                    return Promise.resolve({
                        confirmed: item?.confirmed ?? false,
                    }) as unknown as Xrm.Async.PromiseLike<Xrm.Navigation.ConfirmResult>;
                }
            ),
        };
        jest.spyOn(Xrm.Navigation, "openAlertDialog").mockImplementation(this.xrmNavigationMockStubs.openAlertDialog);
        jest.spyOn(Xrm.Navigation, "openConfirmDialog").mockImplementation(this.xrmNavigationMockStubs.openConfirmDialog);
    }

    private InitUiNotificationMocks(): void {
        this.xrmUiNotificationsMocks = {
            setFormNotification: jest.fn((_message: string, _level: Xrm.FormNotificationLevel, _uniqueId: string): boolean => {
                return true;
            }),
            clearFormNotification: jest.fn((_uniqueId: string): void => { }),
        };
        const formContext = XrmMockGenerator.getFormContext();
        if (this.xrmUiNotificationsMocks.setFormNotification) {
            formContext.ui.setFormNotification = this.xrmUiNotificationsMocks.setFormNotification;
        }
        if (this.xrmUiNotificationsMocks.clearFormNotification) {
            formContext.ui.clearFormNotification = this.xrmUiNotificationsMocks.clearFormNotification;
        }
    }

    private InitXrmUtilityMocks(): void {
        this.xrmUtilityApiMockStubs = {
            closeProgressIndicator: jest.fn(),
            showProgressIndicator: jest.fn(),
        };
        jest.spyOn(Xrm.Utility, "closeProgressIndicator").mockImplementation(this.xrmUtilityApiMockStubs.closeProgressIndicator);
        jest.spyOn(Xrm.Utility, "showProgressIndicator").mockImplementation(this.xrmUtilityApiMockStubs.showProgressIndicator);
    }

    private RetrieveMultipleRecords(
        entityLogicalName: string,
        options?: string,
        _maxPageSize?: number
    ): Xrm.Async.PromiseLike<Xrm.RetrieveMultipleResult<XrmTable.DTO.Table<string>>> {
        const entityDTOs = this.serverMockData[entityLogicalName];
        if (!entityDTOs) {
            return Promise.resolve({
                entities: [],
                nextLink: "",
            }) as unknown as Xrm.Async.PromiseLike<Xrm.RetrieveMultipleResult<XrmTable.DTO.Table<string>>>;
        }
        if (!options) {
            return Promise.resolve({
                entities: [...entityDTOs],
                nextLink: "",
            }) as unknown as Xrm.Async.PromiseLike<Xrm.RetrieveMultipleResult<XrmTable.DTO.Table<string>>>;
        }
        const oDataQueryString = options?.startsWith("?") ? options.substring(1) : options;
        const mockFilterResults = XrmMockFormODataFilter.executeRetrieveMultipleRecord(entityDTOs, oDataQueryString);
        return Promise.resolve({
            entities: [...mockFilterResults],
            nextLink: "",
        }) as unknown as Xrm.Async.PromiseLike<Xrm.RetrieveMultipleResult<XrmTable.DTO.Table<string>>>;
    }

    private RetrieveSingleStubRecord(
        entityLogicalName: string,
        id: string,
        options?: string
    ): Xrm.Async.PromiseLike<Partial<XrmTable.DTO.Table<string>>> {
        const entity = this.serverMockData[entityLogicalName];
        if (!entity) {
            throw new Error(`No record not found for entity ${entityLogicalName}`);
        }
        const record = entity.find((x) => x[`${entityLogicalName}id`] === id);
        if (!record) {
            throw new Error(`Record not found for entity ${entityLogicalName} with ${id}`);
        }
        if (options) {
            const oDataQueryString = options?.startsWith("?") ? options.substring(1) : options;
            return Promise.resolve(
                XrmMockFormODataFilter.executeSelectSingleRecord(record, oDataQueryString)
            ) as unknown as Xrm.Async.PromiseLike<Partial<XrmTable.DTO.Table<string>>>;
        }
        return Promise.resolve(record) as unknown as Xrm.Async.PromiseLike<Partial<XrmTable.DTO.Table<string>>>;
    }

    private GetCustomApiMockResponse(request: XrmWebApi.ExecuteRequest): Xrm.Async.PromiseLike<Response> {
        const apiName = request.getMetadata().operationName;
        try {
            if (apiName) {
                const webApiResponse = this.serverCustomApiMockData[apiName];
                if (webApiResponse) {
                    return Promise.resolve(new Response(JSON.stringify(webApiResponse))) as unknown as Xrm.Async.PromiseLike<Response>;
                }
            }
            return Promise.resolve(
                new Response(JSON.stringify({ error: "no response for received request " }), {
                    status: 400,
                    statusText: XrmMockFormTestContextBuilder.ERROR_NO_CUSTOM_API,
                })
            ) as unknown as Xrm.Async.PromiseLike<Response>;
        } catch (error: any) {
            throw new Error(error?.message);
        }
    }

    private UpdateControlConfig(updateAttributes: XrmFormMockAttributeControlUpdateBase<TControlName, TAttributeNames>[]): void {
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
            const attributeMock = this.CreateXrmMockFakeAttribute(attribute.name, attribute);
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
        const entityMock = new EntityMock({
            ...this.formEntity,
            attributes: new ItemCollectionMock<Xrm.Attributes.Attribute<Xrm.Attributes.AttributeValues>>(
                <Xrm.Attributes.Attribute<Xrm.Attributes.AttributeValues>[]>attributes
            ),
        });
        formContext.data = new DataMock(entityMock);
        formContext.data.getIsDirty = this.getIsDirty.bind(this);
    }

    private InitTabs(): this {
        for (const tabSection of this.tabConfig) {
            this.InitTab(tabSection);
        }
        return this;
    }

    private InitTab(tab: XrmForm.Tester.XrmFormMockTab<TTabNames, TSectionNames, TControlName>): this {
        const mockTab = XrmMockGenerator.Tab.createTab(tab.name, tab.label, tab.isVisible, tab.displayState, tab.parent);
        for (const tabSection of tab.sections) {
            this.InitTabSection(mockTab, tabSection);
        }
        return this;
    }

    private InitTabSection(tab: TabMock, section: XrmForm.Tester.XrmFormMockTabSection<TSectionNames, TControlName>): this {
        const controls = this.FilterControlList(section.controlNames);
        const controlCollection: ItemCollectionMock<Xrm.Controls.Control> = new ItemCollectionMock<Xrm.Controls.Control>(
            <Xrm.Controls.Control[]>controls
        );
        XrmMockGenerator.Section.createSection(section.name, section.label, section.isVisible, tab, controlCollection);
        return this;
    }

    private FilterControlList(controlNames: TControlName[]): XrmMockControlType[] {
        return (
            controlNames
                .map((name) => this.mockControls.get(name))
                .filter((x): x is XrmMockControlType => {
                    return x !== null && x !== undefined;
                }) ?? []
        );
    }

    private CreateXrmMockFakeAttribute(
        name: string,
        mockAttribute: XrmForm.Tester.XrmFormMockAttribute<TAttributeNames>
    ): XrmMockAttributeType | null {
        switch (mockAttribute.type) {
            case "string":
                return XrmMockGenerator.Attribute.createString({
                    name,
                    value: mockAttribute.value?.valueString,
                });
            case "boolean":
                return XrmMockGenerator.Attribute.createBoolean({
                    name,
                    value: mockAttribute.value?.valueBoolean,
                    isDirty: mockAttribute?.isDirty,
                });
            case "date":
                return XrmMockGenerator.Attribute.createDate({
                    name,
                    value: mockAttribute.value?.valueDate,
                    isDirty: mockAttribute?.isDirty,
                });
            case "number":
                return XrmMockGenerator.Attribute.createNumber({
                    name,
                    value: mockAttribute.value?.valueNumber,
                    isDirty: mockAttribute?.isDirty,
                });
            case "lookup":
                return XrmMockGenerator.Attribute.createLookup({
                    name,
                    value: mockAttribute.value?.valueLookup ?? null,
                    isDirty: mockAttribute?.isDirty,
                });
            case "optionSet":
            case "multiSet":
                return XrmMockGenerator.Attribute.createOptionSet({
                    name,
                    value: mockAttribute.value?.valueNumber,
                    options: mockAttribute.options ?? [],
                    isDirty: mockAttribute?.isDirty,
                });
            // case "Xrm.Controls.GridControl":
            // case "Xrm.Controls.KbSearchControl":
            // case "Xrm.Control.QuickFormControl":
            // TO BE DONE MOCK GRID CONTROL
            // return null;
            default:
                return null;
        }
    }

    private CreateXrmMockFakeControl(
        name: string,
        control: XrmForm.Tester.XrmFormMockControl<TControlName, TAttributeNames>
    ): XrmMockControlType | null {
        const mockAttribute = control.attributeName ? this.mockAttributes.get(control.attributeName) : null;
        switch (control.type) {
            case "string":
                return this.CreateStringMockControl(mockAttribute, control);
            case "boolean":
                return this.CreateBooleanMockControl(mockAttribute, control);
            case "date":
                return this.CreateDateMockControl(mockAttribute, control);
            case "number":
                return this.CreateNumberMockControl(mockAttribute, control);
            case "lookup":
                return this.CreateLookupControl(mockAttribute, control);
            case "optionSet":
            case "multiSet":
                return this.CreateOptionSetControl(mockAttribute, control);
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

    private CreateStringMockControl(
        mockAttribute: XrmMockAttributeType | null | undefined,
        control: XrmForm.Tester.XrmFormMockControl<TControlName, TAttributeNames>
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

    private CreateBooleanMockControl(
        mockAttribute: XrmMockAttributeType | null | undefined,
        control: XrmForm.Tester.XrmFormMockControl<TControlName, TAttributeNames>
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

    private CreateDateMockControl(
        mockAttribute: XrmMockAttributeType | null | undefined,
        control: XrmForm.Tester.XrmFormMockControl<TControlName, TAttributeNames>
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

    private CreateNumberMockControl(
        mockAttribute: XrmMockAttributeType | null | undefined,
        control: XrmForm.Tester.XrmFormMockControl<TControlName, TAttributeNames>
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

    private CreateLookupControl(
        mockAttribute: XrmMockAttributeType | null | undefined,
        control: XrmForm.Tester.XrmFormMockControl<TControlName, TAttributeNames>
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

    private CreateOptionSetControl(
        mockAttribute: XrmMockAttributeType | null | undefined,
        control: XrmForm.Tester.XrmFormMockControl<TControlName, TAttributeNames>
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

    private getIsDirty() {
        return Array.from(this.mockAttributes).some(([name, attr]) => attr.isDirty);
    }
}
