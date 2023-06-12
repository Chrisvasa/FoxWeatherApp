using Newtonsoft.Json;
using WeatherAPI.Models;
using WeatherAPI.Tests;

namespace WeatherAPI
{
    public class Program
    {
        static ApiCallCounter counter= new ApiCallCounter();
        static Cities cities = new Cities(); // The JSON data gets loaded into these classes
        public static void Main(string[] args)
        {
            LoadJson();
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

            app.MapGet("/weather/{cityName}", (string cityName) =>
            {
                counter.Increment();
                var city = cities.city.Where(x => x.name.Equals(cityName)).FirstOrDefault();
                if (city is null)
                {
                    return Results.NotFound();
                }
            int callCount = counter.GetCount();
            Console.WriteLine($"API Calls Made: {callCount}");
                return Results.Ok(city);
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
    }
}