using System.Data.SqlClient;
using Dapper;
using MinimalWebApiLearn.Models;
using MinimalWebApiLearn.Services;

namespace MinimalWebApiLearn.Endpoints
{
    public static class AssignmentEndpoints
    {
        public static void MapAssignmentEndpoints(this IEndpointRouteBuilder builder)
        {
            builder.MapGet("/tasks", async (SqlConnectionFactory sqlConnectionFactory) =>
                {

                    using var connection = sqlConnectionFactory.Create();

                    const string sqlQuery = "SELECT * FROM ToDoListDb";

                    var tasks = await connection.QueryAsync<Assignment>(sqlQuery);
                    return Results.Ok(tasks);
                })
                .WithName("Tasks")
                .WithOpenApi();
            builder.MapGet("/task/{id}",async(int id, SqlConnectionFactory sqlConnectionFactory)=>
            {

                using var connection = sqlConnectionFactory.Create();

                const string sqlQuery = """
                                        SELECT * 
                                        FROM ToDoListDb
                                        WHERE Id = @TaskId
                                        """;

                var task = await connection.QuerySingleOrDefaultAsync<Assignment>(sqlQuery,new {TaskId =id});
                return task is not null ? Results.Ok(task): Results.NotFound("Not  task for this Id");
            }).WithName("Task")
            .WithOpenApi();

            builder.MapDelete("/Delete{id}",async (int id, SqlConnectionFactory sqlConnectionFactory) =>
            {

                using var connection = sqlConnectionFactory.Create();

                const string sqlQuery = """
                                        DELETE FROM ToDoListDb
                                        WHERE Id = @TaskId
                                        """;
                var task = await connection.ExecuteAsync(sqlQuery, new { TaskId = id });
                //return task == 0 ? Results.Ok(task) : Results.NotFound("Not  task for this Id");
                return task == 0 ? Results.NotFound($"Task with ID {id} not found.") : Results.Ok($"Task with id {id} delete");

            })
            .WithName("Delete")
            .WithOpenApi();

            builder.MapPut("/CreateNewTask",async (string description, DateTime endTime, SqlConnectionFactory sqlConnectionFactory) =>
            {

                using var connection = sqlConnectionFactory.Create();

                const string sqlQuery = """
                                        INSERT INTO [dbo].[ToDoListDb]
                                        ([Description],[EndDate])
                                        VALUES
                                        (@Description,@EndTime)
                                        """;
                var task = await connection.ExecuteAsync(sqlQuery, new { @Description = description, @EndTime=(DateTime)endTime });
                
                return task == 0 ? Results.NotFound($"Something is wrong.") : Results.Ok($"Task {description} created");

            })
            .WithName("CreateNewTask")
            .WithOpenApi();
        }
    }
}
