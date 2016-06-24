using System;
using System.Collections.Generic;

namespace StatlerWaldorfCorp.TeamService.Models
{
	public interface ITeamRepository {
	    IEnumerable<Team> GetTeams();
		void AddTeam(Team team);
		void DeleteTeam(Guid id);
	}
}