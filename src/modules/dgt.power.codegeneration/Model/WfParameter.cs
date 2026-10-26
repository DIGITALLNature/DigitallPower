// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

namespace dgt.power.codegeneration.Model;
// ReSharper disable UnusedAutoPropertyAccessor.Global

public class WfParameter
{
    public string? Name { get; set; }
    public string? UniqueName { get; set; }
    public string? Type { get; set; }
    public string? Description { get; set; }
    public string? Entityname { get; set; }
    public required bool IsOptional { get; set; }

    public required bool IsOutput { get; set; }
}
