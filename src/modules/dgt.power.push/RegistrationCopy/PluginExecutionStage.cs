// ReSharper disable once CheckNamespace
namespace D365.Extension.Registration;

public enum PluginExecutionStage
{
    PreValidation = 10,
    PreOperation = 20,
    MainOperation = 30,
    PostOperation = 40
}