using Ordering.API;
using Ordering.Applicaton;
using Ordering.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container

builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices();

var app = builder.Build();

app.UseApiServices();

// Configure the HTTP request pipeline

app.Run();
