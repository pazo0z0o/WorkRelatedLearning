using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp_UnderTheHood.Pages.Account
{
    public class LogoutModel : PageModel
    {
        public async Task<IActionResult> OnPost()
        {

            await HttpContext.SignOutAsync("MyCookieAuth");
            return RedirectToPage("/Index");
        }
    }
}
