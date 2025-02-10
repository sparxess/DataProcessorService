using DataProcessorService.HttpApi.Host.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureLogging(builder.Configuration);
builder.Services.ConfigureDatabase(builder.Configuration);
builder.Services.ConfigureOptions(builder.Configuration);
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJwtAuthentication(builder.Configuration);
builder.Services.RegisterServices();
builder.Services.ConfigureAutoMapper();
builder.Services.ConfigureSwagger();
builder.Services.ConfigureControllers();

var app = builder.Build();

var assemblyName = typeof(Program).Assembly.GetName().Name;
Log.Information($"Starting {assemblyName}.");

app.ConfigureMiddlewares();
app.ConfigureApp();

app.Run();

Log.Information($"Shutting down {assemblyName}.");
Log.CloseAndFlush();