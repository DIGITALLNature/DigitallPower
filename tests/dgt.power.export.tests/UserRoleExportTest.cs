// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using dgt.power.dto;
using dgt.power.export.Base;
using dgt.power.export.Logic;
using dgt.power.export.tests.Base;
using dgt.power.tests;
using Microsoft.Xrm.Sdk.Metadata;

namespace dgt.power.export.tests;

public class UserRoleExportTest : ExportTestBase<UserRoleExport>
{
    protected override CommandTestContext<UserRoleExport, ExportVerb> GetContext()
    {
        var businessUnit = new BusinessUnit(Guid.NewGuid())
        {
            Name = "devlab",
            IsDisabled = false
        };
        var user1 = new SystemUser(Guid.NewGuid())
        {
            DomainName = "user.one@devlab.onmicrosoft.com",
            FirstName = "User",
            LastName = "One",
            IsDisabled = false,
            [SystemUser.LogicalNames.FullName] = "User One",
            BusinessUnitId = businessUnit.ToEntityReference()
        };

        var user2 = new SystemUser(Guid.NewGuid())
        {
            DomainName = "user.two@devlab.onmicrosoft.com",
            FirstName = "User",
            LastName = "Two",
            IsDisabled = false,
            [SystemUser.LogicalNames.FullName] = "User Two",
            BusinessUnitId = businessUnit.ToEntityReference()
        };
        var user3 = new SystemUser(Guid.NewGuid())
        {
            DomainName = "user.three@devlab.onmicrosoft.com",
            FirstName = "User",
            LastName = "Three",
            IsDisabled = false,
            [SystemUser.LogicalNames.FullName] = "User Three",
            BusinessUnitId = businessUnit.ToEntityReference()
        };
        var user4 = new SystemUser(Guid.NewGuid())
        {
            DomainName = "user.four@devlab.onmicrosoft.com",
            FirstName = "User",
            LastName = "Four",
            IsDisabled = false,
            [SystemUser.LogicalNames.FullName] = "User Four",
            BusinessUnitId = businessUnit.ToEntityReference()
        };
        var user5 = new SystemUser(Guid.NewGuid())
        {
            DomainName = "user.five@devlab.onmicrosoft.com",
            FirstName = "User",
            LastName = "Five",
            IsDisabled = false,
            [SystemUser.LogicalNames.FullName] = "User Five",
            BusinessUnitId = businessUnit.ToEntityReference()
        };
        var processOwner = new SystemUser(Guid.NewGuid())
        {
            DomainName = "process.owner@devlab.onmicrosoft.com",
            FirstName = "Process",
            LastName = "Owner",
            IsDisabled = false,
            BusinessUnitId = businessUnit.ToEntityReference()
        };
        var systemadministrator = new Role(Guid.Parse("7f65483f-0800-4ebc-9c63-d8b27cd75328"))
        {
            Name = "System Administrator",
            BusinessUnitId = businessUnit.ToEntityReference()
        };
        var salesAppAccess = new Role(Guid.Parse("82d6a737-2fc8-47e9-9cff-c079ecb524c8"))
        {
            Name = "Sales, Enterprise app access",
            BusinessUnitId = businessUnit.ToEntityReference()
        };
        var roleassociation1 = new SystemUserRoles(Guid.NewGuid())
        {
            [SystemUserRoles.LogicalNames.RoleId] = systemadministrator.Id,
            [SystemUserRoles.LogicalNames.SystemUserId] = user4.Id
        };

        var roleassociation2 = new SystemUserRoles(Guid.NewGuid())
        {
            [SystemUserRoles.LogicalNames.RoleId] = salesAppAccess.Id,
            [SystemUserRoles.LogicalNames.SystemUserId] = user1.Id
        };

        return GetBuilder()
            .WithRelationship(new ManyToManyRelationshipMetadata
                {
                    SchemaName = SystemUser.Relations.ManyToMany.SystemuserrolesAssociation,
                    IntersectEntityName = SystemUserRoles.EntityLogicalName,
                    Entity1LogicalName = SystemUser.EntityLogicalName,
                    Entity1IntersectAttribute = SystemUser.LogicalNames.SystemUserId,
                    Entity2LogicalName = Role.EntityLogicalName,
                    Entity2IntersectAttribute = Role.LogicalNames.RoleId
                }
            )
            .WithData([
                    businessUnit,
                    user1,
                    user2,
                    user3,
                    user4,
                    user5,
                    processOwner,
                    systemadministrator,
                    salesAppAccess,
                    roleassociation1,
                    roleassociation2
                ]
            )
            .Build();
    }

    [Test]
    public async Task ShouldFailOnInvalidInlineDataFilter()
    {
        const string fetchXml = @"<filter>
      <condition attribute=""wrongattribute"" operator=""eq"" value=""1"" />
    </filter>";
        await Assert.That(GetContext().Execute(new ExportVerb
            {
                FileName = GetTestFileName(),
                FileDir = ArtifactDirectory,
                InlineData = fetchXml
            }
        )).IsFalse();
    }

    [Test]
    public async Task ShouldFilterUsersWithInlineDataFilter()
    {
        const string domainName = "process.owner@devlab.onmicrosoft.com";
        const string fetchXml = $@"<filter>
      <condition attribute=""domainname"" operator=""eq"" value=""{domainName}"" />
    </filter>";
        await Assert.That(GetContext().Execute(new ExportVerb
            {
                FileName = GetTestFileName(),
                FileDir = ArtifactDirectory,
                InlineData = fetchXml
            }
        )).IsTrue();

        var userRoles = GetConfigurationTestArtifact<List<UserRole>>(GetTestFileName());
        await Assert.That(userRoles).Count().IsEqualTo(1);
        await Assert.That(userRoles.Single().UserName).IsEqualTo(domainName);
    }

    [Test]
    public async Task ShouldExportUserRolesWithDefaultConfiguration()
    {
        await Assert.That(GetContext().Execute(new ExportVerb
            {
                FileName = GetTestFileName(),
                FileDir = ArtifactDirectory
            }
        )).IsTrue();
        var userRoles = GetConfigurationTestArtifact<List<UserRole>>(GetTestFileName());
        await Assert.That(userRoles).Count().IsEqualTo(6);
    }

    [Test]
    public async Task ShouldUseDefaultOnEmptyFileName()
    {
        await Assert.That(GetContext().Execute(new ExportVerb
            {
                FileName = string.Empty,
                FileDir = ArtifactDirectory
            }
        )).IsTrue();
        var userRoles = GetConfigurationTestArtifact<List<UserRole>>("userrole.json");
        await Assert.That(userRoles).Count().IsEqualTo(6);
    }
}
