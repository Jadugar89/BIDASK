using BIDASK.Shared;
using Blazored.Toast.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BIDASK.Client.Services
{
    public class GetProfil : IGetProfil
    {
        private readonly IToastService _toastService;
        private readonly HttpClient _http;

        public GetProfil(IToastService toastService, HttpClient http)
        {
            _toastService = toastService;
            _http = http;
        }

        public UserProfil _UserProfil { get ; set; }
       

        public async Task LoadUserProfilAsync()
        {
            if (_UserProfil == null)
            {
                _UserProfil = await _http.GetFromJsonAsync<UserProfil>("UserProfil");
            }
        }

        public async Task UpdateUserProfilAsync()
        {

                var result = await _http.PutAsJsonAsync("UserProfil", _UserProfil);
                _UserProfil= await result.Content.ReadFromJsonAsync<UserProfil>();
        }
    }
}
