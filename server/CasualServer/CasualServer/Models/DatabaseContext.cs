using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasualServer.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Log> Log { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Category> Caterogies { get; set; }
        public DbSet<UserService> UserServices { get; set; }
        public DbSet<Action> Actions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;UserId=server;Password=server;database=mariadb;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {                 
            // использование Fluent API
            base.OnModelCreating(modelBuilder);
        }
    }
}

