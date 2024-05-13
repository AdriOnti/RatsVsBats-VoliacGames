using MinimalAPI.Models;
using MySql.Data.MySqlClient;

namespace MinimalAPI.Repositories
{
    public interface IProfilesRepository
    {
        Task<IEnumerable<Profile>> GetAllPerfiles();
        Task<Profile> GetPerfil(int id);
        Task<bool> UpdatePerfil(Profile perfil);
    }
}
