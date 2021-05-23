using BookeryApi.Services.User;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WPF.State.Authentication;
using WPF.State.Navigation;
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
                services.AddSingleton<HomeViewModel>();
                services.AddTransient<UserViewModel>();

                services.AddTransient<MainViewModel>();

                services.AddSingleton<CreateViewModel<LoginViewModel>>(serviceProvider => () => new LoginViewModel(
                    serviceProvider.GetRequiredService<IAuthenticator>(),
                    serviceProvider.GetRequiredService<ViewModelRenavigator<HomeViewModel>>()));
                services.AddSingleton<CreateViewModel<HomeViewModel>>(serviceProvider =>
                    serviceProvider.GetRequiredService<HomeViewModel>);
                services.AddSingleton<CreateViewModel<UserViewModel>>(serviceProvider => () => new UserViewModel(
                    serviceProvider.GetRequiredService<IAuthenticator>(),
                    serviceProvider.GetRequiredService<ViewModelRenavigator<LoginViewModel>>(),
                    serviceProvider.GetRequiredService<IUserService>()));

                services.AddSingleton<IViewModelFactory, ViewModelFactory>();

                services.AddSingleton<ViewModelRenavigator<LoginViewModel>>();
                services.AddSingleton<ViewModelRenavigator<HomeViewModel>>();
            });

            return hostBuilder;
        }
    }
}