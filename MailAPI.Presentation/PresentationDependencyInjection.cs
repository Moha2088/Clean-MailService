using MailAPI.Application.Interfaces.Email;
using MailAPI.Application.Interfaces.User;
using MailAPI.Presentation.Services;

namespace MailAPI.Presentation;

public static class PresentationDependencyInjection
{
    public static IServiceCollection AddPresentationServices(this IServiceCollection services)
    {
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IUserService, UserService>();

        return services;
    }
}
