using System.Security.Cryptography;
using System.Text;

namespace HiManager.encryption;

public class PasswordEncryption
{
    public static string EncryptPasswordSHA256(string str)
    {
        SHA256 sha256 = SHA256Managed.Create();
        byte[] hashValue;
        UTF8Encoding objUtf8 = new UTF8Encoding();
        hashValue = sha256.ComputeHash(objUtf8.GetBytes(str));

        return Convert.ToBase64String(hashValue);
    }
}