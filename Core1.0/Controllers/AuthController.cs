using Core1._0.Dtos.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Core1._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private UserManager<IdentityUser> _userManager;

        private SignInManager<IdentityUser> _signInManager;

        public AuthController(IConfiguration configuration, UserManager<IdentityUser> manager, SignInManager<IdentityUser> signInManager)
        {
            _configuration = configuration;
            _userManager = manager;
            _signInManager = signInManager;
        }

        #region Login jwt 登录授权
        /// <summary> 
        /// jwt 登录授权
        /// </summary>
        /// <returns></returns>h
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto logindto)
        {
            var result = await _signInManager.PasswordSignInAsync(logindto.UserName, logindto.Pwd, false, false);

            if (!result.Succeeded)
            {
                return BadRequest();
            }

            var userresult = await _userManager.FindByNameAsync(logindto.UserName);
            var sign = SecurityAlgorithms.HmacSha256;
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userresult.Id)

            };
            var rowsresult = await _userManager.GetRolesAsync(userresult);
            foreach (var item in rowsresult)
            {
                claims.Add(new Claim(ClaimTypes.Role, item));
            }
            var secrebyte = Encoding.UTF8.GetBytes(_configuration["Authentication:SecretKey"]);

            var signingkey = new SymmetricSecurityKey(secrebyte);
            var signingcredentials = new SigningCredentials(signingkey, sign);
            var token = new JwtSecurityToken(
                issuer: "lyc.com",
                audience: "lyc.com",
                claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddDays(1),
                 signingcredentials
                );
            var tokenstr = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(tokenstr);

        }
        #endregion

        #region Register 用户注册
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterDto registerDto)
        {
            var user = new IdentityUser
            {

                UserName = registerDto.UserName,
                Email = registerDto.Email
            };

            IdentityResult result = await _userManager.CreateAsync(user, registerDto.Password);
            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        #endregion
    }
}
