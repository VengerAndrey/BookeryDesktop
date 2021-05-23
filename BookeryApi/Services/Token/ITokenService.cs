using System.Threading.Tasks;
using Domain.Models.DTOs.Requests;
using Domain.Models.DTOs.Responses;

namespace BookeryApi.Services.Token
{
    public interface ITokenService
    {
        Task<AuthenticationResponse> GetToken(AuthenticationRequest authenticationRequest);
        Task<AuthenticationResponse> RefreshToken(string accessToken, string refreshToken);
    }
}