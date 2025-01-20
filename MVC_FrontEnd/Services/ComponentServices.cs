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
    }
}
