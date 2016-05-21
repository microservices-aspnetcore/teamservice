using Xunit;
using System.Collections.Generic;
using FirstMicroservice.Controllers;

namespace FirstMicroservice.UnitTests
{
    public class TestTests
    {
        ValuesController controller = new ValuesController();
        
        [Fact]
        public void PassingTest()
        {
            IEnumerable<string> result = controller.Get();  
            IEnumerator<string> enumerator = result.GetEnumerator();
            enumerator.MoveNext(); 
            Assert.Equal(enumerator.Current, "value1");
        }
    }
}