using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using StatlerWaldorfCorp.TeamService.Models;
using System.Threading.Tasks;

namespace StatlerWaldorfCorp.TeamService
{
	[Route("[controller]")]
	public class MembersController : Controller
	{
		ITeamRepository repository;

		public MembersController(ITeamRepository repo) 
		{
			repository = repo;
		}
        
		[HttpPost]
		[Route("/teams/{teamId}/[controller]")]
		public async virtual Task<IActionResult> CreateMember([FromBody]Member newMember, Guid teamID) 
		{
			Team team = repository.GetTeam(teamID);
            team.Members.Add(newMember);
			var teamMember = new {TeamID = team.ID, MemberID = newMember.ID};
			return Created($"/teams/{teamMember.TeamID}/[controller]/{teamMember.MemberID}", teamMember);
		}        
    }
}