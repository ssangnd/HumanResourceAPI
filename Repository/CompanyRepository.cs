using Entities;
using Entities.Models;
using HumanResource.Infrastructure;

namespace Repository
{
    public class CompanyRepository:RepositoryBase<Company,Guid>, ICompanyRepository
    {
        public CompanyRepository(AppDbContext context):base(context)
        {

        }
    }
}
