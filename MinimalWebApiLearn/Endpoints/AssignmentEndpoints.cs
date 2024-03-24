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
        }
    }
}
