using Bonheur.HttpRecorder.Middlewares;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("ocelot.json", false, true);

// Add services to the container.
builder.Services.AddOcelot();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<RecorderMiddleware>();
app.UseOcelot().Wait();

app.Run();