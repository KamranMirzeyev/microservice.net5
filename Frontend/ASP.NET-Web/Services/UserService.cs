﻿using ASP.NET_Web.Models;
using ASP.NET_Web.Services.Interfaces;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ASP.NET_Web.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _client;

        public UserService(HttpClient client)
        {
            _client = client;
        }

        public async Task<UserViewModel> GetUser()
        {
            return await _client.GetFromJsonAsync<UserViewModel>("/api/user/getuser");
        }
    }
}
