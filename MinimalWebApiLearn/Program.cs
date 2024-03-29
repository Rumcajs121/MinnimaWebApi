using System.Data.SqlClient;
using Dapper;
using Microsoft.AspNetCore.Http.HttpResults;
using MinimalWebApiLearn.Endpoints;
using MinimalWebApiLearn.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ISqlDataAcces, SqlDataAcces>();
builder.Services.AddSingleton<IToDoListData, ToDoListData>();
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
