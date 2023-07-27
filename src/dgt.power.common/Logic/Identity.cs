// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.common.Logic;

public class Identity
{
    public required string ConnectionString { get; init; }
    public string SecurityProtocol { get; init; } = "Tls12";
    public bool Insecure { get; init; }
}
