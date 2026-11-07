// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

// ReSharper disable once CheckNamespace
namespace Digitall.Plugins.Registration;

/// <summary>
/// Impostor double: correct namespace, but defined in the test assembly instead of the registration
/// library. <see cref="dgt.power.plugin.Deployment.Local.AssemblyReflectionReader"/> must not treat
/// it as a supported registration attribute because the defining assembly differs.
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public sealed class ImpostorPluginRegistrationAttribute : Attribute;
