using ApiCounter;
using Newtonsoft.Json;
using System.Net.NetworkInformation;
using WeatherAPI.Models;

namespace WeatherAPI
{
    public class Program
    {
        static ApiCallCounter counter = new ApiCallCounter();//Counter to count api calls
        static Cities cities = new Cities(); // The JSON data gets loaded into these classes

        public static void Main(string[] args)
        {
            LoadJson();
            Task<IPStatus> task = PingServerAsync("dev.kjeld.io");
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(options => options.AddDefaultPolicy(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseHttpsRedirection();
            app.UseAuthorization();

            app.MapGet("/api/weather/{cityName}", async (string cityName) =>
            {
                await Task.Delay(10);
                counter.Increment();
                var city = cities.city.Where(x => x.name.Equals(cityName.ToLower())).FirstOrDefault();
                if (city is null)
                {
                    return Results.NotFound(new {error = $"Sorry, could not find anything about {cityName}"});
                }
                return Results.Ok(city);
            });

            app.MapGet("/api/healthcheck", async () =>
            {
                await Task.Delay(10);
                counter.Increment();
                try
                {
                    return Results.Ok(new { message = "Api is healthy" });
                }
                catch
                {
                    Console.WriteLine($"Api is down.");
                    return Results.NotFound();
                }
            });

            app.MapGet("/api/cities/get", async () =>
            {
                await Task.Delay(10);
                counter.Increment();
                var cityList = cities.city.Select(x => x.name).ToArray();
                if (cityList is null)
                {
                    return Results.NotFound(new { error = "We are unable to fetch any cities at the moment. Please try again later." });
                }
                return Results.Ok(new { cities = cityList });
            });

            //Statuscheck for all api endpoints
            app.MapGet("{endpoint}", async (string endpoint) =>
            {
                await Task.Delay(10);
                counter.Increment();
                try
                {
                    return Results.Ok(new { message = "endpoint is ok!" });
                }
                catch
                {
                    return Results.NotFound();
                }
            });

            app.MapGet("/api/favorite/{favoriteCity}", async (string favoriteCity) =>
            {
                await Task.Delay(10);
                counter.Increment();
                foreach (var city in cities.city)
                {
                    if (city.name == favoriteCity.ToLower())
                    {
                        city.isFavorite = true;
                    }
                    else
                    {
                        city.isFavorite = false;
                    }
                }
                var fav = cities.city.Where(x => x.isFavorite == true).FirstOrDefault();

                if (fav is null)
                {
                    throw new Exception("City not found!");
                }
                return $"Your favorite city is: {favoriteCity}";
            });

            app.MapGet("/api/calls", async () =>
            {
                await Task.Delay(10);
                return counter.GetCount();
            });
            app.UseCors();
            app.Run();
        }
        public static void LoadJson()
        {
            string jsonData = File.ReadAllText("./Data/example.json");
            // Converts the data from the JSON file into classes
            cities = JsonConvert.DeserializeObject<Cities>(jsonData);
        }

        public static async Task<IPStatus> PingServerAsync(string endpoint)
        {
            var hostUrl = endpoint;

            Ping ping = new Ping();

            PingReply result = await ping.SendPingAsync(hostUrl);
            return result.Status;
        }

    }
}