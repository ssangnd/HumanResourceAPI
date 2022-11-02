using Entities;
using Entities.Models;

namespace Repository
{
    public class EmployeeRepository:RepositoryBase<Employee,Guid>
    {
        public EmployeeRepository(AppDbContext context):base(context)
        {

        }
    }
}
