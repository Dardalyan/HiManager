using HiManager.DTO;
using HiManager.Model;
using HiManager.Service.jwt;
using HiManager.Service.user;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HiManager.Controller;

public class AuthController: ControllerBase
{
    
    private readonly UserService _service;
    private readonly ITokenService _tokenService;

    public AuthController(UserService service,ITokenService tokenService)
    {
        this._service = service;
        _tokenService = tokenService;
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("api/auth/login")]
    public async Task<IResult> Login([FromBody] AuthDTO authUser)
    {
        Dictionary<string, object> response = new Dictionary<string, object>();

        User user = await _service.GetUserByEmail(authUser.Email);

        
         if (user.Password == authUser.Password)
           {
               string jwt = _tokenService.GenerateJWTToken(user); 
               response.Add("token",jwt);
               return Results.Ok(response);
           }
         
        response.Add("alert","Email or Password is not correct !");
        return Results.BadRequest(response);
    }
}