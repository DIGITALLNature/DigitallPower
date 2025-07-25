// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.codegeneration.Model;

namespace dgt.power.codegeneration.Templates.tsl.ViewModels;

public record TabDetailsViewModel
{
    public TabDetailsViewModel(KeyValuePair<string, TabDetail> tabDetail)
    {
         Name = tabDetail.Key;
         Detail = tabDetail.Value;
    }

    public TabDetail Detail { get; set; }

    public string Name { get; set; }
}
