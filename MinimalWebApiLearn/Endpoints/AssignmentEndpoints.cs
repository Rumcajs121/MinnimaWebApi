using System.Data.SqlClient;
using System.Net.NetworkInformation;
using Dapper;
using MinimalWebApiLearn.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
//using MinimalWebApiLearn.Services;

namespace MinimalWebApiLearn.Endpoints
{
    public static class AssignmentEndpoints
    {
        public static void MapAssignmentEndpoints(this WebApplication app)
        {

            app.MapGet("/tasks", GetTasks)
                .WithName("Tasks")
                .WithOpenApi();

            app.MapGet("/task/{id}", GetOneTaskId)
                .WithName("Task")
                .WithOpenApi();
            app.MapPut("/createNewTask", CreateNewTask)
                .WithName("CreateNewTask")
                .WithOpenApi();

            //.WithOpenApi();
            //builder.MapGet("/tasks", async (SqlConnectionFactory sqlConnectionFactory) =>
            //    {

            //        using var connection = sqlConnectionFactory.Create();

            //        const string sqlQuery = "SELECT * FROM ToDoListDb";

            //        var tasks = await connection.QueryAsync<Assignment>(sqlQuery);
            //        return Results.Ok(tasks);
            //    })
            //    .WithName("Tasks")
            //    .WithOpenApi();
            //builder.MapGet("/task/{id}",async(int id, SqlConnectionFactory sqlConnectionFactory)=>
            //{

            //    using var connection = sqlConnectionFactory.Create();

            //    const string sqlQuery = """
            //                            SELECT * 
            //                            FROM ToDoListDb
            //                            WHERE Id = @TaskId
            //                            """;

            //    var task = await connection.QuerySingleOrDefaultAsync<Assignment>(sqlQuery,new {TaskId =id});
            //    return task is not null ? Results.Ok(task): Results.NotFound("Not  task for this Id");
            //}).WithName("Task")
            //.WithOpenApi();

            //builder.MapDelete("/Delete{id}",async (int id, SqlConnectionFactory sqlConnectionFactory) =>
            //{

            //    using var connection = sqlConnectionFactory.Create();

            //    const string sqlQuery = """
            //                            DELETE FROM ToDoListDb
            //                            WHERE Id = @TaskId
            //                            """;
            //    var task = await connection.ExecuteAsync(sqlQuery, new { TaskId = id });

            //    return task == 0 ? Results.NotFound($"Task with ID {id} not found.") : Results.Ok($"Task with id {id} delete");

            //})
            //.WithName("Delete")
            //.WithOpenApi();

            //builder.MapPut("/CreateNewTask",async (string description, DateTime endTime, SqlConnectionFactory sqlConnectionFactory) =>
            //{

            //    using var connection = sqlConnectionFactory.Create();

            //    const string sqlQuery = """
            //                            INSERT INTO [dbo].[ToDoListDb]
            //                            ([Description],[EndDate])
            //                            VALUES
            //                            (@Description,@EndTime)
            //                            """;
            //    var task = await connection.ExecuteAsync(sqlQuery, new { @Description = description, @EndTime=(DateTime)endTime });

            //    return task == 0 ? Results.NotFound($"Something is wrong.") : Results.Ok($"Task {description} created");

            //})
            //.WithName("CreateNewTask")
            //.WithOpenApi();

            //builder.MapPut("/EditTask{id}", async (int id, string description, DateTime endTime, SqlConnectionFactory sqlConnectionFactory) =>
            //    {

            //        using var connection = sqlConnectionFactory.Create();

            //        const string sqlQuery = """
            //                                UPDATE [dbo].[ToDoListDb]
            //                                  SET [Description] = @Description, nvarchar(max)
            //                                     ,[IsCompleted] = @EndTime, datetime2(7)
            //                                WHERE Id = @TaskId
            //                                """;
            //        var task = await connection.ExecuteAsync(sqlQuery, new {@TaskId=id, @Description = description, @EndTime = (DateTime)endTime });

            //        return task == 0 ? Results.NotFound($"Something is wrong.") : Results.Ok($"Task {id}is update");

            //    })
            //    .WithName("Update")
            //    .WithOpenApi();
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

        private static async Task<IResult> CreateNewTask(string description, DateTime endDate, IToDoListData data)
        {
            try
            {
                await data.InsertToDoList(description,endDate);
                return Results.Ok(
                    $"Task: {description} is created. Time the end Task is: {(DateTime.Now - endDate).Days}");
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
    }
}
