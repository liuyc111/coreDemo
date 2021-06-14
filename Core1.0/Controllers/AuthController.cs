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

namespace Core1._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        /// <summary> 
        /// jwt 登录授权
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login(LoginDto logindto)
        {

            ///验证用户名密码
            ///todo
            var sign = SecurityAlgorithms.HmacSha256;
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, "user_id")
               //, new Claim(ClaimTypes.Role,"admin")
            };
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
    }
}
