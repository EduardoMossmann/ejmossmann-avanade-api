using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AvanadeBlog.Application.Middleware;
using AvanadeBlog.Domain.Roles;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AvanadeBlog.Api.Controllers
{
    /// <summary>
    /// Login Controller
    /// </summary>
    [ApiController]
    [Route("api/login")]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Login Controller
        /// </summary>
        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Login endpoint. Only email and password is required.3FW
        /// </summary>
        [HttpPost]
        public IActionResult Login([FromBody] LoginRequest model)
        {

            var jwtKey = _configuration["Jwt:Key"];
            var jwtIssuer = _configuration["Jwt:Issuer"];
            var jwtAudience = _configuration["Jwt:Audience"];
            if (jwtKey == null || jwtIssuer == null || jwtAudience == null)
            {
                throw new UnauthorizedException("Error during authentication.");
            }

            var credentialsSection = _configuration.GetSection("Jwt:Crendentials");
            foreach (var credential in credentialsSection.GetChildren())
            {
                var storedLogin = credential["Login"];
                var storedPassword = credential["Password"];
                var userType = credential["Type"];

             
                if (model.Email == storedLogin && model.Password == storedPassword)
                { 
                    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
                    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                    var roleClaim = userType == IdentityUserAccessRoles.ADMIN ? IdentityUserAccessRoles.ADMIN : IdentityUserAccessRoles.USER;

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, model.Email),
                        new Claim(ClaimTypes.Email, model.Email),
                        
                    };

                    if (userType == IdentityUserAccessRoles.ADMIN) claims.Add(new Claim(ClaimTypes.Role, IdentityUserAccessRoles.ADMIN));

                    claims.Add(new Claim(ClaimTypes.Role, IdentityUserAccessRoles.USER));

                    var token = new JwtSecurityToken(
                        issuer: jwtIssuer,
                        audience: jwtAudience,
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(30),
                        signingCredentials: credentials);

                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token)
                    });
                }
            }
            return Unauthorized();
            
        }
    }
}
