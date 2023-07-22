
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Accounts
{
    public class ConfirmEmailModel : PageModel
    {
        private readonly Microsoft.AspNetCore.Identity.UserManager<IdentityUser> _userManager;
        [BindProperty]      
        public string Message { get; set; }
        
        
        public ConfirmEmailModel(Microsoft.AspNetCore.Identity.UserManager<IdentityUser> userManager)
        {
                _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync(string userId, string token)
        {
            
            var user = await this._userManager.FindByIdAsync(userId);
            if(user != null)
            {

             var result = await this._userManager.ConfirmEmailAsync(user, token);
                if(result.Succeeded)
                {
                    this.Message = "Email successfully validated!Try login.";
                    return Page();
                }
            }

            this.Message = "Failed to validate email";



                return Page();
        }
    }
}
