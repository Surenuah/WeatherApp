using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherService
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 

    public class Weather
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }

    public class Cities
    {
        public string name { get; set; }
    }

    public class Main
    {
        private double _temp;
        private double _feels_like;
        private double _temp_min;
        private double _temp_max;
        public double temp
        {
            get
            {
                return _temp - 273.15;
            }
            set
            {
                _temp = value;
            }
        }
        public double feels_like
        {
            get
            {
                return _feels_like - 273.15;
            }
            set
            {
                _feels_like = value;
            }
        }
        public double temp_min
        {
            get
            {
                return _temp_min - 273.15;
            }
            set
            {
                _temp_min = value;
            }
        }
        public double temp_max
        {
            get
            {
                return _temp_max - 273.15;
            }
            set
            {
                _temp_max = value;
            }
        }
        public int pressure { get; set; }
        public int humidity { get; set; }
    }

    public class Wind
    {
        public double speed { get; set; }
        public int deg { get; set; }
        public double gust { get; set; }
    }

    public class Clouds
    {
        public int all { get; set; }
    }

    public class Sys
    {
        public int type { get; set; }
        public int id { get; set; }
        public string country { get; set; }
        public int sunrise { get; set; }
        public int sunset { get; set; }
    }

    public class Root
    {
        public List<Weather> weather { get; set; }
        public string @base { get; set; }
        public Main main { get; set; }
        public int visibility { get; set; }
        public Wind wind { get; set; }
        public Clouds clouds { get; set; }
        public int dt { get; set; }
        public Sys sys { get; set; }
        public int timezone { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string message { get; set; }
        public int cod { get; set; }

    }


}
