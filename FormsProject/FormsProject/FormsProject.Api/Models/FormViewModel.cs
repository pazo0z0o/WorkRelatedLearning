using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormsProject.Api.Models
{
    public class FormViewModel
    {
        public FormViewModel()
        {
            Title = string.Empty;
        }

        [MaxLength(64)]
        [MinLength(1)]
        [Required]
        public string Title { get; set; }
    }
}
