// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using System.Text.Json;
using System.Text.Json.Serialization;
using dgt.power.common;
using dgt.power.common.FileAccess;
using dgt.power.common.Logic;
using Microsoft.Extensions.DependencyInjection;

namespace dgt.power.tests;

public class TestServiceCollection : ServiceCollection
{
    public TestServiceCollection()
    {
        this.AddSingleton<ITracer, TestTracer>();
        this.AddSingleton<IXrmConnection, TestConnection>();
        this.AddSingleton<IProfileManager, ProfileManager>();
        this.AddSingleton<JsonSerializerOptions>(_ => new JsonSerializerOptions
        {
            Converters =
            {
                new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
            }
        });
        this.AddScoped<IConfigResolver, ConfigResolver>();
        this.AddScoped<IFileService, FileService>();
    }
}
