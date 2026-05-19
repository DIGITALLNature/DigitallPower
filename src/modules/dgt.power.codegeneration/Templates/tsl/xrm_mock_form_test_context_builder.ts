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
    GridControlMock,
    DataMock,
    GridMock,
    GridEntityMock,
    GridRowDataMock,
    GridRowMock,
    ContextMock,
    FormSelectorMock,
} from "xrm-mock";
import { EventContextWithEventArgsMock } from "xrm-mock/dist/xrm-mock/events/eventcontextwitheventargs.mock";
import {
    IXrmMockFormTestContextBuilder,
    XrmFormAttributeMethodMockStubs,
    XrmConsoleMockStubs,
    XrmCustomApiMockData,
    XrmFormContextDataMockStubs,
    XrmFormMockAttributeControlUpdateBase,
    XrmFormMockServerData,
    XrmFormTabUpdateData,
    XrmMockAttributeType,
    XrmMockConfirmDialogResponses,
    XrmMockControlType,
    XrmFormContextUiMockStubs,
    XrmNavigationMockStubs,
    XrmSubGridControlMockStubs,
    XrmFormContextTabEventsStub,
    XrmUtilityApiMockStubs,
    XrmWebApiMockStubs,
    XrmWebApiOnlineMockStubs,
    XrmSubGridRowMocks,
    XrmFormGridMockRowConfig,
    XrmFormGridMockRowConfigRow,
    XrmFormSubGridAttributeUpdateValue,
    XrmLookupControlMockStubs,
    XrmFormTabUpdateBase,
    XrmStandardControlMockStubs,
    XrmFormMockFormItem,
    XrmFormSelectorMockStubs,
    XrmOptionSetControlMockStubs,
} from "./xrm_mock_form_test_context_types";
import { XrmMockFormODataFilter } from "./xrm_mock_form_odata_filter";
import Context from "xrm-mock/dist/xrm-mock-generator/context";

/** Auxiliary class for simplified xrm-mock testing */
export class XrmMockFormTestContextBuilder<
    TTabNames extends string,
    TSectionNames extends string,
    TControlName extends string,
    TAttributeNames extends string,
