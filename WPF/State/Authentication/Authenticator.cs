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
        private readonly IAuthenticationService _authenticationService;
        private readonly IItemService _itemService;
        private readonly IPhotoService _photoService;
        private readonly IShareService _shareService;
        private readonly IUserService _userService;

        private AuthenticationResponse _currentAuthenticationResponse;

        private Timer _timer;

        public Authenticator(IAuthenticationService authenticationService, IShareService shareService,
            IItemService itemService,
            IUserService userService, IAccessService accessService, IPhotoService photoService)
        {
            _authenticationService = authenticationService;
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

        public async Task<SignUpResult> SignUp(string email, string username, string password)
        {
            return await _authenticationService.SignUp(email, username, password);
        }

        public void LogOut()
        {
            _authenticationService.LogOut();
            _timer?.Change(Timeout.Infinite, 0);
            _currentAuthenticationResponse = null;
            StateChanged?.Invoke();
        }

        private async Task Authenticate(AuthenticationRequest authenticationRequest)
        {
            _currentAuthenticationResponse = await _authenticationService.GetToken(authenticationRequest);
            SetBearerTokenToServices();
        }

        private async Task RefreshToken()
        {
            _currentAuthenticationResponse = await _authenticationService
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