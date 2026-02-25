import {
    BooleanAttributeMock,
    BooleanControlMock,
    DateAttributeMock,
    DateControlMock,
    GridControlMock,
    GridRowMock,
    LookupAttributeMock,
    LookupControlMock,
    NumberAttributeMock,
    NumberControlMock,
    OptionSetAttributeMock,
    OptionSetControlMock,
    StringAttributeMock,
    StringControlMock,
} from "xrm-mock";
import { EventContextWithEventArgsMock } from "xrm-mock/dist/xrm-mock/events/eventcontextwitheventargs.mock";

export type XrmMockAttributeType =
    | StringAttributeMock
    | BooleanAttributeMock
    | DateAttributeMock
    | NumberAttributeMock
    | LookupAttributeMock
    | OptionSetAttributeMock;

export type XrmMockControlType =
    | BooleanControlMock
    | DateControlMock
    | GridControlMock
    | LookupControlMock
    | NumberControlMock
    | OptionSetControlMock
    | StringControlMock;

export interface XrmMockConfirmDialogResponses {
    confirmed: boolean;
    executed: boolean;
}

export interface XrmFormMockAttributeControlUpdateBase<CtlNames extends string, AttNames extends string> {
    isDisabled?: boolean;
    isVisible?: boolean;
    isDirty?: boolean;
    controlAttributeName: AttNames;
    controlSpecificName?: CtlNames;
    requiredLevel?: Xrm.Attributes.RequirementLevel;
    value?: XrmForm.Tester.XrmMockAttributeValue;
}

export interface XrmFormSectionUpdateBase<TabSectionName extends string> {
    name: TabSectionName;
    isVisible?: boolean;
}

export interface XrmFormTabUpdateBase<TabSectionName extends string> {
    isVisible?: boolean;
    displayState?: Xrm.DisplayState;
    sections?: XrmFormSectionUpdateBase<TabSectionName>[];
}

export interface XrmFormSubGridAttributeUpdateValue<AttNames extends string> {
    name: AttNames;
    value?: XrmForm.Tester.XrmMockAttributeValue;
}

export interface XrmFormGridMockRowConfig<TGridAttributeNames extends string> {
    entityName: string;
    subGridRowsAttributeConfig: XrmForm.Tester.XrmFormMockAttribute<TGridAttributeNames>[];
    gridRows: XrmFormGridMockRowConfigRow<TGridAttributeNames>[];
    selectedRowsIndexes: number[];
}

export interface XrmFormGridMockRowConfigRow<TGridAttributeNames extends string> {
    entityId: string;
    subGridRowsAttributes: XrmFormSubGridAttributeUpdateValue<TGridAttributeNames>[];
}

export interface XrmSubGridRowMocks {
    rows: GridRowMock[];
    selectedGridRowMocksIndexes: number[];
}

export type XrmFormMockServerData = Record<string, XrmTable.DTO.Table<string>[]>;
export type XrmCustomApiMockData = Record<string, XrmWebApi.ExecuteResponse>;

export type XrmWebApiMockStubMethods = jest.FunctionPropertyNames<Required<Xrm.WebApi>>;
export type XrmWebApiOnlineMockStubMethod = jest.FunctionPropertyNames<Required<Xrm.WebApiOnline>>;
export type XrmNavigationApiMockStubMethods = jest.FunctionPropertyNames<Required<Xrm.Navigation>>;
export type XrmUtilityApiMockStubMethod = jest.FunctionPropertyNames<Required<Xrm.Utility>>;
export type XrmFormContextDataStubMethod = jest.FunctionPropertyNames<Required<Xrm.Data>>;
export type XrmFormContextUiNotificationMethods = jest.FunctionPropertyNames<Required<Xrm.Ui>>;
export type XrmSubGridMethodName = jest.FunctionPropertyNames<Required<Xrm.Controls.GridControl>>;
export type XrmLookupControlMethodName = jest.FunctionPropertyNames<Required<Xrm.Controls.LookupControl>>;
export type XrmFormTabMethodNames = jest.FunctionPropertyNames<Required<Xrm.Controls.Tab>>;
export type XrmFormControlMethodNames = jest.FunctionPropertyNames<Required<Xrm.Controls.StandardControl>>;
export type XrmFormAttributeMethods = jest.FunctionPropertyNames<Required<Xrm.Attributes.Attribute<Xrm.Attributes.AttributeValues>>>;
export type XrmConsoleStubMethod = jest.FunctionPropertyNames<Required<typeof console>>;

