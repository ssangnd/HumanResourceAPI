using AutoMapper;
using Entities.DTOs;
using Entities.Models;
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
        private readonly IMapper _mapper;

        private static class RouteNames
        {
            public const string GetCompanies = nameof(GetCompanies);
            public const string GetCompanyById = nameof(GetCompanyById);
            public const string CreateCompany = nameof(CreateCompany);
        }

        public CompaniesController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }


        [HttpGet]
        public IActionResult GetCompanies()
        {
            try
            {
                var companies = _repository.Company.FindAll(false);
                //var companiesDto = companies.Select(c => new CompanyDto
                //{
                //    Id = c.Id,
                //    Name = c.Name,
                //    FullAddress = string.Join(" ", c.Address, c.Country)
                //}).ToList();
                var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);

                return Ok(companiesDto);
            }catch(Exception ex)
            {
                _logger.LogError($"Error at: {nameof(GetCompanies)} action {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}", Name =RouteNames.GetCompanyById),]
        public async Task<IActionResult> GetCompany(Guid id)
        {
            var company = await _repository.Company.FindByIdAsync(id);
            if (company == null)
            {
                _logger.LogInfo($"Company with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            else
            {
                var companyDto = _mapper.Map<CompanyDto>(company);
                return Ok(companyDto);
            }
        }


        [HttpPost(Name =RouteNames.CreateCompany)]
        public async Task<IActionResult> CreateCompany([FromBody] CompanyCreationDto company)
        {
            if (company == null)
            {
                _logger.LogError("CompanyCreationDto object sent from client is null");
                return BadRequest("CompanyCreationDto object is null");
            }

            var companyEntity = _mapper.Map<Company>(company);
            _repository.Company.Create(companyEntity);
            var companyToReturn = _mapper.Map<CompanyDto>(companyEntity);
            return CreatedAtRoute(RouteNames.GetCompanyById, new { id = companyToReturn.Id }, companyToReturn);
        }



    }
}
