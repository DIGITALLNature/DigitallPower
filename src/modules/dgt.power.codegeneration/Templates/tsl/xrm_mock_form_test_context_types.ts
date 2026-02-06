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

export interface IXrmMockFormTestContextBuilder {
    GetLoadExecutionContext(): EventContextWithEventArgsMock<Xrm.Events.LoadEventArguments>;
    WithData(updateData: XrmForm.Tester.XrmFormMockAttributeControlUpdateBase[]): this;
    BuildMockFormContext(): this;
}
