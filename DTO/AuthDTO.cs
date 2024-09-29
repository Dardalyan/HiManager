using HiManager.encryption;

namespace HiManager.DTO;

public class AuthDTO
{
    private string password;

    public string Email
    {
        set;
        get;
    }

    public string Password
    {
        set
        {
            password = PasswordEncryption.EncryptPasswordSHA256(value);
        }

        get
        {
            return password;
        }
    }
}