using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Domain.Models;

namespace BookeryApi.Services
{
    public class StorageService : IStorageService
    {
        private readonly HttpClient _httpClient;

        public StorageService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:42396/api/Storage/");
        }

        public async Task<List<Share>> GetAllShares()
        {
            var response = await _httpClient.GetAsync("");

            if (response.IsSuccessStatusCode)
            {
                var shares = await response.Content.ReadAsAsync<List<Share>>();

                return shares;
            }

            return null;
        }

        public async Task<Share> GetShare(Guid id)
        {
            var response = await _httpClient.GetAsync($"{id}");

            if (response.IsSuccessStatusCode)
            {
                var share = await response.Content.ReadAsAsync<Share>();

                return share;
            }

            return null;
        }

        public async Task<Share> CreateShare(string name)
        {
            var response = await _httpClient.PostAsJsonAsync("", name);

            if (response.IsSuccessStatusCode)
            {
                var createdShare = await response.Content.ReadAsAsync<Share>();

                return createdShare;
            }

            return null;
        }

        public async Task<Share> UpdateShare(Share share)
        {
            var response = await _httpClient.PutAsJsonAsync("", share);

            if (response.IsSuccessStatusCode)
            {
                var updatedShare = await response.Content.ReadAsAsync<Share>();

                return updatedShare;
            }

            return null;
        }

        public async Task<bool> DeleteShare(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"{id}");

            return response.IsSuccessStatusCode;
        }

        public async Task<List<Item>> GetSubItems(string path)
        {
            var response = await _httpClient.GetAsync($"sub/{path}");

            if (response.IsSuccessStatusCode)
            {
                var items = await response.Content.ReadAsAsync<List<Item>>();

                return items;
            }

            return null;
        }

        public async Task<Item> CreateDirectory(string path)
        {
            var response = await _httpClient.PostAsJsonAsync($"create-directory/{path}", "");

            if (response.IsSuccessStatusCode)
            {
                var createdDirectory = await response.Content.ReadAsAsync<Item>();

                return createdDirectory;
            }

            return null;
        }

        public async Task<bool> UploadFile(string path, MultipartFormDataContent content)
        {
            var response = await _httpClient.PostAsync($"upload/{path}", content);

            return response.IsSuccessStatusCode;
        }

        public async Task<Stream> DownloadFile(string path)
        {
            var response = await _httpClient.GetAsync($"download/{path}");

            if (response.IsSuccessStatusCode) return await response.Content.ReadAsStreamAsync();

            return null;
        }
    }
}