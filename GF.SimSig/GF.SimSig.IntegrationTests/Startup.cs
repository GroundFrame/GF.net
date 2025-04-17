using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Serilog.Sinks.XUnit.Injectable.Abstract;
using Serilog.Sinks.XUnit.Injectable;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog.Sinks.XUnit.Injectable.Extensions;

namespace GF.SimSig.IntegrationTests
{
    public class Startup
    {
        /// <summary>
        /// Configurers the services needed to run the unit tests
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureServices(IServiceCollection services)
        {
            var injectableTestOutputSink = new InjectableTestOutputSink();

            //build config
            IConfiguration config = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json")
               .AddEnvironmentVariables()
               .Build();

            //build logger
            Serilog.ILogger logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.InjectableTestOutput(injectableTestOutputSink, Serilog.Events.LogEventLevel.Information)
                .CreateLogger();

            //map register classes to MongoDb

            services.AddOptions();
            services.AddSingleton<Serilog.ILogger>(logger);
            services.AddSingleton<IInjectableTestOutputSink>(injectableTestOutputSink);
            services.AddSingleton<IConfiguration>(config);
            services.AddSingleton<MongoClient>(s => { return new MongoClient(config.GetConnectionString("MongoDb")); });
        }
    }
}
