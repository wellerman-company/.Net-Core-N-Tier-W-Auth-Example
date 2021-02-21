using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Biblioteca.Api.Resources;
using Biblioteca.Api.Resources.Auth;
using Biblioteca.Core.Models.Auth;
using Biblioteca.Core.Services;
using Biblioteca.Core.Services.Auth;
using Biblioteca.Core.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Biblioteca.Api.Controllers
{
    [Route("{culture:culture}/api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        // Dependency Injection

        // This UserManager handles all the methods that we need to handle the user
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;
        private IConfiguration _Configure;

        private JwtSettings _jwtSettings;
        private readonly ITokenService _tokenService; // Token Service
        private readonly IUserService _userService; // User Service


        public AuthController(IMapper mapper, UserManager<User> userManager, RoleManager<Role> roleManager,
            IConfiguration configuration, ITokenService tokenService, IUserService userService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenService = tokenService;
            _Configure = configuration;
            _userService = userService;
            _jwtSettings = _Configure.GetSection("Jwt").Get<JwtSettings>();
        }

        // This is for creating a new user
        // Also validates if the user exits
        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(UserSignUpResource userSignUpResource)
        {
            User userSignIn = new User();
            userSignIn.Email = userSignUpResource.Email;
            userSignIn.Name = userSignUpResource.Name;
            userSignIn.UserName = userSignUpResource.Email;
            userSignIn.State = userSignUpResource.State;

            var newUser = await _userService.Register(userSignIn, userSignUpResource.Password, userSignUpResource.UserRoles);

            if (newUser == null)
                return BadRequest(new { Status = "502", Message = "User already exists" });

            else
                return Ok(userSignIn);
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(UserLoginResource userLoginResource)
        {
            // Email exits ?
            var user = _userManager.Users.SingleOrDefault(u => u.Email == userLoginResource.Email);

            //List<UserRole> list = new List<UserRole>();
            //UserRole userRole = new UserRole();

            if (user is null)
            {
                return NotFound("User not found");
            }

            // Check Password
            var userSigninResult = await _userManager.CheckPasswordAsync(user, userLoginResource.Password);

            List<string> roles = new List<string>();

            if (userSigninResult)
            {
                TokenRequestResource newTokenRequest = new TokenRequestResource();
                newTokenRequest.Email = user.Email;
                newTokenRequest.Password = userLoginResource.Password;

                var listRoles = await _userManager.GetRolesAsync(user);

                foreach (var role in listRoles)
                {
                    Role newRole = new Role();
                    newRole.Name = role;
                    roles.Add(newRole.Name);
                }
                newTokenRequest.Roles=roles;

                var tokenRequest = _mapper.Map<TokenRequestResource, TokenRequest>(newTokenRequest);
                var result = await _tokenService.GetToken(tokenRequest, _jwtSettings);

                var tokenRequestResource = _mapper.Map<TokenRequest, TokenRequestResource>(result);

                return Ok(tokenRequestResource);
            }

            return BadRequest("Email or password incorrect.");
        }

        // Generates the token for the access 
        [HttpPost("Token")]
        public async Task<IActionResult> GenerateToken(TokenRequestResource tokenRequestResource)
        {
            var tokenRequest = _mapper.Map<TokenRequestResource, TokenRequest>(tokenRequestResource);

            var result = await _tokenService.GetToken(tokenRequest, _jwtSettings);
            SetRefreshTokenInCookie(result.RefreshToken);
            return Ok(result);
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var response = await _tokenService.RefreshTokenAsync(refreshToken, _jwtSettings);
            if (!string.IsNullOrEmpty(response.RefreshToken))
                SetRefreshTokenInCookie(response.RefreshToken);
            return Ok(response);
        }

        [HttpPost("RevokeToken")]
        public IActionResult RevokeToken(string requestToken)
        {
            // accept token from request body or cookie
            var token = requestToken ?? Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(token))
                return BadRequest(new { message = "Token is required" });

            var response = _tokenService.RevokeToken(token);

            if (!response.Result)
                return NotFound(new { message = "Token not found" });

            return Ok(new { message = "Token revoked" });
        }

        private void SetRefreshTokenInCookie(string refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(10),
            };
            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }
    }
}
