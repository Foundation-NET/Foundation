using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Foundation.ConfigReader;

namespace Foundation
{
    
    public partial class ApplicationBase
    {
        public static IHost? _Host;

         static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                    services.AddSingleton<IConfigReaderFactory, ConfigReaderFactory>()
                            );

        

        // Run this first thing all ObjectBase derived classes rely on this.
        protected static void ApplicationStart(string[] args)
        {

            _Host = CreateHostBuilder(args).Build();

            _Host.Start();
        }
    }
}
