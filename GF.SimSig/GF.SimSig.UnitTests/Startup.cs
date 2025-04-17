using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Sinks.XUnit.Injectable;
using Serilog.Sinks.XUnit.Injectable.Abstract;
using Serilog.Sinks.XUnit.Injectable.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF.SimSig.UnitTests
{
    public class Startup
    {
        /// <summary>
        /// Configurers the services needed to run the unit tests
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureServices(IServiceCollection services)
        {
            //instantiate new injectable test output sink
            var injectableTestOutputSink = new InjectableTestOutputSink();

            //configure logger
            Serilog.ILogger logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.InjectableTestOutput(injectableTestOutputSink, Serilog.Events.LogEventLevel.Verbose)
                .CreateLogger();

            services.AddSingleton<Serilog.ILogger>(logger);
            services.AddSingleton<IInjectableTestOutputSink>(injectableTestOutputSink);
        }
    }
}
