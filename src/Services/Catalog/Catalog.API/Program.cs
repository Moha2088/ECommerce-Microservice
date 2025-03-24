


var builder = WebApplication.CreateBuilder(args);

var assembly = typeof(Program).Assembly;

// Add Services to the container


builder.Services.AddMediatR(opt =>
{
    opt.RegisterServicesFromAssembly(assembly)
    .AddOpenBehavior(typeof(ValidationBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(assembly);


builder.Services.AddCarter();

builder.Services.AddMarten(opt =>
{
    opt.Connection(builder.Configuration.GetConnectionString("DBConnection") ?? throw new InvalidOperationException("Connection string not found!"));
}).UseLightweightSessions();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();


var app = builder.Build();


// Configure the HTTP request pipeline

app.UseHttpsRedirection();

app.MapCarter();

app.UseExceptionHandler(_ => { });

app.Run();