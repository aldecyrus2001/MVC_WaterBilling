// AuthenticationService.cs
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

public class AuthenticationService
{
    private readonly Blazored.SessionStorage.ISessionStorageService  _sessionStorage;
    private readonly NavigationManager _navManager;

    public AuthenticationService(Blazored.SessionStorage.ISessionStorageService sessionStorage, NavigationManager navManager)
    {
        _sessionStorage = sessionStorage;
        _navManager = navManager;
    }

    public async Task CheckAuthenticationAsync()
    {
        var userId = await _sessionStorage.GetItemAsync<string>("userId");
        var token = await _sessionStorage.GetItemAsync<string>("token");
        var role = await _sessionStorage.GetItemAsync<string>("role");

        if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
        {
            _navManager.NavigateTo("/");  // Redirect to the login page
        }
    }
}
