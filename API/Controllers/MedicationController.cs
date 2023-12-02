using HealthcareManagementSystem.Application.Features.Medications.Commands.CreateMedication;
using HealthcareManagementSystem.Application.Features.Medications.Commands.DeleteMedication;
using HealthcareManagementSystem.Application.Features.Medications.Commands.UpdateMedication;
using HealthcareManagementSystem.Application.Features.Medications.Queries.GetAll;
using HealthcareManagementSystem.Application.Features.Medications.Queries.GetById;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class MedicationController : ApiControllerBase
    {

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await Mediator.Send(new GetAllMedicationsQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await Mediator.Send(new GetByIdMedicationQuery(id));
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreateMedicationCommand command)
        {
            var result = await Mediator.Send(command);
            if (!result.Success)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody] UpdateMedicationCommand command)
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
            var result = await Mediator.Send(new DeleteMedicationCommand(id));
            if (!result.Success)
                return BadRequest(result);
            return Accepted(result);
        }
    }
}
