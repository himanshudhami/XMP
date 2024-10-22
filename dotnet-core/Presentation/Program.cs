using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;
using XMP.Application.Interfaces;
using XMP.Application.Services;
using XMP.Domain.Repositories;
using XMP.Infrastructure.Repositories;
using XMP.Infrastructure.DbContext;


var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables()
    .Build();
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<DapperDbContext>();

// Add services to the container.

// Add Swagger/OpenAPI support
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add AutoMapper (if you're using it)
builder.Services.AddAutoMapper(typeof(XMP.Application.Mappers.UserProfile)); // Correct namespace for your AutoMapper profile

// Add repository and service dependencies
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddAutoMapper(typeof(XMP.Application.Mappers.CompanyProfile)); // Correct namespace for your AutoMapper profile

// Add repository and service dependencies
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<ICompanyService, CompanyService>();

builder.Services.AddAutoMapper(typeof(XMP.Application.Mappers.UserProfile)); // Correct namespace for your AutoMapper profile

// Add repository and service dependencies
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IProjectService, ProjectService>();
// Add Controllers
builder.Services.AddControllers();

// Add Status repository and service dependencies
builder.Services.AddScoped<IStatusRepository, StatusRepository>();
builder.Services.AddScoped<IStatusService, StatusService>();

// Add AutoMapper for Status
builder.Services.AddAutoMapper(typeof(XMP.Application.Mappers.StatusProfile)); // Correct namespace for your AutoMapper profile

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
