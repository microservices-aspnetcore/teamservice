using System;
using System.Collections.Generic;
using System.Linq;

namespace StatlerWaldorfCorp.TeamService.Models
{
	public class MemoryTeamRepository :  ITeamRepository {
		protected ICollection<Team> _teams;

		public MemoryTeamRepository() {
			_teams = new List<Team>();
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

		public void DeleteTeam(Guid id) {			
			_teams.Remove(_teams.Where(t => t.ID == id).First());
		}
	}
}