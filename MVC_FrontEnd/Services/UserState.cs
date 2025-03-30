
public class UserState
{
    private readonly Blazored.SessionStorage.ISessionStorageService _sessionStorage;

    public UserState(Blazored.SessionStorage.ISessionStorageService sessionStorage)
    {
        _sessionStorage = sessionStorage;
    }

    public string UserRole { get; private set; } = string.Empty;

    public event Action OnChange;

    public async Task SetUserRoleAsync(string role)
    {
        UserRole = role;
        await _sessionStorage.SetItemAsync("UserRole", role); // Save to session storage
        NotifyStateChanged();
    }

    public async Task LoadUserRoleAsync()
    {
        UserRole = await _sessionStorage.GetItemAsync<string>("UserRole") ?? string.Empty;
        NotifyStateChanged();
    }

    public void NotifyStateChanged()
    {
        OnChange?.Invoke();
    }
}
