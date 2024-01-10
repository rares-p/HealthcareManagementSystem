using HealthcareManagementSystem.Application.Features.Examinations.Commands.CreateExamination;
using HealthcareManagementSystem.Application.Features.Examinations.Queries.GetAllExaminations;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	public class ExaminationController : ApiControllerBase
	{
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> GetAll()
		{
			var result = await Mediator.Send(new GetAllExaminationsQuery());
			return Ok(result);
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> Create(CreateExaminationCommand command)
		{
			var result = await Mediator.Send(command);
			if (!result.Success)
				return BadRequest(result);
			return Ok(result);
		}
	}
}
