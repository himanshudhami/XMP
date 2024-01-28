using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;
using XMP.Application.Interfaces;
using XMP.Application.Services;
using XMP.Domain.Repositories;
using XMP.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IDbConnection>((sp) =>
{
    var connectionString = "Server=127.0.0.1;Port=5432;Database=financedb;User Id=finance;Password=111;"; // Replace with your connection string
    return new NpgsqlConnection(connectionString);
});

// Add services to the container.

// Add Swagger/OpenAPI support
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add AutoMapper (if you're using it)
builder.Services.AddAutoMapper(typeof(XMP.Application.Mappers.AxisBankTransactionProfile)); // Correct namespace for your AutoMapper profile

// Add repository and service dependencies
builder.Services.AddScoped<IAxisBankTransactionRepository, AxisBankTransactionRepository>();
builder.Services.AddScoped<IAxisBankTransactionService, AxisBankTransactionService>();

// Add Controllers
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

// Use Swagger if in the development environment
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Other middleware
app.UseHttpsRedirection();
app.UseAuthorization();

// Map controllers
app.MapControllers();

app.Run();
