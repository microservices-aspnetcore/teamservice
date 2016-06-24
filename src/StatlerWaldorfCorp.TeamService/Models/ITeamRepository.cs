using System.Collections.Generic;

namespace StatlerWaldorfCorp.TeamService.Models
{
	public interface ITeamRepository {
	       IEnumerable<Team> GetTeams();
	}
}