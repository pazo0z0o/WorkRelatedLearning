using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Dapper;
using System.Data;


namespace DataAccess.DbAccess;
public class SqlDataAccess : ISqlDataAccess
{
    private readonly IConfiguration _config;
    public SqlDataAccess(IConfiguration config)
    {
        _config = config;
    }

    //Just 2 classes with a connection and a query execution(async) are enough for
    //data access. They will be invoked multiple times depending on the occassion

    //<summary>Handles the GetAll or Get sql queries</summary>
    public async Task<IEnumerable<T>> LoadData<T, U>(
        string storedProcedure,
        U parameters,
        string connectionId = "Default")
    {//used to create the connection
        using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));

        //used to execute the storedProcedure we give to it
        return await connection.QueryAsync<T>(storedProcedure
                         , parameters, commandType: CommandType.StoredProcedure);
    }

    //<summary>Handles the Insert,Delete,Update sql queries</summary>
    public async Task SaveData<T>(string storedProcedure,
                                  T parameters,
                                  string connectionId = "Default")
    {
        using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));

        await connection.ExecuteAsync(storedProcedure, parameters
                         , commandType: CommandType.StoredProcedure);

    }
}
