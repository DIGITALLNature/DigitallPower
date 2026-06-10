// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

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

internal sealed class AssemblyModelBuilder : IDisposable
{
    private readonly DataContext _context;

    private readonly string[] _knownNamespaces = { "D365.Extension.Registration", "DGT.Registrations", "dgt.registration", "Digitall.APower.Registration", "Digitall.Plugins.Registration" };

    private readonly string[] _knownPluginAttributes =
    {
        nameof(PluginRegistrationAttribute), nameof(CustomApiRegistrationAttribute),
        nameof(CustomDataProviderRegistrationAttribute)
    };

    private readonly IOrganizationService _service;
    private readonly IAnsiConsole _console;

    public AssemblyModelBuilder(IOrganizationService service, IAnsiConsole? console = null)
    {
        _service = service;
        _console = console ?? AnsiConsole.Console;
        _context = new DataContext(_service) { MergeOption = MergeOption.NoTracking };
    }

    public Package? BuildPackageFromFile(string packageFile)
    {
        Package? result = null;
        try
        {
            var content = Convert.ToBase64String(File.ReadAllBytes(packageFile));
            using var inputStream = new FileStream(packageFile, FileMode.Open);
            using var reader = new PackageArchiveReader(inputStream);
            var nuspec = reader.NuspecReader;

            result = new Package
            {
                Name = nuspec.GetId(),
                Version = nuspec.GetVersion().OriginalVersion ?? nuspec.GetVersion().ToFullString(),
                Content = content
            };
        }
        catch (Exception e) when (e is not OutOfMemoryException and not StackOverflowException)
        {
            _console.MarkupLine(Markup.Escape(e.RootMessage()));
        }

        return result;
    }

    public Package BuildPackageFromCrm(string name, string version)
    {
        var state = GetPluginPackage(name, version, out var pluginPackage);

        if (state == AssemblyState.Create)
        {
            return new Package { Name = name, Version = version, State = state };
        }

        return new Package
        {
            Name = pluginPackage!.Name!,
            Version = pluginPackage.Version!,
            Content = pluginPackage.Content!,
            Id = pluginPackage.PluginPackageId!.Value,
            State = state,
            Solutions = GetSolutions(pluginPackage.ToEntityReference())
        };
    }


