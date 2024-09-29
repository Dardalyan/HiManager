using HiManager.Model;
using HiManager.Repository.role;

namespace HiManager.Service.role;

public class RoleService
{
    private readonly IRoleRepository _roleRepository;

    public RoleService(IRoleRepository repository)
    {
        _roleRepository = repository;
    }
    
    public async Task<bool> SetRoleToUserByUid(Role role)
    {
        try
        {
            await _roleRepository.SetRoleToUser(role);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
    }
    
    public async Task<List<User>> GetUsersByRoleName(string roleName)
    {
        try
        {
            return await _roleRepository.GetUsersByRoleName(roleName);
        }
        catch(Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public async Task<Role> GetRoleByUidWithRoleName(int uid, string name)
    {
        try
        {
            return await _roleRepository.GetRoleByUidWithRoleName(uid, name);
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }
}