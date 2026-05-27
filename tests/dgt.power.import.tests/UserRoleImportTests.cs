// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using dgt.power.dto;
using dgt.power.import.Base;
using dgt.power.import.Logic;
using dgt.power.import.tests.Base;
using dgt.power.tests;
using Microsoft.Xrm.Sdk.Metadata;
#pragma warning disable CS8601
#pragma warning disable CS8602 // Dereference of a possibly null reference.

namespace dgt.power.import.tests;

public class UserRoleImportTests : ImportTestBase<UserRoleImport>
{
    protected override CommandTestContextBuilder<UserRoleImport, ImportVerb> GetBuilder() =>
        base.GetBuilder()
            .WithRelationship(new ManyToManyRelationshipMetadata
            {
                SchemaName = SystemUser.Relations.ManyToMany.SystemuserrolesAssociation,
                Entity1LogicalName = SystemUser.EntityLogicalName,
                Entity1IntersectAttribute = SystemUser.LogicalNames.SystemUserId,
                Entity2LogicalName = Role.EntityLogicalName,
                Entity2IntersectAttribute = Role.LogicalNames.RoleId,
                IntersectEntityName = SystemUserRoles.EntityLogicalName
            });

    [Test]
    public async Task ShouldFailOnWrongConfiguration() =>
        await Assert.That(GetContext().Execute(new ImportVerb
            {
                FileName = string.Empty,
                FileDir = ArtifactDirectory
            }
        )).IsFalse();

    [Test]
    public async Task ShouldFailOnEmptyConfiguration() =>
        await Assert.That(GetContext().Execute(new ImportVerb
        {
            FileName = WriteConfigurationArtifact(new UserRoles()).Name,
            FileDir = ArtifactDirectory
        })).IsFalse();

    [Test]
    public async Task ShouldFailOnNonParsableBusinessUnit()
    {
        var data = GetData();
        var adminUser = data.adminUser;

        var context = GetBuilder()
            .WithData(adminUser)
            .Build();

        await Assert.That(context.Execute(new ImportVerb
        {
            FileName = WriteConfigurationArtifact(new UserRoles { new() { BusinessUnit = "very much separators/s/s/", SecurityRoles = new[] { data.adminRole.Name }!, UserName = adminUser.DomainName } }).Name,
            FileDir = ArtifactDirectory
        })).IsFalse();

        await Assert.That(context.Get<BusinessUnit>()).IsEmpty();
    }


    [Test]
    public async Task ShouldFailOnMissingBusinessUnit()
    {
        var data = GetData();
        var adminRole = data.adminRole;
        var adminUser = data.adminUser;

        var context = GetBuilder()
            .WithData(adminUser)
            .Build();

        await Assert.That(context.Execute(new ImportVerb
        {
            FileName = WriteConfigurationArtifact(new UserRoles { new() { BusinessUnit = "Some Unit", SecurityRoles = new[] { adminRole.Name }!, UserName = adminUser.DomainName } }).Name,
            FileDir = ArtifactDirectory
        })).IsFalse();

        await Assert.That(context.Get<BusinessUnit>()).IsEmpty();
    }

    [Test]
    public async Task ShouldSkipUserIfNotFound()
    {
        var context = GetBuilder()
            .Build();

        await Assert.That(context.Execute(new ImportVerb
        {
            FileName = WriteConfigurationArtifact(new UserRoles { new() { BusinessUnit = "Some Unit", SecurityRoles = ["some role"], UserName = "some.user@test.de" } }).Name,
            FileDir = ArtifactDirectory
        })).IsTrue();

        await Assert.That(context.Get<SystemUser>()).IsEmpty();
        await Assert.That(context.Get<SystemUserRoles>()).IsEmpty();
    }


    [Test]
    public async Task ShouldAddUserRoleForNonUniqueBusinessUnit()
    {
        var data = GetData();
        var businessUnit = data.rootBusinessUnit;
        var childBusinessUnit = data.childBusinessUnit;
        var adminRole = data.adminRole;
        var adminUser = data.adminUser;
        adminRole.BusinessUnitId = childBusinessUnit.ToEntityReference();

        var context = GetBuilder()
            .WithData(data)
            .Build();

        await Assert.That(context.Execute(new ImportVerb
        {
            FileName = WriteConfigurationArtifact(new UserRoles { new() { BusinessUnitSeparator = '/', BusinessUnit = $"{childBusinessUnit.Name}/{businessUnit.Name}", SecurityRoles = new[] { adminRole.Name }!, UserName = adminUser.DomainName } }).Name,
            FileDir = ArtifactDirectory
        })).IsTrue();

        await Assert.That(context.Get<SystemUserRoles>().Count(x => x.RoleId == adminRole.Id && x.SystemUserId == adminUser.Id)).IsEqualTo(1);
    }

