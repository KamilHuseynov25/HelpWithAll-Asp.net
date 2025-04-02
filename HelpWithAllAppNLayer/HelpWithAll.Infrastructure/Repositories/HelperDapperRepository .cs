using HelpWithAll.Core.Models;
using HelpWithAll.Core.Options;
using HelpWithAll.Core.Repositories.Base;
using Microsoft.Extensions.Options;
using Npgsql;
using Dapper;
namespace HelpWithAll.Infrastructure.Repositories;

public class HelperDapperRepository : IHelperRepository
{
    private string connectionString;


    public HelperDapperRepository(IOptionsSnapshot<DatabaseOptions> options){
        this.connectionString = options.Value.ConnectionString;
    }
        public async Task<bool> InsertHelperAsync(Helper helper)
        {
            using var connection = new NpgsqlConnection(connectionString);
            await connection.OpenAsync();
            
            var query = @"INSERT INTO Helpers (Name, Surname, Profession, PaymentPerHour, Age, Experience, Avalibility, Rating)
                        VALUES (@Name, @Surname, @Profession, @PaymentPerHour, @Age, @Experience, true, @Rating)";

            var result = await connection.ExecuteAsync(query, helper);
            return result > 0;
        }

        public async Task<bool> DeleteHelperAsync(int id)
        {
            using var connection = new NpgsqlConnection(connectionString);
            await connection.OpenAsync();
            
            var query = "DELETE FROM Helpers WHERE Id = @Id";
            var result = await connection.ExecuteAsync(query, new { Id = id });
            return result > 0;
        }

        public async Task<IEnumerable<Helper>> SelectAllHelpersAsync()
        {
            using var connection = new NpgsqlConnection(connectionString);
            await connection.OpenAsync();
            
            var query = "SELECT * FROM Helpers";
            return await connection.QueryAsync<Helper>(query);
        }

        public async Task<Helper> SelectHelperByIdAsync(int id)
        {
            using var connection = new NpgsqlConnection(connectionString);
            await connection.OpenAsync();
            
            var query = "SELECT * FROM Helpers WHERE Id = @Id";
            return await connection.QueryFirstAsync<Helper>(query, new { Id = id });
        }

        public async Task<bool> UpdateHelperAsync(Helper updatedHelper)
        {
            using var connection = new NpgsqlConnection(connectionString);
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
