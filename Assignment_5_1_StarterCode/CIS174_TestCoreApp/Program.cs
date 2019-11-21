using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Formatting.Compact;

namespace CIS174_TestCoreApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                            .Enrich.FromLogContext()
                            .WriteTo.Console(new CompactJsonFormatter())
                            .WriteTo.File(new CompactJsonFormatter(), "./logs/myapp.json")
                            .CreateLogger();

           


            try
            {
                Log.Information("Starting up");
                CreateWebHostBuilder(args).Build().Run();

            }catch(Exception ex)
            {
                Log.Fatal(ex, "Host terminated unxpectedly");
            }finally
            {
                Log.CloseAndFlush();
            }
           
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                   .UseSerilog()
                   .UseStartup<Startup>();
    }
}
