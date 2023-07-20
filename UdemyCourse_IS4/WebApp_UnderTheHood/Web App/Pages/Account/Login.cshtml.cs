using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Web_App.Pages.Account
{
    public class LoginModel : PageModel
    {
        public LoginModel(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [BindProperty]
        public CredentialViewModel Creds { get; set; }
        public SignInManager<IdentityUser> _signInManager { get; }

        [HttpPost]
        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid) return Page();

            var result = await _signInManager.PasswordSignInAsync(this.Creds.Email, this.Creds.Password, this.Creds.RememeberMe, false); 
            if(result.Succeeded) 
            {
                return RedirectToPage("/Index");
            }
            else 
            { 
                if(result.IsLockedOut)
                {
                    ModelState.AddModelError("Login", "Locked out after failed attempts");
                }
                else
                {
                    ModelState.AddModelError("Login", "Failed to login");
                }
            }
            return Page();
        }

        public void OnGet()
        {


        }


        public class CredentialViewModel
        {
            [Required]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember Me")]
            public bool RememeberMe { get; set; }
        }
    }
}

