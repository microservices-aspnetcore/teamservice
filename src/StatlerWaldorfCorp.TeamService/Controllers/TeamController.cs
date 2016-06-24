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
	[Route("/teams")]
	public class TeamController : Controller
	{
		ITeamRepository repository;

		public TeamController(ITeamRepository repo) 
		{
			repository = repo;
		}

		[HttpGet]
        public async virtual Task<IActionResult> GetAllTeams()
		{
			return this.Ok(repository.GetTeams());
		}

		[HttpGet]
        public async virtual Task<IActionResult> GetTeam(Guid id)
		{
			return this.Ok(repository.GetTeam(id));
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

		[HttpDelete]
        public async virtual Task<IActionResult> DeleteTeam(Guid id)
		{
			repository.DeleteTeam(id);
			return this.Ok(id);
		}		
	}
}
