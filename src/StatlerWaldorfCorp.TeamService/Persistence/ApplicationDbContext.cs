using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StatlerWaldorfCorp.TeamService.Models; 
using Npgsql.EntityFrameworkCore.PostgreSQL;

namespace StatlerWaldorfCorp.TeamService.Persistence 
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasPostgresExtension("uuid-ossp");
        }

        public DbSet<Team> Teams {get; set;}
    }
}