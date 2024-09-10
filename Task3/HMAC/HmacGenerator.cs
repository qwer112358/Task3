using System.Security.Cryptography;
using System.Text;

public sealed class HmacGenerator
{
    private byte[] _key;

    public HmacGenerator()
    {
        _key = GenerateKey();
    }

    public void GenerateNewKey()
    {
        _key = GenerateKey(); 
    }

    private byte[] GenerateKey()
    {
        const int KeyLengthInBytes = 32; // 256 bits = 32 bytes
        using (var rng = RandomNumberGenerator.Create())
        {
            var key = new byte[KeyLengthInBytes];
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
