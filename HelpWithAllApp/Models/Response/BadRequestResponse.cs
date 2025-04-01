using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpWithAllApp.Models.Response
{
    public class BadRequestResponse
    {
    public string? Parameter { get; set; }
    public string Message { get; set; }

    public BadRequestResponse(string parameter, string message)
    {
        this.Message = message;
        this.Parameter = parameter;
    }

    public BadRequestResponse(string message)
    {
        this.Message = message;
    }
    }
}