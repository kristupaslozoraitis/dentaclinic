using DentaClinic.Auth;
using DentaClinic.Models;
using DentaClinic.Models.Dtos;
using DentaClinic.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DentaClinic.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IPatientCardRepository _patientCards;

        public AuthController(UserManager<User> userManger, IJwtTokenService jwtTokenService, IPatientCardRepository patientCards)
        {
            _userManager = userManger;
            _jwtTokenService = jwtTokenService;
            _patientCards = patientCards;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterUserDto registerUserDto)
        {
            var user = await _userManager.FindByEmailAsync(registerUserDto.Email);
            if(user != null)
            {
                return BadRequest("Invalid");
            }

            var newUser = new User
            {
                Email = registerUserDto.Email,
                UserName = registerUserDto.Email,
                FirstName = registerUserDto.PatientCardData.Name,
                LastName = registerUserDto.PatientCardData.Surname
            };

            var createUserResult = await _userManager.CreateAsync(newUser, registerUserDto.Password);
            if (!createUserResult.Succeeded)
            {
                return BadRequest("Invalid");
            }

            var newCard = new PatientCard
            {
                Name = registerUserDto.PatientCardData.Name,
                Surname = registerUserDto.PatientCardData.Surname,
                BirthDate = registerUserDto.PatientCardData.BirthDate,
                PersonalNumber = registerUserDto.PatientCardData.PersonalNumber,
                HomeAddress = registerUserDto.PatientCardData.HomeAddress,
                PhoneNumber = registerUserDto.PatientCardData.PhoneNumber,
                Height = registerUserDto.PatientCardData.Height,
                Weight = registerUserDto.PatientCardData.Weight,
                UserId = newUser.Id
            };

            await _patientCards.Create(newCard);

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
        [HttpGet]
        [Route("me")]
        public async Task<ActionResult> GetRoles()
        {
            var userId = User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            var user = await _userManager.FindByIdAsync(userId);

            var userRoles = await _userManager.GetRolesAsync(user);
            return Ok(userRoles);
        }
    }
}
