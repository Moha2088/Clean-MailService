using Hangfire.Annotations;
using Hangfire.Dashboard;

namespace MailAPI.Presentation.HangfireAuth;

public class DashboardAuthorizationFilter : IDashboardAuthorizationFilter
{
    public bool Authorize([NotNull] DashboardContext context)
    {
        return true;
    }
}