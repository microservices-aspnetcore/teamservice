using System.Collections.Generic;

namespace StatlerWaldorfCorp.TeamService.Models
{
	public class TestMemoryTeamRepository : MemoryTeamRepository {
		public TestMemoryTeamRepository() {
			 _teams = new string []{ "one", "two" };
		}
	}
}

