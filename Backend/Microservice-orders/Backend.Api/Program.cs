using Backend.Core.Core;
using Backend.Core.Core.Interfaces;
using Backend.DataBase.DataBase;
using Backend.DataBase.Repositories;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();
// --- Configuration MongoDB
builder.Services.Configure<MongoSettings>(
    builder.Configuration.GetSection("MongoSettings"));
builder.Services.AddSingleton<DbContext>();
builder.Services.Configure<PaymentServiceSettings>(
    builder.Configuration.GetSection("PaymentService"));
builder.Services.AddHttpClient<IPaymentServices, PaymentServices>()
    .ConfigureHttpClient((serviceProvider, client) =>
    {
        var settings = serviceProvider
            .GetRequiredService<IOptions<PaymentServiceSettings>>()
            .Value;

        client.BaseAddress = new Uri(settings.BaseUrl);
    });
// --- Add Scopes
builder.Services.AddTransient<IOrderRepositorie, OrderRepositorie>();
builder.Services.AddTransient<IOrderCore, OrderCore>();
builder.Services.AddTransient<IHealthCore, HealthCore>();
builder.Services.AddEndpointsApiExplorer();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Microservices Orders",
        Version = "v1",
        Description = "Microservice in charge of logic about orders in ABC Company"
    });
});
var app = builder.Build();

    app.UseSwagger();
    app.UseSwaggerUI();


app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();   
app.Run();
