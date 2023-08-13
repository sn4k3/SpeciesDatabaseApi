using System.Text;

namespace SpeciesDatabaseApi.Extensions;

public static class StringExtensions
{
    /// <summary>
    /// Prepend a space on a found Upper char, first char excluded.
    /// </summary>
    /// <returns></returns>
    public static string PrependSpaceByUpperChar(string str)
    {
        if (str.Length <= 1) return str;
        var sb = new StringBuilder();

        sb.Append(str[0]);
        for (var i = 1; i < str.Length; i++)
        {
            if (char.IsUpper(str[i])) sb.Append(' ');
            sb.Append(str[i]);
        }

        return sb.ToString();
    }
}