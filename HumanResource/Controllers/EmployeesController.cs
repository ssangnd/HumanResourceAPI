using AutoMapper;
using Entities.DTOs;
using Entities.Models;
using Entities.RequestFeature;
using HumanResource.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HumanResource.Controllers
{
    [Route("api/companies/{companyId}/employees")]
    public class EmployeesController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public EmployeesController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployeesForCompany(Guid companyId, [FromQuery] EmployeeParameters employeeParameters)
        {
            if (!employeeParameters.ValidAgeRange) return
                     BadRequest("Max age can't be less than min amge");
            var company = await _repository.Company.FindByIdAsync(companyId);
            if(company==null)
            {
                _logger.LogInfo($"Company with id: {companyId} doesn't exist in the database");
                return NotFound();
            }
            var employeeFromDb = await _repository.Employee.GetEmployeesAsync(companyId,
                employeeParameters, false);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(employeeFromDb.MetaData));
            var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employeeFromDb);
            return Ok(employeesDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee(Guid companyId, [FromBody] EmployeeForCreationDto employee)
        {
            var company = await _repository.Company.FindByIdAsync(companyId);
            if(company==null)
            {
                _logger.LogInfo($"Company with id: {companyId} doesn't exist in the database");
                return NotFound();
            }

            var employeeEntity = _mapper.Map<Employee>(employee);
            employeeEntity.CompanyId = companyId;

            _repository.Employee.Create(employeeEntity);
            await _repository.SaveAsync();
            var employeeToReturn = _mapper.Map<EmployeeDto>(employeeEntity);

            return CreatedAtRoute("GetEmployeeForCompany", new
            {
                companyId,
                id=employeeToReturn.Id
            },
            employeeToReturn
            );
        }

        [HttpGet("{id}", Name = "GetEmployeeForCompany")]
        public async Task<IActionResult> GetEmployeeForCompany(Guid companyId, Guid id)
        {
            var company = await _repository.Company.FindByIdAsync(companyId);
            if (company == null)
            {
                _logger.LogInfo($"Company with if{companyId} doesn't exist in the daatabase");
                return NotFound();
            }

            var employeeDto = await _repository.Employee.FindByIdAsync(id);
            if (employeeDto == null)
            {
                _logger.LogInfo($"Exployee with id: {id} doesn't exist in the database");
                return NotFound();
            }
            var employee = _mapper.Map<EmployeeDto>(employeeDto);
            return Ok(employee);
        }

        [HttpPost("collection")]
        public async Task<IActionResult> CreateEmployeeCollection(Guid companyId, [FromBody] IEnumerable<EmployeeForCreationDto> emloyeeCollection)
        {
            if (emloyeeCollection == null)
            {
                _logger.LogError("Employee collection sent from client is null");
                return BadRequest("Employee collection is null");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invlid model state for the EmployeeForCreateDto object");
                return UnprocessableEntity(ModelState);
            }

            var company = await _repository.Company.FindByIdAsync(companyId);
            if (company == null)
            {
                _logger.LogInfo($"Company with id: {companyId} doesn't exist in the database");
                return NotFound();
            }

            var employeeEntities = _mapper.Map<IEnumerable<Employee>>(emloyeeCollection);
            foreach (var employee in employeeEntities)
            {
                employee.CompanyId = companyId;
                _repository.Employee.Create(employee);
            }
            await _repository.SaveAsync();
            var companyCollectionToReturn = _mapper.Map<IEnumerable<EmployeeDto>>(employeeEntities);
            return Ok(companyCollectionToReturn);
        }

    }
}
