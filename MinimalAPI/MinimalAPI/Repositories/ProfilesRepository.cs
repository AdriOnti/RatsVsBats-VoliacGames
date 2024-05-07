﻿using Dapper;
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
                        UPDATE  public.perfil
                        SET ""nombrePerfil"" = @nombrePerfil,
                            ubicacion  =  @ubicacion,
                            puntuacion = @puntuacion,
                            intentos = @intentos,
                            nivel = @nivel
                        WHERE id_usuario = @id_usuario;
                        ";

            var result = await db.ExecuteAsync(sql, new { perfil.idProfiles, perfil.nickname, perfil.location, perfil.points, perfil.completedMissions, perfil.completedBranches });
            return result > 0;
        }
    }
}
