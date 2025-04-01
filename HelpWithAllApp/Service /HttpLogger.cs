using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HelpWithAllApp.Models;
using HelpWithAllApp.Repositories.Base;
using HelpWithAllApp.Service.Base;
using Microsoft.AspNetCore.Http;

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
            context.Request.EnableBuffering();
            string requestBody = string.Empty;
            using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true))
            {
                requestBody = await reader.ReadToEndAsync();
                context.Request.Body.Position = 0;
            }

            string responseBody = string.Empty;
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            using (var reader = new StreamReader(context.Response.Body, Encoding.UTF8, leaveOpen: true))
            {
                responseBody = await reader.ReadToEndAsync();
                context.Response.Body.Seek(0, SeekOrigin.Begin);
            }

            var log = new LogEntity()
            {
                
                Url = context.Request.Path,
                RequestBody = requestBody,
                RequestHeaders = context.Request.Headers.ToString(),
                MethodType = context.Request.Method,
                ClientIp = context.Connection.RemoteIpAddress?.ToString(),
                CreationDateTime = DateTime.UtcNow,
                ResponseBody = responseBody,
                ResponseHeaders = context.Response.Headers.ToString(),
                StatusCode = context.Response.StatusCode,
                EndDateTime = DateTime.UtcNow
            };

            await this.repository.InsertAsync(log);
        }
    }
}
