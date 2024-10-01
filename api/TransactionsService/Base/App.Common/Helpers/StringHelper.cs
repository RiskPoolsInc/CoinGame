using System.Globalization;
using System.Text.RegularExpressions;

namespace App.Common.Helpers;

public static class StringHelper {
    private static readonly Regex fromCamelStyleToPhraseRegex = new(@"(?<!^)(?=[A-Z])", RegexOptions.IgnorePatternWhitespace);

    public static string FromCamelStyleToPhrase(this string camelStyleString) {
        var parts = fromCamelStyleToPhraseRegex.Split(camelStyleString);
        return string.Join(' ', parts);
    }

    public static string FromCamelCaseToTitleCase(this string input) {
        return input switch {
            null => throw new ArgumentNullException(nameof(input)),
            ""   => throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input)),
            _    => string.Join(".", input.Split(".").Select(s => s.FirstCharToUpper()))
        };
    }

    private static string FirstCharToUpper(this string input) {
        return input switch {
            null => throw new ArgumentNullException(nameof(input)),
            ""   => throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input)),
            _    => input.First().ToString().ToUpper() + input.Substring(1)
        };
    }

    private static string _FromCamelCaseToTitleCase(this string str) {
        var textInfo = new CultureInfo("en-US", false).TextInfo;
        return textInfo.ToTitleCase(str);
    }
}