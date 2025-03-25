using FluentValidation;
using MailAPI.Application.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace MailAPI.Application
{
    public static class ApplicationDependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services) 
        {
            services.AddMediatR(cfg => cfg
            .RegisterServicesFromAssembly(typeof(ApplicationDependencyInjection).Assembly)
            .AddOpenBehavior(typeof(GlobalValidationBehavior<,>)));

            services.AddValidatorsFromAssembly(typeof(ApplicationDependencyInjection).Assembly);

            return services;
        }
    }
}