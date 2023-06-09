using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using Xunit;

namespace WeatherAPI.Tests
{
    public class WeatherForecastTests
    {
        [Theory]
        [InlineData("/city/ ", HttpStatusCode.NotFound)]
        [InlineData("/city/Stockholm", HttpStatusCode.NotFound)]
        public async Task TryToSearchForFirstResult_ThrowExceptionWhenResultDoesNotExist(string endpoint, HttpStatusCode expected)
        {
            // Arrange
            await using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();
            //Act
            HttpResponseMessage actual = await client.GetAsync(endpoint);
            //Assert
            Assert.Equal(expected, actual.StatusCode);
        }
    }
}
