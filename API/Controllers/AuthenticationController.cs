using API.Models;
using HealthcareManagementSystem.Application.Contracts.Identity;
using HealthcareManagementSystem.Application.Contracts.Interfaces;
using HealthcareManagementSystem.Application.Features.Medics.Commands.CreateMedic;
using HealthcareManagementSystem.Application.Features.Users.Commands.CreateUser;
using HealthcareManagementSystem.Application.Models.Identity;
using HealthcareManagementSystem.Application.Models.Identity.Registration;
using HealthcareManagementSystem.Domain.Common;
using HealthcareManagementSystem.Identity.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace API.Controllers
{
    public class AuthenticationController : ApiControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthenticationController> _logger;
        private readonly ICurrentUserService _currentUserService;

        public AuthenticationController(IAuthService authService, ILogger<AuthenticationController> logger, ICurrentUserService currentUserService)
        {
            _authService = authService;
            _logger = logger;
            _currentUserService = currentUserService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid payload");
                }

                var (status, message) = await _authService.Login(model);

                if (status == 0)
                {
                    return BadRequest(message);
                }

                return Ok(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterUser(RegistrationModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid payload");
                }

                var authResult = await _authService.Registration(model, UserRoles.User);

                if (!authResult.IsSuccess)
                {
                    return BadRequest(authResult.Error);
                }

                var result = await Mediator.Send(new CreateUserCommand
                {
                    Username = model.Username,
                    FirstName = model.Firstname,
                    LastName = model.Lastname,
                    DateOfBirth = model.DateOfBirth,
                    AuthDataId = authResult.Value
                });

                if (!result.Success)
                {
                    await _authService.DeleteUser(authResult.Value);
                    return BadRequest(result);
                }

                return CreatedAtAction(nameof(RegisterUser), model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("register_medic")]
        public async Task<IActionResult> RegisterMedic(MedicRegistrationModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid payload");
                }

                var authResult = await _authService.Registration(model, UserRoles.Medic);

                if (!authResult.IsSuccess)
                {
                    return BadRequest(authResult.Error);
                }

                var result = await Mediator.Send(new CreateMedicCommand
                {
                    FirstName = model.Firstname,
                    LastName = model.Lastname,
                    Department = model.Department,
                    AuthDataId = authResult.Value
                });

                if (!result.Success)
                {
                    await _authService.DeleteUser(authResult.Value);
                    return BadRequest(result);
                }

                return CreatedAtAction(nameof(RegisterMedic), model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("register_admin")]
        public async Task<IActionResult> RegisterAdmin(RegistrationModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid payload");
                }

                var authResult = await _authService.Registration(model, UserRoles.Admin);

                if (!authResult.IsSuccess)
                {
                    return BadRequest(authResult.Error);
                }

                return CreatedAtAction(nameof(RegisterAdmin), model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await _authService.Logout();
            return Ok();
        }

        [HttpGet]
        [Route("currentuserinfo")]
        public CurrentUser CurrentUserInfo()
        {
            if (this._currentUserService.GetCurrentUserId() == null)
            {
                return new CurrentUser
                {
                    IsAuthenticated = false
                };
            }
            return new CurrentUser
            {
                IsAuthenticated = true,
                UserName = this._currentUserService.GetCurrentUserId(),
                Claims = this._currentUserService.GetCurrentClaimsPrincipal().Claims.ToDictionary(c => c.Type, c => c.Value)
            };
        }
    }
}
