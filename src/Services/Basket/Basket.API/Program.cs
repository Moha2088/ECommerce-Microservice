

using BuildingBlocks.Exceptions.Handler;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container

var assembly = typeof(Program).Assembly;

builder.Services.AddMarten(opt =>
{
    opt.Connection(builder.Configuration.GetConnectionString("DBConnection") ?? throw new InvalidOperationException("ConnectionString not found!"));
    opt.Schema.For<ShoppingCart>().Identity(x => x.UserName);
}).UseLightweightSessions();

builder.Services.AddScoped<IBasketRepository, BasketRepository>();

builder.Services.AddCarter();
builder.Services.AddMediatR(opt =>
{
    opt.RegisterServicesFromAssembly(assembly);
    opt.AddOpenBehavior(typeof(ValidationBehavior<,>));
    opt.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

// Configure the HTTPS request pipeline
app.MapCarter();
app.UseExceptionHandler(_ => { });

app.Run();
