using Microsoft.AspNetCore.Http;
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

        [Theory]
        [InlineData($"/weather/city=Stockholm", "Sunny")]
        public async Task GetWeatherData(string weather, string expected)
        {
            // Arrange
            await using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();
            // Act
            string actual = await client.GetStringAsync(weather);
            // Assert
            Assert.Equal(expected, actual);
        }
        [Fact]
        public async Task TryToSearchForResult_ThrowsException()
        {
            //Arrange
            await using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();
            string expected = "City does not exist!";
            //Act
            var exception = await Assert.ThrowsAsync<HttpRequestException>(async () =>
            {
                await client.GetStringAsync($"/weather/city=Uppsala");
            });
            //string actual = await client.GetStringAsync(expected);
            //Assert
            Assert.Equal(StatusCodes.Status500InternalServerError, (int)exception.StatusCode);
        }
        /*[Theory]
        [InlineData($"/weather/city=Uppsala", "Uppsala")]
        public async Task TryToSearchForFirstResult_ThrowExceptionWhenResultDoesNotExist(string endpoint, string expected)
        {
            // Arrange
            await using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();
            //Act
            var exception = Assert.ThrowsAsync<Exception>(async () => await client.GetStringAsync(endpoint));
            //Assert
            Assert.Equal(expected, exception.Message);
        }*/
    }
}
