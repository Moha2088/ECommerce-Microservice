




using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

var assembly = typeof(Program).Assembly;

// Add Services to the container


builder.Services.AddMediatR(opt =>
{
    opt.RegisterServicesFromAssembly(assembly)
    .AddOpenBehavior(typeof(ValidationBehavior<,>))
    .AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(assembly);


builder.Services.AddCarter();

builder.Services.AddMarten(opt =>
{
    opt.Connection(builder.Configuration.GetConnectionString("DBConnection") ?? throw new InvalidOperationException("Connection string not found!"));
}).UseLightweightSessions();

if (builder.Environment.IsDevelopment())
{
    builder.Services.InitializeMartenWith<CatalogInitialData>();
}

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("DBConnection")!);

var app = builder.Build();


// Configure the HTTP request pipeline


app.MapCarter();

app.UseExceptionHandler(_ => { });

app.UseHealthChecks("/health" , new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();