using Entities;
using Entities.Models;
using Entities.RequestFeature;
using HumanResource.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class CompanyRepository:RepositoryBase<Company,Guid>, ICompanyRepository
    {
        public CompanyRepository(AppDbContext context):base(context)
        {

        }

        public async Task<IEnumerable<Company>> GetAllComnpanies(bool trackChanges) =>
            await FindAll(trackChanges).OrderBy(c => c.Name).ToListAsync();

        public async Task<PagedList<Company>> GetCompaniesAsync(CompanyParameters companyParameters, bool trackChange)
        {
            var companyItems = await FindAll(trackChange)
                .OrderBy(c => c.Name)
                .ToListAsync();

            return PagedList<Company>.ToPagedList(companyItems, companyParameters.PageNumber, 
                companyParameters.PageSize);
        }

        //public async Task<IEnumerable<Company>> GetCompaniesAsync(CompanyParameters companyParameters, bool trackChange)
        //{
        //    return await FindAll(trackChange)
        //        .OrderBy(c=>c.Name)
        //        .Skip((companyParameters.PageNumber-1)*companyParameters.PageNumber)
        //        .Take(companyParameters.PageSize)
        //        .ToListAsync()
        //        ;
        //}


    }
}
