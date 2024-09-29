namespace HiManager.DTO;

public class UserDTO
{
    public string? Email { set; get; } 
    public string? Password { set; get; }
    public string? Name { set; get; }
    public string? Surname { set; get; }

    public UserDTO(string email, string password,string name, string surname)
    {
        this.Email = email;
        this.Password = password;
        this.Name = name;
        this.Surname = surname;
    }

    public bool HasNull()
    {
        bool hasNull = false;
        foreach (var prop in this.GetType().GetProperties())
        {
            if (prop.GetValue(this) == null)
            {
                hasNull = true;
                break;
            }
        }

        return hasNull;
    }
    
    public List<string> ShowNullProps()
    {
        List<string> nullProps = new List<string>(); 
        
        foreach (var prop in this.GetType().GetProperties())
        {
            if (prop.GetValue(this) == null)
            {
                nullProps.Add(prop.Name.ToLower());
            }
        }

        return nullProps;
    }
    
    
}