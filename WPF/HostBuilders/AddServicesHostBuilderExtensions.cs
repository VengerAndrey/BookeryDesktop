using BookeryApi.Services.Storage;
using BookeryApi.Services.Token;
using BookeryApi.Services.User;
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
                services.AddSingleton<IUserService, UserService>();
                services.AddSingleton<IAccessService, AccessService>();

                services.AddSingleton<ITokenService, TokenService>();
            });

            return hostBuilder;
        }
    }
}