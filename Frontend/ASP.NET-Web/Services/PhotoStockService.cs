﻿using ASP.NET_Web.Models.PhotoStocks;
using ASP.NET_Web.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Shared.DTO;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ASP.NET_Web.Services
{
    public class PhotoStockService : IPhotoStockService
    {
        private readonly HttpClient _httpClient;

        public PhotoStockService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> DeletePhoto(string photoUrl)
        {
            var response = await _httpClient.DeleteAsync($"photos?photoUrl={photoUrl}");
            return response.IsSuccessStatusCode;
        }

        public async Task<PhotoViewModel> UploadPhoto(IFormFile photo)
        {
            if (photo == null || photo.Length <= 0)
            {
                return null;
            }
            // örnek dosya ismi= 203802340234.jpg
            var randonFilename = $"{Guid.NewGuid().ToString()}{Path.GetExtension(photo.FileName)}";

            using var ms = new MemoryStream();

            await photo.CopyToAsync(ms);

            var multipartContent = new MultipartFormDataContent();

            multipartContent.Add(new ByteArrayContent(ms.ToArray()), "photo", randonFilename);

            var response = await _httpClient.PostAsync("photos", multipartContent);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var responseSuccess = await response.Content.ReadFromJsonAsync<Responce<PhotoViewModel>>();

            return responseSuccess.Data;
        }
    }
}
