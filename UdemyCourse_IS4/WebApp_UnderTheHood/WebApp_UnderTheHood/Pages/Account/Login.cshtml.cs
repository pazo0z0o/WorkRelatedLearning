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
        if(!ModelState.IsValid)  return Page();
            //Verify Credential
            if (Creds.UserName == "admin" && Creds.Password == "pass")
            {
                //Creating security context
              
                var claims = new List<Claim> { 
                    new Claim(ClaimTypes.Name, "admin"),
                    new Claim(ClaimTypes.Email, "admin@mywebsite.com")
                };
                var identity = new ClaimsIdentity(claims, "MyCookieAuth"); //created the identity with a cookie and claims
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity); //created the claims principal that takes our identity

              await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal); //serializes the claimsprincipal, encrypts it,
                                                                              //and saves it as a cookie
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

        }
    }
}
