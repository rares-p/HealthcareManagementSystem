using FluentAssertions;
using HealthcareManagementSystem.API.IntegrationTests.Base;
using HealthcareManagementSystem.Application.Features.MedicationReminders.Queries.GetAllMedicationReminders;
using HealthcareManagementSystem.Application.Features.MedicationReminders.Queries.GetByIdMedicationReminder;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using HealthcareManagementSystem.Application.Features.MedicationReminders;
using HealthcareManagementSystem.Application.Features.MedicationReminders.Commands.CreateMedicationReminder;
using HealthcareManagementSystem.Application.Features.MedicationReminders.Commands.UpdateMedicationReminder;

namespace HealthcareManagementSystem.API.IntegrationTests.Controllers
{
    public class MedicationReminderControllerTests : BaseApplicationContextTests
    {
        private const string RequestUri = "/api/v1/MedicationReminder";


        [Fact]
        public async Task When_GetAllMedicationReminderQueryHandlerIsCalled_Then_Success()
        {
            var response = await Client.GetAsync(RequestUri);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<GetAllMedicationRemindersResponse>(responseString);

            result.Should().NotBeNull();
            result?.MedicationReminders.Count.Should().Be(2);
        }

        [Fact]
        public async Task
            When_GetAllMedicationReminderQueryHandlerIsCalled_Then_GetByIdQueryHandlerIsCalled_Then_TheyShouldMatch()
        {
            var response = await Client.GetAsync(RequestUri);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<GetAllMedicationRemindersResponse>(responseString);

            result?.Should().NotBeNull();

            foreach (var medicationReminder in result!.MedicationReminders)
            {
                medicationReminder.Id.Should().NotBeEmpty();

                var getByIdUri = RequestUri + $"/{medicationReminder.Id}";
                var getByIdResponse = await Client.GetAsync(getByIdUri);
                getByIdResponse.EnsureSuccessStatusCode();

                var getByIdResponseString = await getByIdResponse.Content.ReadAsStringAsync();
                var getByIdResult = JsonConvert.DeserializeObject<MedicationRemindersDto>(getByIdResponseString);

                getByIdResult.Should().NotBeNull();
                getByIdResult?.UserId.Should().Be(medicationReminder.UserId);
            }
        }

        [Fact]
        public async Task
            When_CreateMedicationReminderCommandIsCalled_Then_Success()
        {
            var response = await Client.GetAsync(RequestUri);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<GetAllMedicationRemindersResponse>(responseString);

            result.Should().NotBeNull();

            var token = CreateToken();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var medicationReminder = new CreateMedicationReminderCommand()
            {
                DayInterval = 1u,
                Dosage = 1u,
                StartDate = DateTime.Now,
                EndDate = DateTime.MaxValue,
                HourList = new List<float>
                {
                    1f
                },
                MedicationId = result.MedicationReminders[0].MedicationId,
                UserId = result.MedicationReminders[0].UserId,
            };
            var createMedicationReminderResponse = await Client.PostAsJsonAsync(RequestUri, medicationReminder);
            createMedicationReminderResponse.EnsureSuccessStatusCode();

            var createMedicationReminderResponseString = await createMedicationReminderResponse.Content.ReadAsStringAsync();
            var createMedicationReminderResult = JsonConvert.DeserializeObject<CreateMedicationReminderCommandResponse>(createMedicationReminderResponseString);

            createMedicationReminderResult.Should().NotBeNull();
            createMedicationReminderResult?.MedicationReminder.Should().NotBeNull();
            createMedicationReminderResult?.MedicationReminder?.Id.Should().NotBeEmpty();
            createMedicationReminderResult?.MedicationReminder?.DayInterval.Should().Be(1u);
        }

