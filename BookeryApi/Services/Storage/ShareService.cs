using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Domain.Models;

namespace BookeryApi.Services.Storage
{
    public class ShareService : IShareService
    {
        private readonly HttpClient _httpClient;

        public ShareService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:42396/api/Share/");
        }

        public async Task<IEnumerable<Share>> GetAll()
        {
            var response = await _httpClient.GetAsync("");

            if (response.IsSuccessStatusCode)
            {
                var shares = await response.Content.ReadAsAsync<IEnumerable<Share>>();

                return shares;
            }

            return null;
        }

        public async Task<Share> Get(Guid id)
        {
            var response = await _httpClient.GetAsync($"{id}");

            if (response.IsSuccessStatusCode)
            {
                var share = await response.Content.ReadAsAsync<Share>();

                return share;
            }

            return null;
        }

        public async Task<Share> Create(string name)
        {
            var response = await _httpClient.PostAsJsonAsync("", name);

            if (response.IsSuccessStatusCode)
            {
                var createdShare = await response.Content.ReadAsAsync<Share>();

                return createdShare;
            }

            return null;
        }

        public async Task<Share> Update(Share share)
        {
            var response = await _httpClient.PutAsJsonAsync("", share);

            if (response.IsSuccessStatusCode)
            {
                var updatedShare = await response.Content.ReadAsAsync<Share>();

                return updatedShare;
            }

            return null;
        }

        public async Task<bool> Delete(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"{id}");

            return response.IsSuccessStatusCode;
        }

        public void SetBearerToken(string accessToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }
    }
}