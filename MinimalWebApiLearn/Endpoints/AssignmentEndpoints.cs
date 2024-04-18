using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Net.NetworkInformation;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Text;
using Dapper;
using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using MinimalWebApiLearn.Models;


namespace MinimalWebApiLearn.Endpoints
{
    public static class AssignmentEndpoints
    {
        public static void MapAssignmentEndpoints(this WebApplication app)
        {
            
            app.MapGet("/tasks", GetTasks)
                .WithName("Tasks")
                .WithOpenApi(generatedOperation =>
                {
                    generatedOperation.Summary = "Download all tasks";
                            generatedOperation.Description = "Retrieves a list of all tasks in the ToDoList.";

                            return generatedOperation;
                }).Produces<Assignment>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .WithTags("Read");

            app.MapGet("/task/{id}", GetOneTaskId)
                .WithName("Task")
                .WithOpenApi(generatedOperation =>
                {
                    generatedOperation.Summary = "Download one task for Id";
                            generatedOperation.Description = "Retrieves a one Task in the ToDoList.";

                            return generatedOperation;
                })
                .Produces<Assignment>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .WithTags("Read")
                .RequireAuthorization();

            app.MapPost("/createNewTask", CreateNewTask)
                .WithName("CreateNewTask")
                .WithOpenApi(generatedOperation =>
                {
                    generatedOperation.Summary = "Created task for Id";
                            generatedOperation.Description = "Create new task in the ToDoList.";

                            return generatedOperation;
                })
                .Produces<AssignmentDto>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status400BadRequest)
                .Accepts<AssignmentDto>("application/json").WithTags("Created")
                .WithValidator<AssignmentDto>().RequireAuthorization();;

            app.MapPut("/editTask/{id}", EditTask)
                .WithName("EditTask")
                .WithOpenApi()
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status404NotFound)
                .Accepts<AssignmentDto>("application/json").WithOpenApi(generatedOperation =>
                {
                    generatedOperation.Summary = "Edit task for Id";
                            generatedOperation.Description = "Edit task in the ToDoList for Id.";

                            return generatedOperation;
                }).WithTags("Update")
                .WithValidator<AssignmentDto>()
                .RequireAuthorization();

            app.MapDelete("/deleteTask/{id}", DeleteTask)
                .WithName("DeleteTask")
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status404NotFound)
                .WithOpenApi(generatedOperation =>
                {
                    generatedOperation.Summary = "Delete task for Id";
                            generatedOperation.Description = "Delete task in the ToDoList for Id.";

                            return generatedOperation;
                }).WithTags("Delete")
                .RequireAuthorization();
            app.MapGet("/CreateTokenJwt",CreateToken)
            .WithName("Create test token for Authorization")
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status400BadRequest)
                .WithOpenApi(generatedOperation =>
                {
                    generatedOperation.Summary = "Create test token Jwt";
                            generatedOperation.Description = "Create example token Jwt for testing  Authorization";

                            return generatedOperation;
                }).WithTags("Token Jwt");
        }

        private static async Task<IResult> GetTasks(IToDoListData data)
        {
            try
            {
                return Results.Ok(await data.GetAllToDoListS());
            }
            catch (Exception ex)
            {
                return Results.NotFound(Results.Problem(ex.Message));
            }
        }

        private static async Task<IResult> GetOneTaskId(int id,IToDoListData data)
        {
            try
            {
                var result= await data.GetToDoList(id);
                if (result == null)
                {
                    return Results.NotFound($"Task for ID: {id} is not found");
                }

                return Results.Ok(result);
            }
            catch (Exception ex)
            {
                return Results.NotFound(Results.Problem(ex.Message));
            }
        }

        private static async Task<IResult> CreateNewTask(AssignmentDto assignment, IToDoListData data)
        {
            try
            {   
                await data.InsertToDoList(assignment);
                var timeTheEnd=(DateTime.Parse(assignment.EndDate) - DateTime.Now).Days;
                return Results.Ok(
                    $"Task: {assignment.Description} is created. Time the end Task:{timeTheEnd}");
            }
            catch (Exception ex)
            {
                return Results.BadRequest(Results.Problem(ex.Message));
            }
        }
                private static async Task<IResult> EditTask(int id, AssignmentDto assignment, IToDoListData data)
        {
            try
            {
                await data.UpdateTask(id, assignment);
                return Results.NoContent();
            }
            catch (Exception ex)
            {
                return Results.NotFound(Results.Problem(ex.Message));
            }
        }
                private static async Task<IResult> DeleteTask(int id, IToDoListData data)
        {
            try
            {
                await data.DeleteTask(id);

                return Results.NoContent();
            }
            catch (Exception ex)
            {
                return Results.NotFound(Results.Problem(ex.Message));
            }
        }

        // This method used to verification work JwtBearer
            public static string CreateToken(IConfiguration configuration){
                var claims=new[]{
                    new Claim(ClaimTypes.NameIdentifier, "user-id"),
                    new Claim(ClaimTypes.Name,"Test Name"),
                    new Claim(ClaimTypes.Role, "Admin")
                };
            var token = new JwtSecurityToken(
                issuer: configuration["JwtIssuer"],
                audience: configuration["JwtIssuer"],
                claims: claims,
                expires:DateTime.UtcNow.AddDays(3),
                signingCredentials:new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtKey"])),
                SecurityAlgorithms.HmacSha256)
                
                );
            var jwtToken=new JwtSecurityTokenHandler().WriteToken(token);
            return jwtToken;

            
            }
    }
}
