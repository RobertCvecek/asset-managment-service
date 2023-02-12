using Autofac.Extensions.DependencyInjection;
using NLog;
using NLog.Web;
using SolveX.AssetManagment;

LogManager.Setup().LoadConfigurationFromAppSettings();

try
{
    Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseNLog()
                .Build()
                .Run();
}
catch
{
    throw;
}
finally
{
    LogManager.Flush();
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    LogManager.Shutdown();
}