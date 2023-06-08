using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace WeatherAPI.Tests
{
    public class WeatherForecastTests
    {
        [Theory]
        [InlineData($"/search/", "Stockholm", $"Your search input is: Stockholm")]
        public async Task MapGetShouldReturnSearchedCity(string endpoint, string city, string expected)
        {
            // Arrange
            await using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();
            // Act
            string actual = await client.GetStringAsync(endpoint + city);
            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("/city/Stockholm", "Stockholm")]
        [InlineData("/city/Gothenburg", "Gothenburg")]
        public async Task MapGetShouldReturnCityName(string endpoint, string expected)
        {
            // Arrange
            await using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();
            // Act
            string actual = await client.GetStringAsync(endpoint);
            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
