using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Rummy.Server.Options;
using Rummy.Server.Services;
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
        private readonly AuthService _authService;

        public AuthController(IOptions<AppOptions> appOptions, AuthService authService)
        {
            _appOptions = appOptions;
            _authService = authService;
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
                if (!_authService.AreCredentialsValid(user)) return Unauthorized();

                var response = GenerateToken(user);
                return Ok(response);
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

            _authService.Register(user);

            var response = GenerateToken(user);
            return Ok(response);
        }

        private AuthResponse GenerateToken(User user)
        {
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

            var response = new AuthResponse
            {
                Email = user.Email,
                Token = tokenString
            };
            return response;
        }

    }
}