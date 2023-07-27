// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.codegeneration.Base;
using dgt.power.codegeneration.Logic;
using System.Text.RegularExpressions;

namespace dgt.power.codegeneration.Templates.ts
{
    public partial class D365BusinessProcessFlowTemplate
    {
        private readonly string TypingPath;
        private readonly Tuple<string, string, Guid, string> Process;
        private readonly List<Tuple<string, string, List<Guid>>> Stages;
        private readonly CodeGenerationConfig _cfg;

        public D365BusinessProcessFlowTemplate(string typingPath, Tuple<string, string, Guid, string> process, List<Tuple<string, string, List<Guid>>> stages, CodeGenerationConfig cfg)
        {
            TypingPath = typingPath;
            Process = process;
            Stages = stages;
            _cfg = cfg;
        }

        private static string Sanitize(string value)
        {
            var result = Formatter.PreventFirstNumber(value);
            return Regex.Replace(result, @"[^0-9a-zA-Z_]+", "_");
        }
    }
}
