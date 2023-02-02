using System.Text;
using FakeXrmEasy.Abstractions;
using FakeXrmEasy.Abstractions.FakeMessageExecutors;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
#pragma warning disable CS8602

namespace dgt.power.tests.FakeExecutor;

public class QueryExpressionToFetchXmlExecutor : IFakeMessageExecutor
{
    public bool CanExecute(OrganizationRequest request)
    {
        return request is QueryExpressionToFetchXmlRequest;
    }

    public Type GetResponsibleRequestType()
    {
        return typeof(QueryExpressionToFetchXmlRequest);
    }

    public OrganizationResponse Execute(OrganizationRequest request, IXrmFakedContext ctx)
    {
        var typed = (QueryExpressionToFetchXmlRequest)request;

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

        var response = new QueryExpressionToFetchXmlResponse
        {
            Results =
            {
                ["FetchXml"] = fetchXml.ToString()
            }
        };

        return response;
    }
}
