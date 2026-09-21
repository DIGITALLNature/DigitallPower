// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Globalization;
using System.Reflection;
using dgt.power.common.Extensions;
using dgt.power.dataverse;
using Microsoft.Xrm.Sdk;
using Spectre.Console;
using Assembly = System.Reflection.Assembly;

namespace dgt.power.plugin.Local;

/// <summary>
/// Parses a local plugin assembly (.dll) into a <see cref="LocalAssembly"/> via
/// <see cref="MetadataLoadContext"/> reflection-only loading. Purely local: never touches
/// Dataverse. Message/message-filter resolution for declared steps happens later, in the
/// Dataverse layer, when the step is reconciled against the target environment.
/// </summary>
/// <remarks>
/// Registration attributes are detected purely by type name/namespace via reflection metadata
/// (<see cref="CustomAttributeData"/>), never by a real type reference - the assembly under
/// inspection is loaded into a <see cref="MetadataLoadContext"/>, so its types are never
/// assignable to (or comparable with) any locally referenced attribute type anyway. This module
/// therefore has no compile-time dependency on the registration attributes package.
/// </remarks>
internal sealed class AssemblyReflectionReader(IAnsiConsole console)
{
    public LocalAssembly? Read(string dllFile, MetadataLoadContext metadataLoadContext)
    {
        ArgumentNullException.ThrowIfNull(metadataLoadContext);
        try
        {
            var assembly = metadataLoadContext.LoadFromAssemblyPath(dllFile);
            if (!assembly.IsFullyTrusted)
            {
                return null;
            }

            AssertNoWorkflowActivities(assembly);

            var pluginTypes = GetLoadableTypes(assembly).Where(IsIPluginBased).ToList();

            var (managedIdentityClientId, managedIdentityTenantId) = ParseAssemblyLevelAttributes(assembly);

            var localPluginTypes = pluginTypes.Select(BuildPluginType).ToList();
            foreach (var pluginType in localPluginTypes)
            {
                LocalPluginStepValidator.Validate(pluginType);
            }

            var kind = LocalAssemblyKind.None;
            if (localPluginTypes.Count > 0)
            {
                kind |= LocalAssemblyKind.Plugin;
            }

            if (localPluginTypes.Exists(t => t.HasRegistrationAttribute))
            {
                kind |= LocalAssemblyKind.DeclarativePlugin;
            }

            return new LocalAssembly
            {
                Name = assembly.GetName().Name!,
                Version = assembly.GetName().Version!,
                Content = Convert.ToBase64String(File.ReadAllBytes(dllFile)),
                Kind = kind,
                PluginTypes = localPluginTypes,
                ManagedIdentityClientId = managedIdentityClientId,
                ManagedIdentityTenantId = managedIdentityTenantId
            };
        }
        catch (WorkflowActivityNotSupportedException)
        {
            throw;
        }
        catch (InvalidPluginStepException)
        {
            throw;
        }
        catch (Exception e) when (e is not OutOfMemoryException and not StackOverflowException)
        {
            console.MarkupLine(Markup.Escape(e.RootMessage()));
            return null;
        }
    }

    /// <summary>
    /// Workflow activities (CodeActivity-derived types) are a legacy Dataverse plugin type that
    /// this new module does not support - only the deprecated 'push' command still handles them.
    /// </summary>
    private static void AssertNoWorkflowActivities(Assembly assembly)
    {
        var workflowTypeNames = GetLoadableTypes(assembly)
            .Where(IsCodeActivityBased)
            .Select(t => t.FullName!)
            .ToList();

        if (workflowTypeNames.Count > 0)
        {
            throw new WorkflowActivityNotSupportedException(assembly.GetName().Name!, workflowTypeNames);
        }
    }

