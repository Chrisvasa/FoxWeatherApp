using Xunit;

namespace WeatherAPI.Tests
{
    public class CalculatorTests
    {
        [Theory]
        [InlineData(1, 4, 5)]
        [InlineData(2, 7, 9)]
        [InlineData(1.5, 12, 13.5)]
        [InlineData(double.MaxValue, 69, double.MaxValue)]
        public void Add_SimpleValuesShouldCalculate(double x, double y, double expected)
        {
            // Arange

            // Act
            double actual = Calculator.Add(x, y);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(63, 9, 7)]
        [InlineData(12, 2, 6)]
        public void Divide_SimpleValuesShouldCalculate(double x, double y, double expected)
        {
            // Arange

            // Act
            double actual = Calculator.Divide(x, y);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Divide_DivideByZero()
        {
            // Arange
            double expected = 0;

            // Act
            double actual = Calculator.Divide(30, 0);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
