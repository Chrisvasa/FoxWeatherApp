using ApiCounter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Net.NetworkInformation;
using WeatherAPI.Models;
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

        [Fact]
        public async Task HealthCheck_ReturnsCorrectMessage()
        {
            // Arrange
            await using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();

            // Act
            var response = await client.GetAsync("/api/healthcheck");

            // Assert
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeAnonymousType(content, new { message = "" });

            Assert.Equal("Api is healthy", result.message);
        }

        [Theory]
        [InlineData("dev.kjeld.io", IPStatus.Success)]
        public async Task HealtCheckShouldReturnOk(string endpoint, IPStatus expected)
        {
            //Arrange
            await using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();

            //Act
            IPStatus actual = await Program.PingServerAsync(endpoint);

            //Assert
            Assert.Equal(expected, actual);
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
        [InlineData("/api/favorite/add/stockholm", HttpStatusCode.OK)]
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
        [InlineData($"/api/favorite/add/", "Stockholm", $"You favorited city: Stockholm")]
        [InlineData($"/api/favorite/add/", "Gothenburg", $"You favorited city: Gothenburg")]
        [InlineData($"/api/favorite/add/", "Göteborg", $"City not found!")]
        public async Task AddFavoriteCity_ShouldReturn_FavoritedCityName(string endpoint, string city, string expectedInput)
        {
            // Arrange
            await using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();
            var expected = JsonConvert.SerializeObject(new {message = $"{expectedInput}"});

            // Act
            HttpResponseMessage response = await client.GetAsync(endpoint + city);
            var actual = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData($"/api/favorite/add/Stockholm", HttpStatusCode.OK)]
        [InlineData($"/api/favorite/add/Gothenburg", HttpStatusCode.OK)]
        [InlineData($"/api/favorite/add/Uppsala", HttpStatusCode.NotFound)]
        public async Task AddFavoriteCity_ShouldReturnOK_IfCityExists(string endpoint, HttpStatusCode expected)
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
        public async Task RemoveFavoriteCity_ShouldReturn_RemovedCityName()
        {
            // Arrange
            await using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();
            string expected = "You unfavorited: Stockholm";

            // Act
            HttpResponseMessage response = await client.GetAsync("/api/favorite/remove/Stockholm");
            var actual = await response.Content.ReadAsStringAsync();

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
