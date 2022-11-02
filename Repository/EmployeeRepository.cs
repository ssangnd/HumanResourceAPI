using Entities;
using Entities.Models;
using HumanResource.Infrastructure;

namespace Repository
{
    public class EmployeeRepository:RepositoryBase<Employee,Guid>, IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext context):base(context)
        {

        }
    }
}
