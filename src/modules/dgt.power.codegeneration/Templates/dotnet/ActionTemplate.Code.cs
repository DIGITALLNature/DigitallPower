using System.Globalization;
using System.Text;
using dgt.power.codegeneration.Model;

namespace dgt.power.codegeneration.Templates.dotnet
{
    public partial class ActionTemplate
    {
        private readonly IEnumerable<WfAction> Actions;
        private readonly string NameSpace;
        private readonly string[] ProtectedNames = new[] {"ExtensionData", "Parameters", "RequestId", "RequestName"};

        public ActionTemplate(IEnumerable<WfAction> actions, string ns)
        {
            Actions = actions;
            NameSpace = ns;
        }

        internal static string CamelCase(string phrase)
        {
            return ConvertCaseString(phrase, Case.CamelCase);
        }

        internal string PascalCase(string phrase)
        {
            return ConvertCaseString(phrase, Case.PascalCase);
        }

        internal string MaskOverrides(string phrase)
        {
            return ProtectedNames.Contains(phrase) ? $"{phrase}Parameter" : phrase;
        }

        private static string ConvertCaseString(string phrase, Case cases)
        {
            var splittedPhrase = phrase.Split(' ', '-', '.', '_', ',', '\'', '%');
            var sb = new StringBuilder();

            if (phrase.StartsWith('_'))
            {
                sb.Append('_'); // MS Marks own Duplicate Names with _..
            }

            if (cases == Case.PascalCase)
            {
                sb.Append(splittedPhrase[0].ToLower());
                splittedPhrase[0] = string.Empty;
            }

            foreach (var item in splittedPhrase.Where(item => item.Length > 0))
            {
                sb.Append(item[0].ToString(CultureInfo.InvariantCulture).ToUpperInvariant());
                if (item.Length > 1)
                {
                    sb.Append(item.Substring(1));
                }
            }
            return sb.ToString();
        }

        #region Nested type: Case

        private enum Case
        {
            PascalCase,
            CamelCase
        }

        #endregion
    }
}
