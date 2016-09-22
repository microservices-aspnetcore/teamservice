using Xunit;
using System.Collections.Generic;
using StatlerWaldorfCorp.TeamService.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using StatlerWaldorfCorp.TeamService.LocationClient;

[assembly:CollectionBehavior(MaxParallelThreads = 1)]

namespace StatlerWaldorfCorp.TeamService
{
    public class MembersControllerTest
    {	    
        [Fact]
        public void CreateMemberAddsTeamToList() 
        {
            ITeamRepository repository = new TestMemoryTeamRepository();
            MembersController controller = new MembersController(repository, new MemoryLocationClient());

            Guid teamId = Guid.NewGuid();
            Team team = new Team("TestController", teamId);
            repository.AddTeam(team);        

            Guid newMemberId = Guid.NewGuid();
            Member newMember = new Member(newMemberId);
            controller.CreateMember(newMember, teamId);

            team = repository.GetTeam(teamId);
            Assert.True(team.Members.Contains(newMember));
        }        

        [Fact]
        public void CreateMembertoNonexistantTeamReturnsNotFound() 
        {
            ITeamRepository repository = new TestMemoryTeamRepository();
            MembersController controller = new MembersController(repository, new MemoryLocationClient());

            Guid teamId = Guid.NewGuid();

            Guid newMemberId = Guid.NewGuid();
            Member newMember = new Member(newMemberId);
            var result = controller.CreateMember(newMember, teamId);

            Assert.True(result is NotFoundResult);
        }

        [Fact]
        public void GetExistingMemberReturnsMember() 
        {
            ITeamRepository repository = new TestMemoryTeamRepository();
            MembersController controller = new MembersController(repository, new MemoryLocationClient());

            Guid teamId = Guid.NewGuid();
            Team team = new Team("TestTeam", teamId);
            var debugTeam = repository.AddTeam(team);        

            Guid memberId = Guid.NewGuid();
            Member newMember = new Member(memberId);
            newMember.FirstName = "Jim";
            newMember.LastName = "Smith";
            controller.CreateMember(newMember, teamId);

            Member member = (Member)(controller.GetMember(teamId, memberId) as ObjectResult).Value;
            Assert.Equal(member.ID, newMember.ID);
        }   

        [Fact]
        public void GetMembersReturnsMembers() 
        {
            ITeamRepository repository = new TestMemoryTeamRepository();
            MembersController controller = new MembersController(repository, new MemoryLocationClient());

            Guid teamId = Guid.NewGuid();
            Team team = new Team("TestTeam", teamId);
            var debugTeam = repository.AddTeam(team);        

            Guid firstMemberId = Guid.NewGuid();
            Member newMember = new Member(firstMemberId);
            newMember.FirstName = "Jim";
            newMember.LastName = "Smith";
            controller.CreateMember(newMember, teamId);

            Guid secondMemberId = Guid.NewGuid();
            newMember = new Member(secondMemberId);
            newMember.FirstName = "John";
            newMember.LastName = "Doe";
            controller.CreateMember(newMember, teamId);            

            ICollection<Member> members = (ICollection<Member>)(controller.GetMembers(teamId) as ObjectResult).Value;
            Assert.Equal(2, members.Count());
            Assert.NotNull(members.Where(m => m.ID == firstMemberId).First().ID);            
            Assert.NotNull(members.Where(m => m.ID == secondMemberId).First().ID);                        
        }

        [Fact]
        public void GetMembersForNewTeamIsEmpty() 
        {
            ITeamRepository repository = new TestMemoryTeamRepository();
            MembersController controller = new MembersController(repository, new MemoryLocationClient());

            Guid teamId = Guid.NewGuid();
            Team team = new Team("TestTeam", teamId);
            var debugTeam = repository.AddTeam(team);        

            ICollection<Member> members = (ICollection<Member>)(controller.GetMembers(teamId) as ObjectResult).Value;
            Assert.Empty(members);
        }     

        [Fact]
        public void GetMembersForNonExistantTeamReturnNotFound() 
        {
            ITeamRepository repository = new TestMemoryTeamRepository();
            MembersController controller = new MembersController(repository, new MemoryLocationClient());

            var result = controller.GetMembers(Guid.NewGuid());
            Assert.True(result is NotFoundResult);
        }           

        [Fact]
        public void GetNonExistantTeamReturnsNotFound() 
        {
            ITeamRepository repository = new TestMemoryTeamRepository();
            MembersController controller = new MembersController(repository, new MemoryLocationClient());

            var result = controller.GetMember(Guid.NewGuid(), Guid.NewGuid());
            Assert.True(result is NotFoundResult);
        }

        [Fact]
        public void GetNonExistantMemberReturnsNotFound() 
        {
            ITeamRepository repository = new TestMemoryTeamRepository();
            MembersController controller = new MembersController(repository, new MemoryLocationClient());

            Guid teamId = Guid.NewGuid();
            Team team = new Team("TestTeam", teamId);
            var debugTeam = repository.AddTeam(team);        

            var result = controller.GetMember(teamId, Guid.NewGuid());
            Assert.True(result is NotFoundResult);
        }

