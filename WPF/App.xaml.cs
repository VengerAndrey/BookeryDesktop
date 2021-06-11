using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WPF.Common;
using WPF.HostBuilders;
using WPF.State.Authentication;

namespace WPF
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;
        private readonly TempDataSupervisor _tempDataSupervisor;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .AddConfiguration()
                .AddServices()
                .AddStores()
                .AddViewModels()
                .AddViews()
                .Build();
            _tempDataSupervisor = new TempDataSupervisor();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();
            _tempDataSupervisor.Start();

            Window window = _host.Services.GetRequiredService<MainWindow>();
            window.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            var authenticator = _host.Services.GetRequiredService<IAuthenticator>();
            authenticator.LogOut();

            await _host.StopAsync();
            _host.Dispose();
            _tempDataSupervisor.Dispose();

            base.OnExit(e);
        }
    }
}