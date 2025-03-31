
namespace HelpWithAllWApi.Repositories;
using HelpWithAllWApi.Models;
using HelpWithAllWApi.Repositories.Base;
using Dapper;
using Npgsql;


    public class HelperSqlRepository : IHelperRepository
    {
        private readonly string _connectionString = "Host=localhost;Port=5432;Database=helperdb;UID=postgres;Password=Kh2505;";

        public async Task<bool> InsertHelperAsync(Helper helper)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();
            
            var query = @"INSERT INTO Helpers (Name, Surname, Profession, PaymentPerHour, Age, Experience, Avalibility, Rating)  
                        VALUES (@Name, @Surname, @Profession, @PaymentPerHour, @Age, @Experience, @Avalibility, @Rating)";

            var result = await connection.ExecuteAsync(query, helper);
            return result > 0;
        }

        public async Task<bool> DeleteHelperAsync(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();
            
            var query = "DELETE FROM Helpers WHERE Id = @Id";
            var result = await connection.ExecuteAsync(query, new { Id = id });
            return result > 0;
        }

        public async Task<List<Helper>> SelectAllHelpersAsync()
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();
            
            var query = "SELECT * FROM Helpers";
            var helpers = await connection.QueryAsync<Helper>(query);
            return helpers.ToList();
        }

        public async Task<Helper> SelectHelperByIdAsync(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();
            
            var query = "SELECT * FROM Helpers WHERE Id = @Id";
            return await connection.QueryFirstAsync<Helper>(query, new { Id = id });
        }

        public async Task<bool> UpdateHelperAsync(Helper updatedHelper)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();
            
            var query = @"UPDATE Helpers SET
                            Name = @Name,
                            Surname = @Surname,
                            Profession = @Profession,
                            PaymentPerHour = @PaymentPerHour,
                            Age = @Age,
                            Experience = @Experience,
                            Avalibility = @Avalibility,
                            Rating = @Rating
                            WHERE Id = @Id";

            var result = await connection.ExecuteAsync(query, updatedHelper);
            return result > 0;
        }
    }
