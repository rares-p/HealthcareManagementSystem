using System.Threading;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthcareManagementSystem.Application.Persistence;
using HealthcareManagementSystem.Application.Features.Medics.Commands.UpdateMedic;
using HealthcareManagementSystem.Domain.Data;
using HealthcareManagementSystem.Application.Features.MedicationReminders.Commands.CreateMedicationReminder;

namespace HealthcareManagementSystem.Application.Features.MedicationReminders.Commands.UpdateMedicationReminder
{
	public class UpdateMedicationReminderCommandHandler : IRequestHandler<UpdateMedicationReminderCommand, UpdateMedicationReminderCommandResponse>
	{
		private readonly IMedicationReminderRepository _repository;

		public UpdateMedicationReminderCommandHandler(IMedicationReminderRepository _repository)
		{
			this._repository = _repository;
		}

		public async Task<UpdateMedicationReminderCommandResponse> Handle(UpdateMedicationReminderCommand request, CancellationToken cancellationToken)
		{
			var validator = new UpdateMedicationReminderCommandValidator();
			var validatorResult = await validator.ValidateAsync(request, cancellationToken);

			if (!validatorResult.IsValid)
				return new UpdateMedicationReminderCommandResponse()
				{
					Success = false,
					ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
				};

			var medicationReminder = await _repository.FindByIdAsync((Guid)request.Id!);
			if (!medicationReminder.IsSuccess)
				return new UpdateMedicationReminderCommandResponse()
				{
					Success = false,
					ValidationsErrors = new() { medicationReminder.Error }
				};

			if (request.MedicationId.HasValue && request.MedicationId != Guid.Empty)
			{
				var result = medicationReminder.Value.UpdateMedicationId(request.MedicationId.Value);
				if (!result.IsSuccess)
					return new UpdateMedicationReminderCommandResponse()
					{
						Success = false,
						ValidationsErrors = new() { result.Error }
					};
			}

			if (request.Dosage.HasValue)
			{
				var result = medicationReminder.Value.UpdateDosage(request.Dosage.Value);
				if (!result.IsSuccess)
					return new UpdateMedicationReminderCommandResponse()
					{
						Success = false,
						ValidationsErrors = new() { result.Error }
					};
			}

			if (request.StartDate.HasValue)
			{
				var result = medicationReminder.Value.UpdateStartDate(request.StartDate.Value);
				if (!result.IsSuccess)
				{
					return new UpdateMedicationReminderCommandResponse()
					{
						Success = false,
						ValidationsErrors = new() { result.Error }
					};
				}
			}

			if (request.EndDate.HasValue)
			{
				var result = medicationReminder.Value.UpdateEndDate(request.EndDate.Value);
				if (!result.IsSuccess)
				{
					return new UpdateMedicationReminderCommandResponse()
					{
						Success = false,
						ValidationsErrors = new() { result.Error }
					};
				}
			}

			if (request.DayInterval.HasValue)
			{
				var result = medicationReminder.Value.UpdateDayInterval(request.DayInterval.Value);
				if (!result.IsSuccess)
				{
					return new UpdateMedicationReminderCommandResponse()
					{
						Success = false,
						ValidationsErrors = new() { result.Error }
					};
				}
			}

			if (request.HourList != null && request.HourList.Count > 0)
			{
				var result = medicationReminder.Value.UpdateHourList(request.HourList);
				if (!result.IsSuccess)
				{
					return new UpdateMedicationReminderCommandResponse()
					{
						Success = false,
						ValidationsErrors = new() { result.Error }
					};
				}
			}

			try
			{
				await _repository.UpdateAsync(medicationReminder.Value);
			}
			catch (Exception)
			{
				return new UpdateMedicationReminderCommandResponse()
				{
					Success = false,
					ValidationsErrors = new() { "An internal error occurred. Please try again later" }
				};
			}

			return new UpdateMedicationReminderCommandResponse()
			{
				Success = true,
				MedicationReminder = new MedicationRemindersDto()
				{
					Id = medicationReminder.Value.Id,
					UserId = medicationReminder.Value.UserId,
					MedicationId = medicationReminder.Value.MedicationId,
					Dosage = medicationReminder.Value.Dosage,
					StartDate = medicationReminder.Value.StartDate,
					EndDate = medicationReminder.Value.EndDate,
					DayInterval = medicationReminder.Value.DayInterval,
					HourList = new List<float>(medicationReminder.Value.HourList)
				}
			};
		}
	}
}
