using System;
using System.Collections.Generic;
using System.Linq;
using StatlerWaldorfCorp.TeamService;
using StatlerWaldorfCorp.TeamService.Models;
using Microsoft.EntityFrameworkCore;

namespace StatlerWaldorfCorp.TeamService.Persistence
{
	public class TeamRepository :  ITeamRepository {
       	private TeamDbContext context;

		public TeamRepository(TeamDbContext context) {
			this.context = context;
		}

		public IEnumerable<Team> List() {
			return this.context.Teams.ToList();
		}

		public Team Get(Guid id) {
			return this.context.Teams.Include(team => team.Members).Single(team => team.ID == id);
		}

		public Team Update(Team team) 
		{
			this.context.SaveChanges();
			return team;
		}

		public Team Add(Team team) 
		{
			this.context.Add(team);
			this.context.SaveChanges();
			return team;
		}

		public Team Delete(Guid id) {	
			Team team = this.Get(id);
			this.context.Remove(team);
			this.context.SaveChanges();
			return team;
		}
	}
}