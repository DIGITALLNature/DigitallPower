// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace dgt.power.common.FileAccess;

public class FileService : IFileService
{
    private readonly JsonSerializerOptions _options;

    public FileService(JsonSerializerOptions options)
    {
        _options = options;
    }

    public string ExportJsonFile<TObject>(string fileDirectory, string fileName, TObject jsonObject)
    {
        var json = Regex.Unescape(JsonSerializer.Serialize(JsonSerializer.Serialize(jsonObject, _options))).Trim('"');
        return ExportFile(fileDirectory, fileName, json);
    }

    public string ExportFile(string fileDirectory, string fileName, string content)
    {
        return ExportFile(fileDirectory, fileName, Encoding.UTF8.GetBytes(content));
    }

    public string ExportFile(string fileDirectory, string fileName, byte[] content)
    {
        var directoryName = Path.GetDirectoryName(fileName) ?? "";
        var directory = Path.Combine(fileDirectory, directoryName);

        if (!string.IsNullOrWhiteSpace(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
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
