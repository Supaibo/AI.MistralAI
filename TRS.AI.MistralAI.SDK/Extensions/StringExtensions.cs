namespace TRS.AI.MistralAI.Extensions;

public static class StringExtensions
{
    public static string RemoveIfStartWith(this string text, string search)
    {
        var pos = text.IndexOf(search, StringComparison.Ordinal);
        return pos != 0 ? text : text.Substring(search.Length);
    }
}
