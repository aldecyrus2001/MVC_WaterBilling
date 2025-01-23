namespace MVC_FrontEnd.URL
{
    public class URLs
    {
        private string _BaseURL = "http://localhost:5142/api/";
        public string AuthenticationURL => _BaseURL + "Authentication";
        public string Users => _BaseURL + "User";
        public string Consumer => _BaseURL + "Consumer";
        public string UsersByRole(string role) => $"{_BaseURL}User/role/{role}";
    }
}
