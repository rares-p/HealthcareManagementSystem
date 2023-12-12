using HealthcareManagementSystem.Domain.Common;

namespace HealthcareManagementSystem.Application.Models.Identity
{
    public class AuthResult
    {
        private AuthResult(bool isSuccess, string id, string error)
        {
            IsSuccess = isSuccess;
            Id = id;
            Error = error;
        }

        public bool IsSuccess { get; }
        public string Id { get; }
        public string Error { get; }

        public static AuthResult Success(string id)
        {
            return new AuthResult(true, id, null!);
        }
        public static AuthResult Failure(string error)
        {
            return new AuthResult(false, null!, error);
        }
    }
}
