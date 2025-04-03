using Microsoft.AspNetCore.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.Services.AddRateLimiter(opt =>
{
    opt.AddFixedWindowLimiter("fixed", x =>
    {
        x.Window = TimeSpan.FromSeconds(10);
        x.PermitLimit = 5;
        x.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
    });
});

var app = builder.Build();
// Configure the HTTP Request pipeline

app.MapReverseProxy();

app.Run();