using System.Data.SqlTypes;
using HiManager.Model;
using HiManager.Repository.user;

namespace HiManager.Service.user;

public class UserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        this._repository = repository;
    }


    public async Task<User> CreateUser(User user)
    {
        try
        {
            return await _repository.CreateUser(user);
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

    public async Task<User> GetUserById(int id)
    {
        try
        {
            return await _repository.GetUserById(id);
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }
    
    public async Task<List<User>> GetAllUsers()
    {
        try
        {
            return await _repository.GetAllUsers();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

    public async Task<User> GetUserByEmail(string email)
    {
        try
        {
            return await _repository.GetUserByEmail(email);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }
    
    public async Task<User> UpdateUser(User user)
    {
        try
        {
            return await _repository.UpdateUser(user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

    public async Task<User> DeleteUser(User user)
    {
        try
        {
            return await _repository.DeleteUser(user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }
}