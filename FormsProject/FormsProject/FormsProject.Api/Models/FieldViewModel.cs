using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormsProject.Api.Models
{
    public class FieldViewModel
    {
        public FieldViewModel()
        {
            Name = String.Empty;
        }

        [Required]
        [MinLength(1)]
        [MaxLength(64)]
        public string Name { get; set; }

        [Required]
        public int FormId { get; set; }
        [Required]
        public double Value { get; set; }
    }
}
