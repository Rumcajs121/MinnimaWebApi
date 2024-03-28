using Dapper;
using System.Data;
using System.Data.SqlClient;
namespace MinimalWebApiLearn.Endpoints
{
    public class SqlDataAcces : ISqlDataAcces
    {
        private readonly IConfiguration _config;

        public SqlDataAcces(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IEnumerable<T>> LoadData<T, U>(string storedProcedure, U parameters, string connetionID = "MinimalWebApi")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connetionID));

            return await connection.QueryAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
        }
        public async Task SaveData<T>(string storedProcedure, T parameters, string connetionID = "MinimalWebApi")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connetionID));
            await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
        }


    }
}
