namespace MVC_FrontEnd.Services
{
    public class URLs
    {
        private string _BaseURL = "http://localhost:5142/api/";
        public string FetchUsers => _BaseURL + "User";
    }
}
