using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using StatlerWaldorfCorp.TeamService.Models;
using System.Threading.Tasks;
using StatlerWaldorfCorp.TeamService.LocationClient;
using StatlerWaldorfCorp.TeamService.Persistence;

namespace StatlerWaldorfCorp.TeamService
{
	[Route("/teams/{teamId}/[controller]")]
	public class MembersController : Controller
	{
		private ITeamRepository repository;
		private ILocationClient locationClient;

		public MembersController(ITeamRepository repository, ILocationClient locationClient) 
		{
			this.repository = repository;
			this.locationClient = locationClient;
		}

		[HttpGet]
		public virtual IActionResult GetMembers(Guid teamID) 
		{
			Team team = repository.Get(teamID);
			
			if(team == null) {
				return this.NotFound();
			} else {
				return this.Ok(team.Members);
			}			
		}

		[HttpGet]
		[Route("/teams/{teamId}/[controller]/{memberId}")]		
		public async virtual Task<IActionResult> GetMember(Guid teamID, Guid memberId) 
		{
			Team team = repository.Get(teamID);
			
			if(team == null) {
				return this.NotFound();
			} else {
				var q = team.Members.Where(m => m.ID == memberId);

				if(q.Count() < 1) {
					return this.NotFound();
				} else {
					Member member = (Member)q.First();

					return this.Ok(new LocatedMember {
						ID = member.ID,
						FirstName = member.FirstName,
						LastName = member.LastName,
						LastLocation = await this.locationClient.GetLatestForMember(member.ID)
					});
				}
			}
		}

		[HttpGet]
		[Route("/members/{memberId}/team")]
		public IActionResult GetMemberTeamId(Guid memberId) 
		{
			Guid result = GetTeamIdForMember(memberId);
			if (result != Guid.Empty) {
				return this.Ok(new {
					TeamID = result
				});
			} else {
				return this.NotFound();
			}
		}

		[HttpPut]
		[Route("/teams/{teamId}/[controller]/{memberId}")]		
		public virtual IActionResult UpdateMember([FromBody]Member updatedMember, Guid teamID, Guid memberId) 
		{
			Team team = repository.Get(teamID);
			
			if(team == null) {
				return this.NotFound();
			} else {
				var q = team.Members.Where(m => m.ID == memberId);

				if(q.Count() < 1) {
					return this.NotFound();
				} else {
					team.Members.Remove(q.First());
					team.Members.Add(updatedMember);
					this.repository.Update(team);					
					return this.Ok();
				}
			}			
		}

		[HttpPost]
		public virtual IActionResult CreateMember([FromBody]Member newMember, Guid teamID) 
		{
			Team team = repository.Get(teamID);
			
			if(team == null) {
				return this.NotFound();
			} else {
				team.Members.Add(newMember);
				this.repository.Update(team);
				var teamMember = new {TeamID = team.ID, MemberID = newMember.ID};
				return this.Created($"/teams/{teamMember.TeamID}/[controller]/{teamMember.MemberID}", teamMember);
			}
		}

		private Guid GetTeamIdForMember(Guid memberId) 
		{
			foreach (var team in repository.List()) {
				var member = team.Members.FirstOrDefault( m => m.ID == memberId);
				if (member != null) {
					return team.ID;
				}
			}
			return Guid.Empty;
		}
    }
}