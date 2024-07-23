using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Net;

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
