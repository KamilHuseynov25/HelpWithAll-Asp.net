using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelpWithAllApp.Models;

namespace HelpWithAllApp.Repositories.Base;
public interface IHttpLogRepository
{
        public Task InsertAsync(LogEntity log);
}
