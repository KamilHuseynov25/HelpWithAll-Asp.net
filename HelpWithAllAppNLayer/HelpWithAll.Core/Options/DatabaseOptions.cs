using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelpWithAll.Core.Options
{
    public class DatabaseOptions
    {
    public string ConnectionString { get; set; }

    public DatabaseOptions(string connectionString)
    {
        this.ConnectionString = connectionString;
    }

    public DatabaseOptions()
    {
        this.ConnectionString = string.Empty;
    }
    }
}