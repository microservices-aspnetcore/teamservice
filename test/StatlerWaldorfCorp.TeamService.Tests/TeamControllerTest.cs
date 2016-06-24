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
            Assert.Equal(service.Get(), new string[] { "one", "two" });
        }
    }
}