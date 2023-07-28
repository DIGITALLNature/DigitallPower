// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics;
using dgt.power.common;
using dgt.power.common.Extensions;
using dgt.power.dataverse;
using dgt.power.dto;
using dgt.power.import.Base;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace dgt.power.import.Logic;

public sealed class UserRoleImport : BaseImport
{
    public UserRoleImport(ITracer tracer, IOrganizationService connection, IConfigResolver configResolver) : base(tracer,
        connection, configResolver)
    {
    }

    protected override bool Invoke(ImportVerb args)
    {
        Tracer.Start(this);
        var fileName = string.IsNullOrWhiteSpace(args.FileName) ? "userrole.json" : args.FileName;

        if (!ConfigResolver.TryGetConfigFile<UserRoles>(args.FileDir, fileName, out var userRoles))
        {
            return Tracer.NotConfigured(this);
        }

        if (!userRoles.Any())
        {
            return Tracer.NotConfigured(this);
        }

        var result = true;
        foreach (var userRole in userRoles)
        {
            Tracer.Log(
                $"UserName: {userRole.UserName}; BusinessUnit:{userRole.BusinessUnit}; SecurityRoles:[{string.Join("|", userRole.SecurityRoles)}]",
                TraceEventType.Information);

            using var context = new DataContext(Connection);
            // SystemUser
            var user = (from su in context.SystemUserSet
                where su.DomainName == userRole.UserName
                select su).SingleOrDefault();
            if (user == null)
            {
                Tracer.Log($"Can't find User '{userRole.UserName}' ...", TraceEventType.Warning);
                continue;
            }

            // BusinessUnit
            var businessUnits = userRole.BusinessUnit.Split(userRole.BusinessUnitSeparator);
            if (businessUnits.Length < 1 || businessUnits.Length > 2)
            {
                Tracer.Log(
                    $"Could not parse {userRole.BusinessUnit}; expected 'businessUnit[{userRole.BusinessUnitSeparator}parentBusinessUnit]'",
                    TraceEventType.Error);
                return false;
            }

            var businessUnit = GetBusinessUnit(businessUnits, context);

            if (businessUnit == null)
            {
                Tracer.Log(
                    $"Can't find BusinessUnit. Try 'businessUnit[{userRole.BusinessUnitSeparator}parentBusinessUnit]'",
                    TraceEventType.Error);
                return false;
            }

            List<Role> securityRoles;
            if (userRole.SecurityRoles.Any())
            {
                // SecurityRoles
                securityRoles = GetSecurityRoles(businessUnit, userRole.SecurityRoles);
                if (securityRoles.Count != userRole.SecurityRoles.Count)
                {
                    Tracer.Log("Not all SecurityRoles found, please check manually", TraceEventType.Error);
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
                result = Connection.TryUpdate(update);
            }

            var assignedRoles = GetAssignedRoles(user.Id);
            var missingRoles = (from role in securityRoles
                where assignedRoles.Find(r => r.Id == role.Id) == null
                select new EntityReference(Role.EntityLogicalName, role.Id) { Name = role.Name }).ToList();
            if (missingRoles.Count > 0)
            {
                Tracer.Log(
                    $"Associate SecurityRoles:[{string.Join("|", missingRoles.Select(r => r.Name).ToList())}]",
                    TraceEventType.Information);
                result = Connection.TryAssociate(SystemUser.EntityLogicalName, user.Id,
                    new Relationship(SystemUser.Relations.ManyToMany.SystemuserrolesAssociation),
                    new EntityReferenceCollection(missingRoles)) & result;
            }

            var spareRoles = (from role in assignedRoles
                where securityRoles.Find(r => r.Id == role.Id) == null
                select new EntityReference(Role.EntityLogicalName, role.Id) { Name = role.Name }).ToList();
            if (spareRoles.Count > 0)
            {
                Tracer.Log(
                    $"Disassociate SecurityRoles:[{string.Join("|", spareRoles.Select(r => r.Name).ToList())}]",
                    TraceEventType.Information);
                result = Connection.TryDisassociate(SystemUser.EntityLogicalName, user.Id,
                    new Relationship($"{SystemUserRoles.EntityLogicalName}_association"),
                    new EntityReferenceCollection(spareRoles)) & result;
            }
        }

        return Tracer.End(this, result);
    }

    private static BusinessUnit? GetBusinessUnit(string[] businessUnits, DataContext context)
    {
        BusinessUnit? businessUnit;
        if (businessUnits.Length == 1)
        {
            businessUnit = (from bu in context.BusinessUnitSet
                where bu.ParentBusinessUnitId == null
                where bu.Name == businessUnits[0]
                select bu).SingleOrDefault();
        }
        else
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        {
            businessUnit = (from bu in context.BusinessUnitSet
                join pbu in context.BusinessUnitSet on bu.ParentBusinessUnitId.Id equals pbu.Id
                where bu.ParentBusinessUnitId != null
                where bu.Name == businessUnits[0]
                where pbu.Name == businessUnits[1]
                select bu).SingleOrDefault();
        }
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        return businessUnit;
    }

    private List<Role> GetSecurityRoles(BusinessUnit businessUnit, IEnumerable<string> securityRoles)
    {
        var securityRolesQuery = new QueryExpression
        {
            EntityName = Role.EntityLogicalName,
            ColumnSet = new ColumnSet(Role.LogicalNames.Name),
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

        var securityRolesResponse = Connection.RetrieveMultiple(securityRolesQuery);
        return securityRolesResponse.Entities.Select(s => s.ToEntity<Role>()).ToList();
    }

    private List<Role> GetAssignedRoles(Guid systemUserId)
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
        return Connection.RetrieveMultiple(linkQuery).Entities.Select(e => e.ToEntity<Role>()).ToList();
    }
}
