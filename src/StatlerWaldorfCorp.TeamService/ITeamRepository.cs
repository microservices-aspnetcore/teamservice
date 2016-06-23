using System.Collections.Generic;

namespace StatlerWaldorfCorp.TeamService
{
	public interface ITeamRepository {
	       IEnumerable<string> GetTeamates();
	}
}