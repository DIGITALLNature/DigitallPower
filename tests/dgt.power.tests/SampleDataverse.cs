using dgt.power.dataverse;
using Microsoft.Crm.Sdk;
using Microsoft.Xrm.Sdk;

namespace dgt.power.tests;

public class SampleDataverse
{
    internal readonly BusinessUnit Devlab = new BusinessUnit(Guid.NewGuid())
    {
        Name = "devlab",
        IsDisabled = false,
        ParentBusinessUnitId = null
    };

    internal readonly SystemUser User1 = new SystemUser(Guid.NewGuid())
    {
        DomainName = "user.one@devlab.onmicrosoft.com",
        FirstName = "User",
        LastName = "One",
        IsDisabled = false,
        Attributes =
        {
            {"fullname","User One"}
        }
    };

    internal readonly SystemUser User2 = new SystemUser(Guid.NewGuid())
    {
        DomainName = "user.two@devlab.onmicrosoft.com",
        FirstName = "User",
        LastName = "Two",
        IsDisabled = false,
        Attributes =
        {
            {"fullname","User Two"}
        }
    };

    internal readonly SystemUser User3 = new SystemUser(Guid.NewGuid())
    {
        DomainName = "user.three@devlab.onmicrosoft.com",
        FirstName = "User",
        LastName = "Three",
        IsDisabled = false,
        Attributes =
        {
            {"fullname","User Three"}
        }
    };

    internal readonly SystemUser User4 = new SystemUser(Guid.NewGuid())
    {
        DomainName = "user.four@devlab.onmicrosoft.com",
        FirstName = "User",
        LastName = "Four",
        IsDisabled = false,
        Attributes =
        {
            {"fullname","User Four"}
        }
    };

    internal readonly SystemUser User5 = new SystemUser(Guid.NewGuid())
    {
        DomainName = "user.five@devlab.onmicrosoft.com",
        FirstName = "User",
        LastName = "Five",
        IsDisabled = false,
        Attributes =
        {
            {"fullname","User Five"}
        }
    };

    internal readonly SystemUser ProcessOwner = new SystemUser(Guid.NewGuid())
    {
        DomainName = "process.owner@devlab.onmicrosoft.com",
        FirstName = "Process",
        LastName = "Owner",
        IsDisabled = false,
        Attributes =
        {
            {"fullname","Process Owner"}
        }
    };

    internal readonly SystemUser TechOwner = new SystemUser(Guid.NewGuid())
    {
        DomainName = "tech.owner@devlab.onmicrosoft.com",
        FirstName = "Tech.",
        LastName = "Owner",
        IsDisabled = false,
        Attributes =
        {
            {"fullname","Tech. Owner"}
        }
    };

    internal readonly SystemUser AssigneeOwner = new SystemUser(Guid.NewGuid())
    {
        DomainName = "assignee.owner@devlab.onmicrosoft.com",
        FirstName = "Assignee",
        LastName = "Owner",
        IsDisabled = false,
        Attributes =
        {
            {"fullname","Assignee Owner"}
        }
    };

    internal readonly SystemUser PipelineUser = new SystemUser(Guid.Parse("f4e8821a-97d2-4938-8b73-8744431e59c8"))
    {
        DomainName = "pipeline.user@devlab.onmicrosoft.com",
        FirstName = "Pipeline",
        LastName = "User",
        IsDisabled = false,
        Attributes =
        {
            {"fullname","Pipeline User"}
        }
    };

    internal readonly SystemUser System = new SystemUser(Guid.NewGuid())
    {
        DomainName = null,
        FirstName = null,
        LastName = "SYSTEM",
        IsDisabled = true,
        Attributes =
        {
            {"fullname","SYSTEM"}
        }
    };

    internal readonly SystemUser TechUser = new SystemUser(Guid.NewGuid())
    {
        DomainName = "tech.user@domain.suffix",
        FirstName = "Tech.",
        LastName = "User",
        IsDisabled = false,
        Attributes =
        {
            {"fullname","Tech. User"}
        }
    };

    internal readonly Role SystemAdministrator = new Role(Guid.Parse("7f65483f-0800-4ebc-9c63-d8b27cd75328"))
    {
        Name = "System Administrator"
    };

    internal readonly Role SalesEnterpriseAppAccess = new Role(Guid.Parse("82d6a737-2fc8-47e9-9cff-c079ecb524c8"))
    {
        Name = "Sales, Enterprise app access"
    };

