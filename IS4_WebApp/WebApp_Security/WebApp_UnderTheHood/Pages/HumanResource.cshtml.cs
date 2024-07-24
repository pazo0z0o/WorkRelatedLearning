using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp_UnderTheHood.Pages
{
    public class HumanResourceModel : PageModel
    {
        [Authorize(Policy = "MustBelongToHRDepartment")] //specify the policy 
        public void OnGet()
        {
        }
    }
}
