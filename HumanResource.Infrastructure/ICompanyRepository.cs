using Entities.Models;
using Entities.RequestFeature;

namespace HumanResource.Infrastructure
{
    public interface ICompanyRepository: IRepositoryBase<Company,Guid>
    {
        Task<PagedList<Company>> GetCompaniesAsync(CompanyParameters companyParameters, bool trackChange);
    }
}
