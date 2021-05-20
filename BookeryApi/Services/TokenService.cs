﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.DTOs.Requests;
using Domain.Models.DTOs.Responses;

namespace BookeryApi.Services
{
    public class TokenService : ITokenService
    {
        private readonly HttpClient _httpClient;

        public TokenService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:42396/api/Authentication/");
        }

        public async Task<AuthenticationResponse> GetToken(AuthenticationRequest authenticationRequest)
        {
            var response = await _httpClient.PostAsJsonAsync("token", authenticationRequest)
                .ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<AuthenticationResponse>();
            }

            return null;
        }

        public async Task<AuthenticationResponse> RefreshToken(string accessToken, string refreshToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", accessToken);

            var refreshTokenRequest = new RefreshTokenRequest
            {
                RefreshToken = refreshToken
            };

            var response = await _httpClient.PostAsJsonAsync("refresh-token", refreshTokenRequest);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<AuthenticationResponse>();
            }

            return null;
        }
    }
}
