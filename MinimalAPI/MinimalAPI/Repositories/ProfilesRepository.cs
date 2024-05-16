using Dapper;
using MinimalAPI.Models;
using MySql.Data.MySqlClient;
using Npgsql;

namespace MinimalAPI.Repositories
{
    // Repositorio de sentencias SQL de la tabla Perfil
    public class ProfilesRepository
    {
        private MySqlConnection connectionString;
        public ProfilesRepository(MySqlConnection connectionString)
        {
            this.connectionString = connectionString;
        }

        // Método de conexión prederterminado.
        protected MySqlConnection dbConnection()
        {
            return new MySqlConnection(connectionString.ConnectionString);
        }

        // Método para recuperar todos los Perfiles de la tabla.
        public async Task<IEnumerable<Profile>> GetAllPerfiles()
        {
            var db = dbConnection();

            //var sql = @"
            //            SELECT idProfiles, nickname, location, completedMissions, completedBranches, points
            //                FROM Profiles

            //            ";
            var sql = @"SELECT * FROM Profiles";

            return await db.QueryAsync<Profile>(sql, new { });
        }

        // Método para recuperar un perfil concreto mediante una ID parametrizada.
        public async Task<Profile> GetPerfil(int id)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT idProfiles, nickname, location, completedMissions, completedBranches, points
                            FROM Profiles
                            WHERE idProfiles = @Id
                        ";

            return await db.QueryFirstOrDefaultAsync<Profile>(sql, new { Id = id });
        }

        // Método para actualizar un perfil mediante un objeto parametrizado.
        public async Task<bool> UpdatePerfil(Profile perfil)
        {
            var db = dbConnection();

            var sql = @"
                        UPDATE  Profiles
                        SET nickname = @Nickname,
                            location  =  @Location,
                            completedMissions = @CompletedMissions,
                            completedBranches = @CompletedBranches,
                            points = @points
                        WHERE idProfiles = @IdProfiles;
                        ";

            var result = await db.ExecuteAsync(sql, new { IdProfiles = perfil.idProfiles, Nickname = perfil.nickname, Location = perfil.location, CompletedMissions = perfil.completedMissions, CompletedBranches = perfil.completedBranches, Points = perfil.points });
            return result > 0;
        }

        public async Task<bool> MissionCompleted(int id, int mission, int points)
        {
            var db = dbConnection();
            var sql = @"
                        UPDATE Profiles
                        SET completedMissions = completedMissions + @CompletedMissions, points = points + @Points
                        WHERE idProfiles = @Id
                      ";
            var result = await db.ExecuteAsync(sql, new { Id = id, CompletedMissions = mission, Points = points });
            return result > 0;
        }
    }
}
