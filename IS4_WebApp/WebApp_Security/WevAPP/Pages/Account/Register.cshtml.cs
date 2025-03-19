using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace WevAPP.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<IdentityUser> userManager;

        public RegisterModel(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        [BindProperty]
        public RegisterViewModel RegisterViewModel { get; set; } = new RegisterViewModel();
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync() {

            if (!ModelState.IsValid) { return Page(); }

            //Validate Email uniqueness
            var user = new IdentityUser
            {
                Email = RegisterViewModel.Email,
                UserName = RegisterViewModel.Email,

            };
            //Create the User with all the limitations we added
            var result = await this.userManager.CreateAsync(user,RegisterViewModel.Password);
            if (result.Succeeded) { 
                this.userManager.GenerateEmailConfirmationTokenAsync(user);

                
                return RedirectToPage("/Account/Login"); 
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("Register", error.Description); 
                }
            }
            return Page();
        }
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email Address!")]
        public string Email { get; set; } = string.Empty;
        [Required]
        [DataType(dataType: DataType.Password)]
        public string Password { get; set; } = string.Empty;

    }
}
