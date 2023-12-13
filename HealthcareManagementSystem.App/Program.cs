using System.Text.Json;
using System.Text.Json.Serialization;
using Blazored.LocalStorage;
using HealthcareManagementSystem.App;
using HealthcareManagementSystem.App.Auth;
using HealthcareManagementSystem.App.Contracts;
using HealthcareManagementSystem.App.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage(config =>
{
	config.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
	config.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
	config.JsonSerializerOptions.IgnoreReadOnlyProperties = true;
	config.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
	config.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
	config.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;
	config.JsonSerializerOptions.WriteIndented = false;
});
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<CustomStateProvider>();
builder.Services.AddHttpClient<IMedicDataService, MedicDataService>(client =>
{
	client.BaseAddress = new Uri("https://localhost:7041/");
});
builder.Services.AddHttpClient<IUserDataService, UserDataService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7041/");
});
builder.Services.AddHttpClient<IMedicationDataService, MedicationDataService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7041/");
});
builder.Services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<CustomStateProvider>());

builder.Services.AddHttpClient<IAuthenticationService, AuthenticationService>(client =>
{
	client.BaseAddress = new Uri("https://localhost:7041/");
});

await builder.Build().RunAsync();
