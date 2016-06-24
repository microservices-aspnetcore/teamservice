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

		[HttpPost]
		public async virtual Task<IActionResult> CreateTeam([FromBody]Team newTeam) 
		{
			repository.AddTeam(newTeam);
			return this.Ok();
		}
	}
}
