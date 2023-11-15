using HealthcareManagementSystem.Application.Features.Users.Commands.CreateUser;
using HealthcareManagementSystem.Application.Features.Users.Queries.GetAll;
using HealthcareManagementSystem.Application.Features.Users.Queries.GetById;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class UsersController : ApiControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await Mediator.Send(new GetAllUsersQuery());
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(CreateUserCommand command)
        {
            var result = await Mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await Mediator.Send(new GetByIdUserQuery(id));
            return Ok(result);
        }
    }

}