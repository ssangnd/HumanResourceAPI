using HumanResource.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace HumanResource.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ILoggerManager _logger;

        public TestController(ILoggerManager logger)
        {
            _logger = logger;
        }

     
        [HttpGet]
        public string GetText()
        {
            _logger.LogInfo("This is message from Weather Controller");
            return "Hello";
        }
    }
}
