using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Web_Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        public IConfiguration configuration { get; }

        public AuthController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public class Credentials
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }
        //-----------------------------------------------------------------------------------------------------
        [HttpPost]
        public IActionResult Authenticate([FromBody] Credentials credentials)
        {
            if (credentials.UserName == "admin" && credentials.Password == "pass")
            {
                //Creating security context

                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, "admin"),
                    new Claim(ClaimTypes.Email, "admin@mywebsite.com"),
                    new Claim("Department","HR"), //the claim needed for the HR department page entry
                    new Claim("Admin","true"),
                    new Claim("Manager","true"),
                    new Claim("EmploymentDate","2021-05-01")
                };

                //for use in the SignInAsync
                var expiresAt = DateTime.UtcNow.AddMinutes(30);

                return Ok(new
                {
                    access_token = CreateToken(claims,expiresAt),
                    expires_at = expiresAt,
                });
            }

            ModelState.AddModelError("Unauthorized", "You are not authorized to access the endpoint.");
            return Unauthorized(ModelState);
        }

        //-----------------------------------------------------------------------------------------------------
        private string CreateToken(IEnumerable<Claim> claims, DateTime expiresAt)
        {
            var secretKey = Encoding.ASCII.GetBytes(configuration.GetValue<string>("SecretKey"));//travigma apo appsettings
            //token object 
            var jwt = new JwtSecurityToken
            (
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: expiresAt,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(secretKey),
                    SecurityAlgorithms.HmacSha256Signature)
            );
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

    }
}
