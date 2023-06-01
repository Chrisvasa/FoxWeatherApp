using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace WeatherAPI.Tests
{
    public class WeatherForecastTests
    {
        [Theory]
        [InlineData($"/search/", "Stockholm", $"Your search input is: Stockholm")]
        public async Task MapGetShouldReturnHelloWorld(string endpoint, string city, string expected)
        {
            // Arrange
            await using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();
            // Act
            string actual = await client.GetStringAsync(endpoint + city);
            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