    internal readonly Workflow DisabledWorkflow = new Workflow(Guid.NewGuid())
    {
        Name = "Disabled Workflow",
        UniqueName = "disabled_workflow",
        Type = new OptionSetValue(Workflow.Options.Type.Definition),
        Category = new OptionSetValue(Workflow.Options.Category.Workflow_),
        StateCode = new OptionSetValue(Workflow.Options.StateCode.Activated),
        StatusCode = new OptionSetValue(Workflow.Options.StatusCode.Activated),
        OwnerId = new EntityReference(SystemUser.EntityLogicalName, Guid.NewGuid())
    };

    internal readonly Workflow ActiveWorkflow = new Workflow(Guid.NewGuid())
    {
        Name = "Active Workflow",
        Type = new OptionSetValue(Workflow.Options.Type.Definition),
        Category = new OptionSetValue(Workflow.Options.Category.Workflow_),
        StateCode = new OptionSetValue(Workflow.Options.StateCode.Draft),
        StatusCode = new OptionSetValue(Workflow.Options.StatusCode.Draft),
        OwnerId = new EntityReference(SystemUser.EntityLogicalName, Guid.NewGuid())
    };

    internal readonly Workflow OwnerWorkflow = new Workflow(Guid.NewGuid())
    {
        Name = "Owner Workflow",
        Type = new OptionSetValue(Workflow.Options.Type.Definition),
        Category = new OptionSetValue(Workflow.Options.Category.Workflow_),
        StateCode = new OptionSetValue(Workflow.Options.StateCode.Activated),
        StatusCode = new OptionSetValue(Workflow.Options.StatusCode.Activated)
    };

    internal readonly Workflow SystemOwnedWorkflow = new Workflow(Guid.NewGuid())
    {
        Name = "System Owned Workflow",
        Type = new OptionSetValue(Workflow.Options.Type.Definition),
        Category = new OptionSetValue(Workflow.Options.Category.Workflow_),
        StateCode = new OptionSetValue(Workflow.Options.StateCode.Activated),
        StatusCode = new OptionSetValue(Workflow.Options.StatusCode.Activated)
    };

    internal readonly Workflow WhitelistedOwnedWorkflow = new Workflow(Guid.NewGuid())
    {
        Name = "Whitelisted Owned Workflow",
        Type = new OptionSetValue(Workflow.Options.Type.Definition),
        Category = new OptionSetValue(Workflow.Options.Category.Workflow_),
        StateCode = new OptionSetValue(Workflow.Options.StateCode.Activated),
        StatusCode = new OptionSetValue(Workflow.Options.StatusCode.Activated),
        OwnerId = new SystemUser(Guid.NewGuid())
        {
            DomainName = "tech.user@domain.suffix"
        }.ToEntityReference()
    };

    internal readonly Workflow AccountBusinessRule = new Workflow(Guid.NewGuid())
    {
        Name = "Account Business Rule",
        Type = new OptionSetValue(Workflow.Options.Type.Definition),
        Category = new OptionSetValue(Workflow.Options.Category.BusinessRule),
        StateCode = new OptionSetValue(Workflow.Options.StateCode.Activated),
        StatusCode = new OptionSetValue(Workflow.Options.StatusCode.Activated),
        OwnerId = new EntityReference(SystemUser.EntityLogicalName, Guid.NewGuid()),
        PrimaryEntity = "account",
    };

    internal readonly Workflow ContactBusinessRule = new Workflow(Guid.NewGuid())
    {
        Name = "Contact Business Rule",
        Type = new OptionSetValue(Workflow.Options.Type.Definition),
        Category = new OptionSetValue(Workflow.Options.Category.BusinessRule),
        StateCode = new OptionSetValue(Workflow.Options.StateCode.Draft),
        StatusCode = new OptionSetValue(Workflow.Options.StatusCode.Draft),
        OwnerId = new EntityReference(SystemUser.EntityLogicalName, Guid.NewGuid()),
        PrimaryEntity = "contact",
    };

    internal readonly Workflow SolutionModernFlow = new Workflow(Guid.NewGuid())
    {
        Name = "Solution Modern Flow",
        Type = new OptionSetValue(Workflow.Options.Type.Definition),
        Category = new OptionSetValue(Workflow.Options.Category.ModernFlow),
        StateCode = new OptionSetValue(Workflow.Options.StateCode.Draft),
        StatusCode = new OptionSetValue(Workflow.Options.StatusCode.Draft),
        OwnerId = new EntityReference(SystemUser.EntityLogicalName, Guid.NewGuid()),
    };

