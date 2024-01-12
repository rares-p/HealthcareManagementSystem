using System.Security.Claims;

namespace HealthcareManagementSystem.Application.Contracts.Interfaces
{
    public interface ICurrentUserService
    {
        string UserId { get; }
        ClaimsPrincipal? GetCurrentClaimsPrincipal();
        string? GetCurrentUserId();
    }
}
