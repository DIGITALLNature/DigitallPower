

// ReSharper disable once CheckNamespace
namespace D365.Extension.Registration;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class CustomApiRegistrationAttribute : Attribute
{
    public CustomApiRegistrationAttribute(string messageName)
    {
        MessageName = messageName;
    }

    /// <summary>
    /// Message of plugin for this registration
    /// </summary>
    public string MessageName { get; }
}
