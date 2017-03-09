using Xunit;
using System.Collections.Generic;
using StatlerWaldorfCorp.TeamService.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using StatlerWaldorfCorp.TeamService.LocationClient;
using StatlerWaldorfCorp.TeamService.Persistence;

[assembly:CollectionBehavior(MaxParallelThreads = 1)]

namespace StatlerWaldorfCorp.TeamService
{
    public class MembersControllerTest
    {	    
        [Fact]
        public void TestCreateMemberAddsTeamToList() 
        {
            ITeamRepository repository = new TestMemoryTeamRepository();
            MembersController controller = new MembersController(repository, new MemoryLocationClient());

            Guid teamId = Guid.NewGuid();
            Team team = new Team("TestController", teamId);
            repository.Add(team);        

            Guid newMemberId = Guid.NewGuid();
            Member newMember = new Member(newMemberId);
            controller.CreateMember(newMember, teamId);

            team = repository.Get(teamId);
            Assert.True(team.Members.Contains(newMember));
        }        

        [Fact]
        public void TestCreateMembertoNonexistantTeamReturnsNotFound() 
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
        public async void TestGetExistingMemberReturnsMember() 
        {
            ITeamRepository repository = new TestMemoryTeamRepository();
            MembersController controller = new MembersController(repository, new MemoryLocationClient());

            Guid teamId = Guid.NewGuid();
            Team team = new Team("TestTeam", teamId);
            var debugTeam = repository.Add(team);        

            Guid memberId = Guid.NewGuid();
            Member newMember = new Member(memberId);
            newMember.FirstName = "Jim";
            newMember.LastName = "Smith";
            controller.CreateMember(newMember, teamId);

            Member member = (Member)(await controller.GetMember(teamId, memberId) as ObjectResult).Value;
            Assert.Equal(member.ID, newMember.ID);
        }   

