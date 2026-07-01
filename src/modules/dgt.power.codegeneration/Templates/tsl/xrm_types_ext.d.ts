declare namespace XrmTable.DTO {
    export type Table<FieldNames extends string> = Record<FieldNames, number | string | number | boolean | null | undefined>;
}