    internal readonly AsyncOperation EntityKeyIndexCreation = new AsyncOperation(Guid.NewGuid())
    {
        Name = "Entity Key Index Creation",
        OperationType = new OptionSetValue(AsyncOperation.Options.OperationType.EntityKeyIndexCreation),
        StatusCode = new OptionSetValue(AsyncOperation.Options.StatusCode.InProgress)
    };

    internal readonly AsyncOperation BulkDelete = new AsyncOperation(Guid.NewGuid())
    {
        Name = "Analysis Results Cleanup Job",
        OperationType = new OptionSetValue(AsyncOperation.Options.OperationType.BulkDelete),
        RecurrenceStartTime = DateTime.UtcNow.AddDays(-1),
        RecurrencePattern = "FREQ=WEEKLY",
        Data = "<string>&lt;fetch version=\"1.0\" output-format=\"xml-platform\" mapping=\"logical\" &gt;&lt;entity name=\"testentity\" &gt;&lt;attribute name=\"name\" /&gt;&lt;/entity&gt;&lt;/fetch&gt;</string>"
    };

    internal readonly AsyncOperation OldBulkDelete = new AsyncOperation(Guid.NewGuid())
    {
        Name = "Old Bulk Delete",
        OperationType = new OptionSetValue(AsyncOperation.Options.OperationType.BulkDelete),
        RecurrenceStartTime = DateTime.UtcNow.AddDays(-1),
        RecurrencePattern = "FREQ=WEEKLY",
        Data = "<string>&lt;fetch version=\"1.0\" output-format=\"xml-platform\" mapping=\"logical\" &gt;&lt;entity name=\"testentity\" &gt;&lt;attribute name=\"name\" /&gt;&lt;/entity&gt;&lt;/fetch&gt;</string>"
    };

    internal readonly TestEntity Entity1 = new TestEntity(Guid.NewGuid())
    {
        Name = "Entity 1",
        Email = "mail@entity1.com",
        Phone = "+49 815 4711",
        Webpage = "https://www.entity1.com"
    };

    internal readonly TestEntity Entity2 = new TestEntity(Guid.NewGuid())
    {
        Name = "Entity 2",
        Email = "mail@entity2.com",
        Phone = "+49 815 4711",
        Webpage = "https://www.entity2.com"
    };

    internal readonly TestEntity Entity3 = new TestEntity(Guid.NewGuid())
    {
        Name = "Entity 3",
        Email = "mail@entity3.com",
        Phone = "+49 815 4711",
        Webpage = "https://www.entity3.com"
    };

    internal readonly TestEntity Entity4 = new TestEntity(Guid.NewGuid())
    {
        Name = "Entity 4",
        Email = "mail@entity4.com",
        Phone = "+49 815 4711",
        Webpage = "https://www.entity4.com"
    };

    internal readonly DuplicateRule WrongPhoneRule = new DuplicateRule(Guid.Parse("e4e8866a-97c2-4938-8b71-8743731e59c8"))
    {
        Name = "TestEntities with the same phone number",
        BaseEntityName = "testentity",
        MatchingEntityName = "testentity",
        IsCaseSensitive = true,
        ExcludeInactiveRecords = true,
        Attributes =
        {
            {"statecode",new OptionSetValue(DuplicateRule.Options.StateCode.Active)},
            {"statuscode",new OptionSetValue(DuplicateRule.Options.StatusCode.Published)},
        }
    };

    internal readonly DuplicateRuleCondition WrongPhoneRuleCondition = new DuplicateRuleCondition(Guid.Parse("2e9af82e-2200-e911-a96e-000d3a3a75a1"))
    {
        BaseAttributeName = "phone",
        MatchingAttributeName = "email",
        IgnoreBlankValues = false,
        OperatorCode = new OptionSetValue(DuplicateRuleCondition.Options.OperatorCode.ExactMatch)
    };

    internal readonly DuplicateRule DisabledEmailRule = new DuplicateRule(Guid.Parse("4e396448-6fb4-4dab-9ace-af6aef4f0977"))
    {
        Name = "TestEntities is disable",
        BaseEntityName = "testentity",
        MatchingEntityName = "testentity",
        IsCaseSensitive = false,
        ExcludeInactiveRecords = false
    };

