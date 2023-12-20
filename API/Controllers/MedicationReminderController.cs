using HealthcareManagementSystem.Application.Features.MedicationReminders.Commands.CreateMedicationReminder;
using HealthcareManagementSystem.Application.Features.MedicationReminders.Commands.UpdateMedicationReminder;
using HealthcareManagementSystem.Application.Features.MedicationReminders.Queries.GetAllMedicationReminders;
using HealthcareManagementSystem.Application.Features.Medics.Commands.UpdateMedic;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class MedicationReminderController : ApiControllerBase
	{
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> GetAll()
		{
			var result = await Mediator.Send(new GetAllMedicationRemindersQuery());
			return Ok(result);
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> Create(CreateMedicationReminderCommand command)
		{
			var result = await Mediator.Send(command);
			if (!result.Success)
				return BadRequest(result);
			return Ok(result);
		}

		[HttpPut]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> Update([FromBody] UpdateMedicationReminderCommand command)
		{
			var result = await Mediator.Send(command);
			if (!result.Success)
				return BadRequest(result);
			return Ok(result);
		}
	}
}
