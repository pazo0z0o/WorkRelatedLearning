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

    ///<summary>Handles the GetAll or Get sql queries</summary>
    /// <param name="storedProcedure">The stored procedure of our choice </param>
    /// <param name="parameters">Parameters of input in our stored procedures</param>
    /// <param name="connectionId"> The connection string for the database</param>
    /// <returns>A <see cref="Task{IEnumerable{T}}" /> That is our return list for the GetById or GetAll crud functions</returns>
    public async Task<IEnumerable<T>> LoadData<T, U>(
        string storedProcedure,
        U parameters,
        string connectionId = "Default")
    {//used to create the connection -- Uses IDbConnection instead of config. and  
        using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));

        //used to execute the storedProcedure we give to it
        return await connection.QueryAsync<T>(storedProcedure
                    ,parameters, commandType: CommandType.StoredProcedure);
    }

    ///<summary>Handles the Insert,Update,Delete sql queries</summary>
    /// <param name="storedProcedure">The stored procedure of our choice </param>
    /// <param name="parameters">Parameters of input in our functions</param>
    /// <param name="connectionId"> The connection string for the database</param>
    /// <returns>A <see cref="Task"/> That is our return type for the Insert,Update,Delete crud operations</returns>
    public async Task SaveData<T>(string storedProcedure,
                                  T parameters,
                                  string connectionId = "Default")
    {
        using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));

        await connection.ExecuteAsync(storedProcedure, parameters
                         , commandType: CommandType.StoredProcedure);

    }
}
 