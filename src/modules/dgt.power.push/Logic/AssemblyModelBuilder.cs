using System.Activities;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using dgt.power.common.Extensions;
using dgt.power.dataverse;
using dgt.power.push.Model;
using dgt.registration;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using NuGet.Packaging;
using Spectre.Console;
using Assembly = dgt.power.push.Model.Assembly;
using PluginType = dgt.power.dataverse.PluginType;

namespace dgt.power.push.Logic;

internal class AssemblyModelBuilder
{
    private readonly DataContext _context;

    private readonly string[] _knownNamespaces = { "D365.Extension.Registration", "DGT.Registrations", "dgt.registration", "Digitall.APower.Registration" };

    private readonly string[] _knownPluginAttributes =
    {
        nameof(PluginRegistrationAttribute), nameof(CustomApiRegistrationAttribute),
        nameof(CustomDataProviderRegistrationAttribute)
    };

    private readonly IOrganizationService _service;

    public AssemblyModelBuilder(IOrganizationService service)
    {
        _service = service;
        _context = new DataContext(_service) { MergeOption = MergeOption.NoTracking };
    }

    public Package BuildPackageFromFile(string packageFile)
    {
        var result = default(Package);
        try
        {
            var content = Convert.ToBase64String(File.ReadAllBytes(packageFile));
            using var inputStream = new FileStream(packageFile, FileMode.Open);
            using var reader = new PackageArchiveReader(inputStream);
            var nuspec = reader.NuspecReader;

            result = new Package
            {
                Name = nuspec.GetId(),
                Version = nuspec.GetVersion().OriginalVersion,
                Content = content
            };
        }
        catch (Exception e)
        {
            AnsiConsole.MarkupLine(Markup.Escape(e.RootMessage()));
        }

        return result;
    }

    public Package BuildPackageFromCrm(string name, string version)
    {
        var result = new Package();
        var state = GetPluginPackage(name, version, out var pluginPackage);
        result.State = state;
        if (state != AssemblyState.Create)
        {
            result.Name = pluginPackage!.Name!;
            result.Version = pluginPackage.Version!;
            result.Content = pluginPackage.Content!;
            result.Id = pluginPackage.PluginPackageId!.Value;

            result.Solutions = GetSolutions(pluginPackage.ToEntityReference());
        }

        return result;
    }


    public List<Model.Assembly?> BuildAssemblyFromPackage(Package packageFile)
    {
        using var inputStream = new MemoryStream(Convert.FromBase64String(packageFile.Content!));
        using var reader = new PackageArchiveReader(inputStream);
        var results = new List<Model.Assembly?>();

        var tempPath = Path.Combine(Path.GetTempPath(), "dgtp.push", packageFile.Id.ToString("N"));
        try
        {
            Directory.CreateDirectory(tempPath);

            var files = reader.GetFiles().Where(f => !f.Contains("/System.") && !f.Contains("/Microsoft")  && f.EndsWith(".dll")).ToList();

            foreach (var file in files)
            {
                var filestream = reader.GetStream(file);
                using var tempFile = new FileStream(Path.Combine(tempPath,Path.GetFileName(file)), FileMode.Create, FileAccess.Write);
                filestream.CopyTo(tempFile);
            }


            var env = new List<string>(Directory.GetFiles(RuntimeEnvironment.GetRuntimeDirectory(),"*.dll").Concat(Directory.GetFiles(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location!)!,"*.dll").Concat(Directory.GetFiles(tempPath, "*.dll"))));
            var loadContext = new MetadataLoadContext(new PathAssemblyResolver(env));

            foreach (var nugetDlls in Directory.GetFiles(tempPath, "*.dll"))
            {
                results.Add(BuildAssemblyFromDll(nugetDlls,loadContext));
            }
        }
        finally
        {
            Directory.Delete(tempPath,true);
        }

