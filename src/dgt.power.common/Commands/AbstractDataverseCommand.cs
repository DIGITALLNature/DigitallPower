using dgt.power.dataverse;
using Microsoft.Xrm.Sdk;

namespace dgt.power.common.Commands;

public abstract class AbstractDataverseCommand<TCommandSettings> : AbstractPowerCommand<TCommandSettings>, IDisposable where TCommandSettings : BaseProgramSettings
{
    protected AbstractDataverseCommand(IOrganizationService organizationService, IConfigResolver configResolver) : base(configResolver)
    {
        OrganizationService = organizationService;
        DataContext = new DataContext(OrganizationService);
    }

    protected IOrganizationService OrganizationService { get; }
    protected DataContext DataContext { get; }

    public void Dispose()
    {
        DataContext.Dispose();
        GC.SuppressFinalize(this);
    }
}
