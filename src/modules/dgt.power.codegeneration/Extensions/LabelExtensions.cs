// SPDX-FileCopyrightText: DIGITALL Nature
// SPDX-License-Identifier: MS-RL

using Microsoft.Xrm.Sdk;

namespace dgt.power.codegeneration.Extensions;

public static class LabelExtensions
{
    public static string GetLocalizedLabel(this Label label, int? languageCode = null)
    {
        ArgumentNullException.ThrowIfNull(label);
        return languageCode == null
            ? label.UserLocalizedLabel.Label
            : label.LocalizedLabels.SingleOrDefault(l => l.LanguageCode == languageCode)?.Label ??
              label.UserLocalizedLabel.Label;
    }

}
