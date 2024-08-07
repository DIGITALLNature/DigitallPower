﻿// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

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
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if(disposing)
        {
            DataContext.Dispose();
        }
    }

    ~AbstractDataverseCommand()
    {
        Dispose(false);
    }
}
