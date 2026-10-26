// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.common;
using dgt.power.common.Logic;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;

namespace dgt.power.tests;

public class TestServiceCollection : ServiceCollection
{
    public TestServiceCollection()
    {
        this.AddSingleton<ICommandSpanFactory, TestSpanFactory>();
        this.AddSingleton<IDataverseConnection, TestConnection>();
        this.AddScoped<IConfigResolver, ConfigResolver>();
        this.AddSingleton(AnsiConsole.Console);
    }
}
