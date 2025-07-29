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
            var actualHash = Services.Helpers.CalculoHash.GenerarHash(input);

            // Assert
            Assert.True(Services.Helpers.CalculoHash.VerificarHash(input, actualHash));
        }
    }
}