using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using Xunit;
using ApiCounter;

namespace WeatherAPI.Tests
{
    public class WeatherForecastTests
    {
        [Theory]
        [InlineData("/weather/stockholm", HttpStatusCode.OK)]
        [InlineData("/weather/gothenburg", HttpStatusCode.OK)]
        [InlineData("/weather/newyork", HttpStatusCode.NotFound)]
        [InlineData("/weather/Stockholm", HttpStatusCode.OK)]
        [InlineData("/weather/gOTHENBURG", HttpStatusCode.OK)]
        [InlineData("/weather/StockHolM", HttpStatusCode.OK)]
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
            var counter = new ApiCallCounter();
            counter.Increment();
            Assert.Equal(1, counter.GetCount());
        }
        [Fact]
        public void GetCount_WhenNoIncrement_ReturnsZero()
        {
            var counter = new ApiCallCounter();
            var count = counter.GetCount();
            Assert.Equal(0, count);
        }
        [Theory]
        [InlineData("/api/healthcheck", HttpStatusCode.OK)]
        [InlineData("/api/healthcheck", HttpStatusCode.InternalServerError)]
        public async Task ApiHealthCheckShouldReturnOK(string endpoint, HttpStatusCode expected)
        {
            // Arrange
            await using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();
            // Act
            HttpResponseMessage actual = await client.GetAsync(endpoint);
            // Assert
            Assert.Equal(expected, actual.StatusCode);
        }
    }




}
