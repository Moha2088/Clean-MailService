using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using MailAPI.Infrastructure.Data;
using MailAPI.Presentation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.MsSql;

namespace MailAPI.Tests.IntegrationTests.Factory
{
    public class CustomWebApplicationFactory : WebApplicationFactory<IApiMarker>, IAsyncLifetime
    {
        private readonly MsSqlContainer _container = new MsSqlBuilder().Build();


        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(service =>
            {
                var existingDbContext = service
                    .SingleOrDefault(x => x.ServiceType == typeof(DbContextOptions<DataContext>));

                if (existingDbContext != null)
                {
                    service.Remove(existingDbContext);
                }

                service.AddDbContext<DataContext>(opt => { opt.UseSqlServer(_container.GetConnectionString()); });

                var authScheme = service.SingleOrDefault(x => x.ServiceType == typeof(IAuthenticationSchemeProvider));

                if (authScheme != null)
                {
                    service.Remove(authScheme);
                }

                var authenticationSchemeName = "TestAuthenticationScheme";

                service.AddAuthentication(opt =>
                {
                    opt.DefaultAuthenticateScheme = authenticationSchemeName;
                }).AddScheme<AuthenticationSchemeOptions, TestAuthenticationHandler>(authenticationSchemeName, null);
            });

            base.ConfigureWebHost(builder);
        }

        public async Task InitializeAsync()
        {
            await _container.StartAsync();
            using var scope = Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<DataContext>();

            var pendingMigrations = await context.Database.GetPendingMigrationsAsync();

            if (pendingMigrations.Any())
            {
                await context.Database.MigrateAsync();
            }
        }

        async Task IAsyncLifetime.DisposeAsync()
        {
            await _container.DisposeAsync();
        }
    }
}