using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using HelpWithAllApp.Models;
using HelpWithAllApp.Options;
using HelpWithAllApp.Repositories.Base;
using Microsoft.Extensions.Options;
using Npgsql;

namespace HelpWithAllApp.Repositories;
public class HttpDapperRepository : IHttpLogRepository
{
    private readonly string connectionString;

    public HttpDapperRepository(IOptionsSnapshot<DatabaseOptions> options)
    {
        this.connectionString = options.Value.ConnectionString;
    }

    public async Task InsertAsync(LogEntity log)
    {
        using var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync();

        await connection.ExecuteAsync(
            @"INSERT INTO logs (url, request_body, request_headers, method_type, client_ip, 
                                creation_date_time, response_body, response_headers, status_code, end_date_time)
            VALUES (@Url, @RequestBody, @RequestHeaders, @MethodType, @ClientIp, 
                    @CreationDateTime, @ResponseBody, @ResponseHeaders, @StatusCode, @EndDateTime);",
            log);
    }
}

