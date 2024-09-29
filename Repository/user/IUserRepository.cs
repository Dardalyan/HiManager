using HiManager.Model;

namespace HiManager.Repository.user;

public interface IUserRepository
{
    public Task<User> CreateUser(User user);
    public Task<User> UpdateUser(User user);
    public Task<User> DeleteUser(User user);
    public Task<User> GetUserById(int id);
    public Task<List<User>> GetAllUsers();
    public Task<User> GetUserByEmail(string email);
}