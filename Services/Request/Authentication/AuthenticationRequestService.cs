using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace Proje.Services.Request.Authentication
{
    public class AuthenticationRequestService : IAuthenticationRequestService
    {
         private readonly AuthService AuthenticationState;
         private readonly ILocalStorageService LocalStorage;
         public HttpClient HttpClient { get; set; }


         public AuthenticationRequestService(AuthenticationStateProvider authService,ILocalStorageService localStorage,HttpClient httpClient)
         {
            AuthenticationState = (authService as AuthService);
            LocalStorage = localStorage;
            HttpClient = httpClient;
         }

         public async Task Login(string username,string password)
         {
            var response = await HttpClient.PostAsync("/api/login",JsonContent.Create(new {username =username,password=password}));
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
            }
         }

    }
}