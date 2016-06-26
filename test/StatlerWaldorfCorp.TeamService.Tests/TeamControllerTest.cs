using Xunit;
using System.Collections.Generic;
using StatlerWaldorfCorp.TeamService.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace StatlerWaldorfCorp.TeamService
{
    public class TeamsControllerTest
    {	    
        [Fact]
        public async void QueryTeamListReturnsCorrectTeams()
        {
            TeamsController controller = new TeamsController(new TestMemoryTeamRepository());
            var rawTeams = (IEnumerable<Team>)(await controller.GetAllTeams() as ObjectResult).Value;
            List<Team> teams = new List<Team>(rawTeams);
            Assert.Equal(teams.Count, 2);
            Assert.Equal(teams[0].Name, "one");
            Assert.Equal(teams[1].Name, "two");            
        }

        [Fact]
        public async void GetTeamRetrievesTeam() 
        {
            TeamsController controller = new TeamsController(new TestMemoryTeamRepository());

            string sampleName = "sample";
            Guid id = Guid.NewGuid();
            Team sampleTeam = new Team(sampleName, id);
            await controller.CreateTeam(sampleTeam);

            Team retrievedTeam = (Team)(await controller.GetTeam(id) as ObjectResult).Value;
            Assert.Equal(retrievedTeam.Name, sampleName);
            Assert.Equal(retrievedTeam.ID, id);            
        }

        [Fact]
        public async void GetNonExistentTeamReturnsNotFound() 
        {
            TeamsController controller = new TeamsController(new TestMemoryTeamRepository());

            Guid id = Guid.NewGuid();
            var result = await controller.GetTeam(id);
            Assert.True(result is NotFoundResult);                                
        }

        [Fact]
        public async void CreateTeamAddsTeamToList() 
        {
            TeamsController controller = new TeamsController(new TestMemoryTeamRepository());
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
        public async void UpdateTeamModifiesTeamToList() 
        {
            TeamsController controller = new TeamsController(new TestMemoryTeamRepository());
            var teams = (IEnumerable<Team>)(await controller.GetAllTeams() as ObjectResult).Value;
            List<Team> original = new List<Team>(teams);
            
            Guid id = Guid.NewGuid();
            Team t = new Team("sample", id);
            var result = await controller.CreateTeam(t);

            Team newTeam = new Team("sample2", id);
            controller.UpdateTeam(newTeam, id);

            var newTeamsRaw = (IEnumerable<Team>)(await controller.GetAllTeams() as ObjectResult).Value;
            List<Team> newTeams = new List<Team>(newTeamsRaw);
            var sampleTeam = newTeams.FirstOrDefault( target => target.Name == "sample");
            Assert.Null(sampleTeam);

            Team retrievedTeam = (Team)(await controller.GetTeam(id) as ObjectResult).Value;
            Assert.Equal(retrievedTeam.Name, "sample2");
        }        

        [Fact]
        public async void UpdateNonExistentTeamReturnsNotFound() 
        {
            TeamsController controller = new TeamsController(new TestMemoryTeamRepository());
            var teams = (IEnumerable<Team>)(await controller.GetAllTeams() as ObjectResult).Value;
            List<Team> original = new List<Team>(teams);
            
            Team someTeam = new Team("Some Team", Guid.NewGuid());
            await controller.CreateTeam(someTeam);

            Guid newTeamId = Guid.NewGuid();
            Team newTeam = new Team("New Team", newTeamId);
            var result = await controller.UpdateTeam(newTeam, newTeamId);

            Assert.True(result is NotFoundResult);
        }

        [Fact]
        public async void DeleteTeamRemovesFromList() 
        {
            TeamsController controller = new TeamsController(new TestMemoryTeamRepository());
            var teams = (IEnumerable<Team>)(await controller.GetAllTeams() as ObjectResult).Value;
            int ct = teams.Count();

            string sampleName = "sample";
            Guid id = Guid.NewGuid();
            Team sampleTeam = new Team(sampleName, id);
            await controller.CreateTeam(sampleTeam);

            teams = (IEnumerable<Team>)(await controller.GetAllTeams() as ObjectResult).Value;
            sampleTeam = teams.FirstOrDefault(target => target.Name == sampleName);
            Assert.NotNull(sampleTeam);            

            await controller.DeleteTeam(id);

            teams = (IEnumerable<Team>)(await controller.GetAllTeams() as ObjectResult).Value;
            sampleTeam = teams.FirstOrDefault(target => target.Name == sampleName);
            Assert.Null(sampleTeam);            
        }

        [Fact]
        public async void DeleteNonExistentTeamReturnsNotFound() 
        {
            TeamsController controller = new TeamsController(new TestMemoryTeamRepository());
            Guid id = Guid.NewGuid();

            var result = await controller.DeleteTeam(id);
            Assert.True(result is NotFoundResult);
        }
    }
}