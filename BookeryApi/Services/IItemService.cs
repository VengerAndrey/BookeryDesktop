﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace BookeryApi.Services
{
    public interface IItemService
    {
        Task<List<Item>> GetSubItems(string path);
        Task<Item> CreateDirectory(string path);
        Task<Item> UploadFile(string path, MultipartFormDataContent content);
        Task<Stream> DownloadFile(string path); 
        void SetBearerToken(string accessToken);
    }
}
