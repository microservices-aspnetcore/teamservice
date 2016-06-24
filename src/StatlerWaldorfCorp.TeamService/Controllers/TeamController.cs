using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using StatlerWaldorfCorp.TeamService.Models;

namespace StatlerWaldorfCorp.TeamService
{
	[Route("/teams")]
	public class TeamController
	{
		ITeamRepository _repository;

		public TeamController(ITeamRepository repository) {
			_repository = repository;
		}

		[HttpGet]
        public IEnumerable<Team> GetAllTeams()
		{									
			return _repository.GetTeams();			
		}
	}
}
