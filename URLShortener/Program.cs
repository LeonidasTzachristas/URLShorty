using Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using RepositoryContracts;
using ServiceContracts;
using Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<IUrlShortenerRepository, UrlShortenerRepository>();
builder.Services.AddScoped<IUrlShortenerService, UrlShortenerService>();
builder.Services.AddScoped<IHashingService, HashingService>();

builder.Services.AddDbContext<UrlShortenerDbContext>(options =>
{
    var serverVersion = new MySqlServerVersion(new Version(9, 7, 0));
    options.UseMySql(builder.Configuration.GetConnectionString("MySql"), serverVersion)
        .LogTo(Console.WriteLine, LogLevel.Information)     // Should be removed for production
        .EnableDetailedErrors();
});

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.Run();