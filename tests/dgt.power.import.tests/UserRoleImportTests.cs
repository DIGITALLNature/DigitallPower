// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.dataverse;
using dgt.power.dto;
using dgt.power.import.Base;
using dgt.power.import.Logic;
using dgt.power.import.tests.Base;
using dgt.power.tests;
using FakeXrmEasy.Abstractions;
using FluentAssertions;
using Xunit.Abstractions;
#pragma warning disable CS8601
#pragma warning disable CS8602 // Dereference of a possibly null reference.

namespace dgt.power.import.tests;

public class UserRoleImportTests : ImportTestBase<UserRoleImport>
{
    public UserRoleImportTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    protected override CommandTestContextBuilder<UserRoleImport, ImportVerb> GetBuilder() =>
        base.GetBuilder()
            .WithRelationship(SystemUser.Relations.ManyToMany.SystemuserrolesAssociation, new XrmFakedRelationship
            {
                Entity1LogicalName = SystemUser.EntityLogicalName,
                Entity2LogicalName = Role.EntityLogicalName,
                Entity1Attribute = SystemUser.LogicalNames.SystemUserId,
                Entity2Attribute = Role.LogicalNames.RoleId,
                IntersectEntity = SystemUserRoles.EntityLogicalName,
                RelationshipType = XrmFakedRelationship.FakeRelationshipType.ManyToMany
            });
    
    [Fact]
    public void ShouldFailOnWrongConfiguration() =>
        GetContext().Execute(new ImportVerb
            {
                FileName = string.Empty,
                FileDir = ArtifactDirectory
            }
        ).Should().BeFalse();

    [Fact]
    public void ShouldFailOnEmptyConfiguration() =>
        GetContext().Execute(new ImportVerb
        {
            FileName = WriteConfigurationArtifact(new UserRoles()).Name,
            FileDir = ArtifactDirectory
        }).Should().BeFalse();

    [Fact]
    public void ShouldFailOnNonParsableBusinessUnit()
    {
        var data = GetData();
        var adminUser = data.adminUser;

        var context = GetBuilder()
            .WithData(adminUser)
            .Build();

        context.Execute(new ImportVerb
        {
            FileName = WriteConfigurationArtifact(new UserRoles { new() { BusinessUnit = "very much separators/s/s/", SecurityRoles = new[] { data.adminRole.Name }!, UserName = adminUser.DomainName } }).Name,
            FileDir = ArtifactDirectory
        }).Should().BeFalse();

        context.Get<BusinessUnit>().Should().BeEmpty();
    }


    [Fact]
    public void ShouldFailOnMissingBusinessUnit()
    {
        var data = GetData();
        var adminRole = data.adminRole;
        var adminUser = data.adminUser;

        var context = GetBuilder()
            .WithData(adminUser)
            .Build();

        context.Execute(new ImportVerb
        {
            FileName = WriteConfigurationArtifact(new UserRoles { new() { BusinessUnit = "Some Unit", SecurityRoles = new[] { adminRole.Name }!, UserName = adminUser.DomainName } }).Name,
            FileDir = ArtifactDirectory
        }).Should().BeFalse();

        context.Get<BusinessUnit>().Should().BeEmpty();
    }

    [Fact]
    public void ShouldSkipUserIfNotFound()
    {
        var context = GetBuilder()
            .Build();

        context.Execute(new ImportVerb
        {
            FileName = WriteConfigurationArtifact(new UserRoles { new() { BusinessUnit = "Some Unit", SecurityRoles = new[] { "some role" }, UserName = "some.user@test.de" } }).Name,
            FileDir = ArtifactDirectory
        }).Should().BeTrue();

        context.Get<SystemUser>().Should().BeEmpty();
        context.Get<SystemUserRoles>().Should().BeEmpty();
    }


    [Fact]
    public void ShouldAddUserRoleForNonUniqueBusinessUnit()
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

        context.Execute(new ImportVerb
        {
            FileName = WriteConfigurationArtifact(new UserRoles { new() { BusinessUnitSeparator = '/', BusinessUnit = $"{childBusinessUnit.Name}/{businessUnit.Name}", SecurityRoles = new[] { adminRole.Name }!, UserName = adminUser.DomainName } }).Name,
            FileDir = ArtifactDirectory
        }).Should().BeTrue();

        context.Get<SystemUserRoles>().Should()
            .ContainSingle(x => x.RoleId == adminRole.Id && x.SystemUserId == adminUser.Id);
    }

    [Fact]
    public void ShouldFailOnMissingSecurityRole()
    {
        var data = GetData();
        var businessUnit = data.rootBusinessUnit;
        var adminRole = data.adminRole;
        var adminUser = data.adminUser;

        var context = GetBuilder()
            .WithData(adminUser)
            .WithData(businessUnit)
            .Build();

        context.Execute(new ImportVerb
        {
            FileName = WriteConfigurationArtifact(new UserRoles { new() { BusinessUnit = businessUnit.Name!, SecurityRoles = new[] { adminRole.Name }!, UserName = adminUser.DomainName } }).Name,
            FileDir = ArtifactDirectory
        }).Should().BeFalse();

        context.Get<Role>().Should().BeEmpty();
        context.Get<SystemUserRoles>().Should().BeEmpty();
    }

    [Fact]
    public void ShouldRemoveAdditionalNotSpecifiedUserRoles()
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

        context.Execute(new ImportVerb
        {
            FileName = WriteConfigurationArtifact(new UserRoles { new() { BusinessUnit = businessUnit.Name, UserName = adminUser.DomainName } }).Name,
            FileDir = ArtifactDirectory
        }).Should().BeTrue();

        context.Get<SystemUserRoles>().Should()
            .NotContain(x =>
                x.RoleId == additionalUserRole.RoleId && x.SystemUserId == additionalUserRole.SystemUserId);
    }

    [Fact]
    public void ShouldAddUserRoleForUniqueBusinessUnit()
    {
        var data = GetData();
        var businessUnit = data.rootBusinessUnit;
        var adminRole = data.adminRole;
        var adminUser = data.adminUser;

        var context = GetBuilder()
            .WithData(data)
            .Build();

        context.Execute(new ImportVerb
        {
            FileName = WriteConfigurationArtifact(new UserRoles { new() { BusinessUnit = businessUnit.Name, SecurityRoles = new[] { adminRole.Name }!, UserName = adminUser.DomainName } }).Name,
            FileDir = ArtifactDirectory
        }).Should().BeTrue();

        context.Get<SystemUserRoles>().Should()
            .ContainSingle(x => x.RoleId == adminRole.Id && x.SystemUserId == adminUser.Id);
    }
    
    [Fact]
    public void ShouldUpdateBusinessUnitOfUserToSpecified()
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

        context.Execute(new ImportVerb
        {
            FileName = WriteConfigurationArtifact(new UserRoles { new() { BusinessUnit = newBusinessUnit.Name, SecurityRoles = new[] { adminRole.Name }!, UserName = adminUser.DomainName } }).Name,
            FileDir = ArtifactDirectory
        }).Should().BeTrue();

        context.Get<SystemUserRoles>().Should()
            .ContainSingle(x => x.RoleId == adminRole.Id && x.SystemUserId == adminUser.Id);
        var postUser = context.GetById<SystemUser>(adminUser.Id);
        postUser.BusinessUnitId.Id.Should().Be(newBusinessUnit.Id);
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
