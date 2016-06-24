using System.Collections.Generic;

namespace StatlerWaldorfCorp.TeamService.Models
{
	public class MemoryTeamRepository :  ITeamRepository {
		protected IEnumerable<Team> _teams;

		public MemoryTeamRepository() {
		}

		public MemoryTeamRepository(IEnumerable<Team> teams) {
			_teams = teams;
		}

		public IEnumerable<Team> GetTeams() {
			return _teams; 
		}
	}
}