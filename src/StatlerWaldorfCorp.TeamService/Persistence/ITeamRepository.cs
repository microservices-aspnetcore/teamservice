using System;
using System.Collections.Generic;
using StatlerWaldorfCorp.TeamService.Models;

namespace StatlerWaldorfCorp.TeamService.Persistence
{
	public interface ITeamRepository {
	    IEnumerable<Team> List();
		Team Get(Guid id);
		Team Add(Team team);
		Team Update(Team team);		
		Team Delete(Guid id);
	}
}