using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace  HelpWithAll.Core.Models.Response
{
    public class InternalServerErrorResponse
    {
    public string Message { get; set; }

    public InternalServerErrorResponse(string message)
    {
        this.Message = message;
    }
    }
}