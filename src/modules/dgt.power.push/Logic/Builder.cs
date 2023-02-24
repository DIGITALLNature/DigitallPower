using System.Activities;
using System.Globalization;
using System.Reflection;
using dgt.power.common.Extensions;
using dgt.power.dataverse;
using dgt.power.push.Model;
using dgt.registration;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Spectre.Console;
using Assembly = dgt.power.push.Model.Assembly;
using PluginType = dgt.power.dataverse.PluginType;

namespace dgt.power.push.Logic;

internal static class Builder
{
    private static readonly string[] s_knownNamespaces = { "D365.Extension.Registration", "DGT.Registrations" };
    private static readonly string[] s_knownPluginAttributes = { nameof(PluginRegistrationAttribute), nameof(CustomApiRegistrationAttribute), nameof(CustomDataProviderRegistrationAttribute) };

    private static IEnumerable<Type> GetLoadableTypes(System.Reflection.Assembly assembly)
    {
        if (assembly == null) throw new ArgumentNullException(nameof(assembly));
        try
        {
            return assembly.GetTypes();
        }
        catch (ReflectionTypeLoadException e)
        {
            return e.Types.Where(t => t != null)!;
        }
    }

    public static Assembly? BuildFromDll(string dllFile, IOrganizationService service)
    {
        var result = default(Assembly);
        try
        {
            var assembly = System.Reflection.Assembly.Load(File.ReadAllBytes(dllFile));
            if (assembly.IsFullyTrusted)
            {
                result = new Assembly
                {
                    Name = assembly.GetName().Name!,
                    Version = assembly.GetName().Version!.ToString(),
                    Content = Convert.ToBase64String(File.ReadAllBytes(dllFile))
                };

                var workflowTypes = GetLoadableTypes(assembly).Where(IsCodeActivityBased).ToList();
                var pluginTypes = GetLoadableTypes(assembly).Where(IsIPluginBased).ToList();

                foreach (var workflowType in workflowTypes)
                {
                    result.Type |= AssemblyType.Workflow;
                    var customAttribute = GetWorkflowRegistration(workflowType);
                    result.WorkflowTypes.Add(new WorkflowType
                    {
                        Name = customAttribute?.Name ?? workflowType.FullName!,
                        TypeName = workflowType.FullName!,
                        FriendlyName = Guid.NewGuid().ToString("D"),//fixes: Message: Cannot insert duplicate key exception when executing non-query: System.Data.SqlClient.SqlCommand Exception: System.Data.SqlClient.SqlException (0x80131904): Cannot insert duplicate key row in object 'dbo.PluginTypeBase' with unique index 'UQ1_PluginType'. The statement has been terminated.
                        WorkflowActivityGroupName = (customAttribute?.Group ?? $"{result.Name} ({result.Version})") + (customAttribute != null && customAttribute.IncludeVersion ? $" ({result.Version})" : null),
                        ParentName = result.Name
                    });
                }

                foreach (var pluginType in pluginTypes)
                {
                    result.Type |= AssemblyType.Plugin;

                    var type = new Model.PluginType
                    {
                        Name = pluginType.FullName!,
                        TypeName = pluginType.FullName!,
                        FriendlyName = Guid.NewGuid().ToString("D"),//fixes: Message: Cannot insert duplicate key exception when executing non-query: System.Data.SqlClient.SqlCommand Exception: System.Data.SqlClient.SqlException (0x80131904): Cannot insert duplicate key row in object 'dbo.PluginTypeBase' with unique index 'UQ1_PluginType'. The statement has been terminated.
                        ParentName = result.Name
                    };

                    if (IsPowerPlugin(pluginType))
                    {
                        result.Type |= AssemblyType.PowerPlugin;

                        var customAttributes = pluginType.GetCustomAttributes(false);
                        foreach (var customAttribute in customAttributes)
                        {
                            if (!s_knownNamespaces.Contains(customAttribute.GetType().Namespace)) continue;
                            
                            if (customAttribute.GetType().Name == nameof(CustomApiRegistrationAttribute))
                            {
                                type.CustomApi = GetValue<string>(customAttribute, "MessageName")!;
                            }
                        }

                        type.PluginSteps.AddRange(GetPluginSteps(pluginType, service));
                    }

                    result.PluginTypes.Add(type);
                }
            }
        }
        catch (AssemblyException e)
        {
            AnsiConsole.MarkupLine(e.RootMessage());
            throw;
        }
        catch (Exception e)
        {
            AnsiConsole.MarkupLine(e.RootMessage());
        }
        return result;
    }

