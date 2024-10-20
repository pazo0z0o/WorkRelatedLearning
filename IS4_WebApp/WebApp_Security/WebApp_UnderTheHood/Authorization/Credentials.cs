using System.ComponentModel.DataAnnotations;

namespace WebApp_UnderTheHood.Authorization
{
    public class Credentials
    {
        [Required]
        [Display(Description = "User Name")]
        public string UserName { get; set; } = String.Empty;
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = String.Empty;
        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }



    }
}
