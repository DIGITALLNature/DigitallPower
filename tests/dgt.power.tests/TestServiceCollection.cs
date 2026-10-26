// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

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
