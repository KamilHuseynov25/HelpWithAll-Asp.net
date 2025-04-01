using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Dapper;
using HelpWithAllApp.Models;
using HelpWithAllApp.Repositories.Base;
using HelpWithAllApp.Service.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging.Abstractions;
using Npgsql;

namespace HelpWithAllApp.Middlewares;
public class LoggingMiddleware
{
    private readonly RequestDelegate next;
    private readonly ILogger<LoggingMiddleware> logger;
    

    public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
    {
        this.next = next;
        this.logger = logger;
        
    }

    public async Task Invoke(HttpContext context, IHttpLogger logger)
    {
        await this.next.Invoke(context);
        
        var message = context.Items["exception"];

        await logger.LogAsync(context, message?.ToString());
    }

    
}
