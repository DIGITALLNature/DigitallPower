// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

// ReSharper disable once CheckNamespace
namespace Digitall.Plugins.Registration;

// ReSharper disable UnusedAutoPropertyAccessor.Global
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public sealed class CustomDataProviderRegistrationAttribute : Attribute
{
    public CustomDataProviderRegistrationAttribute() { }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1019", Justification = "Parameter and property names mirror the registration library's reflection contract.")]
    public CustomDataProviderRegistrationAttribute(string dataSourceSchemaName, DataProviderEvent eventRegistration, string providerName)
    {
        DataSourceSchemaName = dataSourceSchemaName;
        Event = eventRegistration;
        ProviderName = providerName;
    }

    public CustomDataProviderRegistrationAttribute(string entityName, int eventRegistration)
    {
        EntityName = entityName;
        EventRegistration = eventRegistration;
    }

    public string? EntityName { get; }
    public int EventRegistration { get; }
    public string? DataSourceSchemaName { get; }
    public DataProviderEvent Event { get; }
    public string? ProviderName { get; }
    public string? DataSourceDisplayName { get; set; }
    public string? DataSourcePluralName { get; set; }
    public string? Description { get; set; }
}