    internal LocalPluginType BuildPluginType(Type pluginType)
    {
        var hasRegistrationAttribute = HasRegistrationAttribute(pluginType);
        var steps = new List<LocalPluginStep>();
        var customApi = string.Empty;

        if (hasRegistrationAttribute)
        {
            var customAttributes = CustomAttributeData.GetCustomAttributes(pluginType);
            foreach (var customAttribute in customAttributes)
            {
                if (!RegistrationAttributeNames.KnownNamespaces.Contains(customAttribute.AttributeType.Namespace))
                {
                    continue;
                }

                if (customAttribute.AttributeType.Name == RegistrationAttributeNames.CustomApiRegistration)
                {
                    customApi = GetValue<string>(customAttribute, "messageName") ?? string.Empty;
                }

                if (customAttribute.AttributeType.Name == RegistrationAttributeNames.CustomDataProviderRegistration)
                {
                    var step = BuildDataProviderStep(pluginType, customAttribute);
                    if (step != null)
                    {
                        steps.Add(step);
                    }
                }
            }

            steps.AddRange(BuildRegistrationSteps(pluginType));
        }
        else
        {
            console.MarkupLine(CultureInfo.InvariantCulture,
                "[yellow]Hint:[/] plugin type [bold]{0}[/] has no registration attribute - its steps (if any) must be managed manually. Did you forget to add one?",
                Markup.Escape(pluginType.FullName!));
        }

        return new LocalPluginType(pluginType.FullName!, pluginType.FullName!, customApi, hasRegistrationAttribute, steps);
    }

    private static LocalPluginStep? BuildDataProviderStep(Type pluginType, CustomAttributeData customAttribute)
    {
        var entityName = GetValue<string>(customAttribute, "entityName");
        if (entityName == null)
        {
            return null;
        }

        var eventValue = GetValue<int>(customAttribute, "eventRegistration");
        var messageName = MapDataProviderEventToMessage(eventValue);

        var step = new LocalPluginStep(
            string.Empty,
            SdkMessageProcessingStep.Options.Mode.Synchronous,
            messageName,
            SdkMessageProcessingStep.Options.Stage.MainOperationForInternalUseOnly,
            entityName,
            "none",
            null,
            1,
            null,
            []);

        return step with { Name = GetStepName(step, pluginType.FullName!) };
    }

    private static List<LocalPluginStep> BuildRegistrationSteps(Type pluginType)
    {
        var steps = new List<LocalPluginStep>();

        var customAttributes = CustomAttributeData.GetCustomAttributes(pluginType);
        foreach (var customAttribute in customAttributes)
        {
            if (!RegistrationAttributeNames.KnownNamespaces.Contains(customAttribute.AttributeType.Namespace) ||
                customAttribute.AttributeType.Name != RegistrationAttributeNames.PluginRegistration)
            {
                continue;
            }

            var messageName = GetValue<string>(customAttribute, "messageName")!;
            var primaryEntityName = GetValue<string>(customAttribute, "PrimaryEntityName") ?? "none";
            var secondaryEntityName = GetValue<string>(customAttribute, "SecondaryEntityName") ?? "none";
            var mode = GetValue<int>(customAttribute, "mode");
            var stage = GetValue<int>(customAttribute, "stage");
            var executionOrder = GetValue<int?>(customAttribute, "ExecutionOrder") ?? 100;
            var configuration = GetValue<string>(customAttribute, "Configuration");

            var images = new List<LocalPluginStepImage>();
            if (GetValue<bool>(customAttribute, "PreEntityImage"))
            {
                images.Add(new LocalPluginStepImage(
                    SdkMessageProcessingStepImage.Options.ImageType.PreImage,
                    "PreImage",
                    "PreImage",
                    GetMessagePropertyName(messageName),
                    GetArrayValues(customAttribute, "PreEntityImageAttributes")));
            }

            if (GetValue<bool>(customAttribute, "PostEntityImage"))
            {
                images.Add(new LocalPluginStepImage(
                    SdkMessageProcessingStepImage.Options.ImageType.PostImage,
                    "PostImage",
                    "PostImage",
                    GetMessagePropertyName(messageName),
                    GetArrayValues(customAttribute, "PostEntityImageAttributes")));
            }

            var step = new LocalPluginStep(
                string.Empty,
                mode,
                messageName,
                stage,
                primaryEntityName,
                secondaryEntityName,
                GetArrayValues(customAttribute, "FilterAttributes"),
                executionOrder,
                configuration,
                images);

            steps.Add(step with { Name = GetStepName(step, pluginType.FullName!) });
        }

        return steps;
    }

    private static (string? ClientId, string? TenantId) ParseAssemblyLevelAttributes(Assembly assembly)
    {
        string? clientId = null;
        string? tenantId = null;
        var assemblyAttributes = CustomAttributeData.GetCustomAttributes(assembly);
        foreach (var attr in assemblyAttributes)
        {
            if (!RegistrationAttributeNames.KnownNamespaces.Contains(attr.AttributeType.Namespace) ||
                attr.AttributeType.Name != RegistrationAttributeNames.ManagedIdentityRegistration)
            {
                continue;
            }

            clientId = GetValue<string>(attr, "clientId");
            tenantId = GetValue<string>(attr, "TenantId");
        }

        return (clientId, tenantId);
    }

