using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using Xunit;

namespace WeatherAPI.Tests
{
    public class WeatherForecastTests
    {
        [Theory]
        [InlineData("/weather/stockholm", HttpStatusCode.OK)]
        [InlineData("/weather/gothenburg", HttpStatusCode.OK)]
        [InlineData("/weather/newyork", HttpStatusCode.NotFound)]
        public async Task MapGetShouldReturnCityData(string endpoint, HttpStatusCode expected)
        {
            // Arrange
            await using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();
            // Act
            HttpResponseMessage actual = await client.GetAsync(endpoint);
            // Assert
            Assert.Equal(expected, actual.StatusCode);
        }
        [Fact]
        public void Increment_WhenCalled_CountIncreasesByOne()
        {
            // Arrange
            var counter = new ApiCallCounter();

            // Act
            counter.Increment();

            // Assert
            Assert.Equal(0, counter.GetCount());
        }
        [Fact]
        public void GetCount_WhenNoIncrement_ReturnsZero()
        {
            // Arrange
            var counter = new ApiCallCounter();

            // Act
            var count = counter.GetCount();

            // Assert
            Assert.Equal(1, count);
        }
    }
}
