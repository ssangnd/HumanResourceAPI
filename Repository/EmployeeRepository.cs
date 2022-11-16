using Entities;
using Entities.Models;
using Entities.RequestFeature;
using HumanResource.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;

namespace Repository
{
    public class EmployeeRepository:RepositoryBase<Employee,Guid>, IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext context):base(context)
        {

        }

        public async Task<PagedList<Employee>> GetEmployeesAsync(Guid companyId, EmployeeParameters employeeParameters, bool trackChange)
        {
            //var employees = await FindAll(trackChange, e => e.CompanyId.Equals(companyId) &&
            //   (e.Age >= employeeParameters.minAge && e.Age <= employeeParameters.maxAge)
            //   && e.FirstName.Contains(employeeParameters.SearchTerm))
            //    .OrderBy(e=>e.FirstName)
            //    .ToListAsync();
            var employees = await FindAll(trackChange, e => e.CompanyId.Equals(companyId))
                .FilterEmployee(employeeParameters.minAge,employeeParameters.maxAge)
                .Search(employeeParameters.SearchTerm)
                .OrderBy(e => e.FirstName)
                .ToListAsync();

            return PagedList<Employee>.ToPagedList(employees, employeeParameters.PageNumber, 
                employeeParameters.PageSize);
        }
    }
}
