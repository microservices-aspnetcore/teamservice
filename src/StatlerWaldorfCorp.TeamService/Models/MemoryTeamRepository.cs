using System.Collections.Generic;

namespace StatlerWaldorfCorp.TeamService.Models
{
	public class MemoryTeamRepository :  ITeamRepository {
		protected string[] _teams;

		public MemoryTeamRepository() {
		}

		public IEnumerable<string> GetTeams() {
			return _teams; 
		}
	}
}