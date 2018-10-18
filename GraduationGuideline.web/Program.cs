using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GraduationGuideline.data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GraduationGuideline.web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IWebHost host = CreateWebHostBuilder(args).Build();
            using (IServiceScope scope = host.Services.CreateScope()) {
                IServiceProvider services = scope.ServiceProvider;
                var context = ((GraduationGuidelineContext)services.GetService(typeof(GraduationGuidelineContext)));
                var provider = context.ProviderName;

                //if not an InMemory database, migrate

                if (!provider.Contains("InMemory")) {
                    ((GraduationGuidelineContext)services.GetService(typeof(GraduationGuidelineContext))).Migrate();
                }
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
