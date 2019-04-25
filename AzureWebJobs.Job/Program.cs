using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace AzureWebJobs.Job
{
    class Program
    {
        static async Task Main()
        {
            await new HostBuilder()
               .ConfigureHostConfiguration(builder =>
               {
                   builder.SetBasePath(Directory.GetCurrentDirectory())
                       .AddJsonFile("appsettings.json", false, true);
#if DEBUG
                   builder.AddUserSecrets<Program>();
#endif
               })
               .ConfigureLogging((context, builder) =>
               {
                   builder.AddConfiguration(context.Configuration.GetSection("Logging"));
                   builder.AddApplicationInsights(o => o.InstrumentationKey = context.Configuration["ApplicationInsights:InstrumentationKey"]);
               })
               .ConfigureServices((context, services) =>
               {
                   services.AddSingleton<Functions>();
               })
               .ConfigureWebJobs(builder => builder.AddAzureStorage())
               .RunConsoleAsync();
        }
    }
}
