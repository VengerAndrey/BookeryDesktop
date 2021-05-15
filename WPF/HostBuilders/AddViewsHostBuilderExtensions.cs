using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WPF.ViewModels;

namespace WPF.HostBuilders
{
    public static class AddViewsHostBuilderExtensions
    {
        public static IHostBuilder AddViews(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices(services =>
            {
                services.AddSingleton(serviceProvider =>
                    new MainWindow(serviceProvider.GetRequiredService<MainViewModel>()));
            });

            return hostBuilder;
        }
    }
}