using System.Security.Claims;
using HiManager.Model;
using HiManager.Repository.user;
using HiManager.Service.user;

namespace HiManager.Service.current_user;

public class CurrentUserService
{
    private readonly IHttpContextAccessor _accessor;
    private readonly UserService _userService;

    public CurrentUserService(IHttpContextAccessor accessor,UserService userService)
    {
        _accessor = accessor;
        _userService = userService;
    }

    private string GetCurrentUserID()
    {
        if (_accessor.HttpContext.User.Identity.IsAuthenticated)
        {
            return _accessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
        else
        {
            return null;
        }
    }

    public async Task<User> GetCurrentUser()
    {
        try
        { 
            return await _userService.GetUserById(Convert.ToInt32(GetCurrentUserID()));
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

}