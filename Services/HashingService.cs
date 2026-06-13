using System.Security.Cryptography;
using System.Text;
using ServiceContracts;

namespace Services;

public class HashingService : IHashingService
{
    private const string Alphabet = 
        "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    
    public string HashUrl(string urlLong, long number)
    {
        string idPart = ToBase62(number);

        if (idPart.Length >= 7)
            return idPart;

        byte[] hash =
            SHA256.HashData(
                Encoding.UTF8.GetBytes($"{number}:{urlLong}"));

        StringBuilder hashPart = new();

        foreach (byte b in hash)
        {
            hashPart.Append(Alphabet[b % 62]);

            if (idPart.Length + hashPart.Length >= 7)
                break;
        }

        return idPart + hashPart;
    }
    
    private static string ToBase62(long value)
    {
        if (value == 0)
            return "0";

        Span<char> buffer = stackalloc char[11];
        int pos = buffer.Length;

        while (value > 0)
        {
            buffer[--pos] = Alphabet[(int)(value % 62)];
            value /= 62;
        }

        return new string(buffer[pos..]);
    }
}