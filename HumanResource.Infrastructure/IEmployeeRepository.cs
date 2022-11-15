using Entities.Models;
using Entities.RequestFeature;

namespace HumanResource.Infrastructure
{
    public interface IEmployeeRepository: IRepositoryBase<Employee,Guid>
    {
        Task<PagedList<Employee>> GetEmployeesAsync(Guid companyId, EmployeeParameters employeeParameters, 
            bool trackChanges);
    }
}
