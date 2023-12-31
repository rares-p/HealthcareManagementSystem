﻿using System.ComponentModel.DataAnnotations;

namespace HealthcareManagementSystem.App.ViewModels
{
	public class RegisterViewModel
	{
		[Required(ErrorMessage = "First name is required.")]
		[MinLength(3, ErrorMessage = "First name must have at least 3 characters.")]
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public string Username { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;
		public DateTime DateOfBirth { get; set; }
		public string PhoneNumber { get; set; } = string.Empty;
	}
}
