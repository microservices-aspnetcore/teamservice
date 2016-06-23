using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace StatlerWaldorfCorp.TeamService
{
	[Route("[controller]")]
	public class TeamController
	{
		// TODO: Inject
		ITeamRepository repository = new MemoryTeamRepository();

		[HttpGet]
        	public IEnumerable<string> Get()
		{
			return repository.GetTeamates();
		}
	}
}
