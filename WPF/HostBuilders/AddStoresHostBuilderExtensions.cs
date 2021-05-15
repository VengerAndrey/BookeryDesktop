using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WPF.State.Navigators;

namespace WPF.HostBuilders
{
    public static class AddStoresHostBuilderExtensions
    {
        public static IHostBuilder AddStores(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices(services => { services.AddSingleton<INavigator, Navigator>(); });

            return hostBuilder;
        }
    }
}