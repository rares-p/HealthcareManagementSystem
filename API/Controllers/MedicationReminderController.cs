using HealthcareManagementSystem.Application.Features.MedicationReminders.Commands;
using HealthcareManagementSystem.Application.Features.MedicationReminders.GetAllMedicationReminders;
using HealthcareManagementSystem.Application.Features.Medications.Queries.GetAll;
using HealthcareManagementSystem.Application.Features.Medics.Commands.CreateMedic;
using MediatR;
using Microsoft.AspNetCore.Http;
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
	}
}
