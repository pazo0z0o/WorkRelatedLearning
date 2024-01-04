using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Net;
using System.Security.Claims;

namespace WebApp_UnderTheHood.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Credential Credentials { get; set; } = new Credential();


        public void OnGet()
        { }

        public async Task<IActionResult> OnPostAsync()
        {   // standard proccess -- If ModelState is invalid, take specific action
            if (!ModelState.IsValid) return Page();

            if (Credentials.UserName == "admin" && Credentials.Password == "pass")
            {
                //Now we create the security context -- Claims,Identity,Policies and all 
                var claims = new List<Claim> {
                new Claim(ClaimTypes.Name,"admin"),
                new Claim(ClaimTypes.Email, "admin@mywebsite.com"),
                new Claim("Department","HR")
                };
                //Identity part of the sec. context
                var identity = new ClaimsIdentity(claims, "MyCookieAuth");

                //Claims Principal -- The principal will contain the httpContext 
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);

                return RedirectToPage("/Index");

            }

            return Page();
        }

    }
        public class Credential
        {
            [Required]
            [Display(Name = "User Name")]
            public string UserName { get; set; } = string.Empty;

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; } = string.Empty;


        }
}
