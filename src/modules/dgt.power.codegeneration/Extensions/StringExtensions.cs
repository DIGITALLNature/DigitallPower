namespace dgt.power.codegeneration.Extensions;

public static class StringExtensions
{
    public static string ReplaceAll(this string value, IEnumerable<char> oldChars, char newChar)
    {
        return oldChars.Aggregate(value, (str, character) => str.Replace(character, newChar));
    }
}