using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Rummy.Server.Options;
using Rummy.Shared.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Rummy.Server.Controllers
{
    [Route("api/[controller]"), ApiController, AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IOptions<AppOptions> _appOptions;

        public AuthController(IOptions<AppOptions> appOptions)
        {
            _appOptions = appOptions;
        }

        [HttpPost, Route("login")]
        public IActionResult Login([FromBody]User user, string returnUrl = null)
        {
            if (user == null)
            {
                return BadRequest("Invalid client request");
            }

            try
            {
                //todo: check if credentials are corect
                //if (!_authService.AreCredentialsValid(user)) return Unauthorized();

                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appOptions.Value.JwtSecretKey));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var tokenOptions = new JwtSecurityToken(
                    issuer: _appOptions.Value.AppDns,
                    audience: _appOptions.Value.AppDns,
                    claims: new List<Claim> { new Claim("email", user.Email) },
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: signinCredentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                return Ok(new AuthResponse
                {
                    Email = user.Email,
                    Token = tokenString
                });
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [HttpPost, Route("register")]
        public IActionResult Register([FromBody]User user)
        {
            if (user == null)
            {
                return BadRequest("Invalid client request");
            }
            //Todo: add user to db
            //_authService.Register(user);
            return Ok();
        }
    }
}