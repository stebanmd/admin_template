using App.API.Models;
using App.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace App.API.Controllers
{
    [Route("auth")]    
    public class AuthController : Controller
    {
        private readonly IConfiguration config;
        private readonly IAdminUserService service;

        public AuthController(IConfiguration config, IAdminUserService service)
        {
            this.config = config;
            this.service = service;
        }

        [HttpPost]
        public IActionResult SignIn([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = service.SignIn(model.Email, model.Password);

            if (user != null)
            {
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.UniqueName, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.GivenName, user.Name),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Prn, user.ProfileId.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                var signInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Auth:AppKey"]));

                var token = new JwtSecurityToken(
                    issuer: config["Auth:IssuerPath"],
                    audience: config["Auth:AudiencePath"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddDays(5),
                    signingCredentials: new SigningCredentials(signInKey, SecurityAlgorithms.HmacSha256)
                    );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            };

            return Unauthorized();
        }
    }
}