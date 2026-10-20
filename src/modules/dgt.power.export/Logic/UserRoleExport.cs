// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Globalization;
using dgt.power.common;
using dgt.power.common.Extensions;
using dgt.power.common.FileAccess;
using dgt.power.dataverse;
using dgt.power.dto;
using dgt.power.export.Base;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Spectre.Console;

namespace dgt.power.export.Logic;

public sealed class UserRoleExport(
    ICommandSpanFactory spans,
    IDataverseConnection connection,
    IFileService fileService,
    IAnsiConsole console)
    : PowerCommand<ExportVerb>(spans, console)
{
    protected override async Task<bool> RunAsync(ExportVerb args, CancellationToken cancellationToken)
    {
        var service = await connection.GetServiceAsync();
        var fileDir = args.FileDir;
        var fileName = string.IsNullOrWhiteSpace(args.FileName) ? "userrole.json" : args.FileName;

        var separator = args.InlineData;
        var filter = string.Empty;
        if (!string.IsNullOrWhiteSpace(args.InlineData))
        {
            var idx = args.InlineData.IndexOf('<', StringComparison.Ordinal);
            if (idx >= 0)
            {
                separator = args.InlineData.Substring(0, idx);
                filter = args.InlineData.Substring(idx);
            }
        }

        var query = await GetQueryExpressionAsync(Console, service, filter, cancellationToken);
        if (query == null)
        {
            return false;
        }

        var json = ExportJson.GetJson(await GetUsersAsync(service, query.Criteria, separator, cancellationToken));

        Console.WriteLine($"Export {fileService.ExportFile(fileDir, fileName, json)} ");
        return true;
    }

    private static async Task<List<UserRole>> GetUsersAsync(IOrganizationServiceAsync2 service, FilterExpression filter, string separator,
        CancellationToken cancellationToken)
    {
        var userRoles = new Dictionary<Guid, UserRole>();

        if (string.IsNullOrWhiteSpace(separator))
        {
            separator = "/";
        }

        // user
        var userQuery = new QueryExpression
        {
            EntityName = SystemUser.EntityLogicalName,
            ColumnSet = new ColumnSet(SystemUser.LogicalNames.SystemUserId, SystemUser.LogicalNames.DomainName,
                SystemUser.LogicalNames.BusinessUnitId),
            NoLock = true,
            Criteria = filter,
            Orders =
            {
                new OrderExpression(SystemUser.LogicalNames.DomainName, OrderType.Ascending)
            },
        };

        // business unit
        var buLink = userQuery.AddLink(BusinessUnit.EntityLogicalName, SystemUser.LogicalNames.BusinessUnitId,
            BusinessUnit.LogicalNames.BusinessUnitId, JoinOperator.Inner);
        buLink.Columns.AddColumns(
            BusinessUnit.LogicalNames.Name
        );
        buLink.EntityAlias = "bu";
        // parent business unit
        var pbuLink = buLink.AddLink(BusinessUnit.EntityLogicalName, BusinessUnit.LogicalNames.ParentBusinessUnitId,
            BusinessUnit.LogicalNames.BusinessUnitId, JoinOperator.LeftOuter);
        pbuLink.Columns.AddColumns(
            BusinessUnit.LogicalNames.Name
        );
        pbuLink.EntityAlias = "pbu";

        // security roles
        var srLink = userQuery.AddLink(SystemUserRoles.EntityLogicalName, SystemUser.LogicalNames.SystemUserId,
            SystemUserRoles.LogicalNames.SystemUserId, JoinOperator.LeftOuter);
        var roLink = srLink.AddLink(Role.EntityLogicalName, SystemUserRoles.LogicalNames.RoleId, Role.LogicalNames.RoleId, JoinOperator.LeftOuter);
        roLink.Columns.AddColumns(
            Role.LogicalNames.Name
        );
        roLink.EntityAlias = "ro";

        var results = await service.RetrieveAllAsync(userQuery, cancellationToken: cancellationToken);
        foreach (var entity in results)
        {
            var systemUser = entity.ToEntity<SystemUser>();
            if (!userRoles.TryGetValue(systemUser.Id, out var userRole))
            {
                var bu = GetAttribute<string>(systemUser, $"bu.{BusinessUnit.LogicalNames.Name}");
                var pbu = GetAttribute<string>(systemUser, $"pbu.{BusinessUnit.LogicalNames.Name}");
                userRole = new UserRole
                {
                    UserName = systemUser.DomainName,
#pragma warning disable CS8601
                    BusinessUnit = string.IsNullOrWhiteSpace(pbu) ? bu : $"{bu}{separator[0]}{pbu}",
#pragma warning restore CS8601
                    BusinessUnitSeparator = separator[0]
                };
                userRoles.Add(systemUser.Id, userRole);
            }

            var role = GetAttribute<string>(systemUser, $"ro.{Role.LogicalNames.Name}");
            if (!string.IsNullOrWhiteSpace(role))
            {
                userRole.SecurityRoles.Add(role);
            }
        }

        foreach (var userRole in userRoles)
        {
            userRole.Value.SecurityRoles.ToList().Sort();
        }

        return userRoles.Values.OrderBy(e => e.UserName).ToList();
    }

    private static T? GetAttribute<T>(Entity entity, string attribute)
        where T : class =>
        entity.Attributes.ContainsKey(attribute) ? (T)entity.GetAttributeValue<AliasedValue>(attribute).Value : null;

    private static async Task<QueryExpression?> GetQueryExpressionAsync(IAnsiConsole console, IOrganizationServiceAsync2 service, string filter,
        CancellationToken cancellationToken)
    {
        var fetchXml = string.Empty;
        try
        {
            fetchXml = "<fetch no-lock=\"true\" >" +
                       "<entity name=\"systemuser\" >" +
                       "<attribute name=\"domainname\" /> " +
                       "<filter type=\"and\" >" +
                       "<condition attribute=\"isdisabled\" operator=\"eq\" value=\"0\" />" +
                       filter +
                       "</filter>" +
                       "</entity>" +
                       "</fetch>";
            var response = (FetchXmlToQueryExpressionResponse)await service.ExecuteAsync(new FetchXmlToQueryExpressionRequest
            {
                FetchXml = fetchXml
            }, cancellationToken);
            response.Query.NoLock = true;
            return response.Query;
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception e) when (e is not OutOfMemoryException and not StackOverflowException)
        {
            console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[red]Invalid fetch-xml: {e.RootMessage()}; check fetch: {fetchXml}[/]");
            return null;
        }
    }
}