    internal readonly DuplicateRuleCondition DisabledEmailRuleCondition = new DuplicateRuleCondition(Guid.Parse("359af82e-2200-e911-a96e-000d3a3a75a1"))
    {
        BaseAttributeName = "email",
        MatchingAttributeName = "email",
        IgnoreBlankValues = false,
        OperatorCode = new OptionSetValue(DuplicateRuleCondition.Options.OperatorCode.ExactMatch)
    };

    internal readonly DuplicateRule OldDescriptionRule = new DuplicateRule(Guid.NewGuid())
    {
        Name = "TestEntities to delete",
        BaseEntityName = "testentity",
        MatchingEntityName = "testentity",
        IsCaseSensitive = true,
        ExcludeInactiveRecords = true,
        Attributes =
        {
            {"statecode",new OptionSetValue(DuplicateRule.Options.StateCode.Active)},
            {"statuscode",new OptionSetValue(DuplicateRule.Options.StatusCode.Published)},
        }
    };

    internal readonly SavedQuery DisabledOutlookTemplate = new SavedQuery(Guid.NewGuid())
    {
        Name = "Disabled Outlook Template",
        QueryType = SavedQueryQueryType.OutlookTemplate,
        FetchXml = "<fetch version=\"1.0\" output-format=\"xml-platform\" mapping=\"logical\" ><entity name=\"testentity\" ><attribute name=\"name\" /><order attribute=\"name\" descending=\"false\" /></entity></fetch>",
        IsDefault = true
    };

    internal readonly SavedQuery OutdatedOutlookTemplate = new SavedQuery(Guid.NewGuid())
    {
        Name = "Outdated Outlook Template",
        QueryType = SavedQueryQueryType.OutlookTemplate,
        FetchXml = "<fetch version=\"1.0\" output-format=\"xml-platform\" mapping=\"logical\" ><entity name=\"testentity\" ><attribute name=\"name\" /><order attribute=\"name\" descending=\"false\" /></entity></fetch>",
        IsDefault = false
    };

    internal readonly SdkMessageProcessingStep Step1 = new SdkMessageProcessingStep(Guid.NewGuid())
    {
        Name = "ec4u.Plugin.Step1",
        Attributes =
        {
            {"statecode",new OptionSetValue(SdkMessageProcessingStep.Options.StateCode.Enabled)},
            {"statuscode",new OptionSetValue(SdkMessageProcessingStep.Options.StatusCode.Enabled)},
        }
    };

    internal readonly SdkMessageProcessingStepSecureConfig SecureConfig = new SdkMessageProcessingStepSecureConfig(Guid.NewGuid())
    {
        SecureConfig = "secure-config"
    };

    internal readonly SdkMessageProcessingStep Step2 = new SdkMessageProcessingStep(Guid.NewGuid())
    {
        Name = "D365.extension.Plugin.Step2",
        Attributes =
        {
            {"statecode",new OptionSetValue(SdkMessageProcessingStep.Options.StateCode.Enabled)},
            {"statuscode",new OptionSetValue(SdkMessageProcessingStep.Options.StatusCode.Enabled)},
        }
    };

    internal readonly SdkMessageProcessingStep Step3 = new SdkMessageProcessingStep(Guid.NewGuid())
    {
        Name = "D365.extension.Plugin.Step3",
        Attributes =
        {
            {"statecode",new OptionSetValue(SdkMessageProcessingStep.Options.StateCode.Disabled)},
            {"statuscode",new OptionSetValue(SdkMessageProcessingStep.Options.StatusCode.Disabled)},
        }
    };

    internal readonly SdkMessageProcessingStep Step4 = new SdkMessageProcessingStep(Guid.NewGuid())
    {
        Name = "D365.extension.Plugin.Step4",
        Attributes =
        {
            {"statecode",new OptionSetValue(SdkMessageProcessingStep.Options.StateCode.Enabled)},
            {"statuscode",new OptionSetValue(SdkMessageProcessingStep.Options.StatusCode.Enabled)},
        }
    };

    internal readonly TeamTemplate AccessTeam1 = new TeamTemplate(Guid.NewGuid())
    {
        TeamTemplateName = "Access Team 1",
        Description = "Access Team 1",
        DefaultAccessRightsMask = 32,
        ObjectTypeCode = 10000
    };

