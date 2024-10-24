using CustomIdentity.DTO;
using CustomIdentity.Jwt;
using CustomIdentity.Models;
using CustomIdentity.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using LoginRequest = CustomIdentity.Models.LoginRequest;
using RegisterRequest = CustomIdentity.Models.RegisterRequest;

namespace CustomIdentity.Controllers
{
    [Route("api/[controller]/v1")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var addUserDto = new AddUserDto { Email = request.Email, Password = request.Password };

            var result = await _userService.AddUser(addUserDto);

            if (result.IsSucceed)
            {
                return Ok(result.Message);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {

            var loginUserDto = new LoginUserDto { Email = request.Email, Password = request.Password };

            var result = await _userService.LoginUser(loginUserDto);

            if (!result.IsSucceed)
            {
                return BadRequest(result.Message);
            }
            
            var user =result.Data;

            var config = HttpContext.RequestServices.GetRequiredService<IConfiguration>();

            var token = JwtHelper.GenerateJwt(new JwtDto { Id = user.Id, Email = user.Email, UserRole = user.UserRole, SecretKey = config["Jwt:SecretKey"]!, Issuer = config["Jwt:Issuer"]!, Audience = config["Jwt:Audience"]!, ExpireMinutes = int.Parse(config["Jwt:ExpireMinutes"]!) });

            return Ok(new LoginResponse {Message="Login succesfull", Token=token });
        }



    }
}
