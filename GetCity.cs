using System;
using System.Collections.Generic;
using System.Text;
using WeatherService;
using Newtonsoft.Json;
using RestSharp;

namespace CityServiceGet
{
    public class GetCity
    {
        private string apiConnectionString = "https://raw.githubusercontent.com/lutangar/cities.json/master/cities.json";

        public List<string> FillCity()
        {
            var client = new RestClient(apiConnectionString);
            var request = new RestRequest(Method.GET);
            var temp = client.Execute<Cities>(request);
            var tempContent = JsonConvert.DeserializeObject<List<Cities>>(temp.Content);
            List<string> cityList = new List<string>();
            tempContent.ForEach(x => cityList.Add(x.name));

            if (cityList.Count > 0)
            {
                return cityList;
            }
            return null;
        }
    }
}