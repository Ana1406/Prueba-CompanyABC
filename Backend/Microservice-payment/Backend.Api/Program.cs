using Backend.Core.Core;
using Backend.Core.Core.Interfaces;
using Backend.DataBase.DataBase;
using Backend.DataBase.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();
// --- Configuration MongoDB
builder.Services.Configure<MongoSettings>(
    builder.Configuration.GetSection("MongoSettings"));
builder.Services.AddSingleton<DbContext>();
builder.Services.Configure<OrdersServiceSettings>(
    builder.Configuration.GetSection("OrderService"));
builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("Jwt"));

builder.Services.AddHttpClient<IOrderServices, OrdersServices>()
    .ConfigureHttpClient((serviceProvider, client) =>
    {
        var settings = serviceProvider
            .GetRequiredService<IOptions<OrdersServiceSettings>>()
            .Value;

        client.BaseAddress = new Uri(settings.BaseUrl);
    });
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var key = Convert.FromBase64String(builder.Configuration["Jwt:Key"]!);

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = "AuthService",
            ValidAudience = "Microservices",
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };

        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine("TOKEN ERROR: " + context.Exception.Message);
                return Task.CompletedTask;
            }
        };
    });
builder.Services.AddHttpContextAccessor();
// --- Add Scopes
builder.Services.AddTransient<IPaymentRepositorie, PaymentRepositorie>();
builder.Services.AddTransient<IPaymentCore, PaymentCore>();
builder.Services.AddTransient<IHealthCore, HealthCore>();
builder.Services.AddEndpointsApiExplorer();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Microservices Payments",
        Version = "v1",
        Description = "Microservice in charge of logic about payments in ABC Company"
    });
});
var app = builder.Build();



 app.UseSwagger();
 app.UseSwaggerUI();


app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();  
app.UseAuthorization(); ;
app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();   
app.Run();
