import { defaultParser, Token, TokenType } from "@odata/parser";

export type TokenValue = string | number | boolean | null | undefined | Date;
export class XrmMockFormODataFilter {
    // Map OData operators to JS logic
    private static readonly ops: Record<string, (a: TokenValue, b: TokenValue) => boolean> = {
        [TokenType.EqualsExpression]: (a, b) => {
            if (
                a === b ||
                ((a === null || a === undefined) && (b === null || b === undefined)) ||
                a?.toLocaleString() === b?.toLocaleString()
            ) {
                return true;
            }
            return false;
        },
        [TokenType.NotEqualsExpression]: (a, b) => a != b, // to consider null/undefined
        [TokenType.GreaterThanExpression]: (a, b) => {
            if (a === undefined || a === null || b === undefined || b === null) {
                return false;
            }
            return a > b;
        },
        [TokenType.GreaterOrEqualsExpression]: (a, b) => {
            if (a === undefined || a === null || b === undefined || b === null) {
                return false;
            }
            return a >= b;
        },
        [TokenType.LesserThanExpression]: (a, b) => {
            if (a === undefined || a === null || b === undefined || b === null) {
                return false;
            }
            return a < b;
        },
        [TokenType.LesserOrEqualsExpression]: (a, b) => {
            if (a === undefined || a === null || b === undefined || b === null) {
                return false;
            }
            return a <= b;
        },
    };

    public static executeRetrieveMultipleRecord(entityLogicalName: string, list: XrmTable.DTO.Table<string>[], oDataString: string): XrmTable.DTO.Table<string>[] {
        try {
            console.log(`Run next query in mock data ${oDataString}`);
            const astObject = defaultParser.query(oDataString);
            const astOptions = astObject.value.options ?? [];
            const selectFields = XrmMockFormODataFilter.getSelectFieldNamesFromAstObject(astOptions);
            selectFields.push(`${entityLogicalName}id`);
            const filterOption = XrmMockFormODataFilter.getFilterFromAstObject(astOptions);
            if (!filterOption) {
                return list.map((x) => XrmMockFormODataFilter.selectProperties(x, selectFields));
            }
            const filteredItems = list.filter((item) => XrmMockFormODataFilter.filterListItem(item, filterOption));
            return filteredItems.map((x) => XrmMockFormODataFilter.selectProperties(x, selectFields));
        } catch (error) {
            console.error(`Failed to parse the OData query ${error}`);
            return list;
        }
    }

    /**
     * Given an Item provide the
     * @param item Table item into which we will apply the oData select
     * @param oDataString oDataString with the select expression to apply to the table
     * @returns
     */
    public static executeSelectSingleRecord(item: XrmTable.DTO.Table<string>, oDataString?: string): XrmTable.DTO.Table<string> {
        if (!oDataString) {
            // if no select return just the whole item
            return item;
        }
        const ast = defaultParser.query(oDataString);
        const astOptions = ast.value.options ?? [];
        const selectFields = XrmMockFormODataFilter.getSelectFieldNamesFromAstObject(astOptions);
        return XrmMockFormODataFilter.selectProperties(item, selectFields);
    }

    private static getSelectFieldNamesFromAstObject(astOptions: Token[]): string[] {
        const selectFieldsToken: Token[] = astOptions.find((opt) => opt.type === TokenType.Select)?.value?.items ?? [];
        return selectFieldsToken.map((x) => x.raw);
    }

    private static getFilterFromAstObject(astOptions: Token[]): Token | null {
        return astOptions.find((opt) => opt.type === TokenType.Filter) ?? null;
    }

    private static selectProperties(listItem: XrmTable.DTO.Table<string>, returnKeys: string[]): XrmTable.DTO.Table<string> {
        const result: XrmTable.DTO.Table<string> = {};
        returnKeys.forEach((fieldName) => {
            if (XrmMockFormODataFilter.isFieldNodeStartAndEndWithLimiters(fieldName, "_", "_value")) {
                const dtoFieldName = XrmMockFormODataFilter.deleteRawNodeStartAndEnd(fieldName, "_", "_value");
                const formattedFieldName = `${fieldName}@OData.Community.Display.V1.FormattedValue`;
                const formattedDtoFieldName = `${dtoFieldName}_name_`;
                result[fieldName] = listItem[dtoFieldName];
                if (listItem[formattedDtoFieldName]) {
                    result[formattedFieldName] = listItem[formattedDtoFieldName];
                } else if (result[fieldName]) {
                    result[formattedFieldName] = `name_${result[fieldName]}`;
                }
            } else {
                result[fieldName] = listItem[fieldName];
            }
        });
        return result;
    }

