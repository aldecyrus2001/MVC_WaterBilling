using System;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.AspNetCore.Components;

namespace MVC_FrontEnd.Services
{
    public class URLStringServices
    {
        private readonly NavigationManager _navigationManager;

        public URLStringServices(NavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
        }

        public string GetURLStringValue(string key)
        {
            var uri = new Uri(_navigationManager.Uri);
            var query = QueryHelpers.ParseQuery(uri.Query);

            if (query.TryGetValue(key, out var value))
            {
                return value;
            }

            return null;

        }

        public int? GetURLStringValueAsInt(string key)
        {
            var value = GetURLStringValue(key);

            if(int.TryParse(value, out var result))
            {
                return result;
            }

            return null;
        }


    }
}
