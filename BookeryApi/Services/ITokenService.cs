using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.DTOs.Requests;
using Domain.Models.DTOs.Responses;

namespace BookeryApi.Services
{
    public interface ITokenService
    {
        Task<AuthenticationResponse> GetToken(AuthenticationRequest authenticationRequest);
        Task<AuthenticationResponse> RefreshToken(string accessToken, string refreshToken);
    }
}
