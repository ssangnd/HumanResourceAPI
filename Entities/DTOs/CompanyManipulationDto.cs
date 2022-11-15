using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class CompanyManipulationDto
    {
        [Required(ErrorMessage ="Company name is a required field.")]
        [MaxLength(60,ErrorMessage ="Maximum length for the Name is 60 chacracter")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Company address is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Name is 250 chacracter")]
        public string Address { get; set; }

        [Required(ErrorMessage ="Country is a required field")]
        public string Country { get; set; }
    }
}
