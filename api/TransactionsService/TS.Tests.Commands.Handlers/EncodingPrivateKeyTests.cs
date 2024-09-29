using System.Security.Cryptography;
using System.Text;

using NUnit.Framework;

namespace CG.Tests.Commands.Handlers;

[TestFixture]
public class EncodingPrivateKeyTests {
    private static string _plainText = "396f5d3530ddf0946c2bdd891887f499e7902bbf0313eeb998abfda353d0994f";
    private static string _key = "396f5d3530ddf0946c2bdd891887f499e";
    
    [Test]
    public void EncodingPrivateKey() {
        var plainText = _plainText;
        var result = Encrypt(plainText, _key);
        Assert.AreNotEqual(plainText, result);
    }
    [Test]
    public void DecodingPrivateKey() {
        var encrypted = Encrypt(_plainText, _key);
        var decrypted = Decrypt(encrypted, _key);
        Assert.AreEqual(_plainText, decrypted);
    }

    private string Decrypt(string cipherText, string keyToUse) {
        var iv = new byte[16];
        var buffer = Convert.FromBase64String(cipherText);

        using (var aes = Aes.Create()) {
            aes.Key = Encoding.UTF8.GetBytes(Get128BitString(keyToUse));
            aes.IV = iv;
            var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using (var memoryStream = new MemoryStream(buffer)) {
                using (var cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read)) {
                    using (var streamReader = new StreamReader((Stream)cryptoStream)) {
                        return streamReader.ReadToEnd();
                    }
                }
            }
        }
    }

    private string Encrypt(string plainText, string key) {
        var iv = new byte[16];
        byte[] array;

        using (var aes = Aes.Create()) {
            aes.Key = Encoding.UTF8.GetBytes(Get128BitString(key));
            aes.IV = iv;

            var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using (var memoryStream = new MemoryStream()) {
                using (var cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write)) {
                    using (var streamWriter = new StreamWriter((Stream)cryptoStream)) {
                        streamWriter.Write(plainText);
                    }

                    array = memoryStream.ToArray();
                }
            }
        }

        var result = Convert.ToBase64String(array);
        return result;
    }

    private string Get128BitString(string keyToConvert) {
        var b = new StringBuilder();

        for (var i = 0; i < 16; i++)
            b.Append(keyToConvert[i % keyToConvert.Length]);

        keyToConvert = b.ToString();

        return keyToConvert;
    }
}