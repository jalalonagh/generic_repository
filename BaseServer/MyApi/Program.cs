using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using System;
using System.IO;

namespace Refah
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

            try
            {
                logger.Debug("init main");
                CreateWebHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), "errorStart.txt"), Newtonsoft.Json.JsonConvert.SerializeObject(ex));
                logger.Error(ex, "Stopped program because of exception");
                throw;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
        }

    //    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
    //WebHost.CreateDefaultBuilder(args)
    //    .UseStartup<Startup>()
    //    .ConfigureLogging(logging =>
    //    {
    //        logging.ClearProviders();
    //        logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
    //    })
    //    .UseNLog();  // NLog: setup NLog for Dependency injection

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
               WebHost.CreateDefaultBuilder(args)
            .ConfigureLogging(options =>
            {
                options.ClearProviders();
                options.SetMinimumLevel(LogLevel.Trace);
            })
            .UseIISIntegration()
            .UseStartup<Startup>()
            .UseNLog();
    }
}
