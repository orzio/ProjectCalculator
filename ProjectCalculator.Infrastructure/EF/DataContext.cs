using Microsoft.EntityFrameworkCore;
using ProjectCalculator.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectCalculator.Infrastructure.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
           : base(options) {}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }


        public DbSet<User> Users { get; set; }
        public DbSet<Token> Tokens { get; set; }
       
    }
}
