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
		[HttpGet]
        	public IEnumerable<string> Get()
		{
			return new string[] { "one", "two" };
		}
	}
}
       