

// ReSharper disable once CheckNamespace
namespace D365.Extension.Registration;

[AttributeUsage(AttributeTargets.Class)]
public class WorkflowRegistrationAttribute: Attribute
{
    public WorkflowRegistrationAttribute(string name, string group = "d365.extension", bool includeVersion = false)
    {
        Name = name;
        Group = group;
        IncludeVersion = includeVersion;
    }

    public string Name { get; }
    public string Group { get; }
    public bool IncludeVersion { get; }
}
