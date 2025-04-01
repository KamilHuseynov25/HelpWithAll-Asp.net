using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using HelpWithAllApp.Models;
using HelpWithAllApp.Repositories.Base;
using HelpWithAllApp.Service.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;

namespace HelpWithAllApp.Service
{
    public class HttpLogger : IHttpLogger
    {
        private readonly IHttpLogRepository repository;

        public HttpLogger(IHttpLogRepository repository)
        {
            this.repository = repository;
        }

        

public async Task LogAsync(HttpContext context, string? message = null)
{
    try
    {
        context.Request.EnableBuffering();

        string requestUrl = context.Request.GetDisplayUrl();
        if (string.IsNullOrEmpty(requestUrl))
        {
            requestUrl = "[UNKNOWN URL]";
        }

        string requestBody = string.Empty;
        using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true))
        {
            requestBody = await reader.ReadToEndAsync();
            context.Request.Body.Position = 0;
        }

        var logEntry = new LogEntity
        {
            Url = requestUrl,
            RequestBody = requestBody,
            RequestHeaders = context.Request.Headers.ToString(),
            MethodType = context.Request.Method,
            ClientIp = context.Connection.RemoteIpAddress?.ToString(),
            CreationDateTime = DateTime.UtcNow
        };

        await repository.InsertAsync(logEntry);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Logging error: {ex.Message}");
    }
}

    }
}

