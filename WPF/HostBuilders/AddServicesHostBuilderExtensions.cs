using System.Linq;
using System.Reflection;
using BookeryApi.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using WPF.Services;

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
                services.AddHostedService<HostedTokenService>();
            });

            return hostBuilder;
        }
    }
}