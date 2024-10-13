using HiManager.Model;

namespace HiManager.Repository.role;

public interface IRoleRepository
{
    public Task<bool> SetRoleToUser(Role role);
    public Task<Role> GetRoleByUidWithRoleName(int uid,string name);

    public Task<List<User>> GetUsersByRoleName(string roleName);
    public Task<bool> RemoveAllRoles(int userID);
}