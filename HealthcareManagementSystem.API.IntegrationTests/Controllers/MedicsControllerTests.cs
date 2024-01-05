using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Azure;
using FluentAssertions;
using HealthcareManagementSystem.API.IntegrationTests.Base;
using HealthcareManagementSystem.Application.Features.MedicationReminders.Queries.GetAllMedicationReminders;
using HealthcareManagementSystem.Application.Features.Medications.Commands.CreateMedication;
using HealthcareManagementSystem.Application.Features.Medications.Commands.DeleteMedication;
using HealthcareManagementSystem.Application.Features.Medications.Commands.UpdateMedication;
using HealthcareManagementSystem.Application.Features.Medications.Queries.GetAll;
using HealthcareManagementSystem.Application.Features.Medics;
using HealthcareManagementSystem.Application.Features.Medics.Commands.CreateMedic;
using HealthcareManagementSystem.Application.Features.Medics.Commands.DeleteMedic;
using HealthcareManagementSystem.Application.Features.Medics.Commands.UpdateMedic;
using HealthcareManagementSystem.Application.Features.Medics.Queries.GetAll;
using HealthcareManagementSystem.Domain.Data;
using HealthcareManagementSystem.Domain.Entities;
using Newtonsoft.Json;

namespace HealthcareManagementSystem.API.IntegrationTests.Controllers
{
    public class MedicsControllerTests : BaseApplicationContextTests
    {
        private const string RequestUri = "/api/v1/Medics";

        [Fact]
        public async Task When_GetAllMedicsQueryHandlerIsCalled_Then_Success()
        {
            var response = await Client.GetAsync(RequestUri);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<GetAllMedicsResponse>(responseString);

            result.Should().NotBeNull();
            result!.Medics.Count.Should().Be(4);
        }

        [Fact]
        public async Task When_CreateMedicCommandHandlerIsCalledWithRightParameters_Then_TheEntityCreatedShouldBeReturned()
        {
            var token = CreateToken();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var medic = new CreateMedicCommand
            {
                FirstName = "Medic",
                LastName = "LastName",
                Department = Department.EmergencyMedicine,
                AuthDataId = "testAuth"
            };
            var response = await Client.PostAsJsonAsync(RequestUri, medic);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<CreateMedicCommandResponse>(responseString);

            result?.Should().NotBeNull();
            result!.Medic.Department.Should().Be(Department.EmergencyMedicine);
            result!.Medic.LastName.Should().Be("LastName");
        }

        [Fact]
        public async Task When_CreateMedicCommandHandlerIsCalledWithRightParameters_Then_GetAllMedicsQueryHandlerIsCalled_Then_Success()
        {
            var token = CreateToken();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var medic = new CreateMedicCommand
            {
                FirstName = "Medic",
                LastName = "LastName",
                Department = Department.EmergencyMedicine,
                AuthDataId = "testAuth"
            };
            var response = await Client.PostAsJsonAsync(RequestUri, medic);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<CreateMedicCommandResponse>(responseString);

            result?.Should().NotBeNull();

            var getAllMedicationResponse = await Client.GetAsync(RequestUri);
            getAllMedicationResponse.EnsureSuccessStatusCode();

            var getAllMedicationResponseString = await getAllMedicationResponse.Content.ReadAsStringAsync();
            var getAllMedicationResult = JsonConvert.DeserializeObject<GetAllMedicsResponse>(getAllMedicationResponseString);

            getAllMedicationResult.Should().NotBeNull();
            getAllMedicationResult?.Medics.Count.Should().Be(5);
        }

        [Fact]
        public async Task When_GetAllMedicsQueryHandlerIsCalled_Then_GetByIdMedicsQueryHandlerIsCalled_Then_Names_Should_Match()
        {
            var response = await Client.GetAsync(RequestUri);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<GetAllMedicsResponse>(responseString);

            result?.Should().NotBeNull();

            foreach (var medic in result!.Medics)
            {
                medic.Id.Should().NotBeEmpty();

                var getByIdUri = RequestUri + $"/{medic.Id}";
                var getByIdResponse = await Client.GetAsync(getByIdUri);
                getByIdResponse.EnsureSuccessStatusCode();

                var getByIdResponseString = await getByIdResponse.Content.ReadAsStringAsync();
                var getByIdResult = JsonConvert.DeserializeObject<MedicDto>(getByIdResponseString);

                getByIdResult.Should().NotBeNull();
                getByIdResult?.LastName.Should().Be(medic.LastName);
            }
        }

        [Fact]
        public async Task When_GetAllMedicsQueryHandlerIsCalled_Then_UpdateMedicCommandHandlerIsCalled_Then_LastName_Should_Match()
        {
            string token = CreateToken();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await Client.GetAsync(RequestUri);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<GetAllMedicsResponse>(responseString);

            result?.Should().NotBeNull();
            result?.Medics[0].Should().NotBeNull();

            var medic = new UpdateMedicCommand()
            {
                Id = result!.Medics[0].Id,
                LastName = "TestName",
                FirstName = "TestName2",
                Department = Department.InternalMedicine
            };
            var updateMedicationResponse = await Client.PutAsJsonAsync(RequestUri, medic);
            updateMedicationResponse.EnsureSuccessStatusCode();

            var updateMedicationResponseString = await updateMedicationResponse.Content.ReadAsStringAsync();
            var updateMedicationResult = JsonConvert.DeserializeObject<UpdateMedicCommandResponse>(updateMedicationResponseString);

            updateMedicationResult.Should().NotBeNull();
            updateMedicationResult?.Medic.LastName.Should().Be(medic.LastName);
        }

        [Fact]
        public async Task When_GetAllMedicsQueryHandlerIsCalled_Then_DeleteMedicCommandHandlerIsCalled_Then_GetAllMedicsQueryHandlerIsCalled_Then_Success()
        {
            string token = CreateToken();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await Client.GetAsync(RequestUri);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<GetAllMedicsResponse>(responseString);

            result?.Should().NotBeNull();
            result?.Medics.Count.Should().Be(4);
            result?.Medics[0].Should().NotBeNull();

            var deleteUri = RequestUri + $"/{result!.Medics[0].Id}";
            var deleteMedicResponse = await Client.DeleteAsync(deleteUri);
            deleteMedicResponse.EnsureSuccessStatusCode();

            var deleteMedicResponseString = await response.Content.ReadAsStringAsync();
            var deleteMedicResult =
                JsonConvert.DeserializeObject<DeleteMedicCommandResponse>(deleteMedicResponseString);

            deleteMedicResult.Should().NotBeNull();


            var getAllMedicsResponse = await Client.GetAsync(RequestUri);
            getAllMedicsResponse.EnsureSuccessStatusCode();

            var getAllMedicationResponseString = await getAllMedicsResponse.Content.ReadAsStringAsync();
            var getAllMedicationResult = JsonConvert.DeserializeObject<GetAllMedicsResponse>(getAllMedicationResponseString);

            getAllMedicationResult.Should().NotBeNull();
            getAllMedicationResult?.Medics.Count.Should().Be(3);
        }

        private static string CreateToken()
        {

            return JwtTokenProvider.JwtSecurityTokenHandler.WriteToken(
                new JwtSecurityToken(
                    JwtTokenProvider.Issuer,
                    JwtTokenProvider.Issuer,
                    new List<Claim> { new(ClaimTypes.Role, "Medic") },
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: JwtTokenProvider.SigningCredentials
                ));
        }
    }
}
