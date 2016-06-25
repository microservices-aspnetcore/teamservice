using System;
using System.Collections.Generic;

namespace StatlerWaldorfCorp.TeamService.Models
{
	public interface ITeamRepository {
	    IEnumerable<Team> GetTeams();
		Team GetTeam(Guid id);
		Team AddTeam(Team team);
		Team UpdateTeam(Team team);		
		Team DeleteTeam(Guid id);
	}
}