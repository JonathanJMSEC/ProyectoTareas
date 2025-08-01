namespace Tareas.Tests.Helpers
{
    public class CalculoHashTest
    {
        [Fact]
        public void TestHashCalculation()
        {
            // Arrange
            var input = "test input";

            // Act
            var actualHash = API.Services.Helpers.CalculoHash.GenerarHash(input);

            // Assert
            Assert.True(API.Services.Helpers.CalculoHash.VerificarHash(input, actualHash));
        }
    }
}