> implements IXrmMockFormTestContextBuilder<TTabNames, TSectionNames, TControlName, TAttributeNames>
{
    public static readonly ERROR_NO_CUSTOM_API = "Bad Request";
    public xrmWebApiMockStubs: XrmWebApiMockStubs = {};
    public xrmWebApiOnlineMockStubs: XrmWebApiOnlineMockStubs = {};
    public xrmNavigationMockStubs: XrmNavigationMockStubs = {};
    public xrmUiFormMocksStubs: XrmFormContextUiMockStubs = {};
    public xrmUtilityApiMockStubs: XrmUtilityApiMockStubs = {};
    public xrmFormAttributeMethodMockStubs: XrmFormAttributeMethodMockStubs<TAttributeNames> = {};
    public xrmSubGridControlMockStubs: XrmSubGridControlMockStubs<TControlName> = {};
    public xrmFormSelectorMockStub: XrmFormSelectorMockStubs = {};
    public xrmStandardControlMockStubs: XrmStandardControlMockStubs<TControlName> = {};
    public xrmOptionSetControlMockStubs: XrmOptionSetControlMockStubs<TControlName> = {};
    public xrmLookupControlMockStubs: XrmLookupControlMockStubs<TControlName> = {};
    public xrmTabEventsStub: XrmFormContextTabEventsStub<TTabNames> = {};
    public xrmFormDataMockStubs: XrmFormContextDataMockStubs = {};
    public xrmConsoleMockStubs: XrmConsoleMockStubs = {};
    public xrmFetchMockStubs: jest.Mock | null = null;
    public xrmPreSaveEvent: jest.Mock | null = null;
    public formEntity: IEntityComponents = {};

    private readonly xrmSubGridConfig: Map<TControlName, XrmFormGridMockRowConfig<string>> = new Map<
        TControlName,
        XrmFormGridMockRowConfig<string>
    >();
    private readonly xrmSubGridRowMocks: Map<TControlName, XrmSubGridRowMocks> = new Map<TControlName, XrmSubGridRowMocks>();
    private formType: XrmEnum.FormType = XrmEnum.FormType.Create; //default value
    private confirmDialogConfirmedSeq: XrmMockConfirmDialogResponses[] = [];
    private isMockAttributeChanges: boolean = false;
    private isSubGridMethodsMock: boolean = false;
    private isControlMethodMock: boolean = false;
    private isMockTabEvents: boolean = false;
    private isMockPreSaveEvent: boolean = false;
    private isMockFormDateEvent: boolean = false;
    private isMockFormUiEvent: boolean = false;
    private isFormDataValid: boolean = false;
    private isFormDataDirty: boolean = false;
    private isConsoleMocked: boolean = false;
    private isControlLookupMethodMock: boolean = false;
    private saveErrorMessage: string | null = null;
    private userRoles: Xrm.LookupValue[] = [];
    private languageId: number = 1033;
    private clientUrl: string | null = null;
    private formSelectorItems: XrmFormMockFormItem[] = [];
    private serverMockDateError: string | null = null;
    private readonly mockAttributes: Map<TAttributeNames, XrmMockAttributeType>;
    private readonly mockControls: Map<TControlName, XrmMockControlType>;
    private readonly controlConfig: XrmForm.Tester.XrmFormMockControl<TControlName, TAttributeNames>[];
    private readonly attributeConfig: XrmForm.Tester.XrmFormMockAttribute<TAttributeNames>[];
    private readonly tabConfig: XrmForm.Tester.XrmFormMockTab<TTabNames, TSectionNames, TControlName>[];
    private readonly serverMockData: XrmFormMockServerData = {};
    private readonly serverCustomApiMockData: XrmCustomApiMockData = {};
    private readonly serverCustomApiExceptions: Map<string, string>;

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
        this.serverCustomApiExceptions = new Map<string, string>();
        this.controlConfig = initialControlsConfig.map((item) => ({ ...item }));
        this.attributeConfig = initialAttributesConfig.map((item) => ({
            ...item,
            options: item.options?.map((opt) => ({ ...opt })),
            value: {
                ...item.value,
                valueLookup: item.value?.valueLookup ? item.value.valueLookup.map((valueLookup) => ({ ...valueLookup })) : undefined,
                valueNumberMset: item.value?.valueNumberMset ? item.value.valueNumberMset.map((valueNumber) => valueNumber) : undefined,
            },
        }));
        this.tabConfig = initialTabsConfig.map((item) => ({
            ...item,
            sections: item.sections.map((section) => ({ ...section, controlNames: section.controlNames.map((x) => x) })),
        }));
        this.formEntity = {
            id: crypto.randomUUID(),
            entityName: "defaultEntity",
        };
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
     * Get mock form context with specific provided type
     * @returns
     */
    public getMockFormContext<T extends Xrm.FormContext>(): T {
        return <T>this.getLoadExecutionContext().getFormContext();
    }

    /**
     * Generate an form even execution context with a input control source event
     * @param source
     * @returns
     */
    public getExecutionContextWithSource(
        source: Xrm.Attributes.Attribute | Xrm.Controls.Control | Xrm.Controls.Tab | Xrm.Entity
    ): EventContextWithEventArgsMock<Xrm.Events.EventContext> {
        const eventContext = XrmMockGenerator.getEventContext() ?? new EventContextMock({});
        return new EventContextWithEventArgsMock<Xrm.Events.EventContext>({
            context: eventContext.context,
            depth: eventContext.depth,
            eventSource: source as unknown as Xrm.Attributes.Attribute | Xrm.Controls.Control | Xrm.Entity,
            formContext: eventContext.formContext,
            sharedVariables: eventContext.sharedVariables,
        });
    }

    /**
     * Generate a form asyn event context
     * @param source
     * @returns
     */
    public getSaveAsyncEventContext(): EventContextWithEventArgsMock<Xrm.Events.SaveEventArgumentsAsync> {
        const eventContext = XrmMockGenerator.getEventContext() ?? new EventContextMock({});
        const eventArgs: Xrm.Events.SaveEventArgumentsAsync = {
            disableAsyncTimeout: jest.fn(),
            getSaveMode: jest.fn(),
            isDefaultPrevented: jest.fn(),
            preventDefault: jest.fn(),
            preventDefaultOnError: jest.fn(),
        };
        return new EventContextWithEventArgsMock<Xrm.Events.SaveEventArgumentsAsync>({
            context: eventContext.context,
            depth: eventContext.depth,
            formContext: eventContext.formContext,
            sharedVariables: eventContext.sharedVariables,
            eventArgs: eventArgs,
        });
    }

    /**
     * Given the form context config and data it generates the xrm-mock form, needs to be called before the test starts
     * @returns The object itself
     */
    public buildMockFormContext(): this {
        XrmMockGenerator.initialise({
            context: this.MapFormContext(),
            entity: this.formEntity,
            process: undefined,
        });

        // If sub grid mock config, then init subGrid row mocks before controls as controls mocks use it
        if (this.isSubGridMethodsMock && this.xrmSubGridConfig.size > 0) {
            this.InitSubGridConfigMockRows();
        }
        this.InitializeMockFormContextControls();
        this.InitTabs();
        this.InitXrmNavigationApiMocks();
        this.InitXrmUtilityMocks();
        this.InitFormUiSelector();

        if (this.isMockFormDateEvent) {
            this.InitMockFormDataEvent();
        }
        if (this.isMockFormUiEvent) {
            this.InitFormUiMocks();
        }
        if (this.serverMockData) {
            this.InitXrmWebApiServerMocks();
        }
        if (this.serverCustomApiMockData || this.serverCustomApiExceptions.size > 0) {
            this.InitXrmWebApiOnlineMocks();
        }
        if (this.isMockPreSaveEvent) {
            this.InitPreSaveMock();
        }
        if (this.isConsoleMocked) {
            this.InitConsoleMock();
        }
        return this;
    }

    /**
     * Modifies the default form context config with the given update data
     * @param updateData Simply array with the control/attributes to be changed
     * @returns the object itself
     */
    public withFormAttributeControlData(updateData: XrmFormMockAttributeControlUpdateBase<TControlName, TAttributeNames>[]): this {
        this.UpdateControlConfig(updateData);
        return this;
    }

    /**
     * Set the entity id with update form data
     * @param updateData
     * @returns
     */
    public withFormTabData(updateData: XrmFormTabUpdateData<TTabNames, TSectionNames>): this {
        this.UpdateTabConfig(updateData);
        return this;
    }

    /***
     * Set the entity id of the form so it is returned when calling entity.GetId()
     * @param recordId unique id of the form entity
     */
    public withEntity(entityName: string, entityId: string): this {
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
     * With error message when calling server retrieve
     * @param erroMsg
     * @returns
     */
    public withServerDataError(erroMsg: string): this {
        this.serverMockDateError = erroMsg;
        return this;
    }

    /**
     * Configure a list of custom api mock data that the test context builder will contain
     * @param data Object containing a map of custom api names and the mock responses
     * @returns
     */
    public withCustomApis(data: XrmCustomApiMockData): this {
        Object.assign(this.serverCustomApiMockData, data);
        return this;
    }

    /**
     * Configure a single custom api with name + response
     * @param name
     * @param response
     * @returns
     */
    public withCustomApi<T extends XrmWebApi.ExecuteResponse>(name: string, response: T): this {
        this.serverCustomApiMockData[name] = response;
        return this;
    }

    /**
     * Configure single custom api to return exception for unhandled exception catch handling
     * @param name
     * @param errorMsg
     */
    public withCustomApiException(name: string, errorMsg: string): this {
        this.serverCustomApiExceptions.set(name, errorMsg);
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
     * Configure if we need a jest attribute mock changes
     * @param isMockAttributeChanges
     * @returns
     */
    public withAttributeMockChange(isMockAttributeChanges: boolean): this {
        this.isMockAttributeChanges = isMockAttributeChanges;
        return this;
    }

    /**
     * Configure if you want to jest mock sub grid mock loads
     * @param isSubGridLoad
     * @returns
     */
    public withSubGridMethodsMock(isSubGridLoad: boolean): this {
        this.isSubGridMethodsMock = isSubGridLoad;
        return this;
    }

    /**
     * Control if you want to jest mock control mocks
     * @param isControlMethodMock
     * @returns
     */
    public WithControlMethodMock(isControlMethodMock: boolean): this {
        this.isControlMethodMock = isControlMethodMock;
        return this;
    }

    /**
     * Configure if you want to jest mock tab events
     * @param isMockTabEvents
     * @returns
     */
    public withRegisterTabEventsMocks(isMockTabEvents: boolean): this {
        this.isMockTabEvents = isMockTabEvents;
        return this;
    }

    /**
     * Configure is you want to jest mock save events
     * @param isPreSaveEventMock
     * @returns
     */
    public withPreSaveEventMock(isPreSaveEventMock: boolean): this {
        this.isMockPreSaveEvent = isPreSaveEventMock;
        return this;
    }

    /**
     * Set form context language id to be returned by the getGlobalContext
     * @param languageId
     * @returns
     */
    public withLanguageId(languageId: number): this {
        this.languageId = languageId;
        return this;
    }

    /**
     * Set form context client url
     * @param clientUrl
     * @returns
     */
    public withClientUrl(clientUrl: string): this {
        this.clientUrl = clientUrl;
        return this;
    }

    /**
     * Set form context user roles to be returned by the getGlobalContext
     * @param roles
     * @returns
     */
    public withUserRoles(roles: Xrm.LookupValue[]): this {
        this.userRoles = roles;
        return this;
    }

    /**
     * Configure if you want to jest mock form data events
     * @param isMockFormDateEventMock
     * @returns
     */
    public withFormDataEventMock(isMockFormDateEventMock: boolean): this {
        this.isMockFormDateEvent = isMockFormDateEventMock;
        return this;
    }

    /**
     * Configure if you want to jest mock form ui events
     * @param isMockFormUiEvent
     * @returns
     */
    public withFormUiEventMock(isMockFormUiEvent: boolean): this {
        this.isMockFormUiEvent = isMockFormUiEvent;
        return this;
    }

    /**
     * Configure if form data valid mock returns specific value
     * @param isValid
     * @returns
     */
    public withFormDataValid(isValid: boolean): this {
        this.isFormDataValid = isValid;
        return this;
    }

    /**
     * Configure if form data valid mock returns specific value
     * @param isDirty
     * @returns
     */
    public withFormDataDirty(isDirty: boolean): this {
        this.isFormDataDirty = isDirty;
        return this;
    }

    /**
     * Configure so form data save returns an error message
     * @param isDirty
     * @returns
     */
    public withSaveErrorMessage(errorMsg: string | null): this {
        this.saveErrorMessage = errorMsg;
        return this;
    }

    /**
     * Configure a console log mock to assertions on to of console statements
     * @param isConsoleMocked
     * @returns
     */
    public withConsoleMocked(isConsoleMocked: boolean): this {
        this.isConsoleMocked = isConsoleMocked;
        return this;
    }

    /**
     * Add a mock for a simple fetch with a certain response
     * @param statusCode
     * @param response
     */
    public withFetchResponse(statusCode: number, response: string): this {
        this.xrmFetchMockStubs = jest.fn((_url: URL | RequestInfo): Promise<Response> => {
            const isOK = statusCode >= 200 && statusCode <= 299;
            return Promise.resolve(
                new Response(response, {
                    status: statusCode,
                    statusText: isOK ? "OK" : "Internal Error",
                })
            );
        });
        globalThis.fetch = this.xrmFetchMockStubs;
        return this;
    }

    /**
     * Add a mock for a simple fetch with a certain response
     * @param statusCode
     * @param response
     */
    public withFetchException(errorMsg: string): this {
        this.xrmFetchMockStubs = jest.fn((_url: URL | RequestInfo): Promise<Response> => {
            throw new Error(errorMsg);
        });
        globalThis.fetch = this.xrmFetchMockStubs;
        return this;
    }

    /**
     * Configure sub grid mocks to be able to interact with the sub grid rows attributes
     * @param name
     * @param updateGridRows
     * @returns
     */
    public withSubGridMockRows<TGridAttributeNames extends string>(
        name: TControlName,
        updateGridRows: XrmFormGridMockRowConfig<TGridAttributeNames>
    ): this {
        if (!updateGridRows.selectedRowsIndexes.every((index) => index >= 0 && index < updateGridRows.gridRows.length)) {
            throw new Error("Wrong input selectedRowsIndexes value over length of rows");
        }
        this.xrmSubGridConfig.set(name, {
            ...updateGridRows,
            subGridRowsAttributeConfig: updateGridRows.subGridRowsAttributeConfig.map((item) => ({
                ...item,
                options: item.options?.map((opt) => ({ ...opt })),
                value: {
                    ...item.value,
                    valueLookup: item.value?.valueLookup ? item.value.valueLookup.map((valueLookup) => ({ ...valueLookup })) : undefined,
                    valueNumberMset: item.value?.valueNumberMset ? item.value.valueNumberMset.map((valueNumber) => valueNumber) : undefined,
                },
            })),
        });
        return this;
    }

    /**
     * Configure if form mocks the control lookup methods
     * @param name
     * @param updateGridRows
     * @returns
     */
    public withLookupControlMethodEventMock(isLookupControlMock: boolean): this {
        this.isControlLookupMethodMock = isLookupControlMock;
        return this;
    }

    /**
     * Configure if form mocks contains other form selectors
     * @param currentForm
     * @param otherItems
     * @returns
     */
    public withFormSelectorItems(currentForm: XrmFormMockFormItem, otherItems: XrmFormMockFormItem[]): this {
        this.formSelectorItems = [...otherItems.map((x) => ({ ...x, isCurrent: false })), { ...currentForm, isCurrent: true }];
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

    private InitMockFormDataEvent(): void {
        this.xrmFormDataMockStubs = {
            refresh: jest.fn((refresh?: boolean): Xrm.Async.PromiseLike<void> => {
                if (this.saveErrorMessage) {
                    throw new Error(this.saveErrorMessage);
                }
                return Promise.resolve() as unknown as Xrm.Async.PromiseLike<void>;
            }),
            isValid: jest.fn(() => this.isFormDataValid),
            getIsDirty: jest.fn(() => this.isFormDataDirty),
            save: jest.fn((): Xrm.Async.PromiseLike<void> => {
                if (this.saveErrorMessage) {
                    throw new Error(this.saveErrorMessage);
                }
                return Promise.resolve() as unknown as Xrm.Async.PromiseLike<void>;
            }),
        };

        jest.spyOn(XrmMockGenerator.getFormContext().data, "refresh").mockImplementation(this.xrmFormDataMockStubs.refresh);
        jest.spyOn(XrmMockGenerator.getFormContext().data, "isValid").mockImplementation(this.xrmFormDataMockStubs.isValid);
        jest.spyOn(XrmMockGenerator.getFormContext().data, "getIsDirty").mockImplementation(this.xrmFormDataMockStubs.getIsDirty);
        jest.spyOn(XrmMockGenerator.getFormContext().data, "save").mockImplementation(this.xrmFormDataMockStubs.save);
    }

    private InitConsoleMock(): void {
        this.xrmConsoleMockStubs = {
            log: jest.fn((_message?: any, ..._optionalParams: any[]) => { }),
            error: jest.fn((_message?: any, ..._optionalParams: any[]) => { }),
            debug: jest.fn((_message?: any, ..._optionalParams: any[]) => { }),
        };
        jest.spyOn(console, "log").mockImplementation(this.xrmConsoleMockStubs.log);
        jest.spyOn(console, "error").mockImplementation(this.xrmConsoleMockStubs.error);
        jest.spyOn(console, "debug").mockImplementation(this.xrmConsoleMockStubs.debug);
    }

    private InitFormUiMocks(): void {
        this.xrmUiFormMocksStubs = {
            setFormNotification: jest.fn((_message: string, _level: Xrm.FormNotificationLevel, _uniqueId: string): boolean => {
                return true;
            }),
            clearFormNotification: jest.fn((_uniqueId: string): void => { }),
            refreshRibbon: jest.fn((_refreshAll?: boolean): void => { }),
        };
        jest.spyOn(XrmMockGenerator.getFormContext().ui, "setFormNotification").mockImplementation(
            this.xrmUiFormMocksStubs.setFormNotification
        );
        jest.spyOn(XrmMockGenerator.getFormContext().ui, "clearFormNotification").mockImplementation(
            this.xrmUiFormMocksStubs.clearFormNotification
        );
        jest.spyOn(XrmMockGenerator.getFormContext().ui, "refreshRibbon").mockImplementation(this.xrmUiFormMocksStubs.refreshRibbon);
    }

    private InitXrmUtilityMocks(): void {
        this.xrmUtilityApiMockStubs = {
            closeProgressIndicator: jest.fn(),
            showProgressIndicator: jest.fn(),
        };
        jest.spyOn(Xrm.Utility, "closeProgressIndicator").mockImplementation(this.xrmUtilityApiMockStubs.closeProgressIndicator);
        jest.spyOn(Xrm.Utility, "showProgressIndicator").mockImplementation(this.xrmUtilityApiMockStubs.showProgressIndicator);
    }

    private InitFormUiSelector(): void {
        const currentForm = this.CreateCurrentFormMockItem();
        const formSelectorMock = XrmMockGenerator.formContext.ui.formSelector as FormSelectorMock;
        formSelectorMock.items = new ItemCollectionMock<FormItemMock>([currentForm]);
        for (const otherItem of this.formSelectorItems.filter((x) => !x.isCurrent)) {
            formSelectorMock.items.push(
                new FormItemMock({
                    id: otherItem.id,
                    label: otherItem.label ?? "",
                    currentItem: false,
                    formType: otherItem.formType ?? XrmEnum.FormType.Update,
                })
            );
        }
        this.AddFormSelectorStubs(formSelectorMock.items.get());
    }

    private CreateCurrentFormMockItem(): FormItemMock {
        const currentFormSelector = this.formSelectorItems.find((x) => x.isCurrent);
        if (currentFormSelector) {
            // Add with explicitly set data
            return new FormItemMock({
                id: currentFormSelector.id,
                label: currentFormSelector.label ?? "",
                currentItem: true,
                formType: currentFormSelector.formType ?? this.formType,
            });
        } else {
            return new FormItemMock({
                id: "{00000000-0000-0000-0000-000000000000}",
                label: "current-form",
                currentItem: true,
                formType: this.formType,
            });
        }
    }

    private AddFormSelectorStubs(formItemMocks: FormItemMock[]): void {
        for (const formItemMock of formItemMocks) {
            const methodMockStub = {
                navigate: jest.fn(),
                setVisible: jest.fn((isVisible: boolean): boolean => this.SetFormSelectorItemVisible(isVisible, formItemMock)),
                getVisible: jest.fn((): boolean => this.GetFormSelectorItemVisible(formItemMock)),
            };
            this.xrmFormSelectorMockStub[formItemMock.id] = methodMockStub;
            formItemMock.navigate = methodMockStub.navigate;
            formItemMock.setVisible = methodMockStub.setVisible;
            formItemMock.getVisible = methodMockStub.getVisible;
        }
    }

    private GetFormSelectorItemVisible(formItemMock: FormItemMock): boolean {
        const formItem = this.formSelectorItems.find((x) => x.id === formItemMock.id);
        if (!formItem) {
            throw new Error(`Form item with ${formItemMock.id} not found!`);
        }
        // if not explicitly set assume visible
        return formItem.visible ?? true;
    }

    private SetFormSelectorItemVisible(isVisible: boolean, formItemMock: FormItemMock): boolean {
        const formItem = this.formSelectorItems.find((x) => x.id === formItemMock.id);
        if (!formItem) {
            throw new Error(`Form item with ${formItemMock.id} not found!`);
        }
        formItem.visible = isVisible;
        return true;
    }

    private RetrieveMultipleRecords(
        entityLogicalName: string,
        options?: string,
        _maxPageSize?: number
    ): Xrm.Async.PromiseLike<Xrm.RetrieveMultipleResult<XrmTable.DTO.Table<string>>> {
        if (this.serverMockDateError) {
            return Promise.reject(new Error(this.serverMockDateError)) as unknown as Xrm.Async.PromiseLike<
                Xrm.RetrieveMultipleResult<XrmTable.DTO.Table<string>>
            >;
        }
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
                if (this.serverCustomApiExceptions.has(apiName)) {
                    throw new Error(this.serverCustomApiExceptions.get(apiName));
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
            if (updateData.isDirty !== undefined && attribute) {
                attribute.isDirty = updateData.isDirty;
            }
            if (updateData.isVisible !== undefined && control) {
                control.isVisible = updateData.isVisible;
            }
        }
    }

    private UpdateTabConfig(updateTabData: XrmFormTabUpdateData<TTabNames, TSectionNames>): void {
        for (const [tabName, tabConfig] of Object.entries<XrmFormTabUpdateBase<TSectionNames>>(
            updateTabData as Record<string, XrmFormTabUpdateBase<TSectionNames>>
        )) {
            const matchTab = this.tabConfig.find((x) => x.name === tabName);
            if (matchTab) {
                matchTab.displayState = tabConfig.displayState;
                matchTab.isVisible = tabConfig.isVisible;
                this.UpdateTabConfigSections(matchTab, tabConfig);
            }
        }
    }

    private UpdateTabConfigSections(
        matchTab: XrmForm.Tester.XrmFormMockTab<TTabNames, TSectionNames, TControlName>,
        tabConfig: XrmFormTabUpdateBase<TSectionNames>
    ): void {
        for (const sectionUpdate of tabConfig.sections ?? []) {
            const matchSection = matchTab.sections.find((x) => x.name == sectionUpdate.name);
            if (matchSection) {
                matchSection.isVisible = sectionUpdate.isVisible;
            }
        }
    }

    private InitSubGridConfigMockRows(): void {
        for (const [subGridName, subGridConfig] of this.xrmSubGridConfig) {
            this.InitSubGridMockRows(subGridName, subGridConfig);
        }
    }

    private InitSubGridMockRows(subGridName: TControlName, subGridConfig: XrmFormGridMockRowConfig<string>): void {
        const subGridRows: XrmSubGridRowMocks = { selectedGridRowMocksIndexes: [...subGridConfig.selectedRowsIndexes], rows: [] };
        for (const gridRow of subGridConfig.gridRows) {
            const gridRowDataMock = this.CreateSubGridRowDataMock(subGridConfig.entityName, gridRow.entityId);
            const dataMock = this.CreateSubGridRowEntityDataMock(
                subGridConfig.subGridRowsAttributeConfig,
                gridRow,
                subGridConfig.entityName
            );
            const gridRowMock = new GridRowMock(dataMock, gridRowDataMock);
            subGridRows.rows.push(gridRowMock);
        }
        this.xrmSubGridRowMocks.set(subGridName, subGridRows);
    }

    private CreateSubGridRowDataMock(entityName: string, entityId: string): GridRowDataMock {
        const gridEntityMock = new GridEntityMock({ entityType: entityName, id: entityId });
        return new GridRowDataMock(gridEntityMock);
    }

    private CreateSubGridRowEntityDataMock(
        subGridConfig: XrmForm.Tester.XrmFormMockAttribute<string>[],
        gridRow: XrmFormGridMockRowConfigRow<string>,
        entityName: string
    ): DataMock {
        const attributeCollection = this.CreateSubGridAttributeDataCollection(subGridConfig, gridRow.subGridRowsAttributes);
        const entityMock = new EntityMock({
            entityName,
            id: gridRow.entityId,
            attributes: attributeCollection,
        });
        return new DataMock(entityMock);
    }

    private CreateSubGridAttributeDataCollection(
        subGridConfig: XrmForm.Tester.XrmFormMockAttribute<string>[],
        subGridRowsAttributes: XrmFormSubGridAttributeUpdateValue<string>[]
    ): ItemCollectionMock<Xrm.Attributes.Attribute<Xrm.Attributes.AttributeValues>> {
        const mergeSubGridRowDefinition = this.MergeSubGridAttributesDefinition(subGridConfig, subGridRowsAttributes);
        const mockAttributes: XrmMockAttributeType[] = mergeSubGridRowDefinition
            .map((x) => this.CreateXrmMockFakeAttribute(x.name, x))
            .filter((x): x is XrmMockAttributeType => !x);
        return new ItemCollectionMock<Xrm.Attributes.Attribute<Xrm.Attributes.AttributeValues>>(
            <Xrm.Attributes.Attribute<Xrm.Attributes.AttributeValues>[]>mockAttributes
        );
    }

    private MergeSubGridAttributesDefinition(
        subGridConfig: XrmForm.Tester.XrmFormMockAttribute<string>[],
        subGridRowsAttributes: XrmFormSubGridAttributeUpdateValue<string>[]
    ): XrmForm.Tester.XrmFormMockAttribute<string>[] {
        return subGridConfig
            .map((item) => ({
                ...item,
                options: item.options?.map((opt) => ({ ...opt })),
                value: {
                    ...item.value,
                    valueLookup: item.value?.valueLookup ? item.value.valueLookup.map((valueLookup) => ({ ...valueLookup })) : undefined,
                    valueNumberMset: item.value?.valueNumberMset ? item.value.valueNumberMset.map((valueNumber) => valueNumber) : undefined,
                },
            }))
            .map((item) => {
                const rowAttrib = subGridRowsAttributes.find((x) => x.name === item.name);
                if (rowAttrib) {
                    return {
                        ...item,
                        value: rowAttrib.value,
                    };
                }
                return item;
            });
    }

    private InitializeMockFormContextControls(): void {
        for (const attribute of this.attributeConfig) {
            const attributeMock = this.CreateXrmFormMockFakeAttribute(attribute.name, attribute);
            this.InitAttributeMock(attributeMock, attribute);
        }
        for (const control of this.controlConfig) {
            const controlMock = this.CreateXrmMockFakeControl(control.name, control);
            this.InitControlMock(controlMock, control);
        }
        const formContext = XrmMockGenerator.getFormContext();
        const attributeCollection = new ItemCollectionMock<Xrm.Attributes.Attribute<Xrm.Attributes.AttributeValues>>(<
            Xrm.Attributes.Attribute<Xrm.Attributes.AttributeValues>[]
            >[...this.mockAttributes.values()]);
        const entityMock = new EntityMock({
            ...this.formEntity,
            attributes: attributeCollection,
        });
        const dataMock = new DataMock(entityMock);
        formContext.data = dataMock;
        formContext.data.attributes = attributeCollection;
        formContext.data.getIsDirty = this.getIsDirty.bind(this);
        formContext.data.entity.getId = () => this.formEntity.id ?? "";
    }

    private InitAttributeMock(
        attributeMock: XrmMockAttributeType | null,
        attribute: XrmForm.Tester.XrmFormMockAttribute<TAttributeNames>
    ): void {
        if (attributeMock) {
            this.mockAttributes.set(attribute.name, attributeMock);
            if (this.isMockAttributeChanges) {
                const mockAddOnChange = {
                    addOnChange: jest.fn(),
                    removeOnChange: jest.fn(),
                };
                attributeMock.addOnChange = mockAddOnChange.addOnChange;
                attributeMock.removeOnChange = mockAddOnChange.removeOnChange;
                this.xrmFormAttributeMethodMockStubs[attribute.name] = mockAddOnChange;
            }
        }
    }

    private InitControlMock(
        controlMock: XrmMockControlType | null,
        control: XrmForm.Tester.XrmFormMockControl<TControlName, TAttributeNames>
    ): void {
        if (controlMock) {
            if (this.isSubGridMethodsMock && this.isGridControl(controlMock)) {
                const gridControlMock = {
                    refresh: jest.fn(),
                    addOnLoad: jest.fn(),
                    getGrid: jest.fn((): Xrm.Controls.Grid => this.CreateMockGrid(control)),
                };
                this.xrmSubGridControlMockStubs[control.name] = gridControlMock;
                controlMock.addOnLoad = gridControlMock.addOnLoad;
                controlMock.refresh = gridControlMock.refresh;
                controlMock.getGrid = gridControlMock.getGrid;
            }
            if (this.isControlLookupMethodMock && this.isLookupControl(controlMock)) {
                const lookupControlMock = {
                    removePreSearch: jest.fn(),
                    addPreSearch: jest.fn(),
                };
                controlMock.removePreSearch = lookupControlMock.removePreSearch;
                controlMock.addPreSearch = lookupControlMock.addPreSearch;
                this.xrmLookupControlMockStubs[control.name] = lookupControlMock;
            }
            if (this.isControlMethodMock && !this.isGridControl(controlMock)) {
                const standardControl = {
                    addNotification: jest.fn(),
                    clearNotification: jest.fn(),
                    clearOptions: jest.fn(),
                    setFocus: jest.fn(),
                };
                controlMock.addNotification = standardControl.addNotification;
                controlMock.clearNotification = standardControl.clearNotification;
                controlMock.setFocus = standardControl.setFocus;
                if (this.isOptionSetControl(controlMock)) {
                    controlMock.clearOptions = standardControl.clearOptions;
                    this.xrmOptionSetControlMockStubs[control.name] = standardControl;
                } else {
                    this.xrmStandardControlMockStubs[control.name] = standardControl;
                }
            }
            this.mockControls.set(control.name, controlMock);
        }
    }

    private InitTabs(): this {
        for (const tabSection of this.tabConfig) {
            this.InitTab(tabSection);
        }
        return this;
    }

    private InitTab(tab: XrmForm.Tester.XrmFormMockTab<TTabNames, TSectionNames, TControlName>): this {
        const mockTab = XrmMockGenerator.Tab.createTab(tab.name, tab.label, tab.isVisible, tab.displayState, tab.parent);
        if (this.isMockTabEvents) {
            const tabEvent = {
                addTabStateChange: jest.fn(),
                removeTabStateChange: jest.fn(),
                setFocus: jest.fn(),
            };
            mockTab.addTabStateChange = tabEvent.addTabStateChange;
            mockTab.removeTabStateChange = tabEvent.removeTabStateChange;
            mockTab.setFocus = tabEvent.setFocus;
            this.xrmTabEventsStub[tab.name] = tabEvent;
        }
        for (const tabSection of tab.sections) {
            this.InitTabSection(mockTab, tabSection);
        }
        return this;
    }

    private InitPreSaveMock(): void {
        this.xrmPreSaveEvent = jest.fn();
        XrmMockGenerator.getFormContext().data.entity.addOnSave = this.xrmPreSaveEvent;
    }

    private InitTabSection(tab: TabMock, section: XrmForm.Tester.XrmFormMockTabSection<TSectionNames, TControlName>): this {
        const controls = this.FilterControlList(section.controlNames);
        const controlCollection: ItemCollectionMock<Xrm.Controls.Control> = new ItemCollectionMock<Xrm.Controls.Control>(
            <Xrm.Controls.Control[]>controls
        );
        XrmMockGenerator.Section.createSection(
            section.name,
            section.label ?? section.name,
            section.isVisible ?? false,
            tab,
            controlCollection
        );
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

    // Creates mock attributes in the context of the static XrmMockGenerator
    private CreateXrmFormMockFakeAttribute(
        name: string,
        mockAttribute: XrmForm.Tester.XrmFormMockAttribute<TAttributeNames>
    ): XrmMockAttributeType | null {
        switch (mockAttribute.type) {
            case "string":
                return XrmMockGenerator.Attribute.createString({
                    name,
                    value: mockAttribute.value?.valueString,
                    isDirty: mockAttribute?.isDirty,
                    requiredLevel: mockAttribute?.requiredLevel,
                });
            case "boolean":
                return this.CreateFormContextBooleanWithNullReturn(name, mockAttribute);
            case "date":
                return XrmMockGenerator.Attribute.createDate({
                    name,
                    value: mockAttribute.value?.valueDate,
                    isDirty: mockAttribute?.isDirty,
                    requiredLevel: mockAttribute?.requiredLevel,
                });
            case "number":
                return XrmMockGenerator.Attribute.createNumber({
                    name,
                    value: mockAttribute.value?.valueNumber,
                    isDirty: mockAttribute?.isDirty,
                    requiredLevel: mockAttribute?.requiredLevel,
                });
            case "lookup":
                return XrmMockGenerator.Attribute.createLookup({
                    name,
                    value: mockAttribute.value?.valueLookup ?? null,
                    isDirty: mockAttribute?.isDirty,
                    requiredLevel: mockAttribute?.requiredLevel,
                });
            case "optionSet":
                return XrmMockGenerator.Attribute.createOptionSet({
                    name,
                    value: mockAttribute.value?.valueNumber,
                    options: mockAttribute.options ?? [],
                    isDirty: mockAttribute?.isDirty,
                    requiredLevel: mockAttribute?.requiredLevel,
                });
            case "multiSet":
                // xrm-mock does not handle multipset controls properly
                return this.CreateFormContextMultiSelectAttributeMock(name, mockAttribute);
            // case "Xrm.Controls.GridControl":
            // case "Xrm.Controls.KbSearchControl":
            // case "Xrm.Control.QuickFormControl":
            // TO BE DONE MOCK GRID CONTROL
            // return null;
            default:
                return null;
        }
    }

    // Creates mock attributes not as part of the XrmMockGenerator , e.g. for the subGrid attributes
    private CreateXrmMockFakeAttribute(
        name: string,
        mockAttribute: XrmForm.Tester.XrmFormMockAttribute<string>
    ): XrmMockAttributeType | null {
        switch (mockAttribute.type) {
            case "string":
                return new StringAttributeMock({
                    name,
                    value: mockAttribute.value?.valueString,
                    isDirty: mockAttribute?.isDirty,
                    requiredLevel: mockAttribute?.requiredLevel,
                });
            case "boolean":
                return new BooleanAttributeMock({
                    name,
                    value: mockAttribute.value?.valueBoolean,
                    isDirty: mockAttribute?.isDirty,
                    requiredLevel: mockAttribute?.requiredLevel,
                });
            case "date":
                return new DateAttributeMock({
                    name,
                    value: mockAttribute.value?.valueDate,
                    isDirty: mockAttribute?.isDirty,
                    requiredLevel: mockAttribute?.requiredLevel,
                });
            case "number":
                return new NumberAttributeMock({
                    name,
                    value: mockAttribute.value?.valueNumber,
                    isDirty: mockAttribute?.isDirty,
                    requiredLevel: mockAttribute?.requiredLevel,
                });
            case "lookup":
                return new LookupAttributeMock({
                    name,
                    value: mockAttribute.value?.valueLookup ?? null,
                    isDirty: mockAttribute?.isDirty,
                    requiredLevel: mockAttribute?.requiredLevel,
                });
            case "optionSet":
                return new OptionSetAttributeMock({
                    name,
                    value: mockAttribute.value?.valueNumber,
                    options: mockAttribute.options ?? [],
                    isDirty: mockAttribute?.isDirty,
                    requiredLevel: mockAttribute?.requiredLevel,
                });
            case "multiSet":
                // xrm-mock does not handle multi set controls properly
                return this.CreateMultiSelectAttributeMock(name, mockAttribute);
            // case "Xrm.Controls.GridControl":
            // case "Xrm.Controls.KbSearchControl":
            // case "Xrm.Control.QuickFormControl":
            // TO BE DONE MOCK GRID CONTROL
            // return null;
            default:
                return null;
        }
    }

    private CreateFormContextMultiSelectAttributeMock(
        name: string,
        mockAttribute: XrmForm.Tester.XrmFormMockAttribute<TAttributeNames>
    ): OptionSetAttributeMock {
        const multiOptionSetAttr = this.CreateMultiSelectAttributeMock(name, mockAttribute);
        return multiOptionSetAttr;
    }

    private CreateMultiSelectAttributeMock(
        name: string,
        mockAttribute: XrmForm.Tester.XrmFormMockAttribute<string>
    ): OptionSetAttributeMock {
        const multiOptionSetAttr = new OptionSetAttributeMock({
            name,
            value: (mockAttribute.value?.valueNumberMset ?? []) as any,
            options: mockAttribute.options ?? [],
            isDirty: mockAttribute?.isDirty,
            requiredLevel: mockAttribute?.requiredLevel,
        }) as unknown as Xrm.Attributes.MultiSelectOptionSetAttribute;
        multiOptionSetAttr.getSelectedOption = (): Xrm.OptionSetValue[] => {
            const currentValues: number[] = multiOptionSetAttr.getValue() || [];
            return (mockAttribute.options ?? []).filter((opt) => currentValues.includes(opt.value));
        };
        return multiOptionSetAttr as unknown as OptionSetAttributeMock;
    }

    private CreateFormContextBooleanWithNullReturn(
        name: string,
        mockAttribute: XrmForm.Tester.XrmFormMockAttribute<TAttributeNames>
    ): BooleanAttributeMock {
        const initialValue = mockAttribute.value?.valueBoolean === null ? undefined : mockAttribute.value?.valueBoolean;
        const attributeBoolean = XrmMockGenerator.Attribute.createBoolean({
            name,
            initialValue: initialValue,
            value: initialValue,
            isDirty: mockAttribute?.isDirty,
            requiredLevel: mockAttribute?.requiredLevel,
        });
        // Known gap of xrm-mock which cannot properly handle the return of empty/null values force the initial assignment to mock the real dynamics behavior
        attributeBoolean.value = mockAttribute.value?.valueBoolean as any;
        return attributeBoolean;
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
                return this.CreateOptionSetControl(mockAttribute, control);
            case "multiSet":
                return this.CreateMultiOptionSetControl(mockAttribute, control);
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
                name: control.name,
                attribute: <StringAttributeMock>mockAttribute,
                visible: control.isVisible,
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
                visible: control.isVisible,
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
                visible: control.isVisible,
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
                visible: control.isVisible,
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
                visible: control.isVisible,
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
                visible: control.isVisible,
            });
        }
        return null;
    }

    private CreateMultiOptionSetControl(
        mockAttribute: XrmMockAttributeType | null | undefined,
        control: XrmForm.Tester.XrmFormMockControl<TControlName, TAttributeNames>
    ): OptionSetControlMock | null {
        if (mockAttribute && control.attributeName) {
            return XrmMockGenerator.Control.createOptionSet({
                name: control.attributeName,
                attribute: mockAttribute as any,
                visible: control.isVisible,
            });
        }
        return null;
    }

    private CreateMockGrid(control: XrmForm.Tester.XrmFormMockControl<TControlName, TAttributeNames>): GridMock {
        const gridRows = this.xrmSubGridRowMocks.get(control.name);
        if (!gridRows) {
            return new GridMock(new ItemCollectionMock([]), new ItemCollectionMock([]));
        }
        const selectedRows = gridRows.rows.filter((_, index) => {
            if (gridRows.selectedGridRowMocksIndexes.length > 0) {
                return gridRows.selectedGridRowMocksIndexes.includes(index);
            }
            return false;
        });
        return new GridMock(new ItemCollectionMock(gridRows.rows), new ItemCollectionMock(selectedRows));
    }

    private MapFormContext(): ContextMock {
        const context = Context.createContext();
        context.clientUrl = this.clientUrl ?? "";
        context.userSettings.languageId = this.languageId;
        context.userSettings.roles = new ItemCollectionMock(this.userRoles);
        return context;
    }

    private getIsDirty() {
        return Array.from(this.mockAttributes).some(([name, attr]) => attr.isDirty);
    }

    private isGridControl(control: XrmMockControlType | null): control is GridControlMock {
        if (!control) {
            return false;
        }
        const controlType = control.getControlType();
        return controlType === "subgrid";
    }

    private isLookupControl(control: XrmMockControlType | null): control is LookupControlMock {
        if (!control) {
            return false;
        }
        const controlType = control.getControlType();
        return controlType === "lookup";
    }

    private isOptionSetControl(control: XrmMockControlType | null): control is OptionSetControlMock {
        if (!control) {
            return false;
        }
        const controlType = control.getControlType();
        return controlType === "optionset";
    }
}
