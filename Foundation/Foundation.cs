using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Foundation.ConfigReader;
using Foundation.Csv.Reader;
using Foundation.Csv.Writer;

namespace Foundation
{
    /// <summary>
    /// Base class for an application, inherit and run ApplicationStart()
    /// </summary>
    public partial class ApplicationBase
    {
        public static IHost? _Host;
        /// <summary>
        /// Create the host builder, add any DI classes here.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
         static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                    services.AddSingleton<IConfigReaderFactory, ConfigReaderFactory>().
                    AddScoped<ICsvReader, CsvReader>()
                    .AddScoped<ICsvWriter, CsvWriter>()
                            );

        

        /// <summary>
        /// Run to create host and start
        /// </summary>
        /// <param name="args">Args from Main</param>
        protected static void ApplicationStart(string[] args)
        {

            _Host = CreateHostBuilder(args).Build();

            _Host.Start();
        }
    }
}
