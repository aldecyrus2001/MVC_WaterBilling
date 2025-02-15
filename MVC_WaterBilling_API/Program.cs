using Microsoft.EntityFrameworkCore;
using MVC_WaterBilling_API.Data;
using MVC_WaterBilling_API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlServer("name=DefaultConnection");
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorApp", builder =>
    {
        builder.WithOrigins("http://localhost:5264")
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddScoped<UserData>();
builder.Services.AddScoped<AuthenticationData>();
builder.Services.AddScoped<BillData>();
builder.Services.AddScoped<ConsumerData>();
builder.Services.AddScoped<MeterReadingData>();
builder.Services.AddScoped<AdvanceData>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseCors("AllowBlazorApp");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
