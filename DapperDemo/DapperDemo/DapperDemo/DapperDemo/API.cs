using Microsoft.AspNetCore.Http;

namespace DapperDemo;

public static class API
{
    //This is instead of a controller class
    public static void ConfigureApi(this WebApplication app)
    {
        //All of my APIs endpoints mapping -- remains public since the configuration needs to be used for the routing
        app.MapGet(pattern: "/Users", GetUsers);
        app.MapGet(pattern: "/Users{id}", GetUserById);
        app.MapPost(pattern: "/Users", InsertUser);
        app.MapPut(pattern: "/Users", UpdateUser);
        app.MapDelete(pattern: "/Users", DeleteUser);
    }

    private static async Task<IResult> GetUsers(IUserData data)
    {
        try
        {
            return Results.Ok(await data.GetAllUsers()); //proper return codes
        }
        catch (Exception ex)
        {

            return Results.Problem(ex.Message);
        }

    }
    //Good Practices 
    private static async Task<IResult> GetUserById(int id, IUserData data)
    {
        try
        {
            var results = await data.GetById(id);
            //measure if we don't get a result by a user's Id 
            if (results == null) { return Results.NotFound(); }

            return Results.Ok(results);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
    private static async Task<IResult> InsertUser(UserModel user, IUserData data)
    {
        try
        {
            await data.InsertUser(user);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }    
    }
    
    private static async Task<IResult> UpdateUser(UserModel user, IUserData data)
    {
        try
        {
            await data.UpdateUser(user);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> DeleteUser(int id, IUserData data)
    {
        try
        {
            await data.DeleteUser(id);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
}
