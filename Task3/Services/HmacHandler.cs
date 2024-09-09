public class HmacHandler
{
    private readonly HmacGenerator _hmacGenerator;

    public string Key => _hmacGenerator.GetKey();

    public HmacHandler()
    {
        _hmacGenerator = HmacGenerator.Instance;
    }

    public void DisplayHmac(string message)
    {
        string hmac = _hmacGenerator.ComputeHmac(message);
        Console.WriteLine($"HMAC: {hmac}");
    }

    public string GenerateHmacVerificationUrl(string message)
    {
        string encodedMessage = Uri.EscapeDataString(message);
        string encodedKey = Uri.EscapeDataString(Key);
        return $"https://cryptii.com/pipes/hmac?key={encodedKey}&data={encodedMessage}&algorithm=HMACSHA256";
    }
}
