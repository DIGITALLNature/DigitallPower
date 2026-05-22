// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Xml.Linq;
using Digitall.Dataverse.Testing;
using Digitall.Dataverse.Testing.OrganizationRequests;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace dgt.power.tests.FakeExecutor;

/// <summary>
/// Handles <see cref="FetchXmlToQueryExpressionRequest"/> by parsing the FetchXml
/// and building a basic <see cref="QueryExpression"/>.
/// </summary>
public class FetchXmlToQueryExpressionRequestFake : IOrganizationRequestFake
{
    public Type ForType => typeof(FetchXmlToQueryExpressionRequest);

    public OrganizationResponse Execute(OrganizationRequest organizationRequest, FakeOrganizationService state)
    {
        var request = (FetchXmlToQueryExpressionRequest)organizationRequest;
        var queryExpression = ConvertFetchXmlToQueryExpression(request.FetchXml);

        return new FetchXmlToQueryExpressionResponse
        {
            Results =
            {
                ["Query"] = queryExpression
            }
        };
    }

    private static QueryExpression ConvertFetchXmlToQueryExpression(string fetchXml)
    {
        var doc = XDocument.Parse(fetchXml);
        var fetchElement = doc.Root!;
        var entityElement = fetchElement.Element("entity")!;
        var entityName = entityElement.Attribute("name")!.Value;

        var query = new QueryExpression(entityName);

        // Parse columns
        if (entityElement.Element("all-attributes") != null)
        {
            query.ColumnSet = new ColumnSet(true);
        }
        else
        {
            var attributes = entityElement.Elements("attribute")
                .Select(a => a.Attribute("name")!.Value)
                .ToArray();
            query.ColumnSet = attributes.Length > 0 ? new ColumnSet(attributes) : new ColumnSet(true);
        }

        // Parse filter
        var filterElement = entityElement.Element("filter");
        if (filterElement != null)
        {
            query.Criteria = ParseFilter(filterElement);
        }

        // Parse order
        foreach (var orderElement in entityElement.Elements("order"))
        {
            var attributeName = orderElement.Attribute("attribute")!.Value;
            var descending = orderElement.Attribute("descending")?.Value == "true";
            query.AddOrder(attributeName, descending ? OrderType.Descending : OrderType.Ascending);
        }

        // Parse top/count
        var topAttr = fetchElement.Attribute("top");
        if (topAttr != null && int.TryParse(topAttr.Value, out var top))
        {
            query.TopCount = top;
        }

        var countAttr = fetchElement.Attribute("count");
        if (countAttr != null && int.TryParse(countAttr.Value, out var count))
        {
            query.PageInfo = new PagingInfo { Count = count, PageNumber = 1 };
        }

        return query;
    }

    private static FilterExpression ParseFilter(XElement filterElement)
    {
        var filterType = filterElement.Attribute("type")?.Value == "or"
            ? LogicalOperator.Or
            : LogicalOperator.And;
        var filter = new FilterExpression(filterType);

        foreach (var condition in filterElement.Elements("condition"))
        {
            var attribute = condition.Attribute("attribute")!.Value;
            var operatorStr = condition.Attribute("operator")!.Value;
            var valueAttr = condition.Attribute("value");

            var conditionOperator = ParseOperator(operatorStr);

            if (conditionOperator == ConditionOperator.In ||
                conditionOperator == ConditionOperator.NotIn)
            {
                var values = condition.Elements("value")
                    .Select(v => (object)v.Value)
                    .ToArray();
                filter.AddCondition(new ConditionExpression(attribute, conditionOperator, values));
            }
            else if (conditionOperator == ConditionOperator.Null ||
                     conditionOperator == ConditionOperator.NotNull)
            {
                filter.AddCondition(new ConditionExpression(attribute, conditionOperator));
            }
            else if (valueAttr != null)
            {
                filter.AddCondition(new ConditionExpression(attribute, conditionOperator, valueAttr.Value));
            }
            else
            {
                filter.AddCondition(new ConditionExpression(attribute, conditionOperator));
            }
        }

        // Nested filters
        foreach (var nestedFilter in filterElement.Elements("filter"))
        {
            filter.Filters.Add(ParseFilter(nestedFilter));
        }

        return filter;
    }

    private static ConditionOperator ParseOperator(string op) => op switch
    {
        "eq" => ConditionOperator.Equal,
        "ne" or "neq" => ConditionOperator.NotEqual,
        "gt" => ConditionOperator.GreaterThan,
        "ge" => ConditionOperator.GreaterEqual,
        "lt" => ConditionOperator.LessThan,
        "le" => ConditionOperator.LessEqual,
        "like" => ConditionOperator.Like,
        "not-like" => ConditionOperator.NotLike,
        "in" => ConditionOperator.In,
        "not-in" => ConditionOperator.NotIn,
        "null" => ConditionOperator.Null,
        "not-null" => ConditionOperator.NotNull,
        "between" => ConditionOperator.Between,
        "not-between" => ConditionOperator.NotBetween,
        "above" => ConditionOperator.Above,
        "under" => ConditionOperator.Under,
        _ => ConditionOperator.Equal
    };
}
