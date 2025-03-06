﻿using Hangfire;
using MailAPI.Application.Interfaces.Email;
using MailAPI.Application.Interfaces.User;
using MailAPI.Domain.Entities.Dtos;
using MailAPI.Infrastructure.Data;
using MailAPI.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MailAPI.Infrastructure
{
    public static class InfrastructureDependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, WebApplicationBuilder builder)
        {
            var hangfireConnectionString = builder.Configuration.GetConnectionString("HangfireCon");

            #region DbContext

            services.AddDbContext<DataContext>(opt => opt
                .UseSqlServer(builder.Configuration.GetConnectionString("DataContextCon") ??
                    throw new InvalidOperationException("ConnectionString 'DataContextCon' not found")));

            services.AddScoped<IEmailRepository, EmailRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            #endregion

            #region Hangfire

            services.AddHangfire(x => x
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(hangfireConnectionString));
            
            services.AddHangfireServer();

            #endregion

            #region Automapper

            services.AddAutoMapper(typeof(MapperProfile));

            #endregion

            return services;
        }
    }
}