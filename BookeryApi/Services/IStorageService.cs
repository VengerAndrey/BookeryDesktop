using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Domain.Models;

namespace BookeryApi.Services
{
    public interface IStorageService
    {
        Task<List<Share>> GetAllShares();
        Task<Share> GetShare(Guid id);
        Task<Share> CreateShare(string name);
        Task<Share> UpdateShare(Share share);
        Task<bool> DeleteShare(Guid id);

        Task<List<Item>> GetSubItems(string path);
        Task<Item> CreateDirectory(string path);
        Task<bool> UploadFile(string path, MultipartFormDataContent content);
        Task<Stream> DownloadFile(string path);
    }
}