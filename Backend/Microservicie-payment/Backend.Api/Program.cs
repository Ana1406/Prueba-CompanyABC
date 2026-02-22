using Backend.Core.Core;
using Backend.Core.Core.Interfaces;
using Backend.DataBase.DataBase;
using Backend.DataBase.Repositories;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();
// --- Configuration MongoDB
builder.Services.Configure<MongoSettings>(
    builder.Configuration.GetSection("MongoSettings"));
builder.Services.AddSingleton<DbContext>();
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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();   
app.Run();
