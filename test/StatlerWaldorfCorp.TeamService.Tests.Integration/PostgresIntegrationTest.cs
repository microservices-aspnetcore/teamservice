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
        private TeamDbContext context;

        public PostgresIntegrationTest()
        {
			config = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddCloudFoundry()
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<TeamDbContext>();
            optionsBuilder.UseNpgsql(config);
            this.context = new TeamDbContext(optionsBuilder.Options);                
        }

        [Fact]
        public void ShouldInsert()
        {
            Guid id = Guid.NewGuid();
            Team team = new Team() {Name = "Team " + id.ToString(), ID = id};

            TeamRepository repository = new TeamRepository(context);
            repository.Add(team);

            Team savedTeam = repository.Get(id);

            Assert.NotNull(savedTeam);
            Assert.Equal(savedTeam.ID, id);
        }
    }
}