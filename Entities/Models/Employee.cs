using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Employee: DomainEntity<Guid>
    {
        //[Column("EmployeeId")]
        //public Guid Id { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        [MaxLength(20,ErrorMessage ="Maximum length for the First Name is 20 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage ="Last Nam is required.")]
        [MaxLength(20,ErrorMessage ="Maximum length for the Last Name is 20 chareacters.")]
        public string LastName { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
        public string Position { get; set; }

        [ForeignKey(nameof(Company))]
        public Guid CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
