using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using HelpWithAll.Core.Models.Response;
using Microsoft.AspNetCore.Http;

namespace HelpWithAllApp.Middlewares;
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (ArgumentNullException ex)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await httpContext.Response.WriteAsJsonAsync(new BadRequestResponse(ex.Message) { Parameter = ex.ParamName });
                httpContext.Items["exception"] = ex.Message;
            }
            catch (HttpRequestException ex)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await httpContext.Response.WriteAsJsonAsync(new BadRequestResponse(ex.Message));
                httpContext.Items["exception"] = ex.Message;
            }
            catch (InvalidOperationException ex)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await httpContext.Response.WriteAsJsonAsync(new BadRequestResponse(ex.Message));
                httpContext.Items["exception"] = ex.Message;
            }
            catch (KeyNotFoundException ex)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                await httpContext.Response.WriteAsJsonAsync(new { Message = "Resource not found", Details = ex.Message });
                httpContext.Items["exception"] = ex.Message;
            }
            catch (FileNotFoundException ex)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                await httpContext.Response.WriteAsJsonAsync(new { Message = "Resource not found", Details = ex.Message });
                httpContext.Items["exception"] = ex.Message;
            }
            catch (Exception ex)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await httpContext.Response.WriteAsJsonAsync(new InternalServerErrorResponse("Internal server error"));
                httpContext.Items["exception"] = ex.ToString();
            }
        }
    }
