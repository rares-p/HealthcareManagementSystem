using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using FluentAssertions;
using HealthcareManagementSystem.API.IntegrationTests.Base;
using HealthcareManagementSystem.Application.Features.Medications;
using HealthcareManagementSystem.Application.Features.Medications.Commands.CreateMedication;
using HealthcareManagementSystem.Application.Features.Medications.Commands.DeleteMedication;
using HealthcareManagementSystem.Application.Features.Medications.Commands.UpdateMedication;
using HealthcareManagementSystem.Application.Features.Medications.Queries.GetAll;
using Newtonsoft.Json;


namespace HealthcareManagementSystem.API.IntegrationTests.Controllers
{
    public class MedicationControllerTests : BaseApplicationContextTests
    {
        private const string RequestUri = "/api/v1/Medication";

        [Fact]
        public async Task When_GetAllMedicationsQueryHandlerIsCalled_Then_Success()
        {
            var response = await Client.GetAsync(RequestUri);

            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<GetAllMedicationsResponse>(responseString);

            result?.Medications.Count.Should().Be(5);
        }

        [Fact]
        public async Task When_CreateMedicationCommandHandlerIsCalledWithRightParameters_Then_TheEntityCreatedShouldBeReturned()
        {
            var token = CreateToken();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            const string medicationTestName = "Test Medication";
            var medication = new CreateMedicationCommand
            {
                Name = medicationTestName,
            };
            var response = await Client.PostAsJsonAsync(RequestUri, medication);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<CreateMedicationCommandResponse>(responseString);

            result?.Should().NotBeNull();
            result?.Medication.Name.Should().Be(medication.Name);
            result?.Medication.Id.Should().NotBeEmpty();
        }

        [Fact]
        public async Task When_CreateMedicationCommandHandlerIsCalledWithRightParameters_Then_GetAllMedicationsQueryHandlerIsCalled_Then_Success()
        {
            var token = CreateToken();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            const string medicationTestName = "Test Medication";
            var medication = new CreateMedicationCommand
            {
                Name = medicationTestName,
            };
            var response = await Client.PostAsJsonAsync(RequestUri, medication);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<CreateMedicationCommandResponse>(responseString);

            result?.Should().NotBeNull();

            var getAllMedicationResponse = await Client.GetAsync(RequestUri);
            getAllMedicationResponse.EnsureSuccessStatusCode();

            var getAllMedicationResponseString = await getAllMedicationResponse.Content.ReadAsStringAsync();
            var getAllMedicationResult = JsonConvert.DeserializeObject<GetAllMedicationsResponse>(getAllMedicationResponseString);

            getAllMedicationResult.Should().NotBeNull();
            getAllMedicationResult?.Medications.Count.Should().Be(6);
        }

        [Fact]
        public async Task When_GetAllMedicationsQueryHandlerIsCalled_Then_GetByIdMedicationQueryHandlerIsCalled_Then_Names_Should_Match()
        {
            var response = await Client.GetAsync(RequestUri);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<GetAllMedicationsResponse>(responseString);

            result?.Should().NotBeNull();

            foreach (var medication in result!.Medications)
            {
                medication.Id.Should().NotBeEmpty();

                var getByIdUri = RequestUri + $"/{medication.Id}";
                var getByIdResponse = await Client.GetAsync(getByIdUri);
                getByIdResponse.EnsureSuccessStatusCode();

                var getByIdResponseString = await getByIdResponse.Content.ReadAsStringAsync();
                var getByIdResult = JsonConvert.DeserializeObject<MedicationDto>(getByIdResponseString);

                getByIdResult.Should().NotBeNull();
                getByIdResult?.Name.Should().Be(medication.Name);
            }
        }

        [Fact]
        public async Task When_GetAllMedicationsQueryHandlerIsCalled_Then_UpdateMedicationCommandHandlerIsCalled_Then_Name_Should_Match()
        {
            string token = CreateToken();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await Client.GetAsync(RequestUri);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<GetAllMedicationsResponse>(responseString);

            result?.Should().NotBeNull();
            result?.Medications[0].Should().NotBeNull();

            var medicationTestName = "Test Name";
            var medication = new UpdateMedicationCommand()
            {
                Id = result!.Medications[0].Id,
                Name = medicationTestName
            };
            var updateMedicationResponse = await Client.PutAsJsonAsync(RequestUri, medication);
            updateMedicationResponse.EnsureSuccessStatusCode();

            var updateMedicationResponseString = await updateMedicationResponse.Content.ReadAsStringAsync();
            var updateMedicationResult = JsonConvert.DeserializeObject<UpdateMedicationCommandResponse>(updateMedicationResponseString);

            updateMedicationResult.Should().NotBeNull();
            updateMedicationResult?.Medication.Name.Should().Be(medicationTestName);
        }

        [Fact]
        public async Task When_GetAllMedicationsQueryHandlerIsCalled_Then_DeleteMedicationCommandHandlerIsCalled_Then_GetAllMedicationsQueryHandlerIsCalled_Then_Success()
        {
            string token = CreateToken();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await Client.GetAsync(RequestUri);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<GetAllMedicationsResponse>(responseString);

            result?.Should().NotBeNull();
            result?.Medications.Count.Should().Be(5);
            result?.Medications[0].Should().NotBeNull();

            var deleteUri = RequestUri + $"/{result!.Medications[0].Id}";
            var deleteMedicationResponse = await Client.DeleteAsync(deleteUri);
            deleteMedicationResponse.EnsureSuccessStatusCode();

            var deleteMedicationResponseString = await response.Content.ReadAsStringAsync();
            var deleteMedicationResult =
                JsonConvert.DeserializeObject<DeleteMedicationCommandResponse>(deleteMedicationResponseString);

            deleteMedicationResult.Should().NotBeNull();


            var getAllMedicationResponse = await Client.GetAsync(RequestUri);
            getAllMedicationResponse.EnsureSuccessStatusCode();

            var getAllMedicationResponseString = await getAllMedicationResponse.Content.ReadAsStringAsync();
            var getAllMedicationResult = JsonConvert.DeserializeObject<GetAllMedicationsResponse>(getAllMedicationResponseString);

            getAllMedicationResult.Should().NotBeNull();
            getAllMedicationResult?.Medications.Count.Should().Be(4);
        }


    private static string CreateToken()
        {

            return JwtTokenProvider.JwtSecurityTokenHandler.WriteToken(
                new JwtSecurityToken(
                    JwtTokenProvider.Issuer,
                    JwtTokenProvider.Issuer,
                    new List<Claim> { new(ClaimTypes.Role, "Medic")},
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: JwtTokenProvider.SigningCredentials
                ));
        }
    }
}
