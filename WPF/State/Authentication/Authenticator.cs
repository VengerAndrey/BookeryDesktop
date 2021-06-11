using System;
using System.Threading;
using System.Threading.Tasks;
using BookeryApi.Services.Storage;
using BookeryApi.Services.Token;
using BookeryApi.Services.User;
using Domain.Models.DTOs.Requests;
using Domain.Models.DTOs.Responses;

namespace WPF.State.Authentication
{
    internal class Authenticator : IAuthenticator
    {
        private readonly IAccessService _accessService;
        private readonly IItemService _itemService;
        private readonly IPhotoService _photoService;
        private readonly IShareService _shareService;
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;

        private AuthenticationResponse _currentAuthenticationResponse;

        private Timer _timer;

        public Authenticator(ITokenService tokenService, IShareService shareService, IItemService itemService,
            IUserService userService, IAccessService accessService, IPhotoService photoService)
        {
            _tokenService = tokenService;
            _shareService = shareService;
            _itemService = itemService;
            _userService = userService;
            _accessService = accessService;
            _photoService = photoService;
        }

        public bool IsLoggedIn => _currentAuthenticationResponse != null;
        public event Action StateChanged;

        public async Task Login(string email, string password)
        {
            await Authenticate(new AuthenticationRequest {Email = email, Password = password});
            _timer = new Timer(async o => { await RefreshToken(); }, null, TimeSpan.Zero,
                _currentAuthenticationResponse.ExpireAt - DateTime.UtcNow);
            StateChanged?.Invoke();
        }

        public void Logout()
        {
            _timer?.Change(Timeout.Infinite, 0);
            _currentAuthenticationResponse = null;
            StateChanged?.Invoke();
        }

        private async Task Authenticate(AuthenticationRequest authenticationRequest)
        {
            _currentAuthenticationResponse = await _tokenService.GetToken(authenticationRequest);
            SetBearerTokenToServices();
        }

        private async Task RefreshToken()
        {
            _currentAuthenticationResponse = await _tokenService
                .RefreshToken(_currentAuthenticationResponse.AccessToken, _currentAuthenticationResponse.RefreshToken);
            SetBearerTokenToServices();
        }

        private void SetBearerTokenToServices()
        {
            _shareService.SetBearerToken(_currentAuthenticationResponse.AccessToken);
            _itemService.SetBearerToken(_currentAuthenticationResponse.AccessToken);
            _shareService.SetBearerToken(_currentAuthenticationResponse.AccessToken);
            _userService.SetBearerToken(_currentAuthenticationResponse.AccessToken);
            _accessService.SetBearerToken(_currentAuthenticationResponse.AccessToken);
            _photoService.SetBearerToken(_currentAuthenticationResponse.AccessToken);
        }
    }
}