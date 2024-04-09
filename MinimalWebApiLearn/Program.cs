using System.Data.SqlClient;
using Dapper;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.OpenApi.Models;
using MinimalWebApiLearn;
using MinimalWebApiLearn.Endpoints;
using MinimalWebApiLearn.Models;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setup => setup.SwaggerDoc("v1", new OpenApiInfo()
{
    Description = "This is a simple implementation of a Minimal Api in Asp.Net 7 Core and implementation CRUD pattern. I used dapper in my database.",
    Title = "ToDoList MinimalWebApi",
    Version = "v1",
    Contact = new OpenApiContact()
    {
        Name = "Rumcaj121",
        Url = new Uri("https://github.com/Rumcajs121/MinnimaWebApi")
    }
}));
builder.Services.AddSingleton<ISqlDataAcces, SqlDataAcces>();
builder.Services.AddSingleton<IToDoListData, ToDoListData>();
builder.Services.AddValidatorsFromAssemblyContaining(typeof(ToDoValidator));
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
