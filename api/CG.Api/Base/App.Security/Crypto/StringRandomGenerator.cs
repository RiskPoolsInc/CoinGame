using System.Security.Cryptography;
using System.Text;

namespace App.Security.Crypto;

public class StringRandomGenerator : IDisposable {
    public const string NumericAlphabet = "0123456789";
    public const string UrlAlphabet = "abcdefghijklmnopqrstuvwxyz0123456789";
    private const string DefaultAlphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

    private RandomNumberGenerator _randomNumberGenerator;

    public StringRandomGenerator() {
        _randomNumberGenerator = RandomNumberGenerator.Create();
    }

    public void Dispose() {
        _randomNumberGenerator?.Dispose();
        _randomNumberGenerator = null;
    }

    public string Generate(int length, string allowedChars = DefaultAlphabet) {
        if (length < 0)
            throw new ArgumentOutOfRangeException("length", "length cannot be less than zero.");

        if (string.IsNullOrEmpty(allowedChars))
            throw new ArgumentException("allowedChars may not be empty.");

        const int byteSize = 0x100;
        var allowedCharSet = new HashSet<char>(allowedChars).ToArray();

        if (byteSize < allowedCharSet.Length)
            throw new ArgumentException(string.Format("allowedChars may contain no more than {0} characters.", byteSize));
        var result = new StringBuilder();
        var buf = new byte[128];

        while (result.Length < length) {
            _randomNumberGenerator.GetBytes(buf);

            for (var i = 0; i < buf.Length && result.Length < length; ++i) {
                var outOfRangeStart = byteSize - byteSize % allowedCharSet.Length;

                if (outOfRangeStart <= buf[i])
                    continue;
                result.Append(allowedCharSet[buf[i] % allowedCharSet.Length]);
            }
        }

        return result.ToString();
    }
}