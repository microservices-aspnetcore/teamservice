using Xunit;
using System.Collections.Generic;
using StatlerWaldorfCorp.TeamService.Models;
using Microsoft.AspNetCore.Mvc;
using System;
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
            //TODO: also assert that the destination URL of the new team reflects the team's GUID
            Assert.Equal((result as ObjectResult).StatusCode, 201);

            var actionResult = await controller.GetAllTeams() as ObjectResult;            
            var newTeamsRaw = (IEnumerable<Team>)(await controller.GetAllTeams() as ObjectResult).Value;
            List<Team> newTeams = new List<Team>(newTeamsRaw);
            Assert.Equal(newTeams.Count, original.Count+1);
            var sampleTeam = newTeams.FirstOrDefault( target => target.Name == "sample");
            Assert.NotNull(sampleTeam);            
        }

        [Fact]
        public async void DeleteTeamRemovesFromList() 
        {
            TeamController controller = new TeamController(new TestMemoryTeamRepository());
            var teams = (IEnumerable<Team>)(await controller.GetAllTeams() as ObjectResult).Value;
            int ct = teams.Count();

            string sampleName = "sample";
            Guid id = Guid.NewGuid();
            Team sampleTeam = new Team(sampleName);  
            sampleTeam.ID = id;          
            await controller.CreateTeam(sampleTeam);

            teams = (IEnumerable<Team>)(await controller.GetAllTeams() as ObjectResult).Value;
            sampleTeam = teams.FirstOrDefault(target => target.Name == sampleName);
            Assert.NotNull(sampleTeam);            

            await controller.DeleteTeam(id);

            teams = (IEnumerable<Team>)(await controller.GetAllTeams() as ObjectResult).Value;
            sampleTeam = teams.FirstOrDefault(target => target.Name == sampleName);
            Assert.Null(sampleTeam);            
        }
    }
}