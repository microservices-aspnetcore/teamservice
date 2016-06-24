using Xunit;
using System.Collections.Generic;
using StatlerWaldorfCorp.TeamService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace StatlerWaldorfCorp.TeamService
{
    public class TeamControllerTest
    {	    
        [Fact]
        public async void QueryTeamListReturnsCorrectTeams()
        {
            TeamController controller = new TeamController(new TestMemoryTeamRepository());
            var rawTeams = (IEnumerable<Team>)(await controller.GetAllTeams() as ObjectResult).Value;
            List<Team> teams = new List<Team>(rawTeams);
            Assert.Equal(teams.Count, 2);
            Assert.Equal(teams[0].Name, "one");
            Assert.Equal(teams[1].Name, "two");            
        }

        [Fact]
        public async void CreateTeamAddsTeamToList() 
        {
            TeamController controller = new TeamController(new TestMemoryTeamRepository());
            var teams = (IEnumerable<Team>)(await controller.GetAllTeams() as ObjectResult).Value;
            List<Team> original = new List<Team>(teams);
            
            Team t = new Team("sample");
            var result = await controller.CreateTeam(t);

            var newTeamsRaw = (IEnumerable<Team>)(await controller.GetAllTeams() as ObjectResult).Value;
            List<Team> newTeams = new List<Team>(newTeamsRaw);
            Assert.Equal(newTeams.Count, original.Count+1);
            var sampleTeam = newTeams.FirstOrDefault( target => target.Name == "sample");
            Assert.NotNull(sampleTeam);            
        }
    }
}