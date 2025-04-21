using Microsoft.JSInterop;

namespace MVC_FrontEnd.Data
{
    public static class FileUtil
    {
        public static ValueTask<object> SaveAs(this IJSRuntime js, string filename, byte[] data)
            => js.InvokeAsync<object>("saveAsFile", filename, Convert.ToBase64String(data));
    }
}
