using Domain.ValueObjects;
using Shouldly;

namespace TransportTests
{
    public class Test
    {
        [Fact]
        public void Random_Test()
        {
            //Arrange

            Address address = new Address("", "", "", "", "", new GpsCoordinate(3.5, 3.6));
            Address address1 = new Address("", "", "", "", "", new GpsCoordinate(3.5, 3.6));

            //Act

            bool result = address == address1;

            //Assert

            result.ShouldBe(true);
        }
    }
}