    internal readonly TeamTemplate AccessTeam2 = new TeamTemplate(Guid.NewGuid())
    {
        TeamTemplateName = "Access Team 2",
        Description = "Access Team 2",
        DefaultAccessRightsMask = 4,
        ObjectTypeCode = 10000
    };

    internal readonly Queue Queue1 = new Queue(Guid.Parse("469005c9-ca23-4d53-a1ae-f909c7863f6b"))
    {
        Name = "Queue 1",
        Description = "Queue 1",
        IncomingEmailFilteringMethod = new OptionSetValue(Queue.Options.IncomingEmailFilteringMethod.AllEmailMessages),
        IncomingEmailDeliveryMethod = new OptionSetValue(Queue.Options.IncomingEmailDeliveryMethod.None),
        OutgoingEmailDeliveryMethod = new OptionSetValue(Queue.Options.OutgoingEmailDeliveryMethod.None),
        QueueViewType = new OptionSetValue(Queue.Options.QueueViewType.Public)
    };

    internal readonly Queue Queue2 = new Queue(Guid.NewGuid())
    {
        Name = "Queue 2",
        Description = "Queue 2",
        IncomingEmailFilteringMethod = new OptionSetValue(Queue.Options.IncomingEmailFilteringMethod.AllEmailMessages),
        IncomingEmailDeliveryMethod = new OptionSetValue(Queue.Options.IncomingEmailDeliveryMethod.None),
        OutgoingEmailDeliveryMethod = new OptionSetValue(Queue.Options.OutgoingEmailDeliveryMethod.None),
        QueueViewType = new OptionSetValue(Queue.Options.QueueViewType.Public)
    };

    internal readonly Queue Queue3 = new Queue(Guid.NewGuid())
    {
        Name = "Queue 3",
        Description = "Queue 3",
        IncomingEmailFilteringMethod = new OptionSetValue(Queue.Options.IncomingEmailFilteringMethod.AllEmailMessages),
        IncomingEmailDeliveryMethod = new OptionSetValue(Queue.Options.IncomingEmailDeliveryMethod.None),
        OutgoingEmailDeliveryMethod = new OptionSetValue(Queue.Options.OutgoingEmailDeliveryMethod.None),
        QueueViewType = new OptionSetValue(Queue.Options.QueueViewType.Private)
    };

    internal readonly Queue QueueSystem = new Queue(Guid.NewGuid())
    {
        Name = "<Queue System>",
        Description = "Queue System",
        IncomingEmailFilteringMethod = new OptionSetValue(Queue.Options.IncomingEmailFilteringMethod.AllEmailMessages),
        IncomingEmailDeliveryMethod = new OptionSetValue(Queue.Options.IncomingEmailDeliveryMethod.None),
        OutgoingEmailDeliveryMethod = new OptionSetValue(Queue.Options.OutgoingEmailDeliveryMethod.None),
        QueueViewType = new OptionSetValue(Queue.Options.QueueViewType.Private)
    };

    internal readonly ImportJob ImportJob = new ImportJob(Guid.Parse("e5ef2e83-12a9-4b2b-af19-0bf0e56d5da7"))
    {
        Name = "ImportJob",
        SolutionName = "xunit_solution",
        ImportContext = "ImportUpgrade",
        OperationContext = "Upgrade"
    };

    internal readonly DocumentTemplate AccountExcel = new DocumentTemplate(Guid.NewGuid())
    {
        Name = "AccountExcel",
        DocumentType = new OptionSetValue(DocumentTemplate.Options.DocumentType.MicrosoftExcel),
        //Content =  Convert.ToBase64String(File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "./DocumentTemplate/Accounts.xlsx"))),
        Description = "Internal",
        LanguageCode = 1033,
        Status = false,//inverted logic, don't ask why
        AssociatedEntityTypeCode = "account"
    };

    internal readonly DocumentTemplate ContactExcel = new DocumentTemplate(Guid.NewGuid())
    {
        Name = "ContactExcel",
        DocumentType = new OptionSetValue(DocumentTemplate.Options.DocumentType.MicrosoftExcel),
        //Content = Convert.ToBase64String(File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "./DocumentTemplate/Contacts.xlsx"))),
        Description = "Internal",
        LanguageCode = 1033,
        Status = false,//inverted logic, don't ask why
        AssociatedEntityTypeCode = "contact"
    };

