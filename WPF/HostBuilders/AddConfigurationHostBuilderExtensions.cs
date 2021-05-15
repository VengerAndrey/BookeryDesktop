using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace WPF.HostBuilders
{
    public static class AddConfigurationHostBuilderExtensions
    {
        public static IHostBuilder AddConfiguration(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureAppConfiguration(configurationBuilder =>
            {
                configurationBuilder.AddJsonFile("appsettings.json");
                configurationBuilder.AddEnvironmentVariables();
            });

            return hostBuilder;
        }
    }
}