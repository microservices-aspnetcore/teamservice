using System;
using System.Collections.Generic;
using System.Linq;
using StatlerWaldorfCorp.TeamService;
using StatlerWaldorfCorp.TeamService.Models;

namespace StatlerWaldorfCorp.TeamService.Persistence
{
	public class MemoryTeamRepository :  ITeamRepository {
		protected static ICollection<Team> teams;

		public MemoryTeamRepository() {
			if(teams == null) {
				teams = new List<Team>();
			}
		}

		public MemoryTeamRepository(ICollection<Team> teams) {
            MemoryTeamRepository.teams = teams;
		}

		public IEnumerable<Team> List() {
			return teams; 
		}

		public Team Get(Guid id) {
			return teams.FirstOrDefault(t => t.ID == id);			
		}

		public Team Update(Team t) 
		{
			Team team = this.Delete(t.ID);
	
			if(team != null) {
				team = this.Add(t);
			}

			return team;
		}

		public Team Add(Team team) 
		{
			teams.Add(team);
			return team;
		}

		public Team Delete(Guid id) {	
			var q = teams.Where(t => t.ID == id);
			Team team = null;

			if (q.Count() > 0) {				
				team = q.First();
				teams.Remove(team);
			}							

			return team; 			
		}
	}
}