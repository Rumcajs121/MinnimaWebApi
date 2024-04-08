using System.Data.SqlClient;
using System.Net.NetworkInformation;
using Dapper;
using MinimalWebApiLearn.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
                .Produces(StatusCodes.Status404NotFound);

            app.MapGet("/task/{id}", GetOneTaskId)
                .WithName("Task")
                .WithOpenApi();
            app.MapPost("/createNewTask", CreateNewTask)
                .WithName("CreateNewTask")
                .WithOpenApi(generatedOperation =>
                {
                    generatedOperation.Summary = "Download ones task for Id";
                            generatedOperation.Description = "Retrieves a one task in the ToDoList for Id.";

                            return generatedOperation;
                })
                .Produces<AssignmentDto>(StatusCodes.Status201Created)
                .Accepts<AssignmentDto>("application/json");
            app.MapPut("/editTask/{id}", EditTask)
                .WithName("EditTask")
                .WithOpenApi();
            app.MapDelete("/deleteTask/{id}", DeleteTask)
                .WithName("DeleteTask")
                .WithOpenApi();
        }

        private static async Task<IResult> GetTasks(IToDoListData data)
        {
            try
            {
                return Results.Ok(await data.GetAllToDoListS());
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
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
                return Results.Problem(ex.Message);
            }
        }

        private static async Task<IResult> CreateNewTask(AssignmentDto assignment, IToDoListData data)
        {
            try
            {
                await data.InsertToDoList(assignment);
                return Results.Created(
                    $"Task: {assignment.Description} is created. Time the end Task is: {(assignment.EndDate-DateTime.Now).Days}",assignment);
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
                return Results.Ok($"Task fo ID: {id} update");
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
                private static async Task<IResult> DeleteTask(int id, IToDoListData data)
        {
            try
            {
                await data.DeleteTask(id);

                return Results.Ok($"Task fo ID: {id} id deleted");
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
    }
}
