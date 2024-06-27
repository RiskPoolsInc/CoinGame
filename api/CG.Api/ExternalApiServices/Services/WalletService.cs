using System.Security.Cryptography;

using Secp256k1Net;

namespace ExternalApiServices.Services; 

public static class Extensions {
    public static string ToHexString(this Span<byte> span) {
        return BitConverter.ToString(span.ToArray()).Replace("-", "").ToLowerInvariant();
    }

    public static byte[] HexToBytes(this string hexString) {
        var chars = hexString.Length;
        var bytes = new byte[chars / 2];

        for (var i = 0; i < chars; i += 2)
            bytes[i / 2] = Convert.ToByte(hexString.Substring(i, 2), 16);
        return bytes;
    }
}

public class WalletService {
    public void CreateKeys() {
        using (var secp256k1 = new Secp256k1()) {
            var dd = GenerateKeyPair(secp256k1);
            var pk = dd.PrivateKey.ToHexString();
            var pubk = dd.PublicKey.ToHexString();

            Span<byte> pp = new byte[65];
            var ttt = pp.ToHexString();
        }
    }

    private Span<byte> GeneratePrivateKey(Secp256k1 secp256k1) {
        var rnd = RandomNumberGenerator.Create();
        Span<byte> privateKey = new byte[32];

        do {
            rnd.GetBytes(privateKey);
        } while (!secp256k1.SecretKeyVerify(privateKey));

        return privateKey;
    }

    private KeyPair GenerateKeyPair(Secp256k1 secp256k1) {
        var privateKey = GeneratePrivateKey(secp256k1);
        Span<byte> publicKey = new byte[64];

        if (!secp256k1.PublicKeyCreate(publicKey, privateKey))
            throw new Exception("Public key creation failed");
        return new KeyPair { PrivateKey = privateKey, PublicKey = publicKey };
    }

    private ref struct KeyPair {
        public Span<byte> PrivateKey;
        public Span<byte> PublicKey;
    }
}