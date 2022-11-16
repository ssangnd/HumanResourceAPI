using Entities.Models;
using Entities.RequestFeature;

namespace HumanResource.Infrastructure
{
    public interface ICompanyRepository: IRepositoryBase<Company,Guid>
    {
        Task<IEnumerable<Company>> GetAllComnpanies(bool trackChanges);
        Task<PagedList<Company>> GetCompaniesAsync(CompanyParameters companyParameters1, bool trackChange);

    }
}
