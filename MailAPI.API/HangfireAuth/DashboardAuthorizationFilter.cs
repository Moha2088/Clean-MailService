using Hangfire.Annotations;
using Hangfire.Dashboard;

namespace MailAPI.API.HangfireAuth;

public class DashboardAuthorizationFilter : IDashboardAuthorizationFilter
{
    public bool Authorize([NotNull] DashboardContext context)
    {
        throw new NotImplementedException();
    }
}