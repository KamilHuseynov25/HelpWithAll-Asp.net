using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace  HelpWithAll.Core.Models.Response
{
    public class NotFoundErrorResponse
    {
        public string Key { get; set; }

        public NotFoundErrorResponse(string Key)
        {
            this.Key = Key;
        }
    }
}