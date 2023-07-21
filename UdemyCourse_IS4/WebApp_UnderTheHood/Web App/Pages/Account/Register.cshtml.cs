using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Web_App.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;

        //ctor
        public RegisterModel(UserManager<IdentityUser> userManager)
        {
            this._userManager = userManager;
        }

        [BindProperty]
        public RegisterViewModel? RegisterView { get; set; }


        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) { return Page(); }

            //Validating Email address (Optional because we already configured the RequireUniqueEmail option in the program.cs)
            //Create the user
            var user = new IdentityUser
            {
                Email = RegisterView.Email,
                UserName = RegisterView.Email
            };

            var result = await this._userManager.CreateAsync(user, RegisterView.Password);
            if(result.Succeeded) 
            { 
                return RedirectToPage("/Account/Login"); 
            }
            else 
            { 
                foreach(var error in result.Errors )
                {
                    ModelState.AddModelError("Register",error.Description);

                }
                
                return Page();
            }

        }

        public class RegisterViewModel
        {
            [Required]
            [EmailAddress(ErrorMessage = "Invalid email address!")]
            public string Email { get; set; }
            [Required]
            [DataType(dataType: DataType.Password)]
            public string Password { get; set; }
        }





    }
}
