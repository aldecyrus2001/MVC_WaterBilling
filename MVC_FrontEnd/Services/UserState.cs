public class UserState
{
    public string UserRole { get; set; } = string.Empty;
    public event Action OnChange;

    public void SetUserRole(string role)
    {
        UserRole = role;
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}
