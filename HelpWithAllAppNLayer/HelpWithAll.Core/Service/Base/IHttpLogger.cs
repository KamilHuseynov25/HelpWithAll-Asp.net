using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using HelpWithAll.Core.Models;
using HelpWithAll.Core.Repositories.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
namespace HelpWithAll.Core.Service .Base
{
    public interface IHttpLogger
    {
    

    public Task LogAsync(HttpContext context, string? message = null);
    }


}