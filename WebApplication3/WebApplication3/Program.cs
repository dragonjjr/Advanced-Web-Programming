using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using Serilog;
using System.IO;

namespace WebApplication3
{
    public class Program
    {
        public static void Main(string[] args)
        {

            Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Debug()
               .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
               .Enrich.FromLogContext()
               .WriteTo.File(@".\Logs\APILog\" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt")
               .WriteTo.Seq("http://localhost:5341/")
               .CreateLogger();
            var files = new DirectoryInfo(@".\Logs\APILog").GetFiles("*.txt");
            //Delete log file
            foreach (var file in files)
            {
                if (file.CreationTime < DateTime.Now.AddDays(-1))
                {
                    File.Delete(file.FullName);
                }
            }


            try
            {
                //Log.Information("Starting up service");
                CreateHostBuilder(args).Build().Run();
            }
            catch (System.Exception ex)
            {
                Log.Fatal(ex, "Error Occured in service");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });     
    }
}
