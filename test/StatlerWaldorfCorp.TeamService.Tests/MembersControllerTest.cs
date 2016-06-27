using Xunit;
using System.Collections.Generic;
using StatlerWaldorfCorp.TeamService.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace StatlerWaldorfCorp.TeamService
{
    public class MembersControllerTest
    {	    
        [Fact]
        public async void CreateMemberAddsTeamToList() 
        {
            ITeamRepository repository = new TestMemoryTeamRepository();
            MembersController controller = new MembersController(repository);

            Guid teamId = Guid.NewGuid();
            Team team = new Team("TestController", teamId);
            repository.AddTeam(team);        

            Guid newMemberId = Guid.NewGuid();
            Member newMember = new Member(newMemberId);
            await controller.CreateMember(newMember, teamId);

            team = repository.GetTeam(teamId);
            Assert.True(team.Members.Contains(newMember));
        }        

        [Fact]
        public async void CreateMembertoNonexistantTeamReturnsNotFound() 
        {
            ITeamRepository repository = new TestMemoryTeamRepository();
            MembersController controller = new MembersController(repository);

            Guid teamId = Guid.NewGuid();

            Guid newMemberId = Guid.NewGuid();
            Member newMember = new Member(newMemberId);
            var result = await controller.CreateMember(newMember, teamId);

            Assert.True(result is NotFoundResult);
        }
    }
}