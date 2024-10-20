using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;

        //get the key from the appsettings
        public AuthController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost]
        public IActionResult Authenticate([FromBody] Credential credential)
        {
            //we are going to have a JWT token here, so we only need the claims here
            credential.UserName.Trim();
            credential.Password.Trim();

            if (credential.UserName == "admin" && credential.Password == "password")
            {
                //Creating Security Context:
                var claims = new List<Claim>{
                    new Claim(ClaimTypes.Name, "admin"),
                    new Claim(ClaimTypes.Email, "admin@foo.com"),
                    new Claim("Department","HR"), //Claim to be used in Authorization
                    new Claim("Admin","true"),
                    new Claim("Manager","true"),
                    new Claim("EmploymentDate","2024-07-23")
                };

                var expiresAt = DateTime.UtcNow.AddMinutes(10);

                return Ok(new
                {//function to generate a token
                    access_token = CreateToken(claims,expiresAt),
                    expires_at = expiresAt,
                });
            }

            ModelState.AddModelError("Unauthorized", "You are not authorized to access this page!");
            return Unauthorized(ModelState); //primitive error handling, try catch to the token generating function should be added 
        }

        //token generation and parametrization
        private string CreateToken(IEnumerable<Claim> claims, DateTime expireAt)
        {
            //get the JWT key from the appsettings and provide it as a KEY to the JwtSecurityToken.signingCredentials
            var secretKey = Encoding.ASCII.GetBytes(_config.GetValue<string>("SecretKey") ?? "");

            var jwt = new JwtSecurityToken(
               claims: claims,
               notBefore: DateTime.UtcNow,
               expires: expireAt,
               signingCredentials: new SigningCredentials(key: new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature)
               );
            //we let the handler write the token we parametrized above
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
        public class Credential
        {
        
        public string? UserName { get; set; }
        public string? Password { get; set; }
        
        }

    }
}
