using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Net;
using System.Security.Claims;


namespace WevAPP.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<IdentityUser> signInManager; //Different from the UserManagert on Register.cshtml

        public LoginModel(SignInManager<IdentityUser> signInManager)
        {
            this.signInManager = signInManager;
        }

        //Avoid Null-Reference in the future by giving empty models and string.Empty , int = 0 , etc.
        [BindProperty]
        public CredentialViewModel Credentials { get; set; } = new CredentialViewModel();
        public void OnGet()
        {


        }

        public async Task<IActionResult> OnPostAsync()
        {
            var result = await signInManager.PasswordSignInAsync(
                  this.Credentials.Email,
                  this.Credentials.Password,
                  this.Credentials.RememberMe, false);

            if (result.Succeeded)
            {
                return RedirectToPage("/Index");
            }
            else
            {
                if (result.IsLockedOut)
                {
                    ModelState.AddModelError("Login", "You are locked out, wait a few minutes and retry!");
                }
                else
                {
                    ModelState.AddModelError("Login", "Login failed");
                }



            }









        }

        public class CredentialViewModel
        {
            [Required]
            public string Email { get; set; } = String.Empty;
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; } = String.Empty;
            [Display(Name = "Remember Me")]
            public bool RememberMe { get; set; }
        }
    }
}
