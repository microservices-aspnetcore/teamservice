using System.Collections.Generic;

namespace StatlerWaldorfCorp.TeamService
{
	public class MemoryTeamRepository :  ITeamRepository {
	       public IEnumerable<string> GetTeams() {
	                 return new string[] { "one", "two" };
	       }
	}
}