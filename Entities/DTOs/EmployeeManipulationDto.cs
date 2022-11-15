using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class EmployeeManipulationDto
    {
        [Required(ErrorMessage ="Employee name is a required field.")]
        [MaxLength(30,ErrorMessage ="Maximum length for the Name is 30 chacracter")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Eployee Name is a required field.")]
        [MaxLength(50, ErrorMessage = "Maximum length for the Name is 50 chacracter")]
        public string LastName { get; set; }

        [Required(ErrorMessage ="Age is required fields")]
        [Range(10,int.MaxValue,ErrorMessage ="Age is required and it can't be lower than 10")]
        public int Age { get; set; }

        [Required(ErrorMessage ="Position is a required field")]
        [MaxLength(20,ErrorMessage ="Maximum length for the position is 20 characters.")]
        public string Position { get; set; }

        public string Address { get; set; }
        public string City { get; set; }
    }
}
