using dgt.power.codegeneration.Base;

namespace dgt.power.codegeneration.Templates.dotnet
{
    public partial class SdkMessagesTemplate
    {
        private readonly IEnumerable<Tuple<string, string>> SdkMessages;
        private readonly CodeGenerationConfig _cfg;
        private readonly string NameSpace;

        public SdkMessagesTemplate(IEnumerable<Tuple<string, string>> sdkMessages, CodeGenerationConfig cfg)
        {
            SdkMessages = sdkMessages;
            _cfg = cfg;
            NameSpace = _cfg.NameSpace;
        }

        private IEnumerable<Tuple<string, string>> Filter(IEnumerable<Tuple<string, string>> sdkMessages)
        {
            if (_cfg.SdkMessageFilters != null && _cfg.SdkMessageFilters.Any())
            {
                return sdkMessages.Where(t => _cfg.SdkMessageFilters.Contains(t.Item2));
            }
            return sdkMessages;
        }
    }
}