    private static filterListItem(item: XrmTable.DTO.Table<string>, filterQueryAst: Token): boolean {
        return XrmMockFormODataFilter.evaluate(item, filterQueryAst.value);
    }

    private static evaluate(item: XrmTable.DTO.Table<string>, node: Token): boolean {
        switch (node.type) {
            case TokenType.AndExpression:
                return XrmMockFormODataFilter.evaluate(item, node.value.left) && XrmMockFormODataFilter.evaluate(item, node.value.right);
            case TokenType.OrExpression:
                return XrmMockFormODataFilter.evaluate(item, node.value.left) || XrmMockFormODataFilter.evaluate(item, node.value.right);
            case TokenType.NotExpression:
                return !XrmMockFormODataFilter.evaluate(item, node.value);
            case TokenType.EqualsExpression:
            case TokenType.NotEqualsExpression:
            case TokenType.GreaterThanExpression:
            case TokenType.GreaterOrEqualsExpression:
            case TokenType.LesserThanExpression:
            case TokenType.LesserOrEqualsExpression:
                return XrmMockFormODataFilter.ops[node.type](
                    XrmMockFormODataFilter.resolveValue(item, node.value.left),
                    XrmMockFormODataFilter.resolveValue(item, node.value.right)
                );
            case TokenType.MethodCallExpression:
                return XrmMockFormODataFilter.handleFunction(item, node);

            default:
                return true;
        }
    }

    private static resolveValue(item: XrmTable.DTO.Table<string>, node: Token): TokenValue {
        // assumption all expression have fields as first value in the expression
        if (node.type === TokenType.FirstMemberExpression) {
            const fieldName = XrmMockFormODataFilter.convertNodeToFieldName(node);
            if (typeof fieldName === "string" && item[fieldName] !== undefined) {
                return item[fieldName];
            }
        }
        if (node.raw == "null") {
            return null;
        }
        if (node.type == TokenType.Literal && typeof node.raw === "string") {
            return XrmMockFormODataFilter.deleteRawNodeStartAndEnd(node.raw, "'", "'");
        }
        return node.raw;
    }

    private static convertNodeToFieldName(node: Token): TokenValue {
        if (typeof node.raw === "string") {
            return XrmMockFormODataFilter.deleteRawNodeStartAndEnd(node.raw, "_", "_value");
        }
        return node.raw;
    }

    private static deleteRawNodeStartAndEnd(nodeRaw: string, startLimiter: string, endLimiter: string): string {
        if (XrmMockFormODataFilter.isFieldNodeStartAndEndWithLimiters(nodeRaw, startLimiter, endLimiter)) {
            return nodeRaw.substring(nodeRaw.indexOf(startLimiter) + 1, nodeRaw.lastIndexOf(endLimiter));
        }
        return nodeRaw;
    }

    private static isFieldNodeStartAndEndWithLimiters(nodeRaw: string, startLimiter: string, endLimiter: string): boolean {
        return nodeRaw.startsWith(startLimiter) && nodeRaw.endsWith(endLimiter);
    }

    private static handleFunction(item: XrmTable.DTO.Table<string>, node: Token): boolean {
        // Resolve arguments (e.g., the property value and the literal string)
        if (!!node.value.parameters && node.value.parameters.length > 1) {
            const fieldName: string | null = node.value?.parameters?.[0]?.raw ?? null;
            const fieldValue: string | null = node.value?.parameters?.[1]?.raw ?? null;
            const operator = node.value.method;
            if (fieldName && fieldValue) {
                const target = item[fieldName];
                switch (operator) {
                    case "contains":
                        return target?.toLocaleString().includes(fieldValue) ?? false;
                    case "startswith":
                        return target?.toLocaleString().startsWith(fieldValue) ?? false;
                    case "endswith":
                        return target?.toLocaleString().endsWith(fieldValue) ?? false;
                    default:
                        return false;
                }
            }
        }
        return false;
    }
}
