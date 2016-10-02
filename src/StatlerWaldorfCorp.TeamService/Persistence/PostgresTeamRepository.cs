using System;
using System.Collections.Generic;
using System.Linq;
using StatlerWaldorfCorp.TeamService;
using StatlerWaldorfCorp.TeamService.Models;

namespace StatlerWaldorfCorp.TeamService.Persistence
{
	public class PostgresTeamRepository :  ITeamRepository {
       	private ApplicationDbContext context;

		public PostgresTeamRepository(ApplicationDbContext context) {
			this.context = context;
		}

		public IEnumerable<Team> GetTeams() {
			return this.context.Teams.ToList();
		}

		public Team GetTeam(Guid id) {
			return this.context.Teams.Single(team => team.ID == id);
		}

		public Team UpdateTeam(Team team) 
		{
			this.context.SaveChanges();
			return team;
		}

		public Team AddTeam(Team team) 
		{
			this.context.Add(team);
			this.context.SaveChanges();
			return team;
		}

		public Team DeleteTeam(Guid id) {	
			Team team = this.GetTeam(id);
			this.context.Remove(team);
			this.context.SaveChanges();
			return team;
		}
	}
}