    public static Assembly BuildFromCrm(string name, string version, IOrganizationService service)
    {
        var result = new Assembly();
        var state = GetPluginAssembly(name, version, service, out var pluginAssembly);
        result.State = state;
        if (state != AssemblyState.Create)
        {
            result.Name = pluginAssembly!.Name!;
            result.Version = pluginAssembly.Version!;
            result.Content = pluginAssembly.Content!;
            result.Id = pluginAssembly.PluginAssemblyId!.Value;

            var workflowTypes = GetWorkflowTypes(pluginAssembly.ToEntityReference(), service);
            foreach (var workflowType in workflowTypes)
            {
                result.Type = AssemblyType.Workflow;
                result.WorkflowTypes.Add(new WorkflowType
                {
                    Name = workflowType.Name!,
                    TypeName = workflowType.TypeName!,
                    FriendlyName = workflowType.FriendlyName!,
                    WorkflowActivityGroupName = $"{result.Name} ({result.Version})",
                    Id = workflowType.PluginTypeId!.Value,
                    ParentId = pluginAssembly.PluginAssemblyId.Value,
                    ParentName = result.Name
                });
            }

            var pluginTypes = GetPluginTypes(pluginAssembly.ToEntityReference(), service);
            foreach (var pluginType in pluginTypes)
            {
                result.Type = AssemblyType.Plugin;
                var type = new Model.PluginType
                {
                    Name = pluginType.Name!,
                    TypeName = pluginType.TypeName!,
                    FriendlyName = pluginType.FriendlyName!,
                    Id = pluginType.PluginTypeId!.Value,
                    ParentId = pluginAssembly.PluginAssemblyId.Value,
                    ParentName = result.Name
                };
                type.PluginSteps.AddRange(GetPluginSteps(pluginType.ToEntityReference(), pluginType.Name!, service));
                result.PluginTypes.Add(type);
            }

            result.Solutions = GetSolutions(pluginAssembly.ToEntityReference(), service);
        }
        return result;
    }

    private static List<string> GetSolutions(EntityReference pluginAssembly, IOrganizationService service)
    {
        using var context = new DataContext(service);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var query = from sc in context.SolutionComponentSet
            join s in context.SolutionSet on sc.SolutionId.Id equals s.Id
            where sc.ObjectId == pluginAssembly.Id
            select s.UniqueName;
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        return query.ToList();
    }

