using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelpWithAll.Core.Models;

namespace  HelpWithAll.Core.Repositories.Base;
public interface IHttpLogRepository
{
        public Task InsertAsync(LogEntity log);
}
