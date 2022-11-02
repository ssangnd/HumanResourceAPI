using Entities.Models;
using HumanResource.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace HumanResource.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private IRepositoryBase<Company, Guid> _companyRepository;

        public TestController(ILoggerManager logger, IRepositoryBase<Company, Guid> companyRepository)
        {
            _logger = logger;
            _companyRepository = companyRepository;
        }


        [HttpGet]
        public List<Company> GetText()
        {
            //_logger.LogInfo("This is message from Weather Controller");
            var result = _companyRepository.FindAll(false).ToList();
            return result;
        }
    }
}
