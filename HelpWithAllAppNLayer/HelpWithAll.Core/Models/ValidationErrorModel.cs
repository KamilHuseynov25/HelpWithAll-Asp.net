using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpWithAll.Core.Models;
public class ValidationErrorModel
{
    public string? Message { get; set; }
    public string? Property { get; set; }
}
