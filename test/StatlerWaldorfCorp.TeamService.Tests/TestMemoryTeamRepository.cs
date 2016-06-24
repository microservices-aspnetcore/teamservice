using System.Collections.Generic;

namespace StatlerWaldorfCorp.TeamService.Models
{
	public class TestMemoryTeamRepository : MemoryTeamRepository {
		public TestMemoryTeamRepository() : base(new Team[] { new Team("one"), new Team("two") }) {
			 
		}
	}
}

