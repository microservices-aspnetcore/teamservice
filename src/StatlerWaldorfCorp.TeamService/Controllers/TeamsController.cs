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
	public class TeamsController : Controller
	{
		ITeamRepository repository;

		public TeamsController(ITeamRepository repo) 
		{
			repository = repo;
		}

		[HttpGet]
        public async virtual Task<IActionResult> GetAllTeams()
		{
			return this.Ok(repository.GetTeams());
		}

		[HttpGet("{id}")]
        public async Task<IActionResult> GetTeam(Guid id)
		{
			Team team = repository.GetTeam(id);			
			if (team != null) // I HATE NULLS, MUST FIXERATE THIS.			  
			{				
				return this.Ok(team);
			} else 
			{			
				return this.NotFound();
			}
			
		}		

		[HttpPost]
		public async virtual Task<IActionResult> CreateTeam([FromBody]Team newTeam) 
		{
			repository.AddTeam(newTeam);			

			//TODO: add test that asserts result is a 201 pointing to URL of the created team.
			//TODO: teams need IDs
			//TODO: return created at route to point to team details			
			return this.Created($"/teams/{newTeam.ID}", newTeam);
		}

		[HttpDelete("{id}")]
        public async virtual Task<IActionResult> DeleteTeam(Guid id)
		{
			//TODO: get-then-delete is probably inefficient. Unsuckify later.
			if (repository.GetTeam(id) == null) {
				return this.NotFound();
			} else {
				repository.DeleteTeam(id);
				return this.Ok(id);
			}
		}		
	}
}