        [Fact]
        public void TestGetMembersReturnsMembers() 
        {
            ITeamRepository repository = new TestMemoryTeamRepository();
            MembersController controller = new MembersController(repository, new MemoryLocationClient());

            Guid teamId = Guid.NewGuid();
            Team team = new Team("TestTeam", teamId);
            var debugTeam = repository.Add(team);        

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
        public void TestGetMembersForNewTeamIsEmpty() 
        {
            ITeamRepository repository = new TestMemoryTeamRepository();
            MembersController controller = new MembersController(repository, new MemoryLocationClient());

            Guid teamId = Guid.NewGuid();
            Team team = new Team("TestTeam", teamId);
            var debugTeam = repository.Add(team);        

            ICollection<Member> members = (ICollection<Member>)(controller.GetMembers(teamId) as ObjectResult).Value;
            Assert.Empty(members);
        }     

        [Fact]
        public void TestGetMembersForNonExistantTeamReturnNotFound() 
        {
            ITeamRepository repository = new TestMemoryTeamRepository();
            MembersController controller = new MembersController(repository, new MemoryLocationClient());

            var result = controller.GetMembers(Guid.NewGuid());
            Assert.True(result is NotFoundResult);
        }           

        [Fact]
        public async void TestGetNonExistantTeamReturnsNotFound() 
        {
            ITeamRepository repository = new TestMemoryTeamRepository();
            MembersController controller = new MembersController(repository, new MemoryLocationClient());

            var result = await controller.GetMember(Guid.NewGuid(), Guid.NewGuid());
            Assert.True(result is NotFoundResult);
        }

        [Fact]
        public async void TestGetNonExistantMemberReturnsNotFound() 
        {
            ITeamRepository repository = new TestMemoryTeamRepository();
            MembersController controller = new MembersController(repository, new MemoryLocationClient());

            Guid teamId = Guid.NewGuid();
            Team team = new Team("TestTeam", teamId);
            var debugTeam = repository.Add(team);        

            var result = await controller.GetMember(teamId, Guid.NewGuid());
            Assert.True(result is NotFoundResult);
        }

        [Fact]
        public void TestUpdateMemberOverwrites() 
        {
            ITeamRepository repository = new TestMemoryTeamRepository();
            MembersController controller = new MembersController(repository, new MemoryLocationClient());

            Guid teamId = Guid.NewGuid();
            Team team = new Team("TestTeam", teamId);
            var debugTeam = repository.Add(team);        

            Guid memberId = Guid.NewGuid();
            Member newMember = new Member(memberId);
            newMember.FirstName = "Jim";
            newMember.LastName = "Smith";
            controller.CreateMember(newMember, teamId);

	        team = repository.Get(teamId);
	    
            Member updatedMember = new Member(memberId);
            updatedMember.FirstName = "Bob";
            updatedMember.LastName = "Jones";            
            controller.UpdateMember(updatedMember, teamId, memberId);
 
            team = repository.Get(teamId);
            Member testMember = team.Members.Where(m => m.ID == memberId).First();
	    
            Assert.Equal(testMember.FirstName, "Bob");
            Assert.Equal(testMember.LastName, "Jones");            
        }           

        [Fact]
        public void TestUpdateMembertoNonexistantMemberReturnsNoMatch() 
        {
            ITeamRepository repository = new TestMemoryTeamRepository();
            MembersController controller = new MembersController(repository, new MemoryLocationClient());

            Guid teamId = Guid.NewGuid();
            Team team = new Team("TestController", teamId);
            repository.Add(team);        

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
        public async void TestAddsLocation() 
        {
            ITeamRepository repository = new TestMemoryTeamRepository();
            MemoryLocationClient locationClient = new MemoryLocationClient();
            MembersController controller = new MembersController(repository, locationClient);

            Guid teamId = Guid.NewGuid();
            Team team = new Team("TestTeam", teamId);
            var debugTeam = repository.Add(team);        

            Guid memberId = Guid.NewGuid();
            Member newMember = new Member(memberId);
            newMember.FirstName = "Jim";
            newMember.LastName = "Smith";
            controller.CreateMember(newMember, teamId);

            await locationClient.AddLocation(memberId, new LocationRecord() {
                Timestamp = 1, Altitude = 123.45f});
            
            Assert.True(locationClient.MemberLocationHistory.ContainsKey(memberId));
        }

        [Fact]
        public async void TestGetNewMemberReturnsMemberWithEmtpyLastLocation() 
        {
            ITeamRepository repository = new TestMemoryTeamRepository();
            MembersController controller = new MembersController(repository, new MemoryLocationClient());

            Guid teamId = Guid.NewGuid();
            Team team = new Team("TestTeam", teamId);
            var debugTeam = repository.Add(team);        

            Guid memberId = Guid.NewGuid();
            Member newMember = new Member(memberId);
            newMember.FirstName = "Jim";
            newMember.LastName = "Smith";
            controller.CreateMember(newMember, teamId);

            LocatedMember member = (LocatedMember)(await controller.GetMember(teamId, memberId) as ObjectResult).Value;
            Assert.Equal(member.ID, newMember.ID);
            Assert.Null(member.LastLocation);
        }

        [Fact]
        public async void TestGetMemberReturnsLastLocation() 
        {
            ITeamRepository repository = new TestMemoryTeamRepository();
            ILocationClient locationClient = new MemoryLocationClient();
            MembersController controller = new MembersController(repository, locationClient);

            Guid teamId = Guid.NewGuid();
            Team team = new Team("TestTeam", teamId);
            var debugTeam = repository.Add(team);        

            Guid memberId = Guid.NewGuid();
            Member newMember = new Member(memberId);
            newMember.FirstName = "Jim";
            newMember.LastName = "Smith";
            controller.CreateMember(newMember, teamId);

            await locationClient.AddLocation(memberId, new LocationRecord() {
                Timestamp = 1, Altitude = 123.45f});
            await locationClient.AddLocation(memberId, new LocationRecord() {
                Timestamp = 3, Altitude = 123.47f});
            await locationClient.AddLocation(memberId, new LocationRecord() {
                Timestamp = 2, Altitude = 123.46f});
            
            LocatedMember member = (LocatedMember)(await controller.GetMember(teamId, memberId) as ObjectResult).Value;

            Assert.Equal(member.ID, newMember.ID);
            Assert.NotNull(member.LastLocation);
            Assert.Equal(123.47f, member.LastLocation.Altitude);            
        }        
#endregion
    }
}