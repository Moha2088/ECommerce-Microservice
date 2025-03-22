

var builder = WebApplication.CreateBuilder(args);

// Add Services to the container

builder.Services.AddMarten(opt =>
{
    opt.Connection(builder.Configuration.GetConnectionString("DBConnection") ?? throw new InvalidOperationException("Connection string not found!"));
}).UseLightweightSessions();

builder.Services.AddCarter();
builder.Services.AddMediatR(opt =>
{
    opt.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

var app = builder.Build();

// Configure the HTTP request pipeline

app.UseHttpsRedirection();

app.MapCarter();

app.Run();