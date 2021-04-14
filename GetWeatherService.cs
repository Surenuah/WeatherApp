using System;
using System.Collections.Generic;
using System.Text;
using WeatherService;
using RestSharp;

namespace WeatherServiceGet
{
    public class GetWeatherService
    {
        private string apiConnectionString = "https://api.openweathermap.org/data/2.5/weather";
        private string apiKey = "5fad835f386b830d8d55fa6f472e535c";
        public Root GetWeather(string city)
        {
            var client = new RestClient(apiConnectionString);
            var request = new RestRequest(Method.GET);
            request.AddParameter("q", city);
            request.AddParameter("appid", apiKey);

            var root = client.Execute<Root>(request);
            if (root.IsSuccessful)
            {
                return root.Data;
            }
            return null;
        }
    }
}