using System.Numerics;
using System.Security.Cryptography;

using EllipticCurve;

namespace ExternalApiServices.Services; 

public class WalletService2 {
    private string screet = "4ipVPmwHLsNesxT4Xl&*";
    public BigInteger SecretNumber { set; get; }
    public PrivateKey PrivKey { set; get; }
    public PublicKey PubKey { set; get; }

    public void CreateKeys() {
        PrivKey = new PrivateKey();
        PubKey = PrivKey.publicKey();
        var tt2 = Convert.ToHexString(PubKey.toString());
        var hex = BitConverter.ToString(PubKey.toString());

        var dd = PrivKey.ToString();
        var dd2 = Convert.ToHexString(PrivKey.toString());
        var hash = SHA256.Create().ComputeHash(PubKey.toString());
        var strBase64 = Convert.ToBase64String(PubKey.toString(true));

        var addres = Convert.ToBase64String(hash);
    }
}