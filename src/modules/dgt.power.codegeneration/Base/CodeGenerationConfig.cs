// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.codegeneration.Base.Config;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable CollectionNeverUpdated.Global

namespace dgt.power.codegeneration.Base;

/// <summary>
///     V1 legacy configuration. Kept for backward compatibility with existing config files
///     that don't have a <c>"type"</c> discriminator. Use <see cref="DotNetCodeGenerationConfig"/>
///     or <see cref="TypeScriptCodeGenerationConfig"/> for new configs.
/// </summary>
#pragma warning disable CA2227 // Entities and Forms are assigned post-construction
[Obsolete("Use DotNetCodeGenerationConfig or TypeScriptCodeGenerationConfig (V2 config with \"type\" discriminator). This class will be removed in a future major version.")]
public class CodeGenerationConfig
{
    private HashSet<string> _actions = new();
    private HashSet<string> _customApis = new();

    private HashSet<string> _entities = new();
    private HashSet<string> _forms = new();
    private HashSet<string> _sdkMessages = new();
    private HashSet<string> _solutions = new();

    public string NameSpace { get; init; } = "Digitall.APower.Model";

    public ICollection<string> Entities
    {
        get => _entities;
        set
        {
            _entities = new HashSet<string>(value);
            _entities.TrimExcess();
        }
    }

    public IReadOnlyCollection<string> Actions
    {
        get => _actions;
        init
        {
            _actions = new HashSet<string>(value);
            _actions.TrimExcess();
        }
    }

    // ReSharper disable once InconsistentNaming
    public IReadOnlyCollection<string> CustomAPIs
    {
        get => _customApis;
        init
        {
            _customApis = new HashSet<string>(value);
            _customApis.TrimExcess();
        }
    }

    //additional sdk messages
    public IReadOnlyCollection<string> AdditionalSdkMessages
    {
        get => _sdkMessages;
        init
        {
            _sdkMessages = new HashSet<string>(value);
            _sdkMessages.TrimExcess();
        }
    }

    public IReadOnlyCollection<string> Solutions
    {
        get => _solutions;
        init
        {
            _solutions = new HashSet<string>(value);
            _solutions.TrimExcess();
        }
    }

    public IReadOnlyCollection<string> Forms
    {
        get => _forms;
        set
        {
            _forms = new HashSet<string>(value);
            _forms.TrimExcess();
        }
    }

    public string? EntityMask { get; init; }

    public bool SuppressDotNet { get; init; }

    public bool SuppressTypeScript { get; init; }

    public bool SuppressMetaData { get; init; }

    public TypescriptGeneratorVersion TypescriptGeneratorVersion { get; init; } = TypescriptGeneratorVersion.Light;

    public bool SuppressOptions { get; init; }

    public bool SuppressLogicalNames { get; init; }

    public bool SuppressContext { get; init; }

    public bool SuppressActions { get; init; }

    public bool SuppressSdkMessages { get; init; }

    public bool SuppressRelations { get; init; }

    public bool SuppressNavigationProperties { get; init; }

    public bool SuppressEntityTypeCode { get; init; }

    public bool SuppressAlternateKeys { get; init; }

    public bool Virtual { get; init; }

    public bool UseBaseLanguage { get; init; }

    public bool NonDebuggerNonUserCode { get; init; }

    public bool EditableReadOnlyProperties { get; init; }

    public string TypingPath { get; init; } = "\"\"../../Typings/Xrm/index.d.ts\"\"";

    public bool OnlyFormsFromSolutions { get; init; }

    public bool XrmMockFormHelpers { get; init; }

    public HashSet<EntityFilter> EntityFilters { get; init; } = new();

    public HashSet<EntityRefFilter> EntityRefFilters { get; init; } = new();

    public HashSet<EntityFormFilter> EntityFormFilters { get; init; } = new();

    public HashSet<string> SdkMessageFilters { get; init; } = new();

    public HashSet<string> GlobalOptionSets { get; init; } = new();

    public HashSet<string> BusinessProcessFlows { get; init; } = new();

    public bool SuppressNullableSupport { get; init; }

    /// <summary>
    ///     Maps this V1 config to the appropriate V2 config(s).
    ///     Returns a <see cref="DotNetCodeGenerationConfig"/> when DotNet is not suppressed,
    ///     or a <see cref="TypeScriptCodeGenerationConfig"/> when TypeScript is not suppressed.
    /// </summary>
    public DotNetCodeGenerationConfig ToDotNetConfig()
    {
        var requests = new HashSet<string>();
        foreach (var a in Actions) requests.Add(a);
        foreach (var c in CustomAPIs) requests.Add(c);
        foreach (var s in AdditionalSdkMessages) requests.Add(s);

        return new DotNetCodeGenerationConfig
        {
            Entities = new HashSet<string>(Entities),
            Solutions = Solutions,
            EntityMask = EntityMask,
            Language = UseBaseLanguage ? null : 0,
            GlobalOptionSets = GlobalOptionSets,
            Requests = requests,
            Namespace = NameSpace,
            Target = SuppressNullableSupport ? DotNetTarget.Framework : DotNetTarget.Modern,
            VirtualProperties = Virtual,
            EditableReadOnlyProperties = EditableReadOnlyProperties,
            Include = new DotNetInclude
            {
                Context = !SuppressContext,
                Options = !SuppressOptions,
                LogicalNames = !SuppressLogicalNames,
                Relations = !SuppressRelations,
                NavigationProperties = !SuppressNavigationProperties,
                EntityTypeCode = !SuppressEntityTypeCode,
                AlternateKeys = !SuppressAlternateKeys,
                Metadata = !SuppressMetaData
            }
        };
    }

    public TypeScriptCodeGenerationConfig ToTypeScriptConfig()
    {
        var requests = new HashSet<string>();
        foreach (var a in Actions) requests.Add(a);
        foreach (var c in CustomAPIs) requests.Add(c);
        foreach (var s in AdditionalSdkMessages) requests.Add(s);

        return new TypeScriptCodeGenerationConfig
        {
            Entities = new HashSet<string>(Entities),
            Solutions = Solutions,
            EntityMask = EntityMask,
            Language = UseBaseLanguage ? null : 0,
            GlobalOptionSets = GlobalOptionSets,
            Requests = requests,
            XrmMockFormHelpers = XrmMockFormHelpers,
            OnlyFormsFromSolutions = OnlyFormsFromSolutions,
            Include = new TypeScriptInclude
            {
                Options = !SuppressOptions,
                SdkMessages = !SuppressSdkMessages
            },
            Forms = new HashSet<string>(Forms)
        };
    }
}
#pragma warning restore CA2227
