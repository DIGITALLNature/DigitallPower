// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.codegeneration.Base;
using dgt.power.codegeneration.Logic;

namespace dgt.power.codegeneration.Templates.ts
{
    public partial class D365OptionSetsTemplate : ITemplate
    {
        private readonly SortedDictionary<string, List<Option>> OptionSets;
        private readonly CodeGenerationConfig _cfg;

        public D365OptionSetsTemplate(SortedDictionary<string, List<Option>> optionSets, CodeGenerationConfig cfg)
        {
            OptionSets = optionSets;
            _cfg = cfg;
        }

        private static string CamelCase(string phrase)
        {
            return Formatter.CamelCase(phrase);
        }

        private static string Sanitize(string value)
        {
            return Formatter.Sanitize(value);
        }

        public string GenerateTemplate() => TransformText();
    }
}
