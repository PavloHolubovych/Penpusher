using System.Web;
using Hangfire;
using Ninject;
using Owin;
using Penpusher.Services.ContentService;

namespace Penpusher
{
    public static class HangfireStartup
    {
        public static void InitHangfire(this IAppBuilder app)
        {
            var options = new DashboardOptions { AppPath = VirtualPathUtility.ToAbsolute("~") };
            app.UseHangfireDashboard("/jobs", options);
            app.UseHangfireServer(
                new BackgroundJobServerOptions { Activator = new NinjectJobActivator(NinjectWebCommon.Kernel) });

            var artService = NinjectWebCommon.Kernel.Get<IProviderTrackingService>();
            RecurringJob.AddOrUpdate(
            "Update articles from news providers",
            () => artService.UpdateArticlesFromNewsProviders(),
            Cron.Daily);
        }
    }
}