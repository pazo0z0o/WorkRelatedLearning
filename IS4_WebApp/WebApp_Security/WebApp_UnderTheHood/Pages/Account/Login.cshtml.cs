using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Net;
using System.Security.Claims;

namespace WebApp_UnderTheHood.Pages.Account
{
    public class LoginModel : PageModel
    {
        //Avoid Null-Reference in the future by giving empty models and string.Empty , int = 0 , etc.
        [BindProperty]
        public Credentials Credential { get; set; } = new Credentials();
        public void OnGet()
        {


        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            { return Page(); }

            Credential.UserName.Trim();
            Credential.Password.Trim();

            if (Credential.UserName == "admin" && Credential.Password == "password")
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
                var identity = new ClaimsIdentity(claims, "MyCookieAuth");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);
               
                //very useful for customization of token,cookie behaviour
                var authenticationProperty = new AuthenticationProperties { IsPersistent = Credential.RememberMe };
                
                //tries to sign in with the security context given to the principal
                await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal,authenticationProperty);

                return RedirectToPage("/Index");
            }

            return Page();
        }
        public class Credentials
        {
            [Required]
            [Display(Description = "User Name")]
            public string UserName { get; set; } = String.Empty;
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; } = String.Empty;
            [Display(Name = "Remember Me")]
            public bool RememberMe { get; set; }



        }








    }
}