    internal readonly DocumentTemplate AccountWord = new DocumentTemplate(Guid.Parse("ecf388de-1033-4c7c-93f3-0803b16c09c7"))
    {
        Name = "AccountWord",
        DocumentType = new OptionSetValue(DocumentTemplate.Options.DocumentType.MicrosoftWord),
        //Content = Convert.ToBase64String(File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "./DocumentTemplate/Account.docx"))),
        Description = "Internal",
        LanguageCode = 1033,
        Status = false,//inverted logic, don't ask why
        AssociatedEntityTypeCode = "account"
    };

    internal readonly DocumentTemplate ContactWord = new DocumentTemplate(Guid.Parse("d1004e38-1033-461c-aa0e-7043f80c49cb"))
    {
        Name = "ContactWord",
        DocumentType = new OptionSetValue(DocumentTemplate.Options.DocumentType.MicrosoftWord),
        //Content = Convert.ToBase64String(File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "./DocumentTemplate/Contact.docx"))),
        Description = "Internal",
        LanguageCode = 1033,
        Status = false,//inverted logic, don't ask why
        AssociatedEntityTypeCode = "contact"
    };

    //internal readonly AppModule AppModule1 = new AppModule(Guid.Parse("a6863290-aa72-4b04-9a43-de12acd463c2"))
    //{
    //    Name = "Demo AppModule"
    //};

    internal readonly MsdynComponentlayer Componentlayer1 = new MsdynComponentlayer(Guid.NewGuid())
    {
        MsdynName = "System Administrator",
        MsdynSolutioncomponentname = "Role",
        MsdynSolutionname = "Active",
        MsdynComponentid = "{7f65483f-0800-4ebc-9c63-d8b27cd75328}",
        MsdynOrder = 1
    };

    internal readonly MsdynComponentlayer Componentlayer2 = new MsdynComponentlayer(Guid.NewGuid())
    {
        MsdynName = "Sales, Enterprise app access",
        MsdynSolutioncomponentname = "Role",
        MsdynSolutionname = "custom_solution",
        MsdynComponentid = "{82d6a737-2fc8-47e9-9cff-c079ecb524c8}",
        MsdynOrder = 1
    };

    internal readonly MsdynComponentlayer Componentlayer3 = new MsdynComponentlayer(Guid.NewGuid())
    {
        MsdynName = "Sales, Enterprise app access",
        MsdynSolutioncomponentname = "Role",
        MsdynSolutionname = "Active",
        MsdynComponentid = "{82d6a737-2fc8-47e9-9cff-c079ecb524c8}",
        MsdynOrder = 2
    };

    internal readonly MsdynComponentlayer Componentlayer4 = new MsdynComponentlayer(Guid.NewGuid())
    {
        MsdynName = "Demo AppModule",
        MsdynSolutioncomponentname = "AppModule",
        MsdynSolutionname = "custom_solution",
        MsdynComponentid = "{a6863290-aa72-4b04-9a43-de12acd463c2}",
        MsdynOrder = 1
    };

    internal readonly Calendar Calendar1 = new Calendar(Guid.Parse("5d2fc991-a347-4e67-ad8b-6d6c517d510a"))
    {
        Name = "Test Calendar",
        Type = new OptionSetValue(Calendar.Options.Type.HolidaySchedule),
    };

    internal readonly CalendarRule CalendarRule1 = new CalendarRule(Guid.Parse("0f56b2f4-1b0f-43ab-9575-6309713820dd"))
    {
        Name = "New Year",
        StartTime = DateTime.Parse("2020-01-01T00:00:00Z"),
        Duration = 1440,
        Description = "Holiday Rule",
        Pattern = "FREQ=DAILY;INTERVAL=1;COUNT=1",
        Rank = 0,
        SubCode = 5,
        TimeCode = 2,
        TimeZoneCode = -1,
        ExtentCode = 2,
        IsSimple = false,
        EffectiveIntervalStart = DateTime.Parse("2020-01-01T00:00:00Z"),
        EffectiveIntervalEnd = DateTime.Parse("2020-01-02T00:00:00Z")

    };

    internal readonly CalendarRule CalendarRule2 = new CalendarRule(Guid.Parse("2a17af31-25c6-4c8d-adae-7ac26e3899e8"))
    {
        Name = "Easter Sunday",
        StartTime = DateTime.Parse("2020-04-12T00:00:00Z"),
        Duration = 1440,
        Description = "Holiday Rule",
        Pattern = "FREQ=DAILY;INTERVAL=1;COUNT=1",
        Rank = 0,
        SubCode = 5,
        TimeCode = 2,
        TimeZoneCode = -1,
        ExtentCode = 2,
        IsSimple = false,
        EffectiveIntervalStart = DateTime.Parse("2020-04-12T00:00:00Z"),
        EffectiveIntervalEnd = DateTime.Parse("2020-04-13T00:00:00Z")

    };