    public IReadOnlyList<Assembly?> BuildAssemblyFromPackage(Package packageFile)
    {
        using var inputStream = new MemoryStream(Convert.FromBase64String(packageFile.Content!));
        using var reader = new PackageArchiveReader(inputStream);
        var results = new List<Assembly?>();

        var tempPath = Path.Combine(Path.GetTempPath(), "dgtp.push", packageFile.Id.ToString("N"));
        try
        {
            Directory.CreateDirectory(tempPath);

            var files = reader.GetFiles()
                .Where(f => !f.Contains("/System.", StringComparison.Ordinal) &&
                            !f.Contains("/Microsoft", StringComparison.Ordinal) &&
                            f.EndsWith(".dll", StringComparison.OrdinalIgnoreCase))
                .ToList();

            foreach (var file in files)
            {
                var filestream = reader.GetStream(file);
                using var tempFile = new FileStream(Path.Combine(tempPath,Path.GetFileName(file)), FileMode.Create, FileAccess.Write);
                filestream.CopyTo(tempFile);
            }


            var env = new List<string>(Directory.GetFiles(RuntimeEnvironment.GetRuntimeDirectory(),"*.dll").Concat(Directory.GetFiles(Path.GetDirectoryName(typeof(AssemblyModelBuilder).Assembly.Location)!,"*.dll").Concat(Directory.GetFiles(tempPath, "*.dll"))));
            using var loadContext = new MetadataLoadContext(new PathAssemblyResolver(env));

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

    public Assembly? BuildAssemblyFromDll(string dllFile, MetadataLoadContext metadataLoadContext)
    {
        ArgumentNullException.ThrowIfNull(metadataLoadContext);
        var result = default(Assembly);
        try
        {
            System.Reflection.Assembly assembly = metadataLoadContext.LoadFromAssemblyPath(dllFile);

            if (assembly.IsFullyTrusted)
            {
                result = new Assembly
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
            _console.MarkupLine(Markup.Escape(e.RootMessage()));
            throw;
        }
        catch (Exception e) when (e is not OutOfMemoryException and not StackOverflowException)
        {
            _console.MarkupLine(Markup.Escape(e.RootMessage()));
        }

        return result;
    }

    public Assembly BuildAssemblyFromCrm(string name, string version)
    {
        var result = new Assembly();
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

    /// <summary>
    /// Builds the list of outdated assembly content from CRM.
    /// </summary>
    /// <param name="name">The name of the assembly.</param>
    /// <returns>The list of outdated assembly content.</returns>
    public IReadOnlyList<AssemblyContent> BuildOutdatedAssemblyContentFromCrm(string name)
    {
        // Get the list of plugin assemblies from CRM
        var assemblies = GetPluginAssemblies(name);

        // Create a list to store the outdated assembly content
        var results = new List<AssemblyContent>();

        // Iterate over the plugin assemblies except the first one which is the latest one
        foreach (var pluginAssembly in assemblies.Skip(1))
        {
            // Create a new assembly content model
            var result = new AssemblyContent();
            result.Id = pluginAssembly.Id;

            // Get the list of plugin types from CRM
            var pluginTypes = GetPluginTypes(pluginAssembly.ToEntityReference());

            // Iterate over the plugin types
            foreach (var pluginType in pluginTypes)
            {
                // Create a new plugin type model
                var type = new Model.PluginType
                {
                    Name = pluginType.Name!,
                    TypeName = pluginType.TypeName!,
                    FriendlyName = pluginType.FriendlyName!,
                    Id = pluginType.PluginTypeId!.Value,
                    ParentId = pluginAssembly.Id,
                    ParentName = pluginAssembly.Name
                };

                // Get the list of plugin steps from CRM
                type.PluginSteps.AddRange(GetPluginSteps(pluginType.ToEntityReference(), pluginType.Name!));

                // Add the plugin type to the assembly content model
                result.PluginTypes.Add(type);
            }

            // Add the assembly content model to the results list
            results.Add(result);
        }

        // Return the list of outdated assembly content
        return results;
    }

    #region Package

    private AssemblyState GetPluginPackage(string name, string version, out PluginPackage? pluginPackage)
    {
        pluginPackage = null;
        var packages = (from pa in _context.PluginPackageSet
            where pa.Name != null && pa.Name.EndsWith(name)
            orderby pa.Version
            select pa).ToList();
        if (packages.Count == 0)
        {
            return AssemblyState.Create;
        }


        var v1 = Version.Parse(version);

        var package = packages.Single();

        pluginPackage = package;
        var v2 = Version.Parse(package.Version!);
        if (v1.Major == v2.Major && v1.Minor == v2.Minor)
        {
            pluginPackage = package;
            return AssemblyState.Update;
        }

        return AssemblyState.Upgrade;
    }

    #endregion

    #region Assembly

    private void ParseAssembly(System.Reflection.Assembly assembly, ref Assembly result)
    {
        // Parse assembly-level attributes (e.g., ManagedIdentityRegistrationAttribute)
        ParseAssemblyLevelAttributes(assembly, result);

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
                WorkflowActivityGroupName = BuildWorkflowActivityGroupName(customAttribute, result),
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
                        type.CustomApi = GetValue<string>(customAttribute, "messageName")!;
                    }

                    if (customAttribute.AttributeType.Name == nameof(CustomDataProviderRegistrationAttribute))
                    {
                        type.PluginSteps.AddRange(GetDataProviderPluginSteps(pluginType, customAttribute));
                    }
                }

                type.PluginSteps.AddRange(GetRegistrationPluginSteps(pluginType));
            }

            result.PluginTypes.Add(type);
        }
    }

    /// <summary>
    /// Parses assembly-level attributes such as ManagedIdentityRegistrationAttribute.
    /// </summary>
    private void ParseAssemblyLevelAttributes(System.Reflection.Assembly assembly, Assembly result)
    {
        var assemblyAttributes = CustomAttributeData.GetCustomAttributes(assembly);
        foreach (var attr in assemblyAttributes)
        {
            if (!_knownNamespaces.Contains(attr.AttributeType.Namespace))
            {
                continue;
            }

            if (attr.AttributeType.Name == "ManagedIdentityRegistrationAttribute")
            {
                result.ManagedIdentityClientId = GetValue<string>(attr, "clientId");
                result.ManagedIdentityTenantId = GetValue<string>(attr, "TenantId");
                _console.MarkupLine(CultureInfo.InvariantCulture,
                    "[blue]ManagedIdentity[/] ClientId=[italic]{0}[/] TenantId=[italic]{1}[/]",
                    result.ManagedIdentityClientId ?? "(none)",
                    result.ManagedIdentityTenantId ?? "(environment default)");
            }
        }
    }

    private static string BuildWorkflowActivityGroupName(WorkflowRegistrationAttribute? customAttribute, Assembly result)
    {
        var workflowActivityGroupName = customAttribute?.Group ?? $"{result.Name} ({result.Version})";
        if (customAttribute?.IncludeVersion == true)
        {
            return $"{workflowActivityGroupName} ({result.Version})";
        }

        return workflowActivityGroupName;
    }

