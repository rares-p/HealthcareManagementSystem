﻿using System.ComponentModel.DataAnnotations;
using HealthcareManagementSystem.Domain.Data;

namespace HealthcareManagementSystem.App.ViewModels
{
    public class CreateMedicViewModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Medic's first name is required!")]
        public string FirstName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Medic's last name is required!")]
        public string LastName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Medic must be part of a department!")]
        public Department Department { get; set; }
        public string Email { get; set; } = string.Empty;
    }
}