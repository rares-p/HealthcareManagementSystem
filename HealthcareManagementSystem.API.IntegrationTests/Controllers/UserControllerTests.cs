using HealthcareManagementSystem.API.IntegrationTests.Base;
using HealthcareManagementSystem.Application.Features.Medications.Queries.GetAll;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using FluentAssertions;
using HealthcareManagementSystem.Application.Features.Users.Queries.GetAll;
using HealthcareManagementSystem.Application.Features.Medications.Commands.CreateMedication;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using HealthcareManagementSystem.Application.Features.Users.Commands.CreateUser;

namespace HealthcareManagementSystem.API.IntegrationTests.Controllers
{
    public class UserControllerTests : BaseApplicationContextTests
    {
        private const string RequestUri = "/api/v1/Users";

        [Fact]
        public async Task When_GetAllUsersQueryHandlerIsCalled_Then_Success()
        {
            var response = await Client.GetAsync(RequestUri);

            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<GetAllUsersResponse>(responseString);

            result?.Users.Count.Should().Be(3);
        }

        [Fact]
        public async Task When_CreateUserCommandHandlerIsCalledWithRightParameters_Then_TheEntityCreatedShouldBeReturned()
        {
            var user = new CreateUserCommand
            {
                FirstName = "TestName",
                LastName = "TestName2",
                DateOfBirth = DateTime.Now,
                AuthDataId = "testauth"
            };
            var response = await Client.PostAsJsonAsync(RequestUri, user);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<CreateUserCommandResponse>(responseString);

            result?.Should().NotBeNull();
            result!.User.FirstName.Should().Be(user.FirstName);
            result!.User.Id.Should().NotBeEmpty();
        }

        [Fact]
        public async Task
            When_CreateUserCommandHandlerIsCalledWithRightParameters_Then_GetAllUsersQueryHandlerIsCalled_Then_Success()
        {
            var user = new CreateUserCommand
            {
                FirstName = "TestName",
                LastName = "TestName2",
                DateOfBirth = DateTime.Now,
                AuthDataId = "testauth"
            };
            var response = await Client.PostAsJsonAsync(RequestUri, user);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<CreateUserCommandResponse>(responseString);

            result?.Should().NotBeNull();

            var getAllUsersResponse = await Client.GetAsync(RequestUri);
            getAllUsersResponse.EnsureSuccessStatusCode();

            var getAllUsersResponseString = await getAllUsersResponse.Content.ReadAsStringAsync();
            var getAllUsersResult =
                JsonConvert.DeserializeObject<GetAllUsersResponse>(getAllUsersResponseString);

            getAllUsersResult.Should().NotBeNull();
            getAllUsersResult!.Users.Count.Should().Be(4);
        }



        private static string CreateToken()
        {

            return JwtTokenProvider.JwtSecurityTokenHandler.WriteToken(
                new JwtSecurityToken(
                    JwtTokenProvider.Issuer,
                    JwtTokenProvider.Issuer,
                    new List<Claim> { new(ClaimTypes.Role, "Admin") },
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: JwtTokenProvider.SigningCredentials
                ));
        }
    }
}
