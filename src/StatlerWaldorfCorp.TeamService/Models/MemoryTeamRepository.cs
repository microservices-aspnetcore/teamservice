using System.Collections.Generic;

namespace StatlerWaldorfCorp.TeamService.Models
{
	public class MemoryTeamRepository :  ITeamRepository {
		protected ICollection<Team> _teams;

		public MemoryTeamRepository() {
		}

		public MemoryTeamRepository(ICollection<Team> teams) {
			_teams = teams;
		}

		public IEnumerable<Team> GetTeams() {
			return _teams; 
		}

		public void AddTeam(Team t) 
		{
			_teams.Add(t);
		}
	}
}