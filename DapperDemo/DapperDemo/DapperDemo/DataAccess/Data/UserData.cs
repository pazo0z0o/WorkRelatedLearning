using DataAccess.DbAccess;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data;
public class UserData : IUserData
//Where we get our user data api calls  
{
    private readonly ISqlDataAccess _db;

    public UserData(ISqlDataAccess db)
    {
        _db = db;
    }

    public Task<IEnumerable<UserModel>> GetAllUsers() =>
        _db.LoadData<UserModel, dynamic>
        (storedProcedure: "dbo.spUser", new {  });

    public async Task<UserModel?> GetById(int id)
    {   //
        var results = await _db.LoadData<UserModel, dynamic>
            (storedProcedure: "dbo.spUser_Get", new { Id = id });

        return results.FirstOrDefault();
    }
    //Insert-Create : It mapped the usermodel already here,
    //hence why user.FirstName is an option
    public Task InsertUser(UserModel user) =>
        _db.SaveData(storedProcedure: "spUser_Insert"
            , new { user.FirstName, user.LastName });

    //Update 
    public Task UpdateUser(UserModel user) =>
        _db.SaveData(storedProcedure: "spUser_Update", user);

    //Delete
    public Task DeleteUser(int id) =>
        _db.SaveData(storedProcedure: "spUser_Delete", new { Id = id });



}
