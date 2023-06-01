using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace WeatherAPI.Tests
{
    public class WeatherForecastTests 
    {
        [Theory]
        [InlineData("/", "Hello world")]
        public async Task MapGetShouldReturnHelloWorld(string endpoint, string expected)
        {
            // Arrange
            await using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();
            // Act
            string actual = await client.GetStringAsync(endpoint);
            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("/greetings", "Hello Bob!", "Bob")]
        public async Task MapGetShouldReturnGreetingToUser(string endpoint, string expected, string name)
        {
            // Arrange
            await using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();
            // Act
            string actual = await client.GetStringAsync(endpoint + "/" + name);
            // Assert
            Assert.Equal(expected, actual);
        }

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
    }
}
