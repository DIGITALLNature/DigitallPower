// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;
using dgt.power.common;
using dgt.power.common.Extensions;
using dgt.power.common.FileAccess;
using dgt.power.dataverse;
using dgt.power.dto;
using dgt.power.export.Base;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace dgt.power.export.Logic;

public sealed class UserRoleExport : BaseExport
{
    public UserRoleExport(ITracer tracer, IOrganizationService connection, IConfigResolver configResolver, IFileService fileService)
        : base(tracer, connection, configResolver, fileService)
    {
    }

    protected override bool Invoke(ExportVerb args)
    {
        Debug.Assert(args != null, nameof(args) + " != null");
        Tracer.Start(this);

        var fileDir = args.FileDir;
        var fileName = string.IsNullOrWhiteSpace(args.FileName) ? "userrole.json" : args.FileName;

        var separator = args.InlineData;
        var filter = string.Empty;
        if (!string.IsNullOrWhiteSpace(args.InlineData) && args.InlineData.IndexOf("<", StringComparison.InvariantCulture) >= 0)
        {
            var idx = args.InlineData.IndexOf("<", StringComparison.InvariantCulture);
            separator = args.InlineData.Substring(0, idx);
            filter = args.InlineData.Substring(idx);
        }

        if (!GetQueryExpression(Tracer, Connection, filter, out var query))
        {
            return Tracer.End(this, false);
        }

        var json = GetJson(GetUsers(Connection, query!.Criteria, separator));

        Tracer.Log($"Export {HandleExportFile(fileDir, fileName, json)} ", TraceEventType.Information);
        return Tracer.End(this, true);
    }

    private static List<UserRole> GetUsers(IOrganizationService service, FilterExpression filter, string separator)
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
            PageInfo = new PagingInfo
            {
                Count = 5000,
                PageNumber = 1,
                PagingCookie = null
            }
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

        bool moreRecords = true;
        while (moreRecords)
        {
            // Retrieve the page.
            var results = service.RetrieveMultiple(userQuery);
            foreach (var entity in results.Entities)
            {
                var systemUser = entity.ToEntity<SystemUser>();
                if (!userRoles.ContainsKey(systemUser.Id))
                {
                    var bu = GetAttribute<string>(systemUser, $"bu.{BusinessUnit.LogicalNames.Name}");
                    var pbu = GetAttribute<string>(systemUser, $"pbu.{BusinessUnit.LogicalNames.Name}");
                    userRoles.Add(systemUser.Id, new UserRole
                    {
                        UserName = systemUser.DomainName,
#pragma warning disable CS8601
                        BusinessUnit = string.IsNullOrWhiteSpace(pbu) ? bu : $"{bu}{separator[0]}{pbu}",
#pragma warning restore CS8601
                        BusinessUnitSeparator = separator[0]
                    });
                }

                var role = GetAttribute<string>(systemUser, $"ro.{Role.LogicalNames.Name}");
                if (!string.IsNullOrWhiteSpace(role))
                {
                    userRoles[systemUser.Id].SecurityRoles.Add(role);
                }
            }

            if (results.MoreRecords)
            {
                userQuery.PageInfo.PageNumber++;
                userQuery.PageInfo.PagingCookie = results.PagingCookie;
            }

            moreRecords = results.MoreRecords;
        }

        foreach (var userRole in userRoles)
        {
            userRole.Value.SecurityRoles.ToList().Sort();
        }

        return userRoles.Values.OrderBy(e => e.UserName).ToList();
    }

    private static T? GetAttribute<T>(Entity entity, string attribute) =>
        entity.Attributes.ContainsKey(attribute) ? (T)entity.GetAttributeValue<AliasedValue>(attribute).Value : default;

    private static bool GetQueryExpression(ITracer tracer, IOrganizationService service, string filter, out QueryExpression? query)
    {
        var fetchXml = string.Empty;
        query = default;
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
            var response = (FetchXmlToQueryExpressionResponse)service.Execute(new FetchXmlToQueryExpressionRequest
            {
                FetchXml = fetchXml
            });
            query = response.Query;
            return true;
        }
        catch (Exception e)
        {
            tracer.Log($"Invalid fetch-xml: {e.RootMessage()}; check fetch: {fetchXml}", TraceEventType.Error);
            return false;
        }
    }
}
