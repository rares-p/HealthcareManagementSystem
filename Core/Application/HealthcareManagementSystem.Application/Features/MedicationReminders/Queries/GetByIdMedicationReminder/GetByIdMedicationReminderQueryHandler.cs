using System.Threading;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthcareManagementSystem.Application.Persistence;
using HealthcareManagementSystem.Application.Features.MedicationReminders.Commands.CreateMedicationReminder;
using HealthcareManagementSystem.Domain.Common;
using HealthcareManagementSystem.Domain.Entities;

namespace HealthcareManagementSystem.Application.Features.MedicationReminders.Queries.GetByIdMedicationReminder
{
	public class GetByIdMedicationReminderQueryHandler : IRequestHandler<GetByIdMedicationReminderQuery, GetByIdMedicationReminderResponse>
	{
		private readonly IMedicationReminderRepository _repository;

		public GetByIdMedicationReminderQueryHandler(IMedicationReminderRepository repository)
		{
			_repository = repository;
		}

		public async Task<GetByIdMedicationReminderResponse> Handle(GetByIdMedicationReminderQuery request, CancellationToken cancellationToken)
		{
			var medicationReminder = await _repository.FindByIdAsync(request.Id);
			if (!medicationReminder.IsSuccess)
				return new GetByIdMedicationReminderResponse()
				{
					Success = false,
					ValidationsErrors = new List<string>
					{
						medicationReminder.Error
					}
				};
			return new GetByIdMedicationReminderResponse()
			{
				Success = true,
				MedicationReminder = new MedicationRemindersDto(){
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
