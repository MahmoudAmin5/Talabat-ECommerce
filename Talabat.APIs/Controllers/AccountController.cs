using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTO;
using Talabat.APIs.Errors;
using Talabat.Core.Entities.Identity;

namespace Talabat.APIs.Controllers
{
    public class AccountController : BaseController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null) return Unauthorized(new ApiResponse(401));
            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (result.Succeeded) return Unauthorized(new ApiResponse(401));
            return Ok(new UserDto
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = "This Will be a token"

            });
        }
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var user = new AppUser()
            {
                Email = registerDto.Email,
                DisplayName = registerDto.DisplayName,
                PhoneNumber = registerDto.PhoneNumber,
                UserName = registerDto.Email.Split('@')[0],
            };
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded) return BadRequest(new ApiResponse(400));
            return Ok(new UserDto
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = "Will be Created "
            });

        }
    }
}
