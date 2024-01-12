using HealthcareManagementSystem.Application.Features.Users.Commands.CreateUser;
using HealthcareManagementSystem.Application.Features.Users.Commands.DeleteUser;
using HealthcareManagementSystem.Application.Features.Users.Commands.UpdateUser;
using HealthcareManagementSystem.Application.Features.Users.Queries.GetAll;
using HealthcareManagementSystem.Application.Features.Users.Queries.GetById;
using HealthcareManagementSystem.Application.Features.Users.Queries.GetByUsernameUser;
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

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await Mediator.Send(new GetByIdUserQuery(id));
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByUsername([FromQuery] string username)
        {
	        var result = await Mediator.Send(new GetByUsernameUserQuery(username));
	        return Ok(result);
        }

		[HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreateUserCommand command)
        {
            var result = await Mediator.Send(command);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody] UpdateUserCommand command)
        {
            var result = await Mediator.Send(command);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await Mediator.Send(new DeleteUserCommand(id));
            if (!result.Success)
                return BadRequest(result);
            return Accepted(result);
        }
    }

}