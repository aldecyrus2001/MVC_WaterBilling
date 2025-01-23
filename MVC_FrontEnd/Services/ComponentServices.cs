namespace MVC_FrontEnd.Services
{
    public class ComponentServices
    {
        public void UpdateInputField<T>(T userData, string fieldName, string value)
        {
            if (userData != null)
            {
                // Using reflection to get the property of userData dynamically
                var property = userData.GetType().GetProperty(fieldName);
                if (property != null)
                {
                    // Set the property value
                    property.SetValue(userData, value);
                }
            }
        }

        public List<SelectOptions> GenderOptions { get; set; } = new()
        {
            new SelectOptions {Value = "Male", Text = "Male"},
            new SelectOptions {Value = "Female", Text = "Female"}
        };

        public List<SelectOptions> PositionOptions { get; set; } = new()
        {
            new SelectOptions {Value = "Administrator", Text = "Administrator"},
            new SelectOptions {Value = "Consumer", Text = "Consumer"},
            new SelectOptions {Value = "Reader", Text = "Reader"},
            new SelectOptions {Value = "Cashier", Text = "Cashier"}
        };

        public List<SelectOptions> ConnectionType { get; set; } = new()
        {
            new SelectOptions {Value = "Residential", Text= "Residential"},
            new SelectOptions {Value = "Commercial", Text= "Commercial"}
        };
    }
}
