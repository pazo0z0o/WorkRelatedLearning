using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() { return View(); }

        [Authorize]
        public IActionResult Secret() 
        {
            return View(); 
        }

        [Authorize(Policy = "Claim.DoB")]
        public IActionResult SecretPolicy()
        {
            return View("Secret");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult SecretRole()
        {
            return View("Secret");
        }


        public IActionResult Authenticate() //authentication
        {   //Below we are creating a user object
            //Create Identity claims about who the user is and give some configurations and info 
            var grandmaClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,"Bob"),
                new Claim(ClaimTypes.Email, "Bob@fmail.com"),
                new Claim(ClaimTypes.DateOfBirth,"11/11/2000" ),
                new Claim(ClaimTypes.Role,"Admin"),
                new Claim("Grandma.Says", "Very Nice boi")
                
            };
            var licenseClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,"Bobomastoras"),
                new Claim("DrivingLicencse", "Max Verstapen lookalike")
            };
            //Identities take the above claims to form the user's identity
            var licenseIdentity = new ClaimsIdentity(licenseClaims,"Government");
            var grandmaIdentity = new ClaimsIdentity(grandmaClaims,"Grandma Identity");
           
            //you can Identify with many different identities as exhibited below
            var userPrincipal = new ClaimsPrincipal(new[] { grandmaIdentity,licenseIdentity }) ;

            HttpContext.SignInAsync(userPrincipal) ;

            return RedirectToAction("Index"); 
        }



    }
}
