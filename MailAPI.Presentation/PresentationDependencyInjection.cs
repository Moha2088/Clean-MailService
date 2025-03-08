using MailAPI.Application.Interfaces.Email;
using MailAPI.Application.Interfaces.User;

namespace MailAPI.Presentation;

public static class PresentationDependencyInjection
{
    public static IServiceCollection AddPresentationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg
            .RegisterServicesFromAssembly(typeof(Program).Assembly));
    
        return services;
    }
}
