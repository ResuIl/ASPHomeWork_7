using System.Security.Cryptography;
using System.Text;

namespace ASPHomeWork_7.Encryptors;

public class SHA256Encryptor
{
    public static string Encrypt(string rawData)
    {
        using SHA256 sha256Hash = SHA256.Create();
        byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

        StringBuilder builder = new();
        for (int i = 0; i < bytes.Length; i++)
            builder.Append(bytes[i].ToString("x2"));

        return builder.ToString();
    }
}
