using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WeatherBoard2.ErrorHandling;
using WeatherBoard2.Models;

namespace WeatherBoard2.Services
{
   public class WeatherService: IWeatherService
    {
        private const string APP_ID = "PLACE-YOUR-APP-ID-HERE";
        private HttpClient client;

        public WeatherService()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://api.openweathermap.org/data/2.5/");
        }

        public async Task<CurrentWeatherModel> GetCurrentWeatherAsync(string location)
        {
            var query = $"weather?q={location}&mode=xml&units=metric&appid={APP_ID}";
            //var response = await client.GetAsync(query);

            //var s = await response.Content.ReadAsStringAsync();
            //var x = XElement.Load(new StringReader(s));
            //var model = new CurrentWeatherModel();
            //var data = x.Element.(w => new CurrentWeatherModel
            //{
            //    Description = w.Element("clouds").Attribute("name").Value,
            //    ID = int.Parse(w.Element("city").Attribute("id").Value),
            //    IconID = w.Element("weather").Attribute("icon").Value,
            //    WindSpeed = double.Parse(w.Element("speed").Attribute("value").Value),
            //    Temperature = double.Parse(w.Element("temperature").Attribute("value").Value),

            //});

            //return data;
            CurrentWeatherModel model = new CurrentWeatherModel();
            return model;
        }

        public async Task<IEnumerable<WeatherForecastModel>> GetForecastAsync(string location)
        {
            if (location == null) throw new ArgumentNullException("Location can't be null.");
            if (location == string.Empty) throw new ArgumentException("Location can't be an empty string.");
     

            var query = $"forecast?q={location}&mode=xml&units=metric&appid={APP_ID}";
            var response = await client.GetAsync(query);

            switch (response.StatusCode)
            {
                case HttpStatusCode.Unauthorized:
                    throw new UnauthorizedAPIConnection("Invalid API key.");
                case HttpStatusCode.NotFound:
                    throw new LocationNotFoundException("Location not found.");
                case HttpStatusCode.OK:
                    var s = await response.Content.ReadAsStringAsync();
                    var x = XElement.Load(new StringReader(s));

                    var data = x.Descendants("time").Select(w => new WeatherForecastModel
                    {
                        Description = w.Element("symbol").Attribute("name").Value,
                        ID = int.Parse(w.Element("symbol").Attribute("number").Value),
                        IconID = w.Element("symbol").Attribute("var").Value,
                        Date = DateTime.Parse(w.Attribute("day").Value),
                        WindType = w.Element("windSpeed").Attribute("name").Value,
                        WindSpeed = double.Parse(w.Element("windSpeed").Attribute("mps").Value),
                        WindDirection = w.Element("windDirection").Attribute("code").Value,
                        DayTemperature = double.Parse(w.Element("temperature").Attribute("day").Value),
                        NightTemperature = double.Parse(w.Element("temperature").Attribute("night").Value),
                        MaxTemperature = double.Parse(w.Element("temperature").Attribute("max").Value),
                        MinTemperature = double.Parse(w.Element("temperature").Attribute("min").Value),
                        Pressure = double.Parse(w.Element("pressure").Attribute("value").Value),
                        Humidity = double.Parse(w.Element("humidity").Attribute("value").Value)
                    });

                    return data;
                default:
                    throw new NotImplementedException(response.StatusCode.ToString());
            }
        }

    }
}

