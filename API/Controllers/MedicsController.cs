using HealthcareManagementSystem.Application.Features.Medications.Queries.GetAll;
using HealthcareManagementSystem.Application.Features.Medics.Commands.CreateMedic;
using HealthcareManagementSystem.Application.Features.Medics.Commands.DeleteMedic;
using HealthcareManagementSystem.Application.Features.Medics.Commands.UpdateMedic;
using HealthcareManagementSystem.Application.Features.Medics.Queries.GetAll;
using HealthcareManagementSystem.Application.Features.Medics.Queries.GetById;
using HealthcareManagementSystem.Application.Features.Users.Commands.DeleteUser;
using HealthcareManagementSystem.Application.Features.Users.Commands.UpdateUser;
using HealthcareManagementSystem.Application.Features.Users.Queries.GetById;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class MedicsController : ApiControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await Mediator.Send(new GetAllMedicsQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await Mediator.Send(new GetByIdMedicQuery(id));
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreateMedicCommand command)
        {
            var result = await Mediator.Send(command);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody] UpdateMedicCommand command)
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
            var result = await Mediator.Send(new DeleteMedicCommand(id));
            if (!result.Success)
                return BadRequest(result);
            return Accepted(result);
        }

    }
}
