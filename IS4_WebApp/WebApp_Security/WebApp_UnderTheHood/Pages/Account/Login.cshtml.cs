using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
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

        public async Task<IActionResult> onPostAsync()
        {
            if (!ModelState.IsValid)
            { return Page(); }

            if (Credential.UserName == "admin" && Credential.Password == "password")
            {
                //Creating Security Context:
                var claims = new List<Claim>{
                    

                    new Claim(ClaimTypes.Name, "admin"),
                    new Claim(ClaimTypes.Email, "admin@foo.com"),
                    new Claim("Department","HR") //Claim to be used in Authorization
                };
                var identity = new ClaimsIdentity(claims, "MyCookieAuth");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);
                //tries to sign in with the security context given to the principal
                await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);

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



        }








    }
}
