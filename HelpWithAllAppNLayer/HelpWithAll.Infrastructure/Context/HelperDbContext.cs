using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Threading.Tasks;
using HelpWithAll.Core.Models;
using Microsoft.EntityFrameworkCore;
namespace HelpWithAll.Infrastructure.Context;
    public class HelperDbContext : DbContext 
    {
        public DbSet<Helper> Helpers{get; set;}
        public DbSet<Customer> Customers{get; set;}

        public HelperDbContext(DbContextOptions<HelperDbContext> options) : base(options){}
    }

