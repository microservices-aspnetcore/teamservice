using System;
using System.Collections.Generic;
using System.Linq;

namespace StatlerWaldorfCorp.TeamService.Models
{
	public class MemoryTeamRepository :  ITeamRepository {
		protected static ICollection<Team> _teams;

		public MemoryTeamRepository() {
			_teams = new List<Team>();
		}

		public MemoryTeamRepository(ICollection<Team> teams) {
			_teams = teams;
		}

		public IEnumerable<Team> GetTeams() {
			return _teams; 
		}

		public Team GetTeam(Guid id) {
			return _teams.FirstOrDefault( t => t.ID == id);			
		}

		public void AddTeam(Team t) 
		{
			_teams.Add(t);
		}

		public void DeleteTeam(Guid id) {
			var q = _teams.Where(t => t.ID == id);
			if (q.Count() > 0) {				
				_teams.Remove(q.First());
			}						 			
		}
	}
}