using Xunit;
using System.Collections.Generic;
using StatlerWaldorfCorp.TeamService.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Steeltoe.Extensions.Configuration;
using Steeltoe.CloudFoundry.Connector.PostgreSql.EFCore;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using StatlerWaldorfCorp.TeamService.Persistence;
using StatlerWaldorfCorp.TeamService.Models;

namespace StatlerWaldorfCorp.TeamService.Integration
{
    public class PostgresIntegrationTest
    {	    
        private IConfigurationRoot config;
        private ApplicationDbContext context;

        public PostgresIntegrationTest()
        {
			config = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddCloudFoundry()
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseNpgsql(config);
            this.context = new ApplicationDbContext(optionsBuilder.Options);                
        }

        [Fact]
        public void ShouldInsert()
        {
            Guid id = Guid.NewGuid();
            Team team = new Team() {Name = "Team " + id.ToString(), ID = id};

            PostgresTeamRepository repository = new PostgresTeamRepository(context);
            repository.AddTeam(team);

            Team savedTeam = repository.GetTeam(id);

            Assert.NotNull(savedTeam);
            Assert.Equal(savedTeam.ID, id);
        }
    }
}