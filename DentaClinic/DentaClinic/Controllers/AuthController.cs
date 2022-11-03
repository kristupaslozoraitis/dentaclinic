using DentaClinic.Auth;
using DentaClinic.Models;
using DentaClinic.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DentaClinic.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IJwtTokenService _jwtTokenService;

        public AuthController(UserManager<User> userManger, IJwtTokenService jwtTokenService )
        {
            _userManager = userManger;
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterUserDto registerUserDto)
        {
            var user = await _userManager.FindByEmailAsync(registerUserDto.Email);
            if(user != null)
            {
                return BadRequest("INvalid");
            }

            var newUser = new User
            {
                Email = registerUserDto.Email,
                UserName = registerUserDto.Email
            };

            var createUserResult = await _userManager.CreateAsync(newUser, registerUserDto.Password);
            if (!createUserResult.Succeeded)
            {
                return BadRequest("Invalid");
            }

            await _userManager.AddToRoleAsync(newUser, Roles.RegisteredUser);
            return CreatedAtAction(nameof(Register), new UserDto(newUser.Id, newUser.Email));
        }
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.Email);
            if (user == null)
            {
                return BadRequest("Invalid");
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!isPasswordValid)
            {
                return BadRequest("Invalid");
            }

            var roles = await _userManager.GetRolesAsync(user);
            var accessToken = _jwtTokenService.CreateAccessToken(user.Email, user.Id.ToString(), roles);

            return Ok(new SuccessfulLoginDto(accessToken));
        }
    }
}
