using DataAccess.Models;

namespace DataAccess.Data;
public interface IUserData
{
    Task DeleteUser(int id);
    Task<IEnumerable<UserModel>> GetAllUsers();
    Task<UserModel?> GetById(int id);
    Task InsertUser(UserModel user);
    Task UpdateUser(UserModel user);
}