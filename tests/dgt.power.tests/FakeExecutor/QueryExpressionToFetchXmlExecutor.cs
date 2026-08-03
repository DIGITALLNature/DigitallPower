// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Text;
using Digitall.Dataverse.Testing;
using Digitall.Dataverse.Testing.OrganizationRequests;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

#pragma warning disable CS8602

namespace dgt.power.tests.FakeExecutor;

public class QueryExpressionToFetchXmlExecutor : IOrganizationRequestFake
{
    public Type ForType => typeof(QueryExpressionToFetchXmlRequest);

    public OrganizationResponse Execute(OrganizationRequest organizationRequest, FakeOrganizationService fakeOrganizationService)
    {
        var typed = (QueryExpressionToFetchXmlRequest)organizationRequest;

        var query = typed.Query as QueryExpression;

        var fetchXml = new StringBuilder();
        fetchXml.Append("<fetch version=\"1.0\" output-format=\"xml-platform\" mapping=\"logical\" ><entity name=\"");
        fetchXml.Append(query.EntityName);
        fetchXml.Append("\" >");

        foreach (var column in query.ColumnSet.Columns.ToList())
        {
            fetchXml.Append("<attribute name=\"");
            fetchXml.Append(column);
            fetchXml.Append("\" />");
        }

        foreach (var orders in query.Orders.ToList())
        {
            fetchXml.Append("<order attribute=\"");
            fetchXml.Append(orders.AttributeName);
            fetchXml.Append("\" descending=\"");
            fetchXml.Append(orders.OrderType == OrderType.Descending ? "true" : "false");
            fetchXml.Append("\" />");
        }

        fetchXml.Append("</entity>");
        fetchXml.Append("</fetch>");

        return new QueryExpressionToFetchXmlResponse
        {
            Results =
            {
                ["FetchXml"] = fetchXml.ToString()
            }
        };
    }
}
