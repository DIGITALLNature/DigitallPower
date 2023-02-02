using dgt.power.dataverse;
using dgt.power.dto;
using dgt.power.export.Base;
using dgt.power.export.Logic;
using dgt.power.export.tests.Base;
using dgt.power.tests;
using FakeXrmEasy.Abstractions;
using FluentAssertions;
using Microsoft.Xrm.Sdk;
using Xunit.Abstractions;

namespace dgt.power.export.tests;

public class UserRoleExportTest : ExportTestBase<UserRoleExport>
{
    public UserRoleExportTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    protected override CommandTestContext<UserRoleExport, ExportVerb> GetContext()
    {
        var businessUnit = new BusinessUnit(Guid.NewGuid())
        {
            Name = "devlab",
            IsDisabled = false,
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
            [SystemUserRoles.LogicalNames.SystemUserId] = user4.Id,
        };

        var roleassociation2 = new SystemUserRoles(Guid.NewGuid())
        {
            [SystemUserRoles.LogicalNames.RoleId] = salesAppAccess.Id,
            [SystemUserRoles.LogicalNames.SystemUserId] = user1.Id,
        };

        return GetBuilder()
            .WithRelationship(SystemUser.Relations.ManyToMany.SystemuserrolesAssociation,
                new XrmFakedRelationship
                {
                    IntersectEntity = SystemUserRoles.EntityLogicalName,
                    Entity1LogicalName = SystemUser.EntityLogicalName,
                    Entity1Attribute = SystemUser.LogicalNames.SystemUserId,
                    Entity2LogicalName = Role.EntityLogicalName,
                    Entity2Attribute = Role.LogicalNames.RoleId
                }
            )
            .WithData(new Entity[]
                {
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
                }
            )
            .Build();
    }

    [Fact]
    public void ShouldFailOnInvalidInlineDataFilter()
    {
        const string fetchXml = @"<filter>
      <condition attribute=""wrongattribute"" operator=""eq"" value=""1"" />
    </filter>";
        GetContext().Execute(new ExportVerb
            {
                FileName = GetTestFileName(),
                FileDir = ArtifactDirectory,
                InlineData = fetchXml
            }
        ).Should().BeFalse();
    }

    [Fact]
    public void ShouldFilterUsersWithInlineDataFilter()
    {
        const string domainName = "process.owner@devlab.onmicrosoft.com";
        const string fetchXml = $@"<filter>
      <condition attribute=""domainname"" operator=""eq"" value=""{domainName}"" />
    </filter>";
        GetContext().Execute(new ExportVerb
            {
                FileName = GetTestFileName(),
                FileDir = ArtifactDirectory,
                InlineData = fetchXml
            }
        ).Should().BeTrue();

        var userRoles = GetConfigurationTestArtifact<List<UserRole>>(GetTestFileName());
        userRoles.Should().ContainSingle();
        userRoles.Single().UserName.Should().Be(domainName);
    }

    [Fact]
    public void ShouldExportUserRolesWithDefaultConfiguration()
    {
        GetContext().Execute(new ExportVerb
            {
                FileName = GetTestFileName(),
                FileDir = ArtifactDirectory,
            }
        ).Should().BeTrue();
        var userRoles = GetConfigurationTestArtifact<List<UserRole>>(GetTestFileName());
        userRoles.Should().HaveCount(6);
    }

    [Fact]
    public void ShouldUseDefaultOnEmptyFileName()
    {
        GetContext().Execute(new ExportVerb
            {
                FileName = string.Empty,
                FileDir = ArtifactDirectory,
            }
        ).Should().BeTrue();
        var userRoles = GetConfigurationTestArtifact<List<UserRole>>("userrole.json");
        userRoles.Should().HaveCount(6);
    }
}