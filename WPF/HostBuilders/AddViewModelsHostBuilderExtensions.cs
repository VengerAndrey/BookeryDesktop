using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WPF.ViewModels;
using WPF.ViewModels.Factories;

namespace WPF.HostBuilders
{
    public static class AddViewModelsHostBuilderExtensions
    {
        public static IHostBuilder AddViewModels(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices(services =>
            {
                services.AddTransient<ItemsViewModel>();
                services.AddTransient<MockViewModel>();
                services.AddTransient<MainViewModel>();

                services.AddSingleton<CreateViewModel<ItemsViewModel>>(serviceProvider =>
                    serviceProvider.GetRequiredService<ItemsViewModel>);
                services.AddSingleton<CreateViewModel<MockViewModel>>(serviceProvider =>
                    serviceProvider.GetRequiredService<MockViewModel>);

                services.AddSingleton<IViewModelFactory, ViewModelFactory>();
            });

            return hostBuilder;
        }
    }
}