
namespace MailAPI.Presentation;

public static class PresentationDependencyInjection
{
    public static IServiceCollection AddPresentationServices(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddHealthChecks()
            .AddSqlServer(connectionString: builder.Configuration.GetConnectionString("DataContextCon") ?? throw new InvalidOperationException("Connection string not found!"),
            name: "DBHealthCheck",
            tags: ["localdb"]);

        return services;
    }
}
