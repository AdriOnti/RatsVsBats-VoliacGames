using MinimalAPI.Models;
using MySql.Data.MySqlClient;

namespace MinimalAPI.Repositories
{
    public interface IUsersRepository
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUser(int id);
        Task<bool> InsertUser(NoIdUser usuario);
        Task<bool> UpdateUser(User usuario);
        Task<bool> DeleteUser(User usuario);
    }
}