    internal readonly SLA Sla1 = new SLA(Guid.Parse("dd9dbcef-ca66-4edd-ba42-740f0c14f554"))
    {
        Name = "Sla1 Draft",
        StatusCode = new OptionSetValue(SLA.Options.StatusCode.Draft),
        StateCode = new OptionSetValue(SLA.Options.StateCode.Draft),
        BusinessHoursId = new EntityReference(Calendar.EntityLogicalName, Guid.Parse("d425590e-7e62-4d82-8186-7f7d500d2fd3")),
    };

    internal readonly SLA Sla2 = new SLA(Guid.Parse("6555bb4d-6c3a-454a-9bda-36ee77e7e0ed"))
    {
        Name = "Sla1 Draft",
        StatusCode = new OptionSetValue(SLA.Options.StatusCode.Active),
        StateCode = new OptionSetValue(SLA.Options.StateCode.Active),
        BusinessHoursId = null
    };

    internal readonly RoutingRule Rule1 = new RoutingRule(Guid.Parse("8a75523b-27eb-4385-9bf9-d6add312383b"))
    {
        Name = "Rule1 Draft",
        StatusCode = new OptionSetValue(RoutingRule.Options.StatusCode.Draft),
        StateCode = new OptionSetValue(RoutingRule.Options.StateCode.Draft),
    };

    internal readonly RoutingRuleItem Item1Rule1 = new RoutingRuleItem(Guid.Parse("da6d62aa-b358-49af-b07a-2269919a0a97"))
    {
        MsdynRouteto = new OptionSetValue(RoutingRuleItem.Options.MsdynRouteto.Queue)
    };

    internal readonly RoutingRuleItem Item2Rule1 = new RoutingRuleItem(Guid.Parse("8790f297-dc24-4bbf-bc8f-6727ad8bf23f"))
    {
        MsdynRouteto = new OptionSetValue(RoutingRuleItem.Options.MsdynRouteto.User_Team),
    };

    internal readonly RoutingRule Rule2 = new RoutingRule(Guid.Parse("3a174e77-7c80-4b78-9ba0-bc1fd8464b62"))
    {
        Name = "Rule2 Active",
        StatusCode = new OptionSetValue(RoutingRule.Options.StatusCode.Active),
        StateCode = new OptionSetValue(RoutingRule.Options.StateCode.Active),
    };

    internal readonly RoutingRuleItem Item1Rule2 = new RoutingRuleItem(Guid.Parse("52afc222-fb8b-46e6-856f-9e07618b91c7"))
    {
        MsdynRouteto = new OptionSetValue(RoutingRuleItem.Options.MsdynRouteto.User_Team)
    };

    internal readonly Team Team1 = new Team(Guid.NewGuid())
    {
        Name = "Team1",
    };

    internal readonly Solution Solution = new Solution(Guid.NewGuid())
    {
        UniqueName = "xunit-solution"
    };

    internal readonly SolutionComponent SolutionComponent1 = new SolutionComponent(Guid.NewGuid())
    {
    };

    internal readonly SolutionComponent SolutionComponent2 = new SolutionComponent(Guid.NewGuid())
    {
    };

    internal readonly SolutionComponent SolutionComponentModernFlow = new SolutionComponent(Guid.NewGuid())
    {
    };

    internal readonly Solution SolutionWorkflow = new Solution(Guid.NewGuid())
    {
        UniqueName = "Workflows",
    };

    internal readonly Publisher Publisher1 = new Publisher(Guid.NewGuid())
    {
        Attributes = { { "uniquename", "Publisher1" } }
    };

    //internal readonly EnvironmentVariableDefinition EnvironmentVariableDefinition1 = new EnvironmentVariableDefinition(Guid.NewGuid())
    //{
    //    SchemaName = "xunit-schema1"
    //};

    //internal readonly EnvironmentVariableDefinition EnvironmentVariableDefinition2 = new EnvironmentVariableDefinition(Guid.Parse("ff6a59bf-f53f-46e4-adb6-e9bf14487073"))
    //{
    //    SchemaName = "xunit-schema2"
    //};

