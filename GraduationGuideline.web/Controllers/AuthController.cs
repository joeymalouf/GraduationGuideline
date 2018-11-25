using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GraduationGuideline.domain.interfaces;
using GraduationGuideline.domain.models;
using GraduationGuideline.domain.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Linq;

namespace GraduationGuideline.web.Controllers
{
    public class AuthController : Controller
    {
        private IAccountService _accountService;
        
        public AuthController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        // GET api/values
        [AllowAnonymous]
        [HttpPost, Route("api/auth/login")]
        public async Task<IActionResult> Login([FromBody] LoginDto user)
        {
            if (user == null)
            {
                return BadRequest("Invalid client request");
            }

            var result = await _accountService.Login(user).ConfigureAwait(false);

            if (result.Username != null)
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("GoodBeanJuiceTasteLikeChocolateMakeMeGoFast"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var Admin = result.Admin;

                var claims = new List<Claim>();
                if (Admin){
                    claims.Add(new Claim(ClaimTypes.Name, user.Username));
                    claims.Add(new Claim(ClaimTypes.Role, "Admin"));
                }
                else {
                    claims.Add(new Claim(ClaimTypes.Name, user.Username));
                }

                var tokenOptions = new JwtSecurityToken(
                    issuer: "https://localhost:5001",
                    audience: "https://localhost:5001",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(20),
                    signingCredentials: signinCredentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return Ok(new { Token = tokenString });
            }
            else
            {
                return Unauthorized();
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/[controller]/create")]
        public async Task<IActionResult> CreateAccount([FromBody] FullUserDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            user.Admin = false;
            var result = await this._accountService.CreateAccount(user).ConfigureAwait(false);
            if (!result) {
                return BadRequest(ModelState);
            }
            
            var loginResult = await Login(new LoginDto { Username = user.Username, Password = user.Password});

            return loginResult;
        }
    }
}