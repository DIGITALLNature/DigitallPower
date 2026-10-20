// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Globalization;
using dgt.power.common;
using dgt.power.common.Extensions;
using dgt.power.dataverse;
using dgt.power.dto;
using dgt.power.import.Base;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using Spectre.Console;

namespace dgt.power.import.Logic;

public sealed class UserRoleImport(
    ICommandSpanFactory spans,
    IDataverseConnection connection,
    IConfigResolver configResolver,
    IAnsiConsole console)
    : PowerCommand<ImportVerb>(spans, console)
{
    protected override async Task<bool> RunAsync(ImportVerb args, CancellationToken cancellationToken)
    {
        var fileName = string.IsNullOrWhiteSpace(args.FileName) ? "userrole.json" : args.FileName;

        if (!configResolver.TryGetConfigFile<List<UserRole>>(args.FileDir, fileName, out var userRoles))
        {
            Console.MarkupLine("[red]User role configuration not found.[/]");
            return false;
        }

        if (userRoles.Count == 0)
        {
            Console.MarkupLine("[red]User role configuration is empty.[/]");
            return false;
        }

        var service = await connection.GetServiceAsync();
        var result = true;
        foreach (var userRole in userRoles)
        {
            Console.WriteLine(
                $"UserName: {userRole.UserName}; BusinessUnit:{userRole.BusinessUnit}; SecurityRoles:[{string.Join("|", userRole.SecurityRoles)}]");

            // SystemUser
            var userQuery = new QueryExpression(SystemUser.EntityLogicalName)
            {
                NoLock = true,
                ColumnSet = new ColumnSet(SystemUser.LogicalNames.SystemUserId, SystemUser.LogicalNames.DomainName,
                    SystemUser.LogicalNames.BusinessUnitId),
                Criteria =
                {
                    Conditions =
                    {
                        new ConditionExpression(SystemUser.LogicalNames.DomainName, ConditionOperator.Equal, userRole.UserName)
                    }
                }
            };
            var user = (await service.RetrieveAllAsync(userQuery, cancellationToken: cancellationToken))
                .SingleOrDefault()?.ToEntity<SystemUser>();
            if (user == null)
            {
                Console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[yellow]Can't find User '{userRole.UserName}' ...[/]");
                continue;
            }

            // BusinessUnit
            var businessUnits = userRole.BusinessUnit.Split(userRole.BusinessUnitSeparator);
            if (businessUnits.Length < 1 || businessUnits.Length > 2)
            {
                Console.MarkupLineInterpolated(
                    CultureInfo.InvariantCulture,
                    $"[red]Could not parse {userRole.BusinessUnit}; expected 'businessUnit[[{userRole.BusinessUnitSeparator}parentBusinessUnit]]'[/]");
                return false;
            }

            var businessUnit = await GetBusinessUnitAsync(service, businessUnits, cancellationToken);

            if (businessUnit == null)
            {
                Console.MarkupLineInterpolated(
                    CultureInfo.InvariantCulture,
                    $"[red]Can't find BusinessUnit. Try 'businessUnit[[{userRole.BusinessUnitSeparator}parentBusinessUnit]]'[/]");
                return false;
            }

            List<Role> securityRoles;
            if (userRole.SecurityRoles.Count > 0)
            {
                // SecurityRoles
                securityRoles = await GetSecurityRolesAsync(service, businessUnit, userRole.SecurityRoles, cancellationToken);
                if (securityRoles.Count != userRole.SecurityRoles.Count)
                {
                    Console.MarkupLine("[red]Not all SecurityRoles found, please check manually[/]");
                    return false;
                }
            }
            else
            {
                securityRoles = new List<Role>(0);
            }


            if (user.BusinessUnitId!.Id != businessUnit.Id)
            {
                var update = new SystemUser(user.Id)
                {
                    BusinessUnitId = businessUnit.ToEntityReference()
                };
                result = await service.TryUpdateAsync(update, cancellationToken);
            }

            var assignedRoles = await GetAssignedRolesAsync(service, user.Id, cancellationToken);
            var missingRoles = (from role in securityRoles
                where assignedRoles.Find(r => r.Id == role.Id) == null
                select new EntityReference(Role.EntityLogicalName, role.Id) { Name = role.Name }).ToList();
            if (missingRoles.Count > 0)
            {
                Console.WriteLine(
                    $"Associate SecurityRoles:[{string.Join("|", missingRoles.Select(r => r.Name).ToList())}]");
                var (succeeded, _) = await service.TryExecuteAsync<AssociateRequest, AssociateResponse>(new AssociateRequest
                {
                    Target = new EntityReference(SystemUser.EntityLogicalName, user.Id),
                    Relationship = new Relationship(SystemUser.Relations.ManyToMany.SystemuserrolesAssociation),
                    RelatedEntities = new EntityReferenceCollection(missingRoles)
                }, cancellationToken);
                result = succeeded && result;
            }

            var spareRoles = (from role in assignedRoles
                where securityRoles.Find(r => r.Id == role.Id) == null
                select new EntityReference(Role.EntityLogicalName, role.Id) { Name = role.Name }).ToList();
            if (spareRoles.Count > 0)
            {
                Console.WriteLine(
                    $"Disassociate SecurityRoles:[{string.Join("|", spareRoles.Select(r => r.Name).ToList())}]");
                var (succeeded, _) = await service.TryExecuteAsync<DisassociateRequest, DisassociateResponse>(new DisassociateRequest
                {
                    Target = new EntityReference(SystemUser.EntityLogicalName, user.Id),
                    Relationship = new Relationship($"{SystemUserRoles.EntityLogicalName}_association"),
                    RelatedEntities = new EntityReferenceCollection(spareRoles)
                }, cancellationToken);
                result = succeeded && result;
            }
        }

        return result;
    }

    private static async Task<BusinessUnit?> GetBusinessUnitAsync(IOrganizationServiceAsync2 service, string[] businessUnits,
        CancellationToken cancellationToken)
    {
        var query = new QueryExpression(BusinessUnit.EntityLogicalName)
        {
            NoLock = true,
            ColumnSet = new ColumnSet(BusinessUnit.LogicalNames.BusinessUnitId, BusinessUnit.LogicalNames.Name,
                BusinessUnit.LogicalNames.ParentBusinessUnitId)
        };
        if (businessUnits.Length == 1)
        {
            query.Criteria.AddCondition(BusinessUnit.LogicalNames.ParentBusinessUnitId, ConditionOperator.Null);
            query.Criteria.AddCondition(BusinessUnit.LogicalNames.Name, ConditionOperator.Equal, businessUnits[0]);
        }
        else if (businessUnits.Length == 2)
        {
            query.Criteria.AddCondition(BusinessUnit.LogicalNames.Name, ConditionOperator.Equal, businessUnits[0]);
            var parentLink = query.AddLink(BusinessUnit.EntityLogicalName, BusinessUnit.LogicalNames.ParentBusinessUnitId,
                BusinessUnit.LogicalNames.BusinessUnitId, JoinOperator.Inner);
            parentLink.EntityAlias = "parent";
            parentLink.LinkCriteria.AddCondition(BusinessUnit.LogicalNames.Name, ConditionOperator.Equal, businessUnits[1]);
        }

        return (await service.RetrieveAllAsync(query, cancellationToken: cancellationToken))
            .SingleOrDefault()?.ToEntity<BusinessUnit>();
    }

    private static async Task<List<Role>> GetSecurityRolesAsync(IOrganizationServiceAsync2 service, BusinessUnit businessUnit,
        IEnumerable<string> securityRoles, CancellationToken cancellationToken)
    {
        var securityRolesQuery = new QueryExpression
        {
            EntityName = Role.EntityLogicalName,
            ColumnSet = new ColumnSet(Role.LogicalNames.Name),
            NoLock = true,
            Criteria =
            {
                Conditions =
                {
                    new ConditionExpression(Role.LogicalNames.BusinessUnitId, ConditionOperator.Equal,
                        businessUnit.Id),
                    new ConditionExpression(Role.LogicalNames.Name, ConditionOperator.In,
                        securityRoles.ToList())
                }
            }
        };

        return (await service.RetrieveAllAsync(securityRolesQuery, cancellationToken: cancellationToken))
            .Select(entity => entity.ToEntity<Role>()).ToList();
    }

    private static async Task<List<Role>> GetAssignedRolesAsync(IOrganizationServiceAsync2 service, Guid systemUserId,
        CancellationToken cancellationToken)
    {
        // Establish a SystemUser link for a query.
        var systemUserLink = new LinkEntity
        {
            LinkFromEntityName = SystemUserRoles.EntityLogicalName,
            LinkFromAttributeName = SystemUser.LogicalNames.SystemUserId,
            LinkToEntityName = SystemUser.EntityLogicalName,
            LinkToAttributeName = SystemUser.LogicalNames.SystemUserId,
            LinkCriteria =
            {
                Conditions =
                {
                    new ConditionExpression(SystemUser.LogicalNames.SystemUserId, ConditionOperator.Equal, systemUserId)
                }
            }
        };

        // Build the query.
        var linkQuery = new QueryExpression
        {
            EntityName = Role.EntityLogicalName,
            ColumnSet = new ColumnSet(Role.LogicalNames.Name),
            NoLock = true,
            LinkEntities =
            {
                new LinkEntity
                {
                    LinkFromEntityName = Role.EntityLogicalName,
                    LinkFromAttributeName = Role.LogicalNames.RoleId,
                    LinkToEntityName = SystemUserRoles.EntityLogicalName,
                    LinkToAttributeName = Role.LogicalNames.RoleId,
                    LinkEntities = { systemUserLink }
                }
            }
        };

        // Retrieve matching roles.
        return (await service.RetrieveAllAsync(linkQuery, cancellationToken: cancellationToken))
            .Select(entity => entity.ToEntity<Role>()).ToList();
    }
}