        [Fact]
        public async Task
            When_CreateMedicationReminderQueryIsCalled_Then_GetAllMedicationReminderQueryHandlerIsCalled_Then_Success()
        {
            var response = await Client.GetAsync(RequestUri);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<GetAllMedicationRemindersResponse>(responseString);

            result.Should().NotBeNull();

            var token = CreateToken();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var medicationReminder = new CreateMedicationReminderCommand()
            {
                DayInterval = 1u,
                Dosage = 1u,
                StartDate = DateTime.Now,
                EndDate = DateTime.MaxValue,
                HourList = new List<float>
                {
                    1f
                },
                MedicationId = result!.MedicationReminders[0].MedicationId,
                UserId = result.MedicationReminders[0].UserId,
            };
            var createMedicationReminderResponse = await Client.PostAsJsonAsync(RequestUri, medicationReminder);
            createMedicationReminderResponse.EnsureSuccessStatusCode();

            var createMedicationReminderResponseString = await createMedicationReminderResponse.Content.ReadAsStringAsync();
            var createMedicationReminderResult = JsonConvert.DeserializeObject<CreateMedicationReminderCommandResponse>(createMedicationReminderResponseString);

            createMedicationReminderResult.Should().NotBeNull();

            var getAllMedicationReminderResponse = await Client.GetAsync(RequestUri);
            getAllMedicationReminderResponse.EnsureSuccessStatusCode();

            var getAllMedicationReminderResponseString =
                await getAllMedicationReminderResponse.Content.ReadAsStringAsync();
            var getAllMedicationReminderResult =
                JsonConvert.DeserializeObject<GetAllMedicationRemindersResponse>(
                    getAllMedicationReminderResponseString);

            getAllMedicationReminderResult.Should().NotBeNull();
            getAllMedicationReminderResult?.MedicationReminders.Count.Should().Be(3);
        }

        [Fact]
        public async Task
            When_UpdateMedicationReminderCommandIsCalled_Then_GetAllMedicationReminderQueryHandlerIsCalled_Then_Success()
        {
            var response = await Client.GetAsync(RequestUri);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<GetAllMedicationRemindersResponse>(responseString);

            result.Should().NotBeNull();

            var token = CreateToken();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var medicationReminder = new UpdateMedicationReminderCommand()
            {
                Id = result!.MedicationReminders.First().Id,
                DayInterval = 1u,
                Dosage = 1u,
                StartDate = DateTime.Now,
                EndDate = DateTime.MaxValue,
                HourList = new List<float>
                {
                    1f
                },
                MedicationId = result!.MedicationReminders.First().MedicationId,
            };
            var updateMedicationReminderResponse = await Client.PutAsJsonAsync(RequestUri, medicationReminder);
            updateMedicationReminderResponse.EnsureSuccessStatusCode();

            var updateMedicationReminderResponseString = await updateMedicationReminderResponse.Content.ReadAsStringAsync();
            var updateMedicationReminderResult = JsonConvert.DeserializeObject<UpdateMedicationReminderCommandResponse>(updateMedicationReminderResponseString);

            updateMedicationReminderResult.Should().NotBeNull();

            var getAllMedicationReminderResponse = await Client.GetAsync(RequestUri);
            getAllMedicationReminderResponse.EnsureSuccessStatusCode();

            var getAllMedicationReminderResponseString =
                await getAllMedicationReminderResponse.Content.ReadAsStringAsync();
            var getAllMedicationReminderResult =
                JsonConvert.DeserializeObject<GetAllMedicationRemindersResponse>(
                    getAllMedicationReminderResponseString);

            getAllMedicationReminderResult.Should().NotBeNull();
            getAllMedicationReminderResult!.MedicationReminders.First().MedicationId.Should()
                .Be(medicationReminder.MedicationId.ToString());
        }

        private static string CreateToken()
        {

            return JwtTokenProvider.JwtSecurityTokenHandler.WriteToken(
                new JwtSecurityToken(
                    JwtTokenProvider.Issuer,
                    JwtTokenProvider.Issuer,
                    new List<Claim> { new(ClaimTypes.Role, "User") },
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: JwtTokenProvider.SigningCredentials
                ));
        }
    }
}
