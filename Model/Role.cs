namespace HiManager.Model;

public class Role
{
    public enum ROLE {owner,manager,admin,moderator,employee}

    public int Uid { set; get; }
    public string RoleName { set; get; }

    public Role(int uid,ROLE name)
    {
        Uid = uid;
        RoleName = name.ToString();
    }
    
    public Role(){}

    public User? User;

    public static List<ROLE> AcceptableRoles()
    {
        return Enum.GetValues<ROLE>().ToList();
    }

    
    /// <summary>
    /**
        * IF the current user has any of these authorities {"owner","admin","manager","moderator" } returns true.  <br />
     */
    /// </summary>
    /// <param name="currentUser">The current authenticated user.</param>
    /// <returns>bool</returns>
    public static bool IsCurrentUserHasAuthority(User currentUser)
    {
        string[] authorities = {"owner","admin","manager","moderator" }; 
        if (authorities.Any(authority => currentUser.RolesToString().Contains(authority)))
            return true;
        return false;
    }
    
    /// <summary>
    /**
      * NOTE: A user can have more than 1 role. Thus we check the roles in this order.
                  ex: Having the admin role is enough to get full control. We don't need to check target user's roles. <br /> 
              
              * If the current user is owner or admin, it has full control over all other members even they have owner or admin roles.  <br /> 
              * If the current user is manager, and target user is not an owner or admin, it has full control over the target user. <br /> 
              * If the current user is moderator and target user is not an owner, admin or manager, it has full control over the target user. <br /> 
              * None of the above -> means the current user is employee -> doesn't have any control over any targets. <br /> 
     */
    /// </summary>
    /// <param name="currentUser">The current authenticated user.</param>
    /// <param name="targetUser">Target user for roles to be appointed. </param>
    /// <returns>bool</returns>
    public static bool CheckRoleHierarchyStatus(User currentUser,User targetUser)
    {
        if (currentUser.RolesToString().Contains(Role.ROLE.owner.ToString()))
            return true;
        if (currentUser.RolesToString().Contains(Role.ROLE.admin.ToString()))
            return true;
        if (currentUser.RolesToString().Contains(Role.ROLE.manager.ToString()) && !targetUser.RolesToString().Contains(Role.ROLE.owner.ToString()) && !targetUser.RolesToString().Contains(Role.ROLE.admin.ToString()))
            return true;
        if (currentUser.RolesToString().Contains(Role.ROLE.moderator.ToString()) && !targetUser.RolesToString().Contains(Role.ROLE.owner.ToString()) && !targetUser.RolesToString().Contains(Role.ROLE.admin.ToString()) && !targetUser.RolesToString().Contains(Role.ROLE.manager.ToString()))
            return true;
        
        return false;
    }

}