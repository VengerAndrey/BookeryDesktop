using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BookeryApi.Services;
using Domain.Models.DTOs.Requests;
using Domain.Models.DTOs.Responses;
using Microsoft.Extensions.Hosting;

namespace WPF.Services
{
    public class HostedTokenService : IHostedService, IDisposable
    {
        private readonly AuthenticationRequest _authenticationRequest = new AuthenticationRequest
        {
            Email = "email@gmail.com",
            Password = "123"
        };

        private Timer _timer;
        private AuthenticationResponse _currentAuthenticationResponse;

        private readonly ITokenService _tokenService;
        private readonly IShareService _shareService;
        private readonly IItemService _itemService;

        public HostedTokenService(ITokenService tokenService, IShareService shareService, IItemService itemService)
        {
            _tokenService = tokenService;
            _shareService = shareService;
            _itemService = itemService;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await Authenticate().ConfigureAwait(false);
            _timer = new Timer(async o =>
            {
                await RefreshToken().ConfigureAwait(false);
            }, null, TimeSpan.Zero, _currentAuthenticationResponse.ExpireAt - DateTime.UtcNow);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        private async Task Authenticate()
        {
            _currentAuthenticationResponse = await _tokenService.GetToken(_authenticationRequest)
                .ConfigureAwait(false);
            SetBearerTokenToServices();
        }

        private async Task RefreshToken()
        {
            _currentAuthenticationResponse = await _tokenService
                .RefreshToken(_currentAuthenticationResponse.AccessToken, _currentAuthenticationResponse.RefreshToken)
                .ConfigureAwait(false);
            SetBearerTokenToServices();
        }

        private void SetBearerTokenToServices()
        {
            _shareService.SetBearerToken(_currentAuthenticationResponse.AccessToken);
            _itemService.SetBearerToken(_currentAuthenticationResponse.AccessToken);
        }
    }
}
