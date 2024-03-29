﻿using HealthcareManagementSystem.Domain.Data;
using MediatR;

namespace HealthcareManagementSystem.Application.Features.Medics.Commands.UpdateMedic
{
    public class UpdateMedicCommand : IRequest<UpdateMedicCommandResponse>
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; } = null; 
        public string? LastName { get; set; } = null;
        public Department? Department{ get; set; } = null;
    }
}
