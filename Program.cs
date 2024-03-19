using Microsoft.EntityFrameworkCore;
using user_service_app.Models;
using System;

var builder = WebApplication.CreateBuilder(args);


// Extract connection string and replace placeholders with environment variables
var connectionString = builder.Configuration.GetConnectionString("DbCon")
    .Replace("{DB_HOST}", Environment.GetEnvironmentVariable("DB_HOST") ?? "localhost")
    .Replace("{DB_NAME}", Environment.GetEnvironmentVariable("DB_NAME") ?? "default_db")
    .Replace("{DB_USER}", Environment.GetEnvironmentVariable("DB_USER") ?? "user")
    .Replace("{DB_PASSWORD}", Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "password");


// Add services to the container.
//builder.Services.AddDbContext<UserContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("DbCon")));
builder.Services.AddDbContext<UserContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
