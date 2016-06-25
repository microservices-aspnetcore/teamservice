using System;
using System.Collections.Generic;

namespace StatlerWaldorfCorp.TeamService.Models
{
	public interface ITeamRepository {
	    IEnumerable<Team> GetTeams();
		Team GetTeam(Guid id);
		void AddTeam(Team team);
		void UpdateTeam(Team team);		
		void DeleteTeam(Guid id);
	}
}