using HumanResource.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace HumanResource.Controllers
{
    [ApiVersion("2.0")]
    [Route("api/companies")]
    //[Route("api/{v:api-version}/companies")]
    [ApiController]
    [ApiExplorerSettings(GroupName ="v2")]
    public class CompaniesV2Controller : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        public CompaniesV2Controller(IRepositoryManager repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            var companies = await _repository.Company.GetAllComnpanies(false);
            return Ok(companies);
        }
    }
}
