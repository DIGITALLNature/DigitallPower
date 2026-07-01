// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Microsoft.Xrm.Sdk.Metadata;

namespace dgt.power.tests.Extensions;

/// <summary>
/// Reflection-based helpers for setting sealed properties on Xrm SDK metadata objects in tests.
/// </summary>
[SuppressMessage("Minor Code Smell", "S4225:Extension methods should not extend \"object\"")]
public static class MetadataExtensions
{
    /// <summary>
    /// Sets a sealed (read-only) property on a metadata object using reflection.
    /// </summary>
    public static void SetSealedPropertyValue(this object metadata, string propertyName, object? value)
    {
        var property = metadata.GetType().GetProperty(propertyName,
            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        if (property == null)
        {
            throw new InvalidOperationException(
                $"Property '{propertyName}' not found on type '{metadata.GetType().Name}'.");
        }

        property.SetValue(metadata, value, null);
    }

    /// <summary>
    /// Sets the Attributes collection on an EntityMetadata object using reflection.
    /// </summary>
    public static void SetAttributeCollection(this EntityMetadata metadata,
        IEnumerable<AttributeMetadata> attributes)
    {
        metadata.GetType()
            .GetProperty("Attributes", BindingFlags.Public | BindingFlags.Instance)!
            .SetValue(metadata, attributes.ToArray());
    }
}
