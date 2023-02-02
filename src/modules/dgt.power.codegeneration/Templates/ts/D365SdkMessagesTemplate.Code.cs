using dgt.power.codegeneration.Base;

namespace dgt.power.codegeneration.Templates.ts
{
    public partial class D365SdkMessagesTemplate
    {
        private readonly IEnumerable<Tuple<string, string>> SdkMessages;
        private readonly CodeGenerationConfig _cfg;

        public D365SdkMessagesTemplate(IEnumerable<Tuple<string, string>> sdkMessages, CodeGenerationConfig cfg)
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
    }
}
