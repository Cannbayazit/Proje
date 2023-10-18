using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazored.LocalStorage;

namespace Proje.Extensions.LocalStorage
{
    public static class AsyncLocalStorageExtension
    {
        public static async Task<string> getJwtAsync(this ILocalStorageService localStorage)
        {
            return await localStorage.GetItemAsStringAsync("JWT");
        }
         public static async Task setJwtAsync(this ILocalStorageService localStorage,string token)
        {
             await localStorage.SetItemAsStringAsync("JWT",token);
        }
        public static async Task removeJwtAsync(this ILocalStorageService localStorage)
        {
             await localStorage.RemoveItemAsync("JWT");
        }
    }
}