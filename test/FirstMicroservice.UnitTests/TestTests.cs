using Xunit;
using System.Collections.Generic;

namespace FirstMicroservice.UnitTests
{
    public class TestTests
    {
        [Fact]
        public void PassingTest()
        {
            Assert.Equal("value1", "value1");
        }
    }
}