    //internal readonly EnvironmentVariableValue EnvironmentVariableValue2 = new EnvironmentVariableValue(Guid.NewGuid())
    //{
    //    EnvironmentVariableDefinitionId = new EntityReference(EnvironmentVariableDefinition.EntityLogicalName, Guid.Parse("ff6a59bf-f53f-46e4-adb6-e9bf14487073")),
    //    Value = "xunit-value2"
    //};

    //internal readonly EnvironmentVariableDefinition EnvironmentVariableDefinition3 = new EnvironmentVariableDefinition(Guid.Parse("dcbd6907-e708-41ef-80ae-4288f11dfc18"))
    //{
    //    SchemaName = "xunit-schema3"
    //};

    //internal readonly EnvironmentVariableValue EnvironmentVariableValue3 = new EnvironmentVariableValue(Guid.NewGuid())
    //{
    //    EnvironmentVariableDefinitionId = new EntityReference(EnvironmentVariableDefinition.EntityLogicalName, Guid.Parse("dcbd6907-e708-41ef-80ae-4288f11dfc18")),
    //    Value = "xunit-value3"
    //};

    public SampleDataverse()
    {
        ImportJob.Attributes[ImportJob.LogicalNames.StartedOn] = DateTime.UtcNow.AddSeconds(-100 * new Random().NextDouble());
        User1.BusinessUnitId = Devlab.ToEntityReference();
        User2.BusinessUnitId = Devlab.ToEntityReference();
        User3.BusinessUnitId = Devlab.ToEntityReference();
        User4.BusinessUnitId = Devlab.ToEntityReference();
        User5.BusinessUnitId = Devlab.ToEntityReference();
        ProcessOwner.BusinessUnitId = Devlab.ToEntityReference();
        SystemAdministrator.BusinessUnitId = Devlab.ToEntityReference();
        SalesEnterpriseAppAccess.BusinessUnitId = Devlab.ToEntityReference();
        OwnerWorkflow.OwnerId = ProcessOwner.ToEntityReference();
        SystemOwnedWorkflow.OwnerId = System.ToEntityReference();
        WhitelistedOwnedWorkflow.OwnerId = TechUser.ToEntityReference();
        WrongPhoneRuleCondition.RegardingObjectId = WrongPhoneRule.ToEntityReference();
        DisabledEmailRuleCondition.RegardingObjectId = DisabledEmailRule.ToEntityReference();
        Step1.SdkMessageProcessingStepSecureConfigId = SecureConfig.ToEntityReference();
        BulkDelete.OwnerId = User1.ToEntityReference();
        OldBulkDelete.OwnerId = PipelineUser.ToEntityReference();
        CalendarRule1.CalendarId = Calendar1.ToEntityReference();
        CalendarRule2.CalendarId = Calendar1.ToEntityReference();
        Sla1.OwnerId = User1.ToEntityReference();
        Item1Rule1.RoutingRuleId = Rule1.ToEntityReference();
        Item1Rule1.RoutedQueueId = Queue1.ToEntityReference();
        Item2Rule1.RoutingRuleId = Rule1.ToEntityReference();
        Item2Rule1.AssignObjectId = User3.ToEntityReference();
        Item1Rule2.RoutingRuleId = Rule2.ToEntityReference();
        var team1Eref = Team1.ToEntityReference();
        team1Eref.Name = Team1.Name;
        Item1Rule2.AssignObjectId = team1Eref;

        SolutionComponent1.Attributes[SolutionComponent.LogicalNames.ComponentType] = new OptionSetValue(SolutionComponent.Options.ComponentType.Entity);
        SolutionComponent1.Attributes[SolutionComponent.LogicalNames.SolutionId] = Solution.ToEntityReference();
        SolutionComponent2.Attributes[SolutionComponent.LogicalNames.ComponentType] = new OptionSetValue(SolutionComponent.Options.ComponentType.PluginAssembly);
        SolutionComponent2.Attributes[SolutionComponent.LogicalNames.SolutionId] = Solution.ToEntityReference();

        SolutionComponentModernFlow.Attributes[SolutionComponent.LogicalNames.ObjectId] = SolutionModernFlow.Id;
        SolutionComponentModernFlow.Attributes[SolutionComponent.LogicalNames.SolutionId] = SolutionWorkflow.ToEntityReference();

        SolutionWorkflow.Attributes[Solution.LogicalNames.PublisherId] = Publisher1.ToEntityReference();
    }
}
