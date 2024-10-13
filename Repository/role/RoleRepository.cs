using HiManager.Connfiguration.db;
using HiManager.Model;
using Microsoft.EntityFrameworkCore;

namespace HiManager.Repository.role;

public class RoleRepository :IRoleRepository
{

    private readonly ApplicationDbContext _dbContext;

    public RoleRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        
    }
    
    public async Task<bool> SetRoleToUser(Role role)
    {
        var result = await _dbContext.Role.AddAsync(role);
        await _dbContext.SaveChangesAsync();
        return true;
    }


    public async Task<bool> RemoveAllRoles(int userID)
    {
        var result = await _dbContext.Role.Where(r => r.Uid == userID).ToListAsync();
        _dbContext.Role.RemoveRange(result);
        await _dbContext.SaveChangesAsync();
        return true; 
    }

    public async Task<List<User>> GetUsersByRoleName(string roleName)
    {
        var result = await _dbContext.User.Include(u => u.Roles)
            .Where(u=>u.Roles.Any(r=>r.RoleName == roleName))
            .ToListAsync();
        return result;
    }

    public async Task<Role> GetRoleByUidWithRoleName(int uid, string name)
    {
        return await _dbContext.Role.FirstOrDefaultAsync(r => r.Uid == uid && r.RoleName == name);
    }
}