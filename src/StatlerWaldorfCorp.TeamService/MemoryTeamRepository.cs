using System.Collections.Generic;

namespace StatlerWaldorfCorp.TeamService
{
	public class MemoryTeamRepository :  ITeamRepository {
	       public IEnumerable<string> GetTeamates() {
	                 return new string[] { "one", "2" };
	       }
	}
}