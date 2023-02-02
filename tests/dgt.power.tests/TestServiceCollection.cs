using dgt.power.common;
using dgt.power.common.Logic;
using Microsoft.Extensions.DependencyInjection;

namespace dgt.power.tests;

public class TestServiceCollection : ServiceCollection
{
    public TestServiceCollection()
    {
        this.AddSingleton<ITracer, TestTracer>();
        this.AddSingleton<IXrmConnection, TestConnection>();
        this.AddScoped<IConfigResolver, ConfigResolver>();
        this.AddSingleton<IProfileManager, ProfileManager>();
    }
}
