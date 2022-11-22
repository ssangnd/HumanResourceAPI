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
        private readonly IAuthenticationManager _authManager;

        public AccountController(ILoggerManager logger, IMapper mapper,UserManager<User> userManager, IAuthenticationManager authManager)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _authManager = authManager;
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

        [HttpPost("login")]
        public async Task<IActionResult> AuthenticateUser([FromBody] UserForAuthenticationFto user)
        {
            if (await _authManager.ValidateUser(user))
                return Ok(new
                {
                    Token = await _authManager.Createtoken()
                }); ;
            _logger.LogWarn($"{nameof(AuthenticateUser)}: Login failed. Your user name or password is wrong");
            return Unauthorized();
        }
    }
}
