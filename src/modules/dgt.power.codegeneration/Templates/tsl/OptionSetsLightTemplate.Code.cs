// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.codegeneration.Logic;
using dgt.power.codegeneration.Templates.ts;

namespace dgt.power.codegeneration.Templates.tsl;

public partial class OptionSetsLightTemplate(SortedDictionary<string, List<Option>> optionSets,string typingPath) : ITemplate
{
    private static string CamelCase(string phrase) => Formatter.CamelCase(phrase);
    private static string Sanitize(string value, bool allowWhitespace = false, bool allowSafeStringChars = false, bool allowFirstNumber = false)
        =>  Formatter.Sanitize(value, allowWhitespace, allowSafeStringChars, allowFirstNumber);

    public  string GenerateTemplate() => TransformText();
}
