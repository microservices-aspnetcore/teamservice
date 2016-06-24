using Xunit;
using System.Collections.Generic;
using StatlerWaldorfCorp.TeamService.Models;

namespace StatlerWaldorfCorp.TeamService
{
    public class TeamControllerTest
    {
	    TeamController service = new TeamController(new TestMemoryTeamRepository());

        [Fact]
        public void PassingTest()
        {
            List<Team> teams = new List<Team>(service.Get());
            Assert.Equal(teams[0].Name, "one");
            Assert.Equal(teams[1].Name, "two");            
        }
    }
}