// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using Microsoft.Xrm.Sdk;

namespace dgt.power.codegeneration.Extensions;

public static class LabelExtensions
{
    public static string GetLocalizedLabel(this Label label, int? languageCode = null) =>
        languageCode == null
            ? label.UserLocalizedLabel.Label
            : label.LocalizedLabels.Single(l => l.LanguageCode == languageCode).Label;
}
