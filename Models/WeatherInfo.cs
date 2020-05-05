using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Karate_GSP.Models
{
    public class WeatherInfo
    {

        public Sys Sys { get; set; }
        public Main Main { get; set; }

    }
    public class Sys
    {
        public string Country { get; set; }
    }

    public class Weather
    {  
        public string Main { get; set; }
        public string Icon { get; set; }
    }

    public class List
    {
        public List<Weather> Weather { get; set; }
    }

    public class Main
    {
        public double Temp { get; set; }
        public double Feels_like { get; set; }
        public double Temp_min { get; set; }
        public double Temp_max { get; set; }
        public int Humidity { get; set; }
        public int Pressure { get; set; }

    }   
}


