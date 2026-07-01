declare namespace XrmWebApi {
    export interface WebApiMetadataObject {
        /**
         * The name of the bound parameter for the action or function to execute.
         */
        boundParameter?: null | undefined | "entity";

        /**
         * Name of the action, function, or one of the following values if you are executing a CRUD request: "Create", "Retrieve", "Update", "Delete" or action name string.
         */
        operationName?: "Create" | "Retrieve" | "Update" | "Delete" | string;

        /**
         * Indicates the type of operation you are executing
         */
        operationType?: WebApiOperationType;

        /**
         * The metadata for parameter types
         */
        parameterTypes: { [key: string]: WebApiParameterType };
    }

    export interface WebApiParameterType {
        /**
         * The fully qualified name of the parameter type.
         */
        typeName: string;

        /**
         * The metadata for enum types.
         */
        enumProperties?: WebApiEnumProperty[];

        /**
         * The category of the parameter type.
         */
        structuralProperty: WebApiStructuralProperty;
    }

    export interface WebApiEnumProperty {
        name: string;
        value: number;
    }

    export interface WebApiGuidProperty {
        guid: string;
    }

    export interface ExecuteRequest {
        getMetadata(): WebApiMetadataObject;
    }

    export interface ExecuteResponse { }

    export const enum WebApiOperationType {
        Action = 0,
        Function = 1,
        CRUD = 2,
    }

    export const enum WebApiStructuralProperty {
        Unknown = 0,
        PrimitiveType = 1,
        ComplexType = 2,
        EnumerationType = 3,
        Collection = 4,
        EntityType = 5,
    }
}
