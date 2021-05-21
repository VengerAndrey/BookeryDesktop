using BookeryApi.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WPF.HostBuilders
{
    public static class AddServicesHostBuilderExtensions
    {
        public static IHostBuilder AddServices(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices(services =>
            {
                services.AddSingleton<IShareService, ShareService>();
                services.AddSingleton<IItemService, ItemService>();

                services.AddSingleton<ITokenService, TokenService>();
            });

            return hostBuilder;
        }
    }
}