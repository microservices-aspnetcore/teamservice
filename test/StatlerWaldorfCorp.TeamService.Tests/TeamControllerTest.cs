using Xunit;
using System.Collections.Generic;

namespace StatlerWaldorfCorp.TeamService
{
    public class TeamControllerTest
    {
	TeamController service = new TeamController();

        [Fact]
        public void PassingTest()
        {
            Assert.Equal(service.Get(), new string[] { "one", "two" });
        }
    }
}