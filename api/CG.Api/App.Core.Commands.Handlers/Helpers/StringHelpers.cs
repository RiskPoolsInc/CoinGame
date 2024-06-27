using System.Text;

namespace App.Core.Commands.Handlers.Helpers {

public static class StringHelpers
{
    public static string AddSpaceBetweenUpperCharacters(this string text)
    {
        var result = new StringBuilder();
        for (var i = 0; i < text.Length; i++)
        {
            var symbol = text[i];
            if (char.IsUpper(symbol) && i > 0) result.Append(' ');

            result.Append(symbol);
        }

        return result.ToString();
    }
}
}