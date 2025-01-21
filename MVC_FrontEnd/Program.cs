using Blazored.LocalStorage;
using Blazored.SessionStorage;
using Blazored.Toast;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MVC_FrontEnd;
using MVC_FrontEnd.Models;
using MVC_FrontEnd.Services;
using MVC_FrontEnd.URL;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddBlazoredSessionStorage();
builder.Services.AddScoped<UserState>();
builder.Services.AddScoped<URLStringServices>();
builder.Services.AddScoped<URLs>();
builder.Services.AddScoped<ComponentServices>();
builder.Services.AddBlazoredToast();

//User Registration
builder.Services.AddScoped<AuthenticationService>();
builder.Services.AddScoped<UsersServices>();




await builder.Build().RunAsync();
