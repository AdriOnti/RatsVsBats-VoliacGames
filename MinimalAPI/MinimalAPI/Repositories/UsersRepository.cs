using Dapper;
using MinimalAPI.Models;
using Npgsql;
using System.Text.Json.Nodes;
using MySql.Data.MySqlClient;

namespace MinimalAPI.Repositories
{
    // Repositorio de Sentencias SQL de la tabla Usuario.
    public class UsersRepository
    {
        private MySqlConnection connectionString;
        public UsersRepository(MySqlConnection connectionString)
        {
            this.connectionString = connectionString;
        }

        // Método de conexión predeterminado.
        protected MySqlConnection dbConnection()
        {
            return new MySqlConnection(connectionString.ConnectionString);
        }

        // Método para recuperar todos los usuarios de la tabla.
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            var db = dbConnection();

            var sql = @"SELECT * FROM Users";
            //var sql = @"
            //            SELECT idProfiles, nickname, location, completedMissions, completedBranches, points
            //                FROM Profiles
            //            ";

            return await db.QueryAsync<User>(sql, new { });
        }

        // Método para recuperar un usuario concreto mediante una ID parametrizada.
        public async Task<User> GetUser(int id)
        {
            var db = dbConnection();

            var sql = @"
                        SELECT *
                            FROM Users
                            WHERE idUsers = @Id
                        ";

            return await db.QueryFirstOrDefaultAsync<User>(sql, new { Id = id });
        }

        // Método para insertar un usuario mediante un objeto parametrizado.
        public async Task<bool> InsertUser(NoIdUser usuario)
        {
            var db = dbConnection();

            var sql = @"
                        INSERT INTO Users (userEmail, userPassword)
                        VALUES (@user, @password)
                        ";

            var result = await db.ExecuteAsync(sql, new { user = usuario.userEmail, password = usuario.userPassword });
            return result > 0;
        }

        // Método para actualizar un usuario mediante un objeto parametrizado.
        public async Task<bool> UpdateUser(User usuario)
        {
            var db = dbConnection();

            var sql = @"
                        UPDATE  public.usuario
                        SET usuario = @usuario,
                            password  =  @password
                        WHERE id = @id;
                        ";

            var result = await db.ExecuteAsync(sql, new { usuario.userEmail, usuario.userPassword, usuario.idUsers });
            return result > 0;
        }

        // Método para eliminar un usuario mediante un objeto parametrizado.
        public async Task<bool> DeleteUser(User usuario)
        {
            var db = dbConnection();

            var sql = @"
                        DELETE FROM public.usuario
                        WHERE id = @Id;
                        ";

            var result = await db.ExecuteAsync(sql, new { Id = usuario.idUsers });
            return result > 0;
        }
    }
}
