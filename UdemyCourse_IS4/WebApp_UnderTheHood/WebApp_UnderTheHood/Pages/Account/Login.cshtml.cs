using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace WebApp_UnderTheHood.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Credential Creds { get; set; }

        [HttpPost]
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            //Verify Credential
            if (Creds.UserName == "admin" && Creds.Password == "pass")
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
                var identity = new ClaimsIdentity(claims, "MyCookieAuth"); //created the identity with a cookie and claims
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity); //created the claims principal that takes our identity

                //for use in the SignInAsync
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = Creds.RememeberMe  //Remember me checkbox that modifies the coockie lifetime
                };
                
                await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal, authProperties); //serializes the claimsprincipal, encrypts it,                                                                               //and saves it as a cookie
                return RedirectToPage("/Index");
            }
            return Page();
        }

        public void OnGet()
        {


        }


        public class Credential
        {
            [Required]
            [Display(Name = "User Name")]
            public string UserName { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember Me")]
            public bool RememeberMe { get; set; }
        }
    }
}
