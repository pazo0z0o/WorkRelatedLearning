﻿using DataAccess.DbAccess;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data;
public class UserData : IUserData
//Where we have all the needed procedures for our API calls later 
{
    private readonly ISqlDataAccess _db;

    public UserData(ISqlDataAccess db)
    {
        _db = db;
    }

    ///<summary>Handles the GetAll sql query</summary>
    /// <returns>A <see cref="Task{IEnumerable{UserModel}}"/>  That is our return list for the GetAll crud function</returns>
    public async Task<IEnumerable<UserModel>> GetAllUsers() =>
      await _db.LoadData<UserModel, dynamic>
        (storedProcedure: "dbo.spUser_GetAll", new {  });

    ///<summary>Handles the GetById sql query</summary>
    /// <param name="id">The id that corresponds to the table element defined in the stored procedure </param>
    /// <returns>A <see cref="UserModel" /> That is our return UserModel for the GetAId crud function</returns>
    public async Task<UserModel?> GetById(int id)
    {   
        var results = await _db.LoadData<UserModel, dynamic>
            (storedProcedure: "dbo.spUser_GetById", new { Id = id });

        return results.FirstOrDefault();
    }
    //Insert-Create : It mapped the usermodel already here,
    //hence why user.FirstName, user.LastName are an option

    ///<summary>Handles the GetAll sql query</summary>
    /// <param name="user">The user pamater fed to the stored procedure of our choice </param>
    /// <typeparam name="UserModel"> UserModel type of user param</typeparam>
    /// <returns>A <see cref="Task{IEnumerable}type<typeparam name="UserModel"}" /> That is our return list for the GetAll crud function</returns>
    public async Task InsertUser(UserModel user) =>
       await _db.SaveData(storedProcedure: "spUser_Insert"
            ,new { user.FirstName, user.LastName });

    //Update 

    ///<summary>Handles the Update sql query</summary>
    /// <param name="user">Accepts a UserModel </param>
    ///<typeparam name="UserModel"> UserModel type of user param</typeparam>
    /// <returns>A <see cref="Task{IEnumerable}" /> That is our return list for the GetAll crud function</returns>
    public async Task UpdateUser(UserModel user) =>
       await _db.SaveData(storedProcedure: "spUser_Update", user);

    //Delete

    ///<summary>Handles the Delete sql query</summary>
    /// <param name="id">The stored procedure of our choice </param>
    /// <returns>A <see cref="Task" /> Returns an awaitable void task</returns>
    public async Task DeleteUser(int id) =>
       await _db.SaveData(storedProcedure: "spUser_Delete", new { Id = id });



}
