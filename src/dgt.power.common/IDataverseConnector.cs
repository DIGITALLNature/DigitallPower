// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using Microsoft.PowerPlatform.Dataverse.Client;

namespace dgt.power.common;

public interface IDataverseConnector
{
    Task<ServiceClient> CreateServiceClientAsync();
}
