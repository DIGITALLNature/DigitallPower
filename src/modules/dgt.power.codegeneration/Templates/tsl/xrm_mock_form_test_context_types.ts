import {
    BooleanAttributeMock,
    BooleanControlMock,
    DateAttributeMock,
    DateControlMock,
    GridControlMock,
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

export type XrmMockUiNotificationMethods = "setFormNotification" | "clearFormNotification";

export type XrmMockUiNotification = Partial<Record<XrmMockUiNotificationMethods, jest.Mock>>;

export interface XrmMockConfirmDialogResponses {
    confirmed: boolean;
    executed: boolean;
}

export interface XrmFormMockAttributeControlUpdateBase<CtlNames extends string, AttNames extends string> {
    isDisabled?: boolean;
    isVisible?: boolean;
    controlAttributeName: AttNames;
    controlSpecificName?: CtlNames;
    requiredLevel?: Xrm.Attributes.RequirementLevel;
    value?: XrmForm.Tester.XrmMockAttributeValue;
}

export type XrmFormMockServerData = Record<string, XrmTable.DTO.Table<string>[]>;
export type XrmCustomApiMockData = Record<string, XrmWebApi.ExecuteResponse>;

export type XrmWebApiMockStubMethods = jest.FunctionPropertyNames<Required<Xrm.WebApi>>;
export type XrmWebApiOnlineMockStubMethod = jest.FunctionPropertyNames<Required<Xrm.WebApiOnline>>;
export type XrmNavigationApiMockStubMethods = jest.FunctionPropertyNames<Required<Xrm.Navigation>>;
export type XrmUtilityApiMockStubMethod = jest.FunctionPropertyNames<Required<Xrm.Utility>>;

export type XrmWebApiMockStubs = Partial<Record<XrmWebApiMockStubMethods, jest.Mock>>;
export type XrmWebApiOnlineMockStubs = Partial<Record<XrmWebApiOnlineMockStubMethod, jest.Mock>>;
export type XrmNavigationMockStubs = Partial<Record<XrmNavigationApiMockStubMethods, jest.Mock>>;
export type XrmUtilityApiMockStubs = Partial<Record<XrmUtilityApiMockStubMethod, jest.Mock>>;

export interface IXrmMockFormTestContextBuilder<TControlNames extends string, TAttributeNames extends string> {
    buildMockFormContext(): this;
    getAttribute<T extends XrmMockAttributeType>(attributeName: TAttributeNames): T | null;
    getControl<T extends XrmMockControlType>(controlName: TControlNames): T | null;
    getLoadExecutionContext(): EventContextWithEventArgsMock<Xrm.Events.LoadEventArguments>;
    withEntityId(entityName: string, entityId: string): this;
    withFormData(updateData: XrmFormMockAttributeControlUpdateBase<TControlNames, TAttributeNames>[]): this;
    withFormType(type: XrmEnum.FormType): this;
    withUserConfirmDialogSeq(confirmDialogConfirmed: boolean[]): this;
    withServerData(data: XrmFormMockServerData): this;
    withCustomApi(data: XrmCustomApiMockData): this;
}
