using System.Data.SqlClient;
using Dapper;
using Microsoft.AspNetCore.Http.HttpResults;
using MinimalWebApiLearn.Endpoints;
using MinimalWebApiLearn.Models;
using MinimalWebApiLearn.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton(serviceProvider =>
{
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();

    var connectionString=configuration.GetConnectionString("MinimalWebApi") ?? 
                         throw new ApplicationException("The conection string is null");
    return new SqlConnectionFactory(connectionString);
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapAssignmentEndpoints();

app.Run();
