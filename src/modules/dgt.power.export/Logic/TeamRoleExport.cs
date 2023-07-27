// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common;
using dgt.power.common.Commands;
using dgt.power.export.Base;
using Microsoft.Xrm.Sdk;

namespace dgt.power.export.Logic;

public class TeamRoleExport : AbstractDataverseCommand<ExportVerb>
{
    public TeamRoleExport(IOrganizationService connection, IConfigResolver configResolver) : base(connection, configResolver)
    {
    }

    // protected override bool Invoke(ExportVerb args)
    // {
    //     // args.FileName = string.IsNullOrWhiteSpace(args.FileName) ? "teamrole.json" : args.FileName;
    //     //
    //     // IEnumerable<Team> teams;
    //     // if (!string.IsNullOrWhiteSpace(args.InlineData))
    //     // {
    //     //     var filter =  string.Empty;
    //     //
    //     //         var fetchXml = "<fetch no-lock=\"true\" >" +
    //     //                    "<entity name=\"systemuser\" >" +
    //     //                    "<attribute name=\"domainname\" /> " +
    //     //                    "<filter type=\"and\" >" +
    //     //                    "<condition attribute=\"isdisabled\" operator=\"eq\" value=\"0\" />" +
    //     //                    filter +
    //     //                    "</filter>" +
    //     //                    "</entity>" +
    //     //                    "</fetch>";
    //     //     var request = new FetchXmlToQueryExpressionRequest {FetchXml = fetchXml};
    //     //     var isValidQuery = _connection.TryExecute<FetchXmlToQueryExpressionRequest, FetchXmlToQueryExpressionResponse>(request, out var response);
    //     //
    //     //     if (!isValidQuery)
    //     //     {
    //     //         AnsiConsole.MarkupLine(CultureInfo.InvariantCulture, "[bold red] Error: The given FetchXML is not valid:");
    //     //         AnsiConsole.MarkupLine($"[bold red] {fetchXml}");
    //     //         return false;
    //     //     }
    //     //
    //     //     var query = response.Query;
    //     //
    //     //     if (query.EntityName != Team.EntityLogicalName)
    //     //     {
    //     //         AnsiConsole.MarkupLine(
    //     //             $"[bold red] Error: The given FetchXML is for entity '{response.Query.EntityName}'. " +
    //     //             $"The FetchXML musst be for '{Team.EntityLogicalName}' entity."
    //     //         );
    //     //
    //     //         return false;
    //     //     }
    //     //
    //     //     teams = _connection.RetrieveMultiple<Team>(query);
    //     // }
    //     // else
    //     // {
    //     //     using var context = new DataContext(_connection);
    //     //     context.TeamSet.Where()
    //     // }
    //     //
    //     // return true;
    // }

    public override ExitCode Execute(ExportVerb settings) => 0;
}
