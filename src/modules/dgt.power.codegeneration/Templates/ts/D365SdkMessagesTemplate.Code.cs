// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.codegeneration.Base;

namespace dgt.power.codegeneration.Templates.ts
{
    public partial class D365SdkMessagesTemplate : ITemplate
    {
        private readonly IEnumerable<(string Name, string Message)> SdkMessages;
        private readonly CodeGenerationConfig _cfg;

        public D365SdkMessagesTemplate(IEnumerable<(string Name, string Message)> sdkMessages, CodeGenerationConfig cfg)
        {
            SdkMessages = sdkMessages;
            _cfg = cfg;
        }

        private IEnumerable<(string Name, string Message)> Filter(IEnumerable<(string Name, string Message)> sdkMessages)
        {
            if (_cfg.SdkMessageFilters.Any())
            {
                return sdkMessages.Where(t => _cfg.SdkMessageFilters.Contains(t.Message));
            }
            return sdkMessages;
        }

        public string GenerateTemplate() => TransformText();
    }
}
