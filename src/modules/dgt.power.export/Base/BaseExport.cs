// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using dgt.power.common;
using dgt.power.common.FileAccess;
using Microsoft.Xrm.Sdk;

namespace dgt.power.export.Base;

public abstract class BaseExport : PowerLogic<ExportVerb>
{
    private readonly IFileService _fileService;

    private readonly JsonSerializerOptions _options = new()
    {
        Converters =
        {
            new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
        }
    };

    protected BaseExport(ITracer tracer, IOrganizationService connection, IConfigResolver configResolver, IFileService fileService) : base(tracer,
        connection, configResolver)
    {
        _fileService = fileService;
    }

    protected string GetJson(object obj) => Regex.Unescape(JsonSerializer.Serialize(JsonSerializer.Serialize(obj, _options))).Trim('"');

    protected string HandleExportFile(string fileDir, string fileName, string content) =>
        _fileService.ExportFile(fileDir, fileName, content);

    protected string HandleExportFile(string fileDir, string fileName, byte[] content) =>
        _fileService.ExportFile(fileDir, fileName, content);
}