    #endregion

    #region Dataverse

    #region Dataverse - Plugins

    private AssemblyState GetPluginAssembly(string name, string version, out PluginAssembly? pluginAssembly)
    {
        pluginAssembly = null;
        var assemblies = GetPluginAssemblies(name);
        if (assemblies.Count == 0)
        {
            return AssemblyState.Create;
        }

        pluginAssembly = assemblies[0];

        if (pluginAssembly.PackageId != null)
        {
            return AssemblyState.Package;
        }

        var v1 = Version.Parse(version);
        var v2 = Version.Parse(pluginAssembly.Version!);
        if (v1.Major == v2.Major && v1.Minor == v2.Minor)
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

    private List<PluginAssembly> GetPluginAssemblies(string name)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        return (from pa in _context.PluginAssemblySet
            where pa.SourceType != null
            where pa.SourceType.Value == PluginAssembly.Options.SourceType.Database ||
                  pa.SourceType.Value == PluginAssembly.Options.SourceType.FileStore
            where pa.IsolationMode != null
            where pa.IsolationMode.Value == PluginAssembly.Options.IsolationMode.Sandbox
            where pa.Name == name
            orderby pa.Version descending
            select pa).ToList();
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

    private List<PluginStep> GetPluginSteps(EntityReference pluginType, string pluginTypeName)
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
                    mps.FilteringAttributesField?.Split(',', StringSplitOptions.RemoveEmptyEntries),
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

    private List<PluginStepImage> GetPluginStepImages(EntityReference pluginStep, string pluginStepName)
    {
        var images = new List<PluginStepImage>();

#pragma warning disable CS8602 // Dereference of a possibly null reference.
        (from mpsi in _context.SdkMessageProcessingStepImageSet
            where mpsi.SdkMessageProcessingStepId.Equals(pluginStep)
            select mpsi).ToList().ForEach(e =>
        {
            if (HasDefaultImageIdentity(e))
            {
                images.Add(new PluginStepImage
                {
                    Name = e.Name ?? string.Empty,
                    EntityAlias = e.EntityAlias ?? string.Empty,
                    ImageType = e.ImageType.Value,
                    Attributes = e.AttributesField?.Split(',', StringSplitOptions.RemoveEmptyEntries),
                    MessagePropertyName = e.MessagePropertyName!,
                    Id = e.SdkMessageProcessingStepImageId!.Value,
                    ParentId = pluginStep.Id,
                    ParentName = pluginStepName
                });
            }
            else
            {
                _console.MarkupLine(CultureInfo.InvariantCulture,
                    "    Delete PluginStepImage [bold green]{0} ({1})[/]", e.Name!, e.EntityAlias!);
                _service.Delete(SdkMessageProcessingStepImage.EntityLogicalName, e.Id);
            }
        });
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        return images;
    }

    private static bool HasDefaultImageIdentity(SdkMessageProcessingStepImage image)
    {
        if (image.ImageType?.Value == SdkMessageProcessingStepImage.Options.ImageType.PreImage)
        {
            return "PreImage".Equals(image.Name, StringComparison.Ordinal)
                   && "PreImage".Equals(image.EntityAlias, StringComparison.Ordinal);
        }

        if (image.ImageType?.Value == SdkMessageProcessingStepImage.Options.ImageType.PostImage)
        {
            return "PostImage".Equals(image.Name, StringComparison.Ordinal)
                   && "PostImage".Equals(image.EntityAlias, StringComparison.Ordinal);
        }

        return false;
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

    /// <summary>
    /// Generates plugin steps from a CustomDataProviderRegistrationAttribute.
    /// Data providers run at Stage 30 (MainOperation), Synchronous mode, on the virtual entity.
    /// </summary>
    private List<PluginStep> GetDataProviderPluginSteps(Type pluginType, CustomAttributeData customAttribute)
    {
        var steps = new List<PluginStep>();

        var entityName = GetValue<string>(customAttribute, "entityName")!;
        var eventValue = GetValue<int>(customAttribute, "eventRegistration");
        var messageName = MapDataProviderEventToMessage(eventValue);

#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var sdk = (from mf in _context.SdkMessageFilterSet
            join m in _context.SdkMessageSet on mf.SdkMessageId.Id equals m.Id
            where mf.PrimaryObjectTypeCode == entityName
            where m.Name == messageName
            select new { mf, m }).SingleOrDefault();
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        if (sdk?.m == null || sdk.mf == null)
        {
            _console.MarkupLine(CultureInfo.InvariantCulture,
                "[yellow]Warning: DataProvider message {0} for entity {1} not found - skipping step[/]",
                messageName, entityName);
            return steps;
        }

        var step = new PluginStep
        {
            Mode = SdkMessageProcessingStep.Options.Mode.Synchronous,
            MessageName = messageName,
            Stage = SdkMessageProcessingStep.Options.Stage.MainOperationForInternalUseOnly,
            PrimaryEntityName = entityName,
            SecondaryEntityName = "none",
            ExecutionOrder = 1,
            ParentName = pluginType.FullName!,
            MessageId = sdk.m.SdkMessageId!.Value,
            MessageFilterId = sdk.mf.SdkMessageFilterId!.Value
        };
        step.Name = GetStepName(step);
        steps.Add(step);

        return steps;
    }

    internal static string MapDataProviderEventToMessage(int eventValue) => eventValue switch
    {
        0 => "Retrieve",
        1 => "RetrieveMultiple",
        2 => "Create",
        3 => "Update",
        4 => "Delete",
        _ => throw new AssemblyException($"Unknown DataProviderEvent value: {eventValue}")
    };

    private List<PluginStep> GetRegistrationPluginSteps(Type pluginType)
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
                ExecutionOrder = GetValue<int?>(customAttribute, "ExecutionOrder") ?? 100,
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


    private WorkflowRegistrationAttribute? GetWorkflowRegistration(Type workflowType)
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

    /// <summary>
    /// Generates a step name for a plugin step based on its properties.
    /// </summary>
    /// <param name="step">The plugin step for which the name is generated.</param>
    /// <returns>A string representing the generated step name.</returns>
    private static string GetStepName(PluginStep step)
    {
        // Check if the execution order has value
        if (step.ExecutionOrder.HasValue)
        {
            // Return formatted step name including execution order
            return $"{step.ParentName}|{(!string.IsNullOrEmpty(step.PrimaryEntityName) ? step.PrimaryEntityName : "entity")}|{Mode(step.Mode)}|{Stage(step.Stage)}|{step.MessageName}|{step.ExecutionOrder}";
        }

        // Return formatted step name without execution order
        return $"{step.ParentName}|{(!string.IsNullOrEmpty(step.PrimaryEntityName) ? step.PrimaryEntityName : "entity")}|{Mode(step.Mode)}|{Stage(step.Stage)}|{step.MessageName}";
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

    private static string[]? GetArrayValues(CustomAttributeData customAttribute, string property)
    {
        if (customAttribute.NamedArguments.Any(a => a.MemberName == property))
        {
            var propertyInfo = customAttribute.NamedArguments.Single(a => a.MemberName == property);
            if (propertyInfo.TypedValue.Value is not IReadOnlyCollection<CustomAttributeTypedArgument> valuesRaw)
            {
                return null;
            }

            return valuesRaw.Select(x => x.Value as string).OfType<string>().ToArray();
        }

        return null;
    }

    private T? GetValue<T>(CustomAttributeData customAttribute, string property)
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
            _console.MarkupLine(CultureInfo.InvariantCulture, $"Attribute Type mismatch [bold red]{Markup.Escape(property)} ({Markup.Escape(typeof(T).Name)}->{Markup.Escape(propertyInfo.TypedValue.ArgumentType.Name)})[/]");
        }

        var ctorPosition = customAttribute.Constructor.GetParameters().SingleOrDefault(c => c.Name == property)?.Position;
        if (ctorPosition.HasValue)
        {
            var propertyInfo = customAttribute.ConstructorArguments[ctorPosition.Value];
            if (propertyInfo.Value is T value){
                return value;
            }
            _console.MarkupLine(CultureInfo.InvariantCulture, $"Attribute Type mismatch [bold red]{Markup.Escape(property)} ({Markup.Escape(typeof(T).Name)}->{Markup.Escape(propertyInfo.ArgumentType.Name)})[/]");
        }

        return default;
    }

    private static T? GetAttribute<T>(Entity entity, string attribute) => entity.Attributes.ContainsKey(attribute)
        ? (T)entity.GetAttributeValue<AliasedValue>(attribute).Value
        : default;

    private static IEnumerable<Type> GetLoadableTypes(System.Reflection.Assembly assembly)
    {
        ArgumentNullException.ThrowIfNull(assembly);

        try
        {
            return assembly.GetTypes();
        }
        catch (ReflectionTypeLoadException e)
        {
            return e.Types.Where(t => t != null)!;
        }
    }


    public void Dispose()
    {
        _context.Dispose();
    }

    #endregion

}
