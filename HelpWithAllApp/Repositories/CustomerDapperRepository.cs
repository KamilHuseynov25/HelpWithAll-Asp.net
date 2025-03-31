using HelpWithAllApp.Models;
using HelpWithAllApp.Options;
using HelpWithAllApp.Repositories.Base;
using Microsoft.Extensions.Options;
using Npgsql;
using Dapper;

namespace HelpWithAllApp.Repositories;

public class CustomerDapperRepository : ICustomerRepository
{
    private string connectionString;

    public CustomerDapperRepository(IOptionsSnapshot<DatabaseOptions> options)
    {
        this.connectionString = options.Value.ConnectionString;
    }

    public async Task<bool> InsertCustomerAsync(Customer customer)
    {
        using var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync();
        
        var query = @"INSERT INTO Customers (Name, Surname, Email, PhoneNumber, IsHelper)
                    VALUES (@Name, @Surname, @Email, @PhoneNumber, @IsHelper)";

        var result = await connection.ExecuteAsync(query, customer);
        return result > 0;
    }

    public async Task<bool> DeleteCustomerAsync(int id)
    {
        using var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync();
        
        var query = "DELETE FROM Customers WHERE Id = @Id";
        var result = await connection.ExecuteAsync(query, new { Id = id });
        return result > 0;
    }

    public async Task<IEnumerable<Customer>> SelectAllCustomersAsync()
    {
        using var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync();
        
        var query = "SELECT * FROM Customers";
        return await connection.QueryAsync<Customer>(query);
    }

    public async Task<Customer> SelectCustomerByIdAsync(int id)
    {
        using var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync();
        
        var query = "SELECT * FROM Customers WHERE Id = @Id";
        return await connection.QueryFirstOrDefaultAsync<Customer>(query, new { Id = id });
    }

    public async Task<bool> UpdateCustomerAsync(Customer updatedCustomer)
    {
        using var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync();
        
        var query = @"UPDATE Customers SET
                        Name = @Name,
                        Surname = @Surname,
                        Email = @Email,
                        PhoneNumber = @PhoneNumber
                        WHERE Id = @Id";

        var result = await connection.ExecuteAsync(query, updatedCustomer);
        return result > 0;
    }


}