    private static IEnumerable<PluginStepImage> GetPluginStepImages(EntityReference pluginStep, string pluginStepName, IOrganizationService service)
    {
        var images = new List<PluginStepImage>();

        using var context = new DataContext(service);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        (from mpsi in context.SdkMessageProcessingStepImageSet
            where mpsi.SdkMessageProcessingStepId.Equals(pluginStep)
            select mpsi).ToList().ForEach(e =>
        {
            if ((e.ImageType!.Value == SdkMessageProcessingStepImage.Options.ImageType.PreImage && "PreImage".Equals(e.Name, StringComparison.Ordinal) && "PreImage".Equals(e.EntityAlias, StringComparison.Ordinal)) ||
                (e.ImageType.Value == SdkMessageProcessingStepImage.Options.ImageType.PostImage && "PostImage".Equals(e.Name, StringComparison.Ordinal) && "PostImage".Equals(e.EntityAlias, StringComparison.Ordinal)))
            {
                images.Add(new PluginStepImage
                {
                    Name = e.Name,
                    EntityAlias = e.EntityAlias,
                    ImageType = e.ImageType.Value,
                    Attributes = e.AttributesField?.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries),
                    MessagePropertyName = e.MessagePropertyName!,
                    Id = e.SdkMessageProcessingStepImageId!.Value,
                    ParentId = pluginStep.Id,
                    ParentName = pluginStepName
                });
            }
            else
            {
                AnsiConsole.MarkupLine(CultureInfo.InvariantCulture,"    Delete PluginStepImage [bold green]{0} ({1})[/]", e.Name!, e.EntityAlias!);
                service.Delete(SdkMessageProcessingStepImage.EntityLogicalName, e.Id);
            }
        });
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        return images;
    }

    private static IEnumerable<PluginStep> GetPluginSteps(EntityReference pluginType, string pluginTypeName, IOrganizationService service)
    {
        var steps = new List<PluginStep>();

        var pagequery = new QueryExpression
        {
            EntityName = SdkMessageProcessingStep.EntityLogicalName,
            ColumnSet = new ColumnSet(
                SdkMessageProcessingStep.LogicalNames.SdkMessageProcessingStepId,
                SdkMessageProcessingStep.LogicalNames.Name,
                SdkMessageProcessingStep.LogicalNames.Mode,
                SdkMessageProcessingStep.LogicalNames.Stage,
                SdkMessageProcessingStep.LogicalNames.FilteringAttributes,
                SdkMessageProcessingStep.LogicalNames.Rank,
                SdkMessageProcessingStep.LogicalNames.PluginTypeId,
                SdkMessageProcessingStep.LogicalNames.Configuration),
            NoLock = true,
            Criteria = new FilterExpression(LogicalOperator.And)
            {
                Conditions = { new ConditionExpression(SdkMessageProcessingStep.LogicalNames.PluginTypeId, ConditionOperator.Equal, pluginType.Id) }
            }
        };
        pagequery.Orders.Add(new OrderExpression
        {
            AttributeName = SdkMessageProcessingStep.LogicalNames.Name,
            OrderType = OrderType.Ascending
        });
        var filterLink = pagequery.AddLink(SdkMessageFilter.EntityLogicalName, SdkMessageProcessingStep.LogicalNames.SdkMessageFilterId, SdkMessageFilter.LogicalNames.SdkMessageFilterId, JoinOperator.LeftOuter);
        filterLink.Columns.AddColumns(
            SdkMessageFilter.LogicalNames.PrimaryObjectTypeCode,
            SdkMessageFilter.LogicalNames.SecondaryObjectTypeCode,
            SdkMessageFilter.LogicalNames.SdkMessageFilterId
        );
        filterLink.EntityAlias = "flt";
        var messageLink = pagequery.AddLink(SdkMessage.EntityLogicalName, SdkMessageProcessingStep.LogicalNames.SdkMessageId, SdkMessage.LogicalNames.SdkMessageId, JoinOperator.Inner);
        messageLink.Columns.AddColumns(
            SdkMessage.LogicalNames.Name,
            SdkMessage.LogicalNames.SdkMessageId
        );
        messageLink.EntityAlias = "msg";
        pagequery.PageInfo = new PagingInfo
        {
            Count = 5000,//no further paging
            PageNumber = 1,
            PagingCookie = null
        };
        var results = service.RetrieveMultiple(pagequery).Entities.Select(e=>e.ToEntity<SdkMessageProcessingStep>());
        foreach (var mps in results)
        {
            var step = new PluginStep
            {
                Name = mps.Name!,
                Mode = mps.Mode!.Value,
                MessageName = GetAttribute<string>(mps, $"msg.{SdkMessage.LogicalNames.Name}")!,
                Stage = mps.Stage!.Value,
                PrimaryEntityName = GetAttribute<string>(mps, $"flt.{SdkMessageFilter.LogicalNames.PrimaryObjectTypeCode}")!,
                SecondaryEntityName = GetAttribute<string>(mps, $"flt.{SdkMessageFilter.LogicalNames.SecondaryObjectTypeCode}")!,
                FilterAttributes = mps.FilteringAttributesField?.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries),
                ExecutionOrder = mps.Rank.GetValueOrDefault(100),
                MessageId = GetAttribute<Guid?>(mps, $"msg.{SdkMessage.LogicalNames.SdkMessageId}").GetValueOrDefault(),
                MessageFilterId = GetAttribute<Guid?>(mps, $"msg.{SdkMessageFilter.LogicalNames.SdkMessageFilterId}").GetValueOrDefault(),
                Id = mps.SdkMessageProcessingStepId.GetValueOrDefault(),
                ParentId = pluginType.Id,
                ParentName = pluginTypeName,
                Configuration = mps.Configuration
            };
            step.PluginStepImages.AddRange(GetPluginStepImages(mps.ToEntityReference(), mps.Name!, service));
            steps.Add(step);
        }

        return steps;
    }

    private static T? GetAttribute<T>(Entity entity, string attribute)
    {
        return entity.Attributes.ContainsKey(attribute) ? (T)entity.GetAttributeValue<AliasedValue>(attribute).Value : default;
    }
    private static List<PluginType> GetWorkflowTypes(EntityReference pluginAssembly, IOrganizationService service)
    {
        using var context = new DataContext(service);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        return (from pt in context.PluginTypeSet
            where pt.IsWorkflowActivity == true
            where pt.PluginAssemblyId.Equals(pluginAssembly)
            select pt).ToList();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
    }

    private static List<PluginType> GetPluginTypes(EntityReference pluginAssembly, IOrganizationService service)
    {
        using var context = new DataContext(service);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        return (from pt in context.PluginTypeSet
            where pt.IsWorkflowActivity == false
            where pt.PluginAssemblyId.Equals(pluginAssembly)
            select pt).ToList();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
    }

    private static AssemblyState GetPluginAssembly(string name, string version, IOrganizationService service, out PluginAssembly? pluginAssembly)
    {
        using var context = new DataContext(service);
        pluginAssembly = default;
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var assemblies = (from pa in context.PluginAssemblySet
            where pa.SourceType != null
            where pa.SourceType.Value == PluginAssembly.Options.SourceType.Database
            where pa.IsolationMode != null
            where pa.IsolationMode.Value == PluginAssembly.Options.IsolationMode.Sandbox
            where pa.Name == name
            orderby pa.Version
            select pa).ToList();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        if (assemblies.Count == 0)
        {
            return AssemblyState.Create;
        }
        var split1 = GetVersion(version);
        var majorMinor1 = $"{split1[0]}.{split1[1]}";
        foreach (var assembly in assemblies)
        {
            pluginAssembly = assembly;
            var split2 = GetVersion(assembly.Version!);
            var majorMinor2 = $"{split2[0]}.{split2[1]}";
            if (majorMinor1.Equals(majorMinor2, StringComparison.Ordinal))
            {
                pluginAssembly = assembly;
                return AssemblyState.Update;
            }
        }
        return AssemblyState.Upgrade;
    }

    private static string[] GetVersion(string version)
    {
        var result = version.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
        if (result.Length != 4)//major.minor.build.patch
        {
            throw new ArgumentException($"Version {version} does not match 'major.minor.build.patch'");
        }
        return result;
    }

    private static IEnumerable<PluginStep> GetPluginSteps(Type pluginType, IOrganizationService service)
    {
        var steps = new List<PluginStep>();

        var customAttributes = pluginType.GetCustomAttributes(false);
        foreach (var customAttribute in customAttributes)
        {
            if (!s_knownNamespaces.Contains(customAttribute.GetType().Namespace)) continue;
            if (customAttribute.GetType().Name != nameof(PluginRegistrationAttribute)) continue;
            var messageName = GetValue<string>(customAttribute, "MessageName");
            var primaryEntityName = GetValue<string>(customAttribute, "PrimaryEntityName") ?? "none";
            var secondaryEntityName = GetValue<string>(customAttribute, "SecondaryEntityName") ?? "none";
            var step = new PluginStep
            {
                Mode = GetValue<int>(customAttribute, "Mode"),
                MessageName = messageName!,
                Stage = GetValue<int>(customAttribute, "Stage"),
                PrimaryEntityName = primaryEntityName,
                SecondaryEntityName = secondaryEntityName,
                FilterAttributes = GetValue<string[]>(customAttribute, "FilterAttributes"),
                ExecutionOrder = GetValue<int>(customAttribute, "ExecutionOrder"),
                ParentName = pluginType.FullName!,
                Configuration = GetValue<string>(customAttribute, "Configuration")
            };
            step.Name = GetStepName(step);
            using (var context = new DataContext(service))
            {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                var sdk = (from mf in context.SdkMessageFilterSet
                    join m in context.SdkMessageSet on mf.SdkMessageId.Id equals m.Id
                    where mf.PrimaryObjectTypeCode == primaryEntityName
                    where mf.SecondaryObjectTypeCode == secondaryEntityName
                    where m.Name == messageName
                    select new { mf, m }).SingleOrDefault();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                if (sdk?.m == null || sdk.mf == null)
                {
                    throw new AssemblyException($"Message {messageName} does not fit. Check primary and secondary entity!");
                }
                step.MessageId = sdk.m.SdkMessageId!.Value;
                step.MessageFilterId = sdk.mf.SdkMessageFilterId!.Value;
            }
            if (GetValue<bool>(customAttribute, "PreEntityImage"))
            {
                step.PluginStepImages.Add(new PluginStepImage
                {
                    Name = "PreImage",
                    EntityAlias = "PreImage",
                    ImageType = SdkMessageProcessingStepImage.Options.ImageType.PreImage,
                    MessagePropertyName = GetMessagePropertyName(step.MessageName),
                    Attributes = GetValue<string[]>(customAttribute, "PreEntityImageAttributes")
                });
            }
            if (GetValue<bool>(customAttribute, "PostEntityImage"))
            {
                step.PluginStepImages.Add(new PluginStepImage
                {
                    Name = "PostImage",
                    EntityAlias = "PostImage",
                    ImageType = SdkMessageProcessingStepImage.Options.ImageType.PostImage,
                    MessagePropertyName = GetMessagePropertyName(step.MessageName),
                    Attributes = GetValue<string[]>(customAttribute, "PostEntityImageAttributes")
                });
            }
            steps.Add(step);
        }

        return steps;
    }

    private static WorkflowRegistrationAttribute? GetWorkflowRegistration(Type workflowType)
    {
        var customAttributes = workflowType.GetCustomAttributes(false);
        var registration = customAttributes.FirstOrDefault(ca =>
            ca.GetType().FullName != null
            && ca.GetType().FullName!.Equals(typeof(WorkflowRegistrationAttribute).FullName, StringComparison.Ordinal));

        if (registration == null) return null;

        var mappedRegistration = new WorkflowRegistrationAttribute(
            GetValue<string>(registration, "Name")!,
            GetValue<string>(registration, "Group")!,
            GetValue<bool>(registration, "IncludeVersion")
        );

        return mappedRegistration;
    }

    private static string? Stage(int stage)
    {
        return stage switch
        {
            10 => "PreValidation",
            20 => "PreOperation",
            30 => "MainOperation",
            40 => "PostOperation",
            _ => null
        };
    }

    private static string? Mode(int mode)
    {
        return mode switch
        {
            0 => "Synchronous",
            1 => "Asynchronous",
            _ => null
        };
    }

    private static string GetStepName(PluginStep step)
    {
        return $"{step.ParentName}|{(!string.IsNullOrEmpty(step.PrimaryEntityName) ? step.PrimaryEntityName : "entity")}|{Mode(step.Mode)}|{Stage(step.Stage)}|{step.MessageName}|{step.ExecutionOrder}";
    }

    private static string GetMessagePropertyName(string messageName)
    {
        var messagePropertyName = "Id";
        switch (messageName.ToLowerInvariant())
        {
            case "update":
            case "delete":
                messagePropertyName = "Target";
                break;
            case "setstate":
            case "setstatedynamicentity":
                messagePropertyName = "EntityMoniker";
                break;
        }
        return messagePropertyName;
    }

    private static T? GetValue<T>(object customAttribute, string property)
    {
        var propertyInfo = customAttribute.GetType().GetProperty(property);
        if (propertyInfo != null)
        {
            return (T?)propertyInfo.GetValue(customAttribute);
        }
        return default;
    }

    private static bool IsIPluginBased(Type declaredType) => declaredType.GetInterface(typeof(IPlugin).FullName!) != null && !declaredType.IsAbstract;
    private static bool IsCodeActivityBased(Type declaredType) => declaredType.GetBaseTypes().Any(t => t.FullName == typeof(CodeActivity).FullName) && !declaredType.IsAbstract;

    private static bool IsPowerPlugin(Type declaredType)
    {
        var customAttributes = declaredType.GetCustomAttributes(false);
        foreach (var customAttribute in customAttributes)
        {
            if (s_knownNamespaces.Contains(customAttribute.GetType().Namespace))
            {
                if (s_knownPluginAttributes.Contains(customAttribute.GetType().Name))
                {
                    return true;
                }
            }
        }
        return false;
    }
}
