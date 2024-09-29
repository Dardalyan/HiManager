using System.Text.Json.Serialization;
using HiManager.encryption;

namespace HiManager.Model;

public class User
{
    
    
    public int Id { set; get; }
    public string Email { set; get; }

    private string password;
    
    [JsonIgnore]
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
    
    public string Name { set; get; }
    public string Surname { set; get; }   

    public User(string email, string password,string name, string surname)
    {
        this.Email = email;
        this.Password = password;
        this.Name = name;
        this.Surname = surname;
    }
    public User()
    {}

    public ICollection<Role> Roles { get; } = new List<Role>();

    public List<string> RolesToString()
    {
        List<string> roles = new List<string>();
        foreach (var role in Roles)
        {
            roles.Add(role.RoleName);
        }

        return roles;
    }

}


