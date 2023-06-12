﻿using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
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
        [InlineData("api/getcities", "stockholm", "gothenburg")]
        public async Task ShouldReturnAllCities(string endpoint, string city1, string city2)
        {
            //Arrange
            await using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();
            List<string> expected = new List<string> { city1, city2 };

            //Act
            var response = await client.GetAsync(endpoint);
            response.EnsureSuccessStatusCode(); // Ensure the response was successful

            var content = await response.Content.ReadAsStringAsync();
            var actual = JsonConvert.DeserializeObject<List<string>>(content);

            //Assert
            Assert.Equal(expected, actual);
        }
      
        [Theory]
        [InlineData($"/weather/favorite/", "Stockholm", $"Your favorite city is: Stockholm")]
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
    }
}
