using AutoMapper;
using Entities.DTOs;
using Entities.Models;
using HumanResource.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HumanResource.Controllers
{
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public AccountController(ILoggerManager logger, IMapper mapper,UserManager<User> userManager)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForregistration)
        {
            var user = _mapper.Map<User>(userForregistration);
            var result = await _userManager.CreateAsync(user, userForregistration.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }

            await _userManager.AddToRolesAsync(user, userForregistration.Roles);
            return StatusCode(201);
        }
    }
}