export type XrmWebApiMockStubs = Partial<Record<XrmWebApiMockStubMethods, jest.Mock>>;
export type XrmWebApiOnlineMockStubs = Partial<Record<XrmWebApiOnlineMockStubMethod, jest.Mock>>;
export type XrmNavigationMockStubs = Partial<Record<XrmNavigationApiMockStubMethods, jest.Mock>>;
export type XrmUtilityApiMockStubs = Partial<Record<XrmUtilityApiMockStubMethod, jest.Mock>>;
export type XrmFormContextUiMockStubs = Partial<Record<XrmFormContextUiNotificationMethods, jest.Mock>>;
export type XrmFormContextDataMockStubs = Partial<Record<XrmFormContextDataStubMethod, jest.Mock>>;
export type XrmConsoleMockStubs = Partial<Record<XrmConsoleStubMethod, jest.Mock>>;
export type XrmSubGridMockStubs = Partial<Record<XrmSubGridMethodName, jest.Mock>>;
export type XrmStandardMockStubs = Partial<Record<XrmFormControlMethodNames, jest.Mock>>;
export type XrmLookupMockStubs = Partial<Record<XrmLookupControlMethodName, jest.Mock>>;

export type XrmTabEventMockStubs = Partial<Record<XrmFormTabMethodNames, jest.Mock>>;
export type XrmFormAttributeMockStubs = Partial<Record<XrmFormAttributeMethods, jest.Mock>>;

export type XrmFormAttributeMethodMockStubs<TAttributeNames extends string> = Partial<Record<TAttributeNames, XrmFormAttributeMockStubs>>;
export type XrmSubGridControlMockStubs<TControlName extends string> = Partial<Record<TControlName, XrmSubGridMockStubs>>;
export type XrmLookupControlMockStubs<TControlName extends string> = Partial<Record<TControlName, XrmLookupMockStubs>>;
export type XrmStandardControlMockStubs<TControlName extends string> = Partial<Record<TControlName, XrmStandardMockStubs>>;
export type XrmFormContextTabEventsStub<TTabNames extends string> = Partial<Record<TTabNames, XrmTabEventMockStubs>>;
export type XrmFormTabUpdateData<TTabNames extends string, TTabSectionNames extends string> = Partial<
    Record<TTabNames, XrmFormTabUpdateBase<TTabSectionNames>>
>;

export interface IXrmMockFormTestContextBuilder<
    TTabNames extends string,
    TSectionNames extends string,
    TControlName extends string,
    TAttributeNames extends string,
> {
    buildMockFormContext(): this;
    getAttribute<T extends XrmMockAttributeType>(attributeName: TAttributeNames): T | null;
    getControl<T extends XrmMockControlType>(controlName: TControlName): T | null;
    getExecutionContextWithSource(
        source: Xrm.Attributes.Attribute | Xrm.Controls.Control | Xrm.Entity
    ): EventContextWithEventArgsMock<Xrm.Events.EventContext>;
    getLoadExecutionContext(): EventContextWithEventArgsMock<Xrm.Events.LoadEventArguments>;
    withAttributeMockChange(isMockAttributeChanges: boolean): this;
    withConsoleMocked(isConsoleMocked: boolean): this;
    WithControlMethodMock(isControlMethodMock: boolean): this;
    withCustomApi<T extends XrmWebApi.ExecuteResponse>(name: string, response: T): this;
    withCustomApiException(name: string, errorMsg: string): this;
    withCustomApis(data: XrmCustomApiMockData): this;
    withEntity(entityName: string, entityId: string): this;
    withFormAttributeControlData(updateData: XrmFormMockAttributeControlUpdateBase<TControlName, TAttributeNames>[]): this;
    withFormDataDirty(isDirty: boolean): this;
    withFormDataEventMock(isMockFormDateEventMock: boolean): this;
    withFormDataValid(isValid: boolean): this;
    withFormTabData(updateData: XrmFormTabUpdateData<TTabNames, TSectionNames>): this;
    withFormType(type: XrmEnum.FormType): this;
    withFormUiEventMock(isMockFormUiEvent: boolean): this;
    withLanguageId(languageId: number): this;
    withLookupControlMethodEventMock(isLookupControlMock: boolean): this;
    withPreSaveEventMock(isPreSaveEventMock: boolean): this;
    withRegisterTabEventsMocks(isMockTabEvents: boolean): this;
    withSaveErrorMessage(errorMsg: string | null): this;
    withServerData(data: XrmFormMockServerData): this;
    withSubGridMethodsMock(isSubGridLoad: boolean): this;
    withSubGridMockRows<TGridAttributeNames extends string>(
        name: TControlName,
        updateGridRows: XrmFormGridMockRowConfig<TGridAttributeNames>
    ): this;
    withUserConfirmDialogSeq(confirmDialogConfirmed: boolean[]): this;
    withUserRoles(roles: Xrm.LookupValue[]): this;
}
