using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StatlerWaldorfCorp.TeamService.Models; 
using Npgsql.EntityFrameworkCore.PostgreSQL;

namespace StatlerWaldorfCorp.TeamService.Persistence 
{
    public class TeamDbContext : DbContext
    {
        public TeamDbContext(DbContextOptions<TeamDbContext> options) :base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasPostgresExtension("uuid-ossp");
        }

        public DbSet<Team> Teams {get; set;}
        public DbSet<Member> Members {get; set;}        
    }
}