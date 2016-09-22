using System;
using System.Collections.Generic;
using System.Linq;
using StatlerWaldorfCorp.TeamService;
using StatlerWaldorfCorp.TeamService.Models;

namespace StatlerWaldorfCorp.TeamService.Persistence
{
	public class MemoryTeamRepository :  ITeamRepository {
		protected static ICollection<Team> _teams;

		public MemoryTeamRepository() {
			if(_teams == null) {
				_teams = new List<Team>();
			}
		}

		public MemoryTeamRepository(ICollection<Team> teams) {
			_teams = teams;
		}

		public IEnumerable<Team> GetTeams() {
			return _teams; 
		}

		public Team GetTeam(Guid id) {
			return _teams.FirstOrDefault(t => t.ID == id);			
		}

		public Team UpdateTeam(Team t) 
		{
			Team team = this.DeleteTeam(t.ID);
	
			if(team != null) {
				team = this.AddTeam(t);
			}

			return team;
		}

		public Team AddTeam(Team team) 
		{
			_teams.Add(team);
			return team;
		}

		public Team DeleteTeam(Guid id) {	
			var q = _teams.Where(t => t.ID == id);
			Team team = null;

			if (q.Count() > 0) {				
				team = q.First();
				_teams.Remove(team);
			}							

			return team; 			
		}
	}
}