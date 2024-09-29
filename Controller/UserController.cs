using System.Security.Claims;
using HiManager.DTO;
using HiManager.Model;
using HiManager.Service.current_user;
using HiManager.Service.role;
using HiManager.Service.user;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Utilities;

namespace HiManager.Controller;

[Authorize]
[ApiController]
public class UserController : ControllerBase
{

    private readonly UserService _userService;
    private readonly RoleService _roleService;
    private readonly CurrentUserService _currentUserService;

    public UserController(UserService userService, CurrentUserService currentUserService,RoleService roleService)
    {
        _currentUserService = currentUserService;
        _userService = userService;
        _roleService = roleService;
    }

    // CREATE User by given fields.
    [HttpPost]
    [Route("api/user/create")]
    public async Task<IResult> CreateUser([FromBody] UserDTO user)
    {
        User current_user = await _currentUserService.GetCurrentUser(); 
        Dictionary<string,object> response = new Dictionary<string,object>();
        if (!user.HasNull())
        {
            if (Role.IsCurrentUserHasAuthority(current_user))
            {
                
                User newUser = new User();
                foreach (var pDTO in user.GetType().GetProperties())
                {
                    foreach (var p in newUser.GetType().GetProperties())
                    {
                        if (pDTO.Name == p.Name)
                        {
                            p.SetValue(newUser,pDTO.GetValue(user));
                        }
                    }
                }

                newUser = await _userService.CreateUser(newUser);
                if (newUser != null)
                {
                    response.Add("message","User has been created successfully !");
                    return Results.Ok(response);
                }
                else
                {
                    response.Add("alert","User cannot be created !");
                    response.Add("message","DB error has occured !");
                    return Results.BadRequest(response);
                }
            }
            else
            {
                response.Add("alert","You are not allowed !");
                return Results.BadRequest(response);
            }
        }
        else
        {
            response.Add("alert",$"Please give the required fields -> {string.Join(", ", user.ShowNullProps())}, " );
            return Results.BadRequest(response);
        }
    }
    
    // UPDATE user by given fields. 
    // NOTE: User email cannot be changed once it is appointed. 
    [HttpPut]
    [Route("api/user/update")]
    public async Task<IResult> UpdateUser([FromBody] UserDTO user)
    {
        Dictionary<string, object> response = new Dictionary<string, object>();

        if (user.Email == null || user.Email.Equals("".Trim()))
        {
            response.Add("alert","Email field is required !");
            return Results.BadRequest(response);
        }

        User currentUser = await _currentUserService.GetCurrentUser();
        User targetUser = await _userService.GetUserByEmail(user.Email);

        if (Role.CheckRoleHierarchyStatus(currentUser, targetUser))
        {
            foreach (var propDTO in user.GetType().GetProperties())
            {
                foreach (var propTU in targetUser.GetType().GetProperties())
                {
                    if (propDTO.Name == propTU.Name && propDTO.GetValue(user) != null)
                    {
                        propTU.SetValue(targetUser,propDTO.GetValue(user));
                    }
                }
            }

            targetUser = await _userService.UpdateUser(targetUser);
            if (targetUser != null)
            {
                response.Add("message","User has been updated !");
                response.Add("updated_user",targetUser);
                return Results.Ok(response);
            }
            else
            {
                response.Add("alert","User cannot be updated !");
                return Results.BadRequest(response);
            }
            
        }
        
        response.Add("alert","You are not allowed !");
        return Results.BadRequest(response);
    }
    
    
    // DELETE user by user id
    [HttpDelete]
    [Route("api/user/{id:int}")]
    public async Task<IResult> DeleteUser(int id)
    {
        Dictionary<string, object> response = new Dictionary<string, object>();
        
        User user = await _userService.GetUserById(id);
        User currentUser = await _currentUserService.GetCurrentUser();

        if (Role.CheckRoleHierarchyStatus(currentUser,user))
        {
            if (user == null)
            {
                response.Add("alert",$"User cannot be found with id:{id} !");
                return Results.NotFound(response);
            }
            else
            {
                await _userService.DeleteUser(user);
                response.Add("deleter_user",user);
                return Results.Ok(response);
            }
        }
        else
        {
            response.Add("alert","You are not allowed !");
            return Results.BadRequest(response);
        }
        
    }
    
    // GET all users.
    // If current user has authority (is not a just employee), c_u can get all the users information
    [HttpGet]
    [Route("api/user/all")]
    public async Task<IResult> AllUsers()
    {
        Dictionary<string, object> response = new Dictionary<string, object>();
        
        User currentUser = await _currentUserService.GetCurrentUser();
        if (Role.IsCurrentUserHasAuthority(currentUser))
        {
            List<User> allUsers = await _userService.GetAllUsers();
            response.Add("users",allUsers);
            return Results.Ok(response);
        }
        else
        {
            response.Add("alert","You are not allowed !");
            return Results.BadRequest(response);
        }
    }

    
    // GET user by user id.
    // If the current user has authority, then can see the user.
    [HttpGet]
    [Route("api/user/{id:int}")]
    public async Task<IResult> GetUserById(int id)
    {
        Dictionary<string, object> response = new Dictionary<string, object>();
        
        User user = await _userService.GetUserById(id);
        User currentUser = await _currentUserService.GetCurrentUser();
        
        if (user == null)
        {
            response.Add("alert",$"User cannot be found with id {id} !");
            return Results.NotFound(response);
        }
        else
        {
            if (Role.IsCurrentUserHasAuthority(currentUser))
            {
                response.Add("user",user);
                return Results.Ok(response);
            }
            else
            {
                response.Add("alert","You are not allowed !");
                return Results.BadRequest(response);
            }
        }
        
    }
}