        [Fact]
        public void UpdateMemberOverwrites() 
        {
            ITeamRepository repository = new TestMemoryTeamRepository();
            MembersController controller = new MembersController(repository, new MemoryLocationClient());

            Guid teamId = Guid.NewGuid();
            Team team = new Team("TestTeam", teamId);
            var debugTeam = repository.AddTeam(team);        

            Guid memberId = Guid.NewGuid();
            Member newMember = new Member(memberId);
            newMember.FirstName = "Jim";
            newMember.LastName = "Smith";
            controller.CreateMember(newMember, teamId);

	        team = repository.GetTeam(teamId);
	    
            Member updatedMember = new Member(memberId);
            updatedMember.FirstName = "Bob";
            updatedMember.LastName = "Jones";            
            controller.UpdateMember(updatedMember, teamId, memberId);
 
            team = repository.GetTeam(teamId);
            Member testMember = team.Members.Where(m => m.ID == memberId).First();
	    
            Assert.Equal(testMember.FirstName, "Bob");
            Assert.Equal(testMember.LastName, "Jones");            
        }           

        [Fact]
        public void UpdateMembertoNonexistantMemberReturnsNoMatch() 
        {
            ITeamRepository repository = new TestMemoryTeamRepository();
            MembersController controller = new MembersController(repository, new MemoryLocationClient());

            Guid teamId = Guid.NewGuid();
            Team team = new Team("TestController", teamId);
            repository.AddTeam(team);        

            Guid memberId = Guid.NewGuid();
            Member newMember = new Member(memberId);
            newMember.FirstName = "Jim";
            controller.CreateMember(newMember, teamId);

            Guid nonMatchedGuid = Guid.NewGuid();
            Member updatedMember = new Member(nonMatchedGuid);
            updatedMember.FirstName = "Bob";
            var result = controller.UpdateMember(updatedMember, teamId, nonMatchedGuid);            

            Assert.True(result is NotFoundResult);
        }         

#region "location" 
        [Fact]
        public async void AddsLocation() 
        {
            ITeamRepository repository = new TestMemoryTeamRepository();
            MemoryLocationClient locationClient = new MemoryLocationClient();
            MembersController controller = new MembersController(repository, locationClient);

            Guid teamId = Guid.NewGuid();
            Team team = new Team("TestTeam", teamId);
            var debugTeam = repository.AddTeam(team);        

            Guid memberId = Guid.NewGuid();
            Member newMember = new Member(memberId);
            newMember.FirstName = "Jim";
            newMember.LastName = "Smith";
            await controller.CreateMember(newMember, teamId);

            locationClient.AddLocation(memberId, new LocationRecord() {
                Timestamp = 1, Altitude = 123.45f});
            
            Assert.True(locationClient.MemberLocationHistory.ContainsKey(memberId));
        }

        [Fact]
        public async void GetNewMemberReturnsMemberWithEmtpyLastLocation() 
        {
            ITeamRepository repository = new TestMemoryTeamRepository();
            MembersController controller = new MembersController(repository, new MemoryLocationClient());

            Guid teamId = Guid.NewGuid();
            Team team = new Team("TestTeam", teamId);
            var debugTeam = repository.AddTeam(team);        

            Guid memberId = Guid.NewGuid();
            Member newMember = new Member(memberId);
            newMember.FirstName = "Jim";
            newMember.LastName = "Smith";
            await controller.CreateMember(newMember, teamId);

            LocatedMember member = (LocatedMember)(await controller.GetMember(teamId, memberId) as ObjectResult).Value;
            Assert.Equal(member.ID, newMember.ID);
            Assert.Null(member.LastLocation);
        }

        [Fact]
        public async void GetMemberReturnsLastLocation() 
        {
            ITeamRepository repository = new TestMemoryTeamRepository();
            ILocationClient locationClient = new MemoryLocationClient();
            MembersController controller = new MembersController(repository, locationClient);

            Guid teamId = Guid.NewGuid();
            Team team = new Team("TestTeam", teamId);
            var debugTeam = repository.AddTeam(team);        

            Guid memberId = Guid.NewGuid();
            Member newMember = new Member(memberId);
            newMember.FirstName = "Jim";
            newMember.LastName = "Smith";
            await controller.CreateMember(newMember, teamId);

            locationClient.AddLocation(memberId, new LocationRecord() {
                Timestamp = 1, Altitude = 123.45f});
            locationClient.AddLocation(memberId, new LocationRecord() {
                Timestamp = 3, Altitude = 123.47f});
            locationClient.AddLocation(memberId, new LocationRecord() {
                Timestamp = 2, Altitude = 123.46f});
            
            LocatedMember member = (LocatedMember)(await controller.GetMember(teamId, memberId) as ObjectResult).Value;

            Assert.Equal(member.ID, newMember.ID);
            Assert.NotNull(member.LastLocation);
            Assert.Equal(123.47f, member.LastLocation.Altitude);            
        }        
#endregion
    }
}