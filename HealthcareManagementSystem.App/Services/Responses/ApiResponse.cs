﻿namespace HealthcareManagementSystem.App.Services.Responses
{
    public class ApiResponse<T>
    {
        public string Message { get; set; } = string.Empty;
        public List<string>? ValidationsErrors { get; set; }
        public bool IsSuccess { get; set; }
        public T? Data { get; set; }
    }
}