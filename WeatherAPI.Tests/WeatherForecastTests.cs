﻿using Microsoft.AspNetCore.Mvc.Testing;
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
