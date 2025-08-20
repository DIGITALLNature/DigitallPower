// Copyright (c) DIGITALL Nature. All rights reserved
// DIGITALL Nature licenses this file to you under the Microsoft Public License.

using dgt.power.codegeneration.Base;

namespace dgt.power.codegeneration.Templates.dotnet
{
    public partial class SdkMessagesTemplate
    {
        private readonly IEnumerable<(string Name, string Message)> SdkMessages;
        private readonly CodeGenerationConfig _cfg;
        private readonly string NameSpace;

        public SdkMessagesTemplate(IEnumerable<(string Name, string Message)> sdkMessages, CodeGenerationConfig cfg)
        {
            SdkMessages = sdkMessages;
            _cfg = cfg;
            NameSpace = _cfg.NameSpace;
        }

        private IEnumerable<(string Name, string Message)> Filter(IEnumerable<(string Name, string Message)> sdkMessages)
        {
            if (_cfg.SdkMessageFilters != null && _cfg.SdkMessageFilters.Any())
            {
                return sdkMessages.Where(t => _cfg.SdkMessageFilters.Contains(t.Item2));
            }
            return sdkMessages;
        }
    }
}
