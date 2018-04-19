using CFlix.Data;
using CFlix.Extensions;
using CFlix.Models;
using CFlix.Services;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CFlix
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (HandleCLIArgs(args))
                return;

            var host = WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureAppConfiguration((hostContext, config) =>
                {
                    config.AddFromZipRessource("settings", "appsettings.json");
                })
                .Build();

            host.Run();
        }

        private static bool HandleCLIArgs(string[] args)
        {
            if (args.FirstOrDefault() == "database")
            {
                if (args.ElementAtOrDefault(1) == "update")
                {
                    switch (args.ElementAtOrDefault(2))
                    {
                        case "mysql":
                            ExecuteDBUpdate(true, false);
                            break;
                        case "postgres":
                            ExecuteDBUpdate(false, true);
                            break;
                        case "all":
                        default:
                            ExecuteDBUpdate(true, true);
                            break;
                    }

                    return true;
                }
            }

            return false;
        }

        private static void ExecuteDBUpdate(bool updateCflixDB, bool updateCFlixDB)
        {
            var host = WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>()
                .ConfigureServices(svc => svc.AddScoped<CFlixAuthInitializer>())
                .ConfigureServices(svc => svc.AddScoped<CFlixInitializer>())
                .Build();

            using (var scope = host.Services.CreateScope())
            {
                var provider = scope.ServiceProvider;
                
                var mysqlDB = provider.GetService<CFlixInitializer>();
                var postgresDB = provider.GetService<CFlixAuthInitializer>();

                var loggerFactory = provider.GetService<ILoggerFactory>();

                var settings = new Microsoft.Extensions.Logging.Console.ConsoleLoggerSettings();
                settings.Switches.Add("Default", LogLevel.Warning);
                settings.Switches.Add("CFlix", LogLevel.Information);

                loggerFactory.AddConsole(settings);

                var tasks = new List<Task>();
                if (updateCflixDB)
                {
                    tasks.Add(mysqlDB.Seed());
                }

                if (updateCFlixDB)
                {
                    tasks.Add(postgresDB.Seed());
                }

                Task.WaitAll(tasks.ToArray());
            }
        }
    }
}
