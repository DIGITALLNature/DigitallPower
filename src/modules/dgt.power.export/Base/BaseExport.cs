using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using dgt.power.common;
using Microsoft.Xrm.Sdk;

namespace dgt.power.export.Base;

public abstract class BaseExport : PowerLogic<ExportVerb>
{
    private readonly JsonSerializerOptions _options = new()
    {
        Converters =
        {
            new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
        }
    };

    protected BaseExport(ITracer tracer, IOrganizationService connection, IConfigResolver configResolver) : base(tracer,
        connection, configResolver)
    {
    }

    protected string GetJson(object obj) => Regex.Unescape(JsonSerializer.Serialize(JsonSerializer.Serialize(obj, _options))).Trim('"');

    protected static string HandleExportFile(string fileDir, string fileName, string content) =>
        HandleExportFile(fileDir, fileName, Encoding.UTF8.GetBytes(content));

    protected static string HandleExportFile(string fileDir, string fileName, byte[] content)
    {
        var directoryName = Path.GetDirectoryName(fileName) ?? "";
        var directory = Path.Combine(fileDir, directoryName);

        if (!string.IsNullOrWhiteSpace(directory))
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        fileName = Path.GetFileName(fileName);

        var work = Path.Combine(directory, $"{Guid.NewGuid():N}-{fileName}");
        var file = Path.Combine(directory, fileName);
        var back = Path.Combine(directory, $"old-{fileName}");

        File.WriteAllBytes(work, content);
        if (File.Exists(file))
        {
            File.Replace(work, file, back, false);
            File.Delete(back);
        }
        else
        {
            File.Move(work, file);
        }

        return file;
    }
}
