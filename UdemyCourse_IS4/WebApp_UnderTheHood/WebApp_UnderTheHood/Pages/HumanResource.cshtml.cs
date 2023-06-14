using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp_UnderTheHood.Pages
{
    [Authorize(Policy = "MustBelongToHRDepartment")] //The name of the policy 
    public class HumanResourceModel : PageModel
    {


        public void OnGet()
        {
        }
    }
}