    /// <summary>
    /// Generates a step name for a plugin step based on its properties.
    /// </summary>
    internal static string GetStepName(LocalPluginStep step, string parentName)
    {
        var entity = !string.IsNullOrEmpty(step.PrimaryEntityName) ? step.PrimaryEntityName : "entity";
        return step.ExecutionOrder.HasValue
            ? $"{parentName}|{entity}|{Mode(step.Mode)}|{Stage(step.Stage)}|{step.MessageName}|{step.ExecutionOrder}"
            : $"{parentName}|{entity}|{Mode(step.Mode)}|{Stage(step.Stage)}|{step.MessageName}";
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

    internal static string GetMessagePropertyName(string messageName) => messageName.ToLowerInvariant() switch
    {
        "update" or "delete" => "Target",
        "setstate" or "setstatedynamicentity" => "EntityMoniker",
        _ => "Id"
    };

    internal static string? Mode(int modeValue) => modeValue switch
    {
        0 => "Synchronous",
        1 => "Asynchronous",
        _ => null
    };

    internal static string? Stage(int stageValue) => stageValue switch
    {
        10 => "PreValidation",
        20 => "PreOperation",
        30 => "MainOperation",
        40 => "PostOperation",
        _ => null
    };

    private static bool IsIPluginBased(Type declaredType) =>
        declaredType.GetInterface(typeof(IPlugin).FullName!) != null && !declaredType.IsAbstract;

    // Detected by FullName only (no System.Activities/UiPath.Workflow dependency needed here) since
    // workflow activities are not modeled/supported by this module - see AssertNoWorkflowActivities.
    private static bool IsCodeActivityBased(Type declaredType) =>
        declaredType.GetBaseTypes().Any(t => t.FullName == "System.Activities.CodeActivity") && !declaredType.IsAbstract;

    private static bool HasRegistrationAttribute(Type declaredType)
    {
        var customAttributes = CustomAttributeData.GetCustomAttributes(declaredType);
        return customAttributes.Any(customAttribute =>
            RegistrationAttributeNames.KnownNamespaces.Contains(customAttribute.AttributeType.Namespace) &&
            RegistrationAttributeNames.KnownPluginAttributes.Contains(customAttribute.AttributeType.Name));
    }

    private static string[]? GetArrayValues(CustomAttributeData customAttribute, string property)
    {
        var namedArgument = customAttribute.NamedArguments.SingleOrDefault(a => a.MemberName == property);
        if (namedArgument.TypedValue.Value is not IReadOnlyCollection<CustomAttributeTypedArgument> valuesRaw)
        {
            return null;
        }

        return valuesRaw.Select(x => x.Value as string).OfType<string>().ToArray();
    }

    private static T? GetValue<T>(CustomAttributeData customAttribute, string property)
    {
        if (typeof(T).IsArray)
        {
            throw new ArgumentOutOfRangeException(nameof(property), "can not be a Array");
        }

        var namedArgument = customAttribute.NamedArguments.SingleOrDefault(a => a.MemberName == property);
        if (TryConvertValue<T>(namedArgument.TypedValue.Value, out var namedValue))
        {
            return namedValue;
        }

        var ctorPosition = customAttribute.Constructor.GetParameters().SingleOrDefault(c => c.Name == property)?.Position;
        if (ctorPosition.HasValue &&
            TryConvertValue<T>(customAttribute.ConstructorArguments[ctorPosition.Value].Value, out var ctorValue))
        {
            return ctorValue;
        }

        return default;
    }

    private static bool TryConvertValue<T>(object? value, out T? result)
    {
        if (value is T typedValue)
        {
            result = typedValue;
            return true;
        }

        var nullableType = Nullable.GetUnderlyingType(typeof(T));
        if (nullableType is not null && value is not null && nullableType.IsInstanceOfType(value))
        {
            result = (T)value;
            return true;
        }

        result = default;
        return false;
    }

    private static IEnumerable<Type> GetLoadableTypes(Assembly assembly)
    {
        try
        {
            return assembly.GetTypes();
        }
        catch (ReflectionTypeLoadException e)
        {
            return e.Types.Where(t => t != null)!;
        }
    }
}
