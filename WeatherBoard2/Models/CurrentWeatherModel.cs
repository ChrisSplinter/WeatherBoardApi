using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherBoard2.Models
{
    public class CurrentWeatherModel
    {
        public string Description { get; set; }
        public int ID { get; set; }
        public string IconID { get; set; }
        public double WindSpeed { get; set; }
        public double Temperature { get; set; }

        public double FeelsLike { get; set; }

    }
}
