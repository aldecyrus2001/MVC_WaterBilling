namespace MVC_FrontEnd.URL
{
    public class URLs
    {
        private string _BaseURL = "http://localhost:5142/api/";
        public string AuthenticationURL => _BaseURL + "Authentication";
        public string Users => _BaseURL + "User";
        public string Consumer => _BaseURL + "Consumer";
        public string Reading => _BaseURL + "Meter";
        public string Advance => _BaseURL + "Advance";
        public string Billing => _BaseURL + "Bill";
        public string Payment => _BaseURL + "Payment";
        public string Settings => _BaseURL + "Settings";
        public string UsersByRole(string role) => $"{_BaseURL}User/role/{role}";
    }
}
