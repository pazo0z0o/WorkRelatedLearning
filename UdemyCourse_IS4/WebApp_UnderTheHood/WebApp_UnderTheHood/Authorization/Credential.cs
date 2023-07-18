﻿using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace WebApp_UnderTheHood.Authorization
{
    public class Credential
    {
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememeberMe { get; set; }
    }
}
