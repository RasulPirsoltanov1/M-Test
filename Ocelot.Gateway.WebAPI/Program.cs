using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials()
    .WithOrigins("http://localhost:4200"));
});



builder.Configuration.AddJsonFile("ocelot.json");
builder.Services.AddOcelot();



var app = builder.Build();

app.UseCors("CorsPolicy");


app.MapGet("/", () => "Hello World!");


await app.UseOcelot();
app.Run();
