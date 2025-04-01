using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using HelpWithAllApp.Models;
using HelpWithAllApp.Repositories.Base;

namespace HelpWithAllApp.Service .Base
{
    public interface IHttpLogger
    {
    

    public Task LogAsync(HttpContext context, string? message = null);
    }
}