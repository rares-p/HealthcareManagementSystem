﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareManagementSystem.Application.Responses
{
    public class BaseResponse
    {
        public BaseResponse() => Success = true;

        public BaseResponse(string message, bool success)
        {
            Success = success;
            Message = message;
        }

        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;

        public List<string>? ValidationsErrors { get; set; }
    }
}
