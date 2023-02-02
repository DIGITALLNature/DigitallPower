namespace dgt.power.common;

public interface IConfigResolver
{
    bool GetConfigFile<TC>(string fileDir, string file, out TC config) where TC : new();
    bool GetConfigFile<TC>(string file, out TC config) where TC : new();
    bool ConfigFromFile<T>(string file, out T obj) where T : new();
}
