using HumanResource.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace HumanResource.Controllers
{
    [Route("api/companies")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        public CompaniesController(IRepositoryManager repository, ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }


        [HttpGet]
        public IActionResult GetCompanies()
        {
            try
            {
                var companies = _repository.Company.FindAll(false);
                return Ok(companies);
            }catch(Exception ex)
            {
                _logger.LogError($"Error at: {nameof(GetCompanies)} action {ex}");
                return StatusCode(500, "Internal server error");
            }
        }


    }
}
