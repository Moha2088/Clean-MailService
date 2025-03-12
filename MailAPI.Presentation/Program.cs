using Hangfire;
using Scalar.AspNetCore;
using MailAPI.Application;
using MailAPI.Infrastructure;
using Serilog;
using MailAPI.Presentation;
using MailAPI.Presentation.HangfireAuth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MailAPI.Presentation.ScalarOptions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddOpenApi("v1", x => x.AddDocumentTransformer<BearerSecuritySchemeTransformer>());

builder.Services
    .AddInfrastructureServices(builder)
    .AddApplicationServices()
    .AddPresentationServices();

builder.Host.UseSerilog((context, config) => config
    .ReadFrom.Configuration(context.Configuration));


builder.Services.AddAuthorization()
    .AddAuthentication(opt =>
    {
        opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(opt =>
    {
        opt.SaveToken = true;
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JWTSettings:Issuer"],
            ValidAudience = builder.Configuration["JWTSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTSettings:Key"] ??
                           throw new ArgumentException("SigningKey not found!")))
        };
    });

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