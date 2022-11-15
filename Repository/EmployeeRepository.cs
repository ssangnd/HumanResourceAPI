using Entities;
using Entities.Models;
using Entities.RequestFeature;
using HumanResource.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class EmployeeRepository:RepositoryBase<Employee,Guid>, IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext context):base(context)
        {

        }

        public async Task<PagedList<Employee>> GetEmployeesAsync(Guid companyId, EmployeeParameters employeeParameters, bool trackChange)
        {
            var employees = await FindAll(trackChange, e => e.CompanyId.Equals(companyId) &&
               (e.Age >= employeeParameters.minAge && e.Age <= employeeParameters.maxAge))
                .OrderBy(e=>e.FirstName)
                .ToListAsync();

            return PagedList<Employee>.ToPagedList(employees, employeeParameters.PageNumber, 
                employeeParameters.PageSize);
        }
    }
}