    [Test]
    public async Task ShouldFailOnMissingSecurityRole()
    {
        var data = GetData();
        var businessUnit = data.rootBusinessUnit;
        var adminRole = data.adminRole;
        var adminUser = data.adminUser;

        var context = GetBuilder()
            .WithData(adminUser)
            .WithData(businessUnit)
            .Build();

        await Assert.That(context.Execute(new ImportVerb
        {
            FileName = WriteConfigurationArtifact(new UserRoles { new() { BusinessUnit = businessUnit.Name!, SecurityRoles = new[] { adminRole.Name }!, UserName = adminUser.DomainName } }).Name,
            FileDir = ArtifactDirectory
        })).IsFalse();

        await Assert.That(context.Get<Role>()).IsEmpty();
        await Assert.That(context.Get<SystemUserRoles>()).IsEmpty();
    }

    [Test]
    public async Task ShouldRemoveAdditionalNotSpecifiedUserRoles()
    {
        var data = GetData();
        var businessUnit = data.rootBusinessUnit;
        var adminUser = data.adminUser;
        var salesAppAccessRole = data.salesAppAccessRole;
        var additionalUserRole = new SystemUserRoles(Guid.NewGuid())
        {
            [SystemUserRoles.LogicalNames.RoleId] = salesAppAccessRole.Id,
            [SystemUserRoles.LogicalNames.SystemUserId] = adminUser.Id
        };

        var context = GetBuilder()
            .WithData(data)
            .WithData(additionalUserRole)
            .Build();

        await Assert.That(context.Execute(new ImportVerb
        {
            FileName = WriteConfigurationArtifact(new UserRoles { new() { BusinessUnit = businessUnit.Name, UserName = adminUser.DomainName } }).Name,
            FileDir = ArtifactDirectory
        })).IsTrue();

        await Assert.That(context.Get<SystemUserRoles>().Any(x =>
                x.RoleId == additionalUserRole.RoleId && x.SystemUserId == additionalUserRole.SystemUserId)).IsFalse();
    }

    [Test]
    public async Task ShouldAddUserRoleForUniqueBusinessUnit()
    {
        var data = GetData();
        var businessUnit = data.rootBusinessUnit;
        var adminRole = data.adminRole;
        var adminUser = data.adminUser;

        var context = GetBuilder()
            .WithData(data)
            .Build();

        await Assert.That(context.Execute(new ImportVerb
        {
            FileName = WriteConfigurationArtifact(new UserRoles { new() { BusinessUnit = businessUnit.Name, SecurityRoles = new[] { adminRole.Name }!, UserName = adminUser.DomainName } }).Name,
            FileDir = ArtifactDirectory
        })).IsTrue();

        await Assert.That(context.Get<SystemUserRoles>().Count(x => x.RoleId == adminRole.Id && x.SystemUserId == adminUser.Id)).IsEqualTo(1);
    }

    [Test]
    public async Task ShouldUpdateBusinessUnitOfUserToSpecified()
    {
        var data = GetData();
        var oldBusinessUnit = data.childBusinessUnit;
        var newBusinessUnit = data.rootBusinessUnit;
        var adminRole = data.adminRole;
        var adminUser = data.adminUser;
        adminUser.BusinessUnitId = oldBusinessUnit.ToEntityReference();

        var context = GetBuilder()
            .WithData(data)
            .Build();

        await Assert.That(context.Execute(new ImportVerb
        {
            FileName = WriteConfigurationArtifact(new UserRoles { new() { BusinessUnit = newBusinessUnit.Name, SecurityRoles = new[] { adminRole.Name }!, UserName = adminUser.DomainName } }).Name,
            FileDir = ArtifactDirectory
        })).IsTrue();

        await Assert.That(context.Get<SystemUserRoles>().Count(x => x.RoleId == adminRole.Id && x.SystemUserId == adminUser.Id)).IsEqualTo(1);
        var postUser = context.GetById<SystemUser>(adminUser.Id);
        await Assert.That(postUser.BusinessUnitId.Id).IsEqualTo(newBusinessUnit.Id);
    }



    private static (BusinessUnit rootBusinessUnit, BusinessUnit childBusinessUnit, Role adminRole, Role
        salesAppAccessRole, SystemUser adminUser, SystemUser childUnitUser) GetData()
    {
        var rootBusinessUnit = new BusinessUnit(Guid.NewGuid())
        {
            Name = "Unit"
        };
        var childBusinessUnit = new BusinessUnit(Guid.NewGuid())
        {
            Name = "Child Unit",
            ParentBusinessUnitId = rootBusinessUnit.ToEntityReference()
        };
        var adminRole = new Role(Guid.NewGuid())
        {
            Name = "System Administrator",
            BusinessUnitId = rootBusinessUnit.ToEntityReference()
        };
        var salesAppAccessRole = new Role(Guid.NewGuid())
        {
            Name = "Sales, Enterprise app access",
            BusinessUnitId = rootBusinessUnit.ToEntityReference()
        };
        var adminUser = new SystemUser(Guid.NewGuid())
        {
            DomainName = "user1@test.de",
            BusinessUnitId = rootBusinessUnit.ToEntityReference()
        };
        var childUnitUser = new SystemUser(Guid.NewGuid())
        {
            DomainName = "child.bu@test.de",
            BusinessUnitId = childBusinessUnit.ToEntityReference()
        };
        return (rootBusinessUnit, childBusinessUnit, adminRole, salesAppAccessRole, adminUser, childUnitUser);
    }
}
