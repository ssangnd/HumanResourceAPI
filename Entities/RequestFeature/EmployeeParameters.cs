using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.RequestFeature
{
    public class EmployeeParameters:RequestParameters
    {
        public EmployeeParameters()
        {
            OrderBy = "FirstName";
        }
        public int minAge { get; set; }
        public int maxAge { get; set; } = int.MaxValue;
        public bool ValidAgeRange => maxAge > minAge;
        public string SearchTerm { get; set; }
    }
}
