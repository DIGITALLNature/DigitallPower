declare namespace XrmForm.Tester {
    export interface XrmMockFormTestContextBuilder<
        TTabNames extends string,
        TSectionNames extends string,
        TControlName extends string,
        TAttributeNames extends string,
    > {
        buildMockFormContext(): this;
        getAttribute<T extends Xrm.Attributes.Attribute>(attributeName: TAttributeNames): T | null;
        getControl<T extends Xrm.Controls.Control>(controlName: TControlName): T | null;
        getExecutionContextWithSource(
            source: Xrm.Attributes.Attribute | Xrm.Controls.Control | Xrm.Entity,
        ): Xrm.Events.EventContext;
        getMockFormContext<T extends Xrm.FormContext>(): T;
        getLoadExecutionContext(): Xrm.Events.LoadEventArguments;
        withAttributeMockChange(isMockAttributeChanges: boolean): this;
        withClientUrl(clientUrl: string): this;
        withConsoleMocked(isConsoleMocked: boolean): this;
        WithControlMethodMock(isControlMethodMock: boolean): this;
        withCustomApi<T extends XrmWebApi.ExecuteResponse>(name: string, response: T): this;
        withCustomApiException(name: string, errorMsg: string): this;
        withCustomApis(data: XrmCustomApiMockData): this;
        withEntity(entityName: string, entityId: string): this;
        withFetchResponse(statusCode: number, response: string): this;
        withFetchException(errorMsg: string): this;
        withFormAttributeControlData(
            updateData: XrmFormMockAttributeControlUpdateBase<TControlName, TAttributeNames>[],
        ): this;
        withFormDataDirty(isDirty: boolean): this;
        withFormDataEventMock(isMockFormDateEventMock: boolean): this;
        withFormDataValid(isValid: boolean): this;
        withFormSelectorItems(
            currentForm: XrmFormMockFormItem,
            otherItems: XrmFormMockFormItem[],
        ): this;
        withFormTabData(updateData: XrmFormTabUpdateData<TTabNames, TSectionNames>): this;
        withFormType(type: XrmEnum.FormType): this;
        withFormUiEventMock(isMockFormUiEvent: boolean): this;
        withLanguageId(languageId: number): this;
        withLookupControlMethodEventMock(isLookupControlMock: boolean): this;
        withPreSaveEventMock(isPreSaveEventMock: boolean): this;
        withRefreshErrorMessage(errorMsg: string | null): this;
        withRegisterTabEventsMocks(isMockTabEvents: boolean): this;
        withRetrieveServerDataError(erroMsg: string): this;
        withSaveErrorMessage(errorMsg: string | null): this;
        withServerData(data: XrmFormMockServerData): this;
        withSelectedSubGridRows(
            name: TControlName,
            entityName: string,
            selectedIds: string[],
        ): this;
        withSubGridMethodsMock(isSubGridLoad: boolean): this;
        withSubGridMockRows<TGridAttributeNames extends string>(
            name: TControlName,
            updateGridRows: XrmFormGridMockRowConfig<TGridAttributeNames>,
        ): this;
        withUpdateServerDateErrorMessage(erroMsg: string): this;
        withUserConfirmDialogSeq(confirmDialogConfirmed: boolean[]): this;
        withUserRoles(roles: Xrm.LookupValue[]): this;
    }

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
        valueBoolean?: boolean | null;
        valueString?: string | null;
        valueNumber?: number | null;
        valueNumberMset?: number[] | null;
        valueDate?: Date | null;
        valueLookup?: Xrm.LookupValue[] | null;
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
