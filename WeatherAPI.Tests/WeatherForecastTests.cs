using ApiCounter;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using Xunit;

namespace WeatherAPI.Tests
{
    public class WeatherForecastTests
    {
        [Theory]
        [InlineData("/api/weather/stockholm", HttpStatusCode.OK)]
        [InlineData("/api/weather/gothenburg", HttpStatusCode.OK)]
        [InlineData("/api/weather/newyork", HttpStatusCode.NotFound)]
        [InlineData("/api/weather/Stockholm", HttpStatusCode.OK)]
        [InlineData("/api/weather/gOTHENBURG", HttpStatusCode.OK)]
        [InlineData("/api/weather/StockHolM", HttpStatusCode.OK)]
        public async Task ApiCall_ShouldReturnOK_IfCityExists(string endpoint, HttpStatusCode expected)
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
        //[InlineData("/api/healthcheck", HttpStatusCode.NotFound)]
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

        [Theory]
        [InlineData("/api/cities/get", new string[] { "stockholm", "gothenburg", "tokyo", "chicago" })]
        public async Task GetCities_ShouldReturnAllCities_AsJSON(string endpoint, string[] cities)
        {
            //Arrange
            await using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();
            List<string> expectedList = cities.ToList();

            //Act
            var response = await client.GetAsync(endpoint);
            response.EnsureSuccessStatusCode(); // Ensure the response was successful
            var actual = await response.Content.ReadAsStringAsync();
            var expected = JsonConvert.SerializeObject(new { cities = expectedList });

            //Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("/api/favorite/stockholm", HttpStatusCode.OK)]
        [InlineData("/api/cities/get", HttpStatusCode.OK)]
        [InlineData("/api/weather/stockholm", HttpStatusCode.OK)]
        public async Task API_ShouldReturnOK(string endpoint, HttpStatusCode expected)
        {
            // Arrange
            await using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();
            // Act

            HttpResponseMessage actual = await client.GetAsync(endpoint);

            // Assert
            Assert.Equal(expected, actual.StatusCode);
        }

        [Theory]
        [InlineData($"/api/favorite/", "Stockholm", $"Your favorite city is: Stockholm")]
        public async Task AddFavoriteCity(string endpoint, string city, string expected)
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
        [InlineData(0)]
        [InlineData(5)]
        [InlineData(10)]
        public void IncrementAndGetCount_ShouldReturnCorrectCount(int expectedCount)
        {
            // Arrange
            var counter = new ApiCallCounter();

            // Act
            for (int i = 0; i < expectedCount; i++)
            {
                counter.Increment();
            }

            int count = counter.GetCount();

            // Assert
            Assert.Equal(expectedCount, count);
        }
    }
}
