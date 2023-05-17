using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using API.Extensions;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _config;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;

        public AccountController(ILogger<AccountController> logger,
        SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, ITokenService tokenService
        , Microsoft.AspNetCore.Hosting.IHostingEnvironment environment,
        IConfiguration config,
        RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
            _environment = environment;
            _config = config;
            _roleManager = roleManager;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null) return Unauthorized(new ApiResponse(401));

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized(new ApiResponse(401));

            var roles = await _userManager.GetRolesAsync(user);
            var isAdmin = false;
            if (roles.Contains("Administrator"))
            {
                // var role = await _roleManager.FindByNameAsync("Administrator");
                //  await _userManager.AddToRoleAsync(user, role.Name);
                isAdmin = true;
            }
            return new UserDto
            {
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
                DisplayName = user.DisplayName,
                AvatarUrl = user.AvatarUrl,
                isAdmin = isAdmin
            };
        }

        // [HttpGet("isadmin")]
        // [Authorize]
        // public async Task<ActionResult<UserDto>> CheckIfAdmin(){
        //     var user = await _userManager.FindByEmailFromClaimsPrincipal(HttpContext.User);
        //    return Ok(User.IsInRole("Administrator"));
        // }

        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (CheckEmailExistsAsync(registerDto.Email).Result.Value)
            {
                return new BadRequestObjectResult(
                    new ApiValidationErrorResponse { Errors = new[] { "Email address is in use" } });
            }
            byte[] bytes = Convert.FromBase64String(registerDto.Image.Substring(registerDto.Image.LastIndexOf(',') + 1));
            if (bytes?.Length > 0)
            {
                var folderName = "images";
                var fileName = string.Format(@"{0}.png", Guid.NewGuid());
                var pathToSave = Path.Combine(_environment.WebRootPath, folderName, fileName);

                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    Image pic = Image.FromStream(ms);

                    pic.Save(pathToSave);
                }
                var resultPath = _config["ApiUrl"] + Path.Combine(folderName, fileName).Replace(@"\\", @"/");
                registerDto.AvatarUrl = resultPath;
            }

            var user = new AppUser
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.DisplayName,
                AvatarUrl = registerDto.AvatarUrl
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            var role = await _roleManager.FindByNameAsync("Subscriber");
            await _userManager.AddToRoleAsync(user, role.Name);
            if (!result.Succeeded) return BadRequest(new ApiResponse(400));

            return new UserDto
            {
                DisplayName = user.DisplayName,
                Token = _tokenService.CreateToken(user),
                Email = user.Email,
                AvatarUrl = registerDto.AvatarUrl
            };
        }
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("getcurrentuser")]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var user = await _userManager.FindByEmailFromClaimsPrincipal(HttpContext.User);
            var roles = await _userManager.GetRolesAsync(user);

            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                DisplayName = user.DisplayName,
                AvatarUrl = user.AvatarUrl,
                isAdmin = roles.Contains("Administrator")
            };
        }
    }
}