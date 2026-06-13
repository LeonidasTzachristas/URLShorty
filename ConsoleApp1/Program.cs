using System.Globalization;
using System.Security.Cryptography;
using System.Text;

// string urlLong = "https://google.com";
//
// var something = Encoding.UTF8.GetBytes(urlLong);
//
// byte[] hashBytes = SHA256.HashData(Encoding.UTF8.GetBytes(urlLong));
//
// // string hashed = Convert.ToHexString(hashBytes);
//
// foreach (var b in hashBytes)
// {
//     Console.WriteLine(b);
// }
// Console.WriteLine($"Count is {hashBytes.Length}");
// Console.WriteLine("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789".Length);

const string Base62 =
    "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

static string Generate(long id, string url)
{
    string idPart = ToBase62(id);
    Console.WriteLine(idPart);
    
    if (idPart.Length >= 7)
        return idPart;

    byte[] hash =
        SHA256.HashData(
            Encoding.UTF8.GetBytes($"{id}:{url}"));

    foreach (byte b in hash)
    {
        Console.Write($"{b} ");
    }
    Console.WriteLine();
    
    StringBuilder hashPart = new();

    foreach (byte b in hash)
    {
        hashPart.Append(Base62[b % 62]);

        if (idPart.Length + hashPart.Length >= 7)
            break;
    }

    return idPart + hashPart;
}

static string ToBase62(long value)
{
    if (value == 0)
        return "0";

    Span<char> buffer = stackalloc char[11];
    int pos = buffer.Length;

    while (value > 0)
    {
        buffer[--pos] = Base62[(int)(value % 62)];
        value /= 62;
    }

    return new string(buffer[pos..]);
}

var result = Generate(12345, "https://google.com");
Console.WriteLine($"Id: 12345 | URL: \"https://google.com\"\nShort URL: {result}", result);
result = Generate(45, "https://github.com");
Console.WriteLine($"Id: 45 | URL: \"https://github.com\"\nShort URL: {result}", result);