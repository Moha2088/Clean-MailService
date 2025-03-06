using Hangfire;
using MailAPI.API.HangfireAuth;
using Scalar.AspNetCore;
using MailAPI.Application;
using MailAPI.Infrastructure;
using Serilog;
using MailAPI.Presentation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services
    .AddInfrastructureServices(builder)
    .AddApplicationServices()
    .AddPresentationServices();

builder.Host.UseSerilog((context, config) => config
    .ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(opt => opt
    .WithTitle("MailAPI")
    .WithTheme(ScalarTheme.BluePlanet)
    .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient));
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseHangfireDashboard(options: new DashboardOptions
{
    Authorization =
    [
        new DashboardAuthorizationFilter()
    ],

    DarkModeEnabled = false,
    DashboardTitle = "Hangfire Dashboard"
});

app.MapControllers();

app.Run();
