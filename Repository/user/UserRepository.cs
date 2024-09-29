using HiManager.Connfiguration.db;
using HiManager.Model;
using Microsoft.EntityFrameworkCore;

namespace HiManager.Repository.user;

public class UserRepository :IUserRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UserRepository(ApplicationDbContext dbContext)
    {
        this._dbContext = dbContext;
    }

    public async Task<User> CreateUser(User user)
    {
        var result = await _dbContext.User.AddAsync(user);
        await _dbContext.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<User> UpdateUser(User user)
    {
        var result =  _dbContext.User.Update(user);
        await _dbContext.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<User> GetUserById(int id)
    {
        return await _dbContext.User.Include(user=>user.Roles).FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User> GetUserByEmail(string email)
    {
        return await _dbContext.User.Include(user=>user.Roles).FirstOrDefaultAsync(u=>u.Email == email);
    }

    public async Task<List<User>> GetAllUsers()
    {
        return await _dbContext.User.Include(user=>user.Roles).ToListAsync();
    }

    public async Task<User> DeleteUser(User user)
    {
        _dbContext.User.Remove(user);
        await _dbContext.SaveChangesAsync();
        return user;
    }
}