using HiManager.DTO;
using HiManager.Model;
using HiManager.Service.current_user;
using HiManager.Service.role;
using HiManager.Service.user;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace HiManager.Controller;

public class RoleController : ControllerBase
{
    private readonly UserService _userService;
    private readonly RoleService _roleService;
    private readonly CurrentUserService _currentUserService;

    public RoleController(UserService userService, CurrentUserService currentUserService,RoleService roleService)
    {
        _currentUserService = currentUserService;
        _userService = userService;
        _roleService = roleService;
    }
    
    // ADD a new role or roles to target user !
    [HttpPost]
    [Route(("api/user/management/add-role"))]
    public async Task<IResult> AddRole([FromBody] UserRoleDTO userRoleDto)
    {
        Dictionary<string, object> response = new Dictionary<string, object>();
        
        User current_user = await _currentUserService.GetCurrentUser();
        User target_user = await _userService.GetUserById(userRoleDto.Uid);

        if (current_user == null)
            return Results.Unauthorized();
        if (target_user == null)
        {
            response.Add("alert",$"User cannot be found with id: {userRoleDto.Uid}.");
            return Results.NotFound(response);
        }
        
        if (Role.CheckRoleHierarchyStatus(current_user, target_user))
        {
            // FIRST REMOVES ALL ROLES , THEN ADDS THE ROLES ACCORDING TO GIVEN LIST,
            // UNLESS YOU WANT TO LOSE YOUR ROLES, OLD ROLES MUST BE PROVIDED AGAIN !
            await _roleService.RemoveAllRoles(target_user.Id); 
            foreach (var role in userRoleDto.Roles)
            {
                switch (role)
                {
                    case "owner":
                        await _roleService.SetRoleToUserByUid(new Role(target_user.Id, Role.ROLE.owner));
                        break;
                    case "manager":
                        await _roleService.SetRoleToUserByUid(new Role(target_user.Id, Role.ROLE.manager));
                        break;
                    case "admin":
                        await _roleService.SetRoleToUserByUid(new Role(target_user.Id, Role.ROLE.admin));
                        break;
                    case "moderator":
                        await _roleService.SetRoleToUserByUid(new Role(target_user.Id, Role.ROLE.moderator));
                        break;
                    case "employee":
                        await _roleService.SetRoleToUserByUid(new Role(target_user.Id, Role.ROLE.employee));
                        break;
                    default:
                        return Results.BadRequest($"Acceptable Roles -> {String.Join(", ",Role.AcceptableRoles())}");
                }
            }
            response.Add("message","Roles: {"+ $"{String.Join(",",userRoleDto.Roles)}"+"} have been appointed succesfully ! "); 
            return Results.Ok(response);
        }
        else
        {
            response.Add("message","You are not allowed !");  
            return Results.BadRequest(response);
        }

    }
    
    
    // Get All Users With given role names !
    [HttpGet]
    [Route(("api/user/management/with-role/{roleName}"))]
    public async Task<IResult> GetAllUsersByRoleName(string roleName)
    {
        Dictionary<string, object> response = new Dictionary<string, object>();
        User currentUser = await _currentUserService.GetCurrentUser();

        if (Role.IsCurrentUserHasAuthority(currentUser))
        {
            List<User> users = await _roleService.GetUsersByRoleName(roleName);
            response.Add("users",users);
            return Results.Ok(response);

        }
        response.Add("alert","You are not allowed !");
        return Results.BadRequest(response);

    }
    
    

    
}