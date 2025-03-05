using Hangfire;
using Hangfire.Dashboard;
using MailAPI.API.HangfireAuth;
using MailAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<DataContext>(opt => opt
    .UseSqlServer(builder.Configuration.GetConnectionString("DataContextCon") ??
        throw new InvalidOperationException("ConnectionString 'DataContextCon' not found")));

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
