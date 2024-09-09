using System.Security.Cryptography;
using System.Text;

public sealed class HmacGenerator
{
    private static readonly HmacGenerator _instance = new HmacGenerator();
    private const int KeyLengthInBytes = 32; // 256 bits = 32 bytes
    private byte[] _key;

    private HmacGenerator()
    {
        _key = GenerateKey(KeyLengthInBytes);
    }

    public static HmacGenerator Instance => _instance;

    private byte[] GenerateKey(int length)
    {
        using (var rng = RandomNumberGenerator.Create())
        {
            var key = new byte[length];
            rng.GetBytes(key);
            return key;
        }
    }

    public string ComputeHmac(string message)
    {
        using (var hmac = new HMACSHA256(_key))
        {
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            byte[] hmacBytes = hmac.ComputeHash(messageBytes);
            return BitConverter.ToString(hmacBytes).Replace("-", "");
        }
    }

    public string GetKey() => BitConverter.ToString(_key).Replace("-", "");
}

