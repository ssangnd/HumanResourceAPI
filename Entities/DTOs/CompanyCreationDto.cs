using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
   public class CompanyCreationDto
    {
        [Required(ErrorMessage ="Company name is a required field.")]
        [MaxLength(60,ErrorMessage ="Maximax length for the Name is 60 characters.")]
        public String Name { get; set; }

        [Required(ErrorMessage ="Company address is required field.")]
        [MaxLength(250,ErrorMessage ="Maximum length for the Address is 250 characters.")]
        public string Address { get; set; }

        [Required(ErrorMessage ="Country is a requird field.")]
        public string Country { get; set; }
    }
}
