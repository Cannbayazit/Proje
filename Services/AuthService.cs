using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Proje.Extensions.LocalStorage;

namespace Proje.Services
{
    public class AuthService : AuthenticationStateProvider
    {
        private readonly ILocalStorageService LocalStorage;
        private AuthenticationState AnonymousUser { get; set; }

        
        public AuthService(ILocalStorageService localStorage)
        {
         LocalStorage = localStorage;
         AnonymousUser = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())); 
        }

        public AuthenticationState createAuthUser(string token)
        {
           return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(decodeJWT(token).Claims , "JWT")));
        }
          public JwtSecurityToken decodeJWT(string token)
        {
            return new JwtSecurityTokenHandler().ReadJwtToken(token);
        }
          public void notifyLogin(string? token)
        {
           if (!String.IsNullOrEmpty(token))
           {
            NotifyAuthenticationStateChanged(Task.FromResult(createAuthUser(token)));
           }
        }
          public void notifyLogout(string? token)
        {
            NotifyAuthenticationStateChanged(Task.FromResult(AnonymousUser));
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token =await LocalStorage.getJwtAsync();
            if (String.IsNullOrEmpty(token))
            {
                return AnonymousUser;
            }
            return createAuthUser(token);
        }
    }
}