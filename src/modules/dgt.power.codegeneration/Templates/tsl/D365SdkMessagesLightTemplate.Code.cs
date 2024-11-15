// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.codegeneration.Base;
using dgt.power.codegeneration.Templates.ts;

namespace dgt.power.codegeneration.Templates.tsl
{
    public partial class D365SdkMessagesLightTemplate : ITemplate
    {
        private readonly IEnumerable<Tuple<string, string>> SdkMessages;
        private readonly CodeGenerationConfig _cfg;

        public D365SdkMessagesLightTemplate(IEnumerable<Tuple<string, string>> sdkMessages, CodeGenerationConfig cfg)
        {
            SdkMessages = sdkMessages;
            _cfg = cfg;
        }

        private IEnumerable<Tuple<string, string>> Filter(IEnumerable<Tuple<string, string>> sdkMessages)
        {
            if (_cfg.SdkMessageFilters.Any())
            {
                return sdkMessages.Where(t => _cfg.SdkMessageFilters.Contains(t.Item2));
            }
            return sdkMessages;
        }

        public string GenerateTemplate() => TransformText();
    }
}