        return results;
    }

    public Assembly? BuildAssemblyFromDll(string dllFile, MetadataLoadContext? metadataLoadContext)
    {
        var result = default(Model.Assembly);
        try
        {
            System.Reflection.Assembly assembly = metadataLoadContext.LoadFromAssemblyPath(dllFile);

            if (assembly.IsFullyTrusted)
            {
                result = new Model.Assembly
                {
                    Name = assembly.GetName().Name!,
                    Version = assembly.GetName().Version!.ToString(),
                    Content = Convert.ToBase64String(File.ReadAllBytes(dllFile))
                };

                ParseAssembly(assembly, ref result);
            }
        }
        catch (AssemblyException e)
        {
            AnsiConsole.MarkupLine(Markup.Escape(e.RootMessage()));
            throw;
        }
        catch (Exception e)
        {
            AnsiConsole.MarkupLine(Markup.Escape(e.RootMessage()));
        }

        return result;
    }

    public Model.Assembly BuildAssemblyFromCrm(string name, string version)
    {
        var result = new Model.Assembly();
        var state = GetPluginAssembly(name, version, out var pluginAssembly);
        result.State = state;
        if (state != AssemblyState.Create)
        {
            result.Name = pluginAssembly!.Name!;
            result.Version = pluginAssembly.Version!;
            result.Content = pluginAssembly.Content!;
            result.Id = pluginAssembly.PluginAssemblyId!.Value;

            var workflowTypes = GetWorkflowTypes(pluginAssembly.ToEntityReference());
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

            var pluginTypes = GetPluginTypes(pluginAssembly.ToEntityReference());
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
                type.PluginSteps.AddRange(GetPluginSteps(pluginType.ToEntityReference(), pluginType.Name!));
                result.PluginTypes.Add(type);
            }

            result.Solutions = GetSolutions(pluginAssembly.ToEntityReference());
        }

        return result;
    }

    #region Package

    private AssemblyState GetPluginPackage(string name, string version, out PluginPackage? pluginPackage)
    {
        pluginPackage = default;
        var packages = (from pa in _context.PluginPackageSet
            where pa.Name.EndsWith(name)
            orderby pa.Version
            select pa).ToList();
        if (packages.Count == 0)
        {
            return AssemblyState.Create;
        }


        var split1 = GetVersion(version);
        var majorMinor1 = $"{split1[0]}.{split1[1]}";

        var package = packages.Single();

        pluginPackage = package;
        var split2 = GetVersion(package.Version!);
        var majorMinor2 = $"{split2[0]}.{split2[1]}";
        if (majorMinor1.Equals(majorMinor2, StringComparison.Ordinal))
        {
            pluginPackage = package;
            return AssemblyState.Update;
        }

        return AssemblyState.Upgrade;
    }

    #endregion

    #region Assembly

    private void ParseAssembly(System.Reflection.Assembly assembly, ref Model.Assembly result)
    {
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
                FriendlyName =
                    Guid.NewGuid()
                        .ToString("D"), //fixes: Message: Cannot insert duplicate key exception when executing non-query: System.Data.SqlClient.SqlCommand Exception: System.Data.SqlClient.SqlException (0x80131904): Cannot insert duplicate key row in object 'dbo.PluginTypeBase' with unique index 'UQ1_PluginType'. The statement has been terminated.
                WorkflowActivityGroupName = (customAttribute?.Group ?? $"{result.Name} ({result.Version})") +
                                            (customAttribute != null && customAttribute.IncludeVersion
                                                ? $" ({result.Version})"
                                                : null),
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
                FriendlyName =
                    Guid.NewGuid()
                        .ToString("D"), //fixes: Message: Cannot insert duplicate key exception when executing non-query: System.Data.SqlClient.SqlCommand Exception: System.Data.SqlClient.SqlException (0x80131904): Cannot insert duplicate key row in object 'dbo.PluginTypeBase' with unique index 'UQ1_PluginType'. The statement has been terminated.
                ParentName = result.Name
            };

            if (IsPowerPlugin(pluginType))
            {
                result.Type |= AssemblyType.PowerPlugin;

                var customAttributes = CustomAttributeData.GetCustomAttributes(pluginType); // TODO
                foreach (var customAttribute in customAttributes)
                {
                    if (!_knownNamespaces.Contains(customAttribute.AttributeType.Namespace))
                    {
                        continue;
                    }

                    if (customAttribute.AttributeType.Name == nameof(CustomApiRegistrationAttribute))
                    {
                        type.CustomApi = GetValue<string>(customAttribute, "MessageName")!;
                    }
                }

                type.PluginSteps.AddRange(GetPluginSteps(pluginType));
            }

            result.PluginTypes.Add(type);
        }
    }

    #endregion

    #region Dataverse

    #region Dataverse - Plugins

    private AssemblyState GetPluginAssembly(string name, string version, out PluginAssembly? pluginAssembly)
    {
        pluginAssembly = default;
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var assemblies = (from pa in _context.PluginAssemblySet
            where pa.SourceType != null
            where pa.SourceType.Value == PluginAssembly.Options.SourceType.Database ||
                  pa.SourceType.Value == PluginAssembly.Options.SourceType.FileStore
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

        pluginAssembly = assemblies.Single();

        if (pluginAssembly.PackageId != null)
        {
            return AssemblyState.Package;
        }

        var split1 = GetVersion(version);
        var majorMinor1 = $"{split1[0]}.{split1[1]}";
        var split2 = GetVersion(pluginAssembly.Version!);
        var majorMinor2 = $"{split2[0]}.{split2[1]}";
        if (majorMinor1.Equals(majorMinor2, StringComparison.Ordinal))
        {
            return AssemblyState.Update;
        }

        return AssemblyState.Upgrade;
    }

    private List<PluginType> GetPluginTypes(EntityReference pluginAssembly)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        return (from pt in _context.PluginTypeSet
            where pt.IsWorkflowActivity == false
            where pt.PluginAssemblyId.Equals(pluginAssembly)
            select pt).ToList();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
    }

    private List<PluginType> GetWorkflowTypes(EntityReference pluginAssembly)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        return (from pt in _context.PluginTypeSet
            where pt.IsWorkflowActivity == true
            where pt.PluginAssemblyId.Equals(pluginAssembly)
            select pt).ToList();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
    }

    private IEnumerable<PluginStep> GetPluginSteps(EntityReference pluginType, string pluginTypeName)
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
                Conditions =
                {
                    new ConditionExpression(SdkMessageProcessingStep.LogicalNames.PluginTypeId, ConditionOperator.Equal,
                        pluginType.Id)
                }
            }
        };
        pagequery.Orders.Add(new OrderExpression
        {
            AttributeName = SdkMessageProcessingStep.LogicalNames.Name,
            OrderType = OrderType.Ascending
        });
        var filterLink = pagequery.AddLink(SdkMessageFilter.EntityLogicalName,
            SdkMessageProcessingStep.LogicalNames.SdkMessageFilterId, SdkMessageFilter.LogicalNames.SdkMessageFilterId,
            JoinOperator.LeftOuter);
        filterLink.Columns.AddColumns(
            SdkMessageFilter.LogicalNames.PrimaryObjectTypeCode,
            SdkMessageFilter.LogicalNames.SecondaryObjectTypeCode,
            SdkMessageFilter.LogicalNames.SdkMessageFilterId
        );
        filterLink.EntityAlias = "flt";
        var messageLink = pagequery.AddLink(SdkMessage.EntityLogicalName,
            SdkMessageProcessingStep.LogicalNames.SdkMessageId, SdkMessage.LogicalNames.SdkMessageId,
            JoinOperator.Inner);
        messageLink.Columns.AddColumns(
            SdkMessage.LogicalNames.Name,
            SdkMessage.LogicalNames.SdkMessageId
        );
        messageLink.EntityAlias = "msg";
        pagequery.PageInfo = new PagingInfo
        {
            Count = 5000, //no further paging
            PageNumber = 1,
            PagingCookie = null
        };
        var results = _service.RetrieveMultiple(pagequery).Entities.Select(e => e.ToEntity<SdkMessageProcessingStep>());
        foreach (var mps in results)
        {
            var step = new PluginStep
            {
                Name = mps.Name!,
                Mode = mps.Mode!.Value,
                MessageName = GetAttribute<string>(mps, $"msg.{SdkMessage.LogicalNames.Name}")!,
                Stage = mps.Stage!.Value,
                PrimaryEntityName =
                    GetAttribute<string>(mps, $"flt.{SdkMessageFilter.LogicalNames.PrimaryObjectTypeCode}")!,
                SecondaryEntityName =
                    GetAttribute<string>(mps, $"flt.{SdkMessageFilter.LogicalNames.SecondaryObjectTypeCode}")!,
                FilterAttributes =
                    mps.FilteringAttributesField?.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries),
                ExecutionOrder = mps.Rank.GetValueOrDefault(100),
                MessageId = GetAttribute<Guid?>(mps, $"msg.{SdkMessage.LogicalNames.SdkMessageId}").GetValueOrDefault(),
                MessageFilterId = GetAttribute<Guid?>(mps, $"msg.{SdkMessageFilter.LogicalNames.SdkMessageFilterId}")
                    .GetValueOrDefault(),
                Id = mps.SdkMessageProcessingStepId.GetValueOrDefault(),
                ParentId = pluginType.Id,
                ParentName = pluginTypeName,
                Configuration = mps.Configuration
            };
            step.PluginStepImages.AddRange(GetPluginStepImages(mps.ToEntityReference(), mps.Name!));
            steps.Add(step);
        }

        return steps;
    }

    private IEnumerable<PluginStepImage> GetPluginStepImages(EntityReference pluginStep, string pluginStepName)
    {
        var images = new List<PluginStepImage>();

#pragma warning disable CS8602 // Dereference of a possibly null reference.
        (from mpsi in _context.SdkMessageProcessingStepImageSet
            where mpsi.SdkMessageProcessingStepId.Equals(pluginStep)
            select mpsi).ToList().ForEach(e =>
        {
            if ((e.ImageType!.Value == SdkMessageProcessingStepImage.Options.ImageType.PreImage &&
                 "PreImage".Equals(e.Name, StringComparison.Ordinal) &&
                 "PreImage".Equals(e.EntityAlias, StringComparison.Ordinal)) ||
                (e.ImageType.Value == SdkMessageProcessingStepImage.Options.ImageType.PostImage &&
                 "PostImage".Equals(e.Name, StringComparison.Ordinal) &&
                 "PostImage".Equals(e.EntityAlias, StringComparison.Ordinal)))
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
                AnsiConsole.MarkupLine(CultureInfo.InvariantCulture,
                    "    Delete PluginStepImage [bold green]{0} ({1})[/]", e.Name!, e.EntityAlias!);
                _service.Delete(SdkMessageProcessingStepImage.EntityLogicalName, e.Id);
            }
        });
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        return images;
    }

    #endregion

    private List<string> GetSolutions(EntityReference pluginAssembly)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var query = from sc in _context.SolutionComponentSet
            join s in _context.SolutionSet on sc.SolutionId.Id equals s.Id
            where sc.ObjectId == pluginAssembly.Id
            select s.UniqueName;
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        return query.ToList();
    }

    #endregion

    #region dgt.registration

    private IEnumerable<PluginStep> GetPluginSteps(Type pluginType)
    {
        var steps = new List<PluginStep>();

        var customAttributes = CustomAttributeData.GetCustomAttributes(pluginType);
        foreach (var customAttribute in customAttributes)
        {
            if (!_knownNamespaces.Contains(customAttribute.AttributeType.Namespace))
            {
                continue;
            }

            if (customAttribute.AttributeType.Name != nameof(PluginRegistrationAttribute))
            {
                continue;
            }

            var messageName = GetValue<string>(customAttribute, "messageName");
            var primaryEntityName = GetValue<string>(customAttribute, "PrimaryEntityName") ?? "none";
            var secondaryEntityName = GetValue<string>(customAttribute, "SecondaryEntityName") ?? "none";
            var step = new PluginStep
            {
                Mode = GetValue<int>(customAttribute, "mode"),
                MessageName = messageName!,
                Stage = GetValue<int>(customAttribute, "stage"),
                PrimaryEntityName = primaryEntityName,
                SecondaryEntityName = secondaryEntityName,
                FilterAttributes = GetArrayValues(customAttribute, "FilterAttributes"),
                ExecutionOrder = GetValue<int>(customAttribute, "ExecutionOrder"),
                ParentName = pluginType.FullName!,
                Configuration = GetValue<string>(customAttribute, "Configuration")
            };
            step.Name = GetStepName(step);

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var sdk = (from mf in _context.SdkMessageFilterSet
                join m in _context.SdkMessageSet on mf.SdkMessageId.Id equals m.Id
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


            if (GetValue<bool>(customAttribute, "PreEntityImage"))
            {
                step.PluginStepImages.Add(new PluginStepImage
                {
                    Name = "PreImage",
                    EntityAlias = "PreImage",
                    ImageType = SdkMessageProcessingStepImage.Options.ImageType.PreImage,
                    MessagePropertyName = GetMessagePropertyName(step.MessageName),
                    Attributes = GetArrayValues(customAttribute, "PreEntityImageAttributes")
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
                    Attributes = GetArrayValues(customAttribute, "PostEntityImageAttributes")
                });
            }

            steps.Add(step);
        }

        return steps;
    }


    private static WorkflowRegistrationAttribute? GetWorkflowRegistration(Type workflowType)
    {
        var customAttributes = CustomAttributeData.GetCustomAttributes(workflowType);
        var registration = customAttributes.FirstOrDefault(ca =>
            ca.AttributeType.FullName != null
            && ca.AttributeType.FullName!.Equals(typeof(WorkflowRegistrationAttribute).FullName, StringComparison.Ordinal));

        if (registration == null)
        {
            return null;
        }

        var mappedRegistration = new WorkflowRegistrationAttribute(
            GetValue<string>(registration, "Name")!,
            GetValue<string>(registration, "Group")!,
            GetValue<bool>(registration, "IncludeVersion")
        );

        return mappedRegistration;
    }

    private static string GetStepName(PluginStep step) =>
        $"{step.ParentName}|{(!string.IsNullOrEmpty(step.PrimaryEntityName) ? step.PrimaryEntityName : "entity")}|{Mode(step.Mode)}|{Stage(step.Stage)}|{step.MessageName}|{step.ExecutionOrder}";


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

    private static string? Mode(int mode) =>
        mode switch
        {
            0 => "Synchronous",
            1 => "Asynchronous",
            _ => null
        };

    private static string? Stage(int stage) =>
        stage switch
        {
            10 => "PreValidation",
            20 => "PreOperation",
            30 => "MainOperation",
            40 => "PostOperation",
            _ => null
        };

    #endregion

    #region Filter

    private static bool IsIPluginBased(Type declaredType) =>
        declaredType.GetInterface(typeof(IPlugin).FullName!) != null && !declaredType.IsAbstract;

    private static bool IsCodeActivityBased(Type declaredType) =>
        declaredType.GetBaseTypes().Any(t => t.FullName == typeof(CodeActivity).FullName) && !declaredType.IsAbstract;

    private bool IsPowerPlugin(Type declaredType)
    {
        var customAttributes = CustomAttributeData.GetCustomAttributes(declaredType);
        foreach (var customAttribute in customAttributes)
        {
            if (_knownNamespaces.Contains(customAttribute.AttributeType.Namespace))
            {
                if (_knownPluginAttributes.Contains(customAttribute.AttributeType.Name))
                {
                    return true;
                }
            }
        }

        return false;
    }

    #endregion

    #region Support

    private static string[] GetArrayValues(CustomAttributeData customAttribute, string property)
    {
        if (customAttribute.NamedArguments.Any(a => a.MemberName == property))
        {
            var propertyInfo = customAttribute.NamedArguments.Single(a => a.MemberName == property);
            var valuesRaw = (IReadOnlyCollection<CustomAttributeTypedArgument>)propertyInfo.TypedValue.Value;
            var result = valuesRaw.Select(x => x.Value as string).ToArray();

            return result;
        }

        return default;
    }

    private static T? GetValue<T>(CustomAttributeData customAttribute, string property)
    {
        if (typeof(T).IsArray)
        {
            throw new ArgumentOutOfRangeException(nameof(property), "can not be a Array");
        }

        if (customAttribute.NamedArguments.Any(a => a.MemberName == property))
        {
            var propertyInfo = customAttribute.NamedArguments.Single(a => a.MemberName == property);
            if (propertyInfo.TypedValue.Value is T value){
                return value;
            }
            AnsiConsole.MarkupLine(CultureInfo.InvariantCulture, $"Attribute Type mismatch [bold red]{Markup.Escape(property)} ({Markup.Escape(typeof(T).Name)}->{Markup.Escape(propertyInfo.TypedValue.ArgumentType.Name)})[/]");
        }

        var ctorPosition = customAttribute.Constructor.GetParameters().SingleOrDefault(c => c.Name == property)?.Position;
        if (ctorPosition.HasValue)
        {
            var propertyInfo = customAttribute.ConstructorArguments[ctorPosition.Value];
            if (propertyInfo.Value is T value){
                return value;
            }
            AnsiConsole.MarkupLine(CultureInfo.InvariantCulture, $"Attribute Type mismatch [bold red]{Markup.Escape(property)} ({Markup.Escape(typeof(T).Name)}->{Markup.Escape(propertyInfo.ArgumentType.Name)})[/]");
        }

        return default;
    }

    private static T? GetAttribute<T>(Entity entity, string attribute) => entity.Attributes.ContainsKey(attribute)
        ? (T)entity.GetAttributeValue<AliasedValue>(attribute).Value
        : default;


    private static byte[] ReadFully(Stream input)
    {
        var buffer = new byte[16 * 1024];
        using var ms = new MemoryStream();
        int read;
        while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
        {
            ms.Write(buffer, 0, read);
        }

        return ms.ToArray();
    }

    private static IEnumerable<Type> GetLoadableTypes(System.Reflection.Assembly assembly)
    {
        if (assembly == null)
        {
            throw new ArgumentNullException(nameof(assembly));
        }

        try
        {
            return assembly.GetTypes();
        }
        catch (ReflectionTypeLoadException e)
        {
            return e.Types.Where(t => t != null)!;
        }
    }


    private static string[] GetVersion(string version)
    {
        var result = version.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
        if (result.Length is < 3 or > 4) //major.minor.build.patch
        {
            throw new ArgumentException($"Version {version} does not match 'major.minor.build[.patch]'");
        }

        return result;
    }

    #endregion
}
