// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

namespace dgt.power.common.Logic;

public class TokenIdentity : Identity
{
    public required string Token { get; set; }
    public string? Username { get; set; }
}
